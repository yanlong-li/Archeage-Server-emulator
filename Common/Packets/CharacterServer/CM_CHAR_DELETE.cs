using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Actors;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_CHAR_DELETE : Packet<CharacterPacketOpcode>
    {
        public CM_CHAR_DELETE()
        {
            this.ID = CharacterPacketOpcode.CM_CHAR_DELETE;
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
            get { return GetUInt(10); }
            set { PutUInt(value, 10); }
        }
    }
}
