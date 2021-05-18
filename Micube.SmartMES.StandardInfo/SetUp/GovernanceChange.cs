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
	public partial class GovernanceChange : SmartConditionManualBaseForm
    {
        #region Local Variables
        string fgSearch = "";

        private ConditionItemTextBox productDefIdBox;
        private ConditionItemTextBox productDefVersionBox;

        #endregion

        #region 생성자
        public GovernanceChange()
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
            //cboPRODUCTCLASS.DisplayMember = "CODENAME";
            //cboPRODUCTCLASS.ValueMember = "CODEID";
            //cboPRODUCTCLASS.ShowHeader = false;
            //Dictionary<string, object> ParamConfirmation = new Dictionary<string, object>();
            //ParamConfirmation.Add("CODECLASSID", "ProductionType");
            //ParamConfirmation.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtConfirmation = SqlExecuter.Query("GetCodeList", "00001", ParamConfirmation);
            //cboPRODUCTCLASS.DataSource = dtConfirmation;

            // 적용구분
            cboIMPLEMENTATIONTYPE.DisplayMember = "CODENAME";
            cboIMPLEMENTATIONTYPE.ValueMember = "CODEID";
            cboIMPLEMENTATIONTYPE.ShowHeader = false;
            Dictionary<string, object> ParamIMPLEMENTATIONTYPE = new Dictionary<string, object>();
            ParamIMPLEMENTATIONTYPE.Add("CODECLASSID", "ImplementationType");
            ParamIMPLEMENTATIONTYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtIMPLEMENTATIONTYPE = SqlExecuter.Query("GetCodeList", "00001", ParamIMPLEMENTATIONTYPE);
            cboIMPLEMENTATIONTYPE.DataSource = dtIMPLEMENTATIONTYPE;


            cboPRIORITY.DisplayMember = "CODENAME";
            cboPRIORITY.ValueMember = "CODEID";
            cboPRIORITY.ShowHeader = false;
            Dictionary<string, object> ParamPRIORITY = new Dictionary<string, object>();
            ParamPRIORITY.Add("CODECLASSID", "Priority");
            ParamPRIORITY.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPRIORITY = SqlExecuter.Query("GetCodeList", "00001", ParamPRIORITY);
            cboPRIORITY.DataSource = dtPRIORITY;

            



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


            //PRC 담당자
            ConditionItemSelectPopup cisiPCRREQUESTER = new ConditionItemSelectPopup();
            cisiPCRREQUESTER.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiPCRREQUESTER.SetPopupLayout("PCRREQUESTER", PopupButtonStyles.Ok_Cancel);
            cisiPCRREQUESTER.Id = "PCRREQUESTER";
            cisiPCRREQUESTER.LabelText = "PCRREQUESTER";
            cisiPCRREQUESTER.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiPCRREQUESTER.IsMultiGrid = false;
            cisiPCRREQUESTER.DisplayFieldName = "USERNAME";
            cisiPCRREQUESTER.ValueFieldName = "USERID";
            cisiPCRREQUESTER.LanguageKey = "PCRREQUESTER";
            cisiPCRREQUESTER.Conditions.AddTextBox("USERNAME");
            cisiPCRREQUESTER.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiPCRREQUESTER.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspPCRNO.SelectPopupCondition = cisiPCRREQUESTER;


            //PCN 
            ConditionItemSelectPopup cisiPCRNO = new ConditionItemSelectPopup();
            cisiPCRNO.SetPopupLayoutForm(800, 500, FormBorderStyle.FixedDialog);
            cisiPCRNO.SetPopupLayout("CHANGEPOINTNO", PopupButtonStyles.Ok_Cancel);
            cisiPCRNO.Id = "CHANGEPOINTNO";
            cisiPCRNO.LabelText = "CHANGEPOINTNO";
            cisiPCRNO.SearchQuery = new SqlQuery("GetChangePointPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisiPCRNO.IsMultiGrid = false;
            cisiPCRNO.DisplayFieldName = "CHANGEPOINTNO";
            cisiPCRNO.ValueFieldName = "CHANGEPOINTNO";
            cisiPCRNO.LanguageKey = "CHANGEPOINTNO";



            cisiPCRNO.Conditions.AddTextBox("CHANGEPOINTNO");
            cisiPCRNO.Conditions.AddTextBox("CHANGEDETAILS");
            productDefIdBox = cisiPCRNO.Conditions.AddTextBox("PRODUCTDEFID").SetIsHidden();
            productDefVersionBox = cisiPCRNO.Conditions.AddTextBox("PRODUCTDEFVERSION").SetIsHidden();

            cisiPCRNO.GridColumns.AddTextBoxColumn("CHANGEPOINTNO", 150);
            cisiPCRNO.GridColumns.AddTextBoxColumn("CHANGEDETAILS", 200);

            sspPCRNO.SelectPopupCondition = cisiPCRNO;

            sspPCRNO.SelectPopupCondition
              .SetPopupApplySelection((selectedRows, dataGridRow) =>
              {
                // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                  foreach (DataRow row in selectedRows)
                  {
                      txtPCRREQUESTER.Text = row["CREATOR"].ToString();
                      txtPCRREQUESTERNAME.Text = row["CREATORNAME"].ToString();
                      dtPCRDATE.Text = row["REQUESTDATE"].ToString();
                  }
              });


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
            //cisiPRODUCTDEFID.GridColumns.AddTextBoxColumn("UOMDEFNAME", 100);

            sspPRODUCTDEFID.SelectPopupCondition = cisiPRODUCTDEFID;

            sspPRODUCTDEFID.SelectPopupCondition
              .SetPopupApplySelection((selectedRows, dataGridRow) =>
              {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                  foreach (DataRow row in selectedRows)
                  {
                      txtPRODUCTDEFVERSION.Text = row["ITEMVERSION"].ToString();
                      txtPRODUCTDEFNAME.Text = row["ITEMNAME"].ToString();

                      productDefIdBox.SetDefault(row["ITEMID"].ToString());
                      productDefVersionBox.SetDefault(row["ITEMVERSION"].ToString());

                  }
              });





            //sspPRODUCTDEFID.Validated += SspPRODUCTDEFID_Validated;
            //sspPRODUCTDEFID.TextChanged += SspPRODUCTDEFID_TextChanged;

            //cboITEMVERSION.TextChanged += CboITEMVERSION_TextChanged;

            // to 제품팝업

            ConditionItemSelectPopup cisiRCPRODUCTDEFID = new ConditionItemSelectPopup();
            cisiRCPRODUCTDEFID.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiRCPRODUCTDEFID.SetPopupLayout("RCPRODUCTDEFID", PopupButtonStyles.Ok_Cancel);

            cisiRCPRODUCTDEFID.Id = "RCPRODUCTDEFID";
            cisiRCPRODUCTDEFID.LabelText = "RCPRODUCTDEFID";
            cisiRCPRODUCTDEFID.SearchQuery = new SqlQuery("GetSpecificationsItemPupop", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}");
            cisiRCPRODUCTDEFID.IsMultiGrid = false;
            cisiRCPRODUCTDEFID.DisplayFieldName = "ITEMID";
            cisiRCPRODUCTDEFID.ValueFieldName = "ITEMID";
            cisiRCPRODUCTDEFID.LanguageKey = "RCPRODUCTDEFID";
            cisiRCPRODUCTDEFID.Conditions.AddTextBox("ITEMID");
            cisiRCPRODUCTDEFID.Conditions.AddTextBox("ITEMVERSION");
            cisiRCPRODUCTDEFID.Conditions.AddTextBox("ITEMNAME");
            cisiRCPRODUCTDEFID.GridColumns.AddTextBoxColumn("ITEMID", 150);
            cisiRCPRODUCTDEFID.GridColumns.AddTextBoxColumn("ITEMVERSION", 200);
            cisiRCPRODUCTDEFID.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
            cisiRCPRODUCTDEFID.GridColumns.AddTextBoxColumn("UOMDEFID", 100);
            //cisiRCPRODUCTDEFID.GridColumns.AddTextBoxColumn("UOMDEFNAME", 100);

            sspRCPRODUCTDEFID.SelectPopupCondition = cisiRCPRODUCTDEFID;
            sspRCPRODUCTDEFID.SelectPopupCondition

              .SetPopupApplySelection((selectedRows, dataGridRow) =>
              {
                  // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                  // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                  foreach (DataRow row in selectedRows)
                  {
                      txtRCPRODUCTDEFVERSION.Text = row["ITEMVERSION"].ToString();
                      txtRCPRODUCTDEFNAME.Text = row["ITEMNAME"].ToString();
                  }
              });




           
         
           

            //sspRCPRODUCTDEFID.Validated += SspRCPRODUCTDEFID_Validated;
            //sspRCPRODUCTDEFID.TextChanged += SspRCPRODUCTDEFID_TextChanged;

            //cboITEMVERSION.TextChanged += CboITEMVERSION_TextChanged;


        }

        //private void SspRCPRODUCTDEFID_Validated(object sender, EventArgs e)
        //{
        //    string sItemcode = "";


        //    if (sspRCPRODUCTDEFID.SelectedData.Count<DataRow>() == 0)
        //    {
        //        sItemcode = "-1";
        //    }

        //    foreach (DataRow row in sspRCPRODUCTDEFID.SelectedData)
        //    {
        //        sItemcode = row["ITEMID"].ToString();

        //    }


        //    cboRCPRODUCTDEFVERSION.Reset();
        //    cboRCPRODUCTDEFVERSION.DisplayMember = "ITEMVERSIONNAME";
        //    cboRCPRODUCTDEFVERSION.ValueMember = "ITEMVERSIONCODE";
        //    cboRCPRODUCTDEFVERSION.ShowHeader = false;
        //    //cboITEMVERSION.EmptyItemValue = 0;
        //    //cboITEMVERSION.EmptyItemCaption = "";
        //    cboRCPRODUCTDEFVERSION.UseEmptyItem = true;

        //    Dictionary<string, object> ParamIv = new Dictionary<string, object>();
        //    ParamIv.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
        //    ParamIv.Add("PLANTID", UserInfo.Current.Plant);
        //    ParamIv.Add("ITEMID", sItemcode);
        //    DataTable dtIv = SqlExecuter.Query("GetItemVersion", "10001", ParamIv);

        //    cboRCPRODUCTDEFVERSION.DataSource = dtIv;
        //}

        //private void SspRCPRODUCTDEFID_TextChanged(object sender, EventArgs e)
        //{
        //    if (fgSearch != "Y")
        //    {
        //        cboRCPRODUCTDEFVERSION.ItemIndex = 0;
        //        cboRCPRODUCTDEFVERSION.EditValue = "";
        //        cboRCPRODUCTDEFVERSION.Text = "";
        //    }
        //}

        //private void SspPRODUCTDEFID_TextChanged(object sender, EventArgs e)
        //{
        //    if (fgSearch != "Y")
        //    {
        //        cboITEMVERSION.ItemIndex = 0;
        //        cboITEMVERSION.EditValue = "";
        //        cboITEMVERSION.Text = "";
        //    }
           
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
        //                rowFocused["GOVERNANCETYPE"] = "RunningChange";
        //                rowFocused["SEQUENCE"] = rownew["SEQ"];
        //            }
        //        }
        //        else
        //        {
        //            grdGovernanceSite.View.AddNewRow();
        //            DataRow rowFocused = grdGovernanceSite.View.GetFocusedDataRow();
        //            rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
        //            rowFocused["PLANTID"] = rownew["PLANTID"];
        //            rowFocused["GOVERNANCETYPE"] = "RunningChange";
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
            

            grdGovernance.View.AddDateEditColumn("STARTDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdGovernance.View.AddDateEditColumn("REQUESTDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            grdGovernance.View.AddDateEditColumn("IMPLEMENTATIONDATE", 100)
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            //grdGovernance.View.AddTextBoxColumn("ERPITEMDATE", 100)
            //.SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            //.SetIsReadOnly()
            //.SetTextAlignment(TextAlignment.Center);


            grdGovernance.View.AddTextBoxColumn("ISCNCATTACHED");
            grdGovernance.View.AddTextBoxColumn("CAMREQUESTID");
            grdGovernance.View.AddTextBoxColumn("CAMPERSON");
            grdGovernance.View.AddTextBoxColumn("PCRNO");
            grdGovernance.View.AddTextBoxColumn("PCRREQUESTER");

            grdGovernance.View.AddDateEditColumn("PCRDATE")
            .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            .SetIsReadOnly()
            .SetTextAlignment(TextAlignment.Center);

            //grdGovernance.View.AddTextBoxColumn("MODELDELIVERYDATE", 100)
            //.SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
            //.SetIsReadOnly()
            //.SetTextAlignment(TextAlignment.Center);

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
            grdGovernance.View.AddTextBoxColumn("RCPRODUCTDEFID");
            grdGovernance.View.AddTextBoxColumn("RCPRODUCTDEFVERSION");

            grdGovernance.View.PopulateColumns();

            //품목
            //grdGovernanceProduct.GridButtonItem = GridButtonItem.All;
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
           

            //사이트
            grdGovernanceSite.GridButtonItem = GridButtonItem.All;
            grdGovernanceSite.View.AddTextBoxColumn("GOVERNANCENO").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("GOVERNANCETYPE").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            
            grdGovernanceSite.View.AddTextBoxColumn("SEQUENCE").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("RECEIPTDATE").SetIsHidden();
            grdGovernanceSite.View.AddTextBoxColumn("STATE").SetIsHidden();
            //grdGovernanceSite.View.AddTextBoxColumn("SITESPECPERSON").SetIsHidden();

           
            if (UserInfo.Current.Enterprise == "INTERFLEX")
            {
                grdGovernanceSite.View.AddComboBoxColumn("PLANTID", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RoutingInterflex", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }
            else
            {
                grdGovernanceSite.View.AddComboBoxColumn("PLANTID", new SqlQuery("GetCodeList", "00001", "CODECLASSID=RoutingYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }

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

            // Lot
            //grdLot.GridButtonItem = GridButtonItem.None;
            //grdLot.View.AddTextBoxColumn("LOTID",250).SetIsReadOnly();
            //grdLot.View.PopulateColumns();

            grdPRODUCTDEFIDfr.GridButtonItem = GridButtonItem.None;
            grdPRODUCTDEFIDfr.View.SetIsReadOnly(true);
            grdPRODUCTDEFIDfr.View.AddTextBoxColumn("PLANTID", 60);
            grdPRODUCTDEFIDfr.View.AddTextBoxColumn("USERSEQUENCE", 60);
            grdPRODUCTDEFIDfr.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdPRODUCTDEFIDfr.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);
            grdPRODUCTDEFIDfr.View.AddComboBoxColumn("PROCESSCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPRODUCTDEFIDfr.View.AddComboBoxColumn("MATERIALCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPRODUCTDEFIDfr.View.AddComboBoxColumn("SPECCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPRODUCTDEFIDfr.View.AddComboBoxColumn("TOOLCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            
            grdPRODUCTDEFIDfr.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdPRODUCTDEFIDfr.View.PopulateColumns();

            grdPRODUCTDEFIDto.GridButtonItem = GridButtonItem.None;
            grdPRODUCTDEFIDto.View.SetIsReadOnly(true);
            grdPRODUCTDEFIDto.View.AddTextBoxColumn("PLANTID", 60);
            grdPRODUCTDEFIDto.View.AddTextBoxColumn("USERSEQUENCE", 60);
            grdPRODUCTDEFIDto.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdPRODUCTDEFIDto.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);
            grdPRODUCTDEFIDto.View.AddComboBoxColumn("PROCESSCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPRODUCTDEFIDto.View.AddComboBoxColumn("MATERIALCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPRODUCTDEFIDto.View.AddComboBoxColumn("SPECCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdPRODUCTDEFIDto.View.AddComboBoxColumn("TOOLCHANGETYPE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CompareType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));

            grdPRODUCTDEFIDto.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdPRODUCTDEFIDto.View.PopulateColumns();


            grdBomCompfr.GridButtonItem = GridButtonItem.None;
            grdBomCompfr.View.SetIsReadOnly(true);
            grdBomCompfr.View.AddTextBoxColumn("MATERIALDEFID", 100);
            grdBomCompfr.View.AddTextBoxColumn("MATERIALDEFNAME", 250);
            grdBomCompfr.View.AddTextBoxColumn("MATERIALDEFVERSION", 100);
            grdBomCompfr.View.AddSpinEditColumn("QTY", 100)
                .SetDisplayFormat("#,##0.#########");
            grdBomCompfr.View.PopulateColumns();

            grdBomCompto.GridButtonItem = GridButtonItem.None;
            grdBomCompto.View.SetIsReadOnly(true);
            grdBomCompto.View.AddTextBoxColumn("MATERIALDEFID", 100);
            grdBomCompto.View.AddTextBoxColumn("MATERIALDEFNAME", 250);
            grdBomCompto.View.AddTextBoxColumn("MATERIALDEFVERSION", 100);
            grdBomCompto.View.AddSpinEditColumn("QTY", 100)
                .SetDisplayFormat("#,##0.#########");
            grdBomCompto.View.PopulateColumns();



            grdSpecattributefr.GridButtonItem = GridButtonItem.None;
            grdSpecattributefr.View.SetIsReadOnly(true);
            grdSpecattributefr.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdSpecattributefr.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSpecattributefr.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSpecattributefr.View.AddTextBoxColumn("SPECSEQUENCE").SetIsHidden();
            grdSpecattributefr.View.AddComboBoxColumn("INSPECTIONDEFID", 100, new SqlQuery("GetInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID").SetIsHidden();
            grdSpecattributefr.View.AddComboBoxColumn("INSPITEMCLASSID", 100, new SqlQuery("GetInspitemClassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSNAME", "INSPITEMCLASSID").SetIsHidden();
            grdSpecattributefr.View.AddTextBoxColumn("INSPITEMID", 100).SetIsHidden();
            grdSpecattributefr.View.AddTextBoxColumn("INSPITEMNAME", 250).SetIsReadOnly();
            grdSpecattributefr.View.AddTextBoxColumn("YN_AOI", 100).SetIsReadOnly();
            grdSpecattributefr.View.AddTextBoxColumn("DESCRIPTION", 250).SetIsReadOnly();
            grdSpecattributefr.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSpecattributefr.View.PopulateColumns();

            grdSpecattributeto.GridButtonItem = GridButtonItem.None;
            grdSpecattributeto.View.SetIsReadOnly(true);
            grdSpecattributeto.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            grdSpecattributeto.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            grdSpecattributeto.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            grdSpecattributeto.View.AddTextBoxColumn("SPECSEQUENCE").SetIsHidden();
            grdSpecattributeto.View.AddComboBoxColumn("INSPECTIONDEFID", 100, new SqlQuery("GetInspectionDefinitionCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPECTIONDEFNAME", "INSPECTIONDEFID").SetIsHidden();
            grdSpecattributeto.View.AddComboBoxColumn("INSPITEMCLASSID", 100, new SqlQuery("GetInspitemClassCombo", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "INSPITEMCLASSNAME", "INSPITEMCLASSID").SetIsHidden();
            grdSpecattributeto.View.AddTextBoxColumn("INSPITEMID", 100).SetIsHidden();
            grdSpecattributeto.View.AddTextBoxColumn("INSPITEMNAME", 250).SetIsReadOnly();
            grdSpecattributeto.View.AddTextBoxColumn("YN_AOI", 100).SetIsReadOnly();
            grdSpecattributeto.View.AddTextBoxColumn("DESCRIPTION", 250).SetIsReadOnly();
            grdSpecattributeto.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdSpecattributeto.View.PopulateColumns();

            //자원관리
            grdToolfr.GridButtonItem = GridButtonItem.None;
            grdToolfr.View.SetIsReadOnly(true);
            grdToolfr.View.AddTextBoxColumn("TOOLCODE", 100).SetIsReadOnly();
            grdToolfr.View.AddTextBoxColumn("TOOLNAME", 100).SetIsReadOnly();
            grdToolfr.View.AddTextBoxColumn("TOOLVERSION", 100).SetIsReadOnly();
            grdToolfr.View.PopulateColumns();


            //자원관리
            grdToolto.GridButtonItem = GridButtonItem.None;
            grdToolto.View.SetIsReadOnly(true);
            grdToolto.View.AddTextBoxColumn("TOOLCODE", 100).SetIsReadOnly();
            grdToolto.View.AddTextBoxColumn("TOOLNAME", 100).SetIsReadOnly();
            grdToolto.View.AddTextBoxColumn("TOOLVERSION", 100).SetIsReadOnly();
            grdToolto.View.PopulateColumns();


            //제품변경이력
            grdGovernanceproductHis.GridButtonItem = GridButtonItem.None;
            grdGovernanceproductHis.View.SetIsReadOnly(true);
            grdGovernanceproductHis.View.AddTextBoxColumn("ITEMCODE", 100).SetIsReadOnly();
            grdGovernanceproductHis.View.AddTextBoxColumn("CUSTOMERREV", 100).SetIsReadOnly();
            grdGovernanceproductHis.View.AddTextBoxColumn("ITEMVERSION", 100).SetIsReadOnly();
            grdGovernanceproductHis.View.AddTextBoxColumn("CREATEDTIME", 100).SetIsReadOnly();
            grdGovernanceproductHis.View.AddComboBoxColumn("GOVERNANCETYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ApprovalType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            grdGovernanceproductHis.View.PopulateColumns();


            


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

            .SetPopupValidationCustom(ValidationSiteSpecPersonPopup);

            // 팝업에서 사용할 조회조건 항목 추가

            parentUser.Conditions.AddTextBox("USERNAME");

            // 팝업 그리드 설정
            parentUser.GridColumns.AddTextBoxColumn("USERID", 150);
            parentUser.GridColumns.AddTextBoxColumn("USERNAME", 100);


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

            // 사이트 
            DataTable dtSite = grdGovernanceSite.GetChangedRows();

            DataTable changed = new DataTable();
            GetControlsFrom confrom = new GetControlsFrom();

            // 채번 시리얼 존재 유무 체크
            DataTable dtDate = SqlExecuter.Query("GetItemId", "10001");
            string sdate = dtDate.Rows[0]["ITEMID"].ToString().Substring(0, 8);


            Dictionary<string, object> parampf = new Dictionary<string, object>();
            parampf.Add("IDCLASSID", "CRGovernance");
            parampf.Add("PREFIX", "RC" + sdate);
            DataTable dtItemserialChk = SqlExecuter.Query("GetProductitemserial", "10001", parampf);

            DataTable dtItemserialI = dtItemserialChk.Clone();
            dtItemserialI.Columns.Add("_STATE_");


            if (dtItemserialChk != null)
            {
                if (dtItemserialChk.Rows.Count == 0)
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = "CRGovernance";
                    rowItemserialI["PREFIX"] = "RC" + sdate;
                    rowItemserialI["LASTSERIALNO"] = "00001";
                    rowItemserialI["_STATE_"] = "added";
                    dtItemserialI.Rows.Add(rowItemserialI);


                }
                else
                {
                    DataRow rowItemserialI = dtItemserialI.NewRow();
                    rowItemserialI["IDCLASSID"] = "CRGovernance";
                    rowItemserialI["PREFIX"] = "RC" + sdate;

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
                rowItemserialI["IDCLASSID"] = "CRGovernance";
                rowItemserialI["PREFIX"] = "RC" + sdate;
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
                row["GOVERNANCETYPE"] = "RunningChange";
                row["STATUS"] = "Working";
                //txtPRIORITY.Text = "A";
                txtGOVERNANCENO.Text = dtItemserialI.Rows[0]["PREFIX"].ToString() + dtItemserialI.Rows[0]["LASTSERIALNO"].ToString();
            }

            confrom.GetControlsFromGrid(smartSplitTableLayoutPanel4, grdGovernance);
            changed = grdGovernance.GetChangedRows();

            // 변경등록
            if(changed != null)
            {
                if(changed.Rows.Count !=0)
                {
                    DataSet ds = new DataSet();
                    changed.TableName = "governance";
                    dtItemserialI.TableName = "idclassserial";

                    foreach (DataRow row in changed.Rows)
                    {
                        if(row["STARTDATE"].ToString() !="")
                        {
                            row["STARTDATE"] = DateTime.Parse(row["STARTDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if(row["PCRDATE"].ToString() != "")
                        {
                            row["PCRDATE"] = DateTime.Parse(row["PCRDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if (row["IMPLEMENTATIONDATE"].ToString() != "")
                        {
                            row["IMPLEMENTATIONDATE"] = DateTime.Parse(row["IMPLEMENTATIONDATE"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }

                        if (row["_STATE_"].ToString() == "added")
                        {
                            row["DEPARTMENT"] = UserInfo.Current.Department;
                        }

                    }


                    ds.Tables.Add(changed);
                    ds.Tables.Add(dtItemserialI);
                    ExecuteRule("Governance", ds);

                    if (changed.Rows[0].RowState == DataRowState.Added)
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

                        dtDelete.Rows[0]["PRODUCTDEFID"] = dtDelete.Rows[0]["BF_PRODUCTDEFID"];
                        dtDelete.Rows[0]["PRODUCTDEFVERSION"] = dtDelete.Rows[0]["BF_PRODUCTDEFVERSION"];

                        dtDelete.Rows[0]["RCPRODUCTDEFID"] = dtDelete.Rows[0]["BF_RCPRODUCTDEFID"];
                        dtDelete.Rows[0]["RCPRODUCTDEFVERSION"] = dtDelete.Rows[0]["BF_RCPRODUCTDEFVERSION"];


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
                               // rowSite["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
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
                               // rowSite["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
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
                            rowSite["SITESPECPERSON"] = sspSPECIFICATIONMAN.GetValue();
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


        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeConditionItem_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductItemMaster", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMNAME", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetValidationIsRequired()
               ;

            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMVERSION");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");
            
            
            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 60);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);
        }


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
            fnSearch();
            //fnSearch1();

        }


        private void fnSearch()
        {
            // 그리드 초기화
            DataTable dtGovernanceClear = (DataTable)grdGovernance.DataSource;
            dtGovernanceClear.Clear();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            DataTable dtGovernaceList = SqlExecuter.Query("GetGovernaceChangeList", "10001", values);
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


                //cboRCPRODUCTDEFVERSION.DisplayMember = "ITEMVERSIONNAME";
                //cboRCPRODUCTDEFVERSION.ValueMember = "ITEMVERSIONCODE";
                //cboRCPRODUCTDEFVERSION.ShowHeader = false;

                //Dictionary<string, object> ParamRc = new Dictionary<string, object>();
                //ParamRc.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //ParamRc.Add("PLANTID", UserInfo.Current.Plant);
                //ParamRc.Add("ITEMID", dtGovernaceList.Rows[0]["RCPRODUCTDEFID"].ToString());
                //DataTable dtRc = SqlExecuter.Query("GetItemVersion", "10001", ParamRc);

                //cboRCPRODUCTDEFVERSION.DataSource = dtRc;


            }
            SetControlsFrom ControlsFrom = new SetControlsFrom();
            ControlsFrom.SetControlsFromTable(smartSplitTableLayoutPanel4, dtGovernaceList);
            fgSearch = "Y";


            ControlsFrom.SetControlsFromTable(smartSplitTableLayoutPanel6, dtGovernaceList);
            ControlsFrom.SetControlsFromTable(smartSplitTableLayoutPanel7, dtGovernaceList);



            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PRODUCTDEFID", txtPRODUCTDEFIDfr.Text);
            DataTable dtGovernanceproductHis = SqlExecuter.Query("GetGovernanceproductHis", "10001", Param);
            grdGovernanceproductHis.DataSource = dtGovernanceproductHis;


            Dictionary<string, object> ParamRoutingcomparefr = new Dictionary<string, object>();
            ParamRoutingcomparefr.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamRoutingcomparefr.Add("GOVERNANCENO", values["GOVERNANCENO"]);
            ParamRoutingcomparefr.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ParamRoutingcomparefr.Add("ISFROMTO", "FROM");
            DataTable dtRoutingcomparefr = SqlExecuter.Query("GetRoutingcompare", "10001", ParamRoutingcomparefr);
            grdPRODUCTDEFIDfr.DataSource = dtRoutingcomparefr;

            Dictionary<string, object> ParamRoutingcompareto = new Dictionary<string, object>();
            ParamRoutingcompareto.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamRoutingcompareto.Add("GOVERNANCENO", values["GOVERNANCENO"]);
            ParamRoutingcompareto.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            ParamRoutingcompareto.Add("ISFROMTO", "TO");
            DataTable dtRoutingcompareto = SqlExecuter.Query("GetRoutingcompare", "10001", ParamRoutingcompareto);
            grdPRODUCTDEFIDto.DataSource = dtRoutingcompareto;


        }

        private void fnSearch1()
        {
            // 그리드 초기화
            DataTable dtGovernanceClear = (DataTable)grdGovernance.DataSource;
            dtGovernanceClear.Clear();

            // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴

            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PRODUCTDEFID", txtPRODUCTDEFIDfr.Text);

            DataTable dtGovernanceproductHis = SqlExecuter.Query("GetGovernanceproductHis", "10001", Param);
            grdGovernanceproductHis.DataSource = dtGovernanceproductHis;

            Dictionary<string, object> ParamFrom = new Dictionary<string, object>();
            ParamFrom.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamFrom.Add("PROCESSDEFID", txtPRODUCTDEFIDfr.Text);
            ParamFrom.Add("PROCESSDEFVERSION", txtPRODUCTDEFVERSIONfr.Text);
            ParamFrom.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtProcessPathListFrom = SqlExecuter.Query("GetProcessPathList", "10001", ParamFrom);

            Dictionary<string, object> ParamTo = new Dictionary<string, object>();
            ParamTo.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamTo.Add("PROCESSDEFID", txtRCPRODUCTDEFIDto.Text);
            ParamTo.Add("PROCESSDEFVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            ParamTo.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtProcessPathListTo = SqlExecuter.Query("GetProcessPathList", "10001", ParamTo);

            //공정
            dtProcessPathListFrom.Columns.Add("PROCESSCHANGETYPE");
            dtProcessPathListTo.Columns.Add("PROCESSCHANGETYPE");
            //자재
            dtProcessPathListFrom.Columns.Add("MATERIALCHANGETYPE");
            dtProcessPathListTo.Columns.Add("MATERIALCHANGETYPE");
            //SPEC
            dtProcessPathListFrom.Columns.Add("SPECCHANGETYPE");
            dtProcessPathListTo.Columns.Add("SPECCHANGETYPE");
            //Tool
            dtProcessPathListFrom.Columns.Add("TOOLCHANGETYPE");
            dtProcessPathListTo.Columns.Add("TOOLCHANGETYPE");
            

            foreach (DataRow row in dtProcessPathListTo.Rows)
            {
                if (dtProcessPathListFrom.Select("USERSEQUENCE = " + row["USERSEQUENCE"].ToString() + " ").Length == 0)
                {
                    DataRow rowNewRow = dtProcessPathListFrom.NewRow();
                    rowNewRow["USERSEQUENCE"] = row["USERSEQUENCE"];
                    dtProcessPathListFrom.Rows.Add(rowNewRow);
                }

                if (dtProcessPathListFrom.Select("USERSEQUENCE = "+ row["USERSEQUENCE"].ToString() + " AND PLANTID  = '" + row["PLANTID"].ToString() + "' ").Length ==0)
                {
                  
                    row["PROCESSCHANGETYPE"] = "Add";
                    //row["DESCRIPTION"] = row["DESCRIPTION"] + "공정추가|";


                }
            }


            foreach (DataRow row in dtProcessPathListFrom.Rows)
            {
                if (dtProcessPathListTo.Select("USERSEQUENCE = " + row["USERSEQUENCE"].ToString() + " ").Length == 0)
                {
                    DataRow rowNewRow = dtProcessPathListTo.NewRow();
                    rowNewRow["USERSEQUENCE"] = row["USERSEQUENCE"];
                    dtProcessPathListTo.Rows.Add(rowNewRow);

                }

                if (dtProcessPathListTo.Select("USERSEQUENCE = " + row["USERSEQUENCE"].ToString() + " AND PLANTID  = '" + row["PLANTID"].ToString() + "' AND PLANTID <> ''").Length == 0)
                {
                   
                    row["PROCESSCHANGETYPE"] = "Delete";
                    //row["DESCRIPTION"] = row["DESCRIPTION"] + "공정삭제|";
                }
            }

            foreach (DataRow row in dtProcessPathListTo.Rows)
            {
                if (dtProcessPathListFrom.Select("USERSEQUENCE <> " + row["USERSEQUENCE"].ToString() + " AND PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' AND PLANTID  = '" + row["PLANTID"].ToString() + "'").Length != 0)
                {
                    string sUSERSEQUENCE = dtProcessPathListFrom.Select("USERSEQUENCE <> " + row["USERSEQUENCE"].ToString() + " AND PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' ")[0]["USERSEQUENCE"].ToString();
                    row["PROCESSCHANGETYPE"] = "Move";
                    row["DESCRIPTION"] = row["DESCRIPTION"] + sUSERSEQUENCE + ">"+ row["USERSEQUENCE"].ToString();
                }
            }


            foreach (DataRow row in dtProcessPathListFrom.Rows)
            {
                if (dtProcessPathListTo.Select("USERSEQUENCE <> " + row["USERSEQUENCE"].ToString() + " AND PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' AND PLANTID  = '" + row["PLANTID"].ToString() + "'").Length != 0)
                {
                    string sUSERSEQUENCE = dtProcessPathListTo.Select("USERSEQUENCE <> " + row["USERSEQUENCE"].ToString() + " AND PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' ")[0]["USERSEQUENCE"].ToString();

                    row["PROCESSCHANGETYPE"] = "Move";
                    row["DESCRIPTION"] = row["DESCRIPTION"] + row["USERSEQUENCE"].ToString() + ">" + sUSERSEQUENCE;
                }
            }

            // 자재
            Dictionary<string, object> ParambomFrom = new Dictionary<string, object>();
            ParambomFrom.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParambomFrom.Add("PROCESSDEFID", txtPRODUCTDEFIDfr.Text);
            ParambomFrom.Add("PROCESSDEFVERSION", txtPRODUCTDEFVERSIONfr.Text);
            DataTable dtbomFrom = SqlExecuter.Query("GetBillofMaterialList", "10001", ParambomFrom);

            Dictionary<string, object> ParambomTo = new Dictionary<string, object>();
            ParambomTo.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParambomTo.Add("PROCESSDEFID", txtRCPRODUCTDEFIDto.Text);
            ParambomTo.Add("PROCESSDEFVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            DataTable dtbomTo = SqlExecuter.Query("GetBillofMaterialList", "10001", ParambomTo);

            //grdBomCompfr.DataSource = dtbomFrom;
            //grdBomCompto.DataSource = dtbomTo;

            foreach (DataRow row in dtProcessPathListFrom.Rows)
            {
                foreach (DataRow rowBomfr in dtbomFrom.Select(" PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtbomTo.Select(" PROCESSSEGMENTID = '" + rowBomfr["PROCESSSEGMENTID"].ToString() + "' AND MATERIALDEFID = '" + rowBomfr["MATERIALDEFID"].ToString() + "' AND MATERIALDEFVERSION = '" + rowBomfr["MATERIALDEFVERSION"].ToString() + "' ").Length == 0)
                    {
                        row["MATERIALCHANGETYPE"] = "Delete";
                        //row["DESCRIPTION"] = row["DESCRIPTION"] + "자재삭제|";

                    }
                }
            }

            foreach (DataRow row in dtProcessPathListFrom.Rows)
            {
                foreach (DataRow rowBomfr in dtbomFrom.Select(" PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtbomTo.Select(" PROCESSSEGMENTID = '" + rowBomfr["PROCESSSEGMENTID"].ToString() + "' AND MATERIALDEFID = '" + rowBomfr["MATERIALDEFID"].ToString() + "' AND MATERIALDEFVERSION = '" + rowBomfr["MATERIALDEFVERSION"].ToString() + "' AND QTY <> "+ rowBomfr["QTY"].ToString() + " ").Length != 0)
                    {
                        row["MATERIALCHANGETYPE"] = "Change";
                        //row["DESCRIPTION"] = row["DESCRIPTION"] + "자재변경|";
                    }
                }
            }



            foreach (DataRow row in dtProcessPathListTo.Rows)
            {
                foreach (DataRow rowBomTo in dtbomTo.Select(" PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtbomFrom.Select(" PROCESSSEGMENTID = '" + rowBomTo["PROCESSSEGMENTID"].ToString() + "' AND MATERIALDEFID = '" + rowBomTo["MATERIALDEFID"].ToString() + "' AND MATERIALDEFVERSION = '" + rowBomTo["MATERIALDEFVERSION"].ToString() + "' ").Length == 0)
                    {
                        row["MATERIALCHANGETYPE"] = "Add";
                    }
                }
            }

            foreach (DataRow row in dtProcessPathListTo.Rows)
            {
                foreach (DataRow rowBomTo in dtbomTo.Select(" PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtbomFrom.Select(" PROCESSSEGMENTID = '" + rowBomTo["PROCESSSEGMENTID"].ToString() + "' AND MATERIALDEFID = '" + rowBomTo["MATERIALDEFID"].ToString() + "' AND MATERIALDEFVERSION = '" + rowBomTo["MATERIALDEFVERSION"].ToString() + "' AND QTY <> " + rowBomTo["QTY"].ToString() + " ").Length != 0)
                    {
                        row["MATERIALCHANGETYPE"] = "Change";
                    }
                }
            }

            // 스펙
            Dictionary<string, object> ParamSpecFrom = new Dictionary<string, object>();
            ParamSpecFrom.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSpecFrom.Add("RESOURCEID", txtPRODUCTDEFIDfr.Text);
            ParamSpecFrom.Add("RESOURCEVERSION", txtPRODUCTDEFVERSIONfr.Text);
            DataTable dtSpecFrom = SqlExecuter.Query("GetSpecDetail", "10001", ParamSpecFrom);

            Dictionary<string, object> ParamSpecTo = new Dictionary<string, object>();
            ParamSpecTo.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSpecTo.Add("PROCESSDEFID", txtRCPRODUCTDEFIDto.Text);
            ParamSpecTo.Add("PROCESSDEFVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            DataTable dtSpecTo = SqlExecuter.Query("GetSpecDetail", "10001", ParamSpecTo);


            foreach (DataRow row in dtProcessPathListFrom.Rows)
            {
                foreach (DataRow rowSpecfr in dtSpecFrom.DefaultView.ToTable(true,new string[] { "PROCESSSEGID" }).Select(" PROCESSSEGID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtSpecTo.Select(" PROCESSSEGID = '" + rowSpecfr["PROCESSSEGID"].ToString() + "' ").Length == 0)
                    {
                        row["SPECCHANGETYPE"] = "Delete";
                    }
                }
            }

            foreach (DataRow row in dtProcessPathListTo.Rows)
            {
                foreach (DataRow rowSpecTo in dtSpecTo.DefaultView.ToTable(true, new string[] { "PROCESSSEGID" }).Select(" PROCESSSEGID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtSpecFrom.Select(" PROCESSSEGID = '" + rowSpecTo["PROCESSSEGID"].ToString() + "' ").Length == 0)
                    {
                        row["SPECCHANGETYPE"] = "Add";
                    }
                }
            }


            foreach (DataRow row in dtProcessPathListFrom.Rows)
            {
                foreach (DataRow rowSpecfr in dtSpecFrom.DefaultView.ToTable(true, new string[] { "PROCESSSEGID", "CONTROLTYPE", "SL", "USL", "LSL", "CL", "LCL", "UCL", "LOL", "UOL" }).Select(" PROCESSSEGID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                        foreach(DataRow rowSpecChang in dtSpecTo.Select(" PROCESSSEGID = '" + rowSpecfr["PROCESSSEGID"].ToString() + "' AND CONTROLTYPE ='" + rowSpecfr["CONTROLTYPE"].ToString() + "' "))
                        {
                                if (rowSpecfr["SL"].ToString() != rowSpecChang["SL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }
                                if (rowSpecfr["USL"].ToString() != rowSpecChang["USL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }

                                if (rowSpecfr["LSL"].ToString() != rowSpecChang["LSL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }
                                if (rowSpecfr["CL"].ToString() != rowSpecChang["CL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }
                                if (rowSpecfr["LCL"].ToString() != rowSpecChang["LCL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }

                                if (rowSpecfr["UCL"].ToString() != rowSpecChang["UCL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }

                                if (rowSpecfr["LOL"].ToString() != rowSpecChang["LOL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }

                                if (rowSpecfr["UOL"].ToString() != rowSpecChang["UOL"].ToString())
                                {
                                    row["SPECCHANGETYPE"] = "Change";
                                }

                    }
                }
            }



            foreach (DataRow row in dtProcessPathListTo.Rows)
            {
                foreach (DataRow rowSpecto in dtSpecTo.DefaultView.ToTable(true, new string[] { "PROCESSSEGID", "CONTROLTYPE", "SL", "USL", "LSL", "CL", "LCL", "UCL", "LOL", "UOL" }).Select(" PROCESSSEGID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    foreach (DataRow rowSpecChang in dtSpecTo.Select(" PROCESSSEGID = '" + rowSpecto["PROCESSSEGID"].ToString() + "' AND CONTROLTYPE ='" + rowSpecto["CONTROLTYPE"].ToString() + "' "))
                    {
                        if (rowSpecto["SL"].ToString() != rowSpecChang["SL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }
                        if (rowSpecto["USL"].ToString() != rowSpecChang["USL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }

                        if (rowSpecto["LSL"].ToString() != rowSpecChang["LSL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }
                        if (rowSpecto["CL"].ToString() != rowSpecChang["CL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }
                        if (rowSpecto["LCL"].ToString() != rowSpecChang["LCL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }

                        if (rowSpecto["UCL"].ToString() != rowSpecChang["UCL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }

                        if (rowSpecto["LOL"].ToString() != rowSpecChang["LOL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }

                        if (rowSpecto["UOL"].ToString() != rowSpecChang["UOL"].ToString())
                        {
                            row["SPECCHANGETYPE"] = "Change";
                        }

                    }
                }
            }



            //치공구
            Dictionary<string, object> paramDurablefr = new Dictionary<string, object>();
            paramDurablefr.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            paramDurablefr.Add("PRODUCTDEFID", txtPRODUCTDEFIDfr.Text);
            paramDurablefr.Add("PROCESSDEFVERSION", txtPRODUCTDEFVERSIONfr.Text);
            paramDurablefr.Add("RESOURCETYPE", "Durable");
            DataTable dtBillofResourceDurablefr = SqlExecuter.Query("GetBillofResourceChk", "10001", paramDurablefr);

            Dictionary<string, object> paramDurableto = new Dictionary<string, object>();
            paramDurableto.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            paramDurableto.Add("PRODUCTDEFID", txtRCPRODUCTDEFIDto.Text);
            paramDurableto.Add("PROCESSDEFVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            paramDurableto.Add("RESOURCETYPE", "Durable");
            DataTable dtBillofResourceDurableto = SqlExecuter.Query("GetBillofResourceChk", "10001", paramDurableto);


            foreach (DataRow row in dtProcessPathListFrom.Rows)
            {
                foreach (DataRow rowDurablefr in dtBillofResourceDurablefr.DefaultView.ToTable(true, new string[] { "PROCESSSEGMENTID", "RESOURCEID" }).Select(" PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtBillofResourceDurableto.Select(" PROCESSSEGMENTID = '" + rowDurablefr["PROCESSSEGMENTID"].ToString() + "' AND RESOURCEID = '" + rowDurablefr["RESOURCEID"].ToString() + "' ").Length == 0)
                    {
                        row["TOOLCHANGETYPE"] = "Delete";
                    }
                }
            }


            foreach (DataRow row in dtProcessPathListTo.Rows)
            {
                foreach (DataRow rowDurableTo in dtBillofResourceDurableto.DefaultView.ToTable(true, new string[] { "PROCESSSEGMENTID", "RESOURCEID" }).Select(" PROCESSSEGMENTID = '" + row["PROCESSSEGMENTID"].ToString() + "' "))
                {
                    if (dtBillofResourceDurablefr.Select(" PROCESSSEGMENTID = '" + rowDurableTo["PROCESSSEGMENTID"].ToString() + "' AND RESOURCEID = '" + rowDurableTo["RESOURCEID"].ToString() + "' ").Length == 0)
                    {
                        row["TOOLCHANGETYPE"] = "Add";
                    }
                }
            }


            DataView dvFrom = new DataView(dtProcessPathListFrom);
            dvFrom.Sort = "USERSEQUENCE";

            DataView dvTo = new DataView(dtProcessPathListTo);
            dvTo.Sort = "USERSEQUENCE";


            grdPRODUCTDEFIDfr.DataSource = dvFrom.ToTable();
            grdPRODUCTDEFIDto.DataSource = dvTo.ToTable();

        
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

            grdPRODUCTDEFIDfr.View.TopRowChanged += grdPRODUCTDEFIDfr_TopRowChanged;
            grdPRODUCTDEFIDfr.View.LeftCoordChanged += grdPRODUCTDEFIDfr_LeftCoordChanged;

            grdPRODUCTDEFIDto.View.TopRowChanged += grdPRODUCTDEFIDto_TopRowChanged;
            grdPRODUCTDEFIDto.View.LeftCoordChanged += grdPRODUCTDEFIDto_LeftCoordChanged;


            grdPRODUCTDEFIDfr.View.ColumnWidthChanged += grdPRODUCTDEFIDfr_ColumnWidthChanged; 

            btnRequestApproval.Click += BtnRequestApproval_Click;
            btnCamReq.Click += BtnCamReq_Click;

            // 파일업로드
            btnFileUpload.Click += BtnFileUpload_Click;

            btnCNCData.Click += BtnCNCData_Click;

            btnNew.Click += BtnNew_Click;

            grdPRODUCTDEFIDfr.View.FocusedRowChanged += grdPRODUCTDEFIDfr_FocusedRowChanged;
            grdPRODUCTDEFIDto.View.FocusedRowChanged += grdPRODUCTDEFIDto_FocusedRowChanged;

            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnChgHisSearch.Click += BtnChgHisSearch_Click;

        }

        private void BtnChgHisSearch_Click(object sender, EventArgs e)
        {
            fnSearch1();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
         
            DataTable dtfrom = (DataTable)grdPRODUCTDEFIDfr.DataSource;
            dtfrom.Columns.Add("_STATE_");
            foreach (DataRow rowfrom in dtfrom.Rows)
            {
                rowfrom["_STATE_"] = "deleted";
            }

            DataTable dtto = (DataTable)grdPRODUCTDEFIDto.DataSource;
            dtto.Columns.Add("_STATE_");
            foreach (DataRow rowto in dtto.Rows)
            {
                rowto["_STATE_"] = "deleted";
            }

            dtfrom.Merge(dtto);
            ExecuteRule("Routingcompare", dtfrom);
            ShowMessage("SuccedSave");
            fnSearch();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            DataTable dtfrom = (DataTable)grdPRODUCTDEFIDfr.DataSource;
            dtfrom.Columns.Add("GOVERNANCENO");
            dtfrom.Columns.Add("ISFROMTO");
            dtfrom.Columns.Add("PRODUCTDEFID");
            dtfrom.Columns.Add("PRODUCTDEFVERSION");
            dtfrom.Columns.Add("_STATE_");
            //dtfrom.Columns.Add("DESCRIPTION");

           
            foreach (DataRow rowfrom in dtfrom.Rows)
            {
                if(rowfrom["PROCESSSEGMENTID"].ToString() == "")
                {
                    rowfrom.Delete();
                  
                }
                
            }
            dtfrom.AcceptChanges();

            foreach (DataRow rowfrom in dtfrom.Rows)
            {
                rowfrom["GOVERNANCENO"] = txtGOVERNANCENO.Text;
                rowfrom["ISFROMTO"] = "FROM";
                rowfrom["PRODUCTDEFID"] = rowfrom["PROCESSDEFID"];
                rowfrom["PRODUCTDEFVERSION"] = rowfrom["PROCESSDEFVERSION"];
                rowfrom["_STATE_"] =  "added";
                
            }


            DataTable dtto = (DataTable)grdPRODUCTDEFIDto.DataSource;
            dtto.Columns.Add("GOVERNANCENO");
            dtto.Columns.Add("ISFROMTO");
            dtto.Columns.Add("PRODUCTDEFID");
            dtto.Columns.Add("PRODUCTDEFVERSION");
            dtto.Columns.Add("_STATE_");
            //dtfrom.Columns.Add("DESCRIPTION");

            foreach (DataRow rowto in dtto.Rows)
            {
                if (rowto["PROCESSSEGMENTID"].ToString() == "")
                {
                    rowto.Delete();
                   
                }

            }

            dtto.AcceptChanges();

            foreach (DataRow rowto in dtto.Rows)
            {
                rowto["GOVERNANCENO"] = txtGOVERNANCENO.Text;
                rowto["ISFROMTO"] = "TO";
                rowto["PRODUCTDEFID"] = rowto["PROCESSDEFID"];
                rowto["PRODUCTDEFVERSION"] = rowto["PROCESSDEFVERSION"];
                rowto["_STATE_"] = "added";

            }
            DataTable dtChange = new DataTable();
            dtfrom.Merge(dtto);
            ExecuteRule("Routingcompare", dtfrom);
            ShowMessage("SuccedSave");

            fnSearch();

            
            

        }

        private void grdPRODUCTDEFIDto_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow rowto = grdPRODUCTDEFIDto.View.GetFocusedDataRow();
            if (rowto == null) return;

            Dictionary<string, object> ParambomTo = new Dictionary<string, object>();
            ParambomTo.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParambomTo.Add("PROCESSDEFID", txtRCPRODUCTDEFIDto.Text);
            ParambomTo.Add("PROCESSDEFVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            ParambomTo.Add("PROCESSSEGMENTID", rowto["PROCESSSEGMENTID"].ToString() == "" ? "-1" : rowto["PROCESSSEGMENTID"].ToString());

            DataTable dtbomTo = SqlExecuter.Query("GetBillofMaterialList", "10001", ParambomTo);
            grdBomCompto.DataSource = dtbomTo;

            // 스펙 
            Dictionary<string, object> ParamSpecattributeto = new Dictionary<string, object>();
            ParamSpecattributeto.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSpecattributeto.Add("RESOURCEID", txtRCPRODUCTDEFIDto.Text);
            ParamSpecattributeto.Add("RESOURCEVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            ParamSpecattributeto.Add("PROCESSSEGMENTID", rowto["PROCESSSEGMENTID"].ToString() == "" ? "-1" : rowto["PROCESSSEGMENTID"].ToString());
            ParamSpecattributeto.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtgrdSpecattributeto = SqlExecuter.Query("GetRoutingSpecAttributelist", "10001", ParamSpecattributeto);
            grdSpecattributeto.DataSource = dtgrdSpecattributeto;

            // 치공구
            Dictionary<string, object> paramDurableto = new Dictionary<string, object>();
            paramDurableto.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            paramDurableto.Add("PRODUCTDEFID", txtRCPRODUCTDEFIDto.Text);
            paramDurableto.Add("PROCESSDEFVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            paramDurableto.Add("RESOURCETYPE", "Durable");
            paramDurableto.Add("PROCESSSEGMENTID", rowto["PROCESSSEGMENTID"].ToString() == "" ? "-1" : rowto["PROCESSSEGMENTID"].ToString());

            DataTable dtBillofResourceDurableto = SqlExecuter.Query("GetBillofResourceChk", "10001", paramDurableto);
            grdToolto.DataSource = dtBillofResourceDurableto;

            //grdPRODUCTDEFIDfr.View.GetRow(e.FocusedRowHandle);
            //DataRow rowfr = grdPRODUCTDEFIDfr.View.GetFocusedDataRow();
            //if (rowfr != null)
            //{
            //    Dictionary<string, object> ParambomFrom = new Dictionary<string, object>();
            //    ParambomFrom.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //    ParambomFrom.Add("PROCESSDEFID", txtPRODUCTDEFIDfr.Text);
            //    ParambomFrom.Add("PROCESSDEFVERSION", txtPRODUCTDEFVERSIONfr.Text);
            //    ParambomFrom.Add("PROCESSSEGMENTID", rowfr["PROCESSSEGMENTID"].ToString());
            //    DataTable dtbomFrom = SqlExecuter.Query("GetBillofMaterialList", "10001", ParambomFrom);
            //    grdBomCompfr.DataSource = dtbomFrom;
            //}


        }

        private void grdPRODUCTDEFIDfr_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow rowfr = grdPRODUCTDEFIDfr.View.GetFocusedDataRow();
            if (rowfr == null) return;

            Dictionary<string, object> ParambomFrom = new Dictionary<string, object>();
            ParambomFrom.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParambomFrom.Add("PROCESSDEFID", txtPRODUCTDEFIDfr.Text);
            ParambomFrom.Add("PROCESSDEFVERSION", txtPRODUCTDEFVERSIONfr.Text);
            ParambomFrom.Add("PROCESSSEGMENTID", rowfr["PROCESSSEGMENTID"].ToString() == "" ? "-1" : rowfr["PROCESSSEGMENTID"].ToString());

            DataTable dtbomFrom = SqlExecuter.Query("GetBillofMaterialList", "10001", ParambomFrom);
            grdBomCompfr.DataSource = dtbomFrom;




            // 스펙 
            Dictionary<string, object> ParamSpecattributefr = new Dictionary<string, object>();
            ParamSpecattributefr.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamSpecattributefr.Add("RESOURCEID", txtPRODUCTDEFIDfr.Text);
            ParamSpecattributefr.Add("RESOURCEVERSION", txtPRODUCTDEFVERSIONfr.Text);
            ParamSpecattributefr.Add("PROCESSSEGMENTID", rowfr["PROCESSSEGMENTID"].ToString() == "" ? "-1" : rowfr["PROCESSSEGMENTID"].ToString());
            ParamSpecattributefr.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            

            DataTable dtSpecattributefr = SqlExecuter.Query("GetRoutingSpecAttributelist", "10001", ParamSpecattributefr);
            grdSpecattributefr.DataSource = dtSpecattributefr;

            // 치공구
            Dictionary<string, object> paramDurablefr = new Dictionary<string, object>();
            paramDurablefr.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            paramDurablefr.Add("PRODUCTDEFID", txtPRODUCTDEFIDfr.Text);
            paramDurablefr.Add("PROCESSDEFVERSION", txtPRODUCTDEFVERSIONfr.Text);
            paramDurablefr.Add("RESOURCETYPE", "Durable");
            paramDurablefr.Add("PROCESSSEGMENTID", rowfr["PROCESSSEGMENTID"].ToString() == "" ? "-1" : rowfr["PROCESSSEGMENTID"].ToString());

            DataTable dtBillofResourceDurablefr = SqlExecuter.Query("GetBillofResourceChk", "10001", paramDurablefr);
            grdToolfr.DataSource = dtBillofResourceDurablefr;


            //grdPRODUCTDEFIDto.View.GetRow(e.FocusedRowHandle);
            //DataRow rowto = grdPRODUCTDEFIDto.View.GetFocusedDataRow();
            //if(rowto != null)
            //{
            //    Dictionary<string, object> ParambomTo = new Dictionary<string, object>();
            //    ParambomTo.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //    ParambomTo.Add("PROCESSDEFID", txtRCPRODUCTDEFIDto.Text);
            //    ParambomTo.Add("PROCESSDEFVERSION", txtRCPRODUCTDEFVERSIONto.Text);
            //    ParambomTo.Add("PROCESSSEGMENTID", rowto["PROCESSSEGMENTID"].ToString());

            //    DataTable dtbomTo = SqlExecuter.Query("GetBillofMaterialList", "10001", ParambomTo);
            //    grdBomCompto.DataSource = dtbomTo;
            //}






        }

        private void grdPRODUCTDEFIDto_LeftCoordChanged(object sender, EventArgs e)
        {
            grdPRODUCTDEFIDfr.View.LeftCoord = grdPRODUCTDEFIDto.View.LeftCoord;
        }

        private void grdPRODUCTDEFIDto_TopRowChanged(object sender, EventArgs e)
        {
            grdPRODUCTDEFIDfr.View.TopRowIndex = grdPRODUCTDEFIDto.View.TopRowIndex;
        }

        private void grdPRODUCTDEFIDfr_TopRowChanged(object sender, EventArgs e)
        {
            grdPRODUCTDEFIDto.View.TopRowIndex = grdPRODUCTDEFIDfr.View.TopRowIndex;
        }

        private void grdPRODUCTDEFIDfr_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            grdPRODUCTDEFIDto.View.Columns[e.Column.FieldName].Width = e.Column.Width;

        
        }

        private void grdPRODUCTDEFIDfr_LeftCoordChanged(object sender, EventArgs e)
        {
            grdPRODUCTDEFIDto.View.LeftCoord = grdPRODUCTDEFIDfr.View.LeftCoord;
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
                ItemMasterfilePopup pis = new ItemMasterfilePopup(row["GOVERNANCENO"].ToString(), "", "RunningChange", "RunningChangeMgnt / RunningChange");
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
            if (row != null)
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
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    if (dt.Select("1=1", "SEQUENCE DESC")[0]["SEQUENCE"].ToString() != "")
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
                dtCamrequest.Columns.Add("PREVPRODUCTDEFID");

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





                // 오늘날짜.
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

                row["PRODUCTDEFID"] = rowGovernance["RCPRODUCTDEFID"];
                row["PRODUCTDEFVERSION"] = rowGovernance["RCPRODUCTDEFVERSION"];
                row["PREVPRODUCTDEFID"] = rowGovernance["PRODUCTDEFVERSION"];


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
                    switch (col.ColumnName)
                    {
                        case "STARTDATE":
                        case "IMPLEMENTATIONDATE":
                        case "REQUESTDATE":
                        case "ERPITEMDATE":
                        case "MODELDELIVERYDATE":
                        case "PCRDATE":
                            if (rowChk[col.ColumnName].ToString() != "")
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

            DataTable change = grdGovernance.GetChangedRows();
            if (change != null)
            {
                if (change.Rows.Count != 0)
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
            switch (rowData["STATE"].ToString())
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

            // 데이터 셋 선언
            DataSet ds = new DataSet();

            //승인 트렌젝션 조회
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", rowData["ENTERPRISEID"].ToString());
            param.Add("APPROVALID", rowData["GOVERNANCENO"].ToString());
            DataTable dtApprovalChk = SqlExecuter.Query("GetGovernanceStatusTransactionList", "10001", param);

            DataRow row = dtApproval.NewRow();
            row["APPROVALTYPE"] = "RunningChange";
            row["APPROVALID"] = rowData["GOVERNANCENO"];  
            row["ENTERPRISEID"] = rowData["ENTERPRISEID"];
            row["PLANTID"] = rowData["PLANTID"];
            row["APPROVALSTATUS"] = "RequestApproval";
            
            row["REQUESTOR"] = UserInfo.Current.Id;
            row["REQUESTDATE"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            row["APPROVER"] = rowData["APPROVEPERSON"];
            row["_STATE_"] = "added";
            dtApproval.Rows.Add(row);
            //ExecuteRule("Approval", dtApproval);


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
            rowData["STATE"] = "RunningChange";

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

            // 사이트 
            DataTable dtGovernanceSite = SqlExecuter.Query("GetGovernaceChangesite", "10001", param);
            grdGovernanceSite.DataSource = dtGovernanceSite;
            // 제품 
            //DataTable dtGovernaceproduct = SqlExecuter.Query("GetGovernaceproduct", "10001", param);
            //grdGovernanceProduct.DataSource = dtGovernaceproduct;
            fgSearch = "";

            if (dtGovernanceSite.Select("STATE in ('Approved','Complete')").Length != 0)
            {
                smartSplitTableLayoutPanel4.Enabled = false;
            }

            // Lot
            //Dictionary<string, object> paramLot = new Dictionary<string, object>();
            //paramLot.Add("PRODUCTDEFID", dataRow["PRODUCTDEFID"].ToString());
            //paramLot.Add("PRODUCTDEFVERSION", dataRow["PRODUCTDEFVERSION"].ToString());
            //DataTable dtLot = SqlExecuter.Query("GetCAMRequestlOTidList", "10001", paramLot);
            //grdLot.DataSource = dtLot;

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



                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("ASSEMBLYITEMID", row["ITEMID"].ToString());
                param.Add("ASSEMBLYITEMVERSION", row["ITEMVERSION"].ToString());
                DataTable dt = SqlExecuter.Query("GetRountingTree", "10003", param);

                DataTable dtsiet = (DataTable)grdGovernanceSite.DataSource;

                foreach (DataRow rownew in dt.Rows)
                {
                    if(dtsiet != null)
                    {
                        if (dtsiet.Select("PLANTID = '" + rownew["PLANTID"].ToString() + "'").Length == 0)
                        {
                            grdGovernanceSite.View.AddNewRow();
                            DataRow rowFocused = grdGovernanceSite.View.GetFocusedDataRow();
                            rowFocused["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                            rowFocused["PLANTID"] = rownew["PLANTID"];
                            rowFocused["GOVERNANCETYPE"] = "RunningChange";
                        }
                    }
                    else
                    {
                        grdGovernanceSite.View.AddNewRow();
                        DataRow rowFocused = grdGovernanceSite.View.GetFocusedDataRow();
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
