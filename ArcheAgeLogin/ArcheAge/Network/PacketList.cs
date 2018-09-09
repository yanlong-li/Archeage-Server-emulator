using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;
using LocalCommons.Cryptography;
using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons.Utilities;
using System;
using System.Linq;
using System.Security.Cryptography;
// ReSharper disable All

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
        private static string clientVersion;

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
            m_GHandlers = new PacketHandler<GameConnection>[0x20];
            m_LHandlers = new PacketHandler<ArcheAgeConnection>[0x30];

            Registration();
        }

        //для теста
        private static void ViewDecodeDD05()
        {
            string msg = "";
            msg = "2901DD05B5764616E6B6877711DFC872642E90A5FA714111E2FE7DADDDF7C297623300D4A4E8CC6B225090C069F33D3D251E52993DED3084D219407F7D5B49F8056AFBBD196F3FDC3C651CB6981BE5A34F7E4D3AB0A6CDC5C2DCB7718CC5CFD3E678F09E1083C394B97ED9F6C60678368A3ADD3014B0AFA10D2B479013A2C700E308E29D41F4A3D942D2AC2E53BD5564EC6BA609B4CF477FF47976BF0D1AE78D4C54B71F5796663707D7A7774010E0B0815121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704110E16E8FDAD0C0F7";
            ViewDecode2(msg);
            msg = "1400DD05AFE722865627F6CF97087265899FE9242175";
            ViewDecode2(msg);
            msg = "5000DD055F20C6C282625271B0CB11257381D3E43840DBA6F90B2C06A99043486EEFCD4B6745F31F35E70901D0441D85A978EE825322F4C494643405D5A5754517E7B7875627F7C797704010E0B0115081B0";
            ViewDecode2(msg);
            msg = "A900DD052893A332FFE0A119356592C1B87D0D80A6E0105A6BBA8A10367483C1AB3C4882A3F1145673BEC8196E6492D9EE3F4690ACF7111C72A0CA052A138BC0F02457CEEABA044766BEC3172173C9D3F536418AFEC91E347486C3E0254B9DACBC16416ABCCD13257981C7AB25579CB3B905596BBBC2052472C8C0F5224398A0E2041E62A0C7162B66909CF3265197ACF5175128B6D710217F92C5AB314B98B0B911236489DFEF51E71747";
            ViewDecode2(msg);
            msg = "2A00DD05AD65DD01D2A2724212E3B3835323F4C494643405B5F1754516E6B6865727F7C797704010E0B08151";
            ViewDecode2(msg);
            msg = "7700DD05B397E33000EEA3724212E2B4845524F4C595643505D7A6764616E7B7875767BED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C49465340AC6A5754566A4B3865727F7C781348DDCAC8F8B99F2";
            ViewDecode2(msg);
            msg = "0900DD051FB63B51104170";
            ViewDecode2(msg);
            msg = "1C00DD054B63BE04D5AF754516E6B9895827F7C897704010E0B0815EC4F4";
            ViewDecode2(msg);
            msg = "0E00DD055A2F3BC697704010E0B08151";
            ViewDecode2(msg);
            msg = "0900DD05FAB9B151114270";
            ViewDecode2(msg);
            msg = "0E00DD054F2D58C697704010E0B08151";
            ViewDecode2(msg);
            msg = "2300DD05C5E87C815223F4C494643405D5A5754516E6B6865727F7C797704010E0B0815142";
            ViewDecode2(msg);
            msg = "0A00DD05357CDC12E0B08151";
            ViewDecode2(msg);
            msg = "0700DD05D7BDF35310";
            ViewDecode2(msg);
            msg = "2A00DD051E6FDD01D3A2724213E3B3835323F4C4946434053B833B2816E6B6865727F7C797704010E0B08151";
            ViewDecode2(msg);
            msg = "FE00DD050DF94A96663707D7A7775020F0C090613101D1A1724212E2B2835323F3C494643404D5A5754515E6B6865626F7C7976737FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C596663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393633304D4A4744415E5B5855526F6C696663707D7A7774010E0B0815121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704010E0B08151";
            ViewDecode2(msg);
            msg = "0F00DD050F37BAC697704010E0B0815186";
            ViewDecode2(msg);
            msg = "0209DD05F8059DB48556F261C495603654B3CB183376E4B591B032FED03F734111A9B382524AF2C393633303D4A4744414E5B5D1D53AF79DCD6636A22DA6724710E0B0805120F1C191636702D2A2734313E3B3845424F4C495653505D5A6764616E6B7875727F7D0A0704010E1F8BCF47BF2C292623303D3A3734414E4B5845525F5C595663606D6A6774717E7B7805020F0C09161315D8AA272E4E8E3B6835323F3C494653404D5A4334516E6B6865627F7C797674010E0B0805121F1C191623202D2A2734313E3B3845424BDF9303F3505D5A6764616E6B7875727F6C090603001D1A1714112E2B2825223F3C393633404D4A4741B4EE5B522AC27F3C696673707D7A6805020F1E391613101D1A2724212E2B3835323F4C494643405D5A5754516E6B6865627F7C7976779C375FA704111E1B1815222F2C292633203D3A3744414E4B4855525F5C696663606D7A7774717F0C051753001795B704412E2B2825223F2C393633586D4A4744515E5B5855626F6C696673707D7A7704010E0B1815121F1C292623202D3A33A7EB6BEB4845424F5C595653506D6A6774617E7B7875730FED0A0704111E1B1815222F2C292633303DBBC7444BD1EB4805525F5C696663706D7A776DC17E0B0805020F1C191613102D2A2724213E3B3835324F4C494643505D5A5754616E6FFBAF27DF7C7A0704010E0B1815121F1C392623202D3A3734313E4B4845424F5C595653506D6A67646F8F0B787FDDAF1C590603101D1A1734212E2B3015323F3C394643404D4A5754515E5B6865626F6C797673707E0B0805020F1C19161314BEF07284213E3B3835424F4C494653504D5A5764616E6B6875727F7C7906030FED0A1714111E1B282733AF2C338993206D4A4744414E5B4855525F74496663707D7A7775020F0C090613101D1A1724212E2B2835323F3C394643404D4A5750C2840EC865626F6C797673707D0A0704110E1B1815122F2C292623303D3A3734414E4B4845525F5C5066736065E3B774736E5B7906030FED0A07C4C11E1B2824A57E1C19363330374E94147DF389DC15626F6C696663707D7977E47EFE0B080AE21F1C191623202D2FD734313E3B3845424F4C495653585EAA6764616E7B7076827FE509F704011E1B1815122A6C29967CF07E7A6764B12EBB2845525F5C595E60906D6264847176788A90CAFCEC091E10E01D2224D4212638C286D1B0B9BCF41CB5A8E808ADF18E849865627F74797674000E0B07151002C0DD158390929A173B5E62CBF848524B3C59EA9A905D6A676AE16E7B7878E98F0C090603001D1AD8B5F12F0538E521DD2A7B040DA12D4A474AD15FD5D3CBE26D4F1969B3707D7B04F9C20F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865627F7C7976730FED0A0704111E1B10941029CB6C8633303D3A47457958EEE855525F5C69666361556CD2C4717F0C09060300179921E1A12E2B2825223F3C393633404D4A4754515E5B5865626F6C697673707D7A0704010E0B1815121F1C292623202D3A3734313E4B4845424F5C58B653506D6A6764617E7B7876030FED0A0714111E1B1825222F3C293633203D3A4744414E4D6E8217EF5C696663606D7A777D1F0E1B086506494B7F40D5E03D0A3A24113E32D815424D8C594655005D5A5764616E6B6875727F7C7F4F05F11BBEA81519132A994623202D3A3734213E4B4850124F5C595653606D6A6764717E7B7875020F0C090613101D1A1724212E2B2835323F35B14496F04D4A5754515E5B6865626F6C697673707E0B0805021F1C191613202D2A2724313E3B3835478AFC494D4F66ED3A5764616E6B6865727F7C6D66030FED0A1714111E1B2825222F3C393633304D4A4744415E5B5855526F6C69666379F578A2C5020F0C090613101D1A1724213E2B2835323F3C394643404D4A5754515E6B6865626F7C797396CFED012B32B17E1B1815122F2C392623302F0A3734414E4B4845525F5C595663606D6A6774717E7B7906030FED1A1714111E2B282CAA2DE9893633304D4A4744415E5B5855426F6C696663707D7A7774010E0B0805121F1C191623202D2636643135077EF5224F4C595653504D6A6764794E7B7875727FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C50EE61B5DD6A7774717E7B0805020F0C190613101D2A2724212E3B3835323F4C494643405D5A5754516EEAE86569334AC91674010E0B0815021F1C193F93202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727F0C09060300149215C1A12E2B2825223F3C393643404D5A4754515E5B5865626F6C697673707D7B0805020F0C19161DE16D2A2C78179E5B3835323F4C495643405D427754516E6B6865727F7C7976030FED0A0714111E1B1825222F2C293633303D3A474448C6498DE5525F5C696663606D7A7774716F0C090603001D1A1714212E2B2825323F3C393643404D4A4545D15E503453D20F6C697673707D6A070401162B1815121F1C292623202D3A3734314E4B4845425F5C595653606D6A6764717E7B787F8B0D38BA0714111E1B1825222F2C293623303D3A4744414E4B5855525F5C696663607D7A7774710E0CFCD5021EDA291612900D2A2724313E3B28E5924F444A93B3102D5A5768203814FA657EC5608E2738B00E0B1815121F2C292623203D3A3734314E4B4845425F5C195653606D6A6764717E7B78750A0CFC090613101D1214D4212628D835323F3C394643400D51974EF15E6B6865626F7C79767B72FE0B0006F21F141AEEEF037EDA272C32CE3B3036B24F444AB864E8E2A0A9AA9E9495B68A87FBFF86F6030FED1217141FEE25D82BC22F2C093633304D4A4654415E5B585AB26F0FD2F66EF0739A7A35008EB2B7161D6012195514212E2B3833723F3C494643404B1A57545E5E6B6865626F7C7BCB237FED0C4704011E14881512212C292623303D3A3734414E4B4855525F5C596663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393EB2024BAD02F4415E5B585552977ACCD663707D7A7704010EF31EB0A21F1C192623202D2B01F3248E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292633303D3A3744414E4B4855525F5C596663606D6A7694717E7B0805020F0C191613101D2A2724212E3B3835323F4C594643504D5A5754616E6B6E53A53ACC797704010E0B08151";
            ViewDecode2(msg);
            msg = "2400DD0535F1FC825223F4C495643405D55A754516E634B7D47DF7C797704010E0B081514272";
            ViewDecode2(msg);
            msg = "1D00DD05CF7771040231744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423";
            ViewDecode2(msg);
            msg = "1D00DD05247071044342744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423";
            ViewDecode2(msg);
            msg = "1D00DD05F77171040231744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423";
            ViewDecode2(msg);
            msg = "1D00DD05E07271044342744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423";
            ViewDecode2(msg);
        }
        ///для теста
        private static void ViewDecode3(string message)
        {
            //шлем дешифрованный пакет
            string msg1 = message;
            //uint cookie = 0x12345678;
            Logger.Trace("Encode: " + msg1);
            string msg2 = msg1.Substring(8, msg1.Length - 8);
            byte[] cipherbytes = Utility.StringToByteArray(msg2);
            //cipherbytes = Encryption.CtoSDecrypt(cipherbytes, cookie);
            string msg3 = Utility.ByteArrayToString(cipherbytes);
            msg2 = msg3;
            msg3 = msg1.Substring(0, 8) + msg3;
            Logger.Trace("Decode: " + msg3);
        }
        private static void ViewDecode2(string message)
        {
            //шлем дешифрованный пакет
            string msg1 = message;
            Logger.Trace("Encode: " + msg1);
            string msg2 = msg1.Substring(8, msg1.Length - 8);
            byte[] cipherbytes = Utility.StringToByteArray(msg2);
            cipherbytes = Encrypt.StoCEncrypt(cipherbytes);
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

            #region MyTestDecrypt

            //для теста


            //uint xorKey = 0xFFC54C94; //xor_key
            //xorKey = xorKey * xorKey & 0xffffffff; //TODO: найти откуда берется!!!;
            //byte[] aesKey = Utility.StringToByteArrayFastest("A9B3F6A8A7D53C7CB525EACA39FAF3E8"); //TODO: найти откуда берется!!!;
            //byte[] iv = new byte[16]; //GenerateIv(); //для дешифрации первого пакета iv = 16 нулей 
            ////для дешифрации следующих пакетов iv = шифрованный предыдущий пакет

            //string msg = "13000005396D07058A4B4A74C112C53F02B73B9362";
            //Logger.Trace("Encode: " + msg);
            //byte[] ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //byte[] plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:0005394250000100D79401003DA500003EA500");
            //Logger.Trace("");

            //msg = "130000053935561344F2D4C469AE603C48832F928F";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000539EB50000200D7940100C53F02B73B9362");
            //Logger.Trace("");

            //msg = "13000005392172A774B0F3173687662E0493E7E90C";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000539B35000010096E70100603C48832F928F");
            //Logger.Trace("");

            //msg = "130000053911F039B5BF1DF3392ED41C165A1DEEF8";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000539D05000020096E70100662E0493E7E90C");
            //Logger.Trace("");

            //msg = "13000005374F2AEF0280CD97646F5B0904430D48BE";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000537A8370001000000000008CC000009CC00");
            //Logger.Trace("");

            //msg = "1300000533376E7E67D38C49005DD3FF57A05EE4EB";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:00053344C90000000000000000000000000000");
            //Logger.Trace("");

            //msg = "1300000533FC2748235DA49344FB75FDF3E4F5285D";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:00053338C90067D38C49005DD3FF57A05EE4EB");
            //Logger.Trace("");

            //msg = "1300000533AF694ABEFAD0EB7C2F79E2D9C518187B";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:00053378C900235DA49344FB75FDF3E4F5285D");
            //Logger.Trace("");

            //msg = "230000053541559E45C16351ABF12AE2C29E2420FF3F2CDF8829C1AC48213B1110748F2033";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000535C222000100000000000000000000000003006E6F700000000000000000000000");
            //Logger.Trace("");

            //msg = "130000053CEABB810C7BB12C61A5E29BCF0E59677A";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:00053CA822000206000000D107000000000000");
            //Logger.Trace("");

            //msg = "1300000533A905B9F086F7BF558FA02F8A407B23E1";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:00053371C90000000000000000000000000000");
            //Logger.Trace("");

            //msg = "1300000533C805E365D177F701A4ECFD588EDCFB0A";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000533F1C900F086F7BF558FA02F8A407B23E1");
            //Logger.Trace("");

            //msg = "130000053354AE7043AC5346FCFCCA456A2637409E";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:0005338BC90065D177F701A4ECFD588EDCFB0A");
            //Logger.Trace("");

            //msg = "130000053389471F9594C20348D03781E0578EB5E4";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:00053344C90043AC5346FCFCCA456A2637409E");
            //Logger.Trace("");

            //msg = "2300000535E0DA040401CAAE7DEC8172617DF059AAB32F5732E6918992D08186DB298050D8";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:000535F822000100000000000000000000000003006E6F700000000000000000000000");
            //Logger.Trace("");

            //msg = "130000053CF81D474D0E3922D20CE20D8EBEBA566A";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:00053CA022000207000000D20700000E59677A");
            //Logger.Trace("");

            //msg = "1300000533A7B5DF1C6D45D056BB0C4F87046C2BFF";
            //Logger.Trace("Encode: " + msg);
            //ciphertext = Encrypt.CtoSEncrypt(Utility.StringToByteArrayFastest(msg), xorKey);
            //Logger.Trace("DecodeXOR:      " + Utility.ByteArrayToString(ciphertext));
            //Logger.Trace("IV:             " + Utility.ByteArrayToString(iv));
            //plaintext = Encrypt.DecryptAes(ciphertext, aesKey, iv);
            //Logger.Trace("DecodeAES:      " + Utility.ByteArrayToString(plaintext));
            //Logger.Trace("Должно быть:0005334BC90000000000000000000000000000");
            //Logger.Trace("");

            //расшифровать пакет DD05
            //ViewDecodeDD05();

            //RunSnippet();

            #endregion

            //------------------------------------------------------------------------------------------------
            //Game Server Delegates Packets
            //------------------------------------------------------------------------------------------------
            Register(0x00, Handle_RegisterGameServer);//Level registration server
            Register(0x02, Handle_UpdateCharacters);//Level registration server
            //------------------------------------------------------------------------------------------------
            //END Game Server Delegates Packets
            //------------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------------
            //Client Delegates Packets
            //------------------------------------------------------------------------------------------------
            switch (clientVersion)
            {   
                case "1"://1.0.1406 Feb 11 2014
                    //последовательность пакетов при подключении клиента к LoginServer
                    //Register(0x01, Handle_CARequestAuthPacket_0x01);
                    //Register(0x00, Handle_ACJoinResponsePacket_0x00);
                    //Register(0x04, Handle_ACChallendge2Packet_0x04);
                    //Register(0x06, Handle_CAChallendgeResponse2Packet_0x06);
                    //Register(0x03, Handle_ACAuthResponsePacket_0x03);
                    //Register(0x0A, Handle_CAListWorldPacket_0x0A);
                    //Register(0x08, Handle_ACWorldListPacket_0x08);
                    //Register(0x0B, Handle_CAEnterWorldPacket_0x0B);
                    //Register(0x0A, Handle_ACWorldCookie_0x0A);
                    //-------------------------------------
                    Register(0x01, Handle_CARequestAuthPacket_0x01);
                    Register(0x04, Handle_CARequestAuth_0x04); //пакет №1 от клиента
                    Register(0x0A, Handle_CAListWorld_0x0A);  //
                    Register(0x0B, Handle_CAEnterWorld_0x0B);  //
                    Register(0x0D, Handle_CARequestReconnect_0x0D);
                    break;
                case "3": //3.0.3.0
                    Register(0x06, Handle_CAChallengeResponse2_0X06); //пакет №1 от клиента
                    Register(0x0c, Handle_CACancelEnterWorld_0X0C); //пакет №2 от клиента
                    Register(0x0d, Handle_CARequestReconnect_0X0D); //пакет №3 от клиента
                    Register(0x0f, Handle_CARequestReconnect_0X0F); //пакет №3 от клиента
                    break;
                default:
                    Register(0x06, Handle_CAChallengeResponse2_0X06); //пакет №1 от клиента
                    Register(0x0c, Handle_RequestServerList); //Return to server list<=2.9
                    Register(0x0d, Handle_ServerSelected);//Return server address based on server id
                    break;
            }
            //------------------------------------------------------------------------------------------------
            //END Client Delegates Packets
            //------------------------------------------------------------------------------------------------
        }

        #region Game Server Delegates
        //используется
        private static void Handle_UpdateCharacters(GameConnection net, PacketReader reader)
        {
            long accountId = reader.ReadLEInt64();
            byte characters = reader.ReadByte(); //количество чаров на аккаунте
            Account currentAcc = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            currentAcc.Characters = characters;
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
        }
        #endregion

        #region Client Delegates

        #region Version1.0

        private static void Handle_CARequestAuthPacket_0x01(ArcheAgeConnection net, PacketReader reader)
        {
            /*
             Скорее всего это пакет от клиента на логинсервер

            CARequestAuthPacket_0x01
            Size Id   p_from   p_to     svc dev account_len account(aatest) mac_len mac              mac_len2 mac2             cpu
            3000 0100 0A000000 07000000 00  00  0600        616174657374    0800    0000000000000000 0800     E839DF54605F0000 52060200FFFBEBBF
                                                ^
             */

            reader.Offset += 10; //скипаем 10 байт
            int m_RUidLength = reader.ReadLEInt16(); //длина строки
            string m_Uid = reader.ReadString(m_RUidLength); //считываем ID
            //long accId = Convert.ToInt64(m_Uid);
            int m_RtokenLength = reader.ReadLEInt16(); // длина строки
            //string m_RToken = reader.ReadHexString(m_RtokenLength); //считываем токен
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_Uid);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                //account numberexist
                //if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                    {
                        //Удалим результаты предыдущего коннекта для нормального реконнекта
                        GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    }
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }
                //Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " token verification failed：" + m_RToken.ToLower());
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + m_Uid);
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Logger.Trace("Create new account: " + m_Uid);
                    Account m_New = new Account
                    {
                        AccountId = AccountHolder.AccountList.Count + 1,
                        LastEnteredTime = Utility.CurrentTimeMilliseconds(),
                        AccessLevel = 1,
                        LastIp = net.ToString(),
                        Membership = 1,
                        Name = m_Uid,
                        //Password = "change_password_now", //заглушка
                        Token = "m_RToken", //заглушка
                        Characters = 0
                    };
                    net.CurrentAccount = m_New;
                    AccountHolder.InsertOrUpdate(m_New);
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + net.CurrentAccount.AccountId + " & Account Name: " + net.CurrentAccount.Name + " landing success");
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }

                net.CurrentAccount = null;
                Logger.Trace("Сan not create account: " + m_Uid);
            }
            //If the front did not terminate, then the account number failed to log in
            net.SendAsync(new NP_ACLoginDenied_0x0C());
        }

        /// <summary>
        /// для версии 2014 года
        /// </summary>
        /// <param name="net"></param>
        /// <param name="reader"></param>
        private static void Handle_CARequestAuth_0x04(ArcheAgeConnection net, PacketReader reader)
        {
            //3F00 0400 0A000000 0700000000 08000000000000000000 0600 616174657374200031E34F2B72D93BB25D5F27BE8A94C47800000000000000000000000000000000
            reader.Offset += 19; //скипаем 19 байт
            int m_RUidLength = reader.ReadLEInt16(); //длина строки
            string m_Uid = reader.ReadString(m_RUidLength); //считываем ID
            //long accId = Convert.ToInt64(m_Uid);
            int m_RtokenLength = reader.ReadLEInt16(); // длина строки
            string m_RToken = reader.ReadHexString(m_RtokenLength); //считываем токен
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_Uid);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                //account numberexist
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                    {
                        //Удалим результаты предыдущего коннекта для нормального реконнекта
                        GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    }
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                    //net.SendAsyncHex(new NP_Hex("0C00000000000300000000000000"));
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    //net.SendAsyncHex(new NP_Hex("280003005833000020003236393631326537613630393431313862623735303764626334326261353934"));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    //net.SendAsyncHex(new NP_Hex("0C00000000000300000000000000"));
                    //net.SendAsyncHex(new NP_Hex("0C00000000000600000000000000"));
                    //    000000000600000000000000
                    //0C00000000000300000000000000
                    //03005833000020003236393631326537613630393431313862623735303764626334326261353934
                    return;
                }
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name +
                             " token verification failed：" + m_RToken.ToLower());
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + m_Uid);
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Logger.Trace("Create new account: " + m_Uid);
                    Account m_New = new Account
                    {
                        AccountId = AccountHolder.AccountList.Count + 1,
                        LastEnteredTime = Utility.CurrentTimeMilliseconds(),
                        AccessLevel = 1,
                        LastIp = net.ToString(),
                        Membership = 1,
                        Name = m_Uid,
                        //Password = "change_password_now",
                        Token = m_RToken,
                        Characters = 0
                    };
                    net.CurrentAccount = m_New;
                    AccountHolder.InsertOrUpdate(m_New);
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + net.CurrentAccount.AccountId + " & Account Name: " + net.CurrentAccount.Name + " landing success");
                    net.SendAsyncHex(new NP_Hex("0C00000000000300000000000000"));
                    //net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsyncHex(new NP_Hex("280003005833000020003236393631326537613630393431313862623735303764626334326261353934"));
                    //net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }

                net.CurrentAccount = null;
                Logger.Trace("Сan not create account: " + m_Uid);
            }
            //If the front did not terminate, then the account number failed to log in
            net.SendAsync(new NP_ACLoginDenied_0x0C());
        }
        private static void Handle_CAListWorld_0x0A(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsync(new AcWorldList_0X08(clientVersion, net));
            //net.SendAsyncHex(new NP_Hex("A802080018010A00D09BD183D186D0B8D0B90101000200000202020000020E00D09AD0B8D0BFD180D0BED0B7D0B00101000200000202020000031000D09CD0B5D0BBD0B8D181D0B0D180D0B00101000200000202020000040800D0A2D0B0D18FD0BD0101000200000202020000051200D090D180D0B0D0BDD0B7D0B5D0B1D0B8D18F0101000200000202020000060800D09ED0BBD0BBD0BE0101000200000202020000070800D090D0BDD0BDD0B00101000200000202020000080E00D090D180D0B0D0BDD0B7D0B5D0B10101000200000202020000090800D098D0BDD0BED18501010002000002020200000A0800D094D0B6D0B8D0BD01020000000000000000000B0E00D09ED180D185D0B8D0B4D0BDD0B001010000000000000000000C0A00D09DD0B0D0B8D0BCD0B001010000000000000000000D1000D090D0BDD182D0B0D0BBD0BBD0BED0BD01010002000002020200000E0E00D0A8D0B0D182D0B8D0B3D0BED0BD01010002000002020200000F0800D090D0B9D18DD1800101000200000202020000101000D0A1D0B0D0BBD18CD184D0B8D180D0B00102000000000000000000110A00D094D0B0D183D182D0B00101000000000000000000120E00D09AD0B0D0BBD0B5D0B8D0BBD18C0101000000000000000000130C00D09AD0B8D180D0B8D0BED1810101000000000000000000140E00D090D0BAD180D0B8D182D0B5D1810101000000000000000000150C00D0ADD0BDD188D0B0D0BAD0B00101000000000000000000160E00D090D188D0B0D0B1D0B5D0BBD18C0101000000000000000000170E00D09AD0B0D0BFD0B0D0B3D0B0D0BD0101000000000000000000180A00D09DD0B5D0B2D0B5D1800102000000000000000000018FA90D000BFF091A000B004A757374746F636865636B010210000E4FC3755AE17949B1F626620F354A930000000000000000"));
        }
        private static void Handle_CAEnterWorld_0x0B(ArcheAgeConnection net, PacketReader reader)
        {
            //net.SendAsyncHex(new NP_Hex("13000A008D0EC89A0E003132372E302E302E31D704"));
            //0B00 0D00 00000000 00000000 01
            int p_from = reader.ReadLEInt32();
            int p_to = reader.ReadLEInt32();
            byte serverId = reader.ReadByte(); //serverId
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
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
                    net.MovedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    //отсылаем Гейм серверу информацию об аккаунте
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(clientVersion, net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    //отсылаем Клиенту информацию о куках
                    net.SendAsync(new AcWorldCookie_0X0A(clientVersion, server, cookie));
                }
            }
            else
            {
                Logger.Trace("No serverID requested：" + serverId);
                net.Dispose();
            }
        }

        /// <summary>
        /// Обрабатываем приход пакета из Лобби "Выбор сервера"
        ///</summary>>
        private static void Handle_CARequestReconnect_0x0D(ArcheAgeConnection net, PacketReader reader)
        {
            //1.0.1406
            //     opcode p_from   p_to     accountId wid cookie   len  mac     
            //1D00 0D00   0A000000 07000000 1AC70000  01  091F831D 0800 0000000000000000

            //3.0.3.0
            int p_from = reader.ReadLEInt32();
            int p_to = reader.ReadLEInt32();
            long accountId = reader.ReadLEInt32();
            byte wid = reader.ReadByte();
            int cookie = reader.ReadLEInt32();

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                net.CurrentAccount = n_Current;
                //Write account number information Write Online account list
                GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + accountId);
            }
        }

        #endregion

        #region Version3.0

        /// <summary>
        /// 0x06_CAChallengeResponse2Packet - token Verification mode
        /// uid+token
        /// </summary>
        private static void Handle_CAChallengeResponse2_0X06(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset += 19; //скипаем 19 байт
            int m_RUidLength = reader.ReadLEInt16(); //длина строки
            string m_Uid = reader.ReadString(m_RUidLength); //считываем имя "aatest"
            int m_RtokenLength = reader.ReadLEInt16(); // длина строки
            string m_RToken = reader.ReadHexString(m_RtokenLength); //считываем токен
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_Uid);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                //account numberexist
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                    {
                        //Удалим результаты предыдущего коннекта для нормального реконнекта
                        GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    }
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name +
                             " token verification failed：" + m_RToken.ToLower());
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + m_Uid);
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Logger.Trace("Create new account: " + m_Uid);
                    Account m_New = new Account
                    {
                        AccountId = AccountHolder.AccountList.Count + 1,
                        LastEnteredTime = Utility.CurrentTimeMilliseconds(),
                        AccessLevel = 1,
                        LastIp = net.ToString(),
                        Membership = 1,
                        Name = m_Uid,
                        //Password = "new_password",
                        Token = m_RToken,
                        Characters = 0
                    };
                    net.CurrentAccount = m_New;
                    AccountHolder.InsertOrUpdate(m_New);
                    //Write account number information Write Online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account ID: " + net.CurrentAccount.AccountId + " & Account Name: " + net.CurrentAccount.Name + " landing success");
                    net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                    net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
                    return;
                }

                net.CurrentAccount = null;
                Logger.Trace("Сan not create account: " + m_Uid);
            }
            //If the front did not terminate, then the account number failed to log in
            net.SendAsync(new NP_ACLoginDenied_0x0C());
        }

        private static void Handle_CACancelEnterWorld_0X0C(ArcheAgeConnection net, PacketReader reader)
        {
            //var unknown = reader.ReadByteArray(8); //unk?
            net.SendAsync(new AcWorldList_0X08(clientVersion, net));
            //net.SendAsync(new AcAccountWarned_0X0D(clientVersion)); //не обязателен
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
            int p_from = reader.ReadLEInt32();
            int p_to = reader.ReadLEInt32();
            byte serverId = reader.ReadByte(); //serverId
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
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
                    net.MovedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    //отсылаем Гейм серверу информацию об аккаунте
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(clientVersion, net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    //отсылаем Клиенту информацию о куках
                    net.SendAsync(new AcWorldCookie_0X0A(clientVersion, server, cookie));
                }
            }
            else
            {
                Logger.Trace("No serverID requested：" + serverId);
                net.Dispose();
            }
        }

        /// <summary>
        /// Обрабатываем приход пакета из Лобби "Выбор сервера"
        ///</summary>>
        private static void Handle_CARequestReconnect_0X0F(ArcheAgeConnection net, PacketReader reader)
        {
            /*
             [1]             C>s             0ms.            14:01:18 .890      21.07.18
             -------------------------------------------------------------------------------
              TType: ArcheageServer: LS1     Parse: 6           EnCode: off         
             ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
             000000 21 00 0F 00 0A 00 00 00 | 08 00 00 00 01 00 00 00     !...............
             000010 00 00 00 00 01 09 1F 83 | 1D 08 00 00 00 00 00 00     .......ƒ........
             000020 00 00 00                                              ...
             -------------------------------------------------------------------------------
             Archeage: "CARequestReconnect"               size: 35     prot: 2  $002
             Addr:  Size:    Type:         Description:     Value:
             0000     2   word          psize             33         | $0021
             0002     2   word          ID                15         | $000F
             0004     4   integer       p_from            10         | $0000000A
             0008     4   integer       p_to              8          | $00000008
             000C     8   int64         accountId         1          | $00000001
             0014     4   integer       cookie            -2095118079 | $831F0901
             0018  2079   WideStr[byte] MAC               00:00:00:00:00:00:00:00:00  ($)
             */

            int p_from = reader.ReadLEInt32();
            int p_to = reader.ReadLEInt32();
            long accountId = reader.ReadLEInt64();
            int cookie = reader.ReadLEInt32();

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            if (n_Current != null)
            {
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " is landing");
                net.CurrentAccount = n_Current;
                //Write account number information Write Online account list
                GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                Logger.Trace("Account ID: " + n_Current.AccountId + " & Account Name: " + n_Current.Name + " landing success");
                net.SendAsync(new AcJoinResponse_0X00(clientVersion));
                net.SendAsync(new AcAuthResponse_0X03(clientVersion, net));
            }
            else
            {
                Logger.Trace("Client try to login to a nonexistent account: " + accountId);
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
                    Account m_New = new Account
                    {
                        AccountId = AccountHolder.AccountList.Count + 1,
                        LastEnteredTime = Utility.CurrentTimeMilliseconds(),
                        AccessLevel = 0,
                        LastIp = net.ToString(),
                        Membership = 0,
                        Name = m_RLogin
                    };
                    net.CurrentAccount = m_New;
                    AccountHolder.AccountList.Add(m_New);
                }
                else
                {
                    net.CurrentAccount = null;
                }
            }
            else
            {
                net.CurrentAccount = n_Current;
            }
            // net.SendAsync(new NP_PasswordCorrect(1));
            net.SendAsync(new NP_ServerList(clientVersion));
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
        private static void Handle_Token_Continue(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset = 21;
            int m_RUidLength = reader.ReadLEInt16();
            string m_uid = reader.ReadString(m_RUidLength); //Reading Login
            int m_RtokenLength = reader.ReadLEInt16();
            string m_RToken = reader.ReadHexString(m_RtokenLength);
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == Convert.ToInt64(m_uid));
            if (n_Current != null)
            {
                Logger.Trace("account number: < " + n_Current.AccountId + ":" + n_Current.Name + "> is landing");
                //accounts exist
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    //Write account information to online account list
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("Account: < " + n_Current.AccountId + ":" + n_Current.Name + "> landing success");
                    net.SendAsync(new NP_AcceptLogin(clientVersion));
                    net.SendAsync(new NP_03key(clientVersion));
                    //return server list
                    //net.SendAsync(new NP_ServerList());
                    return;
                }
                Logger.Trace("Account: < " + n_Current.AccountId + ":" + n_Current.Name + "> token verification failed: " + m_RToken.ToLower());

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

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == 1);
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
                    //Random random = new Random();
                    //int num = random.Next(255) + random.Next(255) + random.Next(255) + random.Next(255);
                    //net.CurrentAccount.Session = num= 1323126619;//Specify session

                    // генерируем cookie
                    Random random = new Random();
                    int cookie = random.Next(255);
                    cookie += random.Next(255) << 8;
                    cookie += random.Next(255) << 16;
                    cookie += random.Next(255) << 24;
                    net.CurrentAccount.Session = cookie; //Designated session

                    net.MovedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(clientVersion, net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    net.SendAsync(new NP_SendGameAuthorization(server, cookie));
                }
            }
            else
            {
                Logger.Trace("Requested a non-existent server ID:" + serverId);
                net.Dispose();
            }
        }

        #endregion
        #endregion

        private static void Register(ushort opcode, OnPacketReceive<ArcheAgeConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<ArcheAgeConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(ushort opcode, OnPacketReceive<GameConnection> e)
        {
            m_GHandlers[opcode] = new PacketHandler<GameConnection>(opcode, e);
            m_Maintained++;
        }
    }
}
