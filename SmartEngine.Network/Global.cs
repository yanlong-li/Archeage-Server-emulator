using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network
{
    
    /// <summary>
    /// 用于保存一些静态共用变量的类
    /// </summary>
    public static class Global
    {       
        /// <summary>
        /// 网络层字符串使用的编码，默认为UTF-8
        /// </summary>
        public static Encoding Encoding = System.Text.Encoding.GetEncoding("utf-8");

        /// <summary>
        /// 线程安全的随机数生成器
        /// </summary>
        public static RandomF Random = new RandomF();
        /// <summary>
        /// 时间0，用于将DateTime转换成int
        /// </summary>
        public static DateTime NeutralDate = new DateTime(1970, 1, 1);

        /// <summary>
        /// 将一个DateTime实例转换成int
        /// </summary>
        /// <param name="date">日期实例</param>
        /// <returns></returns>
        public static int DateToInt(DateTime date)
        {
            return (int)(date - NeutralDate).TotalSeconds;
        }

        /// <summary>
        /// 将int还原成DateTime实例
        /// </summary>
        /// <param name="date">以int表示的日期</param>
        /// <returns></returns>
        public static DateTime IntToDate(int date)
        {
            return NeutralDate.AddSeconds(date);
        }

        /// <summary>
        /// 取得一个跟时间相关的Packet唯一ID，用于识别内部通讯封包来源
        /// </summary>
        public static long PacketSession
        {
            get
            {
                long time = DateTime.Now.ToBinary();
                time |= (long)Random.Next();
                return time;
            }
        }

        public static long LittleToBigEndian(long input)
        {
            return (long)(((ulong)LittleToBigEndian((uint)((ulong)input & 0xFFFFFFFF)) << 32) | (ulong)LittleToBigEndian((uint)(((ulong)input & 0xFFFFFFFF00000000) >> 32)));
        }

        public static ulong LittleToBigEndian(ulong input)
        {
            return (ulong)(((ulong)LittleToBigEndian((uint)(input & 0xFFFFFFFF)) << 32) | (ulong)LittleToBigEndian((uint)((input & 0xFFFFFFFF00000000) >> 32)));
        }

        /// <summary>
        /// 将LittleEndian 整数转换成Big Endian
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int LittleToBigEndian(int input)
        {
            return ((input & 0xff) << 24) + ((input & 0xff00) << 8) + ((input & 0xff0000) >> 8) + ((input >> 24) & 0xff);
        }

        /// <summary>
        /// 将LittleEndian 整数转换成Big Endian
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static uint LittleToBigEndian(uint input)
        {
            return ((input & 0xff) << 24) + ((input & 0xff00) << 8) + ((input & 0xff0000) >> 8) + ((input >> 24) & 0xff);
        }

        /// <summary>
        /// 将LittleEndian 整数转换成Big Endian
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ushort LittleToBigEndian(ushort input)
        {
            return (ushort)(((input >> 8) & 0xff) + ((input << 8) & 0xff00));
        }

        /// <summary>
        /// 将LittleEndian 整数转换成Big Endian
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static short LittleToBigEndian(short input)
        {
            return (short)(((input >> 8) & 0xff) + ((input << 8) & 0xff00));
        }
    }
}
