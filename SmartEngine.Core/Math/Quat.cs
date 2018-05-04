using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_QuatAsAnglesConverter)), LogicSystemBrowsable(true)]
    public struct Quat
    {
        internal float x;
        internal float y;
        internal float z;
        internal float w;
        public static readonly Quat Identity;
        public Quat(Vec3 v, float w)
        {
            this.x = v.X;
            this.y = v.Y;
            this.z = v.Z;
            this.w = w;
        }

        public Quat(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quat(Quat source)
        {
            this.x = source.x;
            this.y = source.y;
            this.z = source.z;
            this.w = source.w;
        }

        static Quat()
        {
            Identity = new Quat(0f, 0f, 0f, 1f);
        }

        [LogicSystemMethodDisplay("Quat( Single x, Single y, Single z, Single w )", "Quat( {0}, {1}, {2}, {3} )")]
        public static Quat Construct(float x, float y, float z, float w)
        {
            return new Quat(x, y, z, w);
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
        [LogicSystemBrowsable(true), DefaultValue((float)0f)]
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
        [DefaultValue((float)1f), LogicSystemBrowsable(true)]
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
        public static Quat Parse(string text)
        {
            Quat quat;
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
                quat = new Quat(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return quat;
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
            return ((obj is Quat) && (this == ((Quat)obj)));
        }

        public override int GetHashCode()
        {
            return (((this.x.GetHashCode() ^ this.y.GetHashCode()) ^ this.z.GetHashCode()) ^ this.w.GetHashCode());
        }

        [LogicSystemBrowsable(true)]
        public static Quat operator +(Quat v1, Quat v2)
        {
            Quat quat;
            quat.x = v1.x + v2.x;
            quat.y = v1.y + v2.y;
            quat.z = v1.z + v2.z;
            quat.w = v1.w + v2.w;
            return quat;
        }

        [LogicSystemBrowsable(true)]
        public static Quat operator -(Quat v1, Quat v2)
        {
            Quat quat;
            quat.x = v1.x - v2.x;
            quat.y = v1.y - v2.y;
            quat.z = v1.z - v2.z;
            quat.w = v1.w - v2.w;
            return quat;
        }

        [LogicSystemBrowsable(true)]
        public static Quat operator *(Quat v1, Quat v2)
        {
            Quat quat;
            quat.x = (((v1.w * v2.x) + (v1.x * v2.w)) + (v1.y * v2.z)) - (v1.z * v2.y);
            quat.y = (((v1.w * v2.y) + (v1.y * v2.w)) + (v1.z * v2.x)) - (v1.x * v2.z);
            quat.z = (((v1.w * v2.z) + (v1.z * v2.w)) + (v1.x * v2.y)) - (v1.y * v2.x);
            quat.w = (((v1.w * v2.w) - (v1.x * v2.x)) - (v1.y * v2.y)) - (v1.z * v2.z);
            return quat;
        }

        [LogicSystemBrowsable(true)]
        public static Vec3 operator *(Quat q, Vec3 v)
        {
            Vec3 vec;
            Multiply(ref q, ref v, out vec);
            return vec;
        }

        public static Vec3 operator *(Vec3 v, Quat q)
        {
            Vec3 vec;
            Multiply(ref q, ref v, out vec);
            return vec;
        }

        [LogicSystemBrowsable(true)]
        public static Quat operator *(Quat q, float v)
        {
            Quat quat;
            quat.x = q.x * v;
            quat.y = q.y * v;
            quat.z = q.z * v;
            quat.w = q.w * v;
            return quat;
        }

        public static Quat operator *(float v, Quat q)
        {
            Quat quat;
            quat.x = q.x * v;
            quat.y = q.y * v;
            quat.z = q.z * v;
            quat.w = q.w * v;
            return quat;
        }

        [LogicSystemBrowsable(true)]
        public static Quat operator -(Quat v)
        {
            Quat quat;
            quat.x = -v.x;
            quat.y = -v.y;
            quat.z = -v.z;
            quat.w = -v.w;
            return quat;
        }

        public static void Add(ref Quat v1, ref Quat v2, out Quat result)
        {
            result.x = v1.x + v2.x;
            result.y = v1.y + v2.y;
            result.z = v1.z + v2.z;
            result.w = v1.w + v2.w;
        }

        public static void Subtract(ref Quat v1, ref Quat v2, out Quat result)
        {
            result.x = v1.x - v2.x;
            result.y = v1.y - v2.y;
            result.z = v1.z - v2.z;
            result.w = v1.w - v2.w;
        }

        public static void Multiply(ref Quat v1, ref Quat v2, out Quat result)
        {
            result.x = (((v1.w * v2.x) + (v1.x * v2.w)) + (v1.y * v2.z)) - (v1.z * v2.y);
            result.y = (((v1.w * v2.y) + (v1.y * v2.w)) + (v1.z * v2.x)) - (v1.x * v2.z);
            result.z = (((v1.w * v2.z) + (v1.z * v2.w)) + (v1.x * v2.y)) - (v1.y * v2.x);
            result.w = (((v1.w * v2.w) - (v1.x * v2.x)) - (v1.y * v2.y)) - (v1.z * v2.z);
        }

        public static void Multiply(ref Quat q, ref Vec3 v, out Vec3 result)
        {
            Vec3 vec;
            Vec3 vec2;
            Vec3 vec3 = new Vec3(q.X, q.Y, q.Z);
            Vec3.Cross(ref vec3, ref v, out vec);
            Vec3.Cross(ref vec3, ref vec, out vec2);
            float num = 2f * q.w;
            vec.x *= num;
            vec.y *= num;
            vec.z *= num;
            vec2.x *= 2f;
            vec2.y *= 2f;
            vec2.z *= 2f;
            result.x = (v.x + vec.x) + vec2.x;
            result.y = (v.y + vec.y) + vec2.y;
            result.z = (v.z + vec.z) + vec2.z;
        }

        public static void Multiply(ref Vec3 v, ref Quat q, out Vec3 result)
        {
            Multiply(ref q, ref v, out result);
        }

        public static void Multiply(ref Quat q, float v, out Quat result)
        {
            result.x = q.x * v;
            result.y = q.y * v;
            result.z = q.z * v;
            result.w = q.w * v;
        }

        public static void Multiply(float v, ref Quat q, out Quat result)
        {
            result.x = q.x * v;
            result.y = q.y * v;
            result.z = q.z * v;
            result.w = q.w * v;
        }

        public static void Negate(ref Quat v, out Quat result)
        {
            result.x = -v.x;
            result.y = -v.y;
            result.z = -v.z;
            result.w = -v.w;
        }

        public static Quat Add(Quat v1, Quat v2)
        {
            Quat quat;
            quat.x = v1.x + v2.x;
            quat.y = v1.y + v2.y;
            quat.z = v1.z + v2.z;
            quat.w = v1.w + v2.w;
            return quat;
        }

        public static Quat Subtract(Quat v1, Quat v2)
        {
            Quat quat;
            quat.x = v1.x - v2.x;
            quat.y = v1.y - v2.y;
            quat.z = v1.z - v2.z;
            quat.w = v1.w - v2.w;
            return quat;
        }

        public static Quat Multiply(Quat v1, Quat v2)
        {
            Quat quat;
            quat.x = (((v1.w * v2.x) + (v1.x * v2.w)) + (v1.y * v2.z)) - (v1.z * v2.y);
            quat.y = (((v1.w * v2.y) + (v1.y * v2.w)) + (v1.z * v2.x)) - (v1.x * v2.z);
            quat.z = (((v1.w * v2.z) + (v1.z * v2.w)) + (v1.x * v2.y)) - (v1.y * v2.x);
            quat.w = (((v1.w * v2.w) - (v1.x * v2.x)) - (v1.y * v2.y)) - (v1.z * v2.z);
            return quat;
        }

        public static Quat Multiply(Quat q, float v)
        {
            Quat quat;
            quat.x = q.x * v;
            quat.y = q.y * v;
            quat.z = q.z * v;
            quat.w = q.w * v;
            return quat;
        }

        public static Quat Multiply(float v, Quat q)
        {
            Quat quat;
            quat.x = q.x * v;
            quat.y = q.y * v;
            quat.z = q.z * v;
            quat.w = q.w * v;
            return quat;
        }

        public static Quat Negate(Quat v)
        {
            Quat quat;
            quat.x = -v.x;
            quat.y = -v.y;
            quat.z = -v.z;
            quat.w = -v.w;
            return quat;
        }

        [LogicSystemBrowsable(true)]
        public unsafe float this[int index]
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
        [LogicSystemBrowsable(true)]
        public static bool operator ==(Quat v1, Quat v2)
        {
            return ((((v1.x == v2.x) && (v1.y == v2.y)) && (v1.z == v2.z)) && (v1.w == v2.w));
        }

        [LogicSystemBrowsable(true)]
        public static bool operator !=(Quat v1, Quat v2)
        {
            if (((v1.x == v2.x) && (v1.y == v2.y)) && (v1.z == v2.z))
            {
                return (v1.w != v2.w);
            }
            return true;
        }

        public bool Equals(Quat v, float epsilon)
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

        public bool Equals(ref Quat v, float epsilon)
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

        [LogicSystemBrowsable(true)]
        public void Inverse()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
        }

        public Quat GetInverse()
        {
            Quat quat;
            quat.x = -this.x;
            quat.y = -this.y;
            quat.z = -this.z;
            quat.w = this.w;
            return quat;
        }

        [LogicSystemBrowsable(true)]
        public float Length()
        {
            return MathFunctions.Sqrt((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
        }

        public float LengthFast()
        {
            return MathFunctions.Sqrt16((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
        }

        [LogicSystemBrowsable(true)]
        public void Normalize()
        {
            float num = MathFunctions.Sqrt((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
            if (num != 0f)
            {
                float num2 = 1f / num;
                this.x *= num2;
                this.y *= num2;
                this.z *= num2;
                this.w *= num2;
            }
        }

        public Quat GetNormalize()
        {
            Quat quat;
            float num = MathFunctions.Sqrt((((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)) + (this.w * this.w));
            if (num != 0f)
            {
                float num2 = 1f / num;
                quat.x = this.x * num2;
                quat.y = this.y * num2;
                quat.z = this.z * num2;
                quat.w = this.w * num2;
                return quat;
            }
            quat.x = this.x;
            quat.y = this.y;
            quat.z = this.z;
            quat.w = this.w;
            return quat;
        }

        public static void GetNormalize(ref Quat q, out Quat result)
        {
            float num = MathFunctions.Sqrt((((q.x * q.x) + (q.y * q.y)) + (q.z * q.z)) + (q.w * q.w));
            if (num != 0f)
            {
                float num2 = 1f / num;
                result.x = q.x * num2;
                result.y = q.y * num2;
                result.z = q.z * num2;
                result.w = q.w * num2;
            }
            else
            {
                result.x = q.x;
                result.y = q.y;
                result.z = q.z;
                result.w = q.w;
            }
        }

        public Mat3 ToMat3()
        {
            Mat3 mat;
            this.ToMat3(out mat);
            return mat;
        }

        public void ToMat3(out Mat3 result)
        {
            float num = this.x + this.x;
            float num2 = this.y + this.y;
            float num3 = this.z + this.z;
            float num4 = this.x * num;
            float num5 = this.x * num2;
            float num6 = this.x * num3;
            float num7 = this.y * num2;
            float num8 = this.y * num3;
            float num9 = this.z * num3;
            float num10 = this.w * num;
            float num11 = this.w * num2;
            float num12 = this.w * num3;
            result.Rt.x = 1f - (num7 + num9);
            result.Rt.y = num5 + num12;
            result.Rt.z = num6 - num11;
            result.RU.x = num5 - num12;
            result.RU.y = 1f - (num4 + num9);
            result.RU.z = num8 + num10;
            result.Ru.x = num6 + num11;
            result.Ru.y = num8 - num10;
            result.Ru.z = 1f - (num4 + num7);
        }

        public static Quat Slerp(Quat from, Quat to, float t)
        {
            Quat quat;
            Slerp(ref from, ref to, t, out quat);
            return quat;
        }

        public static void Slerp(ref Quat from, ref Quat to, float t, out Quat result)
        {
            Quat quat;
            float num4;
            float num5;
            Quat quat2;
            Quat quat3;
            if (t <= 0f)
            {
                result = from;
            }
            if (t >= 1f)
            {
                result = to;
            }
            if (from == to)
            {
                result = to;
            }
            float x = (((from.x * to.x) + (from.y * to.y)) + (from.z * to.z)) + (from.w * to.w);
            if (x < 0f)
            {
                quat = -to;
                x = -x;
            }
            else
            {
                quat = to;
            }
            if ((1f - x) > 1E-06f)
            {
                num4 = 1f - (x * x);
                float num3 = MathFunctions.InvSqrt(num4);
                float num = MathFunctions.ATan(num4 * num3, x);
                num4 = MathFunctions.Sin((1f - t) * num) * num3;
                num5 = MathFunctions.Sin(t * num) * num3;
            }
            else
            {
                num4 = 1f - t;
                num5 = t;
            }
            Multiply(num4, ref from, out quat2);
            Multiply(num5, ref quat, out quat3);
            Add(ref quat2, ref quat3, out result);
        }

        public Angles ToAngles()
        {
            Angles angles;
            Mat3 mat;
            this.ToMat3(out mat);
            mat.ToAngles(out angles);
            return angles;
        }

        public void ToAngles(out Angles result)
        {
            Mat3 mat;
            this.ToMat3(out mat);
            mat.ToAngles(out result);
        }

        public Vec3 GetForward()
        {
            Vec3 vec;
            vec.x = 1f - (((this.y * this.y) + (this.z * this.z)) * 2f);
            vec.y = ((this.z * this.w) + (this.x * this.y)) * 2f;
            vec.z = ((this.x * this.z) - (this.y * this.w)) * 2f;
            return vec;
        }

        public void GetForward(out Vec3 result)
        {
            result.x = 1f - (((this.y * this.y) + (this.z * this.z)) * 2f);
            result.y = ((this.z * this.w) + (this.x * this.y)) * 2f;
            result.z = ((this.x * this.z) - (this.y * this.w)) * 2f;
        }

        public Vec3 GetUp()
        {
            Vec3 vec;
            vec.x = ((this.y * this.w) + (this.z * this.x)) * 2f;
            vec.y = ((this.z * this.y) - (this.x * this.w)) * 2f;
            vec.z = 1f - (((this.x * this.x) + (this.y * this.y)) * 2f);
            return vec;
        }

        public void GetUp(out Vec3 result)
        {
            result.x = ((this.y * this.w) + (this.z * this.x)) * 2f;
            result.y = ((this.z * this.y) - (this.x * this.w)) * 2f;
            result.z = 1f - (((this.x * this.x) + (this.y * this.y)) * 2f);
        }

        public static Quat FromDirectionZAxisUp(Vec3 direction)
        {
            float num = MathFunctions.ATan(direction.Y, direction.X);
            float x = MathFunctions.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y));
            float num2 = MathFunctions.ATan(direction.Z, x);
            float a = -num * 0.5f;
            float num4 = -MathFunctions.Sin(a);
            float num5 = MathFunctions.Cos(a);
            float num9 = num2 * 0.5f;
            float num7 = -MathFunctions.Sin(num9);
            float num8 = MathFunctions.Cos(num9);
            return new Quat(-num4 * num7, num5 * num7, num4 * num8, num5 * num8);
        }

        public static Radian GetAngle(Quat v1, Quat v2)
        {
            float a = (((v1.X * v2.X) + (v1.Y * v2.Y)) + (v1.Z * v2.Z)) + (v1.W * v2.W);
            return new Radian(MathFunctions.ACos(a) * 2f);
        }
    }
}
