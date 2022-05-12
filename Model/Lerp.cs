using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace YAME.Model
{
    public class Lerp : MyObject
    {
        public TimeSpan Duration { get; set; }
        public LerpOverMethod Method { get; set; } = LerpOverMethod.PerlinSmoothStep;
        Stopwatch stopwatch = new Stopwatch();
        float Deltatime;

        float _ratio_linear     { get; set; }           //Always linear.

        float _ratio_external;                          //Depends on the FadeOver Method and is non linear!  
        public float Ratio_external     
        { 
            get { return _ratio_external; }
            set { _ratio_external = value; OnPropertyChanged(nameof(Ratio_external)); }
        }       
        public bool IsRunning           { get; set; }
        public bool Direction           { get; set; } = true;
        public bool IsMovingUpwards     { get { return (IsRunning && Direction); }  }
        public bool IsMovingDownwards   { get { return (IsRunning && !Direction); }  }
        public bool IsPausedUpwards     { get { return (!IsRunning && Direction) && IsInBetween; }  }
        public bool IsPausedDownwards   { get { return (!IsRunning && !Direction) && IsInBetween; }  }
        public bool IsFullUp            { get { return (Ratio_external == 1); }  }
        public bool IsFullDown          { get { return (Ratio_external == 0); }  }
        public bool IsInBetween         { get { return (!IsFullUp && !IsFullDown); }  }
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
                float percentageStepForThisFrame = Deltatime / (float)Duration.TotalMilliseconds;       //Always positive
                if (!Direction)
                {
                    percentageStepForThisFrame *= -1.0f;                                                //Are we going backwards? In this case we make a negative step
                }

                _ratio_linear += percentageStepForThisFrame;

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
                    default:
                        throw new Exception("Method " + Method + " not found.");
                }

                if (_ratio_linear >= 1)                  //Endstop to prevent slight overshoots!
                {
                    ResetTo_One();
                    return;
                }
                if (_ratio_linear <= 0)
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
        public void Stop()
        {
            IsRunning = false;
        }
        public void Pause_Toggle()
        {
            if (IsInBetween)
            {
                IsRunning = !IsRunning;
            }
        }
        public void Reverse()
        {
            if (0 < _ratio_linear && _ratio_linear < 1)
            {
                Direction = !Direction;
            }
        }
        public void ResetTo_Zero()
        {
            _ratio_linear = 0;
            Ratio_external = 0;
            Direction = true;               //It can only go up from here
            IsRunning = false;
        }
        public void ResetTo_One()
        {
            _ratio_linear = 1;
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
        PerlinSmoothStep
    }
}
