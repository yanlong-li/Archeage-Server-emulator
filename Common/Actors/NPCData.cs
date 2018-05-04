using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Actors
{
    public struct NPCDeathSpawn
    {
        public ushort NPCID, Motion;
        public int AppearEffect, Count;
    }
    public class NPCData
    {
        Dictionary<uint, Skills.Skill> skills = new Dictionary<uint, Skills.Skill>();
        Dictionary<uint, int> items = new Dictionary<uint, int>();
        Dictionary<uint, ushort> itemCounts = new Dictionary<uint, ushort>();
        List<ushort> quests = new List<ushort>();
        List<NPCDeathSpawn> deathSpawns = new List<NPCDeathSpawn>();
        byte[] steps = new byte[0];
        public NPCData()
        {
            Faction = Factions.FriendlyNPC;
            SightRange = 300;
            AggroRange = 150;
            CombatThinkPeriod = 2000;
        }
        public ushort NpcID { get; set; }
        public string Name { get; set; }
        public ManaType ManaType { get; set; }
        public byte Level { get; set; }
        public int MaxHP { get; set; }
        public ushort MaxMP { get; set; }
        public ushort SightRange { get; set; }
        public ushort AggroRange { get; set; }
        public uint CorpseItemID { get; set; }
        public uint StoreID { get; set; }
        public uint StoreByItemID { get; set; }
        public Factions Faction { get; set; }
        public Dictionary<uint, Skills.Skill> Skill { get { return skills; } }
        public Dictionary<uint, int> Items { get { return items; } }
        public Dictionary<uint, ushort> ItemCounts { get { return itemCounts; } }
        public List<ushort> QuestIDs { get { return quests; } }
        public List<NPCDeathSpawn> DeathSpawns { get { return deathSpawns; } }
        public byte[] QuestSteps { get { return steps; } set { steps = value; } }
        public int CombatThinkPeriod { get; set; }
        public bool NoPushBack { get; set; }
        public bool NoMove { get; set; }
        public int AtkMin { get; set; }
        public int AtkMax { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", NpcID, Name);
        }
    }
}
