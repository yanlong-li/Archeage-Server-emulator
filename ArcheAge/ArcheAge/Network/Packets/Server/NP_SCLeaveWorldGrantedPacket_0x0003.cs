using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCLeaveWorldGrantedPacket_0x0003 : NetPacket
    {
        public NP_SCLeaveWorldGrantedPacket_0x0003(ClientConnection net) : base(01, 0x0003)
        {
            ns.Write((byte)0x00); //target c
        }
    }
}