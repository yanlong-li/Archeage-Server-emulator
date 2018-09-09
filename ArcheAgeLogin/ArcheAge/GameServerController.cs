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
    public static class GameServerController
    {
        public static Dictionary<long, Account> AuthorizedAccounts { get; } = new Dictionary<long, Account>();

        public static Dictionary<byte, GameServer> CurrentGameServers { get; } = new Dictionary<byte, GameServer>();

        public static bool RegisterGameServer(byte id, string password, GameConnection con, short port, string ip)
        {
            if (!CurrentGameServers.ContainsKey(id))
            {
                Logger.Trace("Game Server ID: {0} is not defined, please check", id);
                return false;
            }

            GameServer template = CurrentGameServers[id]; //Checking Containing By Packet

            if (con.CurrentInfo != null) //Fully Checking.
            {
                con.CurrentInfo = null;
            }

            if (template.password != password) //Checking Password
            {
                Logger.Trace("Game Server ID: {0} bad password", id);
                return false;
            }

            GameServer server = CurrentGameServers[id];
            server.CurrentConnection = con;
            server.IPAddress = ip;
            server.Port = port;
            con.CurrentInfo = server;
            //Update
            CurrentGameServers.Remove(id);
            CurrentGameServers.Add(id, server);
            Logger.Trace("Game Server ID: {0} registered", id);
            return true;
        }
        public static bool DisconnecteGameServer(byte id)
        {
            GameServer server = CurrentGameServers[id];
            server.CurrentConnection = null;
            CurrentGameServers.Remove(id);
            CurrentGameServers.Add(id, server);
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
                CurrentGameServers.Add(game.Id, game);
            }

            Logger.Trace("Loading from Servers.xml {0} servers", CurrentGameServers.Count);
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
