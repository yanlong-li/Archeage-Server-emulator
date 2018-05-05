using LocalCommons.Native.Network;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAgeAuth.ArcheAge.Network
{
    /// <summary>
    /// Sends Information About That Login Was right and we can continue =)
    /// </summary>
    public sealed class NP_AcceptLogin : NetPacket
    {
        public NP_AcceptLogin(string clientVersion) : base(0x00, true)
        {

           
            if (clientVersion == "1")
            {
                ns.Write((short)0x14);
                ns.Write((byte)0x00);
                ns.Write((byte)0xff);
                ns.Write((byte)0xff);
                ns.Write((short)0x1e);
                ns.Write((int)0x00);
            }
            else
            {
                ns.Write((short)0x00);
                ns.Write((short)0x306);
                ns.Write((short)0x48);
                ns.Write((int)0x0);
            }
            
        }
    }
    public sealed class NP_AcceptAuth : NetPacket
    {
        public NP_AcceptAuth() : base(0x01, true)
        {
            ns.Write((byte)0x0);
        }
    }
    /// <summary>
    /// Sends Request To Specified Game Server by Entered Information
    /// </summary>
    public sealed class NP_SendGameAuthorization : NetPacket
    {
        public NP_SendGameAuthorization(GameServer server, int sessionId) : base(0x0A, true)
        {
            string[] ipArray = server.IPAddress.Split('.');

            if (ipArray.Length == 4)
            {
                //写入sessionid
                //ns.Write(sessionId);
                ns.Write((byte)0x5b);//未知
                ns.Write((byte)0x4f);//未知
                ns.Write((byte)0xdd);//未知
                ns.Write((byte)0x4e);//未知

                for (int i = 3; i > -1; i--)
                {
                    var cd = Convert.ToInt32(ipArray[i].ToString());
                    ns.Write((byte)Convert.ToInt32(ipArray[i].ToString()));
                }
            }
            else
            {
                //sessionId
                ns.Write(sessionId);
                //主地址
                ns.Write((byte)0x01);
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);
                ns.Write((byte)0x7f);
            }
            //ns.Write((int)m_AccountId);
            //ns.WriteASCIIFixedNoSize(server.IPAddress, server.IPAddress.Length);
            ns.Write((short)server.Port);
            ns.Write((short)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
        }
    }

    /// <summary>
    /// Sends Information About Current Servers To Client.
    /// 关于服务器信息发送给客户端
    /// </summary>
    public sealed class NP_ServerList : NetPacket
    {
        /// <summary>
        /// 发送服务器列表
        /// </summary>
        public NP_ServerList(string clientVersion) : base(0x08, true)
        {
            List<GameServer> m_Current = GameServerController.CurrentGameServers.Values.ToList<GameServer>();

            //写入服务器数量
            ns.Write((byte)m_Current.Count);
            foreach (GameServer server in m_Current)
            {
                ns.Write((byte)server.Id);
                if(clientVersion=="1")
                ns.Write((short)0x00);
                ns.WriteUTF8Fixed(server.Name,System.Text.UTF8Encoding.UTF8.GetByteCount(server.Name));
                //ns.WriteASCIIFixed(server.Name, server.Name.Length);
                byte online = server.IsOnline() ? (byte)0x01 : (byte)0x02; //1在线 2离线
                ns.Write((byte)online); //Server Status - 0x00 
                int status = server.CurrentAuthorized.Count >= server.MaxPlayers ? 0x01 : 0x00; 
                ns.Write((byte)status); //Server Status - 0x00 - 正常 / 0x01 - 负载 / 0x02 - 排队
                ns.Write((byte)0x00);//未知
                //以下部分是服务器选择界面的该服务器哪些种族限制创建  0正常  2禁止
                ns.Write((byte)0x00);//诺亚族
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);//矮人族
                ns.Write((byte)0x00);//精灵族
                ns.Write((byte)0x00);//哈里兰
                ns.Write((byte)0x00);//兽灵族
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);//战魔族 
            }

            //写入当前用户账号数
            ns.Write((byte)0x00);
            /*
            ns.Write((byte)0x01); //Last Server Id?
            ns.Write((short)0x288C); //Current Users???
            ns.Write((short)0x22); //Undefined
            ns.Write((short)0x174); //Undefined
            ns.Write((short)0x3DEF); //Undefined
            ns.Write((byte)0x00); //Undefined
            
            //String? CharName? Probably Last Character.
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);
            
            //Undefined
            ns.Write((byte)0x01);
            ns.Write((byte)0x02);

            //String? 
            //Undefined //Ten Char String Undefined
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);
            ns.Write((int)0x00); //undefined
            ns.Write((int)0x00); //undefined
            */
        }
    }

    /// <summary>
    /// Sends Information about that Password Were Correct
    ///
    ///发送sessionId
    /// 如果没有错误的话
    /// </summary>
    public sealed class NP_PasswordCorrect : NetPacket
    {
        public NP_PasswordCorrect(int sessionId) : base(0x03, true)
        {
            //原版
            ////ns.Write((int)sessionId);
            //ns.Write((short)0x755c);
            //ns.Write((byte)0x1a);
            //ns.Write((int)0x00);
            //ns.Write((int)0x00);
            ////string encrypted = "f3d02d5dda564e7bb4320de5b27f5c78";
            ////ns.WriteASCIIFixed("\u", '\u'.Length);

            //中国区版（不知道作用，但是后面会用到，可能是密钥之类的）经测试并非sessionID，此值多账号多服务器固定（中国区）
            //ns.Write((short)0xeb63);
            ns.Write((byte)0xeb);
            ns.Write((byte)0x63);
            ns.Write((byte)0x4a);
            ns.Write((byte)0x1d);
            ns.Write((byte)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
        }
    }
    //将账号Id发送回客户端
    public sealed class NP_03key : NetPacket
    {
        public NP_03key(string clientVersion) : base(0x03, true)
        {
            //账号ID
            
            ns.Write((byte)0x1);
            ns.Write((byte)0x0);
            ns.Write((byte)0x0);
            ns.Write((byte)0x0);
            ns.Write((int)0x00);
            if (clientVersion == "1")
            {
                ns.Write((int)0x00);
            }
            else
            {
                ns.WriteUTF8Fixed("e10adc3949ba59abbe56e057f20f883e", "e10adc3949ba59abbe56e057f20f883e".Length);
            }
            ns.Write((byte)0x00);
        }
    }

    /// <summary>
    /// Sends Information About Rijndael(AES) Key
    /// 发送信息加密（AES）的管件
    /// </summary>
    public sealed class NP_AESKey : NetPacket
    {
        public NP_AESKey() : base(0x04, true)
        {
            //Rijndael / SHA256
            ns.Write((int)5000); //Undefined? 5000
            //le - string
            ns.WriteASCIIFixed("xnDekI2enmWuAvwL", 16); //Always 16?
            byte[] b = new byte[32];
            ns.Write(b, 0, b.Length);
        }
    }

    /// <summary>
    /// Sends Message Box About That Error Occured While Logging In.
    /// 发送信息框关于登陆错误的问题
    /// </summary>
    public sealed class NP_FailLogin : NetPacket
    {
        public NP_FailLogin() : base(0x0C, true)
        {
            ns.Write((byte)0x02); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x00);//Undefined
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NP_EditMessage : NetPacket
    {
        public NP_EditMessage() : base(0x0C, true)
        {
            ns.Write((short)0x0d); // Reason
        }
    }

    public sealed class NP_EditMessage2:NetPacket
    {
        public NP_EditMessage2(string Message):base (0x0C,true)
        {
            ns.Write((short)0x054c);
            ns.Write((short)0x00);
            ns.Write((int)0x00);
            ns.WriteASCIIFixed(Message, Message.Length);
            //ns.Write((byte)0x00);

        }
    }
    
    /// <summary>
    /// 重复登陆
    /// </summary>
    public sealed class NP_DuplicateLogin:NetPacket
    {
        public NP_DuplicateLogin() : base(0x07, true)
        {
            ns.Write((short)0x0c); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x03);//Undefined
        }

    }
}
