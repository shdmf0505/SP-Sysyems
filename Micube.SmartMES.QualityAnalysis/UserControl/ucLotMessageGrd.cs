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

namespace Micube.SmartMES.QualityAnalysis
{
    /// <summary>
    /// 프 로 그 램 명  : 공통 > Lot Message 조회용 UserControl
    /// 업  무  설  명  : Lot Message 이력 및 내용확인 UserControl
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-09-05
    /// 수  정  이  력  : ucLotMessage에 등록버튼 추가하여 새로 생성
    /// 
    /// 
    /// </summary>
    public partial class ucLotMessageGrd : UserControl
    {
        #region ◆ Variables
        Dictionary<string, object> _data;
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
        public ucLotMessageGrd()
        {
            InitializeComponent();
            InitializeGrid();

            InitializeEvent();
        }
        #endregion

        #region ◆ Control 초기화 |
        /// <summary>
        /// Control 초기화
        /// </summary>
        public void InitializeControls()
        {

        }

        /// <summary>
        /// Control Event 설정
        /// </summary>
        private void InitializeEvent()
        {
            grdMessage.View.FocusedRowChanged += MessageView_FocusedRowChanged;

            //MESSAGE 등록 버튼 클릭
            btnWrite.Click += (s, e) =>
            {
                ShipmentInspMessagePopup popup = new ShipmentInspMessagePopup();
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.FormBorderStyle = FormBorderStyle.Sizable;
                popup.ShowDialog();
            };
        }

        #region ▶ Grid Control 초기화 |
        /// <summary>
        /// Grid Control 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdMessage.GridButtonItem = GridButtonItem.None;
            grdMessage.ShowButtonBar = false;

            grdMessage.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMessage.View.SetIsReadOnly();
            grdMessage.View.SetAutoFillColumn("TITLE");

            // 등록자
            grdMessage.View.AddTextBoxColumn("CREATOR", 100).SetLabel("CREATEUSER");
            // 내용
            grdMessage.View.AddTextBoxColumn("TITLE", 200);
            // 내용(Rich Text Format)
            grdMessage.View.AddTextBoxColumn("MESSAGE", 200).SetIsHidden();

            grdMessage.View.PopulateColumns();

            grdMessage.View.OptionsView.ShowIndicator = false;
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

            string rtf = grdMessage.View.GetRowCellValue(e.FocusedRowHandle, "MESSAGE").ToString();

            rtbMessage.Rtf = rtf;
        }

        /// <summary>
        /// Grid에 데이터 조회 후 Binding
        /// </summary>
        public void SetDatasource(string strLotID, string strProductDefID, string strProductDefVersion, string strSegmentID)
        {
            Dictionary<string, object> messageParam = new Dictionary<string, object>();
            messageParam.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            messageParam.Add("PLANTID", UserInfo.Current.Plant);
            messageParam.Add("LOTID", strLotID);
            messageParam.Add("PRODUCTDEFID", strProductDefID);
            messageParam.Add("PRODUCTDEFVERSION", strProductDefVersion);
            messageParam.Add("PROCESSSEGMENTID", strSegmentID);
            messageParam.Add("PROCESSSEGMENTVERSION", "*");

            DataTable dt = SqlExecuter.Query("SelectMessageByProcess", "10002", messageParam);

            this.grdMessage.DataSource = dt;
        }
        #endregion

        #region Public Function
        public void getToSaveData()
        {

        }
        #endregion
    }
}
