using System;

// easypcap by Tim Pinkawa
// http://www.timpinkawa.net/misc/easypcap.html

namespace ArcheAge_Packet_Builder
{
    public class PcapPacket
    {
        private int secs;
        private int usecs;
        private byte[] data;

        public PcapPacket(int secs, int usecs, byte[] data)
        {
            this.secs = secs;
            this.usecs = usecs;
            this.data = data;
        }

        public int Seconds
        {
            get
            {
                return secs;
            }
        }

        public int Microseconds
        {
            get
            {
                return usecs;
            }
        }        

        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}: {2} bytes of data", secs, usecs, data.Length);
        }
    }
}
