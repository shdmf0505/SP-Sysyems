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

namespace Micube.SmartMES.StandardInfo
{
	/// <summary>
	/// 프 로 그 램 명  : 기준정보 > Setup > 고객사/제품 라벨 매핑
	/// 업  무  설  명  : 고객사, 제품별 라벨을 매핑한다.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-07-11
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LabelMap : SmartConditionManualBaseForm
	{
		#region Local Variables

		#endregion

		#region 생성자

		public LabelMap()
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
		/// 코드그룹 리스트 그리드를 초기화한다.
		/// </summary>
		private void InitializeGrid()
		{
			// TODO : 그리드 초기화 로직 추가
			grdLabelMapList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			//라벨 ID
			InitializeGrid_LabelDefIdPopup();
			//라벨 타입
			grdLabelMapList.View.AddTextBoxColumn("LABELTYPE", 100)
				.SetIsReadOnly();
            //라벨 타입
            grdLabelMapList.View.AddTextBoxColumn("LABELDEFNAME", 180)
                .SetIsReadOnly();
            //제품 ID
            grdLabelMapList.View.AddTextBoxColumn("PRODUCTDEFID").SetIsHidden();
			InitializeGrid_ProductDefNamePopup();
            //고객사
            //grdLabelMapList.View.AddTextBoxColumn("CUSTOMERID").SetValidationIsRequired();
            InitializeGrid_CustomerIdPopup();
            grdLabelMapList.View.AddTextBoxColumn("CUSTOMERNAME", 120).SetValidationIsRequired();

			//설명
			grdLabelMapList.View.AddTextBoxColumn("DESCRIPTION", 200);

			//생성자, 수정자, 생성시간, 수정시간
			grdLabelMapList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
			grdLabelMapList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
			grdLabelMapList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(Framework.SmartControls.TextAlignment.Center);
			grdLabelMapList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(Framework.SmartControls.TextAlignment.Center);

			grdLabelMapList.View.PopulateColumns();

		}

		/// <summary>
		/// 팝업형 그리드 컬럼 - 라벨 ID
		/// </summary>
		private void InitializeGrid_LabelDefIdPopup()
		{
			var labelDefPopupColumn = grdLabelMapList.View.AddSelectPopupColumn("LABELDEFID", 100, new SqlQuery("GetLabelDefList", "10001", "LABELCLASSID=PackingLabel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
				.SetPopupLayout("SELECTLABELDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetValidationKeyColumn()
				.SetValidationIsRequired()
				.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LABELDEFNAME")
				.SetPopupApplySelection((selectedRows, dataGridRows) =>
				{
					DataRow selectedRow = selectedRows.AsEnumerable().FirstOrDefault();
					dataGridRows["LABELTYPE"] = selectedRow == null? "" : Format.GetString(selectedRow["LABELTYPE"]);

				});

			//labelDefPopupColumn.Conditions.AddComboBox("LABELCLASSID", new SqlQuery("GetLabelClassList", "10001", "LABELCLASSID=포장", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "LABELCLASSNAME", "LABELCLASSID").SetIsReadOnly();
			labelDefPopupColumn.Conditions.AddTextBox("TXTLABELDEFID");

			//라벨 그룹
			//labelDefPopupColumn.GridColumns.AddTextBoxColumn("LABELCLASSID", 150);			
			//라벨 ID
			labelDefPopupColumn.GridColumns.AddTextBoxColumn("LABELDEFID", 150);
			//라벨 명
			labelDefPopupColumn.GridColumns.AddTextBoxColumn("LABELDEFNAME", 200);
			//라벨 타입
			labelDefPopupColumn.GridColumns.AddTextBoxColumn("LABELTYPE", 100);
		}

		/// <summary>
		/// 팝업형 그리드 컬럼 - 고객사 ID
		/// </summary>
		private void InitializeGrid_CustomerIdPopup()
		{
			var customerPopupColumn = grdLabelMapList.View.AddSelectPopupColumn("CUSTOMERID", 150, new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetValidationKeyColumn()
				.SetValidationIsRequired()
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("CUSTOMERNAME")
                .SetPopupApplySelection((selectRow, gridRow) => {
                    DataRow selectedRow = selectRow.AsEnumerable().FirstOrDefault();
                    gridRow["CUSTOMERNAME"] = selectedRow == null ? "" : Format.GetString(selectedRow["CUSTOMERNAME"]);
                });

			customerPopupColumn.Conditions.AddTextBox("TXTCUSTOMERID");

			customerPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
			customerPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
		}

		/// <summary>
		/// 팝업형 그리드 컬럼 - 제품 명
		/// </summary>
		private void InitializeGrid_ProductDefNamePopup()
		{
			var productDefPopupColumn = grdLabelMapList.View.AddSelectPopupColumn("PRODUCTDEFNAME", 250, new SqlQuery("GetProductDefList", "10003"))
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetValidationKeyColumn()
				.SetValidationIsRequired()
				.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetPopupApplySelection((selectRow, gridRow) => {
					DataRow selectedRow = selectRow.AsEnumerable().FirstOrDefault();
					gridRow["PRODUCTDEFID"] = selectedRow == null ? "" : Format.GetString(selectedRow["PRODUCTDEFID"]);
					gridRow["CUSTOMERID"]  = selectedRow == null ? "" : Format.GetString(selectedRow["CUSTOMERID"]);
					gridRow["CUSTOMERNAME"] = selectedRow == null ? "" : Format.GetString(selectedRow["CUSTOMERNAME"]);
				});

			productDefPopupColumn.Conditions.AddTextBox("PRODUCTDEF");

			productDefPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			productDefPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
			productDefPopupColumn.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
			productDefPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERID", 80).SetIsHidden();
			productDefPopupColumn.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
		}

		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
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
			DataTable changed = grdLabelMapList.GetChangedRows();

			ExecuteRule("LabelMap", changed);
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
			values.Add("LanguageType", UserInfo.Current.LanguageType);

			DataTable dtLabelMapList = await QueryAsync("SelectLabelMap", "10001", values);

			if (dtLabelMapList.Rows.Count < 1)
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdLabelMapList.DataSource = dtLabelMapList;
		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용

			//라벨 ID
			InitializeConditionLabelDefId_Popup();
			//고객사 ID
			InitializeConditionCustomerDefId_Popup();
			//품목 ID
			InitializeConditionProductDefId_Popup();
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
		/// 팝업형 조회조건 생성 - 라벨
		/// </summary>
		private void InitializeConditionLabelDefId_Popup()
		{
			var conditionLabelDefId = Conditions.AddSelectPopup("P_LABELDEFID", new SqlQuery("GetLabelDefList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "LABELDEFNAME", "LABELDEFID")
				.SetPopupLayout("SELECTLABELDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(0)
				.SetLabel("LABELDEFID")
				.SetPopupLayoutForm(800, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LABELDEFNAME")
				.SetPosition(0.1);

			conditionLabelDefId.Conditions.AddComboBox("LABELCLASSID", new SqlQuery("GetLabelClassList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "LABELCLASSNAME", "LABELCLASSID");
			conditionLabelDefId.Conditions.AddTextBox("TXTLABELDEFID");

			//라벨 그룹
			conditionLabelDefId.GridColumns.AddTextBoxColumn("LABELCLASSID", 150);
			//라벨 타입
			conditionLabelDefId.GridColumns.AddTextBoxColumn("LABELTYPE", 100);
			//라벨 ID
			conditionLabelDefId.GridColumns.AddTextBoxColumn("LABELDEFID", 150);
			//라벨 명
			conditionLabelDefId.GridColumns.AddTextBoxColumn("LABELDEFNAME", 200);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 고객사
		/// </summary>
		private void InitializeConditionCustomerDefId_Popup()
		{
			var conditionCustomerId = Conditions.AddSelectPopup("P_CUSTOMERID", new SqlQuery("GetCustomerList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "CUSTOMERNAME", "CUSTOMERID")
				.SetPopupLayout("SELECTCUSTOMERID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(0)
				.SetLabel("CUSTOMERID")
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("CUSTOMERNAME")
				.SetPosition(0.2);

			conditionCustomerId.Conditions.AddTextBox("TXTCUSTOMERID");

			//고객 ID
			conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERID", 80);
			//고객명
			conditionCustomerId.GridColumns.AddTextBoxColumn("CUSTOMERNAME", 150);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목코드
		/// </summary>
		private void InitializeConditionProductDefId_Popup()
		{
			var conditionProductDefId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFNAME", "PRODUCTDEFID")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(0)
				.SetLabel("PRODUCTDEFID")
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetPosition(0.3);

			conditionProductDefId.Conditions.AddTextBox("PRODUCTDEF");

			conditionProductDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			conditionProductDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80);
			conditionProductDefId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 150);
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
			grdLabelMapList.View.CheckValidation();

			DataTable changed = grdLabelMapList.GetChangedRows();

			if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}

		#endregion

		#region Private Function

		// TODO : 화면에서 사용할 내부 함수 추가

		#endregion
	}
}
