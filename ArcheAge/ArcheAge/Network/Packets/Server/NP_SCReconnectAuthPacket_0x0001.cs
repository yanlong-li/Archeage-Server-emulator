using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCReconnectAuthPacket_0x0001 : NetPacket
    {
        public NP_SCReconnectAuthPacket_0x0001(ClientConnection net) : base(01, 0x0001)
        {
            //ns.WriteHex("");
            //"0A00 0001 68  18  E501   A3B83568"
            int cookie = net.CurrentAccount.Session;
            ns.Write((int)cookie); //cookie
        }
    }
}