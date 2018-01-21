using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Packet List That Contains All Game / Client Packet Delegates.
    /// </summary>
    public static class PacketList
    {
        private static int m_Maintained;
        private static PacketHandler<GameConnection>[] m_GHandlers;
        private static PacketHandler<ArcheAgeConnection>[] m_LHandlers;

        public static PacketHandler<GameConnection>[] GHandlers
        {
            get { return m_GHandlers; }
        }

        public static PacketHandler<ArcheAgeConnection>[] LHandlers
        {
            get { return m_LHandlers; }
        }

        public static void Initialize()
        {
            m_GHandlers = new PacketHandler<GameConnection>[0x20];
            m_LHandlers = new PacketHandler<ArcheAgeConnection>[0x30];

            Registration();
        }

        private static void Registration()
        {
            //Game Server Packets
            Register(0x00, new OnPacketReceive<GameConnection>(Handle_RegisterGameServer));//等级注册服务器
            //Register(0x00, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected));//等级注册服务器
            Register(0x02, new OnPacketReceive<GameConnection>(Handle_UpdateCharacters));//等级更新服务器

            //Client Packets
            Register(0x01, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //账号登陆服务
            Register(0x03, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue2)); //token验证服务 wegame 中国
            Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue)); //token验证服务 -r模式  美服
            Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_Token_Continue)); //token验证服务 -r模式    中服
            //Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //登机登陆服务
            //Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_05));//登陆验证
            Register(0x0c, new OnPacketReceive<ArcheAgeConnection>(Handle_RequestServerList)); //返回服务器列表
            //Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_0d));//根据服务器id返回服务器地址//0b 00 0d 00 00 00 00 00 00 00 00 36（36可能时服务器id）
            Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected));//根据服务器id返回服务器地址

            //Register(0x08, new OnPacketReceive<ArcheAgeConnection>(Handle_RequestServerList)); //返回服务器列表
            //Register(0x09, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected)); //服务器查询
        }


        #region Game Server Delegates

        private static void Handle_UpdateCharacters(GameConnection net, PacketReader reader)
        {
            int accountId = reader.ReadInt32();
            int characters = reader.ReadInt32();

            Account currentAc = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            currentAc.Characters = characters;
        }

        private static void Handle_RegisterGameServer(GameConnection net, PacketReader reader)
        {
            byte id = reader.ReadByte();
            short port = reader.ReadInt16();

            string ip = reader.ReadDynamicString();
            string password = reader.ReadDynamicString();

            bool success = GameServerController.RegisterGameServer(id, password, net, port, ip);
            net.SendAsync(new NET_GameRegistrationResult(success));
            //net.SendAsync(new NP_ServerList());
        }

        #endregion

        #region Client Delegates
        private static void Handle_SignIn(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset += 12; //Static Data - 0A 00 00 00 07 00 00 00 00 00 
            int m_RLoginLength = reader.ReadLEInt16();
            reader.Offset += 2;
            string m_RLogin = reader.ReadString(m_RLoginLength); //Reading Login

            

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_RLogin);
            if (n_Current == null)
            {
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Account m_New = new Account();
                    m_New.AccountId = AccountHolder.AccountList.Count + 1;
                    m_New.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    m_New.AccessLevel = 0;
                    m_New.LastIp = net.ToString();
                    m_New.Membership = 0;
                    m_New.Name = m_RLogin;
                    net.CurrentAccount = m_New;
                    AccountHolder.AccountList.Add(m_New);
                }
                else
                    net.CurrentAccount = null;
            }
            else
            {
                net.CurrentAccount = n_Current;
            }
            // net.SendAsync(new NP_PasswordCorrect(1));
            net.SendAsync(new NP_ServerList());

        }

        private static void Handle_SignIn_Continue(ArcheAgeConnection net, PacketReader reader)
        {
            //HOW TO DECRYPT IT ????
            //string password = "";
            //如果账户未空，登陆失败
            if (net.CurrentAccount == null)
            {
                //返回登陆失败信息
                net.SendAsync(new NP_FailLogin());
                return;
            }

            /* TODO
            if (net.CurrentAccount.Password == null)
            {
                //Means - New Account.
                net.CurrentAccount.Password = password;
            }
            else
            {
                //Checking Password
                if (net.CurrentAccount.Password != password)
                {
                    net.SendAsync(new NP_FailLogin());
                    return;
                }
            }
            */
            net.SendAsync(new NP_AcceptLogin());
            net.CurrentAccount.Session = net.GetHashCode();
            net.SendAsync(new NP_PasswordCorrect(net.CurrentAccount.Session));
            Logger.Trace("账户登陆: " + net.CurrentAccount.Name);
            GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
        }

        /**
         *token 验证模式
         *uid+token
         * 
         */
        private static void Handle_Token_Continue(ArcheAgeConnection net,PacketReader reader)
        {
            reader.Offset = 21;
            int m_RUidLength = reader.ReadLEInt16();
            string m_uid = reader.ReadString(m_RUidLength); //Reading Login
            int m_RtokenLength = reader.ReadLEInt16();
            string m_RToken = reader.ReadHexString(m_RtokenLength);
            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId ==Convert.ToInt32(m_uid));
            if (n_Current !=null )
            {
                Logger.Trace("账号: < " + n_Current.AccountId + ":" + n_Current.Name + ">正在登陆");
                //账号存在
                if (n_Current.Token.ToLower() == m_RToken.ToLower())
                {
                    net.CurrentAccount = n_Current;
                    //将账号信息写入在线账户列表
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("账号: < "+n_Current.AccountId+":" + n_Current.Name+">登陆成功");
                    net.SendAsync(new NP_AcceptLogin());
                    net.SendAsync(new NP_03key());
                    //返回服务器列表
                    //net.SendAsync(new NP_ServerList());
                    return;
                }
                Logger.Trace("账号: <"+n_Current.AccountId+":" + n_Current.Name+">TOKEN验证失败："+m_RToken.ToLower());

            }
            else
            {
                Logger.Trace("客户端尝试登陆不存在的账户"+ m_uid);
            }

                //如果前面没有终止，那么账号登陆失败
                net.SendAsync(new NP_FailLogin());



        }

          
        private static void Handle_Token_Continue2(ArcheAgeConnection net, PacketReader reader)
        {

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId ==1);
            if (n_Current != null)
            {
                Logger.Trace("账号试图登陆: " + n_Current.Name);
                //账号存在
               // if (n_Current.Password.ToLower() == m_RToken.ToLower())
               // {
                    net.CurrentAccount = n_Current;
                    //将账号信息写入在线账户列表
                    GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
                    Logger.Trace("账号登陆成功: " + net.CurrentAccount.Name);
                    net.SendAsync(new NP_AcceptLogin());
                    net.SendAsync(new NP_03key());
                    //返回服务器列表
                    //net.SendAsync(new NP_ServerList());
                    return;
              //  }
               // Logger.Trace("账号: " + net.CurrentAccount.Name + "/密码不正确：" + m_RToken.ToLower());

            }

            //如果前面没有终止，那么账号登陆失败
            net.SendAsync(new NP_FailLogin());



        }

        //发送服务器列表（基于抓包）
        private static void Handle_05(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsyncHex(new NP_PasswordCorrect(1));
        }

        //返回服务器连接成数据包
        private static void Handle_0d(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsync0d(new NP_PasswordCorrect(1));
            //net.SendAsync(new NP_ServerList());
        }

        private static void Handle_RequestServerList(ArcheAgeConnection net, PacketReader reader)
        {
            byte[] unknown = reader.ReadByteArray(8); //unk?
            net.SendAsync(new NP_ServerList());
        }

        /**
         * 
         * 客户端选择服务器发送
         * 服务器IP
         * 服务器端口号
         * sessionID
         * 
         * */
        private static void Handle_ServerSelected(ArcheAgeConnection net, PacketReader reader)
        {
            //net.SendAsync(new NP_EditMessage2("systemTest"));
            //return;
            reader.Offset += 8; //00 00 00 00 00 00 00 00  Undefined Data
            byte serverId = reader.ReadByte();
            //serverId =1;
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server != null && server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    //create session
                    Random random = new Random();
                    int num = random.Next(255) + random.Next(255) + random.Next(255) + random.Next(255);
                    net.CurrentAccount.Session = num;

                    net.movedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(net.CurrentAccount));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                    net.SendAsync(new NP_SendGameAuthorization(server, num));   
                }
            }
            else
            {
                Logger.Trace("请求了不存在的服务器ID："+serverId);
                net.Dispose();
            }
        }

        #endregion

        private static void Register(short opcode, OnPacketReceive<ArcheAgeConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<ArcheAgeConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(short opcode, OnPacketReceive<GameConnection> e)
        {
            m_GHandlers[opcode] = new PacketHandler<GameConnection>(opcode, e);
            m_Maintained++;
        }
    }
}
