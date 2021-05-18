using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using Micube.Framework.SmartControls;
using System.Linq.Expressions;
using Micube.Framework;
using DevExpress.XtraEditors;

namespace Micube.SmartMES.QualityAnalysis
{
    #region delegate

    public delegate void moveTab(int tabPage);

    #endregion

    public partial class ucDayYieldRate : DevExpress.XtraEditors.XtraUserControl
    {
        #region Public Member

        #region Members

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

        public DevExpress.XtraCharts.ChartControl ChartMonth
        {
            get { return this.chartYiedRateMonth; }
        }

        public DevExpress.XtraCharts.ChartControl ChartWeek
        {
            get { return this.chartYiedRateWeek; }
        }

        public DevExpress.XtraCharts.ChartControl ChartDay
        {
            get { return this.chartYiedRateDay; }
        }

        public SmartBandedGrid GridYield
        {
            get { return this.grdDayYieldRate; }
        }

        #endregion

        #endregion

        #region Local Variables

        Point startPt = new Point();
        Point endPt = new Point();

        Rectangle rectArea = new Rectangle();

        bool isMouseDown = false;

        #endregion

        #region 생성자

        public ucDayYieldRate()
        {
            InitializeComponent();
        }

        private void ucDayYieldRate_Load(object sender, EventArgs e)
        {
            initializeGrid();
            //createBarChart();

            initializeEvent();
        }

        #endregion

        #region Events

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

        #region Member Method

        private void initializeGrid()
        {
            //grdDayYieldRate.GridButtonItem = GridButtonItem.Expand | GridButtonItem.None;
            grdDayYieldRate.View.SetIsReadOnly(true);
            //grdDayYieldRate.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            // 날짜
            grdDayYieldRate.View.AddTextBoxColumn("DAYCATEGORY", 150);

            // 전체
            grdDayYieldRate.View
                .AddTextBoxColumn("YIELD_TOTAL", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // S/S 
            grdDayYieldRate.View
                .AddTextBoxColumn("YIELDSS", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // D/S
            grdDayYieldRate.View
                .AddTextBoxColumn("YIELDDS", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // M/T
            grdDayYieldRate.View
                .AddTextBoxColumn("YIELDMT", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // RF
            grdDayYieldRate.View
                .AddTextBoxColumn("YIELDRF", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetDisplayFormat("##0.#0 %", MaskTypes.Numeric);

            // 입고 PCS 수량
            grdDayYieldRate.View
                .AddTextBoxColumn("INCOMEQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCSINPUTQTY")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 출고 PCS 수량
            grdDayYieldRate.View
                .AddTextBoxColumn("SENDQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCSNORMALQTY")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 불량 수량
            grdDayYieldRate.View
                .AddTextBoxColumn("DEFECTCOUNT", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCSDEFECTQTY")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            // 수량정보 

            grdDayYieldRate.View.PopulateColumns();
        }

        private void initializeEvent()
        {
            this.chartYiedRateMonth.CustomDrawSeriesPoint += ChartYiedRateMonth_CustomDrawSeriesPoint;
            this.chartYiedRateWeek.CustomDrawSeriesPoint += ChartYiedRateMonth_CustomDrawSeriesPoint;
            this.chartYiedRateDay.CustomDrawSeriesPoint += ChartYiedRateMonth_CustomDrawSeriesPoint;
            //this.chartYiedRateDay.MouseDown += YieldChart_MouseDown;
            //this.chartYiedRateDay.MouseUp += YieldChart_MouseUp;
            //this.chartYiedRateDay.MouseMove += YieldChart_MouseMove;
            //this.chartYiedRateDay.CustomPaint += YieldChart_CustomPaint;

            #region 그룹 박스 확대 축소

            gbMonthYield.ExpandEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Panel1;
                this.ucDayUpperSpliter.PanelVisibility = SplitPanelVisibility.Panel1;
                this.ucDayUpper2Spliter.PanelVisibility = SplitPanelVisibility.Panel1;
            };

            gbMonthYield.RestoreEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Both;
                this.ucDayUpperSpliter.PanelVisibility = SplitPanelVisibility.Both;
                this.ucDayUpper2Spliter.PanelVisibility = SplitPanelVisibility.Both;
            };

            gbWeekYield.ExpandEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Panel1;
                this.ucDayUpperSpliter.PanelVisibility = SplitPanelVisibility.Panel2;
                this.ucDayUpper2Spliter.PanelVisibility = SplitPanelVisibility.Panel1;
            };

            gbWeekYield.RestoreEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Both;
                this.ucDayUpperSpliter.PanelVisibility = SplitPanelVisibility.Both;
                this.ucDayUpper2Spliter.PanelVisibility = SplitPanelVisibility.Both;
            };

            gbDayYield.ExpandEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Panel1;
                this.ucDayUpperSpliter.PanelVisibility = SplitPanelVisibility.Panel2;
                this.ucDayUpper2Spliter.PanelVisibility = SplitPanelVisibility.Panel2;
            };

            gbDayYield.RestoreEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Both;
                this.ucDayUpperSpliter.PanelVisibility = SplitPanelVisibility.Both;
                this.ucDayUpper2Spliter.PanelVisibility = SplitPanelVisibility.Both;
            };

            gbGridPeriodYield.ExpandEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Panel2;
            };

