using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 누적분포함수
    /// </summary>
    public class SPCCdf
    {
        /// <summary>
        /// 역 CDF
        /// Quantile function (Inverse CDF) for the normal distribution.
        /// </summary>
        /// <param name="p">Probability.</param>
        /// <param name="mu">Mean of normal distribution.</param>
        /// <param name="sigma">Standard deviation of normal distribution.</param>
        /// <param name="isLowerTail">If true, probability is P[X <= x], otherwise P[X > x].</param>
        /// <param name="isLogValues">If true, probabilities are given as log(p).</param>
        /// <returns>P[X <= x] where x ~ N(mu,sigma^2)</returns>
        /// <remarks>In your case, you want mu=0.0, sigma=1.0, lower_tail=true, log_p=false. </remarks>
        public static double QNorm(double p, double mu, double sigma, bool isLowerTail, bool isLogValues)
        {
            if (double.IsNaN(p) || double.IsNaN(mu) || double.IsNaN(sigma)) return (p + mu + sigma);
            double ans;
            bool isBoundaryCase = R_Q_P01_boundaries(p, double.NegativeInfinity, double.PositiveInfinity, isLowerTail, isLogValues, out ans);
            if (isBoundaryCase) return (ans);
            if (sigma < 0) return (double.NaN);
            if (sigma == 0) return (mu);

            double p_ = R_DT_qIv(p, isLowerTail, isLogValues);
            double q = p_ - 0.5;
            double r, val;

            if (Math.Abs(q) <= 0.425)  // 0.075 <= p <= 0.925
            {
                r = .180625 - q * q;
                val = q * (((((((r * 2509.0809287301226727 +
                           33430.575583588128105) * r + 67265.770927008700853) * r +
                         45921.953931549871457) * r + 13731.693765509461125) * r +
                       1971.5909503065514427) * r + 133.14166789178437745) * r +
                     3.387132872796366608)
                / (((((((r * 5226.495278852854561 +
                         28729.085735721942674) * r + 39307.89580009271061) * r +
                       21213.794301586595867) * r + 5394.1960214247511077) * r +
                     687.1870074920579083) * r + 42.313330701600911252) * r + 1.0);
            }
            else
            {
                r = q > 0 ? R_DT_CIv(p, isLowerTail, isLogValues) : p_;
                r = Math.Sqrt(-((isLogValues && ((isLowerTail && q <= 0) || (!isLowerTail && q > 0))) ? p : Math.Log(r)));

                if (r <= 5)              // <==> min(p,1-p) >= exp(-25) ~= 1.3888e-11
                {
                    r -= 1.6;
                    val = (((((((r * 7.7454501427834140764e-4 +
                            .0227238449892691845833) * r + .24178072517745061177) *
                          r + 1.27045825245236838258) * r +
                         3.64784832476320460504) * r + 5.7694972214606914055) *
                       r + 4.6303378461565452959) * r +
                      1.42343711074968357734)
                     / (((((((r *
                              1.05075007164441684324e-9 + 5.475938084995344946e-4) *
                             r + .0151986665636164571966) * r +
                            .14810397642748007459) * r + .68976733498510000455) *
                          r + 1.6763848301838038494) * r +
                         2.05319162663775882187) * r + 1.0);
                }
                else                     // very close to  0 or 1 
                {
                    r -= 5.0;
                    val = (((((((r * 2.01033439929228813265e-7 +
                            2.71155556874348757815e-5) * r +
                           .0012426609473880784386) * r + .026532189526576123093) *
                         r + .29656057182850489123) * r +
                        1.7848265399172913358) * r + 5.4637849111641143699) *
                      r + 6.6579046435011037772)
                     / (((((((r *
                              2.04426310338993978564e-15 + 1.4215117583164458887e-7) *
                             r + 1.8463183175100546818e-5) * r +
                            7.868691311456132591e-4) * r + .0148753612908506148525)
                          * r + .13692988092273580531) * r +
                         .59983220655588793769) * r + 1.0);
                }
                if (q < 0.0) val = -val;
            }

            return (mu + sigma * val);
        }
        /// <summary>
        /// RQ함수
        /// </summary>
        /// <param name="p"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="isLowerTail"></param>
        /// <param name="isLogValues"></param>
        /// <param name="ans"></param>
        /// <returns></returns>
        private static bool R_Q_P01_boundaries(double p, double left, double right, bool isLowerTail, bool isLogValues, out double ans)
        {
            if (isLogValues)
            {
                if (p > 0.0)
                {
                    ans = double.NaN;
                    return (true);
                }
                if (p == 0.0)
                {
                    ans = isLowerTail ? right : left;
                    return (true);
                }
                if (p == double.NegativeInfinity)
                {
                    ans = isLowerTail ? left : right;
                    return (true);
                }
            }
            else
            {
                if (p < 0.0 || p > 1.0)
                {
                    ans = double.NaN;
                    return (true);
                }
                if (p == 0.0)
                {
                    ans = isLowerTail ? left : right;
                    return (true);
                }
                if (p == 1.0)
                {
                    ans = isLowerTail ? right : left;
                    return (true);
                }
            }
            ans = double.NaN;
            return (false);
        }
        /// <summary>
        /// RQ 함수에서 사용
        /// </summary>
        /// <param name="p"></param>
        /// <param name="isLowerTail"></param>
        /// <param name="isLogValues"></param>
        /// <returns></returns>
        private static double R_DT_qIv(double p, bool isLowerTail, bool isLogValues)
        {
            return (isLogValues ? (isLowerTail ? Math.Exp(p) : -ExpM1(p)) : R_D_Lval(p, isLowerTail));
        }
        /// <summary>
        /// RQ 함수에서 사용
        /// </summary>
        /// <param name="p"></param>
        /// <param name="isLowerTail"></param>
        /// <param name="isLogValues"></param>
        /// <returns></returns>
        private static double R_DT_CIv(double p, bool isLowerTail, bool isLogValues)
        {
            return (isLogValues ? (isLowerTail ? -ExpM1(p) : Math.Exp(p)) : R_D_Cval(p, isLowerTail));
        }
        /// <summary>
        /// RQ 함수에서 사용
        /// </summary>
        /// <param name="p"></param>
        /// <param name="isLowerTail"></param>
        /// <returns></returns>
        private static double R_D_Lval(double p, bool isLowerTail)
        {
            return isLowerTail ? p : 0.5 - p + 0.5;
        }
        /// <summary>
        /// RQ 함수에서 사용
        /// </summary>
        /// <param name="p"></param>
        /// <param name="isLowerTail"></param>
        /// <returns></returns>
        private static double R_D_Cval(double p, bool isLowerTail)
        {
            return isLowerTail ? 0.5 - p + 0.5 : p;
        }
        /// <summary>
        /// RQ 함수에서 사용
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double ExpM1(double x)
        {
            if (Math.Abs(x) < 1e-5)
                return x + 0.5 * x * x;
            else
                return Math.Exp(x) - 1.0;
        }
    }

    
    /// <summary>
    /// 역 확률누적분포함수
    /// </summary>
    public class SPCPdf
    {
        public Chart ChartMath = new Chart();
        public double NORMSINV(double nValue, double nDivide, int nDigit = 2)
        {
            double result = 0;

            //수식                설명                                                       결과
            //= NORMSINV(0.9088)  확률이 0.9088인 표준 정규 누적 분포의 역함수 값을 반환합니다.  1.3334
            //double result1 = ChartMath.DataManipulator.Statistics.InverseNormalDistribution(0.9088);

            Random r = new Random();
            double rValue = 0;
            for (int i = 1; i < 100; i++)
            {
                rValue = r.NextDouble();
                if (rValue > 0 && rValue < 1)
                {
                    break;
                }
            }

            double Nor1 = ChartMath.DataManipulator.Statistics.InverseNormalDistribution(rValue);


            //= ROUNDDOWN(191 + NORMSINV(RAND()) / 0.05, 2) / 1000  = 0.190
            double data1 = Nor1 / nDivide;
            double data2 = nValue + data1;
            double dataRound = Math.Round(data2, nDigit);
            result = dataRound / 1000;

            //Console.WriteLine(result);

            return result;

        }

        public double[] NORMSINVary(double nValue, double nDivide, int Count, int nDigit = 2)
        {
            double[] result = new double[Count];

            //수식                설명                                                       결과
            //= NORMSINV(0.9088)  확률이 0.9088인 표준 정규 누적 분포의 역함수 값을 반환합니다.  1.3334
            //double result1 = ChartMath.DataManipulator.Statistics.InverseNormalDistribution(0.9088);

            Random r = new Random();
            double rValue = 0;
            double data1 = 0;
            double data2 = 0;
            double dataRound = 0;
            double Nor1 = 0;
            for (int i = 1; i < Count; i++)
            {
                rValue = r.NextDouble();
                if (rValue > 0 && rValue < 1)
                {
                    Nor1 = ChartMath.DataManipulator.Statistics.InverseNormalDistribution(rValue);
                    data1 = Nor1 / nDivide;
                    data2 = nValue + data1;
                    dataRound = Math.Round(data2, nDigit);
                    result[i] = dataRound / 1000;
                }
            }

            return result;

        }
    }
}
