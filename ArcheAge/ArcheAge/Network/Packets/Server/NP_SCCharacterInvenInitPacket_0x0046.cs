using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCCharacterInvenInitPacket_0x0046 : NetPacket
    {
        public NP_SCCharacterInvenInitPacket_0x0046(ClientConnection net) : base(01, 0x0046)
        {
            //1.0.1406
            //SCCharacterInvenInitPacket
            //0C00 DD01 4600 32000000 32000000
            ns.Write((int)0x32);   //numInvenSlots d
            ns.Write((int)0x32);   //numBankSlots d
        }
    }
}