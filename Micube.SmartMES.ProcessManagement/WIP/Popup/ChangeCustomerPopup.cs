#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using DevExpress.XtraReports.UI;

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
    /// 프 로 그 램 명  : 공정관리 > 포장작업 > 고객변경
    /// 업  무  설  명  : 고객변경 팝업
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2020-03-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ChangeCustomerPopup : SmartPopupBaseForm, ISmartCustomPopup
	{
        #region ◆ Public Variables |
        /// <summary>
        /// DataRow
        /// </summary>
        public DataRow CurrentDataRow { get; set; }
        #endregion

        #region ◆ Private Variables |
        /// <summary>
        /// View List
        /// </summary>
        private DataTable viewList;
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewList"></param>
        public ChangeCustomerPopup(DataTable dt)
		{
			InitializeComponent();

            this.viewList = dt;

            if (!this.IsDesignMode())
			{
                InitializeContent();
                InitializeEvent();
			}

            if (viewList != null && viewList.Rows.Count > 0)
            {
                grdPackingList.DataSource = viewList;

                grdPackingList.View.CheckedAll(true);
            }
		}
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        protected void InitializeContent()
        {
            InitializeControls();
        }

        #region ▶ Control Initialize |
        /// <summary>
        /// Control Initialize
        /// </summary>
        private void InitializeControls()
        {
            #region - 포장 Grid 설정 |
            grdPackingList.GridButtonItem = GridButtonItem.None;
            grdPackingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdPackingList.View.SetIsReadOnly();
            grdPackingList.SetIsUseContextMenu(false);
            // CheckBox 설정

            grdPackingList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
            grdPackingList.View.AddTextBoxColumn("BOXNO", 120).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("WORKER", 100).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("DEFECTQTY", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PACKINGDATE", 180).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("XOUT", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("CARD", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PACK", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("CASECNT", 70).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
            grdPackingList.View.AddTextBoxColumn("PATHTYPE", 70).SetIsHidden();

            grdPackingList.View.PopulateColumns();
            #endregion

            #region - 고객사 COMBOBOX |
            cboCustomer.DisplayMember = "CUSTOMERNAME";
            cboCustomer.ValueMember = "CUSTOMERID";
            cboCustomer.ShowHeader = false;
            cboCustomer.UseEmptyItem = false; 
            #endregion
        }
        #endregion
        #endregion

        #region ◆ Event |
        /// <summary>
        /// Event 초기화
        /// </summary>
        private void InitializeEvent()
		{
            this.Load += Form_Load;

            this.schCusotmerID.KeyDown += SchCusotmerID_KeyDown;

            this.btnSearch.Click += BtnSearch_Click;
            btnSave.Click += BtnSave_Click;
            btnClose.Click += BtnClose_Click;
        }

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 고객 코드 / 명 TextBox Key Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SchCusotmerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchData();
            }
        }

        /// <summary>
        /// 고객정보 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Box Row 체크
            DataTable bxData = grdPackingList.View.GetCheckedRows();

            if (bxData == null || bxData.Rows.Count == 0)
            {
                ShowMessage("NotCreateBoxNo");
                return;
            }

            if(cboCustomer.GetSelectedDataRow() == null)
            {
                //고객사는 필수 입력입니다.
                throw MessageException.Create("REQUIREDINPUTCUSTOMER");
            }

            string customer = (cboCustomer.GetSelectedDataRow() as DataRowView).Row["CUSTOMERID"].ToString();

            MessageWorker worker = new MessageWorker("SavePackingChangeCustomer");
            worker.SetBody(new MessageBody()
            {
                { "UserId", UserInfo.Current.Id },
                { "CustomerId", customer },
                { "PackingList", bxData }
            });

            worker.Execute();

            this.Close();
        }

        /// <summary>
        /// 닫기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ◆ Function |

        /// <summary>
        /// 고객정보 조회
        /// </summary>
        private void SearchData()
        {
            string strCustomer = Format.GetString(schCusotmerID.GetValue(), "");
            if (string.IsNullOrEmpty(strCustomer))
            {
                //고객사는 필수 입력입니다.
                throw MessageException.Create("REQUIREDINPUTCUSTOMER");
            }

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("TXTCUSTOMERID", CommonFunction.changeArgString(strCustomer));

            DataTable dt = SqlExecuter.Query("GetCustomerList", "10001", param);

            if (dt == null || dt.Rows.Count == 0)
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
                return;
            }

            this.cboCustomer.DataSource =dt;
        }


        #endregion

    }
}
