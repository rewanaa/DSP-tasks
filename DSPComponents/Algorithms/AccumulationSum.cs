using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        List<float> outputSignal;
        public override void Run()
        {
            outputSignal = new List<float>();
            float sum;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                sum = 0;

                for (int j = 0; j <=i; j++)
                {
                    sum += InputSignal.Samples[j];
                }
                outputSignal.Add(sum);
            }
            OutputSignal = new Signal(outputSignal, false);
        }
    }
}
