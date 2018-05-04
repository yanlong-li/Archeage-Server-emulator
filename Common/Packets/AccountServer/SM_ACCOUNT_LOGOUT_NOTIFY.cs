using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Network;
namespace SagaBNS.Common.Packets.AccountServer
{
    public class SM_ACCOUNT_LOGOUT_NOTIFY : Packet<AccountPacketOpcode>
    {
        internal class SM_ACCOUNT_LOGOUT_NOTIFY_INTERNAL<T> : SM_ACCOUNT_LOGOUT_NOTIFY
        {
            public override Packet<AccountPacketOpcode> New()
            {
                return new SM_ACCOUNT_LOGOUT_NOTIFY_INTERNAL<T>();
            }

            public override void OnProcess(Session<AccountPacketOpcode> client)
            {
                ((AccountSession<T>)client).OnAccountLogoutNotify(this);
            }
        }
        public SM_ACCOUNT_LOGOUT_NOTIFY()
        {
            this.ID = AccountPacketOpcode.SM_ACCOUNT_LOGOUT_NOTIFY;
        }

        public uint AccountID
        {
            get
            {
                return GetUInt(2);
            }
            set
            {
                PutUInt(value, 2);
            }
        }
    }
}
