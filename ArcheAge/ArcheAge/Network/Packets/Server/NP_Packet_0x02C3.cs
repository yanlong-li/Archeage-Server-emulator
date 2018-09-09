using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_Packet_0x02C3 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_0x02C3() : base(05, 0x02C3)
        {
            //пакеты для входа в Лобби
            /*
             * [11]            S>c             0ms.            18:52:06 .507      06.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: GS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             пакет не определен  (size=$AB/171)
             000000 A9 00 DD 05 74 93 65 30 | FF E0 A1 19 35 65 92 C1     ©.Ý.t“e0ÿà¡.5e’Á
             000010 B8 7D 0D 80 A6 E0 10 5A | 6B BA 8A 10 36 74 83 C1     ¸}.€¦à.ZkºŠ.6tƒÁ
             000020 AB 3C 48 82 A3 F1 14 56 | 73 BE C8 19 6E 64 92 D9     «<H‚£ñ.Vs¾È.nd’Ù
             000030 EE 3F 46 90 AC F7 11 1C | 72 A0 CA 05 2A 13 8B C0     î?F¬÷..r Ê.*.‹À
             000040 F0 24 57 CE EA BA 04 47 | 66 BE C3 17 21 73 C9 D3     ð$WÎêº.Gf¾Ã.!sÉÓ
             000050 F5 36 41 8A FE C9 1E 34 | 74 86 C3 E0 25 4B 9D AC     õ6AŠþÉ.4t†Ãà%K¬
             000060 BC 16 41 6A BC CD 13 25 | 79 81 C7 AB 25 57 9C B3     ¼.Aj¼Í.%yÇ«%Wœ³
             000070 B9 05 59 6B BB C2 05 24 | 72 C8 C0 F5 22 43 98 A0     ¹.Yk»Â.$rÈÀõ"A˜ 
             000080 E2 04 1E 62 A0 C7 16 2B | 66 90 9C F3 26 51 97 AC     â..b Ç.+fœó&Q—¬
             000090 F5 17 51 28 B6 D7 10 21 | 7F 92 C5 AB 31 4B 98 B0     õ.Q(¶×.!’Å«1K˜°
             0000A0 B9 11 23 64 89 DF EF 51 | E7 17 47                    ¹.#d‰ßïQç.G
            расшифрованный пакет
             CPU Dump
            Address   Hex dump                                         ASCII (OEM - США)
            4456FCD8  B4 03 05 00|01 31 00 68|74 74 70 73|3A 2F 2F 73| ┤ 1 https://s
            4456FCE8  65 73 73 69|6F 6E 2E 64|72 61 66 74|2E 69 6E 74| ession.draft.int
            4456FCF8  65 67 72 61|74 69 6F 6E|2E 74 72 69|6F 6E 67 61| egration.trionga
            4456FD08  6D 65 73 2E|70 72 69 76|69 00 68 74|74 70 73 3A| mes.privi https:
            4456FD18  2F 2F 61 72|63 68 65 61|67 65 2E 64|72 61 66 74| //archeage.draft
            4456FD28  2E 69 6E 74|65 67 72 61|74 69 6F 6E|2E 74 72 69| .integration.tri
            4456FD38  6F 6E 67 61|6D 65 73 2E|70 72 69 76|2F 63 6F 6D| ongames.priv/com
            4456FD48  6D 65 72 63|65 2F 70 75|72 63 68 61|73 65 2F 63| merce/purchase/c
            4456FD58  72 65 64 69|74 73 2F 70|75 72 63 68|61 73 65 2D| redits/purchase-
            4456FD68  63 72 65 64|69 74 73 2D|66 6C 6F 77|2E 61 63 74| credits-flow.act
            4456FD78  69 6F 6E 00|00 00 00 00|00 00 00 00|00 00 00 00| ion
             */
            //ns.WriteHex(
            //"A900DD0574936530FFE0A119356592C1B87D0D80A6E0105A6BBA8A10367483C1AB3C4882A3F1145673BEC8196E6492D9EE3F4690ACF7111C72A0CA052A138BC0F02457CEEABA044766BEC3172173C9D3F536418AFEC91E347486C3E0254B9DACBC16416ABCCD13257981C7AB25579CB3B905596BBBC2052472C8C0F5224398A0E2041E62A0C7162B66909CF3265197ACF5175128B6D710217F92C5AB314B98B0B911236489DFEF51E71747");
            //"A900DD05B4030500" +
            //    "01310068747470733A2F2F73657373696F6E2E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E70726976690068747470733A2F2F61726368656167652E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E707269762F636F6D6D657263652F70757263686173652F637265646974732F70757263686173652D637265646974732D666C6F772E616374696F6E00000000");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"A900 DD05 B4  03  0500   01 3100 68747470733A2F2F73657373696F6E2E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E70726976 6900 68747470733A2F2F61726368656167652E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E707269762F636F6D6D657263652F70757263686173652F637265646974732F70757263686173652D637265646974732D666C6F772E616374696F6E 0000 0000"
            //3.0.3.0
            //"A900 DD05 E8  03  C302   01 3100 68747470733A2F2F73657373696F6E2E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E70726976690068747470733A2F2F61726368656167652E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E707269762F636F6D6D657263652F70757263686173652F637265646974732F70757263686173652D637265646974732D666C6F772E616374696F6E00000000"
            
            //activate 1
            //01
            ns.Write((byte)0x01);
            //size.platformURL
            //3100 68747470733A2F2F73657373696F6E2E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E70726976
            const string platformURL = "https://session.draft.integration.triongames.privi";
            ns.WriteUTF8Fixed(platformURL, platformURL.Length);
            //size.commerceURL
            //6900 68747470733A2F2F61726368656167652E64726166742E696E746567726174696F6E2E7472696F6E67616D65732E707269762F636F6D6D657263652F70757263686173652F637265646974732F70757263686173652D637265646974732D666C6F772E616374696F6E 
            const string commerceURL = "https://archeage.draft.integration.triongames.priv/commerce/purchase/credits/purchasecredits-flow.action";
            ns.WriteUTF8Fixed(commerceURL, commerceURL.Length);
            //size.wikiURL
            //0000
            ns.Write((short)0x00);
            //size.csURL
            //0000
            ns.Write((short)0x00);
            //
        }
    }
}
