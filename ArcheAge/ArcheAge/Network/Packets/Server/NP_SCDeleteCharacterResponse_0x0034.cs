using LocalCommons.Network;
using System;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCDeleteCharacterResponse_0x0034 : NetPacket
    {
        public NP_SCDeleteCharacterResponse_0x0034(uint characterId, int deleteStatus) : base(01, 0x0034)
        {
            ns.Write((uint)characterId); //characterId d
            ns.Write((byte)deleteStatus); //deleteStatus c  // 1 - сразу удалет  2 - запускает таймер

            if (deleteStatus == 1)
            {
                ns.Write((long)0x00); //deleteRequestedTime q
                ns.Write((long)0x00); //deleteDelay q
            }
            else
            {
                ns.Write((long)Environment.TickCount); //deleteRequestedTime q startTime
                ns.Write((long)Environment.TickCount + 1000); //deleteDelay q endTime
            }
        }
    }
}