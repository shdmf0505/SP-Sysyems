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
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 최종검사 작업시작
    /// 업  무  설  명  : 최종검사 작업 시작 한다.
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-21
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class FinalInspectionWorkStart : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가
        #endregion

        #region ◆ 생성자 |

        /// <summary>
        /// 생성자
        /// </summary>
        public FinalInspectionWorkStart()
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

            // TODO : 컨트롤 초기화 로직 구성
            InitializeControls();

            InitializeEvent();

            // Grid 초기화
            //InitializeGrid();
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
            grdLotInfo.SetInvisibleFields("PROCESSSTATE", "PROCESSPATHID", "PROCESSSEGMENTID", "PROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTID",
                                "NEXTPROCESSSEGMENTVERSION", "NEXTPROCESSSEGMENTTYPE", "PRODUCTDEFVERSION", "PRODUCTTYPE", "PRODUCTDEFTYPEID"
                                , "LOTTYPE", "ISHOLD", "AREANAME", "DEFECTUNIT", "PCSPNL", "PANELPERQTY", "PROCESSSEGMENTTYPE", "STEPTYPE"
                                , "ISPRINTLOTCARD", "ISPRINTRCLOTCARD", "TRACKINUSER", "TRACKINUSERNAME", "MATERIALCLASS", "AREAID", "ISRCLOT"
                                , "SELFSHIPINSPRESULT", "SELFTAKEINSPRESULT", "MEASUREINSPRESULT", "OSPINSPRESULT", "ISBEFOREROLLCUTTING"
                                , "PATHTYPE", "LOTSTATE", "WAREHOUSEID", "ISWEEKMNG", "DESCRIPTION", "RTRSHT", "PROCESSSEGMENTCLASSID"
                                , "PCSARY", "PROCESSUOM", "OSMCHECK", "ISCLAIMLOT", "PARENTPROCESSSEGMENTCLASSID", "RESOURCEID", "BLKQTY"
                                , "ISLOCKING", "ISREWORK"
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

            #region - 자주검사 이력 |
            usInspectionResult.Size = new Size(1800, 600);
            usInspectionResult.DefectSplitContainer.Height = 280;
            usInspectionResult.DefectSplitContainer.Panel1.Size = new Size(1400, 280);
            //usInspectionResult.DefectSplitContainer.SplitterPosition = 1400;

            usInspectionResult.InitializeControls();

            
            #endregion
        }
        #endregion

        #region ▶ Grid 초기화 |
        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            string productDefId = grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString();
            string productDefVer = grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString();

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
            InitializeGrid_InspectorPopup();
            grdLotList.View.AddTextBoxColumn("INSPECTIONUSER", 150).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("AREAID", 70).SetIsHidden();

            grdLotList.View.PopulateColumns();

            InitializationSummaryRow();
            #endregion

            #region - 작업시작 설비 |
            grdEquipment.GridButtonItem = GridButtonItem.None;
            grdEquipment.ShowButtonBar = false;
            grdEquipment.ShowStatusBar = false;

            grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdEquipment.View.SetIsReadOnly();

            // 설비코드
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTID", 150);
            // 설비명
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 200);

            grdEquipment.View.PopulateColumns();
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
            grdLotList.View.Columns["PRODUCTDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PRODUCTDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdLotList.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdLotList.View.OptionsView.ShowFooter = true;
            grdLotList.ShowStatusBar = false;
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

            // TextBox Event
            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;
            txtLotId.Editor.Click += Editor_Click;

            // Grid Event
            grdLotList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;

            usInspectionResult.InitializeEvent();
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
            if (e.KeyCode == Keys.Enter)
            {
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
                param.Add("PROCESSSTATE", "Wait");
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
                param.Add("PROCESSSTATE", Constants.Wait);
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("MIDDLESEGMENTCLASSID", "7026','7534");

                DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", queryVersion, param);

                if (lotInfo.Rows.Count < 1)
                {
                    ClearDetailInfo();

                    txtLotId.Text = "";
                    txtLotId.Focus();

                    // 조회할 데이터가 없습니다.
                    //throw MessageException.Create("NoSelectData");
                    // Lot이 작업 시작 가능 상태가 아닙니다. {0}
                    DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);
                    ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
                    return;
                }

                grdLotInfo.DataSource = lotInfo;

                grdLotInfo.Enabled = true;
                pfsInfo.Enabled = true;
                tabInfo.Enabled = true;

                // GRID 초기화
                if (grdLotList.DataSource == null)
                {
                    InitializeGrid();
                }

                // 최종검사 대상 LOT 조회 :: RootLotID 기준
                getFinalInspectionLotList();

                // Lot Message Setting
                this.ucMessage.SetDatasource(CommonFunction.changeArgString(txtLotId.Text), grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString()
                    , grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString()
                    , grdLotInfo.GetFieldValue("PROCESSSEGMENTID").ToString());

                // 설비 데이터 조회
                Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
                equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
                equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                equipmentParam.Add("LOTID", CommonFunction.changeArgString(txtLotId.Text));
                equipmentParam.Add("EQUIPMENTTYPE", "Production");
                equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");

                DataTable equipmentList = SqlExecuter.Query("GetEquipmentByArea", "10031", equipmentParam);

                grdEquipment.DataSource = equipmentList;

                // 데이터가 하나면 자동 체크
                if (equipmentList != null && equipmentList.Rows.Count == 1)
                {
                    grdEquipment.View.CheckedAll(true);
                }

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

                // 자주검사
                usInspectionResult.SearchInspectionData(CommonFunction.changeArgString(txtLotId.Text));

                DialogResult result = WipFunction.ShowWindowTimeArrivedPopup(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());    // Window Time 이 도래한 Lot 목록 표시
                if(result == DialogResult.OK)
                {
                    OpenWindowTimeLot();
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
        #endregion

        #region ▶ Grid Event |

        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdLotList.DataSource as DataTable;

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
            }
            else
            {
                grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "  ";
                grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave") == DialogResult.No) return;

            if (!grdLotInfo.Enabled)
            {
                throw MessageException.Create("NoSaveData");
            }

            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable lotList = grdLotList.View.GetCheckedRows();

            DataTable equipList = grdEquipment.View.GetCheckedRows();

            string equipmentId = string.Join(",", equipList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));

            if (string.IsNullOrWhiteSpace(equipmentId))
            {
                // 설비는 필수로 선택해야 합니다. {0}
                throw MessageException.Create("EquipmentIsRequired");
            }

            MessageWorker messageWorker = new MessageWorker("SaveFinalInspectionStart");
            messageWorker.SetBody(new MessageBody()
            {
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "Comment", txtComment.EditValue.ToString() },
                { "Equipmentlist", equipmentId },
                { "LotList", lotList }
            });

            messageWorker.Execute();

            ShowMessage("SuccedSave");

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
            grdLotList.View.CheckValidation();

            // TODO : 유효성 로직 변경
            var areaId = txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"];

            if (areaId == null || string.IsNullOrWhiteSpace(txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString()))
            {
                // 작업장선택은 필수 선택입니다.
                throw MessageException.Create("NoInputArea");
            }

            DataTable dt = grdLotList.View.GetCheckedRows();

            if(dt == null || dt.Rows.Count <= 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            // 검사자 지정여부 체크
            if(dt.AsEnumerable().Where(r => string.IsNullOrWhiteSpace(r.Field<string>("INSPECTIONUSER"))).Count() > 0)
            {
                throw MessageException.Create("NotSelectInspector");
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
            grdLotList.View.ClearDatas();

            this.txtComment.Text = string.Empty;

            grdLotInfo.Enabled = false;
            pfsInfo.Enabled = false;
            tabInfo.Enabled = false;
        }
        #endregion

        #region ▶ 최종검사 대상 LOT 조회 :: RootLotID 기준 |
        /// <summary>
        /// 최종검사 대상 LOT 조회 :: RootLotID 기준
        /// </summary>
        private void getFinalInspectionLotList()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", this.txtLotId.Text.Trim());
            param.Add("PROCESSSTATE", Constants.Wait);

            DataTable dt = SqlExecuter.Query("SelectLotListForFinalInspection", "10001", param);

            if (dt.Rows.Count < 1)
            {
                ClearDetailInfo();

                txtLotId.Text = "";
                txtLotId.Focus();

                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdLotList.DataSource = dt;

            grdLotList.View.CheckedAll(true);
        }
        #endregion

        #region ▶ 검사원 조회 팝업창 |
        /// <summary>
        /// 원인품목 조회 팝업창
        /// </summary>
        private void InitializeGrid_InspectorPopup()
        {
            string productDefId = grdLotInfo.GetFieldValue("PRODUCTDEFID").ToString();
            string productDefVer = grdLotInfo.GetFieldValue("PRODUCTDEFVERSION").ToString();

            var inspectorColumn = grdLotList.View.AddSelectPopupColumn("INSPECTORNAME", 200, new SqlQuery("GetFinalInspector", "10001", $"PRODUCTDEFID={productDefId}", $"PRODUCTDEFVERSION={productDefVer}"))
                .SetPopupLayout("FINISHINSPECTORNAME", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(500, 500, FormBorderStyle.SizableToolWindow)
                .SetPopupResultMapping("INSPECTORNAME", "INSPECTORNAME")
                .SetLabel("INSPECTIONUSER")
                .SetValidationIsRequired()
                .SetPopupApplySelection((selectedRows, gridRow) => {

                    if (selectedRows.ToList().Count > 0)
                    {
                        gridRow["INSPECTIONUSER"] = string.Join(",", selectedRows.ToList().Cast<DataRow>().Select(r => Format.GetString(r["INSPECTORID"])));
                    }
                    else if (selectedRows.ToList().Count == 0)
                    {
                        gridRow["INSPECTIONUSER"] = "";
                    }
                })
                ;

            inspectorColumn.Conditions.AddTextBox("LOTID").SetPopupDefaultByGridColumnId("LOTID").SetIsHidden();
            inspectorColumn.Conditions.AddTextBox("AREAID").SetPopupDefaultByGridColumnId("AREAID").SetIsHidden();

            inspectorColumn.GridColumns.AddTextBoxColumn("INSPECTORID", 100);
            inspectorColumn.GridColumns.AddTextBoxColumn("INSPECTORNAME", 80);
            inspectorColumn.GridColumns.AddTextBoxColumn("GRADE", 80);
        }
        #endregion

        private void OpenWindowTimeLot()
        {
            try
            {
                DialogManager.ShowWaitDialog();
                string menuId = "PG-SG-0480";   // Window Time Lot 조회
                var param = new Dictionary<string, object>();
                OpenMenu(menuId, param); //다른창 호출..
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
            finally
            {
                DialogManager.Close();
            }
        }
        #endregion
    }
}