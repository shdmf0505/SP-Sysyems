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
using System.IO;

#endregion

namespace Micube.SmartMES.Commons
{
    /// <summary>
    /// 프 로 그 램 명  : 검사결과 등록 시 Email 전송 action
    /// 업  무  설  명  : 검사결과 이메일 발송
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-12-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SendInspectionMailPopup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region Local Variables

        /// <summary>
        /// 팝업 그리드와 원래 그리드를 비교하기 위한 변수
        /// </summary>
        private DataTable _mappingDataSource = new DataTable();
        private DataTable _sendContent;
        private DataTable _sendFile;

        /// <summary>
        /// 팝업진입전 그리드의 Row
        /// </summary>
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
       //*2019-12-26 강유라 xml로 수정중
        public SendInspectionMailPopup(DataTable dt)
        {
            InitializeComponent();

            InitializeControl();
            InitializeEvent();

            _sendContent = dt.Copy();
        }

        /// <summary>
        /// 생성자
        /// </summary>
        // 2020.02.18 유석진 첨부파일 파라미터 추가
        public SendInspectionMailPopup(DataTable dt, DataTable fileDt)
        {
            InitializeComponent();

            InitializeControl();
            InitializeEvent();

            _sendContent = dt.Copy();
            _sendFile = fileDt.Copy();
        }

