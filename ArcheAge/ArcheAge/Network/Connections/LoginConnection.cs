using LocalCommons.Logging;
using LocalCommons.Network;
using System;
using System.Net.Sockets;

namespace ArcheAge.ArcheAge.Network.Connections
{
    /// <summary>
    /// Connection That Used For Login Server Connection.
    /// </summary>
    public class LoginConnection : IConnection
    {
        public LoginConnection(Socket socket) : base(socket)
        {
            Logger.Trace("Connected to LoginServer, installing data...");
            DisconnectedEvent += LoginConnection_DisconnectedEvent;
            SendAsync(new Net_RegisterGameServer());
        }

        void LoginConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            Logger.Trace("LoginServer IP: {0} disconnected", this);
            Dispose();
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);
            short opcode = reader.ReadLEInt16();
            PacketHandler<LoginConnection> handler = DelegateList.LHandlers[opcode];
            if (handler != null)
                handler.OnReceive(this, reader);
            else
                Logger.Trace("Received Undefined Packet 0x{0:x2}", opcode);
        }
    }
}
