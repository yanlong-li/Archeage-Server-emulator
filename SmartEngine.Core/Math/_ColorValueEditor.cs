using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Reflection;

namespace SmartEngine.Core.Math
{
    public class _ColorValueEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                bool flag;
                ColorValue value2;
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
                    _MathExEditorBridge.Instance.ColorValueEditorControlShow(provider, propertyOwners, property, out flag, out value2);
                }
                else
                {
                    _MathExEditorBridge.Instance.ColorValueEditorControlShow(provider, (ColorValue)value, out flag, out value2);
                }
                if (flag)
                {
                    return value2;
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
                ColorValue value2 = (ColorValue)e.Value;
                int[] numArray = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    int num2 = (int)(value2[i] * 255f);
                    if (num2 < 0)
                    {
                        num2 = 0;
                    }
                    if (num2 > 255)
                    {
                        num2 = 255;
                    }
                    numArray[i] = num2;
                }
                if (value2.Alpha != 1f)
                {
                    using (HatchBrush brush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.FromArgb(128, 128, 128), Color.FromArgb(192, 192, 192)))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                }
                Color color = Color.FromArgb(255, numArray[0], numArray[1], numArray[2]);
                Color color2 = Color.FromArgb(numArray[3], numArray[0], numArray[1], numArray[2]);
                using (LinearGradientBrush brush2 = new LinearGradientBrush(e.Bounds, color, color2, 90f, false))
                {
                    e.Graphics.FillRectangle(brush2, e.Bounds);
                }
            }
            base.PaintValue(e);
        }
    }
}
