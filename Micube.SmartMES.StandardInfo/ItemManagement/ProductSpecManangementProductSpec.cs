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
	/// <summary> 
	/// 프 로 그 램 명  : 기준정보 > 품목관리 > 품목사양정보
	/// 업  무  설  명  : 픔목 스펙 저장
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-12-12
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ProductSpecManangementProductSpec : UserControl
    {
        #region 생성자
		public ProductSpecManangementProductSpec()
        {
            InitializeComponent();

            InitializeComboControl();
            InitializeTextControl();
        }
		#endregion

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
			SetSmartComboBox(cbElogation);
			SetSmartComboBox(cbImpidence);
			SetSmartComboBox(cbOxide);
			SetSmartComboBox(cbUlMark);

			cbAssy.DataSource = dtYn.Copy();
			cbElogation.DataSource = dtYn.Copy();
			cbImpidence.DataSource = dtYn.Copy();
			cbOxide.DataSource = dtYn.Copy();
			cbUlMark.DataSource = dtYn.Copy();

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

			Dictionary<string, object> ParamWeekCnt = new Dictionary<string, object>();
			ParamWeekCnt.Add("CODECLASSID", "WeekCount");
			ParamWeekCnt.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtWeekCnt = SqlExecuter.Query("GetTypeList", "10001", ParamWeekCnt);

			SetSmartComboBox(cbWeekSeq);
			cbWeekSeq.DataSource = dtWeekCnt.Copy();

			///////////////////////////////////////////////////////////////////////////////////

			Dictionary<string, object> ParamInputSize = new Dictionary<string, object>();
			ParamInputSize.Add("CODECLASSID", "InputSize");
			ParamInputSize.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			DataTable dtInputSize = SqlExecuter.Query("GetTypeList", "10001", ParamInputSize);

			SetSmartComboBox(cbInputSizeX);
			cbInputSizeX.DataSource = dtInputSize.Copy();

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
            TextBoxHelper.SetMarkMask(Mark.PLUSMINUS, tbMinCl);
            TextBoxHelper.SetMarkMask(Mark.PLUSMINUS, tbMinPsr);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbInputScaleX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbInputScaleY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbArySizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbArySizeY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbPcsSizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbPcsSizeY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbPnlSizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbPnlSizeY);
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

		/*
        private void InitializePopupControl()
        {
            ConditionItemSelectPopup layerSelectPopup = new ConditionItemSelectPopup();
            layerSelectPopup.Id = "SELECTSPECOWNER";
            layerSelectPopup.SearchQuery = new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            layerSelectPopup.ValueFieldName = "USERID";
            layerSelectPopup.DisplayFieldName = "USERNAME";
            layerSelectPopup.SetPopupLayout("SELECTSPECOWNER", PopupButtonStyles.Ok_Cancel, true, true);
            layerSelectPopup.SetPopupResultCount(0);
            layerSelectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
            layerSelectPopup.SetPopupAutoFillColumns("DEPARTMENT");

            layerSelectPopup.GridColumns.AddTextBoxColumn("USERID", 150);
            layerSelectPopup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            layerSelectPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 60);
        }
		*/

		#endregion

		#region event

		#endregion

		#region 저장

		/// <summary>
		/// 
		/// </summary>
		public Dictionary<string, object> Save()
        {
			//필수 입력 체크
			List<string> requiredList = new List<string>();
			CommonFunctionProductSpec.GetRequiredValidationList(tlpProductSpec, requiredList);
			CommonFunctionProductSpec.RequiredListNullOrEmptyCheck(tlpProductSpec, requiredList, smartGroupBox1.LanguageKey);

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

    }
}
