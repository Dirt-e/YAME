using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public class Fader : MyObject
    {
        //external:
        TimeSpan _duration;
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged("Duration");
            }
        }

        float _ratio_internal;  //always linear.

        float _ratio;
        public float Ratio
        {
            get
            {
                return _ratio;
            }
            private set
            {
                _ratio = value; OnPropertyChanged("Ratio");
            }
        }
        bool _isrunning;
        public bool IsRunning
        {
            get
            {
                return _isrunning;
            }
            private set
            {
                _isrunning = value; OnPropertyChanged("IsRunning");
            }
        }
        bool _direction = true;
        public bool Direction
        {
            get
            {
                return _direction;
            }
            private set
            {
                _direction = value; OnPropertyChanged("Direction");
            }
        }

        public Fader()
        {
            Duration = TimeSpan.FromSeconds(5);
        }
        public Fader(TimeSpan duration)
        {
            Duration = duration;
        }
        FadeOverMethod _method = FadeOverMethod.Cosine;
        public FadeOverMethod Method
        {
            get { return _method; }
            set
            {
                _method = value;
                OnPropertyChanged("Method");
            }
        }

        public virtual void Update()
        {
            //if (IsRunning)
            //{
            //    float percentageStepForThisFrame = engine.DeltaTime / (float)Duration.TotalMilliseconds;

            //    if (Direction) { _ratio_internal += percentageStepForThisFrame; }
            //    else { _ratio_internal -= percentageStepForThisFrame; }

            //    switch (Method)
            //    {
            //        case FadeOverMethod.linear:
            //            Ratio = Utility.Lerp(0, 1, _ratio_internal);
            //            break;
            //        case FadeOverMethod.Cosine:
            //            float CosineRatio = Utility.CosInterpolation(_ratio_internal);
            //            Ratio = Utility.Lerp(0, 1, CosineRatio);
            //            break;
            //        case FadeOverMethod.SmoothStep:
            //            float SmoothstepRatio = Utility.SmoothStep(_ratio_internal);
            //            Ratio = Utility.Lerp(0, 1, SmoothstepRatio);
            //            break;
            //        case FadeOverMethod.PerlinSmoothStep:
            //            float PerlinSmoothstepRatio = Utility.PerlinSmoothStep(_ratio_internal);
            //            Ratio = Utility.Lerp(0, 1, PerlinSmoothstepRatio);
            //            break;
            //        default:
            //            throw new Exception("Method " + Method + " not found.");
            //    }

            //    if (_ratio_internal > 1)                  //Endstop!
            //    {
            //        ResetTo_One();
            //        return;
            //    }
            //    if (_ratio_internal < 0)
            //    {
            //        ResetTo_Zero();
            //        return;
            //    }
            //}
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
            Ratio = 0;
            Direction = true;               //It can only go up from here
            IsRunning = false;
        }
        public void ResetTo_One()
        {
            _ratio_internal = 1;
            Ratio = 1;
            Direction = false;               //It can only go down from here
            IsRunning = false;
        }
    }
    public class Fader_floats : Fader
    {
        float _value1;
        public float Value1
        {
            get { return _value1; }
            set { _value1 = value; OnPropertyChanged("Value1"); }
        }
        float _value2;
        public float Value2
        {
            get { return _value2; }
            set { _value2 = value; OnPropertyChanged("Value2"); }
        }
        float _currentvalue;
        public float CurrentValue
        {
            get { return _currentvalue; }
            private set { _currentvalue = value; OnPropertyChanged("CurrentValue"); }
        }

        public Fader_floats(float val1, float val2, TimeSpan duration)
        {
            Value1 = val1;
            Value2 = val2;
            Duration = duration;
        }

        public override void Update()
        {
            if (IsRunning)
            {
                base.Update();
                CurrentValue = Value1 + (Value2 - Value1) * Ratio;
            }
        }
    }
    public class Fader_Vector3D : Fader
    {
        Vector3D _vector1;
        public Vector3D Vector1
        {
            get { return _vector1; }
            set { _vector1 = value; OnPropertyChanged("Vector1"); }
        }
        Vector3D _vector2;
        public Vector3D Vector2
        {
            get { return _vector2; }
            set { _vector2 = value; OnPropertyChanged("Vector2"); }
        }
        Vector3D _currentvector;
        public Vector3D CurrentVector
        {
            get { return _currentvector; }
            set { _currentvector = value; OnPropertyChanged("CurrentVector"); }
        }

        public Fader_Vector3D(Vector3D v1, Vector3D v2, TimeSpan duration)
        {
            Vector1 = v1;
            Vector2 = v2;
            Duration = duration;
        }

        public override void Update()
        {
            if (IsRunning)
            {
                base.Update();
                CurrentVector = Utility.Lerp(Vector1, Vector2, Ratio);
            }
        }
    }
    public class Fader_Point3D : Fader
    {
        Point3D _point1;
        public Point3D Point1
        {
            get { return _point1; }
            set { _point1 = value; OnPropertyChanged("Point1"); }
        }
        Point3D _Point2;
        public Point3D Point2
        {
            get { return _Point2; }
            set { _Point2 = value; OnPropertyChanged("Point2"); }
        }
        Point3D _currentpoint;
        public Point3D CurrentPoint
        {
            get { return _currentpoint; }
            set { _currentpoint = value; OnPropertyChanged("CurrentPoint"); }
        }

        public Fader_Point3D(Point3D p1, Point3D p2, TimeSpan duration)
        {
            Point1 = p1;
            Point2 = p2;
            Duration = duration;
        }

        public override void Update()
        {
            if (IsRunning)
            {
                base.Update();
                CurrentPoint = Utility.Lerp(Point1, Point2, Ratio);
            }
        }
    }
    public class Fader_Quaternion : Fader
    {
        Quaternion _quaternion1;
        public Quaternion Quaternion1
        {
            get { return _quaternion1; }
            set { _quaternion1 = value; OnPropertyChanged("Quaternion1"); }
        }
        Quaternion _Quaternion2;
        public Quaternion Quaternion2
        {
            get { return _Quaternion2; }
            set { _Quaternion2 = value; OnPropertyChanged("Quaternion2"); }
        }
        Quaternion _currentquaternion;
        public Quaternion CurrentQuaternion
        {
            get { return _currentquaternion; }
            set { _currentquaternion = value; OnPropertyChanged("CurrentQuaternion"); }
        }

        public Fader_Quaternion(Quaternion q1, Quaternion q2, TimeSpan duration)
        {
            Quaternion1 = q1;
            Quaternion2 = q2;
            Duration = duration;
        }

        public override void Update()
        {
            if (IsRunning)
            {
                base.Update();
                CurrentQuaternion = Utility.Slerp(Quaternion1, Quaternion2, Ratio);
            }
        }
    }
    public class Fader_Transform3D : Fader
    {
        Transform3D _tf1;
        public Transform3D TF1
        {
            get { return _tf1; }
            set { _tf1 = value; OnPropertyChanged("TF1"); }
        }
        Transform3D _tf2;
        public Transform3D TF2
        {
            get { return _tf2; }
            set { _tf2 = value; OnPropertyChanged("TF2"); }
        }
        Transform3D _currenttransform;
        public Transform3D CurrentTransform
        {
            get { return _currenttransform; }
            set { _currenttransform = value; OnPropertyChanged("CurrentTransform"); }
        }

        public Fader_Transform3D(Transform3D tf1, Transform3D tf2, TimeSpan duration)
        {
            TF1 = tf1;
            TF2 = tf2;
            Duration = duration;
        }

        public override void Update()
        {
            if (IsRunning)
            {
                base.Update();
                CurrentTransform = Utility.Lerp(TF1, TF2, Ratio);
            }
        }
    }


    public enum FadeOverMethod
    {
        linear,
        Cosine,
        SmoothStep,
        PerlinSmoothStep,
    }
}
