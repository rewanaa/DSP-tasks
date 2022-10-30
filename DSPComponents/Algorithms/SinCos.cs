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
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        //public Signal OutputSignal { get; set; }
        public override void Run() 
        {
            samples = new List<float>();
            float oo;
            if (type == "sin")
            {
                for (int i = 0; i <SamplingFrequency; i++)
                {
                    oo =((float)( A *( Math.Sin((2 * Math.PI*(AnalogFrequency/SamplingFrequency)*i)+PhaseShift))));
                    samples.Add(oo);

                }
                
            }
            else
            {
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    oo = ((float)(A * (Math.Cos((2 * Math.PI * (AnalogFrequency / SamplingFrequency) * i) + PhaseShift))));
                    samples.Add(oo);

                }
            }
            
        }
    }
}
