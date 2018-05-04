using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets
{
    public enum CharacterPacketOpcode
    {
        CM_LOGIN_REQUEST,
        SM_LOGIN_RESULT,
        CM_CHAR_LIST_REQUEST,
        SM_CHAR_LIST,
        CM_CHAR_CREATE,
        SM_CHAR_CREATE_RESULT,
        CM_CHAR_SAVE,
        CM_CHAR_DELETE,
        SM_CHAR_DELETE_RESULT,
        CM_ITEM_CREATE,
        SM_ITEM_CREATE_RESULT,
        CM_ITEM_SAVE,
        CM_ITEM_LIST_SAVE,
        CM_ITEM_DELETE,
        CM_ITEM_INVENTORY_GET,
        SM_ITEM_INVENTORY_ITEM,
        CM_QUEST_GET,
        SM_QUEST_INFO,
        CM_QUEST_SAVE,
        CM_SKILL_GET,
        SM_SKILL_INFO,
        CM_SKILL_SAVE,
        CM_TELEPORT_GET,
        SM_TELEPORT_INFO,
        CM_TELEPORT_SAVE,
        CM_ACTOR_INFO_REQUEST,
        SM_ACTOR_INFO,

    }
}
