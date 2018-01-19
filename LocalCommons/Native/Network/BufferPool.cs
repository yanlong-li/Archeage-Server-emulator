using System;
using System.Collections;
using System.Collections.Generic;

namespace LocalCommons.Native.Network
{
    /// <summary>
    /// Buffer Pool For byte[]s
    /// Author: Raphail
    /// </summary>
	public class BufferPool
	{
		private static List<BufferPool> m_Pools = new List<BufferPool>();

		public static List<BufferPool> Pools{ get{ return m_Pools; } set{ m_Pools = value; } }

		private string m_Name;

		private int m_InitialCapacity;
		private int m_BufferSize;

		private int m_Misses;

		private Queue<byte[]> m_FreeBuffers;
        
        /// <summary>
        /// Writing Information About your Pool Into your Variables.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="freeCount">Free Buffer Count</param>
        /// <param name="initialCapacity">Initial Capacity</param>
        /// <param name="currentCapacity">Capacity In Use</param>
        /// <param name="bufferSize">Buffer Length</param>
        /// <param name="misses">Misses Count</param>
		public void GetInfo( out string name, out int freeCount, out int initialCapacity, out int currentCapacity, out int bufferSize, out int misses )
		{
			lock ( this )
			{
				name = m_Name;
				freeCount = m_FreeBuffers.Count;
				initialCapacity = m_InitialCapacity;
				currentCapacity = m_InitialCapacity * (1 + m_Misses);
				bufferSize = m_BufferSize;
				misses = m_Misses;
			}
		}

        /// <summary>
        /// Initializes New Buffer Pool
        /// </summary>
        /// <param name="name">Buffer Pool's Name/</param>
        /// <param name="initialCapacity">Buffer Pool's Capacity</param>
        /// <param name="bufferSize">Length Of Any Buffer.</param>
		public BufferPool( string name, int initialCapacity, int bufferSize )
		{
			m_Name = name;

			m_InitialCapacity = initialCapacity;
			m_BufferSize = bufferSize;

			m_FreeBuffers = new Queue<byte[]>( initialCapacity );

			for ( int i = 0; i < initialCapacity; ++i )
				m_FreeBuffers.Enqueue( new byte[bufferSize] );

			lock ( m_Pools )
				m_Pools.Add( this );
		}

        /// <summary>
        /// Returns Free Buffer.
        /// </summary>
        /// <returns></returns>
		public byte[] AcquireBuffer()
		{
			lock ( this )
			{
				if ( m_FreeBuffers.Count > 0 )
					return m_FreeBuffers.Dequeue();

				++m_Misses;

				for ( int i = 0; i < m_InitialCapacity; ++i )
					m_FreeBuffers.Enqueue( new byte[m_BufferSize] );

				return m_FreeBuffers.Dequeue();
			}
		}

        /// <summary>
        /// Releases Buffer and Put it to Free Buffers.
        /// </summary>
        /// <param name="buffer"></param>
		public void ReleaseBuffer( byte[] buffer )
		{
			if ( buffer == null )
				return;

			lock ( this )
				m_FreeBuffers.Enqueue( buffer );
		}

        /// <summary>
        /// Fully Release Buffer
        /// </summary>
		public void Free()
		{
			lock ( m_Pools )
				m_Pools.Remove( this );
		}
	}
}