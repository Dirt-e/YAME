using MOTUS.DataFomats;
using MOTUS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

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
        public MyTransform Plat_Fix_Pause;
        public MyTransform Plat_CoR;
        public MyTransform Plat_LFC;
        public MyTransform Plat_HFC;
        public MyTransform Plat_Motion;
        public ConnectingPoints LowerPoints;
        public MyTransform Plat_Fix_Park;
        public MyTransform Plat_Float_Physical;
        public ConnectingPoints UpperPoints;
        #endregion

        public Integrator()
        {
            InstantiateObjectsUponConstruction();
        }

        public void Process(DOF_Data data)
        {
            Input = new DOF_Data(data);
            DriveRigGeometry();
        }

        private void InstantiateObjectsUponConstruction()
        {
            World = new MyTransform();
            Plat_Fix_Base = new MyTransform();
            Plat_Fix_Pause = new MyTransform(); //(50% extension position)
            Plat_CoR = new MyTransform();
            Plat_LFC = new MyTransform();
            Plat_HFC = new MyTransform();
            Plat_Motion = new MyTransform();
            LowerPoints = new ConnectingPoints();
            Plat_Fix_Park = new MyTransform();
            Plat_Float_Physical = new MyTransform();
            UpperPoints = new ConnectingPoints();

            Fader_3Way = new Fader_Threeway(    TimeSpan.FromSeconds(fade_duration_ParkToPause_seconds),
                                                TimeSpan.FromSeconds(fade_duration_PauseToMotion_seconds));
        }
        
        private void DriveRigGeometry()
        {
            //Dynamic offsets here only!

            //Static offsets are done via bindings. No need to update every frame :-)
        }
    }
}
