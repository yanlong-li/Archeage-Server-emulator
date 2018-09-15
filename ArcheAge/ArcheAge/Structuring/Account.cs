using System;
using System.Collections.Generic;
using ArcheAge.ArcheAge.Holders;
using ArcheAge.ArcheAge.Network.Connections;

namespace ArcheAge.ArcheAge.Structuring
{
    /// <summary>
    /// Stucture That Contains Information About Account
    /// </summary>
    public class Account
    {
        public Account()
        {
        }

        public Account(uint accountId, byte charactersCount, int session, bool isWaitingForReAuthorization,
            ClientConnection connection, Character character, string name, string token, byte accessLevel,
            byte membership, string lastIp, long lastEnteredTime)
        {
            AccountId = accountId;
            CharactersCount = charactersCount;
            Session = session;
            IsWaitingForReAuthorization = isWaitingForReAuthorization;
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            Character = character ?? throw new ArgumentNullException(nameof(character));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Token = token ?? throw new ArgumentNullException(nameof(token));
            AccessLevel = accessLevel;
            Membership = membership;
            LastIp = lastIp ?? throw new ArgumentNullException(nameof(lastIp));
            LastEnteredTime = lastEnteredTime;
        }

        public uint AccountId { get; set; }
        public byte CharactersCount { get; set; }
        public int Session { get; set; } //cookie
        public bool IsWaitingForReAuthorization { get; set; }
        public ClientConnection Connection { get; set; } //текущее соединение
        public Character Character { get; set; } //имеющиеся на аккаунте персонажи
        public string Name { get; set; }
        public string Token { get; set; }
        public byte AccessLevel { get; set; }
        public byte Membership { get; set; }
        public string LastIp { get; set; }
        public long LastEnteredTime { get; set; }
    }
}
