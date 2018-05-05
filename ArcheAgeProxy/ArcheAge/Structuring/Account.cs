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
        private int m_AccountId;
        private string m_PassHash;
        private string m_Token;
        private byte m_Access;
        private byte m_Membership;
        private string m_LastIp;
        private long m_LastLogged;
        private string m_Name;
        private ArcheAgeConnection m_Connection;
        private bool m_WaitingReAuthorization;
        private int sessionId;
        private int characters;

        public int Characters
        {
            get { return characters; }
            set { characters = value; }
        }

        public int Session
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        public bool IsWaitingForReAuthorization
        {
            get { return m_WaitingReAuthorization; }
            set { m_WaitingReAuthorization = value; }
        }

        public ArcheAgeConnection Connection
        {
            get { return m_Connection; }
            set { m_Connection = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int AccountId
        {
            get { return m_AccountId; }
            set { m_AccountId = value; }
        }

        public string Password
        {
            get { return m_PassHash; }
            set { m_PassHash = value; }
        }

        public string Token
        {
            get { return m_Token; }
            set { m_Token = value; }
        }

        public byte AccessLevel
        {
            get { return m_Access; }
            set { m_Access = value; }
        }

        public byte Membership
        {
            get { return m_Membership; }
            set { m_Membership = value; }
        }

        public string LastIp
        {
            get { return m_LastIp; }
            set { m_LastIp = value; }
        }

        public long LastEnteredTime
        {
            get { return m_LastLogged; }
            set { m_LastLogged = value; }
        }
    }
}
