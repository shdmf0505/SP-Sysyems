using System;
#region using
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
#endregion


namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > 자주검사 입고, 출하
	/// 업  무  설  명  : 사진 수정 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-10-15
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ModifiyImagePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

		private Dictionary<string, object> _param = null;
		private string _path = "SegmentDefect/";
		public delegate void FileInfoHandler(DataTable dtFileInfo, bool isAdd);
		public event FileInfoHandler FileInfo;
		public ModifiyImagePopup(string inspectionDefId, DataRow row)
		{
			InitializeComponent();

			if(string.IsNullOrEmpty(inspectionDefId) || row == null)
			{
				//선택된 row가 없습니다.
				throw MessageException.Create("");
			}
			_path += inspectionDefId; //SelfInspectionTake vs SelfInspectionShip

			_param = row.Table.Columns.Cast<DataColumn>()
										 .ToDictionary(col => col.ColumnName, col => row[col.ColumnName]);

			if (!this.IsDesignMode())
			{
				InitializeEvent();
				InitializeGrid();

				SelectDataBind(_param);
			}
		}

		#region 이벤트
		private void InitializeEvent()
		{
			grdFileList.View.AddingNewRow += View_AddingNewRow;
			grdFileList.View.FocusedRowChanged += View_FocusedRowChanged;
			btnApply.Click += BtnApply_Click;
			btnClose.Click += BtnClose_Click;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.Filter = "All Files (*.*)|*.*";
			openFileDialog.FilterIndex = 0;

			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string[] fullFileName = openFileDialog.FileNames;

				DataTable dtFileList = grdFileList.DataSource as DataTable;

				string uploadPath = "";
				string resourceId = "";
				if(dtFileList.Rows.Count > 0)
				{
					uploadPath = Format.GetString(dtFileList.Rows[0]["FILEPATH"]);
					resourceId = Format.GetString(dtFileList.Rows[0]["IMAGERESOURCEID"]);
				}
				
				int rowCount = 0;
				foreach (string fileName in fullFileName)
				{
					FileInfo fileInfo = new FileInfo(fileName);

					string addedFileName = "";
					DataRow[] rows = dtFileList.Select("SAFEFILENAME = '" + fileInfo.Name + "'");
					if (rows.Count() > 0)
						addedFileName = "(1)";

					if (rowCount.Equals(0))
					{
						args.NewRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
						args.NewRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
						args.NewRow["FILESIZE"] = fileInfo.Length;
						args.NewRow["FILEPATH"] = uploadPath;
						args.NewRow["IMAGERESOURCEID"] = resourceId;
						args.NewRow["LOCALFILEPATH"] = fileInfo.FullName;
					}
					else
					{
						DataRow newRow = dtFileList.NewRow();

						newRow["FILENAME"] = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + addedFileName;
						newRow["FILEEXT"] = fileInfo.Extension.Replace(".", "");
						newRow["FILESIZE"] = fileInfo.Length;
						newRow["FILEPATH"] = uploadPath;
						newRow["IMAGERESOURCEID"] = resourceId;
						newRow["LOCALFILEPATH"] = fileInfo.FullName;

						dtFileList.Rows.Add(newRow);
						
					}
					rowCount++;
				}

				FocusedRowDataBind();
			}
			else
			{
				args.IsCancel = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(grdFileList.View.FocusedRowHandle < 0) return;

			FocusedRowDataBind();
		}

		private void FocusedRowDataBind()
		{
			DataRow selectRow = grdFileList.View.GetFocusedDataRow();
			if (selectRow == null) return;

			if (selectRow.RowState.Equals(DataRowState.Added))
			{
				picDefectPhoto.LoadAsync(Format.GetString(grdFileList.View.GetFocusedRowCellValue("LOCALFILEPATH")));
			}
			else
			{
                //picDefectPhoto.LoadAsync(Format.GetString(grdFileList.View.GetFocusedRowCellValue("URL")));
                // TODO : 파일 업로드 경로 변경 (SmartDeploy -> NAS FTP) 시 수정
                string filePath = Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEPATH"));
                string fileName = string.Join(".", Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILENAME")), Format.GetString(grdFileList.View.GetFocusedRowCellValue("FILEEXT")));

                picDefectPhoto.EditValue = CommonFunction.GetFtpImageFileToByte(filePath, fileName);
            }
        }

		/// <summary>
		/// 적용 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnApply_Click(object sender, EventArgs e)
		{
			//저장
			DataTable changed = grdFileList.GetChangedRows();
			//삭제
			DataTable deleted = grdFileList.GetChangesDeleted();

			if (changed.Rows.Count >= 1 && deleted.Rows.Count < 1)
			{
				changed.Columns.Add("PROCESSINGSTATUS", typeof(string));

				int totalFileSize = 0;

				foreach (DataRow row in changed.Rows)
				{
					row["PROCESSINGSTATUS"] = "Wait";

					totalFileSize += Format.GetInteger(row["FILESIZE"]);
				}

				changed.AcceptChanges();


				Commons.Controls.FileProgressDialog fileProgressDialog = new Commons.Controls.FileProgressDialog(changed, Commons.Controls.UpDownType.Upload, "", totalFileSize);
				fileProgressDialog.ShowDialog(this);

				if (fileProgressDialog.DialogResult == DialogResult.Cancel)
				{
					throw MessageException.Create("파일 업로드를 취소하였습니다.");
				}

				Commons.Controls.ProgressingResult result = fileProgressDialog.Result;

				FileInfo(changed, true);
			}
			else if (deleted.Rows.Count >= 1)
			{
				foreach (DataRow row in deleted.Rows)
				{
					SmartDeploy.Common.DeployCommonFunction.RemoveFile(Format.GetString(row["FILEPATH"]), Format.GetString(row["FILENAME"]) + "." + Format.GetString(row["FILEEXT"]));
				}

				DataTable dt = (grdFileList.DataSource as DataTable).Clone();

				foreach (DataRow r in (grdFileList.DataSource as DataTable).Rows)
				{
					bool found = false;
					foreach (DataRow dr in deleted.Rows)
					{
						if (r["URL"].ToString().Equals(dr["URL"].ToString()))
						{
							found = true;
							break;
						}
					}

					if (!found) dt.ImportRow(r);
				}
				grdFileList.DataSource = dt;


				FileInfo(dt, false);
			}

			this.Close();
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

			grdFileList.View.AddTextBoxColumn("FILENAME", 150);
			grdFileList.View.AddTextBoxColumn("FILEEXT", 60).SetTextAlignment(TextAlignment.Center);
			grdFileList.View.AddSpinEditColumn("FILESIZE", 60).SetDisplayFormat("#,##0", MaskTypes.Numeric, false).SetTextAlignment(TextAlignment.Right);
			grdFileList.View.AddTextBoxColumn("FILEPATH", 100).SetIsHidden();
			grdFileList.View.AddTextBoxColumn("IMAGERESOURCEID").SetIsHidden();
			grdFileList.View.AddTextBoxColumn("URL").SetIsHidden();
			grdFileList.View.AddTextBoxColumn("SAFEFILENAME").SetIsHidden();
			grdFileList.View.AddTextBoxColumn("LOCALFILEPATH").SetIsHidden();

			grdFileList.View.PopulateColumns();
		}
		#endregion

		private void SelectDataBind(Dictionary<string, object> param)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("FILENAME", typeof(string));
			dt.Columns.Add("FILEEXT", typeof(string));
			dt.Columns.Add("FILESIZE", typeof(string));
			dt.Columns.Add("FILEPATH", typeof(string));
			dt.Columns.Add("IMAGERESOURCEID", typeof(string));
			dt.Columns.Add("URL", typeof(string));
			dt.Columns.Add("SAFEFILENAME", typeof(string));
			dt.Columns.Add("LOCALFILEPATH", typeof(string));

			string[] fileName = Format.GetString(param["FILENAME"]).Split(',');
			string[] fileExt = Format.GetString(param["FILEEXT"]).Split(',');
			string[] fileSize = Format.GetString(param["FILESIZE"]).Split(',');
			string url = AppConfiguration.GetString("Application.SmartDeploy.Url") + param["FILEPATH"].ToString();

			for (int i = 1; i < fileName.Length; i++)
			{
				string fileUrl = url + "/" +fileName[i] + "." + fileExt[i];
				dt.Rows.Add(fileName[i], fileExt[i], fileSize[i], param["FILEPATH"], param["IMAGERESOURCEID"], fileUrl, fileName[i], "");
			}

			grdFileList.DataSource = dt;
		}
	}
}
