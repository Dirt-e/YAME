using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public class MyTransform : INotifyPropertyChanged
    {
        Transform3D _transform = Transform3D.Identity;
        public Transform3D Transform
        {
            get { return _transform; }
            private set { _transform = value; OnPropertyChanged("Transform"); }
        }

        Quaternion _quaternion = Quaternion.Identity;
        public Quaternion Quaternion
        {
            get { return _quaternion; }
            private set
            {
                _quaternion = value;
                OnPropertyChanged("Quaternion");
            }
        }

        float _offset_x;
        public float Offset_X
        {
            get { return _offset_x; }
            private set
            {
                _offset_x = value;
                OnPropertyChanged("Offset_X");
            }
        }
        float _offset_y;
        public float Offset_Y
        {
            get { return _offset_y; }
            private set
            {
                _offset_y = value;
                OnPropertyChanged("Offset_Y");
            }
        }
        float _offset_z;
        public float Offset_Z
        {
            get { return _offset_z; }
            private set
            {
                _offset_z = value;
                OnPropertyChanged("Offset_Z");
            }
        }

        //all angles in radians!!!
        float _yawangle;
        public float YawAngle
        {
            get { return _yawangle; }
            private set
            {
                _yawangle = value;
                OnPropertyChanged("YawAngle");
            }
        }
        float _pitchangle;
        public float PitchAngle
        {
            get { return _pitchangle; }
            private set
            {
                _pitchangle = value;
                OnPropertyChanged("PitchAngle");
            }
        }
        float _rollangle;
        public float RollAngle
        {
            get { return _rollangle; }
            private set
            {
                _rollangle = value;
                OnPropertyChanged("RollAngle");
            }
        }

        public MyTransform(MyTransform MyTF)
        {
            Offset_X = MyTF.Offset_X;
            Offset_Y = MyTF.Offset_Y;
            Offset_Z = MyTF.Offset_Z;

            YawAngle = MyTF.YawAngle;
            PitchAngle = MyTF.PitchAngle;
            RollAngle = MyTF.RollAngle;

            Quaternion = MyTF.Quaternion;

            Transform = MyTF.Transform;
        }
        public MyTransform() { }

        //Helpers:
        public void SetTranslation(Vector3D v)
        {
            SetTranslation((float)v.X, (float)v.Y, (float)v.Z);
        }
        public void SetTranslation(float x, float y, float z)
        {
            Offset_X = x;
            Offset_Y = y;
            Offset_Z = z;

            ComposeTransform();
        }

        public void SetOrientation(Vector3D eulervector)
        {
            SetOrientation((float)eulervector.X, (float)eulervector.Y, (float)eulervector.Z);
        }
        public void SetOrientation(float yaw, float pitch, float roll)
        {
            YawAngle = yaw;
            PitchAngle = pitch;
            RollAngle = roll;

            Quaternion = Utility.QuaternionFrom(YawAngle, PitchAngle, RollAngle);

            ComposeTransform();
        }
        public void SetOrientation(Quaternion q)
        {
            Quaternion = q;
            Vector3D eulervector = Utility.EulerFrom(q);
            YawAngle = (float)eulervector.X;
            PitchAngle = (float)eulervector.Y;
            RollAngle = (float)eulervector.Z;

            ComposeTransform();
        }

        private void ComposeTransform()
        {
            TranslateTransform3D Translation = new TranslateTransform3D(Offset_X, Offset_Y, Offset_Z);
            RotateTransform3D Rotation = new RotateTransform3D(new QuaternionRotation3D(Quaternion));

            Transform3DGroup Group = new Transform3DGroup();
            Group.Children.Add(Rotation);
            Group.Children.Add(Translation);

            Transform = Group;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        private protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
