using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets
{
    public enum LobbyPacketOpcode
    {
        CM_AUTH = 0x3,
        SM_AUTH_RESULT,
        CM_KEEP_ALIVE = 0x9,
        CM_REQUEST_LOGIN = 0xD,
        SM_REQUEST_LOGIN,
        CM_SERVER_LIST = 0x18,
        SM_SERVER_LIST,
        CM_CHARACTER_LIST = 0x1B,
        SM_CHARACTER_LIST,
        CM_CHAR_CREATE = 0x21,
        SM_CHAR_CREATE,
        SM_CHAR_CREATE_FAILED,
        CM_CHAR_DELETE,
        SM_CHAR_DELETE,
        SM_CHAR_DELETE_FINISH = 0x27,
        CM_CHAR_DELETE_CANCEL,
        SM_CHAR_DELETE_CANCEL,
    }
}
