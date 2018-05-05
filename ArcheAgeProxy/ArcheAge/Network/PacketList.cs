using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ArcheAgeProxy.Properties;

namespace ArcheAgeProxy.ArcheAge.Network
{
    /// <summary>
    /// Packet List That Contains All Game / Client Packet Delegates.
    /// </summary>
    public static class PacketList
    {
        private static int m_Maintained;
        private static PacketHandler<ProxyConnection>[] m_GHandlers;
        //private static string   clientVersion;

        public static PacketHandler<ProxyConnection>[] GHandlers
        {
            get { return m_GHandlers; }
        }

        public static void Initialize()
        {
            m_GHandlers = new PacketHandler<ProxyConnection>[0x20];

            Registration();
        }

        private static void Registration()
        {
            //Proxy Server Packets
            Register(0x01, new OnPacketReceive<ProxyConnection>(Handle_RegisterProxyServer));
            Register(0x02, new OnPacketReceive<ProxyConnection>(Handle_Packet0x02));
        }


        #region Game Server Delegates
        private static void Handle_RegisterProxyServer(ProxyConnection net, PacketReader reader)
        {
            byte id = reader.ReadByte();
            int AccountID = reader.ReadLEInt32();
            int unk1 = reader.ReadLEInt32();
            int cookie = reader.ReadLEInt32();


            byte success = 0; //GameServerController.RegisterGameServer(id, password, net, port, ip);
            net.SendAsync(new NET_GameRegistrationResult(success));
        }
        private static void Handle_Packet0x02(ProxyConnection net, PacketReader reader)
        {
            byte id = reader.ReadByte();
            int AccountID = reader.ReadLEInt32();
            int unk1 = reader.ReadLEInt32();
            int cookie = reader.ReadLEInt32();


            byte success = 0; //GameServerController.RegisterGameServer(id, password, net, port, ip);
            net.SendAsync(new NET_Result0x09(success));
        }
        #endregion

        private static void Register(ushort opcode, OnPacketReceive<ProxyConnection> e)
        {
            m_GHandlers[opcode] = new PacketHandler<ProxyConnection>(opcode, e);
            m_Maintained++;
        }
    }
}
