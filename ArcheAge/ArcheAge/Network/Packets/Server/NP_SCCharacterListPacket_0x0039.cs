using ArcheAge.ArcheAge.Holders;
using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Network;
using System.Collections.Generic;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_CharacterListPacket_0x0039 : NetPacket
    {
        private void WriteItem(int itemId)
        {
            ns.Write((int)itemId);
            if (itemId <= 0)
            {
                return;
            }

            ns.Write((int)0x01);
            for (int i = 0; i < 6; i++)
            {
                ns.Write((byte)0x00);
            }

            ns.Write((byte)0x01);
            for (int i = 0; i < 3; i++)
            {
                ns.Write((byte)0x00);
            }

            ns.Write((byte)0x01);
            for (int i = 0; i < 4; i++)
            {
                ns.Write((byte)0x00);
            }

            ns.Write((byte)0x55);
            for (int i = 0; i < 62; i++)
            {
                ns.Write((byte)0x00);
            }

            ns.Write((byte)0x0B);
            for (int i = 0; i < 16; i++)
            {
                ns.Write((byte)0x00);
            }
        }

        /// <summary>
        /// пакет для входа в Лобби
        /// CharacterListPacket01_0x0039
        /// author: NLObP
        /// </summary>
        /// <param name="net"></param>
        /// <param name="num">номер по порядку персонажа (от общего количества), которого выводим в пакете</param>
        /// <param name="last">0 - ещё ожидается пакет CharacterList, 1 - последний пакет, больше не будет</param>
        public NP_CharacterListPacket_0x0039(ClientConnection net, int num, int last) : base(01, 0x0039)
        {
            var accountId = net.CurrentAccount.AccountId;
            List<Character> charList = CharacterHolder.LoadCharacterData(accountId);
            var totalChars = CharacterHolder.GetCount();

            byte race = 1;
            byte gender = 1;
            int face = 19838;
            int body = 536;
            int id = 298;
            int model_id = 10;
            int hair_id = 24133;
            int hair_color_id = 733;
            int skin_color_id = 1;
            int face_movable_decal_asset_id = 0;
            int face_movable_decal_scale = 1;
            int face_movable_decal_rotate = 0;
            int face_movable_decal_move_x = 0;
            int face_movable_decal_move_y = 0;
            int face_fixed_decal_asset_0_id = 0;
            int face_fixed_decal_asset_1_id = 0;
            int face_fixed_decal_asset_2_id = 560;
            int face_fixed_decal_asset_3_id = 682;
            int face_diffuse_map_id = 0;
            int face_normal_map_id = 29;
            int face_eyelash_map_id = 0;
            int lip_color = 0;
            long left_pupil_color = 4294489434;
            long right_pupil_color = 4294489434;
            long eyebrow_color = 4278199100;
            int owner_type_id = 1;
            float face_movable_decal_weight = 1;
            float face_fixed_decal_asset_0_weight = 1;
            float face_fixed_decal_asset_2_weight = 1;
            float face_normal_map_weight = 1;
            long deco_color = 4282924640;
            string name = "nu_m_face0001";
            int npcOnly = 0; //'f';
            string modifier =
                "00F5000011DC000B00000000170000000000F323000000003D00000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            float face_fixed_decal_asset_3_weight = 1;
            float face_fixed_decal_asset_1_weight = 1;

            //item
            //Id: 58; Name: 'Простые латные доспехи';
            int head = 20924;
            int chest = 20951;
            int legs = 20978;
            int gloves = 21005;
            int feet = 21032;

            int weapon = 1278;
            int weaponExtra = 1313;
            int weaponRanged = 0;
            int instrument = 0;

            //------------------------------------------------------------------------------------------------
            ns.Write((byte)last); //last c
            if (totalChars == 0)
            {
                ns.Write((byte)0); //totalChars); //count c
                return; //если пустой список, заканчиваем работу
            }
            else
            {
                ns.Write((byte)1); //totalChars); //count c
            }
            int aa = 0;
            foreach (Character chr in charList)
            {
                if (num == aa) //параметр NUM отвечает, которого чара выводить в пакете (может быть от 0 до 2)
                {
                    ns.Write((int)chr.CharacterId); //type d
                    ns.WriteUTF8Fixed(chr.CharName, chr.CharName.Length); //name S
                    ns.Write((byte)chr.CharRace); //CharRace c
                    ns.Write((byte)chr.CharGender); //CharGender c
                    ns.Write((byte)chr.Level); //level c
                    ns.Write((int)0x001C4); //health d
                    ns.Write((int)0x001CE); //mana d
                    ns.Write((int)179); //starting_zone_id : поле в таблице characters //zid d
                    ns.Write((int)101); //faction_id : поле в таблице characters //type d
                    string factionName = ""; //factionName SS
                    ns.WriteUTF8Fixed(factionName, factionName.Length);
                    //-----------------------------
                    ns.Write((int)0x00); //type d
                    ns.Write((int)0x00); //family d
                    //------------------------------------
                    // инвентарь персонажа
                    //------------------------------------
                    // 1.шлем (0 голова)
                    // 2.нагрудник (23387 грубая рубаха )
                    // 3.пояс (0 )
                    // 4.наручи (0 )
                    // 5.перчатки (0 )
                    // 6.плащ (0 )
                    // 7.поножи (23388 грубые штаны)
                    // 8.обувь (23390 грубые башмаки)
                    // 9. нижнее белье
                    //10. ожерелье
                    //11.серьга
                    //12.серьга
                    //13.кольцо
                    //14.кольцо
                    //15.оружие, правая рука
                    //16.дополнительное оружие, левая рука
                    //17.оружие дальноего боя
                    //18.инструмент
                    //19.груз
                    //for (int i = 0; i < 19; i++)
                    {
                        WriteItem(0); //костюм 
                        WriteItem(0); //шлем
                        WriteItem(23387); //нагрудник
                        WriteItem(0); //пояс
                        WriteItem(23388); //поножи
                        WriteItem(0); //наручи
                        WriteItem(23390); //обувь
                        WriteItem(0); //плащ
                        WriteItem(0); //нижнее белье
                        WriteItem(0); //нижнее белье
                        WriteItem(0); //ожерелье
                        WriteItem(0); //серьга
                        WriteItem(0); //серьга
                        WriteItem(0); //кольцо
                        WriteItem(0); //кольцо
                        WriteItem(6102); //оружие, правая рука
                        WriteItem(0); //дополнительное оружие, левая рука
                        WriteItem(0); //WriteItem(6127); //оружие дальноего боя
                        WriteItem(0); //WriteItem(6202); //инструмент
                                      //WriteItem(0); //груз
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        ns.Write((int)chr.Type[i]); //type[somehow_special] d
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        WriteItem(0);
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        ns.Write((byte)chr.Ability[i]); //ability[] c
                    }

                    ns.Write((long)7975971); //x Q
                    ns.Write((long)7875611); //y Q
                    ns.Write((float)226392); //z f

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
                            ns.Write((int)chr.Type[7]); //type d
                            ns.Write((int)chr.Type[8]); //type d
                            ns.Write((int)chr.Type[9]); //type d
                            ns.Write((int)chr.Type[10]); //type d
                            ns.Write((float)chr.Weight[10]); //weight f
                            ns.Write((float)chr.Scale); //scale f
                            ns.Write((float)chr.Rotate); //rotate f
                            ns.Write((short)chr.MoveX); //moveX h
                            ns.Write((short)chr.MoveY); //moveY h
                            for (int i = 11; i < 15; i++)
                            {
                                ns.Write((int)chr.Type[i]); //type d
                                ns.Write((float)chr.Weight[i]); //weight f
                            }

                            ns.Write((int)chr.Type[15]); //type d
                            ns.Write((int)chr.Type[16]); //type d
                            ns.Write((int)chr.Type[17]); //type d
                            ns.Write((float)chr.Weight[17]); //weight f
                            ns.Write((int)chr.Lip); //lip d
                            ns.Write((int)chr.LeftPupil); //leftPupil d
                            ns.Write((int)chr.RightPupil); //rightPupil d
                            ns.Write((int)chr.Eyebrow); //eyebrow d
                            ns.Write((int)chr.Decor); //decor d
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
                }
                ++aa;
            }
        }
    }
}
