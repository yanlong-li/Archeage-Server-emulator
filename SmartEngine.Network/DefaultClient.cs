using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using SmartEngine.Core;
namespace SmartEngine.Network
{
    /// <summary>
    /// Default client for connection
    /// </summary>
    /// <typeparam name="T">Packet Opcode Enumeration</typeparam>
    public class DefaultClient<T> : Session<T>
    {
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        int port;
        bool encrypt = true, autoLock = false;
        Dictionary<T, Packet<T>> commandTable = new Dictionary<T, Packet<T>>();

        /// <summary>
        /// Server Host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The port of the server
        /// </summary>
        public int Port { get { return port; } set { this.port = value; } }

        /// <summary>
        /// Whether the packet will be encrypted, the default is True
        /// </summary>
        public bool Encrypt { get { return encrypt; } set { this.encrypt = value; } }

        /// <summary>
        /// Whether to automatically synchronize the lock when processing the packet. The default value is false
        /// </summary>
        public bool AutoLock { get { return autoLock; } set { this.autoLock = value; } }

        /// <summary>
        /// Try to connect to the server
        /// </summary>
        /// <param name="times">number of retries</param>
        /// <returns>whether succeed</returns>
        public bool Connect(int times)
        {
            bool Connected = false;
            do
            {
                if (times < 0)
                {
                    return false;
                }
                try
                {
                    sock.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(Host), port));
                    Connected = true;
                }
                catch (Exception e)
                {
                    Logger.ShowError("Failed... Trying again in 5sec");
                    Logger.ShowError(e.ToString());
                    System.Threading.Thread.Sleep(5000);
                    Connected = false;
                }
                times--;
            } while (!Connected);

            try
            {
                this.netIO = Network<T>.Implementation.CreateNewInstance(sock, this.commandTable, this);
                this.netIO.Encrypt = encrypt;
                this.netIO.AutoLock = autoLock;
                this.netIO.SetMode(Mode.Client);
                SendInitialPacket();
            }
            catch (Exception ex)
            {
                Logger.ShowWarning(ex.StackTrace);
            }
            return true;
        }

        public override void OnDisconnect()
        {
            base.OnDisconnect();
            ClientManager<T>.StopSendReceiveThreads();
        }

        /// <summary>
        /// Registered packet processing class
        /// </summary>
        /// <param name="opcode">Opcode</param>
        /// <param name="packetHandler">Corresponding processing
        /// </param>
        protected void RegisterPacketHandler(T opcode, Packet<T> packetHandler)
        {
            if (!commandTable.ContainsKey(opcode))
                commandTable.Add(opcode, packetHandler);
            else
                Logger.ShowWarning(string.Format("{0} already registered", opcode));
        }

        /// <summary>
        /// Send initial packet (first packet), if not return directly after overload
        /// </summary>
        protected virtual void SendInitialPacket()
        {
            Packet<T> p = new Packet<T>(8);
            p.data[7] = 0x10;
            this.netIO.SendPacket(p, true);     
        }
    }
}
