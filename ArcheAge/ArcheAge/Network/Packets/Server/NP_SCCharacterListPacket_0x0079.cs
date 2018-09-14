using ArcheAge.ArcheAge.Holders;
using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Network;
using System.Collections.Generic;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_0x05_CharacterListPacket_0x0079 : NetPacket
    {
        public void WriteItem(int itemId)
        {
            ns.Write((int)itemId); //ItemId d 
            if (itemId > 0)
            {
                ns.Write((long)0x01); //objectId q
                ns.Write((byte)0x00); //type c
                ns.Write((byte)0x00); //flags c
                ns.Write((int)0x01);  //stackSize d
                ns.Write((byte)0x01); //detailType c
                ns.Write((byte)0x55); //durability c
                ns.Write((short)0x00);//chargeCount h
                ns.Write((long)0x00); //chargeTime q
                ns.Write((short)0x00);//scaledA h
                ns.Write((short)0x00);//scaledB h
                for (int i = 0; i < 4; i++)
                {
                    ns.Write((byte)0x00); //pish c
                    ns.Write((int)0x00); //pisc d
                }
                ns.Write((long)0x5B2D8098); //creationTime q
                ns.Write((int)0x00);        //lifespanMins d
                ns.Write((int)0x00);        //type d
                ns.Write((byte)0x01);       //worldId c
                ns.Write((long)0x00);       //unsecureDateTime q
                ns.Write((long)0x00);       //unpackDateTime q
                ns.Write((long)0x00);       //chargeUseSkillTime q
            }
        }
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_0x05_CharacterListPacket_0x0079(ClientConnection net, int num, int last) : base(05, 0x0079)
        {
            var accountId = net.CurrentAccount.AccountId;
            List<Character> charList = CharacterHolder.LoadCharacterData(accountId);
            var totalChars = CharacterHolder.GetCount();


            ns.Write((byte)last); //"last" type="c"
            if (totalChars == 0)
            {
                ns.Write((byte)0); //totalChars); //"count" type="c"
                return; //если пустой список, заканчиваем работу
            }
            else
            {
                ns.Write((byte)1); //totalChars); //"count" type="c"
            }
            int aa = 0;
            foreach (Character chr in charList)
            {
                if (num == aa) //параметр NUM отвечает, которого чара выводить в пакете (может быть от 0 до 2)
                {
                    CharacterHolder.LoadEquipPacksData(chr, chr.Ability[0]); //дополнительно прочитать NewbieClothPackId, NewbieWeaponPackId из таблицы character_equip_packs
                    CharacterHolder.LoadClothsData(chr, chr.NewbieClothPackId); //дополнительно прочитать Head,Chest,Legs,Gloves,Feet из таблицы equip_pack_cloths
                    CharacterHolder.LoadWeaponsData(chr, chr.NewbieWeaponPackId); //дополнительно прочитать Weapon,WeaponExtra,WeaponRanged,Instrument из таблицы equip_pack_weapons
                    CharacterHolder.LoadCharacterBodyCoord(chr, chr.CharRace, chr.CharGender); //дополнительно прочитать body, x, y, z из таблицы charactermodel
                    CharacterHolder.LoadZoneFaction(chr, chr.CharRace, chr.CharGender); //дополнительно прочитать FactionId,StartingZoneId из таблицы characters

                    //D7940100
                    ns.Write((int)chr.CharacterId); //type d
                                                    //size.name
                                                    //0600 52656D6F7461 (Remota)
                    ns.WriteUTF8Fixed(chr.CharName, chr.CharName.Length); //name S
                    ns.Write((byte)chr.CharRace); //CharRace c
                    ns.Write((byte)chr.CharGender); //CharGender c
                    ns.Write((byte)chr.Level); //level c"
                    ns.Write((int)0x02D0); //health d
                    ns.Write((int)0x029E); //mana d
                    ns.Write((int)chr.StartingZoneId); //zone_id d
                    ns.Write((int)chr.FactionId); //FactionId d
                    //size.factionName
                    //0000
                    string msg = "";
                    ns.WriteUTF8Fixed(msg, msg.Length);
                    ns.Write((int)0x00); //type d
                    ns.Write((int)0x00); //family d
                    ns.Write((int)0x011F8054); // validFlags d
                    //------------------------------------
                    //цикл по equio персонажа
                    //------------------------------------
                    //0.шлем (0 голова)
                    //1.нагрудник (23387 грубая рубаха )
                    //2.пояс (0 )
                    //3.наручи (0 )
                    //4.перчатки (0 )
                    //5.плащ (0 )
                    //6.поножи (23388 грубые штаны)
                    //7.обувь (23390 грубые башмаки)
                    //8. плащ
                    //9. нижнее белье
                    //10.нижнее белье
                    //11.ожерелье
                    //12.серьга
                    //13.серьга
                    //14.кольцо
                    //15.кольцо
                    //16.двуручное оружие
                    //17.дополнительное оружие
                    //18.оружие дальноего боя
                    //19.инструмент
                    // { 7, раз, предметы на персонаже?
                    // {1}
                    WriteItem(chr.Head); //ItemId d Head
                    // {2}
                    WriteItem(chr.Chest); //ItemId d Chest
                    // {3}
                    WriteItem(chr.Legs); //ItemID d Legs
                    // {4}
                    WriteItem(chr.Gloves); //ItemID d Gloves
                    // {5}
                    WriteItem(chr.Feet); //ItemID d Feet
                    // {6}
                    WriteItem(0x17EF); //ItemID d
                    // {7}
                    WriteItem(0x1821); //ItemId d

                    ns.Write((int)chr.Weapon); // type d
                    ns.Write((int)chr.WeaponExtra); // type d
                    ns.Write((int)chr.WeaponRanged); // type d
                    ns.Write((int)chr.Instrument); // type d

                    //for (int i = 0; i < 3; i++)
                    {
                        //ns.Write((byte)chr.Ability[i]); //"ability[] c"
                        ns.Write((byte)chr.Ability[0]);
                        ns.Write((byte)chr.Ability[1]);
                        ns.Write((byte)chr.Ability[2]);
                    }
                    //pos в пакете нет, в коде 01
                    ns.Write((long)0x0007045E3D800000); //x q
                    ns.Write((long)0x0021F96715C40000); //y q
                    ns.Write((int)0x42CFA1CB);          //z d
                    ns.Write((byte)chr.Ext); //ext c
                    switch (chr.Ext)
                    {
                        case 0:
                            break;
                        default:
                            ns.Write((int)0x10CB); //type d
                            ns.Write((int)0x00); //type d
                            ns.Write((int)0); //defaultHairColor d
                            ns.Write((int)0); //twoToneHair d
                            ns.Write((int)0); //twoToneFirstWidth d
                            ns.Write((int)0); //twoToneSecondWidth d
                            ns.Write((int)0x04); //type d
                            ns.Write((int)0x00); //type d
                            ns.Write((int)0x00); //type d
                            //ns.Write((float)chr.Weight[10]); //weight f
                            ns.Write((float)0x3F800000);

                            ns.Write((int)0x00); //type d
                            //ns.Write((float)chr.Weight[12]); //weight f
                            ns.Write((float)0x3F800000);

                            //ns.Write((float)chr.Scale); //scale f
                            ns.Write((float)0x3F800000);
                            //ns.Write((float)chr.Rotate); //rotate f
                            ns.Write((float)0x00);
                            //ns.Write((short)chr.MoveX); //moveX h
                            ns.Write((short)0x00);
                            //ns.Write((short)chr.MoveY); //moveY h
                            ns.Write((short)0x00);
                            //pish 1
                            ns.Write((byte)0x04);
                            //pisc 5
                            ns.Write((byte)0x00);
                            ns.Write((byte)0xBC);
                            ns.Write((byte)0x01);
                            ns.Write((byte)0xAA);
                            ns.Write((byte)0x00);
                            //pish 1
                            ns.Write((byte)0x00);
                            //pisc 2
                            ns.Write((byte)0x00);
                            ns.Write((byte)0x00);
                            //pish 1
                            ns.Write((byte)0x00);
                            //pisc 3
                            ns.Write((byte)0x00);
                            ns.Write((byte)0x00);
                            ns.Write((byte)0x00);

                            /*
                            ns.Write((float)chr.Weight[12]); //weight f
                            ns.Write((float)chr.Weight[12]); //weight f
                            ns.Write((float)chr.Weight[12]); //weight f
                            ns.Write((float)chr.Weight[12]); //weight f
                            ns.Write((float)chr.Weight[12]); //weight f
                            ns.Write((float)chr.Weight[12]); //weight f

                            ns.Write((float)chr.Weight[12]); //weight f
                            */
                            ns.Write((float)0x3F800000);
                            ns.Write((float)0x3F800000);
                            ns.Write((float)0x3F800000);
                            ns.Write((float)0x3F35C28F);
                            ns.Write((float)0x3F800000);
                            ns.Write((float)0x3F800000);
                            ns.Write((float)0x3F800000);

                            ns.Write((uint)chr.Lip); //lip d
                            ns.Write((uint)chr.LeftPupil); //leftPupil d
                            ns.Write((uint)chr.RightPupil); //rightPupil d
                            ns.Write((uint)chr.Eyebrow); //eyebrow d
                            ns.Write((int)chr.Decor); //decor d
                                                      //следующая инструкция пишет: len.stringHex
                            string subString = chr.Modifiers.Substring(0, 256); //надо отрезать в конце два символа \0\0
                            ns.WriteHex(subString, subString.Length); //  modifiers b"
                            break;
                    }
                    ns.Write((short)0x1388); //laborPower h
                    ns.Write((long)0x5B2D8572); //lastLaborPowerModified q
                    ns.Write((short)0x00); //deadCount h
                    ns.Write((long)0x5B29D9A2); //deadTime q
                    ns.Write((int)0x00); //rezWaitDuration d
                    ns.Write((long)0x5B29D9A2); //rezTime q
                    ns.Write((int)0x00); //rezPenaltyDuration d
                    ns.Write((long)0x5B2D8205); //lastWorldLeaveTime q
                    ns.Write((long)0x00); //moneyAmount q
                    ns.Write((long)0x00); //moneyAmount q
                    ns.Write((short)0x00); //crimePoint h
                    ns.Write((int)0x00); //crimeRecord d
                    ns.Write((short)0x00); //crimeScore h
                    ns.Write((long)0x00); //deleteRequestedTime  q
                    ns.Write((long)0x00); //transferRequestedTime q
                    ns.Write((long)0x00); //deleteDelay q
                    ns.Write((int)0x00); //consumedLp d
                    ns.Write((long)0x1E); //bmPoint q
                    ns.Write((long)0x00); //moneyAmount q
                    ns.Write((long)0x00); //moneyAmount q
                    ns.Write((byte)0x00); //autoUseAApoint c
                    ns.Write((int)0x01); //prevPoint d
                    ns.Write((int)0x01); //point d
                    ns.Write((int)0x00); //gift d
                    ns.Write((long)0x5B3F9014); //updated q
                    ns.Write((byte)0x00); //forceNameChange c
                    ns.Write((int)0x00); //highAbilityRsc d
                }
                ++aa;
            }
        }
    }

    public sealed class NP_CharacterListPacket_0x0079 : NetPacket
    {
        public NP_CharacterListPacket_0x0079() : base(05, 0x0079)
        {
            //пакеты для входа в Лобби
            //ns.WriteHex(
            //"0209DD051E05ACB68556F261C495603654B3CB183376E4B591B032FED03F734111A9B382524AF2C393633303D4A4744414E5B5D1D53AF79DCD6636A22DA6724710E0B0805120F1C191636702D2A2734313E3B3845424F4C495653505D5A6764616E6B7875727F7D0A0704010E1F8BCF47BF2C292623303D3A3734414E4B5845525F5C595663606D6A6774717E7B7805020F0C09161315D8AA272E4E8E3B6835323F3C494653404D5A4334516E6B6865627F7C797674010E0B0805121F1C191623202D2A2734313E3B3845424BDF9303F3505D5A6764616E6B7875727F6C090603001D1A1714112E2B2825223F3C393633404D4A4741B4EE5B522AC27F3C696673707D7A6805020F1E391613101D1A2724212E2B3835323F4C494643405D5A5754516E6B6865627F7C7976779C375FA704111E1B1815222F2C292633203D3A3744414E4B4855525F5C696663606D7A7774717F0C051753001795B704412E2B2825223F2C393633586D4A4744515E5B5855626F6C696673707D7A7704010E0B1815121F1C292623202D3A33A7EB6BEB4845424F5C595653506D6A6774617E7B7875730FED0A0704111E1B1815222F2C292633303DBBC7444BD1EB4805525F5C696663706D7A776DC17E0B0805020F1C191613102D2A2724213E3B3835324F4C494643505D5A5754616E6FFBAF27DF7C7A0704010E0B1815121F1C392623202D3A3734313E4B4845424F5C595653506D6A67646F8F0B787FDDAF1C590603101D1A1734212E2B3015323F3C394643404D4A5754515E5B6865626F6C797673707E0B0805020F1C19161314BEF07284213E3B3835424F4C494653504D5A5764616E6B6875727F7C7906030FED0A1714111E1B282733AF2C338993206D4A4744414E5B4855525F74496663707D7A7775020F0C090613101D1A1724212E2B2835323F3C394643404D4A5750C2840EC865626F6C797673707D0A0704110E1B1815122F2C292623303D3A3734414E4B4845525F5C5066736065E3B774736E5B7906030FED0A07C4C11E1B282F2F1E7C19363330364F8464761449CC15626F6C696663707D7977E47EFE0B080AE21F1C191623202D2FD734313E3B3845424F4C495653585EAA6764616E7B7076827FE509F704011E1B1815122A6C29967CF07E7A6764B12EBB2845525F5C595E60906D6264847176788A90CAFCEC091E10E01D2224D4212638C286D1B0B9BCF41CB5A8E808ADF18E849865627F74797674000E0B07151002C0DD158390929A173B5E62CBF848524B3C59EA9A905D6A676AE16E7B7878E98F0C090603001D1AD8B5F12F0538E521DD2A7B040DA12D4A474AD15FD5D3CBE26D4F1969B3707D7B04F9C20F0C191613101D2A2724212E3B3835323F4C494643405D5A5754516E6B6865627F7C7976730FED0A0704111E1B109413662FDC8633303D3A474F67AD7EE855525F5C6966636B4B9942C4717F0C090603001DCF24F1A12E2B2825223F3C393633404D4A4754515E5B5865626F6C697673707D7A0704010E0B1815121F1C292623202D3A3734313E4B4845424F5C58B653506D6A6764617E7B7876030FED0A0714111E1B1825222F3C293633203D3A4744414E4A1156A7EF5C696663606D7A777D1F0E1B086506494B7F40D5E03D0A3A24113E32D815424D8C594655005D5A5764616E6B6875727F7C7F4F05F11BBEA81519132A994623202D3A3734213E4B4850124F5C595653606D6A6764717E7B7875020F0C090613101D1A1724212E2B2835323F35B14496F04D4A5754515E5B6865626F6C697673707E0B0805021F1C191613202D2A2724313E3B3835478AFC494D4F66ED3A5764616E6B6865727F7C6D66030FED0A1714111E1B2825222F3C393633304D4A4744415E5B5855526F6C69666379F578A2C5020F0C090613101D1A1724213E2B2835323F3C394643404D4A5754515E6B6865626F7C797396CFED012B32B17E1B1815122F2C392623302F0A3734414E4B4845525F5C595663606D6A6774717E7B7906030FED1A1714111E2B282CAA2DE9893633304D4A4744415E5B5855426F6C696663707D7A7774010E0B0805121F1C191623202D2636643135077EF5224F4C595653504D6A6764794E7B7875727FED0A0704011E1B1815122F2C292623303D3A3734414E4B4845525F5C50EE61B5DD6A7774717E7B0805020F0C190613101D2A2724212E3B3835323F4C494643405D5A5754516EEAE86569334AC91674010E0B0815021F1C193F93202D2A3734313E3B4845424F4C595653505D6A6764616E7B7875727F0C09060300149215C1A12E2B2825223F3C393643404D5A4754515E5B5865626F6C697673707D7B0805020F0C19161DE16D2A2C78179E5B3835323F4C495643405D427754516E6B6865727F7C7976030FED0A0714111E1B1825222F2C293633303D3A474448C6498DE5525F5C696663606D7A7774716F0C090603001D1A1714212E2B2825323F3C393643404D4A4545D15E503453D20F6C697673707D6A070401162B1815121F1C292623202D3A3734314E4B4845425F5C595653606D6A6764717E7B787F8B0D38BA0714111E1B1825222F2C293623303D3A4744414E4B5855525F5C696663607D7A7774710E0CFCD5021EDA291612900D2A2724313E3B28E5924F444A93B3102D5A5768203814FA657EC5608E2738B00E0B1815121F2C292623203D3A3734314E4B4845425F5C195653606D6A6764717E7B78750A0CFC090613101D1214D4212628D835323F3C394643400D51974EF15E6B6865626F7C79767B72FE0B0006F21F141AEEEF037EDA272C32CE3B3036B24F444AB864E8E2A0A9AA9E9495B68A87FBFF86F6030FED1217141FEE25D82BC22F2C093633304D4A4654415E5B585AB26F0FD2F66EF0739A7A35008EB2B7161D6012195514212E2B3833723F3C494643404B1A57545E5E6B6865626F7C7BCB237FED0C4704011E14881512212C292623303D3A3734414E4B4855525F5C596663606D6A7774717E7C0906030FED1A1714111E2B2825222F3C393EB2010449B2F4415E5B58555842FEFCD663707D7A77040104269A80A21F1C192623202D2A6F16E48E3B4845424F4C595653505D6A6764616E7B7875727FED0A0704011E1B1815122F2C292633303D3A3744414E4B4855525F5C596663606D6A7694717E7B0805020F0C191613101D2A2724212E3B3835323F4C594643504D5A5754616E6B692C718ACC797704010E0B08151");

            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0209 DD05 6F  11  4802   01 02 D7940100 0600 52656D6F7461 03 02 01 D0020000 9E020000 2C010000 68000000 0000 00000000 0000000054801F015B5B0000A5FA010500000000000001000000015500000000000000000000000000000000000000000000000000000000000000000000493DA55A000000000000000000000000010000000000000000000000000000000000000000000000005C5B0000A6FA010500000000000001000000014600000000000000000000000000000000000000000000000000000000000000000000493DA55A000000000000000000000000010000000000000000000000000000000000000000000000005E5B0000A7FA010500000000000001000000012300000000000000000000000000000000000000000000000000000000000000000000493DA55A00000000000000000000000001000000000000000000000000000000000000000000000000C1150000A8FA010500000000000001000000018200000000000000000000000000000000000000000000000000000000000000000000493DA55A0000000000000000000000000100000000000000000000000000000000000000000000000008180000A9FA010500000000000001000000019B00000000000000000000000000000000000000000000000000000000000000000000493DA55A00000000000000000000000001000000000000000000000000000000000000000000000000EF170000AAFA010500000000000001000000018200000000000000000000000000000000000000000000000000000000000000000000493DA55A0000000000000000000000000100000000000000000000000000000000000000000000000021180000ABFA010500000000000001000000018200000000000000000000000000000000000000000000000000000000000000000000493DA55A0000000000000000000000000100000000000000000000000000000000000000000000000093010000889D00002102000000000000010D0D0000509E95FE0600000048B330FC2100CBA1CF42030000000000000000300907FF000000FF00000000000000005F00000000000000000000000000803F000000000000803F0000803F000000000000000054000B05FC043405050F060F06000000000000803F0000803F0000803F295C8F3E0000803F0000803F0000803FAB3E38FF5F5B25FF5F5B25FF9A0E0EFF0000000080000000100000F10021DDCC403A0B0BFB0300F6F5CF0C00D10047000BCC9C00000000E800000000D9BF000000000000000CFA1E0012E10C003E21642323EE16000000E80018E8B9E800223700FC00000000CFCC00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000881372852D5B000000000000A2D9295B0000000000000000A2D9295B0000000000000000017F2D5B00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001E000000000000000000000000000000000000000000000000010000000100000000000000FE7D2D5B00000000000000000096E701000600446576656C6F010201D00200009E0200002C010000650000000000000000000000000054801F015B5B0000B0C36B060000000000000100000001550000000000000000000000000000000000000000000000000000000000000000000098802D5B000000000000000000000000010000000000000000000000000000000000000000000000005C5B0000B1C36B060000000000000100000001460000000000000000000000000000000000000000000000000000000000000000000098802D5B000000000000000000000000010000000000000000000000000000000000000000000000005E5B0000B2C36B060000000000000100000001230000000000000000000000000000000000000000000000000000000000000000000098802D5B00000000000000000000000001000000000000000000000000000000000000000000000000C1150000B3C36B060000000000000100000001820000000000000000000000000000000000000000000000000000000000000000000098802D5B0000000000000000000000000100000000000000000000000000000000000000000000000008180000B4C36B0600000000000001000000019B0000000000000000000000000000000000000000000000000000000000000000000098802D5B00000000000000000000000001000000000000000000000000000000000000000000000000EF170000B5C36B060000000000000100000001820000000000000000000000000000000000000000000000000000000000000000000098802D5B0000000000000000000000000100000000000000000000000000000000000000000000000021180000B6C36B060000000000000100000001820000000000000000000000000000000000000000000000000000000000000000000098802D5B000000000000000000000000010000000000000000000000000000000000000000000000007F4D00001C6300001B02000000000000010D0D0000803D5E0407000000C41567F92100CBA1CF4203CB10000000000000000000000000000000000000000000000400000000000000000000000000803F000000000000803F0000803F00000000000000000400BC01AA00000000000000000000803F0000803F0000803F8FC2353F0000803F0000803F0000803FE37B8BFFAFECEFFFAFECEFFF584838FF00000000800000EF00EF00EE000103000000000000110000000000FE00063BB900D800EE00D400281BEBE100E700F037230000000000640000000000000064000000F0000000000000002BD50000006400000000F9000000E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000881372852D5B000000000000A2D9295B0000000000000000A2D9295B000000000000000005822D5B00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001E000000000000000000000000000000000000000000000000010000000100000000000000FE7D2D5B000000000000000000"
            //3.0.3.0
            //"0209 DD05 31  11  7900   0102D7940100060052656D6F7461030201D00200009E02000048010000680000000000000000000000000054801F015B5B0000A5FA010500000000000001000000015500000000000000000000000000000000000000000000000000000000000000000000493DA55A000000000000000000000000010000000000000000000000000000000000000000000000005C5B0000A6FA010500000000000001000000014600000000000000000000000000000000000000000000000000000000000000000000493DA55A000000000000000000000000010000000000000000000000000000000000000000000000005E5B0000A7FA010500000000000001000000012300000000000000000000000000000000000000000000000000000000000000000000493DA55A00000000000000000000000001000000000000000000000000000000000000000000000000C1150000A8FA010500000000000001000000018200000000000000000000000000000000000000000000000000000000000000000000493DA55A0000000000000000000000000100000000000000000000000000000000000000000000000008180000A9FA010500000000000001000000019B00000000000000000000000000000000000000000000000000000000000000000000493DA55A00000000000000000000000001000000000000000000000000000000000000000000000000EF170000AAFA010500000000000001000000018200000000000000000000000000000000000000000000000000000000000000000000493DA55A0000000000000000000000000100000000000000000000000000000000000000000000000021180000ABFA010500000000000001000000018200000000000000000000000000000000000000000000000000000000000000000000493DA55A0000000000000000000000000100000000000000000000000000000000000000000000000093010000889D00002102000000000000010D0D00000000A0D3150200000000B05C320374A12944030000000000000000300907FF000000FF00000000000000005F00000000000000000000000000803F000000000000803F0000803F000000000000000054000B05FC043405050F060F06000000000000803F0000803F0000803F295C8F3E0000803F0000803F0000803FAB3E38FF5F5B25FF5F5B25FF9A0E0EFF0000000080000000100000F10021DDCC403A0B0BFB0300F6F5CF0C00D10047000BCC9C00000000E800000000D9BF000000000000000CFA1E0012E10C003E21642323EE16000000E80018E8B9E800223700FC00000000CFCC0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000088134E42535B000000000000B26E335B0000000000000000B26E335B00000000000000000D533E5B00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001E0000000000000000000000000000000000000000000000000100000001000000000000004E42535B00000000000000000096E701000600446576656C6F010201D00200009E0200002C010000650000000000000000000000000054801F015B5B0000B0C36B060000000000000100000001550000000000000000000000000000000000000000000000000000000000000000000098802D5B000000000000000000000000010000000000000000000000000000000000000000000000005C5B0000B1C36B060000000000000100000001460000000000000000000000000000000000000000000000000000000000000000000098802D5B000000000000000000000000010000000000000000000000000000000000000000000000005E5B0000B2C36B060000000000000100000001230000000000000000000000000000000000000000000000000000000000000000000098802D5B00000000000000000000000001000000000000000000000000000000000000000000000000C1150000B3C36B060000000000000100000001820000000000000000000000000000000000000000000000000000000000000000000098802D5B0000000000000000000000000100000000000000000000000000000000000000000000000008180000B4C36B0600000000000001000000019B0000000000000000000000000000000000000000000000000000000000000000000098802D5B00000000000000000000000001000000000000000000000000000000000000000000000000EF170000B5C36B060000000000000100000001820000000000000000000000000000000000000000000000000000000000000000000098802D5B0000000000000000000000000100000000000000000000000000000000000000000000000021180000B6C36B060000000000000100000001820000000000000000000000000000000000000000000000000000000000000000000098802D5B000000000000000000000000010000000000000000000000000000000000000000000000007F4D00001C6300001B02000000000000010D0D0000803D5E0407000000C41567F92100CBA1CF4203CB10000000000000000000000000000000000000000000000400000000000000000000000000803F000000000000803F0000803F00000000000000000400BC01AA00000000000000000000803F0000803F0000803F8FC2353F0000803F0000803F0000803FE37B8BFFAFECEFFFAFECEFFF584838FF00000000800000EF00EF00EE000103000000000000110000000000FE00063BB900D800EE00D400281BEBE100E700F037230000000000640000000000000064000000F0000000000000002BD50000006400000000F9000000E000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000088134E42535B000000000000B16E335B0000000000000000B16E335B00000000000000006A903F5B00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001E0000000000000000000000000000000000000000000000000100000001000000000000004E42535B000000000000000000"

            //last 1
            //01
            ns.Write((byte)0x01);
            //count 1
            //02
            ns.Write((byte)0x01);
            /*//{ количество персонажей
            //================================================================================
            //====================================== Remota ==================================
            //================================================================================
            //type 4 (charID)
            //D7940100
            ns.Write((int)0x0194D7);
            //size.name
            //0600 52656D6F7461 (Remota)
            string msg = "Remota";
            ns.WriteUTF8Fixed(msg, msg.Length);
            //CharRace 1, Role Race Follow the fields of the login server 1 Noah, 3 Dwarfs, 4 Elf, 5 Harry Blue, 6 Beast, 8 Battle Demon.
            //03 (Гномы)
            ns.Write((byte)0x03);
            //CharGender 1, Character gender 1 male, 2 female
            //02 (Ж)
            ns.Write((byte)0x02);
            //level 1
            //01
            ns.Write((byte)0x01);
            //health 4
            //D0020000 (720)
            ns.Write((int)0x02D0);
            //mana 4
            //9E020000 (670)
            ns.Write((int)0x029E);
            //zone_id 4
            //2C010000
            ns.Write((int)0x012C);
            //type 4 FactionId
            //68000000
            ns.Write((int)0x68);
            //ns.Write((int)0x00); //если 00, то можно в factionName свою фракцию писать
            //size.factionName
            //0000
            //msg = "Моя superb фракция";
            msg = "";
            ns.WriteUTF8Fixed(msg, msg.Length);
            //type 4
            //00000000
            ns.Write((int)0x00);
            //family 4
            //00000000
            ns.Write((int)0x00);
            //{
            // validFlags 4
            ns.Write((int)0x011F8054);
            // { 7, раз, предметы на персонаже?
            // {1}
            //     type 4 ItemID Head
            ns.Write((int)0x5052);
            //     id 8
            ns.Write((long)0x0501FAA5);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x55);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5AA53D49);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {2}
            //     type 4 ItemID Chest
            ns.Write((int)0x506D);
            //     id 8
            ns.Write((long)0x0501FAA6);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x46);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5AA53D49);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {3}
            //     type 4 ItemID Legs
            ns.Write((int)0x5088);
            //     id 8
            ns.Write((long)0x0501FAA7);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x23);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5AA53D49);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {4}
            //     type 4 ItemID Gloves
            ns.Write((int)0x50A3);
            //     id 8
            ns.Write((long)0x0501FAA8);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x82);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5AA53D49);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {5}
            //     type 4 itemId Feet
            ns.Write((int)0x50BE);
            //     id 8 ObjectId
            ns.Write((long)0x0501FAA9);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x9B);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5AA53D49);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {6}
            //     type 4 ItemId
            ns.Write((int)0x17EF);
            //     id 8
            ns.Write((long)0x0501FAAA);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x82);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5AA53D49);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {7}
            //     type 4 itemId
            ns.Write((int)0x1821);
            //     id 8 ObjectId
            ns.Write((long)0x0501FAAB);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x82);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5AA53D49);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // }
            // { 3 раза
            // type 4
            ns.Write((int)0x0193);
            // type 4
            ns.Write((int)0x9D88);
            // type 4
            ns.Write((int)0x0221);
            // flags 4
            ns.Write((int)0x00);
            // }
            //}
            //{3 раза
            //ability 1
            ns.Write((byte)0x01);
            ns.Write((byte)0x0D);
            ns.Write((byte)0x0D);
            //}
            //pos в пакете нет, в коде 01
            //x 8
            ns.Write((long)0x0006FE959E500000);
            //y 8
            ns.Write((long)0x0021FC30B3480000);
            //z 4
            ns.Write((int)0x42CFA1CB);
            //{
            //ext 1
            ns.Write((byte)0x03);
            //type 4
            ns.Write((int)0x00);
            //type 4
            ns.Write((int)0x00);
            //defaultHairColor 4
            ns.Write((int)0x300907FF);
            //twoToneHair 4
            ns.Write((int)0xFF);
            //twoToneFirstWidth 4
            ns.Write((int)0x00);
            //twoToneSecondWidth 4
            ns.Write((int)0x00);
            //type 4
            ns.Write((int)0x5F);
            //type 4
            ns.Write((int)0x00);
            //type 4
            ns.Write((int)0x00);
            //weight 4
            ns.Write((int)0x3F800000);
            //{
            //type 4
            ns.Write((int)0x00);
            //weight 4
            ns.Write((int)0x3F800000);
            //scale 4
            ns.Write((int)0x3F800000);
            //rotate 4
            ns.Write((int)0x00);
            //moveX 2
            ns.Write((short)0x00);
            //moveY 2
            ns.Write((short)0x00);
            //}
            //{
            //pish 1
            ns.Write((byte)0x54);
            //pisc 5
            ns.Write((byte)0x00);
            ns.Write((byte)0x0B);
            ns.Write((byte)0x05);
            ns.Write((byte)0xFC);
            ns.Write((byte)0x04);
            //pish 1
            ns.Write((byte)0x34);
            //pisc 2
            ns.Write((byte)0x05);
            ns.Write((byte)0x05);
            //pish 1
            ns.Write((byte)0x0F);
            //pisc 3
            ns.Write((byte)0x06);
            ns.Write((byte)0x0F);
            ns.Write((byte)0x06);
            //????????????? еще раз ????????
            //pish 1
            ns.Write((byte)0x00);
            //pisc 3
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            //}
            //{6 раз
            //weight 4
            ns.Write((int)0x3F800000);
            ns.Write((int)0x3F800000);
            ns.Write((int)0x3F800000);
            ns.Write((int)0x3F35C28F);
            ns.Write((int)0x3F800000);
            ns.Write((int)0x3F800000);
            //}
            //weight 4
            ns.Write((int)0x3F800000);
            //lip 4
            ns.Write((uint)0xAB3E38FF); //не влезает в INTEGER
                                        //leftPupil 4
            ns.Write((uint)0x5F5B25FF); //не влезает в INTEGER
                                        //rightPupil 4
            ns.Write((uint)0x5F5B25FF); //не влезает в INTEGER
                                        //eyebrow 4
            ns.Write((uint)0x9A0E0EFF); //не влезает в INTEGER
                                        //deco 4
            ns.Write((int)0x00);
            //size.modifiers (128)
            msg = "0000100000F10021DDCC403A0B0BFB0300F6F5CF0C00D10047000BCC9C00000000E800000000D9BF000000000000000CFA1E0012E10C003E21642323EE16000000E80018E8B9E800223700FC00000000CFCC00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            ns.WriteHex(msg, msg.Length);
            //}
            //laborPower 2
            ns.Write((short)0x1388); //очки работы = 5000
                                     //lastLaborPowerModified 8
            ns.Write((long)0x5B2D8572);
            //deadCount 2
            ns.Write((short)0x00);
            //deadTime 8
            ns.Write((long)0x5B29D9A2);
            //rezWaitDuration 4
            ns.Write((int)0x00);
            //rezTime 8
            ns.Write((long)0x5B29D9A2);
            //rezPenaltyDuration 4
            ns.Write((int)0x00);
            //lastWorldLeaveTime 8
            ns.Write((long)0x5B2D8205);
            //moneyAmount 8, Number of copper coins Automatic 1:100:10000 Convert gold coins
            ns.Write((long)0x1E); //серебро, золото и платина (начало)
                                  //moneyAmount 8
            ns.Write((long)0x00); //серебро, золото и платина (продолжение)
                                  //crimePoint 2
            ns.Write((short)0x00);
            //crimeRecord 4
            ns.Write((int)0x00);
            //crimeScore 2
            ns.Write((short)0x00);
            //deleteRequestedTime  8
            ns.Write((long)0x00);
            //transferRequestedTime 8
            ns.Write((long)0x00);
            //deleteDelay 8
            ns.Write((long)0x00);
            //consumedLp 4
            ns.Write((int)0x00);
            //bmPoint 8
            ns.Write((long)0x1E); //монеты дару = 30
                                  //moneyAmount 8
            ns.Write((long)0x00);
            //moneyAmount 8
            ns.Write((long)0x00);
            //autoUseAApoint 1
            ns.Write((byte)0x00);
            //prevPoint 4
            ns.Write((int)0x01);
            //point 4
            ns.Write((int)0x01);
            //gift 4
            ns.Write((int)0x00);
            //updated 8
            ns.Write((long)0x5B3F9014);
            //forceNameChange 1
            ns.Write((byte)0x00);
            //highAbilityRsc 4
            ns.Write((int)0x00);
            //}
*/

            //================================================================================
            //====================================== Develo ==================================
            //================================================================================
            //type 4 (charID)
            //D7940100
            ns.Write((int)0x01E796);
            //size.name
            //0600 52656D6F7461 (Remota)
            string msg = "Develo";
            ns.WriteUTF8Fixed(msg, msg.Length);
            //CharRace 1
            //01 (Нуиане)
            ns.Write((byte)0x01);
            //CharGender 1
            //02 (Ж)
            ns.Write((byte)0x02);
            //level 1
            //01
            ns.Write((byte)0x01);
            //health 4
            //D0020000 (720)
            ns.Write((int)0x02D0);
            //mana 4
            //9E020000 (670)
            ns.Write((int)0x029E);
            //zone_id 4
            //2C010000
            ns.Write((int)0x012C);
            //type 4 FactionId
            //68000000
            ns.Write((int)0x65);
            //size.factionName
            //0000
            msg = "";
            ns.WriteUTF8Fixed(msg, msg.Length);
            //type 4
            //00000000
            ns.Write((int)0x00);
            //family 4
            //00000000
            ns.Write((int)0x00);
            //{
            // validFlags 4
            ns.Write((int)0x011F8054);
            // { 7, раз, предметы на персонаже?
            // {1}
            //     type 4 ItemId Head
            ns.Write((int)0x5103);
            //     id 8
            ns.Write((long)0x066BC3B0);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x55);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5B2D8098);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {2}
            //     type 4 ItemID Chest
            ns.Write((int)0x511E);
            //     id 8
            ns.Write((long)0x066BC3B1);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x46);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5B2D8098);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {3}
            //     type 4 ItemID Legs
            ns.Write((int)0x5139);
            //     id 8
            ns.Write((long)0x066BC3B2);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x23);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5B2D8098);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {4}
            //     type 4 ItemID Gloves
            ns.Write((int)0x5154);
            //     id 8
            ns.Write((long)0x066BC3B3);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x82);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5B2D8098);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {5}
            //     type 4 ItemID Feet
            ns.Write((int)0x516F);
            //     id 8
            ns.Write((long)0x066BC3B4);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x9B);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5B2D8098);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {6}
            //     type 4
            ns.Write((int)0x17EF);
            //     id 8
            ns.Write((long)0x066BC3B5);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x82);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5B2D8098);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // {7}
            //     type 4
            ns.Write((int)0x1821);
            //     id 8
            ns.Write((long)0x066BC3B6);
            //     type 1
            ns.Write((byte)0x00);
            //     flags 1
            ns.Write((byte)0x00);
            //     stackSize 4
            ns.Write((int)0x01);
            //     {
            //         detailType 1 (switch)
            ns.Write((byte)0x01);
            //         {
            //            durability 1
            ns.Write((byte)0x82);
            //            chargeCount 2
            ns.Write((short)0x00);
            //            chargeTime 8
            ns.Write((long)0x00);
            //            scaledA 2
            ns.Write((short)0x00);
            //            scaledB 2
            ns.Write((short)0x00);
            //            { 4, раза
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //                pish 1
            ns.Write((byte)0x00);
            //                pisc 4
            ns.Write((int)0x00);
            //            }
            //         }
            //     }
            //     creationTime 8
            ns.Write((long)0x5B2D8098);
            //     lifespanMins 4
            ns.Write((int)0x00);
            //     type 4
            ns.Write((int)0x00);
            //     worldId 1
            ns.Write((byte)0x01);
            //     unsecureDateTime 8
            ns.Write((long)0x00);
            //     unpackDateTime 8
            ns.Write((long)0x00);
            //     chargeUseSkillTime 8
            ns.Write((long)0x00);
            // }
            // { 8 раз
            // type 4
            ns.Write((int)0x4D7F);
            // type 4
            ns.Write((int)0x631C);
            // type 4
            ns.Write((int)0x021B);
            // flags 4
            ns.Write((int)0x00);
            // }
            //}
            //{3 раза
            //ability 1
            ns.Write((byte)0x01);
            ns.Write((byte)0x0D);
            ns.Write((byte)0x0D);
            //}
            //pos в пакете нет, в коде 01
            //x 8
            ns.Write((long)0x0007045E3D800000);
            //y 8
            ns.Write((long)0x0021F96715C40000);
            //z 4
            ns.Write((float)0x42CFA1CB);
            //{
            //ext 1
            ns.Write((byte)0x03);
            //type 4
            ns.Write((int)0x10CB);
            //type 4
            ns.Write((int)0x00);
            //defaultHairColor 4
            ns.Write((int)0x00);
            //twoToneHair 4
            ns.Write((int)0x00);
            //twoToneFirstWidth 4
            ns.Write((int)0x00);
            //twoToneSecondWidth 4
            ns.Write((int)0x00);
            //type 4
            ns.Write((int)0x04);
            //type 4
            ns.Write((int)0x00);
            //type 4
            ns.Write((int)0x00);
            //weight 4
            ns.Write((float)0x3F800000);
            //{
            //type 4
            ns.Write((int)0x00);
            //weight 4
            ns.Write((float)0x3F800000);
            //scale 4
            ns.Write((float)0x3F800000);
            //rotate 4
            ns.Write((float)0x00);
            //moveX 2
            ns.Write((short)0x00);
            //moveY 2
            ns.Write((short)0x00);
            //}
            //{
            //pish 1
            ns.Write((byte)0x04);
            //pisc 5
            ns.Write((byte)0x00);
            ns.Write((byte)0xBC);
            ns.Write((byte)0x01);
            ns.Write((byte)0xAA);
            ns.Write((byte)0x00);
            //pish 1
            ns.Write((byte)0x00);
            //pisc 2
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            //pish 1
            ns.Write((byte)0x00);
            //pisc 3
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            //pish 1
            //ns.Write((byte)0x00);
            //pisc 3
            //ns.Write((byte)0x00);
            //ns.Write((byte)0x00);
            //ns.Write((byte)0x00);
            //}
            //{6 раз
            //weight 4
            ns.Write((float)0x3F800000);
            ns.Write((float)0x3F800000);
            ns.Write((float)0x3F800000);
            ns.Write((float)0x3F35C28F);
            ns.Write((float)0x3F800000);
            ns.Write((float)0x3F800000);
            //}
            //weight 4
            ns.Write((float)0x3F800000);
            //lip 4
            ns.Write((uint)0xFF8B7BE3); //не влезает в INTEGER
                                        //leftPupil 4
            ns.Write((uint)0xFFEFECAF); //не влезает в INTEGER
                                        //rightPupil 4
            ns.Write((uint)0xFFEFECAF); //не влезает в INTEGER
                                        //eyebrow 4
            ns.Write((uint)0xFF384858); //не влезает в INTEGER
                                        //deco 4
            ns.Write((int)0x00);
            //size.modifiers (128)
            msg = "00EF00EF00EE000103000000000000110000000000FE00063BB900D800EE00D400281BEBE100E700F037230000000000640000000000000064000000F0000000000000002BD50000006400000000F9000000E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            ns.WriteHex(msg, msg.Length);
            //}
            //laborPower 2
            ns.Write((short)0x1388);
            //lastLaborPowerModified 8
            ns.Write((long)0x5B2D8572);
            //deadCount 2
            ns.Write((short)0x00);
            //deadTime 8
            ns.Write((long)0x5B29D9A2);
            //rezWaitDuration 4
            ns.Write((int)0x00);
            //rezTime 8
            ns.Write((long)0x5B29D9A2);
            //rezPenaltyDuration 4
            ns.Write((int)0x00);
            //lastWorldLeaveTime 8
            ns.Write((long)0x5B2D8205);
            //moneyAmount 8
            ns.Write((long)0x00);
            //moneyAmount 8
            ns.Write((long)0x00);
            //crimePoint 2
            ns.Write((short)0x00);
            //crimeRecord 4
            ns.Write((int)0x00);
            //crimeScore 2
            ns.Write((short)0x00);
            //deleteRequestedTime  8
            ns.Write((long)0x00);
            //transferRequestedTime 8
            ns.Write((long)0x00);
            //deleteDelay 8
            ns.Write((long)0x00);
            //consumedLp 4
            ns.Write((int)0x00);
            //bmPoint 8
            ns.Write((long)0x1E);
            //moneyAmount 8
            ns.Write((long)0x00);
            //moneyAmount 8
            ns.Write((long)0x00);
            //autoUseAApoint 1
            ns.Write((byte)0x00);
            //prevPoint 4
            ns.Write((int)0x01);
            //point 4
            ns.Write((int)0x01);
            //gift 4
            ns.Write((int)0x00);
            //updated 8
            ns.Write((long)0x5B3F9014);
            //forceNameChange 1
            ns.Write((byte)0x00);
            //highAbilityRsc 4
            ns.Write((int)0x00);
            //}

        }
    }

    public sealed class NP_Packet_CharList_empty_0x0079 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_CharList_empty_0x0079() : base(05, 0x0079)
        {
            //пакеты для входа в Лобби
            /*
            CPU Dump
            Address   Hex dump                                         ASCII (OEM - США)
            4456FCD8  1E 11 48 02|01 00 00 00|00 00 00 00|00 00 00 00| H
            */
            //ns.WriteHex(
            //"0800DD05FEA1C9531140");
            //"0800DD051E114802" +
            //    "0100");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0209 DD05 1E  11  4802   01 00"
            //"0209 DD05 31  11  7900   01 00"

            //3.0.3.0
            //last 1
            //01
            ns.Write((byte)0x01);
            //count 1
            //00
            ns.Write((byte)0x00);
            //дальше данные не считывались из пакета!!!
        }
    }
    public sealed class NP_Packet_CharList_one_0x0079 : NetPacket
    {
        /// <summary>
        /// пакет для входа в Лобби
        /// author: NLObP
        /// </summary>
        public NP_Packet_CharList_one_0x0079() : base(05, 0x0079)
        {
            //пакеты для входа в Лобби
            /*
            CPU Dump
            Address   Hex dump                                         ASCII (OEM - США)
            4456FCD8  1E 11 48 02|01 00 00 00|00 00 00 00|00 00 00 00| H
            */
            //ns.WriteHex(
            //"0800DD05FEA1C9531140");
            //"0800DD051E114802" +
            //    "0100");
            //расшифрованные данные из снифа пакета
            //3.0.0.7
            // size hash crc idx opcode data
            //"0209 DD05 1E  11  4802   01 00"
            //"0209 DD05 31  11  7900   01 00"

            //3.0.3.0
            //last 1
            //01
            ns.Write((byte)0x01);
            //count 1
            //00
            ns.Write((byte)0x00);
            //дальше данные не считывались из пакета!!!
        }
    }
}
