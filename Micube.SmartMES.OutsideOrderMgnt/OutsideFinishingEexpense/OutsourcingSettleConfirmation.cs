#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주 가공비마감 > 외주실적정산등록
    /// 업  무  설  명  :  외주실적정산등록
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-08-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingSettleConfirmation : SmartConditionBaseForm
    {
        #region Local Variables

        
        #endregion

        #region 생성자

        public OutsourcingSettleConfirmation()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            InitializationSummaryRow();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdMaster.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMaster.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMaster.View.AddTextBoxColumn("PLANTID", 200)
                .SetIsHidden();
            grdMaster.View.AddTextBoxColumn("LOTHISTKEY", 200)
                .SetIsHidden();
            grdMaster.View.AddTextBoxColumn("ENTERPRISEID", 200)
                .SetIsHidden();                                                           //  공장 ID
            grdMaster.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("ISSETTLE", 80)
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(); //
          
            grdMaster.View.AddTextBoxColumn("SETTLEDATE", 120)
                .SetLabel("EXPSETTLEDATE")
                .SetDisplayFormat("yyyy-MM-dd");                                                     //  
            grdMaster.View.AddTextBoxColumn("SETTLEUSER", 80)
                .SetIsHidden();
            grdMaster.View.AddTextBoxColumn("SETTLEUSERNAME", 80)
                 .SetLabel("EXPSETTLEUSERNAME")
                 .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PERIODID", 80)
                .SetIsHidden();
            grdMaster.View.AddComboBoxColumn("PERIODSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetDefault("Open")
                 .SetIsReadOnly();
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                grdMaster.View.AddComboBoxColumn("OSPPRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OSPProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetEmptyItem("*", "*");  // 
            }
            else
            {
                grdMaster.View.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetEmptyItem("*", "*")
                    .SetIsReadOnly();  // 
                grdMaster.View.AddComboBoxColumn("PROCESSPRICETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProcessPriceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                    .SetEmptyItem("*", "*")
                    .SetIsReadOnly();  // 
            }
            grdMaster.View.AddTextBoxColumn("AREAID", 80)
                .SetIsHidden();
            grdMaster.View.AddTextBoxColumn("AREANAME", 100)
                .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("OSPVENDORID", 80)
                .SetIsHidden();
            grdMaster.View.AddTextBoxColumn("OSPVENDORNAME", 100)
                .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                    .SetIsReadOnly();

            grdMaster.View.AddTextBoxColumn("PERFORMANCEDATE", 100)
                    .SetIsReadOnly();                              //  
            grdMaster.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly();
            grdMaster.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdMaster.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                          //  panelqty
            grdMaster.View.AddTextBoxColumn("OSPMM", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);                          //  m2qty

            grdMaster.View.AddTextBoxColumn("DEFECTPCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);                                //  pcsqty

            grdMaster.View.AddTextBoxColumn("OSPPRICE", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            grdMaster.View.AddTextBoxColumn("ACTUALAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdMaster.View.AddTextBoxColumn("REDUCEAMOUNT", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdMaster.View.AddTextBoxColumn("SETTLEAMOUNT", 120)
               .SetIsReadOnly()
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdMaster.View.PopulateColumns();

          
        }
        private void InitializationSummaryRow()
        {
            grdMaster.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["LOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdMaster.View.Columns["PCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdMaster.View.Columns["OSPMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OSPMM"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdMaster.View.Columns["DEFECTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["DEFECTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["ACTUALAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["ACTUALAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["REDUCEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["REDUCEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdMaster.View.Columns["SETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["SETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###0}";

            grdMaster.View.OptionsView.ShowFooter = true;
            grdMaster.ShowStatusBar = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
           
        }

       
       

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
           
        }

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            //base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;

            if (btn.Name.ToString().Equals("Settleprocess"))
            {
                ProcSettleprocess(btn.Text);


            }
            if (btn.Name.ToString().Equals("Settlecancel"))
            {

                ProcSettlecancel(btn.Text);
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

            var values = Conditions.GetValues();
            if (values["P_EXPSETTLEDATE_PERIODFR"].ToString().Equals("") && values["P_EXPSETTLEDATE_PERIODTO"].ToString().Equals("") 
              && values["P_SENDDATE_PERIODFR"].ToString().Equals("") && values["P_SENDDATE_PERIODTO"].ToString().Equals(""))
            {
                ShowMessage("NoConditions_01"); // 검색조건중 (정산기간은 입력해야합니다.다국어
                return;
            }
            //if (values["P_PROCESSSEGMENTID"].ToString().Equals("") && values["P_PRODUCTCODE"].ToString().Equals("")
            //  && values["P_PRODUCTDEFNAME"].ToString().Equals("") && values["P_OSPVENDORID"].ToString().Equals("") && values["P_AREAID"].ToString().Equals("") && values["P_LOTID"].ToString().Equals(""))
            //{
            //    ShowMessage("NoConditions_02"); // 검색조건중 적어도(공정,품목,작업장,lot no) 기간입력해야합니다.다국어
            //    return;
            //}
            //품목코드 품목명 공정 작업장 lot 
            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #endregion
            #region 기간 검색형 전환 처리 
            if (!(values["P_EXPSETTLEDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODFR"]);
                values.Remove("P_EXPSETTLEDATE_PERIODFR");
                values.Add("P_EXPSETTLEDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_EXPSETTLEDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODTO"]);
                values.Remove("P_EXPSETTLEDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_EXPSETTLEDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            if (!(values["P_SENDDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SENDDATE_PERIODFR"]);
                values.Remove("P_SENDDATE_PERIODFR");
                values.Add("P_SENDDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SENDDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SENDDATE_PERIODTO"]);
                values.Remove("P_SENDDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_SENDDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtSettle = SqlExecuter.Query("GetOutsourcingSettleConfirmation", "10001", values);

            grdMaster.DataSource = dtSettle;
            
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
           
            InitializeConditionPopup_PeriodTypeOSP();
          
            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //실적기간

            //정산기간 
            
            //마감년월
            InitializeCondition_Yearmonth();

            //품목코드
            InitializeConditionPopup_ProductDefId();
            //품목명
            //공정id
            InitializeConditionPopup_ProcessSegment();
            //lotid
            InitializeConditionPopup_Lotno();
            //생산구분 

            //작업장.
            InitializeConditionPopup_OspAreaid();
            //협력사
            //협력사명 
            InitializeConditionPopup_OspVendorid();
            
            //정산여부 
            InitializeCondition_IsSettle();

            
           
        }

        private void InitializeConditionPopup_PeriodTypeOSP()
        {

            var owntypecbobox = Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodTypeOSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PERIODTYPEOSP")
               .SetPosition(0.2)
               .SetValidationIsRequired()
               .SetDefault("OutSourcing")
            ;
        }
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(3.2)
               .SetDefault("")
;
        }

        /// <summary>
        /// 품목코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_ProductDefId()
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
                 .SetPosition(3.4);
            // 팝업 조회조건
            popupProduct.Conditions.AddComboBox("P_PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTDEFTYPE")
                .SetDefault("Product");

            popupProduct.Conditions.AddTextBox("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID");


            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 150)
                .SetIsHidden();
            popupProduct.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetIsReadOnly();
            popupProduct.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();

            var txtProductName = Conditions.AddTextBox("P_PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFNAME")
                .SetPosition(3.6);
        }
        /// <summary>
        /// ProcessSegment 설정 
        /// </summary>
        private void InitializeConditionPopup_ProcessSegment()
        {
            var ProcessSegmentPopupColumn = Conditions.AddSelectPopup("p_processsegmentid",
                                                               new SqlQuery("GetProcessSegmentListByOsp", "10001"
                                                                               , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                               , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                               ), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
            .SetPopupLayout("PROCESSSEGMENTID", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600)
            .SetLabel("PROCESSSEGMENTID")
            .SetPopupResultCount(1)
            .SetPosition(3.8);

            // 팝업 조회조건
            ProcessSegmentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                 .SetLabel("PROCESSSEGMENT");

            // 팝업 그리드
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }

        private void InitializeConditionPopup_Lotno()
        {
            
            var txtOspVendor = Conditions.AddTextBox("P_LOTID")
               .SetLabel("LOTID")
               .SetPosition(4.0);

        }
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_areaid", new SqlQuery("GetAreaidPopupListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "AREANAME", "AREAID")
               .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("AREANAME")
               .SetRelationIds("p_plantId")
               .SetPopupResultCount(1)
               .SetPosition(4.4);
            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREAID", 120);
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 작업업체 .고객 조회조건
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("p_ospvendorid", new SqlQuery("GetVendorListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetPopupResultCount(1)
               .SetRelationIds("p_plantid", "p_areaid")
               .SetPosition(4.6);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
              .SetLabel("OSPVENDORNAME")
              .SetPosition(4.8);


        }

        /// <summary>
        /// 정산여부
        /// </summary>
        private void InitializeCondition_IsSettle()
        {
            var YesNobox = Conditions.AddComboBox("p_isSettle", new SqlQuery("GetCodeList", "00001", $"CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                 .SetLabel("ISSETTLE")
                 .SetPosition(5.4)
                 .SetEmptyItem("","")
                 .SetDefault("*")
                 ;
        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            // 기간 포맷 용
            InitializeDatePeriod();
           
        }
        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriod()
        {

            InitializeDatePeriodSetting("P_EXPSETTLEDATE");


        }
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
           
        }

        #endregion

        #region Private Function
        /// <summary>
        /// 기간 포맷 재정의 
        /// </summary>
        private void InitializeDatePeriodSetting(string sPeriodname)
        {
            // 기간 포맷 재정의 
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add(sPeriodname, "CUSTOM");
            values.Add(sPeriodname + "_PERIODFR", "");
            values.Add(sPeriodname + "_PERIODTO", "");

            Conditions.GetControl<SmartPeriodEdit>(sPeriodname).SetValue(values);

        }
        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }
        // Settlecancel ProcSettleprocess
        private void ProcSettlecancel(string strtitle)
        {
            if (blSettlecancelCheck(strtitle) == false) return;
            DialogResult result = System.Windows.Forms.DialogResult.No;
            DataRow dr = null;
            DataTable dtSave = null;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                  
                    dtSave = (grdMaster.DataSource as DataTable).Clone();
                    DataTable dtcheck = grdMaster.View.GetCheckedRows();

                    for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                    {
                        dr = dtcheck.Rows[irow];

                        if (dr["ISSETTLE"].ToString().Equals("Y"))
                        {
                            dr["SETTLEUSER"] = "";
                            dr["ISSETTLE"] = "N";
                            dtSave.ImportRow(dr);
                        }
                    }
                   // this.ExecuteRule("OutsourcingSettleConfirmation", dtSave);
                    MessageWorker worker = new MessageWorker("OutsourcingSettleConfirmation");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    worker.ExecuteWithTimeout(300);
                    ShowMessage("SuccessOspProcess");
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

        private void ProcSettleprocess(string strtitle)
        {
            if (blSettleprocessCheck(strtitle) == false) return;
            DialogResult result = System.Windows.Forms.DialogResult.No;
            DataRow dr = null;
            DataTable dtSave = null;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "OspDoProcessWant", strtitle);//저장하시겠습니까? 

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                 
                    dtSave = (grdMaster.DataSource as DataTable).Clone();
                    DataTable dtcheck = grdMaster.View.GetCheckedRows();
                    DateTime dtTrans = Convert.ToDateTime(dtpSettledate.EditValue.ToString());
                    string strSettledate= dtTrans.ToString("yyyy-MM-dd");
                    for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
                    {
                        dr = dtcheck.Rows[irow];

                        if (dr["ISSETTLE"].ToString().Equals("N"))
                        {
                            dr["SETTLEUSER"] = UserInfo.Current.Id.ToString();
                            dr["SETTLEDATE"] = strSettledate;
                            dr["ISSETTLE"] = "Y";
                            dtSave.ImportRow(dr);
                        }
                    }
                   // this.ExecuteRule("OutsourcingSettleConfirmation", dtSave);
                    MessageWorker worker = new MessageWorker("OutsourcingSettleConfirmation");
                    worker.SetBody(new MessageBody()
                        {
                            { "list", dtSave }
                        });
                    worker.ExecuteWithTimeout(300);
                    ShowMessage("SuccessOspProcess");
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

        private bool blSettleprocessCheck(string strtitle)
        {
            int idatacount = 0;
            string strIssettle = "";
            string strPeriodid = "";
            DataRow dr = null;
           
            DataTable dtcheck = grdMaster.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return false;
            }
            for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
            {
                dr = dtcheck.Rows[irow];
                strIssettle = dr["ISSETTLE"].ToString();
                strPeriodid = dr["PERIODID"].ToString();
                if (!(strPeriodid.Equals("")))
                {
                    string lblConsumabledefid = grdMaster.View.Columns["OSPVENDORNAME"].Caption.ToString();

                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                    return false;

                }
                if (strIssettle.Equals("N"))
                {
                    idatacount = idatacount + 1;
                }

            }
           
            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", strtitle);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }
        private bool blSettlecancelCheck(string strtitle)
        {
            int idatacount = 0;
            string strIssettle = "";
            string strPeriodid = "";
            DataRow dr = null;
            DataTable dtcheck = grdMaster.View.GetCheckedRows();

            if (dtcheck.Rows.Count == 0)
            {
                ShowMessage("GridNoChecked"); //(선택된 내역이 없습니다)/
                return false;
            }
            for (int irow = 0; irow < dtcheck.Rows.Count; irow++)
            {
                dr = dtcheck.Rows[irow];
                strPeriodid = dr["PERIODID"].ToString();
                if (!(strPeriodid.Equals("")))
                {
                    string lblConsumabledefid = grdMaster.View.Columns["OSPVENDORNAME"].Caption.ToString();

                    this.ShowMessage(MessageBoxButtons.OK, "InValidOspData010", lblConsumabledefid + dr["OSPVENDORNAME"].ToString());
                    return false;

                }
                strIssettle = dr["ISSETTLE"].ToString();
                if (strIssettle.Equals("Y"))
                {
                    idatacount = idatacount + 1;
                }
            }
            
            if (idatacount == 0)
            {
                this.ShowMessage(MessageBoxButtons.OK, "OspDataCountCheck", strtitle);   //다국어 메세지 처리 
                return false;
            }
            return true;
        }
        /// <summary>
        /// 저장 후 재조회용 
        /// </summary>

        private void OnSaveConfrimSearch()
        {

          
            var values = Conditions.GetValues();
            values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise);
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #endregion
            #region 품목코드 전환 처리 
            if (!(values["P_PRODUCTCODE"] == null))
            {
                string sproductcode = values["P_PRODUCTCODE"].ToString();
                // 품목코드값이 있으면
                if (!(sproductcode.Equals("")))
                {
                    string[] sproductd = sproductcode.Split('|');
                    // plant 정보 다시 가져오기 
                    values.Add("P_PRODUCTDEFID", sproductd[0].ToString());
                    values.Add("P_PRODUCTDEFVERSION", sproductd[1].ToString());
                }
            }
            #endregion
            #region 기간 검색형 전환 처리 
            if (!(values["P_EXPSETTLEDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODFR"]);
                values.Remove("P_EXPSETTLEDATE_PERIODFR");
                values.Add("P_EXPSETTLEDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_EXPSETTLEDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_EXPSETTLEDATE_PERIODTO"]);
                values.Remove("P_EXPSETTLEDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_EXPSETTLEDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            if (!(values["P_SENDDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SENDDATE_PERIODFR"]);
                values.Remove("P_SENDDATE_PERIODFR");
                values.Add("P_SENDDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SENDDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SENDDATE_PERIODTO"]);
                values.Remove("P_SENDDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_SENDDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable dtSettle = SqlExecuter.Query("GetOutsourcingSettleConfirmation", "10001", values);

            grdMaster.DataSource = dtSettle;
        }

        #endregion
    }
}