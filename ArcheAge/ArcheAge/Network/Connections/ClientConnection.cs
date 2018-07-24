using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Logging;
using LocalCommons.Network;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ArcheAge.ArcheAge.Net.Connections
{

    /// <summary>
    /// Connection That Used For ArcheAge Client( Game Side )
    /// </summary>
    public class ClientConnection : IConnection
    {
        //----- Static
        private static Dictionary<int, Account> m_CurrentAccounts = new Dictionary<int, Account>();
        private byte m_Random;
        public static Dictionary<int, Account> CurrentAccounts
        {
            get { return m_CurrentAccounts; }
        }

        //----- Static

        private Account m_CurrentAccount;
        
        public Account CurrentAccount
        {
            get { return m_CurrentAccount; }
            set { m_CurrentAccount = value; }
        }

        public ClientConnection(Socket socket) : base(socket) {
            DisconnectedEvent += ClientConnection_DisconnectedEvent;
            m_LittleEndian = true;
            Logger.Trace("Client IP: {0} connected", this);
        }

        public override void SendAsync(NetPacket packet)
        {
            packet.IsArcheAgePacket = true;
            base.SendAsync(packet);
        }
        public  void SendAsyncd(NetPacket packet)
        {
            packet.IsArcheAgePacket = false;
            base.SendAsync(packet);
        }
        void ClientConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            Dispose();
            Logger.Trace("Client IP: {0} disconnected", this);
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);

            //Logger.Trace("Allocated Memory = " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1000000) + " MB");

            //reader.Offset += 1; //Undefined Random Byte
            byte seq = reader.ReadByte();
            byte header = reader.ReadByte(); //Packet Level
            ushort opcode = reader.ReadLEUInt16(); //Packet Opcode

            if (header == 0x05)
            {
                reader.Offset -= 2; //вернемся к hash, count
                byte hash = reader.ReadByte(); //считываем hash или CRC (он не меняется)
                byte count = reader.ReadByte(); //считываем count (шифрован, меняется)
                if (hash == 0x34)
                {
                    opcode = 0x0088; //пакет на релогин
                }
                //reader.Offset -= 2; //Undefined Random Byte
            }

            if (!DelegateList.ClientHandlers.ContainsKey(header))
            {
                Logger.Trace("Received undefined seq {0} packet Level {1} - Opcode 0x{2:X2}", seq, header, opcode);
                return;
            }
            try { 
            PacketHandler<ClientConnection> handler = DelegateList.ClientHandlers[header][opcode];
                if (handler != null)
                    handler.OnReceive(this, reader);
                else
                    Logger.Trace("Received undefined seq {0} packet Level {1} - Opcode 0x{2:X2}", seq, header, opcode);
            }
            catch(Exception exp)
            {
                Logger.Trace("Received undefined seq {0} packet Level2 {1} - Opcode 0x{2:X2}", seq, header, opcode);
                throw exp;
            }
        }
    }
}
