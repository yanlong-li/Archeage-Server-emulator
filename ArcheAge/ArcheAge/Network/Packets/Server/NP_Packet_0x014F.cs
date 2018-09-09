using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x014F : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x014F() : base(05, 0x014F)
        {
            //пакеты для входа в Лобби
            /*
             * [51]            S>c             0ms.            18:52:08 .938      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$26/38)
             000000 24 00 DD 05 64 F1 1F 82 | 52 23 F4 C4 95 64 34 05     $.Ý.dñ.‚R#ôÄ•d4.
             000010 D5 5A 75 45 16 E6 34 B7 | D4 7D F7 C7 97 70 40 10     ÕZuE.æ4·Ô}÷Ç—p@.
             000020 E0 B0 81 51 42 72                                     à°QBr
             */
            //ns.WriteHex(
            //"2400DD0564F11F825223F4C495643405D55A754516E634B7D47DF7C797704010E0B081514272");
            //"2400DD057612AC01" +
            //    "010000000100000000FF000000008231835A000000000000000000000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"2400 DD05 76  12  AC01   01000000 01 00000000 FF 00000000 8231835A00000000 0000000000000000"
            //3.0.3.0
            //"2400 DD05 27  12  4F01   01000000 01 00000000 FF 00000000 8231835A00000000 0000000000000000"

            //count 4
            //01000000
            ns.Write((int)0x01);
            //AccountAttributeKind 1
            //01
            ns.Write((byte)0x01);
            //extraKind 4
            //00000000
            ns.Write((int)0x00);
            //worldId 1
            //FF
            ns.Write((byte)0xFF);
            //count 4
            //00000000
            ns.Write((int)0x00);
            //startDate 8
            //8231835A00000000
            ns.Write((long)0x5A833182);
            //endData 8
            //0000000000000000
            ns.Write((long)0x00);
        }
    }
}
