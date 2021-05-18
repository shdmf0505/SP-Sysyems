using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;
using DevExpress.XtraCharts;
using Micube.Framework;
using Micube.Framework.Net;

namespace Micube.SmartMES.QualityAnalysis
{
    public partial class ucItemYieldRate : DevExpress.XtraEditors.XtraUserControl
    {
        #region Public Member

        public string PeriodText
        {
            get { return this.periodSmartLabelTextBox.Text; }

            set { this.periodSmartLabelTextBox.Text = value; }
        }

        public string ItemText
        {
            get { return this.itemSmartLabelTextBox.Text; }
            set { this.itemSmartLabelTextBox.Text = value; }
        }

        public ChartControl ChartDefectCode
        {
            get { return this.chartDefectCode; }
        }

        public ChartControl ChartLotYield
        {
            get { return this.chartLotYield; }
        }

        public ChartControl ChartItemYield
        {
            get { return this.chartItemYield; }
        }

        public SmartComboBox CboItemYieldChartViewType
        {
            get { return this.cboItemYieldChartViewType; }
        }

        public object ItemComboValue
        {
            get { return this.cboItemYieldChartViewType.EditValue; }
        }
        #endregion

        #region Members

        Point startPt = new Point();
        Point endPt = new Point();

        Rectangle rectArea = new Rectangle();

        bool isMouseDown = false;

        #endregion

        #region Create

        public ucItemYieldRate()
        {
            InitializeComponent();
        }

        private void ucItemYieldRate_Load(object sender, EventArgs e)
        {
            InitializeComboBox();

            InitializeEvent();

            chartItemYield.SeriesSelectionMode = SeriesSelectionMode.Argument;
            chartItemYield.SelectionMode = ElementSelectionMode.Single;
        }

        private void InitializeEvent()
        {
            #region 그룹박스 확대/축소 이벤트

            // 품목별 수율 그룹
            gbItemYieldRate.ExpandEvent += (s, e) =>
            {
                ucItemMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            };

            gbItemYieldRate.RestoreEvent += (s, e) =>
            {
                ucItemMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };

            // 품목별 Defect Code 그룹
            gbItemDefectCodeChart.ExpandEvent += (s, e) =>
            {
                ucItemMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                ucItemLowerSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;

            };

            gbItemDefectCodeChart.RestoreEvent += (s, e) =>
            {
                ucItemMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                ucItemLowerSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };

            // 품목별 LOT Defect 그룹
            gbItemLOTDefectChart.ExpandEvent += (s, e) =>
            {
                ucItemMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                ucItemLowerSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;

            };

            gbItemLOTDefectChart.RestoreEvent += (s, e) =>
            {
                ucItemMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                ucItemLowerSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };

            #endregion
        }

        #endregion

        #region Member Method

