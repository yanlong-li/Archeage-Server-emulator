using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Packets;
using SagaBNS.Common.Packets.AccountServer;
using SagaBNS.Common.Account;

namespace SagaBNS.Common.Network
{
    public abstract partial class AccountSession<T> : DefaultClient<AccountPacketOpcode>
    {
        public void AccountLogin(uint accountID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_ACCOUNT_LOGIN p = new CM_ACCOUNT_LOGIN();
            p.SessionID = session;
            p.AccountID = accountID;

            this.Network.SendPacket(p);
        }

        public void AccountLogout(uint accountID, T client)
        {
            long session = Global.PacketSession;
            //packetSessions.Add(session, client);

            CM_ACCOUNT_LOGOUT p = new CM_ACCOUNT_LOGOUT();
            p.SessionID = session;
            p.AccountID = accountID;

            this.Network.SendPacket(p);
        }

        public void AccountSave(Account.Account account, T client)
        {
            long session = Global.PacketSession;
            //packetSessions.Add(session, client);

            CM_ACCOUNT_SAVE p = new CM_ACCOUNT_SAVE();
            p.SessionID = session;
            p.Account = account;

            this.Network.SendPacket(p);
        }

        internal void OnAccountLoginResult(Packets.AccountServer.SM_ACCOUNT_LOGIN_RESULT p)
        {
            long session = p.SessionID;
            T client;
            if (packetSessions.TryRemove(session, out client))
            {
                OnAccountLoginResult(client, p.Result);
            }
        }

        protected abstract void OnAccountLoginResult(T client, AccountLoginResult result);

        internal void OnAccountLogoutNotify(Packets.AccountServer.SM_ACCOUNT_LOGOUT_NOTIFY p)
        {
            uint accountID = p.AccountID;
            OnAccountLogoutNotify(accountID);
        }

        protected abstract void OnAccountLogoutNotify(uint accountID);

        public void RequestAccountInfo(string username, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_ACCOUNT_INFO_REQUEST p = new CM_ACCOUNT_INFO_REQUEST();
            p.SessionID = session;
            p.Username = username;

            this.Network.SendPacket(p);
        }

        public void RequestAccountInfo(uint accountID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_ACCOUNT_INFO_REQUEST_ID p = new CM_ACCOUNT_INFO_REQUEST_ID();
            p.SessionID = session;
            p.AccountID = accountID;

            this.Network.SendPacket(p);
        }

        internal void OnAccountInfo(SM_ACCOUNT_INFO p)
        {
            long session = p.SessionID;
            T client;
            if (packetSessions.TryRemove(session, out client))
            {
                Account.Account acc = null;
                if (p.Result == AccountLoginResult.OK)
                    acc = p.Account;
                OnAccountInfo(client, p.Result, acc);
            }
        }

        protected abstract void OnAccountInfo(T client, AccountLoginResult result, Account.Account acc);
    }
}
