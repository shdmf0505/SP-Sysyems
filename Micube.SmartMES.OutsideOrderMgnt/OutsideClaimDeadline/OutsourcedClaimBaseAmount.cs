#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.OutsideOrderMgnt.Popup;
#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리 > 외주 Claim 마감 >공정별 Claim 기준 금액
    /// 업  무  설  명  :  공정별 Claim 기준 금액 등록한다..
    /// 생    성    자  : choisstar
    /// 생    성    일  : 2019-06-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcedClaimBaseAmount : SmartConditionManualBaseForm
    {
        #region Local Variables

        private string _strPlantid = ""; // plant 변경시 작업 

        #endregion

        #region 생성자

        public OutsourcedClaimBaseAmount()
        {
            InitializeComponent();

            usrProductdefid.CODE.Enabled = true;
            usrProductdefid.CODE.ReadOnly = true;
            usrProductdefid.CODE.Properties.ReadOnly = true;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeComboBox();

            InitializeEvent();
            // 작업용 plant 셋팅 (조회시 다시 셋팅)
            _strPlantid = UserInfo.Current.Plant.ToString();
            
          
            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
           
            
        }
        #region ComboBox  

        /// <summary>
        /// SmartComboBox 컨트롤 추가
        /// </summary>
        private void InitializeComboBox()
        {
            // 작업구분값 정의 
            cboCurrencyunit.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboCurrencyunit.ValueMember = "UOMDEFID";
            cboCurrencyunit.DisplayMember = "UOMDEFNAME";
            cboCurrencyunit.EditValue = "KRW";

            cboCurrencyunit.DataSource = SqlExecuter.Query("GetUomDefinitionListByOsp", "10001"
             , new Dictionary<string, object>() {  { "UOMTYPE", "Currency" } });

            cboCurrencyunit.ShowHeader = false;
           
        }

        #endregion

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOutsourcedClaimBaseAmount.GridButtonItem = GridButtonItem.Export;
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();                                                          //  회사 ID
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("ENTERPRISEID", 120)
                .SetIsHidden();                                                           //  공장 ID
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("CURRENCYUNIT", 120)
                .SetIsHidden();                                                             //  화폐단위
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("DEFECTAMOUNTTOT", 120)
                .SetIsHidden();                                                              //전체불량반영금액
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsHidden();//제품정의 ID
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsHidden();//제품정의 VER
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PROCESSDEFID", 120)
                .SetIsHidden();//제품정의 ID
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PROCESSDEFVERSION", 80)
                .SetIsHidden();//제품정의 VER
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PROCESSSEGMENTID", 200)
                .SetIsHidden();//공정명
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200)
                .SetIsHidden();
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PATHSEQUENCE", 120)
                .SetIsReadOnly();//PATHSEQUENCE
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetIsReadOnly();//공정명

            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("VARIABLECOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//변동비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("FIXEDCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###",  MaskTypes.Numeric);//고정비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("MATERIALCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//자재비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("MATERIALPROCESSCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //자재가공비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("BOXCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //BOX비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("MOLDCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//금형비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("SAMPLECOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//샘플비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("MANUFACTURINGOVERHEAD", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//제조간접비
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("GENERALCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//일반관리비
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("ETCCOST", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);//기타비용
            grdOutsourcedClaimBaseAmount.View.AddSpinEditColumn("DEFECTAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //불량반영금액
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("AMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric); //견적금액
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("CUMULATIVEAMOUNT", 120)
                .SetIsReadOnly().SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###", MaskTypes.Numeric);                          //견적누적금액
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("CUMULATIVERATE", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###.##", MaskTypes.Numeric);                          //견적누적비율
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("PCSAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###.##", MaskTypes.Numeric); //PCS견적금액
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("CUMULATIVEPCSAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,###.##", MaskTypes.Numeric);                         //PCS누적금액
            grdOutsourcedClaimBaseAmount.View.AddTextBoxColumn("DESCRIPTION", 200);//설명

           // grdOutsourcedClaimBaseAmount.View.SetAutoFillColumn("DESCRIPTION");
            grdOutsourcedClaimBaseAmount.View.PopulateColumns();
            
        }

        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            var plantCbobox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
              .SetLabel("PLANT")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.1)
              
              .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
              .SetIsReadOnly(true);
        }
     
        /// <summary>
        /// Product 설정 
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
        
            var popupProduct = Conditions.AddSelectPopup("p_productcode",
                                                               new SqlQuery("GetProductdefidlistByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PRODUCTDEFNAME", "PRODUCTDEFCODE")
                .SetPopupLayout("PRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(650, 600)
                .SetLabel("PRODUCTDEFID")
                .SetPopupResultCount(1)
                .SetPosition(2.3)
                .SetValidationIsRequired();

            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");

            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");

            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

        }
        
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            btnCal.Click += BtnCal_Click;
            btnExchang.Click += BtnExchang_Click;
            btnCopy.Click += BtnCopy_Click;
            grdOutsourcedClaimBaseAmount.View.AddingNewRow += View_AddingNewRow;
            grdOutsourcedClaimBaseAmount.View.CellValueChanged += View_CellValueChanged;
            grdOutsourcedClaimBaseAmount.View.KeyDown += View_KeyDown;
        }
        /// <summary>
        /// 복사하기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            //품목코드 확인 
            string Productdefid = usrProductdefid.CODE.Text.ToString();
            string ProductdefidVersion = usrProductdefid.VERSION.Text.ToString();
            string ProductdefidTo = usrProductdefidTO.CODE.Text.ToString();
            string ProductdefidVersionTo = usrProductdefidTO.VERSION.Text.ToString();
            string strProductcode = "";
            strProductcode = usrProductdefid.txtProdcutCode.Text.ToString();
            if (Productdefid.Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProductdefid.Text); //메세지 
                usrProductdefid.Focus();
                return;
            }
            if (ProductdefidTo.Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProductdefidTO.Text); //메세지 
                usrProductdefid.Focus();
                return;
            }
            if (Productdefid.Equals(ProductdefidTo) && ProductdefidVersion.Equals(ProductdefidVersionTo))
            {
                //다국어 메세지 처리 
                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProductdefidTO.Text); //메세지 
                usrProductdefid.Focus();
                return;
            }

            // 건수 재비교 처리해야함.
            DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", btnCopy.Text);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnCopy.Enabled = false;

                    string strProductcodeto = "";
                    strProductcodeto = usrProductdefidTO.txtProdcutCode.Text.ToString();
                    string[] sproductd = strProductcode.Split('|');
                    string[] sproductdto = strProductcodeto.Split('|');
                    //테이블 생성 
                    DataTable dtCopy = createSaveDatatable();
                    DataRow drCopy = dtCopy.NewRow();
                    drCopy["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                    
                    drCopy["PRODUCTDEFID"] = usrProductdefid.CODE.Text.ToString();
                    drCopy["PRODUCTDEFVERSION"] = usrProductdefid.VERSION.Text.ToString();
                    drCopy["PROCESSDEFID"] = sproductd[2].ToString();
                    drCopy["PROCESSDEFVERSION"] = sproductd[3].ToString();
                    drCopy["TOPRODUCTDEFID"] = usrProductdefidTO.CODE.Text.ToString();
                    drCopy["TOPRODUCTDEFVERSION"] = usrProductdefidTO.VERSION.Text.ToString();

                    drCopy["TOPROCESSDEFID"] = sproductdto[2].ToString();
                    drCopy["TOPROCESSDEFVERSION"] = sproductdto[3].ToString();
                    dtCopy.Rows.Add(drCopy);
                    this.ExecuteRule("OutsourcedClaimBaseAmountCopy", dtCopy);
                    ShowMessage("SuccessOspProcess");
                    strProductcode = usrProductdefidTO.txtProdcutCode.Text.ToString();
                    Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTCODE").SetValue(strProductcode);
                    Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTCODE").Text = usrProductdefidTO.NAME.Text.ToString();

                    OnCopySearch();
                    usrProductdefidTO.CODE.EditValue = "";
                    usrProductdefidTO.VERSION.EditValue = "";
                    usrProductdefidTO.NAME.EditValue = "";

                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();

                    btnCopy.Enabled = true;
                 
                }
            }

        }
        /// <summary>
        /// 환산금액 조회.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExchang_Click(object sender, EventArgs e)
        {
            //품목코드 확인 
            if (usrProductdefid.CODE.Text.ToString().Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProductdefid.Text); //메세지 
                usrProductdefid.Focus();
                return;
            }
           
            string sProductdefid = usrProductdefid.CODE.EditValue.ToString();
            string sProductdefversion = usrProductdefid.VERSION.EditValue.ToString(); 
            string sProductdefname = usrProductdefid.NAME.EditValue.ToString();
            // 환산금액 조회창 호출 부분 
            OutsourcedClaimBaseAmountPopup itemPopup = new OutsourcedClaimBaseAmountPopup(sProductdefid, sProductdefversion, sProductdefname, _strPlantid);
            itemPopup.ShowDialog(this);
        }
        /// <summary>
        /// 불량 반영금액 계산 부분 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCal_Click(object sender, EventArgs e)
        {
           
            decimal decAmount = 0; // 견적계산 
            decimal decPcsamount = 0; //pcs 견적금액
            string strTotecdefectAmount = txtDefectamount.Text.ToString();
            decimal dectotecdefectAmount = (strTotecdefectAmount.ToString().Equals("") ? 0 : Convert.ToDecimal(strTotecdefectAmount)); //총 불량 반영금액
            string strPcsmm = txtPcsmm.Text.ToString();
            decimal decPcsmm = (strPcsmm.ToString().Equals("") ? 0 : Convert.ToDecimal(strPcsmm)); //MM당PCS수량
            //
            //불량금액 n분하기 
            int intRowCount = grdOutsourcedClaimBaseAmount.View.DataRowCount;
            //
            if (intRowCount == 0) return;
            decimal dectotecdefectAmountDiv = 0;
            decimal dectotecdefectAmountDivsub = 0;
            if (dectotecdefectAmount > 0)
            {
                dectotecdefectAmountDiv = Math.Truncate(dectotecdefectAmount / intRowCount);
                dectotecdefectAmountDivsub = dectotecdefectAmount - (dectotecdefectAmountDiv * intRowCount);
            }
            decimal decAmountSum = 0;// 견적가 누계 
            decimal decCumulativepcsamount = 0;//pcs금액 누계 
                                               // 1.제조비용 합계 구하기
            DataTable dtAmount = grdOutsourcedClaimBaseAmount.DataSource as DataTable;
            dtAmount.DefaultView.Sort = "PATHSEQUENCE ASC";
            for (int i = 0; i < dtAmount.Rows.Count; i++)
            {
                DataRow row = dtAmount.Rows[i];
               
                row["ENTERPRISEID"] = UserInfo.Current.Enterprise.ToString();
                row["PLANTID"] = _strPlantid;
                row["CURRENCYUNIT"] = cboCurrencyunit.EditValue.ToString();
                string strDefectamounttot = txtDefectamount.Text.ToString();
                decimal decDefectamounttot = (strDefectamounttot.ToString().Equals("") ? 0 : Convert.ToDecimal(strDefectamounttot)); //
                row["DEFECTAMOUNTTOT"] = decDefectamounttot;
                string strVariablecost = row["VARIABLECOST"].ToString();
                decimal decVariablecost = (strVariablecost.ToString().Equals("") ? 0 : Convert.ToDecimal(strVariablecost)); //
                row["VARIABLECOST"] = decVariablecost;

                string strFixedcost = row["FIXEDCOST"].ToString();
                decimal decFixedcost = (strFixedcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strFixedcost)); //
                row["FIXEDCOST"] = decFixedcost;
                string strMaterialcost = row["MATERIALCOST"].ToString();
                decimal decMaterialcost = (strMaterialcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMaterialcost)); //
                row["MATERIALCOST"] = decMaterialcost;
                string strMaterialprocesscost = row["MATERIALPROCESSCOST"].ToString();
                decimal decMaterialprocesscost = (strMaterialprocesscost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMaterialprocesscost)); //
                row["MATERIALPROCESSCOST"] = decMaterialprocesscost;
                string strBoxcost = row["BOXCOST"].ToString();
                decimal decBoxcost = (strBoxcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strBoxcost)); //
                row["BOXCOST"] = decBoxcost;
                string strMoldcost = row["MOLDCOST"].ToString();
                decimal decMoldcost = (strMoldcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMoldcost)); //
                row["MOLDCOST"] = decMoldcost;
                string strSamplecost = row["SAMPLECOST"].ToString();
                decimal decSamplecost = (strSamplecost.ToString().Equals("") ? 0 : Convert.ToDecimal(strSamplecost)); //
                row["SAMPLECOST"] = decSamplecost;
                string strManufacturingoverhead = row["MANUFACTURINGOVERHEAD"].ToString();
                decimal decManufacturingoverhead = (strManufacturingoverhead.ToString().Equals("") ? 0 : Convert.ToDecimal(strManufacturingoverhead)); //
                row["MANUFACTURINGOVERHEAD"] = decManufacturingoverhead;
                string strGeneralcost = row["GENERALCOST"].ToString();
                decimal decGeneralcost = (strGeneralcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strGeneralcost)); //
                row["GENERALCOST"] = decGeneralcost;
                string strEtccost = row["ETCCOST"].ToString();
                decimal decEtccost = (strEtccost.ToString().Equals("") ? 0 : Convert.ToDecimal(strEtccost)); //
                row["ETCCOST"] = decEtccost;
                decimal decsumcost = decVariablecost + decFixedcost + decMaterialcost + decMaterialprocesscost + decBoxcost
                                     + decMoldcost + decSamplecost + decManufacturingoverhead + decGeneralcost + decEtccost;
                row["DEFECTAMOUNT"] = dectotecdefectAmountDiv;
                decAmount = decsumcost + dectotecdefectAmountDiv;//견적가

                dectotecdefectAmountDiv =  Math.Truncate(dectotecdefectAmount / (intRowCount- i));
                row["DEFECTAMOUNT"] = dectotecdefectAmountDiv;
                dectotecdefectAmount = dectotecdefectAmount - dectotecdefectAmountDiv;
                decAmount = decsumcost + dectotecdefectAmountDiv;//견적가
                row["AMOUNT"] = decAmount;
                decAmountSum = decAmountSum + decAmount;// 견적누계
                row["CUMULATIVEAMOUNT"] = decAmountSum;//견적누계
                if (decPcsmm == 0)
                {
                    decPcsamount = 0;
                    decCumulativepcsamount = decCumulativepcsamount + decPcsamount;
                }
                else
                {
                    decPcsamount = Math.Round(decAmount / decPcsmm, 2);
                    decCumulativepcsamount = decCumulativepcsamount + decPcsamount;
                }
                row["CUMULATIVEAMOUNT"] = decAmountSum;//견적누계
                row["PCSAMOUNT"] = decPcsamount;
                row["CUMULATIVEPCSAMOUNT"] = decCumulativepcsamount;
               
            }


            // 2.1 불량 반영금액 계산하기 (비용누계/비용합계*불량반영금액 )
            // 2.2 불량 반영금액 보정하기
            // 2.3 견적가계산 
            // 2.4 견격누계
            // 2.5 불량반영금액 누계 
            // 2.6 pcs 견적금액, pcs견적누계
            //누적비율  decAmountSum 
            
            decimal decCumulativeRateSum = 0;
            for (int i = 0; i < dtAmount.Rows.Count; i++)
            {
                DataRow row = dtAmount.Rows[i];
                string strCumulativepcsamount = row["CUMULATIVEAMOUNT"].ToString();
                decCumulativepcsamount = (strCumulativepcsamount.ToString().Equals("") ? 0 : Convert.ToDecimal(strCumulativepcsamount)); //
                decimal decCumulativeRate = 0; // 불량 반영 금액
                 // 견적누계 0
                if (decAmountSum == 0)  
                {
                     row["CUMULATIVERATE"] = 0;  //누계비율
                }
                else
                {
                    if (decCumulativepcsamount > 0)
                    {
                        decCumulativeRate = Math.Round(decCumulativepcsamount / decAmountSum, 4) * 100;//견적가누계/견적합계

                        decCumulativeRateSum = decCumulativeRateSum + decCumulativeRate;
                        
                       row["CUMULATIVERATE"] = decCumulativeRate;//견적비율

                    }
                    else
                    {
                        row["CUMULATIVERATE"] = 0;//견적비율
                    }
                }
            }
            grdOutsourcedClaimBaseAmount.DataSource = dtAmount;
        }
        /// <summary>
        /// 비용합계 계산 부분 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                string strFocusedColumn = grdOutsourcedClaimBaseAmount.View.FocusedColumn.FieldName.ToString();
                if (!(grdOutsourcedClaimBaseAmount.View.FocusedColumn.OptionsColumn.AllowEdit== false))
                {
                    grdOutsourcedClaimBaseAmount.View.SetFocusedRowCellValue(grdOutsourcedClaimBaseAmount.View.FocusedColumn, null);
                }
               
            }
        }


        /// <summary>
        /// 비용합계 계산 부분 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            grdOutsourcedClaimBaseAmount.View.CellValueChanged -= View_CellValueChanged;
            DataRow row = grdOutsourcedClaimBaseAmount.View.GetFocusedDataRow();
            string strVariablecost = row["VARIABLECOST"].ToString();
            decimal decVariablecost = (strVariablecost.ToString().Equals("") ? 0 : Convert.ToDecimal(strVariablecost)); //
            row["VARIABLECOST"] = decVariablecost;

            string strFixedcost = row["FIXEDCOST"].ToString();
            decimal decFixedcost = (strFixedcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strFixedcost)); //
            row["FIXEDCOST"] = decFixedcost;
            string strMaterialcost = row["MATERIALCOST"].ToString();
            decimal decMaterialcost = (strMaterialcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMaterialcost)); //
            row["MATERIALCOST"] = decMaterialcost;
            string strMaterialprocesscost = row["MATERIALPROCESSCOST"].ToString();
            decimal decMaterialprocesscost = (strMaterialprocesscost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMaterialprocesscost)); //
            row["MATERIALPROCESSCOST"] = decMaterialprocesscost;
            string strBoxcost = row["BOXCOST"].ToString();
            decimal decBoxcost = (strBoxcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strBoxcost)); //
            row["BOXCOST"] = decBoxcost;
            string strMoldcost = row["MOLDCOST"].ToString();
            decimal decMoldcost = (strMoldcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strMoldcost)); //
            row["MOLDCOST"] = decMoldcost;
            string strSamplecost = row["SAMPLECOST"].ToString();
            decimal decSamplecost = (strSamplecost.ToString().Equals("") ? 0 : Convert.ToDecimal(strSamplecost)); //
            row["SAMPLECOST"] = decSamplecost;
            string strManufacturingoverhead = row["MANUFACTURINGOVERHEAD"].ToString();
            decimal decManufacturingoverhead = (strManufacturingoverhead.ToString().Equals("") ? 0 : Convert.ToDecimal(strManufacturingoverhead)); //
            row["MANUFACTURINGOVERHEAD"] = decManufacturingoverhead;
            string strGeneralcost = row["GENERALCOST"].ToString();
            decimal decGeneralcost = (strGeneralcost.ToString().Equals("") ? 0 : Convert.ToDecimal(strGeneralcost)); //
            row["GENERALCOST"] = decGeneralcost;
            string strEtccost = row["ETCCOST"].ToString();
            decimal decEtccost = (strEtccost.ToString().Equals("") ? 0 : Convert.ToDecimal(strEtccost)); //
            row["ETCCOST"] = decEtccost;
          
            grdOutsourcedClaimBaseAmount.View.CellValueChanged += View_CellValueChanged;
     

        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            int intfocusRow = grdOutsourcedClaimBaseAmount.View.FocusedRowHandle;
            grdOutsourcedClaimBaseAmount.View.DeleteRow(intfocusRow);
            
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            //base.OnToolbarSaveClick();
            //// 재계산로직 호출.
            //BtnCal_Click(null, null);
            //DataTable changed = grdOutsourcedClaimBaseAmount.DataSource as DataTable;
            //ExecuteRule("OutsourcedClaimBaseAmount", changed);
          
        }
        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;
            if (btn.Name.ToString().Equals("Copy"))
            {

                 BtnCopy_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Recalculation"))
            {

                BtnCal_Click(null, null);
            }
            if (btn.Name.ToString().Equals("Save"))
            {

                ProcSave(btn.Text);
            }
            if (btn.Name.ToString().Equals("Conamountview"))
            {

                BtnExchang_Click(null, null);
            }

        }
        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            string sproductcode = "";
            if (!(values["P_PRODUCTCODE"] == null))
            {
                sproductcode = values["P_PRODUCTCODE"].ToString();
              
            }
            string sPlantid = values["P_PLANTID"].ToString();
            _strPlantid = sPlantid;
            if (sproductcode.Equals(""))
            {
                //화면 clear 
                usrProductdefid.CODE.EditValue = "";
                usrProductdefid.VERSION.EditValue = "";
                usrProductdefid.NAME.EditValue = "";
                usrProductdefidTO.CODE.EditValue = "";
                usrProductdefidTO.VERSION.EditValue = "";
                usrProductdefidTO.NAME.EditValue = "";
                DataTable dt = (grdOutsourcedClaimBaseAmount.View.DataSource as DataTable).Clone();
                grdOutsourcedClaimBaseAmount.DataSource = dt;
                return;
            }
            string[] sproductd = sproductcode.Split('|');
            // plant 정보 다시 가져오기 
            values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
            values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            values.Add("P_PROCESSDEFID", sproductd[2].ToString());
            values.Add("P_PROCESSDEFVERSION", sproductd[3].ToString());
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtBudgetEexpenses = await SqlExecuter.QueryAsync("GetOutsourcedClaimBaseAmount", "10001", values);
            if (dtBudgetEexpenses.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdOutsourcedClaimBaseAmount.DataSource = dtBudgetEexpenses;

            OnProductInformationSearch(sproductd[0].ToString(), sproductd[1].ToString(), sproductd[2].ToString() , sproductd[3].ToString());
           
        }
        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //plant
            //InitializeConditionPopup_Plant();
            //product 
            InitializeConditionPopup_Product();


        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            //1.제품코드 
            if (usrProductdefid.CODE.Text.ToString().Equals(""))
            {
               
                throw MessageException.Create("InValidOspRequiredField", lblProductdefid.Text);
               
            }
            //2.화페단위 
            if (cboCurrencyunit.EditValue.ToString().Equals(""))
            {
              
                throw MessageException.Create("InValidOspRequiredField", lblCurrencyunit.Text);
               
            }
            // TODO : 유효성 로직 변경
            grdOutsourcedClaimBaseAmount.View.CheckValidation();

            DataTable changed = grdOutsourcedClaimBaseAmount.DataSource as DataTable;

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        /// <summary>
        /// Product에서 통화및 기타정보 가져오기 
        /// </summary>
        private void OnProductInformationSearch(string sproductdefid, string sproductdefversion,string sprocessdefid, string sprocessdefversion)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("PRODUCTDEFID", sproductdefid);
            dicParam.Add("PRODUCTDEFVERSION", sproductdefversion);
            dicParam.Add("PROCESSDEFID", sprocessdefid);
            dicParam.Add("PROCESSDEFVERSION", sprocessdefversion);
            dicParam = Commons.CommonFunction.ConvertParameter(dicParam);
            DataTable dtProduct = SqlExecuter.Query("GetProductInformatByOsp", "10001", dicParam);
            if (dtProduct.Rows.Count == 1)
            {
                DataRow drProduct = dtProduct.Rows[0];
                //
                usrProductdefid.CODE.EditValue = drProduct["PRODUCTDEFID"].ToString();
                usrProductdefid.VERSION.EditValue = drProduct["PRODUCTDEFVERSION"].ToString();
                usrProductdefid.NAME.EditValue = drProduct["PRODUCTDEFNAME"].ToString();
                cboCurrencyunit.EditValue = drProduct["CURRENCYUNIT"].ToString();
                txtDefectamount.EditValue = drProduct["DEFECTAMOUNT"].ToString();
                txtPcsmm.EditValue = drProduct["PCSMM"].ToString();
                usrProductdefid.txtProdcutCode.Text = sproductdefid + "|" + sproductdefversion + "|" + sprocessdefid + "|" + sprocessdefversion + "|";
                // txtPcsmm.EditValue = "20";
            }
            else
            {
                usrProductdefid.CODE.EditValue ="";
                usrProductdefid.VERSION.EditValue = "";
                usrProductdefid.NAME.EditValue = "";
                cboCurrencyunit.EditValue = "";
                txtDefectamount.EditValue = "";
                txtPcsmm.EditValue = "";

            }

        }
        /// <summary>
        /// 복사시 기본테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable createSaveDatatable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "list";
            dt.Columns.Add("ENTERPRISEID");
           
            dt.Columns.Add("PRODUCTDEFID");
            dt.Columns.Add("PRODUCTDEFVERSION");
            dt.Columns.Add("PROCESSDEFID");
            dt.Columns.Add("PROCESSDEFVERSION");
            dt.Columns.Add("TOPRODUCTDEFID");
            dt.Columns.Add("TOPRODUCTDEFVERSION");
            dt.Columns.Add("TOPROCESSDEFID");
            dt.Columns.Add("TOPROCESSDEFVERSION");
            return dt;
        }
       
        /// <summary>
        /// 복사 후 재조회용 
        /// </summary>

        private void OnCopySearch()
        {
            try
            {
                DialogManager.ShowWaitArea(this.pnlContent);
                var values = Conditions.GetValues();
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                #region 품목코드 전환 처리 
                string sproductcode = "";
                if (!(values["P_PRODUCTCODE"] == null))
                {
                    sproductcode = values["P_PRODUCTCODE"].ToString();
                }
                if (sproductcode.Equals("")) return;
                    // 품목코드값이 있으면
                string[] sproductd = sproductcode.Split('|');
                 // plant 정보 다시 가져오기 
                values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                values.Add("P_PROCESSDEFID", sproductd[2].ToString());
                values.Add("P_PROCESSDEFVERSION", sproductd[3].ToString());
                #endregion
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable dtBudgetEexpenses = SqlExecuter.Query("GetOutsourcedClaimBaseAmount", "10001", values);
                if (dtBudgetEexpenses.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOutsourcedClaimBaseAmount.DataSource = dtBudgetEexpenses;

                OnProductInformationSearch(sproductd[0].ToString(), sproductd[1].ToString(), sproductd[2].ToString(), sproductd[3].ToString());

            }
            catch (Exception ex)
            {
                throw Framework.MessageException.Create(ex.ToString());
            }
            finally
            {
                DialogManager.CloseWaitArea(this.pnlContent);

            }
        }
        private void ProcSave(string strtitle)
        {
            //1.제품코드 
            if (usrProductdefid.CODE.Text.ToString().Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblProductdefid.Text);
                return;

            }
            //2.화페단위 
            if (cboCurrencyunit.EditValue.ToString().Equals(""))
            {

                this.ShowMessage(MessageBoxButtons.OK, "InValidOspRequiredField", lblCurrencyunit.Text);
                return;

            }
            // TODO : 유효성 로직 변경
            grdOutsourcedClaimBaseAmount.View.CheckValidation();

            DataTable changed = grdOutsourcedClaimBaseAmount.DataSource as DataTable;

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                this.ShowMessage(MessageBoxButtons.OK, "NoSaveData");
                return;
            }

             DialogResult result = System.Windows.Forms.DialogResult.No;

            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    BtnCal_Click(null, null);
                    DataTable dtSave = grdOutsourcedClaimBaseAmount.DataSource as DataTable;
                    ExecuteRule("OutsourcedClaimBaseAmount", dtSave);

                    ShowMessage("SuccessOspProcess");
                    //재조회 
                    OnSaveConfrimSearch();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();


                }
            }
        }
        private void OnSaveConfrimSearch()
        {

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            string sproductcode = "";
            if (!(values["P_PRODUCTCODE"] == null))
            {
                sproductcode = values["P_PRODUCTCODE"].ToString();

            }
            string sPlantid = values["P_PLANTID"].ToString();
            _strPlantid = sPlantid;
            if (sproductcode.Equals(""))
            {
                //화면 clear 
                usrProductdefid.CODE.EditValue = "";
                usrProductdefid.VERSION.EditValue = "";
                usrProductdefid.NAME.EditValue = "";
                usrProductdefidTO.CODE.EditValue = "";
                usrProductdefidTO.VERSION.EditValue = "";
                usrProductdefidTO.NAME.EditValue = "";
                DataTable dt = (grdOutsourcedClaimBaseAmount.View.DataSource as DataTable).Clone();
                grdOutsourcedClaimBaseAmount.DataSource = dt;
                return;
            }
            string[] sproductd = sproductcode.Split('|');
            // plant 정보 다시 가져오기 
            values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
            values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
            values.Add("P_PROCESSDEFID", sproductd[2].ToString());
            values.Add("P_PROCESSDEFVERSION", sproductd[3].ToString());
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtBudgetEexpenses =  SqlExecuter.Query("GetOutsourcedClaimBaseAmount", "10001", values);
            if (dtBudgetEexpenses.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdOutsourcedClaimBaseAmount.DataSource = dtBudgetEexpenses;

            OnProductInformationSearch(sproductd[0].ToString(), sproductd[1].ToString(), sproductd[2].ToString(), sproductd[3].ToString());

        }
        #endregion
    }
}
