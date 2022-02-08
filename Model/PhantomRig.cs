using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YAME.DataFomats;
using static Utility;

namespace YAME.Model
{
    public class PhantomRig : MyObject
    {
        Integrator_basic integrator_basic;
        IK_Module ik_module;
        ActuatorSystem ActSys;
        DOF_Data dof_data = new DOF_Data();
        
        int step_translations = 20;     //mm!
        int step_rotations = 5;         //degrees,... I think :-/   BETTER CONFIRM!
        float stepsize_trans_min = 0.01f;
        float stepsize_rot_min = 0.1f;

        public bool IsInlimits      { get { return ActSys.AllInLimits; } }
        public float ParkPos_Ideal  { get { return integrator_basic.Offset_Park; } }
        public float PausePos_Ideal { get { return integrator_basic.Offset_Pause; } }
        
        //Constructor
        public PhantomRig()
        {
            var mw = Application.Current.MainWindow as MainWindow;
            engine = mw.engine;

            //Objects:
            integrator_basic    = new Integrator_basic(engine.integrator);
            ik_module           = new IK_Module(integrator_basic);
            ActSys              = new ActuatorSystem(ik_module);

            //Settings:
            integrator_basic.Plat_Motion.IsParentOf(integrator_basic.UpperPoints);
            ActSys.Stroke = engine.actuatorsystem.Stroke;
            ActSys.MinLength = engine.actuatorsystem.MinLength;
            Update();

            calibrate();
        }
        
        void Update()
        {
            integrator_basic.Update(dof_data);
            ik_module.Update();
            ActSys.Update();
        }
        
        void calibrate()
        {
            Calibrate_OffsetPark();
            Calibrate_OffsetPause();
        }
        void Calibrate_OffsetPark()
        {
            //This algorithm finds AND SETS the Park Position (0% Actuator)

            ZeroAll_DOFs();

            //Attach UpperPoints to Plat_Park
            integrator_basic.Plat_Fix_Park.IsParentOf(integrator_basic.UpperPoints);
            
            //Flatten
            integrator_basic.Offset_Park = 0;
            Update();

            //Check plausibility
            if (IsInlimits) throw new Exception("A flat rig should be out of Limits!");

            //Root search
            float stepsize = step_translations;
            bool withinEnvelope = false;
            bool done = false;

            while (!done)
            {
                integrator_basic.Offset_Park += stepsize;
                Update();

                if (IsInlimits != withinEnvelope)               //Crossed a boundary?    
                {                                   
                    stepsize *= -0.5f;                          //Reverse and tighten search
                    withinEnvelope = IsInlimits;
                    if (Math.Abs(stepsize) < stepsize_trans_min && IsInlimits)
                    {
                        done = true;
                    }
                }
            }
            
            //Restore state
            integrator_basic.Plat_Motion.IsParentOf(integrator_basic.UpperPoints);
            Update();
        }
        void Calibrate_OffsetPause()
        {
            //This algorithm searches AND SETS the Pause Position (50% Actuator).
            //It relies upon the ParkPosition to be WITHIN THE ENVELOPE!!!

            //Attach UpperPoints to Plat_Pause
            integrator_basic.Plat_Fix_Pause.IsParentOf(integrator_basic.UpperPoints);

            //Start from Park Position
            integrator_basic.Offset_Pause = integrator_basic.Offset_Park;
            Update();

            //Check plausibility
            if (!IsInlimits) throw new Exception("The search for Pause Position must begin from within the envelpope");

            //Root search
            float stepsize = step_translations;
            bool below50pct = true;
            bool done = false;

            while (!done)
            {
                integrator_basic.Offset_Pause += stepsize;
                Update();

                bool crossingUpWards = ActSys.UtilisationList.Average() > 0.5f && below50pct;    //Crossing UP?
                bool crossingDownWards = ActSys.UtilisationList.Average() < 0.5f && !below50pct;   //Crossing DOWN?

                if (crossingUpWards || crossingDownWards)       //Crossing just took place
                {
                    stepsize *= -0.5f;
                }
                if (crossingUpWards) below50pct = false;
                if (crossingDownWards) below50pct = true;

                if (Math.Abs(stepsize) < stepsize_trans_min) done = true;
            }

            //Restore state
            integrator_basic.Plat_Motion.IsParentOf(integrator_basic.UpperPoints);
            Update();
        }

