using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.Framework;
using Micube.Framework.Net;

using DevExpress.XtraCharts;
using Micube.Framework.SmartControls;
using DevExpress.XtraEditors;

namespace Micube.SmartMES.Test
{
    public partial class SmartChartExample : Form
    {
        public SmartChartExample()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> dicParam;

            dicParam = new Dictionary<string, object> { { "p_id", "OperatingRatePerDay" } };

            smartChart1.AddLineSeries("Operating Rate", SqlExecuter.Procedure("usp_com_selectChartData", dicParam))
                .SetX_DataMember(ScaleType.Qualitative, "DATE")
                .SetY_DataMember(ScaleType.Numerical, "RATE");
            //.SetEachColor();
            //.SetCrosshairPattern(DevExpress.Utils.DefaultBoolean.False)
            //.SetToolTipPattern(DevExpress.Utils.DefaultBoolean.True, "{A} : {V}", "{S}");

            smartChart1.SetVisibleOptions(true, true, false)
                .SetAxisXTitle("Date")
                .SetAxisYTitle("Count")
                .SetAxisXZoom()
                .SetAxisYZoom()
                .SetUseRectangleSelection()
                .SetUseSeriesPointSelection(new string[] { "DATE", "RATE" });

            smartChart1.PopulateSeries();


            //ChartRectangleSelect select = new ChartRectangleSelect(smartChart1, smartCheckBox1);
            //smartChart1.MouseClick += SmartChart1_MouseClick;


            LookUpEdit lookup = new LookUpEdit();
            

            dicParam = new Dictionary<string, object> { { "p_id", "ComprehensiveInformation" } };

            smartChart2.AddPieSeries("Total", SqlExecuter.Procedure("usp_com_selectChartData", dicParam))
                .SetX_DataMember(ScaleType.Qualitative, "STATE")
                .SetY_DataMember(ScaleType.Numerical, "CNT")
                .SetLegendTextPattern("{A}")
                .SetExplodeMode(PieExplodeMode.MaxValue)
                .SetToolTipPattern(true, "{A}: {V}");

            smartChart2.PopulateSeries();

            

            dicParam = new Dictionary<string, object> { { "p_id", "MCCRecordInquiry" } };

            smartChart3.AddGanttSeries("Gantt", SqlExecuter.Procedure("usp_com_selectChartData", dicParam))
                .SetMultiSeries(true, "TYPE")
                .SetX_DataMember(ScaleType.Qualitative, "ARGUMENT")
                .SetY_DataMember(ScaleType.DateTime, "DATEFR", "DATETO")
                .SetCrosshairPattern(true, "{A}: {V1:yyyy-MM-dd HH:mm} ~ {V2:yyyy-MM-dd HH:mm}");
                //.SetDataColumnName("TYPE", "DATEFR", "DATETO");

            //DataTable dtMCCRecordInquiry = SqlExecuter.Procedure("usp_com_selectChartData", dicParam);

            //DataTable dtLot = GetSelectData(dtMCCRecordInquiry, "TYPE", "Lot");
            //DataTable dtProcess = GetSelectData(dtMCCRecordInquiry, "TYPE", "Process");
            //DataTable dtMainPanel_1 = GetSelectData(dtMCCRecordInquiry, "TYPE", "MainPanel_1");
            //DataTable dtMainPanel_2 = GetSelectData(dtMCCRecordInquiry, "TYPE", "MainPanel_2");
            //DataTable dtMainPanel_3 = GetSelectData(dtMCCRecordInquiry, "TYPE", "MainPanel_3");

            //smartChart3.AddGanttSeries("Lot", dtLot)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("TYPE", "DATEFR", "DATETO")
            //    .SetCrosshairPattern("{A}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}");

            //smartChart3.AddGanttSeries("Process", dtProcess)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("TYPE", "DATEFR", "DATETO")
            //    .SetCrosshairPattern("{A}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}");

            //smartChart3.AddGanttSeries("MainPanel_1", dtMainPanel_1)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("TYPE", "DATEFR", "DATETO")
            //    .SetCrosshairPattern("{A}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}");

            //smartChart3.AddGanttSeries("MainPanel_2", dtMainPanel_2)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("TYPE", "DATEFR", "DATETO")
            //    .SetCrosshairPattern("{A}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}");

            //smartChart3.AddGanttSeries("MainPanel_3", dtMainPanel_3)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("TYPE", "DATEFR", "DATETO")
            //    .SetCrosshairPattern("{A}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}");

            smartChart3.PopulateSeries();


