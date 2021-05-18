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
using DevExpress.Utils;

namespace Micube.SmartMES.QualityAnalysis
{
    public partial class ucLotYieldRate : DevExpress.XtraEditors.XtraUserControl
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

        public Framework.SmartControls.SmartButton ButtonLOTAnalysis
        {
            get { return this.btnLotAnalysis; }
        }

        public Framework.SmartControls.SmartChart ChartLotYieldRate
        {
            get { return this.chartLotYieldRate; }
        }

        public Framework.SmartControls.SmartChart ChartDefectCodeRate
        {
            get { return this.chartDefectCodeRate; }
        }
        #endregion

        #region Local Members

        // Access the Default ToolTipController. 
        ToolTipController mToolTIpCtrl = ToolTipController.DefaultController;

        #endregion

        public ucLotYieldRate()
        {
            InitializeComponent();

            // Customize the controller's settings. 
            mToolTIpCtrl.Appearance.BackColor = Color.AntiqueWhite;
            mToolTIpCtrl.ShowBeak = true;
            // Set a tooltip for the TextBox control. 
            //defController.SetToolTip(textBox1, "A hint for the Standard TextBox");
        }

        private void ucLotYieldRate_Load(object sender, EventArgs e)
        {
            #region LOT 수율 차트 설정

            chartLotYieldRate.Legend.Visible = false;

            chartLotYieldRate.SeriesSelectionMode = SeriesSelectionMode.Series;
            chartLotYieldRate.SelectionMode = ElementSelectionMode.Multiple;
            chartLotYieldRate.SetUseZoomAndScroll();

            chartLotYieldRate.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            //chartLotYieldRate.CrosshairOptions.ShowValueLine = true;
            //chartLotYieldRate.CrosshairOptions.ShowCrosshairLabels = true;
            //chartLotYieldRate.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.False;
            //chartLotYieldRate.ToolTipOptions.ShowForPoints = true;

            chartLotYieldRate.RuntimeHitTesting = true;
            
            #endregion

            #region 불량코드 차트 설정

            chartDefectCodeRate.SeriesSelectionMode = SeriesSelectionMode.Point;
            chartDefectCodeRate.SelectionMode = ElementSelectionMode.Single;

            chartDefectCodeRate.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            //chartDefectYeildRate.CrosshairOptions.ContentShowMode = CrosshairContentShowMode.Label;

            chartDefectCodeRate.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            chartDefectCodeRate.ToolTipOptions.ShowForPoints = true;

            // Hide the legend (if necessary). 
            chartDefectCodeRate.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
            chartDefectCodeRate.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chartDefectCodeRate.Legend.Direction = LegendDirection.LeftToRight;

            chartDefectCodeRate.Legend.Visible = true;

            chartDefectCodeRate.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            chartDefectCodeRate.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;

            #endregion

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            chartLotYieldRate.CustomDrawSeriesPoint += (s, e) =>
            {
                var x = e.SeriesPoint.Values[0];
                var vLot = e.Series.Name;
                //((DevExpress.XtraCharts.BarDrawOptions)e.SeriesDrawOptions).FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
                e.Series.ToolTipPointPattern = vLot+ " : {V}";
            };


            chartLotYieldRate.MouseMove += (s, e) =>
            {
                ChartControl cht = s as ChartControl;

                // Obtain hit information under the test point. 
                ChartHitInfo hi = cht.CalcHitInfo(e.X, e.Y);

                // Obtain the series point under the test point. 
                SeriesPoint point = hi.SeriesPoint;

                // Check whether the series point was clicked or not. 
                if (point != null)
                {
                    // Obtain the series point argument. 
                    string seriesName = hi.HitObject.ToString();// "Argument: " + point.se.Argument.ToString();

                    // Obtain series point values. 
                    string values = point.Values[0].ToString();
                    //string values = "Value(s): " + point.Values[0].ToString();
                    //if (point.Values.Length > 1)
                    //{
                    //    for (int i = 1; i < point.Values.Length; i++)
                    //    {
                    //        values = values + ", " + point.Values[i].ToString();
                    //    }
                    //}

                    string title = "LOT No";
                    title = title.PadRight(30, ' ') + "수율";

                    // Show the tooltip. 
                    mToolTIpCtrl.ShowHint(seriesName + " : " + values, title);
                }
                else
                {
                    // Hide the tooltip. 
                    mToolTIpCtrl.HideHint();
                }
            };

            chartDefectCodeRate.MouseMove += (s, e) =>
            {
                ChartControl chtDefect = s as ChartControl;

                // Obtain hit information under the test point. 
                ChartHitInfo hi = chtDefect.CalcHitInfo(e.X, e.Y);

                // Obtain the series point under the test point. 
                SeriesPoint point = hi.SeriesPoint;

                // Check whether the series point was clicked or not. 
                if (point != null)
                {
                    // Obtain the series point argument. 
                    string seriesName = hi.HitObject.ToString();// "Argument: " + point.se.Argument.ToString();

                    // Obtain series point values. 
                    string values = point.Values[0].ToString();
                    
                    string strArgs = Convert.ToDateTime(point.Argument.ToString()).ToString("yyyy-MM-dd");

                    DataTable data = chtDefect.DataSource as DataTable;
                    DataView dv = data.DefaultView;
                    dv.RowFilter = "DAYGUBUN = '" + strArgs + "'";

                    data = dv.ToTable(true, new string[] { "LOTID", "DEFECTCODENAME", "LOTDEFECTRATE" });

                    var newDt = from df in data.AsEnumerable()
                                      group df by new
                                      {
                                          LOTID = df.Field<string>("LOTID"),
                                          DFNAME = df.Field<string>("DEFECTCODENAME"),
                                          DR = df.Field<decimal>("LOTDEFECTRATE")
                                      } into g
                                      select new
                                      {
                                          LOTID = g.Key.LOTID,
                                          DEFECTNAME = g.Key.DFNAME,
                                          DEFECTRATE = g.Key.DR
                                      };
                    

                    string strValues = string.Empty;
                    foreach(var d in newDt)
                    {
                        strValues = strValues + d.LOTID + "-" + d.DEFECTNAME + " : " + d.DEFECTRATE + "\n";
                    }

                    string title = "LOT No - Defect Name";
                    title = title.PadRight(30, ' ') + "불량율";



                    // Show the tooltip. 
                    mToolTIpCtrl.ShowHint(strValues, title);
                }
                else
                {
                    // Hide the tooltip. 
                    mToolTIpCtrl.HideHint();
                }
            };

            gbLotYieldRate.ExpandEvent += (s, e) =>
            {
                ucLOTMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            };

            gbLotYieldRate.RestoreEvent += (s, e) =>
            {
                ucLOTMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };

            gbDefectCodeRate.ExpandEvent += (s, e) =>
            {
                ucLOTMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            };

            gbDefectCodeRate.RestoreEvent += (s, e) =>
            {
                ucLOTMainSpliter.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            };
        }

