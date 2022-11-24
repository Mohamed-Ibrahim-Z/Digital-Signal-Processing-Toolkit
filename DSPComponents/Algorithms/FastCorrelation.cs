using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public List<float> ShiftNonPeriodicSignal(List<float> samples, int shift)
        {
            List<float> shiftedSignal = new List<float>();
            for (int i = 0; i < samples.Count; i++)
            {
                if (i + shift < 0 || i + shift >= samples.Count)
                    shiftedSignal.Add(0);
                else
                    shiftedSignal.Add(samples[i + shift]);
            }
            return shiftedSignal;
        }
        
        //shift nonperiodic signal

        
        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            if (InputSignal2 == null) { InputSignal2 = new Signal(InputSignal1.Samples, false); }
            float sumIN1 = 0;
            float sumIN2 = 0;

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                sumIN1 += (float)Math.Pow(InputSignal1.Samples[i], 2);
                sumIN2 += (float)Math.Pow(InputSignal2.Samples[i], 2);
            }
            float norm = (float)(Math.Sqrt(sumIN1 * sumIN2) / InputSignal1.Samples.Count);
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                float sum = 0;
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    int index= (i+j) % InputSignal1.Samples.Count;
                    sum += InputSignal1.Samples[j] * InputSignal2.Samples[index];
                }
                OutputNonNormalizedCorrelation.Add(sum / InputSignal1.Samples.Count);
                OutputNormalizedCorrelation.Add((sum / InputSignal1.Samples.Count) / norm);
            }

        }
    }
}