using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.DataFomats
{
    public class SixSisters
    {
        public float[] values = new float[6];

        //Operators
        public static SixSisters operator +(SixSisters s1, SixSisters s2)
        {
            SixSisters result = new SixSisters();

            for (int i = 0; i < result.values.Length; i++)
            {
                result.values[i] = s1.values[i] + s2.values[i];
            }

            return result;
        }
        public static SixSisters operator -(SixSisters s1, SixSisters s2)
        {
            return s1 + (-s2);
        }
        public static SixSisters operator -(SixSisters s1)
        {
            SixSisters result = new SixSisters();

            for (int i = 0; i < result.values.Length; i++)
            {
                result.values[i] = s1.values[i] * -1.0f;
            }

            return result;
        }
        public static SixSisters operator *(SixSisters s1, float f)
        {
            SixSisters result = new SixSisters();

            for (int i = 0; i < result.values.Length; i++)
            {
                result.values[i] = s1.values[i] * f;
            }

            return result;
        }
        public static SixSisters operator *(float f, SixSisters s1)
        {
            return s1 * f;
        }
        public static SixSisters operator /(SixSisters s1, float f)
        {
            if (f == 0)
            {
                SixSisters result = new SixSisters();
                for (int i = 0; i < result.values.Length; i++)
                {
                    result.values[i] = float.NaN;
                }
                return result;
            }
            else
            {
                return s1 * (1.0f / f);
            }
            
            
        }
    }
}