        public void SetData(DataSet ds, string dfsegcd = "")
        {
            chartLotYieldRate.Series.Clear();
            chartDefectCodeRate.Series.Clear();

            if (ds == null) return;

            DataTable dt;

            #region 로트 수율 차트

            if (ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                chartLotYieldRate.DataSource = ds.Tables[0].Copy();
                chartLotYieldRate.SeriesTemplate.ArgumentScaleType = ScaleType.Qualitative;
                //ChartLotYieldRate.SeriesTemplate.SeriesPointsSorting = SortingMode.Ascending;
                //chartLotYieldRate.SeriesDataMember = "LOTID";
                chartLotYieldRate.SeriesTemplate.ArgumentDataMember = "DAYGUBUN";
                //chartLotYieldRate.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "YIELDRATE", "DEFECTRATE" });

                dt = ds.Tables[0].Copy();

                Series serLotYield;
                List<Series> lstLotYield = new List<Series>();

                var baseLinq = from items in dt.AsEnumerable()
                               select items;

                var vLots = from dr in baseLinq
                            select new
                            {
                                LOTID = dr.Field<string>("LOTID"),
                            };

                var vItems = from dr in baseLinq
                             select new
                             {
                                 DATE = dr.Field<string>("DAYGUBUN"),
                                 LOTID = dr.Field<string>("LOTID"),
                                 RATE = dr.Field<decimal>("YIELDRATE"),
                             };

                foreach (var v in vLots)
                {
                    serLotYield = new Series(v.LOTID, ViewType.Point);
                    
                    foreach (var it in vItems)
                    {
                        if (v.LOTID.Equals(it.LOTID))
                        {
                            serLotYield.Points.Add(new SeriesPoint(it.DATE, it.RATE));
                        }
                    }

                    lstLotYield.Add(serLotYield);
                }

                chartLotYieldRate.Series.AddRange(lstLotYield.ToArray());


                #region 불량코드 선택시 해당 LOT 포인트 선택

                if(!string.IsNullOrEmpty(dfsegcd))
                {
                    foreach(DataRow dr in ds.Tables[1].Rows)
                    {
                        string strLOT = dr["LOTID"].ToString();
                        Series ser = chartLotYieldRate.GetSeriesByName(strLOT);
                        chartLotYieldRate.SetObjectSelection(ser);
                    }
                }
                #endregion
            }

            List<decimal> rate = ds.Tables[0].AsEnumerable().Select(s => s.Field<decimal>("YIELDRATE")).Distinct().ToList();
            double min = (double)rate.Min() - 5;
            double max = (double)rate.Max() + 5;

            if (min < 0)
                min = 0;
            if (max > 100)
                max = 100;

            // Access the chart's diagram. 
            XYDiagram diagram = ((XYDiagram)chartLotYieldRate.Diagram);
            // Access the type-specific options of the diagram. 
            diagram.AxisY.WholeRange.MinValue = min;
            diagram.AxisY.WholeRange.MaxValue = max;

            #endregion

            #region 불량 스택 바 차트

            if (ds.Tables.Count > 1 && ds.Tables[1] != null)
            {
                dt = ds.Tables[1].Copy();

                chartDefectCodeRate.DataSource = dt;

                //chartDefectCodeRate.SeriesDataMember = "DEFECTCODENAME";
                //chartDefectCodeRate.SeriesTemplate.ArgumentDataMember = "DAYGUBUN";
                //chartDefectCodeRate.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "DEFECTRATE" });

                chartDefectCodeRate.SeriesTemplate.ArgumentScaleType = ScaleType.Auto;
                chartDefectCodeRate.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.StackedBar);

                foreach(Series s in chartDefectCodeRate.Series)
                {
                    ((StackedBarSeriesView)s.View).BarWidth = 0.2;
                }

                // Series serDefectRate;
                List<Series> lstDefectRate = new List<Series>();

                var baseLinq = from items in dt.AsEnumerable()
                               select items;

                var vSeries = (from dr in baseLinq
                               select new
                               {
                                   DEFECTCODENAME = dr.Field<string>("DEFECTCODENAME")
                               }).Distinct();

                var vDefects = from dr in baseLinq
                               select new
                               {
                                   DATE = dr.Field<string>("DAYGUBUN"),
                                   //LOTID = dr.Field<string>("LOTID"),
                                   DEFECTCODENAME = dr.Field<string>("DEFECTCODENAME"),
                                   RATE = dr.Field<decimal?>("DEFECTRATE"),
                               };

                var dfGrp = from dr in vDefects
                            group dr by new
                            {
                                Date = dr.DATE,
                                DefectNM = dr.DEFECTCODENAME
                            } into g
                            select new
                            {
                                DATE = g.Key.Date,
                                DEFECTNAME = g.Key.DefectNM,
                                RATE = g.Sum(r => r.RATE)
                            };

                Series serDefectRate;

                foreach (var v in vSeries)
                {
                    serDefectRate = new Series(v.DEFECTCODENAME, ViewType.StackedBar);
                    serDefectRate.ArgumentScaleType = ScaleType.Auto;
                    // Access the view-type-specific options of the series. 
                    ((StackedBarSeriesView)serDefectRate.View).BarWidth = 0.2;

                    if (v.DEFECTCODENAME == null) continue;

                    foreach (var it in dfGrp)
                    {
                        if (it.DEFECTNAME == null) continue;

                        if (v.DEFECTCODENAME.Equals(it.DEFECTNAME))
                        {
                            serDefectRate.Points.Add(new SeriesPoint(it.DATE, it.RATE == null ? 0 : it.RATE));
                            //serDefectRate.Label.TextPattern = string.Format("LOTID : {0}, Value : {1}", it.LOTID, it.RATE);
                        }
                    }

                    lstDefectRate.Add(serDefectRate);
                }

                chartDefectCodeRate.Series.AddRange(lstDefectRate.ToArray());

                StackedBarTotalLabel totalLabel = ((XYDiagram)chartDefectCodeRate.Diagram).DefaultPane.StackedBarTotalLabel;
                totalLabel.Visible = true;
                totalLabel.ShowConnector = true;
                totalLabel.TextPattern = "Total:{TV:F2}";
            }

