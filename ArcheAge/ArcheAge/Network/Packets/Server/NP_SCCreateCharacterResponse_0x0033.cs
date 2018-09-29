using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Network.Packets.Server.Utils;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Network;
using LocalCommons.Utilities;

namespace ArcheAge.ArcheAge.Network.Packets.Server
{
    public sealed class NP_SCCreateCharacterResponse_0x0033 : NetPacket
    {
        public NP_SCCreateCharacterResponse_0x0033(ClientConnection net, int num, int last) : base(01, 0x0033)
        {
            //            var accountId = net.CurrentAccount.AccountId;
            //            List<Character> charList = CharacterHolder.LoadCharacterData(accountId);
            //            var totalChars = CharacterHolder.GetCount();
            //
            //            //------------------------------------------------------------------------------------------------
            //            ns.Write((byte)last); //last c
            //            if (totalChars == 0)
            //            {
            //                ns.Write((byte)0); //totalChars); //count c
            //                return; //если пустой список, заканчиваем работу
            //            }
            //            else
            //            {
            //                ns.Write((byte)1); //totalChars); //count c"
            //            }
            //
            //            int aa = 0;
            //            foreach (Character chr in charList)
            //            {
            //                if (num == aa) //параметр NUM отвечает, которого чара выводить в пакете (может быть от 0 до 2)
            //                {

            Character chr = net.CurrentAccount.Character;

            ns.Write((int)chr.CharacterId); //type d
            ns.WriteUTF8Fixed(chr.CharName, chr.CharName.Length); //name S
            ns.Write((byte)chr.CharRace); //CharRace c
            ns.Write((byte)chr.CharGender); //CharGender c
            ns.Write((byte)chr.Level); //level c
            ns.Write((int)0x001C4); //health d
            ns.Write((int)0x001CE); //mana d
            ns.Write((int)chr.StartingZoneId); //zid d
            ns.Write((int)chr.FactionId); //faction_id d
                                          //ns.Write((int)179); //starting_zone_id : поле в таблице characters //zid d
                                          //ns.Write((int)101); //faction_id : поле в таблице characters //type d
            string factionName = ""; //factionName SS
            ns.WriteUTF8Fixed(factionName, factionName.Length);
            //-----------------------------
            ns.Write((int)0x00); //type d
            ns.Write((int)0x00); //family d

            //<!--  same as in character packets --> 
            /*
            * инвентарь персонажа
            */
            CharacterInfo.WriteItemInfo(net, net.CurrentAccount.Character);
            //<!--  same as in character packets ends --> 

            for (int i = 0; i < 3; i++)
            {
                ns.Write((byte)chr.Ability[i]); //ability[] c
            }

            //position
            //ns.Write(Float24.ToFloat24(chr.Position.X));
            //ns.Write(Float24.ToFloat24(chr.Position.Y));
            //ns.Write(Float24.ToFloat24(chr.Position.Z));
            ns.Write((int)0x00);
            ns.Write((int)Float24.ToInt32(net.CurrentAccount.Character.Position.X));
            ns.Write((int)0x00);
            ns.Write((int)Float24.ToInt32(net.CurrentAccount.Character.Position.Y));
            ns.Write((float)net.CurrentAccount.Character.Position.Z);

            //<!--  same as in character packets (2) --> 
            CharacterInfo.WriteStaticData(net, net.CurrentAccount.Character);
            //<!--  same as in character packets (2) ends --> 

            ns.Write((short)0x36); //laborPower h //очки работы = 5000
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
            ns.Write((int)0x07); //consumedLp d
            ns.Write((long)0x1E); //bmPoint Q  //монеты дару = 30
            ns.Write((long)0x00); //moneyAmount Q
            ns.Write((long)0x00); //moneyAmount Q
            ns.Write((byte)0x00); //autoUseAApoint A
            ns.Write((int)0x00); //point d
            ns.Write((int)0x00); //gift d
            ns.Write((long)0x00532F3FB3); //updated Q
                                          //                }
                                          //                ++aa;
                                          //            }
        }
    }
}
