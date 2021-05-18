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
	/// 프 로 그 램 명  : 공정관리 > 생산실적 > 자재 사용 이력
	/// 업  무  설  명  : 자재LOT별 사용이력조회
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-09-18
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LotConsumableHistory : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public LotConsumableHistory()
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


			// TODO : 컨트롤 초기화 로직 구성
			InitializeGrid();
		}

		/// <summary>        
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			// TODO : 그리드 초기화 로직 추가
			grdConsumableLotHistory.GridButtonItem = GridButtonItem.Export;
			grdConsumableLotHistory.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdConsumableLotHistory.View.SetIsReadOnly();

			//처리일자
			grdConsumableLotHistory.View.AddTextBoxColumn("CLOSEDATE", 130).SetTextAlignment(TextAlignment.Center);
			//자재코드
			grdConsumableLotHistory.View.AddTextBoxColumn("CONSUMABLEDEFID", 100);
			grdConsumableLotHistory.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 50).SetTextAlignment(TextAlignment.Center);
			//자재명
			grdConsumableLotHistory.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
			//단위
			grdConsumableLotHistory.View.AddTextBoxColumn("UNIT", 50).SetTextAlignment(TextAlignment.Center);
			//자재LOT NO
			grdConsumableLotHistory.View.AddTextBoxColumn("CONSUMABLELOTID", 150).SetTextAlignment(TextAlignment.Center);
			//사용된 LOTID
			grdConsumableLotHistory.View.AddTextBoxColumn("USEDLOTID", 150).SetTextAlignment(TextAlignment.Center);
			//사용 품목코드
			grdConsumableLotHistory.View.AddTextBoxColumn("USEDPRODUCTDEFID", 100);
			//사용 품목명
			grdConsumableLotHistory.View.AddTextBoxColumn("USEDPRODUCTDEFNAME", 150);
			//입고수량
			grdConsumableLotHistory.View.AddSpinEditColumn("INQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
			//소모수량
			grdConsumableLotHistory.View.AddSpinEditColumn("CONSUMEDQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);
			//현재수량
			grdConsumableLotHistory.View.AddSpinEditColumn("CONSUMABLELOTQTY", 80).SetTextAlignment(TextAlignment.Right).SetDisplayFormat("#,##0.##", MaskTypes.Numeric, true);		
			//청구번호
			//grdConsumableLotHistory.View.AddTextBoxColumn("REQUESTNO", 150);
			//청구구분
			grdConsumableLotHistory.View.AddTextBoxColumn("TRANSACTIONCODE", 120);
			//창고코드
			grdConsumableLotHistory.View.AddTextBoxColumn("WAREHOUSEID", 80);
			//작업장
			grdConsumableLotHistory.View.AddTextBoxColumn("AREAID").SetIsHidden();
			grdConsumableLotHistory.View.AddTextBoxColumn("AREANAME", 150);

			grdConsumableLotHistory.View.PopulateColumns();
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

			DataTable dtMaterialHistory = await QueryAsyncDirect("SelectConsumableLotHistory", "10001", values);

			if (dtMaterialHistory.Rows.Count < 1)
			{
				// 조회할 데이터가 없습니다.
				ShowMessage("NoSelectData"); 
			}

			grdConsumableLotHistory.DataSource = dtMaterialHistory;
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //공급사
            //InitializeCondition_VendorPopup();
            //작업장
            Commons.CommonFunction.AddConditionAreaByAuthorityPopup("P_AREAID", 2.2, false, Conditions, false, false);
			//자재코드
			InitializeCondition_ConsumableDefPopup();
		}


		/// <summary>
		/// 공급사 - 팝업형 조회조건 생성
		/// </summary>
		private void InitializeCondition_VendorPopup()
		{
			var vendorPopup = Conditions.AddSelectPopup("VENDORID", new SqlQuery("GetVendorList", "10001"), "VENDORID")
				.SetPopupLayout("SELECTVENDORID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("VENDORNAME")
				.SetPosition(2.5)
				.SetLabel("CONVENDORNAME");

			vendorPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
				.SetLabel("SITE")
				.SetDefault(UserInfo.Current.Plant);
			vendorPopup.Conditions.AddTextBox("VENDORID");

			vendorPopup.GridColumns.AddTextBoxColumn("VENDORID", 150);
			vendorPopup.GridColumns.AddTextBoxColumn("VENDORNAME", 250);
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
				.SetPosition(2.5)
				.SetLabel("MATERIALDEF")
				.SetPopupApplySelection((selectRow, gridRow) =>  
				{
					string consumableName = "";

					selectRow.AsEnumerable().ForEach(r => {
						consumableName += Format.GetString(r["CONSUMABLEDEFNAME"]) + ",";
					});

					Conditions.GetControl<SmartTextBox>("P_CONSUMENAME").EditValue = consumableName;
				});

			consumableDefPopup.Conditions.AddComboBox("CONSUMABLECLASSID", new SqlQuery("GetConsumableclassListByCsm", "10001"), "CONSUMABLECLASSNAME", "CONSUMABLECLASSID").SetEmptyItem();
			consumableDefPopup.Conditions.AddTextBox("CONSUMABLEDEF");

			consumableDefPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFID", 150);
			consumableDefPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFVERSION", 80);
			consumableDefPopup.GridColumns.AddTextBoxColumn("CONSUMABLEDEFNAME", 250);
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
			SmartSelectPopupEdit area = Conditions.GetControl<SmartSelectPopupEdit>("P_AREAID");
			area.LanguageKey = "AREANAME";
			area.LabelText = Language.Get(area.LanguageKey);

			Conditions.GetControl<SmartTextBox>("P_CONSUMABLELOT").ImeMode = ImeMode.Alpha;
		}

		#endregion

		#region Private Function

		#endregion
	}
}