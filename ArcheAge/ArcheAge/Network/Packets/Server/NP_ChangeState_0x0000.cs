using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_ChangeState_0x0000 : NetPacket
    {
        public NP_ChangeState_0x0000(int state) : base(02, 0x00)
        {
            //            ID     state
            //08 00 DD 02 00 00 {00 00 00 00} //SCChangeState
            //08 00 00 02 01 00 {00 00 00 00} //CSFinishState
            state += 1;
            ns.Write(state);
        }
    }
}
