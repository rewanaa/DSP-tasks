using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{

    public class DC_Component: Algorithm
    {
        float mean = 0f;
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> arr = new List<float>();
            OutputSignal = new Signal(arr, false);
            for (int i=0; i<InputSignal.Samples.Count; i++)
            {
                 mean = mean + InputSignal.Samples[i];
            }
            mean = mean / InputSignal.Samples.Count;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float sig = InputSignal.Samples[i] - mean;
                OutputSignal.Samples.Add(sig);
            }
        }
    }
}
