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

// TODO : GetFinalInspector 쿼리에서 AREAID 파라미터가 입력되지 않아 조회되지 않음

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 최종검사 작업완료 (PG-SG-0700)
    /// 업  무  설  명  : 최종검사 작업을 완료한다.
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-27
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class FinalInspectionWorkComplete : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가
        private decimal _panelPerQty = 0;
        private decimal _panelQty = 0;
        private decimal _qty = 0;

        // File Data
        private DataTable _fileData = new DataTable();
        #endregion

        #region ◆ 생성자 |

        /// <summary>
        /// 생성자
        /// </summary>
        public FinalInspectionWorkComplete()
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

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeControls();

            // Grid 초기화
            //InitializeGrid();

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

            // CheckBox 설정
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
            grdLotList.View.AddTextBoxColumn("PANELPERQTY", 80).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            InitializeGrid_InspectorPopup();
            grdLotList.View.AddTextBoxColumn("INSPECTIONUSER", 150).SetIsHidden();

            //2021.03.05 배선용
            grdLotList.View.AddTextBoxColumn("PROCESSUOM", 150).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PCSARY", 150).SetIsHidden();

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

            #region - NCR Grid |
            grdNcrList.View.SetIsReadOnly();
            grdNcrList.GridButtonItem = GridButtonItem.None;

            grdNcrList.View.AddTextBoxColumn("DECISIONDEGREE").SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("DECISIONDEGREENAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("DECISIONDEGREE");
            grdNcrList.View.AddTextBoxColumn("QCGRADE", 80).SetTextAlignment(TextAlignment.Center);
            grdNcrList.View.AddTextBoxColumn("NGCONDITION", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("NGCONDITIONNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("NGCONDITION");
            grdNcrList.View.AddTextBoxColumn("QTYORRATE", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("QTYORRATENAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("QTYORRATE");
            grdNcrList.View.AddTextBoxColumn("NCRSPEC", 100).SetTextAlignment(TextAlignment.Center).SetLabel("RANGE");
            grdNcrList.View.AddTextBoxColumn("FROMRATE", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("TORATE", 60).SetIsHidden();
            grdNcrList.View.AddTextBoxColumn("NGQUANTITY", 60).SetIsHidden();
            grdNcrList.View.PopulateColumns();
            #endregion

            #region - Defect Grid |
            grdDefect.VisibleLotId = true;
            grdDefect.VisibleTopDefectCode = true;
            grdDefect.VisibleFileUpLoad = true;

            grdDefect.InitializeControls();
            grdDefect.InitializeEvent();
            grdDefect.SetInspectionDefinitionId("FinishInspection");
            #endregion

            #region - File Upload |
            grdFileList.GridButtonItem = GridButtonItem.None;
            grdFileList.View.SetIsReadOnly();
            grdFileList.ShowStatusBar = false;

            grdFileList.View.AddTextBoxColumn("FILENAME", 150);
            grdFileList.View.AddTextBoxColumn("FILEEXT", 60).SetTextAlignment(TextAlignment.Center);
            grdFileList.View.AddSpinEditColumn("FILESIZE", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
            grdFileList.View.AddTextBoxColumn("FILEPATH", 100).SetIsHidden();
            grdFileList.View.AddTextBoxColumn("URL").SetIsHidden();

            grdFileList.View.PopulateColumns();

            _fileData = (grdFileList.DataSource as DataTable).Clone();

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

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// ComboBox 초기화
        /// </summary>
        private void InitializeComboBox()
        {
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
            grdLotList.View.DoubleClick += LotList_DoubleClick;
            grdLotList.View.RowStyle += LotList_RowStyle;
            grdLotList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;

            grdDefect.DefectQtyChanged += GrdDefect_DefectQtyChanged;
            grdDefect.View.AddingNewRow += Defect_AddingNewRow;
            grdDefect.DefectFileInfoChanged += GrdDefect_DefectFileInfoChanged;

            //파일
            grdFileList.View.FocusedRowChanged += GrdFileView_FocusedRowChanged;
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
                param.Add("PROCESSSTATE", "Run");
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
                // 최종검사 대상 LOT 조회 :: RootLotID 기준
                getLotInfo(CommonFunction.changeArgString(txtLotId.Text));

                Dictionary<string, object> param2 = new Dictionary<string, object>();
                param2.Add("PLANTID", UserInfo.Current.Plant);
                param2.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param2.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
                param2.Add("LOTID", CommonFunction.changeArgString(txtLotId.Text));
                param2.Add("PROCESSSTATE", Constants.Run);

                 dt = SqlExecuter.Query("SelectLotListForFinalInspection", "10001", param2);

                if (dt.Rows.Count < 1)
                {
                    DataTable AreaLot = SqlExecuter.Query("GetLotAreaAndState", "10001", param);
                    ShowMessage("NotAreaOrState", Format.GetString(AreaLot.Rows[0]["AREANAME"]), Format.GetString(AreaLot.Rows[0]["PROCESSTATE"]));
                    return;
                }

                grdLotList.DataSource = dt;

                // NCR 기준정보
                getNCRInfo();

                tabInfo.Enabled = true;

                string lotId = string.Join(",", (grdLotList.DataSource as DataTable).AsEnumerable().Select(c => Format.GetString(c["LOTID"])).Distinct());

                grdDefect.SetConsumableDefComboBox(lotId);
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

            // LOT 정보 조회
            string strLotid = dr["LOTID"].ToString();

            getLotInfo(strLotid);
            //this.cboInspector.EditValue = strInspector;

            grdLotList.View.CheckRow(((SmartBandedGridView)sender).FocusedRowHandle, true);
        }
        #endregion

        #region - Lot 목록 Row Stype Event |
        /// <summary>
        /// Lot 목록 Row Stype Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LotList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            bool isChecked = grdLotList.View.IsRowChecked(e.RowHandle);

            if (isChecked)
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }
        #endregion

        #region - LotList Grid Footer Sum Event |
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
            }
            else
            {
                grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "  ";
                grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
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
            DataRow dr = grdLotList.View.GetFocusedDataRow();

            if (dr == null) return;

            string lotid = dr["LOTID"].ToString();

            string ProcessUOM = Format.GetTrimString(dr["PROCESSUOM"]);
            decimal BlkQty = Format.GetDecimal(dr["PCSARY"]);
            decimal pcsPnl = Format.GetDecimal(dr["PCSPNL"].ToString());

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

            //grdDefect.SetInfo(CalQty, _qty);

            grdDefect.SetInfo(CalQty, Format.GetInteger(dr["QTY"].ToString()));
            args.NewRow["LOTID"] = lotid;

            grdDefect.LotID = lotid;
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
            CheckDefectQty(e.RowHandle);
        }
        #endregion

        #region - Defect Grid 파일정보 변경 Event |
        /// <summary>
        /// Defect Grid 파일정보 변경 Event |
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefect_DefectFileInfoChanged(object sender, EventArgs e)
        {
            if (grdDefect.View.FocusedRowHandle < 0) return;

            FocusedDataBindOfFileInfo();
        }
        #endregion

        #region - GrdFileView_FocusedRowChanged :: 파일 View |
        // <summary>
        /// 파일 View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdFileView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdFileList.View.FocusedRowHandle < 0) return;

            string filePath = Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEPATH"));
            string fileName = string.Join(".", Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILENAME")), Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEEXT")));

            picDefectPhoto.EditValue = CommonFunction.GetFtpImageFileToByte(filePath, fileName);
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
            if (ShowMessage(MessageBoxButtons.YesNo, DialogResult.No, "InfoSave") == DialogResult.No) return;

            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경

            // Lot 정보
            DataTable lotList = grdLotList.View.GetCheckedRows();

            if(lotList == null || lotList.Rows.Count == 0)
            {
                throw MessageException.Create("NoSeletedLot"); 
            }

            // Defect 정보
            grdDefect.CheckDefect();

            DataTable defectList = grdDefect.DataSource as DataTable;

            #region - NCR Check |
            DataTable _dtDefectData = defectList.Clone();
            DataTable ncrList = grdNcrList.DataSource as DataTable;

            // 예외 목록(업로드 사진 필수 등록)
            DataTable dtExceptionImageList = getRegistrationOfDefectImageExcudingUploadsInfo();

            //********************************************************************************************
            // INSPECTION DEFECT DATA
            //********************************************************************************************

            if (defectList != null)
            {
                string message = string.Empty;

                foreach (DataRow row in defectList.Rows)
                {
                    decimal pcsQty = Format.GetDecimal(row["QTY"]);
                    decimal pnlQty = Format.GetDecimal(row["PNLQTY"]);
                    double defectRate = Format.GetDouble(row["DEFECTRATE"], 0);
                    string defectGrade = Format.GetTrimString(row["DECISIONDEGREE"]);
                    string defectGradeName = Format.GetTrimString(row["DECISIONDEGREENAME"]);
                    string defectCode = Format.GetTrimString(row["DEFECTCODE"]);
                    string defectCodeName = Format.GetTrimString(row["DEFECTCODENAME"]);
                    string qcsegmentid = Format.GetTrimString(row["QCSEGMENTID"]);
                    string qcsegmentname = Format.GetTrimString(row["QCSEGMENTNAME"]);

                    // 예외 목록(업로드 사진 필수 등록) Count ==> 예외 목록에 포함 됨 : count > 0
                    int checkExceptionImage = dtExceptionImageList.AsEnumerable().Where(c => c.Field<string>("CODEID").Equals(row["DEFECTCODE"])).Count();

                    bool bChk3 = !(checkExceptionImage > 0);

                    bool bChk2 = 
                    (
                           string.IsNullOrEmpty(Format.GetString(row["FILEPATH"]))
                        || string.IsNullOrEmpty(Format.GetString(row["FILENAME"]))
                        || string.IsNullOrEmpty(Format.GetString(row["FILEEXT"]))
                        || string.IsNullOrEmpty(Format.GetString(row["FILESIZE"]))
                        || string.IsNullOrEmpty(Format.GetString(row["IMAGERESOURCEID"]))
                    );

                    message += checkNCR2(defectGrade, defectRate, pcsQty, defectCode, defectCodeName, qcsegmentid, qcsegmentname, bChk2, bChk3, defectGradeName);
                    
                    if (pcsQty > 0)
                    {
                        _dtDefectData.ImportRow(row);
                    }
                }
                if (message.Length > 0)
                {
                    throw MessageException.Create("CHECKIMEAGEBYNCR2", "\r\n\r\n" + Language.Get("TARGETITEM") + message);
                }

            }

            #endregion

            DataTable dfdt = _dtDefectData.Clone();

            // 체크된 Lot 기준으로 Defect Code List를 Join하여 데이터를 한번 걸러냄
            var dfList = from lotRows in lotList.AsEnumerable()
                         join dfRows in defectList.AsEnumerable() on lotRows.Field<string>("LOTID") equals dfRows.Field<string>("LOTID")
                         select new
                         {
                             LOTID = lotRows.Field<string>("LOTID"),
                             DEFECTCODE = dfRows.Field<string>("DEFECTCODE"),
                             QCSEGMENTID = dfRows.Field<string>("QCSEGMENTID"),
                             PNLQTY = Format.GetDecimal(dfRows["PNLQTY"]),
                             QTY = Format.GetDecimal(dfRows["QTY"]),
                             REASONCONSUMABLEDEFID = dfRows.Field<string>("REASONCONSUMABLEDEFID"),
                             REASONCONSUMABLELOTID = dfRows.Field<string>("REASONCONSUMABLELOTID"),
                             REASONPROCESSSEGMENTID = dfRows.Field<string>("REASONPROCESSSEGMENTID"),
                             REASONAREAID = dfRows.Field<string>("REASONAREAID"),
                             //2019-12-10 강유라 DEFECTCODENAME, QCSEGMENTNAME, DECISIONDEGREE 추가
                             DEFECTCODENAME = dfRows.Field<string>("DEFECTCODENAME"),
                             QCSEGMENTNAME = dfRows.Field<string>("QCSEGMENTNAME"),
                             DECISIONDEGREE = dfRows.Field<string>("DECISIONDEGREE"),
                             DEFECTRATE = Format.GetDecimal(dfRows["DEFECTRATE"]),
                             IMAGERESOURCEID = dfRows.Field<string>("IMAGERESOURCEID")
                         };

            foreach (var df in dfList)
            {
                DataRow dr = dfdt.NewRow();
                dr["LOTID"] = df.LOTID;
                dr["DEFECTCODE"] = df.DEFECTCODE;
                dr["QCSEGMENTID"] = df.QCSEGMENTID;
                dr["PNLQTY"] = df.PNLQTY;
                dr["QTY"] = df.QTY;
                dr["REASONCONSUMABLEDEFID"] = df.REASONCONSUMABLEDEFID;
                dr["REASONCONSUMABLELOTID"] = df.REASONCONSUMABLELOTID;
                dr["REASONPROCESSSEGMENTID"] = df.REASONPROCESSSEGMENTID;
                dr["REASONAREAID"] = df.REASONAREAID;
                //2019-12-10 강유라 DEFECTCODENAME, QCSEGMENTNAME, DECISIONDEGREE 추가
                dr["DEFECTCODENAME"] = df.DEFECTCODENAME;
                dr["QCSEGMENTNAME"] = df.QCSEGMENTNAME;
                dr["DECISIONDEGREE"] = df.DECISIONDEGREE;
                dr["DEFECTRATE"] = df.DEFECTRATE;
                dr["IMAGERESOURCEID"] = df.IMAGERESOURCEID;
                dfdt.Rows.Add(dr);
            }


            //***********2020-03-08 강유라
            //***********검사 사진 sf_objectfilemap, sf_objectfile 테이블에 저장될 데이터 가공******************
            DataTable defectImage = _dtDefectData.Clone();

            // 체크된 Lot 기준으로 Defect Code List를 Join하여 데이터를 한번 걸러냄
            var defectImageList = from lotRows in lotList.AsEnumerable()
                         join dfRows in defectList.AsEnumerable() on lotRows.Field<string>("LOTID") equals dfRows.Field<string>("LOTID")
                         where !string.IsNullOrWhiteSpace(Format.GetString(dfRows.Field<string>("IMAGERESOURCEID")))
                         select new
                         {
                             LOTID = lotRows.Field<string>("LOTID"),
                             IMAGERESOURCEID = dfRows.Field<string>("IMAGERESOURCEID"),
                             FILENAME = dfRows.Field<string>("FILENAME"),
                             FILEEXT = dfRows.Field<string>("FILEEXT"),
                             FILESIZE = dfRows.Field<string>("FILESIZE"),
                             FILEPATH = dfRows.Field<string>("FILEPATH")
                         };

            foreach (var dfFile in defectImageList)
            {
                DataRow dr = defectImage.NewRow();
                dr["LOTID"] = dfFile.LOTID;
                dr["IMAGERESOURCEID"] = dfFile.IMAGERESOURCEID;
                dr["FILENAME"] = dfFile.FILENAME;
                dr["FILEEXT"] = dfFile.FILEEXT;
                dr["FILESIZE"] = dfFile.FILESIZE;
                dr["FILEPATH"] = dfFile.FILEPATH;

                defectImage.Rows.Add(dr);
            }
            //***********2020-03-08 강유라 검사 사진 *********************************************************

            // Lot별 Defect Sum 정보
            DataTable defectLot = new DataTable();
            defectLot.Columns.Add("LOTID", typeof(String));
            defectLot.Columns.Add("DEFECTQTY", typeof(Double));
            //defectLot.Columns.Add("DEFECTPNLQTY", typeof(Double));

            // 불량정보가 있을 경우 LOT별로 Group By하여 Defect수량 및 Panel수량 산출
            if (dfdt != null && dfdt.Rows.Count > 0)
            {
                var dqry = from dr in dfdt.AsEnumerable()
                           group dr by dr["LOTID"] into dg
                           select new
                           {
                               lotid = dg.Key,
                               defectqty = dg.Sum(r => r.Field<decimal>("QTY"))
                           };

                foreach (var x in dqry)
                {
                    DataRow dr = defectLot.NewRow();
                    dr["LOTID"] = x.lotid;
                    dr["DEFECTQTY"] = x.defectqty;
                    //dr["DEFECTPNLQTY"] = Math.Ceiling(x.defectqty / _panelPerQty);

                    defectLot.Rows.Add(dr);
                }
            }

            // Lot 수량과 Defect SUM Qty 비교
            var cp = from lot in lotList.AsEnumerable()
                     join defect in defectLot.AsEnumerable() on lot.Field<string>("LOTID") equals defect.Field<string>("LOTID")
                     where lot.Field<decimal>("QTY") < Convert.ToDecimal(defect.Field<double>("DEFECTQTY"))
                     select defect
                     ;

            if(cp != null && cp.Count() > 0)
            {
                // 불량 수량은 Lot 수량보다 많을 수 없습니다. {0}
                throw MessageException.Create("LotQtyLargerThanDefectQty");
            }

            // 인계작업장
            string strTransitArea = string.Empty;
            string strResourceid = string.Empty;
            if (cboTransitArea.GetValue() != null)
            {
                strTransitArea = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("AREAID"));
                strResourceid = Format.GetFullTrimString(cboTransitArea.Editor.GetColumnValue("RESOURCEID"));
            }

            DataTable equipList = grdEquipment.View.GetCheckedRows();

            string equipmentId = string.Join(",", equipList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));

            if (string.IsNullOrWhiteSpace(equipmentId))
            {
                // 설비는 필수로 선택해야 합니다. {0}
                throw MessageException.Create("EquipmentIsRequired");
            }

            MessageWorker messageWorker = new MessageWorker("SaveFinalInspectionComplete");
            messageWorker.SetBody(new MessageBody()
            {
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "TransitArea", strTransitArea },
                { "Resourceid", strResourceid },
                { "LotList", lotList },
                { "Equipmentlist", equipmentId },
                { "DefectLot", defectLot },
                { "DefectList", dfdt },
                { "DefectFileList",defectImage}//2020-03-08 강유라
            });

            //2019-12-10 강유라 이메일 내용 받아서 lot 별로 처리
            var isSendEmailDt = messageWorker.Execute<DataTable>();
            var isSendEmailRS = isSendEmailDt.GetResultSet();

            //여러lot 한번에 처리 되므로 lot 별로 한세트로 만들기
            if (isSendEmailRS.Rows.Count > 0)
            {
                //2019-12-20 강유라 이메일 내용 공통으로 뺌
                //2019-12-27 강유라 xml로 변경
                DataTable toSendDt = CommonFunction.CreateFinishAbnormalEmailDt();

                foreach (DataRow lotRow in lotList.Rows)
                {
                    var lotDefect = isSendEmailRS.AsEnumerable()
                                                 .Where(r => Format.GetString(r["LOTID"]).Equals(Format.GetString(lotRow["LOTID"])))
                                                 .ToList();

                    if (lotDefect.Count > 0)
                    {
                        string lotDefectNameContents = string.Empty;

                        foreach (DataRow defectRow in lotDefect)
                        {
                            lotDefectNameContents += Format.GetString(defectRow["DEFECTINFO"]) + "<br></br>";
                        }

                        //2019-12-20 이메일 내용 반환함수에 넘겨줄 DataRow
                        //2019-12-27 강유라 xml로 변경
                        DataRow newRow = toSendDt.NewRow();
                        newRow["PROCESSSEGMENTNAME"] = Format.GetString(lotRow["PROCESSSEGMENTNAME"]);
                        newRow["AREANAME"] = Format.GetString(lotRow["AREANAME"]);
                        newRow["PRODUCTDEFNAME"] = Format.GetString(lotRow["PRODUCTDEFNAME"]);
                        newRow["PRODUCTDEFID"] = Format.GetString(lotRow["PRODUCTDEFID"]);
                        newRow["PRODUCTDEFVERSION"] = Format.GetString(lotRow["PRODUCTDEFVERSION"]);
                        newRow["LOTID"] = Format.GetString(lotRow["LOTID"]);
                        newRow["DEFECTNAME"] = lotDefectNameContents;
                        newRow["USERID"] = UserInfo.Current.Id;
                        newRow["TITLE"] = Language.Get("FINISHABNORMALTITLE");
                        newRow["INSPECTION"] = "FinishInspection";
                        newRow["LANGUAGETYPE"] = UserInfo.Current.LanguageType;

                        toSendDt.Rows.Add(newRow);
                    }
                }
                //2019-12-27 강유라 xml로 변경 컬럼변경 및 dataTable을 파라미터로 변경
                CommonFunction.ShowSendEmailPopupDataTable(toSendDt);
            }

            ShowMessage("SuccedSave");

            ClearDetailInfo();

            this.txtLotId.Text = string.Empty;
            grdDefect.SetImageYNChecked = true;
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

            DataTable dt = grdLotList.View.GetCheckedRows();

            if (dt == null || dt.Rows.Count <= 0)
            {
                throw MessageException.Create("NoSaveData");
            }

            // 검사자 지정여부 체크
            if (dt.AsEnumerable().Where(r => string.IsNullOrWhiteSpace(r.Field<string>("INSPECTIONUSER"))).Count() > 0)
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
            grdLotList.View.ClearDatas();
            grdDefect.View.ClearDatas();
            // 불량 수기 입력 컨트롤 값 초기화
            grdDefect.ClearManualDefectData();
            grdComment.View.ClearDatas();
            grdProcessSpec.View.ClearDatas();
            grdNcrList.View.ClearDatas();
            grdFileList.View.ClearDatas();
            picDefectPhoto.EditValue = null;

            ucMessage.ClearDatas();

            this.tabInfo.Enabled = false;
        }
        #endregion

        #region ▶ Lot 정보 조회 |
        /// <summary>
        /// Lot 정보 조회
        /// </summary>
        /// <param name="strLotId"></param>
        private void getLotInfo(string strLotId)
        {
            // 재작업 여부 확인
            Dictionary<string, object> param = new Dictionary<string, object>();
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
                DataRow rowRouting = processDefTypeInfo.AsEnumerable().FirstOrDefault();

                processDefType = Format.GetString(rowRouting["PROCESSDEFTYPE"]);
                processDefId = Format.GetString(rowRouting["PROCESSDEFID"]);
                processDefVersion = Format.GetString(rowRouting["PROCESSDEFVERSION"]);
            }

            string queryVersion = "10031";

            if (processDefType == "Rework")
                queryVersion = "30031";

            param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("AREAID", txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString());
            param.Add("LOTID", strLotId);
            param.Add("PROCESSSTATE", "Run");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("MIDDLESEGMENTCLASSID", "7026','7534");

            DataTable lotInfo = SqlExecuter.Query("SelectLotInfoByProcess", queryVersion, param);

            if (lotInfo.Rows.Count < 1)
            {
                ClearDetailInfo();

                txtLotId.Text = "";
                txtLotId.Focus();

                // 조회할 데이터가 없습니다.
                throw MessageException.Create("NoSelectData");
            }

            grdLotInfo.DataSource = lotInfo;

            if (grdLotList.DataSource == null)
            {
                InitializeGrid();
            }

            //_panelPerQty = decimal.Parse(lotInfo.Rows[0]["PANELPERQTY"].ToString());
            //_panelQty = decimal.Parse(lotInfo.Rows[0]["PNLQTY"].ToString());
            //_qty = decimal.Parse(lotInfo.Rows[0]["PCSQTY"].ToString());
            _panelPerQty = Format.GetDecimal(lotInfo.Rows[0]["PANELPERQTY"]);
            _panelQty = Format.GetDecimal(lotInfo.Rows[0]["PNLQTY"]);
            _qty = Format.GetDecimal(lotInfo.Rows[0]["PCSQTY"]);

            // Step Type에 따라 인계작업장 지정 여부 설정
            DataRow row = lotInfo.Rows[0];

            string stepType = row["STEPTYPE"].ToString();

            // 인계 작업장
            if (!stepType.Split(',').Contains("WaitForSend"))
            {
                cboTransitArea.Visible = true;

                Dictionary<string, object> transitAreaParam = new Dictionary<string, object>();
                transitAreaParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                transitAreaParam.Add("PLANTID", UserInfo.Current.Plant);
                transitAreaParam.Add("LOTID", strLotId);
                transitAreaParam.Add("PROCESSSEGMENTID", row["NEXTPROCESSSEGMENTID"].ToString());
                transitAreaParam.Add("PROCESSSEGMENTVERSION", row["NEXTPROCESSSEGMENTVERSION"].ToString());
                transitAreaParam.Add("RESOURCETYPE", "Area");
                transitAreaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                cboTransitArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboTransitArea.Editor.ShowHeader = false;
                cboTransitArea.Editor.ValueMember = "RESOURCEID";
                cboTransitArea.Editor.DisplayMember = "RESOURCENAME";
                cboTransitArea.Editor.UseEmptyItem = true;
                cboTransitArea.Editor.EmptyItemValue = "";
                cboTransitArea.Editor.EmptyItemCaption = "";
                cboTransitArea.Editor.DataSource = SqlExecuter.Query("GetTransitAreaList", "10032", transitAreaParam);
                cboTransitArea.EditValue = cboTransitArea.Editor.EmptyItemValue;
            }
            else
            {
                cboTransitArea.Visible = false;

                cboTransitArea.EditValue = null;
                cboTransitArea.Editor.DataSource = null;
            }

            // Lot Message Setting
            this.ucMessage.SetDatasource(CommonFunction.changeArgString(strLotId), lotInfo.Rows[0]["PRODUCTDEFID"].ToString()
                , lotInfo.Rows[0]["PRODUCTDEFVERSION"].ToString()
                , lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString());

            // 설비 데이터 조회
            Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
            equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
            equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            equipmentParam.Add("LOTID", CommonFunction.changeArgString(strLotId));
            equipmentParam.Add("EQUIPMENTTYPE", "Production");
            equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");

            DataTable equipmentList = SqlExecuter.Query("GetEquipmentByArea", "10031", equipmentParam);

            grdEquipment.DataSource = equipmentList;

            // 작업시작시 체크(Track-In Time이 있는) 설비 자동체크
            if (equipmentList != null && equipmentList.Rows.Count > 0)
            {
                for (int i = 0; i < equipmentList.Rows.Count; i++)
                {
                    DataRow dr = equipmentList.Rows[i];
                    if (dr != null && dr["TRACKINTIME"] != null && !string.IsNullOrWhiteSpace(dr["TRACKINTIME"].ToString()))
                        grdEquipment.View.CheckRow(i, true);
                }
            }

            // 특기사항
            Dictionary<string, object> commentParam = new Dictionary<string, object>();
            commentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            commentParam.Add("PLANTID", UserInfo.Current.Plant);
            commentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            commentParam.Add("LOTID", CommonFunction.changeArgString(strLotId));
            commentParam.Add("PROCESSSEGMENTID", lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString());

            this.grdComment.DataSource = SqlExecuter.Query("SelectCommentByProcess", "10001", commentParam);

            // 공정 SPEC
            Dictionary<string, object> processSpecParam = new Dictionary<string, object>();
            processSpecParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            processSpecParam.Add("PLANTID", UserInfo.Current.Plant);
            processSpecParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            processSpecParam.Add("LOTID", CommonFunction.changeArgString(strLotId));
            processSpecParam.Add("PROCESSSEGMENTID", lotInfo.Rows[0]["PROCESSSEGMENTID"].ToString());
            processSpecParam.Add("CONTROLTYPE", "XBARR");
            processSpecParam.Add("SPECCLASSID", "OperationSpec");

            grdProcessSpec.DataSource = SqlExecuter.Query("SelectProcessSpecByProcess", "10001", processSpecParam);

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

        #region ▶ Lot 수량과 Defect 수량 비교 |
        /// <summary>
        /// Lot 수량과 Defect 수량 비교
        /// </summary>
        private void CheckDefectQty(int rowhandle)
        {
            DataTable lotList = grdLotList.DataSource as DataTable;
            DataTable defectList = grdDefect.DataSource as DataTable;

            var lt = lotList.AsEnumerable().ToList();

            var defect = from dr in defectList.AsEnumerable()
                         group dr by dr["LOTID"] into dg
                         select new
                         {
                             LOTID = dg.Key,
                             DEFECTQTY = dg.Sum(r => r.Field<decimal?>("QTY"))
                         };

            // Lot 수량과 Defect SUM Qty 비교
            var cp = from lot in lt.AsEnumerable()
                     join df in defect.AsEnumerable() on lot.Field<string>("LOTID") equals df.LOTID
                     where lot.Field<decimal>("QTY") < df.DEFECTQTY
                     select df
                     ;

            if (cp != null && cp.Count() > 0)
            {
                grdDefect.View.SetRowCellValue(rowhandle, "QTY", null);
                grdDefect.View.SetRowCellValue(rowhandle, "PNLQTY", null);
                grdDefect.View.SetRowCellValue(rowhandle, "DEFECTRATE", null); 

                // 불량 수량은 Lot 수량보다 많을 수 없습니다. {0}
                ShowMessage("LotQtyLargerThanDefectQty");
                //throw MessageException.Create("LotQtyLargerThanDefectQty");
            }

            var dl = defect.ToList();

            for (int i = 0; i < dl.Count(); i++)
            {
                grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID), "DEFECTQTY", Format.GetInteger(dl[i].DEFECTQTY));
            }
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

            var inspectorColumn = grdLotList.View.AddSelectPopupColumn("INSPECTORNAME", 200, new SqlQuery("GetFinalInspector", "10001", $"PRODUCTDEFID={productDefId}"
                , $"PRODUCTDEFVERSION={productDefVer}", $"AREAID={ txtArea.Editor.SelectedData.FirstOrDefault()["AREAID"].ToString() }"))
                .SetPopupLayout("FINISHINSPECTORNAME", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(500, 500, FormBorderStyle.SizableToolWindow)
                .SetPopupResultMapping("INSPECTORNAME", "INSPECTORNAME")
                .SetLabel("INSPECTIONUSER")
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

            inspectorColumn.GridColumns.AddTextBoxColumn("INSPECTORID", 100);
            inspectorColumn.GridColumns.AddTextBoxColumn("INSPECTORNAME", 80);
            inspectorColumn.GridColumns.AddTextBoxColumn("GRADE", 80);
        }
        #endregion

        //2019-12-20 강유라 추가
        #region ▶ 이메일 내용 데이타 테이블 생성 |

        /// <summary>
        /// 이메일 내용을 담을 테이블 생성
        /// </summary>
        /// <returns></returns>
        private DataTable GetEmailContentsTable()
        {
            DataTable emailDt = new DataTable("");

            emailDt.Columns.Add("PROCESSSEGMENTNAME");
            emailDt.Columns.Add("AREANAME");
            emailDt.Columns.Add("PRODUCTDEFNAME");
            emailDt.Columns.Add("PRODUCTDEFID");
            emailDt.Columns.Add("PRODUCTDEFVERSION");
            emailDt.Columns.Add("LOTID");
            emailDt.Columns.Add("DEFECTNAME");

            return emailDt;
        }
        #endregion

        #region ▶ NCR 검사기준정보 조회 |
        /// <summary>
        /// NCR 검사기준정보 조회
        /// </summary>
        private void getNCRInfo()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("INSPECTIONDEFID", "FinishInspection");
            param.Add("PLANTID", UserInfo.Current.Plant);

            DataTable dtNcrList = SqlExecuter.Query("GetNcrStandardOfSelfInspection", "10002", param);
            grdNcrList.DataSource = dtNcrList;
        }
        #endregion

        #region ▶ 파일 정보 바인드 |
        /// <summary>
        /// 파일 정보 바인드
        /// </summary>
        private void FocusedDataBindOfFileInfo()
        {
            grdFileList.View.ClearDatas();
            picDefectPhoto.EditValue = null;

            DataRow selectRow = grdDefect.View.GetFocusedDataRow();
            if (selectRow == null) return;

            DataTable dt = grdFileList.DataSource as DataTable;
            string[] fileNames = Format.GetString(selectRow["FILENAME"]).Split(',');
            string[] fileExts = Format.GetString(selectRow["FILEEXT"]).Split(',');
            string[] fileSizes = Format.GetString(selectRow["FILESIZE"]).Split(',');
            string url = AppConfiguration.GetString("Application.SmartDeploy.Url") + selectRow["FILEPATH"].ToString();

            for (int i = 1; i < fileNames.Length; i++)
            {
                DataRow newRow = dt.NewRow();

                string fileUrl = url + "/" + fileNames[i] + "." + fileExts[i];
                dt.Rows.Add(fileNames[i], fileExts[i], fileSizes[i], selectRow["FILEPATH"], fileUrl);

            }

            grdFileList.DataSource = dt;
        }
        #endregion

        #region ▶ NCR 체크 |
        /// <summary>
        /// NCR 체크
        /// </summary>
        /// <param name="defectGrade"></param>
        /// <param name="defectRate"></param>
        /// <param name="defectQty"></param>
        /// <param name="strDefectCode"></param>
        /// <returns></returns>
        private bool checkNCR(string defectGrade, double defectRate, decimal defectQty, string strDefectCode, bool bChk2, bool bChk3)
        {
            DataTable dt = grdNcrList.DataSource as DataTable;

            List<DataRow> listDr = dt.AsEnumerable().Where(c => c.Field<string>("DECISIONDEGREE").Equals(defectGrade)).ToList<DataRow>();

            bool bcheck = true;

            string message = string.Empty;

            foreach (DataRow dr in listDr)
            {
                string judgeType = Format.GetTrimString(dr["NGCONDITION"]);
                double fromRate = Format.GetDouble(dr["FROMRATE"], 0);
                double toRate = Format.GetDouble(dr["TORATE"], 0);
                double ngQty = Format.GetDouble(dr["NGQUANTITY"], 0);
                double chkDefectQty = Format.GetDouble(defectQty, 0);
                string checkType = Format.GetTrimString(dr["QTYORRATE"]);

                string spec = Format.GetTrimString(dr["NCRSPEC"]);
                string gcGrad = Format.GetTrimString(dr["QCGRADE"]);

                message = "\r\n" + Language.Get("DEFECTCODE") + " : " + strDefectCode + "\r\n" + Language.Get("QCGRADE") + " : " + gcGrad + "\r\n";
                if (checkType.Equals("QTY"))
                {
                    message = message + Language.Get("PCSDEFECTQTY") + " : " + defectQty + "\r\n" + Language.Get("RANGE") + " : " + spec;
                }
                else
                {
                    message = message + Language.Get("NCRDEFECTRATE") + " : " + defectRate + "\r\n" + Language.Get("RANGE") + " : " + spec;
                }
                switch (judgeType)
                {
                    case "GE": //이상
                        if (checkType.Equals("RATE") && defectRate >= toRate)
                        {


                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty >= ngQty)
                        {

                            bcheck = false;
                        }
                        break;
                    case "BT": //사이
                        if (checkType.Equals("RATE") && defectRate >= fromRate && defectRate <= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && ngQty >= fromRate && ngQty <= toRate)
                        {
                            bcheck = false;
                        }
                        break;
                    case "GT": //초과
                        if (checkType.Equals("RATE") && defectRate > toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty > ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "LE": //이하
                        if (checkType.Equals("RATE") && defectRate <= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty <= ngQty)
                        {

                        }
                        break;
                    case "LT": //미만
                        if (checkType.Equals("RATE") && defectRate < toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty < ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "EQ": //이퀄
                        if (checkType.Equals("RATE") && defectRate == toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("RATE") && chkDefectQty == ngQty)
                        {
                            bcheck = false;
                        }

                        break;
                }
            }

            //2020-03-06 강유라 영풍만 한시적으로 사진 등록 필수 해제   2021-02-09 오근영 INTERFLEX만 동작 조건 주석처리
            //if(UserInfo.Current.Enterprise.Equals("INTERFLEX"))
            //{ 
            //    if (!bcheck)
            //    {
            //        //throw MessageException.Create("CHECKIMEAGEBYNCR");
            //        ShowMessage(MessageBoxButtons.OK, "CHECKIMEAGEBYNCR", message);
            //    }
            //}
            if 
            (
                !bcheck
                &&
                bChk2
                &&
                bChk3
            )
            {
                //throw MessageException.Create("CHECKIMEAGEBYNCR");
                ShowMessage(MessageBoxButtons.OK, "CHECKIMEAGEBYNCR", message);
            }
            return bcheck;
        }

        private string checkNCR2(string defectGrade, double defectRate, decimal defectQty, string strDefectCode, string strDefectCodeName, string strQCSegmentID, string strQCSegmentName, bool bChk2, bool bChk3, string defectGradeName)
        {
            DataTable dt = grdNcrList.DataSource as DataTable;

            List<DataRow> listDr = dt.AsEnumerable().Where(c => c.Field<string>("DECISIONDEGREE").Equals(defectGrade)).ToList<DataRow>();

            bool bcheck = true;

            string message = string.Empty;

            foreach (DataRow dr in listDr)
            {
                string judgeType = Format.GetTrimString(dr["NGCONDITION"]);
                double fromRate = Format.GetDouble(dr["FROMRATE"], 0);
                double toRate = Format.GetDouble(dr["TORATE"], 0);
                double ngQty = Format.GetDouble(dr["NGQUANTITY"], 0);
                double chkDefectQty = Format.GetDouble(defectQty, 0);
                string checkType = Format.GetTrimString(dr["QTYORRATE"]);

                string spec = Format.GetTrimString(dr["NCRSPEC"]);
                string gcGrad = Format.GetTrimString(dr["QCGRADE"]);

                message = 
                    ""
                    + "\r\n"
                    + "-. "
                    //+ Language.Get("DEFECTCODENAME")
                    //+ " : "
                    + strDefectCodeName
                    + ", "
                    //+ Language.Get("DECISIONDEGREE") 
                    //+ " : " 
                    + defectGradeName
                    + ", "
                    //+ Language.Get("QCSEGMENTNAME")
                    //+ " : "
                    + strQCSegmentName
                    + ", "
                    ;
                if (checkType.Equals("QTY"))
                {
                    message = 
                        message 
                        //+ Language.Get("PCSDEFECTQTY") 
                        //+ " : " 
                        + defectQty 
                        + "PCS"
                        ;
                }
                else
                {
                    message = 
                        message 
                        //+ Language.Get("NCRDEFECTRATE") 
                        //+ " : " 
                        + defectRate.ToString("#,##0.00")
                        + "%"
                        ;
                }
                switch (judgeType)
                {
                    case "GE": //이상
                        if (checkType.Equals("RATE") && defectRate >= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty >= ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "BT": //사이
                        if (checkType.Equals("RATE") && defectRate >= fromRate && defectRate <= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && ngQty >= fromRate && ngQty <= toRate)
                        {
                            bcheck = false;
                        }
                        break;
                    case "GT": //초과
                        if (checkType.Equals("RATE") && defectRate > toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty > ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "LE": //이하
                        if (checkType.Equals("RATE") && defectRate <= toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty <= ngQty)
                        {
                            bcheck = false; // 기존 로직에서는 빠져 있었음.
                        }
                        break;
                    case "LT": //미만
                        if (checkType.Equals("RATE") && defectRate < toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("QTY") && chkDefectQty < ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                    case "EQ": //이퀄
                        if (checkType.Equals("RATE") && defectRate == toRate)
                        {
                            bcheck = false;
                        }
                        else if (checkType.Equals("RATE") && chkDefectQty == ngQty)
                        {
                            bcheck = false;
                        }
                        break;
                }
            }

            if 
                (
                !bcheck
                &&
                (
                    (
                        bChk2
                        &&
                        bChk3
                    )
                    &&
                    grdDefect.IsImageYNChecked
                )
                )
            {
                return message;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region ▶ 업로드제외불량사진등록 목록
        /// <summary>
        /// 업로드제외불량사진등록 목록
        /// </summary>
        private DataTable getRegistrationOfDefectImageExcudingUploadsInfo()
        {
            //conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CODECLASSID", "RegistrationOfDefectImageExcudingUploads");
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable dt = SqlExecuter.Query("GetCodeList", "00001", param);

            return dt;
        }
        #endregion


        #endregion
    }
}
 