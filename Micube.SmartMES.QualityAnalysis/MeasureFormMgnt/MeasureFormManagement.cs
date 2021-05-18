#region using

using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
using SmartDeploy.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 대책서 관리 > 대책서 양식 관리
    /// 업  무  설  명  : 이상발생에서 사용되는 대책서를 Site별로 관리한다.
    /// 생    성    자  : 유태근
    /// 생    성    일  : 2019-12-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MeasureFormManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public MeasureFormManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
            InitializeGrid();
        }

        /// <summary>
        /// File List 그리드 정보를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdFileList.GridButtonItem = GridButtonItem.Export;
            grdFileList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdFileList.View.SetSortOrder("SEQUENCE");

            grdFileList.View.AddTextBoxColumn("FILEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILENAME", 300)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEEXT", 100)
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("FILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("SAFEFILENAME", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("FILESIZE", 100)
                .SetDisplayFormat()
                .SetIsReadOnly();
            grdFileList.View.AddSpinEditColumn("SEQUENCE", 70)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("LOCALFILEPATH", 200)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCETYPE", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("RESOURCEVERSION", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("ENTERPRISEID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("PLANTID", 100)
                .SetIsHidden()
                .SetIsReadOnly();
            grdFileList.View.AddTextBoxColumn("COMMENTS", 300);
            grdFileList.View.AddComboBoxColumn("VALIDSTATE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Invalid")
                .SetValidationIsRequired();

            grdFileList.View.PopulateColumns();

            grdFileList.View.OptionsNavigation.AutoMoveRowFocus = false;
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
        {
            btnFileAdd.Click += BtnFileAdd_Click;
            btnFileDelete.Click += BtnFileDelete_Click;
            btnFileDownload.Click += BtnFileDownload_Click;
            grdFileList.View.DoubleClick += View_DoubleClick;
        }

        /// <summary>
        /// 파일추가 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFileAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] fullFileName = openFileDialog.FileNames;
                string[] safeFileName = openFileDialog.SafeFileNames;

                DataTable dataSource = grdFileList.DataSource as DataTable;

                int rowCount = grdFileList.View.RowCount + 1;

                foreach (string fileName in fullFileName)
                {
                    FileInfo fileInfo = new FileInfo(fileName);


                    DataRow[] rows = dataSource.Select("SAFEFILENAME = '" + fileInfo.Name + "'");
                    string addedFileName = "";

                    if (rows.Count() > 0)
                        addedFileName = "(1)";

                    DataRow newRow = dataSource.NewRow();

                    newRow["FILEID"] = "";
                    newRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
                    newRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
                    //newRow["FILEPATH"] = UploadPath;
                    newRow["FILEPATH"] = "MeasureFormMgnt/MeasureFormFile";
                    newRow["SAFEFILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName + fileInfo.Extension;
                    newRow["FILESIZE"] = fileInfo.Length;
                    newRow["SEQUENCE"] = rowCount;
                    newRow["LOCALFILEPATH"] = fileInfo.FullName;
                    //newRow["RESOURCETYPE"] = Resource.Type;
                    newRow["RESOURCETYPE"] = "MeasureForm";
                    newRow["RESOURCEID"] = Resource.Id;
                    //newRow["RESOURCEVERSION"] = Resource.Version;
                    newRow["RESOURCEVERSION"] = UserInfo.Current.Plant;
                    newRow["ENTERPRISEID"] = UserInfo.Current.Enterprise;
                    newRow["PLANTID"] = UserInfo.Current.Plant;

                    dataSource.Rows.Add(newRow);

                    rowCount++;
                }
            }
        }

        /// <summary>
        /// 파일삭제 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFileDelete_Click(object sender, EventArgs e)
        {
            ValidationCheckFile();

            grdFileList.View.DeleteCheckedRows();
        }

        /// <summary>
        /// 다운로드 버튼 클릭 이벤트 (복수 파일 다운로드)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFileDownload_Click(object sender, EventArgs e)
        {
            ValidationCheckFile();

            DataTable selectedFiles = grdFileList.View.GetCheckedRows();

            FileDownload(selectedFiles, DownloadType.Multi);
        }

        /// <summary>
        /// 파일 리스트 더블클릭 이벤트 (단일 파일 다운로드)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (grdFileList.View.FocusedRowHandle < 0)
                return;

            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);

            if (info.InRowCell && info.Column.FieldName == "COMMENTS")
                return;

            if (info.InRow)
            {
                DataTable selectedFiles = (DataSource as DataTable).Clone();
                DataRow selectedRow = view.GetDataRow(info.RowHandle);

                selectedFiles.Rows.Add(selectedRow.ItemArray.Clone() as object[]);

                FileDownload(selectedFiles, DownloadType.Multi);
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdFileList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                throw MessageException.Create("NoSaveData");
            }
            //2021-01-22 오근영 주석처리 한개의 유효건만 등록 해제
            //else if ((grdFileList.DataSource as DataTable).AsEnumerable().Where(r => r["VALIDSTATE"].Equals("Valid")).Count() > 1)
            //{
            //    throw MessageException.Create("RegisterOnlyOneValidOne"); // 한개의 유효건만 등록해주세요.
            //}

            //int chkAdded = 0;
            //2020-03-02 강유라 파일 서버 업로드 로직 수정
            //서버업로드용 Dt
            DataTable fileUploadTable = QcmImageHelper.GetImageFileTable();
            int totalFileSize = 0;

            foreach (DataRow row in changed.Rows)
            {
                if (row["_STATE_"].ToString() == "added")
                {
                    DataRow newRow = fileUploadTable.NewRow();

                    newRow["FILENAME"] = row["FILENAME"];
                    newRow["FILEEXT"] = row["FILEEXT"];
                    newRow["FILEPATH"] = "MeasureFormMgnt/MeasureFormFile"; ;
                    newRow["SAFEFILENAME"] = row["SAFEFILENAME"];
                    newRow["LOCALFILEPATH"] = row["LOCALFILEPATH"];
                    newRow["PROCESSINGSTATUS"] = "Wait";
                    newRow["SEQUENCE"] = row["SEQUENCE"];


                    //서버에서 데이타베이스를 입력하기 위해 파일아이디를 전달해야 한다.
                    totalFileSize += row.GetInteger("FILESIZE");

                    fileUploadTable.Rows.Add(newRow);
                }

                row["RESOURCEVERSION"] = UserInfo.Current.Plant;
                row["RESOURCETYPE"] = "MeasureForm";
                row["FILEPATH"] = "MeasureFormMgnt/MeasureFormFile";
            }

            if (fileUploadTable.Rows.Count > 0)
            {
                FileProgressDialog fileProgressDialog = new FileProgressDialog(fileUploadTable, UpDownType.Upload, "", totalFileSize);
                fileProgressDialog.ShowDialog(this);

                if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                    throw MessageException.Create("CancelMessageToUploadFile");  //파일업로드를 취소하였습니다.

                ProgressingResult fileResult = fileProgressDialog.Result;

                if (!fileResult.IsSuccess)
                    throw MessageException.Create("FailMessageToUploadFile"); //파일업로드를 실패하였습니다.
            }

            DataTable result = this.ExecuteRule<DataTable>("SaveMeasureForm", changed);
            //DataRow resultRow = result.Rows[0];
            /*
            if (chkAdded > 0)
            {
                Resource.Id = Format.GetString(resultRow["RESOURCEID"]);
                SaveChangedFiles();
            }*/
        }

        #endregion

        #region 검색

        /// <summary>
        /// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            var values = Conditions.GetValues();
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("GetMeasureFormFileList", "10001", values);

            if (dt.Rows.Count == 0)
            {
                // 조회할 데이터가 없습니다.
                this.ShowMessage("NoSelectData");
                ClearData();
                return;
            }

            grdFileList.DataSource = dt;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            //Conditions.AddComboBox("p_capacityType", new SqlQuery("GetCapacityType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "INSPECTIONCLASSNAME", "INSPECTIONCLASSID")
            //    .SetLabel("CAPACITYTYPE")
            //    .SetEmptyItem()
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
            //    .SetPosition(1.2);
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();
        }

        #endregion

        #region Private Function

        /// <summary>
        /// 체크한 항목이 있는지 확인
        /// </summary>
        private void ValidationCheckFile()
        {
            DataTable selectedFiles = grdFileList.View.GetCheckedRows();

            if (selectedFiles.Rows.Count < 1)
            {
                throw MessageException.Create("GridNoChecked");
            }
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
                        try
                        {
                            string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + Format.GetString(files.Rows[0]["FILEPATH"]);
                            serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/");
                            DeployCommonFunction.DownLoadFile(serverPath, folderBrowserDialog.SelectedPath, Format.GetString(files.Rows[0]["SAFEFILENAME"]));
                        }
                        catch (Exception ex)
                        {
                            throw MessageException.Create(ex.Message);
                        }

                        break;
                    case DownloadType.Multi:

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
                }
            }
        }

        /// <summary>
        /// 추가된 파일들을 서버에 업로드
        /// </summary>
        private int FileUpload()
        {
            DataTable addFiles = grdFileList.GetChangesAdded();
            addFiles.Columns.Add("PROCESSINGSTATUS", typeof(string));

            int totalFileSize = 0;

            foreach (DataRow row in addFiles.Rows)
            {
                row["PROCESSINGSTATUS"] = "Wait";

                totalFileSize += Format.GetInteger(row["FILESIZE"]);
            }

            addFiles.AcceptChanges();


            FileProgressDialogPopup fileProgressDialog = new FileProgressDialogPopup(addFiles, UpDownType.Upload, "", totalFileSize);
            fileProgressDialog.ShowDialog(this);

            if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                throw MessageException.Create("파일 업로드를 취소하였습니다.");


            MeasureProgressingResult result = fileProgressDialog.Result;

            if (result.IsSuccess)
                return result.SuccessFileCount;
            else
                return 0;

        }

        /// <summary>
        /// 체크한 파일들을 서버, DB 상에서 삭제
        /// </summary>
        private int FileDelete()
        {
            DataTable files = grdFileList.GetChangesDeleted();

            int deleteSuccessCount = 0;
            int deleteFailCount = 0;

            foreach (DataRow row in files.Rows)
            {
                if (DeployCommonFunction.RemoveFile(UploadPath, Format.GetString(row["SAFEFILENAME"])))
                    deleteFailCount++;
                else
                    deleteSuccessCount++;
            }

            return deleteSuccessCount;
        }

        /// <summary>
        /// 파일 서버에 업로드
        /// </summary>
        public void SaveChangedFiles()
        {
            int uploadFileCount = FileUpload();
            int deleteFileCount = FileDelete();
        }

        /// <summary>
        /// 파일 그리드 초기화
        /// </summary>
        public void ClearData()
        {
            if (grdFileList.DataSource == null) return;
            grdFileList.View.ClearDatas();
        }

        #endregion

        #region Properties

        [DefaultValue(null)]
        public object DataSource
        {
            get
            {
                return grdFileList.DataSource;
            }
            set
            {
                grdFileList.DataSource = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public MeasureResourceInfo Resource { get; set; } = new MeasureResourceInfo();

        public string UploadPath { get; set; } = "";

        [DefaultValue(false)]
        public bool UseCommentsColumn
        {
            get
            {
                return grdFileList.View.Columns["COMMENTS"].Visible;
            }
            set
            {
                grdFileList.View.Columns["COMMENTS"].Visible = value;
            }
        }

        [DefaultValue("GRIDFILELIST")]
        public string LanguageKey
        {
            get
            {
                return grdFileList.LanguageKey;
            }
            set
            {
                grdFileList.LanguageKey = value;
            }
        }

        [DefaultValue(true)]
        public bool ButtonVisible
        {
            get
            {
                return pnlFileUploadButton.Visible;
            }
            set
            {
                pnlFileUploadButton.Visible = value;
            }
        }

        #endregion
    }

    #region Public Class

    public class MeasureResourceInfo
    {
        public string Type { get; set; } = "";
        public string Id { get; set; } = "*";
        public string Version { get; set; } = "";
    }

    public class MeasureProgressingResult
    {
        public bool IsSuccess { get; set; } = true;
        public int SuccessFileCount { get; set; } = 0;
        public int FailFileCount { get; set; } = 0;
    }

    #endregion
}