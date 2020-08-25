/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }



        // uding dft *****************************
        public List<float> dt(float[] a1, float[] a2)
        {
            int n = a1.Length;
            float sumc = 0;
            double r = 0;
            float[] x = new float[n];
            List<float> lc = new List<float>();
            List<float> ls = new List<float>();
            List<float> Lsum = new List<float>();
            List<float> Lsum2 = new List<float>();
            for (int i = 0; i < Lsum.Count; i++)
                Lsum.Add(0);



             float sums = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0)
                    {
                        sumc += a1[j];

                    }
                    else
                    {
                        r = ((i * j * 2 * Math.PI) / n);
                        
                            double c = (Math.Cos(r));
                            double si = (Math.Sin(r * -1));
                            sumc = sumc + (float)(a1[j] * c);
                            sums = sums + (float)(a1[j] * si );


                        

                    }
                    
                }
                lc.Add(sumc);
                ls.Add(sums);
                sumc = 0;
                sumc = 0;

            }
            int a = 1; 
            for (int i = ls.Count/2; i < ls.Count; i++)
            {  if (i == (ls.Count / 2))
                {
                    ls[i] = 0;
                }
            else
                {
                    ls[i] = -ls[i - (2 * a)] ;
                    a++;
                }

            }
            for (int i = 0; i < ls.Count; i++)
            {
                ls[i] = ls[i] * -1; 
            }
            for(int  i = 0; i < ls.Count; i ++)
            {
                lc[i] = lc[i] * lc[i];
                ls[i] = ls[i] * ls[i];

                Lsum2.Add(lc[i] + ls[i]); 
            }
           Lsum =idt(Lsum2); 
               
            return Lsum;
        }

        public List<float> sh(List<float> arr)
        {
            List < float > d = new List<float>();
            int ix = 0;
            // float s = arr[0];
            for (int i = 0; i < arr.Count - 1; i++)
            {
                d[ix] = arr[i + 1];
                ix++;
            }
            //demo[demo.Length - 1] = s;
            return d;
        }
        public List<float> idt(List<float> a1 )
        {
            List<float> l = new List<float>();


            double r = 0;
            double sumc = 0;
            double sums = 0;

             int n = a1.Count; 
            for (int i = 0; i < n; i++)
            {
                
                    for (int j = 0; j < n; j++)
                    {
                        r = ((i * j * 2 * Math.PI) / n);
                        double c = (Math.Cos(r));
                        double si = (Math.Sin(r));
                        sumc = sumc + (a1[j] * c);
                        sums = sums + (a1[j] * si * 1);

                    }
                    double ree = (sumc - sums);
                    float re = (float)(ree / n);
                    l.Add(re/n);
                    sumc = 0;
                    sums = 0;

                  

            }
            return l;

        }
        // *************************
      
        public float[] Rn(float[] items, int places)
        {
            int rotate = places;
            float[] results = new float[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                results[i] = items[(i + rotate) % items.Length];
            }
            return results;
        }
       public float[] nnorm(float[] arr1, float[] arr2)
        {
            float d;
            float sum = 0;
            float p1 = 0;
            float p2 = 0;
            int n = arr1.Length;
            float[] malt = new float[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += (arr1[j] * arr2[j]);
                    p1 += (float)Math.Pow(arr1[j], 2);
                    p2 += (float)Math.Pow(arr1[j], 2);
                }
                d = sum / n;
                malt[i] = d / ((float)Math.Sqrt((p1 * p2)) / n);
                //arr2 = shiftLeft(arr2);
                sum = 0;
                p1 = 0;
                p2 = 0;
            }

            return malt;

        }

        public override void Run()
        {
            Signal s1 = InputSignal1;
            Signal s2 = InputSignal2;
            int n = s1.Samples.Count;
            int n2 = InputSignal2.Samples.Count;
            /* int c = 0;
             if (n != n2)
             {
                 c = (n + n2) - 1;
                 for (int i = 0; i < (c - n); i++)
                 {
                     s1.Samples.Add(0);
                 }
                 for (int i = 0; i < (c - n2); i++)
                 {
                     InputSignal2.Samples.Add(0);
                 }
             }*/
/* List<float> lis1 = new List<float>();
 List<float> lis2 = new List<float>();
 List<float> l = new List<float>();
 List<float> norml = new List<float>();
 float[] l1 = new float[n];
 for (int i = 0; i < n; i++)
     l1[i] = InputSignal1.Samples[i];
 float[] cl1 = new float[n];
 for (int i = 0; i < n; i++)
     cl1[i] = s1.Samples[i];
 float[] l2 = new float[n2];
 for (int i = 0; i < n2; i++)
     l2[i] = InputSignal2.Samples[i];
 float[] rn = new float[n];



 l = dt(l1, l2);
 OutputNormalizedCorrelation = l; 
}
}
}   
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        List<float> inp_of_ifft = new List<float>();
        public List<float> clculatecorrelation(Signal inputs1, Signal inputs2)
        {
            List<float> Results = new List<float>();
            // find fft l signal el 2ola w el tania 

            FastFourierTransform fastfun = new FastFourierTransform();
            ///// signal 1 
            ///
            fastfun.InputTimeDomainSignal = inputs1;
            fastfun.InputSamplingFrequency = 4;
            fastfun.Run();
            Signal outoffft1 = new Signal(false, new List<float>(), new List<float>(), new List<float>()); // ناتج fft1 
            outoffft1 = fastfun.OutputFreqDomainSignal;
            /////// 
            ///
            //////signal 2 
            ///
            fastfun.InputTimeDomainSignal = inputs1;
            fastfun.InputSamplingFrequency = 4;
            fastfun.Run();
            Signal outoffft2 = new Signal(false, new List<float>(), new List<float>(), new List<float>());

            outoffft2 = fastfun.InputTimeDomainSignal = inputs2;
            /////////////// 
            /////// 
            ///هنجيب الكونجكت بتاع السيجنال الاولي  هنضرب الايمجن ف -1 
            ///
            Complex result_of_multyplay2signals = new Complex();
            for (int i = 0; i < outoffft1.FrequenciesAmplitudes.Count; i++)
            {
                double Real1 = outoffft1.FrequenciesAmplitudes[i] * Math.Cos(outoffft1.FrequenciesPhaseShifts[i]);
                double Imag1 = outoffft1.FrequenciesAmplitudes[i] * Math.Sin(outoffft1.FrequenciesPhaseShifts[i]);

                double Real2 = outoffft2.FrequenciesAmplitudes[i] * Math.Cos(outoffft2.FrequenciesPhaseShifts[i]);
                double Imag2 = outoffft2.FrequenciesAmplitudes[i] * Math.Sin(outoffft2.FrequenciesPhaseShifts[i]);
                double Real = Real1 * Real2;
                double Imag = -1 * Imag1 * Imag2;
                inp_of_ifft.Add((float)(Real + Imag));
                Complex x = new Complex(Real, Imag);
                result_of_multyplay2signals = new Complex(x.Real, x.Imaginary);

            }
            ///////////////////// find ifft 
            ///
            InverseFastFourierTransform invfastfun = new InverseFastFourierTransform();
            for (int i = 0; i < inp_of_ifft.Count; i++)
                invfastfun.InputFreqDomainSignal.FrequenciesAmplitudes[i] = inp_of_ifft[i];
            invfastfun.Run();




            for (int i = 0; i < invfastfun.OutputTimeDomainSignal.Samples.Count; i++)
            {
                float Result = (float)((double)(invfastfun.OutputTimeDomainSignal.Samples[i] / invfastfun.OutputTimeDomainSignal.Samples.Count));
                if (inputs1 != inputs2)
                    Results.Add((float)Math.Round(Result, 8));
                else
                    Results.Add(Result);
            }
            return Results;
        }

        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            if (InputSignal2 == null)
                InputSignal2 = InputSignal1;
            //IF None Periodic Size = Max(signal1,signal2)
            if (!InputSignal1.Periodic)
            {
                for (int i = InputSignal1.Samples.Count; i < InputSignal2.Samples.Count; i++)
                    InputSignal1.Samples.Add(0);
                for (int i = InputSignal2.Samples.Count; i < InputSignal1.Samples.Count; i++)
                    InputSignal2.Samples.Add(0);
            }
            //If Periodic Size =(signal1+signal2-1)
            else
            {
                if (InputSignal1.Samples.Count != InputSignal2.Samples.Count)
                {
                    int Size = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
                    for (int i = InputSignal1.Samples.Count; i < Size; i++)
                        InputSignal1.Samples.Add(0);
                    for (int i = InputSignal2.Samples.Count; i < Size; i++)
                        InputSignal2.Samples.Add(0);
                }
            }

            List<float> convlution1 = new List<float>();
            List<float> convlution2 = new List<float>();


            //  OutputNonNormalizedCorrelation = clculatecorrelation(InputSignal1, InputSignal2);

            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                convlution1.Add((float)Math.Pow(InputSignal1.Samples[i], 2));
                convlution1.Add((float)Math.Pow(InputSignal2.Samples[i], 2));
            }


            float d = (float)Math.Sqrt(convlution1.Sum() * convlution2.Sum());

            for (int j = 0; j < convlution1.Count; j++)
                OutputNormalizedCorrelation.Add(OutputNonNormalizedCorrelation[j] * InputSignal1.Samples.Count / d);


        }
    }
}