using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YAME.DataFomats;

namespace YAME.Model
{
    public class Actuator_Override : MyObject
    {
        public SixSisters Input = new SixSisters();
        public SixSisters Output = new SixSisters();
        public SixSisters Prev_Output = new SixSisters();
        public SixSisters Slider_LP3 = new SixSisters();

        

        LowPassNthOrder LP_sldr_Act1 = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_Act2 = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_Act3 = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_Act4 = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_Act5 = new LowPassNthOrder(3);
        LowPassNthOrder LP_sldr_Act6 = new LowPassNthOrder(3);

        Lerp lerp = new Lerp(2000);

        #region ViewModel General
        bool _isOverride;
        public bool IsOverride
        {
            get { return _isOverride; }
            set
            {
                _isOverride = value;
                OnPropertyChanged(nameof(IsOverride));
                lerp.Run(value);
            }
        }
        #endregion
        #region ViewModel SliderValuesRaw
        float _sldr_act1;
        public float Sldr_Act1
        {
            get { return _sldr_act1; }
            set { _sldr_act1 = value; OnPropertyChanged(nameof(Sldr_Act1)); }
        }
        float _sldr_act2;
        public float Sldr_Act2
        {
            get { return _sldr_act2; }
            set { _sldr_act2 = value; OnPropertyChanged(nameof(Sldr_Act2)); }
        }
        float _sldr_act3;
        public float Sldr_Act3
        {
            get { return _sldr_act3; }
            set { _sldr_act3 = value; OnPropertyChanged(nameof(Sldr_Act3)); }
        }

        float _sldr_act4;
        public float Sldr_Act4
        {
            get { return _sldr_act4; }
            set { _sldr_act4 = value; OnPropertyChanged(nameof(Sldr_Act4)); }
        }
        float _sldr_act5;
        public float Sldr_Act5
        {
            get { return _sldr_act5; }
            set { _sldr_act5 = value; OnPropertyChanged(nameof(Sldr_Act5)); }
        }

        float _sldr_act6;
        public float Sldr_Act6
        {
            get { return _sldr_act6; }
            set { _sldr_act6 = value; OnPropertyChanged(nameof(Sldr_Act6)); }
        }
        #endregion
        #region ViewModel SelectionRanges (Blue Bars)
        float _sel_act1;
        public float SelAct1
        {
            get { return _sel_act1; }
            set {  _sel_act1 = value; OnPropertyChanged(nameof(SelAct1)); }
        }
        float _sel_act2;
        public float SelAct2
        {
            get { return _sel_act2; }
            set { _sel_act2 = value; OnPropertyChanged(nameof(SelAct2)); }
        }
        float _sel_act3;
        public float SelAct3
        {
            get { return _sel_act3; }
            set { _sel_act3 = value; OnPropertyChanged(nameof(SelAct3)); }
        }

        float _sel_act4;
        public float SelAct4
        {
            get { return _sel_act4; }
            set { _sel_act4 = value;  OnPropertyChanged(nameof(SelAct4));  }
        }
        float _sel_act5;
        public float SelAct5
        {
            get { return _sel_act5; }
            set { _sel_act5 = value;  OnPropertyChanged(nameof(SelAct5)); }
        }

