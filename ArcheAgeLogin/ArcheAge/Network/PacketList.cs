using LocalCommons.Cryptography;
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
            //msg = "1300000538DC1A5BEC83FDB4DAD75F9FEBB786DBCA";
            //ViewDecode3(msg);
            msg = "2800DD054EB437438404B2835223E2C4A3520230E297427620DF80C06162C5F7A4427054D0F18D510131";
            ViewDecode2(msg);

            //msg = "2901DD05D7764616E6B687C26B284D72649718E1FA714111E2FE7DADDDF7C297623300D4A4CF84FC802BA4312ABBAE04E1052DB5DB14E8DD6E2D3F6D089800E10B7B2DE0899E3820DE93DB4B4C7854E8E594223C4B15B3718919A2865E949010AC430E5062B2B11A5393F1B04B628C4CCA63497B09DFCCC28AAD05BEEC0224815138DA234771C1EC50639B6278877B01E329D76F5E6B39B25548F4E79209BAF13D5179182F96663707D7A7774010E0B0815121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704110E14CF2FF803FD8";
            //ViewDecode2(msg);
            //msg = "1000DD05412472C591704610E2B0815186B6";
            //ViewDecode2(msg);
            //msg = "2A00DD051565B903D3A2724213E3B3835323F4C494643405D5A5754516E6B6865727F7C797704010E0B08151";
            //ViewDecode2(msg);
            //msg = "7700DD053B97433200EEA3734311E1B4845624F4C595673505D5A6764616E7B78757A7C1D0A0300011E171C15122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C49565340AC6A5754566A4B3865727F7C7A3338DDCAC8F8BAEF0";
            //ViewDecode2(msg);
            //msg = "0900DD05AAB68353104070";
            //ViewDecode2(msg);
            //msg = "1C00DD054263A105D5AF754508E6B9895827F7C897704010EFB0815EC4F4";
            //ViewDecode2(msg);
            //msg = "0E00DD056E2FFCC797704010E0B08151";
            //ViewDecode2(msg);
            //msg = "0900DD0533B9D851114270";
            //ViewDecode2(msg);
            //msg = "0E00DD05B22D99C697704010E0B08151";
            //ViewDecode2(msg);
            //msg = "2300DD057BE854835223F4C494643405D5A5754516E6B6865727F7C797704010E0B0815142";
            //ViewDecode2(msg);
            //msg = "0A00DD05507C6E11E0B08151";
            //ViewDecode2(msg);
            //msg = "1200DD0525EB54846D98A29C97704010AC4F7EAE";
            //ViewDecode2(msg);
            //msg = "2A00DD05BF6FB903D3A2724213E3B3835323F4C494643405D5A5754516E6B686A8D80838688FBFEFE0B08151";
            //ViewDecode2(msg);
            //msg = "2A00DD05FB6EB903D7A2724213E3B3835323F4C494643405FEB0461D16E6B686A8D80838688FBFEFE0B08151";
            //ViewDecode2(msg);
            //msg = "1200DD05ECF6E5845626F7C796704010E0B08151";
            //ViewDecode2(msg);
            //msg = "0700DD05E1A1FA5010";
            //ViewDecode2(msg);
            //msg = "1D01DD05EB22EBD1A1714111E2B2825222F3C393633304D4A4744415E5B5855526F6C696663707D7A7775020F0C090613101D1A1724212E2B2835323F3C494643404D5A5754515E6B6865626F7C7976737FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C595663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393633304D4A4744415E5B5855526F6C696663707D7A7774010E0B0815121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704010E0B0815190C7F7";
            //ViewDecode2(msg);
            //msg = "1000DD05DB34CCC797704010E0B0815186B6";
            //ViewDecode2(msg);
            //msg = "0800DD0593A4EF531140";
            //ViewDecode2(msg);
            //msg = "6300DD05CDC3A5755616372756D2F746001BA0F5C154613480A3F2801B623303D3A3744414E4B48D5424F5C5966C1070D6A7774717E7B0805020F0C191613101D2A2724212E3F38EDE23F5C494643405D5A5754516E6B6865727F7C797704010E0B081563D";
            //ViewDecode2(msg);
            //msg = "1D00DD05A47071040231744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423";
            //ViewDecode2(msg);
            //msg = "1D00DD05777171044342744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423";
            //ViewDecode2(msg);
            //msg = "1D00DD05E07271044342744517E6BD86214285B4FE1F2E30D1BD8B5DC4F423";
            //ViewDecode2(msg);
            //msg = "0C00DD058367371137248051C6F7";
            //ViewDecode2(msg);
            //msg = "1000DD05543FC9C797704010E0B0815186B7";
            //ViewDecode2(msg);
            //msg = "1D00DD051C70B6070231744514E6BD86214285B4FE1F2E30D2BD8B5DC4F423";
            //ViewDecode2(msg);
            //msg = "1902DD05885CEEE5A8865626EBC697673606D7485140105E7D875D21F1C29289B302D3A173AD11E4B49BFDF8F6C5956536F557A6764417FD868760D30264BA714111E14A035222F6C2A75C3303BB728C7A14E4B5853EA7F5C69366821FD7A757A7A1EBB0805021A14091613402C2857243D6EFE2B05424F4C47BE43505D2A55C5416E6A9A16124F7C7A070E392E0B189514BF5C292086F67D1A37343144D3684542CF579BF65368E58C9524717E7B750D320F0CA900D1101D1A0714E00E2B283536677C3946F34E4CEA5752EC00CA6865626F71C1467371CE06AA55021734C5C743202D2A2A0C213E3BE830A2FF4C4C6254503D5A576465D62B6875927C7C29060EF1BA2A0714111E252015222FCC32F753304F2A4176715E5B585ADA5F6C68666511BD7A79BE9EFC0C0906131FC52A1725312A0948353DF4061BB643404D5C0F44515F4B69A4D26F73B31993FFED0A070E992E1B19251F6F1C292A1EB86D1A3734414DE36845531F565B56636C75AE0594717E7C03FE330FEC4A13B6811E2D1370A32F3C39363DB80D4A46244E9EDB585E08CE5C2966637074A24774007E0C6975121FB5037753202D2A38CC713E3AC841004F4C5D852C813D6A67646AE64B7874E2738D7A070B0F4D7B2815122F23211623319D3856F4414A46D244325F5C596F8B406D6BC77FD30E7B0CA59BDE5C19161319651A2721F12B7B58353D7642C90643405D54EF1461685B67C0827F7D20BE97C10E0B081AB20D404";
            //ViewDecode2(msg);
            //msg = "9A06DD057A0B3DB0825222F2D393BF3E0F2877945C53489FD800CFB1DA49663607D770E34620F6C0C2045C6EA5C071401332B08353BDF1C3942C3504D4CD754515E5B6865626F6C79767375350BF711B4BE1B124AB20F7C292623203D3A2734414E5E1845525F5C595663606D6A6774717E7B7906030FED0A1714111E1B2825222F2C393637A3E71FE744414E5B5855525F6C696663607D7A7774010E0B0805121F1C191623202D2A2734313E3B3D80F24F4626F643005D5A6764616E7B7875726B1D0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C595663606D6A63E7AB2BDB7805020F0C091613101D1A2734212E2B3835323F4C494643405D5A5754516E6B6865627F799CC6740B71AB1855121F1C191623302D2A2726013E3B3845424F4C495653505D5A6764616E7B7875727F0C090603001D1A1714112E2FBBFF779F3C393633404D4A4744515E5B4855626F6C696673707D7A7805020F0C091613101D2A27242D3F6B383FBD9F5C194643405D5A5744516E6B7045627F7C7976730FED0A0704111E1B1815222F2C292633303D3A3744414E4B5855525BCFB333C3606D7A7774717F0C090603000D1A1714112E2B2825223F3C393633404D4A4744515E5B5855E3EF6C63E9D3602D7A0704010E0B0815121F05992623202D3A3734313E4B4845424F5C595653506D6A6764617E7B7875730FED0A070082C44EB825222F2C293633303D3A4744514E4B5855525F5C696663606D7A7774717E0B0805020F1C17E7631027858734713E3B3835324F5C494653487D5A5764616E6B6875727F7C7A0704010E0B1815121F1C292623202D3A3734313E4B4CD6981AFC595653506D6A6764617E7B7865020F0C090613101D1A1724212E2B2835323F3C394643404F5BD7545BE1FB7835626F6C797673607E0B081D220F1C191613202D2A2724313E3B3835424F4C494653505D5A5764616E6B6875727F78EADC56AFED0A1714111E1B2825222F2C293633303D4A4744415E5B5855526F6C696663707D7A77750B3F1C090E9AC01D180704212E2B2835322FECE946434047476604715E5B686E67AC4C4E3C61EBAD3A0704011E1B1815112FBC56D6233032CA3734414E4B484557AF5C595663606D6A6774717E7B7105F30FED0A1714191DEB282D21DF3C393633304D4A420441EE049816123F3C990693107D7A7774010608F8051A1CEC191E20D2B8E2D4D4313638C8454A4CBC495E50AAEE89E89194DC248D80C02014AAE7EBF11E1B181D122F2C2826233F2D382AE8854DEBF8FAE26F53360A93A0607A630471C2B2C805020F029916131010B1D724212E3B383532F0EDA9476D509D59B542135C558905627F72F977FA8A908B0A266210DC1916232CD1EA3734313E3B4845424F4C595653505D6A6764616E7B7875727F0C090603001D1A1714112E2B2825223F3C39363348CC7EA36164EE5B5865626F674F9546C07D7B0805020F0C1230F025AD2A2724212E3B3835E70CA9F94643405D5A5754516E6B6865627F7C7976730FED0A0714111E1B1825222F2C293633303D3A4744414E4B5855525F5C696663606D7A7774709F0C090603001D1A1714112E2B2825323F3C393643404D4A4744515E5B4865626F6C69767794584FB704010E0B1815121F1C292623202D3A373F0B5758B845424F5C595653606D6A6764717E7B7386030FE6EA0714111E1B1825222F2C293633303D3A4744414E4B5855525F5C696663606D7A7774717E0B0805021F1C191613202D2A2724313E3B3835424F4C494653505D5A5764616E6B6875727F7C7A0704010E0B1815113C3C292623202D3A3734314E4B4845425F5C595653606D6A6764717E7B7875020F0C090613101D1A1724212E2B2835323F3C394643404D4A5754515E5B6865626F7C797DA3510E0DBBDF57BF1C191613202D2A2724313E3B3835424F4C494653505D5A5764616E6B6875727F7C7906030FED1A1714111E2B2825222F3C393633304D4A4744415E5B5855526F6C696663707D7A7775020F0C090613101D1A172292F47E8835323F3C494643404D5A5754515E6B6865626F7C7976737FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C595663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393633304D4A4744415E5B5855526F6C696663707D7A7774010E0B0805121F1C192623202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292623303D3A3744414E4B4855525F5C596663606D6A7774717E7B0805020F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865727F7C797704010E0B08151";
            //ViewDecode2(msg);
            //msg = "0E00DD055D3C5EC7A5704010D2B08151";
            //ViewDecode2(msg);
            //msg = "BB00DD03749DA10EC2400C40BB4B98398222281C3F308FDA9D820FE985048B22412070083E034720906050640A83008543A0502C9B59C2DA2E4BCECCBF5C9B975E5B5780DACA6095012CB5B9860E77FAAB1C7C28D61B22DA5303E0E920843D9A798E544D77B5953B1EBE1521A6233349F1C1562EFF84DEF0AD08F1FAC5598AFD83EEAA22A87E50049C0D5B11E2969B59A315214E2DBB5863B2D763AA950D45F1AD08D1EE9967A31521C8CA397434FF3B600BEE06310C258F120000FFFF";
            //ViewDecode2(msg);
            //msg = "0700DD05DA0F635010";
            //ViewDecode2(msg);
        }
        ///для теста
        private static void ViewDecode3(string message)
        {
            ////шлем дешифрованный пакет
            string msg1 = message;
            uint cookie = 0x15A0243D;
            Logger.Trace("Encode: " + msg1);
            string msg2 = msg1.Substring(8, msg1.Length - 8);
            byte[] cipherbytes = Utility.StringToByteArray(msg2);
            cipherbytes = Encryption.CtoSDecrypt(cipherbytes, cookie);
            string msg3 = Utility.ByteArrayToString(cipherbytes);
            msg2 = msg3;
            msg3 = msg1.Substring(0, 8) + msg3;
            Logger.Trace("Decode: " + msg3);
        }
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
            Register(0x02, new OnPacketReceive<GameConnection>(Handle_UpdateCharacters));//Level registration server
            //------------------------------------------------------------------------------------------------
            //END Game Server Delegates Packets
            //------------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------------
            //Client Delegates Packets
            //------------------------------------------------------------------------------------------------
            if (clientVersion == "3")
            {   //ver.3.0
                //Register(0x01, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuth_0x01));  //
                //Register(0x02, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuthTencent_0x02));  //
                //Register(0x03, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuthGameOn_0x03));  //
                //Register(0x04, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestAuthMailRu_0x04));  //
                //Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_CAChallengeResponse_0x05));  //
                //Register(0x09, new OnPacketReceive<ArcheAgeConnection>(Handle_CAPcCertNumber_0x09));  //
                //Register(0x0A, new OnPacketReceive<ArcheAgeConnection>(Handle_CAListWorld_0x0A));  //
                //Register(0x0B, new OnPacketReceive<ArcheAgeConnection>(Handle_CAEnterWorld_0x0B));  //
                Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_CAChallengeResponse2_0X06)); //пакет №1 от клиента
                Register(0x0c, new OnPacketReceive<ArcheAgeConnection>(Handle_CACancelEnterWorld_0X0C)); //пакет №2 от клиента
                Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestReconnect_0X0D)); //пакет №3 от клиента
                Register(0x0f, new OnPacketReceive<ArcheAgeConnection>(Handle_CARequestReconnect_0X0F)); //пакет №3 от клиента
            }
            else
            {   //ver.4.0
                Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_CAChallengeResponse2_0X06)); //пакет №1 от клиента
                Register(0x0c, new OnPacketReceive<ArcheAgeConnection>(Handle_RequestServerList)); //Return to server list<=2.9
                Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected));//Return server address based on server id
            }
            ////Register(0x01, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //Account login service
            ////Register(0x03, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue2)); //Token verification service wegame China
            ////Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue));  //Token authentication service -r mode
            ///Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue));  //Token authentication service -r mode service
            //////Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //Boarding landing service
            //////Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_05));     //Login verification
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
                        Password = "new_password",
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
                else
                {
                    net.CurrentAccount = null;
                    Logger.Trace("Сan not create account: " + m_Uid);
                }
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
                    net.movedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    //отсылаем Гейм серверу информацию об аккаунте
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    //отсылаем Клиенту информацию о куках
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
                return;
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
                    net.CurrentAccount.Session = cookie = 0x7AB41028; //Designated session

                    net.movedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(net.CurrentAccount));
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
