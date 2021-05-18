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

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목등록(자재)
    /// 업  무  설  명  : 자재품목등록
    /// 생    성    자  :  윤성원
    /// 생    성    일  : 2019-06-28
    /// 수  정  이  력  : 
    /// </summary>
    public partial class MaterialSpecPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region Local Variables
        public DataRow CurrentDataRow { get; set; }
        string _sITEMID = "";   //품목id
        string _sITEMNAME = "";   //품목명
        string _sITEMVERSION = ""; // 품목버전
        string _sMATERIALTYPE = ""; // 자재유형
        string _sENTERPRISEID = ""; // 회사

        private string _STATE_ = "";

		private DataTable _resourceData = new DataTable(); //resource 데이터 Set 변수

		///  선택한 설비 list를 보내기 위한 Handler
		/// </summary>
		/// <param name="dt"></param>
		public delegate void ResultDataHandler(DataTable dt);
		public event ResultDataHandler ResultDataEvent;
		#endregion
        
		#region 생성자
		public MaterialSpecPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeCondition();
        }
        public MaterialSpecPopup(string sITEMID, string sITEMNAME, string sITEMVERSION, string sMATERIALTYPE, string sENTERPRISEID)
        {
            _sITEMID = sITEMID;
            _sITEMNAME = sITEMNAME;
            _sITEMVERSION = sITEMVERSION;
            _sMATERIALTYPE = sMATERIALTYPE;
            _sENTERPRISEID = sENTERPRISEID;

            InitializeComponent();
            
            InitializeEvent();
            InitializeCondition();
            InitializeGridIdDefinitionManagement();

            SearchSpecData();

            this.Text = this.Text + " [ " + _sITEMNAME + " ] ";
        }

        private void SearchSpecData()
        {
            try
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("ITEMID", _sITEMID);
                Param.Add("ITEMVERSION", _sITEMVERSION);
                Param.Add("MATERIALTYPE", _sMATERIALTYPE);
                Param.Add("ENTERPRISEID", _sENTERPRISEID);
                //Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable dtSpecData = SqlExecuter.Query("GetMaterialItemSpec", "00001", Param);

                if (dtSpecData != null && dtSpecData.Rows.Count != 0)
                {
                    //this.Text += " - " + dtSpecData.Rows[0]["ITEMNAME"].ToString();
                    //품목
                    cboProductGroup.Tag = dtSpecData.Rows[0]["ITEMID"].ToString();
                    cboProductGroup.EditValue = dtSpecData.Rows[0]["MATERIALTYPE"].ToString();
                    sseMaterialLength.EditValue = dtSpecData.Rows[0]["MATERIALLENGTH"].ToString();
                    sseMaterialWidth.EditValue = dtSpecData.Rows[0]["MATERIALWIDTH"].ToString();
                    cboOrderPolicy.EditValue = dtSpecData.Rows[0]["ORDERPOLICY"].ToString();

                    cboHalogenYN.EditValue = dtSpecData.Rows[0]["ISCONTAINHALOGEN"].ToString();
                    txtConversionFactor.EditValue = dtSpecData.Rows[0]["CONVERSIONFACTOR"].ToString();
                    sspPurchaseman.SetValue(dtSpecData.Rows[0]["PURCHASEMAN"]);
                    txtPrice.EditValue = dtSpecData.Rows[0]["TXTPRICE"].ToString();

                    cboReceiptRoute.EditValue = dtSpecData.Rows[0]["RECEIPTROUTE"].ToString();
                    cboReceiptLOC.EditValue = dtSpecData.Rows[0]["RECEIPTLOCATOR"].ToString();
                    cboMaterialOutType.EditValue = dtSpecData.Rows[0]["MATERIALOUTTYPE"].ToString();
                    cboMakeReceiptClass.EditValue = dtSpecData.Rows[0]["MAKERECEIPTTYPE"].ToString();

                    //계정
                    cboAccountGroup.EditValue = dtSpecData.Rows[0]["ACCOUNTGROUP"].ToString();
                    cboAccountCode.EditValue = dtSpecData.Rows[0]["ACCOUNTCODE"].ToString();
                    cboAccountType.EditValue = dtSpecData.Rows[0]["ACCOUNTTYPE"].ToString();
                    cboDebitCredit.EditValue = dtSpecData.Rows[0]["RECORDEDTYPE"].ToString();

                    _STATE_ = dtSpecData.Rows[0]["_STATE_"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            InitializeComboBox();
        }
        /// <summary>
        /// 콤보 박스 초기화
        /// </summary>
        private void InitializeComboBox()
        {
            // 품목구분
            //cboProductGroup.DisplayMember = "CODENAME";
            //cboProductGroup.ValueMember = "CODEID";
            //cboProductGroup.ShowHeader = false;
            //Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            //ParamRt.Add("CODECLASSID", "MaterialLargeClass");
            //ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //DataTable dtProductType = SqlExecuter.Query("GetCodeList", "00001", ParamRt);
            cboProductGroup.DisplayMember = "MASTERDATACLASSNAME";
            cboProductGroup.ValueMember = "MASTERDATACLASSID";
            cboProductGroup.ShowHeader = false;
            Dictionary<string, object> ParamRt = new Dictionary<string, object>();
            ParamRt.Add("ITEMOWNER", "Material");
            ParamRt.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamRt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtProductType = SqlExecuter.Query("GetmasterdataclassList", "10001", ParamRt);
            cboProductGroup.DataSource = dtProductType;


            //할로겐
            cboHalogenYN.DisplayMember = "CODENAME";
            cboHalogenYN.ValueMember = "CODEID";
            cboHalogenYN.ShowHeader = false;
            Dictionary<string, object> ParamHg = new Dictionary<string, object>();
            ParamHg.Add("CODECLASSID", "YesNo");
            ParamHg.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtHalogenYN = SqlExecuter.Query("GetCodeList", "00001", ParamHg);
            cboHalogenYN.DataSource = dtHalogenYN;

            //입고 경로
            cboReceiptRoute.DisplayMember = "CODENAME";
            cboReceiptRoute.ValueMember = "CODEID";
            cboReceiptRoute.ShowHeader = false;
            Dictionary<string, object> ParamRo = new Dictionary<string, object>();
            ParamRo.Add("CODECLASSID", "ReceiptRoute");
            ParamRo.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtReceiptRo = SqlExecuter.Query("GetCodeList", "00001", ParamRo);
            cboReceiptRoute.DataSource = dtReceiptRo;

            //입고 LOC
            cboReceiptLOC.DisplayMember = "CODENAME";
            cboReceiptLOC.ValueMember = "CODEID";
            cboReceiptLOC.ShowHeader = false;
            Dictionary<string, object> ParamRL = new Dictionary<string, object>();
            ParamRL.Add("CODECLASSID", "ReceiptLocator");
            ParamRL.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtReceiptLO = SqlExecuter.Query("GetCodeList", "00001", ParamRL);
            cboReceiptLOC.DataSource = dtReceiptLO;

            //출고 방식
            cboMaterialOutType.DisplayMember = "CODENAME";
            cboMaterialOutType.ValueMember = "CODEID";
            cboMaterialOutType.ShowHeader = false;
            Dictionary<string, object> ParamMt = new Dictionary<string, object>();
            ParamMt.Add("CODECLASSID", "MaterialOutType");
            ParamMt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMaterialOutType = SqlExecuter.Query("GetCodeList", "00001", ParamMt);
            cboMaterialOutType.DataSource = dtMaterialOutType;


            //제조/입고일자 구분
            cboMakeReceiptClass.DisplayMember = "CODENAME";
            cboMakeReceiptClass.ValueMember = "CODEID";
            cboMakeReceiptClass.ShowHeader = false;
            Dictionary<string, object> ParamMr = new Dictionary<string, object>();
            ParamMr.Add("CODECLASSID", "MakeReceiptType");
            ParamMr.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtMakeReceiptClass = SqlExecuter.Query("GetCodeList", "00001", ParamMr);
            cboMakeReceiptClass.DataSource = dtMakeReceiptClass;

            //계정 그룹
            cboAccountGroup.DisplayMember = "CODENAME";
            cboAccountGroup.ValueMember = "CODEID";
            cboAccountGroup.ShowHeader = false;
            Dictionary<string, object> ParamAg = new Dictionary<string, object>();
            ParamAg.Add("CODECLASSID", "AccountGroup");
            ParamAg.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtAccountGroup = SqlExecuter.Query("GetCodeList", "00001", ParamAg);
            cboAccountGroup.DataSource = dtAccountGroup;

            //계정 코드
            cboAccountCode.DisplayMember = "CODENAME";
            cboAccountCode.ValueMember = "CODEID";
            cboAccountCode.ShowHeader = false;
            Dictionary<string, object> ParamAC = new Dictionary<string, object>();
            ParamAC.Add("CODECLASSID", "AccountCode");
            ParamAC.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtAccountCode = SqlExecuter.Query("GetCodeList", "00001", ParamAC);
            cboAccountCode.DataSource = dtAccountCode;

            //계정 분류
            cboAccountType.DisplayMember = "CODENAME";
            cboAccountType.ValueMember = "CODEID";
            cboAccountType.ShowHeader = false;
            Dictionary<string, object> ParamAT = new Dictionary<string, object>();
            ParamAT.Add("CODECLASSID", "AccountClass");
            ParamAT.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtAccountType = SqlExecuter.Query("GetCodeList", "00001", ParamAT);
            cboAccountType.DataSource = dtAccountType;

            //차변/대변
            cboDebitCredit.DisplayMember = "CODENAME";
            cboDebitCredit.ValueMember = "CODEID";
            cboDebitCredit.ShowHeader = false;
            Dictionary<string, object> ParamDC = new Dictionary<string, object>();
            ParamDC.Add("CODECLASSID", "RecordedType");
            ParamDC.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtDebitCredit = SqlExecuter.Query("GetCodeList", "00001", ParamDC);
            cboDebitCredit.DataSource = dtDebitCredit;

            //기본 구매자
            ConditionItemSelectPopup cisiPurchaseman = new ConditionItemSelectPopup();
            cisiPurchaseman.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisiPurchaseman.SetPopupLayout("PURCHASEMAN", PopupButtonStyles.Ok_Cancel);
            cisiPurchaseman.Id = "PURCHASEMAN";
            cisiPurchaseman.LabelText = "PURCHASEMAN";
            cisiPurchaseman.SearchQuery = new SqlQuery("GetUserAreaPerson", "10001");
            cisiPurchaseman.IsMultiGrid = false;
            cisiPurchaseman.DisplayFieldName = "USERNAME";
            cisiPurchaseman.ValueFieldName = "USERID";
            cisiPurchaseman.LanguageKey = "PURCHASEMAN";
            cisiPurchaseman.Conditions.AddTextBox("USERNAME");
            cisiPurchaseman.GridColumns.AddTextBoxColumn("USERID", 150);
            cisiPurchaseman.GridColumns.AddTextBoxColumn("USERNAME", 200);
            sspPurchaseman.SelectPopupCondition = cisiPurchaseman;

			//발주정책
			cboOrderPolicy.DisplayMember = "CODENAME";
			cboOrderPolicy.ValueMember = "CODEID";
			cboOrderPolicy.ShowHeader = false;
			cboOrderPolicy.DataSource = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>(){ { "CODECLASSID", "OrderPolicy" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

			//ConditionItemSelectPopup cisidMaker = new ConditionItemSelectPopup();
			//cisidMaker.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
			//cisidMaker.SetPopupLayout("VENDORCODE", PopupButtonStyles.Ok_Cancel);
			//cisidMaker.Id = "VENDORCODE";
			//cisidMaker.LabelText = "VENDORCODE";
			//cisidMaker.SearchQuery = new SqlQuery("GetVendorList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
			//cisidMaker.IsMultiGrid = false;
			//cisidMaker.DisplayFieldName = "VENDORNAME";
			//cisidMaker.ValueFieldName = "VENDORID";
			//cisidMaker.LanguageKey = "VENDORCODE";
			//cisidMaker.Conditions.AddTextBox("VENDORID");
			//cisidMaker.GridColumns.AddTextBoxColumn("VENDORID", 150);
			//cisidMaker.GridColumns.AddTextBoxColumn("VENDORNAME", 200);
			//sspMaker.SelectPopupCondition = cisidMaker;

			//grdMaterialItemSpec.Hide();

		}
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            //btnCancel.Click += BtnCancel_Click;
        }

		/// <summary>
		/// 확인 클릭 - 메인 grid에 체크 데이터 전달
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
            DataTable dt = new DataTable();

            dt.TableName = "MaterialItemSpec";

            dt.Columns.Add("ENTERPRISEID");
            dt.Columns.Add("ITEMID");
            dt.Columns.Add("ITEMVERSION");
            dt.Columns.Add("MATERIALTYPE");
            //dt.Columns.Add("MATERIALCLASS");
            dt.Columns.Add("PURCHASEMAN");
            dt.Columns.Add("ORDERPOLICY");
            dt.Columns.Add("RECEIPTWAREHOUSEID");
            dt.Columns.Add("RECEIPTLOCATOR");
            dt.Columns.Add("RECEIPTROUTE");
            dt.Columns.Add("VENDORID");
            dt.Columns.Add("MAKER");
            dt.Columns.Add("MATERIALWIDTH");
            dt.Columns.Add("MATERIALLENGTH");
            dt.Columns.Add("MAKERECEIPTTYPE");
            dt.Columns.Add("ISCONTAINHALOGEN");
            dt.Columns.Add("GITYPE");
            dt.Columns.Add("ACCOUNTGROUP");
            dt.Columns.Add("ACCOUNTCODE");
            dt.Columns.Add("ACCOUNTTYPE");
            dt.Columns.Add("RECORDEDTYPE");
            dt.Columns.Add("CONVERSIONFACTOR");
            dt.Columns.Add("BOOKPRICE");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("_STATE_");

            DataRow newSpecRow = dt.NewRow();

            newSpecRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            newSpecRow["ITEMID"] = cboProductGroup.Tag;
            newSpecRow["ITEMVERSION"] = _sITEMVERSION;

            newSpecRow["MATERIALTYPE"] = cboProductGroup.GetDataValue();
            newSpecRow["MATERIALWIDTH"] = sseMaterialWidth.EditValue;
            newSpecRow["MATERIALLENGTH"] = sseMaterialLength.EditValue;
            newSpecRow["ORDERPOLICY"] = cboOrderPolicy.EditValue;

            newSpecRow["ISCONTAINHALOGEN"] = cboHalogenYN.GetDataValue();
            newSpecRow["CONVERSIONFACTOR"] = txtConversionFactor.EditValue;
            newSpecRow["PURCHASEMAN"] = sspPurchaseman.GetValue();
            newSpecRow["BOOKPRICE"] = txtPrice.EditValue;
            
            newSpecRow["RECEIPTROUTE"] = cboReceiptRoute.GetDataValue();
            newSpecRow["RECEIPTLOCATOR"] = cboReceiptLOC.GetDataValue();
            newSpecRow["MAKERECEIPTTYPE"] = cboMakeReceiptClass.GetDataValue();
            newSpecRow["GITYPE"] = cboMaterialOutType.GetDataValue();

            newSpecRow["ACCOUNTGROUP"] = cboAccountGroup.GetDataValue();
            newSpecRow["ACCOUNTCODE"] = cboAccountCode.GetDataValue();
            newSpecRow["ACCOUNTTYPE"] = cboAccountType.GetDataValue();
            newSpecRow["RECORDEDTYPE"] = cboDebitCredit.GetDataValue();
            newSpecRow["_STATE_"] = _STATE_;
            //newSpecRow["Receiptwarehouseid"] = txt
            //newSpecRow["Vendorid"] =
            //newSpecRow["Maker"] =
            //newSpecRow["Description"] =

            dt.Rows.Add(newSpecRow);

            dt.AcceptChanges();

            ExecuteRule("SaveMaterialSpec", dt);

            MSGBox.Show(MessageBoxType.Information, "SuccedSave");

            this.Close();
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridIdDefinitionManagement()
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ITEMID", _sITEMID);
            Param.Add("ITEMVERSION", _sITEMVERSION);
          
            Param.Add("ENTERPRISEID", _sENTERPRISEID);

            DataTable dt = SqlExecuter.Query("GetMaterialItemSpec", "00001", Param);
            if (dt.Rows.Count == 0)
            {
                //grdMaterialItemSpec.View.AddNewRow();

                //object objNew = this.grdMaterialItemSpec.DataSource;
                //DataTable dtNew = (DataTable)objNew;

                //dtNew.Rows[0]["ITEMID"] = _sITEMID;
                //dtNew.Rows[0]["ITEMVERSION"] = _sITEMVERSION;
                //dtNew.Rows[0]["MASTERDATACLASSID"] = _sMASTERDATACLASSID;
                //dtNew.Rows[0]["ENTERPRISEID"] = _sENTERPRISEID;
            }
            
        }
        #endregion


    }
}
