using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Bounds>))]
    public struct Bounds
    {
        internal Vec3 rf;
        internal Vec3 rG;
        public static readonly Bounds Zero;
        public static readonly Bounds Cleared;
        public Bounds(Bounds source)
        {
            this.rf = source.rf;
            this.rG = source.rG;
        }

        public Bounds(Vec3 minimum, Vec3 maximum)
        {
            this.rf = minimum;
            this.rG = maximum;
        }

        public Bounds(float minimumX, float minimumY, float minimumZ, float maximumX, float maximumY, float maximumZ)
        {
            this.rf = new Vec3(minimumX, minimumY, minimumZ);
            this.rG = new Vec3(maximumX, maximumY, maximumZ);
        }

        public Bounds(Vec3 v)
        {
            this.rf = v;
            this.rG = v;
        }

        static Bounds()
        {
            Zero = new Bounds(Vec3.Zero, Vec3.Zero);
            Cleared = new Bounds(new Vec3(1E+30f, 1E+30f, 1E+30f), new Vec3(-1E+30f, -1E+30f, -1E+30f));
        }

        [DefaultValue(typeof(Vec3), "0 0 0")]
        public Vec3 Minimum
        {
            get
            {
                return this.rf;
            }
            set
            {
                this.rf = value;
            }
        }
        [DefaultValue(typeof(Vec3), "0 0 0")]
        public Vec3 Maximum
        {
            get
            {
                return this.rG;
            }
            set
            {
                this.rG = value;
            }
        }
        public override bool Equals(object obj)
        {
            return ((obj is Bounds) && (this == ((Bounds)obj)));
        }

        public override int GetHashCode()
        {
            return (this.rf.GetHashCode() ^ this.rG.GetHashCode());
        }

        public static bool operator ==(Bounds v1, Bounds v2)
        {
            return ((v1.rf == v2.rf) && (v1.rG == v2.rG));
        }

        public static bool operator !=(Bounds v1, Bounds v2)
        {
            if (!(v1.rf != v2.rf))
            {
                return (v1.rG != v2.rG);
            }
            return true;
        }

        public unsafe Vec3 this[int index]
        {
            get
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (Vec3* vecRef = &this.rf)
                {
                    return vecRef[index];
                }
            }
            set
            {
                if ((index < 0) || (index > 1))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (Vec3* vecRef = &this.rf)
                {
                    vecRef[index] = value;
                }
            }
        }
        public bool Equals(Bounds v, float epsilon)
        {
            if (!this.rf.Equals(ref v.rf, epsilon))
            {
                return false;
            }
            if (!this.rG.Equals(ref v.rG, epsilon))
            {
                return false;
            }
            return true;
        }

        public bool IsCleared()
        {
            return (this.rf.X > this.rG.X);
        }

        public Vec3 GetCenter()
        {
            Vec3 vec;
            vec.x = (this.rf.x + this.rG.x) * 0.5f;
            vec.y = (this.rf.y + this.rG.y) * 0.5f;
            vec.z = (this.rf.z + this.rG.z) * 0.5f;
            return vec;
        }

        public void GetCenter(out Vec3 result)
        {
            result.x = (this.rf.x + this.rG.x) * 0.5f;
            result.y = (this.rf.y + this.rG.y) * 0.5f;
            result.z = (this.rf.z + this.rG.z) * 0.5f;
        }

        public float GetRadius()
        {
            float x = 0f;
            for (int i = 0; i < 3; i++)
            {
                float num3 = System.Math.Abs(this.Minimum[i]);
                float num4 = System.Math.Abs(this.Maximum[i]);
                if (num3 > num4)
                {
                    x += num3 * num3;
                }
                else
                {
                    x += num4 * num4;
                }
            }
            return MathFunctions.Sqrt(x);
        }

        public float GetRadius(Vec3 center)
        {
            float x = 0f;
            for (int i = 0; i < 3; i++)
            {
                float num3 = System.Math.Abs((float)(center[i] - this.Minimum[i]));
                float num4 = System.Math.Abs((float)(this.Maximum[i] - center[i]));
                if (num3 > num4)
                {
                    x += num3 * num3;
                }
                else
                {
                    x += num4 * num4;
                }
            }
            return MathFunctions.Sqrt(x);
        }

        public float GetVolume()
        {
            Vec3 vec;
            Vec3.Subtract(ref this.rG, ref this.rf, out vec);
            return ((vec.X * vec.Y) * vec.Z);
        }

        public void Add(Vec3 v)
        {
            if (v.X < this.rf.X)
            {
                this.rf.X = v.X;
            }
            if (v.X > this.rG.X)
            {
                this.rG.X = v.X;
            }
            if (v.Y < this.rf.Y)
            {
                this.rf.Y = v.Y;
            }
            if (v.Y > this.rG.Y)
            {
                this.rG.Y = v.Y;
            }
            if (v.Z < this.rf.Z)
            {
                this.rf.Z = v.Z;
            }
            if (v.Z > this.rG.Z)
            {
                this.rG.Z = v.Z;
            }
        }

        public void Add(ref Vec3 v)
        {
            if (v.X < this.rf.X)
            {
                this.rf.X = v.X;
            }
            if (v.X > this.rG.X)
            {
                this.rG.X = v.X;
            }
            if (v.Y < this.rf.Y)
            {
                this.rf.Y = v.Y;
            }
            if (v.Y > this.rG.Y)
            {
                this.rG.Y = v.Y;
            }
            if (v.Z < this.rf.Z)
            {
                this.rf.Z = v.Z;
            }
            if (v.Z > this.rG.Z)
            {
                this.rG.Z = v.Z;
            }
        }

        public void Add(Bounds v)
        {
            if (v.rf.X < this.rf.X)
            {
                this.rf.X = v.rf.X;
            }
            if (v.rf.Y < this.rf.Y)
            {
                this.rf.Y = v.rf.Y;
            }
            if (v.rf.Z < this.rf.Z)
            {
                this.rf.Z = v.rf.Z;
            }
            if (v.rG.X > this.rG.X)
            {
                this.rG.X = v.rG.X;
            }
            if (v.rG.Y > this.rG.Y)
            {
                this.rG.Y = v.rG.Y;
            }
            if (v.rG.Z > this.rG.Z)
            {
                this.rG.Z = v.rG.Z;
            }
        }

        public void Add(ref Bounds v)
        {
            if (v.rf.X < this.rf.X)
            {
                this.rf.X = v.rf.X;
            }
            if (v.rf.Y < this.rf.Y)
            {
                this.rf.Y = v.rf.Y;
            }
            if (v.rf.Z < this.rf.Z)
            {
                this.rf.Z = v.rf.Z;
            }
            if (v.rG.X > this.rG.X)
            {
                this.rG.X = v.rG.X;
            }
            if (v.rG.Y > this.rG.Y)
            {
                this.rG.Y = v.rG.Y;
            }
            if (v.rG.Z > this.rG.Z)
            {
                this.rG.Z = v.rG.Z;
            }
        }

        public Bounds Intersect(Bounds v)
        {
            Bounds bounds;
            bounds.rf.x = (v.rf.X > this.rf.X) ? v.rf.X : this.rf.X;
            bounds.rf.y = (v.rf.Y > this.rf.Y) ? v.rf.Y : this.rf.Y;
            bounds.rf.z = (v.rf.Z > this.rf.Z) ? v.rf.Z : this.rf.Z;
            bounds.rG.x = (v.rG.X < this.rG.X) ? v.rG.X : this.rG.X;
            bounds.rG.y = (v.rG.Y < this.rG.Y) ? v.rG.Y : this.rG.Y;
            bounds.rG.z = (v.rG.Z < this.rG.Z) ? v.rG.Z : this.rG.Z;
            return bounds;
        }

        public void Intersect(ref Bounds v, out Bounds result)
        {
            result.rf.x = (v.rf.X > this.rf.X) ? v.rf.X : this.rf.X;
            result.rf.y = (v.rf.Y > this.rf.Y) ? v.rf.Y : this.rf.Y;
            result.rf.z = (v.rf.Z > this.rf.Z) ? v.rf.Z : this.rf.Z;
            result.rG.x = (v.rG.X < this.rG.X) ? v.rG.X : this.rG.X;
            result.rG.y = (v.rG.Y < this.rG.Y) ? v.rG.Y : this.rG.Y;
            result.rG.z = (v.rG.Z < this.rG.Z) ? v.rG.Z : this.rG.Z;
        }

        public void Expand(float d)
        {
            this.rf.X -= d;
            this.rf.Y -= d;
            this.rf.Z -= d;
            this.rG.X += d;
            this.rG.Y += d;
            this.rG.Z += d;
        }

        public void Expand(Vec3 d)
        {
            this.rf.X -= d.X;
            this.rf.Y -= d.Y;
            this.rf.Z -= d.Z;
            this.rG.X += d.X;
            this.rG.Y += d.Y;
            this.rG.Z += d.Z;
        }

        public bool IsContainsPoint(Vec3 p)
        {
            return ((((p.X >= this.rf.X) && (p.Y >= this.rf.Y)) && ((p.Z >= this.rf.Z) && (p.X <= this.rG.X))) && ((p.Y <= this.rG.Y) && (p.Z <= this.rG.Z)));
        }

        public bool IsContainsBounds(Bounds v)
        {
            return ((((v.rf.X >= this.rf.X) && (v.rf.Y >= this.rf.Y)) && ((v.rf.Z >= this.rf.Z) && (v.rG.X <= this.rG.X))) && ((v.rG.Y <= this.rG.Y) && (v.rG.Z <= this.rG.Z)));
        }

        public bool IsContainsBounds(ref Bounds v)
        {
            return ((((v.rf.X >= this.rf.X) && (v.rf.Y >= this.rf.Y)) && ((v.rf.Z >= this.rf.Z) && (v.rG.X <= this.rG.X))) && ((v.rG.Y <= this.rG.Y) && (v.rG.Z <= this.rG.Z)));
        }

        public bool IsIntersectsBounds(Bounds v)
        {
            return ((((v.rG.X >= this.rf.X) && (v.rG.Y >= this.rf.Y)) && ((v.rG.Z >= this.rf.Z) && (v.rf.X <= this.rG.X))) && ((v.rf.Y <= this.rG.Y) && (v.rf.Z <= this.rG.Z)));
        }

        public bool IsIntersectsBounds(ref Bounds v)
        {
            return ((((v.rG.X >= this.rf.X) && (v.rG.Y >= this.rf.Y)) && ((v.rG.Z >= this.rf.Z) && (v.rf.X <= this.rG.X))) && ((v.rf.Y <= this.rG.Y) && (v.rf.Z <= this.rG.Z)));
        }

        public Vec3 GetSize()
        {
            Vec3 vec;
            Vec3.Subtract(ref this.rG, ref this.rf, out vec);
            return vec;
        }

        public void GetSize(out Vec3 result)
        {
            Vec3.Subtract(ref this.rG, ref this.rf, out result);
        }

        public void ToPoints(ref Vec3[] points)
        {
            if ((points == null) || (points.Length < 8))
            {
                points = new Vec3[8];
            }
            for (int i = 0; i < 8; i++)
            {
                Vec3 vec = this[(i ^ (i >> 1)) & 1];
                points[i].X = vec.X;
                Vec3 vec2 = this[(i >> 1) & 1];
                points[i].Y = vec2.Y;
                Vec3 vec3 = this[(i >> 2) & 1];
                points[i].Z = vec3.Z;
            }
        }

        public static Bounds Parse(string text)
        {
            Bounds bounds;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 6)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 6 parts separated by spaces in the form (x y z x y z).", text));
            }
            try
            {
                bounds = new Bounds(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]), float.Parse(strArray[4]), float.Parse(strArray[5]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers.");
            }
            return bounds;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", new object[] { this.Minimum.X, this.Minimum.Y, this.Minimum.Z, this.Maximum.X, this.Maximum.Y, this.Maximum.Z });
        }

        public bool RayIntersection(ref Ray ray, out float scale)
        {
            Vec3 zero = Vec3.Zero;
            scale = 0f;
            int num2 = -1;
            int num6 = 0;
            for (int i = 0; i < 3; i++)
            {
                int num5;
                if (ray.ri[i] < this.Minimum[i])
                {
                    num5 = 0;
                }
                else if (ray.ri[i] > this.Maximum[i])
                {
                    num5 = 1;
                }
                else
                {
                    num6++;
                    continue;
                }
                if (ray.rJ[i] != 0f)
                {
                    Vec3 vec4 = this[num5];
                    float num7 = ray.ri[i] - vec4[i];
                    if ((num2 < 0) || (System.Math.Abs(num7) > System.Math.Abs((float)(scale * ray.rJ[i]))))
                    {
                        scale = -(num7 / ray.rJ[i]);
                        num2 = i;
                    }
                }
            }
            if ((scale < 0f) || (scale > 1f))
            {
                return false;
            }
            if (num2 < 0)
            {
                scale = 0f;
                return (num6 == 3);
            }
            int num3 = (num2 + 1) % 3;
            int num4 = (num2 + 2) % 3;
            zero[num3] = ray.ri[num3] + (scale * ray.rJ[num3]);
            zero[num4] = ray.ri[num4] + (scale * ray.rJ[num4]);
            return ((((zero[num3] >= this.Minimum[num3]) && (zero[num3] <= this.Maximum[num3])) && (zero[num4] >= this.Minimum[num4])) && (zero[num4] <= this.Maximum[num4]));
        }

        public bool RayIntersection(Ray ray, out float scale)
        {
            return this.RayIntersection(ref ray, out scale);
        }

        public bool RayIntersection(ref Ray ray)
        {
            float num;
            return this.RayIntersection(ref ray, out num);
        }

        public bool RayIntersection(Ray ray)
        {
            float num;
            return this.RayIntersection(ref ray, out num);
        }

        public static Bounds operator +(Bounds b, Vec3 v)
        {
            Bounds bounds;
            Vec3.Add(ref b.rf, ref v, out bounds.rf);
            Vec3.Add(ref b.rG, ref v, out bounds.rG);
            return bounds;
        }

        public static Bounds operator +(Vec3 v, Bounds b)
        {
            Bounds bounds;
            Vec3.Add(ref v, ref b.rf, out bounds.rf);
            Vec3.Add(ref v, ref b.rG, out bounds.rG);
            return bounds;
        }

        public static Bounds operator -(Bounds b, Vec3 v)
        {
            Bounds bounds;
            Vec3.Subtract(ref b.rf, ref v, out bounds.rf);
            Vec3.Subtract(ref b.rG, ref v, out bounds.rG);
            return bounds;
        }

        public static Bounds operator -(Vec3 v, Bounds b)
        {
            Bounds bounds;
            Vec3.Subtract(ref v, ref b.rf, out bounds.rf);
            Vec3.Subtract(ref v, ref b.rG, out bounds.rG);
            return bounds;
        }

        public static void Add(ref Bounds b, ref Vec3 v, out Bounds result)
        {
            Vec3.Add(ref b.rf, ref v, out result.rf);
            Vec3.Add(ref b.rG, ref v, out result.rG);
        }

        public static void Add(ref Vec3 v, ref Bounds b, out Bounds result)
        {
            Vec3.Add(ref v, ref b.rf, out result.rf);
            Vec3.Add(ref v, ref b.rG, out result.rG);
        }

        public static void Subtract(ref Bounds b, ref Vec3 v, out Bounds result)
        {
            Vec3.Subtract(ref b.rf, ref v, out result.rf);
            Vec3.Subtract(ref b.rG, ref v, out result.rG);
        }

        public static void Subtract(ref Vec3 v, ref Bounds b, out Bounds result)
        {
            Vec3.Subtract(ref v, ref b.rf, out result.rf);
            Vec3.Subtract(ref v, ref b.rG, out result.rG);
        }

        public static Bounds Add(ref Bounds b, ref Vec3 v)
        {
            Bounds bounds;
            Vec3.Add(ref b.rf, ref v, out bounds.rf);
            Vec3.Add(ref b.rG, ref v, out bounds.rG);
            return bounds;
        }

        public static Bounds Add(ref Vec3 v, ref Bounds b)
        {
            Bounds bounds;
            Vec3.Add(ref v, ref b.rf, out bounds.rf);
            Vec3.Add(ref v, ref b.rG, out bounds.rG);
            return bounds;
        }

        public static Bounds Subtract(ref Bounds b, ref Vec3 v)
        {
            Bounds bounds;
            Vec3.Subtract(ref b.rf, ref v, out bounds.rf);
            Vec3.Subtract(ref b.rG, ref v, out bounds.rG);
            return bounds;
        }

        public static Bounds Subtract(ref Vec3 v, ref Bounds b)
        {
            Bounds bounds;
            Vec3.Subtract(ref v, ref b.rf, out bounds.rf);
            Vec3.Subtract(ref v, ref b.rG, out bounds.rG);
            return bounds;
        }

        public Plane.Side GetPlaneSide(ref Plane plane)
        {
            Vec3 vec;
            this.GetCenter(out vec);
            float distance = plane.GetDistance(ref vec);
            float num2 = (System.Math.Abs((float)((this.Maximum.X - vec.X) * plane.RI)) + System.Math.Abs((float)((this.Maximum.Y - vec.Y) * plane.Ri))) + System.Math.Abs((float)((this.Maximum.Z - vec.Z) * plane.RJ));
            if ((distance - num2) > 0f)
            {
                return Plane.Side.Positive;
            }
            if ((distance + num2) < 0f)
            {
                return Plane.Side.Negative;
            }
            return Plane.Side.No;
        }

        public Plane.Side GetPlaneSide(Plane plane)
        {
            return this.GetPlaneSide(ref plane);
        }

        public float GetPlaneDistance(ref Plane plane)
        {
            Vec3 vec;
            this.GetCenter(out vec);
            float distance = plane.GetDistance(ref vec);
            float num2 = (System.Math.Abs((float)((this.Maximum.X - vec.X) * plane.Normal.X)) + System.Math.Abs((float)((this.Maximum.Y - vec.Y) * plane.Normal.Y))) + System.Math.Abs((float)((this.Maximum.Z - vec.Z) * plane.Normal.Z));
            if ((distance - num2) > 0f)
            {
                return (distance - num2);
            }
            if ((distance + num2) < 0f)
            {
                return (distance + num2);
            }
            return 0f;
        }

        public float GetPlaneDistance(Plane plane)
        {
            return this.GetPlaneDistance(ref plane);
        }

        internal bool A(ref Vec3 A, ref Vec3 a)
        {
            Vec3 vec;
            Vec3 vec2;
            Vec3 vec3;
            Vec3 vec4;
            Vec3 vec5;
            Vec3 vec6;
            this.GetCenter(out vec);
            Vec3.Subtract(ref this.rG, ref vec, out vec2);
            vec3.x = 0.5f * (a.x - A.x);
            vec3.y = 0.5f * (a.y - A.y);
            vec3.z = 0.5f * (a.z - A.z);
            Vec3.Add(ref A, ref vec3, out vec4);
            Vec3.Subtract(ref vec4, ref vec, out vec5);
            float num = System.Math.Abs(vec3.X);
            if (System.Math.Abs(vec5.X) > (vec2.X + num))
            {
                return false;
            }
            float num2 = System.Math.Abs(vec3.Y);
            if (System.Math.Abs(vec5.Y) > (vec2.Y + num2))
            {
                return false;
            }
            float num3 = System.Math.Abs(vec3.Z);
            if (System.Math.Abs(vec5.Z) > (vec2.Z + num3))
            {
                return false;
            }
            Vec3.Cross(ref vec3, ref vec5, out vec6);
            if (System.Math.Abs(vec6.X) > ((vec2.Y * num3) + (vec2.Z * num2)))
            {
                return false;
            }
            if (System.Math.Abs(vec6.Y) > ((vec2.X * num3) + (vec2.Z * num)))
            {
                return false;
            }
            if (System.Math.Abs(vec6.Z) > ((vec2.X * num2) + (vec2.Y * num)))
            {
                return false;
            }
            return true;
        }

        public Rect ToRect()
        {
            Rect rect;
            rect.rA = this.rf.X;
            rect.ra = this.rf.Y;
            rect.rB = this.rG.X;
            rect.rb = this.rG.Y;
            return rect;
        }

        public void ToRect(out Rect result)
        {
            result.rA = this.rf.X;
            result.ra = this.rf.Y;
            result.rB = this.rG.X;
            result.rb = this.rG.Y;
        }

        public float GetPointDistance(Vec3 point)
        {
            float num;
            float num2;
            float num3;
            if (point.x < this.rf.x)
            {
                num = this.rf.x - point.x;
            }
            else if (point.x > this.rG.x)
            {
                num = point.x - this.rG.x;
            }
            else
            {
                num = 0f;
            }
            if (point.y < this.rf.y)
            {
                num2 = this.rf.y - point.y;
            }
            else if (point.y > this.rG.y)
            {
                num2 = point.y - this.rG.y;
            }
            else
            {
                num2 = 0f;
            }
            if (point.z < this.rf.z)
            {
                num3 = this.rf.z - point.z;
            }
            else if (point.z > this.rG.z)
            {
                num3 = point.z - this.rG.z;
            }
            else
            {
                num3 = 0f;
            }
            float x = ((num * num) + (num2 * num2)) + (num3 * num3);
            if (x == 0f)
            {
                return 0f;
            }
            return MathFunctions.Sqrt(x);
        }
    }
}
