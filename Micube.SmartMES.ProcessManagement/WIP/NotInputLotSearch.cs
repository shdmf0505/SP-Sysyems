#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using Micube.SmartMES.Commons;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 투입관리 > 미투입 현황
    /// 업  무  설  명  : 미투입 Lot 리스트를 조회 한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-09-06
    /// 수  정  이  력  : 2019-10-23 강호윤 - 디자인, 로직 수정
    /// 
    /// </summary>
    public partial class NotInputLotSearch : SmartConditionManualBaseForm
    {
        #region Private Variables

        #endregion

        #region 생성자

        public NotInputLotSearch()
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

            InitializeNotInputListGrid();
            InitializeNotInputMaterialRequirementGrid();
            InitializeNotInputSalesOrderGrid();
            InitializationSummaryRow();
            btnPrint.Visible = false;
            this.tabNotInputList.TabPages[1].PageVisible = false;
          
        }

        private void InitializeNotInputListGrid()
        {
            grdNotInputList.GridButtonItem = GridButtonItem.Export;
            
            grdNotInputList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdNotInputList.View.SetIsReadOnly();

            // 품목코드
            grdNotInputList.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            // 품목버전
            grdNotInputList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목명
            grdNotInputList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 고객사ID
            grdNotInputList.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();
            // 고객사
            grdNotInputList.View.AddTextBoxColumn("CUSTOMERNAME", 120)
                .SetLabel("CUSTOMER");
            // 품목구분
            grdNotInputList.View.AddTextBoxColumn("PRODUCTDEFTYPE", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 생산구분
            grdNotInputList.View.AddTextBoxColumn("PRODUCTIONTYPE", 80)
                .SetTextAlignment(TextAlignment.Center);
            // Layer
            grdNotInputList.View.AddTextBoxColumn("LAYER", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 합수
            grdNotInputList.View.AddSpinEditColumn("PCSPNL", 70)
                .SetLabel("ARRAY");
            // 산출수
            grdNotInputList.View.AddSpinEditColumn("PCSMM", 70)
                .SetLabel("CALCULATION");
            // PCS
            grdNotInputList.View.AddSpinEditColumn("QTY", 80)
                .SetLabel("PCS");
            // PNL
            grdNotInputList.View.AddSpinEditColumn("PANELQTY", 80)
                .SetLabel("PNL");
            // M2
            grdNotInputList.View.AddSpinEditColumn("M2", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            // 금액(억)
            grdNotInputList.View.AddSpinEditColumn("SALESPRICE", 80)
                .SetLabel("SALESPRICEPERHUNDREDMILLION")
                .SetDisplayFormat("#,##0.#####");
            // Roll/Sheet
            grdNotInputList.View.AddTextBoxColumn("RTRSHT", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 투입공정
            grdNotInputList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetLabel("INPUTPROCESSSEGMENT");


            grdNotInputList.View.PopulateColumns();


            grdNotInputList.View.OptionsView.ShowIndicator = false;
        }

        private void InitializeNotInputMaterialRequirementGrid()
        {
            grdNotInputMaterialRequirement.GridButtonItem = GridButtonItem.Export;

            grdNotInputMaterialRequirement.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdNotInputMaterialRequirement.View.SetIsReadOnly();

            // Key Column
            grdNotInputMaterialRequirement.View.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsHidden();
            // 품목코드
            grdNotInputMaterialRequirement.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목버전
            grdNotInputMaterialRequirement.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목명
            grdNotInputMaterialRequirement.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 자재ID
            grdNotInputMaterialRequirement.View.AddTextBoxColumn("CONSUMABLEDEFID", 120)
                .SetIsHidden();
            // 자재버전
            grdNotInputMaterialRequirement.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 70)
                .SetIsHidden();
            // MAIN BASE
            grdNotInputMaterialRequirement.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetLabel("MAINBASE");
            // 미투입수량(PCS)
            grdNotInputMaterialRequirement.View.AddSpinEditColumn("NOTINPUTPCSQTY", 90);
            // 미투입수량(PNL)
            grdNotInputMaterialRequirement.View.AddSpinEditColumn("NOTINPUTPNLQTY", 90);
            // 자재소요량
            grdNotInputMaterialRequirement.View.AddSpinEditColumn("MATERIALREQUIREMENTQTY", 90)
                .SetLabel("MATERIALREQUIREQTY");


            grdNotInputMaterialRequirement.View.PopulateColumns();


            grdNotInputMaterialRequirement.View.OptionsView.ShowIndicator = false;

            grdNotInputMaterialRequirement.View.OptionsDetail.ShowDetailTabs = false;


            SmartGridControl mainGrid = grdNotInputMaterialRequirement.GridControl;

            SmartBandedGridView detailView = new SmartBandedGridView(mainGrid);
            mainGrid.LevelTree.Nodes.Add("ConsumableList", detailView);
            detailView.ViewCaption = "Consumable List";

            detailView.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            detailView.SetIsReadOnly();

            // Key Column
            detailView.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsHidden();
            // 품목코드
            detailView.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsHidden();
            // 품목버전
            detailView.AddTextBoxColumn("PRODUCTDEFVERSION", 70)
                .SetIsHidden();
            // 자재코드
            detailView.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            // 자재버전
            detailView.AddTextBoxColumn("CONSUMABLEDEFVERSION", 70);
            // 자재명
            detailView.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            // 소요량
            detailView.AddSpinEditColumn("QTY", 90)
                .SetLabel("REQUIREMENTQTY");


            detailView.PopulateColumns();

            detailView.OptionsView.ShowIndicator = false;
        }

        private void InitializeNotInputSalesOrderGrid()
        {
            grdNotInputSalesOrder.GridButtonItem = GridButtonItem.Export;

            grdNotInputSalesOrder.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdNotInputSalesOrder.View.SetIsReadOnly();

            // 생산의뢰 번호
            grdNotInputSalesOrder.View.AddTextBoxColumn("PRODUCTIONORDERID", 100)
                .SetLabel("PRODUCTIONREQUESTNO")
                .SetTextAlignment(TextAlignment.Center);
            // 수주 번호
            grdNotInputSalesOrder.View.AddTextBoxColumn("ERPSALESORDERNO", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("SALESORDERID");

            // 라인
            grdNotInputSalesOrder.View.AddTextBoxColumn("LINENO", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 승인
            grdNotInputSalesOrder.View.AddTextBoxColumn("ISAPPROVAL", 60)
                .SetTextAlignment(TextAlignment.Center);
            // Lot생성여부
            grdNotInputSalesOrder.View.AddComboBoxColumn("ISLOTCREATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // 품목코드
            grdNotInputSalesOrder.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            // 품목버전
            grdNotInputSalesOrder.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목명
            grdNotInputSalesOrder.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 고객사ID
            grdNotInputSalesOrder.View.AddTextBoxColumn("CUSTOMERID", 80)
                .SetIsHidden();
            // 고객사
            grdNotInputSalesOrder.View.AddTextBoxColumn("CUSTOMERNAME", 120)
                .SetLabel("CUSTOMER");
            // 품목구분
            grdNotInputSalesOrder.View.AddTextBoxColumn("PRODUCTDEFTYPE", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 생산구분
            grdNotInputSalesOrder.View.AddTextBoxColumn("PRODUCTIONTYPE", 80)
                .SetTextAlignment(TextAlignment.Center);
            // Layer
           
            grdNotInputSalesOrder.View.AddComboBoxColumn("LAYER", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=Layer", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // 합수
            grdNotInputSalesOrder.View.AddSpinEditColumn("PCSPNL", 70)
                .SetLabel("ARRAY");
            // 산출수
            grdNotInputSalesOrder.View.AddSpinEditColumn("PCSMM", 70)
                .SetLabel("CALCULATION");
            // PCS
            grdNotInputSalesOrder.View.AddSpinEditColumn("QTY", 80)
                .SetLabel("PCS");
            // PNL
            grdNotInputSalesOrder.View.AddSpinEditColumn("PANELQTY", 80)
                .SetLabel("PNL");
            // M2
            grdNotInputSalesOrder.View.AddSpinEditColumn("M2", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            // 금액(억)
            grdNotInputSalesOrder.View.AddSpinEditColumn("SALESPRICE", 80)
                .SetLabel("SALESPRICEPERHUNDREDMILLION")
                .SetDisplayFormat("#,##0.#####");
            // Roll/Sheet
            grdNotInputSalesOrder.View.AddTextBoxColumn("RTRSHT", 80)
                .SetTextAlignment(TextAlignment.Center);
            // 투입공정
            grdNotInputSalesOrder.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetLabel("INPUTPROCESSSEGMENT");
            grdNotInputSalesOrder.View.PopulateColumns();
            grdNotInputSalesOrder.View.OptionsView.ShowIndicator = false;
        }

        private void InitializationSummaryRow()
        {
            // 미투입 List 
            grdNotInputList.View.AddSpinEditColumn("PCSPNL", 70)
                .SetLabel("ARRAY");
            // 산출수
            grdNotInputList.View.AddSpinEditColumn("PCSMM", 70)
                .SetLabel("CALCULATION");
            // PCS
            grdNotInputList.View.AddSpinEditColumn("QTY", 80)
                .SetLabel("PCS");
            // PNL
            grdNotInputList.View.AddSpinEditColumn("PANELQTY", 80)
                .SetLabel("PNL");
            // M2
            grdNotInputList.View.AddSpinEditColumn("M2", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            // 금액(억)
            grdNotInputList.View.AddSpinEditColumn("SALESPRICE", 80)
                .SetLabel("SALESPRICEPERHUNDREDMILLION")
                .SetDisplayFormat("#,##0.#####");

            grdNotInputList.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputList.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            //grdNotInputList.View.Columns["PCSPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdNotInputList.View.Columns["PCSPNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            //grdNotInputList.View.Columns["PCSMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdNotInputList.View.Columns["PCSMM"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputList.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputList.View.Columns["QTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputList.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputList.View.Columns["M2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputList.View.Columns["M2"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputList.View.Columns["SALESPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputList.View.Columns["SALESPRICE"].SummaryItem.DisplayFormat = "{0:###,##0.##}";

            grdNotInputList.View.OptionsView.ShowFooter = true;
            grdNotInputList.ShowStatusBar = false;

            // 미투입 List (수주+품목)
            grdNotInputSalesOrder.View.AddSpinEditColumn("PCSPNL", 70)
                .SetLabel("ARRAY");
            // 산출수
            grdNotInputSalesOrder.View.AddSpinEditColumn("PCSMM", 70)
                .SetLabel("CALCULATION");
            // PCS
            grdNotInputSalesOrder.View.AddSpinEditColumn("QTY", 80)
                .SetLabel("PCS");
            // PNL
            grdNotInputSalesOrder.View.AddSpinEditColumn("PANELQTY", 80)
                .SetLabel("PNL");
            // M2
            grdNotInputSalesOrder.View.AddSpinEditColumn("M2", 80).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
            // 금액(억)
            grdNotInputSalesOrder.View.AddSpinEditColumn("SALESPRICE", 80)
                .SetLabel("SALESPRICEPERHUNDREDMILLION")
                .SetDisplayFormat("#,##0.#####");

            grdNotInputSalesOrder.View.Columns["PRODUCTDEFNAME"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputSalesOrder.View.Columns["PRODUCTDEFNAME"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));

            //grdNotInputSalesOrder.View.Columns["PCSPNL"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdNotInputSalesOrder.View.Columns["PCSPNL"].SummaryItem.DisplayFormat = "{0:###,###}";

            //grdNotInputSalesOrder.View.Columns["PCSMM"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //grdNotInputSalesOrder.View.Columns["PCSMM"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputSalesOrder.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputSalesOrder.View.Columns["QTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputSalesOrder.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputSalesOrder.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputSalesOrder.View.Columns["M2"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputSalesOrder.View.Columns["M2"].SummaryItem.DisplayFormat = "{0:###,###}";

            grdNotInputSalesOrder.View.Columns["SALESPRICE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdNotInputSalesOrder.View.Columns["SALESPRICE"].SummaryItem.DisplayFormat = "{0:###,##0.##}";

            grdNotInputSalesOrder.View.OptionsView.ShowFooter = true;
            grdNotInputSalesOrder.ShowStatusBar = false;
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            tabNotInputList.SelectedPageChanged += TabNotInputList_SelectedPageChanged;

            btnPrint.Click += BtnPrint_Click;
            grdNotInputList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdNotInputSalesOrder.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
        }
        private void View_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Font ft = new Font("Mangul Gothic", 10F, FontStyle.Bold);
            e.Appearance.BackColor = Color.LightBlue;
            e.Appearance.FillRectangle(e.Cache, e.Bounds);
            e.Info.AllowDrawBackground = false;
            e.Appearance.Font = ft;
        }
        private void TabNotInputList_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tpgNotInputList)
            {
                btnPrint.Visible = false;
            }
            else if (e.Page == tpgNotInputMaterialRequirement)
            {
                btnPrint.Visible = true;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            // BOM 구성품 기준 합산 출력 기능 추가
        }

        #endregion

        #region 툴바

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

            var values = Conditions.GetValues();
            
            if (tabNotInputList.SelectedTabPage == tpgNotInputList)
            {
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable notInputList = SqlExecuter.Query("SelectNotInputList", "10001", values);

                if (notInputList.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdNotInputList.DataSource = notInputList;
            }
            else if (tabNotInputList.SelectedTabPage == tpgNotInputMaterialRequirement)
            {
                DataTable notInputMainBaseList = SqlExecuter.Query("SelectNotInputMaterialRequirementByMainBase", "10001", values);

                if (notInputMainBaseList.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");

                    grdNotInputMaterialRequirement.DataSource = notInputMainBaseList;

                    return;
                }

                string productList = string.Join(",", notInputMainBaseList.AsEnumerable().Select(row => string.Join("|", Format.GetString(row["KEYCOLUMN"]), Format.GetString(row["CONSUMABLEDEFID"]))));
                values.Add("PRODUCTLIST", productList);

                DataTable notInputConsumableList = SqlExecuter.Query("SelectNotInputMaterialRequirementByConsumable", "10002", values);

                DataTable mainList = notInputMainBaseList.Clone();
                mainList.TableName = "MainBase";
                DataTable consumableList = notInputConsumableList.Clone();
                consumableList.TableName = "Consumable";

                notInputMainBaseList.AsEnumerable().ForEach(row =>
                {
                    DataRow newRow = mainList.NewRow();
                    newRow.ItemArray = row.ItemArray.Clone() as object[];

                    mainList.Rows.Add(newRow);
                });

                notInputConsumableList.AsEnumerable().ForEach(row =>
                {
                    DataRow newRow = consumableList.NewRow();
                    newRow.ItemArray = row.ItemArray.Clone() as object[];

                    consumableList.Rows.Add(newRow);
                });

                DataSet dataSource = new DataSet();
                dataSource.Tables.Add(mainList);
                dataSource.Tables.Add(consumableList);

                dataSource.Relations.Add("ConsumableList", mainList.Columns["KEYCOLUMN"], consumableList.Columns["KEYCOLUMN"]);

                grdNotInputMaterialRequirement.DataSource = dataSource.Tables["MainBase"];
            }
            else if (tabNotInputList.SelectedTabPage == tpgNotInputSalesOrder)
            {
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable notInputSalesOrder = SqlExecuter.Query("SelectNotInputListBySalseOrder", "10001", values);

                if (notInputSalesOrder.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdNotInputSalesOrder.DataSource = notInputSalesOrder;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 품목코드
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 3.5, false, Conditions);

            // 고객
            var customerIdPopup = this.Conditions.AddSelectPopup("P_CUSTOMERID", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
                .SetPopupLayout(Language.Get("SELECTCUSTOMERID"), PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 800)
                .SetPopupAutoFillColumns("CUSTOMERNAME")
                .SetPosition(4.5)
                .SetLabel("CUSTOMER");

            customerIdPopup.Conditions.AddTextBox("TXTCUSTOMERID");

            customerIdPopup.GridColumns.AddTextBoxColumn("CUSTOMERID", 100);
            customerIdPopup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        #region 유효성 검사

        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region Private Function

        #endregion

    }
}
