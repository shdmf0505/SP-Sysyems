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
	public partial class Governance : SmartConditionManualBaseForm
    {
        #region Local Variables
        string fgSearch = "";
        #endregion

        #region 생성자
        public Governance()
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
            grdGovernance.Hide();

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


            // 생산구분
            cboPRODUCTCLASS.DisplayMember = "CODENAME";
            cboPRODUCTCLASS.ValueMember = "CODEID";
            cboPRODUCTCLASS.ShowHeader = false;
            Dictionary<string, object> ParamConfirmation = new Dictionary<string, object>();
            ParamConfirmation.Add("CODECLASSID", "ProductionType");
            ParamConfirmation.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtConfirmation = SqlExecuter.Query("GetCodeList", "00001", ParamConfirmation);
            cboPRODUCTCLASS.DataSource = dtConfirmation;

            // 적용구분
            cboIMPLEMENTATIONTYPE.DisplayMember = "CODENAME";
            cboIMPLEMENTATIONTYPE.ValueMember = "CODEID";
            cboIMPLEMENTATIONTYPE.ShowHeader = false;
            Dictionary<string, object> ParamIMPLEMENTATIONTYPE = new Dictionary<string, object>();
            ParamIMPLEMENTATIONTYPE.Add("CODECLASSID", "ImplementationType");
            ParamIMPLEMENTATIONTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtIMPLEMENTATIONTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamIMPLEMENTATIONTYPE);
            cboIMPLEMENTATIONTYPE.DataSource = dtIMPLEMENTATIONTYPE;

            // 작업유형
            cboPLATINGTYPE.DisplayMember = "CODENAME";
            cboPLATINGTYPE.ValueMember = "CODEID";
            cboPLATINGTYPE.ShowHeader = false;
            Dictionary<string, object> ParamPLATINGTYPE = new Dictionary<string, object>();
            ParamPLATINGTYPE.Add("CODECLASSID", "PlatingType");
            ParamPLATINGTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPLATINGTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamPLATINGTYPE);
            cboPLATINGTYPE.DataSource = dtPLATINGTYPE;

            // 작업구분
            cboWORKCLASS.DisplayMember = "CODENAME";
            cboWORKCLASS.ValueMember = "CODEID";
            cboWORKCLASS.ShowHeader = false;
            Dictionary<string, object> ParamJOBCLASS = new Dictionary<string, object>();
            ParamJOBCLASS.Add("CODECLASSID", "JobType");
            ParamJOBCLASS.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtJOBCLASS = SqlExecuter.Query("GetCodeList", "00001", ParamJOBCLASS);
            cboWORKCLASS.DataSource = dtJOBCLASS;


            //CAM담당
            ConditionItemSelectPopup cisiCAMMAN = new ConditionItemSelectPopup();
            cisiCAMMAN.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiCAMMAN.SetPopupLayout("CAMMAN", PopupButtonStyles.Ok_Cancel);
            cisiCAMMAN.Id = "CAMMAN";
            cisiCAMMAN.LabelText = "CAMMAN";
            cisiCAMMAN.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiCAMMAN.IsMultiGrid = false;
            cisiCAMMAN.DisplayFieldName = "USERNAME";
            cisiCAMMAN.ValueFieldName = "USERID";
            cisiCAMMAN.LanguageKey = "CAMMAN";
            cisiCAMMAN.Conditions.AddTextBox("USERNAME");
            cisiCAMMAN.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiCAMMAN.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspCAMPERSON.SelectPopupCondition = cisiCAMMAN;

            //영업담당
            ConditionItemSelectPopup cisiSALESMAN = new ConditionItemSelectPopup();
            cisiSALESMAN.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiSALESMAN.SetPopupLayout("SALESMAN", PopupButtonStyles.Ok_Cancel);
            cisiSALESMAN.Id = "SALESMAN";
            cisiSALESMAN.LabelText = "SALESMAN";
            cisiSALESMAN.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiSALESMAN.IsMultiGrid = false;
            cisiSALESMAN.DisplayFieldName = "USERNAME";
            cisiSALESMAN.ValueFieldName = "USERID";
            cisiSALESMAN.LanguageKey = "SALESMAN";
            cisiSALESMAN.Conditions.AddTextBox("USERNAME");
            cisiSALESMAN.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiSALESMAN.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspSALESPERSON.SelectPopupCondition = cisiSALESMAN;

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

            // 거래처팝업
            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);

            cisidvendorCode.Id = "CUSTOMERID";
            cisidvendorCode.LabelText = "CUSTOMERID";
            cisidvendorCode.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "CUSTOMERNAME";
            cisidvendorCode.ValueFieldName = "CUSTOMERID";
            cisidvendorCode.LanguageKey = "CUSTOMERID";

            cisidvendorCode.Conditions.AddTextBox("TXTCUSTOMERID");
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            cisidvendorCode.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CEONAME", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("TELNO", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("FAXNO", 100);
            sspCUSTOMERID.SelectPopupCondition = cisidvendorCode;

            
            // 제품팝업
            ConditionItemSelectPopup cisiPRODUCTDEFID = new ConditionItemSelectPopup();
            cisiPRODUCTDEFID.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiPRODUCTDEFID.SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel);

            cisiPRODUCTDEFID.Id = "PRODUCTDEFID";
            cisiPRODUCTDEFID.LabelText = "PRODUCTDEFID";
            cisiPRODUCTDEFID.SearchQuery = new SqlQuery("GetSpecificationsItemPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}");
            cisiPRODUCTDEFID.IsMultiGrid = false;
            cisiPRODUCTDEFID.DisplayFieldName = "ITEMID";
            cisiPRODUCTDEFID.ValueFieldName = "ITEMID";
            cisiPRODUCTDEFID.LanguageKey = "PRODUCTDEFID";

            cisiPRODUCTDEFID.Conditions.AddTextBox("ITEMID");
            cisiPRODUCTDEFID.Conditions.AddTextBox("ITEMVERSION");
            cisiPRODUCTDEFID.Conditions.AddTextBox("ITEMNAME");

            cisiPRODUCTDEFID.GridColumns.AddTextBoxColumn("ITEMID", 150);
            cisiPRODUCTDEFID.GridColumns.AddTextBoxColumn("ITEMVERSION", 200);
            cisiPRODUCTDEFID.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
            cisiPRODUCTDEFID.GridColumns.AddTextBoxColumn("UOMDEFID", 100);
            cisiPRODUCTDEFID.GridColumns.AddTextBoxColumn("UOMDEFNAME", 100);
            
            sspPRODUCTDEFID.SelectPopupCondition = cisiPRODUCTDEFID;
            sspPRODUCTDEFID.SelectPopupCondition
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    foreach (DataRow row in selectedRows)
                    {
                        txtVer.Text = row["ITEMVERSION"].ToString();
                        txtPRODUCTDEFNAME.Text = row["ITEMNAME"].ToString();
                    }
                });

            //sspPRODUCTDEFID.Validated += SspPRODUCTDEFID_Validated;
            //sspPRODUCTDEFID.TextChanged += SspPRODUCTDEFID_TextChanged;


            //cboITEMVERSION.TextChanged += CboITEMVERSION_TextChanged;



        }

        //private void SspPRODUCTDEFID_TextChanged(object sender, EventArgs e)
        //{
        //    if(fgSearch != "Y")
        //    {
        //        cboITEMVERSION.ItemIndex = 0;
        //        cboITEMVERSION.EditValue = "";
        //        cboITEMVERSION.Text = "";
        //    }

        //    fgSearch = "";
        //}

        //private void CboITEMVERSION_TextChanged(object sender, EventArgs e)
        //{
        //    Dictionary<string, object> param = new Dictionary<string, object>();
        //    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
        //    param.Add("ASSEMBLYITEMID", sspPRODUCTDEFID.GetValue());
        //    param.Add("ASSEMBLYITEMVERSION", cboITEMVERSION.EditValue);
        //    DataTable dt = SqlExecuter.Query("GetRountingTree", "10003", param);

        //    DataTable dtsiet = (DataTable)grdGovernanceSite.DataSource;

        //    foreach (DataRow rownew in dt.Rows)
        //    {
        //        if (dtsiet != null)
        //        {
        //            if (dtsiet.Select("PLANTID = '" + rownew["PLANTID"].ToString() + "'").Length == 0)
        //            {
                     
        //                grdGovernanceSite.View.AddNewRow();
        //                DataRow rowFocused = grdGovernanceSite.View.GetFocusedDataRow();
        //                rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
        //                rowFocused["PLANTID"] = rownew["PLANTID"];
        //                rowFocused["GOVERNANCETYPE"] = "NewRequest";
        //                rowFocused["SEQUENCE"] = rownew["SEQ"];

        //            }
        //        }
        //        else
        //        {
        //            grdGovernanceSite.View.AddNewRow();
        //            DataRow rowFocused = grdGovernanceSite.View.GetFocusedDataRow();
        //            rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
        //            rowFocused["PLANTID"] = rownew["PLANTID"];
        //            rowFocused["GOVERNANCETYPE"] = "NewRequest";
        //            rowFocused["SEQUENCE"] = rownew["SEQ"];

        //        }

        //    }
        //}

        //private void SspPRODUCTDEFID_Validated(object sender, EventArgs e)
        //{

        //    string sItemcode = "";


        //    if (sspPRODUCTDEFID.SelectedData.Count<DataRow>() == 0)
        //    {
        //        sItemcode = "-1";
        //    }

        //    foreach (DataRow row in sspPRODUCTDEFID.SelectedData)
        //    {
        //        sItemcode = row["ITEMID"].ToString();

        //    }
           

        //    cboITEMVERSION.Reset();
            


        //    cboITEMVERSION.DisplayMember = "ITEMVERSIONNAME";
        //    cboITEMVERSION.ValueMember = "ITEMVERSIONCODE";
        //    cboITEMVERSION.ShowHeader = false;
        //    //cboITEMVERSION.EmptyItemValue = 0;
        //    //cboITEMVERSION.EmptyItemCaption = "";
        //    cboITEMVERSION.UseEmptyItem = true;

        //    Dictionary<string, object> ParamIv = new Dictionary<string, object>();
        //    ParamIv.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
        //    ParamIv.Add("PLANTID", UserInfo.Current.Plant);
        //    ParamIv.Add("ITEMID", sItemcode);
        //    DataTable dtIv = SqlExecuter.Query("GetItemVersion", "10001", ParamIv);

        //    cboITEMVERSION.DataSource = dtIv;
        //}



        #endregion
        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {

            grdGovernance.GridButtonItem = GridButtonItem.None;   // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdGovernance.View.AddTextBoxColumn("GOVERNANCENO");
            grdGovernance.View.AddTextBoxColumn("GOVERNANCETYPE");
            grdGovernance.View.AddTextBoxColumn("ENTERPRISEID");
            grdGovernance.View.AddTextBoxColumn("PLANTID");
            grdGovernance.View.AddTextBoxColumn("DEPARTMENT");
            grdGovernance.View.AddTextBoxColumn("REASON");
            grdGovernance.View.AddTextBoxColumn("PRIORITY");
            grdGovernance.View.AddTextBoxColumn("APPROVALTYPE");
            grdGovernance.View.AddTextBoxColumn("APPROVALID");
            grdGovernance.View.AddTextBoxColumn("SPECPERSON");
            grdGovernance.View.AddTextBoxColumn("SALESPERSON");
            grdGovernance.View.AddTextBoxColumn("STATUS");
            grdGovernance.View.AddTextBoxColumn("STARTDATE");
            
            grdGovernance.View.AddTextBoxColumn("REQUESTDATE");

            grdGovernance.View.AddTextBoxColumn("IMPLEMENTATIONTYPE");
            grdGovernance.View.AddTextBoxColumn("IMPLEMENTATIONDATE");

            grdGovernance.View.AddTextBoxColumn("ERPITEMDATE");
            
            grdGovernance.View.AddTextBoxColumn("ISCNCATTACHED");
            grdGovernance.View.AddTextBoxColumn("CAMREQUESTID");
            grdGovernance.View.AddTextBoxColumn("CAMPERSON");
            grdGovernance.View.AddTextBoxColumn("PCRNO");
            grdGovernance.View.AddTextBoxColumn("PCRREQUESTER");
            grdGovernance.View.AddTextBoxColumn("PCRDATE");
            grdGovernance.View.AddTextBoxColumn("MODELDELIVERYDATE");
           
            grdGovernance.View.AddTextBoxColumn("MODELNO");
            grdGovernance.View.AddTextBoxColumn("WORKTYPE");
            grdGovernance.View.AddTextBoxColumn("WORKCLASS");
            grdGovernance.View.AddTextBoxColumn("PRODUCTCLASS");
            grdGovernance.View.AddTextBoxColumn("CUSTOMERID", 80);
            grdGovernance.View.AddTextBoxColumn("CUSTOMERREV");
            grdGovernance.View.AddTextBoxColumn("PANELSIZE");
            grdGovernance.View.AddTextBoxColumn("DESCRIPTION");
            grdGovernance.View.AddTextBoxColumn("VALIDSTATE");

            grdGovernance.View.AddTextBoxColumn("PRODUCTDEFID");
            grdGovernance.View.AddTextBoxColumn("PRODUCTDEFVERSION");

            grdGovernance.View.PopulateColumns();

 

            //품목
            //grdGovernanceProduct.GridButtonItem = GridButtonItem.None;
            //grdGovernanceProduct.View.AddTextBoxColumn("GOVERNANCENO").SetIsHidden();
            //grdGovernanceProduct.View.AddTextBoxColumn("GOVERNANCETYPE").SetIsHidden();
            //grdGovernanceProduct.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            //grdGovernanceProduct.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            //InitializeGrid_ItemMasterPopup();
            //grdGovernanceProduct.View.AddTextBoxColumn("PRODUCTDEFNAME").SetIsReadOnly();
            //grdGovernanceProduct.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsReadOnly();
            //grdGovernanceProduct.View.AddTextBoxColumn("MODELNO");
            //grdGovernanceProduct.View.AddTextBoxColumn("DESCRIPTION");
            //grdGovernanceProduct.View.PopulateColumns();
            ////행추가
            //grdGovernanceProduct.View.AddNewRow();

            //사이트
            grdGovernanceSite.GridButtonItem = GridButtonItem.All;
            grdGovernanceSite.View.AddTextBoxColumn("GOVERNANCENO").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("GOVERNANCETYPE").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("RECEIPTDATE").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("SITESPECPERSON").SetIsHidden();
            
            if(UserInfo.Current.Enterprise == "INTERFLEX")
            {
                grdGovernanceSite.View.AddComboBoxColumn("PLANTID", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RoutingInterflex", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }
            else
            {
                grdGovernanceSite.View.AddComboBoxColumn("PLANTID", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RoutingYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }

            grdGovernanceSite.View.AddTextBoxColumn("STATE").SetIsReadOnly();
            grdGovernanceSite.View.AddTextBoxColumn("REQUESTDATE").SetIsReadOnly();
            //InitializeGrid_RequesterPopup();

            InitializeGrid_SiteSpecpersonPopup();
            grdGovernanceSite.View.AddTextBoxColumn("SITESPECPERSONNAME").SetIsReadOnly();

            grdGovernanceSite.View.AddTextBoxColumn("REQUESTER").SetIsReadOnly();
            grdGovernanceSite.View.AddTextBoxColumn("REQUESTERNAME").SetIsReadOnly();
            
            grdGovernanceSite.View.AddTextBoxColumn("APPROVEDDATE").SetIsHidden();
            InitializeGrid_ApprovepersonPopup();
            grdGovernanceSite.View.AddTextBoxColumn("APPROVEPERSONNAME").SetIsReadOnly();

            grdGovernanceSite.View.AddTextBoxColumn("DESCRIPTION").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("VALIDSTATE").SetIsHidden();
            grdGovernanceSite.View.PopulateColumns();

       

        }


        // 승인자
        private void InitializeGrid_ApprovepersonPopup()
        {

            var parentUser = this.grdGovernanceSite.View.AddSelectPopupColumn("APPROVEPERSON", new SqlQuery("GetUserAreaPerson", "10001"))
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
        // 사양담당자
        private void InitializeGrid_SiteSpecpersonPopup()
        {

            var parentUser = this.grdGovernanceSite.View.AddSelectPopupColumn("SITESPECPERSON", new SqlQuery("GetUserAreaPerson", "10001"))
            // 팝업 폼 타이틀(다국어 키 입력), 버튼 유형, 검색 버튼 활성화 여부, 자동 검색 여부
            .SetPopupLayout("SITESPECPERSON", PopupButtonStyles.Ok_Cancel, true, false)
            // 팝업에서 전달해주는 결과값 개수 설정 (1 = 단일, 0 = 복수)
            .SetPopupResultCount(1)
             // 그리드의 컬럼과 팝업의 컬럼이 필드명이 다른 경우 맵핑 정보 설정 (그리드 컬럼 필드명, 팝업 컬럼 필드명)

             .SetPopupResultMapping("SITESPECPERSON", "USERID")
            // 팝업 폼 Layout 설정 (넓이, 높이, Border Style, 조회조건 구성 방향)
            .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
            // 그리드의 남은 영역을 채울 컬럼 설정
            //.SetPopupAutoFillColumns("CODECLASSNAME")
            // Validation 이 필요한 경우 호출할 Method 지정

            .SetPopupValidationCustom(ValidationSiteSpecpersonPopup);

            // 팝업에서 사용할 조회조건 항목 추가

            parentUser.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            parentUser.GridColumns.AddTextBoxColumn("USERID", 150);
            parentUser.GridColumns.AddTextBoxColumn("USERNAME", 100);


        }

        //// 요청자
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
            // 사이트 
            DataTable dtSite = grdGovernanceSite.GetChangedRows();

            DataTable changed = new DataTable();
            GetControlsFrom confrom = new GetControlsFrom();

            // 채번 시리얼 존재 유무 체크
            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);


            Dictionary<string, object> parampf = new Dictionary<string, object>();
            parampf.Add("IDCLASSID", "NRGovernance");
            parampf.Add("PREFIX", "NR" + sdate);
            DataTable dtItemserialChk = SqlExecuter.Query("GetProductitemserial", "10001", parampf);

            DataTable dtItemserialI = dtItemserialChk.Clone();
            dtItemserialI.Columns.Add("_STATE_");


            if (dtItemserialChk != null)
            {
                if (dtItemserialChk.Rows.Count == 0)
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = "NRGovernance";
                    rowItemserialI["PREFIX"] = "NR" + sdate;
                    rowItemserialI["LASTSERIALNO"] = "00001";
                    rowItemserialI["_STATE_"] = "added";
                    dtItemserialI.Rows.Add(rowItemserialI);


                }
                else
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = "NRGovernance";
                    rowItemserialI["PREFIX"] = "NR" + sdate;

                    int ilastserialno = 0;
                    ilastserialno = Int32.Parse(dtItemserialChk.Rows[0]["LASTSERIALNO"].ToString());
                    ilastserialno = ilastserialno + 1;


                    rowItemserialI["LASTSERIALNO"] = ("0000" + ilastserialno.ToString()).Substring(("0000" + ilastserialno.ToString()).Length - 5, 5);
                    rowItemserialI["_STATE_"] = "modified";
                    dtItemserialI.Rows.Add(rowItemserialI);
                }
            }
            else
            {
                DataRow rowItemserialI = dtItemserialI.NewRow();
                rowItemserialI["IDCLASSID"] = "NRGovernance";
                rowItemserialI["PREFIX"] = "NR" + sdate;
                rowItemserialI["LASTSERIALNO"] = "00001";
                rowItemserialI["_STATE_"] = "added";
                dtItemserialI.Rows.Add(rowItemserialI);
            }

            object objNew = this.grdGovernance.DataSource;
            DataTable dtNew = (DataTable)objNew;

            if (dtNew.Rows.Count ==0)
            {
                grdGovernance.View.AddNewRow();
                DataRow row = grdGovernance.View.GetFocusedDataRow();
                row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                row["PLANTID"] = UserInfo.Current.Plant;
                row["GOVERNANCETYPE"] = "NewRequest";
                row["STATUS"] = "Working";
                txtPRIORITY.Text = "A";
                txtGOVERNANCENO.Text = dtItemserialI.Rows[0]["PREFIX"].ToString() + dtItemserialI.Rows[0]["LASTSERIALNO"].ToString();
            }

            confrom.GetControlsFromGrid(smartSplitTableLayoutPanel4, grdGovernance);
            changed = grdGovernance.GetChangedRows();

            // 신규등록
            if(changed != null)
            {
                if(changed.Rows.Count !=0)
                {
                    DataSet ds = new DataSet();
                    changed.TableName = "governance";
                    dtItemserialI.TableName = "idclassserial";

                    foreach (DataRow row in changed.Rows)
                    {
                        if (row["STARTDATE"].ToString() != "")
                        {
                            row["STARTDATE"] = DateTime.Parse(row["STARTDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if (row["IMPLEMENTATIONDATE"].ToString() != "")
                        {
                            row["IMPLEMENTATIONDATE"] = DateTime.Parse(row["IMPLEMENTATIONDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if(row["_STATE_"].ToString() == "added")
                        {
                            row["DEPARTMENT"] = UserInfo.Current.Department;
                        }

                        
                    }



                    ds.Tables.Add(changed);
                    ds.Tables.Add(dtItemserialI);

                    ExecuteRule("Governance", ds);

                    if(changed.Rows[0].RowState == DataRowState.Added)
                    {
                        ExecuteRule("GovernanceProduct", changed);
                    }
                    else
                    {

                        DataTable dtDelete = new DataTable();
                        dtDelete = changed.Copy();
                        dtDelete.Rows[0]["_STATE_"] = "deleted";
                        dtDelete.Rows[0]["PRODUCTDEFID"] = dtDelete.Rows[0]["BF_PRODUCTDEFID"];
                        dtDelete.Rows[0]["PRODUCTDEFVERSION"] = dtDelete.Rows[0]["BF_PRODUCTDEFVERSION"];
                        ExecuteRule("GovernanceProduct", dtDelete);

                        DataTable dtAdd = new DataTable();
                        dtAdd = changed.Copy();
                        dtAdd.Rows[0]["_STATE_"] = "added";
                        ExecuteRule("GovernanceProduct", dtAdd);
                     
                    }

                }
                
            }

            // 품목지정
            //DataTable dtProduct = grdGovernanceProduct.GetChangedRows();
            //if (dtProduct != null)
            //{
            //    if(dtProduct.Rows.Count !=0)
            //    {
            //        if (changed != null)
            //        {
            //            if (changed.Rows.Count != 0)
            //            {
            //                foreach (DataRow rowProduct in dtProduct.Rows)
            //                {
            //                    rowProduct["GOVERNANCENO"] = changed.Rows[0]["GOVERNANCENO"];
            //                    rowProduct["GOVERNANCETYPE"] = changed.Rows[0]["GOVERNANCETYPE"];
            //                }
            //            }
            //            else
            //            {
            //                DataRow row = grdGovernance.View.GetFocusedDataRow();

            //                foreach (DataRow rowProduct in dtProduct.Rows)
            //                {
            //                    rowProduct["GOVERNANCENO"] = row["GOVERNANCENO"];
            //                    rowProduct["GOVERNANCETYPE"] = row["GOVERNANCETYPE"];
            //                }
            //            }
                        
            //        }
            //        else
            //        {
            //            DataRow row = grdGovernance.View.GetFocusedDataRow();

            //            foreach (DataRow rowProduct in dtProduct.Rows)
            //            {
            //                rowProduct["GOVERNANCENO"] = row["GOVERNANCENO"];
            //                rowProduct["GOVERNANCETYPE"] = row["GOVERNANCETYPE"];
            //            }

            //        }
            //        ExecuteRule("GovernanceProduct", dtProduct);
            //    }
            //}
            // 사이트
           
            if (dtSite != null)
            {
                if (dtSite.Rows.Count != 0)
                {
                    if(changed != null)
                    {
                        if(changed.Rows.Count !=0)
                        {
                            foreach (DataRow rowSite in dtSite.Rows)
                            {
                                rowSite["GOVERNANCENO"] = changed.Rows[0]["GOVERNANCENO"];
                                rowSite["GOVERNANCETYPE"] = changed.Rows[0]["GOVERNANCETYPE"];
                                rowSite["ENTERPRISEID"] = UserInfo.Current.Enterprise;

                                //rowSite["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
                            }
                        }
                        else
                        {
                            DataRow row = grdGovernance.View.GetFocusedDataRow();

                            foreach (DataRow rowSite in dtSite.Rows)
                            {
                                rowSite["GOVERNANCENO"] = row["GOVERNANCENO"];
                                rowSite["GOVERNANCETYPE"] = row["GOVERNANCETYPE"];
                                rowSite["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                                //rowSite["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
                            }
                        }

                       
                    }
                    else
                    {
                        DataRow row = grdGovernance.View.GetFocusedDataRow();

                        foreach (DataRow rowSite in dtSite.Rows)
                        {
                            rowSite["GOVERNANCENO"] = row["GOVERNANCENO"];
                            rowSite["GOVERNANCETYPE"] = row["GOVERNANCETYPE"];
                            rowSite["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            //rowSite["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
                        }

                    }

                    DataTable dttemp = new DataTable();
                    DataSet ds = new DataSet();
                    dtSite.TableName = "governanceSite";
                    dttemp.TableName = "temp";

                    ds.Tables.Add(dtSite);
                    ds.Tables.Add(dttemp);


                    ExecuteRule("GovernanceSite", ds);
                }
            }

            SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("GOVERNANCENO");
            Popupedit.SetValue(txtGOVERNANCENO.Text);
            Popupedit.Text = txtGOVERNANCENO.Text;
            Popupedit.Refresh();



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
            var parentPopupColumn = Conditions.AddSelectPopup("GOVERNANCENO", new SqlQuery("GetGovernacePupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
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
            
            if(this._parameters != null)
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

            // 조회
            fnSearch();

        }

        private void fnSearch()
        {
            // 그리드 초기화
            DataTable dtGovernanceClear = (DataTable)grdGovernance.DataSource;
            dtGovernanceClear.Clear();

            DataTable dtGovernanceSiteClear = (DataTable)grdGovernanceSite.DataSource;
            dtGovernanceSiteClear.Clear();


            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            DataTable dtGovernaceList = SqlExecuter.Query("GetGovernaceList", "10001", values);
            grdGovernance.DataSource = dtGovernaceList;
            if (dtGovernaceList.Rows.Count < 1) // 
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                //cboITEMVERSION.DisplayMember = "ITEMVERSIONNAME";
                //cboITEMVERSION.ValueMember = "ITEMVERSIONCODE";
                //cboITEMVERSION.ShowHeader = false;

                //Dictionary<string, object> ParamIv = new Dictionary<string, object>();
                //ParamIv.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //ParamIv.Add("PLANTID", UserInfo.Current.Plant);
                //ParamIv.Add("ITEMID", dtGovernaceList.Rows[0]["PRODUCTDEFID"].ToString());
                //DataTable dtIv = SqlExecuter.Query("GetItemVersion", "10001", ParamIv);

                //cboITEMVERSION.DataSource = dtIv;
            }

            // textchange 이벤트 제어하기 위한 여부
            fgSearch = "Y";


            //if (dtIv.Rows.Count != 0)
            //{
            //    cboITEMVERSION.EditValue = dtIv.Rows[0]["PRODUCTDEFVERSION"];
            //}

            SetControlsFrom ControlsFrom = new SetControlsFrom();
            ControlsFrom.SetControlsFromTable(smartSplitTableLayoutPanel4, dtGovernaceList);
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
            grdGovernanceSite.View.AddingNewRow += grdGovernanceSite_AddingNewRow;
            grdGovernanceSite.View.SelectionChanged += grdGovernanceSite_SelectionChanged;
            grdGovernance.View.FocusedRowChanged += grdGovernance_FocusedRowChanged;

            btnRequestApproval.Click += BtnRequestApproval_Click;
            btnCamReq.Click += BtnCamReq_Click;
            // 파일업로드
            btnFileUpload.Click += BtnFileUpload_Click;

            btnCNCData.Click += BtnCNCData_Click;

            btnNew.Click += BtnNew_Click;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {

            smartSplitTableLayoutPanel4.Enabled = true;

            SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("GOVERNANCENO");
            Popupedit.SetValue("");
            Popupedit.Text = "";
            Popupedit.Refresh();

            // 그리드 초기화
            DataTable dtGovernanceClear = (DataTable)grdGovernance.DataSource;
            dtGovernanceClear.Clear();

            DataTable dtGovernanceSiteClear = (DataTable)grdGovernanceSite.DataSource;
            dtGovernanceSiteClear.Clear();

            DataTable dtGovernaceList = new DataTable();
            SetControlsFrom ControlsFrom = new SetControlsFrom();
            ControlsFrom.SetControlsFromTable(smartSplitTableLayoutPanel4, dtGovernaceList);


            //fnSearch();

        }

        private void BtnCNCData_Click(object sender, EventArgs e)
        {
            if (grdGovernance.View.FocusedRowHandle < 0)
                return;

            DataRow row = this.grdGovernance.View.GetFocusedDataRow();

            if (row.RowState == DataRowState.Unchanged)
            {
                Dictionary<string, object> ParamRc = new Dictionary<string, object>();
                ParamRc.Add("GOVERNANCENO", row["GOVERNANCENO"].ToString());
                this.OpenMenu("PG-SD-0241", ParamRc);
            }
            else
            {
                ShowMessage("NotSaveUnchanged");
            }
        }




        private void BtnFileUpload_Click(object sender, EventArgs e)
        {

            DataRow row = this.grdGovernance.View.GetFocusedDataRow();

            if (row.RowState == DataRowState.Unchanged)
            {
                ItemMasterfilePopup pis = new ItemMasterfilePopup(row["GOVERNANCENO"].ToString(), "", "Governance", "GovernanceMgnt / Governance");
                pis.ShowDialog();
            }
            else
            {
                ShowMessage("NotSaveUnchanged");
            }

        }
        private void grdGovernanceSite_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            DataRow row = this.grdGovernanceSite.View.GetFocusedDataRow();
            if( row != null)
            {
                switch (row["STATE"].ToString())
                {
                    case "Approved":
                    case "Complete":
                        grdGovernanceSite.View.SetIsReadOnly(true);
                        break;
                    default:
                        grdGovernanceSite.View.SetIsReadOnly(false);
                        break;
                }
            }
            
        }

      

        private void grdGovernanceSite_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataTable dt = (DataTable)grdGovernanceSite.DataSource;
            if(dt != null)
            {
                if(dt.Rows.Count != 0)
                {
                    if( dt.Select("1=1","SEQUENCE DESC")[0]["SEQUENCE"].ToString() !="")
                    {
                        args.NewRow["SEQUENCE"] = decimal.Parse(dt.Select("1=1", "SEQUENCE DESC")[0]["SEQUENCE"].ToString()) + 1;
                    }
                    else
                    {
                        args.NewRow["SEQUENCE"] = 1;
                        args.NewRow["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
                        args.NewRow["SITESPECPERSONNAME"] = sspSPECIFICATIONMAN.Text;
                    }
                }
                else
                {
                    args.NewRow["SEQUENCE"] = 1;
                    args.NewRow["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
                    args.NewRow["SITESPECPERSONNAME"] = sspSPECIFICATIONMAN.Text;
                }
            }
            else
            {
                args.NewRow["SEQUENCE"] = 1;
                args.NewRow["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
                args.NewRow["SITESPECPERSONNAME"] = sspSPECIFICATIONMAN.Text;
            }
        }

        private void BtnCamReq_Click(object sender, EventArgs e)
        {

            
            DataRow rowGovernance = grdGovernance.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("GOVERNANCENO", rowGovernance["GOVERNANCENO"].ToString());


            DataTable dtCamRequest = SqlExecuter.Query("GetCamrequestChk", "10001", param);

            if (dtCamRequest.Rows.Count < 1) // 
            {



                DataTable dtCamrequest = new DataTable();
                dtCamrequest.Columns.Add("CAMREQUESTID");
                dtCamrequest.Columns.Add("ENTERPRISEID");
                dtCamrequest.Columns.Add("PLANTID");
                dtCamrequest.Columns.Add("GOVERNANCENO");
                dtCamrequest.Columns.Add("DEPARTMENT");
                dtCamrequest.Columns.Add("PRODUCTDEFID");
                dtCamrequest.Columns.Add("PRODUCTDEFVERSION");
                dtCamrequest.Columns.Add("MODELNO");
                dtCamrequest.Columns.Add("SPECPERSON");
                dtCamrequest.Columns.Add("CAMPERSON");
                dtCamrequest.Columns.Add("WORKTYPE");
                dtCamrequest.Columns.Add("PRODUCTTYPE");
                dtCamrequest.Columns.Add("REASON");
                dtCamrequest.Columns.Add("PRIORITY");
                //dtCamrequest.Columns.Add("REQUESTER");
                //dtCamrequest.Columns.Add("REQUESTDATE");
                dtCamrequest.Columns.Add("CUSTOMERNAME");
                dtCamrequest.Columns.Add("PANELSIZE");
                dtCamrequest.Columns.Add("STATUS");

                dtCamrequest.Columns.Add("_STATE_");


                //DataRow rowProduct = grdGovernanceProduct.View.GetFocusedDataRow();


                //if (rowProduct["PRODUCTDEFID"].ToString() == "")
                //{
                //    ShowMessage("PRODUCTDEFID");
                //}

                if (rowGovernance["SPECPERSON"].ToString() == "")
                {
                    ShowMessage("Specperson");
                }


                // 채번 시리얼 존재 유무 체크
                DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
                string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);

                DataRow row = dtCamrequest.NewRow();

                // 채번 시리얼 
                GetNumber number = new GetNumber();

                row["CAMREQUESTID"] = number.GetStdNumber("Camrequestid", "CAM" + sdate);
                row["ENTERPRISEID"] = rowGovernance["ENTERPRISEID"];
                row["PLANTID"] = rowGovernance["PLANTID"];
                row["GOVERNANCENO"] = rowGovernance["GOVERNANCENO"];
                row["DEPARTMENT"] = "";// rowGovernance["DEPARTMENT"];

                row["PRODUCTDEFID"] = rowGovernance["PRODUCTDEFID"];
                row["PRODUCTDEFVERSION"] = rowGovernance["PRODUCTDEFVERSION"];




                row["MODELNO"] = rowGovernance["MODELNO"];
                row["SPECPERSON"] = rowGovernance["SPECPERSON"];
                row["CAMPERSON"] = rowGovernance["CAMPERSON"];
                row["WORKTYPE"] = rowGovernance["WORKCLASS"];
                row["PRODUCTTYPE"] = rowGovernance["PRODUCTCLASS"];
                row["REASON"] = "";// rowGovernance["REASON"];
                row["PRIORITY"] = rowGovernance["PRIORITY"];
                //row["REQUESTER"] = "";//UserInfo.Current.Id;
                //row["REQUESTDATE"] = ""; //DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                row["CUSTOMERNAME"] = rowGovernance["CUSTOMERNAME"];
                row["PANELSIZE"] = rowGovernance["PANELSIZE"];
                row["STATUS"] = "Working";
                row["_STATE_"] = "added";
                dtCamrequest.Rows.Add(row);


                rowGovernance["CAMREQUESTID"] = dtCamrequest.Rows[0]["CAMREQUESTID"].ToString();
                DataTable changed = grdGovernance.GetChangedRows();
                DataSet ds = new DataSet();
                changed.TableName = "governance";

                dtCamrequest.TableName = "camrequest";
                ds.Tables.Add(changed);
                ds.Tables.Add(dtCamrequest);

                ExecuteRule("Governance", ds);

                // 커밋
                DataTable dt = (DataTable)grdGovernance.DataSource;
                dt.AcceptChanges();
                ShowMessage("Camrequest");

                Dictionary<string, object> ParamRc = new Dictionary<string, object>();
                ParamRc.Add("CAMREQUESTID", rowGovernance["CAMREQUESTID"].ToString());
                this.OpenMenu("PG-SD-0271", ParamRc);


            }
            else
            {
                Dictionary<string, object> ParamRc = new Dictionary<string, object>();
                ParamRc.Add("CAMREQUESTID", rowGovernance["CAMREQUESTID"].ToString());
                this.OpenMenu("PG-SD-0271", ParamRc);
            }
        }

        private void BtnRequestApproval_Click(object sender, EventArgs e)
        {
           

            DataTable dtApproval = new DataTable();
            dtApproval.Columns.Add("APPROVALTYPE");
            dtApproval.Columns.Add("APPROVALID");
            dtApproval.Columns.Add("ENTERPRISEID");
            dtApproval.Columns.Add("PLANTID");
            dtApproval.Columns.Add("APPROVALSTATUS");
            dtApproval.Columns.Add("REQUESTOR");
            dtApproval.Columns.Add("REQUESTDATE");
            dtApproval.Columns.Add("APPROVER");
            dtApproval.Columns.Add("_STATE_");

            DataTable Approvaltransaction = new DataTable();
            Approvaltransaction.Columns.Add("APPROVALTYPE");
            Approvaltransaction.Columns.Add("APPROVALID");
            Approvaltransaction.Columns.Add("ENTERPRISEID");
            Approvaltransaction.Columns.Add("PLANTID");
            Approvaltransaction.Columns.Add("SEQUENCE");
            Approvaltransaction.Columns.Add("RESULTS");
            Approvaltransaction.Columns.Add("RESULTTYPE");
            Approvaltransaction.Columns.Add("ACTOR");
            Approvaltransaction.Columns.Add("STARTDATE");
            Approvaltransaction.Columns.Add("ENDDATE");
            Approvaltransaction.Columns.Add("DESCRIPTION");
            Approvaltransaction.Columns.Add("VALIDSTATE");
            Approvaltransaction.Columns.Add("_STATE_");

            GetControlsFrom confrom = new GetControlsFrom();

            DataTable dtChk = (DataTable)grdGovernance.DataSource;


            DataTable dtChkCopy = dtChk.Clone();

            dtChkCopy.Columns["STARTDATE"].DataType = typeof(string);
            dtChkCopy.Columns["IMPLEMENTATIONDATE"].DataType = typeof(string);
            dtChkCopy.Columns["REQUESTDATE"].DataType = typeof(string);
            dtChkCopy.Columns["ERPITEMDATE"].DataType = typeof(string);
            dtChkCopy.Columns["MODELDELIVERYDATE"].DataType = typeof(string);
            dtChkCopy.Columns["PCRDATE"].DataType = typeof(string);
            

            DataRow dataRowChkCopy = dtChkCopy.NewRow();

            foreach (DataRow rowChk in dtChk.Rows)
            {
                foreach (DataColumn col in dtChk.Columns)
                {
                    switch(col.ColumnName)
                    {
                        case "STARTDATE":
                        case "IMPLEMENTATIONDATE":
                        case "REQUESTDATE":
                        case "ERPITEMDATE":
                        case "MODELDELIVERYDATE":
                        case "PCRDATE":
                            if(rowChk[col.ColumnName].ToString() != "")
                            {
                                dataRowChkCopy[col.ColumnName] = rowChk[col.ColumnName].ToString().Substring(0, 10);
                            }
                            else
                            {
                                dataRowChkCopy[col.ColumnName] = "";
                            }
                            break;
                        default:
                            dataRowChkCopy[col.ColumnName] = rowChk[col.ColumnName].ToString();
                            break;
                    }
                    
                }
            }
            dtChkCopy.Rows.Add(dataRowChkCopy);
            
            grdGovernance.DataSource = dtChkCopy;

            confrom.GetControlsFromGrid(smartSplitTableLayoutPanel4, grdGovernance);

            // 변경된 내역이 존재 합니다.
            DataTable changed = grdGovernance.GetChangedRows();
            if (changed != null)
            {
                if (changed.Rows.Count != 0)
                {
                    ShowMessage("NotSaveUnchanged");
                    return;
                }
            }

            // 변경된 내역이 존재 합니다.
            DataTable dtchange = grdGovernanceSite.GetChangedRows();
            if (dtchange != null)
            {
                if (dtchange.Rows.Count != 0)
                {
                    ShowMessage("NotSaveUnchanged");
                    return;
                }
            }


            DataRow rowData = grdGovernanceSite.View.GetFocusedDataRow();

            if (rowData["APPROVEPERSON"].ToString() == "")
            {
                ShowMessage("APPROVEPERSON");
                return;
            }


            // 승인요청 여부 (반려,작업,요청취소 불가)
            switch(rowData["STATE"].ToString())
            {
                case "Reject":
                case "Working":
                case "CancelRequest":
                case "":
                    break;
                default:
                    ShowMessage("RequestApprovalState");
                    return;
                    
            }

            DataSet ds = new DataSet();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", rowData["ENTERPRISEID"].ToString());
            param.Add("APPROVALID", rowData["GOVERNANCENO"].ToString());
            DataTable dtApprovalChk = SqlExecuter.Query("GetGovernanceStatusTransactionList", "10001", param);

            DataRow row = dtApproval.NewRow();
            row["APPROVALTYPE"] = "NewRequest";
            row["APPROVALID"] = rowData["GOVERNANCENO"];
            row["ENTERPRISEID"] = rowData["ENTERPRISEID"];
            row["PLANTID"] = rowData["PLANTID"];
            row["APPROVALSTATUS"] = "RequestApproval";

            row["REQUESTOR"] = UserInfo.Current.Id;
            row["REQUESTDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            if(dtApprovalChk != null)
            {
                if (dtApprovalChk.Rows.Count == 0)
                {
                    row["APPROVER"] = rowData["APPROVEPERSON"];
                    row["_STATE_"] = "added";
                    dtApproval.Rows.Add(row);
                }
                else
                {
                    row["APPROVER"] = rowData["APPROVEPERSON"];
                    row["_STATE_"] = "modified";
                    dtApproval.Rows.Add(row);
                }
            }
            else
            {
                row["APPROVER"] = rowData["APPROVEPERSON"];
                row["_STATE_"] = "added";
                dtApproval.Rows.Add(row);
            }

            


            DataRow rowAt = Approvaltransaction.NewRow();
            rowAt["APPROVALTYPE"] = rowData["GOVERNANCETYPE"];
            rowAt["APPROVALID"] = rowData["GOVERNANCENO"];
            rowAt["ENTERPRISEID"] = rowData["ENTERPRISEID"];
            rowAt["PLANTID"] = rowData["PLANTID"];

            if (dtApprovalChk != null)
            {
                if (dtApprovalChk.Rows.Count != 0)
                {
                    rowAt["SEQUENCE"] = decimal.Parse(dtApprovalChk.Compute("MAX(SEQUENCE)", "").ToString()) + 1;
                }
                else
                {
                    rowAt["SEQUENCE"] = 1;
                }
            }
            else
            {
                rowAt["SEQUENCE"] = 1;
            }

            
            rowAt["RESULTS"] = "RequestApproval"; 
            rowAt["RESULTTYPE"] = "Requester";
            rowAt["ACTOR"] = UserInfo.Current.Id;

            if (dtApprovalChk != null)
            {
                if (dtApprovalChk.Rows.Count != 0)
                {
                    rowAt["STARTDATE"] = DateTime.Parse(dtApprovalChk.Compute("MAX(ENDDATE)", "").ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
            }

            rowAt["ENDDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            rowAt["_STATE_"] = "added";
            Approvaltransaction.Rows.Add(rowAt);


            rowData["REQUESTER"] = UserInfo.Current.Id;
            rowData["REQUESTDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            rowData["STATE"] = "RequestApproval";

            DataTable dtSite = grdGovernanceSite.GetChangedRows();

            Approvaltransaction.TableName = "approvaltransaction"; 
            dtApproval.TableName = "approval";
            dtSite.TableName = "governanceSite";

            


            ds.Tables.Add(Approvaltransaction);
            ds.Tables.Add(dtApproval);
            ds.Tables.Add(dtSite);
            ExecuteRule("GovernanceSite", ds);

            DataTable dt = (DataTable)grdGovernanceSite.DataSource;
            dt.AcceptChanges();
            ShowMessage("SuccedSave");

        }

        private void grdGovernance_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdGovernance.View.FocusedRowHandle < 0)
                return;

            // 그리드 초기화
            DataTable dtGovernanceSiteClear = (DataTable)grdGovernanceSite.DataSource;
            dtGovernanceSiteClear.Clear();
            //DataTable dtGovernanceProductClear = (DataTable)grdGovernanceProduct.DataSource;
            //dtGovernanceProductClear.Clear();


            DataRow dataRow = grdGovernance.View.GetFocusedDataRow();
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("ENTERPRISEID", dataRow["ENTERPRISEID"].ToString());
            param.Add("GOVERNANCENO", dataRow["GOVERNANCENO"].ToString());
            param.Add("GOVERNANCETYPE", dataRow["GOVERNANCETYPE"].ToString());

            // 사이트 
            DataTable dtGovernanceSite = SqlExecuter.Query("GetGovernacesite", "10001", param);
            grdGovernanceSite.DataSource = dtGovernanceSite;

            //DataTable dtGovernaceproduct = SqlExecuter.Query("GetGovernaceproduct", "10001", param);
            //grdGovernanceProduct.DataSource = dtGovernaceproduct;


            
            if (dtGovernanceSite.Select("STATE in ('Approved','Complete')").Length !=0)
            {
                smartSplitTableLayoutPanel4.Enabled = false;
            }

        }

        private void grdGovernanceProduct_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {

            DataRow dataRow = grdGovernance.View.GetFocusedDataRow();

            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = UserInfo.Current.Plant;
           



            if (dataRow != null)
            {
                if (dataRow["GOVERNANCENO"].ToString() != "")
                {
                    args.NewRow["GOVERNANCENO"] = dataRow["GOVERNANCENO"];
                    args.NewRow["GOVERNANCETYPE"] = dataRow["GOVERNANCETYPE"];
                }
            }
            

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



                //Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //param.Add("ASSEMBLYITEMID", row["ITEMID"].ToString());
                //param.Add("ASSEMBLYITEMVERSION", row["ITEMVERSION"].ToString());
                //DataTable dt = SqlExecuter.Query("GetRountingTree", "10003", param);

                //DataTable dtsiet = (DataTable)grdGovernanceSite.DataSource;

                //foreach (DataRow rownew in dt.Rows)
                //{
                //    if(dtsiet != null)
                //    {
                //        if (dtsiet.Select("PLANTID = '" + rownew["PLANTID"].ToString() + "'").Length == 0)
                //        {
                //            grdGovernanceSite.View.AddNewRow();
                //            DataRow rowFocused = grdGovernanceSite.View.GetFocusedDataRow();
                //            rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                //            rowFocused["PLANTID"] = rownew["PLANTID"];
                //            rowFocused["GOVERNANCETYPE"] = "NewRequest";
                //            rowFocused["SEQUENCE"] = rownew["SEQ"];
                            
                //        }
                //    }
                //    else
                //    {
                //        grdGovernanceSite.View.AddNewRow();
                //        DataRow rowFocused = grdGovernanceSite.View.GetFocusedDataRow();
                //        rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                //        rowFocused["PLANTID"] = rownew["PLANTID"];
                //        rowFocused["GOVERNANCETYPE"] = "NewRequest";
                //        rowFocused["SEQUENCE"] = rownew["SEQ"];

                //    }
                   
                //}
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

        /// <summary>
        /// 팝업 컬럼에서 확인 클릭 시 다른 데이터 매핑 ( 사양담당자 )
        /// </summary>
        /// <param name="currentGridRow">화면 grid row</param>
        /// <param name="popupSelections">팝업에서 선택한 row</param>
        /// <returns></returns>
        private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationSiteSpecpersonPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
        {
            Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

            foreach (DataRow row in popupSelections)
            {
                currentGridRow["SITESPECPERSONNAME"] = row["USERNAME"];
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
