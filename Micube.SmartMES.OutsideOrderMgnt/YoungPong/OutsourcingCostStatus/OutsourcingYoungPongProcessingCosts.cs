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

#endregion

namespace Micube.SmartMES.OutsideOrderMgnt
{
    /// <summary>
    /// 프 로 그 램 명  :  외주관리> 외주비현황 > 외주가공비 집계현황
    /// 업  무  설  명  : 외주가공비 집계현황 조회
    /// 생    성    자  : CHOISSTAR
    /// 생    성    일  : 2019-09-17
    /// 수  정  이  력  : 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class OutsourcingYoungPongProcessingCosts : SmartConditionManualBaseForm
    {
        #region Local Variables


        #endregion

        #region 생성자

        public OutsourcingYoungPongProcessingCosts()
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
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGridLayer();
            InitializeGridProduct();
        }

        private void InitializeGridLayer()
        {
            //grdOspProcLayer
            grdOspProcLayer.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspProcLayer.View.SetIsReadOnly();
            var costprocess = grdOspProcLayer.View.AddGroupColumn("OSPPROCESSCOSTS");
            costprocess.AddComboBoxColumn("LAYER", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));   //   layer 
            costprocess.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100);
            costprocess.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);
            //사내(pnl,pcs,mm,금액)
            var costinhouse = grdOspProcLayer.View.AddGroupColumn("INHOUSEOSP");
            costinhouse.AddTextBoxColumn("INHOUSEOSPPANELQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costinhouse.AddTextBoxColumn("INHOUSEOSPPCSQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costinhouse.AddTextBoxColumn("INHOUSEOSPM2QTY", 120)
                .SetLabel("M2")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costinhouse.AddTextBoxColumn("INHOUSEOSPAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costinhouse.AddTextBoxColumn("INHOUSEOSPAMOUNTETC", 120).SetLabel("OEAAMOUNT")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //costinhouse.AddTextBoxColumn("INHOUSEOSPAMOUNTCLAIM", 120).SetLabel("OSPTOLOTTYPECLAIM")
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            //사외(pnl,pcs,mm,금액)
            var costoutside = grdOspProcLayer.View.AddGroupColumn("OUTSIDEOSP");
            costoutside.AddTextBoxColumn("OUTSIDEOSPPANELQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costoutside.AddTextBoxColumn("OUTSIDEOSPPCSQTY", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costoutside.AddTextBoxColumn("OUTSIDEOSPM2QTY", 120)
                .SetLabel("M2")
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            costoutside.AddTextBoxColumn("OUTSIDEOSPAMOUNT", 120)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //자사(pnl,pcs,mm,금액)
            //var costourcomp = grdOspProcLayer.View.AddGroupColumn("");
            //costourcomp.AddTextBoxColumn("OURCOMPANYPANELQTY", 120).SetLabel("OUTSIDEOSPPANELQTY")
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            //costourcomp.AddTextBoxColumn("OURCOMPANYPCSQTY", 120).SetLabel("OUTSIDEOSPPCSQTY")
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //costourcomp.AddTextBoxColumn("OURCOMPANYM2QTY", 120).SetLabel("OUTSIDEOSPM2QTY")
            //    .SetLabel("M2")
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            //costourcomp.AddTextBoxColumn("OURCOMPANYAMOUNT", 120).SetLabel("OUTSIDEOSPAMOUNT")
            //    .SetTextAlignment(TextAlignment.Right)
            //    .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdOspProcLayer.View.PopulateColumns();
            grdOspProcLayer.View.Columns["PROCESSSEGMENTCLASSNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["PROCESSSEGMENTCLASSNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdOspProcLayer.View.Columns["INHOUSEOSPPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["INHOUSEOSPPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.Columns["INHOUSEOSPPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["INHOUSEOSPPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.Columns["INHOUSEOSPM2QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["INHOUSEOSPM2QTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.Columns["INHOUSEOSPAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["INHOUSEOSPAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.Columns["INHOUSEOSPAMOUNTETC"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["INHOUSEOSPAMOUNTETC"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspProcLayer.View.Columns["OUTSIDEOSPPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["OUTSIDEOSPPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.Columns["OUTSIDEOSPPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["OUTSIDEOSPPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.Columns["OUTSIDEOSPM2QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["OUTSIDEOSPM2QTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.Columns["OUTSIDEOSPAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcLayer.View.Columns["OUTSIDEOSPAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcLayer.View.OptionsView.ShowFooter = true;
            grdOspProcLayer.ShowStatusBar = false;
        }

        private void InitializeGridProduct()
        {

            grdOspProcProduct.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdOspProcProduct.View.SetIsReadOnly();
            var costprocess = grdOspProcProduct.View.AddGroupColumn("OSPCOSTSBYDATE");//공정및 협력사정보
            costprocess.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 120);

            costprocess.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            costprocess.AddTextBoxColumn("OSPVENDORID", 80);
            costprocess.AddTextBoxColumn("OSPVENDORNAME", 100);
            var costtotlottype = grdOspProcProduct.View.AddGroupColumn("OSPTOLOTTYPE");
            //전체(pnl,pcs,mm ,외주비 ,기타외주비 )Production
            costtotlottype.AddTextBoxColumn("OSPTOLOTTYPEPANELQTY", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlottype.AddTextBoxColumn("OSPTOLOTTYPEPCSQTY", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlottype.AddTextBoxColumn("OSPTOLOTTYPEMMQTY", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlottype.AddTextBoxColumn("OSPTOLOTTYPEAMOUNT", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlottype.AddTextBoxColumn("OSPTOLOTTYPEETC", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlottype.AddTextBoxColumn("OSPTOLOTTYPECLAIM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            var costtotlotprd = grdOspProcProduct.View.AddGroupColumn("OSPLOTTYPEPRD");
            costtotlotprd.AddTextBoxColumn("OSPLOTTYPEPRDPANELQTY", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPLOTTYPEPRDPCSQTY", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPLOTTYPEPRDMMQTY", 120)
               .SetTextAlignment(TextAlignment.Right)
             .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPLOTTYPEPRDAMOUNT", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPLOTTYPEPRDETC", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotprd.AddTextBoxColumn("OSPLOTTYPEPRDCLAIM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //양산(pnl,pcs,mm ,외주비 ,기타외주비 )
            var costtotlotsim = grdOspProcProduct.View.AddGroupColumn("OSPLOTTYPESIM");
            costtotlotsim.AddTextBoxColumn("OSPLOTTYPESIMPANELQTY", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddTextBoxColumn("OSPLOTTYPESIMPCSQTY", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddTextBoxColumn("OSPLOTTYPESIMMMQTY", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddTextBoxColumn("OSPLOTTYPESIMAMOUNT", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddTextBoxColumn("OSPLOTTYPESIMETC", 120)
               .SetTextAlignment(TextAlignment.Right)
               .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            costtotlotsim.AddTextBoxColumn("OSPLOTTYPESIMCLAIM", 120)
              .SetTextAlignment(TextAlignment.Right)
              .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdOspProcProduct.View.PopulateColumns();
            InitializationSummaryRow();
        }

        private void InitializationSummaryRow()
        {
            grdOspProcProduct.View.Columns["OSPVENDORNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPVENDORNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEMMQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEETC"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPTOLOTTYPEETC"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPTOLOTTYPECLAIM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPTOLOTTYPECLAIM"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDMMQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDETC"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDETC"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDCLAIM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPEPRDCLAIM"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMPANELQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMMMQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMMMQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMETC"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMETC"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMCLAIM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOspProcProduct.View.Columns["OSPLOTTYPESIMCLAIM"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOspProcProduct.View.OptionsView.ShowFooter = true;
            grdOspProcProduct.ShowStatusBar = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {

        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();


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

            if (values["P_SEARCHDATE_PERIODFR"].ToString().Equals("") && values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")
                && values["P_YEARMONTH"].ToString().Equals(""))
            {
                ShowMessage("NoConditions_03"); // 검색조건중 (정산기간은 입력해야합니다.다국어
                return;
            }

            #region 기간 검색형 전환 처리 
            if (!(values["P_SEARCHDATE_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_SEARCHDATE_PERIODFR"]);
                values.Remove("P_SEARCHDATE_PERIODFR");
                values.Add("P_SEARCHDATE_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_SEARCHDATE_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_SEARCHDATE_PERIODTO"]);
                values.Remove("P_SEARCHDATE_PERIODTO");
                //requestDateTo = requestDateTo.AddDays(1);
                values.Add("P_SEARCHDATE_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            #endregion
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            
            //prestanddate = prestanddate.AddDays(1);
            
            values.Add("USERID", UserInfo.Current.Id.ToString());
            values = Commons.CommonFunction.ConvertParameter(values);
            if (tapProcessCosts.SelectedTabPage.Name.Equals("tapOspProcProduct"))
            {
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongProcessingCostsProduct", "10001", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspProcProduct.DataSource = dt;
            }
            else if (tapProcessCosts.SelectedTabPage.Name.Equals("tapOspProcLayer"))
            {
                //DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongProcessingCostsLayer", "10001", values);
                DataTable dt = await SqlExecuter.QueryAsync("GetOutsourcingYoungPongProcessingCostsLayer2", "10002", values);

                if (dt.Rows.Count < 1) // 검색 조건에 해당하는 DATA가 없는 경우 
                {
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                }

                grdOspProcLayer.DataSource = dt;
            }

        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            //1.site
            //InitializeConditionPopup_Plant();
            InitializeConditionPopup_PeriodTypeOSP();
            //2.기준일
            // InitializeCondition_StandDate();
            InitializeCondition_Yearmonth();
            //3.협력사
            InitializeConditionPopup_OspVendorid();
            //4.양산구분
            InitializeConditionPopup_ProductionType();
            //5.공정
            InitializeConditionPopup_ProcessSegment();
            //6.층수 
            InitializeConditionPopup_Layer();
        }
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {

            var planttxtbox = Conditions.AddComboBox("p_plantid", new SqlQuery("GetPlantList", "00001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
               .SetPosition(0.2)
               .SetDefault(UserInfo.Current.Plant, "p_plantId") //기본값 설정 UserInfo.Current.Plant
               .SetIsReadOnly(true)
               .SetValidationIsRequired()
            ;
            //   

        }
        /// <summary>
        ///외주실적
        /// </summary>
        private void InitializeConditionPopup_PeriodTypeOSP()
        {

            var owntypecbobox = Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodTypeOSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PERIODTYPEOSP")
               .SetPosition(0.3)
               .SetDefault("") //OutSourcing
               .SetEmptyItem("", "")//
            ;
        }

        /// <summary>
        /// 마감년월
        /// </summary>
        private void InitializeCondition_Yearmonth()
        {
            DateTime dateNow = DateTime.Now;
            string strym = dateNow.ToString("yyyy-MM");
            var YearmonthDT = Conditions.AddDateEdit("p_yearmonth")
               .SetLabel("CLOSEYM")
               .SetDisplayFormat("yyyy-MM")
               .SetPosition(1.1)
            // .SetDefault(strym)
            //.SetValidationIsRequired()
            ;

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
               .SetRelationIds("p_plantid")
               .SetPosition(2.2);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(2.4);

        }
        /// <summary>
        ///양산구분
        /// </summary>
        private void InitializeConditionPopup_ProductionType()
        {

            var owntypecbobox = Conditions.AddComboBox("p_ProductionType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PRODUCTIONTYPE")
               .SetPosition(2.6)
               .SetEmptyItem("", "")
            ;


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
            .SetPosition(2.8);

            // 팝업 조회조건
            ProcessSegmentPopupColumn.Conditions.AddTextBox("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENTID");

            // 팝업 그리드
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetValidationKeyColumn();
            ProcessSegmentPopupColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);

        }
        /// <summary>
        ///LAYER 
        /// </summary>
        private void InitializeConditionPopup_Layer()
        {

            var owntypecbobox = Conditions.AddComboBox("p_Layer", new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("LAYER")
               .SetPosition(2.9)
               .SetEmptyItem("", "")
            ;


        }
        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartPeriodEdit>("P_SEARCHDATE").LanguageKey = "EXPSETTLEDATE";
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();


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

        #endregion
    }
}
