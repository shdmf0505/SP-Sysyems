#region using

using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

// TODO : 계획에 미투입이 포함되어야 하는데 일별 미투입 정보가 없어서 아직 반영되지 않음

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > 계획 대비 투입 실적 조회
    /// 업  무  설  명  : 계획 대비 투입 실적 조회 화면
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-10-04
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class InputLotRecordPerPlan : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public InputLotRecordPerPlan()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다. 
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMain.View.ClearColumns();
            grdMain.GridButtonItem = GridButtonItem.None;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMain.View.SetIsReadOnly();

            var groupProduct = grdMain.View.AddGroupColumn("");
            // 품목 코드
            groupProduct.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            // 품목 버전
            groupProduct.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsHidden();
            // 품목명
            groupProduct.AddTextBoxColumn("PRODUCTDEFNAME", 265);
            // 고객사명
            groupProduct.AddTextBoxColumn("CUSTOMERNAME", 160);
            // 양산구분
            groupProduct.AddTextBoxColumn("PRODUCTIONTYPE", 90).SetTextAlignment(TextAlignment.Center);

            grdMain.View.PopulateColumns();
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            SearchChart();
            SearchGrid();
        }

        private async void SearchChart()
        {
            this.chaMain.ClearSeries();

            var values = Conditions.GetValues();

            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10);
            values["P_PERIOD_PERIODTO"] = DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)).AddDays(-1).ToString("yyyy-MM-dd");

            // 계획
            DataTable planTable = await SqlExecuter.QueryAsync("GetInputLotRecordPerPlanChartPlan", "10001", values);
            // 실적
            DataTable resultTable = await SqlExecuter.QueryAsync("GetInputLotRecordPerPlanChartResult", "10001", values);

            if (planTable.Rows.Count == 0 && resultTable.Rows.Count == 0)
            {
                this.ShowMessage("NoSelectCondData");
            }
            else
            {
                this.chaMain.AddBarSeries(Language.Get("PLAN"), planTable)
                    .SetX_DataMember(DevExpress.XtraCharts.ScaleType.Auto, "FCSTDATE")
                    .SetY_DataMember(DevExpress.XtraCharts.ScaleType.Numerical, "QTY")
                    .SetSeriesColor(true, "Blue")
                    .SetLegendTextPattern("{A}");

                this.chaMain.AddBarSeries(Language.Get("FIGURE"), resultTable)
                    .SetX_DataMember(DevExpress.XtraCharts.ScaleType.Auto, "INPUTDATE")
                    .SetY_DataMember(DevExpress.XtraCharts.ScaleType.Numerical, "INPUTQTY")
                    .SetSeriesColor(true, "Red")
                    .SetLegendTextPattern("{A}");

                this.chaMain.PopulateSeries();

                XYDiagram diagram = chaMain.Diagram as XYDiagram;
                diagram.AxisY.Label.TextPattern = "{V:#,##0}";

                this.chaMain.SetVisibleOptions(true, true, true).SetAxisInteger(true);
                this.chaMain.CrosshairOptions.ShowCrosshairLabels = true;
            }
        }

        private void SearchGrid()
        {
            var values = Conditions.GetValues();

            if (values["P_PRODUCTDEFID"] == null)
            {
                values["P_PRODUCTDEFID"] = string.Empty;
            }

            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10);
            values["P_PERIOD_PERIODTO"] = DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)).AddDays(-1).ToString("yyyy-MM-dd");
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Remove("P_PERIOD");
            if (!values.ContainsKey("P_LOTPRODUCTTYPESTATUS"))
            {
                values.Add("P_LOTPRODUCTTYPESTATUS", "");
            }

            InitializeGrid();
            AddDateColumnsToGrid(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));
            grdMain.View.PopulateColumns();
            grdMain.View.FixColumn(new string[] { "PRODUCTDEFID", "PRODUCTDEFNAME", "CUSTOMERNAME", "PRODUCTIONTYPE" });

            DataTable dtResult = this.Procedure("usp_wip_selectinputlotrecordperplangrid", values);
            grdMain.DataSource = dtResult;
        }

        private void AddDateColumnsToGrid(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                var groupEachDay = grdMain.View.AddGroupColumn(day.ToString("yyyy-MM-dd"));

                // 실적 PCS
                ConditionItemTextBox rslt_pcs = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_PLAN", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                rslt_pcs.LanguageKey = "PLAN";

                // 실적 PNL
                ConditionItemTextBox rslt_pnl = groupEachDay.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_RSLT", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                rslt_pnl.LanguageKey = "FIGURE";
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            // 품목코드
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.5, false, Conditions);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        #endregion
    }
}
