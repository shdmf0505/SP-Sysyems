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
using DevExpress.XtraEditors.Repository;
#endregion

namespace Micube.SmartMES.StandardInfo
{
	/// <summary>
	/// 프로그램명 : 기준정보 > Setup > Site 정의
	/// 업 무 설명 :  회사별 공장(Site)관리 화면
	/// 생  성  자 :  정승원
	/// 생  성  일 :  2019-05-10
	/// 수정 이 력 : 
	/// 
	/// 
	/// </summary>
	public partial class PlantManagement : SmartConditionManualBaseForm
	{
		#region 생성자
		public PlantManagement()
		{
			InitializeComponent();
		}
		#endregion

		#region 컨텐츠 영역 초기화

		/// <summary>
		/// 그리드 초기화
		/// </summary>
		private void InitializeGridPlantList()
		{
			grdPlantList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdPlantList.View.AddTextBoxColumn("PLANTID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			grdPlantList.View.AddLanguageColumn("PLANTNAME", 200);
			grdPlantList.View.AddTextBoxColumn("DESCRIPTION", 250);
			grdPlantList.View.AddComboBoxColumn("ENTERPRISEID", 150, new SqlQuery("GetEnterpriseList", "10001", $"LANGUAGETYPE={Framework.UserInfo.Current.LanguageType}"), "ENTERPRISENAME", "ENTERPRISEID")
				.SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, false)
				.SetLabel("ENTERPRISE");
			grdPlantList.View.AddTextBoxColumn("ADDRESS", 300);
			grdPlantList.View.AddTextBoxColumn("PHONE", 150);
			grdPlantList.View.AddTextBoxColumn("FAXNO", 150);
            //skPark - language 컬럼 추가
            grdPlantList.View.AddComboBoxColumn("LANGUAGE", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LanguageType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME", "CODEID")
                .SetTextAlignment(TextAlignment.Center);

            //smjang - audit 여부 컬럼 추가
            grdPlantList.View.AddComboBoxColumn("ISAUDIT", 70, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            //choi  isospwarehouseoperate
            grdPlantList.View.AddTextBoxColumn("ISOSPWAREHOUSEOPERATE", 100)
                .SetDefault(false);
            //유효상태, 생성자, 수정자...
            grdPlantList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdPlantList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdPlantList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdPlantList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdPlantList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdPlantList.View.PopulateColumns();

            RepositoryItemCheckEdit repositoryCheckEdit1 = grdPlantList.View.GridControl.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
            repositoryCheckEdit1.ValueChecked = "Y";
            repositoryCheckEdit1.ValueUnchecked = "N";
            repositoryCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            grdPlantList.View.Columns["ISOSPWAREHOUSEOPERATE"].ColumnEdit = repositoryCheckEdit1;
           
        }

		/// <summary>
		/// 컨텐츠 영역 초기화 시작
		/// </summary>
		protected override void InitializeContent()
		{
			base.InitializeContent();

			InitializeGridPlantList();

		}
		#endregion

		#region 툴바
		/// <summary>
		/// 저장버튼 클릭
		/// </summary>
		protected override void OnToolbarSaveClick()
		{
			base.OnToolbarSaveClick();

			DataTable changed = grdPlantList.GetChangedRows();

			ExecuteRule("PlantManagement", changed);
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

			DataTable dtPlantList = await QueryAsync("SelectSiteList", "10001", values);//ProcedureAsync("usp_com_selectplantlist", values);

			if (dtPlantList.Rows.Count < 1) // 
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}

			grdPlantList.DataSource = dtPlantList;
		}
		#endregion

		#region 유효성 검사
		/// <summary>
		/// 데이터 저장할때 컨텐츠 영역의 유효성 검사
		/// </summary>
		protected override void OnValidateContent()
		{
			base.OnValidateContent();

			grdPlantList.View.CheckValidation();

			DataTable changed = grdPlantList.GetChangedRows();

			if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}
		#endregion

	}
}
