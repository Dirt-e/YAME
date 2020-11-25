﻿using MOTUS.DataFomats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class PositionOffsetCorrector
    {
        public PreprocessorData Output;
        private PreprocessorData Input;

        public bool IsActive { get; set; }

        public float Delta_X { get; set; }
        public float Delta_Y { get; set; }
        public float Delta_Z { get; set; }
        
        #region Internal Properties
        private float delta_Wx;
        private float delta_Wy;
        private float delta_Wz;

        private float deltaTime;

        private float Wx_dot;
        private float Wy_dot;
        private float Wz_dot;

        private float adoptionrate = 0.3f;        //slight LP filter to get a smoother signal
        private float Wx_dot_smooth;
        private float Wy_dot_smooth;
        private float Wz_dot_smooth;

        private float cent_x;
        private float cent_y;
        private float cent_z;

        private float tang_x;
        private float tang_y;
        private float tang_z;

        private float _ax_output;
        private float _ay_output;
        private float _az_output;
        #endregion

        public void Process(PreprocessorData data, float deltatime)
        {
            Input = data;
            deltaTime = deltatime;

            if (IsActive) ApplyPositionCorrection();
            else PassThrough();
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
                Wx_dot = delta_Wx / deltaTime;
                Wy_dot = delta_Wy / deltaTime;
                Wz_dot = delta_Wz / deltaTime;
            }

            Wx_dot_smooth = Wx_dot * adoptionrate + Wx_dot_smooth * (1 - adoptionrate);
            Wy_dot_smooth = Wy_dot * adoptionrate + Wy_dot_smooth * (1 - adoptionrate);
            Wz_dot_smooth = Wz_dot * adoptionrate + Wz_dot_smooth * (1 - adoptionrate);
        }
        void Correct_Ax()
        {
            //Centrifugal acceleration:
            cent_x = -Delta_X * (float)(Math.Pow((Input.WY), 2) + Math.Pow((Input.WZ), 2)) / 9.81f;      //division by 9.81 to convert m/s^2 into G.
            //Tangential acceleration:
            tang_x = (Wy_dot_smooth * Delta_Z - Wz_dot_smooth * Delta_Y) / 9.81f;                                             //division by 9.81 to convert m/s^2 into G.

            _ax_output = Input.AX + cent_x + tang_x;
        }
        void Correct_Ay()
        {
            //Centrifugal acceleration:
            cent_y = -Delta_Y * (float)(Math.Pow((Input.WX), 2) + Math.Pow((Input.WZ), 2)) / 9.81f;     //division by 9.81 to convert m/s^2 into G.
            //Tangential acceleration:
            tang_y = (Wz_dot_smooth * Delta_X - Wx_dot_smooth * Delta_Z) / 9.81f;                                             //division by 9.81 to convert m/s^2 into G.

            _ay_output = Input.AY + cent_y + tang_y;
        }
        void Correct_Az()
        {
            //Centrifugal acceleration:
            cent_z = -Delta_Z * (float)(Math.Pow((Input.WX), 2) + Math.Pow((Input.WY), 2)) / 9.81f;     //division by 9.81 to convert m/s^2 into G.
            //Tangential acceleration:
            tang_z = (Wx_dot_smooth * Delta_Y - Wy_dot_smooth * Delta_X) / 9.81f;                                               //division by 9.81 to convert m/s^2 into G.

            _az_output = Input.AZ + cent_z + tang_z;
        }

        void WriteCorrectedOuptutData()
        {
            //First, just copy over EVERYTHING...
            Output = Input;

            //...then manipulate as needed
            Output.AX = _ax_output;
            Output.AY = _ay_output;
            Output.AZ = _az_output;
        }
        void PassThrough()
        {
            Output = Input;
        }
    }
}
