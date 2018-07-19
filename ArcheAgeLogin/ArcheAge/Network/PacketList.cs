using LocalCommons.Encryption;

using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Packet List That Contains All Game / Client Packet Delegates.
    /// </summary>
    public static class PacketList
    {
        private static int m_Maintained;
        private static PacketHandler<GameConnection>[] m_GHandlers;
        private static PacketHandler<ArcheAgeConnection>[] m_LHandlers;
        private static string   clientVersion;

        public static PacketHandler<GameConnection>[] GHandlers
        {
            get { return m_GHandlers; }
        }

        public static PacketHandler<ArcheAgeConnection>[] LHandlers
        {
            get { return m_LHandlers; }
        }

        public static void Initialize(string clientVersion)
        {
            PacketList.clientVersion = clientVersion;
            m_GHandlers = new PacketHandler<GameConnection>[0x10];
            m_LHandlers = new PacketHandler<ArcheAgeConnection>[0x10];

            Registration();
        }

        private static void RunSnippet()
        {
            byte checksum;
            byte[] testVal = new byte[]
            {0xee, 0x01, 0x13, 0x00, 0x06, 0x1c, 0x00, 0x20,  0x1d, 0x00, 0x00};
            Encryption.CRC8Calc crc_8 = new Encryption.CRC8Calc(Encryption.CRC8_POLY.CRC8);
            checksum = crc_8.Checksum(testVal);
            Encryption.CRC8Calc crc_dallas = new Encryption.CRC8Calc(Encryption.CRC8_POLY.CRC8_DALLAS_MAXIM);
            checksum = crc_dallas.Checksum(testVal);
            Encryption.CRC8Calc crc = new Encryption.CRC8Calc(Encryption.CRC8_POLY.CRC8_CCITT);
            checksum = crc.Checksum(testVal);
            ;
        }
        ///для теста
        private static void ViewDecodeDD05()
        {
            string msg = "";
            msg = "3100DD05713755C16F9E3101D2A2724212E3B3835323F4C494643405D5A5754C16A1D9E9330A95BEF2514010E0B0815180B0E7";
            ViewDecode2(msg);
            msg = "0A00DD054568831103D1A6E0";
            ViewDecode2(msg);
            msg = "3100DD05713755C16F9E3101D2A2724212E3B3835323F4C494643405D5A5754C16A1D9E9330A95BEF2514010E0B0815180B0E7";
            ViewDecode2(msg);
            msg = "0A00DD05B5688311E9AF024C";
            ViewDecode2(msg);


            //пакет от клиента - не правильно декодируется
            msg = "13000005393DB7A29CAA4C2365F02DB94C5B18BB50";
            ViewDecode2(msg);
            //пакет от сервера - правильно декодируется
            msg = "0F00DD05AA2DE0C797704010E0B0815186";
            ViewDecode2(msg);
            msg = "1400DD05C2E7E3865627F6CF97087265899FE9242175";
            ViewDecode2(msg);
            msg = "5000DD057F20F4C282625271B0CB11257381D3E43840DBA6F90B2C06A99043486EEFCD4B6745F31F35E70901D0441D85AB78EE825322F4C494643405D5A5754517E7B7875627F7C797704010E0B0115081B0";
            ViewDecode2(msg);
            msg = "A900DD0574936530FFE0A119356592C1B87D0D80A6E0105A6BBA8A10367483C1AB3C4882A3F1145673BEC8196E6492D9EE3F4690ACF7111C72A0CA052A138BC0F02457CEEABA044766BEC3172173C9D3F536418AFEC91E347486C3E0254B9DACBC16416ABCCD13257981C7AB25579CB3B905596BBBC2052472C8C0F5224398A0E2041E62A0C7162B66909CF3265197ACF5175128B6D710217F92C5AB314B98B0B911236489DFEF51E71747";
            ViewDecode2(msg);
            msg = "2A00DD05B9656B03D2A2724212E3B3835323F4C494643405B5F1754516E6B6865727F7C797704010E0B08151";
            ViewDecode2(msg);
            msg = "7700DD05F997B53000EEA3724212E2B4845524F4C595643505D7A6764616E7B7875767BED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C49465340AC6A5754566A4B3865727F7C781348DDCAC8F8B99F2";
            ViewDecode2(msg);
            msg = "0900DD05BFB67E50104170";
            ViewDecode2(msg);
            msg = "1C00DD054863A207D5AF754516E6B9895827F7C897704010E0B0815EC4F4";
            ViewDecode2(msg);
            msg = "0E00DD05842F7EC797704010E0B08151";
            ViewDecode2(msg);
            msg = "0900DD052CB90D53114270";
            ViewDecode2(msg);
            msg = "0E00DD054E2DB8C597704010E0B08151";
            ViewDecode2(msg);
            msg = "2300DD0500E82E825223F4C494643405D5A5754516E6B6865727F7C797704010E0B0815142";
            ViewDecode2(msg);
            msg = "0A00DD05817C5812E0B08151";
            ViewDecode2(msg);
            msg = "0700DD050CBD7B5010";
            ViewDecode2(msg);
            msg = "2A00DD05EA6F6B03D3A2724213E3B3835323F4C4946434053B833B2816E6B6865727F7C797704010E0B08151";
            ViewDecode2(msg);
            msg = "FE00DD0595F92296663707D7A7775020F0C090613101D1A1724212E2B2835323F3C494643404D5A5754515E6B6865626F7C7976737FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C596663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393633304D4A4744415E5B5855526F6C696663707D7A7774010E0B0815121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704010E0B08151";
            ViewDecode2(msg);
            msg = "0F00DD050637C9C697704010E0B0815186";
            ViewDecode2(msg);
            msg = "0209DD052C05ACB68556F261C495603654B3CB183376E4B591B032FED03F734111CDB382524AF2C393633303D4A4744414E5B5D1D53AF79DCD6636A22DA6724710E0B0805120F1C191636702D2A2734313E3B3845424F4C495653505D5A6764616E6B7875727F7D0A0704010E1F8BCF47BF2C292623303D3A3734414E4B5845525F5C595663606D6A6774717E7B7805020F0C09161315D8AA272E4E8E3B6835323F3C494653404D5A4334516E6B6865627F7C797674010E0B0805121F1C191623202D2A2734313E3B3845424BDF9303F3505D5A6764616E6B7875727F6C090603001D1A1714112E2B2825223F3C393633404D4A4741B4EE5B522AC27F3C696673707D7A6805020F1E391613101D1A2724212E2B3835323F4C494643405D5A5754516E6B6865627F7C7976779C375FA704111E1B1815222F2C292633203D3A3744414E4B4855525F5C696663606D7A7774717F0C051753001795B704412E2B2825223F2C393633586D4A4744515E5B5855626F6C696673707D7A7704010E0B1815121F1C292623202D3A33A7EB6BEB4845424F5C595653506D6A6774617E7B7875730FED0A0704111E1B1815222F2C292633303DBBC7444BD1EB4805525F5C696663706D7A776DC17E0B0805020F1C191613102D2A2724213E3B3835324F4C494643505D5A5754616E6FFBAF27DF7C7A0704010E0B1815121F1C392623202D3A3734313E4B4845424F5C595653506D6A67646F8F0B787FDDAF1C590603101D1A1734212E2B3015323F3C394643404D4A5754515E5B6865626F6C797673707E0B0805020F1C19161314BEF07284213E3B3835424F4C494653504D5A5764616E6B6875727F7C7906030FED0A1714111E1B282733AF2C338993206D4A4744414E5B4855525F74496663707D7A7775020F0C090613101D1A1724212E2B2835323F3C394643404D4A5750C2840EC865626F6C797673707D0A0704110E1B1815122F2C292623303D3A3734414E4B4845525F5C5066736065E3B774736E5B7906030FED0A07C4C11E1E21CC7DCF4C393637BB0E4585544DE447AC75626F6C696663707D7977E47EFE0B080AE21F1C191623202D2FD734313E3B3845424F4C495653585EAA6764616E7B7076827FE509F704011E1B1815122A6C29967CF07E7A6764B12EBB2845525F5C595E60906D6264847176788A90CAFCEC091E10E01D2224D4212638C286D1B0B9BCF41CB5A8E808ADF18E849865627F74797674000E0B07151002C0DD158390929A173B5E62CBF848524B3C59EA9A905D6A676AE16E7B7878E98F0C090603001D1AD8B5F12F0538E521DD2A7B040DA12D4A474AD15FD5D3CBE26D4F1969B3707D7B04F9C20F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865627F7C7976730FED0A0704111E1B109415077EFC8633303D3A474E6CDCDEE855525F5C6966636A40E8E2C4717F0C090603001D0DE5C1A12E2B2825223F3C393633404D4A4754515E5B5865626F6C697673707D7A0704010E0B1815121F1C292623202D3A3734313E4B4845424F5C58B653506D6A6764617E7B7876030FED0A0714111E1B1825222F3C293633203D3A4744414E44BF8787EF5C696663606D7A777D1F0E1B086506494B7F40D5E03D0A3A24113E32D815424D8C594655005D5A5764616E6B6875727F7C7F4F05F11BBEA81519132A994623202D3A3734213E4B4850124F5C595653606D6A6764717E7B7875020F0C090613101D1A1724212E2B2835323F35B14496F04D4A5754515E5B6865626F6C697673707E0B0805021F1C191613202D2A2724313E3B3835478AFC494D4F66ED3A5764616E6B6865727F7C6D66030FED0A1714111E1B2825222F3C393633304D4A4744415E5B5855526F6C69666379F578A2C5020F0C090613101D1A1724213E2B2835323F3C394643404D4A5754515E6B6865626F7C797396CFED012B32B17E1B1815122F2C392623302F0A3734414E4B4845525F5C595663606D6A6774717E7B7906030FED1A1714111E2B282CAA2DE9893633304D4A4744415E5B5855426F6C696663707D7A7774010E0B0805121F1C191623202D2636643135077EF5224F4C595653504D6A6764794E7B7875727FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C50EE61B5DD6A7774717E7B0805020F0C190613101D2A2724212E3B3835323F4C494643405D5A5754516EEAE86569334AC91674010E0B0815021F1C193F93202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727F0C09060300149215C1A12E2B2825223F3C393643404D5A4754515E5B5865626F6C697673707D7B0805020F0C19161DE16D2A2C78179E5B3835323F4C495643405D427754516E6B6865727F7C7976030FED0A0714111E1B1825222F2C293633303D3A474448C6498DE5525F5C696663606D7A7774716F0C090603001D1A1714212E2B2825323F3C393643404D4A4545D15E503453D20F6C697673707D6A070401162B1815121F1C292623202D3A3734314E4B4845425F5C595653606D6A6764717E7B787F8B0D38BA0714111E1B1825222F2C293623303D3A4744414E4B5855525F5C696663607D7A7774710E0CFCD5021EDA291612900D2A2724313E3B28E5924F444A93B3102D5A5768203814FA657EC5608E2738B00E0B1815121F2C292623203D3A3734314E4B4845425F5C195653606D6A6764717E7B78750A0CFC090613101D1214D4212628D835323F3C394643400D51974EF15E6B6865626F7C79767B72FE0B0006F21F141AEEEF037EDA272C32CE3B3036B24F444AB864E8E2A0A9AA9E9495B68A87FBFF86F6030FED1217141FEE25D82BC22F2C093633304D4A4654415E5B585AB26F0FD2F66EF0739A7A35008EB2B7161D6012195514212E2B3833723F3C494643404B1A57545E5E6B6865626F7C7BCB237FED0C4704011E14881512212C292623303D3A3734414E4B4855525F5C596663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393EB207651892F4415E5B58555842FEFCD663707D7A77040104269A80A21F1C192623202D2A6F16E48E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292633303D3A3744414E4B4855525F5C596663606D6A7694717E7B0805020F0C191613101D2A2724212E3B3835323F4C594643504D5A5754616E6B6782A0AACC797704010E0B08151";
            ViewDecode2(msg);
            msg = "2400DD0564F11F825223F4C495643405D55A754516E634B7D47DF7C797704010E0B081514272";
            ViewDecode2(msg);
            msg = "1D00DD052777B6070231744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423";
            ViewDecode2(msg);
            msg = "1D00DD051C70B6070231744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423";
            ViewDecode2(msg);
            msg = "0F00DD05AA2DE0C797704010E0B0815186";
            ViewDecode2(msg);
            msg = "1D00DD050D71B6074342744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423";
            ViewDecode2(msg);
            msg = "1D00DD05FA72B6074342744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423";
            ViewDecode2(msg);
            //msg = "";
            //ViewDecode2(msg);
            //msg = "";
            //ViewDecode2(msg);
        }
        ///для теста
        private static void ViewDecode2(string message)
        {
            ////шлем дешифрованный пакет
            string msg1 = message;
            Logger.Trace("Encode: " + msg1);
            string msg2 = msg1.Substring(8, msg1.Length - 8);
            byte[] cipherbytes = Utility.StringToByteArray(msg2);
            cipherbytes = Encryption.StoCEncrypt(cipherbytes);
            string msg3 = Utility.ByteArrayToString(cipherbytes);
            msg2 = msg3;
            msg3 = msg1.Substring(0, 8) + msg3;
            Logger.Trace("Decode: " + msg3);

            //byte[] testVal = new byte[]
            //{ 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB9, 0x2E, 0x30, 0x7A, 0xE2, 0x04, 0x6C, 0x46, 0xA4, 0x5A, 0x00, 0x00, 0x00, 0x00, 0x4C, 0xFF, 0xFF, 0xFF, 0x04, 0x01, 0x04, 0x01, 0x00, 0x04, 0x00, 0x00, 0xEC, 0xDD, 0x39, 0xAB, 0x65, 0xC9, 0xFD, 0x7F, 0x6B, 0x9D, 0xD4, 0x37, 0xA5, 0xD2, 0x36, 0x75, 0x4E, 0x2A, 0xA5, 0xC3, 0xCE, 0xBB, 0xB6, 0x99, 0xBF, 0xAD, 0x4B, 0xD0, 0xCE, 0x73, 0x4F, 0xAF, 0x2E, 0x5A, 0x62, 0x09, 0x9B, 0x39, 0xB6, 0xA9, 0x59, 0x13, 0x26, 0xCE, 0x91, 0x63, 0x1F, 0x6B, 0x8E, 0x8F, 0xD4, 0x9C, 0x88, 0x4D, 0x5F, 0x54, 0x15, 0xA3, 0x0A, 0xE3, 0xB1, 0xDB, 0x46, 0xF9, 0x35, 0xBB, 0xA8, 0x17, 0xDD, 0x77, 0xF6, 0x88, 0x5E, 0x62, 0xA2, 0xE8, 0xF7, 0x59, 0x27, 0x0B, 0x5B, 0xB7, 0xDD, 0xCC, 0xE4, 0x92, 0x12, 0x23, 0x5D, 0x98, 0x49, 0x32, 0xBF, 0xD8, 0x01, 0x89, 0xF8, 0xB3, 0x80, 0x6A, 0xB9, 0xB1, 0x28, 0x1B, 0x99, 0x35, 0xB5, 0x57, 0x71, 0x36, 0xDF, 0x09, 0x69, 0x3C, 0xA9, 0x35, 0x3C, 0xE2, 0xA7, 0xDF, 0xCE, 0xBC, 0x46, 0x2B, 0xC1, 0xC4, 0xE9, 0x7F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0xA0, 0x74, 0xB7, 0x3E, 0x4A, 0x95 };
            ////{0x11, 0x48, 0x02, 0x01, 0x00};

            //uint checksum00 = Encryption.Crc32.ComputeChecksum(testVal);
            //byte checksum5 = Encryption.Crc8_(testVal, testVal.Length);

            //Encryption.CRC8Calc crc_8 = new Encryption.CRC8Calc(Encryption.CRC8_POLY.CRC8);
            //byte checksum0 = crc_8.Checksum(testVal);

            //msg1 = msg2.Substring(2, msg2.Length-2*2);
            //byte[] cipherbytes2 = Utility.StringToByteArray(msg1);
            //byte crc1 = Encryption.CRC8(cipherbytes2, cipherbytes2.Length); //02 Д.Б 211, 0xD3
            //byte crc2 = Encryption.Crc8.ComputeChecksum(cipherbytes2); //243
            //byte crc3 = Encryption.CRC_8.Calculate(cipherbytes2); //226
            //var checksum = Encryption.CRC_8.Calculate(new byte[] { 0xAA, 0x07, 0x00, 0x00, 0x07, 0x66, 0xE6, 0x60, 0x44, 0x73 }); //105
            //var checksum2 = Encryption.CRC_8.Calculate(new byte[] { 0xAA, 0x07, 0x00, 0x00, 0x07, 0x66, 0xE6, 0x60, 0x44 });     //191
            //var checksum3 = Encryption.CRC_8.Calculate(new byte[] { 170, 7, 0, 0, 7, 102, 230, 96, 68 });                               //191
            //var checksum4 = Encryption.CRC_8.Calculate(new byte[] { 170, 7, 0, 0, 7, 102, 230, 96, 68, 115 });                   //105
            ;
        }
        private static void Registration()
        {
            ///для теста
            //var checksum = Encryption._CRC8_(new byte[] { 0x9F, 0x3E, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x6E, 0x6F, 0x70 }); //, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, }); //0x73 - должно быть
            //var checksum = Encryption._CRC8_(new byte[] { 0x03, 0x3E, 0x01, 0x02, 0x09, 0x00, 0x00, 0x00, 0xB9, 0x0B, 0x00, 0x00, 0x62, 0x14, 0xFB, 0x19 });
            //var checksum = Encryption._CRC8_(new byte[] { 0x01, 0x55, 0x00, 0x01, 0x00, 0x01, 0x08, 0x00, 0x78, 0x32, 0x75, 0x69, 0x2F, 0x68, 0x75, 0x64, 0x00}); //0xD4 - должно быть

            //расшифровать пакет DD05
            //ViewDecodeDD05();

            ///RunSnippet();
            //------------------------------------------------------------------------------------------------
            //Game Server Delegates Packets
            //------------------------------------------------------------------------------------------------
            Register(0x00, new OnPacketReceive<GameConnection>(Handle_RegisterGameServer));//Level registration server
            //Register(0x00, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected));//Level registration server
            Register(0x02, new OnPacketReceive<GameConnection>(Handle_UpdateCharacters));//Level registration server
            //------------------------------------------------------------------------------------------------
            //END Game Server Delegates Packets
            //------------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------------
            //Client Delegates Packets
            //------------------------------------------------------------------------------------------------
            //Register(0x01, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuth_0x01));  //
            //Register(0x02, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuthTencent_0x02));  //
            //Register(0x03, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuthGameOn_0x03));  //
            //Register(0x04, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuthMailRu_0x04));  //
            //Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_CAChallengeResponse_0x05));  //Token authentication service -r mode
            //Register(0x09, new OnPacketReceive<ArcheAgeConnection>(Handle_CAPcCertNumber_0x09));  //
            //Register(0x0A, new OnPacketReceive<ArcheAgeConnection>(Handle_CAListWorld_0x0A));  //
            //Register(0x0B, new OnPacketReceive<ArcheAgeConnection>(Handle_CAEnterWorld_0x0B));  //

            ////Register(0x01, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //Account login service
            ////Register(0x03, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue2)); //Token verification service wegame China
            ////Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue));  //Token authentication service -r mode
            ///Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue));  //Token authentication service -r mode service
            //////Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //Boarding landing service
            //////Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_05));     //Login verification
            if (clientVersion == "3")
            {   //ver.3.0
                Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_CAChallengeResponse2_0X06)); //пакет №1 от клиента
                Register(0x0c, new OnPacketReceive<ArcheAgeConnection>(Handle_CACancelEnterWorld_0X0C)); //пакет №2 от клиента
                Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestReconnect_0X0D)); //пакет №3 от клиента
                Register(0x0f, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestReconnect2_0X0F)); //пакет №3 от клиента
            }
            else
            {   //ver.4.0
                Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_CAChallengeResponse2_0X06)); //пакет №1 от клиента
                Register(0x0c, new OnPacketReceive<ArcheAgeConnection>(Handle_RequestServerList)); //Return to server list<=2.9
                Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected));//Return server address based on server id
            }
            //Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_0d));//Return server address based on server id //0b 00 0d 00 00 00 00 00 00 00 00 36（36 possible server id）
            //Register(0x08, new OnPacketReceive<ArcheAgeConnection>(Handle_RequestServerList)); //Return to server list
            //Register(0x09, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected)); //Server query
            //------------------------------------------------------------------------------------------------
            //END Client Delegates Packets
            //------------------------------------------------------------------------------------------------

        }

        #region Game Server Delegates
        //используется
        private static void Handle_UpdateCharacters(GameConnection net, PacketReader reader)
        {
            long accountId = reader.ReadLEInt64();  //AccID
            int characters = reader.ReadLEInt32(); //количество чаров на аккаунте

            Account currentAc = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            currentAc.Characters = characters;
        }

        //используется
        private static void Handle_RegisterGameServer(GameConnection net, PacketReader reader)
        {
            byte id = reader.ReadByte();
            short port = reader.ReadLEInt16();

            string ip = reader.ReadDynamicString();
            string password = reader.ReadDynamicString();

            bool success = GameServerController.RegisterGameServer(id, password, net, port, ip);
            net.SendAsync(new NET_GameRegistrationResult(success));
            //net.SendAsync(new NP_ServerList());
        }
        #endregion

        #region Client Delegates
        /// <summary>
         /// 0x06_CAChallengeResponse2Packet - token Verification mode
        /// uid+token
        /// </summary>
        private static void Handle_CAChallengeResponse2_0X06(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset = 21; //скипаем 2 байта опкода и 19 неизвестных байт
            int mRUidLength = reader.ReadLEInt16(); //длина строки
            var mUid = reader.ReadString(mRUidLength); //считываем Reading Login or User ID
            int mRtokenLength = reader.ReadLEInt16(); // длина строки
            var mRToken = reader.ReadHexString(mRtokenLength); //считываем токен
            var nCurrent = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == Convert.ToInt32(mUid));
            if (nCurrent != null)
            {
                Logger.Trace("account number: <<" + nCurrent.AccountId + ":" + nCurrent.Name + ">> Landing");
                //account numberexist
                if (nCurrent.Token.ToLower() == mRToken.ToLower())
                {
                    net.CurrentAccount = nCurrent;
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("account number: <<" + nCurrent.AccountId + ":" + nCurrent.Name + ">> Landed successfully");
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }

                Logger.Trace("account number: <<" + nCurrent.AccountId + ":" + nCurrent.Name +
                             ">> Token verification failed：" + mRToken.ToLower());
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + mUid);
            }

            //If the front did not terminate, then the account number failed to log in
            net.SendAsync(new NP_FailLogin());
        }

        private static void Handle_CACancelEnterWorld_0X0C(ArcheAgeConnection net, PacketReader reader)
        {
            var unknown = reader.ReadByteArray(8); //unk?
            net.SendAsync(new AcWorldList_0X08(clientVersion));
            net.SendAsync(new AcAccountWarned_0X0D(clientVersion));
        }

        /// <summary>
        /// Client choose server to send serverIP, server port number, sessionID
        ///</summary>>
        private static void Handle_CARequestReconnect_0X0D(ArcheAgeConnection net, PacketReader reader)
        {
            /*
             [7]             C>s             0ms.            23:56:45 .957      10.03.18
               -------------------------------------------------------------------------------
               TType: ArcheageServer: undef   Parse: 6           EnCode: off         
               ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
               000000 0B 00 0D 00 00 00 00 00 | 00 00 00 00 01              .............
               -------------------------------------------------------------------------------
               Archeage: "CARequestReconnect"               size: 13     prot: 2  $002
               Addr:  Size:    Type:         Description:     Value:
               0000     2   word          psize             11         | $000B
               0002     2   word          ID                13         | $000D
               0004     4   integer       p_from            0          | $00000000
               0008     4   integer       p_to              0          | $00000000
               000C     1   byte          serverId          1          | $01
                        4   integer       cookie
                        ?   WideStr[byte] MAC
             */
            //0B00 0D00 00000000 00000000 01
            //reader.Offset += 8; //Undefined Data
            var p_from = reader.ReadLEInt32();
            var p_to = reader.ReadLEInt32();
            var serverId = reader.ReadByte();
            //serverId = 01;
            var server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server != null && server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    net.CurrentAccount.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    net.CurrentAccount.LastIp = net.ToString(); // IP
                    //net.CurrentAccount.AccountId = net.CurrentAccount.AccountId; // 
                    //create session (cookie)
                    ///var cookie = 128665876; //$07AB4914 - для теста
                    ///net.CurrentAccount.Session = cookie;
                    //AccountHolder.AccountList.FirstOrDefault(n => n.AccId == Convert.ToInt32(cookie));
                    
                    // генерируем cookie
                    Random random = new Random();
                    int cookie = random.Next(255);
                    cookie += random.Next(255) << 8;
                    cookie += random.Next(255) << 16;
                    cookie += random.Next(255) << 24;
                    net.CurrentAccount.Session = cookie; //Designated session

                    //Передаем управление Гейм серверу
                    net.movedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    //отсылаем Гейм серверу информацию об аккаунте
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    //отсылаем Клиенту информацию об куках
                    net.SendAsync(new AcWorldCookie_0X0A(server, cookie));
                }
            }
            else
            {
                Logger.Trace("No serverID requested：" + serverId);
                net.Dispose();
            }
        }
        /// <summary>
        /// Client choose server to send serverIP, server port number, sessionID
        ///</summary>>
        private static void Handle_CARequestReconnect2_0X0F(ArcheAgeConnection net, PacketReader reader)
        {
            /*
             [1]             C>s             0ms.            15:57:39 .521      25.06.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: LS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             000000 21 00 0F 00 0A 00 00 00 | 08 00 00 00 1A C7 00 00     !............Ç..
             000010 00 00 00 00 01 09 1F 83 | 1D 08 00 00 00 00 00 00     .......ƒ........
             000020 00 00 00                                              ...
             -------------------------------------------------------------------------------
             Archeage: "CARequestReconnect"               size: 35     prot: 2  $002
             Addr:  Size:    Type:         Description:     Value:
             0000     2   word          psize             33         | $0021
             0002     2   word          ID                15         | $000F
             0004     4   integer       p_from            10         | $0000000A
             0008     4   integer       p_to              8          | $00000008
             000C     8   int64         accountId         50970      | $0000C71A
             0014     4   integer       cookie            -2095118079 | $831F0901
             0018  2079   WideStr[byte] MAC               00:00:00:00:00:00:00:00:00  ($)
             */
            //0B00 0D00 00000000 00000000 01
            //reader.Offset += 8; //Undefined Data
            ///cookie = 6838A8D5
            ///cookie = $E0169438
            ///cookie = $2761E301
            ///
            var p_from = reader.ReadLEInt32();
            var p_to = reader.ReadLEInt32();
            var serverId = reader.ReadByte();
            //serverId = 01;
            var server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server != null && server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    net.CurrentAccount.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    net.CurrentAccount.LastIp = net.ToString(); // IP
                    // генерируем cookie
                    Random random = new Random();
                    int cookie = random.Next(255);
                    cookie += random.Next(255) << 8;
                    cookie += random.Next(255) << 16;
                    cookie += random.Next(255) << 24;
                    net.CurrentAccount.Session = cookie; //Designated session

                    //Передаем управление Гейм серверу
                    net.movedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    //отсылаем Гейм серверу информацию об аккаунте
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    //отсылаем Клиенту информацию об куках
                    net.SendAsync(new AcWorldCookie_0X0A(server, cookie));
                }
            }
            else
            {
                Logger.Trace("No serverID requested：" + serverId);
                net.Dispose();
            }
        }

        private static void Handle_SignIn(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset += 12; //Static Data - 0A 00 00 00 07 00 00 00 00 00 
            int m_RLoginLength = reader.ReadLEInt16();
            reader.Offset += 2;
            string m_RLogin = reader.ReadString(m_RLoginLength); //Reading Login
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_RLogin);
            if (n_Current == null)
            {
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Account m_New = new Account();
                    m_New.AccountId = AccountHolder.AccountList.Count + 1;
                    m_New.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    m_New.AccessLevel = 0;
                    m_New.LastIp = net.ToString();
                    m_New.Membership = 0;
                    m_New.Name = m_RLogin;
                    net.CurrentAccount = m_New;
                    AccountHolder.AccountList.Add(m_New);
                }
                else
                    net.CurrentAccount = null;
            }
            else
            {
                net.CurrentAccount = n_Current;
            }
            // net.SendAsync(new NP_PasswordCorrect(1));
            net.SendAsync(new NP_ServerList( clientVersion));
        }

        private static void Handle_SignIn_Continue(ArcheAgeConnection net, PacketReader reader)
        {
            //HOW TO DECRYPT IT ????
            //string password = "";
            //If the account is not empty, login fails
            if (net.CurrentAccount == null)
            {
                //Return login failure information
                net.SendAsync(new NP_FailLogin());
                return;
            }

            /* TODO
            if (net.CurrentAccount.Password == null)
            {
                //Means - New Account.
                net.CurrentAccount.Password = password;
            }
            else
            {
                //Checking Password
                if (net.CurrentAccount.Password != password)
                {
                    net.SendAsync(new NP_FailLogin());
                    return;
                }
            }
            */
            net.SendAsync(new NP_AcceptLogin(clientVersion));
            net.CurrentAccount.Session = net.GetHashCode();
            net.SendAsync(new NP_PasswordCorrect(net.CurrentAccount.Session));
            Logger.Trace("Account login: " + net.CurrentAccount.Name);
            GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
        }

        /**
         *token Verification mode
         *uid+token
         * 
         */
        private static void Handle_Token_Continue(ArcheAgeConnection net,PacketReader reader)
        {
            reader.Offset = 21;
            int m_RUidLength = reader.ReadLEInt16();
            string m_uid = reader.ReadString(m_RUidLength); //Reading Login
            int m_RtokenLength = reader.ReadLEInt16();
            string m_RToken = reader.ReadHexString(m_RtokenLength);
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId ==Convert.ToInt64(m_uid));
            if (n_Current !=null )
            {
                Logger.Trace("account number: < " + n_Current.AccountId + ":" + n_Current.Name + "> is landing");
                //accounts exist
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    //Write account information to online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account: < " + n_Current.AccountId+":" + n_Current.Name+ "> landing success");
                    net.SendAsync(new NP_AcceptLogin(clientVersion));
                    net.SendAsync(new NP_03key(clientVersion));
                    //return server list
                    //net.SendAsync(new NP_ServerList());
                    return;
                }
                Logger.Trace("Account: < " + n_Current.AccountId+":" + n_Current.Name+ "> token verification failed: " + m_RToken.ToLower());

            }
            else
            {
                Logger.Trace("Client attempts to login to a nonexistent account" + m_uid);
            }

            //If there is no termination before, the account login fails
            net.SendAsync(new NP_FailLogin());
        }

          
        private static void Handle_Token_Continue2(ArcheAgeConnection net, PacketReader reader)
        {

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId ==1);
            if (n_Current != null)
            {
                Logger.Trace("The account is trying to login: " + n_Current.Name);
                //Account exists
                // if (n_Current.Password.ToLower() == m_RToken.ToLower())
                // {
                net.CurrentAccount = n_Current;
                //Write account information to online account list
                GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account login successful: " + net.CurrentAccount.Name);
                    net.SendAsync(new NP_AcceptLogin(clientVersion));
                    net.SendAsync(new NP_03key(clientVersion));
                //Return to server list
                //net.SendAsync(new NP_ServerList());
                return;
                //  }
                // Logger.Trace("account number: " + net.CurrentAccount.Name + "/Incorrect password：" + m_RToken.ToLower());
            }
            //If the previous did not stop, then account landing failed
            net.SendAsync(new NP_FailLogin());
        }

        //Send server list (based on packet capture)
        private static void Handle_05(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsyncHex(new NP_PasswordCorrect(1));
        }

        //Return server connection into packets
        private static void Handle_0d(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsync0d(new NP_PasswordCorrect(1));
            //net.SendAsync(new NP_ServerList());
        }

        private static void Handle_RequestServerList(ArcheAgeConnection net, PacketReader reader)
        {
            byte[] unknown = reader.ReadByteArray(8); //unk?
            net.SendAsync(new NP_ServerList(clientVersion));
        }

        /**
         * 
         * Client selects server to send
         * Server IP
         * Server port number
         * sessionID
         * */
        private static void Handle_ServerSelected(ArcheAgeConnection net, PacketReader reader)
        {
            //net.SendAsync(new NP_EditMessage2("systemTest"));
            //return;
            reader.Offset += 8; //00 00 00 00 00 00 00 00  Undefined Data
            byte serverId = reader.ReadByte();
            //serverId =1;
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server != null && server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    //create session
                    Random random = new Random();
                    int num = random.Next(255) + random.Next(255) + random.Next(255) + random.Next(255);
                    net.CurrentAccount.Session = num= 1323126619;//Specify session

                    net.movedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    net.SendAsync(new NP_SendGameAuthorization(server, num));   
                }
            }
            else
            {
                Logger.Trace("Requested a non-existent server ID:" + serverId);
                net.Dispose();
            }
        }

        #endregion

        private static void Register(short opcode, OnPacketReceive<ArcheAgeConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<ArcheAgeConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(short opcode, OnPacketReceive<GameConnection> e)
        {
            m_GHandlers[opcode] = new PacketHandler<GameConnection>(opcode, e);
            m_Maintained++;
        }
    }
}
