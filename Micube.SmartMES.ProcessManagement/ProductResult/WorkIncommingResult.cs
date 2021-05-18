#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
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

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 생산실적 > 생산입고실적조회
	/// 업  무  설  명  : 재공데이터를 일자별 실적 및 재공으로 구분하여 보여준다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-23
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class WorkIncommingResult : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public WorkIncommingResult()
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
			InitializeByDayGrid();
			InitializeByProductGrid();
            InitializeByLotGrid();
            InitializeByDaySMTGrid();

            InitializeByDayGridSummary();
            InitializeByProductGridSummary();
            InitializeByLotGridSummary();

            workInResultByDaySMT.PageVisible = false;
        }

		/// <summary>        
		/// 일별 실적현황 그리드 초기화
		/// </summary>
		private void InitializeByDayGrid()
		{
			// TODO : 그리드 초기화 로직 추가
			grdByDay.GridButtonItem = GridButtonItem.Export;
			grdByDay.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdByDay.View.OptionsView.ShowFooter = true;
			grdByDay.View.SetIsReadOnly();

			//일별
			grdByDay.View.AddTextBoxColumn("RESULTDATE", 100).SetTextAlignment(TextAlignment.Center).SetLabel("DATE");
			//실적구분
			grdByDay.View.AddTextBoxColumn("EXPORTPACKINGTYPE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("RESULTTYPE2");
			//실적pcs
			grdByDay.View.AddSpinEditColumn("RESULTPCSQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("RESULTPCS");
			//실적pnl
			grdByDay.View.AddSpinEditColumn("RESULTPNLQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right).SetLabel("RESULTPNL");
			//실적mm
			grdByDay.View.AddSpinEditColumn("RESULTMM", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//실적금액
			grdByDay.View.AddSpinEditColumn("RESULTPRICE", 120).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//누계pcs
			grdByDay.View.AddSpinEditColumn("CUMPCSQTY", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//누계pnl
			grdByDay.View.AddSpinEditColumn("CUMPNLQTY", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//누계mm
			grdByDay.View.AddSpinEditColumn("CUMMM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//누계금액
			grdByDay.View.AddSpinEditColumn("CUMPRICE", 140).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//누계BARE금액
			grdByDay.View.AddSpinEditColumn("CUMBAREPRICE", 140).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//누계SMT금액
			grdByDay.View.AddSpinEditColumn("CUMSMTPRICE", 140).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);

			grdByDay.View.PopulateColumns();
		}

        private void InitializeByDayGridSummary()
        {
            grdByDay.View.Columns["RESULTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByDay.View.Columns["RESULTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByDay.View.Columns["RESULTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByDay.View.Columns["RESULTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByDay.View.Columns["RESULTMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByDay.View.Columns["RESULTMM"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdByDay.View.Columns["RESULTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByDay.View.Columns["RESULTPRICE"].SummaryItem.DisplayFormat = "{0:###,###.##}";

            grdByDay.View.Columns["RESULTDATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByDay.View.Columns["RESULTDATE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
        }

        /// <summary>
        /// 품목별 실적현황 그리드 초기화
        /// </summary>
        private void InitializeByProductGrid()
		{
			grdByProduct.GridButtonItem = GridButtonItem.Export;
			grdByProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdByProduct.View.OptionsView.ShowFooter = true;
            grdByProduct.View.SetIsReadOnly();

			//입고일자
			grdByProduct.View.AddTextBoxColumn("INBOUNDDATE", 80).SetTextAlignment(TextAlignment.Center);
			//고객
			grdByProduct.View.AddTextBoxColumn("CUSTOMERNAME", 80);
			//Type
			grdByProduct.View.AddTextBoxColumn("PRODUCTDEFTYPE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
			//품목코드
			grdByProduct.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            //REV
            grdByProduct.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("ITEMVERSION");
            //품목명
            grdByProduct.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 제품Type
            grdByProduct.View.AddTextBoxColumn("PRODUCTTYPE", 90);
            //Layer
            grdByProduct.View.AddTextBoxColumn("LAYER", 60).SetTextAlignment(TextAlignment.Center);
			//생산처
			grdByProduct.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
			//양산구분
			grdByProduct.View.AddTextBoxColumn("LOTPRODUCTTYPE", 60).SetTextAlignment(TextAlignment.Center);
			//포장구분(제품/반제품)
			grdByProduct.View.AddTextBoxColumn("PACKINGTYPE", 60).SetTextAlignment(TextAlignment.Center);
			//실적구분(포장/수출포장)
			grdByProduct.View.AddTextBoxColumn("EXPORTPACKINGTYPE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("RESULTTYPE2");
			//합수
			grdByProduct.View.AddSpinEditColumn("PCSPNL", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("ARRAY");
			//산출수
			grdByProduct.View.AddSpinEditColumn("CALVALUE", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("CALCULATION");
			//실적pcs
			grdByProduct.View.AddSpinEditColumn("RESULTPCSQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("RESULTPCS");
			//실적pnl
			grdByProduct.View.AddSpinEditColumn("RESULTPNLQTY", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right).SetLabel("RESULTPNL");
			//실적mm
			grdByProduct.View.AddSpinEditColumn("RESULTMM", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//실적금액
			grdByProduct.View.AddSpinEditColumn("RESULTPRICE", 120).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//FPCB금액
			grdByProduct.View.AddSpinEditColumn("FPCBPRICE", 120).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//SMT금액
			grdByProduct.View.AddSpinEditColumn("SMTPRICE", 120).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);

			grdByProduct.View.PopulateColumns();
		}

        private void InitializeByProductGridSummary()
        {
            grdByProduct.View.Columns["RESULTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["RESULTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProduct.View.Columns["RESULTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["RESULTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByProduct.View.Columns["RESULTMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["RESULTMM"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdByProduct.View.Columns["RESULTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["RESULTPRICE"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdByProduct.View.Columns["FPCBPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["FPCBPRICE"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdByProduct.View.Columns["SMTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["SMTPRICE"].SummaryItem.DisplayFormat = "{0:###,###.##}";

            grdByProduct.View.Columns["CUSTOMERNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByProduct.View.Columns["CUSTOMERNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
        }

        /// <summary>
        /// LOT별 실적현황 그리드 초기화
        /// </summary>
        private void InitializeByLotGrid()
		{
			grdByLot.GridButtonItem = GridButtonItem.Export;
			grdByLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdByLot.View.SetIsReadOnly();
            grdByLot.View.OptionsView.ShowFooter = true;

            //고객
            grdByLot.View.AddTextBoxColumn("CUSTOMERNAME" ,80);
			//Type
			grdByLot.View.AddTextBoxColumn("PRODUCTDEFTYPE").SetTextAlignment(TextAlignment.Center).SetIsHidden();
			//품목코드
			grdByLot.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            //rev
            grdByLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("ITEMVERSION");
            //품목명
            grdByLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			//Layer
			grdByLot.View.AddTextBoxColumn("LAYER", 50).SetTextAlignment(TextAlignment.Center);
			//SMT여부
			grdByLot.View.AddTextBoxColumn("ISSMT", 50);
			//LOTID
			grdByLot.View.AddTextBoxColumn("LOTID", 170);
			//창고명
			grdByLot.View.AddTextBoxColumn("WAREHOUSENAME", 80);
			//양산구분
			grdByLot.View.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center);
			//실적구분
			grdByLot.View.AddTextBoxColumn("EXPORTPACKINGTYPE", 60).SetTextAlignment(TextAlignment.Center).SetLabel("RESULTTYPE2");
			//투입일자
			grdByLot.View.AddTextBoxColumn("STARTEDDATE", 120).SetTextAlignment(TextAlignment.Center);
			//생산입고일자
			grdByLot.View.AddTextBoxColumn("SENDTIME", 120).SetLabel("PRODUCTINCOMETIME").SetTextAlignment(TextAlignment.Center);
			//실적pcs
			grdByLot.View.AddSpinEditColumn("RESULTPCSQTY", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//실적pnl
			grdByLot.View.AddSpinEditColumn("RESULTPNLQTY", 60).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//실적mm
			grdByLot.View.AddSpinEditColumn("RESULTMM", 60).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            //실적금액
            grdByLot.View.AddSpinEditColumn("RESULTPRICE", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            //FPCB단가
            grdByLot.View.AddSpinEditColumn("FPCBUNITPRICE", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//FPCB금액
			grdByLot.View.AddSpinEditColumn("FPCBPRICE", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//SMT단가
			grdByLot.View.AddSpinEditColumn("SMTUNITPRICE", 80).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//SMT금액
			grdByLot.View.AddSpinEditColumn("SMTPRICE", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//작업장
			grdByLot.View.AddTextBoxColumn("AREANAME", 120);
			//전공정
			grdByLot.View.AddTextBoxColumn("PREVPROCESSSEGMENTNAME", 100);
			//생산처
			grdByLot.View.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
			//SMT작업장
			grdByLot.View.AddTextBoxColumn("SMTAREANAME", 120);
			//출하검사작업장
			grdByLot.View.AddTextBoxColumn("SHIPMENTAREANAME", 120);

			grdByLot.View.PopulateColumns();
		}

        private void InitializeByLotGridSummary()
        {
            grdByLot.View.Columns["RESULTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByLot.View.Columns["RESULTPCSQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByLot.View.Columns["RESULTPNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByLot.View.Columns["RESULTPNLQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdByLot.View.Columns["RESULTMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByLot.View.Columns["RESULTMM"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdByLot.View.Columns["RESULTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByLot.View.Columns["RESULTPRICE"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdByLot.View.Columns["FPCBPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByLot.View.Columns["FPCBPRICE"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdByLot.View.Columns["SMTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByLot.View.Columns["SMTPRICE"].SummaryItem.DisplayFormat = "{0:###,###.##}";

            grdByLot.View.Columns["CUSTOMERNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByLot.View.Columns["CUSTOMERNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
        }

        /// <summary>
        /// 일별(SMT) 실적현황 그리드 초기화
        /// </summary>
        private void InitializeByDaySMTGrid()
		{
			grdByDaySMT.GridButtonItem = GridButtonItem.Export;
			grdByDaySMT.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdByDaySMT.View.SetIsReadOnly();

			//일별
			grdByDaySMT.View.AddTextBoxColumn("RESULTDATE", 150).SetTextAlignment(TextAlignment.Center).SetLabel("DATE");
			//실적pcs
			grdByDaySMT.View.AddSpinEditColumn("RESULTPCSQTY", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//실적pnl
			grdByDaySMT.View.AddSpinEditColumn("RESULTPNLQTY", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//실적mm
			grdByDaySMT.View.AddSpinEditColumn("RESULTMM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//FPCB금액
			grdByDaySMT.View.AddSpinEditColumn("FPCBPRICE", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//SMT PCS
			grdByDaySMT.View.AddSpinEditColumn("SMTPCSQTY").SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//SMT MM
			grdByDaySMT.View.AddSpinEditColumn("SMTMM").SetDisplayFormat("#,##0.##", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//SMT금액
			grdByDaySMT.View.AddSpinEditColumn("SMTPRICE", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//기타금액
			grdByDaySMT.View.AddSpinEditColumn("ETCPRICE", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);

			grdByDaySMT.View.PopulateColumns();
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
            // TODO : 화면에서 사용할 이벤트 추가
            //tabWorkIncommingResult.SelectedPageChanged += TabWorkIncommingResult_SelectedPageChanged;
            grdByDay.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdByProduct.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdByLot.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
        }

        private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.Font = ft;
        }

        /// <summary>
        /// 탭 변경 시 실적 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabWorkIncommingResult_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			SmartBandedGrid grid = e.Page.Controls[0] as SmartBandedGrid;

			DataSearchTabChanged(grid);
		}

		#endregion

		#region 검색

		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();


            //workInResultByLotPage
            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            
            DataTable dt = null;

            switch (tabWorkIncommingResult.SelectedTabPage.Name)
            {
                case "workInResultByDayPage":
                    //SelectWorkIncommingResultByDate
                    dt = SqlExecuter.Query("SelectWorkIncommingResultByDate", "10001", values);
                    grdByDay.DataSource = dt;
                    break;
                case "workInResultByProductPage":
                    dt = SqlExecuter.Query("SelectWorkIncommingResultByProduct", "10001", values);
                    grdByProduct.DataSource = dt;
                    break;
                case "workInResultByLotPage":
                    dt = SqlExecuter.Query("SelectWorkIncommingResultByLot", "10001", values);
                    grdByLot.DataSource = dt;
                    break;
                case "workInResultByDaySMT":
                    break;
            }
            if (dt.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
            }
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
            {
                //작업장
                CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.1, true, Conditions, false, false);

                //공정
                CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 2.1, true, Conditions);
            }

            //품목코드
            InitializeCondition_ProductPopup();
            //작업구분
            //Conditions.GetCondition<ConditionItemComboBox>("P_WORKTYPE").SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
            {
                Conditions.GetCondition<ConditionItemComboBox>("P_OWNERFACTORY").SetIsHidden();
            }
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
				.SetPosition(1.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
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
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		#endregion

		#region Private Function

		/// <summary>
		/// 데이터 조회
		/// </summary>
		private void DataSearchTabChanged(SmartBandedGrid grid = null)
		{	
			var values = Conditions.GetValues();
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			int index = tabWorkIncommingResult.SelectedTabPageIndex;
			values.Remove("TYPE");
			switch (index)
			{
				case 0://일별
					grid = grid == null ? grdByDay : grid;
					if(!grid.View.IsInitializeColumns)
					{
						InitializeByDayGrid();
					}

					values.Add("TYPE", "BYDAY");
					DataTable dtByDay = SqlExecuter.Query("SelectWorkIncommingResult", "10002", values);

					if (dtByDay.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByDay.DataSource = dtByDay;

					break;
				case 1://품목별
					grid = grid == null ? grdByProduct : grid;
					if (!grid.View.IsInitializeColumns)
					{
						InitializeByProductGrid();
					}

					values.Add("TYPE", "BYPRODUCT");
					DataTable dtByProduct = SqlExecuter.Query("SelectWorkIncommingResult", "10002", values);

					if (dtByProduct.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByProduct.DataSource = dtByProduct;

					break;
				case 2://LOT별
					grid = grid == null ? grdByLot : grid;
					if(!grid.View.IsInitializeColumns)
					{
						InitializeByLotGrid();
					}

					values.Add("TYPE", "BYLOT");
					DataTable dtByLot = SqlExecuter.Query("SelectWorkIncommingResult", "10002", values);

					if (dtByLot.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByLot.DataSource = dtByLot;

					break;
				case 3://일별(SMT)
					grid = grid == null ? grdByDaySMT : grid;
					if(!grid.View.IsInitializeColumns)
					{
						InitializeByDaySMTGrid();
					}

					values.Add("TYPE", "BYDAYSMT");
					DataTable dtByLotSMT = SqlExecuter.Query("SelectWorkIncommingResult", "10002", values);

					if (dtByLotSMT.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByDaySMT.DataSource = dtByLotSMT;
					break;
			}
		}
		#endregion
	}
}