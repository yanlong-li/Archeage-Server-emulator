using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network.Packets.Server
{
    public sealed class NP_SCCancelCharacterDeleteResponse_0x0037 : NetPacket
    {
        public NP_SCCancelCharacterDeleteResponse_0x0037(int characterId) : base(01, 0x0037)
        {
            ns.Write((int)characterId); //characterId d
            ns.Write((byte)0x01); //deleteStatus c
        }
    }
}
