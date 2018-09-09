using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x00BA : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x00BA() : base(05, 0x00BA)
        {
            //пакеты для входа в Лобби
            /*
             * [14]            S>c             0ms.            18:52:06 .517      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$0B/11)
             000000 09 00 DD 05 BF B6 7E 50 | 10 41 70                    ..Ý.¿¶~P.Ap
             */
            //ns.WriteHex(
            //"0900DD05BFB67E50104170");
            //"0900DD055F06" +
            //    "FF01000100");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0900 DD05 5F  06  FF01   00 01 00"
            //3.0.3.0
            //"0900 DD05 FF  06  BA00   00 01 00"

            //{ 3 цикл
            //used 1
            //00 01 00
            //}
            ns.Write((byte)0x00);
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
        }
    }
}
