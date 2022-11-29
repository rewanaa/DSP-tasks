using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            List<float> outpustSignal = new List<float>();
            int outputSize = InputSignal.Samples.Count - InputWindowSize + 1;
            float sum = 0;
            for (int i = 0; i < outputSize; i++)
            {

                for (int j = InputWindowSize - 1; j >=0; j--)
                {
                    sum += InputSignal.Samples[j + i];
                }
                outpustSignal.Add(sum / InputWindowSize);
                sum = 0;
            }

            OutputAverageSignal = new Signal(outpustSignal, false);
        }
    }
}
