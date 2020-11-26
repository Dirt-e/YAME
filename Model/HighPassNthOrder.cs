using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class HighPassNthOrder : Filter
    {   
        public LowPassNthOrder LP = new LowPassNthOrder();

        float _filtervariable = 100;
        new public float FilterVariable
        {
            get { return _filtervariable; }
            set 
            {  
                _filtervariable = value;
                if (LP != null) LP.FilterVariable = value;  
            }
        }

        
        //Constructor
        public HighPassNthOrder(int order = 1)
        {
            Order = (FilterOrder)order;
        }

        public override void CreateOutput()
        {
            LP.Push(InValue);
            OutValue = InValue - LP.OutValue;
        }
        public void Set(float f)
        {
            LP.Set(f);
        }
    }
}
