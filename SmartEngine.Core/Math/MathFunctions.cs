using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core.Math
{
    [LogicSystemClassDisplay("Math"), LogicSystemBrowsable(true)]
    public static class MathFunctions
    {
        public const float Epsilon = 1E-06f;
        public const float Infinity = 1E+30f;
        public const float PI = 3.141593f;

        [LogicSystemBrowsable(true)]
        public static float ACos(float a)
        {
            return (float)System.Math.Acos((double)a);
        }

        public static float ACos16(float a)
        {
            return (float)System.Math.Acos((double)a);
        }

        [LogicSystemBrowsable(true)]
        public static float ASin(float a)
        {
            return (float)System.Math.Asin((double)a);
        }

        public static float ASin16(float a)
        {
            return (float)System.Math.Asin((double)a);
        }

        [LogicSystemBrowsable(true)]
        public static float ATan(float a)
        {
            return (float)System.Math.Atan((double)a);
        }

        [LogicSystemBrowsable(true)]
        public static float ATan(float y, float x)
        {
            return (float)System.Math.Atan2((double)y, (double)x);
        }

        public static float ATan16(float a)
        {
            return (float)System.Math.Atan((double)a);
        }

        public static float ATan16(float y, float x)
        {
            return (float)System.Math.Atan2((double)y, (double)x);
        }

        public static void Clamp(ref Degree value, Degree min, Degree max)
        {
            if (value < min)
            {
                value = min;
            }
            if (value > max)
            {
                value = max;
            }
        }

        public static void Clamp(ref Radian value, Radian min, Radian max)
        {
            if (value < min)
            {
                value = min;
            }
            if (value > max)
            {
                value = max;
            }
        }

        public static void Clamp(ref byte value, byte min, byte max)
        {
            if (value < min)
            {
                value = min;
            }
            if (value > max)
            {
                value = max;
            }
        }

        public static void Clamp(ref int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            if (value > max)
            {
                value = max;
            }
        }

        public static void Clamp(ref float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
            }
            if (value > max)
            {
                value = max;
            }
        }

        [LogicSystemBrowsable(true)]
        public static float Cos(float a)
        {
            return (float)System.Math.Cos((double)a);
        }

        public static float Cos16(float a)
        {
            return (float)System.Math.Cos((double)a);
        }

        [LogicSystemBrowsable(true)]
        public static float DegToRad(float a)
        {
            return (a * 0.01745329f);
        }

        [LogicSystemBrowsable(true)]
        public static float Exp(float f)
        {
            return (float)System.Math.Exp((double)f);
        }

        public static float Exp16(float f)
        {
            return (float)System.Math.Exp((double)f);
        }

        [LogicSystemBrowsable(true)]
        public static float Floor(float f)
        {
            return (float)System.Math.Floor((double)f);
        }

        public static float InvSqrt(float x)
        {
            return (1f / ((float)System.Math.Sqrt((double)x)));
        }

        public static float InvSqrt16(float x)
        {
            return (1f / ((float)System.Math.Sqrt((double)x)));
        }

        [LogicSystemBrowsable(true)]
        public static float Log(float a)
        {
            return (float)System.Math.Log((double)a);
        }

        [LogicSystemBrowsable(true)]
        public static float Log(float a, float newBase)
        {
            return (float)System.Math.Log((double)a, (double)newBase);
        }

        [LogicSystemBrowsable(true)]
        public static float Log10(float a)
        {
            return (float)System.Math.Log10((double)a);
        }

        [LogicSystemBrowsable(true)]
        public static float Pow(float y, float x)
        {
            return (float)System.Math.Pow((double)y, (double)x);
        }

        public static float Pow16(float y, float x)
        {
            return (float)System.Math.Pow((double)y, (double)x);
        }

        public static float RadiansDelta(float angle1, float angle2)
        {
            return RadiansNormalize180(angle1 - angle2);
        }

        public static float RadiansNormalize180(float angle)
        {
            angle = RadiansNormalize360(angle);
            if (angle > 3.141593f)
            {
                angle -= 6.283185f;
            }
            return angle;
        }

        public static float RadiansNormalize360(float angle)
        {
            if ((angle >= 6.283185f) || (angle < 0f))
            {
                angle -= Floor(angle / 6.283185f) * 6.283185f;
            }
            return angle;
        }

        [LogicSystemBrowsable(true)]
        public static float RadToDeg(float a)
        {
            return (a * 57.29578f);
        }

        public static float Round(float a)
        {
            return (float)System.Math.Round((double)a);
        }

        public static void Saturate(ref float value)
        {
            if (value < 0f)
            {
                value = 0f;
            }
            if (value > 1f)
            {
                value = 1f;
            }
        }

        [LogicSystemBrowsable(true)]
        public static float Sin(float a)
        {
            return (float)System.Math.Sin((double)a);
        }

        public static float Sin16(float a)
        {
            return (float)System.Math.Sin((double)a);
        }

        [LogicSystemBrowsable(true)]
        public static float Sqrt(float x)
        {
            return (float)System.Math.Sqrt((double)x);
        }

        public static float Sqrt16(float x)
        {
            return (float)System.Math.Sqrt((double)x);
        }

        public static void Swap(ref float a, ref float b)
        {
            float num = a;
            a = b;
            b = num;
        }

        [LogicSystemBrowsable(true)]
        public static float Tan(float a)
        {
            return (float)System.Math.Tan((double)a);
        }

        public static float Tan16(float a)
        {
            return (float)System.Math.Cos((double)a);
        }
    }
}
