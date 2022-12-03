using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class FrameTimer
    {
        public float DeltaTime {get; set;}

        Stopwatch stopwatch = Stopwatch.StartNew();

        public void update()
        {
            DeltaTime = (float)stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Restart();
        }
    }
}
