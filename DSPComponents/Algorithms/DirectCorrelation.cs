using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        public List<float> clcco(Signal s1, Signal s2)// (calculate corelation if periodic or not if priodic aplay round if not shift )
        {
            List<double> lis = new List<double>();
            List<float> result = new List<float>();

            for (int j = 0; j < s1.Samples.Count; j++)
            {
                lis = new List<double>();
                for (int i = 0; i < s1.Samples.Count; i++)
                    lis.Add(s1.Samples[i] * s2.Samples[i]);

                double sum = lis.Sum();
                result.Add((float)Math.Round((sum / s1.Samples.Count), 15));
                float R = s2.Samples[0];
                s2.Samples.RemoveAt(0);



                if (s1.Periodic)
                    s2.Samples.Add(R);
                else
                    s2.Samples.Add(0);
            }
            return result;

        }
        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            if (InputSignal2 == null) 
            {
                InputSignal2 = new Signal(new List<float>(), false, new List<float>(), new List<float>(), new List<float>());
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                    InputSignal2.Samples.Add(InputSignal1.Samples[i]);
            }
            if (!InputSignal1.Periodic)
            {   
                for (int i = InputSignal1.Samples.Count; i < InputSignal2.Samples.Count; i++)
                    InputSignal1.Samples.Add(0);
                for (int i = InputSignal2.Samples.Count; i < InputSignal1.Samples.Count; i++)
                    InputSignal2.Samples.Add(0);
            }
            else
                if (InputSignal1.Samples.Count != InputSignal2.Samples.Count)
            {
                int Sum = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
                for (int i = InputSignal1.Samples.Count; i < Sum; i++)
                    InputSignal1.Samples.Add(0);
                for (int i = InputSignal2.Samples.Count; i < Sum; i++)
                    InputSignal2.Samples.Add(0);
            }


            Signal s1 = new Signal(new List<float>(), false, new List<float>(), new List<float>(), new List<float>());
            Signal s2 = new Signal(new List<float>(), false, new List<float>(), new List<float>(), new List<float>());

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                s1.Samples.Add(InputSignal1.Samples[i]);

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
                s2.Samples.Add(InputSignal2.Samples[i]);

            OutputNonNormalizedCorrelation = clcco(InputSignal1, InputSignal2);

            List<float> corelation1 = new List<float>();// (list (s1,s1 ))// cross corelation 
            List<float> corelation2 = new List<float>();//(list (s2,s2)
            corelation1 = clcco(s1, s1);
            corelation2 = clcco(s2, s2);

            float Co = 0;
            Co = (float)Math.Sqrt((corelation1[0] * corelation2[0]));

            for (int i = 0; i < corelation1.Count; i++)
                OutputNormalizedCorrelation.Add((OutputNonNormalizedCorrelation[i] / Co));
        }
    }
}