#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

using DevExpress.XtraGrid.Views.Grid;
using Micube.SmartMES.Commons.Controls;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정관리 > 설비 변경/추가 등록
    /// 업  무  설  명  : LOT의 현재 작업중인 설비를 변경/추가 등록한다
    /// 생    성    자  : 황유성
    /// 생    성    일  : 
    /// 수  정  이  력  : 
    /// 
    /// </summary>
    public partial class ChangeLotEquipment : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private string lotId;   // 현재 LOT ID
        private EquipmentGrid lastFocusedGrid = EquipmentGrid.None;
        private enum EquipmentGrid { Run, Change, None }
        private readonly string ENTERPRISE_YOUNGPOONG = "YOUNGPOONG";
        #endregion

        #region ◆ 생성자 |
        public ChangeLotEquipment()
        {
            InitializeComponent();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

        #region ▶ Control 설정 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();
            InitializeControls();
            InitializeGrid();
            InitializeEvent();
        }

        #region - 기본 Control 설정 |
        /// <summary>
        /// 기본 Control 설정
        /// </summary>
        private void InitializeControls()
        {
            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "PRODUCTDEFVERSION", "PRODUCTTYPE", "DEFECTUNIT"
                , "PANELPERQTY", "PCSPNL", "PROCESSSEGMENTTYPE", "STEPTYPE", "DURABLEDEFID", "PROCESSSTATE");
            grdEquipmentsRun.Enabled = false;
            grdDurableRun.Enabled = false;
            grdEquipmentsRun.GridButtonItem = GridButtonItem.None;
            grdDurableRun.GridButtonItem = GridButtonItem.None;
        }
        #endregion

        #endregion

        #region ▶ 그리드 설정 |
        /// <summary>
        /// 그리드 설정
        /// </summary>
        private void InitializeGrid()
        {
            #region - 작업설비 Grid |
            grdEquipmentsRun.GridButtonItem = GridButtonItem.None;
            grdEquipmentsRun.View.ClearColumns();

            grdEquipmentsRun.View.AddCheckBoxColumn("ISCOMPLETED", 60);    // 완료여부
            // 설비ID
            grdEquipmentsRun.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdEquipmentsRun.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly();
            // 작업시작시간
            grdEquipmentsRun.View.AddTextBoxColumn("TRACKINTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // 작업완료시간
            grdEquipmentsRun.View.AddTextBoxColumn("TRACKOUTTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // LOT ID
            grdEquipmentsRun.View.AddTextBoxColumn("LOTID").SetIsHidden();
            grdEquipmentsRun.View.AddTextBoxColumn("TXNHISTKEY").SetIsHidden();
            // 공정 ID
            grdEquipmentsRun.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            // 공정 버전
            grdEquipmentsRun.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            // 작업차수
            grdEquipmentsRun.View.AddTextBoxColumn("WORKCOUNT").SetIsHidden();
            // 설비 제어모드 (OnLine/OffLine)
            grdEquipmentsRun.View.AddTextBoxColumn("CONTROLMODE").SetIsHidden();

            grdEquipmentsRun.View.PopulateColumns();
            #endregion

            #region - 변경대상 설비 |
            grdEquipmentsChange.GridButtonItem = GridButtonItem.None;
            grdEquipmentsChange.View.ClearColumns();

            grdEquipmentsChange.View.AddCheckBoxColumn("CHECK", 60).SetLabel("SELECT");    // 설비지정
            // 설비ID
            grdEquipmentsChange.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdEquipmentsChange.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly();
            // 작업시작시간
            grdEquipmentsChange.View.AddTextBoxColumn("TRACKINTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // LOT ID
            grdEquipmentsChange.View.AddTextBoxColumn("LOTID").SetIsHidden();
            // 공정 ID
            grdEquipmentsChange.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            // 공정 버전
            grdEquipmentsChange.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            // 작업차수
            grdEquipmentsChange.View.AddTextBoxColumn("WORKCOUNT").SetIsHidden();
            // 설비 제어모드 (OnLine/OffLine)
            grdEquipmentsChange.View.AddTextBoxColumn("CONTROLMODE").SetIsHidden();

            grdEquipmentsChange.View.PopulateColumns();
            #endregion

            #region - 장착된 치공구 현황 |
            grdDurableRun.GridButtonItem = GridButtonItem.None;
            grdDurableRun.View.ClearColumns();

            grdDurableRun.View.AddCheckBoxColumn("ISRUNNING", 60).SetIsReadOnly();        // 작업여부
            grdDurableRun.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdDurableRun.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly();
            // 치공구 LOT ID
            grdDurableRun.View.AddTextBoxColumn("DURABLELOTID", 160).SetIsReadOnly();
            // 치공구 정의명
            grdDurableRun.View.AddTextBoxColumn("DURABLEDEFNAME", 320).SetIsReadOnly();
            // 작업시작시간
            grdDurableRun.View.AddTextBoxColumn("WORKSTARTTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // 작업완료시간
            grdDurableRun.View.AddTextBoxColumn("WORKENDTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // 사용횟수
            grdDurableRun.View.AddTextBoxColumn("USEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 누적 사용횟수
            grdDurableRun.View.AddTextBoxColumn("TOTALUSEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 보장사용횟수
            grdDurableRun.View.AddTextBoxColumn("USEDLIMIT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 치공구 정의 ID
            grdDurableRun.View.AddTextBoxColumn("DURABLEDEFID").SetIsHidden();
            // 치공구 정의 버전
            grdDurableRun.View.AddTextBoxColumn("DURABLEDEFVERSION").SetIsHidden();
            // 작업완료여부
            grdDurableRun.View.AddTextBoxColumn("ISCOMPLETED").SetIsHidden();
            // LOT ID
            grdDurableRun.View.AddTextBoxColumn("LOTID").SetIsHidden();
            grdDurableRun.View.AddTextBoxColumn("TXNHISTKEY").SetIsHidden();
            // 공정 ID
            grdDurableRun.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            // 공정 버전
            grdDurableRun.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            // 작업차수
            grdDurableRun.View.AddTextBoxColumn("WORKCOUNT").SetIsHidden();

            grdDurableRun.View.PopulateColumns();
            #endregion

            #region - 변경설비에 사용할 치공구 |
            grdDurableChange.GridButtonItem = GridButtonItem.None;
            grdDurableChange.View.ClearColumns();

            grdDurableChange.View.AddCheckBoxColumn("CHECK", 60).SetLabel("SELECT");        // 선택여부
            grdDurableChange.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdDurableChange.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly();
            // 치공구 LOT ID
            grdDurableChange.View.AddTextBoxColumn("DURABLELOTID", 160).SetIsReadOnly();
            // 치공구 정의명
            grdDurableChange.View.AddTextBoxColumn("DURABLEDEFNAME", 320).SetIsReadOnly();
            // 작업시작시간
            grdDurableChange.View.AddTextBoxColumn("WORKSTARTTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // 사용횟수
            grdDurableChange.View.AddTextBoxColumn("USEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 누적 사용횟수
            grdDurableChange.View.AddTextBoxColumn("TOTALUSEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 보장사용횟수
            grdDurableChange.View.AddTextBoxColumn("USEDLIMIT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 치공구 정의 ID
            grdDurableChange.View.AddTextBoxColumn("DURABLEDEFID").SetIsHidden();
            // 치공구 정의 버전
            grdDurableChange.View.AddTextBoxColumn("DURABLEDEFVERSION").SetIsHidden();
            // LOT ID
            grdDurableChange.View.AddTextBoxColumn("LOTID").SetIsHidden();
            // 공정 ID
            grdDurableChange.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            // 공정 버전
            grdDurableChange.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            // 작업차수
            grdDurableChange.View.AddTextBoxColumn("WORKCOUNT").SetIsHidden();

            grdDurableChange.View.PopulateColumns();
            #endregion

            #region - 치공구 변경 :: 작업설비 Grid |
            grdWorkingEquipment.GridButtonItem = GridButtonItem.None;
            grdWorkingEquipment.View.ClearColumns();

            // 설비ID
            grdWorkingEquipment.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdWorkingEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly();
            // 작업시작시간
            grdWorkingEquipment.View.AddTextBoxColumn("TRACKINTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // LOT ID
            grdWorkingEquipment.View.AddTextBoxColumn("LOTID").SetIsHidden();
            grdWorkingEquipment.View.AddTextBoxColumn("TXNHISTKEY").SetIsHidden();
            // 공정 ID
            grdWorkingEquipment.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            // 공정 버전
            grdWorkingEquipment.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            // 작업차수
            grdWorkingEquipment.View.AddTextBoxColumn("WORKCOUNT").SetIsHidden();
            // 설비 제어모드 (OnLine/OffLine)
            grdWorkingEquipment.View.AddTextBoxColumn("CONTROLMODE").SetIsHidden();

            grdWorkingEquipment.View.PopulateColumns();
            #endregion

            #region - 장착된 치공구 현황 2 |
            grdDurableRun2.GridButtonItem = GridButtonItem.None;
            grdDurableRun2.View.ClearColumns();

            grdDurableRun2.View.AddCheckBoxColumn("ISCOMPLETED", 60);        // 완료여부
            grdDurableRun2.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdDurableRun2.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly();
            // 치공구 LOT ID
            grdDurableRun2.View.AddTextBoxColumn("DURABLELOTID", 160).SetIsReadOnly();
            // 치공구 정의명
            grdDurableRun2.View.AddTextBoxColumn("DURABLEDEFNAME", 320).SetIsReadOnly();
            // 작업시작시간
            grdDurableRun2.View.AddTextBoxColumn("WORKSTARTTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // 작업완료시간
            grdDurableRun2.View.AddTextBoxColumn("WORKENDTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // 사용횟수
            grdDurableRun2.View.AddTextBoxColumn("USEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 누적 사용횟수
            grdDurableRun2.View.AddTextBoxColumn("TOTALUSEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 보장사용횟수
            grdDurableRun2.View.AddTextBoxColumn("USEDLIMIT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 치공구 정의 ID
            grdDurableRun2.View.AddTextBoxColumn("DURABLEDEFID").SetIsHidden();
            // 치공구 정의 버전
            grdDurableRun2.View.AddTextBoxColumn("DURABLEDEFVERSION").SetIsHidden();
            // LOT ID
            grdDurableRun2.View.AddTextBoxColumn("LOTID").SetIsHidden();
            grdDurableRun2.View.AddTextBoxColumn("TXNHISTKEY").SetIsHidden();
            // 공정 ID
            grdDurableRun2.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            // 공정 버전
            grdDurableRun2.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            // 작업차수
            grdDurableRun2.View.AddTextBoxColumn("WORKCOUNT").SetIsHidden();

            grdDurableRun2.View.PopulateColumns();
            #endregion

            #region - 변경 치공구 2 |
            grdDurableChange2.GridButtonItem = GridButtonItem.None;
            grdDurableChange2.View.ClearColumns();

            grdDurableChange2.View.AddCheckBoxColumn("CHECK", 60).SetLabel("SELECT");        // 선택여부
            grdDurableChange2.View.AddTextBoxColumn("EQUIPMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdDurableChange2.View.AddTextBoxColumn("EQUIPMENTNAME", 210).SetIsReadOnly();
            // 치공구 LOT ID
            grdDurableChange2.View.AddTextBoxColumn("DURABLELOTID", 160).SetIsReadOnly();
            // 치공구 정의명
            grdDurableChange2.View.AddTextBoxColumn("DURABLEDEFNAME", 320).SetIsReadOnly();
            // 작업시작시간
            grdDurableChange2.View.AddTextBoxColumn("WORKSTARTTIME", 140).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetIsReadOnly();
            // 사용횟수
            grdDurableChange2.View.AddTextBoxColumn("USEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 누적 사용횟수
            grdDurableChange2.View.AddTextBoxColumn("TOTALUSEDCOUNT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 보장사용횟수
            grdDurableChange2.View.AddTextBoxColumn("USEDLIMIT", 100).SetDisplayFormat("#,##0").SetIsReadOnly();
            // 치공구 정의 ID
            grdDurableChange2.View.AddTextBoxColumn("DURABLEDEFID").SetIsHidden();
            // 치공구 정의 버전
            grdDurableChange2.View.AddTextBoxColumn("DURABLEDEFVERSION").SetIsHidden();
            // LOT ID
            grdDurableChange2.View.AddTextBoxColumn("LOTID").SetIsHidden();
            // 공정 ID
            grdDurableChange2.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
            // 공정 버전
            grdDurableChange2.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
            // 작업차수
            grdDurableChange2.View.AddTextBoxColumn("WORKCOUNT").SetIsHidden();

            grdDurableChange2.View.PopulateColumns();
            #endregion
        }
        #endregion

        #endregion

        #region ◆ Event |
        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            // TextBox
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;

            // Button
            btnInit.Click += BtnInit_Click;

            // Grid
            grdEquipmentsRun.View.CellValueChanged += grdEquipmentsRunView_CellValueChanged;
            grdEquipmentsRun.View.GotFocus += grdEquipmentsRunView_GotFocus;
            grdEquipmentsChange.View.CellValueChanged += grdEquipmentChangeView_CellValueChanged;
            grdEquipmentsChange.View.GotFocus += grdEquipmentsChangeView_GotFocus;

            grdDurableRun.View.RowCellStyle += DurableView_RowCellStyle;
            grdDurableChange.View.CellValueChanged += grdDurableChangeView_CellValueChanged;

            grdWorkingEquipment.View.FocusedRowChanged += grdWorkingEquipmentView_FocusedRowChanged;

            grdDurableRun2.View.CellValueChanged += grdDurableRun2View_CellValueChanged;
            grdDurableChange2.View.CellValueChanged += grdDurableChange2View_CellValueChanged;

            // Tab Index
            tabControl.SelectedPageChanged += TabControl_SelectedPageChanged;
        }

        #region ▶ Grid Got Focus Event
        private void grdEquipmentsRunView_GotFocus(object sender, EventArgs e)
        {
            this.lastFocusedGrid = EquipmentGrid.Run;
        }

        private void grdEquipmentsChangeView_GotFocus(object sender, EventArgs e)
        {
            this.lastFocusedGrid = EquipmentGrid.Change;
        }
        #endregion

        #region ▶ Tab Index Changed Event
        /// <summary>
        /// Tab Index Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    SearchEquipments();
                    SearchDurables();
                    break;
                case 1:
                    SearchEquipments();
                    SearchDurables2();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ▶ Grid Event |

        #region - 가동설비 Cell Value Changed |
        /// <summary>
        /// 가동설비 Cell Value Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdEquipmentsRunView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdEquipmentsRun.View.GetFocusedDataRow();

            if (e.Column.FieldName == "ISCOMPLETED")
            {
                if ((bool)row["ISCOMPLETED"] == true)
                {
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    grdEquipmentsRun.View.SetRowCellValue(e.RowHandle, "TRACKOUTTIME", time);

                    DataTable dt = grdDurableRun.DataSource as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        if (!dr["EQUIPMENTID"].ToString().Equals(row["EQUIPMENTID"].ToString())) continue;

                        grdDurableRun.View.SetRowCellValue(i, "WORKENDTIME", time);
                        grdDurableRun.View.SetRowCellValue(i, "ISCOMPLETED", "Y");
                    }
                }
                else
                {
                    grdEquipmentsRun.View.SetRowCellValue(e.RowHandle, "TRACKOUTTIME", DBNull.Value);

                    DataTable dt = grdDurableRun.DataSource as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        if (!dr["EQUIPMENTID"].ToString().Equals(row["EQUIPMENTID"].ToString())) continue;

                        grdDurableRun.View.SetRowCellValue(i, "WORKENDTIME", DBNull.Value);
                        grdDurableRun.View.SetRowCellValue(i, "ISCOMPLETED", "N");
                    }
                }
            }
        }
        #endregion

        #region - 변경대상 설비 Cell Value Changed |
        /// <summary>
        /// 변경대상 설비 Cell Value Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdEquipmentChangeView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            grdEquipmentsChange.View.CellValueChanged -= grdEquipmentChangeView_CellValueChanged;

            DataRow row = grdEquipmentsChange.View.GetFocusedDataRow();

            if (e.Column.FieldName.Equals("CHECK"))
            {
                if ((bool)row["CHECK"] == true)
                {
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    grdEquipmentsChange.View.SetRowCellValue(e.RowHandle, "TRACKINTIME", time);
                }
                else
                {
                    grdEquipmentsChange.View.SetRowCellValue(e.RowHandle, "TRACKINTIME", "");
                }
            }

            grdEquipmentsChange.View.CellValueChanged += grdEquipmentChangeView_CellValueChanged;
        }
        #endregion

        #region - 치공구 사용횟수 유효성 검증 등 ||
        /// <summary>
        /// 치공구 사용횟수 유효성 검증 등
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDurableChangeView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow focusedRow = grdDurableChange.View.GetFocusedDataRow();

            if (e.Column.FieldName == "CHECK")
            {
                grdDurableChange.View.CellValueChanged -= grdDurableChangeView_CellValueChanged;

                try
                {
                    DataRow chEquipment = null;
                    if (lastFocusedGrid == EquipmentGrid.Run)
                    {
                        chEquipment = grdEquipmentsRun.View.GetFocusedDataRow();
                        if (chEquipment == null)
                        {
                            grdDurableChange.View.SetRowCellValue(e.RowHandle, "CHECK", "False");

                            // 설비는 필수로 선택해야 합니다. {0}
                            this.ShowMessage("EquipmentIsRequired");
                            return;
                        }
                    }
                    else if (lastFocusedGrid == EquipmentGrid.Change)
                    {
                        chEquipment = grdEquipmentsChange.View.GetFocusedDataRow();
                        if ((bool)chEquipment["CHECK"] == false)
                        {
                            grdDurableChange.View.SetRowCellValue(e.RowHandle, "CHECK", "False");

                            // 설비는 필수로 선택해야 합니다. {0}
                            this.ShowMessage("EquipmentIsRequired");
                            return;
                        }
                    }
                    else
                    {
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "CHECK", "False");

                        // 설비는 필수로 선택해야 합니다. {0}
                        this.ShowMessage("EquipmentIsRequired");
                        return;
                    }

                    // 선택된 치공구가 완료되었는지 체크
                    if (CheckDurableComplete(focusedRow["DURABLELOTID"].ToString()) == 0)
                    {
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "CHECK", "False");

                        // 사용 중인 치공구 입니다.
                        this.ShowMessage("DurableLotIsUsing");
                        return;
                    }

                    if (CheckEquipmentHasDurable(chEquipment["EQUIPMENTID"].ToString()))
                    {
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "CHECK", "False");

                        // 설비에서 이미 사용중인 치공구가 있습니다. {0}
                        this.ShowMessage("EquipmentHasDurableLot", "");
                        return;
                    }

                    if ((bool)focusedRow["CHECK"] == true)  // 신규 치공구 선택
                    {
                        decimal usedLimit = decimal.Parse(focusedRow["USEDLIMIT"].ToString());
                        decimal totalUsedCount = decimal.Parse(focusedRow["TOTALUSEDCOUNT"].ToString());

                        if (usedLimit <= totalUsedCount)    // 누적 사용횟수 초과
                        {
                            // 치공구의 현재 사용횟수가 한계 사용횟수를 초과하였습니다. 그래도 진행 하겠습니까?
                            if (MSGBox.Show(MessageBoxType.Question, "UsedCountLargerThanUsedLimit", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            {
                                grdDurableChange.View.SetRowCellValue(e.RowHandle, "CHECK", false);
                                grdDurableChange.View.SetRowCellValue(e.RowHandle, "WORKSTARTTIME", DBNull.Value);
                                grdDurableChange.View.SetRowCellValue(e.RowHandle, "EQUIPMENTID", DBNull.Value);
                                grdDurableChange.View.SetRowCellValue(e.RowHandle, "EQUIPMENTNAME", DBNull.Value);

                                grdDurableChange.View.CellValueChanged += grdDurableChangeView_CellValueChanged;

                                return;
                            }
                        }

                        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "WORKSTARTTIME", time);
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "EQUIPMENTID", chEquipment["EQUIPMENTID"].ToString());
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "EQUIPMENTNAME", chEquipment["EQUIPMENTNAME"].ToString());
                    }
                    else
                    {
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "WORKSTARTTIME", DBNull.Value);
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "EQUIPMENTID", DBNull.Value);
                        grdDurableChange.View.SetRowCellValue(e.RowHandle, "EQUIPMENTNAME", DBNull.Value);
                    }
                }
                finally
                {
                    grdDurableChange.View.CellValueChanged += grdDurableChangeView_CellValueChanged;
                }
            }
        }
        #endregion

        #region - 치공구 RowCell Style |
        /// <summary>
        /// 치공구 RowCell Style
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DurableView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            // 현재 사용중인 치공구 다른색상으로 표시
            if (grdDurableRun.View.GetRowCellValue(e.RowHandle, "ISCOMPLETED").ToString().Equals("Y"))
            {
                e.Appearance.ForeColor = Color.DarkGray;
            }
        }
        #endregion

        #region - 가동설비 선택 Event |
        /// <summary>
        /// 가동설비 선택 Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWorkingEquipmentView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchDurables2();
        }
        #endregion

        #region - 치공구 변경 :: 사용중 치공구 |
        /// <summary>
        /// 치공구 변경 :: 사용중 치공구 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDurableRun2View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdDurableRun2.View.GetFocusedDataRow();

            if (e.Column.FieldName == "ISCOMPLETED")
            {
                if ((bool)row["ISCOMPLETED"] == true)
                {
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    grdDurableRun2.View.SetRowCellValue(e.RowHandle, "WORKENDTIME", time);
                }
                else
                {
                    grdDurableRun2.View.SetRowCellValue(e.RowHandle, "WORKENDTIME", DBNull.Value);
                }
            }
        }
        #endregion

        #region - 치공구 변경 :: 변경대상 치공구 선택 |
        /// <summary>
        /// 치공구 변경 :: 변경대상 치공구 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDurableChange2View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdDurableChange2.View.CellValueChanged -= grdDurableChange2View_CellValueChanged;

            try
            {
                DataRow row = grdWorkingEquipment.View.GetFocusedDataRow();
                DataRow focusedRow = grdDurableChange2.View.GetFocusedDataRow();
                DataTable runDt = grdDurableRun2.DataSource as DataTable;

                if (row == null)
                {
                    // 설비는 필수로 선택해야 합니다. {0}
                    this.ShowMessage("EquipmentIsRequired");
                    return;
                }

                var chk = runDt.AsEnumerable().Where(r => (bool)r.Field<bool>("ISCOMPLETED") == false && r.Field<string>("DURABLELOTID") == focusedRow["DURABLELOTID"].ToString()).Count();

                // 선택된 치공구가 완료되었는지 체크
                if (chk > 0)
                {
                    grdDurableChange2.View.SetRowCellValue(e.RowHandle, "CHECK", false);

                    // 사용 중인 치공구 입니다.
                    this.ShowMessage("DurableLotIsUsing");

                    return;
                }

                if ((bool)focusedRow["CHECK"] == true)  // 신규 치공구 선택
                {
                    if (CheckEquipmentHasDurable2(row["EQUIPMENTID"].ToString()))
                    {
                        grdDurableChange2.View.SetRowCellValue(e.RowHandle, "CHECK", false);

                        // 설비에서 이미 사용중인 치공구가 있습니다. {0}
                        this.ShowMessage("EquipmentHasDurableLot", "");
                        return;
                    }

                    decimal usedLimit = decimal.Parse(focusedRow["USEDLIMIT"].ToString());
                    decimal totalUsedCount = decimal.Parse(focusedRow["TOTALUSEDCOUNT"].ToString());

                    if (usedLimit <= totalUsedCount)    // 누적 사용횟수 초과
                    {
                        // 치공구의 현재 사용횟수가 한계 사용횟수를 초과하였습니다. 그래도 진행 하겠습니까?
                        if (MSGBox.Show(MessageBoxType.Question, "UsedCountLargerThanUsedLimit", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            grdDurableChange2.View.SetRowCellValue(e.RowHandle, "CHECK", false);
                            grdDurableChange2.View.SetRowCellValue(e.RowHandle, "WORKSTARTTIME", DBNull.Value);
                            grdDurableChange2.View.SetRowCellValue(e.RowHandle, "EQUIPMENTID", DBNull.Value);
                            grdDurableChange2.View.SetRowCellValue(e.RowHandle, "EQUIPMENTNAME", DBNull.Value);

                            grdDurableChange2.View.CellValueChanged += grdDurableChange2View_CellValueChanged;

                            return;
                        }
                    }

                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    grdDurableChange2.View.SetRowCellValue(e.RowHandle, "WORKSTARTTIME", time);
                    grdDurableChange2.View.SetRowCellValue(e.RowHandle, "EQUIPMENTID", row["EQUIPMENTID"].ToString());
                    grdDurableChange2.View.SetRowCellValue(e.RowHandle, "EQUIPMENTNAME", row["EQUIPMENTNAME"].ToString());
                }
                else
                {
                    grdDurableChange2.View.SetRowCellValue(e.RowHandle, "WORKSTARTTIME", DBNull.Value);
                    grdDurableChange2.View.SetRowCellValue(e.RowHandle, "EQUIPMENTID", DBNull.Value);
                    grdDurableChange2.View.SetRowCellValue(e.RowHandle, "EQUIPMENTNAME", DBNull.Value);
                }
            }
            finally
            {
                grdDurableChange2.View.CellValueChanged += grdDurableChange2View_CellValueChanged;
            }
        }
        #endregion

        #endregion

        #region ▶ Button Event |
        /// <summary>
        /// 초기화 버튼 클릭 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInit_Click(object sender, EventArgs e)
        {
            Clear();
            txtLotId.Text = "";
        }
        #endregion

        #region ▶ TextBox Event
        /// <summary>
        /// LOT ID 입력 시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Clear();
                SearchLotInfo();
                InitializeGrid();

                TabControl_SelectedPageChanged(null, null);
            }
        }
        #endregion
        #endregion

        #region ◆ 검색 |

        #region ▶ LOT 정보 조회 |
        /// <summary>
        /// LOT 정보 조회
        /// </summary>
        private void SearchLotInfo()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", txtLotId.Text);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoBylotIDbyAreaAuthority", "10001", param);

            if (lotInfo.Rows.Count > 0)
            {
                if (lotInfo.Rows[0]["PROCESSSTATE"].ToString() != "Run")
                {
                    lotId = null;
                    grdEquipmentsRun.Enabled = false;
                    grdDurableRun.Enabled = false;
                    grdLotInfo.DataSource = lotInfo.Clone();
                    // LOT의 공정진행 상태가 Run이 아닙니다.
                    this.ShowMessage("LotProcessStateIsNotRun");
                    return;
                }
                lotId = lotInfo.Rows[0]["LOTID"].ToString();
                grdEquipmentsRun.Enabled = true;
                grdEquipmentsChange.Enabled = true;
                grdDurableRun.Enabled = true;
                grdDurableChange.Enabled = true;

                grdWorkingEquipment.Enabled = true;
                grdDurableRun2.Enabled = true;
                grdDurableChange2.Enabled = true;

                grdLotInfo.DataSource = lotInfo;
            }
            else
            {
                Clear();

                grdLotInfo.DataSource = lotInfo;
                // 해당 Lot이 존재하지 않습니다. {0}
                this.ShowMessage("NotExistLot", string.Format("LotId = {0}", txtLotId.Text));
            }
        }
        #endregion

        #region ▶ 설비정보를 조회한다 |
        /// <summary>
        /// 설비정보를 조회한다
        /// </summary>
        private void SearchEquipments()
        {
            // 가동중 설비
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", lotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable equipments = SqlExecuter.Query("SelectEquipmentInfoByLotID", "10002", param);

            grdEquipmentsRun.DataSource = equipments;
            grdWorkingEquipment.DataSource = equipments;

            // 변경 대상 설비
            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param2.Add("PLANTID", UserInfo.Current.Plant);
            param2.Add("LOTID", lotId);
            param2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable changeEquip = SqlExecuter.Query("SelectEquipmentInfoByLotID", "10003", param2);

            grdEquipmentsChange.DataSource = changeEquip;
        }
        #endregion

        #region ▶ 설비변경 치공구정보를 조회한다 |
        /// <summary>
        /// 치공구정보를 조회한다
        /// </summary>
        private void SearchDurables()
        {
            // 가동중 설비의 치공구 조회
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", lotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable durables = SqlExecuter.Query("SelectDurableInfoByLotID", "10001", param);

            grdDurableRun.DataSource = durables;

            // 변경설비에 사용할 치공구 조회
            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param2.Add("PLANTID", UserInfo.Current.Plant);
            param2.Add("LOTID", lotId);
            param2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable change = SqlExecuter.Query("SelectDurableInfoByLotID", "10002", param2);

            grdDurableChange.DataSource = change;
        }
        #endregion

        #region ▶ 치공구변경 :: 치공구정보를 조회한다 |
        /// <summary>
        /// 치공구정보를 조회한다
        /// </summary>
        private void SearchDurables2()
        {
            DataRow dr = grdWorkingEquipment.View.GetFocusedDataRow();

            if (dr == null) return;

            // 가동중 설비의 치공구 조회
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", lotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("EQUIPMENTID", dr["EQUIPMENTID"].ToString());

            DataTable durables = SqlExecuter.Query("SelectDurableInfoByLotID", "10003", param);

            grdDurableRun2.DataSource = durables;

            // 변경설비에 사용할 치공구 조회
            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param2.Add("PLANTID", UserInfo.Current.Plant);
            param2.Add("LOTID", lotId);
            param2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param2.Add("EQUIPMENTID", dr["EQUIPMENTID"].ToString());

            DataTable change = SqlExecuter.Query("SelectDurableInfoByLotID", "10004", param2);

            grdDurableChange2.DataSource = change;
        }
        #endregion

        #endregion

        #region ◆ 툴바 |
        /// <summary>
        /// 변경사항을 저장한다
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            if (tabControl.SelectedTabPageIndex == 0)
            {
                if (!grdEquipmentsRun.Enabled)
                {
                    return;
                }

                DataTable endedEquipments = grdEquipmentsRun.GetChangedRows();
                DataTable endedDurables = grdDurableRun.GetChangedRows();
                DataTable changedEquipments = grdEquipmentsChange.GetChangedRows();
                DataTable changedDurables = grdDurableChange.GetChangedRows();

                ValidateDurableRequired();

                MessageWorker worker = new MessageWorker("SaveChangeEquipment");
                worker.SetBody(new MessageBody()
                {
                    { "transactionType", "SetChangeEquipment" },
                    { "lotid", this.lotId },
                    { "EndedEquipments", endedEquipments },
                    { "EndedDurables", endedDurables },
                    { "ChangedEquipments", changedEquipments },
                    { "ChangedDurables", changedDurables }
                });

                worker.Execute();

                Clear();
                SearchLotInfo();
                InitializeGrid();

                TabControl_SelectedPageChanged(null, null);
            }
            else
            {
                if (!grdWorkingEquipment.Enabled)
                {
                    return;
                }
                if (grdWorkingEquipment.View.GetFocusedDataRow() == null)
                {
                    return;
                }

                DataRow equipments = grdWorkingEquipment.View.GetFocusedDataRow();
                DataTable endedDurables = grdDurableRun2.GetChangedRows();
                DataTable changedDurables = grdDurableChange2.GetChangedRows();

                ValidateDurableRequired2();

                MessageWorker worker = new MessageWorker("SaveChangeEquipment");
                worker.SetBody(new MessageBody()
                {
                    { "transactionType", "SetChangeDurable" },
                    { "lotid", this.lotId },
                    { "EquipmentID", equipments["EQUIPMENTID"].ToString() },
                    { "EndedDurables", endedDurables },
                    { "ChangedDurables", changedDurables }
                });

                worker.Execute();

                Clear();
                SearchLotInfo();
                InitializeGrid();

                TabControl_SelectedPageChanged(null, null);
            }
        }
        #endregion

        #region ◆ 유효성 검사 |
        
        #region ▶ 설비 변경 유효성 검사 |
        /// <summary>
        /// 유효성 검사
        /// </summary>
        private void ValidateDurableRequired()
        {
            DataTable dtEndEquip = grdEquipmentsRun.DataSource as DataTable;
            DataTable dtEndDurable = grdDurableRun.DataSource as DataTable;
            DataTable dtChangeEquip = grdEquipmentsChange.DataSource as DataTable;
            DataTable dtChangeDurable = grdDurableChange.DataSource as DataTable;

            if (UserInfo.Current.Enterprise != ENTERPRISE_YOUNGPOONG)
            {
                // 작업 시작 유효성검사
                var chkRequired = dtChangeEquip.AsEnumerable().Where(r => r.Field<string>("DURABLEREQUIRED").Equals("Y")
                    && r.Field<bool>("CHECK") == true).ToList();

                if (Format.GetInteger(chkRequired.Count()) > 0)
                {
                    bool hasDurable = false;

                    foreach (var equip in chkRequired)
                    {
                        foreach (DataRow each in dtChangeDurable.Rows)
                        {
                            if (equip.GetString("EQUIPMENTID").Equals(each["EQUIPMENTID"].ToString()) && (bool)each["CHECK"])
                            {
                                hasDurable = true;
                                break;
                            }
                        }
                        if (!hasDurable)
                        {
                            // 필수 치공구를 입력 하지 않았습니다.
                            throw MessageException.Create("CheckRequireDurable");
                        }
                    }
                }
            }

            // 작업 완료 유효성검사
            var chkRequiredEnd = dtEndEquip.AsEnumerable().Where(r => r.Field<string>("DURABLEREQUIRED").Equals("Y")
                && r.Field<bool>("ISCOMPLETED") == true).ToList();

            if (Format.GetInteger(chkRequiredEnd.Count()) > 0)
            {
                bool hasDurable = false;

                foreach (var equip in chkRequiredEnd)
                {
                    foreach (DataRow each in dtEndDurable.Rows)
                    {
                        if (equip.GetString("EQUIPMENTID").Equals(each["EQUIPMENTID"].ToString()))
                        {
                            hasDurable = true;
                            break;
                        }
                    }
                    if (!hasDurable)
                    {
                        // 필수 치공구를 입력 하지 않았습니다.
                        throw MessageException.Create("CheckRequireDurable");
                    }
                }
            }
        }
        #endregion

        #region ▶ 치공구 변경 유효성 검사 |
        /// <summary>
        /// 유효성 검사
        /// </summary>
        private void ValidateDurableRequired2()
        {
            DataRow EquipRow = grdWorkingEquipment.View.GetFocusedDataRow();
            DataTable dtEndDurable = grdDurableRun2.DataSource as DataTable;
            DataTable dtChangeDurable = grdDurableChange2.DataSource as DataTable;

            if (EquipRow == null)
            {
                // 작업 완료 할 설비를 선택하시기 바랍니다.
                throw MessageException.Create("SelectWorkCompletionEquipment");
            }

            var chk = dtEndDurable.AsEnumerable().Where(r => (bool)r.Field<bool>("ISCOMPLETED") == true).Count();

            if (UserInfo.Current.Enterprise != ENTERPRISE_YOUNGPOONG)
            {
                if (Format.GetInteger(chk) == 0)
                {
                    // 작업완료할 치공구를 선택하여 주십시오.
                    throw MessageException.Create("NotExistsEndDurable");
                }
            }

            if (UserInfo.Current.Enterprise != ENTERPRISE_YOUNGPOONG)
            {
                var changeChk = dtChangeDurable.AsEnumerable().Where(r => (bool)r.Field<bool>("CHECK") == true).Count();

                if (Format.GetInteger(changeChk) == 0)
                {
                    // 변경할 치공구를 선택하여 주십시오.
                    throw MessageException.Create("NoSelectChangeDurable");
                }
            }
        }
        #endregion
        #endregion

        #region ◆ Private Function |

        #region ▶ 화면 클리어 |
        /// <summary>
        /// 화면 클리어
        /// </summary>
        private void Clear()
        {
            lotId = string.Empty;

            grdLotInfo.ClearData();
            grdEquipmentsRun.DataSource = null;
            grdEquipmentsRun.Enabled = false;
            grdEquipmentsChange.DataSource = null;
            grdEquipmentsChange.Enabled = false;
            grdDurableRun.DataSource = null;
            grdDurableRun.Enabled = false;
            grdDurableChange.DataSource = null;
            grdDurableChange.Enabled = false;

            grdWorkingEquipment.DataSource = null;
            grdWorkingEquipment.Enabled = false;
            grdDurableRun2.DataSource = null;
            grdDurableRun2.Enabled = false;
            grdDurableChange2.DataSource = null;
            grdDurableChange2.Enabled = false;
        }
        #endregion

        #region ▶ FocusEquipmentGrid :: 설비 그리드의 특정 EquipmentId가 있는 행에 Focus를 준다 |
        /// <summary>
        /// 설비 그리드의 특정 EquipmentId가 있는 행에 Focus를 준다
        /// </summary>
        /// <param name="equipmentId"></param>
        private void FocusEquipmentGrid(string equipmentId)
        {
            DataTable dt = grdEquipmentsRun.DataSource as DataTable;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["EQUIPMENTID"].ToString() == equipmentId)
                {
                    grdEquipmentsRun.View.FocusedRowHandle = grdEquipmentsRun.View.GetRowHandle(i);
                    break;
                }
            }
        }
        #endregion

        #region ▶ CheckRunningEquipment :: 가동중 설비 완료설정 여부 체크 |
        /// <summary>
        /// 가동중 설비 완료설정 여부 체크
        /// </summary>
        /// <returns></returns>
        private int CheckRunningEquipment()
        {
            DataTable dt = grdEquipmentsRun.DataSource as DataTable;

            int count = dt.AsEnumerable().Where(r => r.Field<bool>("ISCOMPLETED").Equals(true)).Count();

            return count;
        }
        #endregion

        #region ▶ CheckChangeEquipment :: 변경설비 체크설정 여부 체크 |
        /// <summary>
        /// 변경설비 체크설정 여부 체크
        /// </summary>
        /// <returns></returns>
        private int CheckChangeEquipment()
        {
            DataTable dt = grdEquipmentsChange.DataSource as DataTable;

            int count = dt.AsEnumerable().Where(r => r.Field<bool>("CHECK").Equals(true)).Count();

            return count;
        }
        #endregion

        #region ▶ CheckDurableComplete :: 치공구 LOTID의 작업완료 체크 여부 조회 |
        /// <summary>
        /// 치공구 LOTID의 작업완료 체크 여부 조회
        /// </summary>
        /// <param name="durablelotid"></param>
        /// <returns></returns>
        private int CheckDurableComplete(string durablelotid)
        {
            DataTable dt = grdDurableRun.DataSource as DataTable;

            int count = 0;

            if (dt.AsEnumerable().Where(r => r.Field<string>("DURABLELOTID").Equals(durablelotid)).Count() > 0)
            {
                count = dt.AsEnumerable().Where(r => r.Field<string>("DURABLELOTID").Equals(durablelotid) && r.Field<string>("ISCOMPLETED").Equals("Y")).Count();
            }
            else
            {
                count = 1;
            }

            return count;
        }

        /// <summary>
        /// 설비에서 사용중인 치공구가 있는지 확인(설비변경 탭)
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        private bool CheckEquipmentHasDurable(string equipmentId)
        {
            DataTable dt = grdDurableRun.DataSource as DataTable;
            int durablesInEquipment = dt.AsEnumerable().Where(r => r.Field<string>("EQUIPMENTID").Equals(equipmentId) && r.Field<string>("ISCOMPLETED").Equals("N")).Count();
            return durablesInEquipment > 0;
        }

        /// 설비에서 사용중인 치공구가 있는지 확인(치공구변경 탭)
        private bool CheckEquipmentHasDurable2(string equipmentId)
        {
            DataTable dt = grdDurableRun2.DataSource as DataTable;
            int durablesInEquipment = dt.AsEnumerable().Where(r => r.Field<string>("EQUIPMENTID").Equals(equipmentId) && r.Field<bool>("ISCOMPLETED") == false).Count();
            return durablesInEquipment > 0;
        }
        #endregion
        #endregion
    }
}
