using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace SmartEngine.Core.Math
{
    public abstract class _MathExEditorBridge
    {
        private static _MathExEditorBridge instance;

        protected _MathExEditorBridge()
        {
            instance = this;
        }

        private static Assembly A(string A)
        {
            Assembly assembly2;
            string extension = Path.GetExtension(A);
            if (string.IsNullOrEmpty(extension) || (extension.ToLower() != ".dll"))
            {
                A = A + ".dll";
            }
            AssemblyName assemblyName = AssemblyName.GetAssemblyName(A);
            if (assemblyName == null)
            {
                Log.Fatal("Assembly not found \"{0}\".", A);
            }
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName == assemblyName.FullName)
                {
                    return assembly;
                }
            }
            try
            {
                assembly2 = AppDomain.CurrentDomain.Load(assemblyName);
            }
            catch
            {
                Log.Fatal("Load assembly failed \"{0}\".", assemblyName.FullName);
                return null;
            }
            return assembly2;
        }

        public abstract void ColorValueEditorControlShow(IServiceProvider provider, ColorValue colorValue, out bool changed, out ColorValue newValue);
        public abstract void ColorValueEditorControlShow(IServiceProvider provider, object[] propertyOwners, PropertyInfo property, out bool changed, out ColorValue newValue);
        public abstract void ColorValueRangeEditorControlShow(IServiceProvider provider, ColorValueRange colorValueRange, out bool changed, out ColorValueRange newValue);
        public abstract void ColorValueRangeEditorControlShow(IServiceProvider provider, object[] propertyOwners, PropertyInfo property, out bool changed, out ColorValueRange newValue);
        public abstract void RangeEditorControlShow(IServiceProvider provider, Range value, Range limitsRange, out bool changed, out Range newValue);
        public abstract void RangeEditorControlShow(IServiceProvider provider, object[] propertyOwners, PropertyInfo property, Range limitsRange, out bool changed, out Range newValue);
        public abstract void SingleEditorControlShow(IServiceProvider provider, float value, Range limitsRange, out bool changed, out float newValue);
        public abstract void SingleEditorControlShow(IServiceProvider provider, object[] propertyOwners, PropertyInfo property, Range limitsRange, out bool changed, out float newValue);
        public abstract void Vec2EditorControlShow(IServiceProvider provider, Vec2 value, Range limitsRange, out bool changed, out Vec2 newValue);
        public abstract void Vec2EditorControlShow(IServiceProvider provider, object[] propertyOwners, PropertyInfo property, Range limitsRange, out bool changed, out Vec2 newValue);

        public static _MathExEditorBridge Instance
        {
            get
            {
                if (instance == null)
                {
                    A("MathEx.Editor").GetType("Engine.MathEx.Editor.MathExEditorBridgeImpl").GetMethod("Init").Invoke(null, new object[0]);
                }
                return instance;
            }
        }
    }
}
