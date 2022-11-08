using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public int InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            List<float> PhaseShift = new List<float>();
            List<float> amplitude = new List<float>();
            
            for (int k = 0; k < InputTimeDomainSignal.Samples.Count; k++)
            {
                float imagin = 0.0f;
                float real = 0.0f;
                for (int n = 0; n < InputTimeDomainSignal.Samples.Count; n++)
                {

                    Double e = (k * 2 * Math.PI * n) / InputTimeDomainSignal.Samples.Count;

                    real += InputTimeDomainSignal.Samples[n] * (float)Math.Cos(e);
                    imagin += -InputTimeDomainSignal.Samples[n] * (float)Math.Sin(e);
                    
                }

                amplitude.Add((float)Math.Sqrt((Math.Pow(real , 2)) + (Math.Pow(imagin, 2))));
                PhaseShift.Add((float)Math.Atan2(imagin, real)); 
                OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Samples, false);
                OutputFreqDomainSignal = new Signal(amplitude, false);
                OutputFreqDomainSignal = new Signal(PhaseShift, false);
                OutputFreqDomainSignal.FrequenciesAmplitudes = amplitude;
                OutputFreqDomainSignal.FrequenciesPhaseShifts = PhaseShift;
                
            }
        }
    }
}
