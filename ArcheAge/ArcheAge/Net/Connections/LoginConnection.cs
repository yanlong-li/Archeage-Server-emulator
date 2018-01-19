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
            Logger.Trace("连接服务器，安装数据…");
            DisconnectedEvent += LoginConnection_DisconnectedEvent;
            SendAsync(new Net_RegisterGameServer());
            //SendAsync(new test());
        }

        void LoginConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            Logger.Trace("登录服务器{0}：断开连接", this);
            Dispose();
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);
            short opcode = reader.ReadInt16();
            PacketHandler<LoginConnection> handler = DelegateList.LHandlers[opcode];
            if (handler != null)
                handler.OnReceive(this, reader);
            else
                Logger.Trace("收到未定义数据包 0x{0:x2", opcode);
        }
    }
}
