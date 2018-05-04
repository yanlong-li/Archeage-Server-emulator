using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Packets;
using SagaBNS.Common.Packets.CharacterServer;

namespace SagaBNS.Common.Network
{
    public abstract partial class CharacterSession<T>: DefaultClient<CharacterPacketOpcode>
    {
        ConcurrentDictionary<long, T> packetSessions = new ConcurrentDictionary<long, T>();

        SESSION_STATE state = SESSION_STATE.DISCONNECTED;

        /// <summary>
        /// 帐号服务器的连接状态
        /// </summary>
        public SESSION_STATE State { get { return state; } }

        public bool Disconnecting { get; set; }

        protected string CharacterPassword { get; set; }

        public CharacterSession()
        {
            this.Encrypt = false;

            RegisterPacketHandler(CharacterPacketOpcode.SM_LOGIN_RESULT, new Packets.CharacterServer.SM_LOGIN_RESULT.SM_LOGIN_RESULT_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_CHAR_LIST, new Packets.CharacterServer.SM_CHAR_LIST.SM_CHAR_LIST_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_ACTOR_INFO, new Packets.CharacterServer.SM_ACTOR_INFO.SM_ACTOR_INFO_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_CHAR_CREATE_RESULT, new Packets.CharacterServer.SM_CHAR_CREATE_RESULT.SM_CHAR_CREATE_RESULT_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_CHAR_DELETE_RESULT, new Packets.CharacterServer.SM_CHAR_DELETE_RESULT.SM_CHAR_DELETE_RESULT_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_ITEM_CREATE_RESULT, new Packets.CharacterServer.SM_ITEM_CREATE_RESULT.SM_ITEM_CREATE_RESULT_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_ITEM_INVENTORY_ITEM, new Packets.CharacterServer.SM_ITEM_INVENTORY_ITEM.SM_ITEM_INVENTORY_ITEM_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_SKILL_INFO, new Packets.CharacterServer.SM_SKILL_INFO.SM_SKILL_INFO_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_QUEST_INFO, new Packets.CharacterServer.SM_QUEST_INFO.SM_QUEST_INFO_INTERNAL<T>());
            RegisterPacketHandler(CharacterPacketOpcode.SM_TELEPORT_INFO, new Packets.CharacterServer.SM_TELEPORT_INFO.SM_TELEPORT_INFO_INTERNAL<T>());
        }

        public override void OnConnect()
        {
            this.state = SESSION_STATE.NOT_IDENTIFIED;

            CM_LOGIN_REQUEST p = new CM_LOGIN_REQUEST();
            p.Password = CharacterPassword;
            this.Network.SendPacket(p);
        }

        public override void OnDisconnect()
        {
            //因为OnConncect的Exception会被Catch，因此开新线程强制中断服务器
            if (state == SESSION_STATE.IDENTIFIED && !Disconnecting)
            {
                Thread th = new Thread(() =>
                {
                    throw new Exception("Fatal: Account server disconnected, shuting down...");
                });
                th.Start();
            }
            else
                state = SESSION_STATE.REJECTED;
        }
    }
}
