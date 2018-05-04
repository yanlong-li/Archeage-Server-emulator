using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets.GameServer
{
    public enum PacketParameter
    {
        [ParameterData(2)]
        X = 0x62, //2 bytes
        [ParameterData(2)]
        Y = 0x63, //2 bytes
        [ParameterData(2)]
        Z = 0x64, //2 bytes
        [ParameterData(2)]
        Dir = 0x65, //2 bytes
        [ParameterData(1)]
        Level = 0x66, //1 byte
        [ParameterData(4)]
        EXPBC = 0x67, //4 bytes
        [ParameterData(4)]
        HP = 0x69, //4 bytes
        [ParameterData(4)]
        Gold = 0x6A, //4 bytes
        [ParameterData(1)]
        Unk6E = 0x6E, //1 byte
        [ParameterData(2)]
        Unk72 = 0x72, //2 bytes
        [ParameterData(1)]
        Unk73 = 0x73, //1 byte
        [ParameterData(8)]
        Unk74 = 0x74, //8 bytes
        [ParameterData(1)]
        Unk75 = 0x75, //1 byte
        [ParameterData(1)]
        Unk76 = 0x76, //1 byte
        [ParameterData(1)]
        Unk77 = 0x77, //1 byte
        [ParameterData(8)]
        Unk78 = 0x78, //8 bytes
        [ParameterData(1)]
        PickingStatus = 0x79, //1 byte
        [ParameterData(8)]
        PickingObject = 0x7A, //8 bytes
        [ParameterData(8)]
        Unk7C = 0x7C, //8 bytes
        [ParameterData(8)]
        Unk7D = 0x7D, //8 bytes
        [ParameterData(8)]
        Unk7E = 0x7E, //8 bytes
        [ParameterData(8)]
        Unk7F = 0x7F, //8 bytes
        [ParameterData(8)]
        Unk80 = 0x80, //8 bytes
        [ParameterData(1)]
        InventorySlots = 0x84, //1 byte
        [ParameterData(2)]
        Unk87 = 0x87, //2 bytes
        [ParameterData(1)]
        Craft3 = 0x88, //1 byte
        [ParameterData(1)]
        Craft4 = 0x89, //1 byte
        [ParameterData(1)]
        Craft1 = 0x8A, //1 byte
        [ParameterData(1)]
        Craft2 = 0x8B, //1 byte
        [ParameterData(2)]
        Craft3Rep = 0x8C, //2 bytes
        [ParameterData(2)]
        Craft4Rep = 0x8D, //2 bytes
        [ParameterData(2)]
        Craft1Rep = 0x8E, //2 bytes
        [ParameterData(2)]
        Craft2Rep = 0x8F, //2 bytes
        [ParameterData(8)]
        Craft3Order = 0x90, //8 bytes
        [ParameterData(8)]
        Craft4Order = 0x91, //8 bytes
        [ParameterData(8)]
        Craft1Order = 0x92, //8 bytes
        [ParameterData(8)]
        Craft2Order = 0x93, //8 bytes
        [ParameterData(2)]
        Unk96 = 0x96, //2 bytes
        [ParameterData(2)]
        Unk97 = 0x97, //2 bytes
        [ParameterData(2)]
        Unk98 = 0x98, //2 bytes
        [ParameterData(1)]
        Unk9A = 0x9A, //1 byte
        [ParameterData(1)]
        Unk9D = 0x9D, //1 byte
        [ParameterData(1)]
        UnkA0 = 0xA0, //1 byte
        [ParameterData(1)]
        UnkA1 = 0xA1, //1 byte
        [ParameterData(1)]
        UnkA2 = 0xA2, //1 byte
        [ParameterData(1)]
        UnkA3 = 0xA3, //1 byte
        [ParameterData(1)]
        UnkA4 = 0xA4, //1 byte
        [ParameterData(4)]
        UnkA6 = 0xA6, //4 bytes
        [ParameterData(2)]
        UnkA7 = 0xA7, //2 bytes
        [ParameterData(2)]
        UnkA8 = 0xA8, //2 bytes
        [ParameterData(2)]
        UnkA9 = 0xA9, //2 bytes
        [ParameterData(4)]
        UnkAA = 0xAA, //4 bytes
        [ParameterData(4)]
        UnkAB = 0xAB, //4 bytes
        [ParameterData(2)]
        UnkAC = 0xAC, //2 bytes
        [ParameterData(2)]
        UnkAD = 0xAD, //2 bytes
        [ParameterData(2)]
        UnkAE = 0xAE, //2 bytes
        [ParameterData(2)]
        UnkAF = 0xAF, //2 bytes
        [ParameterData(2)]
        UnkB0 = 0xB0, //2 bytes
        [ParameterData(2)]
        UnkB1 = 0xB1, //2 bytes
        [ParameterData(2)]
        UnkB2 = 0xB2, //2 bytes
        [ParameterData(2)]
        UnkB3 = 0xB3, //2 bytes
        [ParameterData(2)]
        UnkB4 = 0xB4, //2 bytes
        [ParameterData(2)]
        UnkB5 = 0xB5, //2 bytes
        [ParameterData(2)]
        UnkB6 = 0xB6, //2 bytes
        [ParameterData(2)]
        UnkB7 = 0xB7, //2 bytes
        [ParameterData(2)]
        UnkB8 = 0xB8, //2 bytes
        [ParameterData(2)]
        UnkB9 = 0xB9, //2 bytes
        [ParameterData(2)]
        UnkBA = 0xBA, //2 bytes
        [ParameterData(2)]
        UnkBB = 0xBB, //2 bytes
        [ParameterData(2)]
        UnkBC = 0xBC, //2 bytes
        [ParameterData(2)]
        UnkBD = 0xBD, //2 bytes
        [ParameterData(2)]
        UnkBE = 0xBE, //2 bytes
        [ParameterData(2)]
        UnkBF = 0xBF, //2 bytes
        [ParameterData(2)]
        UnkC0 = 0xC0, //2 bytes
        [ParameterData(2)]
        UnkC1 = 0xC1, //2 bytes
        [ParameterData(2)]
        UnkC2 = 0xC2, //2 bytes
        [ParameterData(2)]
        UnkC3 = 0xC3, //2 bytes
        [ParameterData(2)]
        UnkC4 = 0xC4, //2 bytes
        [ParameterData(2)]
        UnkC5 = 0xC5, //2 bytes
        [ParameterData(2)]
        UnkC6 = 0xC6, //2 bytes
        [ParameterData(2)]
        UnkC7 = 0xC7, //2 bytes
        [ParameterData(2)]
        UnkC8 = 0xC8, //2 bytes
        [ParameterData(2)]
        UnkC9 = 0xC9, //2 bytes
        [ParameterData(2)]
        UnkCA = 0xCA, //2 bytes
        [ParameterData(2)]
        UnkCB = 0xCB, //2 bytes
        [ParameterData(2)]
        UnkCC = 0xCC, //2 bytes
        [ParameterData(2)]
        UnkCD = 0xCD, //2 bytes
        [ParameterData(2)]
        UnkCE = 0xCE, //2 bytes
        [ParameterData(2)]
        UnkCF = 0xCF, //2 bytes
        [ParameterData(2)]
        UnkD0 = 0xD0, //2 bytes
        [ParameterData(2)]
        UnkD1 = 0xD1, //2 bytes
        [ParameterData(2)]
        UnkD2 = 0xD2, //2 bytes
        [ParameterData(2)]
        UnkD3 = 0xD3, //2 bytes
        [ParameterData(2)]
        UnkD4 = 0xD4, //2 bytes
        [ParameterData(2)]
        UnkD5 = 0xD5, //2 bytes
        [ParameterData(2)]
        UnkD6 = 0xD6, //2 bytes
        [ParameterData(2)]
        UnkD7 = 0xD7, //2 bytes
        [ParameterData(2)]
        UnkD8 = 0xD8, //2 bytes
        [ParameterData(1)]
        UnkD9 = 0xD9, //1 byte
        [ParameterData(1)]
        UnkDA = 0xDA, //1 byte
        [ParameterData(1)]
        UnkDB = 0xDB, //1 byte
        [ParameterData(1)]
        UnkDE = 0xDE, //1 byte
        [ParameterData(1)]
        UnkDF = 0xDF, //1 byte
        [ParameterData(1)]
        UnkE0 = 0xE0, //1 byte
        [ParameterData(1)]
        UnkE1 = 0xE1, //1 byte
        [ParameterData(1)]
        UnkE4 = 0xE4, //1 byte
        [ParameterData(1)]
        UnkE5 = 0xE5, //1 byte
        [ParameterData(1)]
        UnkE6 = 0xE6, //1 byte
        [ParameterData(1)]
        UnkE7 = 0xE7, //1 byte
        [ParameterData(4)]
        UnkEA = 0xEA, //4 bytes
        [ParameterData(4)]
        UnkEC = 0xEC, //4 bytes
        [ParameterData(4)]
        UnkEE = 0xEE, //4 bytes
        [ParameterData(4)]
        UnkEF = 0xEF, //4 bytes
        [ParameterData(4)]
        UnkF1 = 0xF1, //4 bytes
    }
}
