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
using System.IO;
using Micube.SmartMES.Commons;
using Micube.Framework.Net;
using Micube.SmartMES.Commons.Controls;
using SmartDeploy.Common;
#endregion


namespace Micube.SmartMES.SystemManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 시스템관리 > 메뉴관리 > 매뉴얼 정보
	/// 업  무  설  명  : 매뉴얼 등록 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-10-15
	/// 수  정  이  력  : 2020-02-17 황유성 매뉴얼 등록에 맞게 화면 수정
	/// 
	/// 
	/// </summary>
	public partial class ModifyManualPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        private string _path = "Manual/";
        private string _menuId;
        private string _resourceId;

        public DataRow CurrentDataRow { get; set; }
        public delegate void FileInfoHandler(DataTable dtFileInfo);
		public event FileInfoHandler FileInfo;

		public ModifyManualPopup(string menuId)
		{
			InitializeComponent();

            _menuId = menuId;

            if (string.IsNullOrEmpty(menuId))
            {
                // 선택된 항목이 없습니다.
                throw MessageException.Create("NoSelections");
            }

			if (!this.IsDesignMode())
			{
				InitializeEvent();
				InitializeGrid();
				SelectDataBind();
                _path += this._resourceId;
            }
        }

        #region 이벤트
        private void InitializeEvent()
		{
			grdFileList.View.AddingNewRow += View_AddingNewRow;
			btnApply.Click += BtnApply_Click;
			btnClose.Click += BtnClose_Click;
            btnDownload.Click += BtnDownload_Click;
		}

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            FileDownload(grdFileList.View.GetCheckedRows(), DownloadType.Multi);
        }

        /// <summary>
        /// 파일 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.Filter = "PowerPoint Presentations|*.ppt;*.pptx" +
                                    "|Portable Document Format|*.pdf";
            openFileDialog.FilterIndex = 0;

			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string[] fullFileName = openFileDialog.FileNames;

				DataTable dtFileList = grdFileList.DataSource as DataTable;

				int rowCount = 0;
				foreach (string fileName in fullFileName)
				{
					FileInfo fileInfo = new FileInfo(fileName);

					string addedFileName = "";
					DataRow[] rows = dtFileList.Select("SAFEFILENAME = '" + fileInfo.Name + "'");
                    if (rows.Count() > 0)
                    {
                        addedFileName = "(1)";
                    }

					if (rowCount == 0)
					{
                        args.NewRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
						args.NewRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
						args.NewRow["FILESIZE"] = fileInfo.Length;
						args.NewRow["FILEPATH"] = _path;
						args.NewRow["RESOURCEID"] = _resourceId;
						args.NewRow["LOCALFILEPATH"] = fileInfo.FullName;
					}
					else
					{
						DataRow newRow = dtFileList.NewRow();
						newRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
						newRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
						newRow["FILESIZE"] = fileInfo.Length;
						newRow["FILEPATH"] = _path;
						newRow["RESOURCEID"] = _resourceId;
						newRow["LOCALFILEPATH"] = fileInfo.FullName;
						dtFileList.Rows.Add(newRow);
					}
					rowCount++;
				}
			}
			else
			{
				args.IsCancel = true;
			}
		}

        /// <summary>
        /// 적용 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataTable changed = grdFileList.GetChangedRows();       // 저장

            changed.Columns.Add("PROCESSINGSTATUS", typeof(string));
            int totalFileSize = 0;
            foreach (DataRow row in changed.Rows)
            {
                row["PROCESSINGSTATUS"] = "Wait";
                totalFileSize += Format.GetInteger(row["FILESIZE"]);
            }

            Commons.Controls.FileProgressDialog fileProgressDialog =
                new Commons.Controls.FileProgressDialog(ExtractNotDeleted(changed), Commons.Controls.UpDownType.Upload, "", totalFileSize);
            fileProgressDialog.ShowDialog(this);
            if (fileProgressDialog.DialogResult == DialogResult.Cancel)
            {
                // 파일 업로드를 취소하였습니다.
                throw MessageException.Create("FileUploadCanceled");
            }
            Commons.Controls.ProgressingResult result = fileProgressDialog.Result;
            changed.AcceptChanges();

            FileInfo(changed);
            this.Close();
        }

        private DataTable ExtractNotDeleted(DataTable table)
        {
            DataTable result = table.Clone();
            foreach(DataRow each in table.Rows)
            {
                if(each["_STATE_"].ToString() != "deleted")
                {
                    result.ImportRow(each);
                }
            }
            return result;
        }

		/// <summary>
		/// 닫기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion

		#region 컨텐츠 초기화
		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdFileList.GridButtonItem = GridButtonItem.Add | GridButtonItem.Delete;
			grdFileList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdFileList.View.SetIsReadOnly();

			grdFileList.View.AddTextBoxColumn("FILENAME", 300);
			grdFileList.View.AddTextBoxColumn("FILEEXT", 80).SetTextAlignment(TextAlignment.Center);
			grdFileList.View.AddSpinEditColumn("FILESIZE", 100).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			grdFileList.View.AddTextBoxColumn("FILEPATH").SetIsHidden();
			grdFileList.View.AddTextBoxColumn("RESOURCEID").SetIsHidden();
			grdFileList.View.AddTextBoxColumn("SAFEFILENAME").SetIsHidden();
			grdFileList.View.AddTextBoxColumn("LOCALFILEPATH").SetIsHidden();

			grdFileList.View.PopulateColumns();
		}
		#endregion

		private void SelectDataBind()
		{
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MENUID", this._menuId);
            param.Add("UIID", UserInfo.Current.Uiid);

            DataTable dt = SqlExecuter.Query("SelectManualOfMenu", "10001", param);
            grdFileList.DataSource = dt;

            if(dt.Rows.Count > 0)
            {
                this._resourceId = dt.Rows[0]["RESOURCEID"].ToString();
            }
            else
            {
                this._resourceId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
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
            folderBrowserDialog.SelectedPath = Application.StartupPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                switch (type)
                {
                    case DownloadType.Single:
                        try
                        {
                            string serverPath = AppConfiguration.GetString("Application.Ftp.Url") + Format.GetString(files.Rows[0]["FILEPATH"]);
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

                        FileProgressDialog fileProgressDialog = new FileProgressDialog(files, UpDownType.Download, folderBrowserDialog.SelectedPath, totalFileSize);
                        fileProgressDialog.ShowDialog();

                        if (fileProgressDialog.DialogResult == DialogResult.Cancel)
                            throw MessageException.Create("파일 다운로드를 취소하였습니다.");

                        ProgressingResult result = fileProgressDialog.Result;

                        int resultCount = 0;

                        if (result.IsSuccess)
                            resultCount = result.SuccessFileCount;

                        grdFileList.View.CheckedAll(false);
                        break;
                }
            }
        }
    }
}
