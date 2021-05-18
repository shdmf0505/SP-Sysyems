#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons;

using System;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > Roll Lot 분할
    /// 업  무  설  명  : 대표 Roll Lot을 조회 한 후 분할 처리 한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-07-24
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RollSplitWorkStart : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private string _sequence = "";

        #endregion

        #region 생성자

        public RollSplitWorkStart()
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

            UseAutoWaitArea = false;

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeRollLotListGrid();
            InitializeConsumableListGrid();
            InitializeCreateListGrid();
            InitializeCreateLotListGrid();

            // 생성일 기간 초기화 (Today ± 3d)
            DateTime today = DateTime.Now;
            dtpCreateDateFr.EditValue = today;
            dtpCreateDateTo.EditValue = today;
            chkLotCardPrint.CheckState = CheckState.Checked;
            btnCreateLot.Visible = false;
        }

        /// <summary>        
        /// Roll Lot 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeRollLotListGrid()
        {
            grdRollLotList.GridButtonItem = GridButtonItem.None;
            grdRollLotList.ShowButtonBar = false;
            grdRollLotList.ShowStatusBar = false;

            grdRollLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdRollLotList.View.SetIsReadOnly();

            // 작업장ID
            grdRollLotList.View.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            // 창고ID
            grdRollLotList.View.AddTextBoxColumn("WAREHOUSEID", 80)
                .SetIsHidden();
            // 작업장
            grdRollLotList.View.AddTextBoxColumn("AREANAME", 200)
                .SetLabel("AREA");
            // 품목코드
            grdRollLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150);
            // 품목버전
            grdRollLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목명
            grdRollLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // Lot No.
            grdRollLotList.View.AddTextBoxColumn("LOTID", 200);


            // 공정ID
            grdRollLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정버전
            grdRollLotList.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 100)
                .SetIsHidden();
            // 작업공정
            grdRollLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)
                .SetLabel("WORKPROCESSSEGMENT");
            grdRollLotList.View.AddTextBoxColumn("INPUTDATE", 120);
            grdRollLotList.View.AddSpinEditColumn("INPUTPNLQTY", 90);
                                
            //grdRollLotList.View.AddSpinEditColumn("PANELQTY", 90)
            //    .SetLabel("PNL");
            // PNL
            grdRollLotList.View.AddSpinEditColumn("PANELQTY", 90)
                .SetLabel("PNL");
            // PCS
            grdRollLotList.View.AddSpinEditColumn("QTY", 90)
                .SetLabel("PCS");
            // MM
            grdRollLotList.View.AddSpinEditColumn("EXTENT", 100)
                .SetLabel("MM").SetDisplayFormat("#,##0.##");
            // 합수
            grdRollLotList.View.AddSpinEditColumn("PANELPERQTY", 90)
                .SetIsHidden();
            // 자재코드
            grdRollLotList.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();
            // 자재버전
            grdRollLotList.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 60)
                .SetIsHidden();
            // 자재명
            grdRollLotList.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetIsHidden();
            // 산출수
            grdRollLotList.View.AddSpinEditColumn("PCSMM", 60).SetDisplayFormat("###,##0.##")
                .SetIsHidden();
            // 자재명
            grdRollLotList.View.AddTextBoxColumn("MATERIALCLASS", 60)
                .SetIsHidden();

            grdRollLotList.View.AddTextBoxColumn("SALESORDERID", 150).SetTextAlignment(TextAlignment.Left);
            grdRollLotList.View.AddTextBoxColumn("LINENO", 50).SetTextAlignment(TextAlignment.Right);

            grdRollLotList.View.PopulateColumns();


            grdRollLotList.View.OptionsView.ShowIndicator = false;
        }

        /// <summary>
        /// 자재 Lot 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeConsumableListGrid()
        {
            grdConsumableList.GridButtonItem = GridButtonItem.None;
            grdConsumableList.ShowButtonBar = false;
            grdConsumableList.ShowStatusBar = false;

            grdConsumableList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            // Area Id
            grdConsumableList.View.AddTextBoxColumn("AREAID", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 자재코드
            grdConsumableList.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsReadOnly();
            // 자재버전
            grdConsumableList.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 100)
                .SetIsReadOnly()
                .SetIsHidden();
            // 자재명
            grdConsumableList.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetIsReadOnly();
            // 자재 LOT ID
            grdConsumableList.View.AddTextBoxColumn("CONSUMABLELOTID", 120)
                .SetIsReadOnly();
            // 수량(M)
            grdConsumableList.View.AddSpinEditColumn("QTY", 90)
                .SetIsReadOnly()
                .SetLabel("METERQTY")
                .SetDisplayFormat("#,##0.##");
            // 수량(PNL)
            grdConsumableList.View.AddSpinEditColumn("PNLQTY", 90)
                .SetIsReadOnly();
            // 사용수량(M)
            grdConsumableList.View.AddSpinEditColumn("USEQTY", 90)
                .SetLabel("USEMETERQTY").SetDisplayFormat("###,##0.##");
            // 사용수량(PNL)
            grdConsumableList.View.AddSpinEditColumn("USEPNLQTY", 90)
                .SetLabel("USEPANELQTY");
            // Pnl Size
            //grdConsumableList.View.AddSpinEditColumn("PNLSIZEYAXIS", 90)
            //    .SetIsReadOnly()
            //    .SetIsHidden();
            // Bom Qty
            grdConsumableList.View.AddSpinEditColumn("BOMQTY", 90)
                .SetIsReadOnly()
                .SetIsHidden();
            // Panel Per Qty
            grdConsumableList.View.AddSpinEditColumn("PANELPERQTY", 90)
                .SetIsReadOnly()
                .SetIsHidden();


            grdConsumableList.View.PopulateColumns();


            grdConsumableList.View.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            grdConsumableList.View.OptionsCustomization.AllowFilter = false;
            grdConsumableList.View.OptionsCustomization.AllowSort = false;
            grdConsumableList.View.OptionsView.ShowIndicator = false;
        }

        /// <summary>
        /// 품목별 사용 자재 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeCreateListGrid()
        {
            grdCreateList.GridButtonItem = GridButtonItem.None;
            grdCreateList.ShowButtonBar = false;
            grdCreateList.ShowStatusBar = false;

            grdCreateList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdCreateList.View.SetIsReadOnly();

            grdCreateList.View.AddTextBoxColumn("AREANAME", 100);
            // 품목코드
            grdCreateList.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            // 품목버전
            grdCreateList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 70);
            // 품목명
            grdCreateList.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            // 생성일
            grdCreateList.View.AddTextBoxColumn("CREATEDTIME", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 투입량(PNL)
            grdCreateList.View.AddSpinEditColumn("INPUTPNLQTY", 90);
            // 수량(PCS)
            grdCreateList.View.AddSpinEditColumn("PCSQTY", 70);
            // 사용자재ID
            grdCreateList.View.AddTextBoxColumn("CONSUMABLEDEFID", 150)
                .SetIsHidden();
            // 사용자재버전
            grdCreateList.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 100)
                .SetIsHidden();
            // 사용자재명
            grdCreateList.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200)
                .SetLabel("USECONSUMABLEDEF");
            // 사용수량(M)
            grdCreateList.View.AddSpinEditColumn("USEQTY", 80)
                .SetLabel("USEMETERQTY");
            // 공정ID
            grdCreateList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정
            grdCreateList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120)
                .SetLabel("PROCESSSEGMENT");
            // 담당자
            grdCreateList.View.AddTextBoxColumn("MANAGER", 90);


            grdCreateList.View.PopulateColumns();


            grdCreateList.View.OptionsView.ShowIndicator = false;
        }

        private void InitializeCreateLotListGrid()
        {
            grdCreateLotList.GridButtonItem = GridButtonItem.None;
            grdCreateLotList.ShowButtonBar = false;
            grdCreateLotList.ShowStatusBar = false;

            grdCreateLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdCreateLotList.View.SetIsReadOnly();

            // Lot No.
            grdCreateLotList.View.AddTextBoxColumn("LOTID", 170);
            // 투입수량(PNL)
            grdCreateLotList.View.AddSpinEditColumn("INPUTPNLQTY", 90);
            // 수량(PCS)
            grdCreateLotList.View.AddSpinEditColumn("PCSQTY", 70);
            // 사용수량(M)
            grdCreateLotList.View.AddSpinEditColumn("USEMETERQTY", 80);


            grdCreateLotList.View.PopulateColumns();


            grdCreateLotList.View.OptionsView.ShowIndicator = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            pnlContent.SizeChanged += PnlContent_SizeChanged;

            btnInit.Click += BtnInit_Click;

            grdRollLotList.View.FocusedRowChanged += RollLotListView_FocusedRowChanged;

            txtConsumableLotId.KeyDown += TxtConsumableLotId_KeyDown;
            txtConsumableLotId.PreviewKeyDown += TxtConsumableLotId_PreviewKeyDown;
            btnCreateLot.Click += BtnCreateLot_Click;
            grdConsumableList.View.RowCellStyle += ConsumableListView_RowCellStyle;
            grdConsumableList.View.CellValueChanged += ConsumableListView_CellValueChanged;

            btnQuery.Click += BtnQuery_Click;
            btnSelectConsumableLot.Click += BtnSelectConsumableLot_Click;

            grdCreateList.View.FocusedRowChanged += CreateListView_FocusedRowChanged;
        }

        private void TxtConsumableLotId_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (!string.IsNullOrEmpty(Format.GetString(txtConsumableLotId.EditValue)))
                {
                    KeyEventArgs arg = new KeyEventArgs(Keys.Enter);
                    TxtConsumableLotId_KeyDown(null, arg);
                }
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            GetCreatedLotList();
        }

        private void BtnSelectConsumableLot_Click(object sender, EventArgs e)
        {
            if (grdRollLotList.View.FocusedRowHandle < 0)
                return;

            string areaId = Format.GetString(grdRollLotList.View.GetFocusedRowCellValue("AREAID"));
            string consumableDefId = txtConsumableDefId.Text;
            string consumableDefVersion = txtConsumableDefVersion.Text;


            RollLotConsumableLotListPopup popup = new RollLotConsumableLotListPopup(areaId, consumableDefId, consumableDefVersion);
            popup.ShowDialog();
        }

        private void CreateListView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GetCreatedLotListByProduct();
        }

        /// <summary>
        /// Size가 변경 되면 특이사항 입력 컨트롤의 Label Size를 조정한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlContent_SizeChanged(object sender, EventArgs e)
        {
            tlpComment.ColumnStyles[0].Width = lblConsumableLotId.Width;
        }

        /// <summary>
        /// 초기화 버튼을 클릭하면 자재 Lot 정보를 초기화 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInit_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        /// <summary>
        /// Roll Lot 리스트의 포커스된 행이 변경되면 호출 한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollLotListView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged(e.FocusedRowHandle);

            //string lotId = grdRollLotList.View.GetRowCellValue(e.FocusedRowHandle, "LOTID").ToString();

            //txtLotId.Text = lotId;
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == Keys.Tab && txtConsumableLotId.IsEditorActive)
        //    {
        //        if (!string.IsNullOrEmpty(Format.GetString(txtConsumableLotId.EditValue)))
        //        {
        //            KeyEventArgs arg = new KeyEventArgs(Keys.Enter);
        //            TxtConsumableLotId_KeyDown(null, arg);
        //        }

        //    }
        //    return false;
        //}
        /// <summary>
        /// 자재 Lot에 Lot Id를 입력하면 유효한 자재 Lot인지 체크하고 유효한 경우 그리드에 정보를 보여준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtConsumableLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (grdRollLotList.View.FocusedRowHandle < 0)
                {
                    // 선택한 대표 Roll Lot이 존재하지 않습니다.
                    ShowMessage("NotExistsRollLot");
                    txtConsumableLotId.Text = "";
                    grdConsumableList.View.ClearDatas();
                    return;
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LOTID", grdRollLotList.View.GetFocusedRowCellValue("LOTID").ToString());
                param.Add("CONSUMABLELOTID", txtConsumableLotId.Text);
                param.Add("MATERIALTYPE", "Consumable");
                param.Add("SEQUENCE", _sequence);
                param.Add("CONSUMABLECLASSID", "RawMaterial");

                DataTable consumableLot = new DataTable();

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_YoungPoong)
                    consumableLot = SqlExecuter.Query("SelectConsumableLotInfo_YP", "10001", param);
                else
                    consumableLot = SqlExecuter.Query("SelectConsumableLotInfo", "10001", param);

                if (consumableLot.Rows.Count < 1)
                {
                    // 유효하지 않은 자재 Lot 입니다. 자재 Lot Id : {0}
                    ShowMessage("InvalidMaterialLot", txtConsumableLotId.Text);
                    txtConsumableLotId.Text = "";
                    grdConsumableList.View.ClearDatas();
                    return;
                }

                DataRow consumableRow = consumableLot.Rows.Cast<DataRow>().FirstOrDefault();

                string currentAreaId = Format.GetString(grdRollLotList.View.GetFocusedRowCellValue("WAREHOUSEID"));
                string consumableAreaId = Format.GetString(consumableRow["WAREHOUSEID"]);

                if (currentAreaId != consumableAreaId)
                {
                    // 현재 창고에서 보유하고 있는 자재가 아닙니다. 현재 창고 ID : {0}, 소유 창고 ID : {1}
                    ShowMessage("ConsumableNotInArea", currentAreaId, consumableAreaId);
                    return;
                }


                grdConsumableList.DataSource = consumableLot;
                BtnCreateLot_Click(btnCreateLot, new EventArgs());
            }
        }

        /// <summary>
        /// Lot Id 생성 버튼을 클릭하면 Split Lot Id를 생성한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateLot_Click(object sender, EventArgs e)
        {
            if (grdRollLotList.View.FocusedRowHandle < 0)
                return;

            if(grdConsumableList.View.RowCount == 0)
            {
                throw MessageException.Create("InputUsingMaterialLot");
            }
            string lotId = grdRollLotList.View.GetFocusedRowCellValue("LOTID").ToString();
            string lotIdStarted = lotId.Substring(0, lotId.LastIndexOf('-'));

            lotIdStarted = lotIdStarted.Substring(0, lotIdStarted.LastIndexOf('-'));

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotIdStarted);

            DataTable dtResult = SqlExecuter.Query("GetLotIdMaxSequence", "10001", param);

            if (dtResult.Rows.Count < 1)
            {
                ShowMessage("");
                return;
            }

            int lotSplitNo = Format.GetInteger(dtResult.Rows.Cast<DataRow>().FirstOrDefault()["LOTDEGREE"]) + 1;

            string newLotId = string.Join("-", lotIdStarted, lotSplitNo.ToString("000"), "001");


            txtLotId.Text = newLotId;
        }

        /// <summary>
        /// 자재 Lot 정보 그리드의 Cell 별 색상을 지정해준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsumableListView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "USEQTY")
                e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
            else if (e.Column.FieldName == "USEPNLQTY")
                e.Appearance.BackColor = Color.FromArgb(204, 255, 153);
            else
                e.Appearance.BackColor = Color.FromArgb(242, 242, 242);

            e.Appearance.ForeColor = Color.Black;
        }

        /// <summary>
        /// 자재 Lot 그리드의 사용수량(M)을 변경하면 Panel 사용수량을 변경해준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsumableListView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "USEQTY")
            {
                grdConsumableList.View.CellValueChanged -= ConsumableListView_CellValueChanged;

                //decimal pnlSizeYAxis = Format.GetDecimal(grdConsumableList.View.GetRowCellValue(e.RowHandle, "PNLSIZEYAXIS"));

                //if (pnlSizeYAxis == 0)
                //{
                //    // Y축 Panel Size가 0 입니다.
                //    ShowMessage("YAxisSizeIsZero");
                //    return;
                //}

                decimal bomQty = Format.GetDecimal(grdConsumableList.View.GetFocusedRowCellValue("BOMQTY"));
                decimal panelPerQty = Format.GetDecimal(grdConsumableList.View.GetFocusedRowCellValue("PANELPERQTY"));
                decimal pcsmm = Format.GetDecimal(grdRollLotList.View.GetFocusedRowCellValue("PCSMM"));
                string  materialClass = Format.GetTrimString(grdRollLotList.View.GetFocusedRowCellValue("MATERIALCLASS"));

                if (bomQty <= 0)
                {
                    // BOM 수량이 0 입니다.
                    ShowMessage("BomQtyIsZero");
                    return;
                }

                if (panelPerQty <= 0)
                {
                    // Lot의 합수가 0 입니다.
                    ShowMessage("PanelPerQtyIsZero");
                    return;
                }
                decimal usePnlQty = 0;
                if (materialClass.Equals("CL") && UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
                {
                    usePnlQty = Math.Ceiling((pcsmm * Format.GetDecimal(e.Value) / panelPerQty));
                }
                else
                {
                    usePnlQty = (int)(Math.Floor(Format.GetDecimal(e.Value) / bomQty / panelPerQty));
                }
                grdConsumableList.View.SetRowCellValue(e.RowHandle, "USEPNLQTY", usePnlQty);

                grdConsumableList.View.CellValueChanged += ConsumableListView_CellValueChanged;
            }
            else if (e.Column.FieldName == "USEPNLQTY")
            {
                grdConsumableList.View.CellValueChanged -= ConsumableListView_CellValueChanged;

                //decimal pnlSizeYAxis = Format.GetDecimal(grdConsumableList.View.GetRowCellValue(e.RowHandle, "PNLSIZEYAXIS"));

                //if (pnlSizeYAxis == 0)
                //{
                //    // Y축 Panel Size가 0 입니다.
                //    ShowMessage("YAxisSizeIsZero");
                //    return;
                //}

                decimal bomQty = Format.GetDecimal(grdConsumableList.View.GetFocusedRowCellValue("BOMQTY"));
                decimal panelPerQty = Format.GetDecimal(grdConsumableList.View.GetFocusedRowCellValue("PANELPERQTY"));
                decimal pcsmm = Format.GetDecimal(grdRollLotList.View.GetFocusedRowCellValue("PCSMM"));
                string materialClass = Format.GetTrimString(grdRollLotList.View.GetFocusedRowCellValue("MATERIALCLASS"));

                if (bomQty <= 0)
                {
                    // BOM 수량이 0 입니다.
                    ShowMessage("BomQtyIsZero");
                    return;
                }

                if (panelPerQty <= 0)
                {
                    // Lot의 합수가 0 입니다.
                    ShowMessage("PanelPerQtyIsZero");
                    return;
                }

                decimal useQty = 0;

                if (materialClass.Equals("CL") && UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
                {
                    useQty = Math.Round((Format.GetDecimal(e.Value) * panelPerQty / pcsmm), 2);
                }
                else
                {
                    if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
                    {
                        useQty = Math.Round((Format.GetDecimal(e.Value) * bomQty * panelPerQty),2);
                    }
                    else
                    {
                        useQty = (int)(Math.Ceiling(Format.GetDecimal(e.Value) * bomQty * panelPerQty));
                    }
                }
                grdConsumableList.View.SetRowCellValue(e.RowHandle, "USEQTY", useQty);

                grdConsumableList.View.CellValueChanged += ConsumableListView_CellValueChanged;
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

            // 재공실사 진행 여부 체크
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable isWipSurveyResult = SqlExecuter.Query("GetPlantIsWipSurvey", "10001", param);

            if (isWipSurveyResult.Rows.Count > 0)
            {
                DataRow row = isWipSurveyResult.AsEnumerable().FirstOrDefault();

                string isWipSurvey = Format.GetString(row["ISWIPSURVEY"]);

                if (isWipSurvey == "Y")
                {
                    // 재공실사가 진행 중 입니다. {0}을 진행할 수 없습니다.
                    ShowMessage("PLANTINWIPSURVEY", Language.Get(string.Join("_", "MENU", MenuId)));

                    return;
                }
            }

            // TODO : 저장 Rule 변경
            string lotId = grdRollLotList.View.GetFocusedRowCellValue("LOTID").ToString();
            string childLotId = txtLotId.Text;
            string commnet = txtComment.Text;
            string processSegmentId = grdRollLotList.View.GetFocusedRowCellValue("PROCESSSEGMENTID").ToString();
            int splitQty = Format.GetInteger(grdConsumableList.View.GetFocusedRowCellValue("USEPNLQTY")) * Format.GetInteger(grdRollLotList.View.GetFocusedRowCellValue("PANELPERQTY"));
            int splitPnlQty = Format.GetInteger(grdConsumableList.View.GetFocusedRowCellValue("USEPNLQTY"));

            DataTable dtConsumableList = grdConsumableList.DataSource as DataTable;

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker worker = new MessageWorker("SaveRollLotSplit");
                worker.SetBody(new MessageBody()
                {
                    { "EnterpriseId", UserInfo.Current.Enterprise },
                    { "PlantId", UserInfo.Current.Plant },
                    { "LotId", lotId },
                    { "ChildLotId", childLotId },
                    { "SplitQty", splitQty },
                    { "SplitPnlQty", splitPnlQty },
                    { "Comment", commnet },
                    { "ProcessSegmentId", processSegmentId },
                    { "ConsumableList", dtConsumableList }
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

            if (chkLotCardPrint.Checked)
            {
                // Lot Card 출력 함수 호출
                //Assembly assembly = Assembly.GetAssembly(this.GetType());
                //Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.LotCardProduction.repx");

                //CommonFunction.PrintLotCard_Ver2(childLotId, stream);
                CommonFunction.PrintLotCard_Ver2(childLotId, LotCardType.Normal);
            }

            ClearData();

            BtnQuery_Click(null, null);
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
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("PLANTID", UserInfo.Current.Plant);
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtRollLotList = await QueryAsync("SelectRollLotList", "10001", values);

            if (dtRollLotList.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                FocusedRowChanged(-1);
            }

            int beforeRowHandle = grdRollLotList.View.FocusedRowHandle;

            grdRollLotList.DataSource = dtRollLotList;

            if (dtRollLotList.Rows.Count > 0 && beforeRowHandle == 0 && grdRollLotList.View.FocusedRowHandle == 0)
                FocusedRowChanged(0);

            //GetCreatedLotList();
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // 작업장
            var conditionArea = Conditions.AddSelectPopup("P_AREAID", new SqlQuery("GetAreaListByAuthority", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}",$"LANGUAGETYPE={UserInfo.Current.LanguageType}", "AREATYPE=Area"), "AREANAME", "AREAID")
                .SetLabel("AREA")
                .SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupResultCount(1)
                .SetPopupAutoFillColumns("AREANAME")
                .SetPosition(1);
            

            // 팝업 조회조건
            conditionArea.Conditions.AddTextBox("AREA");
            conditionArea.IsRequired = true;
            // 팝업 그리드 컬럼
            conditionArea.GridColumns.AddTextBoxColumn("AREAID", 150);
            conditionArea.GridColumns.AddTextBoxColumn("AREANAME", 200);

            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 2, false, Conditions);
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
            if (string.IsNullOrWhiteSpace(txtConsumableLotId.Text))
            {
                // 사용할 자재 LOT을 입력하시기 바랍니다.
                throw MessageException.Create("InputUsingMaterialLot");
            }

            if (string.IsNullOrWhiteSpace(txtLotId.Text))
            {
                // Lot No.가 생성되지 않았습니다. {0} 버튼을 클릭하여 Lot No.를 생성하시기 바랍니다.
                throw MessageException.Create("NotCreatedLotId", Language.Get("CREATELOT"));
            }

            int parentPnlQty = Format.GetInteger(grdRollLotList.View.GetFocusedRowCellValue("PANELQTY"));
            int childPnlQty = Format.GetInteger(grdConsumableList.View.GetFocusedRowCellValue("USEPNLQTY"));

            if(childPnlQty == 0)
            {
                throw MessageException.Create("InputConsumableUseQty");
            }
            if (parentPnlQty < childPnlQty)
            {
                // 자재 Panel 사용 수량은 모 Lot의 Panel 수량보다 클 수 없습니다. 자재 Panel 사용 수량 : {0}, 모 Lot Panel 수량 : {1}
                throw MessageException.Create("UsingQtyLessThanParentQty", childPnlQty.ToString("#,##0"), parentPnlQty.ToString("#,##0"));
            }

            int consumableLotQty = Format.GetInteger(grdConsumableList.View.GetFocusedRowCellValue("PNLQTY"));
            int consumableUseQty = Format.GetInteger(grdConsumableList.View.GetFocusedRowCellValue("USEPNLQTY"));

            if (consumableLotQty < consumableUseQty)
            {
                // 자재 Panel 사용 수량은 자재 Lot Panel 수량 보다 클 수 없습니다. 자재 Panel 사용 수량 : {0}, 자재 Lot Panel 수량 : {1}
                throw MessageException.Create("UsingQtyLessThanLotQty", consumableUseQty.ToString("#,##0"), consumableLotQty.ToString("#,##0"));
            }

            decimal consumableLotQtyM = Format.GetDecimal(grdConsumableList.View.GetFocusedRowCellValue("QTY"));
            decimal consumableUseQtyM = Format.GetDecimal(grdConsumableList.View.GetFocusedRowCellValue("USEQTY"));

            if (consumableLotQtyM < consumableUseQtyM)
            {
                // 자재 미터 사용 수량은 자재 Lot 미터 수량 보다 클 수 없습니다. 자재 미터 사용 수량 : {0}, 자재 Lot M 수량 : {1}
                throw MessageException.Create("UsingMeterQtyLessThanLotMeterQty", consumableLotQtyM.ToString("#,##0.#####"), consumableUseQtyM.ToString("#,##0.#####"));
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private void ClearData()
        {
            txtConsumableLotId.Text = "";
            txtLotId.Text = "";
            txtComment.Text = "";
            chkLotCardPrint.CheckState = CheckState.Unchecked;

            grdConsumableList.View.ClearDatas();
        }

        /// <summary>
        /// Roll Lot 리스트 그리드의 포커스 된 행이 변경되면 자재 Lot 정보를 초기화 한다.
        /// </summary>
        private void FocusedRowChanged(int focusedRowHandle)
        {
            ClearData();

            if (focusedRowHandle > -1)
            {
                DataRow dr = grdRollLotList.View.GetDataRow(focusedRowHandle);

                txtProductDefId.Text = Format.GetString(dr["PRODUCTDEFID"]);
                txtProductDefName.Text = Format.GetString(dr["PRODUCTDEFNAME"]);
                txtParentLotid.Text = Format.GetString(dr["LOTID"]);
                txtConsumableDefId.Text = Format.GetString(dr["CONSUMABLEDEFID"]);
                txtConsumableDefVersion.Text = Format.GetString(dr["CONSUMABLEDEFVERSION"]);
                txtConsumableDefName.Text = Format.GetString(dr["CONSUMABLEDEFNAME"]);

                chkLotCardPrint.CheckState = CheckState.Checked;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LOTID", Format.GetString(dr["LOTID"]));

                DataTable minSequence = SqlExecuter.Query("GetMinSequenceByBillOfMaterial", "10001", param);

                if (minSequence.Rows.Count > 0)
                {
                    _sequence = Format.GetString(minSequence.Rows.Cast<DataRow>().FirstOrDefault()["SEQUENCE"]);
                }
            }
            else
            {
                txtProductDefId.Text = string.Empty;
                txtProductDefName.Text = string.Empty;
                txtParentLotid.Text = string.Empty;
                txtConsumableDefId.Text = string.Empty;
                txtConsumableDefVersion.Text = string.Empty;
                txtConsumableDefName.Text = string.Empty;

                chkLotCardPrint.CheckState = CheckState.Unchecked;

                _sequence = "";
            }
        }

        /// <summary>
        /// 품목별 사용 자재 리스트를 조회 한다.
        /// </summary>
        private void GetCreatedLotList()
        {
            int beforeHandle = grdCreateList.View.FocusedRowHandle;

            string areaID = Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID").GetValue().ToString();

            DateTime dtCreateDateFr = dtpCreateDateFr.DateTime;
            DateTime dtCreateDateTo = dtpCreateDateTo.DateTime;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MATERIALTYPE", "Consumable");
            param.Add("CONSUMABLECLASSID", "RawMaterial");
            param.Add("CREATEDATEFR", dtCreateDateFr.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            param.Add("CREATEDATETO", dtCreateDateTo.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("AREAID", areaID);
            DataTable result = SqlExecuter.Query("SelectCreatedListByRollLotSplit", "10001", param);

            grdCreateList.DataSource = result;

            if (beforeHandle >= result.Rows.Count)
                grdCreateList.View.FocusedRowHandle = 0;

            if (beforeHandle == grdCreateList.View.FocusedRowHandle)
                GetCreatedLotListByProduct();
        }

        private void GetCreatedLotListByProduct()
        {
            if (grdCreateList.View.FocusedRowHandle < 0)
            {
                grdCreateLotList.View.ClearDatas();

                return;
            }

            DataRow row = grdCreateList.View.GetFocusedDataRow();

            string createDate = Format.GetString(row["CREATEDTIME"]);
            string productDefId = Format.GetString(row["PRODUCTDEFID"]);
            string productDefVersion = Format.GetString(row["PRODUCTDEFVERSION"]);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CREATEDTIMEFR", createDate + " 00:00:00");
            param.Add("CREATEDTIMETO", createDate + " 23:59:59");
            param.Add("MATERIALTYPE", "Consumable");
            param.Add("LOTCREATEDTYPE", "SplitRoll");
            param.Add("PRODUCTDEFID", productDefId);
            param.Add("PRODUCTDEFVERSION", productDefVersion);

            DataTable lotList = SqlExecuter.Query("SelectCreatedLotListByRollLotSplit", "10001", param);

            grdCreateLotList.DataSource = lotList;
        }

        #endregion
    }
}
