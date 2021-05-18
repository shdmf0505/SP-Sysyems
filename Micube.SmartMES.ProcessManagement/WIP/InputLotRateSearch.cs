#region using

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using Micube.SmartMES.Commons.Controls;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;
using Micube.Framework.SmartControls.Grid.BandedGrid;


#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > 투입율현황
    /// 업  무  설  명  : 투입율현황을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-17
    /// 수  정  이  력  : 2019-10-24    황유성     그리드 컬럼명 변경
    /// 
    /// </summary>

    public partial class InputLotRateSearch : SmartConditionManualBaseForm
    {
        #region 생성자

        public InputLotRateSearch()
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
            InitializeInputDayGrid();
            InitializeModelGrid();
            InitializeCustomerGrid();
            InitializationLotInputTypeSummaryRow();
            InitializationLotProductSummaryRow();
            InitializationDaySummaryRow();
            InitializationByCustomerSummaryRow();
            InitializationByProductSummaryRow();


        }

        #region 투입일 투입구분 summary
        private void InitializationLotInputTypeSummaryRow()
        {
            grdInputDaySummaryByLotInputType.View.Columns["LOTINPUTTYPE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["LOTINPUTTYPE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));


            grdInputDaySummaryByLotInputType.View.Columns["PLANQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["PLANQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["PLANQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["PLANQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["PLANQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["PLANQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDaySummaryByLotInputType.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.DisplayFormat = "{0:f2}%";


            grdInputDaySummaryByLotInputType.View.OptionsView.ShowFooter = true;
            grdInputDaySummaryByLotInputType.ShowStatusBar = false;
        }
        #endregion

        #region 투입일 품목별 summary
        private void InitializationLotProductSummaryRow()
        {
            grdInputDaySummaryByProduct.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));


            grdInputDaySummaryByProduct.View.Columns["PLANQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["PLANQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["PLANQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["PLANQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["PLANQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["PLANQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDaySummaryByProduct.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDaySummaryByProduct.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDaySummaryByProduct.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDaySummaryByProduct.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDaySummaryByProduct.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDaySummaryByProduct.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDaySummaryByProduct.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDaySummaryByProduct.View.OptionsView.ShowFooter = true;
            grdInputDaySummaryByProduct.ShowStatusBar = false;
        }
        #endregion

        #region 투입일 summary
        private void InitializationDaySummaryRow()
        {
            grdInputDay.View.Columns["PRODUCTIONTYPE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["PRODUCTIONTYPE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));


            grdInputDay.View.Columns["PLANQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["PLANQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["PLANQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["PLANQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["PLANQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["PLANQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["INPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["INPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdInputDay.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdInputDay.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDay.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDay.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDay.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDay.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdInputDay.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdInputDay.View.OptionsView.ShowFooter = true;
            grdInputDay.ShowStatusBar = false;
        }
        #endregion

        #region 품목탭 summary
        private void InitializationByProductSummaryRow()
        {
            grdModel.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));


            grdModel.View.Columns["PLANQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["PLANQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["PLANQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["PLANQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["PLANQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["PLANQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["INPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["INPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdModel.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdModel.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdModel.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdModel.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdModel.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdModel.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdModel.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdModel.View.OptionsView.ShowFooter = true;
            grdModel.ShowStatusBar = false;
        }
        #endregion

        #region 고객사별 summary
        private void InitializationByCustomerSummaryRow()
        {
            grdCustomer.View.Columns["CUSTOMERNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["CUSTOMERNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));


            grdCustomer.View.Columns["PLANQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["PLANQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["PLANQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["PLANQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["PLANQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["PLANQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["INPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["INPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["INPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["INPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["INPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["OVERINPUTQTY_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["OVERINPUTQTY_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCustomer.View.Columns["OVERINPUTQTY_AMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdCustomer.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdCustomer.View.Columns["OVERINPUTRATIO_PNL"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdCustomer.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdCustomer.View.Columns["OVERINPUTRATIO_PCS"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdCustomer.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            grdCustomer.View.Columns["OVERINPUTRATIO_AMOUNT"].SummaryItem.DisplayFormat = "{0:f2}%";

            grdCustomer.View.OptionsView.ShowFooter = true;
            grdCustomer.ShowStatusBar = false;
        }
        #endregion

        /// <summary>        
        /// 투입일 탭 그리드
        /// </summary>
        private void InitializeInputDayGrid()
        {
            #region 투입율 현황

            grdInputDay.ShowButtonBar = true;

            // 투입정보
            var grpInputDayInfo = grdInputDay.View.AddGroupColumn("");
            grdInputDay.GridButtonItem = GridButtonItem.Export;
            // 투입일
            grpInputDayInfo.AddTextBoxColumn("INPUTDAY", 90)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // 투입구분
            grpInputDayInfo.AddTextBoxColumn("LOTINPUTTYPE", 90)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //투입구분코드
            grpInputDayInfo.AddTextBoxColumn("LOTINPUTTYPECODE", 90)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetIsHidden();
            // 양산구분
            grpInputDayInfo.AddTextBoxColumn("PRODUCTIONTYPE", 90)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("LOTTYPE")
                .SetIsReadOnly();
            // 양산구분코드
            grpInputDayInfo.AddTextBoxColumn("PRODUCTIONTYPECODE", 90)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("LOTTYPE")
                .SetIsReadOnly()
                .SetIsHidden();

            // 수주량
            var grpInputDayPlan = grdInputDay.View.AddGroupColumn("PLANQTY");
            // PNL
            grpInputDayPlan.AddSpinEditColumn("PLANQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDayPlan.AddSpinEditColumn("PLANQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDayPlan.AddSpinEditColumn("PLANQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();


            // 투입량
            var grpInputDayInput = grdInputDay.View.AddGroupColumn("ASSIGNEDUNITS");
            // PNL
            grpInputDayInput.AddSpinEditColumn("INPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDayInput.AddSpinEditColumn("INPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDayInput.AddSpinEditColumn("INPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 과투입량
            var grpInputDayOverInput = grdInputDay.View.AddGroupColumn("OVERINPUTQTY");
            // PNL
            grpInputDayOverInput.AddSpinEditColumn("OVERINPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDayOverInput.AddSpinEditColumn("OVERINPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDayOverInput.AddSpinEditColumn("OVERINPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 과투입율(%)
            var grpInputDayOverInputRatio = grdInputDay.View.AddGroupColumn("OVERINPUTRATE");
            // PNL
            grpInputDayOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PNL", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDayOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PCS", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDayOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_AMOUNT", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            grdInputDay.View.PopulateColumns();

            #endregion

            #region SUMMARY
            grdInputDaySummaryByLotInputType.GridButtonItem = GridButtonItem.Export;
            grdInputDaySummaryByLotInputType.View.OptionsView.ShowFooter = false;

            // 투입정보
            var grpInputDaySummaryInfo = grdInputDaySummaryByLotInputType.View.AddGroupColumn("");
            // 양산구분
            grpInputDaySummaryInfo.AddTextBoxColumn("PRODUCTIONTYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // 투입구분
            grpInputDaySummaryInfo.AddTextBoxColumn("LOTINPUTTYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();

            // 수주량
            var grpInputDaySummaryPlan = grdInputDaySummaryByLotInputType.View.AddGroupColumn("PLANQTY");
            // PNL
            grpInputDaySummaryPlan.AddSpinEditColumn("PLANQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryPlan.AddSpinEditColumn("PLANQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryPlan.AddSpinEditColumn("PLANQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();


            // 투입량
            var grpInputDaySummaryInput = grdInputDaySummaryByLotInputType.View.AddGroupColumn("ASSIGNEDUNITS");
            // PNL
            grpInputDaySummaryInput.AddSpinEditColumn("INPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryInput.AddSpinEditColumn("INPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryInput.AddSpinEditColumn("INPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();


            // 과투입량
            var grpInputDaySummaryOverInput = grdInputDaySummaryByLotInputType.View.AddGroupColumn("OVERINPUTQTY");
            // PNL
            grpInputDaySummaryOverInput.AddSpinEditColumn("OVERINPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryOverInput.AddSpinEditColumn("OVERINPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryOverInput.AddSpinEditColumn("OVERINPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();


            // 과투입율(%)
            var grpInputDaySummaryOverInputRatio = grdInputDaySummaryByLotInputType.View.AddGroupColumn("OVERINPUTRATE");
            // PNL
            grpInputDaySummaryOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PNL", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PCS", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_AMOUNT", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            grdInputDaySummaryByLotInputType.View.PopulateColumns();

            #endregion

            #region SUMMARY 2
            grdInputDaySummaryByProduct.GridButtonItem = GridButtonItem.Export;
            grdInputDaySummaryByProduct.View.OptionsView.ShowFooter = false;

            // 투입정보
            var grpInputDaySummaryByProductInfo = grdInputDaySummaryByProduct.View.AddGroupColumn("");
            // 투입구분
            grpInputDaySummaryByProductInfo.AddTextBoxColumn("LOTINPUTTYPE", 70)
                .SetTextAlignment(TextAlignment.Center)
                 .SetIsReadOnly()
                 .SetIsHidden()
                 ;
            // 품목코드
            grpInputDaySummaryByProductInfo.AddTextBoxColumn("PRODUCTDEFID", 110)
                 .SetIsReadOnly();
            //rev
            grpInputDaySummaryByProductInfo.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                 .SetLabel("ITEMVERSION")
                 .SetTextAlignment(TextAlignment.Center)
                 .SetIsReadOnly();
            // 품목명
            grpInputDaySummaryByProductInfo.AddTextBoxColumn("PRODUCTDEFNAME", 200)
                 .SetIsReadOnly();
            // 양산구분
            grpInputDaySummaryByProductInfo.AddTextBoxColumn("PRODUCTIONTYPE", 70)
                .SetLabel("LOTTYPE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetIsHidden();

            // 수주량
            var grpInputDaySummaryByProductPlan = grdInputDaySummaryByProduct.View.AddGroupColumn("PLANQTY");
            // PNL
            grpInputDaySummaryByProductPlan.AddSpinEditColumn("PLANQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryByProductPlan.AddSpinEditColumn("PLANQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryByProductPlan.AddSpinEditColumn("PLANQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();


            // 투입량
            var grpInputDaySummaryByProductInput = grdInputDaySummaryByProduct.View.AddGroupColumn("ASSIGNEDUNITS");
            // PNL
            grpInputDaySummaryByProductInput.AddSpinEditColumn("INPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryByProductInput.AddSpinEditColumn("INPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryByProductInput.AddSpinEditColumn("INPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();


            // 과투입량
            var grpInputDaySummaryByProductOverInput = grdInputDaySummaryByProduct.View.AddGroupColumn("OVERINPUTQTY");
            // PNL
            grpInputDaySummaryByProductOverInput.AddSpinEditColumn("OVERINPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryByProductOverInput.AddSpinEditColumn("OVERINPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryByProductOverInput.AddSpinEditColumn("OVERINPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();


            // 과투입율(%)
            var grpInputDaySummaryByProductOverInputRatio = grdInputDaySummaryByProduct.View.AddGroupColumn("OVERINPUTRATE");
            // PNL
            grpInputDaySummaryByProductOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PNL", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpInputDaySummaryByProductOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PCS", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpInputDaySummaryByProductOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_AMOUNT", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            grdInputDaySummaryByProduct.View.PopulateColumns();

            #endregion
        }

        /// <summary>
        /// 모델별 탭 그리드
        /// </summary>
        private void InitializeModelGrid()
        {
            grdModel.GridButtonItem = GridButtonItem.Export;

            // 기본정보
            var grpModelInfo = grdModel.View.AddGroupColumn("");
            // 투입구분
            grpModelInfo.AddTextBoxColumn("LOTINPUTTYPE", 70)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // 품목코드
            grpModelInfo.AddTextBoxColumn("PRODUCTDEFID", 140)
                .SetIsReadOnly();
            // 품목명
            grpModelInfo.AddTextBoxColumn("PRODUCTDEFNAME", 280)
                .SetIsReadOnly();

            // 수주량
            var grpModelPlan = grdModel.View.AddGroupColumn("PLANQTY");
            // PNL
            grpModelPlan.AddSpinEditColumn("PLANQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpModelPlan.AddSpinEditColumn("PLANQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpModelPlan.AddSpinEditColumn("PLANQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 투입량
            var grpModelInput = grdModel.View.AddGroupColumn("ASSIGNEDUNITS");
            // PNL
            grpModelInput.AddSpinEditColumn("INPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpModelInput.AddSpinEditColumn("INPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpModelInput.AddSpinEditColumn("INPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 과투입량
            var grpModelOverInput = grdModel.View.AddGroupColumn("OVERINPUTQTY");
            // PNL
            grpModelOverInput.AddSpinEditColumn("OVERINPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpModelOverInput.AddSpinEditColumn("OVERINPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpModelOverInput.AddSpinEditColumn("OVERINPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 과투입율(%)
            var grpModelOverInputRatio = grdModel.View.AddGroupColumn("OVERINPUTRATE");
            // PNL
            grpModelOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PNL", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpModelOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PCS", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpModelOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_AMOUNT", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            grdModel.View.PopulateColumns();
        }

        /// <summary>
        /// 고객사별 탭 그리드
        /// </summary>
        private void InitializeCustomerGrid()
        {
            grdCustomer.GridButtonItem = GridButtonItem.Export;

            // 기본정보
            var grpCustomerInfo = grdCustomer.View.AddGroupColumn("");
            // 고객사명
            grpCustomerInfo.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 고객사명
            grpCustomerInfo.AddTextBoxColumn("CUSTOMERNAME", 140)
                .SetIsReadOnly();

            // 수주량
            var grpCustomerPlan = grdCustomer.View.AddGroupColumn("PLANQTY");
            // PNL
            grpCustomerPlan.AddSpinEditColumn("PLANQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpCustomerPlan.AddSpinEditColumn("PLANQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpCustomerPlan.AddSpinEditColumn("PLANQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 투입량
            var grpCustomerInput = grdCustomer.View.AddGroupColumn("ASSIGNEDUNITS");
            // PNL
            grpCustomerInput.AddSpinEditColumn("INPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpCustomerInput.AddSpinEditColumn("INPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpCustomerInput.AddSpinEditColumn("INPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 과투입량
            var grpCustomerOverInput = grdCustomer.View.AddGroupColumn("OVERINPUTQTY");
            // PNL
            grpCustomerOverInput.AddSpinEditColumn("OVERINPUTQTY_PNL", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpCustomerOverInput.AddSpinEditColumn("OVERINPUTQTY_PCS", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpCustomerOverInput.AddSpinEditColumn("OVERINPUTQTY_AMOUNT", 100)
                .SetDisplayFormat("{0:#,##0}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            // 과투입율(%)
            var grpCustomerOverInputRatio = grdCustomer.View.AddGroupColumn("OVERINPUTRATE");
            // PNL
            grpCustomerOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PNL", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PNL")
                .SetIsReadOnly();
            // PCS
            grpCustomerOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_PCS", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("PCS")
                .SetIsReadOnly();
            // 금액
            grpCustomerOverInputRatio.AddSpinEditColumn("OVERINPUTRATIO_AMOUNT", 100)
                .SetDisplayFormat("{0:P2}")
                .SetLabel("CLAIMAMOUNT")
                .SetIsReadOnly();

            grdCustomer.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdInputDay.View.FocusedRowChanged += View_FocusedRowChanged;

            // Footer Custom Draw
            grdInputDaySummaryByLotInputType.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdInputDay.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdModel.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdCustomer.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdInputDaySummaryByProduct.View.CustomDrawFooterCell += View_CustomDrawFooterCell;

            // Footer Custom Calculate
            grdInputDaySummaryByLotInputType.View.CustomSummaryCalculate += View_CustomSummaryCalculate;
            grdInputDay.View.CustomSummaryCalculate += View_CustomSummaryCalculate;
            grdModel.View.CustomSummaryCalculate += View_CustomSummaryCalculate;
            grdCustomer.View.CustomSummaryCalculate += View_CustomSummaryCalculate;
            grdInputDaySummaryByProduct.View.CustomSummaryCalculate += View_CustomSummaryCalculate;

            grdModel.View.RowStyle += View_RowStyle;
            grdCustomer.View.RowStyle += View_RowStyle1;
            grdInputDaySummaryByLotInputType.View.RowStyle += View_RowStyle2;
            grdInputDaySummaryByProduct.View.RowStyle += View_RowStyle3;

        }

        private void View_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                SmartBandedGridView view = sender as SmartBandedGridView;

                GridSummaryItem item = e.Item as GridSummaryItem;


                double planPnl = Format.GetDouble(view.Columns["PLANQTY_PNL"].SummaryText, 0);
                double planPCS = Format.GetDouble(view.Columns["PLANQTY_PCS"].SummaryText, 0);
                double planAmount = Format.GetDouble(view.Columns["PLANQTY_AMOUNT"].SummaryText, 0);

                double InputPnl = Format.GetDouble(view.Columns["INPUTQTY_PNL"].SummaryText, 0);
                double InputPCS = Format.GetDouble(view.Columns["INPUTQTY_PCS"].SummaryText, 0);
                double InputAmount = Format.GetDouble(view.Columns["INPUTQTY_AMOUNT"].SummaryText, 0);

                if (item.FieldName.Equals("OVERINPUTRATIO_PNL"))
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        double overRatePnl = ((InputPnl / planPnl) - 1) * 100;

                        e.TotalValue = overRatePnl;
                    }
                }
                else if (item.FieldName.Equals("OVERINPUTRATIO_PCS"))
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        double overRatePcs = ((InputPCS / planPCS) - 1) * 100;

                        e.TotalValue = overRatePcs;
                    }
                }
                else if (item.FieldName.Equals("OVERINPUTRATIO_AMOUNT"))
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        double overRateAmount = ((InputAmount / planAmount) - 1) * 100;

                        e.TotalValue = overRateAmount;
                    }
                }
            }
        }

        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            /*
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (row["PRODUCTDEFID"] == DBNull.Value)
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            */
        }

        private void View_RowStyle1(object sender, RowStyleEventArgs e)
        {
            /*
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (row["CUSTOMERID"] == DBNull.Value)
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            */
        }

        private void View_RowStyle2(object sender, RowStyleEventArgs e)
        {
            /*
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (row["LOTINPUTTYPE"] == DBNull.Value)
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            */
        }

        private void View_RowStyle3(object sender, RowStyleEventArgs e)
        {
            /*
            GridView view = sender as GridView;
            if (e.RowHandle < 0) return;

            DataRow row = (DataRow)view.GetDataRow(e.RowHandle);

            if (row["INPUTDAY"] == DBNull.Value)
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            */
        }

        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 9F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            DataRow dr = grdInputDay.View.GetFocusedDataRow();
            if(dr == null)
            {
                return;
            }
            string InputDay = Format.GetFullTrimString(dr["INPUTDAY"]);
            string InputType = Format.GetFullTrimString(dr["LOTINPUTTYPECODE"]);
            string ProductionType = Format.GetFullTrimString(dr["PRODUCTIONTYPECODE"]);

            dic.Add("P_PRODUCTIONTYPE", ProductionType);
            dic.Add("P_LOTINPUTTYPE", InputType);
            dic.Add("P_INPUTDAY", InputDay);
            dic.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable inputRatioByInputDaySummaryByProduct =  SqlExecuter.Query("SelectInputLotRateByInputDateSummaryByProduct", "10002", dic);
            grdInputDaySummaryByProduct.DataSource = inputRatioByInputDaySummaryByProduct;
        }

        #endregion

        #region 툴바

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10);
            values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10);

            switch (tabInputLotRateSearch.SelectedTabPage.Name)
            {
                case "pagInputDay":
                    DataTable inputRatioByInputDay = await SqlExecuter.QueryAsync("SelectInputLotRateByInputDate", "10001", values);
                    grdInputDay.DataSource = inputRatioByInputDay;


                    DataTable inputRatioByInputDaySummaryByLotInputType = await SqlExecuter.QueryAsync("SelectInputLotRateByInputDateSummaryByLotInputType", "10002", values);
                    grdInputDaySummaryByLotInputType.DataSource = inputRatioByInputDaySummaryByLotInputType;

                    /*
                    DataTable inputRatioByInputDaySummaryByProduct = await SqlExecuter.QueryAsync("SelectInputLotRateByInputDateSummaryByProduct", "10001", values);
                    grdInputDaySummaryByProduct.DataSource = inputRatioByInputDaySummaryByProduct;
                    */
                    if (inputRatioByInputDay.Rows.Count == 0 && inputRatioByInputDaySummaryByLotInputType.Rows.Count == 0 )//&& inputRatioByInputDaySummaryByProduct.Rows.Count == 0)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    break;
                case "pagModel":
                    DataTable inputRatioByModel = await SqlExecuter.QueryAsync("SelectInputLotRateByProduct", "10001", values);
                    grdModel.DataSource = inputRatioByModel;

                    if (inputRatioByModel.Rows.Count == 0)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    break;
                case "pagCustomer":
                    DataTable inputRatioByCustomer = await SqlExecuter.QueryAsync("SelectInputLotRateByCustomer", "10001", values);
                    grdCustomer.DataSource = inputRatioByCustomer;

                    if (inputRatioByCustomer.Rows.Count == 0)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    break;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            InitializeCondition_ProductPopup();
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(1.5)
                .SetPopupResultCount(0);

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private decimal GetSafeDecimal(object obj)
        {
            if(obj == DBNull.Value)
            {
                return 0;
            }
            return Format.GetDecimal(obj);
        }
        #endregion
    }
}
