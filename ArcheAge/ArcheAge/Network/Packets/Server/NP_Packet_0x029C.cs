using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x029C : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x029C() : base(05, 0x029C)
        {
            //пакеты для входа в Лобби
            /*
             * [20]            S>c             0ms.            18:52:06 .538      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$0C/12)
             000000 0A 00 DD 05 81 7C 58 12 | E0 B0 81 51                 ..Ý.|X.à°Q
             */
            //ns.WriteHex(
            //"0A00DD05817C5812E0B08151");
            //"0A00DD05160C1802" +
            //    "00000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0A00 DD05 16  0C  1802   00000000"
            //3.0.3.0
            //"0A00 DD05 A2  0C  9C02   00000000"

            //count 4
            //00000000
            ns.Write((int)0x00);
            //{
            //type
            //declareDominion
            //}
        }
    }
}
