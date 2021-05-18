#region using

using DevExpress.Utils;
using DevExpress.XtraCharts;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 >출하검사 > 출하검사 LRR 실적
    /// 업  무  설  명  : 출하검사 LRR 실적
    /// 생    성    자  : 최별
    /// 생    성    일  : 2019-10-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspLRRModelList : SmartConditionManualBaseForm
    {
        #region Local Variables

      

        #endregion

        #region 생성자

        public ShipmentInspLRRModelList()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
        }

        /// <summary>        
        ///  그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            InitializeGrid_Master();
        }
        private void InitializeGrid_Master()
        {
            grdMaster.GridButtonItem = GridButtonItem.Export | GridButtonItem.Expand | GridButtonItem.Restore;
            grdMaster.View.SetIsReadOnly();

            var defaultCol = grdMaster.View.AddGroupColumn("");

            defaultCol.AddTextBoxColumn("PLANTID", 120)
                .SetIsHidden();

            defaultCol.AddComboBoxColumn("PRODUCTIONTYPE", 100, new SqlQuery("GetShipProducttionTypeCodeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("LOTPRODUCTTYPE"); // 양산구분
            defaultCol.AddTextBoxColumn("PRODUCTDEFID", 100);
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            defaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            //grdMaster.View.AddTextBoxColumn("PRODUCTREVISION", 100);

            defaultCol.AddTextBoxColumn("ENTERLOTQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            defaultCol.AddTextBoxColumn("NGLOTQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            defaultCol.AddTextBoxColumn("SPECOUTPERCENTAGE", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            defaultCol.AddTextBoxColumn("RJRATIO", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric);
            defaultCol.AddTextBoxColumn("NGQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            defaultCol.AddTextBoxColumn("OKQTY", 80)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            #region WORST

            for (int siteNo = 1; siteNo < 11; siteNo++)
            {
                var worstGroup = grdMaster.View.AddGroupColumn("WORST" + siteNo);

                // 불량명
                worstGroup.AddTextBoxColumn("DEFECTNAME" + siteNo, 50)
                        .SetTextAlignment(TextAlignment.Left)
                        .SetLabel("DEFECTNAME");
                // 불량수
                worstGroup.AddTextBoxColumn("PCSDEFECTQTY" + siteNo, 50)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                    .SetLabel("PCSDEFECTQTY");
                // 불량율 
                /*
                worstGroup.AddTextBoxColumn("PCSDEFECTRATE" + siteNo, 50)
                    .SetTextAlignment(TextAlignment.Right)
                    .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                    .SetLabel("PCSDEFECTRATE");
                */
            }

            #endregion WORST

            var extGroup = grdMaster.View.AddGroupColumn("Etc");

            // 불량명
            extGroup.AddTextBoxColumn("EXTDEFECTNAME", 50)
                    .SetTextAlignment(TextAlignment.Left)
                    .SetLabel("DEFECTNAME");
            // 불량수
            extGroup.AddTextBoxColumn("EXTPCSDEFECTQTY", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric)
                .SetLabel("PCSDEFECTQTY");
            // 불량율
            /*
            extGroup.AddTextBoxColumn("EXTPCSDEFECTRATE", 50)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric)
                .SetLabel("PCSDEFECTRATE");
            */

            grdMaster.View.PopulateColumns();
            grdMaster.View.BestFitColumns(true);
            //SetFooterSummary();

            //grdMaster.View.OptionsCustomization.AllowColumnMoving = false;
            //grdMaster.View.OptionsView.ShowFooter = true;
            //grdMaster.View.FooterPanelHeight = 10;
            //grdMaster.ShowStatusBar = false;
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

            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            #region 기간 검색형 전환 처리 
            if (!(values["P_SENDTIME_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_SENDTIME_PERIODFR"]);
                values.Remove("P_SENDTIME_PERIODFR");
                values.Add("P_SENDTIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_SENDTIME_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_SENDTIME_PERIODTO"]);
                values.Remove("P_SENDTIME_PERIODTO");
                actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_SENDTIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }
           
            #endregion
            DataTable dt = await SqlExecuter.QueryAsync("SelectShipmentInspLRRModelList", "10001", values);

            grdMaster.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                grdMaster.View.FocusedRowHandle = 0;
                grdMaster.View.SelectRow(0);
            }

            ChartPeriodBainding();
            ChartProductBainding();
            ChartProductRJBainding();
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            //InitializeConditionPopup_Plant();
            InitializeConditionPopup_Customer();

            InitializeConditionPopup_Product();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();


        }
        /// <summary>
        /// 사이트 조회조건
        /// </summary>
        private void InitializeConditionPopup_Plant()
        {
            this.Conditions.AddComboBox("p_plantId", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetDefault(UserInfo.Current.Plant)
                .SetValidationIsRequired()
                .SetLabel("PLANT")
                .SetPosition(0.1);
        }
        /// <summary>
        /// 품목 조회조건
        /// </summary>
        private void InitializeConditionPopup_Product()
        {
            // 팝업 컬럼설정
            var productPopup = Conditions.AddSelectPopup("p_productdefId", new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFIDVERSION")
               .SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(600, 800)
               .SetLabel("PRODUCT")
               .SetPopupAutoFillColumns("PRODUCTDEFNAME")
               .SetPopupResultCount(1)
               .SetPosition(1.2);

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetValidationKeyColumn();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 220);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFIDVERSION", 100)
                .SetIsHidden();
        }
        /// <summary>
        /// 고객사 조회조건
        /// </summary>
        private void InitializeConditionPopup_Customer()
        {
            // 팝업 컬럼설정
            var areaPopup = Conditions.AddSelectPopup("p_customerId", new SqlQuery("GetCustomerList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
               .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("CUSTOMER")
               .SetPopupResultCount(1)
               .SetPosition(1.4);

            // 팝업 조회조건
            areaPopup.Conditions.AddTextBox("TXTCUSTOMERID");

            // 팝업 그리드
            areaPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150)
                .SetValidationKeyColumn();
            areaPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
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

        /// <summary>
        /// 일자멸 적용
        /// </summary>
        /// <param name="parPlotData"></param>
        public void ChartPeriodBainding()
        {
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            if (!(values["P_SENDTIME_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_SENDTIME_PERIODFR"]);
                values.Remove("P_SENDTIME_PERIODFR");
                values.Add("P_SENDTIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_SENDTIME_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_SENDTIME_PERIODTO"]);
                values.Remove("P_SENDTIME_PERIODTO");
                actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_SENDTIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }

            DataTable parPlotData = SqlExecuter.Query("SelectShipmentInspLRRModelPeriodChart", "10001", values);

            ctPariod.DataSource = parPlotData;
            //Plot Chart
            ctPariod.Series["Series01Value"].ValueDataMembers.Clear();
            ctPariod.Series["Series01Value"].ChangeView(ViewType.StackedBar);
            ctPariod.Series["Series01Value"].ArgumentDataMember = "WORKENDTIME";
            ctPariod.Series["Series01Value"].ValueDataMembers.AddRange(new string[] { "ENTERLOTQTY" });
            ctPariod.Series["Series01Value"].CrosshairLabelPattern = Language.Get("ENTERLOTQTY") + " : {V}";

            ctPariod.Series["Series01Value"].Visible = true;
            //ctPariod.Series["Series03Value"].View.Color = Color.DarkGray;

            ctPariod.Series["Series02Value"].ValueDataMembers.Clear();
            ctPariod.Series["Series02Value"].ChangeView(ViewType.Bar);
            ctPariod.Series["Series02Value"].ArgumentDataMember = "WORKENDTIME";
            ctPariod.Series["Series02Value"].ValueDataMembers.AddRange(new string[] { "NGLOTQTY" });
            ctPariod.Series["Series02Value"].CrosshairLabelPattern = Language.Get("NGLOTQTY") + " : {V}";
            ctPariod.Series["Series02Value"].Visible = true;
            //ctPariod.Series["Series03Value"].View.Color = Color.DarkGray;

            ctPariod.Series["Series03Value"].ValueDataMembers.Clear();
            ctPariod.Series["Series03Value"].ChangeView(ViewType.Line);
            LineSeriesView lineView = (LineSeriesView)ctPariod.Series["Series03Value"].View;
            lineView.MarkerVisibility = DefaultBoolean.True;
            ctPariod.Series["Series03Value"].ArgumentDataMember = "WORKENDTIME";
            ctPariod.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "SPECOUTPERCENTAGE" });
            ctPariod.Series["Series03Value"].CrosshairLabelPattern = Language.Get("SPECOUTPERCENTAGE") + " : {V}"; ;
            ctPariod.Series["Series03Value"].Visible = true;

            //ctPariod.Legends[0].CustomItems[0].MarkerColor = ctPariod.Series[0].Points[1].Color;
        }

        /// <summary>
        /// 품목별 적용
        /// </summary>
        /// <param name="parPlotData"></param>
        public void ChartProductBainding()
        {
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            if (!(values["P_SENDTIME_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_SENDTIME_PERIODFR"]);
                values.Remove("P_SENDTIME_PERIODFR");
                values.Add("P_SENDTIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_SENDTIME_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_SENDTIME_PERIODTO"]);
                values.Remove("P_SENDTIME_PERIODTO");
                actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_SENDTIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }

            DataTable parPlotData = SqlExecuter.Query("SelectShipmentInspLRRModelProductChart", "10001", values);

            ctProduct.DataSource = parPlotData;
            //Plot Chart
            ctProduct.Series["Series01Value"].ValueDataMembers.Clear();
            ctProduct.Series["Series01Value"].ChangeView(ViewType.StackedBar);
            ctProduct.Series["Series01Value"].ArgumentDataMember = "PRODUCTDEFNAME";
            ctProduct.Series["Series01Value"].ValueDataMembers.AddRange(new string[] { "ENTERLOTQTY" });
            ctProduct.Series["Series01Value"].CrosshairLabelPattern = Language.Get("ENTERLOTQTY") + " : {V}";

            ctProduct.Series["Series01Value"].Visible = true;
            //ctPariod.Series["Series03Value"].View.Color = Color.DarkGray;

            ctProduct.Series["Series02Value"].ValueDataMembers.Clear();
            ctProduct.Series["Series02Value"].ChangeView(ViewType.Bar);
            ctProduct.Series["Series02Value"].ArgumentDataMember = "PRODUCTDEFNAME";
            ctProduct.Series["Series02Value"].ValueDataMembers.AddRange(new string[] { "NGLOTQTY" });
            ctProduct.Series["Series02Value"].CrosshairLabelPattern = Language.Get("NGLOTQTY") + " : {V}";
            ctProduct.Series["Series02Value"].Visible = true;
            //ctPariod.Series["Series03Value"].View.Color = Color.DarkGray;

            ctProduct.Series["Series03Value"].ValueDataMembers.Clear();
            ctProduct.Series["Series03Value"].ChangeView(ViewType.Point);
            //LineSeriesView lineView = (LineSeriesView)ctPariod.Series["Series03Value"].View;
            //lineView.MarkerVisibility = DefaultBoolean.True;
            ctProduct.Series["Series03Value"].ArgumentDataMember = "PRODUCTDEFNAME";
            ctProduct.Series["Series03Value"].ValueDataMembers.AddRange(new string[] { "SPECOUTPERCENTAGE" });
            ctProduct.Series["Series03Value"].CrosshairLabelPattern = Language.Get("SPECOUTPERCENTAGE") + " : {V}"; ;
            ctProduct.Series["Series03Value"].Visible = true;

            //ctPariod.Legends[0].CustomItems[0].MarkerColor = ctPariod.Series[0].Points[1].Color;
        }

        /// <summary>
        /// 품목별 RJ 적용
        /// </summary>
        /// <param name="parPlotData"></param>
        public void ChartProductRJBainding()
        {
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            if (!(values["P_SENDTIME_PERIODFR"].ToString().Equals("")))
            {
                DateTime actualDateFr = Convert.ToDateTime(values["P_SENDTIME_PERIODFR"]);
                values.Remove("P_SENDTIME_PERIODFR");
                values.Add("P_SENDTIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", actualDateFr));
            }
            if (!(values["P_SENDTIME_PERIODTO"].ToString().Equals("")))
            {
                DateTime actualDateTo = Convert.ToDateTime(values["P_SENDTIME_PERIODTO"]);
                values.Remove("P_SENDTIME_PERIODTO");
                actualDateTo = actualDateTo.AddDays(1);
                values.Add("P_SENDTIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", actualDateTo));
            }

            DataTable parPlotData = SqlExecuter.Query("SelectShipmentInspLRRModelProductRJChart", "10001", values);

            ctRjRatio.DataSource = parPlotData;
            //Plot Chart
            ctRjRatio.Series["Series01Value"].ValueDataMembers.Clear();
            ctRjRatio.Series["Series01Value"].ChangeView(ViewType.Pie);
            ctRjRatio.Series["Series01Value"].ArgumentDataMember = "PRODUCTDEFNAME";
            ctRjRatio.Series["Series01Value"].ValueDataMembers.AddRange(new string[] { "RJRATIO" });
            ctRjRatio.Series["Series01Value"].CrosshairLabelPattern = Language.Get("RJRATIO") + " : {V}";
            ctRjRatio.Series["Series01Value"].LegendTextPattern = "{A}";

            ctRjRatio.Series["Series01Value"].Visible = true;

            //ctPariod.Legends[0].CustomItems[0].MarkerColor = ctPariod.Series[0].Points[1].Color;
        }

        #endregion

        private void ShipmentInspLRRModelList_Load(object sender, EventArgs e)
        {
            ctPariod.Legends[0].CustomItems[0].Text = Language.Get("ENTERLOTQTY");            
            ctPariod.Legends[0].CustomItems[1].Text = Language.Get("NGLOTQTY");
            ctPariod.Legends[0].CustomItems[2].Text = Language.Get("SPECOUTPERCENTAGE");

            ctPariod.Legends[0].CustomItems[0].MarkerColor = Color.FromArgb(0, 112, 192);
            ctPariod.Legends[0].CustomItems[1].MarkerColor = Color.FromArgb(240, 0, 0);
            ctPariod.Legends[0].CustomItems[2].MarkerColor = Color.FromArgb(0, 0, 0);

            ctProduct.Legends[0].CustomItems[0].Text = Language.Get("ENTERLOTQTY");
            ctProduct.Legends[0].CustomItems[1].Text = Language.Get("NGLOTQTY");
            ctProduct.Legends[0].CustomItems[2].Text = Language.Get("SPECOUTPERCENTAGE");

            ctProduct.Legends[0].CustomItems[0].MarkerColor = Color.FromArgb(0, 112, 192);
            ctProduct.Legends[0].CustomItems[1].MarkerColor = Color.FromArgb(240, 0, 0);
            ctProduct.Legends[0].CustomItems[2].MarkerColor = Color.FromArgb(0, 0, 0);
        }

        /// <summary>
        /// Main Grid에 Layer에 Footer에 Summary 처리
        /// </summary>
        private void SetFooterSummary()
        {
            grdMaster.View.Columns["ENTERLOTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["ENTERLOTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["NGLOTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["NGLOTQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["RJRATIO"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["RJRATIO"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["NGQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["NGQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdMaster.View.Columns["OKQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaster.View.Columns["OKQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
        }
    }
}
