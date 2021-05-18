#region using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.Framework.SmartControls;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.SmartMES.Commons.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartDeploy.Common;
using System.IO;
using System.Net;
using Micube.SmartMES.QualityAnalysis.Popup;
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 이상등록 User Control
    /// 업  무  설  명  : 품질관리에서 사용되는 이상등록 User Control이다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-06-19
    /// 수  정  이  력  : 2019-08-06 강유라
    ///                  2019-10-21 유태근
    ///                  2019-11-08 강유라 원인공정 등..추가
    ///                  2019-11-13 유태근 임시저장 체크
    ///                  2019-11-20 유태근 파일 삭제후 임시저장하면 파일삭제 안되던 오류 수정
    ///                  2019-11-22 강유라 임시저장 오류 수정
    ///                  2019-12-13 유태근 원인품목 등..수정
    ///                  2019-12-20 유태근 대책서 양식 다운로드 추가
    /// 
    /// </summary>
    public partial class IssueRegistrationControl : UserControl
    {
        #region Local Variables

        /// <summary>
        /// 임시저장값을 담을 변수
        /// </summary>
        public DataTable _requestDt;
        public DataTable _receiptDt;
        public DataTable _receiptFileDt;
        public DataTable _acceptDt;
        public DataTable _validationDt;
        public DataTable _validationFileDt;
        public int _tabIndex = 0;
        public string _queryId = "";
        public string _queryVersion = "";
        public string _inspectionType = "";

        /// <summary>
        /// 이상발생정보
        /// </summary>
        public DataRow ParentDataRow { get; set; }
        public string _state;
        public int _lastDegree = 0;
        private int? _handle = null;//승인여부를 입력할 때  rowhandle을 할당
        private bool _istempSaved = true;//임시저장 여부를 판별하는 변수
        private bool _isClosedChanged = false;//마감여부를 변경했는지 판별하는 변수

        private string _reasonConsumableDefId = "";
        private string _reasonConsumableDefVersion = "";
        private string _reasonConsumableLotId = "";
        private string _reasonSegmentId = "";
        private bool _autoChange = false;
        private string carRequestQueryId = "GetCarRequest";

        #endregion

        #region 생성자

        /// <summary>
        /// 생성자
        /// </summary>
        public IssueRegistrationControl()
        {
            InitializeComponent();

            if (!tabCarProgress.IsDesignMode())
            {               
                InitializeComboBox();

                InitializePopup();

                InitializeGrid();

                SettingDataTable();

                InitializeDefaultValue();

                InitializeEvent();

            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 그리드 초기화
        /// </summary>
        /// 
        private void InitializeGrid()
        {
            #region 승인 이력 그리드 초기화
            grdCARAccept.GridButtonItem = GridButtonItem.None;
            grdCARAccept.View.SetSortOrder("DEGREE", DevExpress.Data.ColumnSortOrder.Ascending);

            grdCARAccept.View.AddTextBoxColumn("DEGREE", 130)
                .SetIsReadOnly();
            grdCARAccept.View.AddTextBoxColumn("APPROVALDATE", 200)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetLabel("ACCEPT/REFUSEDATE");
            grdCARAccept.View.AddComboBoxColumn("ACTIONRESULT", 130, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("ISACCEPT");
            grdCARAccept.View.AddTextBoxColumn("REFUSEREASON");
            grdCARAccept.View.SetAutoFillColumn("REFUSEREASON");

            grdCARAccept.View.PopulateColumns();
            #endregion

            #region 마감이력 그리드 초기화
            grdValidationHistory.GridButtonItem = GridButtonItem.None;
            grdValidationHistory.View.SetSortOrder("DEGREE", DevExpress.Data.ColumnSortOrder.Ascending);

            grdValidationHistory.View.AddTextBoxColumn("DEGREE", 130)
                .SetIsReadOnly();
            grdValidationHistory.View.AddTextBoxColumn("CREATEDTIME", 200)
                .SetIsReadOnly()
                .SetLabel("CLOSETIME")
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");

            grdValidationHistory.View.AddComboBoxColumn("ISCLOSE", 130, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetLabel("CLAIMYN");

            grdValidationHistory.View.PopulateColumns();
            #endregion

            #region 파일 그리드 초기화
            grdFileList2.GridButtonItem = GridButtonItem.None;
            grdFileList2.View.SetSortOrder("FILEID", DevExpress.Data.ColumnSortOrder.Ascending);
            grdFileList2.View.AddTextBoxColumn("FILEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList2.View.PopulateColumns();
            #endregion
        }

        /// <summary>
        /// 로드시 컨트롤에 기본값
        /// </summary>
        private void InitializeDefaultValue()
        {
            txtRequestDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtReceiptDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txtCheckDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 탭의 순번에 따라 진행상태 조회
        /// </summary>
        private void InitializeSearchState()
        {
            int sequence;

            if (tabCarProgress.SelectedTabPageIndex == 0)
            {
                sequence = Convert.ToInt32(cboRequestNumber.EditValue);
            }
            else if (tabCarProgress.SelectedTabPageIndex == 1)
            {
                sequence = Convert.ToInt32(cboReceiptNumber.EditValue);
            }
            else if (tabCarProgress.SelectedTabPageIndex == 2)
            {
                sequence = Convert.ToInt32(cboAcceptNumber.EditValue);
            }
            else
            {
                sequence = Convert.ToInt32(cboValidationNumber.EditValue);
            }

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                //{"ABNOCRNO", ParentDataRow["ABNOCRNO"]},
                //{"ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"]},
                //{"SEQUENCE", sequence}
            };

            //DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

            //if (dt.Rows.Count == 0)
            //{
            //    _state = "";
            //}
            //else
            //{
            //    _state = dt.Rows[0]["STATE"].ToString();
            //}
        }

        /// <summary>
        /// 탭의 순번에 따라 마지막차수 조회
        /// </summary>
        private void InitializeSearchLastDegree()
        {
            int sequence;

            if (tabCarProgress.SelectedTabPageIndex == 0)
            {
                sequence = Convert.ToInt32(cboRequestNumber.EditValue);
            }
            else if (tabCarProgress.SelectedTabPageIndex == 1)
            {
                sequence = Convert.ToInt32(cboReceiptNumber.EditValue);
            }
            else if (tabCarProgress.SelectedTabPageIndex == 2)
            {
                sequence = Convert.ToInt32(cboAcceptNumber.EditValue);
            }
            else
            {
                sequence = Convert.ToInt32(cboValidationNumber.EditValue);
            }

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"ABNOCRNO", ParentDataRow["ABNOCRNO"]},
                {"ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"]},
                {"SEQUENCE", sequence}
            };

            DataTable dt = SqlExecuter.Query("GetSequenceDegree", "10001", param);

            if (dt.Rows.Count == 0)
            {
                _lastDegree = 0;
            }
            else
            {
                _lastDegree = Convert.ToInt32(dt.Rows[0]["DEGREE"]);
            }
        }

        #endregion

        #region ComboBox

        /// <summary>
        /// ComboBox 초기화
        /// </summary>
        private void InitializeComboBox()
        {
            SetNumberCombo(cboRequestNumber);
            SetNumberCombo(cboReceiptNumber);
            SetNumberCombo(cboAcceptNumber);
            SetNumberCombo(cboValidationNumber);
            SetYesNoCombo(cboIsclose);
            SetYesNoCombo(cboCheckResult);
        }

        /// <summary>
        /// 순번을 만들어주는 콤보박스 메서드
        /// </summary>
        /// <param name="con"></param>
        private void SetNumberCombo(SmartComboBox con)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CODECLASSID", "CARNumber" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            con.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            con.ValueMember = "CODEID";
            con.DisplayMember = "CODENAME";
            con.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            con.EditValue = "1";
            con.ShowHeader = false;
        }

        /// <summary>
        /// YesNo를 만들어주는 콤보박스 메서드
        /// </summary>
        /// <param name="con"></param>
        private void SetYesNoCombo(SmartComboBox con)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "CODECLASSID", "YesNo" },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            con.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            con.ValueMember = "CODEID";
            con.DisplayMember = "CODENAME";
            con.DataSource = SqlExecuter.Query("GetCodeList", "00001", param);
            //con.EditValue = "1";
            con.ShowHeader = false;
            //con.ItemIndex = 0;
        }

        #endregion

        #region Popup

        /// <summary>
        /// Popup 초기화
        /// </summary>
        private void InitializePopup()
        {
            popupManager.SelectPopupCondition = GetUserPopup();
            popupReceiptor.SelectPopupCondition = GetUserPopup();
            popupInspector.SelectPopupCondition = GetUserPopup();
        }

        /// <summary>
        /// 요청자, 담당자, 접수자 리스트를 조회하는 팝업 메서드
        /// </summary>
        private ConditionItemSelectPopup GetUserPopup()
        {
            ConditionItemSelectPopup popup = new ConditionItemSelectPopup();

            popup.SetPopupLayoutForm(450, 600, FormBorderStyle.FixedDialog);
            popup.SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, true);
            popup.Id = "USER";
            popup.SearchQuery = new SqlQuery("GetUserList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");
            popup.IsMultiGrid = false;
            popup.DisplayFieldName = "USERNAME";
            popup.ValueFieldName = "USERID";
            popup.LanguageKey = "USER";

            popup.Conditions.AddTextBox("USERIDNAME");

            popup.GridColumns.AddTextBoxColumn("USERID", 150);
            popup.GridColumns.AddTextBoxColumn("USERNAME", 200);
            popup.GridColumns.AddTextBoxColumn("DEPARTMENT", 200);
            popup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 200).SetLabel("CONTACT");

            return popup;
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.Load += IssueRegistrationControl_Load;

            tabCarProgress.SelectedPageChanged += TabCarProgress_SelectedPageChanged;

            // CAR 요청 임시 데이터 처리
            cboRequestNumber.EditValueChanged += CboRequestNumber_EditValueChanged;
            btnReqestSave.Click += BtnReqestSave_Click;
            btnRequestReset.Click += BtnRequestReset_Click;

            // CAR 접수 임시 데이터 처리
            cboReceiptNumber.EditValueChanged += CboReceiptNumber_EditValueChanged;
            btnReceiptSave.Click += BtnReceiptSave_Click;
            btnReceiptReset.Click += BtnReceiptReset_Click;

            // CAR 승인 임시 데이터 처리
            cboAcceptNumber.EditValueChanged += CboAcceptNumber_EditValueChanged;
            btnAcceptSave.Click += BtnAcceptSave_Click;
            btnAcceptReset.Click += BtnAcceptReset_Click;
            grdCARAccept.View.ShowingEditor += View_ShowingEditor;           

            // 유효성 평가 임시 데이터 처리
            dtrSearchDate.QueryCloseUp += DtrSearchDate_QueryCloseUp;
            cboValidationNumber.EditValueChanged += CboValidationNumber_EditValueChanged;
            btnValidationSave.Click += BtnValidationSave_Click;
            btnValidationReset.Click += BtnValidationReset_Click;

            //순번 콤보박스 이벤트
            cboRequestNumber.Click += CboSequenceNumber_Click;
            cboReceiptNumber.Click += CboSequenceNumber_Click;
            cboAcceptNumber.Click += CboSequenceNumber_Click;
            cboValidationNumber.Click += CboSequenceNumber_Click;

            //grdCARAccept 승인여부 바뀔 때 이벤트
            grdCARAccept.View.CellValueChanged += View_CellValueChanged;
            //cboIsclose  마감여부 바뀔 때 이벤트
            cboIsclose.EditValueChanged += CboIsclose_EditValueChanged;

            // 등록된 대책서 양식 다운로드
            btnDownload.Click += BtnDownload_Click;
        }

        /// <summary>
        /// 대책서 양식 다운로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownload_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("P_PLANTID", UserInfo.Current.Plant);
            param.Add("P_VALIDSTATE", "Valid");

            DataTable dt = SqlExecuter.Query("GetMeasureFormFileList", "10001", param);

            if (dt.Rows.Count == 0) throw MessageException.Create("ThisSiteNoVaildMeasureForm"); // 현재 Site에 유효한 대책서 양식이 등록되지 않았습니다.

            //2021-01-22 오근영 대책서 다운로드전 유효한 양식 선택 팝업 추가
            MeasureFormManagementPopup itemPopup = new MeasureFormManagementPopup("");

            // 반환된 데이터가 없을 경우 
            if (itemPopup.CurrentDataRow != null)
            {
                // 한개의 행을 반환시 팝업 자동으로 닫고 값을 반환
                if (itemPopup.CurrentDataRow.Table.Rows.Count == 1)
                {
                    itemPopup.Close();
                }
                else
                {
                    itemPopup.grdParent = dt;
                    itemPopup.ShowDialog();
                    dt = itemPopup.grdParent;
                }
            }
            else
            {
                itemPopup.grdParent = dt;
                itemPopup.ShowDialog();
                dt = itemPopup.grdParent;
            }

            FileDownload(dt, DownloadType.Single);
        }

        /// <summary>
        /// 체크한 파일들을 선택한 폴더에 다운로드
        /// </summary>
        /// <param name="files">체크한 파일 목록</param>
        /// <param name="type">다운로드 구분 (단일, 복수)</param>
        private void FileDownload(DataTable files, DownloadType type)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                switch (type)
                {
                    case DownloadType.Single:

                        files.Columns.Add("PROCESSINGSTATUS", typeof(string));

                        int totalFileSize = 0;

                        foreach (DataRow row in files.Rows)
                        {
                            row["PROCESSINGSTATUS"] = "Wait";

                            totalFileSize += Format.GetInteger(row["FILESIZE"]);
                        }

                        FileProgressDialogPopup fileProgressDialog = new FileProgressDialogPopup(files, UpDownType.Download, folderBrowserDialog.SelectedPath, totalFileSize);
                        fileProgressDialog.ShowDialog();

                        if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                            throw MessageException.Create("파일 다운로드를 취소하였습니다.");

                        break;

                    //case DownloadType.Single:
                    //    try
                    //    {
                    //        DirectoryInfo di = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                    //        FileInfo[] file = di.GetFiles("*.*");

                    //        string today = DateTime.Now.ToString("_yyyy_MM_dd_HHmmss");
                    //        string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + Format.GetString(files.Rows[0]["FILEPATH"]);
                    //        serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/");

                    //        if (file.Length != 0)
                    //        {
                    //            for (int i = 0; i < file.Length; i++)
                    //            {
                    //                if (file[i].Name.Equals(Format.GetString(files.Rows[0]["SAFEFILENAME"])))
                    //                {
                    //                    /*string oldFileName = folderBrowserDialog.SelectedPath + "/" + Format.GetString(files.Rows[0]["SAFEFILENAME"]);
                    //                    string newFileName = folderBrowserDialog.SelectedPath + "/" + Format.GetString(files.Rows[0]["FILENAME"]) + today + "." + Format.GetString(files.Rows[0]["FILEEXT"]);

                    //                    System.IO.File.Copy(oldFileName, newFileName);*/

                    //                   //DataTable dt = DeployCommonFunction.GetServerFileInfoList(serverPath + "\\" + Format.GetString(files.Rows[0]["SAFEFILENAME"]));

                    //                    WebClient Client = new WebClient();
                    //                    Client.DownloadFile(serverPath+ "\\" +Format.GetString(files.Rows[0]["SAFEFILENAME"]), @folderBrowserDialog.SelectedPath+"\\"+ Format.GetString(files.Rows[0]["FILENAME"]) + today + "." + Format.GetString(files.Rows[0]["FILEEXT"]));
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            DeployCommonFunction.DownLoadFile(serverPath, folderBrowserDialog.SelectedPath, Format.GetString(files.Rows[0]["SAFEFILENAME"]));
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        throw MessageException.Create(ex.Message);
                    //    }

                    //    break;
                }
            }
        }

        /// <summary>
        /// 원인 공정 콤보박스 값 바뀌었을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReasonSegmentId_EditValueChanged(object sender, EventArgs e)
        {
            _reasonSegmentId = cboReasonSegmentId.Editor.EditValue.ToString();
            DataTable dt = cboReasonSegmentId.Editor.DataSource as DataTable;

            var areaInfo = dt.AsEnumerable()
                .Where(r => r["PROCESSSEGMENTID"].Equals(_reasonSegmentId))
                .ToList();

            string areaId = string.Empty;
            string areaName = string.Empty;

            if (areaInfo.Count > 0)
            {
                areaId = Format.GetString(areaInfo[0]["AREAID"]);
                areaName = Format.GetString(areaInfo[0]["AREANAME"]);
            }

            SetReasonAreaIdCombo(areaId, areaName);

            /*
            _autoChange = true;
            _autoChange = false;
            */
            
        }

        /// <summary>
        /// 원인 LOTID 콤보박스 값 바뀌었을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReasonLotId_EditValueChanged(object sender, EventArgs e)
        {
            _reasonConsumableLotId = cboReasonLotId.Editor.EditValue.ToString();

            SetReasonSegmentIdCombo();

            /*2020-01-28
            _autoChange = true;
            cboReasonSegmentId.Editor.EditValue = string.Empty;
            txtReasonArea.Editor.Text = string.Empty;
            txtReasonArea.Editor.Tag = string.Empty;
            _autoChange = false;
            */

        }

        /// <summary>
        /// 원인품목 콤보박스 값 바뀌었을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReasonIdVersion_EditValueChanged(object sender, EventArgs e)
        {
            string idVersion = cboReasonIdVersion.Editor.EditValue.ToString();

            SetReasonLotIdCombo(idVersion);

            /*2020-01-28
            _autoChange = true;
            cboReasonLotId.Editor.EditValue = string.Empty;
            cboReasonSegmentId.Editor.EditValue = string.Empty;
            txtReasonArea.Editor.Text = string.Empty;
            txtReasonArea.Editor.Tag = string.Empty;
            _autoChange = false;
            */
        }

        /// <summary>
        /// 마감여부 바뀔 때 수정 여부를 변수에 할당해준다
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboIsclose_EditValueChanged(object sender, EventArgs e)
        {
            if(cboIsclose.EditValue.ToString().Equals("Y"))
            { 
                _isClosedChanged = true;

            }
        }
        /// <summary>
        /// 승인 여부가 바뀔때 rowhandle을 할당하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ACTIONRESULT")
            {
                _handle = e.RowHandle;
            }
        }

        /// <summary>
        /// 순번을 바꾸기 전에 해당 순번의 입력값이 임시저장 되어있지않으면
        /// 메시지를 보여주는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboSequenceNumber_Click(object sender, EventArgs e)
        {
            _istempSaved = true;

            switch (tabCarProgress.SelectedTabPageIndex)
            {//각각 탭
                case 0:
                    if (memoRequestDescription.ReadOnly == false)
                    {
                        if (!string.IsNullOrWhiteSpace(Format.GetString(memoRequestDescription.EditValue)))
                        {
                            CheckIsTempSaved(cboRequestNumber.EditValue.ToString(), _requestDt);
                        }
                    }
                    break;
                case 1:
                    if (memoReceiptDescription.ReadOnly == false)
                    {
                        if (!string.IsNullOrWhiteSpace(Format.GetString(memoReceiptDescription.EditValue)))
                        {
                            CheckIsTempSaved(cboReceiptNumber.EditValue.ToString(), _receiptDt);
                        }
                    }
                    break;
                case 2:
                    if (!grdCARAccept.View.GetIsReadOnly() && _handle != null)
                        CheckIsTempSaved(cboAcceptNumber.EditValue.ToString(), _acceptDt);
                    break;

                case 3:
                    if(cboIsclose.Enabled == true && _isClosedChanged == true)
                        CheckIsTempSaved(cboValidationNumber.EditValue.ToString(), _validationDt);
                    break;
            }

            if (_istempSaved == false)
            {
                MSGBox.Show(MessageBoxType.Information, "NeedToTempSave");
                return;
            }
        }

        /// <summary>
        /// CAR 승인 그리드의 마지막 차수만 수정 가능 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow row = grdCARAccept.View.GetFocusedDataRow();
            //lastDegree가 아닌경우 수정 불가
            if (_lastDegree != row["DEGREE"].ToSafeInt16())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Tab이 변할때마다 해당 상태의 순번의 차수 가져오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabCarProgress_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            InitializeSearchState();
            InitializeSearchLastDegree();
            ScreenByState(_state);
            _tabIndex = tabCarProgress.SelectedTabPageIndex;

            switch (tabCarProgress.SelectedTabPageIndex)
            {
                case 0:
                    GetSaveRequest(Convert.ToInt32(cboRequestNumber.EditValue));
                    break;
                case 1:
                    GetSaveReceipt(Convert.ToInt32(cboReceiptNumber.EditValue));
                    break;
                case 2:
                    GetSaveAccept(Convert.ToInt32(cboAcceptNumber.EditValue));
                    break;
                case 3:
                    GetSaveValidation(Convert.ToInt32(cboValidationNumber.EditValue));
                    break;
            }
        }

        /// <summary>
        /// 컨트롤 로드시 작업
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IssueRegistrationControl_Load(object sender, EventArgs e)
        {
            if (isShowReasonCombo == true)
            {
                SetReasonIdVersionCombo();
                //SetReasonLotIdCombo(null);
                //SetReasonSegmentIdCombo();
                //SetReasonAreaIdCombo();
                   
            }

            InitializeSearchState();
            InitializeSearchLastDegree();
            ScreenByState(_state);
            fileReceiptControl.LanguageKey = "ATTACHEDFILE";
            fileValidationControl.LanguageKey = "ATTACHEDFILE";


            switch (tabCarProgress.SelectedTabPageIndex)
            {
                case 0:
                    GetSaveRequest(Convert.ToInt32(cboRequestNumber.EditValue));
                    break;
                case 1:
                    GetSaveReceipt(Convert.ToInt32(cboReceiptNumber.EditValue));
                    break;
                case 2:
                    GetSaveAccept(Convert.ToInt32(cboAcceptNumber.EditValue));
                    break;
                case 3:
                    GetSaveValidation(Convert.ToInt32(cboValidationNumber.EditValue));
                    break;
            }
        }

        /// <summary>
        /// CAR 요청 임시데이터 불러오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboRequestNumber_EditValueChanged(object sender, EventArgs e)
        {
            InitializeSearchState();
            InitializeSearchLastDegree();
            ScreenByState(_state);
            GetSaveRequest(Convert.ToInt32(cboRequestNumber.EditValue));      
            _istempSaved = true;
        }

        /// <summary>
        /// CAR 요청 순번에 따른 임시저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnReqestSave_Click(object sender, EventArgs e)
        {
            SetSaveRequest(Convert.ToInt32(cboRequestNumber.EditValue));
        }

        /// <summary>
        /// CAR 요청 임시 데이터 초기화 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRequestReset_Click(object sender, EventArgs e)
        {
            ResetRequest(Convert.ToInt32(cboRequestNumber.EditValue));
        }

        /// <summary>
        /// CAR 접수 임시데이터 불러오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboReceiptNumber_EditValueChanged(object sender, EventArgs e)
        {
            InitializeSearchState();
            InitializeSearchLastDegree();
            ScreenByState(_state);
            GetSaveReceipt(Convert.ToInt32(cboReceiptNumber.EditValue));
            _istempSaved = true;
        }

        /// <summary>
        /// CAR 접수 순번에 따른 임시저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceiptSave_Click(object sender, EventArgs e)
        {
            SetSaveReceipt(Convert.ToInt32(cboReceiptNumber.EditValue));
        }

        /// <summary>
        /// CAR 접수 임시 데이터 초기화 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceiptReset_Click(object sender, EventArgs e)
        {
            ResetReceipt(Convert.ToInt32(cboReceiptNumber.EditValue));
        }

        /// <summary>
        /// CAR 승인 임시데이터 불러오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAcceptNumber_EditValueChanged(object sender, EventArgs e)
        {
            InitializeSearchState();
            InitializeSearchLastDegree();
            ScreenByState(_state);
            GetSaveAccept(Convert.ToInt32(cboAcceptNumber.EditValue));
            _istempSaved = true;
            _handle = null;
        }

        /// <summary>
        /// CAR 승인 순번에 따른 임시저장
        /// 임시저장 버튼 클릭시 승인 / 반려 일자 자동입력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAcceptSave_Click(object sender, EventArgs e)
        {
            if (_handle == null)
                return;

            int inthandle = Convert.ToInt16(_handle);
            DataRow row = grdCARAccept.View.GetDataRow(inthandle);
            //grdCARAccept.View.SetRowCellValue(inthandle, "APPROVALDATE", DateTime.Now.ToString("yyyy-MM-dd"));
            //**row["APPROVALDATE"] = DateTime.Now.ToString("yyyy-MM-dd");
            //row["APPROVALDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            row["APPROVALDATE"] = DateTime.Now;
            SetSaveAccept(Convert.ToInt32(cboAcceptNumber.EditValue));
        }

        /// <summary>
        /// CAR 승인 임시 데이터 초기화 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAcceptReset_Click(object sender, EventArgs e)
        {
            ResetAccept(Convert.ToInt32(cboAcceptNumber.EditValue));
            AcceptSearchHistroy();
        }

        /// <summary>
        /// 조회기간 컨트롤이 변할때마다 해당 조회기간의 Spec Out 갯수 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtrSearchDate_QueryCloseUp(object sender, CancelEventArgs e)
        {
            txtConcurrence.EditValue = SearchConcurrenceCount(dtrSearchDate.EditValue.ToString().Split(',')[0], dtrSearchDate.EditValue.ToString().Split(',')[1], _inspectionType, _queryId, _queryVersion);
        }

        /// <summary>
        /// 유효성 평가 임시데이터 불러오기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboValidationNumber_EditValueChanged(object sender, EventArgs e)
        {
            InitializeSearchState();
            InitializeSearchLastDegree();
            ScreenByState(_state);
            GetSaveValidation(Convert.ToInt32(cboValidationNumber.EditValue));
            _istempSaved = true;
            _isClosedChanged = false;
        }

        /// <summary>
        /// 유효성 평가 순번에 따른 임시저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValidationSave_Click(object sender, EventArgs e)
        {
            SetSaveValidation(Convert.ToInt32(cboValidationNumber.EditValue));
        }

        /// <summary>
        /// 유효성 평가 임시 데이터 초기화 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValidationReset_Click(object sender, EventArgs e)
        {
            ResetValidation(Convert.ToInt32(cboValidationNumber.EditValue));
        }

        /// <summary>
        /// 컨트롤값 초기화 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, EventArgs e)
        {
            ClearAllChildControl(tabCarProgress.SelectedTabPage);
        }

        #endregion

        #region 임시저장

        /// <summary>
        /// CAR 요청 임시저장
        /// </summary>
        private void SetSaveRequest(int index)
        {
            // 해당 순번에 저장된 데이터가 없다면 새로운 행을 생성한다.
            if (_requestDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
            {
                DataRow row;

                row = _requestDt.NewRow();

                row["SEQUENCE"] = cboRequestNumber.EditValue.ToString();
                row["CARREQUESTDATE"] = txtRequestDate.EditValue.ToString();
                row["ISREQUESTMEASURES"] = chkRequestMeasure.EditValue.ToString();
                row["CHARGERID"] = popupManager.GetValue();
                row["CHARGERNAME"] = popupManager.Text;
                row["COMMENTS"] = memoRequestDescription.Text.ToString();
                row["ABNOCRNO"] = ParentDataRow["ABNOCRNO"];
                row["ABNOCRTYPE"] = ParentDataRow["ABNOCRTYPE"];

                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;

                row["REASONCONSUMABLEDEFID"] = _reasonConsumableDefId;
                row["REASONCONSUMABLEDEFVERSION"] = _reasonConsumableDefVersion;
                row["REASONCONSUMABLELOTID"] = _reasonConsumableLotId;
                row["REASONSEGMENTID"] = _reasonSegmentId;
                row["REASONAREAID"] = txtReasonArea.Tag;
                row["REASONAREANAME"] = txtReasonArea.Text;

                _requestDt.Rows.Add(row);
            }
            // 저장된 데이터가 있다면 기존 행을 업데이트한다.
            else
            {
                DataRow row;

                row = _requestDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();

                row["SEQUENCE"] = cboRequestNumber.EditValue.ToString();
                row["CARREQUESTDATE"] = txtRequestDate.EditValue.ToString();
                row["ISREQUESTMEASURES"] = chkRequestMeasure.EditValue.ToString();
                row["CHARGERID"] = popupManager.GetValue();
                row["CHARGERNAME"] = popupManager.Text;
                row["COMMENTS"] = memoRequestDescription.Text.ToString();

                row["REASONCONSUMABLEDEFID"] = _reasonConsumableDefId;
                row["REASONCONSUMABLEDEFVERSION"] = _reasonConsumableDefVersion;
                row["REASONCONSUMABLELOTID"] = _reasonConsumableLotId;
                row["REASONSEGMENTID"] = _reasonSegmentId;
                row["REASONAREAID"] = txtReasonArea.Tag;
                row["REASONAREANAME"] = txtReasonArea.Text;
            }
        }

        /// <summary>
        /// CAR 요청데이터 가져오기
        /// </summary>
        /// <param name="index"></param>
        private void GetSaveRequest(int index)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "ABNOCRNO", ParentDataRow["ABNOCRNO"] },
                { "ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"] },
                { "SEQUENCE", cboRequestNumber.EditValue }
            };

            // 요청순번에 해당하는 데이터의 상태체크
            DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

            string state;

            if (dt.Rows.Count == 0)
            {
                state = null;
            }
            else
            {
                state = dt.Rows[0]["STATE"].ToString();
            }

            if (RequestSearch().Rows.Count == 0)
            {
                // 해당 순번에 저장된 데이터가 없다면 모든 컨트롤요소의 값을 초기화한다.
                if (_requestDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
                {
                    txtRequestDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    chkRequestMeasure.EditValue = false;
                    popupManager.SetValue(null);
                    popupManager.Text = "";
                    memoRequestDescription.EditValue = null;

                    /* 2020-01-28 다른곳에서 원인항목들 입력되는 경우있으므로 초기화 하지않음
                    cboReasonIdVersion.Editor.EditValue = string.Empty;
                    cboReasonLotId.Editor.EditValue = string.Empty;
                    cboReasonSegmentId.Editor.EditValue = string.Empty;
                    txtReasonArea.Editor.Text = string.Empty;
                    txtReasonArea.Editor.Tag = string.Empty;
                    */

                    popupManager.Refresh();
                }
                // 저장된 데이터가 있다면 저장된 데이터를 가져와서 뿌려준다.
                else
                {
                    DataRow row;

                    row = _requestDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();

                    txtRequestDate.EditValue = row["CARREQUESTDATE"].ToString();
                    chkRequestMeasure.EditValue = Convert.ToBoolean(row["ISREQUESTMEASURES"]);
                    popupManager.SetValue(row["CHARGERID"].ToString());
                    popupManager.Text = row["CHARGERNAME"].ToString();
                    memoRequestDescription.EditValue = row["COMMENTS"].ToString();

                    cboReasonIdVersion.Editor.EditValue = row["REASONCONSUMABLEDEFID"].ToString() + "|"
                          + row["REASONCONSUMABLEDEFVERSION"].ToString();
                    cboReasonLotId.Editor.EditValue = row["REASONCONSUMABLELOTID"].ToString();
                    cboReasonSegmentId.Editor.EditValue = row["REASONSEGMENTID"].ToString();
                    txtReasonArea.Editor.Text = row["REASONAREANAME"].ToString();
                    txtReasonArea.Editor.Tag = row["REASONAREAID"].ToString();

                    popupManager.Refresh();
                }
            }
        }

        /// <summary>
        /// CAR 요청 임시테이블 초기화
        /// </summary>
        private void ResetRequest(int index)
        {
            // 해당순번에 임시저장된 요청데이터가 있으면 임시저장 데이터 삭제
            if (_requestDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count != 0)
            {
                DataRow row;

                row = _requestDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();
                row.Delete();
            }

            // 해당 탭페이지 하위컨트롤 초기화
            ClearAllChildControl(tabCarProgress.SelectedTabPage);
        }

        /// <summary>
        /// CAR 접수 임시저장
        /// </summary>
        private void SetSaveReceipt(int index)
        {
            //2020-01-15 CAR 접수 임시저장시 사진등록되지않으면 사진 추가하라는 메세지
            int fileCount = (fileReceiptControl.DataSource as DataTable).Rows.Count;

            if (fileCount == 0)
            {
                MSGBox.Show(MessageBoxType.Information,"FileIsRequired");
                return;
            }


            // 해당 순번에 저장된 데이터가 없다면 새로운 행을 생성한다.
            if (_receiptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
            {
                DataRow row;

                row = _receiptDt.NewRow();

                row["SEQUENCE"] = cboReceiptNumber.EditValue.ToString();
                row["RECEIPTDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //row["ISRECEIVEMEASURES"] = chkReceiptMeasure.EditValue.ToString();
                row["RECEIPTUSER"] = popupReceiptor.GetValue();
                row["RECEIPTUSERNAME"] = popupReceiptor.Text;
                row["DESCRIPTION"] = memoReceiptDescription.Text.ToString();
                row["ABNOCRNO"] = ParentDataRow["ABNOCRNO"];
                row["ABNOCRTYPE"] = ParentDataRow["ABNOCRTYPE"];
                row["DEGREE"] = _lastDegree;
                row["PLANTID"] = Framework.UserInfo.Current.Plant;
                row["ENTERPRISEID"] = Framework.UserInfo.Current.Enterprise;

                _receiptDt.Rows.Add(row);

                if (fileReceiptControl.DataSource != null)
                {
                    _receiptFileDt.Merge(fileReceiptControl.DataSource as DataTable, true, MissingSchemaAction.Ignore);

                    for (int i = 0; i < _receiptFileDt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(_receiptFileDt.Rows[i]["NUMBER"].ToString()))
                        {
                            _receiptFileDt.Rows[i]["NUMBER"] = cboReceiptNumber.EditValue.ToString();
                            _receiptFileDt.Rows[i]["_STATE_"] = "added";
                        }
                    }

                    _receiptFileDt = _receiptFileDt.DefaultView.ToTable(true);
                }
            }
            // 저장된 데이터가 있다면 기존 행을 업데이트한다.
            else
            {
                DataRow row;

                row = _receiptDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();

                row["SEQUENCE"] = cboReceiptNumber.EditValue.ToString();
                row["RECEIPTDATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                row["RECEIPTUSER"] = popupReceiptor.GetValue();
                row["RECEIPTUSERNAME"] = popupReceiptor.Text;
                row["DESCRIPTION"] = memoReceiptDescription.Text.ToString();

                if ((fileReceiptControl.DataSource as DataTable).Rows.Count != 0)
                {
                    _receiptFileDt.Merge(fileReceiptControl.DataSource as DataTable, true, MissingSchemaAction.Ignore);

                    for (int i = 0; i < _receiptFileDt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(_receiptFileDt.Rows[i]["NUMBER"].ToString()))
                        {
                            _receiptFileDt.Rows[i]["NUMBER"] = cboReceiptNumber.EditValue.ToString();
                            _receiptFileDt.Rows[i]["_STATE_"] = "added";

                        }
                    }

                    _receiptFileDt = _receiptFileDt.DefaultView.ToTable(true);
                }
                else
                {
                    for (int i = 0; i < _receiptFileDt.Rows.Count; i++)
                    {
                        if (_receiptFileDt.Rows[i]["NUMBER"].Equals(cboReceiptNumber.EditValue.ToString()))
                        {
                            _receiptFileDt.Rows[i].Delete();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// CAR 접수데이터 가져오기
        /// </summary>
        /// <param name="index"></param>
        private void GetSaveReceipt(int index)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "ABNOCRNO", ParentDataRow["ABNOCRNO"] },
                    { "ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"] },
                    { "SEQUENCE", cboReceiptNumber.EditValue }
                };

            // 요청순번에 해당하는 데이터의 상태체크
            DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

            string state;

            if (dt.Rows.Count == 0)
            {
                state = null;
            }
            else
            {
                state = dt.Rows[0]["STATE"].ToString();
            }

            // 상태가 없으면 가져올데이터가 없다.
            if (string.IsNullOrWhiteSpace(state))
            {
                ReadOnly("CARReceivingCompleted");
                ClearAllChildControl(tabCarProgress.SelectedTabPage);
                return;
            }
            // 상태가 요청완료가 아니라면 검색한다.
            if (!state.Equals("CARRequestCompleted"))
            {
                ReceiptSearch();
                return;
            }

            // 해당 순번에 저장된 데이터가 없다면 모든 컨트롤요소의 값을 초기화한다.
            if (_receiptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
            {
                ClearAllChildControl(tabCarProgress.SelectedTabPage);
                txtReceiptDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //chkReceiptMeasure.EditValue = false;
                //popupReceiptor.SetValue(null);
                popupReceiptor.SetValue(null);
                popupReceiptor.Text = "";
                memoReceiptDescription.EditValue = null;

                fileReceiptControl.ClearData();

                popupReceiptor.Refresh();
            }
            // 저장된 데이터가 있다면 저장된 데이터를 가져와서 뿌려준다.
            else
            {
                DataRow row;

                row = _receiptDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();

                txtReceiptDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //chkReceiptMeasure.EditValue = Convert.ToBoolean(row["ISRECEIVEMEASURES"]);
                popupReceiptor.SetValue(row["RECEIPTUSER"].ToString());
                popupReceiptor.Text = row["RECEIPTUSERNAME"].ToString();
                memoReceiptDescription.EditValue = row["DESCRIPTION"].ToString();

                popupReceiptor.Refresh();

                fileReceiptControl.ClearData();

                fileReceiptControl.DataSource = _receiptFileDt.Clone();

                foreach (DataRow fileRow in _receiptFileDt.Rows.Cast<DataRow>().Where(r => r["NUMBER"].Equals(index.ToString())).ToList())
                {
                    //(fileReceipt.DataSource as DataTable).ImportRow(fileRow);
                    (fileReceiptControl.DataSource as DataTable).Rows.Add(fileRow.ItemArray);
                }
            }
        }

        /// <summary>
        /// CAR 접수 임시테이블 초기화
        /// </summary>
        private void ResetReceipt(int index)
        {
            // 해당순번에 임시저장된 접수데이터가 있으면 임시저장 데이터 삭제
            if (_receiptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count != 0)
            {
                DataRow row;

                row = _receiptDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();
                row.Delete();

                foreach (DataRow deleteRow in _receiptFileDt.AsEnumerable().Where(r => r["NUMBER"].Equals(index.ToString())).ToList())
                {
                    _receiptFileDt.Rows.Remove(deleteRow);
                }
                _receiptFileDt.AcceptChanges();
            }

            // 해당 탭페이지 하위컨트롤 초기화
            ClearAllChildControl(tabCarProgress.SelectedTabPage);
        }

        /// <summary>
        /// CAR 승인 임시저장
        /// </summary>
        private void SetSaveAccept(int index)
        {
            // 변경된 행이 없으면 저장하지 않는다.
            if (grdCARAccept.GetChangedRows().Rows.Count != 0)
            {
                DataRow dr = (grdCARAccept.DataSource as DataTable).AsEnumerable()
                                                                   .OrderByDescending(x => x.Field<string>("DEGREE"))
                                                                   .FirstOrDefault(x => x.Field<int>("SEQUENCE") == index);

                // 해당 순번에 데이터가 없으면 마지막 차수에 해당하는 Row를 Add한다.
                if (_acceptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
                {
                    _acceptDt.ImportRow(dr);
                }
                // 해당 순번에 데이터가 있으면 Update 한다.
                else
                {
                    _acceptDt.Rows.Cast<DataRow>()
                                .Where(r => r["SEQUENCE"].Equals(index.ToString()))
                                .OrderBy(r => Convert.ToInt32(r["DEGREE"]))
                                .Last()["ACTIONRESULT"] = grdCARAccept.GetChangedRows().Rows[0]["ACTIONRESULT"].ToString();
                    _acceptDt.Rows.Cast<DataRow>()
                                .Where(r => r["SEQUENCE"].Equals(index.ToString()))
                                .OrderBy(r => Convert.ToInt32(r["DEGREE"]))
                                .Last()["REFUSEREASON"] = grdCARAccept.GetChangedRows().Rows[0]["REFUSEREASON"].ToString();
                }
            }
        }

        /// <summary>
        /// CAR 승인데이터 가져오기
        /// </summary>
        private void GetSaveAccept(int index)
        {          
            // 임시저장된 데이터가 없다면 검색해온 데이터를 뿌려준다.
            if (_acceptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
            {
                AcceptSearchHistroy();
            }
            // 임시저장된 데이터가 있다면 검색해온 데이터테이블의 스키마를 복사한 후 검색해온데이터랑 임시저장테이블을 순차적으로 넣는다.
            else
            {
                AcceptSearchHistroy();

                DataTable newDt = (grdCARAccept.DataSource as DataTable).Clone();
                DataRow newRow;

                foreach (DataRow row1 in _acceptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())))
                {
                    foreach (DataRow row2 in (grdCARAccept.DataSource as DataTable).Rows)
                    {
                        if (!row1["DEGREE"].Equals(row2["DEGREE"]))
                        {
                            newRow = newDt.NewRow();
                            newRow.ItemArray = row2.ItemArray.Clone() as object[];
                            newDt.Rows.Add(newRow);
                        }                     
                    }
                }

                grdCARAccept.DataSource = newDt;
                (grdCARAccept.DataSource as DataTable).Merge(_acceptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).CopyToDataTable(), true, MissingSchemaAction.Ignore);
                (grdCARAccept.DataSource as DataTable).AcceptChanges();
            }
        }

        /// <summary>
        /// CAR 승인 임시테이블 초기화
        /// </summary>
        private void ResetAccept(int index)
        {
            // 해당순번에 임시저장된 승인데이터가 있으면 임시저장 데이터 삭제
            if (_acceptDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count != 0)
            {
                foreach (DataRow row in _acceptDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList())
                {
                    _acceptDt.Rows.Remove(row);
                }
                _acceptDt.AcceptChanges();
            }

            // 승인탭의 수정중인 Row Handle 초기화 
            _handle = null;
        }

        /// <summary>
        /// 유효성 평가 임시저장
        /// </summary>
        private void SetSaveValidation(int index)
        {
            // 해당 순번에 저장된 데이터가 없다면 새로운 행을 생성한다.
            if (_validationDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
            {
                DataRow row;

                row = _validationDt.NewRow();

                row["SEQUENCE"] = cboValidationNumber.EditValue.ToString();
                row["CHECKDATE"] = Convert.ToDateTime(txtCheckDate.EditValue).ToString("yyyy-MM-dd");
                row["CHECKUSER"] = popupInspector.GetValue().ToString();
                row["CHECKUSERNAME"] = popupInspector.Text.ToString();
                row["CHECKRESULT"] = cboCheckResult.EditValue.ToString();
                row["CHECKDETAILS"] = memoCheckDescription.Text;
                row["INQUIRYFROM"] = dtrSearchDate.EditValue.ToString().Split(',')[0];
                row["INQUIRYTO"] = dtrSearchDate.EditValue.ToString().Split(',')[1];
                row["CONCURRENCE"] = txtConcurrence.EditValue.ToString();
                row["ISCLOSE"] = cboIsclose.EditValue.ToString();
                row["ABNOCRNO"] = ParentDataRow["ABNOCRNO"];
                row["ABNOCRTYPE"] = ParentDataRow["ABNOCRTYPE"];
                row["DEGREE"] = _lastDegree;

                _validationDt.Rows.Add(row);

                _validationFileDt.Merge(fileValidationControl.DataSource as DataTable, true, MissingSchemaAction.Ignore);

                for (int i = 0; i < _validationFileDt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(_validationFileDt.Rows[i]["NUMBER"].ToString()))
                    {
                        _validationFileDt.Rows[i]["NUMBER"] = cboValidationNumber.EditValue.ToString();
                        _validationFileDt.Rows[i]["_STATE_"] = "added";
                    }
                }

                _validationFileDt = _validationFileDt.DefaultView.ToTable(true);
            }
            // 저장된 데이터가 있다면 기존 행을 업데이트한다.
            else
            {
                DataRow row;

                row = _validationDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();

                row["SEQUENCE"] = cboValidationNumber.EditValue.ToString();
                row["CHECKDATE"] = Convert.ToDateTime(txtCheckDate.EditValue).ToString("yyyy-MM-dd");
                row["CHECKUSER"] = popupInspector.GetValue().ToString();
                row["CHECKUSERNAME"] = popupInspector.Text.ToString();
                row["CHECKRESULT"] = cboCheckResult.EditValue.ToString();
                row["CHECKDETAILS"] = memoCheckDescription.Text;
                row["INQUIRYFROM"] = dtrSearchDate.EditValue.ToString().Split(',')[0];
                row["INQUIRYTO"] = dtrSearchDate.EditValue.ToString().Split(',')[1];
                row["CONCURRENCE"] = txtConcurrence.EditValue.ToString();
                row["ISCLOSE"] = cboIsclose.EditValue.ToString();

                if ((fileValidationControl.DataSource as DataTable).Rows.Count != 0)
                {
                    _validationFileDt.Merge(fileValidationControl.DataSource as DataTable, true, MissingSchemaAction.Ignore);

                    for (int i = 0; i < _validationFileDt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(_validationFileDt.Rows[i]["NUMBER"].ToString()))
                        {
                            _validationFileDt.Rows[i]["NUMBER"] = cboValidationNumber.EditValue.ToString();
                            _validationFileDt.Rows[i]["_STATE_"] = "added";
                        }
                    }

                    _validationFileDt = _validationFileDt.DefaultView.ToTable(true);
                }
                else
                {
                    for (int i = 0; i < _validationFileDt.Rows.Count; i++)
                    {
                        if (_validationFileDt.Rows[i]["NUMBER"].Equals(cboValidationNumber.EditValue.ToString()))
                        {
                            _validationFileDt.Rows[i].Delete();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 유효성 평가 가져오기
        /// </summary>
        private void GetSaveValidation(int index)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "ABNOCRNO", ParentDataRow["ABNOCRNO"] },
                { "ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"] },
                { "SEQUENCE", cboValidationNumber.EditValue }
            };

            // 유효성평가순번에 해당하는 데이터의 상태체크
            DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

            string state;

            if (dt.Rows.Count == 0)
            {
                state = null;
            }
            else
            {
                state = dt.Rows[0]["STATE"].ToString();
            }

            // 상태가 없다면 유효성평가 ReadOnly후 데이터 Clear
            if (string.IsNullOrWhiteSpace(state))
            {
                ReadOnly("ValidityEvaluationCompleted");
                ClearAllChildControl(tabCarProgress.SelectedTabPage);
                return;
            }
            // 진행상황이 승인완료상태가 아니라면 데이터검색
            if (!state.Equals("CARApprovalCompleted"))
            {
                ValidationSearch();
                return;
            }

            // 해당 순번에 저장된 데이터가 없다면 모든 컨트롤요소의 값을 초기화한다.
            if (_validationDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count == 0)
            {
                txtCheckDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //chkReceiptMeasure.EditValue = false;
      
                popupInspector.SetValue(null);
                popupInspector.Text = "";
                //cboCheckResult.ItemIndex = 0;
                memoCheckDescription.Text = null;
                //2020-01-07 주석
                txtConcurrence.EditValue = SearchConcurrenceCount(dtrSearchDate.EditValue.ToString().Split(',')[0], dtrSearchDate.EditValue.ToString().Split(',')[1], _inspectionType, _queryId, _queryVersion);
                //cboIsclose.ItemIndex = 0;

                fileValidationControl.ClearData();

                popupInspector.Refresh();
            }
            // 저장된 데이터가 있다면 저장된 데이터를 가져와서 뿌려준다.
            else
            {
                DataRow row;

                row = _validationDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();

                cboValidationNumber.EditValue = row["SEQUENCE"].ToString();
                txtCheckDate.EditValue = row["CHECKDATE"].ToString();
                popupInspector.SetValue(row["CHECKUSER"].ToString());
                popupInspector.Text = row["CHECKUSERNAME"].ToString();
                cboCheckResult.EditValue = row["CHECKRESULT"].ToString();
                memoCheckDescription.Text = row["CHECKDETAILS"].ToString();
                dtrSearchDate.EditValue = row["INQUIRYFROM"].ToString(); // 수정해야함
                dtrSearchDate.EditValue = row["INQUIRYTO"].ToString(); // 수정해야함
                txtConcurrence.EditValue = row["CONCURRENCE"].ToString();
                cboIsclose.EditValue = row["ISCLOSE"].ToString();

                popupInspector.Refresh();

                fileValidationControl.ClearData();

                fileValidationControl.DataSource = _validationFileDt.Clone();

                foreach (DataRow fileRow in _validationFileDt.Rows.Cast<DataRow>().Where(r => r["NUMBER"].Equals(index.ToString())).ToList())
                {
                    (fileValidationControl.DataSource as DataTable).Rows.Add(fileRow.ItemArray);
                }
            }
        }

        /// <summary>
        /// 유효성 평가 임시테이블 초기화
        /// </summary>
        private void ResetValidation(int index)
        {
            // 해당순번에 임시저장된 유효성평가 데이터가 있으면 임시저장 데이터 삭제
            if (_validationDt.Rows.Cast<DataRow>().Where(r => r["SEQUENCE"].Equals(index.ToString())).ToList().Count != 0)
            {
                DataRow row;

                row = _validationDt.AsEnumerable().Where(r => r["SEQUENCE"].Equals(index.ToString())).First();
                row.Delete();

                foreach (DataRow deleteRow in _validationFileDt.AsEnumerable().Where(r => r["NUMBER"].Equals(index.ToString())).ToList())
                {
                    _validationFileDt.Rows.Remove(deleteRow);
                }
                _validationFileDt.AcceptChanges();
            }

            ClearAllChildControl(tabCarProgress.SelectedTabPage);
        }

        #endregion

        #region DataTable Struct

        private void SettingDataTable()
        {
            // CAR 요청 DataTable 객체선언
            _requestDt = new DataTable();
            _requestDt.TableName = "list";

            _requestDt.Columns.Add("SEQUENCE"); // CAR 요청순번
            _requestDt.Columns.Add("CARREQUESTDATE"); // CAR 요청날짜
            _requestDt.Columns.Add("ISREQUESTMEASURES"); // 대책요청여부
            _requestDt.Columns.Add("CHARGERID"); // 담당자ID
            _requestDt.Columns.Add("CHARGERNAME"); // 담당자명
            _requestDt.Columns.Add("COMMENTS"); // 비고

            _requestDt.Columns.Add("ABNOCRNO"); // 이상발생번호
            _requestDt.Columns.Add("ABNOCRTYPE"); // 이상발생유형

            _requestDt.Columns.Add("PLANTID"); // PLANTID
            _requestDt.Columns.Add("ENTERPRISEID"); // ENTERPRISEID

            _requestDt.Columns.Add("REASONCONSUMABLEDEFID"); // REASONCONSUMABLEDEFID
            _requestDt.Columns.Add("REASONCONSUMABLEDEFVERSION"); // REASONCONSUMABLEDEFVERSION
            _requestDt.Columns.Add("REASONCONSUMABLELOTID"); // REASONCONSUMABLELOTID
            _requestDt.Columns.Add("REASONSEGMENTID"); // REASONSEGMENTID
            _requestDt.Columns.Add("REASONAREAID"); // REASONAREAID
            _requestDt.Columns.Add("REASONAREANAME"); // REASONAREANAME

            // CAR 접수 DataTable 객체선언
            _receiptDt = new DataTable();
            _receiptDt.TableName = "list";

            _receiptDt.Columns.Add("SEQUENCE"); // CAR 접수순번
            _receiptDt.Columns.Add("RECEIPTDATE"); // CAR 접수날짜
            _receiptDt.Columns.Add("ISRECEIVEMEASURES"); // 대책접수여부
            _receiptDt.Columns.Add("RECEIPTUSER"); // 접수자ID
            _receiptDt.Columns.Add("RECEIPTUSERNAME"); // 접수자명
            _receiptDt.Columns.Add("DESCRIPTION"); // 비고

            _receiptDt.Columns.Add("ABNOCRNO"); // 이상발생번호
            _receiptDt.Columns.Add("ABNOCRTYPE"); // 이상발생유형
            _receiptDt.Columns.Add("DEGREE"); // 차수

            _receiptDt.Columns.Add("PLANTID"); // PLANTID
            _receiptDt.Columns.Add("ENTERPRISEID"); // ENTERPRISEID

            // CAR 접수 파일 DataTable 객체선언
            _receiptFileDt = new DataTable();
            _receiptFileDt.TableName = "receiptFileList";

            _receiptFileDt.Columns.Add("NUMBER"); // 해당 접수 순번의 번호
            _receiptFileDt.Columns.Add("FILEID"); 
            _receiptFileDt.Columns.Add("FILENAME");
            _receiptFileDt.Columns.Add("FILEEXT");
            _receiptFileDt.Columns.Add("FILEPATH");
            _receiptFileDt.Columns.Add("SAFEFILENAME");
            _receiptFileDt.Columns.Add("FILESIZE");
            _receiptFileDt.Columns.Add("SEQUENCE");
            _receiptFileDt.Columns.Add("LOCALFILEPATH");
            _receiptFileDt.Columns.Add("RESOURCETYPE");
            _receiptFileDt.Columns.Add("RESOURCEID");
            _receiptFileDt.Columns.Add("RESOURCEVERSION");
            _receiptFileDt.Columns.Add("COMMENTS"); 
            _receiptFileDt.Columns.Add("_STATE_");

            // CAR 승인 DataTable 객체선언
            _acceptDt = new DataTable();
            _acceptDt.TableName = "list";

            _acceptDt.Columns.Add("SEQUENCE"); // CAR 승인순번
            _acceptDt.Columns.Add("DEGREE"); // 차수
            _acceptDt.Columns.Add(new DataColumn("APPROVALDATE", typeof(DateTime)));// CAR 승인날짜
            _acceptDt.Columns.Add("ACTIONRESULT"); // 승인여부
            _acceptDt.Columns.Add("REFUSEREASON"); // 반려사유

            _acceptDt.Columns.Add("ABNOCRNO"); // 이상발생번호
            _acceptDt.Columns.Add("ABNOCRTYPE"); // 이상발생유형

            _acceptDt.Columns.Add("PLANTID"); // PLANTID
            _acceptDt.Columns.Add("ENTERPRISEID"); // ENTERPRISEID

            // 유효성 평가 DataTable 객체선언
            _validationDt = new DataTable();
            _validationDt.TableName = "list";

            _validationDt.Columns.Add("SEQUENCE"); // 유효성 평가 순번
            _validationDt.Columns.Add("CHECKDATE"); // 점검일시
            _validationDt.Columns.Add("CHECKUSER"); // 점검자ID
            _validationDt.Columns.Add("CHECKUSERNAME"); // 점검자명
            _validationDt.Columns.Add("CHECKRESULT"); // 점검결과
            _validationDt.Columns.Add("CHECKDETAILS"); // 점검내용
            _validationDt.Columns.Add("INQUIRYFROM"); // 조회기간(시작)
            _validationDt.Columns.Add("INQUIRYTO"); // 조회기간(끝)
            _validationDt.Columns.Add("CONCURRENCE"); // 동시발생건
            _validationDt.Columns.Add("ISCLOSE"); // 마감여부

            _validationDt.Columns.Add("ABNOCRNO"); // 이상발생번호
            _validationDt.Columns.Add("ABNOCRTYPE"); // 이상발생유형
            _validationDt.Columns.Add("DEGREE"); // 차수

            // 유효성 평가 File DataTable 객체선언
            _validationFileDt = new DataTable();
            _validationFileDt.TableName = "validationFileList";

            _validationFileDt.Columns.Add("NUMBER");
            _validationFileDt.Columns.Add("FILEID");
            _validationFileDt.Columns.Add("FILENAME");
            _validationFileDt.Columns.Add("FILEEXT");
            _validationFileDt.Columns.Add("FILEPATH");
            _validationFileDt.Columns.Add("SAFEFILENAME");
            _validationFileDt.Columns.Add("FILESIZE");
            _validationFileDt.Columns.Add("SEQUENCE");
            _validationFileDt.Columns.Add("LOCALFILEPATH");
            _validationFileDt.Columns.Add("RESOURCETYPE");
            _validationFileDt.Columns.Add("RESOURCEID");
            _validationFileDt.Columns.Add("RESOURCEVERSION");
            _validationFileDt.Columns.Add("COMMENTS");
            _validationFileDt.Columns.Add("_STATE_");
        }

        #endregion

        #region Global Function

        /// <summary>
        /// 임시저장된 CAR 요청데이터 리턴
        /// </summary>
        /// <returns></returns>
        public DataTable RequestCarData()
        {
            return _requestDt.Copy();
        }

        /// <summary>
        /// 임시저장된 CAR 접수데이터 리턴
        /// </summary>
        /// <returns></returns>
        public DataSet ReceiptCarData()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(_receiptDt.Copy());

            foreach (DataRow addfile in _receiptFileDt.Rows)
            {
                addfile["_STATE_"] = "added";
            }

            ds.Tables.Add(_receiptFileDt.Copy());

            return ds;
        }

        /// <summary>
        /// 임시저장된 CAR 승인데이터 리턴
        /// </summary>
        /// <returns></returns>
        public DataTable AcceptCarData()
        {
            return _acceptDt.Copy();
        }

        /// <summary>
        /// 임시저장된 유효성평가 데이터 리턴
        /// </summary>
        /// <returns></returns>
        public DataSet ValidationCarData()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(_validationDt.Copy());

            foreach (DataRow addfile in _validationFileDt.Rows)
            {
                addfile["_STATE_"] = "added";
            }

            ds.Tables.Add(_validationFileDt.Copy());

            return ds;
        }

        /// <summary>
        /// CAR 요청 Search
        /// </summary>
        public DataTable RequestSearch()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"ABNOCRNO", ParentDataRow["ABNOCRNO"]},
                {"ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"]},
                {"SEQUENCE", cboRequestNumber.EditValue},
                {"LANGUAGETYPE", UserInfo.Current.LanguageType}
            };

            DataTable dt = SqlExecuter.Query(carRequestQueryId, "10001", param);

            if (dt.Rows.Count == 0 && isShowReasonCombo == true)
            {
                //메인그리드에 원인품목아이디 없으면 원인항목삭제/ 있으면 삭제하지않음
                bool isDeleteReason = string.IsNullOrWhiteSpace(Format.GetString(ParentDataRow["REASONCONSUMABLEDEFID"])) ? true : false;

                ClearAllChildControl(tabCarProgress.SelectedTabPage, isDeleteReason);
                return dt;
            }
            else if(dt.Rows.Count > 0)
            {         
                txtRequestDate.EditValue = dt.Rows[0]["CARREQUESTDATE"].ToString();
                chkRequestMeasure.EditValue = Convert.ToBoolean(dt.Rows[0]["ISREQUESTMEASURES"]);
                popupManager.SetValue(dt.Rows[0]["CHARGERID"].ToString());
                popupManager.Text = dt.Rows[0]["CHARGERNAME"].ToString();
                memoRequestDescription.EditValue = dt.Rows[0]["COMMENTS"].ToString();

                //원인 품목등..
                cboReasonIdVersion.Editor.EditValue = dt.Rows[0]["REASONCONSUMABLEDEFID"].ToString()+"|"
                                                    + dt.Rows[0]["REASONCONSUMABLEDEFVERSION"].ToString();
                cboReasonLotId.Editor.EditValue = dt.Rows[0]["REASONCONSUMABLELOTID"].ToString();
                cboReasonSegmentId.Editor.EditValue = dt.Rows[0]["REASONSEGMENTID"].ToString();
                txtReasonArea.Editor.Text = dt.Rows[0]["REASONAREANAME"].ToString();
                txtReasonArea.Editor.Tag = dt.Rows[0]["REASONAREAID"].ToString();
            }

            popupManager.Refresh();

            return dt;
        }

        /// <summary>
        /// CAR 접수 Search
        /// </summary>
        public DataTable ReceiptSearch()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"ABNOCRNO", ParentDataRow["ABNOCRNO"]},
                {"ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"]},
                {"SEQUENCE", cboReceiptNumber.EditValue},
                {"DEGREE", _lastDegree.ToString()},
                {"RESOURCEID", ParentDataRow["ABNOCRNO"].ToString() + cboReceiptNumber.EditValue.ToString() + _lastDegree.ToString() + "A"}
            };

            DataTable dt = SqlExecuter.Query("GetCarReceipt", "10001", param);
            DataTable fdt = SqlExecuter.Query("GetCarFile", "10001", param);

            if (dt.Rows.Count == 0)
            {
                UnReadOnly(_state);
                ClearAllChildControl(tabCarProgress.SelectedTabPage);
                return dt;
            }

            txtReceiptDate.EditValue = dt.Rows[0]["RECEIPTDATE"].ToString();
            //chkReceiptMeasure.EditValue = Convert.ToBoolean(dt.Rows[0]["ISRECEIVEMEASURES"]);
            popupReceiptor.SetValue(dt.Rows[0]["RECEIPTUSER"].ToString());
            popupReceiptor.Text = dt.Rows[0]["RECEIPTUSERNAME"].ToString();
            memoReceiptDescription.EditValue = dt.Rows[0]["DESCRIPTION"].ToString();
            _lastDegree = Convert.ToInt32(dt.Rows[0]["DEGREE"]);

            popupReceiptor.Refresh();

            fileReceiptControl.DataSource = fdt;

            return dt;
        }

        /// <summary>
        /// CAR 승인이력 Search
        /// </summary>
        public DataTable AcceptSearchHistroy()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"ABNOCRNO", ParentDataRow["ABNOCRNO"]},
                {"ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"]},
                {"SEQUENCE", cboAcceptNumber.EditValue},
                {"STATE", _state},
                {"DEGREE",_lastDegree },
                {"LANGUAGETYPE",Framework.UserInfo.Current.LanguageType}
            };

            DataTable dt = SqlExecuter.Query("GetCarAcceptHistory", "10001", param);

            DataTable dt2 = SqlExecuter.Query("GetCarEstimateHistory", "10001", param);

            grdCARAccept.DataSource = dt;

            grdValidationHistory.DataSource = dt2;

            grdCARAccept.Refresh();
            grdValidationHistory.Refresh();
            return dt;
        }

        /// <summary>
        /// CAR 유효성평가 Search
        /// </summary>
        public DataTable ValidationSearch()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"ABNOCRNO", ParentDataRow["ABNOCRNO"]},
                {"ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"]},
                {"SEQUENCE", cboValidationNumber.EditValue},
                {"RESOURCEID", ParentDataRow["ABNOCRNO"].ToString() + cboValidationNumber.EditValue.ToString() + _lastDegree.ToString() + "E"}
            };

            DataTable dt = SqlExecuter.Query("GetCarValidation", "10001", param);
            DataTable fdt = SqlExecuter.Query("GetCarFile", "10001", param);

            if (dt.Rows.Count == 0)
            {
                ClearAllChildControl(tabCarProgress.SelectedTabPage);
                return dt;
            }

            txtCheckDate.EditValue = dt.Rows[0]["CHECKDATE"].ToString();
            popupInspector.SetValue(dt.Rows[0]["CHECKUSER"].ToString());
            popupInspector.Text = dt.Rows[0]["CHECKUSERNAME"].ToString();
            cboCheckResult.EditValue = dt.Rows[0]["CHECKRESULT"].ToString();
            memoCheckDescription.Text = dt.Rows[0]["CHECKDETAILS"].ToString();
            dtrSearchDate.EditValue = dt.Rows[0]["INQUIRYFROM"].ToString() + "," + dt.Rows[0]["INQUIRYTO"].ToString();
            txtConcurrence.EditValue = dt.Rows[0]["CONCURRENCE"].ToString();
            cboIsclose.EditValue = dt.Rows[0]["ISCLOSE"].ToString();

            popupInspector.Refresh();

            fileValidationControl.DataSource = fdt;

            return dt;
        }

        /// <summary>
        /// 상태값을 받아와서 해당 탭 컨트롤 요소들 ReadOnly
        /// </summary>
        public void ReadOnly(string state)
        {
            switch (state)
            {
                case "CARRequestCompleted":
                    chkRequestMeasure.ReadOnly = true;
                    popupManager.ReadOnly = true;
                    popupManager.Enabled = false;
                    memoRequestDescription.ReadOnly = true;
                    cboReasonIdVersion.Editor.ReadOnly = true;
                    cboReasonLotId.Editor.ReadOnly = true;
                    cboReasonSegmentId.Editor.ReadOnly = true;
                    btnReqestSave.Hide();
                    btnRequestReset.Hide();
                    break;

                case "CARReceivingCompleted":
                    //chkReceiptMeasure.ReadOnly = true;
                    popupReceiptor.ReadOnly = true;
                    popupReceiptor.Enabled = false;
                    memoReceiptDescription.ReadOnly = true;
                    fileReceiptControl.ButtonVisibleDownload();
                    //fileReceiptControl.grdFileList.View.SetIsReadOnly(true);
                    btnReceiptSave.Hide();
                    btnReceiptReset.Hide();                  
                    break;

                case "CARApprovalCompleted":
                    grdCARAccept.View.SetIsReadOnly();
                    btnAcceptSave.Hide();
                    btnAcceptReset.Hide();
                    break;

                case "ValidityEvaluationCompleted":
                    popupInspector.ReadOnly = true;
                    popupInspector.Enabled = false;
                    cboCheckResult.Enabled = false;
                    memoCheckDescription.ReadOnly = true;
                    dtrSearchDate.Enabled = false;
                    cboIsclose.Enabled = false;
                    fileReceiptControl.ButtonVisibleDownload();
                    btnValidationSave.Hide();
                    btnValidationReset.Hide();
                    break;

                default:

                    break;
            }
        }

        /// <summary>
        /// 상태값을 받아와서 해당 탭 컨트롤 요소들 UnReadOnly
        /// </summary>
        public void UnReadOnly(string state)
        {
            switch (state)
            {
                case "CARRequestCompleted":
                    chkRequestMeasure.ReadOnly = false;
                    popupManager.ReadOnly = false;
                    popupManager.Enabled = true;
                    memoRequestDescription.ReadOnly = false;
                    cboReasonIdVersion.Editor.ReadOnly = false;
                    cboReasonLotId.Editor.ReadOnly = false;
                    cboReasonSegmentId.Editor.ReadOnly = false;
                    btnReqestSave.Show();
                    btnRequestReset.Show();
                    break;

                case "CARReceivingCompleted":
                    //chkReceiptMeasure.ReadOnly = false;
                    popupReceiptor.ReadOnly = false;
                    popupReceiptor.Enabled = true;
                    memoReceiptDescription.ReadOnly = false;
                    fileReceiptControl.ButtonVisibleAll();
                    fileReceiptControl.grdFileList.View.SetIsReadOnly(false);
                    btnReceiptSave.Show();
                    btnReceiptReset.Show();
                    break;

                case "CARApprovalCompleted":
                    grdCARAccept.View.SetIsReadOnly(false);
                    btnAcceptSave.Show();
                    btnAcceptReset.Show();
                    break;

                case "ValidityEvaluationCompleted":
                    popupInspector.ReadOnly = false;
                    popupInspector.Enabled = true;
                    cboCheckResult.Enabled = true;
                    memoCheckDescription.ReadOnly = false;
                    dtrSearchDate.Enabled = true;
                    cboIsclose.Enabled = true;
                    fileValidationControl.ButtonVisibleAll();
                    btnValidationSave.Show();
                    btnValidationReset.Show();
                    break;

                default:

                    break;
            }
        }

        /// <summary>
        /// 상태값을 받아와서 저장된 데이터가 있는지 검사
        /// </summary>
        /// <param name="state">진행상태</param>
        /// <returns></returns>
        public Boolean SaveFlag(int tabPageIndex)
        {
            Boolean flag = false;

            switch (tabPageIndex)
            {
                // 요청저장
                case 0:
                    if (memoRequestDescription.ReadOnly == true) flag = true;
                    break;
                // 접수저장
                case 1:
                    if (memoReceiptDescription.ReadOnly == true) flag = true;                   
                    break;
                // 승인저장
                case 2:
                    if (grdCARAccept.View.GetIsReadOnly() == true) flag = true;                  
                    break;
                // 유효성저장
                case 3:
                    if (memoCheckDescription.ReadOnly == true) flag = true;              
                    break;
            }

            return flag;
        }

        /// <summary>
        /// 원인콤보 Visible 설정
        /// </summary>
        public void SetControlsVisible()
        {
            if (isShowReasonCombo == true)
            {
                tblReason.Visible = true;
            }
            else
            {
                tblReason.Visible = false;
            }
        }

        /// <summary>
        /// abnormalType 타입에 따라 다른 CarRequest 죄회쿼리를 설정하는 함수
        /// </summary>
        public void SetCarRequestQueryId()
        {
            if (abnormalType.Equals("SelfInspectionTake") ||
               abnormalType.Equals("SelfInspectionShip") ||
               abnormalType.Equals("FinishInspection") ||
               abnormalType.Equals("AOIInspection") ||
               abnormalType.Equals("ShipmentInspection"))
            {
                carRequestQueryId = "GetCarRequestAbnormal";
            }
            else
            {
                carRequestQueryId = "GetCarRequest";
            }
        }
        #endregion

        #region Private Function

        /// <summary>
        /// 원인 품목 콤보박스를 초기화 하는 함수
        /// </summary>
        private void SetReasonIdVersionCombo()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "LOTID",ParentDataRow["LOTID"] }
            };

            DataTable dt = SqlExecuter.Query("GetReasonConsumableList", "10002", param);

            cboReasonIdVersion.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.Custom;
            cboReasonIdVersion.Editor.PopupWidth = 600;
            cboReasonIdVersion.Editor.SetVisibleColumns("CONSUMABLEDEFID", "CONSUMABLEDEFVERSION", "CONSUMABLEDEFNAME", "MATERIALTYPE");
            cboReasonIdVersion.Editor.SetVisibleColumnsWidth(90, 70, 200, 80);
            cboReasonIdVersion.Editor.ShowHeader = true;

            cboReasonIdVersion.Editor.DisplayMember = "CONSUMABLEDEFNAME";
            cboReasonIdVersion.Editor.ValueMember = "SPLITCONSUMABLEDEFIDVERSION";
            cboReasonIdVersion.Editor.DataSource = dt;
            cboReasonIdVersion.Editor.UseEmptyItem = true;
            

            //원인 품목, 원인LOTID, 원인 공정, 원인 작업장 콤보 이벤트
            cboReasonIdVersion.Editor.EditValueChanged += ReasonIdVersion_EditValueChanged;
            cboReasonLotId.Editor.EditValueChanged += ReasonLotId_EditValueChanged;
            cboReasonSegmentId.Editor.EditValueChanged += ReasonSegmentId_EditValueChanged;

            //2020-01-28 메인그리드 원인 항목 바인딩
            setReason();

            //2020-01-28 원인lotID의 데이터 소스가 바뀔때 원인공정, 원인작업장 초기화
            cboReasonLotId.Editor.ListChanged += Editor_ReasonLotId_ListChanged;
        }

        private void Editor_ReasonLotId_ListChanged(object sender, ListChangedEventArgs e)
        {
            cboReasonLotId.Editor.EditValueChanged -= ReasonLotId_EditValueChanged;
            cboReasonSegmentId.Editor.EditValueChanged -= ReasonSegmentId_EditValueChanged;

            cboReasonLotId.Editor.EditValue = string.Empty;
            cboReasonSegmentId.Editor.EditValue = string.Empty;
            txtReasonArea.Editor.Text = string.Empty;
            txtReasonArea.Editor.Tag = string.Empty;

            cboReasonLotId.Editor.EditValueChanged += ReasonLotId_EditValueChanged;
            cboReasonSegmentId.Editor.EditValueChanged += ReasonSegmentId_EditValueChanged;
        }

        /// <summary>
        /// 원인 LOT 콤보박스를 초기화 하는 함수
        /// </summary>
        private void SetReasonLotIdCombo(string idVersion)
        {
            if(idVersion != null && !string.IsNullOrWhiteSpace(idVersion))
            { 
                _reasonConsumableDefId = idVersion.Split('|')[0];
                _reasonConsumableDefVersion = idVersion.Split('|')[1];
            }

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "LOTID", ParentDataRow["LOTID"] },
                { "REASONCONSUMABLEDEFID", _reasonConsumableDefId },
                { "REASONCONSUMABLEDEFVERSION", _reasonConsumableDefVersion },
            };

            DataTable dt = SqlExecuter.Query("GetDefectReasonConsumableLot", "10002", param);

            cboReasonLotId.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboReasonLotId.Editor.ShowHeader = true;

            cboReasonLotId.Editor.DisplayMember = "CONSUMABLELOTID";
            cboReasonLotId.Editor.ValueMember = "CONSUMABLELOTID";
            cboReasonLotId.Editor.DataSource = dt;
            cboReasonLotId.Editor.UseEmptyItem = true;
            
        }

        /// <summary>
        /// 원인 공정 콤보박스를 초기화 하는 함수
        /// </summary>
        private void SetReasonSegmentIdCombo()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "LOTID", ParentDataRow["LOTID"] },
                { "REASONCONSUMABLEDEFID", _reasonConsumableDefId },
                { "REASONCONSUMABLEDEFVERSION", _reasonConsumableDefVersion },
                { "REASONCONSUMABLELOTID", _reasonConsumableLotId }
            };

            DataTable dt = SqlExecuter.Query("GetDefectReasonProcesssegment", "10002", param);

            cboReasonSegmentId.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberAndValueMember;
            cboReasonSegmentId.Editor.ShowHeader = false;

            cboReasonSegmentId.Editor.DisplayMember = "PROCESSSEGMENTNAME";
            cboReasonSegmentId.Editor.ValueMember = "PROCESSSEGMENTID";
            cboReasonSegmentId.Editor.DataSource = dt;
            cboReasonSegmentId.Editor.UseEmptyItem = true;
            

        }

        /// <summary>
        /// 원인 작업장 텍스트박스에 바인딩해주는 함수
        /// </summary>
        private void SetReasonAreaIdCombo(string areaId, string areaName)
        {
            txtReasonArea.Text = areaName;
            txtReasonArea.Tag = areaId;
        }

        /// <summary>
        /// 상태값에 따른 화면처리
        /// </summary>
        private void ScreenByState(string state)
        {
            // 요청탭일때
            if (tabCarProgress.SelectedTabPage == tpgCarReqeust)
            {
                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "ABNOCRNO", ParentDataRow["ABNOCRNO"] },
                    { "ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"] },
                    { "SEQUENCE", cboRequestNumber.EditValue }
                };

                // 요청순번에 해당하는 데이터의 상태체크
                DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

                if (dt.Rows.Count == 0)
                {
                    state = null;
                }
                else
                {
                    state = dt.Rows[0]["STATE"].ToString();
                }

                // 상태값이 없으면 요청탭 입력이 가능해야한다.
                if (string.IsNullOrEmpty(state))
                {
                    UnReadOnly("CARRequestCompleted");
                    ClearAllChildControl(tabCarProgress.SelectedTabPage);
                    popupManager.SetValue(null);
                    popupManager.Text = "";
                }
                // 첫번째 상태값이 있다면
                else
                {
                    ReadOnly("CARRequestCompleted");
                    RequestSearch();
                }
            }

            // 접수탭일때
            else if (tabCarProgress.SelectedTabPage == tpgCarReceipt)
            {
                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "ABNOCRNO", ParentDataRow["ABNOCRNO"] },
                    { "ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"] },
                    { "SEQUENCE", cboReceiptNumber.EditValue }
                };

                // 접수순번에 해당하는 데이터의 상태체크
                DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

                if (dt.Rows.Count == 0)
                {
                    state = null;
                }
                else
                {
                    state = dt.Rows[0]["STATE"].ToString();
                }

                // 상태값이 없으면 접수탭 ReadOnly
                if (string.IsNullOrEmpty(state))
                {
                    ReadOnly("CARReceivingCompleted");
                }
                // 상태값이 요청완료라면 UnReadOnly
                else if (state.Equals("CARRequestCompleted"))
                {
                    UnReadOnly("CARReceivingCompleted");
                    ClearAllChildControl(tabCarProgress.SelectedTabPage);
                    popupReceiptor.SetValue(null);
                    popupReceiptor.Text = "";
                }
                // 상태값이 접수완료, 승인완료, 유효성평가완료라면 ReadOnly후 데이터검색
                else
                {
                    ReadOnly("CARReceivingCompleted");
                    ReceiptSearch();
                }
            }

            // 승인탭일때
            else if (tabCarProgress.SelectedTabPage == tpgAccept)
            {
                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "ABNOCRNO", ParentDataRow["ABNOCRNO"] },
                    { "ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"] },
                    { "SEQUENCE", cboAcceptNumber.EditValue }
                };

                // 승인순번에 해당하는 데이터의 상태체크
                DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

                if (dt.Rows.Count == 0)
                {
                    state = null;
                }
                else
                {
                    state = dt.Rows[0]["STATE"].ToString();
                }

                // 상태값이 없으면 승인탭 ReadOnly
                if (string.IsNullOrEmpty(state))
                {
                    ReadOnly("CARApprovalCompleted");
                }
                // 상태값이 접수완료라면 UnReadOnly
                else if (state.Equals("CARReceivingCompleted"))
                {
                    UnReadOnly("CARApprovalCompleted");
                    AcceptSearchHistroy();
                }
                // 상태값이 요청완료, 접수완료, 유효성평가완료라면 ReadOnly후 데이터검색
                else
                {
                    ReadOnly("CARApprovalCompleted");
                    AcceptSearchHistroy();
                }
            }

            // 유효성탭일때
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "ABNOCRNO", ParentDataRow["ABNOCRNO"] },
                    { "ABNOCRTYPE", ParentDataRow["ABNOCRTYPE"] },
                    { "SEQUENCE", cboValidationNumber.EditValue }
                };

                // 유효성순번에 해당하는 데이터의 상태체크
                DataTable dt = SqlExecuter.Query("GetSequenceState", "10001", param);

                if (dt.Rows.Count == 0)
                {
                    state = null;
                }
                else
                {
                    state = dt.Rows[0]["STATE"].ToString();
                }

                // 상태값이 없으면 유효성탭 ReadOnly
                if (string.IsNullOrEmpty(state))
                {
                    ReadOnly("ValidityEvaluationCompleted");
                }
                // 상태값이 승인완료라면 유효성탭 UnReadOnly후 Clear
                else if (state.Equals("CARApprovalCompleted"))
                {
                    UnReadOnly("ValidityEvaluationCompleted");
                    ClearAllChildControl(tabCarProgress.SelectedTabPage);
                }
                // 상태값이 요청완료, 접수완료, 유효성평가완료라면 ReadOnly후 데이터검색
                else
                {
                    ReadOnly("ValidityEvaluationCompleted");
                    ValidationSearch();
                }
            }
        }

        /// <summary>
        /// 해당 기간의 이상발생 갯수 조회
        /// </summary>
        /// <returns></returns>
        private string SearchConcurrenceCount(string fromDate, string toDate, string inspectionType, string queryId, string queryVersion)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"FROMDATE", fromDate},
                {"TODATE", toDate},
                {"RESOURCETYPE", inspectionType}
            };

            DataTable dt = SqlExecuter.Query(queryId, queryVersion, param);

            return dt.Rows[0]["COUNT"].ToString();
        }

        /// <summary>
        /// 탭페이지 기준으로 하위 컨트롤 초기화
        /// </summary>
        /// <param name="con"></param>
        private void ClearAllChildControl(Control con, bool isDeleteReason = false)
        {
            if (con is SmartLabelComboBox && isDeleteReason == true)
            {
                ((SmartLabelComboBox)con).Editor.EditValue = string.Empty;
            }
            else if (con is SmartComboBox && !(con is SmartLabelComboBox))
            {
                if (!string.IsNullOrWhiteSpace(con.Name) && !con.Name.Contains("Number"))
                {
                   ((SmartComboBox)con).ItemIndex = 0;
                }
            }

            else if (con is SmartCheckBox)
            {
                ((SmartCheckBox)con).Checked = false;
            }
            else if (con is SmartSelectPopupEdit)
            {
                //((SmartSelectPopupEdit)con).ClearValue();
                //((SmartSelectPopupEdit)con).ToolTip = null;
                //((SmartSelectPopupEdit)con).Refresh();
            }
            else if (con is SmartMemoEdit)
            {
                ((SmartMemoEdit)con).ResetText();
            }
            else if (con is SmartFileProcessingControl)
            {
                ((SmartFileProcessingControl)con).ClearData();
            }
            else if (con is SmartBandedGrid)
            {
                ((SmartBandedGrid)con).DataSource = null;
            }
            else if (con is SmartDateRangeEdit)
            {
                ((SmartDateRangeEdit)con).Reset();
            }

            for (int i = 0; i < con.Controls.Count; i++)
            {
                if (con is SmartFileProcessingControl) continue;
                ClearAllChildControl(con.Controls[i], isDeleteReason);
            }
        }

        /// <summary>
        /// 기존에 등록된 원인 항목들은 바인딩하는 함수
        /// </summary>
        private void setReason()
        {
            cboReasonIdVersion.Editor.EditValue = ParentDataRow["REASONCONSUMABLEDEFID"] + "|" + ParentDataRow["REASONCONSUMABLEDEFVERSION"];
            cboReasonLotId.Editor.EditValue = ParentDataRow["REASONCONSUMABLELOTID"];
            cboReasonSegmentId.Editor.EditValue = ParentDataRow["REASONSEGMENTID"];
        }

        /// <summary>
        /// CAR접수 파일을 저장하는 함수
        /// </summary>
        public void SaveReceiptFile()
        {

            fileReceiptControl.DataSource = _receiptFileDt;
            fileReceiptControl.SaveAddedFiles();
        }

        /// <summary>
        /// CAR유효성 파일을 저장하는 함수
        /// </summary>
        public void SaveValidationFile()
        {

            fileValidationControl.DataSource = _validationFileDt;
            fileValidationControl.SaveAddedFiles();
        }
        /// <summary>
        /// 선택된 탭 마다 해당 순번과 임시저장 테이블을 파라미터로받아
        /// 임시저장 되었는지 체크하는 함수
        /// </summary>
        public bool CheckIsTempSaved(string sequence, DataTable table)
        {
            var hasSequence = table.Rows.Cast<DataRow>()
                .Where(r => r["SEQUENCE"].ToString().Equals(sequence)).ToList();

            _istempSaved = hasSequence.Count == 0 ? false : true;

            return _istempSaved;
        }

        /// <summary>
        /// 입력중인 데이터가 있는지 확인
        /// </summary>
        /// <param name="tapPageIndex"></param>
        /// <returns></returns>
        public bool IsInputData(int tapPageIndex)
        {
            bool flag = false;

            //활성화 되어있을때 체크
            // 0:요청, 1:접수, 2:승인, 3:유효성평가
            switch (tapPageIndex)
            {
                case 0:
                    if ((memoRequestDescription.ReadOnly == false && !string.IsNullOrWhiteSpace(Format.GetString(memoRequestDescription.Text)))
                        || (!cboReasonIdVersion.Editor.ReadOnly && !string.IsNullOrWhiteSpace(Format.GetString(cboReasonIdVersion.GetValue())))
                        || (!cboReasonLotId.Editor.ReadOnly && !string.IsNullOrWhiteSpace(Format.GetString(cboReasonLotId.GetValue())))
                        || (!cboReasonSegmentId.Editor.ReadOnly && !string.IsNullOrWhiteSpace(Format.GetString(cboReasonSegmentId.GetValue())))
                        || (popupManager.ReadOnly == false && !string.IsNullOrWhiteSpace(Format.GetString(popupManager.GetValue()))))
                    {
                        flag = true;                    
                    }
                    break;
                case 1:
                    if (memoReceiptDescription.ReadOnly == false && (!string.IsNullOrWhiteSpace(Format.GetString(memoReceiptDescription.Text)) || (fileReceiptControl.DataSource as DataTable).Rows.Count != 0)
                        || (popupReceiptor.ReadOnly == false && !string.IsNullOrWhiteSpace(Format.GetString(popupReceiptor.GetValue()))))
                    {
                        flag = true;
                    }
                    break;
                case 2:
                    if (grdCARAccept.GetChangedRows().Rows.Count != 0)
                    {
                        flag = true;
                    }
                    break;
                case 3:
                    if (cboIsclose.Enabled == false || cboCheckResult.Enabled == false)
                    {
                        flag = false;
                    }
                    else if (memoCheckDescription.ReadOnly == false && (!string.IsNullOrWhiteSpace(Format.GetString(memoCheckDescription.Text)) || (fileValidationControl.DataSource as DataTable).Rows.Count != 0)
                             || (popupInspector.ReadOnly == false && !string.IsNullOrWhiteSpace(Format.GetString(popupInspector.GetValue())))
                             || (cboCheckResult.Enabled == true && !string.IsNullOrWhiteSpace(Format.GetString(cboCheckResult.EditValue)))
                             || (cboIsclose.Enabled == true && !string.IsNullOrWhiteSpace(Format.GetString(cboIsclose.EditValue))))
                    {
                        flag = true;
                    }
                    break;
            }

            return flag;
        }

        /// <summary>
        /// 2020-01-07 강유라
        /// _tabIndex 값을 세팅 하는 함수
        /// </summary>
        /// <param name="tabIndex"></param>
        public void setTabIndex(int tabIndex)
        {
            _tabIndex = tabIndex;
        }
        #endregion

        #region Property

        /// <summary>
        /// User Control이 사용되는 화면 구분
        /// </summary>
        public bool isShowReasonCombo { get; set; }

        [Browsable(false)]
        public bool ReasonTableVisible
        {
            get
            {
                return tblReason.Visible;
            }
            set
            {
                tblReason.Visible = value;
            }
        }

        /// <summary>
        /// abnormalType 에 의해 다른 쿼리 조회 
        /// </summary>
        public string abnormalType { get; set; }
        #endregion

    }

}
