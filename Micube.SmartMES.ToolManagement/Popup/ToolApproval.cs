#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using Micube.SmartMES.Commons.Controls;

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

namespace Micube.SmartMES.ToolManagement.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 치공구 제작수정 > 치공구 제작의뢰
    /// 업  무  설  명  : 치공구 제작의뢰시 결재를 진행
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-29
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ToolApproval : SmartPopupBaseForm, ISmartCustomPopup
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
        /// 결재 테이블
        /// </summary>
        private DataTable _approvalTable;

        /// <summary>
        /// 파일 리스트 테이블
        /// </summary>
        private DataTable _requestTable;

        string _approvalNo;
        string _requestDate;
        string _requestSequence;
        string _approvalStatus;

        public delegate void reSearchDelegator(string requestSequence, string requestDate);
        public event reSearchDelegator reSearchHandler;

        public string ApprovalNo
        {
            get { return _approvalNo; }
        }
        #endregion

        public ToolApproval(string approvalNo, string requestDate, string requestSequence, string approvalStatus):this()
        {
            _approvalNo = approvalNo;
            _requestDate = requestDate;
            _requestSequence = requestSequence;
            _approvalStatus = approvalStatus;
        }
        private ToolApproval()
        {
            InitializeComponent();

            InitializeEvent();
        }

        #region 컨텐츠 영역 초기화

        #region InitializeReviewAndApprovalGrid
        private void InitializeReviewAndApprovalGrid()
        {
        }
        #endregion

        #region InitializeFileControl - 파일업로드 컨트롤 초기화
        private void InitializeFileControl()
        {
            grdAttachment.UploadPath = "";
            grdAttachment.Resource = new ResourceInfo()
            {
                Type = "ToolRequestApproval",
                Id = _approvalNo,
                Version = "*"
            };
            grdAttachment.UseCommentsColumn = true;
        }
        #endregion

        #region InitializeData
        private void InitializeData()
        {
            // 변경점 검토내용 Data
            DataTable approvalDt = new DataTable();
            if (!_approvalNo.Equals(""))
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                param.Add("APPROVALNO", _approvalNo);

                // 결재내용 Data
                param = Commons.CommonFunction.ConvertParameter(param);
                approvalDt = SqlExecuter.Query("GetToolRequestApprovalDataByTool", "10001", param);
            }
            ucApproval.SetApproval = approvalDt;

            //파일내용
            InitializeFileControl();
            AttachmentSearch();

            _requestTable = new DataTable("ToolRequestInfo");
            _requestTable.Columns.Add("REQUESTDATE");
            _requestTable.Columns.Add("REQUESTSEQUENCE");
        }
        #endregion

        #endregion

        #region Event
        #region InitializeEvent
        public void InitializeEvent()
        {
            Shown += ToolApproval_Shown;

            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;

            ucApproval.grdApproval.View.ShowingEditor += ucApproval_grdApproval_ShowingEditor;
        }

        private void ToolApproval_Shown(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeSetting();
            InitializeComboBox();
            InitializePopup();
            InitializeDataTable();

            InitializeReviewAndApprovalGrid();
            InitializeData();
            SetReadOnly();
        }
        #endregion

        #region ucApproval_grdApproval_ShowingEditor
        /// <summary>
        /// 로그인한 유저가 해당 절차구분에서 결재를 했다면 ReadOnly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucApproval_grdApproval_ShowingEditor(object sender, CancelEventArgs e)
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
        #endregion

        #region btnClose_Click
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region btnSave_Click
        private void BtnSave_Click(object sender, EventArgs e)
        {
            string messageCode = "";
            if (ValidateApproval(out messageCode))
            {
                SettingSaveData();

                // 저장하시겠습니까?
                if (this.ShowMessageBox("InfoPopupSave", "Information", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    _approvalTable.TableName = "approval"; // 결재

                    //데이터 저장전 서버로부터 ApprovalNo를 할당
                    if (_approvalNo.Equals(""))
                    {
                        DataSet dsApproval = new DataSet();
                        dsApproval.Tables.Add(_approvalTable.Copy());

                        DataTable resultTable = ExecuteRule<DataTable>("GetToolRequestApprovalNo", dsApproval);

                        DataRow resultRow = resultTable.Rows[0];

                        //할당받은 approvalNO를 할당
                        _approvalNo = resultRow.GetString("APPROVALNO");

                        for(int i =0; i < _approvalTable.Rows.Count; i++)
                        {
                            _approvalTable.Rows[i]["APPROVALNO"] = _approvalNo;
                        }
                    }
                    #region 첨부파일 입력
                    //데이터 저장전 첨부파일의 저장을 진행
                    if (grdAttachment.Resource.Type.Equals("ToolRequestApproval"))
                    {
                        if (grdAttachment.GetChangedRows().Rows.Count > 0)
                        {
                            if (grdAttachment.Resource.Id.Equals(""))
                                grdAttachment.Resource.Id = _approvalNo;

                            grdAttachment.SaveChangedFiles();

                            DataTable changed = grdAttachment.GetChangedRows();

                            ExecuteRule("SaveObjectFile", changed);
                        }
                    }
                    #endregion

                    DataSet ds = new DataSet();
                    ds.Tables.Add(_approvalTable.Copy());
                    ds.Tables.Add(_requestTable.Copy());

                    this.ExecuteRule("ToolRequestApproval", ds);

                    reSearchHandler?.Invoke(_requestSequence, _requestDate);

                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                ShowMessage(MessageBoxButtons.OK, messageCode, "");
            }
        }
        #endregion
        #endregion

        #region Private Function

        #region ValidateApproval - 결재 상태 Validation
        private bool ValidateApproval(out string messageCode)
        {
            messageCode = "";
            //내역등록 상태이거나 Approval상태라면 데이터를 저장할 수 없다.
            if (_approvalStatus.Equals("Approval") || _approvalStatus.Equals("DetailRegist"))
            {
                messageCode = "ValidateToolRequestForApproval";
                return false;
            }

            return true;
        }
        #endregion

        #region SettingSaveData - 결재정보저장
        private void SettingSaveData()
        {
            //// 결재정보
            _approvalTable = ucApproval.grdApproval.DataSource as DataTable;

            //APPROVALNO필드가 없을 수 있음
            if (_approvalTable.Columns["APPROVALNO"] == null)
                _approvalTable.Columns.Add("APPROVALNO");

            DataRow newRow = _requestTable.NewRow();

            newRow["REQUESTDATE"] = _requestDate;
            newRow["REQUESTSEQUENCE"] = _requestSequence;

            _requestTable.Rows.Add(newRow);
        }
        #endregion

        #region GetCheckValue - 체크박스의 체크유무에 따른 Value값을 구분자 ,로 하여 string형태로 만들어준다.
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
        #endregion

        #region SetCheckValue - 조회해온 데이터를 CheckBox에 반영해준다.
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
        #endregion

        #region SetReadOnly - 상황에 따라 화면을 ReadOnly처리한다.
        private void SetReadOnly()
        {
            // 기존 데이터가 있지만 결재를 등록하지 않았다면 넘어간다.
            if (ucApproval.grdApproval.View.RowCount == 0)
            {
                return;
            }
            else
            {
                //승인 완료가 되었다면 종료한다.
                string approvalResult = "Y";

                for(int i =0; i < ucApproval.grdApproval.View.RowCount; i++)
                {
                    if(!ucApproval.grdApproval.View.GetDataRow(i).GetString("APPROVALSTATE").Equals("Approval"))
                    {
                        approvalResult = "N";
                    }
                }

                //전체가 승인되었다면
                if (approvalResult.Equals("Y"))
                {
                    ucApproval.btnADDAPPROVAL.Enabled = false;
                    grdAttachment.ButtonVisible = false;
                }
                else
                {
                    ucApproval.btnADDAPPROVAL.Enabled = true;
                    grdAttachment.ButtonVisible = true;
                }
            }
        }
        #endregion

        #region InitalizeSetting

        /// <summary>
        /// 폼 최초 세팅
        /// </summary>
        private void InitializeSetting()
        {
            grdAttachment.LanguageKey = "FILELIST";
        }

        #endregion

        #region InitalizeComboBox

        /// <summary>
        /// ComboBox 컨트롤 초기화
        /// </summary>
        private void InitializeComboBox()
        {
        }

        #endregion

        #region InitalizePopup

        /// <summary>
        /// Popup 컨트롤 초기화
        /// </summary>
        private void InitializePopup()
        {
        }

        #endregion

        #region InitalizeDataTable

        /// <summary>
        /// Rule로 보낼 DataTable 초기화
        /// </summary>
        private void InitializeDataTable()
        {
        }

        #endregion

        #region AttachmentSearch : 첨부파일검색
        /// <summary>
        /// 검색을 수행한다. 각 컨트롤에 입력된 값을 파라미터로 받아들인다.
        /// </summary>
        void AttachmentSearch()
        {
            if (!grdAttachment.Resource.Id.Equals(""))
            {
                Dictionary<string, object> Param = new Dictionary<string, object>();
                Param.Add("P_RESOURCETYPE", grdAttachment.Resource.Type);
                Param.Add("P_RESOURCEID", grdAttachment.Resource.Id);
                Param.Add("P_RESOURCEVERSION", grdAttachment.Resource.Version);

                DataTable objectFileTable = this.Procedure("usp_com_selectObjectFile", Param);

                grdAttachment.DataSource = objectFileTable;
            }
        }
        #endregion
        #endregion
    }
}
