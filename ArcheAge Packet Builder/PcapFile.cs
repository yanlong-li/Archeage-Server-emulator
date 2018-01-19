using System;
using System.Collections.Generic;
using System.IO;

// easypcap by Tim Pinkawa
// http://www.timpinkawa.net/misc/easypcap.html

namespace ArcheAge_Packet_Builder
{
    public class PcapFile : IDisposable, IEnumerable<PcapPacket>
    {
        private BinaryReader br;
        private Version version;
        private uint snaplen;
        private int thiszone;
        private uint sigfigs;
        private LinkType linktype;

        private long basePos;
        private bool byteSwap;

        private const uint TCPDUMP_MAGIC = 0xa1b2c3d4;
        private const uint TCPDUMP_MAGIC_ENDIAN = 0xd4c3b2a1;

        public PcapFile(string path)
            : this(new FileStream(path, FileMode.Open))
        {
        }

        public PcapFile(Stream s)
        {
            br = new BinaryReader(s);
            uint magic = br.ReadUInt32();
            if (magic == TCPDUMP_MAGIC)
                byteSwap = false;
            else if (magic == TCPDUMP_MAGIC_ENDIAN)
                byteSwap = true;
            else
                throw new Exception("File not recognized as valid pcap data");
            ushort major = br.ReadUInt16();
            ushort minor = br.ReadUInt16();

            thiszone = br.ReadInt32();
            sigfigs = br.ReadUInt32();
            snaplen = br.ReadUInt32();
            uint ltype = br.ReadUInt32();

            if (byteSwap)
            {
                major = ByteSwap.Swap(major);
                minor = ByteSwap.Swap(minor);
                thiszone = ByteSwap.Swap(thiszone);
                snaplen = ByteSwap.Swap(snaplen);
                ltype = ByteSwap.Swap(ltype);
            }

            version = new Version(major, minor);
            linktype = (LinkType)((int)ltype);
            basePos = br.BaseStream.Position;
        }

        public void Close()
        {
            br.Close();
        }

        public PcapPacket ReadPacket()
        {
            if (br.BaseStream.Position < br.BaseStream.Length)
            {
                int secs = br.ReadInt32();
                int usecs = br.ReadInt32();
                uint caplen = br.ReadUInt32();
                uint len = br.ReadUInt32();

                if (byteSwap)
                {
                    secs = ByteSwap.Swap(secs);
                    usecs = ByteSwap.Swap(usecs);
                    caplen = ByteSwap.Swap(caplen);
                    len = ByteSwap.Swap(len);
                }

                byte[] data = br.ReadBytes((int)caplen);

                return new PcapPacket(secs, usecs, data);
            }
            else
            {
                return null;
            }
        }

        public Version Version
        {
            get
            {
                return version;
            }
        }

        public uint MaximumCaptureLength
        {
            get
            {
                return snaplen;
            }
        }

        public int TimezoneOffset
        {
            get
            {
                return thiszone;
            }
        }

        public uint SignificantFigures
        {
            get
            {
                return sigfigs;
            }
        }


        public LinkType LinkType
        {
            get
            {
                return linktype;
            }
        }

        public void Rewind()
        {
            br.BaseStream.Position = basePos;
        }

        public override string ToString()
        {
            string endianness;

            if(BitConverter.IsLittleEndian)
            {
                if(byteSwap)
                    endianness = "Big";
                else
                    endianness = "Little";
            }
            else
            {
                if(byteSwap)
                    endianness = "Little";
                else
                    endianness = "Big";
            }

            return String.Format("{0}-endian {1} capture, pcap version {2}", endianness, linktype.ToString(), version.ToString());
        }

        #region IDisposable Members

        public void Dispose()
        {
            br.Close();
        }

        #endregion


        private class PcapPacketEnumerator : IEnumerator<PcapPacket>
        {
            private PcapFile file;
            private PcapPacket currentPacket = null;

            public PcapPacketEnumerator(PcapFile file)
            {
                this.file = file;
            }

            #region IEnumerator<PcapPacket> Members

            public PcapPacket Current
            {
                get { return currentPacket; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return currentPacket; }
            }

            public bool MoveNext()
            {
                currentPacket = file.ReadPacket();
                return currentPacket != null;
            }

            public void Reset()
            {
                file.Rewind();
            }

            #endregion
        }

        #region IEnumerable<PcapPacket> Members

        public IEnumerator<PcapPacket> GetEnumerator()
        {
            return new PcapPacketEnumerator(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new PcapPacketEnumerator(this);
        }

        #endregion
    }

    public enum LinkType
    {
    	Null,
	    Ethernet,
    	ExpEthernet,
	    AX25,
	    ProNet,
	    Chaos,
	    TokenRing,
	    ArcNet,
	    Slip,
	    Ppp,
	    Fddi
    }

    internal class ByteSwap
    {
        public static uint Swap(uint num)
        {
            return (uint)(((num & 0xff000000) >> 24) |
                 ((num & 0x00ff0000) >> 8) |
                 ((num & 0x0000ff00) << 8) |
                 ((num & 0x000000ff) << 24));
        }

        public static int Swap(int num)
        {
            uint temp = (uint)num;
            return (int)Swap(temp);
        }

        public static ushort Swap(ushort num)
        {
            return (ushort)(((num & 0xff00) >> 8) |
                 ((num & 0x00ff) << 8));
        }
    }
}
