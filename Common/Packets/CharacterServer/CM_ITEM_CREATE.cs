using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Item;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_ITEM_CREATE : Packet<CharacterPacketOpcode>
    {
        public CM_ITEM_CREATE()
        {
            this.ID = CharacterPacketOpcode.CM_ITEM_CREATE;
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

        public Item.Item Item
        {
            get
            {
                uint itemID = GetUInt(10);
                ItemData data = new ItemData();
                data.ItemID = itemID;
                Common.Item.Item item = new Common.Item.Item(data);
                item.CharID = GetUInt();
                item.SlotID = GetUShort();
                item.Container = (Containers)GetByte();
                item.Count = GetUShort();
                item.Synthesis = GetByte();
                return item;
            }
            set
            {
                PutUInt(value.ItemID, 10);
                PutUInt(value.CharID);
                PutUShort(value.SlotID);
                PutByte((byte)value.Container);
                PutUShort(value.Count);
                PutByte(value.Synthesis);
            }
        }
    }
}
