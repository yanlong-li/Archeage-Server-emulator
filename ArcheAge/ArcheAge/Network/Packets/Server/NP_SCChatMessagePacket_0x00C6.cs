using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;
using LocalCommons.Utilities;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCChatMessagePacket_0x00C6 : NetPacket
    {
        public NP_SCChatMessagePacket_0x00C6(ClientConnection net, short chatId, string msg, string msg2) : base(01, 0x00C6)
        {
            //1.0.1406
            //SCChatMessagePacket
            //                chat_id -2                                                          W e l c o m e !
            // 2D00 DD01 C600 FEFF         0000 00000000 000000 00000000 00 00 00000000 0000 0800 57656C636F6D6521 0000000000000000
            /*
             * chat_id
             * Ответ -5
               Шепот -3
               система -2 ???
                       -1 ???
               Рядом 0
               Крик 1
               Торговля 2
               Поиск отряда 3
               Отряд 4
               Рейд 5
               Фракция 6
               Гильдия 7
               Семья 9
               Глава рейда 10
               Суд 11
               Отыгрыш роли 13
               Союз 14
             */

            ns.Write((short)chatId); //chat_id h
            ns.Write((short)0x00); //unk h
            ns.Write((int)0x00);   //chat_obj d
            ns.Write((Uint24)0x00);//gameObjectId d3
            ns.Write((int)0x00);   //objectId d
            ns.Write((byte)0x00);  //LanguageType c
            //ns.Write((byte)net.CurrentAccount.Character.CharRace);  //CharRace c
            ns.Write((byte)0x00);  //CharRace c
            ns.Write((int)0x00);   //type d
            ns.WriteUTF8Fixed(msg, msg.Length);   //name SS
            ns.WriteUTF8Fixed(msg2, msg2.Length); //name SS
            ns.Write((int)0x00);   //ability d
            ns.Write((int)0x00);   //option d
        }
    }
}