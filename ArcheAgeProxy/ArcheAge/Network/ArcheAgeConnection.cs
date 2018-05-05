using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using LocalCommons.Native.Significant;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using ArcheAgeAuth.ArcheAge.Structuring;

namespace ArcheAgeAuth.ArcheAge.Network
{
    /// <summary>
    /// Connection For ArcheAge Client - Using Only Fpr Authorization.
    /// </summary>
    public class ArcheAgeConnection : IConnection
    {
        private Account m_CurrentAccount;

        public bool movedToGame = false;    
        public Account CurrentAccount
        {
            get
            {
                return m_CurrentAccount;
            }
            set { m_CurrentAccount = value; }
        }

        public ArcheAgeConnection(Socket s) : base(s)
        {
            CurrentAccount =  AccountHolder.AccountList.FirstOrDefault(n => n.Name =="a");
            Logger.Trace("Client: <<{0}>>connection", this);
            DisconnectedEvent += ArcheAgeConnection_DisconnectedEvent;
            m_LittleEndian = true;
        }

        void ArcheAgeConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            if (m_CurrentAccount != null)
            {
                if(GameServerController.AuthorizedAccounts.ContainsKey(m_CurrentAccount.AccountId)){
                    GameServerController.AuthorizedAccounts.Remove(m_CurrentAccount.AccountId);
                }
                //Removing Account From All GameServers
                foreach (GameServer server in GameServerController.CurrentGameServers.Values)
                {
                    if (server.CurrentAuthorized.Contains(m_CurrentAccount.AccountId))
                        server.CurrentAuthorized.Remove(m_CurrentAccount.AccountId);
                }
                if (m_CurrentAccount.Password != null) //If you been fully authroized.
                {
                    m_CurrentAccount.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    AccountHolder.InsertOrUpdate(m_CurrentAccount);
                }
            }
            string arg = movedToGame ? "entered the game " : "disconnect";
            Logger.Trace("Clients {0} : {1}", m_CurrentAccount == null ? this.ToString() : m_CurrentAccount.Name, arg);
            Dispose();
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);
            short opcode = reader.ReadLEInt16();
            if (opcode > PacketList.LHandlers.Length)
            {
                Logger.Trace("Not enough length to handle.");
                Dispose();
                return;
            }

            PacketHandler<ArcheAgeConnection> handler = PacketList.LHandlers[opcode];
            if (handler != null)
                handler.OnReceive(this, reader);
            else
                Logger.Trace("Received undefined package 0x{0:x2}", opcode);

            reader = null;
        }
    }
}
