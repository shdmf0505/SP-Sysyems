#region using

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

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    public partial class SelectResourcePopup : SmartPopupBaseForm
    {
        #region Local Variables

        private string _lotId = "";
        private string _processSegmentId = "";
        private string _areaId = "";

        #endregion

        #region 생성자

        public SelectResourcePopup()
        {
            InitializeComponent();
        }

        public SelectResourcePopup(string lotId, string processSegmentId, string areaId)
        {
            InitializeComponent();

            _lotId = lotId;
            _processSegmentId = processSegmentId;
            _areaId = areaId;

            InitializeEvent();

            if (!tlpResource.IsDesignMode())
            {
                InitializeControls();
            }
        }

        #endregion

        #region 초기화

        private void InitializeControls()
        {
            // 현재 공정에서 사용할 자원을 선택하시기 바랍니다.
            lblTitle.Text = Language.GetMessage("SelectResourceForCurrentProcess").Message;


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", _lotId);
            param.Add("PROCESSSEGMENTID", _processSegmentId);
            param.Add("RESOURCETYPE", "Resource");
            param.Add("AREAID", _areaId);
            DataTable resourceList = SqlExecuter.Query("GetTransitAreaList", "10031", param);

            string primaryResourceId = "";

            for (int i = 0; i < resourceList.Rows.Count; i++)
            {
                DataRow row = resourceList.Rows[i];

                if (Format.GetString(row["ISPRIMARYRESOURCE"]) == "Y")
                {
                    primaryResourceId = Format.GetString(row["RESOURCEID"]);
                    break;
                }
            }

            cboResource.Editor.PopupWidth = 430;
            cboResource.Editor.SetVisibleColumns("RESOURCENAME", "AREANAME", "EQUIPMENTCLASSID");
            cboResource.Editor.SetVisibleColumnsWidth(200, 130, 100);
            cboResource.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.Custom;
            cboResource.Editor.ShowHeader = true;
            cboResource.Editor.ValueMember = "RESOURCEID";
            cboResource.Editor.DisplayMember = "RESOURCENAME";
            cboResource.Editor.UseEmptyItem = false;
            cboResource.Editor.DataSource = resourceList;
            cboResource.Editor.EditValue = string.IsNullOrWhiteSpace(primaryResourceId) ? "" : primaryResourceId;
        }

        #endregion

        #region Event

        private void InitializeEvent()
        {
            btnConfirm.Click += BtnConfirm_Click;
            btnCancle.Click += BtnCancle_Click;

            FormClosing += SelectResourcePopup_FormClosing;
        }

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Format.GetString(cboResource.EditValue)))
            {
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void SelectResourcePopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (DialogResult != DialogResult.OK)
              //  e.Cancel = true;
        }

        #endregion

        #region Property

        public string ResourceId
        {
            get
            {
                return Format.GetString(cboResource.EditValue);
            }
        }

        #endregion
    }
}