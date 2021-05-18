#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
using Micube.Framework.SmartControls.Grid;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Base;
using SmartDeploy.Common;
using System.IO;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;
using System.Net;
using System.Diagnostics;
#endregion

namespace Micube.SmartMES.Commons.Controls
{
    /// <summary>
    /// 프 로 그 램 명  : 매뉴얼을 지원하는 베이스폼
    /// 업  무  설  명  : 
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-02-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SmartConditionManualBaseForm : SmartConditionBaseForm
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public SmartConditionManualBaseForm()
        {
            InitializeComponent();
            if (!this.IsDesignMode())
            {
                InitializeEvent();
            }
        }

        private void InitializeEvent()
        {
            this.Load += SmartConditionManualBaseForm_Load;
        }

        private void SmartConditionManualBaseForm_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MENUID", this.MenuId);
            param.Add("UIID", UserInfo.Current.Uiid);
            System.Data.DataTable dt = SqlExecuter.Query("SelectManualOfMenu", "10001", param);

            if(dt.Rows.Count > 0)
            {
                SmartPanel panel = new SmartPanel();
                panel.Width = 11;
                panel.Dock = DockStyle.Right;
                panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                pnlToolbar.Controls.Add(panel);

                SmartButton button = new SmartButton();
                button.LanguageKey = "MANUAL";
                button.Size = new Size(75, 23);
                button.Dock = DockStyle.Right;
                button.Click += BtnManual_Click;
                pnlToolbar.Controls.Add(button);
            }
        }

        private void BtnManual_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MENUID", this.MenuId);
            param.Add("UIID", UserInfo.Current.Uiid);
            System.Data.DataTable dt = SqlExecuter.Query("SelectManualOfMenu", "10001", param);

            if (dt.Rows.Count > 0)
            {
                //string downloadPath = Path.GetTempPath();
                string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
                DownloadFiles(dt, downloadPath);
            }
        }

        private void DownloadFiles(System.Data.DataTable files, string downloadPath)
        {
            try
            {
                foreach (DataRow each in files.Rows)
                {
                    string ftpFilePath = Format.GetString(each["FILEPATH"]) + "/" + Format.GetString(each["SAFEFILENAME"]);
                    string downloadFilePath = downloadPath + Format.GetString(each["SAFEFILENAME"]);

                    GetFtpFile(ftpFilePath, downloadFilePath);
                    string pdfPath;
                    if (Format.GetString(each["FILEEXT"]).ToUpper() == "PPT"
                        || Format.GetString(each["FILEEXT"]).ToUpper() == "PPTX")
                    {
                        pdfPath = Ppt2Pdf(downloadFilePath);
                    }
                    else
                    {
                        pdfPath = downloadFilePath;
                    }
                    Process.Start("explorer.exe", pdfPath);
                }
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.Message);
            }
        }

        private string Ppt2Pdf(string pptPath)
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pptPath) + ".pdf";
            string pdfPath = Path.GetDirectoryName(pptPath) + "\\" + pdfFileName;

            File.Delete(pdfPath);

            Microsoft.Office.Interop.PowerPoint.Application ppApp = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Interop.PowerPoint.Presentations ppPresentations = ppApp.Presentations;
            Microsoft.Office.Interop.PowerPoint.Presentation prsPres
                = ppPresentations.Open(pptPath, 
                    MsoTriState.msoTrue,
                    MsoTriState.msoFalse,
                    MsoTriState.msoFalse);
            prsPres.SaveAs(pdfPath,
                Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoFalse);

            Marshal.ReleaseComObject(prsPres);
            Marshal.ReleaseComObject(ppPresentations);
            Marshal.ReleaseComObject(ppApp);
            return pdfPath;
        }

        /// <summary>
        /// FTP에 업로드 된 파일을 다운로드함
        /// </summary>
        /// <param name="ftpFilePath">FTP 하위 파일 경로 (Sf_ObjectFile 테이블의 FilePath 필드 값) + 확장자 포함 전체 파일명 (ex> SelfTakeInspection/ErrorImage.png)</param>
        /// <param name="localFilePath">로컬 파일 경로 (ex> SelfTakeInspection/ErrorImage.png)</param>
        /// <returns></returns>
        private void GetFtpFile(string ftpFilePath, string localFilePath)
        {
            string ftpServerPath = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Url"));
            string ftpServerUserId = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Id"));
            string ftpServerPassword = Cryptography.AESDecrypt(AppConfiguration.GetString("Application.Ftp.Password"));
            string ftpFileFullPath = ftpServerPath + ftpFilePath;

            try
            {
                WebClient ftpClient = new WebClient();
                ftpClient.Credentials = new NetworkCredential(ftpServerUserId, ftpServerPassword);
                ftpClient.DownloadFile(ftpFileFullPath, localFilePath);
                ftpClient.Dispose();
            }
            catch
            {
            }
        }
    }
}
