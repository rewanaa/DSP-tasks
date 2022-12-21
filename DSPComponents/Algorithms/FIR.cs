using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            float hd;
            float wn;
            float N=0.0f;
            int j;
            List<float> h = new List<float>();
            List<float> w = new List<float>();
            List<float> result = new List<float>();
            if (InputFilterType==FILTER_TYPES.LOW)
            {
                InputCutOffFrequency = InputCutOffFrequency + (InputTransitionBand / 2);
               for (int i=0;i <= InputTimeDomainSignal.Samples.Count; i++)
                {
                    if (i == 0)
                    {
                        hd = 2.0f * (float)InputCutOffFrequency;
                    }
                    else
                    {
                        hd = ((2.0f * (float)InputCutOffFrequency) * (((float)Math.Sin(i * (2 * (float)Math.PI * (float)InputCutOffFrequency)))
                            / (i * (2 * (float)Math.PI * (float)InputCutOffFrequency))));
                    }
                    h.Add(hd);
                }

            }
           else if(InputFilterType == FILTER_TYPES.HIGH)
           {
                InputCutOffFrequency = InputCutOffFrequency - (InputTransitionBand / 2);
                for (int i = 0; i <= InputTimeDomainSignal.Samples.Count; i++)
                {
                    if (i == 0)
                    {
                        hd = 1-(2.0f * (float)InputCutOffFrequency);
                    }
                    else
                    {
                        hd = (-1)*((2.0f * (float)InputCutOffFrequency) * (((float)Math.Sin(i * (2 * (float)Math.PI * (float)InputCutOffFrequency)))
                            / (i * (2 * (float)Math.PI * (float)InputCutOffFrequency))));
                    }
                    h.Add(hd);
                }
            }
           else if(InputFilterType == FILTER_TYPES.BAND_PASS)
           {
                InputF1 = InputF1 - (InputTransitionBand / 2);
                InputF2 = InputF2 + (InputTransitionBand / 2);
                for (int i = 0; i <= InputTimeDomainSignal.Samples.Count; i++)
                {
                    if (i == 0)
                    {
                        hd = 2*((float)InputF2 - (float)InputF1);
                    }
                    else
                    {
                        hd =  (((2.0f * (float)InputF2) * (((float)Math.Sin(i * (2 * (float)Math.PI * (float)InputF2))) / (i * (2 * (float)Math.PI * (float)InputF2)))))-
                           (-1) * ((2.0f * (float)InputF1) * (((float)Math.Sin(i * (2 * (float)Math.PI * (float)InputF1))) / (i * (2 * (float)Math.PI * (float)InputF1))));
                    }
                    h.Add(hd);
                }
            }
           else if(InputFilterType==FILTER_TYPES.BAND_STOP)
           {
                InputF1 = InputF1 + (InputTransitionBand / 2);
                InputF2 = InputF2 - (InputTransitionBand / 2);
                for (int i = 0; i <= InputTimeDomainSignal.Samples.Count; i++)
                {
                    if (i == 0)
                    {
                        hd =1-( 2 * ((float)InputF2 - (float)InputF1));
                    }
                    else
                    {
                        hd = (((2.0f * (float)InputF1) * (((float)Math.Sin(i * (2 * (float)Math.PI * (float)InputF1))) / (i * (2 * (float)Math.PI * (float)InputF1))))) -
                           (-1) * ((2.0f * (float)InputF2) * (((float)Math.Sin(i * (2 * (float)Math.PI * (float)InputF2))) / (i * (2 * (float)Math.PI * (float)InputF2))));
                    }
                    h.Add(hd);
                }
            }

           if(InputStopBandAttenuation<=21)
            {
                wn = 1;
                 N = (InputTransitionBand / InputFS) / 0.9f;
                N = (int)Math.Ceiling(N);
                if (N % 2 == 0)
                {
                    N = N + 1;
                }
                 j = (int)Math.Floor(N / 2);
                for (int i = 0; i <= j; i++)
                {
                    w.Add(wn);
                }
            }
           else if(InputStopBandAttenuation>21&& InputStopBandAttenuation<=44)
            {
                N = (InputTransitionBand / InputFS) / 3.1f;
                N = (int)Math.Ceiling(N);
                if (N % 2 == 0)
                {
                    N = N + 1;
                }
                 j = (int)Math.Floor(N/2);
                for (int i = 0; i <= j; i++)
                {
                    wn = 0.5f + (0.5f * (float)Math.Cos((2.0f * (float)Math.PI * i) / N));
                    w.Add(wn);
                }

            }
            else if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53)
            {
                N = (InputTransitionBand / InputFS) / 3.3f;
                N = (int)Math.Ceiling(N);
                if (N % 2 == 0)
                {
                    N = N + 1;
                }
                 j = (int)Math.Floor(N / 2);
                for (int i = 0; i <= j; i++)
                {
                    wn = 0.54f + (0.46f * (float)Math.Cos((2.0f * (float)Math.PI * i) / N));
                    w.Add(wn);
                }
            }
            else if (InputStopBandAttenuation > 53 && InputStopBandAttenuation <= 74)
            {
                N = (InputTransitionBand / InputFS) / 5.5f;
                N = (int)Math.Ceiling(N);
                if (N % 2 == 0)
                {
                    N = N + 1;
                }
                 j = (int)Math.Floor(N / 2);
                for (int i = 0; i <= j; i++)
                {
                    wn = 0.42f + (0.5f * (float)Math.Cos((2.0f * (float)Math.PI * i) / (N-1)))+
                        (0.08f * (float)Math.Cos((4.0f * (float)Math.PI * i) / (N - 1)));
                        w.Add(wn);
                }
            }
             j = (int)Math.Floor(N / 2);
            for (int i = 0; i <= j; i++)
            {
                result[i] = h[i] * w[i];
            }
             OutputHn = new Signal(result,false);
            DirectConvolution d = new DirectConvolution();
            d.InputSignal1 = new Signal(h, false);
            d.InputSignal2 = new Signal(w, false);
            d.Run();
            OutputYn = d.OutputConvolvedSignal;
        }
    }
}
