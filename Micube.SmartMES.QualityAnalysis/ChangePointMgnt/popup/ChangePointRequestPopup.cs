#region using

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
    /// 프 로 그 램 명  : 품질관리 > 변경점 관리 > 변경점 신청서 등록/이력조회 > 변경점 신청서 등록
    /// 업  무  설  명  : 변경점 신청을 등록하고 이력을 조회하는 화면에서 변경점을 신청할 수 있는 팝업을 호출한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-09-09
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChangePointRequestPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 입력, 수정을 구분하기 위한 변수
        /// </summary>
        public string Type;

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        /// <summary>
        /// 변경점 신청내용 테이블
        /// </summary>
        private DataTable _changePoint;

        /// <summary>
        /// 변경점 신청 검토 테이블
        /// </summary>
        private DataTable _changePointReview;

        /// <summary>
        /// 결재 테이블
        /// </summary>
        private DataTable _approval;

        /// <summary>
        /// 파일 리스트 테이블
        /// </summary>
        private DataTable _fileList;

        /// <summary>
        /// 품목 ID, Version에 따른 Lot No, 고객사를 선택하기 위한 변수
        /// </summary>
        private ConditionItemTextBox productDefIdBox;
        private ConditionItemTextBox productDefVersionBox;
        private ConditionItemTextBox productDefIdBox2;
        private ConditionItemTextBox productDefVersionBox2;
        private ConditionItemTextBox hasCustomer;

        /// <summary>
        /// 품목 ID에 따라 고객사가 존재하는지 확인하기 위한 변수
        /// </summary>
        private string _customerFlag;

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public ChangePointRequestPopup()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 검토/승인 그리드 초기화
        /// </summary>
        private void InitializeReviewAndApprovalGrid()
        {
            grdReviewAndApproval.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdReviewAndApproval.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var userPopup = grdReviewAndApproval.View.AddSelectPopupColumn("USERNAME", new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupApplySelection((selectedRows, dataGridRow) =>
                {
                    // selectedRows : 팝업에서 선택한 DataRow(컬렉션)
                    // dataGridRow : 현재 Focus가 있는 그리드의 DataRow
                    if (selectedRows.Count() < 1)
                    {
                        return;
                    }

                    foreach (DataRow row in selectedRows)
                    {
                        dataGridRow["USERID"] = row["USERID"].ToString();
                        dataGridRow["USERNAME"] = row["USERNAME"].ToString();
                        dataGridRow["DEPARTMENT"] = row["DEPARTMENT"].ToString();
                    }
                });

            userPopup.Conditions.AddTextBox("USERIDNAME");

            userPopup.GridColumns.AddTextBoxColumn("USERID", 180); // 사용자ID
            userPopup.GridColumns.AddTextBoxColumn("USERNAME", 120)
                .SetTextAlignment(TextAlignment.Center); // 사용자명
            userPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 150); // 부서
            userPopup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 180); // 이메일
            userPopup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 180)
                .SetTextAlignment(TextAlignment.Center); // 핸드폰번호 

            grdReviewAndApproval.View.AddTextBoxColumn("DEPARTMENT", 120)
                .SetIsReadOnly(); // 부서명
            grdReviewAndApproval.View.AddTextBoxColumn("REVIEWDATE", 220)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetLabel("REVIEWTIME"); // 검토일시
            grdReviewAndApproval.View.AddTextBoxColumn("REVIEWCOMMENTS", 250)
                .SetLabel("COMMENTS"); // 내용 

            grdReviewAndApproval.View.AddTextBoxColumn("CHANGEPOINTNO", 100)
                .SetIsHidden(); // 변경점 관리번호
            grdReviewAndApproval.View.AddTextBoxColumn("SEQUENCE", 100)
                .SetIsHidden(); // 순서
            grdReviewAndApproval.View.AddTextBoxColumn("USERID", 100)
                .SetIsHidden(); // 유저 ID

            grdReviewAndApproval.View.PopulateColumns();           
        }

        /// <summary>
        /// Data 초기화
        /// </summary>
        private void InitializeData()
        {
            if (Type == "Old")
            {
                // 변경점 신청내용 Data 
                txtChangePointNo.EditValue = CurrentDataRow["CHANGEPOINTNO"];
                dtpRequestDate.EditValue = CurrentDataRow["REQUESTDATE"];
                txtRequestDepartment.EditValue = CurrentDataRow["REQUESTDEPARTMENT"];
                txtChangePointTitle.EditValue = CurrentDataRow["SUBJECT"];
                popupProduct.SetValue(CurrentDataRow["PRODUCTDEFID"].ToString());
                popupProduct.Text = CurrentDataRow["PRODUCTDEFNAME"].ToString();
                popupProduct.Tag = CurrentDataRow["PRODUCTDEFVERSION"].ToString();
                popupProcessSegment.SetValue(CurrentDataRow["PROCESSSEGMENTID"].ToString());
                popupProcessSegment.Text = CurrentDataRow["PROCESSSEGMENTNAME"].ToString();
                popupProcessSegment.Tag = CurrentDataRow["PROCESSSEGMENTVERSION"].ToString();
                txtMaterial.EditValue = CurrentDataRow["CONSUMABLENAME"];
                popupLot.SetValue(CurrentDataRow["LOTID"].ToString());
                popupLot.Text = CurrentDataRow["LOTID"].ToString();
                txtStockState.EditValue = CurrentDataRow["STOCKSTATUS"];
                SetCheckValue(CurrentDataRow["SEGMENTTYPE"].ToString(), chkProductionProcess, chkPurchase, chkSpecification, chkEtc);
                SetCheckValue(CurrentDataRow["CHANGETYPE"].ToString(), chkRawMaterial, chkEquipment, chkWorkingCondition, chkPerson, chkEnvironment);
                SetCheckValue(CurrentDataRow["APPLICATIONTYPE"].ToString(), chkAfterApproval, chkAfterExhaust);
                txtChangeProductManagement.EditValue = CurrentDataRow["CHANGEITEMMGNT"];
                popupCustomer.SetValue(CurrentDataRow["CUSTOMERID"].ToString());
                popupCustomer.Text = CurrentDataRow["CUSTOMERNAME"].ToString();
                txtStockTreatmentPlan.EditValue = CurrentDataRow["STOCKHANDLEMETHOD"];
                memoChangeReason.Text = CurrentDataRow["REASONCOMMENTS"].ToString();
                memoBeforeChange.Text = CurrentDataRow["BEFORECOMMENTS"].ToString();
                memoAfterChange.Text = CurrentDataRow["AFTERCOMMENTS"].ToString();
                memoChangeDetail.Text = CurrentDataRow["CHANGEDETAILS"].ToString();
                cboChangePointType.EditValue = CurrentDataRow["CHANGEPOINTTYPE"];
                rdoGrade.EditValue = CurrentDataRow["RATINGDECISION"];

                productDefIdBox.SetDefault(CurrentDataRow["PRODUCTDEFID"].ToString());
                productDefVersionBox.SetDefault(CurrentDataRow["PRODUCTDEFVERSION"].ToString());

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("P_CHANGEPOINTNO", CurrentDataRow["CHANGEPOINTNO"]);
                param.Add("P_REQUESTNO", CurrentDataRow["CHANGEPOINTNO"]);
                param.Add("P_APPROVALTYPE", "ChangePointManagement");

                // 변경점 검토내용 Data
                DataTable dt = SqlExecuter.Query("GetChangePointReview", "10001", param);
                grdReviewAndApproval.DataSource = dt;

                // 결재내용 Data
                DataTable approvalDt = SqlExecuter.Query("GetQCApproval", "10001", param);
                ucApproval.SetApproval = approvalDt;         

                // 파일내용 Data
                Dictionary<string, object> fileParam = new Dictionary<string, object>();
                fileParam.Add("RESOURCEID", CurrentDataRow["CHANGEPOINTNO"]);
                fileParam.Add("RESOURCETYPE", "ChangePoint");
                fileParam.Add("RESOURCEVERSION", "1");

                DataTable fileDt = SqlExecuter.Query("GetInspectionPaperFile", "10001", fileParam);
                fileChangePoint.DataSource = fileDt;

                fileChangePoint.Resource.Type = "ChangePoint";
                fileChangePoint.Resource.Version = "1";
                fileChangePoint.UploadPath = "ChangePointMgnt/ChangePointFile";
            }
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_REQUESTNO", "");
                param.Add("P_APPROVALTYPE", "");

                // 신규여도 강제로 결제컨트롤에 SetApproval
                DataTable approvalDt = SqlExecuter.Query("GetQCApproval", "10001", param);
                ucApproval.SetApproval = approvalDt;

                fileChangePoint.Resource.Type = "ChangePoint";
                fileChangePoint.Resource.Version = "1";
                fileChangePoint.UploadPath = "ChangePointMgnt/ChangePointFile";
            }
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;

            popupProduct.ButtonClick += PopupProduct_ButtonClick;
            grdReviewAndApproval.View.AddingNewRow += View_AddingNewRow;
            ucApproval.grdApproval.View.ShowingEditor += View_ShowingEditor;
            cboIsUseProduct.EditValueChanged += CboIsUseProduct_EditValueChanged;
        }
        
        /// <summary>
        /// 품목사용여부 Y일때 품목팝업 활성화, N일때 비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboIsUseProduct_EditValueChanged(object sender, EventArgs e)
        {
            if (!txtChangePointTitle.ReadOnly)
            {
                if (cboIsUseProduct.EditValue.Equals("Y"))
                {
                    popupProduct.Enabled = true;
                }
                else
                {
                    popupProduct.ClearValue();
                    popupProduct.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 품목 Popup 삭제했을때 Lot Popup, Customer Popup 조회조건 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                productDefIdBox.SetDefault(null);
                productDefVersionBox.SetDefault(null);
                popupProduct.Tag = null;
                popupLot.SetValue(null);
                popupLot.Text = null;

                productDefIdBox2.SetDefault(null);
                productDefVersionBox2.SetDefault(null);
                hasCustomer.SetDefault(null);
                popupCustomer.SetValue(null);
                popupCustomer.Text = null;
            }
        }

        /// <summary>
        /// 로그인한 유저가 해당 절차구분에서 결재를 했다면 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        { 
            DataRow row = ucApproval.grdApproval.View.GetFocusedDataRow();

            // 해당 Row의 결재자가 로그인한 유저가 아니라면 수정불가
            if (!row["CHARGERID"].Equals(UserInfo.Current.Id))
            {
                e.Cancel = true;
            }

            // 결재상태가 아무것도 입력이 안됬으면 return
            if (string.IsNullOrWhiteSpace(row["APPROVALSTATE"].ToString()))
            {
                return;
            }

            // 절차구분이 요청이면서, 역할구분이 기안인것
            if (row["PROCESSTYPE"].Equals("Draft") && row["CHARGETYPE"].Equals("Request"))
            {
                // 결재상태가 회수가 아니라면 수정불가
                if (!row["APPROVALSTATE"].Equals("Reclamation"))
                {
                    if (row.RowState == DataRowState.Unchanged)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// 검토/승인 내용 그리드 추가시 검토일자 자동입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            args.NewRow["REVIEWDATE"] = DateTime.Now;
            args.NewRow["USERID"] = UserInfo.Current.Id;
            args.NewRow["USERNAME"] = UserInfo.Current.Name;
            args.NewRow["DEPARTMENT"] = UserInfo.Current.Department;

            if (Type == "Old")
            {
                args.NewRow["CHANGEPOINTNO"] = CurrentDataRow["CHANGEPOINTNO"];
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
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 품목사용여부가 Y인 경우 품목필수입력
            if (cboIsUseProduct.EditValue.Equals("Y"))
            {
                // 품목은 필수입력입니다.
                if (string.IsNullOrWhiteSpace(Format.GetString(popupProduct.Text)))
                {
                    throw MessageException.Create("RequiredProductDefId");
                }
            }

            // 공정유형, 변경유형, 적용구분은 각각 최소 1개 필수입력
            if (!chkProductionProcess.ReadOnly && !chkRawMaterial.ReadOnly && !chkAfterApproval.ReadOnly)
            {
                if (CheckValidColumn(chkProductionProcess, chkPurchase, chkSpecification, chkEtc)
                    || CheckValidColumn(chkRawMaterial, chkEquipment, chkWorkingCondition, chkPerson, chkEnvironment)
                    || CheckValidColumn(chkAfterApproval, chkAfterExhaust))
                {
                    throw MessageException.Create("ChangePointRequiredCheck"); // 공정유형, 변경유형, 적용구분은 각각 최소 1개의 값을 체크해야합니다.
                }
            }

            if (!txtChangePointTitle.ReadOnly && !memoChangeReason.ReadOnly && !memoBeforeChange.ReadOnly && !memoAfterChange.ReadOnly)
            {
                // 변경점제목, 변경사유, 변경전, 변경후는 필수입력
                if (string.IsNullOrWhiteSpace(Format.GetString(txtChangePointTitle.EditValue))
                    || string.IsNullOrWhiteSpace(memoChangeReason.Text)
                    || string.IsNullOrWhiteSpace(memoBeforeChange.Text)
                    || string.IsNullOrWhiteSpace(memoAfterChange.Text))
                {
                    throw MessageException.Create("ValidateRequiredData"); // 필수값이 입력되지 않았습니다.
                }
            }

            // 저장하시겠습니까?
            if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ucApproval.grdApproval.View.CloseEditor();
                grdReviewAndApproval.View.CloseEditor();

                // 저장데이터 취합
                SettingSaveData();

                if (_changePointReview.Rows.Count > 0)
                {
                    foreach (DataRow row in _changePointReview.Rows)
                    {
                        if (string.IsNullOrWhiteSpace(row["USERNAME"].ToString()))
                        {
                            // 검토/승인내용에 사용자가 누락되었습니다.                           
                            throw MessageException.Create("UserMissingForReviewAndApproval");
                        }
                    }
                }

                ucApproval.ValidateApproval(); // 결재 유효성 검사             

                _changePoint.TableName = "changePoint"; // 변경점 신청 
                _changePointReview.TableName = "changePointReview"; // 변경점 검토
                _approval.TableName = "approval"; // 결재
                _fileList.TableName = "fileList"; // 파일

                DataSet ds = new DataSet();
                ds.Tables.Add(_changePoint.Copy());
                ds.Tables.Add(_changePointReview.Copy());
                ds.Tables.Add(_approval.Copy());
                ds.Tables.Add(_fileList.Copy());

                DataTable result = this.ExecuteRule<DataTable>("SaveChangePoint", ds);
                DataRow resultRow = result.Rows[0];

                // 저장할 파일데이터가 있다면 서버에 업로드한다.
                if (_fileList.Rows.Count != 0)
                {
                    fileChangePoint.Resource.Id = resultRow["CHANGEPOINTID"].ToString();
                    fileChangePoint.SaveChangedFiles();                   
                }
                
                if (CurrentDataRow != null)
                {
                    string mailContents = MailContents(CurrentDataRow, (grdReviewAndApproval.DataSource as DataTable)); // Mail로 보낼 내용 취합
                    ucApproval.ApprovalMail(Language.Get("CHANGEREVIEWANDAPPROVALREQUEST"), mailContents); // 결재라인에 메일 보내기 (타이틀 : 변경점 검토 및 결재요청)
                    ucApproval.ApprovalCompanionMail(Language.Get("CHANGEREVIEWCOMPANION"), mailContents); // 결재라인에 메일 보내기 (타이틀 : 변경점 검토 반려)
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Form Road시 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalissuePopup_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeSetting();
            InitializeComboBox();
            InitializePopup();
            InitializeDataTable();

            InitializeReviewAndApprovalGrid();
            InitializeData();
            SetReadOnly();

            (grdReviewAndApproval.View.Columns["USERNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += ChangePointRequestPopup_ButtonClick;
        }

        /// <summary>
        /// 사용자를 지웠을때 부서명도 함께지우기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePointRequestPopup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
            {
                grdReviewAndApproval.View.SetFocusedRowCellValue("DEPARTMENT", null);
            }
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 저장시 저장할 데이터를 취합한다.
        /// </summary>
        private void SettingSaveData()
        {
            // 변경점 신청 내용 
            DataRow row = _changePoint.NewRow();

            if (Type == "New")
            {
                row["CHANGEPOINTNO"] = "";
            }
            else
            {
                row["CHANGEPOINTNO"] = CurrentDataRow["CHANGEPOINTNO"];
            }
            row["ENTERPRISEID"] = UserInfo.Current.Enterprise;
            row["PLANTID"] = UserInfo.Current.Plant;
            row["REQUESTDATE"] = Convert.ToDateTime(dtpRequestDate.EditValue).ToString("yyyy-MM-dd HH:mm:ss");
            row["REQUESTDEPARTMENT"] = txtRequestDepartment.EditValue;
            row["SUBJECT"] = txtChangePointTitle.EditValue;
            row["PRODUCTDEFID"] = popupProduct.GetValue();
            row["PRODUCTDEFVERSION"] = popupProduct.Tag;
            row["PROCESSSEGMENTID"] = popupProcessSegment.GetValue();
            row["PROCESSSEGMENTVERSION"] = popupProcessSegment.Tag;
            row["CONSUMABLENAME"] = txtMaterial.EditValue;
            row["LOTID"] = popupLot.GetValue();
            row["STOCKSTATUS"] = txtStockState.EditValue;
            row["SEGMENTTYPE"] = GetCheckValue(chkProductionProcess, chkPurchase, chkSpecification, chkEtc);
            row["CHANGETYPE"] = GetCheckValue(chkRawMaterial, chkEquipment, chkWorkingCondition, chkPerson, chkEnvironment);
            row["APPLICATIONTYPE"] = GetCheckValue(chkAfterApproval, chkAfterExhaust);
            row["CHANGEITEMMGNT"] = txtChangeProductManagement.EditValue;
            row["CUSTOMERID"] = popupCustomer.GetValue();
            row["STOCKHANDLEMETHOD"] = txtStockTreatmentPlan.EditValue;
            row["REASONCOMMENTS"] = memoChangeReason.Text;
            row["BEFORECOMMENTS"] = memoBeforeChange.Text;
            row["AFTERCOMMENTS"] = memoAfterChange.Text;
            row["CHANGEDETAILS"] = memoChangeDetail.Text;
            row["CHANGEPOINTTYPE"] = cboChangePointType.EditValue;
            row["RATINGDECISION"] = rdoGrade.EditValue;

            _changePoint.Rows.Add(row);

            // 변경점 검토내용
            _changePointReview = grdReviewAndApproval.GetChangedRows();

            // 결재정보
            _approval = ucApproval.grdApproval.DataSource as DataTable;

            // 파일 리스트
            _fileList = fileChangePoint.GetChangedRows();
        }

        /// <summary>
        /// 체크박스의 체크유무에 따른 Value값을 구분자 ,로 하여 string형태로 만들어준다.
        /// </summary>
        /// <returns></returns>
        private string GetCheckValue(params SmartCheckBox[] chk)
        {
            string value = "";

            foreach (SmartCheckBox chkValue in chk)
            {
                if (Convert.ToBoolean(chkValue.EditValue))
                {
                    value += chkValue.Tag + ",";
                }
            }

            if (!string.IsNullOrEmpty(value))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return value;
        }

        /// <summary>
        /// 조회해온 데이터를 CheckBox에 반영해준다.
        /// </summary>
        /// <param name="checkList"></param>
        private void SetCheckValue(string col, params SmartCheckBox[] chkList)
        {
            if (!string.IsNullOrWhiteSpace(col))
            {
                string[] splitList;
                splitList = col.Split(',');

                foreach (string split in splitList)
                {
                    foreach (SmartCheckBox chk in chkList)
                    {
                        if (split == chk.Tag.ToString())
                        {
                            chk.EditValue = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 상황에 따라 화면을 ReadOnly처리한다.
        /// </summary>
        private void SetReadOnly()
        {
            // 기존 데이터가 존재하지 않으면 넘어간다.
            if (Type != "Old")
            {
                popupSelectLot.ReadOnly = true;
                popupSelectLot.Enabled = false;
                return;
            }
            else
            {
                // 기존 데이터가 있지만 결재를 등록하지 않았다면 넘어간다.
                if (ucApproval.grdApproval.View.RowCount == 0)
                {
                    return;
                }
                else
                {
                    // 절차구분이 있다면 요청의 결재상태를 확인한다.
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("P_APPROVALNO", CurrentDataRow["CHANGEPOINTNO"]);

                    // 요청에 대한 결재상태가 회수인지 조사
                    DataTable dt = SqlExecuter.Query("GetIsDraft", "10001", param);

                    // 이번 차례의 결재자가 누구인지 조사
                    DataTable dt2 = SqlExecuter.Query("GetChangePointThisTimeApprovalUser", "10001", param); 

                    // 결재테이블의 요청의 결재상태가 회수가 아니라면 ReadOnly처리
                    if (dt.Rows.Count == 0)
                    {
                        //ucApproval.btnADDAPPROVAL.Enabled = false;
                        txtChangePointTitle.ReadOnly = true;
                        txtMaterial.ReadOnly = true;
                        txtStockState.ReadOnly = true;
                        chkProductionProcess.Enabled = false;
                        chkPurchase.Enabled = false;
                        chkSpecification.Enabled = false;
                        chkEtc.Enabled = false;
                        chkRawMaterial.Enabled = false;
                        chkEquipment.Enabled = false;
                        chkWorkingCondition.Enabled = false;
                        chkPerson.Enabled = false;
                        chkEnvironment.Enabled = false;
                        chkAfterApproval.Enabled = false;
                        chkAfterExhaust.Enabled = false;
                        txtChangeProductManagement.ReadOnly = true;
                        txtStockTreatmentPlan.ReadOnly = true;
                        memoChangeReason.ReadOnly = true;
                        memoBeforeChange.ReadOnly = true;
                        memoAfterChange.ReadOnly = true;
                        memoChangeDetail.ReadOnly = true;
                        //fileChangePoint.btnFileAdd.Enabled = false;
                        //fileChangePoint.btnFileDelete.Enabled = false;
                        //fileChangePoint.grdFileList.View.SetIsReadOnly();
                        //grdReviewAndApproval.GridButtonItem = GridButtonItem.None;
                        //grdReviewAndApproval.View.SetIsReadOnly();
                        popupLot.Enabled = false;
                        popupProduct.Enabled = false;
                        popupProcessSegment.Enabled = false;
                        popupCustomer.Enabled = false;
                    }

                    // 이번 차례의 결재가 절차구분이 합의 또는 승인부서이고 로그인한 유저가 담당자라면 등급판정 UnReadOnly
                    if (dt2.Rows.Count == 0) return;

                    if ((dt2.Rows[0]["PROCESSTYPE"].Equals("Review") || dt2.Rows[0]["PROCESSTYPE"].Equals("Approval"))
                        && dt2.Rows[0]["CHARGERID"].Equals(UserInfo.Current.Id))
                    {                     
                        cboChangePointType.ReadOnly = false;
                        rdoGrade.Enabled = true;
                        rdoGrade.ReadOnly = false;
                    }
                }
            }
        }

        /// <summary>
        /// 메일로 보낼 내용을 지정한다.
        /// </summary>
        /// <param name="changeDt">변경점 메인테이블</param>
        /// <param name="reviewDt">변경점 검토/승인테이블</param>
        /// <returns></returns>
        private string MailContents(DataRow currentRow, DataTable reviewDt)
        {
            string reviewContents = string.Empty; // 검토/승인내용 

            // 여러개의 검토/승인내용 밑으로 나열
            if (reviewDt.Rows.Count != 0)
            {
                foreach (DataRow row in reviewDt.Rows)
                {
                    reviewContents += row["USERNAME"] + ", " + row["DEPARTMENT"] + ", " + row["REVIEWDATE"]
                                   + "<br><p style='text-align:left;'>" + row["REVIEWCOMMENTS"] + "<br>";
                }
            }

            // 공정유형 다국어처리
            string[] processType = GetCheckValue(chkProductionProcess, chkPurchase, chkSpecification, chkEtc).Split(',');
            string processTypeDic = "";

            for (int i = 0; i < processType.Length; i++)
            {
                if (i == processType.Length - 1)
                {
                    processTypeDic += Language.Get(processType[i]);
                }
                else
                {
                    processTypeDic += Language.Get(processType[i]) + ", ";
                }
            }

            // 변경유형 다국어처리
            string[] changeType = GetCheckValue(chkRawMaterial, chkEquipment, chkWorkingCondition, chkPerson, chkEnvironment).Split(',');
            string changeTypeDic = "";

            for (int i = 0; i < changeType.Length; i++)
            {
                if (i == changeType.Length - 1)
                {
                    changeTypeDic += Language.Get(changeType[i]);
                }
                else
                {
                    changeTypeDic += Language.Get(changeType[i]) + ", ";
                }
            }

            // 적용구분 다국어처리
            string[] applicationType = GetCheckValue(chkAfterApproval, chkAfterExhaust).Split(',');
            string applicationTypeDic = "";

            for (int i = 0; i < applicationType.Length; i++)
            {
                if (i == applicationType.Length - 1)
                {
                    applicationTypeDic += Language.Get(applicationType[i]);
                }
                else
                {
                    applicationTypeDic += Language.Get(applicationType[i]) + ", ";
                }
            }

            // 메일내용 다국어처리 및 데이터바인딩
            string contents = "<p style='text-align:left;'>" + "○ " + Language.Get("CHANGEPOINTNO") + " : " + txtChangePointNo.Text + "</p><br>" // 변경점 번호 
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("CHANGEPOINTDATE") + " : " + dtpRequestDate.Text + "</p><br>" // 변경점 신청일
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("CHANGEPOINTTITLE") + " : " + txtChangePointTitle.Text + "</p><br>" // 변경점 제목
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("GRADE") + "/" + Language.Get("CHANGEPOINTTYPE") + " : " + rdoGrade.Text + "/" + cboChangePointType.Text + "</p><br>" // 등급/변경점구분
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("CUTOMERNAME") + " : " + cboChangePointType.Text + "</p><br>" // 고객사명
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("PRODUCTDEFNAME") + " : " + popupProduct.Text + "</p><br>" // 품목명
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("SEGMENTTYPE") + " : " + processTypeDic + "</p><br>" // 공정유형
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("CHANGETYPE") + " : " + changeTypeDic + "</p><br>" // 변경유형
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("APPLICATIONTYPE") + " : " + applicationTypeDic + "</p><br>" // 적용구분
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("CHANGEREASON") + " : " + memoChangeReason.Text + "</p><br>" // 변경사유
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("BEFORECHANGE") + "/" + Language.Get("BEFORECHANGE") + " : " + memoBeforeChange.Text + "/" + memoAfterChange.Text + "</p><br>" // 변경전/후
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("CHANGEDETAILS") + " : " + memoChangeDetail.Text + "</p><br>" // 변경세부내용
                            + "<p style='text-align:left;'>" + "○ " + Language.Get("REVIEWANDAPPROVAL") + "</p><br>" + reviewContents; // 검토/승인내용

            return contents;
        }

        /// <summary>
        /// 공정유형, 변경유형, 적용구분은 각각 최소의 한개값을 체크하는 함수
        /// </summary>
        /// <returns></returns>
        private bool CheckValidColumn(params SmartCheckBox[] chk)
        {
            bool flag = false;
            int count = chk.Count();
            int falseCount = 0;

            foreach (SmartCheckBox chkBox in chk)
            {
                if (chkBox.EditValue.Equals(false)) falseCount += 1;
            }

            if (count == falseCount) flag = true;

            return flag;
        }

        #endregion

        #region InitalizeSetting

        /// <summary>
        /// 폼 최초 세팅
        /// </summary>
        private void InitializeSetting()
        {
            fileChangePoint.LanguageKey = "FILELIST";

            dtpRequestDate.EditValue = DateTime.Now;
            txtRequestDepartment.EditValue = UserInfo.Current.Department;
            cboChangePointType.EditValue = "ACN";

            // Radio Group 세팅
            Dictionary<string, object> radioParam = new Dictionary<string, object>();
            radioParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            radioParam.Add("CODECLASSID", "GradeClass");

            DataTable radioDt = SqlExecuter.Query("GetCodeList", "00001", radioParam);

            foreach (DataRow dr in radioDt.Rows)
            {
                rdoGrade.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(dr["CODEID"], dr["CODENAME"].ToString()));
            }
        }

        #endregion

        #region InitalizeComboBox

        /// <summary>
        /// ComboBox 컨트롤 초기화
        /// </summary>
        private void InitializeComboBox()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "ChangePointType"}
            };

            cboChangePointType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboChangePointType.ValueMember = "CODEID";
            cboChangePointType.DisplayMember = "CODENAME";
            cboChangePointType.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboChangePointType.ShowHeader = false;

            param.Remove("CODECLASSID");
            param.Add("CODECLASSID", "YesNo");

            cboIsUseProduct.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboIsUseProduct.ValueMember = "CODEID";
            cboIsUseProduct.DisplayMember = "CODENAME";
            cboIsUseProduct.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            cboIsUseProduct.EditValue = "N";
            cboIsUseProduct.ShowHeader = false;
        }

        #endregion

        #region InitalizePopup

        /// <summary>
        /// Popup 컨트롤 초기화
        /// </summary>
        private void InitializePopup()
        {
            popupProduct.SelectPopupCondition = ProductPopup();
            popupLot.SelectPopupCondition = LotPopup();
            popupSelectLot.SelectPopupCondition = SelectLotPopup();
            popupCustomer.SelectPopupCondition = CutomerPopup();
            popupProcessSegment.SelectPopupCondition = ProcessSegmentPopup();
        }

        /// <summary>
        /// 품목 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup ProductPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PRODUCT", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "PRODUCT";
            popup.SearchQuery = new SqlQuery("GetProductListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PRODUCTDEFNAME";
            popup.ValueFieldName = "PRODUCTDEFID";
            popup.LanguageKey = "PRODUCT";
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        popupProduct.Tag = row["PRODUCTDEFVERSION"].ToString();
                        productDefIdBox.SetDefault(row["PRODUCTDEFID"].ToString());
                        productDefVersionBox.SetDefault(row["PRODUCTDEFVERSION"].ToString());
                        productDefIdBox2.SetDefault(row["PRODUCTDEFID"].ToString());
                        productDefVersionBox2.SetDefault(row["PRODUCTDEFVERSION"].ToString());
                        popupLot.SetValue("");
                        popupLot.EditValue = "";
                        popupLot.ClearValue();
                        popupLot.Text = null;
                        popupCustomer.SetValue("");
                        popupCustomer.EditValue = "";
                        popupCustomer.ClearValue();
                        popupCustomer.Text = null;

                        // 해당 품목에 등록된 고객사가 있는지 판별
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("PRODUCTDEFID", row["PRODUCTDEFID"]);
                        param.Add("PRODUCTDEFVERSION", row["PRODUCTDEFVERSION"]);

                        DataTable dt = SqlExecuter.Query("GetCustomerListByProduct", "10001", param);

                        if (dt.Rows.Count == 0)
                        {
                            hasCustomer.SetDefault("N");
                            popupCustomer.Text = null;
                            popupCustomer.ClearValue();
                        } 
                        else
                        {
                            hasCustomer.SetDefault("Y");
                            popupCustomer.SetValue(Format.GetString(dt.Rows[0]["CUSTOMERID"]));
                            popupCustomer.Text = Format.GetString(dt.Rows[0]["CUSTOMERNAME"]);
                        }
                    }
                    else
                    {
                        productDefIdBox.SetDefault(null);
                        productDefVersionBox.SetDefault(null);
                        productDefIdBox2.SetDefault(null);
                        productDefVersionBox2.SetDefault(null);
                        hasCustomer.SetDefault(null);
                        popupProduct.Tag = null;
                        popupLot.SetValue("");
                        popupLot.EditValue = "";
                        popupLot.ClearValue();
                        popupLot.Text = null;
                        popupCustomer.SetValue("");
                        popupCustomer.EditValue = "";
                        popupCustomer.ClearValue();
                        popupCustomer.Text = null;                       
                    }
                }
            });

            popup.Conditions.AddTextBox("PRODUCTDEFIDNAME");

            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);

            return popup;
        }

        /// <summary>
        /// Lot 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup LotPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(1000, 800, FormBorderStyle.SizableToolWindow);
            popup.SetPopupLayout("LOT", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "LOT";
            popup.SearchQuery = new SqlQuery("GetLotIdListByReliabilityVerificationNonRegularRequest", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "LOTID";
            popup.ValueFieldName = "LOTID";
            popup.SetPopupResultCount(0);

            popup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "PLANTID", "PLANTNAME")
                .SetEmptyItem()
                .SetDefault(UserInfo.Current.Plant)
                .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                .SetLabel("PLANT");
            popup.Conditions.AddTextBox("LOTID");
            productDefIdBox = popup.Conditions.AddTextBox("PRODUCTDEFID").SetIsHidden();
            productDefVersionBox = popup.Conditions.AddTextBox("PRODUCTDEFVERSION").SetIsHidden();

            popup.GridColumns.AddTextBoxColumn("LOTID", 180)
                .SetLabel("Lot No"); // Lot ID
            popup.GridColumns.AddComboBoxColumn("LOTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetTextAlignment(TextAlignment.Center); // 양산구분
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150); // 품목코드
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 품목버전         
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200); // 품목명    
            //popup.GridColumns.AddTextBoxColumn("PROCESSDEFID", 150); // 라우팅
            //popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 120); // 공정 ID
            popup.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Center); // 순서
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 180); // 공정명
            popup.GridColumns.AddTextBoxColumn("PLANTID", 100)
                .SetTextAlignment(TextAlignment.Center); // Site ID
            //popup.GridColumns.AddTextBoxColumn("AREAID", 120); // 작업장 ID
            popup.GridColumns.AddTextBoxColumn("AREANAME", 150); // 작업장명
            popup.GridColumns.AddTextBoxColumn("RTRSHT", 100); // Roll/Sheet           
            //popup.GridColumns.AddTextBoxColumn("UNIT", 80)
            //    .SetTextAlignment(TextAlignment.Center); // 단위
            popup.GridColumns.AddSpinEditColumn("QTY", 80); // 수량
            popup.GridColumns.AddSpinEditColumn("PCSQTY", 80); // PCS 수량
            popup.GridColumns.AddSpinEditColumn("PANELQTY", 80);  // PNL 수량
            popup.GridColumns.AddSpinEditColumn("M2QTY", 80); // M2 수량
            popup.GridColumns.AddSpinEditColumn("LOTSTATE", 100)
                .SetTextAlignment(TextAlignment.Center); // Lot 상태

            return popup;
        }

        /// <summary>
        /// 저장한 Lot List 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup SelectLotPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(1000, 800, FormBorderStyle.SizableToolWindow);
            popup.SetPopupLayout("LOT", PopupButtonStyles.Ok_Cancel, false, true);
            popup.Id = "LOT";
            popup.SearchQuery = new SqlQuery("GetSaveLotId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "LOTID";
            popup.ValueFieldName = "LOTID";

            if (CurrentDataRow == null)
            {
                popup.Conditions.AddTextBox("SAVELOTID").SetIsHidden();
            }
            else
            {
                popup.Conditions.AddTextBox("SAVELOTID").SetDefault(CurrentDataRow["LOTID"]).SetIsHidden();
            }

            popup.GridColumns.AddTextBoxColumn("LOTID", 180)
                .SetLabel("Lot No"); // Lot ID
            popup.GridColumns.AddComboBoxColumn("LOTTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("PRODUCTIONTYPE")
                .SetTextAlignment(TextAlignment.Center); // 양산구분
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150); // 품목코드
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetTextAlignment(TextAlignment.Center); // 품목버전         
            popup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200); // 품목명    
            popup.GridColumns.AddTextBoxColumn("PROCESSDEFID", 150); // 라우팅
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 120); // 공정 ID
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 180); // 공정명
            popup.GridColumns.AddTextBoxColumn("USERSEQUENCE", 80)
                .SetTextAlignment(TextAlignment.Center); // 순서
            popup.GridColumns.AddTextBoxColumn("PLANTID", 120); // Site ID
            popup.GridColumns.AddTextBoxColumn("AREAID", 120); // 작업장 ID
            popup.GridColumns.AddTextBoxColumn("AREANAME", 150); // 작업장명
            popup.GridColumns.AddTextBoxColumn("RTRSHT", 100); // Roll/Sheet           
            popup.GridColumns.AddTextBoxColumn("UNIT", 80)
                .SetTextAlignment(TextAlignment.Center); // 단위
            popup.GridColumns.AddSpinEditColumn("QTY", 80); // 수량
            popup.GridColumns.AddSpinEditColumn("PCSQTY", 80); // PCS 수량
            popup.GridColumns.AddSpinEditColumn("PANELQTY", 80);  // PNL 수량
            popup.GridColumns.AddSpinEditColumn("M2QTY", 80); // M2 수량
            popup.GridColumns.AddSpinEditColumn("LOTSTATE", 100)
                .SetTextAlignment(TextAlignment.Center); // Lot 상태

            return popup;
        }

        /// <summary>
        /// 고객사 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup CutomerPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("CUSTOMER", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "CUSTOMER";
            popup.SearchQuery = new SqlQuery("GetCustomerListByChangePoint", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "CUSTOMERNAME";
            popup.ValueFieldName = "CUSTOMERID";

            popup.Conditions.AddTextBox("CUSTOMERIDNAME");
            productDefIdBox2 = popup.Conditions.AddTextBox("PRODUCTDEFID").SetIsHidden();
            productDefVersionBox2 = popup.Conditions.AddTextBox("PRODUCTDEFVERSION").SetIsHidden();
            hasCustomer = popup.Conditions.AddTextBox("HASCUSTOMER").SetIsHidden();

            popup.GridColumns.AddTextBoxColumn("CUSTOMERID", 150);
            popup.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 200);

            return popup;
        }

        /// <summary>
        /// 표준공정 팝업
        /// </summary>
        /// <returns></returns>
        private ConditionItemSelectPopup ProcessSegmentPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("PROCESSSEGMENT", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "PROCESSSEGMENT";
            popup.SearchQuery = new SqlQuery("GetSmallProcesssegmentListByQcm", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"P_ENTERPRISEID={UserInfo.Current.Enterprise}", $"P_PLANTID={UserInfo.Current.Plant}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "PROCESSSEGMENTNAME";
            popup.ValueFieldName = "PROCESSSEGMENTID";
            popup.SetPopupApplySelection((IEnumerable<DataRow> selectedRow, DataRow dataGirdRow) =>
            {
                foreach (DataRow row in selectedRow)
                {
                    if (selectedRow.Count() > 0)
                    {
                        popupProcessSegment.Tag = row["PROCESSSEGMENTVERSION"].ToString();
                    }                  
                }
            });

            popup.Conditions.AddTextBox("PROCESSSEGMENTIDNAME");

            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 220);
            popup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);

            return popup;
        }

        #endregion

        #region InitalizeDataTable

        /// <summary>
        /// Rule로 보낼 DataTable 초기화
        /// </summary>
        private void InitializeDataTable()
        {
            // 변경점 신청 내용 데이터테이블
            _changePoint = new DataTable();
            _changePoint.TableName = "changePoint";

            _changePoint.Columns.Add("CHANGEPOINTNO"); // 변경점 관리번호 
            _changePoint.Columns.Add("ENTERPRISEID"); // 회사 ID
            _changePoint.Columns.Add("PLANTID"); // Site ID
            _changePoint.Columns.Add("REQUESTDATE"); // 신청일
            _changePoint.Columns.Add("REQUESTDEPARTMENT"); // 신청부서
            _changePoint.Columns.Add("SUBJECT"); // 제목
            _changePoint.Columns.Add("PRODUCTDEFID"); // 품목 ID
            _changePoint.Columns.Add("PRODUCTDEFVERSION"); // 품목 Version
            _changePoint.Columns.Add("PROCESSSEGMENTID"); // 공정 ID
            _changePoint.Columns.Add("PROCESSSEGMENTVERSION"); // 공정 Version
            _changePoint.Columns.Add("CONSUMABLENAME"); // 자재명
            _changePoint.Columns.Add("LOTID"); // Lot No
            _changePoint.Columns.Add("STOCKSTATUS"); // 재고현황
            _changePoint.Columns.Add("SEGMENTTYPE"); // 공정분류
            _changePoint.Columns.Add("CHANGETYPE"); // 변경유형
            _changePoint.Columns.Add("APPLICATIONTYPE"); // 적용구분
            _changePoint.Columns.Add("CHANGEITEMMGNT"); // 변경품관리
            _changePoint.Columns.Add("CUSTOMERID"); // 고객사 ID
            _changePoint.Columns.Add("STOCKHANDLEMETHOD"); // 재고처리방안
            _changePoint.Columns.Add("REASONCOMMENTS"); // 변경사유
            _changePoint.Columns.Add("BEFORECOMMENTS"); // 변경전
            _changePoint.Columns.Add("AFTERCOMMENTS"); // 변경후
            _changePoint.Columns.Add("CHANGEDETAILS"); // 변경세부내용
            _changePoint.Columns.Add("CHANGEPOINTTYPE"); // 변경점구분
            _changePoint.Columns.Add("RATINGDECISION"); // 등급판정
        } 

        #endregion
    }
}
