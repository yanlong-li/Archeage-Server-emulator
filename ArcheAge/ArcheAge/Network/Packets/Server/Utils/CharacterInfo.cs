using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network.Packets.Server.Utils
{
    public sealed class CharacterInfo : NetPacket
    {
        public static void WriteItemInfo(ClientConnection net, Character character)
        {
            //------------------------------------
            // инвентарь персонажа
            //------------------------------------
            // 1.костюм
            // 2.Шлем (ткань, кожа, латы)
            // 3.Нагрудник (ткань, кожа, латы)
            // 4.Пояс (ткань, кожа, латы)
            // 5.Наручи (ткань, кожа, латы)
            // 6.Перчатки (ткань, кожа, латы)	
            // 7.Накидка
            // 8.Поножи (ткань, кожа, латы)
            // 9.Обувь (ткань, кожа, латы)
            //10.Ожерелье
            //11.Серьга
            //12.Серьга
            //13.Кольцо
            //14.Кольцо
            //15.Оружие
            //16.Дополнительное оружие
            //17.Лук
            //18.Муз. инструмент (струнный, духовой, ударный)
            //19.Груз (глайдер, торговый груз, местный товар)
            //for (int i = 0; i < 19; i++)
            //{
            //equip_slot//<for id="-1" size="19">
            //equip_slot
            ItemInfo.Write(character.Head); //ES_HEAD
            ItemInfo.Write(0); //ES_NECK
            ItemInfo.Write(character.Chest); //ES_CHEST Нагрудник (ткань, кожа, латы)	23387
            ItemInfo.Write(0); //ES_WAIST
            ItemInfo.Write(character.Legs); //ES_LEGS Поножи (ткань, кожа, латы) 23388
            ItemInfo.Write(character.Gloves); //ES_HANDS
            ItemInfo.Write(character.Feet); //ES_FEET Обувь (ткань, кожа, латы) 23390
            ItemInfo.Write(0); //ES_ARMS
            ItemInfo.Write(0); //ES_BACK	
            ItemInfo.Write(0); //ES_EAR_1
            ItemInfo.Write(0); //ES_EAR_2
            ItemInfo.Write(0); //ES_FINGER_1
            ItemInfo.Write(0); //ES_FINGER_2
            ItemInfo.Write(0); //ES_UNDERSHIRT
            ItemInfo.Write(0); //ES_UNDERPANTS
            ItemInfo.Write(character.Weapon); //ES_MAINHAND Оружие
            ItemInfo.Write(character.WeaponExtra); //ES_OFFHAND Дополнительное оружие
            ItemInfo.Write(character.WeaponRanged); //ES_RANGED Лук
            ItemInfo.Write(character.Instrument); //ES_MUSICAL Муз. инструмент (струнный, духовой, ударный)
            //}
            //for (int i = 0; i < 7; i++)
            //{
            ns.Write((int)character.Type[0]); //type[somehow_special] d 19839 face
            ns.Write((int)character.Type[1]); //type[somehow_special] d 25372 hair_id
            ns.Write((int)character.Type[2]); //type[somehow_special] d 
            ns.Write((int)character.Type[3]); //type[somehow_special] d
            ns.Write((int)character.Type[4]); //type[somehow_special] d

            ns.Write((int)character.CharBody); //type[somehow_special] d 539   body
            ns.Write((int)character.Type[6]); //type[somehow_special] d
            //}

            //for (int i = 0; i < 2; i++)
            //{
            //equip_slot
            ItemInfo.Write(0); //ES_BACKPACK
            ItemInfo.Write(0); //ES_COSPLAY
            //}
        }

        public static void WriteStaticData(ClientConnection net, Character character)
        {
            ns.Write((byte)character.Ext); //ext c
            switch (character.Ext)
            {
                case 0:
                    break;
                case 1:
                    ns.Write((int)character.Type[7]); //type d
                    break;
                case 2:
                    ns.Write((int)character.Type[7]); //type d
                    ns.Write((int)character.Type[8]); //type d
                    ns.Write((int)character.Type[9]); //type d
                    break;
                default:
                    ns.Write((int)character.Type[7]); //type d        4299 hair_color_id
                    ns.Write((int)character.Type[8]); //type d        4    skin_color_id
                    ns.Write((int)character.Type[9]); //type d        0
                    ns.Write((int)character.Type[10]); //type d       0
                    ns.Write((float)character.Weight[10]); //weight f 1
                    ns.Write((float)character.Scale); //scale f       1
                    ns.Write((float)character.Rotate); //rotate f     0
                    ns.Write((short)character.MoveX); //moveX h       0
                    ns.Write((short)character.MoveY); //moveY h       0
                    //for (int i = 11; i < 15; i++)
                    //{
                    ns.Write((int)character.Type[11]); //type d          0    face_fixed_decal_asset_0_id
                    ns.Write((float)character.Weight[11]); //weight f    1    face_fixed_decal_asset_0_weight
                    ns.Write((int)character.Type[12]); //type d          444  face_fixed_decal_asset_1_id
                    ns.Write((float)character.Weight[12]); //weight f    1    face_fixed_decal_asset_1_weight
                    ns.Write((int)character.Type[13]); //type d          170  face_fixed_decal_asset_2_id
                    ns.Write((float)character.Weight[13]); //weight f    1    face_fixed_decal_asset_2_weight)
                    ns.Write((int)character.Type[14]); //type d          0    face_fixed_decal_asset_3_id
                    ns.Write((float)character.Weight[14]); //weight f    0.71 face_fixed_decal_asset_3_weight
                    //}
                    ns.Write((int)character.Type[15]); //type d             0
                    ns.Write((int)character.Type[16]); //type d             0 face_normal_map_id
                    ns.Write((int)character.Type[17]); //type d             0
                    ns.Write((float)character.Weight[17]); //weight f       1
                    ns.Write((int)character.Lip); //lip d                   0
                    ns.Write((int)character.LeftPupil); //leftPupil d       left_pupil_color
                    ns.Write((int)character.RightPupil); //rightPupil d     right_pupil_color
                    ns.Write((int)character.Eyebrow); //eyebrow d           eyebrow_color
                    ns.Write((int)character.Decor); //decor d               deco_color

                    //00EF00EF00EE0017D40000000000001000000000000000063BB900D800EE00D400281BEBE100E700F037230000000000640000000000000064000000F0000000000000002BD50000006400000000F9000000E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
                    //следующая инструкция пишет: len.stringHex
                    //---ns.Write((short)0x00); //modifiers_len h
                    string subString = character.Modifiers.Substring(0, 256); //надо отрезать в конце два символа \0\0
                    ns.WriteHex(subString, subString.Length); //modifiers b"
                    break;
            }
        }


        public CharacterInfo(byte level, int packetId) : base(level, packetId)
        {
        }
    }
}