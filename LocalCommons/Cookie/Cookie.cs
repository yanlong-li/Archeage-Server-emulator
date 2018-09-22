using System;

namespace LocalCommons.Cookie
{
    public static class Cookie
    {
        /// <summary>
        /// генерируем cookie
        /// </summary>
        /// <returns></returns>
        public static int Generate()
        {
            Random random = new Random();
            int cookie = random.Next(255);
            cookie += random.Next(255) << 8;
            cookie += random.Next(255) << 16;
            cookie += random.Next(255) << 24;
            return cookie;
        }
    }
}
