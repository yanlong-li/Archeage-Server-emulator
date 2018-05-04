using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SmartEngine.Core;
using SmartEngine.Network.Memory;
namespace SmartEngine.Network
{
    public abstract class Network<T>
    {
        static Network<T> impl = new DefaultNetwork<T>();
        protected bool encrypt = true;
        protected bool autoLock = false;

        Socket sock;

        /// <summary>
        /// 加密算法实例
        /// </summary>
        public Encryption Crypt;
        //internal NetworkStream stream;

        /// <summary>
        /// 绑定的客户端
        /// </summary>
        internal Session<T> client;

        protected bool isDisconnected;
        private bool disconnecting;

        internal int waitCounter = 0;
        protected DateTime receiveStamp = DateTime.Now;
        protected DateTime sendStamp = DateTime.Now;
        protected int receivedBytes = 0;
        protected int sentBytes = 0;
        protected int avarageReceive = 0;
        protected int avarageSend = 0;
        protected byte[] lastContent = null;
        SocketAsyncEventArgs receiveCompletion;
        object bufferLockObj = new object();
        int currentBufferIndex;
        BufferBlock currentBlock;
        bool aldreadyInQueue;
        Queue<BufferBlock> pendingQueue = new Queue<BufferBlock>();
        /// <summary>
        /// 已用发送缓存
        /// </summary>
        internal int usedSendBuffer = 0;
        public delegate void PacketEventArg(Packet<T> p);
        
        public static Network<T> Implementation { get { return impl; } set { impl = value; } }

        /// <summary>
        /// 是否屏蔽未知封包提示
        /// </summary>
        public static bool SuppressUnknownPackets { get; set; }

        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type
        /// </summary>
        protected Dictionary<T, Packet<T>> commandTable;

        /// <summary>
        /// 本NetIO绑定的套接字
        /// </summary>
        public Socket Socket { get { return sock; } }

        /// <summary>
        /// 标识该IO是否处于断线状态
        /// </summary>
        public bool Disconnected { get { return this.isDisconnected; } }
        /// <summary>
        /// 是否需要加密、解密封包
        /// </summary>
        public bool Encrypt { get { return encrypt; } set { this.encrypt = value; } }
        /// <summary>
        /// Whether the packet is automatically locked when the packet is processed.
        /// It is recommended that internal communication be set to false during services that do not require synchronization.
        /// Global locks are not recommended due to the fact that the protection against deadlocks is more difficult to control. 
        /// </summary>
        [Obsolete("Global locks are not recommended due to the fact that the protection against deadlocks is more difficult to control.\r\n", false)]        
        public bool AutoLock { get { return autoLock; } set { autoLock = value; } }

        /// <summary>
        /// Current average used upstream bandwidth in bytes 
        /// </summary>
        public int UpStreamBand { get { return avarageSend; } }

        /// <summary>
        /// The current average used downlink bandwidth in bytes
        /// </summary>
        public int DownStreamBand { get { return avarageReceive; } }

        public abstract Network<T> CreateNewInstance(Socket sock, Dictionary<T, Packet<T>> commandTable, Session<T> client);

        protected void CreateNewInstance(Network<T> network, Socket sock, Dictionary<T, Packet<T>> commandTable, Session<T> client)
        {
            network.sock = sock;
            //network.stream = new NetworkStream(sock);
            network.commandTable = commandTable;
            network.client = client;
            network.Crypt = Encryption.Implementation.Create();
            network.isDisconnected = false;
        }

        void DoDisconnectAndClearup()
        {
            receiveCompletion.Completed -= Receive_Completed;
            BufferBlock block = receiveCompletion.UserToken as BufferBlock;
            if (block != null)
            {
                block.Free();
            }
            receiveCompletion.UserToken = null;
            receiveCompletion.Dispose();
            lock (bufferLockObj)
            {
                while (pendingQueue.Count > 0)
                {
                    pendingQueue.Dequeue().Free();
                }
            }
            OnDisconnect();
            client = null;
        }

