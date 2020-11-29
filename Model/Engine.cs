using MOTUS.DataFomats;
using MOTUS.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class Engine
    {
        //Objects:
        public BackgroundWorker         backgroundworker;
        public Server                   server;
        public Chopper                  chopper;
        public Inverter                 inverter;
        public CrashDetector            crashdetector;
        public PositionOffsetCorrector  positionoffsetcorrector;
        public Protector                protector;
        public AlphaCompensator         alphacompensator;
        public FilterSystem             filtersystem;
        public CompressorSystem         compressorsystem;
        //...
        //...
        //...

        //ViewModels:
        public ViewModel_MainWindow                 VM_MainWindow;
        public ViewModel_RawData                    VM_Rawdata;
        public ViewModel_CrashDetector              VM_CrashDetector;
        public ViewModel_PositionOffsetCorrector    VM_PositionOffsetCorrector;
        public ViewModel_AlphaCompensator           VM_AlphaCompensator;
        public ViewModel_FiltersWindow              VM_FiltersWindow;
        //...
        //...
        //...


        //Internal properties:
        float deltatime_processing;
        Stopwatch stopwatch = new Stopwatch();

        public Engine()
        {
            InstatiateObjects();
            InstantiateViewModels();
        }

        private void InstatiateObjects()
        {
            backgroundworker        = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
            };
            server                  = new Server();
            chopper                 = new Chopper();
            inverter                = new Inverter();
            crashdetector           = new CrashDetector();
            positionoffsetcorrector = new PositionOffsetCorrector();
            protector               = new Protector();
            alphacompensator        = new AlphaCompensator();
            filtersystem            = new FilterSystem();
            compressorsystem        = new CompressorSystem();
            //...
            //...
            //...
        }
        private void InstantiateViewModels()
        {
            VM_MainWindow               = new ViewModel_MainWindow(this);
            VM_Rawdata                  = new ViewModel_RawData(this);
            VM_CrashDetector            = new ViewModel_CrashDetector(this);
            VM_PositionOffsetCorrector  = new ViewModel_PositionOffsetCorrector(this);
            VM_AlphaCompensator         = new ViewModel_AlphaCompensator(this);
            VM_FiltersWindow            = new ViewModel_FiltersWindow(this);
                //VM_FilterBox_WX_HP       = new VM_FilterBox_WX_HP(this);
            //...
            //...
            //...
        }

        public void StartEngine()
        {
            if (!backgroundworker.IsBusy)
            {
                server.StartServer();
                stopwatch.Start();

                backgroundworker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    while (!backgroundworker.CancellationPending)
                    {
                        Update();
                        MeasureLoopTime();
                        Thread.Sleep(1);
                    }
                };
                backgroundworker.RunWorkerAsync();
            }
            
        }
        public void StopEngine()
        {
            backgroundworker.CancelAsync();
        }

        private void Update()
        {
            Update_Server();
            Update_Chopper();
            Update_Inverter();
            Update_Crashdetector();
            Update_PositionOffsetCorrector();
            Update_Protector();
            Update_Alphacompensator();
            Update_Filtersystem();
            Update_CompressorSytem();
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
        private void Update_Crashdetector()
        {
            crashdetector.CheckForCrash(inverter.Output);
            //The ViewModel is being updated by the StartStopLogic
        }
        private void Update_PositionOffsetCorrector()
        {
            positionoffsetcorrector.Process(crashdetector.Output, deltatime_processing);
            #region Update ViewModel
            VM_PositionOffsetCorrector.IsActive = positionoffsetcorrector.IsActive;
            VM_PositionOffsetCorrector.Ax_output = positionoffsetcorrector.Ax_output;
            VM_PositionOffsetCorrector.Ay_output = positionoffsetcorrector.Ay_output;
            VM_PositionOffsetCorrector.Az_output = positionoffsetcorrector.Az_output;
            #endregion
        }
        private void Update_Protector()
        {
            protector.Process(positionoffsetcorrector.Output);
            //No ViewModel here
        }
        private void Update_Alphacompensator()
        {
            alphacompensator.Process(protector.Output);
            #region Update ViewModel
            VM_AlphaCompensator.AoA = alphacompensator.AoA;
            VM_AlphaCompensator.FadeIn_Percentage = alphacompensator.FadeIn_Percentage;
            #endregion
        }
        private void Update_Filtersystem()
        {
            filtersystem.Process(alphacompensator.Output);

            #region Update ViewModel
            //...
            //...
            //...
            #endregion
        }
        private void Update_CompressorSytem()
        {
            compressorsystem.Process(filtersystem.Output);
            //No ViewModel, because the View binds directly into the objects.
        }


        //Helpers:
        private void MeasureLoopTime()
        {
            var looptime = (float)stopwatch.Elapsed.TotalMilliseconds;
            deltatime_processing = looptime;
            VM_MainWindow.DeltaTime_Processing = looptime;
            stopwatch.Restart();
        }
    }
}
