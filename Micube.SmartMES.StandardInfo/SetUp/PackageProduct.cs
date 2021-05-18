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
using Micube.SmartMES.Commons;
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
	public partial class PackageProduct : SmartConditionManualBaseForm
    {
        #region Local Variables
        DataTable dtpakage;
        #endregion

        #region 생성자
        public PackageProduct()
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
            //grdPackageProduct.Hide();
            InitializeCondition_CustomerPopup();

            InitializeCondition_Popup();

           


        }
        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControl()
        {
            // 거래처팝업
            ConditionItemSelectPopup cisidvendorCode = new ConditionItemSelectPopup();
            cisidvendorCode.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);

            cisidvendorCode.Id = "CUSTOMERID";
            cisidvendorCode.LabelText = "CUSTOMERID";
            cisidvendorCode.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode.IsMultiGrid = false;
            cisidvendorCode.DisplayFieldName = "CUSTOMERID";
            cisidvendorCode.ValueFieldName = "CUSTOMERID";
            cisidvendorCode.LanguageKey = "CUSTOMERID";

            cisidvendorCode.Conditions.AddTextBox("TXTCUSTOMERID");
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            cisidvendorCode.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            cisidvendorCode.GridColumns.AddTextBoxColumn("CEONAME", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("TELNO", 100);
            cisidvendorCode.GridColumns.AddTextBoxColumn("FAXNO", 100);
            sspCustomerId.SelectPopupCondition = cisidvendorCode;
            sspCustomerId.TextChanged += SspCustomerId_TextChanged;

            // 품목
            ConditionItemSelectPopup cisItemId = new ConditionItemSelectPopup();
            cisItemId.SetPopupLayoutForm(800, 500, FormBorderStyle.FixedToolWindow);
            cisItemId.SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false);
            cisItemId.Id = "ITEMID";
            cisItemId.LabelText = "ITEMID";
            cisItemId.SearchQuery = new SqlQuery("GetProductItemGroup", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisItemId.IsMultiGrid = false;
            cisItemId.DisplayFieldName = "ITEMID";
            cisItemId.ValueFieldName = "ITEMID";
            cisItemId.LanguageKey = "ITEMID";
            cisItemId.Conditions.AddComboBox("MASTERDATACLASSID",
                new SqlQuery("GetmasterdataclassList", "10001", $"ITEMOWNER={"Specifications"}",
                    $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "MASTERDATACLASSNAME", "MASTERDATACLASSID").SetDefault("Product");
            cisItemId.Conditions.AddTextBox("ITEMID");
            cisItemId.Conditions.AddTextBox("ITEMNAME");
            cisItemId.GridColumns.AddTextBoxColumn("ITEMID", 150);
            cisItemId.GridColumns.AddTextBoxColumn("ITEMNAME", 250);
            cisItemId.GridColumns.AddTextBoxColumn("UOMDEFID", 80);
            //cisItemId.GridColumns.AddTextBoxColumn("SPEC", 250);
            sspItemId.SelectPopupCondition = cisItemId;

            // 영풍은 포장사양을 내부 리비전 단위로 관리하지 않음 (2020-04-02, 담당TFT:우영민K)
            if ( UserInfo.Current.Enterprise.Equals("INTERFLEX"))
            {
                sspItemId.Validated += SspItemId_Validated;

                //버전
                lblRev.ForeColor = Color.Red;
                cboVer.TextChanged += CboVer_TextChanged;
            }
            else
            {
                cisItemId.SetPopupApplySelection((selectRow, gridRow) => {
                    selectRow.AsEnumerable().ForEach(r => {
                        txtPRODUCTDEFNAME.Text = r["ITEMNAME"].ToString();
                        txtUom.Text = r["UOMDEFID"].ToString();
                    });
                });

                //버전
                lblRev.Visible = false;
                cboVer.Visible = false;

                smartLabel3.Visible = false;
                txtOperationItem.Visible = false;

                smartLabel1.Visible = false;
                cboUSERSEQUENCE.Visible = false;
            }

            // 유효상태
            cboValidState.DisplayMember = "CODENAME";
            cboValidState.ValueMember = "CODEID";
            cboValidState.ShowHeader = false;
            Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            ParamRt.Add("CODECLASSID", "ValidState");
            ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtValidState = SqlExecuter.Query("GetCodeList", "00001", ParamRt);
            cboValidState.DataSource = dtValidState;

            // 확인
            cboIsConfirmation.DisplayMember = "CODENAME";
            cboIsConfirmation.ValueMember = "CODEID";
            cboIsConfirmation.ShowHeader = false;
            Dictionary<string, object> ParamConfirmation = new Dictionary<string, object>();
            ParamConfirmation.Add("CODECLASSID", "YesNo");
            ParamConfirmation.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtConfirmation = SqlExecuter.Query("GetCodeList", "00001", ParamConfirmation);
            cboIsConfirmation.DataSource = dtConfirmation;


            // TYPE
            cboPACKAGETYPE.DisplayMember = "CODENAME";
            cboPACKAGETYPE.ValueMember = "CODEID";
            cboPACKAGETYPE.ShowHeader = false;
            Dictionary<string, object> ParamPACKAGETYPE = new Dictionary<string, object>();
            ParamPACKAGETYPE.Add("CODECLASSID", "PackageType");
            ParamPACKAGETYPE.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPackageType = SqlExecuter.Query("GetCodeList", "00001", ParamPACKAGETYPE);
            cboPACKAGETYPE.DataSource = dtPackageType;


            // 포장방업
            cboPackingMethod.DisplayMember = "CODENAME";
            cboPackingMethod.ValueMember = "CODEID";
            cboPackingMethod.ShowHeader = false;
            Dictionary<string, object> ParamPackingMethod = new Dictionary<string, object>();
            ParamPackingMethod.Add("CODECLASSID", "PackingMethod");
            ParamPackingMethod.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtParamPackingMethod = SqlExecuter.Query("GetCodeList", "00001", ParamPackingMethod);
            cboPackingMethod.DataSource = dtParamPackingMethod;

            // 진공장법
            cboVacuumMethod.DisplayMember = "CODENAME";
            cboVacuumMethod.ValueMember = "CODEID";
            cboVacuumMethod.ShowHeader = false;
            Dictionary<string, object> ParamVacuumMethod = new Dictionary<string, object>();
            ParamVacuumMethod.Add("CODECLASSID", "VacuumMethod");
            ParamVacuumMethod.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtVacuumMethod = SqlExecuter.Query("GetCodeList", "00001", ParamVacuumMethod);
            cboVacuumMethod.DataSource = dtVacuumMethod;

            // 습도카드
            cboMoistureCard.DisplayMember = "CODENAME";
            cboMoistureCard.ValueMember = "CODEID";
            cboMoistureCard.ShowHeader = false;
            Dictionary<string, object> ParamMoistureCard = new Dictionary<string, object>();
            ParamMoistureCard.Add("CODECLASSID", "MoistureCard");
            ParamMoistureCard.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMoistureCard = SqlExecuter.Query("GetCodeList", "00001", ParamMoistureCard);
            cboMoistureCard.DataSource = dtMoistureCard;


            // 실리카겔
            cboSilicaGel.DisplayMember = "CODENAME";
            cboSilicaGel.ValueMember = "CODEID";
            cboSilicaGel.ShowHeader = false;
            Dictionary<string, object> ParamSilicaGel = new Dictionary<string, object>();
            ParamSilicaGel.Add("CODECLASSID", "SilicaGel");
            ParamSilicaGel.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtSilicaGel = SqlExecuter.Query("GetCodeList", "00001", ParamSilicaGel);
            cboSilicaGel.DataSource = dtSilicaGel;

            // 포장지
            cboPackagePaper.DisplayMember = "CODENAME";
            cboPackagePaper.ValueMember = "CODEID";
            cboPackagePaper.ShowHeader = false;
            Dictionary<string, object> ParamPackagePaper = new Dictionary<string, object>();
            ParamPackagePaper.Add("CODECLASSID", "PackagePaper");
            ParamPackagePaper.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPackagePaper = SqlExecuter.Query("GetCodeList", "00001", ParamPackagePaper);
            cboPackagePaper.DataSource = dtPackagePaper;

            // 포장코드
            cboPackingcode.DisplayMember = "CODENAME";
            cboPackingcode.ValueMember = "CODEID";
            cboPackingcode.ShowHeader = false;
            Dictionary<string, object> ParamPackingcode = new Dictionary<string, object>();
            ParamPackingcode.Add("CODECLASSID", "Packingcode");
            ParamPackingcode.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPackingcode = SqlExecuter.Query("GetCodeList", "00001", ParamPackingcode);
            cboPackingcode.DataSource = dtPackingcode;

            // 포장재명
            cboPackingName.DisplayMember = "CODENAME";
            cboPackingName.ValueMember = "CODEID";
            cboPackingName.ShowHeader = false;
            Dictionary<string, object> ParamPackingName = new Dictionary<string, object>();
            ParamPackingName.Add("CODECLASSID", "Packingcode");
            ParamPackingName.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtPackingName = SqlExecuter.Query("GetCodeList", "00001", ParamPackingName);
            cboPackingName.DataSource = dtPackingName;

            // RoHS
            cboISROSH.DisplayMember = "CODENAME";
            cboISROSH.ValueMember = "CODEID";
            cboISROSH.ShowHeader = false;
            Dictionary<string, object> ParamISROSH = new Dictionary<string, object>();
            ParamISROSH.Add("CODECLASSID", "YesNo");
            ParamISROSH.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtISROSH = SqlExecuter.Query("GetCodeList", "00001", ParamISROSH);
            cboISROSH.DataSource = dtISROSH;

            // H/F
            cboISHF.DisplayMember = "CODENAME";
            cboISHF.ValueMember = "CODEID";
            cboISHF.ShowHeader = false;
            Dictionary<string, object> ParamISHF = new Dictionary<string, object>();
            ParamISHF.Add("CODECLASSID", "YesNo");
            ParamISHF.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtISHF = SqlExecuter.Query("GetCodeList", "00001", ParamISHF);
            cboISHF.DataSource = dtISHF;

            //포장분류
            //lblPACKAGECLASS.ForeColor = Color.Red;

            //1CARD/1TARY
            lbl1CARD1TARY.ForeColor = Color.Red;

            //Case
            lblCase.ForeColor = Color.Red;

            //거래처
            lblCustomerID.ForeColor = Color.Red;

            //품목
            lblProductDefID.ForeColor = Color.Red;


            //보증기간
            cboWarranty.DisplayMember = "CODENAME";
            cboWarranty.ValueMember = "CODEID";
            cboWarranty.ShowHeader = false;
            Dictionary<string, object> ParamWarranty = new Dictionary<string, object>();
            ParamWarranty.Add("CODECLASSID", "Warranty");
            ParamWarranty.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtWarranty = SqlExecuter.Query("GetCodeList", "00001", ParamWarranty);
            cboWarranty.DataSource = dtWarranty;
        }
        private void SspCustomerId_TextChanged(object sender, EventArgs e)
        {

            if(sspCustomerId.EditValue.ToString() != "")
            {
                Dictionary<string, object> ParamOLBCircuit = new Dictionary<string, object>();
                ParamOLBCircuit.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                ParamOLBCircuit.Add("PLANTID", UserInfo.Current.Plant);
                ParamOLBCircuit.Add("CUSTOMERID", sspCustomerId.EditValue.ToString());

                DataTable dtCustomer = SqlExecuter.Query("GetCustomerList", "10002", ParamOLBCircuit);

                if ( dtCustomer != null && dtCustomer.Rows.Count > 0 )
                    txtCustomerName.Text = dtCustomer.Rows[0]["CUSTOMERNAME"].ToString();
            }
            
        }
        private void CboVer_TextChanged(object sender, EventArgs e)
        {

            Dictionary<string, object> Paramitem = new Dictionary<string, object>();
            Paramitem.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Paramitem.Add("ITEMID", sspItemId.GetValue());
            Paramitem.Add("ITEMVERSION", cboVer.EditValue);

            DataTable dtitem = SqlExecuter.Query("GetProductItemSearch", "10001", Paramitem);

            if (dtitem != null)
            {
                if (dtitem.Rows.Count != 0)
                {
                    //txtProductDefID.Text = dtitem.Rows[0]["ITEMID"].ToString();
                    //txtVer.Text = dtitem.Rows[0]["ITEMVERSION"].ToString();
                    txtPRODUCTDEFNAME.Text = dtitem.Rows[0]["ITEMNAME"].ToString();
                    txtUom.Text = dtitem.Rows[0]["UOMDEFID"].ToString();
                    //txtPRODUCTDEFNAME.Text = dtitem.Rows[0]["CUSTOMERITEMNAME"].ToString();
                }
            }

            //공장반제품
            Dictionary<string, object> ParamItemcode = new Dictionary<string, object>();
            ParamItemcode.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamItemcode.Add("P_PLANTID", UserInfo.Current.Plant);
            ParamItemcode.Add("ITEMID", sspItemId.GetValue());
            ParamItemcode.Add("ITEMVERSION", cboVer.EditValue);
            DataTable dtOperationItemCode = SqlExecuter.Query("GetOperationItemCodeCombo", "10001", ParamItemcode);

            if (dtOperationItemCode != null)
            {
                if (dtOperationItemCode.Rows.Count != 0)
                {
                    txtOperationItem.Text = dtOperationItemCode.Rows[0]["ASSEMBLYITEMID"].ToString();
                }
            }

            cboUSERSEQUENCE.DisplayMember = "USERSEQUENCE";
            cboUSERSEQUENCE.ValueMember = "USERSEQUENCE";
            cboUSERSEQUENCE.ShowHeader = false;
            cboUSERSEQUENCE.EmptyItemValue = 0;
            cboUSERSEQUENCE.EmptyItemCaption = "전체";
            cboUSERSEQUENCE.UseEmptyItem = true;
            Dictionary<string, object> ParamUserSeq = new Dictionary<string, object>();
            ParamUserSeq.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamUserSeq.Add("P_PLANTID", UserInfo.Current.Plant);

            ParamUserSeq.Add("ITEMID", sspItemId.GetValue());
            ParamUserSeq.Add("ITEMVERSION", cboVer.EditValue);
            ParamUserSeq.Add("OPERATIONITEM", txtOperationItem.Text);
            DataTable dtUSERSEQUENCE = SqlExecuter.Query("GetUserSequenceCombo", "10001", ParamUserSeq);

            if (dtUSERSEQUENCE != null)
            {
                if (dtUSERSEQUENCE.Rows.Count != 0)
                {
                    cboUSERSEQUENCE.DataSource = dtUSERSEQUENCE;
                }
            }


        }

        private void SspItemId_Validated(object sender, EventArgs e)
        {
            SmartSelectPopupEdit Popupedit = (SmartSelectPopupEdit)sender;

            string sItemid = "";


            if (Popupedit.SelectedData.Count<DataRow>() == 0)
            {
                sItemid = "-1";
            }

            foreach (DataRow row in Popupedit.SelectedData)
            {
                sItemid = row["ITEMID"].ToString();

            }


            cboVer.DisplayMember = "ITEMVERSIONNAME";
            cboVer.ValueMember = "ITEMVERSIONCODE";
            // combobox.ShowHeader = false;
            Dictionary<string, object> ParamIv = new Dictionary<string, object>();
            ParamIv.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamIv.Add("PLANTID", UserInfo.Current.Plant);
            ParamIv.Add("ITEMID", sItemid);
            DataTable dtIv = SqlExecuter.Query("GetItemVersion", "10001", ParamIv);

            cboVer.DataSource = dtIv;

            if (dtIv.Rows.Count != 0)
            {
                cboVer.EditValue = dtIv.Rows[0]["ITEMVERSIONCODE"];
            }

        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            grdPackageProduct.GridButtonItem = GridButtonItem.All;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdPackageProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            
            // 필수 입력 컬럼 지정
            grdPackageProduct.View.AddTextBoxColumn("PACKAGECLASS").SetIsHidden();
            // 필수 입력 컬럼 지정
            grdPackageProduct.View.AddTextBoxColumn("ENTERPRISEID")
                .SetValidationIsRequired()
                .SetIsReadOnly()
                .SetIsHidden();

            // 필수 입력 컬럼 지정
            grdPackageProduct.View.AddTextBoxColumn("PLANTID", 80)
                .SetValidationIsRequired().SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            // 필수 입력 컬럼 지정
            grdPackageProduct.View.AddTextBoxColumn("CUSTOMERID", 80).SetValidationIsRequired().SetIsReadOnly(); //고객
            grdPackageProduct.View.AddTextBoxColumn("CUSTOMERNAME", 150).SetIsReadOnly();

            // 필수 입력 컬럼 지정
            grdPackageProduct.View.AddTextBoxColumn("ITEMID", 100).SetValidationIsRequired().SetIsReadOnly();
            // 영풍의 경우 품목별 포장사양으로 관리하고 내부리비전은 관리하지 않음
            if (UserInfo.Current.Enterprise.Equals("INTERFLEX"))
            {
                grdPackageProduct.View.AddTextBoxColumn("ITEMVERSION", 60).SetValidationIsRequired().SetIsReadOnly();
            }
            else
            {
                grdPackageProduct.View.AddTextBoxColumn("ITEMVERSION", 60).SetIsHidden();
            }
            grdPackageProduct.View.AddTextBoxColumn("ITEMNAME", 200).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("OPERITEMID", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("USERSEQUENCE", 60)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);

            grdPackageProduct.View.AddTextBoxColumn("PARTNO1", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTNO2", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTNO3", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTNO4", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTNAME1", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTNAME2", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTNAME3", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTNAME4", 100).SetIsReadOnly();

            //포장방법
            //grdPackageProduct.View.AddTextBoxColumn("PACKINGMETHOD", 100).SetIsReadOnly();
            grdPackageProduct.View.AddComboBoxColumn("PACKINGMETHOD", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PackingMethod", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            //진공방법
            //grdPackageProduct.View.AddTextBoxColumn("VACUUMMETHOD", 100).SetIsReadOnly();
            grdPackageProduct.View.AddComboBoxColumn("VACUUMMETHOD", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=VacuumMethod", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();

            grdPackageProduct.View.AddSpinEditColumn("TRAYQTY", 100).SetIsReadOnly();
            grdPackageProduct.View.AddSpinEditColumn("CASEPCSQTY", 100).SetIsReadOnly();
            grdPackageProduct.View.AddSpinEditColumn("PACKQTY", 100).SetIsReadOnly().SetIsHidden();
            grdPackageProduct.View.AddSpinEditColumn("BOXQTY", 100).SetIsReadOnly().SetIsHidden();
            grdPackageProduct.View.AddSpinEditColumn("PACKAGEQTY", 100).SetIsReadOnly().SetIsHidden();

            grdPackageProduct.View.AddTextBoxColumn("BOXTOTALPCS", 300).SetIsReadOnly();

            // 묶음라벨 수
            grdPackageProduct.View.AddTextBoxColumn("BUNDLEQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetIsReadOnly();

            // 습도카드
            grdPackageProduct.View.AddComboBoxColumn("MOISTURECARD", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MoistureCard", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            //grdPackageProduct.View.AddTextBoxColumn("MOISTURECARD", 100).SetIsReadOnly();

            //실리카겔
            grdPackageProduct.View.AddComboBoxColumn("SILICAGEL", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=SilicaGel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            //grdPackageProduct.View.AddTextBoxColumn("SILICAGEL", 100).SetIsReadOnly();

            //포장지
            grdPackageProduct.View.AddComboBoxColumn("PACKAGEPAPER", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PackagePaper", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            //grdPackageProduct.View.AddTextBoxColumn("PACKAGEPAPER", 80).SetIsReadOnly();


            grdPackageProduct.View.AddTextBoxColumn("DESCRIPTION", 200).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PACKINGCODE", 80).SetIsReadOnly();

            // 포장재명
            grdPackageProduct.View.AddComboBoxColumn("PACKINGNAME", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Packingcode", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            //grdPackageProduct.View.AddTextBoxColumn("PACKINGNAME", 100).SetIsReadOnly();

            grdPackageProduct.View.AddSpinEditColumn("SHIPTO", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("UOMDEFID", 80).SetIsReadOnly();
            //grdPackageProduct.View.AddTextBoxColumn("WARRANTY", 100).SetIsReadOnly();
            grdPackageProduct.View.AddComboBoxColumn("WARRANTY", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Warranty", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();


            grdPackageProduct.View.AddTextBoxColumn("SUPPLIERNAME", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PARTDESCRIPTION", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("SUPPLIERCODE", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("ETC", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("PCSWEIGHT", 100).SetIsReadOnly();
            grdPackageProduct.View.AddTextBoxColumn("BOXWEIGHT", 100).SetIsReadOnly();

            grdPackageProduct.View.AddComboBoxColumn("ISROSH", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdPackageProduct.View.AddComboBoxColumn("ISHF", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdPackageProduct.View.AddTextBoxColumn("PACKAGETYPE", 100).SetIsHidden();
            grdPackageProduct.View.AddTextBoxColumn("CASEQTY", 100).SetIsHidden();

            grdPackageProduct.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            //확정여부
            grdPackageProduct.View.AddTextBoxColumn("ISCONFIRMATION", 60).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);

            grdPackageProduct.View.AddTextBoxColumn("CREATOR", 80)
                      .SetIsReadOnly()
                      .SetTextAlignment(TextAlignment.Center);
            grdPackageProduct.View.AddTextBoxColumn("CREATEDTIME", 130)
                                // Display Format 지정
                                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);
            grdPackageProduct.View.AddTextBoxColumn("MODIFIER", 80)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);
            grdPackageProduct.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss", MaskTypes.DateTime)
                                .SetIsReadOnly()
                                .SetTextAlignment(TextAlignment.Center);

            // grdPackageProduct.View.AddSpinEditColumn("CUSTOMERITEMNAME", 100).SetIsReadOnly();
            grdPackageProduct.View.PopulateColumns();


        }

        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeControl();
            InitializeGridIdDefinitionManagement();
        }

        #endregion


        #region 툴바
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            switch (btn.Name.ToString())
            {
                case "New":

                    CommonFunctionProductSpec.ClearData(smartGroupBox1);

                    dtpakage = new DataTable();

                    dtpakage = (grdPackageProduct.DataSource as DataTable).Clone();
                    dtpakage.Columns.Add("_STATE_");

                    DataRow dr = dtpakage.NewRow();


                    var values = this.Conditions.GetValues();
                    dr["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    dr["PLANTID"] = values["P_PLANTID"].ToString();
                    dr["PACKAGECLASS"] = "*";

                    dr["VALIDSTATE"] = "Valid";
                    dr["ISCONFIRMATION"] = "N";
                    dr["_STATE_"] = "added";
                    cboValidState.EditValue = "Valid";
                    cboIsConfirmation.EditValue = "N";
                    dtpakage.Rows.Add(dr);

                    break;
            }

        }

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if (dtpakage != null)
            {
                foreach (DataRow drpakage in dtpakage.Rows)
                {
                    if (Format.GetString(drpakage["_STATE_"]).Equals("added"))
                    {
                        dtpakage.Columns.Add("ITEMCODE");

                        foreach (DataRow row in dtpakage.Rows)
                        {
                            row["ITEMCODE"] = row["OPERITEMID"];
                            row["PLANTID"] = UserInfo.Current.Plant;
                            row["CUSTOMERID"] = sspCustomerId.EditValue;
                            row["CUSTOMERNAME"] = txtCustomerName.EditValue;
                            row["ITEMID"] = sspItemId.EditValue;
                            row["ITEMVERSION"] = cboVer.EditValue;
                            row["ITEMNAME"] = txtPRODUCTDEFNAME.EditValue;
                            row["USERSEQUENCE"] = cboUSERSEQUENCE.EditValue;
                            row["PARTNO1"] = txtPartNo1.EditValue;
                            row["PARTNO2"] = txtPartNo2.EditValue;
                            row["PARTNO3"] = txtPartNo3.EditValue;
                            row["PARTNO4"] = txtPartNo4.EditValue;
                            row["PARTNAME1"] = txtPartName1.EditValue;
                            row["PARTNAME2"] = txtPartName2.EditValue;
                            row["PARTNAME3"] = txtPartName3.EditValue;
                            row["PARTNAME4"] = txtPartName4.EditValue;
                            row["PACKINGMETHOD"] = cboPackingMethod.EditValue;
                            row["VACUUMMETHOD"] = cboVacuumMethod.EditValue;
                            row["TRAYQTY"] = txtCardTary.EditValue;
                            row["CASEPCSQTY"] = txtCase.EditValue;
                            row["PACKQTY"] = txtPackQty.EditValue;
                            row["BOXQTY"] = txtBoxQty.EditValue;
                            row["PACKAGEQTY"] = txtBoxTot.EditValue;
                            row["BUNDLEQTY"] = spBundleQty.EditValue;
                            row["MOISTURECARD"] = cboMoistureCard.EditValue;
                            row["SILICAGEL"] = cboSilicaGel.EditValue;
                            row["PACKAGEPAPER"] = cboPackagePaper.EditValue;
                            row["DESCRIPTION"] = txtDESCRIPTION.EditValue;
                            row["PACKINGCODE"] = cboPackingcode.EditValue;
                            row["PACKINGNAME"] = cboPackingName.EditValue;
                            row["SHIPTO"] = txtShipto.EditValue;
                            row["UOMDEFID"] = txtUom.EditValue;
                            row["WARRANTY"] = cboWarranty.EditValue;
                            row["SUPPLIERNAME"] = smartTextBox17.EditValue;
                            row["PARTDESCRIPTION"] = txtPartDescription.EditValue;
                            row["SUPPLIERCODE"] = txtSupplierCode.EditValue;
                            row["ETC"] = txtETC.EditValue;
                            row["PCSWEIGHT"] = txtPCSWeight.EditValue;
                            row["BOXWEIGHT"] = txtBoxWeight.EditValue;
                            row["ISROSH"] = cboISROSH.EditValue;
                            row["ISHF"] = cboISHF.EditValue;
                            row["PACKAGETYPE"] = cboPACKAGETYPE.EditValue;
                            row["CASEQTY"] = txtCase.EditValue;
                            row["VALIDSTATE"] = cboValidState.EditValue;
                            row["ISCONFIRMATION"] = cboIsConfirmation.EditValue;

                        }
                        ExecuteRule("PackageProduct", dtpakage);
                    }

                }
            }

            DataTable changed = new DataTable();
            changed = grdPackageProduct.GetChangedRows();

            changed.Columns.Add("ITEMCODE");

            foreach (DataRow row in changed.Rows)
            {
                row["ITEMCODE"] = row["OPERITEMID"];
            }

            ExecuteRule("PackageProduct", changed);

        }
        #endregion

        #region 검색



   

        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_Popup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("ITEMID", new SqlQuery("GetProductItemGroup", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "ITEMID", "ITEMID")
               .SetPopupLayout("ITEMID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1)  //팝업창 선택가능한 개수
               .SetPosition(2)
               .SetPopupApplySelection((selectRow, gridRow) => {

                   List<string> productRevisionList = new List<string>();
                   List<string> productNameList = new List<string>();

                   selectRow.AsEnumerable().ForEach(r => {
                        productRevisionList.Add(Format.GetString(r["ITEMVERSION"]));
                        productNameList.Add(Format.GetString(r["ITEMNAME"]));
                   });

                    Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Join(",", productRevisionList);
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Join(",", productNameList);
               });

            parentPopupColumn.Conditions.AddTextBox("ITEMID");
            parentPopupColumn.Conditions.AddTextBox("ITEMNAME");

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMID", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMNAME", 200);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ITEMVERSION", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("SPEC", 250);
        }


        /// <summary>
        /// 검색조건 팝업 예제
        /// </summary>
        private void InitializeCondition_CustomerPopup()
        {
            //팝업 컬럼 설정
            var parentPopupColumn = Conditions.AddSelectPopup("CUSTOMERID", new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
               .SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)  //팝업창 UI 설정.
               .SetPopupResultCount(1);  //팝업창 선택가능한 개수



            parentPopupColumn.Conditions.AddTextBox("TXTCUSTOMERID");
        

            //팝업 그리드
            parentPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            parentPopupColumn.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            parentPopupColumn.GridColumns.AddTextBoxColumn("CEONAME", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("TELNO", 100);
            parentPopupColumn.GridColumns.AddTextBoxColumn("FAXNO", 100);
            

        }


        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            
            Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").ReadOnly = true;
            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").ReadOnly = true;
            Conditions.GetControl<SmartSelectPopupEdit>("ITEMID").EditValueChanged += ProductDefIDChanged;


            //SmartSelectPopupEdit CustmerPopupedit = Conditions.GetControl<SmartSelectPopupEdit>("CUSTOMERID");
            //CustmerPopupedit.Validated += CustmerPopupedit_Validated;

            SmartComboBox combo = Conditions.GetControl<SmartComboBox>("P_PLANTID");
            combo.EditValue = UserInfo.Current.Plant;
            combo.ReadOnly = true;

        }

        private void ProductDefIDChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit PopProdutid = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(PopProdutid.EditValue).Equals(string.Empty))
            {
                Conditions.GetControl<SmartTextBox>("P_ITEMVERSION").EditValue = string.Empty;
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = string.Empty;
            }
        }

        

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
           
            // 그리드 초기화
            grdPackageProduct.View.ClearDatas();
           // 조회조건에 입력된 값들을 Dictionary<string, object> Type으로 가져옴
            var values = this.Conditions.GetValues();
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            //TODO : Id를 수정하세요            
                    
            DataTable dtPackageProductList = await SqlExecuter.QueryAsync("GetPackageProductList", "10001", values);
            grdPackageProduct.DataSource = dtPackageProductList;
            if (dtPackageProductList.Rows.Count < 1) // 
            {
                //grdPackageProduct.View.AddNewRow();
                       
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

        }
        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            DataTable changed = new DataTable();

            if (dtpakage==null)
            {
                GetControlsFrom gcf = new GetControlsFrom();
                gcf.GetControlsFromPanelControlDelGrid(smartSplitTableLayoutPanel4, grdPackageProduct);


                //grdPackageProduct.View.CheckValidation();

                changed = grdPackageProduct.GetChangedRows();
            }
            // 1CARD/1TARY
            if (decimal.Parse(txtCardTary.Text) == 0 && decimal.Parse(txtCase.Text) == 0)
            {
                throw MessageException.Create("CardTaryCase");
            }

            // 포장분류
            //if (txtPackageclass.Text == "")
            //{
            //    throw MessageException.Create("Packageclass");
            //}



            if (dtpakage == null && changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }
        #endregion

        #region 이벤트
        private void InitializeEvent()
        {

            grdPackageProduct.View.AddingNewRow += grdPackageProduct_AddingNewRow;
            grdPackageProduct.View.FocusedRowChanged += grdPackageProduct_FocusedRowChanged;
            grdPackageProduct.ToolbarCopyRow += GrdPackageProduct_ToolbarCopyRow;
            txtCardTary.TextChanged += TxtCalculus_TextChanged;
            txtCase.TextChanged += TxtCalculus_TextChanged;
            txtBoxQty.TextChanged += TxtCalculus_TextChanged;
            
            cboPackingMethod.TextChanged += CboPackingMethod_TextChanged;

        }

        private void GrdPackageProduct_ToolbarCopyRow(object sender, EventArgs e)
        {
            displayRow();
        }

        private void CboPackingMethod_TextChanged(object sender, EventArgs e)
        {
            if(cboPackingMethod.GetDataValue() != null)
            {
                if (cboPackingMethod.GetDataValue().ToString() == "Tray")
                {
                    lbl1CARD1TARY.Text = "1Cell";
                    lblCase.Text = "1Tray Cell";
                    lbltot.Text = "Tray";
                    lblUnitBox.Text = "Tray";
                }

                if (cboPackingMethod.GetDataValue().ToString() == "Case")
                {
                    lbl1CARD1TARY.Text = "1Pack";
                    lblCase.Text = "1Case Pack";
                    lbltot.Text = "Case";
                    lblUnitBox.Text = "Case";
                }

                if (cboPackingMethod.GetDataValue().ToString() == "Block")
                {
                    lbl1CARD1TARY.Text = "1Block";
                    lblCase.Text = "1Case Block";
                    lbltot.Text = "Case";
                    lblUnitBox.Text = "Case";
                }
            }

        }


        private void grdPackageProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            displayRow();            
        }

     

        private void TxtCalculus_TextChanged(object sender, EventArgs e)
        {
            Calculus();
        }

        void Calculus()
        {
            txtPackQty.Text = (decimal.Parse(txtCardTary.Text) * decimal.Parse(txtCase.Text)).ToString();
            txtBoxTot.Text = (decimal.Parse(txtBoxQty.Text) * decimal.Parse(txtPackQty.Text)).ToString();
        }
  



        #region 그리드이벤트

        // 품목유형별 정의 + 툴버튼  그리드 추가 이벤트
        private void grdPackageProduct_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            var values = this.Conditions.GetValues();
            args.NewRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            args.NewRow["PLANTID"] = values["P_PLANTID"].ToString();
            args.NewRow["PACKAGECLASS"] = "*";
            
            args.NewRow["VALIDSTATE"] = "Valid";
            args.NewRow["ISCONFIRMATION"] = "N";

            cboValidState.EditValue = "Valid";
            cboIsConfirmation.EditValue = "N";

        }






        #endregion

        #region 기타이벤트



        #endregion

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        private void displayRow()
        {
            CommonFunction.SuspendDrawing(this);    // 속도향상을 위해 화면 드로잉 정지
            try
            {
                SetControlsFrom scf = new SetControlsFrom();
                DataRow row = grdPackageProduct.View.GetFocusedDataRow();

                if (row == null)
                {
                    return;
                }

                // Version 콤보박스 설정
                //cboVer.DisplayMember = "ITEMVERSIONNAME";
                //cboVer.ValueMember = "ITEMVERSIONCODE";

                //Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //param.Add("PLANTID", UserInfo.Current.Plant);
                //param.Add("ITEMID", row["ITEMID"].ToString());

                //DataTable dtVersion = SqlExecuter.Query("GetItemVersion", "10001", param);

                //// combobox.ShowHeader = false;
                //cboVer.DataSource = dtVersion;
                //if (dtVersion != null && !dtVersion.Rows.Count.Equals(0))
                //{
                //    cboVer.EditValue= row["ITEMID"].ToString();
                //}

                // 콘트롤 설정
                scf.SetControlsFromPanelControlDelRow(smartSplitTableLayoutPanel4, row);

                // Operation Item 설정
                //param = new Dictionary<string, object>();
                //param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //param.Add("P_PLANTID", UserInfo.Current.Plant);
                //param.Add("ITEMID", row["ITEMID"].ToString());
                //param.Add("ITEMVERSION", row["ITEMVERSION"].ToString());

                //DataTable dtOperationItem = SqlExecuter.Query("GetOperationItemCodeCombo", "10001", param);

                //txtOperationItem.EditValue = null;

                //if (dtOperationItem != null && !dtOperationItem.Rows.Count.Equals(0))
                //{
                //    txtOperationItem.EditValue = dtOperationItem.Rows[0]["ASSEMBLYITEMID"].ToString();
                //}

                //// UserSequence 설정
                //cboUSERSEQUENCE.DisplayMember = "USERSEQUENCE";
                //cboUSERSEQUENCE.ValueMember = "USERSEQUENCE";
                //cboUSERSEQUENCE.ShowHeader = false;
                //cboUSERSEQUENCE.EmptyItemValue = 0;
                //cboUSERSEQUENCE.EmptyItemCaption = "전체";
                //cboUSERSEQUENCE.UseEmptyItem = true;

                //param = new Dictionary<string, object>();
                //param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                //param.Add("P_PLANTID", UserInfo.Current.Plant);
                //param.Add("ITEMID", row["ITEMID"].ToString());
                //param.Add("ITEMVERSION", row["ITEMVERSION"].ToString());
                //param.Add("OPERATIONITEM", txtOperationItem.EditValue);

                //DataTable dtSequence = SqlExecuter.Query("GetUserSequenceCombo", "10001", param);

                //cboUSERSEQUENCE.DataSource = dtSequence;
                //if (dtSequence != null && !dtSequence.Rows.Count.Equals(0))
                //{
                //    cboUSERSEQUENCE.EditValue = row["USERSEQUENCE"].ToString();
                //}

            }
            finally
            {
                CommonFunction.ResumeDrawing(this);
            }
        }
        #endregion

        private void txtDESCRIPTION_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
