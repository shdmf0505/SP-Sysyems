#region using

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 >원자재 수입검사 이상발생
    /// 업  무  설  명  : 원자재 수입검사 결과 중 불량이 발생한 건에 대하여 이상발생 결과를 등록한다. 
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-08-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RawMaterialImportInspectionAbnormalOccurrence : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private DataTable _defectTable;//팝업에 넘겨줄 dt 외관검사
        private DataTable _measureTable;//팝업에 넘겨줄 dt 측정검사

        #endregion

        #region 생성자

        public RawMaterialImportInspectionAbnormalOccurrence()
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
            InitializeGrdRawMaterialAbnormal();
            InitializeGrdRawAssyAbnormal();

        }

        #region 원자재 이상발생 탭 초기화
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrdRawMaterialAbnormal()
        {
            #region 원자재 수입검사 이상발생 현황 그리드 초기화

            grdRawMaterialAbnormal.View.SetIsReadOnly();
            grdRawMaterialAbnormal.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdRawMaterialAbnormal.View.SetSortOrder("NCRISSUEDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdRawMaterialAbnormal.View.SetSortOrder("ORDERNUMBER");
            grdRawMaterialAbnormal.View.SetSortOrder("DEGREE");

            //진행현황
            grdRawMaterialAbnormal.View.AddTextBoxColumn("STATENAME", 150)
                .SetLabel("PROCESSSTATUS") ;

            grdRawMaterialAbnormal.View.AddTextBoxColumn("ORDERNUMBER", 150);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("STORENO", 150);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("INSPECTIONDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdRawMaterialAbnormal.View.AddTextBoxColumn("DEGREE", 150)
                .SetLabel("SEQ")
                .SetIsHidden();

            grdRawMaterialAbnormal.View.AddTextBoxColumn("VENDORNAME", 150);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("QTY", 150);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("UNIT", 150);

            //car 관련 날짜
            grdRawMaterialAbnormal.View.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdRawMaterialAbnormal.View.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);

            //마감여부
            grdRawMaterialAbnormal.View.AddTextBoxColumn("ISCLOSE", 150);

            grdRawMaterialAbnormal.View.PopulateColumns();

            #endregion

            #region 원자재 수입검사 불량 정보 그리드 초기화  

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

        #region 원자재 가공품 이상발생 탭 초기화
        private void InitializeGrdRawAssyAbnormal()
        {
            #region 원자재 수입검사 이상발생 현황 그리드 초기화

            grdRawAssyAbnormal.View.SetIsReadOnly();
            grdRawAssyAbnormal.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdRawAssyAbnormal.View.SetSortOrder("NCRISSUEDATE", DevExpress.Data.ColumnSortOrder.Descending);
            grdRawAssyAbnormal.View.SetSortOrder("ORDERNUMBER");
            grdRawAssyAbnormal.View.SetSortOrder("DEGREE");

            //진행현황
            grdRawAssyAbnormal.View.AddTextBoxColumn("STATENAME", 150)
                .SetLabel("PROCESSSTATUS");

            grdRawAssyAbnormal.View.AddTextBoxColumn("ORDERNUMBER", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("STORENO", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("INSPECTIONDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdRawAssyAbnormal.View.AddTextBoxColumn("DEGREE", 150)
                .SetLabel("SEQ")
                .SetIsHidden();

            grdRawAssyAbnormal.View.AddTextBoxColumn("VENDORNAME", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("QTY", 150);

            grdRawAssyAbnormal.View.AddTextBoxColumn("UNIT", 150);

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

            //마감여부
            grdRawAssyAbnormal.View.AddTextBoxColumn("ISCLOSE", 150);

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

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPECTIONMETHODNAME", 200)
                .SetIsReadOnly();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMCLASSID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMNAME", 250)
                .SetIsReadOnly();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMVERSION", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddTextBoxColumn("INSPITEMTYPE", 300)
                .SetIsHidden();

            grdInspectionItemRawAssy.View.AddSpinEditColumn("INSPECTIONQTY", 150);

            grdInspectionItemRawAssy.View.AddSpinEditColumn("SPECOUTQTY", 150);

            grdInspectionItemRawAssy.View.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly()
               // .SetDisplayFormat("{###.#:P0}", MaskTypes.Numeric);
               .SetDisplayFormat("###.#", MaskTypes.Numeric);


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

            grdInspectionItemSpecRawAssy.View.AddSpinEditColumn("MEASUREVALUE", 150);

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
            //그리드 더블클릭 이벤트
            grdRawMaterialAbnormal.View.DoubleClick += View_DoubleClick;
            grdRawAssyAbnormal.View.DoubleClick += View_DoubleClick;

            grdRawMaterialAbnormal.View.FocusedRowChanged += (s, e) =>
            {
                SearchDefectList(grdRawMaterialAbnormal.View.GetDataRow(e.FocusedRowHandle), grdInspectionItem, grdInspectionItemSpec, "OK_NG", "RawInspection");//**원자재
            };
            grdRawAssyAbnormal.View.FocusedRowChanged += (s, e) =>
            {
                SearchDefectList(grdRawAssyAbnormal.View.GetDataRow(e.FocusedRowHandle), grdInspectionItemRawAssy, grdInspectionItemSpecRawAssy, "QTY", "ArrivalRawMaterialInspection");
            };

            //tabAbnormal 탭 체인지 이벤트
            tabAbnormal.SelectedPageChanged += TabAbnormal_SelectedPageChanged;

            //그리드 Merge이벤트
            grdInspectionItem.View.CellMerge += View_CellMerge;
            grdInspectionItemSpec.View.CellMerge += View_CellMerge;
            grdInspectionItemRawAssy.View.CellMerge += View_CellMerge;
            grdInspectionItemSpecRawAssy.View.CellMerge += View_CellMerge;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabAbnormal_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SearchNgInfo();
        }

        /// <summary>
        /// 그리드 Row를 더블클릭 했을때 팝업을 뛰우는 이벤트
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

            RawMaterialImportInspectionAbnormalOccurrencePopup popup = new RawMaterialImportInspectionAbnormalOccurrencePopup(row);
            popup.CurrentDataRow = row;
            popup.isEnable = btnPopupFlag.Enabled;
            popup._tabIndex = tabAbnormal.SelectedTabPageIndex;
            popup._defectTable = _defectTable;
            popup._meassureTable = _measureTable;
            popup.FormBorderStyle = FormBorderStyle.Sizable;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Owner = this;
            popup.ShowDialog();

            DialogManager.CloseWaitArea(pnlContent);

            if (popup.DialogResult == DialogResult.OK)
            {
                Popup_FormClosed();
            }
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

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_CONSUMABLECLASSID", "RawMaterial");
            values.Add("RESOURCETYPE", "RawInspection");//**원자재

            DataTable dt = await SqlExecuter.QueryAsync("SelectRawMaterialAbnormal", "10001", values);
            grdRawMaterialAbnormal.DataSource = dt;

            var param = Conditions.GetValues();
            param.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            param.Add("RESOURCETYPE", "ArrivalRawMaterialInspection");

            DataTable dt2 = await SqlExecuter.QueryAsync("SelectArrivalMaterialAbnormal", "10001", param);
            grdRawAssyAbnormal.DataSource = dt2;

            if (tabAbnormal.SelectedTabPageIndex == 0)
            {
                CheckData(dt);
            }
            else
            {
                CheckData(dt2);
            }

            SearchNgInfo();
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
                                        .SetPosition(1.2)
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
                                         .SetPopupLayoutForm(400, 600)
                                         .SetPopupResultCount(1)
                                         .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                                         .SetPosition(1.5)
                                         .SetLabel("CONSUMABLEDEF");

            productPopup.Conditions.AddTextBox("CONSUMABLEDEF");

            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);

            productPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
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

        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
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
                {"P_PROCESSRELNO", row["ORDERNUMBER"]+row["STORENO"].ToString()},
                {"LANGUAGETYPE", Framework.UserInfo.Current.LanguageType},
                {"RESOURCETYPE", resourceType},
                {"P_INSPITEMTYPE", inspItemType},
                {"P_RESOURCEID", row["RESOURCEID"]},
                {"P_CONSUMABLEDEFID", row["CONSUMABLEDEFID"]},
                {"P_CONSUMABLEDEFVERSION", row["CONSUMABLEDEFVERSION"]},
                {"P_RESULT", "NG"},
                {"P_RELRESOURCEID", row["CONSUMABLEDEFID"] },
                {"P_RELRESOURCEVERSION", row["CONSUMABLEDEFVERSION"] },
                {"P_RELRESOURCETYPE", "Consumable" },
                {"P_INSPECTIONDEFID", "RawInspection"},
                {"P_INSPECTIONCLASSID", "RawInspection" }
            };

            if (tabAbnormal.SelectedTabPageIndex == 0)
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
                _defectTable = defectItemTable;
                grdInspectionItem.DataSource = defectItemTable;

                //grdInspectionItemSpec - 측정검사
                //_measureTable = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
                _measureTable = measureItemTable;
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
                _defectTable = defectItemTable;
                grdInspectionItemRawAssy.DataSource = defectItemTable;

                //grdMeasureInspection
                //_measureTable = SetDataTableOrder(measureItemClassTable, measureItemTable);//대분류, 소분류를 정렬시켜 그리드 바인딩
                _measureTable = measureItemTable;
                grdInspectionItemSpecRawAssy.DataSource = measureItemTable;
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
        private void SearchNgInfo()
        {
            if (tabAbnormal.SelectedTabPageIndex == 0)
            {
                SearchDefectList(grdRawMaterialAbnormal.View.GetFocusedDataRow(), grdInspectionItem, grdInspectionItemSpec, "OK_NG", "RawInspection");//**원자재
            }
            else
            {
                SearchDefectList(grdRawAssyAbnormal.View.GetFocusedDataRow(), grdInspectionItemRawAssy, grdInspectionItemSpecRawAssy, "QTY", "ArrivalRawMaterialInspection");
            }
        }
        #endregion
    }
}
