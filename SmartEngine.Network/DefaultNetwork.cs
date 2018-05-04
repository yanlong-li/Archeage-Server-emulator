using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Text;
using SmartEngine.Core;
using SmartEngine.Network.Utils;

namespace SmartEngine.Network
{
    public enum Mode
    {
        Server,
        Client
    }
    /// <summary>
    /// Network Layer IO
    /// </summary>
    /// <typeparam name="T">Packet Opcode Enumeration</typeparam>
    public class DefaultNetwork<T> : Network<T>
    {
        bool ready = false;
        Mode currentMode;
        int expectedKeySize;
        /// <summary>
        /// Create a new IO instance
        /// </summary>
        /// <param name="sock">Socket that needs to be bound</param>
        /// <param name="commandTable">Packet processing registry</param>
        /// <param name="client">Bound client</param>
        public override Network<T> CreateNewInstance(Socket sock, Dictionary<T, Packet<T>> commandTable, Session<T> client)
        {
            DefaultNetwork<T> newInstance = new DefaultNetwork<T>();
            base.CreateNewInstance(newInstance, sock, commandTable, client);
            //newInstance.NoQueueing = true;
            return newInstance;
        }

        /// <summary>
        /// Starts the start of the main packet processing, usually after the encryption key is exchanged
        /// </summary>
        private void StartPacketParsing()
        {
            ready = true; 
            client.Connected = true;
            client.OnConnect();
        }

        /// <summary>
        /// Set the current network layer mode, client or server
        /// </summary>
        /// <param name="mode">Need to set the mode</param>
        public override void SetMode(Mode mode)
        {
            base.SetMode(mode);
            currentMode = mode;
            switch (mode)
            {
                case Mode.Server:
                    expectedKeySize = 8;
                    break;
                case Mode.Client:
                    expectedKeySize = 529;
                    break;
            }
        }

        protected override void OnReceivePacket(byte[] buffer)
        {
            if (!ready)//If encryption system not ready
            {
                if (buffer.Length < expectedKeySize)
                {
                    lastContent = buffer;
                    return;
                }
                byte[] raw = new byte[expectedKeySize];
                Array.Copy(buffer, 0, raw, 0, expectedKeySize);
                lastContent = new byte[buffer.Length - expectedKeySize];
                Array.Copy(buffer, expectedKeySize, lastContent, 0, lastContent.Length);
                switch (expectedKeySize)
                {
                    case 8:
                        {
                            Packet<T> p1 = new Packet<T>(529);
                            p1.PutUInt(1, 4);
                            p1.PutByte(0x32, 8);
                            p1.PutUInt(0x100, 9);
                            Crypt.KeyExchange.MakePrivateKey();
                            string bufstring = Conversions.bytes2HexString(EncryptionKeyExchange.Module.ToByteArray());
                            p1.PutBytes(System.Text.Encoding.ASCII.GetBytes(bufstring.ToLower()), 13);
                            p1.PutUInt(0x100, 269);
                            bufstring = Conversions.bytes2HexString(Crypt.KeyExchange.GetKeyExchangeBytes(Mode.Server));
                            p1.PutBytes(System.Text.Encoding.ASCII.GetBytes(bufstring), 273);
                            SendPacketRaw(p1.Buffer, 0, p1.length);
                            expectedKeySize = 260;
                        }
                        break;
                    case 529:
                        {
                            Packet<T> p1 = new Packet<T>();
                            p1.data = raw;
                            byte[] keyBuf = p1.GetBytes(256, 273);
                            Crypt.KeyExchange.MakePrivateKey();
                            Packet<T> p2 = new Packet<T>(260);
                            p2.PutUInt(0x100, 0);
                            string bufstring = Conversions.bytes2HexString(Crypt.KeyExchange.GetKeyExchangeBytes(Mode.Client));
                            p2.PutBytes(System.Text.Encoding.ASCII.GetBytes(bufstring), 4);
                            SendPacket(p2, true);
                            Crypt.KeyExchange.MakeKey(Mode.Client, keyBuf);
                            StartPacketParsing();
                        }
                        break;
                    case 260:
                        {
                            Packet<T> p1 = new Packet<T>();
                            p1.data = raw;
                            byte[] keyBuf = p1.GetBytes(256, 4);
                            Crypt.KeyExchange.MakeKey(Mode.Server, keyBuf);
                            StartPacketParsing();
                        }
                        break;
                }
            }
            else
            {
                int totalLen = BitConverter.ToInt16(buffer, 0);
                totalLen = (totalLen & 0xfff) * 4;
                if (totalLen > 0x3F00)
                {
                    Logger.ShowWarning("Abnormal Packet Size(>16128) :" + totalLen + ", cleaning buffer");
                    if (lastContent != null)
                    {
                        Packet<T> p = new Packet<T>();
                        p.PutBytes(lastContent, 0);
                        Logger.ShowWarning("OldContent:" + p.DumpData());
                    }
                    lastContent = null;
                    return;
                }
                if (buffer.Length - 2 >= totalLen)
                {
                    try
                    {
                        byte[] buf2 = new byte[totalLen];
                        Array.Copy(buffer, 2, buf2, 0, totalLen);
                        if (encrypt)
                            Crypt.Decrypt(buf2, 0, totalLen);
                        int len = BitConverter.ToInt16(buf2, 0) - 2;
                        byte[] tmp = new byte[len];
                        Array.Copy(buf2, 2, tmp, 0, len);
                        Packet<T> p = new Packet<T>();
                        p.PutBytes(tmp, 0);
                        ProcessPacket(p);
                        if (totalLen < buffer.Length - 2)
                        {
                            int rest = buffer.Length - 2 - totalLen;
                            buf2 = new byte[rest];
                            Array.Copy(buffer, totalLen + 2, buf2, 0, rest);
                            OnReceivePacket(buf2);
                        }
                        else
                            lastContent = null;
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        lastContent = null;
                    }

                }
                else
                    lastContent = buffer;
            }
        }

        public unsafe override void SendPacket(Packet<T> p, bool noWarper)
        {
            if (Disconnected)
                return;
            if (!noWarper)
            {
                int rest = 16 - ((int)(p.Length + 2) % 16);
                int oldSize = (int)p.Length;
                if (rest == 16)
                    rest = 0;
                byte[] buf = new byte[p.Length + rest + 4];
                p.ToArray().CopyTo(buf, 4);
                fixed (byte* ptr = &buf[2])
                {
                    *((ushort*)ptr) = (ushort)(oldSize + 2);
                }
                if (ready && p.Encrypt && Encrypt)
                    Crypt.Encrypt(buf, 2, buf.Length - 2);
                if ((buf.Length - 2) / 4 > 0xfff)
                    throw new NotSupportedException("Packet bigger than 16300 bytes are not supported");
                fixed (byte* ptr = buf)
                {
                    *((ushort*)ptr) = (ushort)(((buf.Length - 2) / 4) & 0xfff ^ 0x8000);
                }
                SendPacketRaw(buf, 0, buf.Length);
            }
            else
                SendPacketRaw(p.Buffer, 0, p.length);
        }

        public override void SendPacket(Packet<T> p)
        {
            SendPacket(p, false);            
        }
    }
}
