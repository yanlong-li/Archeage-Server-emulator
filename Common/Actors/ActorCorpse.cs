using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
namespace SagaBNS.Common.Actors
{
    public class ActorCorpse : ActorExt
    {
        public ActorNPC NPC { get; set; }
        public ActorExt Owner { get; set; }
        public int Gold { get; set; }
        public uint TreasureType
        {
            get
            {
                if (Items != null)
                {
                    int maxGrade = 0;
                    bool hasEquip = false;
                    foreach (Item.Item i in Items)
                    {
                        if (i == null || i.BaseData == null)
                            continue;
                        if (i.BaseData.EquipSlot != Inventory.EquipSlot.None)
                            hasEquip = true;
                        if (maxGrade < i.BaseData.Grade)
                            maxGrade = i.BaseData.Grade;
                    }
                    int final = hasEquip ? 16 + maxGrade : maxGrade;
                    return (uint)(final << 8);
                }
                else
                    return 0x100;
            }
        }
        public Item.Item[] Items { get; set; }
        public ushort QuestID { get; set; }
        public byte Step { get; set; }
        public bool PickUp { get; set; }
        public bool ShouldDisappear { get; set; }
        public List<Item.Item> AvailableItems
        {
            get
            {
                if (Items == null)
                    return new List<Item.Item>();
                var items = from item in Items
                            where item != null
                            select item;
                return items.ToList();
            }
        }
        public ulong CurrentPickingPlayer { get; set; }
        public ActorCorpse(ActorNPC npc)
        {
            this.NPC = npc;
            this.type = SmartEngine.Network.Map.ActorType.CORPSE;
            this.SightRange = 0;
            this.Faction = Factions.QuestNPC;
        }
    }
}
