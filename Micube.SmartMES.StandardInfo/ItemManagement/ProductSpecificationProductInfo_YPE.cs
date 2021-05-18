using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

namespace Micube.SmartMES.StandardInfo
{
    public partial class ProductSpecificationProductInfo_YPE : UserControl
    {
        public DataTable DataSource
        {
            get; private set;
        }
        public ProductSpecificationProductInfo_YPE()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                InitializeControl();
            }
        }

        #region 컨텐츠 초기화

        /// <summary>
        /// 컨텐츠 초기화
        /// </summary>
        private void InitializeControl()
        {
            // 사양담당
            ConditionItemSelectPopup specManagerSelectPopup = new ConditionItemSelectPopup();
            specManagerSelectPopup.Id = "USERID";
            specManagerSelectPopup.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID=SPECOWNER");
            specManagerSelectPopup.ValueFieldName = "USERID";
            specManagerSelectPopup.DisplayFieldName = "USERNAME";
            specManagerSelectPopup.SetPopupLayout("SELECTUSER", PopupButtonStyles.Ok_Cancel, true, true);
            specManagerSelectPopup.SetPopupResultCount(0);
            specManagerSelectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            specManagerSelectPopup.SetPopupAutoFillColumns("DEPARTMENT");

            specManagerSelectPopup.Conditions.AddTextBox("USERIDNAME");

            specManagerSelectPopup.GridColumns.AddTextBoxColumn("USERID", 150);
            specManagerSelectPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            specManagerSelectPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 60);

            // 영업담당
            ConditionItemSelectPopup salesManagerSelectPopup = new ConditionItemSelectPopup();
            salesManagerSelectPopup.Id = "USERID";
            salesManagerSelectPopup.SearchQuery = new SqlQuery("SelectUserGroupUserSearch", "10001", $"P_USERGROUPID=SALESOWNER");
            salesManagerSelectPopup.ValueFieldName = "USERID";
            salesManagerSelectPopup.DisplayFieldName = "USERNAME";
            salesManagerSelectPopup.SetPopupLayout("SELECTUSER", PopupButtonStyles.Ok_Cancel, true, true);
            salesManagerSelectPopup.SetPopupResultCount(0);
            salesManagerSelectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            salesManagerSelectPopup.SetPopupAutoFillColumns("DEPARTMENT");

            salesManagerSelectPopup.Conditions.AddTextBox("USERIDNAME");

            salesManagerSelectPopup.GridColumns.AddTextBoxColumn("USERID", 150);
            salesManagerSelectPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            salesManagerSelectPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 60);

            smartSelectPopupEditSpecMen.SelectPopupCondition = specManagerSelectPopup;
            smartSelectPopupEditSalesMen.SelectPopupCondition = salesManagerSelectPopup;

            Dictionary<string, object> paramProductionType = new Dictionary<string, object>();
            paramProductionType.Add("CODECLASSID", "ProductionType");
            paramProductionType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtProductionType = SqlExecuter.Query("GetTypeList", "10001", paramProductionType);
            SetSmartComboBox(cboProductionType);
            cboProductionType.DataSource = dtProductionType.Copy();

            Dictionary<string, object> paramItemAccount = new Dictionary<string, object>();
            paramItemAccount.Add("CODECLASSID", "ItemAccount");
            paramItemAccount.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtItemAccount = SqlExecuter.Query("GetTypeList", "10001", paramItemAccount);
            SetSmartComboBox(cbodtItemAccount);
            cbodtItemAccount.DataSource = dtItemAccount.Copy();

            Dictionary<string, object> paramItemClass = new Dictionary<string, object>();
            paramItemClass.Add("CODECLASSID", "ItemClass2");
            paramItemClass.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtItemClass = SqlExecuter.Query("GetTypeList", "10001", paramItemClass);
            SetSmartComboBox(cboItemClass);
            cboItemClass.DataSource = dtItemClass.Copy();

            Dictionary<string, object> paramNewData = new Dictionary<string, object>();
            paramNewData.Add("CODECLASSID", "YesNo");
            paramNewData.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtparamNewData = SqlExecuter.Query("GetTypeList", "10001", paramNewData);
            SetSmartComboBox(cbNewDataValid);
            cbNewDataValid.DataSource = dtparamNewData.Copy();

            Dictionary<string, object> paramLayer = new Dictionary<string, object>();
            paramLayer.Add("CODECLASSID", "Layer");
            paramLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtlayer = SqlExecuter.Query("GetTypeList", "10001", paramLayer);
            SetSmartComboBox(cbLayer);
            cbLayer.DataSource = dtlayer.Copy();

            Dictionary<string, object> paramQrProduct = new Dictionary<string, object>();
            paramQrProduct.Add("CODECLASSID", "QRProductionType");
            paramQrProduct.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtQrProduction = SqlExecuter.Query("GetTypeList", "10001", paramQrProduct);
            SetSmartComboBox(cboqrproducttype);
            cboqrproducttype.DataSource = dtQrProduction.Copy();

            Dictionary<string, object> paramQrSub = new Dictionary<string, object>();
            paramQrSub.Add("CODECLASSID", "QRBusinessSub");
            paramQrSub.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtQrSub = SqlExecuter.Query("GetTypeList", "10001", paramQrSub);
            SetSmartComboBox(cboqrbusinesssub);
            cboqrbusinesssub.DataSource = dtQrSub.Copy();

            Dictionary<string, object> paramQrInfo = new Dictionary<string, object>();
            paramQrInfo.Add("CODECLASSID", "QRBusinessInfo");
            paramQrInfo.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtQrInfo = SqlExecuter.Query("GetTypeList", "10001", paramQrInfo);
            SetSmartComboBox(cboqrbusinessinfo);
            cboqrbusinessinfo.DataSource = dtQrInfo.Copy();

            Dictionary<string, object> paramEndType = new Dictionary<string, object>();
            paramEndType.Add("CODECLASSID", "DiscontinueType");
            paramEndType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtEndType = SqlExecuter.Query("GetTypeList", "10001", paramEndType);
            SetSmartComboBox(cboendtype);
            cboendtype.DataSource = dtEndType.Copy();

            Dictionary<string, object> paramSalesType = new Dictionary<string, object>();
            paramSalesType.Add("CODECLASSID", "SalesType");
            paramSalesType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtSales = SqlExecuter.Query("GetTypeList", "10001", paramSalesType);
            SetSmartComboBox(cbosalestype);
            cbosalestype.DataSource = dtSales.Copy();

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

            popcontractor.SelectPopupCondition = cisidvendorCode;
            popbillto.SelectPopupCondition = cisidvendorCode;
            popshipto.SelectPopupCondition = cisidvendorCode;

            // 거래처팝업
            ConditionItemSelectPopup cisidvendorCode2 = new ConditionItemSelectPopup();
            cisidvendorCode2.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            cisidvendorCode2.SetPopupLayout("CUSTOMERID", PopupButtonStyles.Ok_Cancel);

            cisidvendorCode2.Id = "CUSTOMERID";
            cisidvendorCode2.LabelText = "CUSTOMERID";
            cisidvendorCode2.SearchQuery = new SqlQuery("GetCustomerList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            cisidvendorCode2.IsMultiGrid = false;
            cisidvendorCode2.DisplayFieldName = "CUSTOMER";
            cisidvendorCode2.ValueFieldName = "CUSTOMER";
            cisidvendorCode2.LanguageKey = "CUSTOMERID";

            cisidvendorCode2.Conditions.AddTextBox("TXTCUSTOMERID");
            cisidvendorCode2.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("ADDRESS", 250);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("CEONAME", 100);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("TELNO", 100);
            cisidvendorCode2.GridColumns.AddTextBoxColumn("FAXNO", 100);
            popcustomername.SelectPopupCondition = cisidvendorCode2;
        }

        /// <summary>
        /// Combo 초기화 공통
        /// </summary>
        /// <param name="comboBox"></param>
        private void SetSmartComboBox(SmartComboBox comboBox)
        {
            comboBox.DisplayMember = "CODENAME";
            comboBox.ValueMember = "CODEID";
            comboBox.ShowHeader = false;
            comboBox.UseEmptyItem = true;
            comboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
        }

        #endregion

        #region Public Function

        /// <summary>
        /// 저장 데이터 전달
        /// </summary>
        public Dictionary<string, object> Save()
        {
            //필수 입력 체크
            List<string> requiredList = new List<string>();

            CommonFunctionProductSpec.GetRequiredValidationList(tlpProductInfo, requiredList);
            CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpProductInfo, requiredList, smartGroupBox1.LanguageKey);

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            CommonFunctionProductSpec.GetSaveDataDictionary(tlpProductInfo, dataDictionary);

            return dataDictionary;
        }

        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind(DataTable dt)
        {
            if (dt.Rows.Count <= 0)
            {
                return;
            }

            CommonFunctionProductSpec.SearchDataBind(dt.Rows[0], tlpProductInfo);
        }

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData() => CommonFunctionProductSpec.ClearData(tlpProductInfo);

        public string Itemcodereturn() => tbProductCode.EditValue.ToString();

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public DataTable Productinforeturn()
        {
            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(tlpProductInfo, DataSource);

            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, tlpProductInfo);
            row["QRBUSINESSINFO"] = cboqrbusinessinfo.Text;
            row["QRBUSINESSSUB"] = cboqrbusinesssub.Text;
            row["QRPRODUCTIONTYPE"] = cboqrproducttype.Text;
            row["PRODUCTIONTYPE"] = cboProductionType.Text;
            DataSource.Rows.Add(row);

            return DataSource;
        }

        #endregion
    }
}
