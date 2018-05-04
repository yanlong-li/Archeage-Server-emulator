using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ArcheAge.ArcheAge.Net.Connections
{
    /// <summary>
    /// Connection That Used For Login Server Connection.
    /// </summary>
    public class LoginConnection : IConnection
    {
        public LoginConnection(Socket socket) : base(socket) {
            Logger.Trace("Connection server, installation data...");
            DisconnectedEvent += LoginConnection_DisconnectedEvent;
            SendAsync(new Net_RegisterGameServer());
            //SendAsync(new test());
        }

        void LoginConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            Logger.Trace("Login server {0}: disconnected", this);
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
                Logger.Trace("Undefined packet received 0x{0:x2", opcode);
        }
    }
}
