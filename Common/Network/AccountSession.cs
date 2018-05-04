using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Packets;
using SagaBNS.Common.Packets.AccountServer;

namespace SagaBNS.Common.Network
{
    public abstract partial class AccountSession<T> : DefaultClient<AccountPacketOpcode>
    {
        ConcurrentDictionary<long, T> packetSessions = new ConcurrentDictionary<long, T>();
        protected string AccountPassword { get; set; }
        SESSION_STATE state = SESSION_STATE.DISCONNECTED;

        /// <summary>
        /// 帐号服务器的连接状态
        /// </summary>
        public SESSION_STATE State { get { return state; } }

        public bool Disconnecting { get; set; }

        public AccountSession()
        {
            this.Encrypt = false;
            
            RegisterPacketHandler(AccountPacketOpcode.SM_LOGIN_RESULT, new Packets.AccountServer.SM_LOGIN_RESULT.SM_LOGIN_RESULT_INTERNAL<T>());
            RegisterPacketHandler(AccountPacketOpcode.SM_ACCOUNT_INFO, new Packets.AccountServer.SM_ACCOUNT_INFO.SM_ACCOUNT_INFO_INTERNAL<T>());
            RegisterPacketHandler(AccountPacketOpcode.SM_ACCOUNT_LOGIN_RESULT, new Packets.AccountServer.SM_ACCOUNT_LOGIN_RESULT.SM_ACCOUNT_LOGIN_RESULT_INTERNAL<T>());
            RegisterPacketHandler(AccountPacketOpcode.SM_ACCOUNT_LOGOUT_NOTIFY, new Packets.AccountServer.SM_ACCOUNT_LOGOUT_NOTIFY.SM_ACCOUNT_LOGOUT_NOTIFY_INTERNAL<T>());
        }

        public override void OnConnect()
        {
            this.state = SESSION_STATE.NOT_IDENTIFIED;

            CM_LOGIN_REQUEST p = new CM_LOGIN_REQUEST();
            p.Password = AccountPassword;
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
