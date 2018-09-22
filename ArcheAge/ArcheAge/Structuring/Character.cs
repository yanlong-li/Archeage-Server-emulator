using System;
using System.Collections.Generic;
using LocalCommons.UID;

namespace ArcheAge.ArcheAge.Structuring
{
    /// <summary>
    /// Structure For Character.
    /// </summary>
    public class Character
    {

        public Character()
        {
        }

        public uint AccountId { get; set; }
        public uint CharacterId { get; set; }
        public byte WorldId { get; set; }
        public string CharName { get; set; }
        public byte CharRace { get; set; }
        public byte CharGender { get; set; }
        public string Guid { get; set; }  = "DC0D0CFCD3E01847AD2A5D55EA471CDF"; //для теста // может быть пустой строкой (не null)!
        public long V { get; set; }
        public int[] Type { get; set; } = new int[18];
        public float[] Weight { get; set; } = new float[18];
        public float Scale { get; set; }
        public float Rotate { get; set; }
        public float MoveX { get; set; }
        public float MoveY { get; set; }
        public int Lip { get; set; }
        public int LeftPupil { get; set; }
        public int RightPupil { get; set; }
        public int Eyebrow { get; set; }
        public int Decor { get; set; }
        public string Modifiers { get; set; }
        public byte[] Ability { get; set; } = new byte[3];
        public byte Level { get; set; }
        public byte Ext { get; set; }
        public int CharBody { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public float Z { get; set; }
        public int Head { get; set; }
        public int Chest { get; set; }
        public int Legs { get; set; }
        public int Gloves { get; set; }
        public int Feet { get; set; }
        public int Weapon { get; set; }
        public int WeaponExtra { get; set; }
        public int WeaponRanged { get; set; }
        public int Instrument { get; set; }
        public int NewbieClothPackId { get; set; }
        public int NewbieWeaponPackId { get; set; }
        public int FactionId { get; set; }
        public int StartingZoneId { get; set; }
        public int ModelRef { get; set; }
    }
}
