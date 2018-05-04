using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(DegreeTypeConverter))]
    public struct Degree : IComparable<Degree>, IComparable<float>
    {
        public static readonly Degree Zero;
        private float degree;
        public Degree(float r)
        {
            this.degree = r;
        }

        public Degree(int r)
        {
            this.degree = r;
        }

        public Degree(Degree d)
        {
            this.degree = d.degree;
        }

        public Degree(Radian r)
        {
            this.degree = (float)r.InDegrees();
        }

        static Degree()
        {
            Zero = new Degree(0f);
        }

        public Radian InRadians()
        {
            return MathFunctions.DegToRad(this.degree);
        }

        public static implicit operator Degree(float value)
        {
            return new Degree(value);
        }

        public static implicit operator Degree(int value)
        {
            return new Degree((float)value);
        }

        public static implicit operator Degree(Radian value)
        {
            return new Degree(value);
        }

        public static implicit operator float(Degree value)
        {
            return value.degree;
        }

        public static Degree operator +(Degree left, float right)
        {
            return (left.degree + right);
        }

        public static Degree operator +(Degree left, int right)
        {
            return (left.degree + right);
        }

        public static Degree operator +(Degree left, Degree right)
        {
            return (left.degree + right.degree);
        }

        public static Degree operator +(Degree left, Radian right)
        {
            return (left + right.InDegrees());
        }

        public static Degree operator -(Degree r)
        {
            return -r.degree;
        }

        public static Degree operator -(Degree left, float right)
        {
            return (left.degree - right);
        }

        public static Degree operator -(Degree left, int right)
        {
            return (left.degree - right);
        }

        public static Degree operator -(Degree left, Degree right)
        {
            return (left.degree - right.degree);
        }

        public static Degree operator -(Degree left, Radian right)
        {
            return (left - right.InDegrees());
        }

        public static Degree operator *(Degree left, float right)
        {
            return (left.degree * right);
        }

        public static Degree operator *(Degree left, int right)
        {
            return (left.degree * right);
        }

        public static Degree operator *(float left, Degree right)
        {
            return (left * right.degree);
        }

        public static Degree operator *(int left, Degree right)
        {
            return (left * right.degree);
        }

        public static Degree operator *(Degree left, Degree right)
        {
            return (left.degree * right.degree);
        }

        public static Degree operator *(Degree left, Radian right)
        {
            return (left.degree * right.InDegrees());
        }

        public static Degree operator /(Degree left, float right)
        {
            return (left.degree / right);
        }

        public static bool operator <(Degree left, Degree right)
        {
            return (left.degree < right.degree);
        }

        public static bool operator ==(Degree left, Degree right)
        {
            return (left.degree == right.degree);
        }

        public static bool operator !=(Degree left, Degree right)
        {
            return (left.degree != right.degree);
        }

        public static bool operator >(Degree left, Degree right)
        {
            return (left.degree > right.degree);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Degree) && (this == ((Degree)obj)));
        }

        public override int GetHashCode()
        {
            return this.degree.GetHashCode();
        }

        public int CompareTo(Degree other)
        {
            return this.degree.CompareTo((float)other);
        }

        public int CompareTo(Radian other)
        {
            return this.degree.CompareTo((float)other.InDegrees());
        }

        public int CompareTo(float other)
        {
            return this.degree.CompareTo(other);
        }

        [LogicSystemBrowsable(true)]
        public static Degree Parse(string text)
        {
            return new Degree(float.Parse(text));
        }

        public override string ToString()
        {
            return this.degree.ToString();
        }
    }
}
