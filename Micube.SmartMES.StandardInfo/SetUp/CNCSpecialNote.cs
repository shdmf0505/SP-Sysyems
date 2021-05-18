#region using
using Micube.Framework.Net;
using Micube.Framework;
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
using DevExpress.XtraCharts;
using DevExpress.Utils;
using Micube.Framework.SmartControls.Validations;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 품목유형 등록 및 조회
    /// 업 무 설명 : 품목 유형등록 
    /// 생  성  자 : 윤성원
    /// 생  성  일 : 2019-06-27
    /// 수정 이 력 : 
    /// 
    /// 
    /// </summary> 
	public partial class CNCSpecialNote : SmartConditionManualBaseForm
    {
        #region Local Variables
       
        #endregion

        #region 생성자
        public CNCSpecialNote()
        {
            InitializeComponent();
            InitializeEvent();
            
        }
        #endregion


        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {

            base.InitializeCondition();
           

            //Conditions.AddComboBox("STATUS", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=Status", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            InitializeCondition_Popup();

            //InitializeCondition_Popup();
            // 버전
            //Conditions.AddComboBox("ITEMVERSION", new SqlQuery("GetItemVersion", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}")).SetValidationIsRequired();


        }
        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControl()
        {
            // 관리유형
            //cboGOVERNANCETYPE.DisplayMember = "CODENAME";
            //cboGOVERNANCETYPE.ValueMember = "CODEID";
            //cboGOVERNANCETYPE.ShowHeader = false;
            //Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            //ParamRt.Add("CODECLASSID", "GovernanceType");
            //ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtValidState = SqlExecuter.Query("GetCodeList", "00001", ParamRt);
            //cboGOVERNANCETYPE.DataSource = dtValidState;


            //생산구분
            cboPRODUCTCLASS.DisplayMember = "CODENAME";
            cboPRODUCTCLASS.ValueMember = "CODEID";
            cboPRODUCTCLASS.ShowHeader = false;
            Dictionary<string, object> ParamConfirmation = new Dictionary<string, object>();
            ParamConfirmation.Add("CODECLASSID", "ProductionType");
            ParamConfirmation.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtConfirmation = SqlExecuter.Query("GetCodeList", "00001", ParamConfirmation);
            cboPRODUCTCLASS.DataSource = dtConfirmation;

            // 적용구분
            //cboIMPLEMENTATIONTYPE.DisplayMember = "CODENAME";
            //cboIMPLEMENTATIONTYPE.ValueMember = "CODEID";
            //cboIMPLEMENTATIONTYPE.ShowHeader = false;
            //Dictionary<string, object> ParamIMPLEMENTATIONTYPE = new Dictionary<string, object>();
            //ParamIMPLEMENTATIONTYPE.Add("CODECLASSID", "ImplementationType");
            //ParamIMPLEMENTATIONTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtIMPLEMENTATIONTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamIMPLEMENTATIONTYPE);
            //cboIMPLEMENTATIONTYPE.DataSource = dtIMPLEMENTATIONTYPE;

            // 작업유형
            //cboPLATINGTYPE.DisplayMember = "CODENAME";
            //cboPLATINGTYPE.ValueMember = "CODEID";
            //cboPLATINGTYPE.ShowHeader = false;
            //Dictionary<string, object> ParamPLATINGTYPE = new Dictionary<string, object>();
            //ParamPLATINGTYPE.Add("CODECLASSID", "PlatingType");
            //ParamPLATINGTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtPLATINGTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamPLATINGTYPE);
            //cboPLATINGTYPE.DataSource = dtPLATINGTYPE;

            // 작업구분
            //cboWORKCLASS.DisplayMember = "CODENAME";
            //cboWORKCLASS.ValueMember = "CODEID";
            //cboWORKCLASS.ShowHeader = false;
            //Dictionary<string, object> ParamJOBCLASS = new Dictionary<string, object>();
            //ParamJOBCLASS.Add("CODECLASSID", "JobClass");
            //ParamJOBCLASS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtJOBCLASS = SqlExecuter.Query("GetCodeList", "00001", ParamJOBCLASS);
            //cboWORKCLASS.DataSource = dtJOBCLASS;



          

            //사양담당
            ConditionItemSelectPopup cisiSPECIFICATIONMAN = new ConditionItemSelectPopup();
            cisiSPECIFICATIONMAN.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiSPECIFICATIONMAN.SetPopupLayout("SPECIFICATIONMAN", PopupButtonStyles.Ok_Cancel);
            cisiSPECIFICATIONMAN.Id = "SPECIFICATIONMAN";
            cisiSPECIFICATIONMAN.LabelText = "SPECIFICATIONMAN";
            cisiSPECIFICATIONMAN.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiSPECIFICATIONMAN.IsMultiGrid = false;
            cisiSPECIFICATIONMAN.DisplayFieldName = "USERNAME";
            cisiSPECIFICATIONMAN.ValueFieldName = "USERID";
            cisiSPECIFICATIONMAN.LanguageKey = "SPECIFICATIONMAN";
            cisiSPECIFICATIONMAN.Conditions.AddTextBox("USERNAME");
            cisiSPECIFICATIONMAN.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiSPECIFICATIONMAN.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspSPECIFICATIONMAN.SelectPopupCondition = cisiSPECIFICATIONMAN;



        }

     
      

        #endregion
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {

            grdCNCSpecialNote1.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdCNCSpecialNote1.View.AddTextBoxColumn("GOVERNANCENO").SetIsHidden();
            grdCNCSpecialNote1.View.AddTextBoxColumn("GOVERNANCETYPE").SetIsHidden();
            grdCNCSpecialNote1.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdCNCSpecialNote1.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdCNCSpecialNote1.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();
            grdCNCSpecialNote1.View.AddComboBoxColumn("PARENTNOTECLASSID", 100, new SqlQuery("GetCamSpecialnoteclassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"NOTECLASSTYPE={"CNCMain"}"), "PARENTNOTECLASSNAME", "PARENTNOTECLASSID");

            grdCNCSpecialNote1.View.AddTextBoxColumn("NOTECLASSID").SetIsHidden();
            InitializeGrid_VendorListPopup();
            grdCNCSpecialNote1.View.AddTextBoxColumn("VENDORNAME", 250);
            grdCNCSpecialNote1.View.AddTextBoxColumn("PROGRESSPERSON").SetIsHidden();
            grdCNCSpecialNote1.View.AddTextBoxColumn("CAMPERSON").SetIsHidden();
            grdCNCSpecialNote1.View.AddTextBoxColumn("STATUS", 100);
            grdCNCSpecialNote1.View.AddSpinEditColumn("XAXIS",100);
            grdCNCSpecialNote1.View.AddSpinEditColumn("YAXIS", 100);
            grdCNCSpecialNote1.View.AddSpinEditColumn("ST", 100);
            grdCNCSpecialNote1.View.AddTextBoxColumn("DESCRIPTION").SetIsHidden();
            grdCNCSpecialNote1.View.PopulateColumns();


            grdCNCSpecialNote2.GridButtonItem = GridButtonItem.All;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdCNCSpecialNote2.View.AddTextBoxColumn("GOVERNANCENO").SetIsHidden();
            grdCNCSpecialNote2.View.AddTextBoxColumn("GOVERNANCETYPE").SetIsHidden();
            grdCNCSpecialNote2.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdCNCSpecialNote2.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdCNCSpecialNote2.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();

            grdCNCSpecialNote2.View.AddComboBoxColumn("PARENTNOTECLASSID", 100, new SqlQuery("GetCamSpecialnoteclassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"NOTECLASSTYPE={"CNCMain"}"), "PARENTNOTECLASSNAME", "PARENTNOTECLASSID")
            .SetIsReadOnly()
            ;
            grdCNCSpecialNote2.View.AddComboBoxColumn("NOTECLASSID", 100, new SqlQuery("GetCamSpecialnoteclassList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"NOTECLASSTYPE={"CNCMain"}"), "NOTECLASSNAME", "NOTECLASSID")
            .SetRelationIds("PARENTNOTECLASSID");


            grdCNCSpecialNote2.View.AddTextBoxColumn("DESCRIPTION", 250);

            grdCNCSpecialNote2.View.AddTextBoxColumn("VENDORID").SetIsHidden();
            grdCNCSpecialNote2.View.AddTextBoxColumn("PROGRESSPERSON").SetIsHidden();
            grdCNCSpecialNote2.View.AddTextBoxColumn("CAMPERSON").SetIsHidden();
            grdCNCSpecialNote2.View.AddTextBoxColumn("STATUS").SetIsHidden();
            grdCNCSpecialNote2.View.AddSpinEditColumn("XAXIS").SetIsHidden();
            grdCNCSpecialNote2.View.AddSpinEditColumn("YAXIS").SetIsHidden();
            grdCNCSpecialNote2.View.AddSpinEditColumn("ST").SetIsHidden();
            
            grdCNCSpecialNote2.View.PopulateColumns();


            // 특기사항그룹
            grdCamSpecIalnoteClass.GridButtonItem = GridButtonItem.All;
            //grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTECLASSTYPE").SetIsHidden();
            grdCamSpecIalnoteClass.View.AddComboBoxColumn("NOTECLASSTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=NoteClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsHidden();
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("PARENTNOTECLASSID", 100);
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTECLASSNAME", 100);
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTECLASSID", 100).SetIsHidden();
            grdCamSpecIalnoteClass.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("PLANTID").SetIsHidden();

            grdCamSpecIalnoteClass.View.AddTextBoxColumn("NOTETYPE").SetIsHidden();
            grdCamSpecIalnoteClass.View.AddTextBoxColumn("DESCRIPTION").SetIsHidden();
            grdCamSpecIalnoteClass.View.PopulateColumns();


            grdFileList.GridButtonItem = GridButtonItem.None;
         
            grdFileList.View.SetSortOrder("SEQUENCE");

            grdFileList.View.AddTextBoxColumn("FILEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILENAME", 300)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEEXT", 100)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("SAFEFILENAME", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("FILESIZE", 100)
                .SetDisplayFormat()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("SEQUENCE", 70)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("LOCALFILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("COMMENTS", 300);

            grdFileList.View.PopulateColumns();

        }

        /// <summary>
		/// 거래처 팝업
		/// </summary>
		private void InitializeGrid_VendorListPopup()
        {
            var values = Conditions.GetValues();
            string plantId = UserInfo.Current.Plant;

            var vendorPopupColumn = grdCNCSpecialNote1.View.AddSelectPopupColumn("VENDORID", new SqlQuery("GetVendorList", "10001"))
                .SetPopupLayout("SELECTVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                //.SetPopupAutoFillColumns("VENDORNAME")
                 .SetPopupApplySelection((selectedRows, dataGridRow) =>
                 {
                     // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                     // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                     foreach (DataRow row in selectedRows)
                     {
                         dataGridRow["VENDORNAME"] = row["VENDORNAME"].ToString();
                     }
                 });

            ;

            vendorPopupColumn.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "10001"), "PLANTNAME", "PLANTID")
                .SetLabel("SITE")
                .SetIsReadOnly()
                .SetDefault(plantId);
            vendorPopupColumn.Conditions.AddTextBox("VENDOR");

            vendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORID", 150)
                .SetValidationKeyColumn()
                .SetIsReadOnly();
            vendorPopupColumn.GridColumns.AddTextBoxColumn("VENDORNAME", 200)
                .SetIsReadOnly();
        }

     
        // 승인자
        private void InitializeGrid_ApprovepersonPopup()
        {

            var parentUser = this.grdCNCSpecialNote2.View.AddSelectPopupColumn("APPROVEPERSON", new SqlQuery("GetUserAreaPerson", "10001"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("APPROVEPERSON", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
             // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)

             .SetPopupResultMapping("APPROVEPERSON", "USERID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            .SetPopupValidationCustom(ValidationApprovepersonPopup);

            // 팝업에서 사용할 조회조건 항목 추가

            parentUser.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            parentUser.GridColumns.AddTextBoxColumn("USERID", 150);
            parentUser.GridColumns.AddTextBoxColumn("USERNAME", 100);


        }


        // 요청자
        //private void InitializeGrid_RequesterPopup()
        //{

        //    var parentUser = this.grdGovernanceProduct.View.AddSelectPopupColumn("REQUESTER", new SqlQuery("GetUserAreaPerson", "10001"))
        //    // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
        //    .SetPopupLayout("REQUESTER", PopupButtonStyles.Ok_Cancel, true, false)
        //    // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
        //    .SetPopupResultCount(1)
        //     // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)

        //     .SetPopupResultMapping("REQUESTER", "USERID")
        //    // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
        //    .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
        //    // 그리드의 남은 영역을 채울 컬럼 설정
        //    //.SetPopupAutoFillColumns("CODECLASSNAME")
        //    // Validation 이 필요한 경우 호출할 Method 지정

        //    .SetPopupValidationCustom(ValidationRequesterPopup);

        //    // 팝업에서 사용할 조회조건 항목 추가

        //    parentUser.Conditions.AddTextBox("USERNAME");

        //    // 팝업 그리드 설정
        //    parentUser.GridColumns.AddTextBoxColumn("USERID", 150);
        //    parentUser.GridColumns.AddTextBoxColumn("USERNAME", 100);


        //}

      

        //private void InitializeGrid_ItemMasterPopup()
        //{

        //    var parentItem = this.grdGovernanceProduct.View.AddSelectPopupColumn("PRODUCTDEFID", new SqlQuery("GetSpecificationsItemPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
        //    // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
        //    .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
        //    // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
        //    .SetPopupResultCount(1)
        //     // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)

        //     .SetPopupResultMapping("PRODUCTDEFID", "ITEMID")
        //    // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
        //    .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
        //    // 그리드의 남은 영역을 채울 컬럼 설정
        //    //.SetPopupAutoFillColumns("CODECLASSNAME")
        //    // Validation 이 필요한 경우 호출할 Method 지정

        //    .SetPopupValidationCustom(ValidationItemMasterPopup);

        //    // 팝업에서 사용할 조회조건 항목 추가
        //    parentItem.Conditions.AddTextBox("ITEMID");
        //    parentItem.Conditions.AddTextBox("ITEMVERSION");
        //    parentItem.Conditions.AddTextBox("ITEMNAME");

        //    // 팝업 그리드 설정
        //    parentItem.GridColumns.AddTextBoxColumn("ITEMID", 150);
        //    parentItem.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
        //    parentItem.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
        //    parentItem.GridColumns.AddTextBoxColumn("UOMDEFID", 250);
        //    parentItem.GridColumns.AddTextBoxColumn("UOMDEFNAME", 250);

        //}

        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeControl();
            InitializeGridIdDefinitionManagement();

            // 파라메타 null 이 아니면
            if (this._parameters != null)
            {
                fnSearch();
            }
        }



        #region 툴바
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();


            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://의뢰

                    DataTable changed1 = grdCNCSpecialNote1.GetChangedRows();
                    if (changed1 != null)
                    {
                        if (changed1.Rows.Count != 0)
                        {
                            ExecuteRule("CncSpecialNote", changed1);
                        }

                    }

                    DataTable changed2 = grdCNCSpecialNote2.GetChangedRows();
                    if (changed2 != null)
                    {
                        if (changed2.Rows.Count != 0)
                        {
                            ExecuteRule("CncSpecialNote", changed2);
                        }

                    }

                    break;
                case 1://특이사항
                    DataTable changedCamSpecIalnoteClass = grdCamSpecIalnoteClass.GetChangedRows();
                    if (changedCamSpecIalnoteClass != null)
                    {
                        if (changedCamSpecIalnoteClass.Rows.Count != 0)
                        {
                            ExecuteRule("CamSpecialnoteclass", changedCamSpecIalnoteClass);
                        }
                    }

                    break;
              


            }

           

         
            
        }
        #endregion

        ////#region 검색


        /////// <summary>
        /////// 검색조건 팝업 예제
        /////// </summary>
        ////private void InitializeCondition_Popup()
        ////{
        ////    //팝업 컬럼 설정
        ////    var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductItemGroup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMNAME", "ITEMID")
        ////       .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
        ////       .SetPopupResultCount(1)  //팝업창 선택가능한 개수
        ////       .SetValidationIsRequired()
        ////       ;

        ////    parentPopupColumn.Conditions.AddTextBox("ITEMID");
        ////    parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

        ////    //팝업 그리드
        ////    parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
        ////    parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
        ////    parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);
        ////}


        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("GOVERNANCENO", new SqlQuery("GetGovernaceRunningChangePupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
               .SetPopupLayout("GOVERNANCENO", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               ;
            
            parentPopupColumn.Conditions.AddTextBox("GOVERNANCENO");
            parentPopupColumn.Conditions.AddTextBox("PRODUCTDEFID");
            parentPopupColumn.Conditions.AddTextBox("PRODUCTDEFVERSION");
            parentPopupColumn.Conditions.AddDateEdit("STARTDATE")
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                .SetTextAlignment(TextAlignment.Center);

            parentPopupColumn.Conditions.AddTextBox("TXTSPECPERSON");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("GOVERNANCENO", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("STARTDATE", 100);
               
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPECPERSON", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPECPERSONNAME", 200);


        }


        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            if (this._parameters != null)
            {
                SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("GOVERNANCENO");
                Popupedit.SetValue(_parameters["GOVERNANCENO"].ToString());
                Popupedit.Text = _parameters["GOVERNANCENO"].ToString();
                Popupedit.Refresh();
            }

        }

        ////private void CustmerPopupedit_Validated(object sender, EventArgs e)
        ////{
        ////    SmartSelectPopupEdit Popupedit = (SmartSelectPopupEdit)sender;

        ////    foreach (DataRow row in Popupedit.SelectedData)
        ////    {
        ////        txtShipto.Text = row["SHIPTO"].ToString();
        ////    }
        ////}

        ////private void Popupedit_Validated(object sender, EventArgs e)
        ////{
        ////    SmartSelectPopupEdit Popupedit = (SmartSelectPopupEdit)sender;


        ////    string sItemcode = "";


        ////    if (Popupedit.SelectedData.Count<DataRow>() == 0)
        ////    {
        ////        sItemcode = "-1";
        ////    }

        ////    foreach (DataRow row in Popupedit.SelectedData)
        ////    {
        ////        sItemcode = row["ITEMID"].ToString();

        ////    }

        ////    SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("ITEMVERSION");
        ////    combobox.DisplayMember = "ITEMVERSIONNAME";
        ////    combobox.ValueMember = "ITEMVERSIONCODE";
        ////    // combobox.ShowHeader = false;
        ////    Dictionary<string, object> ParamIv = new Dictionary<string, object>();
        ////    ParamIv.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
        ////    ParamIv.Add("PLANTID", UserInfo.Current.Plant);
        ////    ParamIv.Add("ITEMID", sItemcode);
        ////    DataTable dtIv = SqlExecuter.Query("GetItemVersion", "10001", ParamIv);

        ////    combobox.DataSource = dtIv;

        ////    if (dtIv.Rows.Count != 0)
        ////    {
        ////        combobox.EditValue = dtIv.Rows[0]["ITEMVERSIONCODE"];
        ////    }
        ////}

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            // 조회 함수
            fnSearch();
        }


        private void fnSearch()
        {

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();

            //TODO : Id를 수정하세요            

            SetControlsFrom scf = new SetControlsFrom();

            DataTable dtCamSpecialnote = new DataTable();
            switch (tabIdManagement.SelectedTabPageIndex)
            {
                case 0://CNC Data

                    DataTable dtCamRequest = SqlExecuter.Query("GetCNCDataList", "10001", values);
                    scf.SetControlsFromTable(smartSplitTableLayoutPanel4, dtCamRequest);
                    if (dtCamRequest.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }


                    DataTable dtCNCSpecialnote = SqlExecuter.Query("GetCNCSpecialnoteList1", "10001", values);
                    grdCNCSpecialNote1.DataSource = dtCNCSpecialnote;


                    break;
                case 1://특이사항

                    values.Add("PLANTID", UserInfo.Current.Plant);
                    values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    values.Add("NOTECLASSTYPE", "CNCMain");

                    DataTable dtCamSpecIalnoteClass = SqlExecuter.Query("GetCamSpecialnoteclassList", "10001", values);

                    if (dtCamSpecIalnoteClass.Rows.Count < 1) // 
                    {
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                    }

                    grdCamSpecIalnoteClass.DataSource = dtCamSpecIalnoteClass;


                    break;


            }
         
        }


        ////#region 유효성 검사
        /////// <summary>
        /////// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /////// </summary>
        ////protected override void OnValidateContent()
        ////{
        ////    base.OnValidateContent();

        ////    DataTable changed = new DataTable();
        ////    switch (tabIdManagement.SelectedTabPageIndex)
        ////    {
        ////        case 0://ID Class
        ////            GetControlsFrom gcf = new GetControlsFrom();
        ////            gcf.GetControlsFromGrid(smartSplitTableLayoutPanel4, grdPackageProduct);


        ////            //grdPackageProduct.View.CheckValidation();

        ////            changed = grdPackageProduct.GetChangedRows();

        ////            // 1CARD/1TARY
        ////            if (decimal.Parse(txtCardTary.Text) == 0 && decimal.Parse(txtCase.Text) == 0)
        ////            {
        ////                throw MessageException.Create("CardTaryCase");
        ////            }

        ////            // 포장분류
        ////            if (txtPackageclass.Text == "")
        ////            {
        ////                throw MessageException.Create("Packageclass");
        ////            }


        ////            //object obj = grdMDCList.DataSource;
        ////            //DataTable dt = (DataTable)obj;
        ////            //string sMessage = "";
        ////            //foreach (DataRow row in dt.DefaultView.ToTable(true, new string[] { "MASTERDATACLASSID" }).Rows)
        ////            //{
        ////            //    int count = dt.Select("MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").Length;
        ////            //    //int i = int.Parse(dt.Compute("COUNT(MASTERDATACLASSID)", "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").ToString());
        ////            //    if (count > 1)
        ////            //    {
        ////            //        sMessage = sMessage + "품목유형 코드 " + row["MASTERDATACLASSID"].ToString() + "은" + count.ToString() + " 개가 중복입니다." + "\r\n";
        ////            //        //sfilter = "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'";
        ////            //    }
        ////            //}
        ////            //if (sMessage != "")
        ////            //{
        ////            //    throw MessageException.Create(sMessage);
        ////            //}

        ////            break;
        ////        case 1://ID Definition


        ////            //object obj1 = grdAAGList.DataSource;
        ////            //DataTable dt1 = (DataTable)obj1;
        ////            //string sMessage1 = "";



        ////            //foreach (DataRow row in dt1.Rows)
        ////            //{
        ////            //    int count = dt1.Select("MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "' AND ATTRIBUTEGROUPID = '" + row["ATTRIBUTEGROUPID"].ToString() + "'").Length;
        ////            //    //int i = int.Parse(dt.Compute("COUNT(MASTERDATACLASSID)", "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'").ToString());
        ////            //    if (count > 1)
        ////            //    {
        ////            //        sMessage1 = row["MASTERDATACLASSID"].ToString() + "/" + row["ATTRIBUTEGROUPID"].ToString();
        ////            //        //sfilter = "MASTERDATACLASSID = '" + row["MASTERDATACLASSID"].ToString() + "'";
        ////            //    }
        ////            //}
        ////            //if (sMessage1 != "")
        ////            //{
        ////            //    throw MessageException.Create("InValidData007", sMessage1);
        ////            //}


        ////            break;
        ////        case 2://ID Definition
        ////            //grdIdDefinitionList.View.CheckValidation();
        ////            //changed = grdIdDefinitionList.GetChangedRows();
        ////            break;
        ////    }
        ////    if (changed.Rows.Count == 0)
        ////    {
        ////        // 저장할 데이터가 존재하지 않습니다.
        ////        throw MessageException.Create("NoSaveData");
        ////    }
        ////}
        ////#endregion

        #region 이벤트
        private void InitializeEvent()
        {
            grdCNCSpecialNote1.View.AddingNewRow += grdCNCSpecialNote1_AddingNewRow;
            grdCNCSpecialNote1.View.FocusedRowChanged += grdCNCSpecialNote1_FocusedRowChanged;

            grdCNCSpecialNote2.View.AddingNewRow += grdCNCSpecialNote2_AddingNewRow;
            grdCNCSpecialNote2.View.FocusedRowChanged += grdCNCSpecialNote2_FocusedRowChanged;


            grdCamSpecIalnoteClass.View.AddingNewRow += grdCamSpecIalnoteClass_AddingNewRow;

            // 품목업로드
            btnFileUpload.Click += BtnFileUpload_Click;

        }

        private void grdCNCSpecialNote2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdCNCSpecialNote2.View.FocusedRowHandle < 0)
                return;

            // 그리드 초기화
            DataTable dtFileList = (DataTable)grdFileList.DataSource;
            if (dtFileList != null)
            {
                dtFileList.Clear();
            }


            DataRow row = this.grdCNCSpecialNote2.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("RESOURCEID", row["PARENTNOTECLASSID"].ToString() + row["NOTECLASSID"].ToString() + row["VENDORID"].ToString());
            param.Add("RESOURCETYPE", "CNCSpecialNote");
            param.Add("RESOURCEVERSION", "");
            
            DataTable dtFileLists = SqlExecuter.Query("GetFileUploadList", "10001", param);

            grdFileList.DataSource = dtFileLists;

        }

        private void BtnFileUpload_Click(object sender, EventArgs e)
        {
            if (grdCNCSpecialNote2.View.FocusedRowHandle < 0)
                return;

            DataRow row = this.grdCNCSpecialNote2.View.GetFocusedDataRow();

            if (row.RowState != DataRowState.Unchanged)
            {
                ItemMasterfilePopup pis = new ItemMasterfilePopup(row["PARENTNOTECLASSID"].ToString() + row["NOTECLASSID"].ToString() + row["VENDORID"].ToString(), "", "CNCSpecialNote", "CNCSpecialNoteMgnt / CNCSpecialNote");
                pis.ShowDialog();
            }
            else
            {
                ShowMessage("NotSaveUnchanged");
            }
           
        }

        private void grdCamSpecIalnoteClass_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["NOTECLASSTYPE"] = "CNCMain";
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VALIDSTATE"] = "Valid";

            // 오늘날짜.
            Dictionary<string, object> paramdt = new Dictionary<string, object>();
            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001", paramdt);
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

            GetNumber number = new GetNumber();
            args.NewRow["NOTECLASSID"] = number.GetStdNumber("NoteClassId", "NS" + sdate);



        }

        private void grdCNCSpecialNote2_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataRow rowCNCSpecialNote1 = grdCNCSpecialNote1.View.GetFocusedDataRow();

            DataTable dt = (DataTable)grdCNCSpecialNote2.DataSource;
            args.NewRow["GOVERNANCENO"] = txtGOVERNANCENO.Text;
            args.NewRow["GOVERNANCETYPE"] = txtGOVERNANCETYPE.Text;
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            args.NewRow["VENDORID"] = rowCNCSpecialNote1["VENDORID"];

            args.NewRow["PARENTNOTECLASSID"] = rowCNCSpecialNote1["PARENTNOTECLASSID"];

           
        }

        private void grdCNCSpecialNote1_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {


            DataTable dt = (DataTable)grdCNCSpecialNote1.DataSource;
            args.NewRow["GOVERNANCENO"] = txtGOVERNANCENO.Text;
            args.NewRow["GOVERNANCETYPE"] = txtGOVERNANCETYPE.Text;
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
            //args.NewRow["PARENTNOTECLASSID"] = cboCamSpecialnoteClass.GetDataValue();

          
        }

        private void grdCNCSpecialNote1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdCNCSpecialNote1.View.FocusedRowHandle < 0)
                return;
            // 그리드 초기화
            DataTable dtCNCSpecialNote2 = (DataTable)grdCNCSpecialNote2.DataSource;
            if( dtCNCSpecialNote2 != null)
            {
                dtCNCSpecialNote2.Clear();
            }
            DataRow dataRow = grdCNCSpecialNote1.View.GetFocusedDataRow();


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("GOVERNANCENO", dataRow["GOVERNANCENO"].ToString());
            param.Add("VENDORID", dataRow["VENDORID"].ToString());
            DataTable dtCNCSpecialnote2 = SqlExecuter.Query("GetCNCSpecialnoteList2", "10001", param);
            grdCNCSpecialNote2.DataSource = dtCNCSpecialnote2;
           

        }

       

        #endregion


        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 작업장)
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationItemMasterPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["PRODUCTDEFNAME"] = row["ITEMNAME"];
                currentGridRow["PRODUCTDEFVERSION"] = row["ITEMVERSION"];



                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("ASSEMBLYITEMID", row["ITEMID"].ToString());
                param.Add("ASSEMBLYITEMVERSION", row["ITEMVERSION"].ToString());
                DataTable dt = SqlExecuter.Query("GetRountingTree", "10003", param);

                DataTable dtsiet = (DataTable)grdCNCSpecialNote2.DataSource;

                foreach (DataRow rownew in dt.Rows)
                {
                    if(dtsiet != null)
                    {
                        if (dtsiet.Select("PLANTID = '" + rownew["PLANTID"].ToString() + "'").Length == 0)
                        {
                            grdCNCSpecialNote2.View.AddNewRow();
                            DataRow rowFocused = grdCNCSpecialNote2.View.GetFocusedDataRow();
                            rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            rowFocused["PLANTID"] = rownew["PLANTID"];
                            rowFocused["GOVERNANCETYPE"] = "RunningChange";
                        }
                    }
                    else
                    {
                        grdCNCSpecialNote2.View.AddNewRow();
                        DataRow rowFocused = grdCNCSpecialNote2.View.GetFocusedDataRow();
                        rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        rowFocused["PLANTID"] = rownew["PLANTID"];
                        rowFocused["GOVERNANCETYPE"] = "RunningChange";
                        
                    }
                   
                }
            }
            return result;
        }

       
        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 사양담당자 )
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationSiteSpecPersonPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["SITESPECPERSONNAME"] = row["USERNAME"];
            }
            return result;
        }
        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 요청자 )
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationRequesterPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["REQUESTERNAME"] = row["USERNAME"];
            }
            return result;
        }


        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 승인자 )
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationApprovepersonPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["APPROVEPERSONNAME"] = row["USERNAME"];
            }
            return result;
        }

        #region 그리드이벤트
        #endregion

        #region 기타이벤트



        #endregion

        ////#endregion



        ////#region Private Function

        ////// TODO : 화면에서 사용할 내부 함수 추가

        ////#endregion
    }
}
