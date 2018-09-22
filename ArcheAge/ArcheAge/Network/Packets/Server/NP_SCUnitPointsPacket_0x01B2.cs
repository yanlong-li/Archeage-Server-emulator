using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCUnitPointsPacket_0x01B2 : NetPacket
    {
        public NP_SCUnitPointsPacket_0x01B2(ClientConnection net) : base(01, 0x01B2)
        {
            //1.0.1406
            //SCUnitPointsPacket
            //          op   unit   hp       mp
            //0F00 DD01 B200 347F00 90650000 48710000
            ns.Write((int)0x00);  //unit d
            ns.Write((int)0x00);  //preciseHealth d
            ns.Write((byte)0x00); //preciseMana d
        }
    }
}