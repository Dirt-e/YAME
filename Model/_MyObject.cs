using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YAME.Model
{
    public class MyObject : INotifyPropertyChanged
    {
        protected Engine engine;

        //public MyObject()
        //{
        //    var mw = Application.Current.MainWindow as MainWindow;
        //    engine = mw.engine;
        //}

        //INotifyPropertyChanged:
        public event PropertyChangedEventHandler PropertyChanged;
        private protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
