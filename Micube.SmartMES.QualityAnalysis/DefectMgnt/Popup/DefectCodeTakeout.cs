#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 불량관리 > 불량품 원인판정 > 불량반출 팝업
    /// 업  무  설  명  : 불량코드에 대해서 반출할 수 있는 팝업이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-07-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DefectCodeTakeout : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 불량코드 하나에 대한 반출정보
        /// </summary>
        private DataTable _defectDt;

        /// <summary>
        /// 삭제한 Row를 담고있는 정보
        /// </summary>
        private DataTable _deletedt;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public DefectCodeTakeout()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 불량코드 한줄을 보여줄 그리드
        /// </summary>
        private void InitializeDefectCode()
        {
            grdDefectCode.View.SetIsReadOnly();

            grdDefectCode.View.SetAutoFillColumn("DEFECTNAME");

            grdDefectCode.View.AddTextBoxColumn("DEFECTCODE", 80)
                .SetTextAlignment(TextAlignment.Center); // 불량코드
            grdDefectCode.View.AddTextBoxColumn("DEFECTNAME", 130); // 불량명
            grdDefectCode.View.AddTextBoxColumn("QCSEGMENTNAME", 130); // 품질공정명
            grdDefectCode.View.AddSpinEditColumn("DEFECTQTY", 80); // 불량수량
            grdDefectCode.View.AddSpinEditColumn("OUTBOUNDQTY", 80); // 총반출수량
            grdDefectCode.View.AddSpinEditColumn("LEFTQTY", 80); // 잔량
            grdDefectCode.View.AddTextBoxColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("UOM"); // UOM

            grdDefectCode.View.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden(); // Lot Id
            grdDefectCode.View.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden(); // TXN Hist key
            grdDefectCode.View.AddTextBoxColumn("QCSEGMENTID", 100)
                .SetIsHidden(); // 품질공정 ID

            grdDefectCode.View.PopulateColumns();

            grdDefectCode.View.AddNewRow();
            grdDefectCode.View.SetRowCellValue(0, "DEFECTCODE", CurrentDataRow["DEFECTCODE"]);
            grdDefectCode.View.SetRowCellValue(0, "DEFECTNAME", CurrentDataRow["DEFECTCODENAME"]);
            grdDefectCode.View.SetRowCellValue(0, "QCSEGMENTID", CurrentDataRow["QCSEGMENTID"]);
            grdDefectCode.View.SetRowCellValue(0, "QCSEGMENTNAME", CurrentDataRow["QCSEGMENTNAME"]);
            grdDefectCode.View.SetRowCellValue(0, "DEFECTQTY", CurrentDataRow["DEFECTQTY"]);
            grdDefectCode.View.SetRowCellValue(0, "OUTBOUNDQTY", CurrentDataRow["OUTBOUNDQTY"]);
            grdDefectCode.View.SetRowCellValue(0, "LEFTQTY", CurrentDataRow["LEFTQTY"]);
            grdDefectCode.View.SetRowCellValue(0, "UNIT", CurrentDataRow["UNIT"]);
            grdDefectCode.View.SetRowCellValue(0, "LOTID", CurrentDataRow["LOTID"]);
            grdDefectCode.View.SetRowCellValue(0, "TXNHISTKEY", CurrentDataRow["TXNHISTKEY"]);
        }

        /// <summary>
        /// 한 불량코드에 대해 반출할 그리드
        /// </summary>
        private void InitializeDefectCodeTakeOut()
        {
            grdDefectCodeTakeOut.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDefectCodeTakeOut.View.CheckValidation();

            grdDefectCodeTakeOut.View.SetAutoFillColumn("REASONCOMMENT");

            grdDefectCodeTakeOut.View.AddTextBoxColumn("OUTBOUNDDATE", 180)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(); // 반출일시
            grdDefectCodeTakeOut.View.AddTextBoxColumn("REASONCOMMENT", 200)
                .SetLabel("OUTBOUNDREASON"); // 반출사유
            grdDefectCodeTakeOut.View.AddSpinEditColumn("OUTBOUNDQTY", 80)
                .SetDefault(0)
                .SetValidationCustom(OutboundQtyValidation); // 반출수량
            grdDefectCodeTakeOut.View.AddTextBoxColumn("OUTBOUNDUSER", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetValidationCustom(OutboundUserValidation); // 반출자

            grdDefectCodeTakeOut.View.AddTextBoxColumn("LOTID", 100)
                .SetIsHidden();
            grdDefectCodeTakeOut.View.AddTextBoxColumn("TXNHISTKEY", 100)
                .SetIsHidden();
            grdDefectCodeTakeOut.View.AddTextBoxColumn("SEQUENCE", 100)
                .SetIsHidden();

            grdDefectCodeTakeOut.View.PopulateColumns();

            SearchOutbound();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            grdDefectCodeTakeOut.View.AddingNewRow += View_AddingNewRow;
            grdDefectCodeTakeOut.View.CellValueChanged += View_CellValueChanged;
            grdDefectCodeTakeOut.ToolbarDeleteRow += GrdDefectCodeTakeOut_ToolbarDeleteRow;
            grdDefectCodeTakeOut.ToolbarDeletingRow += GrdDefectCodeTakeOut_ToolbarDeletingRow;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// 삭제시 삭제한 Row를 변수에 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectCodeTakeOut_ToolbarDeletingRow(object sender, CancelEventArgs e)
        {
            foreach (DataRow row in grdDefectCodeTakeOut.View.GetCheckedRows().Rows)
            {
                if (string.IsNullOrEmpty(row["SEQUENCE"].ToString())) continue;
                _deletedt.ImportRow(row);
            }

            foreach (DataRow row in _deletedt.Rows)
            {
                row["OUTBOUNDDATE"] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(row["OUTBOUNDDATE"].ToString()));
            }
        }

        /// <summary>
        /// 반출 하나 삭제시 계산
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdDefectCodeTakeOut_ToolbarDeleteRow(object sender, EventArgs e)
        {
            CalcOutbound(grdDefectCodeTakeOut.View.FocusedRowHandle, "OUTBOUNDQTY");
        }

        /// <summary>
        /// 반출수량 입력시 자동계산 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            CalcOutbound(e.RowHandle, e.Column.FieldName);
        }

        /// <summary>
        /// 현재날짜 자동입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["LOTID"] = CurrentDataRow["LOTID"].ToString();
            args.NewRow["TXNHISTKEY"] = CurrentDataRow["TXNHISTKEY"].ToString();
            args.NewRow["OUTBOUNDDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //args.NewRow["OUTBOUNDUSER"] = UserInfo.Current.Id.ToString();
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable changed1 = grdDefectCode.DataSource as DataTable;
            DataTable changed2 = grdDefectCodeTakeOut.GetChangedRows();

            if (changed2.Rows.Count == 0 && _deletedt.Rows.Count == 0)
            {
                this.ShowMessage("NoSaveData");
            }
            else
            {
                foreach (DataRow row in changed2.Rows)
                {
                    if (Convert.ToInt32(row["OUTBOUNDQTY"]) <= 0)
                    {
                        // 반출수량은 0보다 커야합니다.
                        this.ShowMessage("OutboundValidation");
                        return;
                    }

                    if (string.IsNullOrEmpty(row["OUTBOUNDUSER"].ToString()))
                    {
                        // 반출자가 입력되지않은 행이 있습니다.
                        this.ShowMessage("OutboundUserValidation");
                        return;
                    }
                }

                if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    changed1.TableName = "result";

                    DataSet ds = new DataSet();
                    ds.Tables.Add(changed1.Copy());
                    ds.Tables.Add(changed2.Copy());
                    ds.Tables.Add(_deletedt.Copy());

                    this.ExecuteRule("SaveLotDefectOutbound", ds);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeDataTable();

            InitializeDefectCode();

            InitializeDefectCodeTakeOut();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 반출수량은 0이상 Validation
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> OutboundQtyValidation(int rowHandle)
        {
            var currentRow = grdDefectCodeTakeOut.View.GetDataRow(rowHandle);
            List<ValidationResult> result = new List<ValidationResult>();
            ValidationResult outBound = new ValidationResult();

            if (Convert.ToInt32(currentRow["OUTBOUNDQTY"]) == 0)
            {
                outBound.Caption = "Message";
                outBound.FailMessage = Language.GetMessage("OutboundValidation").Message;
                outBound.Id = "OUTBOUNDQTY";
                outBound.IsSucced = false;
                result.Add(outBound);
            }

            return result;
        }

        /// <summary>
        /// 반출자는 필수입력
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        private IEnumerable<ValidationResult> OutboundUserValidation(int rowHandle)
        {
            var currentRow = grdDefectCodeTakeOut.View.GetDataRow(rowHandle);
            List<ValidationResult> result = new List<ValidationResult>();
            ValidationResult outBound = new ValidationResult();

            if (string.IsNullOrEmpty(currentRow["OUTBOUNDUSER"].ToString()))
            {
                outBound.Caption = "Message";
                outBound.FailMessage = Language.GetMessage("OutboundUserValidation").Message;
                outBound.Id = "OUTBOUNDUSER";
                outBound.IsSucced = false;
                result.Add(outBound);
            }

            return result;
        }

        /// <summary>
        /// 반출수량에 따른 총 반출수량과 잔량 계산
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="columnName"></param>
        private void CalcOutbound(int rowHandle, string columnName)
        {
            int totalOutboundQty = 0;
            int leftQty = 0;

            if (columnName == "OUTBOUNDQTY")
            {
                DataTable dt = (grdDefectCodeTakeOut.DataSource as DataTable);

                // 총 반출수량 계산
                foreach (DataRow row in dt.Rows)
                {
                    totalOutboundQty += Convert.ToInt32(row["OUTBOUNDQTY"]);
                }

                // 잔량 계산
                leftQty = Convert.ToInt32(grdDefectCode.View.GetRowCellValue(0, "DEFECTQTY")) - totalOutboundQty;

                if (totalOutboundQty > Convert.ToInt32(grdDefectCode.View.GetRowCellValue(0, "DEFECTQTY")))
                {
                    this.ShowMessage("OutboundGreatThenDefectCount");
                    grdDefectCodeTakeOut.View.SetRowCellValue(rowHandle, "OUTBOUNDQTY", grdDefectCodeTakeOut.View.ActiveEditor.OldEditValue);
                }
                else
                {
                    grdDefectCode.View.SetRowCellValue(0, "OUTBOUNDQTY", totalOutboundQty);
                    grdDefectCode.View.SetRowCellValue(0, "LEFTQTY", leftQty);
                    (grdDefectCode.DataSource as DataTable).AcceptChanges();
                }
            }
        }

        /// <summary>
        /// 불량코드별 반출정보 조회
        /// </summary>
        private void SearchOutbound()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LOTID", CurrentDataRow["LOTID"].ToString());
            param.Add("TXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());

            _defectDt = SqlExecuter.Query("GetDefectCodeOutbound", "10001", param);

            foreach (DataRow row in _defectDt.Rows)
            {
                (grdDefectCodeTakeOut.DataSource as DataTable).ImportRow(row);
            }
        }

        /// <summary>
        /// 임시 DataTable 변수 컬럼만들기
        /// </summary>
        private void InitializeDataTable()
        {
            _deletedt = new DataTable();
            _deletedt.TableName = "delete";

            _deletedt.Columns.Add("REASONCOMMENT");
            _deletedt.Columns.Add("OUTBOUNDDATE");
            _deletedt.Columns.Add("OUTBOUNDQTY");
            _deletedt.Columns.Add("OUTBOUNDUSER");
            _deletedt.Columns.Add("LOTID");
            _deletedt.Columns.Add("TXNHISTKEY");
            _deletedt.Columns.Add("SEQUENCE");
        }

        #endregion
    }
}
