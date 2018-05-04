using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Rect>)), LogicSystemBrowsable(true)]
    public struct Rect
    {
        internal float rA;
        internal float ra;
        internal float rB;
        internal float rb;
        public static readonly Rect Zero;
        public static readonly Rect Cleared;
        public Rect(Rect source)
        {
            this.rA = source.rA;
            this.ra = source.ra;
            this.rB = source.rB;
            this.rb = source.rb;
        }

        public Rect(float left, float top, float right, float bottom)
        {
            this.rA = left;
            this.ra = top;
            this.rB = right;
            this.rb = bottom;
        }

        public Rect(Vec2 leftTop, Vec2 rightBottom)
        {
            this.rA = leftTop.X;
            this.ra = leftTop.Y;
            this.rB = rightBottom.X;
            this.rb = rightBottom.Y;
        }

        public Rect(Vec2 v)
        {
            this.rA = v.X;
            this.rB = v.X;
            this.ra = v.Y;
            this.rb = v.Y;
        }

        static Rect()
        {
            Zero = new Rect(0f, 0f, 0f, 0f);
            Cleared = new Rect(new Vec2(1E+30f, 1E+30f), new Vec2(-1E+30f, -1E+30f));
        }

        [LogicSystemMethodDisplay("Rect( Single left, Single top, Single right, Single bottom )", "Rect( {0}, {1}, {2}, {3} )")]
        public static Rect Construct(float left, float top, float right, float bottom)
        {
            return new Rect(left, top, right, bottom);
        }

        [LogicSystemBrowsable(true), DefaultValue((float)0f)]
        public float Left
        {
            get
            {
                return this.rA;
            }
            set
            {
                this.rA = value;
            }
        }
        [DefaultValue((float)0f), LogicSystemBrowsable(true)]
        public float Top
        {
            get
            {
                return this.ra;
            }
            set
            {
                this.ra = value;
            }
        }
        [DefaultValue((float)0f), LogicSystemBrowsable(true)]
        public float Right
        {
            get
            {
                return this.rB;
            }
            set
            {
                this.rB = value;
            }
        }
        [LogicSystemBrowsable(true), DefaultValue((float)0f)]
        public float Bottom
        {
            get
            {
                return this.rb;
            }
            set
            {
                this.rb = value;
            }
        }
        [LogicSystemBrowsable(true)]
        public static Rect Parse(string text)
        {
            Rect rect;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 4)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 4 parts separated by spaces in the form (left top right bottom).", text));
            }
            try
            {
                rect = new Rect(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return rect;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", new object[] { this.rA, this.ra, this.rB, this.rb });
        }

        [LogicSystemBrowsable(true)]
        public string ToString(int precision)
        {
            string str = "";
            str = str.PadLeft(precision, '#');
            return string.Format("{0:0." + str + "} {1:0." + str + "} {2:0." + str + "} {3:0." + str + "}", new object[] { this.rA, this.ra, this.rB, this.rb });
        }

        public override bool Equals(object obj)
        {
            return ((obj is Rect) && (this == ((Rect)obj)));
        }

        public override int GetHashCode()
        {
            return (((this.rA.GetHashCode() ^ this.ra.GetHashCode()) ^ this.rB.GetHashCode()) ^ this.rb.GetHashCode());
        }

        [LogicSystemBrowsable(true)]
        public static Rect operator +(Rect r, Vec2 v)
        {
            Rect rect;
            rect.rA = r.rA + v.X;
            rect.ra = r.ra + v.Y;
            rect.rB = r.rB + v.X;
            rect.rb = r.rb + v.Y;
            return rect;
        }

        [LogicSystemBrowsable(true)]
        public static Rect operator +(Vec2 v, Rect r)
        {
            Rect rect;
            rect.rA = v.X + r.rA;
            rect.ra = v.Y + r.ra;
            rect.rB = v.X + r.rB;
            rect.rb = v.Y + r.rb;
            return rect;
        }

        [LogicSystemBrowsable(true)]
        public static Rect operator -(Rect r, Vec2 v)
        {
            Rect rect;
            rect.rA = r.rA - v.X;
            rect.ra = r.ra - v.Y;
            rect.rB = r.rB - v.X;
            rect.rb = r.rb - v.Y;
            return rect;
        }

        [LogicSystemBrowsable(true)]
        public static Rect operator -(Vec2 v, Rect r)
        {
            Rect rect;
            rect.rA = v.X - r.rA;
            rect.ra = v.Y - r.ra;
            rect.rB = v.X - r.rB;
            rect.rb = v.Y - r.rb;
            return rect;
        }

        public static Rect Add(Rect r, Vec2 v)
        {
            Rect rect;
            rect.rA = r.rA + v.X;
            rect.ra = r.ra + v.Y;
            rect.rB = r.rB + v.X;
            rect.rb = r.rb + v.Y;
            return rect;
        }

        public static Rect Add(Vec2 v, Rect r)
        {
            Rect rect;
            rect.rA = v.X + r.rA;
            rect.ra = v.Y + r.ra;
            rect.rB = v.X + r.rB;
            rect.rb = v.Y + r.rb;
            return rect;
        }

        public static Rect Subtract(Rect r, Vec2 v)
        {
            Rect rect;
            rect.rA = r.rA - v.X;
            rect.ra = r.ra - v.Y;
            rect.rB = r.rB - v.X;
            rect.rb = r.rb - v.Y;
            return rect;
        }

        public static Rect Subtract(Vec2 v, Rect r)
        {
            Rect rect;
            rect.rA = v.X - r.rA;
            rect.ra = v.Y - r.ra;
            rect.rB = v.X - r.rB;
            rect.rb = v.Y - r.rb;
            return rect;
        }

        public static void Add(Rect r, Vec2 v, out Rect result)
        {
            result.rA = r.rA + v.X;
            result.ra = r.ra + v.Y;
            result.rB = r.rB + v.X;
            result.rb = r.rb + v.Y;
        }

        public static void Add(Vec2 v, Rect r, out Rect result)
        {
            result.rA = v.X + r.rA;
            result.ra = v.Y + r.ra;
            result.rB = v.X + r.rB;
            result.rb = v.Y + r.rb;
        }

        public static void Subtract(Rect r, Vec2 v, out Rect result)
        {
            result.rA = r.rA - v.X;
            result.ra = r.ra - v.Y;
            result.rB = r.rB - v.X;
            result.rb = r.rb - v.Y;
        }

        public static void Subtract(Vec2 v, Rect r, out Rect result)
        {
            result.rA = v.X - r.rA;
            result.ra = v.Y - r.ra;
            result.rB = v.X - r.rB;
            result.rb = v.Y - r.rb;
        }

        [LogicSystemBrowsable(true)]
        public static bool operator ==(Rect v1, Rect v2)
        {
            return ((((v1.rA == v2.rA) && (v1.ra == v2.ra)) && (v1.rB == v2.rB)) && (v1.rb == v2.rb));
        }

        [LogicSystemBrowsable(true)]
        public static bool operator !=(Rect v1, Rect v2)
        {
            if (((v1.rA == v2.rA) && (v1.ra == v2.ra)) && (v1.rB == v2.rB))
            {
                return (v1.rb != v2.rb);
            }
            return true;
        }

        [LogicSystemBrowsable(true)]
        public unsafe float this[int index]
        {
            get
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.rA)
                {
                    return numRef[index * 4];
                }
            }
            set
            {
                if ((index < 0) || (index > 3))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.rA)
                {
                    numRef[index * 4] = value;
                }
            }
        }
        public bool Equals(Rect v, float epsilon)
        {
            if (System.Math.Abs((float)(this.rA - v.rA)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.ra - v.ra)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.rB - v.rB)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.rb - v.rb)) > epsilon)
            {
                return false;
            }
            return true;
        }

        [LogicSystemBrowsable(true), Browsable(false)]
        public Vec2 Size
        {
            get
            {
                Vec2 vec;
                vec.x = this.rB - this.rA;
                vec.y = this.rb - this.ra;
                return vec;
            }
            set
            {
                this.rB = this.rA + value.X;
                this.rb = this.ra + value.Y;
            }
        }
        public Vec2 GetSize()
        {
            Vec2 vec;
            vec.x = this.rB - this.rA;
            vec.y = this.rb - this.ra;
            return vec;
        }

        public void GetSize(out Vec2 result)
        {
            result.x = this.rB - this.rA;
            result.y = this.rb - this.ra;
        }

        [Browsable(false), LogicSystemBrowsable(true)]
        public Vec2 LeftTop
        {
            get
            {
                Vec2 vec;
                vec.x = this.rA;
                vec.y = this.ra;
                return vec;
            }
            set
            {
                this.rA = value.X;
                this.ra = value.Y;
            }
        }
        [LogicSystemBrowsable(true), Browsable(false)]
        public Vec2 RightTop
        {
            get
            {
                Vec2 vec;
                vec.x = this.rB;
                vec.y = this.ra;
                return vec;
            }
            set
            {
                this.rB = value.X;
                this.ra = value.Y;
            }
        }
        [Browsable(false), LogicSystemBrowsable(true)]
        public Vec2 LeftBottom
        {
            get
            {
                Vec2 vec;
                vec.x = this.rA;
                vec.y = this.rb;
                return vec;
            }
            set
            {
                this.rA = value.X;
                this.rb = value.Y;
            }
        }
        [Browsable(false), LogicSystemBrowsable(true)]
        public Vec2 RightBottom
        {
            get
            {
                Vec2 vec;
                vec.x = this.rB;
                vec.y = this.rb;
                return vec;
            }
            set
            {
                this.rB = value.X;
                this.rb = value.Y;
            }
        }
        [LogicSystemBrowsable(true), Browsable(false)]
        public Vec2 Minimum
        {
            get
            {
                Vec2 vec;
                vec.x = this.rA;
                vec.y = this.ra;
                return vec;
            }
            set
            {
                this.rA = value.X;
                this.ra = value.Y;
            }
        }
        [Browsable(false), LogicSystemBrowsable(true)]
        public Vec2 Maximum
        {
            get
            {
                Vec2 vec;
                vec.x = this.rB;
                vec.y = this.rb;
                return vec;
            }
            set
            {
                this.rB = value.X;
                this.rb = value.Y;
            }
        }
        public bool IsInvalid()
        {
            if (this.rB >= this.rA)
            {
                return (this.rb < this.ra);
            }
            return true;
        }

        [LogicSystemBrowsable(true)]
        public bool IsContainsPoint(Vec2 p)
        {
            return (((p.X >= this.rA) && (p.Y >= this.ra)) && ((p.X <= this.rB) && (p.Y <= this.rb)));
        }

        public bool IsContainsRect(Rect v)
        {
            return (((v.rA >= this.rA) && (v.ra >= this.ra)) && ((v.rB <= this.rB) && (v.rb <= this.rb)));
        }

        public bool IsContainsRect(ref Rect v)
        {
            return (((v.rA >= this.rA) && (v.ra >= this.ra)) && ((v.rB <= this.rB) && (v.rb <= this.rb)));
        }

        public bool IsIntersectsRect(Rect v)
        {
            return (((v.rB >= this.rA) && (v.rb >= this.ra)) && ((v.rA <= this.rB) && (v.ra <= this.rb)));
        }

        public bool IsIntersectsRect(ref Rect v)
        {
            return (((v.rB >= this.rA) && (v.rb >= this.ra)) && ((v.rA <= this.rB) && (v.ra <= this.rb)));
        }

        [LogicSystemBrowsable(true)]
        public void Add(Vec2 v)
        {
            if (v.X < this.rA)
            {
                this.rA = v.X;
            }
            if (v.X > this.rB)
            {
                this.rB = v.X;
            }
            if (v.Y < this.ra)
            {
                this.ra = v.Y;
            }
            if (v.Y > this.rb)
            {
                this.rb = v.Y;
            }
        }

        public void Add(Rect a)
        {
            if (a.rA < this.rA)
            {
                this.rA = a.rA;
            }
            if (a.ra < this.ra)
            {
                this.ra = a.ra;
            }
            if (a.rB > this.rB)
            {
                this.rB = a.rB;
            }
            if (a.rb > this.rb)
            {
                this.rb = a.rb;
            }
        }

        [LogicSystemBrowsable(true)]
        public void Expand(float d)
        {
            this.rA -= d;
            this.ra -= d;
            this.rB += d;
            this.rb += d;
        }

        [LogicSystemBrowsable(true)]
        public void Expand(Vec2 d)
        {
            this.rA -= d.X;
            this.ra -= d.Y;
            this.rB += d.X;
            this.rb += d.Y;
        }

        [LogicSystemBrowsable(true)]
        public bool IsCleared()
        {
            return (this.rA > this.rB);
        }

        public Rect Intersect(Rect v)
        {
            Rect rect;
            rect.rA = (v.rA > this.rA) ? v.rA : this.rA;
            rect.ra = (v.ra > this.ra) ? v.ra : this.ra;
            rect.rB = (v.rB < this.rB) ? v.rB : this.rB;
            rect.rb = (v.rb < this.rb) ? v.rb : this.rb;
            return rect;
        }

        public void Intersect(ref Rect v, out Rect result)
        {
            result.rA = (v.rA > this.rA) ? v.rA : this.rA;
            result.ra = (v.ra > this.ra) ? v.ra : this.ra;
            result.rB = (v.rB < this.rB) ? v.rB : this.rB;
            result.rb = (v.rb < this.rb) ? v.rb : this.rb;
        }
    }
}