        public SendInspectionMailPopup()
        {
            InitializeComponent();

            InitializeControl();
            InitializeEvent();

            memoInfo.ReadOnly = false;
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 컨트롤러 초기화
        /// </summary>
        private void InitializeControl()
        {
            this.AcceptButton = btnSend;
            this.CancelButton = btnClose;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            grdUserList.GridButtonItem = GridButtonItem.None;
        }

        /// <summary>
        /// Mail로 보낼 데이터 MemoEdit에 바인딩
        /// </summary>
        private void InitializeCurrentData()
        {
            //grbIssueInformation.GridButtonItem = GridButtonItem.None;

            //txtAnalysisDate.EditValue = CurrentDataRow["ANALYSISDATE"]; // 분석일
            //txtEquipmentName.EditValue = CurrentDataRow["EQUIPMENTNAME"]; // 설비명
            //txtChemicalLevel.EditValue = CurrentDataRow["CHEMICALLEVEL"]; // 약품등급
            //txtDegree.EditValue = CurrentDataRow["DEGREE"]; // 차수
            //txtChildEquipmentName.EditValue = CurrentDataRow["CHILDEQUIPMENTNAME"]; // 설비단명
            //txtManagementScope.EditValue = CurrentDataRow["MANAGEMENTSCOPE"]; // 관리범위
            //txtProcesssegmentclassName.EditValue = CurrentDataRow["PROCESSSEGMENTCLASSNAME"]; // 대공정명
            //txtChemicalName.EditValue = CurrentDataRow["CHEMICALNAME"]; // 약품
            //txtAnalysisValue.EditValue = CurrentDataRow["ANALYSISVALUE"]; // 분석치
        }

        /// <summary>
        /// 유저 리스트 그리드 초기화
        /// </summary>
        private void InitializeUserList()
        {
            grdUserList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdUserList.View.SetIsReadOnly();

            grdUserList.View.AddTextBoxColumn("USERID", 100); // 유저 ID
            grdUserList.View.AddTextBoxColumn("USERNAME", 100); // 유저 Name
            grdUserList.View.AddTextBoxColumn("EMAILADDRESS", 220); // Email
            grdUserList.View.AddTextBoxColumn("CELLPHONENUMBER", 150); // 핸드폰

            grdUserList.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            this.Load += ChemicalissuePopup_Load;

            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            //btnSend.Click += BtnSend_Click;
            //2019-12-26 xml로 수정중
            btnSend.Click += BtnSend_Click_XML;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// 메일 송신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //*2019-12-26 xml로 수정중
        private void BtnSend_Click_XML(object sender, EventArgs e)
        {
            DataTable emailAddressdt = grdUserList.DataSource as DataTable;

            // 2020.02.28-유석진-메일보낼때 파일 첨부 기능 추가
            if (_sendFile == null)
            {
                _sendFile = new DataTable();
                DataTable fileDt = ucFileInfo.grdFileList.GetChangedRows();

                if (fileDt != null)
                {
                    _sendFile.Columns.Add("FILENAME");
                    _sendFile.Columns.Add("FILEDATA");

                    foreach(DataRow fileDr in fileDt.Rows)
                    {
                        DataRow dr = _sendFile.NewRow();
                        dr["FILENAME"] = fileDr["SAFEFILENAME"];

                        byte[] filebytes = File.ReadAllBytes(fileDr["LOCALFILEPATH"].ToString());
                        string encodedData =Convert.ToBase64String(filebytes,Base64FormattingOptions.InsertLineBreaks);
                        dr["FILEDATA"] = encodedData;

                        _sendFile.Rows.Add(dr);
                    }
                }
            }

            if (emailAddressdt.Rows.Count == 0)
            {
                throw MessageException.Create("NoRecipientEmail");
            }

            if (ShowMessage(MessageBoxButtons.YesNo, "IsSendMail").Equals(DialogResult.Yes))
            {
                // 2021.03.23 전우성 Default로 화면의 내용만 보낼 때 사용
                if(_sendContent == null)
                {
                    _sendContent = new DataTable();
                    _sendContent.Columns.Add("CONTENTS");
                    _sendContent.Columns.Add("REMARK");
                    _sendContent.Columns.Add("USERID");
                    _sendContent.Columns.Add("TITLE");
                    _sendContent.Columns.Add("INSPECTION");
                    _sendContent.Columns.Add("LANGUAGETYPE");

                    DataRow dr = _sendContent.NewRow();
                    dr["USERID"] = UserInfo.Current.Id;
                    dr["CONTENTS"] = memoInfo.EditValue.ToString().Replace("\r\n", "<br/><br/>");
                    dr["INSPECTION"] = "Default";
                    dr["LANGUAGETYPE"] = UserInfo.Current.LanguageType;
                    _sendContent.Rows.Add(dr);
                }

                _sendContent.TableName = "list";

                foreach(DataRow contentsRow in _sendContent.Rows)
                {
                    contentsRow["REMARK"] = memoCustom.EditValue;
                    contentsRow["TITLE"] = txtTitle.Text;
                }

                emailAddressdt.TableName = "list2";
                //2020-02-20 강유라 첨부파일 없을 때 에러 -> 수정
                if(_sendFile != null)
                _sendFile.TableName = "list3";

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(_sendContent.Copy());
                rullSet.Tables.Add(emailAddressdt.Copy());
                //2020-02-20 강유라 첨부파일 없을 때 에러 -> 수정
                if (_sendFile != null)
                    rullSet.Tables.Add(_sendFile.Copy());
                ExecuteRule("SendAbnormalEmail", rullSet);

                ShowMessage("SuccessSendMail");
                this.Close();
            }
        }
        

        /// <summary>
        /// 메일 송신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, EventArgs e)
        {
            DataTable dt = grdUserList.DataSource as DataTable;

            if (dt.Rows.Count == 0)
            {
                throw MessageException.Create("NoRecipientEmail");
            }

            if (ShowMessage(MessageBoxButtons.YesNo, "IsSendMail") == DialogResult.Yes)
            {
                DataTable sendTable = new DataTable();
                sendTable.TableName = "list";
                DataRow row = null;

                sendTable.Columns.Add(new DataColumn("USERID", typeof(string)));
                sendTable.Columns.Add(new DataColumn("EMAILADDRESS", typeof(string)));
                sendTable.Columns.Add(new DataColumn("TITLE", typeof(string)));
                sendTable.Columns.Add(new DataColumn("CONTENT", typeof(string)));

                foreach (DataRow dr in dt.Rows)
                {
                    row = sendTable.NewRow();
                    row["USERID"] = UserInfo.Current.Id;
                    row["EMAILADDRESS"] = dr["EMAILADDRESS"];
                    row["TITLE"] = txtTitle.EditValue;
                    row["CONTENT"] = memoInfo.EditValue.ToString().Replace("\r\n","<br/>") + "<br />"
                        + memoCustom.EditValue;

                    sendTable.Rows.Add(row);
                }

                DataSet rullSet = new DataSet();
                rullSet.Tables.Add(sendTable);
                ExecuteRule("SendMailChemicalAnalysis", rullSet);

                ShowMessage("SuccessSendMail");
                this.Close();
            }
        }

        /// <summary>
        /// 유저 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (grdUserList.View.RowCount == 0) return;

            DataTable userList = (grdUserList.DataSource as DataTable).Clone();

            for (int i = 0; i < grdUserList.View.RowCount; i++)
            {
                if (!grdUserList.View.IsRowChecked(i))
                {
                    userList.ImportRow((grdUserList.DataSource as DataTable).Rows[i]);
                }
            }

            grdUserList.View.ClearDatas();
            grdUserList.DataSource = userList;
        }

        /// <summary>
        /// 유저 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            selectUserPopup();
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

            InitializeUserList();
            InitializeCurrentData();            
        }

