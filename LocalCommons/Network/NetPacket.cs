using LocalCommons.Cryptography;
using System;
using System.Linq;

namespace LocalCommons.Network
{
    /// <summary>
    /// Abstract Class For Writing Packets
    /// Author: Raphail
    /// </summary>
    public abstract class NetPacket
    {
        protected PacketWriter ns;
        /// <summary>
        /// Опкод пакета
        /// </summary>
        private readonly int m_packetId;
        /// <summary>
        /// Сначала младшие байты
        /// </summary>
        private readonly bool m_littleEndian;
        /// <summary>
        /// Пакет от сервера
        /// </summary>
        private bool m_IsArcheAge;
        /// <summary>
        /// Уровень схатия/шифрования
        /// </summary>
        private readonly byte level;
        //Fix by Yanlong-LI
        /// <summary>
        /// Глобальный подсчет пакетов DD05
        /// </summary>
        //Исправление входа второго пользователя, вторичный логин, счетчик повторного соединения с возвратом в лобби, вызванный ошибкой
        public static byte NumPckSc = 0;  //修复第二用户、二次登陆、大厅返回重连DD05计数器造成错误问题 BUG глобальный подсчет пакетов DD05
        public static sbyte NumPckCs = -1; //глобальный подсчет пакетов 0005

        /// <summary>
        /// Пакет от сервера/клиента
        /// </summary>
        public bool IsArcheAgePacket
        {
            get { return m_IsArcheAge; }
            set { m_IsArcheAge = true; }
        }

        /// <summary>
        /// Creates Instance Of Any Other Packet
        /// </summary>
        /// <param name="packetId">Packet Identifier(opcode)</param>
        /// <param name="isLittleEndian">Send Data In Little Endian Or Not.</param>
        protected NetPacket(int packetId, bool isLittleEndian)
        {
            this.m_packetId = packetId;
            this.m_littleEndian = isLittleEndian;
            ns = PacketWriter.CreateInstance(4092, isLittleEndian);
        }

        /// <summary>
        /// Creates Instance Of ArcheAge Game Packet.
        /// </summary>
        /// <param name="level">Packet Level</param>
        /// <param name="packetId">Packet Identifier(opcode)</param>
        protected NetPacket(byte level, int packetId)
        {
            this.m_packetId = packetId;
            this.level = level;
            this.m_littleEndian = true;
            this.m_IsArcheAge = true;
            ns = PacketWriter.CreateInstance(4092, true);
        }

        /// <summary>
        /// Stream Where We Writing Data.
        /// </summary>
        public PacketWriter UnderlyingStream
        {
            get { return ns; }
        }

        /// <summary>
        /// Compiles Data And Return Compiled byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] Compile()
        {
            PacketWriter temporary = PacketWriter.CreateInstance(4096 * 4, m_littleEndian);
            //temporary.Write((short)(ns.Length + (m_IsArcheAge ? 6 : 2)));
            if (m_IsArcheAge)
            {
                //Серверные пакеты
                if (level == 5)
                {
                    //здесь будет шифрование пакета DD05
                    temporary.Write((short)(ns.Length + 6));

                    temporary.Write((byte)0xDD);
                    temporary.Write((byte)level);

                    //TODO: посмотреть, может по другому написать, через copy?
                    byte[] numPck = new byte[1];
                    numPck[0] = NumPckSc; //вставили номер пакета в массив
                    byte[] data = numPck.Concat(BitConverter.GetBytes((short)m_packetId)).ToArray(); //объединили с ID
                    data = data.Concat(ns.ToArray()).ToArray(); //объединили с телом пакета
                    byte crc8 = Encrypt.Crc8(data); //посчитали CRC пакета
                    byte[] crc = new byte[1];
                    crc[0] = crc8; //вставили crc в массив
                    data = crc.Concat(data).ToArray(); //добавили спереди контрольную сумму
                    byte[] encrypt = Encrypt.StoCEncrypt(data); //зашифровали пакет
                    temporary.Write(encrypt, 0, encrypt.Length);
                    ++NumPckSc; //следующий номер шифрованного пакета DD05
                }
                else
                {
                    temporary.Write((short)(ns.Length + 4));

                    temporary.Write((byte)0xDD);
                    temporary.Write((byte)level);
                    temporary.Write((short)m_packetId);

                    byte[] redata = ns.ToArray();
                    temporary.Write(redata, 0, redata.Length);
                }
            }
            else
            {
                temporary.Write((short)(ns.Length + 2));
                temporary.Write((short)m_packetId);
                byte[] redata = ns.ToArray();
                temporary.Write(redata, 0, redata.Length);
            }
            PacketWriter.ReleaseInstance(ns);
            ns = null;
            byte[] compiled = temporary.ToArray();
            PacketWriter.ReleaseInstance(temporary);
            temporary = null;

            return compiled;
        }
        /// <summary>
        /// Compiles Data And Return Compiled byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] Compile0()
        {
            PacketWriter temporary = PacketWriter.CreateInstance(4096 * 4, m_littleEndian);
            temporary.Write((short)(ns.Length + (m_IsArcheAge ? 4 : 2)));
            if (m_IsArcheAge)
            {
                temporary.Write((byte)0xDD);
                temporary.Write((byte)level);
                temporary.Write((short)m_packetId);
            }
            else
            {
                temporary.Write((short)m_packetId);
            }

            byte[] redata = ns.ToArray();
            PacketWriter.ReleaseInstance(ns);
            ns = null;
            temporary.Write(redata, 0, redata.Length);
            byte[] compiled = temporary.ToArray();
            PacketWriter.ReleaseInstance(temporary);
            temporary = null;
            return compiled;
        }
        /// <summary>
        /// Compiles Data And Return Compiled byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] Compile2()
        {
            PacketWriter temporary = PacketWriter.CreateInstance(4096 * 4, m_littleEndian);

            byte[] redata = ns.ToArray();
            PacketWriter.ReleaseInstance(ns);
            ns = null;
            temporary.Write(redata, 0, redata.Length);
            byte[] compiled = temporary.ToArray();
            PacketWriter.ReleaseInstance(temporary);
            temporary = null;
            return compiled;
        }
    }
}
