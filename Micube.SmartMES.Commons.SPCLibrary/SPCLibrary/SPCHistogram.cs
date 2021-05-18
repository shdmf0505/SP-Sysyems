#region using
using System.Linq;
#endregion

using System.Data;

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 히스도그램 처리
    /// </summary>
    public class SPCHistogram
    {
        private void InitBlock()
        {
            intervalWidth = SpcLimit.MIN;
        }

        /// <summary> 
        /// 급의 수
        /// </summary>
        private int nInterval = 0;

        /// <summary> 
        /// 최대 급의 수
        /// </summary>
        private int maxInterval = 0;

        /// <summary> 
        /// 급의 너비
        /// </summary>
        private double intervalWidth;

        /// <summary> 
        /// 급
        /// </summary>
        private double[] intervals = null;

        /// <summary> 
        /// 도수 (급 내 자료의 개수)
        /// </summary>
        private int[] frequencies = null;

        /// <summary> 
        /// 데이터 참조자
        /// </summary>
        private SPCValue stdata = null;

        /// <summary> 
        /// 자릿 수	: default 소수점 자리수 4번째 까지 유효
        /// </summary>
        private readonly int digit = -4;

        public SPCHistogramAnalysisDataTable dtAnalysis = null;

        //public SPCHistogram()
        //{
        //    InitBlock();
        //}


        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SPCHistogram"/> is reclaimed by garbage collection.
        /// </summary>
		~SPCHistogram()
        {

        }

        /// <summary>
        /// SPCHistogram
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_maxinterval"></param>
        /// <param name="_digit"></param>
        /// <returns></returns>
        public static SPCHistogram Create(SPCValue _data, int _maxinterval, int _digit)
        {
            SPCHistogram c = new SPCHistogram();
            c.InitBlock();

            try
            {
                c.dtAnalysis = SPCHistogramAnalysisDataTable.Create();

                if (_data == null)
                {
                    new System.Exception("Data is null.");
                }

                if (_maxinterval < 0)
                {
                    new System.Exception("Interval is not below zero.");
                }

                c.stdata = _data;
                c.maxInterval = _maxinterval;
                c.MakeHistogram();
            }
            catch (System.Exception e)
            {
                string dd = e.Message.ToString();
            }

            return c;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistogramNormalDistributionLine"/> class.
        /// </summary>
        /// <param name="_data">StatisticalValues.</param>
        /// <param name="_maxinterval">Max interval.</param>
        /// <param name="_digit">Digit.</param>
        public SPCValue SPCHistogramCreate(SPCValue _data, int _maxinterval, int _digit, out ResultHistogram resultData)
        {
            InitBlock();
            resultData = new ResultHistogram();

            try
            {
                if (_data == null)
                {
                    new System.Exception("Data is null.");
                }

                if (_maxinterval < 0)
                {
                    new System.Exception("Interval is not below zero.");
                }

                stdata = _data;
                maxInterval = _maxinterval;
                MakeHistogram();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            resultData.ninterval = nInterval;
            resultData.maxinterval = maxInterval;
            resultData.intervalwidth = intervalWidth;
            resultData.intervals = intervals;
            resultData.frequencies = frequencies;

            return stdata;
        }

        /// <summary> 정규 확률 밀도함수에 의해 y값 계산</summary>
        private void MakeHistogram()
        {
            // sorting
            double[] values = new double[stdata.getvalues().Length];
            stdata.getvalues().CopyTo(values, 0);
            SPCMath.Sort(false, values);

            // 급의 수
            int n = (int)System.Math.Ceiling(System.Math.Sqrt(stdata.getsize()));

            if (maxInterval > 0 && n > maxInterval)
            {
                nInterval = maxInterval;
            }
            else
            {
                nInterval = n+3;
            }

            // 급의 너비
            if (digit != -1)
            {
                intervalWidth = System.Math.Ceiling((stdata.getrange() / this.nInterval) * System.Math.Pow(10, (-1) * (digit + 1))) / System.Math.Pow(10, (-1) * (digit + 1));
            }
            else
            {
                intervalWidth = System.Math.Ceiling(stdata.getrange() / this.nInterval);
            }

            // 급
            intervals = new double[nInterval];
            for (int i = 0; i < nInterval; i++)
            {
                if (i == 0)
                {
                    intervals[i] = values[0];
                }
                else
                {
                    intervals[i] = values[0] + intervalWidth * i;
                }
            }

            // 도수 (급 내 자료의 개수)  //정렬이 되어 있어야 한다.
            frequencies = new int[nInterval];
            //initialize
            for (int i = 0; i < frequencies.Length; i++)
            {
                frequencies[i] = 0;
            }

            int csum = 0;
            for (int i = 0; i < stdata.getsize(); i++)
            {
                for (int j = 0; j < frequencies.Length; j++)
                {
                    if (values[i] >= intervals[j] && values[i] < intervals[j] + intervalWidth)
                    {
                        frequencies[j]++;
                        csum++;
                    }
                }
            }

            if (csum < stdata.getsize())
            {
                frequencies[frequencies.Length - 1] += stdata.getsize() - csum;
            }

            //데이터 해제
            values = null;
        }

        /// <summary>
        /// Get size of level.
        /// </summary>
        /// <returns>size of level.</returns>
		public virtual int GetIntervalSize() => nInterval;

        /// <summary>
        /// Get interval width.
        /// </summary>
        /// <returns>Interval width.</returns>
		public virtual double GetIntervalWidth() => intervalWidth;

        /// <summary>
        /// Get level value the specified index.
        /// </summary>
        /// <param name="index">Index of level</param>
        /// <returns>level value.</returns>
		public virtual double GetInterval(int index) => index > -1 && index < nInterval ? intervals[index] : SpcLimit.MIN;

        /// <summary>
        /// Get level boundary value.
        /// </summary>
        /// <param name="index">Index of level</param>
        /// <returns>level boundary value.</returns>
		public virtual double GetIntervalLimit(int index)
        {
            double dnum = 1;

            if (digit < 0)
            {
                dnum = 1 / (-10) * digit;
            }
            else if (digit == 0)
            {
                dnum = 1;
            }
            else
            {
                dnum = 10 * digit;
            }

            if (index > -1 && index < nInterval)
            {
                return intervals[index] + intervalWidth - dnum;
            }

            return SpcLimit.MIN;
        }

        /// <summary>
        /// Get level frequency value.
        /// </summary>
        /// <param name="index">Index of level</param>
        /// <returns>level frequency value.</returns>
		public virtual int GetFrequency(int index) => index > -1 && index < nInterval ? frequencies[index] : -1;

        /// <summary>
        /// Get level center value.
        /// </summary>
        /// <param name="index">Index of level</param>
        /// <returns>level center value.</returns>
		public virtual double GetCenter(int index) => index > -1 && index < nInterval ? intervals[index] + intervalWidth / 2 : SpcLimit.MIN;

        /// <summary>
        /// Get y value of normal distribution line.
        /// </summary>
        /// <param name="xvalue">x value</param>
        /// <param name="stddev">standard deviation.</param>
        /// <returns>y value</returns>
		public virtual double GetNormaly(double xvalue, double stddev)
        {
            double dparam = -(System.Math.Pow((xvalue - stdata.getmean()), 2) / (2 * System.Math.Pow(stddev, 2)));
            double fx = 1 / (stddev * System.Math.Sqrt(2 * System.Math.PI)) * System.Math.Exp(dparam);

            return fx * stdata.getsize() * intervalWidth;
        }

        /// <summary>
        /// Get center value of normal distribution line.
        /// </summary>
        /// <param name="stddev">standard deviation.</param>
        /// <returns>center value</returns>
		public virtual double GetNormalCenter(double stddev)
        {
            int n = 0;
            double sx = stdata.getmean() - 10 * stddev;
            double ex = stdata.getmean() + 10 * stddev;
            double max = 10000;
            double iv = (ex - sx) / max;
            double x = sx;
            double prey = SpcLimit.MIN;

            while (n < max)
            {
                double y = this.GetNormaly(x, stdata.getstddev());
                if (prey != SpcLimit.MIN && prey >= y)
                    break;
                prey = y;
                x += iv;
                n++;
            }

            if (prey != SpcLimit.MIN)
            {
                return x;
            }

            return SpcLimit.MIN;
        }

        /// <summary>
        /// Get left 3sigma value of normal distribution line.
        /// </summary>
        /// <param name="center">center value.</param>
        /// <param name="stddev">standard deviation.</param>
        /// <returns>left 3sigma value</returns>
		public virtual double GetNormalLeft(double center, double stddev) => center - (3 * stddev);

        /// <summary>
        /// Get right 3sigma value of normal distribution line.
        /// </summary>
        /// <param name="center">center value.</param>
        /// <param name="stddev">standard deviation.</param>
        /// <returns>right 3sigma value</returns>
		public virtual double GetNormalRight(double center, double stddev) => center + (3 * stddev);


    }//end

    /// <summary>
    /// Histogram 분석값 저장 DataTable
    /// </summary>
    public class SPCHistogramAnalysisDataTable
    {
        /// <summary>
        /// Bar 값
        /// </summary>
        public DataTable dtBar;
        /// <summary>
        /// 내부 값
        /// </summary>
        public DataTable dtWithin;
        /// <summary>
        /// 전체 값
        /// </summary>
        public DataTable dtTotal;

        /// <summary>
        /// Histogram 분석 집계값 저장.
        /// </summary>
        public HistogramAnalysisTotalize totalize;

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static SPCHistogramAnalysisDataTable Create()
        {
            SPCHistogramAnalysisDataTable c = new SPCHistogramAnalysisDataTable();
            c.dtBar = c.CreateAnalysisDataTable("dtBar");
            c.dtWithin = c.CreateAnalysisDataTable("dtWithin");
            c.dtTotal = c.CreateAnalysisDataTable("dtTotal");
            c.totalize = HistogramAnalysisTotalize.Create();
            return c;
        }

        /// <summary>
        /// Histogram 분석값 저장 DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable CreateAnalysisDataTable(string tableName = "dtAnalysisHistogram")
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = tableName;

            //Filed
            dataTable.Columns.Add("TempId", typeof(long));
            dataTable.Columns.Add("GroupLabel", typeof(string));
            dataTable.Columns.Add("Label", typeof(string));
            dataTable.Columns.Add("nLabelLng", typeof(long));
            dataTable.Columns.Add("nLabelDbl", typeof(double));
            dataTable.Columns.Add("nValueLng", typeof(long));
            dataTable.Columns.Add("nValueDbl", typeof(double));
            return dataTable;
        }

        /// <summary>
        /// 분석 집계 자료 반환.
        /// </summary>
        /// <param name="analysisDataTable"></param>
        /// <returns></returns>
        public HistogramAnalysisTotalize GetAnalysisTotalize(SPCHistogramAnalysisDataTable analysisDataTable)
        {
            HistogramAnalysisTotalize c = HistogramAnalysisTotalize.Create();
            HistogramTotalize returnType;
            returnType = HistogramTotalize.Bar;
            c.Bar = GetAnalysisTotalizeValue(analysisDataTable.dtBar, returnType);
            returnType = HistogramTotalize.Within;
            c.WithIn = GetAnalysisTotalizeValue(analysisDataTable.dtWithin, returnType);
            returnType = HistogramTotalize.Total;
            c.Total = GetAnalysisTotalizeValue(analysisDataTable.dtTotal, returnType);

            return c;
        }

        /// <summary>
        /// 분석값 집계값 반환.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public TotalizeiTem GetAnalysisTotalizeValue(DataTable dataTable, HistogramTotalize returnType)
        {
            TotalizeiTem item = TotalizeiTem.Create();
            string xAxisFieldType = "";
            string yAxisFieldType = "";
            item.Type = returnType.ToString();
            item.returnType = returnType;
            //dataTable.Columns.Add("nLabelInt", typeof(long));
            //dataTable.Columns.Add("nLabelDbl", typeof(double));
            //dataTable.Columns.Add("nValueInt", typeof(long));
            //dataTable.Columns.Add("nValueDbl", typeof(double));
            var rowDatas = dataTable.AsEnumerable();
            var query = from f in rowDatas
                        group f by f.Field<string>("GroupLabel") into g
                        select new
                        {
                            pk = g.Key,
                            //nLabelMaxX = g.Max(s => s.Field<long>("nLabelLng")),
                            //nLabelMinX = g.Min(s => s.Field<long>("nLabelLng")),
                            nLabelMaxXd = g.Max(s => s.Field<double>("nLabelDbl")),
                            nLabelMinXd = g.Min(s => s.Field<double>("nLabelDbl")),

                            //nValueMaxY = g.Max(s => s.Field<long>("nValueLng")),
                            //nValueMinY = g.Min(s => s.Field<long>("nValueLng")),
                            nValueMaxYd = g.Max(s => s.Field<double>("nValueDbl")),
                            nValueMinYd = g.Min(s => s.Field<double>("nValueDbl")),

                            nCount = g.Count()
                        };
            foreach (var q in query)
            {

                switch (returnType)
                {
                    case HistogramTotalize.Bar:
                        item.ndXMax = q.nLabelMaxXd;
                        item.ndXMin = q.nLabelMinXd;
                        item.ndYMax = q.nValueMaxYd;
                        item.ndYMin = q.nValueMinYd;
                        break;
                    case HistogramTotalize.Within:
                        item.ndXMax = q.nLabelMaxXd;
                        item.ndXMin = q.nLabelMinXd;
                        item.ndYMax = q.nValueMaxYd;
                        item.ndYMin = q.nValueMinYd;
                        break;
                    case HistogramTotalize.Total:
                        item.ndXMax = q.nLabelMaxXd;
                        item.ndXMin = q.nLabelMinXd;
                        item.ndYMax = q.nValueMaxYd;
                        item.ndYMin = q.nValueMinYd;
                        break;
                    default:
                        item.ndXMax = q.nLabelMaxXd;
                        item.ndXMin = q.nLabelMinXd;
                        item.ndYMax = q.nValueMaxYd;
                        item.ndYMin = q.nValueMinYd;
                        break;
                }
            }

            return item;
        }


    }//class end

    /// <summary>
    /// Histogram 분석 집계값 저장.
    /// </summary>
    public class HistogramAnalysisTotalize
    {
        /// <summary>
        /// Bar 집계
        /// </summary>
        public TotalizeiTem Bar;
        /// <summary>
        /// WithIn 집계
        /// </summary>
        public TotalizeiTem WithIn;
        /// <summary>
        /// Total 집계
        /// </summary>
        public TotalizeiTem Total;
        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static HistogramAnalysisTotalize Create()
        {
            HistogramAnalysisTotalize c = new HistogramAnalysisTotalize();
            c.Bar = TotalizeiTem.Create();
            c.WithIn = TotalizeiTem.Create();
            c.Total = TotalizeiTem.Create();
            return c;
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public HistogramAnalysisTotalize CopyDeep()
        {
            HistogramAnalysisTotalize cs = HistogramAnalysisTotalize.Create();
            cs.Bar = this.Bar.CopyDeep();
            cs.WithIn = this.WithIn.CopyDeep();
            cs.Total = this.Total.CopyDeep();
            return cs;
        }

    }//end

    /// <summary>
    /// 집계 항목 정의.
    /// </summary>
    public class TotalizeiTem
    {
        /// <summary>
        /// 항목구분
        /// </summary>
        public string Type;
        /// <summary>
        /// 반환 형식 구분
        /// </summary>
        public HistogramTotalize returnType;

        /// <summary>
        /// X int형 Min
        /// </summary>
        public int niXMin;
        /// <summary>
        /// X int형 Max
        /// </summary>
        public int niXMax;
        /// <summary>
        /// X Long형 Min
        /// </summary>
        public long nlXMin;
        /// <summary>
        /// X Long형 Max
        /// </summary>
        public long nlXMax;
        /// <summary>
        /// X double형 Min
        /// </summary>
        public double ndXMin;
        /// <summary>
        /// X double형 Max
        /// </summary>
        public double ndXMax;

        /// <summary>
        /// Y int형 Min
        /// </summary>
        public int niYMin;
        /// <summary>
        /// Y int형 Max
        /// </summary>
        public int niYMax;
        /// <summary>
        /// Y Long형 Min
        /// </summary>
        public long nlYMin;
        /// <summary>
        /// Y Long형 Max
        /// </summary>
        public long nlYMax;
        /// <summary>
        /// Y double형 Min
        /// </summary>
        public double ndYMin;
        /// <summary>
        /// Y double형 Max
        /// </summary>
        public double ndYMax;


        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static TotalizeiTem Create()
        {
            TotalizeiTem c = new TotalizeiTem();
            c.Type = "";
            c.returnType = HistogramTotalize.None;
            c.niXMin = 0;
            c.niXMax = 0;
            c.nlXMin = 0;
            c.nlXMax = 0;
            c.ndXMin = SpcLimit.MIN;
            c.ndXMax = SpcLimit.MIN;

            return c;
        }


        /// <summary>
        /// Class 복사.
        /// </summary>
        /// <returns></returns>
        public TotalizeiTem CopyDeep()
        {
            TotalizeiTem cs = TotalizeiTem.Create();
            cs.Type = this.Type;
            cs.returnType = this.returnType;
            cs.niXMin = this.niXMin;
            cs.niXMax = this.niXMax;
            cs.nlXMin = this.nlXMin;
            cs.nlXMax = this.nlXMax;
            cs.ndXMin = this.ndXMin;
            cs.ndXMax = this.ndXMax;
            cs.niYMin = this.niYMin;
            cs.niYMax = this.niYMax;
            cs.nlYMin = this.nlYMin;
            cs.nlYMax = this.nlYMax;
            cs.ndYMin = this.ndYMin;
            cs.ndYMax = this.ndYMax;
            return cs;
        }

    }

}//end