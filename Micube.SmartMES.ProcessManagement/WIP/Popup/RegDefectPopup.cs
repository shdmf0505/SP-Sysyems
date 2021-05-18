#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using System;
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
    /// 프 로 그 램 명  : 공정 관리 > 공정작업 > 작업 시작 > 설비 Recipe 파라미터 팝업
    /// 업  무  설  명  : 작업 시작에서 저장 시 선택된 설비 Recipe의 파라미터 리스트를 보여주는 팝업
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2020-01-08
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class RegDefectPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region ISmartCustomPopup

        public DataRow CurrentDataRow { get; set; }

        public DataTable defectList;
        #endregion

        #region Local Variables

        private DataTable _lotInfo;

        #endregion

        #region 생성자

        public RegDefectPopup()
        {
            InitializeComponent();
        }

        public RegDefectPopup(DataTable dataSource,DataTable dtDefect)
        {
            _lotInfo = dataSource;
            InitializeComponent();

            InitializeEvent();

            grdDefect.DataSource = dtDefect;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        private void InitializeControls()
        {
            grdDefect.VisibleLotId = false;
            grdDefect.VisibleFileUpLoad = false;
            grdDefect.InitializeControls();
            grdDefect.InitializeEvent();
            grdDefect.SetConsumableDefComboBox(Format.GetTrimString(_lotInfo.Rows[0]["LOTID"]));
            setQtyColText();

        }

        #region  원인 품목 팝업
        /// <summary>
        /// 원인 품목 팝업
        /// </summary>
        private void InitializeGrid_CauseProductIdPopup()
        {
            string lotid = Format.GetTrimString(_lotInfo.Rows[0]["LOTID"]);

            var causeProductIdColumn = grdDefect.View.AddSelectPopupColumn("CONSUMABLEDEFNAME", 200, new SqlQuery("GetCauseMaterialList", "10001"))
                .SetPopupLayout("SELECTREASONMATERIAL", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.SizableToolWindow)
                .SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
                .SetLabel("REASONPRODUCT")
                .SetPopupQueryPopup((DataRow currentRow) => {
                    currentRow["LOTID"] = lotid;
                    return true;
                })
                .SetPopupApplySelection((selectedRows, gridRow) => {
                    DataRow row = selectedRows.FirstOrDefault();

                    if (row != null)
                    {
                        string hasRouting = Format.GetString(row["HASROUTING"]);    // 반제품 여부

                        gridRow["HASROUTING"] = Format.GetString(row["HASROUTING"]);
                        gridRow["CONSUMABLEDEFID"] = Format.GetString(row["CONSUMABLEDEFID"]);
                        gridRow["CONSUMABLEDEFVERSION"] = Format.GetString(row["CONSUMABLEDEFVERSION"]);
                        gridRow["REASONCONSUMABLELOTID"] = Format.GetString(row["CONSUMABLEDEFID"]);
                    }
                    else
                    {
                        gridRow["HASROUTING"] = string.Empty;
                        gridRow["CONSUMABLEDEFID"] = string.Empty;
                        gridRow["CONSUMABLEDEFVERSION"] = string.Empty;
                        gridRow["REASONCONSUMABLELOTID"] = string.Empty;
                    }
                });

            causeProductIdColumn.Conditions.AddTextBox("LOTID").SetPopupDefaultByGridColumnId("LOTID").SetIsHidden();
            causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTID").SetPopupDefaultByGridColumnId("PROCESSSEGMENTID").SetIsHidden();
            causeProductIdColumn.Conditions.AddTextBox("PROCESSSEGMENTVERSION").SetPopupDefaultByGridColumnId("PROCESSSEGMENTVERSION").SetIsHidden();

            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
            causeProductIdColumn.GridColumns.AddTextBoxColumn("LOTID", 10).SetIsHidden();
            causeProductIdColumn.GridColumns.AddTextBoxColumn("HASROUTING", 10).SetIsHidden();
            causeProductIdColumn.GridColumns.AddTextBoxColumn("PRODUCT", 10).SetIsHidden();
        }
        #endregion

        #region 원인 공정 팝업
        /// <summary>
        /// 원인 공정 팝업
        /// </summary>
        private void InitializeGrid_CauseSegmentIdPopup()
        {
            var causeSegmentIdColumn = grdDefect.View.AddSelectPopupColumn("PROCESSSEGMENTNAME", 150, new SqlQuery("GetCauseProcessList", "10003", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("SELECTREASONSEGMENT", PopupButtonStyles.Ok_Cancel, false, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.SizableToolWindow)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("CAUSEPROCESS")
                .SetPopupApplySelection((selectedRows, gridRow) => {
                    DataRow row = selectedRows.FirstOrDefault();
                    if (row != null)
                    {
                        gridRow["PROCESSSEGMENTID"] = Format.GetString(row["PROCESSSEGMENTID"]);
                        gridRow["PROCESSSEGMENTVERSION"] = Format.GetString(row["PROCESSSEGMENTVERSION"]);
                        gridRow["REASONSEGMENTID"] = Format.GetString(row["PROCESSSEGMENTID"]);
                    }
                    else
                    {
                        gridRow["PROCESSSEGMENTID"] = string.Empty;
                        gridRow["PROCESSSEGMENTVERSION"] = string.Empty;
                        gridRow["REASONSEGMENTID"] = string.Empty;
                    }
                });

            //조회조건
            causeSegmentIdColumn.Conditions.AddTextBox("LOTID").SetPopupDefaultByGridColumnId("LOTID").SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("HASROUTING").SetPopupDefaultByGridColumnId("HASROUTING").SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFID").SetPopupDefaultByGridColumnId("CONSUMABLEDEFID").SetIsHidden();
            causeSegmentIdColumn.Conditions.AddTextBox("CONSUMABLEDEFVERSION").SetPopupDefaultByGridColumnId("CONSUMABLEDEFVERSION").SetIsHidden();

            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFID", 10).SetIsHidden();
            causeSegmentIdColumn.GridColumns.AddTextBoxColumn("PROCESSDEFVERSION", 10).SetIsHidden();
        }
        #endregion

        #region 불량코드 팝업
        private void InitializeDefectCodePopup()
        {
            var defectCodePopupColumn = grdDefect.View.AddSelectPopupColumn("DEFECTCODE", 120, new SqlQuery("GetDefectCodeByProcess", "10002", "RESOURCETYPE=QCSegmentID", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "DEFECTCODE")
                .SetPopupLayout("SELECTDEFECTCODE", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("QCSEGMENTNAME")
                .SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
                {
                    if (selectedRow.Count() > 0)
                    {
                        grdDefect.View.SetFocusedRowCellValue("DEFECTCODENAME", selectedRow.FirstOrDefault()["DEFECTCODENAME"]);
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTID", selectedRow.FirstOrDefault()["QCSEGMENTID"]);
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTNAME", selectedRow.FirstOrDefault()["QCSEGMENTNAME"]);
                    }
                    else
                    {
                        grdDefect.View.SetFocusedRowCellValue("DEFECTCODENAME", "");
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTID", "");
                        grdDefect.View.SetFocusedRowCellValue("QCSEGMENTNAME", "");
                    }
                });

            // 팝업의 검색조건 항목 추가 (불량코드/명)
            defectCodePopupColumn.Conditions.AddTextBox("TXTDEFECTCODENAME");

            // 팝업의 그리드에 컬럼 추가
            // 불량코드
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODE", 80);
            // 불량코드명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("DEFECTCODENAME", 200);
            // 중공정ID
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTID", 80);
            // 중공정명
            defectCodePopupColumn.GridColumns.AddTextBoxColumn("QCSEGMENTNAME", 200);
        }
        #endregion
        #endregion

        #region Event

        private void InitializeEvent()
        {
            InitializeControls();

            grdDefect.View.AddingNewRow += DefectView_AddingNewRow;
            btnClose.Click += BtnClose_Click;
            btnSave.Click += BtnSave_Click;   
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            grdDefect.ValidateData();

            defectList = grdDefect.DataSource as DataTable;

            if(defectList == null || defectList.Rows.Count == 0)
            {
                if(MSGBox.Show(MessageBoxType.Information,Language.GetMessage("CHECKINPUTDEFECT").Message,MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DefectView_AddingNewRow(SmartBandedGridView sender, AddNewRowArgs args)
        {
            DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();

            if (row == null)
                return;

            //공정 uom별로계산 로직 변경
            //grdDefect.SetInfo(Format.GetInteger(row["PANELPERQTY"].ToString()), Format.GetInteger(row["PCSQTY"].ToString()));
            string processUOM = Format.GetTrimString(row["PROCESSUOM"]);
            decimal pcsArry = Format.GetDecimal(row["PCSARY"].ToString());
            decimal pcsPnl = Format.GetDecimal(row["PCSPNL"].ToString());

            if (pcsArry.Equals(0))
            {
                // grdDefect.View.RemoveRow(grdDefect.View.RowCount-1);

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

            decimal CalQty = processUOM == "BLK" ? pcsPnl / pcsArry : Format.GetInteger(row["PANELPERQTY"].ToString());

            grdDefect.SetInfo(CalQty, Format.GetInteger(row["PCSQTY"].ToString()));
            //grdDefect.SetInfo(Format.GetInteger(row["PANELPERQTY"].ToString()), Format.GetInteger(row["PCSQTY"].ToString()));
            grdDefect.View.SetFocusedRowCellValue("LOTID", Format.GetTrimString(row["LOTID"]));

            setQtyColText();
            //grdDefect.SetConsumableDefComboBox();
        }
        #endregion

        #region private function
        public void setQtyColText()
        {

            DataRow row = _lotInfo.AsEnumerable().FirstOrDefault();

            string Uom = Format.GetTrimString(row["PROCESSUOM"]);

            Color copyColor = grdDefect.View.Columns["DECISIONDEGREENAME"].AppearanceHeader.ForeColor;

            if (!Uom.Equals("PCS"))
            {
                grdDefect.PcsSetting = true;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = true;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = false;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = copyColor;
                //
            }
            else
            {
                grdDefect.PcsSetting = false;
                grdDefect.View.GetConditions().GetCondition("PNLQTY").IsRequired = false;
                grdDefect.View.GetConditions().GetCondition("QTY").IsRequired = true;
                grdDefect.View.Columns["QTY"].AppearanceHeader.ForeColor = Color.Red;
                grdDefect.View.Columns["PNLQTY"].AppearanceHeader.ForeColor = copyColor;
            }

            string ColCaption = Uom == "BLK" ? Language.Get("QTY") + "(" + "BLK" + ")" : Language.Get("QTY").ToString() + "(" + "PNL" + ")";
            grdDefect.View.Columns["PNLQTY"].Caption = ColCaption;
            grdDefect.View.Columns["PNLQTY"].ToolTip = ColCaption;

            grdDefect.DefectUOM = Uom;

        }
        #endregion
    }
}