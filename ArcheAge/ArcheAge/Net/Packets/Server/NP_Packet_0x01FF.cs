using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Net
{
    public sealed class NP_Packet_0x01FF : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x01FF() : base(05, 0x01FF)
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
            ////расшифрованные данные из снифа пакета
            // size hash crc idx opcode data
            //"0900 DD05 5F  06  FF01   00 01 00"
            ///{ 3 цикл
            ///used 1
            ///00 01 00
            ///}
            ns.Write((byte)0x00);
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
        }
    }
}
