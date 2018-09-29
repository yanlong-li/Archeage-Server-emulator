using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Network.Packets.Server.Utils;
using LocalCommons.Network;
using LocalCommons.Utilities;
using System.Windows.Documents;
using LocalCommons.World;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCUnitStatePacket_0x0064 : NetPacket
    {
        public NP_SCUnitStatePacket_0x0064(ClientConnection net) : base(01, 0x0064)
        {
            //B504 DD01 6400
            //F52700
            //0B00 4A757374746F636865636B
            //00
            //FF091A00 0000000000000000
            //- <packet id="0x006401" name="SCUnitStatePacket">
            net.CurrentAccount.Character.LiveObjectId = Program.LiveObjectUid.Next(); //liveObjectId d3
            ns.Write((Uint24)net.CurrentAccount.Character.LiveObjectId); //liveObjectId d3 "F52700"
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

            /*if (net.CurrentAccount.Character.Position.X == 0)
            {
                float x = Float24.ToFloat(0x79F6F5);
                float y = Float24.ToFloat(0x74D20C);
                float z = Float24.ToFloat(0x03BC99);
                net.CurrentAccount.Character.Position = new Position(x, y, z);
            }*/
            //F5F679
            //ns.WriteHex("F5F679"); //x d3 
            //0CD274
            //ns.WriteHex("0CD274"); //y d3 
            //99BC03
            //ns.WriteHex("99BC03"); //z d3 

            //записываем uint24
            ns.Write(Helpers.ConvertX(net.CurrentAccount.Character.Position.X), 0, 3);
            ns.Write(Helpers.ConvertY(net.CurrentAccount.Character.Position.Y), 0, 3);
            ns.Write(Helpers.ConvertZ(net.CurrentAccount.Character.Position.Z), 0, 3);

            //0000803F
            ns.Write((float)net.CurrentAccount.Character.Scale); //scale f 
            //03
            ns.Write((byte)net.CurrentAccount.Character.Level); //level c 
            //0B000000
            ns.Write((int)net.CurrentAccount.Character.ModelRef); //modelRef d //archeage:charactermodel = model_id

            //<!--  same as in character packets --> 
            /*
            * инвентарь персонажа
            */
            CharacterInfo.WriteItemInfo(net, net.CurrentAccount.Character);
            //<!--  same as in character packets ends --> 

            //<!--  same as in character packets (2) --> 
            CharacterInfo.WriteStaticData(net, net.CurrentAccount.Character);
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


