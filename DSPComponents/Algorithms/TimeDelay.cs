using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            List<float> NONnormalized = new List<float>();

            if (InputSignal2 == null)
            {
                InputSignal2 = InputSignal1;
                Console.WriteLine("Auto");
            }
            int index;
            float sum = 0;
            float max = -1;
            int itr = -1;
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {

                for (int j = 0; j < InputSignal2.Samples.Count; j++)
                {
                    if (InputSignal2.Periodic)
                    {
                        index = j - i;
                        sum += InputSignal1.Samples[j] * InputSignal2.Samples[((j + i) % InputSignal2.Samples.Count)];
                    }
                    else
                    {
                        index = j - i;

                        if (index < 0)
                        {
                            continue;
                        }
                        sum += InputSignal1.Samples[index] * InputSignal2.Samples[index + i];
                    }
                }
                sum = sum / InputSignal1.Samples.Count;
                if (sum > max)
                {
                    max = sum;
                    itr = i;
                }
                sum = 0;
            }

            Console.WriteLine("max itr " + itr + " value " + max);
            OutputTimeDelay = itr * (InputSamplingPeriod / InputSignal1.Samples.Count);
            Console.WriteLine("time delat" + OutputTimeDelay);

        }
    }
}