        private void InitializeComboBox()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "ItemYieldRateType"}
            };

            cboItemYieldChartViewType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboItemYieldChartViewType.ValueMember = "CODEID";
            cboItemYieldChartViewType.DisplayMember = "CODENAME";
            cboItemYieldChartViewType.EditValue = "PeriodSummary";
            cboItemYieldChartViewType.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboItemYieldChartViewType.ShowHeader = false;
        }

        private void YieldChart_CustomPaint(object sender, CustomPaintEventArgs e)
        {
            if (isMouseDown)
            {
                e.Graphics.DrawRectangle(Pens.Red, rectArea);
            }
        }

        private void YieldChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                rectArea.X = startPt.X;
                rectArea.Y = startPt.Y;

                rectArea.Width = (e.X - startPt.X);
                rectArea.Height = (e.Y - startPt.Y);

                endPt.X = e.X;
                endPt.Y = e.Y;
            }
        }

        private void YieldChart_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void YieldChart_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            startPt.X = e.X;
            startPt.Y = e.Y;
        }

        #endregion

        #region Test

        public DataSet CreateData(string type = "")
        {
            DataSet ds = new DataSet();

            #region Test 품목 데이터 테이블

            DataTable itemTable = new DataTable();


            if (type.Equals("Daily"))
            {
                itemTable.Columns.Add("Date", typeof(string));
                itemTable.Columns.Add("ItemID", typeof(string));
                itemTable.Columns.Add("Min", typeof(float));
                itemTable.Columns.Add("Max", typeof(float));
                itemTable.Columns.Add("Open", typeof(float));
                itemTable.Columns.Add("Close", typeof(float));
                itemTable.Columns.Add("Value", typeof(float));

                itemTable.Rows.Add(new object[] { "10-14", string.Format("2FM00317CL02B1"), 10.1, 93.7, 43.0, 65.6, 56.4 });
                itemTable.Rows.Add(new object[] { "10-14", string.Format("2FM00246SE02A1"), 13.1, 93.7, 43.0, 65.6, 46.4 });
                itemTable.Rows.Add(new object[] { "10-14", string.Format("2FM00079CL02B7"), 14.1, 93.7, 43.0, 65.6, 26.4 });
                itemTable.Rows.Add(new object[] { "10-14", string.Format("1FM00317B1"), 15.1, 93.7, 43.0, 65.6, 76.4 });

                itemTable.Rows.Add(new object[] { "10-15", string.Format("2FM00246SE02A1"), 23.6, 93.7, 43.0, 65.6, 76.4 });
                itemTable.Rows.Add(new object[] { "10-15", string.Format("2FM00079CL02B7"), 28.6, 93.7, 43.0, 65.6, 17.4 });

                itemTable.Rows.Add(new object[] { "10-16", string.Format("2FM00079CL02B7"), 76.1, 99.7, 63.0, 35.6, 56.4 });
                itemTable.Rows.Add(new object[] { "10-17", string.Format("2FM00317CL01B1"), 8.1, 73.7, 46.0, 65.6, 56.4 });
                itemTable.Rows.Add(new object[] { "10-18", string.Format("2FM00067GF01B5"), 26.8, 43.7, 4.0, 89.6, 56.4 });
                itemTable.Rows.Add(new object[] { "10-19", string.Format("1FM00317B1"), 10.1, 93.7, 43.0, 65.6, 6.4 });
                itemTable.Rows.Add(new object[] { "10-20", string.Format("2FM00213BA02B7"), 13.1, 93.7, 43.0, 65.6, 56.4 });
                itemTable.Rows.Add(new object[] { "10-21", string.Format("1FM00246A7"), 44.1, 93.7, 78.0, 87.6, 45.4 });
                itemTable.Rows.Add(new object[] { "10-22", string.Format("2FM00067CL02B5"), 87.1, 93.7, 88.0, 89.6, 90.4 });
                itemTable.Rows.Add(new object[] { "10-23", string.Format("2FM00067CL02B5"), 87.1, 93.7, 88.0, 89.6, 90.4 });
                itemTable.Rows.Add(new object[] { "10-24", string.Format("2FM00067CL02B5"), 87.1, 93.7, 88.0, 89.6, 90.4 });
                itemTable.Rows.Add(new object[] { "10-25", string.Format("2FM00067CL02B5"), 87.1, 93.7, 88.0, 89.6, 90.4 });
                itemTable.Rows.Add(new object[] { "10-26", string.Format("2FM00067CL02B5"), 87.1, 93.7, 88.0, 89.6, 90.4 });
                itemTable.Rows.Add(new object[] { "10-27", string.Format("2FM00067CL02B5"), 87.1, 93.7, 88.0, 89.6, 90.4 });
            }
            else
            {
                itemTable.Columns.Add("ItemID", typeof(string));
                itemTable.Columns.Add("Min", typeof(float));
                itemTable.Columns.Add("Max", typeof(float));
                itemTable.Columns.Add("Open", typeof(float));
                itemTable.Columns.Add("Close", typeof(float));
                itemTable.Columns.Add("Avg", typeof(float));

                itemTable.Rows.Add(new object[] { string.Format("2FM00317CL02B1"), 10.1, 93.7, 43.0, 65.6, 56.4 });
                itemTable.Rows.Add(new object[] { string.Format("2FM00246SE02A1"), 23.6, 93.7, 43.0, 65.6, 56.4 });
                itemTable.Rows.Add(new object[] { string.Format("2FM00079CL02B7"), 76.1, 99.7, 63.0, 35.6, 56.4 });
                itemTable.Rows.Add(new object[] { string.Format("2FM00317CL01B1"), 8.1, 73.7, 46.0, 65.6, 56.4 });
                itemTable.Rows.Add(new object[] { string.Format("2FM00067GF01B5"), 26.8, 43.7, 4.0, 89.6, 56.4 });
                itemTable.Rows.Add(new object[] { string.Format("1FM00317B1"), 10.1, 93.7, 43.0, 65.6, 6.4 });
                itemTable.Rows.Add(new object[] { string.Format("2FM00213BA02B7"), 13.1, 93.7, 43.0, 65.6, 56.4 });
                itemTable.Rows.Add(new object[] { string.Format("1FM00246A7"), 44.1, 93.7, 78.0, 87.6, 45.4 });
                itemTable.Rows.Add(new object[] { string.Format("2FM00067CL02B5"), 87.1, 93.7, 88.0, 89.6, 90.4 });
            }
            ds.Tables.Add(itemTable);
            #endregion

            #region Test 불량코드 테이블

            DataTable defectItemTable = new DataTable();
            defectItemTable.Columns.Add("DefectCode", typeof(string));
            defectItemTable.Columns.Add("ItemID", typeof(string));
            defectItemTable.Columns.Add("Value", typeof(float));

            defectItemTable.Rows.Add(new object[] { string.Format("양품불량-공통"), string.Format("2FM00317CL02B1"), 10.1 });
            defectItemTable.Rows.Add(new object[] { string.Format("양품불량-공통"), string.Format("2FM00246SE02A1"), 1.8 });
            defectItemTable.Rows.Add(new object[] { string.Format("양품불량-공통"), string.Format("2FM00079CL02B7"), 18.9 });

            defectItemTable.Rows.Add(new object[] { string.Format("찍힘-공통"), string.Format("2FM00246SE02A1"), 23.6 });
            defectItemTable.Rows.Add(new object[] { string.Format("찍힘-공통"), string.Format("2FM00317CL02B1"), 7.6 });

            defectItemTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("2FM00246SE02A1"), 6.5 });
            defectItemTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("2FM00317CL01B1"), 7.1 });
            defectItemTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("2FM00079CL02B7"), 14.7 });
            defectItemTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("1FM00317B1"), 10.1 });

            defectItemTable.Rows.Add(new object[] { string.Format("이물-공정"), string.Format("2FM00317CL01B1"), 8.1 });
            defectItemTable.Rows.Add(new object[] { string.Format("거침-표면처리"), string.Format("2FM00067GF01B5"), 26.8 });
            defectItemTable.Rows.Add(new object[] { string.Format("SHORT-노광"), string.Format("1FM00317B1"), 10.1 });
            defectItemTable.Rows.Add(new object[] { string.Format("액침투-원자재"), string.Format("2FM00213BA02B7"), 13.1 });
            defectItemTable.Rows.Add(new object[] { string.Format("얼룩-표면처리"), string.Format("1FM00246A7"), 44.1 });
            defectItemTable.Rows.Add(new object[] { string.Format("브릿지파손-공통"), string.Format("2FM00067CL02B5"), 87.1 });
            defectItemTable.Rows.Add(new object[] { string.Format("기타"), string.Format("2FM00067CL02B5"), 87.1 });

            ds.Tables.Add(defectItemTable);
            #endregion


            #region Test Lot 불량율 테이블

            DataTable lotDefecRateTable = new DataTable();
            lotDefecRateTable.Columns.Add("DefectCode", typeof(string));
            lotDefecRateTable.Columns.Add("LotID", typeof(string));
            lotDefecRateTable.Columns.Add("Value", typeof(float));

            lotDefecRateTable.Rows.Add(new object[] { string.Format("양품불량-공통"), string.Format("190910F004-0-FG00-001-001"), 10.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("양품불량-공통"), string.Format("190910F004-0-BA02-014-000"), 1.8 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("양품불량-공통"), string.Format("190910F005-0-FG00-001-039"), 18.9 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("찍힘-공통"), string.Format("190910F004-0-FG00-001-001"), 23.6 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("찍힘-공통"), string.Format("190910F004-0-BA02-014-000"), 7.6 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("190910F004-0-FG00-001-001"), 6.5 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("190910F004-0-BA02-014-000"), 7.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("190910F005-0-FG00-001-A08"), 14.7 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("눌림-적층"), string.Format("190910F005-0-FG00-001-A09"), 10.1 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("이물-공정"), string.Format("190910F004-0-FG00-001-001"), 8.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("이물-공정"), string.Format("190910F004-0-BA02-014-000"), 8.1 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("거침-표면처리"), string.Format("190910F004-0-FG00-001-001"), 26.8 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("거침-표면처리"), string.Format("190910F001-0-L02A-011-001"), 26.8 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("거침-표면처리"), string.Format("190917F001-0-FG00-001-002"), 26.8 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("거침-표면처리"), string.Format("190910F002-0-FG00-002-000"), 26.8 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("SHORT-노광"), string.Format("190910F002-0-FG00-002-000"), 10.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("SHORT-노광"), string.Format("190910F002-0-FG00-002-001"), 10.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("SHORT-노광"), string.Format("190910F005-0-FG00-001-038"), 10.1 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("액침투-원자재"), string.Format("190910F005-0-FG00-001-A09"), 13.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("액침투-원자재"), string.Format("190910F005-0-FG00-001-012"), 13.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("액침투-원자재"), string.Format("190910F001-0-L02A-011-001"), 13.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("액침투-원자재"), string.Format("190917F001-0-FG00-001-002"), 13.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("액침투-원자재"), string.Format("190910F002-0-FG00-002-000"), 13.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("액침투-원자재"), string.Format("190910F005-0-FG00-001-039"), 13.1 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("얼룩-표면처리"), string.Format("190910F005-0-FG00-001-012"), 44.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("얼룩-표면처리"), string.Format("190918F002-0-FG00-002-A17"), 44.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("얼룩-표면처리"), string.Format("190910F001-0-L02A-011-001"), 44.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("얼룩-표면처리"), string.Format("190910F005-0-FG00-001-038"), 44.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("얼룩-표면처리"), string.Format("190916F002-0-FG00-001-002"), 44.1 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("브릿지파손-공통"), string.Format("190917F001-0-FG00-001-002"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("브릿지파손-공통"), string.Format("190910F002-0-FG00-002-001"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("브릿지파손-공통"), string.Format("190910F005-0-FG00-001-012"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("브릿지파손-공통"), string.Format("190910F004-0-BA02-014-000"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("브릿지파손-공통"), string.Format("190910F005-0-FG00-001-A08"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("브릿지파손-공통"), string.Format("190910F004-0-FG00-001-A01"), 87.1 });

            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190916F002-0-FG00-001-002"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190910F001-0-L02A-011-001"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190917F001-0-FG00-001-002"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190910F002-0-FG00-002-000"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190910F002-0-FG00-002-001"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190910F005-0-FG00-001-038"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190910F005-0-FG00-001-039"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190910F001-0-FG00-001-051"), 87.1 });
            lotDefecRateTable.Rows.Add(new object[] { string.Format("기타"), string.Format("190910F001-0-FG00-001-050"), 87.1 });

            ds.Tables.Add(lotDefecRateTable);
            #endregion

            return ds;
        }

        public void SetChartDataItemYield(DataTable dt, DataTable  dfdt, string strChartType)
        {
            chartItemYield.Series.Clear();
            chartDefectCode.Series.Clear();
            chartLotYield.Series.Clear();

            if (dt == null || dt.Rows.Count == 0) return;

            if(chartItemYield.Series.Count == 0)
            {
                this.chartItemYield.Series.Add(new Series("BoxPlot", ViewType.CandleStick));
                this.chartItemYield.Series.Add(new Series("Median", ViewType.Line));
                this.chartItemYield.Series.Add(new Series("Average", ViewType.Line));
                //this.chartItemYield.Series.Add(new Series("LOTPoint", ViewType.Point));
            }

            this.chartItemYield.Series["BoxPlot"].Visible = false;
            this.chartItemYield.Series["Median"].Visible = false;
            this.chartItemYield.Series["Average"].Visible = false;
            //this.chartItemYield.Series["LOTPoint"].Visible = false;

            chartItemYield.SeriesSelectionMode = SeriesSelectionMode.Point;
            chartItemYield.SelectionMode = ElementSelectionMode.Single;

            chartItemYield.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            //chartItemYield.CrosshairOptions.ContentShowMode = CrosshairContentShowMode.Label;

            //chartItemYield.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            //chartItemYield.ToolTipOptions.ShowForPoints = true;

            YieldParameter parPlotData = YieldParameter.Create();

            var baseLinq = dt.AsEnumerable();
            var dfLinq = dfdt.AsEnumerable();

            

            // Hide the legend (if necessary). 
            chartItemYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            object param;

            if (baseLinq.Count() > 0)
            {
                if (strChartType.Equals("Daily"))
                {
                    var rowSrc = from dr in baseLinq
                                 select new
                                 {
                                     SAMPLING = dr.Field<string>("SUMMARYDATE"),
                                     SUBGROUP = "SUBGROUP",
                                     SUBGROUPNAME = dr.Field<string>("LOTID"),
                                     SAMPLINGNAME = dr.Field<string>("SUMMARYDATE"),
                                     NVALUE = dr.Field<decimal>("RATE")
                                 };
                    param = rowSrc;

                    var cs = rowSrc
                    //.Where(w => w.SUBGROUP == parSubgoupID)    
                    .GroupBy(g => new
                    {
                        //g.SUBGROUP,
                        g.SAMPLING
                    })
                    .Select(s => new
                    {
                        //sSUBGROUP = s.Key.SUBGROUP,
                        sSUBGROUP = s.Max(ss => ss.SUBGROUP),
                        sSAMPLING = s.Key.SAMPLING,
                        sSUBGROUPNAME = s.Max(ss => ss.SUBGROUPNAME),
                        sSAMPLINGNAME = s.Max(ss => ss.SAMPLINGNAME),
                        nMin = s.Min(ss => ss.NVALUE),
                        nMax = s.Max(ss => ss.NVALUE),
                        nTot = s.Sum(ss => ss.NVALUE),
                        nAvg = s.Average(ss => ss.NVALUE),
                        nSampingCount = s.Count()
                    });

                    foreach (var item in cs)
                    {
                        DataRow row = parPlotData.YieldDataAnalysisTable.NewRow();
                        row["SUBGROUP"] = "SUBGROUP"; //item.sSUBGROUP;
                        row["SAMPLING"] = item.sSAMPLING;
                        row["SUBGROUPNAME"] = item.sSUBGROUPNAME;
                        row["SAMPLINGNAME"] = item.sSAMPLINGNAME;
                        row["Label"] = item.sSAMPLINGNAME;

                        row["n01LowMin"] = item.nMin;
                        row["n02HightMax"] = item.nMax;
                        row["n05Mean"] = item.nAvg;

                        row["n41MIN"] = item.nMin;
                        row["n42MAX"] = item.nMax;

                        row["n33SamplingCount"] = item.nSampingCount;

                        parPlotData.YieldDataAnalysisTable.Rows.Add(row);
                    };

                    //DB Binding Clear
                    this.chartItemYield.DataSource = null;

                    //BoxPlot
                    this.chartItemYield.Series["BoxPlot"].ValueDataMembers.Clear();
                    this.chartItemYield.Series["BoxPlot"].Points.Clear();

                    //평균
                    this.chartItemYield.Series["Average"].ValueDataMembers.Clear();
                    this.chartItemYield.Series["Average"].Points.Clear();

                    //중앙값
                    this.chartItemYield.Series["Median"].ValueDataMembers.Clear();
                    this.chartItemYield.Series["Median"].Points.Clear();

                    ChartBoxPlot(ref parPlotData, AnalysisChartType.AnalysisPolt, ViewType.CandleStick, strChartType, dt);

                    //ChartLOTPointDiagram(ref parPlotData, AnalysisChartType.AnalysisPolt, ViewType.CandleStick, strChartType, dt);

                    #region 수율현황 > 품목별 수율 탭 > 수율 박스 플롯

                    //var vItems = from dr in baseLinq
                    //             select new
                    //             {
                    //                 DATE = dr.Field<string>("Date"),
                    //                 ITEMID = dr.Field<string>("ItemID"),
                    //                 MIN = dr.Field<decimal>("Min"),
                    //                 MAX = dr.Field<decimal>("Max"),
                    //                 OPEN = dr.Field<decimal>("Open"),
                    //                 CLOSE = dr.Field<decimal>("Close"),
                    //             };

                    //var gItems = from it in vItems
                    //             group it by it.DATE into g
                    //             select new
                    //             {
                    //                 DATE = g.Key,
                    //                 MIN = g.Min(d => d.MIN),
                    //                 MAX = g.Max(d => d.MAX),
                    //                 OPEN = g.Max(d => d.OPEN),
                    //                 CLOSE = g.Max(d => d.CLOSE),
                    //             } into selection
                    //             orderby selection.DATE
                    //             select selection
                    //             ;

                    //var vTot = from dr in gItems
                    //           group dr by dr.DATE into g
                    //           select new
                    //           {
                    //               Date = g.Key,
                    //               AVG = g.Average(x => (x.MIN + x.MAX + x.OPEN + x.CLOSE) / 4)
                    //           };

                    //// Create a candlestick series. 
                    //Series series1 = new Series("Candle", ViewType.CandleStick);
                    //Series totSeries = new Series("Average", ViewType.Line);


                    //// Specify the date-time argument scale type for the series, 
                    //// as it is qualitative, by default. 
                    //series1.ArgumentScaleType = ScaleType.Qualitative;
                    //totSeries.ArgumentScaleType = ScaleType.Qualitative;

                    //foreach (var item in vTot)
                    //{
                    //    totSeries.Points.Add(new SeriesPoint(item.Date, item.AVG));
                    //}

                    //foreach (var item in gItems)
                    //{
                    //    // object[] { Low, High, Open, Close }
                    //    if (item.DATE == null) continue;
                    //    series1.Points.Add(new SeriesPoint(item.DATE, new object[] { item.MIN, item.MAX, item.OPEN, item.CLOSE }));
                    //}

                    //// Add the series to the chart. 
                    //chartItemYield.Series.Add(series1);
                    //chartItemYield.Series.Add(totSeries);

                    //// Access the view-type-specific options of the series. 
                    //CandleStickSeriesView myView = (CandleStickSeriesView)series1.View;

                    //myView.LineThickness = 1;
                    //myView.LevelLineLength = 0.1;
                    ////myView.LevelLineLength = 0.5;

                    //// Specify the series reduction options. 
                    //myView.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
                    //myView.ReductionOptions.FillMode = CandleStickFillMode.AlwaysEmpty;
                    //myView.ReductionOptions.Level = StockLevel.Close;
                    //myView.ReductionOptions.Visible = true;

                    //// Access the view-type-specific options of the series. 
                    //((LineSeriesView)totSeries.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

                    //((LineSeriesView)totSeries.View).LineMarkerOptions.Kind = MarkerKind.Cross;
                    //((LineSeriesView)totSeries.View).LineMarkerOptions.Size = 15;
                    //((LineSeriesView)totSeries.View).LineMarkerOptions.Color = Color.Black;
                    //((LineSeriesView)totSeries.View).LineMarkerOptions.FillStyle.FillMode = FillMode.Empty;
                    //((LineSeriesView)totSeries.View).LineStyle.DashStyle = DashStyle.Solid;
                    //totSeries.View.Color = Color.DarkCyan;

                    //// Access the chart's diagram. 
                    //XYDiagram diagram = ((XYDiagram)chartItemYield.Diagram);
                    ////((XYDiagram)chartItemYield.Diagram).EnableAxisXZooming = true;
                    //diagram.AxisX.CrosshairAxisLabelOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;


                    //// Access the type-specific options of the diagram. 
                    //diagram.AxisY.WholeRange.MinValue = 0;

                    //// Exclude weekends from the X-axis range, 
                    //// to avoid gaps in the chart's data. 
                    ////diagram.AxisX.DateTimeScaleOptions.WorkdaysOnly = true;

                    //// Specify the date-time argument scale type for the series, 
                    //// as it is qualitative, by default. 
                    ////ptSeries.ArgumentScaleType = ScaleType.Qualitative;

                    #endregion

                    #region LOT 수율 포인트 차트

                    var vdfItems = from dr in dfLinq
                                   group dr by new
                                   {
                                       LotID = dr.Field<string>("LOTID"),
                                       Date = dr.Field<string>("SENDTIME"),
                                   } into g
                                   select new
                                   {
                                       LOTID = g.Key.LotID,
                                       DATE = g.Key.Date,
                                       RECEIVEQTY = g.Max(x => x.Field<int>("RECEIVEPCSQTY")),
                                       SENDPCSQTY = g.Max(x => x.Field<int>("SENDPCSQTY")),
                                   };

                    var vItms = (from dr in vdfItems
                                 select new
                                 {
                                     ITEM = dr.LOTID
                                 }).Distinct();

                    var vals = from dr in vdfItems
                               select new
                               {
                                   DATE = dr.DATE,
                                   ITEM = dr.LOTID,
                                   VALUE = Math.Round(100 * ((decimal)dr.SENDPCSQTY / (decimal)dr.RECEIVEQTY), 2),
                               } into selection
                               orderby selection.DATE
                               select selection;

                    List<Series> lstDayPtSeries = new List<Series>();
                    Series ptSeries;
                    foreach (var it in vItms)
                    {
                        ptSeries = new Series(it.ITEM.ToString(), ViewType.Point);
                        ptSeries.ArgumentScaleType = ScaleType.Auto;

                        foreach (var val in vals)
                        {
                            if (it.ITEM.ToString().Equals(val.ITEM.ToString()))
                            {
                                if (val.DATE == null) continue;
                                ptSeries.Points.Add(new SeriesPoint(val.DATE, val.VALUE));
                            }
                        }

                        lstDayPtSeries.Add(ptSeries);
                    }

                    chartItemYield.Series.AddRange(lstDayPtSeries.ToArray());

                    #endregion


                }
                else
                {
                    var rowSrc = from dr in baseLinq
                                 select new
                                 {
                                     SAMPLING = dr.Field<string>("PRODUCTDEFID"),
                                     SUBGROUP = "SUBGROUP",
                                     SUBGROUPNAME = dr.Field<string>("LOTID"),
                                     SAMPLINGNAME = dr.Field<string>("PRODUCTDEFNAME"),
                                     NVALUE = dr.Field<decimal>("RATE")
                                 };
                    param = rowSrc;

                    var cs = rowSrc
                    //.Where(w => w.SUBGROUP == parSubgoupID)    
                    .GroupBy(g => new
                    {
                        //g.SUBGROUP,
                        g.SAMPLING
                    })
                    .Select(s => new
                    {
                        //sSUBGROUP = s.Key.SUBGROUP,
                        sSUBGROUP = s.Max(ss => ss.SUBGROUP),
                        sSAMPLING = s.Key.SAMPLING,
                        sSUBGROUPNAME = s.Max(ss => ss.SUBGROUPNAME),
                        sSAMPLINGNAME = s.Max(ss => ss.SAMPLINGNAME),
                        nMin = s.Min(ss => ss.NVALUE),
                        nMax = s.Max(ss => ss.NVALUE),
                        nTot = s.Sum(ss => ss.NVALUE),
                        nAvg = s.Average(ss => ss.NVALUE),
                        nSampingCount = s.Count()
                    });

                    foreach (var item in cs)
                    {
                        DataRow row = parPlotData.YieldDataAnalysisTable.NewRow();
                        row["SUBGROUP"] = "SUBGROUP"; //item.sSUBGROUP;
                        row["SAMPLING"] = item.sSAMPLING;
                        row["SUBGROUPNAME"] = item.sSUBGROUPNAME;
                        row["SAMPLINGNAME"] = item.sSAMPLINGNAME;
                        row["Label"] = item.sSAMPLINGNAME;

                        row["n01LowMin"] = item.nMin;
                        row["n02HightMax"] = item.nMax;
                        row["n05Mean"] = item.nAvg;

                        row["n41MIN"] = item.nMin;
                        row["n42MAX"] = item.nMax;

                        row["n33SamplingCount"] = item.nSampingCount;

                        parPlotData.YieldDataAnalysisTable.Rows.Add(row);
                    };

                    //DB Binding Clear
                    this.chartItemYield.DataSource = null;

                    //BoxPlot
                    this.chartItemYield.Series["BoxPlot"].ValueDataMembers.Clear();
                    this.chartItemYield.Series["BoxPlot"].Points.Clear();

                    //평균
                    this.chartItemYield.Series["Average"].ValueDataMembers.Clear();
                    this.chartItemYield.Series["Average"].Points.Clear();

                    //중앙값
                    this.chartItemYield.Series["Median"].ValueDataMembers.Clear();
                    this.chartItemYield.Series["Median"].Points.Clear();

                    ChartBoxPlot(ref parPlotData, AnalysisChartType.AnalysisPolt, ViewType.CandleStick, strChartType, dt);

                    //ChartLOTPointDiagram(ref parPlotData, AnalysisChartType.AnalysisPolt, ViewType.CandleStick, strChartType, dt);

                    #region 수율현황 > 품목별 수율 탭 > 기간합계 수율 박스 플롯

                    //var vItems = from dr in baseLinq
                    //             select new
                    //             {
                    //                 ItemID = dr.Field<string>("PRODUCTDEFNAME"),
                    //                 MIN = dr.Field<decimal>("MIN"),
                    //                 MAX = dr.Field<decimal>("MAX"),
                    //                 OPEN = dr.Field<decimal>("OPEN"),
                    //                 CLOSE = dr.Field<decimal>("CLOSE"),
                    //             };

                    //var vTot = from dr in vItems
                    //           group dr by dr.ItemID into g
                    //           select new
                    //           {
                    //               ITEMID = g.Key,
                    //               AVG = g.Average(x => (x.MIN+x.MAX+x.OPEN+x.CLOSE)/4)
                    //           };

                    //// Create a candlestick series. 
                    //Series series1 = new Series("Candle", ViewType.CandleStick);
                    //Series totSeries = new Series("Average", ViewType.Line);


                    //// Specify the date-time argument scale type for the series, 
                    //// as it is qualitative, by default. 
                    //series1.ArgumentScaleType = ScaleType.Auto;
                    //totSeries.ArgumentScaleType = ScaleType.Auto;

                    //foreach (var item in vTot)
                    //{
                    //    totSeries.Points.Add(new SeriesPoint(item.ITEMID, item.AVG));
                    //}

                    //foreach (var item in vItems)
                    //{
                    //    // object[] { Low, High, Open, Close }
                    //    series1.Points.Add(new SeriesPoint(item.ItemID, new object[] { item.MIN, item.MAX, item.OPEN, item.CLOSE }));
                    //}

                    //// Add the series to the chart. 
                    //chartItemYield.Series.Add(series1);
                    //chartItemYield.Series.Add(totSeries);

                    //// Access the view-type-specific options of the series. 
                    //CandleStickSeriesView myView = (CandleStickSeriesView)series1.View;

                    //myView.LineThickness = 1;
                    //myView.LevelLineLength = 0.1;

                    //// Specify the series reduction options. 
                    //myView.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
                    //myView.ReductionOptions.FillMode = CandleStickFillMode.AlwaysEmpty;
                    //myView.ReductionOptions.Level = StockLevel.Close;
                    //myView.ReductionOptions.Visible = true;

                    //// Access the view-type-specific options of the series. 
                    //((LineSeriesView)totSeries.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

                    //((LineSeriesView)totSeries.View).LineMarkerOptions.Kind = MarkerKind.Cross;
                    //((LineSeriesView)totSeries.View).LineMarkerOptions.Size = 15;
                    //((LineSeriesView)totSeries.View).LineMarkerOptions.Color = Color.Black;
                    //((LineSeriesView)totSeries.View).LineMarkerOptions.FillStyle.FillMode = FillMode.Empty;
                    //((LineSeriesView)totSeries.View).LineStyle.DashStyle = DashStyle.Solid;
                    //totSeries.View.Color = Color.DarkCyan;
                    //// Access the chart's diagram. 
                    //XYDiagram diagram = ((XYDiagram)chartItemYield.Diagram);
                    ////((XYDiagram)chartItemYield.Diagram).EnableAxisXZooming = true;

                    //// Access the type-specific options of the diagram. 
                    //diagram.AxisY.WholeRange.MinValue = 0;

                    //// Exclude weekends from the X-axis range, 
                    //// to avoid gaps in the chart's data. 
                    //diagram.AxisX.DateTimeScaleOptions.WorkdaysOnly = true;

                    //// Hide the legend (if necessary). 
                    //chartItemYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                    #endregion

                    #region LOT 수율 포인트 차트

                    var vdfItems = from dr in dfLinq
                                   group dr by new
                                   {
                                       LotID = dr.Field<string>("LOTID"),
                                       ItemID = dr.Field<string>("PRODUCTDEFNAME"),
                                   } into g
                                   select new
                                   {
                                       LOTID = g.Key.LotID,
                                       ITEMID = g.Key.ItemID,
                                       RECEIVEQTY = g.Max(x => x.Field<int>("RECEIVEPCSQTY")),
                                       SENDPCSQTY = g.Max(x => x.Field<int>("SENDPCSQTY")),
                                   };

                    var vItms = (from dr in vdfItems
                                 select new
                                 {
                                     ITEM = dr.LOTID
                                 }).Distinct();

                    var vals = from dr in vdfItems
                               select new
                               {
                                   ITEMID = dr.ITEMID,
                                   ITEM = dr.LOTID,
                                   VALUE = dr.RECEIVEQTY == 0 ? 0 : Math.Round(100 * ((decimal)dr.SENDPCSQTY / (decimal)dr.RECEIVEQTY), 2),
                               } into selection
                               orderby selection.ITEMID
                               select selection;

                    List<Series> lstDayPtSeries = new List<Series>();
                    Series ptSeries;
                    foreach (var it in vItms)
                    {
                        ptSeries = new Series(it.ITEM.ToString(), ViewType.Point);
                        ptSeries.ArgumentScaleType = ScaleType.Qualitative;

                        foreach (var val in vals)
                        {
                            if (it.ITEM.ToString().Equals(val.ITEM.ToString()))
                            {
                                if (val.ITEMID == null) continue;
                                ptSeries.Points.Add(new SeriesPoint(val.ITEMID, val.VALUE));
                            }
                        }

                        lstDayPtSeries.Add(ptSeries);
                    }

                    chartItemYield.Series.AddRange(lstDayPtSeries.ToArray());

                    #endregion

                }
            }

            List<decimal> rate = dt.AsEnumerable().Select(s => s.Field<decimal>("RATE")).Distinct().ToList();
            double min = (double)rate.Min() - 5;
            double max = (double)rate.Max() + 5;

            if (min < 0)
                min = 0;
            if (max > 100)
                max = 100;
                
            // Access the chart's diagram. 
            XYDiagram diagram = ((XYDiagram)chartItemYield.Diagram);
            // Access the type-specific options of the diagram. 
            diagram.AxisY.WholeRange.MinValue = min;
            diagram.AxisY.WholeRange.MaxValue = max;

        }

        /// <summary>
        /// LOT 수율 포인트 시리즈
        /// </summary>
        /// <param name="parPlotData"></param>
        /// <param name="parSubgoupID"></param>
        private void ChartLOTPointDiagram(ref YieldParameter parPlotData, AnalysisChartType chartType, ViewType chartViewType, string strChartType, DataTable dt)
        {
            // LOT
            this.chartItemYield.Series["LOTPoint"].ValueDataMembers.Clear();

            long rowMax;
            string subgroupId;
            string samplingId;
            string samplingName;

            if (parPlotData.YieldDataAnalysisTable != null && parPlotData.YieldDataAnalysisTable.Rows.Count > 0)
            {
                rowMax = parPlotData.YieldDataAnalysisTable.Rows.Count;

                for (int i = 0; i < rowMax; i++)
                {
                    DataRow row = parPlotData.YieldDataAnalysisTable.Rows[i];
                    subgroupId = row["SUBGROUP"].ToSafeString();
                    samplingId = row["SAMPLING"].ToSafeString();
                    samplingName = row["SAMPLINGNAME"].ToSafeString();

                    if (strChartType.Equals("Daily"))
                    {

                        var partPoint = dt.AsEnumerable()
                                        .Where(w => w.Field<string>("SUMMARYDATE") == samplingId)
                                        .Select(s => new
                                        {
                                            sSUBGROUP = "SUBGROUP",
                                            sSAMPLING = s.Field<string>("SUMMARYDATE"),
                                            sSUBGROUPNAME = s.Field<string>("LOTID"),
                                            sSAMPLINGNAME = s.Field<string>("SUMMARYDATE"),
                                            nNVALUE = s.Field<decimal>("RATE")
                                        })
                                        .OrderBy(r => r.sSAMPLING);

                        long aa = partPoint.Count();

                        foreach (var item in partPoint)
                        {
                            SeriesPoint seriesValuePoint = new SeriesPoint();
                            seriesValuePoint.Argument = string.Format("{0}", item.sSAMPLINGNAME);
                            double[] val = new double[1];
                            val[0] = (double)item.nNVALUE;
                            seriesValuePoint.Values = val;
                            seriesValuePoint.Color = Color.Blue;
                            this.chartItemYield.Series["LOTPoint"].Points.Add(seriesValuePoint);
                        }
                    }
                    else
                    {
                        var partPoint = dt.AsEnumerable()
                                        .Where(w => w.Field<string>("PRODUCTDEFID") == samplingId)
                                        .Select(s => new
                                        {
                                            sSUBGROUP = "SUBGROUP",
                                            sSAMPLING = s.Field<string>("PRODUCTDEFID"),
                                            sSUBGROUPNAME = s.Field<string>("LOTID"),
                                            sSAMPLINGNAME = s.Field<string>("PRODUCTDEFNAME"),
                                            nNVALUE = s.Field<decimal>("RATE")
                                        })
                                        .OrderBy(r => r.sSAMPLING);

                        long aa = partPoint.Count();

                        foreach (var item in partPoint)
                        {
                            SeriesPoint seriesValuePoint = new SeriesPoint();
                            seriesValuePoint.Argument = string.Format("{0}", item.sSAMPLINGNAME);
                            double[] val = new double[1];
                            val[0] = (double)item.nNVALUE;
                            seriesValuePoint.Values = val;
                            seriesValuePoint.Color = Color.Blue;
                            this.chartItemYield.Series["LOTPoint"].Points.Add(seriesValuePoint);
                        }
                    }
                }

                this.chartItemYield.Series["LOTPoint"].View.Color = Color.Blue;

                this.chartItemYield.Series["LOTPoint"].CrosshairLabelPattern = "{A}(수율): {V:F3}";

                this.chartItemYield.Series["LOTPoint"].Visible = true;
            }
        }

        /// <summary>
        /// Box Plot Chart
        /// </summary>
        /// <param name="parPlotData"></param>
        /// <param name="chartType"></param>
        /// <param name="chartViewType"></param>
        /// <param name="dt"></param>
        private void ChartBoxPlot(ref YieldParameter parPlotData, AnalysisChartType chartType, ViewType chartViewType, string strChartType, DataTable dt = null)
        {
            if (dt == null) return;

            int rowMax = 0;
            string subgroupId = "";
            string samplingId = "";
            string samplingName = "";
            int npQ1, npQ2, npQ3, npQ4, npMedian;
            double nQ1, nQ2, nQ3, nQ4, nMedian;
            int nSCount = 0, nisResidue;
            double nRange = 0;
            double nIQR = 0;
            double nMin = 0, nMax = 0, nAvg = 0;
            string labelPattern = "";
            switch (chartType)
            {
                case AnalysisChartType.AnalysisPolt:
                case AnalysisChartType.BoxPlot:
                    labelPattern = "{A}\nMin: {LV:F3}\nMax: {HV:F3}\nQ1: {CV:F3}\nQ3: {OV:F3}";
                    break;
                case AnalysisChartType.AnalysisLine:
                    labelPattern = "{A}\nMin: {LV:F3}\nMax: {HV:F3}";
                    break;
            }

            //Box Plot
            this.chartItemYield.Series["BoxPlot"].ValueDataMembers.Clear();
            this.chartItemYield.Series["BoxPlot"].Points.Clear();
            this.chartItemYield.Series["BoxPlot"].CrosshairLabelPattern = labelPattern;
            this.chartItemYield.Series["BoxPlot"].ChangeView(chartViewType);

            //평균 Line
            this.chartItemYield.Series["Average"].ValueDataMembers.Clear();
            this.chartItemYield.Series["Average"].Points.Clear();
            this.chartItemYield.Series["Average"].CrosshairLabelPattern = "평균: {V:F3}";

            //중앙값 Line
            this.chartItemYield.Series["Median"].ValueDataMembers.Clear();
            this.chartItemYield.Series["Median"].Points.Clear();
            this.chartItemYield.Series["Median"].CrosshairLabelPattern = "중앙값: {V:F3}";

            if (parPlotData.YieldDataAnalysisTable != null && parPlotData.YieldDataAnalysisTable.Rows.Count > 0)
            {
                rowMax = parPlotData.YieldDataAnalysisTable.Rows.Count;
                for (int i = 0; i < rowMax; i++)
                {
                    DataRow row = parPlotData.YieldDataAnalysisTable.Rows[i];
                    subgroupId = row["SUBGROUP"].ToSafeString();
                    samplingId = row["SAMPLING"].ToSafeString();
                    samplingName = row["SAMPLINGNAME"].ToSafeString();
                    nAvg = row["n05Mean"].ToSafeDoubleZero();
                    nMin = row["n41MIN"].ToSafeDoubleZero();
                    nMax = row["n42MAX"].ToSafeDoubleZero();
                    nSCount = (int)row["n33SamplingCount"];

                    npQ1 = ((nSCount + 1) * 1) / 4;
                    npQ2 = ((nSCount + 1) * 2) / 4;
                    npQ3 = ((nSCount + 1) * 3) / 4;
                    npQ4 = ((nSCount + 1) * 4) / 4;

                    nisResidue = nSCount % 2;
                    if (nisResidue != 0)
                    {
                        npMedian = (nSCount + 1) / 2;
                    }
                    else
                    {
                        npMedian = nSCount / 2;
                    }



                    int p = 0;
                    double[] dValue = new double[nSCount + 2];
                    int recCount = 0;

                    if (strChartType.Equals("Daily"))
                    {
                        var recDatax = dt.AsEnumerable()
                            //.Where(w => w.SUBGROUP == subgroupId && w.SAMPLING == samplingId)
                            .Where(w => w.Field<string>("SUMMARYDATE") == samplingId)
                            .Select(s => new { NVALUE = s.Field<decimal>("RATE")})
                            .OrderBy(r1 => r1.NVALUE);

                        recCount = recDatax.Count();

                        foreach (var item in recDatax)
                        {
                            p++;
                            dValue[p] = item.NVALUE.ToSafeDoubleStaMin();
                        }
                    }
                    else
                    {
                        var recDatax = dt.AsEnumerable()
                            //.Where(w => w.SUBGROUP == subgroupId && w.SAMPLING == samplingId)
                            .Where(w => w.Field<string>("PRODUCTDEFID") == samplingId)
                            .Select(s => new { NVALUE = s.Field<decimal>("RATE") })
                            .OrderBy(r1 => r1.NVALUE);

                        recCount = recDatax.Count();

                        foreach (var item in recDatax)
                        {
                            p++;
                            dValue[p] = item.NVALUE.ToSafeDoubleStaMin();
                        }
                    }

                    if (recCount == 1)
                    {
                        nQ1 = dValue[1];
                        nQ2 = dValue[1];
                        nQ3 = dValue[1];
                        nQ4 = dValue[1];
                    }
                    else if (recCount == 2)
                    {
                        nQ1 = dValue[1];
                        nQ2 = dValue[1];
                        nQ3 = dValue[2];
                        nQ4 = dValue[2];
                    }
                    else
                    {
                        nQ1 = dValue[npQ1];
                        nQ2 = dValue[npQ2];
                        nQ3 = dValue[npQ3];
                        nQ4 = dValue[npQ4];
                    }
                    nMedian = dValue[npMedian];

                    row["n03CloseQ1"] = nQ1;
                    row["n06OpenQ3"] = nQ3;

                    row["n04Median"] = nMedian;
                    //row["n21Qp1"] = npQ1;
                    //row["n22Qp2"] = npQ2;
                    //row["n23Qp3"] = npQ3;
                    //row["n24Qp4"] = npQ4;
                    //row["n25Q1"] = nQ1;
                    //row["n26Q2"] = nQ2;
                    //row["n27Q3"] = nQ3;
                    //row["n28Q4"] = nQ4;
                    //row["n31UiQR"] = nQ4;
                    //row["n32LiQR"] = nQ4;

                    //BoxPlot
                    SeriesPoint seriesPoint = new SeriesPoint();
                    seriesPoint.Argument = samplingName;
                    double[] val = new double[4];
                    val[0] = nMin;
                    val[1] = nMax;
                    val[2] = nQ3;
                    val[3] = nQ1;
                    seriesPoint.Values = val;


                    seriesPoint.Color = Color.FromArgb(92, Color.DarkGray);


                    ////nMedian 중앙값
                    SeriesPoint seriesMedianLine = new SeriesPoint();
                    seriesMedianLine.Argument = samplingName;
                    double[] valMedianLine = new double[1];
                    valMedianLine[0] = nMedian;
                    seriesMedianLine.Values = valMedianLine;
                    seriesMedianLine.Color = Color.LightGreen;
                    this.chartItemYield.Series["Median"].Points.Add(seriesMedianLine);
                    this.chartItemYield.Series["Median"].View.Color = Color.LightGreen;

                    //this.ChrPoltA.Series["Series03Value"].Points.AddFinancialPoint( argument, low, high, open, close);
                    this.chartItemYield.Series["BoxPlot"].Points.Add(seriesPoint);

                    switch (chartType)
                    {
                        case AnalysisChartType.AnalysisLine:
                        case AnalysisChartType.AnalysisPolt:
                            //BoxPlot
                            SeriesPoint seriesPointLine = new SeriesPoint();
                            seriesPointLine.Argument = samplingName;
                            double[] valLine = new double[1];
                            valLine[0] = nAvg;
                            seriesPointLine.Values = valLine;
                            seriesPointLine.Color = Color.Black;
                            this.chartItemYield.Series["Average"].Points.Add(seriesPointLine);
                            this.chartItemYield.Series["Average"].View.Color = Color.Black;
                            break;
                    }

                }

                ((FinancialSeriesViewBase)chartItemYield.Series["BoxPlot"].View).ReductionOptions.Color = Color.FromArgb(92, Color.DarkGray);

                ((LineSeriesView)this.chartItemYield.Series["Median"].View).PointMarkerOptions.Kind = MarkerKind.Triangle;
                ((LineSeriesView)this.chartItemYield.Series["Average"].View).PointMarkerOptions.Kind = MarkerKind.Pentagon;

                //((LineSeriesView)totSeries.View).LineMarkerOptions.Kind = MarkerKind.Cross;
                this.chartItemYield.Series["BoxPlot"].Visible = true;
                this.chartItemYield.Series["Median"].Visible = true;
                this.chartItemYield.Series["Average"].Visible = true;
            }
        }

        public void SetChartDataDefect(DataTable dt, string strChartType)
        {
            var baseLinq = dt.AsEnumerable();

            chartDefectCode.Series.Clear();
            chartLotYield.Series.Clear();

            if (dt == null || dt.Rows.Count == 0)
                return;

            chartLotYield.DataSource = null;
            chartLotYield.DataSource = dt;

            var itemSeries = from item in baseLinq
                             select new
                             {
                                 ITEMID = item.Field<string>("PRODUCTDEFNAME"),
                                 LOTID = "　" + item.Field<string>("LOTID"),
                                 DEFECTCODE = item.Field<string>("DEFECTCODE"),
                                 DEFECTCODENAME = item.Field<string>("DEFECTCODENAME"),
                                 RECVQTY = item.Field<int>("RECEIVEPCSQTY"),
                                 SENDQTY = item.Field<int>("SENDPCSQTY"),
                                 DEFECTQTY = item.Field<int>("DEFECTQTY"),
                                 RANK = item.Field<int>("RNK")
                             };

            #region 불량코드별차트

            #region 불량코드별차트 - Area Chart 시리즈 데이터 처리

            //var itemDefectCodeVal = from qtyGrp in itemDefectCodeQtyG
            //                        select new
            //                        {
            //                            DEFECTCODE = qtyGrp.DEFECTCODE,
            //                            DEFECTCODENAME = qtyGrp.DEFECTCODENAME,
            //                            Value = (double)qtyGrp.DEFECTQTY / qtyGrp.RECVQTY
            //                        };

            //var itemArea = from it in itemDefectCodeVal
            //               group it by new
            //               {
            //                   it.DEFECTCODE,
            //                   it.DEFECTCODENAME
            //               } into g
            //               select new
            //               {
            //                   DefectCode = g.Key.DEFECTCODE,
            //                   DefectCodeName = g.Key.DEFECTCODENAME,
            //                   Value = g.Sum(x => x.Value)
            //               }
            //                ;

            //Series serArea = new Series("", ViewType.Area);
            //serArea.ArgumentScaleType = ScaleType.Qualitative;
            //serArea.View.Color = Color.FromArgb(128, Color.DarkGray);

            //foreach (var ser in itemArea)
            //{
            //    serArea.Points.Add(new SeriesPoint(ser.DefectCodeName, Math.Round(ser.Value * 100, 2)));
            //}
            //chartDefectCode.Series.Add(serArea);

            #endregion 불량코드별차트 - Area Chart 시리즈 데이터 처리

            #region 불량코드별차트 - Item 별 Defect Code 불량율 평균 바차트 시리즈 처리

            #region 불량코드 평균 계산

            var lotDfCdQty = from it in itemSeries
                             group it by new
                             {
                                 it.LOTID,
                                 it.DEFECTCODE,
                                 it.DEFECTCODENAME,
                             } into g
                             orderby g.Key.DEFECTCODE
                             select new
                             {
                                 g.Key.LOTID,
                                 g.Key.DEFECTCODE,
                                 g.Key.DEFECTCODENAME,
                                 ReceiveQty = g.Max(x => x.RECVQTY)
                             };

            var DfCdInQty = from it in lotDfCdQty
                            group it by new
                            {
                                it.DEFECTCODE,
                                it.DEFECTCODENAME,
                            } into g
                            select new
                            {
                                g.Key.DEFECTCODE,
                                g.Key.DEFECTCODENAME,
                                RECVQTY = g.Sum(x => x.ReceiveQty)
                            };

            var dfCodeQty = from it in itemSeries
                            group it by new
                            {
                                it.DEFECTCODE,
                                it.DEFECTCODENAME,
                                it.RANK
                            } into g
                            select new
                            {
                                g.Key.DEFECTCODE,
                                g.Key.DEFECTCODENAME,
                                g.Key.RANK,
                                DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                            };

            var dfAvgBase = from it in dfCodeQty
                            join iq in DfCdInQty on it.DEFECTCODE equals iq.DEFECTCODE
                            orderby it.RANK
                            select new
                            {
                                it.DEFECTCODE,
                                it.DEFECTCODENAME,
                                it.DEFECTQTY,
                                it.RANK,
                                iq.RECVQTY
                            };

            var dfAvg = from it in dfAvgBase
                        orderby it.RANK
                        select new
                        {
                            it.DEFECTCODE,
                            it.DEFECTCODENAME,
                            it.RANK,
                            Value = (double)it.DEFECTQTY / it.RECVQTY
                        };

            #endregion

            Series serAvg = new Series("", ViewType.Bar);
            serAvg.ArgumentScaleType = ScaleType.Qualitative;
            serAvg.View.Color = Color.FromArgb(128, Color.Blue);

            foreach (var ser in dfAvg)
            {
                serAvg.Points.Add(new SeriesPoint(ser.DEFECTCODENAME, Math.Round(ser.Value * 100, 2)));
            }
            chartDefectCode.Series.Add(serAvg);

            #endregion 불량코드별차트 - Item 별 Defect Code 불량율 평균 바차트 시리즈 처리

            #region 불량코드별차트 - Item 별 Defect Code 불량율 포인트 차트 시리즈 처리
            {
                List<Series> ptList = new List<Series>();
                Series serItem;

                var lotDefectGroup = from it in itemSeries
                                     group it by new
                                     {
                                         LotID = it.LOTID,
                                         DefectCode = it.DEFECTCODE,
                                         DefectCodeName = it.DEFECTCODENAME,
                                         Rank = it.RANK,
                                         ItemID = it.ITEMID
                                     } into g
                                     select new
                                     {
                                         LOTID = g.Key.LotID,
                                         DEFECTCODE = g.Key.DefectCode,
                                         DEFECTCODENAME = g.Key.DefectCodeName,
                                         RANK = g.Key.Rank,
                                         LOTINQTY = g.Max(x => x.RECVQTY),
                                         DEFECTQTY = g.Sum(x => x.DEFECTQTY)
                                     };

                var lotDefectRate = from it in lotDefectGroup
                                    select new
                                    {
                                        it.LOTID,
                                        it.DEFECTCODENAME,
                                        VALUE = (double)it.DEFECTQTY/it.LOTINQTY
                                    };


                var vLots = from it in lotDefectGroup
                            group it by new
                            {
                                it.LOTID,
                                it.DEFECTCODENAME
                            } into g
                            select new
                            {
                                LotID = g.Key.LOTID,
                                DEFECTNM = g.Key.DEFECTCODENAME
                            };


                foreach (var ser in vLots)
                {
                    serItem = new Series(ser.LotID, ViewType.Point);
                    serItem.ArgumentScaleType = ScaleType.Qualitative;


                    foreach (var dr in lotDefectRate)
                    {
                        if (dr.LOTID.Equals(ser.LotID) && dr.DEFECTCODENAME.Equals(ser.DEFECTNM))
                        {
                            serItem.Points.Add(new SeriesPoint(dr.DEFECTCODENAME, Math.Round(dr.VALUE * 100, 2)));
                        }
                    }

                    ptList.Add(serItem);
                }

                chartDefectCode.Series.AddRange(ptList.ToArray());

                // Access the chart's diagram. 
                XYDiagram diagramDefect = ((XYDiagram)chartDefectCode.Diagram);
                //((XYDiagram)chartDefectCode.Diagram).EnableAxisXZooming = true;
                //diagramDefect.AxisX.CrosshairAxisLabelOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagramDefect.AxisX.Label.Angle = 90;
                //diagramDefect.AxisX.LabelVisibilityMode = AxisLabelVisibilityMode.AutoGeneratedAndCustom;
                //diagramDefect.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;

                // Access the type-specific options of the diagram. 
                diagramDefect.AxisY.WholeRange.MinValue = 0;

                // Exclude weekends from the X-axis range, 
                // to avoid gaps in the chart's data. 
                //diagramDefect.AxisX.DateTimeScaleOptions.WorkdaysOnly = true;

                // Hide the legend (if necessary). 
                chartDefectCode.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            #endregion 불량코드별차트 - Item 별 Defect Code 불량율 포인트 차트 시리즈 처리

            #endregion 불량코드별차트


            {
                var serData = (from dat in baseLinq
                               select new
                               {
                                   DEFECTCODE = dat.Field<string>("DEFECTCODE"),
                                   DEFECTCODENAME = dat.Field<string>("DEFECTCODENAME")
                               }).Distinct();

                var itemLotSeries = from it in itemSeries
                                    select new
                                    {
                                        ItemID = it.ITEMID,
                                        LotID = it.LOTID,
                                        DefectCode = it.DEFECTCODE,
                                        DefectCodeName = it.DEFECTCODENAME,
                                        Value = (double)it.DEFECTQTY / it.RECVQTY
                                    };

                List<Series> ptList = new List<Series>();
                Series serItem;
                foreach (var ser in serData)
                {
                    serItem = new Series(ser.DEFECTCODENAME, ViewType.StackedBar);

                    foreach (var dr in itemLotSeries)
                    {
                        if (dr.DefectCode.Equals(ser.DEFECTCODE))
                        {
                            serItem.Points.Add(new SeriesPoint(dr.LotID, Math.Round(dr.Value * 100, 2)));
                        }
                    }

                    ptList.Add(serItem);
                }

                chartLotYield.Series.AddRange(ptList.ToArray());

                StackedBarTotalLabel totalLabel = ((XYDiagram)chartLotYield.Diagram).DefaultPane.StackedBarTotalLabel;
                totalLabel.Visible = true;
                totalLabel.ShowConnector = true;
                totalLabel.TextPattern = "Total:{TV:F2}";


                //XYDiagram diagramLotDefect = ((XYDiagram)chartLotYield.Diagram);
                ////((XYDiagram)chartLotYield.Diagram).EnableAxisXZooming = true;
                ////diagramLotDefect.AxisX.CrosshairAxisLabelOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
                //diagramLotDefect.AxisX.Label.Angle = 45;

                // Hide the legend (if necessary). 
                chartLotYield.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                chartLotYield.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                chartLotYield.Legend.Direction = LegendDirection.LeftToRight;
                chartLotYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        public void SetData(DataSet ds, string strChartType)
        {
            if (ds == null) return;

            chartItemYield.Series.Clear();
            chartDefectCode.Series.Clear();
            chartLotYield.Series.Clear();

            chartItemYield.SeriesSelectionMode = SeriesSelectionMode.Point;
            chartItemYield.SelectionMode = ElementSelectionMode.Single;

            chartItemYield.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            //chartItemYield.CrosshairOptions.ContentShowMode = CrosshairContentShowMode.Label;

            //chartItemYield.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            //chartItemYield.ToolTipOptions.ShowForPoints = true;

            DataTable dt;
            if (ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                dt = ds.Tables[0].Copy();

                var baseLinq = from items in dt.AsEnumerable()
                               select items;

                if (strChartType.Equals("Daily"))
                {
                    var vItems = from dr in baseLinq
                                 select new
                                 {
                                     DATE = dr.Field<string>("Date"),
                                     ITEMID = dr.Field<string>("ItemID"),
                                     MIN = dr.Field<float>("Min"),
                                     MAX = dr.Field<float>("Max"),
                                     OPEN = dr.Field<float>("Open"),
                                     CLOSE = dr.Field<float>("Close"),
                                 };

                    var gItems = from it in vItems
                                 group it by it.DATE into g
                                 select new
                                 {
                                     DATE = g.Key,
                                     MIN = g.Min(d => d.MIN),
                                     MAX = g.Max(d => d.MAX),
                                     OPEN = g.Max(d => d.OPEN),
                                     CLOSE = g.Max(d => d.CLOSE),
                                 };

                    // Create a candlestick series. 
                    Series series1 = new Series("Candle", ViewType.CandleStick);

                    // Specify the date-time argument scale type for the series, 
                    // as it is qualitative, by default. 
                    series1.ArgumentScaleType = ScaleType.Qualitative;


                    foreach (var item in gItems)
                    {
                        // object[] { Low, High, Open, Close }
                        series1.Points.Add(new SeriesPoint(item.DATE, new object[] { item.MIN, item.MAX, item.OPEN, item.CLOSE }));
                    }

                    // Add the series to the chart. 
                    chartItemYield.Series.Add(series1);

                    // Access the view-type-specific options of the series. 
                    CandleStickSeriesView myView = (CandleStickSeriesView)series1.View;

                    myView.LineThickness = 1;
                    myView.LevelLineLength = 0.25;

                    // Specify the series reduction options. 
                    myView.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
                    myView.ReductionOptions.FillMode = CandleStickFillMode.AlwaysEmpty;
                    myView.ReductionOptions.Level = StockLevel.Close;
                    myView.ReductionOptions.Visible = true;


                    // Access the chart's diagram. 
                    XYDiagram diagram = ((XYDiagram)chartItemYield.Diagram);
                    //((XYDiagram)chartItemYield.Diagram).EnableAxisXZooming = true;
                    diagram.AxisX.CrosshairAxisLabelOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;


                    // Access the type-specific options of the diagram. 
                    diagram.AxisY.WholeRange.MinValue = 0;

                    // Exclude weekends from the X-axis range, 
                    // to avoid gaps in the chart's data. 
                    //diagram.AxisX.DateTimeScaleOptions.WorkdaysOnly = true;

                    Series ptSeries;// = new Series("", ViewType.Point);

                    // Specify the date-time argument scale type for the series, 
                    // as it is qualitative, by default. 
                    //ptSeries.ArgumentScaleType = ScaleType.Qualitative;
                    var vItms = (from dr in baseLinq
                                 select new
                                 {
                                     ITEM = dr.Field<string>("ItemID")
                                 }).Distinct();

                    var vals = from dr in baseLinq
                               select new
                               {
                                   DATE = dr.Field<string>("Date"),
                                   ITEM = dr.Field<string>("ItemID"),
                                   VALUE = dr.Field<float>("Value"),
                               };

                    List<Series> lstDayPtSeries = new List<Series>();
                    foreach (var it in vItms)
                    {
                        ptSeries = new Series(it.ITEM.ToString(), ViewType.Point);
                        ptSeries.ArgumentScaleType = ScaleType.Qualitative;

                        foreach (var val in vals)
                        {
                            if (it.ITEM.ToString().Equals(val.ITEM.ToString()))
                            {
                                ptSeries.Points.Add(new SeriesPoint(val.DATE, val.VALUE));
                            }
                        }

                        lstDayPtSeries.Add(ptSeries);
                    }


                    chartItemYield.Series.AddRange(lstDayPtSeries.ToArray());

                    // Hide the legend (if necessary). 
                    chartItemYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    var vItems = from dr in baseLinq
                                 select new
                                 {
                                     ITEMID = dr.Field<string>("ItemID"),
                                     MIN = dr.Field<float>("Min"),
                                     MAX = dr.Field<float>("Max"),
                                     OPEN = dr.Field<float>("Open"),
                                     CLOSE = dr.Field<float>("Close"),
                                 };

                    var vTot = from dr in baseLinq
                               select new
                               {
                                   ITEMID = dr.Field<string>("ItemID"),
                                   AVG = dr.Field<float>("Avg"),

                               };
                    // Create a candlestick series. 
                    Series series1 = new Series("Candle", ViewType.CandleStick);
                    Series ptSeries = new Series("Average", ViewType.Line);


                    // Specify the date-time argument scale type for the series, 
                    // as it is qualitative, by default. 
                    series1.ArgumentScaleType = ScaleType.Auto;
                    ptSeries.ArgumentScaleType = ScaleType.Auto;

                    foreach (var item in vTot)
                    {
                        ptSeries.Points.Add(new SeriesPoint(item.ITEMID, item.AVG));
                    }

                    foreach (var item in vItems)
                    {
                        // object[] { Low, High, Open, Close }
                        series1.Points.Add(new SeriesPoint(item.ITEMID, new object[] { item.MIN, item.MAX, item.OPEN, item.CLOSE }));
                    }

                    // Add the series to the chart. 
                    chartItemYield.Series.Add(series1);
                    chartItemYield.Series.Add(ptSeries);

                    // Access the view-type-specific options of the series. 
                    CandleStickSeriesView myView = (CandleStickSeriesView)series1.View;

                    myView.LineThickness = 1;
                    myView.LevelLineLength = 0.25;

                    // Specify the series reduction options. 
                    myView.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
                    myView.ReductionOptions.FillMode = CandleStickFillMode.AlwaysEmpty;
                    myView.ReductionOptions.Level = StockLevel.Close;
                    myView.ReductionOptions.Visible = true;

                    // Access the view-type-specific options of the series. 
                    ((LineSeriesView)ptSeries.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

                    ((LineSeriesView)ptSeries.View).LineMarkerOptions.Kind = MarkerKind.Cross;
                    ((LineSeriesView)ptSeries.View).LineMarkerOptions.Size = 15;
                    ((LineSeriesView)ptSeries.View).LineMarkerOptions.Color = Color.Black;
                    ((LineSeriesView)ptSeries.View).LineMarkerOptions.FillStyle.FillMode = FillMode.Empty;
                    ((LineSeriesView)ptSeries.View).LineStyle.DashStyle = DashStyle.Solid;
                    ptSeries.View.Color = Color.DarkCyan;
                    // Access the chart's diagram. 
                    XYDiagram diagram = ((XYDiagram)chartItemYield.Diagram);
                    //((XYDiagram)chartItemYield.Diagram).EnableAxisXZooming = true;

                    // Access the type-specific options of the diagram. 
                    diagram.AxisY.WholeRange.MinValue = 0;

                    // Exclude weekends from the X-axis range, 
                    // to avoid gaps in the chart's data. 
                    //diagram.AxisX.DateTimeScaleOptions.WorkdaysOnly = true;

                    // Hide the legend (if necessary). 
                    chartItemYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;



                    // Add a title to the chart (if necessary). 
                    //chartYieldRate.Titles.Add(new ChartTitle());
                    //chartYieldRate.Titles[0].Text = "Candlestick Chart";

                    //// Add the chart to the form. 
                    //chartControl1.Dock = DockStyle.Fill;
                    //this.Controls.Add(chartControl1);
                }
            }

            if (ds.Tables.Count > 1 && ds.Tables[1] != null)
            {
                dt = ds.Tables[1].Copy();

                var baseLinq = dt.AsEnumerable();

                var itemSeries = (from item in baseLinq
                                  select new
                                  {
                                      ITEMID = item.Field<string>("ItemID"),
                                  }).Distinct();

                var itemArea = from it in baseLinq
                               group it by it.Field<string>("DefectCOde") into g
                               select new
                               {
                                   DEFECTCODE = g.Key,
                                   MIN = g.Min(it => it.Field<float>("Value"))
                               };

                Series serArea = new Series("", ViewType.Area);
                serArea.ArgumentScaleType = ScaleType.Qualitative;
                serArea.View.Color = Color.FromArgb(128, Color.DarkGray);

                foreach (var ser in itemArea)
                {
                    serArea.Points.Add(new SeriesPoint(ser.DEFECTCODE, ser.MIN));
                }
                chartDefectCode.Series.Add(serArea);

                var itemAvgs = from it in baseLinq
                               group it by it.Field<string>("DefectCOde") into g
                               select new
                               {
                                   DEFECTCODE = g.Key,
                                   AVG = g.Average(it => it.Field<float>("Value"))
                               };

                Series serAvg = new Series("", ViewType.Bar);
                serAvg.ArgumentScaleType = ScaleType.Qualitative;
                serAvg.View.Color = Color.FromArgb(128, Color.Blue);

                foreach (var ser in itemAvgs)
                {
                    serAvg.Points.Add(new SeriesPoint(ser.DEFECTCODE, ser.AVG));
                }
                chartDefectCode.Series.Add(serAvg);

                List<Series> ptList = new List<Series>();
                Series serItem;
                foreach (var ser in itemSeries)
                {
                    serItem = new Series(ser.ITEMID, ViewType.Point);
                    serItem.ArgumentScaleType = ScaleType.Qualitative;


                    foreach (DataRow dr in baseLinq)
                    {
                        if (dr["ItemID"].ToString().Equals(ser.ITEMID))
                        {
                            serItem.Points.Add(new SeriesPoint(dr["DefectCode"].ToString(), dr["Value"] ?? 0));
                        }
                    }

                    ptList.Add(serItem);
                }

                chartDefectCode.Series.AddRange(ptList.ToArray());

                // Access the chart's diagram. 
                XYDiagram diagramDefect = ((XYDiagram)chartDefectCode.Diagram);
                //((XYDiagram)chartDefectCode.Diagram).EnableAxisXZooming = true;
                //diagramDefect.AxisX.CrosshairAxisLabelOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagramDefect.AxisX.Label.Angle = 90;
                //diagramDefect.AxisX.LabelVisibilityMode = AxisLabelVisibilityMode.AutoGeneratedAndCustom;
                //diagramDefect.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;

                // Access the type-specific options of the diagram. 
                diagramDefect.AxisY.WholeRange.MinValue = 0;

                // Exclude weekends from the X-axis range, 
                // to avoid gaps in the chart's data. 
                //diagramDefect.AxisX.DateTimeScaleOptions.WorkdaysOnly = true;

                // Hide the legend (if necessary). 
                chartDefectCode.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            }

            if (ds.Tables.Count > 2 && ds.Tables[2] != null)
            {
                dt = ds.Tables[2].Copy();

                var baseLinq = dt.AsEnumerable();

                var serData = (from dat in baseLinq
                               select new
                               {
                                   DEFECTCODE = dat.Field<string>("DefectCode"),
                               }).Distinct();

                List<Series> ptList = new List<Series>();
                Series serItem;
                foreach (var ser in serData)
                {
                    serItem = new Series(ser.DEFECTCODE, ViewType.StackedBar);

                    foreach (DataRow dr in baseLinq)
                    {
                        if (dr["DefectCode"].ToString().Equals(ser.DEFECTCODE))
                        {
                            serItem.Points.Add(new SeriesPoint(dr["LotID"].ToString(), dr["Value"] ?? 0));
                        }
                    }

                    ptList.Add(serItem);
                }

                chartLotYield.Series.AddRange(ptList.ToArray());


                // Hide the legend (if necessary). 
                chartLotYield.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                chartLotYield.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                chartLotYield.Legend.Direction = LegendDirection.LeftToRight;
                chartLotYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        void createBarAreaChart()
        {

            Series series1 = new Series("BarDefectRate", ViewType.Bar);
            series1.Points.Add(new SeriesPoint("A", 30));
            series1.Points.Add(new SeriesPoint("B", 10));
            series1.Points.Add(new SeriesPoint("C", 80));
            series1.Points.Add(new SeriesPoint("D", 70));

            // Create the second side-by-side bar series and add points to it. 
            Series series2 = new Series("AreaDefectRate", ViewType.Area);
            series2.Points.Add(new SeriesPoint("A", 80));
            series2.Points.Add(new SeriesPoint("B", 20));
            series2.Points.Add(new SeriesPoint("C", 25));
            series2.Points.Add(new SeriesPoint("D", 90));

            // Add the series to the chart. 
            chartDefectCode.Series.Add(series1);
            chartDefectCode.Series.Add(series2);

            // Hide the legend (if necessary). 
            chartLotYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // Rotate the diagram (if necessary). 
            //((XYDiagram)chartDefectYield.Diagram).Rotated = true;

            // Add a title to the chart (if necessary). 
            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Text = "Side-by-Side Bar Chart";
            //chartDefectYield.Titles.Add(chartTitle1);

            //// Add the chart to the form. 
            //chartDefectYield.Dock = DockStyle.Fill;
            //this.Controls.Add(chartDefectYield);

        }

        void createStackedBarAreaChart()
        {
            // Create two stacked bar series. 
            Series series1 = new Series("Series 1", ViewType.StackedBar);
            Series series2 = new Series("Series 2", ViewType.StackedBar);

            // Add points to them 
            series1.Points.Add(new SeriesPoint("A", 10));
            series1.Points.Add(new SeriesPoint("B", 12));
            series1.Points.Add(new SeriesPoint("C", 14));
            series1.Points.Add(new SeriesPoint("D", 17));

            series2.Points.Add(new SeriesPoint("A", 15));
            series2.Points.Add(new SeriesPoint("B", 18));
            series2.Points.Add(new SeriesPoint("C", 25));
            series2.Points.Add(new SeriesPoint("D", 33));

            // Add both series to the chart. 
            chartLotYield.Series.AddRange(new Series[] { series1, series2 });

            // Access the view-type-specific options of the series. 
            //((StackedBarSeriesView)series1.View).BarWidth = 0.8;

            // Access the type-specific options of the diagram. 
            //((XYDiagram)chartLotYield.Diagram).EnableAxisXZooming = true;

            // Hide the legend (if necessary). 
            chartLotYield.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chartLotYield.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chartLotYield.Legend.Direction = LegendDirection.LeftToRight;

            chartLotYield.Legend.Visible = true;

            // Add a title to the chart (if necessary). 
            //chartLotYield.Titles.Add(new ChartTitle());
            //chartLotYield.Titles[0].Text = "A Stacked Bar Chart";

            // Add the chart to the form. 
            //chartLotYield.Dock = DockStyle.Fill;
            //this.Controls.Add(chartLotYield);

        }
        void createCandleChart()
        {

            // Create a candlestick series. 
            Series series1 = new Series("", ViewType.CandleStick);
            Series ptSeries = new Series("", ViewType.Line);


            // Specify the date-time argument scale type for the series, 
            // as it is qualitative, by default. 
            series1.ArgumentScaleType = ScaleType.Auto;
            ptSeries.ArgumentScaleType = ScaleType.Auto;

            ptSeries.Points.Add(new SeriesPoint("9AAA01002", 75.6));
            ptSeries.Points.Add(new SeriesPoint("9ANA01066", 49.6));
            ptSeries.Points.Add(new SeriesPoint("2FM00246BA01A1", 87.5));
            ptSeries.Points.Add(new SeriesPoint("1FM00213B7", 90.1));
            ptSeries.Points.Add(new SeriesPoint("1FD25256D6", 55.6));
            ptSeries.Points.Add(new SeriesPoint("2FM00067CL02B5", 32.8));
            ptSeries.Points.Add(new SeriesPoint("M5L", 46.0));
            ptSeries.Points.Add(new SeriesPoint("2FM00079SH01B7", 27.6));

            // object[] { Low, High, Open, Close }
            series1.Points.Add(new SeriesPoint("9AAA01002", new object[] { 17.901, 86.00, 65.625, 29.75 }));
            series1.Points.Add(new SeriesPoint("9ANA01066", new object[] { 32.501, 53.00, 55.725, 75.75 }));
            series1.Points.Add(new SeriesPoint("2FM00246BA01A1", new object[] { 52.041, 94.00, 25.825, 25.75 }));
            series1.Points.Add(new SeriesPoint("1FM00213B7", new object[] { 52.801, 85.00, 35.125, 45.75 }));
            series1.Points.Add(new SeriesPoint("1FD25256D6", new object[] { 12.001, 80.00, 45.425, 75.75 }));
            series1.Points.Add(new SeriesPoint("2FM00067CL02B5", new object[] { 29.401, 77.00, 55.325, 65.75 }));
            series1.Points.Add(new SeriesPoint("M5L", new object[] { 38.051, 97.00, 35.325, 55.75 }));
            series1.Points.Add(new SeriesPoint("2FM00079SH01B7", new object[] { 61.501, 77.00, 55.325, 15.75 }));

            // Add the series to the chart. 
            chartItemYield.Series.Add(series1);
            chartItemYield.Series.Add(ptSeries);

            // Access the view-type-specific options of the series. 
            CandleStickSeriesView myView = (CandleStickSeriesView)series1.View;

            myView.LineThickness = 1;
            myView.LevelLineLength = 0.25;

            // Specify the series reduction options. 
            myView.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
            myView.ReductionOptions.FillMode = CandleStickFillMode.AlwaysEmpty;
            myView.ReductionOptions.Level = StockLevel.Close;
            myView.ReductionOptions.Visible = true;

            // Access the view-type-specific options of the series. 
            ((LineSeriesView)ptSeries.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            ((LineSeriesView)ptSeries.View).LineMarkerOptions.Kind = MarkerKind.Cross;
            ((LineSeriesView)ptSeries.View).LineMarkerOptions.Size = 15;
            ((LineSeriesView)ptSeries.View).LineMarkerOptions.Color = Color.Black;
            ((LineSeriesView)ptSeries.View).LineMarkerOptions.FillStyle.FillMode = FillMode.Empty;
            ((LineSeriesView)ptSeries.View).LineStyle.DashStyle = DashStyle.Solid;
            ptSeries.View.Color = Color.DarkCyan;
            // Access the chart's diagram. 
            XYDiagram diagram = ((XYDiagram)chartItemYield.Diagram);
            ((XYDiagram)chartItemYield.Diagram).EnableAxisXZooming = true;

            // Access the type-specific options of the diagram. 
            diagram.AxisY.WholeRange.MinValue = 0;

            // Exclude weekends from the X-axis range, 
            // to avoid gaps in the chart's data. 
            //diagram.AxisX.DateTimeScaleOptions.WorkdaysOnly = true;

            // Hide the legend (if necessary). 
            chartItemYield.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;



            // Add a title to the chart (if necessary). 
            //chartYieldRate.Titles.Add(new ChartTitle());
            //chartYieldRate.Titles[0].Text = "Candlestick Chart";

            //// Add the chart to the form. 
            //chartControl1.Dock = DockStyle.Fill;
            //this.Controls.Add(chartControl1);

        }

        #endregion
    }
}
