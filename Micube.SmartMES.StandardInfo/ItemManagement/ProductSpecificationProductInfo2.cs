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
    public partial class ProductSpecificationProductInfo2 : UserControl
    {

        DataTable DataSource = new DataTable();

        public ProductSpecificationProductInfo2()
        {
            InitializeComponent();

            if (!this.IsDesignMode())
            {
                IntializeEvent();
                InitializeControl();
            }
        }

        #region event

        /// <summary>
        /// 
        /// </summary>
        private void IntializeEvent()
        {

        }

        #endregion

        #region 컨텐츠 초기화

        /// <summary>
        /// 컨텐츠 초기화
        /// </summary>
        private void InitializeControl()
        {
            ConditionItemSelectPopup managerSelectPopup = new ConditionItemSelectPopup();
            managerSelectPopup.Id = "USERID";
            managerSelectPopup.SearchQuery = new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            managerSelectPopup.ValueFieldName = "USERID";
            managerSelectPopup.DisplayFieldName = "USERNAME";
            managerSelectPopup.SetPopupLayout("SELECTUSER", PopupButtonStyles.Ok_Cancel, true, true);
            managerSelectPopup.SetPopupResultCount(1);
            managerSelectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            managerSelectPopup.SetPopupAutoFillColumns("DEPARTMENT");

            managerSelectPopup.Conditions.AddTextBox("USERIDNAME");

            managerSelectPopup.GridColumns.AddTextBoxColumn("USERID", 150);
            managerSelectPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            managerSelectPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 60);

            smartSelectPopupEditSpecMen.SelectPopupCondition = managerSelectPopup;
            smartSelectPopupEditCamMen.SelectPopupCondition = managerSelectPopup;
            smartSelectPopupEditSalesMen.SelectPopupCondition = managerSelectPopup;


            Dictionary<string, object> ParamEndUser = new Dictionary<string, object>();
            ParamEndUser.Add("CODECLASSID", "EndUser");
            ParamEndUser.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtEndUser = SqlExecuter.Query("GetTypeList", "10001", ParamEndUser);
            SetSmartComboBox(cbEndUser);
            cbEndUser.DataSource = dtEndUser.Copy();

            Dictionary<string, object> ParamOutbound = new Dictionary<string, object>();
            ParamOutbound.Add("CODECLASSID", "OutboundType");
            ParamOutbound.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtOutbound = SqlExecuter.Query("GetTypeList", "10001", ParamOutbound);
            SetSmartComboBox(cbOutboundType);
            cbOutboundType.DataSource = dtOutbound.Copy();

            Dictionary<string, object> paramJobType = new Dictionary<string, object>();
            paramJobType.Add("CODECLASSID", "JobType");
            paramJobType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtJobType = SqlExecuter.Query("GetTypeList", "10001", paramJobType);
            SetSmartComboBox(cboJobType);
            cboJobType.DataSource = dtJobType.Copy();

            Dictionary<string, object> paramProductionType = new Dictionary<string, object>();
            paramProductionType.Add("CODECLASSID", "ProductionType");
            paramProductionType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtProductionType = SqlExecuter.Query("GetTypeList", "10001", paramProductionType);
            SetSmartComboBox(cboProductionType);
            cboProductionType.DataSource = dtProductionType.Copy();

            Dictionary<string, object> paramInputType = new Dictionary<string, object>();
            paramInputType.Add("CODECLASSID", "InputType");
            paramInputType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtInputType = SqlExecuter.Query("GetTypeList", "10001", paramInputType);
            SetSmartComboBox(cboInputType);
            cboInputType.DataSource = dtInputType.Copy();
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

        #region 저장

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

            /*
			foreach (var control in tlpProductInfo.Controls)
			{
				switch(control.GetType().Name)
				{
					case "SmartTextBox":
						SmartTextBox txtCtl = control as SmartTextBox;
						dataDictionary.Add(txtCtl.Tag.ToString(), txtCtl.Text);
						break;

					case "SmartComboBox":
						SmartComboBox cboCtl = control as SmartComboBox;
						dataDictionary.Add(cboCtl.Tag.ToString(), cboCtl.GetDataValue());
						break;
					case "SmartSelectPopupEdit":
						SmartSelectPopupEdit popCtl = control as SmartSelectPopupEdit;
						dataDictionary.Add(popCtl.Tag.ToString(), popCtl.GetValue());
						break;
				}
			}
			*/

            return dataDictionary;
        }

        #endregion

        #region 데이터 바인드

        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind(DataTable dt)
        {
            if (dt.Rows.Count <= 0) return;

            DataRow r = dt.Rows[0];

            CommonFunctionProductSpec.SearchDataBind(r, tlpProductInfo);
  
        }

        public void DisplayComboChange(SmartComboBox comboBox)
        {
            comboBox.DisplayMember = "CODENAME";
            comboBox.ValueMember = "CODENAME";
            comboBox.ShowHeader = false;
            comboBox.UseEmptyItem = true;
            comboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
        }



        public void DisplayComboChange2(SmartComboBox comboBox)
        {
            comboBox.DisplayMember = "CODENAME";
            comboBox.ValueMember = "CODEID";
            comboBox.ShowHeader = false;
            comboBox.UseEmptyItem = true;
            comboBox.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
        }


        public void ComboChange(int i)
        {
            if(i==1)
            {
                DisplayComboChange(cbEndUser);
                DisplayComboChange(cbOutboundType);
                DisplayComboChange(cboJobType);
                DisplayComboChange(cboProductionType);
                DisplayComboChange(cboInputType);
            }
            else {
                DisplayComboChange2(cbEndUser);
                DisplayComboChange2(cbOutboundType);
                DisplayComboChange2(cboJobType);
                DisplayComboChange2(cboProductionType);
                DisplayComboChange2(cboInputType);

            }
        }


        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(tlpProductInfo);
        }

        #endregion

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public DataTable productinforeturn()
        {

            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(tlpProductInfo, DataSource);


            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, tlpProductInfo);
            DataSource.Rows.Add(row);

            return DataSource;


        }


        public string itemcodereturn()
        {

            return tbProductCode.EditValue.ToString();
        }
        private void smartLabel5_Click(object sender, EventArgs e)
        {

        }
    }


}
