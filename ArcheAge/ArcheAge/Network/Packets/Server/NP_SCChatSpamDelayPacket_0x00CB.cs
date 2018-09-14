using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCChatSpamDelayPacket_0x00CB : NetPacket
    {
        public NP_SCChatSpamDelayPacket_0x00CB(ClientConnection net) : base(01, 0x00CB)
        {
            //1.0.1406
            //SCChatSpamDelayPacket
            //1400 DD01 CB00 00000000 00000000 00000000 00000000
            ns.Write((float)0x00); //yellDelay f
            ns.Write((int)0x00);   //maxSpamYell d
            ns.Write((float)0x00); //spamYellDelay f
            ns.Write((int)0x00);   //maxChatLen d
        }
    }
}