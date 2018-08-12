using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheAgeAuth.ArcheAge.Network;

namespace ArcheAgeAuth.ArcheAge.Structuring
{
    /// <summary>
    /// Structure For Accounts.
    /// </summary>
    public class Account
    {
        public byte Characters { get; set; }

        public int Session { get; set; }

        public bool IsWaitingForReAuthorization { get; set; }

        public ArcheAgeConnection Connection { get; set; }

        public string Name { get; set; }

        public long AccountId { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public byte AccessLevel { get; set; }

        public byte Membership { get; set; }

        public string LastIp { get; set; }

        public long LastEnteredTime { get; set; }
    }
}
