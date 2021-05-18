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
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 최종검사 인수 등록
    /// 업  무  설  명  : 입고 검사 완료된 인수 대기 중인 최종검사 인수 등록 한다.
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-15
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class FinalInspectionAccept : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가
        #endregion

        #region ◆ 생성자 |

        /// <summary>
        /// 생성자
        /// </summary>
        public FinalInspectionAccept()
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

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeControls();

            // Grid 초기화
            InitializeGrid();
        }

        #region ▶ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        private void InitializeControls()
        {
            this.txtLotId.ImeMode = ImeMode.Alpha;

            // LOT 정보 GRID
            grdLotInfo.ColumnCount = 5;
            grdLotInfo.LabelWidthWeight = "40%";
            grdLotInfo.SetInvisibleFields("PROCESSSTATE", "PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTID", 
                                            "NEXTPROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTTYPE", "PRODUCTDEFVERSION", "PRODUCTTYPE", "PRODUCTDEFTYPEID"
                                            , "LOTTYPE", "ISHOLD", "AREANAME", "DEFECTUNIT", "PCSPNL", "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE"
                                            , "ISPRINTLOTCARD", "ISPRINTRCLOTCARD", "TRACKINUSER", "TRACKINUSERNAME", "MATERIALCLASS", "AREAID", "ISRCLOT"
                                            , "SELFSHIPINSPRESULT", "SELFTAKEINSPRESULT", "MEASUREINSPRESULT", "OSPINSPRESULT", "ISBEFOREROLLCUTTING"
                                            , "PATHTYPE", "LOTSTATE", "WAREHOUSEID", "ISWEEKMNG", "DESCRIPTION", "RTRSHT", "PROCESSSEGMENTCLASSID"
                                            , "PCSARY", "PROCESSUOM", "OSMCHECK", "ISCLAIMLOT", "PARENTPROCESSSEGMENTCLASSID","RESOURCEID","BLKQTY"
                                            , "ISLOCKING","ISREWORK"
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

            this.txtParentLotQty.Text = "0";

            this.tabMain.Enabled = false;
        }
        #endregion

        #region ▶ Grid 초기화 |
        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - Split Grid |
            grdSplit.GridButtonItem = GridButtonItem.Delete | GridButtonItem.Add;
            
            // CheckBox 설정
            this.grdSplit.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdSplit.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdSplit.View.AddTextBoxColumn("ISPARENT", 70).SetTextAlignment(TextAlignment.Center).SetIsHidden();
            grdSplit.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdSplit.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdSplit.View.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();
            grdSplit.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdSplit.View.AddSpinEditColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            grdSplit.View.AddSpinEditColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

            grdSplit.View.PopulateColumns();

            InitializationSummaryRow();
            #endregion

            #region - Lot Message |

            this.ucMessage.InitializeControls();

            #endregion

            #region - 특기사항 |
            grdComment.GridButtonItem = GridButtonItem.None;
            grdComment.ShowButtonBar = false;
            grdComment.ShowStatusBar = false;

            grdComment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdComment.View.SetIsReadOnly();

            // 공정수순ID
            grdComment.View.AddTextBoxColumn("PROCESSPATHID", 150)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // 공정수순
            grdComment.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정명
            grdComment.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 특기사항
            grdComment.View.AddTextBoxColumn("DESCRIPTION", 500)
                .SetLabel("REMARKS");
            // 현재공정여부
            grdComment.View.AddTextBoxColumn("ISCURRENTPROCESS", 70)
                .SetIsHidden();

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
            grdProcessSpec.View.AddTextBoxColumn("PROCESSPATHID", 150)
                .SetValidationKeyColumn()
                .SetIsHidden();
            // 공정수순
            grdProcessSpec.View.AddTextBoxColumn("USERSEQUENCE", 80);
            // 공정ID
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTID", 150)
                .SetIsHidden();
            // 공정명
            grdProcessSpec.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 항목
            grdProcessSpec.View.AddTextBoxColumn("SPECCLASSNAME", 120)
                .SetLabel("SPECITEM");
            // 하한값
            grdProcessSpec.View.AddSpinEditColumn("LSL", 90)
                .SetDisplayFormat("#,##0.00");
            // 중간값
            grdProcessSpec.View.AddSpinEditColumn("SL", 90)
                .SetDisplayFormat("#,##0.00");
            // 상한값
            grdProcessSpec.View.AddSpinEditColumn("USL", 90)
                .SetDisplayFormat("#,##0.00");
            // 현재공정여부
            grdProcessSpec.View.AddTextBoxColumn("ISCURRENTPROCESS", 70)
                .SetIsHidden();

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
            grdSplit.View.Columns["PRODUCTDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSplit.View.Columns["PRODUCTDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdSplit.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSplit.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = " ";
            grdSplit.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdSplit.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdSplit.View.OptionsView.ShowFooter = true;
            grdSplit.ShowStatusBar = false;
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
            this.Load += FinalInspectionAccept_Load;

            // 초기화
            btnInit.Click += BtnInit_Click;
            btnSplit.Click += BtnSplit_Click;

            // TextBox Event
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += Editor_Click;

            //txtSplitQty.Editor.KeyPress += SplitQty_KeyPress;

            // Grid Event
            grdSplit.View.CellValueChanged += GrdSplitView_CellValueChanged;
            grdSplit.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdSplit.View.AddingNewRow += grdSplitView_AddingNewRow;
            grdSplit.HeaderButtonClickEvent += GrdSplit_HeaderButtonClickEvent;
        }

        private void FinalInspectionAccept_Load(object sender, EventArgs e)
        {
            if(UserInfo.Current.Enterprise == "YOUNGPOONG")
            {
                tabMain.TabPages[0].PageVisible = false;
            }
        }

        private void GrdSplit_HeaderButtonClickEvent(object sender, HeaderButtonClickArgs args)
        {
            if (args.ClickItem == GridButtonItem.Delete)
            {
                grdSplit.View.CellValueChanged -= GrdSplitView_CellValueChanged;
                try
                {
                    DataTable dt = grdSplit.DataSource as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ISPARENT"].ToString() == "N")
                        {
                            dt.Rows[i]["LOTID"] = "Split-" + i.ToString();
                        }
                    }
                }
                finally
                {
                    grdSplit.View.CellValueChanged += GrdSplitView_CellValueChanged;
                }
            }
        }

        private void grdSplitView_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            grdSplit.View.CellValueChanged -= GrdSplitView_CellValueChanged;
            try
            {
                DataTable dt = grdSplit.DataSource as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ISPARENT"].ToString() == "N")
                    {
                        dt.Rows[i]["LOTID"] = "Split-" + i.ToString();
                    }
                }
            }
            finally
            {
                grdSplit.View.CellValueChanged += GrdSplitView_CellValueChanged;
            }
        }

        private void grdSplitView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataTable lotInfo = grdLotInfo.DataSource as DataTable;
            if (lotInfo == null)
            {
                args.IsCancel = true;
                return;
            }

            if(txtSplitQty.EditValue == null || int.Parse(txtSplitQty.EditValue.ToString()) <= 0)
            {
                ShowMessage("NotSplitQty", Language.Get("SPLITQTY"));
                args.IsCancel = true;
                return;
            }

            // Panel별 단위 수량 조회
            int pnlperqty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PANELPERQTY"].ToString());

            // 분할 수량 체크
            DataTable dt = grdSplit.DataSource as DataTable;
            var pcsRT = dt.AsEnumerable().Sum(r => Format.GetInteger(r.Field<object>("QTY")));
            int usedQty = Format.GetInteger(pcsRT.ToString());  // 이미 분할된 수량

            int lotQty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PCSQTY"].ToString());

            if(usedQty >= lotQty)
            {
                args.IsCancel = true;
                return;
            }

            if(int.Parse(txtSplitQty.EditValue.ToString()) <= 0)
            {
                args.IsCancel = true;
                return;
            }

            if (cboUOM.GetValue() == null || cboUOM.GetValue().Equals(""))
            {
                args.IsCancel = true;
                // UOM을 선택하여 주십시오.
                ShowMessage("SelectUOM");
                return;
            }

            string parentLotid = lotInfo.AsEnumerable().FirstOrDefault()["LOTID"].ToString();
            int splitQty = 0;
            if (cboUOM.GetValue().Equals("PNL"))
            {
                splitQty = int.Parse(txtSplitQty.EditValue.ToString()) * pnlperqty;
            }
            else
            {
                splitQty = int.Parse(txtSplitQty.EditValue.ToString());
            }

            if (splitQty > lotQty - usedQty)
            {
                splitQty = lotQty - usedQty;
            }

            int count = dt.Rows.Count;
            if (count == 1)
            {
                args.NewRow["LOTID"] = parentLotid;
                args.NewRow["ISPARENT"] = "Y";
            }
            else
            {
                args.NewRow["LOTID"] = "Split-" + (count - 1).ToString();
                args.NewRow["ISPARENT"] = "N";
            }

            args.NewRow["PRODUCTDEFID"] = grdLotInfo.GetFieldValue("PRODUCTDEFID");
            args.NewRow["PRODUCTDEFVERSION"] = grdLotInfo.GetFieldValue("PRODUCTDEFVERSION");
            args.NewRow["PRODUCTDEFNAME"] = grdLotInfo.GetFieldValue("PRODUCTDEFNAME");
            args.NewRow["UNIT"] = "PCS";

            args.NewRow["PANELQTY"] = Math.Ceiling(Convert.ToDecimal((decimal)splitQty / (decimal)pnlperqty));
            args.NewRow["QTY"] = splitQty;
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
            if (e.KeyCode != Keys.Enter) return;

            ClearDetailInfo();

            if (txtArea.Editor.SelectedData.Count() < 1 || string.IsNullOrWhiteSpace(txtArea.EditValue.ToString()) || string.IsNullOrWhiteSpace(txtLotId.Text))
            {
                // 작업장, LOT No.는 필수 입력 항목입니다.
                ShowMessage("AreaLotIdIsRequired");
                ClearDetailInfo();
                return;
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", txtLotId.Text);
            param.Add("PROCESSSTATE", "WaitForReceive");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

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

            // 재작업 여부 확인
            param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", txtLotId.Text);
            param.Add("PROCESSSTATE", "WaitForReceive");
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

            string queryVersion = "10031";

            if (processDefType == "Rework")
                queryVersion = "30031";

            param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", CommonFunction.changeArgString(txtLotId.Text));
            param.Add("PROCESSSTATE", Constants.WaitForReceive);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("MIDDLESEGMENTCLASSID", "7026','7534"); 

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", queryVersion, param);

            if (lotInfo.Rows.Count < 1)
            {
                DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);
                ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
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
            else if (Format.GetFullTrimString(lotInfo.Rows[0]["LOTSTATE"]).Equals("OverSeaInTransit"))
            {
                // 해외 물류 이동중입니다.
                ShowMessage("CheckOverSeasLogistic");
            }

            grdLotInfo.Enabled = true;

            grdLotInfo.DataSource = lotInfo;

            this.txtParentLotQty.Text = lotInfo.AsEnumerable().FirstOrDefault()["PCSQTY"].ToString();
            this.cboUOM.Editor.EditValue = "PNL";

            // Lot Message Setting
            this.ucMessage.SetDatasource(CommonFunction.changeArgString(txtLotId.Text), grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString()
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

            // Lot Message Popup
            WipFunction.ShowLotMessagePopup(lotInfo, CommonFunction.changeArgString(txtLotId.Text), grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString()
                    , grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString(), grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

            this.tabMain.Enabled = true;
        }

        /// <summary>
        /// 분할수량 Key Press Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplitQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(char.IsDigit(e.KeyChar)) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
                e.Handled = true;

                BtnSplit_Click(null, null);
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
            grdSplit.View.ClearDatas();

            DataTable dt = grdSplit.DataSource as DataTable;

            if(int.Parse(this.txtParentLotQty.Text) == 0 && dt != null && dt.Rows.Count > 0)
            {
                return;
            }

            if(Format.GetInteger(this.txtSplitQty.EditValue.ToString()) < 1)
            {
                return;
            }

            SplitAddNewRow("Button");
        }
        #endregion

        #region ▶ Grid Event |

        #region - Split Grid Cell Value Changed Event |
        /// <summary>
        /// Split Grid Cell Value Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdSplitView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdSplit.View.CellValueChanged -= GrdSplitView_CellValueChanged;

            // Panel별 단위 수량 조회
            int pnlperqty = 0;

            DataTable lotInfo = grdLotInfo.DataSource as DataTable;

            if (lotInfo == null) return;

            pnlperqty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PANELPERQTY"].ToString());

            DataTable dt = grdSplit.DataSource as DataTable;

            int lotQty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PCSQTY"].ToString());

            if (cboUOM.GetValue() == null || cboUOM.GetValue().Equals(""))
            {
                // UOM을 선택하여 주십시오.
                ShowMessage("SelectUOM");
                return;
            }

            int qty = Format.GetInteger(grdSplit.View.GetFocusedValue());
            if (e.Column.FieldName.Equals("QTY"))
                grdSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling(Convert.ToDecimal(qty / pnlperqty)));
            else
                grdSplit.View.SetFocusedRowCellValue("QTY", qty * pnlperqty);

            var pcsRT = dt.AsEnumerable().Sum(r => Format.GetInteger(r.Field<object>("QTY")));

            if (Format.GetInteger(pcsRT.ToString()) > lotQty)
            {
                // 분할 Lot의 수량은 모 Lot의 수량보다 클 수 없습니다. {0}
                MSGBox.Show(MessageBoxType.Warning, "SplitQtyLessThanParentQty");

                grdSplit.View.SetFocusedRowCellValue("PANELQTY", 0);
                grdSplit.View.SetFocusedRowCellValue("QTY", 0);

                grdSplit.View.CellValueChanged += GrdSplitView_CellValueChanged;

                return;
            }

            grdSplit.View.CellValueChanged += GrdSplitView_CellValueChanged;
        }
        #endregion

        #region - Split Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdSplit.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int PanelSum = 0;
                int qtySum = 0;
                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    PanelSum += Format.GetInteger(row["PANELQTY"]);
                    qtySum += Format.GetInteger(row["QTY"]);
                });

                if (e.Column.FieldName == "PANELQTY")
                {
                    e.Info.DisplayText = Format.GetString(PanelSum);
                }
                if (e.Column.FieldName == "QTY")
                {
                    e.Info.DisplayText = Format.GetString(qtySum);
                }

                DataTable lotInfo = grdLotInfo.DataSource as DataTable;

                this.txtParentLotQty.Text = (int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PCSQTY"].ToString()) - int.Parse(Format.GetString(qtySum))).ToString();
            }
            else
            {
                grdSplit.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "  ";
                grdSplit.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
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

            // TODO : 저장 Rule 변경

            string lotId = grdLotInfo.GetFieldValue("LOTID").ToString();
            string processPathId = grdLotInfo.GetFieldValue("PROCESSPATHID").ToString();
            string processSegmentId = grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString();

            DataTable lotInfo = grdLotInfo.DataSource as DataTable;
            DataTable splitdt = grdSplit.DataSource as DataTable;

            string lotQty = grdLotInfo.GetFieldValue("PCSQTY").ToString();
            string lotPnlQty = grdLotInfo.GetFieldValue("PNLQTY").ToString();

            if (splitdt != null && splitdt.Rows.Count > 0)
            {
                lotQty = splitdt.AsEnumerable().Where(r => r.Field<string>("ISPARENT").Equals("Y")).FirstOrDefault()["QTY"].ToString();
                lotPnlQty = splitdt.AsEnumerable().Where(r => r.Field<string>("ISPARENT").Equals("Y")).FirstOrDefault()["PANELQTY"].ToString();
            }

            string unit = Format.GetString(cboUOM.EditValue);

            DataTable resultData = null;

            try
            {
                pnlContent.ShowWaitArea();

                MessageWorker messageWorker = new MessageWorker("SaveFinalInspectionAccept");
                messageWorker.SetBody(new MessageBody()
                {
                    { "EnterpriseId", UserInfo.Current.Enterprise },
                    { "PlantId", UserInfo.Current.Plant },
                    { "UserId", UserInfo.Current.Id },
                    { "AreaId", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString() },
                    { "LotId", lotId },
                    { "ProcessPathId", processPathId },
                    { "ProcessSegmentId", processSegmentId },
                    { "Unit", unit },
                    { "LotQty", lotQty },
                    { "LotPNLQty", lotPnlQty },
                    { "SplitList", splitdt }
                });

                //messageWorker.Execute();
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

            txtLotId.Text = "";
            txtLotId.Focus();
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
            /*
            Dictionary<string, object> values = pfsInfo.GetValues();

            if (values.ContainsKey("WORKER") && string.IsNullOrEmpty(values["WORKER"].ToString()))
            {
                // 작업자는 필수 입력 항목입니다.
                throw MessageException.Create("WorkerIsRequired");
            }
            
            if (values.ContainsKey("GOODQTY") && Format.GetDecimal(values["GOODQTY"]) < 1)
            {
                // 양품수량은 0 보다 커야 합니다.
                throw MessageException.Create("GoodQtyLargerThanZero");
            }

            string unit = values["UNIT"].ToString();
            decimal goodPnlQty = Format.GetDecimal(values["GOODPNLQTY"]);

            if (unit != "PCS" && goodPnlQty % 1 != 0)
            {
                // 단위가 PCS가 아닌 경우 PNL 수량은 정수로 나와야 합니다.
                throw MessageException.Create("PanelQtyHasNotInteger");
            }
            */
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
            grdSplit.View.ClearDatas();

            grdLotInfo.Enabled = false;

            this.cboUOM.Editor.EditValue = string.Empty;

            this.txtSplitQty.Text = string.Empty;
            this.txtParentLotQty.Text = string.Empty;
        } 
        #endregion

        #region ▶ Split Add New Row
        /// <summary>
        /// Split Add New Row
        /// </summary>
        private void SplitAddNewRow(string addType)
        {
            grdSplit.View.AddingNewRow -= grdSplitView_AddingNewRow;
            grdSplit.View.CellValueChanged -= GrdSplitView_CellValueChanged;
            try
            {
                if (string.IsNullOrWhiteSpace(txtSplitQty.Text.Trim()))
                {
                    // 분할수량을(를) 먼저 입력하세요.
                    ShowMessage("PriorityInputSomething", Language.Get("SPLITQTY"));
                    return;
                }

                // Panel별 단위 수량 조회
                int pnlperqty = 0;

                DataTable lotInfo = grdLotInfo.DataSource as DataTable;

                if (lotInfo == null) return;

                pnlperqty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PANELPERQTY"].ToString());

                // 분할 수량 체크
                DataTable dt = grdSplit.DataSource as DataTable;

                int lotQty = int.Parse(lotInfo.AsEnumerable().FirstOrDefault()["PCSQTY"].ToString());

                if (cboUOM.GetValue() == null || cboUOM.GetValue().Equals(""))
                {
                    // UOM을 선택하여 주십시오.
                    ShowMessage("SelectUOM");
                    return;
                }

                string parentLotid = lotInfo.AsEnumerable().FirstOrDefault()["LOTID"].ToString();

                bool chk = true;
                int splitQty = 0;

                if (cboUOM.GetValue().Equals("PNL"))
                    splitQty = int.Parse(txtSplitQty.EditValue.ToString()) * pnlperqty;
                else
                    splitQty = int.Parse(txtSplitQty.EditValue.ToString());

                int count = 0;

                while (chk)
                {
                    if (lotQty < splitQty)
                    {
                        chk = false;
                        continue;
                    }

                    grdSplit.View.AddNewRow();
                    if (count == 0)
                    {
                        grdSplit.View.SetFocusedRowCellValue("LOTID", parentLotid);
                        grdSplit.View.SetFocusedRowCellValue("ISPARENT", "Y");
                    }
                    else
                    {
                        grdSplit.View.SetFocusedRowCellValue("LOTID", "Split-" + count.ToString());
                        grdSplit.View.SetFocusedRowCellValue("ISPARENT", "N");
                    }

                    grdSplit.View.SetFocusedRowCellValue("PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
                    grdSplit.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                    grdSplit.View.SetFocusedRowCellValue("PRODUCTDEFNAME", grdLotInfo.GetFieldValue("PRODUCTDEFNAME"));
                    grdSplit.View.SetFocusedRowCellValue("UNIT", "PCS");

                    grdSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling(Convert.ToDecimal((decimal)splitQty / (decimal)pnlperqty)));
                    grdSplit.View.SetFocusedRowCellValue("QTY", splitQty);

                    lotQty -= splitQty;
                    count++;

                    // 최종 나머지 수량은 모LOT으로 지정하여 Row 추가
                    if (lotQty <= splitQty && lotQty > 0)
                    {
                        grdSplit.View.AddNewRow();

                        grdSplit.View.SetFocusedRowCellValue("LOTID", "Split-" + count.ToString());
                        grdSplit.View.SetFocusedRowCellValue("ISPARENT", "N");
                        grdSplit.View.SetFocusedRowCellValue("PRODUCTDEFID", grdLotInfo.GetFieldValue("PRODUCTDEFID"));
                        grdSplit.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", grdLotInfo.GetFieldValue("PRODUCTDEFVERSION"));
                        grdSplit.View.SetFocusedRowCellValue("PRODUCTDEFNAME", grdLotInfo.GetFieldValue("PRODUCTDEFNAME"));
                        grdSplit.View.SetFocusedRowCellValue("UNIT", "PCS");

                        decimal a = Math.Ceiling(Convert.ToDecimal(lotQty / pnlperqty));

                        grdSplit.View.SetFocusedRowCellValue("PANELQTY", Math.Ceiling(Convert.ToDecimal((decimal)lotQty / (decimal)pnlperqty)));
                        grdSplit.View.SetFocusedRowCellValue("QTY", lotQty);

                        lotQty -= splitQty;
                        count++;
                    }
                }
            }
            finally
            {
                grdSplit.View.AddingNewRow += grdSplitView_AddingNewRow;
                grdSplit.View.CellValueChanged += GrdSplitView_CellValueChanged;
            }
        }
        #endregion

        #endregion
    }
}