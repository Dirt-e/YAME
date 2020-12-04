using MOTUS.DataFomats;
using MOTUS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static Utility;

namespace MOTUS.Model
{
    public class Integrator
    {
        public DOF_Data Input = new DOF_Data();

        //Fader:
        public Fader_Threeway Fader_3Way;

        float fade_duration_ParkToPause_seconds = 5;
        float fade_duration_PauseToMotion_seconds = 5;

        #region MyTransforms
        public MyTransform World;
            public MyTransform Plat_Fix_Base;
                public MyTransform Plat_Pause;
                    public MyTransform Plat_CoR;
                        public MyTransform Plat_LFC;
                            public MyTransform Plat_HFC;
                                public MyTransform Plat_Motion;
                                    public ConnectingPoints UpperPoints;
                public ConnectingPoints LowerPoints;

            public MyTransform Plat_Park;
            public MyTransform Plat_Float_Physical;
        
        #endregion

        public Integrator()
        {
            World = new MyTransform();
            Plat_Fix_Base = new MyTransform();
            Plat_Pause = new MyTransform(); //(50% extension position)
            Plat_CoR = new MyTransform();
            Plat_LFC = new MyTransform();
            Plat_HFC = new MyTransform();
            Plat_Motion = new MyTransform();
            LowerPoints = new ConnectingPoints();
            Plat_Park = new MyTransform();
            Plat_Float_Physical = new MyTransform();
            UpperPoints = new ConnectingPoints();

            Fader_3Way = new Fader_Threeway(    TimeSpan.FromSeconds(fade_duration_ParkToPause_seconds),
                                                TimeSpan.FromSeconds(fade_duration_PauseToMotion_seconds));

            EstablishHierarchy();
        }

