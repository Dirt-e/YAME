using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static Utility;

namespace MOTUS.Model
{
    public class IK_Module
    {
        Integrator integrator;

        public float[] Lengths;

        Point3D[] LP = new Point3D[6];
        Point3D[] UP = new Point3D[6];

        public IK_Module(ref Integrator i)
        {
            integrator = i;
            Lengths = new float[6];
        }
        public void Update()
        {
            DetermineActuatorLengths();
        }

        private void DetermineActuatorLengths()
        {
            //Convert P1-P6 into Point3Ds
            LP[0] = PointFrom(integrator.LowerPoints.P1.GetWorldTransform());
            LP[1] = PointFrom(integrator.LowerPoints.P2.GetWorldTransform());
            LP[2] = PointFrom(integrator.LowerPoints.P3.GetWorldTransform());
            LP[3] = PointFrom(integrator.LowerPoints.P4.GetWorldTransform());
            LP[4] = PointFrom(integrator.LowerPoints.P5.GetWorldTransform());
            LP[5] = PointFrom(integrator.LowerPoints.P6.GetWorldTransform());

            UP[0] = PointFrom(integrator.UpperPoints.P1.GetWorldTransform());
            UP[1] = PointFrom(integrator.UpperPoints.P2.GetWorldTransform());
            UP[2] = PointFrom(integrator.UpperPoints.P3.GetWorldTransform());
            UP[3] = PointFrom(integrator.UpperPoints.P4.GetWorldTransform());
            UP[4] = PointFrom(integrator.UpperPoints.P5.GetWorldTransform());
            UP[5] = PointFrom(integrator.UpperPoints.P6.GetWorldTransform());

            //...and then determie the distances between them:
            for (int i = 0; i < 6; i++)
            {
                Lengths[i] = DistanceBetween(LP[i], UP[i]);
            }
        }
    }
}
