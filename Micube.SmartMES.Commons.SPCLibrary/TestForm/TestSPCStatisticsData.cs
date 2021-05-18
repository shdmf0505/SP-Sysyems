using Micube.SmartMES.Commons.SPCLibrary.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

namespace Micube.SmartMES.Commons.SPCLibrary
{
    public partial class TestSPCStatisticsData : Form
    {

        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        /// <summary>
        /// Test Data
        /// </summary>
        private SPCTestData _spcTestData = new SPCTestData();
        #endregion

        #region 생성자
        public TestSPCStatisticsData()
        {
            InitializeComponent();
        }
        #endregion

        #region 툴바

        ///// <summary>
        ///// 저장버튼 클릭
        ///// </summary>
        //protected override void OnToolbarSaveClick()
        //{
        //    base.OnToolbarSaveClick();

        //    // TODO : 저장 Rule 변경
        //    //DataTable changed = grdCodeClass.GetChangedRows();

        //    //ExecuteRule("SaveCodeClass", changed);
        //}

        #endregion

        #region 검색

        ///// <summary>
        ///// 비동기 override 모델
        ///// </summary>
        //protected async override Task OnSearchAsync()
        //{
        //    await base.OnSearchAsync();

        //    // TODO : 조회 SP 변경
        //    var values = Conditions.GetValues();

        //    DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass", values);

        //    if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
        //    {
        //        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
        //    }

        //    //grdCodeClass.DataSource = dtCodeClass;
        //}

        //protected override void InitializeCondition()
        //{
        //    base.InitializeCondition();

        //    // TODO : 조회조건 추가 구성이 필요한 경우 사용
        //}

        //protected override void InitializeConditionControls()
        //{
        //    base.InitializeConditionControls();

        //    // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        //}

        #endregion

        #region 유효성 검사

        ///// <summary>
        ///// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        ///// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    // TODO : 유효성 로직 변경
        //    //grdCodeClass.View.CheckValidation();

        //    //DataTable changed = grdCodeClass.GetChangedRows();

        //    //if (changed.Rows.Count == 0)
        //    //{
        //    //    // 저장할 데이터가 존재하지 않습니다.
        //    //    throw MessageException.Create("NoSaveData");
        //    //}
        //}

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }

        #endregion

        #region 컨텐츠 영역 초기화

        //protected override void InitializeContent()
        //{
        //    base.InitializeContent();

        //    InitializeEvent();

