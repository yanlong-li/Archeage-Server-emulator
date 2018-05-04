using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Actors
{
    public class PortalTrigger
    {
        public int Dir { get; set; }
        public ushort Quest { get; set; }
        public int Step { get; set; }
        public uint MapTarget { get; set; }
        public PortalTrigger()
        {
            Step = -1;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
    public class ActorPortal : ActorExt
    {
        List<PortalTrigger> triggers = new List<PortalTrigger>();

        public List<PortalTrigger> PortalTriggers { get { return triggers; } }
        public uint ID { get; set; }

        public ActorPortal()
        {
            this.type = SmartEngine.Network.Map.ActorType.PORTAL;
            this.SightRange = 100;       
        }
    }
}
