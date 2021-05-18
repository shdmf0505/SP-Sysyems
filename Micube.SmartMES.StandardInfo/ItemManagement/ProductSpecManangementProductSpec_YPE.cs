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
    public partial class ProductSpecManangementProductSpec_YPE : UserControl
    {
        public DataTable DataSource
        {

            get; private set;
        }
        public ProductSpecManangementProductSpec_YPE()
        {
            InitializeComponent();

            InitializeComboControl();
            InitializeTextControl();
        }

		#region 컨트롤 초기화

		/// <summary>
		/// Combo 컨트롤 초기화
		/// </summary>
		private void InitializeComboControl()
        {
            Dictionary<string, object> ParamYn = new Dictionary<string, object>();
            ParamYn.Add("CODECLASSID", "YesNo");
            ParamYn.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtYn = SqlExecuter.Query("GetTypeList", "10001", ParamYn);

			SetSmartComboBox(cbAssy);
			SetSmartComboBox(cbImpidence);
			SetSmartComboBox(cbOxide);
			SetSmartComboBox(cbUlMark);
			SetSmartComboBox(cbSeparatingportion);

			cbAssy.DataSource = dtYn.Copy();           
            cbImpidence.DataSource = dtYn.Copy();
            cbOxide.DataSource = dtYn.Copy();
            cbUlMark.DataSource = dtYn.Copy();
            cbSeparatingportion.DataSource = dtYn.Copy();

            ///////////////////////////////////////////////////////////////////////////////////

            Dictionary<string, object> ParamLayer = new Dictionary<string, object>();
            ParamLayer.Add("CODECLASSID", "Layer");
            ParamLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtLayer = SqlExecuter.Query("GetTypeList", "10001", ParamLayer);

			SetSmartComboBox(cbLayer);
			SetSmartComboBox(cbAppliedLayer1);
			SetSmartComboBox(cbAppliedLayer2);
			SetSmartComboBox(cbAppliedLayer3);

			cbLayer.DataSource = dtLayer.Copy();
            cbAppliedLayer1.DataSource = dtLayer.Copy();
            cbAppliedLayer2.DataSource = dtLayer.Copy();
            cbAppliedLayer3.DataSource = dtLayer.Copy();

			///////////////////////////////////////////////////////////////////////////////////

			Dictionary<string, object> ParamUserLayer = new Dictionary<string, object>();
			ParamUserLayer.Add("CODECLASSID", "UserLayer");
			ParamUserLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtUserLayer = SqlExecuter.Query("GetTypeList", "10001", ParamUserLayer);

			SetSmartComboBox(cbUseLayer);
			cbUseLayer.DataSource = dtUserLayer.Copy();

			///////////////////////////////////////////////////////////////////////////////////

			Dictionary<string, object> ParamWeekCnt = new Dictionary<string, object>();
            ParamWeekCnt.Add("CODECLASSID", "WeekCount");
            ParamWeekCnt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtWeekCnt = SqlExecuter.Query("GetTypeList", "10001", ParamWeekCnt);
			SetSmartComboBox(cbWeekSeq);
			cbWeekSeq.DataSource = dtWeekCnt.Copy();

			///////////////////////////////////////////////////////////////////////////////////

			Dictionary<string, object> ParamClass = new Dictionary<string, object>();
            ParamClass.Add("CODECLASSID", "ProductSpecDivision");
            ParamClass.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtClass = SqlExecuter.Query("GetTypeList", "10001", ParamClass);

			SetSmartComboBox(cbClass1);
			SetSmartComboBox(cbClass2);
			SetSmartComboBox(cbClass3);

			cbClass1.DataSource = dtClass.Copy();
            cbClass2.DataSource = dtClass.Copy();
            cbClass3.DataSource = dtClass.Copy();
        }

		/// <summary>
		/// Mask 텍스트 박스 초기화
		/// </summary>
		private void InitializeTextControl()
        {
            //TextBoxHelper.SetMarkMask(Mark.PLUSMINUS, tbMinCl);
            //TextBoxHelper.SetMarkMask(Mark.PLUSMINUS, tbMinPcr);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbInputScaleX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbInputScaleY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbArySizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbArySizeY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbPcsSizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbPcsSizeY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbPnlSizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbPnlSizeY);

            TextBoxHelper.SetUnitMask(Unit.PCS, tbMeasurement); 
            TextBoxHelper.SetUnitMask(Unit.PCS, tbReliablity); 
            TextBoxHelper.SetUnitMask(Unit.PCS, tbHazard);
        }

        /// <summary>
        /// ComboBox 초기화 공통
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
        /// 데이터 초기화
        /// </summary>
        public DataTable productspecreturn()
        {
            DataSource = new DataTable();
            DataSource.Columns.Add(new DataColumn("MEASUREMENT", typeof(string))); // 치수측정
            DataSource.Columns.Add(new DataColumn("RELIABLITY", typeof(string))); // 신뢰성
            DataSource.Columns.Add(new DataColumn("HAZARD", typeof(string))); // 유해물질
            DataSource.Columns.Add(new DataColumn("LAYER", typeof(string))); // 층수

            DataRow row = DataSource.NewRow();

            row["MEASUREMENT"] = tbMeasurement.EditValue;
            row["RELIABLITY"] = tbReliablity.EditValue;
            row["HAZARD"] = tbHazard.EditValue;

            DataTable dt = cbLayer.DataSource as DataTable;
            if (cbLayer.EditValue != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Equals(cbLayer.EditValue.ToString()))
                    {
                        row["LAYER"] = dt.Rows[i]["CODENAME"].ToString();
                    }
                }
            }
            

            DataSource.Rows.Add(row);

            return DataSource;
        }
        #region 저장

        public Dictionary<string, object> Save()
        {
			//필수 입력 체크
			List<string> requiredList = new List<string>();
			CommonFunctionProductSpec.GetRequiredValidationList(tlpProductSpec, requiredList);
			CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpProductSpec, requiredList);

			Dictionary<string, object> dataDictionary = new Dictionary<string, object>();
			CommonFunctionProductSpec.GetSaveDataDictionary(tlpProductSpec, dataDictionary);

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

			CommonFunctionProductSpec.SearchDataBind(r, tlpProductSpec);
		}

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData()
        {
            //데이터 초기화
            CommonFunctionProductSpec.ClearData(tlpProductSpec);
        }


        #endregion

        private void smartLabel28_Click(object sender, EventArgs e)
        {

        }
    }
}
