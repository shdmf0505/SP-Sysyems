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
    /// <summary>
    /// 프 로 그 램 명  : 품질관리 > 출하검사 > 결과 등록 및 작업완료
    /// 업  무  설  명  : 출하검사 결과등록시 NG항목들일때 재작업라우팅을 설정할 수 있게한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-03-06
    /// 수  정  이  력  : 2020-03-07 유태근 / 데이터 바인딩 및 화면, 컬럼사이즈 조정
    /// 
    /// 
    ///
    /// </summary>
    public partial class SelectReworkRoutingShipToFinish : SmartPopupBaseForm
    {
        #region Local Variables

        private string _reworkResourceId = "";
        private string _reworkProcessPathId = "";
        private string _reworkAreaId = "";
        private string _reworkProcessDefId = "";
        private string _reworkProcessDefVersion = "";
        private string _reworkProcesssegmentId = "";
        private string _reworkProcesssegmentVersion = "";
        private string _reworkUserSequence = "";

        private string _paramProcessDefId = "";
        private string _paramProcessDefVersion = "";

        #endregion

        #region
        public SelectReworkRoutingShipToFinish()
        {
            InitializeComponent();

            InitializeEvent();
            InitializeControls();
            InitializePathGrd();
            InitializePathResourceGrd();
        }


        #endregion

        #region 초기화

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void InitializeControls()
        {
            lblInfo.Text = Language.GetMessage("SelectResourceToFinish").Message;

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            DataTable resourceList = SqlExecuter.Query("GetReworkRoutingShipment", "10001", param);

            cboReworkRouting.Editor.PopupWidth = 430;
            cboReworkRouting.Editor.SetVisibleColumns("PROCESSDEFID", "PROCESSDEFVERSION", "PROCESSDEFNAME", "DESCRIPTION");
            cboReworkRouting.Editor.SetVisibleColumnsWidth(100, 80, 100,100);
            cboReworkRouting.Editor.ComboBoxColumnShowType = Framework.SmartControls.ComboBoxColumnShowType.Custom;
            cboReworkRouting.Editor.ShowHeader = true;
            cboReworkRouting.Editor.ValueMember = "PROCESSDEFIDVERSION";
            cboReworkRouting.Editor.DisplayMember = "PROCESSDEFNAME";
            cboReworkRouting.Editor.UseEmptyItem = false;
            cboReworkRouting.Editor.DataSource = resourceList;
        }

        #endregion

        #region 그리드 초기화

        /// <summary>
        /// 라우팅 순서 그리드
        /// </summary>
        private void InitializePathGrd()
        {
            grdReworkPath.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdReworkPath.View.CheckMarkSelection.MultiSelectCount = 1;

            grdReworkPath.View.SetIsReadOnly();

            grdReworkPath.View.AddTextBoxColumn("USERSEQUENCE", 100)
                .SetTextAlignment(TextAlignment.Right);
            grdReworkPath.View.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            grdReworkPath.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdReworkPath.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetTextAlignment(TextAlignment.Center);

            grdReworkPath.View.PopulateColumns();
        }

        /// <summary>
        /// 라우팅상의 공정에 해당하는 자원 선택 그리드
        /// </summary>
        private void InitializePathResourceGrd()
        {
            grdReworkResource.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdReworkResource.View.CheckMarkSelection.MultiSelectCount = 1;

            grdReworkResource.View.SetIsReadOnly();

            grdReworkResource.View.AddTextBoxColumn("AREAID", 100);
            grdReworkResource.View.AddTextBoxColumn("AREANAME", 180);
            grdReworkResource.View.AddTextBoxColumn("RESOURCEID", 100);
            grdReworkResource.View.AddTextBoxColumn("RESOURCENAME", 200);

            grdReworkResource.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            btnSave.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancle_Click;

            cboReworkRouting.Editor.EditValueChanged += Editor_EditValueChanged;
            grdReworkPath.View.CheckStateChanged += View_CheckStateChanged;
        }

        /// <summary>
        /// 라우팅상의 상세공정 클릭시 자원리스트 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            if (!grdReworkPath.View.IsRowChecked(grdReworkPath.View.FocusedRowHandle))
            {
                grdReworkResource.DataSource = null;
                return;
            }

            DataRow row = grdReworkPath.View.GetFocusedDataRow();

            if (row == null) return;

            string paramProcesssegmentId = Format.GetString(row["PROCESSSEGMENTID"]);
            string paramProcesssegmentVersion = Format.GetString(row["PROCESSSEGMENTVERSION"]);
            string paramProcessDefId = Format.GetString(row["PROCESSDEFID"]);
            string paramProcessDefVersion = Format.GetString(row["PROCESSDEFVERSION"]);
            string paramProcessPathId = Format.GetString(row["PROCESSPATHID"]);
            string paramEnterpriseId = UserInfo.Current.Enterprise;
            string paramPlantId = UserInfo.Current.Plant;

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "PROCESSDEFID", paramProcessDefId },
                { "PROCESSDEFVERSION", paramProcessDefVersion} ,
                { "PROCESSSEGMENTID", paramProcesssegmentId },
                { "PROCESSSEGMENTVERSION", paramProcesssegmentVersion },
                { "PROCESSPATHID", paramProcessPathId },
                { "ENTERPRISEID", UserInfo.Current.Enterprise },
                { "PLANTID", UserInfo.Current.Plant },
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
            };

            DataTable dt = SqlExecuter.Query("GetReworkRoutingResourceShipment", "10001", param);

            grdReworkResource.DataSource = dt;
        }

        /// <summary>
        /// 재작업 라우팅 콤보박스 선택 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            _paramProcessDefId = Format.GetString(cboReworkRouting.Properties.GetDataSourceValue("PROCESSDEFID", cboReworkRouting.Properties.GetDataSourceRowIndex("PROCESSDEFIDVERSION", cboReworkRouting.Editor.EditValue)));
            _paramProcessDefVersion = Format.GetString(cboReworkRouting.Properties.GetDataSourceValue("PROCESSDEFVERSION", cboReworkRouting.Properties.GetDataSourceRowIndex("PROCESSDEFIDVERSION", cboReworkRouting.Editor.EditValue)));

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"PROCESSDEFID", _paramProcessDefId},
                {"PROCESSSEGMENTVERSION", _paramProcessDefVersion},
                {"ENTERPRISEID", UserInfo.Current.Enterprise},
                {"PLANTID", UserInfo.Current.Plant },
                {"LANGUAGETYPE", UserInfo.Current.LanguageType }
            };

            DataTable dt = SqlExecuter.Query("GetReworkRoutingProcessPathShipment", "10001", param);

            grdReworkPath.DataSource = dt;
        }

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataTable pathDt = grdReworkPath.View.GetCheckedRows();
            DataTable resourceDt = grdReworkResource.View.GetCheckedRows();

            if (pathDt.Rows.Count == 0 || resourceDt.Rows.Count == 0)
            {
                // 체크된 행 없을 때
                if (pathDt.Rows.Count == 0)
                    throw MessageException.Create("ReworkRoutingValidation"); // 재작업 라우팅을 선택 해주세요.
                else
                    throw MessageException.Create("SelectTransitResource"); // 인계 자원을 선택하시기 바랍니다.
            }

            _reworkProcessDefId = pathDt.Rows[0].GetString("PROCESSDEFID");
            _reworkProcessDefVersion = pathDt.Rows[0].GetString("PROCESSDEFVERSION");
            _reworkProcessPathId = pathDt.Rows[0].GetString("PROCESSPATHID");

            _reworkProcesssegmentId= pathDt.Rows[0].GetString("PROCESSSEGMENTID");
            _reworkProcesssegmentVersion = pathDt.Rows[0].GetString("PROCESSSEGMENTVERSION");
            _reworkUserSequence = pathDt.Rows[0].GetString("USERSEQUENCE");

            _reworkResourceId = resourceDt.Rows[0].GetString("RESOURCEID");
            _reworkAreaId = resourceDt.Rows[0].GetString("AREAID");

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 취소버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Property
        public string ReworkResourceId
        {
            get
            {
                return Format.GetString(_reworkResourceId);
            }
        }

        public string ReworkAreaId
        {
            get
            {
                return Format.GetString(_reworkAreaId);
            }
        }


        public string ReworkProcessDefId
        {
            get
            {
                return Format.GetString(_reworkProcessDefId);
            }
        }

        public string ReworkProcessDefVersion
        {
            get
            {
                return Format.GetString(_reworkProcessDefVersion);
            }
        }

        public string ReworkProcessPathId
        {
            get
            {
                return Format.GetString(_reworkProcessPathId);
            }
        }

        public string ReworkProcesSegmentId
        {
            get
            {
                return Format.GetString(_reworkProcesssegmentId);
            }
        }

        public string ReworkProcesSegmentVersion
        {
            get
            {
                return Format.GetString(_reworkProcesssegmentVersion);
            }
        }

        public string ReworkUserSequence
        {
            get
            {
                return Format.GetString(_reworkUserSequence);
            }
        }

        #endregion
    }
}
