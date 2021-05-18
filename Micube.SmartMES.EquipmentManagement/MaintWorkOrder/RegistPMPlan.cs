#region using
using DevExpress.XtraEditors.Repository;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 설비PM계획등록
    /// 업  무  설  명  : 설비 점검중 PM의 계획을 등록한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegistPMPlan : SmartConditionManualBaseForm
    {
        #region Local Variables      
        string _currentStatus;                                      //현재상태
        string _searchEquipmentClassID;                             //설비그룹
        string[] _clipDatas;
        int _clipIndex = 1;
        IDataObject _clipBoardData;
        bool _isGoodCopy = true;
        DataTable _maintItemTable;
        ConditionItemSelectPopup equipmentPopup;
        ConditionItemComboBox factoryBox;
        ConditionItemSelectPopup popupGridToolArea;
        #endregion

        #region 생성자

        public RegistPMPlan()
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

            InitRequiredControl();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();

            InitializeGridUpdateSend();
        }

        #region InitRequiredControl - 필수입력항목들을 체크한다.
        private void InitRequiredControl()
        {            
        }
        #endregion

        #region InitializeGrid - 제작입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
        }
        #endregion

        #region InitializeGridDetail - 제작입고목록입력을 위한 그리드를 초기화한다.
        /// <summary>        
        /// 제작입고목록입력을 위한 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridUpdateSend()
        {
            _maintItemTable = GetAllMaintItem();
            // TODO : 그리드 초기화 로직 추가
            grdPMPlan.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore | GridButtonItem.Add | GridButtonItem.Delete;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdPMPlan.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;            

            grdPMPlan.View.AddTextBoxColumn("EQUIPMENTID", 150)         //Tool 번호
                .SetIsReadOnly(true);
            InitializeEquipmentIDInDetailGrid();
            //grdPMPlan.View.AddTextBoxColumn("EQUIPMENTNAME", 250)           //Tool 코드
            //    .SetIsReadOnly(true);
            //grdPMPlan.View.AddComboBoxColumn("MAINTITEMID", 150, new SqlQuery("GetMaintItemListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MAINTITEMNAME", "MAINTITEMID")         //Tool 버전
                ;
            grdPMPlan.View.AddTextBoxColumn("MAINTITEMIDID", 150)
            .SetIsReadOnly(true)            
            ;
            grdPMPlan.View.AddTextBoxColumn("MAINTITEMID", 150)
                ;
            //grdPMPlan.View.AddTextBoxColumn("MAINTITEMNAME", 180)
            //    .SetIsReadOnly(true);
            grdPMPlan.View.AddDateEditColumn("PLANDATE", 120)                
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                ;                                                             //계획일
            grdPMPlan.View.AddTextBoxColumn("WORKORDERID", 150)
                .SetIsReadOnly(true);
            grdPMPlan.View.AddTextBoxColumn("MAINTITEMCLASSID")
                .SetIsHidden();
            grdPMPlan.View.AddTextBoxColumn("MAINTITEMCLASSNAME")
                .SetIsHidden();
            grdPMPlan.View.AddTextBoxColumn("ISNEW")
                .SetIsHidden();

            //grdInputToolUpdateSend.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdPMPlan.View.PopulateColumns();

            RepositoryItemLookUpEdit repositoryItems = new RepositoryItemLookUpEdit();
            repositoryItems.DisplayMember = "MAINTITEMNAME";
            repositoryItems.ValueMember = "MAINTITEMID";
            repositoryItems.DataSource = _maintItemTable;
            repositoryItems.ShowHeader = false;            
            repositoryItems.NullText = "";
            repositoryItems.NullValuePromptShowForEmptyValue = true;
            repositoryItems.PopulateColumns();

            grdPMPlan.View.Columns["MAINTITEMID"].ColumnEdit = repositoryItems;
        }
        #endregion

        #region InitializeEquipmentIDInDetailGrid - 설비를 조회할 팝업화면 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeEquipmentIDInDetailGrid()
        {
            equipmentPopup = grdPMPlan.View.AddSelectPopupColumn("EQUIPMENTNAME", 250, new SqlQuery("GetEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("EQUIPMENTNAME", "EQUIPMENTNAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("EQUIPMENTNAME")
                .SetRelationIds("P_PLANTID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdPMPlan.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdPMPlan.View.GetFocusedDataRow()["EQUIPMENTID"] = row["EQUIPMENTID"];
                            grdPMPlan.View.GetFocusedDataRow()["EQUIPMENTNAME"] = row["EQUIPMENTNAME"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            equipmentPopup.Conditions.AddTextBox("EQUIPMENTID")
                .SetLabel("EQUIPMENTID");
            equipmentPopup.Conditions.AddComboBox("EQUIPMENTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentType" } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("EQUIPMENTCLASSTYPE")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;
            equipmentPopup.Conditions.AddComboBox("DETAILEQUIPMENTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "DetailEquipmentType" } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("DETAILEQUIPMENTTYPE")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;

            InitializeAreaPopupInSearchCondition(equipmentPopup.Conditions);

            // 팝업 그리드
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 50)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASS", 100)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTTYPEID", 50)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTTYPE", 100)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPEID", 200)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPE", 100)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("MODEL", 100)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("SERIAL", 150)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsHidden();
        }
        #endregion        

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupColumnInDetailGrid()
        {
            ConditionItemSelectPopup areaCondition = grdPMPlan.View.AddSelectPopupColumn("RECEIPTAREA", 150, new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
                // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
                .SetPopupLayout("RECEIPTAREA", PopupButtonStyles.Ok_Cancel, true, false)
                // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
                .SetPopupResultCount(1)
                //필수값설정
                .SetValidationIsRequired()
                // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)
                .SetPopupResultMapping("RECEIPTAREA", "AREANAME")
                // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("AREANAME")
                .SetRelationIds("P_PLANTID")
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    int i = 0;
                    int currentIndex = grdPMPlan.View.GetFocusedDataSourceRowIndex();

                    foreach (DataRow row in selectedRows)
                    {
                        if (i == 0)
                        {
                            grdPMPlan.View.GetFocusedDataRow()["RECEIPTAREAID"] = row["AREAID"];
                            grdPMPlan.View.GetFocusedDataRow()["RECEIPTAREA"] = row["AREANAME"];
                        }
                        i++;
                    }
                })
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;

            // 팝업에서 사용할 조회조건 항목 추가
            areaCondition.Conditions.AddTextBox("AREANAME");

            ConditionItemComboBox factoryCondition = areaCondition.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "FACTORYNAME", "FACTORYID");
            factoryCondition.SetRelationIds("P_PLANTID");




            // 팝업 그리드 설정
            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion        
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdPMPlan.View.CellValueChanged += grdPMPlan_CellValueChanged;
            //grdEquipmentMaint.View.CellValueChanged += grdEquipmentMaint_CellValueChanged;
            //grdEquipmentMaint.View.FocusedRowChanged += grdEquipmentMaint_FocusedRowChanged;

            btnSave.Click += BtnSave_Click;
            btnCreateSchedule.Click += BtnCreateSchedule_Click;
            btnPublishMaintOrder.Click += BtnPublishMaintOrder_Click;
            btnCancelMaintOrder.Click += BtnCancelMaintOrder_Click;
            btnAutoCreate.Click += BtnAutoCreate_Click;

            grdPMPlan.View.ShowingEditor += View_ShowingEditor;
            grdPMPlan.View.ShownEditor += grdPMPlan_ShownEditor;

            Shown += RegistPMPlan_Shown;
        }

        #region BtnAutoCreate_Click - 자동입력이벤트
        private void BtnAutoCreate_Click(object sender, EventArgs e)
        {
            Popup.PlanWorkOrderUploader uploader = new Popup.PlanWorkOrderUploader(Conditions.GetValue("P_PLANTID").ToString());
            if (Conditions.GetValue("P_PLANDATE") != null)
            {
                Dictionary<string, object> planDateCondition = (Dictionary<string, object>)Conditions.GetValue("P_PLANDATE");
                uploader.StartDate = Convert.ToDateTime(planDateCondition["P_PLANDATE_PERIODFR"]);
                uploader.EndDate = Convert.ToDateTime(planDateCondition["P_PLANDATE_PERIODTO"]);
            }
            uploader.EquipmentClassID = Convert.ToString(Conditions.GetValue("EQUIPMENTCLASSID"));
            uploader.EquipmentID = Convert.ToString(Conditions.GetValue("EQUIPMENTID"));
            uploader.FactoryID = Convert.ToString(Conditions.GetValue("FACTORYID"));
            uploader.MaintType = Convert.ToString(Conditions.GetValue("MAINTTYPE"));

            uploader.SearchHandler += Research;
            uploader.ShowDialog();
        }
        #endregion

        #region RegistPMPlan_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RegistPMPlan_Shown(object sender, EventArgs e)
        {
            ((SmartComboBox)Conditions.GetControl("MAINTTYPE")).EditValueChanged += MaintTypeCondition_EditValueChanged;

            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;

            //Button사이즈 조절
            if (pnlToolbar.Controls["layoutToolbar"] != null)
            {
                if (pnlToolbar.Controls["layoutToolbar"].Controls["AutoScheduler"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["AutoScheduler"].Width = GetButtonWidth(pnlToolbar.Controls["layoutToolbar"].Controls["AutoScheduler"].Text);
                if (pnlToolbar.Controls["layoutToolbar"].Controls["CreateSchedule"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["CreateSchedule"].Width = GetButtonWidth(pnlToolbar.Controls["layoutToolbar"].Controls["CreateSchedule"].Text);
                if (pnlToolbar.Controls["layoutToolbar"].Controls["PublishPMOrder"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["PublishPMOrder"].Width = GetButtonWidth(pnlToolbar.Controls["layoutToolbar"].Controls["PublishPMOrder"].Text);
                if (pnlToolbar.Controls["layoutToolbar"].Controls["CancelPMOrder"] != null)
                    pnlToolbar.Controls["layoutToolbar"].Controls["CancelPMOrder"].Width = GetButtonWidth(pnlToolbar.Controls["layoutToolbar"].Controls["CancelPMOrder"].Text);
            }
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region MaintTypeCondition_EditValueChanged - 점검종류 변경이벤트
        private void MaintTypeCondition_EditValueChanged(object sender, EventArgs e)
        {
            equipmentPopup.SearchQuery = new SqlQuery("GetEquipmentListByEqp"
                , "10001"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                , $"PLANTID={Conditions.GetValue("P_PLANTID")}"
                , $"MAINTTYPE={((Micube.Framework.SmartControls.SmartComboBox)Conditions.GetControl("MAINTTYPE")).EditValue}"
                , $"CURRENTLOGINID={UserInfo.Current.Id}"
                , "ISMOD=Y"
                );
        }
        #endregion

        #region grdPMPlan_ShownEditor - 동적바인딩 지원이벤트
        private void grdPMPlan_ShownEditor(object sender, EventArgs e)
        {
            if (grdPMPlan.View.FocusedColumn.FieldName == "MAINTITEMID")
            {
                string equipmentID = grdPMPlan.View.GetFocusedDataRow().GetString("EQUIPMENTID");

                if (grdPMPlan.View.ActiveEditor != null)
                {
                    ((DevExpress.XtraEditors.LookUpEdit)grdPMPlan.View.ActiveEditor).Properties.DataSource = GetMaintItem(equipmentID, ((Micube.Framework.SmartControls.SmartComboBox)Conditions.GetControl("MAINTTYPE")).EditValue.ToString());
                }
            }
            else if(grdPMPlan.View.FocusedColumn.FieldName.Equals("EQUIPMENTNAME"))
            {
                if(grdPMPlan.View.ActiveEditor != null)
                {
                    if(grdPMPlan.View.ActiveEditor is DevExpress.XtraEditors.ButtonEdit)
                    {
                        ((DevExpress.XtraEditors.ButtonEdit)grdPMPlan.View.ActiveEditor).ButtonClick += RegistPMPlan_ButtonClick;
                    }
                }
            }
        }
        #endregion

        #region RegistPMPlan_ButtonClick - 그리드내 설비검색취소버튼 클릭이벤트
        private void RegistPMPlan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                grdPMPlan.View.SetRowCellValue(grdPMPlan.View.FocusedRowHandle, "EQUIPMENTID", "");
            }
        }
        #endregion

        #region View_ShowingEditor - 그리드의 수정제어
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!grdPMPlan.View.GetFocusedDataRow().GetString("ISNEW").Equals(""))
            {
                e.Cancel = true;
            }            
        }
        #endregion

        #region BtnCancelMaintOrder_Click - 보전Order발행취소이벤트
        private void BtnCancelMaintOrder_Click(object sender, EventArgs e)
        {
            CancelPublishWorkOrder();
        }
        #endregion

        #region BtnPublishMaintOrder_Click - 보전Order발행이벤트
        private void BtnPublishMaintOrder_Click(object sender, EventArgs e)
        {
            PublishWorkOrder();
        }
        #endregion

        #region BtnCreateSchedule_Click - 스케쥴생성 클릭이벤트
        private void BtnCreateSchedule_Click(object sender, EventArgs e)
        {
            CreateSchedule();
        }
        #endregion

        #region BtnSave_click - 저장이벤트
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region grdEquipmentMaint_CellValueChanged : 설비점검항목의 목록에서 계획시작일과 종료일을 설정시 사용
        private void grdEquipmentMaint_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.FieldName == "MAINTITEMID")
            //{
            //    grdEquipmentMaint.View.CellValueChanged -= grdEquipmentMaint_CellValueChanged;

            //    DataRow row = grdEquipmentMaint.View.GetFocusedDataRow();

            //    if (row["RULEDATE"].ToString().Equals(""))
            //    {
            //        grdEquipmentMaint.View.CellValueChanged += grdEquipmentMaint_CellValueChanged;
            //        return;
            //    }
            //    DateTime dateBudget = Convert.ToDateTime(row["RULEDATE"].ToString());
            //    grdEquipmentMaint.View.SetFocusedRowCellValue("RULEDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
            //    grdEquipmentMaint.View.CellValueChanged += grdEquipmentMaint_CellValueChanged;
            //}
            //else if (e.Column.FieldName == "ENDRULEDATE")
            //{
            //    grdEquipmentMaint.View.CellValueChanged -= grdEquipmentMaint_CellValueChanged;

            //    DataRow row = grdEquipmentMaint.View.GetFocusedDataRow();

            //    if (row["ENDRULEDATE"].ToString().Equals(""))
            //    {
            //        grdEquipmentMaint.View.CellValueChanged += grdEquipmentMaint_CellValueChanged;
            //        return;
            //    }
            //    DateTime dateBudget = Convert.ToDateTime(row["ENDRULEDATE"].ToString());
            //    grdEquipmentMaint.View.SetFocusedRowCellValue("ENDRULEDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
            //    grdEquipmentMaint.View.CellValueChanged += grdEquipmentMaint_CellValueChanged;
            //}
        }
        #endregion

        #region grdPMPlan_CellValueChanged : PM계획 조회/수정/삭제 그리드의 CellValueChanged 이벤트
        private void grdPMPlan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PLANDATE")
            {
                grdPMPlan.View.CellValueChanged -= grdPMPlan_CellValueChanged;

                DataRow row = grdPMPlan.View.GetFocusedDataRow();

                if (row["PLANDATE"].ToString().Equals(""))
                {
                    grdPMPlan.View.CellValueChanged += grdPMPlan_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["PLANDATE"].ToString());
                grdPMPlan.View.SetFocusedRowCellValue("PLANDATE", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdPMPlan.View.CellValueChanged += grdPMPlan_CellValueChanged;
            }
            else if (e.Column.FieldName == "EQUIPMENTNAME")
            {
                grdPMPlan.View.CellValueChanged -= grdPMPlan_CellValueChanged;

                DataRow row = grdPMPlan.View.GetDataRow(e.RowHandle);

                if (row["EQUIPMENTNAME"].ToString().Equals(""))
                {
                    grdPMPlan.View.CellValueChanged += grdPMPlan_CellValueChanged;
                    return;
                }
                GetSingleEquipment(row.GetString("EQUIPMENTNAME"), e.RowHandle);
                grdPMPlan.View.CellValueChanged += grdPMPlan_CellValueChanged;
            }            
            else if (e.Column.FieldName == "MAINTITEMID")
            {
                grdPMPlan.View.CellValueChanged -= grdPMPlan_CellValueChanged;

                grdPMPlan.View.SetRowCellValue(e.RowHandle, "MAINTITEMIDID", e.Value);
                
                grdPMPlan.View.CellValueChanged += grdPMPlan_CellValueChanged;
            }
        }
        #endregion
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("AutoScheduler"))
                BtnAutoCreate_Click(sender, e);
            else if (btn.Name.ToString().Equals("CreateSchedule"))
                BtnCreateSchedule_Click(sender, e);
            else if (btn.Name.ToString().Equals("Save"))
                BtnSave_Click(sender, e);
            else if (btn.Name.ToString().Equals("PublishPMOrder"))
                BtnPublishMaintOrder_Click(sender, e);
            else if (btn.Name.ToString().Equals("CancelPMOrder"))
                BtnCancelMaintOrder_Click(sender, e);
        }

        #endregion

        #region 검색
        #region OnSearchAsync - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            #region 이전 소스
            //DataTable toolRepairResult = SqlExecuter.Query("GetInspectionEquipmentListByEqp", "10001", values);

            //grdEquipmentMaint.DataSource = toolRepairResult;

            //if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            //{
            //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //    grdPMPlan.DataSource = null;
            //}
            //else
            //{
            //    grdEquipmentMaint.View.FocusedRowHandle = 0;
            //    DisplayPMPlanList();
            //}
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetPMPlanListByEqp", "10001", values);

            grdPMPlan.DataSource = searchResult;

            _currentStatus = "modified";
        }
        #endregion

        #region Research - 재검색(데이터 입력후와 같은 경우에 사용)
        private void Research(string subMaintType)
        {
            // TODO : 조회 SP 변경
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (!values["MAINTTYPE"].ToString().Equals(subMaintType))
                ShowMessage(MessageBoxButtons.OK, "ValidateSearchMaintTypeForMaintWorkOrder", "");
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetPMPlanListByEqp", "10001", values);

            grdPMPlan.DataSource = searchResult;

            _currentStatus = "modified";
        }
        #endregion

        #region SearchPMPlanList - PM계획리스트를 조회한다.
        /// <summary>
        /// SearchPMPlanList - PM계획리스트를 조회한다.
        /// </summary>
        private void SearchPMPlanList(string equipmentID, string maintItemID)
        {
            //DataRow currentRow = grdEquipmentMaint.View.GetFocusedDataRow();
            //if (currentRow != null)
            //{
            //    Dictionary<string, object> values = new Dictionary<string, object>();

            //    #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            //    values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //    values.Add("EQUIPMENTID", equipmentID);
            //    values.Add("MAINTITEMID", maintItemID);
            //    #endregion

            //    DataTable searchResult = SqlExecuter.Query("GetPMPlanListByEqp", "10001", values);

            //    grdPMPlan.DataSource = searchResult;
            //}
            //_currentStatus = "modified";
        }
        #endregion

        #region 조회조건 제어
        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializeConditionPlant();
            InitializeConditionFactory();
            InitializeConditionEquipmentClassTypePopup();
            InitializeConditionEquipmentIDPopup();
            InitializeConditionMaintType();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            string sPeriodname = "P_PLANDATE";
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add(sPeriodname, "CUSTOM");
            values.Add(sPeriodname + "_PERIODFR", DateTime.Now.ToString("yyyy-MM-dd"));
            values.Add(sPeriodname + "_PERIODTO", DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"));

            Conditions.GetControl<SmartPeriodEdit>(sPeriodname).SetValue(values);
        }

        #region InitializeConditionPlant : Site를 설정한다.
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #region InitializeConditionMaintType : 보전구분을 설정한다.
        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionMaintType()
        {
            var planttxtbox = Conditions.AddComboBox("MAINTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=MaintType"), "CODENAME", "CODEID")
               .SetLabel("MAINTTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetValidationIsRequired()
               .SetDefault("PM")
            ;
        }
        #endregion

        #region InitializeConditionFactory : 공장을 조회한다.
        /// <summary>
        /// 제작업체 설정
        /// </summary>
        private void InitializeConditionFactory()
        {
            var planttxtbox = Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
               .SetLabel("FACTORY")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(2.1)
               .SetRelationIds("P_PLANTID")
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionEquipmentClassTypePopup : 설비그룹조회
        /// <summary>
        /// 설비그룹조회설정
        /// </summary>
        private void InitializeConditionEquipmentClassTypePopup()
        {
            ConditionItemSelectPopup eqpPopup = Conditions.AddSelectPopup("EQUIPMENTCLASSID", new SqlQuery("GetEquipmentClassListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
            .SetPopupLayout("EQUIPMENTCLASS", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("EQUIPMENTCLASSNAME", "EQUIPMENTCLASSNAME")
            .SetLabel("EQUIPMENTCLASS")
            .SetPopupResultCount(1)
            .SetPosition(2.2)
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        _searchEquipmentClassID = row["EQUIPMENTCLASSID"].ToString();                        
                    }
                    i++;
                }
            })
            ;

            eqpPopup.ValueFieldName = "EQUIPMENTCLASSID";
            eqpPopup.DisplayFieldName = "EQUIPMENTCLASSNAME";

            eqpPopup.SetRelationIds("P_PLANTID");

            // 팝업 조회조건
            eqpPopup.Conditions.AddTextBox("EQUIPMENTCLASSNAME")
                .SetLabel("EQUIPMENTCLASSNAME");
            eqpPopup.Conditions.AddComboBox("EQUIPMENTCLASSTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { {"LANGUAGETYPE", UserInfo.Current.LanguageType}, { "CODECLASSID", "EquipmentClassType" } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("EQUIPMENTCLASSTYPE")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;
            eqpPopup.Conditions.AddComboBox("EQUIPMENTCLASSHIERARCHY", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentClassHierarchy" } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("EQUIPMENTCLASSHIERARCHY")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;

            // 팝업 그리드
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 120)
                .SetIsHidden();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSTYPEID", 50)
                .SetIsHidden();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSTYPE", 100)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSHIERARCHYID", 200)
                .SetIsHidden();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSHIERARCHY", 100)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeConditionEquipmentIDPopup : 설비조회
        /// <summary>
        /// 설비그룹조회설정
        /// </summary>
        private void InitializeConditionEquipmentIDPopup()
        {
            ConditionItemSelectPopup eqpPopup = Conditions.AddSelectPopup("EQUIPMENTID", new SqlQuery("GetEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("EQUIPMENTID", "EQUIPMENTID")
            .SetLabel("EQUIPMENT")
            .SetPopupResultCount(1)
            .SetPosition(2.3)
            ;

            eqpPopup.ValueFieldName = "EQUIPMENTID";
            eqpPopup.DisplayFieldName = "EQUIPMENTNAME";

            eqpPopup.SetRelationIds("P_PLANTID");

            // 팝업 조회조건
            eqpPopup.Conditions.AddTextBox("EQUIPMENTID")
                .SetLabel("EQUIPMENTID");
            eqpPopup.Conditions.AddComboBox("EQUIPMENTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentType" } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("EQUIPMENTCLASSTYPE")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;
            eqpPopup.Conditions.AddComboBox("DETAILEQUIPMENTTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "DetailEquipmentType" } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("DETAILEQUIPMENTTYPE")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;
            

            InitializeAreaPopupInSearchCondition(eqpPopup.Conditions);

            // 팝업 그리드
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120)
                .SetIsHidden();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 50)
                .SetIsHidden();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASS", 100)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTTYPEID", 50)
                .SetIsHidden();
            eqpPopup.GridColumns.AddTextBoxColumn("EQUIPMENTTYPE", 100)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPEID", 200)
                .SetIsHidden();
            eqpPopup.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPE", 100)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("MODEL", 100)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("SERIAL", 150)
                .SetIsReadOnly();
            eqpPopup.GridColumns.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();
        }
        #endregion

        #region InitializeAreaPopupColumnInDetailGrid - 작업장을 팝업검색할 필드 설정
        /// <summary>
        /// grid ProcesssegmentidLis
        /// </summary>
        private void InitializeAreaPopupInSearchCondition(ConditionCollection conditions)
        {
            popupGridToolArea = conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y"))
                .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("AREAID", "AREAID")
                .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                // 그리드의 남은 영역을 채울 컬럼 설정                
                .SetPopupAutoFillColumns("AREAID")
            //.SetPopupValidationCustom(ValidationParentAreaIdPopup);
            ;
            popupGridToolArea.ValueFieldName = "AREAID";
            popupGridToolArea.DisplayFieldName = "AREANAME";

            // 팝업에서 사용할 조회조건 항목 추가
            popupGridToolArea.Conditions.AddTextBox("AREANAME");

            //factoryBox = popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID");
            //factoryBox.SetRelationIds("P_PLANTID");

            // 팝업 그리드 설정
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion        
        #endregion
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }


        #region ValidateContent - 내용 Validation
        private bool ValidateContent(string saveType, out string messageCode)
        {
            messageCode = "";
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            

            DataTable inputTable = grdPMPlan.View.GetCheckedRows();

            if (saveType.Equals("Schedule"))
            {
                foreach (DataRow inputRow in inputTable.Rows)
                {
                    if (!ValidateCellInGrid(inputRow, new string[] { "EQUIPMENTID", "MAINTITEMID", "PLANDATE" }))
                    {
                        messageCode = "ToolDetailValidation";
                        return false;
                    }
                }
            }
            else if (saveType.Equals("Order"))
            {
                foreach (DataRow inputRow in inputTable.Rows)
                {
                    if (!ValidateCellInGrid(inputRow, new string[] { "EQUIPMENTID", "MAINTITEMID", "PLANDATE" }))
                    {
                        messageCode = "ToolDetailValidation";
                        return false;
                    }
                    if (!ValidateCellInGrid(inputRow, new string[] { "MAINTITEMCLASSID" }))
                    {
                        messageCode = "ValidatePlanRowMaintItemClass";
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스의 값비교
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형텍스트박스의 Validation, 기준숫자보다 높은지 검증
        private bool ValidateNumericBox(SmartTextBox originBox, int ruleValue)
        {
            //값이 없으면 안된다.
            if (originBox.EditValue == null)
                return false;

            int resultValue = 0;

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(originBox.EditValue.ToString(), out resultValue))
                if (resultValue <= ruleValue)
                    return false;

            //모두 통과했으므로 Validation Check완료
            return true;
        }
        #endregion

        #region ValidateEditValue - 특정 값에 대한 Validation
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드내의 특정 컬럼에 대한 Validation
        private bool ValidateCellInGrid(DataRow currentRow, string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (currentRow[columnName] == null)
                    return false;
                if (currentRow[columnName].ToString() == "")
                    return false;
            }

            return true;
        }
        #endregion

        #region SetRequiredValidationControl - 필수입력항목설정
        private void SetRequiredValidationControl(Control requiredControl)
        {
            requiredControl.ForeColor = Color.Red;
        }
        #endregion

        #endregion

        #region Private Function
        #region GetAllMaintItem : maintItem의 전체를 조회
        private DataTable GetAllMaintItem()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values = Commons.CommonFunction.ConvertParameter(values);
            return SqlExecuter.Query("GetMaintItemListByEqp", "10001", values);
        }
        #endregion

        #region GetMaintItem : 특정 설비와 연관된 점검항목을 조회
        private DataTable GetMaintItem(string equipmentID, string maintType)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("EQUIPMENTID", equipmentID);
            values.Add("MAINTITEMCLASSTYPE", maintType);
            values = Commons.CommonFunction.ConvertParameter(values);
            return SqlExecuter.Query("GetMaintItemListByEqp", "10001", values);
        }
        #endregion

        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");
            }
            catch (Exception err)
            {
                ShowError(err);
            }
        }
        #endregion

        #region controlEnableProcess : 입력/수정/삭제를 위한 화면내 컨트롤들의 Enable 제어
        /// <summary>
        /// 진행상태값에 따른 입력 항목 lock 처리
        /// </summary>
        /// <param name="blProcess"></param>
        private void ControlEnableProcess(string currentStatus)
        {
            if (currentStatus == "added") //초기화 버튼을 클릭한 경우
            {
                
            }
            else if (currentStatus == "modified") //
            {
                
            }
            else
            {
                
            }
        }
        #endregion

        #region LoadData - 팝업창에서 호출하는 메소드
        /// <summary>
        /// 팝업창에서 호출하는 메소드
        /// </summary>
        private void LoadData(DataTable table)
        {
            
        }
        #endregion        

        #region CreateSaveDatatable : PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "preventiveMaintPlanList";
            //===================================================================================            
            dt.Columns.Add("MAINTITEMID");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("PLANDATE");
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

            if (useState)
                dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region CreateOrderDatatable : PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateOrderDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "maintWorkOrderList";
            //===================================================================================            
            dt.Columns.Add("WORKORDERID");
            dt.Columns.Add("WORKORDERNAME");
            dt.Columns.Add("MAINTITEMID");
            dt.Columns.Add("MAINTITEMNAME");
            dt.Columns.Add("MAINTTYPEID");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("PLANDATE");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("EQUIPMENTNAME");
            dt.Columns.Add("MAINTITEMCLASSID");
            dt.Columns.Add("MAINTITEMCLASSNAME");
            dt.Columns.Add("MAINTKINDCODE");

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

            if (useState)
                dt.Columns.Add("_STATE_");

            return dt;
        }
        #endregion

        #region DisplayPMPlanList : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayPMPlanList()
        {
            //포커스 행 체크 
            //if (grdEquipmentMaint.View.FocusedRowHandle < 0) return;

            //DisplayToolRepairSendInfoDetail(grdEquipmentMaint.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void ViewSavedData(string sendDate, string sendSequence)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("SENDDATE", Convert.ToDateTime(sendDate).ToString("yyyy-MM-dd"));
            values.Add("SENDSEQUENCE", Int32.Parse(sendSequence));
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetToolUpdateSendListByTool", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                DisplayToolRepairSendInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplayToolRepairSendInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayToolRepairSendInfoDetail(DataRow currentRow)
        {
            //이벤트를 제거
            //grdInputToolUpdateSend.View.CheckStateChanged -= grdInputToolUpdateSend_CheckStateChanged;

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");
            
            //그리드 데이터 바인딩
            SearchPMPlanList(currentRow.GetString("EQUIPMENTID"), currentRow.GetString("MAINTITEMID"));

            _currentStatus = "modified";
            //각 순서에 따라 바인딩

            //이벤트를 추가
            //grdInputToolUpdateSend.View.CheckStateChanged += grdInputToolUpdateSend_CheckStateChanged;
        }
        #endregion

        #region GetRowHandleInGrid : 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// <summary>
        /// 특정아이디의 값을 바탕으로 해당 내용을 그리드에서 찾은후 결과값의 그리드내의 인덱스를 반환한다.
        /// 현재로선 String비교만을 한다. DateTime및 기타 다른 값들에 대해선 지원하지 않음
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="findValue"></param>
        /// <returns></returns>
        private int GetRowHandleInGrid(SmartBandedGrid targetGrid, string firstColumnName, string secondColumnName, string firstFindValue, string secondFindValue)
        {
            for (int i = 0; i < targetGrid.View.RowCount; i++)
            {
                if (firstFindValue.Equals(targetGrid.View.GetDataRow(i)[firstColumnName].ToString()) && secondFindValue.Equals(targetGrid.View.GetDataRow(i)[secondColumnName].ToString()))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region CreateSchedule : CreateSchedule버튼의 기능을 수행
        private void CreateSchedule()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();
            //저장 로직
            try
            {
                if (Conditions.Validation())
                {
                    Popup.PlanWorkOrderCreatorPopup planPopup = new Popup.PlanWorkOrderCreatorPopup(Conditions.GetValue("P_PLANTID").ToString());
                    planPopup.MaintTypeID = Conditions.GetValue("MAINTTYPE").ToString();
                    planPopup.SearchHandler += Research;
                    planPopup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
            }
        }
        #endregion

        #region SaveData : 저장구문을 수행
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;
                btnPublishMaintOrder.Enabled = false;
                btnCancelMaintOrder.Enabled = false;
                btnCreateSchedule.Enabled = false;
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent("Schedule", out messageCode))
                {
                    DataSet maintItemSet = new DataSet();
                    DataTable maintItemTable = CreateSaveDatatable(true);
                    
                    DataTable planTable = grdPMPlan.GetChangedRows();

                    foreach (DataRow planRow in planTable.Rows)
                    {
                        DataRow maintItemRow = maintItemTable.NewRow();

                        maintItemRow["MAINTITEMID"] = planRow.GetString("MAINTITEMID");
                        maintItemRow["EQUIPMENTID"] = planRow.GetString("EQUIPMENTID");
                        maintItemRow["PLANDATE"] = Convert.ToDateTime(planRow.GetString("PLANDATE")).ToString("yyyy-MM-dd");
                        maintItemRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        maintItemRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                        maintItemRow["CREATOR"] = UserInfo.Current.Id;
                        maintItemRow["MODIFIER"] = UserInfo.Current.Id;

                        if (planRow.GetString("_STATE_").Equals("deleted"))
                        {
                            maintItemRow["_STATE_"] = "deleted";
                            maintItemRow["VALIDSTATE"] = "Invalid";
                        }
                        else
                        {
                            //삭제가 아니라면 무조건 생성으로 본다. (생성역시 아이디가 존재한다면 자동으로 수정으로 수행한다.)
                            maintItemRow["_STATE_"] = "added";
                            maintItemRow["VALIDSTATE"] = "Valid";
                        }

                        maintItemTable.Rows.Add(maintItemRow);
                    }

                    maintItemSet.Tables.Add(maintItemTable);


                    ExecuteRule<DataTable>("PreventiveMaintPlan", maintItemSet);

                    ControlEnableProcess("modified");

                    ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    Research(Conditions.GetValue("MAINTTYPE").ToString());
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
                this.CloseWaitArea();
                btnSave.Enabled = true;
                btnPublishMaintOrder.Enabled = true;
                btnCancelMaintOrder.Enabled = true;
                btnCreateSchedule.Enabled = true;
            }
        }
        #endregion

        #region PublishWorkOrder : 설비보전Order를 발행
        private void PublishWorkOrder()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;
                btnPublishMaintOrder.Enabled = false;
                btnCancelMaintOrder.Enabled = false;
                btnCreateSchedule.Enabled = false;

                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent("Order", out messageCode))
                {
                    DataSet maintOrderSet = new DataSet();
                    DataTable maintOrderTable = CreateOrderDatatable(true);
                    
                    DataTable savedOrderTable = grdPMPlan.View.GetCheckedRows();

                    foreach (DataRow savedOrderRow in savedOrderTable.Rows)
                    {
                        DataRow maintOrderRow = maintOrderTable.NewRow();

                        maintOrderRow["MAINTITEMID"] = savedOrderRow.GetString("MAINTITEMID");
                        maintOrderRow["EQUIPMENTID"] = savedOrderRow.GetString("EQUIPMENTID");
                        maintOrderRow["EQUIPMENTNAME"] = savedOrderRow.GetString("EQUIPMENTNAME");
                        maintOrderRow["MAINTITEMCLASSID"] = savedOrderRow.GetString("MAINTITEMCLASSID");
                        maintOrderRow["MAINTITEMCLASSNAME"] = savedOrderRow.GetString("MAINTITEMCLASSNAME");
                        //발행시에 WorkOrderID는 공값으로 전달
                        maintOrderRow["WORKORDERID"] = "";
                        maintOrderRow["WORKORDERNAME"] = "";
                        maintOrderRow["PLANDATE"] = Convert.ToDateTime(savedOrderRow.GetString("PLANDATE")).ToString("yyyy-MM-dd");
                        maintOrderRow["MAINTITEMNAME"] = savedOrderRow.GetString("MAINTITEMNAME");
                        maintOrderRow["MAINTTYPEID"] = Conditions.GetValue("MAINTTYPE");
                        maintOrderRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        maintOrderRow["PLANTID"] = Conditions.GetValue("P_PLANTID");
                        maintOrderRow["MAINTKINDCODE"] = "Plan";
                        maintOrderRow["CREATOR"] = UserInfo.Current.Id;
                        maintOrderRow["MODIFIER"] = UserInfo.Current.Id;
                        maintOrderRow["_STATE_"] = "added";
                        maintOrderRow["VALIDSTATE"] = "Valid";

                        maintOrderTable.Rows.Add(maintOrderRow);
                    }

                    maintOrderSet.Tables.Add(maintOrderTable);

                    ExecuteRule<DataTable>("MaintWorkOrder", maintOrderSet);

                    ControlEnableProcess("modified");

                    ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    Research(Conditions.GetValue("MAINTTYPE").ToString());
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
                this.CloseWaitArea();
                btnSave.Enabled = true;
                btnPublishMaintOrder.Enabled = true;
                btnCancelMaintOrder.Enabled = true;
                btnCreateSchedule.Enabled = true;
            }
        }
        #endregion

        #region CancelPublishWorkOrder : 설비보전Order를 취소
        private void CancelPublishWorkOrder()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnSave.Enabled = false;
                btnPublishMaintOrder.Enabled = false;
                btnCancelMaintOrder.Enabled = false;
                btnCreateSchedule.Enabled = false;

                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent("Order", out messageCode))
                {
                    DataSet maintOrderSet = new DataSet();
                    DataTable maintOrderTable = CreateOrderDatatable(true);

                    DataTable savedOrderTable = grdPMPlan.View.GetCheckedRows();

                    foreach (DataRow savedOrderRow in savedOrderTable.Rows)
                    {
                        DataRow maintOrderRow = maintOrderTable.NewRow();

                        maintOrderRow["MAINTITEMID"] = savedOrderRow.GetString("MAINTITEMID");
                        maintOrderRow["EQUIPMENTID"] = savedOrderRow.GetString("EQUIPMENTID");
                        //발행시에 WorkOrderID는 공값으로 전달
                        maintOrderRow["WORKORDERID"] = "";
                        maintOrderRow["WORKORDERNAME"] = "";
                        maintOrderRow["PLANDATE"] = Convert.ToDateTime(savedOrderRow.GetString("PLANDATE")).ToString("yyyy-MM-dd");
                        maintOrderRow["MAINTITEMNAME"] = savedOrderRow.GetString("MAINTITEMNAME");
                        maintOrderRow["MAINTTYPEID"] = Conditions.GetValue("MAINTTYPE");
                        maintOrderRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        maintOrderRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                        maintOrderRow["CREATOR"] = UserInfo.Current.Id;
                        maintOrderRow["MODIFIER"] = UserInfo.Current.Id;
                        maintOrderRow["_STATE_"] = "deleted";
                        maintOrderRow["VALIDSTATE"] = "Invalid";

                        maintOrderTable.Rows.Add(maintOrderRow);
                    }

                    maintOrderSet.Tables.Add(maintOrderTable);

                    ExecuteRule<DataTable>("MaintWorkOrder", maintOrderSet);

                    ControlEnableProcess("modified");

                    ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    Research(Conditions.GetValue("MAINTTYPE").ToString());
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, "ToolDetailValidation", "");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                this.CloseWaitArea();
                btnSave.Enabled = true;
                btnPublishMaintOrder.Enabled = true;
                btnCancelMaintOrder.Enabled = true;
                btnCreateSchedule.Enabled = true;
            }
        }
        #endregion

        #region DeleteLotGridRow : 삭제버튼을 통해 선택한 행을 삭제한다.
        private void DeleteLotGridRow()
        {
            grdPMPlan.View.DeleteCheckedRows();
        }
        #endregion

        #region IsCurrentProcess : 현재 화면 상태에 따라 버튼 동작유무를 결정한다.
        /// <summary>
        /// 현재 화면 상태에 따라 버튼 동작유무를 결정한다.
        /// </summary>
        /// <returns></returns>
        private bool IsCurrentProcess()
        {
            //if (_currentStatus == "added")
            //    return true;
            //return false;
            return true;
        }
        #endregion

        #region GetDateDurationCollection : 시작일과 종료일 내에서 점검주기단위에 따른 주기별 날짜를 계산하여 반환한다.
        private List<DateTime> GetDateDurationCollection(string startDateStr, string endDateStr, int duration, string durationUnit)
        {
            List<DateTime> resultDates = new List<DateTime>();

            DateTime startDate = Convert.ToDateTime(startDateStr);
            DateTime endDate = Convert.ToDateTime(endDateStr);

            for(; DateTime.Compare(startDate, endDate) <= 0 ; startDate = GetNextDateTime(startDate, duration, durationUnit))            
                resultDates.Add(startDate);

            return resultDates;
        }

        private DateTime GetNextDateTime(DateTime ruleDate, int duration, string durationUnit)
        {
            //ToDo : 요구사항에 결정된 주기에 맞추어 개량할 필요가 있음
            switch (durationUnit)
            {
                case "Quarter":
                    return ruleDate.AddMonths(duration * 3);
                case "Month":
                    return ruleDate.AddMonths(duration);
                case "Week":
                    return ruleDate.AddDays(duration * 7);
                case "Day":
                    return ruleDate.AddDays(duration);
                default:
                    return ruleDate.AddDays(duration);
            }
        }
        #endregion

        #region GetSingleEquipment : 사용자가 입력한 설비명으로 검색 (클립보드의 데이터를 이용)
        /// <summary>
        /// 사용자가 입력한 설비명으로 검색 (클립보드의 데이터를 이용)
        /// </summary>
        private void GetSingleEquipment(string equipmentName, int rowHandle)
        {
            if (_clipDatas == null)
            {
                _clipBoardData = Clipboard.GetDataObject();
                if (_clipBoardData != null)
                {
                    string[] clipFormats = _clipBoardData.GetFormats(true);


                    foreach (string format in clipFormats)
                    {
                        if (format.Equals("System.String"))
                        {
                            string tempStr = Convert.ToString(_clipBoardData.GetData(format));
                            tempStr = tempStr.Replace("\r\n", "\n");
                            if (tempStr.Substring(tempStr.Length - 1).Equals("\n"))
                                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                            _clipDatas = tempStr.Split('\n');
                        }
                    }
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("EQUIPMENTID", equipmentName);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            values.Add("ISMOD", "Y");
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetEquipmentListByEqp", "10001", values);

            if (savedResult.Rows.Count.Equals(1))
            {
                grdPMPlan.View.SetRowCellValue(rowHandle, "EQUIPMENTID", savedResult.Rows[0].GetString("EQUIPMENTID"));
                grdPMPlan.View.SetRowCellValue(rowHandle, "EQUIPMENTNAME", savedResult.Rows[0].GetString("EQUIPMENTNAME"));

                if (grdPMPlan.View.ActiveEditor != null)
                {
                    DataTable templateTable = GetMaintItem(savedResult.Rows[0].GetString("EQUIPMENTID"), ((Micube.Framework.SmartControls.SmartComboBox)Conditions.GetControl("MAINTTYPE")).EditValue.ToString());

                    ((DevExpress.XtraEditors.LookUpEdit)grdPMPlan.View.ActiveEditor).Properties.DataSource = templateTable;

                    if (templateTable.Rows.Count == 1)
                        ((DevExpress.XtraEditors.LookUpEdit)grdPMPlan.View.ActiveEditor).ItemIndex = 0;
                }
            }
            else
            {
                grdPMPlan.View.SetRowCellValue(rowHandle, "EQUIPMENTNAME", "");
                grdPMPlan.View.SetRowCellValue(rowHandle, "EQUIPMENTID", "");

                _isGoodCopy = false; //무조건 메세지 출력
            }

            if (_clipDatas != null)
            {
                if (_clipIndex.Equals(_clipDatas.Length)) //마지막 행을 수행하고 난 이후 메세지 출력
                {
                    if (!_isGoodCopy)
                    {
                        ShowMessage(MessageBoxButtons.OK, "TOOLMAKEVENDORSELECT", "");
                    }

                    //초기화
                    _clipIndex = 1;
                    _clipDatas = null;
                    _isGoodCopy = true;
                }
                else
                {
                    _clipIndex++;
                }
            }
        }
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (factoryBox != null)
                factoryBox.Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (popupGridToolArea != null)
            //    popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if(Conditions.GetCondition("EQUIPMENTID") != null)
                ((ConditionItemSelectPopup)((ConditionItemSelectPopup)Conditions.GetCondition("EQUIPMENTID")).Conditions.GetCondition("AREAID")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}"); 
            //if (segmentCondition != null)
            //    segmentCondition.Query = new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", Conditions.GetValue("P_PLANTID") } });

            //if (makeVendorPopup != null)
            //    makeVendorPopup.SearchQuery = new SqlQuery("GetVendorListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (filmCodeCondition != null)
            //    filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");

            //ucFilmCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

            //작업장 검색조건 초기화
            //((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }
        #endregion

        #region GetButtonWidth - 글자수에 따라 버튼의 크기를 결정
        private int GetButtonWidth(string caption)
        {
            return caption.Length * 18;
        }
        #endregion
        #endregion
    }
}
