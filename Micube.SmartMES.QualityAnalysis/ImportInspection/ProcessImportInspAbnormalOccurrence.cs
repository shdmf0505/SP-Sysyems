#region using

using DevExpress.Utils.Menu;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 수입 검사 관리 > 공정 수입검사 이상발생 
    /// 업  무  설  명  : 
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-10-15
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessImportInspAbnormalOccurrence : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private DataTable _selectMeasureValueDt;
        private bool _autoChange = false;
        private List<string> areaList = new List<string>();
        DXMenuItem _myContextMenu1;
        #endregion

        #region 생성자

        public ProcessImportInspAbnormalOccurrence()
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
            InitializeCombo();
            InitializeGrid();
        }

        private void InitializeCombo()
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"LANGUAGETYPE", UserInfo.Current.LanguageType},
                { "CODECLASSID","InspectionDecisionClass"}
            };

            DataTable dt = SqlExecuter.Query("GetTypeList", "10001", values);

            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.ShowHeader = false;
            cboStandardType.Editor.DisplayMember = "CODENAME";
            cboStandardType.Editor.ValueMember = "CODEID";
            cboStandardType.Editor.UseEmptyItem = false;
            cboStandardType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboStandardType.Editor.DataSource = dt;
        }

        /// <summary>        
        /// 검사결과 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            #region 공정 수입검사 이상발생 현황 그리드 초기화

            grdAbnormal.View.SetIsReadOnly();
            grdAbnormal.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore | GridButtonItem.Export;
            grdAbnormal.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdAbnormal.View.CheckMarkSelection.MultiSelectCount = 1;
            grdAbnormal.View.SetSortOrder("NCRISSUEDATE", DevExpress.Data.ColumnSortOrder.Descending);

            //진행현황
            grdAbnormal.View.AddTextBoxColumn("COMPANYAGREE", 150);

            grdAbnormal.View.AddTextBoxColumn("COMPANYAGREEDATE", 150);

            grdAbnormal.View.AddTextBoxColumn("STATENAME", 150)
                .SetLabel("PROCESSSTATUS");

            grdAbnormal.View.AddTextBoxColumn("INSPECTIONDATE", 150);

            grdAbnormal.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetLabel("PROCESSSEGMENTCLASS");

            grdAbnormal.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

            grdAbnormal.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200);

            grdAbnormal.View.AddTextBoxColumn("AREANAME", 200);

            grdAbnormal.View.AddTextBoxColumn("PRODUCTDEFID", 150);

            grdAbnormal.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            grdAbnormal.View.AddTextBoxColumn("PRODUCTDEFVERSION", 200);

            grdAbnormal.View.AddTextBoxColumn("TRANSITAREANAME", 200)
                .SetLabel("TRANSITAREA");

            grdAbnormal.View.AddTextBoxColumn("LOTID", 200);

            grdAbnormal.View.AddTextBoxColumn("DEGREE", 80);

            grdAbnormal.View.AddTextBoxColumn("ISSENDNAME", 150)
                .SetLabel("ISTAKEOVER");

            grdAbnormal.View.AddTextBoxColumn("INSPECTIONRESULT", 100);

            //원인
            grdAbnormal.View.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150)
                .SetLabel("REASONPRODUCTDEFNAME");

            grdAbnormal.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 150);

            grdAbnormal.View.AddTextBoxColumn("REASONCONSUMABLELOTID", 150)
                .SetLabel("CAUSEMATERIALLOT");

            grdAbnormal.View.AddTextBoxColumn("REASONSEGMENTNAME", 150);

            grdAbnormal.View.AddTextBoxColumn("REASONAREANAME", 150);

            //car 관련 날짜
            grdAbnormal.View.AddTextBoxColumn("CARREQUESTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CARREQUESTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdAbnormal.View.AddTextBoxColumn("CAREXPECTEDRECEIPTDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("CAREXPECTEDRECEIPTDATE")
                .SetTextAlignment(TextAlignment.Center);

            grdAbnormal.View.AddTextBoxColumn("RECEIPTDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARRECEIPTDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdAbnormal.View.AddTextBoxColumn("APPROVALDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARAPPROVALDATE")
                 .SetTextAlignment(TextAlignment.Center);

            grdAbnormal.View.AddTextBoxColumn("CLOSEDATE", 150)
                 .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                 .SetLabel("CARCLOSEDATE")
                 .SetTextAlignment(TextAlignment.Center);

            //마감여부
            grdAbnormal.View.AddTextBoxColumn("ISCLOSE", 150);

            grdAbnormal.View.PopulateColumns();

            #endregion

            #region 외관검사 탭 초기화
            grdInspectionItem.GridButtonItem = GridButtonItem.None;
            grdInspectionItem.View.SetIsReadOnly();
            
            //구분 등 검사항목
            var item = grdInspectionItem.View.AddGroupColumn("");

            item.AddTextBoxColumn("DEFECTCODE", 250)
                .SetIsHidden();

            item.AddTextBoxColumn("DEFECTCODENAME", 250)
                .SetIsReadOnly();

            item.AddTextBoxColumn("QCSEGMENTNAME", 250)
                .SetIsReadOnly();

            //검사 수량
            var groupInspQty = grdInspectionItem.View.AddGroupColumn("INSPECTIONQTY");
            //PCS
                groupInspQty.AddSpinEditColumn("INSPECTIONQTY", 150)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetLabel("PCS");

                //PNL
                groupInspQty.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetLabel("PNL");


            //불량수량
            var groupSpecOutQty = grdInspectionItem.View.AddGroupColumn("DEFECTQTY");
            //PCS
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTY", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCS");
            //PNL
            groupSpecOutQty.AddSpinEditColumn("DEFECTQTYPNL", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PNL");

            //결과
            var result = grdInspectionItem.View.AddGroupColumn("");

            result.AddTextBoxColumn("DEFECTRATE", 150)
                .SetIsReadOnly()
               .SetDisplayFormat("###.#", MaskTypes.Numeric);

            result.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            result.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            result.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdInspectionItem.View.PopulateColumns();
            #endregion

            #region 측정검사 탭 초기화

            #region 검사 항목 그리드 초기화

            grdInspectionItemSpec.GridButtonItem = GridButtonItem.None;
            grdInspectionItemSpec.View.SetIsReadOnly();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMNAME", 250);

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPECTIONSTANDARD", 250);

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("INSPITEMTYPE", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("USL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("LSL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("LCL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.AddTextBoxColumn("UCL", 200)
                .SetIsHidden();

            grdInspectionItemSpec.View.PopulateColumns();

            #endregion

            #region 측정값 그리드 초기화

            grdMeasuredValue.GridButtonItem = GridButtonItem.None;
            grdMeasuredValue.View.SetIsReadOnly();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMID", 200)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("INSPITEMVERSION", 200)
                .SetIsHidden();

            grdMeasuredValue.View.AddSpinEditColumn("MEASUREVALUE", 150)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,0.######", MaskTypes.Numeric);

            grdMeasuredValue.View.AddComboBoxColumn("INSPECTIONRESULT", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OKNG", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();

            grdMeasuredValue.View.AddTextBoxColumn("TXNHISTKEY", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("RESOURCEID", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("RESOURCETYPE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("PROCESSRELNO", 300)
                .SetIsHidden();

            grdMeasuredValue.View.AddTextBoxColumn("DEGREE", 300)
                .SetIsHidden();

            grdMeasuredValue.View.PopulateColumns();

            #endregion

            #endregion

            #region 불량코드 탭 초기화

            grdDefect.GridButtonItem = GridButtonItem.None;
            grdDefect.View.SetIsReadOnly();

            //구분 등 검사항목
            var standardItem = grdDefect.View.AddGroupColumn("");

            //LOT ID
            standardItem.AddTextBoxColumn("LOTID", 150)
                .SetIsHidden();

            //불량코드
            standardItem.AddTextBoxColumn("DEFECTCODE", 150)
                .SetIsHidden();

            //불량코드명
            standardItem.AddTextBoxColumn("DEFECTCODENAME", 150);

            //품질공정
            standardItem.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden();

            standardItem.AddTextBoxColumn("QCSEGMENTNAME", 250);

            //검사 수량
            var groupInspectionQty = grdDefect.View.AddGroupColumn("INSPECTIONQTY");
            //PCS
            groupInspectionQty.AddSpinEditColumn("INSPECTIONQTY", 150)
                .SetLabel("PCS")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);
            //PNL
            groupInspectionQty.AddSpinEditColumn("INSPECTIONQTYPNL", 150)
                .SetLabel("PNL")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);

            //불량수량
            var groupDefectQty = grdDefect.View.AddGroupColumn("SPECOUTQTY");
            //PCS
            groupDefectQty.AddSpinEditColumn("DEFECTQTY", 150)
                .SetLabel("PCS")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);

            //PNL
            groupDefectQty.AddSpinEditColumn("DEFECTQTYPNL", 150)
                .SetLabel("PNL")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetTextAlignment(TextAlignment.Right);

            //결과
            var inspResult = grdDefect.View.AddGroupColumn("");

            //불량률
            inspResult.AddSpinEditColumn("DEFECTRATE", 80)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                .SetTextAlignment(TextAlignment.Right);


            //결과
            var reason = grdDefect.View.AddGroupColumn("");

            reason.AddTextBoxColumn("REASONCONSUMABLEDEFNAME", 150)
                .SetLabel("REASONPRODUCTDEFNAME");

            reason.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 150);

            //원인품목 -> 원인자재?
            reason.AddTextBoxColumn("REASONCONSUMABLELOTID", 150)
                .SetLabel("CAUSEMATERIALLOT");

            //원인공정
            reason.AddTextBoxColumn("REASONSEGMENTID", 150)
                .SetIsHidden();

            reason.AddTextBoxColumn("REASONSEGMENTNAME", 150);

            reason.AddTextBoxColumn("REASONAREANAME", 150);

            grdDefect.View.PopulateColumns();

            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            Load += ProcessImportInspAbnormalOccurrence_Load;
            //grdAbnormal 그리드 포커스Row Change이벤트
            grdAbnormal.View.FocusedRowChanged += (s, e) =>
            {
                DataRow focusedRow = grdAbnormal.View.GetDataRow(e.FocusedRowHandle);
                SearchResult(focusedRow);
            };

            //그리드 더블 클릭 이벤트
            grdAbnormal.View.DoubleClick += View_DoubleClick;

            //업체동의 버튼 클릭 이벤트
            //btnAgree.Click += BtnAgree_Click;

            //업체동의를 한 경우 체크 불가 이벤트
            grdAbnormal.View.CheckStateChanged += View_CheckStateChanged;
            grdAbnormal.InitContextMenuEvent += grdAbnormal_InitContextMenuEvent;

            //2020-01-13 affectLot isLocking 에 따른 추가 
            grdAbnormal.View.RowCellStyle += View_RowCellStyle;

        }

        /// <summary>
        /// load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessImportInspAbnormalOccurrence_Load(object sender, EventArgs e)
        {/*
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("USERID",UserInfo.Current.Id);
            values.Add("ENTERPRISEID",UserInfo.Current.Enterprise);
            values.Add("P_PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE",UserInfo.Current.LanguageType);

            DataTable areaDt = SqlExecuter.Query("GetAuthorityUserUseArea", "10001", values);

            areaList = areaDt.AsEnumerable()
                .Select(r => r["AREAID"].ToString()).ToList();*/
        }
        private void grdAbnormal_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            //Affect LOT 산정(출) 화면전환 메뉴ID : PG-QC-0556, 프로그램ID : Micube.SmartMES.QualityAnalysis.AffectLot
            args.AddMenus.Add(_myContextMenu1 = new DXMenuItem(Language.Get("MENU_PG-QC-0556"), OpenForm) { BeginGroup = true, Tag = "PG-QC-0556" });
        }
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                DataRow currentRow = grdAbnormal.View.GetFocusedDataRow();
                if (currentRow == null) return;

                string menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }
        /// <summary>
        /// 업체동의 된 row를 체크할 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            //if (_autoChange == true)
            //{
            //    _autoChange = false;
            //    return;
            //}

            grdAbnormal.View.CheckStateChanged -= View_CheckStateChanged;

            DataRow row = grdAbnormal.View.GetFocusedDataRow();

            if (row["ISMODIFY"].ToString().Equals("Y"))
            {
                if (row["COMPANYAGREE"].ToString().Equals("Y"))
                {
                    ShowMessage("AlreadyCompanyAgree");//이미 업체동의를 한건 입니다.
                    //_autoChange = true;
                    grdAbnormal.View.CheckRow(grdAbnormal.View.FocusedRowHandle, false);
                }
            }
            else
            {
                ShowMessage("NotMatchingUserWithArea", $"AreaId={row["AREAID"].ToString()}", $"AreaName={row["AREANAME"].ToString()}");//사용자가 속하지 않은 작업장은 업체동의 할 수 없습니다.
                //_autoChange = true;
                grdAbnormal.View.CheckRow(grdAbnormal.View.FocusedRowHandle, false);
            }
            grdAbnormal.View.CheckStateChanged += View_CheckStateChanged;
        }

        /// <summary>
        /// 업체동의를 저장 하는 이벤트
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgree_Click(object sender, EventArgs e)
        {
            Btn_CompanyAgreeClick();
        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = grdAbnormal.View.GetFocusedDataRow();
               
            if (row == null) return;

            //버튼의 enable
            bool isEnable = btnPopupFlag.Enabled;

            if (!Format.GetString(row["ISMODIFY"]).Equals("Y"))
            {
                isEnable = false;
            }
            

            DialogManager.ShowWaitArea(pnlContent);

            ProcessImportInspAbnormalOccurrencePopup popup = new ProcessImportInspAbnormalOccurrencePopup(row);
            popup.CurrentDataRow = row;
            int rowCount = popup.SelectData();

            DialogManager.CloseWaitArea(pnlContent);
            if (rowCount > 0)
            { 
                popup.FormBorderStyle = FormBorderStyle.Sizable;
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.isEnable = isEnable;
                popup.Owner = this;
                popup.ShowDialog();

                DialogManager.CloseWaitArea(pnlContent);
                Popup_FormClosed();
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


        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            //DataTable changed = grdAbnormal.GetChangedRows();

            //ExecuteRule("SaveCodeClass", changed);
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("CompanyAgree"))
            {
                Btn_CompanyAgreeClick();
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
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            //values.Add("PLANTID", Framework.UserInfo.Current.Plant);

            DataTable dt = await SqlExecuter.QueryAsync("SelectProcessInspAbnormal", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdAbnormal.DataSource = dt;
            DataRow focusedRow = grdAbnormal.View.GetFocusedDataRow();

            SearchResult(focusedRow);
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionPopup_ProcessSegmentClassPopup();
            this.Conditions.AddComboBox("P_PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetEmptyItem()
                .SetLabel("LARGEPROCESSSEGMENT")
                .SetPosition(2.1);

            InitializeConditionProcessSegmentId_Popup();
            // 작업장
            //CommonFunction.AddConditionAreaPopup("P_AREAID", 2.3, false, Conditions);
            InitializeConditionAreaId_Popup();
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2.4, true, Conditions);
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

        /// <summary>
        /// 팝업형 조회조건 생성 - 작업장
        /// </summary>
        private void InitializeConditionAreaId_Popup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            //param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("AREA", UserInfo.Current.Area);
            param.Add("P_USERID", UserInfo.Current.Id);

            var areaIdPopup = this.Conditions.AddSelectPopup("p_areaId", new SqlQuery("GetAuthorityUserUseArea", "10001", param), "AREANAME", "AREAID")
                .SetPopupLayout(Language.Get("SELECTAREAID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("AREANAME")
                .SetLabel("AREA")
                .SetRelationIds("P_PLANTID")
                .SetPosition(2.3);

            areaIdPopup.Conditions.AddTextBox("AREAIDNAME");

            areaIdPopup.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaIdPopup.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        private void InitializeConditionProcessSegmentId_Popup()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentByTclass", "10001", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetLabel("PROCESSSEGMENT")
                .SetRelationIds("P_PROCESSSEGMENTCLASSID")
                .SetPosition(2.2);

            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");

            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200);
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
            grdAbnormal.View.CheckValidation();

            DataTable changed = grdAbnormal.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void SearchResult(DataRow row)
        {
            if (row == null) return;

            cboStandardType.Editor.EditValue = row["JUDGMENTCRITERIA"];

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", Framework.UserInfo.Current.Enterprise);
            values.Add("PLANTID", Framework.UserInfo.Current.Plant);
            values.Add("P_RESOURCEID", row["LOTID"]);
            values.Add("RESOURCETYPE", "ProcessInspection");
            values.Add("P_PROCESSRELNO", row["PROCESSRELNO"]);
            values.Add("P_RELRESOURCEID", row["PRODUCTDEFID"]);
            values.Add("P_RELRESOURCEVERSION", row["PRODUCTDEFVERSION"]);
            values.Add("P_RELRESOURCETYPE", "Product");
            //values.Add("P_INSPITEMTYPE", "OK_NG");
            values.Add("P_INSPECTIONDEFID", "OSPInspection");
            values.Add("P_INSPECTIONCLASSID", "OSPInspection");
            values.Add("P_RESULTTXNHISTKEY", row["TXNHISTKEY"].ToString());
            values.Add("P_RESULTTXNGROUPHISTKEY", row["TXNGROUPHISTKEY"].ToString());
            //측정검사 검사항목 가져오기위한 파라미터
            values.Add("P_PROCESSSEGMENTID", row["PROCESSSEGMENTID"]);
            values.Add("P_PROCESSSEGMENTVERSION", row["PROCESSSEGMENTVERSION"]);
            values.Add("P_INSPITEMTYPE", "SPC");
            values.Add("P_RESULT", "NG");

            //외관검사 - 불량코드
            DataTable exteriorDefectCode = SqlExecuter.Query("SelectOSPInspectionExterior", "10001", values);

            grdInspectionItem.DataSource = exteriorDefectCode;
            
            values.Remove("P_INSPECTIONDEFID");
            values.Remove("P_INSPECTIONCLASSID");
            values.Add("P_INSPECTIONDEFID", "OperationInspection");
            values.Add("P_INSPECTIONCLASSID", "OperationInspection");

            //측정검사
            //Item
            DataTable itemDt = SqlExecuter.Query("SelectOSPInspectionMeasure", "10001", values);
            grdInspectionItemSpec.DataSource = itemDt;

            SelectMeasureValueAfterSave(row);

            DataRow itemRow = grdInspectionItemSpec.View.GetFocusedDataRow();
            BindingMeasureValueAfterSave(itemRow);

            //불량처리
            DataTable defectTable = SqlExecuter.Query("SelectOSPInspDefect", "10001", values);
            grdDefect.DataSource = defectTable;
        }

        /// <summary>
        /// 측정검사의 검사항목이 바뀔때 다른 측정값을 검색하는 함수
        /// </summary>
        private void SelectMeasureValueAfterSave(DataRow row)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                {"ENTERPRISEID", UserInfo.Current.Enterprise},
                {"PLANTID", UserInfo.Current.Plant},
                {"P_TXNGROUPHISTKEY", row["TXNGROUPHISTKEY"]},
                {"P_RESOURCEID",row["LOTID"]},
                {"P_PROCESSRELNO",row["PROCESSRELNO"]},
                { "RESOURCETYPE","ProcessInspection"}
            };

            _selectMeasureValueDt = SqlExecuter.Query("SelectOSPMeasureByInspItem", "10001", values);
        }

        /// <summary>
        /// Select한 측정값중 inspitemId와 version 으로 해당 내역만 바인딩하는 함수
        /// </summary>
        /// <param name="row"></param>
        private void BindingMeasureValueAfterSave(DataRow row)
        {
            if (row == null)
            {
                grdMeasuredValue.View.ClearDatas();
                return;
            }

            var toBiding = _selectMeasureValueDt.AsEnumerable()
                .Where(r => r["INSPITEMID"].ToString().Equals(row["INSPITEMID"]) && r["INSPITEMVERSION"].ToString().Equals(row["INSPITEMVERSION"]))
                .ToList();

            if (toBiding.Count > 0)
            {
                grdMeasuredValue.DataSource = toBiding.CopyToDataTable();
            }
            else
            {
                grdMeasuredValue.View.ClearDatas();
            }
        }

        //popup이 닫힐때 그리드를 재조회 하는 이벤트 (팝업결과 저장 후 재조회)
        private async void Popup_FormClosed()
        {
            await OnSearchAsync();
        }

        /// <summary>
        /// 업체동의 버튼 클릭 함수
        /// </summary>
        private void Btn_CompanyAgreeClick()
        {
            DataTable checkedDt = grdAbnormal.View.GetCheckedRows();
            DataRow focusedRow = grdAbnormal.View.GetFocusedDataRow();
            if (checkedDt.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked");
                return;
            }


            //서버에서 COMPANYAGREE <- "Y", COMPANYAGREEDATE 현재 시간 입력
            ExecuteRule("SaveOSPInspectionCompanyAgree", checkedDt);

            Popup_FormClosed();
            SearchResult(focusedRow);
        }
        #endregion
    }
}
