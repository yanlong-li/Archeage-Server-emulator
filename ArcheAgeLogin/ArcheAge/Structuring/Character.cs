namespace ArcheAgeLogin.ArcheAge.Structuring
{
    /// <summary>
    /// Structure For Character.
    /// </summary>
    public class Character
    {
        public uint CharacterId { get; set; }
        public uint AccountId { get; set; }
        public byte WorldId { get; set; }
        public int Type { get; set; }
        public string CharName { get; set; }
        public byte CharRace { get; set; }
        public byte CharGender { get; set; }
        public string GUID { get; set; }
        public long V { get; set; }
    }
}
