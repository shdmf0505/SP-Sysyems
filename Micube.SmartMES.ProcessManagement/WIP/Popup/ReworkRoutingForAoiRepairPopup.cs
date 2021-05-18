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
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 공정작업 > 재작업 Lot 투입 > 재작업 라우팅 팝업
    /// 업  무  설  명  : 재작업 라우팅을 선택한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-10-17
    /// 수  정  이  력  : 2019-12-26, 박정훈, Region 및 주석정리
    ///                                      재작업 Routing의 대공정 조회조건 추가
    /// 
    /// 
    /// </summary>
    public partial class ReworkRoutingForAoiRepairPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		#region Local Variables
        public string LotId { get; set; }
        public string TopProcessSegmentId { get; set; } = "";
		public DataRow CurrentDataRow { get; set; }

        public string ResourceId { get; set; }
        public string AreaId { get; set; }

        // 공정 ID
        public string ProcessSegmentId { get; set; }
        #endregion

        #region 생성자
        public ReworkRoutingForAoiRepairPopup()
		{
			InitializeComponent();
            InitializeCombobox();

            InitializeEvent();
            InitializeGrid();
		}

        private void InitializeCombobox()
        {
            cboProcessClass.DisplayMember = "CODENAME";
            cboProcessClass.ValueMember = "CODEID";
            cboProcessClass.UseEmptyItem = true;
            cboProcessClass.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboProcessClass.ShowHeader = false;
            // 쿼리 틀린거 같음
            DataTable processClass = new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProcessClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
            cboProcessClass.DataSource = processClass;

            // 인계자원
            cboResourceId.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboResourceId.Editor.ShowHeader = false;
            cboResourceId.Editor.ValueMember = "RESOURCEID";
            cboResourceId.Editor.DisplayMember = "RESOURCENAME";
            cboResourceId.Editor.UseEmptyItem = true;
            cboResourceId.Editor.EmptyItemValue = "";
            cboResourceId.Editor.EmptyItemCaption = "";

            // 대공정
            cboTopSegment.DisplayMember = "PROCESSSEGMENTCLASSNAME";
            cboTopSegment.ValueMember = "PROCESSSEGMENTCLASSID";
            cboTopSegment.UseEmptyItem = true;
            cboTopSegment.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
            cboTopSegment.ShowHeader = false;

            DataTable segmentClass = new SqlQuery("GetProcessSegmentClassByType", "10001", $"PROCESSSEGMENTCLASSTYPE=TopProcessSegmentClass", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
            cboTopSegment.DataSource = segmentClass;

            // 공정ID가 있을 경우 대공정을 조회하여 ComboBox가 기본 선택되도록 함
            if (!string.IsNullOrWhiteSpace(ProcessSegmentId))
            {
                DataTable topDt = new SqlQuery("GetProcessSegmentClassBySegmentID", "10001", $"P_PROCESSCLASSTYPE=TopProcessSegmentClass", $"PROCESSSEGMENTID={ProcessSegmentId}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();

                if (topDt == null || topDt.Rows.Count <= 0) return;

                DataRow dr = topDt.Rows[0];

                cboTopSegment.EditValue = dr["PROCESSSEGMENTCLASSID"].ToString();
            }
        }

        private void InitializeGrid()
        {
            InitializeProcessDefGrid();
            InitializeProcessPathGrid();
        }

        private void InitializeProcessDefGrid()
        {
            grdProcessDef.View.OptionsSelection.MultiSelect = false;
            grdProcessDef.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdProcessDef.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdProcessDef.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdProcessDef.View.SetIsReadOnly();

            grdProcessDef.View.AddTextBoxColumn("APPLICATIONTYPE", 80);         // 적용구분
            grdProcessDef.View.AddTextBoxColumn("REWORKTYPE", 120)              // 재작업구분
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("TOPPROCESSSEGMENTID", 70)      // 대공정
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("REWORKNUMBER", 100)            // 재작업번호
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("REWORKVERSION", 80)            // 재작업버전
                .SetTextAlignment(TextAlignment.Left);
            grdProcessDef.View.AddTextBoxColumn("REWORKNAME", 100)              // 재작업명
                .SetTextAlignment(TextAlignment.Left);

            grdProcessDef.View.PopulateColumns();
        }

        private void InitializeProcessPathGrid()
        {
            grdProcessPath.View.OptionsSelection.MultiSelect = false;
            grdProcessPath.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdProcessPath.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdProcessPath.View.AddTextBoxColumn("USERSEQUENCE", 80)        // 공정순서
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)   // 공정코드
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();
            grdProcessPath.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200) // 공정명
                .SetTextAlignment(TextAlignment.Left)
                .SetIsReadOnly();
            grdProcessPath.View.PopulateColumns();
            grdProcessPath.View.CheckMarkSelection.ShowCheckBoxHeader = false;
        }

        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
		{
            grdProcessDef.View.CheckStateChanged += View_CheckStateChanged;

            btnSearch.Click += BtnSearch_Click;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;

            txtReworkNumber.KeyDown += TxtReworkNumber_KeyDown;
            txtReworkName.KeyDown += TxtReworkName_KeyDown;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Format.GetString(cboResourceId.EditValue)))
            {
                // 인계 자원을 선택하시기 바랍니다.
                ShowMessage("SelectTransitResource");
                return;
            }

            this.DialogResult = DialogResult.OK;

            ResourceId = cboResourceId.EditValue.ToString();
            AreaId = cboResourceId.Editor.GetColumnValue("AREAID").ToString();

            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.CurrentDataRow = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            // 한 행만 체크 가능
            grdProcessDef.View.CheckStateChanged -= View_CheckStateChanged;
            grdProcessDef.View.CheckedAll(false);

            grdProcessDef.View.CheckRow(grdProcessDef.View.GetFocusedDataSourceRowIndex(), true);
            grdProcessDef.View.CheckStateChanged += View_CheckStateChanged;

            DataTable checkedRows = grdProcessDef.View.GetCheckedRows();

            if (checkedRows.Rows.Count > 0)
            {
                this.CurrentDataRow = checkedRows.Rows[0];
                SearchProcessPath(checkedRows.Rows[0]["REWORKNUMBER"].ToString(), checkedRows.Rows[0]["REWORKVERSION"].ToString());
            }
        }

        private void SearchProcessPath(string processDefId, string processDefVersion)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PROCESSDEFID", processDefId);
            param.Add("PROCESSDEFVERSION", processDefVersion);

            DataTable transitResource = SqlExecuter.Query("GetTransitResourceForAoiRepair", "10001", param);

            string primaryResource = "";

            for (int i = 0; i < transitResource.Rows.Count; i++)
            {
                DataRow row = transitResource.Rows[i];

                string isPrimaryResource = Format.GetString(row["ISPRIMARYRESOURCE"]);

                if (isPrimaryResource == "Y")
                {
                    primaryResource = Format.GetString(row["RESOURCEID"]);
                    break;
                }
            }

            cboResourceId.Editor.DataSource = transitResource;
            cboResourceId.EditValue = primaryResource;

            grdProcessPath.DataSource = new SqlQuery("GetProcessPathList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PROCESSDEFID={processDefId}", $"PROCESSDEFVERSION={processDefVersion}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}").Execute();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("PLANTID", UserInfo.Current.Plant);
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_LOTID", LotId);
            param.Add("P_PROCESSCLASSTYPE", cboProcessClass.EditValue);
            param.Add("REWORKNUMBER", txtReworkNumber.Text);
            param.Add("REWORKNAME", txtReworkName.Text);
            param.Add("TOPPROCESSSEGMENTID", cboTopSegment.EditValue);

            DataTable dt = SqlExecuter.Query("SelectReworkRouting", "10001", param);

            grdProcessDef.DataSource = dt;
        }

        #region ▶ Text Event |

        #region - 재작업 번호 조회 |
        /// <summary>
        /// 재작업 번호 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReworkNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(this, null);
            }
        }
        #endregion

        #region - 재작업명 조회 |
        /// <summary>
        /// 재작업명 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtReworkName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(this, null);
            }
        }
        #endregion

        #endregion
        #endregion

        #region

        #endregion

        #region Private Function
        #endregion
    }
}
