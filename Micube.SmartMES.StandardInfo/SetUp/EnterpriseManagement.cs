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
	/// 프로그램명 : 기준정보 > Setup > Enterprise 정의
	/// 업 무 설명 :  회사 관리 화면
	/// 생  성  자 :  정승원
	/// 생  성  일 :  2019-05-10
	/// 수정 이 력 : 
	/// 
	/// 
	/// </summary>
	public partial class EnterpriseManagement : SmartConditionManualBaseForm
	{
		#region 생성자
		public EnterpriseManagement()
		{
			InitializeComponent();
		}
		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGridEnterpriseList()
		{
			grdEnterpriseList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdEnterpriseList.View.AddTextBoxColumn("ENTERPRISEID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			grdEnterpriseList.View.AddTextBoxColumn("ENTERPRISENAME", 200)
				.SetValidationIsRequired();
			grdEnterpriseList.View.AddTextBoxColumn("DESCRIPTION", 250);
			grdEnterpriseList.View.AddTextBoxColumn("ADDRESS", 300);
			grdEnterpriseList.View.AddTextBoxColumn("PHONE", 150);
			grdEnterpriseList.View.AddTextBoxColumn("FAXNO", 150);

			//유효상태, 생성자, 수정자...
			grdEnterpriseList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdEnterpriseList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdEnterpriseList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdEnterpriseList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdEnterpriseList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdEnterpriseList.View.PopulateColumns();
		}

		/// <summary>
		/// 컨텐츠 영역 초기화 시작
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeGridEnterpriseList();

		}
		#endregion

		#region 툴바
		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			DataTable changed = grdEnterpriseList.GetChangedRows();

			ExecuteRule("EnterpriseManagement", changed);
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

			DataTable dtEnterpriseList = await QueryAsync("SelectEnterpriseList", "10001", values);//ProcedureAsync("usp_com_selectenterpriselist", values);

			if (dtEnterpriseList.Rows.Count < 1) // 
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdEnterpriseList.DataSource = dtEnterpriseList;
		}
		#endregion

		#region 유효성 검사
		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

			grdEnterpriseList.View.CheckValidation();

			DataTable changed = grdEnterpriseList.GetChangedRows();

			if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}
		#endregion

	}
}
