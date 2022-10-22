using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), false);
            int maxLength =0 ;
            for(int i =0; i< InputSignals.Count;i++)
            {
                if (InputSignals[i].Samples.Count > maxLength)
                    maxLength = InputSignals[i].Samples.Count;
            }
            for(int i = 0; i< maxLength; i++)
            {
                
                float val=0;
                for (int j = 0; j < InputSignals.Count; j++)
                    val += InputSignals[j].Samples[i];
                OutputSignal.Samples.Add(val);
            }
        }
    }
}