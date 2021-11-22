using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MOTUS.Model
{
    public struct Platforms_Struct
    {
        public Matrix3D Mx_Plat_Fix_Base;
        public Matrix3D Mx_Plat_Fix_Park;
        public Matrix3D Mx_Plat_Fix_Pause;
        public Matrix3D Mx_Plat_CoR;
        public Matrix3D Mx_Plat_LFC;
        public Matrix3D Mx_Plat_HFC;
        public Matrix3D Mx_Plat_Motion;
        public Matrix3D Mx_Plat_Float_Physical;
    }
}
