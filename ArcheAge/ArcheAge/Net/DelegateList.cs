using ArcheAge.ArcheAge.Net.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons.Utilities;
using LocalCommons.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAge.ArcheAge.Net
{
    /// <summary>
    /// Delegate List That Contains Information About Received Packets.
    /// Contains a list of delegates that receive packet information.
    /// </summary>
    public class DelegateList
    {
        private static int m_Maintained;
        private static long m_NumPck = 0;
        private static PacketHandler<LoginConnection>[] m_LHandlers;
        private static PacketHandler<ClientConnection>[] m_CHandlers;
        private static Dictionary<int, PacketHandler<ClientConnection>[]> levels;
        private static LoginConnection m_CurrentLoginServer;

        public static LoginConnection CurrentLoginServer
        {
            get { return m_CurrentLoginServer; }
        }

        public static Dictionary<int, PacketHandler<ClientConnection>[]> ClientHandlers
        {
            get { return levels; }
        }

        public static PacketHandler<LoginConnection>[] LHandlers
        {
            get { return m_LHandlers; }
        }

        public static void Initialize()
        {
            m_LHandlers = new PacketHandler<LoginConnection>[0x20];
            //m_LHandlers = new PacketHandler<ClientConnection>[0x30];
            levels = new Dictionary<int, PacketHandler<ClientConnection>[]>();

            RegisterDelegates();
        }
        /// <summary>
        /// Registration service
        /// Using - Packet Level - Packet Opcode(short) - Receive Delegate
        /// </summary>
        private static void RegisterDelegates()
        {
            //-------------- Login - Game Communication Packets ------------
            //Game Communication Service
            Register(0x00, new OnPacketReceive<LoginConnection>(Handle_GameRegisterResult)); //Taken Fully
            Register(0x01, new OnPacketReceive<LoginConnection>(Handle_AccountInfoReceived)); //Taken Fully

            //-------------- Client Communication Packets ------------------
            //Client Communication Service
            //-------------- Using - Packet Level - Packet Opcode(short) - Receive Delegate -----
            Register(0x01, 0x0000, new OnPacketReceive<ClientConnection>(OnPacketReceive_X2EnterWorld)); //+
            Register(0x02, 0x0012, new OnPacketReceive<ClientConnection>(OnPacketReceive_Ping)); //+
            Register(0x02, 0x0001, new OnPacketReceive<ClientConnection>(OnPacketReceive_FinishState0201)); //+
            Register(0x01, 0xE4FB, new OnPacketReceive<ClientConnection>(OnPacketReceive_ClientE4FB));
            Register(0x01, 0x0D7C, new OnPacketReceive<ClientConnection>(OnPacketReceive_Client0D7C));
            Register(0x01, 0xE17B, new OnPacketReceive<ClientConnection>(OnPacketReceive_ClientE17B));
            Register(0x05, 0x0438, new OnPacketReceive<ClientConnection>(OnPacketReceive_Client0438));
            //Register(0x02, 0x12, new OnPacketReceive<ClientConnection>(Onpacket0212));
            //Register(0x01, 0x7f1b, new OnPacketReceive<ClientConnection>(OnPacketReceive_Client01));
            ////Register(0x01, 0x00, new OnPacketReceive<ClientConnection>(OnPacketReceive_ClientAuthorized));
            ////Register(0x01, 0x1b7f, new OnPacketReceive<ClientConnection>(OnPacketReceive_Client01));
            ////Register(0x02, 0x01, new OnPacketReceive<ClientConnection>(Onpacket0201));
            //////Register(0x02, 0x01, new OnPacketReceive<ClientConnection>(Onpacket0201));
        }

        //private static void Register(int v1, int v2, OnPacketReceive<ClientConnection> onPacketReceive)
        //{
        //    if (v2 == 0xE4FB)
        //       new OnPacketReceive_ClientE4FB;
        //    //throw new NotImplementedException();
        //}

        #region Client Callbacks Implementation
        /// <summary>
        /// Verify user login permissions do not know how to use, abandoned
        /// </summary>
        public static void OnPacketReceive_X2EnterWorld(ClientConnection net, PacketReader reader)
        {
            //v.3.0.0.7
            /*
            [1]             C>s             0ms.            22:13:45 .071      08.06.18
            -------------------------------------------------------------------------------
             TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
            ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
            000000 28 00 00 01 00 00 00 00 | 1C 05 00 00 1C 05 00 00     (...............
            000010 01 00 00 00 00 00 00 00 | 14 49 AB 07 FF FF FF FF     .........I«.ÿÿÿÿ
            000020 00 07 E8 04 00 00 00 00 | 00 00                       ..è.......
            -------------------------------------------------------------------------------
            Archeage: "X2EnterWorld"                     size: 42     prot: 2  $002
            Addr:  Size:    Type:         Description:     Value:
            0000     2   word          psize             40         | $0028
            0002     2   word          type              256        | $0100
            0004     2   word          ID                0          | $0000
            0006     2   word          type              0          | $0000
            0008     4   integer       p_from            1308       | $0000051C
            000C     4   integer       p_to              1308       | $0000051C
            0010     8   int64         accountId         1          | $00000001
            0018     4   integer       cookie            128665876  | $07AB4914
            001C     4   integer       zoneId            -1         | $FFFFFFFF
            0020     2   word          tb                1792       | $0700
            0022     4   integer       revision          1256       | $000004E8
            0026     4   integer       index             0          | $00000000            
            */
            //reader.Offset += 2; //Undefined Random Byte
            short type = reader.ReadLEInt16(); //type
            int pFrom = reader.ReadLEInt32(); //p_from
            int pTo = reader.ReadLEInt32(); //p_to
            long accountId = reader.ReadLEInt64(); //Account Id
            int cookie = reader.ReadLEInt32(); //cookie
            int zoneId = reader.ReadLEInt32(); //User Session Id? zoneId?
            //reader.Offset += 1; //Undefined Random Byte
            short tb = reader.ReadByte(); //tb
            int revision = reader.ReadLEInt32(); //revision
            int index = reader.ReadLEInt32(); //index

            m_NumPck += 1; //глобальный подсчет пакетов DD05

            //пропускаем недо X2EnterWorld
            if (type == 0)
            { 
                Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.AccountId == accountId).Value;
                //Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.Session == cookie && kv.Value.AccountId == accountId).Value;
                if (m_Authorized == null)
                {
                    net.Dispose();
                    Logger.Trace("Account {0} not logged in: can not continue.", net);
                }
                else
                {
                    net.CurrentAccount = m_Authorized;
                    //первый пакет DD05 S>C
                    //0x01 0x0000_X2EnterWorldPacket
                    //net.SendAsyncHex(new NP_X2EnterWorldResponsePacket());
                    net.SendAsync(new NP_X2EnterWorldResponsePacket());
                    //DD02 C>S
                    net.SendAsync(new NP_ChangeState(-1)); //начальный пакет NP_ChangeState с параметром 0
                }
            }
        }
        public static void OnPacketReceive_FinishState0201(ClientConnection net, PacketReader reader)
        {
            int state = reader.ReadLEInt32(); //считываем state
            //--------------------------------------------------------------------------
            //NP_ChangeState
            net.SendAsync(new NP_ChangeState(state));
            //--------------------------------------------------------------------------
            if (state == 0)
            {
                //выводим один раз
                //пакет №8 DD05 S>C
                net.SendAsync(new NP_Packet_0x0055());
                //--------------------------------------------------------------------------
                //SetGameType
                net.SendAsync(new NP_SetGameType());
                //--------------------------------------------------------------------------
                //пакеты для входа в Лобби
                //пакет №10 DD05 S>C
                net.SendAsync(new NP_Packet_0x0006());
                //пакет №11 DD05 S>C
                net.SendAsync(new NP_Packet_0x0005());
                //пакет №12 DD05 S>C
                net.SendAsync(new NP_Packet_0x025A());
                //пакет №13 DD05 S>C
                net.SendAsync(new NP_Packet_0x02D7());
                //пакет №14 DD05 S>C
                net.SendAsync(new NP_Packet_0x01FF());
                //пакет №15 DD05 S>C
                net.SendAsync(new NP_Packet_0x0296());
                //пакет №16 DD05 S>C
                net.SendAsync(new NP_Packet_0x0089());
                //пакет №17 DD05 S>C
                net.SendAsync(new NP_Packet_0x028C());
                //пакет №18 DD05 S>C
                net.SendAsync(new NP_Packet_0x024F());
                //пакет №19 DD05 S>C
                net.SendAsync(new NP_Packet_0x019D());
                //пакет №19 DD05 S>C
                net.SendAsync(new NP_Packet_0x0218());
            }
        }

        public static void OnPacketReceive_ClientE4FB(ClientConnection net, PacketReader reader)
        {
            var number1 = reader.ReadLEInt32();
            //net.SendAsync(new NP_ChangeState(0));
        }

        public static void OnPacketReceive_Client0D7C(ClientConnection net, PacketReader reader)
        {
            var number1 = reader.ReadLEInt32();
            //var number2 = reader.ReadLEInt32();
            //var number3 = reader.ReadLEInt32();
            //var number4 = reader.ReadLEInt32();
            //var number5 = reader.ReadLEInt32();
            //net.SendAsync(new NP_ChangeState(0));
        }
        public static void OnPacketReceive_ClientE17B(ClientConnection net, PacketReader reader)
        {
            ////пакет для входа в Лобби - продолжение
            //пакет №32 DD05 S>C
            net.SendAsync(new NP_Packet_0x01FA());
            net.SendAsync(new NP_Packet_0x025A_2());
            net.SendAsync(new NP_Packet_0x00E4());
            net.SendAsync(new NP_Packet_0x013E());
            //список чаров
            //net.SendAsync(new NP_Packet_CharList_0x0248());
            net.SendAsync(new NP_Packet_0x0248_CharList_empty());
            ///идет клиентский пакет 13000005393DB7A29CAA4C2365F02DB94C5B18BB50
            ///идет клиентский пакет 1300000539297EE205DC192D2A33B7071BC23B38BC
            ///идет клиентский пакет 1300000539B1D74AE4C48857E02BAB7E33AF496A8C
            ///идет клиентский пакет 1300000539211BA0D0AC0DE28974E1158F1BE5BB86
            net.SendAsync(new NP_Packet_0x01AC());
            //net.SendAsync(new NP_Packet_quit_0x00A5());
        }
        public static void OnPacketReceive_Client0438(ClientConnection net, PacketReader reader)
        {
            ///клиентский пакет на вход в мир 13000005 3804 2E8CFF98F0282A5A79DE98E9BE80B6
            ///зашифрован - не ловится
        }

        /**
         * Получили клиентский пакет Ping, отвечаем серверным пакетом Pong
        * */
        public static void OnPacketReceive_Ping(ClientConnection net, PacketReader reader)
        {
            //Ping
            long tm = reader.ReadLEInt64(); //tm
            long when = reader.ReadLEInt64(); //when
            int local = reader.ReadLEInt32(); //local
            net.SendAsync(new NP_Pong(tm, when, local));
        }


        //Authenticate user login permissions I do not know how to use, discarded
        public static void OnPacketReceive_ClientAuthorized(ClientConnection net, PacketReader reader)
        {
            //B3 04 00 00 B3 04 00 00 8C 28 22 00 E7 F0 0C C6 FF FF FF FF 00 
            reader.Offset += 2;
            long protocol = reader.ReadLEInt64(); //Protocols?

            long accountId = reader.ReadLEInt64(); //Account Id
            reader.Offset += 4;
            int sessionId = reader.ReadLEInt32(); //User Session Id
            //Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.AccountId == accountId).Value;
            Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.Session == sessionId && kv.Value.AccountId == accountId).Value;
            if (m_Authorized == null)
            {
                net.Dispose();
                Logger.Trace("Account {0} is not logged in: unable to continue.", net);
            }
            else
            {
                net.CurrentAccount = m_Authorized;
                net.SendAsync(new NP_ClientConnected2());
                net.SendAsync(new NP_Client02());
                //net.SendAsync(new NP_ClientConnected());
            }
        }

        /**
         * 
         * Connect game server first package
         * */
        public static void OnPacketReceive_Client01(ClientConnection net, PacketReader reader)
        {
            net.SendAsyncHex(new NP_Hex("0700dd05f2bdb150102a00dd056f6fcc01d3a2724213e3b3e05321512c00dd0205d012452606e6b6865727f7c797704010e0b081512c00dd021300157f26060000000060bee1d96c0100000000058ef05d96663707d219375020f0b62d01007dd3e50ffe00dd058ef95d96663707d7a7775020f0c090613101d1a1724212e2b2835323f3c494643404d5a5754515e6b6865626f7c7976737fed0a0704011e1b1815122f2c292623303d3a3734414e4b4845525f5c596663606d6a7774717e7c0906030fed1a1714111e2b2825222f3c393633304d4a4744415e5b5855526f6c696663707d7a7774010e0b0815121f1c192623202d2a3734313e3b4845424f4c595653505d6a6764616e7b7875727fed0a0704011e1b1815122f2c292623303d3a3744414e4b4855525f5c596663606d6a7774717e7b0805020f0c191613101d2a2724212e3b3835323f4c494643405d5a5754516e6b680b08151860800dd0520b181510f00dd0552379ac797704010e0b08151860800dd0520a188501140"));
        }
        public static void Onpacket0201(ClientConnection net, PacketReader reader)
        {
            byte b3 = reader.ReadByte();
            if (b3 == 0x0)
            {
                net.SendAsync(new NP_Client0200());//Also returns an error
                net.SendAsyncHex(new NP_Hex("1400dd05fee767865627f6cf97087265899fe9242175"));
                net.SendAsyncHex(new NP_Hex("1e00dd020f000f00735f7069726174655f69736c616e640000000000000000014d00dd05e1606b03c3a31536778cd1e4324092a4fb031865b9ca6f4768d0bf8f29288d0aa62032df76266a421005dc04e238f2c494643405d5a5754516e6b7875626f6c797704010e0b0815152f322a900dd05e1936731ffe0a119356592c1b87d0d80a6e0105a6bba8a10367483c1ab3c4882a3f1145673bec8196e6492d9ee3f4690acf7111c72a0ca052a138bc0f02457ceeaba044766bec3172173c9d3f536418afec91e347486c3e0254b9dacbc16416abccd13257981c7ab25579cb3b905596bbbc2052472c8c0f5224398a0e2041e62a0c7162b66909cf3265197acf5175128b6d710217f92c5ab314b98b0b911236489dfef51e717472a00dd050e65cc01d2a2724212e3b3835323f4c4946434053561754516e6b6865727f7c797704010e0b081517700dd057997103300eea3724212e2b4845524f4c595643505d7a6764616e7b7875767bed0a0704011e1b1815122f2c292623303d3a3744414e4b4855525f5c596663606d6a7774717e7b0805020f0c191613101d2a2724212e3b3835323f4c49465340ac6a5754566a4b3865727f7c781348ddcac8f8b99f20900dd05a9b6e5511041701c00dd05d0635d04d5af754516e6b9895827f7c897704010e0b0815ec4f40e00dd057a2fb0c797704010e0b081510900dd059db9ac531142700e00dd05282df0c797704010e0b081512300dd0523e85c835223f4c494643405d5a5754516e6b6865727f7c797704010e0b08151420a00dd058b7cf511e0b08151"));
            }
            else if (b3 == 0x01)
            {
                net.SendAsync(new NP_Client02002());
            }
            //net.SendAsync(new NP_Clientdd05bae9());
        }

        public static void Onpacket0212(ClientConnection net, PacketReader reader)
        {
            //reader.Offset += 8; //00 00 00 00 00 00 00 00  Undefined Data
            int number1 = reader.ReadLEInt32();
            int number2 = reader.ReadLEInt32();
            int number3 = reader.ReadLEInt32();
            int number4 = reader.ReadLEInt32();
            int number5 = reader.ReadLEInt32();
            net.SendAsync(new NP_Client0212(number1, number2, number3, number4, number5));
        }
        #endregion

        #region LOGIN<->GAME server Callbacks Implementation

        /// <summary>
        /// Логин сервер передал Гейм серверу пакет с информацией об подключаемом аккаунте
        /// </summary>
        private static void Handle_AccountInfoReceived(LoginConnection net, PacketReader reader)
        {
            //Set Account Info
            Account account = new Account
            {
                //reader.Offset += 2; //Undefined Random Byte
                AccountId = reader.ReadLEInt64(), //вместо AccountID
                AccessLevel = reader.ReadByte(),
                Membership = reader.ReadByte(),
                Name = reader.ReadDynamicString(),
                //account.Password = reader.ReadDynamicString();
                Session = reader.ReadLEInt32(),
                LastEnteredTime = reader.ReadLEInt64(),
                LastIp = reader.ReadDynamicString()
            };
            Logger.Trace("Prepare login account: <<" + account.AccountId + ">>;");
            //Check if the account is online and force it to disconnect online
            Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.AccountId == account.AccountId).Value;
            if (m_Authorized != null)
            {
                //Already
                Account acc = ClientConnection.CurrentAccounts[m_Authorized.Session];
                if (acc.Connection != null)
                {
                    acc.Connection.Dispose(); //Disconenct  
                    Logger.Trace("Account <<" + acc.Name + ">> log in twice, old connection is forcibly disconnected");
                }
                else
                {
                    ClientConnection.CurrentAccounts.Remove(account.Session);
                }
            }
            else
            {
                Logger.Trace("Account <<{0}>>, Session(cookie) <<{1}>>: Authorized", account.Name, account.Session);
                ClientConnection.CurrentAccounts.Add(account.Session, account);
            }
        }

        private static void Handle_GameRegisterResult(LoginConnection con, PacketReader reader)
        {
            bool result = reader.ReadBoolean();
            if (result)
                Logger.Trace("Successfully installed login server");
            else
                Logger.Trace("Some issues arise when installing the login server");
            if (result)
                m_CurrentLoginServer = con;
        }
        #endregion

        private static void Register(short opcode, OnPacketReceive<LoginConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<LoginConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(byte level, ushort opcode, OnPacketReceive<ClientConnection> e)
        {
            if (!levels.ContainsKey(level))
            {
                PacketHandler<ClientConnection>[] handlers = new PacketHandler<ClientConnection>[0xFFFF];
                handlers[opcode] = new PacketHandler<ClientConnection>(opcode, e);
                levels.Add(level, handlers);
            }
            else
            {
                levels[level][opcode] = new PacketHandler<ClientConnection>(opcode, e);
            }
        }

    }
}
