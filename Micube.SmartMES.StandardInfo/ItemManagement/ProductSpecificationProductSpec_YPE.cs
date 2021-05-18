using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

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
    public partial class ProductSpecificationProductSpec_YPE : UserControl
    {
        #region 생성자

        public ProductSpecificationProductSpec_YPE()
        {
            InitializeComponent();
            InitializeComboControl();
            InitializeTextControl();
            InitializeEvent();
        }

        #endregion 생성자

        #region 컨트롤 초기화

        /// <summary>
        /// Combo 컨트롤 초기화
        /// </summary>
        private void InitializeComboControl()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CODECLASSID", "YesNo" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            DataTable dtYn = SqlExecuter.Query("GetTypeList", "10001", param);

            SetSmartComboBox(cbAssy);
            SetSmartComboBox(cbOxide);
            SetSmartComboBox(cbUlMark);
            SetSmartComboBox(cbSeparatingportion);
            cbAssy.DataSource = dtYn.Copy();
            cbOxide.DataSource = dtYn.Copy();
            cbUlMark.DataSource = dtYn.Copy();
            cbSeparatingportion.DataSource = dtYn.Copy();

            param["CODECLASSID"] = "UserLayer";
            DataTable dtLayer = SqlExecuter.Query("GetTypeList", "10001", param);
            SetSmartComboBox(cbUseLayer);
            cbUseLayer.DataSource = dtLayer.Copy();

            param["CODECLASSID"] = "WeekCount";
            DataTable dtWeekCnt = SqlExecuter.Query("GetTypeList", "10001", param);

            SetSmartComboBox(cbWeekSeq);
            cbWeekSeq.DataSource = dtWeekCnt.Copy();
        }

        /// <summary>
        /// Mask 텍스트 박스 초기화
        /// </summary>
        private void InitializeTextControl()
        {
            TextBoxHelper.SetMarkMask(Mark.XAxisDecimalPoint, tbInputScaleX);
            TextBoxHelper.SetMarkMask(Mark.YAxisDecimalPoint, tbInputScaleY);
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

        #endregion 컨트롤 초기화

        #region event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //smartSpinEditPcsPnl.EditValueChanged += SmartSpinEditPcsPnl_EditValueChanged;
            //tbPnlSizeX.EditValueChanged += TbPnlSizeX_EditValueChanged;
            //tbPnlSizeY.EditValueChanged += TbPnlSizeY_EditValueChanged;
        }

        private void TbPnlSizeY_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Format.GetString(smartSpinEditPcsPnl.EditValue)) &&
                !string.IsNullOrEmpty(Format.GetString(tbPnlSizeX.EditValue)) && !string.IsNullOrEmpty(Format.GetString(tbPnlSizeY.EditValue)))
            {
                if (!tbPnlSizeX.EditValue.Equals("X : ") && !tbPnlSizeY.EditValue.Equals("Y : "))
                {
                    string strValue1 = Format.GetString(smartSpinEditPcsPnl.EditValue);
                    string strx = Format.GetString(tbPnlSizeX.EditValue);
                    string stry = Format.GetString(tbPnlSizeY.EditValue);

                    if (strValue1.Equals("-"))
                    {
                        strValue1 = "-1";
                    }

                    strx = strx.Replace("X : ", "");
                    stry = stry.Replace("Y : ", "");

                    if (strx.Equals("0") || stry.Equals("0") || strValue1.Equals("0"))
                    {
                        smartTextBoxPcsmm.EditValue = 0;
                        return;
                    }

                    smartTextBoxPcsmm.EditValue = Convert.ToInt32(Convert.ToDouble(strValue1) / (Convert.ToDouble(strx) / 1000) / (Convert.ToDouble(stry) / 1000));
                }
            }
            else
            {
                smartTextBoxPcsmm.EditValue = 0;
            }
        }

        private void TbPnlSizeX_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Format.GetString(smartSpinEditPcsPnl.EditValue)) &&
                !string.IsNullOrEmpty(Format.GetString(tbPnlSizeX.EditValue)) && !string.IsNullOrEmpty(Format.GetString(tbPnlSizeY.EditValue)))
            {
                if (!tbPnlSizeX.EditValue.Equals("X : ") && !tbPnlSizeY.EditValue.Equals("Y : "))
                {
                    string strValue1 = Format.GetString(smartSpinEditPcsPnl.EditValue);
                    string strx = Format.GetString(tbPnlSizeX.EditValue);
                    string stry = Format.GetString(tbPnlSizeY.EditValue);

                    if (strValue1.Equals("-"))
                    {
                        strValue1 = "-1";
                    }

                    strx = strx.Replace("X : ", "");
                    stry = stry.Replace("Y : ", "");

                    if (strx.Equals("0") || stry.Equals("0") || strValue1.Equals("0"))
                    {
                        smartTextBoxPcsmm.EditValue = 0;
                        return;
                    }

                    smartTextBoxPcsmm.EditValue = Convert.ToInt32(Convert.ToDouble(strValue1) / (Convert.ToDouble(strx) / 1000) / (Convert.ToDouble(stry) / 1000));
                }
            }
            else
            {
                smartTextBoxPcsmm.EditValue = 0;
            }
        }

        private void SmartSpinEditPcsPnl_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Format.GetString(smartSpinEditPcsPnl.EditValue)) &&
                !string.IsNullOrEmpty(Format.GetString(tbPnlSizeX.EditValue)) && !string.IsNullOrEmpty(Format.GetString(tbPnlSizeY.EditValue)))
            {
                if (!tbPnlSizeX.EditValue.Equals("X :") && !tbPnlSizeY.EditValue.Equals("Y :"))
                {
                    string strValue1 = Format.GetString(smartSpinEditPcsPnl.EditValue);
                    string strx = Format.GetString(tbPnlSizeX.EditValue);
                    string stry = Format.GetString(tbPnlSizeY.EditValue);

                    if (strValue1.Equals("-"))
                    {
                        strValue1 = "-1";
                    }

                    strx = strx.Replace("X : ", "");
                    stry = stry.Replace("Y : ", "");

                    if (strx.Equals("0") || stry.Equals("0") || strValue1.Equals("0"))
                    {
                        smartTextBoxPcsmm.EditValue = 0;
                        return;
                    }

                    smartTextBoxPcsmm.EditValue = Convert.ToInt32(Convert.ToDouble(strValue1) / (Convert.ToDouble(strx) / 1000) / (Convert.ToDouble(stry) / 1000));
                }
            }
            else
            {
                smartTextBoxPcsmm.EditValue = 0;
            }
        }

        #endregion event

        #region public Function

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

            if (Format.GetDouble(dataDictionary["PNLSIZEXAXIS"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("PNLSIZE"));
            }

            if (Format.GetDouble(dataDictionary["PNLSIZEYAXIS"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("PNLSIZE"));
            }

            if (Format.GetDouble(dataDictionary["PCSPNL"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("ARRAYPCSPNL"));
            }

            if (Format.GetDouble(dataDictionary["PCSMM"], 0).Equals(0))
            {
                throw MessageException.Create("QtyInputZero", Language.Get("CALCULATIONPCSM"));
            }

            return dataDictionary;
        }

        public DataTable Productspecreturn()
        {
            DataTable DataSource = new DataTable();
            ReportTableReturn.GetLabelDataTable(tlpProductSpec, DataSource);

            DataRow row = DataSource.NewRow();
            ReportTableReturn.GetDataRow(row, tlpProductSpec);
            DataSource.Rows.Add(row);
            return DataSource;
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

            CommonFunctionProductSpec.SearchDataBind(dt.Rows[0], tlpProductSpec);
        }

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public void ClearData() => CommonFunctionProductSpec.ClearData(tlpProductSpec);

        #endregion public Function
    }
}