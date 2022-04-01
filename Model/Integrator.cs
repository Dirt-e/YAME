using YAME.DataFomats;
using YAME.ViewModel;
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
using System.Windows.Input;

namespace YAME.Model
{
    public class Integrator : Integrator_basic
    {
        //The Integrator ONLY generates the different platforms (Airplane.STL) and displays them
        //via a Dispatcher.invoke() call. All inverse kinematics are done in the IK Module!!!

        Stopwatch invoke_timer = Stopwatch.StartNew();

        public Lerp_3Way Lerp_3Way;
        float fade_duration_ParkToPause_seconds = 5;
        float fade_duration_PauseToMotion_seconds = 5;

        public Integrator(Engine e)
        {
            engine = e;         //To accesss other Objects

            Lerp_3Way = new Lerp_3Way(  TimeSpan.FromSeconds(fade_duration_ParkToPause_seconds),
                                        TimeSpan.FromSeconds(fade_duration_PauseToMotion_seconds),
                                        engine);

        }

          

        new public void Update(DOF_Data data)
        {
            base.Update(data);

            

            LerpPhysical_Between_ParkPauseMotion();
            UpdateUI_ViaDispatcherInvoke();
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

        #region Update UI Callback
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

                mainwindow.engine.VM_SceneView.Lower1 = new Point3D(    Mx.Mx_LowerPoints_1.OffsetX,
                                                                        Mx.Mx_LowerPoints_1.OffsetY,
                                                                        Mx.Mx_LowerPoints_1.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower2 = new Point3D(    Mx.Mx_LowerPoints_2.OffsetX,
                                                                        Mx.Mx_LowerPoints_2.OffsetY,
                                                                        Mx.Mx_LowerPoints_2.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower3 = new Point3D(    Mx.Mx_LowerPoints_3.OffsetX,
                                                                        Mx.Mx_LowerPoints_3.OffsetY,
                                                                        Mx.Mx_LowerPoints_3.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower4 = new Point3D(    Mx.Mx_LowerPoints_4.OffsetX,
                                                                        Mx.Mx_LowerPoints_4.OffsetY,
                                                                        Mx.Mx_LowerPoints_4.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower5 = new Point3D(    Mx.Mx_LowerPoints_5.OffsetX,
                                                                        Mx.Mx_LowerPoints_5.OffsetY,
                                                                        Mx.Mx_LowerPoints_5.OffsetZ);
                mainwindow.engine.VM_SceneView.Lower6 = new Point3D(    Mx.Mx_LowerPoints_6.OffsetX,
                                                                        Mx.Mx_LowerPoints_6.OffsetY,
                                                                        Mx.Mx_LowerPoints_6.OffsetZ);

                mainwindow.engine.VM_SceneView.Upper1 = new Point3D(    Mx.Mx_UpperPoints_1.OffsetX,
                                                                        Mx.Mx_UpperPoints_1.OffsetY,
                                                                        Mx.Mx_UpperPoints_1.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper2 = new Point3D(    Mx.Mx_UpperPoints_2.OffsetX,
                                                                        Mx.Mx_UpperPoints_2.OffsetY,
                                                                        Mx.Mx_UpperPoints_2.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper3 = new Point3D(    Mx.Mx_UpperPoints_3.OffsetX,
                                                                        Mx.Mx_UpperPoints_3.OffsetY,
                                                                        Mx.Mx_UpperPoints_3.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper4 = new Point3D(    Mx.Mx_UpperPoints_4.OffsetX,
                                                                        Mx.Mx_UpperPoints_4.OffsetY,
                                                                        Mx.Mx_UpperPoints_4.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper5 = new Point3D(    Mx.Mx_UpperPoints_5.OffsetX,
                                                                        Mx.Mx_UpperPoints_5.OffsetY,
                                                                        Mx.Mx_UpperPoints_5.OffsetZ);
                mainwindow.engine.VM_SceneView.Upper6 = new Point3D(    Mx.Mx_UpperPoints_6.OffsetX,
                                                                        Mx.Mx_UpperPoints_6.OffsetY,
                                                                        Mx.Mx_UpperPoints_6.OffsetZ);
            }
        } 
        #endregion
    }
}
