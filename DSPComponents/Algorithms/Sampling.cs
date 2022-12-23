﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }




        public override void Run()
        {
            FIR obj = new FIR();
            obj.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            obj.InputFS = 8000;
            obj.InputStopBandAttenuation = 50;
            obj.InputCutOffFrequency = 1500;
            obj.InputTransitionBand = 500;


            List<float> result = new List<float>();
            if (L == 0 && M == 0)
            {

            }
            else if (L != 0 && M == 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count - 1; i++)
                {
                    result.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L-1; j++)
                    {
                        result.Add(0);
                    }
                }
                result.Add(InputSignal.Samples[InputSignal.Samples.Count - 1]);
                obj.InputTimeDomainSignal = new Signal(result, false);
                obj.Run();

                OutputSignal = new Signal(obj.OutputYn.Samples, false);
            }
            else if (L == 0 && M != 0)
            {

                obj.InputTimeDomainSignal = InputSignal;
                obj.Run();
                Signal output = obj.OutputYn;
                int counter = 0;
                for (int i = 0; i < output.Samples.Count; i++)
                {
                    if (i % (M ) == 0)
                    {
                        //Console.WriteLine(i);
                        result.Add(output.Samples[i]);
                    }
                }
                OutputSignal = new Signal(result, false);
            }
            else {
                for (int i = 0; i < InputSignal.Samples.Count - 1; i++)
                {
                    result.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        result.Add(0);
                    }
                }
                result.Add(InputSignal.Samples[InputSignal.Samples.Count - 1]);
                obj.InputTimeDomainSignal = new Signal(result, false);
                obj.Run();
                result = new List<float>();
                Signal output = obj.OutputYn;
                int counter = 0;
                for (int i = 0; i < output.Samples.Count; i++)
                {
                    if (i % (M) == 0)
                    {
                        result.Add(output.Samples[i]);
                    }
                }
                OutputSignal = new Signal(result, false);

            }
 
        }
    }

}