using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcheAge_Packet_Builder
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "PacketFamily", Namespace = "", IsNullable = false)]
    public class PacketFamily
    {
        [XmlElement("PacketWay", Form = XmlSchemaForm.Unqualified)]
        public List<PacketWay> ways;

        public PacketWay getWayByPort(short port)
        {
            try
            {
                return ways.FirstOrDefault(n => n.Port == port);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class PacketWay
    {
        [XmlAttribute]
        public int Port;

        public PacketTypeWay getTypeWay(PacketType type)
        {
            return typeways.FirstOrDefault(n => n.type.Equals(type));
        }

        [XmlElement("TypeWay", Form = XmlSchemaForm.Unqualified)]
        public List<PacketTypeWay> typeways;
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class PacketTypeWay
    {
        [XmlAttribute]
        public PacketType type;

        [XmlElement("packet", Form = XmlSchemaForm.Unqualified)]
        public List<Packet> packets;
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class Packet
    {
        [XmlAttribute]
        public string id;

        [XmlAttribute]
        public string level;

        ushort _op = 0;
        public ushort opcode
        {
            get
            {
                if (_op == 0) _op = ushort.Parse(id.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                return _op;
            }
        }

        [XmlAttribute]
        public string name;

        [XmlElement("part", Form = XmlSchemaForm.Unqualified)]
        public List<PacketPart> parts;

        [XmlElement("array", Form = XmlSchemaForm.Unqualified)]
        public List<PacketArray> arrays;
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class PacketPart
    {
        [XmlAttribute]
        public String Name;

        [XmlAttribute]
        public String ArrayId = null;

        [XmlAttribute]
        public String BreakItOn;

        [XmlAttribute]
        public Int32 ByteArrayLength;

        [XmlAttribute]
        public PartType Type = PartType.None;
    }

    [Serializable]
    [XmlType(Namespace = "", AnonymousType = true)]
    public class PacketArray
    {
        [XmlAttribute]
        public string ArrayId;

        [XmlElement("part", Form = XmlSchemaForm.Unqualified)]
        public List<PacketPart> parts;
    }

    public enum PacketType
    {
        CLIENT,
        SERVER
    }
}
