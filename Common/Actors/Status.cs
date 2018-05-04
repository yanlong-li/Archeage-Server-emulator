using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network.Utils;
namespace SagaBNS.Common.Actors
{
    public enum StanceU1 : long
    {
        Unknown4 = 0x4,
        Poisen = 0x20,
        Dash = 0x40,
        Unknown200 = 0x200,
        Down1 = 0x1000,
        Unknown2000 = 0x2000,
        Unknown40000 = 0x40000,
        Unknown400000 = 0x400000,
        Stun = 0x800000,
        NoMove = 0x2000000,
        Unknown10000000 = 0x10000000,
        TakenDown = 0x100000000,
        Unknown200000000000 = 0x200000000000,
        Unknown400000000000 = 0x400000000000,
    }
    public enum StanceU2 : long
    {
        TakenDown1 = 0x2000000,
        TakenDown2 = 0x400000000000,
    }
    public class Status
    {
        public int MaxHPExt = 0;
        public int MaxHPExt2 = 0;
        public int AtkMin = 0;
        public int AtkMinBase = 6;
        public int AtkMinExt = 0;
        public int AtkMinExt2 = 0;
        public int AtkMax = 0;
        public int AtkMaxBase = 10;
        public int AtkMaxExt = 0;
        public int AtkMaxExt2 = 0;
        public int Penetration = 0;
        public int PenetrationBase = 0;
        public int PenetrationExt = 0;
        public int Pierce = 0;
        public int PierceBase = 0;
        public int PierceExt = 0;
        public int PierceExt2 = 0;
        public int Hit = 0;
        public int HitBase = 8;
        public int HitExt = 0;
        public int HitExt2 = 0;
        public int Critical = 0;
        public int CriticalBase = 2;
        public int CriticalExt = 0;
        public int CriticalExt2 = 0;
        public int Practice = 0;
        public int PracticeBase = 0;
        public int PracticeExt = 0;
        public int Defence = 0;
        public int DefenceBase = 3;
        public int DefenceExt = 0;
        public int DefenceExt2 = 0;
        public int Parry = 0;
        public int ParryBase = 0;
        public int ParryExt = 0;
        public int ParryExt2 = 0;
        public int Avoid = 0;
        public int AvoidBase = 1;
        public int AvoidExt = 0;
        public int AvoidExt2 = 0;
        public int CriticalResist = 0;
        public int CriticalResistBase = 0;
        public int CriticalResistExt = 0;
        public int CriticalResistExt2 = 0;
        public int Tough = 0;
        public int ToughBase = 0;
        public int ToughExt = 0;
        public bool CastingSkill = false;
        public bool IsInCombat = false;
        public bool Blocking = false;
        public bool Counter = false;
        public bool Dead = false, Dying = false, Recovering = false, Down = false, Stun = false, TakeDown = false, TakenDown = false, Stealth = false, ShouldLoadMap = false, ShouldRespawn = false, Frosen = false, Catch = false, Invincible = false, Dummy = false;
        public ulong InteractWith = 0;
        public Stances Stance = Stances.None;
        public BitMask64<StanceU1> StanceFlag1 = new BitMask64<StanceU1>();
        public BitMask64<StanceU2> StanceFlag2 = new BitMask64<StanceU2>();
        public ulong CorpseActorID;
        public DateTime SkillCooldownEnd;
        public uint LastSkillID = 0;
        public int DisappearEffect = 0;

        public Status()
        {
            SkillCooldownEnd = DateTime.Now;
        }

        public void ClearStatus()
        {
            AtkMax = 0;
            AtkMin = 0;
            Penetration = 0;
            Pierce = 0;
            Hit = 0;
            Critical = 0;
            Practice = 0;
            Defence = 0;
            Parry = 0;
            Avoid = 0;
            CriticalResist = 0;
            Tough = 0;
        }

        public void ClearBaGuaBonus()
        {
            MaxHPExt2 = 0;
            AtkMinExt2 = 0;
            AtkMaxExt2 = 0;
            PierceExt2 = 0;
            HitExt2 = 0;
            CriticalExt2 = 0;
            DefenceExt2 = 0;
            ParryExt2 = 0;
            AvoidExt2 = 0;
            CriticalResistExt2 = 0;
        }
    }
}
