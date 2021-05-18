#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > Lot관리 > Sample Routing
    /// 업  무  설  명  : Sample Routing 화면의 치공구선택 팝업
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-12-11
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class SampleRoutingDurablePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region Local Variables
        public string EnterpriseId { get; set; }
        public string PlantId { get; set; }
        public string DurableType { get; set; }
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region 생성자
        public SampleRoutingDurablePopup()
		{
			InitializeComponent();
            InitializeEvent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            grdDurable.GridButtonItem = GridButtonItem.None;
            grdDurable.View.OptionsSelection.MultiSelect = false;
            grdDurable.View.SetAutoFillColumn("DURABLEDEFNAME");
            grdDurable.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdDurable.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            grdDurable.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdDurable.View.SetIsReadOnly();
            grdDurable.View.AddTextBoxColumn("DURABLEDEFID", 130);                 // 치공구 ID
            grdDurable.View.AddTextBoxColumn("DURABLEDEFVERSION", 90)              // 치공구 버전
                .SetTextAlignment(TextAlignment.Center);
            grdDurable.View.AddTextBoxColumn("DURABLEDEFNAME", 200);               // 치공구 명
            grdDurable.View.AddTextBoxColumn("DURABLETYPE", 130).SetIsHidden();    // 치공구 구분
            grdDurable.View.PopulateColumns();
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
            txtDurable.KeyDown += txt_KeyDown;
            grdDurable.View.CheckStateChanged += View_CheckStateChanged;
		}

        /// <summary>
        /// 한 ROW만 체크 가능하도록 강제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_CheckStateChanged(object sender, EventArgs e)
        {
            grdDurable.View.CheckStateChanged -= View_CheckStateChanged;
            grdDurable.View.CheckedAll(false);
            grdDurable.View.CheckRow(grdDurable.View.GetVisibleIndex(grdDurable.View.FocusedRowHandle), true);
            grdDurable.View.CheckStateChanged += View_CheckStateChanged;

            DataTable dt = grdDurable.View.GetCheckedRows();
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
            this.FireSelected(this.grdDurable.View);
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
            DataTable dt = new SqlQuery("GetLotDurable", "10001"
                , $"ENTERPRISEID={this.EnterpriseId}"
                , $"PLANTID={this.PlantId}"
                , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                , $"DURABLE={txtDurable.Text}"
                , $"DURABLETYPE={this.DurableType}"
                ).Execute();
            grdDurable.DataSource = dt;
        }

        #endregion

        #region

        #endregion

        #region Private Function
        #endregion
    }
}
