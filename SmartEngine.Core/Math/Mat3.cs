using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Design;
namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Mat3
    {
        internal Vec3 Rt;
        internal Vec3 RU;
        internal Vec3 Ru;
        public static readonly Mat3 Zero;
        public static readonly Mat3 Identity;
        public Mat3(float xx, float xy, float xz, float yx, float yy, float yz, float zx, float zy, float zz)
        {
            this.Rt = new Vec3(xx, xy, xz);
            this.RU = new Vec3(yx, yy, yz);
            this.Ru = new Vec3(zx, zy, zz);
        }

        public Mat3(Vec3 x, Vec3 y, Vec3 z)
        {
            this.Rt = x;
            this.RU = y;
            this.Ru = z;
        }

        public Mat3(Mat3 source)
        {
            this.Rt = source.Rt;
            this.RU = source.RU;
            this.Ru = source.Ru;
        }

        static Mat3()
        {
            Zero = new Mat3(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            Identity = new Mat3(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f);
        }

        public unsafe Vec3 this[int index]
        {
            get
            {
                if ((index < 0) || (index > 2))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (Vec3* vecRef = &this.Rt)
                {
                    return vecRef[index];
                }
            }
            set
            {
                if ((index < 0) || (index > 2))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (Vec3* vecRef = &this.Rt)
                {
                    vecRef[index] = value;
                }
            }
        }
        public override int GetHashCode()
        {
            return ((this.Rt.GetHashCode() ^ this.RU.GetHashCode()) ^ this.Ru.GetHashCode());
        }

        public static Mat3 operator +(Mat3 v1, Mat3 v2)
        {
            Mat3 mat;
            mat.Rt.x = v1.Rt.x + v2.Rt.x;
            mat.Rt.y = v1.Rt.y + v2.Rt.y;
            mat.Rt.z = v1.Rt.z + v2.Rt.z;
            mat.RU.x = v1.RU.x + v2.RU.x;
            mat.RU.y = v1.RU.y + v2.RU.y;
            mat.RU.z = v1.RU.z + v2.RU.z;
            mat.Ru.x = v1.Ru.x + v2.Ru.x;
            mat.Ru.y = v1.Ru.y + v2.Ru.y;
            mat.Ru.z = v1.Ru.z + v2.Ru.z;
            return mat;
        }

        public static Mat3 operator -(Mat3 v1, Mat3 v2)
        {
            Mat3 mat;
            mat.Rt.x = v1.Rt.x - v2.Rt.x;
            mat.Rt.y = v1.Rt.y - v2.Rt.y;
            mat.Rt.z = v1.Rt.z - v2.Rt.z;
            mat.RU.x = v1.RU.x - v2.RU.x;
            mat.RU.y = v1.RU.y - v2.RU.y;
            mat.RU.z = v1.RU.z - v2.RU.z;
            mat.Ru.x = v1.Ru.x - v2.Ru.x;
            mat.Ru.y = v1.Ru.y - v2.Ru.y;
            mat.Ru.z = v1.Ru.z - v2.Ru.z;
            return mat;
        }

        public static Mat3 operator *(Mat3 m, float s)
        {
            Mat3 mat;
            mat.Rt.x = m.Rt.x * s;
            mat.Rt.y = m.Rt.y * s;
            mat.Rt.z = m.Rt.z * s;
            mat.RU.x = m.RU.x * s;
            mat.RU.y = m.RU.y * s;
            mat.RU.z = m.RU.z * s;
            mat.Ru.x = m.Ru.x * s;
            mat.Ru.y = m.Ru.y * s;
            mat.Ru.z = m.Ru.z * s;
            return mat;
        }

        public static Mat3 operator *(float s, Mat3 m)
        {
            Mat3 mat;
            mat.Rt.x = m.Rt.x * s;
            mat.Rt.y = m.Rt.y * s;
            mat.Rt.z = m.Rt.z * s;
            mat.RU.x = m.RU.x * s;
            mat.RU.y = m.RU.y * s;
            mat.RU.z = m.RU.z * s;
            mat.Ru.x = m.Ru.x * s;
            mat.Ru.y = m.Ru.y * s;
            mat.Ru.z = m.Ru.z * s;
            return mat;
        }

        public static Vec3 operator *(Mat3 m, Vec3 v)
        {
            Vec3 vec;
            vec.x = ((m.Rt.x * v.X) + (m.RU.x * v.Y)) + (m.Ru.x * v.Z);
            vec.y = ((m.Rt.y * v.X) + (m.RU.y * v.Y)) + (m.Ru.y * v.Z);
            vec.z = ((m.Rt.z * v.X) + (m.RU.z * v.Y)) + (m.Ru.z * v.Z);
            return vec;
        }

        public static Vec3 operator *(Vec3 v, Mat3 m)
        {
            Vec3 vec;
            vec.x = ((m.Rt.x * v.X) + (m.RU.x * v.Y)) + (m.Ru.x * v.Z);
            vec.y = ((m.Rt.y * v.X) + (m.RU.y * v.Y)) + (m.Ru.y * v.Z);
            vec.z = ((m.Rt.z * v.X) + (m.RU.z * v.Y)) + (m.Ru.z * v.Z);
            return vec;
        }

        public static Mat3 operator *(Mat3 v1, Mat3 v2)
        {
            Mat3 mat;
            mat.Rt.x = ((v1.Rt.x * v2.Rt.x) + (v1.RU.x * v2.Rt.y)) + (v1.Ru.x * v2.Rt.z);
            mat.Rt.y = ((v1.Rt.y * v2.Rt.x) + (v1.RU.y * v2.Rt.y)) + (v1.Ru.y * v2.Rt.z);
            mat.Rt.z = ((v1.Rt.z * v2.Rt.x) + (v1.RU.z * v2.Rt.y)) + (v1.Ru.z * v2.Rt.z);
            mat.RU.x = ((v1.Rt.x * v2.RU.x) + (v1.RU.x * v2.RU.y)) + (v1.Ru.x * v2.RU.z);
            mat.RU.y = ((v1.Rt.y * v2.RU.x) + (v1.RU.y * v2.RU.y)) + (v1.Ru.y * v2.RU.z);
            mat.RU.z = ((v1.Rt.z * v2.RU.x) + (v1.RU.z * v2.RU.y)) + (v1.Ru.z * v2.RU.z);
            mat.Ru.x = ((v1.Rt.x * v2.Ru.x) + (v1.RU.x * v2.Ru.y)) + (v1.Ru.x * v2.Ru.z);
            mat.Ru.y = ((v1.Rt.y * v2.Ru.x) + (v1.RU.y * v2.Ru.y)) + (v1.Ru.y * v2.Ru.z);
            mat.Ru.z = ((v1.Rt.z * v2.Ru.x) + (v1.RU.z * v2.Ru.y)) + (v1.Ru.z * v2.Ru.z);
            return mat;
        }

        public static Mat3 operator -(Mat3 v)
        {
            Mat3 mat;
            mat.Rt.x = -v.Rt.x;
            mat.Rt.y = -v.Rt.y;
            mat.Rt.z = -v.Rt.z;
            mat.RU.x = -v.RU.x;
            mat.RU.y = -v.RU.y;
            mat.RU.z = -v.RU.z;
            mat.Ru.x = -v.Ru.x;
            mat.Ru.y = -v.Ru.y;
            mat.Ru.z = -v.Ru.z;
            return mat;
        }

        public static void Add(ref Mat3 v1, ref Mat3 v2, out Mat3 result)
        {
            result.Rt.x = v1.Rt.x + v2.Rt.x;
            result.Rt.y = v1.Rt.y + v2.Rt.y;
            result.Rt.z = v1.Rt.z + v2.Rt.z;
            result.RU.x = v1.RU.x + v2.RU.x;
            result.RU.y = v1.RU.y + v2.RU.y;
            result.RU.z = v1.RU.z + v2.RU.z;
            result.Ru.x = v1.Ru.x + v2.Ru.x;
            result.Ru.y = v1.Ru.y + v2.Ru.y;
            result.Ru.z = v1.Ru.z + v2.Ru.z;
        }

        public static void Subtract(ref Mat3 v1, ref Mat3 v2, out Mat3 result)
        {
            result.Rt.x = v1.Rt.x - v2.Rt.x;
            result.Rt.y = v1.Rt.y - v2.Rt.y;
            result.Rt.z = v1.Rt.z - v2.Rt.z;
            result.RU.x = v1.RU.x - v2.RU.x;
            result.RU.y = v1.RU.y - v2.RU.y;
            result.RU.z = v1.RU.z - v2.RU.z;
            result.Ru.x = v1.Ru.x - v2.Ru.x;
            result.Ru.y = v1.Ru.y - v2.Ru.y;
            result.Ru.z = v1.Ru.z - v2.Ru.z;
        }

        public static void Multiply(ref Mat3 m, float s, out Mat3 result)
        {
            result.Rt.x = m.Rt.x * s;
            result.Rt.y = m.Rt.y * s;
            result.Rt.z = m.Rt.z * s;
            result.RU.x = m.RU.x * s;
            result.RU.y = m.RU.y * s;
            result.RU.z = m.RU.z * s;
            result.Ru.x = m.Ru.x * s;
            result.Ru.y = m.Ru.y * s;
            result.Ru.z = m.Ru.z * s;
        }

        public static void Multiply(float s, ref Mat3 m, out Mat3 result)
        {
            result.Rt.x = m.Rt.x * s;
            result.Rt.y = m.Rt.y * s;
            result.Rt.z = m.Rt.z * s;
            result.RU.x = m.RU.x * s;
            result.RU.y = m.RU.y * s;
            result.RU.z = m.RU.z * s;
            result.Ru.x = m.Ru.x * s;
            result.Ru.y = m.Ru.y * s;
            result.Ru.z = m.Ru.z * s;
        }

        public static void Multiply(ref Vec3 v, ref Mat3 m, out Vec3 result)
        {
            result.x = ((m.Rt.x * v.x) + (m.RU.x * v.y)) + (m.Ru.x * v.z);
            result.y = ((m.Rt.y * v.x) + (m.RU.y * v.y)) + (m.Ru.y * v.z);
            result.z = ((m.Rt.z * v.x) + (m.RU.z * v.y)) + (m.Ru.z * v.z);
        }

        public static void Multiply(ref Mat3 m, ref Vec3 v, out Vec3 result)
        {
            result.x = ((m.Rt.x * v.x) + (m.RU.x * v.y)) + (m.Ru.x * v.z);
            result.y = ((m.Rt.y * v.x) + (m.RU.y * v.y)) + (m.Ru.y * v.z);
            result.z = ((m.Rt.z * v.x) + (m.RU.z * v.y)) + (m.Ru.z * v.z);
        }

        public static void Multiply(ref Mat3 v1, ref Mat3 v2, out Mat3 result)
        {
            result.Rt.x = ((v1.Rt.x * v2.Rt.x) + (v1.RU.x * v2.Rt.y)) + (v1.Ru.x * v2.Rt.z);
            result.Rt.y = ((v1.Rt.y * v2.Rt.x) + (v1.RU.y * v2.Rt.y)) + (v1.Ru.y * v2.Rt.z);
            result.Rt.z = ((v1.Rt.z * v2.Rt.x) + (v1.RU.z * v2.Rt.y)) + (v1.Ru.z * v2.Rt.z);
            result.RU.x = ((v1.Rt.x * v2.RU.x) + (v1.RU.x * v2.RU.y)) + (v1.Ru.x * v2.RU.z);
            result.RU.y = ((v1.Rt.y * v2.RU.x) + (v1.RU.y * v2.RU.y)) + (v1.Ru.y * v2.RU.z);
            result.RU.z = ((v1.Rt.z * v2.RU.x) + (v1.RU.z * v2.RU.y)) + (v1.Ru.z * v2.RU.z);
            result.Ru.x = ((v1.Rt.x * v2.Ru.x) + (v1.RU.x * v2.Ru.y)) + (v1.Ru.x * v2.Ru.z);
            result.Ru.y = ((v1.Rt.y * v2.Ru.x) + (v1.RU.y * v2.Ru.y)) + (v1.Ru.y * v2.Ru.z);
            result.Ru.z = ((v1.Rt.z * v2.Ru.x) + (v1.RU.z * v2.Ru.y)) + (v1.Ru.z * v2.Ru.z);
        }

        public static void Negate(ref Mat3 m, out Mat3 result)
        {
            result.Rt.x = -m.Rt.x;
            result.Rt.y = -m.Rt.y;
            result.Rt.z = -m.Rt.z;
            result.RU.x = -m.RU.x;
            result.RU.y = -m.RU.y;
            result.RU.z = -m.RU.z;
            result.Ru.x = -m.Ru.x;
            result.Ru.y = -m.Ru.y;
            result.Ru.z = -m.Ru.z;
        }

        public static Mat3 Add(Mat3 v1, Mat3 v2)
        {
            Mat3 mat;
            Add(ref v1, ref v2, out mat);
            return mat;
        }

        public static Mat3 Subtract(Mat3 v1, Mat3 v2)
        {
            Mat3 mat;
            Subtract(ref v1, ref v2, out mat);
            return mat;
        }

        public static Mat3 Multiply(Mat3 m, float s)
        {
            Mat3 mat;
            Multiply(ref m, s, out mat);
            return mat;
        }

        public static Mat3 Multiply(float s, Mat3 m)
        {
            Mat3 mat;
            Multiply(ref m, s, out mat);
            return mat;
        }

        public static Vec3 Multiply(Mat3 m, Vec3 v)
        {
            Vec3 vec;
            Multiply(ref m, ref v, out vec);
            return vec;
        }

        public static Vec3 Multiply(Vec3 v, Mat3 m)
        {
            Vec3 vec;
            Multiply(ref m, ref v, out vec);
            return vec;
        }

        public static Mat3 Multiply(Mat3 v1, Mat3 v2)
        {
            Mat3 mat;
            Multiply(ref v1, ref v2, out mat);
            return mat;
        }

        public static Mat3 Negate(Mat3 m)
        {
            Mat3 mat;
            Negate(ref m, out mat);
            return mat;
        }

        public bool Equals(Mat3 v, float epsilon)
        {
            if (!this.Rt.Equals(ref v.Rt, epsilon))
            {
                return false;
            }
            if (!this.RU.Equals(ref v.RU, epsilon))
            {
                return false;
            }
            if (!this.Ru.Equals(ref v.Ru, epsilon))
            {
                return false;
            }
            return true;
        }

        public bool Equals(ref Mat3 v, float epsilon)
        {
            if (!this.Rt.Equals(ref v.Rt, epsilon))
            {
                return false;
            }
            if (!this.RU.Equals(ref v.RU, epsilon))
            {
                return false;
            }
            if (!this.Ru.Equals(ref v.Ru, epsilon))
            {
                return false;
            }
            return true;
        }

        public float GetTrace()
        {
            return ((this.Rt.X + this.RU.Y) + this.Ru.Z);
        }

        public void Transpose()
        {
            float rZ = this.Rt.y;
            this.Rt.y = this.RU.x;
            this.RU.x = rZ;
            rZ = this.Rt.z;
            this.Rt.z = this.Ru.x;
            this.Ru.x = rZ;
            rZ = this.RU.z;
            this.RU.z = this.Ru.y;
            this.Ru.y = rZ;
        }

        public Mat3 GetTranspose()
        {
            Mat3 mat;
            mat.Rt.x = this.Rt.x;
            mat.Rt.y = this.RU.x;
            mat.Rt.z = this.Ru.x;
            mat.RU.x = this.Rt.y;
            mat.RU.y = this.RU.y;
            mat.RU.z = this.Ru.y;
            mat.Ru.x = this.Rt.z;
            mat.Ru.y = this.RU.z;
            mat.Ru.z = this.Ru.z;
            return mat;
        }

        public void GetTranspose(out Mat3 result)
        {
            result.Rt.x = this.Rt.x;
            result.Rt.y = this.RU.x;
            result.Rt.z = this.Ru.x;
            result.RU.x = this.Rt.y;
            result.RU.y = this.RU.y;
            result.RU.z = this.Ru.y;
            result.Ru.x = this.Rt.z;
            result.Ru.y = this.RU.z;
            result.Ru.z = this.Ru.z;
        }

        public bool Inverse()
        {
            Mat3 mat;
            mat.Rt.x = (this.RU.y * this.Ru.z) - (this.RU.z * this.Ru.y);
            mat.RU.x = (this.RU.z * this.Ru.x) - (this.RU.x * this.Ru.z);
            mat.Ru.x = (this.RU.x * this.Ru.y) - (this.RU.y * this.Ru.x);
            double num = ((this.Rt.x * mat.Rt.x) + (this.Rt.y * mat.RU.x)) + (this.Rt.z * mat.Ru.x);
            if (System.Math.Abs(num) < 1E-14)
            {
                return false;
            }
            double num2 = 1.0 / num;
            mat.Rt.y = (this.Rt.z * this.Ru.y) - (this.Rt.y * this.Ru.z);
            mat.Rt.z = (this.Rt.y * this.RU.z) - (this.Rt.z * this.RU.y);
            mat.RU.y = (this.Rt.x * this.Ru.z) - (this.Rt.z * this.Ru.x);
            mat.RU.z = (this.Rt.z * this.RU.x) - (this.Rt.x * this.RU.z);
            mat.Ru.y = (this.Rt.y * this.Ru.x) - (this.Rt.x * this.Ru.y);
            mat.Ru.z = (this.Rt.x * this.RU.y) - (this.Rt.y * this.RU.x);
            this.Rt.x = (float)(mat.Rt.x * num2);
            this.Rt.y = (float)(mat.Rt.y * num2);
            this.Rt.z = (float)(mat.Rt.z * num2);
            this.RU.x = (float)(mat.RU.x * num2);
            this.RU.y = (float)(mat.RU.y * num2);
            this.RU.z = (float)(mat.RU.z * num2);
            this.Ru.x = (float)(mat.Ru.x * num2);
            this.Ru.y = (float)(mat.Ru.y * num2);
            this.Ru.z = (float)(mat.Ru.z * num2);
            return true;
        }

        public Mat3 GetInverse()
        {
            Mat3 mat = this;
            mat.Inverse();
            return mat;
        }

        public void GetInverse(out Mat3 result)
        {
            result = this;
            result.Inverse();
        }

        public Angles ToAngles()
        {
            Angles angles;
            float ry = this.Ru.x;
            if (ry > 1f)
            {
                ry = 1f;
            }
            else if (ry < -1f)
            {
                ry = -1f;
            }
            double d = -System.Math.Asin((double)ry);
            if (System.Math.Cos(d) > 0.0009765625)
            {
                angles.roll = MathFunctions.RadToDeg((float)System.Math.Atan2((double)this.Ru.y, (double)this.Ru.z));
                angles.pitch = MathFunctions.RadToDeg((float)d);
                angles.yaw = MathFunctions.RadToDeg((float)System.Math.Atan2((double)this.RU.x, (double)this.Rt.x));
                return angles;
            }
            angles.roll = 0f;
            angles.pitch = MathFunctions.RadToDeg((float)d);
            angles.yaw = MathFunctions.RadToDeg((float)-System.Math.Atan2((double)this.Rt.y, (double)this.RU.y));
            return angles;
        }

        public void ToAngles(out Angles result)
        {
            float ry = this.Ru.x;
            if (ry > 1f)
            {
                ry = 1f;
            }
            else if (ry < -1f)
            {
                ry = -1f;
            }
            double d = -System.Math.Asin((double)ry);
            if (System.Math.Cos(d) > 0.0009765625)
            {
                result.roll = MathFunctions.RadToDeg((float)System.Math.Atan2((double)this.Ru.y, (double)this.Ru.z));
                result.pitch = MathFunctions.RadToDeg((float)d);
                result.yaw = MathFunctions.RadToDeg((float)System.Math.Atan2((double)this.RU.x, (double)this.Rt.x));
            }
            else
            {
                result.roll = 0f;
                result.pitch = MathFunctions.RadToDeg((float)d);
                result.yaw = MathFunctions.RadToDeg((float)-System.Math.Atan2((double)this.Rt.y, (double)this.RU.y));
            }
        }

        public Quat ToQuat()
        {
            Quat identity;
            float num;
            float num2;
            float num3 = (this.Rt.x + this.RU.y) + this.Ru.z;
            if (num3 > 0f)
            {
                num2 = num3 + 1f;
                num = MathFunctions.InvSqrt(num2) * 0.5f;
                identity.x = (this.RU.z - this.Ru.y) * num;
                identity.y = (this.Ru.x - this.Rt.z) * num;
                identity.z = (this.Rt.y - this.RU.x) * num;
                identity.w = num * num2;
                return identity;
            }
            int num4 = 0;
            if (this.RU.y > this.Rt.x)
            {
                num4 = 1;
            }
            if (this.Ru.z > this[num4, num4])
            {
                num4 = 2;
            }
            int num5 = num4 + 1;
            if (num5 > 2)
            {
                num5 = 0;
            }
            int num6 = num5 + 1;
            if (num6 > 2)
            {
                num6 = 0;
            }
            num2 = (this[num4, num4] - (this[num5, num5] + this[num6, num6])) + 1f;
            num = MathFunctions.InvSqrt(num2) * 0.5f;
            identity = Quat.Identity;
            identity[num4] = num * num2;
            identity[3] = (this[num5, num6] - this[num6, num5]) * num;
            identity[num5] = (this[num4, num5] + this[num5, num4]) * num;
            identity[num6] = (this[num4, num6] + this[num6, num4]) * num;
            return identity;
        }

        public void ToQuat(out Quat result)
        {
            float num;
            float num2;
            float num3 = (this.Rt.x + this.RU.y) + this.Ru.z;
            if (num3 > 0f)
            {
                num2 = num3 + 1f;
                num = MathFunctions.InvSqrt(num2) * 0.5f;
                result.x = (this.RU.z - this.Ru.y) * num;
                result.y = (this.Ru.x - this.Rt.z) * num;
                result.z = (this.Rt.y - this.RU.x) * num;
                result.w = num * num2;
            }
            else
            {
                int num4 = 0;
                if (this.RU.y > this.Rt.x)
                {
                    num4 = 1;
                }
                if (this.Ru.z > this[num4, num4])
                {
                    num4 = 2;
                }
                int num5 = num4 + 1;
                if (num5 > 2)
                {
                    num5 = 0;
                }
                int num6 = num5 + 1;
                if (num6 > 2)
                {
                    num6 = 0;
                }
                num2 = (this[num4, num4] - (this[num5, num5] + this[num6, num6])) + 1f;
                num = MathFunctions.InvSqrt(num2) * 0.5f;
                result = Quat.Identity;
                result[num4] = num * num2;
                result[3] = (this[num5, num6] - this[num6, num5]) * num;
                result[num5] = (this[num4, num5] + this[num5, num4]) * num;
                result[num6] = (this[num4, num6] + this[num6, num4]) * num;
            }
        }

        public Mat4 ToMat4()
        {
            Mat4 mat;
            mat.Rp.x = this.Rt.x;
            mat.Rp.y = this.Rt.y;
            mat.Rp.z = this.Rt.z;
            mat.Rp.w = 0f;
            mat.RQ.x = this.RU.x;
            mat.RQ.y = this.RU.y;
            mat.RQ.z = this.RU.z;
            mat.RQ.w = 0f;
            mat.Rq.x = this.Ru.x;
            mat.Rq.y = this.Ru.y;
            mat.Rq.z = this.Ru.z;
            mat.Rq.w = 0f;
            mat.RR.x = 0f;
            mat.RR.y = 0f;
            mat.RR.z = 0f;
            mat.RR.w = 1f;
            return mat;
        }

        public void ToMat4(out Mat4 result)
        {
            result.Rp.x = this.Rt.x;
            result.Rp.y = this.Rt.y;
            result.Rp.z = this.Rt.z;
            result.Rp.w = 0f;
            result.RQ.x = this.RU.x;
            result.RQ.y = this.RU.y;
            result.RQ.z = this.RU.z;
            result.RQ.w = 0f;
            result.Rq.x = this.Ru.x;
            result.Rq.y = this.Ru.y;
            result.Rq.z = this.Ru.z;
            result.Rq.w = 0f;
            result.RR.x = 0f;
            result.RR.y = 0f;
            result.RR.z = 0f;
            result.RR.w = 1f;
        }

        public override bool Equals(object obj)
        {
            return ((obj is Mat3) && (this == ((Mat3)obj)));
        }

        public static bool operator ==(Mat3 v1, Mat3 v2)
        {
            return (((v1.Rt == v2.Rt) && (v1.RU == v2.RU)) && (v1.Ru == v2.Ru));
        }

        public static bool operator !=(Mat3 v1, Mat3 v2)
        {
            if (!(v1.Rt != v2.Rt) && !(v1.RU != v2.RU))
            {
                return (v1.Ru != v2.Ru);
            }
            return true;
        }

        public unsafe float this[int row, int column]
        {
            get
            {
                if ((row < 0) || (row > 2))
                {
                    throw new ArgumentOutOfRangeException("row");
                }
                if ((column < 0) || (column > 2))
                {
                    throw new ArgumentOutOfRangeException("column");
                }
                fixed (float* numRef = &this.Rt.x)
                {
                    return numRef[((row * 3) + column) * 4];
                }
            }
            set
            {
                if ((row < 0) || (row > 2))
                {
                    throw new ArgumentOutOfRangeException("row");
                }
                if ((column < 0) || (column > 2))
                {
                    throw new ArgumentOutOfRangeException("column");
                }
                fixed (float* numRef = &this.Rt.x)
                {
                    numRef[((row * 3) + column) * 4] = value;
                }
            }
        }
        public static Mat3 Parse(string text)
        {
            Mat3 mat;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 9)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 9 parts separated by spaces.", text));
            }
            try
            {
                mat = new Mat3(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]), float.Parse(strArray[4]), float.Parse(strArray[5]), float.Parse(strArray[6]), float.Parse(strArray[7]), float.Parse(strArray[8]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return mat;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.Rt.ToString(), this.RU.ToString(), this.Ru.ToString());
        }

        [Browsable(false)]
        public Vec3 Item0
        {
            get
            {
                return this.Rt;
            }
        }
        [Browsable(false)]
        public Vec3 Item1
        {
            get
            {
                return this.RU;
            }
        }
        [Browsable(false)]
        public Vec3 Item2
        {
            get
            {
                return this.Ru;
            }
        }
        public static Mat3 FromScale(Vec3 scale)
        {
            Mat3 mat;
            mat.Rt.x = scale.x;
            mat.Rt.y = 0f;
            mat.Rt.z = 0f;
            mat.RU.x = 0f;
            mat.RU.y = scale.y;
            mat.RU.z = 0f;
            mat.Ru.x = 0f;
            mat.Ru.y = 0f;
            mat.Ru.z = scale.z;
            return mat;
        }

        public static void FromScale(ref Vec3 scale, out Mat3 result)
        {
            result.Rt.x = scale.x;
            result.Rt.y = 0f;
            result.Rt.z = 0f;
            result.RU.x = 0f;
            result.RU.y = scale.y;
            result.RU.z = 0f;
            result.Ru.x = 0f;
            result.Ru.y = 0f;
            result.Ru.z = scale.z;
        }

        public static Mat3 FromRotateByX(Radian angle)
        {
            Mat3 mat;
            float num = MathFunctions.Sin((float)angle);
            float num2 = MathFunctions.Cos((float)angle);
            mat.Rt.x = 1f;
            mat.Rt.y = 0f;
            mat.Rt.z = 0f;
            mat.RU.x = 0f;
            mat.RU.y = num2;
            mat.RU.z = -num;
            mat.Ru.x = 0f;
            mat.Ru.y = num;
            mat.Ru.z = num2;
            return mat;
        }

        public static void FromRotateByX(Radian angle, out Mat3 result)
        {
            float num = MathFunctions.Sin((float)angle);
            float num2 = MathFunctions.Cos((float)angle);
            result.Rt.x = 1f;
            result.Rt.y = 0f;
            result.Rt.z = 0f;
            result.RU.x = 0f;
            result.RU.y = num2;
            result.RU.z = -num;
            result.Ru.x = 0f;
            result.Ru.y = num;
            result.Ru.z = num2;
        }

        public static Mat3 FromRotateByY(Radian angle)
        {
            Mat3 mat;
            float num = MathFunctions.Sin((float)angle);
            float num2 = MathFunctions.Cos((float)angle);
            mat.Rt.x = num2;
            mat.Rt.y = 0f;
            mat.Rt.z = num;
            mat.RU.x = 0f;
            mat.RU.y = 1f;
            mat.RU.z = 0f;
            mat.Ru.x = -num;
            mat.Ru.y = 0f;
            mat.Ru.z = num2;
            return mat;
        }

        public static void FromRotateByY(Radian angle, out Mat3 result)
        {
            float num = MathFunctions.Sin((float)angle);
            float num2 = MathFunctions.Cos((float)angle);
            result.Rt.x = num2;
            result.Rt.y = 0f;
            result.Rt.z = num;
            result.RU.x = 0f;
            result.RU.y = 1f;
            result.RU.z = 0f;
            result.Ru.x = -num;
            result.Ru.y = 0f;
            result.Ru.z = num2;
        }

        public static Mat3 FromRotateByZ(Radian angle)
        {
            Mat3 mat;
            float num = MathFunctions.Sin((float)angle);
            float num2 = MathFunctions.Cos((float)angle);
            mat.Rt.x = num2;
            mat.Rt.y = -num;
            mat.Rt.z = 0f;
            mat.RU.x = num;
            mat.RU.y = num2;
            mat.RU.z = 0f;
            mat.Ru.x = 0f;
            mat.Ru.y = 0f;
            mat.Ru.z = 1f;
            return mat;
        }

        public static void FromRotateByZ(Radian angle, out Mat3 result)
        {
            float num = MathFunctions.Sin((float)angle);
            float num2 = MathFunctions.Cos((float)angle);
            result.Rt.x = num2;
            result.Rt.y = -num;
            result.Rt.z = 0f;
            result.RU.x = num;
            result.RU.y = num2;
            result.RU.z = 0f;
            result.Ru.x = 0f;
            result.Ru.y = 0f;
            result.Ru.z = 1f;
        }
    }

}
