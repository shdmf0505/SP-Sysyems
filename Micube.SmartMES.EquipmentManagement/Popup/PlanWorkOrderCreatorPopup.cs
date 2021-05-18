#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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

namespace Micube.SmartMES.EquipmentManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : PM설비수리 생성 팝업
    /// 업  무  설  명  : PM설비수리를 생성하는 팝업
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PlanWorkOrderCreatorPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        public delegate void reSearchEvent(string subMaintType);
        public event reSearchEvent SearchHandler;
        string _currentStatus;
        string _maintTypeID;
        string _plantID;

        ConditionItemSelectPopup equipmentClassPopup;
        ConditionItemSelectPopup equipmentPopup;
        ConditionItemSelectPopup popupGridToolArea;
        ConditionItemSelectPopup maintItemPopup;
        #endregion

        #region Properties
        public string MaintTypeID
        {
            set { _maintTypeID = value; }
        }
        #endregion

        public PlanWorkOrderCreatorPopup(string plantID)
        {
            InitializeComponent();

            InitializeEvent();

            _plantID = plantID;
        }

        #region 컨텐츠영역 초기화
        private void InitializeContent()
        {
            InitializeGrid();

            deStartDate.EditValue = DateTime.Now;

            InitializeRequiredControl();
            InitializeConditionPlant();
            InitializeMaintType();
            InitializeFactory();
            InitializePlanPeriod();
            InitializeConditionEquipmentClassTypePopup();
            InitializeConditionEquipmentIDPopup();
            //InitializeMaintItem();
            InitializeMaintItemPopup();
        }

        #region InitializeGrid - PM점검항목목록을 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdEquipmentMaint.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기                        
            grdEquipmentMaint.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdEquipmentMaint.View.AddTextBoxColumn("EQUIPMENTCLASSID", 120)                //출소상태
                .SetIsReadOnly(true);
            grdEquipmentMaint.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200)             //의뢰일자
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);
            grdEquipmentMaint.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsReadOnly(true);                                                      //의뢰순번
            grdEquipmentMaint.View.AddTextBoxColumn("EQUIPMENTNAME", 300)
                .SetIsReadOnly(true);                                                      //제작구분명
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTITEMID")
                .SetIsHidden();                                                            //제작구분명           
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTITEMNAME", 250)
                .SetIsReadOnly(true);                                                      //품목코드
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTDURATION", 100)
                .SetIsReadOnly(true);                                                      //품목코드
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTDURATIONUNIT", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                      //품목명            
            grdEquipmentMaint.View.AddDateEditColumn("MAINTLASTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                ;
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTPLANTYPE", 100)
                .SetIsReadOnly(true);
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTPLANDAY", 100)
                .SetIsReadOnly(true);
            grdEquipmentMaint.View.AddDateEditColumn("EXPECTSTARTDATE", 120)
                .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
                ;
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTITEMCLASSID", 10)
                .SetIsHidden();
            grdEquipmentMaint.View.AddTextBoxColumn("MAINTITEMCLASSNAME", 10)
                .SetIsHidden();
            //grdToolUpdateSend.View.SetAutoFillColumn("PRODUCTDEFNAME");
            grdEquipmentMaint.View.PopulateColumns();
        }
        #endregion

        #region InitializeRequiredControl - 필수값 설정        
        private void InitializeRequiredControl()
        {
            SetRequiredValidationControl(lblSite, true);
            SetRequiredValidationControl(lblMaintType, true);
            SetRequiredValidationControl(lblPlanDate, true);
        }
        #endregion

        #region InitializeConditionPlant : Site를 설정한다.
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPlant()
        {
            cboPlant.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlant.ValueMember = "PLANTID";
            cboPlant.DisplayMember = "PLANTID";
            cboPlant.EditValue = UserInfo.Current.Plant;
            cboPlant.ReadOnly = true;
            

            cboPlant.DataSource = SqlExecuter.Query("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise } });

            cboPlant.ShowHeader = false;
        }
        #endregion

        #region InitializeMaintType - MaintType콤보박스 초기화
        private void InitializeMaintType()
        {
            // 검색조건에 정의된 공장을 정리
            cboMaintType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboMaintType.ValueMember = "CODEID";
            cboMaintType.DisplayMember = "CODENAME";
            //cboMaintType.UseEmptyItem = true;
            //cboMaintType.EmptyItemValue = "";
            //cboMaintType.EmptyItemCaption = "";

            cboMaintType.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "MaintType" } });

            cboMaintType.ShowHeader = false;

            if (_maintTypeID != null)
                cboMaintType.EditValue = _maintTypeID;
        }
        #endregion

        #region InitializeFactory - Factory콤보박스 초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeFactory()
        {
            // 검색조건에 정의된 공장을 정리
            cboFactory.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboFactory.ValueMember = "FACTORYID";
            cboFactory.DisplayMember = "FACTORYNAME";
            cboFactory.UseEmptyItem = true;
            cboFactory.EmptyItemValue = "";
            cboFactory.EmptyItemCaption = "";

            cboFactory.DataSource = SqlExecuter.Query("GetFactoryListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "PLANTID", UserInfo.Current.Plant } });

            cboFactory.ShowHeader = false;
        }
        #endregion

        #region InitializePlanPeriod - PlanPeriod콤보박스 초기화
        private void InitializePlanPeriod()
        {
            // 검색조건에 정의된 공장을 정리
            cboPlanPeriod.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboPlanPeriod.ValueMember = "CODEID";
            cboPlanPeriod.DisplayMember = "CODENAME";
            cboPlanPeriod.EditValue = "OneMonth";

            cboPlanPeriod.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "MWOCalPeriodType" } });

            cboPlanPeriod.ShowHeader = false;
        }
        #endregion

        #region InitializeConditionEquipmentClassTypePopup : 설비그룹조회
        /// <summary>
        /// 설비그룹조회설정
        /// </summary>
        private void InitializeConditionEquipmentClassTypePopup()
        {
            equipmentClassPopup = new ConditionItemSelectPopup();

            equipmentClassPopup.Id = "EQUIPMENTCLASSID";
            equipmentClassPopup.SearchQuery = new SqlQuery("GetEquipmentClassListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            equipmentClassPopup.SetPopupLayout("EQUIPMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, false);
            equipmentClassPopup.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            equipmentClassPopup.SetPopupResultMapping("EQUIPMENTCLASSID", "EQUIPMENTCLASSID");
            equipmentClassPopup.SetLabel("EQUIPMENTCLASS");
            equipmentClassPopup.SetPopupResultCount(1);
            equipmentClassPopup.SetPosition(2.2);

            equipmentClassPopup.DisplayFieldName = "EQUIPMENTCLASSNAME";
            equipmentClassPopup.ValueFieldName = "EQUIPMENTCLASSID";

            // 팝업 조회조건
            equipmentClassPopup.Conditions.AddTextBox("EQUIPMENTCLASSNAME")
                .SetLabel("EQUIPMENTCLASSNAME");
            equipmentClassPopup.Conditions.AddComboBox("EQUIPMENTCLASSTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentClassType" } }), "CODENAME", "CODEID")
                .SetEmptyItem("", "", true)
                .SetLabel("EQUIPMENTCLASSTYPE")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;
            equipmentClassPopup.Conditions.AddComboBox("EQUIPMENTCLASSHIERARCHY", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentClassHierarchy" } }), "CODENAME", "CODEID")
                .SetDefault("MainEquipment", "CODEID")
                .SetLabel("EQUIPMENTCLASSHIERARCHY")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;

            // 팝업 그리드
            equipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 120)
                .SetIsHidden();
            equipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200)
                .SetIsReadOnly();
            equipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSTYPEID", 50)
                .SetIsHidden();
            equipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSTYPE", 100)
                .SetIsHidden();
            equipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSHIERARCHYID", 200)
                .SetIsHidden();
            equipmentClassPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSHIERARCHY", 100)
                .SetIsHidden();
            equipmentClassPopup.GridColumns.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();

            popEquipmentGroup.SelectPopupCondition = equipmentClassPopup;
        }
        #endregion

        #region InitializeConditionEquipmentIDPopup : 설비조회
        private void InitializeConditionEquipmentIDPopup()
        {
            equipmentPopup = new ConditionItemSelectPopup();

            equipmentPopup.Id = "EQUIPMENTID";
            equipmentPopup.SearchQuery = new SqlQuery("GetEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y");
            equipmentPopup.SetPopupLayout("EQUIPMENTID", PopupButtonStyles.Ok_Cancel, true, false);
            equipmentPopup.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            equipmentPopup.SetPopupResultMapping("EQUIPMENTID", "EQUIPMENTID");
            equipmentPopup.SetLabel("EQUIPMENT");
            equipmentPopup.SetPopupResultCount(1);
            equipmentPopup.SetPosition(2.3);

            equipmentPopup.ValueFieldName = "EQUIPMENTID";
            equipmentPopup.DisplayFieldName = "EQUIPMENTNAME";

            // 팝업 조회조건
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
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASSID", 50)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTCLASS", 100)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTTYPEID", 50)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTTYPE", 100)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPEID", 200)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("DETAILEQUIPMENTTYPE", 100)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("MODEL", 100)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("SERIAL", 150)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();

            popEquipmentID.SelectPopupCondition = equipmentPopup;
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

            popupGridToolArea.Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "FACTORYNAME", "FACTORYID")
                ;

            // 팝업 그리드 설정
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            popupGridToolArea.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }
        #endregion        

        #region InitializeMaintItem - MaintType콤보박스 초기화
        private void InitializeMaintItem()
        {
            // 검색조건에 정의된 공장을 정리
            //cboMaintItem.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            //cboMaintItem.ValueMember = "MAINTITEMID";
            //cboMaintItem.DisplayMember = "MAINTITEMNAME";
            //cboMaintItem.UseEmptyItem = true;
            //cboMaintItem.EmptyItemValue = "";
            //cboMaintItem.EmptyItemCaption = "";

            //cboMaintItem.DataSource = SqlExecuter.Query("GetMaintItemListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            //cboMaintItem.ShowHeader = false;
        }
        #endregion

        #region InitializeMaintItemPopup : 점검항목조회
        private void InitializeMaintItemPopup()
        {
            maintItemPopup = new ConditionItemSelectPopup();

            maintItemPopup.Id = "MAINTITEMID";
            maintItemPopup.SearchQuery = new SqlQuery("GetMaintItemListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            maintItemPopup.SetPopupLayout("MAINTITEMID", PopupButtonStyles.Ok_Cancel, true, false);
            maintItemPopup.SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow);
            maintItemPopup.SetPopupResultMapping("MAINTITEMID", "MAINTITEMID");
            maintItemPopup.SetLabel("MAINTITEMID");
            maintItemPopup.SetPopupResultCount(1);
            maintItemPopup.SetPosition(2.3);

            maintItemPopup.ValueFieldName = "MAINTITEMID";
            maintItemPopup.DisplayFieldName = "MAINTITEMNAME";

            // 팝업 조회조건
            maintItemPopup.Conditions.AddTextBox("MAINTITEMNAME")
                .SetLabel("MAINTITEMNAME");
            maintItemPopup.Conditions.AddComboBox("MAINTITEMCLASSID", new SqlQuery("GetMaintItemClassListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "MAINTITEMCLASSNAME", "MAINTITEMCLASSID")
                .SetEmptyItem("", "", true)
                .SetLabel("MAINTITEMCLASSID")
                .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
                ;

            // 팝업 그리드
            maintItemPopup.GridColumns.AddTextBoxColumn("MAINTITEMID", 120)
                .SetIsHidden();
            maintItemPopup.GridColumns.AddTextBoxColumn("MAINTITEMNAME", 200)
                .SetIsReadOnly();

            popMaintItem.SelectPopupCondition = maintItemPopup;
        }
        #endregion
        #endregion

        #region Event
        private void InitializeEvent()
        {
            Shown += RegistRequestMaintWorkOrderPopup_Shown;
            btnSearch.Click += BtnSearch_Click;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;

            cboPlanPeriod.EditValueChanged += CboPlanPeriod_EditValueChanged;
            deStartDate.EditValueChanged += DeStartDate_EditValueChanged;

            popEquipmentID.ButtonClick += PopEquipmentID_ButtonClick;
            popMaintItem.ButtonClick += PopMaintItem_ButtonClick;   

        }

        private void PopMaintItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Search)
            {
                if (cboMaintType.EditValue != null)
                {
                    popMaintItem.SelectPopupCondition.SearchQuery = new SqlQuery("GetMaintItemListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"MAINTITEMCLASSTYPE={cboMaintType.EditValue.ToString()}");
                }
            }
        }

        private void PopEquipmentID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Search)
            {
                if (popEquipmentGroup.GetValue() != null)
                {
                    popEquipmentID.SelectPopupCondition.SearchQuery = new SqlQuery("GetEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"EQUIPMENTCLASSID={popEquipmentGroup.GetValue().ToString()}", $"MAINTTYPE={cboMaintType.EditValue.ToString()}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y");
                }
            }
        }

        private void DeStartDate_EditValueChanged(object sender, EventArgs e)
        {
            CboPlanPeriod_EditValueChanged(cboPlanPeriod, new EventArgs());
        }

        private void CboPlanPeriod_EditValueChanged(object sender, EventArgs e)
        {
            if(cboPlanPeriod.EditValue != null && deStartDate.EditValue != null)
            switch (cboPlanPeriod.EditValue.ToString())
            {
                    case "OneMonth":                        
                        deEndDate.EditValue = Convert.ToDateTime(deStartDate.EditValue).AddMonths(1).AddDays(-1);
                        break;
                    case "TwoMonth":
                        deEndDate.EditValue = Convert.ToDateTime(deStartDate.EditValue).AddMonths(2).AddDays(-1);
                        break;
                    case "ThreeMonth":
                        deEndDate.EditValue = Convert.ToDateTime(deStartDate.EditValue).AddMonths(3).AddDays(-1);
                        break;
                    case "FourMonth":
                        deEndDate.EditValue = Convert.ToDateTime(deStartDate.EditValue).AddMonths(4).AddDays(-1);
                        break;
                    case "SixMonth":
                        deEndDate.EditValue = Convert.ToDateTime(deStartDate.EditValue).AddMonths(6).AddDays(-1);
                        break;
                    case "OneYear":
                        deEndDate.EditValue = Convert.ToDateTime(deStartDate.EditValue).AddYears(1).AddDays(-1);
                        break;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void CboRequestResult_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            CreateSchedule();
        }

        private void RegistRequestMaintWorkOrderPopup_Shown(object sender, EventArgs e)
        {
            InitializeContent();

            cboPlant.EditValue = _plantID;

            cboFactory.DataSource = SqlExecuter.Query("GetFactoryListByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "PLANTID", _plantID } });
            equipmentClassPopup.SearchQuery = new SqlQuery("GetEquipmentClassListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}");
            equipmentPopup.SearchQuery = new SqlQuery("GetEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y");
            popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y");
            ((ConditionItemComboBox)popupGridToolArea.Conditions.GetCondition("FACTORYID")).Query = new SqlQuery("GetFactoryListByEqp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}");
            maintItemPopup.SearchQuery = new SqlQuery("GetMaintItemListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={_plantID}");
        }
        #endregion

        #region 유효성 검사

        #region ValidateCurrentStatus : 현재 저장요청한 정보가 저장 가능한 정보인지 검증
        private bool ValidateCurrentStatus()
        {
            return true;
        }
        #endregion

        #region ValidateContent : 저장가능한 정보인지 체크
        private bool ValidateContent(out string messageCode, string saveStatus)
        {
            messageCode = "";        

            //if (!ValidateEditValue(cboDownType.EditValue))
            //{
            //    messageCode = "ValidateRequiredData";
            //    return false;
            //}

            //if (!ValidateEditValue(cboDownCode.EditValue))
            //{
            //    messageCode = "ValidateRequiredData";
            //    return false;
            //}

            //if (!ValidateEditValue(cboRequestResult.EditValue))
            //{
            //    messageCode = "ValidateRequiredData";
            //    return false;
            //}

            //if (saveStatus.Equals("Repair"))
            //{
            //    if (!ValidateEditValue(deScheduleTime.EditValue))
            //    {
            //        messageCode = "ValidateRequiredData";
            //        return false;
            //    }
            //}
            //else
            //{
            //    if (!ValidateEditValue(txtAcceptComments.EditValue))
            //    {
            //        messageCode = "ValidateRequiredData";
            //        return false;
            //    }
            //}

            return true;
        }
        #endregion

        /// <summary>
        /// 입력/수정 시의 Validation 체크
        /// 입력/수정 각각의 체크값이 서로 다르다면 상태에 따른 분기가 있어야 한다.
        /// </summary>
        /// <returns></returns>
        private bool ValidateContent()
        {
            //각각의 상태에 따라 보여줄 메세지가 상이하다면 각각의 분기에서 메세지를 표현해야 한다.            

            //DataTable inputTable = grdPMPlan.View.GetCheckedRows();

            //foreach (DataRow inputRow in inputTable.Rows)
            //{
            //    if (!ValidateCellInGrid(inputRow, new string[] { "EQUIPMENTID", "MAINTITEMID", "PLANDATE" }))
            //        return false;
            //}

            return true;
        }

        /// <summary>
        /// 2개의 콤보박스에 데이터가 모두 있어야 하며
        /// 2개의 콤보박스의 데이터가 서로 동일하면 안된다.
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="targetBox"></param>
        /// <returns></returns>
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }

        /// <summary>
        /// 숫자를 입력하는 컨트롤을 대상으로 하여 입력받은 기준값보다 값이 같거나 크다면 true 작다면 false를 반환하는 메소드
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="ruleValue"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 숫자를 입력하는 컨트롤을 대상으로 하여 입력받은 기준값보다 값이 같거나 크다면 true 작다면 false를 반환하는 메소드
        /// </summary>
        /// <param name="originBox"></param>
        /// <param name="ruleValue"></param>
        /// <returns></returns>
        private bool ValidateNumericValue(object editValue, int ruleValue)
        {
            //값이 없으면 안된다.
            if (editValue == null)
                return false;

            int resultValue = 0;

            //입력받은 기준값(예를 들어 0)보다 작다면 false를 반환
            if (Int32.TryParse(editValue.ToString(), out resultValue))
                if (resultValue < ruleValue)
                    return false;

            //모두 통과했으므로 Validation Check완료
            return true;
        }

        /// <summary>
        /// 입력받은 editValue에 아무런 값이 입력되지 않았다면 false 입력받았다면 true
        /// </summary>
        /// <param name="editValue"></param>
        /// <returns></returns>
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }

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

        private void SetRequiredValidationControl(Control requiredControl, bool isRequired)
        {
            if (isRequired)
                requiredControl.ForeColor = Color.Red;
            else
                requiredControl.ForeColor = Color.Black;
        }
        #endregion

        #region Private Function
        #region Search - 검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected void  Search()
        {
            InitializeInsertForm();
            // TODO : 조회 SP 변경
            Dictionary<string, object> values = new Dictionary<string, object>();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", _plantID);
            values.Add("EQUIPMENTCLASSID", popEquipmentGroup.GetValue());
            values.Add("EQUIPMENTID", popEquipmentID.GetValue());
            values.Add("FACTORYID", cboFactory.EditValue);
            values.Add("MAINTITEMID", popMaintItem.GetValue());
            values.Add("MAINTTYPE", cboMaintType.EditValue);            
            values.Add("P_PLANDATE_PERIODFR", Convert.ToDateTime(deStartDate.EditValue).ToString("yyyy-MM-dd"));
            values.Add("P_PLANDATE_PERIODTO", Convert.ToDateTime(deEndDate.EditValue).ToString("yyyy-MM-dd"));
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolRepairResult = SqlExecuter.Query("GetExpectEquipmentWorkOrderListByEqp", "10001", values);

            grdEquipmentMaint.DataSource = toolRepairResult;

            if (toolRepairResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                grdEquipmentMaint.DataSource = null;
            }
            else
            {
                grdEquipmentMaint.View.FocusedRowHandle = 0;
            }

            _currentStatus = "modified";
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

            dt.Columns.Add("STARTDATE");
            dt.Columns.Add("ENDDATE");

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

        #region CreateDeleteDatatable : PreventiveMaintItem 삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// PreventiveMaintItem 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDeleteDatatable(bool useState)
        {
            DataTable dt = new DataTable();
            dt.TableName = "deletePMConditionList";
            //===================================================================================            
            dt.Columns.Add("DELETETYPE");
            dt.Columns.Add("FACTORYID");
            dt.Columns.Add("LANGUAGETYPE");
            dt.Columns.Add("EQUIPMENTCLASSID");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("PLANTID");

            dt.Columns.Add("MAINTITEMID");
            dt.Columns.Add("MAINTTYPE");

            dt.Columns.Add("P_PLANDATE_PERIODFR");
            dt.Columns.Add("P_PLANDATE_PERIODTO");
            dt.Columns.Add("ISWORKORDERID");

            return dt;
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
                btnSave.Enabled = false;

                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent())
                {
                    DataSet maintItemSet = new DataSet();
                    DataTable maintItemTable = CreateSaveDatatable(true);
                    DataTable deleteTable = CreateDeleteDatatable(true);

                    DataTable ruleTable = grdEquipmentMaint.View.GetCheckedRows();
                    string maintItemID = "";
                    string equipmentID = "";
                    string maintItemClassID = "";
                    string startRuleDate = "";

                    if (ruleTable != null && ruleTable.Rows.Count > 0)
                    {
                        //점검시작일을 기준으로 기존의 Equipment가 점검이 종료되지 않았는지 점검시작일 이후의 점검이 존재하는지 조회한다.
                        foreach (DataRow ruleRow in ruleTable.Rows)
                        {
                            equipmentID = ruleRow.GetString("EQUIPMENTID");
                            maintItemID = ruleRow.GetString("MAINTITEMID");
                            maintItemClassID = ruleRow.GetString("MAINTITEMCLASSID");

                            //Duration(점검주기)이 0인 점검계획은 수립할 수 없다.
                            if(ruleRow.GetInteger("MAINTDURATION") < 1)
                            {
                                ShowMessage(MessageBoxButtons.OK, "ValidateCreateScheduleZeroDuration", "");
                                return;//로직종료
                            }

                            //점검주기와 점검주기단위를 이용하여 적절한 날짜의 배열을 구한다. (배열만큼 입력)
                            //ToDo : 마지막 점검일의 다음 주기의 날을 구하여 시작일로 지정한다.
                            //ToDo : DB에서 특정한 값을 바탕으로 마지막 점검일 이후의 날을 시작일로 지정할 지 화면에서 지정된 시작일을 점검시작일로 지정할 지 정한다.
                            List<DateTime> ruleDates = GetDateDurationCollection(
                                ruleRow.GetString("EXPECTSTARTDATE")
                                , Convert.ToDateTime(deEndDate.EditValue).ToString("yyyy-MM-dd")
                                , ruleRow.GetInteger("MAINTDURATION")
                                , ruleRow.GetString("MAINTDURATIONUNIT")
                                , ruleRow.GetString("MAINTPLANDAY")
                                , ruleRow.GetString("MAINTPLANTYPE")
                                );

                            foreach (DateTime rules in ruleDates)
                            {
                                DataRow maintItemRow = maintItemTable.NewRow();

                                maintItemRow["MAINTITEMID"] = ruleRow.GetString("MAINTITEMID");
                                maintItemRow["EQUIPMENTID"] = ruleRow.GetString("EQUIPMENTID");
                                maintItemRow["PLANDATE"] = rules.ToString("yyyy-MM-dd");
                                maintItemRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                maintItemRow["PLANTID"] = _plantID;

                                maintItemRow["STARTDATE"] = ((DateTime)deStartDate.EditValue).ToString("yyyy-MM-dd");
                                maintItemRow["ENDDATE"] = ((DateTime)deEndDate.EditValue).ToString("yyyy-MM-dd");

                                maintItemRow["CREATOR"] = UserInfo.Current.Id;
                                maintItemRow["MODIFIER"] = UserInfo.Current.Id;
                                maintItemRow["_STATE_"] = "added";
                                maintItemRow["VALIDSTATE"] = "Valid";

                                maintItemTable.Rows.Add(maintItemRow);
                            }
                        }

                        maintItemSet.Tables.Add(maintItemTable);

                        DataRow deleteRow = deleteTable.NewRow();
                        //삭제를 위한 데이터 입력
                        deleteRow["DELETETYPE"] = "MANUAL";
                        deleteRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;
                        deleteRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        deleteRow["PLANTID"] = _plantID;
                        deleteRow["P_PLANDATE_PERIODFR"] = ((DateTime)deStartDate.EditValue).ToString("yyyy-MM-dd");
                        deleteRow["P_PLANDATE_PERIODTO"] = ((DateTime)deEndDate.EditValue).ToString("yyyy-MM-dd");
                        deleteRow["FACTORYID"] = cboFactory.EditValue;
                        deleteRow["EQUIPMENTCLASSID"] = popEquipmentGroup.GetValue();
                        deleteRow["EQUIPMENTID"] = popEquipmentID.GetValue();
                        deleteRow["MAINTITEMID"] = popMaintItem.GetValue();
                        deleteRow["MAINTTYPE"] = cboMaintType.EditValue;
                        deleteRow["ISWORKORDERID"] = "Y";

                        deleteTable.Rows.Add(deleteRow);

                        maintItemSet.Tables.Add(deleteTable);

                        ExecuteRule<DataTable>("PreventiveMaintPlanBatch", maintItemSet);

                        ControlEnableProcess("modified");

                        SearchHandler?.Invoke(cboMaintType.EditValue.ToString());

                        Dispose();
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, "ValidateCheckedRowForPMPlan", "");
                    }
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
            }
        }
        #endregion

        #region GetDateDurationCollection : 시작일과 종료일 내에서 점검주기단위에 따른 주기별 날짜를 계산하여 반환한다.
        private List<DateTime> GetDateDurationCollection(string startDateStr, string endDateStr, int duration, string durationUnit, string planDay, string planType)
        {
            List<DateTime> resultDates = new List<DateTime>();

            DateTime startDate = Convert.ToDateTime(startDateStr);
            DateTime endDate = Convert.ToDateTime(endDateStr);

            bool isStart = true;
            
            for (; DateTime.Compare(startDate, endDate) <= 0; startDate = GetNextDateTime(startDate, duration, durationUnit))
                if (DateTime.Compare(startDate, endDate) <= 0) //GetNextDateTime으로 받아온 날짜가 endDate를 초과한다면 Add하지 않아야 한다.
                {
                    //시작일이 기준일보다 
                    resultDates.Add(startDate);

                    if (isStart) //시작일을 입력하고자 할때 특별한 로직을 따른다.
                    {
                        if (durationUnit.Equals("Month") && planType.Equals("Plan"))
                        {
                            //입력하고난 시작일을 다시 설정한다. 즉 다음 루프부터는 계획기준일에 맞추어 점검일을 설정한다.
                            startDate = Convert.ToDateTime(startDate.ToString("yyyy-MM") + "-" + planDay);                            
                        }
                        isStart = false;
                    }
                }

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
        #endregion
    }
}
