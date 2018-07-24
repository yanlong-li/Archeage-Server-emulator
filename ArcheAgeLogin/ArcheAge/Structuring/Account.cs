using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheAgeLogin.ArcheAge.Network;

namespace ArcheAgeLogin.ArcheAge.Structuring
{
    /// <summary>
    /// Structure For Accounts.
    /// </summary>
    public class Account
    {
        private string m_PassHash;
        private bool m_WaitingReAuthorization;

        public int Characters { get; set; }

        public int Session { get; set; }

        public bool IsWaitingForReAuthorization
        {
            get { return m_WaitingReAuthorization; }
            set { m_WaitingReAuthorization = value; }
        }

        public ArcheAgeConnection Connection { get; set; }

        public string Name { get; set; }

        //public long AccId { get; set; }

        public long AccountId { get; set; }

        public string Password
        {
            get { return m_PassHash; }
            set { m_PassHash = value; }
        }

        public string Token { get; set; }

        public byte AccessLevel { get; set; }

        public byte Membership { get; set; }

        public string LastIp { get; set; }

        public long LastEnteredTime { get; set; }
    }
}
