#region using

using System;

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    public class SPCMath
    {
        /// <summary>
        /// Quick Sort
        /// </summary>
        /// <param name="isdesc"></param>
        /// <param name="_values"></param>
        public static void Sort(bool isdesc, double[] _values)
        {
            if (_values.Length == 0)
            {
                return;
            }

            QuickSort.Sort(_values, 0, _values.Length - 1, isdesc);
        }

        #region CDF
        /// <summary>
        /// Normal distribution probabilities accurate to 1.e-15.
        /// Z = no. of standard deviations from the mean.
        /// P, Q = probabilities to the left and right of Z.   P + Q = 1.
        /// PDF = the probability density.
        /// Based upon algorithm 5666 for the error function, from:
        /// Hart, J.F. et al, 'Computer Approximations', Wiley 1968
        /// </summary>
        /// <param name="fZ">Z value.</param>
        /// <returns>The cumulative distribution function value</returns>		
		public static double CdfNormal(double fZ)
        {
            /* Initialized data */
            double[] pfP = new double[] { 220.2068679123761, 221.2135961699311, 112.0792914978709, 33.912866078383, 6.37396220353165, .7003830644436881, .03526249659989109 };
            double[] pfQ = new double[] { 440.4137358247522, 793.8265125199484, 637.3336333788311, 296.5642487796737, 86.78073220294608, 16.06417757920695, 1.755667163182642, .08838834764831844 };
            double cutoff = 7.071, root2pi = 2.506628274631001;

            /* System generated locals */
            double zabs, expntl, pdf, p;

            zabs = System.Math.Abs(fZ); //Math.abs(d) == c++ fabs(d)

            /* 	|Z| > 37. */

            if (zabs > 37.0)
            {
                if (fZ > 0.0)
                    return 1.0;
                else
                    return 0.0;
            }

            /* 	|Z| <= 37. */

            /* Computing 2nd power */
            expntl = System.Math.Exp((-0.5) * zabs * zabs);
            pdf = expntl / root2pi;

            if (zabs < cutoff)
            {
                /* 	|Z| < CUTOFF = 10/sqrt(2). */
                p = expntl * ((((((pfP[6] * zabs + pfP[5]) * zabs + pfP[4]) * zabs + pfP[3]) * zabs + pfP[2]) * zabs + pfP[1]) * zabs + pfP[0]) / (((((((pfQ[7] * zabs + pfQ[6]) * zabs + pfQ[5]) * zabs + pfQ[4]) * zabs + pfQ[3]) * zabs + pfQ[2]) * zabs + pfQ[1]) * zabs + pfQ[0]);
            }
            else
            {
                /* 	|Z| >= CUTOFF. */
                p = pdf / (zabs + 1.0 / (zabs + 2.0 / (zabs + 3.0 / (zabs + 4.0 / (zabs + .65)))));
            }

            if (fZ < 0.0)
                return p;
            else
                return 1.0 - p;
        }
        #endregion CDF
    }
}
