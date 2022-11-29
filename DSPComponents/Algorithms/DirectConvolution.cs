using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> outputSignal = new List<float>();
            List<int> index = new List<int>();
            int convSignalSize = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            int min = InputSignal1.SamplesIndices[0] + InputSignal2.SamplesIndices[0];
            int max = InputSignal1.SamplesIndices[InputSignal1.SamplesIndices.Count - 1] + InputSignal2.SamplesIndices[InputSignal2.SamplesIndices.Count - 1];
            
            while (min <= max){index.Add(min++);}
            for (int i = 0; i < convSignalSize; i++)
            {
                float Signal, Musk, sum = 0;

                for (int j = 0; j <= i; j++)
                {
                    if (j >= InputSignal1.Samples.Count)
                    {
                        continue;
                    }
                    else
                    {
                        Signal = InputSignal1.Samples[j];
                    }
                    if (i - j >= InputSignal2.Samples.Count)
                    {
                        continue;
                    }
                    else
                    {
                        Musk = InputSignal2.Samples[i - j];
                    }

                    sum += Signal * Musk;
                }
                outputSignal.Add(sum);
            }
            int zeros = 0;
            for (int i = 0; i < outputSignal.Count; i++)
            {
                if (outputSignal[i] != 0)
                {
                    break;
                }
                else { 
                    zeros++;
                }
            }
            for (int i = 0; i < zeros; i++) {
                outputSignal.RemoveAt(0);
                index.RemoveAt(0);
            }
            zeros = 0;
            for (int i = outputSignal.Count - 1; i >=0; i--)
            {
                if (outputSignal[i] != 0)
                {
                    break;
                }
                else
                {
                    zeros++;
                }
            }
            for (int i = 0; i < zeros; i++)
            {
                outputSignal.RemoveAt(outputSignal.Count - 1);
                index.RemoveAt(index.Count - 1);
            }




           

            OutputConvolvedSignal = new Signal(outputSignal, index, false);
        }
    }
}
