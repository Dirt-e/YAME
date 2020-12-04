using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public class ConnectingPoints : MyTransform
    {
        float _radius = 1000;
        public float Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                CalculateMajorMinorFromRadAlpha();
                Redraw();
                OnPropertyChanged("Radius");
            }
        }
        float _alpha = 45;
        public float Alpha
        {
            get { return _alpha; }
            set
            {
                _alpha = value;
                _beta = 120 - value;
                CalculateMajorMinorFromRadAlpha();
                Redraw();
                OnPropertyChanged("Alpha");
            }
        }
        float _beta = 75;
        public float Beta
        {
            get { return _beta; }
            set
            {
                _beta = value;
                _alpha = 120 - value;
                CalculateMajorMinorFromRadAlpha();
                Redraw();
                OnPropertyChanged("Beta");
            }
        }

        int _dist_a;
        public int Dist_A
        {
            get { return _dist_a; }
            set
            {
                _dist_a = value;
                CalculateRadAlphaFromDistDist();
                Redraw();
                OnPropertyChanged("Dist_A");
            }
        }
        int _dist_b;
        public int Dist_B
        {
            get { return _dist_b; }
            set
            {
                _dist_b = value;
                CalculateRadAlphaFromDistDist();
                Redraw();
                OnPropertyChanged("Dist_B");
            }
        }

        public MyTransform P1 { get; set; }
        public MyTransform P2 { get; set; }
        public MyTransform P3 { get; set; }
        public MyTransform P4 { get; set; }
        public MyTransform P5 { get; set; }
        public MyTransform P6 { get; set; }

        public ConnectingPoints()
        {
            P1 = new MyTransform();
            P2 = new MyTransform();
            P3 = new MyTransform();
            P4 = new MyTransform();
            P5 = new MyTransform();
            P6 = new MyTransform();

            IsParentOf(P1);
            IsParentOf(P2);
            IsParentOf(P3);
            IsParentOf(P4);
            IsParentOf(P5);
            IsParentOf(P6);

            Redraw();
        }

        //Angles in degrees
        private float A1, A2, A3, A4, A5, A6;

        void Redraw()
        {
            CalculateAllAzimuthAngles();
            CalculateAllConnectingPoints();
        }

        private void CalculateAllAzimuthAngles()
        {
            A1 = Alpha / 2;
            A2 = A1 + Beta;
            A3 = A2 + Alpha;
            A4 = A3 + Beta;
            A5 = A4 + Alpha;
            A6 = A5 + Beta;
        }
        private void CalculateAllConnectingPoints()
        {
            P1.SetTranslation(CoordinatesOfConnectorAtAngle(A1));
            P2.SetTranslation(CoordinatesOfConnectorAtAngle(A2));
            P3.SetTranslation(CoordinatesOfConnectorAtAngle(A3));
            P4.SetTranslation(CoordinatesOfConnectorAtAngle(A4));
            P5.SetTranslation(CoordinatesOfConnectorAtAngle(A5));
            P6.SetTranslation(CoordinatesOfConnectorAtAngle(A6));
        }

        //Helpers:
        Vector3D CoordinatesOfConnectorAtAngle(float angle_deg)
        {
            float angle_rad = Utility.RAD_from_DEG(angle_deg);

            float x = (float)Math.Sin(angle_rad) * Radius;
            float y = (float)Math.Cos(angle_rad) * Radius;
            float z = 0;

            return new Vector3D(x, y, z);
        }
        private void CalculateRadAlphaFromDistDist()
        {
            double square1 = Math.Pow(0 - (Dist_A + Math.Cos(Math.PI / 3) * Dist_B), 2);
            double square2 = Math.Pow(0 - (Math.Sin(Math.PI / 3) * Dist_B), 2);

            double rootterm = Math.Sqrt(square1 + square2);
            double divisor = 2 * Dist_A * (Math.Sin(Math.PI / 3) * Dist_B);

            Radius = (float)(Dist_A * Dist_B * rootterm / divisor);
            Alpha = (float)(2 * Math.Asin(0.5 * Dist_B / Radius) * 180 / Math.PI);
        }
        private void CalculateMajorMinorFromRadAlpha()
        {
            //throw new NotImplementedException();
        }
    }
}
