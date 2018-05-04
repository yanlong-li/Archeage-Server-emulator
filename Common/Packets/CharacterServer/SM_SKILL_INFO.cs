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
    public class SM_SKILL_INFO : Packet<CharacterPacketOpcode>
    {
        internal class SM_SKILL_INFO_INTERNAL<T> : SM_SKILL_INFO
        {
            public override Packet<CharacterPacketOpcode> New()
            {
                return new SM_SKILL_INFO_INTERNAL<T>();
            }

            public override void OnProcess(Session<CharacterPacketOpcode> client)
            {
                ((CharacterSession<T>)client).OnSkillInfo(this);
            }
        }
        public SM_SKILL_INFO()
        {
            this.ID = CharacterPacketOpcode.SM_SKILL_INFO;
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

        public List<Skill> Skills
        {
            get
            {
                short count = GetShort(10);
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
                PutShort((short)value.Count);
                foreach (Skill i in value)
                    PutUInt(i.ID);
            }
        }
    }
}