        /// <summary>
        /// 当断开连接的时候会被呼叫
        /// </summary>
        protected virtual void OnDisconnect()
        {
           
        }

        /// <summary>
        /// 当连接上时会被呼叫
        /// </summary>
        public virtual void OnConnect()
        {
        }
        /// <summary>
        /// 断开连接
        /// <param name="waitSendComplete">是否等待堆积封包发送完毕</param>
        /// </summary>
        public void Disconnect(bool waitSendComplete = false)
        {
            try
            {

                if (this.isDisconnected)
                {
                    return;
                }
                this.isDisconnected = true;
                try
                {
                    if (!disconnecting)
                    {
                        disconnecting = true;
                        this.client.Connected = false;
                        if (this.client.clientManager != null)
                            this.client.clientManager.RemoveClient(this.client);
                        if (waitSendComplete)
                            WaitTillAllPacketSent();
                        this.client.OnDisconnect();
                    }
                }
                catch (Exception e) { SmartEngine.Core.Logger.ShowError(e); }
                try
                {
                    Logger.ShowInfo(sock.RemoteEndPoint.ToString() + " disconnected");
                }
                catch (Exception)
                {
                }
                //try { stream.Close(); }
                //catch (Exception) { }

                try { sock.Close(); }
                catch (Exception) { }
            }
            catch (Exception e)
            {
                Core.Logger.ShowError(e);
                //try { stream.Close(); }
                //catch (Exception) { }

                //try { sock.Disconnect(true); }
                try { sock.Close(); }
                catch (Exception) { }
                //Logger.ShowInfo(sock.RemoteEndPoint.ToString() + " disconnected", null);
            }
            DoDisconnectAndClearup();
            sock.Dispose();
            //this.nlock.ReleaseWriterLock(); 
        }

        /// <summary>
        /// 设置当前网络层模式，客户端或服务器端
        /// </summary>
        /// <param name="mode">需要设定的模式</param>
        public virtual void SetMode(Mode mode)
        {
            receiveCompletion = new System.Net.Sockets.SocketAsyncEventArgs();
            BufferBlock block = BufferManager.Instance.RequestBufferBlock();
            block.UserToken = this;
            receiveCompletion.SetBuffer(block.Buffer, block.StartIndex, block.MaxLength);
            receiveCompletion.Completed += new EventHandler<System.Net.Sockets.SocketAsyncEventArgs>(Receive_Completed);
            receiveCompletion.UserToken = block;
            sock.ReceiveAsync(receiveCompletion);   
        }

        /// <summary>
        /// 阻塞线程直到所有堆积封包已发送完毕
        /// </summary>
        public void WaitTillAllPacketSent()
        {
            while (pendingQueue.Count > 0)
            {
                Thread.Sleep(100);
            }
            Thread.Sleep(100);
        }

