#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Data;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > Lot관리 > Sample Routing
    /// 업  무  설  명  : Sample Routing 화면의 자원선택 팝업
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-11-22
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SampleRoutingResourcePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region Local Variables
        public string EnterpriseId { get; set; }
        public string PlantId { get; set; }
        public string ProcessSegmentId { get; set; }
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region 생성자
        public SampleRoutingResourcePopup()
		{
			InitializeComponent();
            InitializeEvent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            grdResource.View.OptionsSelection.MultiSelect = false;
            grdResource.GridButtonItem = GridButtonItem.None;
            grdResource.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdResource.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdResource.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdResource.View.SetIsReadOnly();
            grdResource.View.AddTextBoxColumn("RESOURCEID", 130);                   // 자원 ID
            grdResource.View.AddTextBoxColumn("AREAID", 90);                        // 작업장 ID
            grdResource.View.AddTextBoxColumn("AREANAME", 160);                     // 작업장 명
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSID", 100);             // 설비그룹 ID
            grdResource.View.AddTextBoxColumn("EQUIPMENTCLASSNAME", 200);           // 설비그룹 명
            grdResource.View.AddTextBoxColumn("RESOURCEVERSION").SetIsHidden();     // 자원 버전
            grdResource.View.PopulateColumns();
        }
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
		{
            btnSearch.Click += BtnSearch_Click;
            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnCancel_Click;
            txtResource.KeyDown += txt_KeyDown;
            txtArea.KeyDown += txt_KeyDown;
            txtEquipmentClass.KeyDown += txt_KeyDown;
            grdResource.View.CheckStateChanged += View_CheckStateChanged;
		}

        /// <summary>
        /// 한 ROW만 체크 가능하도록 강제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            grdResource.View.CheckStateChanged -= View_CheckStateChanged;
            grdResource.View.CheckedAll(false);
            grdResource.View.CheckRow(grdResource.View.GetFocusedDataSourceRowIndex(), true);
            grdResource.View.CheckStateChanged += View_CheckStateChanged;

            DataTable dt = grdResource.View.GetCheckedRows();
            if(dt.Rows.Count > 0)
            {
                this.CurrentDataRow = dt.Rows[0];
            }
            else
            {
                this.CurrentDataRow = null;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.FireSelected(this.grdResource.View);
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.CurrentDataRow = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(this, null);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new SqlQuery("GetLotResource", "10001"
                , $"ENTERPRISEID={this.EnterpriseId}"
                , $"PLANTID={this.PlantId}"
                , $"PROCESSSEGMENTID={this.ProcessSegmentId}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                , $"RESOURCE={txtResource.Text}"
                , $"AREA={txtArea.Text}"
                , $"EQUIPMENTCLASS={txtEquipmentClass.Text}"
                ).Execute();
            grdResource.DataSource = dt;
        }

        #endregion

        #region

        #endregion

        #region Private Function
        #endregion
    }
}
