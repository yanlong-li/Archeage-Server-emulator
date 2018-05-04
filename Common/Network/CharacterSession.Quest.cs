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
        public void GetQuests(uint charID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_QUEST_GET p = new CM_QUEST_GET();
            p.SessionID = session;
            p.CharID = charID;

            this.Network.SendPacket(p);
        }

        public void SaveQuest(ActorPC pc)
        {
            CM_QUEST_SAVE p = new CM_QUEST_SAVE();
            p.CharID = pc.CharID;
            p.Quests = pc.Quests.Values.ToList();
            p.QuestCompleted = pc.QuestsCompleted;

            this.Network.SendPacket(p);
        }

        public void OnQuestInfo(SM_QUEST_INFO p)
        {
            long session = p.SessionID;
            if (packetSessions.ContainsKey(session))
            {
                T client;
                if (packetSessions.TryRemove(session, out client))
                {
                    OnQuestInfo(client, p.Quests, p.QuestCompleted);
                }
            }
        }

        protected abstract void OnQuestInfo(T client, List<Quests.Quest> quests, List<ushort> completed);
    }
}
