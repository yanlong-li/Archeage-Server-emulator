using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Item;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_ITEM_INVENTORY_GET : Packet<CharacterPacketOpcode>
    {
        public CM_ITEM_INVENTORY_GET()
        {
            this.ID = CharacterPacketOpcode.CM_ITEM_INVENTORY_GET;
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

        public uint CharID
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
