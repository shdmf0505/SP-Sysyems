#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons;
using System;
using Micube.SmartMES.Commons.Controls;
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
	/// 프 로 그 램 명  : 공정관리 > 공정작업 > LOT 메시지 관리
	/// 업  무  설  명  : 특정 품목/공정/LOT 에 대해 주요 메시지를 전달할 수 있도록 한다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-03
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LotMessage : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public LotMessage()
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
			InitializeProductDefInfoGrid();
			InitializeSendSegmentFrPdPopup();

			//메시지 유형 조회
			//InitializeMessageTypeCombo();

			this.ucDataUpDownBtn.SourceGrid = this.grdWIP;
			this.ucDataUpDownBtn.TargetGrid = this.grdTargetWip;
		}

		/// <summary>        
		/// 품목 정보 그리드를 초기화
		/// </summary>
		private void InitializeProductDefInfoGrid()
		{
			grdProductDefInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdProductDefInfo.GridButtonItem = GridButtonItem.Export;
			grdProductDefInfo.View.SetIsReadOnly();

			grdProductDefInfo.View.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
			grdProductDefInfo.View.CheckMarkSelection.ShowCheckBoxHeader = false;

			//품목코드
			grdProductDefInfo.View.AddTextBoxColumn("PRODUCTDEFID", 150);
			//품목버전
			grdProductDefInfo.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
			//품목명
			grdProductDefInfo.View.AddTextBoxColumn("PRODUCTDEFNAME", 350);

			grdProductDefInfo.View.PopulateColumns();
		}

		/// <summary>
		/// 공정 정보 그리드 초기화
		/// </summary>
		private void InitializeSegmentInfoGrid()
		{
			grdSegmentInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdSegmentInfo.GridButtonItem = GridButtonItem.Export;
			grdSegmentInfo.View.SetIsReadOnly();

			//대공정
			grdSegmentInfo.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 100)
				.SetLabel("LARGEPROCESSSEGMENT");
			//중공정
			grdSegmentInfo.View.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
				.SetLabel("MIDDLEPROCESSSEGMENT");
			//공정코드
			grdSegmentInfo.View.AddTextBoxColumn("PROCESSSEGMENTID", 150);
			//공정명
			grdSegmentInfo.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250);

			grdSegmentInfo.View.PopulateColumns();
		}

		/// <summary>
		/// 재공 정보 그리드 초기화
		/// </summary>
		private void InitializeWipInfoGrid()
		{
			grdWIP.GridButtonItem = GridButtonItem.Export;

			grdWIP.View.SetIsReadOnly();

			// CheckBox 설정
			this.grdWIP.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			var groupDefaultCol = grdWIP.View.AddGroupColumn("WIPLIST");
			groupDefaultCol.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("PRODUCTDEFNAME", 300);
			groupDefaultCol.AddTextBoxColumn("PROCESSDEFID", 150).SetIsHidden();
			groupDefaultCol.AddTextBoxColumn("PROCESSDEFVERSION", 50).SetIsHidden();
			groupDefaultCol.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTID", 150).SetIsHidden();
			groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTVERSION", 50).SetIsHidden();
			groupDefaultCol.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
			groupDefaultCol.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("AREAID", 150).SetIsHidden();
			groupDefaultCol.AddTextBoxColumn("AREANAME", 150);
			groupDefaultCol.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
			groupDefaultCol.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();

			var groupWipCol = grdWIP.View.AddGroupColumn("WIPQTY");
			groupWipCol.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			groupWipCol.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");


			var groupReceiveCol = grdWIP.View.AddGroupColumn("WAITFORRECEIVE");
			groupReceiveCol.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			groupReceiveCol.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			var groupWorkStartCol = grdWIP.View.AddGroupColumn("ACCEPT");
			groupWorkStartCol.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			groupWorkStartCol.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			var groupWorkEndCol = grdWIP.View.AddGroupColumn("WIPSTARTQTY");
			groupWorkEndCol.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			groupWorkEndCol.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			var groupSendCol = grdWIP.View.AddGroupColumn("WIPENDQTY");
			groupSendCol.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			groupSendCol.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			var groupWIPCol = grdWIP.View.AddGroupColumn("WIPLIST");
			groupWIPCol.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
			groupWIPCol.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
			groupWIPCol.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
			groupWIPCol.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);
			groupWIPCol.AddTextBoxColumn("LOTTYPEID", 50).SetIsHidden();

			grdWIP.View.PopulateColumns();
		}

		/// <summary>
		/// 대상 LOT 그리드 초기화
		/// </summary>
		private void InitializeTargetWipGrid()
		{
			grdTargetWip.GridButtonItem = GridButtonItem.None;

			grdTargetWip.View.SetIsReadOnly();

			// CheckBox 설정
			this.grdTargetWip.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			grdTargetWip.View.AddTextBoxColumn("LOTTYPE", 70).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("PRODUCTDEFVERSION", 80).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("PRODUCTDEFNAME", 300);
			grdTargetWip.View.AddTextBoxColumn("PROCESSDEFID", 150).SetIsHidden();
			grdTargetWip.View.AddTextBoxColumn("PROCESSDEFVERSION", 50).SetIsHidden();
			grdTargetWip.View.AddTextBoxColumn("USERSEQUENCE", 70).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("PROCESSSEGMENTID", 150);
			grdTargetWip.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 50);
			grdTargetWip.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
			grdTargetWip.View.AddTextBoxColumn("PLANTID", 70).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("AREAID", 150).SetIsHidden();
			grdTargetWip.View.AddTextBoxColumn("AREANAME", 150);
			grdTargetWip.View.AddTextBoxColumn("RTRSHT", 70).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("ISLOCKING", 60).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("ISHOLD", 60).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("PROCESSSTATE", 60).SetTextAlignment(TextAlignment.Center).SetIsHidden();

			grdTargetWip.View.AddTextBoxColumn("QTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			grdTargetWip.View.AddTextBoxColumn("PANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			grdTargetWip.View.AddTextBoxColumn("SENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			grdTargetWip.View.AddTextBoxColumn("SENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			grdTargetWip.View.AddTextBoxColumn("RECEIVEPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			grdTargetWip.View.AddTextBoxColumn("RECEIVEPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			grdTargetWip.View.AddTextBoxColumn("WORKSTARTPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			grdTargetWip.View.AddTextBoxColumn("WORKSTARTPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			grdTargetWip.View.AddTextBoxColumn("WORKENDPCSQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");
			grdTargetWip.View.AddTextBoxColumn("WORKENDPANELQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}");

			grdTargetWip.View.AddTextBoxColumn("LEADTIME", 80).SetTextAlignment(TextAlignment.Right);
			grdTargetWip.View.AddTextBoxColumn("LOTINPUTDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
			grdTargetWip.View.AddTextBoxColumn("DELIVERYDATE", 150).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}");
			grdTargetWip.View.AddTextBoxColumn("LEFTDATE", 80).SetTextAlignment(TextAlignment.Center);
			grdTargetWip.View.AddTextBoxColumn("LOTTYPEID", 80).SetIsHidden();

			grdTargetWip.View.PopulateColumns();
		}

		/// <summary>
		/// 품목 - 전달 공정 팝업 초기화
		/// </summary>
		private void InitializeSendSegmentFrPdPopup()
		{
			if(popSendSegmentFrPd.Editor.SelectPopupCondition != null) popSendSegmentFrPd.Editor.SelectPopupCondition = null;

			ConditionItemSelectPopup selectPopup = new ConditionItemSelectPopup();
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			DataTable selected = grdProductDefInfo.View.GetCheckedRows();
			if(selected.Rows.Count > 0)
			{
				string productdefId = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).FirstOrDefault();
				string productdefVersion = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).FirstOrDefault();
				
				param.Add("P_PRODUCTDEFID", productdefId);
				param.Add("P_PRODUCTDEFVERSION", productdefVersion);
			}
			
			selectPopup.Id = "SENDSEGMENT";
			selectPopup.SearchQuery = new SqlQuery("GetProcessPathByProductDefAndSequence", "10002", param);
			selectPopup.ValueFieldName = "PROCESSSEGMENTID";
			selectPopup.DisplayFieldName = "PROCESSSEGMENTNAME";
			selectPopup.SetPopupLayout("SENDSEGMENT", PopupButtonStyles.Ok_Cancel, true, true);
			selectPopup.SetPopupResultCount(1);
			selectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow);
			selectPopup.SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

			selectPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
			selectPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
			selectPopup.GridColumns.AddTextBoxColumn("USERSEQUENCE", 60);
			selectPopup.GridColumns.AddTextBoxColumn("PLANTID", 60);

			popSendSegmentFrPd.Editor.SelectPopupCondition = selectPopup;
			popSendSegmentFrPd.Editor.ClearButtonVisible = false;
		}

		/// <summary>
		/// LOT - 전달 공정 팝업 초기화
		/// </summary>
		/// <param name="productDefId"></param>
		/// <param name="productDefVersion"></param>
		private void InitializeSendSegmentFrLotPopup(bool isInit, object productDefId = null, object productDefVer = null)
		{
			if(popSendSegmentFrLot.Editor.SelectPopupCondition != null) popSendSegmentFrLot.Editor.SelectPopupCondition = null;

			ConditionItemSelectPopup selectPopup = new ConditionItemSelectPopup();
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			if (isInit && string.IsNullOrEmpty(Format.GetString(Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").EditValue)))
			{
				var values = Conditions.GetValues();				
				
				param.Add("P_PRODUCTDEFID", Format.GetString(values["P_PRODUCTDEFID2"]).Split('|')[0]);
				param.Add("P_PRODUCTDEFVERSION", Format.GetString(values["P_PRODUCTDEFID2"]).Split('|')[1]);
			}

			if(isInit && productDefId != null && productDefVer != null)
			{
				param.Add("P_PRODUCTDEFID", productDefId);
				param.Add("P_PRODUCTDEFVERSION", productDefVer);
			}

			selectPopup.Id = "SENDSEGMENT";
			selectPopup.SearchQuery = new SqlQuery("GetProcessPathByProductDefAndSequence", "10002", param);
			selectPopup.ValueFieldName = "PROCESSSEGMENTID";
			selectPopup.DisplayFieldName = "PROCESSSEGMENTNAME";
			selectPopup.SetPopupLayout("SENDSEGMENT", PopupButtonStyles.Ok_Cancel, false, true);
			selectPopup.SetPopupResultCount(0);
			selectPopup.SetPopupLayoutForm(800, 800, FormBorderStyle.SizableToolWindow);
			selectPopup.SetPopupAutoFillColumns("PROCESSSEGMENTNAME");

			selectPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
			selectPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
			selectPopup.GridColumns.AddTextBoxColumn("USERSEQUENCE", 60);
			selectPopup.GridColumns.AddTextBoxColumn("PLANTID", 60);

			popSendSegmentFrLot.Editor.SelectPopupCondition = selectPopup;
			popSendSegmentFrLot.Editor.ClearButtonVisible = false;
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			//품목
			grdProductDefInfo.View.CheckStateChanged += View_CheckStateChanged1;

			//LOT
			grdWIP.View.CheckStateChanged += View_CheckStateChanged;
			grdWIP.View.DoubleClick += View_DoubleClick;
			grdWIP.View.RowStyle += View_RowStyle;
			ucDataUpDownBtn.buttonClick += UcDataUpDownBtn_buttonClick;

			//공통
			tabControl.SelectedPageChanged += TabControl_SelectedPageChanged;
		}

		/// <summary>
		/// 검색조건 LOT팝업의 Value Changed 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Lot_EditValueChanged(object sender, EventArgs e)
		{
			SmartSelectPopupEdit lot = Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID");
			Conditions.GetCondition("P_PRODUCTDEFID2").IsRequired = string.IsNullOrEmpty(Format.GetString(lot.EditValue));

		}

		/// <summary>
		/// 한 행만 체크
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CheckStateChanged1(object sender, EventArgs e)
		{
			// 한 행만 체크 가능
			grdProductDefInfo.View.CheckStateChanged -= View_CheckStateChanged1;
			grdProductDefInfo.View.CheckedAll(false);

			grdProductDefInfo.View.CheckRow(grdProductDefInfo.View.GetFocusedDataSourceRowIndex(), true);
			grdProductDefInfo.View.CheckStateChanged += View_CheckStateChanged1;

			InitializeSendSegmentFrPdPopup();
		}

		/// <summary>
		/// 재공 row style event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if (e.RowHandle < 0)
				return;

			bool isChecked = grdWIP.View.IsRowChecked(e.RowHandle);

			if (isChecked)
			{
				e.Appearance.BackColor = Color.FromArgb(254, 254, 16);
				e.HighPriority = true;
			}
		}

		/// <summary>
		/// row 더블클릭 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_DoubleClick(object sender, EventArgs e)
		{
			// 더블클릭 시 체크박스 체크
			SmartBandedGridView view = (SmartBandedGridView)sender;

			if (grdWIP.View.IsRowChecked(view.FocusedRowHandle))
				grdWIP.View.CheckRow(view.FocusedRowHandle, false);
			else
				grdWIP.View.CheckRow(view.FocusedRowHandle, true);
		}

		/// <summary>
		/// 품목 - 팝업 보여주기 시 필수 선택 값 체크				
		/// </summary>
		/// <param name="selectedTabIndex"></param>
		/// <returns></returns>
		private bool UcMessageInfoProductDef_SelectedOptions(out int selectedTabIndex, out Dictionary<string, object> param)
		{
			DataTable dtSelected = grdProductDefInfo.View.GetCheckedRows();
			string sendSegment = Format.GetString(popSendSegmentFrPd.Editor.GetValue());

			if (dtSelected.Rows.Count < 1 || string.IsNullOrEmpty(sendSegment))
			{
				selectedTabIndex = -1;
				param = null;
				return false;
			}
			else
			{
				selectedTabIndex = tabControl.SelectedTabPageIndex;

				//Get Parameter
				string productDefId = string.Empty;
				string productDefVerison = string.Empty;

				foreach(DataRow row in dtSelected.Rows)
				{
					productDefId += Format.GetString(row["PRODUCTDEFID"]) + ",";
					productDefVerison += Format.GetString(row["PRODUCTDEFVERSION"]) + ",";
				}

				param = new Dictionary<string, object>();
				param.Add("PRODUCTDEFID", productDefId);
				param.Add("PRODUCTDEFVERSION", productDefVerison);
				param.Add("PROCESSSEGMENTID", sendSegment);

				return true;
			}
		}

		/// <summary>
		/// 공정 - 팝업 보여주기 시 필수 선택 값 체크
		/// </summary>
		/// <param name="selectedTabIndex"></param>
		/// <returns></returns>
		private bool UcMessageInfoSegment_SelectedOptions(out int selectedTabIndex, out Dictionary<string, object> param)
		{
			DataTable dtSelected = grdSegmentInfo.View.GetCheckedRows();

			if (dtSelected.Rows.Count < 1)
			{
				selectedTabIndex = -1;
				param = null;
				return false;
			}
			else
			{
				selectedTabIndex = tabControl.SelectedTabPageIndex;

				//Get Parameter
				string processSegmentId = string.Empty;

				foreach(DataRow row in dtSelected.Rows)
				{
					processSegmentId += Format.GetString(row["PROCESSSEGMENTID"]) + ",";
				}

				param = new Dictionary<string, object>();
				param.Add("PROCESSSEGMENTID", processSegmentId);

				return true;
			}
		}

		/// <summary>
		/// LOT - 팝업 보여주기 시 필수 선택 값 체크
		/// </summary>
		/// <param name="selectedTabIndex"></param>
		/// <returns></returns>
		private bool UcMessageInfoLot_SelectedOptions(out int selectedTabIndex, out Dictionary<string, object> param)
		{
			if(grdTargetWip.DataSource == null)
			{
				selectedTabIndex = -1;
				param = null;
				return false;
			}

			int targetListCount = (grdTargetWip.DataSource as DataTable).Rows.Count;
			string sendSegment = Format.GetString(popSendSegmentFrLot.Editor.GetValue());

			if (targetListCount.Equals(0) || string.IsNullOrEmpty(sendSegment))
			{
				selectedTabIndex = -1;
				param = null;
				return false;
			}
			else
			{
				selectedTabIndex = tabControl.SelectedTabPageIndex;

				//Get Parameter
				DataTable dt = grdTargetWip.DataSource as DataTable;
				
				string lotId = string.Empty;
				string productDefId = string.Empty;
				string productDefVersion = string.Empty;

				foreach(DataRow row in dt.Rows)
				{
					lotId += Format.GetString(row["LOTID"]) + ",";
					productDefId += Format.GetString(row["PRODUCTDEFID"]) + ",";
					productDefVersion += Format.GetString(row["PRODUCTDEFVERSION"]) + ",";
				}

				param = new Dictionary<string, object>();
				param.Add("PROCESSSEGMENTID", sendSegment);
				param.Add("LOTID", lotId);
				param.Add("PRODUCTDEFID", productDefId);
				param.Add("PRODUCEDEFVERSION", productDefVersion);

				return true;
			}
		}

		/// <summary>
		/// 같은 품목 체크 확인
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_CheckStateChanged(object sender, EventArgs e)
		{
			DataTable selected = grdWIP.View.GetCheckedRows();
			
			//품목코드
			int productDefId = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFID"])).Distinct().Count();
			if(productDefId > 1)
			{
				grdWIP.View.CheckedAll(false);

				//다른 품목 ID는 선택할 수 없습니다.
				throw MessageException.Create("MixSelectProductDefID");
			}

			//품목버전
			int productDefVer = selected.AsEnumerable().Select(r => Format.GetString(r["PRODUCTDEFVERSION"])).Distinct().Count();
			if(productDefVer > 1)
			{
				grdWIP.View.CheckedAll(false);

				//다른 품목 버전은 선택할 수 없습니다.
				throw MessageException.Create("MixSelectProductDefVersion");

			}
		}

		/// <summary>
		/// Up / Down 컨트롤 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UcDataUpDownBtn_buttonClick(object sender, EventArgs e)
		{
			DataTable dt = grdWIP.View.GetCheckedRows();
			DataTable tgdt = grdTargetWip.DataSource as DataTable;

			if (ucDataUpDownBtn.ButtonState.Equals("Up"))
			{
				int selectedCount = grdTargetWip.View.GetCheckedRows().Rows.Count;
				if(tgdt.Rows.Count.Equals(selectedCount))
				{
					popSendSegmentFrLot.Editor.SetValue(null);
				}
			}
			else
			{
				if (tgdt == null || tgdt.Rows.Count <= 0) return;

				// 제품 ID 체크
				string productdefid = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();
				string tgproductdefid = tgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFID")).Distinct().FirstOrDefault().ToString();

				if (!productdefid.Equals(tgproductdefid))
				{
					grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

					// 다른 품목 ID는 선택할 수 없습니다.
					throw MessageException.Create("MixSelectProductDefID");
				}

				// Version 체크
				string defVersion = dt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();
				string tgdefVersion = tgdt.AsEnumerable().Select(r => r.Field<string>("PRODUCTDEFVERSION")).Distinct().FirstOrDefault().ToString();

				if (!defVersion.Equals(tgdefVersion))
				{
					grdWIP.View.CheckRow(grdWIP.View.GetFocusedDataSourceRowIndex(), false);

					// 다른 품목 버전은 선택할 수 없습니다.
					throw MessageException.Create("MixSelectProductDefVersion");
				}
			}
		}

		/// <summary>
		/// 탭 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			SmartBandedGrid grid = e.Page.Controls[0] as SmartBandedGrid;
			string pageName = tabControl.SelectedTabPage.Name;

			switch(pageName)
			{
				case "tabPageProductDef": //품목
					if(!grid.View.IsInitializeColumns)
					{
						InitializeProductDefInfoGrid();						
					}
					InitializeSendSegmentFrPdPopup();
					SetConditionVisiblility("P_LOTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_AREAID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOTPRODUCTTYPESTATUS", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_OWNTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFID2", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					Conditions.GetCondition("P_PRODUCTDEFID2").IsRequired = false;

					break;
				case "tabPageSegment": //공정
					if (!grid.View.IsInitializeColumns)
					{
						InitializeSegmentInfoGrid();
					}

					SetConditionVisiblility("P_LOTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_AREAID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOTPRODUCTTYPESTATUS", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_OWNTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFID2", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					Conditions.GetCondition("P_PRODUCTDEFID2").IsRequired = false;

					break;
				case "tabPageLot": //LOT
					if (!grid.View.IsInitializeColumns)
					{
						InitializeWipInfoGrid();
						InitializeTargetWipGrid();
					}
					InitializeSendSegmentFrLotPopup(false);
					SetConditionVisiblility("P_LOTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_AREAID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOTPRODUCTTYPESTATUS", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_OWNTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTDEFID2", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					Conditions.GetCondition("P_PRODUCTDEFID2").IsRequired = true;
					//Conditions.GetCondition("P_PRODUCTDEFID2").SetValidationIsRequired();
					//Conditions.GetControl<SmartSelectPopupEdit>("P_PRODUCTDEFID2").ForeColor = Color.Red;
					break;
			}
		}

		#endregion

		#region 툴바

		/// <summary>
		/// 저장 버튼을 클릭하면 호출한다.
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			// TODO : 저장 Rule 변경
			DataTable dtChecked = new DataTable();
			MessageWorker worker = new MessageWorker("SaveLotMessage");

			int index = tabControl.SelectedTabPageIndex;
			string showType = "";
			switch(index)
			{
				case 0://품목
					dtChecked = grdProductDefInfo.View.GetCheckedRows();

					showType = (ucMessageInfoProductDef.CheckedShowType == false) ? "N": "Y";

					string commentByProduct = ReplaceUrtf(ucMessageInfoProductDef.CommentText);

					worker.SetBody(new MessageBody()
					{
						{ "title", ucMessageInfoProductDef.TitleText },
						{ "message", commentByProduct },
						{ "sendSegment", Format.GetString(popSendSegmentFrPd.Editor.GetValue()) },
						{ "isPopupView", showType },
						{ "product", dtChecked }

					});
					break;
				case 1://공정
					dtChecked = grdSegmentInfo.View.GetCheckedRows();

					showType = (ucMessageInfoSegment.CheckedShowType == false) ? "N" : "Y";

					string commentBySegment = ReplaceUrtf(ucMessageInfoSegment.CommentText);

					worker.SetBody(new MessageBody()
					{
						{ "title", ucMessageInfoSegment.TitleText },
						{ "message", commentBySegment },
						{ "isPopupView", showType },
						{ "segment", dtChecked }

					});
					break;
				case 2://LOT
					dtChecked = grdTargetWip.DataSource as DataTable;

					showType = (ucMessageInfoLot.CheckedShowType == false) ? "N" : "Y";

					string commentByLot = ReplaceUrtf(ucMessageInfoLot.CommentText);

					worker.SetBody(new MessageBody()
					{
						{ "title", ucMessageInfoLot.TitleText },
						{ "message", commentByLot },
						{ "sendSegment", Format.GetString(popSendSegmentFrLot.Editor.GetValue()) },
						{ "isPopupView", showType },
						{ "lot", dtChecked }

					});

					break;
			}
			worker.Execute();

			ClearDatas();
		}

		#endregion


		#region 검색

		/// <summary>
		/// 검색 버튼을 클릭하면 조회조건에 맞는 데이터를 비동기 모델로 조회한다.
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			// TODO : 조회 SP 변경
			var values = Conditions.GetValues();
			values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
			values.Add("PLANTID", UserInfo.Current.Plant);
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			values.Add("PROCESSSEGMENTCLASSTYPE", "MiddleProcessSegmentClass");

			DataTable dtResult = new DataTable();
			int index = tabControl.SelectedTabPageIndex;
			switch(index)
			{
				case 0://품목
					ucMessageInfoProductDef.TitleText = "";
					ucMessageInfoProductDef.CommentText = "";

					dtResult = await QueryAsync("GetProductDefinitionList", "10001", values);
					grdProductDefInfo.DataSource = dtResult;

					//저장 전 체크했던 품목 다시 체크
					View_CheckStateChanged1(null, null);

					break;
				case 1://공정
					ucMessageInfoSegment.TitleText = "";
					ucMessageInfoSegment.CommentText = "";

					dtResult = await QueryAsync("GetProcessSegmentList", "10002", values);
					grdSegmentInfo.DataSource = dtResult;
					break;
				case 2://LOT
					ucMessageInfoLot.TitleText = "";
					ucMessageInfoLot.CommentText = "";
					grdTargetWip.View.ClearDatas();

					values.Remove("P_PRODUCTDEFID");
					dtResult = await QueryAsync("SelectWipLotMessageList", "10001", values);
					grdWIP.DataSource = dtResult;

					if(string.IsNullOrEmpty(Format.GetString(Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID").EditValue)))
					{
						InitializeSendSegmentFrLotPopup(true);
					}
					else
					{
						DataRow selectOne = dtResult.AsEnumerable().FirstOrDefault();
						InitializeSendSegmentFrLotPopup(true, selectOne["PRODUCTDEFID"], selectOne["PRODUCTDEFVERSION"]);
					}

					break;
			}

			if (dtResult.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData"); 
			}

		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();
			//Lot
			ConditionCollection lot = CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, true, Conditions, "", true);
			//품목코드
			CommonFunction.AddConditionProductPopup("P_PRODUCTDEFID", 0.5, true, Conditions);
			//품목코드 - LOT별
			InitializeCondition_ProductPopup();
            //작업장
            CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 0.1, false, Conditions, false, true);                 // 작업장
                                                                                                                             //작업공정
            CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 0.9, true, Conditions);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID2", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPosition(0.6)
				.SetPopupResultCount(1);

			// 팝업에서 사용되는 검색조건 (품목코드/명)
			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");
			//제품 구분 추가 2019.08.14 배선용, 노석안 대리 요청
			conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", $"CODECLASSID=ProductDivision2", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
				.SetDefault("Product");

			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 200);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 100);
			// 품목유형구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTDEFTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			// 생산구분
			conditionProductId.GridColumns.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ProductionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			// 단위
			conditionProductId.GridColumns.AddComboBoxColumn("UNIT", 90, new SqlQuery("GetUomDefinitionList", "10001", "UOMCLASSID=Segment"), "UOMDEFNAME", "UOMDEFID");
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
			SetConditionVisiblility("P_LOTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_AREAID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_LOTPRODUCTTYPESTATUS", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_OWNTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_PRODUCTDEFID2", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

			SmartSelectPopupEdit lot = Conditions.GetControl<SmartSelectPopupEdit>("P_LOTID");
			lot.EditValueChanged += Lot_EditValueChanged;
		}

		#endregion

		#region 유효성 검사

		/// <summary>
		/// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

			// TODO : 유효성 로직 변경

			int index = tabControl.SelectedTabPageIndex;
			DataTable changed = null;
			switch(index)
			{
				case 0://품목
					string sendSegment = Format.GetString(popSendSegmentFrPd.Editor.GetValue());
					if(string.IsNullOrEmpty(sendSegment))
					{
						//전달 공정 선택은 필수입니다.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("SENDSEGMENT"));
					}

					changed = grdProductDefInfo.View.GetCheckedRows();
					if (changed.Rows.Count == 0)
					{
						//품목 선택은 필수입니다.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("PRODUCTDEF"));
					}

					if(string.IsNullOrEmpty(ucMessageInfoProductDef.TitleText))
					{
						//제목을 입력하세요.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("TITLE"));
					}

					if(string.IsNullOrEmpty(ucMessageInfoProductDef.CommentText))
					{
						//메시지를 입력하세요.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("MESSAGE"));
					}
					break;
				case 1://공정
					changed = grdSegmentInfo.View.GetCheckedRows();
					if (changed.Rows.Count == 0)
					{
						//공정 선택은 필수입니다.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("PROCESSSEGMENT"));
					}

					if(string.IsNullOrEmpty(ucMessageInfoSegment.TitleText))
					{
						//제목을 입력하세요.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("TITLE"));
					}

					if(string.IsNullOrEmpty(ucMessageInfoSegment.CommentText))
					{
						//메시지를 입력하세요.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("MESSAGE"));
					}
					break;
				case 2://LOT
					string sendSegmentFrLot = Format.GetString(popSendSegmentFrLot.Editor.GetValue());
					if (string.IsNullOrEmpty(sendSegmentFrLot))
					{
						//전달 공정 선택은 필수입니다.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("SENDSEGMENT"));
					}

					DataTable dtTargetLotList = grdTargetWip.DataSource as DataTable;
					if(dtTargetLotList.Rows.Count == 0)
					{
						//저장할 데이터가 존재하지 않습니다.
						throw MessageException.Create("NoSaveData");
					}

					if(string.IsNullOrEmpty(ucMessageInfoLot.TitleText))
					{
						//제목을 입력하세요.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("TITLE"));
					}

					if(string.IsNullOrEmpty(ucMessageInfoLot.CommentText))
					{
						//메시지를 입력하세요.
						throw MessageException.Create("InValidOspRequiredField", Language.Get("MESSAGE"));
					}
					break;
			}

		}

		#endregion

		#region Private Function


		/// <summary>
		/// 저장 후 데이터 클리어
		/// </summary>
		private void ClearDatas()
		{
			popSendSegmentFrPd.Editor.EditValue = string.Empty;
			popSendSegmentFrLot.Editor.EditValue = string.Empty;
			grdTargetWip.View.ClearDatas();

			ucMessageInfoProductDef.CommentText = string.Empty;
			ucMessageInfoProductDef.TitleText = string.Empty;
			ucMessageInfoProductDef.CheckedShowType = false;

			ucMessageInfoSegment.CommentText = string.Empty;
			ucMessageInfoSegment.TitleText = string.Empty;
			ucMessageInfoSegment.CheckedShowType = false;

			ucMessageInfoLot.CommentText = string.Empty;
			ucMessageInfoLot.TitleText = string.Empty;
			ucMessageInfoLot.CheckedShowType = false;
		}


		/// <summary>
		/// 인코딩 urtf로 저장 될 시 rtf로 변환
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private string ReplaceUrtf(string text)
		{
			text.Replace(@"\urtf1", @"\rtf1");

			return text;
		}

		/*
		/// <summary>
		/// 메시지 유형 Combo 초기화
		/// </summary>
		private void InitializeMessageTypeCombo()
		{
			cboMessageType.Editor.ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly;
			cboMessageType.Editor.ValueMember = "CODEID";
			cboMessageType.Editor.DisplayMember = "CODENAME";

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			param.Add("CODECLASSID", "MessageType");

			cboMessageType.Editor.DataSource = SqlExecuter.Query("GetTypeList", "10001", param);
		}
		*/

		#endregion
	}
}
