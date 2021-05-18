#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 재공관리 > 일자별 실적재공 조회
    /// 업  무  설  명  : 일자별 공정별/품목별 실적 및 재공내역을 조회한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-09-06
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class WIPPerDaily : SmartConditionManualBaseForm
    {
        public WIPPerDaily()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeEvent();
            InitializeGridByProcess();
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

			// 품목코드
			InitializeCondition_ProductPopup();
			//CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3.5, false, Conditions);

            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREA", 4.5, true, Conditions, false, false);

            // 공정
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 6.5, false, Conditions);

            // Rev별 집계 여부
            Conditions.AddComboBox("P_GROUPBYREVISION", new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("GROUPBYREVISION").SetDefault("Y").SetPosition(6.5);
        }

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPosition(3.5)
				.SetPopupResultCount(1)
				.SetPopupApplySelection((selectRow, gridRow) => {
					/*
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
					*/
				});

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
		/// 품목버전 별 그룹핑 여부 변경
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GroupByRevision_EditValueChanged(object sender, EventArgs e)
        {
            SmartComboBox groupByRevision = Conditions.GetControl<SmartComboBox>("P_GROUPBYREVISION");
            if (groupByRevision.EditValue.ToString() == "Y")
            {
                grdByProduct.View.Columns["PRODUCTDEFVERSION"].Visible = true;
            }
            else
            {
                grdByProduct.View.Columns["PRODUCTDEFVERSION"].Visible = false;
            }
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            SmartComboBox groupByRevision = Conditions.GetControl<SmartComboBox>("P_GROUPBYREVISION");
            groupByRevision.EditValueChanged += GroupByRevision_EditValueChanged;

            SetConditionVisiblility("P_GROUPBYREVISION", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
        }

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            tabControl.SelectedPageChanged += TabControl_SelectedPageChanged;
            grdByProcess.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdByProduct.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
        }

        /// <summary>
        /// 그리드 하단의 합계란에 색상을 입힌다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }

        /// <summary>
        /// 탭 변경 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (this.tabControl.SelectedTabPageIndex)
            {
                case 0: // 공정별
                    SetConditionVisiblility("P_GROUPBYREVISION", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                    break;
                case 1: // 품목별
                    SetConditionVisiblility("P_GROUPBYREVISION", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
                    break;
            }
        }

        #endregion

        #region 공정별 그리드 초기화

        /// <summary>        
        /// 공정별 그리드를 초기화
        /// </summary>
        private void InitializeGridByProcess()
        {
            grdByProcess.View.ClearColumns();
            grdByProcess.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdByProcess.GridButtonItem = GridButtonItem.Export;
            grdByProcess.View.SetIsReadOnly();
            grdByProcess.View.OptionsView.ShowFooter = true;

            var groupProcessArea = grdByProcess.View.AddGroupColumn("");
            groupProcessArea.AddTextBoxColumn("PROCESSSEGMENTID", 90)
                .SetIsHidden()
                .SetTextAlignment(TextAlignment.Center);                   // 공정ID
            groupProcessArea.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);  // 공정명
            groupProcessArea.AddTextBoxColumn("AREANAME", 160);            // 작업장명
        }

        /// <summary>
        /// 공정별 그리드에 일자별 컬럼들을 추가
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void AddDateColumnsToGridByProcess(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                var groupEachDay = grdByProcess.View.AddGroupColumn(day.ToString("yyyy-MM-dd"));
                var groupResult = groupEachDay.AddGroupColumn("FIGURE");

                // 실적 PCS
                ConditionItemTextBox rslt_pcs = groupResult.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_rslt_pcs", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                rslt_pcs.LanguageKey = "PCS";

                // 실적 PNL
                ConditionItemTextBox rslt_pnl = groupResult.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_rslt_pnl", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                rslt_pnl.LanguageKey = "PNL";

                var groupStock = groupEachDay.AddGroupColumn("WIPSTOCK");

                // 재공 PCS
                ConditionItemTextBox stock_pcs = groupStock.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_stock_pcs", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                stock_pcs.LanguageKey = "PCS";

                // 재공 PNL
                ConditionItemTextBox stock_pnl = groupStock.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_stock_pnl", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                stock_pnl.LanguageKey = "PNL";
            }
        }

        /// <summary>
        /// 공정별 그리드 하단에 총계 컬럼들 추가
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void AddSummaryColumnsToGridByProcess(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProcess.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            }

            grdByProcess.View.Columns["TOTAL_RSLT_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProcess.View.Columns["TOTAL_RSLT_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProcess.View.Columns["TOTAL_RSLT_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProcess.View.Columns["TOTAL_RSLT_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProcess.View.Columns["TOTAL_RSLT_PCS_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProcess.View.Columns["TOTAL_RSLT_PCS_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProcess.View.Columns["TOTAL_RSLT_PNL_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProcess.View.Columns["TOTAL_RSLT_PNL_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProcess.View.Columns["TOTAL_STOCK_PCS_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProcess.View.Columns["TOTAL_STOCK_PCS_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProcess.View.Columns["TOTAL_STOCK_PNL_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProcess.View.Columns["TOTAL_STOCK_PNL_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdByProcess.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProcess.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
        }

        /// <summary>
        /// 공정별 그리드 우측에 총계 컬럼들 추가
        /// </summary>
        private void AddTotalColumnsToGridByProcess()
        {
            var groupTotal = grdByProcess.View.AddGroupColumn("TOTAL");
            var groupTotalResult = groupTotal.AddGroupColumn("FIGURE");

            // 총계 실적 PCS
            ConditionItemTextBox rslt_pcs = groupTotalResult.AddTextBoxColumn("TOTAL_RSLT_PCS", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pcs.LanguageKey = "PCS";

            // 총계 실적 PNL
            ConditionItemTextBox rslt_pnl = groupTotalResult.AddTextBoxColumn("TOTAL_RSLT_PNL", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pnl.LanguageKey = "PNL";

            var groupAverage = grdByProcess.View.AddGroupColumn("AVERAGE");
            var groupAverageResult = groupAverage.AddGroupColumn("FIGURE");

            // 평균 실적 PCS
            ConditionItemTextBox rslt_pcs_avg = groupAverageResult.AddTextBoxColumn("TOTAL_RSLT_PCS_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pcs_avg.LanguageKey = "PCS";

            // 평균 실적 PNL
            ConditionItemTextBox rslt_pnl_avg = groupAverageResult.AddTextBoxColumn("TOTAL_RSLT_PNL_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pnl_avg.LanguageKey = "PNL";

            var groupAverageStock = groupAverage.AddGroupColumn("WIPSTOCK");

            // 평균 재공 PCS
            ConditionItemTextBox stock_pcs_avg = groupAverageStock.AddTextBoxColumn("TOTAL_STOCK_PCS_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            stock_pcs_avg.LanguageKey = "PCS";

            // 평균 재공 PNL
            ConditionItemTextBox stock_pnl_avg = groupAverageStock.AddTextBoxColumn("TOTAL_STOCK_PNL_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            stock_pnl_avg.LanguageKey = "PNL";
        }

        #endregion

        #region 품목별 그리드 초기화

        /// <summary>        
        /// 품목별 그리드를 초기화
        /// </summary>
        private void InitializeGridByProduct()
        {
            grdByProduct.View.ClearColumns();
            grdByProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdByProduct.GridButtonItem = GridButtonItem.Export;
            grdByProduct.View.SetIsReadOnly();
            grdByProduct.View.OptionsView.ShowFooter = true;

            var groupProcessArea = grdByProduct.View.AddGroupColumn("");
            groupProcessArea.AddTextBoxColumn("PRODUCTDEFID", 130)
                .SetTextAlignment(TextAlignment.Center);                    // 품목코드
            groupProcessArea.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);                    // 품목코드
            groupProcessArea.AddTextBoxColumn("PRODUCTDEFNAME", 240);       // 품목명
            groupProcessArea.AddTextBoxColumn("USERSEQUENCE", 70)
                .SetTextAlignment(TextAlignment.Center);                    // 공정순서
            groupProcessArea.AddTextBoxColumn("PROCESSSEGMENTNAME", 170);   // 공정명
            groupProcessArea.AddTextBoxColumn("AREANAME", 150);             // 작업장명
        }

        /// <summary>
        /// 품목별 그리드에 일자별 컬럼들 추가
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void AddDateColumnsToGridByProduct(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                var groupEachDay = grdByProduct.View.AddGroupColumn(day.ToString("yyyy-MM-dd"));
                var groupResult = groupEachDay.AddGroupColumn("FIGURE");

                // 실적 PCS
                ConditionItemTextBox rslt_pcs = groupResult.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_rslt_pcs", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                rslt_pcs.LanguageKey = "PCS";

                // 실적 PNL
                ConditionItemTextBox rslt_pnl = groupResult.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_rslt_pnl", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                rslt_pnl.LanguageKey = "PNL";

                var groupStock = groupEachDay.AddGroupColumn("WIPSTOCK");

                // 재공 PCS
                ConditionItemTextBox stock_pcs = groupStock.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_stock_pcs", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                stock_pcs.LanguageKey = "PCS";

                // 재공 PNL
                ConditionItemTextBox stock_pnl = groupStock.AddTextBoxColumn(day.ToString("yyyy-MM-dd") + "_stock_pnl", 80)
                    .SetDisplayFormat("#,##0")
                    .SetIsReadOnly()
                    .SetTextAlignment(TextAlignment.Right);
                stock_pnl.LanguageKey = "PNL";
            }
        }

        /// <summary>
        /// 품목별 그리드 하단에 총계 컬럼들 추가
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private void AddSummaryColumnsToGridByProduct(DateTime from, DateTime to)
        {
            foreach (DateTime day in EachDay(from, to))
            {
                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_RSLT_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";

                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";

                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdByProduct.View.Columns[day.ToString("yyyy-MM-dd") + "_STOCK_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            }

            grdByProduct.View.Columns["TOTAL_RSLT_PCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["TOTAL_RSLT_PCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProduct.View.Columns["TOTAL_RSLT_PNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["TOTAL_RSLT_PNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProduct.View.Columns["TOTAL_RSLT_PCS_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["TOTAL_RSLT_PCS_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProduct.View.Columns["TOTAL_RSLT_PNL_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["TOTAL_RSLT_PNL_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProduct.View.Columns["TOTAL_STOCK_PCS_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["TOTAL_STOCK_PCS_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProduct.View.Columns["TOTAL_STOCK_PNL_AVG"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["TOTAL_STOCK_PNL_AVG"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdByProduct.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
        }

        /// <summary>
        /// 품목별 그리드 우측에 총계 컬럼들 추가
        /// </summary>
        private void AddTotalColumnsToGridByProduct()
        {
            var groupTotal = grdByProduct.View.AddGroupColumn("TOTAL");
            var groupTotalResult = groupTotal.AddGroupColumn("FIGURE");

            // 총계 실적 PCS
            ConditionItemTextBox rslt_pcs = groupTotalResult.AddTextBoxColumn("TOTAL_RSLT_PCS", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pcs.LanguageKey = "PCS";

            // 총계 실적 PNL
            ConditionItemTextBox rslt_pnl = groupTotalResult.AddTextBoxColumn("TOTAL_RSLT_PNL", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pnl.LanguageKey = "PNL";

            var groupAverage = grdByProduct.View.AddGroupColumn("AVERAGE");
            var groupAverageResult = groupAverage.AddGroupColumn("FIGURE");

            // 평균 실적 PCS
            ConditionItemTextBox rslt_pcs_avg = groupAverageResult.AddTextBoxColumn("TOTAL_RSLT_PCS_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pcs_avg.LanguageKey = "PCS";

            // 평균 실적 PNL
            ConditionItemTextBox rslt_pnl_avg = groupAverageResult.AddTextBoxColumn("TOTAL_RSLT_PNL_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            rslt_pnl_avg.LanguageKey = "PNL";

            var groupAverageStock = groupAverage.AddGroupColumn("WIPSTOCK");

            // 평균 재공 PCS
            ConditionItemTextBox stock_pcs_avg = groupAverageStock.AddTextBoxColumn("TOTAL_STOCK_PCS_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            stock_pcs_avg.LanguageKey = "PCS";

            // 평균 재공 PNL
            ConditionItemTextBox stock_pnl_avg = groupAverageStock.AddTextBoxColumn("TOTAL_STOCK_PNL_AVG", 80)
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            stock_pnl_avg.LanguageKey = "PNL";
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
            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10);
            values["P_PERIOD_PERIODTO"] = DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)).AddDays(-1).ToString("yyyy-MM-dd");
            if (DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10)) >= DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)))
            {
                values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODFR"];
            }
            DataTable dtResult = new DataTable();

			//values.Add("P_PRODUCTDEFVERSION", "");
			if (string.IsNullOrWhiteSpace(Format.GetFullTrimString(values["P_PRODUCTDEFID"])))
            {
                values["P_PRODUCTDEFID"] = string.Empty;
				values["P_PRODUCTDEFVERSION"] = string.Empty;
			}
			else
			{
				values["P_PRODUCTDEFID"] = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text.Split('|')[0];
				values["P_PRODUCTDEFVERSION"] = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text.Split('|')[1];
			}

            if (values["P_PROCESSSEGMENTID"] == null)
            {
                values["P_PROCESSSEGMENTID"] = string.Empty;
            }
            values.Add("P_USERID", UserInfo.Current.Id);

            // NOTE : 인터플렉스는 담당공장 조회조건이 없음. 프로시저 호출 시 오류발생 하지 않도록 값 추가
            if(!values.ContainsKey("P_OWNERFACTORY"))
            {
                values.Add("P_OWNERFACTORY", "");
            }
            int index = tabControl.SelectedTabPageIndex;
            switch (index)
            {
                case 0: // 공정별
                    values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("P_PLANTID", UserInfo.Current.Plant);

                    InitializeGridByProcess();
                    AddDateColumnsToGridByProcess(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                        , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));
                    AddTotalColumnsToGridByProcess();
                    grdByProcess.View.PopulateColumns();

                    AddSummaryColumnsToGridByProcess(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                        , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));

                    grdByProcess.View.FixColumn(new string[] { "PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "AREANAME" });

                    dtResult = this.Procedure("usp_wip_selectdailyresultandstockbyprocesspersite", values);
                    grdByProcess.DataSource = dtResult;
                    break;
                case 1: // 품목별
                    values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                    values.Add("P_PLANTID", UserInfo.Current.Plant);

                    InitializeGridByProduct();
                    AddDateColumnsToGridByProduct(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                        , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));
                    AddTotalColumnsToGridByProduct();
                    grdByProduct.View.PopulateColumns();

                    AddSummaryColumnsToGridByProduct(DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString())
                        , DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString()));

                    grdByProduct.View.FixColumn(new string[] { "PRODUCTDEFID", "PRODUCTDEFNAME", "USERSEQUENCE", "PROCESSSEGMENTNAME", "AREANAME" });

                    SmartComboBox groupByRevision = Conditions.GetControl<SmartComboBox>("P_GROUPBYREVISION");
                    if (groupByRevision.EditValue.ToString() == "Y")
                    {
                        grdByProduct.View.Columns["PRODUCTDEFVERSION"].Visible = true;
                    }
                    else
                    {
                        grdByProduct.View.Columns["PRODUCTDEFVERSION"].Visible = false;
                    }

                    dtResult = this.Procedure("usp_wip_selectdailyresultandstockbyproductpersite", values);
                    grdByProduct.DataSource = dtResult;
                    break;
            }

            if (dtResult.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// DataTable To Dictionary
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetDictionary(DataTable dt)
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                pairs.Add(dt.Columns[i].ColumnName, dt.Rows.Cast<DataRow>().Select(k => k[dt.Columns[i]]).First());
            }
            return pairs;
        }

        #endregion

        /// <summary>
        /// from 부터 to까지의 일자들의 컬렉션을 반환한다.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
    }
}
