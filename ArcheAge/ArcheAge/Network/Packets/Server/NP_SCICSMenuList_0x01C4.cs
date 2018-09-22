using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network.Packets.Server
{
    class NP_SCICSMenuList_0x01C4 : NetPacket
    {
        public NP_SCICSMenuList_0x01C4(ClientConnection net, uint characterId, byte reason) : base(01, 0x01C4)
        {
            //1.0.1406
            //SCICSMenuList
            //0A00 DD01 C401 FF091A00 0100
            ns.Write((uint)characterId);//characterId d
            ns.Write((short)reason); //code h
        }
    }
}
