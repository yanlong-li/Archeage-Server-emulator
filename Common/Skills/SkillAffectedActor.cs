using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaBNS.Common.Skills;
using SagaBNS.Common.Actors;

namespace SagaBNS.Common.Skills
{
    public class SkillAffectedActor
    {
        public SkillAttackResult Result;
        public ActorExt Target;
        public bool NoDamageBroadcast = false;
        public int Damage;
        public uint BonusAdditionID;
    }
}
