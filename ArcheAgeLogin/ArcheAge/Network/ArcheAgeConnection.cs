using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Structuring;
using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons.Utilities;
using System;
using System.Net.Sockets;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Connection For ArcheAge Client - Using Only For Authorization.
    /// </summary>
    public class ArcheAgeConnection : IConnection
    {
        public bool MovedToGame = false;
        public Account CurrentAccount { get; set; }

        public ArcheAgeConnection(Socket socket) : base(socket)
        {
            Logger.Trace("Client IP: {0} connected", this);
            DisconnectedEvent += ArcheAgeConnection_DisconnectedEvent;
            m_LittleEndian = true;
        }

        void ArcheAgeConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            if (CurrentAccount != null)
            {
                if(GameServerController.AuthorizedAccounts.ContainsKey(CurrentAccount.Session)) //AccountID
                {
                    GameServerController.AuthorizedAccounts.Remove(CurrentAccount.Session); //AccountID
                }
                //Removing Account From All GameServers
                foreach (GameServer server in GameServerController.CurrentGameServers.Values)
                {
                    if (server.CurrentAuthorized.Contains(CurrentAccount.AccountId))
                        server.CurrentAuthorized.Remove(CurrentAccount.AccountId);
                }
                if (CurrentAccount.Token!= null) //If you been fully authroized.
                {
                    CurrentAccount.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    AccountHolder.InsertOrUpdate(CurrentAccount);
                }
            }
            string arg = MovedToGame ? "moved to Game" : "disconnected";
            ArcheAgeConnection archeAgeConnection = this;
            Logger.Trace("ArcheAge: {0} {1}", CurrentAccount == null ? archeAgeConnection.ToString() : CurrentAccount.Name, arg);
            Dispose();
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);
            ushort opcode = reader.ReadLEUInt16();
            if (opcode > PacketList.LHandlers.Length)
            {
                Logger.Trace("Not enough length for LHandlers, disposing...");
                Dispose();
                return;
            }

            PacketHandler<ArcheAgeConnection> handler = PacketList.LHandlers[opcode];
            if (handler != null)
                handler.OnReceive(this, reader);
            else
                Logger.Trace("Received undefined packet 0x{0:x2}", opcode);

            reader = null;
        }
    }
}
