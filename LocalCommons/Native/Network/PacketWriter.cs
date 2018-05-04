using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace LocalCommons.Native.Network
{
    /// <summary>
    /// Provides functionality for writing primitive binary data.
    /// Author: Raphail
    /// </summary>
    public class PacketWriter
    {
        private static Stack<PacketWriter> m_Pool = new Stack<PacketWriter>();
        private bool m_LittleEndian;

        // --------------------------------------------------------------------------------------------------------
        private static readonly string DirectoryPath = @"c:\temp";
        // --------------------------------------------------------------------------------------------------------

        private static byte Inline(ref uint cry)
        {
            cry += 3532013U;
            var n = (byte)(cry >> 16);
            return (byte)((int)n == 0 ? 254 : n);
        }

        public static byte[] CtoSDecrypt(byte[] bodyPacket, uint unkKey)
        {
            var array = new byte[bodyPacket.Length];
            var cry = ((uint)(unkKey + (ulong)bodyPacket.Length) * unkKey) ^ 1973428001u;
            var n = 4 * (bodyPacket.Length / 4);
            for (var i = n - 1; i >= 0; i--)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            for (var i = n; i < bodyPacket.Length; i++)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            return array;
        }

        public static byte[] StoCDecrypt(byte[] bodyPacket)
        {
            var array = new byte[bodyPacket.Length];
            var cry = (uint)(bodyPacket.Length ^ 522286496);
            var n = 4 * (bodyPacket.Length / 4);
            for (var i = n - 1; i >= 0; i--)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            for (var i = n; i < bodyPacket.Length; i++)
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            return array;
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
        public static PacketWriter CreateInstance()
        {
            return CreateInstance(32, false);
        }

        public static PacketWriter CreateInstance(int capacity, bool LittleEndian)
        {
            PacketWriter pw = null;
            lock (m_Pool)
            {
                if (m_Pool.Count > 0)
                {
                    pw = m_Pool.Pop();

                    if (pw != null)
                    {
                        pw.m_Capacity = capacity;
                        pw.m_Stream.SetLength(0);
                    }
                }
            }
            if (pw == null)
                pw = new PacketWriter(capacity);

            pw.m_LittleEndian = LittleEndian;
            return pw;

        }

        public static void ReleaseInstance(PacketWriter pw)
        {
            lock (m_Pool)
            {
                if (!m_Pool.Contains(pw))
                {
                    m_Pool.Push(pw);
                }
                else
                {
                    try
                    {
                        using (StreamWriter op = new StreamWriter("neterr.log"))
                        {
                            op.WriteLine("{0}\tInstance pool contains writer", DateTime.Now);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("net error");
                    }
                }
            }
        }

        /// <summary>
        /// Internal stream which holds the entire packet.
        /// </summary>

        private MemoryStream m_Stream;
        private int m_Capacity;

        /// <summary>
        /// Internal format buffer.
        /// </summary>

        private static byte[] m_Buffer = new byte[4];

        /// <summary>
        /// Instantiates a new PacketWriter instance with the default capacity of 4 bytes.
        /// </summary>

        public PacketWriter()
            : this(32)
        {

        }

        /// <summary>
        /// Instantiates a new PacketWriter instance with a given capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity for the internal stream.</param>

        public PacketWriter(int capacity)
        {
            m_Stream = new MemoryStream(capacity);
            m_Capacity = capacity;
        }

        /// <summary>
        /// Writes a 1-byte boolean value to the underlying stream. False is represented by 0, true by 1.
        /// </summary>
        public void Write(bool value)
        {
            m_Stream.WriteByte((byte)(value ? 1 : 0));
        }

        /// <summary>
        /// Writes a 1-byte unsigned integer value to the underlying stream.
        /// </summary>
        public void Write(byte value)
        {
            m_Stream.WriteByte(value);
        }

        /// <summary>
        /// Writes a 1-byte signed integer value to the underlying stream.
        /// </summary>
        public void Write(sbyte value)
        {
            m_Stream.WriteByte((byte)value);
        }

        /// <summary>
        /// Writes a 2-byte signed integer value to the underlying stream.
        /// </summary>
        public void Write(short value)
        {
            if (m_LittleEndian)
            {
                m_Buffer[1] = (byte)(value >> 8);
                m_Buffer[0] = (byte)value;
            }
            else
            {
                m_Buffer[0] = (byte)(value >> 8);
                m_Buffer[1] = (byte)value;
            }
            m_Stream.Write(m_Buffer, 0, 2);
        }

        /// <summary>
        /// Writes a 2-byte unsigned integer value to the underlying stream.
        /// </summary>
        public void Write(ushort value)
        {
            if (m_LittleEndian)
            {
                m_Buffer[1] = (byte)(value >> 8);
                m_Buffer[0] = (byte)value;
            }
            else
            {
                m_Buffer[0] = (byte)(value >> 8);
                m_Buffer[1] = (byte)value;
            }
            m_Stream.Write(m_Buffer, 0, 2);
        }
        public void Writec(float value,Boolean c)
        {
            if (c) { };
            byte[] data = BitConverter.GetBytes(value);
            m_Stream.Write(data, 0, data.Length);
            if (!m_LittleEndian)
                Array.Reverse(data);
        }


        /// <summary>
        /// Writes a 4-byte signed integer value to the underlying stream.
        /// </summary>

        public void Write(int value)
        {

            if (m_LittleEndian)
            {
                m_Buffer[3] = (byte)(value >> 24);
                m_Buffer[2] = (byte)(value >> 16);
                m_Buffer[1] = (byte)(value >> 8);
                m_Buffer[0] = (byte)value;
            }
            else
            {
                m_Buffer[0] = (byte)(value >> 24);
                m_Buffer[1] = (byte)(value >> 16);
                m_Buffer[2] = (byte)(value >> 8);
                m_Buffer[3] = (byte)value;
            }

            m_Stream.Write(m_Buffer, 0, 4);

        }


        public void Write(float value)
        {
            byte[] data = BitConverter.GetBytes(value);
            if (!m_LittleEndian)
                Array.Reverse(data);
            m_Stream.Write(data, 0, 4);
        }

        public void Write(long value)
        {
            byte[] data = BitConverter.GetBytes(value);
            m_Stream.Write(data, 0, 8);
            if (!m_LittleEndian)
                Array.Reverse(data);
        }


        /// <summary>
        /// Writes a 4-byte unsigned integer value to the underlying stream.
        /// </summary>
        public void Write(uint value)
        {

            if (m_LittleEndian)
            {
                m_Buffer[3] = (byte)(value >> 24);
                m_Buffer[2] = (byte)(value >> 16);
                m_Buffer[1] = (byte)(value >> 8);
                m_Buffer[0] = (byte)value;
            }
            else
            {
                m_Buffer[0] = (byte)(value >> 24);
                m_Buffer[1] = (byte)(value >> 16);
                m_Buffer[2] = (byte)(value >> 8);
                m_Buffer[3] = (byte)value;
            }

            m_Stream.Write(m_Buffer, 0, 4);

        }

        /// <summary>
        /// Writes a sequence of bytes to the underlying stream
        /// </summary>

        public void Write(byte[] buffer, int offset, int size)
        {
            m_Stream.Write(buffer, offset, size);
        }


        /// <summary>
        /// Write ASCII Fixed No Size
        /// </summary>
        public void WriteASCIIFixedNoSize(string value, int size)
        {

            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteAsciiFixed() with null value");
                value = String.Empty;
            }

            int length = value.Length;
            //Write((short)length);
            m_Stream.SetLength(m_Stream.Length + size);

            if (length >= size)
                m_Stream.Position += Encoding.ASCII.GetBytes(value, 0, size, m_Stream.GetBuffer(), (int)m_Stream.Position);

            else
            {
                Encoding.ASCII.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
                m_Stream.Position += size;
            }
        }

        /// <summary>
        /// Writes a fixed-length ASCII-encoded string value to the underlying stream. To fit (size), the string content is either truncated or padded with null characters.
        /// </summary>

        public void WriteASCIIFixed(string value, int size)
        {

            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteAsciiFixed() with null value");
                value = String.Empty;
            }

            int length = value.Length;
            Write((short)length);
            m_Stream.SetLength(m_Stream.Length + size);

            if (length >= size)
                m_Stream.Position += Encoding.ASCII.GetBytes(value, 0, size, m_Stream.GetBuffer(), (int)m_Stream.Position);
            
            else
            {
                Encoding.ASCII.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
                m_Stream.Position += size;
            }
        }
        public void WriteUTF8Fixed(string value, int size)
        {
            
            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteAsciiFixed() with null value");
                value = String.Empty;
            }

            int length = value.Length;
            Write((short)size);
            m_Stream.SetLength(m_Stream.Length + size);

            if (length >= size)
                m_Stream.Position += UTF8Encoding.UTF8.GetBytes(value, 0, size, m_Stream.GetBuffer(), (int)m_Stream.Position);

            else
            {
 
                UTF8Encoding.UTF8.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
                m_Stream.Position += length*3;
            }
        }
        public void WriteHex(string value)
        {
            if (value.Length %2 != 0) {
                Console.Write("HexРґИлК§°ЬЈ¬ЧЦЅЪІ»ОЄХыКэ");
                return;
            }

            int length = value.Length / 2;
            //Write((short)length);
            m_Stream.SetLength(m_Stream.Length + length);

            byte[] bytes = new byte[value.Length / 2];

                       for (int i = 0; i < bytes.Length; i++)
            {
                
                    // ГїБЅёцЧЦ·ыКЗТ»ёц byteЎЈ 
                    bytes[i] = byte.Parse(value.Substring(i * 2, 2),
                                             System.Globalization.NumberStyles.HexNumber);
                
            }
            bytes.CopyTo(m_Stream.GetBuffer(),0);


               // UTF8Encoding.UTF8.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
                //m_Stream.Position += length ;
            
        }
        /// <summary>
        /// Writes a dynamic-length ASCII-encoded string value to the underlying stream, followed by a 1-byte null character.
        /// РґТ»ёц¶ЇМ¬і¤¶ИµДASCII±аВлµДЧЦ·ыґ®Цµ»щґЎБчЈ¬ЖдґОКЗТ»ёцЧЦЅЪµДїХЧЦ·ыЎЈ
        /// </summary>
        public void WriteDynamicASCII(string value)
        {
            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteAsciiNull() with null value");
                value = String.Empty;
            }
            int length = value.Length;
            m_Stream.SetLength(m_Stream.Length + length + 1);
            Encoding.ASCII.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
            m_Stream.Position += length + 1;
        }



        /// <summary>
        /// Writes a dynamic-length little-endian unicode string value to the underlying stream, followed by a 2-byte null character.
        /// РґТ»ёц¶ЇМ¬і¤¶ИРЎ¶ЛЧЦЅЪUnicodeЧЦ·ыґ®Цµ»щ±ѕБчЈ¬ЖдґОКЗЛ«ЧЦЅЪЧЦ·ыЎЈ
        /// </summary>

        public void WriteDynamicLittleUni(string value)
        {
            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteLittleUniNull() with null value");
                value = String.Empty;
            }

            int length = value.Length;
            m_Stream.SetLength(m_Stream.Length + ((length + 1) * 2));

            m_Stream.Position += Encoding.Unicode.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
            m_Stream.Position += 2;
        }

        /// <summary>
        /// Writes a fixed-length little-endian unicode string value to the underlying stream. To fit (size), the string content is either truncated or padded with null characters.
        /// </summary>

        public void WriteFixedLittleEndian(string value, int size)
        {

            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteLittleUniFixed() with null value");
                value = String.Empty;

            }
            size *= 2;

            int length = value.Length;
            Write((short)length);
            m_Stream.SetLength(m_Stream.Length + size);

            if ((length * 2) >= size)
                m_Stream.Position += Encoding.Unicode.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
            else
            {
                Encoding.Unicode.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
                m_Stream.Position += size;
            }
        }

        /// <summary>
        /// Writes a dynamic-length big-endian unicode string value to the underlying stream, followed by a 2-byte null character.
        /// </summary>
        public void WriteDynamicBigUnicode(string value)
        {

            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteBigUniNull() with null value");
                value = String.Empty;
            }
            int length = value.Length;
            Write((short)length);
            m_Stream.SetLength(m_Stream.Length + ((length + 1) * 2));

            m_Stream.Position += Encoding.BigEndianUnicode.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
            m_Stream.Position += 2;
        }

        /// <summary>
        /// Writes a fixed-length big-endian unicode string value to the underlying stream. To fit (size), the string content is either truncated or padded with null characters.
        /// </summary>
        public void WriteFixedBigUnicode(string value, int size)
        {
            if (value == null)
            {
                Console.WriteLine("Network: Attempted to WriteBigUniFixed() with null value");
                value = String.Empty;
            }
            size *= 2;
            int length = value.Length;
            Write((short)length);
            m_Stream.SetLength(m_Stream.Length + size);

            if ((length * 2) >= size)
                m_Stream.Position += Encoding.BigEndianUnicode.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
            else
            {
                Encoding.BigEndianUnicode.GetBytes(value, 0, length, m_Stream.GetBuffer(), (int)m_Stream.Position);
                m_Stream.Position += size;
            }
        }

        /// <summary>
        /// Fills the stream from the current position up to (capacity) with 0x00's
        /// </summary>

        public void Fill()
        {
            Fill((int)(m_Capacity - m_Stream.Length));
        }

        /// <summary>
        /// Writes a number of 0x00 byte values to the underlying stream.
        /// </summary>

        public void Fill(int length)
        {
            if (m_Stream.Position == m_Stream.Length)
            {
                m_Stream.SetLength(m_Stream.Length + length);
                m_Stream.Seek(0, SeekOrigin.End);
            }
            else
            {
                m_Stream.Write(new byte[length], 0, length);
            }
        }
        /// <summary>
        /// Gets the total stream length.
        /// </summary>
        public long Length
        {
            get
            {
                return m_Stream.Length;
            }
        }



        /// <summary>
        /// Gets or sets the current stream position.
        /// </summary>
        public long Position
        {

            get
            {
                return m_Stream.Position;
            }

            set
            {
                m_Stream.Position = value;
            }
        }

        /// <summary>
        /// The internal stream used by this PacketWriter instance.
        /// </summary>

        public MemoryStream UnderlyingStream
        {
            get
            {
                return m_Stream;
            }
        }

        /// <summary>
        /// Offsets the current position from an origin.
        /// </summary>

        public long Seek(long offset, SeekOrigin origin)
        {
            return m_Stream.Seek(offset, origin);
        }

        /// <summary>
        /// Gets the entire stream content as a byte array.
        /// </summary>

        public byte[] ToArray()
        {
            return m_Stream.ToArray();
        }

    }

}