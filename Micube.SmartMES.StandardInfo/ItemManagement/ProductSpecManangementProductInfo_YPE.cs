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
    public partial class ProductSpecManangementProductInfo_YPE : UserControl
    {
        public DataTable DataSource
        {

            get; private set;
        }
        public ProductSpecManangementProductInfo_YPE()
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
			ConditionItemSelectPopup managerSelectPopup = new ConditionItemSelectPopup();
            managerSelectPopup.Id = "USERID";
            managerSelectPopup.SearchQuery = new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            managerSelectPopup.ValueFieldName = "USERID";
            managerSelectPopup.DisplayFieldName = "USERNAME";
            managerSelectPopup.SetPopupLayout("SELECTUSER", PopupButtonStyles.Ok_Cancel, true, true);
            managerSelectPopup.SetPopupResultCount(0);
            managerSelectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            managerSelectPopup.SetPopupAutoFillColumns("DEPARTMENT");

			managerSelectPopup.Conditions.AddTextBox("USERIDNAME");

			managerSelectPopup.GridColumns.AddTextBoxColumn("USERID", 150);
            managerSelectPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            managerSelectPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 60);

			smartSelectPopupEditSpecMen.SelectPopupCondition = managerSelectPopup;
			smartSelectPopupEditCamMen.SelectPopupCondition = managerSelectPopup;
            smartSelectPopupEditSalesMen.SelectPopupCondition = managerSelectPopup;

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
			paramInputType.Add("CODECLASSID", "JobType");
			paramInputType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtInputType = SqlExecuter.Query("GetTypeList", "10001", paramInputType);
			SetSmartComboBox(cboInputType);
			cboInputType.DataSource = dtInputType.Copy();

			//smartSelectPopupEditSpecMen.EditValue = UserInfo.Current.Id;
			//smartSelectPopupEditSpecMen.Text = UserInfo.Current.Name;

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
			CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpProductInfo, requiredList);

			Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
			CommonFunctionProductSpec.GetSaveDataDictionary(tlpProductInfo, dataDictionary);

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

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(tlpProductInfo);
        }

        public string itemcodereturn()
        {

            return tbProductCode.EditValue.ToString();
        }

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public DataTable productinforeturn()
        {
            DataSource = new DataTable();
            DataSource.Columns.Add(new DataColumn("INPUTDAY", typeof(string))); // 투입일
            DataSource.Columns.Add(new DataColumn("OUTPUTDAY", typeof(string))); // 납풉요청일
            DataSource.Columns.Add(new DataColumn("ITEMCODE", typeof(string))); // 품목코드
            DataSource.Columns.Add(new DataColumn("ITEMNAME", typeof(string))); // 품목코드
            DataSource.Columns.Add(new DataColumn("PRODUCTDEFVERSION", typeof(string))); // 내부REV
            DataSource.Columns.Add(new DataColumn("PRODUCTLEVEL", typeof(string))); // 제품등급
            DataSource.Columns.Add(new DataColumn("PRODUCTIONTYPE", typeof(string))); // 생산구분
            DataSource.Columns.Add(new DataColumn("JOBTYPE", typeof(string))); // 작업구분
            DataSource.Columns.Add(new DataColumn("MAINFACTORY", typeof(string))); // 주 제조공장
            DataSource.Columns.Add(new DataColumn("SPECPERSON", typeof(string))); // 사양담당자




            DataRow row = DataSource.NewRow();

            row["INPUTDAY"] = smartTextBoxInputDate.EditValue;
            row["OUTPUTDAY"] = smartDateEditDeadLineDate.EditValue;
            row["ITEMCODE"] = tbProductCode.EditValue;
            row["MAINFACTORY"] = smartTextBox1.EditValue;
            row["PRODUCTLEVEL"] = smartTextBoxProdLevel.EditValue;
            row["PRODUCTDEFVERSION"] = smartTextBoxInnerRev.EditValue;

            DataTable dt = cboProductionType.DataSource as DataTable;
            if (cboProductionType.EditValue != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Equals(cboProductionType.EditValue.ToString()))
                    {
                        row["PRODUCTIONTYPE"] = dt.Rows[i]["CODENAME"].ToString();
                    }
                }
            }
            row["JOBTYPE"] = cboJobType.EditValue;
            row["SPECPERSON"] = smartSelectPopupEditSpecMen.EditValue;
            row["ITEMNAME"] = tbProdName.EditValue;

            DataSource.Rows.Add(row);

            return DataSource;
        }
        #endregion

        private void smartLabel7_Click(object sender, EventArgs e)
        {

        }
    }
}
