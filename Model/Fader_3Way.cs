using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public class Fader_3Way
    {
        public Transform3D Output { get; set; }

        public Fader Fader_ParkPause = new Fader();
        public Fader Fader_PauseMotion = new Fader();

        public Fader3_State State
        {
            get
            {
                //Steady state:
                if      (Fader_ParkPause.Ratio_external == 0 && Fader_PauseMotion.Ratio_external == 0) { return Fader3_State.Park; }
                else if (Fader_ParkPause.Ratio_external == 1 && Fader_PauseMotion.Ratio_external == 0) { return Fader3_State.Pause; }
                else if (Fader_ParkPause.Ratio_external == 1 && Fader_PauseMotion.Ratio_external == 1) { return Fader3_State.Motion; }

                //All other cases must be Transit states
                else if (Fader_PauseMotion.Ratio_external == 0) { return Fader3_State.Transit_ParkPause; }
                else if (Fader_ParkPause.Ratio_external == 1) { return Fader3_State.Transit_PauseMotion; }

                //This should never happen:
                else { throw new Exception(); }
            }
        }

        Fader3_Command _command;
        public Fader3_Command Command
        {
            get { return _command; }
            set
            {
                if (value != _command)
                {
                    _command = value;
                    switch (value)
                    {
                        case Fader3_Command.Park:
                            if (State == Fader3_State.Pause) { Fader_ParkPause.Run(); }
                            break;

                        case Fader3_Command.Pause:
                            if (State == Fader3_State.Park) { Fader_ParkPause.Run(); }
                            else if (State == Fader3_State.Motion) { Fader_PauseMotion.Run(); }
                            break;

                        case Fader3_Command.Motion:
                            if (State == Fader3_State.Pause) { Fader_PauseMotion.Run(); }
                            break;

                        default:
                            throw new Exception();
                    }
                }

            }
        }

        public Fader_3Way()
        {
            Fader_ParkPause.Duration = TimeSpan.FromSeconds(5);
            Fader_PauseMotion.Duration = TimeSpan.FromSeconds(5);
            SetToA();
        }
        public Fader_3Way(TimeSpan time_AB, TimeSpan time_BC)
        {
            Fader_ParkPause.Duration = time_AB;
            Fader_PauseMotion.Duration = time_BC;
            SetToA();
        }

        public void SetToA()
        {
            Fader_ParkPause.ResetTo_Zero();
            Fader_PauseMotion.ResetTo_Zero();
        }
        public void SetToB()
        {
            Fader_ParkPause.ResetTo_One();
            Fader_PauseMotion.ResetTo_Zero();
        }
        public void SetToC()
        {
            Fader_ParkPause.ResetTo_One();
            Fader_PauseMotion.ResetTo_One();
        }

        public void Update(Transform3D TF1, Transform3D TF2, Transform3D TF3)
        {
            Fader_ParkPause.Update();
            Fader_PauseMotion.Update();

            Output = CreateInperpolation(TF1, TF2, TF3);
        }

        public Transform3D CreateInperpolation(Transform3D tf1, Transform3D tf2, Transform3D tf3)
        {                                               //Park              Pause           Motion
            Transform3D TF_AB = Utility.Lerp(tf1, tf2, Fader_ParkPause.Ratio_external);
            Transform3D TF_ABC = Utility.Lerp(TF_AB, tf3, Fader_PauseMotion.Ratio_external);

            return TF_ABC;
        }
    }
   
    public enum Fader3_State
    {
        Park,
        Pause,
        Motion,
        Transit,
        Transit_ParkPause,
        Transit_PauseMotion
    }
    public enum Fader3_Command
    {
        Park,
        Pause,
        Motion
    }
}
