using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace SmartEngine.Core.Math
{
    public class ColorValueRangeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                bool flag;
                ColorValueRange range;
                object[] propertyOwners = null;
                PropertyInfo property = null;
                try
                {
                    _IWrappedPropertyDescriptor propertyDescriptor = context.PropertyDescriptor as _IWrappedPropertyDescriptor;
                    if (propertyDescriptor != null)
                    {
                        propertyOwners = new object[] { propertyDescriptor.GetWrappedOwner() };
                        property = propertyDescriptor.GetWrappedProperty();
                    }
                    else
                    {
                        if (context.Instance is Array)
                        {
                            object[] instance = (object[])context.Instance;
                            propertyOwners = new object[instance.Length];
                            for (int i = 0; i < instance.Length; i++)
                            {
                                object wrapperOwner = instance[i];
                                if (wrapperOwner is _IWrappedCustomTypeDescriptor)
                                {
                                    wrapperOwner = ((_IWrappedCustomTypeDescriptor)wrapperOwner).GetWrapperOwner();
                                }
                                propertyOwners[i] = wrapperOwner;
                            }
                        }
                        else
                        {
                            propertyOwners = new object[] { context.Instance };
                        }
                        property = context.PropertyDescriptor.ComponentType.GetProperty(context.PropertyDescriptor.Name);
                    }
                }
                catch (Exception)
                {
                }
                if (property != null)
                {
                    _MathExEditorBridge.Instance.ColorValueRangeEditorControlShow(provider, propertyOwners, property, out flag, out range);
                }
                else
                {
                    _MathExEditorBridge.Instance.ColorValueRangeEditorControlShow(provider, (ColorValueRange)value, out flag, out range);
                }
                if (flag)
                {
                    return range;
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value != null)
            {
                ColorValueRange range = (ColorValueRange)e.Value;
                for (int i = 0; i < 2; i++)
                {
                    Rectangle rectangle;
                    ColorValue value2 = range[i];
                    if (i == 0)
                    {
                        rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Size.Width / 2, e.Bounds.Size.Height);
                    }
                    else
                    {
                        rectangle = new Rectangle(e.Bounds.X + (e.Bounds.Size.Width / 2), e.Bounds.Y, e.Bounds.Size.Width / 2, e.Bounds.Size.Height);
                    }
                    int[] numArray = new int[4];
                    for (int j = 0; j < 4; j++)
                    {
                        int num3 = (int)(value2[j] * 255f);
                        if (num3 < 0)
                        {
                            num3 = 0;
                        }
                        if (num3 > 255)
                        {
                            num3 = 255;
                        }
                        numArray[j] = num3;
                    }
                    if (value2.Alpha != 1f)
                    {
                        using (HatchBrush brush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.FromArgb(128, 128, 128), Color.FromArgb(192, 192, 192)))
                        {
                            e.Graphics.FillRectangle(brush, rectangle);
                        }
                    }
                    Color color = Color.FromArgb(255, numArray[0], numArray[1], numArray[2]);
                    Color color2 = Color.FromArgb(numArray[3], numArray[0], numArray[1], numArray[2]);
                    using (LinearGradientBrush brush2 = new LinearGradientBrush(rectangle, color, color2, 90f, false))
                    {
                        e.Graphics.FillRectangle(brush2, rectangle);
                    }
                }
            }
            base.PaintValue(e);
        }
    }
}
