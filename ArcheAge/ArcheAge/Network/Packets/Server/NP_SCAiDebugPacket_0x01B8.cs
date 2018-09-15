using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCAiDebugPacket_0x01B8 : NetPacket
    {
        public NP_SCAiDebugPacket_0x01B8(ClientConnection net) : base(01, 0x01B8)
        {
            //1.0.1406
            //SCAiDebugPacket
            //1C00 DD01 B801 05000000 01000000 00000000 00000000 73CF5653 00000000

            ns.Write((int)0x05);   //code d
            ns.Write((int)0x01);   //code d
            ns.Write((int)0x00);   //code d
            ns.Write((int)0x00);   //code d
            ns.Write((int)0x5356CF73);   //code d
            ns.Write((int)0x00);   //code d
        }
    }
}