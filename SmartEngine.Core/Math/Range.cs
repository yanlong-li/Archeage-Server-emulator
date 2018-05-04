using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.ComponentModel;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(RangeConverter)), LogicSystemBrowsable(true)]
    public unsafe struct Range
    {
        private float minimum;
        private float maximum;
        public static readonly Range Zero;
        public Range(Range a)
        {
            this.minimum = a.minimum;
            this.maximum = a.maximum;
        }

        public Range(float minimum, float maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }

        static Range()
        {
            Zero = new Range(0f, 0f);
        }

        [LogicSystemMethodDisplay("Range( Single minimum, Single maximum )", "Range( {0}, {1} )")]
        public static Range Construct(float minimum, float maximum)
        {
            return new Range(minimum, maximum);
        }

        [DefaultValue((float)0f), LogicSystemBrowsable(true)]
        public float Minimum
        {
            get
            {
                return this.minimum;
            }
            set
            {
                this.minimum = value;
            }
        }
        [LogicSystemBrowsable(true), DefaultValue((float)0f)]
        public float Maximum
        {
            get
            {
                return this.maximum;
            }
            set
            {
                this.maximum = value;
            }
        }
        [LogicSystemBrowsable(true)]
        public static Range Parse(string text)
        {
            Range range;
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
                range = new Range(float.Parse(strArray[0]), float.Parse(strArray[1]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return range;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.minimum, this.maximum);
        }

        [LogicSystemBrowsable(true)]
        public string ToString(int precision)
        {
            string str = "";
            str = str.PadLeft(precision, '#');
            return string.Format("{0:0." + str + "} {1:0." + str + "}", this.minimum, this.maximum);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Range) && (this == ((Range)obj)));
        }

        public override int GetHashCode()
        {
            return (this.minimum.GetHashCode() ^ this.maximum.GetHashCode());
        }

        [LogicSystemBrowsable(true)]
        public static Range operator *(Range v, float s)
        {
            Range range;
            range.minimum = v.minimum * s;
            range.maximum = v.maximum * s;
            return range;
        }

        public static Range operator *(float s, Range v)
        {
            Range range;
            range.minimum = v.minimum * s;
            range.maximum = v.maximum * s;
            return range;
        }

        [LogicSystemBrowsable(true)]
        public static Range operator /(Range v, float s)
        {
            Range range;
            float num = 1f / s;
            range.minimum = v.minimum * num;
            range.maximum = v.maximum * num;
            return range;
        }

        public static Range operator /(float s, Range v)
        {
            Range range;
            range.minimum = s / v.minimum;
            range.maximum = s / v.maximum;
            return range;
        }

        [LogicSystemBrowsable(true)]
        public static Range operator -(Range v)
        {
            Range range;
            range.minimum = -v.minimum;
            range.maximum = -v.maximum;
            return range;
        }

        public static void Multiply(ref Range v, float s, out Range result)
        {
            result.minimum = v.minimum * s;
            result.maximum = v.maximum * s;
        }

        public static void Multiply(float s, ref Range v, out Range result)
        {
            result.minimum = v.minimum * s;
            result.maximum = v.maximum * s;
        }

        public static void Divide(ref Range v, float s, out Range result)
        {
            float num = 1f / s;
            result.minimum = v.minimum * num;
            result.maximum = v.maximum * num;
        }

        public static void Divide(float s, ref Range v, out Range result)
        {
            result.minimum = s / v.minimum;
            result.maximum = s / v.maximum;
        }

        public static void Negate(ref Range v, out Range result)
        {
            result.minimum = -v.minimum;
            result.maximum = -v.maximum;
        }

        public static Range Multiply(Range v, float s)
        {
            Range range;
            range.minimum = v.minimum * s;
            range.maximum = v.maximum * s;
            return range;
        }

        public static Range Multiply(float s, Range v)
        {
            Range range;
            range.minimum = v.minimum * s;
            range.maximum = v.maximum * s;
            return range;
        }

        public static Range Divide(Range v, float s)
        {
            Range range;
            float num = 1f / s;
            range.minimum = v.minimum * num;
            range.maximum = v.maximum * num;
            return range;
        }

        public static Range Divide(float s, Range v)
        {
            Range range;
            range.minimum = s / v.minimum;
            range.maximum = s / v.maximum;
            return range;
        }

        public static Range Negate(Range v)
        {
            Range range;
            range.minimum = -v.minimum;
            range.maximum = -v.maximum;
            return range;
        }

        [LogicSystemBrowsable(true)]
        public static bool operator ==(Range v1, Range v2)
        {
            return ((v1.minimum == v2.minimum) && (v1.maximum == v2.maximum));
        }

        [LogicSystemBrowsable(true)]
        public static bool operator !=(Range v1, Range v2)
        {
            if (v1.minimum == v2.minimum)
            {
                return (v1.maximum != v2.maximum);
            }
            return true;
        }

        [LogicSystemBrowsable(true)]
        public float this[int index]
        {
            get
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.minimum)
                {
                    return numRef[index * 4];
                }
            }
            set
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.minimum)
                {
                    numRef[index * 4] = value;
                }
            }
        }
        public bool Equals(Range v, float epsilon)
        {
            if (System.Math.Abs((float)(this.minimum - v.minimum)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.maximum - v.maximum)) > epsilon)
            {
                return false;
            }
            return true;
        }

        public float Size()
        {
            return (this.maximum - this.minimum);
        }
    }
}
