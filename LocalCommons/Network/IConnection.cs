using LocalCommons.Logging;
using LocalCommons.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LocalCommons.Network
{
    /// <summary>
    /// Abstract Connection Which You Must Inherit
    /// Author: Raphail
    /// </summary>
    public abstract class IConnection : IDisposable
    {
        protected Socket m_CurrentChannel;
        private SocketAsyncEventArgs m_AsyncReceive;
        private byte[] m_RecvBuffer;
        private readonly object m_SyncRoot = new object();
        private Queue<NetPacket> m_PacketQueue;
        private bool m_Disposing;
        private readonly DateTime m_NextCheckActivity;
        private readonly string m_Address;
        private static int m_CoalesceSleep = -1;
        private bool m_BlockAllPackets;
        private readonly DateTime m_ConnectedOn;
        private static BufferPool m_RecvBufferPool = new BufferPool("Receive", 4096, 4096);
        protected event EventHandler DisconnectedEvent;
        private bool m_Running;

        //For ArcheAge Connections.
        protected bool m_LittleEndian;

        /// <summary>
        /// Current TCP Client.
        /// </summary>
        public Socket CurrentChannel
        {
            get { return m_CurrentChannel; }
        }

        /// <summary>
        /// Sleeping On Send.
        /// </summary>
        public static int CoalesceSleep
        {
            get { return m_CoalesceSleep; }
            set { m_CoalesceSleep = value; }
        }

        /// <summary>
        /// Blocking Packets - Not Send Means.
        /// </summary>
        public bool BlockAllPackets
        {
            get { return m_BlockAllPackets; }
            set { m_BlockAllPackets = value; }
        }

        /// <summary>
        /// New Instance Of IConnection or Any Your Connection.
        /// </summary>
        /// <param name="socket">Accepted Socket.</param>
        public IConnection(Socket socket)
        {
            m_CurrentChannel = socket;
            m_ConnectedOn = DateTime.Now;
            m_RecvBuffer = m_RecvBufferPool.AcquireBuffer();
            //-------------Async Receive ----------------------
            m_AsyncReceive = new SocketAsyncEventArgs();
            m_AsyncReceive.Completed += M_AsyncReceive_Completed;
            m_AsyncReceive.SetBuffer(m_RecvBuffer, 0, m_RecvBuffer.Length);
            //-------------------------------------------------
            m_PacketQueue = new Queue<NetPacket>();
            //-----------------------------------------------
            m_Address = ((IPEndPoint)m_CurrentChannel.RemoteEndPoint).Address.ToString();
            if (m_CurrentChannel == null)
            {
                return;
            }

            RunReceive();
            m_Running = true;
        }

        /// <summary>
        /// Set TRUE If you want Break Running.
        /// </summary>
        private readonly bool BreakRunProcess;

        /// <summary>
        /// Start Running Receiving Process.
        /// </summary>
        public void RunReceive()
        {
            try
            {
                bool res = false;
                do
                {
                    if (m_AsyncReceive == null) //Disposed
                    {
                        break;
                    }

                    lock (m_SyncRoot)
                    {
                        res = !m_CurrentChannel.ReceiveAsync(m_AsyncReceive);
                    }

                    if (res)
                    {
                        ProceedReceiving(m_AsyncReceive);
                    }
                }
                while (res);
            }
            catch (Exception e)
            {
                Logger.Trace(e.ToString());
                DisconnectedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Adds Packet To Queue And After Send It.
        /// </summary>
        /// <param name="packet"></param>
        public virtual void SendAsync(NetPacket packet)
        {
            if (CoalesceSleep != -1)
            {
                Thread.Sleep(CoalesceSleep);
            }

            m_PacketQueue.Enqueue(packet);
            M_AsyncSend_Do();
        }

        /// <summary>
        /// Calls When We Need Send Data
        /// </summary>
        private void M_AsyncSend_Do()
        {
            try
            {
                if (m_PacketQueue.Count > 0)
                {
                    NetPacket packet = m_PacketQueue.Dequeue();
                    byte[] compiled = packet.Compile();
                    m_CurrentChannel.Send(compiled, compiled.Length, SocketFlags.None); //отправляем пакет

                    //ushort length = BitConverter.ToUInt16(compiled, 0); //проверяем, есть ли еще пакет
                    //--- Console Hexadecimal 
                    //вывод лога пакетов в консоль
                    /*StringBuilder*/
                    StringBuilder builder = new StringBuilder();
                    builder.Append("Send: ");
                    //builder.Append(Utility.IntToHex(compiled.Length));
                    //builder.Append(" ");
                    for (int i = 0; i < compiled.Length; i++)
                    {
                        builder.AppendFormat("{0:X2} ", compiled[i]);
                    }
                    //не выводим Pong
                    if (compiled[4] != 0x13)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Logger.Trace(builder.ToString());
                        Console.ResetColor();
                    }
                    //обрабатываем слипшиеся пакеты
                    //ushort offset = 2;
                    //ushort length = BitConverter.ToUInt16(compiled, 0); //проверяем, есть ли еще пакет
                    //var path = "d:\\dump.txt";
                    //while (length > 0 && offset < compiled.Length)
                    //{
                    //    byte[] data = new byte[length];
                    //    Buffer.BlockCopy(compiled, offset, data, 0, length);
                    //m_CurrentChannel.Send(data, data.Length, SocketFlags.None); //отправляем на обработку данные пакета

                    //--- Console Hexadecimal 
                    //вывод лога пакетов в консоль
                    //*StringBuilder*/
                    //StringBuilder builder = new StringBuilder();
                    //builder.Append("Send: ");
                    //builder.Append(Utility.IntToHex(length));
                    //builder.Append(" ");
                    //for (int i = 0; i < length; i++)
                    //{
                    //    builder.AppendFormat("{0:X2} ", data[i]);
                    //}
                    //не выводим Pong
                    //if (data[2] != 0x13)
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Gray;
                    //    Logger.Trace(builder.ToString());
                    //    Console.ResetColor();
                    //}
                    //--- Console Hexadecimal
#if DEBUG
                    //--- File Hexadecimal
                    //вывод лога пакетов в файл
                    //не выводим Pong
                    //if (data[2] != 0x13)
                    //{
                    //    var fs = new FileStream(path, FileMode.Append);
                    //    var sw = new StreamWriter(fs);
                    //    sw.WriteLine(builder.ToString());
                    //    sw.Close();
                    //    fs.Close();
                    //}
#endif
                    //--- File Hexadecimal
                    //    offset += length;
                    //    if (offset >= compiled.Length)
                    //    {
                    //        break;
                    //    }
                    //
                    //    length = BitConverter.ToUInt16(compiled, offset); //проверяем, есть ли еще пакет
                    //    offset += 2;
                    //}
                    //#endif
                }
            }
            catch (Exception e)
            {
                Logger.Trace(e.ToString());
                DisconnectedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Adds Packet To Queue And After Send It.
        /// длина не вычисляется, надо самому дописать перед пакетом
        /// </summary>
        /// <param name="packet"></param>
        public virtual void SendAsyncHex(NetPacket packet)
        {
            if (CoalesceSleep != -1)
            {
                Thread.Sleep(CoalesceSleep);
            }

            byte[] compiled = packet.Compile2();
            m_CurrentChannel.Send(compiled, compiled.Length, SocketFlags.None);
            //--- Console Hexadecimal 
            StringBuilder builder = new StringBuilder();
            builder.Append("Send: ");
            foreach (byte b in compiled)
            {
                builder.AppendFormat("{0:X2} ", b);
            }
            //не выводим Pong
            if (compiled[4] != 0x13)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Logger.Trace(builder.ToString());
                Console.ResetColor();
            }
#if DEBUG
            //--- File Hexadecimal
            //не выводим Pong
            //if (compiled[4] != 0x13)
            //{
            //    var path = "d:\\dump.txt";
            //    var fs = new FileStream(path, FileMode.Append);
            //    var sw = new StreamWriter(fs);
            //    sw.WriteLine(builder.ToString());
            //    sw.Close();
            //    fs.Close();
            //}
#endif
            //отправляем пакет
        }

        /// <summary>
        /// Reading Length And Handles Data By [HandleReceived(byte[])] Without Length.
        /// </summary>
        /// <param name="e"></param>
        private void ProceedReceiving(SocketAsyncEventArgs e)
        {
            int transfered = e.BytesTransferred;
            if (e.SocketError != SocketError.Success || transfered <= 0)
            {
                DisconnectedEvent?.Invoke(this, EventArgs.Empty);
                return;
            }
            PacketReader reader = new PacketReader(m_RecvBuffer, 0);
            ushort length = reader.ReadLEUInt16();
            ushort offset = 2;
            //обрабатываем слипшиеся пакеты
            //var path = "d:\\dump.txt";
            while (length > 0 && offset < reader.Size)
            {
                byte[] data = new byte[length];
                Buffer.BlockCopy(m_RecvBuffer, offset, data, 0, length);
                //--- Console Hexadecimal 
                //сначало надо вывести лог пакета в консоль
                StringBuilder builder = new StringBuilder();
                builder.Append("Recv: ");
                builder.Append(Utility.IntToHex(length));
                builder.Append(" ");
                for (int i = 0; i < length; i++)
                {
                    builder.AppendFormat("{0:X2} ", data[i]);
                }
                //не выводим Ping
                if (data[2] != 0x12)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Logger.Trace(builder.ToString());
                    Console.ResetColor();
                }
#if DEBUG
                //--- File Hexadecimal
                //вывод лога пакетов в файл
                //не выводим Ping
                //if (data[2] != 0x12)
                //{
                //    var fs = new FileStream(path, FileMode.Append);
                //    var sw = new StreamWriter(fs);
                //    sw.WriteLine(builder.ToString());
                //    sw.Close();
                //    fs.Close();
                //}
#endif
                offset += length;
                reader.Offset = offset;
                length = reader.ReadLEUInt16(); //проверяем, есть ли еще пакет
                offset += 2;
                //и только затем отправить на обработку
                HandleReceived(data); //отправляем на обработку данные пакета
            }
            reader.Clear(); //почистим буфер, инача считываются старые данные
            reader = null;
        }

        /// <summary>
        /// Calls When Data Received From Server.
        /// </summary>
        /// <param name="data"></param>
        public abstract void HandleReceived(byte[] data);

        /// <summary>
        /// Returns Address Of Current Connection.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_Address;
        }

        /// <summary>
        /// Calls When Receiving Done.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_AsyncReceive_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProceedReceiving(e);
            if (!m_Disposing)
            {
                RunReceive();
            }
        }

        #region IDisposable Support
        /// <summary>
        /// Dispose Current Listener.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Fully Dispose Current Connection.
        /// Can Be Overriden.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                if (m_CurrentChannel == null || m_Disposing)
                {
                    return;
                }

                m_Disposing = true;

                try { m_CurrentChannel.Shutdown(SocketShutdown.Both); }
                catch (SocketException ex) { Logger.Trace(ex.ToString()); }

                try { m_CurrentChannel.Close(); }
                catch (SocketException ex) { Logger.Trace(ex.ToString()); }

                if (m_RecvBuffer != null)
                {
                    m_RecvBufferPool.ReleaseBuffer(m_RecvBuffer);
                }

                m_CurrentChannel.Close();
                m_AsyncReceive.Dispose();
                m_CurrentChannel = null;
                m_RecvBuffer = null;
                m_AsyncReceive = null;
                if (m_PacketQueue.Count <= 0)
                {
                    lock (m_PacketQueue)
                    {
                        m_PacketQueue.Clear();
                    }
                }

                m_PacketQueue = null;
                m_Disposing = false;
                m_Running = false;
            }
            // free native resources
        }
        #endregion
    }
}
