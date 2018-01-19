using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LocalCommons.Native.Network
{
    /// <summary>
    /// Abstract Class For Writing Packets
    /// Author: Raphail
    /// </summary>
    public abstract class NetPacket
    {
        protected PacketWriter ns;
        private int packetId;
        private bool m_littleEndian;
        private bool m_IsArcheAge;
        private byte level;

        public bool IsArcheAgePacket
        {
            get { return m_IsArcheAge; }
            set { m_IsArcheAge = true; }
        }

        /// <summary>
        /// Creates Instance Of Any Other Packet
        /// </summary>
        /// <param name="packetId">Packet Identifier(opcode)</param>
        /// <param name="isLittleEndian">Send Data In Little Endian Or Not.</param>
        protected NetPacket(int packetId, bool isLittleEndian)
        {
            this.packetId = packetId;
            ns = PacketWriter.CreateInstance(8192, isLittleEndian);
            this.m_littleEndian = isLittleEndian;
        }

        /// <summary>
        /// Creates Instance Of ArcheAge Game Packet.
        /// </summary>
        /// <param name="level">Packet Level</param>
        /// <param name="packetId">Packet Identifier(opcode)</param>
        protected NetPacket(byte level, int packetId)
        {
            this.packetId = packetId;
            this.level = level;
            this.m_littleEndian = true;
            this.m_IsArcheAge = true;
            ns = PacketWriter.CreateInstance(8192, true);
        }

        /// <summary>
        /// Stream Where We Writing Data.
        /// </summary>
        public PacketWriter UnderlyingStream
        {
            get { return ns; }
        }

        /// <summary>
        /// Compiles Data And Return Compiled byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] Compile()
        {
            PacketWriter temporary = PacketWriter.CreateInstance(8192 * 4, m_littleEndian);
            temporary.Write((short)(ns.Length + (m_IsArcheAge ? 4 : 2)));
            if (m_IsArcheAge)
            {
                temporary.Write((byte)0xDD);
                temporary.Write((byte)level);
                temporary.Write((short)packetId);
            }
            else
                temporary.Write((short) packetId);
            byte[] redata = ns.ToArray();
            PacketWriter.ReleaseInstance(ns);
            ns = null;
            temporary.Write(redata, 0, redata.Length);
            byte[] compiled = temporary.ToArray();
            PacketWriter.ReleaseInstance(temporary);
            temporary = null;
            return compiled;
        }
    }
}
