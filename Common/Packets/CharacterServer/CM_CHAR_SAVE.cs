using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Actors;

namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_CHAR_SAVE : Packet<CharacterPacketOpcode>
    {
        public CM_CHAR_SAVE()
        {
            this.ID = CharacterPacketOpcode.CM_CHAR_SAVE;
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

        public ActorPC Character
        {
            get
            {
                ActorPC pc = new ActorPC();
                pc.AccountID = GetUInt(10);
                pc.CharID = GetUInt();
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
                return pc;
            }
            set
            {
                PutUInt(value.AccountID, 10);
                PutUInt(value.CharID);
                PutByte(value.SlotID);
                PutByte(value.WorldID);
                PutString(value.Name);
                PutByte(value.Level);
                PutUInt(value.Exp);
                PutByte((byte)value.Race);
                PutByte((byte)value.Gender);
                PutByte((byte)value.Job);
                PutByte((byte)value.Appearence1.Length);
                PutBytes(value.Appearence1);
                PutByte((byte)value.Appearence2.Length);
                PutBytes(value.Appearence2);
                PutString(value.UISettings);
                PutULong(value.PartyID);
                PutByte(value.Offline ? (byte)1 : (byte)0);
                PutInt(value.HP);
                PutUShort((ushort)value.MP);
                PutInt(value.MaxHP);
                PutUShort(value.MaxMP);
                PutInt(value.Gold);
                PutUInt(value.MapID);
                PutShort((short)value.X);
                PutShort((short)value.Y);
                PutShort((short)value.Z);
                PutUShort(value.Dir);
                PutByte(value.InventorySize);
            }
        }
    }
}
