using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network.Packets.Server
{
    public sealed class NP_SCCharacterCreationFailed_0x0038 : NetPacket
    {
        public NP_SCCharacterCreationFailed_0x0038(ClientConnection net) : base(01, 0x0038)
        {
            byte result = 0; // 0, 1, 2 - имя принадлежит персонажу, ожидающему удаления
            ns.Write((byte)result);
        }
    }
}
