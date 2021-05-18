#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    ///     프 로 그 램 명  : 공정관리 > 투입관리 > 투입지시리스트
    ///     업  무  설  명  :
    ///     생    성    자  : 조혜인
    ///     생    성    일  : 2019-10-22
    ///     수  정  이  력  :
    /// </summary>
    public partial class InputLotList : SmartConditionManualBaseForm
    {
        #region 생성자

        public InputLotList()
        {
            InitializeComponent();
            isCheckReadyProduct = false;
            isCheckResultProduct = false;
        }

        #endregion

        #region 툴바

        /// <summary>
        ///     저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {

            //xtraTabPageInputResult
            if (tabMain.SelectedTabPage.Name.Equals("xtraTabPageInputResult")) return;

            base.OnToolbarSaveClick();

            DataTable dt = grdLotList.DataSource as DataTable;

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker worker = new MessageWorker("SaveInputConfirm");
                worker.SetBody(new MessageBody()
                {
                    { "EnterpriseID", UserInfo.Current.Enterprise },
                    { "PlantID", UserInfo.Current.Plant },
                    { "UserId", UserInfo.Current.Id },
                    { "Lotlist", dt },
                });

                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }

            if (chkPrintLotCard.Checked)
            {
                string lotId = "";
                DataTable dtlot = grdLotList.DataSource as DataTable;
                dtlot.Rows.Cast<DataRow>().ForEach(row =>
                {
                    lotId += row["LOTID"].ToString() + ",";
                });

                lotId = lotId.Substring(0, lotId.Length - 1);

                CommonFunction.PrintLotCard(lotId, LotCardType.Normal);
            }
            DataRow dr = grdInputList.View.GetFocusedDataRow() ;
            var param = dr.Table.Columns
                .Cast<DataColumn>()
                .ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
            OpenMenu("PG-SG-0720", param);
            
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        ///     데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            if (grdInputList.View.FocusedRowHandle < 0)
            {
                throw MessageException.Create("NoSaveData");
            }
            // TODO : 유효성 로직 변경
        }

        #endregion

        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private bool isCheckReadyProduct { get; set; }
        private bool isCheckResultProduct { get; set; }
        private FlashedCellsHelper flashedCellsHelper { get; set; }
        private Dictionary<string, object> conditionValues { get; set; }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        ///     화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            UseAutoWaitArea = false;

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();

            Initialize();

            InitializeEvent();
        }

        private void Initialize()                   
        {
            conditionValues = new Dictionary<string, object>();

            //dateTo.DateTime = DateTime.Today;
            flashedCellsHelper = new FlashedCellsHelper(grdInputList.View);
        }

        /// <summary>
        ///     코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가

            #region 투입대기

            //InputList
            grdInputList.GridButtonItem = GridButtonItem.Export;
            grdInputList.ShowButtonBar = true;

            grdInputList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //상태
            grdInputList.View.AddTextBoxColumn("STATE", 80).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //투입일시
            grdInputList.View.AddTextBoxColumn("INPUTDATE", 100).SetIsReadOnly();
            // 양산구분
            grdInputList.View.AddTextBoxColumn("LOTTYPE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //품목 코드
            grdInputList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
            //REVISION
            grdInputList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetIsReadOnly().SetLabel("ITEMVERSION");
            // 품목명
            grdInputList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            //투입공정ID
            grdInputList.View.AddTextBoxColumn("INPUTSEGMENTID").SetIsHidden();
            //투입공정
            grdInputList.View.AddTextBoxColumn("INPUTSEGMENT", 120).SetIsReadOnly();
            //투입작업장ID
            grdInputList.View.AddTextBoxColumn("AREAID", 60).SetIsReadOnly().SetIsHidden();
            //투입작업장
            grdInputList.View.AddTextBoxColumn("AREANAME", 100).SetIsReadOnly();
            //투입PCS
            grdInputList.View.AddSpinEditColumn("INPUTPCS", 60).SetIsReadOnly();
            //투입PNL
            grdInputList.View.AddSpinEditColumn("INPUTPNL", 60).SetIsReadOnly();
            //LOT 수
            grdInputList.View.AddTextBoxColumn("LOTCNT", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //투입자재명
            grdInputList.View.AddTextBoxColumn("INPUTMATERIALNAME", 200).SetIsReadOnly();
            //자재소요량
            grdInputList.View.AddTextBoxColumn("MATERIALREQUIREQTY", 80).SetIsReadOnly();
            //전표출력
            grdInputList.View.AddTextBoxColumn("DOCUMENTOUTPUT", 80).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //투입담당ID
            grdInputList.View.AddTextBoxColumn("INPUTCHARGEID").SetIsHidden();
            //투입담당
            grdInputList.View.AddTextBoxColumn("INPUTCHARGE", 80).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //수주NO
            grdInputList.View.AddTextBoxColumn("SALESORDERID", 80).SetIsReadOnly();

            grdInputList.View.PopulateColumns();

            //LostList
            grdLotList.GridButtonItem = GridButtonItem.Export;

            //grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //LOT NO
            grdLotList.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly();
            //공정순서
            grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 60).SetIsReadOnly();
            //공정명
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250).SetIsReadOnly();
            //작업장
            grdLotList.View.AddTextBoxColumn("AREANAME", 150).SetIsReadOnly();
            //투입PCS
            grdLotList.View.AddSpinEditColumn("INPUTPCS", 70).SetIsReadOnly();
            //투입PNL
            grdLotList.View.AddSpinEditColumn("INPUTPNL", 70).SetIsReadOnly();
            //투입자재명
            grdLotList.View.AddTextBoxColumn("INPUTMATERIALNAME", 200).SetIsReadOnly();
            //자재소요량
            grdLotList.View.AddTextBoxColumn("MATERIALREQUIREQTY", 100).SetIsReadOnly();

            //랏카드출력여부
            grdLotList.View.AddTextBoxColumn("ISPRINTLOTCARD", 60).SetIsReadOnly();
            grdLotList.View.PopulateColumns();

            #endregion

            #region 투입실적

            //input result
            grdInputResult.GridButtonItem = GridButtonItem.Export;
            grdInputResult.ShowButtonBar = true;

            grdInputResult.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            //투입일시
            grdInputResult.View.AddTextBoxColumn("INPUTDATE", 100).SetIsReadOnly();
            // 양산구분
            grdInputResult.View.AddTextBoxColumn("LOTTYPE", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //품목 코드
            grdInputResult.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetIsReadOnly();
            //REVISION
            grdInputResult.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetIsReadOnly().SetLabel("ITEMVERSION");
            // 품목명
            grdInputResult.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            //투입공정ID
            grdInputResult.View.AddTextBoxColumn("INPUTSEGMENTID").SetIsHidden();
            //투입공정
            grdInputResult.View.AddTextBoxColumn("INPUTSEGMENT", 120).SetIsReadOnly();
            //투입작업장ID
            grdInputResult.View.AddTextBoxColumn("AREAID", 60).SetIsReadOnly().SetIsHidden();
            //투입작업장
            grdInputResult.View.AddTextBoxColumn("AREANAME", 100).SetIsReadOnly();
            //투입PCS
            grdInputResult.View.AddSpinEditColumn("INPUTPCS", 60).SetIsReadOnly();
            //투입PNL
            grdInputResult.View.AddSpinEditColumn("INPUTPNL", 60).SetIsReadOnly();
            //LOT 수
            grdInputResult.View.AddTextBoxColumn("LOTCNT", 60).SetIsReadOnly().SetTextAlignment(TextAlignment.Center);
            //투입자재명
            grdInputResult.View.AddTextBoxColumn("INPUTMATERIALNAME", 200).SetIsReadOnly();
            //자재소요량
            grdInputResult.View.AddTextBoxColumn("MATERIALREQUIREQTY", 80).SetIsReadOnly();
            //전표출력
            grdInputResult.View.AddTextBoxColumn("DOCUMENTOUTPUT", 80).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //현장담당
            grdInputResult.View.AddTextBoxColumn("FIELDCHARGE", 80).SetIsReadOnly();
            //투입담당ID
            grdInputResult.View.AddTextBoxColumn("INPUTCHARGEID").SetIsHidden();
            //투입담당
            grdInputResult.View.AddTextBoxColumn("INPUTCHARGE", 80).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //현장확인 
            grdInputResult.View.AddTextBoxColumn("FIELDCHECKTIME", 100).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //자재불출 
            grdInputResult.View.AddTextBoxColumn("MATERIALDISCHARGE", 100).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //인수 
            grdInputResult.View.AddTextBoxColumn("RECEIVETIME", 100).SetIsReadOnly().SetLabel("ACCEPT")
                .SetTextAlignment(TextAlignment.Center);
            //작업시작
            grdInputResult.View.AddTextBoxColumn("TRACKINTIME", 100).SetIsReadOnly().SetLabel("WORKSTART")
                .SetTextAlignment(TextAlignment.Center);
            //투입LT
            grdInputResult.View.AddTextBoxColumn("INPUTLT", 80).SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            //수주NO
            grdInputResult.View.AddTextBoxColumn("SALESORDERID", 80).SetIsReadOnly();
            grdInputResult.View.PopulateColumns();
            #endregion

            #region 투입실적 하단 LOT LIST
            //lot list
            grdLotListResult.GridButtonItem = GridButtonItem.Export;
           // grdLotListResult.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            //LOT NO
            grdLotListResult.View.AddTextBoxColumn("LOTID", 200).SetIsReadOnly();
            //공정순서
            grdLotListResult.View.AddTextBoxColumn("USERSEQUENCE", 100).SetIsReadOnly();
            //공정명
            grdLotListResult.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250).SetIsReadOnly();
            //작업장
            grdLotListResult.View.AddTextBoxColumn("AREANAME", 150).SetIsReadOnly();
            //투입PCS
            grdLotListResult.View.AddSpinEditColumn("INPUTPCS", 80).SetIsReadOnly();
            //투입PNL
            grdLotListResult.View.AddSpinEditColumn("INPUTPNL", 80).SetIsReadOnly();
            //투입자재명
            grdLotListResult.View.AddTextBoxColumn("INPUTMATERIALNAME", 200).SetIsReadOnly();
            //자재소요량
            grdLotListResult.View.AddTextBoxColumn("MATERIALREQUIREQTY", 100).SetIsReadOnly();

            grdLotListResult.View.PopulateColumns();

            #endregion
        }

        #endregion

        #region Event

        /// <summary>
        ///     화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdInputList.View.FocusedRowChanged += grdInputList_FocusedRowChanged;
            grdInputResult.View.FocusedRowChanged += grdInputResult_FocusedRowChanged;
            grdInputList.View.RowCellStyle += grdInputList_RowCellStyle;
            grdInputResult.InitContextMenuEvent += grdInputResult_InitContextMenuEvent;
            grdLotListResult.InitContextMenuEvent += grdLotListResult_InitContextMenuEvent;
            grdInputList.View.CellValueChanged += grdInputList_CellValueChanged;

           // btnConfirm.Click += BtnConfirm_Click;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (grdInputList.View.FocusedRowHandle < 0) return;


            OnToolbarSaveClick();
        }

        private void grdInputList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (grdInputList.View.GetRowCellValue(e.RowHandle, grdInputList.View.Columns["STATE"]).ToString()
                .Contains("NEW"))
            {
                FlashedCellsHelper.FlashedCellAppearance.BackColor = Color.Blue;

                flashedCellsHelper.SetFlashSpeed(e.RowHandle, grdInputList.View.Columns["STATE"], 1000);
            }
        }

        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page.TabIndex == 1)
            {
                var param = new Dictionary<string, object>();
                param = Conditions.GetValues();

                //var values = Conditions.GetValues();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                param.Add("TYPE", "READY");

                var dtInputResult = SqlExecuter.Query("SelectInputLotListResult", "10001", param);

                if (dtInputResult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                grdInputResult.DataSource = dtInputResult;
            }
        }

        private async void grdInputResult_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;


            DataRow dr = grdInputResult.View.GetFocusedDataRow();

            string inputDate = Format.GetTrimString(dr["INPUTDATE"]);
            string productdefid = Format.GetFullTrimString(dr["PRODUCTDEFID"]);
            string productdefVersion = Format.GetFullTrimString(dr["PRODUCTDEFVERSION"]);
            string productionOrderId = Format.GetFullTrimString(dr["SALESORDERID"]);

            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("P_INPUTDAY", inputDate);
            dic.Add("P_PRODUCTDEFID", productdefid);
            dic.Add("P_PRODUCTDEFVERSION", productdefVersion);
            dic.Add("P_PRODUCTIONORDERID", productionOrderId);
            dic.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = SqlExecuter.Query("SelectInputResultListDetail", "10001", dic);

            grdLotListResult.DataSource = dt;
        }

        private void grdLotListResult_InitContextMenuEvent(SmartBandedGrid sender, InitContextMenuEventArgs args)
        {
            args.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0340"), OpenForm)
                {BeginGroup = true, Tag = "PG-SG-0340"});
            args.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0070"), OpenForm) {Tag = "PG-SG-0070"});
        }

        private void grdInputResult_InitContextMenuEvent(SmartBandedGrid sender, InitContextMenuEventArgs args)
        {
            args.AddMenus.Add(new DXMenuItem(Language.Get("MENU_PG-SG-0720"), OpenForm)
                {BeginGroup = true, Tag = "PG-SG-0720" });
        }

        private void grdInputList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.Name.Contains("STATE")) e.Appearance.BackColor = SetStateColor(e.CellValue.ToString());
        }

        private async void grdInputList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;


            DataRow dr = grdInputList.View.GetFocusedDataRow();
            
            string inputDate = Format.GetTrimString(dr["INPUTDATE"]);
            string productdefid = Format.GetFullTrimString(dr["PRODUCTDEFID"]);
            string productdefVersion = Format.GetFullTrimString(dr["PRODUCTDEFVERSION"]);
            string productionOrderId = Format.GetFullTrimString(dr["SALESORDERID"]);
            
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("P_INPUTDAY", inputDate);
            dic.Add("P_PRODUCTDEFID", productdefid);
            dic.Add("P_PRODUCTDEFVERSION", productdefVersion);
            dic.Add("P_PRODUCTIONORDERID", productionOrderId);
            dic.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dt = SqlExecuter.Query("SelectInputLotListDetail", "10001", dic);

            grdLotList.DataSource = dt;

            /*
            isCheckReadyProduct = true;
            if (e.FocusedRowHandle < 0)
                return;
            if (grdInputList.View.GetRowCellValue(e.FocusedRowHandle, grdInputList.View.Columns["STATE"]).Equals("NEW"))
                flashedCellsHelper.DisposeFlash(e.FocusedRowHandle, grdInputList.View.Columns["STATE"]);
            if (e.PrevFocusedRowHandle > 0
                && grdInputList.View.DataRowCount > e.PrevFocusedRowHandle
                && grdInputList.View.GetRowCellValue(e.PrevFocusedRowHandle, grdInputList.View.Columns["STATE"])
                    .ToString()
                    .Contains("NEW"))
            {
                FlashedCellsHelper.FlashedCellAppearance.BackColor = Color.Black;

                flashedCellsHelper.SetFlashSpeed(e.PrevFocusedRowHandle, grdInputList.View.Columns["STATE"], 1000);
            }

            if (grdInputList.View.GetFocusedDataRow() == null)
                return;

            GetLotList();

            //         conditionValues = new Dictionary<string, object>();

            //         conditionValues.Add("LOTTYPE", grdInputList.View.GetFocusedDataRow()["LOTTYPE"].ToString());
            //         conditionValues.Add("INPUTSEGMENTID", grdInputList.View.GetFocusedDataRow()["INPUTSEGMENTID"].ToString());
            //         conditionValues.Add("INPUTMATERIALNAMEID", grdInputList.View.GetFocusedDataRow()["INPUTMATERIALNAMEID"].ToString());
            //         conditionValues.Add("PRODUCTDEFID", grdInputList.View.GetFocusedDataRow()["PRODUCTDEFID"].ToString());
            //         conditionValues.Add("SALESORDERID", grdInputList.View.GetFocusedDataRow()["SALESORDERID"].ToString());
            //         conditionValues.Add("INPUTDATE", grdInputList.View.GetFocusedDataRow()["INPUTDATE"].ToString());
            //         conditionValues.Add("ISREQUIRED", grdInputList.View.GetFocusedDataRow()["DOCUMENTOUTPUT"].ToString());
            //         conditionValues.Add("INPUTCHARGEID", grdInputList.View.GetFocusedDataRow()["INPUTCHARGEID"].ToString());

            //(grdInputList.DataSource as DataTable).AcceptChanges();

            //         await OnSearchAsync();
            */
        }

        #endregion

        #region 검색

        /// <summary>
        ///     검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected override async Task OnSearchAsync()
        {

            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:

                    grdInputList.DataSource = null;
                    DataTable dtLotList = await QueryAsync("SelectInputLotListReady", "10002", values);

                    if (dtLotList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                    grdInputList.DataSource = dtLotList;
                       
                    break;
                case 1:

                    grdInputResult.DataSource = null;
                    DataTable dtLotresult = await QueryAsync("SelectInputLotListResult", "10002", values);

                    if (dtLotresult.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                        ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                    grdInputResult.DataSource = dtLotresult;
                
                    break;
            }


        }

        /// <summary>
        ///     조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // 품목
            InitializeCondition_ProductPopup();

            //작업장
            CommonFunction.AddConditionAreaPopup("P_AREAID", 3.1, true, Conditions);

            InitializeConditionProductOrderId_Popup();
        }

        /// <summary>
        ///     팝업형 조회조건 생성 - PO
        /// </summary>
        private void InitializeConditionProductOrderId_Popup()
        {
            var conditionProductionOrderId = Conditions.AddSelectPopup("P_PRODUCTIONORDERID"
                    , new SqlQuery("GetProductionOrderIdListOfLotInput", "10002"
                        , $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}")
                    , "PRODUCTIONORDERID", "PRODUCTIONORDERID")
                .SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(1000, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTIONORDERID")
                .SetPosition(10)
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                });


            // 팝업에서 사용되는 검색조건 (P/O번호)
            conditionProductionOrderId.Conditions.AddTextBox("TXTPRODUCTIONORDERID");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // P/O번호
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 150)
                .SetValidationKeyColumn();
            // 수주라인
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("LINENO", 50);
            // 수주량
            conditionProductionOrderId.GridColumns.AddSpinEditColumn("PLANQTY", 90);
            // 품목코드
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목코드 | 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEF", 0).SetIsHidden();
        }

        /// <summary>
        ///     팝업형 조회조건 생성 - 품목
        /// </summary>
        private void InitializeCondition_ProductPopup()
        {
            // SelectPopup 항목 추가
            var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID",
                    new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"),
                    "PRODUCTDEF", "PRODUCTDEF")
                .SetPopupLayout("SELECTPRODUCTDEFID")
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME")
                .SetLabel("PRODUCTDEFID")
                .SetPosition(1.1)
                .SetPopupResultCount(0)
                .SetPopupApplySelection((selectRow, gridRow) =>
                {
                    var productDefName = "";

                    selectRow.AsEnumerable().ForEach(r =>
                    {
                        productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
                    });

                    Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue =
                        productDefName[productDefName.Length - 1] == ','
                            ? productDefName.Remove(productDefName.Length - 1)
                            : productDefName;
                });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION",
                    new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductDivision2",
                        $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Product");

            // 품목코드
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목명
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 품목버전
            conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
            // 품목유형구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType",
                    $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 생산구분
            conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90,
                new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType",
                    $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            // 단위
            conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90,
                new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
        }

        /// <summary>
        ///     조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFTYPE").EditValue = "Product";
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        ///     State 컬러를 가져온다
        /// </summary>
        private Color SetStateColor(string state)
        {
            try
            {
                switch (state)
                {
                    case "NEW":
                        return Color.Blue;
                    default:
                        return Color.Red;
                }
            }
            catch (Exception ex)
            {
                return Color.Transparent;
            }
        }

        private void SetState()
        {
            var inputList = grdInputList.DataSource as DataTable;

            if (inputList.Rows.Count == 0)
                return;

            for (var i = inputList.Rows.Count; i > 0; i--)
                if ((string) inputList.Rows[i - 1]["STATE"] != "NEW")
                {
                    var state = string.Format(inputList.Rows[i - 1]["STATE"] + " DAY");
                    //inputList.Rows[i-1]["STATE"] = state;
                    grdInputList.View.SetRowCellValue(i - 1, "STATE", state);
                }

            //grdInputList.DataSource = inputList;
        }

        // Customizing Context Menu Item Click 이벤트
        private void OpenForm(object sender, EventArgs args)
        {
            try
            {
                DialogManager.ShowWaitDialog();

                var currentRow = grdInputResult.View.GetFocusedDataRow();

                var menuId = (sender as DXMenuItem).Tag.ToString();

                var param = currentRow.Table.Columns
                    .Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => currentRow[col.ColumnName]);

                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }

        /// <summary>
        ///     Lot List 클리어
        /// </summary>
        private void ClearGridLotList()
        {
			grdInputList.View.ClearDatas();
            grdLotList.View.ClearDatas();
        }

        private void GetLotList()
        {
            try
            {
                var param = new Dictionary<string, object>();
                param = Conditions.GetValues();

                //var values = Conditions.GetValues();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                param.Add("TYPE", "LOT");
                param.Add("LOTTYPE", grdInputList.View.GetFocusedDataRow()["LOTTYPE"].ToString());
                param.Add("INPUTSEGMENT", grdInputList.View.GetFocusedDataRow()["INPUTSEGMENT"].ToString());
                param.Add("INPUTMATERIALNAME", grdInputList.View.GetFocusedDataRow()["INPUTMATERIALNAME"].ToString());
                param.Add("PRODUCTDEFID", grdInputList.View.GetFocusedDataRow()["PRODUCTDEFID"].ToString());
                param.Add("SALESORDERID", grdInputList.View.GetFocusedDataRow()["SALESORDERID"].ToString());
                param.Add("INPUTDATE", grdInputList.View.GetFocusedDataRow()["INPUTDATE"].ToString());
                param.Add("ISREQUIRED", grdInputList.View.GetFocusedDataRow()["DOCUMENTOUTPUT"].ToString());
                param.Add("INPUTCHARGEID", grdInputList.View.GetFocusedDataRow()["INPUTCHARGEID"].ToString());

                var dtLotList = SqlExecuter.Query("SelectInputLotListReady", "10002", param);

                if (dtLotList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
                    ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                grdLotList.DataSource = dtLotList;

                isCheckReadyProduct = false;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }

    #region FlashCellHelper

    public class FlashedCellsHelper
    {
        public static AppearanceObject FlashedCellAppearance = new AppearanceObject();
        private readonly GridView _View;

        private readonly List<FlashedCell> flashedCells = new List<FlashedCell>();

        public FlashedCellsHelper(GridView view)
        {
            _View = view;
        }

        private FlashedCell FindFlashedCell(int rowHanlde, GridColumn col)
        {
            foreach (var cell in flashedCells)
                if (cell.RowHandle == rowHanlde && cell.Column == col)
                    return cell;

            var result = new FlashedCell(rowHanlde, col, _View);
            flashedCells.Add(result);
            return result;
        }

        public void SetFlashSpeed(int rowHanlde, GridColumn col, int speed)
        {
            FindFlashedCell(rowHanlde, col).Speed = speed;
        }

        public void DisposeFlash(int rowHandle, GridColumn col)
        {
            var cell = FindFlashedCell(rowHandle, col);
            cell.Dispose();
        }
    }

    public class FlashedCell : GridCell
    {
        private readonly GridView _View;

        private int _Speed;

        private bool isColored;

        private readonly Timer timer = new Timer();

        public FlashedCell(int rowHandle, GridColumn column, GridView view)
            : base(rowHandle, column)
        {
            _View = view;
            view.RowCellStyle += view_RowCellStyle;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        public int Speed
        {
            get => _Speed;
            set
            {
                if (value < 0 || _Speed == value)
                    return;
                _Speed = value;
                timer.Stop();
                if (_Speed == 0) return;
                timer.Interval = value;
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            isColored = !isColored;
            _View.RefreshRowCell(RowHandle, Column);
        }

        private void view_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (isColored)
                if (e.RowHandle == RowHandle && e.Column == Column)
                    e.Appearance.Assign(FlashedCellsHelper.FlashedCellAppearance);
        }

        public void Dispose()
        {
            timer.Stop();
        }
    }

    #endregion
}