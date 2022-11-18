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

        public override void Run()
        {
            
                for (int j = 0; j < InputSignal.Samples.Count/2; j++)
                {
                float tmp = InputSignal.Samples[j];
                InputSignal.Samples[j] = InputSignal.Samples[InputSignal.Samples.Count - j - 1];
                InputSignal.Samples[InputSignal.Samples.Count - j - 1] = tmp;
                OutputFoldedSignal.Samples[j] = InputSignal.Samples[j] ;
                   
                }
            
        }
    }
}
