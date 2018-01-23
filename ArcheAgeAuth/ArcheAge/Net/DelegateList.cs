using ArcheAgeAuth.ArcheAge.Net.Connections;
using ArcheAgeAuth.ArcheAge.Structuring;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAgeAuth.ArcheAge.Net
{
    /// <summary>
    /// Delegate List That Contains Information About Received Packets.
    /// 包含接收包信息的委托列表。
    /// </summary>
    public class DelegateList
    {
        private static int m_Maintained;
        private static PacketHandler<LoginConnection>[] m_LHandlers;
        private static PacketHandler<ClientConnection>[] m_CHandlers;
        private static Dictionary<int, PacketHandler<ClientConnection>[]> levels;
        private static LoginConnection m_CurrentLoginServer;

        public static LoginConnection CurrentLoginServer
        {
            get { return m_CurrentLoginServer; }
        }

        public static Dictionary<int, PacketHandler<ClientConnection>[]> ClientHandlers
        {
            get { return levels; }
        }

        public static PacketHandler<LoginConnection>[] LHandlers
        {
            get { return m_LHandlers; }
        }

        public static void Initialize()
        {
            m_LHandlers = new PacketHandler<LoginConnection>[0x20];
            m_LHandlers = new PacketHandler<A>[0x30];
            levels = new Dictionary<int, PacketHandler<ClientConnection>[]>();

            RegisterDelegates();
        }
        //注册服务
        private static void RegisterDelegates()
        {
            //-------------- Login - Game Communication Packets ------------
            Register(0x00, new OnPacketReceive<LoginConnection>(Handle_GameRegisterResult)); //Taken Fully
            Register(0x01, new OnPacketReceive<LoginConnection>(Handle_AccountInfoReceived)); //Taken Fully

            //-------------- Client Communication Packets ------------------
            //客户端通讯处
            //-------------- Using - Packet Level - Packet Opcode(short) - Receive Delegate -----


            Register(0x01, 0x00, new OnPacketReceive<ClientConnection>(OnPacketReceive_ClientAuthorized));
           //  Register(0x01, 0x77, new OnPacketReceive<ClientConnection>(OnPacketReceive_Client01));
            //Register(0x02, 0x01, new OnPacketReceive<ClientConnection>(Onpacket0201));
            //Register(0x02, 0x01, new OnPacketReceive<ClientConnection>(Onpacket0201));
            //Register(0x02, 0x12, new OnPacketReceive<ClientConnection>(Onpacket0212));
        }



        #region Client Callbacks Implementation

        //验证用户登录权限  不知如何使用，废弃中
        public static void OnPacketReceive_ClientAuthorized(ClientConnection net, PacketReader reader)
        {
              
                net.SendAsync(new NP_ClientConnected());
    
        }

        
        /**
         * 
         * 连接游戏服务器第一包
         * */
        public static void OnPacketReceive_Client01(ClientConnection net,PacketReader reader)
        {
            //net.CurrentAccount = m_Authorized;
            //模拟回馈数据验证
            net.SendAsync(new NP_Client01());
            //紧跟返回数据
            net.SendAsync(new NP_Client02());
            net.SendAsync(new NP_Clientdd05002());
            net.SendAsync(new NP_Clientdd02001());
            net.SendAsync(new NP_Clientdd05003());
            net.SendAsync(new NP_Clientdd05004());
            net.SendAsync(new NP_Clientdd05005());
            net.SendAsync(new NP_Clientdd05006());
            net.SendAsync(new NP_Clientdd05007());
            net.SendAsync(new NP_Clientdd05008());
            net.SendAsync(new NP_Clientdd05009());
            net.SendAsync(new NP_Clientdd05010());
            net.SendAsync(new NP_Clientdd05011());
            net.SendAsync(new NP_Clientdd05012());
            net.SendAsync(new NP_Clientdd05013());
            net.SendAsync(new NP_Clientdd05014());
            net.SendAsync(new NP_Clientdd05015());
            net.SendAsync(new NP_Clientdd05016());
            net.SendAsync(new NP_Clientdd05017());
            net.SendAsync(new NP_Clientdd05018());
            net.SendAsync(new NP_Clientdd05019());
            net.SendAsync(new NP_Client02002());


        }
        public static void Onpacket0201(ClientConnection net,PacketReader reader)
        {
            //net.SendAsync(new NP_Client0200());//同样返回报错
            //net.SendAsync(new NP_Clientdd0537e7());
            //net.SendAsync(new NP_Clientdd05bae9());
        }

        public static void Onpacket0212(ClientConnection net, PacketReader reader)
        {
            //reader.Offset += 8; //00 00 00 00 00 00 00 00  Undefined Data
            int number1 = reader.ReadLEInt32();
            int number2 = reader.ReadLEInt32();
            int number3 = reader.ReadLEInt32();
            int number4 = reader.ReadLEInt32();
            int number5 = reader.ReadLEInt32();
            net.SendAsync(new NP_Client0212(number1,number2,number3,number4,number5));
        }

        #endregion

        #region Callbacks Implementation

        private static void Handle_AccountInfoReceived(LoginConnection net, PacketReader reader)
        {
            //Set Account Info
            Account account = new Account();
            account.AccountId = reader.ReadInt32();
            account.AccessLevel = reader.ReadByte();
            account.Membership = reader.ReadByte();
            account.Name = reader.ReadDynamicString();
            //account.Password = reader.ReadDynamicString();
            account.Session = reader.ReadInt32();
            account.LastEnteredTime = reader.ReadInt64();
            account.LastIp = reader.ReadDynamicString();

            Console.WriteLine("准备登陆的账号:<"+account.AccountId+">;");
            //检查账户是否在线，若在线则强制断开
            Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.AccountId == account.AccountId).Value;
            if (m_Authorized!=null)
            {
                //Already
                Account acc = ClientConnection.CurrentAccounts[m_Authorized.Session];
                if (acc.Connection != null)
                {
                    acc.Connection.Dispose(); //Disconenct  
                    Logger.Trace("账户《 " + acc.Name + "》 二次登陆，旧连接被强行断开");
                }
                else
                {
                    ClientConnection.CurrentAccounts.Remove(account.Session);
                }
            }
            else
            {
                Logger.Trace("账户《 {0}》: 授权", account.Name);
                ClientConnection.CurrentAccounts.Add(account.Session, account);
            }
        }

        private static void Handle_GameRegisterResult(LoginConnection con, PacketReader reader)
        {
            bool result = reader.ReadBoolean();
            if (result)
                Logger.Trace("成功安装登陆服务器");
            else
                Logger.Trace("有些问题是在安装登录服务器出现");
            if(result)
               m_CurrentLoginServer = con;
        }

        #endregion

        private static void Register(short opcode, OnPacketReceive<LoginConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<LoginConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(byte level, short opcode, OnPacketReceive<ClientConnection> e)
        {
            if (!levels.ContainsKey(level))
            {
                PacketHandler<ClientConnection>[] handlers = new PacketHandler<ClientConnection>[0xFFFF];
                handlers[opcode] = new PacketHandler<ClientConnection>(opcode, e);
                levels.Add(level, handlers);
            }
            else
            {
                levels[level][opcode] = new PacketHandler<ClientConnection>(opcode, e);
            }
        }

    }
}
