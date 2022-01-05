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

namespace YAME.View
{
    public partial class FiltersWindow : Window
    {
        public FiltersWindow()
        {
            InitializeComponent();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            var engine = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().engine;

            #region Inverters are bound to the ViewModel
            chkbx_invert_Wx.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Wy.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Wz.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Ax.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Ay.DataContext = engine.VM_FiltersWindow;
            chkbx_invert_Az.DataContext = engine.VM_FiltersWindow;
            #endregion

            #region Filters are bound directly to the Filter-objects 
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
            #endregion

            #region Comperssors are bound directly to the CompressorModules
            cmprbx_Roll_HFC.DataContext     = engine.compressorsystem.CMP_Roll_HFC;
            cmprbx_Yaw_HFC.DataContext      = engine.compressorsystem.CMP_Yaw_HFC;
            cmprbx_Pitch_HFC.DataContext    = engine.compressorsystem.CMP_Pitch_HFC;

            cmprbx_Surge_HFC.DataContext    = engine.compressorsystem.CMP_Surge_HFC;
            cmprbx_Pitch_LFC.DataContext    = engine.compressorsystem.CMP_Pitch_LFC;
            cmprbx_Heave_HFC.DataContext    = engine.compressorsystem.CMP_Heave_HFC;

            cmprbx_Sway_HFC.DataContext     = engine.compressorsystem.CMP_Sway_HFC;
            cmprbx_Roll_LFC.DataContext     = engine.compressorsystem.CMP_Roll_LFC;
            #endregion

            #region Scalers are bound directly to the Scaler objects
            sclr_Roll_HFC.DataContext   = engine.scalersystem.SCL_Roll_HFC;
            sclr_Yaw_HFC.DataContext    = engine.scalersystem.SCL_Yaw_HFC;
            sclr_Pitch_HFC.DataContext  = engine.scalersystem.SCL_Pitch_HFC;

            sclr_Surge_HFC.DataContext  = engine.scalersystem.SCL_Surge_HFC;
            sclr_Pitch_LFC.DataContext  = engine.scalersystem.SCL_Pitch_LFC;

            sclr_Heave_HFC.DataContext  = engine.scalersystem.SCL_Heave_HFC;

            sclr_Sway_HFC.DataContext   = engine.scalersystem.SCL_Sway_HFC;
            sclr_Roll_LFC.DataContext   = engine.scalersystem.SCL_Roll_LFC;
            #endregion

            #region Zeros are bound directly to the ZeroMaker
            chkbx_zero_RollHFC.DataContext = engine.zeromaker;
            chkbx_zero_YawHFC.DataContext = engine.zeromaker;
            chkbx_zero_PitchHFC.DataContext = engine.zeromaker;

            chkbx_zero_SurgeHFC.DataContext = engine.zeromaker;
            chkbx_zero_HeaveHFC.DataContext = engine.zeromaker;
            chkbx_zero_SwayHFC.DataContext = engine.zeromaker;

            chkbx_zero_PitchLFC.DataContext = engine.zeromaker;
            chkbx_zero_RollLFC.DataContext = engine.zeromaker;
            #endregion

            #region Outputs are bound directly to the Zeromaker
            txtblk_output_wx.DataContext        = engine.zeromaker;
            txtblk_output_wy.DataContext        = engine.zeromaker;
            txtblk_output_wz.DataContext        = engine.zeromaker;
            txtblk_output_ax_hfc.DataContext    = engine.zeromaker;
            txtblk_output_ax_lfc.DataContext    = engine.zeromaker;
            txtblk_output_ay_hfc.DataContext    = engine.zeromaker;
            txtblk_output_az_hfc.DataContext    = engine.zeromaker;
            txtblk_output_az_lfc.DataContext    = engine.zeromaker;
            #endregion

        }
        //---------- Buttons ----------
        private void btn_isol_wx_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = false;
            chkbx_zero_YawHFC.IsChecked = true;
            chkbx_zero_PitchHFC.IsChecked = true;
            chkbx_zero_SurgeHFC.IsChecked = true;
            chkbx_zero_PitchLFC.IsChecked = true;
            chkbx_zero_HeaveHFC.IsChecked = true;
            chkbx_zero_SwayHFC.IsChecked = true;
            chkbx_zero_RollLFC.IsChecked = true;
        }
        private void btn_isol_wy_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = true;
            chkbx_zero_YawHFC.IsChecked = false;
            chkbx_zero_PitchHFC.IsChecked = true;
            chkbx_zero_SurgeHFC.IsChecked = true;
            chkbx_zero_PitchLFC.IsChecked = true;
            chkbx_zero_HeaveHFC.IsChecked = true;
            chkbx_zero_SwayHFC.IsChecked = true;
            chkbx_zero_RollLFC.IsChecked = true;
        }
        private void btn_isol_wz_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = true;
            chkbx_zero_YawHFC.IsChecked = true;
            chkbx_zero_PitchHFC.IsChecked = false;
            chkbx_zero_SurgeHFC.IsChecked = true;
            chkbx_zero_PitchLFC.IsChecked = true;
            chkbx_zero_HeaveHFC.IsChecked = true;
            chkbx_zero_SwayHFC.IsChecked = true;
            chkbx_zero_RollLFC.IsChecked = true;
        }
        private void btn_isol_ax_hfc_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = true;
            chkbx_zero_YawHFC.IsChecked = true;
            chkbx_zero_PitchHFC.IsChecked = true;
            chkbx_zero_SurgeHFC.IsChecked = false;
            chkbx_zero_PitchLFC.IsChecked = true;
            chkbx_zero_HeaveHFC.IsChecked = true;
            chkbx_zero_SwayHFC.IsChecked = true;
            chkbx_zero_RollLFC.IsChecked = true;
        }
        private void btn_isol_ax_lfc_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = true;
            chkbx_zero_YawHFC.IsChecked = true;
            chkbx_zero_PitchHFC.IsChecked = true;
            chkbx_zero_SurgeHFC.IsChecked = true;
            chkbx_zero_PitchLFC.IsChecked = false;
            chkbx_zero_HeaveHFC.IsChecked = true;
            chkbx_zero_SwayHFC.IsChecked = true;
            chkbx_zero_RollLFC.IsChecked = true;
        }
        private void btn_isol_ay_hfc_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = true;
            chkbx_zero_YawHFC.IsChecked = true;
            chkbx_zero_PitchHFC.IsChecked = true;
            chkbx_zero_SurgeHFC.IsChecked = true;
            chkbx_zero_PitchLFC.IsChecked = true;
            chkbx_zero_HeaveHFC.IsChecked = false;
            chkbx_zero_SwayHFC.IsChecked = true;
            chkbx_zero_RollLFC.IsChecked = true;
        }
        private void btn_isol_az_hfc_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = true;
            chkbx_zero_YawHFC.IsChecked = true;
            chkbx_zero_PitchHFC.IsChecked = true;
            chkbx_zero_SurgeHFC.IsChecked = true;
            chkbx_zero_PitchLFC.IsChecked = true;
            chkbx_zero_HeaveHFC.IsChecked = true;
            chkbx_zero_SwayHFC.IsChecked = false;
            chkbx_zero_RollLFC.IsChecked = true;
        }
        private void btn_isol_az_lfc_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = true;
            chkbx_zero_YawHFC.IsChecked = true;
            chkbx_zero_PitchHFC.IsChecked = true;
            chkbx_zero_SurgeHFC.IsChecked = true;
            chkbx_zero_PitchLFC.IsChecked = true;
            chkbx_zero_HeaveHFC.IsChecked = true;
            chkbx_zero_SwayHFC.IsChecked = true;
            chkbx_zero_RollLFC.IsChecked = false;
        }
        private void btn_AllActive_Click(object sender, RoutedEventArgs e)
        {
            chkbx_zero_RollHFC.IsChecked = false;
            chkbx_zero_YawHFC.IsChecked = false;
            chkbx_zero_PitchHFC.IsChecked = false;
            chkbx_zero_SurgeHFC.IsChecked = false;
            chkbx_zero_PitchLFC.IsChecked = false;
            chkbx_zero_HeaveHFC.IsChecked = false;
            chkbx_zero_SwayHFC.IsChecked = false;
            chkbx_zero_RollLFC.IsChecked = false;
        }
        
        //---------- Mouse ----------
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left    = Properties.Settings.Default.Window_Filters_Position_X;
            Top     = Properties.Settings.Default.Window_Filters_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Window_Filters_Position_X = (float)Left;
            Properties.Settings.Default.Window_Filters_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }
    }
}
