#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

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

using DevExpress.XtraGrid.Views.Base;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > Lot Split
    /// 업  무  설  명  : Lot Split
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class LotSplit : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |
        private int panelPerQty = 0;
        private int panelQty = 0;
        private int qty = 0;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public LotSplit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Form_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeGrid();
            InitializeControls();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            UseAutoWaitArea = false;

            InitializeEvent();
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // Lot
            CommonFunction.AddConditionBriefLotPopup("P_LOTID", 0.1, true, Conditions);
            // 품목
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);
            // 작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.5, false, Conditions, false, true);
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            Conditions.GetControl<SmartComboBox>("P_LOTPRODUCTTYPESTATUS").EditValue = "Production";
        }
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("UOMCLASSID", "Segment");

            cboUOM.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUOM.Editor.ShowHeader = false;
            cboUOM.Editor.ValueMember = "UOMDEFID";
            cboUOM.Editor.DisplayMember = "UOMDEFNAME";
            cboUOM.Editor.UseEmptyItem = true;
            cboUOM.Editor.EmptyItemValue = "";
            cboUOM.Editor.EmptyItemCaption = "";
            cboUOM.Editor.DataSource = SqlExecuter.Query("GetUomDefinitionList", "10001", param);
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - 재공 Grid 설정 |
            grdWIP.GridButtonItem = GridButtonItem.None;

            grdWIP.View.SetIsReadOnly();

            // CheckBox 설정
            //this.grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var groupDefaultCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupDefaultCol.AddTextBoxColumn("PROCESSCLASSID_R", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("REWORKDIVISION", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 300);
            groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("AREANAME", 150);
            groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
            groupDefaultCol.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            groupDefaultCol.AddTextBoxColumn("STATE", 70).SetTextAlignment(TextAlignment.Center);

            var groupWipCol = grdWIP.View.AddGroupColumn("WIPQTY");
            groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupReceiveCol = grdWIP.View.AddGroupColumn("WAITFORRECEIVE");
            groupReceiveCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupReceiveCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkStartCol = grdWIP.View.AddGroupColumn("ACCEPT");
            groupWorkStartCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkStartCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
            groupWorkEndCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupWorkEndCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupSendCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
            groupSendCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            groupSendCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            var groupWIPCol = grdWIP.View.AddGroupColumn("WIPLIST");
            groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
            groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
            groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);

            grdWIP.View.PopulateColumns();

            #endregion

            #region - Target Grid |
            grdTarget.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            //grdTarget.View.SetIsReadOnly();

            // CheckBox 설정
            this.grdTarget.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdTarget.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdTarget.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetValidationIsRequired();
            grdTarget.View.AddSpinEditColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetValidationIsRequired();
            grdTarget.View.AddComboBoxColumn("REASONCODEID", 120, new SqlQuery("GetReasonCodeList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_REASONCODECLASSID=LotSplit"), "REASONCODENAME", "REASONCODEID")
                .SetLabel("SPLITREASON");
            // 재작업 Routing CodeHelp
            grdTarget.View.AddButtonColumn("REWORKROUTING", 120).SetTextAlignment(TextAlignment.Center);
            grdTarget.View.AddTextBoxColumn("PROCESSDEFID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("PROCESSDEFVER", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("PROCESSSEGMENTID", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 120).SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("PROCESSPATHID", 120).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("PATHSEQUENCE", 120).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("RETURNPROCESSSEGMENTID", 120).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("RETURNPATHSEQUENCE", 120).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("REWORKTYPE", 120).SetIsHidden();
            grdTarget.View.AddTextBoxColumn("RETURNPROCESSPATHID", 120).SetIsHidden();
            // 자원
            /*grdTarget.View.AddComboBoxColumn("RESOURCEID", 200, new SqlQuery("GetResourceInBORByProcessDefId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"RESOURCETYPE=RESOURCE"), "RESOURCENAME", "RESOURCEID")
                .SetLabel("RESOURCEID");*/
            grdTarget.View.AddTextBoxColumn("RESOURCENAME", 200).SetIsReadOnly();
            grdTarget.View.AddTextBoxColumn("RETURNRESOURCENAME", 200).SetIsReadOnly();

            grdTarget.View.AddTextBoxColumn("RESOURCEID").SetIsReadOnly().SetIsHidden();
            grdTarget.View.AddTextBoxColumn("RETURNRESOURCEID").SetIsReadOnly().SetIsHidden();

            grdTarget.View.PopulateColumns();
            #endregion

        }
        #endregion

        #region ▶ 화면 Control 설정 |
        /// <summary>
        /// 화면 Control 설정
        /// </summary>
        private void InitializeControls()
        {
            grdWIP.Height = 450;
        }
        #endregion

        #endregion

        #region ◆ Event |

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += Form_Load;

            // Grid Event
            grdWIP.View.DoubleClick += View_DoubleClick;
            grdWIP.View.RowStyle += WIP_RowStyle;

            grdTarget.View.AddingNewRow += View_AddingNewRow;
            grdTarget.View.CellValueChanged += View_CellValueChanged;
            grdTarget.View.GridCellButtonClickEvent += TargetView_GridCellButtonClickEvent;
            cboUOM.Editor.EditValueChanged += Editor_EditValueChanged;
        }

        #region ▶ Grid Event |

        #region - 재공 Grid 더블클릭 |
        /// <summary>
        /// 재공 Grid 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = grdWIP.View.GetFocusedDataRow();

            if (dr == null) return;

            if (Format.GetFullTrimString(dr["REWORKDIVISION"]).Equals("Rework"))
            {
                ShowMessage(MessageBoxButtons.OK, "LotSplitCheckRework");
                this.grdTarget.DataSource = null;

                grdLotInfo.ClearData();
                return;
            }
            // LOT 정보 조회
            string strLotid = dr["LOTID"].ToString();

            getLotData(strLotid);
        }
        #endregion
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            string uom = Format.GetFullTrimString(cboUOM.Editor.EditValue);

            if (uom.Equals("PNL"))
            {
                grdTarget.View.Columns["QTY"].OptionsColumn.ReadOnly = true;
                grdTarget.View.Columns["PANELQTY"].OptionsColumn.ReadOnly = false;
            }
            else if (uom.Equals("PCS"))
            {
                grdTarget.View.Columns["PANELQTY"].OptionsColumn.ReadOnly = true;
                grdTarget.View.Columns["QTY"].OptionsColumn.ReadOnly = false;
            }
        }
        #region - 재공 Row Style Event |
        /// <summary>
        /// 재공 Row Stype Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WIP_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            int rowIndex = grdWIP.View.FocusedRowHandle;

            if (rowIndex == e.RowHandle)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }
        #endregion

        #region - Split Grid AddRow |
        /// <summary>
        /// Split Grid AddRow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            if (grdLotInfo.DataSource == null || string.IsNullOrWhiteSpace(grdLotInfo.GetFieldValue("LOTID").ToString())) return;

            if (cboUOM.EditValue == null || string.IsNullOrWhiteSpace(cboUOM.GetValue().ToString()))
            {
                // UOM 선택하여 주십시오.
                MSGBox.Show(MessageBoxType.Warning, "SelectUOM");

                // 추가 Row 제거
                grdTarget.View.RemoveRow(sender.GetFocusedDataSourceRowIndex());
                return;
            }

            grdTarget.View.SetFocusedRowCellValue("LOTID", "");
            grdTarget.View.SetFocusedRowCellValue("PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
            grdTarget.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
            grdTarget.View.SetFocusedRowCellValue("PRODUCTDEFNAME", grdLotInfo.GetFieldValue("PRODUCTDEFNAME"));
            grdTarget.View.SetFocusedRowCellValue("UNIT", cboUOM.GetValue().ToString());
        }
        #endregion

        #region - 분할 수량 입력 Cell Value Change |
        /// <summary>
        /// 분할 수량 입력 Cell Value Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "QTY")
            {
                grdTarget.View.CellValueChanged -= View_CellValueChanged;

                decimal lotQty = decimal.Parse(grdLotInfo.GetFieldValue("PCSQTY").ToString());

                decimal qty = Format.GetDecimal(e.Value);

                DataTable dt = grdTarget.DataSource as DataTable;

                decimal totalPnlQty = dt.AsEnumerable().Where(c => c.Field<decimal?>("QTY") != null).Sum(r => r.Field<decimal>("QTY"));

                if (lotQty <= totalPnlQty)
                {
                    grdTarget.View.SetRowCellValue(e.RowHandle, "QTY", null);
                    grdTarget.View.SetRowCellValue(e.RowHandle, "PANELQTY", null);

                    // 분할 수량 
                    MSGBox.Show(MessageBoxType.Warning, "SplitQtyLessThanParentQty");

                    grdTarget.View.CellValueChanged += View_CellValueChanged;
                    return;
                }
                decimal panelPerQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PANELPERQTY"));

                decimal pnlQty = 0;
                if (panelPerQty > 0)
                {
                    pnlQty = qty / panelPerQty;
                }
                grdTarget.View.SetRowCellValue(e.RowHandle, "PANELQTY", Math.Ceiling(pnlQty));

                grdTarget.View.CellValueChanged += View_CellValueChanged;
            }
            else if (e.Column.FieldName == "PANELQTY")
            {
                grdTarget.View.CellValueChanged -= View_CellValueChanged;

                decimal lotQty = decimal.Parse(grdLotInfo.GetFieldValue("PNLQTY").ToString());

                decimal pnlqty = Format.GetDecimal(e.Value);

                DataTable dt = grdTarget.DataSource as DataTable;

                decimal totalPnlQty = dt.AsEnumerable().Where(c => c.Field<decimal?>("PANELQTY") != null).Sum(r => r.Field<decimal>("PANELQTY"));

                if (lotQty <= totalPnlQty)
                {
                    grdTarget.View.SetRowCellValue(e.RowHandle, "QTY", null);
                    grdTarget.View.SetRowCellValue(e.RowHandle, "PANELQTY", null);

                    // 분할 수량 
                    MSGBox.Show(MessageBoxType.Warning, "SplitQtyLessThanParentQty");

                    grdTarget.View.CellValueChanged += View_CellValueChanged;

                    return;
                }
                decimal panelPerQty = decimal.Parse(grdLotInfo.GetFieldValue("PANELPERQTY").ToString());

                decimal qty = pnlqty * panelPerQty;

                grdTarget.View.SetRowCellValue(e.RowHandle, "QTY", qty);

                grdTarget.View.CellValueChanged += View_CellValueChanged;
            }
            else if (e.Column.FieldName.Equals("REASONCODEID"))
            {
                if (grdTarget.View.GetRowCellValue(e.RowHandle, "REASONCODEID").Equals("SplitRework"))
                {
                    GetReworkRouting();
                }
                else
                {
                    grdTarget.View.SetFocusedRowCellValue("PROCESSDEFID", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("PROCESSDEFVER", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("PROCESSSEGMENTID", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("PROCESSSEGMENTNAME", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("PROCESSPATHID", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("REWORKTYPE", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("RESOURCEID", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("RESOURCENAME", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("RETURNRESOURCEID", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("RETURNRESOURCENAME", string.Empty);
                    grdTarget.View.SetFocusedRowCellValue("RETURNPROCESSPATHID", string.Empty);
                }
            }
        }
        #endregion

        #region - 재작업 Routing 버튼 클릭 |
        /// <summary>
        /// 재작업 Routing 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TargetView_GridCellButtonClickEvent(SmartBandedGridView sender, GridCellButtonClickEventArgs args)
        {
            if (!args.FieldName.Equals("REWORKROUTING"))
                return;

            DataRow dr = args.CurrentRow;

            if (dr["REASONCODEID"].ToString().Equals("SplitRework"))
            {
                GetReworkRouting();
            }
        }
        #endregion

        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
            string lotId = Format.GetString(grdWIP.View.GetFocusedRowCellValue("LOTID"));




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


            SaveRule();

            // 데이터 초기화
            SetInitControl();
        }

        #region ▶ 데이터 저장 |
        /// <summary>
        /// 데이터 저장
        /// </summary>
        private void SaveRule()
        {
            // TODO : 저장 Rule 변경

            DataTable targetList = grdTarget.DataSource as DataTable;

            if (grdLotInfo == null || string.IsNullOrWhiteSpace(grdLotInfo.GetFieldValue("LOTID").ToString()))
            {
                // LOT을 선택하여 주십시오.
                throw MessageException.Create("NoSeletedLot");
            }

            if (cboUOM.EditValue == null || string.IsNullOrWhiteSpace(cboUOM.GetValue().ToString()))
            {
                // UOM 선택하여 주십시오.
                MSGBox.Show(MessageBoxType.Warning, "SelectUOM");
                return;
            }

            if (targetList == null || targetList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            //if (targetList.AsEnumerable().Count(r => !r.IsNull("QTY")).ToInt32() == 0 && 
            //    targetList.AsEnumerable().Count(r => !r.IsNull("PANELQTY")).ToInt32() == 0)
            if (targetList.AsEnumerable().Count(r => Format.GetDecimal(r["QTY"]) <= 0) > 0 ||
                targetList.AsEnumerable().Count(r => Format.GetDecimal(r["PANELQTY"]) <= 0) > 0)
            {
                // 분할수량을 입력하여 주십시오.
                throw MessageException.Create("NotSplitQty");
            }

            if (targetList.AsEnumerable().Count(r => !r.IsNull("REASONCODEID")).ToInt32() == 0)
            {
                // 분할사유를 선택하여 주십시오.
                throw MessageException.Create("NotSplitReason");
            }

            // 분할수량 체크 :: 모 LOT 기준 자LOT의 총 수량 체크
            decimal lotQty = 0;
            decimal lotPnlQty = 0;
            decimal totalQty = 0;
            decimal totalPnlQty = 0;

            //lotQty = decimal.Parse(grdLotInfo.GetFieldValue("PCSQTY").ToString());
            lotQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PCSQTY"));
            lotPnlQty = Format.GetDecimal(grdLotInfo.GetFieldValue("PNLQTY"));
            totalQty = targetList.AsEnumerable().Sum(r => Format.GetDecimal(r["QTY"]));
            totalPnlQty = targetList.AsEnumerable().Sum(r => Format.GetDecimal(r["PANELQTY"]));

            if (lotQty <= totalQty)
            {
                // 분할 수량 
                throw MessageException.Create("SplitQtyLessThanParentQty");
            }

            // 재작업 ROUTING인 경우 재작업 ROUTING 선택 및 작업장 설정 여부 체크
            if (targetList.AsEnumerable().Count(r => r.Field<string>("REASONCODEID").Equals("SplitRework")) > 0)
            {
                // PROCESSDEFID
                if (targetList.AsEnumerable().Count(r => r.Field<string>("PROCESSDEFID").Equals("") && r.Field<string>("REASONCODEID").Equals("SplitRework")) > 0)
                {
                    // 라우팅 정보를 찾을수 없습니다.({0})
                    throw MessageException.Create("checkProcessDef");
                }

                if (targetList.AsEnumerable().Count(r => string.IsNullOrWhiteSpace(r.Field<string>("RESOURCEID")) && r.Field<string>("REASONCODEID").Equals("SplitRework")) > 0)
                {
                    // 선택된 자원이 없습니다.
                    throw MessageException.Create("NoResourceSelected");
                }
            }

            DataTable resultData = null;

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker worker = new MessageWorker("SaveLotSplit");
                worker.SetBody(new MessageBody()
                {
                    { "EnterpriseID", UserInfo.Current.Enterprise },
                    { "PlantID", UserInfo.Current.Plant },
                    { "UserId", UserInfo.Current.Id },
                    { "LotId", grdLotInfo.GetFieldValue("LOTID").ToString() },
                    { "Lotlist", targetList }
                });

                //worker.Execute();

                var result = worker.Execute<DataTable>();
                resultData = result.GetResultSet();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }

            string NormalLotlist = string.Empty;
            string ReworkLotList = string.Empty;

            // Print Lot Card
            for (int i = 0; i < resultData.Rows.Count; i++)
            {
                string strLot = Format.GetFullTrimString(resultData.Rows[i]["LOTID"]);
                string strRework = Format.GetFullTrimString(resultData.Rows[i]["REWORKTYPE"]);

                if (strRework.Equals("REWORK"))
                {
                    ReworkLotList += strLot + ",";
                }
                else
                {
                    NormalLotlist += strLot + ",";
                }
            }
            NormalLotlist = NormalLotlist.TrimEnd(',');
            ReworkLotList = ReworkLotList.TrimEnd(',');

            //pnlContent.ShowWaitArea();

            if (!string.IsNullOrEmpty(NormalLotlist))
            {
                CommonFunction.PrintLotCard_Ver2(NormalLotlist, LotCardType.Normal);
            }
            if (!string.IsNullOrEmpty(ReworkLotList))
            {
                CommonFunction.PrintLotCard_Ver2(ReworkLotList, LotCardType.Rework);
            }

            if (resultData.Rows.Count < 1)
            {
                // LOT CARD를 출력할 Lot을 선택하시기 바랍니다.
                ShowMessage("");
                return;
            }

            //pnlContent.CloseWaitArea();
        }
        #endregion

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // 기존 Grid Data 초기화
            SetInitControl();

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("P_PROCESSSTATE", "WaitForSend,Wait");

            DataTable dt = await SqlExecuter.QueryAsync("SelectWIPList", "10001", values);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWIP.DataSource = dt;
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Lot 정보 조회 |
        /// <summary>
        /// Lot 정보 조회
        /// </summary>
        /// <param name="strLotId"></param>
        private void getLotData(string strLotId)
        {
            if (string.IsNullOrWhiteSpace(strLotId)) return;

            this.grdLotInfo.ClearData();
            this.grdTarget.DataSource = null;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", strLotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoBylotID", "10001", param);

            var islock = lotInfo.AsEnumerable().Where(r => r.Field<string>("ISLOCKING").Equals("Y")).Count();

            if (islock > 0)
            {
                // Locking 상태의 Lot 입니다. {0}
                throw MessageException.Create("LotIsLocking");
            }

            if (lotInfo.Rows.Count < 1)
            {
                // 조회할 데이터가 없습니다.
                ShowMessage("NoSelectData");
                grdLotInfo.ClearData();

                return;
            }

            // Lot정보 Data Binding
            grdLotInfo.DataSource = lotInfo;

            this.cboUOM.EditValue = lotInfo.AsEnumerable().FirstOrDefault()["UOM"].ToString();
        }
        #endregion

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetInitControl()
        {
            // Data 초기화
            this.grdWIP.DataSource = null;
            this.grdTarget.DataSource = null;

            grdLotInfo.ClearData();
        }
        #endregion

        #region ▶ 재작업 Routing 팝업창 호출 |
        /// <summary>
        /// 재작업 Routing 팝업창 호출
        /// </summary>
        private void GetReworkRouting()
        {
            Micube.SmartMES.Commons.Controls.ReworkRoutingPop pop = new Micube.SmartMES.Commons.Controls.ReworkRoutingPop();
            pop.ProductDefID = grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString();
            pop.ProductDefVersion = grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString();
            pop.PathSequence = grdLotInfo.GetFieldValue("USERSEQUENCE").ToString();
            pop.ProcessSegmentId = grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString();
            pop.LotId = grdLotInfo.GetFieldValue("LOTID").ToString();
            pop.ShowDialog();

            if (!string.IsNullOrWhiteSpace(pop.ProcessDefId))
            {
                grdTarget.View.SetFocusedRowCellValue("PROCESSDEFID", pop.ProcessDefId);
                grdTarget.View.SetFocusedRowCellValue("PROCESSDEFVER", pop.ProcessDefVersion);
                grdTarget.View.SetFocusedRowCellValue("PROCESSSEGMENTID", pop.ProcessSegmentId);
                grdTarget.View.SetFocusedRowCellValue("PROCESSSEGMENTNAME", pop.ProcessSegmentName);
                grdTarget.View.SetFocusedRowCellValue("PROCESSPATHID", pop.ProcessPathId);
                grdTarget.View.SetFocusedRowCellValue("PATHSEQUENCE", pop.PathSequence);
                grdTarget.View.SetFocusedRowCellValue("RETURNPROCESSSEGMENTID", pop.ReturnProcessSegmentId);
                grdTarget.View.SetFocusedRowCellValue("RETURNPATHSEQUENCE", pop.ReturnPathSequence);
                grdTarget.View.SetFocusedRowCellValue("REWORKTYPE", pop.ReworkType);
                grdTarget.View.SetFocusedRowCellValue("RESOURCEID", pop.ResourceId);
                grdTarget.View.SetFocusedRowCellValue("RESOURCENAME", pop.ResourceName);
                grdTarget.View.SetFocusedRowCellValue("RETURNRESOURCEID", pop.ReturnResourceId);
                grdTarget.View.SetFocusedRowCellValue("RETURNRESOURCENAME", pop.ReturnResourceName);
                if (pop.ReworkType == "REWORK")
                {
                    grdTarget.View.SetFocusedRowCellValue("RETURNPROCESSPATHID", pop.ReturnPathId);
                }
                else
                {
                    grdTarget.View.SetFocusedRowCellValue("RETURNPROCESSPATHID", pop.ProcessPathId);
                }
            }

            DataRow selectedRow = grdTarget.View.GetFocusedDataRow();

            string strProcessDefID = selectedRow["PRODUCTDEFID"].ToString();
            string strProcessDefVerNo = selectedRow["PRODUCTDEFVERSION"].ToString();
            string strSegmentID = selectedRow["PROCESSSEGMENTID"].ToString();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("RESOURCETYPE", "RESOURCE");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("PROCESSDEFID", pop.ProcessDefId);
            param.Add("PROCESSDEFVERSION", pop.ProcessDefVersion);
            param.Add("PROCESSSEGMENTID", pop.ProcessSegmentId);

            // TODO : 아래 삭제
            // 작업장 Combo조회
            // grdTarget.View.RefreshComboBoxDataSource("RESOURCEID", new SqlQuery("GetTransitAreaList", "10022", param));
        }
        #endregion

        #endregion
    }
}