            #endregion
        }

        #region test

        public DataSet CreateData(bool bChecked)
        {
            DataSet ds = new DataSet();

            DataTable lotYieldTable = new DataTable();

            lotYieldTable.Columns.Add("Date", typeof(string));
            lotYieldTable.Columns.Add("LOTID", typeof(string));
            lotYieldTable.Columns.Add("YieldRate", typeof(float));
            
            lotYieldTable.Rows.Add(new object[] { "10-14", string.Format("190910F004-0-FG00-001-001"), 56.6 });
            lotYieldTable.Rows.Add(new object[] { "10-15", string.Format("190910F004-0-BA02-014-000"), 46.8 });
            lotYieldTable.Rows.Add(new object[] { "10-16", string.Format("190910F005-0-FG00-001-039"), 26.2 });
            lotYieldTable.Rows.Add(new object[] { "10-17", string.Format("190910F004-0-FG00-001-001"), 76.7 });
            lotYieldTable.Rows.Add(new object[] { "10-18", string.Format("190910F005-0-FG00-001-A09"), 87.8 });
            lotYieldTable.Rows.Add(new object[] { "10-19", string.Format("190917F001-0-FG00-001-002"), 96.2 });
            lotYieldTable.Rows.Add(new object[] { "10-20", string.Format("190910F005-0-FG00-001-039"), 72.3 });

            ds.Tables.Add(lotYieldTable);

            DataTable lotDefectTable = new DataTable();

            lotDefectTable.Columns.Add("Date", typeof(string));
            lotDefectTable.Columns.Add("LOTID", typeof(string));
            lotDefectTable.Columns.Add("DefectName", typeof(string));
            lotDefectTable.Columns.Add("DefectRate", typeof(float));

            lotDefectTable.Rows.Add(new object[] { "10-14", string.Format("190910F004-0-FG00-001-001"), 56.6 });
            lotDefectTable.Rows.Add(new object[] { "10-14", string.Format("190910F004-0-FG00-001-001"), "눌림-적충", 1.6 });
            lotDefectTable.Rows.Add(new object[] { "10-15", string.Format("190910F004-0-BA02-014-000"), "눌림-적충", 3.8 });
            lotDefectTable.Rows.Add(new object[] { "10-16", string.Format("190910F005-0-FG00-001-039"), "눌림-적충", 2.2 });
            lotDefectTable.Rows.Add(new object[] { "10-17", string.Format("190910F004-0-FG00-001-001"), "눌림-적충", 10.7 });
            lotDefectTable.Rows.Add(new object[] { "10-18", string.Format("190910F005-0-FG00-001-A09"), "눌림-적충", 6.8 });
            lotDefectTable.Rows.Add(new object[] { "10-19", string.Format("190917F001-0-FG00-001-002"), "눌림-적충", 7.2 });
            lotDefectTable.Rows.Add(new object[] { "10-20", string.Format("190910F005-0-FG00-001-039"), "눌림-적충", 3.3 });

            lotDefectTable.Rows.Add(new object[] { "10-14", string.Format("190910F004-0-FG00-001-001"), "구김-공통", 1.6 });
            lotDefectTable.Rows.Add(new object[] { "10-15", string.Format("190910F004-0-BA02-014-000"), "구김-공통", 3.8 });
            lotDefectTable.Rows.Add(new object[] { "10-16", string.Format("190910F005-0-FG00-001-039"), "구김-공통", 2.2 });
            lotDefectTable.Rows.Add(new object[] { "10-17", string.Format("190910F004-0-FG00-001-001"), "구김-공통", 10.7 });
            lotDefectTable.Rows.Add(new object[] { "10-18", string.Format("190910F005-0-FG00-001-A09"), "구김-공통", 6.8 });
            lotDefectTable.Rows.Add(new object[] { "10-19", string.Format("190917F001-0-FG00-001-002"), "구김-공통", 7.2 });
            lotDefectTable.Rows.Add(new object[] { "10-20", string.Format("190910F005-0-FG00-001-039"), "구김-공통", 3.3 });

            lotDefectTable.Rows.Add(new object[] { "10-14", string.Format("190910F004-0-FG00-001-001"), "미현상-PSR", 1.6 });
            lotDefectTable.Rows.Add(new object[] { "10-15", string.Format("190910F004-0-BA02-014-000"), "미현상-PSR", 3.8 });
            lotDefectTable.Rows.Add(new object[] { "10-16", string.Format("190910F005-0-FG00-001-039"), "미현상-PSR", 2.2 });
            lotDefectTable.Rows.Add(new object[] { "10-17", string.Format("190910F004-0-FG00-001-001"), "미현상-PSR", 10.7 });
            lotDefectTable.Rows.Add(new object[] { "10-18", string.Format("190910F005-0-FG00-001-A09"), "미현상-PSR", 6.8 });
            lotDefectTable.Rows.Add(new object[] { "10-19", string.Format("190917F001-0-FG00-001-002"), "미현상-PSR", 7.2 });
            lotDefectTable.Rows.Add(new object[] { "10-20", string.Format("190910F005-0-FG00-001-039"), "미현상-PSR", 3.3 });

            ds.Tables.Add(lotDefectTable);

            return ds;
        }

        

        void createStackedBarAreaChart()
        {
            //chartDefectYeildRate.SeriesDataMember = "DEFECT";
            //chartDefectYeildRate.SeriesTemplate.ArgumentDataMember = "DATE";
            //chartDefectYeildRate.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "DEFECTRATE" });

            //chartDefectYeildRate.SeriesTemplate.ArgumentScaleType = ScaleType.Auto;
            //chartDefectYeildRate.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.StackedBar);

            // Create two stacked bar series. 
            Series series1 = new Series("눌림-적충", ViewType.StackedBar);
            Series series2 = new Series("구김-공통", ViewType.StackedBar);
            Series series3 = new Series("미현상-PSR", ViewType.StackedBar);
            //Series series4 =  new Series("07/29", ViewType.StackedBar);
            //Series series5 =  new Series("07/30", ViewType.StackedBar);
            //Series series6 =  new Series("07/31", ViewType.StackedBar);
            //Series series7 =  new Series("08/01", ViewType.StackedBar);
            //Series series8 =  new Series("08/02", ViewType.StackedBar);
            //Series series9 =  new Series("08/03", ViewType.StackedBar);
            //Series series10=  new Series("08/04", ViewType.StackedBar);
            //Series series11 = new Series("08/05", ViewType.StackedBar);
            //Series series12 = new Series("08/06", ViewType.StackedBar);
            //Series series13 = new Series("08/07", ViewType.StackedBar);
            //Series series14 = new Series("08/08", ViewType.StackedBar);

            // Add points to them 
            series1.Points.Add(new SeriesPoint("07/26", 2.8));
            series1.Points.Add(new SeriesPoint("07/27", 1.5));
            series1.Points.Add(new SeriesPoint("07/28", 5.2));
            series1.Points.Add(new SeriesPoint("07/29", 9.8));
            series1.Points.Add(new SeriesPoint("07/30", 0.9));
            series1.Points.Add(new SeriesPoint("07/31", 4.9));
            series1.Points.Add(new SeriesPoint("08/01", 1));
            series1.Points.Add(new SeriesPoint("08/02", 20));
            series1.Points.Add(new SeriesPoint("08/03", 5));
            series1.Points.Add(new SeriesPoint("08/04", 6));
            series1.Points.Add(new SeriesPoint("08/05", 7));
            series1.Points.Add(new SeriesPoint("08/06", 9));
            series1.Points.Add(new SeriesPoint("08/07", 11));
            series1.Points.Add(new SeriesPoint("08/08", 8));

            series2.Points.Add(new SeriesPoint("07/26", 2.8));
            series2.Points.Add(new SeriesPoint("07/27", 1.5));
            series2.Points.Add(new SeriesPoint("07/28", 5.2));
            series2.Points.Add(new SeriesPoint("07/29", 9.8));
            series2.Points.Add(new SeriesPoint("07/30", 0.9));
            series2.Points.Add(new SeriesPoint("07/31", 4.9));
            series2.Points.Add(new SeriesPoint("08/01", 1));
            series2.Points.Add(new SeriesPoint("08/02", 20));
            series2.Points.Add(new SeriesPoint("08/03", 5));
            series2.Points.Add(new SeriesPoint("08/04", 6));
            series2.Points.Add(new SeriesPoint("08/05", 7));
            series2.Points.Add(new SeriesPoint("08/06", 9));
            series2.Points.Add(new SeriesPoint("08/07", 11));
            series2.Points.Add(new SeriesPoint("08/08", 8));

            series3.Points.Add(new SeriesPoint("07/26", 2.8));
            series3.Points.Add(new SeriesPoint("07/27", 1.5));
            series3.Points.Add(new SeriesPoint("07/28", 5.2));
            series3.Points.Add(new SeriesPoint("07/29", 9.8));
            series3.Points.Add(new SeriesPoint("07/30", 0.9));
            series3.Points.Add(new SeriesPoint("07/31", 4.9));
            series3.Points.Add(new SeriesPoint("08/01", 1));
            series3.Points.Add(new SeriesPoint("08/02", 20));
            series3.Points.Add(new SeriesPoint("08/03", 5));
            series3.Points.Add(new SeriesPoint("08/04", 6));
            series3.Points.Add(new SeriesPoint("08/05", 7));
            series3.Points.Add(new SeriesPoint("08/06", 9));
            series3.Points.Add(new SeriesPoint("08/07", 11));
            series3.Points.Add(new SeriesPoint("08/08", 8));

            // Add both series to the chart. 
            chartDefectCodeRate.Series.AddRange(new Series[] { series1, series2, series3/*, series4, series5, series6, series7, series8, series9, series10, series11, series12, series13, series14 */});

            // Access the view-type-specific options of the series. 
            //((StackedBarSeriesView)series1.View).BarWidth = 0.8;

            // Access the type-specific options of the diagram. 
            //((XYDiagram)chartLotYield.Diagram).EnableAxisXZooming = true;

            // Hide the legend (if necessary). 
            chartDefectCodeRate.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
            chartDefectCodeRate.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            chartDefectCodeRate.Legend.Direction = LegendDirection.LeftToRight;

            chartDefectCodeRate.Legend.Visible = true;

            // Add a title to the chart (if necessary). 
            //chartLotYield.Titles.Add(new ChartTitle());
            //chartLotYield.Titles[0].Text = "A Stacked Bar Chart";

            // Add the chart to the form. 
            //chartLotYield.Dock = DockStyle.Fill;
            //this.Controls.Add(chartLotYield);

        }

        public DataTable CreateGridTable()
        {
            Dictionary<string, string> dicDefectCode = new Dictionary<string, string>();
            dicDefectCode.Add("009", "기포(자재)");
            dicDefectCode.Add("010", "이색(자재)");
            dicDefectCode.Add("011", "구김(자재)");
            dicDefectCode.Add("012", "스크래치(자재)");
            dicDefectCode.Add("013", "두께 불량(자재)");
            dicDefectCode.Add("014", "CRACK(자재)");
            dicDefectCode.Add("015", "환경유해물질 불량(자재)");
            dicDefectCode.Add("016", "홀미관통");
            dicDefectCode.Add("017", "홀누락");
            dicDefectCode.Add("018", "홀쏠림");
            dicDefectCode.Add("019", "홀터짐");
            dicDefectCode.Add("020", "홀SIZE");

            DataTable tbl = new DataTable();
            tbl.Columns.Add("DEFECT", typeof(string));
            tbl.Columns.Add("DATE", typeof(DateTime));
            tbl.Columns.Add("LOTID", typeof(string));
            tbl.Columns.Add("DEFCTCOUNT", typeof(int));
            tbl.Columns.Add("DEFECTRATE", typeof(float));

            Random r = new Random();
            DateTime dt;
            Random rd = new Random(100);

            foreach(string dfCd in dicDefectCode.Keys)
            {
                dt = DateTime.Today.AddDays(-14);

                while (dt <= DateTime.Today)
                {
                    tbl.Rows.Add(new object[] { dicDefectCode[dfCd].ToString(), dt, "LOTID01", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100, Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100});
                    tbl.Rows.Add(new object[] { dicDefectCode[dfCd].ToString(), dt, "LOTID02", Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100, Math.Round(rd.NextDouble(), 3, MidpointRounding.AwayFromZero) * 100 });

                    dt = dt.AddDays(1);
                }
            }
            
            return tbl;
        }

        private void createLotChartData()
        {
            chartLotYieldRate.SeriesDataMember = "LOTID";
            chartLotYieldRate.SeriesTemplate.ArgumentDataMember = "DATE";
            chartLotYieldRate.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "DEFECTRATE" });

            chartLotYieldRate.SeriesTemplate.ArgumentScaleType = ScaleType.Auto;
            chartLotYieldRate.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.Point);

            //chartLotYieldRate.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;
            //chartLotYieldRate.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            //chartLotYieldRate.Legend.Direction = LegendDirection.LeftToRight;

            chartLotYieldRate.Legend.Visible = false;

            chartLotYieldRate.SeriesSelectionMode = SeriesSelectionMode.Point;
            chartLotYieldRate.SelectionMode = ElementSelectionMode.Single;

            //Series ptSeries = new Series("Point Test", ViewType.Point);

            ////ptSeries.Points.Add(new SeriesPoint("07/26", 75.6));
            ////ptSeries.Points.Add(new SeriesPoint("07/26", 78.6));
            ////ptSeries.Points.Add(new SeriesPoint("07/26", 35.6));
            //ptSeries.Points.Add(new SeriesPoint("07/26", 75.6));
            //ptSeries.Points.Add(new SeriesPoint("07/27", 49.6));
            //ptSeries.Points.Add(new SeriesPoint("07/28", 87.5));
            //ptSeries.Points.Add(new SeriesPoint("07/29", 90.1));
            //ptSeries.Points.Add(new SeriesPoint("07/30", 55.6));
            //ptSeries.Points.Add(new SeriesPoint("07/31", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/01", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/02", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/03", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/04", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/05", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/06", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/07", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/08", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/09", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/10", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/11", 62.9));
            //ptSeries.Points.Add(new SeriesPoint("08/12", 62.9));


            //chartLotYieldRate.Series.Add(ptSeries);
        }

        #endregion
    }
}
