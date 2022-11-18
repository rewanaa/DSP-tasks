using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            if (ShiftingValue > 0)
            {
                for(int i=ShiftingValue;i<InputSignal.Samples.Count;i++)
                {
                    InputSignal.Samples[i - ShiftingValue] = InputSignal.Samples[i];
                }
            }
            else if(ShiftingValue < 0)
            {
                for (int i = InputSignal.Samples.Count-ShiftingValue-1; i >= 0; i--)
                {
                    InputSignal.Samples[i + ShiftingValue] = InputSignal.Samples[i];
                }

            }
            OutputShiftedSignal = InputSignal;
        }
    }
}
