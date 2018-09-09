using System;
using System.Collections.Generic;

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

        public Character(long id, int accountId, int characterId, byte worldId, string charName, byte charRace, byte charGender,
            string gUid, long v, int[] type, float[] weight, float scale, float rotate, float moveX, float moveY,
            int lip, int leftPupil, int rightPupil, int eyebrow, int decor, string modifiers, byte[] ability, byte level, byte ext)
        {
            Id = id;
            AccountId = accountId;
            CharacterId = characterId;
            WorldId = worldId;
            CharName = charName;
            CharRace = charRace;
            CharGender = charGender;
            Guid = gUid;
            V = v;
            Type = type;
            Weight = weight;
            Scale = scale;
            Rotate = rotate;
            MoveX = moveX;
            MoveY = moveY;
            Lip = lip;
            LeftPupil = leftPupil;
            RightPupil = rightPupil;
            Eyebrow = eyebrow;
            Decor = decor;
            Modifiers = modifiers;
            Ability = ability;
            Level = level;
            Ext = ext;
        }

        internal long Id { get; set; }
        public int AccountId { get; set; }
        public int CharacterId { get; set; }
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
    }
}
