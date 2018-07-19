using ArcheAgeLogin.ArcheAge.Holders;
using LocalCommons.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using ArcheAgeLogin.ArcheAge.Network;
using ArcheAgeLogin.ArcheAge.Structuring;

namespace ArcheAgeLogin.ArcheAge
{
    /// <summary>
    /// Controller For Game Servers And Authorized Accounts
    /// Contains All Current Game Servers Info and Current Authroized Accounts.
    /// </summary>
    public class GameServerController
    {
        private static Dictionary<byte, GameServer> gameservers = new Dictionary<byte, GameServer>();
        private static Dictionary<long, Account> m_Authorized = new Dictionary<long, Account>();

        public static Dictionary<long, Account> AuthorizedAccounts
        {
            get { return m_Authorized; }
        }

        public static Dictionary<byte, GameServer> CurrentGameServers
        {
            get { return gameservers; }
        }

        public static bool RegisterGameServer(byte id, string password, GameConnection con, short port, string ip)
        {
            if (!gameservers.ContainsKey(id))
            {
                Logger.Trace("Server is not defined - {0}-, please check", id);
                return false;
            }

            GameServer template = gameservers[id]; //Checking Containing By Packet

            if (con.CurrentInfo != null) //Fully Checking.
            {
                con.CurrentInfo = null;
            }

            if (template.password != password) //Checking Password
            {
                Logger.Trace("World Server - {0} - bad password", id);
                return false;
            }

            GameServer server = gameservers[id];
            server.CurrentConnection = con;
            server.IPAddress = ip;
            server.Port = port;
            con.CurrentInfo = server;
            //Update
            gameservers.Remove(id);
            gameservers.Add(id, server);
            Logger.Trace("World server - id: {0} - register", id);
            return true;
        }
        public static bool DisconnecteGameServer(byte id)
        {
            GameServer server = gameservers[id];
            server.CurrentConnection = null;
            gameservers.Remove(id);
            gameservers.Add(id, server);
            return true;
        }

        public static void LoadAvailableGameServers()
        {
            XmlSerializer ser = new XmlSerializer(typeof(GameServerTemplate));
            GameServerTemplate template = (GameServerTemplate)ser.Deserialize(new FileStream(@"data/Servers.xml", FileMode.Open));
            for (int i = 0; i < template.xmlservers.Count; i++)
            {
                GameServer game = template.xmlservers[i];
                game.CurrentAuthorized = new List<long>();
                gameservers.Add(game.Id, game);
            }

            Logger.Trace("From -- Servers.xml -- loading - {0} - servers", gameservers.Count);
        }
    }

    #region Classes For Server Info Deserialization.

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "servers", Namespace = "", IsNullable = false)]
    public class GameServerTemplate
    {
        [XmlElement("server", Form = XmlSchemaForm.Unqualified)]
        public List<GameServer> xmlservers;
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class GameServer
    {
        [XmlAttribute]
        public byte Id;

        [XmlIgnore]
        public string IPAddress;

        [XmlIgnore]
        public short Port;

        [XmlIgnore]
        public List<long> CurrentAuthorized;

        [XmlIgnore]
        public GameConnection CurrentConnection;

        [XmlAttribute]
        public string Name;

        [XmlAttribute]
        public short MaxPlayers;

        [XmlAttribute]
        public string password;

        public bool IsOnline()
        {
            return CurrentConnection != null;
        }
    }

    #endregion
}
