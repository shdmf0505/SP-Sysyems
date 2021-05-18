#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls;
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

using DevExpress.XtraReports.UI;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
	/// 프 로 그 램 명  : 공정관리 > 포장관리 > 포장 실적 등록 (PG-SG-0630)
	/// 업  무  설  명  : 포장BOX별로 LOT의 포장작업 진행, BOX번호 생성 후 LOT정보 매핑
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-07-12
	/// 수  정  이  력  : 2019-09-20, 박정훈 :: 포장실적등록 화면 재구성 
    ///                  2020-02-25, 박정훈 :: Box자동생성 로직 추가
	/// 
	/// 
	/// </summary>
    public partial class PackingResult2 : SmartConditionManualBaseForm
    {
        #region ◆ Local Variables |

        // TODO : 화면에서 사용할 내부 변수 추가
        private decimal _panelPerQty = 0;
        private decimal _qty = 0;

        // 수출포장(Export) / 제품포장(Shipping)
        private string packingType = string.Empty;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public PackingResult2()
        {
            InitializeComponent();
        }
        #endregion

        #region ◆ 컨텐츠 영역 초기화 |
        /// <summary>
        /// Control 설정
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeControls();
            InitializePopupArea();
            InitializeComboBox();
            InitializeGrid();

            InitializeEvent();
            chkMixingLot.Enabled = false;
        }

        #region ▶ 조회조건 설정 |
        /// <summary>
        /// 검색조건 설정
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
        }

        /// <summary>
        /// 조회조건 설정
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }
        #endregion

        #region ▶ Control 기본 설정 |
        private void InitializeControls()
        {
            this.grpPackingSpec.Width = 390;

            this.grdLotList.Enabled = false;
            this.tabMain.Enabled = false;

            chkMixingLot.Size = new Size(120, 22);

            ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackingResult2));
            btnDocking.Image = ((System.Drawing.Image)Properties.Resources.btnDocking_Left);

            // 영풍 Case Tab 안보이도록 설정 (안씀...ㅡ.,ㅡ)
            tabMain.TabPages[5].PageVisible = false;

            if(UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
            {
                tabMain.TabPages[1].PageVisible = false;
                tabMain.TabPages[4].PageVisible = false;

                //grdXOUT.Visible = false;

                btnPrintLabelXOUTInner.Visible = false;
                btnPrintLabelXOUTOuter.Visible = false;
                btnSaveXOUT.Visible = false;

                pnlXOUT.Visible = false;
            }
        }
        #endregion

        #region ▶ ComboBox 초기화 |
        /// <summary>
        /// 콤보박스를 초기화 하는 함수
        /// </summary>
        private void InitializeComboBox()
        {
            
        }
        #endregion

        #region ▶ Grid 설정 |
        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - Lot Grid |
            grdLotList.GridButtonItem = GridButtonItem.Delete;

            // CheckBox 설정
            grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLotList.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 220).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 60).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("WEEK", 80).SetIsReadOnly(); 
            grdLotList.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("GOODQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PACKINGQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("LEFTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PANELPERQTY", 80).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("INPUTDATE", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTIONORDERID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("DUEDATE", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTDEFTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PRODUCTIONTYPE", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("CUSTOMERNAME", 100).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLotList.View.AddTextBoxColumn("PCSPNL", 60).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PCSARY", 60).SetIsHidden();
            grdLotList.View.AddTextBoxColumn("PROCESSUOM", 60).SetIsHidden();


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

            #region - Defect Grid |
            grdDefect.VisibleLotId = true;
            grdDefect.VisibleTopDefectCode = true;
            grdDefect.InitializeControls();
            grdDefect.InitializeEvent();
            #endregion

            #region - Box Grid |
            grdBox.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

            // CheckBox 설정
            grdBox.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdBox.View.AddTextBoxColumn("BOXNO", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdBox.View.AddTextBoxColumn("PCSPERBOX", 80).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdBox.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdBox.View.AddTextBoxColumn("LEFTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdBox.View.PopulateColumns();
            #endregion

            #region - Box Lot Grid |
            grdBoxLot.GridButtonItem = GridButtonItem.Delete;

            // CheckBox 설정
            grdBoxLot.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdBoxLot.View.AddTextBoxColumn("BOXNO", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdBoxLot.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdBoxLot.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdBoxLot.View.AddTextBoxColumn("PRODUCTDEFNAME", 180).SetIsReadOnly();
            grdBoxLot.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdBoxLot.View.AddTextBoxColumn("WEEK", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdBoxLot.View.AddTextBoxColumn("PACKINGWEEK", 80).SetTextAlignment(TextAlignment.Center);
            grdBoxLot.View.AddTextBoxColumn("LOTSPLIT", 60).SetTextAlignment(TextAlignment.Center);
            grdBoxLot.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
            if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
            {
                grdBoxLot.View.AddTextBoxColumn("PCSPNL", 80).SetIsHidden();
                grdBoxLot.View.AddTextBoxColumn("PCSARY", 80).SetLabel("ARRAYQTY").SetTextAlignment(TextAlignment.Right);
                grdBoxLot.View.AddTextBoxColumn("CARD", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
                grdBoxLot.View.AddTextBoxColumn("XOUT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
                grdBoxLot.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            }
            else
            {
                grdBoxLot.View.AddTextBoxColumn("PCSPNL", 80).SetIsHidden();
                grdBoxLot.View.AddTextBoxColumn("PCSARY", 80).SetIsHidden();
                grdBoxLot.View.AddTextBoxColumn("CARD", 80).SetIsHidden();
                grdBoxLot.View.AddTextBoxColumn("XOUT", 80).SetIsHidden();
                grdBoxLot.View.AddTextBoxColumn("DEFECTQTY", 80).SetIsHidden();
            }

            grdBoxLot.View.PopulateColumns();
            #endregion

            #region - Label Print |
            grdLabel.GridButtonItem = GridButtonItem.None;

            // CheckBox 설정
            grdLabel.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLabel.View.AddTextBoxColumn("BOXNO", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdLabel.View.AddTextBoxColumn("WEEK", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel.View.AddTextBoxColumn("LOTID", 170).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdLabel.View.PopulateColumns();
            #endregion

            #region - Label2 Print |
            grdLabel2.GridButtonItem = GridButtonItem.None;

            // CheckBox 설정
            grdLabel2.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLabel2.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("PCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("PCSPNL", 80).SetIsHidden();
            grdLabel2.View.AddTextBoxColumn("PCSARY", 80).SetIsHidden();
            grdLabel2.View.AddTextBoxColumn("XOUT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("CARD", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("PACK", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel2.View.AddTextBoxColumn("CASECNT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdLabel2.View.PopulateColumns();
            #endregion

            #region - Label2 Print Lot |
            grdPrintLot2.GridButtonItem = GridButtonItem.None;

            // CheckBox 설정
            grdPrintLot2.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdPrintLot2.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot2.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot2.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot2.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdPrintLot2.View.AddTextBoxColumn("PCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPrintLot2.View.AddTextBoxColumn("WEEK", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot2.View.AddTextBoxColumn("PACKINGWEEK", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            grdPrintLot2.View.PopulateColumns();
            #endregion

            #region - X-OUT Grid |
            grdXOUT.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

            // CheckBox 설정
            grdXOUT.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdXOUT.View.AddTextBoxColumn("XOUT", 60).SetTextAlignment(TextAlignment.Right);
            grdXOUT.View.AddTextBoxColumn("CARD", 60).SetTextAlignment(TextAlignment.Right);
            grdXOUT.View.AddTextBoxColumn("GOODQTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdXOUT.View.AddTextBoxColumn("DEFECTQTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdXOUT.View.PopulateColumns();

            InitializationXOUTSummaryRow();
            #endregion

            #region - Label3 Print |
            grdLabel3.GridButtonItem = GridButtonItem.None;

            // CheckBox 설정
            grdLabel3.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdLabel3.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("PCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("PCSPNL", 80).SetIsHidden();
            grdLabel3.View.AddTextBoxColumn("XOUT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("CARD", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("PACK", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdLabel3.View.AddTextBoxColumn("CASECNT", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdLabel3.View.PopulateColumns();
            #endregion

            #region - Label3 Print Lot |
            grdPrintLot3.GridButtonItem = GridButtonItem.None;

            // CheckBox 설정
            grdPrintLot3.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdPrintLot3.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot3.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot3.View.AddTextBoxColumn("PRODUCTDEFID", 120).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot3.View.AddTextBoxColumn("PRODUCTDEFNAME", 200).SetIsReadOnly();
            grdPrintLot3.View.AddTextBoxColumn("PCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPrintLot3.View.AddTextBoxColumn("WEEK", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdPrintLot3.View.AddTextBoxColumn("PACKINGWEEK", 80).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            grdPrintLot3.View.PopulateColumns();
            #endregion

            #region - Case Grid |
            grdCase.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;

            // CheckBox 설정
            grdCase.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdCase.View.AddTextBoxColumn("CASENO", 60).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            grdCase.View.AddTextBoxColumn("QTY", 60).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();

            grdCase.View.PopulateColumns();

            InitializationCaseSummaryRow();
            #endregion
        }
        #endregion

        #region ▶ 작업장 팝업 초기화 및 작업자 초기화 |
        /// <summary>
        /// 작업장 팝업 초기화 및 작업자 초기화
        /// </summary>
        private void InitializePopupArea()
        {
            ConditionItemSelectPopup areaCondition = new ConditionItemSelectPopup();
            areaCondition.Id = "AREA";
            areaCondition.SearchQuery = new SqlQuery("GetAreaListByAuthority", "10001", $"PLANTID={UserInfo.Current.Plant}", "ISMODIFY=Y", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("SELECTAREA", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetPopupApplySelection((selectedRows, dataGridRows) =>
            {
                //작업장 변경 시 작업자 조회
                string areaId = string.Empty;
                selectedRows.Cast<DataRow>().ForEach(row =>
                {
                    areaId = Format.GetString(row["AREAID"], "");
                });

                if (string.IsNullOrEmpty(areaId)) return;

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("AreaId", areaId);
                param.Add("EnterpriseId", UserInfo.Current.Enterprise);
                param.Add("PlantId", UserInfo.Current.Plant);

                cboWorker.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboWorker.ValueMember = "USERID";
                cboWorker.DisplayMember = "WORKERNAME";
                cboWorker.DataSource = SqlExecuter.Query("GetAreaworkerList", "10001", param);
                cboWorker.ShowHeader = false;

                DataTable dtuser = cboWorker.DataSource as DataTable;
                if (dtuser.Rows.Count == 0)
                {
                    //현 작업장에 작업자가 없습니다. 작업자 등록 후 사용 가능합니다.
                    ShowMessage("UserEmptyInArea");
                    return;
                }

                List<DataRow> list = dtuser.AsEnumerable().Where(r => r["USERID"].Equals(UserInfo.Current.Id)).ToList();
                cboWorker.EditValue = (list.Count > 0) ? list[0]["USERID"].ToString() : dtuser.Rows[0]["USERID"];

            });


            areaCondition.Conditions.AddTextBox("AREA");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150);
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);

            popArea.SelectPopupCondition = areaCondition;
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
            txtLotID.Editor.KeyDown += TxtLotId_KeyDown;
            txtPCSBox.KeyDown += TxtPCSBox_KeyDown;

            // Button Event
            btnDocking.Click += BtnDocking_Click;           // 포장사양 Panel 보이기 & 감추기 설정
            btnAutoCreateBox.Click += BtnAutoCreateBox_Click;
            btnPacking.Click += BtnPacking_Click;           // Box 생성
            btnLabelPrint.Click += BtnLabelPrint_Click;     // Box 라벨 Print
            btnSaveXOUT.Click += BtnSaveXOUT_Click;         // X-OUT 데이터 저장
            btnPrintLabelXOUTOuter.Click += BtnPrintLabelXOUTOuter_Click;     // X-OUT 라벨 Print
            btnPrintLabelXOUTInner.Click += BtnPrintLabelXOUTInner_Click;
            btnSaveCase.Click += BtnSaveCase_Click;                 // Case 데이터 저장
            btnPrintLabelCase.Click += BtnPrintLabelCase_Click;     // Case 라벨 Print

            // Grid Event
            grdLotList.View.DoubleClick += View_DoubleClick;
            grdLotList.View.RowStyle += View_RowStyle;
            grdLotList.View.CustomDrawFooterCell += View_CustomDrawFooterCell;
            grdLotList.ToolbarDeleteRow += LotList_DeleteRow;

            grdDefect.DefectQtyChanged += GrdDefect_DefectQtyChanged;
            grdDefect.View.AddingNewRow += Defect_AddingNewRow;

            grdBox.View.AddingNewRow += GrdBoxView_AddingNewRow;
            grdBox.ToolbarDeletingRow += GrdBox_DeletingRow;

            grdBoxLot.ToolbarDeletingRow += GrdBoxLot_ToolbarDeletingRow; ;
            grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;

            grdLabel.View.RowCellClick += GrdLabelView_RowCellClick;

            grdLabel2.View.RowCellClick += GrdLabel2View_RowCellClick;
            grdLabel2.View.CellValueChanged += GrdLabel2View_CellValueChanged;

            // X-OUT Grid Event
            grdXOUT.View.AddingNewRow += GrdXOUTView_AddingNewRow;
            grdXOUT.ToolbarDeletingRow += GrdXOUT_DeletingRow;
            grdXOUT.View.CellValueChanged += GrdXOUTView_CellValueChanged;
            grdXOUT.View.CustomDrawFooterCell += GrdXOUTView_CustomDrawFooterCell;
            grdXOUT.View.RowCellStyle += GrdXOUTView_RowCellStyle;

            grdLabel3.View.RowCellClick += GrdLabel3View_RowCellClick;

            // Case Grid Event
            grdCase.View.CustomDrawFooterCell += GrdCaseView_CustomDrawFooterCell;
        }

        

        #region ▶ TextBox Event |

        #region - Lot 조회|
        /// <summary>
        /// Lot 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            btnPrintLabel.Enabled = false;

            if (e.KeyCode == Keys.Enter)
            {
                string currentAreaId = Format.GetString(popArea.GetValue(), "");
                if (string.IsNullOrEmpty(currentAreaId))
                {
                    //작업장을 선택하세요.
                    throw MessageException.Create("NoAreaSelected");
                }

                string currentUserId = Format.GetString(cboWorker.GetDataValue(), "");
                if (string.IsNullOrEmpty(currentUserId))
                {
                    //작업자를 선택하세요.
                    throw MessageException.Create("NoSelectWorker");
                }

                if (string.IsNullOrWhiteSpace(this.txtLotID.Text.Trim())) return;

                SearchLot();

                string lotId = string.Join(",", (grdLotList.DataSource as DataTable).AsEnumerable().Select(c => Format.GetString(c["LOTID"])).Distinct());

                if (!UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
                {
                    grdDefect.SetConsumableDefComboBox(lotId);
                }
            }
        }
        #endregion

        #region - Box 수량 |
        /// <summary>
        /// Box 수량
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPCSBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (this.txtPCSBox.EditValue.Equals("0"))
            {
                // Box수량을 입력해야 합니다.
                ShowMessage("BoxQtyRequired");
                return;
            }
            // Defect 정보 체크
            if (this.tabMain.TabPages[1].PageEnabled)
            {
                grdDefect.CheckDefect();

                // Box작업 시작시 불량 탭 선택 못하도록 설정
                this.tabMain.TabPages[1].PageEnabled = false;
            }

            grdBox.View.AddNewRow();
        } 
        #endregion
        
        #endregion

        #region ▶ Grid Event |

        #region - Lot 목록 더블클릭 |
        /// <summary>
        /// Lot 목록 더블클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (tabMain.SelectedTabPageIndex == 1)
            {
                if (this.tabMain.TabPages[1].PageEnabled)
                {
                    grdDefect.View.AddNewRow();
                    // 이후 Defect Row 추가되면 추가 이벤트에서 처리
                }
            }
        }
        #endregion

        #region - Lot 목록 Row Style 설정 |
        /// <summary>
        /// Lot 목록 Row Style 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            CommonFunction.SetGridRowStyle(grdLotList, e);
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
                int qoodQtySum = 0;
                int packingSum = 0;

                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    PanelSum += Format.GetInteger(row["PANELQTY"]);
                    qtySum += Format.GetInteger(row["QTY"]);
                    qoodQtySum += Format.GetInteger(row["GOODQTY"]);
                    packingSum += Format.GetInteger(row["PACKINGQTY"]); 
                });

                if (e.Column.FieldName == "PANELQTY")
                {
                    e.Info.DisplayText = Format.GetString(PanelSum);
                }
                if (e.Column.FieldName == "QTY")
                {
                    e.Info.DisplayText = Format.GetString(qtySum);
                }
                if (e.Column.FieldName == "GOODQTY")
                {
                    e.Info.DisplayText = Format.GetString(qoodQtySum);
                }
                if (e.Column.FieldName == "PACKINGQTY")
                {
                    e.Info.DisplayText = Format.GetString(packingSum);
                }
            }
            else
            {
                grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = "  ";
                grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
                grdLotList.View.Columns["GOODQTY"].SummaryItem.DisplayFormat = "  ";
                grdLotList.View.Columns["PACKINGQTY"].SummaryItem.DisplayFormat = "  "; 
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
            CheckDefectQty(e.RowHandle);
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

            grdDefect.SetInfo(CalQty, Format.GetInteger(dr["QTY"].ToString()));
            //grdDefect.View.SetFocusedRowCellValue("LOTID", lotid);
            args.NewRow["LOTID"] = lotid;

            grdDefect.LotID = lotid;
        }
        #endregion

        #region - LOT LIST의 ROW 제거 |
        /// <summary>
        /// LOT LIST의 ROW 제거
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LotList_DeleteRow(object sender, EventArgs e)
        {
            DataTable dt = grdLotList.DataSource as DataTable;

            if(dt != null && dt.Rows.Count == 0)
            {
                ClearPackingSpec();
                grdDefect.View.ClearDatas();
                grdBoxLot.View.ClearDatas();
                grdLabel.View.ClearDatas();
                grdLabel2.View.ClearDatas();
                grdPrintLot2.View.ClearDatas();
                grdXOUT.View.ClearDatas();
                grdLabel3.View.ClearDatas();
                grdPrintLot3.View.ClearDatas();
                grdCase.View.ClearDatas();

                this.txtPCSBox.EditValue = string.Empty;

                this.tabMain.TabPages[1].PageEnabled = true;
            }
        }
        #endregion

        #region - Box Lot List의 Cell Value Changed |
        /// <summary>
        /// Box Lot List의 Cell Value Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdBoxLotView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdBoxLot.View.CellValueChanged -= GrdBoxLotView_CellValueChanged;

            if (e.Column.FieldName == "QTY")
            {
                DataRow dr = grdBoxLot.View.GetFocusedDataRow();

                if (dr["LOTID"] == null) return;

                DataTable dt = grdBoxLot.DataSource as DataTable;
                DataTable lotDt = grdLotList.DataSource as DataTable;

                var chk = from row in dt.AsEnumerable()
                          where object.ReferenceEquals(row.Field<string>("LOTID"), dr["LOTID"].ToString())
                          select row
                          ;

                if(chk.Count() == 0)
                {
                    grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;

                    return;
                }
                var boxRt = dt.AsEnumerable().Where(r => r.Field<string>("LOTID").Equals(dr["LOTID"].ToString())).Sum(r => Format.GetInteger(r.Field<object>("QTY")));
                var lotRt = lotDt.AsEnumerable().Where(r => r.Field<string>("LOTID").Equals(dr["LOTID"].ToString())).Select(r => r.Field<object>("GOODQTY")).FirstOrDefault();

                if (Format.GetInteger(boxRt.ToString()) > Format.GetInteger(lotRt.ToString()))
                {
                    // 포장수량이 LOT수량을 초과할 수 없습니다.
                    MSGBox.Show(MessageBoxType.Warning, "OverPackingQty");

                    grdBoxLot.View.SetFocusedValue(0);

                    GetSumPackingCount("ADD");

                    grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;

                    return;
                }

                DataTable packingData = grdBox.DataSource as DataTable;
                var packRt = packingData.AsEnumerable().Where(r => r.Field<object>("BOXNO").ToString().Equals(dr["BOXNO"].ToString()))
                                                       .Sum(r => Format.GetInteger(r.Field<object>("LEFTQTY")) + Format.GetInteger(r.Field<object>("QTY")));
                var blotRt = dt.AsEnumerable().Where(r => r.Field<object>("BOXNO").ToString().Equals(dr["BOXNO"].ToString()))
                                                       .Sum(r => Format.GetInteger(r.Field<object>("QTY")));

                if (Format.GetInteger(blotRt.ToString()) > Format.GetInteger(packRt.ToString()))
                {
                    // 포장수량을 초과할 수 없습니다.
                    MSGBox.Show(MessageBoxType.Warning, "OverBoxPackingQty");

                    grdBoxLot.View.SetFocusedValue(0);

                    GetSumPackingCount("ADD");

                    grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;

                    return;
                }

                GetSumPackingCount("ADD");
            }
            else if (e.Column.FieldName == "CARD")
            {
                DataRow dr = grdBoxLot.View.GetFocusedDataRow();

                if (dr != null || dr["PCSARY"] == null || Format.GetInteger(dr["PCSARY"].ToString()) == 0)
                {
                    // 기준정보에 PCS/ARY 정보가 없습니다
                    MSGBox.Show(MessageBoxType.Warning, "STDNoExistPCSARY");

                    grdBoxLot.View.SetFocusedValue(null);

                    grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;

                    return;
                }

                int ary = Format.GetInteger(dr["PCSARY"].ToString());
                int pcspnl = Format.GetInteger(dr["PCSPNL"].ToString());
                int block = Format.GetInteger(dr["CARD"].ToString());

                int xout = (pcspnl / ary) * block;

                grdBoxLot.View.SetFocusedRowCellValue("XOUT", xout);
                grdBoxLot.View.SetFocusedRowCellValue("DEFECTQTY", xout * block);
            }
            grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;
        }
        #endregion

        #region - Packing 데이터 제거 |
        /// <summary>
        /// Packing 데이터 제거
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdBoxLot_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            DataTable dt = grdBoxLot.DataSource as DataTable;

            DataTable bxDt = grdBoxLot.View.GetCheckedRows();

            // Cell Event로 인해 무한 루프돌며 일어나는 문제 방지
            grdBoxLot.View.CellValueChanged -= GrdBoxLotView_CellValueChanged;

            if (dt == null || dt.Rows.Count == 0)
                GetSumPackingCount("ALL");
            else
                GetSumPackingCount("DEL");

            grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;
        }
        #endregion

        #region - Box Row 추가 |
        /// <summary>
        /// Box Row 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdBoxView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            if(this.txtPCSBox.EditValue.Equals("0"))
            {
                // Box수량을 입력해야 합니다.
                ShowMessage("BoxQtyRequired");
                args.IsCancel = true; 
            }
            // Defect 정보 체크
            if (this.tabMain.TabPages[1].PageEnabled)
            {
                grdDefect.CheckDefect();

                // Box작업 시작시 불량 탭 선택 못하도록 설정
                this.tabMain.TabPages[1].PageEnabled = false;
            }

            int boxCount = 0;

            DataTable boxdata = grdBox.DataSource as DataTable;

            var maxBoxNo = boxdata.AsEnumerable().Max(r => r.Field<object>("BOXNO"));

            if (maxBoxNo == null)
                boxCount++;
            else
                boxCount = Format.GetInteger(maxBoxNo.ToString()) + 1;

            grdBox.View.CheckRow(sender.RowCount -1, true);
            grdBox.View.SetFocusedRowCellValue("BOXNO", boxCount);
            grdBox.View.SetFocusedRowCellValue("PCSPERBOX", Format.GetInteger(this.txtPCSBox.EditValue));
            grdBox.View.SetFocusedRowCellValue("QTY", 0);
            grdBox.View.SetFocusedRowCellValue("LEFTQTY", Format.GetInteger(this.txtPCSBox.EditValue));
        }
        #endregion

        #region - Box Row 제거 |
        /// <summary>
        /// Box Row 제거
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdBox_DeletingRow(object sender, CancelEventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "IsDeleted"); //삭제하시겠습니까? 

            if (result == DialogResult.Yes)
            {
                GetSumPackingCount("BOX");
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region - 라벨출력 Grid RowCell Click |
        /// <summary>
        /// 라벨출력 Grid RowCell Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLabelView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdLabel.View.GetFocusedDataRow();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt = SqlExecuter.Query("SelectPackingLotList", "10001", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdPrintLot2.DataSource = dt;

            grdXOUT.View.ClearDatas();

            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt2 = SqlExecuter.Query("SelectPackingXOutList", "10001", param2);

            grdXOUT.DataSource = dt2;

            if (dt2 != null && dt2.Rows.Count > 1)
            {
                this.btnSaveXOUT.Enabled = false;
            }
            else
            {
                this.btnSaveXOUT.Enabled = true;
            }
        }
        #endregion

        #region - 라벨출력2 Grid RowCell Click |
        /// <summary>
        /// 라벨출력 Grid RowCell Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLabel2View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdLabel2.View.GetFocusedDataRow();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt = SqlExecuter.Query("SelectPackingLotList", "10001", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdPrintLot2.DataSource = dt;

            grdXOUT.View.ClearDatas();

            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt2 = SqlExecuter.Query("SelectPackingXOutList", "10001", param2);

            grdXOUT.DataSource = dt2;

            if(dt2 != null && dt2.Rows.Count > 1)
            {
                this.btnSaveXOUT.Enabled = false;
            }
            else
            {
                this.btnSaveXOUT.Enabled = true;
            }
        }
        #endregion

        #region - 라벨2 CARD 수 입력시 X-OUT 계산 |
        /// <summary>
        /// 라벨2 CARD 수 입력시 X-OUT 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLabel2View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdLabel2.View.CellValueChanged -= GrdLabel2View_CellValueChanged;

            if (e.Column.FieldName == "CARD")
            {
                DataRow dr = grdLabel2.View.GetFocusedDataRow();

                if(dr != null || dr["PCSARY"] == null)
                {
                    // 기준정보에 PCS/ARY 정보가 없습니다
                    MSGBox.Show(MessageBoxType.Warning, "STDNoExistPCSARY");

                    grdLabel2.View.SetFocusedValue(null);

                    grdLabel2.View.CellValueChanged += GrdLabel2View_CellValueChanged;

                    return;
                }

                int ary = Format.GetInteger(dr["PCSARY"].ToString());
                int pcspnl = Format.GetInteger(dr["PCSPNL"].ToString());
                int block = Format.GetInteger(dr["CARD"].ToString());

                int xout = (pcspnl / ary) * block;

                grdLabel2.View.SetFocusedRowCellValue("XOUT", xout);
                grdLabel2.View.SetFocusedRowCellValue("DEFECTQTY", xout * block);
            }
            grdLabel2.View.CellValueChanged += GrdLabel2View_CellValueChanged;
        } 
        #endregion

        #region - X-OUT Row 추가 |
        /// <summary>
        /// X-OUT Row 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrdXOUTView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow dr = grdLabel2.View.GetFocusedDataRow();
            string pnlPerQty = dr["PCSPNL"].ToString();

            grdXOUT.View.SetFocusedRowCellValue("GOODQTY", pnlPerQty);
        }
        #endregion

        #region - X-OUT Row 제거 |
        /// <summary>
        /// X-OUT Row 제거
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdXOUT_DeletingRow(object sender, CancelEventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            result = this.ShowMessage(MessageBoxButtons.YesNo, "IsDeleted"); //삭제하시겠습니까? 

            if (result == DialogResult.Yes)
            {
                //GetSumPackingCount("BOX");
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region - X-OUT Grid의 Cell Value Changed |
        /// <summary>
        /// X-OUT의 Cell Value Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdXOUTView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grdXOUT.View.CellValueChanged -= GrdXOUTView_CellValueChanged;

            DataRow dr = grdXOUT.View.GetFocusedDataRow();

            if (dr["GOODQTY"] == null) return;

            if (e.Column.FieldName == "XOUT" || e.Column.FieldName == "CARD")
            {
                DataRow boxdr = grdLabel2.View.GetFocusedDataRow();
                string pnlPerQty = boxdr["PCSPNL"].ToString();
                string boxQty = boxdr["PCSQTY"].ToString();

                if(e.Column.FieldName == "XOUT" && !string.IsNullOrWhiteSpace(dr["CARD"].ToString()))
                {
                    int xout = Format.GetInteger(grdXOUT.View.GetFocusedRowCellValue("XOUT").ToString());
                    int card = Format.GetInteger(grdXOUT.View.GetFocusedRowCellValue("CARD").ToString());
                    int goodQty = Format.GetInteger(grdXOUT.View.GetFocusedRowCellValue("GOODQTY").ToString());

                    grdXOUT.View.SetFocusedRowCellValue("DEFECTQTY", xout * card);
                    grdXOUT.View.SetFocusedRowCellValue("GOODQTY", (Format.GetInteger(pnlPerQty) * card) - (xout * card));
                }

                if (e.Column.FieldName == "CARD")
                {
                    int card = Format.GetInteger(grdXOUT.View.GetFocusedRowCellValue("CARD").ToString());

                    if (dr["XOUT"] == null || string.IsNullOrWhiteSpace(dr["XOUT"].ToString()))
                    {
                        grdXOUT.View.SetFocusedRowCellValue("GOODQTY", Format.GetInteger(pnlPerQty) * card);
                    }
                    else if(dr["XOUT"] != null && !string.IsNullOrWhiteSpace(dr["XOUT"].ToString()))
                    {
                        int xout = Format.GetInteger(grdXOUT.View.GetFocusedRowCellValue("XOUT").ToString());

                        grdXOUT.View.SetFocusedRowCellValue("DEFECTQTY", xout * card);
                        grdXOUT.View.SetFocusedRowCellValue("GOODQTY", (Format.GetInteger(pnlPerQty) * card) - (xout * card));
                    }
                }

                DataTable dt = grdXOUT.DataSource as DataTable;

                var caseRt = dt.AsEnumerable().Sum(r => Format.GetInteger(r.Field<object>("GOODQTY")));

                if (Format.GetInteger(caseRt.ToString()) > Format.GetInteger(boxQty.ToString()))
                {
                    // 포장수량을 초과할 수 없습니다.
                    MSGBox.Show(MessageBoxType.Warning, "OverBoxPackingQty");

                    grdXOUT.View.SetFocusedRowCellValue("XOUT", 0);
                    grdXOUT.View.SetFocusedRowCellValue("CARD", 0);

                    grdXOUT.View.CellValueChanged += GrdXOUTView_CellValueChanged;

                    return;
                }
            }

            grdXOUT.View.CellValueChanged += GrdXOUTView_CellValueChanged;
        }
        #endregion

        #region - X-OUT Grid Footer Sum Event |
        /// <summary>
        /// Grid Footer Sum Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdXOUTView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdXOUT.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int xoutSum = 0;
                int cardSum = 0;
                int qoodQtySum = 0;
                int defectSum = 0;

                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    xoutSum += Format.GetInteger(row["XOUT"]);
                    cardSum += Format.GetInteger(row["CARD"]);
                    qoodQtySum += Format.GetInteger(row["GOODQTY"]);
                    defectSum += Format.GetInteger(row["DEFECTQTY"]);
                });

                if (e.Column.FieldName == "XOUT")
                {
                    e.Info.DisplayText = Format.GetString(xoutSum);
                }
                if (e.Column.FieldName == "CARD")
                {
                    e.Info.DisplayText = Format.GetString(cardSum);
                }
                if (e.Column.FieldName == "GOODQTY")
                {
                    e.Info.DisplayText = Format.GetString(qoodQtySum);
                }
                if (e.Column.FieldName == "DEFECTQTY")
                {
                    e.Info.DisplayText = Format.GetString(defectSum);
                }
            }
            else
            {
                grdXOUT.View.Columns["XOUT"].SummaryItem.DisplayFormat = "  ";
                grdXOUT.View.Columns["CARD"].SummaryItem.DisplayFormat = "  ";
                grdXOUT.View.Columns["GOODQTY"].SummaryItem.DisplayFormat = "  ";
                grdXOUT.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion

        #region - X-OUT 목록 RowCellStyle Event |
        /// <summary>
        /// X-OUT 목록 RowCellStyle Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdXOUTView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "XOUT")
            {
                e.Appearance.BackColor = Color.Aqua;
            }
            if (e.Column.FieldName == "CARD")
            {
                e.Appearance.BackColor = Color.Aqua;
            }
        }
        #endregion

        #region - 라벨출력 Grid3 RowCell Click |
        /// <summary>
        /// 라벨출력 Grid RowCell Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdLabel3View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = grdLabel3.View.GetFocusedDataRow();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt = SqlExecuter.Query("SelectPackingLotList", "10001", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            grdPrintLot3.DataSource = dt;

            Dictionary<string, object> param2 = new Dictionary<string, object>();
            param2.Add("P_BOXNO", dr["BOXNO"].ToString());

            DataTable dt2 = SqlExecuter.Query("SelectPackingCaseList", "10001", param2);

            this.grdCase.DataSource = dt2;

            if (dt2 != null && dt2.Rows.Count > 1)
            {
                this.btnSaveCase.Enabled = false;
            }
            else
            {
                this.btnSaveCase.Enabled = true;

                // Case 자동생성

                string boxQty = dr["PCSQTY"].ToString();
                string pnlPerQty = dr["PCSPNL"].ToString();

                grdCase.View.ClearDatas();

                int caseCount = 0;

                DataTable casedata = grdCase.DataSource as DataTable;

                var maxCaseNo = casedata.AsEnumerable().Max(r => r.Field<object>("CASENO"));

                bool chk = true;

                while (chk)
                {
                    int qty = Format.GetInteger(pnlPerQty);

                    if (maxCaseNo == null)
                        caseCount++;
                    else
                        caseCount = Format.GetInteger(maxCaseNo.ToString()) + 1;

                    DataTable rowDt = grdCase.DataSource as DataTable;

                    var qtySum = 0;

                    if(rowDt != null && rowDt.Rows.Count > 0)
                        qtySum = rowDt.AsEnumerable().Sum(r => r.Field<int>("QTY"));

                    if (Format.GetInteger(boxQty) <= Format.GetInteger(qtySum.ToString()))
                    {
                        break;
                    }
                    else if (Format.GetInteger(boxQty) < Format.GetInteger(qtySum.ToString()) + qty)
                    {
                        qty = Format.GetInteger(boxQty) - Format.GetInteger(qtySum.ToString());
                        chk = false;
                    }
                    grdCase.View.AddNewRow();

                    grdCase.View.SetFocusedRowCellValue("CASENO", caseCount);
                    grdCase.View.SetFocusedRowCellValue("QTY", qty);
                }
            }
        }
        #endregion

        #region - Case Grid Footer Sum |
        /// <summary>
        /// Case Grid Footer Sum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdCaseView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = grdCase.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                int QtySum = 0;

                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    QtySum += Format.GetInteger(row["QTY"]);
                });

                if (e.Column.FieldName == "QTY")
                {
                    e.Info.DisplayText = Format.GetString(QtySum);
                }
            }
            else
            {
                grdCase.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
        }
        #endregion

        #endregion

        #region ▶ Button Event |
        
        #region - BOX 자동생성 |
        /// <summary>
        /// BOX 자동생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAutoCreateBox_Click(object sender, EventArgs e)
        {
            grdBox.View.ClearDatas();

            // Lot Check
            DataTable lotDt = grdLotList.View.GetCheckedRows();

            if (lotDt == null || lotDt.Rows.Count == 0)
            {
                // LOT을 선택하여 주십시오.
                ShowMessage("NoSeletedLot");
                return;
            }

            if (this.txtPCSBox.EditValue.Equals("0"))
            {
                // Box수량을 입력하여 주십시오
                ShowMessage("NoExistBoxQty");
                return;
            }

            // LOT의 잔량 체크
            DataTable boxDt = grdBox.DataSource as DataTable;

            var result = lotDt.AsEnumerable().Sum(r => Format.GetInteger(r["LEFTQTY"]));

            var boxSum = boxDt.AsEnumerable().Sum(r => Format.GetInteger(r["LEFTQTY"]));

            int boxqty = Format.GetInteger(this.txtPCSBox.EditValue);

            int boxRow = 0;
            if (chkMixingLot.Checked)
            {
                boxRow = Format.GetInteger(Math.Truncate((result.ToDecimal() - boxSum.ToDecimal()) / boxqty.ToDecimal()));

                decimal remain = (result.ToDecimal() - boxSum.ToDecimal()) % boxqty.ToDecimal();

                if (remain > 0 && boxqty > remain)
                {
                    boxRow++;
                }

                for (int i = 1; i <= boxRow; i++)
                {
                    grdBox.View.AddNewRow();
                }
            }
            else
            {
                for (int i = 0; i < lotDt.Rows.Count; i++)
                {
                    DataRow ldr = lotDt.Rows[i];
                    int leftQty = Format.GetInteger(ldr["LEFTQTY"]);

                    if (boxqty > leftQty)
                    {
                        boxRow = 1;
                    }
                    else
                    {
                        boxRow = Format.GetInteger(Math.Truncate(Format.GetDecimal(leftQty / boxqty)));
                        decimal remain = leftQty % boxqty;

                        if (remain > 0 && boxqty > remain)
                        {
                            boxRow++;
                        }
                    }

                    for (int k = 1; k <= boxRow; k++)
                    {
                        grdBox.View.AddNewRow();
                    }
                }
            }
        } 
        #endregion

        #region - 포장 작업 시작 |
        /// <summary>
        /// 포장 작업 시작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPacking_Click(object sender, EventArgs e)
        {
            // Defect 정보 체크
            grdDefect.CheckDefect();

            this.tabMain.TabPages[1].PageEnabled = false;

            // Box 추가 Row 체크
            DataTable bxData = grdBox.View.GetCheckedRows();

            if (bxData == null || bxData.Rows.Count == 0)
            {
                ShowMessage("NotCreateBoxNo");
                return;
            }

            foreach (DataRow bxRow in bxData.Rows)
            {
                // Lot Check
                DataTable lotDt = grdLotList.View.GetCheckedRows();

                if (lotDt == null || lotDt.Rows.Count == 0)
                {
                    // LOT을 선택하여 주십시오.
                    ShowMessage("NoSeletedLot");
                    return;
                }

                int boxleftqty = Format.GetInteger(bxRow["LEFTQTY"].ToString());

                if (boxleftqty == 0)
                {
                    //박스 수량이 부족합니다.
                    ShowMessage("NotEnoughBoxQty");
                    return;
                }

                // Cell Event로 인해 무한 루프돌며 일어나는 문제 방지
                grdBoxLot.View.CellValueChanged -= GrdBoxLotView_CellValueChanged;

                SetBoxPacking(bxRow, lotDt);

                grdBoxLot.View.CellValueChanged += GrdBoxLotView_CellValueChanged;
            }
        }
        #endregion

        #region - 포장 라벨출력 |
        /// <summary>
        /// 포장 라벨출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLabelPrint_Click(object sender, EventArgs e)
        {
            DataTable dtChange = grdLabel.View.GetCheckedRows();

            if (dtChange == null || dtChange.Rows.Count == 0) return;

            string boxNos = string.Empty;
            foreach (DataRow each in grdLabel.View.GetCheckedRows().Rows)
            {
                if (each["BOXNO"] != DBNull.Value)
                {
                    boxNos += each["BOXNO"].ToString() + ",";
                }
            }
            if (boxNos.EndsWith(","))
            {
                boxNos = boxNos.Substring(0, boxNos.Length - 1);
            }
            PrintPackingLabelPopup4 pop = new PrintPackingLabelPopup4(boxNos);
            pop.ShowDialog();
            /*
            var query = from dr in dtChange.AsEnumerable()
                        group dr by dr["BOXNO"] into dg
                        select new
                        {
                            boxno = dg.Key
                        };
            List<LabelInfo> viewList = new List<LabelInfo>();

            string isShipping = "N";
            foreach (var x in query)
            {
                if (packingType.Equals("Export"))
                    isShipping = "Y";
                else
                    isShipping = "N";

                viewList.AddRange(SmartMES.Commons.CommonFunction.GetLabelData(x.boxno.ToString(), "Box", isShipping));
            }

            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup3 pop = new PrintPackingLabelPopup3(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
            */
        }
        #endregion

        #region - 포장 Spec Docking 보이기 / 감추기 |
        /// <summary>
        /// 포장 Spec Docking 보이기 / 감추기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDocking_Click(object sender, EventArgs e)
        {
            ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackingResult2));

            if (grpPackingSpec.Visible)
            {
                grpPackingSpec.Visible = false;
                btnDocking.Image = ((System.Drawing.Image)Properties.Resources.btnDocking_Right);
            }
            else
            {
                grpPackingSpec.Visible = true;
                btnDocking.Image = ((System.Drawing.Image)Properties.Resources.btnDocking_Left);
            }
        }
        #endregion

        #region - X-OUT 데이터 저장 |
        /// <summary>
        /// X-OUT 데이터 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveXOUT_Click(object sender, EventArgs e)
        {
            #region † 유효성 검사 |

            DataTable xoutList = grdXOUT.DataSource as DataTable;

            if (xoutList == null || xoutList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 포장 수량 체크
            DataRow dr = grdLabel2.View.GetFocusedDataRow();

            var chk = xoutList.AsEnumerable().Sum(r => r.Field<int>("GOODQTY"));

            if (Format.GetInteger(dr["PCSQTY"].ToString()) < Format.GetInteger(chk.ToString()))
            {
                // 포장수량을 초과할 수 없습니다.
                throw MessageException.Create("OverBoxPackingQty");
            }
            #endregion

            // XOUT 데이터 저장
            MessageWorker worker = new MessageWorker("SaveBoxPacking2");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SaveXOut" },
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "BoxNo", dr["BOXNO"].ToString() },
                { "XOutList", xoutList }
            });

            var result = worker.Execute<DataTable>();
            var resultData = result.GetResultSet();

            // Label Print
            if (resultData.Rows.Count < 1)
            {
                return;
            }

            grdXOUT.View.ClearDatas();
            resultData.Rows.Cast<DataRow>().ForEach(row =>
            {
                grdXOUT.View.AddNewRow();
                CommonFunction.SetGridColumnData(grdXOUT, row);
            });
        }
        #endregion

        #region - X-OUT 외부 라벨 Print |
        /// <summary>
        /// X-OUT 외부 라벨 Print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintLabelXOUTOuter_Click(object sender, EventArgs e)
        {
            DataRow dr = grdLabel2.View.GetFocusedDataRow();

            List<XtraReport> viewList = new List<XtraReport>();
            viewList.AddRange(SmartMES.Commons.CommonFunction.GetBoxLabelXOUTOuter(dr["BOXNO"].ToString()));

            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup pop = new PrintPackingLabelPopup(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
        }
        #endregion

        #region - X-OUT 내부 라벨 Print |
        /// <summary>
        /// X-OUT 내부 라벨 Print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintLabelXOUTInner_Click(object sender, EventArgs e)
        {
            DataRow dr = grdLabel2.View.GetFocusedDataRow();

            DataTable xoutDt = grdXOUT.View.GetCheckedRows();

            string strXoutSeq = string.Join(",", xoutDt.AsEnumerable().Select(c => Format.GetString(c["SERIALNO"])).Distinct());

            List<XtraReport> viewList = new List<XtraReport>();
            viewList.AddRange(SmartMES.Commons.CommonFunction.GetBoxLabelXOUTInner(dr["BOXNO"].ToString(), strXoutSeq));

            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup pop = new PrintPackingLabelPopup(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
        } 
        #endregion

        #region - Case 데이터 저장 |
        /// <summary>
        /// Case 데이터 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveCase_Click(object sender, EventArgs e)
        {
            #region † 유효성 검사 |

            DataTable caseList = grdCase.DataSource as DataTable;

            if (caseList == null || caseList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 포장 수량 체크
            DataRow dr = grdLabel3.View.GetFocusedDataRow();

            var chk = caseList.AsEnumerable().Sum(r => r.Field<int>("QTY"));

            if (Format.GetInteger(dr["PCSQTY"].ToString()) < Format.GetInteger(chk.ToString()))
            {
                // 포장수량을 초과할 수 없습니다.
                throw MessageException.Create("OverBoxPackingQty");
            }
            #endregion

            // Case 데이터 저장
            MessageWorker worker = new MessageWorker("SaveBoxPacking2");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SaveCase" },
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "BoxNo", dr["BOXNO"].ToString() },
                { "CaseList", caseList }
            });

            var result = worker.Execute<DataTable>();
            var resultData = result.GetResultSet();

            // Label Print
            if (resultData.Rows.Count < 1)
            {
                return;
            }

            grdCase.View.ClearDatas();
            resultData.Rows.Cast<DataRow>().ForEach(row =>
            {
                grdCase.View.AddNewRow();

                CommonFunction.SetGridColumnData(grdCase, row);
            });

        }
        #endregion

        #region - Case 라벨 Print |
        /// <summary>
        /// Case 라벨 Print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintLabelCase_Click(object sender, EventArgs e)
        {
            DataRow dr = grdLabel3.View.GetFocusedDataRow();

            DataTable caseDt = grdCase.View.GetCheckedRows();

            string strCaseNo = string.Join(",", caseDt.AsEnumerable().Select(c => Format.GetString(c["CASENO"])).Distinct());

            List<XtraReport> viewList = new List<XtraReport>();
            viewList.AddRange(SmartMES.Commons.CommonFunction.GetBoxLabelCase(dr["BOXNO"].ToString(), strCaseNo));

            if (viewList.Count > 0)
            {
                PrintPackingLabelPopup pop = new PrintPackingLabelPopup(viewList);
                pop.ShowDialog();
            }
            else
            {
                ShowMessage("NoLabelPrintInfo"); // 조회할 데이터가 없습니다.
            }
        } 
        #endregion

        #endregion

        #region ▶ ComboBox Event |

        #endregion

        #endregion

        #region ◆ 툴바 |

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            grdLabel2.View.ClearDatas();
            grdLabel3.View.ClearDatas();

            string currentAreaId = Format.GetString(popArea.GetValue(), "");
            string currentWorker = Format.GetString(cboWorker.GetDataValue(), "");

            DataTable packingLotList = grdLotList.DataSource as DataTable;
            DataTable boxList = grdBoxLot.DataSource as DataTable;
            DataTable defectList = grdDefect.DataSource as DataTable;

            // 포장 대상 LOT 설별 및 포장 수량과 잔량 집계 - 포장 수량과 잔량이 있을 경우 해당 LOT은 우선 SPLIT 후 포장진행 필요

            #region - 포장 LOT 취합 |
            var list = from dr in boxList.AsEnumerable()
                       group dr by dr["LOTID"] into dg
                       select new
                       {
                           lotid = dg.Key,
                           packingqty = dg.Sum(r => Format.GetInteger(r.Field<object>("QTY")))
                       };

            var cp = from lot in packingLotList.AsEnumerable()
                     join box in list.AsEnumerable() on lot.Field<string>("LOTID") equals box.lotid
                     select new
                     {
                         LOTID = box.lotid,
                         GOODQTY = Format.GetInteger(lot.Field<object>("GOODQTY")),
                         DEFECTQTY = Format.GetInteger(lot.Field<object>("DEFECTQTY")),
                         PACKINGQTY = box.packingqty,
                         LEFTQTY = Format.GetInteger(lot.Field<object>("GOODQTY")) - box.packingqty
                     }
                     ;

            // Lot별 Sum 정보
            DataTable lotList = new DataTable();
            lotList.Columns.Add("LOTID", typeof(String));
            lotList.Columns.Add("GOODQTY", typeof(Double));
            lotList.Columns.Add("DEFECTQTY", typeof(Double));
            lotList.Columns.Add("PACKINGQTY", typeof(Double));
            lotList.Columns.Add("LEFTQTY", typeof(Double));

            if (cp != null)
            {
                foreach (var x in cp)
                {
                    DataRow dr = lotList.NewRow();
                    dr["LOTID"] = x.LOTID;
                    dr["GOODQTY"] = x.GOODQTY;
                    dr["DEFECTQTY"] = x.DEFECTQTY;
                    dr["PACKINGQTY"] = x.PACKINGQTY;
                    dr["LEFTQTY"] = x.LEFTQTY;

                    lotList.Rows.Add(dr);
                }
            }
            #endregion

            #region - 불량 LOT 데이터 취합 및 SUM |

            DataTable dfdt = defectList.Clone();

            // 체크된 Lot 기준으로 Defect Code List를 Join하여 데이터를 한번 걸러냄
            var dfList = from lotRows in lotList.AsEnumerable()
                         join dfRows in defectList.AsEnumerable() on lotRows.Field<string>("LOTID") equals dfRows.Field<string>("LOTID")
                         select new
                         {
                             LOTID = lotRows.Field<string>("LOTID"),
                             DEFECTCODE = dfRows.Field<string>("DEFECTCODE"),
                             QCSEGMENTID = dfRows.Field<string>("QCSEGMENTID"),
                             PNLQTY = dfRows.Field<decimal>("PNLQTY"),
                             QTY = dfRows.Field<decimal>("QTY"),
                             REASONCONSUMABLEDEFID = dfRows.Field<string>("REASONCONSUMABLEDEFID"),
                             REASONCONSUMABLELOTID = dfRows.Field<string>("REASONCONSUMABLELOTID"),
                             REASONPROCESSSEGMENTID = dfRows.Field<string>("REASONPROCESSSEGMENTID"),
                             REASONAREAID = dfRows.Field<string>("REASONAREAID")
                         };

            foreach (var dfdr in dfList)
            {
                DataRow dr = dfdt.NewRow();
                dr["LOTID"] = dfdr.LOTID;
                dr["DEFECTCODE"] = dfdr.DEFECTCODE;
                dr["QCSEGMENTID"] = dfdr.QCSEGMENTID;
                dr["PNLQTY"] = dfdr.PNLQTY;
                dr["QTY"] = dfdr.QTY;
                dr["REASONCONSUMABLEDEFID"] = dfdr.REASONCONSUMABLEDEFID;
                dr["REASONCONSUMABLELOTID"] = dfdr.REASONCONSUMABLELOTID;
                dr["REASONPROCESSSEGMENTID"] = dfdr.REASONPROCESSSEGMENTID;
                dr["REASONAREAID"] = dfdr.REASONAREAID;
                dfdt.Rows.Add(dr);
            }

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
            var df = from lot in lotList.AsEnumerable()
                     join defect in defectLot.AsEnumerable() on lot.Field<string>("LOTID") equals defect.Field<string>("LOTID")
                     where Format.GetDecimal(lot.Field<object>("PACKINGQTY")) < Format.GetDecimal(defect.Field<object>("DEFECTQTY"))
                     select defect
                     ;

            if (df != null && df.Count() > 0)
            {
                // 불량 수량은 Lot 수량보다 많을 수 없습니다. {0}
                throw MessageException.Create("LotQtyLargerThanDefectQty");
            }

            DataTable equipList = grdEquipment.View.GetCheckedRows();

            string equipmentId = string.Join(",", equipList.Rows.Cast<DataRow>().Select(row => Format.GetString(row["EQUIPMENTID"])));

            if (string.IsNullOrWhiteSpace(equipmentId))
            {
                // 설비는 필수로 선택해야 합니다. {0}
                throw MessageException.Create("EquipmentIsRequired");
            }

            #endregion

            //포장 작업 시작
            MessageWorker worker = new MessageWorker("SaveBoxPacking2");
            worker.SetBody(new MessageBody()
            {
                { "TransactionType", "SaveBoxPacking" },
                { "EnterpriseId", UserInfo.Current.Enterprise },
                { "PlantId", UserInfo.Current.Plant },
                { "UserId", UserInfo.Current.Id },
                { "AreaID", currentAreaId },
                { "Worker", currentWorker },
                { "PackingLotList", lotList },
                { "BoxList", boxList },
                { "Equipmentlist", equipmentId },
                { "DefectLot", defectLot },
                { "DefectList", defectList }
            });

            var result = worker.Execute<DataTable>();
            var resultData = result.GetResultSet();

            // Label Print
            if (resultData.Rows.Count < 1)
            {
                return;
            }

            // 기존데이터 초기화
            // grdLabel.View.ClearDatas();

            resultData.Rows.Cast<DataRow>().ForEach(row =>
            {
                grdLabel.View.AddNewRow();

                CommonFunction.SetGridColumnData(grdLabel, row);
            });

            string lotId = string.Join(",", resultData.AsEnumerable().Select(c => Format.GetString(c["LOTID"])).Distinct());

            // XOUT / CASE별 데이터 저장 부분 주석처리
            //SelectBoxList(grdLabel2, lotId);
            //SelectBoxList(grdLabel3, lotId);

            this.tabMain.TabPages[3].Select();

            this.grdLotList.View.ClearDatas();
            this.grdDefect.View.ClearDatas();
            this.grdBoxLot.View.ClearDatas();
            this.grdBox.View.ClearDatas();

            this.tabMain.TabPages[1].PageEnabled = true;

            //packingType = string.Empty;
        }

        #endregion

        #region ◆ 검색 |

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
        }

        #endregion

        #region ◆ 유효성 검사 |

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            string currentAreaId = Format.GetString(popArea.GetValue(), "");
            if (string.IsNullOrEmpty(currentAreaId))
            {
                //작업장을 선택하세요.
                throw MessageException.Create("NoAreaSelected");
            }

            string currentUserId = Format.GetString(cboWorker.GetDataValue(), "");
            if (string.IsNullOrEmpty(currentUserId))
            {
                //작업자를 선택하세요.
                throw MessageException.Create("NoSelectWorker");
            }

            // Defect 정보
            grdDefect.CheckDefect();

            DataTable boxList = grdBoxLot.DataSource as DataTable;

            if(boxList == null || boxList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 포장 수량 체크

            DataTable lotList = grdLotList.DataSource as DataTable;

            var result = lotList.AsEnumerable().Where(r => r.Field<decimal?>("GOODQTY") < Format.GetInteger(r.Field<object>("PACKINGQTY"))).Count();

            if(Format.GetInteger(result.ToString()) > 1)
            {
                // 포장수량이 LOT수량을 초과할 수 없습니다.
                throw MessageException.Create("OverPackingQty"); 
            }
        }

        #endregion

        #region ◆ Private Function |

        // TODO : 화면에서 사용할 내부 함수 추가

        #region ▶ Control Data 초기화 |
        /// <summary>
        /// Control Data 초기화
        /// </summary>
        private void SetInitControl()
        {
            // Data 초기화
            this.grdLotList.View.ClearDatas();
            this.grdBoxLot.View.ClearDatas();
            this.grdEquipment.View.ClearDatas();
            this.grdDefect.View.ClearDatas();
            this.grdLabel2.View.ClearDatas();
            this.grdLabel3.View.ClearDatas();
            this.grdPrintLot2.View.ClearDatas();
            this.grdPrintLot3.View.ClearDatas();
            this.grdXOUT.View.ClearDatas();
            this.grdCase.View.ClearDatas();

            ClearPackingSpec();

            this.tabMain.TabPages[1].PageEnabled = true;
        }
        #endregion

        #region ▶ 데이터 조회 |
        /// <summary>
        /// 데이터 조회
        /// </summary>
        private void SearchLot()
        {
            DataTable lotdt = this.grdLotList.DataSource as DataTable;

            var lv = lotdt.AsEnumerable().Where(r => r.Field<string>("LOTID").Equals(this.txtLotID.EditValue)).Count();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LotId", CommonFunction.changeArgString(this.txtLotID.Editor.Text));
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("EnterpriseId", UserInfo.Current.Enterprise);
            param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("ProcessState", "Wait,Run");

            DataTable dtlotInfo = SqlExecuter.Query("SelectLotInfoByProcessForPacking", "10001", param);

            if (dtlotInfo == null || dtlotInfo.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            // Lot 정보 체크
            if (lotdt != null && lotdt.Rows.Count > 0 && !CheckLotInfo(dtlotInfo)) return;

            DataRow dr = dtlotInfo.Rows[0];

            this.grdLotList.View.AddNewRow();

            CommonFunction.SetGridColumnData(grdLotList, dr);

            // 포장유형 체크
            // 반제품 확인
            string pathType = dr["PATHTYPE"].ToString();

            if (pathType.Equals("End"))
                packingType = "Shipping"; // 제품포장
            else
                packingType = "Export"; // 수출포장

            if (packingType.Equals("Shipping") && !GetPackageSpec()) return;

            // 4Step 유형
            string stepType = dr["STEPTYPE"].ToString();

            #region - 인계작업장 |
            if (!stepType.Split(',').Contains("WaitForSend"))
            {
                cboTransitArea.Visible = true;

                Dictionary<string, object> transitAreaParam = new Dictionary<string, object>();
                transitAreaParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
                transitAreaParam.Add("PLANTID", UserInfo.Current.Plant);
                transitAreaParam.Add("LOTID", CommonFunction.changeArgString(this.txtLotID.Editor.Text));
                transitAreaParam.Add("PROCESSSEGMENTID", dr["NEXTPROCESSSEGMENTID"].ToString());
                transitAreaParam.Add("PROCESSSEGMENTVERSION", dr["NEXTPROCESSSEGMENTVERSION"].ToString());
                transitAreaParam.Add("RESOURCETYPE", "Area");
                transitAreaParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

                cboTransitArea.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
                cboTransitArea.Editor.ShowHeader = false;
                cboTransitArea.Editor.ValueMember = "AREAID";
                cboTransitArea.Editor.DisplayMember = "AREANAME";
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
            #endregion

            this.grdLotList.Enabled = true;
            this.tabMain.Enabled = true;

            #region - 설비 데이터 조회 |
            Dictionary<string, object> equipmentParam = new Dictionary<string, object>();
            equipmentParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            equipmentParam.Add("PLANTID", UserInfo.Current.Plant);
            equipmentParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            equipmentParam.Add("LOTID", CommonFunction.changeArgString(this.txtLotID.Editor.Text));
            equipmentParam.Add("EQUIPMENTTYPE", "Production");
            equipmentParam.Add("DETAILEQUIPMENTTYPE", "Main");

            DataTable equipmentList = SqlExecuter.Query("GetEquipmentByArea", "10031", equipmentParam);

            grdEquipment.DataSource = equipmentList;

            // 데이터가 하나면 자동 체크
            if (equipmentList != null && equipmentList.Rows.Count == 1)
            {
                grdEquipment.View.CheckedAll(true);
            } 
            #endregion

            this.txtLotID.Editor.Text = string.Empty;

            // 마지막 공정이 아니고 후공정이 있는 경우 수출포장공정으로 간주 (포장수량 수정불가, LOT혼입불가 처리)
            if(!pathType.Equals("End") && dr["NEXTPROCESSSEGMENTID"] != null)
            {
                chkMixingLot.Checked = false;
                chkMixingLot.Enabled = false;

                // 불량수량의 PCS입력 허용
                grdDefect.View.Columns["QTY"].OptionsColumn.ReadOnly = true;
            }
            else
            {
            

                // 불량수량의 PCS입력을 못하도록 설정
                grdDefect.View.Columns["QTY"].OptionsColumn.ReadOnly = false;
            }

            _panelPerQty = Format.GetDecimal(dtlotInfo.Rows[0]["PANELPERQTY"]);

            //2021.03.05 uom에따른 불량 수량 변경
            string ProcessUOM = Format.GetTrimString(dtlotInfo.Rows[0]["PROCESSUOM"]);
            decimal BlkQty = Format.GetDecimal(dtlotInfo.Rows[0]["PCSARY"]);
            decimal pcsPnl = Format.GetDecimal(dtlotInfo.Rows[0]["PCSPNL"].ToString());

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
        #region ▶ 포장사양 |
        /// <summary>
        /// 포장사양
        /// </summary>
        private bool GetPackageSpec()
        {
            // 포장사양정보
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", CommonFunction.changeArgString(CommonFunction.changeArgString(this.txtLotID.Editor.Text)));

            DataTable dt = SqlExecuter.Query("GetPackageSpec", "10001", param);

            if (dt.Rows.Count < 1)
            {
                ShowMessage("NoExistPackingSpec"); // 포장사양이 등록되지 않았습니다. 기준정보를 확인하여 주십시오.
                SetInitControl();

                this.grdLotList.Enabled = false;
                this.tabMain.Enabled = false;

                return false;
            }

            DataTable sdt = grdPackingSpec.DataSource as DataTable;

            if (sdt == null || sdt.Rows.Count == 0)
            {
                grdPackingSpec.DataSource = dt;

                DataRow dr = dt.Rows[0];

                string packqty = dr["PACKQTY"].ToString();
                string boxqty = dr["BOXQTY"].ToString();
                string caseqty = dr["CASEQTY"].ToString();
                string packageqty = dr["PACKAGEQTY"].ToString();

                if (!string.IsNullOrWhiteSpace(packageqty))
                {
                    txtPCSBox.EditValue = Format.GetInteger(packageqty);
                }
                else
                {
                    txtPCSBox.EditValue = Format.GetInteger(boxqty);
                }
            }

            return true;
        }
        #endregion

        #region ▶ Lot 정보 체크 |
        /// <summary>
        /// Lot 정보 체크
        /// </summary>
        /// <param name="tgdt"></param>
        /// <returns></returns>
        private bool CheckLotInfo(DataTable tgdt)
        {
            bool result = true;

            DataTable dt = grdLotList.DataSource as DataTable;

            if (dt == null || dt.Rows.Count == 0)
                dt = grdBoxLot.DataSource as DataTable;

            string productdefid = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();
            string tgproductdefid = tgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();

            if (!productdefid.Equals(tgproductdefid))
            {
                result = false;

                this.txtLotID.Editor.Text = string.Empty;

                // 다른 품목 ID는 선택할 수 없습니다.
                throw MessageException.Create("MixSelectProductDefID");
            }

            // 공정이 같은지 체크
            string processSegment = dt.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();
            string tgprocessSegment = tgdt.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();

            if (!processSegment.Equals(tgprocessSegment))
            {
                result = false;

                this.txtLotID.Editor.Text = string.Empty;

                // 동일 공정만 선택 할 수 있습니다,
                throw MessageException.Create("CheckDupliSegment");
            }

            int count = dt.AsEnumerable().Where(r => r.Field<string>("LOTID").Equals(this.txtLotID.Editor.EditValue.ToString().Trim())).Count();

            if(count > 0)
            {
                result = false;

                this.txtLotID.Editor.Text = string.Empty;

                // 이미 추가 된 LOT 입니다. {0}
                throw MessageException.Create("ExistLot");
            }

            return result;
        }
        #endregion

        #region ▶ Lot 수량과 Defect 수량 비교 |
        /// <summary>
        /// Lot 수량과 Defect 수량 비교
        /// </summary>
        private void CheckDefectQty(int rowhandle)
        {
            DataTable lotList = grdLotList.DataSource as DataTable;
            DataTable defectList = grdDefect.DataSource as DataTable;

            if (defectList == null || defectList.Rows.Count == 0) return;

            var lt = lotList.AsEnumerable().ToList();

            var defect = from dr in defectList.AsEnumerable()
                         group dr by dr["LOTID"] into dg
                         select new
                         {
                             LOTID = dg.Key,
                             DEFECTQTY = dg.Sum(r => r.Field<decimal?>("QTY")) ?? 0
                         };

            // Lot 수량과 Defect SUM Qty 비교
            var cp = from lot in lt.AsEnumerable()
                     join df in defect.AsEnumerable() on lot.Field<string>("LOTID") equals df.LOTID
                     where Format.GetDecimal(lot["QTY"]) < df.DEFECTQTY
                     select df
                     ;

            if (cp != null && cp.Count() > 0)
            {
                grdDefect.View.SetRowCellValue(rowhandle, "QTY", null);
                grdDefect.View.SetRowCellValue(rowhandle, "PNLQTY", null);
                grdDefect.View.SetRowCellValue(rowhandle, "DEFECTRATE", null);

                // 불량 수량은 Lot 수량보다 많을 수 없습니다. {0}
                ShowMessage("LotQtyLargerThanDefectQty");
            }

            var dl = defect.ToList();

            for (int i = 0; i < dl.Count(); i++)
            {
                var goodqty = grdLotList.View.GetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID), "QTY");
                var packingqty = grdLotList.View.GetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID), "PACKINGQTY");

                grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID), "DEFECTQTY", dl[i].DEFECTQTY.ToDecimal());
                grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID), "GOODQTY", Format.GetDecimal(goodqty.ToString()) - dl[i].DEFECTQTY.ToDecimal());
                grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i].LOTID), "LEFTQTY", Format.GetDecimal(goodqty.ToString()) - dl[i].DEFECTQTY.ToDecimal() - Format.GetDecimal(packingqty.ToString()));
            }
        }
        #endregion

        #region ▶ Lot Grid Footer 추가 Panel, PCS 합계 표시 |
        /// <summary>
        /// Lot Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitializationSummaryRow()
        {
            grdLotList.View.Columns["PRODUCTDEFID"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PRODUCTDEFID"].SummaryItem.DisplayFormat = string.Format("{0}", Language.Get("SUM"));
            grdLotList.View.Columns["PANELQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PANELQTY"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["GOODQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["GOODQTY"].SummaryItem.DisplayFormat = " ";
            grdLotList.View.Columns["PANELPERQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdLotList.View.Columns["PANELPERQTY"].SummaryItem.DisplayFormat = " ";

            grdLotList.View.OptionsView.ShowFooter = true;
            grdLotList.ShowStatusBar = false;
        }
        #endregion

        #region ▶ XOUT Grid Footer 추가 Panel, PCS 합계 표시 |
        /// <summary>
        /// Case Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitializationXOUTSummaryRow()
        {
            grdXOUT.View.Columns["XOUT"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["XOUT"].SummaryItem.DisplayFormat = " ";
            grdXOUT.View.Columns["CARD"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["CARD"].SummaryItem.DisplayFormat = " ";
            grdXOUT.View.Columns["GOODQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["GOODQTY"].SummaryItem.DisplayFormat = " ";
            grdXOUT.View.Columns["DEFECTQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdXOUT.View.Columns["DEFECTQTY"].SummaryItem.DisplayFormat = " ";

            grdXOUT.View.OptionsView.ShowFooter = true;
            grdXOUT.ShowStatusBar = false;
        }
        #endregion

        #region ▶ Case Grid Footer 추가 Panel, PCS 합계 표시 |
        /// <summary>
        /// Case Grid Footer 추가 Panel, PCS 합계 표시
        /// </summary>
        private void InitializationCaseSummaryRow()
        {
            grdCase.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            grdCase.View.Columns["QTY"].SummaryItem.DisplayFormat = " ";

            grdCase.View.OptionsView.ShowFooter = true;
            grdCase.ShowStatusBar = false;
        }
        #endregion

        #region ▶ Lot별 포장수량 집계 |
        /// <summary>
        /// Lot별 포장수량 집계
        /// </summary>
        private void GetSumPackingCount(string type)
        {
            DataTable lotData = grdLotList.DataSource as DataTable;

            DataTable dt = null;

            if (type.Equals("ALL"))
            {
                for(int k =0; k < lotData.Rows.Count; k++)
                {
                    int goodQty = Format.GetInteger(grdLotList.View.GetRowCellValue(k, "GOODQTY"));
                    grdLotList.View.SetRowCellValue(k, "PACKINGQTY", 0);
                    grdLotList.View.SetRowCellValue(k, "LEFTQTY", goodQty);
                }
                DataTable boxData = grdBox.DataSource as DataTable;
                for(int z =0; z < boxData.Rows.Count; z++)
                {
                    int boxQty = Format.GetInteger(grdBox.View.GetRowCellValue(z, "PCSPERBOX"));
                    grdBox.View.SetRowCellValue(z, "QTY", 0);
                    grdBox.View.SetRowCellValue(z, "LEFTQTY", boxQty);
                }
            }
            else
            {
                #region - LOT LIST 수량 업데이트 |
                if (type.Equals("DEL"))
                {
                    dt = grdBoxLot.View.GetCheckedRows();
                }
                else if (type.Equals("BOX")) // box정보 삭제시
                {
                    DataTable bxDt = grdBox.View.GetCheckedRows();
                    DataTable delData = (grdBoxLot.DataSource as DataTable).Clone();

                    foreach (DataRow dr in bxDt.Rows)
                    {
                        string boxNo = dr["BOXNO"].ToString();

                        for (int i = grdBoxLot.View.RowCount - 1; i >= 0; i--)
                        {
                            DataRow row = grdBoxLot.View.GetDataRow(i);
                            if (row == null || !row["BOXNO"].ToString().Equals(boxNo)) continue;

                            DataRow newRow = delData.NewRow();
                            newRow["BOXNO"] = row["BOXNO"];
                            newRow["PRODUCTDEFID"] = row["PRODUCTDEFID"];
                            newRow["PRODUCTDEFVERSION"] = row["PRODUCTDEFVERSION"];
                            newRow["PRODUCTDEFNAME"] = row["PRODUCTDEFNAME"];
                            newRow["LOTID"] = row["LOTID"];
                            newRow["WEEK"] = row["WEEK"]; 
                            newRow["LOTSPLIT"] = row["LOTSPLIT"];
                            newRow["QTY"] = row["QTY"];
                            delData.Rows.Add(newRow);

                            if (boxNo.Equals(row["BOXNO"].ToString()))
                                grdBoxLot.View.RemoveRow(i);
                        }
                        delData.AcceptChanges();
                    }

                    dt = delData;
                }
                else
                {
                    dt = grdBoxLot.DataSource as DataTable;
                }

                if (dt == null || dt.Rows.Count == 0) return;

                DataTable boxData = new DataTable();
                boxData.Columns.Add("LOTID", typeof(string));
                boxData.Columns.Add("PACKINGQTY", typeof(int));

                var result = dt.AsEnumerable()
                              .GroupBy(r => r.Field<string>("LOTID"))
                              .Select(g =>
                              {
                                  var row = boxData.NewRow();

                                  row["LOTID"] = g.Key;
                                  row["PACKINGQTY"] = g.Sum(r => Format.GetInteger(r.Field<object>("QTY")));

                                  return row;
                              });

                var dl = result.ToList();

                if (type.Equals("ADD"))
                {
                    for (int i = 0; i < dl.Count(); i++)
                    {
                        if (string.IsNullOrWhiteSpace(dl[i]["LOTID"].ToString())) continue;

                        int goodQty = Format.GetInteger(grdLotList.View.GetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "GOODQTY").ToString());
                        int packingQty = Format.GetInteger(grdLotList.View.GetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "PACKINGQTY").ToString());

                        grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "PACKINGQTY", Format.GetInteger(dl[i]["PACKINGQTY"].ToString()));
                        grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "LEFTQTY", goodQty - Format.GetInteger(dl[i]["PACKINGQTY"].ToString()));
                    }
                }
                else
                {
                    for (int i = 0; i < dl.Count(); i++)
                    {
                        int goodQty = Format.GetInteger(grdLotList.View.GetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "GOODQTY").ToString());
                        int packingQty = Format.GetInteger(grdLotList.View.GetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "PACKINGQTY").ToString());

                        grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "PACKINGQTY", packingQty - Format.GetInteger(dl[i]["PACKINGQTY"].ToString()));
                        grdLotList.View.SetRowCellValue(grdLotList.View.LocateByValue("LOTID", dl[i]["LOTID"].ToString()), "LEFTQTY", goodQty - (packingQty - Format.GetInteger(dl[i]["PACKINGQTY"].ToString())));
                    }
                }
                #endregion

                #region - BOX 수량 업데이트 |
                var result2 = from dr in dt.AsEnumerable()
                              group dr by dr["BOXNO"] into dg
                              select new
                              {
                                  BOXNO = dg.Key,
                                  PACKINGQTY = dg.Sum(r => Format.GetInteger(r.Field<object>("QTY")))
                              };

                var bl = result2.ToList();

                if (type.Equals("ADD"))
                {
                    for (int i = 0; i < bl.Count(); i++)
                    {
                        if (string.IsNullOrWhiteSpace(bl[i].BOXNO.ToString())) continue;

                        int boxQty = Format.GetInteger(grdBox.View.GetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "PCSPERBOX").ToString());
                        int packingQty = Format.GetInteger(grdBox.View.GetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "QTY").ToString());
                        int leftQty = Format.GetInteger(grdBox.View.GetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "LEFTQTY").ToString());

                        if (packingQty == 0 && leftQty > 0)
                        {
                            grdBox.View.SetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "QTY", Format.GetInteger(bl[i].PACKINGQTY.ToString()));
                            grdBox.View.SetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "LEFTQTY", leftQty - Format.GetInteger(bl[i].PACKINGQTY.ToString()));
                        }
                        else
                        {
                            grdBox.View.SetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "QTY", Format.GetInteger(bl[i].PACKINGQTY.ToString()));
                            grdBox.View.SetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "LEFTQTY", boxQty - Format.GetInteger(bl[i].PACKINGQTY.ToString()));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < bl.Count(); i++)
                    {
                        if (string.IsNullOrWhiteSpace(bl[i].BOXNO.ToString())) continue;

                        int packingQty = Format.GetInteger(grdBox.View.GetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "QTY").ToString());
                        int leftQty = Format.GetInteger(grdBox.View.GetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "LEFTQTY").ToString());

                        grdBox.View.SetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "QTY", packingQty - Format.GetInteger(bl[i].PACKINGQTY.ToString()));
                        grdBox.View.SetRowCellValue(grdBox.View.LocateByValue("BOXNO", Format.GetInteger(bl[i].BOXNO)), "LEFTQTY", leftQty + Format.GetInteger(bl[i].PACKINGQTY.ToString()));
                    }
                }
                #endregion
            }
        }
        #endregion

        #region ▶ 포장사양 초기화 |
        /// <summary>
        /// 포장사양 초기화
        /// </summary>
        private void ClearPackingSpec()
        {
            DataTable dataSource = grdPackingSpec.DataSource as DataTable;
            if (dataSource != null)
            {
                dataSource.Rows.Clear();
                dataSource.AcceptChanges();

                grdPackingSpec.DataSource = dataSource;
            }
        }
        #endregion

        #region ▶ 포장 작업 진행 |
        /// <summary>
        /// 포장 작업 진행
        /// </summary>
        /// <param name="bxRow"></param>
        /// <param name="lotDt"></param>
        private void SetBoxPacking(DataRow bxRow, DataTable lotDt)
        {
            int boxqty = Format.GetInteger(bxRow["PCSPERBOX"].ToString());
            int lotqty = 0;
            int lotleftqty = 0;
            int boxleftqty = Format.GetInteger(bxRow["LEFTQTY"].ToString());
            int packingqty = 0;
            int goodQty = 0;

            for (int i = 0; i < lotDt.Rows.Count; i++)
            {
                DataRow row = lotDt.Rows[i];

                bool chk = true;

                goodQty = Format.GetInteger(row["GOODQTY"].ToString());
                lotqty = Format.GetInteger(row["LEFTQTY"].ToString());

                if (lotqty == 0) continue;

                // LOT 혼입 여부에 따라 Box별 Lot분할 가능여부 체크
                if (!chkMixingLot.Checked)
                {
                    if (boxqty < goodQty)
                    {
                        //if(boxleftqty > lotqty)
                        //    continue;
                    }
                    else
                    {
                        if (boxleftqty < lotqty)
                            break;
                    }
                }
                else if (chkMixingLot.Checked)
                {
                    if (boxqty < goodQty)
                    {
                        if (lotqty == 0)
                            break;
                    }
                    else
                    {
                        if (boxleftqty <= 0)
                            break;
                    }
                }

                while (chk)
                {
                    DataTable dt = grdBoxLot.DataSource as DataTable;

                    int count = 0;
                    int boxCnt = 0;
                    int boxLotCnt = 0;

                    count = dt.AsEnumerable().Where(r => r.Field<string>("LOTID").Equals(row["LOTID"].ToString())).Count();

                    // Lot 혼입 여부에 따라 같은 Box에 Lot을 담을 수 있는 지 여부 체크
                    if (!chkMixingLot.Checked)
                    {
                        boxCnt = dt.AsEnumerable().Where(r => r.Field<string>("BOXNO").Equals(bxRow["BOXNO"].ToString())).Count();
                        boxLotCnt = dt.AsEnumerable().Where(r => r.Field<string>("BOXNO").Equals(bxRow["BOXNO"].ToString()) 
                                                            && r.Field<string>("LOTID").Equals(row["LOTID"].ToString())).Count();
                    }

                    if (boxCnt > 0 && boxLotCnt == 0) break;

                    if (boxqty <= lotqty)
                    {
                        if (boxleftqty == boxqty)
                        {
                            packingqty = boxleftqty;
                            boxleftqty = 0;
                            lotleftqty = lotqty - packingqty;
                        }
                        else if (boxleftqty > 0 && boxleftqty < lotqty)
                        {
                            packingqty = boxleftqty;
                            boxleftqty -= packingqty;
                            lotleftqty = lotqty - packingqty;
                        }
                        else if (boxleftqty > 0 && boxleftqty >= lotqty)
                        {
                            packingqty = lotqty;
                            boxleftqty -= packingqty;
                            lotleftqty = lotqty - packingqty;
                        }
                    }
                    else if (boxqty > lotqty)
                    {
                        if (boxleftqty == boxqty)
                        {
                            packingqty = lotqty;
                            boxleftqty -= packingqty;
                            lotleftqty = 0;
                        }
                        else if (boxleftqty > 0 && boxleftqty < lotqty)
                        {
                            packingqty = boxleftqty;
                            boxleftqty -= packingqty;
                            lotleftqty = lotqty - packingqty;
                        }
                        else if (boxleftqty > 0 && boxleftqty >= lotqty)
                        {
                            packingqty = lotqty;
                            boxleftqty -= packingqty;
                            lotleftqty = lotqty - packingqty;
                        }
                    }

                    grdBoxLot.View.AddNewRow();

                    grdBoxLot.View.SetFocusedRowCellValue("BOXNO", bxRow["BOXNO"].ToString());
                    grdBoxLot.View.SetFocusedRowCellValue("PRODUCTDEFID", row["PRODUCTDEFID"].ToString());
                    grdBoxLot.View.SetFocusedRowCellValue("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"].ToString());
                    grdBoxLot.View.SetFocusedRowCellValue("PRODUCTDEFNAME", row["PRODUCTDEFNAME"].ToString());
                    grdBoxLot.View.SetFocusedRowCellValue("LOTID", row["LOTID"].ToString());
                    grdBoxLot.View.SetFocusedRowCellValue("WEEK", row["WEEK"].ToString());
                    grdBoxLot.View.SetFocusedRowCellValue("PACKINGWEEK", string.IsNullOrWhiteSpace(this.txtWeek.Text) ? row["WEEK"].ToString() : this.txtWeek.Text);
                    grdBoxLot.View.SetFocusedRowCellValue("LOTSPLIT", count);
                    grdBoxLot.View.SetFocusedRowCellValue("QTY", packingqty);

                    if (UserInfo.Current.Enterprise.Equals(Constants.EnterPrise_YoungPoong))
                    {
                        grdBoxLot.View.SetFocusedRowCellValue("PCSPNL", Format.GetInteger(row["PCSPNL"].ToString()));
                        grdBoxLot.View.SetFocusedRowCellValue("PCSARY", Format.GetInteger(row["PCSARY"].ToString()));
                    }

                    if (boxleftqty == 0 || lotleftqty == 0)
                    {
                        chk = false;
                    }
                }

                if (boxleftqty == 0)
                {
                    chk = false;
                    break;
                }
            }

            GetSumPackingCount("ADD");
        }
        #endregion

        #region ▶ Box List 조회 |
        /// <summary>
        /// Box List 조회 
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="lotid"></param>
        private void SelectBoxList(SmartBandedGrid grd, string lotid)
        {
            DateTime date = DateTime.Now;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("P_PERIOD_PERIODFR", date.ToString("yyyy-MM-dd") + " 00:00");
            param.Add("P_PERIOD_PERIODTO", date.AddDays(1).ToString("yyyy-MM-dd") + " 23:59");
            param.Add("P_LOTID", lotid);

            DataTable dt = SqlExecuter.Query("SelectPackingAndPackageWipList", "10001", param);

            grd.DataSource = dt;
        } 
        #endregion

        #endregion
    }
}
