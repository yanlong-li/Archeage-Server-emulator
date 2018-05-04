using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core.Math
{
    public class BezierCurve : Curve
    {
        private unsafe void a(int A, float a, float* B, int b)
        {
            this.A(A - 1, a, B, 1 + b);
            B[b * 4] = 0f;
            for (int i = 0; i < (A - 1); i++)
            {
                float* singlePtr1 = B + ((i + b) * 4);
                singlePtr1[0] -= B[((i + 1) + b) * 4];
            }
        }

        private unsafe void A(int A, float a, float* B, int b)
        {
            B[b * 4] = 1f;
            int num3 = A - 1;
            if (num3 > 0)
            {
                byte* d = stackalloc byte[(4 * (num3 + 1))];
                float* numPtr = (float*)d;
                float num6 = (a - base.Times[0]) / (base.Times[base.Times.Count - 1] - base.Times[0]);
                float num7 = 1f - num6;
                float num8 = num6;
                float num9 = num7;
                int num = 1;
                while (num < num3)
                {
                    numPtr[num * 4] = 1f;
                    num++;
                }
                num = 1;
                while (num < num3)
                {
                    numPtr[(num - 1) * 4] = 0f;
                    float num4 = numPtr[num * 4];
                    numPtr[num * 4] = 1f;
                    for (int i = num + 1; i <= num3; i++)
                    {
                        float num5 = numPtr[i * 4];
                        numPtr[i * 4] = num4 + numPtr[(i - 1) * 4];
                        num4 = num5;
                    }
                    B[(num + b) * 4] = numPtr[num3 * 4] * num8;
                    num8 *= num6;
                    num++;
                }
                for (num = num3 - 1; num >= 0; num--)
                {
                    float* singlePtr1 = B + ((num + b) * 4);
                    singlePtr1[0] *= num9;
                    num9 *= num7;
                }
                B[(num3 + b) * 4] = num8;
            }
        }

        public override unsafe Vec3 CalculateValueByTime(float time)
        {
            byte* d = stackalloc byte[(4 * base.Values.Count)];
            float* b = (float*)d;
            this.A(base.Values.Count, time, b, 0);
            Vec3 vec = (Vec3)(b[0] * base.Values[0]);
            for (int i = 1; i < base.Values.Count; i++)
            {
                vec += (Vec3)(b[i * 4] * base.Values[i]);
            }
            return vec;
        }

        public override unsafe Vec3 GetCurrentFirstDerivative(float time)
        {
            byte* d = stackalloc byte[(4 * base.Values.Count)];
            float* b = (float*)d;
            this.a(base.Values.Count, time, b, 0);
            Vec3 vec = (Vec3)(b[0] * base.Values[0]);
            for (int i = 1; i < base.Values.Count; i++)
            {
                vec += (Vec3)(b[i * 4] * base.Values[i]);
            }
            float num2 = base.Times[base.Times.Count - 1] - base.Times[0];
            return (Vec3)((((float)(base.Values.Count - 1)) / num2) * vec);
        }
    }
}
