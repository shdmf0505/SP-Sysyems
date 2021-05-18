#region using

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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
    /// 프 로 그 램 명  : 품질관리 > Audit 관리 > Audit 관리대장
    /// 업  무  설  명  : 협력업체 관리대장 현황 을 조회하여 점검결과를 등록한다.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-08-13
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class AuditManageRegistQuarterPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 그리드에 보여줄 데이터테이블
        /// </summary>
        private DataTable _AuditManageDt;
        private DataTable _AuditHeaderDt;

        string _sENTERPRISEID = string.Empty;
        string _sPLANTID = string.Empty;
        string _sVENDORID = string.Empty;
        string _sAREAID = string.Empty;
        //string _sPROCESSSEGMENTID = string.Empty;
        //string _sPROCESSSEGMENTVERSION = string.Empty;
        string _sYEAR = string.Empty;
        string _sQUARTER = string.Empty;
        string _sVENDORNAME = string.Empty;
        string _sAREANAME = string.Empty;
        //DataTable _dtNewReport;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public AuditManageRegistQuarterPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        public AuditManageRegistQuarterPopup(string sENTERPRISEID, string sPLANTID, string sVENDORID, string sAREAID, string sYEAR, string sQuarter, string sVENDORNAME, string sAREANAME)
        {
            InitializeComponent();
            InitializeEvent();

            _sENTERPRISEID = sENTERPRISEID;
            _sPLANTID = sPLANTID;
            _sVENDORID = sVENDORID;
            _sAREAID = sAREAID;
            //_sPROCESSSEGMENTID = sPROCESSSEGMENTID;
            //_sPROCESSSEGMENTVERSION = sPROCESSSEGMENTVERSION;
            _sYEAR = sYEAR;
            _sQUARTER = sQuarter;
            _sVENDORNAME = sVENDORNAME;
            _sAREANAME = sAREANAME;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdAuditManageregist.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore | GridButtonItem.Add | GridButtonItem.Delete;
            //grdAuditManageregist.View.SetIsReadOnly();
            grdAuditManageregist.View.AddTextBoxColumn("VENDORNAME", 160).SetLabel("VENDORNAME").SetIsReadOnly();
            grdAuditManageregist.View.AddTextBoxColumn("AREANAME", 100).SetLabel("AREANAME").SetIsReadOnly();
            grdAuditManageregist.View.AddTextBoxColumn("DEGREE", 80).SetLabel("DEGREE").SetIsReadOnly().SetTextAlignment(TextAlignment.Center); // 자동증가/신규등록시
            grdAuditManageregist.View.AddDateEditColumn("INSPECTIONDATE", 100).SetLabel("AUDITDATE").SetValidationIsRequired().SetTextAlignment(TextAlignment.Center); // 수동입력/일자
            grdAuditManageregist.View.AddSpinEditColumn("INSPECTIONRESULT", 100).SetLabel("EXAMINRESULT").SetValidationIsRequired(); // 수동입력/숫자만

            if (_sPLANTID == "YPE" || _sPLANTID == "YPEV")
            {
                grdAuditManageregist.View.AddTextBoxColumn("QUALITYWARNINGLETTER", 100).SetTextAlignment(TextAlignment.Left); // 수동입력
                grdAuditManageregist.View.AddTextBoxColumn("THINKINGPERSONALITY", 100).SetTextAlignment(TextAlignment.Left); // 수동입력
                grdAuditManageregist.View.AddTextBoxColumn("NCRDESCRIPTION", 100).SetTextAlignment(TextAlignment.Left); // 수동입력
                grdAuditManageregist.View.AddTextBoxColumn("REENACTIVATION", 100).SetTextAlignment(TextAlignment.Left); // 수동입력
                grdAuditManageregist.View.AddTextBoxColumn("GENERALJUDGMENT", 100).SetTextAlignment(TextAlignment.Left); // 수동입력
            }
            grdAuditManageregist.View.AddSpinEditColumn("MONTHLYSALESAMOUNT", 100); // 수동입력
            grdAuditManageregist.View.AddComboBoxColumn("ISOPRATIONSTOP", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// 공통코드(YesNo)


            grdAuditManageregist.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("VENDORID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("AREAID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("PROCESSSEGMENTID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200).SetIsHidden();

            //YesNo
            //     var auditTypeCombo = Conditions.AddComboBox("p_auditType", new SqlQuery("GetCodeList", "00001", "CODECLASSID=AuditType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //auditTypeCombo.SetLabel("CLASS");

            //grdAuditManageregist.View.AddTextBoxColumn("ISOPRATIONSTOP", 150).SetLabel("ISOPRATIONSTOP"); // 공통코드(YesNo)

            grdAuditManageregist.View.PopulateColumns();

            RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date.Mask.EditMask = "yyyy-MM-dd";
            date.Mask.UseMaskAsDisplayFormat = true;

            grdAuditManageregist.View.Columns["INSPECTIONDATE"].ColumnEdit = date;

        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;
            grdAuditManageregist.View.CellValueChanged += View_CellValueChanged;
            grdAuditManageregist.View.ShowingEditor += View_ShowingEditor;
            //grdAuditManageregist.View.RowCellClick += View_RowCellClick;
            grdAuditManageregist.View.FocusedRowChanged += View_FocusedRowChanged;
            grdAuditManageregist.View.AddingNewRow += View_AddingNewRow;
            grdAuditManageregist.ToolbarAddingRow += View_ToolbarAddingRow;
            this.txtRemark.EditValueChanged += new System.EventHandler(this.txtRemark_EditValueChanged);

            grdAuditManageregist.HeaderButtonClickEvent += GrdAuditManageregist_HeaderButtonClickEvent;

            btnClose.Click += BtnClose_Click;
            //팝업저장버튼을 클릭시 이벤트
            btnSave.Click += BtnSave_Click;      

            //dtpInspectionPlanMonth.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            //dtpInspectionPlanMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            //dtpInspectionPlanMonth.Properties.DisplayFormat.FormatString = "MM";
            //dtpInspectionPlanMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dtpInspectionPlanMonth.Properties.EditFormat.FormatString = "MM";
            //dtpInspectionPlanMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

        }
        #region 그리드이벤트
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow row = grdAuditManageregist.View.GetFocusedDataRow();
            GridView view = sender as GridView;
            string sUser = UserInfo.Current.Id;
            if (!view.FocusedColumn.FieldName.Equals("INSPECTIONDATE"))
            {
                return;
            }
            string sDEGREE = row["DEGREE"].ToString();
            string sINSPECTIONDATE = row["INSPECTIONDATE"].ToString();
            if (sDEGREE.Length > 0)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 그리드 해더 확장, 축소 버튼 에빈트
        /// </summary>
        private void GrdAuditManageregist_HeaderButtonClickEvent(object sender, Framework.SmartControls.HeaderButtonClickArgs args)
        {
            args.Button.Visible = true;
            if (args.ClickItem == GridButtonItem.Add)
            {
                //args.Button.Enabled = false;
                //GridButtonItem buttonItem = grdAuditManageregist.GridButtonItem;

                //DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton groupBoxButton = args.Button;

            }
            if (args.ClickItem == GridButtonItem.Expand) // 확장
            {
                grdAuditManageregist.Parent = this;
                grdAuditManageregist.BringToFront();
            }
            else if (args.ClickItem == GridButtonItem.Restore) // 축소
            {
                grdAuditManageregist.Parent = smartSplitTableLayoutPanel2;
                grdAuditManageregist.BringToFront();
            }
        }
        private void View_ToolbarAddingRow(object sender, CancelEventArgs e)
        {
            SmartBandedGrid grid = sender as SmartBandedGrid;
            if (grid == null) return;

            DataTable dt = grid.DataSource as DataTable;
            if (dt.Rows.Cast<DataRow>().Where(r => r.RowState == DataRowState.Added).ToList().Count > 0)
            {
                e.Cancel = true;
            }
            
            //추가 한 후 저장하지 않은 데이터가 있을 때
            //var rowState = (grid.DataSource as DataTable).Rows.Cast<DataRow>().Select(r => r.RowState.ToString()).ToList();
            //if (rowState.Contains("Added"))
            //{
            //    DialogResult result = System.Windows.Forms.DialogResult.No;

            //    result = this.ShowMessage(MessageBoxButtons.YesNo, "DataIsNotSaved");//새로운 항목을 추가 할 경우 입력한 정보는 저장 되지 않습니다. 계속 하시겠습니까?

            //    if (result == System.Windows.Forms.DialogResult.No)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            //DataTable dt = grdAuditManageregist.DataSource as DataTable;
            //DataRow[] drs = dt.Select("DEGREE is null ");
            //if (drs.Length > 1)
            //{
            //    args.IsCancel = true;
            //    return;
            //}

            if (!args.NewRow.Table.Columns.Contains("YEAR")) args.NewRow.Table.Columns.Add("YEAR");
            if (!args.NewRow.Table.Columns.Contains("QUARTER")) args.NewRow.Table.Columns.Add("QUARTER");
            if (!args.NewRow.Table.Columns.Contains("ENTERPRISEID")) args.NewRow.Table.Columns.Add("ENTERPRISEID");
            if (!args.NewRow.Table.Columns.Contains("PLANTID")) args.NewRow.Table.Columns.Add("PLANTID");
            if (!args.NewRow.Table.Columns.Contains("VENDORID")) args.NewRow.Table.Columns.Add("VENDORID");
            if (!args.NewRow.Table.Columns.Contains("AREAID")) args.NewRow.Table.Columns.Add("AREAID");
            //if (!args.NewRow.Table.Columns.Contains("PROCESSSEGMENTID")) args.NewRow.Table.Columns.Add("PROCESSSEGMENTID");
            //if (!args.NewRow.Table.Columns.Contains("PROCESSSEGMENTVERSION")) args.NewRow.Table.Columns.Add("PROCESSSEGMENTVERSION");
            if (!args.NewRow.Table.Columns.Contains("VENDORNAME")) args.NewRow.Table.Columns.Add("VENDORNAME");
            if (!args.NewRow.Table.Columns.Contains("AREANAME")) args.NewRow.Table.Columns.Add("AREANAME");

            args.NewRow["YEAR"] = _sYEAR;
            args.NewRow["QUARTER"] = _sQUARTER;
            args.NewRow["ENTERPRISEID"] = _sENTERPRISEID;
            args.NewRow["PLANTID"] = _sPLANTID;
            args.NewRow["VENDORID"] = _sVENDORID;
            args.NewRow["AREAID"] = _sAREAID;
            //args.NewRow["PROCESSSEGMENTID"] = _sPROCESSSEGMENTID;
            //args.NewRow["PROCESSSEGMENTVERSION"] = _sPROCESSSEGMENTVERSION;
            args.NewRow["VENDORNAME"] = _sVENDORNAME;
            args.NewRow["AREANAME"] = _sAREANAME;

            args.NewRow["ISOPRATIONSTOP"] = "N";
            //args.NewRow["INSPECTIONDATE"] = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// 차수에 등록된 파일조회
        /// 차수가 없는 신규일 경우 Temp에 있는 파일 정보를 바인딩 해준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = grdAuditManageregist.View.GetDataRow(grdAuditManageregist.View.FocusedRowHandle);
            if (row == null) return;
            if (txtRemark.Text != row["DESCRIPTION"].ToString())
                txtRemark.Text = row["DESCRIPTION"].ToString();

            string sDEGREE = row["DEGREE"].ToString();
            Dictionary<string, object> fileValues = new Dictionary<string, object>();
            //이미지 파일Search parameter
            fileValues.Add("P_FILERESOURCETYPE", "AUDITMANAGEREGISTQUARTER");
            //fileValues.Add("P_FILERESOURCEID", _sYEAR + _sQUARTER + _sENTERPRISEID + _sPLANTID + _sVENDORID + _sAREAID + _sPROCESSSEGMENTID + "QUARTER" + sDEGREE);
            fileValues.Add("P_FILERESOURCEID", _sYEAR + _sQUARTER + _sENTERPRISEID + _sPLANTID + _sVENDORID + _sAREAID + "QUARTER" + sDEGREE);
            fileValues.Add("P_FILERESOURCEVERSION", "0");

            DataTable fileTable = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);

            fpcReport.DataSource = fileTable;

            (fpcReport.DataSource as DataTable).RowChanged -= new DataRowChangeEventHandler(OnRowChanged);
            (fpcReport.DataSource as DataTable).RowChanged += new DataRowChangeEventHandler(OnRowChanged);

            //if (row["DEGREE"].ToString() == string.Empty && _dtNewReport != null)
            //{
            //    fpcReport.DataSource = _dtNewReport;
            //}
          
        }

        /// <summary>
        /// fpcReport
        /// 저장할 파일의 Key생성에 사용되는 컬럼목록 추가 해줌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OnRowChanged(object sender, DataRowChangeEventArgs args)
        {
            if (args.Action == DataRowAction.Add)
            {
                if (!args.Row.Table.Columns.Contains("YEAR")) args.Row.Table.Columns.Add("YEAR");
                if (!args.Row.Table.Columns.Contains("QUARTER")) args.Row.Table.Columns.Add("QUARTER");
                if (!args.Row.Table.Columns.Contains("ENTERPRISEID")) args.Row.Table.Columns.Add("ENTERPRISEID");
                if (!args.Row.Table.Columns.Contains("PLANTID")) args.Row.Table.Columns.Add("PLANTID");
                if (!args.Row.Table.Columns.Contains("VENDORID")) args.Row.Table.Columns.Add("VENDORID");
                if (!args.Row.Table.Columns.Contains("AREAID")) args.Row.Table.Columns.Add("AREAID");
                //if (!args.Row.Table.Columns.Contains("PROCESSSEGMENTID")) args.Row.Table.Columns.Add("PROCESSSEGMENTID");
                //if (!args.Row.Table.Columns.Contains("PROCESSSEGMENTVERSION")) args.Row.Table.Columns.Add("PROCESSSEGMENTVERSION");
                if (!args.Row.Table.Columns.Contains("DEGREE")) args.Row.Table.Columns.Add("DEGREE");

                DataRow dr = args.Row;
                dr["YEAR"] = _sYEAR;
                dr["QUARTER"] = _sQUARTER;
                dr["ENTERPRISEID"] = _sENTERPRISEID;
                dr["PLANTID"] = _sPLANTID;
                dr["VENDORID"] = _sVENDORID;
                dr["AREAID"] = _sAREAID;
                //dr["PROCESSSEGMENTID"] = _sPROCESSSEGMENTID;
                //dr["PROCESSSEGMENTVERSION"] = _sPROCESSSEGMENTVERSION;
                DataRow rowFocused = grdAuditManageregist.View.GetFocusedDataRow();
                dr["DEGREE"] = rowFocused["DEGREE"].ToString();

                //if (_dtNewReport == null || _dtNewReport.Columns.Count == 0)
                //    _dtNewReport = args.Row.Table.Clone();

                //if (rowFocused["DEGREE"].ToString() == string.Empty)
                //    _dtNewReport.ImportRow(dr);
            }
        }
        /// <summary>
        /// 제작년월을 yyyy-MM형식으로 지정한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = grdAuditManageregist.View.GetFocusedDataRow();
            DataTable dtAudit = grdAuditManageregist.DataSource as DataTable;
            if (e.Column.FieldName == "INSPECTIONDATE")
            {
                string sDEGREE = row["DEGREE"].ToString();
                DateTime de = new DateTime();
                //DateTime.TryParse(e.Value.ToString(), out de);

                string s_date = e.Value.ToString().Substring(0,10);

                de = DateTime.ParseExact(s_date, "yyyy-MM-dd", null);

                if (dtAudit.Rows.Count == 1)//이전 이후 차수 비교할 필요가 없을떄.
                {
                    if (DateTime.Compare(de, DateTime.Now) > 0)
                    {
                        ShowMessage("InValidInspectionDate");//점검일자는 오늘 이전 날짜입니다.
                        row["INSPECTIONDATE"] = string.Empty;
                        return;
                    }
                    else
                    {                        
                        row["INSPECTIONDATE"] = de.ToShortDateString().Substring(0,10);
                    }
                        
                }
                else
                {
                    if (sDEGREE == string.Empty)
                    {
                        sDEGREE = "99999";//신규일때는 최대로 설정
                    }
                    int iDEGREE = 0;
                    Int32.TryParse(sDEGREE, out iDEGREE);

                    var listMin = dtAudit.AsEnumerable()
                        .Select(r => {
                            int seq = 0;
                            string sOrgDEGREE = r["DEGREE"].ToString();
                            Int32.TryParse(r["DEGREE"].ToString(), out seq);
                            return new { sOrgDEGREE, seq };
                        })
                        .Where(c => c.seq < iDEGREE)
                        .ToList();

                    var listMax = dtAudit.AsEnumerable()
                        .Select(r => {
                            int seq = 0;
                            string sOrgDEGREE = r["DEGREE"].ToString();
                            Int32.TryParse(r["DEGREE"].ToString(), out seq);
                            return new { sOrgDEGREE, seq };
                        })
                        .Where(c => c.seq > iDEGREE)
                        .ToList();

                    if (listMin.Count > 0)
                    {
                        string maxValue = listMin.Max(r => r.sOrgDEGREE).ToString();
                        var listDegreeMax = dtAudit.AsEnumerable().ToList().Where(r => r["DEGREE"].ToString() == maxValue).FirstOrDefault();
                        string sInspectionDate = listDegreeMax == null ? string.Empty : listDegreeMax["INSPECTIONDATE"].ToString();

                        DateTime deMaxDate = new DateTime();
                        if (DateTime.TryParse(sInspectionDate, out deMaxDate))
                        {
                            if (DateTime.Compare(deMaxDate, de) >= 0)//이전차수와 비교 커야 한다
                            {
                                ShowMessage("InValidInspectionPreDate");//이전 차수보다 이후 날짜이어야 합니다
                                row["INSPECTIONDATE"] = string.Empty;
                                return;
                            }
                        }
                    }
                    if (listMax.Count > 0)
                    {
                        string minValue = listMax.Min(r => r.sOrgDEGREE).ToString();
                        var listDegreeMin = dtAudit.AsEnumerable().ToList().Where(r => r["DEGREE"].ToString() == minValue).FirstOrDefault();
                        string sInspectionDate = listDegreeMin == null ? string.Empty : listDegreeMin["INSPECTIONDATE"].ToString();

                        DateTime deMinDate = new DateTime();
                        if (DateTime.TryParse(sInspectionDate, out deMinDate))
                        {
                            if (DateTime.Compare(deMinDate, de) <= 0)//이후차수와 비교 작아야 한다
                            {
                                ShowMessage("InValidInspectionNextDate");//이후 차수보다 이전 날짜이어야 합니다
                                row["INSPECTIONDATE"] = string.Empty;
                                return;
                            }
                        }
                    }
                }
                //_sQUARTER
                int iMonthQuarter = 1;
                switch (_sQUARTER)
                {
                    case "1":
                        iMonthQuarter = 1;
                        break;
                    case "2":
                        iMonthQuarter = 4;
                        break;
                    case "3":
                        iMonthQuarter = 7;
                        break;
                    case "4":
                        iMonthQuarter = 10;
                        break;
                }
                //팝업의 분기
                DateTime datePopup = new DateTime(Convert.ToInt32(_sYEAR), iMonthQuarter, 01);
                DateTime dateFirstDatePopup = GetFirstDateOfQuarter(datePopup);
                DateTime dateLastDatePopup = GetLastDateOfQuarter(datePopup);

                if (DateTime.Compare(dateFirstDatePopup, de) > 0 || DateTime.Compare(de, dateLastDatePopup) > 0)
                {
                    ShowMessage("InValidDateOfQuarter");//선택된 분기와 날짜가 적합하지 않습니다.
                    row["INSPECTIONDATE"] = string.Empty;
                    return;
                }
                else if (DateTime.Compare(de, DateTime.Now) > 0)
                {
                    ShowMessage("InValidInspectionDate");//점검일자는 오늘 이전 날짜입니다.
                    row["INSPECTIONDATE"] = string.Empty;
                    return;
                }
                else
                {
                    row["INSPECTIONDATE"] = de.ToShortDateString().Substring(0, 10); ;
                }
            }
        }
        #region 분기 첫번째 일자 구하기 - GetFirstDateOfQuarter(sourceDate)
        /// <summary>
        /// 분기 첫번째 일자 구하기
        /// </summary>
        /// <param name="sourceDate">소스 일자</param>
        /// <returns>분기 첫번째 일자</returns>
        public DateTime GetFirstDateOfQuarter(DateTime sourceDate)
        {
            int sourceMonth = sourceDate.Month;
            DateTime targetDate;
            DateTime temporaryDate;

            if (sourceMonth != 0)
            {
                int monthCount = (sourceMonth % 3) - 1;
                temporaryDate = sourceDate.AddMonths(-monthCount);
                targetDate = new DateTime(temporaryDate.Year, temporaryDate.Month, 1);
            }
            else
            {
                temporaryDate = sourceDate.AddMonths(-2);
                targetDate = new DateTime(temporaryDate.Year, temporaryDate.Month, 1);
            }

            return targetDate;
        }
        #endregion
        #region 분기 마지막 일자 구하기 - GetLastDateOfQuarter(sourceDate)
        /// <summary>
        /// 분기 마지막 일자 구하기
        /// </summary>
        /// <param name="sourceDate">소스 일자</param>
        /// <returns>분기 마지막 일자</returns>
        public DateTime GetLastDateOfQuarter(DateTime sourceDate)
        {
            int sourceMonth = sourceDate.Month;
            DateTime targetDate;
            DateTime temporaryDate;

            if (sourceMonth != 0)
            {
                int monthCount = sourceMonth + (3 - (sourceMonth % 3));
                temporaryDate = new DateTime(sourceDate.Year, monthCount, 1);
                temporaryDate.AddMonths(1);
                temporaryDate.AddDays(-1.0);
                targetDate = temporaryDate.AddMonths(1).AddDays(-1.0);
            }
            else
            {
                temporaryDate = new DateTime(sourceDate.Year, sourceDate.Month, 1);
                temporaryDate.AddMonths(1);
                temporaryDate.AddDays(-1.0);
                targetDate = temporaryDate.AddMonths(1).AddDays(-1.0);
            }
            return targetDate;
        }
        #endregion

        #endregion
        /// <summary>
        /// 저장버튼을 클릭했을때 검사 결과를 저장하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.No;
            DataTable dt = grdAuditManageregist.DataSource as DataTable;
           
            //if (grdAuditManageregist.GetChangedRows().Rows.Count == 0 && fpcReport.GetChangedRows().Rows.Count == 0) return;
            if (cboInspectionPlanMonth.Text == string.Empty || cboInspectionPlanWeek.Text == string.Empty)
            {
                ShowMessage("ValidAuditPlanDate");//정기심사계왹일을 선택해주세요.
                return;
            }
            if (_AuditHeaderDt != null && _AuditHeaderDt.Rows.Count > 0)
            {
                _AuditHeaderDt.AcceptChanges();
                if (_AuditHeaderDt.Rows[0]["INSPECTIONPLANMONTH"].ToString() != cboInspectionPlanMonth.Text)
                    _AuditHeaderDt.Rows[0]["INSPECTIONPLANMONTH"] = cboInspectionPlanMonth.Text;

                if (_AuditHeaderDt.Rows[0]["INSPECTIONPLANWEEK"].ToString() != cboInspectionPlanWeek.Text)
                    _AuditHeaderDt.Rows[0]["INSPECTIONPLANWEEK"] = cboInspectionPlanWeek.Text;
            }
            else
            {
                if (!_AuditHeaderDt.Columns.Contains("YEAR")) _AuditHeaderDt.Columns.Add("YEAR");
                if (!_AuditHeaderDt.Columns.Contains("QUARTER")) _AuditHeaderDt.Columns.Add("QUARTER");
                if (!_AuditHeaderDt.Columns.Contains("ENTERPRISEID")) _AuditHeaderDt.Columns.Add("ENTERPRISEID");
                if (!_AuditHeaderDt.Columns.Contains("PLANTID")) _AuditHeaderDt.Columns.Add("PLANTID");
                if (!_AuditHeaderDt.Columns.Contains("VENDORID")) _AuditHeaderDt.Columns.Add("VENDORID");
                if (!_AuditHeaderDt.Columns.Contains("AREAID")) _AuditHeaderDt.Columns.Add("AREAID");
                if (!_AuditHeaderDt.Columns.Contains("INSPECTIONPLANMONTH")) _AuditHeaderDt.Columns.Add("INSPECTIONPLANMONTH");
                if (!_AuditHeaderDt.Columns.Contains("INSPECTIONPLANWEEK")) _AuditHeaderDt.Columns.Add("INSPECTIONPLANWEEK");
                if (!_AuditHeaderDt.Columns.Contains("_STATE_")) _AuditHeaderDt.Columns.Add("_STATE_");

                DataRow dr = _AuditHeaderDt.NewRow();
                dr["YEAR"] = _sYEAR;
                dr["QUARTER"] = _sQUARTER;
                dr["ENTERPRISEID"] = _sENTERPRISEID;
                dr["PLANTID"] = _sPLANTID;
                dr["VENDORID"] = _sVENDORID;
                dr["AREAID"] = _sAREAID;
                dr["INSPECTIONPLANMONTH"] = cboInspectionPlanMonth.Text;
                dr["INSPECTIONPLANWEEK"] = cboInspectionPlanWeek.Text;
                dr["_STATE_"] = "added";

                _AuditHeaderDt.Rows.Add(dr);
            }

            var changedCols = new List<DataColumn>();
            foreach (DataRow dr in _AuditHeaderDt.Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    //DataColumn dc = _AuditHeaderDt.Columns["INSPECTIONPLANMONTH"];
                    //changedCols.Add(dc);

                    foreach (DataColumn dc in _AuditHeaderDt.Columns)
                    {
                        changedCols.Add(dc);
                    }
                }
                else if (dr.RowState == DataRowState.Modified)
                {
                    foreach (DataColumn dc in _AuditHeaderDt.Columns)
                    {
                        if (!dr[dc, DataRowVersion.Original].Equals(
                             dr[dc, DataRowVersion.Current]))
                        {
                            changedCols.Add(dc);
                        }
                    }
                }
            }
            //DataRowState.Added 이면 _STATE_ 컬럼이 추가되었다
            if (changedCols.Count > 0 && !_AuditHeaderDt.Columns.Contains("_STATE_"))
            {
                _AuditHeaderDt.Columns.Add("_STATE_");
                _AuditHeaderDt.Rows[0]["_STATE_"] = "modified";
            }

            foreach (DataRow dr in dt.Rows)
            {
                string sMonth = cboInspectionPlanMonth.Text.Replace("0", "");
                string sWeek = cboInspectionPlanWeek.Text;
                if (dr["INSPECTIONPLANMONTH"].ToString() != sMonth)
                    dr["INSPECTIONPLANMONTH"] = sMonth;
                if (dr["INSPECTIONPLANWEEK"].ToString() != sWeek)
                    dr["INSPECTIONPLANWEEK"] = sWeek;
            }
            
            DataTable changed = grdAuditManageregist.GetChangedRows();
            //Reportfile DataTable
            DataTable fileChanged = fpcReport.GetChangedRows();
            if (changed.Rows.Count == 0 && fileChanged.Rows.Count == 0 && changedCols.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            grdAuditManageregist.View.CheckValidation();
            result = this.ShowMessage(MessageBoxButtons.YesNo, "InfoPopupSave");//변경 내용을 저장하시겠습니까??

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    this.ShowWaitArea();
                    btnSave.Enabled = false;
                    btnClose.Enabled = false;

                    changed.TableName = "list1";
                    foreach (DataRow dr in changed.Rows)
                    {
                        dr["ENTERPRISEID"] = _sENTERPRISEID;
                        dr["PLANTID"] = _sPLANTID;
                        dr["VENDORID"] = _sVENDORID;
                        dr["AREAID"] = _sAREAID;
                        dr["YEAR"] = _sYEAR;
                        dr["QUARTER"] = _sQUARTER;
                        //dr["INSPECTIONPLANMONTH"] = cboInspectionPlanMonth.Text;
                        //dr["INSPECTIONPLANWEEK"] = cboInspectionPlanWeek.Text;
                    }

                    fileChanged.TableName = "list2";
                    _AuditHeaderDt.TableName = "list3";
                    DataSet rullSet = new DataSet();
                    rullSet.Tables.Add(changed);
                    rullSet.Tables.Add(fileChanged);
                    rullSet.Tables.Add(_AuditHeaderDt.Copy());
                    ExecuteRule("SaveAuditManageRegistQuarter", rullSet);

                    ShowMessage("SuccessSave");
                }
                catch (Exception ex)
                {
                    this.ShowError(ex);
                }
                finally
                {
                    this.CloseWaitArea();
                    btnSave.Enabled = true;
                    btnClose.Enabled = true;
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

            InitializeGrid();
            fpcReport.LanguageKey = "ATTACHEDFILE";
            DataTable dtWeek = new DataTable();
            dtWeek.Columns.Add("Key");
            dtWeek.Columns.Add("Value");
            for (int i = 1; i < 6; i++)
            {
                DataRow dr = null;
                dr = dtWeek.NewRow();
                dr["Key"] = i.ToString();
                dr["Value"] = i.ToString();
                dtWeek.Rows.Add(dr);
            }
            cboInspectionPlanWeek.DataSource = dtWeek;
            cboInspectionPlanWeek.DisplayMember = "Key";
            cboInspectionPlanWeek.ValueMember = "Value";
            cboInspectionPlanWeek.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboInspectionPlanWeek.Columns["Key"].Visible = false;

            int iMonthQuarter = 1;
            switch (_sQUARTER)
            {
                case "1":
                    iMonthQuarter = 1;
                    break;
                case "2":
                    iMonthQuarter = 4;
                    break;
                case "3":
                    iMonthQuarter = 7;
                    break;
                case "4":
                    iMonthQuarter = 10;
                    break;
            }

            DataTable dtMonth = new DataTable();
            dtMonth.Columns.Add("Key");
            dtMonth.Columns.Add("Value");
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = null;
                dr = dtMonth.NewRow();
                dr["Key"] = (i+ iMonthQuarter).ToString();
                dr["Value"] = (i + iMonthQuarter).ToString();
                dtMonth.Rows.Add(dr);
            }
            cboInspectionPlanMonth.DataSource = dtMonth;
            cboInspectionPlanMonth.DisplayMember = "Key";
            cboInspectionPlanMonth.ValueMember = "Value";
            cboInspectionPlanMonth.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboInspectionPlanMonth.Columns["Key"].Visible = false;

            SearchAuditManagereg();

            View_FocusedRowChanged(null, null);
        }
        private void txtRemark_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRemark.EditValue.ToString().Length > 0)
            {
                DataRow row = this.grdAuditManageregist.View.GetFocusedDataRow();
                if (row != null && row["DESCRIPTION"].ToString() != txtRemark.EditValue.ToString())
                    row["DESCRIPTION"] = txtRemark.EditValue;
            }

        }



        #endregion

        #region Private Function

        /// <summary>
        /// 메인 그리드에서 Lot ID를 가져와서 해당 Lot의 불량코드 정보조회
        /// </summary>
        private void SearchAuditManagereg()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_YEAR", _sYEAR);
            param.Add("P_QUARTER", _sQUARTER);
            param.Add("P_ENTERPRISEID", _sENTERPRISEID);
            param.Add("P_PLANTID", _sPLANTID);
            param.Add("P_VENDORID", _sVENDORID);
            param.Add("P_AREAID", _sAREAID);
            //param.Add("P_PROCESSSEGMENTID", _sPROCESSSEGMENTID);
            //param.Add("AREAID", UserInfo.Current.LanguageType);

            
            _AuditHeaderDt = SqlExecuter.Query("SelectAuditManageRegistQuarterHeaderResult", "10001", param);
            _AuditManageDt = SqlExecuter.Query("SelectAuditManageRegistQuarterResult", "10001", param);

            if (_AuditHeaderDt.Rows.Count < 1 && _AuditManageDt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdAuditManageregist.DataSource = _AuditManageDt;

            //                string sMonth = dtpInspectionPlanMonth.Text.Replace("0", "");
            //string sWeek = cboInspectionPlanWeek.Text;
            if (_AuditHeaderDt != null && _AuditHeaderDt.Rows.Count > 0)
            {
                DataRow dr = _AuditHeaderDt.Rows[0];
                cboInspectionPlanMonth.EditValue = dr["INSPECTIONPLANMONTH"].ToString();
                cboInspectionPlanWeek.EditValue = dr["INSPECTIONPLANWEEK"].ToString();
            }
        }




        #endregion

        #region 유효성 검사
        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        //protected override void OnValidateContent()
        //{
        //    base.OnValidateContent();

        //    grdAuditManageregist.View.CheckValidation();

        //    DataTable changed = grdAuditManageregist.GetChangedRows();

        //    if (changed.Rows.Count == 0)
        //    {
        //        // 저장할 데이터가 존재하지 않습니다.
        //        throw MessageException.Create("NoSaveData");
        //    }
        //}
        #endregion
    }
}
