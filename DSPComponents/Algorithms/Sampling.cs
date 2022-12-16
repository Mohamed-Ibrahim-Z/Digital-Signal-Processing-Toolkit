﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }


        

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), false);
            if (M != 0 && L == 0)
            {
                FIR fir = new FIR();
                fir.InputTimeDomainSignal = InputSignal;
                fir.InputFilterType = FILTER_TYPES.LOW;
                fir.InputCutOffFrequency = 1500;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputTransitionBand = 500;
                fir.Run();
                InputSignal = fir.OutputYn;
                for (int i = 0; i < InputSignal.Samples.Count; i += M)
                {
                    OutputSignal.Samples.Add(InputSignal.Samples[i]);
                }

            }
            else if (L != 0 && M == 0)
            {
                int index = 0;
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {

                    OutputSignal.Samples.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        OutputSignal.Samples.Add(0);
                        OutputSignal.SamplesIndices.Add(index);
                        index++;
                    }
                }
                FIR fir = new FIR();
                fir.InputTimeDomainSignal = OutputSignal;
                fir.InputFilterType = FILTER_TYPES.LOW;
                fir.InputCutOffFrequency = 1500;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputTransitionBand = 500;
                fir.Run();
                OutputSignal = fir.OutputYn;
            }
            else
            {
                int index = 0;
                Signal temp = new Signal(new List<float>(),new List<int>(),false);
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    temp.Samples.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L - 1; j++)
                    {
                        temp.Samples.Add(0);
                        temp.SamplesIndices.Add(index);
                        index++;
                    }
                }
                FIR fir = new FIR();
                fir.InputTimeDomainSignal = temp;
                fir.InputFilterType = FILTER_TYPES.LOW;
                fir.InputCutOffFrequency = 1500;
                fir.InputFS = 8000;
                fir.InputStopBandAttenuation = 50;
                fir.InputTransitionBand = 500;
                fir.Run();
                

                for (int i = 0; i < fir.OutputYn.Samples.Count; i += M)
                {
                    OutputSignal.Samples.Add(fir.OutputYn.Samples[i]);
                }

            }

        }
    }
    
}