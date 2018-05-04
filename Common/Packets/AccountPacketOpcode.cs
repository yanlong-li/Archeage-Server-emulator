using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets
{
    public enum AccountPacketOpcode
    {
        CM_LOGIN_REQUEST,
        SM_LOGIN_RESULT,
        CM_ACCOUNT_INFO_REQUEST,
        CM_ACCOUNT_INFO_REQUEST_ID,
        SM_ACCOUNT_INFO,
        CM_ACCOUNT_LOGIN,
        SM_ACCOUNT_LOGIN_RESULT,
        CM_ACCOUNT_LOGOUT,
        CM_ACCOUNT_SAVE,
        SM_ACCOUNT_LOGOUT_NOTIFY,
    }
}
