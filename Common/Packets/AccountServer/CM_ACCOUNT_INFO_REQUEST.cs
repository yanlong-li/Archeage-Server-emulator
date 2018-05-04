using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
namespace SagaBNS.Common.Packets.AccountServer
{
    public class CM_ACCOUNT_INFO_REQUEST : Packet<AccountPacketOpcode>
    {
        public CM_ACCOUNT_INFO_REQUEST()
        {
            this.ID = AccountPacketOpcode.CM_ACCOUNT_INFO_REQUEST;
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

        public string Username
        {
            get
            {
                return GetString(10);
            }
            set
            {
                PutString(value, 10);
            }
        }
    }
}
