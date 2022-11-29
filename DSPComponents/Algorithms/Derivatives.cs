using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }
        List<float> d1;
        List<float> d2;
        public override void Run()
        {
            d1 = new List<float>();
            d2 = new List<float>();
            d1.Add(InputSignal.Samples[0]);
            d2.Add(InputSignal.Samples[1] - (2 * InputSignal.Samples[0]));

            for (int i = 1; i < InputSignal.Samples.Count - 1; i++)
            {
                d1.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);
                d2.Add(InputSignal.Samples[i + 1] - (2 * InputSignal.Samples[i]) + InputSignal.Samples[i - 1]);
            }
            FirstDerivative = new Signal(d1, false);
            SecondDerivative = new Signal(d2, false);
        }
    }
}
