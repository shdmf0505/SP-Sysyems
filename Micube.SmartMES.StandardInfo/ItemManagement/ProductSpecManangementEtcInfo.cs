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
using System.Text.RegularExpressions;

namespace Micube.SmartMES.StandardInfo
{
    public partial class ProductSpecManangementEtcInfo : UserControl
    {
		#region 생성자 

		public ProductSpecManangementEtcInfo()
        {
            InitializeComponent();

			if(!this.IsDesignMode())
			{
				InitializeComboControl();
				InitializeTextControl();
			}
            
        }

		#endregion

		#region 컨텐츠 초기화

		/// <summary>
		/// ComboBox 초기화
		/// </summary>
		private void InitializeComboControl()
        {
			Dictionary<string, object> ParamLayerUp = new Dictionary<string, object>();
			ParamLayerUp.Add("CODECLASSID", "CopperLayerUp");
			ParamLayerUp.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtLayerUp = SqlExecuter.Query("GetTypeList", "10001", ParamLayerUp);

			SetSmartComboBox(cbCooperLayerUp);
			cbCooperLayerUp.DataSource = dtLayerUp.Copy();

			////////////////////////////////////////////////////////////////////////////////////////

            Dictionary<string, object> ParamLayerDown = new Dictionary<string, object>();
            ParamLayerDown.Add("CODECLASSID", "CopperLayerDown");
            ParamLayerDown.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtLayerDown = SqlExecuter.Query("GetTypeList", "10001", ParamLayerDown);

			SetSmartComboBox(cbCooperLayerDown);
			cbCooperLayerDown.DataSource = dtLayerDown.Copy();

			////////////////////////////////////////////////////////////////////////////////////////

			Dictionary<string, object> ParamYn = new Dictionary<string, object>();
            ParamYn.Add("CODECLASSID", "YesNo");
            ParamYn.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtYn = SqlExecuter.Query("GetTypeList", "10001", ParamYn);

			SetSmartComboBox(cbNewDataValid);
			cbNewDataValid.DataSource = dtYn.Copy();

        }

