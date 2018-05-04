using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace SmartEngine.Core.Math
{
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(_GeneralTypeConverter<Angles>))]
    public struct Angles
    {
        internal float roll;
        internal float pitch;
        internal float yaw;
        public static readonly Angles Zero;
        public Angles(Vec3 v)
        {
            this.roll = v.Z;
            this.pitch = v.X;
            this.yaw = v.Y;
        }

        public Angles(float roll, float pitch, float yaw)
        {
            this.roll = roll;
            this.pitch = pitch;
            this.yaw = yaw;
        }

        public Angles(Angles source)
        {
            this.roll = source.roll;
            this.pitch = source.pitch;
            this.yaw = source.yaw;
        }

        static Angles()
        {
            Zero = new Angles(0f, 0f, 0f);
        }

        [DefaultValue((float)0f)]
        public float Roll
        {
            get
            {
                return this.roll;
            }
            set
            {
                this.roll = value;
            }
        }
        [DefaultValue((float)0f)]
        public float Pitch
        {
            get
            {
                return this.pitch;
            }
            set
            {
                this.pitch = value;
            }
        }
        [DefaultValue((float)0f)]
        public float Yaw
        {
            get
            {
                return this.yaw;
            }
            set
            {
                this.yaw = value;
            }
        }
        public static Angles Parse(string text)
        {
            Angles angles;
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("The text parameter cannot be null or zero length.");
            }
            string[] strArray = text.Split(SpaceCharacter.arrayWithOneSpaceCharacter, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 3)
            {
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 4 parts separated by spaces in the form (pitch yaw roll).", text));
            }
            try
            {
                angles = new Angles(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]));
            }
            catch (Exception)
            {
                throw new FormatException("The parts of the angles must be decimal numbers.");
            }
            return angles;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.roll, this.pitch, this.yaw);
        }

        public string ToString(int precision)
        {
            string str = "";
            str = str.PadLeft(precision, '#');
            return string.Format("{0:0." + str + "} {1:0." + str + "} {2:0." + str + "}", this.roll, this.pitch, this.yaw);
        }

        public override bool Equals(object obj)
        {
            return ((obj is Angles) && (this == ((Angles)obj)));
        }

        public override int GetHashCode()
        {
            return ((this.roll.GetHashCode() ^ this.pitch.GetHashCode()) ^ this.yaw.GetHashCode());
        }

        public unsafe float this[int index]
        {
            get
            {
                if ((index < 0) || (index > 2))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.roll)
                {
                    return numRef[index * 4];
                }
            }
            set
            {
                if ((index < 0) || (index > 2))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                fixed (float* numRef = &this.roll)
                {
                    numRef[index * 4] = value;
                }
            }
        }
        public static bool operator ==(Angles a, Angles b)
        {
            return (((a.roll == b.roll) && (a.pitch == b.pitch)) && (a.yaw == b.yaw));
        }

        public static bool operator !=(Angles a, Angles b)
        {
            if ((a.roll == b.roll) && (a.pitch == b.pitch))
            {
                return (a.yaw != b.yaw);
            }
            return true;
        }

        public bool Equals(Angles a, float epsilon)
        {
            if (System.Math.Abs((float)(this.roll - a.roll)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.pitch - a.pitch)) > epsilon)
            {
                return false;
            }
            if (System.Math.Abs((float)(this.yaw - a.yaw)) > epsilon)
            {
                return false;
            }
            return true;
        }

        public static Angles operator -(Angles a)
        {
            return new Angles(-a.roll, -a.pitch, -a.yaw);
        }

        public static Angles operator +(Angles a, Angles b)
        {
            return new Angles(a.roll + b.roll, a.pitch + b.pitch, a.yaw + b.yaw);
        }

        public static Angles operator -(Angles a, Angles b)
        {
            return new Angles(a.roll - b.roll, a.pitch - b.pitch, a.yaw - b.yaw);
        }

        public static Angles operator *(Angles a, float b)
        {
            return new Angles(a.roll * b, a.pitch * b, a.yaw * b);
        }

        public static Angles operator /(Angles a, float b)
        {
            float num = 1f / b;
            return new Angles(a.roll * num, a.pitch * num, a.yaw * num);
        }

        public static Angles operator *(float a, Angles b)
        {
            float num = 1f / a;
            return new Angles(b.roll * num, b.pitch * num, b.yaw * num);
        }

        public void Clamp(Angles min, Angles max)
        {
            if (this.roll < min.roll)
            {
                this.roll = min.roll;
            }
            else if (this.roll > max.roll)
            {
                this.roll = max.roll;
            }
            if (this.pitch < min.pitch)
            {
                this.pitch = min.pitch;
            }
            else if (this.pitch > max.pitch)
            {
                this.pitch = max.pitch;
            }
            if (this.yaw < min.yaw)
            {
                this.yaw = min.yaw;
            }
            else if (this.yaw > max.yaw)
            {
                this.yaw = max.yaw;
            }
        }

        public Quat ToQuat()
        {
            float a = MathFunctions.DegToRad(this.yaw) * 0.5f;
            float num2 = MathFunctions.Sin(a);
            float num3 = MathFunctions.Cos(a);
            a = MathFunctions.DegToRad(this.pitch) * 0.5f;
            float num4 = MathFunctions.Sin(a);
            float num5 = MathFunctions.Cos(a);
            a = MathFunctions.DegToRad(this.roll) * 0.5f;
            float num6 = MathFunctions.Sin(a);
            float num7 = MathFunctions.Cos(a);
            float num8 = num6 * num5;
            float num9 = num7 * num5;
            float num10 = num6 * num4;
            float num11 = num7 * num4;
            return new Quat((num11 * num2) - (num8 * num3), (-num11 * num3) - (num8 * num2), (num10 * num3) - (num9 * num2), (num9 * num3) + (num10 * num2));
        }

        public unsafe void Normalize360()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((this[i] >= 360f) || (this[i] < 0f))
                {
                    this[i] = this[i] - (MathFunctions.Floor(this[i] / 360f) * 360f);
                    if (this[i] >= 360f)
                    {
                        this[i] = this[i] - 360f;
                    }
                    if (this[i] < 0f)
                    {
                        this[i] = this[i] + 360f;
                    }
                }
            }
        }

        public void Normalize180()
        {
            this.Normalize360();
            if (this.pitch > 180f)
            {
                this.pitch -= 360f;
            }
            if (this.yaw > 180f)
            {
                this.yaw -= 360f;
            }
            if (this.roll > 180f)
            {
                this.roll -= 360f;
            }
        }
    }
}
