using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
namespace SagaBNS.Common.Packets.AccountServer
{
    public class CM_LOGIN_REQUEST : Packet<AccountPacketOpcode>
    {
        public CM_LOGIN_REQUEST()
        {
            this.ID = AccountPacketOpcode.CM_LOGIN_REQUEST;
        }

        public string Password
        {
            get
            {
                return GetString(2);
            }
            set
            {
                PutString(value, 2);
            }
        }
    }
}
