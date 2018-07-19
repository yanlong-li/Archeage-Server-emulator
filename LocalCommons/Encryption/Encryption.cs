using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace LocalCommons.Encryption
{
    /// <summary>
    /// Provides functionality for encrypting primitive binary data.
    /// Author: взято из разных источников
    /// </summary>
    public class Encryption
    {
        ///--------
        //пакет до ксора, смотреть trace 11 и trace12
        //len=0x23(35)

        //CPU Dump
        //Address Hex dump ASCII(OEM - США)
        //           hash count type
        //             vv    vv vvvvv
        //               CRC
        //                vv
        //0016FC20  00 05 B4 9F|3E 01 01 00|00 00 00 00|00 00 00 00|  +?>
        //0016FC30  00 00 00 03|00 6E 6F 70|00 00 00 00|00 00 00 00|     nop
        //0016FC40  00 00 00 00|

        //0x9F, 0x3E, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x6E, 0x6F, 0x70

        //пакет после подсчета контрольной суммы пакета, считаются байты(9F3E010100000000000000000000000003006E6F70)
        //начиная с адреса 0016FC23 len = 15(21), нулевые байты не считают?
        //CPU Dump
        //Address   Hex dump                                         ASCII(OEM - США)
        //                vv
        //0016FC20  00 05 73 9F|3E 01 01 00|00 00 00 00|00 00 00 00|  +?>
        //0016FC30  00 00 00 03|00 6E 6F 70|00 00 00 00|00 00 00 00|     nop
        //0016FC40  00 00 00 00|

        /*13.07.2018 - подсчитывает правильно - D4
        CPU Dump
        Address   Hex dump                                         ASCII (OEM - США)
                     vv-начинаем отсюда
        444CFCD8  D4 01 55 00|01 00 01 08|00 78 32 75|69 2F 68 75| ╘U   x2ui/hu
        444CFCE8  64 00                                            d

        // подсчет контрольной суммы в EAX, по выходу используется только AL
        int __usercall CRC8_B8EF@<eax>(int crc@<eax>, int data@<ecx>, int size@<ebp>)
        {
            *(_DWORD *)(size + 12);
            return *(unsigned __int8 *)(data + *(_DWORD *)(size + 8)) + 19 * crc;
        }
        */
        /// <summary>
        /// Подсчет контрольной суммы пакета, используется в шифровании пакетов DD05
        /// </summary>
        public static byte _CRC8_(byte[] data, int size)
        {
            int len = size;
            UInt32 checksum = 0;
            for (int i = 0; i <= len - 1; i++)
            {
                checksum = checksum * 0x13;
                checksum += data[i];
            }
            return (byte)(checksum);
        }
        public static byte _CRC8_(byte[] data)
        {
            int len = data.Length;
            UInt32 checksum = 0;

            for (int i = 0; i <= len - 1; i++)
            {
                checksum = checksum * 0x13;
                checksum += data[i];
            }
            return (byte)(checksum);
        }
        //=====================================================================================
        /// <summary>
        /// вспомогательная подпрограмма для encode/decode серверных пакетов
        /// </summary>
        /// <param name="cry"></param>
        /// <returns></returns>
        private static byte Inline(ref uint cry)
        {
            cry += 0x2FCBD5U; //3132373
            byte n = (byte)(cry >> 0x10);
            n = (byte)(n & 0x0F7);
            return (byte)(n == 0 ? 0x0FE : n);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// подпрограмма для encode/decode серверных пакетов, правильно шифрует и расшифровывает серверные пакеты DD05 для версии 3.0.0.7
        /// </summary>
        /// <param name="bodyPacket">адрес начиная с DD</param>
        /// <returns>возвращает адрес на подготовленные данные</returns>
        public static byte[] StoCEncrypt(byte[] bodyPacket)
        {
            byte[] array = new byte[bodyPacket.Length];
            uint cry = (uint)(bodyPacket.Length ^ 0x1F2175A0); //522286496
            int n = 4 * (bodyPacket.Length / 4);
            for (int i = n - 1; i >= 0; i--)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            for (int i = n; i < bodyPacket.Length; i++)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            return array;
        }
        //=====================================================================================
        ///шифрует и расшифровывает клиентские пакеты 0005 - не подтерждено
        ///

        public static byte[] CtoSDecrypt(byte[] bodyPacket, uint unkKey)
        {
            byte[] array = new byte[bodyPacket.Length];
            uint cry = ((uint)(unkKey + (ulong)bodyPacket.Length) * unkKey) ^ 0x75A02453u; // 0x75A01F21u;
            int n = 4 * (bodyPacket.Length / 4);
            for (int i = n - 1; i >= 0; i--)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            for (int i = n; i < bodyPacket.Length; i++)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            return array;
        }

        // --------------------------------------------------------------------------------------------------------
        //--------------------------- непроверенные !!! --------------------------
        // --------------------------------------------------------------------------------------------------------
        //public static uint CheckSum(ushort[] data, uint size)
        //{
        //    ushort[] m_data = new ushort[5];
        //    uint m_size = size;
        //    uint v4 = 0;
        //    uint v5 = 0;
        //    uint v6 = 0;
        //    if ((m_size / 2) < 2)
        //    {
        //        m_data = data;
        //    }
        //    else
        //    {
        //        uint len = ((uint)(m_size - 4) >> 2) + 1;
        //        m_size = size - 4 * len;
        //        m_data = data;
        //        do
        //        {
        //            v4 += m_data;
        //            v5 += m_data[1];
        //            m_data += 2;
        //            len--;
        //        }
        //        while (Len);
        //    }
        //    if (m_size > 1)
        //    {
        //        v6 = m_data;
        //        m_data++;
        //        m_size -= 2;
        //    }
        //    ushort crc = v4 + v5 + v6;
        //    if (m_size > 0)
        //        crc += (byte)m_data;
        //    return ~(crc + (crc >> 16) + ((crc + (crc >> 16)) >> 16));
        //}

        /*
         * ///из xlcommon.dll
        unsigned int __stdcall XlPing::CheckSum(unsigned __int16 * data, int Size)
        {
            int m_Size; // esi
            int v4; // edi
            int v5; // ebx
            int v6; // ebp
            unsigned int Len; // ecx
            unsigned __int16 *m_data; // eax
            unsigned int crc; // ebp

            m_Size = Size;
            v4 = 0;
            v5 = 0;
            v6 = 0;
            if (Size / 2 < 2 )
            {
                m_data = data;
            }
            else
            {
                Len = ((unsigned int)(Size - 4) >> 2) + 1;
                m_Size = Size - 4 * Len;
                m_data = data;
                do
                {
                    v4 += * m_data;
                    v5 += m_data[1];
                    m_data += 2;
                    --Len;
                }
                while (Len );
            }
            if (m_Size > 1 )
            {
                v6 = * m_data;
                ++m_data;
                m_Size -= 2;
            }
            crc = v4 + v5 + v6;
            if (m_Size > 0 )
            crc += * (unsigned __int8 *)m_data;
            return ~((unsigned __int16) crc + (crc >> 16) + (((unsigned __int16) crc + (crc >> 16)) >> 16));
          }
        */

        public static byte Crc8_(byte[] data, int size)
        {
            byte checksum = 0;
            for (int i = 0; i <= size - 1; i++)
                checksum += data[i];

            return (byte)(-checksum);
        }

        ///
        /// This enum is used to indicate what kind of checksum you will be calculating.
        /// 
        /// образующий многочлен
        public enum CRC8_POLY
        {
            ///для контроля для сообщения "123456789"  = byte[] {0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39}
            CRC8_CCITT = 0x07,       // 0xFE
            CRC8_SAE_J1850 = 0x1D,   // 0x37
            CRC8_DALLAS_MAXIM = 0x31,// 0xA2
            CRC8 = 0xd5,             // 0xBC
            CRC_8_WCDMA = 0x9b       // 0xEA
        };

        /// 
        /// Class for calculating CRC8 checksums...
        /// начальное содержимое = 00
        /// конечное XOR значение = 00
        /// реверс байтов сообщения - нет
        /// реверс CRC перед финальным XOR - нет
        /// 
        /// 

        public class CRC8Calc
        {
            private byte[] table = new byte[256];

            public byte Checksum(params byte[] val)
            {
                if (val == null)
                    throw new ArgumentNullException("val");

                byte c = 0;

                foreach (byte b in val)
                {
                    c = table[c ^ b];
                }

                return c;
            }

            public byte[] Table
            {
                get
                {
                    return this.table;
                }
                set
                {
                    this.table = value;
                }
            }

            public byte[] GenerateTable(CRC8_POLY polynomial)
            {
                byte[] csTable = new byte[256];

                for (int i = 0; i < 256; ++i)
                {
                    int curr = i;

                    for (int j = 0; j < 8; ++j)
                    {
                        if ((curr & 0x80) != 0)
                        {
                            curr = (curr << 1) ^ (int)polynomial;
                        }
                        else
                        {
                            curr <<= 1;
                        }
                    }

                    csTable[i] = (byte)curr;
                }

                return csTable;
            }

            public CRC8Calc(CRC8_POLY polynomial)
            {
                this.table = this.GenerateTable(polynomial);
            }
        }

        public static class CRC_8
        {
            static readonly byte[] CRC8_TABLE = new byte[]
            {
                0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
                157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
                35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
                190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
                70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
                219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
                101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
                248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
                140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
                17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
                175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
                50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
                202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
                87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
                233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
                116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53
            };

            public static byte Calculate(byte[] data, byte init = 0)
            {
                byte result = init;
                for (var i = 0; i < data.Length; i++)
                {
                    result = CRC8_TABLE[result ^ data[i]];
                }
                return result;
            }
        }

        //
        //byte[] b = { 1, 2, 3, 4, 5, 6, 7, 8 };
        //Console.WriteLine(CS8(b));
        //
        static byte RotLeft(byte val, byte lShift, byte rShift)
        {
            return (byte)((val << lShift) | (val >> rShift));
        }

        public static byte CRC8(byte[] buffer, int Size)
        {
            byte bits = 8;
            byte lShift = 2;
            byte rShift = (byte)(bits - lShift);
            byte res = 0;
            byte index = 0;
            //int count = buffer.Length;
            int count = Size;

            while (count-- > 0)
                res = (byte)(RotLeft(res, lShift, rShift) ^ buffer[index++]);

            return RotLeft(res, lShift, rShift);
        }

        ////Here's a sample of how I was using it (and testing it):
        //byte crc = Crc8.ComputeChecksum(1, 2, 3);
        //byte check = Crc8.ComputeChecksum(1, 2, 3, crc);
        ////here check should equal 0 to show that the checksum is accurate
        //if (check != 0 )
        //{
        //Console.WriteLine( "Error in the checksum" );
        //}
        public static class Crc8
        {
            static byte[] table = new byte[256];
            // x8 + x7 + x6 + x4 + x2 + 1
            const byte poly = 0xd5;

            public static byte ComputeChecksum(params byte[] bytes)
            {
                byte crc = 0;
                if (bytes != null && bytes.Length > 0)
                {
                    foreach (byte b in bytes)
                    {
                        crc = table[crc ^ b];
                    }
                }
                return crc;
            }

            static Crc8()
            {
                for (int i = 0; i < 256; ++i)
                {
                    int temp = i;
                    for (int j = 0; j < 8; ++j)
                    {
                        if ((temp & 0x80) != 0)
                        {
                            temp = (temp << 1) ^ poly;
                        }
                        else
                        {
                            temp <<= 1;
                        }
                    }
                    table[i] = (byte)temp;
                }
            }
        }

        ///эта процедура очень похожа на xlcommon:33021390 ?GetCRC32@Crc32Gen@@QBEIPBD@Z
        ///; unsigned int __thiscall Crc32Gen::GetCRC32(Crc32Gen *this, const char *)
        public static class Crc32
        {
            static uint[] table;

            public static uint ComputeChecksum(byte[] bytes)
            {
                uint crc = 0xffffffff;
                for (int i = 0; i < bytes.Length; ++i)
                {
                    byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
                    crc = (uint)((crc >> 8) ^ table[index]);
                }
                return ~crc;
            }
            public static byte[] ComputeChecksumBytes(byte[] bytes)
            {
                return BitConverter.GetBytes(ComputeChecksum(bytes));
            }
            static Crc32()
            {
                uint poly = 0xedb88320;
                table = new uint[256];
                uint temp = 0;
                for (uint i = 0; i < table.Length; ++i)
                {
                    temp = i;
                    for (int j = 8; j > 0; --j)
                    {
                        if ((temp & 1) == 1)
                        {
                            temp = (uint)((temp >> 1) ^ poly);
                        }
                        else
                        {
                            temp >>= 1;
                        }
                    }
                    table[i] = temp;
                }
            }
        }

        // --------------------------------------------------------------------------------------------------------

        /*
         Sample Code: Compressing ViewState with Deflate
         As usual, we intercept the handling of ViewState by overriding two public virtual method:

         Hide   Copy Code
         public class BasePage : System.Web.UI.Page
{
    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        LosFormatter formatter = new LosFormatter();
        StringWriter writer = new StringWriter();
        formatter.Serialize(writer, viewState);
        string viewStateString = writer.ToString();
        byte[] bytes = Convert.FromBase64String(viewStateString);
        bytes = Compress(bytes);
        ClientScript.RegisterHiddenField("__VIEWSTATE__", Convert.ToBase64String(bytes));
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        string viewState = Request.Form["__VIEWSTATE__"];
        byte[] bytes = Convert.FromBase64String(viewState);
        bytes = Decompress(bytes);
        LosFormatter formatter = new LosFormatter();
        return formatter.Deserialize(Convert.ToBase64String(bytes));
    }
}
        */
        public static byte[] Compress(byte[] data)
        {
            var output = new MemoryStream();
            using (var dstream = new DeflateStream(output, CompressionMode.Compress))
            {
                dstream.Write(data, 0, data.Length);
                dstream.Close();
            }

            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            var input = new MemoryStream(data);
            var output = new MemoryStream();
            using (var dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
                dstream.Close();
            }

            return output.ToArray();
        }
        /* CRC32 Adler32
public class Adler32Computer
{
    private int a = 1;
    private int b = 0;

    public int Checksum
    {
        get
        {
            return ((b * 65536) + a);
        }
    }

    private static readonly int Modulus = 65521;

    public void Update(byte[] data, int offset, int length)
    {
        for (int counter = 0; counter < length; ++counter)
        {
            a = (a + (data[offset + counter])) % Modulus;
            b = (b + a) % Modulus;
        }
    }
}
*/
        /*
           The following C code computes the Adler-32 checksum of a data buffer.
           It is written for clarity, not for speed.  The sample code is in the
           ANSI C programming language. Non C users may find it easier to read
           with these hints:

              &      Bitwise AND operator.
              >>     Bitwise right shift operator. When applied to an
                     unsigned quantity, as here, right shift inserts zero bit(s)
                     at the left.
              <<     Bitwise left shift operator. Left shift inserts zero
                     bit(s) at the right.
              ++     "n++" increments the variable n.
              %      modulo operator: a % b is the remainder of a divided by b.

              #define BASE 65521 /* largest prime smaller than 65536 */

        /*
           Update a running Adler-32 checksum with the bytes buf[0..len-1]
         and return the updated checksum. The Adler-32 checksum should be
         initialized to 1.

         Usage example:

           unsigned long adler = 1L;

           while (read_buffer(buffer, length) != EOF) {
             adler = update_adler32(adler, buffer, length);
           }
           if (adler != original_adler) error();
        */
        /*        unsigned long update_adler32(unsigned long adler,
                   unsigned char* buf, int len)
                {
                    unsigned long s1 = adler & 0xffff;
                    unsigned long s2 = (adler >> 16) & 0xffff;
                    int n;

                    for (n = 0; n < len; n++)
                    {
                        s1 = (s1 + buf[n]) % BASE;
                        s2 = (s2 + s1) % BASE;
                    }
                    return (s2 << 16) + s1;
                }
        */
        /* Return the adler32 of the bytes buf[0..len-1] */
        /*
        unsigned long adler32(unsigned char* buf, int len)
        {
            return update_adler32(1L, buf, len);
        }
        */
        /*
        1)
        public static void Main()
        {

            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            Compress(directorySelected);


            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.cmp"))
            {
                Decompress(fileToDecompress);
            }
        }
        */
        // --------------------------------------------------------------------------------------------------------
        private static readonly string DirectoryPath = @"c:\temp";
        // --------------------------------------------------------------------------------------------------------

        public static void Compress(DirectoryInfo directorySelected)
        {
            foreach (var file in directorySelected.GetFiles("*.xml"))
                using (var originalFileStream = file.OpenRead())
                {
                    if (((File.GetAttributes(file.FullName) & FileAttributes.Hidden)
                         != FileAttributes.Hidden) & (file.Extension != ".cmp"))
                    {
                        using (var compressedFileStream = File.Create(file.FullName + ".cmp"))
                        {
                            using (var compressionStream =
                                new DeflateStream(compressedFileStream, CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }

                        var info = new FileInfo(DirectoryPath + "\\" + file.Name + ".cmp");
                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.", file.Name, file.Length, info.Length);
                    }
                }
        }

        public static void Decompress(FileInfo fileToDecompress)
        {
            using (var originalFileStream = fileToDecompress.OpenRead())
            {
                var currentFileName = fileToDecompress.FullName;
                var newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (var decompressedFileStream = File.Create(newFileName))
                {
                    using (var decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }

        /*

        2)
        public static void Main()
        {
            // Path to directory of files to compress and decompress.
            string dirpath = @"c:\users\public\reports";

            DirectoryInfo di = new DirectoryInfo(dirpath);

            // Compress the directory's files.
            foreach (FileInfo fi in di.GetFiles())
            {
                Compress(fi);
            }

            // Decompress all *.cmp files in the directory.
            foreach (FileInfo fi in di.GetFiles("*.cmp"))
            {
                Decompress(fi);
            }


        }
        */

        public static void Compress2(FileInfo fi)
        {
            // Get the stream of the source file.
            using (var inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and already compressed files.
                if (((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden) &
                    (fi.Extension != ".cmp"))
                    using (var outFile = File.Create(fi.FullName + ".cmp"))
                    {
                        using (var compress = new DeflateStream(outFile, CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(compress);

                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.", fi.Name, fi.Length,
                                outFile.Length);
                        }
                    }
            }
        }

        public static void Decompress2(FileInfo fi)
        {
            // Get the stream of the source file.
            using (var inFile = fi.OpenRead())
            {
                // Get original file extension, 
                // for example "doc" from report.doc.cmp.
                var curFile = fi.FullName;
                var origName = curFile.Remove(curFile.Length - fi.Extension.Length);

                //Create the decompressed file.
                using (var outFile = File.Create(origName))
                {
                    using (var decompress = new DeflateStream(inFile, CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fi.Name);
                    }
                }
            }
        }

        // ---------------------------------------------------------------------------------------------------------                    

        /*
    public static void TestCompression()
    {
        byte[] test = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        byte[] result = DecompressString(CompressString(test));
        // This will fail, result.Length is 0
        Debug.Assert(result.Length == test.Length);
    }
     */

        /// <summary>
        ///     Сжатие (архивирование) строки. Функция возвращает запакованную версию строки в виде байтового массива.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] CompressString(string value)
        {
            var byteArray = new byte[0];
            if (!string.IsNullOrEmpty(value))
            {
                byteArray = Encoding.UTF8.GetBytes(value);
                using (var stream = new MemoryStream())
                {
                    using (var zip = new GZipStream(stream, CompressionMode.Compress))
                    {
                        zip.Write(byteArray, 0, byteArray.Length);
                    }

                    byteArray = stream.ToArray();
                }
            }

            return byteArray;
        }

        /// <summary>
        ///     Сжатие (архивирование) строки байт. Функция возвращает запакованную версию строки в виде байтового массива.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] CompressString(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
        }

        /// <summary>
        ///     Сжатие (архивирование) строки. Функция возвращает запакованную версию строки в виде строки.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="mode"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GZipCompress(string s, CompressionMode mode, Encoding encoding)
        {
            if (mode == CompressionMode.Compress)
                using (var outputStream = new MemoryStream())
                {
                    using (var compressionStream = new GZipStream(outputStream, CompressionMode.Compress))
                    using (var inputStream = new MemoryStream(encoding.GetBytes(s)))
                    {
                        inputStream.CopyTo(compressionStream);
                    }

                    return Convert.ToBase64String(outputStream.ToArray());
                }
            else
                using (var outputStream = new MemoryStream())
                {
                    using (var inputStream = new MemoryStream(Convert.FromBase64String(s)))
                    using (var compressionStream = new GZipStream(inputStream, mode))
                    {
                        compressionStream.CopyTo(outputStream);
                    }

                    return encoding.GetString(outputStream.ToArray());
                }
        }

        /*        Пример использования:
                static void Main()
                {
                    var encoding = new UTF8Encoding();

                    string sourceText = "“ ... ”";
                    string compressedText = sourceText.GZipCompress(CompressionMode.Compress, encoding);
                    string decompressedText = compressedText.GZipCompress(CompressionMode.Decompress, encoding);
                }

                public static PacketWriter CreateInstance()
                {
                    return CreateInstance(32, false);
                }
        */

        //======================================================================================================
        //Добавил функции для работы со сжатием строк
        /// <summary>
        ///     Распаковка(разархивирование) сжатой строки.На входе — данные, предварительно сжатые предыдущей функцией
        ///     CompressString.
        ///     На выходе — распакованная строка.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecompressString(byte[] value)
        {
            var resultString = string.Empty;
            if (value != null && value.Length > 0)
                using (var stream = new MemoryStream(value))
                using (var zip = new GZipStream(stream, CompressionMode.Decompress))
                using (var reader = new StreamReader(zip))
                {
                    resultString = reader.ReadToEnd();
                }

            return resultString;
        }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DecompressString2(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                var buffer = new byte[4096];
                int read;
                while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0) resultStream.Write(buffer, 0, read);
                return resultStream.ToArray();
            }
        }
        //======================================================================================================

        /* 
         * Пример вызова
            // This code example produces the following output:
            //
            // The MD5 hash of Hello World! is: ed076287532e86365e841e92bfc50d8c.
            // Verifying the hash...
            // The hashes are the same.            
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);
                Console.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");
                Console.WriteLine("Verifying the hash...");
                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
            }
        */
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);
            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

