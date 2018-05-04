using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAgeProxy.ArcheAge.Network
{
    /// <summary>
    /// Sends Information About Registration Result.
    /// </summary>
    public sealed class NET_GameRegistrationResult : NetPacket
    {
        public NET_GameRegistrationResult(byte success) : base(0x01, true)
        {
            ns.Write(success);
        }
    }
    public sealed class NET_Result0x09 : NetPacket
    {
        public NET_Result0x09(byte success) : base(0x09, true)
        {
            //16000900
            ns.WriteHex("D61900000E00D09BD0B0D180D0B5D0B9D0BAD0B0");
        }
    }
}
