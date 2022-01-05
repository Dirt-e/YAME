using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YAME.Model;

namespace YAME.Viewmodel
{
    public class _ViewModel : INotifyPropertyChanged
    {
        public Engine engine;

        //INotifyPropertyChanged:
        public event PropertyChangedEventHandler PropertyChanged;
        private protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
