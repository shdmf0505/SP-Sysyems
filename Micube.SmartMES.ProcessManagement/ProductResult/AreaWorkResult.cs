#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using Micube.Framework.SmartControls.Grid.Conditions;

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
	public partial class AreaWorkResult : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public AreaWorkResult()
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
			InitializeByLotGrid();
			InitializationSummaryRowOfArea();
			//InitializationSummaryRowOfLot();

           // lblSearchCriteria.Text = Language.Get("SEARCHCRITERIA") + " 08:30";

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
            defaultCol.AddTextBoxColumn("VENDORNAME", 150);
            defaultCol.AddTextBoxColumn("VENDORID", 150).SetIsHidden();
            //공정명
            defaultCol.AddTextBoxColumn("AREANAME", 150);
            defaultCol.AddTextBoxColumn("AREAID", 150).SetIsHidden();
            //공정명
            defaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            defaultCol.AddTextBoxColumn("MIDDLEPROCESSSEGMENTID", 150).SetIsHidden();


            //RTR/SHT
            defaultCol.AddTextBoxColumn("RTRSHT", 80).SetTextAlignment(TextAlignment.Center);

            //기초
            var baseCol = grdByArea.View.AddGroupColumn("BASICS");
            baseCol.AddTextBoxColumn("BASEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            baseCol.AddTextBoxColumn("BASEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            baseCol.AddTextBoxColumn("BASEM2", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            //실적
            var sendCol = grdByArea.View.AddGroupColumn("FIGURE");
			sendCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			sendCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            sendCol.AddTextBoxColumn("SENDM2", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            sendCol.AddTextBoxColumn("SENDLOTCOUNT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("LOTCNT");
            //현재공
            var wipCol = grdByArea.View.AddGroupColumn("CURRENTWIP");
			wipCol.AddTextBoxColumn("WIPPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			wipCol.AddTextBoxColumn("WIPPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            wipCol.AddTextBoxColumn("WIPM2", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            wipCol.AddTextBoxColumn("WIPLOTCOUNT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("LOTCNT");
            //투입
            var inputCol = grdByArea.View.AddGroupColumn("INPUT");
			inputCol.AddTextBoxColumn("INPUTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
			inputCol.AddTextBoxColumn("INPUTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            inputCol.AddTextBoxColumn("INPUTM2", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            inputCol.AddTextBoxColumn("INPUTLOTCOUNT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("LOTCNT");

            //투입
            var waitForSendCol = grdByArea.View.AddGroupColumn("WAITFORSEND");
            waitForSendCol.AddTextBoxColumn("WAITFORSENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            waitForSendCol.AddTextBoxColumn("WAITFORSENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            waitForSendCol.AddTextBoxColumn("WAITFORSENDM2", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");
            waitForSendCol.AddTextBoxColumn("WAITFORSENDLOTCOUNT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("LOTCNT");

            grdByArea.View.PopulateColumns();
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
			defaultCol.AddTextBoxColumn("LOTTYPENAME", 60).SetTextAlignment(TextAlignment.Center).SetLabel("LOTTYPE");
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
            //중공정명
            defaultCol.AddTextBoxColumn("MIDDLEPROCESSSEGMENTNAME", 150);
            //공정명
			defaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
			//작업장
			defaultCol.AddTextBoxColumn("AREANAME", 150);
			//RTR/SHT
			defaultCol.AddTextBoxColumn("RTRSHT", 80).SetTextAlignment(TextAlignment.Center);
            //작업구분
            defaultCol.AddTextBoxColumn("STATENAME", 80);

            //기초
            var cntCol = grdByLot.View.AddGroupColumn("수량");
            cntCol.AddTextBoxColumn("PCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric).SetLabel("PCS");
            cntCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PNL");
            cntCol.AddTextBoxColumn("PCSMM", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("PCSMM");
            cntCol.AddTextBoxColumn("M2", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric).SetLabel("M2");

            var timeCol = grdByLot.View.AddGroupColumn("시간");
            timeCol.AddTextBoxColumn("STAYINGTIME", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#", MaskTypes.Numeric).SetLabel("STAYINGTIME_M");
            timeCol.AddTextBoxColumn("LEADTIME", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.#", MaskTypes.Numeric).SetLabel("LEADTIME_H");


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
            grdByArea.View.Columns["SENDM2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByArea.View.Columns["SENDM2"].SummaryItem.DisplayFormat = "{0:#,##0.##}";

            //현재공
            grdByArea.View.Columns["WIPPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["WIPPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByArea.View.Columns["WIPPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["WIPPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            grdByArea.View.Columns["WIPM2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByArea.View.Columns["WIPM2"].SummaryItem.DisplayFormat = "{0:#,##0.##}";

            //투입

            grdByArea.View.Columns["INPUTPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["INPUTPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByArea.View.Columns["INPUTPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["INPUTPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            grdByArea.View.Columns["INPUTM2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByArea.View.Columns["INPUTM2"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            //기초
            grdByArea.View.Columns["BASEPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["BASEPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
			grdByArea.View.Columns["BASEPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			grdByArea.View.Columns["BASEPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            grdByArea.View.Columns["BASEM2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByArea.View.Columns["BASEM2"].SummaryItem.DisplayFormat = "{0:#,##0.##}";

            //인계대기
            grdByArea.View.Columns["WAITFORSENDPCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByArea.View.Columns["WAITFORSENDPCSQTY"].SummaryItem.DisplayFormat = "{0:#,##0}";
            grdByArea.View.Columns["WAITFORSENDPANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByArea.View.Columns["WAITFORSENDPANELQTY"].SummaryItem.DisplayFormat = "{0:#,##0.##}";
            grdByArea.View.Columns["WAITFORSENDM2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdByArea.View.Columns["WAITFORSENDM2"].SummaryItem.DisplayFormat = "{0:#,##0.##}";



            grdByArea.View.OptionsView.ShowFooter = true;
			grdByArea.ShowStatusBar = false;
		}


		
		#endregion

		#region 이벤트
		private void InitializeEvent()
		{
			grdByArea.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
			//grdByProduct.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
			grdByLot.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdByArea.View.DoubleClick += grdByAreaView_DoubleClick;
            //grdByProduct.View.DoubleClick += grdByProductView_DoubleClick;
        }



        private void grdByAreaView_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grdByArea.View.FocusedRowHandle;

            if (rowHandle < 0) return;

            string areaId = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "AREAID"));
            string areaname = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "AREANAME"));

            string middleprocesssegmentid = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "MIDDLEPROCESSSEGMENTID"));
            string middleprocesssegmentname = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "PROCESSSEGMENTNAME"));

            string vendorid = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "VENDORID"));
            string vendorname = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "VENDORNAME"));

            

            string rtrsht = Format.GetFullTrimString(grdByArea.View.GetRowCellValue(rowHandle, "RTRSHT"));

            string currentColName = this.grdByArea.View.FocusedColumn.FieldName;

            if(currentColName.Equals("BASEPCSQTY") || currentColName.Equals("BASEPANELQTY") || currentColName.Equals("BASEM2"))
            {
                Conditions.GetControl<SmartComboBox>("P_COLSNAME").ItemIndex = 1;
            }
            else if (currentColName.Equals("INPUTPCSQTY") || currentColName.Equals("INPUTPANELQTY") || currentColName.Equals("INPUTM2") || currentColName.Equals("INPUTLOTCOUNT") )
            {
                Conditions.GetControl<SmartComboBox>("P_COLSNAME").ItemIndex = 4;
            }
            else if(currentColName.Equals("WIPPCSQTY") || currentColName.Equals("WIPPANELQTY") || currentColName.Equals("WIPM2") || currentColName.Equals("WIPLOTCOUNT"))
            {
                Conditions.GetControl<SmartComboBox>("P_COLSNAME").ItemIndex = 3;
            }
            else if(currentColName.Equals("SENDPCSQTY") || currentColName.Equals("SENDPANELQTY") || currentColName.Equals("SENDM2") || currentColName.Equals("SENDLOTCOUNT"))
            {
                Conditions.GetControl<SmartComboBox>("P_COLSNAME").ItemIndex = 2;
            }
            else if (currentColName.Equals("WAITFORSENDPCSQTY") || currentColName.Equals("WAITFORSENDPANELQTY") || currentColName.Equals("WAITFORSENDM2") || currentColName.Equals("WAITFORSENDLOTCOUNT"))
            {
                Conditions.GetControl<SmartComboBox>("P_COLSNAME").ItemIndex = 5;
            }
            else
            {
                ShowMessage("SelectDept");
                return;
            }

            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(areaId);
            Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = areaname;

            Conditions.GetControl<SmartSelectPopupEdit>("P_MIDDLEPROCESSSEGMENTCLASSID").SetValue(middleprocesssegmentid);
            Conditions.GetControl<SmartSelectPopupEdit>("P_MIDDLEPROCESSSEGMENTCLASSID").Text = middleprocesssegmentname;

            if (vendorname.Equals("영풍전자"))
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_VENDORID").SetValue("");
                Conditions.GetControl<SmartSelectPopupEdit>("P_VENDORID").Text = "";
            }
            else
            {
                Conditions.GetControl<SmartSelectPopupEdit>("P_VENDORID").SetValue(vendorid);
                Conditions.GetControl<SmartSelectPopupEdit>("P_VENDORID").Text = vendorname;
            }

            if (rtrsht.Equals("SHT"))
            {
                Conditions.GetControl<SmartComboBox>("P_RTRSHT").ItemIndex = 2;
            }
            else
            {
                Conditions.GetControl<SmartComboBox>("P_RTRSHT").ItemIndex = 1;
            }

            tabAreaWorkResult.SelectedTabPage = tabAreaWorkResult.TabPages[1];

            OnSearchAsync();
        }

        ///// <summary>
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
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			DataTable dtAreaWorkResult = new DataTable();
			int index = tabAreaWorkResult.SelectedTabPageIndex;
			values.Remove("TYPE");
			switch (index)
			{
				case 0:
					values.Add("TYPE", "BYAREA");
					dtAreaWorkResult = await QueryAsync("SelectAreaWorkResultList", "10001", values);

					if (dtAreaWorkResult.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByArea.DataSource = dtAreaWorkResult;
					break;
				case 1:
					values.Add("TYPE", "BYLOT");
					dtAreaWorkResult = await QueryAsync("SelectAreaWorkResultList_LOT", "10001", values);

					if (dtAreaWorkResult.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdByLot.DataSource = dtAreaWorkResult;
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

            //업체
            InitializeCondition_VendorPopup();

            //중공정
            InitializeCondition_MiddleProcesssegmentClassPopup();

            
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
				.SetPosition(4.8)
				.SetPopupResultCount(0);


			condition.Conditions.AddTextBox("PROCESSSEGMENTCLASS").SetLabel("TXTLOADMIDDLESEGMENTCLASS");

			//중공정ID
			condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100).SetLabel("MIDDLEPROCESSSEGMENTID");
			//중공정명
			condition.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200).SetLabel("MIDDLEPROCESSSEGMENTNAME");
        }

        /// <summary>
        /// 팝업형 조회조건 생성 - 업체명
        /// </summary>
        private void InitializeCondition_VendorPopup()
        {
            var condition = Conditions.AddSelectPopup("p_vendorid", new SqlQuery("GetVendor", "10001",  $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={ UserInfo.Current.Plant}") , "VENDORNAME" , "VENDORID")
                .SetPopupLayout("VENDORLIST", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupLayoutForm(600, 600)
                .SetPopupAutoFillColumns("VENDORID")
                .SetLabel("VENDORLIST")
                .SetPosition(4.9)
                .SetPopupResultCount(0);


            condition.Conditions.AddTextBox("VENDOR").SetLabel("TXTVENDOR");

            //업체ID
            condition.GridColumns.AddTextBoxColumn("VENDORID", 100).SetLabel("VENDORID");
            //업체명
            condition.GridColumns.AddTextBoxColumn("VENDORNAME", 300).SetLabel("VENDORNAME");
            //업체 주소
            condition.GridColumns.AddTextBoxColumn("ADDRESS", 200).SetLabel("ADDRESS");

        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            // 날짜 초기화
            // 조회일자
            DateTime originedate = DateTime.Parse(DateTime.Now.ToString());
            string hourMin = originedate.ToString("HH:mm");

            if (String.Compare(hourMin, "08:30") <= 0)
            {
                //변경작업
                Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodFr.EditValue = originedate.AddDays(-1).ToString("yyyy-MM-dd") + " " + "08:30";
                Conditions.GetControl<SmartPeriodEdit>("P_PERIOD").datePeriodTo.EditValue = originedate.ToString("yyyy-MM-dd") + " " + "08:30";
            }
        }
        #endregion

        protected override void OnToolbarClick(object sender, EventArgs e)
        {
            base.OnToolbarClick(sender, e);
            SmartButton btn = sender as SmartButton;
            if (btn.Name.ToString().Equals("Initialization"))
            {
                // 조회일자
              //  Conditions.GetControl<SmartDateEdit>("P_PERIOD").EditValue = DateTime.Now;

                // 작업장 ID
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").SetValue(string.Empty);
                Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").Text = string.Empty;

                // 품목코드
                //Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").SetValue(string.Empty);
                //Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").Text = string.Empty;

                // 품목명
                //Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").Text = string.Empty;

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
                grdByLot.DataSource = null;
            }
        }

        #region Private Function
        #endregion
    }
}