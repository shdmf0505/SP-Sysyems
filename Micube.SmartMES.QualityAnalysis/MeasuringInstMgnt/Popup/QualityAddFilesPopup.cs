#region using
using Micube.Framework.Log;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using Micube.SmartMES.QualityAnalysis.Helper;
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
    /// 프 로 그 램 명  : Report 파일 등록
    /// 업  무  설  명  : File 등록 팝업
    /// 생    성    자  : 이승우
    /// 생    성    일  : 
    /// 수  정  이  력  : 
    /// 2019-11-29 - 이승우 수정
    /// 2019-06-05 - 강유라 최초 생성.
    /// 
    /// </summary>
    public partial class QualityAddFilesPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

		private string _resourceId = string.Empty;
        private string _path = "";// MeasuringHelper.mServerPath.filesReport;
		public delegate void FileInfoHandler(DataTable dtFileInfo);
		public event FileInfoHandler FileInfo;
		

		#region 생성자
		public QualityAddFilesPopup(string inspectionDefId)
		{
			InitializeComponent();

			if (!this.IsDesignMode())
			{
				InitializeEvent();

				//리소스 ID 설정
				Guid guid = Guid.NewGuid();
				_resourceId = guid.ToString().Replace("-", "").ToUpper();
				//fileProcessingControl.Resource.Id = _resourceId;

				//파일 경로 설정
				inspectionDefId = inspectionDefId == "SelfInspectionTake" ? "SIT-" : "SIS-";

				_path += inspectionDefId + _resourceId;
				//fileProcessingControl.UploadPath = _path;

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
        /// Form Load 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QualityAddFilesPopup_Load(object sender, EventArgs e)
        {
            fpcImage.LanguageKey = "Report";
            InitializeFilesControl();
        }

        /// 파일추가시 데이터 **** map정보 Key 고려
        /// </summary>
        private void InitializeFilesControl()
        {
            fpcImage.UploadPath = MeasuringHelper.mServerPath.filesReport;
            fpcImage.Resource = new ResourceInfo()
            {
                Type = "MeasuringReport",
                Id = "IFC_Report_001",
                Version = "0"
            };

            fpcImage.UseCommentsColumn = true;
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

		/// <summary>
		/// Apply 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnApply_Click(object sender, EventArgs e)
		{
            try
            {
                //fileProcessingControl.SaveChangedFiles();
                DataTable dtFileList = fpcImage.GetChangedRows();
                dtFileList.TableName = "filesList";

                //등록 파일 서버에 저장.
                if (fpcImage.GetChangedRows().Rows.Count > 0)
                {
                    fpcImage.SaveChangedFiles();
                }

                foreach (DataRow row in dtFileList.Rows)
                {
                    row["RESOURCEID"] = _resourceId;
                    row["FILEPATH"] = _path;
                }

                //이벤트 발생.
                FileInfo(dtFileList);

                this.Close();//종료.
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

		}

        #endregion


    }//class end


}//end namespace
