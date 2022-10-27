using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using Microsoft.SqlServer.Server;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            List<Tuple<float, float>> intervals = new List<Tuple<float, float>>();
            OutputQuantizedSignal = new Signal(new List<float>(), false);
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            OutputSamplesError = new List<float>();

            float minAmplitude = InputSignal.Samples.AsQueryable().Min(),
                  maxAmplitude = InputSignal.Samples.AsQueryable().Max(),
                   delta;
            int levelsNum = (InputLevel != 0) ? InputLevel : (int)Math.Pow(2, InputNumBits);
            delta = (maxAmplitude - minAmplitude) / levelsNum;

            float start = minAmplitude;
            for (int i = 0; i < levelsNum; i++)
            {
                intervals.Add(new Tuple<float, float>((float)Math.Round(start, 2),(float)Math.Round(start + delta, 2)));
                start+=delta;
            }


            foreach(float element in InputSignal.Samples)
            {
                for (int i=0;i<intervals.Count;i++)
                {
                    if (element >= intervals[i].Item1 && element <= intervals[i].Item2)
                    {
                        float midpoint = (intervals[i].Item1 + intervals[i].Item2) / 2;
                        OutputQuantizedSignal.Samples.Add(midpoint);
                        OutputIntervalIndices.Add(i + 1);
                        OutputSamplesError.Add(midpoint-element);
                        OutputEncodedSignal.Add(Convert.ToString(i, 2).PadLeft((int)Math.Log(levelsNum, 2), '0'));
                        break;
                    }
                }
            }
            
        }
    }
}
