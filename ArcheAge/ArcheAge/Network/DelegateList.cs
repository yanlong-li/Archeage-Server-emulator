using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Logging;
using LocalCommons.Network;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ArcheAge.ArcheAge.Network
{
    /// <summary>
    /// Delegate List That Contains Information About Received Packets.
    /// Contains a list of delegates that receive packet information.
    /// </summary>
    public class DelegateList
    {
        private static int m_Maintained;
        private static PacketHandler<LoginConnection>[] m_LHandlers;
        //private static PacketHandler<ClientConnection>[] m_CHandlers;
        private static Dictionary<int, PacketHandler<ClientConnection>[]> levels;
        private static LoginConnection m_CurrentLoginServer;
        private static bool enter1;
        private static bool enter2;
        private static bool enter3;
        private static bool enter4;
        private static bool enter5;
        private static bool enter6;
        private static bool enter7;
        private static bool enter8;
        private static bool enter9;
        private static bool once2;
        private static bool once3;
        private static bool once4;
        private static bool once5;
        private static bool once6;



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
            once2 = true; // если false, то больше не повторять
            once3 = true; // если false, то больше не повторять
            once4 = true; // если false, то больше не повторять
            once5 = true; // если false, то больше не повторять
            once6 = true; // если false, то больше не повторять
            enter1 = false; // если true, то больше не повторять
            enter4 = false; // если true, то больше не повторять
            enter5 = false; // если true, то больше не повторять
            enter6 = false; // если true, то больше не повторять
            enter7 = false; // если true, то больше не повторять
            enter8 = false; // если true, то больше не повторять
            enter9 = false; // если true, то больше не повторять

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
            Register(0x05, 0x0088, new OnPacketReceive<ClientConnection>(OnPacketReceive_ReloginRequest_0x0088));
            Register(0x05, 0x008A, new OnPacketReceive<ClientConnection>(OnPacketReceive_EnterWorld_0x008A));  //вход в игру1
            Register(0x05, 0x008B, new OnPacketReceive<ClientConnection>(OnPacketReceive_EnterWorld_0x008B));  //вход в игру2
            Register(0x05, 0x008C, new OnPacketReceive<ClientConnection>(OnPacketReceive_EnterWorld_0x008C));  //вход в игру3
            Register(0x05, 0x008D, new OnPacketReceive<ClientConnection>(OnPacketReceive_EnterWorld_0x008D));  //вход в игру4
            Register(0x05, 0x008E, new OnPacketReceive<ClientConnection>(OnPacketReceive_EnterWorld_0x008E));  //вход в игру5
            Register(0x05, 0x008F, new OnPacketReceive<ClientConnection>(OnPacketReceive_EnterWorld_0x008F));  //вход в игру6
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

        public static void OnPacketReceive_EnterWorld_0x008A(ClientConnection net, PacketReader reader)
        {
            if (!enter1) //регулируем последовательность входа
            {
                ///клиентский пакет  Recv: 130000053829157BA816DB909183220859E934EFF6
                net.SendAsyncHex(new NP_EnterGame_008A());//вход в игру1, пакет C>s 0x038
                enter1 = true;
            }
        }
        public static void OnPacketReceive_EnterWorld_0x008B(ClientConnection net, PacketReader reader)
        {
            if (enter1) //регулируем последовательность входа
            {
                if (once2) //защитим от повтора посылки пакетов
                {
                    //вход в игру2
                    //13000005371947B88E92319E86B077729237FC244E
                    net.SendAsyncHex(new NP_EnterGame_008B());//вход в игру2, пакет C>s 0x037
                    enter2 = true;
                    once2 = false;
                }
            }
        }
        public static void OnPacketReceive_EnterWorld_0x008C(ClientConnection net, PacketReader reader)
        {
            if (enter2) //регулируем последовательность входа
            {
                if (once3)
                {
                    //вход в игру3
                    //13000005390AEDA4C3949E6A5B4AC06820F2BC202A
                    //13000005370B469961E9F541A6AF4E8DB8BBB3EAFE
                    net.SendAsyncHex(new NP_EnterGame_008C());//вход в игру3, пакет C>s 0x039
                    enter3 = true;
                    enter2 = false;
                    once3 = false;
                }
            }
        }
        public static void OnPacketReceive_EnterWorld_0x008D(ClientConnection net, PacketReader reader)
        {
            if (enter3)
            {
                if (once4)
                {
                    net.SendAsyncHex(new NP_EnterGame_008D());//вход в игру4, пакет C>s 0x03F
                    enter4 = true;
                    enter3 = false;
                    once4 = false;
            }
        }
    }
        public static void OnPacketReceive_EnterWorld_0x008E(ClientConnection net, PacketReader reader)
        {
            if (enter4)
            {
                if (once5)
                {
                    net.SendAsyncHex(new NP_EnterGame_008E());//вход в игру5, пакет C>s 0x033
                    once5 = false;
                    enter5 = true;
                    enter4 = false;
                }
            }
        }
        public static void OnPacketReceive_EnterWorld_0x008F(ClientConnection net, PacketReader reader)
        {
            if (enter5)
            {
                if (once6)
                {
                    net.SendAsyncHex(new NP_EnterGame_008F());//вход в игру6, пакет C>s 0x036
                    once6 = false;
                    enter6 = true;
                    enter5 = false;
                }
            }
        }

        /// <summary>
        /// Verify user login permissions
        /// </summary>
        /// <param name="net"></param>
        /// <param name="reader"></param>
        public static void OnPacketReceive_X2EnterWorld(ClientConnection net, PacketReader reader)
        {
            //v.3.0.3.0
            /*
            [1]             C>s             0ms.            19:48:01 .455      25.07.18
            -------------------------------------------------------------------------------
             TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
            ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
            000000 28 00 00 01 00 00 00 00 | 6D 05 00 00 6D 05 00 00     (.......m...m...
            000010 1A C7 00 00 00 00 00 00 | 28 10 B4 7A FF FF FF FF     .З......(.ґzяяяя
            000020 00 F3 0C 05 00 00 00 00 | 00 00                       .у........
            -------------------------------------------------------------------------------
            Archeage: "X2EnterWorld"                     size: 42     prot: 2  $002
            Addr:  Size:    Type:         Description:     Value:
            0000     2   word          psize             40         | $0028
            0002     2   word          type              256        | $0100
            0004     2   word          ID                0          | $0000
            0006     2   word          type              0          | $0000
            0008     4   integer       p_from            1389       | $0000056D
            000C     4   integer       p_to              1389       | $0000056D
            0010     8   int64         accountId         50970      | $0000C71A
            0018     4   integer       cookie            2058620968 | $7AB41028
            001C     4   integer       zoneId            -1         | $FFFFFFFF
            0020     2   word          tb                62208      | $F300
            0022     4   integer       revision          1292       | $0000050C
            0026     4   integer       index             0          | $00000000
            
            Recv: 2800 0001 0000 0000 6D050000 6D050000 1AC7000000000000 2810B47A FFFFFFFF 00F3 0C050000 00000000
             */
            //type и ID нет в теле пакета (забрано ранее)
            //reader.Offset += 2; //Undefined Random Byte
            short type = reader.ReadLEInt16(); //type
            int pFrom = reader.ReadLEInt32(); //p_from
            int pTo = reader.ReadLEInt32(); //p_to
            long accountId = reader.ReadLEInt64(); //Account Id
            int cookie = reader.ReadLEInt32(); //cookie
            int zoneId = reader.ReadLEInt32(); //User Session Id? zoneId?
            //reader.Offset += 1; //Undefined Random Byte
            short tb = reader.ReadLEInt16(); //tb
            int revision = reader.ReadLEInt32(); //revision, The resource version is the same as the brackets in the client header. (r.321543)
            int index = reader.ReadLEInt32(); //index

            //пропускаем недо X2EnterWorld
            //if (type == 0)
            {
                //Thread.Sleep(100);
                Account m_Authorized = ClientConnection.CurrentAccounts.First(kv => kv.Value.Session == cookie).Value;
                //Account m_Authorized = ClientConnection.CurrentAccounts.First(kv => kv.Value.AccountId == accountId).Value;
                //Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.Session == cookie && kv.Value.AccountId == accountId).Value;
                if (m_Authorized == null)
                {
                    net.Dispose();
                    Logger.Trace("Account ID: {0} not logged in, can not continue", net);
                }
                else
                {
                    net.CurrentAccount = m_Authorized;
                    //нулевой пакет DD05 S>C
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
                //пакет №1 DD05 S>C
                net.SendAsync(new NP_Packet_0x0094()); //1400DD05C2E7E3865627F6CF97087265899FE9242175
                //--------------------------------------------------------------------------
                //SetGameType
                net.SendAsync(new NP_SetGameType());
                //--------------------------------------------------------------------------
                //пакеты для входа в Лобби
                //пакет №2 DD05 S>C
                net.SendAsync(new NP_Packet_0x0034()); //5000DD057F20F4C282625271B0CB11257381D3E43840DBA6F90B2C06A99043486EEFCD4B6745F31F35E70901D0441D85AB78EE825322F4C494643405D5A5754517E7B7875627F7C797704010E0B0115081B0
                //пакет №3 DD05 S>C
                net.SendAsync(new NP_Packet_0x02C3()); //A900DD0574936530FFE0A119356592C1B87D0D80A6E0105A6BBA8A10367483C1AB3C4882A3F1145673BEC8196E6492D9EE3F4690ACF7111C72A0CA052A138BC0F02457CEEABA044766BEC3172173C9D3F536418AFEC91E347486C3E0254B9DACBC16416ABCCD13257981C7AB25579CB3B905596BBBC2052472C8C0F5224398A0E2041E62A0C7162B66909CF3265197ACF5175128B6D710217F92C5AB314B98B0B911236489DFEF51E71747
                //пакет №4 DD05 S>C
                net.SendAsync(new NP_Packet_0x00EC()); //2A00DD05B9656B03D2A2724212E3B3835323F4C494643405B5F1754516E6B6865727F7C797704010E0B08151
                //пакет №5 DD05 S>C
                net.SendAsync(new NP_Packet_0x0281()); //7700DD05F997B53000EEA3724212E2B4845524F4C595643505D7A6764616E7B7875767BED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C49465340AC6A5754566A4B3865727F7C781348DDCAC8F8B99F2
                //пакет №6 DD05 S>C
                net.SendAsync(new NP_Packet_0x00BA()); //0900DD05BFB67E50104170
                //пакет №7 DD05 S>C
                net.SendAsync(new NP_Packet_0x018A()); //1C00DD054863A207D5AF754516E6B9895827F7C897704010E0B0815EC4F4
                //пакет №8 DD05 S>C
                net.SendAsync(new NP_Packet_0x01CC()); //0E00DD05842F7EC797704010E0B08151
                //пакет №9 DD05 S>C
                net.SendAsync(new NP_Packet_0x0030()); //0900DD052CB90D53114270
                //пакет №10 DD05 S>C
                net.SendAsync(new NP_Packet_0x01AF()); //0E00DD054E2DB8C597704010E0B08151
                //пакет №11 DD05 S>C
                net.SendAsync(new NP_Packet_0x02CF()); //2300DD0500E82E825223F4C494643405D5A5754516E6B6865727F7C797704010E0B0815142
                //пакет №12 DD05 S>C
                net.SendAsync(new NP_Packet_0x029C()); //0A00DD05817C5812E0B08151
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
            //пакет №13 DD05 S>C
            net.SendAsync(new NP_Packet_0x0272());   //0700DD050CBD7B5010
            //пакет №14 DD05 S>C
            net.SendAsync(new NP_Packet_0x00EC_2()); //2A00DD05EA6F6B03D3A2724213E3B3835323F4C4946434053B833B2816E6B6865727F7C797704010E0B08151
            //пакет №15 DD05 S>C
            net.SendAsync(new NP_Packet_0x008C());   //FE00DD0595F92296663707D7A7775020F0C090613101D1A1724212E2B2835323F3C494643404D5A5754515E6B6865626F7C7976737FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C596663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393633304D4A4744415E5B5855526F6C696663707D7A7774010E0B0815121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704010E0B08151
            //пакет №16 DD05 S>C
            net.SendAsync(new NP_Packet_0x014D());   //0F00DD050637C9C697704010E0B0815186
            //var ii = GameServerController.AuthorizedAccounts.FirstOrDefault(n => n.Value.AccountId == net.CurrentAccount.AccountId);
            //список чаров
            if (net.CurrentAccount.Characters == 2)
            {
                //пакет №17 DD05 S>C
                net.SendAsync(new NP_Packet_CharList_0x0079()); //0209DD051E05ACB68556F261C495603654B3CB183376E4B591B032F
                                                                //эти пакеты нужны когда есть чары в лобби
                //пакет №18 DD05 S>C
                net.SendAsync(new NP_Packet_0x014F()); //2400DD0564F11F825223F4C495643405D55A754516E634B7D47DF7C797704010E0B081514272
                //пакет №19 DD05 S>C
                net.SendAsync(new NP_Packet_0x0145());   //1D00DD052777B6070231744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423
                //пакет №20 DD05 S>C
                net.SendAsync(new NP_Packet_0x0145_2()); //1D00DD051C70B6070231744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423
                //пакет №21 DD05 S>C
                net.SendAsync(new NP_Packet_0x0145_3()); //1D00DD050D71B6074342744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423
                //пакет №22 DD05 S>C
                net.SendAsync(new NP_Packet_0x0145_4()); //1D00DD05FA72B6074342744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423
            }
            else
            {
                //не забыть установить кол-во чаров в ArcheAgeLoginServer :: ArcheAgePackets.cs :: AcWorldList_0X08
                //пакет №17 DD05 S>C
                net.SendAsync(new NP_Packet_CharList_empty_0x0079()); //0800DD05FEA1C9531140
                //пакет №18 DD05 S>C
                net.SendAsync(new NP_Packet_0x014F()); //2400DD0564F11F825223F4C495643405D55A754516E634B7D47DF7C797704010E0B081514272
            }
            ///идет клиентский пакет 13000005393DB7A29CAA4C2365F02DB94C5B18BB50
            ///идет клиентский пакет 1300000539297EE205DC192D2A33B7071BC23B38BC
            ///идет клиентский пакет 1300000539B1D74AE4C48857E02BAB7E33AF496A8C
            ///идет клиентский пакет 1300000539211BA0D0AC0DE28974E1158F1BE5BB86

            //net.SendAsync(new NP_Packet_quit_0x00A5());
        }
        public static void OnPacketReceive_Client0438(ClientConnection net, PacketReader reader)
        {
            ///клиентский пакет на вход в мир 13000005 3804 2E8CFF98F0282A5A79DE98E9BE80B6
            ///зашифрован - не ловится
        }
        public static void OnPacketReceive_ReloginRequest_0x0088(ClientConnection net, PacketReader reader)
        {
            ///клиентский пакет на релогин Recv: 13 00 00 05 34 0E 6F 39 8E 0A E3 5C E5 B9 85 25 D3 3E B3 8A 74
            net.SendAsync(new NP_Packet_quit_0x01F1()); //Good-Bye
            net.SendAsync(new NP_Packet_0x01E5()); //
        }

        /// <summary>
        /// Получили клиентский пакет Ping, отвечаем серверным пакетом Pong
        /// </summary>
        /// <param name="net"></param>
        /// <param name="reader"></param>
        public static void OnPacketReceive_Ping(ClientConnection net, PacketReader reader)
        {
            //Ping
            long tm = reader.ReadLEInt64(); //tm
            long when = reader.ReadLEInt64(); //when
            int local = reader.ReadLEInt32(); //local
            net.SendAsync(new NP_Pong(tm, when, local));
        }

        /// <summary>
        /// Authenticate user login permissions I do not know how to use, discarded
        /// </summary>
        /// <param name="net"></param>
        /// <param name="reader"></param>
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
                Logger.Trace("Account ID: {0} is not logged in, unable to continue.", net);
            }
            else
            {
                net.CurrentAccount = m_Authorized;
                net.SendAsync(new NP_ClientConnected2());
                net.SendAsync(new NP_Client02());
                //net.SendAsync(new NP_ClientConnected());
            }
        }

        /// <summary>
        /// Connect game server first package
        /// </summary>
        /// <param name="net"></param>
        /// <param name="reader"></param>
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
        /// <param name="net"></param>
        /// <param name="reader"></param>
        private static void Handle_AccountInfoReceived(LoginConnection net, PacketReader reader)
        {
            /*
                5400 0100
                1AC7000000000000 61617465737400 616174657374616100 333165333466326237326439336262323564356632376265386139346334373800 01 01 3132372E302E302E3100 4329871565010000 02 2810B47A
            */
            //Set Account Info
            Account account = new Account
            {
                //reader.Offset += 2; //Undefined Random Byte
                AccountId = reader.ReadLEInt64(),
                Name = reader.ReadDynamicString(),
                Password = reader.ReadDynamicString(),
                Token = reader.ReadDynamicString(),
                AccessLevel = reader.ReadByte(),
                Membership = reader.ReadByte(),
                LastIp = reader.ReadDynamicString(),
                LastEnteredTime = reader.ReadLEInt64(),
                Characters = reader.ReadByte(),
                Session = reader.ReadLEInt32()
            };
            Logger.Trace("Prepare login account ID: " + account.AccountId);
            //Check if the account is online and force it to disconnect online
            //Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.AccountId == account.AccountId).Value;
            //Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.Session == account.Session && kv.Value.AccountId == account.AccountId).Value;
            Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.Session == account.Session).Value;
            if (m_Authorized != null)
            {
                //Already
                Account acc = ClientConnection.CurrentAccounts[m_Authorized.Session];
                if (acc.Connection != null)
                {
                    acc.Connection.Dispose(); //Disconenct  
                    Logger.Trace("Account Name: " + acc.Name + " log in twice, old connection is forcibly disconnected");
                }
                else
                {
                    ClientConnection.CurrentAccounts.Remove(m_Authorized.Session);
                    Logger.Trace("Account Name: " + acc.Name + " double connection is forcibly disconnected");
                }
            }
            else
            {
                ClientConnection.CurrentAccounts.Add(account.Session, account);
                Logger.Trace("Account Name: {0}, Session(cookie): {1} authorized", account.Name, account.Session);
            }
            //if (ClientConnection.CurrentAccounts.ContainsKey(account.Session))
            //{
            //    //Already
            //    Account acc = ClientConnection.CurrentAccounts[account.Session];
            //    if (acc.Connection != null)
            //    {
            //        acc.Connection.Dispose(); //Disconenct  
            //        Logger.Trace("Account " + acc.Name + " Was Forcibly Disconnected");
            //    }
            //    else
            //    {
            //        Logger.Trace("Account " + account.Name + " was forcibly disconnected");
            //        ClientConnection.CurrentAccounts.Remove(account.Session);
            //    }
            //}
            //else
            //{
            //    Logger.Trace("Account {0}: Authorized", account.Name);
            //    ClientConnection.CurrentAccounts.Add(account.Session, account);
            //}

        }

        private static void Handle_GameRegisterResult(LoginConnection con, PacketReader reader)
        {
            bool result = reader.ReadBoolean();
            if (result)
                Logger.Trace("LoginServer successfully installed");
            else
                Logger.Trace("Some problems are appear while installing LoginServer");
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
