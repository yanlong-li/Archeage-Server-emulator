using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCRaceCongestionPacket_0x003A : NetPacket
    {
        public NP_SCRaceCongestionPacket_0x003A(ClientConnection net) : base(01, 0x003A)
        {
            //1.0.1406
            //SCResultRestrictCheck- <packet id="0x003A01" name="SCRaceCongestionPacket">
            //0D00 DD01 3A00 00 00 00 00 00 00 00 00 00

            ns.Write((byte)0x00);  //con[0] c
            ns.Write((byte)0x00);  //con[1] c
            ns.Write((byte)0x00);  //con[2] c
            ns.Write((byte)0x00);  //con[3] c
            ns.Write((byte)0x00);  //con[4] c
            ns.Write((byte)0x00);  //con[5] c
            ns.Write((byte)0x00);  //con[6] c
            ns.Write((byte)0x00);  //con[7] c
            ns.Write((byte)0x00);  //con[8] c
        }
    }
}