using MOTUS.DataFomats;
using MOTUS.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static Utility;

namespace MOTUS.Model
{
    public class Integrator : MyObject
    {
        //The Integrator ONLY generates the different platforms (Airplane.STL) and displays them
        //via a Dispatcher.invoke() call. All inverse kinematics are done in the IK Module!!!

        public DOF_Data Input = new DOF_Data();
        Stopwatch invoke_timer = Stopwatch.StartNew();

        #region ViewModel
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
            set 
            { 
                _offset_park = value; 
                OnPropertyChanged("Offset_Park"); 
            }
        }

        float _offset_pause;
        public float Offset_Pause
        {
            get { return _offset_pause; }
            set { _offset_pause = value; OnPropertyChanged("Offset_Pause"); }
        }

        float _offset_cor;
        public float Offset_CoR
        {
            get { return _offset_cor; }
            set { _offset_cor = value; OnPropertyChanged("Offset_CoR"); }
        }
        #endregion

        public Lerp_3Way Lerp_3Way;
        float fade_duration_ParkToPause_seconds = 5;
        float fade_duration_PauseToMotion_seconds = 5;

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


        public Integrator(Engine e)
        {
            engine = e;         //To accesss other Objects

            World                   = new MyTransform();
            Plat_Fix_Base           = new MyTransform();
            Plat_Fix_Pause          = new MyTransform(); //(50% extension position)
            Plat_CoR                = new MyTransform();
            Plat_LFC                = new MyTransform();
            Plat_HFC                = new MyTransform();
            Plat_Motion             = new MyTransform();
            Plat_Float_Physical     = new MyTransform();
            Plat_Fix_Park           = new MyTransform();
            LowerPoints             = new ConnectingPoints();
            UpperPoints             = new ConnectingPoints();

            Lerp_3Way = new Lerp_3Way(  TimeSpan.FromSeconds(fade_duration_ParkToPause_seconds),
                                        TimeSpan.FromSeconds(fade_duration_PauseToMotion_seconds),
                                        engine);
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
            LerpPhysical_Between_ParkPauseMotion();
            UpdateUI_ViaDispatcherInvoke();
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
                                        Offset_Pause                    );

            Plat_CoR.SetTranslation(    0,
                                        0,
                                        Offset_CoR                      );

            Plat_LFC.SetOrientation(    0,      
                                        RAD_from_DEG(Input.LFC_Pitch), 
                                        RAD_from_DEG(Input.LFC_Roll)    );
            
            Plat_HFC.SetTranslation(    Input.HFC_Sway, 
                                        Input.HFC_Surge, 
                                        Input.HFC_Heave                 );
            Plat_HFC.SetOrientation(    RAD_from_DEG(Input.HFC_Yaw), 
                                        RAD_from_DEG(Input.HFC_Pitch), 
                                        RAD_from_DEG(Input.HFC_Roll)    );

