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
    public partial class ProductSpecManagementPlatingSpec : UserControl
    {
		#region 생성자

		public ProductSpecManagementPlatingSpec()
        {
            InitializeComponent();

			if(!this.IsDesignMode())
			{
				InitializeComboControl();
				InitializeTextControl();
                
			}
		}

		#endregion

		#region 컨트롤 초기화

		/// <summary>
		/// ComboBox 컨트롤 초기화
		/// </summary>
		private void InitializeComboControl()
        {
			Dictionary<string, object> ParamCopperPlatingType = new Dictionary<string, object>();
			ParamCopperPlatingType.Add("CODECLASSID", "CopperPlatingType");
			ParamCopperPlatingType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtCopperPlatingType = SqlExecuter.Query("GetTypeList", "10001", ParamCopperPlatingType);

			SetSmartComboBox(cbInnerCopper1);
            SetSmartComboBox(cbInnerCopper2);
            SetSmartComboBox(cbInnerCopper3);
            SetSmartComboBox(cbOuterCopper);

			cbInnerCopper1.DataSource = dtCopperPlatingType.Copy();
			cbInnerCopper2.DataSource = dtCopperPlatingType.Copy();
			cbInnerCopper3.DataSource = dtCopperPlatingType.Copy();
			cbOuterCopper.DataSource = dtCopperPlatingType.Copy();

            //cbInnerCopper1.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbInnerCopper2.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbInnerCopper3.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbOuterCopper.EditValueChanged += ComboBoxValidation_EditValueChanged;

            /////////////////////////////////////////////////////////////////////////////////

            Dictionary<string, object> ParamPlatingType = new Dictionary<string, object>();
			ParamPlatingType.Add("CODECLASSID", "PlatingType");
			ParamPlatingType.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtPlatingType = SqlExecuter.Query("GetTypeList", "10001", ParamPlatingType);
			
			SetSmartComboBox(cbPlatingType1);
            SetSmartComboBox(cbPlatingType2);
            SetSmartComboBox(cbPlatingType3);

			cbPlatingType1.DataSource = dtPlatingType.Copy();
			cbPlatingType2.DataSource = dtPlatingType.Copy();
			cbPlatingType3.DataSource = dtPlatingType.Copy();

            //cbPlatingType1.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbPlatingType2.EditValueChanged += ComboBoxValidation_EditValueChanged;
            //cbPlatingType3.EditValueChanged += ComboBoxValidation_EditValueChanged;

            /////////////////////////////////////////////////////////////////////////////////

            Dictionary<string, object> ParamWorkSurface = new Dictionary<string, object>();
			ParamWorkSurface.Add("CODECLASSID", "WorkSurface");
			ParamWorkSurface.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtWorkSurface = SqlExecuter.Query("GetTypeList", "10001", ParamWorkSurface);

			SetSmartComboBox(cbPlatingType1_4);
            SetSmartComboBox(cbPlatingType1_5);
            SetSmartComboBox(cbPlatingType2_4);
            SetSmartComboBox(cbPlatingType2_5);
            SetSmartComboBox(cbPlatingType3_4);
            SetSmartComboBox(cbPlatingType3_5);
            
            cbPlatingType1_4.UseEmptyItem = false;
            cbPlatingType1_4.DataSource = dtWorkSurface.Copy();
            cbPlatingType1_4.ItemIndex = 0;
            cbPlatingType1_5.UseEmptyItem = false;
            cbPlatingType1_5.DataSource = dtWorkSurface.Copy();
            cbPlatingType1_5.ItemIndex = 1;
            cbPlatingType2_4.UseEmptyItem = false;
            cbPlatingType2_4.DataSource = dtWorkSurface.Copy();
            cbPlatingType2_4.ItemIndex = 0;
            cbPlatingType2_5.UseEmptyItem = false;
            cbPlatingType2_5.DataSource = dtWorkSurface.Copy();
            cbPlatingType2_5.ItemIndex = 1;
            cbPlatingType3_4.UseEmptyItem = false;
            cbPlatingType3_4.DataSource = dtWorkSurface.Copy();
            cbPlatingType3_4.ItemIndex = 0;
            cbPlatingType3_5.UseEmptyItem = false;
            cbPlatingType3_5.DataSource = dtWorkSurface.Copy();
            cbPlatingType3_5.ItemIndex = 1;
        }

        /// <summary>
        /// Combo를 선택했을 때 필수 체크 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxValidation_EditValueChanged(object sender, EventArgs e)
        {
            SmartComboBox cmb = sender as SmartComboBox;
            string strName = cmb.Name.Substring(2);

            var lableControls = this.Controls.Find<SmartLabel>(true);

            if (string.IsNullOrEmpty(cmb.GetDataValue().ToString()))
            {
                foreach (SmartLabel lb in lableControls)
                {
                    if (lb.Name.Contains(strName))
                        lb.ForeColor = Color.Black;
                }

                // text 값 초기화
                var txtControls = this.Controls.Find<SmartTextBox>(true);
                foreach (SmartTextBox tb in txtControls)
                {
                    if (tb.Name.StartsWith("tb" + strName))
                        tb.EditValue = string.Empty;
                }
            }
            else
            {
                foreach (SmartLabel lb in lableControls)
                {
                    if (lb.Name.Contains(strName))
                        lb.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// TextBox 컨트롤 초기화
        /// </summary>
        private void InitializeTextControl()
        {
            TextBoxHelper.SetUnitMask(Unit.um, tbOuterCopper1);
            TextBoxHelper.SetUnitMask(Unit.um, tbInnerCopper1_1);
            TextBoxHelper.SetUnitMask(Unit.um, tbInnerCopper2_1);
            TextBoxHelper.SetUnitMask(Unit.um, tbInnerCopper3_1);
            TextBoxHelper.SetUnitMask(Unit.um, tbOuterCopper2);
            TextBoxHelper.SetUnitMask(Unit.um, tbInnerCopper1_2);
            TextBoxHelper.SetUnitMask(Unit.um, tbInnerCopper2_2);
            TextBoxHelper.SetUnitMask(Unit.um, tbInnerCopper3_2);

            TextBoxHelper.SetUnitMask(Unit.um, tbPlatingType1_1);
            TextBoxHelper.SetUnitMask(Unit.um, tbPlatingType2_1);
            TextBoxHelper.SetUnitMask(Unit.um, tbPlatingType3_1);

            TextBoxHelper.SetUnitMask(Unit.um, tbPlatingType1_2);
            TextBoxHelper.SetUnitMask(Unit.um, tbtbPlatingType2_2);
            TextBoxHelper.SetUnitMask(Unit.um, tbPlatingType3_2);

            TextBoxHelper.SetUnitMask(Unit.um, tbPlatingType1_3);
            TextBoxHelper.SetUnitMask(Unit.um, tbtbPlatingType2_3);
            TextBoxHelper.SetUnitMask(Unit.um, tbPlatingType3_3);

            TextBoxHelper.SetUnitMask(Unit.sqmm, tbPlatingType1_6);
            TextBoxHelper.SetUnitMask(Unit.sqmm, tbPlatingType1_7);
            TextBoxHelper.SetUnitMask(Unit.sqmm, tbtbPlatingType2_6);
            TextBoxHelper.SetUnitMask(Unit.sqmm, tbtbPlatingType2_7);
            TextBoxHelper.SetUnitMask(Unit.sqmm, tbPlatingType3_6);
            TextBoxHelper.SetUnitMask(Unit.sqmm, tbPlatingType3_7);
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


        /// <summary>
        /// 조회 데이터 바인드
        /// </summary>
        /// <param name="dt"></param>
        public void DataBind(DataTable dt)
        {

            if (dt == null || dt.Rows.Count <= 0) return;

            foreach (DataRow dr in dt.Rows)
            {
                string strDetailType = string.Empty;
                if (dr["DETAILTYPE"].ToString().Equals("CopperPlating"))
                    strDetailType = "COPPER" + dr["SEQUENCE"].ToString();
                else if (dr["DETAILTYPE"].ToString().Equals("SurfacePlating"))
                    strDetailType = "SURFACE" + dr["SEQUENCE"].ToString(); ;

                Dictionary<string, object> dicData = new Dictionary<string, object>();
                dicData.Add(strDetailType, dr["DETAILNAME"]);
                dicData.Add(strDetailType + "_SPECDETAILFROM", dr["SPECDETAILFROM"]);
                dicData.Add(strDetailType + "_SPECDETAILTO", dr["SPECDETAILTO"]);

                if (dr["FROMORIGINAL"].ToString().Contains("|"))
                {
                    string[] strValues = dr["FROMORIGINAL"].ToString().Split('|');
                    for (int i = 0; i < strValues.Length; i++)
                    {
                        dicData.Add(strDetailType + "_FROMORIGINAL" + (i + 1).ToString(), strValues[i]);
                    }
                }
                else
                    dicData.Add(strDetailType + "_FROMORIGINAL", dr["FROMORIGINAL"]);

                if (dr["TOORIGINAL"].ToString().Contains("|"))
                {
                    string[] strValues = dr["TOORIGINAL"].ToString().Split('|');
                    for (int i = 0; i < strValues.Length; i++)
                    {
                        dicData.Add(strDetailType + "_TOORIGINAL" + (i + 1).ToString(), strValues[i]);
                    }
                }
                else
                    dicData.Add(strDetailType + "_TOORIGINAL", dr["TOORIGINAL"]);

                if (dr["DESCRIPTION"].ToString().Contains("|"))
                {
                    string[] strValues = dr["DESCRIPTION"].ToString().Split('|');
                    for (int i = 0; i < strValues.Length; i++)
                    {
                        dicData.Add(strDetailType + "_DESCRIPTION" + (i + 1).ToString(), strValues[i]);
                    }
                }
                else
                    dicData.Add(strDetailType + "_DESCRIPTION", dr["DESCRIPTION"]);

                CommonFunctionProductSpec.SearchDataBind(dicData, smartSplitTableLayoutPanel1);
            }

        }


        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(smartSplitTableLayoutPanel1);
        }


        /// <summary>
        /// 저장 
        /// </summary>
        //public Dictionary<string, object> Save()
        public DataTable Save()
        {
            //ValidationCheck();
            DataTable dtRtn = new DataTable("PlatingInfo");
            dtRtn.Columns.Add("DETAILTYPE");
            dtRtn.Columns.Add("SEQUENCE");
            dtRtn.Columns.Add("DETAILNAME");
            dtRtn.Columns.Add("SPECDETAILFROM");
            dtRtn.Columns.Add("SPECDETAILTO");
            dtRtn.Columns.Add("FROMORIGINAL");
            dtRtn.Columns.Add("TOORIGINAL");
            dtRtn.Columns.Add("DESCRIPTION");

            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
            CommonFunctionProductSpec.GetSaveDataDictionary(smartSplitTableLayoutPanel1, dataDictionary);

            // 동도금 정보
            for (int i = 1; i <= 4; i++)
            {
                DataRow drCopperPlating = dtRtn.NewRow();
                drCopperPlating["DETAILTYPE"] = "CopperPlating";
                drCopperPlating["SEQUENCE"] = i.ToString();

                if (dataDictionary.ContainsKey("COPPER" + i.ToString()))
                    drCopperPlating["DETAILNAME"] = dataDictionary["COPPER" + i.ToString()];

                if (dataDictionary.ContainsKey("COPPER" + i.ToString() + "_SPECDETAILFROM"))    //도금조건
                    drCopperPlating["SPECDETAILFROM"] = dataDictionary["COPPER" + i.ToString() + "_SPECDETAILFROM"];

                if (dataDictionary.ContainsKey("COPPER" + i.ToString() + "_SPECDETAILTO"))          //고객조건
                    drCopperPlating["SPECDETAILTO"] = dataDictionary["COPPER" + i.ToString() + "_SPECDETAILTO"];

                var vFormOrig = from p in dataDictionary.AsEnumerable()
                                where p.Key.StartsWith("COPPER" + i.ToString() + "_FROMORIGINAL")
                                orderby p.Key
                                select p.Value.ToString().Split(' ')[0];

                if (vFormOrig != null && vFormOrig.Count() > 0)
                    drCopperPlating["FROMORIGINAL"] = string.Join("|", vFormOrig);

                var vToOrig = from p in dataDictionary.AsEnumerable()
                              where p.Key.StartsWith("COPPER" + i.ToString() + "_TOORIGINAL")
                              orderby p.Key
                              select p.Value.ToString().Split(' ')[0]; 

                if (vToOrig != null && vToOrig.Count() > 0)
                    drCopperPlating["TOORIGINAL"] = string.Join("|", vToOrig);

                dtRtn.Rows.Add(drCopperPlating);
            }

            // 표면도금 정보
            for (int i = 1; i <= 3; i++)
            {
                DataRow drCopperPlating = dtRtn.NewRow();
                drCopperPlating["DETAILTYPE"] = "SurfacePlating";
                drCopperPlating["SEQUENCE"] = i.ToString();

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString()))
                    drCopperPlating["DETAILNAME"] = dataDictionary["SURFACE" + i.ToString()];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_SPECDETAILFROM"))    //도금면적 C 콤보
                    drCopperPlating["SPECDETAILFROM"] = dataDictionary["SURFACE" + i.ToString() + "_SPECDETAILFROM"];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_SPECDETAILTO"))          //도금면적 S 콤보
                    drCopperPlating["SPECDETAILTO"] = dataDictionary["SURFACE" + i.ToString() + "_SPECDETAILTO"];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_FROMORIGINAL"))    //도금면적 C 값
                    drCopperPlating["FROMORIGINAL"] = dataDictionary["SURFACE" + i.ToString() + "_FROMORIGINAL"];

                if (dataDictionary.ContainsKey("SURFACE" + i.ToString() + "_TOORIGINAL"))          //도금면적 S 값
                    drCopperPlating["TOORIGINAL"] = dataDictionary["SURFACE" + i.ToString() + "_TOORIGINAL"];

                var vDesc = from p in dataDictionary.AsEnumerable()
                            where p.Key.StartsWith("SURFACE" + i.ToString() + "_DESCRIPTION")
                            orderby p.Key
                            select p.Value.ToString().Split(' ')[0];

                if (vDesc != null && vDesc.Count() > 0)
                    drCopperPlating["DESCRIPTION"] = string.Join("|", vDesc);

                dtRtn.Rows.Add(drCopperPlating);
            }


            return dtRtn;

            #region Dictionary Retrun 주석
            //Dictionary<string, object> dataDictionary = new Dictionary<string, object>();

            //foreach (var control in smartSplitTableLayoutPanel1.Controls)
            //{
            //	switch (control.GetType().Name)
            //	{
            //		case "SmartTextBox":
            //			SmartTextBox txtCtl = control as SmartTextBox;
            //			dataDictionary.Add(txtCtl.Tag.ToString(), txtCtl.Text);
            //			break;

            //		case "SmartComboBox":
            //			SmartComboBox cboCtl = control as SmartComboBox;
            //			dataDictionary.Add(cboCtl.Tag.ToString(), cboCtl.GetDataValue());
            //			break;
            //		case "SmartSelectPopupEdit":
            //			SmartSelectPopupEdit popCtl = control as SmartSelectPopupEdit;
            //			dataDictionary.Add(popCtl.Tag.ToString(), popCtl.GetValue());
            //			break;
            //	}

            //}
            //return dataDictionary;

            #endregion
        }

        /// <summary>
        /// 필수 체크 (콤보박스 선택값이 있을 경우에만...)
        /// </summary>
        private void ValidationCheck()
        {
            //string groupName = this.smartLabelName.Text;
            string groupName = this.smartGroupBox1.Text;

            if (!string.IsNullOrEmpty(cbOuterCopper.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbOuterCopper1.Text)
                    || string.IsNullOrWhiteSpace(tbOuterCopper2.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + cbOuterCopper.Text);
            }

            if (!string.IsNullOrEmpty(cbInnerCopper1.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbInnerCopper1_1.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper1_2.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbInnerCopper1.Text);
            }

            if (!string.IsNullOrEmpty(cbInnerCopper2.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbInnerCopper2_1.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper2_2.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbInnerCopper2.Text);
            }

            if (!string.IsNullOrEmpty(cbInnerCopper3.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbInnerCopper3_1.Text)
                    || string.IsNullOrWhiteSpace(tbInnerCopper3_2.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbInnerCopper3.Text);
            }

            if (!string.IsNullOrEmpty(cbPlatingType1.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbPlatingType1_1.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType1_2.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType1_3.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType1_6.Text) || string.IsNullOrWhiteSpace(tbPlatingType1_7.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbPlatingType1.Text);
            }

            if (!string.IsNullOrEmpty(cbPlatingType2.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbPlatingType2_1.Text)
                    || string.IsNullOrWhiteSpace(tbtbPlatingType2_2.Text)
                    || string.IsNullOrWhiteSpace(tbtbPlatingType2_3.Text)
                    || string.IsNullOrWhiteSpace(tbtbPlatingType2_6.Text) || string.IsNullOrWhiteSpace(tbtbPlatingType2_7.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbPlatingType2.Text);
            }

            if (!string.IsNullOrEmpty(cbPlatingType3.GetDataValue().ToString()))
            {
                if (string.IsNullOrWhiteSpace(tbPlatingType3_1.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType3_2.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType3_3.Text)
                    || string.IsNullOrWhiteSpace(tbPlatingType3_6.Text) || string.IsNullOrWhiteSpace(tbPlatingType3_7.Text))
                    throw MessageException.Create("InValidOspRequiredField", groupName + lbPlatingType3.Text);
            }

            #region 주석


            //if (!string.IsNullOrEmpty(cbOuterCopper.GetDataValue().ToString()))
            //{
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition1.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGCONDITION"));
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition1.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("CUSTOMERCRITERIA"));
            //}

            //if (!string.IsNullOrEmpty(cbInnerCopper1.GetDataValue().ToString()))
            //{
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition2.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGCONDITION"));
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition2.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("CUSTOMERCRITERIA"));
            //}

            //if (!string.IsNullOrEmpty(cbInnerCopper2.GetDataValue().ToString()))
            //{
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition3.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGCONDITION"));
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition3.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("CUSTOMERCRITERIA"));
            //}

            //if (!string.IsNullOrEmpty(cbInnerCopper3.GetDataValue().ToString()))
            //{
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition4.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGCONDITION"));
            //    if (string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition4.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("CUSTOMERCRITERIA"));
            //}

            //if (!string.IsNullOrEmpty(cbPlatingType1.GetDataValue().ToString()))
            //{
            //    if (string.IsNullOrWhiteSpace(tbNi1.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Ni"));
            //    if (string.IsNullOrWhiteSpace(tbAu1.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Au"));
            //    if (string.IsNullOrWhiteSpace(tbPd1.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Pd"));
            //    if (string.IsNullOrWhiteSpace(tbCopperArea11.Text) || string.IsNullOrWhiteSpace(tbCopperArea12.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGAREA"));
            //}

            //if (!string.IsNullOrEmpty(cbPlatingType2.GetDataValue().ToString()))
            //{
            //    if (string.IsNullOrWhiteSpace(tbNi2.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Ni"));
            //    if (string.IsNullOrWhiteSpace(tbAu2.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Au"));
            //    if (string.IsNullOrWhiteSpace(tbPd2.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Pd"));
            //    if (string.IsNullOrWhiteSpace(tbCopperArea21.Text) || string.IsNullOrWhiteSpace(tbCopperArea22.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGAREA"));
            //}

            //if (!string.IsNullOrEmpty(cbPlatingType3.GetDataValue().ToString()))
            //{
            //    if (string.IsNullOrWhiteSpace(tbNi3.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Ni"));
            //    if (string.IsNullOrWhiteSpace(tbAu3.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Au"));
            //    if (string.IsNullOrWhiteSpace(tbPd3.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Pd"));
            //    if (string.IsNullOrWhiteSpace(tbCopperArea31.Text) || string.IsNullOrWhiteSpace(tbCopperArea32.Text))
            //        throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGAREA"));
            //}

            #endregion

            #region 콤보선택과 상관없이 무조건 체크 - 주석처리
            //if (string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition1.Text) ||
            //    string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition2.Text) ||
            //    string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition3.Text) ||
            //    string.IsNullOrWhiteSpace(SmartTextBoxCopperPlatingCondition4.Text))
            //    throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGCONDITION"));

            //if (string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition1.Text) ||
            //    string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition2.Text) ||
            //    string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition3.Text) ||
            //    string.IsNullOrWhiteSpace(SmartTextBoxCustomerCondition4.Text))
            //    throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("CUSTOMERCRITERIA"));

            //if (string.IsNullOrWhiteSpace(tbAu1.Text) ||
            //    string.IsNullOrWhiteSpace(tbAu2.Text) ||
            //    string.IsNullOrWhiteSpace(tbAu3.Text))
            //    throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Au"));

            //if (string.IsNullOrWhiteSpace(tbNi1.Text) ||
            //    string.IsNullOrWhiteSpace(tbNi2.Text) ||
            //    string.IsNullOrWhiteSpace(tbNi3.Text))
            //    throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Ni"));

            //if (string.IsNullOrWhiteSpace(tbPd1.Text) ||
            //    string.IsNullOrWhiteSpace(tbPd2.Text) ||
            //    string.IsNullOrWhiteSpace(tbPd3.Text))
            //    throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("Pd"));

            //if (string.IsNullOrWhiteSpace(tbCopperArea11.Text) ||
            //    string.IsNullOrWhiteSpace(tbCopperArea12.Text) ||
            //    string.IsNullOrWhiteSpace(tbCopperArea21.Text) ||
            //    string.IsNullOrWhiteSpace(tbCopperArea22.Text) ||
            //    string.IsNullOrWhiteSpace(tbCopperArea31.Text) ||
            //    string.IsNullOrWhiteSpace(tbCopperArea32.Text))
            //    throw MessageException.Create("InValidOspRequiredField", groupName + Language.Get("PLATINGAREA"));
            #endregion
        }

        #region event

        #endregion
    }
}
