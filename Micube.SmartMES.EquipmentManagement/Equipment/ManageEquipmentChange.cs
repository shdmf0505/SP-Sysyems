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

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > 설비보전 > 설비보전이력관리
    /// 업  무  설  명  : 설비의 이력을 관리한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2020-01-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ManageEquipmentChange : SmartConditionManualBaseForm
    {
        #region Local Variables
        ConditionItemSelectPopup equipmentPopup;
        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup areaSearchCondition;
        ConditionItemSelectPopup equipmentSearchPopup;
        #endregion

        #region 생성자

        public ManageEquipmentChange()
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

        #region InitializeGrid - 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdEqpHistory.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete | GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            

            grdEqpHistory.View.AddDateEditColumn("FINISHEDTIME", 160)             //설명
              .SetDisplayFormat("yyyy-MM-dd", MaskTypes.DateTime)
              .SetIsReadOnly(false);
            grdEqpHistory.View.AddTextBoxColumn("TXNHISTKEY", 200)             //의뢰일자
                .SetIsReadOnly(true)   
                .SetIsHidden()
                ;
            grdEqpHistory.View.AddTextBoxColumn("EQUIPMENTID", 100)           //의뢰순번                
                .SetValidationIsRequired()
                ;
            InitializeEquipmentNo();
            //grdEqpHistory.View.AddTextBoxColumn("EQUIPMENTNAME", 60)            //팝업
            //    .SetTextAlignment(TextAlignment.Center)
            //    ;                                                     //제작구분명
            grdEqpHistory.View.AddTextBoxColumn("AREAID", 100)             //설명
                .SetIsReadOnly(true)
                ;
            grdEqpHistory.View.AddTextBoxColumn("AREANAME", 180)             //설명
                .SetIsReadOnly(true)
                ;
            grdEqpHistory.View.AddTextBoxColumn("REQUESTDEPARTMENTNAME", 180)      //의뢰부서
                ;
            grdEqpHistory.View.AddTextBoxColumn("REQUESTUSER", 120)             //의뢰자
                ;
            grdEqpHistory.View.AddTextBoxColumn("PROCESSDEPARTMENT", 180)      //진행부서
                ;
            grdEqpHistory.View.AddTextBoxColumn("CHANGETYPENAME", 120)           //변경구분
                ;
            grdEqpHistory.View.AddSpinEditColumn("AMOUNTCURRENCY", 120)             //변경구분
                ;
            grdEqpHistory.View.AddTextBoxColumn("COMMENTS", 200)             //변경구분
                ;

            grdEqpHistory.View.AddTextBoxColumn("CREATEDTIME", 160)             //설명
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetIsReadOnly(true);
            grdEqpHistory.View.AddTextBoxColumn("CREATOR", 100)             //설명
                .SetIsReadOnly(true);           
            grdEqpHistory.View.AddTextBoxColumn("MODIFIER", 100)             //설명
                .SetIsReadOnly(true)
                .SetIsHidden()
                ;
            grdEqpHistory.View.AddTextBoxColumn("ISMODIFY", 100)             //설명
                .SetIsHidden()
                ;
            grdEqpHistory.View.PopulateColumns();
        }
        #endregion

        #region InitializeEquipmentNo - 설비조회화면설정
        /// <summary>
        /// 결재지정자를 조회
        /// </summary>
        private void InitializeEquipmentNo()
        {
            equipmentPopup = grdEqpHistory.View.AddSelectPopupColumn("EQUIPMENTNAME", 180, new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"));

            equipmentPopup.SetPopupLayout("EQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false);
            equipmentPopup.SetPopupResultCount(1);
            equipmentPopup.SetPopupLayoutForm(1000, 600, FormBorderStyle.FixedToolWindow);
            equipmentPopup.SetPopupAutoFillColumns("EQUIPMENTNAME");
            equipmentPopup.SetPopupResultMapping("EQUIPMENTNAME", "EQUIPMENTNAME");
            equipmentPopup.ValueFieldName = "EQUIPMENTNAME";
            equipmentPopup.DisplayFieldName = "EQUIPMENTNAME";
            equipmentPopup.SetValidationIsRequired();
            equipmentPopup.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                foreach (DataRow row in selectedRows)
                {
                    grdEqpHistory.View.SetFocusedRowCellValue("EQUIPMENTID", row["EQUIPMENTID"]);
                    grdEqpHistory.View.SetFocusedRowCellValue("AREAID", row["AREAID"]);
                    grdEqpHistory.View.SetFocusedRowCellValue("AREANAME", row["AREANAME"]);
                }
            });

            // 팝업 조회조건            
            //equipmentPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "PLANTID", "PLANTID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PLANTID")
            //    .SetDefault(UserInfo.Current.Plant, "PLANTID")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;

            //equipmentPopup.Conditions.AddComboBox("AREAID", new SqlQuery("GetAreaListByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } }), "AREANAME", "AREAID")
            //   .SetEmptyItem("", "", true)
            //   .SetLabel("AREAID")
            //   .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //   ;

            equipmentPopup.Conditions.AddTextBox("EQUIPMENTNAME")
                .SetLabel("EQUIPMENTNAME");

            InitializeReceiptAreaPopupForPopup(equipmentPopup.Conditions);

            equipmentPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
               .SetEmptyItem("", "", true)
               .SetLabel("PROCESSSEGMENTCLASS")
               .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
               ;


            // 팝업 그리드
            equipmentPopup.IsMultiGrid = false;
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250)
                .SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("FACTORYID", 150)
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
        }
        #endregion

        #region InitializeReceiptAreaPopup : 입고작업장 팝업창
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

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                ;
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        #region InitializeFileControl - 파일업로드 컨트롤 초기화
        private void InitializeFileControl(string resourceID, string resourceVersion)
        {
            grdAttachment.UploadPath = "";
            grdAttachment.Resource = new ResourceInfo()
            {
                Type = "EquipmentChange",
                Id = resourceID,
                Version = resourceVersion
            };
            grdAttachment.UseCommentsColumn = true;
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            Shown += ManageEquipmentChange_Shown;
            grdEqpHistory.View.FocusedRowChanged += grdEqpHistory_FocusedRowChanged;
            grdEqpHistory.View.AddingNewRow += grdEqpHistory_AddingNewRow;
            grdEqpHistory.View.CellValueChanged += grdEqpHistory_CellValueChanged;
        }

        #region grdEqpHistory_CellValueChanged - 그리디의 셀값 변경이벤트
        private void grdEqpHistory_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "FINISHEDTIME")
            {
                grdEqpHistory.View.CellValueChanged -= grdEqpHistory_CellValueChanged;

                DataRow row = grdEqpHistory.View.GetFocusedDataRow();

                if (row["FINISHEDTIME"].ToString().Equals(""))
                {
                    grdEqpHistory.View.CellValueChanged += grdEqpHistory_CellValueChanged;
                    return;
                }
                DateTime dateBudget = Convert.ToDateTime(row["FINISHEDTIME"].ToString());
                grdEqpHistory.View.SetFocusedRowCellValue("FINISHEDTIME", dateBudget.ToString("yyyy-MM-dd"));// 예산일자
                grdEqpHistory.View.CellValueChanged += grdEqpHistory_CellValueChanged;
            }
        }
        #endregion

        #region ManageEquipmentChange_Shown - 화면로딩후 이벤트
        private void ManageEquipmentChange_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ManageEquipmentChange_EditValueChanged;
        }
        #endregion

        #region ManageEquipmentChange_EditValueChanged - 조회조건(Site) 데이터 변경이벤트
        private void ManageEquipmentChange_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region grdEqpHistory_AddingNewRow - 그리드의 행추가이벤트
        private void grdEqpHistory_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["REQUESTDEPARTMENTNAME"] = UserInfo.Current.Department;
            args.NewRow["REQUESTUSER"] = UserInfo.Current.Name;
            args.NewRow["TXNHISTKEY"] = Guid.NewGuid().ToString("N");
            InitializeFileControl(args.NewRow.GetString("TXNHISTKEY"), "*");
            AttachmentSearch();
        }
        #endregion

        #region grdEqpHistory_FocusedRowChanged - 그리드의 행 변경이벤트
        private void grdEqpHistory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                InitializeFileControl(grdEqpHistory.View.GetDataRow(e.FocusedRowHandle).GetString("TXNHISTKEY"), "*");
                AttachmentSearch();
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
            SaveData();
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

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            DataTable eqpTable = SqlExecuter.Query("GetEquipmentHistoryByEqp", "10001", values);

            grdEqpHistory.DataSource = eqpTable;

            if (eqpTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdEqpHistory.View.FocusedRowHandle = 0;
                InitializeFileControl(grdEqpHistory.View.GetFocusedDataRow().GetString("TXNHISTKEY"), "*");
                AttachmentSearch();
            }
            //_currentStatus = "modified";
        }
        #endregion

        #region ReSearch - 재검색
        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected void ReSearch()
        {   
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            DataTable eqpTable = SqlExecuter.Query("GetEquipmentHistoryByEqp", "10001", values);

            grdEqpHistory.DataSource = eqpTable;

            if (eqpTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdEqpHistory.View.FocusedRowHandle = 0;
                InitializeFileControl(grdEqpHistory.View.GetFocusedDataRow().GetString("TXNHISTKEY"), "*");
                AttachmentSearch();
            }
            //_currentStatus = "modified";
        }
        #endregion

        #region AttachmentSearch : 첨부파일검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        void AttachmentSearch()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_RESOURCETYPE", grdAttachment.Resource.Type);
            Param.Add("P_RESOURCEID", grdAttachment.Resource.Id);
            Param.Add("P_RESOURCEVERSION", grdAttachment.Resource.Version);

            DataTable objectFileTable = this.Procedure("usp_com_selectObjectFile", Param);

            grdAttachment.DataSource = objectFileTable;
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            InitializeConditionFactory();
            InitializeConditionEquipmentIDPopup();
            InitializeReceiptArea();
            InitializeConditionEquipmentClassTypePopup();
            //InitializeConditionEquipmentGrade();
            //InitializeConditionEquipmentGroupType();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region InitializeConditionFactory : 공장 검색조건
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionFactory()
        {
            var planttxtbox = Conditions.AddComboBox("FACTORYID", new SqlQuery("GetFactoryListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "FACTORYNAME", "FACTORYID")
               .SetLabel("FACTORY")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetEmptyItem("", "", true)
               .SetRelationIds("P_PLANTID")
            ;
        }
        #endregion

        #region InitializeConditionEquipmentGrade : 설비구분
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionEquipmentGrade()
        {
            var planttxtbox = Conditions.AddComboBox("EQUIPMENTSEPARATOR", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=Separator"), "CODENAME", "CODEID")
               .SetLabel("SEPARATOR")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(1.5)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeConditionEquipmentGroupType : 설비그룹유형
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionEquipmentGroupType()
        {
            var planttxtbox = Conditions.AddComboBox("EQUIPMENTGROUPTYPE", new SqlQuery("GetTypeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=EquipmentType"), "CODENAME", "CODEID")
               .SetLabel("EQUIPMENTTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.9)
               .SetEmptyItem("", "", true)
            ;
        }
        #endregion

        #region InitializeReceiptArea : 작업장 검색조건
        /// <summary>
        /// 작업장 검색조건
        /// </summary>
        private void InitializeReceiptArea()
        {
            areaSearchCondition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
            .SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true)
            .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
            .SetPopupAutoFillColumns("AREANAME")
            .SetLabel("AREA")
            .SetPopupResultCount(1)
            .SetPosition(0.2)
            .SetPopupResultMapping("AREANAME", "AREANAME")
            .SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                   // _searchAreaID = row.GetString("AREAID");
                });

            });

            areaSearchCondition.DisplayFieldName = "AREANAME";
            areaSearchCondition.ValueFieldName = "AREAID";

            areaSearchCondition.Conditions.AddTextBox("AREANAME");

            areaSearchCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            areaSearchCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion

        #region InitializeConditionEquipmentIDPopup : 설비조회
        /// <summary>
        /// 설비그룹조회설정
        /// </summary>
        private void InitializeConditionEquipmentIDPopup()
        {
            equipmentSearchPopup = Conditions.AddSelectPopup("EQUIPMENTID", new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
            .SetPopupLayout("EQUIPMENTNAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("EQUIPMENTNAME", "EQUIPMENTNAME")
            .SetLabel("EQUIPMENT")
            .SetPopupResultCount(1)
            .SetPosition(2)
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        //_searchEquipmentID = row["EQUIPMENTID"].ToString();
                    }
                    i++;
                }
            })
            ;

            equipmentSearchPopup.DisplayFieldName = "EQUIPMENTNAME";
            equipmentSearchPopup.ValueFieldName = "EQUIPMENTID";

            // 팝업 조회조건
            equipmentSearchPopup.Conditions.AddTextBox("EQUIPMENTNAME")
                .SetLabel("EQUIPMENTNAME");

            //equipmentSearchPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "PLANTID", "PLANTID")
            //   .SetEmptyItem("", "", true)
            //   .SetLabel("PLANTID")
            //   .SetDefault(UserInfo.Current.Plant, "PLANTID")
            //   .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //   ;

            //equipmentPopup.Conditions.AddComboBox("AREAID", new SqlQuery("GetAreaListByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } }), "AREANAME", "AREAID")
            //   .SetEmptyItem("", "", true)
            //   .SetLabel("AREAID")
            //   .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //   ;

            InitializeReceiptAreaPopupForPopup(equipmentSearchPopup.Conditions);

            equipmentSearchPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
               .SetEmptyItem("", "", true)
               .SetLabel("PROCESSSEGMENTCLASS")
               .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
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
           
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetIsReadOnly();
            equipmentSearchPopup.GridColumns.AddTextBoxColumn("EQUPMENTLEVEL", 200)
                .SetIsReadOnly()
                .SetIsHidden();

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
            .SetPosition(1)
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                //int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    //if (i == 0)
                    //{
                    //    _searchEquipmentClassID = row["EQUIPMENTCLASSID"].ToString();
                    //}
                    //i++;
                }
            })
            ;

            eqpPopup.ValueFieldName = "EQUIPMENTCLASSID";
            eqpPopup.DisplayFieldName = "EQUIPMENTCLASSNAME";

            eqpPopup.SetRelationIds("P_PLANTID");

            // 팝업 조회조건
            eqpPopup.Conditions.AddTextBox("EQUIPMENTCLASSNAME")
                .SetLabel("EQUIPMENTCLASSNAME");
            eqpPopup.Conditions.AddComboBox("EQUIPMENTCLASSTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "CODECLASSID", "EquipmentClassType" } }), "CODENAME", "CODEID")
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

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #region ValidationContent
        private bool ValidateContent(out string messageCode)
        {
            messageCode = "";

            DataTable changedTable = grdEqpHistory.GetChangedRows();

            foreach (DataRow changedRow in changedTable.Rows)
            {
                if (changedRow.GetString("EQUIPMENTID").Equals(""))
                {
                    messageCode = "InValidRequiredField";//{0} 항목은 필수 입력입니다
                    return false;
                }
            }

            return true;
        }
        #endregion
        #endregion

        #region Private Function
        #region CreateSaveDatatable : 설비수리실적등록
        private DataTable CreateSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            //===================================================================================
            dt.Columns.Add("TXNHISTKEY");
            dt.Columns.Add("EQUIPMENTID");
            dt.Columns.Add("REQUESTDEPARTMENT");
            dt.Columns.Add("REQUESTUSER");
            dt.Columns.Add("PROCESSDEPARTMENT");
            dt.Columns.Add("CHANGETYPE");
            dt.Columns.Add("AMOUNT");
            dt.Columns.Add("FINISHDATE");
            dt.Columns.Add("DESCRIPTION");

            dt.Columns.Add("CREATOR");
            dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("MODIFIER");
            dt.Columns.Add("MODIFIEDTIME");
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
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (ValidateContent(out messageCode))
                {
                    DataSet eqpSet = new DataSet();
                    //치공구 제작의뢰를 입력
                    DataTable eqpTable = CreateSaveDatatable();

                    #region 내역 입력

                    if (grdEqpHistory.DataSource != null)
                    {
                        DataTable changedTable = grdEqpHistory.GetChangedRows();

                        foreach (DataRow changedRow in changedTable.Rows)
                        {
                            DataRow eqpRow = eqpTable.NewRow();

                            eqpRow["EQUIPMENTID"] = changedRow.GetString("EQUIPMENTID");
                            eqpRow["REQUESTDEPARTMENT"] = changedRow.GetString("REQUESTDEPARTMENTNAME");
                            eqpRow["REQUESTUSER"] = changedRow.GetString("REQUESTUSER");
                            eqpRow["PROCESSDEPARTMENT"] = changedRow.GetString("PROCESSDEPARTMENT");
                            eqpRow["CHANGETYPE"] = changedRow.GetString("CHANGETYPENAME");
                            eqpRow["AMOUNT"] = changedRow.GetString("AMOUNTCURRENCY");
                            eqpRow["DESCRIPTION"] = changedRow.GetString("COMMENTS");

                            if(!changedRow.GetString("FINISHEDTIME").Equals(""))
                                eqpRow["FINISHDATE"] = Convert.ToDateTime(changedRow.GetString("FINISHEDTIME")).ToString("yyyy-MM-dd HH:mm:ss");

                            if (changedRow.GetString("_STATE_").Equals("added"))
                            {
                                eqpRow["_STATE_"] = "added";
                                eqpRow["TXNHISTKEY"] = changedRow.GetString("TXNHISTKEY");
                                eqpRow["CREATOR"] = UserInfo.Current.Id;
                            }                            
                            else
                            {
                                eqpRow["_STATE_"] = changedRow.GetString("_STATE_"); //modified || deleted
                                eqpRow["TXNHISTKEY"] = changedRow.GetString("TXNHISTKEY");
                                eqpRow["MODIFIER"] = UserInfo.Current.Id;
                            }

                            eqpTable.Rows.Add(eqpRow);
                        }
                    }

                    eqpSet.Tables.Add(eqpTable);
                    #endregion


                    ExecuteRule<DataTable>("EquipmentChange", eqpSet);

                    //Server에서 결과값을 현재는 반환하지 않음 만약 반환해야 한다면 아래로직 구현
                    //DataRow resultRow = resultTable.Rows[0];
                    //ControlEnableProcess("modified");

                    ////가져온 결과값에 따라 재검색한 후 가져온 아이디에 맞는 행을 선택으로 변경한다.
                    SaveAttachment();
                    ReSearch();
                }
                else
                {
                    ShowMessage(MessageBoxButtons.OK, messageCode, Language.Get("EQUIPMENTID"));
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

        #region SaveAttachment : 첨부파일 입력
        private void SaveAttachment()
        {
            //Validation 체크 부분 작성 필요
            this.ShowWaitArea();

            //저장 로직
            try
            {
                string messageCode = "";
                //Validation Check시 입력/수정시 검사할 값이 다르다면 수정필요
                if (grdEqpHistory.View != null)
                {
                    if (grdEqpHistory.View.GetFocusedDataRow() != null)
                    {

                        #region 첨부파일 입력
                        //데이터 저장전 첨부파일의 저장을 진행
                        if (grdAttachment.Resource.Type.Equals("EquipmentChange") && grdAttachment.Resource.Id.Equals(grdEqpHistory.View.GetFocusedDataRow().GetString("TXNHISTKEY")))
                        {
                            if (grdAttachment.GetChangedRows().Rows.Count > 0)
                            {
                                grdAttachment.SaveChangedFiles();

                                DataTable changed = grdAttachment.GetChangedRows();

                                ExecuteRule("SaveObjectFile", changed);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        ShowMessage(MessageBoxButtons.OK, messageCode, "");
                    }
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

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (equipmentPopup != null)
                equipmentPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (areaSearchCondition != null)
                areaSearchCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");
            
            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}", $"ISMOD=Y");

            if (equipmentSearchPopup != null)
                equipmentSearchPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue();
        }
        #endregion
        #endregion
    }
}