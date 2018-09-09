using ArcheAge.ArcheAge.Holders;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCDeleteCharacterResponse_0x0034 : NetPacket
    {
        public NP_SCDeleteCharacterResponse_0x0034(int characterId) : base(01, 0x0001)
        {
            //Немедленное удаление данных!
            CharacterHolder.DeleteCharacterData(characterId); //удаляем запись с героем из базы
            ns.Write((int) characterId); //characterId d
            ns.Write((byte) 0x01); //deleteStatus c
            ns.Write((long) 0x00); //deleteRequestedTime q
            //ns.Write((long)Environment.TickCount); //deleteRequestedTime q
            ns.Write((long) 0x00); //deleteDelay q
            //ns.Write((long)Environment.TickCount+10000); //deleteDelay q
        }
    }
}