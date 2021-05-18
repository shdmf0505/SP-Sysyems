#region using

using DevExpress.XtraCharts;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System;
using System.Drawing;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 인시생산성
    /// 업  무  설  명  : 
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-11-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class HumanTimeProductivityChart : SmartConditionManualBaseForm
	{
        #region ◆ Local Variables |
        private const int CHART_HEIGHT = 300;

        // 차트 콜렉션(집계기준, 집계기간, 항목; 예;FACTORY, YEAR, P1)
        private Dictionary<string, Dictionary<string, Dictionary<string, SmartChart>>> charts = new Dictionary<string, Dictionary<string, Dictionary<string, SmartChart>>>();
        private Dictionary<string, Dictionary<string, DataTable>> dataTables = new Dictionary<string, Dictionary<string, DataTable>>();

        // 표시된 데이터의 기간
        private int selectedYear;
        private int selectedMonth;
        private int selectedWeek;
        #endregion

        #region ◆ 생성자 |

        public HumanTimeProductivityChart()
		{
			InitializeComponent();
		}

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();
            InitializePanels();

            grdFactory.GridButtonItem = GridButtonItem.Export;
            grdVendor.GridButtonItem = GridButtonItem.Export;
            grdProcessGroup.GridButtonItem = GridButtonItem.Export;
        }

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid(SmartBandedGrid grid, DataTable table, string idColumn, string nameColumn)
        {
            grid.View.ClearColumns();
            grid.GridButtonItem = GridButtonItem.Export;

            grid.View.SetIsReadOnly();
            grid.View.DateTimeColumnTextByTimeZone = true;

            grid.View.AddTextBoxColumn(idColumn, 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grid.View.AddTextBoxColumn(nameColumn, 120);
            grid.View.AddTextBoxColumn("TYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grid.View.AddTextBoxColumn("TYPENAME", 80).SetTextAlignment(TextAlignment.Center);
            grid.View.AddTextBoxColumn("UNIT", 80).SetTextAlignment(TextAlignment.Center);

            foreach (DataColumn col in table.Columns)
            {
                if(col.ColumnName != idColumn && col.ColumnName != "TYPE" && col.ColumnName != "UNIT" && col.ColumnName != "TYPENAME" && col.ColumnName != nameColumn)
                {
                    grid.View.AddSpinEditColumn(col.ColumnName, 80).SetLabel(col.Caption);
                }
            }

            grid.View.PopulateColumns();
        } 

        private void InitializePanels()
        {
            // 공장별
            pnlFactory.AutoScroll = false;
            pnlFactory.HorizontalScroll.Enabled = false;
            pnlFactory.HorizontalScroll.Visible = false;
            pnlFactory.HorizontalScroll.Maximum = 0;
            pnlFactory.AutoScroll = true;

            // 공정별
            pnlProcessGroup.AutoScroll = false;
            pnlProcessGroup.HorizontalScroll.Enabled = false;
            pnlProcessGroup.HorizontalScroll.Visible = false;
            pnlProcessGroup.HorizontalScroll.Maximum = 0;
            pnlProcessGroup.AutoScroll = true;

            // 협력사별
            pnlVendor.AutoScroll = false;
            pnlVendor.HorizontalScroll.Enabled = false;
            pnlVendor.HorizontalScroll.Visible = false;
            pnlVendor.HorizontalScroll.Maximum = 0;
            pnlVendor.AutoScroll = true;

            splitContainer2.SplitterDistance = (int)(pnlFactory.Width * (3.0 / (3 + 12 + 4 + 7)));
            splitContainer3.SplitterDistance = (int)(pnlFactory.Width * (12.0 / (3 + 12 + 4 + 7)));
            splitContainer4.SplitterDistance = (int)(pnlFactory.Width * (4.0 / (3 + 12 + 4 + 7)));

            splitContainer5.SplitterDistance = (int)(pnlFactory.Width * (3.0 / (3 + 12 + 4 + 7)));
            splitContainer6.SplitterDistance = (int)(pnlFactory.Width * (12.0 / (3 + 12 + 4 + 7)));
            splitContainer7.SplitterDistance = (int)(pnlFactory.Width * (4.0 / (3 + 12 + 4 + 7)));

            splitContainer9.SplitterDistance = (int)(pnlFactory.Width * (3.0 / (3 + 12 + 4 + 7)));
            splitContainer10.SplitterDistance = (int)(pnlFactory.Width * (12.0 / (3 + 12 + 4 + 7)));
            splitContainer13.SplitterDistance = (int)(pnlFactory.Width * (4.0 / (3 + 12 + 4 + 7)));
        }

        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            Conditions.GetControl<SmartCheckEdit>("PRODUCTIONRESULT").CheckStateChanged += ChartSeriesOnOff_CheckStateChanged;
            Conditions.GetControl<SmartCheckEdit>("WORKEFFORT").CheckStateChanged += ChartSeriesOnOff_CheckStateChanged;
        }

        private void ChartSeriesOnOff_CheckStateChanged(object sender, System.EventArgs e)
        {
            foreach (var group in this.charts.Values)
            {
                foreach (var period in group.Values)
                {
                    foreach (var chart in period.Values)
                    {
                        SeriesBase result = chart.Series[Language.Get("PRODUCTIONRESULT") + "(km2)"];
                        SeriesBase workEffort = chart.Series[Language.Get("WORKEFFORT") + "(khr)"];
                        if (result != null)
                        {
                            result.Visible = Conditions.GetControl<SmartCheckEdit>("PRODUCTIONRESULT").Checked;
                        }
                        if (workEffort != null)
                        {
                            workEffort.Visible = Conditions.GetControl<SmartCheckEdit>("WORKEFFORT").Checked;
                        }
                        chart.RefreshData();
                    }
                }
            }
        }

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }
        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            var values = Conditions.GetValues();
            this.selectedYear = ((DateTime)values["P_STANDARDDATE"]).Year;
            this.selectedMonth = ((DateTime)values["P_STANDARDDATE"]).Month;
            this.selectedWeek = await GetWeekOfDayInMonthAsync((DateTime)values["P_STANDARDDATE"]);

            switch (tabHumanTimeProductivityChart.SelectedTabPageIndex)
			{
				case 0: // 공장별
                    // 년
                    await SelectYearChartAsync(this.selectedYear, "FACTORY", "FACTORYID", "FACTORYNAME", "FACTORYNAME",
                        splitContainer2.Panel1, splitContainer3.Panel1, splitContainer4.Panel1, splitContainer4.Panel2, splitContainer2, grdFactory);
                    // 월
                    await SelectMonthChartAsync(this.selectedYear, "FACTORY", "FACTORYID", "FACTORYNAME", grdFactory);
                    // 주
                    await SelectWeekChartAsync(this.selectedYear, this.selectedMonth, "FACTORY", "FACTORYID", "FACTORYNAME", grdFactory);
                    // 일
                    await SelectDayChartAsync(this.selectedYear, this.selectedWeek, "FACTORY", "FACTORYID", "FACTORYNAME", grdFactory);
                    break;
				case 1: // 공정별
                    // 년
                    await SelectYearChartAsync(this.selectedYear, "PROCESSGROUP", "PROCESSGROUPID", "PROCESSGROUPNAME", "PROCESSGROUPNAME",
                        splitContainer5.Panel1, splitContainer6.Panel1, splitContainer7.Panel1, splitContainer7.Panel2, splitContainer5, grdProcessGroup);
                    // 월
                    await SelectMonthChartAsync(this.selectedYear, "PROCESSGROUP", "PROCESSGROUPID", "PROCESSGROUPNAME", grdProcessGroup);
                    // 주
                    await SelectWeekChartAsync(this.selectedYear, this.selectedMonth, "PROCESSGROUP", "PROCESSGROUPID", "PROCESSGROUPNAME", grdProcessGroup);
                    // 일
                    await SelectDayChartAsync(this.selectedYear, this.selectedWeek, "PROCESSGROUP", "PROCESSGROUPID", "PROCESSGROUPNAME", grdProcessGroup);
                    break;
				case 2: // 협력사별
                    // 년
                    await SelectYearChartAsync(this.selectedYear, "VENDOR", "VENDORID", "VENDORNAME", "VENDORNAME",
                        splitContainer9.Panel1, splitContainer10.Panel1, splitContainer13.Panel1, splitContainer13.Panel2, splitContainer9, grdVendor);
                    // 월
                    await SelectMonthChartAsync(this.selectedYear, "VENDOR", "VENDORID", "VENDORNAME", grdVendor);
                    // 주
                    await SelectWeekChartAsync(this.selectedYear, this.selectedMonth, "VENDOR", "VENDORID", "VENDORNAME", grdVendor);
                    // 일
                    await SelectDayChartAsync(this.selectedYear, this.selectedWeek, "VENDOR", "VENDORID", "VENDORNAME", grdVendor);
                    break;
			}
        }

        private async Task<int> GetWeekOfDayInMonthAsync(DateTime date)
        {
            var args = new Dictionary<string, object>();
            args.Add("YEAR", date.Year);
            args.Add("MONTH", date.Month);
            args.Add("DAY", date.Day);

            DataTable dtWeek = await SqlExecuter.QueryAsync("GetWeekOfDayInMonth", "10001", args);
            return (int)dtWeek.Rows[0]["WEEK"];
        }

        private async Task SelectYearChartAsync(int year, string groupByType, string groupByColumn, string nameColumn, string titleColumn,
            Panel panel1, Panel panel2, Panel panel3, Panel panel4, SplitContainer container, SmartBandedGrid grid)
        {
            // 차트 객체 초기화
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            panel3.Controls.Clear();
            panel4.Controls.Clear();
            InitializeCharts(groupByType);

            // 데이터 조회(해당 기간의 모든 공장의 데이터)
            var args = new Dictionary<string, object>();
            args.Add("GROUPBY", groupByType);
            args.Add("PERIOD", "YEAR");
            args.Add("YEAR_FR", year - 2); // 3년간 데이터 조회
            args.Add("YEAR_TO", year);
            args.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dataByYear = await SqlExecuter.QueryAsync("SelectLaborProductivityChart", "10001", args);

            // groupByType 별로 테이블 분리
            var tables = SplitTable(dataByYear, groupByColumn);
            tables.Reverse();   // winform 의 레이아웃 규칙에 맞추기 위해 화면에 표시될 역순으로 화면에 추가
            foreach (DataTable table in tables)
            {
                string groupById = table.Rows[0][groupByColumn].ToString();
                string title = table.Rows[0][titleColumn].ToString();
                InitializeSplitPanels(groupByType, groupById, panel1, panel2, panel3, panel4);
                var chart = charts[groupByType]["YEAR"][groupById];
                UpdateChart(chart, title, table);
                chart.ObjectSelected += YearChart_ObjectSelectedAsync;
            }
            container.Height = tables.Count * CHART_HEIGHT;

            this.dataTables[groupByType]["YEAR"] = PivotTable(dataByYear, groupByColumn, nameColumn);

            DataTable concatData = ConcatTable(groupByType, "YEAR", groupByColumn, nameColumn);
            InitializeGrid(grid, concatData, groupByColumn, nameColumn);
            grid.DataSource = concatData;
        }

        private void InitializeCharts(string groupBy)
        {
            charts[groupBy] = new Dictionary<string, Dictionary<string, SmartChart>>();
            charts[groupBy]["YEAR"] = new Dictionary<string, SmartChart>();
            charts[groupBy]["MONTH"] = new Dictionary<string, SmartChart>();
            charts[groupBy]["WEEK"] = new Dictionary<string, SmartChart>();
            charts[groupBy]["DAY"] = new Dictionary<string, SmartChart>();

            dataTables[groupBy] = new Dictionary<string, DataTable>();
            dataTables[groupBy]["YEAR"] = null;
            dataTables[groupBy]["MONTH"] = null;
            dataTables[groupBy]["WEEK"] = null;
            dataTables[groupBy]["DAY"] = null;
        }

        private void InitializeSplitPanels(string groupByType, string itemId, Panel panel1, Panel panel2, Panel panel3, Panel panel4)
        {
            charts[groupByType]["YEAR"][itemId] = new SmartChart();
            charts[groupByType]["MONTH"][itemId] = new SmartChart();
            charts[groupByType]["WEEK"][itemId] = new SmartChart();
            charts[groupByType]["DAY"][itemId] = new SmartChart();

            charts[groupByType]["YEAR"][itemId].Dock = DockStyle.Top;
            charts[groupByType]["MONTH"][itemId].Dock = DockStyle.Top;
            charts[groupByType]["WEEK"][itemId].Dock = DockStyle.Top;
            charts[groupByType]["DAY"][itemId].Dock = DockStyle.Top;

            charts[groupByType]["YEAR"][itemId].Height = CHART_HEIGHT;
            charts[groupByType]["MONTH"][itemId].Height = CHART_HEIGHT;
            charts[groupByType]["WEEK"][itemId].Height = CHART_HEIGHT;
            charts[groupByType]["DAY"][itemId].Height = CHART_HEIGHT;

            panel1.Controls.Add(charts[groupByType]["YEAR"][itemId]);
            panel2.Controls.Add(charts[groupByType]["MONTH"][itemId]);
            panel3.Controls.Add(charts[groupByType]["WEEK"][itemId]);
            panel4.Controls.Add(charts[groupByType]["DAY"][itemId]);
        }

        private void UpdateChart(SmartChart chart, string title, DataTable table, bool hideLegend=true)
        {
            DataTable table2 = table.Copy();

            foreach(DataRow each in table2.Rows)
            {
                if(each["WORKDATE"].ToString().EndsWith("_TARGET"))
                {
                    each["WORKDATE"] = each["WORKDATE"].ToString().Substring(0, each["WORKDATE"].ToString().Length - "_TARGET".Length) + " 목표";
                }
            }

            chart.ClearSeries();

            // 차트 제목
            ChartTitle chartTitle = new ChartTitle();
            chartTitle.Text = title;
            chartTitle.Alignment = System.Drawing.StringAlignment.Center;
            chartTitle.Dock = ChartTitleDockStyle.Top;
            chartTitle.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            chartTitle.Font = new Font("맑은고딕", 12);
            chartTitle.TextColor = Color.Black;
            chart.Titles.Clear();
            chart.Titles.Add(chartTitle);

            // 시리즈 추가
            var resultSeries = ExtractSeries(Language.Get("PRODUCTIONRESULT") + "(km2)",
                                            ViewType.Bar, table2, "WORKDATE", "PRODUCTIONRESULT");
            var workEffortSeries = ExtractSeries(Language.Get("WORKEFFORT") + "(khr)",
                                            ViewType.Line, table2, "WORKDATE", "WORKEFFORT");
            var ratioSeries = ExtractSeries(Language.Get("PERHUMANTIMEPRODUCTIVITY") + "(m2/hr)",
                                            ViewType.Line, table2, "WORKDATE", "PERHUMANTIMEPRODUCTIVITY");

            chart.Series.AddRange(resultSeries, workEffortSeries, ratioSeries);

            // 마커 설정
            ((LineSeriesView)workEffortSeries.View).LineMarkerOptions.Kind = MarkerKind.Circle;
            ((LineSeriesView)workEffortSeries.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((LineSeriesView)ratioSeries.View).LineMarkerOptions.Kind = MarkerKind.Triangle;
            ((LineSeriesView)ratioSeries.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            // 라벨 설정
            resultSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            workEffortSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ratioSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            resultSeries.Label.TextPattern = "{V:N0}";
            workEffortSeries.Label.TextPattern = "{V:N0}";
            ratioSeries.Label.TextPattern = "{V:N0}";

            // 축 설정
            SecondaryAxisY secondAxisY = new SecondaryAxisY("second");
            XYDiagram diagram = chart.Diagram as XYDiagram;
            diagram.SecondaryAxesY.Clear();
            diagram.SecondaryAxesY.Add(secondAxisY);
            ((LineSeriesView)workEffortSeries.View).AxisY = secondAxisY;
            ((LineSeriesView)ratioSeries.View).AxisY = secondAxisY;

            // 기타 설정
            chart.CrosshairOptions.ShowCrosshairLabels = true;
            if (hideLegend)
            {
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
        }

        private Series ExtractSeries(string name, ViewType viewType, DataTable table, string columnX, string columnY)
        {
            Series series = new Series(name, viewType);
            series.ArgumentScaleType = ScaleType.Qualitative;
            foreach(DataRow each in table.Rows)
            {
                if (each[columnY] != DBNull.Value)
                {
                    series.Points.Add(new SeriesPoint(each[columnX].ToString(), each[columnY]));
                }
            }
            return series;
        }

        private async void YearChart_ObjectSelectedAsync(object sender, HotTrackEventArgs e)
        {
            SeriesPoint sp = e.AdditionalObject as SeriesPoint;
            if (sp == null)
            {
                return;
            }
            if (Format.GetString(sp.Argument).EndsWith("목표"))
            {
                return;
            }
            this.selectedYear = Format.GetInteger(sp.Argument);

            switch (tabHumanTimeProductivityChart.SelectedTabPageIndex)
            {
                case 0: // 공장별
                    await SelectMonthChartAsync(this.selectedYear, "FACTORY", "FACTORYID", "FACTORYNAME", grdFactory);
                    break;
                case 1: // 공정별
                    await SelectMonthChartAsync(this.selectedYear, "PROCESSGROUP", "PROCESSGROUPID", "PROCESSGROUPNAME", grdProcessGroup);
                    break;
                case 2: // 협력사별
                    await SelectMonthChartAsync(this.selectedYear, "VENDOR", "VENDORID", "VENDORNAME", grdVendor);
                    break;
            }
        }

        private async Task SelectMonthChartAsync(int year, string groupByType, string groupByColumn, string nameColumn, SmartBandedGrid grid)
        {
            ClearCharts(groupByType, "MONTH");
            ClearCharts(groupByType, "WEEK");
            ClearCharts(groupByType, "DAY");

            var args = new Dictionary<string, object>();
            args.Add("GROUPBY", groupByType);
            args.Add("PERIOD", "MONTH");
            args.Add("YEAR", year);

            DataTable dataByMonth = await SqlExecuter.QueryAsync("SelectLaborProductivityChart", "10001", args);

            // 테이블 공장별로 분리
            var tables = SplitTable(dataByMonth, groupByColumn);
            foreach (DataTable table in tables)
            {
                string itemId = table.Rows[0][groupByColumn].ToString();
                var chart = charts[groupByType]["MONTH"][itemId];
                UpdateChart(chart, year + "년(월간)", table);
                chart.ObjectSelected += MonthChart_ObjectSelectedAsync;
            }

            this.dataTables[groupByType]["MONTH"] = PivotTable(dataByMonth, groupByColumn, nameColumn);

            DataTable concatData = ConcatTable(groupByType, "MONTH", groupByColumn, nameColumn);
            InitializeGrid(grid, concatData, groupByColumn, nameColumn);
            grid.DataSource = concatData;
        }

        private void ClearCharts(string groupBy, string period)
        {
            foreach (SmartChart each in this.charts[groupBy][period].Values)
            {
                each.SetTitle("", isShowTitle: false);
                each.ClearSeries();
            }
        }

        private async void MonthChart_ObjectSelectedAsync(object sender, HotTrackEventArgs e)
        {
            SeriesPoint sp = e.AdditionalObject as SeriesPoint;
            if (sp == null)
            {
                return;
            }
            if (Format.GetString(sp.Argument).EndsWith("목표"))
            {
                return;
            }
            this.selectedMonth = Format.GetInteger(sp.Argument);

            switch (tabHumanTimeProductivityChart.SelectedTabPageIndex)
            {
                case 0: // 공장별
                    await SelectWeekChartAsync(this.selectedYear, this.selectedMonth, "FACTORY", "FACTORYID", "FACTORYNAME", grdFactory);
                    break;
                case 1: // 공정별
                    await SelectWeekChartAsync(this.selectedYear, this.selectedMonth, "PROCESSGROUP", "PROCESSGROUPID", "PROCESSGROUPNAME", grdProcessGroup);
                    break;
                case 2: // 협력사별
                    await SelectWeekChartAsync(this.selectedYear, this.selectedMonth, "VENDOR", "VENDORID", "VENDORNAME", grdVendor);
                    break;
            }
        }

        private async Task SelectWeekChartAsync(int year, int month, string groupByType, string groupByColumn, string nameColumn, SmartBandedGrid grid)
        {
            ClearCharts(groupByType, "WEEK");
            ClearCharts(groupByType, "DAY");

            var args = new Dictionary<string, object>();
            args.Add("GROUPBY", groupByType);
            args.Add("PERIOD", "WEEK");
            args.Add("WEEKYEAR", year);
            args.Add("MONTH", month);

            DataTable dataByWeek = await SqlExecuter.QueryAsync("SelectLaborProductivityChart", "10001", args);

            // 테이블 공장별로 분리
            var tables = SplitTable(dataByWeek, groupByColumn);
            foreach (DataTable table in tables)
            {
                string itemId = table.Rows[0][groupByColumn].ToString();
                var chart = charts[groupByType]["WEEK"][itemId];
                UpdateChart(chart, month + "월(주간)", table);
                chart.ObjectSelected += WeekChart_ObjectSelectedAsync;
            }

            this.dataTables[groupByType]["WEEK"] = PivotTable(dataByWeek, groupByColumn, nameColumn);

            DataTable concatData = ConcatTable(groupByType, "WEEK", groupByColumn, nameColumn);
            InitializeGrid(grid, concatData, groupByColumn, nameColumn);
            grid.DataSource = concatData;
        }

        private async void WeekChart_ObjectSelectedAsync(object sender, HotTrackEventArgs e)
        {
            SeriesPoint sp = e.AdditionalObject as SeriesPoint;
            if (sp == null)
            {
                return;
            }
            if (Format.GetString(sp.Argument).EndsWith("목표"))
            {
                return;
            }
            this.selectedWeek = Format.GetInteger(sp.Argument);

            switch (tabHumanTimeProductivityChart.SelectedTabPageIndex)
            {
                case 0: // 공장별
                    await SelectDayChartAsync(this.selectedYear, this.selectedWeek, "FACTORY", "FACTORYID", "FACTORYNAME", grdFactory);
                    break;
                case 1: // 공정별
                    await SelectDayChartAsync(this.selectedYear, this.selectedWeek, "PROCESSGROUP", "PROCESSGROUPID", "PROCESSGROUPNAME", grdProcessGroup);
                    break;
                case 2: // 협력사별
                    await SelectDayChartAsync(this.selectedYear, this.selectedWeek, "VENDOR", "VENDORID", "VENDORNAME", grdVendor);
                    break;
            }
        }

        private async Task SelectDayChartAsync(int year, int week, string groupByType, string groupByColumn, string nameColumn, SmartBandedGrid grid)
        {
            ClearCharts(groupByType, "DAY");

            var args = new Dictionary<string, object>();
            args.Add("GROUPBY", groupByType);
            args.Add("PERIOD", "DAY");
            args.Add("WEEKYEAR", year);
            args.Add("WEEK", week);

            DataTable dataByDay = await SqlExecuter.QueryAsync("SelectLaborProductivityChart", "10001", args);

            // 테이블 공장별로 분리
            var tables = SplitTable(dataByDay, groupByColumn);
            foreach (DataTable table in tables)
            {
                string itemId = table.Rows[0][groupByColumn].ToString();
                var chart = charts[groupByType]["DAY"][itemId];
                UpdateChart(chart, week + "주(일간)", table);
            }

            this.dataTables[groupByType]["DAY"] = PivotTable(dataByDay, groupByColumn, nameColumn);

            DataTable concatData = ConcatTable(groupByType, "DAY", groupByColumn, nameColumn);
            InitializeGrid(grid, concatData, groupByColumn, nameColumn);
            grid.DataSource = concatData;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
		{
			base.InitializeCondition();

            Conditions.AddCheckEdit("PRODUCTIONRESULT");
            Conditions.AddCheckEdit("WORKEFFORT");
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

            Conditions.GetControl<SmartCheckEdit>("PRODUCTIONRESULT").Checked = true;
            Conditions.GetControl<SmartCheckEdit>("WORKEFFORT").Checked = true;
            Conditions.GetControl<SmartDateEdit>("P_STANDARDDATE").EditValue = DateTime.Today.AddDays(-1);
        }

        #endregion

        #region ◆ Private Function |
        private List<DataTable> SplitTable(DataTable orgTable, string seperatorColumn)
        {
            var result = new List<DataTable>();
            DataTable table = null;
            string prevKey = null;
            foreach(DataRow each in orgTable.Rows)
            {
                string key = each[seperatorColumn].ToString();
                if(prevKey != key)
                {
                    prevKey = key;
                    table = orgTable.Clone();
                    result.Add(table);
                }
                table.ImportRow(each);
            }
            return result;
        }

        private DataTable PivotTable(DataTable orgTable, string idColumn, string nameColumn)
        {
            // 기본 컬럼 설정
            DataTable result = new DataTable();
            result.Columns.Add(idColumn);
            result.Columns.Add(nameColumn);
            result.Columns.Add("TYPE");
            result.Columns.Add("TYPENAME");
            result.Columns.Add("UNIT");

            // 컬럼 추가
            var workDates = orgTable.AsEnumerable().Select(r => Format.GetString(r["WORKDATE"])).Distinct();
            foreach(var each in workDates)
            {
                result.Columns.Add(each.ToString());
            }

            // 기본 row 추가
            foreach(DataRow each in DistinctIdName(orgTable, idColumn, nameColumn).Rows)
            { 
                DataRow newRow1 = result.NewRow();
                newRow1[idColumn] = each["ID"];
                newRow1[nameColumn] = each["NAME"];
                newRow1["TYPE"] = "PRODUCTIONRESULT";
                newRow1["TYPENAME"] = Language.Get("PRODUCTIONRESULT");
                newRow1["UNIT"] = "km2";
                result.Rows.Add(newRow1);

                DataRow newRow2 = result.NewRow();
                newRow2[idColumn] = each["ID"];
                newRow2[nameColumn] = each["NAME"];
                newRow2["TYPE"] = "WORKEFFORT";
                newRow2["TYPENAME"] = Language.Get("WORKEFFORT");
                newRow2["UNIT"] = "khr";
                result.Rows.Add(newRow2);

                DataRow newRow3 = result.NewRow();
                newRow3[idColumn] = each["ID"];
                newRow3[nameColumn] = each["NAME"];
                newRow3["TYPE"] = "PERHUMANTIMEPRODUCTIVITY";
                newRow3["TYPENAME"] = Language.Get("PERHUMANTIMEPRODUCTIVITY");
                newRow3["UNIT"] = "m2/hr";
                result.Rows.Add(newRow3);
            }

            foreach (DataRow row in orgTable.Rows)
            {
                SetPivotData(result, idColumn, row[idColumn].ToString(), "PRODUCTIONRESULT", row["WORKDATE"].ToString(), row["PRODUCTIONRESULT"]);
                SetPivotData(result, idColumn, row[idColumn].ToString(), "WORKEFFORT", row["WORKDATE"].ToString(), row["WORKEFFORT"]);
                SetPivotData(result, idColumn, row[idColumn].ToString(), "PERHUMANTIMEPRODUCTIVITY", row["WORKDATE"].ToString(), row["PERHUMANTIMEPRODUCTIVITY"]);
            }

            return result;
        }

        private void SetPivotData(DataTable table, string idColumn, string id, string type, string workdate, object value)
        {
            foreach(DataRow row in table.Rows)
            {
                if(Format.GetString(row[idColumn]) == id && Format.GetString(row["TYPE"]) == type)
                {
                    row[workdate] = value;
                    break;
                }
            }
        }

        private DataTable ConcatTable(string groupBy, string period, string idColumn, string nameColumn)
        {
            DataTable result = new DataTable();
            result.Columns.Add(idColumn);
            result.Columns.Add(nameColumn);
            result.Columns.Add("TYPE");
            result.Columns.Add("TYPENAME");
            result.Columns.Add("UNIT");

            // 컬럼 추가
            AddColumns(result, groupBy, "YEAR", idColumn, nameColumn, "년", "_Y");
            if (period == "MONTH" || period == "WEEK" || period == "DAY")
            {
                AddColumns(result, groupBy, "MONTH", idColumn, nameColumn, "월", "_M");
            }
            if (period == "WEEK" || period == "DAY")
            {
                AddColumns(result, groupBy, "WEEK", idColumn, nameColumn, "주", "_W");
            }
            if (period == "DAY")
            {
                AddColumns(result, groupBy, "DAY", idColumn, nameColumn, "일", "_D");
            }

            // 기본 행 추가
            AddBaseRows(result, groupBy, "YEAR", idColumn, nameColumn);

            // 데이터 설정
            SetRow(result, groupBy, "YEAR", idColumn, nameColumn, columnSuffix:"_Y");
            if (period == "MONTH" || period == "WEEK" || period == "DAY")
            {
                SetRow(result, groupBy, "MONTH", idColumn, nameColumn, columnSuffix: "_M");
            }
            if (period == "WEEK" || period == "DAY")
            {
                SetRow(result, groupBy, "WEEK", idColumn, nameColumn, columnSuffix: "_W");
            }
            if (period == "DAY")
            {
                SetRow(result, groupBy, "DAY", idColumn, nameColumn, columnSuffix: "_D");
            }

            return result;
        }

        private void AddColumns(DataTable target, string groupBy, string period, string idColumn, string nameColumn, string captionSuffix, string columnSuffix)
        {
            foreach (DataColumn col in this.dataTables[groupBy][period].Columns)
            {
                if (col.ColumnName == idColumn || col.ColumnName == "TYPE" || col.ColumnName == "UNIT" || col.ColumnName == "TYPENAME" || col.ColumnName == nameColumn)
                {
                    continue;
                }

                if (col.ColumnName.EndsWith("_TARGET"))
                {
                    target.Columns.Add(col.ColumnName + columnSuffix);
                    target.Columns[col.ColumnName + columnSuffix].Caption = col.ColumnName.Substring(0, col.ColumnName.Length - "_TARGET".Length) + captionSuffix + " 목표";
                }
                else
                {
                    target.Columns.Add(col.ColumnName + columnSuffix);
                    target.Columns[col.ColumnName + columnSuffix].Caption = col.ColumnName + captionSuffix;
                }
            }
        }
        
        private void AddBaseRows(DataTable target, string groupBy, string period, string idColumn, string nameColumn)
        {
            foreach (DataRow each in DistinctIdName(this.dataTables[groupBy][period], idColumn, nameColumn).Rows)
            {
                DataRow newRow1 = target.NewRow();
                newRow1[idColumn] = each["ID"];
                newRow1[nameColumn] = each["NAME"];
                newRow1["TYPE"] = "PRODUCTIONRESULT";
                newRow1["TYPENAME"] = Language.Get("PRODUCTIONRESULT");
                newRow1["UNIT"] = "km2";
                target.Rows.Add(newRow1);

                DataRow newRow2 = target.NewRow();
                newRow2[idColumn] = each["ID"];
                newRow2[nameColumn] = each["NAME"];
                newRow2["TYPE"] = "WORKEFFORT";
                newRow2["TYPENAME"] = Language.Get("WORKEFFORT");
                newRow2["UNIT"] = "khr";
                target.Rows.Add(newRow2);

                DataRow newRow3 = target.NewRow();
                newRow3[idColumn] = each["ID"];
                newRow3[nameColumn] = each["NAME"];
                newRow3["TYPE"] = "PERHUMANTIMEPRODUCTIVITY";
                newRow3["TYPENAME"] = Language.Get("PERHUMANTIMEPRODUCTIVITY");
                newRow3["UNIT"] = "m2/hr";
                target.Rows.Add(newRow3);
            }
        }

        private void SetRow(DataTable target, string groupBy, string period, string idColumn, string nameColumn, string columnSuffix = "")
        {
            foreach(DataRow row in this.dataTables[groupBy][period].Rows)
            {
                foreach (DataColumn col in this.dataTables[groupBy][period].Columns)
                {
                    if (col.ColumnName != idColumn && col.ColumnName != "TYPE" && col.ColumnName != "UNIT" && col.ColumnName != "TYPENAME" && col.ColumnName != nameColumn)
                    {
                        SetPivotData(target, idColumn, row[idColumn].ToString(), row["TYPE"].ToString(), col.ColumnName + columnSuffix, row[col.ColumnName]);
                    }
                }
            }
        }

        private DataTable DistinctIdName(DataTable table, string idColumn, string nameColumn)
        {
            DataTable result = new DataTable();
            result.Columns.Add("ID");
            result.Columns.Add("NAME");

            foreach(DataRow each in table.Rows)
            {
                if(!ContainsId(result, "ID", each[idColumn].ToString()))
                {
                    var newRow = result.NewRow();
                    newRow["ID"] = each[idColumn];
                    newRow["NAME"] = each[nameColumn];
                    result.Rows.Add(newRow);
                }
            }

            return result;
        }

        private bool ContainsId(DataTable table, string idColumn, string id)
        {
            foreach(DataRow each in table.Rows)
            {
                if(each[idColumn].ToString() == id)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}