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

namespace Micube.SmartMES.StandardInfo
{
    public partial class ItemMasterfilePopup : SmartPopupBaseForm, ISmartCustomPopup
    {

        #region Interface

        string _itemid = "";
        string _itemver = "";
        string _RESOURCETYPE = "";
        string _FILEPATH = "";
        
        public DataRow CurrentDataRow { get; set; }

        #endregion

        #region Local Variables
        public DataTable checkTable;
        #endregion

        #region 생성자
        public ItemMasterfilePopup(string itemid ,string itemver,string RESOURCETYPE,string FILEPATH)
        {
            _itemid = itemid;
            _itemver = itemver;
            _RESOURCETYPE = RESOURCETYPE;
            _FILEPATH = FILEPATH;

			InitializeComponent();
            InitializeEvent();
            InitializeGrid();

			fileInspectionPaper.Resource.Id = itemid;
			fileInspectionPaper.Resource.Version = itemver;
			fileInspectionPaper.Resource.Type = RESOURCETYPE;
			fileInspectionPaper.UploadPath = FILEPATH;


		}
        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// Grid 초기화
        /// </summary>
        private void InitializeGrid()
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "RESOURCEID",_itemid},
                { "RESOURCETYPE", _RESOURCETYPE},
                { "RESOURCEVERSION", _itemver}
            };

            DataTable dt = SqlExecuter.Query("GetFileUploadList", "10001", param);

            if (dt.Rows.Count == 0)
            {
                fileInspectionPaper.ClearData();
                return;
            }

            fileInspectionPaper.DataSource = dt;
        }


        #endregion

        #region Event

        /// <summary>
        /// Event 초기화 
        /// </summary>
        private void InitializeEvent()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 저장 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DataTable changed = new DataTable();

            changed = fileInspectionPaper.GetChangedRows();

			if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            // 저장할 파일데이터가 있다면 서버에 업로드한다.
            if (changed.Rows.Count != 0)
            {
                int chkAdded = 0;

                foreach (DataRow row in changed.Rows)
                {
                    if (row["_STATE_"].ToString() == "added")
                    {
                        chkAdded++;
                    }

                    row["RESOURCEID"] = _itemid;
                    row["RESOURCETYPE"] = _RESOURCETYPE;
                    row["RESOURCEVERSION"] = _itemver;
                    row["FILEPATH"] = _FILEPATH;
                }

				if (chkAdded > 0)
                {
                    fileInspectionPaper.SaveChangedFiles();
                }
            }
   

            MessageWorker saveWorker = new MessageWorker("SaveObjectFile");
            saveWorker.SetBody(new MessageBody()
                {
                   { "list", changed }
                });

            saveWorker.Execute();

            MSGBox.Show(MessageBoxType.Information, "SuccedSave");

            changed.AcceptChanges();
			this.Close();
        }




        #endregion

        #region Public Function


        #endregion
    }
}
