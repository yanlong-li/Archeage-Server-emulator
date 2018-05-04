using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Actors
{
    public class ActorNPC : ActorExt
    {
        NPCData data;
        public NPCData BaseData
        {
            get { return data; }
        }
        public ActorNPC(NPCData data)
        {
            this.data = data;
            this.type = SmartEngine.Network.Map.ActorType.NPC;
            this.SightRange = data.SightRange;
            this.HP = data.MaxHP;
            this.MaxHP = data.MaxHP;
            this.MP = data.MaxMP;
            this.MaxMP = data.MaxMP;
            this.Level = data.Level;
            this.ManaType = data.ManaType;
            this.Faction = data.Faction;
            this.Speed = 1000;
            this.Status.AtkMin = data.AtkMin;
            this.Status.AtkMax = data.AtkMax;
        }

        public ushort NpcID
        {
            get
            {
                return data.NpcID;
            }
        }

        public ushort StandartMotion { get; set; }

        public int AppearEffect { get; set; }

        public int DisappearEffect { get; set; }

        public int MoveRange { get; set; }

        public short X_Ori { get; set; }
        public short Y_Ori { get; set; }
        public short Z_Ori { get; set; }

    }
}