		/// <summary>
		/// TextBox 초기화
		/// </summary>
		private void InitializeTextControl()
        {
            tbLotManager.Text = "Y";
            tbLotManager.ReadOnly = true;

            TextBoxHelper.SetUnitMask(Unit.PCS, tbMeasurement);
            TextBoxHelper.SetUnitMask(Unit.PCS, tbReliablity);
            TextBoxHelper.SetUnitMask(Unit.PCS, tbHazard);
            TextBoxHelper.SetUnitMask(Unit.MM, tbArray);
            TextBoxHelper.SetUnitMask(Unit.MM, tbActualMeasure1);
            TextBoxHelper.SetUnitMask(Unit.MM, tbActualMeasure2);
            TextBoxHelper.SetUnitMask(Unit.MM, tbActualMeasure3);
            TextBoxHelper.SetUnitMask(Unit.MM, tbActualMeasure4);
            TextBoxHelper.SetUnitMask(Unit.MM, tbActualMeasure5);
            TextBoxHelper.SetUnitMask(Unit.MM, tbSpec1);
            TextBoxHelper.SetUnitMask(Unit.MM, tbSpec2);
            TextBoxHelper.SetUnitMask(Unit.MM, tbSpec3);
            TextBoxHelper.SetUnitMask(Unit.MM, tbSpec4);
            TextBoxHelper.SetUnitMask(Unit.MM, tbSpec5);
            TextBoxHelper.SetUnitMask(Unit.MM, tbTheoriticalFigure1);
            TextBoxHelper.SetUnitMask(Unit.MM, tbTheoriticalFigure2);
            TextBoxHelper.SetUnitMask(Unit.MM, tbTheoriticalFigure3);
            TextBoxHelper.SetUnitMask(Unit.MM, tbTheoriticalFigure4);
            TextBoxHelper.SetUnitMask(Unit.MM, tbTheoriticalFigure5);


            TextBoxHelper.SetMarkMask(Mark.XAxis, tbInputNumaxisX);
            TextBoxHelper.SetMarkMask(Mark.YAxis, tbInputNumaxisY);
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
		/// 
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> Save(out DataTable dt)
        {
			//필수 입력 체크
			List<string> requiredList = new List<string>();
			CommonFunctionProductSpec.GetRequiredValidationList(tlpEtcInfo, requiredList);

            // 사양변경 내용, 특이사항은 하나만 입력되더라도 저장 진행될수 있도록 Validation 에서 제외후 다시 확인  
            if (requiredList.Contains("SPECCHANGE"))
                requiredList.Remove("SPECCHANGE");

            if (requiredList.Contains("SPECIALNOTE"))
                requiredList.Remove("SPECIALNOTE");

            CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpEtcInfo, requiredList, smartGroupBox1.LanguageKey);

            //사양변경 내용, 특이사항은 하나만 입력되더라도 저장 진행될수 있도록 Validation 에서 제외후 다시 확인  
            if (string.IsNullOrEmpty(txtSpecChange1.Text) 
                && string.IsNullOrEmpty(txtSpecChange2.Text) 
                && string.IsNullOrEmpty(txtSpecChange3.Text) 
                && string.IsNullOrEmpty(txtSpecChange4.Text))
                throw MessageException.Create("InValidOspRequiredField", smartGroupBox1.Text + " - " + smartLabel93.Text);

            if (string.IsNullOrEmpty(txtSpecNotice1.Text)
                && string.IsNullOrEmpty(txtSpecNotice2.Text) 
                && string.IsNullOrEmpty(txtSpecNotice3.Text) 
                && string.IsNullOrEmpty(txtSpecNotice4.Text) 
                && string.IsNullOrEmpty(txtSpecNotice5.Text))
                throw MessageException.Create("InValidOspRequiredField", smartGroupBox1.Text + " - " + smartLabel96.Text);


            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
			CommonFunctionProductSpec.GetSaveDataDictionary(tlpEtcInfo, dataDictionary);

			List<string> thickList = new List<string>();
			thickList.Add("THICKNO");//
			thickList.Add("THICKPOSITION");//부위
			thickList.Add("THICKSPEC");//SPEC
			thickList.Add("THICKTHEORETICALVALUE");//이론치
			thickList.Add("THICKEXPERIMENTALVALUE");//실측지

			dt = new DataTable();

			dt.Columns.Add("THICKNO", typeof(string));
			dt.Columns.Add("THICKPOSITION", typeof(string));
			dt.Columns.Add("THICKSPEC", typeof(string));
			dt.Columns.Add("THICKTHEORETICALVALUE", typeof(string));
			dt.Columns.Add("THICKEXPERIMENTALVALUE", typeof(string));
			
			DataRow row1 = dt.NewRow();
			DataRow row2 = dt.NewRow();
			DataRow row3 = dt.NewRow();
			DataRow row4 = dt.NewRow();
			DataRow row5 = dt.NewRow();

			row1["THICKNO"] = "1";
			row2["THICKNO"] = "2";
			row3["THICKNO"] = "3";
			row4["THICKNO"] = "4";
			row5["THICKNO"] = "5";


			for (int i = 0; i < thickList.Count; i++)
			{
				var query = from p in dataDictionary.AsEnumerable()
							where p.Key.Contains(thickList[i])
							select p;

				List<KeyValuePair<string, object>> pl = query.ToList();
				for(int k = 0; k < pl.Count; k++)
				{
					string no = Regex.Replace(pl[k].Key, @"[^\d+]", "");
					string column = Regex.Replace(pl[k].Key, @"\d+", "");

					switch(no)
					{
						case "1":
							row1[column] = pl[k].Value;
							break;
						case "2":
							row2[column] = pl[k].Value;
							break;
						case "3":
							row3[column] = pl[k].Value;
							break;
						case "4":
							row4[column] = pl[k].Value;
							break;
						case "5":
							row5[column] = pl[k].Value;
							break;
					}//switch
				}//for
			}//for


			dt.Rows.Add(row1);
			dt.Rows.Add(row2);
			dt.Rows.Add(row3);
			dt.Rows.Add(row4);
			dt.Rows.Add(row5);

			return dataDictionary;
        }

		#region 데이터 바인드

		/// <summary>
		/// 조회 데이터 바인드
		/// </summary>
		/// <param name="dt"></param>
		public void DataBind(DataTable dt)
		{
            if (dt.Rows.Count <= 0) return;

			DataRow r = dt.Rows[0];

			CommonFunctionProductSpec.SearchDataBind(r, tlpEtcInfo);
		}

		/// <summary>
		/// 조회 데이터 바인드
		/// </summary>
		/// <param name="dt"></param>
		public void DataBind(DataTable dt, bool isAnother)
		{
			if (dt.Rows.Count <= 0) return;

			foreach(DataRow r in dt.Rows)
			{
				DataTable dtBind = new DataTable();
				DataRow row = dtBind.NewRow();
				string no = Format.GetString(r["THICKNO"]);
				if(!string.IsNullOrEmpty(no))
				{
					dtBind.Columns.Add("THICKPOSITION" + no, typeof(string));
					dtBind.Columns.Add("THICKSPEC" + no, typeof(string));
					dtBind.Columns.Add("THICKTHEORETICALVALUE" + no, typeof(string));
					dtBind.Columns.Add("THICKEXPERIMENTALVALUE" + no, typeof(string));

					row["THICKPOSITION" + no] = r["THICKPOSITION"];
					row["THICKSPEC" + no] = r["THICKSPEC"];
					row["THICKTHEORETICALVALUE" + no] = r["THICKTHEORETICALVALUE"];
					row["THICKEXPERIMENTALVALUE" + no] = r["THICKEXPERIMENTALVALUE"];
				}
				dtBind.Rows.Add(row);
				CommonFunctionProductSpec.SearchDataBind(row, tlpEtcInfo);
			}
			
		}


        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(tlpEtcInfo);
        }

        #endregion
    }
}
