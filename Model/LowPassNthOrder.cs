using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOTUS.Model
{
    public class LowPassNthOrder : Filter
    {
        

        private LowPassModule[] LP_Array = new LowPassModule[4];
        
        //Constructor
        public LowPassNthOrder(int order = 1)
        {
            Order = (FilterOrder)order;
            
            for (int i = 0; i < 4; i++)        //initialise all four filter instances
            {
                LP_Array[i] = new LowPassModule();
            }
        }
        
        public override void CreateOutput()
        {
            float temp = InValue;                               
            float adoptionrate = 1 / FilterVariable;            //The LP filter internally uses adoption rate!
            
            for (int i = 0; i < (int)Order; i++)                //Push it through as many LP-filters as the filter order
            {
                LP_Array[i].Push(temp, adoptionrate);          
                temp = LP_Array[i].Output;
            }
            OutValue = temp;
        }
        public void Set(float f)
        {
            foreach (LowPassModule lpm in LP_Array)
            {
                lpm.Set(f);
            }
        }
    }
}
