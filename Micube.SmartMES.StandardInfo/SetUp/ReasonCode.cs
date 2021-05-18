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
	/// 프 로 그 램 명  : 기준정보 > Setup > 사유코드 관리
	/// 업  무  설  명  : 공통으로 사용되는 사유코드를 관리한다. 사유코드 그룹(ReasonCodeClass), 
	///						사유코드(ReasonCode)를 같이 관리 함.
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-05-20
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class ReasonCode : SmartConditionManualBaseForm
	{
		#region Local Variables

		// TODO : 화면에서 사용할 내부 변수 추가

		#endregion

		#region 생성자

		public ReasonCode()
		{
			InitializeComponent();
		}

		#endregion

		#region 컨텐츠 영역 초기화
		/// <summary>
		/// 컨텐츠 영역 초기화 시작
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
			// TODO : 그리드 초기화 로직 추가

			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//TAB 0 : 사유코드  그룹 그리드 초기화
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			grdMainReasonCodeClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			grdMainReasonCodeClassList.View.AddTextBoxColumn("REASONCODECLASSID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			grdMainReasonCodeClassList.View.AddLanguageColumn("REASONCODECLASSNAME", 200);
			grdMainReasonCodeClassList.View.AddTextBoxColumn("DESCRIPTION", 150);
			grdMainReasonCodeClassList.View.AddTextBoxColumn("ENTERPRISEID", 100)
				.SetDefault("*", "*")
				.SetIsHidden();
			grdMainReasonCodeClassList.View.AddTextBoxColumn("PLANTID", 100)
				.SetDefault("*", "*")
				.SetIsHidden();
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
			param.Add("CODECLASSID", "ReasonCodeType");
			grdMainReasonCodeClassList.View.AddComboBoxColumn("REASONCODECLASSTYPE", 100, new SqlQuery("GetTypeList", "10001", param), "CODENAME", "CODEID");

			InitializeGrid_ParentReasonCodeClassListPopup();

			//유효상태, 생성자, 수정자...
			grdMainReasonCodeClassList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdMainReasonCodeClassList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMainReasonCodeClassList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMainReasonCodeClassList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMainReasonCodeClassList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdMainReasonCodeClassList.View.PopulateColumns();

			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//TAB 1 : 사유코드 그리드 초기화
			////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			//사유코드 그룹
			grdReasonCodeClassList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

			grdReasonCodeClassList.View.SetAutoFillColumn("REASONCODECLASSNAME");//사유코드 그룹명

			grdReasonCodeClassList.View.AddTextBoxColumn("REASONCODECLASSID", 150)//사유코드 그룹ID
				.SetIsReadOnly();
			grdReasonCodeClassList.View.AddTextBoxColumn("REASONCODECLASSNAME")//사유코드 그룹명
                .SetIsReadOnly();

			grdReasonCodeClassList.View.PopulateColumns();

			//사유코드 그리드 초기화
			grdReasonCodeList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;  //체크 박스

			grdReasonCodeList.View.AddTextBoxColumn("REASONCODEID", 150) //
				.SetValidationKeyColumn()
				.SetValidationIsRequired();
			grdReasonCodeList.View.AddLanguageColumn("REASONCODENAME", 200);
			grdReasonCodeList.View.AddTextBoxColumn("DESCRIPTION", 150);//설명
			grdReasonCodeList.View.AddTextBoxColumn("ENTERPRISEID", 100)//회사ID
				.SetDefault("*", "*")
				.SetIsHidden();
			grdReasonCodeList.View.AddTextBoxColumn("PLANTID", 100)//Site ID
				.SetDefault("*", "*")
				.SetIsHidden();
			grdReasonCodeList.View.AddTextBoxColumn("REASONCODECLASSID", 150)  //사유 그룹코드
				.SetIsReadOnly();
			//grdReasonCodeList.View.AddComboBoxColumn("REASONCODECLASSID", new SqlQuery("GetReasoncodeClassList", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "REASONCODECLASSNAME", "REASONCODECLASSID")
			//	.SetIsRefreshByOpen(true);
			grdReasonCodeList.View.AddSpinEditColumn("DISPLAYSEQUENCE")
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false)// 소수점 표시 안함
				.SetValueRange(1, decimal.MaxValue)
				.SetDefault("1");

			//유효상태, 생성자, 수정자...
			grdReasonCodeList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))//유효여부
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdReasonCodeList.View.AddTextBoxColumn("CREATOR", 80)//생성자
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdReasonCodeList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdReasonCodeList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdReasonCodeList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdReasonCodeList.View.PopulateColumns();
		}

		/// <summary>
		/// 상위 사유 코드 그룹 컬럼 초기화
		/// </summary>
		private void InitializeGrid_ParentReasonCodeClassListPopup()
		{
			// 팝업 컬럼 설정
			var parentEquipmentPopupColumn = grdMainReasonCodeClassList.View.AddSelectPopupColumn("PARENTREASONCODECLASSID", new SqlQuery("GetParentReasonCodeClassId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				// .SetRelationIds("PLANT")
				.SetPopupLayout("SELECTPARENTREASONCODECLASSID", PopupButtonStyles.Ok_Cancel, true, false)
				.SetPopupResultCount(1)
				.SetPopupResultMapping("PARENTREASONCODECLASSID", "REASONCODECLASSID")
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("CODECLASSNAME")
				.SetPopupValidationCustom(ValidationParentReasonCodeClassIdPopup)
			   ;

			parentEquipmentPopupColumn.Conditions.AddTextBox("TXTREASONCODECLASSIDNAME");

			parentEquipmentPopupColumn.Conditions.AddTextBox("REASONCODECLASSID")
				.SetPopupDefaultByGridColumnId("REASONCODECLASSID")
				.SetIsHidden();

			// 팝업 그리드
			parentEquipmentPopupColumn.GridColumns.AddTextBoxColumn("REASONCODECLASSID", 250)
				.SetValidationKeyColumn();
			parentEquipmentPopupColumn.GridColumns.AddTextBoxColumn("REASONCODECLASSNAME", 250);
		}

		#endregion

		#region Event

		/// <summary>        
		/// 이벤트 초기화
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
			grdReasonCodeClassList.View.RowClick += View_RowClick;
			grdReasonCodeClassList.ToolbarRefresh += GrdReasonCodeClassList_ToolbarRefresh;
			tabPartition.SelectedPageChanged += TabPartition_SelectedPageChanged;
			grdReasonCodeList.View.AddingNewRow += View_AddingNewRow;
		}

		/// <summary>
		/// 사유코드 그룹 그리드(왼쪽)에서 row 클릭 시 사유코드 리스트 조회 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
		{
			DataRow row = grdReasonCodeClassList.View.GetFocusedDataRow();

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("p_reasoncodeclassid", row["REASONCODECLASSID"].ToString());
			param.Add("p_validstate", "Valid");

			DataTable dt =SqlExecuter.Query("SelectReasoncodeList", "10001", param);//Procedure("usp_com_selectreasoncode", param);

			grdReasonCodeList.DataSource = dt;
		}

		/// <summary>
		/// grdReasonCodeClassList 리프레쉬 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GrdReasonCodeClassList_ToolbarRefresh(object sender, EventArgs e)
		{
			try
			{
				int beforeFocusCodeClass = grdReasonCodeClassList.View.FocusedRowHandle;  // 기존 포커스행을 가져오기 위한 handle.
				grdReasonCodeClassList.ShowWaitArea(); // 진행중 박스
				LoadDataGridReasonCodeClass(); // 재로드
				grdReasonCodeClassList.View.FocusedRowHandle = 0; // 첫 행 포커스
				grdReasonCodeClassList.View.UnselectRow(beforeFocusCodeClass); // 기존 포커스행 선택 해제
				grdReasonCodeClassList.View.SelectRow(0); //기존행 선택
				FocusedRowChanged(); //포커스된 class의 데이터 변경
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
			finally
			{
				grdReasonCodeClassList.CloseWaitArea();
			}
		}

		/// <summary>
		/// 탭 페이지 변경 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPartition_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			string languageKey = tabPartition.GetLanguageKey(tabPartition.SelectedTabPage);
			if (languageKey != "REASONCODE") return;

			LoadDataGridReasonCodeClass();
			FocusedRowChanged();
		}

		/// <summary>
		/// '사유코드' 그리드에서 새로운 행 추가 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			DataRow row = grdReasonCodeClassList.View.GetFocusedDataRow();
			args.NewRow["REASONCODECLASSID"] = row["REASONCODECLASSID"].ToString();

			decimal maxDisplaySequence = 0;

			//기존의 표시 순서 값들 중 "" or 공백 값이 아닌 행들만 가져옴
			var sequenceRows = args.NewRow.Table.Rows
				.Cast<DataRow>()
				.Where(r => r != args.NewRow && r.ToStringNullToEmpty("DISPLAYSEQUENCE") != "");

			// 기존의 표시 순서 값들이 있으면, 표시 순서 'Max' 처리
			if (sequenceRows.Count() > 0)
			{
				maxDisplaySequence = sequenceRows.Max(r => r.Field<int>("DISPLAYSEQUENCE"));
			}

			// 공백만 있을 경우와 새로 행 추가 될 때에는 값 1로 세팅
			args.NewRow["DISPLAYSEQUENCE"] = (maxDisplaySequence + 1).ToString();

		}

		#endregion

		#region 툴바

		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			// TODO : 저장 Rule 변경
			DataTable changed = new DataTable();
			switch(tabPartition.SelectedTabPageIndex)
			{
				case 0: //ReasoncodeClass
					changed = grdMainReasonCodeClassList.GetChangedRows();
					ExecuteRule("ReasonCodeClass", changed);
					break;
				case 1: //Reasoncode
					changed = grdReasonCodeList.GetChangedRows();
					ExecuteRule("ReasonCode", changed);
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

			// TODO : 조회 SP 변경
			var values = Conditions.GetValues();

			switch(tabPartition.SelectedTabPageIndex) 
			{
				case 0: //ReasonCode Class
					DataTable dtReasonCodeClassList = await QueryAsync("SelectReasoncodeClassList", "10001", values);//ProcedureAsync("usp_com_selectreasoncodeclass", values);
					if (dtReasonCodeClassList.Rows.Count < 1) // 
					{
						ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
					}

					grdMainReasonCodeClassList.DataSource = dtReasonCodeClassList;
					break;
				case 1: //ReasonCode
					values.Remove("P_REASONCODETYPE");

					DataTable dtReasonCodeList = await QueryAsync("SelectReasoncodeList", "10001", values);//ProcedureAsync("usp_com_selectreasoncode", values);
					if (dtReasonCodeList.Rows.Count < 1) // 
					{
						ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
					}

					grdReasonCodeList.DataSource = dtReasonCodeList;
					break;
			}

		}
		/// <summary>
		/// 조회조건 추가 구성
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용
		}

		/// <summary>
		/// 조회조건 컨트롤 기능 추가
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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
			// TODO : 유효성 로직 변경
			switch (tabPartition.SelectedTabPageIndex)
			{
				case 0: //ReasonCode Class
					grdMainReasonCodeClassList.View.CheckValidation();

					changed = grdMainReasonCodeClassList.GetChangedRows();
					if (changed.Rows.Count == 0)
					{
						// 저장할 데이터가 존재하지 않습니다.
						throw MessageException.Create("NoSaveData");
					}
					break;
				case 1: //ReasonCode

					changed = grdReasonCodeList.GetChangedRows();
					if (changed.Rows.Count == 0)
					{
						// 저장할 데이터가 존재하지 않습니다.
						throw MessageException.Create("NoSaveData");
					}
					break;
			}
		}

		#endregion

		#region Private Function

		/// <summary>
		/// Focused 된 사유코드 그룹에 해당하는 사유코드 목록 조회
		/// </summary>
		private void FocusedRowChanged()
		{
			var row = grdReasonCodeClassList.View.GetDataRow(grdReasonCodeClassList.View.FocusedRowHandle);

			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("p_reasoncodeclassid", row["REASONCODECLASSID"].ToString());
			param.Add("p_validstate", "Valid");

			if (string.IsNullOrEmpty(row["REASONCODECLASSID"].ToString()))
			{
				ShowMessage("NoSelectData");
			}
			grdReasonCodeList.DataSource = SqlExecuter.Query("SelectReasoncodeList", "10001", param);//Procedure("usp_com_selectreasoncode", param);
		}

		/// <summary>
		/// 사유코드 그룹 전체 조회(Query)
		/// </summary>
		private void LoadDataGridReasonCodeClass()
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("ReasonCodeClassType", "*");
			param.Add("LanguageType", UserInfo.Current.LanguageType);
			param.Add("ReasonCodeClassId", "*");
			DataTable dt = SqlExecuter.Query("GetReasonCodeClassList", "10001", param);

			if (dt.Rows.Count < 1) // 
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdReasonCodeClassList.DataSource = dt;
		}

		// TODO : 화면에서 사용할 내부 함수 추가
		/// <summary>
		/// 팝업에서 선택할때 유효성 검사루틴
		/// </summary>
		/// <param name="currentGridRow">그리드의 현재 ROW</param>
		/// <param name="popupSelections">팝업에서 선택한 ROW</param>
		/// <returns></returns>
		private Micube.Framework.SmartControls.Validations.ValidationResultCommon ValidationParentReasonCodeClassIdPopup(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
		{
			Micube.Framework.SmartControls.Validations.ValidationResultCommon result = new Framework.SmartControls.Validations.ValidationResultCommon();

			//DataRow.ToStringNullToEmpty("FieldName") : null, DBNull.Value 일 경우 빈문자열을 리턴해주는 함수입니다.

			string myReasonCodeClassId = currentGridRow.ToStringNullToEmpty("REASONCODECLASSID");
			if (popupSelections.Any(s => s.ToStringNullToEmpty("REASONCODECLASSID") == myReasonCodeClassId))
			{
				//result.IsSucced = false;
				//result.FailMessage = $"상위ReasonCodeClassID[{myReasonCodeClassId}]는 본인 ReasonCodeClassID와 같을 수 없습니다"; // 
				//result.Caption = "오류";
				ShowMessage("NoSameParentCodeClass");
			}

			return result;
		}

		#endregion
	}
}
