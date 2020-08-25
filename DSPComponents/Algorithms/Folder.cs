using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {

            int origin_index = 0;
            for (int i = 0; i < InputSignal.SamplesIndices.Count; ++i)
            {
                if (InputSignal.SamplesIndices[i] == 0)
                    origin_index = i;
            }

            List<float> after_origin_samples = InputSignal.Samples.GetRange(origin_index + 1, InputSignal.Samples.Count - (origin_index + 1));
            List<float> before_origin_samples = InputSignal.Samples.GetRange(0, origin_index);

            List<int> after_origin_indices = InputSignal.SamplesIndices.GetRange(origin_index + 1, InputSignal.SamplesIndices.Count - (origin_index + 1));
            List<int> before_origin_indices = InputSignal.SamplesIndices.GetRange(0, origin_index);


            before_origin_samples.Reverse();
            after_origin_samples.Reverse();

            after_origin_indices.Reverse();
            before_origin_indices.Reverse();
           
                for (int i = 0; i < after_origin_indices.Count; ++i)
                    after_origin_indices[i] *= -1;
                for (int i = 0; i < before_origin_indices.Count; ++i)
                    before_origin_indices[i] *= -1;
            

            List<float> output_samples = new List<float>();
            output_samples = output_samples.Concat(after_origin_samples).ToList();
            output_samples.Add(InputSignal.Samples[origin_index]);
            output_samples = output_samples.Concat(before_origin_samples).ToList();

            List<int> output_indices = new List<int>();
            output_indices = output_indices.Concat(after_origin_indices).ToList();
            output_indices.Add(InputSignal.SamplesIndices[origin_index]);
            output_indices = output_indices.Concat(before_origin_indices).ToList();


            OutputFoldedSignal = new Signal(output_samples, output_indices, !InputSignal.Periodic, InputSignal.Frequencies, InputSignal.FrequenciesAmplitudes, InputSignal.FrequenciesPhaseShifts);





        }
    }
}
