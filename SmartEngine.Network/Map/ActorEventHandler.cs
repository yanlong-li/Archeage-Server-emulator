using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Map
{
    public interface MapEventArgs { }

    public interface ActorEventHandler
    {
        void OnCreate(bool success);

        void OnDelete();

        void OnActorStartsMoving(Actor mActor, MoveArg arg);

        void OnActorStopsMoving(Actor mActor, MoveArg arg);

        void OnActorAppears(Actor aActor);

        void OnActorDisappears(Actor dActor);

        void OnTeleport(float x, float y, float z);

        void OnGotVisibleActors(List<Actor> actors);
    }
}