        #endregion

        #region Public Function
        /// <summary>
        /// 이상발생 정보를 받아와 세팅하는 함수
        /// </summary>
        /// <param name="contents"></param>
        /// 
        //*2019-12-26 xml로 수정중
        public void setTitleAndContentsDataTable()
        {           
            txtTitle.EditValue = _sendContent.Rows[0]["TITLE"].ToString();

            SetContentString(_sendContent);
        }
        
        /// <summary>
        /// 이상발생 정보를 받아와 세팅하는 함수
        /// </summary>
        /// <param name="contents"></param>
        public void setTitleAndContents(string title, string contents)
        {
            txtTitle.EditValue = title;
            memoInfo.EditValue = contents;
        }
        #endregion

        #region Private Function
        #endregion

        #region Popup

        /// <summary>
        /// 유저 선택팝업
        /// </summary>
        private void selectUserPopup()
        {
            var userPopup = this.CreateSelectPopup("USERID", new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(500, 600, System.Windows.Forms.FormBorderStyle.FixedToolWindow);

            userPopup.Conditions.AddTextBox("USERIDNAME");

            userPopup.GridColumns.AddTextBoxColumn("USERID", 120);
            userPopup.GridColumns.AddTextBoxColumn("USERNAME", 120);
            userPopup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 250); 
            userPopup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 150);

            DataTable mappingTable = grdUserList.DataSource as DataTable;
            var filter = (grdUserList.View.DataSource as DataView).RowStateFilter;

            IEnumerable<DataRow> selectedDatas = this.ShowPopup(userPopup, mappingTable.Rows.Cast<DataRow>()
                                                                                            .Where(m => !grdUserList.View.IsDeletedRow(m)));

