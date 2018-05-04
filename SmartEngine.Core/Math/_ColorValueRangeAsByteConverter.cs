using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
namespace SmartEngine.Core.Math
{
    public class _ColorValueRangeAsByteConverter : TypeConverter
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
                    ColorValueRange range = ColorValueRange.Parse((string)value);
                    string[] strArray = ((string)value).Split(new char[] { ';' });
                    if (strArray[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length == 3)
                    {
                        range.Minimum *= new ColorValue(1f, 1f, 1f, 255f);
                    }
                    if (strArray[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length == 3)
                    {
                        range.Maximum *= new ColorValue(1f, 1f, 1f, 255f);
                    }
                    range.Minimum = (ColorValue)(range.Minimum / 255f);
                    range.Maximum = (ColorValue)(range.Maximum / 255f);
                    return range;
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
            if ((destinationType != typeof(string)) || (value.GetType() != typeof(ColorValueRange)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            ColorValueRange range = (ColorValueRange)value;
            string str = "";
            int num = (int)((range.Minimum[0] * 255f) + 0.5f);
            int num2 = (int)((range.Minimum[1] * 255f) + 0.5f);
            int num3 = (int)((range.Minimum[2] * 255f) + 0.5f);
            int num4 = (int)((range.Minimum[3] * 255f) + 0.5f);
            if (num4 != 255)
            {
                str = str + string.Format("{0} {1} {2} {3}", new object[] { num, num2, num3, num4 });
            }
            else
            {
                str = str + string.Format("{0} {1} {2}", num, num2, num3);
            }
            str = str + "; ";
            num = (int)((range.Maximum[0] * 255f) + 0.5f);
            num2 = (int)((range.Maximum[1] * 255f) + 0.5f);
            num3 = (int)((range.Maximum[2] * 255f) + 0.5f);
            num4 = (int)((range.Maximum[3] * 255f) + 0.5f);
            if (num4 != 255)
            {
                return (str + string.Format("{0} {1} {2} {3}", new object[] { num, num2, num3, num4 }));
            }
            return (str + string.Format("{0} {1} {2}", num, num2, num3));
        }
    }
}
