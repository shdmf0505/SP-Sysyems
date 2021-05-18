#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
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
	/// 프 로 그 램 명  : 공정관리 > 자재관리 > 자재 입출고 내역
	/// 업  무  설  명  : 자재 입출고내역 조회
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-23
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ConsumableInOutHistory : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public ConsumableInOutHistory()
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
			InitializeMainGrid();
			InitializeHistoryInfoGrid();
		}

		/// <summary>        
		/// 자재 입출고 내역 그리드를 초기화
		/// </summary>
		private void InitializeMainGrid()
		{
			// TODO : 그리드 초기화 로직 추가
			grdInOutHistory.GridButtonItem = GridButtonItem.Export;
			grdInOutHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdInOutHistory.View.SetIsReadOnly();

			//그룹
			var defaultCol = grdInOutHistory.View.AddGroupColumn("");
			//작업장
			defaultCol.AddTextBoxColumn("WAREHOUSENAME", 150);
			//자재품목코드
			defaultCol.AddTextBoxColumn("CONSUMABLEDEFID", 150);
			//자재품목명
			defaultCol.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
			//단위
			defaultCol.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);

			//그룹 - 입고
			var inbound = grdInOutHistory.View.AddGroupColumn("INBOUND");
			//불출
			inbound.AddSpinEditColumn("INBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//재고이동
			inbound.AddSpinEditColumn("MOVEINBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//기타입고MaterialInventoryAdjustment
			inbound.AddSpinEditColumn("MISCINBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//생산입고
			inbound.AddSpinEditColumn("WIPINBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);

			//그룹 - 출고
			var outbound = grdInOutHistory.View.AddGroupColumn("OUTBOUND");
			//반납
			outbound.AddSpinEditColumn("RETURNQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//재고이동
			outbound.AddSpinEditColumn("MOVEOUTBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//기타출고
			outbound.AddSpinEditColumn("MISCOUTBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);
			//공정출고
			outbound.AddSpinEditColumn("WIPOUTBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetTextAlignment(TextAlignment.Right);

			grdInOutHistory.View.PopulateColumns();
		}

		/// <summary>
		/// 이력 정보 그리드 초기화
		/// </summary>
		private void InitializeHistoryInfoGrid()
		{
			#region 입고 - 불출
			grdConsumRequestInbound.GridButtonItem = GridButtonItem.Export;
			grdConsumRequestInbound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdConsumRequestInbound.View.SetIsReadOnly();

			//청구번호
			grdConsumRequestInbound.View.AddTextBoxColumn("REQUESTNO", 120).SetTextAlignment(TextAlignment.Center);
			//공정
			grdConsumRequestInbound.View.AddTextBoxColumn("PROCESSSEGMENTID").SetIsHidden();
			grdConsumRequestInbound.View.AddTextBoxColumn("PROCESSSEGMENTVERSION").SetIsHidden();
			grdConsumRequestInbound.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
			//작업장
			grdConsumRequestInbound.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdConsumRequestInbound.View.AddTextBoxColumn("AREANAME", 120);
			//자재구분
			grdConsumRequestInbound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdConsumRequestInbound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdConsumRequestInbound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdConsumRequestInbound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdConsumRequestInbound.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			//기본수량
			grdConsumRequestInbound.View.AddSpinEditColumn("QTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true);
			//소요수량
			grdConsumRequestInbound.View.AddSpinEditColumn("REQUIREQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("USEQTY");
			//청구수량
			grdConsumRequestInbound.View.AddSpinEditColumn("REQUESTQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("CHARGEQTY");
			//인수수량
			grdConsumRequestInbound.View.AddSpinEditColumn("INBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("INQTY");
			//요청자
			grdConsumRequestInbound.View.AddTextBoxColumn("REQUESTUSER", 80).SetTextAlignment(TextAlignment.Center).SetLabel("REQUESTUSER");
			//요청일
			grdConsumRequestInbound.View.AddTextBoxColumn("REQUESTDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("REQDATE");
			//최종인수자
			grdConsumRequestInbound.View.AddTextBoxColumn("RECEIPTUSERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEUSER");
			//최종인수일
			grdConsumRequestInbound.View.AddTextBoxColumn("RECEIPTDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEDATE");

			grdConsumRequestInbound.View.PopulateColumns();
			#endregion

			#region 입고 - 재고이동
			grdStockMoveInbound.GridButtonItem = GridButtonItem.Export;
			grdStockMoveInbound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdStockMoveInbound.View.SetIsReadOnly();

			//입고번호
			grdStockMoveInbound.View.AddTextBoxColumn("RELKEYNO", 120).SetLabel("TRANSACTIONNO").SetTextAlignment(TextAlignment.Center);
			//작업장
			grdStockMoveInbound.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdStockMoveInbound.View.AddTextBoxColumn("AREANAME", 120);
			//이동작업장
			grdStockMoveInbound.View.AddTextBoxColumn("RELATIONAREAID").SetIsHidden();
			grdStockMoveInbound.View.AddTextBoxColumn("RELATIONAREANAME", 120);
			//자재구분
			grdStockMoveInbound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdStockMoveInbound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdStockMoveInbound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdStockMoveInbound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdStockMoveInbound.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			//입고유형
			grdStockMoveInbound.View.AddTextBoxColumn("TRANSACTIONTYPE", 100).SetTextAlignment(TextAlignment.Center);
			//단위
			grdStockMoveInbound.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			//수량
			grdStockMoveInbound.View.AddSpinEditColumn("INBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("QTY");
			//최종인수자
			grdStockMoveInbound.View.AddTextBoxColumn("RECEIPTUSERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEUSER");
			//최종인수일
			grdStockMoveInbound.View.AddTextBoxColumn("RECEIPTDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEDATE");

			grdStockMoveInbound.View.PopulateColumns();
			#endregion

			#region 입고 - 기타입고
			grdEtcInbound.GridButtonItem = GridButtonItem.Export;
			grdEtcInbound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdEtcInbound.View.SetIsReadOnly();

			//입고번호
			grdEtcInbound.View.AddTextBoxColumn("RELKEYNO", 120).SetLabel("TRANSACTIONNO").SetTextAlignment(TextAlignment.Center);
			//작업장
			grdEtcInbound.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdEtcInbound.View.AddTextBoxColumn("AREANAME", 120);
			//자재구분
			grdEtcInbound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdEtcInbound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdEtcInbound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdEtcInbound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdEtcInbound.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			//입고유형
			grdEtcInbound.View.AddTextBoxColumn("TRANSACTIONTYPE", 100).SetTextAlignment(TextAlignment.Center);
			//단위
			grdEtcInbound.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			//수량
			grdEtcInbound.View.AddSpinEditColumn("INBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("QTY");
			//최종인수자
			grdEtcInbound.View.AddTextBoxColumn("RECEIPTUSERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEUSER");
			//최종인수일
			grdEtcInbound.View.AddTextBoxColumn("RECEIPTDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEDATE");

			grdEtcInbound.View.PopulateColumns();
			#endregion

			#region 입고 - 생산입고
			grdWipInbound.GridButtonItem = GridButtonItem.Export;
			grdWipInbound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdWipInbound.View.SetIsReadOnly();

			//입고번호
			grdWipInbound.View.AddTextBoxColumn("RELKEYNO", 120).SetLabel("TRANSACTIONNO").SetTextAlignment(TextAlignment.Center);
			//작업장
			grdWipInbound.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdWipInbound.View.AddTextBoxColumn("AREANAME", 120);
			//자재구분
			grdWipInbound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdWipInbound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdWipInbound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdWipInbound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdWipInbound.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			//입고유형
			grdWipInbound.View.AddTextBoxColumn("TRANSACTIONTYPE", 100).SetTextAlignment(TextAlignment.Center);
			//단위
			grdWipInbound.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			//수량
			grdWipInbound.View.AddSpinEditColumn("INBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("QTY");
			//최종인수자
			grdWipInbound.View.AddTextBoxColumn("RECEIPTUSERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEUSER");
			//최종인수일
			grdWipInbound.View.AddTextBoxColumn("RECEIPTDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTRECEIVEDATE");

			grdWipInbound.View.PopulateColumns();
			#endregion

			#region 출고 - 반납
			grdReturnOutbound.GridButtonItem = GridButtonItem.Export;
			grdReturnOutbound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdReturnOutbound.View.SetIsReadOnly();

			//출고번호
			grdReturnOutbound.View.AddTextBoxColumn("RELKEYNO", 120).SetLabel("TRANSACTIONNOISSUE").SetTextAlignment(TextAlignment.Center);
			//작업장
			grdReturnOutbound.View.AddTextBoxColumn("AREAID");
			grdReturnOutbound.View.AddTextBoxColumn("AREANAME", 120);
			//이동작업장
			grdReturnOutbound.View.AddTextBoxColumn("TOAREA", 120).SetTextAlignment(TextAlignment.Center);
			//자재구분
			grdReturnOutbound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdReturnOutbound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdReturnOutbound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdReturnOutbound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdReturnOutbound.View.AddTextBoxColumn("LOTID", 200).SetTextAlignment(TextAlignment.Center);
			//출고유형
			grdReturnOutbound.View.AddTextBoxColumn("TRANSACTIONTYPE", 100).SetTextAlignment(TextAlignment.Center);
			//단위
			grdReturnOutbound.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			//수량
			grdReturnOutbound.View.AddSpinEditColumn("OUTBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true);
			//최종출고자
			grdReturnOutbound.View.AddTextBoxColumn("USERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTUSER");
			//최종출고일
			grdReturnOutbound.View.AddTextBoxColumn("TXNDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTDATE");

			grdReturnOutbound.View.PopulateColumns();
			#endregion

			#region 출고 - 재고이동
			grdStockMoveOutbound.GridButtonItem = GridButtonItem.Export;
			grdStockMoveOutbound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdStockMoveOutbound.View.SetIsReadOnly();

			//출고번호
			grdStockMoveOutbound.View.AddTextBoxColumn("RELKEYNO", 120).SetLabel("TRANSACTIONNOISSUE").SetTextAlignment(TextAlignment.Center);
			//작업장
			grdStockMoveOutbound.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdStockMoveOutbound.View.AddTextBoxColumn("AREANAME", 120);
			//이동작업장
			grdStockMoveOutbound.View.AddTextBoxColumn("RELATIONAREAID").SetIsHidden();
			grdStockMoveOutbound.View.AddTextBoxColumn("RELATIONAREANAME", 120);
			//자재구분
			grdStockMoveOutbound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdStockMoveOutbound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdStockMoveOutbound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdStockMoveOutbound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdStockMoveOutbound.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			//출고유형
			grdStockMoveOutbound.View.AddTextBoxColumn("TRANSACTIONTYPE", 100).SetTextAlignment(TextAlignment.Center);
			//단위
			grdStockMoveOutbound.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			//수량
			grdStockMoveOutbound.View.AddSpinEditColumn("OUTBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("QTY");
			//최종출고자
			grdStockMoveOutbound.View.AddTextBoxColumn("USERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTUSER");
			//최종출고일
			grdStockMoveOutbound.View.AddTextBoxColumn("TXNDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTDATE");

			grdStockMoveOutbound.View.PopulateColumns();
			#endregion

			#region 출고 - 기타출고
			grdEtcOutbound.GridButtonItem = GridButtonItem.Export;
			grdEtcOutbound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdEtcOutbound.View.SetIsReadOnly();

			//출고번호
			grdEtcOutbound.View.AddTextBoxColumn("RELKEYNO", 120).SetLabel("TRANSACTIONNOISSUE").SetTextAlignment(TextAlignment.Center);
			//작업장
			grdEtcOutbound.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdEtcOutbound.View.AddTextBoxColumn("AREANAME", 120);
			//자재구분
			grdEtcOutbound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdEtcOutbound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdEtcOutbound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdEtcOutbound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdEtcOutbound.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			//출고유형
			grdEtcOutbound.View.AddTextBoxColumn("TRANSACTIONTYPE", 100).SetTextAlignment(TextAlignment.Center);
			//단위
			grdEtcOutbound.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			//수량
			grdEtcOutbound.View.AddSpinEditColumn("OUTBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("QTY");
			//최종출고자
			grdEtcOutbound.View.AddTextBoxColumn("USERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTUSER");
			//최종출고일
			grdEtcOutbound.View.AddTextBoxColumn("TXNDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTDATE");

			grdEtcOutbound.View.PopulateColumns();
			#endregion

			#region 출고 - 공정출고
			grdWipOutBound.GridButtonItem = GridButtonItem.Export;
			grdWipOutBound.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdWipOutBound.View.SetIsReadOnly();

			//출고번호
			grdWipOutBound.View.AddTextBoxColumn("RELKEYNO", 120).SetLabel("TRANSACTIONNOISSUE").SetTextAlignment(TextAlignment.Center);
			//작업장
			grdWipOutBound.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdWipOutBound.View.AddTextBoxColumn("AREANAME", 120);
			//자재구분
			grdWipOutBound.View.AddTextBoxColumn("MATERIALCLASSIFICATION", 80).SetTextAlignment(TextAlignment.Center);
			//자재품목코드
			grdWipOutBound.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
			grdWipOutBound.View.AddTextBoxColumn("CONSUMABLEDEFVERSION").SetIsHidden();
			//자재품목명
			grdWipOutBound.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//LOTID OR CONSUMABLELOTID
			grdWipOutBound.View.AddTextBoxColumn("LOTID", 180).SetTextAlignment(TextAlignment.Center);
			//출고유형
			grdWipOutBound.View.AddTextBoxColumn("TRANSACTIONTYPE", 100).SetTextAlignment(TextAlignment.Center);
			//단위
			grdWipOutBound.View.AddTextBoxColumn("UNIT", 60).SetTextAlignment(TextAlignment.Center);
			//수량
			grdWipOutBound.View.AddSpinEditColumn("OUTBOUNDQTY", 80).SetDisplayFormat("#,##0.#####", MaskTypes.Numeric, true).SetLabel("QTY");
			//최종인수자
			grdWipOutBound.View.AddTextBoxColumn("USERNAME", 80).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTUSER");
			//최종인수일
			grdWipOutBound.View.AddTextBoxColumn("TXNDATE", 130).SetTextAlignment(TextAlignment.Center).SetLabel("LASTSENTDATE");

			grdWipOutBound.View.PopulateColumns();
			#endregion
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			grdInOutHistory.View.FocusedRowChanged += View_FocusedRowChanged;
			tabInConsumOutHistory.SelectedPageChanged += TabInConsumOutHistory_SelectedPageChanged;
		}

		/// <summary>
		/// 이력정보 탭 별 조회
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabInConsumOutHistory_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			FocusedRowDataBind();
		}


		/// <summary>
		/// 자재 입출고 내역 Row 선택 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(grdInOutHistory.View.FocusedRowHandle < 0) return;

			FocusedRowDataBind();
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
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			//자재 입출고 내역
			DataTable dtInOutHistory = await QueryAsync("SelectInOutHistoryList", "10001", values);
			grdInOutHistory.DataSource = dtInOutHistory;

			OnSearchData(values);

			grdInOutHistory.View.FocusedRowHandle = 0;
			grdInOutHistory.View.SelectRow(grdInOutHistory.View.FocusedRowHandle);
			FocusedRowDataBind();
		}


		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            //작업장-2020.06.30-유석진-창고명으로 조회조건 변경으로 인한 주석 처리
            //Commons.CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 1.1, true, Conditions, false, false);
            // 창고명
            InitializeConditionPopup_Warehouseid();
            //자재코드
            InitializeCondition_ConsumableDefPopup();
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
		}

		/// <summary>
		/// 자재코드 - 팝업형 조회조건 생성
		/// </summary>
		private void InitializeCondition_ConsumableDefPopup()
		{
			var consumableDefPopup = Conditions.AddSelectPopup("P_CONSUMABLEDEFID", new SqlQuery("GetConsumableDefList", "10002", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "CONSUMEDEF", "CONSUMEDEF")
				.SetPopupLayout("SELECTCONSUMABLEDEFID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("CONSUMABLEDEFNAME")
				.SetPosition(1.3)
				.SetLabel("MATERIALDEF");

			consumableDefPopup.Conditions.AddComboBox("CONSUMABLECLASSID", new SqlQuery("GetConsumableclassListByCsm", "10001"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID").SetEmptyItem();
			consumableDefPopup.Conditions.AddTextBox("CONSUMABLEDEF");

			consumableDefPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
			consumableDefPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
			consumableDefPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
		}

        /// <summary>
        /// 창고명 조회조건-2020.06.30-유석진-창공명 조회 조건 추가(작업장에서 변경)
        /// </summary>
        private void InitializeConditionPopup_Warehouseid()
        {
            var popupProduct = Conditions.AddSelectPopup("P_WAREHOUSEID",
                                                              new SqlQuery("GetWarehouseidListByCsm", "10001"
                                                                             , $"P_ENTERPRISEID={UserInfo.Current.Enterprise}"
                                                                             , $"P_PLANTID={UserInfo.Current.Plant}"
                                                                             , $"LANGUAGETYPE={UserInfo.Current.LanguageType}"
                                                                             , $"USERID={UserInfo.Current.Id}"
                                                                              ), "WAREHOUSENAME", "WAREHOUSEID")
               .SetPopupLayout("WAREHOUSENAME", PopupButtonStyles.Ok_Cancel, true, false)
               .SetPopupLayoutForm(520, 600)
               .SetLabel("WAREHOUSENAME")
               .SetPopupResultCount(1)
               .SetPosition(1.1);
            // 팝업 조회조건
            popupProduct.Conditions.AddTextBox("P_WAREHOUSENAME")
                .SetLabel("WAREHOUSENAME");
            popupProduct.GridColumns.AddTextBoxColumn("WAREHOUSEID", 120)
                .SetValidationKeyColumn();
            popupProduct.GridColumns.AddTextBoxColumn("WAREHOUSENAME", 200);


        }

        #endregion

        #region Private Function
        /// <summary>
        /// 선택된 탭 이력정보 데이터 바인드
        /// </summary>
        private void FocusedRowDataBind()
		{
			DataRow selected = grdInOutHistory.View.GetFocusedDataRow();

			if(selected == null) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("P_PERIOD_PERIODFR", Conditions.GetValues()["P_PERIOD_PERIODFR"]);
			param.Add("P_PERIOD_PERIODTO", Conditions.GetValues()["P_PERIOD_PERIODTO"]);
			param.Add("WAREHOUSEID", Format.GetString(selected["WAREHOUSEID"]));
			param.Add("CONSUMABLEDEFID", Format.GetString(selected["CONSUMABLEDEFID"]));
			param.Add("CONSUMABLEDEFVERSION", Format.GetString(selected["CONSUMABLEDEFVERSION"]));
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);

			OnSearchData(param);
		}

		/// <summary>
		/// 하단 탭 데이터 조회
		/// </summary>
		private void OnSearchData(Dictionary<string, object> values)
		{
			int index = tabInConsumOutHistory.SelectedTabPageIndex;
			switch (index)
			{
				case 0://입고 - 불출
					values.Add("TYPE", "INBOUND_CONSUMREQUEST");
					DataTable dtConsumRequestInbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdConsumRequestInbound.DataSource = dtConsumRequestInbound;
					break;
				case 1://입고 - 재고이동
					values.Add("TYPE", "INBOUND_STOCKMOVE");
					DataTable dtStockMoveInbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdStockMoveInbound.DataSource = dtStockMoveInbound;
					break;
				case 2://입고 - 기타입고
					values.Add("TYPE", "INBOUND_ETC");
					DataTable dtEtcInbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdEtcInbound.DataSource = dtEtcInbound;
					break;
				case 3://입고 - 생산입고
					values.Add("TYPE", "INBOUND_WIP");
					DataTable dtWipInbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdWipInbound.DataSource = dtWipInbound;
					break;
				case 4://출고 - 반납
					values.Add("TYPE", "OUTBOUND_RETURN");
					DataTable dtReturnOutbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdReturnOutbound.DataSource = dtReturnOutbound;
					break;
				case 5://출고 - 재고이동
					values.Add("TYPE", "OUTBOUND_STOCKMOVE");
					DataTable dtStockMoveOutbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdStockMoveOutbound.DataSource = dtStockMoveOutbound;
					break;
				case 6://출고 - 기타출고
					values.Add("TYPE", "OUTBOUND_ETC");
					DataTable dtEtcOutbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdEtcOutbound.DataSource = dtEtcOutbound;
					break;
				case 7://출고 - 공정출고
					values.Add("TYPE", "OUTBOUND_WIP");
					DataTable dtWipOutbound = SqlExecuter.Query("SelectInOutHistoryInfoList", "10001", values);
					grdWipOutBound.DataSource = dtWipOutbound;
					break;
			}
		}

		#endregion
	}
}