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
	/// 프 로 그 램 명  : LOT 메시지 팝업 View
	/// 업  무  설  명  : 안 읽은 LOT 메시지를 보여주는 팝업
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-05
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LotMessagePopupView : SmartPopupBaseForm
	{
		#region Local Variables

		public DataTable MessageDataSource
		{
			get { return grdMessageList.DataSource as DataTable; }
			set { grdMessageList.DataSource = value; }
		}

		#endregion

		#region 생성자

		public LotMessagePopupView()
		{
			InitializeComponent();
		}

		public LotMessagePopupView(DataTable lotInfo)
		{
			InitializeComponent();

            InitializeEvent();
            InitializeGrid();

            InitializeLotInfo(lotInfo);
            //LotMessageSearch(lotInfo);
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// LOT 정보 초기화
		/// </summary>
		private void InitializeLotInfo(DataTable lotInfo)
		{
			grdLotInfo.ColumnCount = 1;
			grdLotInfo.LabelWidthWeight = "40%";
			grdLotInfo.SetInvisibleFields("PROCESSSEGMENTID", "PROCESSSEGMENTVERSION");

			grdLotInfo.DataSource = lotInfo;
		}

		/// <summary>        
		/// 그리드를 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdMessageList.View.SetIsReadOnly();
            grdMessageList.View.EnableRowStateStyle = false;
			grdMessageList.GridButtonItem = GridButtonItem.None;

			grdMessageList.View.AddTextBoxColumn("SEQUENCE")
				.SetIsHidden();
			grdMessageList.View.AddTextBoxColumn("CREATOR", 80)
				.SetTextAlignment(TextAlignment.Center);
			grdMessageList.View.AddTextBoxColumn("WRITEPROCESSSEGMENT", 130);//입력공정
			grdMessageList.View.AddTextBoxColumn("TITLE", 100);
			grdMessageList.View.AddTextBoxColumn("ISREAD", 60)
				.SetTextAlignment(TextAlignment.Center);
			grdMessageList.View.AddTextBoxColumn("MESSAGE")
				.SetIsHidden();

			grdMessageList.View.PopulateColumns();
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		private void InitializeEvent()
		{
			btnClose.Click += BtnClose_Click;
			grdMessageList.View.FocusedRowChanged += View_FocusedRowChanged;
		}


		/// <summary>
		/// 읽은 메시지 UPDATE
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClose_Click(object sender, EventArgs e)
		{
            Close();
		}

		/// <summary>
		/// 메시지 Row Change시 해당 메시지 View
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(e.FocusedRowHandle < 0) return;

			string isRead = Format.GetString(grdMessageList.View.GetFocusedRowCellValue("ISREAD"));
			if(isRead.Equals("N") || string.IsNullOrEmpty(isRead))
			{
                grdMessageList.View.SetFocusedRowCellValue("ISREAD", "Y");
            }

			string comment = Format.GetString(grdMessageList.View.GetFocusedRowCellValue("MESSAGE"));
            txtTitle.Text = Format.GetString(grdMessageList.View.GetFocusedRowCellValue("TITLE"));
			rtbMessage.Rtf = comment;

            grdMessageList.View.UpdateCurrentRow();
		}

		#endregion

		#region Private Function
		/// <summary>
		/// 메시지 조회
		/// </summary>
		private void LotMessageSearch(DataTable lotInfo)
		{
			string lotId = lotInfo.AsEnumerable().Select(r => r.Field<string>("LOTID")).Distinct().FirstOrDefault().ToString();
			string productDefId = lotInfo.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();
			string productDefVersion = lotInfo.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
			string processSegmentId = lotInfo.AsEnumerable().Select(r => r.Field<string>("PROCESSSEGMENTID")).Distinct().FirstOrDefault().ToString();

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LOTID", lotId);
			param.Add("PRODUCTDEFID", productDefId);
			param.Add("PRODUCTDEFVERSION", productDefVersion);
			param.Add("PROCESSSEGMENTID", processSegmentId);
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            param.Add("SHOWTYPE", "Y");

			grdMessageList.DataSource = SqlExecuter.Query("GetLotMessageList", "10002", param);
		}
        #endregion

        #region Public Function

        public DataTable GetChangedRows()
        {
            return grdMessageList.GetChangedRows();
        }

        #endregion
    }
}