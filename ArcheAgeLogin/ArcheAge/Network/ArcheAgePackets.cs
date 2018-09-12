using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Structuring;
using LocalCommons.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Для посылки неразобранных пакетов
    /// </summary>
    public sealed class NP_Hex : NetPacket
    {
        public NP_Hex(string value) : base(0, 0x0)
        {
            ns.WriteHex(value);
        }
    }
    /// <summary>
    /// Sends Information About That Login Was right and we can continue =)
    /// </summary>
    public sealed class AcJoinResponse_0X00 : NetPacket
    {
        public AcJoinResponse_0X00(string clientVersion) : base(0x00, true)
        {
            switch (clientVersion)
            {
                case "1":
                    //0C00 0000 00 0003000000000000 00
                    ns.Write((byte)0x00);     //reason
                    ns.Write((long)0x0300);  //afs
                    ns.Write((byte)0x00);   //slotCount
                    break;
                case "3":
                    //v.3.0.3.0
                    /*
                [2]             S>c             0ms.            20:17:29 .900      05.07.18
                -------------------------------------------------------------------------------
                 TType: ArcheageServer: LS1     Parse: 6           EnCode: off         
                ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                000000 0D 00 00 00 01 00 00 06 | 03 48 00 00 00 00 00        .........H.....
                -------------------------------------------------------------------------------
                Archeage: "ACJoinResponse"                   size: 15     prot: 2  $002
                Addr:  Size:    Type:         Description:     Value:
                0000     2   word          psize             13         | $000D
                0002     2   word          ID                0          | $0000
                0004     2   WideStr[byte] reason            ""
                0006     8   int64         afs               1208157696 | $48030600
                000E     1   byte          slotCount         0          | $00
                
                0D00 0000 0100 0006034800000000 00
                */
                    ns.Write((short)0x01);        //reason
                    ns.Write((long)0x48030600);  //afs
                    ns.Write((byte)0x00);       //slotCount
                    break;
                default:
                    //4.5.5.1
                    /*
                [2]             S>c             0ms.            14:19:03 .472      23.07.18
                -------------------------------------------------------------------------------
                 TType: ArcheageServer: LS1     Parse: 6           EnCode: off         
                ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                000000 0D 00 00 00 00 00 00 04 | 02 1B 00 00 00 00 00        ...............
                -------------------------------------------------------------------------------
                Archeage: "ACJoinResponse"                   size: 15     prot: 2  $002
                Addr:  Size:    Type:         Description:     Value:
                0000     2   word          psize             13         | $000D
                0002     2   word          ID                0          | $0000
                0004     1   WideStr[byte] reason            
                0005     8   int64         afs               115997933568 | $1B02040000
                000D     1   byte          slotCount         0          | $00
                
                0D00 0000 0000 0004021B00000000 00
                */
                    ns.Write((short)0x00);        //reason
                    ns.Write((long)0x1B020400);  //afs
                    ns.Write((byte)0x00);       //slotCount
                    break;
            }
        }
    }
    /// <summary>
    /// Send the account ID back to the client
    /// </summary>
    public sealed class AcAuthResponse_0X03 : NetPacket
    {
        public AcAuthResponse_0X03(string clientVersion, ArcheAgeConnection net) : base(0x03, true)
        {
            switch (clientVersion)
            {
                case "1":
                    //2800 0300 58330000 2000 3236393631326537613630393431313862623735303764626334326261353934
                    ns.Write((int)net.CurrentAccount.AccountId); // записываем AccountID
                    //wsk - wide string key, в каждой сесии один и тот-же, даже при перелогине (выборе сервера)
                    string wsk = "A18D7A05E22E459BD1A819222B821030"; //для теста
                    ns.WriteASCIIFixed(wsk, wsk.Length);
                    break;
                /*
                [3]             S>c             0ms.            2:25:10 .845      23.06.18
                -------------------------------------------------------------------------------
                TType: ArcheageServer: undef   Parse: 6           EnCode: off         
                ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                000000 2D 00 03 00 1A C7 00 00 | 00 00 00 00 20 00 41 31     -....Ç...... .A1
                000010 38 44 37 41 30 35 45 32 | 32 45 34 35 39 42 44 31     8D7A05E22E459BD1
                000020 41 38 31 39 32 32 32 42 | 38 32 31 30 33 30 00        A819222B821030.
                -------------------------------------------------------------------------------
                Archeage: "ACAuthResponse"                   size: 47     prot: 2  $002
                            Addr:  Size:    Type:         Description:     Value:
                            0000     2   word          psize             45         | $002D
                0002     2   word          ID                3          | $0003
                0004     8   int64         accountId         50970      | $0000C71A
                000C    34   WideStr[byte] wsk               A18D7A05E22E459BD1A819222B821030  ($)
                002E     1   byte          slotCount         0          | $00
            
                2D00 0300 1AC7000000000000 2000 3346393243304532324430383344313843333233353433363932413442373630 00
                */
                //v.3.0.3.0
                case "3":
                    ns.Write((long)net.CurrentAccount.AccountId); // записываем AccountID
                                                                  //wsk - wide string key, в каждой сесии один и тот-же, даже при перелогине (выборе сервера)
                    wsk = "A18D7A05E22E459BD1A819222B821030"; //для теста
                    ns.WriteUTF8Fixed(wsk, wsk.Length);
                    //slotCount
                    ns.Write((byte)0x00);
                    break;
                default:
                    /*
                    [3]             S>c             0ms.            14:42:03 .045      23.07.18
                    -------------------------------------------------------------------------------
                    TType: ArcheageServer: LS1     Parse: 6           EnCode: off         
                    ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                    000000 2F 00 03 00 5A 43 05 00 | 00 00 00 00 20 00 63 34     /...ZC...... .c4
                    000010 30 39 39 32 30 36 63 38 | 66 32 34 38 63 35 39 65     099206c8f248c59e
                    000020 66 64 63 33 66 36 61 63 | 66 66 64 66 66 37 00 00     fdc3f6acffdff7..
                    000030 00                                                    .
                    -------------------------------------------------------------------------------
                    Archeage: "ACAuthResponse"                   size: 49     prot: 2  $002
                    Addr:  Size:    Type:         Description:     Value:
                    0000     2   word          psize             47         | $002F
                    0002     2   word          ID                3          | $0003
                    0004     8   int64         accountId         344922     | $0005435A
                    000C    34   WideStr[byte] wsk               c4099206c8f248c59efdc3f6acffdff7  ($)
                    002E     1   byte          slotCount         0          | $00
                    2F00 0300 5A43050000000000 2000 6334303939323036633866323438633539656664633366366163666664666637 0000 00
                     */
                    ns.Write((long)net.CurrentAccount.AccountId); // записываем AccountID
                    //wsk - wide string key, в каждой сесии один и тот-же, даже при перелогине (выборе сервера)
                    wsk = "c4099206c8f248c59efdc3f6acffdff7"; //для теста
                    ns.WriteUTF8Fixed(wsk, wsk.Length);
                    ns.Write((short)0x00);
                    //slotCount
                    ns.Write((byte)0x00);
                    break;
            }
        }
    }
    public sealed class AcAccountWarned_0X0D : NetPacket
    {
        public AcAccountWarned_0X0D(string clientVersion) : base(0x0D, true)
        {
            switch (clientVersion)
            {
                /*
                [6]             S>c             0ms.            2:25:15 .942      23.06.18
                -------------------------------------------------------------------------------
                 TType: ArcheageServer: undef   Parse: 6           EnCode: off         
                ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                000000 28 00 0D 00 01 23 00 59 | 6F 75 20 61 72 65 20 69     (....#.You are i
                000010 6E 20 58 32 20 41 75 74 | 68 20 53 65 72 76 65 72     n X2 Auth Server
                000020 2C 20 77 65 6C 63 6F 6D | 65 21                       , welcome!
                -------------------------------------------------------------------------------
                Archeage: "ACAccountWarned"                  size: 42     prot: 2  $002
                Addr:  Size:    Type:         Description:     Value:
                0000     2   word          psize             40         | $0028
                0002     2   word          ID                13         | $000D
                0004     1   byte          source            1          | $01
                0005    37   WideStr[byte] msg               You are in X2 Auth Server, welcome!  ($)
                */
                //v.3.0.3.0
                case "3":
                    ns.Write((byte)1); //source = 1;
                    const string msg = "You are in X2 Auth Server, welcome!"; //для теста
                    ns.WriteUTF8Fixed(msg, msg.Length);
                    break;
                default:
                    ns.WriteHex(
                        "012300596F752061726520696E2058322041757468205365727665722C2077656C636F6D6521"); //запись байтов без пробелов
                    break;
            }
        }
    }

    /// <summary>
    ///     Sends Request To Specified Game Server by Entered Information
    /// </summary>
    public sealed class AcWorldCookie_0X0A : NetPacket
    {
        public AcWorldCookie_0X0A(string clientVersion, GameServer server, int cookie) : base(0x0A, true)
        {
            switch (clientVersion)
            {
                case "1":
                    ns.Write((int)cookie);
                    string ipAddress = server.IPAddress; //Main address
                    ns.WriteUTF8Fixed(ipAddress, ipAddress.Length);
                    ns.Write(server.Port); //1239
                    break;
                //v.3.0.3.0
                case "3":
                    /*
                    [8]             S>c             0ms.            23:56:46 .052      10.03.18
                    -------------------------------------------------------------------------------
                     TType: ArcheageServer: undef   Parse: 6           EnCode: off         
                    ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                    000000 1E 00 0A 00 14 49 AB 07 | 7F 7E 20 B2 D7 04 00 00     .....I«.~ ІЧ...
                    000010 00 00 00 00 00 00 00 00 | 00 00 00 00 00 00 00 00     ................
                    -------------------------------------------------------------------------------
                    Archeage: "ACWorldCookie"                    size: 32     prot: 2  $002
                    Addr:  Size:    Type:         Description:     Value:
                    0000     2   word          psize             30         | $001E
                    0002     2   word          ID                10         | $000A
                    0004     4   integer       cookie            128665876  | $07AB4914
                    0008     1   byte          IP4               127        | $7F ''
                    0009     1   byte          IP3               126        | $7E '~'
                    000A     1   byte          IP2               32         | $20
                    000B     1   byte          IP1               178        | $B2 'І'
                    000C     2   word          port              1239       | $04D7
                    */
                    //v.3.0.3.0
                    var ipArray = server.IPAddress.Split('.');
                    if (ipArray.Length == 4)
                    {
                        //Write cookie
                        ns.Write((int)cookie);
                        //The main address
                        for (var i = 3; i > -1; i--)
                        {
                            ns.Write((byte)Convert.ToInt32(ipArray[i]));
                        }
                    }
                    else
                    {
                        //Write cookie
                        ns.Write(cookie);
                        //The main address
                        ns.Write((byte)0x01); //1
                        ns.Write((byte)0x00); //0
                        ns.Write((byte)0x00); //0
                        ns.Write((byte)0x7f); //127
                    }

                    ns.Write(server.Port); //1239
                    ns.Write((short)0x00);
                    ns.Write((int)0x00);
                    ns.Write((int)0x00);
                    ns.Write((int)0x00);
                    ns.Write((int)0x00);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Sends Information About Current Servers To Client.
    /// </summary>
    public sealed class AcWorldList_0X08 : NetPacket
    {
        /// <summary>
        /// Send server list
        /// </summary>
        public AcWorldList_0X08(string clientVersion, ArcheAgeConnection net) : base(0x08, true)
        {
            switch (clientVersion)
            {
                case "1":
                    var m_Current = GameServerController.CurrentGameServers.Values.ToList();
                    ns.Write((byte)m_Current.Count); // Count
                    foreach (var server in m_Current)
                    {
                        ns.Write((byte)server.Id);
                        ns.WriteUTF8Fixed(server.Name, Encoding.UTF8.GetByteCount(server.Name));
                        var online = server.IsOnline() ? (byte)0x01 : (byte)0x02; //1 Online 2 Offline
                        ns.Write((byte)online); //Server Status - 0x01 
                        switch (online)
                        {
                            case 0:
                                break;
                            default:
                                var status = server.CurrentAuthorized.Count >= server.MaxPlayers ? 0x01 : 0x00;
                                ns.Write((byte)status); //Server Status - 0x00 - normal / 0x01 - load / 0x02 - queue
                                //The following sections are the racial restrictions on server creation for this server selection interface 0 Normal 2 Prohibited
                                for (int i = 0; i < 9; i++)
                                {
                                    ns.Write((byte)0x00); //rcon
                                }
                                break;
                        }
                    }
                    int CharCount = CharacterHolder.LoadCharacterData(net.CurrentAccount.AccountId); //считываем данные персонажей и их количество
                    ns.Write((byte)CharCount); //CharCount

                    net.CurrentAccount.Characters = (byte)CharCount;

                    if (CharCount != 0)
                    {
                        long m_AccountId = net.CurrentAccount.AccountId; //считываем данные только наших персонажей
                        foreach (Character n_Current in CharacterHolder.CharactersList)
                        {
                            if (n_Current.AccountId == m_AccountId)
                            {
                                ns.Write((int)n_Current.AccountId); //AccountID
                                ns.Write((byte)n_Current.WorldId); //WorldID
                                ns.Write((int)n_Current.Type); //charID
                                string charname = n_Current.CharName;
                                ns.WriteASCIIFixed(charname, charname.Length);
                                ns.Write((byte)n_Current.CharRace); //CharRace
                                ns.Write((byte)n_Current.CharGender); //CharGender
                                string uid = n_Current.GUID; // = ""; //UID - Параметры чара, возможно пустая строка!
                                ns.WriteHex(uid, uid.Length);
                                ns.Write((long)n_Current.V); //v
                            }
                        }
                    }

                    AccountHolder.InsertOrUpdate(net.CurrentAccount);

                    break;
                case "3":
                    //4E000800  //пробная запись с одним чаром Remota - гномка
                    //ns.WriteHex("0101010009004172636865526167650100000000000000000000011AC70000000000000152770100060052656D6F7461030210001F3F1EE73B4D974BA9F5659BA68279570000000000000000");
                    //1D000800  //пробная запись - сервер ArcheRage, нет чаров, начало создания
                    ////ns.WriteHex("010101000900417263686552616765010000000000000000000000");
                    //v.3.0.3.0
                    //Посылаем список серверов, количество чаров на аккаунтах
                    m_Current = GameServerController.CurrentGameServers.Values.ToList();
                    //Write The number of servers
                    ns.Write((byte)m_Current.Count);
                    //Информация по серверу
                    foreach (var server in m_Current)
                    {
                        ns.Write((byte)server.Id);
                        ns.Write((byte)0x01); //надпись в списке серверов 00-нет надписи, 01- НОВЫЙ, 02-ОБЪЕДИНЕННЫЙ, 03-ОБЪЕДИНЕННЫЙ, 04-нет надписи
                        ns.Write((byte)0x02); //цвут надписи в списке серверов 00-синий, 01- зеленая, 02-фиолет, 03, 04, 08-красный, 0x10-
                        ns.WriteUTF8Fixed(server.Name, Encoding.UTF8.GetByteCount(server.Name));
                        //ns.WriteASCIIFixed(server.Name, server.Name.Length);
                        var online = server.IsOnline() ? (byte)0x01 : (byte)0x02; //1 Online 2 Offline
                        ns.Write((byte)online); //Server Status - 0x00 
                        var status = server.CurrentAuthorized.Count >= server.MaxPlayers ? 0x01 : 0x00;
                        ns.Write((byte)status); //Server Status - 0x00 - normal / 0x01 - load / 0x02 - queue
                        //The following sections are the racial restrictions on server creation for this server selection interface 0 Normal 2 Prohibited
                        ns.Write((byte)0x00); //unknown
                        ns.Write((byte)0x00); //Noah
                        ns.Write((byte)0x00);
                        ns.Write((byte)0x00); //Dwarf family
                        ns.Write((byte)0x00); //Elf
                        ns.Write((byte)0x00); //Haliland
                        ns.Write((byte)0x00); //Animal clan
                        ns.Write((byte)0x00);
                        ns.Write((byte)0x00); //War Mozu 
                    }
                    CharCount = CharacterHolder.LoadCharacterData(net.CurrentAccount.AccountId); //считываем данные персонажей и их количество
                    //Write The current user account number
                    ns.Write((byte)CharCount); //CharCount
                    if (CharCount != 0)
                    {
                        long m_AccountId = net.CurrentAccount.AccountId; //считываем данные только наших персонажей
                        foreach (Character n_Current in CharacterHolder.CharactersList)
                        {
                            if (n_Current.AccountId == m_AccountId)
                            {
                                ns.Write((long)n_Current.AccountId); //AccountID
                                ns.Write((byte)n_Current.WorldId); //WorldID
                                ns.Write((int)n_Current.Type); //type
                                string charname = n_Current.CharName;
                                ns.WriteASCIIFixed(charname, charname.Length);
                                ns.Write((byte)n_Current.CharRace); //Char Race - 01=нуиане, 03 = гномы
                                ns.Write((byte)n_Current.CharGender); //CharGender - 01-М, 02=Ж
                                string uid = n_Current.GUID; //UID - Параметры чара
                                ns.WriteHex(uid, uid.Length);
                                ns.Write((long)n_Current.V); //v
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Sends Request To Specified Game Server by Entered Information
    /// </summary>
    public sealed class NP_SendGameAuthorization_ : NetPacket
    {
        public NP_SendGameAuthorization_(GameServer server, int sessionId) : base(0x0A, true) //false
        {
            var ipArray = server.IPAddress.Split('.');

            if (ipArray.Length == 4)
            {
                //Write sessionid
                ns.Write(sessionId);
                //The main address
                for (var i = 3; i > -1; i--)
                {
                    ns.Write((byte)Convert.ToInt32(ipArray[i]));
                }
            }
            else
            {
                //sessionId
                ns.Write(sessionId);
                //The main address
                ns.Write((byte)0x01);
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);
                ns.Write((byte)0x7f);
            }

            //ns.Write((int)m_AccountId);
            //ns.WriteASCIIFixedNoSize(server.IPAddress, server.IPAddress.Length);
            ns.Write(server.Port);
            ns.Write((short)0x00);
            ns.Write(0x00);
            ns.Write(0x00);
            ns.Write(0x00);
            ns.Write(0x00);
        }
    }

    /// <summary>
    /// Sends Information About That Login Was right and we can continue =)
    /// </summary>
    public sealed class NP_AcceptLogin : NetPacket
    {
        public NP_AcceptLogin(string clientVersion) : base(0x00, true)
        {
            if (clientVersion == "3")
            {
                ns.Write((short)0x14);
                ns.Write((byte)0x00);
                ns.Write((byte)0xff);
                ns.Write((byte)0xff);
                ns.Write((short)0x1e);
                ns.Write((int)0x00);
            }
            else
            {
                ns.Write((short)0x00);
                ns.Write((short)0x306);
                ns.Write((short)0x48);
                ns.Write((int)0x0);
            }

        }
    }

    /// <summary>
    /// Sends Request To Specified Game Server by Entered Information
    /// </summary>
    public sealed class NP_SendGameAuthorization : NetPacket
    {
        public NP_SendGameAuthorization(GameServer server, int sessionId) : base(0x0A, true)
        {
            string[] ipArray = server.IPAddress.Split('.');

            if (ipArray.Length == 4)
            {
                //Write sessionid
                //ns.Write(sessionId);
                ns.Write((byte)0x5b);//unknown
                ns.Write((byte)0x4f);//unknown
                ns.Write((byte)0xdd);//unknown
                ns.Write((byte)0x4e);//unknown

                for (int i = 3; i > -1; i--)
                {
                    var cd = Convert.ToInt32(ipArray[i].ToString());
                    ns.Write((byte)Convert.ToInt32(ipArray[i].ToString()));
                }
            }
            else
            {
                //sessionId
                ns.Write(sessionId);
                //Main address
                ns.Write((byte)0x01);
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);
                ns.Write((byte)0x7f);
            }
            //ns.Write((int)m_AccountId);
            //ns.WriteASCIIFixedNoSize(server.IPAddress, server.IPAddress.Length);
            ns.Write((short)server.Port);
            ns.Write((short)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
        }
    }

    /// <summary>
    /// Sends Information About Current Servers To Client.
    /// About server information sent to the client
    /// </summary>
    public sealed class NP_ServerList : NetPacket
    {
        /// <summary>
        /// Send server list
        /// </summary>
        //не использую
        public NP_ServerList(string clientVersion) : base(0x08, true)
        {
            List<GameServer> m_Current = GameServerController.CurrentGameServers.Values.ToList<GameServer>();
            //Write server number
            //Write The number of servers
            ns.Write((byte)m_Current.Count);
            foreach (GameServer server in m_Current)
            {
                ns.Write(server.Id);
                if (clientVersion == "3")
                {
                    ns.Write((short)0x00);
                }

                ns.WriteUTF8Fixed(server.Name, System.Text.UTF8Encoding.UTF8.GetByteCount(server.Name));
                //ns.WriteASCIIFixed(server.Name, server.Name.Length);
                var online = server.IsOnline() ? (byte)0x01 : (byte)0x02; //1 Online 2 Offline
                ns.Write(online); //Server Status - 0x00 
                var status = server.CurrentAuthorized.Count >= server.MaxPlayers ? 0x01 : 0x00;
                ns.Write((byte)status); //Server Status - 0x00 - normal / 0x01 - load / 0x02 - queue
                ns.Write((byte)0x00); //unknown
                                      //The following sections are the racial restrictions on server creation for this server selection interface 0 Normal 2 Prohibited
                ns.Write((byte)0x00); //Noah
                ns.Write((byte)0x00);
                ns.Write((byte)0x00); //Dwarf family
                ns.Write((byte)0x00); //Elf
                ns.Write((byte)0x00); //Haliland
                ns.Write((byte)0x00); //Animal clan
                ns.Write((byte)0x00);
                ns.Write((byte)0x00); //War Mozu            
            }
            //Write The current user account number
            ns.Write((byte)0x00);
            /*
            ns.Write((byte)0x01); //Last Server Id?
            ns.Write((short)0x288C); //Current Users???
            ns.Write((short)0x22); //Undefined
            ns.Write((short)0x174); //Undefined
            ns.Write((short)0x3DEF); //Undefined
            ns.Write((byte)0x00); //Undefined

            //String? CharName? Probably Last Character.
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);

            //Undefined
            ns.Write((byte)0x01);
            ns.Write((byte)0x02);

            //String? 
            //Undefined //Ten Char String Undefined
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);
            ns.Write((int)0x00); //undefined
            ns.Write((int)0x00); //undefined
            */
        }
    }

    /// <summary>
    /// Sends Information about that Password Were Correct
    ///
    ///send sessionId
    /// If there are no errors words
    /// </summary>
    public sealed class NP_PasswordCorrect : NetPacket
    {
        public NP_PasswordCorrect(int sessionId) : base(0x03, true)
        {
            //Original
            ////ns.Write((int)sessionId);
            //ns.Write((short)0x755c);
            //ns.Write((byte)0x1a);
            //ns.Write((int)0x00);
            //ns.Write((int)0x00);
            ////string encrypted = "f3d02d5dda564e7bb4320de5b27f5c78";
            ////ns.WriteASCIIFixed("\u", '\u'.Length);

            //China version (do not know the role, but will be used later, may be the key and the like) Tested not sessionID, this value more accounts and more servers fixed (China)
            //ns.Write((short)0xeb63);
            ns.Write((byte)0xeb);
            ns.Write((byte)0x63);
            ns.Write((byte)0x4a);
            ns.Write((byte)0x1d);
            ns.Write((byte)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
        }
    }
    //Send account ID back to the client
    public sealed class NP_03key : NetPacket
    {
        public NP_03key(string clientVersion) : base(0x03, true)
        {
            //Account ID

            ns.Write((byte)0x1);
            ns.Write((byte)0x0);
            ns.Write((byte)0x0);
            ns.Write((byte)0x0);
            ns.Write((int)0x00);
            if (clientVersion == "3")
            {
                ns.Write((int)0x00);
            }
            else
            {
                ns.WriteUTF8Fixed("e10adc3949ba59abbe56e057f20f883e", "e10adc3949ba59abbe56e057f20f883e".Length);
            }
            ns.Write((byte)0x00);
        }
    }

    /// <summary>
    /// Sends Information About Rijndael(AES) Key
    /// Sending Information Encryption (AES) Pipes
    /// </summary>
    public sealed class NP_AESKey : NetPacket
    {
        public NP_AESKey() : base(0x04, true)
        {
            //Rijndael / SHA256
            ns.Write((int)5000); //round 5000
                                 //le - string
            ns.WriteASCIIFixed("xnDekI2enmWuAvwL", 16); //initVec
            byte[] b = new byte[32];
            ns.Write(b, 0, b.Length);
        }
    }

    /// <summary>
    /// Sends Message Box About That Error Occured While Logging In.
    /// Send message box about landing errors
    /// </summary>
    public sealed class NP_FailLogin : NetPacket
    {
        public NP_FailLogin() : base(0x0C, true)
        {
            ns.Write((byte)0x02);  //Reason
            ns.Write((short)0x00); //Undefined
            ns.Write((short)0x00); //Undefined
        }
    }

    /// <summary>
    /// Sends Message Box About That Error Occured While Logging In.
    /// Send message box about landing errors
    /// </summary>
    public sealed class NP_ACLoginDenied_0x0C : NetPacket
    {
        public NP_ACLoginDenied_0x0C() : base(0x0C, true)
        {
            /*
             [2]             S>c             0ms.            14:40:40 .831      21.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: LS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             000000 07 00 0C 00 02 00 00 00 | 00                          .........
             -------------------------------------------------------------------------------
             Archeage: "ACLoginDenied"                    size: 9      prot: 2  $002
             Addr:  Size:    Type:         Description:     Value:
             0000     2   word          psize             7          | $0007
             0002     2   word          ID                12         | $000C
             0004     2   byte          reason            2          | $0002 
             0004     2   word          ___               0          | $0000 
             0004     2   word          ___               0          | $0000 
             */
            //v.3.0.3.0
            ns.Write((byte)0x02);  //Reason "нет такого пользователя"
            ns.Write((short)0x00); //Undefined
            ns.Write((short)0x00); //Undefined
        }
    }

    /// <summary>
    /// Prompt when the account is blocked
    /// </summary>
    public sealed class NP_BanLogin : NetPacket
    {
        public NP_BanLogin() : base(0x0C, true)
        {
            ns.Write((byte)0x06); // Reason
            ns.Write((int)0x00);//Undefined
                                //ns.Write((short)0x00);//Undefined
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NP_EditMessage : NetPacket
    {
        public NP_EditMessage() : base(0x0C, true)
        {
            ns.Write((short)0x0d); // Reason
        }
    }

    public sealed class NP_EditMessage2 : NetPacket
    {
        public NP_EditMessage2(string Message) : base(0x0C, true)
        {
            ns.Write((short)0x054c);
            ns.Write((short)0x00);
            ns.Write((int)0x00);
            ns.WriteASCIIFixed(Message, Message.Length);
            //ns.Write((byte)0x00);

        }
    }

    /// <summary>
    /// Repeat login
    /// </summary>
    public sealed class NP_DuplicateLogin : NetPacket
    {
        public NP_DuplicateLogin() : base(0x07, true)
        {
            ns.Write((short)0x0c); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x03);//Undefined
        }

    }
}
