#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 >원자재 수입검사 결과등록
    /// 업  무  설  명  : 원자재 수입검사 결과를 등록한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-06-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RawMaterialImportInspectionResult : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private DataTable _defectTable;//팝업에 넘겨줄 dt 외관검사
        private DataTable _measureTable;//팝업에 넘겨줄 dt 측정검사
        #endregion

        #region 생성자

        public RawMaterialImportInspectionResult()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeRawMaterialGrid();
            InitializeRawAssyGrid();

            // 2020.02.26-유석진-페이지수 추가 및 기본 값 1~10 까지 설정
            spPageCount.Properties.MinValue = 1;
            spPageCount.Properties.MaxValue = 10;
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>

        #region 원자재 수입검사 결과 탭 초기화
        private void InitializeRawMaterialGrid()
        {
            #region 상단 그리드
            // TODO : 그리드 초기화 로직 추가
            grdMaterialInspectionResult.View.SetIsReadOnly();
            grdMaterialInspectionResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMaterialInspectionResult.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMaterialInspectionResult.View.SetSortOrder("ORDERNUMBER");
            grdMaterialInspectionResult.View.SetSortOrder("DEGREE");

            grdMaterialInspectionResult.View.AddTextBoxColumn("ORDERNUMBER", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("STORENO", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("MATERIALLOTID", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("ENTRYEXITDATE", 200);

            grdMaterialInspectionResult.View.AddTextBoxColumn("ACCEPTDATE", 200)
                .SetLabel("RECEPTIONDATE");

            grdMaterialInspectionResult.View.AddTextBoxColumn("INSPECTIONDATE", 200);

            grdMaterialInspectionResult.View.AddTextBoxColumn("DEGREE", 150)
                .SetLabel("SEQ")
                .SetIsHidden();

            grdMaterialInspectionResult.View.AddTextBoxColumn("VENDORNAME", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("CONSUMABLEDEFID", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 150);

            //grdMaterialInspectionResult.View.AddTextBoxColumn("CONSUMABLETYPE", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("QTY", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("UNIT", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("ISINSPECTION", 150);

            grdMaterialInspectionResult.View.AddComboBoxColumn("INSPECTIONRESULT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdMaterialInspectionResult.View.AddTextBoxColumn("HASREPORTFILE", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();

            grdMaterialInspectionResult.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdMaterialInspectionResult.View.AddTextBoxColumn("TXNHISTKEY", 150)
                .SetIsHidden();

            grdMaterialInspectionResult.View.PopulateColumns();
            #endregion

            #region 하단그리드

            #region 외관검사
            grdInspectionItem.GridButtonItem = GridButtonItem.None;
            grdInspectionItem.View.OptionsView.AllowCellMerge = true;
            grdInspectionItem.View.SetIsReadOnly();

            grdInspectionItem.View.SetSortOrder("SORT");

            //grdInspectionItem.View.AddTextBoxColumn("TYPE", 200)
            //    .SetIsReadOnly()
            //    .SetLabel("INSPTYPE");

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMMIDDLECLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMVERSION", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("INSPITEMTYPE", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddSpinEditColumn("MEASUREVALUE", 150)
                .SetIsHidden();

            grdInspectionItem.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdInspectionItem.View.AddTextBoxColumn("INSPECTIONSTANDARD", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItem.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItem.View.PopulateColumns();
            #endregion

            #region 측정검사
            grdInspectionItemSpec.GridButtonItem = GridButtonItem.None;
            grdInspectionItemSpec.View.OptionsView.AllowCellMerge = true;
            grdInspectionItemSpec.View.SetAutoFillColumn("INSPECTIONSTANDARD");
            grdInspectionItemSpec.View.SetIsReadOnly();

            grdInspectionItemSpec.View.SetSortOrder("SORT");

            //grdInspectionItemSpec.View.AddTextBoxColumn("TYPE", 200)
            //    .SetIsReadOnly()
            //    .SetLabel("INSPTYPE");

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("UOMDEFID", 80)
                .SetLabel("UNIT")
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("MEASUREVALUE", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,0.######", MaskTypes.Numeric);

            grdInspectionItemSpec.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONSTANDARD", 200)
                .SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItemSpec.View.PopulateColumns();
            #endregion


            #endregion

        }
        #endregion

        #region 원자재 가공품 수입검사 결과 탭 초기화
        private void InitializeRawAssyGrid()
        {
            #region 상단 그리드
            // TODO : 그리드 초기화 로직 추가
            grdRawAssyInspectionResult.View.SetIsReadOnly();
            grdRawAssyInspectionResult.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdRawAssyInspectionResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdRawAssyInspectionResult.View.SetSortOrder("ORDERNUMBER");
            grdRawAssyInspectionResult.View.SetSortOrder("DEGREE");

            grdRawAssyInspectionResult.View.AddTextBoxColumn("ORDERNUMBER", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("STORENO", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("MATERIALLOTID", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("ENTRYEXITDATE", 200);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("ACCEPTDATE", 200)
                .SetLabel("RECEPTIONDATE");

            grdRawAssyInspectionResult.View.AddTextBoxColumn("INSPECTIONDATE", 200);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("DEGREE", 150)
                .SetLabel("SEQ")
                .SetIsHidden();

            grdRawAssyInspectionResult.View.AddTextBoxColumn("VENDORNAME", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();

            grdRawAssyInspectionResult.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 150)
                .SetIsHidden();


            grdRawAssyInspectionResult.View.AddTextBoxColumn("QTY", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("UNIT", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("ISINSPECTION", 150);

            grdRawAssyInspectionResult.View.AddComboBoxColumn("INSPECTIONRESULT", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdRawAssyInspectionResult.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();

            grdRawAssyInspectionResult.View.AddTextBoxColumn("DESCRIPTION", 150);

            grdRawAssyInspectionResult.View.AddTextBoxColumn("TXNHISTKEY", 150)
                .SetIsHidden();

            grdRawAssyInspectionResult.View.PopulateColumns();
            #endregion

            #region 하단그리드

            #region 외관검사
            grdInspectionItemRawAssy.GridButtonItem = GridButtonItem.None;
            grdInspectionItemRawAssy.View.OptionsView.AllowCellMerge = true;
            grdInspectionItemRawAssy.View.SetIsReadOnly();

            grdInspectionItemRawAssy.View.SetSortOrder("SORT");

            //grdInspectionItemRawAssy.View.AddTextBoxColumn("TYPE", 200)
            //   .SetIsReadOnly()
            //   .SetLabel("INSPTYPE");

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMVERSION", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMTYPE", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddSpinEditColumn("INSPECTIONQTY", 150)
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionItemRawAssy.View.AddSpinEditColumn("SPECOUTQTY", 150)
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionItemRawAssy.View.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly()
               // .SetDisplayFormat("{###.#:P0}", MaskTypes.Numeric);
               .SetDisplayFormat("###.#", MaskTypes.Numeric)
               .SetTextAlignment(TextAlignment.Right);


            grdInspectionItemRawAssy.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.PopulateColumns();
            #endregion

            #region 측정검사
            grdInspectionItemSpecRawAssy.GridButtonItem = GridButtonItem.None;
            grdInspectionItemSpecRawAssy.View.OptionsView.AllowCellMerge = true;
            grdInspectionItemSpecRawAssy.View.SetAutoFillColumn("INSPECTIONSTANDARD");
            grdInspectionItemSpecRawAssy.View.SetIsReadOnly();

            grdInspectionItemSpecRawAssy.View.SetSortOrder("SORT");

            //grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("TYPE", 200)
            //    .SetIsReadOnly()
            //    .SetLabel("INSPTYPE");

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("UOMDEFID", 80)
                .SetLabel("UNIT")
                .SetIsReadOnly();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddSpinEditColumn("MEASUREVALUE", 150)
                .SetTextAlignment(TextAlignment.Right);

            grdInspectionItemSpecRawAssy.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("INSPECTIONSTANDARD", 200)
                .SetIsReadOnly();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("SORT", 300)
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItemSpecRawAssy.View.PopulateColumns();
            #endregion


            #endregion
        }
        #endregion

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            //grdMaterialInspectionResult 더블클릭 이벤트
            grdMaterialInspectionResult.View.DoubleClick += GrdRawMaterial_DoubleClick;
            ///grdRawAssyInspectionResult 더블클릭 이벤트
            grdRawAssyInspectionResult.View.DoubleClick += GrdRawAssy_DoubleClick;

            //의뢰서 출력버튼 클릭 이벤트
            //btnPrint.Click += BtnPrint_Click;

            grdMaterialInspectionResult.View.FocusedRowChanged += (s, e) =>
            {
                SearchDefectList(grdMaterialInspectionResult.View.GetDataRow(e.FocusedRowHandle), grdInspectionItem,grdInspectionItemSpec, "OK_NG", "RawInspection");//**원자재
            };
            grdRawAssyInspectionResult.View.FocusedRowChanged += (s, e) =>
            {
                SearchDefectList(grdRawAssyInspectionResult.View.GetDataRow(e.FocusedRowHandle), grdInspectionItemRawAssy,grdInspectionItemSpecRawAssy, "OK_NG", "ArrivalRawMaterialInspection");
            };

            grdInspectionItem.View.CellMerge += View_CellMerge;
            grdInspectionItemSpec.View.CellMerge += View_CellMerge;
            grdInspectionItemRawAssy.View.CellMerge += View_CellMerge;
            grdInspectionItemSpecRawAssy.View.CellMerge += View_CellMerge;

            tabMaterialInspection.SelectedPageChanged += TabMaterialInspection_SelectedPageChanged;

            //cellStyle 이벤트
            grdMaterialInspectionResult.View.RowCellStyle += View_RowCellStyle;            
            grdRawAssyInspectionResult.View.RowCellStyle += View_RowCellStyle;

            grdInspectionItem.View.RowCellStyle += View_RowCellStyle;
            grdInspectionItemSpec.View.RowCellStyle += View_RowCellStyle;

            grdInspectionItemRawAssy.View.RowCellStyle += View_RowCellStyle;
            grdInspectionItemSpecRawAssy.View.RowCellStyle += View_RowCellStyle;
        }

        /// <summary>
        /// NG 시 Row 빨간색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView grid = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)grid.GetDataRow(e.RowHandle);

            if (Format.GetString(row["INSPECTIONRESULT"]).Equals("NG"))
            {
                if (!grid.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
            }
 
        }
        /// <summary>
        /// 탭이 바뀔 때 하단그리드 재조회 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabMaterialInspection_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SerchNgInfo();
        }

        /// <summary>
        /// 선택된 탭에 따라 의뢰서 출력하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            Btn_PrintClick();
        }

        /// <summary>
        /// grdMaterialInspectionResult그리드 ROW를 더블 클릭 했을 때 검사결과를 입력하는 팝업이 뜨는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdRawAssy_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow row = grdRawAssyInspectionResult.View.GetDataRow(info.RowHandle);

            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            string type = "";
            if (string.IsNullOrWhiteSpace(row["TXNHISTKEY"].ToString()))
            {
                type = "insertData";
            }
            else
            {
                type = "updateData";
            }

            RawAssyImportInspectionResultPopup rawAssypopup = new RawAssyImportInspectionResultPopup(type);
            rawAssypopup.StartPosition = FormStartPosition.CenterParent;
            rawAssypopup.FormBorderStyle = FormBorderStyle.Sizable;
            rawAssypopup.Owner = this;
            rawAssypopup.CurrentDataRow = row;
            rawAssypopup.isEnable = btnPopupFlag.Enabled;
            rawAssypopup.UserPopup();
            //rawAssypopup.OnSearch();
            DialogManager.CloseWaitArea(pnlContent);

            rawAssypopup.ShowDialog();
            if (rawAssypopup.DialogResult == DialogResult.OK)
            {
                Popup_FormClosed();
                SerchNgInfo();
            }
        }


        /// <summary>
        /// grdMaterialInspectionResult그리드 ROW를 더블 클릭 했을 때 검사결과를 입력하는 팝업이 뜨는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdRawMaterial_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            //DataRow row = grdMaterialInspectionResult.View.GetDataRow(info.RowHandle);
            DataRow row = grdMaterialInspectionResult.View.GetFocusedDataRow();

            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);

            string type;

            if (string.IsNullOrWhiteSpace(row["TXNHISTKEY"].ToString()))
            {
                type = "insertData";
            }
            else
            {
                type = "updateData";
            }

            RawMaterialImportInspectionResultPopup rawMaterialpopup = new RawMaterialImportInspectionResultPopup(type);
            rawMaterialpopup.StartPosition = FormStartPosition.CenterParent;
            rawMaterialpopup.FormBorderStyle = FormBorderStyle.Sizable;
            rawMaterialpopup.Owner = this;
            rawMaterialpopup.CurrentDataRow = row;
            rawMaterialpopup.isEnable = btnPopupFlag.Enabled;
            rawMaterialpopup.UserPopup();
            //rawMaterialpopup.OnSearch();
            DialogManager.CloseWaitArea(pnlContent);

            rawMaterialpopup.ShowDialog();
            if (rawMaterialpopup.DialogResult == DialogResult.OK)
            {
                Popup_FormClosed();
                SerchNgInfo(); 
            }
        }


        /// <summary>
        /// CellMerge이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;

            if (view == null) return;

            if (e.Column.FieldName == "INSPECTIONMETHODNAME" || e.Column.FieldName == "INSPECTIONSTANDARD")
            {
                string str1 = view.GetRowCellDisplayText(e.RowHandle1, e.Column);
                string str2 = view.GetRowCellDisplayText(e.RowHandle2, e.Column);
                e.Merge = (str1 == str2);
            }
            else
            {
                e.Merge = false;
            }

            e.Handled = true;
        }
        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdCodeClass.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("ReportsPrint"))
            {
                Btn_PrintClick();
            }
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            DataTable dt = null;
            DataTable dt2 = null;

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("FILERESOURCETYPE", "RawMaterialReport");
            values.Add("RESOURCETYPE", "RawInspection");//**원자재
            //values.Add("P_ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            dt = await SqlExecuter.QueryAsync("SelectRawMaterialInspection", "10001", values);
            grdMaterialInspectionResult.DataSource = dt;

            var param = Conditions.GetValues();
            param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            param.Add("RESOURCETYPE", "ArrivalRawMaterialInspection");
            dt2 = await SqlExecuter.QueryAsync("SelectRawAssyInspection", "10001", param);
            grdRawAssyInspectionResult.DataSource = dt2;

            if (tabMaterialInspection.SelectedTabPageIndex == 0)
            {
                CheckData(dt);
            }
            else
            {
                CheckData(dt2);
            }

            SerchNgInfo();
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            InitializeConditionPopup_Vendor();
            InitializeConditionPopup_Consumable();
        }

        /// <summary>
        /// vendor를 선택하는 팝업
        /// </summary>
        private void InitializeConditionPopup_Vendor()
        {
            // 팝업 컬럼 설정
            var vendorPopup = Conditions.AddSelectPopup("p_vendorid", new SqlQuery("GetVendorList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "VENDORNAME", "VENDORID")
                                        .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(400, 600)
                                        .SetPopupResultCount(1)
                                        .SetPosition(2.1)
                                        .SetLabel("VENDOR")
                                        .SetPopupAutoFillColumns("VENDORNAME");

            // 팝업 조회조건
            vendorPopup.Conditions.AddTextBox("VENDORID")
                       .SetLabel("VENDOR");

            // 팝업 그리드
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);

            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }

        /// <summary>
        /// 자재를 선택하는 팝업
        /// </summary>
        private void InitializeConditionPopup_Consumable()
        {
            var productPopup = Conditions.AddSelectPopup("p_consumabledefid", new SqlQuery("GetConsumableDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLEDEFNAME", "CONSUMABLEDEFID")
                                         .SetPopupLayout("CONSUMABLEDEF", PopupButtonStyles.Ok_Cancel, true, false)
                                         .SetPopupLayoutForm(600, 600)
                                         .SetPopupResultCount(1)
                                         .SetPosition(2.2)
                                         .SetLabel("CONSUMABLEDEF");

            productPopup.Conditions.AddTextBox("CONSUMABLEDEF");

            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 200);
        }
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }


        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            //grdCodeClass.View.CheckValidation();

            //DataTable changed = grdCodeClass.GetChangedRows();

            //if (changed.Rows.Count == 0)
            //{
            //    // 저장할 데이터가 존재하지 않습니다.
            //    throw MessageException.Create("NoSaveData");
            //}
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void CheckData(DataTable table)
        {
            if (table.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }

        /// <summary>
        /// 하단 그리드 불량정보를 조회하는 함수
        /// </summary>
        /// <param name="row"></param>
        /// <param name="grid"></param>
        /// <param name="gridSpec"></param>
        /// <param name="inspItemType"></param>
        private void SearchDefectList(DataRow row, SmartBandedGrid grid, SmartBandedGrid gridSpec, string inspItemType, string resourceType)
        {
            if (row == null)
            {
                grid.DataSource = null;
                gridSpec.DataSource = null;
                return;
            }

            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"P_RESULTTXNGROUPHISTKEY", row["TXNGROUPHISTKEY"]},
                {"P_PROCESSRELNO", row["ORDERNUMBER"]+row["STORENO"].ToString()},
                {"LANGUAGETYPE", Framework.UserInfo.Current.LanguageType},
                {"RESOURCETYPE", resourceType},
                {"P_INSPITEMTYPE", inspItemType},
                {"P_RESOURCEID", row["MATERIALLOTID"]},
                {"P_CONSUMABLEDEFID", row["CONSUMABLEDEFID"]},
                {"P_CONSUMABLEDEFVERSION", row["CONSUMABLEDEFVERSION"]},
                {"P_RELRESOURCEID", row["CONSUMABLEDEFID"] },
                {"P_RELRESOURCEVERSION", row["CONSUMABLEDEFVERSION"] },
                {"P_RELRESOURCETYPE", "Consumable" },
                {"P_INSPECTIONDEFID", "RawInspection"},
                {"P_INSPECTIONCLASSID", "RawInspection"}
            };

            if (tabMaterialInspection.SelectedTabPageIndex == 0)
            {//원자재 검사
             //ItemClassTable
                //DataTable defectItemClassTable = SqlExecuter.Query("SelectRawMaterialItemClass", "10002", values);
                //ItemTable
                DataTable defectItemTable = SqlExecuter.Query("SelectRawInspectionExterior", "10001", values);
                values.Remove("P_INSPITEMTYPE");
                values.Add("P_INSPITEMTYPE", "SPC");

                //측정검사
                //ItemClassTable
                //DataTable measureItemClassTable = SqlExecuter.Query("SelectRawMaterialItemClass", "10001", values);
                //ItemTable
                DataTable measureItemTable = SqlExecuter.Query("SelectRawInspectionMeasure", "10001", values);

                //grdInspectionItem - 합불판정
                //_defectTable = SetDataTableOrder(defectItemClassTable, defectItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩             
                grdInspectionItem.DataSource = defectItemTable;

                //grdInspectionItemSpec - 측정검사
                //_measureTable = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
                grdInspectionItemSpec.DataSource = measureItemTable;
            }
            else
            {//원자재 가공품
             //ItemClassTable - Defect
                //DataTable defectItemClassTable = SqlExecuter.Query("SelectItemClassArrivalRawMaterialEx", "10001", values);
                //ItemTable - Defect
                DataTable defectItemTable = SqlExecuter.Query("SelectRawInspectionExterior", "10001", values);

                values.Remove("P_INSPITEMTYPE");
                values.Add("P_INSPITEMTYPE", "SPC");

                //ItemClassTable - Measure
                //DataTable measureItemClassTable = SqlExecuter.Query("SelectItemClassArrivalRawMaterial", "10001", values);
                //ItemTable - Measure
                DataTable measureItemTable = SqlExecuter.Query("SelectRawInspectionMeasure", "10001", values);

                //grdDefectInspection
                //_defectTable = SetDataTableOrder(defectItemClassTable, defectItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
                grdInspectionItemRawAssy.DataSource = defectItemTable;

                //grdMeasureInspection
                //_measureTable = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
                grdInspectionItemSpecRawAssy.DataSource = measureItemTable;
            }

        }

        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
        }

        /// <summary>
        /// Report 파일의 컨트롤 중 Tag(FieldName) 값이 있는 컨트롤에 DataBinding(Text)를 추가한다.
        /// </summary>
        private void SetReportControlDataBinding(XRControlCollection controls, DataTable dataSource)
        {
            if (controls.Count > 0)
            {
                foreach (XRControl control in controls)
                {
                    if (!string.IsNullOrWhiteSpace(control.Tag.ToString()))
                        control.DataBindings.Add("Text", dataSource, control.Tag.ToString());

                    SetReportControlDataBinding(control.Controls, dataSource);
                }
            }
        }

        /// <summary>
        /// 대분류에 해당하는 소분류 나열 순서를 정해주는 함수
        /// ex) 대분류 10,20,30..
        ///     소분류 10-1,10-2...
        /// </summary>
        private DataTable SetDataTableOrder(DataTable table1, DataTable table2)
        {
            int inspItemclassNo = 10;

            DataTable bindingTable = table2.Clone();
            DataRow addRow;

            foreach (DataRow row1 in table1.Rows)
            {
                int inspItemNo = 1;
                row1["SORT"] = inspItemclassNo;
        
                foreach (DataRow row2 in table2.Rows)
                {
                    if (row1["INSPITEMID"].Equals(row2["INSPITEMCLASSID"]))
                    {
                        row2["SORT"] = inspItemclassNo + "_" + inspItemNo;

                        addRow = bindingTable.NewRow();
                        addRow.ItemArray = row2.ItemArray.Clone() as object[];
                        bindingTable.Rows.Add(addRow);
                        inspItemNo++;
                    }
                }

                addRow = bindingTable.NewRow();
                addRow.ItemArray = row1.ItemArray.Clone() as object[];
                bindingTable.Rows.Add(addRow);
                inspItemclassNo += 10;
            }

            return bindingTable;
        }


        /// <summary>
        /// tabIndex에 따라 하단그리드 조회하는 함수
        /// </summary>
        private void SerchNgInfo()
        {
            if (tabMaterialInspection.SelectedTabPageIndex == 0)
            {
                SearchDefectList(grdMaterialInspectionResult.View.GetFocusedDataRow(), grdInspectionItem, grdInspectionItemSpec, "OK_NG", "RawInspection");//**원자재
            }
            else
            {
                SearchDefectList(grdRawAssyInspectionResult.View.GetFocusedDataRow(), grdInspectionItemRawAssy, grdInspectionItemSpecRawAssy, "OK_NG", "RawInspection");
            }
        }

        /// <summary>
        /// 성적서 출력 이벤트 함수
        /// </summary>
        private void Btn_PrintClick()
        {
            DataTable check;
            DataSet dsReport = new DataSet();
            string title = "";
            DataTable dtailData = new DataTable();

            if (tabMaterialInspection.SelectedTabPageIndex == 0)
            {
                check = grdMaterialInspectionResult.View.GetCheckedRows();
                title = Language.Get("RAWMATERIALIMPORTREPORTTITLE");
            }
            else
            {
                check = grdRawAssyInspectionResult.View.GetCheckedRows();
                title = Language.Get("RAWASSYIMPORTREPORTTITLE");
            }

            if (check.Rows.Count == 0)
            {
                throw MessageException.Create("NotSelectedPintInfo");
            }
            else
            {
                DataTable header = new DataTable();
                header.Columns.Add(new DataColumn("LBLTITLE", typeof(string)));
                DataRow headerRow = header.NewRow();
                headerRow["LBLTITLE"] = title;
                header.Rows.Add(headerRow);

                //check.Columns.Add(new DataColumn("LBLTITLE", typeof(string)));
                check.Columns.Add(new DataColumn("LBLMATERIALINFO", typeof(string)));
                check.Columns.Add(new DataColumn("LBLINSPECTIONINFO", typeof(string)));
                check.Columns.Add(new DataColumn("LBLVENDOR", typeof(string)));
                check.Columns.Add(new DataColumn("LBLORDERNUMBER", typeof(string)));
                check.Columns.Add(new DataColumn("LBLCONSUMABLEDEFNAME", typeof(string)));
                check.Columns.Add(new DataColumn("LBLMATERIALLOTID", typeof(string)));
                check.Columns.Add(new DataColumn("LBLENTRYEXITDATE", typeof(string)));
                check.Columns.Add(new DataColumn("LBLCONSUMABLETYPE", typeof(string)));
                check.Columns.Add(new DataColumn("LBLCONSUMABLEDEFID", typeof(string)));
                check.Columns.Add(new DataColumn("LBLQTY", typeof(string)));
                check.Columns.Add(new DataColumn("LBLACCEPTDATE", typeof(string)));
                check.Columns.Add(new DataColumn("LBLINSPECTIONDATE", typeof(string)));
                check.Columns.Add(new DataColumn("LBLINSPECTIONRESULT", typeof(string)));
                check.Columns.Add(new DataColumn("LBLDESCRIPTION", typeof(string)));
                check.Columns.Add(new DataColumn("LBLREQUESTUSER", typeof(string)));
                check.Columns.Add(new DataColumn("LBLINSPECTIONUSER", typeof(string)));
                check.Columns.Add(new DataColumn("LBLSUBTITLE", typeof(string)));
                check.Columns.Add(new DataColumn("LBLINSPECTIONITEM", typeof(string)));
                check.Columns.Add(new DataColumn("LBLSAMPLEATTACHMENT", typeof(string)));


                foreach (DataRow row in check.Rows)
                {
                    //row["LBLTITLE"] = title; 
                    row["LBLMATERIALINFO"] = Language.Get("MATERIALINFO");
                    row["LBLINSPECTIONINFO"] = Language.Get("INSPECTIONINFO");
                    row["LBLVENDOR"] = Language.Get("VENDOR");
                    row["LBLORDERNUMBER"] = Language.Get("ORDERNUMBER");
                    row["LBLCONSUMABLEDEFNAME"] = Language.Get("MATERIALDEFNAME");
                    row["LBLMATERIALLOTID"] = Language.Get("MATERIALLOTID");
                    row["LBLENTRYEXITDATE"] = Language.Get("ENTRYEXITDATE");
                    row["LBLCONSUMABLETYPE"] = Language.Get("MATERIALTYPE");
                    row["LBLCONSUMABLEDEFID"] = Language.Get("MATERIALDEFID");
                    row["LBLQTY"] = Language.Get("QTY");
                    row["LBLACCEPTDATE"] = Language.Get("RECEPTIONDATE");
                    row["LBLINSPECTIONDATE"] = Language.Get("INSPECTIONDATE");
                    row["LBLINSPECTIONRESULT"] = Language.Get("INSPECTIONRESULT");
                    row["LBLDESCRIPTION"] = Language.Get("DESCRIPTION");
                    row["LBLREQUESTUSER"] = Language.Get("REQUESTUSER");
                    row["LBLINSPECTIONUSER"] = Language.Get("INSPECTIONUSER");
                    row["LBLSUBTITLE"] = Language.Get("MATERIALIMPORTLBLSUBTITLE");
                    row["LBLINSPECTIONITEM"] = Language.Get("INSPECTIONITEM");
                    row["LBLSAMPLEATTACHMENT"] = Language.Get("SAMPLEATTACHMENT");
                }
                
                dtailData.Columns.Add(new DataColumn("LBLINSPECTIONITEM", typeof(string)));
                dtailData.Columns.Add(new DataColumn("LBLSAMPLEATTACHMENT", typeof(string)));

                // 2020.02.26-유석진-페이지 수에 따라 추가 페이지 설정 추가
                for (int i = 0; i < spPageCount.EditValue.ToSafeInt16() - 1; i++)
                {
                    DataRow row = dtailData.NewRow();
                    row["LBLINSPECTIONITEM"] = Language.Get("INSPECTIONITEM"); ;
                    row["LBLSAMPLEATTACHMENT"] = Language.Get("SAMPLEATTACHMENT"); ;

                    dtailData.Rows.Add(row);
                }

                dsReport.Tables.Add(header);
                dsReport.Tables.Add(check);
                dsReport.Tables.Add(dtailData); // 2020.02.26-유석진-페이지 수에 따라 추가 페이지 설정 추가

                Assembly assembly = Assembly.GetAssembly(this.GetType());
                Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.QualityAnalysis.Report.ImportInspection.repx");

                XtraReport report = XtraReport.FromStream(stream);
                report.DataSource = dsReport;
                report.DataMember = dsReport.Tables[1].TableName;

                Band headerPage = report.Bands["PageHeader"];
                SetReportControlDataBinding(headerPage.Controls, dsReport.Tables[0]);

                Band band = report.Bands["Detail"];
                SetReportControlDataBinding(band.Controls, dsReport.Tables[1]);

                // 2020.02.26-유석진-페이지 수에 따라 추가 페이지 설정 추가
                DetailReportBand detailReport = report.Bands["DetailReport"] as DetailReportBand;
                detailReport.DataSource = dsReport;
                detailReport.DataMember = dsReport.Tables[2].TableName;

                Band detailBand = detailReport.Bands["Detail1"];
                if (spPageCount.EditValue.ToSafeInt16() == 1)
                    detailBand.Visible = false;
                SetReportControlDataBinding(detailBand.Controls, dsReport.Tables[2]);

                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();
            }
        }
        #endregion
    }
}
