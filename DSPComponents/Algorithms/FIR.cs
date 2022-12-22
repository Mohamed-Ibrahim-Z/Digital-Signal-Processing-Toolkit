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
            OutputHn = new Signal(new List<float>(),new List<int>(), false);
            OutputYn = new Signal(new List<float>(),new List<int>(),false);
            List<float> w = new List<float>();
            List<float> h = new List<float>();
            int N;
            if (InputStopBandAttenuation <= 21)
            {
                N =(int)Math.Ceiling( 0.9f / (float)(InputTransitionBand / InputFS));
                if (N % 2 == 0){ N++;}
                for (int i = -N/2; i <= N/2; i++){w.Add(1);}
            }
            else if (InputStopBandAttenuation <= 44)
            {
                N = (int)Math.Ceiling(3.1f / (float)(InputTransitionBand / InputFS));
                if (N % 2 == 0) { N++; }
                for (int i = -N/2; i <= N/2; i++) { w.Add((float)(0.5f+0.5f*Math.Cos((2*Math.PI*i)/N))); }
            }
            else if (InputStopBandAttenuation <= 53)
            {
                N = (int)Math.Ceiling(3.3f / (float)(InputTransitionBand / InputFS));
                if (N % 2 == 0) { N++; }
                for (int i = -N/2; i <= N/2; i++) { w.Add((float)(0.54f + 0.46f *(float) Math.Cos((2 * Math.PI * i) / N))); }
            }
            else
            {
                N = (int)Math.Ceiling(5.5f / (float)(InputTransitionBand / InputFS));
                if (N % 2 == 0) { N++; }
                for (int i = -N/2; i <= N/2; i++) { 
                    w.Add((float)(0.42f + (0.5f * Math.Cos((2 * Math.PI * i) / (N-1)))+
                                              (0.08f * Math.Cos((4 * Math.PI * i) / (N-1)))));
                }
            }

            switch (InputFilterType)
            {
                case FILTER_TYPES.LOW:
                    {
                        float fc = (float)(InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS;
                        for (int i = -N / 2; i <= N / 2; i++)
                        {
                            if (i == 0) { h.Add(2 * fc); }
                            else h.Add((float)(2 * fc * (Math.Sin(2 * Math.PI * fc * i)) / (2 * Math.PI * fc * i)));
                        }
                        break;
                    }
                case FILTER_TYPES.HIGH:
                    {
                        float fc = (float)(InputCutOffFrequency - (InputTransitionBand / 2)) / InputFS;
                        for (int i = -N / 2; i <= N / 2; i++)
                        {
                            if (i == 0) { h.Add(1 - (2 * fc)); }
                            else h.Add((float)(-2 * fc * (Math.Sin(2 * Math.PI * fc * i)) / (2 * Math.PI * fc * i)));
                        }
                        break;
                    }
                case FILTER_TYPES.BAND_PASS:
                    {
                        float fc1 = (float)((InputF1 - (InputTransitionBand / 2)) / InputFS);
                        float fc2 = (float)((InputF2 + (InputTransitionBand / 2)) / InputFS);
                        for (int i = -N / 2; i <= N / 2; i++)
                        {
                            if (i == 0) { h.Add(2 * (fc2 - fc1)); }
                            else h.Add((float)((2 * fc2 * Math.Sin((2 * Math.PI * fc2 * i)) / (2 * Math.PI * fc2 * i)) -
                                               (2 * fc1 * Math.Sin((2 * Math.PI * fc1 * i)) / (2 * Math.PI * fc1 * i))));
                        }
                        break;
                    }
                case FILTER_TYPES.BAND_STOP:
                    {
                        float fc1 = (float)((InputF1 + (InputTransitionBand / 2)) / InputFS);
                        float fc2 = (float)((InputF2 - (InputTransitionBand / 2)) / InputFS);
                        for (int i = -N / 2; i <= N / 2; i++)
                        {
                            if (i == 0) { h.Add(1 - 2 * (fc2 - fc1)); }
                            else h.Add((float)((2 * fc1 * Math.Sin((2 * Math.PI * fc1 * i)) / (2 * Math.PI * fc1 * i)) -
                                               (2 * fc2 * Math.Sin((2 * Math.PI * fc2 * i)) / (2 * Math.PI * fc2 * i))));
                        }
                        break;
                    }
            }
            for (int i =0, j=-N/2; i < N; i++,j++)
            {
                OutputHn.Samples.Add(h[i] * w[i]);
                OutputHn.SamplesIndices.Add(j);
            }

            DirectConvolution dc = new DirectConvolution();
            dc.InputSignal1 = OutputHn;
            dc.InputSignal2 = InputTimeDomainSignal;
            dc.Run();
            OutputYn = dc.OutputConvolvedSignal;
        }
    }
}
