using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCCooldownsPacket_0x0045 : NetPacket
    {
        public NP_SCCooldownsPacket_0x0045(ClientConnection net) : base(01, 0x0045)
        {
            //1.0.1406
            //SCCooldownsPacket
            //          op   skillCount tagCount
            //0C00 DD01 4500 00000000   00000000
            int skillCount = 0;
            ns.Write((int)skillCount); //skillCount d
            for (int i = 0; i < skillCount; i++)
            {
                ns.Write((uint)0);//type d
                ns.Write((uint)0);//type d
                ns.Write((uint)0);//type d
            }
            int tagCount = 0;
            ns.Write((int)tagCount); //tagCount d
            for (int i = 0; i < tagCount; i++)
            {
                ns.Write((uint)0);//type d
                ns.Write((uint)0);//type d
                ns.Write((uint)0);//type d
            }
        }
    }
}