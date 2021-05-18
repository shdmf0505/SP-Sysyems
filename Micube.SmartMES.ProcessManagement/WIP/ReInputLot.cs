#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 투입관리 > LOT 재투입
    /// 업  무  설  명  : 수주에 대해 이미 투입된 Lot에 대해 추가 투입 Lot을 생성 한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 
    /// </summary>
    public partial class ReInputLot : SmartConditionManualBaseForm
    {
        #region Local Variables
        private bool etcOnly = false;   // 기타 품목만 조회 여부

        private const int Base = 35;
        private const string Chars = "0123456789ABCDEFGHIJKLMNOPQSTUVWXYZ";
        #endregion

        #region 생성자

        public ReInputLot()
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

            InitializeControls();
            InitializeProductionOrderGrid();
            InitializeProductListGrid();
            InitializeLotListGrid();
            InitializeReinputReasonListGrid();
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void InitializeControls()
        {
            numReInputSequence.Editor.EditValue = 2;    // 기본값 : 2차
        }

        /// <summary>        
        /// 수주 리스트 그리드를 초기화 한다.
        /// </summary>
        private void InitializeProductionOrderGrid()
        {
            grdProductionOrderList.GridButtonItem = GridButtonItem.None;
            grdProductionOrderList.ShowStatusBar = false;

            grdProductionOrderList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProductionOrderList.View.SetIsReadOnly();
            grdProductionOrderList.View.SetSortOrder("PRODUCTIONORDERID");
            grdProductionOrderList.View.SetSortOrder("SEQUENCE");
            grdProductionOrderList.View.EnableRowStateStyle = false;

            // S/O번호
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTIONORDERID", 120);
            // 라인
            grdProductionOrderList.View.AddTextBoxColumn("LINENO", 70).SetTextAlignment(TextAlignment.Center);
            // 순서
            grdProductionOrderList.View.AddSpinEditColumn("SEQUENCE", 70).SetIsHidden();
            // 품목코드
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTDEFID", 170);
            // 품목버전
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100).SetTextAlignment(TextAlignment.Center);
            // 품목명
            grdProductionOrderList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250);
            // 수주량
            grdProductionOrderList.View.AddSpinEditColumn("PLANQTY", 110);
            // 생산구분
            grdProductionOrderList.View.AddComboBoxColumn("PRODUCTIONTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            // 합수
            grdProductionOrderList.View.AddSpinEditColumn("PCSPNL", 110)
                .SetLabel("ARRAY");
            // 납기일자
            grdProductionOrderList.View.AddTextBoxColumn("PLANENDTIME", 120)
                .SetLabel("DELIVERYDATE")
                .SetTextAlignment(TextAlignment.Center);
            // 수주일자
            grdProductionOrderList.View.AddTextBoxColumn("SALEORDERDATE", 120)
                .SetLabel("ORDERDATE")
                .SetTextAlignment(TextAlignment.Center);
            // 투입일자
            grdProductionOrderList.View.AddTextBoxColumn("STARTTIME", 120)
                .SetLabel("STARTEDDATE")
                .SetTextAlignment(TextAlignment.Center);
            // 재투입차수
            grdProductionOrderList.View.AddTextBoxColumn("INPUTSEQUENCE").SetIsHidden();

            grdProductionOrderList.View.PopulateColumns();
            grdProductionOrderList.View.OptionsView.ShowIndicator = false;
        }

        /// <summary>
        /// 품목 List 그리드를 초기화 한다.
        /// </summary>
        private void InitializeProductListGrid()
        {
            grdProductList.GridButtonItem = GridButtonItem.None;
            grdProductList.ShowStatusBar = false;

            grdProductList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdProductList.View.EnableRowStateStyle = false;

            // 품목코드
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFID", 170)
                .SetIsReadOnly();
            // 품목버전
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 품목명
            grdProductList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250)
                .SetIsReadOnly();
            // Roll/Sheet
            grdProductList.View.AddComboBoxColumn("RTRSHT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();
            // 실투입량(PCS)
            grdProductList.View.AddTextBoxColumn("QTY", 120)
                .SetLabel("REALINPUTPCS")
                .SetIsReadOnly()
                .SetDisplayFormat("#,##0");
            // 실투입량(PNL)
            grdProductList.View.AddTextBoxColumn("PNLQTY", 120)
                .SetLabel("REALINPUTPNL")
                .SetIsReadOnly()
                .SetDisplayFormat("#,##0")
                .SetIsHidden();
            // Lot Size
            grdProductList.View.AddSpinEditColumn("LOTSIZE", 80);
            // 판넬
            grdProductList.View.AddTextBoxColumn("PANEL", 120)
                .SetLabel("PANEL")
                .SetDisplayFormat("#,##0");
            // 단위
            grdProductList.View.AddComboBoxColumn("UNIT", 70, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 접합수
            grdProductList.View.AddTextBoxColumn("JOINTQTY", 120)
                .SetIsReadOnly()
                .SetDisplayFormat("#,##0");
            // 자원
            //grdProductList.View.AddTextBoxColumn("RESOURCEID").SetIsHidden();
            // 투입 작업장
            //grdProductList.View.AddTextBoxColumn("AREAID").SetIsHidden();

            grdProductList.View.PopulateColumns();
            grdProductList.View.OptionsView.ShowIndicator = false;
        }

        private void InitializeReinputReasonListGrid()
        {
            grdReinputReason.GridButtonItem = GridButtonItem.None;
            grdReinputReason.ShowStatusBar = true;

            grdReinputReason.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            // grdLotList.View.SetIsReadOnly();
            grdReinputReason.View.EnableRowStateStyle = false;

            grdReinputReason.View.AddTextBoxColumn("REINPUTSEQUENCE", 80).SetTextAlignment(TextAlignment.Center);
            grdReinputReason.View.AddTextBoxColumn("REINPUTREASON", 300);

            grdReinputReason.View.PopulateColumns();
        }
        /// <summary>
        /// LOT List 그리드를 초기화 한다.
        /// </summary>
        private void InitializeLotListGrid()
        {
            grdLotList.GridButtonItem = GridButtonItem.None;
            grdLotList.ShowStatusBar = true;

            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            // grdLotList.View.SetIsReadOnly();
            grdLotList.View.EnableRowStateStyle = false;

            // 품목코드
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 170).SetIsReadOnly();
            // 품목버전
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION").SetIsHidden();
            // LOT ID
            grdLotList.View.AddTextBoxColumn("LOTID", 230)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // 단위
            grdLotList.View.AddComboBoxColumn("UNIT", 120, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            // 수량
            grdLotList.View.AddSpinEditColumn("QTY", 140)
                .SetIsReadOnly();
            // 수량(PNL)
            grdLotList.View.AddSpinEditColumn("PNLQTY", 140)
                .SetIsReadOnly();
            // 자원
            /*
            grdLotList.View.AddComboBoxColumn("RESOURCEID", 280, new SqlQueryAdapter(), "RESOURCENAME", "RESOURCEID")
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetRelationIds("PRODUCTDEFID", "PRODUCTDEFVERSION")
                .SetValidationIsRequired();
            */
            // 투입 작업장
            //grdLotList.View.AddTextBoxColumn("INPUTAREA").SetIsHidden();
            // 수주번호
            grdLotList.View.AddTextBoxColumn("PRODUCTIONORDERID").SetIsHidden();
            // 회사코드
            grdLotList.View.AddTextBoxColumn("ENTERPRISEID").SetIsHidden();
            // 공장코드
            grdLotList.View.AddTextBoxColumn("PLANTID").SetIsHidden();
            // 접합수
            grdLotList.View.AddTextBoxColumn("JOINTQTY").SetIsHidden();
            // 순투입
            grdLotList.View.AddTextBoxColumn("PUREINPUT").SetIsHidden();
            // 순수주
            grdLotList.View.AddTextBoxColumn("PUREORDER").SetIsHidden();

            grdLotList.View.PopulateColumns();
            grdLotList.View.OptionsView.ShowIndicator = false;

        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btnReInputSplit.Click += BtnReInputSplit_Click;

            grdProductionOrderList.View.FocusedRowChanged += grdProductionOrderListView_FocusedRowChanged;

            grdProductList.View.CellValueChanged += grdProductListView_CellValueChanged;
            grdProductList.View.CheckStateChanged += grdProductListView_CheckStateChanged;
            grdProductList.View.RowCellStyle += grdProductListView_RowCellStyle;

            grdLotList.View.CellValueChanged += View_CellValueChanged;

            numSurplusApply.Editor.EditValueChanged += Editor_EditValueChanged;
            numLotSizePanel.Editor.EditValueChanged += Editor_EditValueChanged;
            numRealInputPanel.Editor.EditValueChanged += Editor_EditValueChanged;
            numReInputSequence.Editor.EditValueChanged += numReInputSequenceEditor_EditValueChanged;

            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValueChanged += Editor_EditValueChanged1;
            Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValueChanged += ReInputLot_EditValueChanged1;
        }

        private void Editor_EditValueChanged1(object sender, EventArgs e)
        {
            string productDefId = (string)Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").EditValue;
            if (string.IsNullOrEmpty(productDefId))
            {
                DataTable dt = (Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource as DataTable).Clone();
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValueChanged -= ReInputLot_EditValueChanged1;
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = "*";
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValueChanged += ReInputLot_EditValueChanged1;

                Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTIONORDERID").SearchQuery = new SqlQuery("GetProductionOrderIdList", "10003"
                    , $"PLANTID={UserInfo.Current.Plant}", "ISSPLIT=N");
            }
        }

        private void ReInputLot_EditValueChanged1(object sender, EventArgs e)
        {
            string productDefId = Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID").GetValue().ToString();
            string productDefVersion = Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue.ToString();
            Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTIONORDERID").SearchQuery = new SqlQuery("GetProductionOrderIdList", "10003"
                , $"PLANTID={UserInfo.Current.Plant}", "ISSPLIT=N", $"PRODUCTDEFID={productDefId}", $"PRODUCTDEFVERSION={productDefVersion}");
        }

        /// <summary>
        ///  수주선택시 자동 조회 2019.08.01 배선용
        ///  우영민 과장 요청
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ReInputLot_EditValueChanged(object sender, EventArgs e)
        {
            SmartSelectPopupEdit poPopup = sender as SmartSelectPopupEdit;

            if (Format.GetFullTrimString(poPopup.EditValue).Equals(string.Empty))
            {
                poPopup.ClearValue();
                Conditions.GetControl<SmartComboBox>("P_LINENO").EditValue = "*";
            }

            //await this.OnSearchAsync();
        }

        /// <summary>
        /// 재투입 분할버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReInputSplit_Click(object sender, EventArgs e)
        {
            RefreshGrdLotList();
        }

        /// <summary>
        /// 수주 리스트 그리드의 선택된 행이 변경될 경우 호출 한다.
        /// 순수주, 순투입, 기준투입(PCS), 기준로스 값을 다시 계산 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdProductionOrderListView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged(e.FocusedRowHandle);
            CalcProductList();
            getReinputReasonList(e.FocusedRowHandle);
        }

        /// <summary>
        /// 품목 리스트 그리드의 판넬 값을 변경하면 실투입량을 변경한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdProductListView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PANEL")
            {
                DataRow row = grdProductList.View.GetDataRow(e.RowHandle);
                row["QTY"] = Format.GetDouble(row["PANEL"], 0) * Format.GetDouble(row["JOINTQTY"], 0);
            }
        }

        private void grdProductListView_CheckStateChanged(object sender, EventArgs e)
        {
            RefreshGrdLotList();
        }

        private void grdProductListView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "PANEL")
            {
                e.Appearance.BackColor = System.Drawing.Color.Yellow;
                e.Appearance.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "RESOURCEID")
            {
                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;
                string areaId = Format.GetString(edit.GetDataSourceValue("AREAID", edit.GetDataSourceRowIndex("RESOURCEID", e.Value)));
                grdLotList.View.SetFocusedRowCellValue("INPUTAREA", areaId);
            }
        }

        /// <summary>
        /// 잉여 재고, 잉여 재공, 부족, 기준 투입, LOT PNL 컨트롤이 값이 변경될 경우 호출 한다.
        /// 순수주, 순투입, 기준투입(PCS), 기준로스 값을 다시 계산 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            CalcProductionOrderInfo(grdProductionOrderList.View.FocusedRowHandle);

            SmartSpinEdit editor = sender as SmartSpinEdit;

            if (editor == numRealInputPanel.Editor)
            {
                CalcProductList();
            }
        }

        /// <summary>
        /// 재투입 차수 값 변경시 처리
        /// 재투입차수 값의 범위는 2 ~ 9
        /// 6차 이상일 경우 부자재만 표시한다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numReInputSequenceEditor_EditValueChanged(object sender, EventArgs e)
        {
            if (UserInfo.Current.Enterprise != Constants.EnterPrise_YoungPoong)
            {
                int MinValue = 2;
                int MaxValue = 9;

                int reInputSeq = Format.GetInteger(numReInputSequence.EditValue, 2);
                if (reInputSeq > MaxValue)
                {
                    reInputSeq = MaxValue;
                }
                if (reInputSeq < MinValue)
                {
                    reInputSeq = MinValue;
                }
                numReInputSequence.Editor.EditValueChanged -= numReInputSequenceEditor_EditValueChanged;
                numReInputSequence.EditValue = reInputSeq;

                if (etcOnly && Format.GetInteger(numReInputSequence.EditValue, 2) < 6)
                {
                    etcOnly = false;
                    FocusedRowChanged(grdProductionOrderList.View.FocusedRowHandle);
                }
                else if (!etcOnly && Format.GetInteger(numReInputSequence.EditValue, 2) >= 6)
                {
                    etcOnly = true;
                    FocusedRowChanged(grdProductionOrderList.View.FocusedRowHandle);
                }

                numReInputSequence.Editor.EditValueChanged += numReInputSequenceEditor_EditValueChanged;
            }
            else
            {
                FocusedRowChanged(grdProductionOrderList.View.FocusedRowHandle);
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable lotList = grdLotList.DataSource as DataTable;

            if (lotList == null || lotList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            DataRow row = grdProductionOrderList.View.GetDataRow(grdProductionOrderList.View.FocusedRowHandle);

            MessageWorker worker = new MessageWorker("SaveReInputLot");
            worker.SetBody(new MessageBody()
            {
                { "PlantId", UserInfo.Current.Plant },
                { "ProductionOrderId", row["PRODUCTIONORDERID"].ToString() },
                { "LineNo", row["LINENO"].ToString() },
                { "LotSizePanel", numLotSizePanel.EditValue },
                { "InputPanel", numRealInputPanel.EditValue },
                { "UserId", UserInfo.Current.Id },
                { "InputSequence", numReInputSequence.EditValue },
                { "InputReason", txtReason.Text.Trim() },
                { "list", lotList }
            });

            worker.Execute();
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

            DataTable dtProductionOrder = await QueryAsync("SelectProductionOrderListForReInput", "10001", values);

            int beforeFocusedRowHandle = grdProductionOrderList.View.FocusedRowHandle;

            grdProductionOrderList.DataSource = dtProductionOrder;

            // 검색 결과에 데이터가 없는 경우
            if (dtProductionOrder.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
                ClearProductionOrderInfo();
                grdProductList.View.ClearDatas();
                grdLotList.View.ClearDatas();
                txtReason.Text = string.Empty;
                grdReinputReason.View.ClearDatas();
            }
            else
            {
                // 검색 전 FocusedRowHandle과 검색 후 FocusedRowHandle이 0 인 경우 내부적으로 FocusedRowChanged 이벤트가 호출되지 않음
                // FocusedRowChanged 이벤트 로직 강제 호출
                if (beforeFocusedRowHandle == 0 && grdProductionOrderList.View.FocusedRowHandle == 0)
                {
                    FocusedRowChanged(grdProductionOrderList.View.FocusedRowHandle);
                    getReinputReasonList(grdProductionOrderList.View.FocusedRowHandle);
                }
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // S/O번호 검색조건 팝업 추가
            var conditionProductionOrderId = Conditions.AddSelectPopup("P_PRODUCTIONORDERID", new SqlQuery("GetProductionOrderIdList", "10003", $"PLANTID={UserInfo.Current.Plant}", "ISSPLIT=N"), "PRODUCTIONORDERID", "PRODUCTIONORDERID")
                //.SetMultiGrid(true)
                .SetPopupLayout("SELECTPRODUCTIONORDER", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetLabel("PRODUCTIONORDERID")
                .SetPosition(1.5)
                .SetPopupAutoFillColumns("PRODUCTDEFNAME");

            // 팝업에서 사용되는 검색조건 (P/O번호)
            conditionProductionOrderId.Conditions.AddTextBox("TXTPRODUCTIONORDERID");

            // 팝업 그리드에서 보여줄 컬럼 정의
            // Value
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("VALUEFIELD", 100)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // S/O번호
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTIONORDERID", 80);
            // 라인
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("LINENO", 50);
            // 수주량
            conditionProductionOrderId.GridColumns.AddSpinEditColumn("PLANQTY", 70);
            // 품목코드
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 120);
            // 품목버전
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            // 품목명
            conditionProductionOrderId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);

            // 팝업 값 선택 이벤트
            conditionProductionOrderId.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                List<DataRow> selectedList = selectedRows.ToList();
                List<string> lineNoList = new List<string>();

                string poList = "";

                selectedList.ForEach(row =>
                {
                    string productionOrderId = Format.GetString(row["PRODUCTIONORDERID"]);
                    string lineNo = Format.GetString(row["LINENO"]);

                    poList += productionOrderId + ",";
                    lineNoList.Add(lineNo);
                });


                poList = poList.TrimEnd(',');

                lineNoList = lineNoList.Distinct().ToList();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTIONORDERID", poList);

                DataTable lineNoInfo = SqlExecuter.Query("GetLineNoByProductionOrder", "10001", param);


                Conditions.GetControl<SmartComboBox>("P_LINENO").DataSource = lineNoInfo;

                if (lineNoList.Count == 1)
                    Conditions.GetControl<SmartComboBox>("P_LINENO").EditValue = Format.GetFullTrimString(lineNoList[0]);
                else
                    Conditions.GetControl<SmartComboBox>("P_LINENO").EditValue = "*";
            });


            // 품목 검색조건 팝업 추가
            AddConditionProductPopup("P_PRODUCTDEFID", 3.5, false, Conditions);
        }

        private ConditionCollection AddConditionProductPopup(string id, double position, bool isMultiSelect, ConditionCollection conditions, string displayFieldName = "PRODUCTDEFNAME", string valueFieldName = "PRODUCTDEFID")
        {
            // SelectPopup 항목 추가
            var conditionProductId = conditions.AddSelectPopup(id, new SqlQuery("GetProductDefinitionList", "10001", $"PLANTID={UserInfo.Current.Plant}"), displayFieldName, valueFieldName)
                .SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("UNIT")
                .SetLabel("PRODUCTDEFID")
                .SetValidationIsRequired()
                .SetPosition(position);

            // 복수 선택 여부에 따른 Result Count 지정
            if (isMultiSelect)
                conditionProductId.SetPopupResultCount(0);
            else
                conditionProductId.SetPopupResultCount(1);


            // 품목 VERSION 설정
            conditionProductId.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                List<DataRow> list = selectedRows.ToList<DataRow>();

                List<string> listRev = new List<string>();

                string productlist = string.Empty;
                selectedRows.ForEach(row =>
                {
                    string productid = Format.GetString(row["PRODUCTDEFID"]);
                    string revision = Format.GetString(row["PRODUCTDEFVERSION"]);
                    productlist = productlist + productid + ',';
                    listRev.Add(revision);
                }
                );

                productlist = productlist.TrimEnd(',');

                listRev = listRev.Distinct().ToList();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("P_PLANTID", UserInfo.Current.Plant);
                param.Add("P_PRODUCTDEFID", productlist);

                DataTable dt = SqlExecuter.Query("selectProductdefVesion", "10001", param);

                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValueChanged -= ReInputLot_EditValueChanged1;
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").DataSource = dt;
                string productDefVersion = "*";
                if (listRev.Count > 0)
                {
                    if (listRev.Count == 1)
                    {
                        productDefVersion = Format.GetFullTrimString(listRev[0]);
                    }
                    else
                    {
                        productDefVersion = Format.GetFullTrimString('*');
                    }
                    Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValue = productDefVersion;
                }
                Conditions.GetControl<SmartComboBox>("P_PRODUCTDEFVERSION").EditValueChanged += ReInputLot_EditValueChanged1;

                Conditions.GetCondition<ConditionItemSelectPopup>("P_PRODUCTIONORDERID").SearchQuery = new SqlQuery("GetProductionOrderIdList", "10003"
                    , $"PLANTID={UserInfo.Current.Plant}", "ISSPLIT=N", $"PRODUCTDEFID={productlist}", $"PRODUCTDEFVERSION={productDefVersion}");
            });

            // 팝업에서 사용되는 검색조건 (품목코드/명)
            conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
            //제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
            conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                                        .SetDefault("Product");

            // 팝업 그리드에서 보여줄 컬럼 정의
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

            return conditions;
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
            Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTIONORDERID").EditValueChanged += ReInputLot_EditValueChanged;
        }

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

        /// <summary>
        /// 수주 리스트 그리드의 선택된 행이 변경된 경우 다시 계산하는 로직을 호출한다.
        /// </summary>
        /// <param name="focusedRowHandle">그리드에 선택된 행 Index</param>
        private void FocusedRowChanged(int focusedRowHandle)
        {
            if (focusedRowHandle < 0)
            {
                ClearProductionOrderInfo();
                grdProductList.View.ClearDatas();
                grdLotList.View.ClearDatas();
                txtReason.Text = string.Empty;
                grdReinputReason.View.ClearDatas();
                return;
            }

            DataRow row = grdProductionOrderList.View.GetDataRow(focusedRowHandle);

            decimal planQty = Format.GetDecimal(row["PLANQTY"]);
            decimal firstInputQty = Format.GetDecimal(row["FIRSTINPUTQTY"]);
            string state = row["STATE"].ToString();

            numPureInput.EditValue = planQty;
            numSurplusApply.EditValue = firstInputQty;
            numReInputSequence.EditValue = Format.GetInteger(row["INPUTSEQUENCE"]) + 1;
            GetProductDefinitionList(grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PRODUCTDEFID").ToString() ,grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PRODUCTDEFVERSION").ToString());

            string productionOrderId = row["PRODUCTIONORDERID"].ToString();
            string lineNo = row["LINENO"].ToString();

            GetProductionOrderInfo(focusedRowHandle);
        }

        /// <summary>
        /// 수주 정보의 값(순수주, 순투입, 기준투입(PCS), 기준로스)을 다시 계산한다.
        /// </summary>
        /// <param name="focusedRowHandle">수주 리스트 그리드에 선택된 행 Index</param>
        private void CalcProductionOrderInfo(int focusedRowHandle)
        {
            int planQty = Format.GetInteger(grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PLANQTY"));
            int surplusStock = Format.GetInteger(numSurplusApply.EditValue);

            numPureInput.EditValue = planQty - surplusStock;

            int pcsPnl = Format.GetInteger(grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PCSPNL"));

            numRealInput.EditValue = Format.GetInteger(numRealInputPanel.EditValue) * pcsPnl;

            double pureInput = Format.GetDouble(numPureInput.EditValue, 0);
            double realInput = Format.GetDouble(numRealInput.EditValue, 0);

            if (realInput != 0)
            {
                numYield.EditValue = pureInput / realInput * 100;
            }
            else
            {
                numYield.EditValue = 0;
            }
        }
        private void getReinputReasonList(int rowhandle)
        {
            if (rowhandle < 0)
                return;

            txtReason.Text = string.Empty;
            DataRow dr = grdProductionOrderList.View.GetDataRow(rowhandle);

            string ProductionOrder = Format.GetTrimString(dr["PRODUCTIONORDERID"]);
            string Line = Format.GetTrimString(dr["LINENO"]);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("P_PRODUCTIONORDERID", ProductionOrder);
            dic.Add("LINENO", Line);

            DataTable dt = SqlExecuter.Query("SelectReinputReason", "10001", dic);

            grdReinputReason.DataSource = dt;
        }
        /// <summary>
        /// 수주 리스트 그리드에 정보가 없는 경우 상세 정보를 초기화 한다.
        /// </summary>
        private void ClearProductionOrderInfo()
        {
            tlpProductionOrderInfo.Controls.Find<SmartLabelSpinEdit>(true).ForEach(control =>
            {
                if (control.Name != "numPureInput" && control.Name != "numReInputSequence")
                {
                    control.Editor.EditValueChanged -= Editor_EditValueChanged;

                    control.EditValue = 0;

                    control.Editor.EditValueChanged += Editor_EditValueChanged;
                }
            });
        }

        /// <summary>
        /// 품목 리스트 그리드의 데이터를 조회 한다.
        /// </summary>
        /// <param name="productDefId">품목코드</param>
        private void GetProductDefinitionList(string productDefId, string productDefVersion)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("PRODUCTDEFID", productDefId);
            values.Add("PRODUCTDEFVERSION", productDefVersion);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            if(etcOnly)
            {
                values.Add("ETCONLY", "Y");
            }
            //DataTable dtProductDefinition = SqlExecuter.Query("SelectProductDefinitionListByProductionOrder", "10005", values);
            DataTable dtProductDefinition = SqlExecuter.Query("SelectProductDefinitionListByProductionOrder_YP", "10003", values);
            grdProductList.DataSource = dtProductDefinition;
            grdLotList.View.ClearDatas();
            CalcProductList();
        }

        /// <summary>
        /// 품목 리스트 그리드의 수량을 재계산 한다
        /// </summary>
        private void CalcProductList()
        {
            DataTable dataTable = grdProductList.DataSource as DataTable;
            foreach (DataRow each in dataTable.Rows)
            {
                //each["PANEL"] = Format.GetDouble(numRealInputPanel.EditValue, 0) * Format.GetDouble(each["USEBOMCNT"], 0) * (Format.GetString(each["ISINNERPUBLIC"]) == "Y" ? 2 : 1);
                //each["QTY"] = Format.GetDouble(each["PANEL"], 0) * Format.GetDouble(each["JOINTQTY"], 0);

                each["QTY"] = Format.GetDecimal(numRealInput.EditValue) * Format.GetDecimal(each["USEBOMCNT"]) * (Format.GetString(each["ISINNERPUBLIC"]) == "Y" ? 2 : 1);
                each["PANEL"] = Math.Ceiling(Format.GetDecimal(each["QTY"]) / Format.GetDecimal(each["JOINTQTY"]));
                each["LOTSIZE"] = Format.GetInteger(numLotSizePanel.EditValue);
            }
        }

        /// <summary>
        /// 수량 정보를 조회 한다.
        /// </summary>
        /// <param name="focusedRowHandle"></param>
        private void GetProductionOrderInfo(int focusedRowHandle)
        {
            DataRow row = grdProductionOrderList.View.GetDataRow(focusedRowHandle);

            //numSurplusApply.EditValue = 0;
            numRealInputPanel.EditValue = Format.GetInteger(row["STDINPUTPNLQTY"]); // Panel 투입 수량
            numLotSizePanel.EditValue = Format.GetInteger(row["LOTINPUTPNLQTY"]);   // LotSize

            CalcProductionOrderInfo(focusedRowHandle);
        }

        private void RefreshGrdLotList()
        {
            int focusedRowHandle = grdProductionOrderList.View.FocusedRowHandle;
            string productionOrderId = grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "PRODUCTIONORDERID").ToString();
            string lineNo = grdProductionOrderList.View.GetRowCellValue(focusedRowHandle, "LINENO").ToString();

            DataRow dt = grdProductionOrderList.View.GetFocusedDataRow() ;

            DataTable checkedProducts = grdProductList.View.GetCheckedRows();

            DataTable lotList = grdLotList.DataSource as DataTable;
            DataTable newLotList = lotList.Clone();

            string productDef = string.Empty;
            foreach (DataRow drProduct in checkedProducts.Rows)
            {
                productDef += drProduct["PRODUCTDEFID"].ToString() + "|" + drProduct["PRODUCTDEFVERSION"].ToString() + ",";

                double jointQty = Format.GetDouble(drProduct["JOINTQTY"], 0);

                string siteCode = GetSiteCode();

                DataRow dataRow = drProduct;
                string rtrSht = dataRow["RTRSHT"].ToString();
                string lotStartNo = DateTime.Now.ToString("yyMMdd") + siteCode;

                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("LOTID", lotStartNo);

                DataTable dtSequence = SqlExecuter.Query("GetLotIdMaxSequence", "10001", values);

                int sequece = Format.GetInteger(dtSequence.Rows.Cast<DataRow>().FirstOrDefault()["SEQUENCE"]);

                string lotNo = lotStartNo + sequece.ToString("000");
                string reInput = "";
                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    reInput = GetBase35String(Format.GetInteger(numReInputSequence.EditValue));
                else
                    reInput = numReInputSequence.EditValue.ToString();

                string materialClass = Format.GetString(dataRow["MATERIALCLASS"]);
                string materialSequence = Format.GetString(dataRow["MATERIALSEQUENCE"]);

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    if (string.IsNullOrWhiteSpace(materialClass) || string.IsNullOrWhiteSpace(materialSequence))
                    {
                        // 자재품목구분 또는 자재순번이 등록되지 않았습니다. 자재품목구분, 자재순번을 확인하시기 바랍니다. {0}
                        throw MessageException.Create("NotExistsMaterialInfo", string.Format("Product Id : {0}, Product Version : {1}, Material Class : {2}, Material Sequence : {3}", Format.GetString(dataRow["PRODUCTDEFID"]), Format.GetString(dataRow["PRODUCTDEFVERSION"]), materialClass, materialSequence));
                    }
                }
                else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    if (string.IsNullOrWhiteSpace(materialClass))
                    {
                        // 자재품목구분이 등록되지 않았습니다. 자재품목구분을 확인하시기 바랍니다. {0}
                        throw MessageException.Create("NotExistsMaterialInfo", string.Format("Product Id : {0}, Product Version : {1}, Material Class : {2}", Format.GetString(dataRow["PRODUCTDEFID"]), Format.GetString(dataRow["PRODUCTDEFVERSION"]), materialClass));
                    }
                }

                string material = "";

                if (Format.GetString(dataRow["PRODUCTDEFTYPE"]) == "SubAssembly" && Format.GetInteger(materialSequence) == 0)
                {
                    int materialSeq = 1;

                    int cnt = newLotList.AsEnumerable().Where(r => Format.GetString(r["LOTID"]).Substring(13, 2) == materialClass).Count();

                    if (cnt > 0)
                        materialSeq = cnt + 1;


                    material = materialClass + Format.GetInteger(materialSeq).ToString("00");
                }
                else
                {
                    material = materialClass + Format.GetInteger(materialSequence).ToString("00");
                }

                List<string> lotDegree = new List<string>();
                string lotSplit = "000";

                double planQty = Format.GetDouble(grdProductionOrderList.View.GetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "PLANQTY"), 0);
                double pcsPnl = Format.GetDouble(grdProductionOrderList.View.GetRowCellValue(grdProductionOrderList.View.FocusedRowHandle, "PCSPNL"), 0);

                double pureInput = Format.GetDouble(numPureInput.EditValue, 0);
                double realInput = Format.GetDouble(numRealInput.EditValue, 0);

                double panelPerLot = Format.GetDouble(numLotSizePanel.EditValue, 0); // LOT Size

                double lotPnl = Format.GetDouble(dataRow["LOTSIZE"], 0);

                double pnlQty = Format.GetDouble(dataRow["PANEL"], 0);

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    if (rtrSht == "RTR" && (materialClass == "FG" || materialClass == "SB"))
                    {
                        //int nRtrLotDegree = (standardQty / rollStandardInput) + (standardQty % rollStandardInput == 0 ? 0 : 1);

                        //for (int i = 0; i < nRtrLotDegree; i++)
                        //{
                        //    lotDegree.Add((i + 1).ToString("000"));
                        //}

                        lotDegree.Add("000");

                        lotSplit = "000";
                    }
                    else
                    {
                        int nShtLotDegree = (int)(pnlQty / lotPnl) + (pnlQty % lotPnl == 0 ? 0 : 1);

                        int totalSeq = 1;
                        int lastSeq = 1;

                        for (int i = 0; i < nShtLotDegree; i++)
                        {
                            if (i >= 999)
                            {
                                if (totalSeq % 100 == 0)
                                    totalSeq++;

                                int startIndex = Format.GetInteger(totalSeq.ToString().Substring(0, 2));

                                if (lastSeq > 99)
                                    lastSeq = 1;

                                lotDegree.Add(CommonFunction.GetBase36String(startIndex) + lastSeq.ToString("00"));
                            }
                            else
                            {
                                lotDegree.Add(lastSeq.ToString("000"));
                            }

                            totalSeq++;
                            lastSeq++;
                        }

                        lotSplit = "001";
                    }
                }
                else
                {
                    switch (rtrSht)
                    {
                        case "RTR":
                            //int nRtrLotDegree = (standardQty / rollStandardInput) + (standardQty % rollStandardInput == 0 ? 0 : 1);

                            //for (int i = 0; i < nRtrLotDegree; i++)
                            //{
                            //    lotDegree.Add((i + 1).ToString("000"));
                            //}

                            lotDegree.Add("000");

                            lotSplit = "000";

                            break;
                        case "SHT":
                            int nShtLotDegree = (int)(pnlQty / lotPnl) + (pnlQty % lotPnl == 0 ? 0 : 1);

                            int totalSeq = 1;
                            int lastSeq = 1;

                            for (int i = 0; i < nShtLotDegree; i++)
                            {
                                if (i >= 999)
                                {
                                    if (totalSeq % 100 == 0)
                                        totalSeq++;

                                    int startIndex = Format.GetInteger(totalSeq.ToString().Substring(0, 2));

                                    if (lastSeq > 99)
                                        lastSeq = 1;

                                    lotDegree.Add(CommonFunction.GetBase36String(startIndex) + lastSeq.ToString("00"));
                                }
                                else
                                {
                                    lotDegree.Add(lastSeq.ToString("000"));
                                }

                                totalSeq++;
                                lastSeq++;
                            }

                            lotSplit = "001";

                            break;
                    }
                }
                /*
                switch (rtrSht)
                {
                    case "RTR":
                        lotDegree.Add("000");
                        lotSplit = "000";
                        break;
                    case "SHT":
                        int nShtLotDegree = (int)(pnlQty / panelPerLot) + (pnlQty % panelPerLot == 0 ? 0 : 1);

                        for (int i = 0; i < nShtLotDegree; i++)
                        {
                            lotDegree.Add((i + 1).ToString("000"));
                        }

                        lotSplit = "001";
                        break;
                }
                */
                int addCount = 0;

                lotDegree.ForEach(value =>
                {
                    string lotId = string.Join("-", lotNo, reInput, material, value, lotSplit);

                    DataRow newRow = newLotList.NewRow();

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                    {
                        newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        newRow["PLANTID"] = UserInfo.Current.Plant;
                        newRow["PRODUCTDEFID"] = dataRow["PRODUCTDEFID"];
                        newRow["PRODUCTDEFVERSION"] = dataRow["PRODUCTDEFVERSION"];
                        newRow["LOTID"] = lotId;
                        newRow["PNLQTY"] = rtrSht == "RTR" ? (int)pnlQty : (int)(addCount == (int)(pnlQty / panelPerLot) ? (pnlQty % panelPerLot) : panelPerLot);
                        newRow["QTY"] = (int)(Format.GetDouble(newRow["PNLQTY"], 0) * jointQty);
                        newRow["JOINTQTY"] = jointQty;
                        newRow["UNIT"] = dataRow["UNIT"];
                        //      newRow["RESOURCEID"] = dataRow["RESOURCEID"];   // todo : PrimaryResource 없고 작업장이 여러개일 경우 기본설정 하지 않게 수정?
                        //    newRow["INPUTAREA"] = dataRow["AREAID"];
                    }
                    else if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                        newRow["PLANTID"] = UserInfo.Current.Plant;
                        newRow["PRODUCTDEFID"] = dataRow["PRODUCTDEFID"];
                        newRow["PRODUCTDEFVERSION"] = dataRow["PRODUCTDEFVERSION"];
                        newRow["LOTID"] = lotId;
                        if (rtrSht == "RTR" && (materialClass == "FG" || materialClass == "SB"))
                        {
                            newRow["PNLQTY"] = (int)pnlQty;
                            newRow["QTY"] = (int)(pnlQty * jointQty);
                        }
                        else
                        {
                            newRow["PNLQTY"] = (int)(addCount == (int)(pnlQty / lotPnl) ? (pnlQty % lotPnl) : lotPnl);
                            newRow["QTY"] = (int)(Format.GetDouble(newRow["PNLQTY"], 0) * jointQty);
                        }
                        //newRow["PNLQTY"] = rtrSht == "RTR" ? (int)pnlQty : (int)(addCount == (int)(pnlQty / panelPerLot) ? (pnlQty % panelPerLot) : panelPerLot);
                        //newRow["QTY"] = (int)(Format.GetDouble(newRow["PNLQTY"], 0) * jointQty);
                        newRow["JOINTQTY"] = jointQty;
                        newRow["UNIT"] = dataRow["UNIT"];
                    }

                    newLotList.Rows.Add(newRow);

                    addCount++;
                });
            }

            newLotList.AcceptChanges();
            newLotList.DefaultView.Sort = "LOTID";
  
            if (productDef.EndsWith(","))
            {
                productDef = productDef.Substring(0, productDef.Length - 1);
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("ProductDef", productDef);

            //grdLotList.View.RefreshComboBoxDataSource("RESOURCEID", new SqlQuery("GetLotInputAreaList", "10032", param));

            grdLotList.DataSource = newLotList;
        }

        /// <summary>
        /// Plant ID 별 LOT ID 내 구분자 반환
        /// </summary>
        /// <returns></returns>
        private string GetSiteCode()
        {
            switch (UserInfo.Current.Plant)
            {
                case "IFC":
                    return "F";
                case "IFV":
                    return "V";
                case "CCT":
                    return "C";
                case "YPE":
                    return "Y";
                case "YPEV":
                    return "P";
                default:
                    throw new ArgumentException("Unknown PlantId");
            }
        }

        /// <summary>
        /// 숫자를 "R"을 제외한 35진수 문자열로 반환
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetBase35String(int value)
        {
            string result;

            result = Chars[value % Base].ToString();


            return result;
        }

        #endregion
    }
}
