#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using static Micube.SmartMES.Commons.SPCLibrary.DataSets.SpcDataSet;

using DevExpress.XtraEditors.Repository;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using Micube.SmartMES.Commons.SPCLibrary;
using DevExpress.XtraLayout.Utils;

#endregion

namespace Micube.SmartMES.SPC
{
    /// <summary>
    /// 프 로 그 램 명  : SPC> 공통 Popup> 
    /// 업  무  설  명  : Raw Data 표시
    /// 생    성    자  : 이승우
    /// 생    성    일  : 2020-03-16
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SpcStatusRawDataChartPointPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// Raw Data DataTable
        /// </summary>
        public DataTable RawData = new DataTable();
        private ParPIDataTable _changeDataIMR = new ParPIDataTable();

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// subGroup Id
        /// </summary>
        public string subgroupId = string.Empty;

        /// <summary>
        /// Grid Title
        /// </summary>
        public string gridTitle = string.Empty;
        /// <summary>
        /// Chart 구분자.
        /// </summary>
        public ControlChartType chartType;
        /// <summary>
        /// IMR 자료 변환 여부.
        /// </summary>
        private bool _isIMRSummary = false;

        ///// <summary>
        ///// 불량코드 하나에 대한 반출정보
        ///// </summary>
        //private DataTable _defectDt;

        ///// <summary>
        ///// 삭제한 Row를 담고있는 정보
        ///// </summary>
        //private DataTable _deletedt;
        #endregion

