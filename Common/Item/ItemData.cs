using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaBNS.Common.Actors;

namespace SagaBNS.Common.Item
{
    public class ItemData
    {
        public ItemType ItemType { get; set; }
        public Inventory.EquipSlot EquipSlot { get; set; }
        public uint ItemID { get; set; }
        public uint Price { get; set; }
        public byte RequiredLevel { get; set; }
        public short ItemLevel { get; set; }
        public Job RequiredJob { get; set; }
        public ushort MaxDurability { get; set; }
        public int MaxStackableCount { get; set; }
        public uint BaGuaPowderId { get; set; }
        public uint BaGuaPowderCount { get; set; }
        public uint BaGuaPowderIdWep { get; set; }
        public uint BaGuaPowderCountWep { get; set; }
        public uint CastSkill { get; set; }
        public bool CanDispose { get; set; }
        public bool CanSell { get; set; }
        public bool CanTrade { get; set; }
        public byte Grade { get; set; }
        public byte Faction { get; set; }
        public int BaoPaiSetNumber { get; set; }
        public Dictionary<Stats, int> PrimaryStats = new Dictionary<Stats, int>();
        public Dictionary<Stats, int> SecondaryStats = new Dictionary<Stats, int>();
    }
}
