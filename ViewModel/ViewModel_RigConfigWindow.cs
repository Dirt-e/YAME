using MOTUS.Model;
using MOTUS.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.ViewModel
{
    public class Viewmodel_RigConfigWindow : _ViewModel
    {
        int _cor_offset;
        public int CoR_Offset_Z
        {
            get { return _cor_offset; }
            set
            {
                _cor_offset = value;
                InformAllRecipients();
                OnPropertyChanged("CoR_Offset_Z");
            }
        }

        public Viewmodel_RigConfigWindow(Engine e)
        {
            base.engine = e;
        }

        private void InformAllRecipients()
        {
            //Changes to CoR_Height must be propagated to TWO platforms! 
            //They will effectively cancel each other out.

            //engine.integrator.Plat_CoR.Offset_Z = CoR_Offset_Z;
            //engine.integrator.Plat_Motion.Offset_Z = -CoR_Offset_Z;
        }
    }
}
