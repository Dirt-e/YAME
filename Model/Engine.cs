﻿using MOTUS.DataFomats;
using MOTUS.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MOTUS.Model
{
    public class Engine
    {   
        //UI-thread objects:
        public Server                   server;
        public LoaderSaver              loadersaver;

        //Worker-thread objects:
        public BackgroundWorker         backgroundworker;
        public Chopper                  chopper;
        public Inverter                 inverter;
        public ExceedanceDetector       exceedancedetector;
        public RecoveryLogic            recoverylogic;
        public PositionOffsetCorrector  positionoffsetcorrector;
        public Protector                protector;
        public AlphaCompensator         alphacompensator;
        public FilterSystem             filtersystem;
        public CompressorSystem         compressorsystem;
        public ScalerSystem             scalersystem;
        public ZeroMaker                zeromaker;
        public DOF_Override             dof_override;
        public Integrator               integrator;
        public IK_Module                IK_Module;
        public ActuatorSystem           actuatorsystem;
        public SerialTalker             serialtalker;
        //...
        //...
        //...

        //ViewModels:
        public ViewModel_MainWindow                 VM_MainWindow;
        public ViewModel_RawData                    VM_Rawdata;
        public ViewModel_CrashDetector              VM_CrashDetector;
        public ViewModel_PositionOffsetCorrector    VM_PositionOffsetCorrector;
        public ViewModel_FiltersWindow              VM_FiltersWindow;
        public ViewModel_MotionControlWindow        VM_MotionControlWindow;
        public ViewModel_Sceneview                  VM_SceneView;
        //...
        //...
        //...


        //Internal properties:

        float deltatime_processing;
        Stopwatch stopwatch = Stopwatch.StartNew();

        public Engine()
        {
            backgroundworker    = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
            };
            server              = new Server();
            loadersaver         = new LoaderSaver(this);
            InstantiateViewModels();
        }
        
        private void InstantiateViewModels()
        {
            VM_MainWindow               = new ViewModel_MainWindow(this);
            VM_Rawdata                  = new ViewModel_RawData(this);
            VM_CrashDetector            = new ViewModel_CrashDetector(this);
            VM_PositionOffsetCorrector  = new ViewModel_PositionOffsetCorrector(this);
            VM_FiltersWindow            = new ViewModel_FiltersWindow(this);
            VM_MotionControlWindow      = new ViewModel_MotionControlWindow();
            VM_SceneView                = new ViewModel_Sceneview(this);
            //...
            //...
            //...
        }
        
        public void StartEngine()
        {
            if (!backgroundworker.IsBusy)
            {
                server.StartServer();

                backgroundworker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    InstatiateObjects();
                    while (!backgroundworker.CancellationPending)
                    {
                        UpdateObjects();
                        WaitForTargetFramerate(500);
                        //if (VM_MainWindow.IsChecked_HotIdle) WaitForTargetFramerate(500);
                        //else if (VM_MainWindow.IsChecked_ShortSleep) ShortSleep(3);
                    }
                };
                backgroundworker.RunWorkerAsync();
            }
            
        }
        public void StopEngine()
        {
            backgroundworker.CancelAsync();
        }

        //------- This happens on the Worker Thread -----------------
        private void InstatiateObjects()
        {
            chopper                 = new Chopper();
            inverter                = new Inverter();
            exceedancedetector      = new ExceedanceDetector(this);
            recoverylogic           = new RecoveryLogic(this);
            positionoffsetcorrector = new PositionOffsetCorrector();
            protector               = new Protector();
            alphacompensator        = new AlphaCompensator();
            filtersystem            = new FilterSystem();
            compressorsystem        = new CompressorSystem();
            scalersystem            = new ScalerSystem();
            zeromaker               = new ZeroMaker();
            dof_override            = new DOF_Override();
            loadersaver             = new LoaderSaver(this);
            integrator              = new Integrator(this);
            IK_Module               = new IK_Module(ref integrator);
            actuatorsystem          = new ActuatorSystem(ref IK_Module);
            serialtalker            = new SerialTalker(this);
            //...
            //...
            //...
        }
        private void UpdateObjects()
        {
            Update_Server();
            Update_Chopper();
            Update_Inverter();
            Update_ExceedanceDetector();
            Update_RecoveryLogic();
            Update_PositionOffsetCorrector();
            Update_Protector();
            Update_Alphacompensator();
            Update_Filtersystem();
            Update_CompressorSytem();
            Update_ScalerSystem();
            Update_ZeroMaker();
            Update_DOF_Override();
            Update_Integrator();
            Update_IK_Module();
            Update_ActuatorSystem();
            Update_SerialTalker();
            //TestCode:
            
            //...
            //...
            //...
        }

        private void Update_Server()
        {
            server.Read();
        }
        private void Update_Chopper()
        {
            chopper.ChopParseAndPackage(server.RawDatastring);

            #region update ViewModel
            VM_Rawdata.IAS = chopper.Output.IAS;
            VM_Rawdata.MACH = chopper.Output.MACH;
            VM_Rawdata.TAS = chopper.Output.TAS;
            VM_Rawdata.GS = chopper.Output.GS;
            VM_Rawdata.AOA = chopper.Output.AOA;
            VM_Rawdata.VS = chopper.Output.VS;
            VM_Rawdata.HGT = chopper.Output.HGT;

            VM_Rawdata.HDG = chopper.Output.HDG;
            VM_Rawdata.PITCH = chopper.Output.PITCH;
            VM_Rawdata.BANK = chopper.Output.BANK;

            VM_Rawdata.WX = chopper.Output.WX;
            VM_Rawdata.WY = chopper.Output.WY;
            VM_Rawdata.WZ = chopper.Output.WZ;

            VM_Rawdata.AX = chopper.Output.AX;
            VM_Rawdata.AY = chopper.Output.AY;
            VM_Rawdata.AZ = chopper.Output.AZ;

            VM_Rawdata.TIME = chopper.Output.TIME;
            VM_Rawdata.COUNTER = chopper.Output.COUNTER;
            VM_Rawdata.SIM = chopper.Output.SIMULATOR;
            #endregion
            
        }
        private void Update_Inverter()
        {
            inverter.InvertDataAsNeeded(chopper.Output);

            #region Update ViewModel
            VM_FiltersWindow.InvertWx = inverter.Invert_Wx;
            VM_FiltersWindow.InvertWy = inverter.Invert_Wy;
            VM_FiltersWindow.InvertWz = inverter.Invert_Wz; 

            VM_FiltersWindow.InvertAx = inverter.Invert_Ax;
            VM_FiltersWindow.InvertAy = inverter.Invert_Ay;
            VM_FiltersWindow.InvertAz = inverter.Invert_Az;
            #endregion

        }
        private void Update_ExceedanceDetector()
        {
            exceedancedetector.Process(inverter.Output);
        }
        private void Update_RecoveryLogic()
        {
            recoverylogic.Update();
        }
        private void Update_PositionOffsetCorrector()
        {
            positionoffsetcorrector.Process(exceedancedetector.Output, deltatime_processing);
            //#region Update ViewModel
            //VM_PositionOffsetCorrector.Delta_X = positionoffsetcorrector.Delta_X;
            //VM_PositionOffsetCorrector.Delta_Y = positionoffsetcorrector.Delta_Y;
            //VM_PositionOffsetCorrector.Delta_Z = positionoffsetcorrector.Delta_Z;
            //VM_PositionOffsetCorrector.Ax_output = positionoffsetcorrector.Ax_output;
            //VM_PositionOffsetCorrector.Ay_output = positionoffsetcorrector.Ay_output;
            //VM_PositionOffsetCorrector.Az_output = positionoffsetcorrector.Az_output;
            //VM_PositionOffsetCorrector.IsActive = positionoffsetcorrector.IsActive;
            //#endregion
        }
        private void Update_Protector()
        {
            protector.Process(positionoffsetcorrector.Output);
        }
        private void Update_Alphacompensator()
        {
            alphacompensator.Process(protector.Output);
        }
        private void Update_Filtersystem()
        {
            filtersystem.Process(alphacompensator.Output);
        }
        private void Update_CompressorSytem()
        {
            compressorsystem.Process(filtersystem.Output);
        }
        private void Update_ScalerSystem()
        {
            scalersystem.Process(compressorsystem.Output);
        }
        private void Update_ZeroMaker()
        {
            zeromaker.Process(scalersystem.Output);
        }
        private void Update_DOF_Override()
        {
            dof_override.Process(zeromaker.Output);
        }
        private void Update_Integrator()
        {
            integrator.Update(dof_override.Output);
        }
        private void Update_IK_Module()
        {
            IK_Module.Update();
        }
        private void Update_ActuatorSystem()
        {
            actuatorsystem.Update();
        }
        private void Update_SerialTalker()
        {
            serialtalker.Update(actuatorsystem.Output);
        }

        //Helpers:
        private void WaitForTargetFramerate(int fps)
        {
            //Hic sunt dracones!
            var targetTicksPerFrame = Stopwatch.Frequency / fps;
            
            while (stopwatch.ElapsedTicks < targetTicksPerFrame)
            {
                //DoNothing();
            }

            var looptime = (float)stopwatch.Elapsed.TotalMilliseconds;
            deltatime_processing = looptime;
            VM_MainWindow.DeltaTime_Processing = looptime;
            stopwatch.Restart();
        }
        private void ShortSleep(int ms)
        {
            Thread.Sleep(ms);
            var looptime = (float)stopwatch.Elapsed.TotalMilliseconds;
            deltatime_processing = looptime;
            VM_MainWindow.DeltaTime_Processing = looptime;
            stopwatch.Restart();
        }
    }
}
