using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using ArcheAgeLogin.ArcheAge;
using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Network;
using ArcheAgeLogin.ArcheAge.Structuring;
using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons;
using LocalCommons.Utilities;
using System.Diagnostics;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Connection For ArcheAge Client - Using Only Fpr Authorization.
    /// </summary>
    public class ArcheAgeConnection : IConnection
    {
        public bool movedToGame = false;
        public Account CurrentAccount { get; set; }

        public ArcheAgeConnection(Socket s) : base(s)
        {
            CurrentAccount =  AccountHolder.AccountList.FirstOrDefault(n => n.Name =="-=zZZz=-");
            Logger.Trace("Client: <<{0}>> connection", this);
            DisconnectedEvent += ArcheAgeConnection_DisconnectedEvent;
            m_LittleEndian = true;
        }

        void ArcheAgeConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            if (CurrentAccount != null)
            {
                if(GameServerController.AuthorizedAccounts.ContainsKey(CurrentAccount.AccountId)) //AccountId
                {
                    GameServerController.AuthorizedAccounts.Remove(CurrentAccount.AccountId);
                }
                //Removing Account From All GameServers
                foreach (GameServer server in GameServerController.CurrentGameServers.Values)
                {
                    if (server.CurrentAuthorized.Contains(CurrentAccount.AccountId))
                        server.CurrentAuthorized.Remove(CurrentAccount.AccountId);
                }
                if (CurrentAccount.Password != null) //If you been fully authroized.
                {
                    CurrentAccount.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    AccountHolder.InsertOrUpdate(CurrentAccount);
                }
            }
            string arg = movedToGame ? "entered the game " : "disconnect";
            ArcheAgeConnection archeAgeConnection = this;
            Logger.Trace("Client {0} : {1}", archeAgeConnection.CurrentAccount == null ? archeAgeConnection.ToString() : archeAgeConnection.CurrentAccount.Name, arg);
            Dispose();
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);

            //Logger.Trace("Allocated Memory = " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1000000) + " MB");

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
