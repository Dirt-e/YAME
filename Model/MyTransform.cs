using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace YAME.Model
{
    public class MyTransform : MyObject
    {
        Transform3D _transform = Transform3D.Identity;
        public Transform3D Transform
        {
            get { return _transform; }
            set { _transform = value; OnPropertyChanged("Transform"); }
        }

        public MyTransform Parent = null;
        public List<MyTransform> Children = new List<MyTransform>();

        #region State
        Quaternion _quaternion = Quaternion.Identity;
        public Quaternion Quaternion
        {
            get { return _quaternion; }
            set
            {
                _quaternion = value;
                OnPropertyChanged("Quaternion");
            }
        }

        float _offset_x;
        public float Offset_X
        {
            get { return _offset_x; }
            set
            {
                _offset_x = value;
                OnPropertyChanged("Offset_X");
                ComposeTransform();
            }
        }
        float _offset_y;
        public float Offset_Y
        {
            get { return _offset_y; }
            set
            {
                _offset_y = value;
                OnPropertyChanged("Offset_Y");
                ComposeTransform();
            }
        }
        float _offset_z;
        public float Offset_Z
        {
            get { return _offset_z; }
            set
            {
                _offset_z = value;
                OnPropertyChanged("Offset_Z");
                ComposeTransform();
            }
        }

        //all angles in radians!!!
        float _yawangle;
        public float YawAngle
        {
            get { return _yawangle; }
            set
            {
                _yawangle = value;
                OnPropertyChanged("YawAngle");
                Quaternion = Utility.QuaternionFrom(YawAngle, PitchAngle, RollAngle);
                ComposeTransform();
            }
        }
        float _pitchangle;
        public float PitchAngle
        {
            get { return _pitchangle; }
            set
            {
                _pitchangle = value;
                OnPropertyChanged("PitchAngle");
                Quaternion = Utility.QuaternionFrom(YawAngle, PitchAngle, RollAngle);
                ComposeTransform();
            }
        }
        float _rollangle;
        public float RollAngle
        {
            get { return _rollangle; }
            set
            {
                _rollangle = value;
                OnPropertyChanged("RollAngle");
                Quaternion = Utility.QuaternionFrom(YawAngle, PitchAngle, RollAngle);
                ComposeTransform();
            }
        }
        #endregion
        
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

            Transform = Group as Transform3D;
        }

        public Transform3D GetWorldTransform()
        {
            if (Parent == null) { return Transform; }       //This IS world!
            else
            {
                Transform3DGroup Group = new Transform3DGroup();
                    Group.Children.Add(Transform);
                Group.Children.Add(Parent.GetWorldTransform());     //Recursion!

                return Group;
            }
        }


        public void IsParentOf(MyTransform MyTF)
        {   
            MyTF.Parent = this;
            Children.Add(MyTF);
        }
    }
}
