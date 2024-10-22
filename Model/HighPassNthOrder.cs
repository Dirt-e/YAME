﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public class HighPassNthOrder : Filter
    {   
        public LowPassNthOrder LP = new LowPassNthOrder();

        new public float FilterVariable
        {
            get { return _filtervariable; }
            set 
            {  
                _filtervariable = value;
                if (LP != null) LP.FilterVariable = value;
                OnPropertyChanged(nameof(FilterVariable));
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
        public void Equalize()
        {
            LP.Equalize();
        }
    }
}
