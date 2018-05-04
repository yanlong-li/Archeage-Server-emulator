using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets.GameServer
{
    public enum PacketParameterCBT2
    {
        [ParameterData(1)]
        Unk5 = 0x05, //1 byte
        [ParameterData(8)]
        Unk10 = 0x10, //8 bytes
        [ParameterData(2)]
        Unk12 = 0x12, //2 bytes
        [ParameterData(2)]
        Unk13 = 0x13, //2 bytes
        [ParameterData(2)]
        Unk14 = 0x14, //2 bytes
        [ParameterData(2)]
        Unk15 = 0x15, //2 bytes
        [ParameterData(1)]
        Unk16 = 0x16, //1 bytes
        [ParameterData(4)]
        Unk17 = 0x17, //4 bytes
        [ParameterData(4)]
        Unk18 = 0x18, //4 bytes
        [ParameterData(2)]
        Unk19 = 0x19, //2 bytes
        [ParameterData(4)]
        Unk1A = 0x1A, //4 bytes
        [ParameterData(2)]
        Unk1E = 0x1E, //2 bytes
        [ParameterData(1)]
        Unk1F = 0x1F, //1 bytes
        [ParameterData(8)]
        Unk20 = 0x20, //8 bytes
        [ParameterData(1)]
        Unk21 = 0x21, //1 bytes
        [ParameterData(1)]
        Unk22 = 0x22, //1 bytes
        [ParameterData(1)]
        Unk23 = 0x23, //1 bytes
        [ParameterData(8)]
        Unk24 = 0x24, //8 bytes
        [ParameterData(2)]
        Unk27 = 0x27, //2 bytes
        [ParameterData(8)]
        Unk29 = 0x29, //8 bytes
        [ParameterData(8)]
        Unk2A = 0x2A, //8 bytes
        [ParameterData(8)]
        Unk2B = 0x2B, //8 bytes
        [ParameterData(1)]
        Unk37 = 0x37, //1 byte
        [ParameterData(1)]
        Unk38 = 0x38, //1 byte
        [ParameterData(1)]
        Unk3B = 0x3B, //1 byte
        [ParameterData(1)]
        Unk3C = 0x3C, //1 byte
        [ParameterData(1)]
        Unk3D = 0x3D, //1 byte
        [ParameterData(1)]
        Unk3E = 0x3E, //1 byte
        [ParameterData(4)]
        Unk40 = 0x40, //4 bytes
        [ParameterData(2)]
        Unk41 = 0x41, //2 bytes
        [ParameterData(2)]
        Unk42 = 0x42, //2 bytes
        [ParameterData(2)]
        Unk43 = 0x43, //2 bytes
        [ParameterData(2)]
        Unk44 = 0x44, //2 bytes
        [ParameterData(4)]
        Unk45 = 0x45, //4 bytes
        [ParameterData(4)]
        Unk46 = 0x46, //4 bytes
        [ParameterData(2)]
        Unk47 = 0x47, //2 bytes
        [ParameterData(2)]
        Unk48 = 0x48, //2 bytes
        [ParameterData(2)]
        Unk49 = 0x49, //2 bytes
        [ParameterData(2)]
        Unk4A = 0x4A, //2 bytes
        [ParameterData(2)]
        Unk4B = 0x4B, //2 bytes
        [ParameterData(2)]
        Unk4C = 0x4C, //2 bytes
        [ParameterData(2)]
        Unk4D = 0x4D, //2 bytes
        [ParameterData(2)]
        Unk4E = 0x4E, //2 bytes
        [ParameterData(2)]
        Unk4F = 0x4F, //2 bytes
        [ParameterData(2)]
        Unk50 = 0x50, //2 bytes
        [ParameterData(2)]
        Unk51 = 0x51, //2 bytes
        [ParameterData(2)]
        Unk52 = 0x52, //2 bytes
        [ParameterData(2)]
        Unk53 = 0x53, //2 bytes
        [ParameterData(2)]
        Unk54 = 0x54, //2 bytes
        [ParameterData(2)]
        Unk55 = 0x55, //2 bytes
        [ParameterData(2)]
        Unk56 = 0x56, //2 bytes
        [ParameterData(2)]
        Unk57 = 0x57, //2 bytes
        [ParameterData(2)]
        Unk58 = 0x58, //2 bytes
        [ParameterData(2)]
        Unk59 = 0x59, //2 bytes
        [ParameterData(2)]
        Unk5A = 0x5A, //2 bytes
        [ParameterData(2)]
        Unk5B = 0x5B, //2 bytes
        [ParameterData(2)]
        Unk5C = 0x5C, //2 bytes
        [ParameterData(2)]
        Unk5D = 0x5D, //2 bytes
        [ParameterData(2)]
        Unk5E = 0x5E, //2 bytes
        [ParameterData(2)]
        Unk5F = 0x5F, //2 bytes
        [ParameterData(2)]
        Unk60 = 0x60, //2 bytes
        [ParameterData(2)]
        Unk61 = 0x61, //2 bytes
        [ParameterData(2)]
        Unk62 = 0x62, //2 bytes
        [ParameterData(2)]
        X = 0x63, //2 bytes
        [ParameterData(2)]
        Y = 0x64, //2 bytes
        [ParameterData(2)]
        Z = 0x65, //2 bytes
        [ParameterData(2)]
        Dir = 0x66, //2 bytes
        [ParameterData(1)]
        Level = 0x67, //1 byte
        [ParameterData(4)]
        EXPBC = 0x68, //4 bytes
        [ParameterData(4)]
        HP = 0x69, //4 bytes
        [ParameterData(2)]
        MP = 0x6A, //2 bytes
        [ParameterData(4)]
        Gold = 0x6B, //4 bytes
        [ParameterData(2)]
        Unk6C = 0x6C, //2 bytes
        [ParameterData(1)]
        PlayerFaction = 0x6D, //1 bytes
        [ParameterData(2)]
        Unk6E = 0x6E, //2 bytes
        [ParameterData(2)]
        BladeSpirit = 0x6F, //2 bytes
        [ParameterData(1)]
        Stance = 0x70, //1 byte
        [ParameterData(8)]
        InteractWith = 0x71, //8 byte
        [ParameterData(1)]
        InteractionType = 0x72, //2 byte
        [ParameterData(1)]
        InteractionRelation = 0x73, //2 byte
        [ParameterData(1)]
        Unk74 = 0x74, //1 byte
        [ParameterData(8)]
        FaceTo = 0x75, //8 bytes
        [ParameterData(8)]
        Unk76 = 0x76, //8 bytes
        [ParameterData(1)]
        Unk78 = 0x78, //1 byte
        [ParameterData(8)]
        Unk7A = 0x7A, //8 bytes
        [ParameterData(8)]
        Unk7B = 0x7B, //8 bytes
        [ParameterData(8)]
        Hold = 0x7C, //1 byte
        [ParameterData(1)]
        Unk80 = 0x80, //1 byte
        [ParameterData(1)]
        InventorySlots = 0x82, //1 byte
        [ParameterData(1)]
        Unk83 = 0x83, //1 byte
        [ParameterData(2)]
        SkillPoints = 0x84, //2 bytes
        [ParameterData(2)]
        Unk85 = 0x85, //2 bytes
        [ParameterData(1)]
        Craft3 = 0x86, //1 byte
        [ParameterData(1)]
        Craft4 = 0x87, //1 byte
        [ParameterData(1)]
        Craft1 = 0x88, //1 byte
        [ParameterData(1)]
        Craft2 = 0x89, //1 byte
        [ParameterData(2)]
        Craft3Rep = 0x8A, //2 bytes
        [ParameterData(2)]
        Craft4Rep = 0x8B, //2 bytes
        [ParameterData(2)]
        Craft1Rep = 0x8C, //2 bytes
        [ParameterData(2)]
        Craft2Rep = 0x8D, //2 bytes
        [ParameterData(1)]
        Unk90 = 0x90, //1 byte
        [ParameterData(6)]
        Unk92 = 0x92, //6 byte
        [ParameterData(1)]
        Unk93 = 0x93, //1 bytes
        [ParameterData(1)]
        Unk94 = 0x94, //1 bytes
        [ParameterData(1)]
        Unk95 = 0x95, //1 byte
        [ParameterData(1)]
        Dead = 0x96, //1 byte
        [ParameterData(1)]
        CombatStatus = 0x97, //1 byte
        [ParameterData(1)]
        Unk98 = 0x98, //1 byte
        [ParameterData(1)]
        Unk9A = 0x9A, //1 byte
        [ParameterData(1)]
        Unk9B = 0x9B, //1 byte
        [ParameterData(1)]
        Unk9C = 0x9C, //1 byte
        [ParameterData(1)]
        PickingUp = 0x9D, //1 byte
        [ParameterData(4)]
        MaxHP = 0x9F, //4 bytes
        [ParameterData(2)]
        UnkA0 = 0xA0, //2 bytes
        [ParameterData(2)]
        Stance2 = 0xA1, //2 bytes
        [ParameterData(2)]
        Speed = 0xA2, //2 bytes
        [ParameterData(2)]
        UnkA3 = 0xA3, //2 bytes
        [ParameterData(4)]
        UnkA4 = 0xA4, //4 bytes
        [ParameterData(4)]
        UnkA5 = 0xA5, //4 bytes
        [ParameterData(2)]
        UnkA6 = 0xA6, //2 byte
        [ParameterData(2)]
        UnkA7 = 0xA7, //2 byte
        [ParameterData(2)]
        HitPercent = 0xA8, //2 byte
        [ParameterData(2)]
        HitBase = 0xA9, //2 byte
        [ParameterData(2)]
        PierceBase = 0xAA, //2 byte
        [ParameterData(2)]
        Penetration = 0xAB, //2 byte
        [ParameterData(2)]
        CriticalPercent = 0xAC, //2 byte
        [ParameterData(2)]
        CriticalBase = 0xAD, //2 byte
        [ParameterData(2)]
        CriticalDefPercent = 0xAE, //2 bytes
        [ParameterData(2)]
        CriticalResist = 0xAF, //2 byte
        [ParameterData(2)]
        UnkB0 = 0xB0, //2 byte
        [ParameterData(2)]
        AvoidPercent = 0xB1, //2 byte
        [ParameterData(2)]
        AvoidBase = 0xB2, //2 byte
        [ParameterData(2)]
        ParryPercent = 0xB3, //2 byte
        [ParameterData(2)]
        ParryBase = 0xB4, //2 byte
        [ParameterData(2)]
        UnkB5 = 0xB5, //2 byte
        [ParameterData(2)]
        UnkB6 = 0xB6, //2 byte
        [ParameterData(2)]
        UnkB7 = 0xB7,
        [ParameterData(2)]
        UnkB8 = 0xB8,
        [ParameterData(2)]
        EquiptMinAtk2 = 0xB9,
        [ParameterData(2)]
        EquiptMaxAtk2 = 0xBA,
        [ParameterData(2)]
        EquiptMinAtk = 0xBB,
        [ParameterData(2)]
        EquiptMaxAtk = 0xBC,
        [ParameterData(2)]
        Def = 0xBD,
        [ParameterData(2)]
        Def2 = 0xBE,
        [ParameterData(2)]
        UnkBF = 0xBF,
        [ParameterData(2)]
        UnkC0 = 0xC0,
        [ParameterData(2)]
        UnkC1 = 0xC1,
        [ParameterData(2)]
        UnkC2 = 0xC2,
        [ParameterData(2)]
        UnkC3 = 0xC3,
        [ParameterData(2)]
        UnkC4 = 0xC4,
        [ParameterData(2)]
        UnkC5 = 0xC5,
        [ParameterData(2)]
        UnkC6 = 0xC6,
        [ParameterData(2)]
        UnkC7 = 0xC7,
        [ParameterData(2)]
        UnkC8 = 0xC8,
        [ParameterData(2)]
        Expertise = 0xC9,
        [ParameterData(2)]
        UnkCA = 0xCA,
        [ParameterData(2)]
        Spirit = 0xCB,
        [ParameterData(2)]
        UnkCC = 0xCC,
        [ParameterData(2)]
        UnkCD = 0xCD,
        [ParameterData(2)]
        UnkCE = 0xCE,
        [ParameterData(2)]
        UnkCF = 0xCF,
        [ParameterData(2)]
        UnkD0 = 0xD0,
        [ParameterData(2)]
        UnkD1 = 0xD1,
        [ParameterData(1)]
        UnkD3 = 0xD3, //1 byte
        [ParameterData(1)]
        UnkD4 = 0xD4, //1 byte
        [ParameterData(1)]
        UnkD5 = 0xD5, //1 byte
        [ParameterData(1)]
        UnkD7 = 0xD7, //1 byte
        [ParameterData(1)]
        UnkD9 = 0xD9, //1 byte
        [ParameterData(1)]
        UnkDD = 0xDD, //1 byte
        [ParameterData(1)]
        UnkDF = 0xDF, //1 byte
        [ParameterData(1)]
        UnkE0 = 0xE0, //1 byte
        [ParameterData(1)]
        BlockingStance = 0xE1, //1 byte
        [ParameterData(1)]
        UnkE2 = 0xE2, //1 byte
        [ParameterData(1)]
        UnkE4 = 0xE4, //1 byte
        [ParameterData(1)]
        UnkE7 = 0xE7, //1 byte
        [ParameterData(1)]
        UnkE9 = 0xE9, //1 byte
        [ParameterData(1)]
        UnkEA = 0xEA, //1 byte
        [ParameterData(1)]
        UnkEB = 0xEB, //1 byte
        [ParameterData(1)]
        UnkF0 = 0xF0, //1 byte
        [ParameterData(1)]
        UnkF1 = 0xF1, //1 byte
        [ParameterData(1)]
        UnkF4 = 0xF4, //1 byte
        [ParameterData(1)]
        UnkF7 = 0xF7, //1 byte
        [ParameterData(1)]
        UnkF8 = 0xF8, //1 byte
        [ParameterData(1)]
        UnkF9 = 0xF9, //1 byte
        [ParameterData(1)]
        UnkFA = 0xFA, //1 byte
        [ParameterData(1)]
        NoMove = 0x101, //1 byte
        [ParameterData(1)]
        NoRotate = 0x102, //1 byte
        [ParameterData(1)]
        Unk103 = 0x103, //1 byte
        [ParameterData(1)]
        Unk105 = 0x105, //1 byte
        [ParameterData(1)]
        Unk106 = 0x106, //1 byte
        [ParameterData(1)]
        Unk107 = 0x107, //1 byte
        [ParameterData(1)]
        Unk108 = 0x108, //1 byte
        [ParameterData(1)]
        Unk109 = 0x109, //1 byte
        [ParameterData(1)]
        Unk10C = 0x10C, //1 byte
        [ParameterData(1)]
        Unk10D = 0x10D, //1 byte
        [ParameterData(1)]
        Unk10E = 0x10E, //1 byte
        [ParameterData(1)]
        Unk10F = 0x10F, //1 byte
        [ParameterData(4)]
        Weapon = 0x112, //4 bytes
        [ParameterData(4)]
        Costume = 0x114, //4 bytes
        [ParameterData(4)]
        Eyewear = 0x116, //4 bytes
        [ParameterData(4)]
        Hat = 0x117, //4 bytes
        [ParameterData(4)]
        CostumeAccessory = 0x118, //4 bytes
        
    }
}
