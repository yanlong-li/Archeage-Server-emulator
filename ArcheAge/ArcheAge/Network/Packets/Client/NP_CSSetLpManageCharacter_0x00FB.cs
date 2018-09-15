using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_CSSetLpManageCharacter_0x00FB : NetPacket
    {
        public NP_CSSetLpManageCharacter_0x00FB(ClientConnection net) : base(01, 0x00FB)
        {
            //1.0.1406
            //CSSetLpManageCharacter
            //0800 0001 FB00 FF091A00
            ns.Write((int)net.CurrentAccount.Character.CharacterId);   //characterId d
        }
    }
}