        bool HandleReceived(System.Net.Sockets.SocketAsyncEventArgs e)
        {
            if (isDisconnected || client == null)
            {
                return false;
            }
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                try
                {
                    DateTime now = DateTime.Now;

                    this.receivedBytes += e.BytesTransferred;
                    if ((now - this.receiveStamp).TotalSeconds > 10)
                    {
                        this.avarageReceive = (int)(this.receivedBytes / (now - this.receiveStamp).TotalSeconds);
                        this.receivedBytes = 0;
                        this.receiveStamp = now;
                    }
                    if (this.lastContent == null)
                    {
                        byte[] buf = new byte[e.BytesTransferred];
                        Array.Copy(e.Buffer, e.Offset, buf, 0, e.BytesTransferred);

                        if (!this.isDisconnected)
                            this.OnReceivePacket(buf);
                    }
                    else
                    {
                        byte[] buf = new byte[e.BytesTransferred + this.lastContent.Length];
                        this.lastContent.CopyTo(buf, 0);
                        Array.Copy(e.Buffer, e.Offset, buf, this.lastContent.Length, e.BytesTransferred);
                        if (!this.isDisconnected)
                            this.OnReceivePacket(buf);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
            else
            {
                if (e.SocketError != SocketError.Success && e.SocketError != SocketError.ConnectionReset && e.SocketError != SocketError.ConnectionAborted)
                    Logger.ShowError(new SocketException((int)e.SocketError));
                return false;
            }
            return true;
        }
        internal static void Receive_Completed(object sender, System.Net.Sockets.SocketAsyncEventArgs e)
        {
            BufferBlock block = e.UserToken as BufferBlock;
            if (block == null)
                return;
            Network<T> network = block.UserToken as Network<T>;
            e.UserToken = null;

            if (network != null && network.HandleReceived(e))
            {
                if (!network.isDisconnected)
                {
                    BufferBlock newBlock = BufferManager.Instance.RequestBufferBlock();
                    newBlock.UserToken = network;
                    e.UserToken = newBlock;
                    e.SetBuffer(newBlock.Buffer, newBlock.StartIndex, newBlock.MaxLength);
                    try
                    {
                        if (!network.sock.ReceiveAsync(e))
                        {
                            Receive_Completed(null, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        newBlock.Free();
                    }
                }
            }
            else
            {
                if (network != null)
                {
                    if (network.autoLock)
                        ClientManager.EnterCriticalArea();
                    network.Disconnect();
                    if (network.autoLock)
                        ClientManager.LeaveCriticalArea();
                }
                else
                    Logger.ShowWarning("network == null");
            }
            block.Free();
        }

        /// <summary>
        /// 当网络层接收到封包时会被调用，请自行处理封包拆分，拆分后剩余部分缓存请赋予lastContent，再下次呼叫OnReceivePacket的时候则会自动在lastContent结尾处接收数据
        /// </summary>
        /// <param name="buffer">收到的数据</param>
        protected abstract void OnReceivePacket(byte[] buffer);

        /// <summary>
        /// 放送封包元数据，请在SendPacket中处理完封包头等的写入后呼叫此方法将数据发送
        /// </summary>
        /// <param name="buffer">需要发送的数据所在的缓存区</param>
        /// <param name="offset">数据开始的偏移</param>
        /// <param name="length">数据长度</param>
        protected void SendPacketRaw(byte[] buffer, int offset, int length)
        {
            if (isDisconnected || client == null)
                return;
            sentBytes += length;
            DateTime now = DateTime.Now;
            if ((now - sendStamp).TotalSeconds > 10)
            {
                avarageSend = (int)(sentBytes / (now - sendStamp).TotalSeconds);
                sentBytes = 0;
                sendStamp = now;
            }
            lock (bufferLockObj)
            {
                int index = 0;
                while (index < length)
                {
                    if (currentBlock == null)
                    {
                        currentBlock = BufferManager.Instance.RequestBufferBlock();
                        int size = currentBlock.MaxLength >= (length - index) ? (length - index) : currentBlock.MaxLength;
                        Array.Copy(buffer, offset + index, currentBlock.Buffer, currentBlock.StartIndex, size);
                        currentBufferIndex = size;
                        currentBlock.UsedLength = size;
                        EnqueueSendRequest(currentBlock);
                        if (size == currentBlock.MaxLength)
                        {
                            currentBufferIndex = 0;
                            currentBlock = null;
                        }
                        index += size;
                    }
                    else
                    {

                        int avaliable = currentBlock.MaxLength - currentBlock.UsedLength;
                        if (avaliable >= (length - index))
                        {
                            Array.Copy(buffer, offset + index, currentBlock.Buffer, currentBlock.StartIndex + currentBufferIndex, length - index);
                            currentBlock.UsedLength += (length - index);
                            currentBufferIndex += (length - index);
                            index += (length - index);
                        }
                        else
                        {
                            Array.Copy(buffer, offset + index, currentBlock.Buffer, currentBlock.StartIndex + currentBufferIndex, avaliable);
                            currentBlock.UsedLength += avaliable;
                            index += avaliable;
                            currentBufferIndex = 0;
                            currentBlock = null;
                        }
                    }
                }
            }
        }

        void EnqueueSendRequest(BufferBlock block)
        {
            if (aldreadyInQueue)
            {
                pendingQueue.Enqueue(block);
            }
            else
            {
                aldreadyInQueue = true;
                ClientManager<T>.EnqueueSendRequest(this, block);
            }
        }

        internal void BlockHandled(BufferBlock block)
        {
            lock (bufferLockObj)
            {
                if (currentBlock == block)
                    currentBlock = null;
            }
        }

        internal static void Send_Completed(object sender, System.Net.Sockets.SocketAsyncEventArgs e)
        {
            BufferBlock block = e.UserToken as BufferBlock;
            Network<T> network = block.UserToken as Network<T>;
            block.Free();
            if (network == null || network.isDisconnected || network.client == null)
            {
                ClientManager<T>.FinishSendQuest(e);
                return;
            }
            lock (network.bufferLockObj)
            {
                if (network.pendingQueue.Count > 0)
                {
                    BufferBlock block2 = network.pendingQueue.Dequeue();
                    ClientManager<T>.EnqueueSendRequest(network, block2);
                }
                else
                    network.aldreadyInQueue = false;
            }
            try
            {
                if (e.SocketError != SocketError.Success)
                {
                    if (e.SocketError != SocketError.ConnectionReset && e.SocketError != SocketError.ConnectionAborted)
                        Logger.ShowError(new SocketException((int)e.SocketError));
                    if (network.autoLock)
                        ClientManager.EnterCriticalArea();
                    network.Disconnect();
                    if (network.autoLock)
                        ClientManager.LeaveCriticalArea();
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager<T>.FinishSendQuest(e);                
        }
        /// <summary>
        /// 发送封包
        /// </summary>
        /// <param name="p">需要发送的封包</param>
        /// <param name="noWarper">是否不需要封装封包头，仅用于交换密钥，其余时候不建议使用</param>
        public abstract void SendPacket(Packet<T> p, bool noWarper);

        /// <summary>
        /// 发送封包
        /// </summary>
        /// <param name="p">需要发送的封包</param>
        public abstract void SendPacket(Packet<T> p);

        /// <summary>
        /// 当拆分完封包后呼叫，调用此方法后将自动根据封包分派函数交由事先指定的处理函数处理该封包
        /// </summary>
        /// <param name="p">需要处理的封包</param>
        protected virtual void ProcessPacket(Packet<T> p)
        {
            if (p.data.Length < 2) return;
            if (client == null)
                return;
            {
                try
                {
                    Packet<T> command;
                    commandTable.TryGetValue(p.ID, out command);
                    if (command != null)
                    {
                        Packet<T> p1 = command.New();
                        p1.data = p.data;
                        p1.length = p.length;
                        if (autoLock)
                            ClientManager.EnterCriticalArea();
                        try
                        {
                            if (this.client != null)
                                p1.OnProcess(this.client);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                        if (autoLock)
                            ClientManager.LeaveCriticalArea();
                    }
                    else
                    {
                        if (commandTable.ContainsKey((T)(object)0xFFFF))
                        {
                            Packet<T> p1 = commandTable[(T)(object)0xFFFF].New();
                            p1.data = p.data;
                            if (autoLock)
                                ClientManager.EnterCriticalArea();
                            try
                            {
                                if (this.client != null)
                                    p1.OnProcess(this.client);
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                            if (autoLock)
                                ClientManager.LeaveCriticalArea();
                        }
                        else
                        {
                            if (!SuppressUnknownPackets)
                                Logger.ShowDebug(string.Format("Unknown Packet:{2}(0x{0:X4})\r\n       Data:{1}", (int)(object)p.ID, p.DumpData(), p.ID));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }                
            }
        }
    }
}
