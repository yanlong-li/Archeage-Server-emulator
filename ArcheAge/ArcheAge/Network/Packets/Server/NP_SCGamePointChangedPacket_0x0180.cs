using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCGamePointChangedPacket_0x0180 : NetPacket
    {
        public NP_SCGamePointChangedPacket_0x0180(ClientConnection net) : base(01, 0x0180)
        {
            //1.0.1406
            //SCGamePointChangedPacket
            //2C00 DD01 8001 00000000100000000000000000000000000000000000000000000000000000000000000010000000
            //- <packet id="0x018001" name="SCGamePointChangedPacket">
            //ns.Write((byte)0x00);  //kind C
            //ns.Write((int)0x00); //amount d

            ns.WriteHex("00000000100000000000000000000000000000000000000000000000000000000000000010000000");
        }
    }
}