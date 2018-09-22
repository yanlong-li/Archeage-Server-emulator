using System;
using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_DetailedTimeOfDayPacket_0x00EA : NetPacket
    {
        public NP_DetailedTimeOfDayPacket_0x00EA(ClientConnection net) : base(01, 0x00EA)
        {
            //1.0.1406
            //DetailedTimeOfDayPacket
            //1400 DD01 EA00 4871B241 D171DA3A 00000000 0000C041
            ns.Write((float)DateTime.Now.Hour); //DateTime.UtcNow.Hour); //time f (время суток на сервере)(Environment.TickCount & Int32.MaxValue)
            ns.Write((float) 0.001667); //speed f 0.001666600001044571399688720703125
            ns.Write((float) 0.0); //start f
            ns.Write((float) 24.0); //end f
        }
    }
}