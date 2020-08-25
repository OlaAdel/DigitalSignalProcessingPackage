using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public void FFTrecursive(ref List<Complex> samples, int cnt, bool inverse)
        {
            if (cnt == 1)
                return;

            List<Complex> even_samples = new List<Complex>();
            List<Complex> odd_samples = new List<Complex>();

            for (int i = 0; i < cnt; ++i)
            {
                if (i % 2 == 0)
                    even_samples.Add(samples[i]);
                else
                    odd_samples.Add(samples[i]);
            }
            FFTrecursive(ref even_samples, cnt / 2, inverse);
            FFTrecursive(ref odd_samples, cnt / 2, inverse);
            for (int k = 0; k < cnt / 2; ++k)
            {
                double real = (double)Math.Cos((-2.0f * Math.PI * ((double)k)) / (double)cnt);
                double imaginary = (double)Math.Sin((-2.0f * Math.PI * ((double)k)) / (double)cnt);
                Complex t = new Complex();
                if (inverse == true)
                    t = new Complex(real, imaginary * -1.0) * odd_samples[k];
                else
                    t = new Complex(real, imaginary) * odd_samples[k];

                samples[k] = even_samples[k] + t;
                samples[k + (cnt / 2)] = even_samples[k] - t;
            }
        }


        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            OutputConvolvedSignal = new Signal(new List<float>(), new List<int>(), false);

            int N = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;

            List<Complex> signal1_freq_domain = new List<Complex>();
            for (int i = 0; i < N; ++i)
            {
                if(i < InputSignal1.Samples.Count)
                    signal1_freq_domain.Add(new Complex(InputSignal1.Samples[i], 0));
                else
                    signal1_freq_domain.Add(new Complex(0, 0));
            }

            List<Complex> signal2_freq_domain = new List<Complex>();
            for (int i = 0; i < N; ++i)
            {
                if(i < InputSignal2.Samples.Count)
                    signal2_freq_domain.Add(new Complex(InputSignal2.Samples[i], 0));
                else
                    signal2_freq_domain.Add(new Complex(0, 0));
            }

            FFTrecursive(ref signal1_freq_domain, N, false);
            FFTrecursive(ref signal2_freq_domain, N, false);

            List<Complex> convolved_signal = new List<Complex>();
            for (int i = 0; i < N; ++i)
                convolved_signal.Add(signal1_freq_domain[i] * signal2_freq_domain[i]);

            FFTrecursive(ref convolved_signal, convolved_signal.Count, true);

            for (int i = 0; i < N; ++i)
                OutputConvolvedSignal.Samples.Add((float)(convolved_signal[i].Real / N));
        }
    }
}
