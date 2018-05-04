using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Utils
{
    /// <summary>
    /// 原子掩码类的泛型封装
    /// </summary>
    /// <typeparam name="T">一个枚举类型</typeparam>
    public class BitMask<T>
    {
        BitMask ori;
        /// <summary>
        /// 
        /// </summary>
        public BitMask()
        {
            this.ori = new BitMask();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ori"></param>
        public BitMask(BitMask ori)
        {
            this.ori = ori;
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>
        public bool Test(T Mask)
        {
            return ori.Test(Mask);
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>
        public void SetValue(T bitmask, bool val)
        {
            ori.SetValue(bitmask, val);
        }

        /// <summary>
        /// 此子掩码32位整数值
        /// </summary>
        public int Value
        {
            get
            {
                return ori.Value;
            }
            set
            {
                ori.Value = value;
            }
        }

        /// <summary>
        /// 强制转换
        /// </summary>
        /// <param name="ori"></param>
        /// <returns></returns>
        public static implicit operator BitMask<T>(BitMask ori)
        {
            return new BitMask<T>(ori);
        }
    }

    /// <summary>
    /// 子掩码标识类
    /// </summary>
    [Serializable]
    public class BitMask
    {
        int value;

        /// <summary>
        /// 子掩码值
        /// </summary>
        public int Value { get { return this.value; } set { this.value = value; } }

        /// <summary>
        /// 
        /// </summary>
        public BitMask()
        {
            value = 0;
        }

        public BitMask(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(object Mask)
        {
            return Test((int)Mask);
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(int Mask)
        {
            return (value & Mask) != 0;
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>        
        public void SetValue(object bitmask, bool val)
        {
            SetValue((int)bitmask, val);
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>
        public void SetValue(int bitmask, bool val)
        {
            if (this.Test(bitmask) != val)
            {
                if (val)
                    value = value | bitmask;
                else
                    value = value ^ bitmask;
            }
        }
    }

    /// <summary>
    /// 原子掩码类的泛型封装
    /// </summary>
    /// <typeparam name="T">一个枚举类型</typeparam>
    public class BitMask64<T>
    {
        BitMask64 ori;
        /// <summary>
        /// 
        /// </summary>
        public BitMask64()
        {
            this.ori = new BitMask64();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ori"></param>
        public BitMask64(BitMask64 ori)
        {
            this.ori = ori;
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>
        public bool Test(T Mask)
        {
            return ori.Test(Mask);
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>
        public void SetValue(T bitmask, bool val)
        {
            ori.SetValue(bitmask, val);
        }

        /// <summary>
        /// 此子掩码64位整数值
        /// </summary>
        public long Value
        {
            get
            {
                return ori.Value;
            }
            set
            {
                ori.Value = value;
            }
        }

        /// <summary>
        /// 强制转换
        /// </summary>
        /// <param name="ori"></param>
        /// <returns></returns>
        public static implicit operator BitMask64<T>(BitMask64 ori)
        {
            return new BitMask64<T>(ori);
        }
    }
    /// <summary>
    /// 子掩码标识类
    /// </summary>
    [Serializable]
    public class BitMask64
    {
        long value;

        /// <summary>
        /// 子掩码值
        /// </summary>
        public long Value { get { return this.value; } set { this.value = value; } }

        /// <summary>
        /// 
        /// </summary>
        public BitMask64()
        {
            value = 0;
        }

        public BitMask64(long value)
        {
            this.value = value;
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(object Mask)
        {
            return Test((long)Mask);
        }

        /// <summary>
        /// 检测某个标识
        /// </summary>
        /// <param name="Mask">标识</param>
        /// <returns>值</returns>        
        public bool Test(long Mask)
        {
            return (value & Mask) != 0;
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>        
        public void SetValue(object bitmask, bool val)
        {
            SetValue((long)bitmask, val);
        }

        /// <summary>
        /// 设定某标识的值
        /// </summary>
        /// <param name="bitmask">标识</param>
        /// <param name="val">真值</param>
        public void SetValue(long bitmask, bool val)
        {
            if (this.Test(bitmask) != val)
            {
                if (val)
                    value = value | bitmask;
                else
                    value = value ^ bitmask;
            }
        }
    }
}
