using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static Utility;

namespace YAME.Model
{
    public class Lerp : MyObject
    {
        public TimeSpan Duration { get; set; }
        public LerpOverMethod Method { get; set; } = LerpOverMethod.linear;
        Stopwatch stopwatch = new Stopwatch();
        float Deltatime;

        float _ratio_linear                 { get; set; } = 0;              //Always linear.
        float percentageStepForThisFrame    { get; set; } = 0;

        //for LerpOverMethod.LowPass3rdOrder
        const float alpha = 0.01f;
        float LP1 { get; set; }
        float LP2 { get; set; }
        float LP3 { get; set; }

        float _ratio_external;                          //Depends on the FadeOver Method and is non linear!  
        public float Ratio_external
        {
            get { return _ratio_external; }
            set { _ratio_external = value; OnPropertyChanged(nameof(Ratio_external)); }
        }
        public bool IsRunning           { get; set; } = false;
        public bool Direction           { get; set; } = true;
        public bool IsMovingUpwards     { get { return (IsRunning && Direction); }  }
        public bool IsMovingDownwards   { get { return (IsRunning && !Direction); }  }
        public bool IsFullUp            { get { return (_ratio_linear == 1); }  }
        public bool IsFullDown          { get { return (_ratio_linear == 0); }  }
        public bool IsInBetween         { get { return (!IsFullUp && !IsFullDown); }  }
        
        //Constructor:
        public Lerp()
        {
            Duration = TimeSpan.FromMilliseconds(5000);
            stopwatch.Start();
        }
        public Lerp(int ms, LerpOverMethod method = LerpOverMethod.linear)
        {
            Method = method;
            Duration = TimeSpan.FromMilliseconds(ms);
            stopwatch.Start();
        }

        public void Update()
        {   
            DetermineDeltaTime();

            if (IsRunning)
            {
                percentageStepForThisFrame = Deltatime / (float)Duration.TotalMilliseconds;       //Always positive
            }
            else
            {
                percentageStepForThisFrame = 0;
            }

            if (!Direction)
            {
                percentageStepForThisFrame *= -1.0f;                                                //Are we going backwards? In this case we make a negative step
            }

            _ratio_linear += percentageStepForThisFrame;
                
            Utility.Clamp(_ratio_linear, 0, 1);

            if (_ratio_linear >= 1)                  //Endstop to prevent slight overshoots!
            {
                ResetTo_One();
            }
            if (_ratio_linear <= 0)
            {
                ResetTo_Zero();
            }

            switch (Method)
            {
                case LerpOverMethod.linear:
                    Ratio_external = _ratio_linear;
                    break;
                case LerpOverMethod.Cosine:
                    Ratio_external = Utility.CosInterpolation(_ratio_linear);
                    break;
                case LerpOverMethod.SmoothStep:
                    Ratio_external = Utility.SmoothStep(_ratio_linear);
                    break;
                case LerpOverMethod.PerlinSmoothStep:
                    Ratio_external = Utility.PerlinSmoothStep(_ratio_linear);
                    break;
                case LerpOverMethod.LowPass3rdOrder:
                    LP1 += (_ratio_linear - LP1) * alpha;
                    LP2 += (LP1 - LP2) * alpha;
                    LP3 += (LP2 - LP3) * alpha;
                    Ratio_external = LP3;
                    break;
                default:
                    throw new Exception("Method " + Method + " not found.");
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
        public void Stop()
        {
            IsRunning = false;
        }
        public void ResetTo_Zero()
        {
            _ratio_linear = 0;
            //Ratio_external = 0;
            Direction = true;               //It can only go up from here
            IsRunning = false;
        }
        public void ResetTo_One()
        {
            _ratio_linear = 1;
            //Ratio_external = 1;
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
        LowPass3rdOrder
    }
}
