using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Network;
namespace SagaBNS.Common.Packets.AccountServer
{
    public class SM_ACCOUNT_LOGIN_RESULT : Packet<AccountPacketOpcode>
    {
        internal class SM_ACCOUNT_LOGIN_RESULT_INTERNAL<T> : SM_ACCOUNT_LOGIN_RESULT
        {
            public override Packet<AccountPacketOpcode> New()
            {
                return new SM_ACCOUNT_LOGIN_RESULT_INTERNAL<T>();
            }

            public override void OnProcess(Session<AccountPacketOpcode> client)
            {
                ((AccountSession<T>)client).OnAccountLoginResult(this);
            }
        }

        public SM_ACCOUNT_LOGIN_RESULT()
        {
            this.ID = AccountPacketOpcode.SM_ACCOUNT_LOGIN_RESULT;
        }

        public long SessionID
        {
            get
            {
                return GetLong(2);
            }
            set
            {
                PutLong(value, 2);
            }
        }

        public AccountLoginResult Result
        {
            get
            {
                return (AccountLoginResult)GetByte(10);
            }
            set
            {
                PutByte((byte)value, 10);
            }
        }
    }
}
