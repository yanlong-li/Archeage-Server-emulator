using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x01AF : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x01AF() : base(05, 0x01AF)
        {
            //пакеты для входа в Лобби
            /*
             * [18]            S>c             0ms.            18:52:06 .530      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$10/16)
             000000 0E 00 DD 05 4E 2D B8 C5 | 97 70 40 10 E0 B0 81 51     ..Ý.N-¸Å—p@.à°Q
             */
            //ns.WriteHex(
            //"0E00DD054E2DB8C597704010E0B08151");
            //"0E00DD05190A4F02" +
            //    "0000000000000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0E00 DD05 19  0A  4F02   00000000 00000000"
            //3.0.3.0
            //"0E00 DD05 18  0A  AF01   0000000000000000"

            //indunCount 4
            //00000000
            ns.Write((int)0x00);
            //{
            //type 2
            //pvp 1
            //duel 1
            //}
            //conflictCount 4
            //00000000
            ns.Write((int)0x00);
            //{
            //type 2
            //peaceMin 4
            //}
        }
    }
}
