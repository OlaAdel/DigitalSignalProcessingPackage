using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

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
            List<float> _SignalSamples = new List<float>();
            bool _periodic = false;
            OutputQuantizedSignal = new Signal(_SignalSamples, _periodic);
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            OutputSamplesError = new List<float>();


            
            if (InputNumBits > 0)
            {
                InputLevel = 1;
                for (int i = 0; i < InputNumBits; ++i)
                    InputLevel *= 2;
            }
            if(InputLevel > 0)
            {
                double bits = Math.Log(InputLevel, 2);
                if (bits % 1 != 0)
                    InputNumBits = Convert.ToInt32(bits) + 1;
                else
                    InputNumBits = Convert.ToInt32(bits);
            }
            float mini = InputSignal.Samples.Min();
            float maxi = InputSignal.Samples.Max();
            float delta = (maxi - mini) / InputLevel;

            List<Tuple<float, float>> levels_intervals = new List<Tuple<float, float>>();
            List<float> levels_midpoints = new List<float>();
            float start = mini;
            float end = maxi;
            for(int i = 0; i < InputLevel; ++i)
            {
                end = start + delta;
                levels_intervals.Add(new Tuple<float, float>(start, end));

                levels_midpoints.Add((float)Math.Round((start + end) / 2.0F, 3));
                start = end;
            }
            for (int i = 0; i < InputSignal.Samples.Count; ++i)
            {
                float intervel = ((InputSignal.Samples[i] - mini) / (maxi - mini)) * InputLevel;

                int intervel_index = (int)((float)Math.Round(intervel, 0));
                if (intervel_index > intervel) intervel_index -= 1;
                if (intervel_index == InputLevel) intervel_index -= 1;

                OutputIntervalIndices.Add(intervel_index + 1);
                OutputQuantizedSignal.Samples.Add(levels_midpoints[intervel_index]);
                string encoded = Convert.ToString(intervel_index, 2);
                string rem = "";
                for (int j = 0; j < InputNumBits - encoded.Length; ++j)
                    rem += '0';

                OutputEncodedSignal.Add(rem + encoded);
                OutputSamplesError.Add(levels_midpoints[intervel_index] - InputSignal.Samples[i]);
               
            }
           
           
            
        }
    }
}
