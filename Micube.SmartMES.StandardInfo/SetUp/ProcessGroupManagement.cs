#region using
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System.Data;
using System.Threading.Tasks;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프로그램명 : 기준정보 > Setup > 인시당 집계그룹 관리
    /// 업 무 설명 : 인시생산성 집계용 중공정 관리
    /// 생  성  자 :  황유성
    /// 생  성  일 :  2021-03-05
    /// 수 정 이 력 : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessGroupManagement : SmartConditionManualBaseForm
	{
		#region 생성자
		public ProcessGroupManagement()
		{
			InitializeComponent();
		}
		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			grdProcessGroupList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdProcessGroupList.GridButtonItem -= GridButtonItem.Delete;
            grdProcessGroupList.View.AddTextBoxColumn("PROCESSGROUPID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			grdProcessGroupList.View.AddLanguageColumn("PROCESSGROUPNAME", 200);
			grdProcessGroupList.View.AddTextBoxColumn("DESCRIPTION", 250);
			grdProcessGroupList.View.AddComboBoxColumn("ENTERPRISEID", 150, new SqlQuery("GetEnterpriseList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "ENTERPRISENAME", "ENTERPRISEID")
				.SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
				.SetLabel("ENTERPRISE");
			grdProcessGroupList.View.AddComboBoxColumn("PLANTID", 150, new SqlQuery("GetPlantList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
				.SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
				.SetLabel("PLANT");
            grdProcessGroupList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdProcessGroupList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdProcessGroupList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdProcessGroupList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdProcessGroupList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdProcessGroupList.View.PopulateColumns();
        }

		/// <summary>
		/// 컨텐츠 영역 초기화 시작
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();
			InitializeGrid();
		}
		#endregion

		#region 툴바
		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();
			DataTable changed = grdProcessGroupList.GetChangedRows();
			ExecuteRule("ProcessGroupManagement", changed);
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
			DataTable dtPlantList = await QueryAsync("SelectProcessGroupList", "10001", values);
			if (dtPlantList.Rows.Count < 1)
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}
			grdProcessGroupList.DataSource = dtPlantList;
		}
		#endregion

		#region 유효성 검사
		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

			grdProcessGroupList.View.CheckValidation();

			DataTable changed = grdProcessGroupList.GetChangedRows();

			if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}
		#endregion
	}
}
