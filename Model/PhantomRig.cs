using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YAME.DataFomats;

namespace YAME.Model
{
    public class PhantomRig : MyObject
    {
        Integrator_basic integrator_basic;
        IK_Module ik_module;
        ActuatorSystem ActSys;

        DOF_Data dof_data = new DOF_Data();
        public bool IsInlimits
        { 
            get
            {
                return ActSys.AllInLimits;
            }
        }

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
            ActSys.MaxLength = engine.actuatorsystem.MaxLength;
            ActSys.MinLength = engine.actuatorsystem.MinLength;

            Update();
        }
        
        void Update()
        {
            integrator_basic.Update(dof_data);
            ik_module.Update();
            ActSys.Update();
        }

        public void Process(float surge = 0, float heave = 0, float sway = 0, float yaw = 0, float pitch = 0, float roll = 0, float pitch_lfc = 0, float roll_lfc = 0)
        {
            dof_data = new DOF_Data(surge, heave, sway, yaw, pitch, roll, pitch_lfc, roll_lfc);
            Update();
        }
        public bool CanHandle(float surge = 0, float heave = 0, float sway = 0, float yaw = 0, float pitch = 0, float roll = 0, float pitch_lfc = 0, float roll_lfc = 0)
        {
            Process(surge, heave, sway, yaw, pitch, roll, pitch_lfc, roll_lfc);

            if (IsInlimits) return true;
            return false;
        }
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
        public float RootSearchBoundary_FromOutside(DOF dof, float value)
        {
            //This algorithm searches the Pause Position (50%) starting from a "flat rig"
            if (IsInlimits) throw new Exception("This function must start from outside envelope. (Flat rig!)");

            bool withinEnvelope = false;
            bool done = false;
            int level = 0;

            while (!done)
            {
                Increment(dof, value);
                if (IsInlimits != withinEnvelope)
                {
                    value *= -0.5f;
                    withinEnvelope = IsInlimits;
                    level++;
                    if (level >= 5) done = true;            //Magic Constant
                }
            }
            float limit = dof_data.report(dof);
            Debug.WriteLine(limit);

            return limit;
        }
        public float RootSearchBoundary_FromWithin(DOF dof, float value)
        {
            if (!IsInlimits) throw new Exception("Root search must start from within envelope.");
            
            bool withinEnvelope = true;
            bool done = false;
            int level = 0;  

            while (!done)
            {
                Increment(dof, value);
                if (IsInlimits != withinEnvelope)
                {
                    value *= -0.5f;
                    withinEnvelope = IsInlimits;
                    level++;
                    if (level >= 10) done = true;            //Magic Constant
                }
            }  
            float limit = dof_data.report(dof);
            Debug.WriteLine(limit);

            return limit; 
        }
    }
}
