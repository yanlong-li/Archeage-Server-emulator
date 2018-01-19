using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalCommons.Native.Utilities
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
            return (long) (DateTime.UtcNow - Time).TotalMilliseconds;
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
    }
}
