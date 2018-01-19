using System;

namespace LocalCommons.Native.Network
{
    /// <summary>
    /// Delegate Which Class When Packet Received
    /// </summary>
    /// <typeparam name="T">Any Connection Where T : IConnection, new()</typeparam>
    /// <param name="net">Connection </param>
    /// <param name="reader"></param>
	public delegate void OnPacketReceive<T>(T net, PacketReader reader );

    /// <summary>
    /// PacketHandler That Uses For Holding Delegate(OnPacketReceive) For Handle Packets.
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class PacketHandler<T>
	{
		private int m_PacketID;
		private OnPacketReceive<T> m_OnReceive;

		public PacketHandler(int packetID, OnPacketReceive<T> onReceive )
		{
			m_PacketID = packetID;
			m_OnReceive = onReceive;
		}

		public int PacketID
		{
			get
			{
				return m_PacketID;
			}
		}

		public OnPacketReceive<T> OnReceive
		{
			get
			{
				return m_OnReceive;
			}
		}
	}
}