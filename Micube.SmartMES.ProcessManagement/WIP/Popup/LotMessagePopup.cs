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
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > LOT 메시지 관리 > 팝업으로 보기 
	/// 업  무  설  명  : 조건에 맞는 LOT 메시지를 팝업으로 보여줌
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-05
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LotMessagePopup : SmartPopupBaseForm, ISmartCustomPopup
	{
		public DataRow CurrentDataRow { get; set; }

		#region Local Variables

		public int SelectedTabIndex { get; set; }


		#endregion

		#region 생성자

		public LotMessagePopup()
		{
			InitializeComponent();
		}

		public LotMessagePopup(DataTable dtResult)
		{
			InitializeComponent();

			if (!this.IsDesignMode())
			{
				InitializeEvent();
				InitializeGrid();
				ResultBind(dtResult);
			}
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 메시지 bind
		/// </summary>
		/// <param name="param"></param>
		private void ResultBind(DataTable dt)
		{
			grdMessageList.DataSource = dt;
		}

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdMessageList.View.SetIsReadOnly(true);
			grdMessageList.GridButtonItem = GridButtonItem.Export;
			grdMessageList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
			grdMessageList.View.SetIsReadOnly();

			grdMessageList.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
			grdMessageList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 100);
			grdMessageList.View.AddTextBoxColumn("PRODUCTDEFID", 100);
			grdMessageList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50);
			grdMessageList.View.AddTextBoxColumn("PRODUCTDEFNAME", 280);
			grdMessageList.View.AddTextBoxColumn("LOTID", 200);
			grdMessageList.View.AddTextBoxColumn("MESSAGE", 500)
				.SetIsHidden();

			grdMessageList.View.PopulateColumns();
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			grdMessageList.View.FocusedRowChanged += View_FocusedRowChanged;
			btnClose.Click += BtnClose_Click;
		}

		/// <summary>
		/// 그리드 ROW 선택 시 메시지 VIEW
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(e.FocusedRowHandle < 0) return;

			string comment = Format.GetString(grdMessageList.View.GetRowCellValue(e.FocusedRowHandle, "MESSAGE"));
			rtpComment.Rtf = comment;
		}

		/// <summary>
		/// 닫기 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion
	}
}