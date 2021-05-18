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

namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 환산금액 팝업 화면 공정별 Claim 기준 금액 
    /// 업  무  설  명  : 공정별 Claim 기준 금액 에서 환산금액 조회 시 팝업
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-07-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedClaimBaseAmountPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        public DataRow CurrentDataRow { get; set; }
        #region Local Variables
        string _sProductdefid = "";
        string _sProductdefversion = "";
        string _sPlantid = "";
        DataTable dtClaimBaseAmount = null;
        #endregion

        #region 생성자
        public OutsourcedClaimBaseAmountPopup()
        {
            InitializeComponent();

            InitializeEvent();

            InitializeCondition();

            _sPlantid = UserInfo.Current.Plant.ToString();

        }
        /// <summary>
        /// 공정별 claim 기준 금액 팝업부분 
        /// </summary>
        /// <param name="SProductdefid"></param>
        /// <param name="SProductdefversion"></param>
        /// <param name="SProductdeaname"></param>
        /// <param name="SPlantid"></param>
        public OutsourcedClaimBaseAmountPopup(string SProductdefid, string SProductdefversion, string SProductdeaname, string SPlantid)
        {
            InitializeComponent();
            usrProductdefid.Enabled = true;
            usrProductdefid.CODE.Enabled = true;
            usrProductdefid.CODE.ReadOnly = true;
            usrProductdefid.CODE.Properties.ReadOnly = true;
            // 화폐단위 cbobox 
            InitializeComboBox();

            InitializeEvent();

            InitializeCondition();

            InitializeGrid();

            _sProductdefid = SProductdefid;
            _sProductdefversion = SProductdefversion;
            usrProductdefid.CODE.EditValue = SProductdefid;
            usrProductdefid.VERSION.EditValue = SProductdefversion;
            usrProductdefid.NAME.EditValue = SProductdeaname;
            _sPlantid = SPlantid;
            //기초 자료 가져오기 (공정별 claim 금액기준)
            SearchClaimBaseAmount();

        }

        #endregion
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 화폐단위 정의
            cboCurrencyunit.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboCurrencyunit.ValueMember = "UOMDEFID";
            cboCurrencyunit.DisplayMember = "UOMDEFNAME";
            cboCurrencyunit.EditValue = "KRW";

            cboCurrencyunit.DataSource = SqlExecuter.Query("GetUomDefinitionListByOsp", "10001"
             , new Dictionary<string, object>() { { "UOMTYPE", "Currency" } });

            cboCurrencyunit.ShowHeader = false;
            // 화폐단위 정의
            cboCurrencyunitTo.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboCurrencyunitTo.ValueMember = "UOMDEFID";
            cboCurrencyunitTo.DisplayMember = "UOMDEFNAME";
            cboCurrencyunitTo.EditValue = "KRW";

            cboCurrencyunitTo.DataSource = SqlExecuter.Query("GetUomDefinitionListByOsp", "10001"
             , new Dictionary<string, object>() { { "UOMTYPE", "Currency" } });

            cboCurrencyunitTo.ShowHeader = false;
        }

        #endregion
        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            // 계획년월
            DateTime dateNow = DateTime.Now;
            dtpStdDate.EditValue = dateNow.ToString("yyyy-MM");

        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            // 검색
            btnSearch.Click += BtnSearch_Click;
            // 닫기
            btnClose.Click += BtnClose_Click;


        }

        /// <summary>
        /// 닫기 클릭시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        /// <summary>
        /// 조회 클릭 - 메인 grid에 체크 데이터 전달
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (cboCurrencyunit.Text.ToString().Equals(""))
            {
                return;
            }
                // 동일 환율시 공정별 claim 금액 기준값 처리 
            if (cboCurrencyunit.EditValue.ToString().Equals(cboCurrencyunitTo.EditValue.ToString()))
            {
                txtExchangevalue.EditValue = 1;
                //공정별 Claim 금액기준 목록 계획 환율 적용 처리  
                calGridExchangValue(dtClaimBaseAmount, 1);
                return;
            }
            SearchExchangeValue();
        }

        /// <summary>
        /// 계획 환율정보 가져오기 .
        /// </summary>
        private void SearchExchangeValue()
        {
            
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_CURRENCYUNIT_FR", cboCurrencyunit.EditValue);
            Param.Add("P_CURRENCYUNIT_TO", cboCurrencyunitTo.EditValue);
            Param.Add("P_EXCHANGEDATE", dtpStdDate.EditValue);
            Param.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
            Param.Add("P_PLANTID", _sPlantid);

            DataTable dtExchange = SqlExecuter.Query("GetOutsourcedClaimExchangeValueByOsp", "10001", Param);
            decimal decExchangevalue = 0;
            if (dtExchange.Rows.Count > 0)
            {
                DataRow row = dtExchange.Rows[0];
                string sExchangevalue = row["EXCHANGEVALUE"].ToString();
                       decExchangevalue = (sExchangevalue.ToString().Equals("") ? 0 : Convert.ToDecimal(sExchangevalue));
                txtExchangevalue.EditValue = decExchangevalue;
            }
            else
            {
                txtExchangevalue.EditValue = 0;
            }
            //공정별 Claim 금액기준 목록 계획 환율 적용 처리  
            calGridExchangValue(dtClaimBaseAmount, decExchangevalue);
        }

        /// <summary>
        /// 공정별 claim 금액 기준 가져오기 
        /// </summary>
        private void SearchClaimBaseAmount()
        {
         
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("P_PRODUCTDEFID", _sProductdefid);
            Param.Add("P_PRODUCTDEFVERSION", _sProductdefversion);
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            Param = Commons.CommonFunction.ConvertParameter(Param);
            dtClaimBaseAmount = SqlExecuter.Query("GetOutsourcedClaimBaseAmount", "10001", Param);

            if (dtClaimBaseAmount.Rows.Count > 0)
            {
                DataRow row = dtClaimBaseAmount.Rows[0];
                string sCurrencyunit = row["CURRENCYUNIT"].ToString();
                cboCurrencyunit.EditValue = sCurrencyunit;
                cboCurrencyunitTo.EditValue = sCurrencyunit;
                txtExchangevalue.EditValue = 1;
            }

            //공정별 Claim 금액기준 목록 계획 환율 적용 처리  
            calGridExchangValue(dtClaimBaseAmount, 1);

        }

        /// <summary>
        /// 계획 환율값 그리등에 반영하기.
        /// </summary>
        /// <param name="dtClaimBase"></param> 
        /// <param name="decexchangevalue"></param>
        private void calGridExchangValue(DataTable dtClaimBase,decimal decexchangevalue)
        {
            grdClaimBaseAmountPopup.View.ClearDatas();
            //작업용 임시 테이블 생성. 
            DataTable dtCalWork = dtClaimBase.Clone();
            // 1.제조비용 합계 구하기
            for (int i = 0; i < dtClaimBase.Rows.Count; i++)
            {
                DataRow Trow = dtClaimBase.Rows[i];
                dtCalWork.ImportRow(Trow);
                DataRow row = dtCalWork.Rows[i];
                string strVariablecost = row["VARIABLECOST"].ToString();
                decimal decVariablecost = (strVariablecost.ToString().Equals("") ? 0 : Convert.ToDecimal(strVariablecost)); //
                row["VARIABLECOST"] = Math.Round(decVariablecost / decexchangevalue,0);

                string strFixedcost = row["FIXEDCOST"].ToString();
                decimal decFixedcost = (strFixedcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strFixedcost)); //
                row["FIXEDCOST"] = Math.Round(decFixedcost / decexchangevalue, 0);
                string strMaterialcost = row["MATERIALCOST"].ToString();
                decimal decMaterialcost = (strMaterialcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMaterialcost)); //
                row["MATERIALCOST"] = Math.Round(decMaterialcost / decexchangevalue, 0);
                string strMaterialprocesscost = row["MATERIALPROCESSCOST"].ToString();
                decimal decMaterialprocesscost = (strMaterialprocesscost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMaterialprocesscost)); //
                row["MATERIALPROCESSCOST"] = Math.Round(decMaterialprocesscost / decexchangevalue, 0);
                string strBoxcost = row["BOXCOST"].ToString();
                decimal decBoxcost = (strBoxcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strBoxcost)); //
                row["BOXCOST"] = Math.Round(decBoxcost / decexchangevalue, 0);
                string strMoldcost = row["MOLDCOST"].ToString();
                decimal decMoldcost = (strMoldcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMoldcost)); //
                row["MOLDCOST"] = Math.Round(decMoldcost / decexchangevalue, 0);
                string strSamplecost = row["SAMPLECOST"].ToString();
                decimal decSamplecost = (strSamplecost.ToString().Equals("") ? 0 : Convert.ToDecimal(strSamplecost)); //
                row["SAMPLECOST"] = Math.Round(decSamplecost / decexchangevalue, 0);
                string strManufacturingoverhead = row["MANUFACTURINGOVERHEAD"].ToString();
                decimal decManufacturingoverhead = (strManufacturingoverhead.ToString().Equals("") ? 0 : Convert.ToDecimal(strManufacturingoverhead)); //
                row["MANUFACTURINGOVERHEAD"] = Math.Round(decManufacturingoverhead / decexchangevalue, 0);
                string strGeneralcost = row["GENERALCOST"].ToString();
                decimal decGeneralcost = (strGeneralcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strGeneralcost)); //
                row["GENERALCOST"] = Math.Round(decGeneralcost / decexchangevalue, 0);
                string strEtccost = row["ETCCOST"].ToString();
                decimal decEtccost = (strEtccost.ToString().Equals("") ? 0 : Convert.ToDecimal(strEtccost)); //
                row["ETCCOST"] = Math.Round(decEtccost / decexchangevalue, 0);
                
               
                string strDefectamount = row["DEFECTAMOUNT"].ToString();
                decimal decDefectamount = (strDefectamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strDefectamount)); //
                row["DEFECTAMOUNT"] = Math.Round(decDefectamount / decexchangevalue, 0);
                string strAmount = row["AMOUNT"].ToString();
                decimal decAmount = (strAmount.ToString().Equals("") ? 0 : Convert.ToDecimal(strAmount)); //
                row["AMOUNT"] = Math.Round(decAmount / decexchangevalue, 0);
               
                string strCumulativeamount = row["CUMULATIVEAMOUNT"].ToString();
                decimal decCumulativeamount = (strCumulativeamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strCumulativeamount)); //
                row["CUMULATIVEAMOUNT"] = Math.Round(decCumulativeamount / decexchangevalue, 0);
                string strPcsamount = row["PCSAMOUNT"].ToString();
                decimal decPcsamount = (strPcsamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strPcsamount)); //
                row["PCSAMOUNT"] = Math.Round(decPcsamount / decexchangevalue, 0); 
                string strcumulativepcsamount = row["CUMULATIVEPCSAMOUNT"].ToString();
                decimal deccumulativepcsamount = (strcumulativepcsamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strcumulativepcsamount)); //
                row["CUMULATIVEPCSAMOUNT"] = Math.Round(deccumulativepcsamount / decexchangevalue, 0); 
            }

            grdClaimBaseAmountPopup.DataSource = dtCalWork;
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

        #endregion

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdClaimBaseAmountPopup.GridButtonItem = GridButtonItem.Export;
            grdClaimBaseAmountPopup.View.SetIsReadOnly();
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                          //  회사 ID
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                           //  공장 ID
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("CURRENCYUNIT", 120)
                .SetIsHidden();                                                             //  화폐단위
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("DEFECTAMOUNTTOT", 120)
                .SetIsHidden();                                                              //전체불량반영금액
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsHidden();//제품정의 ID
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsHidden();//제품정의 VER
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("PROCESSPATHID", 120)
                .SetIsHidden();//순서
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("PATHSEQUENCE", 120)
                .SetIsReadOnly();//PATHSEQUENCE
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetIsReadOnly();//공정명

            grdClaimBaseAmountPopup.View.AddSpinEditColumn("VARIABLECOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//변동비용
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("FIXEDCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//고정비용
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("MATERIALCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//자재비용
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("MATERIALPROCESSCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //자재가공비용
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("BOXCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //BOX비용
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("MOLDCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//금형비용
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("SAMPLECOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//샘플비용
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("MANUFACTURINGOVERHEAD", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//제조간접비
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("GENERALCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//일반관리비
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("ETCCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//기타비용
          
            grdClaimBaseAmountPopup.View.AddSpinEditColumn("DEFECTAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //불량반영금액
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("AMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //견적금액
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("CUMULATIVEAMOUNT", 120)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);                          //견적누적금액
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("CUMULATIVERATE", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###.##", MaskTypes.Numeric);                          //견적누적비율
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("PCSAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //PCS견적금액
           
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("CUMULATIVEPCSAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);                         //PCS누적금액
            grdClaimBaseAmountPopup.View.AddTextBoxColumn("DESCRIPTION", 200);//설명

           // grdClaimBaseAmountPopup.View.SetAutoFillColumn("DESCRIPTION");
            grdClaimBaseAmountPopup.View.PopulateColumns();

        }
    }
}
