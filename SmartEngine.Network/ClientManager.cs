using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using SmartEngine.Core;
using SmartEngine.Network.Memory;
namespace SmartEngine.Network
{
    /// <summary>
    /// The main client manager class should not inherit this class but ClientManager(T),
    /// which contains key area synchronization implementation
    /// </summary>    
    public class ClientManager
    {
        static AutoResetEvent waitressQueue = new AutoResetEvent(true);

        static bool noCheckDeadLock = false;

        private static bool enteredcriarea = false;
        private static HashSet<Thread> blockedThread = new HashSet<Thread>();
        internal static Dictionary<string, Thread> Threads = new Dictionary<string, Thread>();
        private static Thread currentBlocker;
        private static DateTime timestamp;

        /// <summary>
        /// Is the main lock occluded?
        /// </summary>
        public static bool Blocked
        {
            get
            {
                return (blockedThread.Contains(Thread.CurrentThread));
            }
        }

        /// <summary>
        /// Add threads
        /// </summary>
        /// <param name="thread">Threads</param>
        public static void AddThread(Thread thread)
        {
            AddThread(thread.Name, thread);
        }

        /// <summary>
        /// Add threads
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="thread">Threads</param>
        public static void AddThread(string name, Thread thread)
        {
            if (!Threads.ContainsKey(name))
            {
                lock (Threads)
                {
                    try
                    {
                        Threads.Add(name, thread);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        Logger.ShowDebug("Threads count:" + Threads.Count);
                    }
                }
            }
        }

        /// <summary>
        /// Delete thread
        /// </summary>
        /// <param name="name">name</param>
        public static void RemoveThread(string name)
        {
            if (Threads.ContainsKey(name))
            {
                lock (Threads)
                {
                    Threads.Remove(name);
                }
            }
        }

        /// <summary>
        /// Get thread
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Threads</returns>
        public static Thread GetThread(string name)
        {
            if (Threads.ContainsKey(name))
            {
                lock (Threads)
                {
                    return Threads[name];
                }
            }
            else
                return null;
        }

