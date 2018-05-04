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
    public struct Mat4
    {
        public static readonly Mat4 Zero;
        public static readonly Mat4 Identity;
        internal Vec4 Rp;
        internal Vec4 RQ;
        internal Vec4 Rq;
        internal Vec4 RR;
        public Mat4(float xx, float xy, float xz, float xw, float yx, float yy, float yz, float yw, float zx, float zy, float zz, float zw, float wx, float wy, float wz, float ww)
        {
            this.Rp = new Vec4(xx, xy, xz, xw);
            this.RQ = new Vec4(yx, yy, yz, yw);
            this.Rq = new Vec4(zx, zy, zz, zw);
            this.RR = new Vec4(wx, wy, wz, ww);
        }

        public Mat4(Vec4 x, Vec4 y, Vec4 z, Vec4 w)
        {
            this.Rp = x;
            this.RQ = y;
            this.Rq = z;
            this.RR = z;
        }

        public Mat4(Mat4 source)
        {
            this.Rp = source.Rp;
            this.RQ = source.RQ;
            this.Rq = source.Rq;
            this.RR = source.RR;
        }

        public Mat4(Mat3 rotation, Vec3 translation)
        {
            this.Rp.x = rotation.Rt.x;
            this.Rp.y = rotation.Rt.y;
            this.Rp.z = rotation.Rt.z;
            this.Rp.w = 0f;
            this.RQ.x = rotation.RU.x;
            this.RQ.y = rotation.RU.y;
            this.RQ.z = rotation.RU.z;
            this.RQ.w = 0f;
            this.Rq.x = rotation.Ru.x;
            this.Rq.y = rotation.Ru.y;
            this.Rq.z = rotation.Ru.z;
            this.Rq.w = 0f;
            this.RR.x = translation.x;
            this.RR.y = translation.y;
            this.RR.z = translation.z;
            this.RR.w = 1f;
        }

        public Mat4(float[] array)
        {
            this.Rp = new Vec4(array[0], array[1], array[2], array[3]);
            this.RQ = new Vec4(array[4], array[5], array[6], array[7]);
            this.Rq = new Vec4(array[8], array[9], array[10], array[11]);
            this.RR = new Vec4(array[12], array[13], array[14], array[15]);
        }

        static Mat4()
        {
            Zero = new Mat4(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            Identity = new Mat4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
        }

        public unsafe Vec4 this[int index]
        {
            get
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (Vec4* vecRef = &this.Rp)
                {
                    return vecRef[index];
                }
            }
            set
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (Vec4* vecRef = &this.Rp)
                {
                    vecRef[index] = value;
                }
            }
        }
        public override int GetHashCode()
        {
            return (((this.Rp.GetHashCode() ^ this.RQ.GetHashCode()) ^ this.Rq.GetHashCode()) ^ this.RR.GetHashCode());
        }

        public static Mat4 operator +(Mat4 v1, Mat4 v2)
        {
            Mat4 mat;
            mat.Rp.x = v1.Rp.x + v2.Rp.x;
            mat.Rp.y = v1.Rp.y + v2.Rp.y;
            mat.Rp.z = v1.Rp.z + v2.Rp.z;
            mat.Rp.w = v1.Rp.w + v2.Rp.w;
            mat.RQ.x = v1.RQ.x + v2.RQ.x;
            mat.RQ.y = v1.RQ.y + v2.RQ.y;
            mat.RQ.z = v1.RQ.z + v2.RQ.z;
            mat.RQ.w = v1.RQ.w + v2.RQ.w;
            mat.Rq.x = v1.Rq.x + v2.Rq.x;
            mat.Rq.y = v1.Rq.y + v2.Rq.y;
            mat.Rq.z = v1.Rq.z + v2.Rq.z;
            mat.Rq.w = v1.Rq.w + v2.Rq.w;
            mat.RR.x = v1.RR.x + v2.RR.x;
            mat.RR.y = v1.RR.y + v2.RR.y;
            mat.RR.z = v1.RR.z + v2.RR.z;
            mat.RR.w = v1.RR.w + v2.RR.w;
            return mat;
        }

        public static Mat4 operator -(Mat4 v1, Mat4 v2)
        {
            Mat4 mat;
            mat.Rp.x = v1.Rp.x - v2.Rp.x;
            mat.Rp.y = v1.Rp.y - v2.Rp.y;
            mat.Rp.z = v1.Rp.z - v2.Rp.z;
            mat.Rp.w = v1.Rp.w - v2.Rp.w;
            mat.RQ.x = v1.RQ.x - v2.RQ.x;
            mat.RQ.y = v1.RQ.y - v2.RQ.y;
            mat.RQ.z = v1.RQ.z - v2.RQ.z;
            mat.RQ.w = v1.RQ.w - v2.RQ.w;
            mat.Rq.x = v1.Rq.x - v2.Rq.x;
            mat.Rq.y = v1.Rq.y - v2.Rq.y;
            mat.Rq.z = v1.Rq.z - v2.Rq.z;
            mat.Rq.w = v1.Rq.w - v2.Rq.w;
            mat.RR.x = v1.RR.x - v2.RR.x;
            mat.RR.y = v1.RR.y - v2.RR.y;
            mat.RR.z = v1.RR.z - v2.RR.z;
            mat.RR.w = v1.RR.w - v2.RR.w;
            return mat;
        }

        public static Mat4 operator *(Mat4 m, float s)
        {
            Mat4 mat;
            mat.Rp.x = m.Rp.x * s;
            mat.Rp.y = m.Rp.y * s;
            mat.Rp.z = m.Rp.z * s;
            mat.Rp.w = m.Rp.w * s;
            mat.RQ.x = m.RQ.x * s;
            mat.RQ.y = m.RQ.y * s;
            mat.RQ.z = m.RQ.z * s;
            mat.RQ.w = m.RQ.w * s;
            mat.Rq.x = m.Rq.x * s;
            mat.Rq.y = m.Rq.y * s;
            mat.Rq.z = m.Rq.z * s;
            mat.Rq.w = m.Rq.w * s;
            mat.RR.x = m.RR.x * s;
            mat.RR.y = m.RR.y * s;
            mat.RR.z = m.RR.z * s;
            mat.RR.w = m.RR.w * s;
            return mat;
        }

        public static Mat4 operator *(float s, Mat4 m)
        {
            Mat4 mat;
            mat.Rp.x = m.Rp.x * s;
            mat.Rp.y = m.Rp.y * s;
            mat.Rp.z = m.Rp.z * s;
            mat.Rp.w = m.Rp.w * s;
            mat.RQ.x = m.RQ.x * s;
            mat.RQ.y = m.RQ.y * s;
            mat.RQ.z = m.RQ.z * s;
            mat.RQ.w = m.RQ.w * s;
            mat.Rq.x = m.Rq.x * s;
            mat.Rq.y = m.Rq.y * s;
            mat.Rq.z = m.Rq.z * s;
            mat.Rq.w = m.Rq.w * s;
            mat.RR.x = m.RR.x * s;
            mat.RR.y = m.RR.y * s;
            mat.RR.z = m.RR.z * s;
            mat.RR.w = m.RR.w * s;
            return mat;
        }

        public static Ray operator *(Mat4 m, Ray r)
        {
            Ray ray;
            Vec3 vec;
            Vec3 vec2;
            Multiply(ref r.ri, ref m, out ray.ri);
            Vec3.Add(ref r.ri, ref r.rJ, out vec);
            Multiply(ref vec, ref m, out vec2);
            Vec3.Subtract(ref vec2, ref ray.ri, out ray.rJ);
            return ray;
        }

        public static Ray operator *(Ray r, Mat4 m)
        {
            Ray ray;
            Vec3 vec;
            Vec3 vec2;
            Multiply(ref r.ri, ref m, out ray.ri);
            Vec3.Add(ref r.ri, ref r.rJ, out vec);
            Multiply(ref vec, ref m, out vec2);
            Vec3.Subtract(ref vec2, ref ray.ri, out ray.rJ);
            return ray;
        }

        public static Vec3 operator *(Mat4 m, Vec3 v)
        {
            Vec3 vec;
            vec.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + m.RR.x;
            vec.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + m.RR.y;
            vec.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + m.RR.z;
            return vec;
        }

        public static Vec3 operator *(Vec3 v, Mat4 m)
        {
            Vec3 vec;
            vec.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + m.RR.x;
            vec.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + m.RR.y;
            vec.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + m.RR.z;
            return vec;
        }

        public static Vec4 operator *(Mat4 m, Vec4 v)
        {
            Vec4 vec;
            vec.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + (m.RR.x * v.W);
            vec.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + (m.RR.y * v.W);
            vec.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + (m.RR.z * v.W);
            vec.w = (((m.Rp.w * v.X) + (m.RQ.w * v.Y)) + (m.Rq.w * v.Z)) + (m.RR.w * v.W);
            return vec;
        }

        public static Vec4 operator *(Vec4 v, Mat4 m)
        {
            Vec4 vec;
            vec.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + (m.RR.x * v.W);
            vec.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + (m.RR.y * v.W);
            vec.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + (m.RR.z * v.W);
            vec.w = (((m.Rp.w * v.X) + (m.RQ.w * v.Y)) + (m.Rq.w * v.Z)) + (m.RR.w * v.W);
            return vec;
        }

        public static Mat4 operator *(Mat4 v1, Mat4 v2)
        {
            Mat4 mat;
            mat.Rp.x = (((v1.Rp.x * v2.Rp.x) + (v1.RQ.x * v2.Rp.y)) + (v1.Rq.x * v2.Rp.z)) + (v1.RR.x * v2.Rp.w);
            mat.Rp.y = (((v1.Rp.y * v2.Rp.x) + (v1.RQ.y * v2.Rp.y)) + (v1.Rq.y * v2.Rp.z)) + (v1.RR.y * v2.Rp.w);
            mat.Rp.z = (((v1.Rp.z * v2.Rp.x) + (v1.RQ.z * v2.Rp.y)) + (v1.Rq.z * v2.Rp.z)) + (v1.RR.z * v2.Rp.w);
            mat.Rp.w = (((v1.Rp.w * v2.Rp.x) + (v1.RQ.w * v2.Rp.y)) + (v1.Rq.w * v2.Rp.z)) + (v1.RR.w * v2.Rp.w);
            mat.RQ.x = (((v1.Rp.x * v2.RQ.x) + (v1.RQ.x * v2.RQ.y)) + (v1.Rq.x * v2.RQ.z)) + (v1.RR.x * v2.RQ.w);
            mat.RQ.y = (((v1.Rp.y * v2.RQ.x) + (v1.RQ.y * v2.RQ.y)) + (v1.Rq.y * v2.RQ.z)) + (v1.RR.y * v2.RQ.w);
            mat.RQ.z = (((v1.Rp.z * v2.RQ.x) + (v1.RQ.z * v2.RQ.y)) + (v1.Rq.z * v2.RQ.z)) + (v1.RR.z * v2.RQ.w);
            mat.RQ.w = (((v1.Rp.w * v2.RQ.x) + (v1.RQ.w * v2.RQ.y)) + (v1.Rq.w * v2.RQ.z)) + (v1.RR.w * v2.RQ.w);
            mat.Rq.x = (((v1.Rp.x * v2.Rq.x) + (v1.RQ.x * v2.Rq.y)) + (v1.Rq.x * v2.Rq.z)) + (v1.RR.x * v2.Rq.w);
            mat.Rq.y = (((v1.Rp.y * v2.Rq.x) + (v1.RQ.y * v2.Rq.y)) + (v1.Rq.y * v2.Rq.z)) + (v1.RR.y * v2.Rq.w);
            mat.Rq.z = (((v1.Rp.z * v2.Rq.x) + (v1.RQ.z * v2.Rq.y)) + (v1.Rq.z * v2.Rq.z)) + (v1.RR.z * v2.Rq.w);
            mat.Rq.w = (((v1.Rp.w * v2.Rq.x) + (v1.RQ.w * v2.Rq.y)) + (v1.Rq.w * v2.Rq.z)) + (v1.RR.w * v2.Rq.w);
            mat.RR.x = (((v1.Rp.x * v2.RR.x) + (v1.RQ.x * v2.RR.y)) + (v1.Rq.x * v2.RR.z)) + (v1.RR.x * v2.RR.w);
            mat.RR.y = (((v1.Rp.y * v2.RR.x) + (v1.RQ.y * v2.RR.y)) + (v1.Rq.y * v2.RR.z)) + (v1.RR.y * v2.RR.w);
            mat.RR.z = (((v1.Rp.z * v2.RR.x) + (v1.RQ.z * v2.RR.y)) + (v1.Rq.z * v2.RR.z)) + (v1.RR.z * v2.RR.w);
            mat.RR.w = (((v1.Rp.w * v2.RR.x) + (v1.RQ.w * v2.RR.y)) + (v1.Rq.w * v2.RR.z)) + (v1.RR.w * v2.RR.w);
            return mat;
        }

        public static Mat4 operator -(Mat4 v)
        {
            Mat4 mat;
            mat.Rp.x = -v.Rp.x;
            mat.Rp.y = -v.Rp.y;
            mat.Rp.z = -v.Rp.z;
            mat.Rp.w = -v.Rp.w;
            mat.RQ.x = -v.RQ.x;
            mat.RQ.y = -v.RQ.y;
            mat.RQ.z = -v.RQ.z;
            mat.RQ.w = -v.RQ.w;
            mat.Rq.x = -v.Rq.x;
            mat.Rq.y = -v.Rq.y;
            mat.Rq.z = -v.Rq.z;
            mat.Rq.w = -v.Rq.w;
            mat.RR.x = -v.RR.x;
            mat.RR.y = -v.RR.y;
            mat.RR.z = -v.RR.z;
            mat.RR.w = -v.RR.w;
            return mat;
        }

        public static void Add(ref Mat4 v1, ref Mat4 v2, out Mat4 result)
        {
            result.Rp.x = v1.Rp.x + v2.Rp.x;
            result.Rp.y = v1.Rp.y + v2.Rp.y;
            result.Rp.z = v1.Rp.z + v2.Rp.z;
            result.Rp.w = v1.Rp.w + v2.Rp.w;
            result.RQ.x = v1.RQ.x + v2.RQ.x;
            result.RQ.y = v1.RQ.y + v2.RQ.y;
            result.RQ.z = v1.RQ.z + v2.RQ.z;
            result.RQ.w = v1.RQ.w + v2.RQ.w;
            result.Rq.x = v1.Rq.x + v2.Rq.x;
            result.Rq.y = v1.Rq.y + v2.Rq.y;
            result.Rq.z = v1.Rq.z + v2.Rq.z;
            result.Rq.w = v1.Rq.w + v2.Rq.w;
            result.RR.x = v1.RR.x + v2.RR.x;
            result.RR.y = v1.RR.y + v2.RR.y;
            result.RR.z = v1.RR.z + v2.RR.z;
            result.RR.w = v1.RR.w + v2.RR.w;
        }

        public static void Subtract(ref Mat4 v1, ref Mat4 v2, out Mat4 result)
        {
            result.Rp.x = v1.Rp.x - v2.Rp.x;
            result.Rp.y = v1.Rp.y - v2.Rp.y;
            result.Rp.z = v1.Rp.z - v2.Rp.z;
            result.Rp.w = v1.Rp.w - v2.Rp.w;
            result.RQ.x = v1.RQ.x - v2.RQ.x;
            result.RQ.y = v1.RQ.y - v2.RQ.y;
            result.RQ.z = v1.RQ.z - v2.RQ.z;
            result.RQ.w = v1.RQ.w - v2.RQ.w;
            result.Rq.x = v1.Rq.x - v2.Rq.x;
            result.Rq.y = v1.Rq.y - v2.Rq.y;
            result.Rq.z = v1.Rq.z - v2.Rq.z;
            result.Rq.w = v1.Rq.w - v2.Rq.w;
            result.RR.x = v1.RR.x - v2.RR.x;
            result.RR.y = v1.RR.y - v2.RR.y;
            result.RR.z = v1.RR.z - v2.RR.z;
            result.RR.w = v1.RR.w - v2.RR.w;
        }

        public static void Multiply(ref Mat4 m, float s, out Mat4 result)
        {
            result.Rp.x = m.Rp.x * s;
            result.Rp.y = m.Rp.y * s;
            result.Rp.z = m.Rp.z * s;
            result.Rp.w = m.Rp.w * s;
            result.RQ.x = m.RQ.x * s;
            result.RQ.y = m.RQ.y * s;
            result.RQ.z = m.RQ.z * s;
            result.RQ.w = m.RQ.w * s;
            result.Rq.x = m.Rq.x * s;
            result.Rq.y = m.Rq.y * s;
            result.Rq.z = m.Rq.z * s;
            result.Rq.w = m.Rq.w * s;
            result.RR.x = m.RR.x * s;
            result.RR.y = m.RR.y * s;
            result.RR.z = m.RR.z * s;
            result.RR.w = m.RR.w * s;
        }

        public static void Multiply(float s, ref Mat4 m, out Mat4 result)
        {
            result.Rp.x = m.Rp.x * s;
            result.Rp.y = m.Rp.y * s;
            result.Rp.z = m.Rp.z * s;
            result.Rp.w = m.Rp.w * s;
            result.RQ.x = m.RQ.x * s;
            result.RQ.y = m.RQ.y * s;
            result.RQ.z = m.RQ.z * s;
            result.RQ.w = m.RQ.w * s;
            result.Rq.x = m.Rq.x * s;
            result.Rq.y = m.Rq.y * s;
            result.Rq.z = m.Rq.z * s;
            result.Rq.w = m.Rq.w * s;
            result.RR.x = m.RR.x * s;
            result.RR.y = m.RR.y * s;
            result.RR.z = m.RR.z * s;
            result.RR.w = m.RR.w * s;
        }

        public static void Multiply(ref Mat4 m, ref Ray r, out Ray result)
        {
            Vec3 vec;
            Vec3 vec2;
            Multiply(ref r.ri, ref m, out result.ri);
            Vec3.Add(ref r.ri, ref r.rJ, out vec);
            Multiply(ref vec, ref m, out vec2);
            Vec3.Subtract(ref vec2, ref result.ri, out result.rJ);
        }

        public static void Multiply(ref Ray r, ref Mat4 m, out Ray result)
        {
            Vec3 vec;
            Vec3 vec2;
            Multiply(ref r.ri, ref m, out result.ri);
            Vec3.Add(ref r.ri, ref r.rJ, out vec);
            Multiply(ref vec, ref m, out vec2);
            Vec3.Subtract(ref vec2, ref result.ri, out result.rJ);
        }

        public static void Multiply(ref Mat4 m, ref Vec3 v, out Vec3 result)
        {
            result.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + m.RR.x;
            result.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + m.RR.y;
            result.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + m.RR.z;
        }

        public static void Multiply(ref Vec3 v, ref Mat4 m, out Vec3 result)
        {
            result.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + m.RR.x;
            result.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + m.RR.y;
            result.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + m.RR.z;
        }

        public static void Multiply(ref Mat4 m, ref Vec4 v, out Vec4 result)
        {
            result.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + (m.RR.x * v.W);
            result.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + (m.RR.y * v.W);
            result.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + (m.RR.z * v.W);
            result.w = (((m.Rp.w * v.X) + (m.RQ.w * v.Y)) + (m.Rq.w * v.Z)) + (m.RR.w * v.W);
        }

        public static void Multiply(ref Vec4 v, ref Mat4 m, out Vec4 result)
        {
            result.x = (((m.Rp.x * v.X) + (m.RQ.x * v.Y)) + (m.Rq.x * v.Z)) + (m.RR.x * v.W);
            result.y = (((m.Rp.y * v.X) + (m.RQ.y * v.Y)) + (m.Rq.y * v.Z)) + (m.RR.y * v.W);
            result.z = (((m.Rp.z * v.X) + (m.RQ.z * v.Y)) + (m.Rq.z * v.Z)) + (m.RR.z * v.W);
            result.w = (((m.Rp.w * v.X) + (m.RQ.w * v.Y)) + (m.Rq.w * v.Z)) + (m.RR.w * v.W);
        }

        public static void Multiply(ref Mat4 v1, ref Mat4 v2, out Mat4 result)
        {
            result.Rp.x = (((v1.Rp.x * v2.Rp.x) + (v1.RQ.x * v2.Rp.y)) + (v1.Rq.x * v2.Rp.z)) + (v1.RR.x * v2.Rp.w);
            result.Rp.y = (((v1.Rp.y * v2.Rp.x) + (v1.RQ.y * v2.Rp.y)) + (v1.Rq.y * v2.Rp.z)) + (v1.RR.y * v2.Rp.w);
            result.Rp.z = (((v1.Rp.z * v2.Rp.x) + (v1.RQ.z * v2.Rp.y)) + (v1.Rq.z * v2.Rp.z)) + (v1.RR.z * v2.Rp.w);
            result.Rp.w = (((v1.Rp.w * v2.Rp.x) + (v1.RQ.w * v2.Rp.y)) + (v1.Rq.w * v2.Rp.z)) + (v1.RR.w * v2.Rp.w);
            result.RQ.x = (((v1.Rp.x * v2.RQ.x) + (v1.RQ.x * v2.RQ.y)) + (v1.Rq.x * v2.RQ.z)) + (v1.RR.x * v2.RQ.w);
            result.RQ.y = (((v1.Rp.y * v2.RQ.x) + (v1.RQ.y * v2.RQ.y)) + (v1.Rq.y * v2.RQ.z)) + (v1.RR.y * v2.RQ.w);
            result.RQ.z = (((v1.Rp.z * v2.RQ.x) + (v1.RQ.z * v2.RQ.y)) + (v1.Rq.z * v2.RQ.z)) + (v1.RR.z * v2.RQ.w);
            result.RQ.w = (((v1.Rp.w * v2.RQ.x) + (v1.RQ.w * v2.RQ.y)) + (v1.Rq.w * v2.RQ.z)) + (v1.RR.w * v2.RQ.w);
            result.Rq.x = (((v1.Rp.x * v2.Rq.x) + (v1.RQ.x * v2.Rq.y)) + (v1.Rq.x * v2.Rq.z)) + (v1.RR.x * v2.Rq.w);
            result.Rq.y = (((v1.Rp.y * v2.Rq.x) + (v1.RQ.y * v2.Rq.y)) + (v1.Rq.y * v2.Rq.z)) + (v1.RR.y * v2.Rq.w);
            result.Rq.z = (((v1.Rp.z * v2.Rq.x) + (v1.RQ.z * v2.Rq.y)) + (v1.Rq.z * v2.Rq.z)) + (v1.RR.z * v2.Rq.w);
            result.Rq.w = (((v1.Rp.w * v2.Rq.x) + (v1.RQ.w * v2.Rq.y)) + (v1.Rq.w * v2.Rq.z)) + (v1.RR.w * v2.Rq.w);
            result.RR.x = (((v1.Rp.x * v2.RR.x) + (v1.RQ.x * v2.RR.y)) + (v1.Rq.x * v2.RR.z)) + (v1.RR.x * v2.RR.w);
            result.RR.y = (((v1.Rp.y * v2.RR.x) + (v1.RQ.y * v2.RR.y)) + (v1.Rq.y * v2.RR.z)) + (v1.RR.y * v2.RR.w);
            result.RR.z = (((v1.Rp.z * v2.RR.x) + (v1.RQ.z * v2.RR.y)) + (v1.Rq.z * v2.RR.z)) + (v1.RR.z * v2.RR.w);
            result.RR.w = (((v1.Rp.w * v2.RR.x) + (v1.RQ.w * v2.RR.y)) + (v1.Rq.w * v2.RR.z)) + (v1.RR.w * v2.RR.w);
        }

        public static void Negate(ref Mat4 m, out Mat4 result)
        {
            result.Rp.x = -m.Rp.x;
            result.Rp.y = -m.Rp.y;
            result.Rp.z = -m.Rp.z;
            result.Rp.w = -m.Rp.w;
            result.RQ.x = -m.RQ.x;
            result.RQ.y = -m.RQ.y;
            result.RQ.z = -m.RQ.z;
            result.RQ.w = -m.RQ.w;
            result.Rq.x = -m.Rq.x;
            result.Rq.y = -m.Rq.y;
            result.Rq.z = -m.Rq.z;
            result.Rq.w = -m.Rq.w;
            result.RR.x = -m.RR.x;
            result.RR.y = -m.RR.y;
            result.RR.z = -m.RR.z;
            result.RR.w = -m.RR.w;
        }

        public static Mat4 Add(Mat4 v1, Mat4 v2)
        {
            Mat4 mat;
            Add(ref v1, ref v2, out mat);
            return mat;
        }

        public static Mat4 Subtract(Mat4 v1, Mat4 v2)
        {
            Mat4 mat;
            Subtract(ref v1, ref v2, out mat);
            return mat;
        }

        public static Mat4 Multiply(Mat4 m, float s)
        {
            Mat4 mat;
            Multiply(ref m, s, out mat);
            return mat;
        }

        public static Mat4 Multiply(float s, Mat4 m)
        {
            Mat4 mat;
            Multiply(ref m, s, out mat);
            return mat;
        }

        public static Ray Multiply(Mat4 m, Ray r)
        {
            Ray ray;
            Multiply(ref m, ref r, out ray);
            return ray;
        }

        public static Ray Multiply(Ray r, Mat4 m)
        {
            Ray ray;
            Multiply(ref m, ref r, out ray);
            return ray;
        }

        public static Vec3 Multiply(Mat4 m, Vec3 v)
        {
            Vec3 vec;
            Multiply(ref m, ref v, out vec);
            return vec;
        }

        public static Vec3 Multiply(Vec3 v, Mat4 m)
        {
            Vec3 vec;
            Multiply(ref m, ref v, out vec);
            return vec;
        }

        public static Vec4 Multiply(Mat4 m, Vec4 v)
        {
            Vec4 vec;
            Multiply(ref m, ref v, out vec);
            return vec;
        }

        public static Vec4 Multiply(Vec4 v, Mat4 m)
        {
            Vec4 vec;
            Multiply(ref m, ref v, out vec);
            return vec;
        }

        public static Mat4 Multiply(Mat4 v1, Mat4 v2)
        {
            Mat4 mat;
            Multiply(ref v1, ref v2, out mat);
            return mat;
        }

        public static Mat4 Negate(Mat4 m)
        {
            Mat4 mat;
            Negate(ref m, out mat);
            return mat;
        }

        public float GetTrace()
        {
            return (((this.Rp.X + this.RQ.Y) + this.Rq.Z) + this.RR.W);
        }

        public void Transpose()
        {
            float rv = this.Rp.y;
            this.Rp.y = this.RQ.x;
            this.RQ.x = rv;
            rv = this.Rp.z;
            this.Rp.z = this.Rq.x;
            this.Rq.x = rv;
            rv = this.Rp.w;
            this.Rp.w = this.RR.x;
            this.RR.x = rv;
            rv = this.RQ.z;
            this.RQ.z = this.Rq.y;
            this.Rq.y = rv;
            rv = this.RQ.w;
            this.RQ.w = this.RR.y;
            this.RR.y = rv;
            rv = this.Rq.w;
            this.Rq.w = this.RR.z;
            this.RR.z = rv;
        }

        public Mat4 GetTranspose()
        {
            Mat4 mat = this;
            mat.Transpose();
            return mat;
        }

        public void GetTranspose(out Mat4 result)
        {
            result = this;
            result.Transpose();
        }

        public bool Inverse()
        {
            float num3 = (this.Rp.x * this.RQ.y) - (this.Rp.y * this.RQ.x);
            float num4 = (this.Rp.x * this.RQ.z) - (this.Rp.z * this.RQ.x);
            float num5 = (this.Rp.x * this.RQ.w) - (this.Rp.w * this.RQ.x);
            float num6 = (this.Rp.y * this.RQ.z) - (this.Rp.z * this.RQ.y);
            float num7 = (this.Rp.y * this.RQ.w) - (this.Rp.w * this.RQ.y);
            float num8 = (this.Rp.z * this.RQ.w) - (this.Rp.w * this.RQ.z);
            float num9 = ((this.Rq.x * num6) - (this.Rq.y * num4)) + (this.Rq.z * num3);
            float num10 = ((this.Rq.x * num7) - (this.Rq.y * num5)) + (this.Rq.w * num3);
            float num11 = ((this.Rq.x * num8) - (this.Rq.z * num5)) + (this.Rq.w * num4);
            float num12 = ((this.Rq.y * num8) - (this.Rq.z * num7)) + (this.Rq.w * num6);
            double num = (((-num12 * this.RR.x) + (num11 * this.RR.y)) - (num10 * this.RR.z)) + (num9 * this.RR.w);
            if (System.Math.Abs(num) < 1E-14)
            {
                return false;
            }
            double num2 = 1.0 / num;
            float num13 = (this.Rp.x * this.RR.y) - (this.Rp.y * this.RR.x);
            float num14 = (this.Rp.x * this.RR.z) - (this.Rp.z * this.RR.x);
            float num15 = (this.Rp.x * this.RR.w) - (this.Rp.w * this.RR.x);
            float num16 = (this.Rp.y * this.RR.z) - (this.Rp.z * this.RR.y);
            float num17 = (this.Rp.y * this.RR.w) - (this.Rp.w * this.RR.y);
            float num18 = (this.Rp.z * this.RR.w) - (this.Rp.w * this.RR.z);
            float num19 = (this.RQ.x * this.RR.y) - (this.RQ.y * this.RR.x);
            float num20 = (this.RQ.x * this.RR.z) - (this.RQ.z * this.RR.x);
            float num21 = (this.RQ.x * this.RR.w) - (this.RQ.w * this.RR.x);
            float num22 = (this.RQ.y * this.RR.z) - (this.RQ.z * this.RR.y);
            float num23 = (this.RQ.y * this.RR.w) - (this.RQ.w * this.RR.y);
            float num24 = (this.RQ.z * this.RR.w) - (this.RQ.w * this.RR.z);
            float num25 = ((this.Rq.x * num16) - (this.Rq.y * num14)) + (this.Rq.z * num13);
            float num26 = ((this.Rq.x * num17) - (this.Rq.y * num15)) + (this.Rq.w * num13);
            float num27 = ((this.Rq.x * num18) - (this.Rq.z * num15)) + (this.Rq.w * num14);
            float num28 = ((this.Rq.y * num18) - (this.Rq.z * num17)) + (this.Rq.w * num16);
            float num29 = ((this.Rq.x * num22) - (this.Rq.y * num20)) + (this.Rq.z * num19);
            float num30 = ((this.Rq.x * num23) - (this.Rq.y * num21)) + (this.Rq.w * num19);
            float num31 = ((this.Rq.x * num24) - (this.Rq.z * num21)) + (this.Rq.w * num20);
            float num32 = ((this.Rq.y * num24) - (this.Rq.z * num23)) + (this.Rq.w * num22);
            float num33 = ((this.RR.x * num6) - (this.RR.y * num4)) + (this.RR.z * num3);
            float num34 = ((this.RR.x * num7) - (this.RR.y * num5)) + (this.RR.w * num3);
            float num35 = ((this.RR.x * num8) - (this.RR.z * num5)) + (this.RR.w * num4);
            float num36 = ((this.RR.y * num8) - (this.RR.z * num7)) + (this.RR.w * num6);
            this.Rp.x = (float)(-num32 * num2);
            this.RQ.x = (float)(num31 * num2);
            this.Rq.x = (float)(-num30 * num2);
            this.RR.x = (float)(num29 * num2);
            this.Rp.y = (float)(num28 * num2);
            this.RQ.y = (float)(-num27 * num2);
            this.Rq.y = (float)(num26 * num2);
            this.RR.y = (float)(-num25 * num2);
            this.Rp.z = (float)(num36 * num2);
            this.RQ.z = (float)(-num35 * num2);
            this.Rq.z = (float)(num34 * num2);
            this.RR.z = (float)(-num33 * num2);
            this.Rp.w = (float)(-num12 * num2);
            this.RQ.w = (float)(num11 * num2);
            this.Rq.w = (float)(-num10 * num2);
            this.RR.w = (float)(num9 * num2);
            return true;
        }

        public Mat4 GetInverse()
        {
            Mat4 mat = this;
            mat.Inverse();
            return mat;
        }

        public void GetInverse(out Mat4 result)
        {
            result = this;
            result.Inverse();
        }

        public override bool Equals(object obj)
        {
            return ((obj is Mat4) && (this == ((Mat4)obj)));
        }

        public static bool operator ==(Mat4 v1, Mat4 v2)
        {
            return ((((v1.Rp == v2.Rp) && (v1.RQ == v2.RQ)) && (v1.Rq == v2.Rq)) && (v1.RR == v2.RR));
        }

        public static bool operator !=(Mat4 v1, Mat4 v2)
        {
            if ((!(v1.Rp != v2.Rp) && !(v1.RQ != v2.RQ)) && !(v1.Rq != v2.Rq))
            {
                return (v1.RR != v2.RR);
            }
            return true;
        }

        public unsafe float this[int row, int column]
        {
            get
            {
                if ((row < 0) || (row > 3))
                {
                    throw new ArgumentOutOfRangeException("row");
                }
                if ((column < 0) || (column > 3))
                {
                    throw new ArgumentOutOfRangeException("column");
                }
                fixed (float* numRef = &this.Rp.x)
                {
                    return numRef[((row * 4) + column) * 4];
                }
            }
            set
            {
                if ((row < 0) || (row > 3))
                {
                    throw new ArgumentOutOfRangeException("row");
                }
                if ((column < 0) || (column > 3))
                {
                    throw new ArgumentOutOfRangeException("column");
                }
                fixed (float* numRef = &this.Rp.x)
                {
                    numRef[((row * 4) + column) * 4] = value;
                }
            }
        }
        public bool Equals(Mat4 v, float epsilon)
        {
            if (!this.Rp.Equals(ref v.Rp, epsilon))
            {
                return false;
            }
            if (!this.RQ.Equals(ref v.RQ, epsilon))
            {
                return false;
            }
            if (!this.Rq.Equals(ref v.Rq, epsilon))
            {
                return false;
            }
            if (!this.RR.Equals(ref v.RR, epsilon))
            {
                return false;
            }
            return true;
        }

        public static Mat4 LookAt(Vec3 eye, Vec3 dir, Vec3 up)
        {
            Mat4 mat;
            LookAt(ref eye, ref dir, ref up, out mat);
            return mat;
        }

        public static void LookAt(ref Vec3 eye, ref Vec3 dir, ref Vec3 up, out Mat4 result)
        {
            Vec3 vec;
            Vec3 vec2;
            Vec3 vec3;
            Vec3.Subtract(ref eye, ref dir, out vec);
            vec.Normalize();
            Vec3.Cross(ref up, ref vec, out vec2);
            vec2.Normalize();
            Vec3.Cross(ref vec, ref vec2, out vec3);
            vec3.Normalize();
            result.Rp.x = vec2.x;
            result.RQ.x = vec2.y;
            result.Rq.x = vec2.z;
            result.RR.x = 0f;
            result.Rp.y = vec3.x;
            result.RQ.y = vec3.y;
            result.Rq.y = vec3.z;
            result.RR.y = 0f;
            result.Rp.z = vec.x;
            result.RQ.z = vec.y;
            result.Rq.z = vec.z;
            result.RR.z = 0f;
            result.Rp.w = 0f;
            result.RQ.w = 0f;
            result.Rq.w = 0f;
            result.RR.w = 1f;
            Mat4 identity = Identity;
            identity[3, 0] = -eye.X;
            identity[3, 1] = -eye.Y;
            identity[3, 2] = -eye.Z;
            result *= identity;
        }

        public static Mat4 Perspective(float fov, float aspect, float znear, float zfar)
        {
            Mat4 mat;
            Perspective(fov, aspect, znear, zfar, out mat);
            return mat;
        }

        public static void Perspective(float fov, float aspect, float znear, float zfar, out Mat4 result)
        {
            float num = MathFunctions.Tan((fov * 3.141593f) / 360f);
            float num2 = num * aspect;
            result.Rp.x = 1f / num2;
            result.RQ.x = 0f;
            result.Rq.x = 0f;
            result.RR.x = 0f;
            result.Rp.y = 0f;
            result.RQ.y = 1f / num;
            result.Rq.y = 0f;
            result.RR.y = 0f;
            result.Rp.z = 0f;
            result.RQ.z = 0f;
            result.Rq.z = -(zfar + znear) / (zfar - znear);
            result.RR.z = -((2f * zfar) * znear) / (zfar - znear);
            result.Rp.w = 0f;
            result.RQ.w = 0f;
            result.Rq.w = -1f;
            result.RR.w = 0f;
        }

        public static Mat4 FromTranslate(Vec3 translation)
        {
            Mat4 mat;
            mat.Rp.x = 1f;
            mat.Rp.y = 0f;
            mat.Rp.z = 0f;
            mat.Rp.w = 0f;
            mat.RQ.x = 0f;
            mat.RQ.y = 1f;
            mat.RQ.z = 0f;
            mat.RQ.w = 0f;
            mat.Rq.x = 0f;
            mat.Rq.y = 0f;
            mat.Rq.z = 1f;
            mat.Rq.w = 0f;
            mat.RR.x = translation.x;
            mat.RR.y = translation.y;
            mat.RR.z = translation.z;
            mat.RR.w = 1f;
            return mat;
        }

        public static void FromTranslate(ref Vec3 translation, out Mat4 result)
        {
            result.Rp.x = 1f;
            result.Rp.y = 0f;
            result.Rp.z = 0f;
            result.Rp.w = 0f;
            result.RQ.x = 0f;
            result.RQ.y = 1f;
            result.RQ.z = 0f;
            result.RQ.w = 0f;
            result.Rq.x = 0f;
            result.Rq.y = 0f;
            result.Rq.z = 1f;
            result.Rq.w = 0f;
            result.RR.x = translation.x;
            result.RR.y = translation.y;
            result.RR.z = translation.z;
            result.RR.w = 1f;
        }

        public Mat3 ToMat3()
        {
            Mat3 mat;
            mat.Rt.x = this.Rp.x;
            mat.Rt.y = this.Rp.y;
            mat.Rt.z = this.Rp.z;
            mat.RU.x = this.RQ.x;
            mat.RU.y = this.RQ.y;
            mat.RU.z = this.RQ.z;
            mat.Ru.x = this.Rq.x;
            mat.Ru.y = this.Rq.y;
            mat.Ru.z = this.Rq.z;
            return mat;
        }

        public void ToMat3(out Mat3 result)
        {
            result.Rt.x = this.Rp.x;
            result.Rt.y = this.Rp.y;
            result.Rt.z = this.Rp.z;
            result.RU.x = this.RQ.x;
            result.RU.y = this.RQ.y;
            result.RU.z = this.RQ.z;
            result.Ru.x = this.Rq.x;
            result.Ru.y = this.Rq.y;
            result.Ru.z = this.Rq.z;
        }

        public static Mat4 Parse(string text)
        {
            Mat4 mat;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 16)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 16 parts separated by spaces.", text));
            }
            try
            {
                mat = new Mat4(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]), float.Parse(strArray[4]), float.Parse(strArray[5]), float.Parse(strArray[6]), float.Parse(strArray[7]), float.Parse(strArray[8]), float.Parse(strArray[9]), float.Parse(strArray[10]), float.Parse(strArray[11]), float.Parse(strArray[12]), float.Parse(strArray[13]), float.Parse(strArray[14]), float.Parse(strArray[15]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return mat;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", new object[] { this.Rp.ToString(), this.RQ.ToString(), this.Rq.ToString(), this.RR.ToString() });
        }

        [Browsable(false)]
        public Vec4 Item0
        {
            get
            {
                return this.Rp;
            }
        }
        [Browsable(false)]
        public Vec4 Item1
        {
            get
            {
                return this.RQ;
            }
        }
        [Browsable(false)]
        public Vec4 Item2
        {
            get
            {
                return this.Rq;
            }
        }
        [Browsable(false)]
        public Vec4 Item3
        {
            get
            {
                return this.RR;
            }
        }
    }


}
