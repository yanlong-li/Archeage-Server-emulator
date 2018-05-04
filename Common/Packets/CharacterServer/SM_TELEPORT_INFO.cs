using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Actors;
using SagaBNS.Common.Skills;
using SagaBNS.Common.Network;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class SM_TELEPORT_INFO : Packet<CharacterPacketOpcode>
    {
        internal class SM_TELEPORT_INFO_INTERNAL<T> : SM_TELEPORT_INFO
        {
            public override Packet<CharacterPacketOpcode> New()
            {
                return new SM_TELEPORT_INFO_INTERNAL<T>();
            }

            public override void OnProcess(Session<CharacterPacketOpcode> client)
            {
                ((CharacterSession<T>)client).OnTeleportInfo(this);
            }
        }
        public SM_TELEPORT_INFO()
        {
            this.ID = CharacterPacketOpcode.SM_TELEPORT_INFO;
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

        public List<ushort> TeleportLocations
        {
            get
            {
                short count = GetShort(10);
                List<ushort> list = new List<ushort>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(GetUShort());
                }
                return list;
            }
            set
            {
                PutShort((short)value.Count,10);
                foreach (ushort i in value)
                    PutUShort(i);
            }
        }
    }
}
