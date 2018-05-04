using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Actors;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_QUEST_SAVE : Packet<CharacterPacketOpcode>
    {
        ushort offsetQuest;
        public CM_QUEST_SAVE()
        {
            this.ID = CharacterPacketOpcode.CM_QUEST_SAVE;
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

        public List<Quests.Quest> Quests
        {
            get
            {
                short count = GetShort(14);
                List<Common.Quests.Quest> list = new List<Quests.Quest>();
                for (int i = 0; i < count; i++)
                {
                    Common.Quests.Quest q = new Quests.Quest();
                    q.QuestID = GetUShort();
                    q.Step = GetByte();
                    q.StepStatus = GetByte();
                    q.NextStep = GetByte();
                    q.Flag1 = GetShort();
                    q.Flag2 = GetShort();
                    q.Flag3 = GetShort();
                    for (int j = 0; j < Common.Quests.Quest.Counts; j++)
                    {
                        q.Count[j] = GetInt();
                    }
                    list.Add(q);
                }
                offsetQuest = offset;
                return list;
            }
            set
            {
                PutShort((short)value.Count, 14);
                foreach (Common.Quests.Quest q in value)
                {
                    PutUShort(q.QuestID);
                    PutByte(q.Step);
                    PutByte(q.StepStatus);
                    PutByte(q.NextStep);
                    PutShort(q.Flag1);
                    PutShort(q.Flag2);
                    PutShort(q.Flag3);
                    for (int j = 0; j < Common.Quests.Quest.Counts; j++)
                    {
                        PutInt(q.Count[j]);
                    }
                }
                offsetQuest = offset;
            }
        }

        public List<ushort> QuestCompleted
        {
            get
            {
                short count = GetShort(offsetQuest);
                List<ushort> list = new List<ushort>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(GetUShort());
                }
                return list;
            }
            set
            {
                PutShort((short)value.Count);
                foreach (ushort i in value)
                    PutUShort(i);
            }
        }
    }
}
