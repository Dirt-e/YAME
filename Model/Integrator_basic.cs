using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using YAME.DataFomats;
using static Utility;

namespace YAME.Model
{
    public class Integrator_basic : MyObject
    {
        public DOF_Data Input = new DOF_Data();

        #region Geometry
        float _dist_a_upper;
        public float Dist_A_Upper
        {
            get { return _dist_a_upper; }
            set { _dist_a_upper = value; OnPropertyChanged("Dist_A_Upper"); }
        }

        float _dist_b_upper;
        public float Dist_B_Upper
        {
            get { return _dist_b_upper; }
            set { _dist_b_upper = value; OnPropertyChanged("Dist_B_Upper"); }
        }

        float _dist_a_lower;
        public float Dist_A_Lower
        {
            get { return _dist_a_lower; }
            set { _dist_a_lower = value; OnPropertyChanged("Dist_A_Lower"); }
        }

        float _dist_b_lower;
        public float Dist_B_Lower
        {
            get { return _dist_b_lower; }
            set { _dist_b_lower = value; OnPropertyChanged("Dist_B_Lower"); }
        }

        float _offset_park;
        public float Offset_Park
        {
            get { return _offset_park; }
            set { _offset_park = value; OnPropertyChanged(nameof(Offset_Park)); }
        }

        float _offset_pause;
        public float Offset_Pause
        {
            get { return _offset_pause; }
            set { _offset_pause = value; OnPropertyChanged(nameof(Offset_Pause)); }
        }

        float _offset_cor;
        public float Offset_CoR
        {
            get { return _offset_cor; }
            set { _offset_cor = value; OnPropertyChanged(nameof(Offset_CoR)); }
        }
        #endregion

        #region MyTransforms
        public MyTransform World;
        public MyTransform Plat_Fix_Base;
        public MyTransform Plat_Fix_Pause;
        public MyTransform Plat_CoR;
        public MyTransform Plat_LFC;
        public MyTransform Plat_HFC;
        public MyTransform Plat_Motion;
        public ConnectingPoints UpperPoints;
        public ConnectingPoints LowerPoints;

        public MyTransform Plat_Fix_Park;
        public MyTransform Plat_Float_Physical;

        #endregion

        //Constructor
        public Integrator_basic()
        {
            World = new MyTransform();
            Plat_Fix_Base = new MyTransform();
            Plat_Fix_Pause = new MyTransform(); //(50% extension position)
            Plat_CoR = new MyTransform();
            Plat_LFC = new MyTransform();
            Plat_HFC = new MyTransform();
            Plat_Motion = new MyTransform();
            Plat_Float_Physical = new MyTransform();
            Plat_Fix_Park = new MyTransform();
            LowerPoints = new ConnectingPoints();
            UpperPoints = new ConnectingPoints();

            EstablishHierarchy();

        }
        //Copy Constructor
        public Integrator_basic(Integrator_basic int_bas)
        {
            Dist_A_Upper    = int_bas.Dist_A_Upper;
            Dist_B_Upper    = int_bas.Dist_B_Upper;
            Dist_A_Lower    = int_bas.Dist_A_Lower;
            Dist_B_Lower    = int_bas.Dist_B_Lower;
            Offset_Park     = int_bas.Offset_Park;
            Offset_Pause    = int_bas.Offset_Pause;
            Offset_CoR      = int_bas.Offset_CoR;

            World = new MyTransform();
            Plat_Fix_Base = new MyTransform();  //(0% extension position)
            Plat_Fix_Pause = new MyTransform(); //(50% extension position)
            Plat_CoR = new MyTransform();
            Plat_LFC = new MyTransform();
            Plat_HFC = new MyTransform();
            Plat_Motion = new MyTransform();
            Plat_Float_Physical = new MyTransform();
            Plat_Fix_Park = new MyTransform();
            LowerPoints = new ConnectingPoints();
            UpperPoints = new ConnectingPoints();

            EstablishHierarchy();
        }
        
        private void EstablishHierarchy()
        {
            World.IsParentOf(Plat_Fix_Base);
                Plat_Fix_Base.IsParentOf(Plat_Fix_Park);
                Plat_Fix_Base.IsParentOf(Plat_Fix_Pause);
                    Plat_Fix_Pause.IsParentOf(Plat_CoR);
                        Plat_CoR.IsParentOf(Plat_LFC);
                            Plat_LFC.IsParentOf(Plat_HFC);
                                Plat_HFC.IsParentOf(Plat_Motion);
                Plat_Fix_Base.IsParentOf(LowerPoints);
            World.IsParentOf(Plat_Float_Physical);
                Plat_Float_Physical.IsParentOf(UpperPoints);
        }

        public void Update(DOF_Data data)
        {
            Input = new DOF_Data(data);
            Integrate_Platforms();
        }

        private void Integrate_Platforms()
        {
            UpperPoints.Dist_A = Dist_A_Upper;
            UpperPoints.Dist_B = Dist_B_Upper;

            LowerPoints.Dist_A = Dist_A_Lower;
            LowerPoints.Dist_B = Dist_B_Lower;

            Plat_Fix_Park.SetTranslation(   0,
                                            0,
                                            Offset_Park);

            Plat_Fix_Pause.SetTranslation(  0,
                                            0,
                                            Offset_Pause);

            Plat_CoR.SetTranslation(        0,
                                            0,
                                            Offset_CoR);

            Plat_LFC.SetOrientation(        0,
                                            RAD_from_DEG(Input.LFC_Pitch),
                                            -RAD_from_DEG(Input.LFC_Roll));     //Negative sign, because a positive accel (right) shall tilt the platform towards negative roll (left)

            Plat_HFC.SetTranslation(        Input.HFC_Sway,
                                            Input.HFC_Surge,
                                            Input.HFC_Heave);
            Plat_HFC.SetOrientation(        RAD_from_DEG(Input.HFC_Yaw),
                                            RAD_from_DEG(Input.HFC_Pitch),
                                            RAD_from_DEG(Input.HFC_Roll));

            Plat_Motion.SetTranslation(     0,
                                            0,
                                            -Offset_CoR);
        }
    }
}
