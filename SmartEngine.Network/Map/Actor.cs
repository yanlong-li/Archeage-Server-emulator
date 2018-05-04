using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Core.Math;
namespace SmartEngine.Network.Map
{
    public enum ActorType
    {
        PC,
        NPC,
        MAPOBJECT,
        ITEM,
        CORPSE,
        PORTAL,
        QUEST,
        PET,
        SKILL,
        SHADOW,
        EVENT,
        FURNITURE,
        GOLEM,
    }

    public class Actor
    {
        ushort dir;
        uint mapID;
        string name;
        ulong id;
        int region;
        bool invisible;
        protected ActorType type;
        ActorEventHandler e;
        ushort speed = 2000, sightRange;
        public DateTime moveCheckStamp;

        public ulong ActorID { get { return id; } set { this.id = value; } }

        public bool Invisible { get { return invisible; } set { this.invisible = value; } }

        public ActorType ActorType { get { return type; } }

        public ActorEventHandler EventHandler { get { return e; } set { e = value; } }

        public int Region { get { return region; } set { this.region = value; } }

        public string Name { get { return name; } set { this.name = value; } }

        public uint MapID { get { return this.mapID; } set { this.mapID = value; } }

        public uint MapInstanceID { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public ushort Dir { get { return dir; } set { this.dir = value; } }

        public ushort Speed { get { return speed; } set { this.speed = value; } }

        public ushort SightRange { get { return sightRange; } set { this.sightRange = value; } }

        public int DistanceToActor(Actor a2)
        {
            if (a2 == null)
                return int.MaxValue;
            int dX = a2.X - this.X;
            int dY = a2.Y - this.Y;
            int dZ = a2.Z - this.Z;
            return (int)(Math.Sqrt(dX * dX + dY * dY + dZ * dZ));
        }

        public int DistanceToPoint2D(int x, int y)
        {
            int dX = x - this.X;
            int dY = y - this.Y;
            return (int)(Math.Sqrt(dX * dX + dY * dY));
        }

        public int DistanceToPoint(int x, int y, int z)
        {
            int dX = x - this.X;
            int dY = y - this.Y;
            int dZ = z - this.Z;
            return (int)(Math.Sqrt(dX * dX + dY * dY + dZ * dZ));
        }
    }
}
