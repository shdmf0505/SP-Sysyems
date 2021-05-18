#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 >원자재 가공품 수입검사 이상발생
    /// 업  무  설  명  : 원자재 가공품 수입검사 결과 중 불량이 발생한 건에 대하여 이상발생 결과를 등록한다. 
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SubassemblyInspAbnormalOccurrence : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private DataTable _defectTable;//팝업에 넘겨줄 dt 외관검사
        private DataTable _measureTable;//팝업에 넘겨줄 dt 측정검사


        #endregion

        #region 생성자

        public SubassemblyInspAbnormalOccurrence()
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
            #region 원자재 수입검사 이상발생 현황 그리드 초기화

            grdRawAssyAbnormal.View.SetIsReadOnly();
            grdRawAssyAbnormal.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdRawAssyAbnormal.View.SetSortOrder("NCRISSUEDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdRawAssyAbnormal.View.SetSortOrder("ORDERNUMBER");
            grdRawAssyAbnormal.View.SetSortOrder("DEGREE");

            //진행현황
            grdRawAssyAbnormal.View.AddTextBoxColumn("STATENAME", 150)
                .SetLabel("PROCESSSTATUS");

            grdRawAssyAbnormal.View.AddTextBoxColumn("INSPECTIONDATE", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("PRODUCTDEFID", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("PRODUCTDEFNAME", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("PRODUCTDEFVERSION", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("CONSUMABLETYPE", 100);

            grdRawAssyAbnormal.View.AddTextBoxColumn("VENDORNAME", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetLabel("PROCESSSEGMENT");

            grdRawAssyAbnormal.View.AddTextBoxColumn("LOTID", 200);

            grdRawAssyAbnormal.View.AddTextBoxColumn("CREATEDQTY", 150)
                .SetLabel("QTY");

            grdRawAssyAbnormal.View.AddTextBoxColumn("UNIT", 150);

            //마감여부
            grdRawAssyAbnormal.View.AddTextBoxColumn("ISCLOSE", 150);

            //원인
            grdRawAssyAbnormal.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150)
                .SetLabel("REASONPRODUCTDEFNAME");

            grdRawAssyAbnormal.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 150);
            
            grdRawAssyAbnormal.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 150)
                .SetLabel("CAUSEMATERIALLOT");

            grdRawAssyAbnormal.View.AddTextBoxColumn("REASONSEGMENTNAME", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("REASONAREANAME", 150);

            //car 관련 날짜
            grdRawAssyAbnormal.View.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdRawAssyAbnormal.View.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdRawAssyAbnormal.View.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdRawAssyAbnormal.View.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdRawAssyAbnormal.View.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdRawAssyAbnormal.View.PopulateColumns();

            #endregion

            #region 원자재 수입검사 불량 정보 그리드 초기화

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

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

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

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdRawAssyAbnormal.View.DoubleClick += View_DoubleClick;

            grdRawAssyAbnormal.View.FocusedRowChanged += (s, e) =>
             { SearchDefectList(grdRawAssyAbnormal.View.GetDataRow(e.FocusedRowHandle), grdInspectionItemRawAssy, grdInspectionItemSpecRawAssy, "OK_NG", "SubassemblyInspection"); };

            grdInspectionItemRawAssy.View.CellMerge += View_CellMerge;
            grdInspectionItemSpecRawAssy.View.CellMerge += View_CellMerge;

            //2020-01-13 affectLot isLocking 에 따른 추가 
            grdRawAssyAbnormal.View.RowCellStyle += View_RowCellStyle;
        }
        /// <summary>
        /// 이상발생 그리드 더블클릭시 팝업 띄우는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            SmartBandedGridView view = sender as SmartBandedGridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            DataRow row = view.GetDataRow(info.RowHandle);

            if (row == null) return;

            DialogManager.ShowWaitArea(pnlContent);
            SubassemblyInspAbnormalOccurrencePopup popup = new SubassemblyInspAbnormalOccurrencePopup(row);
            popup.CurrentDataRow = row;
            popup.isEnable = btnPopupFlag.Enabled;
            popup._defectTable = _defectTable;
            popup._measureTable = _measureTable;
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);

            if (popup.DialogResult == DialogResult.OK)
            {
                SearchGrd();
            }
        }

        /// <summary>
        /// affectLot isLocking Y 시 Row 빨간색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView grid = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)grid.GetDataRow(e.RowHandle);

            if (Format.GetString(row["AFFECTISLOCKING"]).Equals("Y"))
            {
                if (!grid.IsCellSelected(e.RowHandle, e.Column))
                    e.Appearance.ForeColor = Color.Red;
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
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdList.GetChangedRows();

           // ExecuteRule("SaveCodeClass", changed);
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
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectSubassemblyAbnormal","10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdRawAssyAbnormal.DataSource = dt;
            SearchDefectList(grdRawAssyAbnormal.View.GetFocusedDataRow(), grdInspectionItemRawAssy, grdInspectionItemSpecRawAssy, "OK_NG", "SubassemblyInspection");
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionPopup_Vendor();
            InitializeConditionProcessSegmentId_Popup();
            InitializeConditionPopup_ProductDefId();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region 조회조건 팝업
        private void InitializeConditionPopup_Vendor()
        {
            // 팝업 컬럼 설정
            var vendorPopup = Conditions.AddSelectPopup("P_VENDORID", new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", "VENDORTYPE=Supplier"), "VENDORNAME", "VENDORID")
                                        .SetPopupLayout("VENDOR", PopupButtonStyles.Ok_Cancel, true, false)
                                        .SetPopupLayoutForm(400, 600)
                                        .SetPopupResultCount(1)
                                        .SetPosition(3.1)
                                        .SetLabel("VENDOR")
                                        .SetRelationIds("P_PLANTID")
                                        .SetPopupAutoFillColumns("VENDORNAME");

            // 팝업 조회조건
            vendorPopup.Conditions.AddTextBox("VENDORID")
                       .SetLabel("VENDOR");

            // 팝업 그리드
            vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);

            vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
        }

        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetPosition(3.2);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }
        private void InitializeConditionPopup_ProductDefId()
        {
            var productPopup = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
                                            .SetPopupLayout("PRODUCTDEF", PopupButtonStyles.Ok_Cancel, true, false)
                                            .SetPopupLayoutForm(600, 600)
                                            .SetPopupResultCount(1)
                                            .SetPosition(4.1)
                                            .SetLabel("PRODUCTDEF");

            productPopup.Conditions.AddTextBox("PRODUCTDEF");

            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 200);
        }

        #endregion

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdRawAssyAbnormal.View.CheckValidation();

            DataTable changed = grdRawAssyAbnormal.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private async void SearchGrd()
        {
            await OnSearchAsync();
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
                {"TXNGROUPHISTKEY", row["TXNGROUPHISTKEY"]},
                {"P_PROCESSRELNO", row["LOTID"]},
                {"LANGUAGETYPE", Framework.UserInfo.Current.LanguageType},
                {"RESOURCETYPE", resourceType},
                {"P_INSPITEMTYPE", inspItemType},
                {"P_RESOURCEID",  row["LOTID"]},
                {"P_RESULT", "NG"},
                {"P_RELRESOURCEID", row["PRODUCTDEFID"] },
                {"P_RELRESOURCEVERSION", row["PRODUCTDEFVERSION"] },
                {"P_RELRESOURCETYPE", "Consumable" },
                {"P_INSPECTIONDEFID", "SubassemblyInspection"},
                {"P_INSPECTIONCLASSID", "SubassemblyInspection"}
            };

            //원자재 가공품
            //ItemClassTable - Defect
            //DataTable defectItemClassTable = SqlExecuter.Query("SelectItemClassToInspection", "10002", values);
            //ItemTable - Defect
            DataTable defectItemTable = SqlExecuter.Query("SelectSubassemblyInspectionExterior", "10001", values);

            values.Remove("P_INSPITEMTYPE");
            values.Add("P_INSPITEMTYPE", "SPC");

            //ItemClassTable - Measure
            //DataTable measureItemClassTable = SqlExecuter.Query("SelectItemClassToInspection", "10001", values);
            //ItemTable - Measure
            DataTable measureItemTable = SqlExecuter.Query("SelectSubassemblyInspectionMeasure", "10001", values);

            //grdDefectInspection
            //_defectTable = SetDataTableOrder(defectItemClassTable, defectItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            grdInspectionItemRawAssy.DataSource = defectItemTable;
            _defectTable = defectItemTable;

            //grdMeasureInspection
            //_measureTable = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
            grdInspectionItemSpecRawAssy.DataSource = measureItemTable;
            _measureTable = measureItemTable;

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

        #endregion
    }
}
