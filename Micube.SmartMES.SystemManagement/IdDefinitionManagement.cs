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

namespace Micube.SmartMES.SystemManagement
{
	/// <summary>
	/// 프로그램명 : 시스템관리 > 코드관리 > ID Serial
	/// 업 무 설명 :  LotID 등 ID 채번을 위한 IdClass , IdDefinition 관리하는 화면
	/// 생  성  자 :  정승원
	/// 생  성  일 :  2019-05-10
	/// 수정 이 력 : 
	/// 
	/// 
	/// </summary>
	public partial class IdDefinitionManagement : SmartConditionManualBaseForm
	{
		#region 생성자
		public IdDefinitionManagement()
		{
			InitializeComponent();
		}
		#endregion

		#region 조회조건
		/// <summary>
		/// 조회 조건 초기화
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			//ID CLASS 그룹
			InitializeCondition_IdClassPopup();
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - ID Class 그룹
		/// </summary>
		private void InitializeCondition_IdClassPopup()
		{
			var conditionIdClassId = Conditions.AddSelectPopup("P_IDCLASSID", new SqlQuery("GetIdClassList", "10001"), "IDCLASSNAME", "IDCLASSID")
				.SetPopupLayout("IDCLASSINFOLIST", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("IDCLASSNAME")
				.SetPosition(2.5)
				.SetPopupAutoFillColumns("DESCRIPTION")
				.SetPopupResultCount(0)
				.SetLabel("IDCLASSID");

			// 팝업 조회조건
			conditionIdClassId.Conditions.AddTextBox("TXTIDCLASSID");

			// 팝업 그리드
			conditionIdClassId.GridColumns.AddTextBoxColumn("IDCLASSID", 150);
			conditionIdClassId.GridColumns.AddTextBoxColumn("IDCLASSNAME", 200);
			conditionIdClassId.GridColumns.AddTextBoxColumn("DESCRIPTION", 200);
		}

		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 컨텐츠 초기화 시작
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitailizeEvent();
			InitializeGridIdDefinition();

		}

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGridIdDefinition()
		{
			# region ID CLASS GRID 초기화
			grdIdClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			grdIdClassList.View.AddTextBoxColumn("IDCLASSID", 200)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			grdIdClassList.View.AddTextBoxColumn("IDCLASSNAME", 200);
			grdIdClassList.View.AddTextBoxColumn("DESCRIPTION", 250);
			grdIdClassList.View.AddSpinEditColumn("LENGTH", 100)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
				.SetValueRange(1, decimal.MaxValue)
				.SetDefault("1");

			//유효상태, 생성자, 수정자...
			grdIdClassList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdIdClassList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdIdClassList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdIdClassList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdIdClassList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdIdClassList.View.PopulateColumns();
			#endregion


			# region ID DEFINITION GRID 초기화
			grdIdDefinitionList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			grdIdDefinitionList.View.AddTextBoxColumn("IDCLASSID", 150).SetIsHidden();
			InitailizeGridColumn_IdClassName();
			
			grdIdDefinitionList.View.AddTextBoxColumn("IDDEFID", 200)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			grdIdDefinitionList.View.AddTextBoxColumn("IDDEFNAME", 200);

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			param.Add("CODECLASSID", "IDDefType");
			grdIdDefinitionList.View.AddComboBoxColumn("IDDEFTYPE", 100, new SqlQuery("GetTypeList", "10001", param), "CODENAME", "CODEID");
			grdIdDefinitionList.View.AddSpinEditColumn("LENGTH", 100)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
				.SetValueRange(1, decimal.MaxValue)
				.SetDefault("1");

			grdIdDefinitionList.View.AddTextBoxColumn("FORMAT", 150);

			param["CODECLASSID"] = "IDDefCategory";
			grdIdDefinitionList.View.AddComboBoxColumn("IDDEFCATEGORY", 100, new SqlQuery("GetTypeList", "10001", param), "CODENAME", "CODEID");
			grdIdDefinitionList.View.AddSpinEditColumn("SEQUENCE", 100)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
				.SetValueRange(1, decimal.MaxValue)
				.SetDefault("1")
				.SetValidationIsRequired()
				.SetValidationKeyColumn();

			//유효상태, 생성자, 수정자...
			grdIdDefinitionList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdIdDefinitionList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdIdDefinitionList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdIdDefinitionList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdIdDefinitionList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdIdDefinitionList.View.PopulateColumns();
			#endregion

			#region ID Serial 그리드 초기화

			grdIdSerialList.GridButtonItem = GridButtonItem.Export;
			grdIdSerialList.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
			grdIdSerialList.View.SetIsReadOnly();

			grdIdSerialList.View.AddTextBoxColumn("IDSERIALNO", 150).SetTextAlignment(TextAlignment.Center);
			grdIdSerialList.View.AddTextBoxColumn("CREATOR", 80).SetTextAlignment(TextAlignment.Center);
			grdIdSerialList.View.AddTextBoxColumn("CREATEDTIME", 130).SetDisplayFormat("yyyy-MM-dd HH:mm:ss").SetTextAlignment(TextAlignment.Center);

			grdIdSerialList.View.PopulateColumns();

			#endregion
		}

