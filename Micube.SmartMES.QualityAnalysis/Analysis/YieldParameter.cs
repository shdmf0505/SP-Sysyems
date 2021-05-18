using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micube.SmartMES.QualityAnalysis
{
    class YieldParameter
    {
        #region Global Variable

        /// <summary>
        /// Spc 입력 측정값 저장.
        /// </summary>
        public DataTable YieldData;

        /// <summary>
        /// Spc 입력 통계분석 측정값 저장. 10/2
        /// </summary>
        public DataTable YieldDataAnalysisTable;

        #endregion 


        #region 생성자

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public static YieldParameter Create()
        {
            YieldParameter y = new YieldParameter();
            y.YieldData = y.CreateTableSpcData();
            y.YieldDataAnalysisTable = y.CreateTableSpcDataAnalysisMode();

            return y;
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 수율 자료 등록 테이블 객체 생성.(BOX PLOT ONLY)
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableSpcData()
        {
            DataTable dataTable = new DataTable();

            //XBAR
            dataTable.Columns.Add("Label", typeof(string));
            dataTable.Columns.Add("LabelInt", typeof(Int64));
            dataTable.Columns.Add("nValue", typeof(double));
            dataTable.Columns.Add("nSigmaWithin", typeof(double));
            dataTable.Columns.Add("nSigmaTotal", typeof(double));
            //dataTable.Columns.Add("nOverRuleUSL", typeof(double));
            //dataTable.Columns.Add("nOverRuleLSL", typeof(double));
            //dataTable.Columns.Add("nOverRuleUCL", typeof(double));
            //dataTable.Columns.Add("nOverRuleLCL", typeof(double));
            //dataTable.Columns.Add("nOverRuleUOL", typeof(double));
            //dataTable.Columns.Add("nOverRuleLOL", typeof(double));
            ////dataTable.Columns.Add("UOL", typeof(DateTime));
            //dataTable.Columns.Add("UOL", typeof(double));
            //dataTable.Columns.Add("LOL", typeof(double));
            //dataTable.Columns.Add("USL", typeof(double));
            //dataTable.Columns.Add("CSL", typeof(double));
            //dataTable.Columns.Add("LSL", typeof(double));
            //dataTable.Columns.Add("UCL", typeof(double));
            //dataTable.Columns.Add("CCL", typeof(double));
            //dataTable.Columns.Add("LCL", typeof(double));
            //dataTable.Columns.Add("AUCL", typeof(double));//Analysis
            //dataTable.Columns.Add("ACCL", typeof(double));
            //dataTable.Columns.Add("ALCL", typeof(double));

            ////R
            //dataTable.Columns.Add("nRValue", typeof(double));
            //dataTable.Columns.Add("nROverRuleUCL", typeof(double));
            //dataTable.Columns.Add("nROverRuleLCL", typeof(double));
            //dataTable.Columns.Add("RAUCL", typeof(double));//Analysis
            //dataTable.Columns.Add("RACCL", typeof(double));
            //dataTable.Columns.Add("RALCL", typeof(double));

            return dataTable;
        }

        /// <summary>
        /// 수율 자료 등록 테이블 객체 생성.(BOX PLOT ONLY)
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableSpcDataAnalysisMode()
        {
            DataTable dataTable = new DataTable();

            //XBAR
            dataTable.Columns.Add("SUBGROUP", typeof(string));
            dataTable.Columns.Add("SUBGROUPNAME", typeof(string));
            dataTable.Columns.Add("SAMPLING", typeof(string));
            dataTable.Columns.Add("SAMPLINGNAME", typeof(string));
            dataTable.Columns.Add("Label", typeof(string));
            dataTable.Columns.Add("LabelInt", typeof(Int64));
            dataTable.Columns.Add("nValue", typeof(double));

            //Candle Stick Type
            dataTable.Columns.Add("n01LowMin", typeof(double));
            dataTable.Columns.Add("n02HightMax", typeof(double));
            dataTable.Columns.Add("n03CloseQ1", typeof(double));
            dataTable.Columns.Add("n04Median", typeof(double));
            dataTable.Columns.Add("n05Mean", typeof(double));
            dataTable.Columns.Add("n06OpenQ3", typeof(double));

            //dataTable.Columns.Add("n11pCBS", typeof(double));
            //dataTable.Columns.Add("n12CBS", typeof(double));
            //dataTable.Columns.Add("n21Qp1", typeof(int));
            //dataTable.Columns.Add("n22Qp2", typeof(int));
            //dataTable.Columns.Add("n23Qp3", typeof(int));
            //dataTable.Columns.Add("n24Qp4", typeof(int));
            //dataTable.Columns.Add("n25Q1", typeof(double));
            //dataTable.Columns.Add("n26Q2", typeof(double));
            //dataTable.Columns.Add("n27Q3", typeof(double));
            //dataTable.Columns.Add("n28Q4", typeof(double));
            //dataTable.Columns.Add("n31UiQR", typeof(double));
            //dataTable.Columns.Add("n32LiQR", typeof(double));
            dataTable.Columns.Add("n33SamplingCount", typeof(int));

            dataTable.Columns.Add("n41MIN", typeof(double));
            dataTable.Columns.Add("n42MAX", typeof(double));



            dataTable.Columns.Add("nSigmaWithin", typeof(double));
            dataTable.Columns.Add("nSigmaTotal", typeof(double));
            //dataTable.Columns.Add("nOverRuleUSL", typeof(double));
            //dataTable.Columns.Add("nOverRuleLSL", typeof(double));
            //dataTable.Columns.Add("nOverRuleUCL", typeof(double));
            //dataTable.Columns.Add("nOverRuleLCL", typeof(double));
            //dataTable.Columns.Add("nOverRuleUOL", typeof(double));
            //dataTable.Columns.Add("nOverRuleLOL", typeof(double));
            ////dataTable.Columns.Add("UOL", typeof(DateTime));
            //dataTable.Columns.Add("UOL", typeof(double));
            //dataTable.Columns.Add("LOL", typeof(double));
            //dataTable.Columns.Add("USL", typeof(double));
            //dataTable.Columns.Add("CSL", typeof(double));
            //dataTable.Columns.Add("LSL", typeof(double));
            //dataTable.Columns.Add("UCL", typeof(double));
            //dataTable.Columns.Add("CCL", typeof(double));
            //dataTable.Columns.Add("LCL", typeof(double));
            //dataTable.Columns.Add("AUCL", typeof(double));//Analysis
            //dataTable.Columns.Add("ACCL", typeof(double));
            //dataTable.Columns.Add("ALCL", typeof(double));

            ////R
            //dataTable.Columns.Add("nRValue", typeof(double));
            //dataTable.Columns.Add("nROverRuleUCL", typeof(double));
            //dataTable.Columns.Add("nROverRuleLCL", typeof(double));
            //dataTable.Columns.Add("RAUCL", typeof(double));//Analysis
            //dataTable.Columns.Add("RACCL", typeof(double));
            //dataTable.Columns.Add("RALCL", typeof(double));

            return dataTable;
        }

        #endregion
    }

    /// <summary>
    /// 분석용 Chart 구분 - 분석 Chart Type
    /// </summary>
    public enum AnalysisChartType
    {
        None = 0              //미선택
        , AnalysisPolt = 1      //비교분석 Polt
        , AnalysisLine = 2      //비교분석 Line
        , BoxPlot = 3            //Box Plot
        , ThreePointDiagram = 4  //산점도
        , TimeSeries = 5        //시계열도
    }
}
