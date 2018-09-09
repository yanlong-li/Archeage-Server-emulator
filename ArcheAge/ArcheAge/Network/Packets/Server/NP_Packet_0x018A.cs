using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x018A : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x018A() : base(05, 0x018A)
        {
            //пакеты для входа в Лобби
            /*
             * [15]            S>c             0ms.            18:52:06 .519      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$1E/30)
             000000 1C 00 DD 05 48 63 A2 07 | D5 AF 75 45 16 E6 B9 89     ..Ý.Hc¢.Õ¯uE.æ¹‰
             000010 58 27 F7 C8 97 70 40 10 | E0 B0 81 5E C4 F4           X'÷È—p@.à°^Äô
             */
            //ns.WriteHex(
            //"1C00DD054863A207D5AF754516E6B9895827F7C897704010E0B0815EC4F4");
            //"1C00DD05DC079602" +
            //    "000A000000000F0F0F00000F000000000000000F0000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"1C00 DD05 DC  07  9602   00 0A 00 00 00 00 0F 0F 0F 00 00 0F 00 00 00 00 00 00 00 0F 00 00"
            //3.0.3.0
            //"1C00 DD05 DF  07  8A01   000A000000000F0F0F00000F000000000000000F0000"

            //searchLevel 1
            //00
            ns.Write((byte)0x00);
            //bidLevel 1
            //0A
            ns.Write((byte)0x0A);
            //postLevel 1
            //00
            ns.Write((byte)0x00);
            //trade 1
            //00
            ns.Write((byte)0x00);
            //mail 1
            //00
            ns.Write((byte)0x00);
            //{ 11h (17) цикл
            //limitLevels 1
            //00 0F 0F 0F 00 00 0F 00 00 00 00 00 00 00 0F 00 00
            ns.Write((byte)0x00);
            ns.Write((byte)0x0F);
            ns.Write((byte)0x0F);
            ns.Write((byte)0x0F);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x0F);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x0F);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            //}
        }
    }
}
