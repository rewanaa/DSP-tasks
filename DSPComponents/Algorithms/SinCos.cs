using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos : Algorithm
    {
        double oo;
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public Signal OutputSignal { get; set; }
        public override void Run()
        {
            List<float> arr = new List<float>();
            OutputSignal = new Signal(arr, false);
            if (type == "sin")
            {
                for (int i = 0; i < samples.Count; i++)
                {
                    oo = A *( Math.Sin((2 * 180*(AnalogFrequency/SamplingFrequency)*i)+PhaseShift));
                    OutputSignal.Samples.Add(Convert.ToSingle(oo));

                }
                
            }
            else
            {
                for (int i = 0; i < samples.Count; i++)
                {
                    oo = A *( Math.Cos((2 * 180 * (AnalogFrequency / SamplingFrequency) * i) + PhaseShift));
                    OutputSignal.Samples.Add(Convert.ToSingle(oo));
                   
                }
            }
        }
    }
}
