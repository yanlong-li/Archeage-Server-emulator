using LocalCommons.Network;
using LocalCommons.Utilities;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x0094 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x0094() : base(05, 0x0094)
        {
            //пакеты для входа в Лобби
            /*
             * [8]             S>c             0ms.            18:52:06 .496      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$16/22)
             000000 14 00 DD 05 C2 E7 E3 86 | 56 27 F6 CF 97 08 72 65     ..Ý.Âçã†V'öÏ—.re
             000010 89 9F E9 24 21 75                                     ‰Ÿé$!u
            
            CPU Dump
            Address   Hex dump                                         ASCII (OEM - США)
            270990C8  14 00 DD 05|D4 00 55 00|01 00 01 08|00 78 32 75|  ▌╘ U   x2u
            270990D8  69 2F 68 75|64 00                              | i/hud  
*/
            //ns.WriteHex(
            //"1400DD05C2E7E3865627F6CF97087265899FE9242175");
            //ns.WriteHex(
            //"1400DD05D4015500" +
            //    "0100010800783275692F68756400");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"1400 DD05 D4  01  5500   01 00 01 0800 783275692F687564 00"
            //3.0.3.0
            //"1400 DD05 B9  01  9400   01 00 01 0800 783275692F687564 00"

            //sendAddrs 1
            //01
            ns.Write((byte)0x01);
            //spMd5 1
            //00
            ns.Write((byte)0x00);
            //luaMd5 1
            //01
            ns.Write((byte)0x01);
            //size.dir
            //0800 783275692F687564 "x2ui/hud"
            const string dir = "x2ui/hud";
            ns.WriteUTF8Fixed(dir, dir.Length);
            //modPack 1
            //00
            ns.Write((byte)0x01);
        }
    }
}
