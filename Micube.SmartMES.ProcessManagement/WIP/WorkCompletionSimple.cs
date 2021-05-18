#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using Micube.SmartMES.Commons;

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

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using Micube.Framework.SmartControls.Grid;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 작업 완료
    /// 업  무  설  명  : 완료 대기중인 Lot을 작업 완료 처리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-08-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WorkCompletionSimple : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private string _processSegmentType = "";

        private bool _isInspectionProcess = false;
        private bool _isRepairProcess = false;

        private bool isMessageLoaded = false;
        private string lotId = null;
        private string _queryVersion = "";

        private DataTable _consumableDefectList;
        private DataTable _dtDefect = null;
        private Dictionary<string, decimal> _consumableQty;

        private string[] DontUseSegmentList = new string[] { "7012" ,"7014","7018","7022","7026","7030","7038","8010","8012","8014","8016"};
        #endregion

        #region 생성자

        public WorkCompletionSimple()
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
            UseAutoWaitArea = false;
            InitializeGrid();
            InitializeControls();
            InitializeEvent();
        }

        private void InitializeControls()
        {
            
            txtLotId.ImeMode = ImeMode.Alpha;
        
            lblIsRework.Visible = false;
            lblIsRCLot.Visible = false;

            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSSTATE", "PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION",
                "NEXTPROCESSSEGMENTID", "NEXTPROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTTYPE", "PRODUCTDEFVERSION",
                "PRODUCTTYPE", "PRODUCTDEFTYPEID", "LOTTYPE", "ISHOLD",
                "AREANAME", "DEFECTUNIT", "PCSPNL", "PANELPERQTY",
                "PROCESSSEGMENTTYPE", "STEPTYPE", "ISPRINTLOTCARD", "ISPRINTRCLOTCARD",
                "TRACKINUSER", "TRACKINUSERNAME", "MATERIALCLASS", "AREAID",
                "ISRCLOT", "SELFSHIPINSPRESULT", "SELFTAKEINSPRESULT", "MEASUREINSPRESULT",
                "OSPINSPRESULT", "ISBEFOREROLLCUTTING", "PATHTYPE", "LOTSTATE",
                "WAREHOUSEID", "ISWEEKMNG", "DESCRIPTION", "PARENTPROCESSSEGMENTCLASSID",
                "RTRSHT", "PROCESSSEGMENTCLASSID", "RESOURCENAME", "ISCLAIMLOT", "OSMCHECK", "PROCESSUOM", "PCSARY", "BLKQTY", "RESOURCEID");
            grdLotInfo.Enabled = false;
          
            pfsInfo.SetControlsVisible();
            //pfsInfo.Enabled = false;
            pfsInfo.SetReadOnly(true);
            pfsInfo.UnitComboReadOnly = true;
            pfsEquipment.ProcessType = ProcessType.WorkCompletion;
           
            pfsEquipment.SetEquipmentWorkTimeColumnHidden();

            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "AREATYPE=Area");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, false);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");

            areaCondition.Conditions.AddTextBox("AREA");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            txtArea.Editor.SelectPopupCondition = areaCondition;

            txtArea.Editor.Font = new Font("Tahoma", 14);
            txtLotId.Editor.Font = new Font("Tahoma", 14);
            txtConsumableLotIdComplete.Editor.Font = new Font("Tahoma", 14);
        }

        private void InitializeGrid()
        {
            #region 자재 그리드
            grdConsumableComplete.GridButtonItem = GridButtonItem.Delete;
            grdConsumableComplete.ShowStatusBar = false;

            grdConsumableComplete.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdConsumableComplete.View.EnableRowStateStyle = false;

            // Area Id
            grdConsumableComplete.View.AddTextBoxColumn("AREAID", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // Warehouse Id
            grdConsumableComplete.View.AddTextBoxColumn("WAREHOUSEID", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // Key
            grdConsumableComplete.View.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목코드
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsReadOnly();
            // 품목버전
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목명
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetIsReadOnly();
            // 자재 LOT ID
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLELOTID", 150)
                .SetIsReadOnly();
            // 재고수량
            grdConsumableComplete.View.AddSpinEditColumn("STOCKQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // 가용수량
            grdConsumableComplete.View.AddSpinEditColumn("AVAILABLEQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // 투입수량
            grdConsumableComplete.View.AddSpinEditColumn("INPUTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetValidationIsRequired();
            // 실투입량
            grdConsumableComplete.View.AddSpinEditColumn("ORGINPUTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly()
                .SetIsHidden();
            // 불량수량
            grdConsumableComplete.View.AddSpinEditColumn("DEFECTQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // Lot사용수량
            grdConsumableComplete.View.AddSpinEditColumn("LOTUSINGQTY", 100)
                .SetDisplayFormat("#,##0.#########")
                .SetIsReadOnly();
            // 자재불량
            grdConsumableComplete.View.AddButtonColumn("CONSUMABLEDEFECT", 100);
            // 상태
            grdConsumableComplete.View.AddTextBoxColumn("CONSUMABLESTATE", 80)
                .SetIsReadOnly()
                .SetIsHidden();

            grdConsumableComplete.View.PopulateColumns();


            grdConsumableComplete.View.OptionsView.ShowIndicator = false;
            grdConsumableComplete.View.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;


            InitializeStandardRequirementGrid(grdStandardRequirementComplete);

            _consumableDefectList = new DataTable();
            _consumableDefectList.Columns.Add("CONSUMABLELOTID", typeof(string));
            _consumableDefectList.Columns.Add("DEFECTQTY", typeof(decimal));
            _consumableDefectList.Columns.Add("REASONCODE", typeof(string));
            #endregion

            #region 치공구 그리드
            grdDurableComplete.GridButtonItem = GridButtonItem.None;
            grdDurableComplete.ShowStatusBar = false;

            grdDurableComplete.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdDurableComplete.View.EnableRowStateStyle = false;

            // Transaction History Key
            grdDurableComplete.View.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목코드
            grdDurableComplete.View.AddTextBoxColumn("DURABLEDEFID", 150)
                .SetIsReadOnly();
            // 품목버전
            grdDurableComplete.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 품목명
            grdDurableComplete.View.AddTextBoxColumn("DURABLEDEFNAME", 200)
                .SetIsReadOnly();
            // 치공구 LOT ID
            grdDurableComplete.View.AddTextBoxColumn("DURABLELOTID", 150)
                .SetIsReadOnly();
            // 작업시작시간
            grdDurableComplete.View.AddTextBoxColumn("WORKSTARTTIME", 130)
                .SetIsReadOnly();
            // 작업완료시간
            grdDurableComplete.View.AddTextBoxColumn("WORKENDTIME", 130)
                .SetIsReadOnly();
            // 사용타수
            grdDurableComplete.View.AddSpinEditColumn("USEDCOUNT", 90)
                .SetIsReadOnly();
            // 누적타수
            grdDurableComplete.View.AddSpinEditColumn("TOTALUSEDCOUNT", 90)
                .SetIsReadOnly();
            // 보증타수
            grdDurableComplete.View.AddSpinEditColumn("USEDLIMIT", 90)
                .SetIsReadOnly();
            // 연마기준타수
            grdDurableComplete.View.AddSpinEditColumn("CLEANLIMIT", 90)
                .SetIsReadOnly();
            // 사용횟수
            grdDurableComplete.View.AddSpinEditColumn("USINGQTY", 90)
                .SetValidationIsRequired();
            // 설비
            DataTable equipmentList = new DataTable();
            equipmentList.Columns.Add("EQUIPMENTID", typeof(string));
            equipmentList.Columns.Add("EQUIPMENTNAME", typeof(string));
            grdDurableComplete.View.AddComboBoxColumn("EQUIPMENTID", 150, new SqlQueryAdapter(equipmentList), "EQUIPMENTNAME", "EQUIPMENTID")
                .SetLabel("EQUIPMENT")
                .SetIsReadOnly();
            // 마지막 치공구 여부
            grdDurableComplete.View.AddTextBoxColumn("ISLASTDURABLE", 70)
                .SetIsReadOnly()
                .SetIsHidden();

            grdDurableComplete.View.PopulateColumns();


            grdDurableComplete.View.OptionsView.ShowIndicator = false;


            grdBORDurableComplete.GridButtonItem = GridButtonItem.None;
            grdBORDurableComplete.ShowStatusBar = false;
            grdBORDurableComplete.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdBORDurableComplete.View.SetIsReadOnly();

            // 치공구 ID
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLEDEFID", 150);
            // 치공구 버전
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLEDEFVERSION", 80)
                .SetIsHidden();
            // 치공구 명
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLEDEFNAME", 300);
            // 치공구 그룹 ID
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLECLASSID", 100)
                .SetIsHidden();
            // 치공구 유형
            grdBORDurableComplete.View.AddTextBoxColumn("DURABLECLASSNAME", 100)
                .SetLabel("DURABLETYPE");
            // 보증타수
            grdBORDurableComplete.View.AddSpinEditColumn("USEDLIMIT", 90);
            // 연마기준타수
            grdBORDurableComplete.View.AddSpinEditColumn("CLEANLIMIT", 90);

            grdBORDurableComplete.View.PopulateColumns();


            grdBORDurableComplete.View.OptionsView.ShowIndicator = false;
            #endregion
        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            btnInit.Click += BtnInit_Click;

            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += TxtLotId_Click;

            pfsEquipment.EquipmentCheckedChange += PfsEquipment_EquipmentCheckedChange;

            txtConsumableLotIdComplete.Editor.KeyDown += TxtConsumableLotIdComplete_KeyDown;
            txtConsumableLotIdComplete.Editor.PreviewKeyDown += TxtConsumableLotIdComplete_PreviewKeyDown;
            grdConsumableComplete.ToolbarDeleteRow += GrdConsumableComplete_ToolbarDeleteRow;
            grdConsumableComplete.View.CellValueChanged += ConsumableCompleteView_CellValueChanged;
            grdConsumableComplete.View.GridCellButtonClickEvent += ConsumableCompleteView_GridCellButtonClickEvent;

            grdDurableComplete.ToolbarDeleteRow += GrdDurableComplete_ToolbarDeleteRow;

            btnRegDefect.Click += BtnRegDefect_Click;
        }

        private void BtnRegDefect_Click(object sender, EventArgs e)
        {
            DataTable dtLotinfo = grdLotInfo.DataSource as DataTable;

            if (dtLotinfo == null) return;

            RegDefectPopup pop = new RegDefectPopup(grdLotInfo.DataSource as DataTable,_dtDefect);

            pop.ShowDialog();
            if (pop.DialogResult == DialogResult.OK)
            {
                _dtDefect = pop.defectList;
            }

            if (_dtDefect == null || _dtDefect.Rows.Count == 0) return;

            decimal DefectSum = _dtDefect.AsEnumerable().Select(c => c.Field<decimal>("QTY")).Sum();
            /*
            decimal pcsary = Format.GetDecimal(dtLotinfo.Rows[0]["PCSARY"]);
            decimal pcspnl = Format.GetDecimal(dtLotinfo.Rows[0]["PCSPNL"]);
            decimal panelPerQty = Format.GetDecimal(dtLotinfo.Rows[0]["PANELPERQTY"]);
            string processUom = Format.GetTrimString(dtLotinfo.Rows[0]["PROCESSUOM"]);
            */
            pfsInfo.SetQty(DefectSum);
        }

        private void GrdDurableComplete_ToolbarDeleteRow(object sender, EventArgs e)
        {
            (grdDurableComplete.View.DataSource as DataView).Delete(grdDurableComplete.View.FocusedRowHandle);
            (grdDurableComplete.View.DataSource as DataView).Table.AcceptChanges();
        }
        #region 자재 관련 이벤트
        private void ConsumableCompleteView_GridCellButtonClickEvent(SmartBandedGridView sender, GridCellButtonClickEventArgs e)
        {
            if (e.FieldName == "CONSUMABLEDEFECT")
            {
                string consumableLotId = grdConsumableComplete.View.GetFocusedRowCellValue("CONSUMABLELOTID").ToString();
                //decimal currentQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("QTY"));
                //decimal originalQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("ORGQTY"));
                decimal stockQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("STOCKQTY"));
                decimal availableQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("AVAILABLEQTY"));
                decimal inputQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("INPUTQTY"));
                decimal originalInputQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("ORGINPUTQTY"));
                decimal defectQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("DEFECTQTY"));



                ConsumableDefectProcessPopup popup = new ConsumableDefectProcessPopup(consumableLotId, availableQty, _consumableDefectList);
                popup.ShowDialog();

                

                if (popup.DialogResult == DialogResult.OK)
                {
                    decimal inputDefectQty = popup.GetDefectQty();

                    grdConsumableComplete.View.SetFocusedRowCellValue("DEFECTQTY", inputDefectQty);

                    if (inputDefectQty + _consumableQty[consumableLotId] > availableQty)
                    {
                        grdConsumableComplete.View.SetFocusedRowCellValue("INPUTQTY", availableQty);
                        grdConsumableComplete.View.SetFocusedRowCellValue("ORGINPUTQTY", availableQty - inputDefectQty);
                    }
                    else
                    {
                        grdConsumableComplete.View.SetFocusedRowCellValue("INPUTQTY", inputDefectQty + _consumableQty[consumableLotId]);
                        grdConsumableComplete.View.SetFocusedRowCellValue("ORGINPUTQTY", _consumableQty[consumableLotId]);
                    }
                }
            }
        }

        private void ConsumableCompleteView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "INPUTQTY")
            {
                grdConsumableComplete.View.CellValueChanged -= ConsumableCompleteView_CellValueChanged;

                decimal stockQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("STOCKQTY"));
                decimal inputQty = Format.GetDecimal(e.Value);
                decimal defectQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("DEFECTQTY"));
                decimal lotUsingQty = Format.GetDecimal(grdConsumableComplete.View.GetFocusedRowCellValue("LOTUSINGQTY"));

                if (inputQty > stockQty - lotUsingQty)
                {
                    // 투입수량은 가용수량(재고수량 - Lot 사용수량)을 초과할 수 없습니다. 가용수량 = {0}
                    MSGBox.Show(MessageBoxType.Warning, "InputQtyGreaterThanAvailableQty", (stockQty - lotUsingQty).ToString("#,##0.#####"));

                    grdConsumableComplete.View.SetFocusedRowCellValue("INPUTQTY", stockQty - lotUsingQty);

                    grdConsumableComplete.View.CellValueChanged += ConsumableCompleteView_CellValueChanged;

                    return;
                }

                decimal originalInputQty = inputQty - defectQty;


                grdConsumableComplete.View.SetFocusedRowCellValue("ORGINPUTQTY", originalInputQty);

                grdConsumableComplete.View.CellValueChanged += ConsumableCompleteView_CellValueChanged;
            }
        }
       
        private void GrdConsumableComplete_ToolbarDeleteRow(object sender, EventArgs e)
        {
            if (grdConsumableComplete.View.FocusedRowHandle < 0)
                return;

            //grdConsumableComplete.View.DeleteRow(grdConsumableComplete.View.FocusedRowHandle);
            (grdConsumableComplete.View.DataSource as DataView).Delete(grdConsumableComplete.View.FocusedRowHandle);
            (grdConsumableComplete.View.DataSource as DataView).Table.AcceptChanges();
        }


        private void TxtConsumableLotIdComplete_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                KeyEventArgs args = new KeyEventArgs(Keys.Enter);
                TxtConsumableLotIdComplete_KeyDown(sender, args);
            }
        }
        private void TxtConsumableLotIdComplete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!CheckAlreadyInputLot(txtConsumableLotIdComplete.Text, "CONSUMABLELOTID", grdConsumableComplete))
                {
                    txtConsumableLotIdComplete.Text = "";
                    return;
                }

                string lotid = Format.GetTrimString(grdLotInfo.GetFieldValue("LOTID"));

                DataTable lotinfo = grdLotInfo.DataSource as DataTable;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LOTID", lotid);
                param.Add("CONSUMABLELOTID", txtConsumableLotIdComplete.Text);

                txtConsumableLotIdComplete.Text = "";


                DataTable consumableList = new DataTable();

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                {
                    if (Format.GetString(lotinfo.AsEnumerable().FirstOrDefault()["DESCRIPTION"]) == "MIG")
                        consumableList = SqlExecuter.Query("GetConsumableLotByProcess_MIG", _queryVersion, param);
                    else
                        consumableList = SqlExecuter.Query("GetConsumableLotByProcess_YP", _queryVersion, param);
                }
                else
                    consumableList = SqlExecuter.Query("GetConsumableLotByProcess", _queryVersion, param);

                if (consumableList.Rows.Count > 0)
                {
                    DataRow consumableRow = consumableList.Rows.Cast<DataRow>().FirstOrDefault();
                    DataRow lotRow = lotinfo.Rows.Cast<DataRow>().FirstOrDefault();

                    string lotType = Format.GetString(lotRow["LOTTYPE"]);

                    string currentAreaId = Format.GetString(lotRow["AREAID"]);
                    string consumableAreaId = Format.GetString(consumableRow["AREAID"]);

                    string currentWarehouseId = Format.GetString(lotRow["WAREHOUSEID"]);
                    string consumableWarehouseId = Format.GetString(consumableRow["WAREHOUSEID"]);

                    string consumableState = Format.GetString(consumableRow["CONSUMABLESTATE"]);

                    if (currentWarehouseId != consumableWarehouseId)
                    {
                        // 현재 창고에서 보유하고 있는 자재가 아닙니다. 현재 창고 ID : {0}, 소유 창고 ID : {1}
                        throw MessageException.Create("ConsumableNotInWarehouse", currentWarehouseId, consumableWarehouseId);
                    }

                    /*
                    if (lotType != "Sample" && currentAreaId != consumableAreaId)
                    {
                        // 현재 작업장에서 보유하고 있는 자재가 아닙니다. 현재 작업장 ID : {0}, 소유 작업장 ID : {1}
                        throw MessageException.Create("ConsumableNotInArea", currentAreaId, consumableAreaId);
                    }
                    */

                    if (consumableState == "Consumed")
                    {
                        // 해당 자재 Lot은 사용이 완료 된 Lot 입니다. Lot Id : {0}
                        throw MessageException.Create("ConsumedConsumableLot", lotid);
                    }

                    if (consumableState == "Scrapped")
                    {
                        // 해당 자재 Lot은 폐기 처리 된 Lot 입니다. Lot Id : {0}
                        throw MessageException.Create("ScrappedConsumableLot", lotid);
                    }

                    decimal inputQty = Format.GetDecimal(consumableRow["INPUTQTY"]);

                    if (inputQty <= 0)
                    {
                        decimal stockQty = Format.GetDecimal(consumableRow["STOCKQTY"]);
                        decimal lotUsingQty = Format.GetDecimal(consumableRow["LOTUSINGQTY"]);

                        // 해당 자재 Lot은 다른 Lot에서 모두 사용되었습니다. 재고수량 : {0}, Lot 사용수량 : {1}
                        throw MessageException.Create("ConsumableLotIsAlreadyUsingInLot", stockQty.ToString("#,##0.#####"), lotUsingQty.ToString("#,##0.#####"));
                    }

                    string consumableLotId = Format.GetString(consumableRow["CONSUMABLELOTID"]);
                    string consumableDefId = Format.GetString(consumableRow["CONSUMABLEDEFID"]);
                    string consumableDefVersion = Format.GetString(consumableRow["CONSUMABLEDEFVERSION"]);

                    Dictionary<string, object> checkParam = new Dictionary<string, object>();
                    checkParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    checkParam.Add("PLANTID", UserInfo.Current.Plant);
                    checkParam.Add("AREAID", Format.GetString(lotRow["AREAID"]));
                    checkParam.Add("CONSUMABLELOTID", consumableLotId);
                    checkParam.Add("CONSUMABLEDEFID", consumableDefId);
                    checkParam.Add("CONSUMABLEDEFVERSION", consumableDefVersion);

                    DataTable priorityConsumableList = SqlExecuter.Query("GetPriorityConsumableLotByProcess", "10001", checkParam);

                    if (priorityConsumableList.Rows.Count > 0)
                    {
                        // 해당 자재 보다 먼저 입고된 자재 Lot이 있습니다. 그래도 진행 하겠습니까?
                        if (MSGBox.Show(MessageBoxType.Question, "ExistsFirstStockLot", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }

                    DataTable dataSource = grdConsumableComplete.DataSource as DataTable;

                    var query = from consumable in dataSource.Rows.Cast<DataRow>()
                                group consumable by consumable["KEYCOLUMN"].ToString() into g
                                select new
                                {
                                    ConsumableId = g.Key,
                                    InputQty = g.Sum(qty => Format.GetDecimal(qty["ORGINPUTQTY"]))
                                };

                    string key = Format.GetString(consumableRow["KEYCOLUMN"]);

                    Dictionary<string, decimal> inputQtyList = query.ToDictionary(value => value.ConsumableId, value => value.InputQty);

                    inputQtyList.ForEach(value =>
                    {
                        if (key == value.Key)
                        {
                            decimal reqQty = Format.GetDecimal(grdStandardRequirementComplete.View.GetRowCellValue(grdStandardRequirementComplete.View.LocateByValue("KEYCOLUMN", value.Key), "REQUIREMENTQTY"));

                            if (reqQty - value.Value <= 0)
                            {
                                consumableRow["INPUTQTY"] = 0;
                                consumableRow["ORGINPUTQTY"] = 0;
                            }
                            else
                            {
                                if (Format.GetDecimal(consumableRow["INPUTQTY"]) > reqQty - value.Value)
                                {
                                    consumableRow["INPUTQTY"] = reqQty - value.Value;
                                    consumableRow["ORGINPUTQTY"] = reqQty - value.Value;
                                }
                            }
                        }
                    });

                    consumableList.Rows.Cast<DataRow>().ForEach(row =>
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];

                        dataSource.Rows.Add(newRow);


                        string consumableLot = Format.GetString(row["CONSUMABLELOTID"]);
                        if (_consumableQty.Keys.Contains(consumableLot))
                        {
                            _consumableQty[consumableLot] = _consumableQty[consumableLot] + Format.GetDecimal(row["ORGINPUTQTY"]);
                        }
                        else
                        {
                            _consumableQty.Add(consumableLot, Format.GetDecimal(row["ORGINPUTQTY"]));
                        }
                    });

                    dataSource.AcceptChanges();


                    return;
                }


                DataTable alternativeList = SqlExecuter.Query("GetConsumableAlternativeLotByProcess", _queryVersion, param);

                if (alternativeList.Rows.Count > 0)
                {
                    DataRow consumableRow = alternativeList.Rows.Cast<DataRow>().FirstOrDefault();

                    DataRow lotRow = lotinfo.Rows.Cast<DataRow>().FirstOrDefault();

                    string lotType = Format.GetString(lotRow["LOTTYPE"]);

                    string currentAreaId = Format.GetString(lotRow["AREAID"]);
                    string consumableAreaId = Format.GetString(consumableRow["AREAID"]);

                    string currentWarehouseId = Format.GetString(lotRow["WAREHOUSEID"]);
                    string consumableWarehouseId = Format.GetString(consumableRow["WAREHOUSEID"]);

                    if (currentWarehouseId != consumableWarehouseId)
                    {
                        // 현재 창고에서 보유하고 있는 자재가 아닙니다. 현재 창고 ID : {0}, 소유 창고 ID : {1}
                        throw MessageException.Create("ConsumableNotInWarehouse", currentWarehouseId, consumableWarehouseId);
                    }

                    /*
                    if (lotType != "Sample" && currentAreaId != consumableAreaId)
                    {
                        // 현재 작업장에서 보유하고 있는 자재가 아닙니다. 현재 작업장 ID : {0}, 소유 작업장 ID : {1}
                        throw MessageException.Create("ConsumableNotInArea", currentAreaId, consumableAreaId);
                    }
                    */

                    DataTable dataSource = grdConsumableComplete.DataSource as DataTable;

                    var query = from consumable in dataSource.Rows.Cast<DataRow>()
                                group consumable by consumable["KEYCOLUMN"].ToString() into g
                                select new
                                {
                                    ConsumableId = g.Key,
                                    InputQty = g.Sum(qty => Format.GetDecimal(qty["ORGINPUTQTY"]))
                                };

                    string key = Format.GetString(consumableRow["KEYCOLUMN"]);

                    Dictionary<string, decimal> inputQtyList = query.ToDictionary(value => value.ConsumableId, value => value.InputQty);

                    inputQtyList.ForEach(value =>
                    {
                        if (key == value.Key)
                        {
                            decimal reqQty = Format.GetDecimal(grdStandardRequirementComplete.View.GetRowCellValue(grdStandardRequirementComplete.View.LocateByValue("KEYCOLUMN", value.Key), "REQUIREMENTQTY"));

                            if (reqQty - value.Value < 0)
                            {
                                consumableRow["INPUTQTY"] = 0;
                                consumableRow["ORGINPUTQTY"] = 0;
                            }
                            else
                            {
                                if (Format.GetDecimal(consumableRow["INPUTQTY"]) > reqQty - value.Value)
                                {
                                    consumableRow["INPUTQTY"] = reqQty - value.Value;
                                    consumableRow["ORGINPUTQTY"] = reqQty - value.Value;
                                }
                            }
                        }
                    });

                    alternativeList.Rows.Cast<DataRow>().ForEach(row =>
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[];

                        dataSource.Rows.Add(newRow);


                        string consumableLot = Format.GetString(row["CONSUMABLELOTID"]);
                        if (_consumableQty.Keys.Contains(consumableLot))
                        {
                            _consumableQty[consumableLot] = _consumableQty[consumableLot] + Format.GetDecimal(row["ORGINPUTQTY"]);
                        }
                        else
                        {
                            _consumableQty.Add(consumableLot, Format.GetDecimal(row["ORGINPUTQTY"]));
                        }
                    });

                    dataSource.AcceptChanges();


                    return;
                }

                // 해당 자재 Lot의 품목은 BOM에 등록 되지 않은 자재 입니다.
                MSGBox.Show(MessageBoxType.Information, "NotExistsConsumableInBOM");
            }
        }
        #endregion


        private void PfsEquipment_EquipmentCheckedChange()
        {
            SetDurableLotEquipmentDataSource(pfsEquipment.GetEquipmentCheckeRow());
        }


        private void BtnInit_Click(object sender, EventArgs e)
        {
            txtLotId.Text = "";

            ClearDetailInfo();
        }

        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearDetailInfo();
                _dtDefect = null;
                if (txtArea.Editor.SelectedData.Count() < 1 || string.IsNullOrWhiteSpace(txtArea.EditValue.ToString()) || string.IsNullOrWhiteSpace(txtLotId.Text))
                {
                    // 작업장, LOT No.는 필수 입력 항목입니다.
                    ShowMessage("AreaLotIdIsRequired");
                    return;
                }

                var buttons = pnlToolbar.Controls.Find<SmartButton>(true);

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", txtLotId.Text);

                DataTable dt = SqlExecuter.Query("selectAreaResourceByLot", "10001", param);

                if (dt.Rows.Count < 1)
                {
                    //존재 하지 않는 Lot No. 입니다.
                    ShowMessage("NotExistLotNo");
                    return;
                }
                //작업장이나, 자원이 없을 경우
                if (string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["AREAID"])) || string.IsNullOrWhiteSpace(Format.GetFullTrimString(dt.Rows[0]["RESOURCEID"])))
                {
                    SelectResourcePopup resourcePopup = new SelectResourcePopup(Format.GetString(dt.Rows[0]["LOTID"]), Format.GetString(dt.Rows[0]["PROCESSSEGMENTID"]), string.Empty);
                    resourcePopup.ShowDialog();

                    if (resourcePopup.DialogResult == DialogResult.OK)
                    {
                        MessageWorker resourceWorker = new MessageWorker("SaveLotResourceId");
                        resourceWorker.SetBody(new MessageBody()
                            {
                                { "LotId", txtLotId.Text },
                                { "ResourceId", resourcePopup.ResourceId }
                            });

                        resourceWorker.Execute();

                        TxtLotId_KeyDown(sender, e);
                    }
                    else
                    {
                        // 현재 공정에서 사용할 자원을 선택하시기 바랍니다.
                        throw MessageException.Create("SelectResourceForCurrentProcess");
                    }
                }

                try
                {
                    buttons.ForEach(button => button.IsBusy = true);
                    pnlContent.ShowWaitArea();

                    // 재작업 여부 확인
                    param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("PLANTID", UserInfo.Current.Plant);
                    param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
                    param.Add("LOTID", txtLotId.Text);
                    param.Add("PROCESSSTATE", "Run");
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

                    string processDefType = "";
                    string lastRework = "";
                    string processDefId = "";
                    string processDefVersion = "";

                    if (processDefTypeInfo.Rows.Count > 0)
                    {
                        DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                        processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                        processDefId = Format.GetString(row["PROCESSDEFID"]);
                        processDefVersion = Format.GetString(row["PROCESSDEFVERSION"]);
                        lastRework = Format.GetString(row["LASTREWORK"]);
                    }

                    string queryVersion = "10001";

                    if (processDefType == "Rework")
                        queryVersion = "10011";


                    _queryVersion = queryVersion;
                    // TODO : Query Version 변경
                    DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", processDefType == "Rework" ? "10032" : "10031", param);

                    if (lotInfo.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);

                        ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }
                    //AOI,BBT,최종,출하,포장 공정은 해당 화면을 사용할 수 없습니다.

                    string processsegmentclass = Format.GetTrimString(lotInfo.Rows[0]["PROCESSSEGMENTCLASSID"]);

                    if( DontUseSegmentList.Contains(processsegmentclass))
                    {
                        ShowMessage("CHECKSEGMENT");
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }

                    pfsEquipment.SetLotInfo(lotInfo);
                    if (!CommonFunction.CheckLotProcessStateByStepType(pfsInfo.ProcessType, lotInfo.Rows[0]["PROCESSSTATE"].ToString(), lotInfo.Rows[0]["STEPTYPE"].ToString()))
                    {
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();
                        return;
                    }


                    //체공 상태 체크
                    DataTable dtStaying = SqlExecuter.Query("GetCheckStaying", "10001", param);

                    if (Format.GetTrimString(dtStaying.Rows[0]["ISLOCKING"]).Equals("Y"))
                    {
                        StayReasonCode popup = new StayReasonCode(dtStaying);
                        popup.ShowDialog();

                        if (popup.DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                    }

                    if (!CommonFunction.CheckRCLot(lotInfo))
                    {
                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }

                    string isHold = Format.GetString(lotInfo.Rows[0]["ISHOLD"]);
                    string isLocking = Format.GetString(lotInfo.Rows[0]["ISLOCKING"]);

                    if (isHold == "Y")
                    {
                        // 보류 상태의 Lot 입니다.
                        ShowMessage("LotIsHold", string.Format("LotId = {0}", txtLotId.Text));

                        ClearDetailInfo();

                        txtLotId.Text = "";
                        txtLotId.Focus();

                        return;
                    }
                    //InTransit 상태 체크
                    if (Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("InTransit"))
                    {
                        // 물류창고 입/출고 처리 대상입니다
                        ShowMessage("CheckOSLogisticStatus");
                        return;
                    }
                    //if (isLocking == "Y")
                    //{
                    //    // Locking 상태의 Lot 입니다.
                    //    ShowMessage("LotIsLocking", string.Format("LotId = {0}", txtLotId.Text));

                    //    ClearDetailInfo();

                    //    txtLotId.Text = "";
                    //    txtLotId.Focus();

                    //    return;
                    //}

                    //InTransit 상태 체크
                    if (Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("InTransit"))
                    {
                        // 물류창고 입/출고 처리 대상입니다
                        ShowMessage("CheckOSLogisticStatus");
                        return;
                    }
                    else if (Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("OverSeaInTransit"))
                    {
                        // 해외 물류 이동중입니다.
                        ShowMessage("CheckOverSeasLogistic");
                    }

                    this.lotId = txtLotId.Text;
                    this.isMessageLoaded = false;
                    grdLotInfo.DataSource = lotInfo;
                    pfsInfo.SetControlsInfo(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString(), txtLotId.Text, lotInfo, queryVersion, lastRework);

                    grdLotInfo.Enabled = true;
                    pfsInfo.SetReadOnly(false);

                    string productDefVersion = Format.GetString(grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                    string isRcLot = Format.GetString(grdLotInfo.GetFieldValue("ISRCLOT"));

                    lblInnerRevisionText.Text = productDefVersion;
                    if (processDefType == "Rework")
                        lblIsRework.Visible = true;
                    else
                        lblIsRework.Visible = false;
                    if (isRcLot == "Y")
                        lblIsRCLot.Visible = true;
                    else
                        lblIsRCLot.Visible = false;


                    //2021.03.04 UOM정보 셋팅
                    pfsInfo.setBasicSetUOM();

                    Dictionary<string, object> lotWorkerParam = new Dictionary<string, object>();
                    lotWorkerParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    lotWorkerParam.Add("PLANTID", UserInfo.Current.Plant);
                    lotWorkerParam.Add("LOTID", txtLotId.Text);

                    DataTable lotWorkerInfo = SqlExecuter.Query("GetLotWorkerByTrackOut", "10001", lotWorkerParam);

                    if (lotWorkerInfo.Rows.Count > 0)
                    {
                        string workerId = string.Join(",", lotWorkerInfo.Rows.Cast<DataRow>().Select(row => Format.GetString(row["WORKERID"])));
                        string workerName = string.Join(",", lotWorkerInfo.Rows.Cast<DataRow>().Select(row => Format.GetString(row["WORKERNAME"])));

                        pfsInfo.SetWorker(workerId, workerName);
                    }


                    Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
                    equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
                    equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    equipmentParam.Add("LOTID", txtLotId.Text);
                    equipmentParam.Add("EQUIPMENTTYPE", "Production");
                    equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");
                    // TODO : Query Version 변경
                    DataTable equipmentList = SqlExecuter.Query("GetEquipmentByArea", "10031", equipmentParam);
                    DataTable lotEquipmentList = SqlExecuter.Query("GetLotEquipmentByArea", "10031", equipmentParam);

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong && lotEquipmentList.Rows.Count == 1)
                    {
                        decimal pcsQty = Format.GetInteger(grdLotInfo.GetFieldValue("PCSQTY"));
                        decimal panelPerQty = Format.GetInteger(grdLotInfo.GetFieldValue("PANELPERQTY"));
                        decimal pnlQty = 0;
                        if (panelPerQty > 0)
                            pnlQty = Math.Ceiling(pcsQty / panelPerQty);

                        DataRow equipmentRow = lotEquipmentList.AsEnumerable().First();
                        equipmentRow["PCSQTY"] = pcsQty;
                        equipmentRow["PNLQTY"] = pnlQty;

                        lotEquipmentList.AcceptChanges();
                    }

                    List<string> lstLotEquipment = lotEquipmentList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])).ToList();

                    pfsEquipment.SetEquipmentDataSource(lotEquipmentList);


                    Dictionary<string, object> consumableParam = new Dictionary<string, object>();
                    consumableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    consumableParam.Add("PLANTID", UserInfo.Current.Plant);
                    consumableParam.Add("LOTID", txtLotId.Text);
                    consumableParam.Add("MATERIALTYPE", "Consumable");


                    DataTable consumableList = SqlExecuter.Query("SelectConsumableListByProcessWorkComplete", queryVersion, consumableParam);

                    SetConsumableWorkCompleteDataSource(consumableList);

                    Dictionary<string, object> standardRequirementParam = new Dictionary<string, object>();
                    standardRequirementParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    standardRequirementParam.Add("PLANTID", UserInfo.Current.Plant);
                    standardRequirementParam.Add("LOTID", txtLotId.Text);

                    DataTable standardRequirementInfo = new DataTable();

                    if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    {
                        if (Format.GetString(grdLotInfo.GetFieldValue("DESCRIPTION")) == "MIG")
                            standardRequirementInfo = SqlExecuter.Query("SelectStandardRequirementByProcess_MIG", "10001", standardRequirementParam);
                        else
                            standardRequirementInfo = SqlExecuter.Query("SelectStandardRequirementByProcess_YP", "10001", standardRequirementParam);
                    }
                    else
                        standardRequirementInfo = SqlExecuter.Query("SelectStandardRequirementByProcess", queryVersion, standardRequirementParam);

                    grdStandardRequirementComplete.DataSource = standardRequirementInfo;


                    //치공구 BOR 등록 
                    Dictionary<string, object> durableParam = new Dictionary<string, object>();
                    durableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    durableParam.Add("PLANTID", UserInfo.Current.Plant);
                    durableParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    durableParam.Add("LOTID", txtLotId.Text);
                    durableParam.Add("MATERIALTYPE", "Durable");

                    DataTable durableList = SqlExecuter.Query("GetDurableDEFByBOR", queryVersion, durableParam);

                    grdBORDurableComplete.DataSource = durableList;


                    DataTable durableLotList = SqlExecuter.Query("SelectDurableListByProcessWorkComplete", "10002", durableParam);

                    grdDurableComplete.DataSource = durableLotList;

                    // 설비별 Recipe 조회
                    //string equipmentId = string.Join(",", equipmentList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));
                    string equipmentId = string.Join(",", lotEquipmentList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));
                    string recipeNameFieldName = "";
                    string parameterNameFieldName = "";

                    switch (UserInfo.Current.LanguageType)
                    {
                        case "ko-KR":
                            recipeNameFieldName = "KRECIPENAME";
                            parameterNameFieldName = "KPARAMETERNAME";
                            break;
                        case "en-US":
                            recipeNameFieldName = "ERECIPENAME";
                            parameterNameFieldName = "EPARAMETERNAME";
                            break;
                        case "zh-CN":
                            recipeNameFieldName = "CRECIPENAME";
                            parameterNameFieldName = "CPARAMETERNAME";
                            break;
                        case "vi-VN":
                            recipeNameFieldName = "VRECIPENAME";
                            parameterNameFieldName = "VPARAMETERNAME";
                            break;
                    }

                    string selectedRecipeList = "";

                    lotEquipmentList.AsEnumerable().ForEach(r =>
                    {
                        string recipeId = Format.GetString(r["RECIPEID"]);
                        string recipeVersion = Format.GetString(r["RECIPEVERSION"]);

                        if (!string.IsNullOrEmpty(recipeId) && !string.IsNullOrEmpty(recipeVersion))
                        {
                            string recipe = string.Join("|", recipeId, recipeVersion);

                            selectedRecipeList += recipe + ",";
                        }
                    });

                    selectedRecipeList = selectedRecipeList.TrimEnd(',');

                    Dictionary<string, object> recipeParam = new Dictionary<string, object>();
                    recipeParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                    recipeParam.Add("RECIPENAME", recipeNameFieldName);
                    recipeParam.Add("PARAMETERNAME", parameterNameFieldName);
                    recipeParam.Add("PRODUCTID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
                    recipeParam.Add("PRODUCTVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                    recipeParam.Add("PROCESSDEFID", processDefId);
                    recipeParam.Add("PROCESSDEFVERSION", processDefVersion);
                    recipeParam.Add("SEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID"));
                    recipeParam.Add("RESOURCEID", grdLotInfo.GetFieldValue("RESOURCEID"));
                    recipeParam.Add("LOTID", txtLotId.Text);
                    recipeParam.Add("EQUIPMENTID", equipmentId);
                    recipeParam.Add("RECIPEIDVERSION", selectedRecipeList);

                    DataTable equipmentRecipeInfo = SqlExecuter.Query("SelectEquipmentRecipe", queryVersion, recipeParam);

                    pfsEquipment.SetEquipmentRecipeDataSource(equipmentRecipeInfo);
                    

                    pfsEquipment.SetRecipeComboDataSource(null);

                    string uom = lotInfo.Rows[0]["PROCESSUOM"].ToString();
                    switch(uom)
                    {
                        case "PNL":
                        case "BLK":
                            pfsEquipment.EnableColumn("PNLQTY");
                            pfsEquipment.DisableColumn("PCSQTY");
                            break;
                        case "PCS":
                            pfsEquipment.EnableColumn("PCSQTY");
                            pfsEquipment.DisableColumn("PNLQTY");
                            break;
                    }

                    //txtLotId.Text = "";
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.ToString());
                }
                finally
                {
                    pnlContent.CloseWaitArea();
                    buttons.ForEach(button => button.IsBusy = false);
                }
            }
        }

        
        private void TxtLotId_Click(object sender, EventArgs e)
        {
            txtLotId.Editor.SelectAll();
        }
        
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (!grdLotInfo.Enabled)
                throw MessageException.Create("NoSaveData");

            if (ShowMessage(MessageBoxButtons.YesNo, "InfoSave") == DialogResult.No) return;

            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            Dictionary<string, object> workInfo = pfsInfo.GetValues();

            string worker = workInfo["WORKER"].ToString();
            string lotId = grdLotInfo.GetFieldValue("LOTID").ToString();
            string processPathId = grdLotInfo.GetFieldValue("PROCESSPATHID").ToString();
            string processSegmentId = grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString();
            string unit = Format.GetString(workInfo["UNIT"]);
            //string equipmentClassId = workInfo["EQUIPMENTCLASS"].ToString();
            //string equipmentId = workInfo["EQUIPMENT"].ToString();
            double goodQty = Format.GetDouble(workInfo["GOODQTY"], 0);
            double goodPnlQty = Format.GetDouble(workInfo["GOODPNLQTY"], 0);
            double defectQty = Format.GetDouble(workInfo["DEFECTQTY"], 0);
            double defectPnlQty = Format.GetDouble(workInfo["DEFECTPNLQTY"], 0);
            string printWeek = workInfo.ContainsKey("PRINTWEEK") ? workInfo["PRINTWEEK"].ToString() : "";
            string resourceId = workInfo.ContainsKey("RESOURCEID") ? workInfo["RESOURCEID"].ToString() : "";
            string transitArea = workInfo.ContainsKey("TRANSITAREA") ? workInfo["TRANSITAREA"].ToString() : "";

            DataTable recipeParameterInfo = pfsEquipment.GetEquipmentRecipeDataSource();
            
            DataTable consumableInfo = grdConsumableComplete.DataSource as DataTable;
            DataTable durableInfo = grdDurableComplete.DataSource as DataTable;
            //DataTable consumableDefectInfo = pfsDetail.GetConsumableDefectList();

            string equipmentId = string.Join(",", (pfsEquipment.GetEquipmentDataSource()).Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));
            DataTable equipmentInfo = pfsEquipment.GetEquipmentDataSource() ;

            // 기준타수 초과 치공구 조회
            MessageWorker checkDurableLimit = new MessageWorker("CheckDurableOverLimit");
            checkDurableLimit.SetBody(new MessageBody()
            {
                { "LOTID", lotId },
                { "TRANSACTIONTYPE", "TrackOut" },
                { "DURABLELOTIDLIST", string.Join(",", durableInfo.AsEnumerable().Select(row => "'" + row.Field<string>("durablelotid") + "'").ToList()) }
            });

            var saveResult = checkDurableLimit.Execute<DataTable>();
            DataTable result = saveResult.GetResultSet();

            if (result.Rows.Count > 0)
            {
                var useOver95p = new List<string>();
                var cleanOver95p = new List<string>();
                var useOver80p = new List<string>();
                var cleanOver80p = new List<string>();

                foreach (DataRow row in result.Rows)
                {
                    if (row["TYPEID"].ToString() == "USELIMITOVER95P")
                    {
                        useOver95p.Add(row["DURABLELOTID"].ToString());
                    }
                    else if(row["TYPEID"].ToString() == "CLEANLIMITOVER95P")
                    {
                        cleanOver95p.Add(row["DURABLELOTID"].ToString());
                    }
                    else if (row["TYPEID"].ToString() == "USELIMITOVER80P")
                    {
                        useOver80p.Add(row["DURABLELOTID"].ToString());
                    }
                    else if (row["TYPEID"].ToString() == "CLEANLIMITOVER80P")
                    {
                        cleanOver80p.Add(row["DURABLELOTID"].ToString());
                    }
                } 

                /*
                if (useOver95p.Count > 0)
                {
                    ReloadDurableLotList();
                    // 치공구의 누적타수가 보증타수를 초과(95%)하여 작업을 진행 할 수 없습니다. {0} 
                    throw MessageException.Create("DurableLotOverUseLimit95P", 
                        string.Format("{0} : {1}", Language.Get("DURABLELOTID"), string.Join(",", useOver95p)));
                }
                if (cleanOver95p.Count > 0)
                {
                    ReloadDurableLotList();
                    // 치공구의 사용타수가 연마기준 타수를 초과(95%)하여 작업을 진행 할 수 없습니다. {0} 
                    throw MessageException.Create("DurableLotOverCleanLimit95P", 
                        string.Format("{0} : {1}", Language.Get("DURABLELOTID"), string.Join(",", cleanOver95p)));
                }
                */
                if (useOver80p.Count > 0 || useOver95p.Count > 0)
                {
                    // 치공구의 누적타수가 보증타수를 초과(80%)합니다. 계속 진행하시겠습니까? {0}
                    var wantProceed = this.ShowMessage(MessageBoxButtons.YesNo, "DurableLotOverUseLimit80P", string.Join(",", useOver80p));
                    if (wantProceed != DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (cleanOver80p.Count > 0 || cleanOver95p.Count > 0)
                {
                    // 치공구의 사용타수가 연마기준 타수를 초과(80%) 합니다. 계속 진행하시겠습니까? {0}
                    var wantProceed = this.ShowMessage(MessageBoxButtons.YesNo, "DurableLotOverCleanLimit80P", string.Join(",", cleanOver80p));
                    if (wantProceed != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }

            string subProcessDefId = "";
            string subProcessDefVersion = "";
            string reworkResourceId = "";
            string reworkAreaId = "";




            /* 2020-03-13 박윤신 추가(Reel 자재 입력 팝업)
            * 
            * //////Reel 자재 입력 팝업 추가
            */
            DataTable reelConsumableInfo = null;

            if (grdLotInfo.GetFieldValue("PROCESSSEGMENTCLASSID").ToString().Equals("7512"))
            {
                AddReelConsumableInputPopup reelConsumablePopup = new AddReelConsumableInputPopup();
                reelConsumablePopup.ShowDialog();
                reelConsumableInfo = reelConsumablePopup.GetData;
            }
          

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker messageWorker = new MessageWorker("SaveTrackOutLot");
                messageWorker.SetBody(new MessageBody()
                {
                    { "EnterpriseId", UserInfo.Current.Enterprise },
                    { "PlantId", UserInfo.Current.Plant },
                    { "Worker", worker },
                    { "LotId", lotId },
                    { "ProcessPathId", processPathId },
                    { "ProcessSegmentId", processSegmentId },
                    { "DefectUnit", unit },
                    //{ "EquipmentClassId", equipmentClassId },
                    { "EquipmentId", equipmentId },
                    { "EquipmentList", equipmentInfo },
                    { "GoodQty", goodQty },
                    { "GoodPnlQty", goodPnlQty },
                    { "DefectQty", defectQty },
                    { "DefectPnlQty", defectPnlQty },
                    { "PrintWeek", printWeek },
                    { "ResourceId", resourceId },
                    { "TransitArea", transitArea },
                    { "Comment", null },
                    { "DefectList", _dtDefect },
                    { "ConsumableList", consumableInfo },
                    { "DurableList", durableInfo },
                    { "ProcessSegmentType", _processSegmentType },
                    { "IsInspectionProcess", _isInspectionProcess },
                    { "IsRepairProcess", _isRepairProcess },
                    //{ "AOIDefectList", aoiDefectInfo },
                    //{ "BBTHOLEDefectList", bbtHoleDefectInfo },
                    { "HasAnalysisTarget", "" },
                    { "IsReworkPublish", "" },
                    { "SubProcessDefId", subProcessDefId },
                    { "SubProcessDefVersion", subProcessDefVersion },
                    { "ReworkResourceId", reworkResourceId },
                    { "ReworkAreaId", reworkAreaId },
                    { "RecipeParameterList", recipeParameterInfo },
                    { "ReelConsumableList", reelConsumableInfo }
                    //{ "AoiBbtDefectList", aoibbtDefectInfo }
                    //{ "ConsumableDefectList", consumableDefectInfo }
                });

                messageWorker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
                ShowMessage("SuccedSave");
            }


            ClearDetailInfo();

            txtLotId.Text = "";
            txtLotId.Focus();
        }

        private void ReloadDurableLotList()
        {
            // 재작업 여부 확인
            var param = new Dictionary<string, object>();
            param.Add("LOTID", txtLotId.Text);
            param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", txtLotId.Text);
            param.Add("PROCESSSTATE", "Run");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            string processDefType = "";

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);
            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();
                processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
            }

            string queryVersion = "10001";

            if (processDefType == "Rework")
                queryVersion = "10011";

            // 치공구 BOR 등록 
            Dictionary<string, object> durableParam = new Dictionary<string, object>();
            durableParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            durableParam.Add("PLANTID", UserInfo.Current.Plant);
            durableParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            durableParam.Add("LOTID", txtLotId.Text);
            durableParam.Add("MATERIALTYPE", "Durable");

            DataTable durableList = SqlExecuter.Query("GetDurableDEFByBOR", queryVersion, durableParam);

            grdBORDurableComplete.DataSource = durableList;


            DataTable durableLotList = SqlExecuter.Query("SelectDurableListByProcessWorkComplete", "10002", durableParam);

            grdDurableComplete.DataSource = durableLotList;

        /*

            // 재작업 여부 확인
            var param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", txtLotId.Text);
            param.Add("PROCESSSTATE", "Wait");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

            string processDefType = "";
            string processDefId = "";
            string processDefVersion = "";

            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow row = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                processDefType = Format.GetString(row["PROCESSDEFTYPE"]);
                processDefId = Format.GetString(row["PROCESSDEFID"]);
                processDefVersion = Format.GetString(row["PROCESSDEFVERSION"]);
            }

            string queryVersion = "10001";

            if (processDefType == "Rework")
                queryVersion = "10011";

            //치공구 BOR 등록 
            Dictionary<string, object> durableparam = new Dictionary<string, object>();
            durableparam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            durableparam.Add("PLANTID", UserInfo.Current.Plant);
            durableparam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            durableparam.Add("LOTID", txtLotId.Text);

            string productDefType = Format.GetString(grdLotInfo.GetFieldValue("PRODUCTDEFTYPEID"));
            if (productDefType == "SubAssembly")
                durableparam.Add("DURABLESTATE", "Available,InUse");
            else
                durableparam.Add("DURABLESTATE", "Available");

            DataTable durableList = SqlExecuter.Query("GetDurableDEFByBOR", queryVersion, durableparam);

            pfsDetail.DurableDefDataSource = durableList;

            DataTable durableLotList = SqlExecuter.Query("SelectDurableListByProcessWorkStart", queryVersion, durableparam);

            pfsDetail.SetDurableWorkStartDataSource(durableLotList);

        */
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
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
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

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            Dictionary<string, object> values = pfsInfo.GetValues();

            if (values.ContainsKey("WORKER") && string.IsNullOrEmpty(values["WORKER"].ToString()))
            {
                // 작업자는 필수 입력 항목입니다.
                throw MessageException.Create("WorkerIsRequired");
            }

            if (values.ContainsKey("GOODQTY") && Format.GetDecimal(values["GOODQTY"]) < 0)
            {
                // 양품수량은 0 보다 커야 합니다.
                throw MessageException.Create("GoodQtyLargerThanZero");
            }

            if (values.ContainsKey("PRINTWEEK") && string.IsNullOrEmpty(Format.GetString(values["PRINTWEEK"])))
            {
                // 인쇄주차를 입력하시기 바랍니다.
                throw MessageException.Create("PrintWeekIsRequired");
            }

            if (values.ContainsKey("TRANSITAREA") && string.IsNullOrEmpty(values["TRANSITAREA"].ToString()))
            {
                // 인계 작업장을 선택하시기 바랍니다.
                throw MessageException.Create("SelectTransitArea");
            }

            if (values.ContainsKey("RESOURCEID") && string.IsNullOrEmpty(values["RESOURCEID"].ToString()))
            {
                // 인계 자원을 선택하시기 바랍니다.
                throw MessageException.Create("SelectTransitResource");
            }

            if ((pfsEquipment.GetEquipmentDataSource()).Rows.Count < 1)
            {
                // 작업 완료 할 설비를 선택하시기 바랍니다.
                throw MessageException.Create("SelectWorkCompletionEquipment");
            }

            //pfsDetail.GetCheckedEquipmentList().Rows.Cast<DataRow>().ForEach(row =>
            //{
            //    string recipeId = Format.GetString(row["RECIPEID"]);

            //    if (string.IsNullOrEmpty(recipeId))
            //    {
            //        // 설비에 사용할 Recipe가 선택되지 않았습니다. Equipment Id = {0}
            //        throw MessageException.Create("NotSelectRecipe", Format.GetString(row["EQUIPMENTID"]));
            //    }
            //});

            string unit = values["UNIT"].ToString();
            decimal panelPerQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PANELPERQTY"));
            decimal goodQty = Format.GetDecimal(values["GOODQTY"]);

            if (unit == "PNL" && panelPerQty > 0 && goodQty % panelPerQty != 0)
            {
                // 단위가 PCS가 아닌 경우 PNL 수량은 정수로 나와야 합니다.
                throw MessageException.Create("PanelQtyHasNotInteger");
            }

            ///일단 자재가 등록 되었는지 먼저 체크 간단하게 2019.09.24 배선용
            
            DataTable consumableRequirementList = grdStandardRequirementComplete.DataSource as DataTable;
            DataTable consumableLotList = grdConsumableComplete.DataSource as DataTable;

            foreach (DataRow row in consumableRequirementList.Rows)
            {
                decimal requirementQty = Format.GetDecimal(row["REQUIREMENTQTY"]);

                if (requirementQty == 0)
                {
                    string consumableId = Format.GetString(row["CONSUMABLEDEFID"]);

                    // 자재 소요량이 0 입니다. Routing의 자재 소요량을 확인하시기 바랍니다. 자재 : {0}
                    throw MessageException.Create("ConsumableRequirementQtyIsZero", consumableId);
                }
            }

            if (consumableRequirementList.Rows.Count > 0 && consumableLotList.Rows.Count == 0)
            {
                // BOM 기준 사용 자재가 모두 등록되지 않았습니다.
                throw MessageException.Create("DoNotInsertMaterialByBOM");

            }
            var query = from consumable in (grdConsumableComplete.DataSource as DataTable).Rows.Cast<DataRow>()
                        group consumable by consumable["KEYCOLUMN"].ToString() into g
                        select new
                        {
                            ConsumableId = g.Key,
                            InputQty = g.Sum(qty => Format.GetDecimal(qty["INPUTQTY"])),
                            RealInputQty = g.Sum(qty => Format.GetDecimal(qty["ORGINPUTQTY"])),
                            DefectQty = g.Sum(qty => Format.GetDecimal(qty["DEFECTQTY"]))
                        };

            Dictionary<string, decimal> consumableList = query.ToDictionary(value => value.ConsumableId, value => value.RealInputQty);

            bool isUsedConsumable = true;
            bool isUseQtyMatch = true;
            bool isRealInputQtyLagerThanRequirementQty = true;
            bool isInputQtyLessThanQty = true;

            string consumableDefId = "";

            consumableRequirementList.Rows.Cast<DataRow>().ForEach(row =>
            {
                if (!consumableList.ContainsKey(row["KEYCOLUMN"].ToString()))
                    isUsedConsumable = false;

                if (consumableList[row["KEYCOLUMN"].ToString()] != Format.GetDecimal(row["REQUIREMENTQTY"]))
                    isUseQtyMatch = false;

                if (consumableList[row["KEYCOLUMN"].ToString()] > Format.GetDecimal(row["REQUIREMENTQTY"]))
                {
                    isRealInputQtyLagerThanRequirementQty = false;
                    consumableDefId = row["KEYCOLUMN"].ToString();
                }
            });

            //DataTable consumableLotList = pfsDetail.ConsumableCompleteDataSource as DataTable; 위로 이동

            consumableLotList.Rows.Cast<DataRow>().ForEach(row =>
            {
                if (Format.GetDecimal(row["INPUTQTY"]) < Format.GetDecimal(row["INPUTQTY"]))
                    isInputQtyLessThanQty = false;
            });

            if (!isUsedConsumable)
            {
                // BOM 기준 사용 자재가 모두 등록되지 않았습니다.
                throw MessageException.Create("DoNotInsertMaterialByBOM");
            }

            if (!isRealInputQtyLagerThanRequirementQty)
            {
                // {0} 자재의 자재 투입량이 BOM 기준 소요량 보다 많습니다. 자재불량을 등록하시기 바랍니다.
                throw MessageException.Create("InputQtyLagerThanRequirementQty", consumableDefId);
            }

            if (!isUseQtyMatch)
            {
                // 사용 자재 수량은 BOM 기준 소요량과 같아야 합니다.
                throw MessageException.Create("UsingMaterialQtyLessThanBomRequirementQty");
            }

            if (!isInputQtyLessThanQty)
            {
                // 투입수량은 자재 수량을 초과할 수 없습니다.
                throw MessageException.Create("CanNotInputQtyLagerThanQty");
            }

            if (!CheckDurable())
            {
                throw MessageException.Create("CheckRequireDurable");
            }

            // 치공구 설비 필수
            DataTable durableLotList = grdDurableComplete.DataSource as DataTable;

            durableLotList.Rows.Cast<DataRow>().ForEach(row =>
            {
                if (string.IsNullOrWhiteSpace(Format.GetString(row["EQUIPMENTID"])))
                {
                    // 설비는 필수로 선택해야 합니다. {0}
                    throw MessageException.Create("EquipmentIsRequired", string.Format("Durable Lot Id : {0}", Format.GetString(row["DURABLELOTID"])));
                }
            });

            if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
            {
                DataTable equipmentList = pfsEquipment.GetEquipmentDataSource() as DataTable;

                int equipmentPcsQty = equipmentList.AsEnumerable().Sum(row => Format.GetInteger(row["PCSQTY"]));
                int lotPcsQty = Format.GetInteger(grdLotInfo.GetFieldValue("PCSQTY"));

                if (equipmentPcsQty != lotPcsQty)
                {
                    // Lot Pcs 수량과 설비에서 작업되는 Pcs 수량의 합이 일치하지 않습니다.
                    throw MessageException.Create("EquipmentQtyIsNotEqualLotQty");
                }
            }

            // 작업 중인 설비 존재 여부 체크
            if (!CheckExistsUsingEquipment())
            {
                // 현재 Lot을 작업 중인 설비가 존재하지 않습니다.
                throw MessageException.Create("NotExistsUsingEquipment");
            }

            // 작업 중인 설비에서 사용되는 치공구 존재 여부 체크
            if (durableLotList.Rows.Count > 0)
            {
                string checkEquipmentId = CheckExistsUsingDurableOnEquipment();
                if (!string.IsNullOrWhiteSpace(checkEquipmentId))
                {
                    // 현재 Lot을 작업 중인 설비에서 사용되는 치공구가 없습니다. Equipment Id : {0}
                    throw MessageException.Create("NotExistsDurableOnEquipment", checkEquipmentId);
                }
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void SetDurableLotEquipmentDataSource(object dataSource)
        {
            grdDurableComplete.View.RefreshComboBoxDataSource("EQUIPMENTID", new SqlQueryAdapter(dataSource as DataTable));
        }
        private void ClearDetailInfo()
        {
            lblInnerRevisionText.Text = "";
            lblIsRework.Visible = false;
            lblIsRCLot.Visible = false;

            _processSegmentType = "";
            _isInspectionProcess = false;
            _isRepairProcess = false;

            grdLotInfo.ClearData();
            pfsInfo.ClearData();
            grdDurableComplete.View.ClearDatas();
            grdBORDurableComplete.View.ClearDatas();
            grdConsumableComplete.View.ClearDatas();
            grdStandardRequirementComplete.View.ClearDatas();
            pfsEquipment.ClearDatas();

            grdLotInfo.Enabled = false;
            //pfsInfo.Enabled = false;
            pfsInfo.SetReadOnly(true);
            pfsInfo.UnitComboReadOnly = true;
           
        }

        private bool CheckDurable()
        {
            DataTable durableInfo = grdDurableComplete.DataSource as DataTable;
            DataTable durableBOR = grdBORDurableComplete.DataSource as DataTable;

            List<string> list = durableBOR.AsEnumerable().Select(c => Format.GetString(c["DURABLEDEFID"])).ToList();

            int icnt = durableInfo.AsEnumerable().Select(c => Format.GetString(c["DURABLEDEFID"])).Where(c => list.Contains(c)).Distinct().Count();

            if (icnt < list.Count)
            {
                return false;
            }
            return true;
        }

        private bool CheckExistsUsingEquipment()
        {
            DataTable equipmentList = pfsEquipment.GetEquipmentDataSource() as DataTable;

            var list = equipmentList.AsEnumerable().Where(row => string.IsNullOrEmpty(Format.GetString(row["TRACKOUTTIME"])));

            if (list.Count() < 1)
                return false;

            return true;
        }
        private void InitializeStandardRequirementGrid(SmartBandedGrid grid)
        {
            grid.GridButtonItem = GridButtonItem.None;
            grid.ShowStatusBar = false;

            grid.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grid.View.SetIsReadOnly();

            // Key
            grid.View.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsHidden();
            // 품목코드
            grid.View.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            // 품목버전
            grid.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80)
                .SetIsHidden();
            // 품목명
            grid.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            // 기준소요량
            grid.View.AddSpinEditColumn("STANDARDREQUIREMENTQTY", 110)
                .SetDisplayFormat("#,##0.#########");
            // 소요량
            grid.View.AddSpinEditColumn("REQUIREMENTQTY", 90)
                .SetDisplayFormat("#,##0.#########");
            // 순번
            grid.View.AddTextBoxColumn("SEQUENCE", 50)
                .SetIsHidden();
            // 총투입량
            //grid.View.AddSpinEditColumn("TOTALINPUTQTY", 80);

            grid.View.PopulateColumns();


            grid.View.OptionsView.ShowIndicator = false;
        }
        private bool CheckAlreadyInputLot(string lotId, string fieldName, SmartBandedGrid grid)
        {
            var result = (grid.DataSource as DataTable).Select(fieldName + " = '" + lotId + "'");

            if (result.Count() > 0)
            {
                // 이미 등록된 Lot Id 입니다. Lot Id = {0}
                MSGBox.Show(MessageBoxType.Warning, "AlreadyInputLot", MessageBoxButtons.OK, lotId);
                grid.View.FocusedRowHandle = grid.View.LocateByValue(fieldName, lotId);
                grid.View.FocusedColumn = grid.View.Columns[fieldName];
                return false;
            }

            return true;
        }
        private void SetConsumableWorkCompleteDataSource(object dataSource)
        {
            _consumableQty = new Dictionary<string, decimal>();

            DataTable data = dataSource as DataTable;

            data.AsEnumerable().ForEach(row =>
            {
                string consumableLotId = Format.GetString(row["CONSUMABLELOTID"]);

                if (_consumableQty.Keys.Contains(consumableLotId))
                {
                    _consumableQty[consumableLotId] = _consumableQty[consumableLotId] + Format.GetDecimal(row["ORGINPUTQTY"]);
                }
                else
                {
                    _consumableQty.Add(consumableLotId, Format.GetDecimal(row["ORGINPUTQTY"]));
                }
            });

            grdConsumableComplete.DataSource = dataSource;
        }
        private string CheckExistsUsingDurableOnEquipment()
        {
            string result = "";

            DataTable equipmentList = pfsEquipment.GetEquipmentDataSource() as DataTable;
            DataTable durableList = grdDurableComplete.DataSource as DataTable;

            var list = equipmentList.AsEnumerable().Where(row => string.IsNullOrEmpty(Format.GetString(row["TRACKOUTTIME"])));

            list.ForEach(row =>
            {
                string equipmentId = Format.GetString(row["EQUIPMENTID"]);

                var listDurable = durableList.AsEnumerable().Where(r => string.IsNullOrEmpty(Format.GetString(r["WORKENDTIME"])) && Format.GetString(r["EQUIPMENTID"]) == equipmentId);


                if (listDurable.Count() < 1)
                    result += equipmentId + ",";
            });

            if (!string.IsNullOrWhiteSpace(result))
                result.TrimEnd(',');

            return result;
        }

        #endregion
    }
}
