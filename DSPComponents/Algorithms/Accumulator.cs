using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Accumulator : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputAccumulatedSignal { get; set; }

        public override void Run()
        {
            OutputAccumulatedSignal = new Signal(new List<float>(), InputSignal.SamplesIndices, false);
            for (int i = 0; i < InputSignal.Samples.Count; ++i)
            {
                if (i == 0)
                    OutputAccumulatedSignal.Samples.Add(InputSignal.Samples[i]);
                else
                    OutputAccumulatedSignal.Samples.Add(OutputAccumulatedSignal.Samples[i - 1] + InputSignal.Samples[i]);
            }

        }
    }
}
