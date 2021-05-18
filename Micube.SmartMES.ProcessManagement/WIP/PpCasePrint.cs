#region using

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using System.Collections.Generic;
using Micube.SmartMES.Commons.Controls;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > PP CASE 바코드 출력
    /// 업  무  설  명  : PP CASE 바코드 출력 화면
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-11-28
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PpCasePrint : SmartConditionManualBaseForm
    {
        // TODO : 라벨 용지크기 확인 필요
        #region Local Variables
        private const string LABELDEFID = "PpCaseLabel";    
        private const string LABELCLASSID = "InkJetQR";

        private const string TRANSACTIONTYPE_PRINT = "Print";
        private const string TRANSACTIONTYPE_REPRINT = "Reprint";

        private readonly object locker = new object();
        #endregion

        #region 생성자

        public PpCasePrint()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeControls();
            InitializeWaitForPrintGrid();
            InitializePrintedGrid();
            InitializeEvent();
        }

        private void InitializeControls()
        {
            txtCurrentSeq.ReadOnly = true;
        }

        /// <summary>        
        /// 출력대기 그리드 초기화
        /// </summary>
        private void InitializeWaitForPrintGrid()
        {
            grdWaitForPrint.View.AddTextBoxColumn("LOTID", 190)                 // LOT ID
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTDEFID", 130)          // 품목 ID
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTDEFNAME", 350)        // 품목명
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)    // 공정명
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("USERSEQUENCE", 80)           // 공정순서
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PCSQTY", 80)                 // LOT PCS 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PANELQTY", 80)               // LOT Panel 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("TRAYQTY", 85)                // 단위 pack 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("CASEPCSQTY", 85)             // 단위 case 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("CASEQTY", 110)               // PP Case 수량 CEILING(LOT.PCSQTY / (PKG.CASEPCSQTY * PKG.TRAYQTY))
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("CURRENT_CASEQTY", 80)        // 현재 채번된 PP Case 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("CURRENT_PCSQTY", 80)         // 현재 스캔된 PCS 수량
                .SetDisplayFormat("#,##0").SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("WEEK", 80)                   // 주차
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("CUSTOMERNAME", 120)          // 고객사명
                .SetIsReadOnly();
            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTIONDATE", 90)         // 생산일(투입일) YMMDD
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdWaitForPrint.View.AddTextBoxColumn("CUSTOMERITEMID", 90)         // 고객사품명(6자리)
                .SetIsReadOnly()
                .SetValidationIsRequired();

            grdWaitForPrint.View.AddTextBoxColumn("PRODUCTIONREGION")           // 생산지(1자리) Y = YP(영풍)
                .SetIsHidden();
            grdWaitForPrint.View.AddTextBoxColumn("LOTNUMBER")                  // LOTID 마지막 6자리
                .SetIsHidden();
            grdWaitForPrint.View.AddTextBoxColumn("MKWEEK")                     // M/K 주차(4자리)
                .SetIsHidden();
            grdWaitForPrint.View.AddTextBoxColumn("PRINTABLE")                  // 라벨 발행가능 여부(N 인 경우는 바코드 생성관련 데이터 누락, 흐린색으로 표시)
                .SetIsHidden();
            grdWaitForPrint.View.PopulateColumns();

            grdWaitForPrintPcs.View.AddTextBoxColumn("PPCASEID", 220)           // PP Case ID
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdWaitForPrintPcs.View.AddTextBoxColumn("SEQ", 80)                 // 순번(재조회 시 순서가 달라질 수 있음)
                .SetIsReadOnly();
            grdWaitForPrintPcs.View.AddTextBoxColumn("PCSID", 260)              // PCS ID
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdWaitForPrintPcs.View.AddTextBoxColumn("PRINTCOUNT", 80)          // 출력횟수
                .SetIsReadOnly();
            grdWaitForPrintPcs.View.OptionsView.AllowCellMerge = true;          // CellMerge
            grdWaitForPrintPcs.View.PopulateColumns();
        }

        /// <summary>        
        /// 출력완료 그리드 초기화
        /// </summary>
        private void InitializePrintedGrid()
        {
            grdPrinted.View.AddTextBoxColumn("LOTID", 190)                          // LOT ID
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PRODUCTDEFID", 130)                   // 품목 ID
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PRODUCTDEFNAME", 350)                 // 품목명
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)             // 공정명
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("USERSEQUENCE", 80)                    // 공정순서
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PCSQTY", 80)                          // LOT PCS 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PANELQTY", 80)                        // LOT Panel 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("TRAYQTY", 85)                         // 단위 Pack 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdPrinted.View.AddTextBoxColumn("CASEPCSQTY", 85)                      // 단위 Case 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly()
                .SetValidationIsRequired();
            grdPrinted.View.AddTextBoxColumn("CASEQTY", 110)                        // PP Case 수량 CEILING(LOT.PCSQTY / (PKG.CASEPCSQTY * PKG.TRAYQTY))
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("CURRENT_CASEQTY", 80)                 // 현재 채번된 PP Case 수량
                .SetDisplayFormat("#,##0")
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("CURRENT_PCSQTY", 80)                  // 현재 스캔된 PCS 수량
                .SetDisplayFormat("#,##0").SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("WEEK", 80)                            // 주차
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("CUSTOMERNAME", 120)                   // 고객사명
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("PRODUCTIONDATE", 90)                  // 생산일(투입일) YMMD
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdPrinted.View.AddTextBoxColumn("CUSTOMERITEMID", 90)                  // 고객사품명(6자리)
                .SetIsReadOnly();

            grdPrinted.View.AddTextBoxColumn("PRODUCTIONREGION")                    // 생산지(1자리) Y = YP(영풍)
                .SetIsHidden();
            grdPrinted.View.AddTextBoxColumn("LOTNUMBER")                           // LOTID 마지막 6자리
                .SetIsHidden();
            grdPrinted.View.AddTextBoxColumn("MKWEEK")                              // M/K 주차(4자리)
                .SetIsHidden();
            grdPrinted.View.AddTextBoxColumn("PRINTABLE")                           // 라벨 발행가능 여부(N 인 경우는 바코드 생성관련 데이터 누락, 흐린색으로 표시)
                .SetIsHidden();
            grdPrinted.View.PopulateColumns();

            grdPrintedPpCase.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPrintedPpCase.View.AddTextBoxColumn("PPCASEID", 220)                    // PP Case ID
                .SetTextAlignment(Framework.SmartControls.TextAlignment.Center)
                .SetIsReadOnly();
            grdPrintedPpCase.View.AddTextBoxColumn("PCSQTY", 80)                       // PP Case에 맵핑된 PCS 수량
                .SetIsReadOnly();
            grdPrintedPpCase.View.AddTextBoxColumn("PRINTCOUNT", 60)                   // PP Case 발행 횟수
                .SetIsReadOnly();
            grdPrintedPpCase.View.AddTextBoxColumn("LOTID")                            // LOT ID
                .SetIsHidden();
            grdPrintedPpCase.View.PopulateColumns();
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

            grdWaitForPrintPcs.View.CellMerge += grdWaitForPrintPcsView_CellMerge;

            grdPrinted.View.FocusedRowChanged += grdPrinted_View_FocusedRowChanged;

            txtPcsNoScan.KeyDown += TxtPcsNoScan_KeyDown;
        }

        private void TxtPcsNoScan_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                SaveScan();
            }
        }

        /// <summary>
        /// 스캔한 PCS ID 목록 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWaitForPrint_View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchScannedPcsOfFocusedLotAsync();
        }

        private async void SearchScannedPcsOfFocusedLotAsync()
        {
            DataRow row = grdWaitForPrint.View.GetFocusedDataRow();
            int pcsQty = 0;
            if (row != null && int.TryParse(row["PCSQTY"].ToString(), out pcsQty))
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", row["LOTID"].ToString());
                DataTable result = await QueryAsync("SelectScannedPcsIdForPpCasePrint", "10001", param);
                grdWaitForPrintPcs.DataSource = result;
                txtCurrentSeq.Text = result.Rows.Count.ToString();
            }
            else
            {
                grdWaitForPrintPcs.DataSource = null;
                txtCurrentSeq.Text = string.Empty;
            }
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

        /// <summary>
        /// 라벨 발행에 필요한 정보가 없는 LOT은 체크박스에 체크할 수 없도록 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWaitForPrintView_CheckStateChanged(object sender, System.EventArgs e)
        {
            DataTable dataTable = grdWaitForPrint.DataSource as DataTable;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow each = dataTable.Rows[i];
                int rowHandle = grdWaitForPrint.View.GetRowHandle(i);
                if (each["PRINTABLE"].ToString() == "N" && grdWaitForPrint.View.IsRowChecked(rowHandle))
                {
                    grdWaitForPrint.View.CheckRow(rowHandle, false);
                }
            }
        }

        /// <summary>
        /// PPCASEID 컬럼 셀머지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWaitForPrintPcsView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName != "PPCASEID")
            {
                e.Handled = true;
                return;
            }
            GridView view = sender as GridView;
            string val1 = (string)view.GetRowCellValue(e.RowHandle1, e.Column);
            string val2 = (string)view.GetRowCellValue(e.RowHandle2, e.Column);
            e.Merge = val1 == val2;
            e.Handled = true;
        }

        /// <summary>
        /// 출력완료 그리드에서 선택된 LOT의 PP CASE 목록을 조회(DB에서 조회)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPrinted_View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchPpCaseListOfFocusedLotAsync();
        }

        private async void SearchPpCaseListOfFocusedLotAsync()
        {
            DataRow row = grdPrinted.View.GetFocusedDataRow();
            if (row != null)
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LOTID", row["LOTID"].ToString());
                grdPrintedPpCase.DataSource = await QueryAsync("SelectPpCaseIdForPpCasePrint", "10001", param);
            }
            else
            {
                grdPrintedPpCase.DataSource = null;
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
                    // 저장버튼 기능 없음
                    break;
                case 1:
                    // 선택된 LOT이 없음
                    DataRow row = grdPrinted.View.GetFocusedDataRow();
                    if (row == null)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }

                    // 선택된 PP Case 가 없음
                    if (grdPrintedPpCase.View.GetCheckedRows().Rows.Count == 0)
                    {
                        // 저장할 데이터가 존재하지 않습니다.
                        throw MessageException.Create("NoSaveData");
                    }

                    SaveReprint();
                    ReprintPcs();
                    break;
            }
        }

        /// <summary>
        /// 잉크젯 QR 라벨 발행 여부 저장
        /// </summary>
        private void SaveScan()
        {
            DataRow lotRow = grdWaitForPrint.View.GetFocusedDataRow();
            if(lotRow == null)
            {
                return;
            }
            if (lotRow["PRINTABLE"].ToString() == "N")
            {
                // 라벨 발행에 필요한 정보가 누락된 LOT입니다. {0}
                throw MessageException.Create("NotPrintableLot", lotRow["LOTID"].ToString());
            }

            DataTable resultData;

            // 동시에 여러 요청이 발생할 시 PP Case 라벨을 발행하지 못하는 문제를 해결하기 위해 동시처리 제한
            lock (locker)   
            {
                MessageWorker worker = new MessageWorker("SavePpCasePrint");

                worker.SetBody(new MessageBody()
                {
                    { "TRANSACTIONTYPE", TRANSACTIONTYPE_PRINT },
                    { "LOTID", lotRow["LOTID"].ToString() },
                    { "PCSID", txtPcsNoScan.Text }
                });
                var result = worker.Execute<DataTable>();
                resultData = result.GetResultSet();
            }

            DataTable pcsDataTable = grdWaitForPrintPcs.DataSource as DataTable;    
            DataRow newRow = pcsDataTable.NewRow();
            newRow["PPCASEID"] = resultData.Rows[0]["PPCASEID"];
            newRow["SEQ"] = pcsDataTable.Rows.Count + 1;
            newRow["PCSID"] = resultData.Rows[0]["PCSID"];
            newRow["PRINTCOUNT"] = resultData.Rows[0]["PRINTCOUNT"];
            pcsDataTable.Rows.Add(newRow);
            pcsDataTable.AcceptChanges();
            txtPcsNoScan.Text = string.Empty;
            txtCurrentSeq.Text = newRow["SEQ"].ToString();

            if(resultData.Rows[0]["PRINTLABEL"].ToString() == "Y")
            {
                Print(newRow["PPCASEID"].ToString());
            }
        }

        /// <summary>
        /// PP CASE 라벨 발행
        /// </summary>
        private void Print(string ppCaseId)
        {
            DataTable table = new DataTable();
            table.Columns.Add("PPCASEID");
            DataRow newRow = table.NewRow();
            newRow["PPCASEID"] = ppCaseId;
            table.Rows.Add(newRow);
            PrintLabel(table);
        }

        /// <summary>
        /// 재발행 여부 저장
        /// </summary>
        /// <param name="lotId"></param>
        private void SaveReprint()
        {
            MessageWorker worker = new MessageWorker("SavePpCasePrint");

            worker.SetBody(new MessageBody()
                    {
                        { "TRANSACTIONTYPE", TRANSACTIONTYPE_REPRINT },
                        { "LIST", grdPrintedPpCase.View.GetCheckedRows() }
                    });
            worker.Execute();
        }

        /// <summary>
        /// 재발행
        /// </summary>
        /// <param name="lotId"></param>
        private void ReprintPcs()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PPCASEID");
            foreach (DataRow each in grdPrintedPpCase.View.GetCheckedRows().Rows)
            {
                DataRow newRow = table.NewRow();
                newRow["PPCASEID"] = each["PPCASEID"].ToString();
                table.Rows.Add(newRow);
            }
            PrintLabel(table);
        }

        /// <summary>
        /// 라벨 발행
        /// </summary>
        /// <param name="dataTable"></param>
        private void PrintLabel(DataTable dataTable)
        {
            string zplFormat = CommonFunction.GetLabelScriptByDefId("PpCaseLabel");
            if (zplFormat != null)
            {
                StringBuilder pages = new StringBuilder();
                foreach (DataRow eachRow in dataTable.Rows)
                {
                    StringBuilder eachLabel = new StringBuilder(zplFormat);

                    foreach (DataColumn eachColumn in dataTable.Columns)
                    {
                        if (eachRow[eachColumn.ColumnName] != System.DBNull.Value)
                        {
                            eachLabel.Replace(string.Format("{{!@{0}}}", eachColumn.ColumnName), eachRow[eachColumn.ColumnName].ToString());
                        }
                    }
                    pages.Append(eachLabel.ToString());
                }
                Commons.CommonFunction.PrintLabel(pages.ToString());
            }
            else
            {
                MessageBox.Show(string.Format("등록된 스크립트가 없습니다 Label ID : {0}", "PPCase"));
            }
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
                    values.Add("P_TRANSACTIONTYPE", TRANSACTIONTYPE_PRINT);
                    dataTable = await QueryAsync("SelectLotListForPpCasePrint", "10001", values);
                    if (dataTable.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdWaitForPrint.DataSource = dataTable;
                    SearchScannedPcsOfFocusedLotAsync();
                    break;
                case 1:
                    values.Add("P_TRANSACTIONTYPE", TRANSACTIONTYPE_REPRINT);
                    dataTable = await QueryAsync("SelectLotListForPpCasePrint", "10001", values);
                    if (dataTable.Rows.Count < 1)
                    {
                        // 조회할 데이터가 없습니다.
                        ShowMessage("NoSelectData");
                    }
                    grdPrinted.DataSource = dataTable;
                    SearchPpCaseListOfFocusedLotAsync();
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
    }
}
