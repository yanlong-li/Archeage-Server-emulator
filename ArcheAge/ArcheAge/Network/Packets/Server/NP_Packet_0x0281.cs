using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x0281 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x0281() : base(05, 0x0281)
        {
            //пакеты для входа в Лобби
            /*
             * [13]            S>c             0ms.            18:52:06 .515      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$79/121)
             000000 77 00 DD 05 F9 97 B5 30 | 00 EE A3 72 42 12 E2 B4     w.Ý.ù—µ0.î£rB.â´
             000010 84 55 24 F4 C5 95 64 35 | 05 D7 A6 76 46 16 E7 B7     „U$ôÅ•d5.×¦vF.ç·
             000020 87 57 67 BE D0 A0 70 40 | 11 E1 B1 81 51 22 F2 C2     ‡Wg¾Ð p@.á±Q"òÂ
             000030 92 62 33 03 D3 A3 74 44 | 14 E4 B4 85 55 25 F5 C5     ’b3.Ó£tD.ä´…U%õÅ
             000040 96 66 36 06 D6 A7 77 47 | 17 E7 B0 80 50 20 F0 C1     –f6.Ö§wG.ç°€P.ðÁ
             000050 91 61 31 01 D2 A2 72 42 | 12 E3 B3 83 53 23 F4 C4     ‘a1.Ò¢rB.ã³ƒS#ôÄ
             000060 94 65 34 0A C6 A5 75 45 | 66 A4 B3 86 57 27 F7 C7     ”e4.Æ¥uEf¤³†W'÷Ç
             000070 81 34 8D DC AC 8F 8B 99 | F2                          4Ü¬‹™ò
             */
            //ns.WriteHex(
            //"7700DD05F997B53000EEA3724212E2B4845524F4C595643505D7A6764616E7B7875767BED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C49465340AC6A5754566A4B3865727F7C781348DDCAC8F8B99F2");
            //"7700DD053805D702" +
            //    "023C00010101010000010000000001000001000000000000000040400000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000F1300000070420500000000001644CDCC4C3F0AC803");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"7700 DD05 38  05  D702   02 3C00 01 01 01 01 00 00 01 00 00 00 00 01 00 00 01 00 00 00000000 00004040 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00 0100 0F 1300 000070420500000000001644CDCC4C3F0AC803"
            //3.0.3.0
            //"7700 DD05 72  05  8102   023C00010101010000010000000001000001000000000000000040400000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000F1300000070420500000000001644CDCC4C3F0AC803"

            //version 1
            //02
            ns.Write((byte)0x02);
            //reportDelay 2
            //3C00
            ns.Write((short)0x3c);
            //{ 11h (17) цикл
            //chatTypeGroup 1
            //01 01 01 01 00 00 01 00 00 00 00 01 00 00 01 00 00
            //}
            ns.Write((byte)0x01);
            ns.Write((byte)0x01);
            ns.Write((byte)0x01);
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            //{ 11h (17) цикл
            //chatGroupDelay 4
            //00000000 00004040 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000 
            //}
            ns.Write((int)0x00);
            ns.Write((int)0x40400000);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            //whisperChatGroup 1
            //00
            ns.Write((byte)0x00);
            //size.applyConfig
            //0100 0F
            const string applyConfig = "0F";
            ns.WriteHex(applyConfig, applyConfig.Length);
            //size.detectConfig
            //1300  000070420500000000001644CDCC4C3F0AC803
            const string detectConfig = "000070420500000000001644CDCC4C3F0AC803";
            ns.WriteHex(detectConfig, detectConfig.Length);
            
        }
    }
}
