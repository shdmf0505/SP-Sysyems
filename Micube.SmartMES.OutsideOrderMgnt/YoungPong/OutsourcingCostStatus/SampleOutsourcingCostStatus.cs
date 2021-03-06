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
    /// 프 로 그 램 명  : 외주관리 > 외주가공비 마감 > 샘플 외주비 현황
    /// 업  무  설  명  : 샘플 외주비의 현황을 조회한다.
    /// 생    성    자  : 오근영
    /// 생    성    일  : 2021-03-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SampleOutsourcingCostStatus : SmartConditionManualBaseForm
    {
        #region Local Variables
        //string _searchVendorID;
        #endregion

        #region 생성자

        public SampleOutsourcingCostStatus()
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

            InitializeGrid();
            InitializationSummaryRow();

            InitializeDetailInfoGrid();
            InitializationDetailInfoSummaryRowDetailInfo();
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdOSAmount.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            //grdOSAmount.View.UseBandHeaderOnly = true;
            grdOSAmount.View.SetIsReadOnly(true);

            grdOSAmount.View.AddTextBoxColumn("OSPVENDORID", 150)           // 1 협력사 ID (Hidden)
                .SetIsHidden();
            grdOSAmount.View.AddTextBoxColumn("OSPVENDORNAME", 200)         // 2 협력사 명
                .SetIsReadOnly(true);
            grdOSAmount.View.AddSpinEditColumn("SPAMOUNT", 150)             // 3 정상 정산 (샘플)
                .SetIsReadOnly(true)
                .SetLabel("NORMALSETTLE");
            grdOSAmount.View.AddSpinEditColumn("OEWAMOUNT_SPAMOUNT", 150)   // 4 기타 실적 (샘플)
                .SetIsReadOnly(true)
                .SetLabel("OEWAMOUNT");
            grdOSAmount.View.AddSpinEditColumn("OEAAMOUNT_SPAMOUNT", 150)   // 5 기타 외주비 (샘플)
                .SetIsReadOnly(true)
                .SetLabel("OEAAMOUNT");

            grdOSAmount.View.PopulateColumns();
        }

        private void InitializationSummaryRow()
        {
            grdOSAmount.View.Columns["OSPVENDORNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmount.View.Columns["OSPVENDORNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdOSAmount.View.Columns["SPAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmount.View.Columns["SPAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOSAmount.View.Columns["OEWAMOUNT_SPAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmount.View.Columns["OEWAMOUNT_SPAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOSAmount.View.Columns["OEAAMOUNT_SPAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmount.View.Columns["OEAAMOUNT_SPAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOSAmount.View.OptionsView.ShowFooter = true;
            grdOSAmount.ShowStatusBar = false;
        }


        #endregion

        #region InitializeDetailInfoGrid : 그리드를 초기화한다.
        private void InitializeDetailInfoGrid()
        {
            grdOSAmountDetail.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdOSAmountDetail.View.SetIsReadOnly(true);
            //sendtime |productdefid  |productdefversion|productdefname |producttype|lotid |processsegmentid|processsegmentname|panelqty|pcsqty|pcsprice|panelprice|amount|
            grdOSAmountDetail.View.AddTextBoxColumn("SETTLEDATE", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("OSPAMOUNTYPENAME", 80)
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("PRODUCTDEFID", 150)
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly(true);
            //양품/샘플
            grdOSAmountDetail.View.AddTextBoxColumn("PRODUCESAMPLE", 80)
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("LOTID", 200)
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("PROCESSSEGMENTID", 80)
                .SetIsHidden();
            grdOSAmountDetail.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("PCSQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric); 
            grdOSAmountDetail.View.AddTextBoxColumn("PANELQTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            grdOSAmountDetail.View.AddTextBoxColumn("M2QTY", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.####", MaskTypes.Numeric);
            grdOSAmountDetail.View.AddTextBoxColumn("OSPSETTLEAMOUNT", 120)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##", MaskTypes.Numeric);
            grdOSAmountDetail.View.AddTextBoxColumn("VENDORID", 80)
                .SetIsReadOnly(true);
            grdOSAmountDetail.View.AddTextBoxColumn("VENDORNAME", 120)
                .SetIsReadOnly(true);

            grdOSAmountDetail.View.PopulateColumns();
        }

        private void InitializationDetailInfoSummaryRowDetailInfo()
        {
            grdOSAmountDetail.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmountDetail.View.Columns["PROCESSSEGMENTNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            grdOSAmountDetail.View.Columns["PCSQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmountDetail.View.Columns["PCSQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdOSAmountDetail.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmountDetail.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###.####}";

            grdOSAmountDetail.View.Columns["M2QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmountDetail.View.Columns["M2QTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOSAmountDetail.View.Columns["OSPSETTLEAMOUNT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdOSAmountDetail.View.Columns["OSPSETTLEAMOUNT"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdOSAmountDetail.View.OptionsView.ShowFooter = true;
            grdOSAmountDetail.ShowStatusBar = false;
        }


        #endregion
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdOSAmount.View.FocusedRowChanged += grdOSAmount_FocusedRowChanged;
        }

        private void grdOSAmount_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchDetailInfo();
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

        #region OnSearchAsync : 검색
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            if (values["P_OSPSTATUSTIME_PERIODFR"].ToString().Equals("") && values["P_OSPSTATUSTIME_PERIODTO"].ToString().Equals("")
                && values["P_YEARMONTH"].ToString().Equals(""))
            {
                ShowMessage("NoConditions_03"); // 검색조건중 (정산기간은 입력해야합니다.다국어
                return;
            }
            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("USERID", UserInfo.Current.Id.ToString());
            #endregion
            #region 기간 검색형 전환 처리 
            if (!(values["P_OSPSTATUSTIME_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_OSPSTATUSTIME_PERIODFR"]);
                values.Remove("P_OSPSTATUSTIME_PERIODFR");
                values.Add("P_OSPSTATUSTIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_OSPSTATUSTIME_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_OSPSTATUSTIME_PERIODTO"]);
                values.Remove("P_OSPSTATUSTIME_PERIODTO");

                values.Add("P_OSPSTATUSTIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }


            #endregion
            //values = Commons.CommonFunction.ConvertParameter(values);
            ////DataTable amountTable = SqlExecuter.Query("GetOutsourcingYoungPongAmountStatusByOsp", "10001", values);
            //DataTable amountTable = SqlExecuter.Query("SelectSampleOutsourcingCostStatus", "10001", values);

            //grdOSAmount.DataSource = amountTable;
            //grdOSAmountDetail.View.ClearDatas();
            //if (amountTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            //{
            //    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            //}
            //else if (amountTable.Rows.Count == 1)
            //{
            //    grdOSAmount.View.FocusedRowHandle = 0;
            //    grdOSAmount_FocusedRowChanged(grdOSAmount, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            //}

            DataTable detailInfoTable = SqlExecuter.Query("SelectSampleOutsourcingCostStatusDetail", "10001", values);
            grdOSAmountDetail.DataSource = detailInfoTable;
            if (detailInfoTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion

        #region SearchDetailInfo - 상세내역검색
        private void SearchDetailInfo()
        {
            if (grdOSAmount.View.FocusedRowHandle > -1)
            {
                // TODO : 조회 SP 변경
                Dictionary<string, object> values = Conditions.GetValues();

                DataRow currentRow = grdOSAmount.View.GetFocusedDataRow();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                #endregion
                values.Add("SELECTEDVENDORID", currentRow.GetString("OSPVENDORID"));

                #region 기간 검색형 전환 처리 
                if (!(values["P_OSPSTATUSTIME_PERIODFR"].ToString().Equals("")))
                {
                    DateTime requestDateFr = Convert.ToDateTime(values["P_OSPSTATUSTIME_PERIODFR"]);
                    values.Remove("P_OSPSTATUSTIME_PERIODFR");
                    values.Add("P_OSPSTATUSTIME_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
                }
                if (!(values["P_OSPSTATUSTIME_PERIODTO"].ToString().Equals("")))
                {
                    DateTime requestDateTo = Convert.ToDateTime(values["P_OSPSTATUSTIME_PERIODTO"]);
                    values.Remove("P_OSPSTATUSTIME_PERIODTO");

                    values.Add("P_OSPSTATUSTIME_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
                }


                #endregion
                values = Commons.CommonFunction.ConvertParameter(values);
                //DataTable detailInfoTable = SqlExecuter.Query("GetOutsourcingYoungPongAmountStatusDetailInfoByOsp", "10001", values);
                DataTable detailInfoTable = SqlExecuter.Query("SelectSampleOutsourcingCostStatusDetail", "10001", values);

                grdOSAmountDetail.DataSource = detailInfoTable;
            }
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // InitializePlant();
            InitializeConditionPopup_PeriodTypeOSP();
            InitializeCondition_Yearmonth();
            InitializeConditionPopup_OspVendorid();
            //품목코드
            InitializeConditionPopup_ProductDefId();
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
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용 		Id	"P_OSPSTATUSTIME"	string
            Conditions.GetControl<SmartPeriodEdit>("P_OSPSTATUSTIME").LanguageKey = "EXPSETTLEDATE";
        }

        #region InitializePlant : Site설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializePlant()
        {

            var planttxtbox = Conditions.AddComboBox("P_PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }

        #endregion
        /// <summary>
        ///외주실적
        /// </summary>
        private void InitializeConditionPopup_PeriodTypeOSP()
        {

            var owntypecbobox = Conditions.AddComboBox("p_PeriodType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=PeriodTypeOSP", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
               .SetLabel("PERIODTYPEOSP")
               .SetPosition(0.2)
               .SetDefault("")
               .SetEmptyItem("", "")//
            ;
        }
        #region InitializeVendors : 팝업창 제어
        /// <summary>
        /// 작업장
        /// </summary>
        private void InitializeConditionPopup_OspAreaid()
        {
            // 팝업 컬럼설정
            var popupProduct = Conditions.AddSelectPopup("p_areaid",
                                                                new SqlQuery("GetAreaidListAuthorityByOsp", "10001"
                                                                                , $"LANGUAGETYPE={ UserInfo.Current.LanguageType }"
                                                                                , $"USERID={UserInfo.Current.Id}"
                                                                                , $"ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "AREANAME", "AREAID")
             .SetPopupLayout("AREAID", PopupButtonStyles.Ok_Cancel, true, false)
             .SetPopupLayoutForm(450, 600)
             .SetLabel("AREANAME")
             .SetPopupResultCount(1)
             .SetRelationIds("p_plantid")
              .SetPosition(0.4);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("P_AREANAME")
                .SetLabel("AREANAME");

            // 팝업 그리드
            popupProduct.GridColumns.AddTextBoxColumn("AREAID", 120);
            popupProduct.GridColumns.AddTextBoxColumn("AREANAME", 200);


        }
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeConditionPopup_OspVendorid()
        {
            // 팝업 컬럼설정
            var vendoridPopupColumn = Conditions.AddSelectPopup("VENDORID", new SqlQuery("GetVendorListByOsp", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "OSPVENDORNAME", "OSPVENDORID")
               .SetPopupLayout("OSPVENDORID", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(500, 600)
               .SetLabel("OSPVENDORID")
               .SetRelationIds("p_plantid")
               .SetPopupResultCount(1)
               .SetPosition(2.5);

            // 팝업 조회조건
            vendoridPopupColumn.Conditions.AddTextBox("OSPVENDORNAME")
                .SetLabel("OSPVENDORNAME");

            // 팝업 그리드
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORID", 150)
                .SetValidationKeyColumn();
            vendoridPopupColumn.GridColumns.AddTextBoxColumn("OSPVENDORNAME", 200);

            var txtOspVendor = Conditions.AddTextBox("P_OSPVENDORNAME")
               .SetLabel("OSPVENDORNAME")
               .SetPosition(2.6);

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
                 .SetPosition(2.1);
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
                .SetPosition(2.2);
        }
        #endregion

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
