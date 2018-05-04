using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
namespace SagaBNS.Common.Packets.AccountServer
{
    public class CM_ACCOUNT_INFO_REQUEST_ID : Packet<AccountPacketOpcode>
    {
        public CM_ACCOUNT_INFO_REQUEST_ID()
        {
            this.ID = AccountPacketOpcode.CM_ACCOUNT_INFO_REQUEST_ID;
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

        public uint AccountID
        {
            get
            {
                return GetUInt(10);
            }
            set
            {
                PutUInt(value, 10);
            }
        }
    }
}
