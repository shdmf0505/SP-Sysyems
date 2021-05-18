#region using

using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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

namespace Micube.SmartMES.StandardInfo
{
	/// <summary>
	/// 프 로 그 램 명  : 기준정보 > Setup > 카테고리 등록 조회
	/// 업  무  설  명  : 품목에 지정될 카테고리를 등록하고 관리하는 화면
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-05-28
	/// 수  정  이  력  : 
	///  
	/// 
	/// </summary>
	public partial class Category : SmartConditionManualBaseForm
	{
		#region Local Variables

		// TODO : 화면에서 사용할 내부 변수 추가

		#endregion

		#region 생성자

		public Category()
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
			grbCategory.GridButtonItem = GridButtonItem.Refresh;
			InitializeGrid();
			InitializeTree();
		}

		/// <summary>        
		/// 그리드 초기화
		/// </summary>
		private void InitializeGrid()
		{
			// TODO : 그리드 초기화 로직 추가
			grdCategoryList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

			//카테고리 ID
			grdCategoryList.View.AddTextBoxColumn("CATEGORYID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			//카테고리 명
			grdCategoryList.View.AddLanguageColumn("CATEGORYNAME", 200);
			//SET 명
			grdCategoryList.View.AddComboBoxColumn("CATEGORYSET", new SqlQuery("GetTypeList", "10001", "CODECLASSID=CategorySet", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			//LEVEL
			grdCategoryList.View.AddComboBoxColumn("LEVEL", new SqlQuery("GetTypeList", "10001", "CODECLASSID=CategoryLevel", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
			//grdCategoryList.View.AddComboBoxColumn("ENTERPRISEID", new SqlQuery("GetEnterpriseList", "10001"), "ENTERPRISENAME", "ENTERPRISEID");
			//상위 카테고리 ID
			grdCategoryList.View.AddTreeListColumn("PARENTCATEGORYID", 150, new SqlQuery("GetParentCategoryId", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={UserInfo.Current.Plant}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"), "CATEGORYNAME", "CATEGORYID", "PARENTCATEGORYID")
				.SetIsRefreshByOpen(true);
			//회사
			grdCategoryList.View.AddTextBoxColumn("ENTERPRISEID", 100)
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Enterprise);
			//회사
			grdCategoryList.View.AddTextBoxColumn("PLANTID", 100)
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Plant);
			//설명
			grdCategoryList.View.AddTextBoxColumn("DESCRIPTION", 150);

			//유효상태, 생성자, 수정자...
			grdCategoryList.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdCategoryList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdCategoryList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdCategoryList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdCategoryList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdCategoryList.View.PopulateColumns();
		}

		/// <summary>
		/// 트리 초기화
		/// </summary>
		private void InitializeTree()
		{
			treeCategory.SetResultCount(1);
			treeCategory.SetIsReadOnly();
			treeCategory.SetEmptyRoot(UserInfo.Current.Enterprise, UserInfo.Current.Enterprise);
			treeCategory.SetMember("CATEGORYNAME", "CATEGORYID", "PARENTCATEGORYID");

			string categoryId = "";
			if (treeCategory.FocusedNode != null)
			{
				categoryId = treeCategory.GetRowCellValue(treeCategory.FocusedNode, treeCategory.Columns["CATEGORYID_COPY"]).ToString();
			}

			LoadTreeData();

			treeCategory.PopulateColumns();
			treeCategory.ExpandToLevel(1);

			treeCategory.SetFocusedNode(treeCategory.FindNodeByFieldValue("CATEGORYID_COPY", categoryId));

		}

		#endregion

		#region Event

		/// <summary>        
		/// 이벤트 초기화
		/// </summary>
		public void InitializeEvent()
		{
			// TODO : 화면에서 사용할 이벤트 추가
			grbCategory.CustomButtonClick += SmartGroupBox1_CustomButtonClick;
			treeCategory.FocusedNodeChanged += TreeCategory_FocusedNodeChanged;
			grdCategoryList.View.AddingNewRow += View_AddingNewRow;

        }

		/// <summary>
		/// GroupBox 새로 고침 눌렀을 때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SmartGroupBox1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
		{
			if (e.Button.Properties.Caption == "GridRefresh")
			{
				InitializeTree();
				treeCategory.FocusedNode = treeCategory.GetNodeByVisibleIndex(0);
			}
		}

		/// <summary>
		/// 트리 리스트에서 다른 노드 선택했을 때 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeCategory_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
		{
			var values = Conditions.GetValues();
			values.Add("LanguageType", UserInfo.Current.LanguageType);
			values.Add("EnterpriseId", UserInfo.Current.Enterprise);
			values.Add("PlantId", UserInfo.Current.Plant);

			DataRow row = treeCategory.GetFocusedDataRow();
			values.Add("ParentCategoryId", row["CATEGORYID"].ToString());

			values["ParentCategoryId"] = values["EnterpriseId"].Equals(values["ParentCategoryId"]) ? "*" : row["CATEGORYID"].ToString();

			int level = Format.GetInteger(row["LEVEL"], 0);
			switch (level)
			{
				case 3:
					grdCategoryList.View.OptionsCustomization.AllowFilter = false;
					grdCategoryList.View.OptionsCustomization.AllowSort = false;
					grdCategoryList.ShowButtonBar = false;

					grdCategoryList.DataSource = null;
					break;
				default:
					grdCategoryList.View.OptionsCustomization.AllowFilter = true;
					grdCategoryList.View.OptionsCustomization.AllowSort = true;
					grdCategoryList.ShowButtonBar = true;

					grdCategoryList.DataSource = SqlExecuter.Query("SelectCategoryGrid", "10001", values);
					break;

			}
		}

		/// <summary>
		/// grid row 추가 시 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			DataRow focusRow = treeCategory.GetFocusedDataRow();
			string parentCategoryId = focusRow["CATEGORYID"].ToString();
			string categorySetId = focusRow["CATEGORYSET"].ToString();
			int level = Format.GetInteger(focusRow["LEVEL"], 0);

			//
			args.NewRow["PARENTCATEGORYID"] = parentCategoryId;
			args.NewRow["CATEGORYSET"] = categorySetId;
			args.NewRow["LEVEL"] = (level +1).ToString();
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
			DataTable changed = grdCategoryList.GetChangedRows();

			ExecuteRule("Category", changed);

			InitializeTree();
		}

		#endregion

		#region 검색

		/// <summary>
		/// 비동기 override 모델
		/// </summary>
		protected async override Task OnSearchAsync()
		{
			await base.OnSearchAsync();

			TreeListNode prevNode = treeCategory.FocusedNode;
			int prevHandle = grdCategoryList.View.FocusedRowHandle;

			grdCategoryList.View.ClearDatas();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
			values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("PlantId", UserInfo.Current.Plant);
            values.Add("EnterpriseId", UserInfo.Current.Enterprise);

            DataTable dtCategoryTree = SqlExecuter.Query("SelectCategoryTree", "10001", values);
			treeCategory.DataSource = dtCategoryTree;

			if(treeCategory.AllNodesCount == 0)
			{
				ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
			}
			else
			{
				treeCategory.PopulateColumns();
				treeCategory.ExpandToLevel(1);

				treeCategory.FocusedNode = prevNode;


				int iHandle = (prevHandle <= 0) ? 0 : prevHandle;
				grdCategoryList.View.FocusedRowHandle = iHandle;

				/*
				DataRow row = treeCategory.GetFocusedDataRow();

				if (row != null)
				{
					values.Add("PARENTCATEGORYID", row["CATEGORYID_COPY"].ToString());
				}

				DataTable dtCategoryList = await SqlExecuter.QueryAsync("SelectCategoryGrid", "10001", values);


				grdCategoryList.DataSource = dtCategoryList;
				*/


			}

			
		}

		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용
		}

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

			// TODO : 유효성 로직 변경
			grdCategoryList.View.CheckValidation();

			DataTable changed = grdCategoryList.GetChangedRows();

			if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}

		#endregion

		#region Private Function

		// TODO : 화면에서 사용할 내부 함수 추가
		/// <summary>
		/// 트리 조회
		/// </summary>
		private void LoadTreeData()
		{
			Dictionary<string, object> param = new Dictionary<string, object>();
			param.Add("PlantId", UserInfo.Current.Plant);
			param.Add("EnterpriseId", UserInfo.Current.Enterprise);
			param.Add("LanguageType", UserInfo.Current.LanguageType);

			treeCategory.DataSource = SqlExecuter.Query("SelectCategoryTree", "10001", param);
		}

		#endregion
	}
}
