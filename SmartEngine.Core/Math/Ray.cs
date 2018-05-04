using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Ray>))]
    public struct Ray
    {
        internal Vec3 ri;
        internal Vec3 rJ;
        public Ray(Ray source)
        {
            this.ri = source.ri;
            this.rJ = source.rJ;
        }

        public Ray(Vec3 origin, Vec3 direction)
        {
            this.ri = origin;
            this.rJ = direction;
        }

        public Vec3 Origin
        {
            get
            {
                return this.ri;
            }
            set
            {
                this.ri = value;
            }
        }
        public Vec3 Direction
        {
            get
            {
                return this.rJ;
            }
            set
            {
                this.rJ = value;
            }
        }
        public override bool Equals(object obj)
        {
            return ((obj is Ray) && (this == ((Ray)obj)));
        }

        public override int GetHashCode()
        {
            return (this.ri.GetHashCode() ^ this.rJ.GetHashCode());
        }

        public static bool operator ==(Ray v1, Ray v2)
        {
            return ((v1.ri == v2.ri) && (v1.rJ == v2.rJ));
        }

        public static bool operator !=(Ray v1, Ray v2)
        {
            if (!(v1.ri != v2.ri))
            {
                return (v1.rJ != v2.rJ);
            }
            return true;
        }

        public bool Equals(Ray v, float epsilon)
        {
            if (!this.ri.Equals(v.ri, epsilon))
            {
                return false;
            }
            if (!this.rJ.Equals(v.rJ, epsilon))
            {
                return false;
            }
            return true;
        }

        public Vec3 GetPointOnRay(float t)
        {
            Vec3 vec;
            vec.x = this.ri.x + (this.rJ.x * t);
            vec.y = this.ri.y + (this.rJ.y * t);
            vec.z = this.ri.z + (this.rJ.z * t);
            return vec;
        }

        public void GetPointOnRay(float t, out Vec3 result)
        {
            result.x = this.ri.x + (this.rJ.x * t);
            result.y = this.ri.y + (this.rJ.y * t);
            result.z = this.ri.z + (this.rJ.z * t);
        }
    }
}
