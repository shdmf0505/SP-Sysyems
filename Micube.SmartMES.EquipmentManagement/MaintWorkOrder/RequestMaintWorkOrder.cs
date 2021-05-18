#region using

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
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 설비수리요청등록
    /// 업  무  설  명  : 설비수리를 요청한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RequestMaintWorkOrder : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _currentStatus;
        string _repairUserID;
        string _requestUserID;
        string _searchEquipmentID;

        ConditionItemSelectPopup equipmentPopup;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup userPopup;
        ConditionItemSelectPopup equipmentSearchPopup;
        #endregion

        #region 생성자

        public RequestMaintWorkOrder()
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
            InitializeEquipmentNo();
            InitializeRequestUser();
            InitializeDownType();
            InitializeDownCode();

            InitializeInsertForm();
        }

        #region InitializeGrid - 제작입고목록을 초기화한다.
        /// <summary>        
        /// 치공구내역목록을 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaintWorkOrder.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdMaintWorkOrder.View.SetIsReadOnly();

            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERID", 160)               //출소상태
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTATUSID", 60)             //의뢰일자
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTATUS", 100)           //의뢰순번
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTEPID")
                .SetIsHidden();                                                     //제작구분명
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKORDERSTEP", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                //제작구분명
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTDEPARTMENT", 150)
                ;                                                                   //제작구분명           
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTUSERID", 10)
                .SetIsHidden();                                                     //품목코드
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTUSER", 100)
                ;                                                                   //품목코드
            grdMaintWorkOrder.View.AddTextBoxColumn("EQUIPMENTID", 120);         //품목명            
            grdMaintWorkOrder.View.AddTextBoxColumn("EQUIPMENTNAME", 300)
                ;                                                                   //제작업체
            grdMaintWorkOrder.View.AddTextBoxColumn("PROCESSSEGMENTCLASS", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;                                                                   //의뢰수량
            grdMaintWorkOrder.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100)
                .SetIsHidden();                                                      //의뢰수량
            grdMaintWorkOrder.View.AddTextBoxColumn("AREANAME", 180)
                ;                                                                   //작업장           
            grdMaintWorkOrder.View.AddTextBoxColumn("WORKFLOOR", 100);             //의뢰자
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNTYPEID")
                .SetIsHidden();                                                     //의뢰자
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNTYPE", 150)           //의뢰부서
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNCODEID", 10)          //제작사유
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("DOWNCODE", 150)             //설명
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEQUIPMENTDOWNREQUEST", 120)             //설명
                .SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTCOMMENT", 200)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("REQUESTTIME", 160)             //설명
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEMERGENCY", 60)             //설명
                .SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("EMERGENCYREASON", 200)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("ACCEPTTIME", 160)             //설명
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMaintWorkOrder.View.AddTextBoxColumn("ACCEPTUSERID", 10)             //설명
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("ACCEPTUSER", 100)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("SCHEDULETIME", 160)             //설명
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRUSER", 200)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRUSERID", 10)             //설명
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRUSERCOUNT", 100)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("CAUSECODEID", 10)             //설명
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("CAUSECODE", 150)             //설명
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRCODEID", 10)             //설명
                .SetIsHidden();
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRCODE", 150)               //설명
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("ISEQUIPMENTDOWN", 60)             //설명
                .SetTextAlignment(TextAlignment.Center);
            grdMaintWorkOrder.View.AddTextBoxColumn("REPAIRCOMMENT", 200)             //설명
                ;
            grdMaintWorkOrder.View.AddTextBoxColumn("STARTREPAIRTIME", 160)             //설명
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMaintWorkOrder.View.AddTextBoxColumn("FINISHREPAIRTIME", 160)             //설명
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");            
            grdMaintWorkOrder.View.PopulateColumns();
        }
        #endregion

        #region InitializeEquipmentNo - 설비조회화면설정
        /// <summary>
        /// 결재지정자를 조회
        /// </summary>
        private void InitializeEquipmentNo()
        {
            equipmentPopup = new ConditionItemSelectPopup();

            equipmentPopup.Id = "EQUIPMENTID";
            equipmentPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}");
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
                    txtProcessSegment.EditValue = row["PROCESSSEGMENTCLASSNAME"];
                    txtAreaName.EditValue = row["AREANAME"];
                    txtEquipmentGrade.EditValue = row["EQUPMENTLEVEL"];
                    popEditEquipmentCode.EditValue = row["EQUIPMENTID"];
                }
            });
            //equipmentPopup.SetRelationIds("P_PLANTID");
            // 팝업 조회조건            
            //equipmentPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "PLANTID", "PLANTID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PLANTID")
            //    .SetDefault(UserInfo.Current.Plant, "PLANTID")
            //    .SetIsReadOnly(true)
            //    .SetRelationIds("P_PLANTID")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;

            InitializeReceiptAreaPopupForPopup(equipmentPopup.Conditions);

            equipmentPopup.Conditions.AddTextBox("EQUIPMENTNAME")
                .SetLabel("EQUIPMENTNAME");
            equipmentPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
               .SetEmptyItem("", "", true)
               .SetLabel("PROCESSSEGMENTCLASS")
               .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
               ;
            //skPark 수정
            equipmentPopup.Conditions.AddComboBox("SEPARATOR", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Separator", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetEmptyItem("", "", true)
               .SetLabel("SEPARATOR")
               .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
               ;
            equipmentPopup.Conditions.AddTextBox("AREAID")
                .SetLabel("AREAID")
                .SetIsHidden()
                ;

            // 팝업 그리드
            equipmentPopup.IsMultiGrid = false;
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("FACTORYID", 150)
              .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();
           
            equipmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUPMENTLEVEL", 200)
                .SetIsReadOnly()
                .SetIsHidden();

            popEditEquipmentCode.SelectPopupCondition = equipmentPopup;
        }

        #endregion

        #region InitializeReceiptAreaPopup : 입고작업장 팝업창
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeReceiptAreaPopupForPopup(ConditionCollection tempControl)
        {
            areaCondition = tempControl.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y"));
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetPopupResultMapping("AREANAME", "AREANAME");
            //areaCondition.SetRelationIds("P_PLANTID");

            areaCondition.Conditions.AddTextBox("AREANAME");
            areaCondition.Conditions.AddTextBox("AREAID");
            

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                ;
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        #region InitializeRequestUser - 사용자선정팝업
        /// <summary>
        /// 결재지정자를 조회
        /// </summary>
        private void InitializeRequestUser()
        {
            userPopup = new ConditionItemSelectPopup();

            userPopup.Id = "REQUESTUSER";
            userPopup.SearchQuery = new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}");
            userPopup.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false);
            userPopup.SetPopupResultCount(1);
            userPopup.SetPopupLayoutForm(1000, 600, FormBorderStyle.FixedToolWindow);
            userPopup.SetPopupAutoFillColumns("EQUIPMENTID");
            userPopup.SetPopupResultMapping("REQUESTUSER", "USERNAME");
            userPopup.ValueFieldName = "USERID";
            userPopup.DisplayFieldName = "USERNAME";
            userPopup.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                foreach (DataRow row in selectedRows)
                {
                    txtRequestDepartment.EditValue = row["DEPARTMENT"];
                    _requestUserID = row["USERID"].ToString();
                    popEditRequestUser.EditValue = row["USERNAME"];
                }
            });
            //userPopup.SetRelationIds("P_PLANTID");

            // 팝업 조회조건            
            userPopup.Conditions.AddTextBox("USERNAME")
                .SetLabel("USERNAME");

            // 팝업 그리드
            userPopup.IsMultiGrid = false;
            userPopup.GridColumns.AddTextBoxColumn("USERID", 10)
                .SetIsHidden();
            userPopup.GridColumns.AddTextBoxColumn("USERNAME", 150)
                .SetIsReadOnly();
            userPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 150)
                .SetIsReadOnly();
            userPopup.GridColumns.AddTextBoxColumn("DUTY", 100)
                .SetIsReadOnly();
            userPopup.GridColumns.AddTextBoxColumn("PLANT", 150)
                .SetIsReadOnly();
            userPopup.GridColumns.AddTextBoxColumn("ENTERPRISE", 200)
                .SetIsReadOnly();

            popEditRequestUser.SelectPopupCondition = userPopup;
        }
        #endregion

        #region InitializeDownType - DownType콤보박스 초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeDownType()
        {
            // 검색조건에 정의된 공장을 정리
            cboDownType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDownType.ValueMember = "CODEID";
            cboDownType.DisplayMember = "CODENAME";
            cboDownType.EditValue = "1";

            cboDownType.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentDownType" } });

            cboDownType.ShowHeader = false;
        }
        #endregion

        #region InitializeDownCode - DownCode콤보박스 초기화
        /// <summary>
        /// Factory ComboBox를 초기화한다. 검색조건의 Site에 따라 값이 변경되어야 하므로
        /// 초기화 버튼 클릭시 재로딩하는 것으로 한다.
        /// </summary>
        /// <param name="plantID"></param>
        private void InitializeDownCode()
        {
            // 검색조건에 정의된 공장을 정리
            cboDownCode.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboDownCode.ValueMember = "CODEID";
            cboDownCode.DisplayMember = "CODENAME";
            cboDownCode.EditValue = "1";

            cboDownCode.DataSource = SqlExecuter.Query("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentDownCode" } });

            cboDownCode.ShowHeader = false;
        }
        #endregion      
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnInitialize.Click += BtnInitialize_Click;
            btnModify.Click += BtnModify_Click;

            grdMaintWorkOrder.View.FocusedRowChanged += View_FocusedRowChanged;

            Shown += RequestMaintWorkOrder_Shown;
        }

        #region RequestMaintWorkOrder_Shown - Site관련정보를 화면로딩후 설정한다.
        private void RequestMaintWorkOrder_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;

            SetRequiredValidationControl(lblRequestQty);

            SetRequiredValidationControl(smartLabel11);

            SetRequiredValidationControl(lblProductDefID);

            SetRequiredValidationControl(smartLabel14);

            SetRequiredValidationControl(smartLabel4);

            SetRequiredValidationControl(smartLabel19);
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region View_FocusedRowChanged - 그리드의 행변경이벤트
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayMaintWorkOrderInfo();
        }
        #endregion

        #region BtnModify_Click - 수정버튼클릭이벤트
        private void BtnModify_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region BtnInitialize_Click - 초기화버튼클릭이벤트
        private void BtnInitialize_Click(object sender, EventArgs e)
        {
            InitializeInsertForm();
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

            if (btn.Name.ToString().Equals("Regist"))
                BtnModify_Click(sender, e);
            else if (btn.Name.ToString().Equals("Initialization"))
                BtnInitialize_Click(sender, e);
        }

        #endregion

        #region 검색

        #region OnSearchAsync - 검색
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            //InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            if (Conditions.GetValue("EQUIPMENTNAME").ToString() != "")
                values.Add("EQUIPMENTID", _searchEquipmentID);

            if (Conditions.GetValue("p_WORKORDERSTATUS").ToString() != "")
                values.Add("WORKORDERSTATUS", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTATUS").ToString()));

            if (Conditions.GetValue("p_WORKORDERSTEP").ToString() != "")
                values.Add("WORKORDERSTEP", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTEP").ToString()));
            #endregion
            //values = Commons.CommonFunction.ConvertParameter(values);
            DataTable maintWorkOrderTable = SqlExecuter.Query("GetMaintWorkOrderListByEqp", "10001", values);

            grdMaintWorkOrder.DataSource = maintWorkOrderTable;

            if (maintWorkOrderTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdMaintWorkOrder.View.FocusedRowHandle = 0;
            }
        }
        #endregion

        #region GetConditionStringValue - 조회조건을 다중선택했을때 '', ''로 변경
        string GetConditionStringValue(string originCondition)
        {
            if(originCondition.IndexOf(",") > -1)
            {
                string[] conditions = originCondition.Split(',');
                string returnStr = "";
                // ' 기호 추가
                for(int i = 0; i < conditions.Length; i++)
                {
                    conditions[i] = "'" + conditions[i].Trim() + "'";
                }

                // ,로 구분하여 합산
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (i == 0)
                        returnStr = conditions[i];
                    else
                        returnStr += "," + conditions[i];
                }

                return returnStr;
            }
            else
            {
                return "'" + originCondition.Trim() + "'";
            }
        }
        #endregion

        #region Research : 재검색 (데이터 입력/수정/삭제후 동작)
        protected void Research(string workOrderID)
        {
            //InitializeInsertForm();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            if (Conditions.GetValue("EQUIPMENTNAME").ToString() != "")
                values.Add("EQUIPMENTID", _searchEquipmentID);

            if (Conditions.GetValue("p_WORKORDERSTATUS").ToString() != "")
                values.Add("WORKORDERSTATUS", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTATUS").ToString()));

            if (Conditions.GetValue("p_WORKORDERSTEP").ToString() != "")
                values.Add("WORKORDERSTEP", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTEP").ToString()));
            #endregion
            //values = Commons.CommonFunction.ConvertParameter(values);
            DataTable maintWorkOrderTable = SqlExecuter.Query("GetMaintWorkOrderListByEqp", "10001", values);

            grdMaintWorkOrder.DataSource = maintWorkOrderTable;

            if (maintWorkOrderTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                ViewSavedData(workOrderID);
            }
        }
        #endregion

        #region 조회조건제어
        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //InitializeConditionPlant();
            InitializeConditionFactory();
            InitializeConditionEquipmentIDPopup();
            //InitializeConditionMaintOrderStatus();
            //InitializeConditionMaintOrderStep();
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region InitializeConditionPlant : 사이트 검색조건
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANTBLANK")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #region InitializeConditionFactory : 공장 검색조건
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionFactory()
        {
            var planttxtbox = Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
               .SetLabel("FACTORY")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.2)
               .SetEmptyItem("", "", true)
               .SetRelationIds("P_PLANTID")
            ;
        }
        #endregion

        #region InitializeConditionEquipmentIDPopup : 설비조회
        /// <summary>
        /// 설비그룹조회설정
        /// </summary>
        private void InitializeConditionEquipmentIDPopup()
        {
            equipmentSearchPopup = Conditions.AddSelectPopup("EQUIPMENTNAME", new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
            .SetPopupLayout("EQUIPMENTNAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("EQUIPMENTNAME", "EQUIPMENTNAME")
            .SetLabel("EQUIPMENT")
            .SetPopupResultCount(1)
            .SetPosition(0.3)
            .SetRelationIds("P_PLANTID")
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
            })
            ;

            // 팝업 조회조건            
            //equipmentSearchPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "PLANTID", "PLANTID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PLANTID")
            //    .SetDefault(UserInfo.Current.Plant, "PLANTID")
            //    .SetIsReadOnly(true)
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;

            //equipmentPopup.Conditions.AddComboBox("AREAID", new SqlQuery("GetAreaListByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } }), "AREANAME", "AREAID")
            //   .SetEmptyItem("", "", true)
            //   .SetLabel("AREAID")
            //   .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //   ;
            equipmentSearchPopup.Conditions.AddTextBox("EQUIPMENTNAME")
                .SetLabel("EQUIPMENTNAME");

            InitializeReceiptAreaPopupForPopup(equipmentSearchPopup.Conditions);

            equipmentSearchPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
               .SetEmptyItem("", "", true)
               .SetLabel("PROCESSSEGMENTCLASS")
               .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
               ;
            //skPark 수정
            equipmentSearchPopup.Conditions.AddComboBox("SEPARATOR", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Separator", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetEmptyItem("", "", true)
               .SetLabel("SEPARATOR")
               .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
               ;

            equipmentSearchPopup.Conditions.AddTextBox("AREAID")
                .SetLabel("AREAID")
                .SetIsHidden()
                ;

            // 팝업 그리드
            equipmentSearchPopup.IsMultiGrid = false;
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120)
                .SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250)
                .SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("FACTORYID", 150)
              .SetIsHidden();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddComboBoxColumn("SEPARATOR", 100
                , new SqlQuery("GetCodeList", "00001", "CODECLASSID=Separator", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();
           
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("EQUPMENTLEVEL", 200)
                .SetIsReadOnly()
                .SetIsHidden();
        }
        #endregion

        #region InitializeConditionMaintOrderStatus : 진행상태를 조회한다.
        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionMaintOrderStatus()
        {
            var planttxtbox = Conditions.AddComboBox("WORKORDERSTATUS", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=MaintOrderStatus"), "CODENAME", "CODEID")
               .SetLabel("MAINTORDERSTATUS")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(1.1)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionMaintOrderStep : 진행단계를 조회한다.
        /// <summary>
        /// 작업장 설정 
        /// </summary>
        private void InitializeConditionMaintOrderStep()
        {
            var planttxtbox = Conditions.AddComboBox("WORKORDERSTEP", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=MaintOrderStep"), "CODENAME", "CODEID")
               .SetLabel("MAINTORDERSTEP")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(1.2)
               .SetEmptyItem("", "", true)
            ;
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

        #region ValidateCurrentStatus : 현재 저장요청한 정보가 저장 가능한 정보인지 검증
        private bool ValidateCurrentStatus()
        {
            return true;
        }
        #endregion

        #region ValidateContent : 저장가능한 정보인지 체크
        private bool ValidateContent(out string messageCode)
        {
            messageCode = "";

            //if (!ValidateEditValue(txtWorkNo.EditValue))
            //{
            //    messageCode = "ValidateRequiredData";
            //    return false;
            //}

            if(!_currentStatus.Equals("added"))
            {
                messageCode = "ValidateRequestMaintWorkOrderUpdate";
                return false;
            }

            if (!ValidateEditValue(popEditRequestUser.EditValue))
            {
                messageCode = "ValidateSetRequestUser";
                return false;
            }

            //if (!ValidateEditValue(txtRequestDepartment.EditValue))
            //{
            //    messageCode = "ValidateSetRequestUser";
            //    return false;
            //}

            if (!ValidateEditValue(popEditEquipmentCode.EditValue))
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

            if (!ValidateEditValue(txtRequestComment.EditValue))
            {
                messageCode = "ValidateRequiredData";
                return false;
            }

            return true;
        }
        #endregion

        #region ValidateComboBoxEqualValue - 2개의 콤보박스의 데이터비교
        private bool ValidateComboBoxEqualValue(SmartComboBox originBox, SmartComboBox targetBox)
        {
            //두 콤보박스의 값이 같으면 안된다.
            if (originBox.EditValue.Equals(targetBox.EditValue))
                return false;
            return true;
        }
        #endregion

        #region ValidateNumericBox - 숫자형 텍스트박스, 기준숫자와 비교
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

        #region ValidateEditValue - 특정 입력값의 Validation
        private bool ValidateEditValue(object editValue)
        {
            if (editValue == null)
                return false;

            if (editValue.ToString() == "")
                return false;

            return true;
        }
        #endregion

        #region ValidateCellInGrid - 그리드의 특정컬럼의 Validation
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
        #region InitializeInsertForm : 입고등록하기 위한 화면 초기화
        /// <summary>
        /// 입고등록정보를 입력하기 위해 화면을 초기화 한다.
        /// </summary>
        private void InitializeInsertForm()
        {
            try
            {
                _currentStatus = "added";           //현재 화면의 상태는 입력중이어야 한다.

                txtAreaName.EditValue = "";
                txtEmergencyCotent.EditValue = "";
                txtEquipmentGrade.EditValue = "";
                txtEquipmentName.EditValue = "";
                txtProcessSegment.EditValue = "";
                txtRequestComment.EditValue = "";
                txtRequestDepartment.EditValue = "";
                txtWorkNo.EditValue = "";
                popEditEquipmentCode.EditValue = null;
                popEditEquipmentCode.Text = "";
                popEditRequestUser.EditValue = null;
                popEditRequestUser.Text = "";

                _repairUserID = "";

                txtRequestDepartment.EditValue = UserInfo.Current.Department;
                _requestUserID = UserInfo.Current.Id;
                popEditRequestUser.EditValue = UserInfo.Current.Name;

                //의뢰일은 오늘날짜를 기본으로 한다.
                cboDownType.EditValue = null;
                cboDownCode.EditValue = null;
                chkCauseEquipmentDown.Checked = false;
                chkIsEmergency.Checked = false;

                //컨트롤 접근여부는 작성상태로 변경한다.
                ControlEnableProcess("added");

                popEditEquipmentCode.Refresh();
                popEditRequestUser.Refresh();
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
                txtWorkNo.ReadOnly = true;
                txtEquipmentGrade.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtAreaName.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
                txtEmergencyCotent.ReadOnly = false;
                txtRequestComment.ReadOnly = false;

                popEditEquipmentCode.ReadOnly = false;
                popEditRequestUser.ReadOnly = false;

                cboDownType.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                chkCauseEquipmentDown.ReadOnly = false;
                chkIsEmergency.ReadOnly = false;
            }
            else if (currentStatus == "modified") //
            {
                txtWorkNo.ReadOnly = true;
                txtEquipmentGrade.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtAreaName.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
                txtEmergencyCotent.ReadOnly = false;
                txtRequestComment.ReadOnly = false;

                popEditEquipmentCode.ReadOnly = false;
                popEditRequestUser.ReadOnly = false;

                cboDownType.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                chkCauseEquipmentDown.ReadOnly = false;
                chkIsEmergency.ReadOnly = false;
            }
            else
            {
                txtWorkNo.ReadOnly = true;
                txtEquipmentGrade.ReadOnly = true;
                txtEquipmentName.ReadOnly = true;
                txtProcessSegment.ReadOnly = true;
                txtAreaName.ReadOnly = true;
                txtRequestDepartment.ReadOnly = true;
                txtEmergencyCotent.ReadOnly = false;
                txtRequestComment.ReadOnly = false;

                popEditEquipmentCode.ReadOnly = false;
                popEditRequestUser.ReadOnly = false;

                cboDownType.ReadOnly = false;
                cboDownCode.ReadOnly = false;
                chkCauseEquipmentDown.ReadOnly = false;
                chkIsEmergency.ReadOnly = false;
            }
        }
        #endregion

        #region CreateSaveDatatable : ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// <summary>
        /// ToolRequest 입력/수정/삭제를 위한 DataTable의 Template 생성
        /// 입력시에는 ToolRequest와 ToolRequestDetail에 입력하고
        /// 수정시에는 ToolRequest와 ToolRequestDetail 및 ToolRequestDetailLot에 까지 데이터를 기입한다.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "maintWorkOrderList";
            //===================================================================================
            dt.Columns.Add("WORKORDERID");
            dt.Columns.Add("WORKORDERNAME");
            dt.Columns.Add("WORKORDERSTEP");
            dt.Columns.Add("WORKORDERSTATUS");
            dt.Columns.Add("REQUESTUSER");
            dt.Columns.Add("REQUESTDEPARTMENT");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("EQUIPMENTNAME");
            dt.Columns.Add("WORKORDERTYPE");
            dt.Columns.Add("MAINTTYPEID");
            dt.Columns.Add("PLANDATE");
            dt.Columns.Add("DOWNTYPE");
            dt.Columns.Add("DOWNTYPENAME");
            dt.Columns.Add("DOWNCODE");
            dt.Columns.Add("REQUESTTIME");
            dt.Columns.Add("REQUESTCOMMENTS");
            dt.Columns.Add("ISEMERGENCY");
            dt.Columns.Add("ISEMERGENCYREASON");
            dt.Columns.Add("ISEQUIPMENTDOWN");
            
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
        #endregion

        #region SaveData : 입력/수정을 수행
        private void SaveData()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                btnInitialize.Enabled = false;
                btnModify.Enabled = false;
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent(out messageCode))
                {
                    DataSet maintWorkOrderSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable maintWorkOrderTable = CreateSaveDatatable();
                    DataRow orderRow = maintWorkOrderTable.NewRow();

                    DateTime requestDate = DateTime.Now;

                    orderRow["REQUESTTIME"] = requestDate.ToString("yyyy-MM-dd HH:mm:ss");
                    orderRow["PLANDATE"] = requestDate.ToString("yyyy-MM-dd");

                    orderRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    orderRow["PLANTID"] = Conditions.GetValue("P_PLANTID");

                    orderRow["WORKORDERID"] = "";
                    orderRow["WORKORDERNAME"] = "";

                    orderRow["EQUIPMENTID"] = popEditEquipmentCode.EditValue;
                    orderRow["EQUIPMENTNAME"] = txtEquipmentName.EditValue;

                    orderRow["REQUESTDEPARTMENT"] = txtRequestDepartment.EditValue;
                    orderRow["REQUESTUSER"] = _requestUserID;

                    orderRow["DOWNTYPE"] = cboDownType.EditValue;
                    orderRow["DOWNTYPENAME"] = cboDownType.GetDisplayText();
                    orderRow["DOWNCODE"] = cboDownCode.EditValue;

                    orderRow["ISEQUIPMENTDOWN"] = chkCauseEquipmentDown.Checked ? "Y" : "N";
                    orderRow["REQUESTCOMMENTS"] = txtRequestComment.EditValue;
                    orderRow["ISEMERGENCY"] = chkIsEmergency.Checked ? "Y" : "N";
                    orderRow["ISEMERGENCYREASON"] = txtEmergencyCotent.EditValue;

                    orderRow["VALIDSTATE"] = "Valid";

                    
                    orderRow["CREATOR"] = UserInfo.Current.Id;
                    orderRow["MODIFIER"] = UserInfo.Current.Id;
                    orderRow["_STATE_"] = "added";
                    

                    maintWorkOrderTable.Rows.Add(orderRow);

                    maintWorkOrderSet.Tables.Add(maintWorkOrderTable);

                    DataTable resultTable = ExecuteRule<DataTable>("RequestMaintWorkOrder", maintWorkOrderSet);

                    ////Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
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
                this.CloseWaitArea();
                btnInitialize.Enabled = true;
                btnModify.Enabled = true;
            }
        }
        #endregion

        #region DeleteData : 삭제를 수행
        private void DeleteData()
        {
            
        }
        #endregion

        #region DisplayMaintWorkOrderInfo : 그리드에서 행 선택시 상세정보를 표시
        /// <summary>
        /// 그리드에서 행 선택시 상세정보를 표시하는 메소드
        /// </summary>
        private void DisplayMaintWorkOrderInfo()
        {
            //포커스 행 체크 
            if (grdMaintWorkOrder.View.FocusedRowHandle < 0) return;

            DisplayMaintWorkOrderInfoDetail(grdMaintWorkOrder.View.GetFocusedDataRow());
        }
        #endregion

        #region ViewSavedData : 새로 입력된 데이터를 화면에 바인딩
        /// <summary>
        /// 새로 입력된 데이터를 화면에 바인딩
        /// </summary>
        private void ViewSavedData(string workOrderID)
        {
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("WORKORDERID", workOrderID);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable savedResult = SqlExecuter.Query("GetMaintWorkOrderListByEqp", "10001", values);

            if (savedResult.Rows.Count > 0)
            {
                grdMaintWorkOrder.View.FocusedRowHandle = GetRowHandleInGrid(grdMaintWorkOrder, "WORKORDERID", workOrderID);

                DisplayMaintWorkOrderInfoDetail(savedResult.Rows[0]);
            }
        }
        #endregion

        #region DisplayMaintWorkOrderInfoDetail : 입력받은 인덱스의 그리드의 내용을 화면에 바인딩
        /// <summary>
        /// 입력받은 그리드내의 인덱스에 대한 내용을 화면에 표시한다.
        /// </summary>
        /// <param name="rowHandle"></param>
        private void DisplayMaintWorkOrderInfoDetail(DataRow workOrderInfo)
        {
            //해당 업무에 맞는 Enable체크 수행
            txtAreaName.EditValue = workOrderInfo.GetString("AREANAME");
            txtEmergencyCotent.EditValue = workOrderInfo.GetString("EMERGENCYREASON");
            txtEquipmentGrade.EditValue = "";// workOrderInfo.GetString("AREANAME");
            txtEquipmentName.EditValue = workOrderInfo.GetString("EQUIPMENTNAME");
            txtProcessSegment.EditValue = workOrderInfo.GetString("PROCESSSEGMENTCLASS");
            txtRequestComment.EditValue = workOrderInfo.GetString("REQUESTCOMMENT");
            txtRequestDepartment.EditValue = workOrderInfo.GetString("REQUESTDEPARTMENT");
            txtWorkNo.EditValue = workOrderInfo.GetString("WORKORDERID");
            popEditEquipmentCode.EditValue = workOrderInfo.GetString("EQUIPMENTID");
            popEditEquipmentCode.Text = workOrderInfo.GetString("EQUIPMENTID");
            popEditRequestUser.EditValue = workOrderInfo.GetString("REQUESTUSER");
            popEditRequestUser.Text = workOrderInfo.GetString("REQUESTUSER");

            _repairUserID = workOrderInfo.GetString("AREANAME");
            //의뢰일은 오늘날짜를 기본으로 한다.
            cboDownType.EditValue = workOrderInfo.GetString("DOWNTYPEID");
            cboDownCode.EditValue = workOrderInfo.GetString("DOWNCODEID");
            chkCauseEquipmentDown.Checked = workOrderInfo.GetString("ISEQUIPMENTDOWNREQUEST").Equals("Y");
            chkIsEmergency.Checked = workOrderInfo.GetString("ISEMERGENCY").Equals("Y");

            //수정상태라 판단하여 화면 제어
            ControlEnableProcess("modified");

            _currentStatus = "modified";
            popEditEquipmentCode.Refresh();
            popEditRequestUser.Refresh();

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
        #endregion

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            //if (popupGridToolArea != null)
            //    popupGridToolArea.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (Conditions.GetCondition("EQUIPMENTNAME") != null)
                ((ConditionItemSelectPopup)((ConditionItemSelectPopup)Conditions.GetCondition("EQUIPMENTNAME")).Conditions.GetCondition("AREANAME")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (equipmentPopup != null)
            {
                equipmentPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");
                ((ConditionItemSelectPopup)equipmentPopup.Conditions.GetCondition("AREANAME")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", "ISMOD=Y");
            }

            if (userPopup != null)
                userPopup.SearchQuery = new SqlQuery("GetUserListForPopupByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

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
            return caption.Length * 20;
        }
        #endregion
        #endregion
    }
}
