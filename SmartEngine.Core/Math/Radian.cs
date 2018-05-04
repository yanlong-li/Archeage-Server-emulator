using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(RadianTypeConverter))]
    public struct Radian : IComparable<float>, IComparable<Radian>
    {
        public static readonly Radian Zero;
        private float radian;
        public Radian(float r)
        {
            this.radian = r;
        }

        public Radian(int r)
        {
            this.radian = r;
        }

        public Radian(Radian r)
        {
            this.radian = r.radian;
        }

        public Radian(Degree d)
        {
            this.radian = (float)d.InRadians();
        }

        static Radian()
        {
            Zero = new Radian(0f);
        }

        public Degree InDegrees()
        {
            return MathFunctions.RadToDeg(this.radian);
        }

        public static implicit operator Radian(float value)
        {
            return new Radian(value);
        }

        public static implicit operator Radian(int value)
        {
            return new Radian((float)value);
        }

        public static implicit operator Radian(Degree value)
        {
            return new Radian(value);
        }

        public static implicit operator float(Radian value)
        {
            return value.radian;
        }

        public static Radian operator +(Radian left, float right)
        {
            return (left.radian + right);
        }

        public static Radian operator +(Radian left, int right)
        {
            return (left.radian + right);
        }

        public static Radian operator +(Radian left, Radian right)
        {
            return (left.radian + right.radian);
        }

        public static Radian operator +(Radian left, Degree right)
        {
            return (left + right.InRadians());
        }

        public static Radian operator -(Radian r)
        {
            return -r.radian;
        }

        public static Radian operator -(Radian left, float right)
        {
            return (left.radian - right);
        }

        public static Radian operator -(Radian left, int right)
        {
            return (left.radian - right);
        }

        public static Radian operator -(Radian left, Radian right)
        {
            return (left.radian - right.radian);
        }

        public static Radian operator -(Radian left, Degree right)
        {
            return (left - right.InRadians());
        }

        public static Radian operator *(Radian left, float right)
        {
            return (left.radian * right);
        }

        public static Radian operator *(Radian left, int right)
        {
            return (left.radian * right);
        }

        public static Radian operator *(float left, Radian right)
        {
            return (left * right.radian);
        }

        public static Radian operator *(int left, Radian right)
        {
            return (left * right.radian);
        }

        public static Radian operator *(Radian left, Radian right)
        {
            return (left.radian * right.radian);
        }

        public static Radian operator *(Radian left, Degree right)
        {
            return (left.radian * right.InRadians());
        }

        public static Radian operator /(Radian left, float right)
        {
            return (left.radian / right);
        }

        public static bool operator <(Radian left, Radian right)
        {
            return (left.radian < right.radian);
        }

        public static bool operator ==(Radian left, Radian right)
        {
            return (left.radian == right.radian);
        }

        public static bool operator !=(Radian left, Radian right)
        {
            return (left.radian != right.radian);
        }

        public static bool operator >(Radian left, Radian right)
        {
            return (left.radian > right.radian);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Radian) && (this == ((Radian)obj)));
        }

        public override int GetHashCode()
        {
            return this.radian.GetHashCode();
        }

        public int CompareTo(Radian other)
        {
            return this.radian.CompareTo(other.radian);
        }

        public int CompareTo(Degree other)
        {
            return this.radian.CompareTo((float)other.InRadians());
        }

        public int CompareTo(float other)
        {
            return this.radian.CompareTo(other);
        }

        [LogicSystemBrowsable(true)]
        public static Radian Parse(string text)
        {
            return new Radian(float.Parse(text));
        }

        public override string ToString()
        {
            return this.radian.ToString();
        }
    }
}
