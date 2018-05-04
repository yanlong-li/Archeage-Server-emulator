using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Network;
namespace SagaBNS.Common.Packets.AccountServer
{
    public class SM_LOGIN_RESULT : Packet<AccountPacketOpcode>
    {
        public enum Results
        {
            OK,
            WRONG_PASSWORD,
        }
        internal class SM_LOGIN_RESULT_INTERNAL<T> : SM_LOGIN_RESULT
        {
            public override Packet<AccountPacketOpcode> New()
            {
                return new SM_LOGIN_RESULT_INTERNAL<T>();
            }

            public override void OnProcess(Session<AccountPacketOpcode> client)
            {
                ((AccountSession<T>)client).OnLoginResult(this);
            }
        }
        public SM_LOGIN_RESULT()
        {
            this.ID = AccountPacketOpcode.SM_LOGIN_RESULT;
        }

        public Results Result
        {
            get
            {
                return (Results)GetByte(2);
            }
            set
            {
                PutByte((byte)value, 2);
            }
        }
    }
}
