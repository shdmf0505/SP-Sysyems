#region using

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using SmartDeploy.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.Commons.Controls
{
    public partial class FileProgressDialog : SmartPopupBaseForm
    {
        // File List
        private object _dataSource;
        // Upload/Download
        private UpDownType _upDownType;
        // Selected Folder Path
        private string _folderPath;
        // Total File Size
        private int _totalFileSize;

        // File Processing Result
        public ProgressingResult Result = new ProgressingResult();

        public FileProgressDialog(object dataSource, UpDownType upDownType, string folderPath, int totalFileSize)
        {
            InitializeComponent();

            _dataSource = dataSource;
            _upDownType = upDownType;
            _folderPath = folderPath;
            _totalFileSize = totalFileSize;

            InitializeEvent();

            InitializeGrid();

            btnClose.Visible = false;
        }

        /// <summary>
        /// FileProgressDialog 팝업에서 사용하는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
        {
            Shown += FileProgressDialog_Shown;

            grdFileList.View.RowStyle += View_RowStyle;

            btnStart.Click += BtnStart_Click;
            btnClose.Click += BtnClose_Click;
        }

        private void FileProgressDialog_Shown(object sender, EventArgs e)
        {
            DataTable dataSource = _dataSource as DataTable;
            if(dataSource.Rows.Count == 0)
            {
                Result.IsSuccess = true;
                Result.SuccessFileCount = 0;
                Result.FailFileCount = 0;
                DialogResult = DialogResult.OK;
                this.Close();
            }

            pnlFileProgressWait.Visible = false;

            switch (_upDownType)
            {
                case UpDownType.Upload:
                    Text = Language.Get("FILEUPLOADTITLE");
                    break;
                case UpDownType.Download:
                    Text = Language.Get("FILEDOWNLOADTITLE");
                    break;
            }

            if (_dataSource != null && (_dataSource as DataTable).Rows.Count > 0)
            {
                if (_upDownType == UpDownType.Upload)
                {
                    btnStart.Visible = false;

                    //FileUploadToServer();
                    FileUploadToFtpServer();

                    DialogResult = DialogResult.OK;

                    Close();
                }
            }
        }

        private void View_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (Format.GetString(view.GetRowCellValue(e.RowHandle, "PROCESSINGSTATUS")).Equals("Complete"))
            {
                e.Appearance.Font = new Font("Malgun Gothic", e.Appearance.Font.Size, FontStyle.Bold);
            }
            else if (Format.GetString(view.GetRowCellValue(e.RowHandle, "PROCESSINGSTATUS")).Equals("Error"))
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.Font = new Font("Malgun Gothic", e.Appearance.Font.Size, FontStyle.Bold);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.IsBusy = true;

            pnlFileProgressWait.Visible = true;

            switch (_upDownType)
            {
                case UpDownType.Upload:
                    //FileUploadToServer();
                    FileUploadToFtpServer();
                    break;
                case UpDownType.Download:
                    //FileDownloadToClient();
                    FileDownloadFromFtpServer();
                    break;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        /// <summary>
        /// File List 그리드 정보를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            grdFileList.GridButtonItem = GridButtonItem.None;
            grdFileList.ShowButtonBar = false;
            grdFileList.ShowStatusBar = false;
            grdFileList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdFileList.View.SetIsReadOnly();
            grdFileList.View.SetSortOrder("SEQUENCE");
            grdFileList.View.EnableRowStateStyle = false;

            grdFileList.View.AddTextBoxColumn("FILENAME", 200);
            grdFileList.View.AddTextBoxColumn("FILEEXT", 100);
            grdFileList.View.AddTextBoxColumn("FILEPATH", 200)
                .SetIsHidden();
            grdFileList.View.AddTextBoxColumn("SAFEFILENAME", 200)
                .SetIsHidden();
            grdFileList.View.AddSpinEditColumn("FILESIZE", 100)
                .SetDisplayFormat();
            //grdFileList.View.AddTextBoxColumn("PROCESSINGSTATUS", 100);
            grdFileList.View.AddComboBoxColumn("PROCESSINGSTATUS", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=FileProcessingStatus", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdFileList.View.AddSpinEditColumn("SEQUENCE", 70)
                .SetIsHidden();
            grdFileList.View.AddTextBoxColumn("LOCALFILEPATH", 200)
                .SetIsHidden();

            grdFileList.View.PopulateColumns();


            grdFileList.DataSource = _dataSource;

            //if (_dataSource != null && (_dataSource as DataTable).Rows.Count > 0)
            //{
            //    if (_upDownType == UpDownType.Upload)
            //    {
            //        btnClose.Visible = true;

            //        //FileUploadToServer();
            //        FileUploadToFtpServer();

            //        DialogResult = DialogResult.OK;
            //    }
            //}
        }

        private void FileDownloadToClient()
        {
            try
            {
                int completeFileSize = 0;
                int completeFileCount = 0;

                DataTable dataSource = grdFileList.DataSource as DataTable;

                foreach (DataRow row in dataSource.Rows)
                {
                    if (row["PROCESSINGSTATUS"].Equals("Wait"))
                        row["PROCESSINGSTATUS"] = "Downloading";

                    if (row["PROCESSINGSTATUS"].Equals("Downloading"))
                    {
                        string filePath = Format.GetString(row["FILEPATH"]);
                        string safeFileName = Format.GetString(row["SAFEFILENAME"]);
                        string downloadPath = _folderPath;

                        try
                        {
                            string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + filePath;
                            serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/");
                            DeployCommonFunction.DownLoadFile(serverPath, downloadPath, safeFileName);

                            row["PROCESSINGSTATUS"] = "Complete";


                            completeFileSize += Format.GetInteger(row["FILESIZE"]);

                            barFileProgress.Position = (int)(((double)completeFileSize / (double)_totalFileSize) * 100);
                        }
                        catch (Exception ex)
                        {
                            row["PROCESSINGSTATUS"] = "Error";
                        }

                        /*
                        SmartDeployService.WebService webService = new SmartDeployService.WebService();
                        webService.DownloadFile(serverPath, safeFileName, downloadPath);

                        webService.DownloadFileCompleted += (sender, args) =>
                        {
                            if (args.Result == "SUCCESS")
                                row["PROCESSINGSTATUS"] = "Complete";
                            else
                                row["PROCESSINGSTATUS"] = "Error";

                            completeFileSize += Format.GetInteger(row["FILESIZE"]);
                            completeFileCount++;

                            barFileProgress.Position = (int)(((double)completeFileSize / (double)_totalFileSize) * 100);

                            if (completeFileCount == dataSource.Rows.Count)
                            {
                                ShowMessage("파일 다운로드가 완료되었습니다.");

                                btnStart.Visible = false;
                                btnClose.Visible = true;

                                pnlFileProgressWait.Visible = false;

                                DialogResult = DialogResult.OK;
                            }
                        };
                        */
                    }
                }

                ShowMessage("파일 다운로드가 완료되었습니다.");

                btnStart.Visible = false;
                btnClose.Visible = true;

                pnlFileProgressWait.Visible = false;

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Result.IsSuccess = false;
                throw MessageException.Create(ex.Message);
            }
        }

        private void FileUploadToServer()
        {
            try
            {
                int completeFileSize = 0;
                int completeFileCount = 0;

                DataTable dataSource = grdFileList.DataSource as DataTable;

                foreach (DataRow row in dataSource.Rows)
                {
                    if (row["PROCESSINGSTATUS"].Equals("Wait"))
                        row["PROCESSINGSTATUS"] = "Uploading";

                    if (row["PROCESSINGSTATUS"].Equals("Uploading"))
                    {
                        string fileName = Format.GetString(row["LOCALFILEPATH"]);
                        string safeFileName = Format.GetString(row["FILENAME"]) + "." + Format.GetString(row["FILEEXT"]);
                        string uploadPath = Format.GetString(row["FILEPATH"]);

                        if (!File.Exists(fileName))
                            throw new FileNotFoundException();

                        byte[] byteFile;

                        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            byteFile = new byte[fileStream.Length];
                            fileStream.Read(byteFile, 0, byteFile.Length);
                        }

                        SmartDeployService.WebService webService = new SmartDeployService.WebService();
                        //string uploadResult = webService.UploadFile(byteFile, "UploadFile/", safeFileName);
                        webService.UploadFileAsync(byteFile, uploadPath, safeFileName);

                        webService.UploadFileCompleted += (sender, args) =>
                        {
                            if (args.Result == "SUCCESS")
                                row["PROCESSINGSTATUS"] = "Complete";
                            else
                                row["PROCESSINGSTATUS"] = "Error";

                            completeFileSize += Format.GetInteger(row["FILESIZE"]);
                            completeFileCount++;

                            barFileProgress.Position = (int)(((double)completeFileSize / (double)_totalFileSize) * 100);
                            //barFileProgress.Increment((int)(((double)Format.GetInteger(row["FILESIZE"]) / (double)_TotalFileSize) * 100));
                            if (completeFileCount == dataSource.Rows.Count)
                            {
                                ShowMessage("파일 업로드가 완료되었습니다.");

                                btnStart.Visible = false;
                                btnClose.Visible = true;

                                pnlFileProgressWait.Visible = false;

                                DialogResult = DialogResult.OK;
                            }
                        };
                    }
                    //if (!uploadResult.Equals("SUCCESS"))
                    //    throw new Exception(uploadResult);

                    //row["PROCESSINGSTATUS"] = "Complete";

                    //completeFileSize += Format.GetInteger(row["FILESIZE"]);

                    //barFileProgress.Position = (completeFileSize / _TotalFileSize) * 100;
                }
            }
            catch (Exception ex)
            {
                Result.IsSuccess = false;
                throw MessageException.Create(ex.Message);
            }
        }

        private void FileUploadToFtpServer()
        {
            try
            {
                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                int completeFileSize = 0;
                int completeFileCount = 0;

                DataTable dataSource = grdFileList.DataSource as DataTable;

                foreach (DataRow row in dataSource.Rows)
                {
                    if (row["PROCESSINGSTATUS"].Equals("Wait"))
                        row["PROCESSINGSTATUS"] = "Uploading";

                    if (row["PROCESSINGSTATUS"].Equals("Uploading"))
                    {
                        try
                        {
                            string fileName = Format.GetString(row["LOCALFILEPATH"]);
                            string safeFileName = Format.GetString(row["FILENAME"]) + "." + Format.GetString(row["FILEEXT"]);
                            string uploadPath = Format.GetString(row["FILEPATH"]);

                            if (!File.Exists(fileName))
                                throw new FileNotFoundException();

                            CreateFtpServerDirectory(ftpServerPath, uploadPath, ftpServerUserId, ftpServerPassword);

                            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpServerPath + string.Join("/", uploadPath, safeFileName));
                            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                            ftpRequest.UseBinary = true;
                            ftpRequest.UsePassive = true;
                            ftpRequest.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

                            byte[] byteFile;

                            //using (StreamReader reader = new StreamReader(fileName))
                            //{
                            //    byteFile = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                            //}

                            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                            {
                                byteFile = new byte[fileStream.Length];
                                fileStream.Read(byteFile, 0, byteFile.Length);
                            }

                            ftpRequest.ContentLength = byteFile.Length;
                            using (Stream stream = ftpRequest.GetRequestStream())
                            {
                                stream.Write(byteFile, 0, byteFile.Length);
                            }

                            using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                            {
                                row["PROCESSINGSTATUS"] = "Complete";

                                completeFileSize += Format.GetInteger(row["FILESIZE"]);
                                completeFileCount++;

                                barFileProgress.Position = (int)(((double)completeFileSize / (double)_totalFileSize) * 100);
                            }
                        }
                        catch (Exception e)
                        {
                            row["PROCESSINGSTATUS"] = "Error";
                            throw MessageException.Create(e.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.IsSuccess = false;
                throw MessageException.Create(ex.Message);
            }


            ShowMessage("파일 업로드가 완료되었습니다.");

            Result.IsSuccess = true;

            btnStart.Visible = false;
            btnClose.Visible = true;

            pnlFileProgressWait.Visible = false;

            DialogResult = DialogResult.OK;
        }

        private void FileDownloadFromFtpServer()
        {
            try
            {
                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                int completeFileSize = 0;
                int completeFileCount = 0;

                DataTable dataSource = grdFileList.DataSource as DataTable;

                foreach (DataRow row in dataSource.Rows)
                {
                    if (row["PROCESSINGSTATUS"].Equals("Wait"))
                        row["PROCESSINGSTATUS"] = "Downloading";

                    if (row["PROCESSINGSTATUS"].Equals("Downloading"))
                    {
                        try
                        {
                            string filePath = Format.GetString(row["FILEPATH"]);
                            string safeFileName = Format.GetString(row["SAFEFILENAME"]);
                            string downloadPath = _folderPath;

                            using (WebClient client = new WebClient())
                            {
                                string serverPath = ftpServerPath + Format.GetString(row["FILEPATH"]);
                                serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + Format.GetString(row["SAFEFILENAME"]);

                                client.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

                                client.DownloadFile(serverPath, string.Join("\\", _folderPath, Format.GetString(row["SAFEFILENAME"])));

                                row["PROCESSINGSTATUS"] = "Complete";


                                completeFileCount++;
                                completeFileSize += Format.GetInteger(row["FILESIZE"]);

                                barFileProgress.Position = (int)(((double)completeFileSize / (double)_totalFileSize) * 100);
                            }
                        }
                        catch (Exception e)
                        {
                            row["PROCESSINGSTATUS"] = "Error";
                            throw MessageException.Create(e.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.IsSuccess = false;
                throw MessageException.Create(ex.Message);
            }

            ShowMessage("파일 다운로드가 완료되었습니다.");

            Result.IsSuccess = true;

            btnStart.Visible = false;
            btnClose.Visible = true;

            pnlFileProgressWait.Visible = false;

            DialogResult = DialogResult.OK;
        }

        private void CreateFtpServerDirectory(string url, string path, string id, string pwd)
        {
            FtpWebRequest ftpRequest = null;
            Stream ftpStream = null;

            string[] subDirs = path.Split('/');

            string currentDir = url;

            foreach (string subDir in subDirs)
            {
                try
                {
                    currentDir = string.Join("/", currentDir.TrimEnd('/'), subDir);
                    ftpRequest = (FtpWebRequest)FtpWebRequest.Create(currentDir);
                    ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                    ftpRequest.Credentials = new NetworkCredential(id, pwd);
                    ftpRequest.UseBinary = true;
                    ftpRequest.UsePassive = true;
                    ftpRequest.Proxy = null;

                    FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                    ftpStream = ftpResponse.GetResponseStream();
                    ftpStream.Close();
                    ftpResponse.Close();
                }
                catch (WebException ex)
                {
                    FtpWebResponse ftpResponse = (FtpWebResponse)ex.Response;
                    ftpResponse.Close();
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            DataTable dataSource = grdFileList.DataSource as DataTable;

            Result.SuccessFileCount = dataSource.Select("PROCESSINGSTATUS = 'Complete'").Count();
            Result.FailFileCount = dataSource.Select("PROCESSINGSTATUS = 'Error'").Count();

            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }
    }
}