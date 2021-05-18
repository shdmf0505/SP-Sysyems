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
    public partial class ProductSpecificationProductSpec2 : UserControl
    {
        DataTable DataSource = new DataTable();
        #region 생성자
        public ProductSpecificationProductSpec2()
        {
            InitializeComponent();

            InitializeComboControl();
            InitializeTextControl();
			InitializeEvent();

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
            SetSmartComboBox(cbOxide);
            SetSmartComboBox(cbUlMark);
            SetSmartComboBox(cbSeparatingportion);
            cbSeparatingportion.DataSource = dtYn.Copy();
            cbAssy.DataSource = dtYn.Copy();
            cbOxide.DataSource = dtYn.Copy();
            cbUlMark.DataSource = dtYn.Copy();
            cbAssy.EditValue = "N";
            cbUlMark.EditValue = "N";
            cbUlMark.EditValue = "N";
            ///////////////////////////////////////////////////////////////////////////////////

            Dictionary<string, object> ParamLayer = new Dictionary<string, object>();
            ParamLayer.Add("CODECLASSID", "Layer");
            ParamLayer.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtLayer = SqlExecuter.Query("GetTypeList", "10001", ParamLayer);

            SetSmartComboBox(cbLayer);


            cbLayer.DataSource = dtLayer.Copy();

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


            Dictionary<string, object> ParamParking = new Dictionary<string, object>();
            ParamParking.Add("CODECLASSID", "ParkingMng");
            ParamParking.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtParking = SqlExecuter.Query("GetTypeList", "10001", ParamParking);

            SetSmartComboBox(cbWeekSeq);
            cbWeekSeq.DataSource = dtParking.Copy();



        }

        /// <summary>
        /// Mask 텍스트 박스 초기화
        /// </summary>
        private void InitializeTextControl()
        {
         
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbArySizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbArySizeY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbPcsSizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbPcsSizeY);
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbPnlSizeX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbPnlSizeY);
            TextBoxHelper.SetUnitMask(Unit.percentWithminus, tbxoutpercent);
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

        private void InitializeEvent()
        {
            tbXout.EditValueChanged += TbXout_EditValueChanged;
            tbPcsArray.EditValueChanged += TbXout_EditValueChanged;


        }

        private void TbXout_EditValueChanged(object sender, EventArgs e)
        {

 
            if (tbXout.EditValue != null && tbPcsArray.EditValue != null && !tbXout.EditValue.Equals("")
                && !tbPcsArray.EditValue.Equals(""))
            {
                string strValue1 = tbPcsArray.EditValue.ToString().Trim();
                string strValue2 = tbXout.EditValue.ToString().Trim();

                if (strValue2.Equals("-"))
                {
                    strValue2 = "-1";
                }
                if (strValue1.Equals("-"))
                {
                    strValue1 = "-1";
                }
                tbxoutpercent.EditValue = Convert.ToInt64(Convert.ToDouble(strValue2) / Convert.ToDouble(strValue1));

            }
            else
            {
                tbxoutpercent.EditValue = "";

            }

            


        }




        
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


        public DataTable productspecreturn()
        {
            DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(tlpProductSpec, DataSource);


            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, tlpProductSpec);
            DataSource.Rows.Add(row);
            return DataSource;
        }
        #endregion

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
            if (i == 1)
            {
                DisplayComboChange(cbLayer);
                DisplayComboChange(cbWeekSeq);
                DisplayComboChange(cbInputSizeX);
            }
            else
            {
                DisplayComboChange2(cbLayer);
                DisplayComboChange2(cbWeekSeq);
                DisplayComboChange2(cbInputSizeX);

            }
        }

 


        private void smartGroupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void smartLabel25_Click(object sender, EventArgs e)
        {

        }

        private void tlpProductSpec_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
