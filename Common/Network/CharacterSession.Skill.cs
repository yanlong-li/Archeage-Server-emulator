using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Packets;
using SagaBNS.Common.Actors;
using SagaBNS.Common.Packets.CharacterServer;

namespace SagaBNS.Common.Network
{
    public abstract partial class CharacterSession<T> : DefaultClient<CharacterPacketOpcode>
    {
        public void GetSkills(uint charID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_SKILL_GET p = new CM_SKILL_GET();
            p.SessionID = session;
            p.CharID = charID;

            this.Network.SendPacket(p);
        }

        public void SaveSkill(ActorPC pc)
        {
            var skills = from skill in pc.Skills.Values
                         where !skill.Dummy
                         select skill;
            CM_SKILL_SAVE p = new CM_SKILL_SAVE();
            p.CharID = pc.CharID;
            p.Skills = skills.ToList();
            this.Network.SendPacket(p);
        }

        public void OnSkillInfo(SM_SKILL_INFO p)
        {
            long session = p.SessionID;
            if (packetSessions.ContainsKey(session))
            {
                T client;
                if (packetSessions.TryRemove(session, out client))
                {
                    OnSkillInfo(client, p.Skills);                    
                }
            }
        }

        protected abstract void OnSkillInfo(T client, List<Skills.Skill> skills);
    }
}
