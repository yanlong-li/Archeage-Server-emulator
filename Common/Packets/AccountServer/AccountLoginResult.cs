using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets.AccountServer
{
    public enum AccountLoginResult
    {
        OK,
        INVALID_PASSWORD,
        ALREADY_LOG_IN,
        NO_SUCH_ACCOUNT,
        DB_ERROR,
    }
}
