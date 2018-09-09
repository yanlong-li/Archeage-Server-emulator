using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x0145 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x0145() : base(05, 0x0145)
        {
            //пакеты для входа в Лобби
            /*
             * [53]            S>c             0ms.            18:52:09 .022      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$1F/31)
             000000 1D 00 DD 05 27 77 B6 07 | 02 31 74 45 17 E6 BD 86     ..Ý.'w¶..1tE.æ½†
             000010 21 42 85 B4 FE 1F 2E 30 | D1 BD 8B 5D C4 F4 23        !B…´þ..0Ñ½‹]Äô#
             */
            //ns.WriteHex(
            //"1D00DD052777B6070231744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423");
            //расшифрованные данные из снифа пакета
            // size hash crc idx opcode data
            //3.0.0.7
            //"1D00 DD05 B3  13  8202   D7940100 0100 0B00 76657273696F6E20310D0A 0C000000"
            //3.0.3.0
            //"1D00 DD05 5B  13  4501   D7940100 0100 0B00 76657273696F6E20310D0A 0C000000"

            //type 4 (charID)
            //D7940100
            ns.Write((int)0x0194D7);
            //uiDataType 2
            //0100
            ns.Write((short)0x01);
            //size.uiData
            //0B00 76657273696F6E20310D0A
            string uiData = "76657273696F6E20310D0A";
            ns.WriteHex(uiData, uiData.Length);
            //size 4
            //0C00000000
            ns.Write((int)0x0C);
        }
    }
    public sealed class NP_Packet_0x0145_2 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x0145_2() : base(05, 0x0145)
        {
            //пакеты для входа в Лобби
            /*
             * [54]            S>c             0ms.            18:52:09 .100      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  A  D  E  F    -------------------
             пакет не определен  (size=$1F/31)
             000000 1D 00 DD 05 1C 70 B6 07 | 02 31 74 45 14 E6 BD 86     ..Ý..p¶..1tE.æ½†
             000010 21 42 85 B4 FE 1F 2E 30 | D2 BD 8B 5D C4 F4 23        !B…´þ..0Ò½‹]Äô#
             */
            //ns.WriteHex(
            //"1D00DD051C70B6070231744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423");
            //расшифрованные данные из снифа пакета
            // size hash crc idx opcode data
            //3.0.0.7
            //"1D00 DD05 88  14  8202   D7940100 0200 0B00 76657273696F6E20320D0A 0C000000"
            //3.0.3.0
            //"1D00 DD05 30  14  4501   D7940100 0200 0B00 76657273696F6E20320D0A 0C000000"

            //type 4 (charID)
            //D7940100 
            ns.Write((int)0x000194D7);
            //uiDataType 2 
            //0200 
            ns.Write((short)0x02);
            //size.uiData
            //0B00 76657273696F6E20320D0A 
            string uiData = "76657273696F6E20320D0A";
            ns.WriteHex(uiData, uiData.Length);
            //size 4 
            //0C00000000
            ns.Write((int)0x0C);
        }
    }
    public sealed class NP_Packet_0x0145_3 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x0145_3() : base(05, 0x0145)
        {
            //пакеты для входа в Лобби
            /*
             * [55]            S>c             0ms.            18:52:09 .106      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  A  D  E  F    -------------------
             пакет не определен  (size=$1F/31)
             000000 1D 00 DD 05 0D 71 B6 07 | 43 42 74 45 17 E6 BD 86     ..Ý..q¶.CBtE.æ½†
             000010 21 42 85 B4 FE 1F 2E 30 | D1 BD 8B 5D C4 F4 23        !B…´þ..0Ñ½‹]Äô#
             */
            //ns.WriteHex(
            //"1D00DD050D71B6074342744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423");
            //расшифрованные данные из снифа пакета
            // size hash crc idx opcode data
            //"1D00 DD05 99  15  8202   96E70100 0100 0B00 76657273696F6E20310D0A 0C000000"
            //3.0.3.0
            //"1D00 DD05 E3  15  4501   96E70100 0200 0B00 76657273696F6E20320D0A 0C000000"

            //type 4 (charID)
            //96E70100
            ns.Write((int)0x01E796);
            //uiDataType 2
            //0100
            ns.Write((short)0x01);
            //size.uiData
            //0B00 76657273696F6E20310D0A
            string uiData = "76657273696F6E20310D0A";
            ns.WriteHex(uiData, uiData.Length);
            //size 4
            //0C00000000
            ns.Write((int)0x0C);
        }
    }
    public sealed class NP_Packet_0x0145_4 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x0145_4() : base(05, 0x0145)
        {
            //пакеты для входа в Лобби
            /*
             * [56]            S>c             0ms.            18:52:09 .108      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  A  D  E  F    -------------------
             пакет не определен  (size=$1F/31)
             000000 1D 00 DD 05 FA 72 B6 07 | 43 42 74 45 14 E6 BD 86     ..Ý.úr¶.CBtE.æ½†
             000010 21 42 85 B4 FE 1F 2E 30 | D2 BD 8B 5D C4 F4 23        !B…´þ..0Ò½‹]Äô#
             */
            //ns.WriteHex(
            //"1D00DD05FA72B6074342744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423");
            //расшифрованные данные из снифа пакета
            // size hash crc idx opcode data
            //"1D00 DD05 6E  16  8202   96E70100 0200 0B00 76657273696F6E20320D0A 0C000000"
            //3.0.3.0
            //"1D00 DD05 74  16  4501   96E70100 0100 0B00 76657273696F6E20310D0A 0C000000"

            //type 4
            //96E70100
            ns.Write((int)0x01E796);
            //uiDataType 2   
            //0200
            ns.Write((short)0x02);
            //size.uiData
            //0B00 76657273696F6E20320D0A
            string uiData = "76657273696F6E20320D0A";
            ns.WriteHex(uiData, uiData.Length);
            //size 4   
            //0C00000000
            ns.Write((int)0x0C);
        }
    }
}
