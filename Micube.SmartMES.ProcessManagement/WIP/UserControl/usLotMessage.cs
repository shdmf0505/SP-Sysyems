#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공통 > Lot Message 조회용 UserControl
    /// 업  무  설  명  : Lot Message 이력 및 내용확인 UserControl
    /// 생    성    자  : 박정훈
    /// 생    성    일  : 2019-09-05
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class usLotMessage : UserControl
    {
        #region ◆ Variables

        #endregion
        
        #region ◆ Public Properties

        /// <summary>
        /// Message Grid DataSource
        /// </summary>
        public object MessageDataSource
        {
            get
            {
                return grdMessage.DataSource;
            }
            set
            {
                grdMessage.DataSource = value;
            }
        } 
        #endregion

        #region ◆ 생성자 |
        /// <summary>
        /// 생성자
        /// </summary>
        public usLotMessage()
        {
            InitializeComponent();
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        public void InitializeControls()
        {
            InitializeGrid();

            InitializeEvent();
        }

        /// <summary>
        /// Control Event 설정
        /// </summary>
        private void InitializeEvent()
        {
            grdMessage.View.FocusedRowChanged += MessageView_FocusedRowChanged;
        }

        #region ▶ Grid Control 초기화 |
        /// <summary>
        /// Grid Control 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMessage.GridButtonItem = GridButtonItem.None;
            grdMessage.ShowButtonBar = false;
            grdMessage.ShowStatusBar = false;

            grdMessage.View.SetIsReadOnly();

            grdMessage.View.AddTextBoxColumn("LOTID", 180).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("TXNHISTKEY", 180).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("PATHSEQUENCE", 70).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("USERSEQUENCE", 60).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
            grdMessage.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
            grdMessage.View.AddTextBoxColumn("AREANAME", 150);
            grdMessage.View.AddTextBoxColumn("MESSAGETYPE", 140).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITER", 60).SetTextAlignment(TextAlignment.Center);
            grdMessage.View.AddTextBoxColumn("WRITEDATE", 140).SetTextAlignment(TextAlignment.Center);

            grdMessage.View.PopulateColumns();
        }
        #endregion
        #endregion

        #region ◆ Event |
        /// <summary>
        /// 메시지 Grid Row Focused Row Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                rtbMessage.Text = "";
                return;
            }

            DataRow dr = this.grdMessage.View.GetFocusedDataRow();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("P_TXNHISTKEY", dr["TXNHISTKEY"].ToString());
            param.Add("P_LOTID", dr["LOTID"].ToString());
            param.Add("P_PROCESSSEGMENTID", dr["PROCESSSEGMENTID"].ToString());
            param.Add("P_USERSEQUENCE", dr["USERSEQUENCE"].ToString());

            DataTable dt = SqlExecuter.Query("SelectLotMessage", "10001", param);

            if (dt.Rows.Count < 1)
            {
                return;
            }

            this.txtTitle.Text = dt.Rows[0]["TITLE"].ToString();
            rtbMessage.Rtf = dt.Rows[0]["MESSAGE"].ToString();
        }

        /// <summary>
        /// Grid에 데이터 조회 후 Binding
        /// </summary>
        public void SetDatasource(string strLotID, string strProductDefID, string strProductDefVersion, string strSegmentID)
        {
            Dictionary<string, object> messageParam = new Dictionary<string, object>();
            messageParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType); 
            messageParam.Add("LOTID", strLotID);

            DataTable dt = SqlExecuter.Query("SelectLotHistoryMessage", "10001", messageParam);

            this.grdMessage.DataSource = dt;
        }

        public  void ClearDatas()
        {
            grdMessage.View.ClearDatas();

            this.txtTitle.Text = string.Empty;
            rtbMessage.Rtf = string.Empty;
        }
        #endregion
    }
}
