using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using SmartEngine.Network;
using SagaBNS.Common.Packets;
using SmartEngine.Network.Utils;

namespace SagaBNS.Common
{
    public class BNSChatNetwork<T> : Network<T>
    {
        public override Network<T> CreateNewInstance(System.Net.Sockets.Socket sock, Dictionary<T, Packet<T>> commandTable, Session<T> client)
        {
            BNSChatNetwork<T> instance = new BNSChatNetwork<T>();
            CreateNewInstance(instance, sock, commandTable, client);
            return instance;
        }

        protected override void OnReceivePacket(byte[] buf)
        {
            int totalLen = BitConverter.ToInt16(buf, 0);
            if (buf.Length >= totalLen)
            {
                try
                {
                    byte[] buf2 = new byte[totalLen - 2];
                    Array.Copy(buf, 2, buf2, 0, totalLen - 2);
                    Packet<T> p = new Packet<T>();
                    p.PutBytes(buf2, 0);
                    ProcessPacket(p);
                    if (totalLen < buf.Length)
                    {
                        int rest = buf.Length - totalLen;
                        buf2 = new byte[rest];
                        Array.Copy(buf, totalLen, buf2, 0, rest);
                        OnReceivePacket(buf2);
                    }
                    else
                        lastContent = null;
                }
                catch (Exception)
                {
                }

            }
            else
                lastContent = buf;
        }

        public override void SendPacket(Packet<T> p, bool noWarper)
        {
            throw new NotImplementedException();
        }

        public override void SendPacket(Packet<T> p)
        {
            if (Disconnected)
                return;

            byte[] buf = new byte[p.Length + 2];
            p.ToArray().CopyTo(buf, 2);
            p.PutUShort((ushort)(p.Length + 2), 0);
            Array.Copy(p.Buffer, 0, buf, 0, 2);
            SendPacketRaw(buf, 0, buf.Length);
        }
    }
}