        //Probes
        public float ProbeMargin_FromWithin(DOF dof, bool IsPositive = true)
        {
            //This function probes the DISTANCE to the boundary from a given starting point by manipulating a
            //given dof. It returns a value indicating how far it can go in that direction on a given dof.
            if (!IsInlimits) throw new Exception("Boundary search must start from within envelope.");
            
            bool withinEnvelope = true;
            bool done = false;
            float stepsize;
            float stepsize_abort;

            //Remember
            DOF_Data dof_data_old = new DOF_Data(dof_data);

            if (IsTranslation(dof))
            {
                stepsize = step_translations;
                stepsize_abort = stepsize_trans_min;
            }  
            else
            {
                stepsize = step_rotations;
                stepsize_abort = stepsize_rot_min;
            }

            if (!IsPositive) stepsize *= -1;

            //Root search:
            while (!done)
            {
                Increment(dof, stepsize);
                if (IsInlimits != withinEnvelope)       //We crossed a boundary
                {
                    stepsize *= -0.5f;
                    withinEnvelope = IsInlimits;
                    if (stepsize < stepsize_abort && IsInlimits) done = true;
                }
            }  

            float limit = dof_data.report(dof);         //We have a result!
            GoTo(dof_data_old);                         //Restore state                          

            return limit; 
        }
        public bool CanHandle(float surge = 0, float heave = 0, float sway = 0, float yaw = 0, float pitch = 0, float roll = 0, float pitch_lfc = 0, float roll_lfc = 0)
        {
            DOF_Data Temp =  new DOF_Data(  surge,
                                            heave,
                                            sway,
                                            yaw,
                                            pitch,
                                            roll,
                                            pitch_lfc,
                                            roll_lfc);
            return CanHandle(Temp);
        }
        public bool CanHandle(DOF_Data dd)
        {
            DOF_Data dof_data_old = new DOF_Data(dof_data);     //Remember

            GoTo(dd);                                           //Go    
            bool canHandle = IsInlimits;                        //Check
            GoTo(dof_data_old);                                 //Restore

            return canHandle;
        }
        
        //DOF manipulators
        public void Increment(DOF dof, float value)
        {
            switch (dof)
            {
                case DOF.surge:
                    dof_data.HFC_Surge += value;
                    break;
                case DOF.heave:
                    dof_data.HFC_Heave += value;
                    break;
                case DOF.sway:
                    dof_data.HFC_Sway += value;
                    break;
                case DOF.yaw:
                    dof_data.HFC_Yaw += value;
                    break;
                case DOF.pitch:
                    dof_data.HFC_Pitch += value;
                    break;
                case DOF.roll:
                    dof_data.HFC_Roll += value;
                    break;
                case DOF.pitch_lfc:
                    dof_data.LFC_Pitch += value;
                    break;
                case DOF.roll_lfc:
                    dof_data.LFC_Roll += value;
                    break;
                default:
                    throw new Exception($"Unknown DOF: {dof}");
            }
            Update();
        }
        public void GoTo(float surge = 0, float heave = 0, float sway = 0, float yaw = 0, float pitch = 0, float roll = 0, float pitch_lfc = 0, float roll_lfc = 0)
        {
            dof_data = new DOF_Data(surge, heave, sway, yaw, pitch, roll, pitch_lfc, roll_lfc);
            Update();
        }
        public void GoTo(DOF_Data dd)
        {
            dof_data = dd;
            Update();
        }
        public void ZeroAll_DOFs()
        {
            ZeroTranslations();
            ZeroRotations();
        }
        public void ZeroTranslations()
        {
            GoTo(0, 0, 0, dof_data.HFC_Yaw, dof_data.HFC_Pitch, dof_data.HFC_Roll, dof_data.LFC_Pitch, dof_data.LFC_Roll);
        }
        public void ZeroRotations()
        {
            GoTo(dof_data.HFC_Surge, dof_data.HFC_Heave, dof_data.HFC_Sway, 0, 0, 0, 0, 0);
        }
    }
}
