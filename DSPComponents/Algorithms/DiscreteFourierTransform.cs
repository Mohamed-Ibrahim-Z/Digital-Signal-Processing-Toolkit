using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;


namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            OutputFreqDomainSignal = new Signal(true,new List<float>(),new List<float>(),new List<float>());
            
            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; i++)
            {
                Complex sum = new Complex(0, 0);
                for (int j = 0; j < InputTimeDomainSignal.Samples.Count; j++)
                {
                    Complex e = new Complex(InputTimeDomainSignal.Samples[j] * (float)Math.Cos(2 * Math.PI * i * j / InputTimeDomainSignal.Samples.Count),
                        -InputTimeDomainSignal.Samples[j] * (float)Math.Sin(2 * Math.PI * i * j / InputTimeDomainSignal.Samples.Count));
                    sum = new Complex(e.Real + sum.Real, e.Imaginary + sum.Imaginary);
                }
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add((float)sum.Magnitude);
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add((float)sum.Phase);

            }


        }
    }
}
