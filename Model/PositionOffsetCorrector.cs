﻿using YAME.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class PositionOffsetCorrector : MyObject
    {
        public PreprocessorData Output = new PreprocessorData();
        public PreprocessorData PreviousOutput = new PreprocessorData();
        public PreprocessorData Input = new PreprocessorData();

        #region ViewModel
        bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        float _delta_x = 0;
        public float Delta_X
        {
            get { return _delta_x; }
            set
            {
                _delta_x = value;
                OnPropertyChanged(nameof(Delta_X));
            }
        }
        float _delta_y = 0;
        public float Delta_Y
        {
            get { return _delta_y; }
            set
            {
                _delta_y = value;
                OnPropertyChanged(nameof(Delta_Y));
            }
        }
        float _delta_z = 0;
        public float Delta_Z
        {
            get { return _delta_z; }
            set
            {
                _delta_z = value;
                OnPropertyChanged(nameof(Delta_Z));
            }
        }

        float _ax_output;
        public float Ax_output
        {
            get { return _ax_output; }
            set
            {
                _ax_output = value;
                OnPropertyChanged(nameof(Ax_output));
            }
        }
        float _ay_output;
        public float Ay_output
        {
            get { return _ay_output; }
            set
            {
                _ay_output = value;
                OnPropertyChanged(nameof(Ay_output));
            }
        }
        float _az_output;
        public float Az_output
        {
            get { return _az_output; }
            set
            {
                _az_output = value;
                OnPropertyChanged(nameof(Az_output));
            }
        }
        #endregion

        #region Internal Properties
        private float delta_Wx;
        private float delta_Wy;
        private float delta_Wz;

        private float deltaTime;

        private float Wx_dot;
        private float Wy_dot;
        private float Wz_dot;

        //LP filter to get a smoother signal, because processing is done at 500FPS while the sim runs at ~60Fps
        //That means that the value changes only on every 10th processing cycle.
        private float adoptionrate = 0.1f;   
        
        private float Wx_dot_smooth;
        private float Wy_dot_smooth;
        private float Wz_dot_smooth;

        private float cent_x;
        private float cent_y;
        private float cent_z;

        private float tang_x;
        private float tang_y;
        private float tang_z;
        #endregion

        public void Process(PreprocessorData data, float deltatime)
        {
            Input = new PreprocessorData(data);
            deltaTime = deltatime;

            if (IsActive) ApplyPositionCorrection();
            else PassThrough();

            PreviousOutput = new PreprocessorData(Output);
        }

        private void ApplyPositionCorrection()
        {
            CalculateDeltasSinceLastFrame();
            CalculateOmegaDots();
            Correct_Ax();
            Correct_Ay();
            Correct_Az();
            WriteCorrectedOuptutData();
        }

        private void CalculateDeltasSinceLastFrame()
        {
            delta_Wx = Input.WX - Output.WX;        //Output is still the previous frames Output, we have not changed anything yet!
            delta_Wy = Input.WY - Output.WY;
            delta_Wz = Input.WZ - Output.WZ;

        }
        private void CalculateOmegaDots()
        {
            if (deltaTime > 0)                      //Prevents division by zero
            {
                Wx_dot = 1000 * delta_Wx / deltaTime;       //Factor of 1000 to convert °/ms into  °/s 
                Wy_dot = 1000 * delta_Wy / deltaTime;
                Wz_dot = 1000 * delta_Wz / deltaTime;
            }

            Wx_dot_smooth = Wx_dot * adoptionrate + Wx_dot_smooth * (1 - adoptionrate);
            Wy_dot_smooth = Wy_dot * adoptionrate + Wy_dot_smooth * (1 - adoptionrate);
            Wz_dot_smooth = Wz_dot * adoptionrate + Wz_dot_smooth * (1 - adoptionrate);
        }
        void Correct_Ax()
        {
            //Centrifugal acceleration:
            cent_x = -Delta_X * (float)(Math.Pow((Input.WY), 2) + Math.Pow((Input.WZ), 2));      //division by 9.81 to convert m/s^2 into G.
            //Tangential acceleration:
            tang_x = Wy_dot_smooth * Delta_Z - Wz_dot_smooth * Delta_Y;                       //division by 9.81 to convert m/s^2 into G.

            Ax_output = Input.AX + cent_x + tang_x;
        }
        void Correct_Ay()
        {
            //Centrifugal acceleration:
            cent_y = -Delta_Y * (float)(Math.Pow((Input.WX), 2) + Math.Pow((Input.WZ), 2));     //division by 9.81 to convert m/s^2 into G.
            //Tangential acceleration:
            tang_y = (Wz_dot_smooth * Delta_X - Wx_dot_smooth * Delta_Z);                       //division by 9.81 to convert m/s^2 into G.

            Ay_output = Input.AY + cent_y + tang_y;
        }
        void Correct_Az()
        {
            //Centrifugal acceleration:
            cent_z = -Delta_Z * (float)(Math.Pow((Input.WX), 2) + Math.Pow((Input.WY), 2));     //division by 9.81 to convert m/s^2 into G.
            //Tangential acceleration:
            tang_z = (Wx_dot_smooth * Delta_Y - Wy_dot_smooth * Delta_X);                       //division by 9.81 to convert m/s^2 into G.

            Az_output = Input.AZ + cent_z + tang_z;
        }
        void WriteCorrectedOuptutData()
        {
            //First, just copy over EVERYTHING...
            Output = new PreprocessorData(Input);

            //...then manipulate as needed
            Output.AX = Ax_output;
            Output.AY = Ay_output;
            Output.AZ = Az_output;
        }

        void PassThrough()
        {
            Output = new PreprocessorData(Input);

            //Just for the UI:
            Ax_output = Output.AX;
            Ay_output = Output.AY;
            Az_output = Output.AZ;
        }
    }
}
