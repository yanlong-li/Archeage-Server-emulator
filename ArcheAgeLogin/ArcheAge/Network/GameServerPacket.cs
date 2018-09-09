using ArcheAgeLogin.ArcheAge.Structuring;
using LocalCommons.Network;

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
        /// <param name="clientVersion"></param>
        /// <param name="account"></param>
        public NET_AccountInfo(string clientVersion, Account account) : base(0x01, true)
        {
            switch (clientVersion)
            {
                case "1":
                    /*                 accountId Lv Ms       Name               Sesion   LastEnteredTime    LastIp
                     * Recv: 6C00 0100 10000000  00 00 0000 333632383338333030 00396166 373166343463656336 31 62 31 36 35 37 32 38 64 38 63 36 65 34 32 34 30 62 33 39 38 33 30 63 65 61 61 66 30 37 66 31 35 35 36 64 62 32 65 64 33 30 32 61 38 64 39 39 37 35 00 01 01 31 32 37 2E 30 2E 30 2E 31 00 21 E5 F6 81 65 01 00 00 00 4F 8E FC D4
                     */
                    ns.Write((int)account.AccountId);
                    ns.WriteDynamicASCII(account.Name);
                    //ns.WriteDynamicASCII(account.Password);
                    ns.WriteDynamicASCII(account.Token);
                    ns.Write((byte)account.AccessLevel);
                    ns.Write((byte)account.Membership);
                    ns.WriteDynamicASCII(account.LastIp);
                    ns.Write((long)account.LastEnteredTime);
                    ns.Write((byte)account.Characters);
                    ns.Write((int)account.Session);
                    break;
                case "3":

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
                    break;
                default:
                    break;
            }
        }
    }
}
