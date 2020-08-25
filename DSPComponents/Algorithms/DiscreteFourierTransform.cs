using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            OutputFreqDomainSignal = new Signal(false, new List<float>(), new List<float>(), new List<float>());
            int N = InputTimeDomainSignal.Samples.Count;
            for (int k = 0; k < N; ++k)
            {
                double imaginary_sum = 0.0F;
                double real_sum = 0.0F;
                for(int n = 0; n < N; ++n)
                {
                    double real = Math.Cos((2.0f * Math.PI * ((double)k) * ((double)n)) / ((double)N));
                    double imaginary = Math.Sin((2.0f * Math.PI * ((double)k) * ((double)n)) / ((double)N)) * -1.0f;

                    real *= InputTimeDomainSignal.Samples[n];
                    imaginary *= InputTimeDomainSignal.Samples[n];

                    real_sum += real;
                    imaginary_sum += imaginary;
                }
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add((float)Math.Sqrt(real_sum * real_sum + imaginary_sum * imaginary_sum));
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add((float)Math.Round(Math.Atan2(imaginary_sum, real_sum),10));
            }
            double freq_base = ((double)(2.0f * Math.PI)) / (((double)N) * (1.0f / ((double)InputSamplingFrequency)));
            for (int i = 0; i < N; ++i)
            {
                OutputFreqDomainSignal.Frequencies.Add((float)(freq_base * (double)(i + 1)));
            }
            
        }
    }
}


