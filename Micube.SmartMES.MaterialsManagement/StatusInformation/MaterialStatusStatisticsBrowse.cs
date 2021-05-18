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

namespace Micube.SmartMES.MaterialsManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 자재관리 > 자재현황 > 자재현황조회
    /// 업  무  설  명  : 자재현황을 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MaterialStatusStatisticsBrowse : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public MaterialStatusStatisticsBrowse()
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
            InitializeDetailInformationGrid();
            InitializationSummaryRow();
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaterialStatistics.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdMaterialStatistics.View.SetIsReadOnly(true);
            
            grdMaterialStatistics.View.AddTextBoxColumn("ENTERPRISEID", 10)                          //Tool번호
                .SetIsHidden();
            grdMaterialStatistics.View.AddTextBoxColumn("PLANTID", 10)                          //Tool번호
                .SetIsHidden();
            grdMaterialStatistics.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdMaterialStatistics.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 60)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdMaterialStatistics.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 380)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdMaterialStatistics.View.AddTextBoxColumn("UNIT", 60)
                .SetIsReadOnly(true);                                                  //품목코드
            grdMaterialStatistics.View.AddTextBoxColumn("CONSUMABLELOTID", 230)
                .SetIsReadOnly(true);
            grdMaterialStatistics.View.AddTextBoxColumn("AREAID", 10)                          //
                .SetIsHidden();
            grdMaterialStatistics.View.AddTextBoxColumn("CREATEDTIME", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")  
                .SetLabel("RECEIPTDATE")
                .SetIsHidden();

            //작업장        
            grdMaterialStatistics.View.AddTextBoxColumn("WAREHOUSEID", 60)
                .SetIsReadOnly(true);
            grdMaterialStatistics.View.AddTextBoxColumn("WAREHOUSENAME", 80)
                .SetIsReadOnly(true);
            grdMaterialStatistics.View.AddSpinEditColumn("BASEQTY", 120)
                .SetDisplayFormat("#,###,###,##0.#####")
                .SetIsReadOnly(true);                                                      //상태
            grdMaterialStatistics.View.AddSpinEditColumn("CSMINBOUNDQTY", 120)
                .SetDisplayFormat("#,###,###,##0.#####")
                .SetIsReadOnly(true);                                                  //상태            
            grdMaterialStatistics.View.AddSpinEditColumn("CSMOUTBOUNDQTY", 120)
                .SetDisplayFormat("#,###,###,##0.#####")
                .SetIsReadOnly(true);                                                  //상태            
            grdMaterialStatistics.View.AddSpinEditColumn("CSMWAITFORRECEIPTQTY", 120)
                .SetDisplayFormat("#,###,###,##0.#####")
                .SetIsReadOnly(true);                                                  //상태            
            grdMaterialStatistics.View.AddSpinEditColumn("CSMRESERVEQTY", 120)
                .SetDisplayFormat("#,###,###,##0.#####")
                .SetIsReadOnly(true);                                                  //상태            
            grdMaterialStatistics.View.AddSpinEditColumn("CSMONHANDQTY", 120)
                .SetDisplayFormat("#,###,###,##0.#####")
                .SetIsReadOnly(true);                                                  //상태            
            grdMaterialStatistics.View.AddSpinEditColumn("STOCKQTY", 120)
                .SetDisplayFormat("#,###,###,##0.#####")
                .SetIsReadOnly(true);                                                  //상태            

            grdMaterialStatistics.View.PopulateColumns();
        }
        #endregion

        #region InitializeDetailInformationGrid : 그리드를 초기화한다.
        private void InitializeDetailInformationGrid()
        {
            grdMaterialDetailInfo.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdMaterialDetailInfo.View.SetIsReadOnly(true);

            grdMaterialDetailInfo.View.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            grdMaterialDetailInfo.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 60);
            grdMaterialDetailInfo.View.AddTextBoxColumn("CONSUMABLELOTID", 180);
            grdMaterialDetailInfo.View.AddTextBoxColumn("TXNDATE", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            //grdMaterialDetailInfo.View.AddComboBoxColumn("TRANSACTIONTYPE", 100
            //   , new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConsumableTransactionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));  // 
            grdMaterialDetailInfo.View.AddTextBoxColumn("TRANSACTIONTYPENAME", 100);
            grdMaterialDetailInfo.View.AddTextBoxColumn("LOTID", 180);
            grdMaterialDetailInfo.View.AddTextBoxColumn("AREAID", 10)
                .SetIsHidden();
            grdMaterialDetailInfo.View.AddTextBoxColumn("AREANAME", 150);
            grdMaterialDetailInfo.View.AddTextBoxColumn("WAREHOUSEID", 60)
                .SetIsReadOnly(true);
            grdMaterialDetailInfo.View.AddTextBoxColumn("WAREHOUSENAME", 80)
                .SetIsReadOnly(true);
            grdMaterialDetailInfo.View.AddTextBoxColumn("RELATIONWAREHOUSEID", 60)
                .SetIsReadOnly(true);
            grdMaterialDetailInfo.View.AddTextBoxColumn("RELATIONWAREHOUSENAME", 80)
                .SetIsReadOnly(true);
            if (UserInfo.Current.Enterprise.Equals("YOUNGPOONG"))
            {
                
                grdMaterialDetailInfo.View.AddTextBoxColumn("COSTCENTERCODE", 80);
                grdMaterialDetailInfo.View.AddTextBoxColumn("COSTCENTERNAME", 150);

            }
            grdMaterialDetailInfo.View.AddSpinEditColumn("CSMINBOUNDQTY", 120)
                .SetDisplayFormat("#,###,###,###.#####");
            grdMaterialDetailInfo.View.AddSpinEditColumn("CSMOUTBOUNDQTY", 120)
                .SetDisplayFormat("#,###,###,###.#####");
            grdMaterialDetailInfo.View.AddSpinEditColumn("CSMRESERVEQTY", 120)
                .SetDisplayFormat("#,###,###,###.#####");
            grdMaterialDetailInfo.View.AddSpinEditColumn("CSMONHANDQTY", 120)
                .SetDisplayFormat("#,###,###,###.#####")
                .SetIsHidden();
            grdMaterialDetailInfo.View.AddTextBoxColumn("DESCRIPTION", 250);
            grdMaterialDetailInfo.View.AddTextBoxColumn("TRANSACTIONCODE", 10)
                .SetIsHidden();
            grdMaterialDetailInfo.View.AddTextBoxColumn("TRANSACTIONNAME", 180);
            grdMaterialDetailInfo.View.AddTextBoxColumn("TRANSACTIONREASONCODE", 10)
                .SetIsHidden();
            grdMaterialDetailInfo.View.AddTextBoxColumn("TRANSACTIONREASONCODENAME", 100);
            grdMaterialDetailInfo.View.AddTextBoxColumn("TXNADDEDCODE", 10)
                .SetIsHidden();
            grdMaterialDetailInfo.View.AddTextBoxColumn("TXNADDEDNAME", 150);
            grdMaterialDetailInfo.View.AddTextBoxColumn("ACTOR", 100)
               .SetTextAlignment(TextAlignment.Center);

            grdMaterialDetailInfo.View.PopulateColumns();
        }

        private void InitializationSummaryRow()
        {
            grdMaterialDetailInfo.View.Columns["AREANAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaterialDetailInfo.View.Columns["AREANAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdMaterialDetailInfo.View.Columns["CSMINBOUNDQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaterialDetailInfo.View.Columns["CSMINBOUNDQTY"].SummaryItem.DisplayFormat = "{0}";            
            grdMaterialDetailInfo.View.Columns["CSMOUTBOUNDQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaterialDetailInfo.View.Columns["CSMOUTBOUNDQTY"].SummaryItem.DisplayFormat = "{0}";
            grdMaterialDetailInfo.View.Columns["CSMRESERVEQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdMaterialDetailInfo.View.Columns["CSMRESERVEQTY"].SummaryItem.DisplayFormat = "{0}";

            grdMaterialDetailInfo.View.OptionsView.ShowFooter = true;
            grdMaterialDetailInfo.ShowStatusBar = false;
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdMaterialStatistics.View.FocusedRowChanged += grdSPStock_FocusedRowChanged;
            // grdMaterialStatistics.View.ColumnFilterChanged += View_ColumnFilterChanged;
            grdMaterialStatistics.View.RowCellClick += View_RowCellClick;
        }
        /// <summary>
        /// 그리드의 Row Click시 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            SearchDetailInformation();
        }
        private void View_ColumnFilterChanged(object sender, EventArgs e)
        {

            SearchDetailInformation();
        }
        private void grdSPStock_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchDetailInformation();
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

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
           
            values.Add("USERID", UserInfo.Current.Id.ToString());
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            #endregion
            #region 기간 검색형 전환 처리 
            if (!(values["P_CONPERIOD_PERIODFR"].ToString().Equals("")))
            {
                DateTime requestDateFr = Convert.ToDateTime(values["P_CONPERIOD_PERIODFR"]);
                values.Remove("P_CONPERIOD_PERIODFR");
                values.Add("P_CONPERIOD_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
            }
            if (!(values["P_CONPERIOD_PERIODTO"].ToString().Equals("")))
            {
                DateTime requestDateTo = Convert.ToDateTime(values["P_CONPERIOD_PERIODTO"]);
                values.Remove("P_CONPERIOD_PERIODTO");

                values.Add("P_CONPERIOD_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
            }
            //values.Add("USERID", UserInfo.Current.Id );


            #endregion
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable stockTable = SqlExecuter.Query("GetConsumablelotStatisticsByCsm", "10001", values);
            grdMaterialDetailInfo.View.ClearDatas();
            grdMaterialStatistics.DataSource = stockTable;
            string strConstatisticstype = values["CONSTATISTICSTYPE"].ToString();
            if (strConstatisticstype.Equals("CONSUMABLEONLY")) // 품목         AREANAME WAREHOUSEID CONSUMABLELOTID
            {
                grdMaterialStatistics.View.Columns["CREATEDTIME"].OwnerBand.Visible = false;
                grdMaterialStatistics.View.Columns["CREATEDTIME"].Visible = false;
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].OwnerBand.Visible = false;
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].Visible = false;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].OwnerBand.Visible = false;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].Visible = false;
            }
            else if (strConstatisticstype.Equals("CONSUMABLEANDLOT")) // 품목 + LOT   AREANAME WAREHOUSEID
            {
                grdMaterialStatistics.View.Columns["CREATEDTIME"].OwnerBand.Visible = true;
                grdMaterialStatistics.View.Columns["CREATEDTIME"].Visible = true;
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].OwnerBand.Visible = false;
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].Visible = false;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].OwnerBand.Visible = true;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].Visible = true;
            }
            else if (strConstatisticstype.Equals("CONSUMABLEANDAREA"))// 품목 + 창고코드
            {
                grdMaterialStatistics.View.Columns["CREATEDTIME"].OwnerBand.Visible = false;
                grdMaterialStatistics.View.Columns["CREATEDTIME"].Visible = false;
                
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].OwnerBand.Visible = true;
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].Visible = true;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].OwnerBand.Visible = false;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].Visible = false;
            }
            else if (strConstatisticstype.Equals("ALLITEM")) // 품목 + LOT + 창고코드
            {
                grdMaterialStatistics.View.Columns["CREATEDTIME"].OwnerBand.Visible = true;
                grdMaterialStatistics.View.Columns["CREATEDTIME"].Visible = true;
               
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].OwnerBand.Visible = true;
                grdMaterialStatistics.View.Columns["WAREHOUSENAME"].Visible = true;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].OwnerBand.Visible = true;
                grdMaterialStatistics.View.Columns["CONSUMABLELOTID"].Visible = true;
            }
            
            if (stockTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                if (stockTable.Rows.Count == 1)
                {
                    grdMaterialStatistics.View.FocusedRowHandle = 0;
                    SearchDetailInformation();
                }
                
            }
        }
        #endregion

        #region SearchDetailInformation - 상세내역을 조회한다.
        private void SearchDetailInformation()
        {
            if (grdMaterialStatistics.View.FocusedRowHandle > -1)
            {
                // TODO : 조회 SP 변경
                Dictionary<string, object> values = Conditions.GetValues();

                DataRow currentRow = grdMaterialStatistics.View.GetFocusedDataRow();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("P_ENTERPRISEID", UserInfo.Current.Enterprise.ToString());
                #endregion
                #region 기간 검색형 전환 처리 
                if (!(values["P_CONPERIOD_PERIODFR"].ToString().Equals("")))
                {
                    DateTime requestDateFr = Convert.ToDateTime(values["P_CONPERIOD_PERIODFR"]);
                    values.Remove("P_CONPERIOD_PERIODFR");
                    values.Add("P_CONPERIOD_PERIODFR", string.Format("{0:yyyy-MM-dd}", requestDateFr));
                }
                if (!(values["P_CONPERIOD_PERIODTO"].ToString().Equals("")))
                {
                    DateTime requestDateTo = Convert.ToDateTime(values["P_CONPERIOD_PERIODTO"]);
                    values.Remove("P_CONPERIOD_PERIODTO");

                    values.Add("P_CONPERIOD_PERIODTO", string.Format("{0:yyyy-MM-dd}", requestDateTo));
                }


                #endregion
                values.Add("CONSUMABLEDEFID_D", currentRow.GetString("CONSUMABLEDEFID"));
                values.Add("CONSUMABLEDEFVERSION_D", currentRow.GetString("CONSUMABLEDEFVERSION"));
                values.Add("CONSUMABLELOTID_D", currentRow.GetString("CONSUMABLELOTID"));
                values["CONSUMABLEDEFVERSION_D"] = currentRow.GetString("CONSUMABLEDEFVERSION");
                values["WAREHOUSEID_D"] = currentRow.GetString("WAREHOUSEID");

                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable inOutTable = SqlExecuter.Query("GetConsumablelotStatisticsDetailInfoByCsm", "10001", values);

                grdMaterialDetailInfo.DataSource = inOutTable;
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
            //제품구분 
            //
            InitializeConditionPopup_Consumableclass();
           
            InitializeConditionPopup_Consumabledefid();
            // 버전
            Conditions.AddComboBox("CONSUMABLEDEFVERSION", new SqlQuery("GetConsumabledefinitionVersionByCsm", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CONSUMABLEDEFVERSIONNAME", "CONSUMABLEDEFVERSION")
                .SetPosition(1.2)
                .SetEmptyItem("", "", true)
                ;
            var txtOspVendor = Conditions.AddTextBox("P_CONSUMABLELOTID")
                          .SetLabel("CONSUMABLELOTID")
                          .SetPosition(1.3);
            InitializeConditionPopup_Warehouseid();
            InitializeConditionPopup_IsStockqty();
            //재고수량 0
            InitializeStatisticsType();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용

            SmartSelectPopupEdit Popupedit = Conditions.GetControl<SmartSelectPopupEdit>("CONSUMABLEDEFID");
            Popupedit.Validating += Popupedit_Validating;
        }
        private void Popupedit_Validating(object sender, CancelEventArgs e)
        {
            SmartSelectPopupEdit Popupedit = (SmartSelectPopupEdit)sender;


            string sConsumabledefid = "";


            if (Popupedit.SelectedData.Count<DataRow>() == 0)
            {
                sConsumabledefid = "-1";
            }

            foreach (DataRow row in Popupedit.SelectedData)
            {
                sConsumabledefid = row["CONSUMABLEDEFID"].ToString();

            }

            SmartComboBox combobox = Conditions.GetControl<SmartComboBox>("CONSUMABLEDEFVERSION");
            combobox.DisplayMember = "CONSUMABLEDEFVERSION";
            combobox.ValueMember = "CONSUMABLEDEFVERSION";
           
            Dictionary<string, object> ParamIv = new Dictionary<string, object>();
            ParamIv.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            ParamIv.Add("PLANTID", UserInfo.Current.Plant);
            ParamIv.Add("CONSUMABLEDEFID", sConsumabledefid);
            DataTable dtIv = SqlExecuter.Query("GetConsumabledefinitionVersionByCsm", "10001", ParamIv);

            combobox.DataSource = dtIv;

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
        private void InitializeConditionPopup_Consumableclass()
        {
            var planttxtbox = Conditions.AddComboBox("P_CONSUMABLECLASSID", new SqlQuery("GetConsumableclassListByCsm", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
              .SetLabel("CONSUMABLECLASSID")
              .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
              .SetPosition(0.2)
              .SetEmptyItem("","");
            
        }
        #region InitializeConditionPopup_Consumabledefid : 
        /// <summary>
        /// 자재코드 조회조건
        /// </summary>
        private void InitializeConditionPopup_Consumabledefid()
        {
            var popupConsumabledef = Conditions.AddSelectPopup("CONSUMABLEDEFID",
                                                                new SqlQuery("GetConsumabledefinitionListByCsm", "10001"
                                                                                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                                , $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                                ), "CONSUMABLEDEFNAME", "CONSUMABLEDEFID")
                 .SetPopupLayout("CONSUMABLEDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                 .SetPopupLayoutForm(900, 600)
                 .SetLabel("CONSUMABLEDEFNAME")
                 
                 .SetPopupResultCount(1)
                 .SetPosition(1.1);
            // 팝업 조회조건

            popupConsumabledef.Conditions.AddComboBox("P_CONSUMABLECLASSID",
                new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                .SetLabel("CONSUMABLECLASSID")
                .SetEmptyItem("", "");
            popupConsumabledef.Conditions.AddTextBox("CONSUMABLEDEFNAME")
                .SetLabel("CONSUMABLEDEFNAME");


            popupConsumabledef.GridColumns.AddComboBoxColumn("CONSUMABLECLASSID", 100,
                new SqlQuery("GetConsumableclassListByCsm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID")
                .SetIsReadOnly();
            popupConsumabledef.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150).SetIsReadOnly();
            popupConsumabledef.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 100).SetIsReadOnly();
            popupConsumabledef.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 300).SetIsReadOnly();

        }
        #endregion

        #region InitializeConditionPopup_Areaid
        /// <summary>
        /// 작업장 조회조건
        /// </summary>
        private void InitializeConditionPopup_Warehouseid()
        {
            var popupProduct = Conditions.AddSelectPopup("P_WAREHOUSEID",
                                                              new SqlQuery("GetWarehouseidListByCsm", "10001"
                                                                             , $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                             , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                             , $"USERID={UserInfo.Current.Id}"
                                                                              ), "WAREHOUSENAME", "WAREHOUSEID")
               .SetPopupLayout("WAREHOUSENAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("WAREHOUSENAME")
               .SetPopupResultCount(1)
               .SetRelationIds("P_PLANTID")
               .SetPosition(1.4);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("P_WAREHOUSENAME")
                .SetLabel("WAREHOUSENAME");
            popupProduct.GridColumns.AddTextBoxColumn("WAREHOUSEID", 120)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 200);

            
        }
        #endregion

        #region InitializeStatisticsType : 집계구분
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializeStatisticsType()
        {

            var planttxtbox = Conditions.AddComboBox("CONSTATISTICSTYPE", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=CSMStatisticsSearchType"), "CODENAME", "CODEID")
               .SetLabel("CONSTATISTICSTYPE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(1.8)
               .SetDefault("ALLITEM")
            ;
        }

        /// <summary>
        ///
        /// </summary>
        private void InitializeConditionPopup_IsStockqty()
        {
            var plantCbobox = Conditions.AddComboBox("p_IsStockqty", new SqlQuery("GetCodeListNotCodebyStd", "10001", new Dictionary<string, object>() { { "CODECLASSID", "YesNo" }, { "NOTCODEID", "N" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "CODENAME", "CODEID")
              .SetLabel("ISSTOCKQTY")
              .SetPosition(2.8)
              .SetEmptyItem("", "")//기본값 설
              .SetDefault("Y")
               ;
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
