using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class LowPassModule
    {
        public double Output { get; set; }
        public void Push(double NewValue, double Alpha)
        {
            Output = NewValue * Alpha + Output * (1 - Alpha);
        }
        public void Set(double d)
        {
            Output = d;     
        }
    }
}
