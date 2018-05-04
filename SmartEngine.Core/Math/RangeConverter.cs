using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.ComponentModel;
using System.Globalization;

namespace SmartEngine.Core.Math
{
    public class RangeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                try
                {
                    return Range.Parse((string)value);
                }
                catch (Exception)
                {
                    return value;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(string)) && (value.GetType() == typeof(Range)))
            {
                Range range = (Range)value;
                return range.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
