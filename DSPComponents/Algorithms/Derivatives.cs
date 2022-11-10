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
           
            for (int i = 0; i < InputSignal.Samples.Count - 1; i++)
            {
                if (i == 0)
                {
                    FirstDerivative.Samples.Add(InputSignal.Samples[i]);
                    SecondDerivative.Samples.Add(InputSignal.Samples[i + 1] - 2 * InputSignal.Samples[i]);
                }
                
                else
                {
                    FirstDerivative.Samples.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);
                    SecondDerivative.Samples.Add(InputSignal.Samples[i + 1] - 2 * InputSignal.Samples[i] + InputSignal.Samples[i - 1]);
                }
            }

        }
    }
}