            dicParam = new Dictionary<string, object> { { "p_id", "StateChangePeriod" } };

            smartChart4.AddRangeBarSeries("RangeBar", SqlExecuter.Procedure("usp_com_selectChartData", dicParam))
                .SetMultiSeries(true, "STATE")
                //.SetX_DataMember(ScaleType.Qualitative, "ID")
                .SetY_DataMember(ScaleType.DateTime, "DATEFR", "DATETO")
                .SetCrosshairPattern(false)
                .SetToolTipPattern(true, "{A}: {V1:MM/dd tt hh:mm} ~ {V2:MM/dd tt hh:mm}");



            smartChart4.SetRotated(true);
            smartChart4.SetVisibleOptions(false, true);


            smartChart4.PopulateSeries();

            
            //((XYDiagram)smartChart4.Diagram).AxisY.Label.TextPattern = "{V:MM/dd tt hh:mm}";

            dicParam = new Dictionary<string, object> { { "p_id", "WorstestAlarm" } };

            smartChart5.AddStackedBarSeries("StackedBar", SqlExecuter.Procedure("usp_com_selectChartData", dicParam))
                .SetMultiSeries(true, "ALARMID")
                .SetX_DataMember(ScaleType.Qualitative, "EQUIPID")
                .SetY_DataMember(ScaleType.Numerical, new string[] { "CNT" });

            //smartChart5.AddLineSeries("Line", SqlExecuter.Procedure("usp_com_selectChartData", dicParam))
            //    .SetMultiSeries(true, "ALARMID")
            //    .SetX_DataMember(ScaleType.Qualitative, "EQUIPID")
            //    .SetY_DataMember(ScaleType.Numerical, new string[] { "RATE" })
            //    .AddSecondaryAxisY()
            //    .SetCrosshairPattern(false, "{S} : {V:P0}")
            //    .SetLineMarker(true);

            smartChart5.SetUseRectangleSelection();

            smartChart1.SelectionMode = ElementSelectionMode.Extended;
            smartChart1.SeriesSelectionMode = SeriesSelectionMode.Point;

            smartChart1.Validated += (o, args) =>
            {
                List<object> list = smartChart1.GetSelectionPoints("");

                if (list.Count > 0)
                {
                    smartChart1.SelectedItems.Add(list);

                    //LotChartPopup popup = new LotChartPopup();
                    //popup.ShowDialog();
                }
            };


            //smartChart5.SetUseSeriesPointSelection();

            //smartChart5.SetCustomSecondaryAxisLabel(true, "{V:P0}");

            smartChart5.PopulateSeries();

            //DataTable dtStateChangePeriod = SqlExecuter.Procedure("usp_com_selectChartData", dicParam);

            //DataTable dtRun = GetSelectData(dtStateChangePeriod, "STATE", "Run");
            //DataTable dtIdle = GetSelectData(dtStateChangePeriod, "STATE", "Idle");
            //DataTable dtAlarm = GetSelectData(dtStateChangePeriod, "STATE", "Alarm");
            //DataTable dtDown = GetSelectData(dtStateChangePeriod, "STATE", "Down");

            //smartChart4.AddRangeBarSeries("Run", dtRun)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("STATE", "DATEFR", "DATETO")
            //    .SetToolTipPattern("{S}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}", "", DevExpress.Utils.DefaultBoolean.True)
            //    .SetCrosshairPattern("", DevExpress.Utils.DefaultBoolean.False);

            //smartChart4.AddRangeBarSeries("Idle", dtIdle)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("STATE", "DATEFR", "DATETO")
            //    .SetToolTipPattern("{S}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}", "", DevExpress.Utils.DefaultBoolean.True)
            //    .SetCrosshairPattern("", DevExpress.Utils.DefaultBoolean.False);

            //smartChart4.AddRangeBarSeries("Alarm", dtAlarm)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("STATE", "DATEFR", "DATETO")
            //    .SetToolTipPattern("{S}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}", "", DevExpress.Utils.DefaultBoolean.True)
            //    .SetCrosshairPattern("", DevExpress.Utils.DefaultBoolean.False);

            //smartChart4.AddRangeBarSeries("Down", dtDown)
            //    .SetY_DataMember(ScaleType.DateTime, "")
            //    .SetDataColumnName("STATE", "DATEFR", "DATETO")
            //    .SetToolTipPattern("{S}: {V1:yyyy-MM-dd HH:mm} - {V2:yyyy-MM-dd HH:mm}", "", DevExpress.Utils.DefaultBoolean.True)
            //    .SetCrosshairPattern("", DevExpress.Utils.DefaultBoolean.False);

