using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using SmartEngine.Core;
using SmartEngine.Network;
using SagaBNS.Common.Packets;

namespace SagaBNS.Common
{
    public class BNSLoginNetwork<T> : Network<T>
    {
        new string lastContent = "";
        public override Network<T> CreateNewInstance(System.Net.Sockets.Socket sock, Dictionary<T, Packet<T>> commandTable, Session<T> client)
        {
            BNSLoginNetwork<T> instance = new BNSLoginNetwork<T>();
            CreateNewInstance(instance, sock, commandTable, client);
            return instance;
        }

        protected override void OnReceivePacket(byte[] buffer)
        {
            if (Crypt.KeyExchange.IsReady)
                Crypt.Decrypt(buffer, 0, buffer.Length);
            string content = lastContent + Encoding.UTF8.GetString(buffer);
            try
            {
                ParseContent(content);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        void Receive_Completed(object sender, System.Net.Sockets.SocketAsyncEventArgs e)
        {
        /*    if (e.BytesTransferred > 0)
            {
                byte[] buf = new byte[e.BytesTransferred];
                Array.Copy(e.Buffer, e.Offset, buf, 0, e.BytesTransferred);
                if (Crypt.KeyExchange.IsReady)
                    Crypt.Decrypt(buf, 0, buf.Length);
                string content = lastContent + Encoding.UTF8.GetString(buf);
                try
                {
                    ParseContent(content);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            if (e.SocketError != SocketError.Success)
            {
                if (e.SocketError != SocketError.ConnectionReset)
                    Logger.ShowError(new SocketException((int)e.SocketError));
                if (autoLock)
                    ClientManager.EnterCriticalArea();
                Disconnect();
                if (autoLock)
                    ClientManager.LeaveCriticalArea();
                return;
            }
            try
            {
                e.SetBuffer(receiveBuffer, 0, 0x2000);
                if(!sock.ReceiveAsync(e))
                {
                    if (e.BytesTransferred > 0)
                    {
                        byte[] buf = new byte[e.BytesTransferred];
                        Array.Copy(e.Buffer, e.Offset, buf, 0, e.BytesTransferred);
                        if (Crypt.KeyExchange.IsReady)
                            Crypt.Decrypt(buf, 0, buf.Length);
                        string content = lastContent + Encoding.UTF8.GetString(buf);
                        ParseContent(content);
                    }
                    if (e.SocketError != SocketError.Success)
                    {
                        if (e.SocketError != SocketError.ConnectionReset)
                            Logger.ShowError(new SocketException((int)e.SocketError));
                        if (autoLock)
                            ClientManager.EnterCriticalArea();
                        Disconnect();
                        if (autoLock)
                            ClientManager.LeaveCriticalArea();
                        return;
                    }
                }
            }
            catch
            {
                if (autoLock)
                    ClientManager.EnterCriticalArea();
                Disconnect();
                if (autoLock)
                    ClientManager.LeaveCriticalArea();
            }*/
        }

        void ParseContent(string content)
        {
            System.IO.StringReader sr = new System.IO.StringReader(content);
            string header = sr.ReadLine();
            int length = int.Parse(sr.ReadLine().Split(':')[1]);
            string tmp = sr.ReadLine();
            string serial="";
            if (tmp != "")
            {
                serial = tmp;
                do
                {
                    tmp = sr.ReadLine();
                } while (tmp != "" && sr.Peek() != -1);
            }

            byte worldID;
            LoginPacketOpcode opcode = ParseHeader(header, out worldID);
            if (opcode == LoginPacketOpcode.Unknown)
            {
                Logger.ShowWarning("Received Unknown Packet:");
                Logger.ShowWarning(content);
            }
            byte[] buf = Encoding.UTF8.GetBytes(sr.ReadToEnd());
            if (buf.Length >= length)
            {
                byte[] tmp2 = new byte[length];
                Array.Copy(buf, 0, tmp2, 0, length);

                Packet<T> p = new Packet<T>();
                p.ID = (T)(object)(int)opcode;
                p.PutInt(serial != "" ? int.Parse(serial.Split(':')[1]) : 0, 2);
                p.PutByte(worldID);
                p.PutBytes(tmp2, 7);

                ProcessPacket(p);
                if (length < buf.Length)
                    ParseContent(Encoding.UTF8.GetString(buf, length, buf.Length - length));
                else
                    lastContent = "";
            }
            else
                lastContent = content;
        }

        LoginPacketOpcode ParseHeader(string header ,out byte worldID)
        {
            string[] token = header.Split(' ');
            return GetOpcode(token[1], out worldID);
        }

        public static LoginPacketOpcode GetOpcode(string header,out byte worldID)
        {
            worldID = 1;
            if (header.Contains("/Game.bns."))
            {
                string[] token = header.Split('/');
                worldID = byte.Parse(token[1].Substring(9));
                token[1] = token[1].Substring(0, 8);
                header = "/" + token[1] + "/" + token[2];
            }
            switch (header)
            {
                case "/Sts/Connect":
                    return LoginPacketOpcode.CM_STS_CONNECT;
                case "/Auth/LoginStart":
                    return LoginPacketOpcode.CM_AUTH_LOGIN_START;
                case "/Auth/KeyData":
                    return LoginPacketOpcode.CM_AUTH_KEY_DATA;
                case "/Auth/LoginFinish":
                    return LoginPacketOpcode.CM_AUTH_LOGIN_FINISH;
                case "/Auth/RequestToken":
                    return LoginPacketOpcode.CM_AUTH_TOKEN;
                case "/Auth/RequestGameToken":
                    return LoginPacketOpcode.CM_AUTH_GAME_TOKEN;
                case "/GameAccount/ListMyAccounts":
                    return LoginPacketOpcode.CM_ACCOUNT_LIST;
                case "/World/ListWorlds":
                    return LoginPacketOpcode.CM_WORLD_LIST;
                case "/Slot/ListCharSlots":
                    return LoginPacketOpcode.CM_CHAR_LIST;
                case "/Slot/GetCharSlot":
                    return LoginPacketOpcode.CM_CHAR_SLOT_REQUEST;
                case "/Game.bns/CreatePC":
                    return LoginPacketOpcode.CM_CHAR_CREATE;
                case "/Game.bns/DeletePC":
                    return LoginPacketOpcode.CM_CHAR_DELETE;
                case "/Sts/Ping":
                    return LoginPacketOpcode.CM_PING;
                case "/Slot/ListSlots":
                    return LoginPacketOpcode.CM_SLOT_LIST;
            }
            return LoginPacketOpcode.Unknown;
        }

        public override void SendPacket(Packet<T> p, bool noWarper)
        {
            throw new NotImplementedException();
        }

        public override void SendPacket(Packet<T> p)
        {
            byte[] buf = p.GetBytes((ushort)(p.Length - 2), 2);
            if (Crypt.KeyExchange.IsReady && p.Encrypt)
                Crypt.Encrypt(buf, 0, buf.Length);
            SendPacketRaw(buf, 0, buf.Length);
        }
    }
}
