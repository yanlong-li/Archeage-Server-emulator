using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x00EC : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x00EC() : base(05, 0x00EC)
        {
            //пакеты для входа в Лобби
            /*
             * [12]            S>c             0ms.            18:52:06 .509      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$2C/44)
             000000 2A 00 DD 05 89 65 6B 03 | D2 A2 72 42 12 E3 B3 83     *.Ý.‰ek.Ò¢rB.ã³ƒ
             000010 53 23 F4 C4 94 64 34 05 | B5 F1 75 45 16 E6 B6 86     S#ôÄ”d4.µñuE.æ¶†
             000020 A8 D8 08 38 68 8F BF EF | E0 B0 81 51                 ¨Ø.8h¿ïà°Q         
             */
            //ns.WriteHex(
            //"2A00DD05B9656B03D2A2724212E3B3835323F4C494643405B5F1754516E6B6865727F7C797704010E0B08151");
            //"2A00DD0528045A02" +
            //    "000000000000000000000000000000006054000000000000000000000000000000000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"2A00 DD05 28  04  5A02   00000000 00000000 0000000000000000 6054000000000000 0000000000000000 00000000"
            //3.0.3.0
            //"2A00 DD05 3C  04  EC00   000000000000000000000000000000006054000000000000000000000000000000000000"

            //payMethod 4
            //00000000
            ns.Write((int)0x00);
            //payLocation 4
            //00000000
            ns.Write((int)0x00);
            //payStart 8
            //0000000000000000
            ns.Write((long)0x00);
            //payEnd 8
            //6054000000000000
            ns.Write((long)0x5460);
            //realPayTime 8
            //0000000000000000
            ns.Write((long)0x00);
            //buyPremiumCount 4
            //00000000
            ns.Write((int)0x00);
        }
    }
    /// <summary>
    /// пакет для входа в Лобби
    /// author: NLObP
    /// </summary>
    public sealed class NP_Packet_0x00EC_2 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x00EC_2() : base(05, 0x00EC)
        {
            //пакеты для входа в Лобби
            /*
             * [34]            S>c             0ms.            18:52:07 .747      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  A  D  E  F    -------------------
             пакет не определен  (size=$2C/44)
             000000 2A 00 DD 05 EA 6F 6B 03 | D3 A2 72 42 13 E3 B3 83     *.Ý.êok.Ó¢rB.ã³ƒ
             000010 53 23 F4 C4 94 64 34 05 | 3B 83 3B 28 16 E6 B6 86     S#ôÄ”d4.;ƒ;(.æ¶†
             000020 57 27 F7 C7 97 70 40 10 | E0 B0 81 51                 W'÷Ç—p@.à°Q
             */
            //ns.WriteHex(
            //"2A00DD05EA6F6B03D3A2724213E3B3835323F4C4946434053B833B2816E6B6865727F7C797704010E0B08151");
            //"2A00DD057B0E5A02" +
            //    "01000000010000000000000000000000EE264E6D00000000000000000000000000000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"2A00 DD05 7B  0E  5A02   01000000 01000000 0000000000000000 EE264E6D00000000 0000000000000000 00000000"
            //3.0.3.0
            //"2A00 DD05 8F  0E  EC00   01000000010000000000000000000000EE264E6D00000000000000000000000000000000"

            //payMethod 4
            //01000000
            ns.Write((int)0x01);
            //payLocation 4
            //01000000
            ns.Write((int)0x01);
            //payStart 8
            //0000000000000000
            ns.Write((long)0x00);
            //payEnd 8
            //EE264E6D00000000
            ns.Write((long)0x6D4E26EE);
            //realPayTime 8
            //0000000000000000
            ns.Write((long)0x00);
            //buyPremiumCount 4
            //00000000
            ns.Write((int)0x00);
        }
    }

}
