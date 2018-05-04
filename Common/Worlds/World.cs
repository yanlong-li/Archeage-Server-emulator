using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Worlds
{
    public class World
    {
        byte[] ip = new byte[4];
        public uint ID { get; set; }
        public string Name { get; set; }
        public string IP
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", ip[0], ip[1], ip[2], ip[3]);
            }
            set
            {
                string[] sp = value.Split('.');
                ip[0] = byte.Parse(sp[0]);
                ip[1] = byte.Parse(sp[1]);
                ip[2] = byte.Parse(sp[2]);
                ip[3] = byte.Parse(sp[3]);
            }
        }
        public byte[] IPAsArray
        {
            get { return ip; }
            set { ip = value; }
        }
        public ushort Port { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayerCount { get; set; }
    }
}
