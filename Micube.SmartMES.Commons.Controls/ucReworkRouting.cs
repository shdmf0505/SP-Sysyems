#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
#endregion

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 프 로 그 램 명  : 공통 > 재작업 라우팅 선택용 UserControl
    /// 업  무  설  명  : 재작업 라우팅 및 복귀공정 선택 UserControl
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-02-12
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ucReworkRouting : UserControl
    {
        #region ◆ Variables
        private ReworkRoutingPopup reworkPopup = new ReworkRoutingPopup();
        #endregion

        #region ◆ Public Properties
        // LOT 정보
        public string LotId { get; private set; }
        public string LotProductDefId { get; private set; }
        public string LotProductDefVersion { get; private set; }
        public string LotResourceId { get; private set; }
        public string LotProcessSegmentId { get; private set; }
        public string LotProcessPathId { get; private set; }
        public string LotProcessState { get; private set; }
        public string LotUserSequence { get; private set; }
        public string LotProcessDefId { get; private set; }
        public string LotProcessDefVersion { get; private set; }
        public string LotProcessSegmentName { get; private set; }
        public int LotPathSequence { get; private set; }

        // 반환값
        public string ReworkProcessDefId { get; private set; }
        public string ReworkProcessDefVersion { get; private set; }
        public string ReworkUserSequence { get; private set; }
        public int ReworkPathSequence { get; private set; }
        public string ReworkProcessSegmentId { get; private set; }
        public string ReworkProcessSegmentName { get; private set; }
        public string ReworkProcessPathId { get; private set; }
        public string ReturnProcessPathId { get; private set; }
        public string ReturnResourceId { get; private set; }
        public string ReturnResourceName { get; private set; }
        public string ReturnAreaId { get; private set; }
        public string ResourceId { get; private set; }
        public string ResourceName { get; private set; }
        public string ReturnProcessSegmentId { get; private set; }
        public int ReturnPathSequence { get; private set; }
        public string AreaId { get; private set; }
        public string ToResourceId { get; private set; }
        public string ToResourceName { get; private set; }
        public string ToProcessPathId { get; private set; }
        public string ToAreaId { get; private set; }
        public bool IsProductRouting
        {
            get { return chkProductRouting.Checked; }
            set { chkProductRouting.Checked = value; }
        }

        // 기타
        public int SplitterPosition
        {
            get { return smartSpliterContainer1.SplitterPosition; }
            set { smartSpliterContainer1.SplitterPosition = value; }
        }
        public bool ReworkTypeVisible
        {
            get { return chkProductRouting.Visible; }
            set { chkProductRouting.Visible = value; }
        }
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public ucReworkRouting()
        {
            InitializeComponent();
            InitializeControls();
            InitializeEvent();
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        public void InitializeControls()
        {
            InitializeGrid();
            InitializeCombobox();
        }

        /// <summary>
        /// Control Event 설정
        /// </summary>
        private void InitializeEvent()
        {
            // Grid
            grdCurrentRouting.View.CheckStateChanged += CurrentRouting_CheckStateChanged;
            grdReworkRouting.View.CheckStateChanged += ReworkRouting_CheckStateChanged;
            grdCurrentRouting.View.RowCellStyle += CurrentRouting_RowCellStyle;

            // CheckBox
            chkProductRouting.CheckedChanged += ChkProductRouting_CheckedChanged;

            // TextBox
            txtReworkRouting.Editor.ButtonClick += Editor_ButtonClick;
        }

        #region ▶ Grid Control 초기화 |
        /// <summary>
        /// Grid Control 초기화
        /// </summary>
        private void InitializeGrid()
        {
            InitializeGrdReworkRouting();
            InitializeGrdCurrentRouting();
        }

        /// <summary>
        /// 재작업 라우팅 그리드 초기화
        /// </summary>
        private void InitializeGrdReworkRouting()
        {
            grdReworkRouting.GridButtonItem = GridButtonItem.None;
            grdReworkRouting.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdReworkRouting.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdReworkRouting.View.ClearColumns();
            grdReworkRouting.View.AddTextBoxColumn("USERSEQUENCE", 80)          // 공정순서
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 180)   // 공정명
                .SetIsReadOnly();
            grdReworkRouting.View.AddTextBoxColumn("RESOURCENAME", 220)         // 자원명
                .SetIsReadOnly();
            grdReworkRouting.View.AddTextBoxColumn("DESCRIPTION", 300)          // 특기사항
                .SetLabel("OPERATIONDFF")
                .SetIsReadOnly();
            grdReworkRouting.View.AddTextBoxColumn("CREATOR", 90)               // 등록자
                .SetIsReadOnly();
            grdReworkRouting.View.AddTextBoxColumn("PROCESSDEFID")              // 라우팅ID
                .SetIsHidden();
            grdReworkRouting.View.AddTextBoxColumn("PROCESSDEFVERSION")         // 라우팅 버전
                .SetIsHidden();
            grdReworkRouting.View.AddTextBoxColumn("PROCESSPATHID")             // 공정경로 ID
                .SetIsHidden();
            grdReworkRouting.View.AddTextBoxColumn("RESOURCEID")                // 자원 ID
                .SetIsHidden();
            grdReworkRouting.View.AddTextBoxColumn("AREAID")                    // 작업장 ID
                .SetIsHidden();
            grdReworkRouting.View.PopulateColumns();
        }

        /// <summary>
        /// LOT 라우팅 그리드 초기화
        /// </summary>
        private void InitializeGrdCurrentRouting()
        {
            grdCurrentRouting.GridButtonItem = GridButtonItem.None;
            grdCurrentRouting.View.OptionsSelection.MultiSelect = false;
            grdCurrentRouting.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCurrentRouting.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdCurrentRouting.View.AddTextBoxColumn("USERSEQUENCE", 80)         // 공정순서
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Right);
            grdCurrentRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200)  // 공정명
                .SetIsReadOnly();
            grdCurrentRouting.View.AddTextBoxColumn("PROCESSPATHID")
                .SetIsHidden();
            grdCurrentRouting.View.AddTextBoxColumn("DISPLAYSEQUENCE")          // 1 = 이전공정, 2 = 현재공정, 3 = 이후공정
                .SetIsHidden();
            grdCurrentRouting.View.PopulateColumns();
            grdCurrentRouting.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdCurrentRouting.View.CheckMarkSelection.ShowCheckBoxHeader = false;
        }
        #endregion

        #region ▶ 콤보박스 초기화 |
        /// <summary>
        /// 콤보박스 초기화
        /// </summary>
        private void InitializeCombobox()
        {
            // 투입 자원
            cboResource.Editor.DisplayMember = "RESOURCENAME";
            cboResource.Editor.ValueMember = "RESOURCEID";
            cboResource.Editor.ShowHeader = false;
            cboResource.Editor.UseEmptyItem = true;

            // 재작업 후 투입 자원
            cboResourceReturn.Editor.DisplayMember = "RESOURCENAME";
            cboResourceReturn.Editor.ValueMember = "RESOURCEID";
            cboResourceReturn.Editor.ShowHeader = false;
            cboResourceReturn.Editor.UseEmptyItem = true;
        }
        #endregion

        #endregion

        #region ◆ Event |
        #region - CodeHelp Popup closed Event |
        /// <summary>
        /// CodeHelp Popup closed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rpop_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReworkRoutingPopup rpop = sender as ReworkRoutingPopup;
            if (rpop.DialogResult == DialogResult.OK)
            {
                DataRow row = rpop.CurrentDataRow;
                string processDefID = Format.GetFullTrimString(row["REWORKNUMBER"]);
                string processDefVersion = Format.GetFullTrimString(row["REWORKVERSION"]);
                txtReworkRouting.Editor.SetValue(processDefID);
                txtReworkRouting.Editor.Text = row["REWORKNAME"].ToString();
                txtRoutingVersion.Text = processDefVersion;
                RefreshReworkRoutingPath(processDefID, processDefVersion);
                RefreshResource();
                RefreshResourceReturn();
            }
        }
        #endregion

        #region - CurrentRouting_CheckStateChanged :: LOT 라우팅 체크 상태 변경 시 이벤트 |
        /// <summary>
        /// LOT 라우팅 체크 상태 변경 시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentRouting_CheckStateChanged(object sender, EventArgs e)
        {
            // 한 행만 체크 가능
            grdCurrentRouting.View.CheckStateChanged -= CurrentRouting_CheckStateChanged;
            grdCurrentRouting.View.CheckedAll(false);

            DataRowView row = grdCurrentRouting.View.GetRow(grdCurrentRouting.View.GetFocusedDataSourceRowIndex()) as DataRowView;
            DataRow rowData = row.Row;
            if (rowData["DISPLAYSEQUENCE"].ToString() != "1")
            {
                grdCurrentRouting.View.CheckRow(grdCurrentRouting.View.GetFocusedDataSourceRowIndex(), true);
            }
            RefreshResourceReturn();
            grdCurrentRouting.View.CheckStateChanged += CurrentRouting_CheckStateChanged;
        }
        #endregion

        #region - ReworkRouting_CheckStateChanged :: 재작업 라우팅 그리드 한 ROW 만 체크되도록 강제, 공정 선택시 작업장 재조회 |
        /// <summary>
        /// 재작업 라우팅 그리드 한 ROW 만 체크되도록 강제, 공정 선택시 작업장 재조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReworkRouting_CheckStateChanged(object sender, EventArgs e)
        {
            // 한 행만 체크 가능
            grdReworkRouting.View.CheckStateChanged -= ReworkRouting_CheckStateChanged;
            grdReworkRouting.View.CheckedAll(false);
            grdReworkRouting.View.CheckRow(grdReworkRouting.View.GetFocusedDataSourceRowIndex(), true);
            grdReworkRouting.View.CheckStateChanged += ReworkRouting_CheckStateChanged;
            RefreshResource();
            RefreshResourceReturn();
        }
        #endregion

        #region - CurrentRouting_RowCellStyle :: LOT 라우팅 현재공정 ROW 색상 변경 |
        /// <summary>
        /// LOT 라우팅 현재공정 ROW 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentRouting_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (grdCurrentRouting.View.GetRowCellValue(e.RowHandle, "DISPLAYSEQUENCE").ToString() == "2")
            {
                e.Appearance.BackColor = Color.Aquamarine;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        #endregion

        #region ▶ CheckBox Event |
        /// <summary>
        /// 품목라우팅 체크박스 체크상태 변경시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkProductRouting_CheckedChanged(object sender, EventArgs e)
        {
            SearchRoutings();
        }
        #endregion

        #region - CodeHepl Button Click Event |
        /// <summary>
        /// CodeHepl Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear && !chkProductRouting.Checked)
            {
                grdReworkRouting.DataSource = null;
                cboResource.Editor.DataSource = null;
                cboResource.Enabled = false;
            }
        }
        #endregion

        #endregion

        #region ▶ CodeHelp 초기화 |
        /// <summary>
        /// 재작업 라우팅 선택 컨트롤 설정
        /// </summary>
        private void SetTxtReworkRouting(string segmentid = "")
        {
            ConditionItemSelectPopup reworkRoutingSelectPopup = new ConditionItemSelectPopup();
            reworkRoutingSelectPopup.CustomPopup = reworkPopup;
            reworkRoutingSelectPopup.Id = "REWORKROUTING";
            reworkRoutingSelectPopup.ValueFieldName = "REWORKNUMBER";
            reworkRoutingSelectPopup.DisplayFieldName = "REWORKNAME";
            reworkRoutingSelectPopup.SetPopupCustomParameter((popup, dataRow) =>
            {
                ReworkRoutingPopup rpop = popup as ReworkRoutingPopup;
                rpop.LotId = LotId;
                rpop.ProcessSegmentId = segmentid;
                rpop.FormClosed += Rpop_FormClosed;
            });
            txtReworkRouting.Editor.SelectPopupCondition = reworkRoutingSelectPopup;
        }
        #endregion

        #region ◆ private function |

        #region ▶ RefreshReworkRoutingPath :: 재작업 라우팅 조회 |
        /// <summary>
        /// 재작업 라우팅 조회
        /// </summary>
        /// <param name="ProcessDefid"></param>
        /// <param name="ProcessDefVersion"></param>
        private void RefreshReworkRoutingPath(string ProcessDefid, string ProcessDefVersion)
        {
            Dictionary<string, object> Param = new Dictionary<string, object>();
            Param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            Param.Add("PROCESSDEFID", ProcessDefid);
            Param.Add("PROCESSDEFVERSION", ProcessDefVersion);
            Param.Add("PROCESSDEFTYPE", "Rework");
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtProcessPath = SqlExecuter.Query("GetProcessPathList", "10021", Param);
            grdReworkRouting.DataSource = dtProcessPath;
            grdReworkRouting.Enabled = true;
        }

        #region ▶ SearchCurrentRouting :: LOT 라우팅 조회 |
        /// <summary>
        /// LOT 라우팅 조회
        /// </summary>
        private void SearchCurrentRouting()
        {
            grdCurrentRouting.Enabled = true;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", LotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable currentRouting = SqlExecuter.Query("GetCurrentRoutingByLot", "10001", param);
            grdCurrentRouting.DataSource = currentRouting;
            CheckDefaultReturnRouting();
        }
        #endregion

        #region ▶ SearchRoutings :: 품목라우팅 체크박스 체크상태 변경시 이벤트 처리 |
        /// <summary>
        /// 품목라우팅 체크박스 체크상태 변경시 이벤트 처리
        /// </summary>
        private void SearchRoutings()
        {
            if (chkProductRouting.Checked)
            {
                // 품목 라우팅

                grdCurrentRouting.Enabled = false;
                grdCurrentRouting.DataSource = null;

                txtReworkRouting.Enabled = false;
                txtReworkRouting.EditValue = string.Empty;

                grdReworkRouting.View.OptionsSelection.MultiSelect = false;
                grdReworkRouting.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", LotId);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable currentRouting = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10001", param);
                grdReworkRouting.DataSource = currentRouting;
                RefreshResource();
                RefreshResourceReturn();
            }
            else
            {
                grdReworkRouting.View.OptionsSelection.MultiSelect = false;
                grdReworkRouting.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

                grdCurrentRouting.Enabled = true;
                grdReworkRouting.DataSource = null;
                txtReworkRouting.Enabled = true;
                SearchCurrentRouting();
                RefreshResource();
                RefreshResourceReturn();
            }
        }
        #endregion

        #endregion


        #region ▶ RefreshResource :: 자원 정보 재조회 |
        /// <summary>
        /// 자원 정보 재조회
        /// </summary>
        private void RefreshResource()
        {
            DataTable dtProcessPath = grdReworkRouting.DataSource as DataTable;
            if (dtProcessPath == null || dtProcessPath.Rows.Count == 0)
            {
                cboResource.Editor.DataSource = null;
                cboResource.Enabled = false;
            }
            else
            {
                if (chkProductRouting.Checked)
                {
                    DataTable repositionProcess = grdReworkRouting.View.GetCheckedRows();
                    if (repositionProcess.Rows.Count == 0)
                    {
                        cboResource.Editor.DataSource = null;
                        cboResource.Enabled = false;
                    }
                    else
                    {
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                        param.Add("PLANTID", UserInfo.Current.Plant);
                        param.Add("PRODUCTDEFID", this.LotProductDefId);
                        param.Add("PRODUCTDEFVERSION", this.LotProductDefVersion);
                        param.Add("PROCESSDEFID", repositionProcess.Rows[0]["PROCESSDEFID"].ToString());
                        param.Add("PROCESSDEFVERSION", repositionProcess.Rows[0]["PROCESSDEFVERSION"].ToString());
                        param.Add("PROCESSPATHID", repositionProcess.Rows[0]["PROCESSPATHID"].ToString());
                        param.Add("PROCESSDEFTYPE", "Main");
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                        DataTable dtResourceList = SqlExecuter.Query("SelectReworkRoutingResource", "10002", param);
                        cboResource.Editor.DataSource = dtResourceList;
                        cboResource.Enabled = true;

                        if (dtResourceList.Rows.Count == 2)
                        {
                            cboResource.Editor.ItemIndex = 1;
                        }
                        else
                        {
                            for (int i = 0; i < dtResourceList.Rows.Count; i++)
                            {
                                DataRow dr = dtResourceList.Rows[i];

                                if (Format.GetFullTrimString(dr["ISPRIMARYRESOURCE"]).Equals("Y"))
                                {
                                    cboResource.Editor.ItemIndex = i;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("PLANTID", UserInfo.Current.Plant);
                    param.Add("PRODUCTDEFID", this.LotProductDefId);
                    param.Add("PRODUCTDEFVERSION", this.LotProductDefVersion);
                    param.Add("PROCESSDEFID", dtProcessPath.Rows[0]["PROCESSDEFID"].ToString());
                    param.Add("PROCESSDEFVERSION", dtProcessPath.Rows[0]["PROCESSDEFVERSION"].ToString());
                    param.Add("PROCESSDEFTYPE", "Rework");
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable dtAreaList = SqlExecuter.Query("SelectReworkRoutingResource", "10002", param);
                    cboResource.Editor.DataSource = dtAreaList;
                    cboResource.Enabled = true;

                    if (dtAreaList.Rows.Count == 2)
                    {
                        cboResource.Editor.ItemIndex = 1;
                    }
                    else
                    {
                        for (int i = 0; i < dtAreaList.Rows.Count; i++)
                        {
                            DataRow dr = dtAreaList.Rows[i];

                            if (Format.GetFullTrimString(dr["ISPRIMARYRESOURCE"]).Equals("Y"))
                            {
                                cboResource.Editor.ItemIndex = i;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ▶ RefreshResourceReturn :: 재작업 후 공정 자원 정보 재조회 |
        /// <summary>
        /// 재작업 후 공정 자원 정보 재조회
        /// </summary>
        private void RefreshResourceReturn()
        {
            DataTable dtProcessPath = grdCurrentRouting.DataSource as DataTable;
            if (dtProcessPath == null || dtProcessPath.Rows.Count == 0 || chkProductRouting.Checked)
            {
                cboResourceReturn.Editor.DataSource = null;
                cboResourceReturn.Enabled = false;
            }
            else
            {
                DataTable returnProcess = grdCurrentRouting.View.GetCheckedRows();
                if (returnProcess.Rows.Count == 0)
                {
                    cboResourceReturn.Editor.DataSource = null;
                    cboResourceReturn.Enabled = false;
                }
                else
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("PLANTID", UserInfo.Current.Plant);
                    param.Add("PRODUCTDEFID", this.LotProductDefId);
                    param.Add("PRODUCTDEFVERSION", this.LotProductDefVersion);
                    param.Add("PROCESSDEFID", returnProcess.Rows[0]["PROCESSDEFID"].ToString());
                    param.Add("PROCESSDEFVERSION", returnProcess.Rows[0]["PROCESSDEFVERSION"].ToString());
                    param.Add("PROCESSPATHID", returnProcess.Rows[0]["PROCESSPATHID"].ToString());
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable dtResourceList = SqlExecuter.Query("SelectReworkRoutingResource", "10001", param);
                    cboResourceReturn.Editor.DataSource = dtResourceList;
                    cboResourceReturn.Enabled = true;


                    if (dtResourceList.Rows.Count == 2)
                    {
                        cboResourceReturn.Editor.ItemIndex = 1;
                    }
                    else
                    {
                        for (int i = 0; i < dtResourceList.Rows.Count; i++)
                        {
                            DataRow dr = dtResourceList.Rows[i];

                            if (returnProcess.Rows[0]["DISPLAYSEQUENCE"].ToString() != "2")     // 재작업 후 공정이 현재공정이 아니면
                            {
                                if (Format.GetFullTrimString(dr["ISPRIMARYRESOURCE"]).Equals("Y"))
                                {
                                    cboResourceReturn.Editor.ItemIndex = i;
                                    break;
                                }
                            }
                            else    // 재작업 후 공정이 현재 공정이면
                            {
                                if (this.LotResourceId == dr["RESOURCEID"].ToString())
                                {
                                    cboResourceReturn.Editor.ItemIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private void LoadLotInfo(string lotId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable lotInfo = SqlExecuter.Query("SelectLotInfo", "10002", param);

            if(lotInfo != null && lotInfo.Rows.Count > 0)
            {
                string processPathStack = lotInfo.Rows[0]["PROCESSPATHSTACK"].ToString();
                string[] processPathIds = processPathStack.Split('.');

                this.LotId = lotId;
                this.LotProductDefId = lotInfo.Rows[0]["PRODUCTDEFID"].ToString();
                this.LotProductDefVersion = lotInfo.Rows[0]["PRODUCTDEFVERSION"].ToString();
                this.LotResourceId = lotInfo.Rows[0]["RESOURCEID"].ToString();
                this.LotProcessSegmentId = lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString();
                this.LotProcessPathId = processPathIds[processPathIds.Length - 1];
                this.LotProcessState = lotInfo.Rows[0]["PROCESSSTATE"].ToString();
                this.LotUserSequence = lotInfo.Rows[0]["USERSEQUENCE"].ToString();
                this.LotProcessDefId = lotInfo.Rows[0]["PROCESSDEFID"].ToString();
                this.LotProcessDefVersion = lotInfo.Rows[0]["PROCESSDEFVERSION"].ToString();
                this.LotProcessSegmentName = lotInfo.Rows[0]["PROCESSSEGMENTNAME"].ToString();
                this.LotPathSequence = int.Parse(lotInfo.Rows[0]["PATHSEQUENCE"].ToString());
            }
            else
            {
                this.LotId = null;
                this.LotProductDefId = null;
                this.LotProductDefVersion = null;
                this.LotResourceId = null;
                this.LotProcessSegmentId = null;
                this.LotProcessPathId = null;
                this.LotProcessState = null;
                this.LotUserSequence = null;
                this.LotProcessDefId = null;
                this.LotProcessDefVersion = null;
                this.LotProcessSegmentName = null;
                this.LotPathSequence = 0;
            }
        }

        #region ▶ CheckDefaultReturnRouting :: 재작업 후 공정의 기본값에 체크표시를 한다 |
        /// <summary>
        /// 재작업 후 공정의 기본값에 체크표시를 한다
        /// </summary>
        private void CheckDefaultReturnRouting()
        {
            if (chkProductRouting.Checked)
            {
                return;
            }
            if(this.LotId == null)
            {
                return;
            }
            int rowIndex = GetCurrentProcessRowIndex(this.LotProcessPathId);
            if (rowIndex < 0)
            {
                return;
            }
            int rowHandle = grdCurrentRouting.View.GetRowHandle(rowIndex);
            grdCurrentRouting.View.CheckStateChanged -= CurrentRouting_CheckStateChanged;
            grdCurrentRouting.View.CheckRow(rowHandle, true);
            grdCurrentRouting.View.CheckStateChanged += CurrentRouting_CheckStateChanged;
        }
        #endregion

        #region ▶ GetCurrentProcessRowIndex :: Routing PathID로 RowIndex 조회 |
        /// <summary>
        /// Routing PathID로 RowIndex 조회
        /// </summary>
        /// <param name="processPathId"></param>
        /// <returns></returns>
        private int GetCurrentProcessRowIndex(string processPathId)
        {
            DataTable dtCurrentRouting = grdCurrentRouting.DataSource as DataTable;
            if (dtCurrentRouting == null)
            {
                return -1;
            }
            for (int i = 0; i < dtCurrentRouting.Rows.Count; i++)
            {
                DataRow each = dtCurrentRouting.Rows[i];
                if (each["PROCESSPATHID"].ToString() == processPathId)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #endregion

        #region ◆ public function |

        public void Apply(string lotId)
        {
            if(lotId == null)
            {
                return;
            }
            LoadLotInfo(lotId);
            if (this.LotId != null)
            {
                grdReworkRouting.Enabled = true;
                SetTxtReworkRouting(this.LotProcessSegmentId);
                SearchRoutings();
            }
            else
            {
                SetTxtReworkRouting("");
                Clear();
            }
        }

        /// <summary>
        /// 화면 클리어
        /// </summary>
        public void Clear()
        {
            ClearData();
            ClearScreen();
        }

        private void ClearData()
        {
            // LOT 정보
            LotId = null;
            LotProductDefId = null;
            LotProductDefVersion = null;
            LotResourceId = null;
            LotProcessSegmentId = null;
            LotProcessPathId = null;
            LotProcessState = null;
            LotUserSequence = null;
            LotProcessDefId = null;
            LotProcessDefVersion = null;
            LotProcessSegmentName = null;
            LotPathSequence = 0;

            // 반환값
            ReworkProcessDefId = null;
            ReworkProcessDefVersion = null;
            ReworkUserSequence = null;
            ReworkPathSequence = 0;
            ReworkProcessSegmentId = null;
            ReworkProcessSegmentName = null;
            ReworkProcessPathId = null;
            ReturnProcessPathId = null;
            ReturnResourceId = null;
            ReturnAreaId = null;
            ResourceId = null;
            ReturnProcessSegmentId = null;
            ReturnPathSequence = 0;
            AreaId = null;
            ToResourceId = null;
            ToProcessPathId = null;
            ToAreaId = null;
            ResourceName = null;
            ToResourceName = null;
            ReturnResourceName = null;
        }

        private void ClearScreen()
        {
            grdReworkRouting.DataSource = null;
            grdReworkRouting.Enabled = false;
            grdCurrentRouting.DataSource = null;
            grdCurrentRouting.Enabled = false;
            cboResource.Enabled = false;
            cboResource.Editor.DataSource = null;
            cboResourceReturn.Enabled = false;
            cboResourceReturn.Editor.DataSource = null;
            txtReworkRouting.Editor.ClearValue();
            txtRoutingVersion.Text = string.Empty;
        }

        public void FillResultProperties()
        {
            ValidateInput();

            if (chkProductRouting.Checked)
            {
                this.ReworkProcessDefId = grdReworkRouting.View.GetCheckedRows().Rows[0]["PROCESSDEFID"].ToString();
                this.ReworkProcessDefVersion = grdReworkRouting.View.GetCheckedRows().Rows[0]["PROCESSDEFVERSION"].ToString();
                this.ReturnProcessPathId = grdReworkRouting.View.GetCheckedRows().Rows[0]["PROCESSPATHID"].ToString();
                this.ReworkUserSequence = grdReworkRouting.View.GetCheckedRows().Rows[0]["USERSEQUENCE"].ToString();
                this.ReworkPathSequence = int.Parse(grdReworkRouting.View.GetCheckedRows().Rows[0]["PATHSEQUENCE"].ToString());
                this.ReworkProcessSegmentId = grdReworkRouting.View.GetCheckedRows().Rows[0]["PROCESSSEGMENTID"].ToString();
                this.ReworkProcessSegmentName = grdReworkRouting.View.GetCheckedRows().Rows[0]["PROCESSSEGMENTNAME"].ToString();
                this.ReworkProcessPathId = grdReworkRouting.View.GetCheckedRows().Rows[0]["PROCESSPATHID"].ToString();
            }
            else
            {
                this.ReworkProcessDefId = Format.GetFullTrimString(txtReworkRouting.Editor.GetValue());
                this.ReworkProcessDefVersion = Format.GetFullTrimString(txtRoutingVersion.Text);
                this.ReturnProcessPathId = grdCurrentRouting.View.GetCheckedRows().Rows[0]["PROCESSPATHID"].ToString();
                this.ReturnProcessSegmentId = grdCurrentRouting.View.GetCheckedRows().Rows[0]["PROCESSSEGMENTID"].ToString();
                this.ReturnPathSequence = int.Parse(grdCurrentRouting.View.GetCheckedRows().Rows[0]["PATHSEQUENCE"].ToString());
                this.ReturnAreaId = (cboResourceReturn.Editor.GetSelectedDataRow() as DataRowView).Row["AREAID"].ToString();
                this.ReturnResourceId = (cboResourceReturn.Editor.GetSelectedDataRow() as DataRowView).Row["RESOURCEID"].ToString();
                this.ReturnResourceName = (cboResourceReturn.Editor.GetSelectedDataRow() as DataRowView).Row["RESOURCENAME"].ToString();
                this.ToResourceId = (cboResource.Editor.GetSelectedDataRow() as DataRowView).Row["RESOURCEID"].ToString();
                this.ToResourceName = (cboResource.Editor.GetSelectedDataRow() as DataRowView).Row["RESOURCENAME"].ToString();

                this.ToAreaId = (cboResource.Editor.GetSelectedDataRow() as DataRowView).Row["AREAID"].ToString();
                this.ToProcessPathId = ((DataTable)grdReworkRouting.DataSource).Rows[0]["PROCESSPATHID"].ToString();
                this.ReworkUserSequence = ((DataTable)grdReworkRouting.DataSource).Rows[0]["USERSEQUENCE"].ToString();
                this.ReworkPathSequence = int.Parse(((DataTable)grdReworkRouting.DataSource).Rows[0]["PATHSEQUENCE"].ToString());
                this.ReworkProcessSegmentId = ((DataTable)grdReworkRouting.DataSource).Rows[0]["PROCESSSEGMENTID"].ToString();
                this.ReworkProcessSegmentName = ((DataTable)grdReworkRouting.DataSource).Rows[0]["PROCESSSEGMENTNAME"].ToString();
                this.ReworkProcessPathId = ((DataTable)grdReworkRouting.DataSource).Rows[0]["PROCESSPATHID"].ToString();
            }
            this.ResourceId = (cboResource.Editor.GetSelectedDataRow() as DataRowView).Row["RESOURCEID"].ToString();
            this.ResourceName = (cboResource.Editor.GetSelectedDataRow() as DataRowView).Row["RESOURCENAME"].ToString();
            this.AreaId = (cboResource.Editor.GetSelectedDataRow() as DataRowView).Row["AREAID"].ToString();
        }

        private void ValidateInput()
        {
            if (this.LotId == null)
            {
                // 해당 Lot이 존재하지 않습니다. {0}
                throw MessageException.Create("NotExistLot");
            }

            if (!chkProductRouting.Checked)
            {
                if (txtReworkRouting.Editor.GetValue() == null || string.IsNullOrEmpty(txtReworkRouting.Editor.GetValue().ToString()))
                {
                    // 재작업 라우팅을 선택 해주세요.
                    throw MessageException.Create("ReworkRoutingValidation");
                }
                if (grdCurrentRouting.View.GetCheckedRows().Rows.Count == 0)
                {
                    // 재작업 후 공정을 선택 해주세요.
                    throw MessageException.Create("ReturnProcessPathValidation");
                }
                if (cboResourceReturn.GetValue() == null || string.IsNullOrEmpty(cboResourceReturn.GetValue().ToString()) || !cboResourceReturn.Enabled)
                {
                    // 선택된 자원이 없습니다.
                    throw MessageException.Create("NoResourceSelected");
                }
            }
            else
            {
                if (grdReworkRouting.View.GetCheckedRows().Rows.Count == 0)
                {
                    // 시작 공정을 선택 해주세요.
                    throw MessageException.Create("StartProcessPathValidation");
                }
            }

            if (cboResource.GetValue() == null || string.IsNullOrEmpty(cboResource.GetValue().ToString()) || !cboResource.Enabled)
            {
                // 선택된 자원이 없습니다.
                throw MessageException.Create("NoResourceSelected");
            }
        }

        #endregion
    }
}
