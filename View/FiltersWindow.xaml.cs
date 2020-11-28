using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MOTUS.View
{
    public partial class FiltersWindow : Window
    {
        //Inverter inverter = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().preprocessor.inverter;
        //FilterSystem filtersystem = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().mainprocessor.filtersystem;
        //CompressorSystem compressorsystem = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().mainprocessor.compressorsystem;
        //ScalerSystem scalersystem = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().mainprocessor.scalersystem;
        //ZeroMaker zeromaker = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().mainprocessor.zeromaker;


        public FiltersWindow()
        {
            InitializeComponent();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;

            //Checkboxes to invert input signal
            chkbx_invert_Wx.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Wy.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Wz.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Ax.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Ay.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Az.DataContext = engine.VM_FiltersWindow;

            //Filters get their DataContext set directly to the Filter-objects 
            filterbox_wx_HP.DataContext     = engine.filtersystem.Wx_HP;
            filterbox_wx_HP_LP.DataContext  = engine.filtersystem.Wx_HP_LP;
            filterbox_wy_HP.DataContext     = engine.filtersystem.Wy_HP;
            filterbox_wy_HP_LP.DataContext  = engine.filtersystem.Wy_HP_LP;
            filterbox_wz_HP.DataContext     = engine.filtersystem.Wz_HP;
            filterbox_wz_HP_LP.DataContext  = engine.filtersystem.Wz_HP_LP;

            filterbox_ax_HP.DataContext     = engine.filtersystem.Ax_HP;
            filterbox_ax_HP_LP2.DataContext = engine.filtersystem.Ax_HP_LP2;
            filterbox_ax_LP3.DataContext    = engine.filtersystem.Ax_LP3;

            filterbox_ay_HP.DataContext     = engine.filtersystem.Ay_HP;
            filterbox_ay_HP_LP2.DataContext = engine.filtersystem.Ay_HP_LP2;

            filterbox_az_HP.DataContext     = engine.filtersystem.Az_HP;
            filterbox_az_HP_LP2.DataContext = engine.filtersystem.Az_HP_LP2;
            filterbox_az_LP3.DataContext    = engine.filtersystem.Az_LP3;


            ////Compressors
            //cmprbx_Roll_HFC.DataContext = compressorsystem.CMP_Roll_HFC;
            //cmprbx_Yaw_HFC.DataContext = compressorsystem.CMP_Yaw_HFC;
            //cmprbx_Pitch_HFC.DataContext = compressorsystem.CMP_Pitch_HFC;

            //cmprbx_Surge_HFC.DataContext = compressorsystem.CMP_Surge_HFC;
            //cmprbx_Pitch_LFC.DataContext = compressorsystem.CMP_Pitch_LFC;
            //cmprbx_Heave_HFC.DataContext = compressorsystem.CMP_Heave_HFC;

            //cmprbx_Sway_HFC.DataContext = compressorsystem.CMP_Sway_HFC;
            //cmprbx_Roll_LFC.DataContext = compressorsystem.CMP_Roll_LFC;

            ////Scalers
            //sclr_Roll_HFC.DataContext = scalersystem.SCL_Roll_HFC;
            //sclr_Yaw_HFC.DataContext = scalersystem.SCL_Yaw_HFC;
            //sclr_Pitch_HFC.DataContext = scalersystem.SCL_Pitch_HFC;

            //sclr_Surge_HFC.DataContext = scalersystem.SCL_Surge_HFC;
            //sclr_Heave_HFC.DataContext = scalersystem.SCL_Heave_HFC;
            //sclr_Sway_HFC.DataContext = scalersystem.SCL_Sway_HFC;

            //sclr_Pitch_LFC.DataContext = scalersystem.SCL_Pitch_LFC;
            //sclr_Roll_LFC.DataContext = scalersystem.SCL_Roll_LFC;

            ////Zeros
            //chkbx_zero_RollHFC.DataContext = zeromaker;
            //chkbx_zero_YawHFC.DataContext = zeromaker;
            //chkbx_zero_PitchHFC.DataContext = zeromaker;

            //chkbx_zero_SurgeHFC.DataContext = zeromaker;
            //chkbx_zero_HeaveHFC.DataContext = zeromaker;
            //chkbx_zero_SwayHFC.DataContext = zeromaker;

            //chkbx_zero_PitchLFC.DataContext = zeromaker;
            //chkbx_zero_RollLFC.DataContext = zeromaker;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
