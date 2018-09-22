using ArcheAge.ArcheAge.Holders;
using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Network;
using System.Collections.Generic;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCCharacterStatePacket_0x003B : NetPacket
    {
        private void WriteItem(int itemId)
        {
            ns.Write((int)itemId);
            switch (itemId)
            {
                case 0:
                    break;
                default:
                    ns.Write((long)Program.ObjectUid.Next()); //id[1] Q //TODO: сделать у вещей постоянные UID
                    ns.Write((byte)0); //type[1] c
                    ns.Write((byte)0); //flags[1] c
                    ns.Write((int)0x01); //stackSize[1] d
                    byte detailType = 1;
                    ns.Write((byte)detailType); //detailType c
                    switch (detailType)
                    {
                        case 0:
                            break;
                        case 1:
                            ns.WriteHex("000000005500000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"); //detail 51 b
                            break;
                        case 2:
                            ns.WriteHex("0000000000000000000000000000000000000000000000000000000000"); //detail 29 b
                            break;
                        case 3:
                            ns.WriteHex("000000000000"); //detail 6 b
                            break;
                        case 4:
                            ns.WriteHex("000000000000000000"); //detail 9 b
                            break;
                        case 5:
                            ns.Write((long)0); //type d
                            ns.Write((long)0); //x Q
                            ns.Write((long)0); //y Q
                            ns.Write((float)0); //z f
                            break;
                        case 6:
                            ns.WriteHex("00000000000000000000000000000000"); //detail 16 b
                            break;
                        case 7:
                            ns.WriteHex("00000000000000000000000000000000"); //detail 16 b
                            break;
                        case 8:
                            ns.WriteHex("0000000000000000"); //detail 8 b
                            break;
                    }

                    ns.Write((long)0); //creationTime[1] Q
                    ns.Write((int)0); //lifespanMins[1] d
                    ns.Write((int)0); //type[1] d
                    ns.Write((byte)0x0b); //worldId c"
                    ns.Write((long)0); //unsecureDateTime Q
                    ns.Write((long)0); //unpackDateTime Q
                    break;
            }
        }

        public NP_SCCharacterStatePacket_0x003B(ClientConnection net, uint characterId, byte gm) : base(01, 0x003B)
        {
            var accountId = net.CurrentAccount.AccountId;
            Character chr = CharacterHolder.LoadCharacterData(accountId, characterId);

            CharacterHolder.LoadEquipPacksData(chr, chr.Ability[0]); //дополнительно прочитать NewbieClothPackId, NewbieWeaponPackId из таблицы character_equip_packs
            CharacterHolder.LoadClothsData(chr, chr.NewbieClothPackId); //дополнительно прочитать Head,Chest,Legs,Gloves,Feet из таблицы equip_pack_cloths
            CharacterHolder.LoadWeaponsData(chr, chr.NewbieWeaponPackId); //дополнительно прочитать Weapon,WeaponExtra,WeaponRanged,Instrument из таблицы equip_pack_weapons
            CharacterHolder.LoadCharacterBodyCoord(chr, chr.CharRace, chr.CharGender); //дополнительно прочитать body, x, y, z из таблицы charactermodel
            CharacterHolder.LoadZoneFaction(chr, chr.CharRace, chr.CharGender); //дополнительно прочитать FactionId,StartingZoneId из таблицы characters


            //  <packet id="0x003B01" name="SCCharacterStatePacket">
            //4205 DD01 3B00 00000000 1000 0E4FC3755AE17949B1F626620F354A93 00000000 FF091A00 0B00 4A757374746F636865636B010203 C4010000 CE010000 B3000000 65000000
            //0000 00000000 00000000
            //{equip 19 slots
            //00000000
            //00000000
            //5B5B00004618C1000000000000000100000001000000005500000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B00000000000000000000000000000000
            //00000000
            //5C5B00004718C1000000000000000100000001000000004600000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B00000000000000000000000000000000
            //00000000
            //5E5B00004818C1000000000000000100000001000000002300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B00000000000000000000000000000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //D61700004918C1000000000000000100000001000000009100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B00000000000000000000000000000000
            //00000000
            //EF1700004A18C1000000000000000100000001000000008200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B00000000000000000000000000000000
            //3A1800004B18C1000000000000000100000001000000008200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B00000000000000000000000000000000
            //}
            //7F4D0000D85D00000000000000000000000000001B020000000000000000000000000000
            //07 a[0]
            //0B a[1]
            //0B a[2]
            //00000000A8B7CF03 x
            //000000006090A603 y
            //EFFC1043         z
            //03BE1000000400000000000000000000000000803F0000803F0000000000000000000000000000803FCF0100000000803FA60000000000803F000000008FC2353F0000000000000000000000000000803FE37B8BFFAFECEFFFAFECEFFF000000FF00000000800000EF00EF00EE0017D40000000000001000000000000000063BB900D800EE00D400281BEBE100E700F037230000000000640000000000000064000000F0000000000000002BD50000006400000000F9000000E000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000036007F422F530000000000000C302B5300000000000000000C302B530000000000000000B4412F5300000000C200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001E00000000000000000000000000000000000000000000000000000000000000000000000000000000
            //B33F2F5300
            //0000000000000000000000DBFB17C092070000000000000000000056010000460000000000000000000000000000000000000000000000000000000000000092070000000000000000000000000000000000000000000000000000323200C200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000C72C000022A21C5300
            //00000000
            ns.Write((int)0); //iid d
            string guid = "0E4FC3755AE17949B1F626620F354A93";
            ns.WriteHex(guid, guid.Length); //guid_len h, guid b
            ns.Write((int)0); //rwd d
            ns.Write((int)chr.CharacterId); //type d
            ns.WriteUTF8Fixed(chr.CharName, chr.CharName.Length); //name S

            ns.Write((byte)chr.CharRace); //CharRace c
            ns.Write((byte)chr.CharGender); //CharGender c
            ns.Write((byte)chr.Level); //level c
            ns.Write((int)0x001C4); //health d
            ns.Write((int)0x001CE); //mana d
            ns.Write((int)chr.StartingZoneId); //zid d
            ns.Write((int)chr.FactionId); //faction_id d
            string factionName = ""; //factionName SS
            ns.WriteUTF8Fixed(factionName, factionName.Length);
            //-----------------------------
            ns.Write((int)0x00); //type d
            ns.Write((int)0x00); //family d
            //------------------------------------
            // инвентарь персонажа
            //for (int i = 0; i < 19; i++)
            //{
            //equip_slot
            WriteItem(chr.Head); //ES_HEAD
            WriteItem(0); //ES_NECK
            WriteItem(chr.Chest); //ES_CHEST Нагрудник (ткань, кожа, латы)	23387
            WriteItem(0); //ES_WAIST
            WriteItem(chr.Legs); //ES_LEGS Поножи (ткань, кожа, латы) 23388
            WriteItem(chr.Gloves); //ES_HANDS
            WriteItem(chr.Feet); //ES_FEET Обувь (ткань, кожа, латы) 23390
            WriteItem(0); //ES_ARMS
            WriteItem(0); //ES_BACK	
            WriteItem(0); //ES_EAR_1
            WriteItem(0); //ES_EAR_2
            WriteItem(0); //ES_FINGER_1
            WriteItem(0); //ES_FINGER_2
            WriteItem(0); //ES_UNDERSHIRT
            WriteItem(0); //ES_UNDERPANTS
            WriteItem(chr.Weapon); //ES_MAINHAND Оружие
            WriteItem(chr.WeaponExtra); //ES_OFFHAND Дополнительное оружие
            WriteItem(chr.WeaponRanged); //ES_RANGED Лук
            WriteItem(0); //chr.Instrument); //ES_MUSICAL Муз. инструмент (струнный, духовой, ударный)
            //}

            //for (int i = 0; i < 7; i++)
            //{
            ns.Write((int)chr.Type[0]); //type[somehow_special] d 19839 face
            ns.Write((int)chr.Type[1]); //type[somehow_special] d 25372 hair_id
            ns.Write((int)chr.Type[2]); //type[somehow_special] d 
            ns.Write((int)chr.Type[3]); //type[somehow_special] d
            ns.Write((int)chr.Type[4]); //type[somehow_special] d

            ns.Write((int)chr.CharBody); //type[somehow_special] d 539   body
            ns.Write((int)chr.Type[6]); //type[somehow_special] d
                                        //}
            //for (int i = 0; i < 2; i++)
            //{
            //equip_slot
            WriteItem(0); //ES_BACKPACK
            WriteItem(0); //ES_COSPLAY
            //}
            //for (int i = 0; i < 3; i++)
            //{
            ns.Write((byte)chr.Ability[0]); //специализация: 1-FIGHTER нападение, 7-MAGIC волшебство, 6-WILD исцеление,
            //10-LOVE преследование, 5-DEATH мистицизм, 8-VOCATION скрытность
            ns.Write((byte)chr.Ability[1]); //эффект класса 1
            ns.Write((byte)chr.Ability[2]); //эффект класса 2
            //}
            /*
            ns.Write((long)867.27);  //x Q
            ns.Write((long)779.11);  //y Q
            ns.Write((float)247.8); //z f
            */
            ns.WriteHex("00000000A8B7CF03");
            ns.WriteHex("000000006090A603");
            ns.WriteHex("EFFC1043");

            //  <part id="6" name="ext c
            ns.Write((byte)chr.Ext); //ext c
            switch (chr.Ext)
            {
                case 0:
                    break;
                case 1:
                    ns.Write((int)chr.Type[7]); //type d
                    break;
                case 2:
                    ns.Write((int)chr.Type[7]); //type d
                    ns.Write((int)chr.Type[8]); //type d
                    ns.Write((int)chr.Type[9]); //type d
                    break;
                default:
                    ns.Write((int)chr.Type[7]); //type d        4299 hair_color_id
                    ns.Write((int)chr.Type[8]); //type d        4    skin_color_id
                    ns.Write((int)chr.Type[9]); //type d        0
                    ns.Write((int)chr.Type[10]); //type d       0
                    ns.Write((float)chr.Weight[10]); //weight f 1
                    ns.Write((float)chr.Scale); //scale f       1
                    ns.Write((float)chr.Rotate); //rotate f     0
                    ns.Write((short)chr.MoveX); //moveX h       0
                    ns.Write((short)chr.MoveY); //moveY h       0
                    //for (int i = 11; i < 15; i++)
                    //{
                    //ns.Write((int)chr.Type[i]); //type d
                    //ns.Write((float)chr.Weight[i]); //weight f
                    ns.Write((int)chr.Type[11]); //type d          0    face_fixed_decal_asset_0_id
                    ns.Write((float)chr.Weight[11]); //weight f    1    face_fixed_decal_asset_0_weight
                    ns.Write((int)chr.Type[12]); //type d          444  face_fixed_decal_asset_1_id
                    ns.Write((float)chr.Weight[12]); //weight f    1    face_fixed_decal_asset_1_weight
                    ns.Write((int)chr.Type[13]); //type d          170  face_fixed_decal_asset_2_id
                    ns.Write((float)chr.Weight[13]); //weight f    1    face_fixed_decal_asset_2_weight)
                    ns.Write((int)chr.Type[14]); //type d          0    face_fixed_decal_asset_3_id
                    ns.Write((float)chr.Weight[14]); //weight f    0.71 face_fixed_decal_asset_3_weight
                    //}

                    ns.Write((int)chr.Type[15]); //type d             0
                    ns.Write((int)chr.Type[16]); //type d             0 face_normal_map_id
                    ns.Write((int)chr.Type[17]); //type d             0
                    ns.Write((float)chr.Weight[17]); //weight f       1
                    ns.Write((int)chr.Lip); //lip d                   0
                    ns.Write((int)chr.LeftPupil); //leftPupil d       left_pupil_color
                    ns.Write((int)chr.RightPupil); //rightPupil d     right_pupil_color
                    ns.Write((int)chr.Eyebrow); //eyebrow d           eyebrow_color
                    ns.Write((int)chr.Decor); //decor d               deco_color
                    //следующая инструкция пишет: len.stringHex
                    //---ns.Write((short)0x00); //modifiers_len h
                    string subString = chr.Modifiers.Substring(0, 256); //надо отрезать в конце два символа \0\0
                    ns.WriteHex(subString, subString.Length); //modifiers b"
                    break;
            }
            ns.Write((short)0x36); //laborPower h  //очки работы = 5000
            ns.Write((long)0x532F427F); //lastLaborPowerModified Q
            ns.Write((short)0x00); //deadCount h
            ns.Write((long)0x532B300C); //deadTime Q
            ns.Write((int)0x00); //rezWaitDuration d
            ns.Write((long)0x532B300C); //rezTime Q
            ns.Write((int)0x00); //rezPenaltyDuration d
            ns.Write((long)0x532F41B4); //lastWorldLeaveTime Q
            ns.Write((long)0xC2); //moneyAmount Q  Number of copper coins Automatic 1:100:10000 Convert gold coins  //серебро, золото и платина (начало)
            ns.Write((long)0x00); //moneyAmount Q //серебро, золото и платина (продолжение)
            ns.Write((short)0x00); //crimePoint h
            ns.Write((int)0x00); //crimeRecord d
            ns.Write((short)0x00); //crimeScore h
            ns.Write((long)0x00); //deleteRequestedTime Q
            ns.Write((long)0x00); //transferRequestedTime Q
            ns.Write((long)0x00); //deleteDelay Q
            //ns.Write((int) 0x07); //consumedLp d
            ns.Write((long)0x1E); //bmPoint Q  //монеты дару = 30
            ns.Write((int)0x00); //consumedLp d ?
            ns.Write((int)0x00); //? пришлось вставить для выравнивания длины пакета
            ns.Write((long)0x00); //moneyAmount Q
            ns.Write((long)0x00); //moneyAmount Q
            ns.Write((byte)0x00); //autoUseAApoint A"
            ns.Write((int)0x00); //point d
            ns.Write((int)0x00); //gift d
            ns.Write((long)0x00532F3FB3); //updated Q
             //B33F2F5300000000

            ns.Write((float)0x00); //angles[0] f
            //00000000
            ns.Write((float)0x00); //angles[1] f
            //00000000
            ns.WriteHex("DBFB17C0"); //angles[2] f
            //DBFB17C0
            ns.Write((int)0x0C17FBDB); //exp d
            //92070000
            ns.Write((int)0x0792); //recoverableExp d
            //00000000
            ns.Write((int)0x00); //returnDistrictId d
            //00000000
            ns.Write((int)0x00); //returnDistrict d
            //56010000
            ns.Write((int)0x0156); //resurrectionDistrict d
            //46000000
            ns.Write((int)0x46); //abilityExp[0] d
            //00000000
            ns.Write((int)0x00); //abilityExp[1] d
            //00000000
            ns.Write((int)0x00); //abilityExp[2] d
            //00000000
            ns.Write((int)0x00); //abilityExp[3] d
            //00000000
            ns.Write((int)0x00); //abilityExp[4] d
            //00000000
            ns.Write((int)0x00); //abilityExp[5] d
            //00000000
            ns.Write((int)0x00); //abilityExp[6] d
            //00000000
            ns.Write((int)0x00); //abilityExp[7] d
            //92070000
            ns.Write((int)0x0792); //abilityExp[8] d
            //00000000
            ns.Write((int)0x00); //abilityExp[9] d
            //00000000
            ns.Write((int)0x00); //abilityExp[10] d
            //00000000
            ns.Write((int)0x00); //unreadMail d
            //00000000
            ns.Write((int)0x00); //unreadMiaMail d
            //00000000
            ns.Write((int)0x00); //unreadCommercialMail d
            //00000000
            ns.Write((byte)0x32); //numInvenSlots c
            //32
            ns.Write((short)0x0032); //numBankSlots h
            //3200
            ns.Write((long)0xc2); //moneyAmount Q
            //C200000000000000
            ns.Write((long)0x00); //moneyAmount Q
            //0000000000000000
            ns.Write((long)0x00); //moneyAmount Q
            //0000000000000000
            ns.Write((long)0x00); //moneyAmount Q
            //0000000000000000
            ns.Write((byte)0x00); //autoUseAAPoint C
            //00
            ns.Write((int)0x00); //juryPoint d
            //00000000
            ns.Write((int)0x00); //jailSeconds d
            //00000000
            ns.Write((long)0x00); //bountyMoney Q
            //0000000000000000
            ns.Write((long)0x00); //bountyTime Q
            //0000000000000000
            ns.Write((int)0x00); //reportedNo d
            //00000000
            ns.Write((int)0x00); //suspectedNo d
            //00000000
            ns.Write((int)0x2CC7); //totalPlayTime d
            //C72C0000
            ns.Write((long)0x531CA222); //createdTime Q
            //22A21C5300000000
            ns.Write((byte)0x00); //expandedExpert C
            //00
        }
    }
}
