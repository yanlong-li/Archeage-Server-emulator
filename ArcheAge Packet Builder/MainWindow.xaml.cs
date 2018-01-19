using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace ArcheAge_Packet_Builder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private short AA_LOGINPORT, AA_GAMEPORT, AA_CHATPORT;
        private string CLIENT_IP;
        private string MINE_IP;
        private PacketFamily m_CurrentFamily;
        public static MainWindow m_CurrentWindow;
        public List<ArcheAgePacket> m_Packets { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            m_CurrentWindow = this;
            DefinePacket.IsEnabled = false;
        }

        #region Reading Pcap
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            m_Packets = null;
            PacketList.SelectionChanged += PacketList_SelectionChanged;
            m_Packets = new List<ArcheAgePacket>();
            OpenFileDialog dial = new OpenFileDialog();
            PacketFamily family = null;
            try
            {
                using (var fs = new FileStream(System.IO.Path.Combine("PacketFamily.xml"), FileMode.Open, FileAccess.Read))
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(PacketFamily));
                    family = ser.Deserialize(reader) as PacketFamily;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
            m_CurrentFamily = family;
            AA_LOGINPORT = 3724;
            bool router = true;
            if (dial.ShowDialog() != null)
            {
                string name = dial.FileName;
                if (String.IsNullOrEmpty(name))
                    return;
                PcapFile file = new PcapFile(name);
                foreach (PcapPacket packet in file)
                {
                    if (packet.Data.Length < 62)
                        continue;
                    BinaryReader reader = new BinaryReader(new MemoryStream(packet.Data));
                    reader.ReadBytes(router ? 26 : 34);

                    string SourceIp = reader.ReadByte() + "." + reader.ReadByte() + "." + reader.ReadByte() + "." + reader.ReadByte();
                    string DestIp = reader.ReadByte() + "." + reader.ReadByte() + "." + reader.ReadByte() + "." + reader.ReadByte();

                    ushort Source = ReverseShort(reader.ReadUInt16());
                    ushort Destination = ReverseShort(reader.ReadUInt16());

                    reader.ReadBytes(16);
                    //Now - Going Data =)

                    if ((reader.BaseStream.Length - reader.BaseStream.Position) > 2)
                    {
                        short len = BitConverter.ToInt16(new byte[] { packet.Data[reader.BaseStream.Position], packet.Data[(reader.BaseStream.Position) + 1] }, 0);
                        //if there's will be data with such length - its ArcheAge =)
                        try
                        {
                            reader.ReadInt16(); //old length :D
                            if (reader.BaseStream.Length - reader.BaseStream.Position == len)
                            {
                                //Construct Packet
                                byte[] data = reader.ReadBytes(len);
                                string type = "";
                                string direct = "";

                                //Set General IP Addresses =)
                                if (CLIENT_IP == null)
                                {
                                    if (Source == AA_LOGINPORT)
                                        CLIENT_IP = SourceIp;
                                    else
                                        CLIENT_IP = DestIp;
                                }

                                if (MINE_IP == null)
                                {
                                    if (Destination == AA_LOGINPORT)
                                        MINE_IP = SourceIp;
                                    else
                                        MINE_IP = DestIp;
                                }

                                short thisport;

                                //Proxies Will not work Correctly.
                                if (Source == AA_LOGINPORT || Destination == AA_LOGINPORT)
                                {
                                    direct = "[LP]";
                                    thisport = AA_LOGINPORT;
                                }
                                else if (Source == AA_GAMEPORT || Destination == AA_GAMEPORT)
                                {
                                    direct = "[GP]";
                                    thisport = AA_GAMEPORT;
                                }
                                else if (Source == AA_CHATPORT || Destination == AA_CHATPORT)
                                {
                                    direct = "[CP]";
                                    thisport = AA_CHATPORT;
                                }
                                else
                                    continue; //Undefined

                                if (SourceIp == CLIENT_IP && DestIp == MINE_IP)
                                    type = "[S]";
                                else if (SourceIp == MINE_IP && DestIp == CLIENT_IP)
                                    type = "[C]";
                                else
                                    continue;

                                m_Packets.Add(new ArcheAgePacket(data, type, direct, thisport));
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                file.Dispose();
                file = null;
                dial = null;

                RefreshListBox(true);
            }
            DefinePacket.IsEnabled = true;
        }
        #endregion

        void PacketList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PacketList.SelectedIndex == -1)
                return;
            HexBox.Document.Blocks.Clear();
            ArcheAgePacket packet = m_Packets[PacketList.SelectedIndex];
            StringBuilder builder = new StringBuilder();
            int offset = 0;
            if(packet.direction.Equals("[GP]"))
                offset += 4;
            else
                offset += 2;
            for (int i = offset; i < packet.data.Length; i++ )
                builder.AppendFormat("{0:X2} ", packet.data[i]);
            HexBox.AppendText(builder.ToString());
            HighlightTextAndShowParts(packet);
            PacketsCurrent.Content = "Текущий: " + PacketList.SelectedIndex;
            if (packet.isDefined && ! packet.name.Contains("Undefined Packet"))
                DefinePacket.Content = "Изменить";
            else
                DefinePacket.Content = "Определить";
        }

        void HighlightTextAndShowParts(ArcheAgePacket packet)
        {
            PartView.Items.Clear();
            int m_GeneralOffset = 0;
            TextPointer m_DocStart = HexBox.Document.ContentStart;
            PacketReader reader = new PacketReader(packet.data, 0);
            bool m_BreakExtremely = false;
            TreeViewItem m_Header = new TreeViewItem();
            if (packet.direction.Equals("[GP]"))
            {
                m_Header.Header = "Packet - Level: " + packet.data[1] + " Opcode: 0x" + BitConverter.ToInt16(packet.data, 2).ToString("X2") + " Undefined: 0x" + packet.data[0].ToString("X2");
                reader.Offset += 4;
            }
            else
            {
                m_Header.Header = "Packet - Opcode 0x" + BitConverter.ToInt16(packet.data, 0).ToString("X2");
                reader.Offset += 2;
            }

            if (packet.parts != null)
            {
                foreach (PacketPart part in packet.parts)
                {
                    if (m_BreakExtremely)
                        break;
                    dynamic value = null;
                    if (part.Type == PartType.None)
                    {
                        MessageBox.Show("Part Type Cannot Be Null - Packet [" + packet.name + "]");
                        Environment.Exit(0);
                        break;
                    }
                    if (part.Type == PartType.ByteArray)
                    {
                        value = "Byte[]: " + part.ByteArrayLength;
                        reader.ReadByteArray(part.ByteArrayLength);
                    }
                    if (value == null)
                        value = ReadDynamicValue(part.Type, reader);
                    bool m_BeenAtArray = false;
                    if (part.ArrayId != null && !part.ArrayId.Equals("0"))
                    {
                        //Read As Array.
                        PacketArray m_CurrentArray = packet.arrays.FirstOrDefault(n => n.ArrayId == part.ArrayId);
                        if (m_CurrentArray == null)
                            continue;
                        if (value is string)
                            continue;

                        bool m_BreakCurrentIteration = false;
                        TreeViewItem i = new TreeViewItem();
                        i.Header = "Iterations";
                        for (int iterations = 0; iterations < (int)value; iterations++)
                        {
                            if (reader.Offset > reader.Size)
                            {
                                PartView.Items.Clear();
                                m_BreakExtremely = true;
                                MessageBox.Show("Specified Position More Than Data Length");
                                break;
                            }
                            TreeViewItem m_CurrentIteration = new TreeViewItem();
                            m_CurrentIteration.Header = "Iteration #" + iterations;
                            foreach (PacketPart p2 in m_CurrentArray.parts)
                            {
                                dynamic m_Dynamic = null;
                                if (p2.Type == PartType.ByteArray)
                                {
                                    m_Dynamic = "Byte[]: " + reader.ReadByteArray(part.ByteArrayLength).Length;
                                }
                                if (m_Dynamic == null)
                                    m_Dynamic = ReadDynamicValue(p2.Type, reader);
                                int m_StringOffset = 0;
                                Color m_ByteArrayColor = Colors.White;
                                if (m_Dynamic is string)
                                {
                                    if (((string)m_Dynamic).StartsWith("Byte[]"))
                                    {
                                        m_StringOffset = part.ByteArrayLength;
                                        m_ByteArrayColor = Colors.MistyRose;
                                    }
                                    else
                                    {
                                        m_StringOffset = ((string)m_Dynamic).Length;
                                        if (p2.Type == PartType.FixedString)
                                            m_StringOffset += 2;
                                    }
                                }
                                HighlighterObject m_SecondHighlighter = m_StringOffset != 0 ? new HighlighterObject(m_StringOffset, m_ByteArrayColor != Colors.White ? m_ByteArrayColor : Colors.Purple) : GetNativeHighlighter(p2.Type.ToString());
                                TextRange range = new TextRange(m_DocStart.GetPositionAtOffset(m_GeneralOffset), m_DocStart.GetPositionAtOffset(m_GeneralOffset += (m_SecondHighlighter.offset * 3 + 1)));
                                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(m_SecondHighlighter.color));
                                m_CurrentIteration.Items.Add("Segment " + p2.Name + " (" + p2.Type.ToString() + ") - Value: " + m_Dynamic);
                                m_SecondHighlighter = null;
                                range = new TextRange(m_DocStart.GetPositionAtOffset(m_GeneralOffset), m_DocStart.GetPositionAtOffset(m_GeneralOffset += 3));
                                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.White));
                                range = null;
                            }
                            i.Items.Add(m_CurrentIteration);
                            if (m_BreakCurrentIteration)
                                break;
                        }
                        m_Header.Items.Add(i);
                        m_BeenAtArray = true;
                    }

                    int m_String = 0;
                    Color bytearrayColor = Colors.White;
                    if (value is string)
                    {
                        if (((string)value).StartsWith("Byte[]"))
                        {
                            m_String = part.ByteArrayLength;
                            bytearrayColor = Colors.MistyRose;
                        }
                        else
                        {
                            m_String = ((string)value).Length;
                            if (part.Type == PartType.FixedString)
                                m_String += 2;
                        }
                    }

                    HighlighterObject highlighter = m_String != 0 ? new HighlighterObject(m_String, bytearrayColor != Colors.White ? bytearrayColor : Colors.Purple) : GetNativeHighlighter(part.Type.ToString());
                    TextRange h_Range = new TextRange(m_DocStart.GetPositionAtOffset(m_GeneralOffset), m_DocStart.GetPositionAtOffset(m_GeneralOffset += (highlighter.offset * 3 + 1)));
                    h_Range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(highlighter.color));
                    if (!m_BeenAtArray)
                        m_Header.Items.Add("Segment " + part.Name + " (" + part.Type.ToString() + ") - Value: " + value);
                    h_Range = new TextRange(m_DocStart.GetPositionAtOffset(m_GeneralOffset), m_DocStart.GetPositionAtOffset(m_GeneralOffset += 3));
                    h_Range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.White));
                    h_Range = null;
                }
            }
            PartView.Items.Add(m_Header);
            reader = null;
        }

        private bool ExtremeDirectionChecking = true;
        private dynamic ReadDynamicValue(PartType part, PacketReader reader)
        {
            dynamic value = null;

            switch (part)
            {
                case PartType.Int16: value = reader.ReadLEInt16(); break;
                case PartType.Int32: value = reader.ReadLEInt32(); break;
                case PartType.Int64: value = reader.ReadLEInt64(); break;
                case PartType.Single: value = reader.ReadLESingle(); break;
                case PartType.FixedString:
                    short l = reader.ReadLEInt16();
                    value = reader.ReadStringSafe(l);
                    break;
                case PartType.DynamicString: value = reader.ReadStringSafe(); break;
                case PartType.Byte: value = reader.ReadByte(); break;
                case PartType.Boolean: value = reader.ReadBoolean(); break;
                default:
                    MessageBox.Show("Undefined Segment Type Found - " + part.ToString());
                    break;
            }

            return value;
        }

        public void RefreshListBox(bool firstLoad)
        {
            PacketList.Items.Clear();
            foreach (ArcheAgePacket packet in m_Packets)
            {
                ListBoxItem item = new ListBoxItem();
                if (packet.type.Equals("[C]"))
                    item.Foreground = Brushes.Green;
                else
                    item.Foreground = Brushes.Red;

                if (ExtremeDirectionChecking)
                {
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(packet.data)))
                    {
                        if ((reader.ReadInt16() - 0x100) >= 0)
                            packet.direction = "[GP]";
                    }
                }

                item.Content = packet.type + " " + packet.direction;
                if (firstLoad)
                {
                    PacketTypeWay way = m_CurrentFamily.getWayByPort(packet.port).getTypeWay(packet.type.Equals("[C]") ? PacketType.CLIENT : PacketType.SERVER);
                    short opcode = BitConverter.ToInt16(packet.data, (packet.direction.Equals("[GP]") ? 2 : 0));

                    Packet p = null;
                    if (packet.PacketLevel != null)
                        item.Content += "";
                    if (way != null)
                        p = way.packets.FirstOrDefault(f => f.opcode.ToString("X2") == opcode.ToString("X2") && f.level == packet.PacketLevel); //we dont want have problems with 0x0C or 0x12
                    if (p == null)
                        item.Content += " Undefined Packet";
                    else
                    {
                        item.Content += " " + p.name;
                        packet.name = p.name;
                        packet.isDefined = true;
                    }

                    item.Content += " Length: " + (packet.data.Length - 2);
                    if (p != null && p.parts != null)
                        packet.parts = p.parts;
                    if (p != null && p.arrays != null)
                        packet.arrays = p.arrays;
                }
                else
                {
                    item.Content += " " + packet.name;
                    item.Content += " Length: " + (packet.data.Length - 2);
                }
                PacketList.Items.Add(item);
            }

            PacketsDefined.Content = "Распознных: " + m_Packets.Count;
        }

        private HighlighterObject GetNativeHighlighter(string part)
        {
            if (part.Equals("Int16"))
                return new HighlighterObject(2, Colors.PaleVioletRed);
            else if (part.Equals("Int32"))
                return new HighlighterObject(4, Colors.Cyan);
            else if (part.Equals("Int64"))
                return new HighlighterObject(8, Colors.Yellow);
            else if (part.Equals("Single"))
                return new HighlighterObject(4, Colors.Green);
            else if (part.Equals("Boolean") || part.Equals("Byte"))
                return new HighlighterObject(1, Colors.SkyBlue);
            else if (part.Equals("Double"))
                return new HighlighterObject(4, Colors.Aqua);
            return null;
        }

        private UInt16 ReverseShort(UInt16 value)
        {
            return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }

        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (m_Packets == null || m_Packets.Count == 0)
                return;
            //Saving Changes
            PacketFamily family = new PacketFamily();
            family.ways = new List<PacketWay>();
            //Filter Ways
            if(m_Packets != null)
            foreach (ArcheAgePacket p1 in m_Packets)
            {
                PacketWay found = family.getWayByPort(p1.port);
                if (found == null)
                {
                    PacketWay m_Way = new PacketWay();
                    m_Way.Port = p1.port;
                    m_Way.typeways = new List<PacketTypeWay>();
                    PacketTypeWay client = new PacketTypeWay();
                    client.packets = new List<Packet>();
                    client.type = PacketType.CLIENT;
                    PacketTypeWay server = new PacketTypeWay();
                    server.packets = new List<Packet>();
                    server.type = PacketType.SERVER;
                    m_Way.typeways.Add(client);
                    m_Way.typeways.Add(server);
                    
                    family.ways.Add(m_Way);
                }
            }
            //Ways Filtered
            //Add Packets
            List<ArcheAgePacket> m_Filtered = new List<ArcheAgePacket>();
            foreach (ArcheAgePacket packet in m_Packets)
            {
                if (packet.name == null || packet.name.Contains("Undefined Packet"))
                    continue;
                if (m_Filtered.Where(n => n.name == packet.name).ToArray().Length > 0)
                    continue;

                m_Filtered.Add(packet);
            }

            foreach (PacketWay way in family.ways)
            {
                PacketWay m_CurrentWay = family.getWayByPort((short)way.Port);
                foreach (ArcheAgePacket packet in m_Filtered)
                {
                    if (way.Port == packet.port)
                    {
                        PacketType type = packet.type.Equals("[C]") ? PacketType.CLIENT : PacketType.SERVER;
                        Packet mSer = new Packet();
                        if (packet.PacketLevel != null)
                            mSer.level = packet.PacketLevel;
                        mSer.parts = packet.parts;
                        mSer.arrays = packet.arrays;
                        mSer.id = "0x" + BitConverter.ToInt16(packet.data, (packet.direction.Equals("[GP]") ? 2 : 0)).ToString("X2");
                        mSer.name = packet.name;
                        List<Packet> packets = way.typeways.Where(n => n.type == type).ToArray()[0].packets;
                        packets.Add(mSer);
                    }
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(PacketFamily));

            var settings = new XmlWriterSettings()
            {
                CheckCharacters = false,
                CloseOutput = false,
                Encoding = new UTF8Encoding(false),
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\n",
            };

            using(var fs = new FileStream(System.IO.Path.Combine("PacketFamily.xml"), FileMode.Create, FileAccess.Write))
            using (XmlWriter writer = XmlWriter.Create(fs, settings))
            {
                writer.WriteComment(" This File Represents Current Defined Packets ");
                writer.WriteComment(" Allowed Only : Int16, Int32, Single, Byte[], DynamicString, FixedString, Byte, Boolean, Double - Types ");
                writer.WriteComment(" Byte[] Syntax: Byte[]:ArrayLength Example: Byte[]:10 ");
                writer.WriteComment(" If you're Using Arrays, Delete Type Mandatory! ");
                writer.WriteComment(" Created By Raphail ");

                serializer.Serialize(writer, family);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void DefinePacket_Click(object sender, RoutedEventArgs e)
        {
            if (PacketList.SelectedIndex == -1)
                PacketList.SelectedIndex = 0;
            DefinePacket packet = new DefinePacket();
            ArcheAgePacket p = m_Packets[PacketList.SelectedIndex];
            if (!p.direction.Equals("[GP]"))
                packet.m_PacketId = "0x" + BitConverter.ToInt16(p.data, 0).ToString("X2");
            else
                packet.m_PacketId = "0x" + BitConverter.ToInt16(p.data, 2).ToString("X2");
            packet.m_PacketType = p.type;
            packet.m_Direction = p.direction;
            packet.m_Level = p.PacketLevel;
            packet.Show();
            packet = null;
        }

        private void mSearch_Click(object sender, RoutedEventArgs e)
        {
            if (mSearchBox.Text.Length == 0)
                return;

            for (int i = 0; i < PacketList.Items.Count; i++)
            {
                if (PacketList.Items[i].ToString().Contains(mSearchBox.Text))
                {
                    if (PacketList.SelectedIndex == i)
                        continue;
                    else
                    {
                        PacketList.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MetroWindow_Closing_1(null, null);
        }
    }

    public class HighlighterObject
    {
        public int offset { get; set; }
        public Color color { get; set; }

        public HighlighterObject(int offset, Color color)
        {
            this.offset = offset;
            this.color = color;
        }
    }

    public class ArcheAgePacket
    {
        public byte[] data { get; set; }
        public String type { get; set; }
        public String direction { get; set; }
        public short port { get; set; }
        public string name { get; set; }
        public List<PacketPart> parts { get; set; }
        public List<PacketArray> arrays { get; set; }
        public bool isDefined { get; set; }

        public string PacketLevel
        {
            get
            {
                if (direction.Equals("[GP]"))
                    return data[1].ToString();
                else
                    return null;
            }
        }

        public ArcheAgePacket(byte[] data, string type, string direction, short port)
        {
            this.data = data;
            this.type = type;
            this.direction = direction;
            this.port = port;
            name = " Undefined Packet";
        }
    }
}
