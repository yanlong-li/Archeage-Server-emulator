using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Account
{
    public class Account
    {
        public uint AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte GMLevel { get; set; }
        public byte ExtraSlots { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime LastLoginTime { get; set; }

        public Guid LoginToken { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }
}
