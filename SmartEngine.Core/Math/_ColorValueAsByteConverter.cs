using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace SmartEngine.Core.Math
{
    public class _ColorValueAsByteConverter : TypeConverter
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
                    ColorValue value2 = ColorValue.Parse((string)value);
                    if (((string)value).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length == 3)
                    {
                        value2.Alpha *= 255f;
                    }
                    return (ColorValue)(value2 / 255f);
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
            if ((destinationType != typeof(string)) || (value.GetType() != typeof(ColorValue)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            ColorValue value2 = (ColorValue)value;
            int num = (int)((value2[0] * 255f) + 0.5f);
            int num2 = (int)((value2[1] * 255f) + 0.5f);
            int num3 = (int)((value2[2] * 255f) + 0.5f);
            int num4 = (int)((value2[3] * 255f) + 0.5f);
            if (num4 != 255)
            {
                return string.Format("{0} {1} {2} {3}", new object[] { num, num2, num3, num4 });
            }
            return string.Format("{0} {1} {2}", num, num2, num3);
        }
    }
}
