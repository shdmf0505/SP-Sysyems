#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 불량품 폐기취소 > 라우팅 팝업
    /// 업  무  설  명  : 불량품을 폐기하여 양품 Lot을 어떤 라우팅을 태울것인지 선택하는 팝업
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-10-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectCancelRoutingPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 투입공정정보를 보내기 위한 Handler
        /// </summary>
        /// <param name="row"></param>
        public delegate void InputSegmentDataHandler(DataRow row);
        public event InputSegmentDataHandler InputSegmentDataEvent;

        /// <summary>
        /// 재작업 후 공정정보를 보내기 위한 Handler
        /// </summary>
        /// <param name="row"></param>
        public delegate void ReturnSegmentDataHandler(DataRow row);
        public event ReturnSegmentDataHandler ReturnSegmentDataEvent;

        /// <summary>
        /// 재작업 라우팅일 경우 선택한 재작업 라우팅 테이블
        /// </summary>
        /// <param name="row"></param>
        public delegate void ReworkRoutingDataHandler(DataTable dt);
        public event ReworkRoutingDataHandler ReworkRoutingDataEvent;

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectCancelRoutingPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 재작업 라우팅 그리드 초기화
        /// </summary>
        private void InitializeGrdReworkRouting()
        {
            grdReworkRouting.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdReworkRouting.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdReworkRouting.View.CheckMarkSelection.MultiSelectCount = 1;
            grdReworkRouting.View.SetIsReadOnly();
            grdReworkRouting.View.SetAutoFillColumn("PROCESSSEGMENTNAME");
            grdReworkRouting.View.ClearColumns();

            grdReworkRouting.View.AddTextBoxColumn("USERSEQUENCE", 80)         
                .SetTextAlignment(TextAlignment.Right); // 공정순서
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTID", 120); // 공정 ID
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정 Version
            grdReworkRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200); // 공정명
            grdReworkRouting.View.AddTextBoxColumn("PROCESSDEFID")              
                .SetIsHidden(); // 라우팅 ID
            grdReworkRouting.View.AddTextBoxColumn("PROCESSDEFVERSION")         
                .SetIsHidden(); // 라우팅 Version
            grdReworkRouting.View.AddTextBoxColumn("PROCESSPATHID")            
                .SetIsHidden(); // 라우팅 상세 ID

            grdReworkRouting.View.PopulateColumns();
        }

        /// <summary>
        /// LOT 라우팅 그리드 초기화
        /// </summary>
        private void InitializeGrdCurrentRouting()
        {
            grdCurrentRouting.View.OptionsSelection.MultiSelect = false;
            grdCurrentRouting.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdCurrentRouting.View.CheckMarkSelection.MultiSelectCount = 1;
            grdCurrentRouting.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdCurrentRouting.GridButtonItem = GridButtonItem.None;
            grdCurrentRouting.View.SetIsReadOnly();

            grdCurrentRouting.View.AddTextBoxColumn("USERSEQUENCE", 80)         
                .SetTextAlignment(TextAlignment.Right); // 공정순서
            grdCurrentRouting.View.AddTextBoxColumn("PROCESSSEGMENTID", 120); // 공정 ID
            grdCurrentRouting.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 공정 Version
            grdCurrentRouting.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200); // 공정명
            grdCurrentRouting.View.AddTextBoxColumn("WORKSTARTTIME", 150)       
                .SetTextAlignment(TextAlignment.Center); // 작업시작시간
            grdCurrentRouting.View.AddTextBoxColumn("WORKENDTIME", 150)        
                .SetTextAlignment(TextAlignment.Center); // 작업완료시간
            grdCurrentRouting.View.AddTextBoxColumn("PROCESSPATHID")
                .SetIsHidden(); // 라우팅 상세 ID
            grdCurrentRouting.View.AddTextBoxColumn("DISPLAYSEQUENCE")          
                .SetIsHidden(); // 1 = 이전공정, 2 = 현재공정, 3 = 이후공정
            grdCurrentRouting.View.AddTextBoxColumn("AREAID")
                .SetIsHidden(); // 작업장 ID
            grdCurrentRouting.View.AddTextBoxColumn("AREANAME")
                .SetIsHidden(); // 작업장명


            grdCurrentRouting.View.PopulateColumns();

            grdCurrentRouting.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdCurrentRouting.View.CheckMarkSelection.ShowCheckBoxHeader = false;

            SearchRoutings();
        }

        /// <summary>
        /// 부모 그리드에서 자식 팝업으로 데이터 바인딩
        /// </summary>
        private void InitializeCurrentData()
        {

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnApply.Click += BtnApply_Click;
            btnClose.Click += BtnClose_Click;

            grdCurrentRouting.View.CheckStateChanged += CurrentRouting_CheckStateChanged;
            grdReworkRouting.View.CheckStateChanged += ReworkRouting_CheckStateChanged;
            grdCurrentRouting.View.RowCellStyle += CurrentRouting_RowCellStyle;
            chkProductRouting.CheckedChanged += ChkProductRouting_CheckedChanged;
            popupReworkRouting.Editor.ButtonClick += Editor_ButtonClick;
        }

        /// <summary>
        /// 재작업 후 공정 그리드 공정 선택시 작업장 재조회
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

            if (rowData["DISPLAYSEQUENCE"].ToString() != "0")
            {
                grdCurrentRouting.View.CheckRow(grdCurrentRouting.View.GetFocusedDataSourceRowIndex(), true);
            }

            RefreshReworkArea();
            grdCurrentRouting.View.CheckStateChanged += CurrentRouting_CheckStateChanged;
        }

        /// <summary>
        /// 재작업 라우팅 그리드 공정 선택시 작업장 재조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReworkRouting_CheckStateChanged(object sender, EventArgs e)
        {
            RefreshArea();
        }

        /// <summary>
        /// LOT 라우팅 현재공정 Row 색상 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentRouting_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (grdCurrentRouting.View.GetRowCellValue(e.RowHandle, "DISPLAYSEQUENCE").ToString() == "1")
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        /// <summary>
        /// 품목라우팅 체크박스 체크상태 변경시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkProductRouting_CheckedChanged(object sender, EventArgs e)
        {
            SearchRoutings();
        }

        /// <summary>
        /// 팝업의 x버튼 클릭시 데이터 초기화
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

        /// <summary>
        /// 적용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            // 라우팅 타입이 품목일때는 체크박스 선택유뮤를 확인한다.
            if (chkProductRouting.Checked == true)
            {
                if (grdReworkRouting.View.GetCheckedRows().Rows.Count == 0)
                {
                    throw MessageException.Create("GridNoChecked");
                }
                else if (string.IsNullOrWhiteSpace(Format.GetString(cboResource.Text)))
                {
                    throw MessageException.Create("대상자원은 필수항목입니다.");
                }
                else
                {
                    DataRow row = grdReworkRouting.View.GetCheckedRows().Rows[0];

                    if (cboResource.EditValue != null)
                    {
                        row["RESOURCEID"] = cboResource.EditValue.ToString().Split('|')[0];
                        row["AREAID"] = cboResource.EditValue.ToString().Split('|')[1];
                        row["RESOURCENAME"] = cboResource.Text;
                    }

                    this.InputSegmentDataEvent(row);
                }
            }

            // 라우팅 타입이 재작업일때는 체크박스가 없다
            else
            {
                // 재작업 라우팅 그리드 확인
                if (grdReworkRouting.View.RowCount == 0)
                {
                    throw MessageException.Create("재작업 라우팅이 선택되지 않았습니다.");
                }
                // 재작업 후 공정 그리드 확인
                else if (grdCurrentRouting.View.GetCheckedRows().Rows.Count == 0)
                {
                    throw MessageException.Create("재작업 후 공정이 선택되지 않았습니다.");
                }
                else if (string.IsNullOrWhiteSpace(Format.GetString(cboResource.Text))
                        || string.IsNullOrWhiteSpace(Format.GetString(cboReworkAfterArea.Text)))
                {
                    throw MessageException.Create("대상자원은 필수항목입니다.");
                }
                else
                {
                    DataTable dt = (grdReworkRouting.DataSource as DataTable);
                    DataTable dt2 = grdCurrentRouting.View.GetCheckedRows();

                    if (!dt.Columns.Contains("ISSKIP"))
                    {
                        dt.Columns.Add("ISSKIP");
                        dt.Columns.Add("LOTID");
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        row["ISSKIP"] = "N";
                        if (cboResource.EditValue != null)
                        {
                            row["RESOURCEID"] = cboResource.EditValue.ToString().Split('|')[0];
                            row["AREAID"] = cboResource.EditValue.ToString().Split('|')[1];
                            row["RESOURCENAME"] = cboResource.Text;
                        }
                    }

                    foreach (DataRow row in dt2.Rows)
                    {
                        row["RESOURCEID"] = cboReworkAfterArea.EditValue.ToString().Split('|')[0];
                        row["AREAID"] = cboReworkAfterArea.EditValue.ToString().Split('|')[1];
                        row["RESOURCENAME"] = cboReworkAfterArea.Text;
                    }

                    this.InputSegmentDataEvent(dt.Rows[0]);
                    this.ReturnSegmentDataEvent(dt2.Rows[0]);
                    this.ReworkRoutingDataEvent(dt);
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            SelectReworkRoutingPopup();
            SelectInputAreaCombobox();
            FirstSetting();

            InitializeGrdReworkRouting();
            InitializeGrdCurrentRouting();
            InitializeCurrentData();                
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 품목라우팅 체크박스 체크상태 변경시 이벤트 처리
        /// </summary>
        private void SearchRoutings()
        {
            // 품목라우팅 선택한경우
            if (chkProductRouting.Checked)
            {
                grdCurrentRouting.Enabled = false;
                grdCurrentRouting.DataSource = null;

                popupReworkRouting.Enabled = false;
                popupReworkRouting.EditValue = string.Empty;

                grdReworkRouting.View.OptionsSelection.MultiSelect = false;
                grdReworkRouting.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

                Dictionary<string, object> param = new Dictionary<string, object>();
                //param.Add("LOTID", CurrentDataRow["LOTID"]);
                param.Add("LOTID", CurrentDataRow["PARENTLOTID"]);
                param.Add("CANCELCODE", CurrentDataRow["CANCELCODE"]);
                param.Add("PROCESSDEFID", CurrentDataRow["PARENTPROCESSDEFID"]);
                param.Add("PROCESSDEFVERSION", CurrentDataRow["PARENTPROCESSDEFVERSION"]);
                param.Add("PROCESSSEGMENTID", CurrentDataRow["PROCESSSEGMENTID"]);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                DataTable currentRouting = SqlExecuter.Query("GetProductRoutingPreviousProcessPaths", "10002", param);
                grdReworkRouting.DataSource = currentRouting;
                RefreshArea();
            }
            // 재작업라우팅 선택한경우
            else
            {
                grdReworkRouting.View.OptionsSelection.MultiSelect = false;
                grdReworkRouting.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

                grdCurrentRouting.Enabled = true;
                grdReworkRouting.DataSource = null;
                popupReworkRouting.Enabled = true;
                SearchCurrentRouting();
                RefreshArea();
            }
        }

        /// <summary>
        /// LOT 라우팅 조회
        /// </summary>
        private void SearchCurrentRouting()
        {
            grdCurrentRouting.Enabled = true;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", CurrentDataRow["LOTID"]);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable currentRouting = SqlExecuter.Query("GetCurrentRoutingByLot", "10002", param);
            grdCurrentRouting.DataSource = currentRouting;
            //CheckDefaultReturnRouting();
        }

        /// <summary>
        /// 팝업 호출시 취소사유에 따라 최초세팅
        /// </summary>
        private void FirstSetting()
        {
            // 취소사유가 불량취소 또는 양품인계일 경우 품목라우팅만 세팅
            if (CurrentDataRow["CANCELCODE"].Equals("Retest") || CurrentDataRow["CANCELCODE"].Equals("TakeoverGoods"))
            {
                chkProductRouting.Checked = true;
                chkProductRouting.Enabled = false;
            }
            // 취소사유가 재작업일경우 품목, 재작업라우팅 세팅
            else
            {

            }
        }

        #endregion

        #region Popup

        /// <summary>
        /// 재작업 라우팅 선택 커스텀 팝업
        /// </summary>
        private void SelectReworkRoutingPopup()
        {
            ReworkRoutingPopup reworkPopup = new ReworkRoutingPopup();
            ConditionItemSelectPopup reworkRoutingSelectPopup = new ConditionItemSelectPopup();
            reworkRoutingSelectPopup.CustomPopup = reworkPopup;
            reworkRoutingSelectPopup.Id = "REWORKROUTING";
            reworkRoutingSelectPopup.ValueFieldName = "REWORKNUMBER";
            reworkRoutingSelectPopup.DisplayFieldName = "REWORKNAME";
            reworkRoutingSelectPopup.SetPopupCustomParameter((popup, dataRow) =>
            {
                ReworkRoutingPopup rpop = popup as ReworkRoutingPopup;
                rpop.LotId = CurrentDataRow["PARENTLOTID"].ToString();
                rpop.FormClosed += Rpop_FormClosed;
            });
            popupReworkRouting.Editor.SelectPopupCondition = reworkRoutingSelectPopup;
        }

        /// <summary>
        /// 재작업 라우팅 커스텀 팝업 닫을때 바인딩
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
                popupReworkRouting.Editor.SetValue(processDefID);
                popupReworkRouting.Editor.Text = row["REWORKNAME"].ToString();
                txtRoutingVersion.Text = processDefVersion;
                RefreshReworkRoutingPath(processDefID, processDefVersion);
                RefreshArea();
            }
        }

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
            Param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dtProcessPath = SqlExecuter.Query("GetProcessPathList", "10021", Param);
            grdReworkRouting.DataSource = dtProcessPath;
            grdReworkRouting.Enabled = true;
        }

        /// <summary>
        /// 작업장 정보 재조회
        /// </summary>
        private void RefreshArea()
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
                    cboReworkAfterArea.Editor.DataSource = null;
                    cboReworkAfterArea.Enabled = false;

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
                        param.Add("PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"].ToString());
                        param.Add("PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"].ToString());
                        param.Add("PROCESSDEFID", repositionProcess.Rows[0]["PROCESSDEFID"].ToString());
                        param.Add("PROCESSDEFVERSION", repositionProcess.Rows[0]["PROCESSDEFVERSION"].ToString());
                        param.Add("PROCESSSEGMENTID", repositionProcess.Rows[0]["PROCESSSEGMENTID"].ToString());
                        param.Add("PROCESSSEGMENTVERSION", repositionProcess.Rows[0]["PROCESSSEGMENTVERSION"].ToString());
                        param.Add("PROCESSPATHID", repositionProcess.Rows[0]["PROCESSPATHID"].ToString());
                        param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                        DataTable dtAreaList = SqlExecuter.Query("GetTransitAreaList", "10022", param);
                        cboResource.Editor.DataSource = dtAreaList;
                        cboResource.Enabled = true;

                        for (int i = 0; i < dtAreaList.Rows.Count; i++)
                        {
                            DataRow dr = dtAreaList.Rows[i];         
                    
                            // 불량처리된 작업장을 기본자원으로 설정
                            if (CurrentDataRow["AREAID"].Equals(dr["AREAID"]))
                            {
                                cboResource.Editor.ItemIndex = i;
                                break;
                            }                         

                            // 불량처리된 작업장이 존재하지 않는다면 Primary가 Y로 설정된 작업장을 기본자원으로 설정
                            if (string.IsNullOrWhiteSpace(Format.GetString(cboResource.Text)))
                            {
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
                    param.Add("PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"].ToString());
                    param.Add("PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"].ToString());
                    param.Add("PROCESSDEFID", dtProcessPath.Rows[0]["PROCESSDEFID"].ToString());
                    param.Add("PROCESSDEFVERSION", dtProcessPath.Rows[0]["PROCESSDEFVERSION"].ToString());
                    param.Add("PROCESSSEGMENTID", dtProcessPath.Rows[0]["PROCESSSEGMENTID"].ToString());
                    param.Add("PROCESSSEGMENTVERSION", dtProcessPath.Rows[0]["PROCESSSEGMENTVERSION"].ToString());
                    param.Add("PROCESSPATHID", dtProcessPath.Rows[0]["PROCESSPATHID"].ToString());
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable dtAreaList = SqlExecuter.Query("GetTransitAreaList", "10022", param);
                    cboResource.Editor.DataSource = dtAreaList;
                    cboResource.Enabled = true;

                    for (int i = 0; i < dtAreaList.Rows.Count; i++)
                    {
                        DataRow dr = dtAreaList.Rows[i];

                        // 불량처리된 작업장을 기본자원으로 설정
                        if (CurrentDataRow["AREAID"].Equals(dr["AREAID"]))
                        {
                            cboResource.Editor.ItemIndex = i;
                            break;
                        }

                        // 불량처리된 작업장이 존재하지 않는다면 Primary가 Y로 설정된 작업장을 기본자원으로 설정
                        if (string.IsNullOrWhiteSpace(Format.GetString(cboReworkAfterArea.Text)))
                        {
                            if (Format.GetFullTrimString(dr["ISPRIMARYRESOURCE"]).Equals("Y"))
                            {
                                cboResource.Editor.ItemIndex = i;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 재작업 후 작업장 정보 재조회
        /// </summary>
        private void RefreshReworkArea()
        {
            DataTable dtProcessPath = grdCurrentRouting.DataSource as DataTable;
            if (dtProcessPath == null || dtProcessPath.Rows.Count == 0 || chkProductRouting.Checked)
            {
                cboReworkAfterArea.Editor.DataSource = null;
                cboReworkAfterArea.Enabled = false;
            }
            else
            {
                DataTable returnProcess = grdCurrentRouting.View.GetCheckedRows();

                if (returnProcess.Rows.Count == 0)
                {
                    cboReworkAfterArea.Editor.DataSource = null;
                    cboReworkAfterArea.Enabled = false;
                }
                else
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                    param.Add("PLANTID", UserInfo.Current.Plant);
                    param.Add("PRODUCTDEFID", CurrentDataRow["PRODUCTDEFID"].ToString());
                    param.Add("PRODUCTDEFVERSION", CurrentDataRow["PRODUCTDEFVERSION"].ToString());
                    param.Add("PROCESSDEFID", returnProcess.Rows[0]["PROCESSDEFID"].ToString());
                    param.Add("PROCESSDEFVERSION", returnProcess.Rows[0]["PROCESSDEFVERSION"].ToString());
                    param.Add("PROCESSSEGMENTID", returnProcess.Rows[0]["PROCESSSEGMENTID"].ToString());
                    param.Add("PROCESSSEGMENTVERSION", returnProcess.Rows[0]["PROCESSSEGMENTVERSION"].ToString());
                    param.Add("PROCESSPATHID", returnProcess.Rows[0]["PROCESSPATHID"].ToString());
                    param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                    DataTable dtResourceList = SqlExecuter.Query("GetTransitAreaList", "10022", param);
                    cboReworkAfterArea.Editor.DataSource = dtResourceList;
                    cboReworkAfterArea.Enabled = true;

                    for (int i = 0; i < dtResourceList.Rows.Count; i++)
                    {
                        DataRow dr = dtResourceList.Rows[i];

                        // 불량처리된 작업장을 기본자원으로 설정
                        if (CurrentDataRow["AREAID"].Equals(dr["AREAID"]))
                        {
                            cboReworkAfterArea.Editor.ItemIndex = i;
                            break;
                        }

                        // 불량처리된 작업장이 존재하지 않는다면 Primary가 Y로 설정된 작업장을 기본자원으로 설정
                        if (string.IsNullOrWhiteSpace(Format.GetString(cboReworkAfterArea.Text)))
                        {
                            if (Format.GetFullTrimString(dr["ISPRIMARYRESOURCE"]).Equals("Y"))
                            {
                                cboReworkAfterArea.Editor.ItemIndex = i;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region ComboBox

        /// <summary>
        /// 콤보박스 초기화
        /// </summary>
        private void SelectInputAreaCombobox()
        {
            // 투입 작업장
            cboResource.Editor.DisplayMember = "RESOURCENAME";
            cboResource.Editor.ValueMember = "RESOURCE";
            cboResource.Editor.ShowHeader = false;
            cboResource.Editor.UseEmptyItem = true;

            // 재작업 후 작업장
            cboReworkAfterArea.Editor.DisplayMember = "RESOURCENAME";
            cboReworkAfterArea.Editor.ValueMember = "RESOURCE";
            cboReworkAfterArea.Editor.ShowHeader = false;
            cboReworkAfterArea.Editor.UseEmptyItem = true;
        }

        #endregion
    }
}
