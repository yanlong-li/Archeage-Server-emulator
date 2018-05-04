using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets
{
    public enum LoginPacketOpcode
    {
        Unknown,
        CM_STS_CONNECT,
        CM_AUTH_LOGIN_START,
        CM_AUTH_KEY_DATA,
        CM_AUTH_LOGIN_FINISH,
        CM_AUTH_TOKEN,
        CM_AUTH_GAME_TOKEN,
        CM_ACCOUNT_LIST,
        CM_WORLD_LIST,
        CM_CHAR_LIST,
        CM_CHAR_CREATE,
        CM_CHAR_SLOT_REQUEST,
        CM_CHAR_DELETE,
        CM_PING,
        CM_SLOT_LIST,
    }
}
