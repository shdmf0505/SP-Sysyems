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
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using Micube.Framework.SmartControls.Grid;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공통 > Defect Code List 
    /// 업  무  설  명  : Defect Code 공통 Grid
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-08-12
    /// 수  정  이  력  : 2019-12-30, 박정훈, 상단 Defect & 품질공정 수기 입력 추가
    /// 
    /// 
    /// </summary>
    public partial class usDefectGrid : UserControl
    {
        #region ◆ Variables
        private static decimal _panelPerQty = 0;
        private static decimal _qty = 0;

        private bool _visibleLotId = true;

        private bool _visibleFileUp = false;
        private bool _VisibleDefectDgree = false;
        private bool _visibleTopDefectCode = false;
        private bool _pcsset = false;
        private string _defectuom = "PNL";

        //private DataTable _fileData = new DataTable();
        //검사정의 ID
        private string _inspectionDefId = string.Empty;

        private bool isComboboxLoaded = false;
        private string lotId = null;
        #endregion

        #region ◆ Public Variables |
        /// <summary>
        /// Lot의 PCS / Panel
        /// </summary>
        public decimal LotPanelPerQty
        {
            set
            {
                _panelPerQty = value;
            }
        }
        public string DefectUOM
        {
            set
            {
                _defectuom = value;
            }
        }

        /// <summary>
        /// Lot 수량
        /// </summary>
        public decimal LotQty
        {
            set
            {
                _qty = value;
            }
        }

        /// <summary>
        /// Grid의 LotID 보여주기 설정
        /// </summary>
        public bool VisibleLotId
        {
            set { _visibleLotId = value; }
        }

        /// <summary>
        /// Grid의 파일업로드 관련 컬럼 설정
        /// </summary>
        public bool VisibleFileUpLoad
        {
            set { _visibleFileUp = value; }
        }

        public bool PcsSetting
        {
            set { _pcsset = value; }
        }
        /// <summary>
        /// 불량 등급 표시 여부
        /// </summary>
        public bool VisibleDefectDgree
        {
            set { _VisibleDefectDgree = value; }
        }
        /// <summary>
        /// Grid의 DataSource 설정
        /// </summary>
        public DataTable DataSource
        {
            get
            {
                return defectList.DataSource as DataTable;
            }
            set
            {
                defectList.DataSource = value;
            }
        }

        public SmartBandedGrid Grid
        {
            get
            {
                return defectList;
            }
        }

        public SmartGridControl GridControl
        {
            get
            {
                return defectList.GridControl;
            }
        }

        public SmartBandedGridView View
        {
            get
            {
                return defectList.View;
            }
        }

        public bool VisibleTopDefectCode
        {
            get
            {
                return _visibleTopDefectCode;
            }
            set
            {
                _visibleTopDefectCode = value;
            }
        }

        public string LotID
        {
            get; set;
        }
        public string MenuId { get; private set; }
        public bool IsImageYNChecked
        {
            get
            {
                return chkImageYN.Checked;
            }
        }
        public bool SetImageYNChecked
        {
            set
            {
                chkImageYN.Checked = value;
            }
        }

        #endregion

        #region ◆ Events |
        public event EventHandler OnStartLoadingComboboxData = delegate { };
        public event EventHandler OnEndLoadingComboboxData = delegate { };
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public usDefectGrid()
        {
            InitializeComponent();

			if (!this.IsDesignMode())
				this.Load += UsDefectGrid_Load;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsDefectGrid_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        public void InitializeControls()
        {
			if (!this.IsDesignMode())
			{
				InitializeGrid();

				//InitializeEvent();

				// 상단 Defect & 품질공정 수기 입력 보이기 설정
				if (_visibleTopDefectCode)
				{
					pnlDefect.Visible = true;
				}
				else
				{
					pnlDefect.Visible = false;
				}
            }

            this.lblImageYN.ToolTip = Language.Get("UNABLETOREGISTERPHOTOSUNCHECKLOT");
            this.chkImageYN.ToolTip = Language.Get("UNABLETOREGISTERPHOTOSUNCHECKLOT");
        }

        /// <summary>
        /// Control Event 설정
        /// </summary>
        public void InitializeEvent()
        {
			if (!this.IsDesignMode())
			{
				defectList.View.CellValueChanged += DefectView_CellValueChanged;
				defectList.ToolbarDeletingRow += DefectList_ToolbarDeletingRow;
				defectList.View.GridCellButtonClickEvent += View_GridCellButtonClickEvent;

				txtDefectCode.Editor.KeyDown += txtDefectCodeEditor_KeyDown;
				txtQCSegment.Editor.KeyDown += txtQCSegmentEditor_KeyDown;
				txtPNLQty.KeyDown += TxtPNLQty_KeyDown;
				txtPCSQty.KeyDown += TxtPCSQty_KeyDown;

				btnDefectSearch.Click += BtnDefectSearch_Click;
                defectList.View.ShowingEditor += View_ShowingEditor;
            }
        }

        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            string lotid = Format.GetString(defectList.View.GetFocusedRowCellValue("LOTID"));

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lotid);
            DataTable dt = SqlExecuter.Query("GetLotProcesssegment", "10001", param);

            if (dt.Rows[0]["PROCESSSEGMENTID"].Equals("75"))
            {
                e.Cancel = false;
            }

            else if (dt.Rows[0]["PROCESSSEGMENTID2"].Equals("7038"))
            {
                e.Cancel = false;
            }
            else if (defectList.View.FocusedColumn.FieldName == "PNLQTY")
            {
                if(_defectuom.Equals("PCS"))
                {
                    e.Cancel = true;
                }
                
            }
            else if (defectList.View.FocusedColumn.FieldName == "QTY")
            {
                if (!_defectuom.Equals("PCS") && !_defectuom.Equals("ALL"))
                //if (!_defectuom.Equals("PCS") )
                {
                    e.Cancel = true;
                }

            }


        }


        #region ▶ Grid Control 초기화 |
        /// <summary>
        /// Grid Control 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region - Defect Grid |
            defectList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            defectList.ShowStatusBar = false;

            defectList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            // Lot No.
            if (_visibleLotId)
                defectList.View.AddTextBoxColumn("LOTID", 160).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
            else
                defectList.View.AddTextBoxColumn("LOTID", 160).SetTextAlignment(TextAlignment.Center).SetIsHidden();

            // 불량코드
            InitializeDefectCodePopup();
            // 불량명
            defectList.View.AddTextBoxColumn("DEFECTCODENAME", 120).SetIsReadOnly();

            //if (_VisibleDefectDgree)
            //    defectList.View.AddTextBoxColumn("DECISIONDEGREENAME", 60).SetIsReadOnly().SetLabel("DECISIONDEGREE");

            defectList.View.AddTextBoxColumn("QCSEGMENTID", 120).SetIsReadOnly().SetIsHidden();
            // 중공정명
            defectList.View.AddTextBoxColumn("QCSEGMENTNAME", 120).SetIsReadOnly().SetLabel("QCSEGMENT");
            //판정등급 2021-02-08 오근영 불량판정등급명 추가
            defectList.View.AddTextBoxColumn("DECISIONDEGREENAME", 100).SetIsReadOnly().SetLabel("DECISIONDEGREE");
            // PNL
            defectList.View.AddSpinEditColumn("PNLQTY", 80).SetDisplayFormat("#,##0.00").SetValidationIsRequired();
            // 수량
            defectList.View.AddSpinEditColumn("QTY", 80);
            
             
            // 불량율
            defectList.View.AddSpinEditColumn("DEFECTRATE", 80).SetDisplayFormat("#,##0.00").SetIsReadOnly();
            
            //이미지
            if (_visibleFileUp)
            {
                defectList.View.AddButtonColumn("IMAGE", 80).SetLabel("ADDIMAGE");
                defectList.View.AddTextBoxColumn("FILENAME", 200).SetIsHidden();
                defectList.View.AddTextBoxColumn("FILESIZE", 200).SetIsHidden();
                defectList.View.AddTextBoxColumn("FILEEXT", 200).SetIsHidden();
                defectList.View.AddTextBoxColumn("FILEPATH", 150).SetIsHidden();
                defectList.View.AddTextBoxColumn("IMAGERESOURCEID", 100).SetIsHidden();
                defectList.View.AddTextBoxColumn("ISUPLOAD").SetIsHidden().SetDefault("N");
            }

            // 원인품목
            defectList.View.AddComboBoxColumn("SPLITCONSUMABLEDEFIDVERSION", 200, new SqlQuery("GetReasonConsumableList", "10002"), "CONSUMABLEDEFNAME", "SPLITCONSUMABLEDEFIDVERSION")
                            .SetLabel("REASONCONSUMABLEDEFID")
                            .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                            .SetRelationIds("LOTID")
                            .SetPopupWidth(600)
                            .SetVisibleColumns("CONSUMABLEDEFID", "CONSUMABLEDEFVERSION", "CONSUMABLEDEFNAME", "MATERIALTYPE")
                            .SetVisibleColumnsWidth(90, 70, 200, 80);
            defectList.View.AddTextBoxColumn("REASONCONSUMABLEDEFID", 100).SetIsReadOnly().SetIsHidden();
            defectList.View.AddTextBoxColumn("REASONCONSUMABLEDEFVERSION", 100).SetIsReadOnly().SetIsHidden();
            // 원인자재LOT
            defectList.View.AddComboBoxColumn("REASONCONSUMABLELOTID", 180, new SqlQuery("GetDefectReasonConsumableLot", "10002"), "CONSUMABLELOTID", "CONSUMABLELOTID")
                            .SetRelationIds("LOTID", "SPLITCONSUMABLEDEFIDVERSION");
            //                            .SetRelationIds("LOTID", "REASONCONSUMABLEDEFIDVERSION");

            // 원인공정
            defectList.View.AddComboBoxColumn("REASONPROCESSSEGMENTID", 200, new SqlQuery("GetDefectReasonProcesssegment", "10002"), "PROCESSSEGMENTNAME", "PROCESSSEGMENTID")
                            .SetMultiColumns(ComboBoxColumnShowType.Custom, true)
                            .SetRelationIds("SPLITCONSUMABLEDEFIDVERSION", "REASONCONSUMABLELOTID")
                            .SetPopupWidth(600)
                            .SetVisibleColumns("PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "USERSEQUENCE", "AREANAME")
                            .SetVisibleColumnsWidth(90, 150, 70, 100);

            // 원인작업장
            defectList.View.AddTextBoxColumn("REASONAREAID", 100).SetIsReadOnly().SetIsHidden();
            defectList.View.AddTextBoxColumn("REASONAREANAME", 150).SetIsReadOnly().SetLabel("REASONAREAID");

            //2019-12-09 불량등급 추가
            defectList.View.AddTextBoxColumn("DECISIONDEGREE", 100).SetIsReadOnly().SetIsHidden();


            defectList.View.PopulateColumns();

            defectList.View.OptionsView.ShowFooter = true;
            defectList.View.OptionsView.ShowIndicator = false;

            RepositoryItemSpinEdit qtyEdit = defectList.View.Columns["QTY"].ColumnEdit as RepositoryItemSpinEdit;
            qtyEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            qtyEdit.Mask.EditMask = "n0";

            #endregion

            /// <summary>
            /// 합계 Row 초기화
            /// </summary>
            defectList.View.Columns["QTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            defectList.View.Columns["QTY"].SummaryItem.DisplayFormat = "{0}";
            defectList.View.Columns["PNLQTY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            defectList.View.Columns["PNLQTY"].SummaryItem.DisplayFormat = "{0:f2}";
            defectList.View.Columns["DEFECTRATE"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            defectList.View.Columns["DEFECTRATE"].SummaryItem.DisplayFormat = "{0:f2} %";
        }
        #endregion

        #region ▶ TextBox Event |
        /// <summary>
        /// 불량코드 Key 입력 Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDefectCodeEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(CommonFunction.changeArgString(txtDefectCode.Editor.Text)))
            {
                txtQCSegment.Editor.Focus();
            }
        }

        /// <summary>
        /// 품질공정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQCSegmentEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(CommonFunction.changeArgString(txtDefectCode.Editor.Text)))
                    return;

                if (string.IsNullOrWhiteSpace(CommonFunction.changeArgString(txtQCSegment.Editor.Text)))
                    return;

                BtnDefectSearch_Click(null, null);
            }
        }

        /// <summary>
        /// PCS 수량
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPCSQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            if (string.IsNullOrWhiteSpace(txtPCSQty.Text))
                return;

            if (_panelPerQty == 0 || string.IsNullOrWhiteSpace(_panelPerQty.ToString()))
                return;

            decimal qty = Format.GetDecimal(txtPCSQty.Text);

            decimal pnlQty = Math.Ceiling(qty / _panelPerQty);

            txtPNLQty.Text = pnlQty.ToString();

            BtnDefectSearch_Click(null, null);
        }

        /// <summary>
        /// PANEL 수량
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPNLQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            if (string.IsNullOrWhiteSpace(txtPNLQty.Text))
                return;

            if (_panelPerQty == 0 || string.IsNullOrWhiteSpace(_panelPerQty.ToString()))
                return;

            decimal pnlQty = Format.GetDecimal(txtPNLQty.Text);

            decimal qty = _panelPerQty * pnlQty;

            txtPCSQty.Text = qty.ToString();
        }
        #endregion

        #region ▶ Button Event |
        /// <summary>
        /// 불량코드 및 품질공정 코드로 데이터 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDefectSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CommonFunction.changeArgString(txtDefectCode.Editor.Text)))
            {
                //불량코드를 먼저 입력하세요.
                throw MessageException.Create("DefectCodeIsRequired");
            }

            if (string.IsNullOrWhiteSpace(CommonFunction.changeArgString(txtQCSegment.Editor.Text)))
            {
                return;
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LanguageType", UserInfo.Current.LanguageType);
            param.Add("DEFECTCODEID", CommonFunction.changeArgString(txtDefectCode.Editor.Text));
            param.Add("QCSEGMENTID", CommonFunction.changeArgString(txtQCSegment.Editor.Text));

            DataTable dt = SqlExecuter.Query("GetDefectCodeByProcess4", "10004", param);

            if (dt == null || dt.Rows.Count <= 0)
                return;

            defectList.View.AddNewRow();

            DataRow dr = dt.Rows[0];

            defectList.View.SetFocusedRowCellValue("LOTID", LotID);
            defectList.View.SetFocusedRowCellValue("DEFECTCODE", dr["DEFECTCODE"].ToString());
            defectList.View.SetFocusedRowCellValue("DEFECTCODENAME", dr["DEFECTCODENAME"].ToString());
            defectList.View.SetFocusedRowCellValue("QCSEGMENTID", dr["QCSEGMENTID"].ToString());
            defectList.View.SetFocusedRowCellValue("QCSEGMENTNAME", dr["QCSEGMENTNAME"].ToString());
            defectList.View.SetFocusedRowCellValue("DECISIONDEGREE", dr["DECISIONDEGREE"].ToString());
            defectList.View.SetFocusedRowCellValue("DECISIONDEGREENAME", dr["DECISIONDEGREENAME"].ToString());  // 2021-02-08 오근영 등급 추가
            if (!txtPCSQty.Text.Equals("0") && !txtPNLQty.Text.Equals("0") && !string.IsNullOrWhiteSpace(txtPCSQty.Text) && !string.IsNullOrWhiteSpace(txtPNLQty.Text))
            {
                defectList.View.SetFocusedRowCellValue("PNLQTY", Format.GetDecimal(txtPNLQty.Text));
                defectList.View.SetFocusedRowCellValue("QTY", Format.GetDecimal(txtPCSQty.Text));
                defectList.View.SetFocusedRowCellValue("DEFECTRATE", (Format.GetDecimal(txtPCSQty.Text) / _qty) * 100);
            }
            

            txtDefectCode.Editor.Text = string.Empty;
            txtQCSegment.Editor.Text = string.Empty;
            txtPNLQty.Text = string.Empty;
            txtPCSQty.Text = string.Empty;

            txtDefectCode.Editor.Focus();
        }
        #endregion
        #endregion

        #region ◆ Event |

        #region - Cell Value Changed |
        /// <summary>
        /// Grid Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefectView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            #region - 수량관련 |
            if (_panelPerQty <= 0 && (e.Column.FieldName.Equals("PNLQTY") || e.Column.FieldName.Equals("QTY") || e.Column.FieldName.Equals("QCSEGMENTNAME")))
            {
                return;
            }
            else
            {
                if (e.Column.FieldName.Equals("PNLQTY"))
                {
                    defectList.View.CellValueChanged -= DefectView_CellValueChanged;

                    decimal pnlQty = Format.GetDecimal(Format.GetInteger(e.Value));
                    decimal qty = 0;
                    if (_panelPerQty * pnlQty > _qty)
                    {
                        qty = _qty;
                    }
                    else
                    {
                        qty = _panelPerQty * pnlQty;
                    }

                    defectList.View.SetRowCellValue(e.RowHandle, "QTY", qty);
                    defectList.View.SetRowCellValue(e.RowHandle, "DEFECTRATE", (qty / _qty) * 100);

                    DefectQtyChanged?.Invoke(sender, e);

                    defectList.View.CellValueChanged += DefectView_CellValueChanged;
                }

                if (e.Column.FieldName.Equals("QTY"))
                {
                    defectList.View.CellValueChanged -= DefectView_CellValueChanged;

                    decimal qty = Format.GetDecimal(Format.GetInteger(e.Value));

                    decimal pnlQty = 0;
                    if (_panelPerQty > 0)
                        pnlQty = Math.Ceiling(qty / _panelPerQty);

                    defectList.View.SetRowCellValue(e.RowHandle, "PNLQTY", pnlQty);
                    defectList.View.SetRowCellValue(e.RowHandle, "DEFECTRATE", (qty / _qty) * 100);

                    DefectQtyChanged?.Invoke(sender, e);

                    defectList.View.CellValueChanged += DefectView_CellValueChanged;
                }

                if (e.Column.FieldName.Equals("QCSEGMENTNAME"))
                {
                    CheckDefectCodeCount();
                }
            }
            #endregion

            #region - 원인 품목 / 공정 / 자재등
            if (/*e.Column.FieldName == "REASONCONSUMABLEDEFIDVERSION" || */e.Column.FieldName == "SPLITCONSUMABLEDEFIDVERSION")
            {
                defectList.View.CellValueChanged -= DefectView_CellValueChanged;

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string consumableDefId = Format.GetString(edit.GetDataSourceValue("CONSUMABLEDEFID", edit.GetDataSourceRowIndex("SPLITCONSUMABLEDEFIDVERSION", e.Value)));
                string consumableDefVersion = Format.GetString(edit.GetDataSourceValue("CONSUMABLEDEFVERSION", edit.GetDataSourceRowIndex("SPLITCONSUMABLEDEFIDVERSION", e.Value)));

                defectList.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFID", consumableDefId);
                defectList.View.SetFocusedRowCellValue("REASONCONSUMABLEDEFVERSION", consumableDefVersion);

                defectList.View.CellValueChanged += DefectView_CellValueChanged;
            }

            if (e.Column.FieldName == "REASONPROCESSSEGMENTID")
            {
                defectList.View.CellValueChanged -= DefectView_CellValueChanged;

                RepositoryItemLookUpEdit edit = e.Column.ColumnEdit as RepositoryItemLookUpEdit;

                string areaId = Format.GetString(edit.GetDataSourceValue("AREAID", edit.GetDataSourceRowIndex("PROCESSSEGMENTID", e.Value)));
                string areaName = Format.GetString(edit.GetDataSourceValue("AREANAME", edit.GetDataSourceRowIndex("PROCESSSEGMENTID", e.Value)));

                defectList.View.SetFocusedRowCellValue("REASONAREAID", areaId);
                defectList.View.SetFocusedRowCellValue("REASONAREANAME", areaName);

                defectList.View.CellValueChanged += DefectView_CellValueChanged;
            }
            #endregion
        }
        #endregion

        #region - Defect Row Deleting |
        /// <summary>
        /// Defect Row Deleting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefectList_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            ToolbarDeletingRow?.Invoke(sender, e);
        }

        /// <summary>
        /// 사진 등록 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_GridCellButtonClickEvent(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.GridCellButtonClickEventArgs args)
        {
            if (_visibleFileUp && args.FieldName == "IMAGE")
            {
                DataRow row = defectList.View.GetFocusedDataRow();

                int iQty = Format.GetInteger(row["QTY"]);

                if (iQty == 0)
                {
                    throw MessageException.Create("CheckRegDefectQty");
                }

                if (row["ISUPLOAD"].ToString().Equals("N"))
                {
                    AddImagePopup imagePopup = new AddImagePopup(_inspectionDefId);
                    imagePopup.FileInfo += (dtFileInfo) =>
                    {
                        string imageName = string.Empty;
                        string imageExt = string.Empty;
                        string imageSize = string.Empty;

                        foreach (DataRow fileRow in dtFileInfo.Rows)
                        {
                            row["IMAGERESOURCEID"] = fileRow["RESOURCEID"].ToString();
                            row["FILEPATH"] = fileRow["FILEPATH"].ToString();
                            imageName += "," + fileRow["FILENAME"].ToString();
                            imageExt += "," + fileRow["FILEEXT"].ToString();
                            imageSize += "," + fileRow["FILESIZE"].ToString();

                            //파일 뷰
                            //DataRow addRow = _fileData.NewRow();
                            //addRow["FILENAME"] = fileRow["FILENAME"];
                            //addRow["FILEEXT"] = fileRow["FILEEXT"];
                            //addRow["FILESIZE"] = fileRow["FILESIZE"];
                            //addRow["FILEPATH"] = fileRow["FILEPATH"];

                            //addRow["URL"] = AppConfiguration.GetString("Application.SmartDeploy.Url") + fileRow["FILEPATH"].ToString() + "/" + fileRow["FILENAME"].ToString() + "." + fileRow["FILEEXT"].ToString();
                            //_fileData.Rows.Add(addRow);

                            //grdFileList.DataSource = _fileData;
                        }

                        row["FILENAME"] = imageName;
                        row["FILEEXT"] = imageExt;
                        row["FILESIZE"] = imageSize;
                        row["ISUPLOAD"] = "Y";

                    };
                    imagePopup.ShowDialog();
                }
                else
                {
                    //업로드된 사진 view
                    //추가 수정 삭제 가능
                    DataRow selectRow = defectList.View.GetFocusedDataRow();

                    ModifiyImagePopup imagePopup = new ModifiyImagePopup(_inspectionDefId, selectRow);
                    imagePopup.FileInfo += (dtFileInfo, isAdd) =>
                    {
                        string fileName = string.Empty;
                        string fileExt = string.Empty;
                        string fileSize = string.Empty;
                        DataRow select = defectList.View.GetFocusedDataRow();
                        foreach (DataRow r in dtFileInfo.Rows)
                        {
                            if (isAdd)
                            {
                                select["FILENAME"] += "," + Format.GetString(r["FILENAME"]);
                                select["FILEEXT"] += "," + Format.GetString(r["FILEEXT"]);
                                select["FILESIZE"] += "," + Format.GetString(r["FILESIZE"]);
                            }
                            else
                            {
                                fileName += "," + Format.GetString(r["FILENAME"]);
                                fileExt += "," + Format.GetString(r["FILEEXT"]);
                                fileSize += "," + Format.GetString(r["FILESIZE"]);
                            }
                        }

                        if (dtFileInfo.Rows.Count < 1)
                        {
                            select["ISUPLOAD"] = "N";
                            select["FILENAME"] = "";
                            select["FILEEXT"] = "";
                            select["FILESIZE"] = "";
                            select["FILEPATH"] = "";
                            select["IMAGERESOURCEID"] = "";
                        }
                        else if (!isAdd && dtFileInfo.Rows.Count >= 1)
                        {
                            select["FILENAME"] = fileName;
                            select["FILEEXT"] = fileExt;
                            select["FILESIZE"] = fileSize;
                        }
                    };
                    imagePopup.ShowDialog();
                }

                DefectFileInfoChanged?.Invoke(sender, args);
            }
        }
        #endregion
        #endregion

        #region ◆ Event Handler |
        /// <summary>
        /// 수량변경 이벤트
        /// </summary>
        public event CellValueChangedEventHandler DefectQtyChanged;

        /// <summary>
        /// Row 삭제시 발생 이벤트
        /// </summary>
        public event EventHandler ToolbarDeletingRow;

        public event EventHandler DefectFileInfoChanged;

        #endregion

        #region ◆ Public Function |

        #region - SetInfo :: 기본정보 설정 |
        /// <summary>
        /// 기본정보 설정
        /// </summary>
        /// <param name="panelPerQty">Panel별 수량</param>
        /// <param name="panelQty">Panel 수</param>
        /// <param name="qty">PCS 수량</param>
        public void SetInfo(decimal panelPerQty, decimal qty, string lotId = "")
        {
            _panelPerQty = panelPerQty;
            _qty = qty;

            if (!string.IsNullOrEmpty(lotId) && string.IsNullOrEmpty(LotID))
                LotID = lotId;
        }
        #endregion

        #region - SetConsumableDefComboBox :: 외부에서 LOT ID 세팅 시 원인품목 세팅 |
        /// <summary>
        /// 외부에서 LOT ID 세팅 시 원인품목 세팅
        /// </summary>
        public void SetConsumableDefComboBox()
        {
            string lotid = Format.GetString(defectList.View.GetFocusedRowCellValue("LOTID"));

            string lotIdList = string.Join(",", (defectList.DataSource as DataTable).AsEnumerable().Select(c => Format.GetString(c["LOTID"])).Distinct());

            if (!string.IsNullOrEmpty(lotid))
                defectList.View.RefreshComboBoxDataSource("REASONCONSUMABLEDEFID", new SqlQuery("GetReasonConsumableList", "10001", $"LOTID={lotid}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
        }

        public void setDefectTextBox(string uom)
        {
            if(uom.Equals("PCS"))
            {
                txtPNLQty.ReadOnly = true;
                txtPCSQty.ReadOnly = false;
                lblPNLQty.Text = "PNL";
            }
            else if (uom.Equals("BLK"))
            {
                txtPNLQty.ReadOnly = false;
                lblPNLQty.Text = "BLK";
                txtPCSQty.ReadOnly = true;
            }
            else
            {
                txtPNLQty.ReadOnly = false;
                txtPCSQty.ReadOnly = true;
                lblPNLQty.Text = "PNL";
            }

        }

        /// <summary>
        /// 외부에서 LOT ID 세팅 시 원인품목 세팅
        /// </summary>
        /// <param name="lotId">Lot Id (Multi)</param>
        public void SetConsumableDefComboBox(string lotId)
        {
            if (!string.IsNullOrEmpty(lotId))
            {
                this.lotId = lotId;
                if(Visible)
                {
                    doSetConsumableDefComboBox(this.lotId);
                }
                else
                {
                    this.isComboboxLoaded = false;
                }
            }
        }

        private void doSetConsumableDefComboBox(string lotId)
        {
            OnStartLoadingComboboxData(this, EventArgs.Empty);
            this.isComboboxLoaded = true;
            try
            {
                defectList.View.RefreshComboBoxDataSource("SPLITCONSUMABLEDEFIDVERSION", new SqlQuery("GetReasonConsumableList", "10002", $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
                defectList.View.RefreshComboBoxDataSource("REASONCONSUMABLELOTID", new SqlQuery("GetDefectReasonConsumableLot", "10002", $"LOTID={lotId}"));
                defectList.View.RefreshComboBoxDataSource("REASONPROCESSSEGMENTID", new SqlQuery("GetDefectReasonProcesssegment", "10002", $"LOTID={lotId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            }
            finally
            {
                OnEndLoadingComboboxData(this, EventArgs.Empty);
            }
        }

        public void SetInspectionDefinitionId(string inspectionDefId)
        {
            _inspectionDefId = inspectionDefId;
        }

        //public DataTable GetFileInfo()
        //{
        //    return _fileData;
        //}
        #endregion
        #endregion

        #region ◆ Private Function |

        #region ▶ Grid 불량코드 조회 팝업창 |
        /// <summary>
        /// Grid 불량코드 조회 팝업창
        /// </summary>
        private void InitializeDefectCodePopup()
        {
            var defectCodePopupColumn = defectList.View.AddSelectPopupColumn("DEFECTCODE", 80, new SqlQuery("GetDefectCodeByProcess5", "10005", "RESOURCETYPE=QCSegmentID", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODE")
                .SetValidationIsRequired()
                .SetPopupLayout("SELECTDEFECTCODE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(900, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("QCSEGMENTNAME")
                .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                {
                    if (selectedRow.Count() > 0)
                    {
                        defectList.View.SetFocusedRowCellValue("DEFECTCODENAME", selectedRow.FirstOrDefault()["DEFECTCODENAME"]);
                        defectList.View.SetFocusedRowCellValue("QCSEGMENTID", selectedRow.FirstOrDefault()["QCSEGMENTID"]);
                        defectList.View.SetFocusedRowCellValue("QCSEGMENTNAME", selectedRow.FirstOrDefault()["QCSEGMENTNAME"]);
                        defectList.View.SetFocusedRowCellValue("DECISIONDEGREE", selectedRow.FirstOrDefault()["DECISIONDEGREE"]);
                        defectList.View.SetFocusedRowCellValue("DECISIONDEGREENAME", selectedRow.FirstOrDefault()["DECISIONDEGREENAME"]);   // 2021-02-09 오근영 불량판정등급명 추가
                    }
                    else
                    {
                        defectList.View.SetFocusedRowCellValue("DEFECTCODENAME", "");
                        defectList.View.SetFocusedRowCellValue("QCSEGMENTID", "");
                        defectList.View.SetFocusedRowCellValue("QCSEGMENTNAME", "");
                        defectList.View.SetFocusedRowCellValue("DECISIONDEGREE", "");
                        defectList.View.SetFocusedRowCellValue("DECISIONDEGREENAME", "");   // 2021-02-09 오근영 불량판정등급명 추가
                    }
                });

            // 검색조건 - 불량코드
            //defectCodePopupColumn.Conditions.AddTextBox("DEFECTCODEID");
            //defectCodePopupColumn.Conditions.AddTextBox("QCSEGMENTID");
            // 팝업의 검색조건 항목 추가 (불량코드/명)
            defectCodePopupColumn.Conditions.AddTextBox("DEFECTCODENAME");
            defectCodePopupColumn.Conditions.AddTextBox("QCSEGMENTNAME");
            defectCodePopupColumn.Conditions.AddTextBox("LOTID").SetPopupDefaultByGridColumnId("LOTID").SetIsHidden();

            // 팝업의 그리드에 컬럼 추가
            // 불량코드
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODE", 150);
            // 불량코드명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200);
            // 중공정ID
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTID", 150);
            // 중공정명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200);
            // 판정등급
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DECISIONDEGREENAME", 80).SetLabel("DECISIONDEGREE");   // 2021-02-09 오근영 불량판정등급명 추가
        }
        #endregion

        #region ▶ Defect Code 중복 체크 |
        /// <summary>
        /// Defect Code 중복 체크
        /// </summary>
        private void CheckDefectCodeCount()
        {
            DataTable dfList = defectList.DataSource as DataTable;

            var defect = from dr in dfList.AsEnumerable()
                         where !string.IsNullOrWhiteSpace(Format.GetString(dr["DEFECTCODE"]))
                         group dr by new { LOTID = dr.Field<string>("LOTID"), DEFECTCODE = dr.Field<string>("DEFECTCODE"), QCSEGMENTID = dr.Field<string>("QCSEGMENTID") } into dg
                         select new
                         {
                             COUNT = dg.Count()
                         };
            var chk = defect.Where(r => r.COUNT > 1).Count();

            if (chk != null && chk > 0)
            {
                defectList.View.RemoveRow(defectList.View.FocusedRowHandle);
                // 같은 불량코드가 중복되었습니다.
                throw MessageException.Create("SameDefectCodeError");
            }
        }
        #endregion

        #region ▶ 불량 입력 데이터 체크 |
        /// <summary>
        /// 불량 입력 데이터 체크
        /// </summary>
        public void CheckDefect()
        {
            // Defect 정보
            DataTable dfList = defectList.DataSource as DataTable;

            // 불량 Row를 추가하였으나 불량코드 선택을 했는지 체크
            if (dfList != null && dfList.Rows.Count > 0)
            {
                if (dfList.AsEnumerable().Count(r => !r.IsNull("DEFECTCODE")).ToInt32() == 0)
                {
                    // 불량코드를 선택하여 주십시오.
                    throw MessageException.Create("NoDefectCode");
                }
                else if (dfList.AsEnumerable().Count(r => !r.Field<decimal?>("QTY").HasValue).ToInt32() > 0 ||
                         dfList.AsEnumerable().Count(r => !r.Field<decimal?>("PNLQTY").HasValue).ToInt32() > 0)
                {
                    // 불량수량은 0이상이어야 합니다.
                    throw MessageException.Create("DefectQtyValidation");
                }
            }
        }
        #endregion

        #region ▶ Grid Footer Sum |
        /// <summary>
        /// Grid Footer Sum
        /// </summary>
        /// <param name="e"></param>
        public void GridSum(DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            DataTable dt = defectList.DataSource as DataTable;

            if (dt == null) return;

            if (dt.Rows.Count > 0)
            {
                decimal PanelSum = 0;
                decimal qtySum = 0;
                dt.Rows.Cast<DataRow>().ForEach(row =>
                {
                    PanelSum += Format.GetDecimal(row["PNLQTY"]);
                    qtySum += Format.GetDecimal(row["QTY"]);
                });

                if (e.Column.FieldName == "PNLQTY")
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
                defectList.View.Columns["PNLQTY"].SummaryItem.DisplayFormat = "  ";
                defectList.View.Columns["QTY"].SummaryItem.DisplayFormat = "  ";
            }
        }

        public void ValidateData()
        {
            DataTable list = defectList.DataSource as DataTable;
            foreach (DataRow row in list.Rows)
            {
                string defectCode = Format.GetString(row["DEFECTCODE"]);
                string defectName = Format.GetString(row["DEFECTCODENAME"]);
                decimal pnlQty = Format.GetDecimal(row["PNLQTY"]);
                decimal qty = Format.GetDecimal(row["QTY"]);

                if (string.IsNullOrEmpty(defectCode) || string.IsNullOrEmpty(defectName))
                {
                    // 불량코드를 선택하여 주십시오.
                    throw MessageException.Create("NoDefectCode");
                }

                if (pnlQty <= 0 || qty <= 0)
                {
                    // 불량수량을 입력해야 합니다.
                    throw MessageException.Create("DefectQtyRequired");
                }
            }
        }

        public void ClearManualDefectData()
        {
            if (pnlDefect.Visible)
            {
                txtDefectCode.Text = "";
                txtQCSegment.Text = "";
                txtPCSQty.Value = 0;
                txtPNLQty.Value = 0;
            }
        }

        #endregion

        #endregion

        // 불량탭이 보일때만 그리드 초기화 및 콤보박스
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible && !Disposing)
            {
                if (!this.isComboboxLoaded)
                {
                    if (!string.IsNullOrEmpty(this.lotId))
                    {
                        doSetConsumableDefComboBox(this.lotId);
                    }
                }
            }
        }
    }
}
