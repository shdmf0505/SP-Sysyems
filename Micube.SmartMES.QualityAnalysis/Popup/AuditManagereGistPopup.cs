#region using

using DevExpress.XtraEditors.Repository;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > Audit 관리 > Audit 관리대장
    /// 업  무  설  명  : 협력업체 관리대장 현황 을 조회하여 점검결과를 등록한다.
    /// 생    성    자  : 유호석
    /// 생    성    일  : 2019-08-09
    /// 수  정  이  력  :
    ///     1. 2021.01.20 전우성 : 화면 개선 및 정리
    ///
    /// </summary>
    public partial class AuditManageRegistPopup : SmartPopupBaseForm, ISmartCustomPopup
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

        private string _sENTERPRISEID = string.Empty;
        private string _sPLANTID = string.Empty;
        private string _sVENDORID = string.Empty;
        private string _sAREAID = string.Empty;
        private string _sVENDORNAME = string.Empty;
        private string _sAREANAME = string.Empty;

        #endregion Local Variables

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="sENTERPRISEID"></param>
        /// <param name="sPLANTID"></param>
        /// <param name="sVENDORID"></param>
        /// <param name="sAREAID"></param>
        /// <param name="sVENDORNAME"></param>
        /// <param name="sAREANAME"></param>
        public AuditManageRegistPopup(string sENTERPRISEID, string sPLANTID, string sVENDORID, string sAREAID, string sVENDORNAME, string sAREANAME)
        {
            InitializeComponent();

            InitializeGrid();
            InitializeEvent();
            InitializeLanguageKey();
            InitializeControls();

            _sENTERPRISEID = sENTERPRISEID;
            _sPLANTID = sPLANTID;
            _sVENDORID = sVENDORID;
            _sAREAID = sAREAID;
            _sVENDORNAME = sVENDORNAME;
            _sAREANAME = sAREANAME;
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControls()
        {
            this.AcceptButton = btnSave;
            this.CancelButton = btnClose;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            LanguageKey = "AUDITMANAGEREGISTPOPUP";
            smartLayoutControl1.SetLanguageKey(layoutControlItem2, "DESCRIPTION");
            grdAuditManageregist.LanguageKey = "LIST";
            btnSave.LanguageKey = "SAVE";
            btnClose.LanguageKey = "CLOSE";
            fpcReport.LanguageKey = "ATTACHEDFILE";
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdAuditManageregist.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Restore | GridButtonItem.Add | GridButtonItem.Delete;

            grdAuditManageregist.View.AddTextBoxColumn("VENDORNAME", 160).SetLabel("VENDORNAME").SetIsReadOnly();
            grdAuditManageregist.View.AddTextBoxColumn("AREANAME", 100).SetLabel("AREANAME").SetIsReadOnly();
            grdAuditManageregist.View.AddComboBoxColumn("AUDITTYPE", 80, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=AuditType"))
                .SetLabel("CLASS").SetTextAlignment(TextAlignment.Center).SetValidationIsRequired();// 공통코드-확인 신규 / 장기미거래(자동입력)

            grdAuditManageregist.View.AddTextBoxColumn("DEGREE", 200).SetLabel("DEGREE").SetIsReadOnly(); // 자동증가/신규등록시
            grdAuditManageregist.View.AddDateEditColumn("INSPECTIONDATE", 150).SetLabel("AUDITDATE").SetValidationIsRequired(); ; // 수동입력/일자
            grdAuditManageregist.View.AddSpinEditColumn("EXAMINRESULT", 100).SetValidationIsRequired(); ; // 수동입력/숫자만
            grdAuditManageregist.View.AddComboBoxColumn("ISOPRATIONSTOP", 100, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "CODECLASSID=YesNo")).SetTextAlignment(TextAlignment.Center);// 공통코드(YesNo)

            grdAuditManageregist.View.AddTextBoxColumn("ENTERPRISEID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("PLANTID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("VENDORID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("AREAID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("PROCESSSEGMENTID", 200).SetIsHidden();
            grdAuditManageregist.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 200).SetIsHidden();

            grdAuditManageregist.View.PopulateColumns();

            RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            date.Mask.EditMask = "yyyy-MM-dd";
            date.Mask.UseMaskAsDisplayFormat = true;

            grdAuditManageregist.View.Columns["INSPECTIONDATE"].ColumnEdit = date;
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += (s, e) =>
            {
                try
                {
                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "P_ENTERPRISEID", _sENTERPRISEID },
                        { "P_PLANTID", _sPLANTID },
                        { "P_VENDORID", _sVENDORID },
                        { "P_AREAID", _sAREAID }
                    };

                    if (SqlExecuter.Query("SelectAuditManageRegistResult", "10001", param) is DataTable dt)
                    {
                        if (dt.Rows.Count.Equals(0))// 조회할 데이터가 없습니다.
                        {
                            ShowMessage("NoSelectData");
                            return;
                        }
                        grdAuditManageregist.DataSource = dt;
                        SetFileIntoLoad();
                    }
                }
                catch
                {
                }
            };

            grdAuditManageregist.View.CellValueChanged += (s, e) =>
            {
                DataRow row = grdAuditManageregist.View.GetFocusedDataRow();

                if (e.Column.FieldName == "INSPECTIONDATE")
                {
                    DataTable dtAudit = grdAuditManageregist.DataSource as DataTable;

                    string sDEGREE = row["DEGREE"].ToString();
                    DateTime de = new DateTime();
                    DateTime.TryParse(e.Value.ToString(), out de);

                    if (dtAudit.Rows.Count == 1)//이전 이후 차수 비교할 필요가 없을떄.
                    {
                        if (DateTime.Compare(de, DateTime.Now) > 0)
                        {
                            ShowMessage("InValidInspectionDate");//점검일자는 오늘 이전 날짜입니다.
                            row["INSPECTIONDATE"] = string.Empty;
                            return;
                        }
                        else
                            row["INSPECTIONDATE"] = de.ToShortDateString();
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
                            .Select(r =>
                            {
                                int seq = 0;
                                string sOrgDEGREE = r["DEGREE"].ToString();
                                Int32.TryParse(r["DEGREE"].ToString(), out seq);
                                return new { sOrgDEGREE, seq };
                            })
                            .Where(c => c.seq < iDEGREE)
                            .ToList();

                        var listMax = dtAudit.AsEnumerable()
                            .Select(r =>
                            {
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

                        if (DateTime.Compare(de, DateTime.Now) > 0)
                        {
                            ShowMessage("InValidInspectionDate");//점검일자는 오늘 이전 날짜입니다.
                            row["INSPECTIONDATE"] = string.Empty;
                            return;
                        }
                        else
                            row["INSPECTIONDATE"] = de.ToShortDateString();
                    }
                }
            };

            grdAuditManageregist.View.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle < 0)
                {
                    return;
                }

                SetFileIntoLoad();
            };

            grdAuditManageregist.View.ShowingEditor += (s, e) =>
            {
                if (!grdAuditManageregist.View.FocusedColumn.FieldName.Equals("INSPECTIONDATE"))
                {
                    return;
                }

                e.Cancel = grdAuditManageregist.View.GetFocusedDataRow()["DEGREE"].ToString().Length.Equals(0);
            };

            grdAuditManageregist.View.AddingNewRow += (s, e) =>
            {
                if (!e.NewRow.Table.Columns.Contains("ENTERPRISEID")) e.NewRow.Table.Columns.Add("ENTERPRISEID");
                if (!e.NewRow.Table.Columns.Contains("PLANTID")) e.NewRow.Table.Columns.Add("PLANTID");
                if (!e.NewRow.Table.Columns.Contains("VENDORID")) e.NewRow.Table.Columns.Add("VENDORID");
                if (!e.NewRow.Table.Columns.Contains("AREAID")) e.NewRow.Table.Columns.Add("AREAID");
                if (!e.NewRow.Table.Columns.Contains("VENDORNAME")) e.NewRow.Table.Columns.Add("VENDORNAME");
                if (!e.NewRow.Table.Columns.Contains("AREANAME")) e.NewRow.Table.Columns.Add("AREANAME");

                e.NewRow["ENTERPRISEID"] = _sENTERPRISEID;
                e.NewRow["PLANTID"] = _sPLANTID;
                e.NewRow["VENDORID"] = _sVENDORID;
                e.NewRow["AREAID"] = _sAREAID;
                e.NewRow["VENDORNAME"] = _sVENDORNAME;
                e.NewRow["AREANAME"] = _sAREANAME;

                e.NewRow["ISOPRATIONSTOP"] = "N";
                e.NewRow["AUDITTYPE"] = "New";
            };

            grdAuditManageregist.ToolbarAddingRow += (s, e) =>
            {
                if (s is SmartBandedGrid grid)
                {
                    e.Cancel = ((grid.DataSource as DataTable).Rows.Cast<DataRow>().Where(r => r.RowState == DataRowState.Added).ToList().Count > 0);
                }
            };

            txtRemark.EditValueChanged += (s, e) =>
            {
                if (txtRemark.EditValue.ToString().Length > 0)
                {
                    DataRow row = this.grdAuditManageregist.View.GetFocusedDataRow();
                    if (row != null && row["DESCRIPTION"].ToString() != txtRemark.EditValue.ToString())
                        row["DESCRIPTION"] = txtRemark.EditValue;
                }
            };

            btnSave.Click += (s, e) =>
            {
                DialogResult result = System.Windows.Forms.DialogResult.No;
                DataTable changed = grdAuditManageregist.GetChangedRows();
                DataTable fileChanged = fpcReport.GetChangedRows();
                if (grdAuditManageregist.GetChangedRows().Rows.Count == 0 && fileChanged.Rows.Count == 0) return;
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
                        }

                        fileChanged.TableName = "list2";

                        DataSet rullSet = new DataSet();
                        rullSet.Tables.Add(changed);
                        rullSet.Tables.Add(fileChanged);
                        ExecuteRule("SaveAuditManagementledger", rullSet);

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
            };

            btnClose.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        #endregion Event

        #region private function

        /// <summary>
        /// fpcReport
        /// 파일이 추가될때 차수가 없는것들을 따로 보관한다
        /// 저장할 파일의 Key생성에 사용되는 컬럼목록 추가 해줌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnRowChanged(object sender, DataRowChangeEventArgs args)
        {
            if (args.Action == DataRowAction.Add)
            {
                if (!args.Row.Table.Columns.Contains("ENTERPRISEID")) args.Row.Table.Columns.Add("ENTERPRISEID");
                if (!args.Row.Table.Columns.Contains("PLANTID")) args.Row.Table.Columns.Add("PLANTID");
                if (!args.Row.Table.Columns.Contains("VENDORID")) args.Row.Table.Columns.Add("VENDORID");
                if (!args.Row.Table.Columns.Contains("AREAID")) args.Row.Table.Columns.Add("AREAID");
                if (!args.Row.Table.Columns.Contains("PROCESSSEGMENTID")) args.Row.Table.Columns.Add("PROCESSSEGMENTID");
                if (!args.Row.Table.Columns.Contains("PROCESSSEGMENTVERSION")) args.Row.Table.Columns.Add("PROCESSSEGMENTVERSION");
                if (!args.Row.Table.Columns.Contains("DEGREE")) args.Row.Table.Columns.Add("DEGREE");

                DataRow dr = args.Row;
                dr["ENTERPRISEID"] = _sENTERPRISEID;
                dr["PLANTID"] = _sPLANTID;
                dr["VENDORID"] = _sVENDORID;
                dr["AREAID"] = _sAREAID;
                DataRow rowFocused = grdAuditManageregist.View.GetFocusedDataRow();
                dr["DEGREE"] = rowFocused["DEGREE"].ToString();
            }
        }

        private void SetFileIntoLoad()
        {
            txtRemark.Text = Format.GetString(grdAuditManageregist.View.GetFocusedDataRow().GetObject("DESCRIPTION"), string.Empty);

            Dictionary<string, object> fileValues = new Dictionary<string, object>
                {
                { "P_FILERESOURCETYPE", "AUDITMANAGEREGIST" },
                { "P_FILERESOURCEID", _sENTERPRISEID + _sPLANTID + _sVENDORID + _sAREAID + Format.GetString(grdAuditManageregist.View.GetFocusedDataRow()["DEGREE"], string.Empty) },
                { "P_FILERESOURCEVERSION", "0" }
                };

            fpcReport.DataSource = SqlExecuter.Query("SelectFileInspResult", "10001", fileValues);

            (fpcReport.DataSource as DataTable).RowChanged -= new DataRowChangeEventHandler(OnRowChanged);
            (fpcReport.DataSource as DataTable).RowChanged += new DataRowChangeEventHandler(OnRowChanged);
        }

        #endregion private function
    }
}