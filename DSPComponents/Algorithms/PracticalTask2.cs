﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DSPAlgorithms.Algorithms
{
    public class PracticalTask2 : Algorithm
    {
        public String SignalPath { get; set; }
        public float Fs { get; set; }
        public float miniF { get; set; }
        public float maxF { get; set; }
        public float newFs { get; set; }
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            //variables
            Signal InputSignal = LoadSignal(SignalPath);
            FIR fir = new FIR();

            Console.WriteLine("number of samples: " + InputSignal.Samples.Count);

            fir.InputF1 = miniF;
            fir.InputF2 = maxF;
            fir.InputFS = Fs;
            fir.InputFilterType = FILTER_TYPES.BAND_PASS;
            fir.InputStopBandAttenuation = 50;
            fir.InputTransitionBand = 500;
            fir.InputTimeDomainSignal = InputSignal;    //input signal
            fir.Run();

            //store
            using (StreamWriter writer = new StreamWriter("C:\\Users\\Legendary\\OneDrive\\Desktop\\firpractice2.ds"))
            {
                writer.WriteLine("0"); //freq
                writer.WriteLine("0"); //peridic 
                writer.WriteLine(fir.OutputYn.Samples.Count().ToString());
                for (int i = 0; i < fir.OutputYn.Samples.Count(); i++){
                    writer.WriteLine(fir.OutputYn.SamplesIndices[i].ToString() + " " + fir.OutputYn.Samples[i].ToString());                
                }
            }





            if (newFs >= maxF / 2){
                Sampling sampling = new Sampling();
                sampling.L = L;
                sampling.M = M;
                sampling.InputSignal = fir.OutputYn; // input sampling
                sampling.Run();
                Console.WriteLine(" L " + L + " M " + M + " #output: " + sampling.OutputSignal.Samples.Count);
                //store
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Legendary\\OneDrive\\Desktop\\firpractice2.ds"))
                {
                    writer.WriteLine("0");
                    writer.WriteLine("0");
                    writer.WriteLine(sampling.OutputSignal.Samples.Count().ToString());
                    for (int i = 0; i < sampling.OutputSignal.Samples.Count(); i++)
                    {
                        writer.WriteLine(sampling.OutputSignal.SamplesIndices[i].ToString() + " " + sampling.OutputSignal.Samples[i].ToString());
                    }
                }


                DC_Component DC = new DC_Component();
                DC.InputSignal = sampling.OutputSignal; //input
                DC.Run();
                Console.WriteLine("DC " + DC.OutputSignal.Samples.Count);

                using (StreamWriter writer = new StreamWriter("C:\\Users\\Legendary\\OneDrive\\Desktop\\firpractice2.ds"))
                {
                    writer.WriteLine("0");
                    writer.WriteLine("0");
                    writer.WriteLine(DC.OutputSignal.Samples.Count().ToString());
                    for (int i = 0; i < DC.OutputSignal.Samples.Count(); i++)
                    {
                        writer.WriteLine(DC.OutputSignal.SamplesIndices[i].ToString() + " " + DC.OutputSignal.Samples[i].ToString());
                    }
                }

                Normalizer norm = new Normalizer();
                norm.InputSignal = DC.OutputSignal;
                norm.InputMinRange = -1;
                norm.InputMaxRange = 1;
                norm.Run();
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Legendary\\OneDrive\\Desktop\\firpractice2.ds"))
                {
                    writer.WriteLine("0");
                    writer.WriteLine("0");
                    writer.WriteLine(norm.OutputNormalizedSignal.Samples.Count().ToString());
                    for (int i = 0; i < norm.OutputNormalizedSignal.Samples.Count(); i++)
                    {

                        writer.WriteLine(norm.OutputNormalizedSignal.SamplesIndices[i].ToString() + " " + norm.OutputNormalizedSignal.Samples[i].ToString());

                    }
                }


                DiscreteFourierTransform discrete = new DiscreteFourierTransform();
                discrete.InputTimeDomainSignal = norm.OutputNormalizedSignal;//input
                discrete.InputSamplingFrequency = Fs;
                discrete.Run();
                for (int i = 0; i < discrete.OutputFreqDomainSignal.Frequencies.Count; i++)
                {
                    discrete.OutputFreqDomainSignal.Frequencies[i] = (float)Math.Round((double)discrete.OutputFreqDomainSignal.Frequencies[i], 1); // round here
                }
                OutputFreqDomainSignal = discrete.OutputFreqDomainSignal;

                using (StreamWriter writer = new StreamWriter("C:\\Users\\Legendary\\OneDrive\\Desktop\\firpractice2.ds"))
                {
                    writer.WriteLine("1");
                    writer.WriteLine("0");
                    writer.WriteLine(discrete.OutputFreqDomainSignal.Frequencies.Count().ToString());
                    for (int i = 0; i < discrete.OutputFreqDomainSignal.Frequencies.Count(); i++)
                    {
                        writer.WriteLine(x.ToString() + " " + discrete.OutputFreqDomainSignal.FrequenciesAmplitudes[i].ToString() + " " + discrete.OutputFreqDomainSignal.FrequenciesPhaseShifts[i].ToString());

                    }
                }

            }
            

     



        }

        public Signal LoadSignal(string filePath)
        {
            Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(stream);

            var sigType = byte.Parse(sr.ReadLine());
            var isPeriodic = byte.Parse(sr.ReadLine());
            long N1 = long.Parse(sr.ReadLine());

            List<float> SigSamples = new List<float>(unchecked((int)N1));
            List<int> SigIndices = new List<int>(unchecked((int)N1));
            List<float> SigFreq = new List<float>(unchecked((int)N1));
            List<float> SigFreqAmp = new List<float>(unchecked((int)N1));
            List<float> SigPhaseShift = new List<float>(unchecked((int)N1));

            if (sigType == 1)
            {
                SigSamples = null;
                SigIndices = null;
            }

            for (int i = 0; i < N1; i++)
            {
                if (sigType == 0 || sigType == 2)
                {
                    var timeIndex_SampleAmplitude = sr.ReadLine().Split();
                    SigIndices.Add(int.Parse(timeIndex_SampleAmplitude[0]));
                    SigSamples.Add(float.Parse(timeIndex_SampleAmplitude[1]));
                }
                else
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            if (!sr.EndOfStream)
            {
                long N2 = long.Parse(sr.ReadLine());

                for (int i = 0; i < N2; i++)
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            stream.Close();
            return new Signal(SigSamples, SigIndices, isPeriodic == 1, SigFreq, SigFreqAmp, SigPhaseShift);
        }
    }
}
