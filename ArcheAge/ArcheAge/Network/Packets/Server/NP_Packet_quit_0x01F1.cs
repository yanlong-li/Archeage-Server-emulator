using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_quit_0x01F1 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_quit_0x01F1() : base(05, 0x01F1)
        {
            //пакеты для входа в Лобби
            /*
            [26]            S>c             0ms.            22:13:45 .174      08.06.18
            -------------------------------------------------------------------------------
             TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
            ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
            пакет не определен  (size=$33/51)
            000000 31 00 DD 05 C5 33 55 C1 | 6F 9E 31 01 D2 A2 72 42     1.Ý.Å3UÁož1.Ò¢rB
            000010 12 E3 B3 83 53 23 F4 C4 | 94 64 34 05 D5 A5 75 4C     .ã³ƒS#ôÄ”d4.Õ¥uL
            000020 16 A1 D9 E9 33 0A 95 BE | F2 51 40 10 E0 B0 81 51     .¡Ùé3.•¾òQ@.à°Q
            000030 80 B0 E7                                              €°ç

            CPU Dump
            Address   Hex dump                                         ASCII (OEM - США)
            4450FCD8  95 13 A5 00|FE FF 00 00|00 00 00 00|00 00 00 00| òÑ ■ 
            4450FCE8  00 00 00 00|00 00 00 00|00 00 00 09|00 47 6F 6F|            	 Goo
            4450FCF8  64 2D 62 79|65 21 00 00|00 00 00 00|00 00 00 00| d-bye!
            4450FD08  00 00 00
             */
            //ns.WriteHex(
            //"3100DD05C53355C16F9E3101D2A2724212E3B3835323F4C494643405D5A5754C16A1D9E9330A95BEF2514010E0B0815180B0E7");
            //расшифрованные данные из снифа пакета
            // size hash crc idx opcode data
            //"3100 DD05 95  13  A500   FEFF000000000000 000000 00000000 00 00 00000000 0000 0900 476F6F642D62796521 00000000 00000000 00"
            //3.0.3.0
            //"3100 DD05 58  17  F101   FEFF0000000000000000000000000000000000000000000900476F6F642D62796521000000000000000000"

            //chat 8
            //FEFF000000000000
            ns.Write((long)0xFFFE);
            //bc 3
            //000000
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            //type 4
            //00000000
            ns.Write((int)0x00);
            //LanguageType 1
            //00
            ns.Write((byte)0x00);
            //CharRace 1
            //00
            ns.Write((byte)0x00);
            //type 4
            //00000000
            ns.Write((int)0x00);
            //size.name
            //0000
            string msg = "";
            ns.WriteUTF8Fixed(msg, msg.Length);
            //size.msg
            //0900 476F6F642D62796521 "Good - bye!"
            msg = "Good-bye!";
            ns.WriteUTF8Fixed(msg, msg.Length);
            //{ 4 раза
            //linkType 1
            //00
            ns.Write((byte)0x00);
            //{ в нашем случае пропущено - нет в пакете
            //start
            //length
            //data
            //qType
            //type
            //itemId
            //}
            //linkType 1
            //00
            ns.Write((byte)0x00);
            //{}
            //linkType 1
            //00
            ns.Write((byte)0x00);
            //{}
            //linkType 1
            //00
            ns.Write((byte)0x00);
            //{}
            //}
            //ability 4
            //00000000
            ns.Write((int)0x00);
            //option 1
            //00
            ns.Write((byte)0x00);
        }
    }
}
