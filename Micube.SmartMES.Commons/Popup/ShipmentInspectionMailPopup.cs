#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.Commons
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 결과등록 및 작업완료 
    /// 업  무  설  명  : 저장 시 메일 발송하기 위한 팝업
    /// 생    성    자  : 전우성
    /// 생    성    일  : 2021-03-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ShipmentInspectionMailPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// InspectionResult 테이블에 Mail Type Update하기 위한 DataTable
        /// </summary>
        private DataTable _MailTypeTable = new DataTable();

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion Local Variables

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="istype">출하검사 이력조회 true / 결과 등록 및 작업완료 false</param>
        public ShipmentInspectionMailPopup(bool istype = true)
        {
            InitializeComponent();

            InitializeControls(istype);
            InitializeLanguageKey();
            InitializeGrid();
            InitializeEvent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Controler 초기화
        /// </summary>
        private void InitializeControls(bool istype)
        {
            this.AcceptButton = btnSend;
            this.CancelButton = btnClose;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            #region Type Combo 설정

            DataTable dt = SqlExecuter.Query("GetCodeList", "00001", new Dictionary<string, object>() { { "CODECLASSID", "NGMAILTYPE" }, { "LANGUAGETYPE", UserInfo.Current.LanguageType } });

            // 출하검사 이력조회 창에서는 (istype : true) 미송부(Unsent) 항목이 삭제되고, ReTest 재검이 기본값이다
            if (istype)
            {
                dt.Rows.RemoveAt(2);

                Language.LanguageMessageItem msg = Language.GetMessage("SHIPMENTINSPECTIONRETESTTITLE", UserInfo.Current.Name);

                txtTitle.Text = msg.Title;
                memoInfo.Text = msg.Message.Replace("/r/n", Environment.NewLine);
            }

            cboMailType.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboMailType.ValueMember = "CODEID";
            cboMailType.DisplayMember = "CODENAME";
            cboMailType.EditValue = istype ? "ReTest" : "Unsent";
            cboMailType.ShowHeader = false;
            cboMailType.DataSource = dt;

            #endregion Type Combo 설정
        }

        /// <summary>
        /// Language Key 초기화
        /// </summary>
        private void InitializeLanguageKey()
        {
            this.LanguageKey = "EMAIL";
            grdUser.LanguageKey = "RECEIVERLIST";
            grdMain.LanguageKey = "LOTINFO";
            btnSend.LanguageKey = "SEND";
            btnClose.LanguageKey = "CLOSE";
            smartLayoutControl1.SetLanguageKey(layoutControlItem5, "TITLE");
            smartLayoutControl1.SetLanguageKey(layoutControlItem6, "COMMENTS");
            smartLayoutControl1.SetLanguageKey(layoutControlItem7, "REMARK");
            smartLayoutControl1.SetLanguageKey(layoutControlItem8, "TYPE");
        }

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            #region 사용자 정보

            grdUser.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
            grdUser.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdUser.View.AddTextBoxColumn("USERID", 70);
            grdUser.View.AddTextBoxColumn("USERNAME", 90);
            grdUser.View.AddTextBoxColumn("EMAILADDRESS", 180);
            grdUser.View.AddTextBoxColumn("CELLPHONENUMBER", 120);

            grdUser.View.PopulateColumns();
            grdUser.View.SetIsReadOnly();

            grdUser.ShowStatusBar = true;

            #endregion

            #region Lot 정보

            grdMain.GridButtonItem = GridButtonItem.None;
            grdMain.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

            grdMain.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            grdMain.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
            grdMain.View.AddTextBoxColumn("RESOURCEID", 180).SetLabel("LOTID");
            grdMain.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdMain.View.AddTextBoxColumn("AREANAME", 150);
            grdMain.View.AddTextBoxColumn("DEFECTITEM", 100);
            grdMain.View.AddTextBoxColumn("INSPECTIONQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdMain.View.AddTextBoxColumn("DEFECTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0", MaskTypes.Numeric);
            grdMain.View.AddTextBoxColumn("DEFECTRATE", 80);

            grdMain.View.AddTextBoxColumn("CONTENTS").SetIsHidden();
            grdMain.View.AddTextBoxColumn("TYPE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("REMARK").SetIsHidden();
            grdMain.View.AddTextBoxColumn("USERID").SetIsHidden();
            grdMain.View.AddTextBoxColumn("TITLE").SetIsHidden();
            grdMain.View.AddTextBoxColumn("INSPECTION").SetIsHidden();
            grdMain.View.AddTextBoxColumn("LANGUAGETYPE").SetIsHidden();

            grdMain.View.PopulateColumns();
            grdMain.View.SetIsReadOnly();

            grdMain.ShowStatusBar = true;

            #endregion 
        }

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            // 사용자 정보 Grid + 클릭 이벤트
            grdUser.ToolbarAddingRow += (s, e) =>
            {
                e.Cancel = true;

                var userPopup = this.CreateSelectPopup("USERID", new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                    .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                    .SetPopupResultCount(0)
                                    .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow);

                userPopup.Conditions.AddTextBox("USERIDNAME");

                userPopup.GridColumns.AddTextBoxColumn("USERID", 120);
                userPopup.GridColumns.AddTextBoxColumn("USERNAME", 120);
                userPopup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 250);
                userPopup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 150);

                if (ShowPopup(userPopup, (grdUser.DataSource as DataTable).Rows.Cast<DataRow>().Where(m => !grdUser.View.IsDeletedRow(m))) is IEnumerable<DataRow> selectedDatas)
                {
                    DataTable mdt = (grdUser.DataSource as DataTable).Clone();

                    foreach (DataRow row in selectedDatas)
                    {
                        mdt.ImportRow(row);
                    }

                   (grdUser.DataSource as DataTable).Merge(mdt, true, MissingSchemaAction.Ignore);
                    grdUser.DataSource = (grdUser.DataSource as DataTable).DefaultView.ToTable(true);
                }
            };

            // 사용자 정보 Grid - 클릭 이벤트
            grdUser.ToolbarDeletingRow += (s, e) =>
            {
                if (grdUser.View.RowCount.Equals(0))
                {
                    return;
                }

                DataTable userList = (grdUser.DataSource as DataTable).Clone();

                for (int i = 0; i < grdUser.View.RowCount; i++)
                {
                    if (!grdUser.View.IsRowChecked(i))
                    {
                        userList.ImportRow((grdUser.DataSource as DataTable).Rows[i]);
                    }
                }

                grdUser.View.ClearDatas();
                grdUser.DataSource = userList;
            };

            // 메일구분 ComboBox 값 변경 이벤트
            cboMailType.EditValueChanged += (s, e) =>
            {
                // 미송부일 경우에는 모든 Data 초기화
                if(cboMailType.EditValue.Equals("Unsent"))
                {
                    ClearData();
                    return;
                }

                string param = cboMailType.ItemIndex.Equals(0) ? "SHIPMENTINSPECTIONRETESTTITLE" : "SHIPMENTINSPECTIONRNCRTITLE";
                Language.LanguageMessageItem msg = Language.GetMessage(param, UserInfo.Current.Name);

                txtTitle.Text = msg.Title;
                memoInfo.Text = msg.Message.Replace("/r/n", Environment.NewLine);
            };

            // 보내기 버튼 클릭 이벤트
            btnSend.Click += (s, e) =>
            {
                if(cboMailType.EditValue.Equals("Unsent"))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                if(grdUser.View.DataRowCount.Equals(0))
                {
                    throw MessageException.Create("NoRecipientEmail"); // 이메일 수신자가 없습니다.
                }

                if(memoInfo.Text.Trim().Equals(string.Empty))
                {
                    throw MessageException.Create("NoCommentsMeasurement"); // 내용이 입력되지 않았습니다.
                }

                if(txtTitle.Text.Trim().Equals(string.Empty))
                {
                    throw MessageException.Create("NoTitleEmail"); // 제목을 입력하지 않았습니다.
                }

                DataSet ds = new DataSet();

                #region 내용 Table Name list

                DataTable dataDt = grdMain.DataSource as DataTable;
                dataDt.TableName = "list";

                dataDt.Rows[0]["TITLE"] = txtTitle.Text;
                dataDt.Rows[0]["TYPE"] = cboMailType.EditValue;
                dataDt.Rows[0]["REMARK"] = memoCustom.Text;
                dataDt.Rows[0]["CONTENTS"] = memoInfo.Text.Replace("\r\n", "<br/>");

                ds.Tables.Add(dataDt);

                #endregion 내용 Table Name list

                #region 사용자 정보 Table Name list2

                DataTable userDt = grdUser.DataSource as DataTable;
                userDt.TableName = "list2";
                ds.Tables.Add(userDt);

                #endregion 사용자 정보 Table Name list2

                #region 파일첨부 Table Name list3

                if (!ucFileInfo.grdFileList.GetChangedRows().Rows.Count.Equals(0))
                {
                    DataTable fileDt = new DataTable();                    
                    DataRow newRow;
                    byte[] fileByte;

                    fileDt.TableName = "list3";
                    fileDt.Columns.Add("FILENAME");
                    fileDt.Columns.Add("FILEDATA");

                    foreach (DataRow dr in ucFileInfo.grdFileList.GetChangedRows().Rows)
                    {
                        newRow = fileDt.NewRow();
                        newRow["FILENAME"] = dr["SAFEFILENAME"];

                        fileByte = File.ReadAllBytes(dr["LOCALFILEPATH"].ToString());
                        newRow["FILEDATA"] = Convert.ToBase64String(fileByte, Base64FormattingOptions.InsertLineBreaks);

                        fileDt.Rows.Add(newRow.ItemArray);
                    }

                    ds.Tables.Add(fileDt);
                }

                #endregion 파일첨부 Table Name list3

                ExecuteRule("SendAbnormalEmail", ds);

                foreach(DataRow dr in _MailTypeTable.Rows)
                {
                    dr["MAILSENDTYPE"] = cboMailType.EditValue;
                }

                ExecuteRule("SaveInspectionResultByMailType", _MailTypeTable);
                ShowMessage("SuccessSendMail");
                this.DialogResult = DialogResult.OK;
            };

            // 닫기 버튼
            btnClose.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        #endregion Event

        #region private Function

        /// <summary>
        /// Data Clear
        /// </summary>
        private void ClearData()
        {
            grdUser.View.ClearDatas();
            txtTitle.Text = string.Empty;
            memoInfo.Text = string.Empty;
            memoCustom.Text = string.Empty;
            ucFileInfo.ClearData();
        }

        #endregion private Function

        #region public Function

        /// <summary>
        /// Mail 보내는 Data 수집
        /// </summary>
        /// <param name="dt">넘겨 받은 DataTable</param>
        /// <param name="isType">출하검사 이력조회 true / 결과 등록 및 작업완료 false</param>
        public void SetMailData(DataTable dt, bool isType = true)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "TXNGROUPHISTKEY", "" },
                { "RESOURCEID", "" },
                { "DEGREE", "" },
                { "PLANTID", "" },
                { "PRODUCTDEFID", "" },
                { "PRODUCTDEFVERSION", "" },
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "USERID", UserInfo.Current.Id },
                { "AREAID", "" },
                { "AREANAME", "" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            DataTable grdMainDt = new DataTable();

            _MailTypeTable.Columns.Add("TXNHISTKEY", typeof(string));
            _MailTypeTable.Columns.Add("RESOURCEID", typeof(string));
            _MailTypeTable.Columns.Add("RESOURCETYPE", typeof(string));
            _MailTypeTable.Columns.Add("PROCESSRELNO", typeof(string));
            _MailTypeTable.Columns.Add("MAILSENDTYPE", typeof(string));

            DataRow newRow;

            foreach (DataRow dr in dt.Rows)
            {
                values["TXNGROUPHISTKEY"] = dr["TXNGROUPHISTKEY"];
                values["RESOURCEID"] = dr["RESOURCEID"];
                values["DEGREE"] = dr["DEGREE"];
                values["PLANTID"] = dr["PLANTID"];
                values["PRODUCTDEFID"] = dr["PRODUCTDEFID"];
                values["PRODUCTDEFVERSION"] = dr["PRODUCTDEFVERSION"];
                values["AREAID"] = isType ? string.Empty : dr["AREAID"];
                values["AREANAME"] = isType ? dr["FINALAREANAME"] : string.Empty;

                if (SqlExecuter.Query("SelectShipmentInspHistoryDetailByMail", "10001", values) is DataTable executerData)
                {
                    if (!executerData.Rows.Count.Equals(0))
                    {
                        grdMainDt.Merge(executerData);

                        newRow = _MailTypeTable.NewRow();
                        newRow["TXNHISTKEY"] = dr["TXNHISTKEY"];
                        newRow["RESOURCEID"] = dr["RESOURCEID"];
                        newRow["RESOURCETYPE"] = dr["RESOURCETYPE"];
                        newRow["PROCESSRELNO"] = dr["PROCESSRELNO"];
                        _MailTypeTable.Rows.Add(newRow.ItemArray);
                    }
                }

                grdMain.DataSource = grdMainDt;
            }
        }

        /// <summary>
        /// 메일 타입 보내기
        /// </summary>
        /// <returns></returns>
        public string GetMailType() => Format.GetString(cboMailType.EditValue, string.Empty);

        #endregion public Function
    }
}