        private void EstablishHierarchy()
        {
            World.IsParentOf(Plat_Fix_Base);
                Plat_Fix_Base.IsParentOf(Plat_Pause);
                    Plat_Pause.IsParentOf(Plat_CoR);
                        Plat_CoR.IsParentOf(Plat_LFC);
                            Plat_LFC.IsParentOf(Plat_HFC);
                                Plat_HFC.IsParentOf(Plat_Motion);
                                    Plat_Motion.IsParentOf(UpperPoints);
                Plat_Fix_Base.IsParentOf(LowerPoints);
                Plat_Fix_Base.IsParentOf(Plat_Park);
                Plat_Fix_Base.IsParentOf(Plat_Float_Physical);
        }

        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);
            DriveRigGeometry();
        }
        private void DriveRigGeometry()
        {
            //Dynamic offsets here only!
            Plat_Pause.SetTranslation(0,0,850);         //To be changed to bindings

            Plat_CoR.SetTranslation(0,0,300);           //To be changed to bindings

            Plat_LFC.SetOrientation(    0,      
                                        RAD_from_DEG(Input.LFC_Pitch), 
                                        RAD_from_DEG(Input.LFC_Roll)    );
            
            Plat_HFC.SetTranslation(    Input.HFC_Sway, 
                                        Input.HFC_Surge, 
                                        Input.HFC_Heave     );
            Plat_HFC.SetOrientation(    RAD_from_DEG(Input.HFC_Yaw), 
                                        RAD_from_DEG(Input.HFC_Pitch), 
                                        RAD_from_DEG(Input.HFC_Roll)    );

            Plat_Motion.SetTranslation( 0, 0, -300);     //To be changed to bindings

            UpdateUI_ViaDispatcherInvoke();
        }

        private void UpdateUI_ViaDispatcherInvoke()
        {
            Matrix_Struct mx = new Matrix_Struct()
            {
                Mx_Plat_Fix_Base        = Plat_Fix_Base.GetWorldTransform().Value,
                Mx_Plat_Park            = Plat_Park.GetWorldTransform().Value,
                Mx_Plat_Pause           = Plat_Pause.GetWorldTransform().Value,
                Mx_Plat_CoR             = Plat_CoR.GetWorldTransform().Value,
                Mx_Plat_LFC             = Plat_LFC.GetWorldTransform().Value,
                Mx_Plat_HFC             = Plat_HFC.GetWorldTransform().Value,
                Mx_Plat_Motion          = Plat_Motion.GetWorldTransform().Value,
                Mx_Plat_Float_Physical  = Plat_Float_Physical.GetWorldTransform().Value,

                Mx_UpperPoints_1    = UpperPoints.P1.GetWorldTransform().Value,
                Mx_UpperPoints_2    = UpperPoints.P2.GetWorldTransform().Value,
                Mx_UpperPoints_3    = UpperPoints.P3.GetWorldTransform().Value,
                Mx_UpperPoints_4    = UpperPoints.P4.GetWorldTransform().Value,
                Mx_UpperPoints_5    = UpperPoints.P5.GetWorldTransform().Value,
                Mx_UpperPoints_6    = UpperPoints.P6.GetWorldTransform().Value,

                Mx_LowerPoints_1 = LowerPoints.P1.GetWorldTransform().Value,
                Mx_LowerPoints_2 = LowerPoints.P2.GetWorldTransform().Value,
                Mx_LowerPoints_3 = LowerPoints.P3.GetWorldTransform().Value,
                Mx_LowerPoints_4 = LowerPoints.P4.GetWorldTransform().Value,
                Mx_LowerPoints_5 = LowerPoints.P5.GetWorldTransform().Value,
                Mx_LowerPoints_6 = LowerPoints.P6.GetWorldTransform().Value,
            };

            Application.Current.Dispatcher.BeginInvoke(new UpdateViewModel_Callback(UpdateViewModel), mx);
        }
        private delegate void UpdateViewModel_Callback(Matrix_Struct Mx);
        private void UpdateViewModel(Matrix_Struct Mx)
        {
            var mainwindow = Application.Current.MainWindow as MainWindow;
            if (mainwindow != null)
            {
                mainwindow.btn_Test.Content = "Marker";

                mainwindow.engine.VM_SceneView.PlatFixBase  = new MatrixTransform3D(Mx.Mx_Plat_Fix_Base);
                mainwindow.engine.VM_SceneView.PlatFixPause = new MatrixTransform3D(Mx.Mx_Plat_Pause);
                mainwindow.engine.VM_SceneView.PlatFixPark  = new MatrixTransform3D(Mx.Mx_Plat_Park);
                mainwindow.engine.VM_SceneView.PlatLFC      = new MatrixTransform3D(Mx.Mx_Plat_LFC);
                mainwindow.engine.VM_SceneView.PlatHFC      = new MatrixTransform3D(Mx.Mx_Plat_HFC);
                mainwindow.engine.VM_SceneView.PlatMotion   = new MatrixTransform3D(Mx.Mx_Plat_Motion);

                mainwindow.engine.VM_SceneView.Lower1 = new Point3D(    Mx.Mx_LowerPoints_1.OffsetX,   
                                                                        Mx.Mx_LowerPoints_1.OffsetY,
                                                                        Mx.Mx_LowerPoints_1.OffsetZ     );
                mainwindow.engine.VM_SceneView.Lower2 = new Point3D(    Mx.Mx_LowerPoints_2.OffsetX,
                                                                        Mx.Mx_LowerPoints_2.OffsetY,
                                                                        Mx.Mx_LowerPoints_2.OffsetZ     );
                mainwindow.engine.VM_SceneView.Lower3 = new Point3D(    Mx.Mx_LowerPoints_3.OffsetX,
                                                                        Mx.Mx_LowerPoints_3.OffsetY,
                                                                        Mx.Mx_LowerPoints_3.OffsetZ     );
                mainwindow.engine.VM_SceneView.Lower4 = new Point3D(    Mx.Mx_LowerPoints_4.OffsetX,
                                                                        Mx.Mx_LowerPoints_4.OffsetY,
                                                                        Mx.Mx_LowerPoints_4.OffsetZ     );
                mainwindow.engine.VM_SceneView.Lower5 = new Point3D (   Mx.Mx_LowerPoints_5.OffsetX,
                                                                        Mx.Mx_LowerPoints_5.OffsetY,
                                                                        Mx.Mx_LowerPoints_5.OffsetZ     );
                mainwindow.engine.VM_SceneView.Lower6 = new Point3D(    Mx.Mx_LowerPoints_6.OffsetX,
                                                                        Mx.Mx_LowerPoints_6.OffsetY,
                                                                        Mx.Mx_LowerPoints_6.OffsetZ     );

                mainwindow.engine.VM_SceneView.Upper1 = new Point3D(    Mx.Mx_UpperPoints_1.OffsetX,
                                                                        Mx.Mx_UpperPoints_1.OffsetY,
                                                                        Mx.Mx_UpperPoints_1.OffsetZ     );
                mainwindow.engine.VM_SceneView.Upper2 = new Point3D(    Mx.Mx_UpperPoints_2.OffsetX,
                                                                        Mx.Mx_UpperPoints_2.OffsetY,
                                                                        Mx.Mx_UpperPoints_2.OffsetZ     );
                mainwindow.engine.VM_SceneView.Upper3 = new Point3D(    Mx.Mx_UpperPoints_3.OffsetX,
                                                                        Mx.Mx_UpperPoints_3.OffsetY,
                                                                        Mx.Mx_UpperPoints_3.OffsetZ     );
                mainwindow.engine.VM_SceneView.Upper4 = new Point3D(    Mx.Mx_UpperPoints_4.OffsetX,
                                                                        Mx.Mx_UpperPoints_4.OffsetY,
                                                                        Mx.Mx_UpperPoints_4.OffsetZ     );
                mainwindow.engine.VM_SceneView.Upper5 = new Point3D(    Mx.Mx_UpperPoints_5.OffsetX,
                                                                        Mx.Mx_UpperPoints_5.OffsetY,
                                                                        Mx.Mx_UpperPoints_5.OffsetZ     );
                mainwindow.engine.VM_SceneView.Upper6 = new Point3D(    Mx.Mx_UpperPoints_6.OffsetX,
                                                                        Mx.Mx_UpperPoints_6.OffsetY,
                                                                        Mx.Mx_UpperPoints_6.OffsetZ     );
            }
            


        }
    }
}
