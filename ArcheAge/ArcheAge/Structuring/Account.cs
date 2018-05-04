using ArcheAge.ArcheAge.Net.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAge.ArcheAge.Structuring
{
    /// <summary>
    /// Stucture That Contains Information About Account
    /// </summary>
    public class Account
    {
        private int m_AccId;
        private int m_AccountId;
        private string m_PassHash;
        private string m_Token;
        private byte m_Access;
        private byte m_Membership;
        private string m_LastIp;
        private long m_LastLogged;
        private string m_Name;
        private ClientConnection m_Connection;
        private bool m_WaitingReAuthorization;
        private int m_SessionId;
        private int m_Characters;

        public int Session
        {
            get { return m_SessionId; }
            set { m_SessionId = value; }
        }

        public bool IsWaitingForReAuthorization
        {
            get { return m_WaitingReAuthorization; }
            set { m_WaitingReAuthorization = value; }
        }

        public ClientConnection Connection
        {
            get { return m_Connection; }
            set { m_Connection = value; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int AccId
        {
            get { return m_AccId; }
            set { m_AccId = value; }
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