            Plat_Motion.SetTranslation( 0,
                                        0,
                                        -Offset_CoR                      );
        }
        private void LerpPhysical_Between_ParkPauseMotion()
        {   
            //Plat_Float_Physical lives inside "World"! That's why we only use World coordinates
            //to determine its position.
            Lerp_3Way.LerpBetween(  Plat_Fix_Park.GetWorldTransform(),
                                    Plat_Fix_Pause.GetWorldTransform(),
                                    Plat_Motion.GetWorldTransform()         );

            Plat_Float_Physical.Transform = Lerp_3Way.Output;
        }

        #region Callback
        private void UpdateUI_ViaDispatcherInvoke()
        {
            if (invoke_timer.ElapsedMilliseconds > 33)      //Update the UI only at ~30fps
            {
                Platforms_Struct mx = new Platforms_Struct()
                {
                    Mx_Plat_Fix_Base = Plat_Fix_Base.GetWorldTransform().Value,
                    Mx_Plat_Fix_Pause = Plat_Fix_Pause.GetWorldTransform().Value,
                    Mx_Plat_CoR = Plat_CoR.GetWorldTransform().Value,
                    Mx_Plat_LFC = Plat_LFC.GetWorldTransform().Value,
                    Mx_Plat_HFC = Plat_HFC.GetWorldTransform().Value,
                    Mx_Plat_Motion = Plat_Motion.GetWorldTransform().Value,
                    Mx_Plat_Fix_Park = Plat_Fix_Park.GetWorldTransform().Value,
                    Mx_Plat_Float_Physical = Plat_Float_Physical.GetWorldTransform().Value,

                    Mx_UpperPoints_1 = UpperPoints.P1.GetWorldTransform().Value,
                    Mx_UpperPoints_2 = UpperPoints.P2.GetWorldTransform().Value,
                    Mx_UpperPoints_3 = UpperPoints.P3.GetWorldTransform().Value,
                    Mx_UpperPoints_4 = UpperPoints.P4.GetWorldTransform().Value,
                    Mx_UpperPoints_5 = UpperPoints.P5.GetWorldTransform().Value,
                    Mx_UpperPoints_6 = UpperPoints.P6.GetWorldTransform().Value,

                    Mx_LowerPoints_1 = LowerPoints.P1.GetWorldTransform().Value,
                    Mx_LowerPoints_2 = LowerPoints.P2.GetWorldTransform().Value,
                    Mx_LowerPoints_3 = LowerPoints.P3.GetWorldTransform().Value,
                    Mx_LowerPoints_4 = LowerPoints.P4.GetWorldTransform().Value,
                    Mx_LowerPoints_5 = LowerPoints.P5.GetWorldTransform().Value,
                    Mx_LowerPoints_6 = LowerPoints.P6.GetWorldTransform().Value,
                };

                Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback(UpdateViewModel), mx);

                invoke_timer.Restart();
            }

        }
        private delegate void UpdateViewModel_Callback(Platforms_Struct Mx);
        private void UpdateViewModel(Platforms_Struct Mx)
        {
            //This code runs on the Main thread!
            if (Application.Current.MainWindow is MainWindow mainwindow)
            {
                mainwindow.engine.VM_SceneView.PlatFixBase          = new MatrixTransform3D(Mx.Mx_Plat_Fix_Base);
                mainwindow.engine.VM_SceneView.PlatFixPause         = new MatrixTransform3D(Mx.Mx_Plat_Fix_Pause);
                mainwindow.engine.VM_SceneView.PlatFixPark          = new MatrixTransform3D(Mx.Mx_Plat_Fix_Park);
                mainwindow.engine.VM_SceneView.PlatCoR              = new MatrixTransform3D(Mx.Mx_Plat_CoR);
                mainwindow.engine.VM_SceneView.PlatLFC              = new MatrixTransform3D(Mx.Mx_Plat_LFC);
                mainwindow.engine.VM_SceneView.PlatHFC              = new MatrixTransform3D(Mx.Mx_Plat_HFC);
                mainwindow.engine.VM_SceneView.PlatMotion           = new MatrixTransform3D(Mx.Mx_Plat_Motion);
                mainwindow.engine.VM_SceneView.PlatFloatPhysical    = new MatrixTransform3D(Mx.Mx_Plat_Float_Physical);

                mainwindow.engine.VM_SceneView.Lower1 = new Point3D(Mx.Mx_LowerPoints_1.OffsetX,
                                                                        Mx.Mx_LowerPoints_1.OffsetY,
                                                                        Mx.Mx_LowerPoints_1.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower2 = new Point3D(Mx.Mx_LowerPoints_2.OffsetX,
                                                                        Mx.Mx_LowerPoints_2.OffsetY,
                                                                        Mx.Mx_LowerPoints_2.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower3 = new Point3D(Mx.Mx_LowerPoints_3.OffsetX,
                                                                        Mx.Mx_LowerPoints_3.OffsetY,
                                                                        Mx.Mx_LowerPoints_3.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower4 = new Point3D(Mx.Mx_LowerPoints_4.OffsetX,
                                                                        Mx.Mx_LowerPoints_4.OffsetY,
                                                                        Mx.Mx_LowerPoints_4.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower5 = new Point3D(Mx.Mx_LowerPoints_5.OffsetX,
                                                                        Mx.Mx_LowerPoints_5.OffsetY,
                                                                        Mx.Mx_LowerPoints_5.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower6 = new Point3D(Mx.Mx_LowerPoints_6.OffsetX,
                                                                        Mx.Mx_LowerPoints_6.OffsetY,
                                                                        Mx.Mx_LowerPoints_6.OffsetZ);

                mainwindow.engine.VM_SceneView.Upper1 = new Point3D(Mx.Mx_UpperPoints_1.OffsetX,
                                                                        Mx.Mx_UpperPoints_1.OffsetY,
                                                                        Mx.Mx_UpperPoints_1.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper2 = new Point3D(Mx.Mx_UpperPoints_2.OffsetX,
                                                                        Mx.Mx_UpperPoints_2.OffsetY,
                                                                        Mx.Mx_UpperPoints_2.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper3 = new Point3D(Mx.Mx_UpperPoints_3.OffsetX,
                                                                        Mx.Mx_UpperPoints_3.OffsetY,
                                                                        Mx.Mx_UpperPoints_3.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper4 = new Point3D(Mx.Mx_UpperPoints_4.OffsetX,
                                                                        Mx.Mx_UpperPoints_4.OffsetY,
                                                                        Mx.Mx_UpperPoints_4.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper5 = new Point3D(Mx.Mx_UpperPoints_5.OffsetX,
                                                                        Mx.Mx_UpperPoints_5.OffsetY,
                                                                        Mx.Mx_UpperPoints_5.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper6 = new Point3D(Mx.Mx_UpperPoints_6.OffsetX,
                                                                        Mx.Mx_UpperPoints_6.OffsetY,
                                                                        Mx.Mx_UpperPoints_6.OffsetZ);
            }
        } 
        #endregion
    }
}
