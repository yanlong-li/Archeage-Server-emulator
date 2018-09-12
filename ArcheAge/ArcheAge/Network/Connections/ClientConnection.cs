using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Logging;
using LocalCommons.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using LocalCommons.Cryptography;
using LocalCommons.Utilities;

namespace ArcheAge.ArcheAge.Network.Connections
{

    /// <summary>
    /// Connection That Used For ArcheAge Client( Game Side )
    /// </summary>
    public class ClientConnection : IConnection
    {
        //----- Static
        private static Dictionary<int, Account> m_CurrentAccounts = new Dictionary<int, Account>();
        private readonly byte _mRandom;
        //Fix by Yanlong-LI
        //Исправление входа второго пользователя, вторичный логин, счетчик повторного соединения с возвратом в лобби, вызванный ошибкой
        public byte NumPck = 0;  //修复第二用户、二次登陆、大厅返回重连DD05计数器造成错误问题 BUG глобальный подсчет пакетов DD05
        public static Dictionary<int, Account> CurrentAccounts
        {
            get { return m_CurrentAccounts; }
        }

        public Account CurrentAccount { get; set; }

        public ClientConnection(Socket socket) : base(socket)
        {
            Logger.Trace("Client IP: {0} connected", this);
            DisconnectedEvent += ClientConnection_DisconnectedEvent;
            m_LittleEndian = true;
        }

        public override void SendAsync(NetPacket packet)
        {
            packet.IsArcheAgePacket = true;
            //Fix by Yanlong-LI
            //Переопределяем счетчик для текущего соединения
            NetPacket.NumPckSc = NumPck;//重写为当前连接的计数
            base.SendAsync(packet);
            //Записываем счетчик обратно
            NumPck = NetPacket.NumPckSc;//将计数回写
        }
        public void SendAsyncd(NetPacket packet)
        {
            packet.IsArcheAgePacket = false;
            base.SendAsync(packet);
        }
        void ClientConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            Logger.Trace("Client IP: {0} disconnected", this);
            Dispose();
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);
            //reader.Offset += 1; //Undefined Random Byte
            byte seq = reader.ReadByte();
            byte header = reader.ReadByte(); //Packet Level
            ushort opcode = reader.ReadLEUInt16(); //Packet Opcode
            //обрабатываем пакеты от клиента
            if (header == 0x05 && seq == 00)
            {
                reader.Offset -= 2; //вернемся к hash, count
                byte hash = reader.ReadByte(); //считываем hash или CRC (он не меняется)
                byte count = reader.ReadByte(); //считываем count (шифрован, меняется)
                //------------------------------
                //потом switch уберем
                switch (hash)
                {
                    case 0x33:
                        opcode = 0x008E; //вход в игру5
                        break;
                    case 0x34:
                        opcode = 0x0088; //пакет на релогин из лобби
                        break;
                    //case 0x35:
                    //    opcode = 0x0088; //пакет на релогин из игры
                    //    break;
                    case 0x36:
                        opcode = 0x008F; //вход в игру6
                        break;
                    case 0x37:
                        opcode = 0x008B; //вход в игру2
                        break;
                    case 0x38:
                        opcode = 0x008A; //вход в игру1
                        break;
                    case 0x39:
                        opcode = 0x008C; //вход в игру3
                        break;
                    case 0x3F:
                        opcode = 0x008D; //вход в игру4
                        break;
                        //default:
                        //    msg = "";
                        //    break;
                }
                //------------------------------
                //здесь распаковка пакетов от клиента 
                //здесь будет расшифровка пакета 0005

                ////пробуем расшифровать
                ////byte[] ciphertext = new byte[] {0x13, 0x00, 0x00, 0x05, 0x39, 0x96, 0x41, 0x57, 0x3F, 0x2C, 0xEF, 0x75, 0x3E, 0xC8, 0xC1, 0x75, 0xB5, 0xD2, 0x10, 0x43, 0x62};
                //var pck = new CtoSDecrypt();

                //var size = BitConverter.GetBytes((short) data.Length);
                //data = size.Concat(data).ToArray(); //объединили с Size

                //var ciphertext = Encrypt.CtoSEncrypt(data);
                //Logger.Trace("EncodeXOR:      " + Utility.ByteArrayToString(ciphertext));
                //var plaintext = pck.DecryptAes2(ciphertext);
                //Logger.Trace("EncodeAES:      " + Utility.ByteArrayToString(plaintext));
                ////"13 00 00 05" потеряли
                ////"16 42|50 00 01 00|D7 94 01 00|3D A5 00 00|A4 3E A5" получили
                ////reader.Offset += 2; //пропускаем hash&count
                //Buffer.BlockCopy(plaintext, 2, data, 0, 2); //reader.ReadLEUInt16(); //Packet Opcode
                //opcode = (ushort) BitConverter.ToInt16(plaintext, 2);
            }

            if (!DelegateList.ClientHandlers.ContainsKey(header))
            {
                Logger.Trace("Received undefined packet - seq: {0}, header: {1}, opcode: 0x{2:X2}", seq, header, opcode);
                return;
            }
            try
            {
                PacketHandler<ClientConnection> handler = DelegateList.ClientHandlers[header][opcode];
                if (handler != null)
                {
                    handler.OnReceive(this, reader);
                }
                else
                {
                    Logger.Trace("Received undefined packet - seq: {0}, header: {1}, opcode: 0x{2:X2}", seq, header, opcode);
                }
            }
            catch (Exception)
            {
                Logger.Trace("Received undefined packet - seq: {0}, header: {1}, opcode: 0x{2:X2}", seq, header, opcode);
                throw;
            }
        }
    }
}
