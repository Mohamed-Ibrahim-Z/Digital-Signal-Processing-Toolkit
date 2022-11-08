using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            OutputConvolvedSignal = new Signal(new List<float>(), new List<int>(), false);
            int minIndex = Math.Min(InputSignal2.SamplesIndices[0], InputSignal1.SamplesIndices[0]);
            int deff= Math.Abs(InputSignal2.SamplesIndices[0]-InputSignal1.SamplesIndices[0]);
            int size = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            float[] sum = new float[size];
            int arrIndex = 0;
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                for (int j = 0; j < InputSignal2.Samples.Count; j++)
                {
                    sum[arrIndex + j] += InputSignal1.Samples[i] * InputSignal2.Samples[j];
                }
                arrIndex++;
            }
            if (InputSignal2.SamplesIndices[0]!=0&&InputSignal1.SamplesIndices[0]!=0) minIndex -= deff;
            for (int i = 0; i < size; i++)
            {
                if (i == size - 1 && sum[i] == 0)
                    break;
                OutputConvolvedSignal.Samples.Add(sum[i]);
                OutputConvolvedSignal.SamplesIndices.Add(minIndex+i);
            }
        }
    }
}
