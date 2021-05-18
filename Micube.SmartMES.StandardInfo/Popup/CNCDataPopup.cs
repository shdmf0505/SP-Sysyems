#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.StandardInfo.Popup
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > 사양진행관리 > CNC Data관리 - CAM PART 전달 내역 팝업
    /// 업  무  설  명  : 전달 내역 작성
    /// 생    성    자  : 신상철
    /// 생    성    일  : 2020-01-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class CNCDataPopup : SmartPopupBaseForm, ISmartCustomPopup
    { 
        #region Local Variables
        public DataRow CurrentDataRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private string _STATE_ = "added";

        private DataTable dtFile;
        #endregion

        #region 생성자
        public CNCDataPopup()
        {
            InitializeComponent();
			InitializeEvent();
            InitializeCondition();

            InitializeControl();
        }
        public CNCDataPopup(DataRow row)
        {
            InitializeComponent();

            InitializeEvent();
            InitializeCondition();

            _STATE_ = "modified";

            InitializeControl(row);
        }

        #endregion

        #region 초기화
        /// <summary>
        /// 조회 조건 초기화
        /// </summary>
        private void InitializeCondition()
        {
            

        }

        private void InitializeControl(DataRow row = null)
        {
            dtFile = new DataTable();

            dtFile.TableName = "fileTable";
            dtFile.Columns.Add("_STATE_");

            dtFile.Columns.Add("FILEID");
            dtFile.Columns.Add("FILENAME");
            dtFile.Columns.Add("FILEEXT");
            dtFile.Columns.Add("FILEPATH");
            dtFile.Columns.Add("SAFEFILENAME");
            dtFile.Columns.Add("FILESIZE", typeof(int));
            dtFile.Columns.Add("SEQUENCE");
            dtFile.Columns.Add("LOCALFILEPATH");
            dtFile.Columns.Add("RESOURCETYPE");
            dtFile.Columns.Add("RESOURCEID");
            dtFile.Columns.Add("RESOURCEVERSION");
            dtFile.Columns.Add("PROCESSINGSTATUS");

            if (row == null)
            {
                lblTitle.Tag = Guid.NewGuid();

                lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

                lblUserName.Tag = UserInfo.Current.Id;
                lblUserName.Text = UserInfo.Current.Name;
            }
            else
            {

                lblTitle.Tag = row["NOTICENO"].ToString();

                lblDateTime.Text = Convert.ToDateTime(row["WRITEDATE"]).ToString("yyyy-MM-dd");

                lblUserName.Tag = row["CREATORID"].ToString();
                lblUserName.Text = row["WRITER"].ToString();

                lblFile.Text = row["ATTACHEDFILE"].ToString();
                lblFile.Tag = row["NOTICEFILEID"].ToString();

                txtContents.EditValue = row["COMMENTS"].ToString();
            }
        }
        #endregion

        #region  이벤트
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;

            btnFileSelect.Click += BtnFileSelect_Click;
            lblFile.Click += LblFile_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            dt.TableName = "list";
            dt.Columns.Add("_STATE_");
            //dt.Columns.Add("CREATOR");
            //dt.Columns.Add("CREATEDTIME");
            dt.Columns.Add("COMMENTS");
            dt.Columns.Add("FILEID");
            dt.Columns.Add("NOTICENO");

            DataRow newContentsRow = dt.NewRow();

            newContentsRow["_STATE_"] = _STATE_;
            //newContentsRow["CREATOR"] = UserInfo.Current.Id; ;
            //newContentsRow["CREATEDTIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            newContentsRow["COMMENTS"] = txtContents.EditValue;
            newContentsRow["FILEID"] = txtSelectFilePath.Tag == null ? lblFile.Tag : txtSelectFilePath.Tag;
            newContentsRow["NOTICENO"] = lblTitle.Tag;

            dt.Rows.Add(newContentsRow);

            ds.Tables.Add(dt);

            //File Table
            DataTable dtFiledata = dtFile.Copy();

            //File Upload
            if (dtFiledata.Rows.Count != 0)
            {
                ds.Tables.Add(dtFiledata);
                FileUploadToServer(dtFiledata);
            }

            ExecuteRule("SaveCNCDataNotice", ds);

            dtFile.Clear();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //파일 업로드
        private void BtnFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] fullFileName = openFileDialog.FileNames;
                string[] safeFileName = openFileDialog.SafeFileNames;

                string uploadPath = "CNCData";

                string resurceId = btnFileSelect.Tag.ToString().ToUpper() + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string fileState = lblFile.Tag == null ? "added" : "modified";

                foreach (string fileName in fullFileName)
                {
                    FileInfo fileInfo = new FileInfo(fileName);

                    DataRow[] rows = dtFile.Select("SAFEFILENAME = '" + fileInfo.Name + "'");
                    string addedFileName = "";

                    if (rows.Count() > 0)
                        addedFileName = "(1)";

                    DataRow newRow = dtFile.NewRow();

                    newRow["_STATE_"] = fileState;
                    newRow["FILEID"] = "FILE-" + Guid.NewGuid().ToString("N");
                    newRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
                    newRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
                    newRow["FILEPATH"] = uploadPath;
                    newRow["SAFEFILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName + fileInfo.Extension;
                    newRow["FILESIZE"] = fileInfo.Length;
                    newRow["SEQUENCE"] = 1;
                    newRow["PROCESSINGSTATUS"] = "";
                    newRow["LOCALFILEPATH"] = fileInfo.FullName;
                    newRow["RESOURCETYPE"] = "CNCData";//Resource.Type;
                    newRow["RESOURCEID"] = resurceId;//Resource.Id;
                    newRow["RESOURCEVERSION"] = 1;//Resource.Version;

                    txtSelectFilePath.EditValue = fileInfo.Name;
                    txtSelectFilePath.Tag = newRow["FILEID"].ToString();//resurceId;

                    dtFile.Rows.Add(newRow);
                }
            }
        }

        //첨부파일 클릭
        private void LblFile_Click(object sender, EventArgs e)
        {
            FileDownload(lblFile.Text);
        }

        //첨부파일 다운
        private void FileDownload(string filePath, string FileId = null)
        {
            string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
            string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
            string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = Application.StartupPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string serverFilePath = "CNCData";

                    using (WebClient client = new WebClient())
                    {
                        string serverPath = ftpServerPath + serverFilePath;
                        serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/") + filePath;

                        client.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);

                        client.DownloadFile(serverPath, string.Join("\\", folderBrowserDialog.SelectedPath, filePath));
                    }

                    //string serverPath = AppConfiguration.GetString("Application.SmartDeploy.Url") + "CNCData";// filePath;//+ Format.GetString(files.Rows[0]["FILEPATH"]);
                    //serverPath = serverPath + ((serverPath.EndsWith("/")) ? "" : "/");
                    //DeployCommonFunction.DownLoadFile(serverPath, folderBrowserDialog.SelectedPath, filePath);// Format.GetString(files.Rows[0]["SAFEFILENAME"]));

                    ShowMessage("파일 다운로드가 완료되었습니다.");
                }
                catch (Exception ex)
                {
                    throw MessageException.Create(ex.Message);
                }
            }
        }

        //파일 서버 업로드
        private void FileUploadToServer(DataTable dtFile)
        {
            try
            {
                string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
                string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
                string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));

                int completeFileSize = 0;
                int completeFileCount = 0;

                DataTable dataSource = dtFile;

                foreach (DataRow row in dataSource.Rows)
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

                    //using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    //{
                    //    byteFile = new byte[fileStream.Length];
                    //    fileStream.Read(byteFile, 0, byteFile.Length);
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

                    //Commons.Controls.SmartDeployService.WebService webService = new Commons.Controls.SmartDeployService.WebService();
                    //webService.UploadFileAsync(byteFile, uploadPath, safeFileName);

                    //webService.UploadFileCompleted += (sender, args) =>
                    //{
                    //    if (args.Result == "SUCCESS")
                    //        row["PROCESSINGSTATUS"] = "Complete";
                    //    else
                    //        row["PROCESSINGSTATUS"] = "Error";

                    //    completeFileSize += Format.GetInteger(row["FILESIZE"]);
                    //    completeFileCount++;

                    //    if (completeFileCount == dataSource.Rows.Count)
                    //    {
                    //        //ShowMessage("파일 업로드가 완료되었습니다.");

                    //        DialogResult = DialogResult.OK;
                    //    }
                    //};
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
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
        #endregion
    }
}