        #region
        private string _messageIMRSummary01 = "";
        private string _messageIMRSummary02 = "";
        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public SpcStatusRawDataChartPointPopup()
        {
            InitializeComponent();
            InitializeEvent();
            InitializeGrid();

            this.CancelButton = btnClose;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Chart Raw Data 
        /// </summary>
        private void InitializeGrid()
        {
            grdRawData.GridButtonItem = GridButtonItem.Export;
            // Group ID;
            grdRawData.View.AddTextBoxColumn("GROUPID", 100)
                .SetLabel("SPCGROUPID")
                .SetIsHidden();
            grdRawData.View.AddTextBoxColumn("SAMPLEID", 100)
                .SetLabel("SPCSAMPLEID")
                .SetIsHidden(); 
            grdRawData.View.AddTextBoxColumn("SUBGROUPNAME", 150)
                .SetLabel("SUBGROUPNAMEVIEW");
            grdRawData.View.AddTextBoxColumn("SAMPLINGNAME", 200)
                .SetLabel("SAMPLINGXAXIS");
            grdRawData.View.AddTextBoxColumn("LOTID", 200);
            grdRawData.View.AddSpinEditColumn("NSUBVALUE", 130)
                .SetLabel("SPCNSUBVALUE")
                .SetDisplayFormat("###0.########################################", MaskTypes.Numeric)
                .SetIsReadOnly() // 검사전체 수
                .SetIsHidden();
            grdRawData.View.AddSpinEditColumn("NVALUE", 130)
                //.SetDisplayFormat("0.000", MaskTypes.Numeric)
                .SetDisplayFormat("###0.########################################", MaskTypes.Numeric)
                .SetLabel("SPCNVALUE")
                .SetIsReadOnly(); // 값, 불량 건수

            grdRawData.View.PopulateColumns();
        }

        public void GridHeddenPCU()
        {
            Application.DoEvents();
            this.grdRawData.View.Columns["GROUPID"].OwnerBand.Visible = false;
            this.grdRawData.View.Columns["SAMPLEID"].OwnerBand.Visible = false;
            this.grdRawData.View.Columns["SUBGROUP"].OwnerBand.Visible = false;
            this.grdRawData.View.Columns["NSUBVALUE"].Visible = true;
            this.grdRawData.View.Columns["NSUBVALUE"].OwnerBand.Visible = true;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += SpcStatusRawDataPopup_Load;

            //grdRawData.View.AddingNewRow += View_AddingNewRow;
            //grdRawData.View.CellValueChanged += View_CellValueChanged;
            //grdRawData.ToolbarDeleteRow += GrdDefectCodeTakeOut_ToolbarDeleteRow;
            //grdRawData.ToolbarDeletingRow += GrdDefectCodeTakeOut_ToolbarDeletingRow;
            btnClose.Click += BtnClose_Click;
            //btnSave.Click += BtnSave_Click;
            //btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpcStatusRawDataPopup_Load(object sender, EventArgs e)
        {
            SpcClass.SpcDictionaryDataSetting();
            this.MessageRead();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.grdRawData.DataSource = RawData;
            this.grdRawData.View.BestFitColumns();
            this.grdRawData.ShowStatusBar = true;
            this.grdRawData.Caption = gridTitle;
            this.SummaryButtonCheck();
            this.btnDataSummary.Height = this.btnClose.Height;
        }

        /// <summary>
        /// IMR 분석자료 변환 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataSummary_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDataSummary.Enabled = false;
                if (_isIMRSummary)
                {
                    this.grdRawData.DataSource = RawData;
                    this.btnDataSummary.Text = _messageIMRSummary02;
                    _isIMRSummary = false;
                }
                else
                {
                    this.DataSummaryIMR();
                    this.btnDataSummary.Text = _messageIMRSummary01;
                    _isIMRSummary = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.btnDataSummary.Enabled = true;
        }

        ///// <summary>
        ///// 삭제시 삭제한 Row를 변수에 저장
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void GrdDefectCodeTakeOut_ToolbarDeletingRow(object sender, CancelEventArgs e)
        //{
        //    foreach (DataRow row in grdRawData.View.GetCheckedRows().Rows)
        //    {
        //        if (string.IsNullOrEmpty(row["SEQUENCE"].ToString())) continue;
        //        _deletedt.ImportRow(row);
        //    }

        //    foreach (DataRow row in _deletedt.Rows)
        //    {
        //        row["OUTBOUNDDATE"] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(row["OUTBOUNDDATE"].ToString()));
        //    }
        //}

        ///// <summary>
        ///// 반출 하나 삭제시 계산
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void GrdDefectCodeTakeOut_ToolbarDeleteRow(object sender, EventArgs e)
        //{
        //    CalcOutbound(grdRawData.View.FocusedRowHandle, "OUTBOUNDQTY");
        //}

        ///// <summary>
        ///// 반출수량 입력시 자동계산 바인딩
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    CalcOutbound(e.RowHandle, e.Column.FieldName);
        //}

        ///// <summary>
        ///// 현재날짜 자동입력
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        //{
        //    args.NewRow["LOTID"] = CurrentDataRow["LOTID"].ToString();
        //    args.NewRow["TXNHISTKEY"] = CurrentDataRow["TXNHISTKEY"].ToString();
        //    args.NewRow["OUTBOUNDDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    args.NewRow["OUTBOUNDUSER"] = UserInfo.Current.Id.ToString();
        //}

        ///// <summary>
        ///// 저장
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BtnSave_Click(object sender, EventArgs e)
        //{
        //    DataTable changed1 = grdRawData.DataSource as DataTable;
        //    DataTable changed2 = grdRawData.GetChangedRows();

        //    if (changed2.Rows.Count == 0 && _deletedt.Rows.Count == 0)
        //    {
        //        this.ShowMessage("NoSaveData");
        //    }
        //    else
        //    {
        //        foreach (DataRow row in changed2.Rows)
        //        {
        //            if (Convert.ToInt32(row["OUTBOUNDQTY"]) <= 0)
        //            {
        //                this.ShowMessage("OutboundValidation");
        //                return;
        //            }
        //        }

        //        if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            changed1.TableName = "result";

        //            DataSet ds = new DataSet();
        //            ds.Tables.Add(changed1.Copy());
        //            ds.Tables.Add(changed2.Copy());
        //            ds.Tables.Add(_deletedt.Copy());

        //            this.ExecuteRule("SaveLotDefectOutbound", ds);
        //            this.DialogResult = DialogResult.OK;
        //            this.Close();
        //        }
        //    }
        //}

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

        #endregion

        #region Private Function
        /// <summary>
        /// Meessage 조회.
        /// </summary>
        private void MessageRead()
        {
            //SpcClass.SpcDictionaryDataSetting();
            bool isCheck = SpcDictionary.CaptionDtCheck(SpcDicClassId.CONTROLLABEL);
            if (isCheck)
            {   
                _messageIMRSummary01 = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRAWIMRSUMMARYTRUE");//원본 자료 전환
                _messageIMRSummary02 = SpcDictionary.readControl(SpcDicClassId.CONTROLLABEL, "SPCRAWIMRSUMMARYFALSE");//I-MR 자료로 변환
            }
            if (_messageIMRSummary01 == "") _messageIMRSummary01 = "Test-원본자료 전환";
            if (_messageIMRSummary02 == "") _messageIMRSummary02 = "Test-I-MR 자료로 변환";
            this.btnDataSummary.Text = _messageIMRSummary02;
        }
        /// <summary>
        /// 자료 합산형으로 변화.
        /// </summary>
        private void SummaryButtonCheck()
        {
            switch (chartType)
            {
                case ControlChartType.I_MR:
                    //btnDataSummary.Visible = true;
                    this.layCtrItemDataSummary.Visibility = LayoutVisibility.Always;
                    break;
                case ControlChartType.XBar_R:
                case ControlChartType.XBar_S:
                case ControlChartType.Merger:
                case ControlChartType.np:
                case ControlChartType.p:
                case ControlChartType.c:
                case ControlChartType.u:
                default:
                    //btnDataSummary.Visible = false;
                    this.layCtrItemDataSummary.Visibility = LayoutVisibility.Never;
                    break;
            }
        }
        /// <summary>
        /// IMR 분석자료 변환
        /// </summary>
        private void DataSummaryIMR()
        {
            try
            {
                _changeDataIMR.Rows.Clear();
                double checkData;
                for (int i = 0; i < RawData.Rows.Count; i++)
                {
                    DataRow dataRow = RawData.Rows[i];
                    checkData = SpcFunction.IsDbNckDoubleMin(dataRow, "NSUBVALUE");
                    if (checkData == SpcLimit.MIN)
                    {
                        dataRow["NSUBVALUE"] = 0;
                    }
                }
                
                var sumSubGroup = RawData.AsEnumerable()
                                  .GroupBy(b => new
                                  {
                                      GROUPID = b.Field<long>("GROUPID"),
                                      SUBGROUP = b.Field<string>("SUBGROUP"),
                                      SAMPLEID = b.Field<long>("SAMPLEID"),
                                      SAMPLING = b.Field<string>("SAMPLING")
                                  })
                                  .Select (grp => new
                                  {
                                      sSUBGROUP = grp.Key.SUBGROUP.ToSafeString(),
                                      sEQUIPMENTID = grp.Where(x => x.Field<object>("LOTID") != null).Max(x => x.Field<object>("LOTID").ToSafeString())
                                  });
                foreach (var s in sumSubGroup)
                {
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTID", s.sEQUIPMENTID.ToSafeString());
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "EQUIPMENTNAME", s.sEQUIPMENTNAME.ToSafeString());
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHILDEQUIPMENTID", s.sCHILDEQUIPMENTID.ToSafeString());
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHILDEQUIPMENTNAME", s.sCHILDEQUIPMENTNAME.ToSafeString());
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTCLASSID", s.sPROCESSSEGMENTCLASSID.ToSafeString());
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "PROCESSSEGMENTCLASSNAME", s.sPROCESSSEGMENTCLASSNAME.ToSafeString());
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "INSPITEMID", s.sINSPITEMID.ToSafeString());
                    //SpcFunction.IsDbNckStringWrite(dataRowOverRule, "CHEMICALNAME", s.sCHEMICALNAME.ToSafeString());
                }

                //foreach (var f in sumSubGroup)
                //{
                //    DataRow dataRow = _changeDataIMR.NewRow();
                //    dataRow["GROUPID"] = f.vGROUPID;
                //    dataRow["SAMPLEID"] = f.vSAMPLEID;
                //    dataRow["SUBGROUP"] = f.vSUBGROUP;
                //    dataRow["SAMPLING"] = f.vSAMPLING;
                //    dataRow["NSUBVALUE"] = f.vSUBVALUEAVG;
                //    dataRow["NVALUE"] = f.vNVALUEAVG;
                //    //dataRow["SUBGROUPNAME"] = f.vSUBGROUPNAME;
                //    //dataRow["SAMPLINGNAME"] = f.vSAMPLINGNAME;
                //    _changeDataIMR.Rows.Add(dataRow);
                //}

                if (_changeDataIMR != null && _changeDataIMR.Rows.Count > 0)
                {
                    this.grdRawData.DataSource = _changeDataIMR;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        ///// <summary>
        ///// 반출수량은 0이상 Validation
        ///// </summary>
        ///// <param name="rowHandle"></param>
        ///// <returns></returns>
        //private IEnumerable<ValidationResult> OutboundQtyValidation(int rowHandle)
        //{
        //    var currentRow = grdRawData.View.GetDataRow(rowHandle);
        //    List<ValidationResult> result = new List<ValidationResult>();
        //    ValidationResult outBound = new ValidationResult();

        //    if (Convert.ToInt32(currentRow["OUTBOUNDQTY"]) == 0)
        //    {
        //        outBound.Caption = "Message";
        //        outBound.FailMessage = Language.GetMessage("OutboundValidation").Message;
        //        outBound.Id = "OUTBOUNDQTY";
        //        outBound.IsSucced = false;
        //        result.Add(outBound);
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 반출수량에 따른 총 반출수량과 잔량 계산
        ///// </summary>
        ///// <param name="rowHandle"></param>
        ///// <param name="columnName"></param>
        //private void CalcOutbound(int rowHandle, string columnName)
        //{
        //    int totalOutboundQty = 0;
        //    int leftQty = 0;

        //    if (columnName == "OUTBOUNDQTY")
        //    {
        //        DataTable dt = (grdRawData.DataSource as DataTable);

        //        // 총 반출수량 계산
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            totalOutboundQty += Convert.ToInt32(row["OUTBOUNDQTY"]);
        //        }

        //        // 잔량 계산
        //        leftQty = Convert.ToInt32(grdRawData.View.GetRowCellValue(0, "DEFECTQTY")) - totalOutboundQty;

        //        if (totalOutboundQty > Convert.ToInt32(grdRawData.View.GetRowCellValue(0, "DEFECTQTY")))
        //        {
        //            this.ShowMessage("OutboundGreatThenDefectCount");
        //            grdRawData.View.SetRowCellValue(rowHandle, "OUTBOUNDQTY", grdRawData.View.ActiveEditor.OldEditValue);
        //        }
        //        else
        //        {
        //            grdRawData.View.SetRowCellValue(0, "OUTBOUNDQTY", totalOutboundQty);
        //            grdRawData.View.SetRowCellValue(0, "LEFTQTY", leftQty);
        //            (grdRawData.DataSource as DataTable).AcceptChanges();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 불량코드별 반출정보 조회
        ///// </summary>
        //private void SearchOutbound()
        //{
        //    Dictionary<string, object> param = new Dictionary<string, object>();
        //    param.Add("LOTID", CurrentDataRow["LOTID"].ToString());
        //    param.Add("TXNHISTKEY", CurrentDataRow["TXNHISTKEY"].ToString());

        //    _defectDt = SqlExecuter.Query("GetDefectCodeOutbound", "10001", param);

        //    foreach (DataRow row in _defectDt.Rows)
        //    {
        //        (grdRawData.DataSource as DataTable).ImportRow(row);
        //    }
        //}

        ///// <summary>
        ///// 임시 DataTable 변수 컬럼만들기
        ///// </summary>
        //private void InitializeDataTable()
        //{
        //    _deletedt = new DataTable
        //    {
        //        TableName = "delete"
        //    };

        //    _deletedt.Columns.Add("REASONCOMMENT");
        //    _deletedt.Columns.Add("OUTBOUNDDATE");
        //    _deletedt.Columns.Add("OUTBOUNDQTY");
        //    _deletedt.Columns.Add("OUTBOUNDUSER");
        //    _deletedt.Columns.Add("LOTID");
        //    _deletedt.Columns.Add("TXNHISTKEY");
        //    _deletedt.Columns.Add("SEQUENCE");
        //}

        #endregion


    }
}
