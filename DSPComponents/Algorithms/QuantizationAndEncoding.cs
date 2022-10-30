using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }
        public List<float> quan_signal { get; set; }
        public List<float> midpoint { get; set; }
        public List<float> End { get; set; }

        public override void Run()
        {
            quan_signal = new List<float>();
            midpoint = new List<float>();
            End = new List<float>();
            OutputSamplesError = new List<float>();
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            float delta;
            float midpoints;
            float min = InputSignal.Samples.Min();
            if (InputLevel == 0)
            {
                InputLevel = (int)Math.Pow(2, InputNumBits);
            }
            else
            {
                InputNumBits = (int)Math.Log(InputLevel, 2);
            }

            delta = ((InputSignal.Samples.Max()) - ((InputSignal.Samples.Min()))) / InputLevel;
            End.Add((InputSignal.Samples.Min()));
            while((InputSignal.Samples.Min()<= (InputSignal.Samples.Max()))){
                End.Add(delta + (InputSignal.Samples.Min()));
                min = min + delta;
            }

           for(int i=0; i < InputLevel; i++)
            {
                midpoints = (End[i] + End[i + 1]) / 2;
                midpoint.Add(midpoints);
            }


           for(int i = 0; i < InputSignal.Samples.Count; i++)
            {
                for(int j=0; j < End.Count; j++)
                {
                    if (InputSignal.Samples[i] >= End[j] && InputSignal.Samples[i] <= End[j + 1])
                    {
                        quan_signal.Add(midpoint[j]);
                        OutputEncodedSignal.Add(Convert.ToString(j, 2).PadLeft(InputNumBits, '0'));
                        OutputIntervalIndices.Add(j + 1);
                    }
                }
            }
            OutputQuantizedSignal = new Signal(quan_signal, false);
            for(int i=0; i < InputSignal.Samples.Count; i++)
            {
                float error = quan_signal[i] - InputSignal.Samples[i];
                OutputSamplesError.Add(error);
            }
        }
    }
}
