using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArcheAge_Packet_Builder
{
    /// <summary>
    /// Логика взаимодействия для DefinePacket.xaml
    /// </summary>
    public partial class DefinePacket : MetroWindow
    {
        private List<PacketPart> segments = new List<PacketPart>();
        private List<PacketArray> arrays = new List<PacketArray>();
        public string m_PacketId;
        public string m_PacketType;
        public string m_Direction;
        public string m_Level;
        public DefinePacket()
        {
            InitializeComponent();
        }

        private void AddSegment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameSegment.Text))
            {
                MessageBox.Show("Имя не может быть пустым!");
                return;
            }

            if(string.IsNullOrWhiteSpace(TypeSegment.SelectedItem.ToString()))
            {
                MessageBox.Show("Тип Сегмента не может быть пустым");
                return;
            }
            if ((bool)IntoArray.IsChecked)
            {
                if (string.IsNullOrWhiteSpace(ArraySegment.Text))
                {
                    MessageBox.Show("Вы выбрали Функцию IntoArray но ArrayId - Пустой");
                    return;
                }

                PacketArray array = arrays.FirstOrDefault(n => n.ArrayId == ArraySegment.Text);
                if (array == null)
                {
                    array = new PacketArray();
                    array.ArrayId = ArraySegment.Text;
                    array.parts = new List<PacketPart>();
                    PacketPart part = ConstructPart();
                    array.parts.Add(part);
                    arrays.Add(array);

                    Segments.Items.Add("Создан Массив [" + array.ArrayId + "] С Сегментом " +  part.Name + " (" + part.Type.ToString() + ")");
                }
                else
                {
                    PacketPart mPart = ConstructPart();
                    array.parts.Add(mPart);
                    Segments.Items.Add("Добавлен Сегмент Массива " + mPart.Name + " (" + mPart.Type.ToString() + ") Array: " + mPart.ArrayId);
                }
            }
            else
            {
                PacketPart part = ConstructPart();
                segments.Add(part);
                Segments.Items.Add("Добавлен Сегмент " + part.Name + " (" + part.Type.ToString() + ")");
            }
        }

        private PacketPart ConstructPart()
        {
            PacketPart part = new PacketPart();
            if (!string.IsNullOrWhiteSpace(BreakIteration.Text))
                part.BreakItOn = BreakIteration.Text;

            if (!string.IsNullOrWhiteSpace(LengthSegment.Text) && TypeSegment.SelectedItem.ToString().Equals("ByteArray"))
                part.ByteArrayLength = Int32.Parse(LengthSegment.Text);

            if (!string.IsNullOrWhiteSpace(ArraySegment.Text))
                part.ArrayId = ArraySegment.Text;

            part.Name = NameSegment.Text;
            PartType parttype = PartType.None;
            switch (((ComboBoxItem)TypeSegment.SelectedItem).Content.ToString())
            {
                case "Int32":
                    parttype = PartType.Int32;
                    break;
                case "Int16":
                    parttype = PartType.Int16;
                    break;
                case "Int64":
                    parttype = PartType.Int64;
                    break;
                case "Single":
                    parttype = PartType.Single;
                    break;
                case "Double":
                    parttype = PartType.Double;
                    break;
                case "FixedString":
                    parttype = PartType.FixedString;
                    break;
                case "DynamicString":
                    parttype = PartType.DynamicString;
                    break;
                case "Byte":
                    parttype = PartType.Byte;
                    break;
                case "Boolean":
                    parttype = PartType.Boolean;
                    break;
                case "ByteArray":
                    parttype = PartType.ByteArray;
                    break;
                default:
                    MessageBox.Show("Неизвестный Тип Данных: " + TypeSegment.SelectedItem.ToString());
                    break;
            }


            part.Type = parttype;
            return part;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PacketName.Text))
            {
                MessageBox.Show("Имя пакета не может быть пустым");
                return;
            }
            ArcheAgePacket[] packets = MainWindow.m_CurrentWindow.m_Packets.ToArray();
            if (packets != null)
            {
                foreach (ArcheAgePacket packet in packets)
                {
                    if (packet.direction == m_Direction && packet.type == m_PacketType && packet.PacketLevel == m_Level)
                    {
                        string opcode = "0x" + BitConverter.ToInt16(packet.data, (packet.direction.Equals("[GP]") ? 2 : 0)).ToString("X2");
                        if (opcode == m_PacketId)
                        {
                            packet.name = PacketName.Text;
                            packet.isDefined = true;
                            packet.parts = segments;
                            packet.arrays = arrays;
                        }
                    }
                }
            }

            MainWindow.m_CurrentWindow.RefreshListBox(false);
        }
    }
}
