﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }


        private Signal lowPassFilter(Signal inputSignal)
        {
            FIR fir = new FIR();
            fir.InputTimeDomainSignal = inputSignal;
            fir.InputFilterType = FILTER_TYPES.LOW;
            fir.InputCutOffFrequency = 1500;
            fir.InputFS = 8000;
            fir.InputStopBandAttenuation = 50;
            fir.InputTransitionBand = 500;
            fir.Run();
            return fir.OutputYn;
        }
        private Signal sample(Signal inputSignal, string s)
        {
            Signal outputSignal = new Signal(new List<float>(), new List<int>(), false);
            int index = 0;
            switch (s)
            {
                case "down":
                    {
                        
                        for (int i = 0; i < inputSignal.Samples.Count; i += M)
                        {
                            outputSignal.Samples.Add(inputSignal.Samples[i]);
                            outputSignal.SamplesIndices.Add(inputSignal.SamplesIndices[index]);
                            index++;
                        }
                        return outputSignal;
                    }
                case "up":
                    {
                        
                        for (int i = 0; i < inputSignal.Samples.Count; i++)
                        {

                            outputSignal.Samples.Add(inputSignal.Samples[i]);
                            for (int j = 0; j < L - 1; j++)
                            {
                                outputSignal.Samples.Add(0);
                                outputSignal.SamplesIndices.Add(index);
                                index++;
                            }
                        }
                        return outputSignal;
                    }
                default:
                    return null;
            }  
        }
        public override void Run()
        {
            if (M != 0 && L == 0)
            {
                InputSignal = lowPassFilter(InputSignal);
                OutputSignal = sample(InputSignal, "down");

            }
            else if (L != 0 && M == 0)
            {
                OutputSignal = sample(InputSignal, "up");
                OutputSignal = lowPassFilter(OutputSignal);
            }
            else
            {
                OutputSignal = sample(InputSignal, "up");
                OutputSignal = lowPassFilter(OutputSignal);
                OutputSignal = sample(OutputSignal, "down");
            }
        }
    }
    
}