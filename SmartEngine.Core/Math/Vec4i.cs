using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Vec4i>))]
    public unsafe struct Vec4i
    {
        internal int RM;
        internal int Rm;
        internal int RN;
        internal int Rn;
        public static readonly Vec4i Zero;
        public Vec4i(Vec4i source)
        {
            this.RM = source.RM;
            this.Rm = source.Rm;
            this.RN = source.RN;
            this.Rn = source.Rn;
        }

        public Vec4i(int x, int y, int z, int w)
        {
            this.RM = x;
            this.Rm = y;
            this.RN = z;
            this.Rn = w;
        }

        public Vec4i(Vec3i v, int w)
        {
            this.RM = v.X;
            this.Rm = v.Y;
            this.RN = v.Z;
            this.Rn = w;
        }

        static Vec4i()
        {
            Zero = new Vec4i(0, 0, 0, 0);
        }

        [DefaultValue((float)0f)]
        public int X
        {
            get
            {
                return this.RM;
            }
            set
            {
                this.RM = value;
            }
        }
        [DefaultValue((float)0f)]
        public int Y
        {
            get
            {
                return this.Rm;
            }
            set
            {
                this.Rm = value;
            }
        }
        [DefaultValue((float)0f)]
        public int Z
        {
            get
            {
                return this.RN;
            }
            set
            {
                this.RN = value;
            }
        }
        [DefaultValue((float)0f)]
        public int W
        {
            get
            {
                return this.Rn;
            }
            set
            {
                this.Rn = value;
            }
        }
        public static Vec4i Parse(string text)
        {
            Vec4i veci;
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
                veci = new Vec4i(int.Parse(strArray[0]), int.Parse(strArray[1]), int.Parse(strArray[2]), int.Parse(strArray[3]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return veci;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", new object[] { this.RM, this.Rm, this.RN, this.Rn });
        }

        public override bool Equals(object obj)
        {
            return ((obj is Vec4i) && (this == ((Vec4i)obj)));
        }

        public override int GetHashCode()
        {
            return (((this.RM.GetHashCode() ^ this.Rm.GetHashCode()) ^ this.RN.GetHashCode()) ^ this.Rn.GetHashCode());
        }

        public static Vec4i operator +(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM + v2.RM;
            veci.Rm = v1.Rm + v2.Rm;
            veci.RN = v1.RN + v2.RN;
            veci.Rn = v1.Rn + v2.Rn;
            return veci;
        }

        public static Vec4i operator -(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM - v2.RM;
            veci.Rm = v1.Rm - v2.Rm;
            veci.RN = v1.RN - v2.RN;
            veci.Rn = v1.Rn - v2.Rn;
            return veci;
        }

        public static Vec4i operator *(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM * v2.RM;
            veci.Rm = v1.Rm * v2.Rm;
            veci.RN = v1.RN * v2.RN;
            veci.Rn = v1.Rn * v2.Rn;
            return veci;
        }

        public static Vec4i operator *(Vec4i v, int i)
        {
            Vec4i veci;
            veci.RM = v.RM * i;
            veci.Rm = v.Rm * i;
            veci.RN = v.RN * i;
            veci.Rn = v.Rn * i;
            return veci;
        }

        public static Vec4i operator *(int i, Vec4i v)
        {
            Vec4i veci;
            veci.RM = i * v.RM;
            veci.Rm = i * v.Rm;
            veci.RN = i * v.RN;
            veci.Rn = i * v.Rn;
            return veci;
        }

        public static Vec4i operator /(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM / v2.RM;
            veci.Rm = v1.Rm / v2.Rm;
            veci.RN = v1.RN / v2.RN;
            veci.Rn = v1.Rn / v2.Rn;
            return veci;
        }

        public static Vec4i operator /(Vec4i v, int i)
        {
            Vec4i veci;
            veci.RM = v.RM / i;
            veci.Rm = v.Rm / i;
            veci.RN = v.RN / i;
            veci.Rn = v.Rn / i;
            return veci;
        }

        public static Vec4i operator /(int i, Vec4i v)
        {
            Vec4i veci;
            veci.RM = i / v.RM;
            veci.Rm = i / v.Rm;
            veci.RN = i / v.RN;
            veci.Rn = i / v.Rn;
            return veci;
        }

        public static Vec4i operator -(Vec4i v)
        {
            Vec4i veci;
            veci.RM = -v.RM;
            veci.Rm = -v.Rm;
            veci.RN = -v.RN;
            veci.Rn = -v.Rn;
            return veci;
        }

        public static void Add(ref Vec4i v1, ref Vec4i v2, out Vec4i result)
        {
            result.RM = v1.RM + v2.RM;
            result.Rm = v1.Rm + v2.Rm;
            result.RN = v1.RN + v2.RN;
            result.Rn = v1.Rn + v2.Rn;
        }

        public static void Subtract(ref Vec4i v1, ref Vec4i v2, out Vec4i result)
        {
            result.RM = v1.RM - v2.RM;
            result.Rm = v1.Rm - v2.Rm;
            result.RN = v1.RN - v2.RN;
            result.Rn = v1.Rn - v2.Rn;
        }

        public static void Multiply(ref Vec4i v1, ref Vec4i v2, out Vec4i result)
        {
            result.RM = v1.RM * v2.RM;
            result.Rm = v1.Rm * v2.Rm;
            result.RN = v1.RN * v2.RN;
            result.Rn = v1.Rn * v2.Rn;
        }

        public static void Multiply(ref Vec4i v, int i, out Vec4i result)
        {
            result.RM = v.RM * i;
            result.Rm = v.Rm * i;
            result.RN = v.RN * i;
            result.Rn = v.Rn * i;
        }

        public static void Multiply(int i, ref Vec4i v, out Vec4i result)
        {
            result.RM = v.RM * i;
            result.Rm = v.Rm * i;
            result.RN = v.RN * i;
            result.Rn = v.Rn * i;
        }

        public static void Divide(ref Vec4i v1, ref Vec4i v2, out Vec4i result)
        {
            result.RM = v1.RM / v2.RM;
            result.Rm = v1.Rm / v2.Rm;
            result.RN = v1.RN / v2.RN;
            result.Rn = v1.Rn / v2.Rn;
        }

        public static void Divide(ref Vec4i v, int i, out Vec4i result)
        {
            result.RM = v.RM / i;
            result.Rm = v.Rm / i;
            result.RN = v.RN / i;
            result.Rn = v.Rn / i;
        }

        public static void Divide(int i, ref Vec4i v, out Vec4i result)
        {
            result.RM = i / v.RM;
            result.Rm = i / v.Rm;
            result.RN = i / v.RN;
            result.Rn = i / v.Rn;
        }

        public static void Negate(ref Vec4i v, out Vec4i result)
        {
            result.RM = -v.RM;
            result.Rm = -v.Rm;
            result.RN = -v.RN;
            result.Rn = -v.Rn;
        }

        public static Vec4i Add(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM + v2.RM;
            veci.Rm = v1.Rm + v2.Rm;
            veci.RN = v1.RN + v2.RN;
            veci.Rn = v1.Rn + v2.Rn;
            return veci;
        }

        public static Vec4i Subtract(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM - v2.RM;
            veci.Rm = v1.Rm - v2.Rm;
            veci.RN = v1.RN - v2.RN;
            veci.Rn = v1.Rn - v2.Rn;
            return veci;
        }

        public static Vec4i Multiply(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM * v2.RM;
            veci.Rm = v1.Rm * v2.Rm;
            veci.RN = v1.RN * v2.RN;
            veci.Rn = v1.Rn * v2.Rn;
            return veci;
        }

        public static Vec4i Multiply(Vec4i v, int i)
        {
            Vec4i veci;
            veci.RM = v.RM * i;
            veci.Rm = v.Rm * i;
            veci.RN = v.RN * i;
            veci.Rn = v.Rn * i;
            return veci;
        }

        public static Vec4i Multiply(int i, Vec4i v)
        {
            Vec4i veci;
            veci.RM = v.RM * i;
            veci.Rm = v.Rm * i;
            veci.RN = v.RN * i;
            veci.Rn = v.Rn * i;
            return veci;
        }

        public static Vec4i Divide(Vec4i v1, Vec4i v2)
        {
            Vec4i veci;
            veci.RM = v1.RM / v2.RM;
            veci.Rm = v1.Rm / v2.Rm;
            veci.RN = v1.RN / v2.RN;
            veci.Rn = v1.Rn / v2.Rn;
            return veci;
        }

        public static Vec4i Divide(Vec4i v1, int v2)
        {
            Vec4i veci;
            veci.RM = v1.RM / v2;
            veci.Rm = v1.Rm / v2;
            veci.RN = v1.RN / v2;
            veci.Rn = v1.Rn / v2;
            return veci;
        }

        public static Vec4i Divide(int i, Vec4i v)
        {
            Vec4i veci;
            veci.RM = i / v.RM;
            veci.Rm = i / v.Rm;
            veci.RN = i / v.RN;
            veci.Rn = i / v.Rn;
            return veci;
        }

        public static Vec4i Negate(Vec4i v)
        {
            Vec4i veci;
            veci.RM = -v.RM;
            veci.Rm = -v.Rm;
            veci.RN = -v.RN;
            veci.Rn = -v.Rn;
            return veci;
        }

        public static bool operator ==(Vec4i v1, Vec4i v2)
        {
            return ((((v1.RM == v2.RM) && (v1.Rm == v2.Rm)) && (v1.RN == v2.RN)) && (v1.Rn == v2.Rn));
        }

        public static bool operator !=(Vec4i v1, Vec4i v2)
        {
            if (((v1.RM == v2.RM) && (v1.Rm == v2.Rm)) && (v1.RN == v2.RN))
            {
                return (v1.Rn != v2.Rn);
            }
            return true;
        }

        public int this[int index]
        {
            get
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (int* numRef = &this.RM)
                {
                    return numRef[index];
                }
            }
            set
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (int* numRef = &this.RM)
                {
                    numRef[index] = value;
                }
            }
        }
        public bool Equals(Vec4i v, int epsilon)
        {
            if (System.Math.Abs((int)(this.RM - v.RM)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((int)(this.Rm - v.Rm)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((int)(this.RN - v.RN)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((int)(this.Rn - v.Rn)) > epsilon)
            {
                return false;
            }
            return true;
        }

        private void A(Vec4i A, Vec4i a)
        {
            if (this.RM < A.RM)
            {
                this.RM = A.RM;
            }
            else if (this.RM > a.RM)
            {
                this.RM = a.RM;
            }
            if (this.Rm < A.Rm)
            {
                this.Rm = A.Rm;
            }
            else if (this.Rm > a.Rm)
            {
                this.Rm = a.Rm;
            }
            if (this.RN < A.RN)
            {
                this.RN = A.RN;
            }
            else if (this.RN > a.RN)
            {
                this.RN = a.RN;
            }
            if (this.Rn < A.Rn)
            {
                this.Rn = A.Rn;
            }
            else if (this.Rn > a.Rn)
            {
                this.Rn = a.Rn;
            }
        }

        public Vec2i ToVec2i()
        {
            Vec2i veci;
            veci.Rf = this.RM;
            veci.RG = this.Rm;
            return veci;
        }

        public Vec3i ToVec3i()
        {
            Vec3i veci;
            veci.Rg = this.RM;
            veci.RH = this.Rm;
            veci.Rh = this.RN;
            return veci;
        }

        public Vec4 ToVec4()
        {
            Vec4 vec;
            vec.x = this.RM;
            vec.y = this.Rm;
            vec.z = this.RN;
            vec.w = this.Rn;
            return vec;
        }
    }
}
