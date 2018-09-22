using LocalCommons.Network;
using System;
using LocalCommons.Cookie;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_X2EnterWorldResponsePacket_0x0000 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        /// <param name="clientversion"></param>
        public NP_X2EnterWorldResponsePacket_0x0000(string clientversion) : base(05, 0x00)
        {
            switch (clientversion)
            {
                case "1":
                    //сделал отдельный класс, так как базовый код = base(01, 0x00) для версии 1.0, а не base(05, 0x00) как для версии 3.0
                    break;
                case "3":
                    /*
                    [2]             S>c             0ms.            13:15:07 .431      26.03.14
                    -------------------------------------------------------------------------------
                     TType: undef   Server: undef   Parse: off (auto)  EnCode: undef (auto)
                    ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
                    000000 15 00 DD 01 00 00 00 00 | 00 C1 21 29 AD E2 04 18     ..Ý......Á!)­â..
                    000010 9A 32 53 00 00 00 00                                  .
                    -------------------------------------------------------------------------------
                    Archeage: "X2EnterWorldResponse"             size: 33     prot: undefined
                    Addr:  Size:    Type:         Description:     Value:
                    0000     2   word          psize             21         | $0015
                    0002     2   word          type              477        | $01DD
                    0004     2   word          ID                0          | $0000
                    0006     2   word          reason            0          | $0000
                    0008     1   byte          gm                0          | $00
                    0009     4   integer       sc                -1389813311 | $AD2921C1
                    000D     2   word          sp                1250       | $04E2
                    000F     8   int64         wf                1395825176 | $53329A18
                    0017     4   integer       tz                48037896   | $02DD0008
                    001B     2   word          pubKeySize        0          | $0000
                    001D     2   WideStr[byte] pubKey              ($)
                    001F     4   integer??     natAddr           0          | $00000000
                             2   word          natPort                 
                    */
                    //3.0.0.7
                    //0x01 0x0000_X2EnterWorldResponse
                    //15.06.2018 13:40[INFO] - Encode: 2901DD0557764616E6B68711130B5372647DBC82FA714111E2FE7DADDDF7C297623300D4A4DBBF625BA130A5FE71C51FC8039AFD6C1DFDC7B2A1E5404F208474380E5FC539F87AD9F3073599ECA6120631B4044394F87790044692A0DCC2DE37389392AF1371C36989F5B9619EDE92E442D0B31461361784A1A1FD68FF1DD384437631C44680319E88389847A5730BAAE8503B68AFFD75534831716C55B22AC026A67ACDFF96663707D7A7774010E0B0815121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704110E1C7BEDAD0C8FC
                    //15.06.2018 13:40[INFO] - Decode: 2901 DD05 F2 00 0000 0000 00 4634FC94 E204 4D42535B00000000 4CFFFFFF 0401 040100040000AFFB77BE14B5F0D8870389AE349D2ACB6AADE7426175217E2155D54A4C4D278B7B29FA00C3A1FDD8A2C7A344F111A5227E21B6F38105C7EB3C0E9748D3834EA2F0924B7B372B03ADDD41473194A7F0D5B242A15464680EC91B052334312623861051AE76E93936E462E9186A02199B6C6E16604CE5D51811A7CF75A3F35C3B390000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010001773F8B05CDC9
                    //расшифрованные данные из снифа пакета
                    //3.0.3.0
                    // size hash crc idx opcode data
                    //"2901 DD05 F2  00  0000   0000 00 4634FC94 E204 4D42535B00000000 4CFFFFFF 0401 040100040000AFFB77BE14B5F0D8870389AE349D2ACB6AADE7426175217E2155D54A4C4D278B7B29FA00C3A1FDD8A2C7A344F111A5227E21B6F38105C7EB3C0E9748D3834EA2F0924B7B372B03ADDD41473194A7F0D5B242A15464680EC91B052334312623861051AE76E93936E462E9186A02199B6C6E16604CE5D51811A7CF75A3F35C3B390000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010001 773F8B05 CDC9"
                    //08.08.2018 0:30 [INFO] - Decode: 2901DD05 3C00 0000 0000 00 389416E0 E204 91E6305B00000000 4CFFFFFF 04010 4010 0040000BBC0E9659E21640C4D689287322A627C63B8FD9EEDAF0C3999D14079393F023B1D6B032D574F2F787C814D90D137DAFD93E5577EDE35E1696A40B0DC031FB1D333E038A15163D278615FEFB9275D9FBD5B99E77F6890D8DA04F226267FCDC487E1A1DCAEB23A13399699B3617BF59C9DF85A81519C5093D61C5F44B8045FEEE9 0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010001FC73AE5522ED
                    //08.08.2018 0:30 [INFO] - Decode: 2901DD05 7200 0000 0000 00 954CDF8A E204 A7E6305B00000000 4CFFFFFF 04010 4010 0040000BBC0E9659E21640C4D689287322A627C63B8FD9EEDAF0C3999D14079393F023B1D6B032D574F2F787C814D90D137DAFD93E5577EDE35E1696A40B0DC031FB1D333E038A15163D278615FEFB9275D9FBD5B99E77F6890D8DA04F226267FCDC487E1A1DCAEB23A13399699B3617BF59C9DF85A81519C5093D61C5F44B8045FEEE9 0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010001FC73AE553AED
                    ns.Write((short) 0x00); //reason
                    ns.Write((byte) 0x00); //gm

                    // генерируем cookie
                    int cookie = Cookie.Generate();
                    ns.Write((uint)cookie);     //sc        cookie

                    ns.Write((short) 0x04E2); //sp 1250
                    ns.Write((long) 0x7F000001); //wf - пробую
                    ns.Write((uint) 0xFFFFFF4C); //tz
                    ns.Write((short) 0x0104); //pubKeySize
                    const string pubKey =
                        "00040000BBC0E9659E21640C4D689287322A627C63B8FD9EEDAF0C3999D14079393F023B1D6B032D574F2F787C814D90D137DAFD93E5577EDE35E1696A40B0DC031FB1D333E038A15163D278615FEFB9275D9FBD5B99E77F6890D8DA04F226267FCDC487E1A1DCAEB23A13399699B3617BF59C9DF85A81519C5093D61C5F44B8045FEEE90000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010001";
                    ns.WriteHex(pubKey, pubKey.Length); //pubKey 260 байт
                    ns.Write((int)0x7F000001); //natAddr 0x55AE73FC     773F8B05 CDC9 FC73AE55 22ED
                    ns.Write((ushort) 0xED22); //natPort                FC73AE55 3AED
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Для версии 1.0.1406
    /// </summary>
    public sealed class NP_X2EnterWorldResponsePacket01_0x0000 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_X2EnterWorldResponsePacket01_0x0000() : base(01, 0x00)
        {
            //          opcod reason gm sc       sp   wf
            //1500 DD01 0000  0000   00 4CA23F2B E204 79422F5300000000        IP 83 47 66 121
            //1500 DD01 0000  0000   00 FA318BCF E204 1795325300000000        IP 83 50 149 23
            ns.Write((short)0x0000); //reason
            ns.Write((byte)0x00);    //gm

            // генерируем cookie
            var cookie = Cookie.Generate();
            ns.Write((uint)cookie);     //sc        cookie

            ns.Write((ushort)0x04E2);   //sp        port  1250
            ns.Write((long)0x7F000001); //wf        IP    7F 00 00 01
        }
    }
}
