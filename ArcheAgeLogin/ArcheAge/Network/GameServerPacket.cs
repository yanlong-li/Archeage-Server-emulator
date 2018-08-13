using LocalCommons.Network;
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
    //используетя
    public sealed class NET_GameRegistrationResult : NetPacket
    {
        //TCJoinResponse - ответ Логин сервера на пакет от Гейм сервера
        public NET_GameRegistrationResult(bool success) : base(0x00, true)
        {
            ns.Write(success);
        }
    }

    /// <summary>
    /// Sends Information About Logged In Account from LoginServer to GameServer.
    /// </summary>
    public sealed class NET_AccountInfo : NetPacket
    {
        /// <summary>
        /// Sends Information About Logged In Account from LoginServer to GameServer.
        /// </summary>
        /// <param name="account"></param>
        public NET_AccountInfo(Account account) : base(0x01, true)
        {
            /*                 accountId        Lv Ms Name           Sesion   LastEnteredTime  LastIp
             * Send: 2900 0100 1AC7000000000000 01 01 61617465737400 2810B47A 565074D264010000 3132372E302E302E3100
             */
            ns.Write((long)account.AccountId);
            ns.WriteDynamicASCII(account.Name);
            //ns.WriteDynamicASCII(account.Password);
            ns.WriteDynamicASCII(account.Token);
            ns.Write((byte)account.AccessLevel);
            ns.Write((byte)account.Membership);
            ns.WriteDynamicASCII(account.LastIp);
            ns.Write((long)account.LastEnteredTime);
            ns.Write((byte)account.Characters);
            ns.Write((int)account.Session);
        }
    }
}
