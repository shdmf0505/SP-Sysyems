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
    /// 프 로 그 램 명  : 공정관리 > 작업시작
    /// 업  무  설  명  : Window Time 이 도래한 해당 작업장의 Lot을 조회한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2021-02-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WindowTimeArrivedPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region Local Variables
        public DataTable DataSource { get; private set; }
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region 생성자
        public WindowTimeArrivedPopup(DataTable dataSource)
		{
            this.DataSource = dataSource;

			InitializeComponent();
            InitializeEvent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            grdWindowTime.View.OptionsSelection.MultiSelect = false;
            grdWindowTime.GridButtonItem = GridButtonItem.None;
            grdWindowTime.ShowStatusBar = false;
            grdWindowTime.View.SetIsReadOnly();
            grdWindowTime.View.AddTextBoxColumn("LOTTYPE", 45).SetTextAlignment(TextAlignment.Center);
            grdWindowTime.View.AddTextBoxColumn("PRODUCTDEFID", 80).SetTextAlignment(TextAlignment.Center);
            grdWindowTime.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdWindowTime.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
            grdWindowTime.View.AddTextBoxColumn("STDTIMEPERMINUTE", 70).SetTextAlignment(TextAlignment.Right);
            grdWindowTime.View.AddTextBoxColumn("LIMITTIME", 140).SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdWindowTime.View.AddTextBoxColumn("LEFTTIME_HOUR", 70).SetTextAlignment(TextAlignment.Right);
            grdWindowTime.View.PopulateColumns();
        }
        #endregion

        #region Event
        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        private void InitializeEvent()
		{
            this.Load += WindowTimeArrivedPopup_Load;
            btnClose.Click += BtnClose_Click;
            btnLink.Click += BtnLink_Click;
		}

        private void WindowTimeArrivedPopup_Load(object sender, EventArgs e)
        {
            grdWindowTime.DataSource = this.DataSource;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnLink_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Private Function
        #endregion
    }
}
