 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            bool finished = false;
            OutputAverageSignal = new Signal(new List<float>(), false);
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float sum = 0;
                for (int j = 0; j < InputWindowSize; j++)
                {
                    sum += InputSignal.Samples[i + j];
                    if(i+j== InputSignal.Samples.Count-1) finished = true;
                }
                OutputAverageSignal.Samples.Add(sum / InputWindowSize);
                if (finished) break;
            }
        }
    }
}
