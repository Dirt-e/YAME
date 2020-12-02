using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace MOTUS.Model
{
    public class ViewModelSceneView_Provider : MyObject
    {
        DispatcherTimer dispatcherTimer;
        Engine engine;

        //ViewModel:
        MyTransform _transform = new MyTransform();
        public MyTransform SomeTransform
        {
            get { return _transform; }
            set { _transform = value; OnPropertyChanged("Transform"); }
        }

        public float yaw;
        public float pitch;
        public float roll;

        public ViewModelSceneView_Provider(Engine e)
        {
            engine = e;
            StartTimer();
        }

        private void StartTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Tick += new EventHandler(UpdateViewModel_OnTick);
            dispatcherTimer.Start();
        }

        private void UpdateViewModel_OnTick(object sender, EventArgs e)
        {
            SomeTransform.SetOrientation(yaw, pitch, roll);
            
            //yaw += 0.1f;
            //pitch += 0.1f;
            //roll += 0.1f;


        }

        
    }
}
