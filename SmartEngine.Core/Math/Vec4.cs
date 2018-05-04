using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Vec4>))]
    public unsafe struct Vec4
    {
        internal float x;
        internal float y;
        internal float z;
        internal float w;
        public static readonly Vec4 Zero;
        public Vec4(Vec4 source)
        {
            this.x = source.x;
            this.y = source.y;
            this.z = source.z;
            this.w = source.w;
        }

        public Vec4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vec4(Vec3 v, float w)
        {
            this.x = v.X;
            this.y = v.Y;
            this.z = v.Z;
            this.w = w;
        }

        static Vec4()
        {
            Zero = new Vec4(0f, 0f, 0f, 0f);
        }

        [LogicSystemMethodDisplay("Vec4( Single x, Single y, Single z, Single w )", "Vec4( {0}, {1}, {2}, {3} )")]
        public static Vec4 Construct(float x, float y, float z, float w)
        {
            return new Vec4(x, y, z, w);
        }

        [LogicSystemBrowsable(true), DefaultValue((float)0f)]
        public float X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        [DefaultValue((float)0f), LogicSystemBrowsable(true)]
        public float Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        [DefaultValue((float)0f), LogicSystemBrowsable(true)]
        public float Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }
        [DefaultValue((float)0f)]
        public float W
        {
            get
            {
                return this.w;
            }
            set
            {
                this.w = value;
            }
        }
        [LogicSystemBrowsable(true)]
        public static Vec4 Parse(string text)
        {
            Vec4 vec;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 4)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 4 parts separated by spaces in the form (x y z w).", text));
            }
            try
            {
                vec = new Vec4(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return vec;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", new object[] { this.x, this.y, this.z, this.w });
        }

        [LogicSystemBrowsable(true)]
        public string ToString(int precision)
        {
            string str = "";
            str = str.PadLeft(precision, '#');
            return string.Format("{0:0." + str + "} {1:0." + str + "} {2:0." + str + "} {3:0." + str + "}", new object[] { this.x, this.y, this.z, this.w });
        }

        public override bool Equals(object obj)
        {
            return ((obj is Vec4) && (this == ((Vec4)obj)));
        }

        public override int GetHashCode()
        {
            return (((this.x.GetHashCode() ^ this.y.GetHashCode()) ^ this.z.GetHashCode()) ^ this.w.GetHashCode());
        }

        public static Vec4 operator +(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x + v2.x;
            vec.y = v1.y + v2.y;
            vec.z = v1.z + v2.z;
            vec.w = v1.w + v2.w;
            return vec;
        }

        public static Vec4 operator -(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x - v2.x;
            vec.y = v1.y - v2.y;
            vec.z = v1.z - v2.z;
            vec.w = v1.w - v2.w;
            return vec;
        }

        public static Vec4 operator *(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x * v2.x;
            vec.y = v1.y * v2.y;
            vec.z = v1.z * v2.z;
            vec.w = v1.w * v2.w;
            return vec;
        }

        public static Vec4 operator *(Vec4 v, float s)
        {
            Vec4 vec;
            vec.x = v.x * s;
            vec.y = v.y * s;
            vec.z = v.z * s;
            vec.w = v.w * s;
            return vec;
        }

        public static Vec4 operator *(float s, Vec4 v)
        {
            Vec4 vec;
            vec.x = s * v.x;
            vec.y = s * v.y;
            vec.z = s * v.z;
            vec.w = s * v.w;
            return vec;
        }

        public static Vec4 operator /(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x / v2.x;
            vec.y = v1.y / v2.y;
            vec.z = v1.z / v2.z;
            vec.w = v1.w / v2.w;
            return vec;
        }

        public static Vec4 operator /(Vec4 v, float s)
        {
            Vec4 vec;
            float num = 1f / s;
            vec.x = v.x * num;
            vec.y = v.y * num;
            vec.z = v.z * num;
            vec.w = v.w * num;
            return vec;
        }

        public static Vec4 operator /(float s, Vec4 v)
        {
            Vec4 vec;
            vec.x = s / v.x;
            vec.y = s / v.y;
            vec.z = s / v.z;
            vec.w = s / v.w;
            return vec;
        }

        public static Vec4 operator -(Vec4 v)
        {
            Vec4 vec;
            vec.x = -v.x;
            vec.y = -v.y;
            vec.z = -v.z;
            vec.w = -v.w;
            return vec;
        }

        public static void Add(ref Vec4 v1, ref Vec4 v2, out Vec4 result)
        {
            result.x = v1.x + v2.x;
            result.y = v1.y + v2.y;
            result.z = v1.z + v2.z;
            result.w = v1.w + v2.w;
        }

        public static void Subtract(ref Vec4 v1, ref Vec4 v2, out Vec4 result)
        {
            result.x = v1.x - v2.x;
            result.y = v1.y - v2.y;
            result.z = v1.z - v2.z;
            result.w = v1.w - v2.w;
        }

        public static void Multiply(ref Vec4 v1, ref Vec4 v2, out Vec4 result)
        {
            result.x = v1.x * v2.x;
            result.y = v1.y * v2.y;
            result.z = v1.z * v2.z;
            result.w = v1.w * v2.w;
        }

        public static void Multiply(ref Vec4 v, float s, out Vec4 result)
        {
            result.x = v.x * s;
            result.y = v.y * s;
            result.z = v.z * s;
            result.w = v.w * s;
        }

        public static void Multiply(float s, ref Vec4 v, out Vec4 result)
        {
            result.x = v.x * s;
            result.y = v.y * s;
            result.z = v.z * s;
            result.w = v.w * s;
        }

        public static void Divide(ref Vec4 v1, ref Vec4 v2, out Vec4 result)
        {
            result.x = v1.x / v2.x;
            result.y = v1.y / v2.y;
            result.z = v1.z / v2.z;
            result.w = v1.w / v2.w;
        }

        public static void Divide(ref Vec4 v, float s, out Vec4 result)
        {
            float num = 1f / s;
            result.x = v.x * num;
            result.y = v.y * num;
            result.z = v.z * num;
            result.w = v.w * num;
        }

        public static void Divide(float s, ref Vec4 v, out Vec4 result)
        {
            result.x = s / v.x;
            result.y = s / v.y;
            result.z = s / v.z;
            result.w = s / v.w;
        }

        public static void Negate(ref Vec4 v, out Vec4 result)
        {
            result.x = -v.x;
            result.y = -v.y;
            result.z = -v.z;
            result.w = -v.w;
        }

        public static Vec4 Add(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x + v2.x;
            vec.y = v1.y + v2.y;
            vec.z = v1.z + v2.z;
            vec.w = v1.w + v2.w;
            return vec;
        }

        public static Vec4 Subtract(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x - v2.x;
            vec.y = v1.y - v2.y;
            vec.z = v1.z - v2.z;
            vec.w = v1.w - v2.w;
            return vec;
        }

        public static Vec4 Multiply(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x * v2.x;
            vec.y = v1.y * v2.y;
            vec.z = v1.z * v2.z;
            vec.w = v1.w * v2.w;
            return vec;
        }

        public static Vec4 Multiply(Vec4 v, float s)
        {
            Vec4 vec;
            vec.x = v.x * s;
            vec.y = v.y * s;
            vec.z = v.z * s;
            vec.w = v.w * s;
            return vec;
        }

        public static Vec4 Multiply(float s, Vec4 v)
        {
            Vec4 vec;
            vec.x = v.x * s;
            vec.y = v.y * s;
            vec.z = v.z * s;
            vec.w = v.w * s;
            return vec;
        }

        public static Vec4 Divide(Vec4 v1, Vec4 v2)
        {
            Vec4 vec;
            vec.x = v1.x / v2.x;
            vec.y = v1.y / v2.y;
            vec.z = v1.z / v2.z;
            vec.w = v1.w / v2.w;
            return vec;
        }

        public static Vec4 Divide(Vec4 v, float s)
        {
            Vec4 vec;
            float num = 1f / s;
            vec.x = v.x * num;
            vec.y = v.y * num;
            vec.z = v.z * num;
            vec.w = v.w * num;
            return vec;
        }

        public static Vec4 Divide(float s, Vec4 v)
        {
            Vec4 vec;
            vec.x = s / v.x;
            vec.y = s / v.y;
            vec.z = s / v.z;
            vec.w = s / v.w;
            return vec;
        }

        public static Vec4 Negate(Vec4 v)
        {
            Vec4 vec;
            vec.x = -v.x;
            vec.y = -v.y;
            vec.z = -v.z;
            vec.w = -v.w;
            return vec;
        }

        public static bool operator ==(Vec4 v1, Vec4 v2)
        {
            return ((((v1.x == v2.x) && (v1.y == v2.y)) && (v1.z == v2.z)) && (v1.w == v2.w));
        }

        public static bool operator !=(Vec4 v1, Vec4 v2)
        {
            if (((v1.x == v2.x) && (v1.y == v2.y)) && (v1.z == v2.z))
            {
                return (v1.w != v2.w);
            }
            return true;
        }

        public float this[int index]
        {
            get
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.x)
                {
                    return numRef[index * 4];
                }
            }
            set
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.x)
                {
                    numRef[index * 4] = value;
                }
            }
        }
        public bool Equals(Vec4 v, float epsilon)
        {
            if (System.Math.Abs((float)(this.x - v.x)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.y - v.y)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.z - v.z)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.w - v.w)) > epsilon)
            {
                return false;
            }
            return true;
        }

        public bool Equals(ref Vec4 v, float epsilon)
        {
            if (System.Math.Abs((float)(this.x - v.x)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.y - v.y)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.z - v.z)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.w - v.w)) > epsilon)
            {
                return false;
            }
            return true;
        }

        public float Length()
        {
            return MathFunctions.Sqrt((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
        }

        public float LengthFast()
        {
            return MathFunctions.Sqrt16((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
        }

        public float LengthSqr()
        {
            return ((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
        }

        private void A(Vec4 A, Vec4 a)
        {
            if (this.x < A.x)
            {
                this.x = A.x;
            }
            else if (this.x > a.x)
            {
                this.x = a.x;
            }
            if (this.y < A.y)
            {
                this.y = A.y;
            }
            else if (this.y > a.y)
            {
                this.y = a.y;
            }
            if (this.z < A.z)
            {
                this.z = A.z;
            }
            else if (this.z > a.z)
            {
                this.z = a.z;
            }
            if (this.w < A.w)
            {
                this.w = A.w;
            }
            else if (this.w > a.w)
            {
                this.w = a.w;
            }
        }

        public float Normalize()
        {
            float x = (((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w);
            float num2 = MathFunctions.InvSqrt(x);
            this.x *= num2;
            this.y *= num2;
            this.z *= num2;
            this.w *= num2;
            return (num2 * x);
        }

        public float NormalizeFast()
        {
            float x = (((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w);
            float num2 = MathFunctions.InvSqrt16(x);
            this.x *= num2;
            this.y *= num2;
            this.z *= num2;
            this.w *= num2;
            return (num2 * x);
        }

        public static Vec4 Normalize(Vec4 v)
        {
            float num = MathFunctions.InvSqrt((((v.x * v.x) + (v.y * v.y)) + (v.z * v.z)) + (v.w * v.w));
            return new Vec4(v.x * num, v.y * num, v.z * num, v.w * num);
        }

        public static Vec4 NormalizeFast(Vec4 v)
        {
            float num = MathFunctions.InvSqrt16((((v.x * v.x) + (v.y * v.y)) + (v.z * v.z)) + (v.w * v.w));
            return new Vec4(v.x * num, v.y * num, v.z * num, v.w * num);
        }

        public static void Normalize(ref Vec4 v, out Vec4 result)
        {
            float num = MathFunctions.InvSqrt((((v.x * v.x) + (v.y * v.y)) + (v.z * v.z)) + (v.w * v.w));
            result.x = v.x * num;
            result.y = v.y * num;
            result.z = v.z * num;
            result.w = v.w * num;
        }

        public static void NormalizeFast(ref Vec4 v, out Vec4 result)
        {
            float num = MathFunctions.InvSqrt16((((v.x * v.x) + (v.y * v.y)) + (v.z * v.z)) + (v.w * v.w));
            result.x = v.x * num;
            result.y = v.y * num;
            result.z = v.z * num;
            result.w = v.w * num;
        }

        public Vec4 GetNormalize()
        {
            float num = MathFunctions.InvSqrt((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
            return new Vec4(this.x * num, this.y * num, this.z * num, this.w * num);
        }

        public Vec4 GetNormalizeFast()
        {
            float num = MathFunctions.InvSqrt16((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
            return new Vec4(this.x * num, this.y * num, this.z * num, this.w * num);
        }

        public Vec2 ToVec2()
        {
            Vec2 vec;
            vec.x = this.x;
            vec.y = this.y;
            return vec;
        }

        public Vec3 ToVec3()
        {
            Vec3 vec;
            vec.x = this.x;
            vec.y = this.y;
            vec.z = this.z;
            return vec;
        }

        public static Vec4 Lerp(Vec4 v1, Vec4 v2, float amount)
        {
            Vec4 vec;
            vec.x = v1.x + ((v2.x - v1.x) * amount);
            vec.y = v1.y + ((v2.y - v1.y) * amount);
            vec.z = v1.z + ((v2.z - v1.z) * amount);
            vec.w = v1.w + ((v2.w - v1.w) * amount);
            return vec;
        }

        public static void Lerp(ref Vec4 v1, ref Vec4 v2, float amount, out Vec4 result)
        {
            result.x = v1.x + ((v2.x - v1.x) * amount);
            result.y = v1.y + ((v2.y - v1.y) * amount);
            result.z = v1.z + ((v2.z - v1.z) * amount);
            result.w = v1.w + ((v2.w - v1.w) * amount);
        }

        public void Saturate()
        {
            MathFunctions.Saturate(ref this.x);
            MathFunctions.Saturate(ref this.y);
            MathFunctions.Saturate(ref this.z);
            MathFunctions.Saturate(ref this.w);
        }
    }
}
