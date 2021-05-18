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

namespace Micube.SmartMES.QualityAnalysis.ShipmentInspection
{
    public partial class SelectNextResourceShipmentPopup : SmartPopupBaseForm
    {
        #region Local Variables

        private string _lotId = "";
        private string _toResourceId = "";
        private string _toProcessPathId = "";
        private string _toAreaId = "";
        private string _toProcesssegmentId = "";
        private string _toProcesssegmentVersion = "";
        private string _toUserSequence = "";


        #endregion

        #region 생성자
        public SelectNextResourceShipmentPopup()
        {
            InitializeComponent();
        }

        public SelectNextResourceShipmentPopup(string lodId)
        {
            InitializeComponent();

            _lotId = lodId;

            InitializeEvent();

            InitializeControls();

        }

        #endregion

        #region 초기화

        private void InitializeControls()
        {
            //현재 공정에서 사용할 자원을 선택하시기 바랍니다.
            lblInfo.Text = Language.GetMessage("SelectResourceToFinish").Message;


            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("LOTID", _lotId);
            param.Add("RESOURCETYPE", "Resource");
   
            DataTable resourceList = SqlExecuter.Query("GetResourceListToFinish", "10001", param);

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
            btnSave.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancle_Click;

            cboResource.Editor.EditValueChanged += Editor_EditValueChanged;

        }

        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Format.GetString(cboResource.Editor.EditValue)))
                return;

            _toAreaId = Format.GetString(cboResource.Properties.GetDataSourceValue("AREAID", cboResource.Properties.GetDataSourceRowIndex("RESOURCEID", cboResource.Editor.EditValue)));
            _toProcessPathId = Format.GetString(cboResource.Properties.GetDataSourceValue("PROCESSPATHID", cboResource.Properties.GetDataSourceRowIndex("RESOURCEID", cboResource.Editor.EditValue)));
            _toProcesssegmentId = Format.GetString(cboResource.Properties.GetDataSourceValue("PROCESSSEGMENTID", cboResource.Properties.GetDataSourceRowIndex("RESOURCEID", cboResource.Editor.EditValue)));
            _toProcesssegmentVersion = Format.GetString(cboResource.Properties.GetDataSourceValue("PROCESSSEGMENTVERSION", cboResource.Properties.GetDataSourceRowIndex("RESOURCEID", cboResource.Editor.EditValue)));
            _toUserSequence = Format.GetString(cboResource.Properties.GetDataSourceValue("USERSEQUENCE", cboResource.Properties.GetDataSourceRowIndex("RESOURCEID", cboResource.Editor.EditValue)));
            _toResourceId = Format.GetString(cboResource.Editor.EditValue);
     
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

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        #endregion



        #region Property

        public string ToResourceId
        {
            get
            {
                return Format.GetString(_toResourceId);
            }
        }
        
        public string ToProcessPathId
        {
            get
            {
                return Format.GetString(_toProcessPathId);
            }
        }

        public string ToProcessSegmentId
        {
            get
            {
                return Format.GetString(_toProcesssegmentId);
            }
        }

        public string ToProcessSegmentVersion
        {
            get
            {
                return Format.GetString(_toProcesssegmentVersion);
            }
        }

        public string ToAreaId
        {
            get
            {
                return Format.GetString(_toAreaId);
            }
        }


        public string ToUsersequence
        {
            get
            {
                return Format.GetString(_toUserSequence);
            }
        }


        #endregion
    }
}
