using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Item;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_ITEM_SAVE : Packet<CharacterPacketOpcode>
    {
        public CM_ITEM_SAVE()
        {
            this.ID = CharacterPacketOpcode.CM_ITEM_SAVE;
        }

        public Item.Item Item
        {
            get
            {
                uint itemID = GetUInt(2);
                ItemData data = new ItemData();
                data.ItemID = itemID;
                Common.Item.Item item = new Common.Item.Item(data);
                item.ID = GetUInt();
                item.CharID = GetUInt();
                item.SlotID = GetUShort();
                item.Container = (Item.Containers)GetByte();
                item.Count = GetUShort();
                item.Synthesis = GetByte();
                return item;
            }
            set
            {
                PutUInt(value.ItemID, 2);
                PutUInt(value.ID);
                PutUInt(value.CharID);
                PutUShort(value.SlotID);
                PutByte((byte)value.Container);
                PutUShort(value.Count);
                PutByte(value.Synthesis);
            }
        }
    }
}
