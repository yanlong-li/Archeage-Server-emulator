using System;
using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;
using LocalCommons.Utilities;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCAccountInfoPacket_0x01B7 : NetPacket
    {
        public NP_SCAccountInfoPacket_0x01B7(ClientConnection net) : base(01, 0x01B7)
        {
            //1.0.1406
            //SCAccountInfoPacket
            ns.Write((int)0x01);   //payMethod d
            ns.Write((int)0x01);   //payLocation d 

            long payStart = DateTime.UtcNow.Ticks;
            ns.Write((long)payStart); //payStart Q

            long payEnd = DateTime.UtcNow.Ticks + 1000000;
            ns.Write((long)payEnd); //payEnd Q
        }
    }
}