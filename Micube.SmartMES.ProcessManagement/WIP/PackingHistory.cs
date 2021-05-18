#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

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
	/// 프 로 그 램 명  : 공정관리 > 포장관리 > 포장실적 조회
	/// 업  무  설  명  : 포장 실적 조회
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-11-04
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class PackingHistory : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public PackingHistory()
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
		}

		/// <summary>        
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			#region 포장 실적
			grdPackingList.GridButtonItem = GridButtonItem.Export;
			grdPackingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdPackingList.View.SetIsReadOnly();

			grdPackingList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();

            // 2020-12-21 오근영 (107) 상단 그리드 헤더 및 데이타 2개 추가 (모LOT, LOT 추가)
            grdPackingList.View.AddTextBoxColumn("PARENTLOTID", 250).SetTextAlignment(TextAlignment.Center);
            grdPackingList.View.AddTextBoxColumn("LOTID", 250).SetTextAlignment(TextAlignment.Center);

            grdPackingList.View.AddTextBoxColumn("PRODUCTDEFID", 150).SetIsReadOnly();
			grdPackingList.View.AddTextBoxColumn("PRODUCTDEFNAME", 300).SetIsReadOnly();
			grdPackingList.View.AddSpinEditColumn("PCSQTY", 100).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("{0:#,###}").SetIsReadOnly();
			grdPackingList.View.AddTextBoxColumn("WORKER", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly();
			grdPackingList.View.AddTextBoxColumn("PACKINGDATE", 200).SetTextAlignment(TextAlignment.Center).SetDisplayFormat("{0:yyyy-MM-dd HH:mm:ss}").SetIsReadOnly();
			grdPackingList.View.AddTextBoxColumn("DOCUMENTNO", 150).SetTextAlignment(TextAlignment.Center).SetIsReadOnly().SetLabel("OSPPRINTNO");

			grdPackingList.View.PopulateColumns();
			#endregion

			#region LOT 정보
			grdLotList.GridButtonItem = GridButtonItem.Export;
			grdLotList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdLotList.View.SetIsReadOnly();

			grdLotList.View.AddTextBoxColumn("BOXNO", 150).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("PARENTLOTID", 250).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("LOTID", 250).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("PRODUCTDEFID", 100).SetTextAlignment(TextAlignment.Left);
			grdLotList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250).SetTextAlignment(TextAlignment.Left);
			grdLotList.View.AddSpinEditColumn("PCSQTY", 70).SetTextAlignment(TextAlignment.Right);
			grdLotList.View.AddTextBoxColumn("WEEK", 70).SetTextAlignment(TextAlignment.Center);
			grdLotList.View.AddTextBoxColumn("PACKINGWEEK", 70).SetTextAlignment(TextAlignment.Center);

			grdLotList.View.PopulateColumns();
			#endregion
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			grdPackingList.View.FocusedRowChanged += View_FocusedRowChanged;
		}

		/// <summary>
		/// 포장 실적 row 클릭 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(grdPackingList.View.FocusedRowHandle < 0) return;

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

			DataTableClear();

			var values = Conditions.GetValues();
			values["P_PRODUCTNAME"] = Format.GetString(values["P_PRODUCTNAME"]).TrimEnd(',');

			DataTable dtPackingHistoryList = await QueryAsync("SelectPackingFinishedList", "10001", values);

			if (dtPackingHistoryList.Rows.Count < 1)
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdPackingList.DataSource = dtPackingHistoryList;
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			//품목코드
			InitializeCondition_ProductPopup();
			//작업장
			Commons.CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 2.5, true, Conditions, false, false);
			//LOTID
			Commons.CommonFunction.AddConditionLotPopup("P_LOTID", 2.7, true, Conditions);
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
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeCondition_ProductPopup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefinitionList", "10002", $"PLANTID={UserInfo.Current.Plant}"), "PRODUCTDEF", "PRODUCTDEF")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEFID")
				.SetPosition(1.2)
				.SetPopupResultCount(0)
				.SetPopupApplySelection((selectRow, gridRow) => {
					string productDefName = "";

					selectRow.AsEnumerable().ForEach(r => {
						productDefName += Format.GetString(r["PRODUCTDEFNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_PRODUCTNAME").EditValue = productDefName;
				});

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
		#endregion

		#region Private Function

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void DataTableClear()
		{
			grdPackingList.View.ClearDatas();
			grdLotList.View.ClearDatas();
		}

		/// <summary>
		/// 선택 된 row 데이터 바인드
		/// </summary>
		private void FocusedRowDataBind()
		{
			DataRow selectRow = grdPackingList.View.GetFocusedDataRow();
			if (selectRow == null) return;

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("P_BOXNO", selectRow["BOXNO"].ToString());

			DataTable dt = SqlExecuter.Query("SelectPackingLotList", "10001", param);

			grdLotList.DataSource = dt;
		}
		#endregion
	}
}
