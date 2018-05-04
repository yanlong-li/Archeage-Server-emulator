using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaBNS.Common.Item;

namespace SagaBNS.Common.Actors
{
    public class ActorMapObj : ActorCorpse
    {
        public ushort ObjectID { get; set; }
        public bool Available { get; set; }
        public int RespawnTime { get; set; }
        public bool Special { get; set; }
        public bool DragonStream { get; set; }
        public uint SpecialMapID { get; set; }
        public Dictionary<uint, int> ItemIDs { get { return itemIds; } }
        public int MinGold { get; set; }
        public int MaxGold { get; set; }

        Dictionary<uint, int> itemIds = new Dictionary<uint, int>();
        List<Item.Item> items = new List<Item.Item>();
        public ActorMapObj(ushort objID)
            :base(null)
        {
            this.ObjectID = objID;
            this.type = SmartEngine.Network.Map.ActorType.MAPOBJECT;
            this.SightRange = 2000;
            RespawnTime = 60000;
            Available = true;
        }
    }
}
