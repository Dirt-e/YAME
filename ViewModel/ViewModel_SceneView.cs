using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MOTUS.ViewModel
{
    public class ViewModel_Sceneview : _ViewModel
    {
        #region Platforms
        Transform3D _plat_fix_base;
        public Transform3D PlatFixBase
        {
            get { return _plat_fix_base; }
            set { _plat_fix_base = value; OnPropertyChanged("PlatFixBase"); }
        }
        Transform3D _plat_fix_park;
        public Transform3D PlatFixPark
        {
            get { return _plat_fix_park; }
            set { _plat_fix_park = value; OnPropertyChanged("PlatFixPark"); }   //Minimum extension
        }
        Transform3D _plat_fix_pause;
        public Transform3D PlatFixPause
        {
            get { return _plat_fix_pause; }
            set { _plat_fix_pause = value; OnPropertyChanged("PlatFixPause"); } //50% extension
        }
        Transform3D _plat_cor;
        public Transform3D PlatCoR
        {
            get { return _plat_cor; }
            set { _plat_cor = value; OnPropertyChanged("PlatCoR"); }
        }
        Transform3D _plat_lfc;
        public Transform3D PlatLFC
        {
            get { return _plat_lfc; }
            set { _plat_lfc = value; OnPropertyChanged("PlatLFC"); }
        }
        Transform3D _plat_hfc;
        public Transform3D PlatHFC
        {
            get { return _plat_hfc; }
            set { _plat_hfc = value; OnPropertyChanged("PlatHFC"); }
        }
        Transform3D _plat_motion;
        public Transform3D PlatMotion
        {
            get { return _plat_motion; }
            set { _plat_motion = value; OnPropertyChanged("PlatMotion"); }
        }
        Transform3D _plat_float_physical;
        public Transform3D PlatFloatPhysical
        {
            get { return _plat_float_physical; }
            set { _plat_float_physical = value; OnPropertyChanged("PlatFloatPhysical"); }
        }
        #endregion

        #region SPHERES (BASE)
        Point3D _lower1;
        public Point3D Lower1
        {
            get { return _lower1; }
            set { _lower1 = value; OnPropertyChanged("Lower1"); }
        }
        Point3D _lower2;
        public Point3D Lower2
        {
            get { return _lower2; }
            set { _lower2 = value; OnPropertyChanged("Lower2"); }
        }
        Point3D _lower3;
        public Point3D Lower3
        {
            get { return _lower3; }
            set { _lower3 = value; OnPropertyChanged("Lower3"); }
        }
        Point3D _lower4;
        public Point3D Lower4
        {
            get { return _lower4; }
            set { _lower4 = value; OnPropertyChanged("Lower4"); }
        }
        Point3D _lower5;
        public Point3D Lower5
        {
            get { return _lower5; }
            set { _lower5 = value; OnPropertyChanged("Lower5"); }
        }
        Point3D _lower6;
        public Point3D Lower6
        {
            get { return _lower6; }
            set { _lower6 = value; OnPropertyChanged("Lower6"); }
        }
        #endregion

        #region SPHERES (UPPER)
        Point3D _Upper1;
        public Point3D Upper1
        {
            get { return _Upper1; }
            set { _Upper1 = value; OnPropertyChanged("Upper1"); }
        }
        Point3D _Upper2;
        public Point3D Upper2
        {
            get { return _Upper2; }
            set { _Upper2 = value; OnPropertyChanged("Upper2"); }
        }
        Point3D _Upper3;
        public Point3D Upper3
        {
            get { return _Upper3; }
            set { _Upper3 = value; OnPropertyChanged("Upper3"); }
        }
        Point3D _Upper4;
        public Point3D Upper4
        {
            get { return _Upper4; }
            set { _Upper4 = value; OnPropertyChanged("Upper4"); }
        }
        Point3D _Upper5;
        public Point3D Upper5
        {
            get { return _Upper5; }
            set { _Upper5 = value; OnPropertyChanged("Upper5"); }
        }
        Point3D _Upper6;
        public Point3D Upper6
        {
            get { return _Upper6; }
            set { _Upper6 = value; OnPropertyChanged("Upper6"); }
        }
        #endregion

        #region Actuator Colors
        //These actuator colors are being updated via a Dispatcher callback.
        //The object making this call is the "engine.actuatorsystem" object.
        SolidColorBrush act1_brush = new SolidColorBrush(Colors.White);
        public SolidColorBrush Act1_Brush
        {
            get { return act1_brush; }
            set { act1_brush = value; OnPropertyChanged(nameof(Act1_Brush)); }
        }
        SolidColorBrush act2_brush = new SolidColorBrush(Colors.White);
        public SolidColorBrush Act2_Brush
        {
            get { return act2_brush; }
            set { act2_brush = value; OnPropertyChanged(nameof(Act2_Brush)); }
        }
        SolidColorBrush act3_brush = new SolidColorBrush(Colors.White);
        public SolidColorBrush Act3_Brush
        {
            get { return act3_brush; }
            set { act3_brush = value; OnPropertyChanged(nameof(Act3_Brush)); }
        }
        SolidColorBrush act4_brush = new SolidColorBrush(Colors.White);
        public SolidColorBrush Act4_Brush
        {
            get { return act4_brush; }
            set { act4_brush = value; OnPropertyChanged(nameof(Act4_Brush)); }
        }
        SolidColorBrush act5_brush = new SolidColorBrush(Colors.White);
        public SolidColorBrush Act5_Brush
        {
            get { return act5_brush; }
            set { act5_brush = value; OnPropertyChanged(nameof(Act5_Brush)); }
        }
        SolidColorBrush act6_brush = new SolidColorBrush(Colors.White);
        public SolidColorBrush Act6_Brush
        {
            get { return act6_brush; }
            set { act6_brush = value; OnPropertyChanged(nameof(Act6_Brush)); }
        }

        #endregion

        public ViewModel_Sceneview(Engine e)
        {
            base.engine = e;
        }
    }
}
