using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Vec3i>))]
    public unsafe struct Vec3i
    {
        internal int Rg;
        internal int RH;
        internal int Rh;
        public static readonly Vec3i Zero;
        public Vec3i(Vec3i source)
        {
            this.Rg = source.Rg;
            this.RH = source.RH;
            this.Rh = source.Rh;
        }

        public Vec3i(int x, int y, int z)
        {
            this.Rg = x;
            this.RH = y;
            this.Rh = z;
        }

        static Vec3i()
        {
            Zero = new Vec3i(0, 0, 0);
        }

        [DefaultValue(0)]
        public int X
        {
            get
            {
                return this.Rg;
            }
            set
            {
                this.Rg = value;
            }
        }
        [DefaultValue(0)]
        public int Y
        {
            get
            {
                return this.RH;
            }
            set
            {
                this.RH = value;
            }
        }
        [DefaultValue(0)]
        public int Z
        {
            get
            {
                return this.Rh;
            }
            set
            {
                this.Rh = value;
            }
        }
        public static Vec3i Parse(string text)
        {
            Vec3i veci;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 3)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 3 parts separated by spaces in the form (x y z).", text));
            }
            try
            {
                veci = new Vec3i(int.Parse(strArray[0]), int.Parse(strArray[1]), int.Parse(strArray[2]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return veci;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.Rg, this.RH, this.Rh);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Vec3i) && (this == ((Vec3i)obj)));
        }

        public override int GetHashCode()
        {
            return ((this.Rg.GetHashCode() ^ this.RH.GetHashCode()) ^ this.Rh.GetHashCode());
        }

        public static Vec3i operator +(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg + v2.Rg;
            veci.RH = v1.RH + v2.RH;
            veci.Rh = v1.Rh + v2.Rh;
            return veci;
        }

        public static Vec3i operator -(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg - v2.Rg;
            veci.RH = v1.RH - v2.RH;
            veci.Rh = v1.Rh - v2.Rh;
            return veci;
        }

        public static Vec3i operator *(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg * v2.Rg;
            veci.RH = v1.RH * v2.RH;
            veci.Rh = v1.Rh * v2.Rh;
            return veci;
        }

        public static Vec3i operator *(Vec3i v, int i)
        {
            Vec3i veci;
            veci.Rg = v.Rg * i;
            veci.RH = v.RH * i;
            veci.Rh = v.Rh * i;
            return veci;
        }

        public static Vec3i operator *(int i, Vec3i v)
        {
            Vec3i veci;
            veci.Rg = i * v.Rg;
            veci.RH = i * v.RH;
            veci.Rh = i * v.Rh;
            return veci;
        }

        public static Vec3i operator /(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg / v2.Rg;
            veci.RH = v1.RH / v2.RH;
            veci.Rh = v1.Rh / v2.Rh;
            return veci;
        }

        public static Vec3i operator /(Vec3i v, int i)
        {
            Vec3i veci;
            veci.Rg = v.Rg / i;
            veci.RH = v.RH / i;
            veci.Rh = v.Rh / i;
            return veci;
        }

        public static Vec3i operator /(int i, Vec3i v)
        {
            Vec3i veci;
            veci.Rg = i / v.Rg;
            veci.RH = i / v.RH;
            veci.Rh = i / v.Rh;
            return veci;
        }

        public static Vec3i operator -(Vec3i v)
        {
            Vec3i veci;
            veci.Rg = -v.Rg;
            veci.RH = -v.RH;
            veci.Rh = -v.Rh;
            return veci;
        }

        public static void Add(ref Vec3i v1, ref Vec3i v2, out Vec3i result)
        {
            result.Rg = v1.Rg + v2.Rg;
            result.RH = v1.RH + v2.RH;
            result.Rh = v1.Rh + v2.Rh;
        }

        public static void Subtract(ref Vec3i v1, ref Vec3i v2, out Vec3i result)
        {
            result.Rg = v1.Rg - v2.Rg;
            result.RH = v1.RH - v2.RH;
            result.Rh = v1.Rh - v2.Rh;
        }

        public static void Multiply(ref Vec3i v1, ref Vec3i v2, out Vec3i result)
        {
            result.Rg = v1.Rg * v2.Rg;
            result.RH = v1.RH * v2.RH;
            result.Rh = v1.Rh * v2.Rh;
        }

        public static void Multiply(ref Vec3i v, int i, out Vec3i result)
        {
            result.Rg = v.Rg * i;
            result.RH = v.RH * i;
            result.Rh = v.Rh * i;
        }

        public static void Multiply(int i, ref Vec3i v, out Vec3i result)
        {
            result.Rg = v.Rg * i;
            result.RH = v.RH * i;
            result.Rh = v.Rh * i;
        }

        public static void Divide(ref Vec3i v1, ref Vec3i v2, out Vec3i result)
        {
            result.Rg = v1.Rg / v2.Rg;
            result.RH = v1.RH / v2.RH;
            result.Rh = v1.Rh / v2.Rh;
        }

        public static void Divide(ref Vec3i v, int i, out Vec3i result)
        {
            result.Rg = v.Rg / i;
            result.RH = v.RH / i;
            result.Rh = v.Rh / i;
        }

        public static void Divide(int i, ref Vec3i v, out Vec3i result)
        {
            result.Rg = i / v.Rg;
            result.RH = i / v.RH;
            result.Rh = i / v.Rh;
        }

        public static void Negate(ref Vec3i v, out Vec3i result)
        {
            result.Rg = -v.Rg;
            result.RH = -v.RH;
            result.Rh = -v.Rh;
        }

        public static Vec3i Add(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg + v2.Rg;
            veci.RH = v1.RH + v2.RH;
            veci.Rh = v1.Rh + v2.Rh;
            return veci;
        }

        public static Vec3i Subtract(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg - v2.Rg;
            veci.RH = v1.RH - v2.RH;
            veci.Rh = v1.Rh - v2.Rh;
            return veci;
        }

        public static Vec3i Multiply(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg * v2.Rg;
            veci.RH = v1.RH * v2.RH;
            veci.Rh = v1.Rh * v2.Rh;
            return veci;
        }

        public static Vec3i Multiply(Vec3i v, int i)
        {
            Vec3i veci;
            veci.Rg = v.Rg * i;
            veci.RH = v.RH * i;
            veci.Rh = v.Rh * i;
            return veci;
        }

        public static Vec3i Multiply(int i, Vec3i v)
        {
            Vec3i veci;
            veci.Rg = v.Rg * i;
            veci.RH = v.RH * i;
            veci.Rh = v.Rh * i;
            return veci;
        }

        public static Vec3i Divide(Vec3i v1, Vec3i v2)
        {
            Vec3i veci;
            veci.Rg = v1.Rg / v2.Rg;
            veci.RH = v1.RH / v2.RH;
            veci.Rh = v1.Rh / v2.Rh;
            return veci;
        }

        public static Vec3i Divide(Vec3i v, int i)
        {
            Vec3i veci;
            veci.Rg = v.Rg / i;
            veci.RH = v.RH / i;
            veci.Rh = v.Rh / i;
            return veci;
        }

        public static Vec3i Divide(int i, Vec3i v)
        {
            Vec3i veci;
            veci.Rg = i / v.Rg;
            veci.RH = i / v.RH;
            veci.Rh = i / v.Rh;
            return veci;
        }

        public static Vec3i Negate(Vec3i v)
        {
            Vec3i veci;
            veci.Rg = -v.Rg;
            veci.RH = -v.RH;
            veci.Rh = -v.Rh;
            return veci;
        }

        public static bool operator ==(Vec3i v1, Vec3i v2)
        {
            return (((v1.Rg == v2.Rg) && (v1.RH == v2.RH)) && (v1.Rh == v2.Rh));
        }

        public static bool operator !=(Vec3i v1, Vec3i v2)
        {
            if ((v1.Rg == v2.Rg) && (v1.RH == v2.RH))
            {
                return (v1.Rh != v2.Rh);
            }
            return true;
        }

        public int this[int index]
        {
            get
            {
                if ((index < 0) || (index > 2))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (int* numRef = &this.Rg)
                {
                    return numRef[index];
                }
            }
            set
            {
                if ((index < 0) || (index > 2))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (int* numRef = &this.Rg)
                {
                    numRef[index] = value;
                }
            }
        }
        public static int Dot(Vec3i v1, Vec3i v2)
        {
            return (((v1.Rg * v2.Rg) + (v1.RH * v2.RH)) + (v1.Rh * v2.Rh));
        }

        public static void Dot(ref Vec3i v1, ref Vec3i v2, out int result)
        {
            result = ((v1.Rg * v2.Rg) + (v1.RH * v2.RH)) + (v1.Rh * v2.Rh);
        }

        public static Vec3i Cross(Vec3i v1, Vec3i v2)
        {
            return new Vec3i((v1.RH * v2.Rh) - (v1.Rh * v2.RH), (v1.Rh * v2.Rg) - (v1.Rg * v2.Rh), (v1.Rg * v2.RH) - (v1.RH * v2.Rg));
        }

        public static void Cross(ref Vec3i v1, ref Vec3i v2, out Vec3i result)
        {
            result.Rg = (v1.RH * v2.Rh) - (v1.Rh * v2.RH);
            result.RH = (v1.Rh * v2.Rg) - (v1.Rg * v2.Rh);
            result.Rh = (v1.Rg * v2.RH) - (v1.RH * v2.Rg);
        }

        public void Clamp(Vec3i min, Vec3i max)
        {
            if (this.Rg < min.Rg)
            {
                this.Rg = min.Rg;
            }
            else if (this.Rg > max.Rg)
            {
                this.Rg = max.Rg;
            }
            if (this.RH < min.RH)
            {
                this.RH = min.RH;
            }
            else if (this.RH > max.RH)
            {
                this.RH = max.RH;
            }
            if (this.Rh < min.Rh)
            {
                this.Rh = min.Rh;
            }
            else if (this.Rh > max.Rh)
            {
                this.Rh = max.Rh;
            }
        }

        public Vec2i ToVec2i()
        {
            return new Vec2i(this.Rg, this.RH);
        }

        public Vec3 ToVec3()
        {
            return new Vec3((float)this.Rg, (float)this.RH, (float)this.Rh);
        }
    }


}
