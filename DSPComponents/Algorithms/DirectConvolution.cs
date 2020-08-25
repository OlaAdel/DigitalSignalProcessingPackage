using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            OutputConvolvedSignal = new Signal(new List<float>(), new List<int>(), false);
            int signal1_index = InputSignal1.SamplesIndices.Min();
            int signal2_index = InputSignal2.SamplesIndices.Min();

            InputSignal2.Samples.Reverse();
            List<float> signal_1 = new List<float>();

            for (int i = 0; i < InputSignal2.Samples.Count - 1; ++i)
                signal_1.Add(0.0F);
            for (int i = 0; i < InputSignal1.Samples.Count; ++i)
                signal_1.Add(InputSignal1.Samples[i]);
            for (int i = 0; i < InputSignal2.Samples.Count - 1; ++i)
                signal_1.Add(0.0F);

            int index = signal1_index + signal2_index;
            for(int i = 0; i < InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1; ++i)
            {
                float sum = 0;
                for(int j = 0; j < InputSignal2.Samples.Count; ++j)
                {
                    sum += signal_1[i + j] * InputSignal2.Samples[j];
                }
                OutputConvolvedSignal.SamplesIndices.Add(index + i);
                OutputConvolvedSignal.Samples.Add(sum);
            }
            for(int i = OutputConvolvedSignal.Samples.Count - 1; i >= 0; --i)
            {
                if (OutputConvolvedSignal.Samples[i] == 0.0f)
                {
                    OutputConvolvedSignal.Samples.RemoveAt(OutputConvolvedSignal.Samples.Count - 1);
                    OutputConvolvedSignal.SamplesIndices.RemoveAt(OutputConvolvedSignal.SamplesIndices.Count - 1);
                }
                else
                    break;
            }

        }
    }
}
