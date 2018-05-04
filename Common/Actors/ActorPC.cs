using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using SagaBNS.Common.Skills;

using SmartEngine.Core;

namespace SagaBNS.Common.Actors
{
    public class ActorPC : ActorExt
    {
        Job job;
        ConcurrentDictionary<ushort, Quests.Quest> quests = new ConcurrentDictionary<ushort, Quests.Quest>();
        List<ushort> locations = new List<ushort>();
        List<ushort> questsCompleted = new List<ushort>();
        ConcurrentDictionary<uint, Skill> savedSkills = new ConcurrentDictionary<uint, Skill>();
        public Crafts Craft1 { get; set; }
        public Crafts Craft2 { get; set; }
        public Crafts Craft3 { get; set; }
        public Crafts Craft4 { get; set; }
        public ushort Craft1Rep { get; set; }
        public ushort Craft2Rep { get; set; }
        public ushort Craft3Rep { get; set; }
        public ushort Craft4Rep { get; set; }
        public byte SkillPoints { get; set; }
        public string UISettings { get; set; }
        public uint CharID { get; set; }
        public uint AccountID { get; set; }
        public byte WorldID { get; set; }
        public byte SlotID { get; set; }
        public Race Race { get; set; }
        public Gender Gender { get; set; }
        public uint MapChangeCutScene { get; set; }
        public int MapChangeCutSceneU1 { get; set; }
        public int MapChangeCutSceneU2 { get; set; }
        public List<uint> BaGuaEffect = new List<uint>();

        public ulong PartyID { get; set; }
        public Party.Party Party { get; set; }
        public bool Offline { get; set; }
        public bool SendRemove { get; set; }
        public bool StillProcess { get; set; }

        public Job Job
        {
            get { return job; }
            set
            {
                job = value; switch (job)
                {
                    case Actors.Job.BladeMaster:
                        ManaType = Actors.ManaType.BladeSpirit;
                        break;
                    case Actors.Job.Destroyer:
                        ManaType = Actors.ManaType.Destroyer;
                        break;
                    case Actors.Job.KungfuMaster:
                        ManaType = Actors.ManaType.CombatSpirit;
                        break;
                    case Actors.Job.ForceMaster:
                        ManaType = Actors.ManaType.Force;
                        break;
                    case Actors.Job.Summoner:
                        ManaType = Actors.ManaType.Summoner;
                        break;
                    case Actors.Job.Assassin:
                        ManaType = Actors.ManaType.Chakra;
                        break;
                }
            }
        }
        public byte[] Appearence1 { get; set; }
        public byte[] Appearence2 { get; set; }

        public uint Exp { get; set; }

        public int Gold;

        public byte InventorySize
        {
            get { return Inventory.ListSize; }
            set { Inventory.ListSize = value; }
        }

        public Inventory.Inventory Inventory { get; set; }

        public ConcurrentDictionary<uint, Skill> Skills { get { return savedSkills; } set { savedSkills = value; } }

        public ConcurrentDictionary<ushort, Quests.Quest> Quests { get { return quests; } set { quests = value; } }

        public List<ushort> Locations { get { return locations; } set { locations = value; } }

        public List<ushort> QuestsCompleted { get { return questsCompleted; } set { questsCompleted = value; } }

        public ActorItem HoldingItem { get; set; }

        public ActorPC()
        {
            type = SmartEngine.Network.Map.ActorType.PC;
            this.SightRange = 800;
            Inventory = new Inventory.Inventory(this);
            this.Faction = Factions.Player1;
            this.Speed = 500;
            this.StillProcess = false;
        }

        public void AppearenceToSteam(System.IO.Stream stream)
        {
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(stream);
            long begin = stream.Position;
            stream.Position += 2;
            bw.Write((short)this.Appearence1.Length);
            bw.Write(this.Appearence1);
            stream.Position += 2;
            bw.Write((byte)this.Race);
            bw.Write((short)1);
            bw.Write((byte)this.Gender);
            bw.Write((short)2);
            bw.Write((byte)this.Job);
            bw.Write((short)3);
            bw.Write((short)this.Appearence2.Length);
            bw.Write(this.Appearence2);
            bw.Write((short)4);
            bw.Write(Encoding.Unicode.GetBytes(this.Name + "\0"));
            bw.Write((short)5);
            bw.Write((short)0);
            bw.Write((short)6);
            bw.Write(2300);
            bw.Write((short)7);
            bw.Write((short)9890);
            bw.Write((short)8);
            bw.Write((short)0x1440);
            bw.Write((short)9);
            bw.Write((short)307);
            bw.Write((short)10);
            bw.Write(this.Level);
            bw.Write((short)11);
            bw.Write(25450);
            bw.Write((short)12);
            bw.Write(256);
            bw.Write((short)13);
            bw.Write((short)375);
            bw.Write((short)14);
            bw.Write(1754);
            bw.Write((short)15);
            //if (this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Weapon] != null)
            //    bw.Write(this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Weapon].ItemID);//weapon
            //else
                bw.Write(0);//weapon
            bw.Write((short)16);
            bw.Write(0);
            bw.Write((short)17);
            //if(this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Costume]!=null)
             //   bw.Write(this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Costume].ItemID);//cloth
            //else
                bw.Write(0);//cloth
            bw.Write((short)18);
            bw.Write(0);
            bw.Write((short)19);
            //if (this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Eye] != null)
           //     bw.Write(this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Eye].ItemID);//eyeware
           // else
                bw.Write(0);//eyeware
            bw.Write((short)20);
            //if (this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Hat] != null)
            //    bw.Write(this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.Hat].ItemID);//hat
            //else
                bw.Write(0);//hat
            bw.Write((short)21);
            //if (this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.CostumeAccessory] != null)
            //    bw.Write(this.Inventory.Equipments[Common.Inventory.InventoryEquipSlot.CostumeAccessory].ItemID);//costume accessory
            //else
                bw.Write(0);//costume accessory
            long size = stream.Position - begin;
            stream.Position = begin;
            bw.Write((short)size);
        }
    }
}
