using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace SmartEngine.Core.Math
{
    public class _QuatAsAnglesConverter : TypeConverter
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
                    Angles angles = Angles.Parse((string)value);
                    angles.Normalize360();
                    return angles.ToQuat();
                }
                catch (Exception)
                {
                    return value;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override unsafe object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType != typeof(string)) || (value.GetType() != typeof(Quat)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            Angles angles = ((Quat)value).ToAngles();
            for (int i = 0; i < 3; i++)
            {
                float num2 = (float)System.Math.Round((double)angles[i]);
                if ((num2 != angles[i]) && (System.Math.Abs((float)(num2 - angles[i])) < 0.0001f))
                {
                    angles[i] = num2;
                }
            }
            angles.Normalize360();
            if ((angles[1] >= 180f) && (angles[2] >= 180f))
            {
                angles[0] = 180f - angles[0];
                angles[1] = angles[1] - 180f;
                angles[2] = angles[2] - 180f;
            }
            angles.Normalize360();
            return angles.ToString();
        }
    }
}
