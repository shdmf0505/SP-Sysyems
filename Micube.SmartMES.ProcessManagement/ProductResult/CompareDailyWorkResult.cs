#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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
	/// 프 로 그 램 명  : 공정관리 > 생산실적 > 일일 생산실적 비교
	/// 업  무  설  명  : 전일/금일 실적 및 재공현황을 비교하여 보여준다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-10-10
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class CompareDailyWorkResult : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public CompareDailyWorkResult()
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
			// TODO : 그리드 초기화 로직 추가
			grdWorkResult.GridButtonItem = GridButtonItem.Export;
			grdWorkResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdWorkResult.View.SetIsReadOnly();

            grdWorkResult.View.AddTextBoxColumn("OPERATION", 130);
            grdWorkResult.View.AddTextBoxColumn("AREANAME", 130);
            grdWorkResult.View.AddTextBoxColumn("OWNTYPE", 80).SetTextAlignment(TextAlignment.Center);
            grdWorkResult.View.AddTextBoxColumn("PARTNERS", 100);
			//계획수량(합계)
			grdWorkResult.View.AddSpinEditColumn("TOTALPLANNEDQTY", 100);
			//전일실적(PNL)
			grdWorkResult.View.AddSpinEditColumn("LASTDAYRESULTPNL", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//금일실적(PNL)
			grdWorkResult.View.AddSpinEditColumn("TODAYRESULTPNL", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//전일재공(PNL)
			grdWorkResult.View.AddSpinEditColumn("LASTDAYWIPPNL", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//금일재공(PNL)
			grdWorkResult.View.AddSpinEditColumn("TODAYWIPPNL", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//전일실적(PCS)
			grdWorkResult.View.AddSpinEditColumn("LASTDAYRESULTPCS", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//전일실적(M2)
			grdWorkResult.View.AddSpinEditColumn("LASTDAYRESULTMM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//금일실적(PCS)
			grdWorkResult.View.AddSpinEditColumn("TODAYRESULTPCS", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//금일실적(M2)
			grdWorkResult.View.AddSpinEditColumn("TODAYRESULTMM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//전일재공(PCS)
			grdWorkResult.View.AddSpinEditColumn("LASTDAYWIPPCS", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//전일재공(M2)
			grdWorkResult.View.AddSpinEditColumn("LASTDAYWIPMM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//금일재공(PCS)
			grdWorkResult.View.AddSpinEditColumn("TODAYWIPPCS", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			//금일재공(M2)
			grdWorkResult.View.AddSpinEditColumn("TODAYWIPMM", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            //전일재공금액
            grdWorkResult.View.AddSpinEditColumn("LASTWIPAMOUNT", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            //금일재공금액
            grdWorkResult.View.AddSpinEditColumn("TODAYWIPAMOUNT", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
            //전일실적금액
            grdWorkResult.View.AddSpinEditColumn("LASTRESULTPRICE", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right)
                .SetIsHidden();
            //금일실적금액
            grdWorkResult.View.AddSpinEditColumn("TODAYRESULTPRICE", 100).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right)
                .SetIsHidden();

            grdWorkResult.View.PopulateColumns();

        }

        private void InitializationSummaryRow()
        {
            grdWorkResult.View.Columns["PARTNERS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["PARTNERS"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdWorkResult.View.Columns["TOTALPLANNEDQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TOTALPLANNEDQTY"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["LASTDAYRESULTPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTDAYRESULTPNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["TODAYRESULTPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYRESULTPNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["LASTDAYWIPPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTDAYWIPPNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["TODAYWIPPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYWIPPNL"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["LASTWIPAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTWIPAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["TODAYWIPAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYWIPAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["LASTRESULTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTRESULTPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["TODAYRESULTPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYRESULTPRICE"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["LASTDAYRESULTPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTDAYRESULTPCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["LASTDAYRESULTMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTDAYRESULTMM"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdWorkResult.View.Columns["TODAYRESULTPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYRESULTPCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["TODAYRESULTMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYRESULTMM"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdWorkResult.View.Columns["LASTDAYWIPPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTDAYWIPPCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["LASTDAYWIPMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["LASTDAYWIPMM"].SummaryItem.DisplayFormat = "{0:###,###.##}";
            grdWorkResult.View.Columns["TODAYWIPPCS"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYWIPPCS"].SummaryItem.DisplayFormat = "{0:###,###}";
            grdWorkResult.View.Columns["TODAYWIPMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdWorkResult.View.Columns["TODAYWIPMM"].SummaryItem.DisplayFormat = "{0:###,###.##}";

            grdWorkResult.View.OptionsView.ShowFooter = true;
            grdWorkResult.ShowStatusBar = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            // TODO : 화면에서 사용할 이벤트 추가
            grdWorkResult.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
        }

        /// <summary>
		/// 합계 스타일
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


        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			//// TODO : 저장 Rule 변경
			//DataTable changed = grdList.GetChangedRows();

			//ExecuteRule("SaveCodeClass", changed);
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

            values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtWorkResult = await SqlExecuter.QueryAsync("SelectCompareDailyWorkResult", "10002", values);

            if (dtWorkResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWorkResult.DataSource = dtWorkResult;
            
        }

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // 품목
            InitializeCondition_ProductPopup();
            //공정
            Commons.CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 1.1, true, Conditions);
            //작업장
            Commons.CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.1, true, Conditions, false, false);
        }

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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
                .SetPosition(3.5)
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

        #endregion

	}
}