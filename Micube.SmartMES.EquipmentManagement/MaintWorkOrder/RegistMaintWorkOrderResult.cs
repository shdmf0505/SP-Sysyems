#region using

using DevExpress.XtraLayout;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 설비수리실적등록
    /// 업  무  설  명  : 설비수리의 실적을 등록한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-10
    /// 수  정  이  력  :
    ///     2021.02.16 전우성 코드 최적화 정리 및 화면 재구성. 부품사용에 설비단 항목 추가
    ///
    /// </summary>
    public partial class RegistMaintWorkOrderResult : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _workOrderName;
        private string _workOrderStep;
        private string _searchEquipmentID;
        private string _searchAreaID;

        private ConditionItemSelectPopup popupGridSparepart;
        private ConditionItemSelectPopup equipmentPopup;
        private ConditionItemSelectPopup areaCondition;
        private ConditionItemSelectPopup areaSearchCondition;
        private ConditionItemSelectPopup equipmentSearchPopup;

        #endregion Local Variables

        #region 생성자
        public RegistMaintWorkOrderResult()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitRequiredControl("Accept");

            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
            InitializeControls();
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            gbSparePartInboundInfo.LanguageKey = "REPAIRRESULTEQUIPMENT";   // 설비수리실적
            chkEmergency.LanguageKey = "ISEMERGENCY";   // 긴급
            chkIsEquipmentDownRequest.LanguageKey = "ISEQUIPMENTDOWN";  // 비가동
            chkIsEquipmentDown.LanguageKey = "ISEQUIPMENTDOWN";  // 비가동
            grdUseEquipment.LanguageKey = "USESPAREPART";   // 부품사용
            grdAttachment.LanguageKey = "RELATIONFILE";     // 관련파일
            grdMaintWorkOrder.LanguageKey = "REPAIRRESULTMAINTWORKORDERLIST";
            btnDownloadTemplate.LanguageKey = "DOWNLOADTEMPLATEFILE";   // Template 다운로드

            smartLayoutControl1.SetLanguageKey(layoutControlItem3, "WORKORDERID"); // 신청번호
            smartLayoutControl1.SetLanguageKey(lblEquipmentCode, "EQUIPMENTID"); // 설비코드
            smartLayoutControl1.SetLanguageKey(layoutControlItem7, "REQUESTCOMMENT"); // 요청내용
            smartLayoutControl1.SetLanguageKey(lblDownType, "DOWNTYPE"); // 고장유형
            smartLayoutControl1.SetLanguageKey(layoutControlItem13, "REPAIRCOMMENT"); // 수리내용
            smartLayoutControl1.SetLanguageKey(layoutControlItem5, "EQUIPMENTNAME"); // 설비명
            smartLayoutControl1.SetLanguageKey(layoutControlItem9, "REQUESTUSER"); // 요청자
            smartLayoutControl1.SetLanguageKey(layoutControlItem10, "REQUESTTIME"); // 요청시간
            smartLayoutControl1.SetLanguageKey(lblDownCode, "DOWNCODE"); // 고장현상
            smartLayoutControl1.SetLanguageKey(lblStartRepairTime, "STARTREPAIRTIME"); // 수리시작시간
            smartLayoutControl1.SetLanguageKey(lblFinishRepairTime, "FINISHREPAIRTIME"); // 수리종료시간
            smartLayoutControl1.SetLanguageKey(layoutControlItem8, "EMERGENCYREASON"); // 긴급내용
            smartLayoutControl1.SetLanguageKey(lblRepairUser, "REPAIRUSER"); // 수리담당자
            smartLayoutControl1.SetLanguageKey(lblUserCnt, "REPAIRUSERCOUNT"); // 수리인원수
            smartLayoutControl1.SetLanguageKey(lblCauseCode, "CAUSECODE"); // 고장원인
            smartLayoutControl1.SetLanguageKey(lblRepairCode, "REPAIRCODE"); // 조치구분
            smartLayoutControl1.SetLanguageKey(layoutControlItem11, "REPAIRACCEPTUSER"); // 접수자
            smartLayoutControl1.SetLanguageKey(layoutControlItem12, "ACCEPTTIME"); // 접수시간
            smartLayoutControl1.SetLanguageKey(lblMaintKindCode, "MAINTKINDCODE"); // 보전종류
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 설비수리 실적목록

            grdMaintWorkOrder.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;
            grdMaintWorkOrder.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTATUSID", 100).SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("ACCEPTUSERID", 10).SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("FACTORYID", 100).SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTUSERID", 10).SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNTYPEID").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNCODEID", 10).SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTEPID").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRUSER").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRUSERCOUNT").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("CAUSECODEID").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRCODEID").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("MAINTKINDCODEID").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRCOMMENT").SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("FINISHREPAIRTIME").SetIsHidden();

            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTATUS", 100).SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTEP", 100).SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("ACCEPTTIME", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime);
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRACCEPTUSER", 100);
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERID", 150);
            grdMaintWorkOrder.View.AddTextBoxColumn("SCHEDULETIME", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime);
            grdMaintWorkOrder.View.AddTextBoxColumn("STARTREPAIRTIME", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime);
            grdMaintWorkOrder.View.AddTextBoxColumn("EQUIPMENTID", 120);
            grdMaintWorkOrder.View.AddTextBoxColumn("EQUIPMENTNAME", 300);
            grdMaintWorkOrder.View.AddTextBoxColumn("AREANAME", 180);
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTUSER", 80);
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTTIME", 160).SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime);
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNTYPE", 120).SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNCODE", 120).SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEQUIPMENTDOWNREQUEST", 110).SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEQUIPMENTDOWN", 120).SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTCOMMENT", 200);
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEMERGENCY", 80).SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("EMERGENCYREASON", 200);

            grdMaintWorkOrder.View.PopulateColumns();

            grdMaintWorkOrder.View.SetIsReadOnly();
            grdMaintWorkOrder.ShowStatusBar = true;

            #endregion 설비수리 실적목록

            #region 부품사용

            grdUseEquipment.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdUseEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdUseEquipment.View.AddTextBoxColumn("FACTORYID").SetIsHidden();
            grdUseEquipment.View.AddSpinEditColumn("OLDQTY").SetIsHidden();
            grdUseEquipment.View.AddSpinEditColumn("EQUIPMENTID").SetIsHidden();

            #region 부품

            popupGridSparepart = grdUseEquipment.View.AddSelectPopupColumn("SPAREPARTID", 150, new SqlQuery("GetSparePartStockListForPMByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                                                .SetPopupLayout("SPAREPARTID", PopupButtonStyles.Ok_Cancel, true, false)
                                                .SetPopupResultCount(1)
                                                .SetValidationIsRequired()
                                                .SetPopupResultMapping("SPAREPARTID", "SPAREPARTID")
                                                .SetPopupLayoutForm(900, 400)
                                                .SetRelationIds("FACTORYID")
                                                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                                {
                                                    foreach (DataRow row in selectedRows)
                                                    {
                                                        grdUseEquipment.View.GetFocusedDataRow()["SPAREPARTID"] = row["SPAREPARTID"];
                                                        grdUseEquipment.View.GetFocusedDataRow()["MODELNAME"] = row["MODELNAME"];
                                                        grdUseEquipment.View.GetFocusedDataRow()["MAKER"] = row["MAKER"];
                                                        grdUseEquipment.View.GetFocusedDataRow()["SPEC"] = row["SPEC"];
                                                        grdUseEquipment.View.GetFocusedDataRow()["CURRENTQTY"] = row["QTY"];
                                                        grdUseEquipment.View.GetFocusedDataRow()["FACTORYID"] = row["FACTORYID"];
                                                    }
                                                });

            popupGridSparepart.Conditions.AddTextBox("SPAREPARTNAME");
            popupGridSparepart.Conditions.AddTextBox("SPAREPARTID");
            popupGridSparepart.Conditions.AddTextBox("MODELNAME");
            popupGridSparepart.Conditions.AddTextBox("FACTORYID").SetPopupDefaultByGridColumnId("FACTORYID").SetIsHidden();

            popupGridSparepart.GridColumns.AddTextBoxColumn("SPAREPARTID", 80).SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("SPAREPARTNAME", 80).SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("MODELNAME", 100).SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("MAKER", 80).SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("SPEC", 80).SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("QTY", 80).SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("DESCRIPTION", 200).SetIsReadOnly();
            popupGridSparepart.GridColumns.AddTextBoxColumn("FACTORYID", 100).SetIsHidden();

            #endregion 부품

            grdUseEquipment.View.AddTextBoxColumn("MODELNAME", 150).SetIsReadOnly();
            grdUseEquipment.View.AddComboBoxColumn("CHILDEQUIPMENTID", 100, new SqlQuery("GetChildEquipmentByEquipment", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                           .SetLabel("CHILDEQUIPMENT").SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);
            grdUseEquipment.View.AddTextBoxColumn("MAKER", 100).SetIsReadOnly();
            grdUseEquipment.View.AddTextBoxColumn("SPEC", 100).SetIsReadOnly();
            grdUseEquipment.View.AddSpinEditColumn("CURRENTQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetIsReadOnly();
            grdUseEquipment.View.AddSpinEditColumn("QTY", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetValidationIsRequired();

            grdUseEquipment.View.PopulateColumns();

            grdUseEquipment.ShowStatusBar = true;

            #endregion 부품사용
        }

        /// <summary>
        /// 컨트롤러 설정
        /// </summary>
        private void InitializeControls()
        {
            gbSparePartInboundInfo.GridButtonItem = GridButtonItem.None;

            #region DownType콤보박스 초기화

            cboDownType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDownType.ValueMember = "CODEID";
            cboDownType.DisplayMember = "CODENAME";
            cboDownType.EditValue = "1";

            cboDownType.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentDownType" } });

            cboDownType.ShowHeader = false;

            #endregion DownType콤보박스 초기화

            #region DownCode콤보박스 초기화

            cboDownCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDownCode.ValueMember = "CODEID";
            cboDownCode.DisplayMember = "CODENAME";
            cboDownCode.EditValue = "1";

            cboDownCode.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentDownCode" } });

            cboDownCode.ShowHeader = false;

            #endregion DownCode콤보박스 초기화

            #region CauseCode콤보박스 초기화

            cboCauseCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboCauseCode.ValueMember = "CODEID";
            cboCauseCode.DisplayMember = "CODENAME";
            cboCauseCode.EditValue = "1";

            cboCauseCode.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentDownCauseCode" } });

            cboCauseCode.ShowHeader = false;

            #endregion CauseCode콤보박스 초기화

            #region RepairCode콤보박스 초기화

            cboRepairCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboRepairCode.ValueMember = "CODEID";
            cboRepairCode.DisplayMember = "CODENAME";
            cboRepairCode.EditValue = "1";

            cboRepairCode.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentRepairCode" } });

            cboRepairCode.ShowHeader = false;

            #endregion RepairCode콤보박스 초기화

            #region MaintKind 콤보박스 초기화

            cboMaintKindCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboMaintKindCode.ValueMember = "CODEID";
            cboMaintKindCode.DisplayMember = "CODENAME";
            cboMaintKindCode.EditValue = "1";

            cboMaintKindCode.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "MaintKind" } });

            cboMaintKindCode.ShowHeader = false;

            #endregion MaintKind 콤보박스 초기화

            #region 결재지정자를 조회

            equipmentPopup = new ConditionItemSelectPopup();
            equipmentPopup.Id = "EQUIPMENTID";
            equipmentPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}");
            equipmentPopup.SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false);
            equipmentPopup.SetPopupResultCount(1);
            equipmentPopup.SetPopupLayoutForm(1000, 600, FormBorderStyle.FixedToolWindow);
            equipmentPopup.SetPopupAutoFillColumns("EQUIPMENTID");
            equipmentPopup.SetPopupResultMapping("EQUIPMENTID", "EQUIPMENTID");
            equipmentPopup.ValueFieldName = "EQUIPMENTID";
            equipmentPopup.DisplayFieldName = "EQUIPMENTID";
            equipmentPopup.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                foreach (DataRow row in selectedRows)
                {
                    txtEquipmentName.EditValue = row["EQUIPMENTNAME"];
                    txtEquipmentCode.EditValue = row["EQUIPMENTID"];
                    txtFactoryID.EditValue = row["FACTORYID"];

                    (grdUseEquipment.View.GetConditions().GetCondition("CHILDEQUIPMENTID") as ConditionItemComboBox).Query = new SqlQuery("GetChildEquipmentByEquipment", "10001", $"EQUIPMENTID={row["EQUIPMENTID"]}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                    grdUseEquipment.View.PopulateColumns();
                }
            });

            equipmentPopup.Conditions.AddTextBox("EQUIPMENTNAME").SetLabel("EQUIPMENTNAME");

            InitializeReceiptAreaPopupForPopup(equipmentPopup.Conditions);

            equipmentPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                          .SetEmptyItem("", "", true)
                          .SetLabel("PROCESSSEGMENTCLASS")
                          .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

            equipmentPopup.IsMultiGrid = false;
            equipmentPopup.GridColumns.AddTextBoxColumn("FACTORYID", 100).SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200).SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUPMENTLEVEL", 200).SetIsHidden();

            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120).SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250).SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("AREANAME", 120).SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200).SetIsReadOnly();

            txtEquipmentCode.SelectPopupCondition = equipmentPopup;

            #endregion 결재지정자를 조회
        }

        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeReceiptAreaPopupForPopup(ConditionCollection tempControl)
        {
            areaCondition = tempControl.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetPopupResultMapping("AREANAME", "AREANAME");

            areaCondition.Conditions.AddTextBox("AREANAME");
            areaCondition.Conditions.AddTextBox("AREAID");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMaintWorkOrder.View.ColumnFilterChanged += (s, e) => DisplayMaintWorkOrderInfo();
            grdMaintWorkOrder.View.FocusedRowChanged += grdMaintWorkOrder_FocusedRowChanged;            
            chkIsEquipmentDown.EditValueChanging += ChkIsEquipmentDown_EditValueChanging;

            // Site관련정보를 화면로딩후 설정한다.
            Shown += (s, e) =>
            {
                ChangeSiteCondition();

                ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += RegistMaintWorkOrderResult_EditValueChanged;
            };

            // Dwonload 버튼 클릭이벤트
            btnDownloadTemplate.Click += (s, e) =>
            {
                using (var fs = new System.IO.FileStream(Environment.CurrentDirectory + @"\EQP_TEMPLATE.xlsx", System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    fs.Write(global::Micube.SmartMES.EquipmentManagement.Properties.EqpResource.EQP_TEMPLATE, 0, global::Micube.SmartMES.EquipmentManagement.Properties.EqpResource.EQP_TEMPLATE.Length);
                }

                System.Diagnostics.Process.Start(Environment.CurrentDirectory + @"\EQP_TEMPLATE.xlsx");
            };

            // 추가 버튼 클릭 시 호출
            grdUseEquipment.View.AddingNewRow += (s, e) =>
            {
                if (txtEquipmentCode.EditValue.Equals(""))
                {
                    grdUseEquipment.View.DeleteRow(grdUseEquipment.View.FocusedRowHandle);
                    return;
                }

                grdUseEquipment.View.SetFocusedRowCellValue("EQUIPMENTID", txtEquipmentCode.EditValue);
                grdUseEquipment.View.SetFocusedRowCellValue("FACTORYID", txtFactoryID.Text);
                grdUseEquipment.View.SetFocusedRowCellValue("OLDQTY", "0");
            };

            // 설비코드 값 변경시 이벤트
            txtEquipmentCode.EditValueChanged += (s, e) =>
            {
                txtFactoryID.Text = txtEquipmentCode.EditValue.Equals(string.Empty) ? string.Empty : txtFactoryID.Text;

                (grdUseEquipment.View.GetConditions().GetCondition("CHILDEQUIPMENTID") as ConditionItemComboBox).Query = new SqlQuery("GetChildEquipmentByEquipment", "10001", $"EQUIPMENTID={txtEquipmentCode.EditValue}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
                grdUseEquipment.View.PopulateColumns();
            };

            chkIsEquipmentDownRequest.EditValueChanged += (s, e) => chkIsEquipmentDown.Checked = chkIsEquipmentDownRequest.Checked;
        }

        /// <summary>
        /// 설비수리 실적목록 row 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdMaintWorkOrder_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                return;
            }

            DisplayMaintWorkOrderInfo(grdMaintWorkOrder.View.GetFocusedDataRow());
        }

        /// <summary>
        /// combo EditValue change 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistMaintWorkOrderResult_EditValueChanged(object sender, EventArgs e) => ChangeSiteCondition();

        /// <summary>
        /// 체크박스변경이벤트 - 점검등록시 비가동이 선택되었다면 선택되어서는 안된다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkIsEquipmentDown_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (chkIsEquipmentDownRequest.Checked)
            {
                e.Cancel = true;
            }
        }

        #endregion Event

        #region 툴바

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("SelfPMOrder"))
            {
                InitializeInsertForm();
                ControlEnableProcess("SelfMaintWorkOrder");
                InitRequiredControl("SelfMaintWorkOrder");
                _workOrderStep = "Accept";
            }
            else if (btn.Name.ToString().Equals("Save"))
            {
                //Validation 체크 부분 작성 필요
                DialogManager.ShowWaitArea(this.pnlContent);

                grdUseEquipment.View.FocusedRowHandle = grdUseEquipment.View.FocusedRowHandle;
                grdUseEquipment.View.FocusedColumn = grdUseEquipment.View.Columns["SPAREPARTNAME"];
                grdUseEquipment.View.ShowEditor();

                try
                {
                    string workOrderID = "";

                    //txtEquipmentCode는 자체보전일때만 ReadOnly가 false가 된다.
                    //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                    if (ValidateContent(_workOrderStep, txtEquipmentCode.ReadOnly ? false : true, out string messageCode))
                    {
                        DataSet maintWorkOrderSet = new DataSet();
                        //치공구 제작의뢰를 입력
                        DataTable maintWorkOrderTable = CreateSaveDatatable();
                        DataRow orderRow = maintWorkOrderTable.NewRow();
                        DateTime requestDate = DateTime.Now;

                        if (_workOrderStep.Equals("Accept"))
                        {
                            orderRow["_STATE_"] = "added";
                            //시작일은 시스템일자
                            orderRow["STARTTIME"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else if (_workOrderStep.Equals("Start"))
                        {
                            orderRow["_STATE_"] = "modified";
                            //종료일은 시스템일자
                            orderRow["FINISHTIME"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            ShowMessage(MessageBoxButtons.OK, "ValidateRepairResultWorkOrderAcceptStartStatus", "");
                            return;
                        }

                        orderRow["WORKORDERID"] = txtWorkNo.EditValue;
                        workOrderID = txtWorkNo.Text;
                        orderRow["WORKORDERNAME"] = _workOrderName;
                        orderRow["WORKORDERTYPE"] = txtWorkNo.EditValue;
                        orderRow["WORKORDERSTEP"] = _workOrderStep;
                        orderRow["WORKORDERSTATUS"] = "";

                        if (txtEquipmentCode.ReadOnly ? false : true)
                        {
                            orderRow["SELFMAINTWORKORDER"] = "Y";
                            //자체보전만을 위한 데이터를 입력
                            orderRow["REQUESTCOMMENTS"] = txtRequestComment.EditValue;
                            orderRow["ISEMERGENCY"] = chkEmergency.Checked ? "Y" : "N";
                            orderRow["ISEMERGENCYREASON"] = txtEmergencyContent.EditValue;

                            orderRow["REQUESTTIME"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");
                            orderRow["ACCEPTTIME"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");
                            orderRow["REQUESTUSER"] = UserInfo.Current.Id;
                            orderRow["ACCEPTUSER"] = txtReceiptUser.EditValue;
                        }
                        else
                        {
                            orderRow["SELFMAINTWORKORDER"] = "N";
                        }

                        orderRow["EQUIPMENTID"] = txtEquipmentCode.EditValue;
                        orderRow["EQUIPMENTNAME"] = txtEquipmentName.EditValue;
                        orderRow["REPAIRUSER"] = txtRepairUser.EditValue;
                        orderRow["REPAIRUSERCOUNT"] = txtRepairUserCount.EditValue;
                        orderRow["DOWNTYPE"] = cboDownType.EditValue;
                        orderRow["DOWNTYPENAME"] = cboDownType.GetDisplayText();
                        orderRow["DOWNCODE"] = cboDownCode.EditValue;
                        orderRow["CAUSECODE"] = cboCauseCode.EditValue;
                        orderRow["REPAIRCODE"] = cboRepairCode.EditValue;
                        orderRow["MAINTKINDCODE"] = cboMaintKindCode.EditValue;
                        orderRow["REPAIRCOMMENT"] = txtRepairContent.EditValue;
                        orderRow["ISEQUIPMENTDOWN"] = chkIsEquipmentDown.Checked ? "Y" : "N";

                        orderRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        orderRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                        orderRow["VALIDSTATE"] = "Valid";

                        orderRow["CREATOR"] = UserInfo.Current.Id;
                        orderRow["MODIFIER"] = UserInfo.Current.Id;

                        maintWorkOrderTable.Rows.Add(orderRow);

                        maintWorkOrderSet.Tables.Add(maintWorkOrderTable);

                        //SparePart부분 입력
                        DataTable sparePartTable = CreateSparePartDatatable();
                        if (grdUseEquipment.DataSource != null)
                        {
                            foreach (DataRow changedRow in grdUseEquipment.GetChangedRows().Rows)
                            {
                                DataRow sparePartRow = sparePartTable.NewRow();

                                sparePartRow["WORKORDERID"] = txtWorkNo.EditValue;
                                sparePartRow["SPAREPARTID"] = changedRow.GetString("SPAREPARTID");
                                sparePartRow["FACTORYID"] = changedRow.GetString("FACTORYID");
                                sparePartRow["QTY"] = changedRow.GetString("QTY");

                                sparePartRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                sparePartRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                                sparePartRow["CHILDEQUIPMENTID"] = Format.GetString(changedRow.GetString("CHILDEQUIPMENTID"), string.Empty);

                                sparePartRow["CREATOR"] = UserInfo.Current.Id;
                                sparePartRow["MODIFIER"] = UserInfo.Current.Id;

                                if (changedRow.GetString("_STATE_").Equals("deleted"))
                                {
                                    sparePartRow["_STATE_"] = "deleted";
                                    sparePartRow["VALIDSTATE"] = "Invalid";
                                }
                                else
                                {
                                    sparePartRow["_STATE_"] = "added";
                                    sparePartRow["VALIDSTATE"] = "Valid";
                                }

                                sparePartTable.Rows.Add(sparePartRow);
                            }
                        }

                        maintWorkOrderSet.Tables.Add(sparePartTable);

                        //데이터 저장전 첨부파일의 저장을 진행
                        if (grdAttachment.Resource.Type.Equals("MaintWorkOrder") && grdAttachment.Resource.Id.Equals(txtWorkNo.Text))
                        {
                            if (grdAttachment.GetChangedRows().Rows.Count > 0)
                            {
                                grdAttachment.SaveChangedFiles();

                                DataTable changed = grdAttachment.GetChangedRows();

                                ExecuteRule("SaveObjectFile", changed);
                            }
                        }

                        DataTable resultTable = ExecuteRule<DataTable>("RepairResultMaintWorkOrder", maintWorkOrderSet);

                        //Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                        DataRow resultRow = resultTable.Rows[0];

                        ControlEnableProcess("modified");

                        ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                        Research(resultRow.GetString("WORKORDERID"));
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
                finally
                {
                    DialogManager.CloseWaitArea(this.pnlContent);
                }
            }
        }

        #endregion 툴바

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                await base.OnSearchAsync();

                grdMaintWorkOrder.View.ClearDatas();

                Dictionary<string, object> values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

                if (Conditions.GetValue("AREANAME").ToString() != "")
                {
                    values.Add("AREAID", _searchAreaID);
                }

                if (Conditions.GetValue("EQUIPMENTNAME").ToString() != "")
                {
                    values.Add("EQUIPMENTID", _searchEquipmentID);
                }

                if (Conditions.GetValue("p_WORKORDERSTEP").ToString() != "")
                {
                    values.Add("WORKORDERSTEP", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTEP").ToString()));
                }

                if (SqlExecuter.Query("GetRepairResultMaintWorkOrderListForResultByEqp", "10001", values) is DataTable dt)
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdMaintWorkOrder.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Site
            Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
                      .SetLabel("FACTORY")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(0.2)
                      .SetEmptyItem("", "", true)
                      .SetRelationIds("P_PLANTID");

            #region 작업장

            areaSearchCondition = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                            .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
                                            .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                                            .SetPopupAutoFillColumns("AREANAME")
                                            .SetLabel("AREA")
                                            .SetPopupResultCount(1)
                                            .SetPosition(0.3)
                                            .SetPopupResultMapping("AREANAME", "AREANAME")
                                            .SetPopupApplySelection((selectedRows, dataGridRows) =>
                                            {
                                                //작업장 변경 시 작업자 조회
                                                selectedRows.Cast<DataRow>().ForEach(row =>
                                                {
                                                    _searchAreaID = row.GetString("AREAID");
                                                });
                                            });

            areaSearchCondition.Conditions.AddTextBox("AREANAME");
            areaSearchCondition.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            areaSearchCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            #endregion 작업장

            #region 설비그룹

            equipmentSearchPopup = Conditions.AddSelectPopup("EQUIPMENTNAME", new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                                             .SetPopupLayout("EQUIPMENTNAME", PopupButtonStyles.Ok_Cancel, true, false)
                                             .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
                                             .SetPopupResultMapping("EQUIPMENTNAME", "EQUIPMENTNAME")
                                             .SetLabel("EQUIPMENT")
                                             .SetPopupResultCount(1)
                                             .SetPosition(0.4)
                                             .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                             {
                                                 int i = 0;

                                                 foreach (DataRow row in selectedRows)
                                                 {
                                                     if (i == 0)
                                                     {
                                                         _searchEquipmentID = row["EQUIPMENTID"].ToString();
                                                     }
                                                     i++;
                                                 }
                                             });

            equipmentSearchPopup.Conditions.AddTextBox("EQUIPMENTNAME").SetLabel("EQUIPMENTNAME");
            InitializeReceiptAreaPopupForPopup(equipmentSearchPopup.Conditions);

            equipmentSearchPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                                .SetEmptyItem("", "", true)
                                .SetLabel("PROCESSSEGMENTCLASS")
                                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;

            equipmentSearchPopup.IsMultiGrid = false;
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("FACTORYID", 100).SetIsHidden();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200).SetIsHidden();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("EQUPMENTLEVEL", 200).SetIsHidden();

            equipmentSearchPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120).SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250).SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("AREANAME", 120).SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200).SetIsReadOnly();

            #endregion 설비그룹
        }

        #endregion 검색

        #region Private Function

        /// <summary>
        /// 필수입력항목들을 체크한다.
        /// </summary>
        /// <param name="currentStatus"></param>
        private void InitRequiredControl(string currentStatus)
        {
            if (currentStatus.Equals("Accept"))
            {
                SetRequiredValidationControl(lblEquipmentCode, false);
                SetRequiredValidationControl(lblDownType, true);
                SetRequiredValidationControl(lblDownCode, true);
                SetRequiredValidationControl(lblCauseCode, false);
                SetRequiredValidationControl(lblRepairCode, false);
                SetRequiredValidationControl(lblMaintKindCode, false);
                SetRequiredValidationControl(lblStartRepairTime, false);
                SetRequiredValidationControl(lblFinishRepairTime, false);

                SetRequiredValidationControl(lblRepairUser, true);
                SetRequiredValidationControl(lblUserCnt, true);
            }
            else if (currentStatus.Equals("Start"))
            {
                SetRequiredValidationControl(lblEquipmentCode, false);
                SetRequiredValidationControl(lblDownType, true);
                SetRequiredValidationControl(lblDownCode, true);
                SetRequiredValidationControl(lblCauseCode, true);
                SetRequiredValidationControl(lblRepairCode, true);
                SetRequiredValidationControl(lblMaintKindCode, true);
                SetRequiredValidationControl(lblStartRepairTime, false);
                SetRequiredValidationControl(lblFinishRepairTime, false);

                SetRequiredValidationControl(lblRepairUser, true);
                SetRequiredValidationControl(lblUserCnt, true);
            }
            else if (currentStatus.Equals("SelfMaintWorkOrder"))
            {
                SetRequiredValidationControl(lblEquipmentCode, true);
                SetRequiredValidationControl(lblDownType, true);
                SetRequiredValidationControl(lblDownCode, true);
                SetRequiredValidationControl(lblCauseCode, true);
                SetRequiredValidationControl(lblRepairCode, false);
                SetRequiredValidationControl(lblMaintKindCode, false);
                SetRequiredValidationControl(lblStartRepairTime, false);
                SetRequiredValidationControl(lblFinishRepairTime, false);

                SetRequiredValidationControl(lblRepairUser, true);
                SetRequiredValidationControl(lblUserCnt, true);
            }
            else if (currentStatus.Equals("Finish"))
            {
                SetRequiredValidationControl(lblEquipmentCode, false);
                SetRequiredValidationControl(lblDownType, false);
                SetRequiredValidationControl(lblDownCode, false);
                SetRequiredValidationControl(lblCauseCode, false);
                SetRequiredValidationControl(lblRepairCode, false);
                SetRequiredValidationControl(lblMaintKindCode, false);
                SetRequiredValidationControl(lblStartRepairTime, false);
                SetRequiredValidationControl(lblFinishRepairTime, false);

                SetRequiredValidationControl(lblRepairUser, false);
                SetRequiredValidationControl(lblUserCnt, false);
            }
        }

        /// <summary>
        /// 재검색
        /// </summary>
        /// <param name="workOrderID"></param>
        private void Research(string workOrderID)
        {
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);

            if (Conditions.GetValue("AREANAME").ToString() != "")
            {
                values.Add("AREAID", _searchAreaID);
            }

            if (Conditions.GetValue("EQUIPMENTNAME").ToString() != "")
            {
                values.Add("EQUIPMENTID", _searchEquipmentID);
            }
            if (Conditions.GetValue("p_WORKORDERSTEP").ToString() != "")
            {
                values.Add("WORKORDERSTEP", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTEP").ToString()));
            }

            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);

                //다른값으로 바인딩되지 않도록 이벤트 제거
                grdMaintWorkOrder.View.FocusedRowChanged -= grdMaintWorkOrder_FocusedRowChanged;

                DataTable maintWorkOrderTable = SqlExecuter.Query("GetRepairResultMaintWorkOrderListForResultByEqp", "10001", values);
                grdMaintWorkOrder.DataSource = maintWorkOrderTable;

                if (maintWorkOrderTable.Rows.Count < 1)
                {
                    InitializeInsertForm();
                }
                else
                {
                    //검색조건과 데이터가 맞지않아  RowHandle을 못가져올 수 있다.
                    int rowHandle = GetRowHandleInGrid(grdMaintWorkOrder, "WORKORDERID", workOrderID);
                    grdMaintWorkOrder.View.FocusedRowHandle = rowHandle < 0 ? 0 : rowHandle;
                    DisplayMaintWorkOrderInfo();
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(Format.GetString(ex));
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);
                grdMaintWorkOrder.View.FocusedRowChanged += grdMaintWorkOrder_FocusedRowChanged;
            }
        }

        /// <summary>
        /// 다중선택된 조회조건을 '', ''의 형태로 변경
        /// </summary>
        /// <param name="originCondition"></param>
        /// <returns></returns>
        private string GetConditionStringValue(string originCondition)
        {
            if (originCondition.IndexOf(",") > -1)
            {
                string[] conditions = originCondition.Split(',');
                string returnStr = "";

                // ' 기호 추가
                for (int i = 0; i < conditions.Length; i++)
                {
                    conditions[i] = "'" + conditions[i].Trim() + "'";
                }

                // ,로 구분하여 합산
                for (int i = 0; i < conditions.Length; i++)
                {
                    returnStr += i.Equals(0) ? conditions[i] : string.Concat(",", conditions[i]);
                }

                return returnStr;
            }
            else
            {
                return "'" + originCondition.Trim() + "'";
            }
        }

        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        private void SparePartSearch(string workOrderID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "WORKORDERID", workOrderID },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };
            param = Commons.CommonFunction.ConvertParameter(param);

            grdUseEquipment.DataSource = SqlExecuter.Query("GetMaintWorkOrderPMSparePartListByEqp", "10001", param);
        }

        /// <summary>
        /// 내용점검
        /// </summary>
        /// <param name="action"></param>
        /// <param name="isSelfMaintWorkOrder"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private bool ValidateContent(string action, bool isSelfMaintWorkOrder, out string messageCode)
        {
            messageCode = string.Empty;

            if (action.Equals("Accept")) //점검생략을 진행하고자 하는 경우
            {
                if (!ValidateEditValue(txtEquipmentCode.EditValue))
                {
                    messageCode = "ValidateSetEquipmentCode";
                    return false;
                }

                if (!ValidateEditValue(cboDownType.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateEditValue(cboDownCode.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (isSelfMaintWorkOrder)
                {
                    if (!ValidateEditValue(cboCauseCode.EditValue))
                    {
                        messageCode = "ValidateRequiredData";
                        return false;
                    }
                }

                if (!ValidateEditValue(txtRepairUser.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateNumericBox(txtRepairUserCount.EditValue, 1))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (grdUseEquipment.DataSource != null)
                {
                    foreach (DataRow sparePartRow in grdUseEquipment.GetChangedRows().Rows)
                    {
                        if (!ValidateCellInGrid(sparePartRow, new string[] { "SPAREPARTID", "QTY" }))
                        {
                            messageCode = "ValidateMaintWorkOrderSparePartModified";
                            return false;
                        }

                        string strQty = sparePartRow["QTY"].ToString();
                        string strStockQty = sparePartRow["CURRENTQTY"].ToString();

                        double dblQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDouble(strQty));
                        double dblStockQty = (strStockQty.ToString().Equals("") ? 0 : Convert.ToDouble(strStockQty));

                        if (dblQty.Equals(0))
                        {
                            messageCode = "InValidOspRequiredField";
                            return false;
                        }

                        if (dblQty > dblStockQty)
                        {
                            messageCode = string.Format("InValidCsmData004", sparePartRow["SPAREPARTID"].ToString()); //다국어 메세지 추가
                            return false;
                        }
                    }
                }
            }
            else if (action.Equals("Start")) //저장을 하고자 하는 경우
            {
                if (!ValidateEditValue(cboDownType.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateEditValue(cboDownCode.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateEditValue(cboCauseCode.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateEditValue(cboMaintKindCode.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateEditValue(cboRepairCode.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateEditValue(txtRepairUser.EditValue))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (!ValidateNumericBox(txtRepairUserCount.EditValue, 1))
                {
                    messageCode = "ValidateRequiredData";
                    return false;
                }

                if (grdUseEquipment.DataSource != null)
                {
                    foreach (DataRow sparePartRow in grdUseEquipment.GetChangedRows().Rows)
                    {
                        if (!ValidateCellInGrid(sparePartRow, new string[] { "SPAREPARTID", "QTY" }))
                        {
                            messageCode = "ValidateMaintWorkOrderSparePartModified";
                            return false;
                        }

                        string strQty = sparePartRow["QTY"].ToString();
                        string strStockQty = sparePartRow["CURRENTQTY"].ToString();

                        double dblQty = (strQty.ToString().Equals("") ? 0 : Convert.ToDouble(strQty));
                        double dblStockQty = (strStockQty.ToString().Equals("") ? 0 : Convert.ToDouble(strStockQty));

                        if (dblQty.Equals(0))
                        {
                            messageCode = "InValidOspRequiredField";
                            return false;
                        }
                        if (dblQty > dblStockQty)
                        {
                            messageCode = string.Format("InValidCsmData004", sparePartRow["SPAREPARTID"].ToString()); //다국어 메세지 추가
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 특정값에 대한 점검, 기준숫자보다 큰지 점검
        /// </summary>
        /// <param name="editValue"></param>
        /// <param name="ruleValue"></param>
        /// <returns></returns>
        private bool ValidateNumericBox(object editValue, int ruleValue)
        {
            //값이 없으면 안된다.
            if (editValue == null)
            {
                return false;
            }

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(editValue.ToString(), out int resultValue))
            {
                if (resultValue < ruleValue)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //모두 통과했으므로 Validation Check완료
            return true;
        }

        /// <summary>
        /// 특정값에 대한 점검
        /// </summary>
        /// <param name="editValue"></param>
        /// <returns></returns>
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null || editValue.ToString() == "")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 그리드의 특정 컬럼에 대한 Validation
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        private bool ValidateCellInGrid(DataRow currentRow, string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (currentRow[columnName] == null || currentRow[columnName].ToString() == "")
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 필수입력항목설정
        /// </summary>
        /// <param name="requiredControl"></param>
        /// <param name="isRequired"></param>
        private void SetRequiredValidationControl(LayoutControlItem requiredControl, bool isRequired) => requiredControl.AppearanceItemCaption.ForeColor = isRequired ? Color.Red : Color.Black;

        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                txtFactoryID.EditValue = "";
                txtWorkNo.EditValue = "";
                txtEquipmentCode.EditValue = "";
                txtEquipmentName.EditValue = "";
                txtRequestComment.EditValue = "";
                txtRequestTime.EditValue = "";
                txtEmergencyContent.EditValue = "";
                txtRequestUser.EditValue = "";
                txtReceiptTime.EditValue = "";
                txtReceiptUser.EditValue = "";
                chkEmergency.Checked = false;
                chkIsEquipmentDownRequest.Checked = false;
                chkIsEquipmentDown.Checked = false;
                cboDownCode.EditValue = "";
                cboDownType.EditValue = "";
                txtRepairUser.EditValue = "";
                txtRepairUserCount.EditValue = "";
                cboCauseCode.EditValue = "";
                cboRepairCode.EditValue = "";
                cboMaintKindCode.EditValue = "";
                txtRepairContent.EditValue = "";
                deStartRepairTime.EditValue = "";
                deEndRepairTime.EditValue = "";

                grdUseEquipment.DataSource = null;
                grdAttachment.DataSource = null;

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");
            }
            catch (Exception err)
            {
                ShowError(err);
            }
        }

        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어
        /// </summary>
        /// <param name="blProcess"></param>
        private void ControlEnableProcess(string currentStatus)
        {
            txtWorkNo.ReadOnly = true;
            txtEquipmentName.ReadOnly = true;
            txtRequestUser.ReadOnly = true;
            txtRequestTime.ReadOnly = true;
            txtReceiptTime.ReadOnly = true;
            chkIsEquipmentDownRequest.ReadOnly = true;

            if (currentStatus.Equals("Accept"))
            {
                txtEquipmentCode.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtReceiptUser.ReadOnly = true;
                chkEmergency.ReadOnly = true;
                chkIsEquipmentDown.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                txtRepairUser.ReadOnly = false;
                txtRepairUserCount.ReadOnly = false;
                cboCauseCode.ReadOnly = true;
                cboRepairCode.ReadOnly = true;
                cboMaintKindCode.ReadOnly = true;
                txtRepairContent.ReadOnly = true;
                deStartRepairTime.ReadOnly = true;
                deEndRepairTime.ReadOnly = true;
                grdAttachment.ButtonVisible = true;
                grdUseEquipment.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
                grdUseEquipment.View.SetIsReadOnly(false);

                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Text = Language.Get("STARTDOREPAIR");
                    }
                }
            }
            else if (currentStatus.Equals("Start"))
            {
                txtEquipmentCode.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtReceiptUser.ReadOnly = true;
                chkEmergency.ReadOnly = true;
                chkIsEquipmentDown.ReadOnly = true;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                txtRepairUser.ReadOnly = false;
                txtRepairUserCount.ReadOnly = false;
                cboCauseCode.ReadOnly = false;
                cboRepairCode.ReadOnly = false;
                cboMaintKindCode.ReadOnly = false;
                txtRepairContent.ReadOnly = false;
                deStartRepairTime.ReadOnly = true;
                deEndRepairTime.ReadOnly = true;
                grdAttachment.ButtonVisible = true;
                grdUseEquipment.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
                grdUseEquipment.View.SetIsReadOnly(false);

                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Text = Language.Get("FINISHDOREPAIR");
                    }
                }
            }
            else if (currentStatus.Equals("SelfMaintWorkOrder"))
            {
                txtEquipmentCode.ReadOnly = false;
                txtRequestComment.ReadOnly = false;
                txtEmergencyContent.ReadOnly = false;
                txtReceiptUser.ReadOnly = false;
                chkEmergency.ReadOnly = false;
                chkIsEquipmentDown.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                txtRepairUser.ReadOnly = false;
                txtRepairUserCount.ReadOnly = false;
                cboCauseCode.ReadOnly = false;
                cboRepairCode.ReadOnly = true;
                cboMaintKindCode.ReadOnly = true;
                txtRepairContent.ReadOnly = true;
                deStartRepairTime.ReadOnly = true;
                deEndRepairTime.ReadOnly = true;
                grdAttachment.ButtonVisible = false;
                grdUseEquipment.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Export;
                grdUseEquipment.View.SetIsReadOnly(false);

                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Text = Language.Get("STARTDOREPAIR");
                    }
                }
            }
            else if (currentStatus == "Finish") //
            {
                txtEquipmentCode.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtReceiptUser.ReadOnly = true;
                chkEmergency.ReadOnly = true;
                chkIsEquipmentDown.ReadOnly = true;
                cboDownCode.ReadOnly = true;
                cboDownType.ReadOnly = true;
                txtRepairUser.ReadOnly = true;
                txtRepairUserCount.ReadOnly = true;
                cboCauseCode.ReadOnly = true;
                cboRepairCode.ReadOnly = true;
                cboMaintKindCode.ReadOnly = true;
                txtRepairContent.ReadOnly = true;
                deStartRepairTime.ReadOnly = true;
                deEndRepairTime.ReadOnly = true;
                grdAttachment.ButtonVisible = false;
                grdUseEquipment.GridButtonItem = GridButtonItem.Export;
                grdUseEquipment.View.SetIsReadOnly(true);

                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Text = Language.Get("SAVE");
                    }
                }
            }
            else
            {
                txtEquipmentCode.ReadOnly = true;
                txtRequestComment.ReadOnly = true;
                txtEmergencyContent.ReadOnly = true;
                txtReceiptUser.ReadOnly = true;
                chkEmergency.ReadOnly = true;
                chkIsEquipmentDown.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                cboDownType.ReadOnly = false;
                txtRepairUser.ReadOnly = false;
                txtRepairUserCount.ReadOnly = false;
                cboCauseCode.ReadOnly = false;
                cboRepairCode.ReadOnly = false;
                cboMaintKindCode.ReadOnly = false;
                txtRepairContent.ReadOnly = false;
                deStartRepairTime.ReadOnly = false;
                deEndRepairTime.ReadOnly = false;
                grdAttachment.Enabled = true;
                grdUseEquipment.GridButtonItem = GridButtonItem.Export;
                grdUseEquipment.View.SetIsReadOnly(true);

                if (pnlToolbar.Controls["layoutToolbar"] != null)
                {
                    if (pnlToolbar.Controls["layoutToolbar"].Controls["Save"] != null)
                    {
                        pnlToolbar.Controls["layoutToolbar"].Controls["Save"].Text = Language.Get("SAVE");
                    }
                }
            }
        }

        /// <summary>
        /// 설비수리실적등록 DataTable 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable
            {
                TableName = "maintWorkOrderList"
            };

            dt.Columns.Add("WORKORDERID");
            dt.Columns.Add("WORKORDERNAME");
            dt.Columns.Add("WORKORDERTYPE");
            dt.Columns.Add("WORKORDERSTEP");
            dt.Columns.Add("WORKORDERSTATUS");
            dt.Columns.Add("SELFMAINTWORKORDER");
            dt.Columns.Add("STARTTIME");
            dt.Columns.Add("FINISHTIME");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("EQUIPMENTNAME");
            dt.Columns.Add("REPAIRUSER");
            dt.Columns.Add("REPAIRUSERCOUNT");
            dt.Columns.Add("DOWNTYPE");
            dt.Columns.Add("DOWNTYPENAME");
            dt.Columns.Add("DOWNCODE");
            dt.Columns.Add("CAUSECODE");
            dt.Columns.Add("REPAIRCODE");
            dt.Columns.Add("MAINTKINDCODE");
            dt.Columns.Add("REPAIRCOMMENT");
            dt.Columns.Add("ISEQUIPMENTDOWN");

            dt.Columns.Add("REQUESTCOMMENTS");
            dt.Columns.Add("ISEMERGENCY");
            dt.Columns.Add("ISEMERGENCYREASON");
            dt.Columns.Add("REQUESTTIME");
            dt.Columns.Add("ACCEPTTIME");
            dt.Columns.Add("REQUESTUSER");
            dt.Columns.Add("ACCEPTUSER");

            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");

            return dt;
        }

        /// <summary>
        /// 설비수리실적등록 중 SparePart부분 DataTable 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSparePartDatatable()
        {
            DataTable dt = new DataTable
            {
                TableName = "maintWoSparePartList"
            };

            dt.Columns.Add("WORKORDERID");
            dt.Columns.Add("SPAREPARTID");
            dt.Columns.Add("FACTORYID");
            dt.Columns.Add("QTY", typeof(int));
            dt.Columns.Add("OLDQTY", typeof(int));
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");
            dt.Columns.Add("CHILDEQUIPMENTID");

            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
            dt.Columns.Add("LASTTXNHISTKEY");
            dt.Columns.Add("LASTTXNID");
            dt.Columns.Add("LASTTXNUSER");
            dt.Columns.Add("LASTTXNTIME");
            dt.Columns.Add("LASTTXNCOMMENT");
            dt.Columns.Add("VALIDSTATE");
            dt.Columns.Add("_STATE_");

            return dt;
        }

        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayMaintWorkOrderInfo()
        {
            if (grdMaintWorkOrder.View.FocusedRowHandle < 0)
            {
                return;
            }

            DisplayMaintWorkOrderInfo(grdMaintWorkOrder.View.GetFocusedDataRow());
        }

        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayMaintWorkOrderInfo(DataRow workOrderInfo)
        {
            //해당 업무에 맞는 Enable체크 수행
            chkIsEquipmentDown.EditValueChanging -= ChkIsEquipmentDown_EditValueChanging;

            if (!workOrderInfo.IsNull("WORKORDERID"))
            {
                _workOrderName = workOrderInfo.GetString("WORKORDERNAME");

                txtWorkNo.EditValue = workOrderInfo.GetString("WORKORDERID");
                txtFactoryID.EditValue = workOrderInfo.GetString("FACTORYID");
                txtEquipmentCode.EditValue = workOrderInfo.GetString("EQUIPMENTID");
                txtEquipmentName.EditValue = workOrderInfo.GetString("EQUIPMENTNAME");
                txtRequestComment.EditValue = workOrderInfo.GetString("REQUESTCOMMENT");
                txtEmergencyContent.EditValue = workOrderInfo.GetString("EMERGENCYREASON");
                txtRequestUser.EditValue = workOrderInfo.GetString("REQUESTUSER");
                txtFactoryID.EditValue = workOrderInfo.GetString("FACTORYID");

                if (!workOrderInfo.GetString("REQUESTTIME").Equals(""))
                {
                    txtRequestTime.EditValue = Convert.ToDateTime(workOrderInfo.GetString("REQUESTTIME")).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    txtRequestTime.EditValue = null;
                }

                if (!workOrderInfo.GetString("ACCEPTTIME").Equals(""))
                {
                    txtReceiptTime.EditValue = Convert.ToDateTime(workOrderInfo.GetString("ACCEPTTIME")).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    txtReceiptTime.EditValue = null;
                }

                txtReceiptUser.EditValue = workOrderInfo.GetString("REPAIRACCEPTUSER");
                chkEmergency.Checked = workOrderInfo.GetString("ISEMERGENCY").Equals("Y");
                chkIsEquipmentDownRequest.Checked = workOrderInfo.GetString("ISEQUIPMENTDOWNREQUEST").Equals("Y");

                //요청시 비가동을 체크한 경우 자동으로 비가동이 체크된다. 아니라면 직접 체크한 결과를 반영
                chkIsEquipmentDown.Checked = workOrderInfo.GetString("ISEQUIPMENTDOWNREQUEST").Equals("Y") ? true : workOrderInfo.GetString("ISEQUIPMENTDOWN").Equals("Y");

                cboDownCode.EditValue = workOrderInfo.GetString("DOWNCODEID");
                cboDownType.EditValue = workOrderInfo.GetString("DOWNTYPEID");
                txtRepairUser.EditValue = workOrderInfo.GetString("REPAIRUSER");
                txtRepairUserCount.EditValue = workOrderInfo.GetString("REPAIRUSERCOUNT");
                cboCauseCode.EditValue = workOrderInfo.GetString("CAUSECODEID");
                cboRepairCode.EditValue = workOrderInfo.GetString("REPAIRCODEID");
                cboMaintKindCode.EditValue = workOrderInfo.GetString("MAINTKINDCODEID");
                txtRepairContent.EditValue = workOrderInfo.GetString("REPAIRCOMMENT");

                if (!workOrderInfo.GetString("STARTREPAIRTIME").Equals(""))
                {
                    deStartRepairTime.EditValue = Convert.ToDateTime(workOrderInfo.GetString("STARTREPAIRTIME"));
                }
                else
                {
                    deStartRepairTime.EditValue = null;
                }

                if (!workOrderInfo.GetString("FINISHREPAIRTIME").Equals(""))
                {
                    deEndRepairTime.EditValue = Convert.ToDateTime(workOrderInfo.GetString("FINISHREPAIRTIME"));
                }
                else
                {
                    deEndRepairTime.EditValue = null;
                }

                _workOrderStep = workOrderInfo.GetString("WORKORDERSTEPID");

                //부품사용바인딩
                SparePartSearch(workOrderInfo.GetString("WORKORDERID"));

                //파일업로드 컨트롤의 초기화 및 데이터바인딩  version은 *로 지정

                grdAttachment.UploadPath = "";
                grdAttachment.Resource = new ResourceInfo()
                {
                    Type = "MaintWorkOrder",
                    Id = workOrderInfo.GetString("WORKORDERID"),
                    Version = "*"
                };

                grdAttachment.UseCommentsColumn = true;

                grdAttachment.DataSource = this.Procedure("usp_com_selectObjectFile", new Dictionary<string, object>
                {
                    { "P_RESOURCETYPE", grdAttachment.Resource.Type },
                    { "P_RESOURCEID", grdAttachment.Resource.Id },
                    { "P_RESOURCEVERSION", grdAttachment.Resource.Version }
                });

                //수정상태라 판단하여 화면 제어
                ControlEnableProcess(_workOrderStep);
                InitRequiredControl(_workOrderStep);

                chkIsEquipmentDown.EditValueChanging += ChkIsEquipmentDown_EditValueChanging;
            }
        }

        /// <summary>
        /// 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// 현재로선 String비교만을 한다. DateTime및 기타 다른 값들에 대해선 지원하지 않음
        /// </summary>
        /// <param name="targetGrid"></param>
        /// <param name="firstColumnName"></param>
        /// <param name="firstFindValue"></param>
        /// <returns></returns>
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string firstFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 검색조건의 Site변경시 관련된 쿼리들을 변경
        /// </summary>
        private void ChangeSiteCondition()
        {
            if (popupGridSparepart != null)
            {
                popupGridSparepart.SearchQuery = new SqlQuery("GetSparePartStockListForPMByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
            }

            if (equipmentPopup != null)
            {
                equipmentPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
                ((ConditionItemSelectPopup)equipmentPopup.Conditions.GetCondition("AREANAME")).SearchQuery = new SqlQuery("GetArtxtWorkNoeaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
            }

            if (areaCondition != null)
            {
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            }

            if (equipmentSearchPopup != null)
            {
                equipmentSearchPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
                ((ConditionItemSelectPopup)equipmentSearchPopup.Conditions.GetCondition("AREANAME")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
            }

            if (areaSearchCondition != null)
            {
                areaSearchCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            }

            InitializeInsertForm();
        }

        #endregion Private Function
    }
}