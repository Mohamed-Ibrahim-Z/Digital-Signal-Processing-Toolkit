using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

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
            int size = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            int n1 = size - InputSignal1.Samples.Count;
            int n2 = size - InputSignal2.Samples.Count;
            OutputConvolvedSignal = new Signal(false,new List<float>(), new List<float>(), new List<float>());
            DiscreteFourierTransform dft = new DiscreteFourierTransform();
            InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
             
            for (int i = 0; i <n1; i++) InputSignal1.Samples.Add(0);
            for (int i = 0; i <n2; i++) InputSignal2.Samples.Add(0);
           
            dft.InputTimeDomainSignal = InputSignal1;
            dft.Run();
            Signal signal1 = new Signal(false, null, 
                new List<float>(dft.OutputFreqDomainSignal.FrequenciesAmplitudes),
                new List<float>(dft.OutputFreqDomainSignal.FrequenciesPhaseShifts));
            dft.InputTimeDomainSignal = InputSignal2;
            dft.Run();
            Signal signal2 = new Signal(false, new List<float>(),
                new List<float>(dft.OutputFreqDomainSignal.FrequenciesAmplitudes),
                new List<float>(dft.OutputFreqDomainSignal.FrequenciesPhaseShifts));

            for (int i = 0; i < size; i++)
            {
                Complex c1 = Complex.FromPolarCoordinates(signal1.FrequenciesAmplitudes[i], signal1.FrequenciesPhaseShifts[i]),
                c2 = Complex.FromPolarCoordinates(signal2.FrequenciesAmplitudes[i], signal2.FrequenciesPhaseShifts[i]),
                result = Complex.Multiply(c1,c2);
                
                OutputConvolvedSignal.FrequenciesAmplitudes.Add((float)result.Magnitude);
                OutputConvolvedSignal.FrequenciesPhaseShifts.Add((float)result.Phase);
                OutputConvolvedSignal.Frequencies.Add(0);
            }
            idft.InputFreqDomainSignal = OutputConvolvedSignal;
            idft.Run();
            OutputConvolvedSignal = idft.OutputTimeDomainSignal;
        }
    }
}