            //smartChart4.PopulateSeries();

            smartChart5.SeriesSelectionMode = SeriesSelectionMode.Point;
            smartChart5.SelectionMode = ElementSelectionMode.Extended;


            smartChart5.ObjectHotTracked += SmartChart5_ObjectHotTracked;
            smartChart5.ObjectSelected += SmartChart5_ObjectSelected;
            smartChart5.SelectedItemsChanged += SmartChart5_SelectedItemsChanged;

            //dicParam = new Dictionary<string, object> { { "p_id", "WorstestAlarm" } };

            //smartChart5.AddStackedBarSeries("", SqlExecuter.Procedure("usp_com_selectChartData", dicParam));

            smartChart6.SetTitle("");

            smartChart6.PopulateSeries();


            smartChart7.SetEmptyChart();


            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("COUNT", typeof(int));
            dt.Columns.Add("TIME", typeof(int));
            dt.Columns.Add("RATE", typeof(decimal));

            dt.Rows.Add("X", 3, 17, 0.27);
            dt.Rows.Add("Y", 5, 23, 0.31);

            dt.AcceptChanges();

            smartChart8.AddLineSeries("COUNT", dt)
                .SetX_DataMember(ScaleType.Qualitative, "ID")
                .SetY_DataMember(ScaleType.Numerical, new string[] { "COUNT" });

            smartChart8.AddLineSeries("TIME", dt)
                .SetX_DataMember(ScaleType.Qualitative, "ID")
                .SetY_DataMember(ScaleType.Numerical, new string[] { "TIME" })
                .AddSecondaryAxisY();

            smartChart8.AddLineSeries("RATE", dt)
                .SetX_DataMember(ScaleType.Qualitative, "ID")
                .SetY_DataMember(ScaleType.Numerical, new string[] { "RATE" })
                .AddSecondaryAxisY(true, AxisAlignment.Near);

            smartChart8.PopulateSeries();
        }

        private void SmartChart5_ObjectHotTracked(object sender, HotTrackEventArgs e)
        {
            if (!(e.Object is Series))
                e.Cancel = true;
        }

