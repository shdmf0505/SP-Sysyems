#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 작업 실적 조회
    /// 업  무  설  명  : 재공데이터를 일자별 실적 및 재공으로 구분하여 보여주는 화면
    /// 생    성    자  : 정승원
    /// 생    성    일  : 2019-09-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ReturnProductResult : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ReturnProductResult()
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
            InitializeGrid();
        }

        /// <summary>        
        /// 공정 / 작업장 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdReturn.GridButtonItem = GridButtonItem.Export;
            grdReturn.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdReturn.View.SetIsReadOnly();

            var defaultCol = grdReturn.View.AddGroupColumn("");
            //layer
            defaultCol.AddTextBoxColumn("LAYER", 60).SetTextAlignment(TextAlignment.Left);
            //품목코드
            defaultCol.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Left);
            //품목버전
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
            //품목명
            defaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 150);


            //기초
            var baseCol = grdReturn.View.AddGroupColumn("BASICS");
            baseCol.AddSpinEditColumn("BASE_QTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("QTY");
            baseCol.AddSpinEditColumn("BASE_MM", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            //투입
            var InputCol = grdReturn.View.AddGroupColumn("INPUT");
            InputCol.AddSpinEditColumn("INPUT_QTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("QTY");
            InputCol.AddSpinEditColumn("INPUT_MM", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            //출고
            var OutCol = grdReturn.View.AddGroupColumn("OUTBOUND");
            OutCol.AddSpinEditColumn("FINISH_QTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("QTY");
            OutCol.AddSpinEditColumn("FINISH_MM", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            //불량
            var DefectCol = grdReturn.View.AddGroupColumn("DEFECT");
            DefectCol.AddSpinEditColumn("DEFECTQTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("QTY");
            DefectCol.AddSpinEditColumn("DEFECTMM", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            //완불
            var AllDefectCol = grdReturn.View.AddGroupColumn("ALLDEFECT");
            AllDefectCol.AddSpinEditColumn("ALLDEFECTQTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("QTY");
            AllDefectCol.AddSpinEditColumn("ALLDEFECTMM", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            //폐기
            var ScrapCol = grdReturn.View.AddGroupColumn("SCRAP");
            ScrapCol.AddSpinEditColumn("SCRAPQTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("QTY");
            ScrapCol.AddSpinEditColumn("SCRAPMM", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");

            //기말
            var EndingCol = grdReturn.View.AddGroupColumn("ENDING");
            EndingCol.AddSpinEditColumn("ENDING_QTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("QTY");
            EndingCol.AddSpinEditColumn("ENDING_MM", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            EndingCol.AddSpinEditColumn("ENDING_AMOUNT", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("AMOUNTS");

            //
            grdReturn.View.PopulateColumns();
        }

        #endregion

        #region 이벤트
        private void InitializeEvent()
        {
            grdReturn.View.RowStyle += View_RowStyle;
        }

        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (Micube.Framework.Format.GetTrimString(grdReturn.View.GetRowCellValue(e.RowHandle, "LAYER")) == Language.Get("SUM"))
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.HighPriority = true;
            }
            else if (Micube.Framework.Format.GetTrimString(grdReturn.View.GetRowCellValue(e.RowHandle, "LAYER")) == Language.Get("TOTAL")) 
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.HighPriority = true;
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            string Date = Convert.ToDateTime(Conditions.GetControl<SmartDateEdit>("p_yearmonth").EditValue.ToString()).ToString("yyyy-MM");

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if(values.ContainsKey("P_YEARMONTH"))
            {
                values.Remove("P_YEARMONTH");
                values.Add("P_YEARMONTH", Date);
            }

            DataTable result = await SqlExecuter.QueryAsyncDirect("SelectReturnProductResult", "10001", values);

            if (result.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdReturn.DataSource = result;

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            InitializeCondition_Yearmonth();
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


        #region Private Function
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("SEARCHMONTH")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(0.1)
               .SetDefault(strym);
        }
        #endregion
    }
}