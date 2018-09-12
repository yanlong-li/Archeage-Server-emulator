using ArcheAge.ArcheAge.Holders;
using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Network;
using System.Collections.Generic;

namespace ArcheAge.ArcheAge.Network.Packets.Server
{
    public sealed class NP_SCCreateCharacterResponse_0x0033 : NetPacket
    {
        private void WriteItem(int itemId)
        {
            ns.Write((int)itemId);
            switch (itemId)
            {
                case 0:
                    break;
                default:
                    ns.Write((long)0x01); //id[1] Q
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

        public NP_SCCreateCharacterResponse_0x0033(ClientConnection net, int num, int last) : base(01, 0x0033)
        {
            var accountId = net.CurrentAccount.AccountId;
            List<Character> charList = CharacterHolder.LoadCharacterData(accountId);
            var totalChars = CharacterHolder.GetCount();

            //------------------------------------------------------------------------------------------------
            ns.Write((byte)last); //last c
            if (totalChars == 0)
            {
                ns.Write((byte)0); //totalChars); //count c
                return; //если пустой список, заканчиваем работу
            }
            else
            {
                ns.Write((byte)1); //totalChars); //count c"
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
                    for (int i = 0; i < 7; i++)
                    {
                        ns.Write((int)chr.Type[i]); //type[somehow_special] d
                    }

                    WriteItem(0); //
                    WriteItem(0); //

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
                            break; //</case>
                        case 1:
                            ns.Write((int)chr.Type[7]); //type d
                            break; //</case>
                        case 2:
                            ns.Write((int)chr.Type[7]); //type d
                            ns.Write((int)chr.Type[8]); //type d
                            ns.Write((int)chr.Type[9]); //type d
                            break; //</case>
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
                            ns.WriteHex(subString, subString.Length); //modifiers b
                            break; //</case>
                    } //</switch>

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
                }
                ++aa;
            }
        }
    }
}
