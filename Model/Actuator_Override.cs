using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAME.DataFomats;

namespace YAME.Model
{
    public class Actuator_Override : MyObject
    {
        public SixSisters Input         = new SixSisters();
        public SixSisters Output        = new SixSisters();
        public SixSisters Slider_LP3    = new SixSisters();

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

        public void Process(SixSisters ss)
        {
            Input = new SixSisters(ss);

            Update_LP_Filters();
            lerp.Update();

            DrawBlueBars();

            Output =    Slider_LP3 * lerp.Ratio_external + 
                        Input * (1 - lerp.Ratio_external);

            if (!IsOverride)
            {
                syncSliders();
            }
        }

        public void syncSliders()
        {
            Sldr_Act1 = Input.values[0];
            Sldr_Act2 = Input.values[1];
            Sldr_Act3 = Input.values[2];
            Sldr_Act4 = Input.values[3];
            Sldr_Act5 = Input.values[4];
            Sldr_Act6 = Input.values[5];

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
            Slider_LP3.values[0] = LP_sldr_Act1.OutValue;
            Slider_LP3.values[1] = LP_sldr_Act2.OutValue;
            Slider_LP3.values[2] = LP_sldr_Act3.OutValue;
            Slider_LP3.values[3] = LP_sldr_Act4.OutValue;
            Slider_LP3.values[4] = LP_sldr_Act5.OutValue;
            Slider_LP3.values[5] = LP_sldr_Act6.OutValue;
        }
        void DrawBlueBars()
        {
            SelAct1 = Input.values[0];
            SelAct2 = Input.values[1];
            SelAct3 = Input.values[2];
            SelAct4 = Input.values[3];
            SelAct5 = Input.values[4];
            SelAct6 = Input.values[5];
        }
        
    }
}
