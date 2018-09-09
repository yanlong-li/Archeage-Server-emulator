using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x01CC : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x01CC() : base(05, 0x01CC)
        {
            //пакеты для входа в Лобби
            /*
             * [16]            S>c             0ms.            18:52:06 .526      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$10/16)
             000000 0E 00 DD 05 84 2F 7E C7 | 97 70 40 10 E0 B0 81 51     ..Ý.„/~Ç—p@.à°Q
             */
            //ns.WriteHex(
            //"0E00DD05842F7EC797704010E0B08151");
            //"0E00DD05D3088900" +
            //    "0000000000000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0E00 DD05 D3  08  8900   0000000000000000"
            //3.0.3.0
            //"0E00 DD05 0D  08  CC01   0000000000000000"

            //convertRatioToAAPoint 8
            //0000000000000000
            ns.Write((long)0x00);
        }
    }
}
