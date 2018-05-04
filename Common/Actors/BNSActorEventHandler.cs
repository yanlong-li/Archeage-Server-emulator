using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network.Map;

namespace SagaBNS.Common.Actors
{
    public abstract class BNSActorEventHandler : ActorEventHandler
    {
        #region ActorEventHandler 成员

        public abstract void OnCreate(bool success);

        public abstract void OnDelete();

        public abstract void OnActorStartsMoving(Actor mActor, MoveArg arg);

        public abstract void OnActorStopsMoving(Actor mActor, MoveArg arg);

        public abstract void OnActorAppears(Actor aActor);

        public abstract void OnActorDisappears(Actor dActor);

        public abstract void OnTeleport(float x, float y, float z);

        public abstract void OnGotVisibleActors(List<Actor> actors);

        public abstract void OnActorEnterPortal(Actor aActor);

        public abstract void OnDie(ActorExt killedBy);

        public abstract void OnSkillDamage(Skills.SkillArg arg, Skills.SkillAttackResult result, int dmg, int bonusCount);

        #endregion
    }
}
