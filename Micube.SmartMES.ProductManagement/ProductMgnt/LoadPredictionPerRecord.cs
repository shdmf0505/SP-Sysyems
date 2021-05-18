#region using

using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProductManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 생산관리 > 공정부하 > 부하량 대비 실적 조회
    /// 업  무  설  명  : 부하량 대비 실적을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-11-06
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LoadPredictionPerRecord : SmartConditionManualBaseForm
    {
        #region Local Variables
        private Font underlined;
        #endregion

        #region 생성자

        public LoadPredictionPerRecord()
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
            InitializeTotalGrid("#,##0");
            InitializeDetailGrid("#,##0");
        }

        private void InitializeTotalGrid(string format)
        {
            #region 종합 그리드 초기화 |
            grdLoadTotal.View.ClearColumns();
            grdLoadTotal.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdLoadTotal.GridButtonItem = GridButtonItem.Export;
            grdLoadTotal.View.SetIsReadOnly();
            grdLoadTotal.View.OptionsView.AllowCellMerge = true;

            var grpTotalInfo = grdLoadTotal.View.AddGroupColumn("");
            grpTotalInfo.AddTextBoxColumn("LOADTOPSEGMENTCLASSNAME", 130);
            grpTotalInfo.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSNAME", 130);
            grpTotalInfo.AddTextBoxColumn("LOADSMALLSEGMENTCLASSNAME", 130);

            //차이현황
            var grpTotalGap = grdLoadTotal.View.AddGroupColumn("GAPSTATE");
            grpTotalGap.AddSpinEditColumn("GAP_TOTALQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("SUM");
            grpTotalGap.AddSpinEditColumn("GAP_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpTotalGap.AddSpinEditColumn("GAP_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");
            grpTotalGap.AddTextBoxColumn("GAP_RATIO", 80).SetDisplayFormat("P").SetLabel("ACHIEVEMENTRATE").SetTextAlignment(TextAlignment.Right);

            //실적현황
            var grpTotalWorkResult = grdLoadTotal.View.AddGroupColumn("WORKRESULT");
            grpTotalWorkResult.AddSpinEditColumn("WR_TOTALQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("SUM");
            grpTotalWorkResult.AddSpinEditColumn("WR_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpTotalWorkResult.AddSpinEditColumn("WR_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            //예상부하
            var grpTotalLoadSegment = grdLoadTotal.View.AddGroupColumn("LOADSEGMENT");
            grpTotalLoadSegment.AddSpinEditColumn("LS_TOTALQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("SUM");
            grpTotalLoadSegment.AddSpinEditColumn("LS_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpTotalLoadSegment.AddSpinEditColumn("LS_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            //기초재공
            var grpTotalBaseWip = grdLoadTotal.View.AddGroupColumn("BASEWIP");
            grpTotalBaseWip.AddSpinEditColumn("BW_TOTALQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("SUM");
            grpTotalBaseWip.AddSpinEditColumn("BW_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpTotalBaseWip.AddSpinEditColumn("BW_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            //기말재공
            var grpTotalLastWip = grdLoadTotal.View.AddGroupColumn("LASTWIP");
            grpTotalLastWip.AddSpinEditColumn("LW_TOTALQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("SUM");
            grpTotalLastWip.AddSpinEditColumn("LW_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpTotalLastWip.AddSpinEditColumn("LW_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            grdLoadTotal.View.PopulateColumns();
            grdLoadTotal.View.FixColumn(new string[] { "LOADTOPSEGMENTCLASSNAME", "LOADMIDDLESEGMENTCLASSNAME", "LOADSMALLSEGMENTCLASSNAME" });
            #endregion
        }

        private void InitializeDetailGrid(string format)
        {
            #region 상세 그리드 초기화 |
            grdLoadDetail.View.ClearColumns();
            grdLoadDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdLoadDetail.GridButtonItem = GridButtonItem.Export;
            grdLoadDetail.View.SetIsReadOnly();
            grdLoadDetail.View.OptionsView.AllowCellMerge = true;

            var defaultInfo = grdLoadDetail.View.AddGroupColumn("");
            defaultInfo.AddTextBoxColumn("LOADTOPSEGMENTCLASSNAME", 130);
            defaultInfo.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSNAME", 130);
            defaultInfo.AddTextBoxColumn("LOADSMALLSEGMENTCLASSNAME", 130);
            defaultInfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            defaultInfo.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            defaultInfo.AddTextBoxColumn("PRODUCTDEFID", 130);

            //차이현황
            var grpGap = grdLoadDetail.View.AddGroupColumn("GAPSTATE");
            grpGap.AddSpinEditColumn("GAP_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpGap.AddSpinEditColumn("GAP_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            //실적현황
            var grpWorkResult = grdLoadDetail.View.AddGroupColumn("WORKRESULT");
            grpWorkResult.AddSpinEditColumn("WR_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpWorkResult.AddSpinEditColumn("WR_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            //예상부하
            var grpLoadSegment = grdLoadDetail.View.AddGroupColumn("LOADSEGMENT");
            grpLoadSegment.AddSpinEditColumn("LS_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpLoadSegment.AddSpinEditColumn("LS_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            //기초재공
            var grpBaseWip = grdLoadDetail.View.AddGroupColumn("BASEWIP");
            grpBaseWip.AddSpinEditColumn("BW_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpBaseWip.AddSpinEditColumn("BW_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            //기말재공
            var grpLastWip = grdLoadDetail.View.AddGroupColumn("LASTWIP");
            grpLastWip.AddSpinEditColumn("LW_PRODUCTIONQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPEPRD");
            grpLastWip.AddSpinEditColumn("LW_SAMPLEQTY", 80).SetDisplayFormat(format, MaskTypes.Numeric, false).SetLabel("OSPLOTTYPESIM");

            grdLoadDetail.View.PopulateColumns();
            grdLoadDetail.View.FixColumn(new string[] { "LOADTOPSEGMENTCLASSNAME", "LOADMIDDLESEGMENTCLASSNAME", "LOADSMALLSEGMENTCLASSNAME"
                    , "PROCESSSEGMENTNAME", "PRODUCTDEFNAME" });
            #endregion
        }
		#endregion

		#region Event
		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
        {
            grdLoadTotal.View.CellMerge += grdLoadTotalView_CellMerge;
            grdLoadTotal.View.RowStyle += grdLoadTotalView_RowStyle;
            grdLoadTotal.View.RowCellStyle += grdLoadTotalView_RowCellStyle;
            grdLoadTotal.View.RowCellClick += grdLoadTotalView_RowCellClickAsync;

            grdLoadDetail.View.CellMerge += grdLoadDetailView_CellMerge;
            grdLoadDetail.View.RowStyle += grdLoadDetailView_RowStyle;
        }

        private async void grdLoadTotalView_RowCellClickAsync(object sender, RowCellClickEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "TOTAL")
            {
                if (e.Column.FieldName.EndsWith("_PRODUCTIONQTY") || e.Column.FieldName.EndsWith("_SAMPLEQTY"))
                {
                    tabControl.SelectedTabPage = pagDetail;
                    var values = Conditions.GetValues();
                    values.Add("P_TOPSEGMENTCLASSID", row["LOADTOPSEGMENTCLASSID"]);
                    values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

                    if (values["P_QTYTYPE"].ToString() == "M2")
                    {
                        InitializeDetailGrid("#,##0.00");
                    }
                    else
                    {
                        InitializeDetailGrid("#,##0");
                    }

                    if (e.Column.FieldName.EndsWith("_PRODUCTIONQTY"))
                    {
                        ShowDetailGridColumnsForProduction();
                    }
                    else if (e.Column.FieldName.EndsWith("_SAMPLEQTY"))
                    {
                        ShowDetailGridColumnsForSample();
                    }

                    DataTable dtResult = await QueryAsync("SelectLoadPredictionPerRecord", "10001", values);
                    if (dtResult.Rows.Count < 1)
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }
                    grdLoadDetail.DataSource = dtResult;
                }
            }
        }

        private void ShowDetailGridColumnsForProduction()
        {
            ShowDetailGridColumns("_PRODUCTIONQTY");
        }

        private void ShowDetailGridColumnsForSample()
        {
            ShowDetailGridColumns("_SAMPLEQTY");
        }

        private void ShowDetailGridColumns(string suffix)
        {
            foreach(BandedGridColumn col in grdLoadDetail.View.Columns)
            {
                if (col.FieldName.Contains("_"))
                {
                    col.Visible = col.FieldName.EndsWith(suffix);
                }
            }
        }

        /// <summary>
        /// 소계, 총계 행 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadTotalView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "TOTAL")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            else if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "SUBTOTAL")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        /// <summary>
        /// 합계 셀 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadTotalView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["LOADSMALLSEGMENTCLASSID"]) == "TOTAL")
            {
                if (e.Column.FieldName.EndsWith("_TOTALQTY"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (e.Column.FieldName.EndsWith("_PRODUCTIONQTY") || e.Column.FieldName.EndsWith("_SAMPLEQTY"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                    if (underlined == null)
                    {
                        underlined = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Underline);
                    }
                    e.Appearance.Font = underlined;
                }
            }
        }

        /// <summary>
        /// 사용자 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadTotalView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "LOADTOPSEGMENTCLASSNAME" || e.Column.FieldName == "LOADMIDDLESEGMENTCLASSNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string top1 = view.GetRowCellValue(e.RowHandle1, "LOADTOPSEGMENTCLASSID").ToString();
                string top2 = view.GetRowCellValue(e.RowHandle2, "LOADTOPSEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && top1 == top2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }

        /// <summary>
        /// 소계, 합계 행 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadDetailView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (Format.GetString(row["PRODUCTDEFID"]) == "TOTAL")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            else if (Format.GetString(row["PRODUCTDEFID"]) == "SUBTOTAL")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }

        /// <summary>
        /// 사용자 Cell Merge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdLoadDetailView_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null)
            {
                return;
            }

            if (e.Column.FieldName == "LOADTOPSEGMENTCLASSNAME" || e.Column.FieldName == "LOADMIDDLESEGMENTCLASSNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string top1 = view.GetRowCellValue(e.RowHandle1, "LOADTOPSEGMENTCLASSID").ToString();
                string top2 = view.GetRowCellValue(e.RowHandle2, "LOADTOPSEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && top1 == top2);
            }
            else if (e.Column.FieldName == "LOADSMALLSEGMENTCLASSNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string mid1 = view.GetRowCellValue(e.RowHandle1, "LOADMIDDLESEGMENTCLASSID").ToString();
                string mid2 = view.GetRowCellValue(e.RowHandle2, "LOADMIDDLESEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && mid1 == mid2);
            }
            else if (e.Column.FieldName == "PROCESSSEGMENTNAME")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);

                string small1 = view.GetRowCellValue(e.RowHandle1, "LOADSMALLSEGMENTCLASSID").ToString();
                string small2 = view.GetRowCellValue(e.RowHandle2, "LOADSMALLSEGMENTCLASSID").ToString();

                e.Merge = (str1 == str2 && small1 == small2);
            }
            else
            {
                e.Merge = false;
            }
            e.Handled = true;
        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            tabControl.SelectedTabPage = pagMain;
            grdLoadDetail.DataSource = null;

            var values = Conditions.GetValues();
			values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);

            if(values["P_QTYTYPE"].ToString() == "M2")
            {
                InitializeTotalGrid("#,##0.00");
            }
            else
            {
                InitializeTotalGrid("#,##0");
            }

            DataTable dtResult = await QueryAsync("SelectLoadPredictionPerRecordTotal", "10001", values);
   
			if (dtResult.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData");
			}

			grdLoadTotal.DataSource = dtResult;
		}

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

			//품목코드
			CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 4.1, true, Conditions, "PRODUCTDEF", "PRODUCTDEF");
            //공정
			CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 4.2, true, Conditions);
            //담당자
            InitializeConditionOwner_Popup();
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 담당자
        /// </summary>
        private void InitializeConditionOwner_Popup()
        {
            var ownerId = Conditions.AddSelectPopup("USERID", new SqlQuery("GetUserList", "10001"), "USERNAME")
                .SetPopupLayout("SELECTOWNER", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("USERNAME")
                .SetLabel("MANAGER")
                .SetPosition(4.3);

            ownerId.Conditions.AddTextBox("USERIDNAME");

            ownerId.GridColumns.AddTextBoxColumn("USERID", 150);
            ownerId.GridColumns.AddTextBoxColumn("USERNAME", 200);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
        }

        #endregion

        #region Private Function
        #endregion
    }
}