            if (selectedDatas == null)
            {
                return;
            }
            else
            {
                if (_mappingDataSource.Columns.Count == 0) // 최초 검색했을때 변수에 담아두어 유지하고 있어야함.
                {
                    _mappingDataSource.Columns.Add("USERID", typeof(string));
                    _mappingDataSource.Columns.Add("USERNAME", typeof(string));
                    _mappingDataSource.Columns.Add("EMAILADDRESS", typeof(string));
                    _mappingDataSource.Columns.Add("CELLPHONENUMBER", typeof(string));
                }

                DataTable mdt = (grdUserList.DataSource as DataTable).Clone();
                
                foreach (DataRow row in selectedDatas)
                {
                    mdt.ImportRow(row);
                }

               (grdUserList.DataSource as DataTable).Merge(mdt, true, MissingSchemaAction.Ignore);
                grdUserList.DataSource = (grdUserList.DataSource as DataTable).DefaultView.ToTable(true);

                // DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
                // 원래것과 매핑결과를 비교하여 새로운 DataSource를 생성해 준다
                //grdUserList.SetDataSourceRemainRowStatus(DataSourceHelper.MappingChanged(_mappingDataSource, selectedDatas, new List<string>() { "USERID" }));
            }
        }

        /// <summary>
        /// popup에 메일 내용 미리 보여주는 함수
        /// </summary>
        /// <param name="sendContentDt"></param>
        private void SetContentString(DataTable sendContentDt)
        {
            string inspection = "";
            string contents = "";

            DataRow firstRow = sendContentDt.Rows[0];
            inspection = firstRow["INSPECTION"].ToString();

            switch (inspection)
            {
                case "RawInspection":
                    contents = CommonFunction.GetRawAbnormalEmailContents(firstRow);
                    break;

                case "ArrivalRawMaterialInspection":
                    contents = CommonFunction.GetRawAbnormalEmailContents(firstRow);
                    break;

                case "SubassemblyInspection":
                    contents = CommonFunction.GetSubassemblyAbnormalEmailContents(firstRow);
                    break;

                case "ProcessInspection":
                    contents = CommonFunction.GetProcessAbnormalEmailContents(firstRow);
                    break;

                case "SelfInspectionTake":
                    contents = CommonFunction.GetSelfTakeShipAbnormalEmailContents(firstRow);
                    break;

                case "SelfInspectionShip":
                    contents = CommonFunction.GetSelfTakeShipAbnormalEmailContents(firstRow);
                    break;

                case "ShipmentInspection":
                    contents = GetEmailContentsRepeat(sendContentDt, inspection);
                    break;

                case "FinishInspection":
                    contents = GetEmailContentsRepeat(sendContentDt, inspection);
                    break;

                case "ChemicalInspection":
                    contents = GetEmailContentsRepeat(sendContentDt, inspection);
                    break;

                case "OperationInspection":
                    contents = GetEmailContentsRepeat(sendContentDt, inspection);
                    break;

                case "ChemicalInspectionReanalysis":
                    contents = CommonFunction.GetChemicalReanalysisAbnormalEmailContents(firstRow);
                    break;

                case "ReliabilityRegular":
                    contents = CommonFunction.GetReliabilityRegularEmailContents(firstRow);
                    break;

                case "ReliabilityNonRegular":
                    contents = CommonFunction.GetReliabilityNonRegularEmailContents(firstRow);
                    break;

                case "ReliabilityBBT":
                    contents = CommonFunction.GetReliabilityBBTEmailContents(firstRow);
                    break;

            }

            memoInfo.EditValue = contents.Replace("<br></br>", "\r\n");
        }

        private string GetEmailContentsRepeat(DataTable sendContentDt, string inspection)
        {
            string contents = "";

            switch (inspection)
            {
                case "ShipmentInspection":
                    foreach (DataRow row in sendContentDt.Rows)
                    {
                        contents += CommonFunction.GetShipmentAbnormalEmailContents(row) + "\r\n";
                    }
                    break;

                case "FinishInspection":
                    foreach (DataRow row in sendContentDt.Rows)
                    {
                        contents += CommonFunction.GetFinishAbnormalEmailContents(row) + "\r\n";
                    }
                    break;
                case "ChemicalInspection":
                    foreach (DataRow row in sendContentDt.Rows)
                    {
                        contents += CommonFunction.GetChemicalAbnormalEmailContents(row) + "\r\n";
                    }
                    break;
                case "OperationInspection":
                    foreach (DataRow row in sendContentDt.Rows)
                    {
                        contents += CommonFunction.GetMeasureValueRegistrationContents(row) + "\r\n";
                    }
                    break;

            }

            return contents;
        }
        #endregion
    }
}
