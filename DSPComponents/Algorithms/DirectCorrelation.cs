using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            List<float> normalized = new List<float>();
            List<float> NONnormalized = new List<float>();

            if (InputSignal2 == null)
            {
                InputSignal2 = InputSignal1;
                Console.WriteLine("Auto");
            }
            int index;
            float sum = 0;
            float norm = 0f;
            float x = 0f;
            float y = 0f;
            for (int z = 0; z < InputSignal1.Samples.Count; z++)
            {
                Console.Write(InputSignal1.Samples[z] + " ");
                x += ((InputSignal1.Samples[z]) * (InputSignal1.Samples[z]));
                y += ((InputSignal2.Samples[z]) * (InputSignal2.Samples[z]));

            }
            Console.WriteLine();
            for (int z = 0; z < InputSignal1.Samples.Count; z++)
            {
                Console.Write(InputSignal2.Samples[z] + " ");

            }
            Console.WriteLine();

            norm += x * y;
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {

                for (int j = 0; j < InputSignal2.Samples.Count; j++)
                {
                    if (InputSignal2.Periodic)
                    {
                        index = j - i;
                        //Console.WriteLine("S1 :" + (index + i) + " S2 : " + ((index + InputSignal2.Samples.Count) % InputSignal2.Samples.Count));
                        sum += InputSignal1.Samples[j] * InputSignal2.Samples[((j + i) % InputSignal2.Samples.Count)];
                        //Console.WriteLine(i + " " + j + " sum " + sum + " " + (index + i) + " " + ((index + InputSignal2.Samples.Count) % InputSignal2.Samples.Count));
                    }
                    else
                    {
                        index = j - i;

                        if (index < 0)
                        {
                            continue;
                        }
                        sum += InputSignal1.Samples[index] * InputSignal2.Samples[index + i];
                        //Console.WriteLine(i + " " + j + " sum " + sum + " " + (index + i) + " " + ((index + InputSignal2.Samples.Count) % InputSignal2.Samples.Count));
                    }
                }
                sum = sum / InputSignal1.Samples.Count;
                NONnormalized.Add(sum);
                normalized.Add(sum / (((float)Math.Sqrt(norm)) / InputSignal1.Samples.Count));
                sum = 0;
            }
            OutputNonNormalizedCorrelation = NONnormalized;
            OutputNormalizedCorrelation = normalized;
            

        }
    }
}               