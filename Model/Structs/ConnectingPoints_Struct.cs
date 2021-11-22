using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model.Structs
{
    public struct ConnectingPoints_Struct
    {
        public Matrix3D Mx_UpperPoints_1;
        public Matrix3D Mx_UpperPoints_2;
        public Matrix3D Mx_UpperPoints_3;
        public Matrix3D Mx_UpperPoints_4;
        public Matrix3D Mx_UpperPoints_5;
        public Matrix3D Mx_UpperPoints_6;

        public Matrix3D Mx_LowerPoints_1;
        public Matrix3D Mx_LowerPoints_2;
        public Matrix3D Mx_LowerPoints_3;
        public Matrix3D Mx_LowerPoints_4;
        public Matrix3D Mx_LowerPoints_5;
        public Matrix3D Mx_LowerPoints_6;
    }
}
