#region using
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
#endregion

namespace Micube.SmartMES.QualityAnalysis
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > 입고검사등록
	/// 업  무  설  명  : 사진등록 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-06-20
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class QualityAddImagePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

		private string _resourceId = string.Empty;
		private string _path = "SegmentDefect/";
		public delegate void FileInfoHandler(DataTable dtFileInfo);
		public event FileInfoHandler FileInfo;
		

		#region 생성자
		public QualityAddImagePopup(string inspectionDefId)
		{
			InitializeComponent();

			if (!this.IsDesignMode())
			{
				InitializeEvent();

				//리소스 ID 설정
				Guid guid = Guid.NewGuid();
				_resourceId = guid.ToString().Replace("-", "").ToUpper();
				fileProcessingControl.Resource.Id = _resourceId;

				//파일 경로 설정
				inspectionDefId = inspectionDefId == "SelfInspectionTake" ? "SIT-" : "SIS-";

				_path += inspectionDefId + _resourceId;
				fileProcessingControl.UploadPath = _path;

			}
		}
		#endregion

		#region Event
		private void InitializeEvent()
		{
			this.btnApply.Click += BtnApply_Click;
			this.btnCancel.Click += BtnCancel_Click;
		}

		/// <summary>
		/// Cancel 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
        private void btnApply_Click_1(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Apply 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, EventArgs e)
		{
			fileProcessingControl.SaveChangedFiles();
			DataTable dtFileList = fileProcessingControl.GetChangedRows();

			foreach(DataRow row in dtFileList.Rows)
			{
				row["RESOURCEID"] = _resourceId;
				row["FILEPATH"] = _path;
			}

			FileInfo(dtFileList);

			this.Close();
		}
        #endregion

        private void fileProcessingControl_Load(object sender, EventArgs e)
        {

        }


    }
}
