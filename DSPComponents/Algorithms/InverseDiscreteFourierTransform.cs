using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            List<float> sample = new List<float>();
            for (int i = 0; i < InputFreqDomainSignal.Frequencies.Count; i++)
            {
                Console.WriteLine(InputFreqDomainSignal.FrequenciesAmplitudes[i] + " freq " + InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
            }
            Console.WriteLine();
            for (int k = 0; k < InputFreqDomainSignal.Frequencies.Count; k++)
            {
                float real = 0.0f;
                for (int n = 0; n < InputFreqDomainSignal.Frequencies.Count; n++)
                {
                    Double e = (k * 2 * Math.PI * n) / InputFreqDomainSignal.FrequenciesAmplitudes.Count;
                    real += InputFreqDomainSignal.FrequenciesAmplitudes[n] * (float)Math.Cos(e + InputFreqDomainSignal.FrequenciesPhaseShifts[n]);
                    //                    Console.Write(real + " ");
                }
                Console.Write(real + " ");
                real = real / InputFreqDomainSignal.Frequencies.Count;
                Console.WriteLine(real);
                sample.Add(real);
                OutputTimeDomainSignal = new Signal(sample, false);
                OutputTimeDomainSignal.Samples = sample;
            }

        }
    }
}
