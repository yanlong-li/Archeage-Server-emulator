using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalCommons.Utilities
{
    /// <summary>
    /// Tools =)
    /// Auhtor: Raphail
    /// </summary>
    public class Utility
    {
        private static readonly DateTime Time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long CurrentTimeMilliseconds()
        {
            return (long)(DateTime.UtcNow - Time).TotalMilliseconds;
        }
        public static byte[] HexToByteArray(string hex)
        {
            string[] split = hex.Split(new string[] { " " }, StringSplitOptions.None);
            byte[] data = new byte[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                data[i] = byte.Parse(split[i], System.Globalization.NumberStyles.HexNumber);
            }
            return data;
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();

        }
        public static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }
        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        //public static string ByteArrayToString(byte[] compiled)
        //{
        //    StringBuilder builder = new StringBuilder(compiled.Length * 2);
        //    foreach (byte b in compiled)
        //        builder.AppendFormat("{0:X2} ", b);

        //    return builder.ToString();
        //}

        ////public static string ByteArrayToString(byte[] ba)
        ////{
        ////    string hex = BitConverter.ToString(ba);
        ////    return hex.Replace("-", "");
        ////}
        
         /*
         * Which works out about 30% faster than PZahras (not that you'd notice with small amounts of data).
         * The BitConverter method itself is pretty quick, it's just having to do the replace which slows it down, so if you can live with the dashes then it's perfectly good.
         */
        public static string ByteArrayToString(byte[] data)
        {
            char[] lookup = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int i = 0, p = 0, l = data.Length;
            char[] c = new char[l * 2 + 2];
            byte d;
            //int p = 2; c[0] = '0'; c[1] = 'x'; //если хотим 0x
            while (i < l)
            {
                d = data[i++];
                c[p++] = lookup[d / 0x10];
                c[p++] = lookup[d % 0x10];
            }
            return new string(c, 0, c.Length);
        }
    }
}
 