using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCUnitStatePacket_0x0064 : NetPacket
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

        public NP_SCUnitStatePacket_0x0064(ClientConnection net) : base(01, 0x0064)
        {
            //B504 DD01 6400
            //F52700
            //0B00 4A757374746F636865636B
            //00
            //FF091A00 0000000000000000
            //- <packet id="0x006401" name="SCUnitStatePacket">
            ns.WriteHex("F52700"); //liveObjectId d3 
            ns.WriteUTF8Fixed(net.CurrentAccount.Character.CharName,
                net.CurrentAccount.Character.CharName.Length); //name SS 
            byte type = 0x00;
            ns.Write((byte)type); //<part id="61" name="type c 
            switch (type) //<switch id="61">
            {
                case 0: //<!-- player //  --> 
                    ns.Write((uint)net.CurrentAccount.Character.CharacterId); //objectId d 
                    ns.Write((long)net.CurrentAccount.Character.V); //v Q 
                    break; //</case>
                case 1: //<!-- npc --> 
                    //bc b" size="3 
                    //npcTemplateId d 
                    //type d 
                    break; //</case>
                case 2:
                    //<case id="2">
                    //<!-- slave --> 
                    //type d 
                    //tl h 
                    //type d 
                    //type d 
                    break; //</case>
                case 3:
                    //<case id="3">
                    //tl h 
                    //type d 
                    //buildstep h 
                    break; //</case>
                case 4:
                    //<case id="4">
                    //tl h 
                    //type d 
                    break; //</case>
                case 5:
                    //<case id="5">
                    //<!-- mate --> 
                    //tl h 
                    //type d 
                    //type d 
                    //<!-- 	isPet c"/> --> 
                    break; //</case>
                case 6:
                    //<case id="6">
                    //type Q 
                    //objectId d 
                    break; //</case>
            } //</switch>

            //0000
            string master = ""; //master SS 
            ns.WriteUTF8Fixed(master, master.Length);

            //F5F679
            ns.WriteHex("F5F679"); //x d3 
            //0CD274
            ns.WriteHex("0CD274"); //y d3 
            //99BC03
            ns.WriteHex("99BC03"); //z d3 
            //0000803F
            ns.Write((float)net.CurrentAccount.Character.Scale); //x0000803F); //scale f 
            //03
            ns.Write((byte)net.CurrentAccount.Character.Level); //level c 
            //0B000000
            ns.Write((int)net.CurrentAccount.Character.ModelRef); //modelRef d bd:charactermodel = model_id

            //00000000
            //00000000
            //5B5B00004618C1000000000000000100000001000000005500000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B0000000000000000000000000000000000000000
            //5C5B00004718C1000000000000000100000001000000004600000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B0000000000000000000000000000000000000000
            //5E5B00004818C1000000000000000100000001000000002300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B0000000000000000000000000000000000000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //00000000
            //D61700004918C1000000000000000100000001000000009100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B0000000000000000000000000000000000000000
            //EF1700004A18C1000000000000000100000001000000008200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B000000000000000000000000000000003A180000
            //4B18C1000000000000000100000001000000008200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B000000000000000000000000000000007F4D0000D85D0000
            //00000000
            //00000000000000001B02000000000000000000000000000003BE1000000400000000000000000000000000803F0000803F0000000000000000000000000000803FCF0100000000803FA60000000000803F000000008FC2353F0000000000000000000000000000803FE37B8BFFAFECEFFFAFECEFFF000000FF000000008000
            //
            //<!--  same as in character packets --> 
            //<for id="-1" size="19">
            //equip_slot
            WriteItem(net.CurrentAccount.Character.Head); //ES_HEAD
            WriteItem(0); //ES_NECK
            WriteItem(net.CurrentAccount.Character.Chest); //ES_CHEST Нагрудник (ткань, кожа, латы)	23387
            WriteItem(0); //ES_WAIST
            WriteItem(net.CurrentAccount.Character.Legs); //ES_LEGS Поножи (ткань, кожа, латы) 23388
            WriteItem(net.CurrentAccount.Character.Gloves); //ES_HANDS
            WriteItem(net.CurrentAccount.Character.Feet); //ES_FEET Обувь (ткань, кожа, латы) 23390
            WriteItem(0); //ES_ARMS
            WriteItem(0); //ES_BACK	
            WriteItem(0); //ES_EAR_1
            WriteItem(0); //ES_EAR_2
            WriteItem(0); //ES_FINGER_1
            WriteItem(0); //ES_FINGER_2
            WriteItem(0); //ES_UNDERSHIRT
            WriteItem(0); //ES_UNDERPANTS
            WriteItem(net.CurrentAccount.Character.Weapon); //ES_MAINHAND Оружие
            WriteItem(net.CurrentAccount.Character.WeaponExtra); //ES_OFFHAND Дополнительное оружие
            WriteItem(net.CurrentAccount.Character.WeaponRanged); //ES_RANGED Лук
            WriteItem(net.CurrentAccount.Character.Instrument); //ES_MUSICAL Муз. инструмент (струнный, духовой, ударный)
            //}
            //for (int i = 0; i < 7; i++)
            //{
            ns.Write((int)net.CurrentAccount.Character.Type[0]); //type[somehow_special] d 19839 face
            ns.Write((int)net.CurrentAccount.Character.Type[1]); //type[somehow_special] d 25372 hair_id
            ns.Write((int)net.CurrentAccount.Character.Type[2]); //type[somehow_special] d 
            ns.Write((int)net.CurrentAccount.Character.Type[3]); //type[somehow_special] d
            ns.Write((int)net.CurrentAccount.Character.Type[4]); //type[somehow_special] d

            ns.Write((int)net.CurrentAccount.Character.CharBody); //type[somehow_special] d 539   body
            ns.Write((int)net.CurrentAccount.Character.Type[6]); //type[somehow_special] d
            //}

            //for (int i = 0; i < 2; i++)
            //{
            //equip_slot
            WriteItem(0); //ES_BACKPACK
            WriteItem(0); //ES_COSPLAY
            //}
            //<!--  same as in character packets ends --> 
            //<!--  same as in character packets (2) --> 
            ns.Write((byte)net.CurrentAccount.Character.Ext); //ext c
            switch (net.CurrentAccount.Character.Ext)
            {
                case 0:
                    break;
                case 1:
                    ns.Write((int)net.CurrentAccount.Character.Type[7]); //type d
                    break;
                case 2:
                    ns.Write((int)net.CurrentAccount.Character.Type[7]); //type d
                    ns.Write((int)net.CurrentAccount.Character.Type[8]); //type d
                    ns.Write((int)net.CurrentAccount.Character.Type[9]); //type d
                    break;
                default:
                    ns.Write((int)net.CurrentAccount.Character.Type[7]); //type d        4299 hair_color_id
                    ns.Write((int)net.CurrentAccount.Character.Type[8]); //type d        4    skin_color_id
                    ns.Write((int)net.CurrentAccount.Character.Type[9]); //type d        0
                    ns.Write((int)net.CurrentAccount.Character.Type[10]); //type d       0
                    ns.Write((float)net.CurrentAccount.Character.Weight[10]); //weight f 1
                    ns.Write((float)net.CurrentAccount.Character.Scale); //scale f       1
                    ns.Write((float)net.CurrentAccount.Character.Rotate); //rotate f     0
                    ns.Write((short)net.CurrentAccount.Character.MoveX); //moveX h       0
                    ns.Write((short)net.CurrentAccount.Character.MoveY); //moveY h       0
                    //for (int i = 11; i < 15; i++)
                    //{
                    ns.Write((int)net.CurrentAccount.Character.Type[11]); //type d          0    face_fixed_decal_asset_0_id
                    ns.Write((float)net.CurrentAccount.Character.Weight[11]); //weight f    1    face_fixed_decal_asset_0_weight
                    ns.Write((int)net.CurrentAccount.Character.Type[12]); //type d          444  face_fixed_decal_asset_1_id
                    ns.Write((float)net.CurrentAccount.Character.Weight[12]); //weight f    1    face_fixed_decal_asset_1_weight
                    ns.Write((int)net.CurrentAccount.Character.Type[13]); //type d          170  face_fixed_decal_asset_2_id
                    ns.Write((float)net.CurrentAccount.Character.Weight[13]); //weight f    1    face_fixed_decal_asset_2_weight)
                    ns.Write((int)net.CurrentAccount.Character.Type[14]); //type d          0    face_fixed_decal_asset_3_id
                    ns.Write((float)net.CurrentAccount.Character.Weight[14]); //weight f    0.71 face_fixed_decal_asset_3_weight
                    //}
                    ns.Write((int)net.CurrentAccount.Character.Type[15]); //type d             0
                    ns.Write((int)net.CurrentAccount.Character.Type[16]); //type d             0 face_normal_map_id
                    ns.Write((int)net.CurrentAccount.Character.Type[17]); //type d             0
                    ns.Write((float)net.CurrentAccount.Character.Weight[17]); //weight f       1
                    ns.Write((int)net.CurrentAccount.Character.Lip); //lip d                   0
                    ns.Write((int)net.CurrentAccount.Character.LeftPupil); //leftPupil d       left_pupil_color
                    ns.Write((int)net.CurrentAccount.Character.RightPupil); //rightPupil d     right_pupil_color
                    ns.Write((int)net.CurrentAccount.Character.Eyebrow); //eyebrow d           eyebrow_color
                    ns.Write((int)net.CurrentAccount.Character.Decor); //decor d               deco_color

                    //00EF00EF00EE0017D40000000000001000000000000000063BB900D800EE00D400281BEBE100E700F037230000000000640000000000000064000000F0000000000000002BD50000006400000000F9000000E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
                    //следующая инструкция пишет: len.stringHex
                    //---ns.Write((short)0x00); //modifiers_len h
                    string subString = net.CurrentAccount.Character.Modifiers.Substring(0, 256); //надо отрезать в конце два символа \0\0
                    ns.WriteHex(subString, subString.Length); //modifiers b"
                    break;
            }

            //<!--  same as in character packets (2) ends --> 
            //
            ns.WriteHex("000000"); //bc b" size="3 
            //90B00000
            ns.Write((int)0xb090); //preciseHealth d 
            //78B40000
            ns.Write((int)0xb478); //preciseMana d 
            //<!--  this part is not 100% correct, need more sniffs --> 
            //FF
            byte point = 0xff; 
            ns.Write((byte)point); //point c" id="8 
            switch (point) //<switch id="8">
            {
                case 0xff: //<case id="-1 
                    break;
                default: //<case id="default">
                    ns.WriteHex("000000"); //bc d3 
                    break; //</case>
            } //</switch>

            //FF
            point = 0xff; //point c" id="9 
            ns.Write((byte)point); //point c" id="9 
            switch (point) //<switch id="9">
            {
                case 0xff: //<case id="-1 
                    break;
                default: //<case id="default">
                    ns.WriteHex("000000"); //bc d3 
                    ns.Write((byte)0x00); //kind c 
                    ns.Write((int)0x00); //space d 
                    ns.Write((int)0x00); //spot d 
                    //<!--  to do need sniff --> 
                    break; //</case>
            } //</switch>

            //<!--  this part is not 100% correct, need more sniffs ends --> 
            //00
            type = 0;
            ns.Write((byte)type); //<part id="60" name="type c 
            byte isLooted = 0x00; //00
            ns.Write((byte)isLooted); //isLooted c 
            switch (type) //<switch id="60">
            {
                case 1: //<case id="1">
                    //00
                    ns.Write((byte)0x00); //door[1/2] c 
                    //02
                    ns.Write((byte)0x02); //door[2/2] c 
                    //AB
                    ns.Write((byte)0xab); //window[1/6] c 
                    //29
                    ns.Write((byte)0x28); //window[2/6] c 
                    //00
                    ns.Write((byte)0x00); //window[3/6] c 
                    //00
                    ns.Write((byte)0x00); //window[4/6] c 
                    //01
                    ns.Write((byte)0x01); //window[5/6] c 
                    //00
                    ns.Write((byte)0x00); //window[6/6] c 
                    break; //</case>
                case 4: //<case id="4">
                    ns.Write((int)0x00); //type d 
                    ns.Write((byte)0x00); //activate c 
                    break; //</case>
                case 7: //<case id="7">
                    ns.Write((int)0x00); //type d 
                    ns.Write((float)0x00); //growRate f 
                    ns.Write((int)0x00); //randomSeed d 
                    ns.Write((byte)0x00); //isWithered c 
                    ns.Write((byte)0x00); //isHarvested c 
                    break; //</case>
                case 8: //<case id="8">
                    ns.Write((float)0x00); //pitch f 
                    ns.Write((float)0x00); //yaw f 
                    break; //</case>
                default: //<case id="default 
                    break;
            } //</switch>

            //00
            byte activeWeapon = 0x00;
            ns.Write((byte)activeWeapon); //activeWeapon c 
            //02
            byte learnedSkillCount = 02;
            ns.Write((byte)learnedSkillCount); //<part id="16" name="learnedSkillCount c 
            //for (int i = 0; i < learnedSkillCount; i++) //<for id="16">
            //{ 
            //(1)
            //AB280000
            ns.Write((int)0x28AB); //type d 
            //01
            ns.Write((byte)0x01); //level c 
            //(2)
            //002A0000
            ns.Write((int)0x2A00); //type d 
            //01
            ns.Write((byte)0x01); //level c 
            //} //</for>

            //00000000
            int learnedBuffCount = 0;
            ns.Write((int)learnedBuffCount); //<part id="17" name="learnedBuffCount d 
            for (int i = 0; i < learnedBuffCount; i++) //<for id="17">
            {
                ns.Write((int)0x00);//type d 
            } //</for>
            //00
            ns.Write((byte)0x00); //rot.x c 
            //00
            ns.Write((byte)0x00); //rot.y c 
            //D0
            ns.Write((byte)0xD0); //rot.z c 
            //21
            int rg = 0;
            rg += net.CurrentAccount.Character.CharGender << 4;
            rg += net.CurrentAccount.Character.CharRace;
            ns.Write((byte)rg); //Gender + Race c 
            //00
            byte pish = 0;
            ns.Write((byte)pish); //pish c" id="202 
            switch (pish) //<switch id="202">
            {
                case 0: //<case id="0">
                    //00000000
                    ns.Write((byte)0x00); //pisc b" size="4 
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    break; //</case>
                case 4: //<case id="4">
                    ns.Write((byte)0x00); //pisc b" size="5 
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    break; //</case>
                case 8: //<case id="8">
                    ns.Write((byte)0x00); //pisc b" size="6
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    ns.Write((byte)0x00);
                    break; //</case>
            } //</switch>
            //00
            pish = 0;
            ns.Write((byte)pish); //pish c" id="203
            switch (pish) //<switch id="203">
            {
                case 0: //<case id="0">
                    //65000000
                    ns.Write((int)net.CurrentAccount.Character.FactionId); //factionId d 
                    break; //</case>
                case 1: //<case id="1">
                    ns.Write((int)net.CurrentAccount.Character.FactionId); //factionId d 
                    ns.Write((byte)0x00);//unk c 
                    break; //</case>
                case 4: //<case id="4">
                    ns.WriteHex("0000000000"); //pisc b" size="5 
                    break; //</case>
                case 5: //<case id="5">
                    ns.WriteHex("000000000000"); //pisc b" size="6 
                    break; //</case>
                case 16: //<case id="16">
                    ns.WriteHex("00000000000000000000000000"); //pisc b" size="13 
                    break; //</case>
                case 20: //<case id="20">
                    ns.WriteHex("00000000"); //pisc b" size="4 
                    break; //</case>
                case 21: //<case id="21">
                    //govId h 
                    ns.Write((int)net.CurrentAccount.Character.FactionId); //FactionId h 
                    ns.WriteHex("00"); //unk b" size="3 
                    break; //</case>
            } //</switch>

                       //000000000000000000FF00000000FF00000000FF00000000FF00000000FF00000000FF920700000000000000FF00000000FF00000000FF01070000001E3C320028640101010000000001020000007709000000F5270000000000010100204E0000000000000000000000000000010000000000000000000000000101000000B119000000F527000000000001010000000000000000000000000000000000010000000000000000000000
            ns.WriteHex("000000000000000000FF00000000FF00000000FF00000000FF00000000FF00000000FF920700000000000000FF00000000FF00000000FF01070000001E3C320028640101010000000001020000007709000000F5270000000000010100204E0000000000000000000000000000010000000000000000000000000101000000B119000000F527000000000001010000000000000000000000000000000000010000000000000000000000");
            //flags (SEE XML) h" id="20 
            //<!--  the rest of packet depends on some bits in some flags, need a lot of time to figure this out --> 
            //<bitwise_switch id="20">
            //<!-- 
            //<case id="0x80">
            //				<for id="-1" size="6">
            //					gmmode c"/>
            //				</for>
            //			</case>
            //--> 
            //<case id="0x100">
            //bc b" size="3 
            //firstHitterTeamId d 
            //</case>
            //<case id="0x35">
            //unk h 
            //</case>
            //<case id="default 
            //</bitwise_switch>
            //<switch id="61">
            //<case id="0">
            //<for id="-1" size="10">
            //exp d 
            //order c 
            //</for>
            //nActive c" id="24 
            //<for id="24">
            //active c 
            //</for>
            //bc b" size="3 
            //stp b" size="6 
            //helmet c 
            //back_holdable c 
            //cosplay c 
            //</case>
            //<case id="default 
            //</switch>
            //goodBuffCount c" id="25 
            //<for id="25">
            //id d 
            //type d 
            //type c" id="100 
            //<switch id="100">
            //<case id="0">
            //bc b" size="3 
            //</case>
            //<case id="1">
            //bc b" size="3 
            //</case>
            //<case id="2">
            //bc b" size="3 
            //itemId Q 
            //itemType d 
            //type c 
            //type d 
            //</case>
            //<case id="3">
            //bc b" size="3 
            //mountSkillType d 
            //</case>
            //<case id="4">
            //bc b" size="3 
            //</case>
            //<case id="default 
            //</switch>
            //type d 
            //sourceLevel c 
            //sourceAbLevel h 
            //totalTime d 
            //elapsedTime d 
            //tickTime d 
            //tickIndex d 
            //stack d 
            //charged d 
            //cooldownSkill d 
            //</for>
            //badBuffCount c" id="26 
            //<for id="26">
            //id d 
            //type d 
            //type c" id="100 
            //<switch id="100">
            //<case id="0">
            //bc b" size="3 
            //</case>
            //<case id="1">
            //bc b" size="3 
            //</case>
            //<case id="2">
            //bc b" size="3 
            //itemId Q 
            //itemType d 
            //type c 
            //type d 
            //</case>
            //<case id="3">
            //bc b" size="3 
            //mountSkillType d 
            //</case>
            //<case id="4">
            //bc b" size="3 
            //</case>
            //<case id="default 
            //</switch>
            //type d 
            //sourceLevel c 
            //sourceAbLevel h 
            //totalTime d 
            //elapsedTime d 
            //tickTime d 
            //tickIndex d 
            //stack d 
            //charged d 
            //cooldownSkill d 
            //</for>
            //hiddenBuffCount c" id="27 
            //<for id="27">
            //id d 
            //type d 
            //type c" id="100 
            //<switch id="100">
            //<case id="0">
            //bc b" size="3 
            //</case>
            //<case id="1">
            //bc b" size="3 
            //</case>
            //<case id="2">
            //bc b" size="3 
            //itemId Q 
            //itemType d 
            //type c 
            //type d 
            //</case>
            //<case id="3">
            //bc b" size="3 
            //mountSkillType d 
            //</case>
            //<case id="4">
            //bc b" size="3 
            //</case>
            //<case id="default 
            //</switch>
            //type d 
            //sourceLevel c 
            //sourceAbLevel h 
            //totalTime d 
            //elapsedTime d 
            //tickTime d 
            //tickIndex d 
            //stack d 
            //charged d 
            //cooldownSkill d 
        } //</for>
    }
}


