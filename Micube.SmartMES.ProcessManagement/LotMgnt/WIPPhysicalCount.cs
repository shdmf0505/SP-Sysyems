#region using

using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 공정관리 > 재공관리 > 재공 실사 관리
	/// 업  무  설  명  : 재공 실사 관리
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-11-06
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class WIPPhysicalCount : SmartConditionManualBaseForm
	{
		#region Local Variables
		double timeLeft = 60;
		private bool _isToggle = false;
		#endregion

		#region 생성자

		public WIPPhysicalCount()
		{
			InitializeComponent();
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 화면의 컨텐츠 영역을 초기화한다.
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeEvent();

			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
            InitializeCompleteListGrid();
        }

		/// <summary>        
		/// 코드그룹 리스트 그리드를 초기화한다.
		/// </summary>
		private void InitializeGrid()
		{
			grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdLotList.GridButtonItem = GridButtonItem.Export;
			//grdLotList.View.SetIsReadOnly();

            grdLotList.View.EnableRowStateStyle = false;
            //grdLotList.View.UseBandHeaderOnly = true;

            grdLotList.View.SetSortOrder("SORTORDER");

            var infoBand = grdLotList.View.AddGroupColumn("");
            //양산구분
            infoBand.AddTextBoxColumn("LOTTYPE", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //작업구분
            infoBand.AddTextBoxColumn("PROCESSCLASSID_R", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("JOBTYPE")
                .SetIsReadOnly();
            //작업장
            infoBand.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();
            //품목코드
            infoBand.AddTextBoxColumn("PRODUCTDEFID", 100)
                .SetIsReadOnly();
            //품목명
            infoBand.AddTextBoxColumn("PRODUCTDEFNAME", 180)
                .SetIsReadOnly();
            //LOTID
            infoBand.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //공정
            infoBand.AddTextBoxColumn("PROCESSSEGMENTNAME", 130)
                .SetIsReadOnly();
            //투입PCS
            infoBand.AddSpinEditColumn("INPUTQTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("INPUTPCS")
                .SetIsReadOnly()
                .SetIsHidden();
            //투입PNL
            infoBand.AddSpinEditColumn("INPUTPNLQTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("INPUTPNL")
                .SetIsReadOnly()
                .SetIsHidden();
            //재공
            var wipBand = grdLotList.View.AddGroupColumn("WIPSTOCK");
            //PCS
            wipBand.AddSpinEditColumn("QTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCS")
                .SetIsReadOnly();
            //PNL
            wipBand.AddSpinEditColumn("PANELQTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PNL")
                .SetIsReadOnly();
            
			//쪽?
			//grdLotList.View.AddSpinEditColumn("EAQTY", 100).SetTextAlignment(TextAlignment.Right);
			//MM
			grdLotList.View.AddSpinEditColumn("MM", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##")
                .SetIsReadOnly()
                .SetIsHidden();
			//투입일
			grdLotList.View.AddTextBoxColumn("INPUTDATE", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetIsHidden();
			//인수일
			grdLotList.View.AddTextBoxColumn("RECEIVETIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetIsHidden();
			//작업시작일
			grdLotList.View.AddTextBoxColumn("WORKSTARTTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetIsHidden();
			//작업종료일
			grdLotList.View.AddTextBoxColumn("WORKENDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly()
                .SetIsHidden();
			//TXNHISKEY
			//grdLotList.View.AddTextBoxColumn("TXNHISTKEY", 130).SetIsHidden();
			//Site
			grdLotList.View.AddTextBoxColumn("PLANTID", 50)
                .SetIsReadOnly()
                .SetIsHidden();
            //SortOrder
            grdLotList.View.AddSpinEditColumn("SORTORDER", 50)
                .SetIsReadOnly()
                .SetIsHidden();
            //실재공
            var realWipBand = grdLotList.View.AddGroupColumn("REALWIP");
            //실재공(PCS)
            realWipBand.AddSpinEditColumn("REALWIPPCSQTY", 70)
                .SetLabel("PCS");
            //실재공(PNL)
            realWipBand.AddSpinEditColumn("REALWIPPNLQTY", 70)
                .SetLabel("PNL");
            //실재공(유실)
            var realWipLossBand = grdLotList.View.AddGroupColumn("REALWIPLOSS");
            //실재공(유실)(PCS)
            realWipLossBand.AddSpinEditColumn("REALWIPLOSSPCSQTY", 70)
                .SetLabel("PCS");
            //실재공(유실)(PNL)
            realWipLossBand.AddSpinEditColumn("REALWIPLOSSPNLQTY", 70)
                .SetLabel("PNL");
            //실재공(기타)
            var realWipEtcBand = grdLotList.View.AddGroupColumn("REALWIPETC");
            //실재공(기타)(PCS)
            realWipEtcBand.AddSpinEditColumn("REALWIPETCPCSQTY", 70)
                .SetLabel("PCS");
            //실재공(기타)(PNL)
            realWipEtcBand.AddSpinEditColumn("REALWIPETCPNLQTY", 70)
                .SetLabel("PNL");
            //실재공(합계)
            grdLotList.View.AddSpinEditColumn("REALWIPTOTALQTY", 90)
                .SetIsReadOnly()
                .SetIsHidden();
            var commentBand = grdLotList.View.AddGroupColumn("");
            //비고(사유)
            commentBand.AddTextBoxColumn("REASONCOMMENT", 250);
            //합수
            grdLotList.View.AddSpinEditColumn("PANELPERQTY", 80)
                .SetIsReadOnly()
                .SetIsHidden();

            grdLotList.View.PopulateColumns();
		}

        private void InitializeCompleteListGrid()
        {
            grdCompleteList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdCompleteList.GridButtonItem = GridButtonItem.Export;
            grdCompleteList.View.SetIsReadOnly();

            grdCompleteList.View.UseBandHeaderOnly = true;

            //양산구분
            grdCompleteList.View.AddTextBoxColumn("LOTTYPE", 60)
                .SetTextAlignment(TextAlignment.Center);
            //작업구분
            grdCompleteList.View.AddTextBoxColumn("PROCESSCLASSID_R", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("JOBTYPE");
            //작업장
            grdCompleteList.View.AddTextBoxColumn("AREANAME", 120);
            //품목코드
            grdCompleteList.View.AddTextBoxColumn("PRODUCTDEFID", 100);
            //품목명
            grdCompleteList.View.AddTextBoxColumn("PRODUCTDEFNAME", 180);
            //LOTID
            grdCompleteList.View.AddTextBoxColumn("LOTID", 200)
                .SetTextAlignment(TextAlignment.Center);
            //공정
            grdCompleteList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 130);
            //투입PCS
            grdCompleteList.View.AddSpinEditColumn("INPUTQTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("INPUTPCS")
                .SetIsHidden();
            //투입PNL
            grdCompleteList.View.AddSpinEditColumn("INPUTPNLQTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("INPUTPNL")
                .SetIsHidden();
            //재공
            var wipBand = grdCompleteList.View.AddGroupColumn("WIPSTOCK");
            //PCS
            wipBand.AddSpinEditColumn("QTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PCS");
            //PNL
            wipBand.AddSpinEditColumn("PANELQTY", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetLabel("PNL");
            //쪽?
            //grdLotList.View.AddSpinEditColumn("EAQTY", 100).SetTextAlignment(TextAlignment.Right);
            //MM
            grdCompleteList.View.AddSpinEditColumn("MM", 70)
                .SetTextAlignment(TextAlignment.Right)
                .SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true)
                .SetIsHidden();
            //투입일
            grdCompleteList.View.AddTextBoxColumn("INPUTDATE", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //인수일
            grdCompleteList.View.AddTextBoxColumn("RECEIVETIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //작업시작일
            grdCompleteList.View.AddTextBoxColumn("WORKSTARTTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //작업종료일
            grdCompleteList.View.AddTextBoxColumn("WORKENDTIME", 130)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //TXNHISKEY
            //grdLotList.View.AddTextBoxColumn("TXNHISTKEY", 130).SetIsHidden();
            //Site
            grdCompleteList.View.AddSpinEditColumn("PLANTID", 50)
                .SetIsHidden();
            //SortOrder
            grdLotList.View.AddSpinEditColumn("SORTORDER", 50)
                .SetIsHidden();
            //실재공
            var realWipBand = grdCompleteList.View.AddGroupColumn("REALWIP");
            //실재공(PCS)
            realWipBand.AddSpinEditColumn("REALWIPPCSQTY", 70)
                .SetLabel("PCS");
            //실재공(PNL)
            realWipBand.AddSpinEditColumn("REALWIPPNLQTY", 70)
                .SetLabel("PNL");
            //실재공(유실)
            var realWipLossBand = grdCompleteList.View.AddGroupColumn("REALWIPLOSS");
            //실재공(유실)(PCS)
            realWipLossBand.AddSpinEditColumn("REALWIPLOSSPCSQTY", 70)
                .SetLabel("PCS");
            //실재공(유실)(PNL)
            realWipLossBand.AddSpinEditColumn("REALWIPLOSSPNLQTY", 70)
                .SetLabel("PNL");
            //실재공(기타)
            var realWipEtcBand = grdCompleteList.View.AddGroupColumn("REALWIPETC");
            //실재공(기타)(PCS)
            realWipEtcBand.AddSpinEditColumn("REALWIPETCPCSQTY", 70)
                .SetLabel("PCS");
            //실재공(기타)(PNL)
            realWipEtcBand.AddSpinEditColumn("REALWIPETCPNLQTY", 70)
                .SetLabel("PNL");
            //실재공(합계)
            grdCompleteList.View.AddSpinEditColumn("REALWIPTOTALQTY", 90)
                .SetIsHidden();
            //비고(사유)
            grdCompleteList.View.AddTextBoxColumn("REASONCOMMENT", 250);

            grdCompleteList.View.PopulateColumns();
        }

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			this.Shown += WIPPhysicalCount_Load;
			timerState.Tick += TimerState_Tick;
			lblState.Click += LblState_Click;

			//인수인계시작
			btnTakeOverStart.Click += BtnTakeOverStart_ClickAsync;
			//인수인계중지
			btnTakeOverStop.Click += BtnTakeOverStop_Click;
			//실사리스트
			btnWipPhysicalList.Click += BtnWipPhysicalList_Click;

            tabLotList.SelectedPageChanged += TabLotList_SelectedPageChanged;

            txtLotId.Editor.KeyDown += TxtLotId_KeyDown;

            grdLotList.View.CellValueChanged += LotListView_CellValueChanged;
            grdLotList.View.RowStyle += LotListView_RowStyle;
            //grdLotList.View.CheckStateChanged += LotListView_CheckStateChanged;
        }

        private void LotListView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal panelPerQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("PANELPERQTY"));

            if (e.Column.FieldName == "REALWIPLOSSPCSQTY")
            {
                grdLotList.View.CellValueChanged -= LotListView_CellValueChanged;

                decimal qty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("QTY"));
                decimal lossQty = Format.GetInteger(e.Value);
                decimal etcQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPETCPCSQTY"));

                decimal pcsQty = qty - lossQty - etcQty;

                decimal panelQty = 0;

                if (panelPerQty > 0)
                    panelQty = Math.Ceiling(lossQty / panelPerQty);

                grdLotList.View.SetFocusedRowCellValue("REALWIPLOSSPNLQTY", panelQty);

                grdLotList.View.CellValueChanged += LotListView_CellValueChanged;

                grdLotList.View.SetFocusedRowCellValue("REALWIPPCSQTY", pcsQty);
            }
            else if (e.Column.FieldName == "REALWIPLOSSPNLQTY")
            {
                grdLotList.View.CellValueChanged -= LotListView_CellValueChanged;

                decimal panelQty = Format.GetInteger(e.Value);

                decimal qty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("QTY"));
                decimal lossQty = panelQty * panelPerQty;
                decimal etcQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPETCPCSQTY"));

                decimal pcsQty = qty - lossQty - etcQty;

                grdLotList.View.SetFocusedRowCellValue("REALWIPLOSSPCSQTY", lossQty);

                grdLotList.View.CellValueChanged += LotListView_CellValueChanged;

                grdLotList.View.SetFocusedRowCellValue("REALWIPPCSQTY", pcsQty);
            }
            else if (e.Column.FieldName == "REALWIPETCPCSQTY")
            {
                grdLotList.View.CellValueChanged -= LotListView_CellValueChanged;

                decimal qty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("QTY"));
                decimal lossQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPLOSSPCSQTY"));
                decimal etcQty = Format.GetInteger(e.Value);

                decimal pcsQty = qty - lossQty - etcQty;

                decimal panelQty = 0;

                if (panelPerQty > 0)
                    panelQty = Math.Ceiling(etcQty / panelPerQty);

                grdLotList.View.SetFocusedRowCellValue("REALWIPETCPNLQTY", panelQty);

                grdLotList.View.CellValueChanged += LotListView_CellValueChanged;

                grdLotList.View.SetFocusedRowCellValue("REALWIPPCSQTY", pcsQty);
            }
            else if (e.Column.FieldName == "REALWIPETCPNLQTY")
            {
                grdLotList.View.CellValueChanged -= LotListView_CellValueChanged;

                decimal panelQty = Format.GetInteger(e.Value);

                decimal qty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("QTY"));
                decimal lossQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPLOSSPCSQTY"));
                decimal etcQty = panelQty * panelPerQty;

                decimal pcsQty = qty - lossQty - etcQty;

                grdLotList.View.SetFocusedRowCellValue("REALWIPETCPCSQTY", etcQty);

                grdLotList.View.CellValueChanged += LotListView_CellValueChanged;

                grdLotList.View.SetFocusedRowCellValue("REALWIPPCSQTY", pcsQty);
            }
            else if (e.Column.FieldName == "REALWIPPCSQTY")
            {
                grdLotList.View.CellValueChanged -= LotListView_CellValueChanged;

                decimal pcsQty = Format.GetInteger(e.Value);
                decimal lossQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPLOSSPCSQTY"));
                decimal etcQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPETCPCSQTY"));

                decimal totalQty = pcsQty + lossQty + etcQty;

                decimal panelQty = 0;

                if (panelPerQty > 0)
                    panelQty = Math.Ceiling(pcsQty / panelPerQty);

                grdLotList.View.SetFocusedRowCellValue("REALWIPPNLQTY", panelQty);
                grdLotList.View.SetFocusedRowCellValue("REALWIPTOTALQTY", totalQty);

                grdLotList.View.CellValueChanged += LotListView_CellValueChanged;
            }
            else if (e.Column.FieldName == "REALWIPPNLQTY")
            {
                grdLotList.View.CellValueChanged -= LotListView_CellValueChanged;

                decimal panelQty = Format.GetInteger(e.Value);

                decimal pcsQty = panelQty * panelPerQty;
                decimal lossQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPLOSSPCSQTY"));
                decimal etcQty = Format.GetInteger(grdLotList.View.GetFocusedRowCellValue("REALWIPETCPCSQTY"));

                decimal totalQty = pcsQty + lossQty + etcQty;

                grdLotList.View.SetFocusedRowCellValue("REALWIPPCSQTY", pcsQty);
                grdLotList.View.SetFocusedRowCellValue("REALWIPTOTALQTY", totalQty);

                grdLotList.View.CellValueChanged += LotListView_CellValueChanged;
            }
        }

        private void TabLotList_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == tpgNotCompleteList)
            {
                SmartButton btnSave = GetToolbarButtonById("Save");

                if (btnSave != null)
                    btnSave.Visible = true;

                SetConditionVisiblility("P_SURVEYMONTH", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
            }
            else if (e.Page == tpgCompleteList)
            {
                SmartButton btnSave = GetToolbarButtonById("Save");

                if (btnSave != null)
                    btnSave.Visible = false;

                SetConditionVisiblility("P_SURVEYMONTH", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
            }
        }

        private void LotListView_CheckStateChanged(object sender, EventArgs e)
        {
            BandedGridHitInfo info = grdLotList.View.CalcHitInfo(grdLotList.View.GridControl.PointToClient(Cursor.Position));

            int rowHandle = 0;
            if (info.InDataRow && info.RowHandle > 0)
                rowHandle = info.RowHandle;
            else
                rowHandle = grdLotList.View.FocusedRowHandle;

            if (grdLotList.View.IsRowChecked(rowHandle))
            {
                grdLotList.View.SetRowCellValue(rowHandle, "SORTORDER", 1);
            }
            else
            {
                grdLotList.View.SetRowCellValue(rowHandle, "SORTORDER", 99);
            }


            grdLotList.View.RefreshData();
        }

        private void LotListView_RowStyle(object sender, RowStyleEventArgs e)
        {
            SmartBandedGridView view = sender as SmartBandedGridView;

            if (view.IsRowChecked(e.RowHandle))
            {
                e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
                e.HighPriority = true;
            }
        }

        private void TxtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string lotId = txtLotId.Text;

                int rowHandle = grdLotList.View.LocateByValue("LOTID", lotId);

                if (rowHandle >= 0)
                {
                    //grdLotList.View.CheckStateChanged -= LotListView_CheckStateChanged;

                    grdLotList.View.FocusedRowHandle = rowHandle;
                    grdLotList.View.CheckRow(rowHandle, true);
                    //grdLotList.View.SetRowCellValue(rowHandle, "SORTORDER", 1);

                    grdLotList.View.RefreshData();

                    //grdLotList.View.CheckStateChanged += LotListView_CheckStateChanged;
                }

                txtLotId.Text = "";
            }
        }

        /// <summary>
        /// 라벨 클릭 시 타이머 중지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblState_Click(object sender, EventArgs e)
		{
			timerState.Enabled = false;
			lblState.ForeColor = Color.FromArgb(255, 0, 0);
		}


		/// <summary>
		/// 타이머 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimerState_Tick(object sender, EventArgs e)
		{
			timeLeft -= 0.5;
			if(timeLeft < 0)
			{
				//1분 뒤 타이머 자동 스탑
				timerState.Stop();
			}

			int color = _isToggle == false ? 255 : 0;

			lblState.ForeColor = Color.FromArgb(color, 0, 0);

			_isToggle = !_isToggle;
		}

		/// <summary>
		/// Form 로드 이벤트 - 재공 실사 상태 체크
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WIPPhysicalCount_Load(object sender, EventArgs e)
		{
            SetState(false);
        }

        private void RefreshLockingMessage()
        {
            var values = Conditions.GetValues();

            //lblState.Text = Language.Get("").message
            DataTable dtIsWipSurvey = SqlExecuter.Query("GetIsWipSurvey", "10001", new Dictionary<string, object>() { { "PLANTID", values["P_PLANTID"].ToString() } });
            string isWipSurvey = Format.GetString(dtIsWipSurvey.AsEnumerable().FirstOrDefault()["ISWIPSURVEY"]);

            if (isWipSurvey.Equals("Y"))
            {
                SetState(true);
            }
            else
            {
                SetState(false);
            }
        }


        /// <summary>
        /// 실사리스트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWipPhysicalList_Click(object sender, EventArgs e)
		{
            DataTable dt = new DataTable();

            if (tabLotList.SelectedTabPage == tpgNotCompleteList)
            {
                DataTable lotList = grdLotList.DataSource as DataTable;

                lotList.AsEnumerable().ForEach(row =>
                {
                    row["REALWIPPCSQTY"] = DBNull.Value;
                    row["REALWIPLOSSQTY"] = DBNull.Value;
                    row["REALWIPETCQTY"] = DBNull.Value;
                });

                lotList.AcceptChanges();

                dt = lotList;
            }
            else if (tabLotList.SelectedTabPage == tpgCompleteList)
            {
                dt = grdCompleteList.DataSource as DataTable;
            }
            else
                return;

			if (dt.Rows.Count == 0)
			{
				//리스트가 없습니다.
				throw MessageException.Create("EXISTSWIPLIST");
			}


			Assembly assembly = Assembly.GetAssembly(this.GetType());
			Stream stream = assembly.GetManifestResourceStream("Micube.SmartMES.ProcessManagement.Report.WipPhysicalCountList.repx");
			Commons.CommonFunction.PrintWipPhysicalCountList(stream, dt);
		}

		/// <summary>
		/// 인수인계 중지
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnTakeOverStop_Click(object sender, EventArgs e)
		{
            var buttons = pnlToolbar.Controls.Find<SmartButton>(true);

            try
            {
                buttons.ForEach(b => b.IsBusy = true);
                pnlContent.ShowWaitArea();

                DataTable dt = grdLotList.DataSource as DataTable;
                if (dt.Rows.Count == 0)
                {
                    //인수인계 할 데이터가 없습니다.
                    throw MessageException.Create("NoTakeOverLot");
                }

                // 인수/인계 시작 하시겠습니까?
                if (ShowMessage(MessageBoxButtons.YesNo, "IsWipSurveyLockingStop") == DialogResult.No)
                    return;

                var values = Conditions.GetValues();

                MessageWorker worker = new MessageWorker("SaveWipSurveyLocking");
                worker.SetBody(new MessageBody()
                {
                    { "lockingType", "Stop" },
                    { "PlantId", values["P_PLANTID"] }
				    //{ "lotList", dt }
			    });

                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.ToString());
            }
            finally
            {
                pnlContent.CloseWaitArea();
                buttons.ForEach(b => b.IsBusy = false);
            }

			//ShowMessage("SuccessSave");

			SetState(false);

			await OnSearchAsync();
		}

		/// <summary>
		/// 인수인계 시작
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnTakeOverStart_ClickAsync(object sender, EventArgs e)
		{
            var buttons = pnlToolbar.Controls.Find<SmartButton>(true);

            try
            {
                buttons.ForEach(b => b.IsBusy = true);
                pnlContent.ShowWaitArea();

                DataTable dt = grdLotList.DataSource as DataTable;
                if (dt.Rows.Count == 0)
                {
                    //인수인계 할 데이터가 없습니다.
                    throw MessageException.Create("NoTakeOverLot");
                }

                // 인수/인계 중지 하시겠습니까?
                if (ShowMessage(MessageBoxButtons.YesNo, "IsWipSurveyLockingStart") == DialogResult.No)
                    return;

                var values = Conditions.GetValues();

                MessageWorker worker = new MessageWorker("SaveWipSurveyLocking");
                worker.SetBody(new MessageBody()
                {
                    { "lockingType", "Start" },
                    { "PlantId", values["P_PLANTID"] }
				    //{ "lotList", dt }
			    });

                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.ToString());
            }
            finally
            {
                pnlContent.CloseWaitArea();
                buttons.ForEach(b => b.IsBusy = false);
            }

			//ShowMessage("SuccessSave");

			SetState(true);

			await OnSearchAsync();
		}

        #endregion

        #region 툴바

        protected override void OnToolbarClick(ToolbarClickEventArgs e)
        {
            base.OnToolbarClick(e);

            if (e.Id == "Save")
            {
                OnToolbarSaveClick();
            }
            else if (e.Id == "TakeOverStart")
            {
                BtnTakeOverStart_ClickAsync(GetToolbarButtonById(e.Id), e);
            }
            else if (e.Id == "TakeOverStop")
            {
                BtnTakeOverStop_Click(GetToolbarButtonById(e.Id), e);
            }
        }

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

            var buttons = pnlToolbar.Controls.Find<SmartButton>(true);

            DataTable lotList = grdLotList.View.GetCheckedRows();

            if (lotList.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            string lotId = string.Join(",", lotList.AsEnumerable().Select(r => Format.GetString(r["LOTID"])));

            try
            {
                buttons.ForEach(b => b.IsBusy = true);
                pnlContent.ShowWaitArea();

                var values = Conditions.GetValues();

                MessageWorker worker = new MessageWorker("SaveWipSurveyLocking");
                worker.SetBody(new MessageBody()
                {
                    { "lockingType", "Save" },
                    { "PlantId", values["P_PLANTID"] },
                    { "LotId", lotId },
                    { "SurveyList", lotList }
                });

                worker.Execute();
            }
            catch (Exception ex)
            {
                throw MessageException.Create(ex.ToString());
            }
            finally
            {
                buttons.ForEach(b => b.IsBusy = false);
                pnlContent.CloseWaitArea();
            }

            SetState(true);
		}

		#endregion

		#region 검색

		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

            SetState(false);

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

            if (tabLotList.SelectedTabPage == tpgNotCompleteList)
            {
                DataTable dtWipList = await QueryAsyncDirect("SelectWipListOfPhysicalCount", "10001", values);

                if (dtWipList.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdLotList.DataSource = dtWipList;
            }
            else if (tabLotList.SelectedTabPage == tpgCompleteList)
            {
                DateTime surveyMonth = DateTime.Parse(Format.GetString(values["P_SURVEYMONTH"]));
                values["P_SURVEYMONTH"] = surveyMonth.ToString("yyyy-MM");

                DataTable dtCompleteList = await QueryAsyncDirect("SelectWipListOfPhysicalCountComplete", "10001", values);

                if (dtCompleteList.Rows.Count < 1)
                {
                    // 조회할 데이터가 없습니다.
                    ShowMessage("NoSelectData");
                }

                grdCompleteList.DataSource = dtCompleteList;
            }

            RefreshLockingMessage();
        }

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //품목코드
            CommonFunction.AddConditionProductPopup("P_PRODUCTDEFIDVERSION", 2.5, true, Conditions, "PRODUCTDEFNAME", "PRODUCTDEF", false, 0, false);

            //작업장
            //CommonFunction.AddConditionAreaPopup("P_AREAID", 5.5, true, Conditions);
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 3.2, true, Conditions, false, false);

            Conditions.GetCondition<ConditionItemDateEdit>("P_SURVEYMONTH").SetIsHidden();
        }

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();
            
            Conditions.GetControl<SmartComboBox>("P_PLANTID").EditValueChanged += (sender, e) =>
            {
                SetState(false);
            };
            
			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		#endregion

		#region 유효성 검사

		/// <summary>
		/// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

            DataTable checkedList = grdLotList.View.GetCheckedRows();

            checkedList.AsEnumerable().ForEach(row =>
            {
                int qty = Format.GetInteger(row["QTY"]);
                int totalQty = Format.GetInteger(row["REALWIPTOTALQTY"]);

                if (!qty.Equals(totalQty))
                {
                    // 재공실사 수량이 재공 수량과 다릅니다. 재공 수량 : {0}, 재공실사 수량 : {1}
                    throw MessageException.Create("WipQtyIsNotEqualSurveyQty", qty.ToString("#,##0"), totalQty.ToString("#,##0"));
                }
            });
		}

		#endregion

		#region Private Function
		/// <summary>
		/// 재공 실사 상태 보여주기
		/// </summary>
		/// <param name="isSet"></param>
		private void SetState(bool isSet)
		{
			if (isSet)
			{
                timerState.Stop();

                timerState.Interval = 500;
				timerState.Start();

                //btnTakeOverStart.Enabled = false;
                //btnTakeOverStop.Enabled = true;

                SmartButton startButton = GetToolbarButtonById("TakeOverStart");
                SmartButton stopButton = GetToolbarButtonById("TakeOverStop");

                if (startButton != null)
                    startButton.IsBusy = true;
                if (stopButton != null)
                    stopButton.IsBusy = false;

                tplWipPhysicalCount.Show();

                pnlLotId.Visible = true;

                grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
                grdLotList.View.CheckMarkSelection.CheckMarkColumnVisible = true;
                //grdLotList.View.CheckMarkSelection.ShowCheckBoxHeader = false;

                SmartButton btnSave = GetToolbarButtonById("Save");

                if (btnSave != null)
                    btnSave.Enabled = true;
			}
			else
			{
				timerState.Stop();

                //btnTakeOverStart.Enabled = true;
                //btnTakeOverStop.Enabled = false;

                SmartButton startButton = GetToolbarButtonById("TakeOverStart");
                SmartButton stopButton = GetToolbarButtonById("TakeOverStop");

                if (startButton != null)
                    startButton.IsBusy = false;
                if (stopButton != null)
                    stopButton.IsBusy = true;

                tplWipPhysicalCount.Hide();

                pnlLotId.Visible = false;

                grdLotList.View.CheckMarkSelection.ClearSelection();

                grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
                grdLotList.View.CheckMarkSelection.CheckMarkColumnVisible = false;

                SmartButton btnSave = GetToolbarButtonById("Save");

                if (btnSave != null)
                    btnSave.Enabled = false;


                DataTable list = grdLotList.DataSource as DataTable;

                for (int i = 0; i < list.Rows.Count; i++)
                {
                    grdLotList.View.SetRowCellValue(i, "SORTORDER", 99);
                }

                grdLotList.View.RefreshData();
            }

            SetSurveyGridColumnEditable(isSet);
        }

        private void SetSurveyGridColumnEditable(bool isEditable)
        {
            grdLotList.View.Columns["REALWIPPCSQTY"].OptionsColumn.AllowEdit = isEditable;
            grdLotList.View.Columns["REALWIPPNLQTY"].OptionsColumn.AllowEdit = isEditable;
            grdLotList.View.Columns["REALWIPLOSSPCSQTY"].OptionsColumn.AllowEdit = isEditable;
            grdLotList.View.Columns["REALWIPLOSSPNLQTY"].OptionsColumn.AllowEdit = isEditable;
            grdLotList.View.Columns["REALWIPETCPCSQTY"].OptionsColumn.AllowEdit = isEditable;
            grdLotList.View.Columns["REALWIPETCPNLQTY"].OptionsColumn.AllowEdit = isEditable;
            grdLotList.View.Columns["REASONCOMMENT"].OptionsColumn.AllowEdit = isEditable;
        }
		#endregion
	}
}