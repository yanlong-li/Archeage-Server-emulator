using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheAgeLogin.ArcheAge.Structuring;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Sends Information About Registration Result.
    /// </summary>
    public sealed class NET_GameRegistrationResult : NetPacket
    {
        //TCJoinResponse - ответ Логин сервера на пает от Гейм сервера
        public NET_GameRegistrationResult(bool success) : base(0x00, true)
        {
            ns.Write(success);
        }
    }

    /// <summary>
    /// Sends Information About Logged In Account.
    /// </summary>
    public sealed class NET_AccountInfo : NetPacket
    {
        public NET_AccountInfo(Account account) : base(0x01, true)
        {
            ns.Write((int)account.AccId); //вместо AccountID
            ns.Write((byte)account.AccessLevel);
            ns.Write((byte)account.Membership);
            ns.WriteDynamicASCII(account.Name);
            //ns.WriteDynamicASCII(account.Password);
            ns.Write((int)account.Session);
            ns.Write((long)account.LastEnteredTime);
            ns.WriteDynamicASCII(account.LastIp);
        }
    }
}
