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
        public FILTER_TYPES InputFilterType { get; set; }       //type of filter
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }        //fc
        public float? InputF1 { get; set; }                     //bandwidth filter
        public float? InputF2 { get; set; }                     //bandwidth filter
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }          //to calculate the f' 
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
       
            //variables
            double hd;
            double wn;
            double deltaf = 0;
            int N =0;
            List<double> h = new List<double>();
            List<double> w = new List<double>();
            List<float> result = new List<float>();
            List<int> index = new List<int>();

            deltaf = InputTransitionBand / InputFS;

//            Console.WriteLine("input : ");
 //           Console.WriteLine("filter type " + InputFilterType);
   //         Console.WriteLine("sampling freq " + InputFS);
     //       Console.WriteLine("Fc " + InputCutOffFrequency);
       //     Console.WriteLine("frequancy " + InputF1 + " to " + InputF2);
         //   Console.WriteLine("StopBand att " + InputStopBandAttenuation);
           // Console.WriteLine("InputTransitionBand "  + InputTransitionBand);
           // Console.WriteLine();

            //caclulate W(n)
            if (InputStopBandAttenuation <= 21) { N = (int)Math.Ceiling(0.9 / deltaf); }
            else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44){  N = (int)Math.Ceiling(3.1 / deltaf);}
            else if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53){N = (int)Math.Ceiling(3.3f / deltaf);}
            else{ N = (int)Math.Ceiling(5.5f / deltaf);}

            if (N % 2 == 0)
            {
                N = N + 1;
            }
            int  itr = (int)Math.Ceiling((float)N / 2);
            if (InputStopBandAttenuation <= 21)
            {
                for (int i = 0; i <itr; i++) 
                {
                    w.Add(1);
                }

            }
            else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
            {
                double x = N;
                //create W(n)
                for (int i = 0; i <itr; i++) 
                {
                    wn = 0.5f + (0.5f * (double)Math.Cos((2.0f * (double)Math.PI * i) / N));
                    w.Add(wn);
                }

            }
            else if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53)
            {
                for (int i = 0; i < itr; i++) 
                {
                    wn = 0.54 + (0.46 * Math.Cos((2.0f *Math.PI * i) / N));
                    w.Add(wn);
                }
            }
            else
            {
                for (int i = 0; i <itr; i++) 
                {
                    wn = 0.42 + (0.5 * Math.Cos((2.0f * Math.PI * i) / (N - 1))) +
                        (0.08f * Math.Cos((4.0f * Math.PI * i) / (N - 1)));
                    w.Add(wn);
                }
            }
            deltaf = InputTransitionBand;//reset it to original value
           // Console.WriteLine("deltaf = " + deltaf);
            //Console.WriteLine("N value " + N);
           // Console.WriteLine("W(n) size: " + w.Count);
            
            //Calculate hd(n)
            if (InputFilterType==FILTER_TYPES.LOW)
            {
                double Fc = ((double)InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS;
                for (int i = 0; i < itr; i++)
                {
                    if (i == 0)
                    {
                        hd = 2.0f * Fc;
                    }
                    else
                    {
                        hd = ((2.0f * Fc) * ((Math.Sin(i * (2 * Math.PI * Fc)))
                            / (i * (2 *Math.PI * Fc))));
                    }
                    h.Add(hd);
                }

            }
           else if(InputFilterType == FILTER_TYPES.HIGH)
           {
                double Fc = ((double)InputCutOffFrequency - deltaf / 2) / InputFS;
                for (int i = 0; i < itr; i++)
                {
                    if (i == 0)
                    {
                        hd = 1-(2.0f * Fc);
                    }
                    else
                    {
                        hd = (-1)*((2.0f * Fc) * ((Math.Sin(i * (2 * Math.PI * Fc)))
                            / (i * (2 * Math.PI * Fc))));
                    }
                    h.Add(hd);
                }
            }
           else if(InputFilterType == FILTER_TYPES.BAND_PASS)
           {
                
                double F1 = ((double)InputF1 - (InputTransitionBand / 2)) / InputFS;
                double F2 = ((double)InputF2 + (InputTransitionBand / 2)) / InputFS;
                for (int i = 0; i <itr; i++)
                {
                    if (i == 0)
                    {
                        hd = 2*(F2 - F1);
                    }
                    else
                    {
                        hd =  (((2 * F2) * (Math.Sin(i * (2 * Math.PI * F2)) / (i * (2 *Math.PI * F2))))) - ((2 * F1) * ((Math.Sin(i * (2 * Math.PI * F1))) / (i * (2 * Math.PI * F1))));
                    }
                    h.Add(hd);
                }
            }
           else if(InputFilterType==FILTER_TYPES.BAND_STOP)
           {
                double F1 = ((double)InputF1 + (InputTransitionBand / 2)) / InputFS;
                double F2 = ((double)InputF2 - (InputTransitionBand / 2)) / InputFS;
                for (int i = 0; i < itr; i++)
                {
                    if (i == 0)
                    {
                        hd =1-( 2 * (F2 - F1));
                    }
                    else
                    {
                        hd = (((2 * F1) * ((Math.Sin(i * (2 * Math.PI * F1))) / (i * (2 * Math.PI * F1)))))-
                            ((2 * F2) * ((Math.Sin(i * (2 * Math.PI * F2))) / (i * (2 * Math.PI * F2))));
                    }
                    h.Add(hd);
                }
            }
           // Console.WriteLine("h(n) size " + h.Count);


            //the h(n) = W(n) * hd(n) but the n here start from 0 to N/2
            //fill first from the end to 0 "without zero" 
            for (int i = w.Count - 1; i >= 0; i--) {
               // Console.WriteLine(-(i) + " h " + h[i] + "       w " + w[i]);
                result.Add((float)(h[i] * w[i]));
                index.Add(-i);
            }
            //fill from 0 to the end "with 0"
            for (int i = 1; i <  w.Count; i++)
            {
               // Console.WriteLine((i) + " h  " + h[i] + "       w  " + w[i]);
                result.Add((float)(h[i] * w[i]));
                index.Add(i);
            }
            //Console.WriteLine(" h.count" + h.Count + " w.count" + w.Count + " res.count" + result.Count);
             
            //set output
            OutputHn = new Signal(result,index,false);
            DirectConvolution d = new DirectConvolution();
            d.InputSignal1 = InputTimeDomainSignal;
            d.InputSignal2 = OutputHn;
            d.Run();
            OutputYn = d.OutputConvolvedSignal;
            
        }
    }
}