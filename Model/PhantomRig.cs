using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
        static Random Random = new Random();
        
        int step_translations = 20;     //mm!
        int step_rotations = 5;         //degrees,... I think :-/   BETTER CONFIRM!
        float stepsize_trans_min = 0.01f;
        float stepsize_rot_min = 0.1f;
        int numberOfPointsOnList = 100;

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

                bool crossingUpWards = ActSys.UtilisationList.Average() > 0.5f && below50pct;       //Crossing UP?
                bool crossingDownWards = ActSys.UtilisationList.Average() < 0.5f && !below50pct;    //Crossing DOWN?

                if (crossingUpWards || crossingDownWards)       //Crossing just took place
                {
                    stepsize *= -0.5f;
                }
                if (crossingUpWards)    below50pct = false;
                if (crossingDownWards)  below50pct = true;

                if (Math.Abs(stepsize) < stepsize_trans_min) done = true;
            }

            //Restore state
            integrator_basic.Plat_Motion.IsParentOf(integrator_basic.UpperPoints);
            Update();
        }

        //Probes
        public float Probe_DOF(DOF dof, bool IsPositive = true)
        {
            //This function probes to which value on the given DOF the platform can go before
            //reaching the boundary. It returns an ABSOLUTE value given in the DOFs unit.
            //However, it does NOT actually go there!

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
                    if (Math.Abs(stepsize) < stepsize_abort && IsInlimits) done = true;
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
        void Increment(DOF dof, float value)
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
        void GoTo(float surge = 0, float heave = 0, float sway = 0, float yaw = 0, float pitch = 0, float roll = 0, float pitch_lfc = 0, float roll_lfc = 0)
        {
            dof_data = new DOF_Data(surge, heave, sway, yaw, pitch, roll, pitch_lfc, roll_lfc);
            Update();
        }
        void GoTo(DOF_Data dd)
        {
            dof_data = dd;
            Update();
        }
        void SetDOF(DOF dof, float val)
        {
            //this function sets a single DOF while leaving all other DOFs unchanged.
            switch (dof)
            {
                case DOF.surge:
                    dof_data.HFC_Surge = val;
                    break;
                case DOF.heave:
                    dof_data.HFC_Heave = val;
                    break;
                case DOF.sway:
                    dof_data.HFC_Sway = val;
                    break;
                case DOF.yaw:
                    dof_data.HFC_Yaw = val;
                    break;
                case DOF.pitch:
                    dof_data.HFC_Pitch = val;
                    break;
                case DOF.roll:
                    dof_data.HFC_Roll = val;
                    break;
                case DOF.pitch_lfc:
                    dof_data.LFC_Pitch = val;
                    break;
                case DOF.roll_lfc:
                    dof_data.LFC_Roll = val;
                    break;
                default:
                    throw new Exception($"Unknown DOF: {dof}");
            }
            Update();
        }
        void ZeroAll_DOFs()
        {
            ZeroTranslations();
            ZeroRotations();
        }
        void ZeroTranslations()
        {
            GoTo(0, 0, 0, dof_data.HFC_Yaw, dof_data.HFC_Pitch, dof_data.HFC_Roll, dof_data.LFC_Pitch, dof_data.LFC_Roll);
        }
        void ZeroRotations()
        {
            GoTo(dof_data.HFC_Surge, dof_data.HFC_Heave, dof_data.HFC_Sway, 0, 0, 0, 0, 0);
        }

        //Explorers
        public List<Point> Explore(DOF dof_x, DOF dof_y)
        {
            List<Point> list = new List<Point> ();


            while (list.Count < numberOfPointsOnList)
            {
                //Probe_x (+/-)
                float max_x = Probe_DOF(dof_x, true);
                float min_x = Probe_DOF(dof_x, false);
                
                //Add 2 bounding points to list
                float current_y = dof_data.report(dof_y);
                list.Add(new Point(max_x, current_y));
                list.Add(new Point(min_x, current_y));

                //Goto Random point on range
                float range_x = max_x - min_x;
                float pointOnRange_x = min_x + (float)Random.NextDouble() * range_x;
                SetDOF(dof_x, pointOnRange_x);


                //Probe_y (+/-)
                float max_y = Probe_DOF(dof_y, true);
                float min_y = Probe_DOF(dof_y, false);

                //Add 2 bounding points to list
                float current_x = dof_data.report(dof_x);
                list.Add(new Point(current_x, max_y));
                list.Add(new Point(current_x, min_y));

                //Goto Random point on range
                float range_y = max_y - min_y;
                float pointOnRange_y = min_y + (float)Random.NextDouble() * range_y;
                SetDOF(dof_y, pointOnRange_y);
            }

            return list;
        }
    }
}
