using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets
{
    public enum GamePacketOpcodeCBT1
    {
        SM_ACTOR_APPEAR_LIST = 1,
        SM_ACTOR_APPEAR_INFO_LIST,
        SM_ACTOR_LIST = 0x3,
        SM_ACTOR_INFO_LIST = 0x4,
        SM_ACTOR_UPDATE_LIST = 0x6,
        CM_LOGIN_START = 0xE,
        SM_LOGIN_INIT,
        SM_MAP_CHANGE_MAP = 0x18,
        CM_MAP_LOAD_FINISHED,
        SM_MAP_LOAD_REPLY,
        SM_QUEST_CUTSCENE=0x1F,
        SM_SERVER_MESSAGE = 0x20,
        CM_CHAT = 0x22,
        SM_CHAT,
        CM_ACTOR_MOVEMENT = 0x2A,
        CM_ACTOR_TURN = 0x2C,
        SM_QUEST_INFO = 0x3A,
        CM_SKILL_LOAD = 0x4B,
        SM_SKILL_LOAD = 0x4C,
        CM_TARGET_SWITCH = 0x56,
        CM_MAP_PORTAL_ENTER=0x91,
        SM_MAP_PORTAL_RESULT,
        CM_QUEST_NPC_OPEN = 0xE2,
        SM_QUEST_NPC_OPEN,
        CM_SKILL_CAST = 0xEC,
        SM_SKILL_CAST_RESULT = 0xED,
        CM_MAPOBJECT_OPEN = 0x101,
        CM_MAPOBJECT_GET_ITEM,
        CM_UNKNOWN = 0x11A,
        SM_ITEM_INFO,
        CM_ITEM_EQUIP = 0x127,
        CM_QUEST_UPDATE_REQUEST = 0x14B,
        SM_QUEST_NEXT_QUEST = 0x151,
        SM_QUEST_UPDATE = 0x152,
        SM_QUEST_FINISH,
        SM_SKILL_ADD = 0x158,
        CM_LOGIN_AUTH = 0xFFFE,
    }
}
