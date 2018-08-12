using LocalCommons.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using ArcheAgeStream.ArcheAge.Network;

namespace ArcheAgeStream.ArcheAge
{
    /// <summary>
    /// Controller For Game Servers And Authorized Accounts
    /// Contains All Current Game Servers Info and Current Authroized Accounts.
    /// </summary>
    public class StreamServerController
    {
        private static Dictionary<byte, StreamServer> Streamservers = new Dictionary<byte, StreamServer>();

        public static Dictionary<byte, StreamServer> CurrentGameServers
        {
            get { return Streamservers; }
        }

        public static bool RegisterStreamServer(byte id, string password, StreamConnection con, short port, string ip)
        {
            Logger.Trace("StreamServer ID: {0} registration", id);
            return true;
        }
        public static bool DisconnecteStreamServer(byte id)
        {
            StreamServer server = Streamservers[id];
            server.CurrentConnection = null;
            Streamservers.Remove(id);
            Streamservers.Add(id, server);
            return true;
        }

        public static void LoadAvailableStreamServers()
        {
            XmlSerializer ser = new XmlSerializer(typeof(GameServerTemplate));
            GameServerTemplate template = (GameServerTemplate)ser.Deserialize(new FileStream(@"data/Servers.xml", FileMode.Open));
            for (int i = 0; i < template.xmlservers.Count; i++)
            {
                StreamServer game = template.xmlservers[i];
                game.CurrentAuthorized = new List<int>();
                Streamservers.Add(game.Id, game);
            }

            Logger.Trace("Loading from Servers.xml {0} servers", Streamservers.Count);
        }
    }

    #region Classes For Server Info Deserialization.

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "servers", Namespace = "", IsNullable = false)]
    public class GameServerTemplate
    {
        [XmlElement("server", Form = XmlSchemaForm.Unqualified)]
        public List<StreamServer> xmlservers;
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class StreamServer
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
        public StreamConnection CurrentConnection;

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
