using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            OutputAverageSignal = new Signal(new List<float>(), false);

            for (int i = 0; i < InputSignal.Samples.Count - InputWindowSize + 1; ++i)
            {
                float sum = 0.0F;
                for (int j = i; j < i + InputWindowSize; ++j)
                    sum += InputSignal.Samples[j];
                
                float average = sum / (float)InputWindowSize;
                OutputAverageSignal.Samples.Add(average);
            }
        }
    }
}
