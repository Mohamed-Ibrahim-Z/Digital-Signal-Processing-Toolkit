using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            OutputTimeDomainSignal = new Signal(new List<float>(), false);
            // Reconstruct the signal from the given frequency domain samples
            // Use the following formula to reconstruct the signal:
            // x[n] = (1/N) * (X[0] + 2 * (X[1] + X[2] + ... + X[N/2 - 1]) + X[N/2]) * e^(j * 2 * pi * n * k / N)

            for (int i = 0; i < InputFreqDomainSignal.Frequencies.Count; i++)
            {
                Complex sum = new Complex(0, 0);
                for (int j = 0; j < InputFreqDomainSignal.Frequencies.Count; j++)
                {
                    Complex x = Complex.FromPolarCoordinates(InputFreqDomainSignal.FrequenciesAmplitudes[j], InputFreqDomainSignal.FrequenciesPhaseShifts[j]);
                    sum += x * Complex.Exp(new Complex(0, 2 * Math.PI * i * j / InputFreqDomainSignal.Frequencies.Count));
                }
                OutputTimeDomainSignal.Samples.Add((float)sum.Real / InputFreqDomainSignal.Frequencies.Count);

            }
        }
    }
}
