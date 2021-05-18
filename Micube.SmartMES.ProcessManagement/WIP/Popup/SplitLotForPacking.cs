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
	/// 프 로 그 램 명  : 공정관리 > 포장관리 > 포장실적등록 > LOT분햘 팝업
	/// 업  무  설  명  : BOX 사양에 맞춰 LOT을 분할할 수 있다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-07-23
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class SplitLotForPacking : SmartPopupBaseForm, ISmartCustomPopup
	{
		#region Local Variables

		public DataRow CurrentDataRow { get; set; }

		//메인 화면에서 넘겨 준 LOT 수량, 포장 수량
		private Dictionary<string, object> _options = new Dictionary<string, object>();

		//분할 랏 이벤트
		//public delegate void SplitLotResultHandler(DataTable dt);
		//public event SplitLotResultHandler SplitLotResult;

		#endregion

		#region 생성자

		public SplitLotForPacking(DataTable dtLotInfo, Dictionary<string, object> options)
		{
			InitializeComponent();

			InitializeEvent();
			InitializeGrid();
			InitializeLotInfo(dtLotInfo);

			//LOT ID, 기준 LOT 수량, 포장 수량
			txtLotId.Text = dtLotInfo.AsEnumerable().First().Field<string>("LOTID");
			_options = options;
			numBoxQty.Text = _options["PACKINGQTY"].ToString();
		}

		public SplitLotForPacking()
		{
			InitializeComponent();
		}

		#endregion

		#region Event
		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			numSplitLotQty.EditValueChanged += NumSplitLotQty_EditValueChanged;
			btnPrintLotCard.Click += BtnPrintLotCard_Click;
			btnClose.Click += BtnClose_Click;
			btnSplitLot.Click += BtnSplitLot_Click;
			btnSave.Click += BtnSave_Click;
		}

		/// <summary>
		/// 닫기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// 분할 LOT 저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSave_Click(object sender, EventArgs e)
		{
			string lotId = txtLotId.Text;

			DataTable dtSplitLotList = grdLotList.DataSource as DataTable;

			MessageWorker worker = new MessageWorker("SplitLotUtil");
			worker.SetBody(new MessageBody()
			{
				{ "LOTID", lotId },
				{ "splitLotList", dtSplitLotList }
			});

			worker.Execute();
			
			//LOT 카드 출력
			//BtnPrintLotCard_Click(null, null);

			this.Close();

			#region 주석
			/*
			DataTable dtLotList = grdLotList.DataSource as DataTable;

			dtLotList.Columns.Add("PRODUCTDEFID", typeof(string));
			dtLotList.Columns.Add("PRODUCTDEFNAME", typeof(string));
			dtLotList.Columns.Add("PRODUCTDEFVERSION", typeof(string));

			foreach(DataRow row in dtLotList.Rows)
			{
				row["PRODUCTDEFID"] = (grdLotInfo.DataSource as DataTable).AsEnumerable().First().Field<string>("PRODUCTDEFID");
				row["PRODUCTDEFNAME"] = (grdLotInfo.DataSource as DataTable).AsEnumerable().First().Field<string>("PRODUCTDEFNAME");
				row["PRODUCTDEFVERSION"] = (grdLotInfo.DataSource as DataTable).AsEnumerable().First().Field<string>("PRODUCTDEFVERSION");
			}

			//this.SplitLotResult(dtLotList);
			DialogResult = DialogResult.OK;
			this.Close();
			*/
			#endregion
		}

		/// <summary>
		/// 분할 수량 변경되었을 때 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NumSplitLotQty_EditValueChanged(object sender, EventArgs e)
		{
			double packingQty = Format.GetDouble(numBoxQty.Text, 0);
			double splitQt = Format.GetDouble(numSplitLotQty.Text, 0);
			if(packingQty < splitQt)
			{
				//분할 수량은 포장 수량보다 클 수 없습니다.
				throw MessageException.Create("");
			}
		}

		/// <summary>
		/// 랏카드 출력
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPrintLotCard_Click(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		/// LOT 분할
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnSplitLot_Click(object sender, EventArgs e)
		{
			if(numSplitLotQty.Equals(0))
			{
				//분할 수량을(를) 입력해 주십시오.
				ShowMessage("InputQty", Language.Get("SPLITQTY"));
				return;
			}

			double splitQty = Format.GetDouble(numSplitLotQty.Text, 0);
			double lotQty = Format.GetDouble(_options["LOTQTY"], 0);
			double rows = Math.Ceiling(lotQty / splitQty);

			DataTable dtLotList = grdLotList.DataSource as DataTable;
			for (int i = 0; i<rows; i++)
			{
				//분할수량
				double qty = (i == (rows-1)) ? ((lotQty % splitQty == 0) ? splitQty : lotQty % splitQty) : splitQty;
				dtLotList.Rows.Add("SplitLot", qty);
			}
		}

		#endregion

		#region Private Function
		/// <summary>
		/// LOT 정보 초기화
		/// </summary>
		private void InitializeLotInfo(DataTable dt)
		{
			grdLotInfo.ColumnCount = 2;
			grdLotInfo.LabelWidthWeight = "40%";
			grdLotInfo.SetInvisibleFields("CUSTOMERID", "USERSEQUENCE", "PROCESSSEGMENTNAME", "NEXTPROCESSSEGMENTNAME", "PREVPROCESSSEGMENTNAME", "ISLOCKING", "ISHOLD", "LOTSTATE", "DEFECTQTY", "PROCESSSTATE", "PROCESSPATHID", 
				"PROCESSDEFID", "PROCESSDEFVERSION", "PROCESSSEGMENTCLASSID", "PROCESSSEGMENTID", 
				"PROCESSSEGMENTVERSION", "PRODUCTDEFVERSION", "PRODUCTTYPE", "DEFECTUNIT", "PCSPNL", "PROCESSSEGMENTTYPE", "AREAID");

			grdLotInfo.DataSource = dt;
		}

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdLotList.View.SetIsReadOnly();
			grdLotList.ShowButtonBar = false;

			grdLotList.View.AddTextBoxColumn("LOTID", 250);
			grdLotList.View.AddSpinEditColumn("QTY", 100);

			grdLotList.View.PopulateColumns();
		}
		#endregion
	}
}