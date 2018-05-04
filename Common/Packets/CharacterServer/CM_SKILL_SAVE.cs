using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Actors;
using SagaBNS.Common.Skills;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_SKILL_SAVE : Packet<CharacterPacketOpcode>
    {
        public CM_SKILL_SAVE()
        {
            this.ID = CharacterPacketOpcode.CM_SKILL_SAVE;
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

        public List<Skill> Skills
        {
            get
            {
                short count = GetShort(14);
                List<Skill> list = new List<Skill>();
                for (int i = 0; i < count; i++)
                {
                    SkillData data = new SkillData();
                    data.ID = GetUInt();
                    list.Add(new Skill(data));
                }
                return list;
            }
            set
            {
                PutShort((short)value.Count, 14);
                foreach (Skill i in value)
                    PutUInt(i.ID);
            }
        }
    }
}
