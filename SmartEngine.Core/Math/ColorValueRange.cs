using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.ComponentModel;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), Editor(typeof(ColorValueRangeEditor), typeof(UITypeEditor)), TypeConverter(typeof(_ColorValueRangeAsByteConverter))]
    public struct ColorValueRange
    {
        private ColorValue minimum;
        private ColorValue maximum;
        public static readonly ColorValueRange Zero;
        public static readonly ColorValueRange White;
        public ColorValueRange(ColorValueRange source)
        {
            this.minimum = source.minimum;
            this.maximum = source.maximum;
        }

        public ColorValueRange(ColorValue minimum, ColorValue maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }

        static ColorValueRange()
        {
            Zero = new ColorValueRange(ColorValue.Zero, ColorValue.Zero);
            White = new ColorValueRange(new ColorValue(1f, 1f, 1f), new ColorValue(1f, 1f, 1f));
        }

        [DefaultValue(typeof(ColorValue), "255 255 255")]
        public ColorValue Minimum
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
        [DefaultValue(typeof(ColorValue), "255 255 255")]
        public ColorValue Maximum
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
        public static ColorValueRange Parse(string text)
        {
            ColorValueRange range;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The parsableText parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(new char[] { ';' });
            if (strArray.Length != 2)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 2 parts separated by \";\" in the form (0 1) with optional parenthesis.", text));
            }
            try
            {
                range = new ColorValueRange(ColorValue.Parse(strArray[0].Trim()), ColorValue.Parse(strArray[1].Trim()));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the colors must be decimal numbers.");
            }
            return range;
        }

        public override string ToString()
        {
            return string.Format("{0}; {1}", this.minimum, this.maximum);
        }

        public override bool Equals(object obj)
        {
            return ((obj is ColorValueRange) && (this == ((ColorValueRange)obj)));
        }

        public override int GetHashCode()
        {
            return (this.minimum.GetHashCode() ^ this.maximum.GetHashCode());
        }

        public static bool operator ==(ColorValueRange v1, ColorValueRange v2)
        {
            return ((v1.minimum == v2.minimum) && (v1.maximum == v2.maximum));
        }

        public static bool operator !=(ColorValueRange v1, ColorValueRange v2)
        {
            if (!(v1.minimum != v2.minimum))
            {
                return (v1.maximum != v2.maximum);
            }
            return true;
        }

        public unsafe ColorValue this[int index]
        {
            get
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (ColorValue* valueRef = &this.minimum)
                {
                    return valueRef[index];
                }
            }
            set
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (ColorValue* valueRef = &this.minimum)
                {
                    valueRef[index] = value;
                }
            }
        }
    }


}
