#region using

#endregion

namespace Micube.SmartMES.Commons.SPCLibrary
{
    public class SPCValue
    {
        /// <summary>
        /// Array of values
        /// </summary>
        protected internal double[] values = null;

        /// <summary>Count of datas</summary>
        private int nct = 0;
        /// <summary>Sum of datas</summary>
        private double sum = 0;
        /// <summary>Average of datas</summary>		
        private double mean;
        /// <summary>Variance of datas</summary>		
        private double variance;
        /// <summary>Standard deviation</summary>
        private double stddev;
        /// <summary>Standard error</summary>
        private double stderr;
        /// <summary>Sum of square</summary>
        private double ssquare = 0;
        /// <summary>Maximum value</summary>
        private double max;
        /// <summary>Minimum value</summary>
        private double min;
        /// <summary>Range</summary>
        private double range;
        /// <summary>Coefficient variance</summary>
        private double coeffvariance;
        /// <summary>Skewness</summary>
		private double skewness;
        /// <summary>Kurtosis</summary>
        private double kurtosis;
        /// <summary>Max frequency</summary>
        private double maxfrequency;
        /// <summary>Median</summary>
        private double median;
        /// <summary>Q1</summary>
        private double q1;
        /// <summary>Q3</summary>
        private double q3;
        /// <summary>IQR</summary>
        private double iqr;
        /// <summary>Upper outlier</summary>
        private double uoutlier;
        /// <summary>Lower outlier</summary>
        private double loutlier;

        private void InitBlock()
        {
            mean = SpcLimit.MIN;
            variance = SpcLimit.MIN;
            stddev = SpcLimit.MIN;
            stderr = SpcLimit.MIN;
            max = SpcLimit.MIN;
            min = SpcLimit.MIN;
            range = SpcLimit.MIN;
            coeffvariance = SpcLimit.MIN;
            skewness = SpcLimit.MIN;
            kurtosis = SpcLimit.MIN;
            maxfrequency = SpcLimit.MIN;
            median = SpcLimit.MIN;
            q1 = SpcLimit.MIN;
            q3 = SpcLimit.MIN;
            iqr = SpcLimit.MIN;
            uoutlier = SpcLimit.MIN;
            loutlier = SpcLimit.MIN;
        }
        /// <summary>
        /// Set Array of values
        /// </summary>
        /// <returns>Array of values</returns>
        public void SetValues(double[] data)
        {
            values = data;
        }
        /// <summary>
        /// Set Count of datas
        /// </summary>
        /// <returns>Count of datas</returns>
        public void setNct(int data)
        {
            nct = data;
        }
        public void SetMax(double data)
        {
            max = data;
        }
        public void SetMin(double data)
        {
            min = data;
        }
        public void SetRange(double data)
        {
            range = data;
        }

        public void SetMean(double data)
        {
            mean = data;
        }
        public void SetStddev(double data)
        {
            stddev = data;
        }

        /// <summary>
        /// Get Array of values
        /// </summary>
        /// <returns>Array of values</returns>
        public virtual double[] getvalues()
        {
            return values;
        }
        /// <summary>
        /// Get size of the datas
        /// </summary>
        /// <returns>Count of datas</returns>
        public int getsize()
        {
            return nct;
        }

        /// <summary>
        /// Get sum value of the datas
        /// </summary>
        /// <returns>Sum of datas</returns>
		public virtual double getsum()
        {
            return sum;
        }

        /// <summary>
        /// Get mean value of the datas
        /// </summary>
        /// <returns>Average of datas</returns>
		public virtual double getmean()
        {
            return mean;
        }

        /// <summary>
        /// Get variance
        /// </summary>
        /// <returns>Variance</returns>
		public virtual double getvariance()
        {
            return variance;
        }

        /// <summary>
        /// Get standard deviation
        /// </summary>
        /// <returns>standard deviation</returns>
		public virtual double getstddev()
        {
            return stddev;
        }

        /// <summary>
        /// Get standard error
        /// </summary>
        /// <returns>standard error</returns>
		public virtual double getstderr()
        {
            return stderr;
        }

        /// <summary>
        /// Get sum of square
        /// </summary>
        /// <returns>sum of square</returns>
		public virtual double getssquare()
        {
            return ssquare;
        }

        /// <summary>
        /// Get maximum value
        /// </summary>
        /// <returns>Maximum value</returns>
		public virtual double getmax()
        {
            return max;
        }

        /// <summary>
        /// Get minimum value
        /// </summary>
        /// <returns>Minimum value</returns>
		public virtual double getmin()
        {
            return min;
        }

        /// <summary>
        /// Get range
        /// </summary>
        /// <returns>Range</returns>
		public virtual double getrange()
        {
            return range;
        }

        /// <summary>
        /// Get Coefficient variance
        /// </summary>
        /// <returns>Coefficient variance</returns>
		public virtual double getcoeffvariance()
        {
            return coeffvariance;
        }

        /// <summary>
        /// Get skewnesses
        /// </summary>
        /// <returns>Skewnesses</returns>
		public virtual double getskewness()
        {
            return skewness;
        }

        /// <summary>
        /// Get kurtosis.
        /// </summary>
        /// <returns>Kurtosis</returns>
		public virtual double getkurtosis()
        {
            return kurtosis;
        }

        /// <summary>
        /// Get max frequency.
        /// </summary>
        /// <returns>Max frequency.</returns>
		public virtual double getmaxfrequency()
        {
            return maxfrequency;
        }

        /// <summary>
        /// Get median.
        /// </summary>
        /// <returns>Median</returns>
		public virtual double getmedian()
        {
            return median;
        }

        /// <summary>
        /// Get Q1.
        /// </summary>
        /// <returns>Q1</returns>
		public virtual double getq1()
        {
            return q1;
        }

        /// <summary>
        /// Get Q3.
        /// </summary>
        /// <returns>Q3</returns>
		public virtual double getq3()
        {
            return q3;
        }

        /// <summary>
        /// Get IQR.
        /// </summary>
        /// <returns>IQR.</returns>
		public virtual double getiqr()
        {
            return iqr;
        }

        /// <summary>
        /// Get lower outlier
        /// </summary>
        /// <returns>Lower outlier</returns>
		public virtual double getloutlier()
        {
            return loutlier;
        }

        /// <summary>
        /// Get upper outlier
        /// </summary>
        /// <returns>Upper outlier</returns>
		public virtual double getuoutlier()
        {
            return uoutlier;
        }

        /// <summary>
        /// Sample Count
        /// </summary>
        /// <returns>Upper outlier</returns>
        public virtual int getnct()
        {
            return nct;
        }
    }

    /// <summary>
    /// Historam 결과 저장.
    /// </summary>
    public class ResultHistogram
    {
        /// <summary> 급의 수</summary>
        public int ninterval = 0;
        /// <summary> 최대 급의 수</summary>
        public int maxinterval = 0;
        /// <summary> 급의 너비</summary>
        public double intervalwidth;
        /// <summary> 급</summary>
        public double[] intervals = null;
        /// <summary> 도수 (급 내 자료의 개수)</summary>
        public int[] frequencies = null;
     
    }
}
