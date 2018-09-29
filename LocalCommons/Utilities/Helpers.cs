using System;

namespace LocalCommons.Utilities
{
    public class Helpers
    {
        public static float ConvertX(byte[] coords)
        {
            return (float)Math.Round(coords[0] * 0.002f + coords[1] * 0.5f + coords[2] * 128, 4, MidpointRounding.ToEven);
        }

        public static byte[] ConvertX(float x)
        {
            var coords = new byte[3];
            var temp = x;
            coords[2] = (byte)(temp / 128f);
            temp -= coords[2] * 128;
            coords[1] = (byte)(temp / 0.5f);
            temp -= coords[1] * 0.5f;
            coords[0] = (byte)(temp * 512);
            return coords;
        }

        public static float ConvertY(byte[] coords)
        {
            return (float)Math.Round(coords[0] * 0.002f + coords[1] * 0.5f + coords[2] * 128, 4, MidpointRounding.ToEven);
        }

        public static byte[] ConvertY(float y)
        {
            var coords = new byte[3];
            var temp = y;
            coords[2] = (byte)(temp / 128);
            temp -= coords[2] * 128;
            coords[1] = (byte)(temp / 0.5f);
            temp -= coords[1] * 0.5f;
            coords[0] = (byte)(temp * 512);
            return coords;
        }

        public static float ConvertZ(byte[] coords)
        {
            return (float)Math.Round(coords[0] * 0.001f + coords[1] * 0.2561f + coords[2] * 65.5625f - 100, 4, MidpointRounding.ToEven);
        }

        public static byte[] ConvertZ(float z)
        {
            var coords = new byte[3];
            var temp = z + 100;
            coords[2] = (byte)(temp / 65.5625f);
            temp -= coords[2] * 65.5625f;
            coords[1] = (byte)(temp / 0.2561);
            temp -= coords[1] * 0.2561f;
            coords[0] = (byte)(temp / 0.001);
            return coords;
        }

        public static float ConvertLongX(long x)
        {
            return ((x >> 32) / 4096f);
        }

        public static long ConvertLongX(float x)
        {
            return ((long)((x) * 4096)) << 32;
        }

        public static float ConvertLongY(long y)
        {
            return ((y >> 32) / 4096f);
        }

        public static long ConvertLongY(float y)
        {
            return ((long)((y) * 4096)) << 32;
        }
    }
}