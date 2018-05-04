using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Actors
{
    public class ActorItem : ActorExt
    {
        public uint ObjectID { get; set; }
        public ActorExt Creator { get; set; }
        public ulong CorpseID { get; set; }
        public int DisappearTime { get; set; }
        public ActorItem(uint objID)
        {
            this.ObjectID = objID;
            this.DisappearTime = 20000;
            this.type = SmartEngine.Network.Map.ActorType.ITEM;
            this.SightRange = 0;
        }
    }
}
