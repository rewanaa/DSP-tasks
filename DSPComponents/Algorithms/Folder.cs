using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public List<float> outputSignal { get; set; }
        public List<int> index { get; set; }

        public override void Run()
        {
             outputSignal = new List<float>();
             index = new List<int>();
            for (int i = InputSignal.Samples.Count - 1; i >= 0; i--)
            {
                outputSignal.Add(InputSignal.Samples[i]);
                index.Add(InputSignal.SamplesIndices[i] * -1);
            }

            OutputFoldedSignal = new Signal(outputSignal, index, !InputSignal.Periodic);
        }
    }
}
