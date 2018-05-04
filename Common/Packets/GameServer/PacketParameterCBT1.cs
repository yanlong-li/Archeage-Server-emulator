using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets.GameServer
{
    public enum PacketParameterCBT1
    {
        [ParameterData(2)]
        X = 0x12, //2 bytes
        [ParameterData(2)]
        Y = 0x13, //2 bytes
        [ParameterData(2)]
        Z = 0x14, //2 bytes
        [ParameterData(2)]
        Dir = 0x15, //2 bytes
        [ParameterData(1)]
        Unk16 = 0x16, //1 bytes
        [ParameterData(4)]
        Unk17 = 0x17, //4 bytes
        [ParameterData(4)]
        HP = 0x18, //4 bytes
        [ParameterData(2)]
        MP = 0x19, //2 bytes
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
        FaceTo = 0x24, //8 bytes
        [ParameterData(2)]
        Unk27 = 0x27, //2 bytes
        [ParameterData(8)]
        Unk29 = 0x29, //8 bytes
        [ParameterData(8)]
        Unk2A = 0x2A, //8 bytes
        [ParameterData(8)]
        Hold = 0x2B, //8 bytes
        [ParameterData(1)]
        Unk37 = 0x37, //1 byte
        [ParameterData(1)]
        CombatStatus = 0x38, //1 byte
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
        Unk63 = 0x63, //2 bytes
        [ParameterData(2)]
        Unk64 = 0x64, //2 bytes
        [ParameterData(2)]
        Unk65 = 0x65, //2 bytes
        [ParameterData(2)]
        Unk66 = 0x66, //2 bytes
        [ParameterData(2)]
        Unk67 = 0x67, //2 bytes
        [ParameterData(2)]
        Unk68 = 0x68, //2 bytes
        [ParameterData(2)]
        Unk69 = 0x69, //2 bytes
        [ParameterData(2)]
        Unk6A = 0x6A, //2 bytes
        [ParameterData(2)]
        Unk6B = 0x6B, //2 bytes
        [ParameterData(2)]
        Unk6C = 0x6C, //2 bytes
        [ParameterData(2)]
        Unk6D = 0x6D, //2 bytes
        [ParameterData(2)]
        Unk6E = 0x6E, //2 bytes
        [ParameterData(2)]
        Unk6F = 0x6F, //2 bytes
        [ParameterData(2)]
        Unk70 = 0x70, //2 byte
        [ParameterData(2)]
        Unk71 = 0x71, //2 byte
        [ParameterData(2)]
        Unk72 = 0x72, //2 byte
        [ParameterData(1)]
        Unk74 = 0x74, //1 byte
        [ParameterData(1)]
        Unk75 = 0x75, //1 byte
        [ParameterData(1)]
        Unk76 = 0x76, //1 byte
        [ParameterData(1)]
        Unk78 = 0x78, //1 byte
        [ParameterData(1)]
        Unk7A = 0x7A, //1 byte
        [ParameterData(1)]
        Unk80 = 0x80, //1 byte
        [ParameterData(1)]
        Unk82 = 0x82, //1 byte
        [ParameterData(1)]
        Unk83 = 0x83, //1 byte
        [ParameterData(1)]
        Unk85 = 0x85, //1 byte
        [ParameterData(1)]
        Unk88 = 0x88, //1 byte
        [ParameterData(1)]
        Unk89 = 0x89, //1 byte
        [ParameterData(1)]
        Unk90 = 0x90, //1 byte
        [ParameterData(1)]
        Unk92 = 0x92, //1 byte
        [ParameterData(1)]
        Unk95 = 0x95, //1 byte
        [ParameterData(1)]
        Unk97 = 0x97, //1 byte
        [ParameterData(1)]
        Unk98 = 0x98, //1 byte
        [ParameterData(1)]
        Unk9B = 0x9B, //1 byte
        [ParameterData(1)]
        Unk9C = 0x9C, //1 byte
        [ParameterData(1)]
        Unk9D = 0x9D, //1 byte
        [ParameterData(1)]
        UnkA0 = 0xA0, //1 bytes
        [ParameterData(1)]
        UnkA1 = 0xA1, //1 bytes
        [ParameterData(1)]
        UnkA2 = 0xA2, //1 bytes
        [ParameterData(1)]
        UnkA6 = 0xA6, //1 bytes
        [ParameterData(1)]
        UnkA7 = 0xA7, //1 bytes
        [ParameterData(1)]
        UnkA8 = 0xA8, //1 bytes
        [ParameterData(1)]
        UnkA9 = 0xA9, //1 bytes
        [ParameterData(4)]
        Weapon = 0xAC, //4 bytes
        [ParameterData(4)]
        Costume = 0xAE //4 bytes (4th variable of 0xA10)
    }
}
