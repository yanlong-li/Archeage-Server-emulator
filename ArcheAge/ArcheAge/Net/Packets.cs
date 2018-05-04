using ArcheAge.ArcheAge.Net.Connections;
using ArcheAge.Properties;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAge.ArcheAge.Net
{
    /// <summary>
    /// Sends Request To Login Server For Authorization.
    /// </summary>
    public sealed class Net_RegisterGameServer : NetPacket
    {
        public Net_RegisterGameServer() : base(0x00, true)
        {
            Settings m_Default = Settings.Default;
            ns.Write((byte)m_Default.Game_Id);
            ns.Write((short)m_Default.ArcheAge_Port);
            ns.WriteDynamicASCII(m_Default.ArcheAge_IP);
            ns.WriteDynamicASCII(m_Default.Game_Password);
        }
    }

    /// <summary>
    /// Sends To Login Server Information About That Character Count Update For Specified Account Is Required.
    /// </summary>
    public sealed class Net_UpdateCharacterCount : NetPacket
    {
        public Net_UpdateCharacterCount(int accountId, int characters) : base(0x02, true)
        {
            ns.Write((int)accountId);
            ns.Write((int)characters);
        }
    }

    public sealed class test : NetPacket
    {
        public test() : base(0x0100, true)
        {
            //ns.Write((byte)0x00);
            //ns.Write((byte)0x01);
            ns.Write((byte)0xff);
            ns.Write((byte)0xfe);
            ns.Write((short)0x00);
            ns.Write((byte)0xcc);
            ns.Write((byte)0x04);
            ns.Write((short)0x00);
            ns.Write((byte)0xca);
            ns.Write((byte)0x04);
            ns.Write((short)0x00);
            ns.Write((byte)0xeb);
            ns.Write((byte)0x63);
            ns.Write((byte)0x4a);
            ns.Write((byte)0x1d);
            ns.Write((short)0x00);
            ns.Write((short)0x00);
            ns.Write((byte)0xf2);//
            ns.Write((byte)0x0b);//
            ns.Write((byte)0xe9);//
            ns.Write((byte)0x7a);//
            ns.Write((byte)0xff);
            ns.Write((byte)0xff);
            ns.Write((byte)0xff);
            ns.Write((byte)0xff);
            ns.Write((byte)0x00);
            ns.Write((byte)0x81);
            ns.Write((byte)0x85);
            ns.Write((byte)0x05);
            ns.Write((short)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
        }
    }
}
