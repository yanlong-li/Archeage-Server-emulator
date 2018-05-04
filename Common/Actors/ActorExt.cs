using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SmartEngine.Network.Map;
using SmartEngine.Network.Tasks;

namespace SagaBNS.Common.Actors
{
    public class ActorExt : Actor
    {
        ConcurrentDictionary<ulong, ulong> visibleActors = new ConcurrentDictionary<ulong, ulong>();
        ConcurrentDictionary<string, Task> tasks = new ConcurrentDictionary<string, Task>();
        Status status = new Status();
        public byte Level { get; set; }
        //Use field instead of property because it'll be used in Interlocked
        public int HP;
        //Use field instead of property because it'll be used in Interlocked
        public int MP;
        public int MaxHP { get; set; }
        public ushort MaxMP { get; set; }
        public ManaType ManaType { get; set; }
        public ActorExt FaceTo { get; set; }
        public bool Combat { get; set; }
        public Factions Faction { get; set; }
        public ConcurrentDictionary<ulong,ulong> VisibleActors { get { return visibleActors; } }
        public Status Status { get { return status; } }
        public ConcurrentDictionary<string, Task> Tasks { get { return tasks; } }
    }
}