        /// <summary>
        /// Deadlock detector
        /// </summary>
        internal void checkCriticalArea()
        {
            while (true)
            {
                if (enteredcriarea)
                {
                    TimeSpan span = DateTime.Now - timestamp;
                    if (span.TotalSeconds > 10 && !noCheckDeadLock && !Debugger.IsAttached)
                    {
                        Logger.ShowError("Deadlock detected");
                        Logger.ShowError("Automatically unlocking....");
                        StackTrace running;
                        try
                        {
                            if (currentBlocker != null)
                            {
                                Logger.ShowError("Call Stack of current blocking Thread:");
                                Logger.ShowError("Thread name:" + getThreadName(currentBlocker));
                                if (currentBlocker.ThreadState != System.Threading.ThreadState.Running)
                                    Logger.ShowWarning("Unexpected thread state:" + currentBlocker.ThreadState.ToString());
                                currentBlocker.Suspend();
                                running = new StackTrace(currentBlocker, true);
                                currentBlocker.Resume();
                                foreach (StackFrame i in running.GetFrames())
                                {
                                    Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                                }
                            }
                        }
                        catch (Exception ex) { Logger.ShowError(ex); }
                        Console.WriteLine();
                        Logger.ShowError("Call Stack of all blocking Threads:");
                        Thread[] list = blockedThread.ToArray();
                        foreach (Thread j in list)
                        {
                            try
                            {
                                Logger.ShowError("Thread name:" + getThreadName(j));
                                if (j.ThreadState != System.Threading.ThreadState.Running)
                                    Logger.ShowWarning("Unexpected thread state:" + j.ThreadState.ToString());
                                j.Suspend();
                                running = new StackTrace(j, true);
                                j.Resume();
                                foreach (StackFrame i in running.GetFrames())
                                {
                                    Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                                }
                            }
                            catch (Exception ex) { Logger.ShowError(ex); }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        Logger.ShowError("Call Stack of all Threads:");
                        string[] keys = new string[Threads.Keys.Count];
                        Threads.Keys.CopyTo(keys, 0);
                        foreach (string k in keys)
                        {
                            try
                            {
                                Thread j = GetThread(k);
                                Logger.ShowError("Thread name:" + k);
                                if (j.ThreadState != System.Threading.ThreadState.Running)
                                    Logger.ShowWarning("Unexpected thread state:" + j.ThreadState.ToString());
                                j.Suspend();
                                running = new StackTrace(j, true);
                                j.Resume();
                                foreach (StackFrame i in running.GetFrames())
                                {
                                    Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                                }
                            }
                            catch
                            {

                            }
                            Console.WriteLine();
                        }
                        LeaveCriticalArea(currentBlocker);
                    }
                }
                Thread.Sleep(10000);
            }
        }

        static string getThreadName(Thread thread)
        {
            foreach (string i in Threads.Keys)
            {
                if (thread == Threads[i])
                    return i;
            }
            return "";
        }

        /// <summary>
        /// Print out the current thread running on the console and its call stack
        /// </summary>
        public static void PrintAllThreads()
        {
            Logger.ShowWarning("Call Stack of all blocking Threads:");
            Thread[] list = blockedThread.ToArray();
            foreach (Thread j in list)
            {
                try
                {
                    Logger.ShowWarning("Thread name:" + getThreadName(j));
                    j.Suspend();
                    StackTrace running = new StackTrace(j, true);
                    j.Resume();
                    foreach (StackFrame i in running.GetFrames())
                    {
                        Logger.ShowWarning("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                    }
                }
                catch { }
                Console.WriteLine();
            }
            Logger.ShowWarning("Call Stack of all Threads:");
            string[] keys = new string[Threads.Keys.Count];
            Threads.Keys.CopyTo(keys, 0);
            foreach (string k in keys)
            {
                try
                {
                    Thread j = GetThread(k);
                    j.Suspend();
                    StackTrace running = new StackTrace(j, true);
                    j.Resume();
                    Logger.ShowWarning("Thread name:" + k);
                    foreach (StackFrame i in running.GetFrames())
                    {
                        Logger.ShowWarning("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                    }
                }
                catch
                {

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Enter key area
        /// </summary>
        public static void EnterCriticalArea()
        {
            if (blockedThread.Contains(Thread.CurrentThread))
            {
                Logger.ShowDebug("Current thread is already blocked, skip blocking to avoid deadlock!");
            }
            else
            {
                //Global.clientMananger.AddWaitingWaitress();
                waitressQueue.WaitOne();
                timestamp = DateTime.Now;
                enteredcriarea = true;
                blockedThread.Add(Thread.CurrentThread);
                currentBlocker = Thread.CurrentThread;
            }
        }

        /// <summary>
        /// Leave the key area
        /// </summary>
        public static void LeaveCriticalArea()
        {
            LeaveCriticalArea(Thread.CurrentThread);
        }

        /// <summary>
        /// Force a thread to leave the critical area
        /// </summary>
        /// <param name="blocker">Locked thread </param>
        public static void LeaveCriticalArea(Thread blocker)
        {
            if (blockedThread.Contains(blocker) || blockedThread.Count != 0)
            {
                int sec = (DateTime.Now - timestamp).Seconds;
                if (sec > 5)
                {
                    Logger.ShowDebug(string.Format("Thread({0}) used unnormal time till unlock({1} sec)", blocker.Name, sec));
                }
                enteredcriarea = false;
                if (blockedThread.Contains(blocker))
                    blockedThread.Remove(blocker);
                /*else
                {
                    if (blockedThread.Count > 0)
                        blockedThread.RemoveAt(0);
                }*/
                currentBlocker = null;
                timestamp = DateTime.Now;
                waitressQueue.Set();
            }
            else
            {
                Logger.ShowDebug("Current thread isn't blocked while trying unblock, skiping");
            }
        }
    }

    /// <summary>
    /// Client Manager
    /// </summary>
    /// <typeparam name="T">Packet Opcode Enumeration</typeparam>
    public abstract class ClientManager<T> : ClientManager
    {
        TcpListener listener;
        HashSet<Session<T>> clients = new HashSet<Session<T>>();
        bool isUp = false;
        int maxNewConnections = 10;
        int port;
        bool encrypt = true, autoLock = false;
        public static int InitialSendCompletionPort = 500;
        public static int NewSendCompletionPortEveryBatch = 200;
        static int currentSendCompletionPort;
        static ConcurrentQueue<SocketAsyncEventArgs> avaliableSendCompletion = new ConcurrentQueue<SocketAsyncEventArgs>();
        static ConcurrentQueue<KeyValuePair<Network<T>, BufferBlock>> sendRequests = new ConcurrentQueue<KeyValuePair<Network<T>, BufferBlock>>();
        static AutoResetEvent sendWaiter = new AutoResetEvent(false);
        static AutoResetEvent sendRequestWaiter = new AutoResetEvent(false);
        
        Dictionary<T, Packet<T>> commandTable = new Dictionary<T, Packet<T>>();
        static List<Network<T>> pendingNetIO = new List<Network<T>>();
        static Thread sender;
        static bool shouldEnd;
        Thread check;
        Thread mainLoop;

        /// <summary>
        /// Currently connected client
        /// </summary>
        public HashSet<Session<T>> Clients { get { return clients; } }

        /// <summary>
        /// The maximum number of connections that can be received at one time
        /// </summary>
        public int MaxNewConnections { get { return maxNewConnections; } set { maxNewConnections = value; } }

        public static int CurrentCompletionPort { get { return currentSendCompletionPort; } }

        public static int FreeCompletionPort { get { return avaliableSendCompletion.Count; } }

        /// <summary>
        /// Server listening port
        /// </summary>
        public int Port { get { return port; } set { this.port = value; } }

        public bool Encrypt { get { return encrypt; } set { this.encrypt = value; } }

        /// <summary>
        /// Whether the packet is automatically locked when the packet is processed. 
        /// It is recommended that internal communication be set to false during services that do not require 
        /// synchronization. Global locks are not recommended due to the fact that the protection against deadlocks 
        /// is more difficult to control.
        /// </summary>
        [Obsolete("Global locks are not recommended due to the fact that the protection against deadlocks is more difficult to control.", false)]
        public bool AutoLock { get { return autoLock; } set { this.autoLock = value; } }

        public virtual bool Start()
        {
            mainLoop = new Thread(new ThreadStart(this.NetworkLoop));
            mainLoop.Start();

            this.listener = new TcpListener(port);
            try { listener.Start(); }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
            }
            isUp = true;
            return true;            
        }

        public virtual void Stop()
        {
            isUp = false;
            if (this.listener != null)
                this.listener.Stop();
            mainLoop.Abort();
            mainLoop = null;
            StopSendReceiveThreads();
            sender = null;
        }

        internal static void StopSendReceiveThreads()
        {
            shouldEnd = true;
            sendRequestWaiter.Set();
            sender = null;
        }

        internal static void EnqueueSendRequest(Network<T> network, BufferBlock buffer)
        {
            if (sender == null)
            {
                shouldEnd = false;
                sender = new Thread(sendLoop);
                sender.Start();
                
                for (int i = 0; i < InitialSendCompletionPort - avaliableSendCompletion.Count; i++)
                {
                    SocketAsyncEventArgs res = new SocketAsyncEventArgs();
                    res.Completed += Network<T>.Send_Completed;
                    avaliableSendCompletion.Enqueue(res);
                }
                currentSendCompletionPort = avaliableSendCompletion.Count;
            }
            buffer.UserToken = network;
            KeyValuePair<Network<T>, BufferBlock> req = new KeyValuePair<Network<T>,BufferBlock>(network, buffer);
            sendRequests.Enqueue(req);
            sendRequestWaiter.Set();
            
        }

        internal static void FinishSendQuest(SocketAsyncEventArgs e)
        {
            e.UserToken = null;
            avaliableSendCompletion.Enqueue(e);
            sendWaiter.Set();
        }

        static void sendLoop()
        {
            while (!shouldEnd)
            {
                SocketAsyncEventArgs arg;
                KeyValuePair<Network<T>,BufferBlock> req;
                while (sendRequests.TryDequeue(out req))
                {
                    while (!avaliableSendCompletion.TryDequeue(out arg))
                    {
                        if (!sendWaiter.WaitOne(10))
                        {
                            for (int i = 0; i < NewSendCompletionPortEveryBatch; i++)
                            {
                                SocketAsyncEventArgs res = new SocketAsyncEventArgs();
                                res.Completed += Network<T>.Send_Completed;
                                avaliableSendCompletion.Enqueue(res);                                
                            }
                            Interlocked.Add(ref currentSendCompletionPort, NewSendCompletionPortEveryBatch);
                        }
                    }
                    Network<T> net = req.Key;
                    arg.UserToken = req.Value;
                    net.BlockHandled(req.Value);
                    arg.SetBuffer(req.Value.Buffer, req.Value.StartIndex, req.Value.UsedLength);
                    try
                    {
                        if (!net.Socket.SendAsync(arg))
                        {
                            Network<T>.Send_Completed(null, arg);
                        }
                    }
                    catch
                    {
                        FinishSendQuest(arg);
                    }
                }
                sendRequestWaiter.WaitOne();
            }
        }

        /// <summary>
        /// Create a new Session instance
        /// </summary>
        /// <returns>new Session</returns>
        protected abstract Session<T> NewSession();

        public virtual void RemoveClient(Session<T> client)
        {
            lock (clients)
            {
                if (clients.Contains(client))
                    clients.Remove(client);
            }
        }

        /// <summary>
        /// Registered packet processing class
        /// </summary>
        /// <param name="opcode">Opcode</param>
        /// <param name="packetHandler">Corresponding processing</param>
        protected void RegisterPacketHandler(T opcode, Packet<T> packetHandler)
        {
            if (!commandTable.ContainsKey(opcode))
                commandTable.Add(opcode, packetHandler);
            else
                Logger.ShowWarning(string.Format("{0} already registered", opcode));
        }

        void NetworkLoop()
        {
            while (true)
            {
                try
                {
                    // let new clients (max 10) connect
                    if (isUp)
                    {
                        for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
                        {
                            CreateNewSession(listener);
                        }
                    }
                    System.Threading.Thread.Sleep(1);
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }            
        }

        void CreateNewSession(TcpListener listener)
        {
            Socket sock = listener.AcceptSocket();
            sock.NoDelay = true;
            string ip = sock.RemoteEndPoint.ToString().Substring(0, sock.RemoteEndPoint.ToString().IndexOf(':'));
            Logger.ShowInfo(string.Format("New Client:{0}", sock.RemoteEndPoint.ToString()));
            Session<T> client = NewSession();
            client.netIO = Network<T>.Implementation.CreateNewInstance(sock, this.commandTable, client);
            client.clientManager = this;
            client.netIO.Encrypt = encrypt;
            client.netIO.AutoLock = autoLock;
            client.netIO.SetMode(Mode.Server);
            client.netIO.OnConnect();
            if (!client.netIO.Disconnected)
            {
                lock (clients)
                    clients.Add(client);
            }
        }
    }
}
