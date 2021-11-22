using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public class Lerp
    {
        public TimeSpan Duration { get; set; }
        public LerpOverMethod Method { get; set; } = LerpOverMethod.Cosine;
        Stopwatch stopwatch = new Stopwatch();
        float Deltatime;

        float _ratio_internal { get; set; }             //always linear.
        public float Ratio_external { get; set; }       //Depends on the FadeOver Method. Can be non linear!  

        public bool IsRunning { get; set; }
        public bool Direction { get; set; } = true;

        //Constructor:
        public Lerp()
        {
            Duration = TimeSpan.FromMilliseconds(5000);
            stopwatch.Start();
        }
        public Lerp(int ms)
        {
            Duration = TimeSpan.FromMilliseconds(ms);
            stopwatch.Start();
        }

        public void Update()
        {
            DetermineDeltaTime();

            if (IsRunning)
            {
                float percentageStepForThisFrame = Deltatime / (float)Duration.TotalMilliseconds;

                if (Direction) { _ratio_internal += percentageStepForThisFrame; }
                else { _ratio_internal -= percentageStepForThisFrame; }

                switch (Method)
                {
                    case LerpOverMethod.linear:
                        Ratio_external = _ratio_internal;
                        break;
                    case LerpOverMethod.Cosine:
                        Ratio_external = Utility.CosInterpolation(_ratio_internal);
                        break;
                    case LerpOverMethod.SmoothStep:
                        Ratio_external = Utility.SmoothStep(_ratio_internal);
                        break;
                    case LerpOverMethod.PerlinSmoothStep:
                        Ratio_external = Utility.PerlinSmoothStep(_ratio_internal);
                        break;
                    default:
                        throw new Exception("Method " + Method + " not found.");
                }

                if (_ratio_internal > 1)                  //Endstop to prevent slight overshoots!
                {
                    ResetTo_One();
                    return;
                }
                if (_ratio_internal < 0)
                {
                    ResetTo_Zero();
                    return;
                }
            }
        }

        private void DetermineDeltaTime()
        {
            Deltatime = (float)stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Restart();
        }

        //Helpers:
        public void Run()
        {
            IsRunning = true;
        }
        public void Run(bool direction)
        {
            Direction = direction;
            IsRunning = true;
        }
        public void Pause()
        {
            IsRunning = false;
        }
        public void Reverse()
        {
            Direction = !Direction;
        }
        public void ResetTo_Zero()
        {
            _ratio_internal = 0;
            Ratio_external = 0;
            Direction = true;               //It can only go up from here
            IsRunning = false;
        }
        public void ResetTo_One()
        {
            _ratio_internal = 1;
            Ratio_external = 1;
            Direction = false;               //It can only go down from here
            IsRunning = false;
        }
    }


    public enum LerpOverMethod
    {
        linear,
        Cosine,
        SmoothStep,
        PerlinSmoothStep,
    }
}