        //    // TODO : 컨트롤 초기화 로직 구성
        //    InitializeGrid();
        //}

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
        }

        /// <summary>
        /// Form Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestSPCStatistics_Load(object sender, EventArgs e)
        {
            string frmTitle = this.Text.ToString();
            string version = "";
            version = "2019.07.11.01";//sia확인 - Version 변경.
            this.Text = string.Format("{0} - {1}", frmTitle, version);
            this.comboBoxMode.SelectedIndex = 0;
            this.comboBoxType.SelectedIndex = 0;

            InitializeEvents();
        }
        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void InitializeEvents()
        {
            //button1.Click += new System.EventHandler(button1_Click);
            //buttonPpCdTest.Click += new System.EventHandler(buttonPpCdTest_Click);
            buttonCDFTest.Click += new System.EventHandler(buttonCDFTest_Click);
            buttonPpIMRTest.Click += new System.EventHandler(buttonPpIMRTest_Click);
            button2.Click += new System.EventHandler(button2_Click);
            buttonPpmUSL.Click += new System.EventHandler(buttonPpmUSL_Click);
            buttonPpmLSL.Click += new System.EventHandler(buttonPpmLSL_Click);
            buttonControlXBarR.Click += new System.EventHandler(buttonControlXBarR_Click);
        }


        #region Events --------------------
        private void buttonPpIMRTest_Click(object sender, EventArgs e)
        {
            SPCLibs spcLib = new SPCLibs();
            SPCPara spcPara = new SPCPara();
            SPCOption spcOption = new SPCOption();
            SPCOutData spcOutData = new SPCOutData();
            RtnPPDataTable rtnData = new RtnPPDataTable();
            spcPara.InputData = new ParPIDataTable();
            //GROUPID" />
            //SAMPLEID" />
            //SUBGROUP" />
            //SAMPLING" />
            //NVALUE" />
            int i = 0;
            string[,] pdata = _spcTestData.IMR01();
            for (i = 1; i < pdata.GetLength(1); i++)
            {
                DataRow dr = spcPara.InputData.NewRow();
                dr["GROUPID"] = pdata[1, i];
                dr["SAMPLEID"] = i;//자동증가
                dr["SUBGROUP"] = pdata[2, i];
                dr["SAMPLING"] = pdata[3, i];
                dr["NVALUE"] = pdata[4, i]; ;
                spcPara.InputData.Rows.Add(dr);
            }

            spcPara.LSL = 44.1;
            spcPara.USL = 44.5;

            string sigmaType = this.comboBoxType.Text.ToString();
            sigmaType = sigmaType.Substring(0, 1).ToString();
            switch (sigmaType)
            {
                case "1":
                    spcOption.sigmaType = SigmaType.No;
                    break;
                case "0":
                    spcOption.sigmaType = SigmaType.Yes;
                    break;
                default:
                    spcOption.sigmaType = SigmaType.Yes;
                    break;
            }

            this.textBoxSigmaMode.Text = "I-MR";
            this.textBoxSigmaUse.Text = this.comboBoxType.Text;

            //rtnData = SPCLibs.SpcLibPPIMR(spcPara, spcOption, ref spcOutData);
            ParPIDataTable singleRawData = null;
            rtnData = SPCLibs.SpcLibPpkIMR(spcPara, spcOption, singleRawData, ref spcOutData);

            this.dataGridViewPPI.DataSource = rtnData;

            Console.WriteLine(rtnData.ToString());

            this.gridResultView(rtnData);

        }
        /// <summary>공정능력 분석 실행.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPpCdTest_Click(object sender, EventArgs e)
        {
            SPCPara spcPara = new SPCPara();
            SPCOption spcOption = new SPCOption();
            SPCOutData spcOutData = new SPCOutData();
            RtnPPDataTable rtnData = new RtnPPDataTable();
            spcPara.InputData = new ParPIDataTable();
            //GROUPID" />
            //SAMPLEID" />
            //SUBGROUP" />
            //SAMPLING" />
            //NVALUE" />
            
            int i = 0;
            int j = 0;
            string[,] pdata = _spcTestData.Cd01();
            for (i = 1; i < pdata.GetLength(1); i++)
            {
                DataRow dr = spcPara.InputData.NewRow();
                dr["GROUPID"] = pdata[1, i];
                dr["SAMPLEID"] = 1;//자동증가
                dr["SUBGROUP"] = pdata[2, i];
                dr["SAMPLING"] = pdata[3, i];
                dr["NVALUE"] = pdata[4, i]; ;
                spcPara.InputData.Rows.Add(dr);
            }

            spcPara.LSL = 1.5;
            spcPara.USL = 2.5;
            string chartType = this.comboBoxMode.Text.ToString();
            chartType = chartType.Substring(0, 1).ToString();
            switch (chartType)
            {
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    break;
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    break;
                case "M":
                    spcOption.chartType = ControlChartType.I_MR;
                    break;
                case "P":
                    spcOption.chartType = ControlChartType.Merger;
                    break;
                default:
                    break;
            }

            string sigmaType = this.comboBoxType.Text.ToString();
            sigmaType = sigmaType.Substring(0, 1).ToString();
            switch (sigmaType)
            {
                case "1":
                    spcOption.sigmaType = SigmaType.No;
                    break;
                case "0":
                    spcOption.sigmaType = SigmaType.Yes;
                    break;
                default:
                    spcOption.sigmaType = SigmaType.Yes;
                    break;
            }
            rtnData = SPCLibs.SpcLibPpkCb(spcPara, spcOption, ref spcOutData);

            this.dataGridViewPPI.DataSource = rtnData;

            Console.WriteLine(rtnData.ToString());

            this.gridResultView(rtnData);
        }

        /// <summary>Grid에 결과 자료를 세로형으로 표시.
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int gridResultView(DataTable dt, int rowIndex = 0)
        {
            int j = 0;
            ResultViewDataTable dtResultView = new ResultViewDataTable();
            //rtnData
            DataRow dr1 = dt.Rows[rowIndex];
            for (j = 0; j < dt.Columns.Count; j++)
            {

                DataRow dr2 = dtResultView.NewRow();
                dr2["SEQ"] = j;
                dr2["ITEM"] = dt.Columns[j].Caption.ToString();
                dr2["NVALUE"] = dr1[j].ToSafeString();
                dtResultView.Rows.Add(dr2);
            }

            this.dataGridViewResult.DataSource = dtResultView;
            //this.dataGridViewResult.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.dataGridViewResult.AutoSize = true;
            return 0;
        }

        private void button1Click_TEST(object sender, EventArgs e)
        {
            SPCPara spcPara = new SPCPara();
            SPCOption spcOption = new SPCOption();
            SPCOutData spcOutData = new SPCOutData();
            RtnPPDataTable rtnData = new RtnPPDataTable();
            spcPara.InputData = new ParPIDataTable();

            //GROUPID" />
            //SAMPLEID" />
            //SUBGROUP" />
            //SAMPLING" />
            //NVALUE" />
            int i = 0;
            string[,] pdata = new string[5, 11];
            DataRow dr = spcPara.InputData.NewRow();

            dr["GROUPID"] = 1;
            dr["SAMPLEID"] = i;
            dr["SUBGROUP"] = "S2" + i.ToString();
            dr["SAMPLING"] = "S3" + i.ToString();
            dr["NVALUE"] = 2;
            spcPara.InputData.Rows.Add(dr);

            i = 2;
            dr = spcPara.InputData.NewRow();
            dr["GROUPID"] = 1;
            dr["SAMPLEID"] = i;
            dr["SUBGROUP"] = "S" + i.ToString();
            dr["SAMPLING"] = "S" + i.ToString();
            dr["NVALUE"] = 3;

            spcPara.InputData.Rows.Add(dr);
            spcPara.LSL = 1.5;
            spcPara.USL = 2.3;

            rtnData = SPCLibs.SpcLibPPIMR(spcPara, spcOption, ref spcOutData);

            Console.WriteLine(rtnData.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            testParallelForEach();
        }

        private void buttonPpmLSL_Click(object sender, EventArgs e)
        {

            double cdf = 0;
            double phi = 0;
            double phi2 = 0;
            double cphi = 0;
            //double z = 2.07992353804897;

            double z = 0;

            double xbar = 44.94;
            int ppmv = 1000000;
            double lsl = 44.1;
            double sg = 0.403861;

            z = (xbar - lsl) / sg;

            z = z * 0.987820761;
            Console.WriteLine(string.Format("z => {0} ", z));

            cdf = SPCLibs.CumDensity(z);
            Console.WriteLine(string.Format("cdf => {0} ", cdf));

            phi = SPCLibs.Phi(z);
            Console.WriteLine(string.Format("phi => {0} ", phi));

            phi2 = Phi2(z);
            Console.WriteLine(string.Format("phi2 => {0} ", phi2));


            cphi = CPhi(z);
            Console.WriteLine(string.Format("cphi => {0} ", cphi));


            ////NormalDistribution();
            //double qnorm = 0;
            //qnorm = phi1.QNorm(lsl, xbar, sg, false, false);
            //Console.WriteLine(string.Format("qnorm => {0} ", qnorm));


            //double visualCDK = 0;
            //var chart = new Chart();
            ////var value = chart.DataManipulator.Statistics.InverseNormalDistribution(.15)
            //visualCDK = chart.DataManipulator.Statistics.NormalDistribution(z);

            //Console.WriteLine(string.Format("visualCDK => {0} ", visualCDK));

            //double visualCDK2 = 0;
            //var chart2 = new Chart();
            //visualCDK2 = chart2.DataManipulator.Statistics.InverseNormalDistribution(0.1);
            //Console.WriteLine(string.Format("visualCDK2 => {0} ", visualCDK2));

            double vNs = 0;
            vNs = NS(z);
            string result = string.Format("PPM 값 => {0} ", vNs);
            this.textBoxDate.Text = result;
            Console.WriteLine(result);
            Console.WriteLine("end..,");
        }
        private void buttonPpmUSL_Click(object sender, EventArgs e)
        {

            double cdf = 0;
            double phi = 0;
            double phi2 = 0;
            double cphi = 0;
            //double z = 2.07992353804897;

            double z = 0;


            double xbar = 44.94;
            int ppmv = 1000000;
            double Usl = 45.5;
            double sg = 0.403861;

            z = (Usl - xbar) / sg;
            Console.WriteLine(string.Format("z => {0} ", z));

            cdf = SPCLibs.CumDensity(z);
            Console.WriteLine(string.Format("cdf => {0} ", cdf));

            phi = SPCLibs.Phi(z);
            Console.WriteLine(string.Format("phi => {0} ", phi));

            phi2 = Phi2(z);
            Console.WriteLine(string.Format("phi2 => {0} ", phi2));


            cphi = CPhi(z);
            Console.WriteLine(string.Format("cphi => {0} ", cphi));


            ////NormalDistribution();
            //double qnorm = 0;
            //qnorm = phi1.QNorm(Usl, xbar, sg, false, false);
            //Console.WriteLine(string.Format("qnorm => {0} ", qnorm));


            //double visualCDK = 0;
            ////var chart = new Chart();
            ////var value = chart.DataManipulator.Statistics.InverseNormalDistribution(.15)
            //visualCDK = chart.DataManipulator.Statistics.NormalDistribution(z);

            //Console.WriteLine(string.Format("visualCDK => {0} ", visualCDK));

            //double visualCDK2 = 0;
            //var chart2 = new Chart();
            //visualCDK2 = chart2.DataManipulator.Statistics.InverseNormalDistribution(z);
            //Console.WriteLine(string.Format("visualCDK2 => {0} ", visualCDK2));

            Console.WriteLine("end..,");
        }

        #endregion Events --------------------

        private float F(float x, float one_over_2pi, float mean, float stddev, float var)
        {
            return (float)(one_over_2pi * Math.Exp(-(x - mean) * (x - mean) / (2 * var)));
        }

        public double vcdf(double z)
        {
            double CND = 0;
            double PI = 3.14159265358979;
            double L = 0;
            double K = 0;

            double a1 = 0.31938153;
            double a2 = -0.356563782;
            double a3 = 1.781477937;
            double a4 = -1.821255978;
            double a5 = 1.330274429;

            L = Math.Abs(z);
            K = 1 / (1 + 0.2316419 * L);
            //CND = 1 - 1 / Math.Sqrt(2 * PI) * Math.Exp(-L ^ 2 / 2) * (a1 * K + a2 * K ^ 2 + a3 * K ^ 3 + a4 * K ^ 4 + a5 * K ^ 5);

            if (z < 0)
            {
                CND = 1 - CND;
            }

            return CND;
        }

        //public double visualCDF(double z)
        //{
        //    StatisticFormula.NormalDistribution
        //}

        public static double Phi2(double x)
        {
            // constants
            double a1 = 0.254829592;//원본
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;



            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x) / Math.Sqrt(2.0);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return 0.5 * (1.0 + sign * y);
        }

        static void TestPhi()
        {
            // Select a few input values
            double[] x =
            {
                -3,
                -1,
                0.0,
                0.5,
                2.1
            };

            // Output computed by Mathematica
            // y = Phi[x]
            double[] y =
            {
                0.00134989803163,
                0.158655253931,
                0.5,
                0.691462461274,
                0.982135579437
            };

            double maxError = 0.0;
            for (int i = 0; i < x.Length; ++i)
            {
                double error = Math.Abs(y[i] - Phi2(x[i]));
                if (error > maxError)
                    maxError = error;
            }

            Console.WriteLine("Maximum error: {0}", maxError);
        }

        static double CPhi(double z)
        {
            // Output computed by Mathematica
            // y = Phi[x]
            double[] y =
            {
                0.00134989803163,
                0.158655253931,
                0.5,
                0.691462461274,
                0.982135579437
            };

            double result = 0;
            double maxError = 0.0;
            //double result = Math.Abs(y[i] - Phi2(z));
            //if (result > maxError)
            //    maxError = result;

            Console.WriteLine("Maximum error: {0}", maxError);

            for (int i = 0; i < y.Length; ++i)
            {
                result = Math.Abs(1 - Phi2(z));
                Console.WriteLine(string.Format("phi{0} => {1} ", i, result));
                if (result > maxError)
                    maxError = result;
            }

            return result;
        }
        public static double NS(double zValue)
        {
            const double b1 = 0.319381530;
            const double b2 = -0.356563782;
            const double b3 = 1.781477937;
            const double b4 = -1.821255978;
            const double b5 = 1.330274429;
            const double p = 0.2316419;
            const double c = 0.39894228;

            if (zValue >= 0.0)
            {
                double t = 1.0 / (1.0 + p * zValue);
                return (1.0 - c * Math.Exp(-zValue * zValue / 2.0) * t * (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1));
            }
            else
            {
                double t = 1.0 / (1.0 - p * zValue);
                return (c * Math.Exp(-zValue * zValue / 2.0) * t * (t * (t * (t * (t * b5 + b4) + b3) + b2) + b1));
            }
        }

        static double CND(double d)
        {
            const double A1 = 0.31938153;
            const double A2 = -0.356563782;
            const double A3 = 1.781477937;
            const double A4 = -1.821255978;
            const double A5 = 1.330274429;
            const double RSQRT2PI = 0.39894228040143267793994605993438;
            //double PI = 3.14159265358979;
            //double K = 1.0 / (1.0 + 0.2316419 * Math.Fabs(d));//원본
            double K = 0;

            double
            cnd = RSQRT2PI * Math.Exp(-0.5 * d * d) *
                  (K * (A1 + K * (A2 + K * (A3 + K * (A4 + K * A5)))));

            if (d > 0)
                cnd = 1.0 - cnd;

            return cnd;
        }
        static void testParallelForEach()
        {
            // The sum of these elements is 40.
            int[] input = { 4, 1, 6, 2, 9, 5, 10, 3 };
            int sum = 0;

            try
            {
                Parallel.ForEach(
                        input,                          // source collection
                        () => 0,                            // thread local initializer
                        (n, loopState, localSum) =>     // body
                        {
                            localSum += n;
                            Console.WriteLine("Thread={0}, n={1}, localSum={2}", Thread.CurrentThread.ManagedThreadId, n, localSum);
                            return localSum;
                        },
                        (localSum) => Interlocked.Add(ref sum, localSum)                    // thread local aggregator
                    );

                Console.WriteLine("\nSum={0}", sum);
            }
            // No exception is expected in this example, but if one is still thrown from a task,
            // it will be wrapped in AggregateException and propagated to the main thread.
            catch (AggregateException e)
            {
                Console.WriteLine("Parallel.ForEach has thrown an exception. THIS WAS NOT EXPECTED.\n{0}", e);
            }
        }
        private void buttonCDFTest_Click_1(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// CDF TEST
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCDFTest_Click(object sender, EventArgs e)
        {
            double phi11 = 0;
            double phi12 = 0;
            double phi13 = 0;

            double phi21 = 0;
            double phi22 = 0;
            double phi23 = 0;

            double phi31 = 0;
            double phi32 = 0;
            double phi33 = 0;

            double z1 = 0;
            double z2 = 0;
            double z3 = 0;
            int nPartsPer = 1000000;


            double xbar = 3.33333333333333; //SPCPVALUE_AVG
            double sg = 1.97316495659037; //SPCSVALUE_RTDC4
            //double sg = 1.18133; //SPCSVALUE_RTDC4
            double LSL = 2; //SPCLSL
            double USL = 5;
            //기대 성능(군내) LSL
            z1 = (xbar - LSL) / sg;
            phi11 = SPCLibs.CumDensity(z1);
            phi12 = nPartsPer * Math.Abs((1 - phi11)) ;
            phi12 = Math.Round(phi12, 2);
            Console.WriteLine(string.Format("phi12 => {0} ", phi12));

            //기대 성능(군내) USL
            z2 = (USL - xbar) / sg;
            phi21 = SPCLibs.CumDensity(z2);
            phi22 = Math.Abs((1 - phi21));
            phi22 = nPartsPer * Math.Abs((1 - phi21));
            phi22 = Math.Round(phi22, 2);
            
            Console.WriteLine(string.Format("phi22 => {0} ", phi22));
            //기대 성능(군내) LSL
            //z3 = (xbar - LSL) / sg;
            z3 = (USL - xbar) / sg;
            //phi31 = SPCMath.CdfNormal(z3);
            //phi31 = System.Math.Log(SPCMath.CdfNormal(z3));
            phi31 = SPCLibs.Phi(z3);
            phi32 = nPartsPer * (1 - phi31);
            phi32 = Math.Round(phi32, 2);
            Console.WriteLine(string.Format("phi32 => {0} ", phi32));

            //phi11 = SPCMath.CdfExponential(1.0, z1);
            //phi11 = SPCMath.CdfNormal(z1);
             //LSL = 2; //SPCLSL
             //USL = 1;


            double ppmWithinLSL, ppmWithinUSL;
            double ppmWithinLSLRound, ppmWithinUSLRound;
            ppmWithinLSL = SPCLibs.SpcPpm(SpcPpmMode.Within, SpcSpecType.LSL, xbar, LSL, sg, out ppmWithinLSLRound);
            ppmWithinUSL = SPCLibs.SpcPpm(SpcPpmMode.Within, SpcSpecType.USL, xbar, USL, sg, out ppmWithinUSLRound);

            Console.WriteLine(string.Format("ppmWithinLSL => {0} ", ppmWithinLSL));
            Console.WriteLine(string.Format("ppmWithinUSL => {0} ", ppmWithinUSL));
        }


        private void buttonCDFTest_Bak()
        {
            double phi01 = 0;
            double phi02 = 0;
            double phi03 = 0;

            double xbar = 12.0;
            double sg = 0.25;
            double x1 = 11.5;
            double x2 = 12.0;
            double z1 = 11.5;
            double z2 = 12.5;


            z1 = (x1 - xbar) / sg;
            z2 = (x2 - xbar) / sg;
            phi01 = SPCLibs.CumDensity(z1);
            phi02 = SPCLibs.CumDensity(z2);
            phi03 = phi01 * phi01;
            Console.WriteLine(string.Format("phi01 => {0} ", phi01));
            Console.WriteLine(string.Format("phi02 => {0} ", phi02));
        }

        private void dataGridViewPPI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aa = "20190412";
            //long no = aa.ToSafeInt64();
            //string vData = aa.ToDateTimeString("yyyy-MM-dd");
            //this.textBoxDate.Text = vData;
            //Coe item = new Coe();
            //item.mode = "", item.seqno = 1, item.nvalue = 0.1234;
            //CoeTable.coe.Add(new Coe("a", 1, 2));
            //List<Coe> coe = new List<Coe>();
            //coe.Add(new Coe("dd", 1, 0.23432));
            //Coeinitial();
            int cnt = CoeTable.coe.Count;
            Console.WriteLine(cnt);
            //IEnumerable<double> nval;
            Coe CoeValue = new Coe();
            double rtnVal;
            //rr = CoeTable.coe.Where(w => w.mode == "c4" && w.seqno == 3).Select(s => s.nvalue).FirstOrDefault();
            CoeValue = CoeTable.coe.Where(w => w.Mode == "c4" && w.SeqNo == 1).SingleOrDefault();
            if (CoeValue.Mode != null)
            {
                rtnVal = CoeValue.NValue;
            }
            else
            {
                rtnVal = 0.99975;//1001
            }

            Console.WriteLine(rtnVal);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxSigmaMode.Text = this.comboBoxMode.Text;
            this.textBoxSigmaUse.Text = this.comboBoxType.Text;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxSigmaMode.Text = this.comboBoxMode.Text;
            this.textBoxSigmaUse.Text = this.comboBoxType.Text;
        }
        private void buttonControlXBarR_Click_1(object sender, EventArgs e)
        {

        }
        /// <summary>XBar-R 관리도 실행.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonControlXBarR_Click(object sender, EventArgs e)
        {

            ControlSpec controlSpec = new ControlSpec();

            SPCPara spcPara = new SPCPara();
            SPCOption spcOption = new SPCOption();
            SPCOutData spcOutData = new SPCOutData();
            RtnControlDataTable rtnData = new RtnControlDataTable();
            spcPara.InputData = new ParPIDataTable();
            //GROUPID" />
            //SAMPLEID" />
            //SUBGROUP" />
            //SAMPLING" />
            //NVALUE" />
            int i = 0;
            int j = 0;
            string[,] pdata = _spcTestData.CTR01();
            for (i = 1; i < pdata.GetLength(1); i++)
            {
                DataRow dr = spcPara.InputData.NewRow();
                dr["GROUPID"] = pdata[1, i];
                dr["SAMPLEID"] = 1;//자동증가
                dr["SUBGROUP"] = pdata[2, i];
                dr["SAMPLING"] = pdata[3, i];
                dr["NVALUE"] = pdata[4, i]; ;
                spcPara.InputData.Rows.Add(dr);
            }

            spcPara.LSL = 1.5;
            spcPara.USL = 2.5;

            string sigmaType = this.comboBoxType.Text.ToString();
            sigmaType = sigmaType.Substring(0, 1).ToString();
            switch (sigmaType)
            {
                case "1":
                    spcOption.sigmaType = SigmaType.No;
                    break;
                case "0":
                    spcOption.sigmaType = SigmaType.Yes;
                    break;
                default:
                    spcOption.sigmaType = SigmaType.Yes;
                    break;
            }

            string chartType = this.comboBoxMode.Text.ToString();
            chartType = chartType.Substring(0, 1).ToString();
            switch (chartType)
            {
                case "R":
                    spcOption.chartType = ControlChartType.XBar_R;
                    break;
                case "S":
                    spcOption.chartType = ControlChartType.XBar_S;
                    break;
                case "M":
                    spcOption.chartType = ControlChartType.I_MR;
                    break;
                case "P":
                    spcOption.chartType = ControlChartType.Merger;
                    break;
                default:
                    break;
            }

            rtnData = SPCLibs.SpcLibXBarR(spcPara, spcOption, ref spcOutData);

            this.dataGridViewPPI.DataSource = rtnData;

            Console.WriteLine(rtnData.ToString());

            this.gridResultView(rtnData);
        }







        #endregion

        private void spcPanel02Sub_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void buttonPpIMRTest_Click_1(object sender, EventArgs e)
        {

        }

        private void btnA2_Click(object sender, EventArgs e)
        {
            int maxCount = 51;
            double d2;
            double A2;
            double d3;
            double d4;
            string message = "";
            for (int i = 1; i < maxCount; i++)
            {
                d2 = SPCSta.GetCoed2(i.ToSafeInt64());
                A2 = 3 / (Math.Sqrt(i) * d2);
                message = string.Format("NN: {0}, d2 -> {1} # A2: {2}", i, d2, A2);
                Console.WriteLine(message);
            }
            int N = 50;
            d3 = 0.80818 - 0.0051871 * N + 0.00005098 * (N * N) - 0.00000019 * (N * N * N);
            d4 = 2.88606 + 0.051313 * N + 0.00049243 * (N * N) + 0.00000188 * (N * N * N);

            Console.WriteLine(message);
        }
    }//end class


    //[SerializableAttribute]
    //public class NormalDistribution : ProbabilityDistribution, IRandomVariableMoments
    //{

    //}
}//end namespace
