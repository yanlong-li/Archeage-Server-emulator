using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x014D : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x014D() : base(05, 0x014D)
        {
            //пакеты для входа в Лобби
            /*
             * [45]            S>c             0ms.            18:52:08 .881      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$11/17)
             000000 0F 00 DD 05 06 37 C9 C6 | 97 70 40 10 E0 B0 81 51     ..Ý..7ÉÆ—p@.à°Q
             000010 86                                                    †
             */
            //ns.WriteHex(
            //"0F00DD050637C9C697704010E0B0815186");
            //"0F00DD0551103E01" +
            //    "000000000000000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0F00 DD05 51  10  3E01   00 00 00 00 00 00 00 00 00"
            //3.0.3.0
            //"0F00 DD05 58  10  4D01   000000000000000000"

            //{ 9 цикл
            // con 1
            // 00 00 00 00 00 00 00 00 00
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            //}
        }
    }
}
