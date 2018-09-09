using System;
using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCReconnectAuthPacket__0x01E5 : NetPacket
    {
        public NP_SCReconnectAuthPacket__0x01E5(ClientConnection net) : base(05, 0x01E5)
        {
            /*
             21.07.2018 13:16 [INFO] - Encode: 0A00 DD05 B568 8311 E9AF024C
             21.07.2018 13:16 [INFO] - Decode: 0A00 DD05 2218 C301 091F831D
             */
            //3.0.0.7
            // size hash crc idx opcode data
            //"0A00 DD05 B5  68  8311   E9AF024C"
            //3.0.3.0
            //"0A00 DD05 68  18  E501   A3B83568"
            int cookie = net.CurrentAccount.Session;
            ns.Write((int)cookie); //cookie
        }
    }
}
