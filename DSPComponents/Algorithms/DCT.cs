using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> dc = new List<float>();
            float r = 0f;
            //Console.Write(InputSignal.Samples.Count);
            for (int k = 0; k <= InputSignal.Samples.Count; k++)
            {
                for (int n = 0; n < InputSignal.Samples.Count; n++)
                {
                    r = InputSignal.Samples[n] * (float)Math.Cos((Math.PI / (4 * InputSignal.Samples.Count)) * ((2 * n) - 1) * ((2 *k) - 1));
                    
                    r += r;
                }
                //Console.Write(r + " ");
                Console.Write(((float)Math.Sqrt(2 / InputSignal.Samples.Count)) * r+"  ");
                dc.Add(((float)Math.Sqrt(2/ InputSignal.Samples.Count))*r);
            }
            OutputSignal = new Signal(dc, false);
        }
    }
}
