using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SmartEngine.Network;
namespace SagaBNS.Common.Packets
{
    public class BNSLoginPacket : Packet<LoginPacketOpcode>
    {
        public string Command { get; set; }
        public int Serial { get; set; }
        public byte WorldID { get; set; }
        public string Content { get; set; }

        protected XmlDocument ReadLoginPacket()
        {
            Serial = GetInt(2);
            WorldID = GetByte();
            Content = Encoding.UTF8.GetString(GetBytes((ushort)(Length - 7), 7));
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Content);
            return xml;
        }

        public void WritePacket()
        {
            string res = Serial != 0 ? 
                string.Format("{0}\r\nl:{1}\r\ns:{2}R\r\n\r\n{3}", Command, Encoding.UTF8.GetByteCount(Content), Serial,Content) :
                string.Format("{0}\r\nl:{1}\r\n\r\n{2}", Command, Encoding.UTF8.GetByteCount(Content), Content);
            byte[] buf = Encoding.UTF8.GetBytes(res);
            PutBytes(buf, 2);
        }
    }
}
