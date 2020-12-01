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

        public MyTransform SomePlaneMyTF = new MyTransform();

        //ViewModel:
        Transform3D _transform = Transform3D.Identity;
        public Transform3D Transform
        {
            get { return _transform; }
            private set { _transform = value; OnPropertyChanged("Transform"); }
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
            dispatcherTimer.Tick += new EventHandler(UpdateViewmodel);
            dispatcherTimer.Start();
        }

        private void UpdateViewmodel(object sender, EventArgs e)
        {
            engine.integrator.World.SetOrientation(yaw, pitch, roll);
            Transform = engine.integrator.World.Transform;
        }
    }
}
