using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;
using LocalCommons.Cryptography;
using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons.Utilities;
using System;
using System.Linq;
using System.Security.Cryptography;
// ReSharper disable All

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Packet List That Contains All Game / Client Packet Delegates.
    /// </summary>
    public static class PacketList
    {
        private static int m_Maintained;
        private static PacketHandler<GameConnection>[] m_GHandlers;
        private static PacketHandler<ArcheAgeConnection>[] m_LHandlers;
        private static string clientVersion;

        public static PacketHandler<GameConnection>[] GHandlers
        {
            get { return m_GHandlers; }
        }

        public static PacketHandler<ArcheAgeConnection>[] LHandlers
        {
            get { return m_LHandlers; }
        }

        public static void Initialize(string clientVersion)
        {
            PacketList.clientVersion = clientVersion;
            m_GHandlers = new PacketHandler<GameConnection>[0x20];
            m_LHandlers = new PacketHandler<ArcheAgeConnection>[0x30];

            Registration();
        }

        private static void Registration()
        {
            //для теста
            //uint xorKey = 0xFFC54C94; //xor_key
            //xorKey = xorKey * xorKey & 0xffffffff; //TODO: найти откуда берется!!!;
            //byte[] aesKey = Utility.StringToByteArrayFastest("A9B3F6A8A7D53C7CB525EACA39FAF3E8"); //TODO: найти откуда берется!!!;
            //byte[] iv = new byte[16]; //GenerateIv(); //для дешифрации первого пакета iv = 16 нулей 
            ////для дешифрации следующих пакетов iv = шифрованный предыдущий пакет

            //string msg = "13000005396D07058A4B4A74C112C53F02B73B9362";
            //Logger.Trace("Encode: " + msg);
            //byte[] ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //byte[] plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:0005394250000100D79401003DA500003EA500");
            //Logger.Trace("");

            //msg = "130000053935561344F2D4C469AE603C48832F928F";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000539EB50000200D7940100C53F02B73B9362");
            //Logger.Trace("");

            //msg = "13000005392172A774B0F3173687662E0493E7E90C";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000539B35000010096E70100603C48832F928F");
            //Logger.Trace("");

            //------------------------------------------------------------------------------------------------
            //Game Server Delegates Packets
            //------------------------------------------------------------------------------------------------
            Register(0x00, Handle_RegisterGameServer);//Level registration server
            Register(0x02, Handle_UpdateCharacters);//Level registration server
            //------------------------------------------------------------------------------------------------
            //END Game Server Delegates Packets
            //------------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------------
            //Client Delegates Packets
            //------------------------------------------------------------------------------------------------
            switch (clientVersion)
            {   
                case "1"://1.0.1406 Feb 11 2014
                    Register(0x04, Handle_CARequestAuth_0X04); //пакет №1 от клиента
                    Register(0x0A, Handle_CAListWorld_0x0A);  //
                    Register(0x0B, Handle_CAEnterWorld_0x0B);  //
                    break;
                case "3": //3.0.3.0
                    Register(0x06, Handle_CAChallengeResponse2_0X06); //пакет №1 от клиента
                    Register(0x0c, Handle_CACancelEnterWorld_0X0C); //пакет №2 от клиента
                    Register(0x0d, Handle_CARequestReconnect_0X0D); //пакет №3 от клиента
                    Register(0x0f, Handle_CARequestReconnect_0X0F); //пакет №3 от клиента
                    break;
                default:
                    Register(0x06, Handle_CAChallengeResponse2_0X06); //пакет №1 от клиента
                    Register(0x0c, Handle_RequestServerList); //Return to server list<=2.9
                    Register(0x0d, Handle_ServerSelected);//Return server address based on server id
                    break;
            }
            //------------------------------------------------------------------------------------------------
            //END Client Delegates Packets
            //------------------------------------------------------------------------------------------------
        }

        #region Game Server Delegates
        //используется
        private static void Handle_UpdateCharacters(GameConnection net, PacketReader reader)
        {
            long accountId = reader.ReadLEInt64();
            byte characters = reader.ReadByte(); //количество чаров на аккаунте
            Account currentAcc = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            currentAcc.Characters = characters;
        }

        //используется
        private static void Handle_RegisterGameServer(GameConnection net, PacketReader reader)
        {
            byte id = reader.ReadByte();
            short port = reader.ReadLEInt16();
            string ip = reader.ReadDynamicString();
            string password = reader.ReadDynamicString();
            bool success = GameServerController.RegisterGameServer(id, password, net, port, ip);
            net.SendAsync(new NET_GameRegistrationResult(success));
        }
        #endregion

        #region Client Delegates
        /// <summary>
        /// для версии2014 года
        /// </summary>
        /// <param name="net"></param>
        /// <param name="reader"></param>
         private static void Handle_CARequestAuth_0X04(ArcheAgeConnection net, PacketReader reader)
        {
            //3F00 0400 0A000000 0700000000 08000000000000000000 0600 616174657374200031E34F2B72D93BB25D5F27BE8A94C47800000000000000000000000000000000
            //3F00 0400 0A000000 0700000000 08000000000000000000 0600 616174657374200031E34F2B72D93BB25D5F27BE8A94C47800000000000000000000000000000000

            reader.Offset += 19; //скипаем 19 байт
            int m_RUidLength = reader.ReadLEInt16(); //длина строки
            string m_Uid = reader.ReadString(m_RUidLength); //считываем ID
            //long accId = Convert.ToInt64(m_Uid);
            int m_RtokenLength = reader.ReadLEInt16(); // длина строки
            string m_RToken = reader.ReadHexString(m_RtokenLength); //считываем токен
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_Uid);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                //account numberexist
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                    {
                        //Удалим результаты предыдущего коннекта для нормального реконнекта
                        GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    }
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                    //net.SendAsyncHex(new NP_Hex("0C00000000000300000000000000"));
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    //net.SendAsyncHex(new NP_Hex("280003005833000020003236393631326537613630393431313862623735303764626334326261353934"));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    //net.SendAsyncHex(new NP_Hex("0C00000000000300000000000000"));
                    //net.SendAsyncHex(new NP_Hex("0C00000000000600000000000000"));
                    //    000000000600000000000000
                    //0C00000000000300000000000000
                    //03005833000020003236393631326537613630393431313862623735303764626334326261353934
                    return;
                }
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name +
                             " token verification failed：" + m_RToken.ToLower());
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + m_Uid);
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Logger.Trace("Create new account: " + m_Uid);
                    Account m_New = new Account
                    {
                        AccountId = AccountHolder.AccountList.Count + 1,
                        LastEnteredTime = Utility.CurrentTimeMilliseconds(),
                        AccessLevel = 1,
                        LastIp = net.ToString(),
                        Membership = 1,
                        Name = m_Uid,
                        Password = "change_password_now",
                        Token = m_RToken,
                        Characters = 0
                    };
                    net.CurrentAccount = m_New;
                    AccountHolder.InsertOrUpdate(m_New);
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + net.CurrentAccount.AccountId + " & Account Name: " + net.CurrentAccount.Name + " landing success");
                    net.SendAsyncHex(new NP_Hex("0C00000000000300000000000000"));
                    //net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsyncHex(new NP_Hex("280003005833000020003236393631326537613630393431313862623735303764626334326261353934"));
                    //net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }

                net.CurrentAccount = null;
                Logger.Trace("Сan not create account: " + m_Uid);
            }
            //If the front did not terminate, then the account number failed to log in
            net.SendAsync(new NP_ACLoginDenied_0x0C());
        }
        private static void Handle_CAListWorld_0x0A(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsync(new AcWorldList_0X08(clientVersion, net));
            //net.SendAsyncHex(new NP_Hex("A802080018010A00D09BD183D186D0B8D0B90101000200000202020000020E00D09AD0B8D0BFD180D0BED0B7D0B00101000200000202020000031000D09CD0B5D0BBD0B8D181D0B0D180D0B00101000200000202020000040800D0A2D0B0D18FD0BD0101000200000202020000051200D090D180D0B0D0BDD0B7D0B5D0B1D0B8D18F0101000200000202020000060800D09ED0BBD0BBD0BE0101000200000202020000070800D090D0BDD0BDD0B00101000200000202020000080E00D090D180D0B0D0BDD0B7D0B5D0B10101000200000202020000090800D098D0BDD0BED18501010002000002020200000A0800D094D0B6D0B8D0BD01020000000000000000000B0E00D09ED180D185D0B8D0B4D0BDD0B001010000000000000000000C0A00D09DD0B0D0B8D0BCD0B001010000000000000000000D1000D090D0BDD182D0B0D0BBD0BBD0BED0BD01010002000002020200000E0E00D0A8D0B0D182D0B8D0B3D0BED0BD01010002000002020200000F0800D090D0B9D18DD1800101000200000202020000101000D0A1D0B0D0BBD18CD184D0B8D180D0B00102000000000000000000110A00D094D0B0D183D182D0B00101000000000000000000120E00D09AD0B0D0BBD0B5D0B8D0BBD18C0101000000000000000000130C00D09AD0B8D180D0B8D0BED1810101000000000000000000140E00D090D0BAD180D0B8D182D0B5D1810101000000000000000000150C00D0ADD0BDD188D0B0D0BAD0B00101000000000000000000160E00D090D188D0B0D0B1D0B5D0BBD18C0101000000000000000000170E00D09AD0B0D0BFD0B0D0B3D0B0D0BD0101000000000000000000180A00D09DD0B5D0B2D0B5D1800102000000000000000000018FA90D000BFF091A000B004A757374746F636865636B010210000E4FC3755AE17949B1F626620F354A930000000000000000"));
        }
        private static void Handle_CAEnterWorld_0x0B(ArcheAgeConnection net, PacketReader reader)
        {
            //net.SendAsyncHex(new NP_Hex("13000A008D0EC89A0E003132372E302E302E31D704"));
            //0B00 0D00 00000000 00000000 01
            int p_from = reader.ReadLEInt32();
            int p_to = reader.ReadLEInt32();
            byte serverId = reader.ReadByte(); //serverId
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server != null && server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    net.CurrentAccount.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    net.CurrentAccount.LastIp = net.ToString(); // IP
                    // генерируем cookie
                    Random random = new Random();
                    int cookie = random.Next(255);
                    cookie += random.Next(255) << 8;
                    cookie += random.Next(255) << 16;
                    cookie += random.Next(255) << 24;
                    net.CurrentAccount.Session = cookie; //Designated session
                    //Передаем управление Гейм серверу
                    net.MovedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    //отсылаем Гейм серверу информацию об аккаунте
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(clientVersion, net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    //отсылаем Клиенту информацию о куках
                    net.SendAsync(new AcWorldCookie_0X0A(clientVersion, server, cookie));
                }
            }
            else
            {
                Logger.Trace("No serverID requested：" + serverId);
                net.Dispose();
            }
        }

        /// <summary>
        /// 0x06_CAChallengeResponse2Packet - token Verification mode
        /// uid+token
        /// </summary>
        private static void Handle_CAChallengeResponse2_0X06(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset += 19; //скипаем 19 байт
            int m_RUidLength = reader.ReadLEInt16(); //длина строки
            string m_Uid = reader.ReadString(m_RUidLength); //считываем имя "aatest"
            int m_RtokenLength = reader.ReadLEInt16(); // длина строки
            string m_RToken = reader.ReadHexString(m_RtokenLength); //считываем токен
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_Uid);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                //account numberexist
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                    {
                        //Удалим результаты предыдущего коннекта для нормального реконнекта
                        GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    }
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name +
                             " token verification failed：" + m_RToken.ToLower());
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + m_Uid);
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Logger.Trace("Create new account: " + m_Uid);
                    Account m_New = new Account
                    {
                        AccountId = AccountHolder.AccountList.Count + 1,
                        LastEnteredTime = Utility.CurrentTimeMilliseconds(),
                        AccessLevel = 1,
                        LastIp = net.ToString(),
                        Membership = 1,
                        Name = m_Uid,
                        Password = "new_password",
                        Token = m_RToken,
                        Characters = 0
                    };
                    net.CurrentAccount = m_New;
                    AccountHolder.InsertOrUpdate(m_New);
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + net.CurrentAccount.AccountId + " & Account Name: " + net.CurrentAccount.Name + " landing success");
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }

                net.CurrentAccount = null;
                Logger.Trace("Сan not create account: " + m_Uid);
            }
            //If the front did not terminate, then the account number failed to log in
            net.SendAsync(new NP_ACLoginDenied_0x0C());
        }

        private static void Handle_CACancelEnterWorld_0X0C(ArcheAgeConnection net, PacketReader reader)
        {
            //var unknown = reader.ReadByteArray(8); //unk?
            net.SendAsync(new AcWorldList_0X08(clientVersion, net));
            //net.SendAsync(new AcAccountWarned_0X0D(clientVersion)); //не обязателен
        }

        /// <summary>
        /// Client choose server to send serverIP, server port number, sessionID
        ///</summary>>
        private static void Handle_CARequestReconnect_0X0D(ArcheAgeConnection net, PacketReader reader)
        {
            /*
             [7]             C>s             0ms.            23:56:45 .957      10.03.18
               -------------------------------------------------------------------------------
               TType: ArcheageServer: undef   Parse: 6           EnCode: off         
               ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
               000000 0B 00 0D 00 00 00 00 00 | 00 00 00 00 01              .............
               -------------------------------------------------------------------------------
               Archeage: "CARequestReconnect"               size: 13     prot: 2  $002
               Addr:  Size:    Type:         Description:     Value:
               0000     2   word          psize             11         | $000B
               0002     2   word          ID                13         | $000D
               0004     4   integer       p_from            0          | $00000000
               0008     4   integer       p_to              0          | $00000000
               000C     1   byte          serverId          1          | $01
                        4   integer       cookie
                        ?   WideStr[byte] MAC
             */
            //0B00 0D00 00000000 00000000 01
            //reader.Offset += 8; //Undefined Data
            int p_from = reader.ReadLEInt32();
            int p_to = reader.ReadLEInt32();
            byte serverId = reader.ReadByte(); //serverId
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server != null && server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    net.CurrentAccount.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    net.CurrentAccount.LastIp = net.ToString(); // IP
                    //net.CurrentAccount.AccountId = net.CurrentAccount.AccountId; // 
                    //create session (cookie)
                    ///var cookie = 128665876; //$07AB4914 - для теста
                    ///net.CurrentAccount.Session = cookie;
                    //AccountHolder.AccountList.FirstOrDefault(n => n.AccId == Convert.ToInt32(cookie));

                    // генерируем cookie
                    Random random = new Random();
                    int cookie = random.Next(255);
                    cookie += random.Next(255) << 8;
                    cookie += random.Next(255) << 16;
                    cookie += random.Next(255) << 24;
                    net.CurrentAccount.Session = cookie; //Designated session

                    //Передаем управление Гейм серверу
                    net.MovedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    //отсылаем Гейм серверу информацию об аккаунте
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(clientVersion, net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    //отсылаем Клиенту информацию о куках
                    net.SendAsync(new AcWorldCookie_0X0A(clientVersion, server, cookie));
                }
            }
            else
            {
                Logger.Trace("No serverID requested：" + serverId);
                net.Dispose();
            }
        }

        /// <summary>
        /// Обрабатываем приход пакета из Лобби "Выбор сервера"
        ///</summary>>
        private static void Handle_CARequestReconnect_0X0F(ArcheAgeConnection net, PacketReader reader)
        {
            /*
             [1]             C>s             0ms.            14:01:18 .890      21.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: LS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             000000 21 00 0F 00 0A 00 00 00 | 08 00 00 00 01 00 00 00     !...............
             000010 00 00 00 00 01 09 1F 83 | 1D 08 00 00 00 00 00 00     .......ƒ........
             000020 00 00 00                                              ...
             -------------------------------------------------------------------------------
             Archeage: "CARequestReconnect"               size: 35     prot: 2  $002
             Addr:  Size:    Type:         Description:     Value:
             0000     2   word          psize             33         | $0021
             0002     2   word          ID                15         | $000F
             0004     4   integer       p_from            10         | $0000000A
             0008     4   integer       p_to              8          | $00000008
             000C     8   int64         accountId         1          | $00000001
             0014     4   integer       cookie            -2095118079 | $831F0901
             0018  2079   WideStr[byte] MAC               00:00:00:00:00:00:00:00:00  ($)
             */

            int p_from = reader.ReadLEInt32();
            int p_to = reader.ReadLEInt32();
            long accountId = reader.ReadLEInt64();
            int cookie = reader.ReadLEInt32();

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                net.CurrentAccount = n_Current;
                //Write account number information Write Online account list
                GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + accountId);
            }
        }

        private static void Handle_SignIn(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset += 12; //Static Data - 0A 00 00 00 07 00 00 00 00 00 
            int m_RLoginLength = reader.ReadLEInt16();
            reader.Offset += 2;
            string m_RLogin = reader.ReadString(m_RLoginLength); //Reading Login
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_RLogin);
            if (n_Current == null)
            {
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Account m_New = new Account
                    {
                        AccountId = AccountHolder.AccountList.Count + 1,
                        LastEnteredTime = Utility.CurrentTimeMilliseconds(),
                        AccessLevel = 0,
                        LastIp = net.ToString(),
                        Membership = 0,
                        Name = m_RLogin
                    };
                    net.CurrentAccount = m_New;
                    AccountHolder.AccountList.Add(m_New);
                }
                else
                {
                    net.CurrentAccount = null;
                }
            }
            else
            {
                net.CurrentAccount = n_Current;
            }
            // net.SendAsync(new NP_PasswordCorrect(1));
            net.SendAsync(new NP_ServerList(clientVersion));
        }

        private static void Handle_SignIn_Continue(ArcheAgeConnection net, PacketReader reader)
        {
            //HOW TO DECRYPT IT ????
            //string password = "";
            //If the account is not empty, login fails
            if (net.CurrentAccount == null)
            {
                //Return login failure information
                net.SendAsync(new NP_FailLogin());
                return;
            }

            /* TODO
            if (net.CurrentAccount.Password == null)
            {
                //Means - New Account.
                net.CurrentAccount.Password = password;
            }
            else
            {
                //Checking Password
                if (net.CurrentAccount.Password != password)
                {
                    net.SendAsync(new NP_FailLogin());
                    return;
                }
            }
            */
            net.SendAsync(new NP_AcceptLogin(clientVersion));
            net.CurrentAccount.Session = net.GetHashCode();
            net.SendAsync(new NP_PasswordCorrect(net.CurrentAccount.Session));
            Logger.Trace("Account login: " + net.CurrentAccount.Name);
            GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
        }

        /**
         *token Verification mode
         *uid+token
         * 
         */
        private static void Handle_Token_Continue(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset = 21;
            int m_RUidLength = reader.ReadLEInt16();
            string m_uid = reader.ReadString(m_RUidLength); //Reading Login
            int m_RtokenLength = reader.ReadLEInt16();
            string m_RToken = reader.ReadHexString(m_RtokenLength);
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == Convert.ToInt64(m_uid));
            if (n_Current != null)
            {
                Logger.Trace("account number: < " + n_Current.AccountId + ":" + n_Current.Name + "> is landing");
                //accounts exist
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    //Write account information to online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account: < " + n_Current.AccountId + ":" + n_Current.Name + "> landing success");
                    net.SendAsync(new NP_AcceptLogin(clientVersion));
                    net.SendAsync(new NP_03key(clientVersion));
                    //return server list
                    //net.SendAsync(new NP_ServerList());
                    return;
                }
                Logger.Trace("Account: < " + n_Current.AccountId + ":" + n_Current.Name + "> token verification failed: " + m_RToken.ToLower());

            }
            else
            {
                Logger.Trace("Client attempts to login to a nonexistent account" + m_uid);
            }

            //If there is no termination before, the account login fails
            net.SendAsync(new NP_FailLogin());
        }


        private static void Handle_Token_Continue2(ArcheAgeConnection net, PacketReader reader)
        {

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == 1);
            if (n_Current != null)
            {
                Logger.Trace("The account is trying to login: " + n_Current.Name);
                //Account exists
                // if (n_Current.Password.ToLower() == m_RToken.ToLower())
                // {
                net.CurrentAccount = n_Current;
                //Write account information to online account list
                GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                Logger.Trace("Account login successful: " + net.CurrentAccount.Name);
                net.SendAsync(new NP_AcceptLogin(clientVersion));
                net.SendAsync(new NP_03key(clientVersion));
                //Return to server list
                //net.SendAsync(new NP_ServerList());
                return;
                //  }
                // Logger.Trace("account number: " + net.CurrentAccount.Name + "/Incorrect password：" + m_RToken.ToLower());
            }
            //If the previous did not stop, then account landing failed
            net.SendAsync(new NP_FailLogin());
        }

        //Send server list (based on packet capture)
        private static void Handle_05(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsyncHex(new NP_PasswordCorrect(1));
        }

        //Return server connection into packets
        private static void Handle_0d(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsync0d(new NP_PasswordCorrect(1));
            //net.SendAsync(new NP_ServerList());
        }

        private static void Handle_RequestServerList(ArcheAgeConnection net, PacketReader reader)
        {
            byte[] unknown = reader.ReadByteArray(8); //unk?
            net.SendAsync(new NP_ServerList(clientVersion));
        }

        /**
         * 
         * Client selects server to send
         * Server IP
         * Server port number
         * sessionID
         * */
        private static void Handle_ServerSelected(ArcheAgeConnection net, PacketReader reader)
        {
            //net.SendAsync(new NP_EditMessage2("systemTest"));
            //return;
            reader.Offset += 8; //00 00 00 00 00 00 00 00  Undefined Data
            byte serverId = reader.ReadByte();
            //serverId =1;
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server != null && server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    //create session
                    //Random random = new Random();
                    //int num = random.Next(255) + random.Next(255) + random.Next(255) + random.Next(255);
                    //net.CurrentAccount.Session = num= 1323126619;//Specify session

                    // генерируем cookie
                    Random random = new Random();
                    int cookie = random.Next(255);
                    cookie += random.Next(255) << 8;
                    cookie += random.Next(255) << 16;
                    cookie += random.Next(255) << 24;
                    net.CurrentAccount.Session = cookie; //Designated session

                    net.MovedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(clientVersion, net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    net.SendAsync(new NP_SendGameAuthorization(server, cookie));
                }
            }
            else
            {
                Logger.Trace("Requested a non-existent server ID:" + serverId);
                net.Dispose();
            }
        }

        #endregion

        private static void Register(ushort opcode, OnPacketReceive<ArcheAgeConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<ArcheAgeConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(ushort opcode, OnPacketReceive<GameConnection> e)
        {
            m_GHandlers[opcode] = new PacketHandler<GameConnection>(opcode, e);
            m_Maintained++;
        }
    }
}
