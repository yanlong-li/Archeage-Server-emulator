using LocalCommons.Network;
using System;

namespace ArcheAge.ArcheAge.Net
{
    public sealed class NP_Pong : NetPacket
    {
        ///<summary>
        ///Ответ на пакеты Ping клиента
        ///</summary>
        public NP_Pong(long tm, long when, int local) : base(2, 0x0013)
        {
            ns.Write((long)tm); //tm
            ns.Write((long)when); //when
            ns.Write((long)0x00); //elapsed
            ns.Write((long)(Environment.TickCount & int.MaxValue) * 1000); //remote
            ns.Write(local); //local
            ns.Write(Environment.TickCount & int.MaxValue); //world
        }
    }
}
