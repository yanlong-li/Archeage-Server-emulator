using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core.Math
{
    public class Curve
    {
        private List<float> Rc = new List<float>();
        private int Rd = -1;
        private List<Vec3> RD = new List<Vec3>();

        public int AddValue(float time, Vec3 value)
        {
            int indexForTime = this.GetIndexForTime(time);
            this.Rc.Insert(indexForTime, time);
            this.RD.Insert(indexForTime, value);
            return indexForTime;
        }

        public virtual Vec3 CalculateValueByTime(float time)
        {
            int indexForTime = this.GetIndexForTime(time);
            if (indexForTime >= this.RD.Count)
            {
                return this.RD[this.RD.Count - 1];
            }
            return this.RD[indexForTime];
        }

        public void Clear()
        {
            this.RD.Clear();
            this.Rc.Clear();
            this.Rd = -1;
        }

        public virtual Vec3 GetCurrentFirstDerivative(float time)
        {
            return Vec3.Zero;
        }

        protected int GetIndexForTime(float time)
        {
            if ((this.Rd >= 0) && (this.Rd <= this.Rc.Count))
            {
                if (this.Rd == 0)
                {
                    if (time <= this.Rc[this.Rd])
                    {
                        return this.Rd;
                    }
                }
                else if (this.Rd == this.Rc.Count)
                {
                    if (time > this.Rc[this.Rd - 1])
                    {
                        return this.Rd;
                    }
                }
                else
                {
                    if ((time > this.Rc[this.Rd - 1]) && (time <= this.Rc[this.Rd]))
                    {
                        return this.Rd;
                    }
                    if ((time > this.Rc[this.Rd]) && (((this.Rd + 1) == this.Rc.Count) || (time <= this.Rc[this.Rd + 1])))
                    {
                        this.Rd++;
                        return this.Rd;
                    }
                }
            }
            int count = this.Rc.Count;
            int num2 = count;
            int num3 = 0;
            int num4 = 0;
            while (num2 > 0)
            {
                num2 = count >> 1;
                if (time == this.Rc[num3 + num2])
                {
                    return (num3 + num2);
                }
                if (time > this.Rc[num3 + num2])
                {
                    num3 += num2;
                    count -= num2;
                    num4 = 1;
                }
                else
                {
                    count -= num2;
                    num4 = 0;
                }
            }
            this.Rd = num3 + num4;
            return this.Rd;
        }

        protected float GetSpeed(float time)
        {
            Vec3 currentFirstDerivative = this.GetCurrentFirstDerivative(time);
            float x = 0f;
            for (int i = 0; i < 3; i++)
            {
                x += currentFirstDerivative[i] * currentFirstDerivative[i];
            }
            return MathFunctions.Sqrt(x);
        }

        public void RemoveValue(int index)
        {
            this.RD.RemoveAt(index);
            this.Rc.RemoveAt(index);
        }

        /// <summary>
        /// <b>Don't modify</b>.
        /// </summary>
        public List<float> Times
        {
            get
            {
                return this.Rc;
            }
        }

        /// <summary>
        /// <b>Don't modify</b>.
        /// </summary>
        public List<Vec3> Values
        {
            get
            {
                return this.RD;
            }
        }
    }
}
