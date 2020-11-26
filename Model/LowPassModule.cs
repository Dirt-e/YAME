using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class LowPassModule
    {
        public float Output { get; set; }
        public void Push(float NewValue, float Alpha)
        {
            Output = NewValue * Alpha + Output * (1 - Alpha);
        }
        public void Set(float f)
        {
            Output = f;
        }
    }
}
