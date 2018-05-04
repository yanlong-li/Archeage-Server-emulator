using LocalCommons.Native.Network;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheAgeLogin.ArcheAge.Structuring;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    ///     Sends Information About That Login Was right and we can continue =)
    /// </summary>
    public sealed class AcJoinResponse_0X00 : NetPacket
    {
        public AcJoinResponse_0X00(string clientVersion) : base(0x00, true)
        {
            if (clientVersion == "1")
            {
                //v.3.0+
                /*
                [2]             S>c             0ms.            23:55:44 .150      10.03.18
                -------------------------------------------------------------------------------
                 TType: ArcheageServer: undef   Parse: 6           EnCode: off         
                ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                000000 0D 00 00 00 01 00 00 06 | 03 48 00 00 00 00 00        .........H.....
                -------------------------------------------------------------------------------
                Archeage: "ACJoinResponse"                   size: 15     prot: 2  $002
                Addr:  Size:    Type:         Description:     Value:
                0000     2   word          psize             13         | $000D
                0002     2   word          ID                0          | $0000
                0004     2   WideStr[byte] reason            ""
                0006     8   int64         afs               1208157696 | $48030600                */
                ns.WriteHex("0100000603480000000000"); //запись байтов без пробелов
            }
            else
            {
                ns.Write((short)0x01);
                ns.Write((byte)0x00);
                ns.Write((short)0x0402);
                ns.Write((short)0x14);
                ns.Write(0x00);
            }
        }
    }
    //Send the account ID back to the client
    public sealed class AcAuthResponse_0X03 : NetPacket
    {
        public AcAuthResponse_0X03(string clientVersion, ArcheAgeConnection net) : base(0x03, true)
        {
            /*
            [3]             S>c             0ms.            23:55:44 .235      10.03.18
            -------------------------------------------------------------------------------
             TType: ArcheageServer: undef   Parse: 6           EnCode: off         
            ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
            000000 2D 00 03 00 1A C7 00 00 | 00 00 00 00 20 00 34 42     -....Ç...... .4B
            000010 33 36 38 46 36 33 41 41 | 32 39 45 38 38 34 46 45     368F63AA29E884FE
            000020 39 43 37 36 31 37 36 41 | 36 41 32 36 32 41 00        9C76176A6A262A.
            -------------------------------------------------------------------------------
            Archeage: "ACAuthResponse"                   size: 47     prot: 2  $002
            Addr:  Size:    Type:         Description:     Value:
            0000     2   word          psize             45         | $002D
            0002     2   word          ID                3          | $0003
            0004     4   integer       accountId         50970      | $0000C71A
            0008     4   integer       __                0          | $00000000
            000C    34   WideStr[byte] wsk               4B368F63AA29E884FE9C76176A6A262A  ($)
            002E     1   byte          __                0          | $00
            */
            //Account ID
            //ns.Write(0xC71A);
            if (clientVersion == "1")
            {
                ns.Write(net.CurrentAccount.AccountId); // записываем AccountID
                //4 байта 00
                ns.Write(0x00);
                //size key
                //ns.Write((ushort)0x20);
                //key
                const string message = "4B368F63AA29E884FE9C76176A6A262A";
                ns.WriteUTF8Fixed(message, message.Length);
                //unk byte
                ns.Write((byte) 0x00);
            }
            else
                ns.WriteHex("1AC70000000000002000344233363846363341413239453838344645394337363137364136413236324100");
        }   
    }
    public sealed class AcAccountWarned_0X0D : NetPacket
    {
        public AcAccountWarned_0X0D(string clientVersion) : base(0x0D, true)
        {
            /*
            [6]             S>c             0ms.            23:55:49 .333      10.03.18
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
            0004     1   byte          __                1          | $01
            0005    37   WideStr[byte] Message           You are in X2 Auth Server, welcome!  ($)
            */
            if (clientVersion == "1")
                ns.WriteHex(
                    "012300596F752061726520696E2058322041757468205365727665722C2077656C636F6D6521"); //запись байтов без пробелов
            else
                ns.WriteHex(
                    "012300596F752061726520696E2058322041757468205365727665722C2077656C636F6D6521"); //запись байтов без пробелов
        }
    }

    /// <summary>
    ///     Sends Request To Specified Game Server by Entered Information
    /// </summary>
    public sealed class AcWorldCookie_0X0A : NetPacket
    {
        public AcWorldCookie_0X0A(GameServer server, int cookie) : base(0x0A, true)
        {
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
            //надо слать
            //1E000A00 1449AB07 7F7E20B2 D704000000000000000000000000000000000000
            //отсылаем
            //1E000A00 1449AB07 0100007F 04D7000000000000000000000000000000000000
            //1E000A00 1449AB07 0100007F D704000000000000000000000000000000000000
            var ipArray = server.IPAddress.Split('.');
            if (ipArray.Length == 4)
            {
                //Write cookie
                ns.Write(cookie);
                //The main address
                for (var i = 3; i > -1; i--) ns.Write((byte)Convert.ToInt32(ipArray[i]));
            }
            else
            {
                //Write cookie
                ns.Write(cookie);
                //The main address
                ns.Write((byte)0x01);
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);
                ns.Write((byte)0x7f);
            }

            ns.Write(server.Port);
            ns.Write((short)0x00);
            ns.Write(0x00);
            ns.Write(0x00);
            ns.Write(0x00);
            ns.Write(0x00);
        }
    }

    /// <summary>
    ///Sends Information About Current Servers To Client.
    ///About server information sent to Client
    /// </summary>
    public sealed class AcWorldList_0X08 : NetPacket
    {
        /// <summary>
        ///     Send server list
        /// </summary>
        public AcWorldList_0X08(string clientVersion) : base(0x08, true)
        {
            /*
            [5]             S>c             0ms.            23:55:44 .336      10.03.18
            -------------------------------------------------------------------------------
             TType: ArcheageServer: undef   Parse: 6           EnCode: off         
            ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
            000000 1D 00 08 00 01 01 01 00 | 09 00 41 72 63 68 65 52     ..........ArcheR
            000010 61 67 65 01 00 00 00 00 | 00 00 00 00 00 00 00        age............
            -------------------------------------------------------------------------------
            Archeage: "ACWorldList"                      size: 31     prot: 2  $002
            Addr:  Size:    Type:         Description:     Value:
            0000     2   word          psize             29         | $001D
            0002     2   word          ID                8          | $0008
            0004     1   byte          count1            1          | $01
            0005     1   byte          id                1          | $01
            0006     2   word          __                1          | $0001
            0008    11   WideStr[byte] ServerName        ArcheRage  ($)
            0013     4   integer       __                1          | $00000001
            0017     4   integer       __                0          | $00000000
            001B     2   word          __                0          | $0000
            001D     1   byte          a                 0          | $00
            001E     1   byte          chCount           0          | $00
             */

            //4E000800  //пробная запись с одним чаром Remota - гномка
            //ns.WriteHex("0101010009004172636865526167650100000000000000000000011AC70000000000000152770100060052656D6F7461030210001F3F1EE73B4D974BA9F5659BA68279570000000000000000");
            //1D000800  //пробная запись - сервер ArcheRage, нет чаров, начало создания
            ////ns.WriteHex("010101000900417263686552616765010000000000000000000000");
            //Посылаем список серверов, количество чаров на аккаунтах 0
            var mCurrent = GameServerController.CurrentGameServers.Values.ToList();
            //Write The number of servers
            ns.Write((byte)mCurrent.Count);
            foreach (var server in mCurrent)
            {
                ns.Write(server.Id);
                if (clientVersion == "1")
                    ns.Write((short)0x00);
                ns.WriteUTF8Fixed(server.Name, Encoding.UTF8.GetByteCount(server.Name));
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
            //Количество чаров на аккаунте = 0
            ns.Write((byte)0x00);
            
        }
    }

    public sealed class NpSendGameAuthorization : NetPacket
    {
        public NpSendGameAuthorization(GameServer server, int sessionId) : base(0x0A, true) //false
        {
            var ipArray = server.IPAddress.Split('.');

            if (ipArray.Length == 4)
            {
                //Write sessionid
                ns.Write(sessionId);
                //The main address
                for (var i = 3; i > -1; i--)
                    //var cd = Convert.ToInt32(ipArray[i]);
                    ns.Write((byte)Convert.ToInt32(ipArray[i]));
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
            if (clientVersion == "1")
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
        public NP_ServerList(string clientVersion) : base(0x08, true)
        {
            List<GameServer> m_Current = GameServerController.CurrentGameServers.Values.ToList<GameServer>();
            //Write server number
            //Write The number of servers
            ns.Write((byte)m_Current.Count);
            foreach (GameServer server in m_Current)
            {
                ns.Write(server.Id);
                 if (clientVersion == "1")
                     ns.Write((short)0x00);
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
            if (clientVersion == "1")
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
            ns.Write((int)5000); //Undefined? 5000
            //le - string
            ns.WriteASCIIFixed("xnDekI2enmWuAvwL", 16); //Always 16?
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
            ns.Write((byte)0x02); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x00);//Undefined
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

    public sealed class NP_EditMessage2:NetPacket
    {
        public NP_EditMessage2(string Message):base (0x0C, true)
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
    public sealed class NP_DuplicateLogin:NetPacket
    {
        public NP_DuplicateLogin() : base(0x07, true)
        {
            ns.Write((short)0x0c); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x03);//Undefined
        }

    }
}
