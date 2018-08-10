using LocalCommons.Logging;
using LocalCommons.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace ArcheAgeStream.ArcheAge.Network
{
    /// <summary>
    /// Connection That Used Only For Interact With Game Servers.
    /// </summary>
    public class StreamConnection : IConnection
    {
        private StreamServer m_CurrentInfo;

        public StreamServer CurrentInfo
        {
            get { return m_CurrentInfo; }
            set { m_CurrentInfo = value; }
        }

        public StreamConnection(Socket socket) : base(socket) 
        {
            Logger.Trace("Client IP: {0} connected", this);
            DisconnectedEvent += GameConnection_DisconnectedEvent;
            m_LittleEndian = true;
        }

        void GameConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            Logger.Trace("Client IP: {0} disconnected", m_CurrentInfo != null ? m_CurrentInfo.Id.ToString() : this.ToString());
            Dispose();
            //StreamServerController.DisconnecteStreamServer(m_CurrentInfo != null ? m_CurrentInfo.Id : this.CurrentInfo.Id);
            m_CurrentInfo = null;
            //Offline the game server's status
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);

            //Logger.Trace("Allocated Memory = " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1000000) + " MB");

            ushort opcode = reader.ReadLEUInt16();
            PacketHandler<StreamConnection> handler = PacketList.GHandlers[opcode];
            if (handler != null) {
                handler.OnReceive(this, reader);
            }
            else
                Logger.Trace("Received Undefined StreamServer Packet 0x{0:X2}", opcode);
            reader = null;
        }
    }
}