            gbGridPeriodYield.RestoreEvent += (s, e) =>
            {
                this.ucDayMainSpliter.PanelVisibility = SplitPanelVisibility.Both;
            };

            #endregion
        }

        private void ChartYiedRateMonth_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            string strValue = e.SeriesPoint.Values[0].ToString();
            if (!string.IsNullOrEmpty(strValue))
            {
                e.LabelText = Math.Round(Convert.ToDecimal(strValue), 2).ToString();
            }
        }


        /// <summary>
        /// 테스트 시 사용, 조회조건으로 타도록 변경 후 사용 안함.
        /// 데이터 바인딩시 차트 시리즈 설정
        /// </summary>
        void createBarChart()
        {
            XYDiagram diagram;

            #region 월 수율 트렌드

            chartYiedRateMonth.SeriesDataMember = "Type";
            chartYiedRateMonth.SeriesTemplate.ArgumentDataMember = "Day";
            chartYiedRateMonth.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });

            diagram = ((XYDiagram)chartYiedRateMonth.Diagram);

            diagram.AxisY.WholeRange.MinValue = 0;
            //diagram.AxisY.WholeRange.MaxValue = 100;

            #endregion

            #region 주 수율 트렌드

            //chartYiedRateWeek.SeriesDataMember = "Type";
            //chartYiedRateWeek.SeriesTemplate.ArgumentDataMember = "Day";
            //chartYiedRateWeek.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });

            diagram = ((XYDiagram)chartYiedRateWeek.Diagram);

            diagram.AxisY.WholeRange.MinValue = 0;
            //diagram.AxisY.WholeRange.MaxValue = 100;

            #endregion

            #region 일 수율 트렌드

            //chartYiedRateDay.SeriesDataMember = "Type";
            //chartYiedRateDay.SeriesTemplate.ArgumentDataMember = "Day";
            //chartYiedRateDay.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });

            diagram = ((XYDiagram)chartYiedRateDay.Diagram);

            diagram.AxisY.WholeRange.MinValue = 0;
            //diagram.AxisY.WholeRange.MaxValue = 100;

            #endregion
        }
        #endregion

        #region public Method

        /// <summary>
        /// 날짜별 차트 데이터 설정(월, 주, 일)
        /// </summary>
        /// <param name="dt">데이터 테이블</param>
        public void SetChartDataSource(DataTable dt)
        {
            #region 월 트렌드 차트 

            chartYiedRateMonth.Series.Clear();

            #region 차트 바 색 프로퍼티 설정

            //chartYiedRateMonth.PaletteName = "Office 2013";

            //chartYiedRateMonth.PaletteBaseColorNumber = 3;

            //chartYiedRateWeek.PaletteName = "Office 2013";

            //chartYiedRateWeek.PaletteBaseColorNumber = 3;

            //chartYiedRateDay.PaletteName = "Office 2013";

            //chartYiedRateDay.PaletteBaseColorNumber = 3;

            #endregion

            double max = 0;
            int maxAxisY = 0;

            DataView dv = dt.DefaultView;
            {
                BarSeriesView bsView;

                string strFilter = "DAYTYPE='MONTH'";
                dv.RowFilter = strFilter;

                Series serSS = new Series("S/S", ViewType.Bar);
                serSS.ArgumentScaleType = ScaleType.Qualitative;
                //serSS.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serSS.View;
                //bsView.BarWidth = 20;

                Series serDS = new Series("D/S", ViewType.Bar);
                serDS.ArgumentScaleType = ScaleType.Qualitative;
                //serDS.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serDS.View;
                //bsView.BarWidth = 20;

                Series serMT = new Series("M/T", ViewType.Bar);
                serMT.ArgumentScaleType = ScaleType.Qualitative;
                //serMT.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serMT.View;
                //bsView.BarWidth = 20;

                Series serRF = new Series("R/F", ViewType.Bar);
                serRF.ArgumentScaleType = ScaleType.Qualitative;
                //serRF.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serRF.View;
                //bsView.BarWidth = 20;

                Series serTotal = new Series("Total", ViewType.Line);
                serTotal.ArgumentScaleType = ScaleType.Qualitative;

                #region Total Series Label 설정

                // series label setting
                serTotal.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                serTotal.Label.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
                serTotal.Label.TextColor = Color.Black;
                serTotal.Label.BackColor = Color.Transparent;

                Font font = new Font(serTotal.Label.Font, FontStyle.Bold);
                //serTotal.Label.Font = font;
                PointSeriesLabel monthLabel = serTotal.Label as PointSeriesLabel;
                monthLabel.Angle = 90;
                monthLabel.Position = PointLabelPosition.Outside;
                monthLabel.Font = font;

                BorderBase border = serTotal.Label.Border;
                border.Visibility = DevExpress.Utils.DefaultBoolean.False;

                #endregion Total Series Label 설정

                // Access the view-type-specific options of the series. 
                ((LineSeriesView)serTotal.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                ((LineSeriesView)serTotal.View).LineStyle.DashStyle = DashStyle.Solid;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.FillStyle.FillMode = FillMode.Empty;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Size = 10;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Color = Color.LightGray;
                //((LineSeriesView)serTotal.View).LineMarkerOptions.BorderVisible = true;
                //((LineSeriesView)serTotal.View).LineMarkerOptions.BorderColor = Color.Black;
                serTotal.View.Color = Color.Blue;


                var baseLinq = dv.ToTable().AsEnumerable();

                var vProductType = (from dr in baseLinq
                                    select new
                                    {
                                        PType = dr.Field<string>("PRODUCTTYPE")
                                    }
                                    ).Distinct();

                var vMonth = from dr in baseLinq
                             select new
                             {
                                 GUBUN = dr.Field<string>("DAYGUBUN"),
                                 PRODTYPE = dr.Field<string>("PRODUCTTYPE"),
                                 RATE = dr.Field<decimal>("RATE"),

                             };

                var vGroup = from dr in baseLinq
                             group dr by new
                             {
                                 GUBUN = dr.Field<string>("DAYGUBUN"),
                             } into g
                             select new
                             {
                                 GUBUN = g.Key.GUBUN,
                                 RECEIVEQTY = g.Sum(x => x.Field<int>("RECEIVEPCSQTY")),
                                 SENDQTY = g.Sum(x => x.Field<int>("SENDPCSQTY"))
                             };

                foreach (var row in vGroup)
                {
                    string strYear = Language.GetDictionary("YEAR").Name;
                    string strMonth = Language.GetDictionary("NMONTH").Name;

                    string[] arrSplit = row.GUBUN.Split('-');

                    string serName = arrSplit[0] + strYear + " " + arrSplit[1] + strMonth;

                    double dPercent = 0.00;
                    if(row.RECEIVEQTY != 0)
                        dPercent = 100 * ((double)row.SENDQTY / (double)row.RECEIVEQTY);

                    serTotal.Points.Add(new SeriesPoint(serName, Math.Round(dPercent, 2)));
                }

                foreach (var row in vMonth)
                {
                    string strYear = Language.GetDictionary("YEAR").Name;
                    string strMonth = Language.GetDictionary("NMONTH").Name;
                    
                    string[] arrSplit = row.GUBUN.Split('-');

                    string serName = arrSplit[0] + strYear + " " + arrSplit[1] + strMonth;

                    if (row.PRODTYPE.Equals("SS"))
                        serSS.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("DS"))
                        serDS.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("RF"))
                        serRF.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("MT"))
                        serMT.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                }
                
                chartYiedRateMonth.Series.Add(serSS);
                chartYiedRateMonth.Series.Add(serDS);
                chartYiedRateMonth.Series.Add(serMT);
                chartYiedRateMonth.Series.Add(serRF);
                chartYiedRateMonth.Series.Add(serTotal);

                // 범례 설정
                chartYiedRateMonth.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                chartYiedRateMonth.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                chartYiedRateMonth.Legend.Direction = LegendDirection.LeftToRight;
                chartYiedRateMonth.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                //((SideBySideBarSeriesView)chartYiedRateMonth.SeriesTemplate.View).BarWidth = 50;


                XYDiagram diagram = ((XYDiagram)chartYiedRateMonth.Diagram);

                max = (double)vMonth.Select(x => x.RATE).Distinct().ToList().Max();
                max = Math.Round(100 * max, 2);

                diagram.AxisY.WholeRange.MinValue = 0;
                diagram.AxisY.WholeRange.MaxValue = max + 10;

                // Define the whole range for the X-axis.  
                diagram.AxisX.WholeRange.Auto = false;
                diagram.AxisX.WholeRange.SetMinMaxValues(vMonth.Select(x => x.GUBUN).Distinct().ToList().Min(), vMonth.Select(x => x.GUBUN).Distinct().ToList().Max());
            }

            #endregion

            #region 주 트렌드 차트

            chartYiedRateWeek.Series.Clear();
            dv.RowFilter = "";
            {
                BarSeriesView bsView;

                string strFilter = "DAYTYPE='WEEK'";
                dv.RowFilter = strFilter;

                Series serSS = new Series("S/S", ViewType.Bar);
                serSS.ArgumentScaleType = ScaleType.Qualitative;
                //serSS.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serSS.View;
                //bsView.BarWidth = 20;

                Series serDS = new Series("D/S", ViewType.Bar);
                serDS.ArgumentScaleType = ScaleType.Qualitative;
                //serDS.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serDS.View;
                //bsView.BarWidth = 20;

                Series serMT = new Series("M/T", ViewType.Bar);
                serMT.ArgumentScaleType = ScaleType.Qualitative;
                //serMT.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serMT.View;
                //bsView.BarWidth = 20;

                Series serRF = new Series("R/F", ViewType.Bar);
                serRF.ArgumentScaleType = ScaleType.Qualitative;
                //serRF.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serRF.View;
                //bsView.BarWidth = 20;

                Series serTotal = new Series("Total", ViewType.Line);
                serTotal.ArgumentScaleType = ScaleType.Qualitative;

                #region Total Series Label 설정

                // series label setting
                serTotal.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                serTotal.Label.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
                serTotal.Label.TextColor = Color.Black;
                serTotal.Label.BackColor = Color.Transparent;

                Font font = new Font(serTotal.Label.Font, FontStyle.Bold);
                //serTotal.Label.Font = font;
                PointSeriesLabel monthLabel = serTotal.Label as PointSeriesLabel;
                monthLabel.Angle = 90;
                monthLabel.Position = PointLabelPosition.Outside;
                monthLabel.Font = font;

                BorderBase border = serTotal.Label.Border;
                border.Visibility = DevExpress.Utils.DefaultBoolean.False;

                #endregion Total Series Label 설정

                // Access the view-type-specific options of the series. 
                ((LineSeriesView)serTotal.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                ((LineSeriesView)serTotal.View).LineStyle.DashStyle = DashStyle.Solid;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.FillStyle.FillMode = FillMode.Empty;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Size = 10;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Color = Color.LightGray;
                //((LineSeriesView)serTotal.View).LineMarkerOptions.BorderVisible = true;
                //((LineSeriesView)serTotal.View).LineMarkerOptions.BorderColor = Color.Black;
                serTotal.View.Color = Color.Blue;

                var baseLinq = dv.ToTable().AsEnumerable();

                var vProductType = (from dr in baseLinq
                                    select new
                                    {
                                        PType = dr.Field<string>("PRODUCTTYPE")
                                    }
                                    ).Distinct();

                var vMonth = from dr in baseLinq
                             select new
                             {
                                 GUBUN = dr.Field<string>("DAYGUBUN"),
                                 PRODTYPE = dr.Field<string>("PRODUCTTYPE"),
                                 RATE = dr.Field<decimal>("RATE"),

                             };

                var vGroup = from dr in baseLinq
                             group dr by new
                             {
                                 GUBUN = dr.Field<string>("DAYGUBUN"),
                             } into g
                             select new
                             {
                                 GUBUN = g.Key.GUBUN,
                                 RECEIVEQTY = g.Sum(x => x.Field<int>("RECEIVEPCSQTY")),
                                 SENDQTY = g.Sum(x => x.Field<int>("SENDPCSQTY"))
                             };

                foreach (var row in vGroup)
                {
                    string strYear = Language.GetDictionary("YEAR").Name;
                    string strWeek = Language.GetDictionary("WEEK").Name;

                    string[] arrSplit = row.GUBUN.Split(' ');

                    string serName = arrSplit[0] + strYear + " " + arrSplit[1] + strWeek;

                    double dPercent = 0.00;
                    if (row.RECEIVEQTY != 0)
                        dPercent = 100 * ((double)row.SENDQTY / (double)row.RECEIVEQTY);

                    serTotal.Points.Add(new SeriesPoint(serName, Math.Round(dPercent, 2)));
                }

                foreach (var row in vMonth)
                {
                    string strYear = Language.GetDictionary("YEAR").Name;
                    string strWeek = Language.GetDictionary("WEEK").Name;

                    string[] arrSplit = row.GUBUN.Split(' ');

                    string serName = arrSplit[0] + strYear + " " + arrSplit[1] + strWeek;

                    if (row.PRODTYPE.Equals("SS"))
                        serSS.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("DS"))
                        serDS.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("RF"))
                        serRF.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("MT"))
                        serMT.Points.Add(new SeriesPoint(serName, Math.Round(100 * row.RATE, 2)));
                }

                chartYiedRateWeek.Series.Add(serSS);
                chartYiedRateWeek.Series.Add(serDS);
                chartYiedRateWeek.Series.Add(serMT);
                chartYiedRateWeek.Series.Add(serRF);
                chartYiedRateWeek.Series.Add(serTotal);

                // 범례 설정
                chartYiedRateWeek.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                chartYiedRateWeek.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                chartYiedRateWeek.Legend.Direction = LegendDirection.LeftToRight;
                chartYiedRateWeek.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                //((SideBySideBarSeriesView)chartYiedRateMonth.SeriesTemplate.View).BarWidth = 50;

                XYDiagram diagram = ((XYDiagram)chartYiedRateWeek.Diagram);

                max = (double)vMonth.Select(x => x.RATE).Distinct().ToList().Max();
                max = Math.Round(100 * max, 2);

                diagram.AxisY.WholeRange.MinValue = 0;
                diagram.AxisY.WholeRange.MaxValue = max + 10;

                // Define the whole range for the X-axis.  
                diagram.AxisX.WholeRange.Auto = false;
                diagram.AxisX.WholeRange.SetMinMaxValues(vMonth.Select(x => x.GUBUN).Distinct().ToList().Min(), vMonth.Select(x => x.GUBUN).Distinct().ToList().Max());
            }

            #endregion

            #region 일별 트렌드 차트

            chartYiedRateDay.Series.Clear();
            dv.RowFilter = "";
            {
                BarSeriesView bsView;

                string strFilter = "DAYTYPE='DAY'";
                dv.RowFilter = strFilter;

                Series serSS = new Series("S/S", ViewType.Bar);
                serSS.ArgumentScaleType = ScaleType.Qualitative;
                //serSS.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serSS.View;
                //bsView.BarWidth = 20;

                Series serDS = new Series("D/S", ViewType.Bar);
                serDS.ArgumentScaleType = ScaleType.Qualitative;
                //serDS.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serDS.View;
                //bsView.BarWidth = 20;

                Series serMT = new Series("M/T", ViewType.Bar);
                serMT.ArgumentScaleType = ScaleType.Qualitative;
                //serMT.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serMT.View;
                //bsView.BarWidth = 20;

                Series serRF = new Series("R/F", ViewType.Bar);
                serRF.ArgumentScaleType = ScaleType.Qualitative;
                //serRF.ValueScaleType = ScaleType.Auto;
                bsView = (BarSeriesView)serRF.View;
                //bsView.BarWidth = 20;

                Series serTotal = new Series("Total", ViewType.Line);
                serTotal.ArgumentScaleType = ScaleType.Qualitative;

                #region Total Series Label 설정

                // series label setting
                serTotal.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                serTotal.Label.LineVisibility = DevExpress.Utils.DefaultBoolean.False;
                serTotal.Label.TextColor = Color.Black;
                serTotal.Label.BackColor = Color.Transparent;

                Font font = new Font(serTotal.Label.Font, FontStyle.Bold);
                //serTotal.Label.Font = font;
                PointSeriesLabel monthLabel = serTotal.Label as PointSeriesLabel;
                monthLabel.Angle = 90;
                monthLabel.Position = PointLabelPosition.Outside;
                monthLabel.Font = font;

                BorderBase border = serTotal.Label.Border;
                border.Visibility = DevExpress.Utils.DefaultBoolean.False;

                #endregion Total Series Label 설정

                // Access the view-type-specific options of the series. 
                ((LineSeriesView)serTotal.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Kind = MarkerKind.Circle;
                ((LineSeriesView)serTotal.View).LineStyle.DashStyle = DashStyle.Solid;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.FillStyle.FillMode = FillMode.Empty;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Size = 10;
                ((LineSeriesView)serTotal.View).LineMarkerOptions.Color = Color.LightGray;
                //((LineSeriesView)serTotal.View).LineMarkerOptions.BorderVisible = true;
                //((LineSeriesView)serTotal.View).LineMarkerOptions.BorderColor = Color.Black;
                serTotal.View.Color = Color.Blue;

                var baseLinq = dv.ToTable().AsEnumerable();

                var vProductType = (from dr in baseLinq
                                    select new
                                    {
                                        PType = dr.Field<string>("PRODUCTTYPE")
                                    }
                                    ).Distinct();

                var vMonth = from dr in baseLinq
                             select new
                             {
                                 GUBUN = dr.Field<string>("DAYGUBUN"),
                                 PRODTYPE = dr.Field<string>("PRODUCTTYPE"),
                                 RATE = dr.Field<decimal>("RATE"),

                             };

                var vGroup = from dr in baseLinq
                             group dr by new
                             {
                                 GUBUN = dr.Field<string>("DAYGUBUN"),
                             } into g
                             select new
                             {
                                 GUBUN = g.Key.GUBUN,
                                 RECEIVEQTY = g.Sum(x => x.Field<int>("RECEIVEPCSQTY")),
                                 SENDQTY = g.Sum(x => x.Field<int>("SENDPCSQTY"))
                             };

                foreach (var row in vGroup)
                {
                    double dPercent = 0.00;
                    if (row.RECEIVEQTY != 0)
                        dPercent = 100 * ((double)row.SENDQTY / (double)row.RECEIVEQTY);

                    serTotal.Points.Add(new SeriesPoint(row.GUBUN, Math.Round(dPercent, 2)));
                }

                foreach (var row in vMonth)
                {
                    if (row.PRODTYPE.Equals("SS"))
                        serSS.Points.Add(new SeriesPoint(row.GUBUN, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("DS"))
                        serDS.Points.Add(new SeriesPoint(row.GUBUN, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("RF"))
                        serRF.Points.Add(new SeriesPoint(row.GUBUN, Math.Round(100 * row.RATE, 2)));
                    else if (row.PRODTYPE.Equals("MT"))
                        serMT.Points.Add(new SeriesPoint(row.GUBUN, Math.Round(100 * row.RATE, 2)));
                }

                chartYiedRateDay.Series.Add(serSS);
                chartYiedRateDay.Series.Add(serDS);
                chartYiedRateDay.Series.Add(serMT);
                chartYiedRateDay.Series.Add(serRF);
                chartYiedRateDay.Series.Add(serTotal);

                // 범례 설정
                chartYiedRateDay.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
                chartYiedRateDay.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
                chartYiedRateDay.Legend.Direction = LegendDirection.LeftToRight;
                chartYiedRateDay.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                //((SideBySideBarSeriesView)chartYiedRateMonth.SeriesTemplate.View).BarWidth = 50;

                //chartYiedRateDay.BorderOptions.Color = Color.Transparent;

                XYDiagram diagram = ((XYDiagram)chartYiedRateDay.Diagram);

                max = (double)vMonth.Select(x => x.RATE).Distinct().ToList().Max();
                max = Math.Round(100 * max, 2);

                diagram.AxisY.WholeRange.MinValue = 0;
                diagram.AxisY.WholeRange.MaxValue = max + 10;

                // Define the whole range for the X-axis.  
                diagram.AxisX.WholeRange.Auto = false;
                diagram.AxisX.WholeRange.SetMinMaxValues(vMonth.Select(x => x.GUBUN).Distinct().ToList().Min(), vMonth.Select(x => x.GUBUN).Distinct().ToList().Max());
                //diagram.DefaultPane.BorderVisible = false;
            }

            chartYiedRateMonth.SeriesSelectionMode = SeriesSelectionMode.Argument;
            chartYiedRateMonth.SelectionMode = ElementSelectionMode.Single;

            chartYiedRateWeek.SeriesSelectionMode = SeriesSelectionMode.Argument;
            chartYiedRateWeek.SelectionMode = ElementSelectionMode.Single;

            chartYiedRateDay.SeriesSelectionMode = SeriesSelectionMode.Argument;
            chartYiedRateDay.SelectionMode = ElementSelectionMode.Single;

            #endregion
        }

        /// <summary>
        /// 그리드 데이터 설정
        /// </summary>
        /// <param name="dt">데이터 테이블</param>
        public void SetGridDataSource(DataTable dt)
        {
            grdDayYieldRate.View.ClearDatas();

            grdDayYieldRate.DataSource = dt;

        }

        #endregion

        #region Test



        public DataTable CreateTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Type", typeof(string));
            tbl.Columns.Add("Day", typeof(string));
            tbl.Columns.Add("DayType", typeof(string));
            tbl.Columns.Add("Value", typeof(double));

            Random r = new Random();
            DateTime dt = DateTime.Today;
            Random rd = new Random(100);

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("8월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("8월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("8월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("8월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("9월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("9월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("9월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("9월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10월"), "Month", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });


            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("33주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("33주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("33주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("33주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("34주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("34주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("34주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("34주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("35주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("35주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("36주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("35주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("36주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("36주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("36주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("36주"), "Week", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10/10"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10/10"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10/10"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10/10"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10/11"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10/11"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10/11"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10/11"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10/12"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10/12"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10/12"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10/12"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10/13"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10/13"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10/13"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10/13"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10/14"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10/14"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10/14"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10/14"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10/15"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10/15"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10/15"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10/15"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            tbl.Rows.Add(new object[] { string.Format("S/S"), string.Format("10/16"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("D/S"), string.Format("10/16"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("M/T"), string.Format("10/16"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });
            tbl.Rows.Add(new object[] { string.Format("R/F"), string.Format("10/16"), "Day", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

            return tbl;
        }

        public DataTable CreateGridTable(DataTable dt)
        {
            var vBase = dt.AsEnumerable();

            var vDayGroup = from dr in vBase
                            group dr by new
                            {
                                DAYGUBUN = dr.Field<string>("DAYGUBUN"),
                                DAYTYPE = dr.Field<string>("DAYTYPE"),
                                //PORDTYPE = dr.Field<string>("PRODUCTTYPE"),
                                //RATE = dr.Field<Decimal>("RATE"),
                            } into g
                            select new
                            {
                                GUBUN = g.Key.DAYGUBUN,
                                DTYPE = g.Key.DAYTYPE,
                                INCOMEQTY = g.Sum(x => x.Field<int>("RECEIVEPCSQTY")),
                                SENDQTY = g.Sum(x => x.Field<int>("SENDPCSQTY")),
                                DEFECTCOUNT = g.Sum(x => x.Field<int>("DEFECTQTY")),
                            }
                        ;

            var vProducTypeGroup = from dr in vBase
                                   group dr by new
                                   {
                                       DAYGUBUN = dr.Field<string>("DAYGUBUN"),
                                       PRODUCTTYPE = dr.Field<string>("PRODUCTTYPE"),
                                   } into g
                                   select new
                                   {
                                       GUBUN = g.Key.DAYGUBUN,
                                       PTYPE = g.Key.PRODUCTTYPE,
                                       SUMRATE = g.Sum(x => x.Field<decimal?>("RATE"))
                                   }
                        ;


            var vTotalGroup = from dr in vBase
                              group dr by new
                         {
                             GUBUN = dr.Field<string>("DAYGUBUN"),
                         } into g
                         select new
                         {
                             GUBUN = g.Key.GUBUN,
                             INPUTQTY = (double)g.Sum(x => x.Field<int>("RECEIVEPCSQTY")),
                             RATE  = Math.Round( (double)g.Sum(x => x.Field<int>("SENDPCSQTY")) /(double)g.Sum(x => x.Field<int>("RECEIVEPCSQTY")), 5 ),
                         };

            //var vDaySum = from dr in vBase

            //              ;


            //var group = from t1 in changed.Rows.Cast<DataRow>()
            //            group t1 by new { PARNETMENUID = t1.Field<String>("PARENTMENUID") } into grp
            //            select new
            //            {
            //                PARENTMENUID = grp.Key.PARNETMENUID
            //            };

            DataTable tbl = new DataTable();
            tbl.Columns.Add("DAYCATEGORY", typeof(string));
            tbl.Columns.Add("DATETYPE", typeof(string));
            tbl.Columns.Add("YIELD_TOTAL", typeof(string));
            tbl.Columns.Add("YIELDSS", typeof(double));
            tbl.Columns.Add("YIELDDS", typeof(double));
            tbl.Columns.Add("YIELDMT", typeof(double));
            tbl.Columns.Add("YIELDRF", typeof(double));
            tbl.Columns.Add("INCOMEQTY", typeof(double));
            tbl.Columns.Add("SENDQTY", typeof(double));
            tbl.Columns.Add("DEFECTCOUNT", typeof(double));

            string strYear = Language.GetDictionary("YEAR").Name;
            string strMonth = Language.GetDictionary("NMONTH").Name;
            string strWeek = Language.GetDictionary("WEEK").Name;
            string[] arrSplit;
            string serName = string.Empty;

            foreach (var row in vDayGroup)
            {
                DataRow nRow = tbl.NewRow();

                if (row.DTYPE.Equals("MONTH"))
                {
                    arrSplit = row.GUBUN.Split('-');

                    serName = arrSplit[0] + strYear + " " + arrSplit[1] + strMonth;
                }
                else if (row.DTYPE.Equals("WEEK"))
                {
                    arrSplit = row.GUBUN.Split(' ');

                    serName = arrSplit[0] + strYear + " " + arrSplit[1] + strWeek;
                }
                else
                    serName = row.GUBUN;

                nRow["DAYCATEGORY"] = serName;
                nRow["DATETYPE"] = row.DTYPE;
                if (row.INCOMEQTY == 0)
                {
                    nRow["INCOMEQTY"] = DBNull.Value;
                    nRow["SENDQTY"] = DBNull.Value;
                    nRow["DEFECTCOUNT"] = DBNull.Value;
                }
                else
                {
                    nRow["INCOMEQTY"] = row.INCOMEQTY;
                    nRow["SENDQTY"] = row.SENDQTY;
                    nRow["DEFECTCOUNT"] = row.DEFECTCOUNT;
                }

                foreach (var tRow in vTotalGroup)
                {
                    if (row.GUBUN.Equals(tRow.GUBUN))
                    {
                        if(tRow.INPUTQTY == 0)
                            nRow["YIELD_TOTAL"] = DBNull.Value;
                        else
                            nRow["YIELD_TOTAL"] = Math.Round(tRow.RATE, 5);
                    }
                }

                foreach (var pRow in vProducTypeGroup)
                {
                    if (row.GUBUN.Equals(pRow.GUBUN))
                    {
                        if (row.INCOMEQTY == 0)
                        {
                            if (pRow.PTYPE.Equals("SS"))
                                nRow["YIELDSS"] = DBNull.Value;
                            else if (pRow.PTYPE.Equals("DS"))
                                nRow["YIELDDS"] = DBNull.Value;
                            else if (pRow.PTYPE.Equals("RF"))
                                nRow["YIELDRF"] = DBNull.Value;
                            else if (pRow.PTYPE.Equals("MT"))
                                nRow["YIELDMT"] = DBNull.Value;
                        }
                        else
                        {
                            if (pRow.PTYPE.Equals("SS"))
                                nRow["YIELDSS"] = pRow.SUMRATE;
                            else if (pRow.PTYPE.Equals("DS"))
                                nRow["YIELDDS"] = pRow.SUMRATE;
                            else if (pRow.PTYPE.Equals("RF"))
                                nRow["YIELDRF"] = pRow.SUMRATE;
                            else if (pRow.PTYPE.Equals("MT"))
                                nRow["YIELDMT"] = pRow.SUMRATE;
                        }
                    }

                }
                tbl.Rows.Add(nRow);
            }


            return tbl;
        }

        #endregion
    }
}
