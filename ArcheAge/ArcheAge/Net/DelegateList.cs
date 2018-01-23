using ArcheAge.ArcheAge.Net.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAge.ArcheAge.Net
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
            //m_LHandlers = new PacketHandler<ClientConnection>[0x30];
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
            Register(0x01, 0x1b7f, new OnPacketReceive<ClientConnection>(OnPacketReceive_Client01));
            Register(0x02, 0x01, new OnPacketReceive<ClientConnection>(Onpacket0201));
            //Register(0x02, 0x01, new OnPacketReceive<ClientConnection>(Onpacket0201));
            Register(0x02, 0x12, new OnPacketReceive<ClientConnection>(Onpacket0212));
        }



        #region Client Callbacks Implementation

        //验证用户登录权限  不知如何使用，废弃中
        public static void OnPacketReceive_ClientAuthorized(ClientConnection net, PacketReader reader)
        {
            //B3 04 00 00 B3 04 00 00 8C 28 22 00 E7 F0 0C C6 FF FF FF FF 00 
            reader.Offset += 2;
            long protocol = reader.ReadLEInt64(); //Protocols?
            
            int accountId = reader.ReadLEInt32(); //Account Id
            reader.Offset += 4;
            int sessionId = reader.ReadLEInt32(); //User Session Id
            Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.Session == sessionId && kv.Value.AccountId == accountId).Value;
            if (m_Authorized == null)
            {
                net.Dispose();
                Logger.Trace("账户 {0} 未登录：无法继续。", net);
            }
            else
            {
                net.CurrentAccount = m_Authorized;
                net.SendAsync(new NP_ClientConnected2());
                net.SendAsync(new NP_Client02());
                //net.SendAsync(new NP_ClientConnected());
            }
        }

        
        /**
         * 
         * 连接游戏服务器第一包
         * */
        public static void OnPacketReceive_Client01(ClientConnection net,PacketReader reader)
        {
            net.SendAsyncHex(new NP_Hex("0700dd05f2bdb150102a00dd056f6fcc01d3a2724213e3b3e05321512c00dd0205d012452606e6b6865727f7c797704010e0b081512c00dd021300157f26060000000060bee1d96c0100000000058ef05d96663707d219375020f0b62d01007dd3e50ffe00dd058ef95d96663707d7a7775020f0c090613101d1a1724212e2b2835323f3c494643404d5a5754515e6b6865626f7c7976737fed0a0704011e1b1815122f2c292623303d3a3734414e4b4845525f5c596663606d6a7774717e7c0906030fed1a1714111e2b2825222f3c393633304d4a4744415e5b5855526f6c696663707d7a7774010e0b0815121f1c192623202d2a3734313e3b4845424f4c595653505d6a6764616e7b7875727fed0a0704011e1b1815122f2c292623303d3a3744414e4b4855525f5c596663606d6a7774717e7b0805020f0c191613101d2a2724212e3b3835323f4c494643405d5a5754516e6b680b08151860800dd0520b181510f00dd0552379ac797704010e0b08151860800dd0520a188501140"));


        }
        public static void Onpacket0201(ClientConnection net,PacketReader reader)
        {
            byte b3 = reader.ReadByte();
            if ( b3== 0x0)
            {


                net.SendAsync(new NP_Client0200());//同样返回报错
                net.SendAsyncHex(new NP_Hex("1400dd05fee767865627f6cf97087265899fe9242175"));
                net.SendAsyncHex(new NP_Hex("1e00dd020f000f00735f7069726174655f69736c616e640000000000000000014d00dd05e1606b03c3a31536778cd1e4324092a4fb031865b9ca6f4768d0bf8f29288d0aa62032df76266a421005dc04e238f2c494643405d5a5754516e6b7875626f6c797704010e0b0815152f322a900dd05e1936731ffe0a119356592c1b87d0d80a6e0105a6bba8a10367483c1ab3c4882a3f1145673bec8196e6492d9ee3f4690acf7111c72a0ca052a138bc0f02457ceeaba044766bec3172173c9d3f536418afec91e347486c3e0254b9dacbc16416abccd13257981c7ab25579cb3b905596bbbc2052472c8c0f5224398a0e2041e62a0c7162b66909cf3265197acf5175128b6d710217f92c5ab314b98b0b911236489dfef51e717472a00dd050e65cc01d2a2724212e3b3835323f4c4946434053561754516e6b6865727f7c797704010e0b081517700dd057997103300eea3724212e2b4845524f4c595643505d7a6764616e7b7875767bed0a0704011e1b1815122f2c292623303d3a3744414e4b4855525f5c596663606d6a7774717e7b0805020f0c191613101d2a2724212e3b3835323f4c49465340ac6a5754566a4b3865727f7c781348ddcac8f8b99f20900dd05a9b6e5511041701c00dd05d0635d04d5af754516e6b9895827f7c897704010e0b0815ec4f40e00dd057a2fb0c797704010e0b081510900dd059db9ac531142700e00dd05282df0c797704010e0b081512300dd0523e85c835223f4c494643405d5a5754516e6b6865727f7c797704010e0b08151420a00dd058b7cf511e0b08151"));

            }else if (b3 == 0x01)
            {
                net.SendAsync(new NP_Client02002());
            }
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
