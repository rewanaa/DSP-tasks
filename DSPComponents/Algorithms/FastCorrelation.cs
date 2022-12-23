using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            List<float> first_signal = new List<float>();
            List<float> second_signal = new List<float>();
            List<float> first_freq = new List<float>();
            List<float> second_freq = new List<float>();
            List<float> first_phase = new List<float>();
            List<float> second_phase = new List<float>();
            List<float> freq = new List<float>();
            List<float> real = new List<float>();
            List<float> imagin = new List<float>();
            List<float> nonnormalized = new List<float>();
            List<float> normalized = new List<float>();
            float x = 0f;
            float y = 0f;
            float norm = 0f;
           
            if (InputSignal2 == null)
            {
                InputSignal2 = InputSignal1;
                
            }
            if (InputSignal1.Samples.Count == InputSignal2.Samples.Count)
            {
                first_signal = InputSignal1.Samples;
                second_signal = InputSignal2.Samples;

            }
            else
            {
                for (int i = 0; i < InputSignal1.Samples.Count() + InputSignal2.Samples.Count() - 1; i++)
                {
                    if (i < InputSignal1.Samples.Count)
                        first_signal.Add(InputSignal1.Samples[i]);
                    else first_signal.Add(0);
                    if (i < InputSignal2.Samples.Count)
                        second_signal.Add(InputSignal2.Samples[i]);
                    else second_signal.Add(0);
                }
            }
            Signal sig1 = new Signal(first_signal, false);
            Signal sig2 = new Signal(second_signal, false);

            DiscreteFourierTransform dft = new DiscreteFourierTransform();
            dft.InputTimeDomainSignal = sig1;
            dft.Run();
            DiscreteFourierTransform dft2 = new DiscreteFourierTransform();
            dft2.InputTimeDomainSignal = sig2;
            dft2.Run();
            first_freq = dft.OutputFreqDomainSignal.FrequenciesAmplitudes;
            second_freq = dft2.OutputFreqDomainSignal.FrequenciesAmplitudes;
            first_phase = dft.OutputFreqDomainSignal.FrequenciesPhaseShifts;
            second_phase = dft2.OutputFreqDomainSignal.FrequenciesPhaseShifts;

            for (int i = 0; i < first_freq.Count; i++)
            {
                Complex r = new Complex(0, 0);
                Complex first = new Complex(first_freq[i] * (float)Math.Cos(first_phase[i]), first_freq[i] * ((-1) * (float)Math.Sin(first_phase[i])));
                Complex second = new Complex(second_freq[i] * (float)Math.Cos(second_phase[i]), second_freq[i] * (float)Math.Sin(second_phase[i]));
                r = first * second;
                real.Add((float)Math.Sqrt((Math.Pow(r.Real, 2)) + (Math.Pow(r.Imaginary, 2))));//amplitude
                imagin.Add((float)Math.Atan2(r.Imaginary, r.Real)); //phase  

            }
            for (int j = 0; j < real.Count; j++)
            {
                freq.Add(j);
            }
            InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
            Signal sig = new Signal(true, freq, real, imagin);
            idft.InputFreqDomainSignal = sig;
            idft.Run();
            Signal outt = idft.OutputTimeDomainSignal;
            for(int k=0;k< sig1.Samples.Count; k++)
            {
                nonnormalized.Add(outt.Samples[k] / sig1.Samples.Count);
            }
            for (int z = 0; z < sig1.Samples.Count; z++)
            {
                x += ((sig1.Samples[z]) * (sig1.Samples[z]));
                y += ((sig2.Samples[z]) * (sig2.Samples[z]));

            }
            norm += x * y;
            for (int t = 0; t < sig1.Samples.Count; t++)
            {
                normalized.Add((outt.Samples[t] / (((float)Math.Sqrt(norm)) / sig1.Samples.Count))/sig1.Samples.Count);
            }
            
            OutputNormalizedCorrelation = normalized;
            OutputNonNormalizedCorrelation = nonnormalized;
        }
    }
}