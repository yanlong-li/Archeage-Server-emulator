using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    //============================================================
    public sealed class NP_Packet_0x0272 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x0272() : base(05, 0x0272)
        {
            //пакеты для входа в Лобби
            /*
             * [32]            S>c             0ms.            18:52:07 .627      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$09/9)
             000000 07 00 DD 05 0C BD 7B 50 | 10                          ..Ý..½{P.
             */
            //ns.WriteHex(
            //"0700DD050CBD7B5010");
            //"0700DD05EC0DFA01" +
            //    "00");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0700 DD05 EC  0D  FA01   00"
            //3.0.3.0
            //"0700 DD05 37  0D  7202   00"

            //sc 1
            //00
            ns.Write((byte)0x00);
        }
    }
}
