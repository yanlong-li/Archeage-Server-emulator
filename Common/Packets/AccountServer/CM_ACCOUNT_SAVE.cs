using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
namespace SagaBNS.Common.Packets.AccountServer
{
    public class CM_ACCOUNT_SAVE : Packet<AccountPacketOpcode>
    {
        public CM_ACCOUNT_SAVE()
        {
            this.ID = AccountPacketOpcode.CM_ACCOUNT_SAVE;
        }

        public long SessionID
        {
            get
            {
                return GetLong(2);
            }
            set
            {
                PutLong(value, 2);
            }
        }

        public Account.Account Account
        {
            get
            {
                Common.Account.Account account = new Account.Account();
                account.AccountID = GetUInt(10);
                account.UserName = GetString();
                account.Password = GetString();
                account.GMLevel = GetByte();
                account.ExtraSlots = GetByte();
                account.LastLoginIP = GetString();
                account.LastLoginTime = DateTime.FromBinary(GetLong());
                account.LoginToken = new Guid(GetBytes(16));
                account.TokenExpireTime = DateTime.FromBinary(GetLong());
                return account;
            }
            set
            {
                PutUInt(value.AccountID,10);
                PutString(value.UserName);
                PutString(value.Password);
                PutByte(value.GMLevel);
                PutByte(value.ExtraSlots);
                PutString(value.LastLoginIP);
                PutLong(value.LastLoginTime.ToBinary());
                PutBytes(value.LoginToken.ToByteArray());
                PutLong(value.TokenExpireTime.ToBinary());
            }
        }
    }
}
