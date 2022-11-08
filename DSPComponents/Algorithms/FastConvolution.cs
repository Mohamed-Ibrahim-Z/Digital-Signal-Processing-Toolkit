using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            //throw new NotImplementedException();
            float[] sum = new float[InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1];
            int index = 0;
            for (int i = 0; i < InputSignal2.Samples.Count; i++)
            {
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    sum[index + j] += InputSignal1.Samples[j] * InputSignal2.Samples[i];
                }
                index++;
            }
            OutputConvolvedSignal = new Signal(sum.ToList(), false);
        }
    }
}
