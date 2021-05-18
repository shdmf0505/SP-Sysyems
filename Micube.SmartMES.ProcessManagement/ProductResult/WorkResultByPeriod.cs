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
	/// 프 로 그 램 명  : 공정관리 > 생산실적 > 작업 실적 조회
	/// 업  무  설  명  : 재공데이터를 일자별 실적 및 재공으로 구분하여 보여주는 화면
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-18
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class WorkResultByPeriod : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public WorkResultByPeriod()
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
			InitializeBySegmentAreaGrid();
			InitializeByProductGrid();
			InitializeByLotGrid();
			InitializationSummaryRowOfArea();
			InitializationSummaryRowOfProduct();
			InitializationSummaryRowOfLot();

            lblSearchCriteria.Text = Language.Get("SEARCHCRITERIA") + " 08:30";

        }

		/// <summary>        
		/// 공정 / 작업장 그리드 초기화
		/// </summary>
		private void InitializeBySegmentAreaGrid()
		{
			grdByArea.GridButtonItem = GridButtonItem.Export;
			grdByArea.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdByArea.View.SetIsReadOnly();

			var defaultCol = grdByArea.View.AddGroupColumn("");
			//SITE
			defaultCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
            //작업장
            defaultCol.AddTextBoxColumn("AREANAME", 150);
            defaultCol.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            //공정명
            defaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            defaultCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            //RTR/SHT
            defaultCol.AddTextBoxColumn("RTRSHT", 80).SetTextAlignment(TextAlignment.Center);

            //기초
            var baseCol = grdByArea.View.AddGroupColumn("BASICS");
            baseCol.AddTextBoxColumn("BASEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            baseCol.AddTextBoxColumn("BASEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            //실적
            var sendCol = grdByArea.View.AddGroupColumn("FIGURE");
			sendCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			sendCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
			//현재공
			var wipCol = grdByArea.View.AddGroupColumn("CURRENTWIP");
			wipCol.AddTextBoxColumn("WIPQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			wipCol.AddTextBoxColumn("WIPPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
			//투입
			var inputCol = grdByArea.View.AddGroupColumn("INPUT");
			inputCol.AddTextBoxColumn("INPUTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			inputCol.AddTextBoxColumn("INPUTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");

			var defaultCol2 = grdByArea.View.AddGroupColumn("");
			//대공정
			defaultCol2.AddTextBoxColumn("LARGEPROCESSSEGMENT", 80);
			//중공정
			defaultCol2.AddTextBoxColumn("MIDDLEPROCESSSEGMENT", 100);

			grdByArea.View.PopulateColumns();
		}

		/// <summary>
		/// 품목 그리드 초기화
		/// </summary>
		private void InitializeByProductGrid()
		{
			grdByProduct.GridButtonItem = GridButtonItem.Export;
			grdByProduct.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdByProduct.View.SetIsReadOnly();

			var defaultCol = grdByProduct.View.AddGroupColumn("");
			//양산구분
			defaultCol.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center);
			//SITE
			defaultCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
			//품목코드
			defaultCol.AddTextBoxColumn("PRODUCTDEFID", 150);
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("PRODUCTREVISION");
            //품목명
            defaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 250);
			//공정순번
			//defaultCol.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
			//공정명
			defaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);

            //기초
            var baseCol = grdByProduct.View.AddGroupColumn("BASICS");
            baseCol.AddTextBoxColumn("BASEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            baseCol.AddTextBoxColumn("BASEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            //실적
            var sendCol = grdByProduct.View.AddGroupColumn("FIGURE");
			sendCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			sendCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
			//현재공
			var wipCol = grdByProduct.View.AddGroupColumn("CURRENTWIP");
			wipCol.AddTextBoxColumn("WIPQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			wipCol.AddTextBoxColumn("WIPPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
			//투입
			var inputCol = grdByProduct.View.AddGroupColumn("INPUT");
			inputCol.AddTextBoxColumn("INPUTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			inputCol.AddTextBoxColumn("INPUTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");

			var defaultCol2 = grdByProduct.View.AddGroupColumn("");
			//대공정
			defaultCol2.AddTextBoxColumn("LARGEPROCESSSEGMENT", 80);
			//중공정
			defaultCol2.AddTextBoxColumn("MIDDLEPROCESSSEGMENT", 100);

			grdByProduct.View.PopulateColumns();
		}

		/// <summary>
		/// LOT 그리드 초기화
		/// </summary>
		private void InitializeByLotGrid()
		{
			grdByLot.GridButtonItem = GridButtonItem.Export;
			grdByLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdByLot.View.SetIsReadOnly();

			var defaultCol = grdByLot.View.AddGroupColumn("");
			//양산구분
			defaultCol.AddTextBoxColumn("LOTTYPE", 60).SetTextAlignment(TextAlignment.Center);
			//SITE
			defaultCol.AddTextBoxColumn("PLANTID", 60).SetTextAlignment(TextAlignment.Center);
			//품목코드
			defaultCol.AddTextBoxColumn("PRODUCTDEFID", 120);
            //품목코드
            defaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("ITEMVERSION");
            //품목코드
            defaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 180);
            //LOTID
            defaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
			//공정명
			defaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
			//작업장
			defaultCol.AddTextBoxColumn("AREANAME", 150);
			//RTR/SHT
			defaultCol.AddTextBoxColumn("RTRSHT", 80).SetTextAlignment(TextAlignment.Center);
			//작업구분
			defaultCol.AddComboBoxColumn("WORKTYPE", 80, new SqlQuery("GetTypeList", "10001", "CODECLASSID=LotWorkType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID").SetTextAlignment(TextAlignment.Center);
			//자원
			defaultCol.AddTextBoxColumn("EQUIPMENT", 60).SetLabel("RESOURCE");

            //기초
            var baseCol = grdByLot.View.AddGroupColumn("BASICS");
            baseCol.AddTextBoxColumn("BASEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS").SetIsHidden();
            baseCol.AddTextBoxColumn("BASEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL").SetIsHidden();
            //실적
            var sendCol = grdByLot.View.AddGroupColumn("FIGURE");
			sendCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			sendCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
			//현재공
			var wipCol = grdByLot.View.AddGroupColumn("CURRENTWIP");
			wipCol.AddTextBoxColumn("WIPQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			wipCol.AddTextBoxColumn("WIPPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
			//투입
			var inputCol = grdByLot.View.AddGroupColumn("INPUT");
			inputCol.AddTextBoxColumn("INPUTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			inputCol.AddTextBoxColumn("INPUTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");

			var defaultCol2 = grdByLot.View.AddGroupColumn("");
			//대공정
			defaultCol2.AddTextBoxColumn("LARGEPROCESSSEGMENT", 80);
			//중공정
			defaultCol2.AddTextBoxColumn("MIDDLEPROCESSSEGMENT", 100);


			grdByLot.View.PopulateColumns();
		}

		/// <summary>
		/// 작업장별 합계 Row 초기화
		/// </summary>
		private void InitializationSummaryRowOfArea()
		{
			grdByArea.View.Columns["AREANAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["AREANAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
			//실적
			grdByArea.View.Columns["SENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["SENDPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByArea.View.Columns["SENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["SENDPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
			//현재공
			grdByArea.View.Columns["WIPQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["WIPQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByArea.View.Columns["WIPPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["WIPPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
			//투입
            /*
			grdByArea.View.Columns["INPUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByArea.View.Columns["INPUTPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["INPUTPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            */
			//기초
			grdByArea.View.Columns["BASEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["BASEPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByArea.View.Columns["BASEPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["BASEPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";


			grdByArea.View.OptionsView.ShowFooter = true;
			grdByArea.ShowStatusBar = false;
		}

		/// <summary>
		/// 품목별 합계 Row 초기화
		/// </summary>
		private void InitializationSummaryRowOfProduct()
		{
			grdByProduct.View.Columns["PRODUCTDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["PRODUCTDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
			//실적
			grdByProduct.View.Columns["SENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["SENDPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByProduct.View.Columns["SENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["SENDPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
			//현재공
			grdByProduct.View.Columns["WIPQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["WIPQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByProduct.View.Columns["WIPPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["WIPPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
			//투입
            /*
			grdByProduct.View.Columns["INPUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByProduct.View.Columns["INPUTPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["INPUTPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            */
			//기초
			grdByProduct.View.Columns["BASEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["BASEPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByProduct.View.Columns["BASEPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByProduct.View.Columns["BASEPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";

			grdByProduct.View.OptionsView.ShowFooter = true;
			grdByProduct.ShowStatusBar = false;
		}

		/// <summary>
		/// LOT별 합계 Row 초기화
		/// </summary>
		private void InitializationSummaryRowOfLot()
		{
			grdByLot.View.Columns["LOTID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["LOTID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
			//실적
			grdByLot.View.Columns["SENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["SENDPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByLot.View.Columns["SENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["SENDPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
			//현재공
			grdByLot.View.Columns["WIPQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["WIPQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByLot.View.Columns["WIPPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["WIPPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
			//투입
            /*
			grdByLot.View.Columns["INPUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByLot.View.Columns["INPUTPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["INPUTPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            */
			//기초
			grdByLot.View.Columns["BASEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["BASEPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByLot.View.Columns["BASEPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByLot.View.Columns["BASEPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";

			grdByLot.View.OptionsView.ShowFooter = true;
			grdByLot.ShowStatusBar = false;
		}
		#endregion

		#region 이벤트
		private void InitializeEvent()
		{
			grdByArea.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
			grdByProduct.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
			grdByLot.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdByArea.View.DoubleClick += grdByAreaView_DoubleClick;
            grdByProduct.View.DoubleClick += grdByProductView_DoubleClick;
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").ButtonClick += WorkResultByPeriod_ButtonClick;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += WorkResultByPeriod_EditValueChanged; ;
        }

        private void WorkResultByPeriod_EditValueChanged(object sender, EventArgs e)
        {
            string productDefId = (string)Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue;
            if (string.IsNullOrEmpty(productDefId))
            {
                DataTable dt = (Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource as DataTable).Clone();
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = "*";
            }
        }

        private void WorkResultByPeriod_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;
            }
        }

        private void grdByAreaView_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grdByArea.View.FocusedRowHandle;

            if (rowHandle < 0) return;

            string areaId = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "AREAID"));
            string areaname = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "AREANAME"));

            string processsegmentid = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "PROCESSSEGMENTID"));
            string processsegmentname = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "PROCESSSEGMENTNAME"));

            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(areaId);
            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = areaname;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").SetValue(processsegmentid);
            Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").Text = processsegmentname;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(string.Empty);
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = string.Empty;

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;

            tabWorkResultByPeriod.SelectedTabPage = tabWorkResultByPeriod.TabPages[2];

            OnSearchAsync();
        }

        private void grdByProductView_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grdByProduct.View.FocusedRowHandle;

            if (rowHandle < 0) return;

            string productDefId = Format.GetFullTrimString(grdByProduct.View.GetRowCellValue(rowHandle, "PRODUCTDEFID"));
            string productDefversion = Format.GetFullTrimString(grdByProduct.View.GetRowCellValue(rowHandle, "PRODUCTDEFVERSION"));
            string productDefName = Format.GetString(grdByProduct.View.GetRowCellValue(rowHandle, "PRODUCTDEFNAME"));

            productDefId = string.Format("{0}|{1}", productDefId, productDefversion);

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(productDefId);
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = productDefId;

            Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = productDefName;

            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(string.Empty);
            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = string.Empty;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").SetValue(string.Empty);
            Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").Text = string.Empty;
            
            tabWorkResultByPeriod.SelectedTabPage = tabWorkResultByPeriod.TabPages[2];

            OnSearchAsync();
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

		#region 검색

		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			// TODO : 조회 SP 변경
			var values = Conditions.GetValues();
            // DateTime 파라미터 -> yyyy-MM-dd 로 변환
            values["P_PERIOD_PERIODFR"] = values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10);
            values["P_PERIOD_PERIODTO"] = DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)).AddDays(-1).ToString("yyyy-MM-dd");
            if (DateTime.Parse(values["P_PERIOD_PERIODFR"].ToString().Substring(0, 10)) >= DateTime.Parse(values["P_PERIOD_PERIODTO"].ToString().Substring(0, 10)))
            {
                values["P_PERIOD_PERIODTO"] = values["P_PERIOD_PERIODFR"];
            }

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			DataTable dtWorkResultByPeriod = new DataTable();
			int index = tabWorkResultByPeriod.SelectedTabPageIndex;
			values.Remove("TYPE");
			switch (index)
			{
				case 0:
					values.Add("TYPE", "BYAREA");
					dtWorkResultByPeriod = await QueryAsync("SelectWorkResultList", "10001", values);

					if (dtWorkResultByPeriod.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByArea.DataSource = dtWorkResultByPeriod;
					break;
				case 1:
					values.Add("TYPE", "BYPRODUCT");
					dtWorkResultByPeriod = await QueryAsync("SelectWorkResultList", "10001", values);

					if (dtWorkResultByPeriod.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByProduct.DataSource = dtWorkResultByPeriod;
					break;					
				case 2:
					values.Add("TYPE", "BYLOT");
					dtWorkResultByPeriod = await QueryAsync("SelectWorkResultList", "10001", values);

					if (dtWorkResultByPeriod.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByLot.DataSource = dtWorkResultByPeriod;
					break;
			}			
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //작업장
            Commons.CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.1, true, Conditions, false, false);
			//품목
			InitializeCondition_ProductPopup();
			//Commons.CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 1.2, true, Conditions);
			//작업구분
			//Conditions.GetCondition<ConditionItemComboBox>("P_WORKTYPE").SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false);
			//공정
			Commons.CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 5.2, true, Conditions);
            Conditions.GetCondition<ConditionItemDateEdit>("P_PERIOD").SetValidationIsRequired();
			//대공정
			InitializeCondition_TopProcesssegmentClassPopup();
			//중공정
			InitializeCondition_MiddleProcesssegmentClassPopup();
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEFID", "PRODUCTDEFID")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPosition(1.2)
				.SetPopupResultCount(1)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

                    string productDefId = "";
                    List<string> listRev = new List<string>();
                    selectRow.AsEnumerable().ForEach(r => {
                        productDefId = Format.GetString(r["PRODUCTDEFID"]);
                        productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                        listRev.Add(Format.GetString(r["PRODUCTDEFVERSION"]));
					});
                    productDefName = productDefName.TrimEnd(',');
                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;

                    listRev = listRev.Distinct().ToList();
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("P_PLANTID", UserInfo.Current.Plant);
                    param.Add("P_PRODUCTDEFID", productDefId);
                    DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);

                    Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                    if (listRev.Count > 0)
                    {
                        if (listRev.Count == 1)
                            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString(listRev[0]);
                        else
                            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = Format.GetFullTrimString('*');
                    }
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
		/// 팝업형 조회조건 생성 - 대공정
		/// </summary>
		private void InitializeCondition_TopProcesssegmentClassPopup()
		{
			var condition = Conditions.AddSelectPopup("P_TOPPROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
				.SetPopupLayout("TOPPROCESSSEGMENTCLASSLIST", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(600, 600)
				.SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
				.SetLabel("LARGEPROCESSSEGMENT")
				.SetPosition(7.8)
				.SetPopupResultCount(0);


			condition.Conditions.AddTextBox("PROCESSSEGMENTCLASS").SetLabel("TXTLOADTOPSEGMENTCLASS");

			//대공정ID
			condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetLabel("TOPPROCESSSEGMENTCLASSID");
			//대공정명
			condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200).SetLabel("TOPPROCESSSEGMENTCLASSNAME");
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 중공정
		/// </summary>
		private void InitializeCondition_MiddleProcesssegmentClassPopup()
		{
			var condition = Conditions.AddSelectPopup("P_MIDDLEPROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClass", "10001", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
				.SetPopupLayout("MIDDLEPROCESSSEGMENTCLASSLIST", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(600, 600)
				.SetPopupAutoFillColumns("PROCESSSEGMENTCLASSNAME")
				.SetLabel("MIDDLEPROCESSSEGMENT")
				.SetPosition(7.9)
				.SetPopupResultCount(0);


			condition.Conditions.AddTextBox("PROCESSSEGMENTCLASS").SetLabel("TXTLOADMIDDLESEGMENTCLASS");

			//중공정ID
			condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetLabel("MIDDLEPROCESSSEGMENTID");
			//중공정명
			condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200).SetLabel("MIDDLEPROCESSSEGMENTNAME");
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

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;
            if (btn.Name.ToString().Equals("Initialization"))
            {
                // 조회일자
                Conditions.GetControl<SmartDateEdit>("P_PERIOD").EditValue = DateTime.Now;

                // 작업장 ID
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(string.Empty);
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = string.Empty;

                // 품목코드
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(string.Empty);
                Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = string.Empty;

                // 품목명
                Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;

                // 양산구분
                Conditions.GetControl<SmartComboBox>("P_LOTPRODUCTTYPESTATUS").EditValue = "*";

                // 작업구분
                Conditions.GetControl<SmartComboBox>("P_WORKTYPE").EditValue = "*";

                // 공정
                Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").SetValue(string.Empty);
                Conditions.GetControl<SmartSelectPopupEdit>("P_PROCESSSEGMENTID").Text = string.Empty;

                // Site
                Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValue = "*";

                // 담당공장
                SmartComboBox ownerFactory = Conditions.GetControl<SmartComboBox>("P_OWNERFACTORY");
                if (ownerFactory != null)
                {
                    Conditions.GetControl<SmartComboBox>("P_OWNERFACTORY").EditValue = "*";
                }

                // 그리드
                grdByArea.DataSource = null;
                grdByProduct.DataSource = null;
                grdByLot.DataSource = null;
            }
        }

        #region Private Function
        #endregion
    }
}