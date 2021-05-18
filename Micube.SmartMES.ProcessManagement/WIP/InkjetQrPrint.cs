#region using

using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using System;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

#endregion

// TODO : 라벨 디자인 컨펌 받기(DataMatrix)
// TODO : 인쇄범위 지정기능 추가(나중에)

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 잉크젯 QR 바코드 출력
    /// 업  무  설  명  : QR 바코드 출력 화면
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-09-20
    /// 수  정  이  력  : 
    /// 특  이  사  항  : 이 라벨은 600 dpi 프린터에 맞게 디자인 되었음
    /// 
    /// </summary>
    public partial class InkjetQrPrint : SmartConditionManualBaseForm
    {
        #region Local Variables
        private const string LABELDEFID = "InkJetQRCode";
        private const string LABELCLASSID = "InkJetQR";
        private const int LABEL_COLUMNS = 4;

        private const string TRANSACTIONTYPE_PRINT = "Print";
        private const string TRANSACTIONTYPE_REPRINT = "Reprint";
        private const string TRANSACTIONTYPE_CANCEL = "Cancel";

        private const string FO_FIELD_PREFIX = "^FO";
        private const string FO_FIELD_SUFFIX = ",";

        private const int NUMBER_OF_COLUMNS = 4;    // 한줄에 있는 라벨 갯수
        private const int WIDTH_OF_COLUMN = 470;
        #endregion

        #region 생성자

        public InkjetQrPrint()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeWaitForPrintGrid();
            InitializePrintedGrid();
            InitializeEvent();
        }

        /// <summary>        
        /// 출력대기 그리드 초기화
        /// </summary>
        private void InitializeWaitForPrintGrid()
        {
            grdWaitForPrint.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdWaitForPrint.GridButtonItem = GridButtonItem.Export;
            grdWaitForPrint.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdWaitForPrint.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;

            grdWaitForPrint.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly().SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTDEFNAME", 350).SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PCSQTY", 80).SetDisplayFormat("#,##0").SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PANELQTY", 80).SetDisplayFormat("#,##0").SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("WEEK", 80).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("CUSTOMERNAME", 120).SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTIONDATE", 90).SetIsReadOnly().SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("CUSTOMERITEMID", 90).SetIsReadOnly().SetValidationIsRequired();

            grdWaitForPrint.View.AddTextBoxColumn("LOTNUMBER").SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("PCSIDPREFIX").SetIsHidden();
            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTIONREGION").SetIsHidden();
            grdWaitForPrint.View.AddTextBoxColumn("MKWEEK").SetIsHidden();
            grdWaitForPrint.View.AddTextBoxColumn("PRINTABLE").SetIsHidden();
            grdWaitForPrint.View.AddTextBoxColumn("CREATED").SetIsHidden();
            grdWaitForPrint.View.PopulateColumns();

            grdWaitForPrintPcs.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdWaitForPrintPcs.GridButtonItem = GridButtonItem.Export;
            grdWaitForPrintPcs.View.AddTextBoxColumn("NO", 80).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdWaitForPrintPcs.View.AddTextBoxColumn("PCSID", 260).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdWaitForPrintPcs.View.PopulateColumns();
        }

        /// <summary>        
        /// 출력완료 그리드 초기화
        /// </summary>
        private void InitializePrintedGrid()
        {
            grdPrinted.GridButtonItem = GridButtonItem.Export;
            grdPrinted.View.AddTextBoxColumn("LOTID", 190).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PRODUCTDEFID", 130).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PRODUCTDEFNAME", 350).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("USERSEQUENCE", 80).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PCSQTY", 80).SetDisplayFormat("#,##0").SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PANELQTY", 80).SetDisplayFormat("#,##0").SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("WEEK", 80).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("CUSTOMERNAME", 120).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PRODUCTIONDATE", 90).SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("CUSTOMERITEMID", 90).SetIsReadOnly();

            grdPrinted.View.AddTextBoxColumn("PRODUCTIONREGION").SetIsHidden();
            grdPrinted.View.AddTextBoxColumn("LOTNUMBER").SetIsHidden();
            grdPrinted.View.AddTextBoxColumn("MKWEEK").SetIsHidden();
            grdPrinted.View.AddTextBoxColumn("PRINTABLE").SetIsHidden();
            grdPrinted.View.PopulateColumns();

            grdPrintedPcs.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPrintedPcs.GridButtonItem = GridButtonItem.Export;
            grdPrintedPcs.View.AddTextBoxColumn("NO", 80).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdPrintedPcs.View.AddTextBoxColumn("PCSID", 260).SetTextAlignment(Framework.SmartControls.TextAlignment.Center).SetIsReadOnly();
            grdPrintedPcs.View.AddTextBoxColumn("PRINTCOUNT", 60).SetIsReadOnly();
            grdPrintedPcs.View.PopulateColumns();
        }
        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            grdWaitForPrint.View.FocusedRowChanged += grdWaitForPrint_View_FocusedRowChanged;
            grdWaitForPrint.View.RowStyle += grdWaitForPrintView_RowStyle;
            grdWaitForPrint.View.CheckStateChanged += grdWaitForPrintView_CheckStateChanged;
            grdWaitForPrint.View.ShowingEditor += View_ShowingEditor;
            grdWaitForPrint.View.RowCellStyle += View_RowCellStyle;
            grdWaitForPrint.View.ValidatingEditor += View_ValidatingEditor;
            grdWaitForPrint.View.InvalidValueException += View_InvalidValueException;

            grdWaitForPrintPcs.View.CheckStateChanged += grdWaitForPrintPcsView_CheckStateChanged;

            grdPrinted.View.FocusedRowChanged += grdPrinted_View_FocusedRowChanged;

            btnSelect.Click += BtnSelect_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            int index = tabPartition.SelectedTabPageIndex;
            switch (index)
            {
                case 1:
                    // 선택된 LOT이 없음
                    DataRow row = grdPrinted.View.GetFocusedDataRow();
                    if (row == null)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }

                    if (ShowMessage(MessageBoxButtons.YesNo, "PcsIdPopupCancel", row["LOTID"].ToString()) == DialogResult.No)
                    {
                        return;
                    }

                    string lotId = row["LOTID"].ToString();

                    CancelPrintAsync(lotId);
                    break;
            }
        }

        private void View_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            /*
            if (column.FieldName == "PRODUCTIONDATE")
            {
                if (e.Value.ToString().Length != 5)
                {
                    e.Valid = false;
                }
                else
                {
                    DataRow row = grdWaitForPrint.View.GetFocusedDataRow();
                    row["PRODUCTIONDATE"] = e.Value;
                    if (row["CUSTOMERITEMID"] != DBNull.Value
                        && row["WEEK"] != DBNull.Value
                        && row["LOTNUMBER"] != DBNull.Value
                        && !string.IsNullOrWhiteSpace(row["CUSTOMERITEMID"].ToString())
                        && !string.IsNullOrWhiteSpace(row["WEEK"].ToString())
                        && !string.IsNullOrWhiteSpace(row["LOTNUMBER"].ToString()))
                    {
                        row["PRINTABLE"] = "Y";
                    }
                    else
                    {
                        row["PRINTABLE"] = "N";
                    }
                    SearchWaitForPrintPcs(grdWaitForPrint.View.FocusedRowHandle);
                }
            }*/

            if (column.FieldName == "WEEK")
            {
                if (e.Value.ToString().Length != 4)
                {
                    e.Valid = false;
                }
                else
                {
                    DataRow row = grdWaitForPrint.View.GetFocusedDataRow();
                    row["WEEK"] = e.Value;
                    row["MKWEEK"] = e.Value;
                    if (row["CUSTOMERITEMID"] != DBNull.Value
                        && row["PRODUCTIONDATE"] != DBNull.Value
                        && row["LOTNUMBER"] != DBNull.Value
                        && !string.IsNullOrWhiteSpace(row["CUSTOMERITEMID"].ToString())
                        && !string.IsNullOrWhiteSpace(row["PRODUCTIONDATE"].ToString())
                        && !string.IsNullOrWhiteSpace(row["LOTNUMBER"].ToString()))
                    {
                        row["PRINTABLE"] = "Y";
                    }
                    else
                    {
                        row["PRINTABLE"] = "N";
                    }
                    SearchWaitForPrintPcs(grdWaitForPrint.View.FocusedRowHandle);
               }
            }
        }

        private void View_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            if (view == null)
            {
                return;
            }
            e.ExceptionMode = ExceptionMode.Ignore;
        }

        private void View_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if(e.Column.FieldName == "WEEK")
            {
                if (grdWaitForPrint.View.GetRowCellValue(e.RowHandle, "CREATED").ToString() == "N")
                {
                    e.Appearance.BackColor = System.Drawing.Color.LightYellow;
                    e.Appearance.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void View_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SmartBandedGridView gridView = sender as SmartBandedGridView;
            DataRow row = gridView.GetFocusedDataRow();
            if(row["CREATED"].ToString() == "Y")
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 출력대기 그리드에서 선택된 LOT의 PCS 목록을 조회(로컬에서 데이터 생성)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWaitForPrint_View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchWaitForPrintPcs(e.FocusedRowHandle);
        }

        private void SearchWaitForPrintPcs(int waitForPrintRowhandle)
        {
            DataRow row = grdWaitForPrint.View.GetDataRow(waitForPrintRowhandle);
            int pcsQty = 0;
            if (row != null && int.TryParse(row["PCSQTY"].ToString(), out pcsQty))
            {
                grdWaitForPrintPcs.DataSource = CreatePcsIdDataTable(row, pcsQty);
                grdWaitForPrint.View.CheckStateChanged -= grdWaitForPrintView_CheckStateChanged;
                grdWaitForPrint.View.CheckedAll(false);
                grdWaitForPrint.View.CheckStateChanged += grdWaitForPrintView_CheckStateChanged;
            }
            else
            {
                grdWaitForPrintPcs.DataSource = null;
            }
        }

        private DataTable CreatePcsIdDataTable(DataRow lot, int pcsQty)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", lot["LOTID"].ToString());
            param.Add("PRINTED", "Y");
            DataTable printedPcs = SqlExecuter.Query("SelectPrintedLotPcs", "10002", param);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NO");
            dataTable.Columns.Add("PCSID");

            string barcodePrefix = lot["PRODUCTIONREGION"].ToString() + lot["CUSTOMERITEMID"].ToString() + lot["LOTNUMBER"].ToString() + lot["MKWEEK"].ToString() + lot["PRODUCTIONDATE"].ToString();

            for (int i = 1; i <= pcsQty; i++)
            {
                if (ContainsPcsId(printedPcs, barcodePrefix + i.ToString("D5")))
                {
                    continue;
                }
                DataRow newRow = dataTable.NewRow();
                newRow["NO"] = i.ToString();
                if (lot["PRINTABLE"].ToString() == "Y")
                {
                    newRow["PCSID"] = barcodePrefix + i.ToString("D5");
                }
                dataTable.Rows.Add(newRow);
            }
            return dataTable;
        }

        private bool ContainsPcsId(DataTable dataTable, string pcsId)
        {
            foreach(DataRow each in dataTable.Rows)
            {
                if (each["PCSID"].ToString() == pcsId)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 바코드 생성이 불가능 한 LOT은 회색으로 표시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWaitForPrintView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if ((string)grdWaitForPrint.View.GetRowCellValue(e.RowHandle, "PRINTABLE") == "N")
            {
                e.Appearance.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void grdWaitForPrintView_CheckStateChanged(object sender, System.EventArgs e)
        {
            // 한 행만 체크 가능
            if (!grdWaitForPrint.View.IsRowChecked(grdWaitForPrint.View.FocusedRowHandle))
            {
                grdWaitForPrintPcs.View.CheckStateChanged -= grdWaitForPrintPcsView_CheckStateChanged;
                grdWaitForPrintPcs.View.CheckedAll(false);
                grdWaitForPrintPcs.View.CheckStateChanged += grdWaitForPrintPcsView_CheckStateChanged;
            }
            else
            {
                grdWaitForPrint.View.CheckStateChanged -= grdWaitForPrintView_CheckStateChanged;
                grdWaitForPrint.View.CheckedAll(false);
                DataRow lotRow = grdWaitForPrint.View.GetFocusedDataRow();
                if (lotRow["PRINTABLE"].ToString() == "Y")
                {
                    grdWaitForPrint.View.CheckRow(grdWaitForPrint.View.FocusedRowHandle, true);
                    grdWaitForPrintPcs.View.CheckStateChanged -= grdWaitForPrintPcsView_CheckStateChanged;
                    grdWaitForPrintPcs.View.CheckedAll(true);
                    grdWaitForPrintPcs.View.CheckStateChanged += grdWaitForPrintPcsView_CheckStateChanged;

                    DataTable pcs = grdWaitForPrintPcs.DataSource as DataTable;
                    if (pcs == null || pcs.Rows.Count == 0)
                    {
                        lseStartNo.Editor.Value = 0;
                        lseEndNo.Editor.Value = 0;
                    }
                    else
                    {
                        lseStartNo.Editor.Value = int.Parse(pcs.Rows[0]["NO"].ToString());
                        lseEndNo.Editor.Value = int.Parse(pcs.Rows[pcs.Rows.Count - 1]["NO"].ToString());
                    }
                }
                else
                {
                    lseStartNo.Editor.Value = 0;
                    lseEndNo.Editor.Value = 0;
                }
                grdWaitForPrint.View.CheckStateChanged += grdWaitForPrintView_CheckStateChanged;
            }
        }

        private void grdWaitForPrintPcsView_CheckStateChanged(object sender, System.EventArgs e)
        {
            if (!grdWaitForPrint.View.IsRowChecked(grdWaitForPrint.View.FocusedRowHandle))
            {
                grdWaitForPrintPcs.View.CheckStateChanged -= grdWaitForPrintPcsView_CheckStateChanged;
                grdWaitForPrintPcs.View.CheckedAll(false);
                grdWaitForPrintPcs.View.CheckStateChanged += grdWaitForPrintPcsView_CheckStateChanged;
            }
        }

        /// <summary>
        /// 출력완료 그리드에서 선택된 LOT의 PCS 목록을 조회(DB에서 조회)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPrinted_View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchPrintedPcsAsync(e.FocusedRowHandle);
        }

        private async void SearchPrintedPcsAsync(int printedRowHandle)
        {
            DataRow row = grdPrinted.View.GetDataRow(printedRowHandle);
            if (row != null)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", row["LOTID"].ToString());
                param.Add("PRINTED", "Y");
                grdPrintedPcs.DataSource = await QueryAsync("SelectPrintedLotPcs", "10002", param);
            }
            else
            {
                grdPrintedPcs.DataSource = null;
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            int index = tabPartition.SelectedTabPageIndex;
            switch (index)
            {
                case 0:
                    // 선택된 LOT이 없음
                    if(grdWaitForPrint.View.GetCheckedRows().Rows.Count == 0
                        || grdWaitForPrintPcs.View.GetCheckedRows().Rows.Count == 0)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }

                    SavePrint();
                    Print();
                    break;
                case 1:
                    // 선택된 LOT이 없음
                    DataRow row = grdPrinted.View.GetFocusedDataRow();
                    if (row == null)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }

                    // 선택된 PCS 가 없음
                    if (grdPrintedPcs.View.GetCheckedRows().Rows.Count == 0)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }

                    string lotId = row["LOTID"].ToString();

                    SaveReprint(lotId);
                    ReprintPcs(lotId);
                    break;
            }
        }

        /// <summary>
        /// 잉크젯 QR 라벨 발행 여부 저장
        /// </summary>
        private void SavePrint()
        {
            MessageWorker worker = new MessageWorker("SaveInkjetQrPrint");

            DataTable checkedLots = grdWaitForPrint.View.GetCheckedRows();
            foreach(DataRow each in checkedLots.Rows)
            {
                each["PCSIDPREFIX"] = each["PRODUCTIONREGION"].ToString() + each["CUSTOMERITEMID"].ToString()
                    + each["LOTNUMBER"].ToString() + each["MKWEEK"].ToString() + each["PRODUCTIONDATE"].ToString();
            }
            DataTable checkedPcs = grdWaitForPrintPcs.View.GetCheckedRows();

            worker.SetBody(new MessageBody()
                    {
                        { "ENTERPRISEID", UserInfo.Current.Enterprise },
                        { "PLANTID", UserInfo.Current.Plant },
                        { "TRANSACTIONTYPE", TRANSACTIONTYPE_PRINT },
                        { "LOTLIST", checkedLots },
                        { "PCSLIST", PcsTableToArray(checkedPcs) }   // NOTE : 데이터 크기를 줄이기 위해 Array로 변환해서 전달
                    });
            worker.Execute();
        }

        private List<int> PcsTableToArray(DataTable pcsTable)
        {
            List<int> pcsNoList = new List<int>();
            foreach(DataRow each in pcsTable.Rows)
            {
                pcsNoList.Add(int.Parse(each["NO"].ToString()));
            }
            return pcsNoList;
        }

        /// <summary>
        /// 잉크젯 QR 라벨 발행
        /// </summary>
        private void Print()
        {
            DataTable lotsToPrint = grdWaitForPrint.View.GetCheckedRows();
            DataTable pcsToPrint = grdWaitForPrintPcs.View.GetCheckedRows();
            DataTable dataTableForPrint = CreatePcsBarcodesForPrint(lotsToPrint, pcsToPrint);
            PrintLabel(dataTableForPrint);
        }

        /// <summary>
        /// 재발행 여부 저장
        /// </summary>
        /// <param name="lotId"></param>
        private void SaveReprint(string lotId)
        {
            MessageWorker worker = new MessageWorker("SaveInkjetQrPrint");

            worker.SetBody(new MessageBody()
                    {
                        { "ENTERPRISEID", UserInfo.Current.Enterprise },
                        { "PLANTID", UserInfo.Current.Plant },
                        { "TRANSACTIONTYPE", TRANSACTIONTYPE_REPRINT },
                        { "LOTID", lotId },
                        { "LOTLIST", grdPrintedPcs.View.GetCheckedRows() }
                    });
            worker.Execute();
        }

        private async void CancelPrintAsync(string lotId)
        {
            MessageWorker worker = new MessageWorker("SaveInkjetQrPrint");

            worker.SetBody(new MessageBody()
                    {
                        { "ENTERPRISEID", UserInfo.Current.Enterprise },
                        { "PLANTID", UserInfo.Current.Plant },
                        { "TRANSACTIONTYPE", TRANSACTIONTYPE_CANCEL },
                        { "LOTID", lotId }
                    });
            worker.Execute();
            await OnSearchAsync();
        }

        /// <summary>
        /// 재발행
        /// </summary>
        /// <param name="lotId"></param>
        private void ReprintPcs(string lotId)
        {
            DataRow lot = grdPrinted.View.GetFocusedDataRow();
            DataTable dataTableForPrint = CreatePcsBarcodesForReprint(lot, grdPrintedPcs.View.GetCheckedRows());
            PrintLabel(dataTableForPrint);
        }

        /// <summary>
        /// 잉크젯 QR 바코드 발행을 위해 PCS 바코드 생성
        /// </summary>
        /// <param name="lotList"></param>
        /// <returns></returns>
        private DataTable CreatePcsBarcodesForPrint(DataTable lotList, DataTable pcsList)
        {
            DataTable result = lotList.Clone();
            result.Columns.Add("NO");
            result.Columns.Add("PCSID");

            foreach (DataRow eachLot in lotList.Rows)
            {
                foreach(DataRow eachPcs in pcsList.Rows)
                {
                    DataRow newRow = result.NewRow();
                    newRow["LOTID"] = eachLot["LOTID"];
                    newRow["PRODUCTIONREGION"] = eachLot["PRODUCTIONREGION"];
                    newRow["CUSTOMERITEMID"] = eachLot["CUSTOMERITEMID"];
                    newRow["LOTNUMBER"] = eachLot["LOTNUMBER"];
                    newRow["MKWEEK"] = eachLot["MKWEEK"];
                    newRow["PRODUCTIONDATE"] = eachLot["PRODUCTIONDATE"];
                    newRow["NO"] = int.Parse(eachPcs["NO"].ToString()).ToString("D4");
                    newRow["PCSID"] = newRow["PRODUCTIONREGION"].ToString() + newRow["CUSTOMERITEMID"].ToString() + newRow["LOTNUMBER"].ToString()
                        + newRow["MKWEEK"].ToString() + newRow["PRODUCTIONDATE"].ToString() + int.Parse(eachPcs["NO"].ToString()).ToString("D5");
                    result.Rows.Add(newRow);
                }
            }
            return result;
        }

        /// <summary>
        /// 잉크젯 QR 바코드 재발행을 위해 PCS 바코드 생성
        /// </summary>
        /// <param name="lotInfo"></param>
        /// <param name="pcsList"></param>
        /// <returns></returns>
        private DataTable CreatePcsBarcodesForReprint(DataRow lotInfo, DataTable pcsList)
        {
            DataTable result = lotInfo.Table.Clone();
            result.Columns.Add("NO");
            result.Columns.Add("PCSID");

            foreach (DataRow eachPcs in pcsList.Rows)
            {
                string pcsId = eachPcs["PCSID"].ToString();

                DataRow newRow = result.NewRow();
                newRow["LOTID"] = lotInfo["LOTID"];
                newRow["PRODUCTIONREGION"] = pcsId.Substring(0, 1);
                newRow["CUSTOMERITEMID"] = pcsId.Substring(1, 6);
                newRow["LOTNUMBER"] = pcsId.Substring(7, 6);
                newRow["MKWEEK"] = pcsId.Substring(13, 4);
                newRow["PRODUCTIONDATE"] = pcsId.Substring(17, 5);
                newRow["NO"] = pcsId.Substring(pcsId.Length - 4, 4);
                newRow["PCSID"] = pcsId;
                result.Rows.Add(newRow);
            }
            return result;
        }

        /// <summary>
        /// 라벨 발행
        /// </summary>
        /// <param name="dataTable"></param>
        private void PrintLabel(DataTable dataTable)
        {
            // ZPL 스크립트를 가져온다
            string zplFormat = CommonFunction.GetLabelScriptByDefId("InkJetQRCode");

            if (zplFormat != null)
            {
                StringBuilder pages = new StringBuilder();

                int col = 0;
                foreach (DataRow eachRow in dataTable.Rows)
                {
                    if(col == 0)
                    {
                        pages.Append("^XA");
                    }

                    StringBuilder eachLabel = new StringBuilder(UpdateXPosition(zplFormat, col, WIDTH_OF_COLUMN));

                    foreach (DataColumn eachColumn in dataTable.Columns)
                    {
                        if(eachRow[eachColumn.ColumnName] != DBNull.Value)
                        {
                            eachLabel.Replace(string.Format("{{!@{0}}}", eachColumn.ColumnName), eachRow[eachColumn.ColumnName].ToString());
                        }
                    }
                    pages.Append(eachLabel.ToString());

                    col++;
                    if (col == NUMBER_OF_COLUMNS)
                    {
                        pages.Append("^XZ");
                        col = 0;
                    }
                }
                if(col != 0)
                {
                    pages.Append("^XZ");
                }
                Commons.CommonFunction.PrintLabel(pages.ToString());
            }
            else
            {
                MessageBox.Show(string.Format("등록된 스크립트가 없습니다 Label ID : {0}", "InkjetQR"));
            }
        }

        private string UpdateXPosition(string zplCommand, int column, int widthOfColumn)
        {
            int first = 0;
            int last = -1;

            string result = zplCommand;
            while (true)
            {
                first = zplCommand.IndexOf(FO_FIELD_PREFIX, first);
                if (first < 0)
                {
                    break;
                }
                last = zplCommand.IndexOf(FO_FIELD_SUFFIX, first + FO_FIELD_PREFIX.Length);
                if (last < 0)
                {
                    throw new SystemException("라벨형식오류");
                }
                string field = zplCommand.Substring(first, last - first + FO_FIELD_SUFFIX.Length);
                first = last + FO_FIELD_SUFFIX.Length;

                int xPos = int.Parse(field.Substring(FO_FIELD_PREFIX.Length, field.Length - FO_FIELD_PREFIX.Length - FO_FIELD_SUFFIX.Length)) + (column * widthOfColumn);
                string newValue = FO_FIELD_PREFIX + xPos + FO_FIELD_SUFFIX;
                result = result.Replace(field, newValue);
            }
            return result;
        }

        private List<string> FindImageFields(string zplCommand)
        {
            List<string> result = new List<string>();

            int first = 0;
            int last = -1;

            while (true)
            {
                first = zplCommand.IndexOf(FO_FIELD_PREFIX, first);
                if (first < 0)
                {
                    break;
                }
                last = zplCommand.IndexOf(FO_FIELD_SUFFIX, first + FO_FIELD_PREFIX.Length);
                if (last < 0)
                {
                    throw new SystemException("라벨형식오류");
                }
                string field = zplCommand.Substring(first, last - first + FO_FIELD_SUFFIX.Length);
                result.Add(field);
                first = last + FO_FIELD_SUFFIX.Length;
            }
            return result;
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            DataTable dataTable;

            int index = tabPartition.SelectedTabPageIndex;
            switch (index)
            {
                case 0:
                    grdWaitForPrintPcs.DataSource = null;
                    values.Add("P_TRANSACTIONTYPE", TRANSACTIONTYPE_PRINT);
                    dataTable = await QueryAsync("SelectLotListForInkjetQrPrint", "10001", values);
                    if (dataTable.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdWaitForPrint.DataSource = dataTable;
                    if (dataTable.Rows.Count > 0)
                    {
                        SearchWaitForPrintPcs(grdWaitForPrint.View.FocusedRowHandle);
                    }
                    break;
                case 1:
                    grdPrintedPcs.DataSource = null;
                    values.Add("P_TRANSACTIONTYPE", TRANSACTIONTYPE_REPRINT);
                    dataTable = await QueryAsync("SelectLotListForInkjetQrPrint", "10001", values);
                    if (dataTable.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdPrinted.DataSource = dataTable;
                    if (dataTable.Rows.Count > 0)
                    {
                        SearchPrintedPcsAsync(grdPrinted.View.FocusedRowHandle);
                    }
                    break;
            }
        }

        /// <summary>
        /// 조회조건 초기화
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, false, Conditions);
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.2, false, Conditions);
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0.3, false, Conditions, true, true);
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 0.4, false, Conditions);
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        private void BtnSelect_Click(object sender, System.EventArgs e)
        {
            DataTable pcs = grdWaitForPrintPcs.DataSource as DataTable;
            int min = int.Parse(pcs.Rows[0]["NO"].ToString());
            int max = int.Parse(pcs.Rows[pcs.Rows.Count - 1]["NO"].ToString());
            if (lseStartNo.Editor.Value > lseEndNo.Editor.Value || lseStartNo.Editor.Value < min || lseEndNo.Editor.Value > max)
            {
                // 시작번호 또는 마지막번호가 범위를 벗어났습니다.
                throw MessageException.Create("OutOfRange");
            }
            grdWaitForPrintPcs.View.CheckStateChanged -= grdWaitForPrintPcsView_CheckStateChanged;
            grdWaitForPrintPcs.View.CheckedAll(false);
            grdWaitForPrintPcs.View.CheckStateChanged += grdWaitForPrintPcsView_CheckStateChanged;
            foreach (DataRow each in pcs.Rows)
            {
                int no = int.Parse(each["NO"].ToString());
                if (no >= lseStartNo.Editor.Value && no <= lseEndNo.Editor.Value)
                {
                    grdWaitForPrintPcs.View.CheckRow(grdWaitForPrintPcs.View.GetRowHandle(pcs.Rows.IndexOf(each)), true);
                }
            }
        }
    }
}
