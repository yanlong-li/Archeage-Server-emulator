using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Vec2i>))]
    public unsafe struct Vec2i
    {
        internal int Rf;
        internal int RG;
        public static readonly Vec2i Zero;
        public Vec2i(Vec2i source)
        {
            this.Rf = source.Rf;
            this.RG = source.RG;
        }

        public Vec2i(int x, int y)
        {
            this.Rf = x;
            this.RG = y;
        }

        static Vec2i()
        {
            Zero = new Vec2i(0, 0);
        }

        [DefaultValue(0)]
        public int X
        {
            get
            {
                return this.Rf;
            }
            set
            {
                this.Rf = value;
            }
        }
        [DefaultValue(0)]
        public int Y
        {
            get
            {
                return this.RG;
            }
            set
            {
                this.RG = value;
            }
        }
        public static Vec2i Parse(string text)
        {
            Vec2i veci;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 2)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 2 parts separated by spaces in the form (x y).", text));
            }
            try
            {
                veci = new Vec2i(int.Parse(strArray[0]), int.Parse(strArray[1]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return veci;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Rf, this.RG);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Vec2i) && (this == ((Vec2i)obj)));
        }

        public override int GetHashCode()
        {
            return (this.Rf.GetHashCode() ^ this.RG.GetHashCode());
        }

        public static Vec2i operator +(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf + v2.Rf;
            veci.RG = v1.RG + v2.RG;
            return veci;
        }

        public static Vec2i operator -(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf - v2.Rf;
            veci.RG = v1.RG - v2.RG;
            return veci;
        }

        public static Vec2i operator *(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf * v2.Rf;
            veci.RG = v1.RG * v2.RG;
            return veci;
        }

        public static Vec2i operator *(Vec2i v, int i)
        {
            Vec2i veci;
            veci.Rf = v.Rf * i;
            veci.RG = v.RG * i;
            return veci;
        }

        public static Vec2i operator *(int i, Vec2i v)
        {
            Vec2i veci;
            veci.Rf = i * v.Rf;
            veci.RG = i * v.RG;
            return veci;
        }

        public static Vec2i operator /(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf / v2.Rf;
            veci.RG = v1.RG / v2.RG;
            return veci;
        }

        public static Vec2i operator /(Vec2i v, int i)
        {
            Vec2i veci;
            veci.Rf = v.Rf / i;
            veci.RG = v.RG / i;
            return veci;
        }

        public static Vec2i operator /(int i, Vec2i v)
        {
            Vec2i veci;
            veci.Rf = i / v.Rf;
            veci.RG = i / v.RG;
            return veci;
        }

        public static Vec2i operator -(Vec2i v)
        {
            Vec2i veci;
            veci.Rf = -v.Rf;
            veci.RG = -v.RG;
            return veci;
        }

        public static void Add(ref Vec2i v1, ref Vec2i v2, out Vec2i result)
        {
            result.Rf = v1.Rf + v2.Rf;
            result.RG = v1.RG + v2.RG;
        }

        public static void Subtract(ref Vec2i v1, ref Vec2i v2, out Vec2i result)
        {
            result.Rf = v1.Rf - v2.Rf;
            result.RG = v1.RG - v2.RG;
        }

        public static void Multiply(ref Vec2i v1, ref Vec2i v2, out Vec2i result)
        {
            result.Rf = v1.Rf * v2.Rf;
            result.RG = v1.RG * v2.RG;
        }

        public static void Multiply(ref Vec2i v, int i, out Vec2i result)
        {
            result.Rf = v.Rf * i;
            result.RG = v.RG * i;
        }

        public static void Multiply(int i, ref Vec2i v, out Vec2i result)
        {
            result.Rf = v.Rf * i;
            result.RG = v.RG * i;
        }

        public static void Divide(ref Vec2i v1, ref Vec2i v2, out Vec2i result)
        {
            result.Rf = v1.Rf / v2.Rf;
            result.RG = v1.RG / v2.RG;
        }

        public static void Divide(ref Vec2i v, int i, out Vec2i result)
        {
            result.Rf = v.Rf / i;
            result.RG = v.RG / i;
        }

        public static void Divide(int v1, ref Vec2i v2, out Vec2i result)
        {
            result.Rf = v1 / v2.Rf;
            result.RG = v1 / v2.RG;
        }

        public static void Negate(ref Vec2i v, out Vec2i result)
        {
            result.Rf = -v.Rf;
            result.RG = -v.RG;
        }

        public static Vec2i Add(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf + v2.Rf;
            veci.RG = v1.RG + v2.RG;
            return veci;
        }

        public static Vec2i Subtract(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf - v2.Rf;
            veci.RG = v1.RG - v2.RG;
            return veci;
        }

        public static Vec2i Multiply(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf * v2.Rf;
            veci.RG = v1.RG * v2.RG;
            return veci;
        }

        public static Vec2i Multiply(Vec2i v, int i)
        {
            Vec2i veci;
            veci.Rf = v.Rf * i;
            veci.RG = v.RG * i;
            return veci;
        }

        public static Vec2i Multiply(int i, Vec2i v)
        {
            Vec2i veci;
            veci.Rf = v.Rf * i;
            veci.RG = v.RG * i;
            return veci;
        }

        public static Vec2i Divide(Vec2i v1, Vec2i v2)
        {
            Vec2i veci;
            veci.Rf = v1.Rf / v2.Rf;
            veci.RG = v1.RG / v2.RG;
            return veci;
        }

        public static Vec2i Divide(Vec2i v, int i)
        {
            Vec2i veci;
            veci.Rf = v.Rf / i;
            veci.RG = v.RG / i;
            return veci;
        }

        public static Vec2i Divide(int i, Vec2i v)
        {
            Vec2i veci;
            veci.Rf = i / v.Rf;
            veci.RG = i / v.RG;
            return veci;
        }

        public static Vec2i Negate(Vec2i v)
        {
            Vec2i veci;
            veci.Rf = -v.Rf;
            veci.RG = -v.RG;
            return veci;
        }

        public static bool operator ==(Vec2i v1, Vec2i v2)
        {
            return ((v1.Rf == v2.Rf) && (v1.RG == v2.RG));
        }

        public static bool operator !=(Vec2i v1, Vec2i v2)
        {
            if (v1.Rf == v2.Rf)
            {
                return (v1.RG != v2.RG);
            }
            return true;
        }

        public int this[int index]
        {
            get
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (int* numRef = &this.Rf)
                {
                    return numRef[index];
                }
            }
            set
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (int* numRef = &this.Rf)
                {
                    numRef[index] = value;
                }
            }
        }
        [LogicSystemBrowsable(true)]
        public static int Dot(Vec2i v1, Vec2i v2)
        {
            return ((v1.Rf * v2.Rf) + (v1.RG * v2.RG));
        }

        public static void Dot(ref Vec2i v1, ref Vec2i v2, out int result)
        {
            result = (v1.Rf * v2.Rf) + (v1.RG * v2.RG);
        }

        [LogicSystemBrowsable(true)]
        public static Vec2i Cross(Vec2i v1, Vec2i v2)
        {
            return new Vec2i((v1.RG * v2.Rf) - (v1.Rf * v2.RG), (v1.Rf * v2.RG) - (v1.RG * v2.Rf));
        }

        public static void Cross(ref Vec2i v1, ref Vec2i v2, out Vec2i result)
        {
            result.Rf = (v1.RG * v2.Rf) - (v1.Rf * v2.RG);
            result.RG = (v1.Rf * v2.RG) - (v1.RG * v2.Rf);
        }

        public bool Equals(Vec2i v, int epsilon)
        {
            if (System.Math.Abs((int)(this.Rf - v.Rf)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((int)(this.RG - v.RG)) > epsilon)
            {
                return false;
            }
            return true;
        }

        public void Clamp(Vec2i min, Vec2i max)
        {
            if (this.Rf < min.Rf)
            {
                this.Rf = min.Rf;
            }
            else if (this.Rf > max.Rf)
            {
                this.Rf = max.Rf;
            }
            if (this.RG < min.RG)
            {
                this.RG = min.RG;
            }
            else if (this.RG > max.RG)
            {
                this.RG = max.RG;
            }
        }

        public Vec2 ToVec2()
        {
            Vec2 vec;
            vec.x = this.Rf;
            vec.y = this.RG;
            return vec;
        }
    }
}