		/// <summary>
		/// 팝업형 그리드 컬럼 초기화 - ID Class 그룹
		/// </summary>
		private void InitailizeGridColumn_IdClassName()
		{
			var conditionIdClassId = grdIdDefinitionList.View.AddSelectPopupColumn("IDCLASSNAME", 200, new SqlQuery("GetIdClassList", "10001"), "IDCLASSNAME")
				.SetPopupLayout("IDCLASSINFOLIST", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupAutoFillColumns("IDCLASSNAME")
				.SetPopupAutoFillColumns("DESCRIPTION")
				.SetPopupResultCount(1)
				.SetValidationIsRequired()
				.SetPopupApplySelection((selectRow, gridRow) => 
				{
					DataRow row = selectRow.FirstOrDefault();
					if(row == null) return;
					
					gridRow["IDCLASSID"] = row["IDCLASSID"];
				})
				.SetClearButtonInvisible();

			// 팝업 조회조건
			conditionIdClassId.Conditions.AddTextBox("TXTIDCLASSID");

			// 팝업 그리드
			conditionIdClassId.GridColumns.AddTextBoxColumn("IDCLASSID", 150);
			conditionIdClassId.GridColumns.AddTextBoxColumn("IDCLASSNAME", 200);
			conditionIdClassId.GridColumns.AddTextBoxColumn("DESCRIPTION", 200);
		}

		#endregion

		#region EVENT

		/// <summary>
		/// 이벤트 초기화
		/// </summary>
		private void InitailizeEvent()
		{
			tabIdManagement.SelectedPageChanged += TabIdManagement_SelectedPageChanged;
			grdIdDefinitionList.View.FocusedRowChanged += View_FocusedRowChanged;
		}

		/// <summary>
		/// 탭 이동 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabIdManagement_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			int index = tabIdManagement.SelectedTabPageIndex;
			switch(index)
			{
				case 0:
					Conditions.GetCondition("P_IDCLASSID").IsRequired = false;
					break;
				case 1:
					Conditions.GetCondition("P_IDCLASSID").IsRequired = true;
					break;
			}
		}

		/// <summary>
		/// Row 포커스 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(grdIdDefinitionList.View.FocusedRowHandle < 0) return;

			FocusedRowDataBind();
		}

		/// <summary>
		/// Row 포커스 변경 시 Id Serial 조회
		/// </summary>
		private void FocusedRowDataBind()
		{
			DataRow selectRow = grdIdDefinitionList.View.GetFocusedDataRow();
			if(selectRow == null) return;

			grdIdSerialList.DataSource = SqlExecuter.Query("GetIdClassSerialList", "10001", new Dictionary<string, object>() { { "IDCLASSID", selectRow["IDCLASSID"] } });
		}
		#endregion

		#region 툴바
		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			DataTable changed = new DataTable();
			switch (tabIdManagement.SelectedTabPageIndex)
			{
				case 0://ID Class
					changed = grdIdClassList.GetChangedRows();
					ExecuteRule("SaveIdClass", changed);
					break;
				case 1://ID Definition
					changed = grdIdDefinitionList.GetChangedRows();
					ExecuteRule("SaveIdDefinition", changed);
					break;
			}
		}
		#endregion

		#region 검색

		/// <summary>
		/// 비동기 override 모델
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			var values = Conditions.GetValues();

			switch (tabIdManagement.SelectedTabPageIndex)
			{
				case 0://ID Class
					DataTable dtIdClassList = await QueryAsync("SelectIdClassList", "10001", values);//ProcedureAsync("usp_com_selectidclasslist", values);
					if (dtIdClassList.Rows.Count < 1) // 
					{
						ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
					}

					grdIdClassList.DataSource = dtIdClassList;

					break;
				case 1://ID Definition
					DataTable dtIdDefinitionList = await QueryAsync("SelectIdDefList", "10001", values);//ProcedureAsync("usp_com_selectiddefinitionlist", values);
					if (dtIdDefinitionList.Rows.Count < 1) // 
					{
						ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
					}

					grdIdDefinitionList.DataSource = dtIdDefinitionList;

					FocusedRowDataBind();

					break;
			}

		}
		#endregion

		#region 유효성 검사
		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

			DataTable changed = new DataTable();
			switch (tabIdManagement.SelectedTabPageIndex)
			{
				case 0://ID Class
					grdIdClassList.View.CheckValidation();
					changed = grdIdClassList.GetChangedRows();
					break;
				case 1://ID Definition
					grdIdDefinitionList.View.CheckValidation();
					changed = grdIdDefinitionList.GetChangedRows();
					break;
			}
			if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}
		#endregion

	}
}
