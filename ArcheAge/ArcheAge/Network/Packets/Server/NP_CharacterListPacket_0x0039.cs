using System;
using ArcheAge.ArcheAge.Holders;
using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;
using System.Collections.Generic;
using Character = ArcheAge.ArcheAge.Structuring.Character;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_CharacterListPacket_0x0039 : NetPacket
    {
        public void WriteItem(int itemId)
        {
            ns.Write((int)itemId);
            if (itemId > 0)
            {
                ns.Write((int)0x01);
                for (int i = 0; i < 6; i++)
                    ns.Write((byte)0x00);
               ns.Write((byte)0x01);
                for (int i = 0; i < 3; i++)
                    ns.Write((byte)0x00);
                ns.Write((byte)0x01);
                for (int i = 0; i < 4; i++)
                    ns.Write((byte)0x00);
                ns.Write((byte)0x55);
                for (int i = 0; i < 62; i++)
                    ns.Write((byte)0x00);
                ns.Write((byte)0x0B);
                for (int i = 0; i < 16; i++)
                {
                    ns.Write((byte)0x00);
                }
            }
        }

        /// <summary>
        /// пакет для входа в Лобби
        /// CharacterListPacket01_0x0039
        /// 
        /// author: NLObP
        /// </summary>
        /// <param name="net"></param>
        public NP_CharacterListPacket_0x0039(ClientConnection net) : base(01, 0x0039)
        {
            var accountId = net.CurrentAccount.AccountId;
            List<Character> charList = CharacterHolder.LoadCharacterData((int)accountId);
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
            ns.Write((byte)0x01); //"last" type="c"
            ns.Write((byte)totalChars); //"count" type="c"

            if (totalChars ==0) return; //если пустой список, заканчиваем работу
            
            foreach (Character chr in charList) //<for id="0">
            {
                ns.Write((int) chr.Type[0]); //  "type" type="d"
                ns.WriteUTF8Fixed(chr.CharName, chr.CharName.Length); //  "name" type="S"
                ns.Write((byte) chr.CharRace); //  "CharRace" type="c"
                ns.Write((byte) chr.CharGender); //  "CharGender" type="c"
                ns.Write((byte) chr.Level); //  "level" type="c"
                ns.Write((int) 0x001C4); //  "health" type="d"
                ns.Write((int) 0x001CE); //  "mana" type="d"
                ns.Write((int) 179); //starting_zone_id : поле в таблице characters //  "zid" type="d"
                ns.Write((int) 101); //faction_id : поле в таблице characters //  "type" type="d"
                string factionName = ""; //  "factionName" type="SS"
                ns.WriteUTF8Fixed(factionName, factionName.Length);
                //-----------------------------
                ns.Write((int) 0x00); //  "type" type="d"
                ns.Write((int) 0x00); //  "family" type="d"
                //------------------------------------
                //цикл по инвентарю герою
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
                    WriteItem(6102); //двуручное оружие
                    WriteItem(0); //дополнительное оружие
                    WriteItem(0); //WriteItem(6127); //оружие дальноего боя
                    WriteItem(0);//WriteItem(6202); //инструмент
                    //WriteItem(0); //груз
                }
                for (int i = 0; i < 7; i++)
                {
                    ns.Write((int) chr.Type[i]); //"type[somehow_special]" type="d"
                }
                for (int i = 0; i < 2; i++)
                {
                    WriteItem(0);
                }
                for (int i = 0; i < 3; i++)
                {
                    ns.Write((byte) chr.Ability[i]); //"ability[]" type="c"
                }
                ns.Write((long) 0x00); //"x" type="Q"
                ns.Write((long) 0x00); //"y" type="Q"
                ns.Write((float) 0x00); //"z" type="f"

                ns.Write((byte) chr.Ext); //"ext" type="c"
                switch (chr.Ext) //<switch id="6">
                {
                    case 0: //  <case id="0">
                        break; //  </case>
                    case 1: //  <case id="1">
                        ns.Write((int) chr.Type[7]); //    "type" type="d"
                        break; //  </case>
                    case 2: //  <case id="2">
                        ns.Write((int) chr.Type[7]); //    "type" type="d"
                        ns.Write((int) chr.Type[8]); //    "type" type="d"
                        ns.Write((int) chr.Type[9]); //    "type" type="d"
                        break; //  </case>
                    default: //  <case id="default">
                        ns.Write((int) chr.Type[7]); //    "type" type="d"
                        ns.Write((int) chr.Type[8]); //    "type" type="d"
                        ns.Write((int) chr.Type[9]); //    "type" type="d"
                        ns.Write((int) chr.Type[10]); //    "type" type="d"
                        ns.Write((float) chr.Weight[10]); //    "weight" type="f"
                        ns.Write((float) chr.Scale); //    "scale" type="f"
                        ns.Write((float) chr.Rotate); //    "rotate" type="f"
                        ns.Write((short) chr.MoveX); //    "moveX" type="h"
                        ns.Write((short) chr.MoveY); //    "moveY" type="h"
                        for (int i = 11; i < 15; i++) //    <for id="-1" size="4">
                        {
                            ns.Write((int) chr.Type[i]); //      "type" type="d"
                            ns.Write((float) chr.Weight[i]); //      "weight" type="f"
                        } //    </for>
                        ns.Write((int) chr.Type[15]); //    "type" type="d"
                        ns.Write((int) chr.Type[16]); //    "type" type="d"
                        ns.Write((int) chr.Type[17]); //    "type" type="d"
                        ns.Write((float) chr.Weight[17]); //    "weight" type="f"
                        ns.Write((int) chr.Lip); //    "lip" type="d"
                        ns.Write((int) chr.LeftPupil); //    "leftPupil" type="d"
                        ns.Write((int) chr.RightPupil); //    "rightPupil" type="d"
                        ns.Write((int) chr.Eyebrow); //    "eyebrow" type="d"
                        ns.Write((int) chr.Decor); //    "decor" type="d"
                        //следующая инструкция пишет: len.stringHex
                        //---ns.Write((short)0x00); //    "modifiers_len" type="h"
                        string subString = chr.Modifiers.Substring(0, 256); //надо отрезать в конце два символа \0\0
                        ns.WriteHex(subString, subString.Length); //    "modifiers" type="b"
                        break; //  </case>
                } //</switch>
                ns.Write((short)0x36); //"laborPower" type="h"  //очки работы = 5000
                ns.Write((long) 0x532F427F); //"lastLaborPowerModified" type="Q"
                ns.Write((short) 0x00); //"deadCount" type="h"
                ns.Write((long) 0x532B300C); //"deadTime" type="Q"
                ns.Write((int) 0x00); //"rezWaitDuration" type="d"
                ns.Write((long)0x532B300C); //"rezTime" type="Q"
                ns.Write((int) 0x00); //"rezPenaltyDuration" type="d"
                ns.Write((long) 0x532F41B4); //"lastWorldLeaveTime" type="Q"
                ns.Write((long) 0xC2); //"moneyAmount" type="Q"  Number of copper coins Automatic 1:100:10000 Convert gold coins  //серебро, золото и платина (начало)
                ns.Write((long) 0x00); //"moneyAmount" type="Q" //серебро, золото и платина (продолжение)
                ns.Write((short) 0x00); //"crimePoint" type="h"
                ns.Write((int) 0x00); //"crimeRecord" type="d"
                ns.Write((short) 0x00); //"crimeScore" type="h"
                ns.Write((long) 0x00); //"deleteRequestedTime" type="Q"
                ns.Write((long) 0x00); //"transferRequestedTime" type="Q"
                ns.Write((long) 0x00); //"deleteDelay" type="Q"
                //ns.Write((int) 0x07); //"consumedLp" type="d"
                ns.Write((long)0x1E); //"bmPoint" type="Q"  //монеты дару = 30
                ns.Write((int)0x00); //"consumedLp" type="d" ?
                ns.Write((int)0x00); //? пришлось вставить для выравнивания длины пакета
                ns.Write((long) 0x00); //"moneyAmount" type="Q"
                ns.Write((long) 0x00); //"moneyAmount" type="Q"
                ns.Write((byte) 0x00); //"autoUseAApoint" type="A"
                ns.Write((int) 0x00); //"point" type="d"
                ns.Write((int) 0x00); //"gift" type="d"
                ns.Write((long) 0x00532F3FB3); //"updated" type="Q"
            }
        }
    }
}