        private void SmartChart5_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            if (!(e.Object is Series))
                e.Cancel = true;
        }

        private void SmartChart5_SelectedItemsChanged(object sender, SelectedItemsChangedEventArgs e)
        {
            //if (e.NewItems.Count < 1)
            //    return;

            //if (!(e.NewItems[0] is DataRowView))
            //    return;

            foreach (DataRowView view in e.NewItems)
            {
                ViewPieChart(view.Row["EQUIPID"].ToString());
            }
        }

        private void ViewPieChart(string equipId)
        {
            //Dictionary<string, object> dicParam = new Dictionary<string, object> { { "p_equipId", equipId } };
            //DataTable dt = SqlExecuter.Procedure("", dicParam);

            //smartChart6.ClearSeries();

            //smartChart6.AddPieSeries("Loss Rate", dt)
            //    .SetX_DataMember(ScaleType.Qualitative, "LOSS")
            //    .SetY_DataMember(ScaleType.Numerical, "RATE");

            //smartChart6.PopulateSeries();

            smartChart6.SetDynamicTitle(equipId);
        }

        private void SmartChart1_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = smartChart1.CalcHitInfo(e.Location);
            SeriesPoint point = hi.SeriesPoint;

            SeriesBase series = hi.Series;

            //if (series != null)
            //{

            //}

            if (point != null)
            {
                string strTest = point.Argument;
            }
        }

        private DataTable GetSelectData(DataTable dataTable, string fieldName, string typeName)
        {
            DataTable dtResult = dataTable.Clone();

            foreach (DataRow row in dataTable.Select(string.Format("{0} = '{1}'", fieldName, typeName)))
            {
                DataRow addRow = dtResult.NewRow();
                addRow.ItemArray = row.ItemArray.Clone() as object[];

                dtResult.Rows.Add(addRow);
            }

            return dtResult;
        }

        private DataTable GetDataRate()
        {
            DataTable dtRate = new DataTable("Rate");
            dtRate.Columns.Add("Id", typeof(string));
            dtRate.Columns.Add("Value", typeof(decimal));

            dtRate.Rows.Add("Equip01", 1.3);
            dtRate.Rows.Add("Equip02", 2.1);
            dtRate.Rows.Add("Equip03", 0.3);
            dtRate.Rows.Add("Equip04", 0.7);

            dtRate.AcceptChanges();

            return dtRate;
        }

        private DataTable GetDataCount()
        {
            DataTable dtCount = new DataTable("Count");
            dtCount.Columns.Add("Id", typeof(string));
            dtCount.Columns.Add("Value", typeof(int));

            dtCount.Rows.Add("Equip01", 5);
            dtCount.Rows.Add("Equip02", 7);
            dtCount.Rows.Add("Equip03", 2);
            dtCount.Rows.Add("Equip04", 2);

            dtCount.AcceptChanges();

            return dtCount;


            //DataTable dtCount2 = new DataTable("Count2");
            //dtCount2.Columns.Add("Id", typeof(string));
            //dtCount2.Columns.Add("Value", typeof(int));

            //dtCount2.Rows.Add("Equip01", 3);
            //dtCount2.Rows.Add("Equip02", 1);
            //dtCount2.Rows.Add("Equip03", 4);
            //dtCount2.Rows.Add("Equip04", 6);

            //dtCount2.AcceptChanges();

            //dsChart.Tables.Add(dtCount2);


            //DataTable dtRate = new DataTable("Rate");
            //dtRate.Columns.Add("Id", typeof(string));
            //dtRate.Columns.Add("Value", typeof(decimal));

            //dtRate.Rows.Add("Equip01", 1.3);
            //dtRate.Rows.Add("Equip02", 2.1);
            //dtRate.Rows.Add("Equip03", 0.3);
            //dtRate.Rows.Add("Equip04", 0.7);

            //dtRate.AcceptChanges();

            //dsChart.Tables.Add(dtRate);


            //DataTable dtGantt = new DataTable("Schedule");
            //dtGantt.Columns.Add("Id", typeof(string));
            //dtGantt.Columns.Add("dateFr", typeof(DateTime));
            //dtGantt.Columns.Add("dateTo", typeof(DateTime));

            //dtGantt.Rows.Add("Lot", "2018-11-07 09:00:00", "2018-11-07 18:00:00");
            //dtGantt.Rows.Add("Process", "2018-11-07 09:20:00", "2018-11-07 16:30:00");
            //dtGantt.Rows.Add("MainPanel_1", "2018-11-07 09:40:00", "2018-11-07 10:25:00");
            //dtGantt.Rows.Add("MainPanel_2", "2018-11-07 10:12:00", "2018-11-07 13:27:00");
            //dtGantt.Rows.Add("MainPanel_3", "2018-11-07 13:00:00", "2018-11-07 14:27:00");

            //dtGantt.AcceptChanges();

            //dsChart.Tables.Add(dtGantt);

            //DataTable dtRange = new DataTable("RangeBar");
            //dtRange.Columns.Add("Id", typeof(string));
            //dtRange.Columns.Add("dateFr", typeof(DateTime));
            //dtRange.Columns.Add("dateTo", typeof(DateTime));

            //dtRange.Rows.Add("Down", "2018-11-07 00:00", "2018-11-07 09:00");
            //dtRange.Rows.Add("Alarm", "2018-11-07 09:00", "2018-11-07 09:30");
            //dtRange.Rows.Add("Idle", "2018-11-07 09:30", "2018-11-07 10:00");
            //dtRange.Rows.Add("Down", "2018-11-07 10:00", "2018-11-07 10:30");
            //dtRange.Rows.Add("Alarm", "2018-11-07 10:30", "2018-11-07 11:00");
            //dtRange.Rows.Add("Run", "2018-11-07 11:00", "2018-11-07 14:00");
            //dtRange.Rows.Add("Idle", "2018-11-07 14:00", "2018-11-07 17:00");

            //dtRange.AcceptChanges();

            //dsChart.Tables.Add(dtRange);
        }

        private DataTable GetDataCount2()
        {
            DataTable dtCount2 = new DataTable("Count2");
            dtCount2.Columns.Add("Id", typeof(string));
            dtCount2.Columns.Add("Value", typeof(int));

            dtCount2.Rows.Add("Equip01", 3);
            dtCount2.Rows.Add("Equip02", 1);
            dtCount2.Rows.Add("Equip03", 4);
            dtCount2.Rows.Add("Equip04", 6);

            dtCount2.AcceptChanges();

            return dtCount2;
        }

        private void GetData()
        {
            TableLayoutPanel layout = new TableLayoutPanel();

            layout.Controls.Find<SmartBandedGrid>(true).ForEach(grid =>
            {
                if (grid.Name.IndexOf("Lot") > -1)
                {

                }
                else if (grid.Name.IndexOf("Parameter") > -1)
                {

                }
            });
        }
    }
}
