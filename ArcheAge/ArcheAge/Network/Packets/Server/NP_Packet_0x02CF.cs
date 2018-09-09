using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x02CF : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x02CF() : base(05, 0x02CF)
        {
            //пакеты для входа в Лобби
            /*
             * [19]            S>c             0ms.            18:52:06 .536      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$25/37)
             000000 23 00 DD 05 00 E8 2E 82 | 52 23 F4 C4 94 64 34 05     #.Ý..è.‚R#ôÄ”d4.
             000010 D5 A5 75 45 16 E6 B6 86 | 57 27 F7 C7 97 70 40 10     Õ¥uE.æ¶†W'÷Ç—p@.
             000020 E0 B0 81 51 42                                        à°QB
             */
            //ns.WriteHex(
            //"2300DD0500E82E825223F4C494643405D5A5754516E6B6865727F7C797704010E0B0815142");
            //"2300DD05120B9D01" +
            //    "0100000000000000000000000000000000000000000000000000000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"2300 DD05 12  0B  9D01   01 0000000000000000 00000000 00000000 00000000 00000000 00000000"
            //3.0.3.0
            //"2300 DD05 D7  0B  CF02   0100000000000000000000000000000000000000000000000000000000"

            //protectFaction 1
            //01
            ns.Write((byte)0x01);
            //time 8
            //0000000000000000
            ns.Write((long)0x00);
            //year 4
            //00000000
            ns.Write((int)0x00);
            //month 4
            //00000000
            ns.Write((int)0x00);
            //day 4
            //00000000
            ns.Write((int)0x00);
            //hour 4
            //00000000
            ns.Write((int)0x00);
            //min 4
            //00000000
            ns.Write((int)0x00);
        }
    }
}
