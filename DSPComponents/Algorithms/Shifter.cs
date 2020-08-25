using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            OutputShiftedSignal = new Signal(InputSignal.Samples, InputSignal.SamplesIndices, InputSignal.Periodic, InputSignal.Frequencies, InputSignal.FrequenciesAmplitudes, InputSignal.FrequenciesPhaseShifts); ;
            for(int i = 0; i < OutputShiftedSignal.SamplesIndices.Count; ++i)
            {
                if(InputSignal.Periodic == false)
                   OutputShiftedSignal.SamplesIndices[i] += ShiftingValue * -1;
                else
                    OutputShiftedSignal.SamplesIndices[i] += ShiftingValue;

            }
        }
    }
}
