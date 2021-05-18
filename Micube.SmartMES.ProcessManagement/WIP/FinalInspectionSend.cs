#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.Framework.SmartControls.Grid;

using Micube.SmartMES.Commons;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.Utils;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 최종검사 인계
    /// 업  무  설  명  : 최종검사 인계처리
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-30
    /// 수  정  이  력  : 2019-12-16, 박정훈, 후공정이 출하인 경우에 한하여 LOT분할 활성화 (By 박주하 주임)
    ///                  2020 -03-07, 강유라 출하 - 최종 재작업 로직 추가
    /// 
    /// 
    /// </summary>
    public partial class FinalInspectionSend : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가
        private decimal _panelPerQty = 0;
        private decimal _panelQty = 0;
        private decimal _qty = 0;
        #endregion

        #region ◆ 생성자 |

        /// <summary>
        /// 생성자
        /// </summary>
        public FinalInspectionSend()
        {
            InitializeComponent();
        }

        #endregion

        #region ◆ 컨텐츠 영역 초기화 |

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            UseAutoWaitArea = false;

            // TODO : 컨트롤 초기화 로직 구성
            InitializeControls();

            InitializeEvent();

            // Grid 초기화
            InitializeGrid();

            // ComboBox 초기화
            InitializeComboBox();
        }

        #region ▶ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void InitializeControls()
        {
            ClearDetailInfo();

            // LOT 정보 GRID
            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "PRODUCTDEFVERSION", 
                                        "PRODUCTTYPE", "DEFECTUNIT", "PCSPNL", "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE"
                                        , "ISRCLOT", "PROCESSSEGMENTCLASSID", "PROCESSUOM", "PCSARY", "SUBPROCESSDEFID", "LOTTYPE"
                                        , "STEPTYPE", "MATERIALCLASS", "TRACKINUSER", "ISPRINTRCLOTCARD", "ISPRINTLOTCARD", "PROCESSSEGMENTTYPE"
                                        , "PCSPNL", "PROCESSSTATE", "PROCESSPATHID", "NEXTPROCESSSEGMENTID", "NEXTPROCESSSEGMENTVERSION"
                                        , "PLANTID", "INPUTDATE", "PRODUCTDEFTYPE", "PRODUCTIONTYPE","ISREWORK","ISHOLD","ISLOCKING","RESOURCEID"    
                                        ,  "TRACKINUSERNAME","AREAID"
                                        );
            grdLotInfo.Enabled = false;

            #region - 작업장 |
            // 작업장
            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"PLANTID={UserInfo.Current.Plant}", "ISMODIFY=Y", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
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
            #endregion

            // 모 LOT 수량 초기화
            this.txtParentLotQty.Text = "0";
        }
        #endregion

        #region ▶ Grid 초기화 |
        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - Lot Grid |
            grdLotList.GridButtonItem = GridButtonItem.None;

            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLotList.View.AddTextBoxColumn("PARENTLOT", 200).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("ISPARENT", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("SPLITQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdLotList.View.AddTextBoxColumn("PANELPERQTY", 80).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("INSPECTORNAME", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            grdLotList.View.PopulateColumns();

            #endregion

            #region - Split Grid |
            grdLotSplit.GridButtonItem = GridButtonItem.Delete;

            // CheckBox 설정
            grdLotSplit.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLotSplit.View.AddTextBoxColumn("PARENTLOTID", 200).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotSplit.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotSplit.View.AddTextBoxColumn("ISPARENT", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdLotSplit.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotSplit.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotSplit.View.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();
            grdLotSplit.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotSplit.View.AddSpinEditColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdLotSplit.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdLotSplit.View.PopulateColumns();

            #endregion

            #region - Defect Grid |
            grdDefect.VisibleLotId = true;
            grdDefect.VisibleTopDefectCode = true;
            grdDefect.InitializeControls();
            grdDefect.InitializeEvent();
            #endregion

            #region - Lot Message |

            this.ucMessage.InitializeControls();

            #endregion

            InitializationSummaryRow();

            #region - 특기사항 |
            grdComment.GridButtonItem = GridButtonItem.None;
            grdComment.ShowButtonBar = false;
            grdComment.ShowStatusBar = false;

            grdComment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdComment.View.SetIsReadOnly();

            // 공정수순ID
            grdComment.View.AddTextBoxColumn("PROCESSPATHID", 150).SetValidationKeyColumn().SetIsHidden();
            // 공정수순
            grdComment.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            // 공정명
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 특기사항
            grdComment.View.AddTextBoxColumn("DESCRIPTION", 500).SetLabel("REMARKS");
            // 현재공정여부
            grdComment.View.AddTextBoxColumn("ISCURRENTPROCESS", 70).SetIsHidden();

            grdComment.View.PopulateColumns();


            grdComment.View.OptionsView.ShowIndicator = false;
            grdComment.View.OptionsView.AllowCellMerge = true;

            grdComment.View.Columns["PROCESSPATHID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["USERSEQUENCE"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTID"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["PROCESSSEGMENTNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["DESCRIPTION"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdComment.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            #endregion

            #region - 공정 SPEC |
            grdProcessSpec.GridButtonItem = GridButtonItem.None;
            grdProcessSpec.ShowButtonBar = false;
            grdProcessSpec.ShowStatusBar = false;

            grdProcessSpec.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessSpec.View.SetIsReadOnly();

            // 공정수순ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSPATHID", 150).SetValidationKeyColumn().SetIsHidden();
            // 공정수순
            grdProcessSpec.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            // 공정명
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 항목
            grdProcessSpec.View.AddTextBoxColumn("SPECCLASSNAME", 120).SetLabel("SPECITEM");
            // 하한값
            grdProcessSpec.View.AddSpinEditColumn("LSL", 90).SetDisplayFormat("#,##0.00");
            // 중간값
            grdProcessSpec.View.AddSpinEditColumn("SL", 90).SetDisplayFormat("#,##0.00");
            // 상한값
            grdProcessSpec.View.AddSpinEditColumn("USL", 90).SetDisplayFormat("#,##0.00");
            // 현재공정여부
            grdProcessSpec.View.AddTextBoxColumn("ISCURRENTPROCESS", 70).SetIsHidden();

            grdProcessSpec.View.PopulateColumns();

            grdProcessSpec.View.OptionsView.ShowIndicator = false;
            grdProcessSpec.View.OptionsView.AllowCellMerge = true;

            grdProcessSpec.View.Columns["SPECCLASSNAME"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["LSL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["SL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["USL"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            grdProcessSpec.View.Columns["ISCURRENTPROCESS"].OptionsColumn.AllowMerge = DefaultBoolean.False;
            #endregion
        }

        /// <summary>
        /// Footer 추가 Panel, PCS 합계 표시 2019.08.01
        /// </summary>
        private void InitializationSummaryRow()
        {
            #region - Lot 목록 |
            grdLotList.View.Columns["PRODUCTDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PRODUCTDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdLotList.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdLotList.View.OptionsView.ShowFooter = true;
            grdLotList.ShowStatusBar = false;
            #endregion

            #region - Lot 분할 Grid |
            grdLotSplit.View.Columns["PRODUCTDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotSplit.View.Columns["PRODUCTDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdLotSplit.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotSplit.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = " ";
            grdLotSplit.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotSplit.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdLotSplit.View.OptionsView.ShowFooter = true;
            grdLotSplit.ShowStatusBar = false;
            #endregion

            #region - Lot Defect Grid |
            grdDefect.View.Columns["DEFECTCODE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["DEFECTCODE"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdDefect.View.Columns["PNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["PNLQTY"].SummaryItem.DisplayFormat = " ";
            grdDefect.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdDefect.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdDefect.View.OptionsView.ShowFooter = true;
            #endregion
        }
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// ComboBox 초기화
        /// </summary>
        private void InitializeComboBox()
        {
            #region - UOM |
            // UOM
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("UOMCLASSID", "Process");

            cboUOM.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboUOM.Editor.ShowHeader = false;
            cboUOM.Editor.ValueMember = "UOMDEFID";
            cboUOM.Editor.DisplayMember = "UOMDEFNAME";
            cboUOM.Editor.UseEmptyItem = true;
            cboUOM.Editor.EmptyItemValue = "";
            cboUOM.Editor.EmptyItemCaption = "";
            cboUOM.Editor.DataSource = SqlExecuter.Query("GetUomDefinitionList", "10001", param);
            #endregion

            cboUOM.EditValue = "PNL";
        }

        #endregion
        #endregion

        #region ◆ Event |

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            // 초기화
            btnInit.Click += BtnInit_Click;
            btnSplit.Click += BtnSplit_Click;

            // TextBox Event
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += Editor_Click;
            txtDefectQty.Editor.TextChanged += DefectText_TextChanged;

            // Grid Event
            grdLotList.View.DoubleClick += LotList_DoubleClick;
            grdLotList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;

            grdLotSplit.View.CellValueChanged += GrdLotSplitView_CellValueChanged;
            grdLotSplit.View.CustomDrawFooterCell += SplitView_CustomDrawFooterCell;
            grdLotSplit.ToolbarDeleteRow += GrdLotSplit_DeleteRow;


            grdDefect.DefectQtyChanged += GrdDefect_DefectQtyChanged;
            grdDefect.View.AddingNewRow += Defect_AddingNewRow;
            grdDefect.Grid.ToolbarDeleteRow += GrdDefect_ToolbarDeletingRow;


            grdDefect.View.CustomDrawFooterCell += DefectView_CustomDrawFooterCell;
        }

        #region ▶ TEXBOX Event |
        /// <summary>
        /// TextBox Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_Click(object sender, EventArgs e)
        {
            if (this.ImeMode != ImeMode.Alpha)
            {
                this.ImeMode = ImeMode.Alpha;
            }
            txtLotId.Editor.SelectAll();
        }

        /// <summary>
        /// LotID KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                if (txtArea.Editor.SelectedData.Count() < 1 || string.IsNullOrWhiteSpace(txtArea.EditValue.ToString()) || string.IsNullOrWhiteSpace(txtLotId.Text))
                {
                    // 작업장, LOT No.는 필수 입력 항목입니다.
                    ShowMessage("AreaLotIdIsRequired");
                    ClearDetailInfo();
                    return;
                }

                ClearDetailInfo();

                //2020-03-07 강유라 순서 변경
                // 최종검사 대상 LOT 조회 :: RootLotID 기준
                string isFinishRework = getLotInfo(CommonFunction.changeArgString(this.txtLotId.Editor.Text));

                // 최종검사 대상 LOT 조회 :: RootLotID 기준
                grdLotList.View.ClearDatas();

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("PLANTID", UserInfo.Current.Plant);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("LOTID", this.txtLotId.Text.Trim());
                param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
                param.Add("PROCESSSTATE", Constants.WaitForSend);
                //2020-03-07 강유라 마지막 재작업여부로 분할lot조회 조건 추가

                //----재작업 Y , subProcess 없는 경우--- 재작업 로직 추가전 최종으로 돌아간 경우
                if(!isFinishRework.Equals("NONE"))
                param.Add("ISREWORK", isFinishRework);

                DataTable dt = SqlExecuter.Query("SelectLotListForFinalInspection", "10001", param);

                if (dt.Rows.Count < 1)
                {
                    DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);
                    ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
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

                // 체공 상태 체크
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

                grdLotList.DataSource = dt;

                

                grdLotInfo.Enabled = true;
                pfsInfo.Enabled = true;
                tabInfo.Enabled = true;

                string lotId = string.Join(",", (grdLotList.DataSource as DataTable).AsEnumerable().Select(c => Format.GetString(c["LOTID"])).Distinct());

                grdDefect.SetConsumableDefComboBox(lotId);
            }
        }

        /// <summary>
        /// Defect 수량변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefectText_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(this.txtDefectQty.Text) && decimal.Parse(this.txtDefectQty.Text) > 0)
            {
                // 불량입력시 Split Data 초기화
                DataTable splitDt = grdLotSplit.DataSource as DataTable;
                if (splitDt != null && splitDt.Rows.Count > 0)
                {
                    for(int i = splitDt.Rows.Count - 1; i >= 0; i--)
                    {
                        grdLotSplit.View.RemoveRow(i);
                    }
                }
            }
        }
        #endregion

        #region ▶ Button Event |
        /// <summary>
        /// 초기화 Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInit_Click(object sender, EventArgs e)
        {
            txtArea.Editor.SetValue(null);
            txtLotId.Text = "";

            ClearDetailInfo();
        }

        /// <summary>
        /// Lot 분할 Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSplit_Click(object sender, EventArgs e)
        {
            //grdLotSplit.View.ClearDatas();

            DataTable dt = grdLotSplit.DataSource as DataTable;

            if (string.IsNullOrWhiteSpace(this.txtParentLotQty.Text) || int.Parse(this.txtParentLotQty.Text) == 0 || dt == null)
            {
                return;
            }

            SplitAddNewRow("Button");
        }
        #endregion

        #region ▶ Grid Event |

        #region - Lot 목록 더블 클릭 |
        /// <summary>
        /// Lot 목록 더블 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LotList_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = grdLotList.View.GetFocusedDataRow();

            if (dr == null) return;

            CommonFunction.SetGridDoubleClickCheck(grdLotList, sender);

            // LOT 정보 조회
            string strLotid = dr["LOTID"].ToString();

            getLotInfo(strLotid);
        }
        #endregion

        #region - LotList Grid Footer Sum Event |
        /// <summary>
        /// Lot Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            GridSum(grdLotList, e);
        }
        #endregion

        #region - Split Lot Grid Footer Sum Event |
        /// <summary>
        /// Split Lot Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplitView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            GridSum(grdLotSplit, e);
        }
        #endregion

        #region - Split Lot Delete Row  |
        /// <summary>
        /// Split Lot Delete Row 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLotSplit_DeleteRow(object sender, EventArgs e)
        {
            SetLotSplitQty();
        }
        #endregion

        #region - Split Grid Cell Value Changed Event |
        /// <summary>
        /// Split Grid Cell Value Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLotSplitView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdLotSplit.View.CellValueChanged -= GrdLotSplitView_CellValueChanged;

            // Panel별 단위 수량 조회
            int pnlperqty = 0;

            DataTable lotInfo = grdLotInfo.DataSource as DataTable;

            if (lotInfo == null) return;

            pnlperqty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PANELPERQTY"].ToString());

            DataTable dt = grdLotSplit.DataSource as DataTable;

            int lotQty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PCSQTY"].ToString());

            if (cboUOM.GetValue() == null || cboUOM.GetValue().Equals(""))
            {
                // UOM을 선택하여 주십시오.
                ShowMessage("SelectUOM");
                return;
            }

            int qty = Format.GetInteger(grdLotSplit.View.GetFocusedValue());
            if (e.Column.FieldName.Equals("QTY"))
                grdLotSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling(Convert.ToDecimal(qty / pnlperqty)));
            else
                grdLotSplit.View.SetFocusedRowCellValue("QTY", qty * pnlperqty);

            var pcsRT = dt.AsEnumerable().Sum(r => Format.GetInteger(r.Field<object>("QTY")));

            if (Format.GetInteger(pcsRT.ToString()) > lotQty)
            {
                // 분할 Lot의 수량은 모 Lot의 수량보다 클 수 없습니다. {0}
                MSGBox.Show(MessageBoxType.Warning, "SplitQtyLessThanParentQty");

                grdLotSplit.View.SetFocusedRowCellValue("PANELQTY", 0);
                grdLotSplit.View.SetFocusedRowCellValue("QTY", 0);

                grdLotSplit.View.CellValueChanged += GrdLotSplitView_CellValueChanged;

                return;
            }

            grdLotSplit.View.CellValueChanged += GrdLotSplitView_CellValueChanged;
        }
        #endregion

        #region - DefectList Grid Footer Sum Event |
        /// <summary>
        /// DefectList Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefectView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            grdDefect.GridSum(e);

            DataTable dt = grdDefect.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count <= 0)
            {
                this.txtGoodQty.Text = _qty.ToString();
                this.txtDefectQty.Text = "0";
            }
        } 
        #endregion

        #region - Defect 수량 Changed Event |
        /// <summary>
        /// Defect 수량 Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefect_DefectQtyChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal pnlQty = 0;
            decimal qty = 0;

            if (e.Column.FieldName.Equals("QTY"))
            {
                qty = Format.GetDecimal(e.Value);
                pnlQty = Math.Ceiling(qty / _panelPerQty);
            }
            else
            {
                pnlQty = Format.GetDecimal(e.Value);
                qty = _panelPerQty * pnlQty;
            }

            SetChangeGoodDefectQty(qty);
        }
        #endregion

        #region - Defect Grid Add New Row Event |
        /// <summary>
        /// Defect Grid Add New Row Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Defect_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataTable dt = grdLotInfo.DataSource as DataTable;

            if (dt == null || dt.Rows.Count <= 0) return;

            string lotid = grdLotInfo.GetFieldValue("LOTID").ToString();

            string ProcessUOM = Format.GetTrimString(dt.Rows[0]["PROCESSUOM"]);
            decimal BlkQty = Format.GetDecimal(dt.Rows[0]["PCSARY"]);
            decimal pcsPnl = Format.GetDecimal(dt.Rows[0]["PCSPNL"].ToString());

            if (BlkQty.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, Language.GetMessage("NotInputPNLBKL").Message);
                args.IsCancel = true;
                return;
            }

            if (pcsPnl.Equals(0))
            {
                MSGBox.Show(MessageBoxType.Information, Language.GetMessage("NotInputPCSPNL").Message);
                args.IsCancel = true;
                return;
            }

            decimal CalQty = ProcessUOM == "BLK" ? pcsPnl/BlkQty : _panelPerQty;

            grdDefect.SetInfo(Format.GetInteger(CalQty), Format.GetInteger(grdLotInfo.GetFieldValue("PCSQTY").ToString()), lotid);
            grdDefect.View.SetFocusedRowCellValue("LOTID", lotid);

            //grdDefect.SetConsumableDefComboBox();
        }
        #endregion

        #region - Grid ToolBar Delete Row |
        /// <summary>
        /// Grid ToolBar Delete Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefect_ToolbarDeletingRow(object sender, EventArgs e)
        {
            DefectText_TextChanged(null, null);

            SetChangeGoodDefectQty(0);
        } 
        #endregion
        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (!grdLotInfo.Enabled)
            {
                throw MessageException.Create("NoSaveData");
            }

            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave") == DialogResult.No) return;

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

            // Lot 정보
            DataTable lotList = grdLotList.View.GetCheckedRows();
            //string LotId = ((DataTable)grdLotInfo.DataSource).Rows[0]["LOTID"].ToString();

			// Defect 정보
			DataTable defectList = grdDefect.DataSource as DataTable;

            decimal defectQty = 0;
            //decimal defectPNLQty = 0;

            if(defectList != null && defectList.Rows.Count > 0)
            {
                defectQty = defectList.AsEnumerable().Sum(r => r.Field<decimal>("QTY"));
                int pnlperqty = int.Parse(((DataTable)grdLotInfo.DataSource).Rows[0]["PANELPERQTY"].ToString());
                //defectPNLQty = decimal.Parse(Math.Ceiling(double.Parse((defectQty / pnlperqty).ToString())).ToString());
            }

            string strTransitArea = string.Empty;
            string strResourceid = string.Empty;
            if (cboTransitArea.GetValue() != null)
            {
                strTransitArea = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("AREAID"));
                strResourceid = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("RESOURCEID"));
            }

            if(string.IsNullOrWhiteSpace(strTransitArea))
            {
                // 인계처리시 인계작업장을 입력해야합니다.
                throw MessageException.Create("NeedToInputAreaWhenTakeOver"); 
            }

            string unit = Format.GetString(cboUOM.EditValue);

            DataTable splitList = grdLotSplit.DataSource as DataTable;

            DataTable resultData = null;

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker messageWorker = new MessageWorker("SaveFinalInspectionSend");
                messageWorker.SetBody(new MessageBody()
                {
                    { "EnterpriseId", UserInfo.Current.Enterprise },
                    { "PlantId", UserInfo.Current.Plant },
                    { "UserId", UserInfo.Current.Id },
                    { "LotList", lotList },
                    { "TransitArea", strTransitArea },
                    { "Resourceid", strResourceid },
                    { "SplitList", splitList },
                    { "Unit", unit },
                    { "DefectQty", defectQty.ToString() },
                    //{ "DefectPNLQty", defectPNLQty.ToString() },
                    { "DefectList", defectList }
                });

                var result = messageWorker.Execute<DataTable>();
                resultData = result.GetResultSet();
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

            #region - Print Split Lot Card |
            // Print Lot Card
            if (resultData.Rows.Count > 0)
            {
                pnlContent.ShowWaitArea();

                string SplitlotId = "";
                resultData.Rows.Cast<DataRow>().ForEach(row =>
                {
                    SplitlotId += row["LOTID"].ToString() + ",";
                });

                SplitlotId = SplitlotId.Substring(0, SplitlotId.Length - 1);

                CommonFunction.PrintLotCard(SplitlotId, LotCardType.Split);

                pnlContent.CloseWaitArea();
            }
            #endregion

            ClearDetailInfo();

            this.txtLotId.Text = string.Empty;
            //TxtLotId_KeyDown(null, null);
        }

        #endregion

        #region ◆ 검색 |

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

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            var areaId = txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"];

            if (areaId == null || string.IsNullOrWhiteSpace(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString()))
            {
                // 작업장선택은 필수 선택입니다.
                throw MessageException.Create("NoInputArea");
            }

            // 선택된 Lot이 있는지 여부
            DataTable lotList = grdLotList.View.GetCheckedRows();

            if (lotList == null || lotList.Rows.Count <= 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            // 인계 작업장
            if(cboTransitArea.GetValue() == null)
            {
                throw MessageException.Create("SelectTransitArea"); 
            }
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void ClearDetailInfo()
        {
            grdLotInfo.ClearData();
            grdDefect.View.ClearDatas();
            // 불량 수기 입력 컨트롤 값 초기화
            grdDefect.ClearManualDefectData();
            grdLotSplit.View.ClearDatas();
            grdLotList.View.ClearDatas();

            this.txtComment.EditValue = string.Empty;
            this.txtGoodQty.EditValue = string.Empty;
            this.txtDefectQty.EditValue = string.Empty;
            this.txtSplitQty.EditValue = string.Empty;
            this.txtParentLotQty.EditValue = string.Empty;

            this.cboUOM.Editor.EditValue = string.Empty;
            this.cboTransitArea.Editor.EditValue = string.Empty;

            this.tabInfo.Enabled = false;

            this.tabInfo.TabPages[0].PageVisible = true;
        }
        #endregion

        #region ▶ Lot 정보 조회 |
        /// <summary>
        /// Lot 정보 조회
        /// </summary>
        /// <param name="strLotId"></param>
        private string getLotInfo(string strLotId)
        {
            //2020-03-07 강유라 최종-출하 재작업 로직 추가
            // 재작업 여부 확인
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LOTID", strLotId);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable processDefTypeInfo = SqlExecuter.Query("GetProcessDefTypeByProcess", "10001", param);

            string processDefType = "";
            string lastRework = "";

            if (processDefTypeInfo.Rows.Count > 0)
            {
                DataRow processDefTypeRow = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                processDefType = Format.GetString(processDefTypeRow["PROCESSDEFTYPE"]);
                lastRework = Format.GetString(processDefTypeRow["LASTREWORK"]);
            }

            string queryVersion = "10001";

            if (processDefType == "Rework")
                queryVersion = "10011";

            Dictionary<string, object> lotParam = new Dictionary<string, object>();
            lotParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            lotParam.Add("PLANTID", UserInfo.Current.Plant);
            lotParam.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            lotParam.Add("LOTID", strLotId);
            lotParam.Add("PROCESSSTATE", Constants.WaitForSend);
            lotParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            //lotParam.Add("MIDDLESEGMENTCLASSID", "7026','7534");

            // TODO : Query Version 변경
            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", queryVersion, lotParam);

            if (lotInfo.Rows.Count < 1)
            {
                ClearDetailInfo();

                txtLotId.Text = "";
                txtLotId.Focus();

                // 조회할 데이터가 없습니다.
                throw MessageException.Create("NoSelectData");
            }

            grdLotInfo.DataSource = lotInfo;

            //_panelPerQty = decimal.Parse(lotInfo.Rows[0]["PANELPERQTY"].ToString());
            //_panelQty = decimal.Parse(lotInfo.Rows[0]["PNLQTY"].ToString());
            //_qty = decimal.Parse(lotInfo.Rows[0]["PCSQTY"].ToString());
            _panelPerQty = Format.GetDecimal(lotInfo.Rows[0]["PANELPERQTY"]);
            _panelQty = Format.GetDecimal(lotInfo.Rows[0]["PNLQTY"]);
            _qty = Format.GetDecimal(lotInfo.Rows[0]["PCSQTY"]);

            //2020-03-07 강유라 분할lot조회시 재작업lot/ 아닌lot 조회조건
            string rework =
                !string.IsNullOrWhiteSpace(Format.GetString(lotInfo.Rows[0]["SUBPROCESSDEFID"])) && Format.GetString(lotInfo.Rows[0]["ISREWORK"]).Equals("Y") ?
                "Y" : "N";

            //2020-03-09 출하 -최종 재작업 전 이동한 내용 임시 처리
            if (string.IsNullOrWhiteSpace(Format.GetString(lotInfo.Rows[0]["SUBPROCESSDEFID"])) && Format.GetString(lotInfo.Rows[0]["ISREWORK"]).Equals("Y"))
                rework = "NONE";

            this.txtGoodQty.Text = _qty.ToString();
            this.txtDefectQty.Text = "0";
            this.txtParentLotQty.Text = lotInfo.AsEnumerable().FirstOrDefault()["PCSQTY"].ToString();

            this.cboUOM.EditValue = "PNL";            

            // Lot Message Setting
            this.ucMessage.SetDatasource(CommonFunction.changeArgString(strLotId), grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString()
                , grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString()
                , grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

            // 특기사항
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            commentParam.Add("LOTID", CommonFunction.changeArgString(txtLotId.Text));
            commentParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

            this.grdComment.DataSource = SqlExecuter.Query("SelectCommentByProcess", "10001", commentParam);

            // 공정 SPEC
            Dictionary<string, object> processSpecParam = new Dictionary<string, object>();
            processSpecParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            processSpecParam.Add("PLANTID", UserInfo.Current.Plant);
            processSpecParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            processSpecParam.Add("LOTID", CommonFunction.changeArgString(txtLotId.Text));
            processSpecParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());
            processSpecParam.Add("CONTROLTYPE", "XBARR");
            processSpecParam.Add("SPECCLASSID", "OperationSpec");

            grdProcessSpec.DataSource = SqlExecuter.Query("SelectProcessSpecByProcess", "10001", processSpecParam);

            // 인계작업장
            Dictionary<string, object> transitAreaParam = new Dictionary<string, object>();
            transitAreaParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            transitAreaParam.Add("PLANTID", UserInfo.Current.Plant);
            transitAreaParam.Add("LOTID", strLotId);
            if (grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID") != null && !string.IsNullOrWhiteSpace(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString()))
            {
                transitAreaParam.Add("PROCESSSEGMENTID", grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString());
                transitAreaParam.Add("PROCESSSEGMENTVERSION", grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTVERSION").ToString());
            }
            transitAreaParam.Add("RESOURCETYPE", "Resource");
            transitAreaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable transitAreaList = new DataTable();

            string stepType = grdLotInfo.GetFieldValue("STEPTYPE").ToString();
            // 인계 작업장
            if (stepType.Split(',').Contains("WaitForSend"))
            {
                cboTransitArea.Visible = true;

                transitAreaList = SqlExecuter.Query("GetTransitAreaList", lastRework == "Y" ? "10032" : "10031", transitAreaParam);

                string primaryAreaId = "";

                //후공정이 없으면 현재공정의 작업장을 보여준다.
                if (string.IsNullOrWhiteSpace(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString()))
                {
                    primaryAreaId = grdLotInfo.GetFieldValue("AREAID").ToString();
                }
                else
                {
                    for (int i = 0; i < transitAreaList.Rows.Count; i++)
                    {
                        DataRow areaRow = transitAreaList.Rows[i];

                        if (areaRow["ISPRIMARYRESOURCE"].ToString() == "Y")
                        {
                            primaryAreaId = areaRow["AREAID"].ToString();
                            break;
                        }
                    }
                }

                cboTransitArea.Editor.PopupWidth = 300;
                cboTransitArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboTransitArea.Editor.ShowHeader = false;
                cboTransitArea.Editor.ValueMember = "RESOURCEID";
                cboTransitArea.Editor.DisplayMember = "RESOURCENAME";
                cboTransitArea.Editor.UseEmptyItem = true;
                cboTransitArea.Editor.EmptyItemValue = "";
                cboTransitArea.Editor.EmptyItemCaption = "";
                cboTransitArea.Editor.DataSource = transitAreaList;
                cboTransitArea.EditValue = string.IsNullOrEmpty(primaryAreaId) ? cboTransitArea.Editor.EmptyItemValue : primaryAreaId;
            }
            else
            {
                cboTransitArea.Visible = false;

                cboTransitArea.EditValue = null;
                cboTransitArea.Editor.DataSource = null;
            }

            //2021.03.05 uom에따른 불량 수량 변경
            string ProcessUOM = Format.GetTrimString(lotInfo.Rows[0]["PROCESSUOM"]);
            decimal BlkQty = Format.GetDecimal(lotInfo.Rows[0]["PCSARY"]);
            decimal pcsPnl = Format.GetDecimal(lotInfo.Rows[0]["PCSPNL"].ToString());

            if (BlkQty.Equals(0))
            {
                throw MessageException.Create("NotInputPNLBKL");
            }

            if (pcsPnl.Equals(0))
            {
                throw MessageException.Create("NotInputPCSPNL");
            }

            decimal CalQty = ProcessUOM == "BLK" ? pcsPnl/BlkQty : _panelPerQty;

            setDefectInputType(ProcessUOM);
            grdDefect.SetInfo(CalQty, _qty);
            // 후공정이 출하검사인지 체크하여 출하검사인 경우 LOT분할 활성화 / 아니면 비활성화
            /* NOTE : 아빅스 제품 분할을 위해 아래 로직 주석처리(김순복님 요청) 2020-03-03
            if (grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID") != null && !string.IsNullOrWhiteSpace(grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString()))
            {
                if (grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString().StartsWith("7030") || grdLotInfo.GetFieldValue("NEXTPROCESSSEGMENTID").ToString().StartsWith("7536"))
                {
                    this.tabInfo.TabPages[0].PageVisible = true;
                }
            }
            */

            return rework;
        } 
        #endregion

        #region ▶ Lot Split Grid Add New Row |
        /// <summary>
        /// Split Add New Row
        /// </summary>
        private void SplitAddNewRow(string addType)
        {
            if (string.IsNullOrWhiteSpace(txtSplitQty.EditValue.ToString().Trim()))
            {
                // 분할수량을(를) 먼저 입력하세요.
                ShowMessage("PriorityInputSomething", Language.Get("SPLITQTY"));
                return;
            }

            DataTable lotInfo = grdLotInfo.DataSource as DataTable;

            if (lotInfo == null) return;

            string parentLotid = lotInfo.AsEnumerable().FirstOrDefault()["LOTID"].ToString();

            // 분할 수량 체크
            decimal lotQty = decimal.Parse(this.txtGoodQty.Text);

            decimal splitQty = 0;
            decimal targetQty = 0;
            int count = 0;

            // 불량수량 체크
            decimal defectQty = 0;

            DataTable defect = grdDefect.DataSource as DataTable;

            if (defect != null && defect.Rows.Count > 0)
            {
                var t = defect.AsEnumerable().FirstOrDefault()["QTY"];

                if (!DBNull.Value.Equals(t))
                {
                    defectQty = (defect.AsEnumerable().Sum(r => r.Field<decimal>("QTY")) as decimal?) ?? 0;
                }
            }

            if (cboUOM.GetValue().Equals("PNL"))
                splitQty = decimal.Parse(txtSplitQty.EditValue.ToString()) * _panelPerQty;
            else
                splitQty = decimal.Parse(txtSplitQty.EditValue.ToString());

            if (lotQty <= defectQty)
            {
                // 불량 수량은 Lot 수량보다 많을 수 없습니다. {0}
                throw MessageException.Create("LotQtyLargerThanDefectQty");
            }

            // 불량 수량과 분할 수량 비교
            if (defectQty >= splitQty)
            {
                // 잔량보다 불량수량이 많습니다.
                throw MessageException.Create("DefectCountGreatThenLeftQty");
            }

            DataTable splitDt = grdLotSplit.DataSource as DataTable;
            if (splitDt != null && splitDt.Rows.Count > 0)
            {
                count = splitDt.AsEnumerable().Where(r => r.Field<object>("PARENTLOTID").Equals(parentLotid)).Count();

                var query = from dr in splitDt.AsEnumerable().Where(r => r.Field<object>("PARENTLOTID").Equals(parentLotid))
                            group dr by dr["PARENTLOTID"] into g
                            select new
                            {
                                lotId = g.Key,
                                sumQty = g.Sum(r => r.Field<decimal>("QTY"))
                            };

                foreach (var x in query)
                {
                    targetQty = x.sumQty;
                }

                if(lotQty < targetQty + splitQty)
                {
                    // 분할 Lot의 수량은 모 Lot의 수량보다 클 수 없습니다. {0}
                    throw MessageException.Create("SplitQtyLessThanParentQty"); 
                }
            }

            if (cboUOM.GetValue() == null || cboUOM.GetValue().Equals(""))
            {
                // UOM을 선택하여 주십시오.
                ShowMessage("SelectUOM");
                return;
            }

            grdLotSplit.View.CellValueChanged -= GrdLotSplitView_CellValueChanged;

            grdLotSplit.View.AddNewRow();

            grdLotSplit.View.SetFocusedRowCellValue("PARENTLOTID", parentLotid);
            if (count == 0)
            {
                grdLotSplit.View.SetFocusedRowCellValue("LOTID", parentLotid);
                grdLotSplit.View.SetFocusedRowCellValue("ISPARENT", "Y");

                grdLotSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling((splitQty - defectQty) / _panelPerQty));
                grdLotSplit.View.SetFocusedRowCellValue("QTY", splitQty - defectQty);

                lotQty -= (splitQty - defectQty);
            }
            else
            {
                grdLotSplit.View.SetFocusedRowCellValue("LOTID", "Split-" + count.ToString());
                grdLotSplit.View.SetFocusedRowCellValue("ISPARENT", "N");

                grdLotSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling(splitQty / _panelPerQty));
                grdLotSplit.View.SetFocusedRowCellValue("QTY", splitQty);

                lotQty -= splitQty;
            }

            grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
            grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
            grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFNAME", grdLotInfo.GetFieldValue("PRODUCTDEFNAME"));
            grdLotSplit.View.SetFocusedRowCellValue("UNIT", "PCS");

            SetLotSplitQty();

            grdLotSplit.View.CellValueChanged += GrdLotSplitView_CellValueChanged;

            #region - 기존 자동 생성 코드 |
            /*
            bool chk = true;            

            int count = 0;

            while (chk)
            {
                if (lotQty < splitQty)
                {
                    chk = false;
                    continue;
                }

                grdLotSplit.View.AddNewRow();

                grdLotSplit.View.SetFocusedRowCellValue("PARENTLOTID", parentLotid);
                if (count == 0)
                {
                    grdLotSplit.View.SetFocusedRowCellValue("LOTID", parentLotid);
                    grdLotSplit.View.SetFocusedRowCellValue("ISPARENT", "Y");

                    grdLotSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling((splitQty - defectQty) / _panelPerQty));
                    grdLotSplit.View.SetFocusedRowCellValue("QTY", splitQty - defectQty);

                    lotQty -= (splitQty - defectQty);
                }
                else
                {
                    grdLotSplit.View.SetFocusedRowCellValue("LOTID", "Split-" + count.ToString());
                    grdLotSplit.View.SetFocusedRowCellValue("ISPARENT", "N");

                    grdLotSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling(splitQty / _panelPerQty));
                    grdLotSplit.View.SetFocusedRowCellValue("QTY", splitQty);

                    lotQty -= splitQty;
                }

                grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
                grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFNAME", grdLotInfo.GetFieldValue("PRODUCTDEFNAME"));
                grdLotSplit.View.SetFocusedRowCellValue("UNIT", "PCS");

                count++;

                // 최종 나머지 수량은 모LOT으로 지정하여 Row 추가
                if (lotQty <= splitQty)
                {
                    chk = false;

                    grdLotSplit.View.AddNewRow();

                    grdLotSplit.View.SetFocusedRowCellValue("LOTID", "Split-" + count.ToString());
                    grdLotSplit.View.SetFocusedRowCellValue("ISPARENT", "N");
                    grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
                    grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                    grdLotSplit.View.SetFocusedRowCellValue("PRODUCTDEFNAME", grdLotInfo.GetFieldValue("PRODUCTDEFNAME"));
                    grdLotSplit.View.SetFocusedRowCellValue("UNIT", "PCS");

                    grdLotSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling(lotQty / _panelPerQty));
                    grdLotSplit.View.SetFocusedRowCellValue("QTY", lotQty);
                }
            }
            */
            #endregion
        }
        #endregion
        private void setDefectInputType(string processUOM)
        {

            Color copyColor = grdDefect.View.Columns["DECISIONDEGREENAME"].AppearanceHeader.ForeColor;

            if (!processUOM.Equals("PCS"))
            {
                grdDefect.PcsSetting = true;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = true;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = false;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = copyColor;
            }
            else
            {
                grdDefect.PcsSetting = false;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = false;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = true;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = copyColor;
            }

            string ColCaption = processUOM == "BLK" ? Language.Get("QTY") + "(" + "BLK" + ")" : Language.Get("QTY").ToString() + "(" + "PNL" + ")";
            grdDefect.View.Columns["PNLQTY"].Caption = ColCaption;
            grdDefect.View.Columns["PNLQTY"].ToolTip = ColCaption;

            grdDefect.DefectUOM = processUOM;
            grdDefect.setDefectTextBox(processUOM);
        }
        #region ▶ 양품, 불량수량 변경 |
        /// <summary>
        /// 양품, 불량수량 변경
        /// </summary>
        /// <param name="defectQty"></param>
        private void SetChangeGoodDefectQty(decimal defectQty)
        {
            DataTable dt = grdDefect.DataSource as DataTable;

            var result = from dr in dt.AsEnumerable()
                         group dr by dr["LOTID"] into dg
                         select new
                         {
                             lotid = dg.Key,
                             defectqty = dg.Sum(r => Format.GetInteger(r.Field<object>("QTY")))
                         };

            int sumQty = 0;
            foreach(var x in result)
            {
                sumQty = x.defectqty;
            }

            this.txtGoodQty.Text = Convert.ToString(decimal.Parse(grdLotInfo.GetFieldValue("PCSQTY").ToString()) - sumQty);
            this.txtDefectQty.Text = Convert.ToString(sumQty);
        }
        #endregion

        #region ▶ 분할 수량 업데이트 |
        /// <summary>
        /// 분할 수량 업데이트
        /// </summary>
        private void SetLotSplitQty()
        {
            DataTable lotData = grdLotList.DataSource as DataTable;

            DataTable dt = grdLotSplit.DataSource as DataTable;

            var result = from lotRows in lotData.AsEnumerable()
                            join splitRows in dt.AsEnumerable() on lotRows.Field<string>("LOTID") equals splitRows.Field<string>("PARENTLOTID") into g
                            select new
                            {
                                LOTID = lotRows.Field<string>("LOTID"),
                                SPLITQTY = g.Sum(r => Format.GetInteger(r.Field<object>("QTY")))
                            };

            var dl = result.ToList();

            for (int i = 0; i < dl.Count(); i++)
            {
                grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID.ToString()), "SPLITQTY", Format.GetInteger(dl[i].SPLITQTY.ToString()));
            }
        }
        #endregion

        #region ▶ Gird Sum |
        /// <summary>
        /// Gird Sum
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="e"></param>
        public void GridSum(SmartBandedGrid grd, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grd.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                decimal PanelSum = 0;
                decimal qtySum = 0;
                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    PanelSum += Format.GetDecimal(row["PANELQTY"]);
                    qtySum += Format.GetDecimal(row["QTY"]);
                });

                if (e.Column.FieldName == "PANELQTY")
                {
                    if (_panelPerQty > 0) PanelSum = Math.Ceiling((qtySum / _panelPerQty).ToDecimal());
                    e.Info.DisplayText = Format.GetString(PanelSum);
                }
                if (e.Column.FieldName == "QTY")
                {
                    e.Info.DisplayText = Format.GetString(qtySum);
                }
            }
            else
            {
                grd.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "  ";
                grd.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
        } 
        #endregion

        #endregion
    }
}