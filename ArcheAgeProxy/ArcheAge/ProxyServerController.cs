using LocalCommons.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using ArcheAgeProxy.ArcheAge.Network;

namespace ArcheAgeProxy.ArcheAge
{
    /// <summary>
    /// Controller For Game Servers And Authorized Accounts
    /// Contains All Current Game Servers Info and Current Authroized Accounts.
    /// </summary>
    public class ProxyServerController
    {
        private static Dictionary<byte, ProxyServer> proxyservers = new Dictionary<byte, ProxyServer>();

        public static Dictionary<byte, ProxyServer> CurrentGameServers
        {
            get { return proxyservers; }
        }

        public static bool RegisterProxyServer(byte id, string password, ProxyConnection con, short port, string ip)
        {
            Logger.Trace("Proxy Server - id:{0} - Registration", id);
            return true;
        }
        public static bool DisconnecteProxyServer(byte id)
        {
            ProxyServer server = proxyservers[id];
            server.CurrentConnection = null;
            proxyservers.Remove(id);
            proxyservers.Add(id, server);
            return true;
        }

        public static void LoadAvailableProxyServers()
        {
            XmlSerializer ser = new XmlSerializer(typeof(GameServerTemplate));
            GameServerTemplate template = (GameServerTemplate)ser.Deserialize(new FileStream(@"data/Servers.xml", FileMode.Open));
            for (int i = 0; i < template.xmlservers.Count; i++)
            {
                ProxyServer game = template.xmlservers[i];
                game.CurrentAuthorized = new List<int>();
                proxyservers.Add(game.Id, game);
            }

            Logger.Trace("From -- Servers.xml -- Loading - {0} - Servers", proxyservers.Count);
        }
    }

    #region Classes For Server Info Deserialization.

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "servers", Namespace = "", IsNullable = false)]
    public class GameServerTemplate
    {
        [XmlElement("server", Form = XmlSchemaForm.Unqualified)]
        public List<ProxyServer> xmlservers;
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class ProxyServer
    {
        [XmlAttribute]
        public byte Id;

        [XmlIgnore]
        public string IPAddress;

        [XmlIgnore]
        public short Port;

        [XmlIgnore]
        public List<int> CurrentAuthorized;

        [XmlIgnore]
        public ProxyConnection CurrentConnection;

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
