using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.DataFomats
{
    public class SixSisters
    {
        public float[] Values = new float[6];

        public SixSisters()
        {

        }
        public SixSisters(SixSisters ss)
        {
            for (int i = 0; i < ss.Values.Length; i++)
            {
                Values[i] = ss.Values[i];
            }
        }

        //Operators
        public static SixSisters operator +(SixSisters s1, SixSisters s2)
        {
            SixSisters result = new SixSisters();

            for (int i = 0; i < result.Values.Length; i++)
            {
                result.Values[i] = s1.Values[i] + s2.Values[i];
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

            for (int i = 0; i < result.Values.Length; i++)
            {
                result.Values[i] = s1.Values[i] * -1.0f;
            }

            return result;
        }
        public static SixSisters operator *(SixSisters s1, float f)
        {
            SixSisters result = new SixSisters();

            for (int i = 0; i < result.Values.Length; i++)
            {
                result.Values[i] = s1.Values[i] * f;
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
                for (int i = 0; i < result.Values.Length; i++)
                {
                    result.Values[i] = float.NaN;
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
