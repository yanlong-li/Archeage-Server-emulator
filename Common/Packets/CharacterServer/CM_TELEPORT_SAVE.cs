using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Actors;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_TELEPORT_SAVE : Packet<CharacterPacketOpcode>
    {
        public CM_TELEPORT_SAVE()
        {
            this.ID = CharacterPacketOpcode.CM_TELEPORT_SAVE;
        }

        public long SessionID
        {
            get
            {
                return GetLong(2);
            }
            set
            {
                PutLong(value, 2);
            }
        }

        public uint CharID
        {
            get
            {
                return GetUInt(10);
            }
            set
            {
                PutUInt(value, 10);
            }
        }

        public List<ushort> TeleportLocations
        {
            get
            {
                short count = GetShort(14);
                List<ushort> list = new List<ushort>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(GetUShort());
                }
                return list;
            }
            set
            {
                PutShort((short)value.Count,14);
                foreach (ushort i in value)
                    PutUShort(i);
            }
        }
    }
}