        float _sel_act6;
        public float SelAct6
        {
            get { return _sel_act6; }
            set { _sel_act6 = value; OnPropertyChanged(nameof(SelAct6)); }
        }
        #endregion
        #region Viewmodel Speeds
        float _spd_act1;
        public float Spd_Act1
        {
            get { return _spd_act1; }
            set { _spd_act1 = value; OnPropertyChanged(nameof(Spd_Act1)); }
        }
        float _spd_act2;
        public float Spd_Act2
        {
            get { return _spd_act2; }
            set { _spd_act2 = value; OnPropertyChanged(nameof(Spd_Act2)); }
        }
        float _spd_act3;
        public float Spd_Act3
        {
            get { return _spd_act3; }
            set { _spd_act3 = value; OnPropertyChanged(nameof(Spd_Act3)); }
        }
        float _spd_act4;
        public float Spd_Act4
        {
            get { return _spd_act4; }
            set { _spd_act4 = value; OnPropertyChanged(nameof(Spd_Act4)); }
        }
        float _spd_act5;
        public float Spd_Act5
        {
            get { return _spd_act5; }
            set { _spd_act5 = value; OnPropertyChanged(nameof(Spd_Act5)); }
        }
        float _spd_act6;
        public float Spd_Act6
        {
            get { return _spd_act6; }
            set { _spd_act6 = value; OnPropertyChanged(nameof(Spd_Act6)); }
        }
        #endregion

        public Actuator_Override(Engine e)
        {
            engine = e;
        }

        public void Process(SixSisters ss)
        {
            Input = new SixSisters(ss);

            Update_LP_Filters();
            lerp.Update();

            DrawBlueBars();

            Output =    Slider_LP3 * lerp.Ratio_external + 
                        Input * (1 - lerp.Ratio_external);

            DetermineSpeed();

            if (!IsOverride) syncSliders();
        }

        private void DetermineSpeed()
        {
            float deltatime = engine.frametimer.DeltaTime;
            float stroke = engine.actuatorsystem.Stroke;

            if (deltatime > 0)
            {
                Spd_Act1 = stroke * 1000 * (Output.Values[0] - Prev_Output.Values[0]) / deltatime;      //Factor of 1000 to convert to "units per second" instead of milliseconds
                Spd_Act2 = stroke * 1000 * (Output.Values[1] - Prev_Output.Values[1]) / deltatime;
                Spd_Act3 = stroke * 1000 * (Output.Values[2] - Prev_Output.Values[2]) / deltatime;
                Spd_Act4 = stroke * 1000 * (Output.Values[3] - Prev_Output.Values[3]) / deltatime;
                Spd_Act5 = stroke * 1000 * (Output.Values[4] - Prev_Output.Values[4]) / deltatime;
                Spd_Act6 = stroke * 1000 * (Output.Values[5] - Prev_Output.Values[5]) / deltatime;
            }
            Prev_Output = Output;                                                                       //For next time
        }

        void Update_LP_Filters()
        {
            //Push values in:
            LP_sldr_Act1.Push(Sldr_Act1);
            LP_sldr_Act2.Push(Sldr_Act2);
            LP_sldr_Act3.Push(Sldr_Act3);
            LP_sldr_Act4.Push(Sldr_Act4);
            LP_sldr_Act5.Push(Sldr_Act5);
            LP_sldr_Act6.Push(Sldr_Act6);

            //...and take them back out:
            Slider_LP3.Values[0] = LP_sldr_Act1.OutValue;
            Slider_LP3.Values[1] = LP_sldr_Act2.OutValue;
            Slider_LP3.Values[2] = LP_sldr_Act3.OutValue;
            Slider_LP3.Values[3] = LP_sldr_Act4.OutValue;
            Slider_LP3.Values[4] = LP_sldr_Act5.OutValue;
            Slider_LP3.Values[5] = LP_sldr_Act6.OutValue;
        }
        void DrawBlueBars()
        {
            SelAct1 = Input.Values[0];
            SelAct2 = Input.Values[1];
            SelAct3 = Input.Values[2];
            SelAct4 = Input.Values[3];
            SelAct5 = Input.Values[4];
            SelAct6 = Input.Values[5];
        }
        public void syncSliders()
        {
            Sldr_Act1 = Input.Values[0];
            Sldr_Act2 = Input.Values[1];
            Sldr_Act3 = Input.Values[2];
            Sldr_Act4 = Input.Values[3];
            Sldr_Act5 = Input.Values[4];
            Sldr_Act6 = Input.Values[5];

        }
        
        
        
    }
}
