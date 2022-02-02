using YAME.Model;
using YAME.Viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.ViewModel
{
    public class ViewModel_MainWindow : _ViewModel
    {
        string _tilte_string = "YAME Main Window";
        public string TitleString
        {
            get { return _tilte_string; }
            set { _tilte_string = value; OnPropertyChanged(nameof(TitleString)); }
        }

        public string Version
        {
            get { return Properties.Settings.Default.Version; }
        }

        string _profile = "nil";
        public string Profile
        {
            get { return _profile; }
            set { _profile = value; OnPropertyChanged(nameof(Profile)); }
        }

        string _aircraft = "nil";
        public string Aircraft
        {
            get { return _aircraft; }
            set { _aircraft = value; OnPropertyChanged(nameof(Aircraft)); }
        }

        public ViewModel_MainWindow(Engine e)
        {
            base.engine = e;
        }
    }
}
