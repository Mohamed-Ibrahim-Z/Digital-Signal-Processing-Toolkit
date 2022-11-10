using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        { 
            FirstDerivative = new Signal(new List<float>(), false);
            SecondDerivative = new Signal(new List<float>(), false);
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float sample = InputSignal.Samples[i], before, after;
                if (i == InputSignal.Samples.Count - 1) after = InputSignal.Samples[i]; else after = InputSignal.Samples[i + 1];
                if (i == 0) before = 0; else before = InputSignal.Samples[i - 1];
                FirstDerivative.Samples.Add(sample - before);
                SecondDerivative.Samples.Add(after - 2*sample+before);
                            
            }

        }
    }
}
