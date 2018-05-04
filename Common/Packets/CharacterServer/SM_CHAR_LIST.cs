using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Actors;
using SagaBNS.Common.Network;
namespace SagaBNS.Common.Packets.CharacterServer
{
    public class SM_CHAR_LIST : Packet<CharacterPacketOpcode>
    {
        internal class SM_CHAR_LIST_INTERNAL<T> : SM_CHAR_LIST
        {
            public override Packet<CharacterPacketOpcode> New()
            {
                return new SM_CHAR_LIST_INTERNAL<T>();
            }

            public override void OnProcess(Session<CharacterPacketOpcode> client)
            {
                ((CharacterSession<T>)client).OnCharList(this);
            }
        }
        public SM_CHAR_LIST()
        {
            this.ID = CharacterPacketOpcode.SM_CHAR_LIST;
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

        public List<ActorPC> Characters
        {
            get
            {
                byte count = GetByte(10);
                List<ActorPC> res = new List<ActorPC>();
                for (int i = 0; i < count; i++)
                {
                    ActorPC pc = new ActorPC();
                    pc.CharID = GetUInt();
                    pc.AccountID = GetUInt();
                    pc.SlotID = GetByte();
                    pc.WorldID = GetByte();
                    pc.Name = GetString();
                    pc.Level = GetByte();
                    pc.Exp = GetUInt();
                    pc.Race = (Race)GetByte();
                    pc.Gender = (Gender)GetByte();
                    pc.Job = (Job)GetByte();
                    pc.Appearence1 = GetBytes(GetByte());
                    pc.Appearence2 = GetBytes(GetByte());
                    pc.UISettings = GetString();
                    pc.PartyID = GetULong();
                    pc.Offline = GetByte() == 1;
                    pc.Exp = GetUInt();
                    pc.HP = GetInt();
                    pc.MP = GetUShort();
                    pc.MaxHP = GetInt();
                    pc.MaxMP = GetUShort();
                    pc.Gold = GetInt();
                    pc.MapID = GetUInt();
                    pc.X = GetShort();
                    pc.Y = GetShort();
                    pc.Z = GetShort();
                    pc.Dir = GetUShort();
                    pc.InventorySize = GetByte();
                    res.Add(pc);
                }
                return res;
            }
            set
            {
                PutByte((byte)value.Count, 10);
                foreach (ActorPC i in value)
                {
                    PutUInt(i.CharID);
                    PutUInt(i.AccountID);
                    PutByte(i.SlotID);
                    PutByte(i.WorldID);
                    PutString(i.Name);
                    PutByte(i.Level);
                    PutUInt(i.Exp);
                    PutByte((byte)i.Race);
                    PutByte((byte)i.Gender);
                    PutByte((byte)i.Job);
                    PutByte((byte)i.Appearence1.Length);
                    PutBytes(i.Appearence1);
                    PutByte((byte)i.Appearence2.Length);
                    PutBytes(i.Appearence2);
                    PutString(i.UISettings);
                    PutULong(i.PartyID);
                    PutByte(i.Offline ? (byte)1 : (byte)0);
                    PutUInt(i.Exp);
                    PutInt(i.HP);
                    PutUShort((ushort)i.MP);
                    PutInt(i.MaxHP);
                    PutUShort(i.MaxMP);
                    PutInt(i.Gold);
                    PutUInt(i.MapID);
                    PutShort((short)i.X);
                    PutShort((short)i.Y);
                    PutShort((short)i.Z);
                    PutUShort(i.Dir);
                    PutByte(i.InventorySize);
                }
            }
        }
    }
}
