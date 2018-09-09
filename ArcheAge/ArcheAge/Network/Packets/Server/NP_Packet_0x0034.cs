using LocalCommons.Network;
using LocalCommons.Utilities;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x0034 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
       public NP_Packet_0x0034() : base(05, 0x0034)
        {
            //пакеты для входа в Лобби
            /*
             * [10]            S>c             0ms.            18:52:06 .505      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$52/82)
             000000 50 00 DD 05 7F 20 F4 C2 | 82 62 52 71 B0 CB 11 25     P.Ý..ôÂ‚bRq°Ë.%
             000010 73 81 D3 E4 38 40 DB A6 | F9 0B 2C 06 A9 90 43 48     sÓä8@Û¦ù.,.©CH
             000020 6E EF CD 4B 67 45 F3 1F | 35 E7 09 01 D0 44 1D 85     nïÍKgEó.5ç..ÐD.…
             000030 AB 78 EE 82 53 22 F4 C4 | 94 64 34 05 D5 A5 75 45     «xî‚S"ôÄ”d4.Õ¥uE
             000040 17 E7 B7 87 56 27 F7 C7 | 97 70 40 10 E0 B0 11 50     .ç·‡V'÷Ç—p@.à°.P
             000050 81 B0         
             */
            //ns.WriteHex(
            //"5000DD057F20F4C282625271B0CB11257381D3E43840DBA6F90B2C06A99043486EEFCD4B6745F31F35E70901D0441D85AB78EE825322F4C494643405D5A5754517E7B7875627F7C797704010E0B0115081B0");
            //"5000DD05EE020600" +
            //    "1000617263686561676567616D652E636F6D1A007F37340F79087DCB376503DEA486380002E66F87B99B5D01000100000000000000000000010101010100000000000000000090010001");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"5000 DD05 2E  02  0600   1000 617263686561676567616D652E636F6D 1A00 7F37340F79087DCB376503DEA486380002E66FC7B99B5D010001 00000000 00000000 00 00 01 01 01 01 01 00 00000000 00 00 00 00 9001 00 01"
            //3.0.3.0
            //"5000 DD05 0E  02  3400   1000 617263686561676567616D652E636F6D 1A00 7F37340F79087DCB376503DEA486380002E66FC7BB9B5D010001 00000000 00000000 00 00 01 01 01 01 01 00 00000000 00 00 00 00 9001 00 01"

            //size.host
            //1000 617263686561676567616D65 2E 636F6D
            // 10h archeagegame.com, 10h-забит в коде
            const string host = "archeagegame.com";
            ns.WriteUTF8Fixed(host, host.Length);
            //size.fset
            //1A00 7F37340F79087DCB376503DEA486380002E66FC7B99B5D010001
            const string fset = "7F37340F79087DCB376503DEA486380002E66FC7BB9B5D010001";
            ns.WriteHex(fset, fset.Length);
            //count 4
            //00000000
            ns.Write((int)0x00);
            //{ 0-забит в коде
            //code 1, максимум 100 (64h) повторений - в нашем случае пропускаем эту секцию
            //}
            //initLp 4
            //00000000
            ns.Write((int)0x00);
            //canPlaceHouse 1
            //00
            ns.Write((byte)0x00);
            //canPayTax 1
            //00
            ns.Write((byte)0x00);
            //canUseAuction 1
            //01
            ns.Write((byte)0x01);
            //canTrade 1
            //01
            ns.Write((byte)0x01);
            //canSendMail 1
            //01
            ns.Write((byte)0x01);
            //canUseBank 1
            //01
            ns.Write((byte)0x01);
            //canUseCopper 1
            //01
            ns.Write((byte)0x01);
            //secondPasswordMaxFailCount 1
            //00
            ns.Write((byte)0x00);
            //idleKickTime 4
            //00000000
            ns.Write((int)0x00);
            //enable 1
            //00
            ns.Write((byte)0x00);
            //pcbang 1
            //00
            ns.Write((byte)0x00);
            //premium 1
            //00
            ns.Write((byte)0x01);
            //maxCh 1
            //00
            ns.Write((byte)0x00);
            //honorPointDuringWarPercent 2
            //9001
            ns.Write((short)0x0190);
            //uccVer 1
            //00
            ns.Write((short)0x00);
            //memberType 1
            //01
            ns.Write((byte)0x01);
            
        }
    }
}
