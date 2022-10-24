using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), false);
            //int maxLength = Math.Max(InputSignal1.Samples.Count, InputSignal2.Samples.Count); 
            //for (int i = 0; i < maxLength; i++)
            //{
            //    float val = InputSignal1.Samples[i] - InputSignal2.Samples[i];
            //    OutputSignal.Samples.Add(val);
            //}
            MultiplySignalByConstant m = new MultiplySignalByConstant();
            m.InputSignal= InputSignal2;
            m.InputConstant = -1;
            m.Run();

            Adder adder = new Adder();
            adder.InputSignals = new List<Signal>();
            adder.InputSignals.Add(InputSignal1);
            adder.InputSignals.Add(m.OutputMultipliedSignal);
            adder.Run();

            OutputSignal = adder.OutputSignal;

            
        }
    }
}