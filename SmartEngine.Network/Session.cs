using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SmartEngine.Network
{
    /// <summary>
    /// Session class, each established connection will have its own session instance
    /// </summary>
    /// <typeparam name="T">Packet Opcode Enumeration</typeparam>
    public class Session<T>
    {
        internal Network<T> netIO;
        internal ClientManager<T> clientManager;

        /// <summary>
        /// Is the connection already connected
        /// </summary>
        public bool Connected
        {
            get;
            set;
        }
        /// <summary>
        /// The ClientManager that this session belongs to
        /// </summary>
        public ClientManager<T> ClientManager { get { return clientManager; } }

        /// <summary>
        /// The network layer of this session
        /// </summary>
        public Network<T> Network { get { return netIO; } }

        public virtual void OnConnect()
        {

        }

        public virtual void OnDisconnect() { }

    }
}
