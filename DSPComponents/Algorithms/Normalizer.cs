using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            float minValue = InputSignal.Samples.Min();
            float maxValue = InputSignal.Samples.Max();
            OutputNormalizedSignal = new Signal(new List<float>(), false);
            for (int i = 0; i < InputSignal.Samples.Count; i++)
                // ( (signal - minVal) / (maxVal - minVal) ) * (InputMaxRange - InputMinRange) + InputMinRange
                OutputNormalizedSignal.Samples.Add(
                    (InputSignal.Samples[i] - minValue) / (maxValue - minValue) *
                      (InputMaxRange - InputMinRange) + InputMinRange
                    );
                
        }
    }
}
