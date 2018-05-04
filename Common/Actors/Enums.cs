using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Actors
{
    public enum Race
    {
        Gun = 1,
        Gon,
        Rin,
        Jin
    }

    public enum Gender
    {
        Male = 1,
        Female,
    }

    public enum Job
    {
        None,
        BladeMaster,
        KungfuMaster,
        ForceMaster,
        Shooter,
        Destroyer,
        Summoner,
        Assassin,
    }

    public enum ManaType
    {
        BladeSpirit = 2,
        SummonSword = 3,
        CombatSpirit = 4,
        Force = 8,
        Destroyer = 13,
        Summoner = 16,
        Chakra = 17,
    }

    public enum Stances
    {
        None = 0,
        Ice = 9,
        Fire = 10,
        Assassin_Normal = 17,
        Assassin_Stealth = 18,
    }

    public enum Factions
    {
        Player1,
        FactionMain1,
        FactionMain2,
        PlayerSelfDefenceForce,
        PlayerChunggakdan,
        SelfDefenceForce,
        Chunggakdan,
        Training,
        QuestNPC,
        FriendlyNPC,
        Monster,
        MonsterAggressive,
        BlackDragonGang,
        PK,
    }

    public enum Relations
    {
        Friendly,
        Neutral,
        Enemy,
    }

    public enum Crafts
    {
        None,
        WeaponSmith,
        ForceSmith,
        BoPae,
        Jewel,
        Potion,
        Cooking,
        Pottery,
        Herbalist,
        Harvester,
        Fisher,
        Horticulture,
        Miner,
        StoneCutter,
        Logger,
    }

    public enum Stats
    {
        MinAtk,
        MaxAtk,
        Defense,
        Resist,
        HitP,
        HitB,
        PrcB,
        CritP,
        CritB,
        DefCritP,
        DefCritB,
        AvoidP,
        AvoidB,
        ParryP,
        ParryB,
        AttackStiffDurationBasePercent,
        AttackStiffDurationValue,
        DefendStiffDurationBasePercent,
        DefendStiffDurationValue,
        CastTimeP,
        CastTimeB,
        DefPhysDmgP,
        DefForceDmgP,
        AttackDamageModifyPercent,
        AttackDamageModifyDiff,
        DefendDamageModifyPercent,
        DefendDamageModifyDiff,
        MaxHp,
        MaxFp,
        HPRegen,
        FPRegen,
        FpRegenCombat,
        None,
    }
}
