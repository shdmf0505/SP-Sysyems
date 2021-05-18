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
    /// 프 로 그 램 명  : 공정관리 > 재공관리 > 재공품 Aging 현황
    /// 업  무  설  명  : 재공품에 Aging 현황을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-09-30
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WIPAging : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public WIPAging()
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
            InitializeGridByItem();
            InitializeGridByLot();

            if(UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
            {
                seUnVisibleCoumn();
            }
        }

        /// <summary>        
        /// 품목별 Grid
        /// </summary>
        private void InitializeGridByItem()
        {
            grdAgingByProduct.GridButtonItem = GridButtonItem.Export;
			grdAgingByProduct.View.SetIsReadOnly();

			var grdstandardinfo = grdAgingByProduct.View.AddGroupColumn("");
            //SITE
            grdstandardinfo.AddTextBoxColumn("PLANTID", 80);
            //고객명
            grdstandardinfo.AddTextBoxColumn("CUSTOMERNAME", 100);
            //양산구분
            grdstandardinfo.AddTextBoxColumn("LOTTYPE", 60);
            //품목코드
            grdstandardinfo.AddTextBoxColumn("PRODUCTDEFID", 110);
            //버전
            grdstandardinfo.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("ITEMVERSION");
            //품목명
            grdstandardinfo.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            //공정
            grdstandardinfo.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            //보류
            grdstandardinfo.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            //1개월미만
            var grdlessonemonth = grdAgingByProduct.View.AddGroupColumn("LESSONEMONTH");
            grdlessonemonth.AddSpinEditColumn("LESS1MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdlessonemonth.AddSpinEditColumn("LESS1MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
			grdlessonemonth.AddSpinEditColumn("LESS1MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                grdlessonemonth.AddSpinEditColumn("LESS1MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");
            else
                grdlessonemonth.AddSpinEditColumn("LESS1MONTH_PRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");

            //1~3개월미만
            var grdlessonetohreemonth = grdAgingByProduct.View.AddGroupColumn("LESSONETOTHREEMONTH"); 
            grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
			grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");
            else
                grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_PRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");

            //3~6개월미만
            var grdlessthreemonth = grdAgingByProduct.View.AddGroupColumn("LESSTHREEMONTH"); 
            grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
			grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");
            else
                grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_PRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");

            //6개월이상
            var grdmoresixmonth = grdAgingByProduct.View.AddGroupColumn("MORESIXMONTH");
            grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
			grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");
            else
                grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_PRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");

            //총계
            var grdsum = grdAgingByProduct.View.AddGroupColumn("TOTAL");
            grdsum.AddSpinEditColumn("QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdsum.AddSpinEditColumn("PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
            grdsum.AddSpinEditColumn("MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                grdsum.AddSpinEditColumn("SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");
            else
                grdsum.AddSpinEditColumn("SUM_PRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("WIPPRICE");

            grdAgingByProduct.View.PopulateColumns();
			grdAgingByProduct.View.FixColumn(new string[] { "PLANTID", "CUSTOMERNAME", "LOTTYPE", "ISHOLD", "PRODUCTDEFID", "PRODUCTDEFNAME", "PROCESSSEGMENTNAME" });
		}


        private void seUnVisibleCoumn()
        {
            string[] UnVisibleField = { "LOTTYPE","PRODUCTDEFTYPE","WORKTYPE" ,"HOLDTOPCLASSID","HOLDCODE","PLANTID","HOLDCOMMENT","PROCESSLEADTIME","PROCESSLEADTIMEMONTH"};

            for(int i = 0; i<grdAgingByLot.View.Columns.Count; i++)
            {
                string colname = grdAgingByLot.View.Columns[i].FieldName;

                if(UnVisibleField.Contains<string>(colname))
                {
                    grdAgingByLot.View.Columns[i].Visible = false;
                }
            }
            grdAgingByLot.View.Bands["HOLD"].Visible = false;

        }
        /// <summary>        
        /// LOT별 Grid
        /// </summary>
        /// 
        private void InitializeGridByLot()
        {
            grdAgingByLot.GridButtonItem = GridButtonItem.Export;
			grdAgingByLot.View.SetIsReadOnly();

			var grdstandardinfo = grdAgingByLot.View.AddGroupColumn("");
            //품목코드
            grdstandardinfo.AddTextBoxColumn("PRODUCTDEFID", 110);
            //품목버전
            grdstandardinfo.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetLabel("ITEMVERSION");
            //생산구분
            grdstandardinfo.AddTextBoxColumn("LOTTYPE", 70).SetLabel("PRODUCTIONTYPE");
            //제품구분
            grdstandardinfo.AddTextBoxColumn("PRODUCTDEFTYPE", 70).SetLabel("THEPRODUCTTYPE");
            //품목명
            grdstandardinfo.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            //고객명
            grdstandardinfo.AddTextBoxColumn("CUSTOMERNAME", 80);
            //LOTNO.
            grdstandardinfo.AddTextBoxColumn("LOTID", 175);
            //PLANT
            grdstandardinfo.AddTextBoxColumn("PLANTID", 60);

            var grdstandardinfoNotFix = grdAgingByLot.View.AddGroupColumn("");
            //작업장
            grdstandardinfoNotFix.AddTextBoxColumn("AREANAME", 120);
            //공정명
            grdstandardinfoNotFix.AddTextBoxColumn("PROCESSSEGMENTNAME", 120);
            //공정투입일시
            grdstandardinfoNotFix.AddTextBoxColumn("PROCESSRECEIPTDATE", 120).SetTextAlignment(TextAlignment.Center).SetLabel("SEGMENTINPUTDATE");
            //LOT투입일시
            grdstandardinfoNotFix.AddTextBoxColumn("LOTSTARTDATE", 120).SetTextAlignment(TextAlignment.Center).SetLabel("LOTINPUTDATE");
            //보류구분
            grdstandardinfoNotFix.AddTextBoxColumn("ISHOLD", 50).SetTextAlignment(TextAlignment.Center);
            //SMT구분
            grdstandardinfoNotFix.AddTextBoxColumn("ISSMT", 50).SetTextAlignment(TextAlignment.Center);
            //작업구분
            grdstandardinfoNotFix.AddTextBoxColumn("WORKTYPE", 60).SetTextAlignment(TextAlignment.Center);

            var grdstandardinfoNHold = grdAgingByLot.View.AddGroupColumn("HOLD");
            //보류 대분류
            grdstandardinfoNHold.AddTextBoxColumn("HOLDTOPCLASSID", 100).SetTextAlignment(TextAlignment.Center).SetLabel("LARGECLASS");
            //보류 중분류
            grdstandardinfoNHold.AddTextBoxColumn("HOLDCODE", 100).SetTextAlignment(TextAlignment.Center).SetLabel("MIDDLECLASS");
            //상세 내용
            grdstandardinfoNHold.AddTextBoxColumn("HOLDCOMMENT", 150).SetTextAlignment(TextAlignment.Center).SetLabel("DETAILNOTE");
            //체공시간
            grdstandardinfoNotFix.AddTextBoxColumn("PROCESSLEADTIME", 60).SetTextAlignment(TextAlignment.Center).SetLabel("DELAYTIME");
            grdstandardinfoNotFix.AddTextBoxColumn("PROCESSLEADTIMEMONTH", 60).SetTextAlignment(TextAlignment.Center).SetLabel("MONTH");

            //1개월미만
            var grdlessonemonth = grdAgingByLot.View.AddGroupColumn("LESSONEMONTH");
            grdlessonemonth.AddSpinEditColumn("LESS1MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdlessonemonth.AddSpinEditColumn("LESS1MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
            grdlessonemonth.AddSpinEditColumn("LESS1MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
			grdlessonemonth.AddSpinEditColumn("LESS1MONTH_BAREPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("BAREPRICE");
			grdlessonemonth.AddSpinEditColumn("LESS1MONTH_SMTPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SMTPRICE");
			grdlessonemonth.AddSpinEditColumn("LESS1MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SUMMARYPRICE");

			//1~3개월미만
			var grdlessonetohreemonth = grdAgingByLot.View.AddGroupColumn("LESSONETOTHREEMONTH");
            grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
            grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
			grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_BAREPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("BAREPRICE");
			grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_SMTPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SMTPRICE");
			grdlessonetohreemonth.AddSpinEditColumn("LESS1TO3MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SUMMARYPRICE");

			//3~6개월미만
			var grdlessthreemonth = grdAgingByLot.View.AddGroupColumn("LESSTHREEMONTH");
            grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
            grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
			grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_BAREPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("BAREPRICE");
			grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_SMTPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SMTPRICE");
			grdlessthreemonth.AddSpinEditColumn("LESS3TO6MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SUMMARYPRICE");

			//6개월이상
			var grdmoresixmonth = grdAgingByLot.View.AddGroupColumn("MORESIXMONTH");
            grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
            grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
			grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_BAREPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("BAREPRICE");
			grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_SMTPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SMTPRICE");
			grdmoresixmonth.AddSpinEditColumn("MORE6MONTH_SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SUMMARYPRICE");

			//총계
			var grpTotal = grdAgingByLot.View.AddGroupColumn("TOTAL");
			grpTotal.AddSpinEditColumn("QTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("QTY");
            grpTotal.AddSpinEditColumn("PNLQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("PNLQTY");
            grpTotal.AddSpinEditColumn("MM", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true).SetLabel("MM");
			grpTotal.AddSpinEditColumn("BAREPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("BAREPRICE");
			grpTotal.AddSpinEditColumn("SMTPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SMTPRICE");
			grpTotal.AddSpinEditColumn("SUMMARYPRICE", 90).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetLabel("SUMMARYPRICE");

			grdAgingByLot.View.PopulateColumns();
			grdAgingByLot.View.FixColumn(new string[] { "PRODUCTDEFID", "PRODUCTDEFVERSION", "PRODUCTDEFNAME", "CUSTOMERNAME", "LOTID" });
		}

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_InterFlex))
            {
                QueryForInterFlex();
            }
            else
            {
                QueryForYoungPoong();
            }
            // TODO : 조회 SP 변경
            
		}

        private void QueryForInterFlex()
        {

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');

            int index = tabWipAging.SelectedTabPageIndex;
            values.Remove("TYPE");
            switch (index)
            {
                case 0://품목별
                    values.Add("TYPE", "PRODUCT");
                    DataTable dtWipAgingListByProduct = SqlExecuter.Query("SelectWipAgingList", "10001", values);
                    if (dtWipAgingListByProduct.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdAgingByProduct.DataSource = dtWipAgingListByProduct;
                    break;
                case 1://LOT별
                    DataTable dtWipAgingListByLot = SqlExecuter.Query("SelectWipAgingList", "10001", values);
                    if (dtWipAgingListByLot.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdAgingByLot.DataSource = dtWipAgingListByLot;
                    break;
            }
        }

        private void QueryForYoungPoong()
        {

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');

            int index = tabWipAging.SelectedTabPageIndex;

            switch (index)
            {
                case 0://품목별
                    DataTable dtWipAgingListByProduct = SqlExecuter.Query("SelectWipAgingProductList", "10001", values);
                    if (dtWipAgingListByProduct.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdAgingByProduct.DataSource = dtWipAgingListByProduct;
                    break;
                case 1://LOT별
                    DataTable dtWipAgingListByLot = SqlExecuter.Query("SelectWipAgingLotList", "10001", values);
                    if (dtWipAgingListByLot.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdAgingByLot.DataSource = dtWipAgingListByLot;
                    break;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목
            InitializeCondition_ProductPopup();
            // 고객
            InitializeConditionCustomer_Popup();
		}

        /// <summary>
		/// 팝업형 조회조건 생성 - 고객
		/// </summary>
		private void InitializeConditionCustomer_Popup()
        {
            var CustomerId = Conditions.AddSelectPopup("P_CUSTOMERID", new SqlQuery("SelectCustomerData", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("CUSTOMERNAME")
                .SetPosition(4.5)
                .SetLabel("CUSTOMER");

            CustomerId.Conditions.AddTextBox("TXTCUSTOMER").SetLabel("CUSTOMER");

            CustomerId.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            CustomerId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
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
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
        }

        #endregion
    }
}