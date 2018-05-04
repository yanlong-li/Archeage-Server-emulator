using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Actors
{
    public class ActorQuest : ActorExt
    {
        public ActorQuest()
        {
            this.type = SmartEngine.Network.Map.ActorType.QUEST;
            this.SightRange = 150;
            this.Faction = Factions.QuestNPC;
        }
    }
}
