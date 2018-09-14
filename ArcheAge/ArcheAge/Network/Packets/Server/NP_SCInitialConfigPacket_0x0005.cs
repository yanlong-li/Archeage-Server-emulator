using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network.Packets.Server
{
    class NP_SCInitialConfigPacket_0x0005 : NetPacket
    {
        public NP_SCInitialConfigPacket_0x0005(ClientConnection net) : base(01, 0x0005)
        {
            //1.0.1406
            //SCInitialConfigPacket
            //2B00 DD01 0500 0A00 61612E6D61696C2E7275 0700 3E320F0F790033 00000000 00 0A00 32000000000001010101 01
            const string host = "aaemu.pw";
            ns.WriteUTF8Fixed(host, host.Length); //host_len h, host b
            string fset = "3E320F0F790033";
            ns.WriteHex(fset, fset.Length);//fset_len h, fset b
            int count = 0;
            ns.Write((int)count);//count d
            for (int i = 0; i < count; i++)
            {
                ns.Write((int) 0x00); //code d
            }
            ns.Write((byte)0x00); //searchLevel c 
            string msg = "32000000000001010101";
            ns.WriteHex(msg, msg.Length); //bidLevel s
            ns.Write((byte)0x01); //postLevel c
        }
    }
}
