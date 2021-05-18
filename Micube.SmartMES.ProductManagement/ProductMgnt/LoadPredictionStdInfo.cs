#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Validations;
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
using Micube.SmartMES.Commons;

#endregion

namespace Micube.SmartMES.ProductManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 생산관리 > 공정부하 > 공정부하 기준정보
	/// 업  무  설  명  : 공정부하그룹 관리 및 매핑
	/// 생    성    자  : 정승원
	/// 생    성    일  : 2019-08-07
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LoadPredictionStdInfo : SmartConditionManualBaseForm
	{
		#region Local Variables

		private readonly string _TOPLOADSEGMENTTYPE = "TopLoadSegmentClass";
		private readonly string _MIDDLELOADSEGMENTTYPE = "MiddleLoadSegmentClass";
		private readonly string _SAMLLLOADSEGMENTTYPE = "SmallLoadSegmentClass";

		#endregion

		#region 생성자

		public LoadPredictionStdInfo()
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

			InitializeGridLoadStandardList();

			(grdLoadStandardList.View.Columns["LOADTOPSEGMENTCLASSNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += LoadPredictionStdInfo_ButtonClick;
			(grdLoadStandardList.View.Columns["LOADMIDDLESEGMENTCLASSNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += LoadPredictionStdInfo_ButtonClick1;
			(grdLoadStandardList.View.Columns["LOADSMALLSEGMENTCLASSNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += LoadPredictionStdInfo_ButtonClick2;
		}

		/// <summary>
		/// 부하량 기준정보 그리드 초기화
		/// </summary>
		private void InitializeGridLoadStandardList()
		{
			grdLoadStandardList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdLoadStandardList.GridButtonItem = GridButtonItem.Export;

			//품목코드
			grdLoadStandardList.View.AddTextBoxColumn("PRODUCTDEFID", 120)
				.SetIsReadOnly();
            grdLoadStandardList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 60)
                .SetIsReadOnly().SetLabel("PRODUCTREVISION");
            //품목명
            grdLoadStandardList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250)
				.SetIsReadOnly();
			//공정순번
			grdLoadStandardList.View.AddTextBoxColumn("USERSEQUENCE", 60)
				.SetIsReadOnly();
			//표준공정코드
			grdLoadStandardList.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
				.SetIsReadOnly();
			//공정명
			grdLoadStandardList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
				.SetIsReadOnly();
            //Site ID
            grdLoadStandardList.View.AddTextBoxColumn("PROCESSPATHPLANTID", 70)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center)
                .SetLabel("PLANTID");
            //담당자
            //InitializeGrid_OwnerIdListPopup(grdLoadStandardList);
            grdLoadStandardList.View.AddTextBoxColumn("OWNER")
				.SetIsReadOnly();
			//대공정그룹
			InitializeGrid_LoadTopSegmentClassIdListPopup(grdLoadStandardList);
			grdLoadStandardList.View.AddTextBoxColumn("LOADTOPSEGMENTCLASSID")
				.SetIsHidden();
			grdLoadStandardList.View.AddTextBoxColumn("LOADTOPSEGMENTCLASSNAME_COPY")
				.SetIsHidden();

			//중공정그룹
			InitializeGrid_LoadMiddleSegmentClassIdListPopup(grdLoadStandardList);
			grdLoadStandardList.View.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSID")
				.SetIsHidden();
			grdLoadStandardList.View.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSNAME_COPY")
				.SetIsHidden();

			//소공정그룹
			InitializeGrid_LoadSmallSegmentClassIdListPopup(grdLoadStandardList);	
			grdLoadStandardList.View.AddTextBoxColumn("LOADSMALLSEGMENTCLASSID")
				.SetIsHidden();
			grdLoadStandardList.View.AddTextBoxColumn("LOADSMALLSEGMENTCLASSNAME_COPY")
				.SetIsHidden();

			//외주비율
			grdLoadStandardList.View.AddSpinEditColumn("OUTSOUCINGRATIO", 70)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
				.SetIsHidden();
			//목표수량
			grdLoadStandardList.View.AddSpinEditColumn("TARGETPANELQTY", 70)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//목표공정개수
			grdLoadStandardList.View.AddSpinEditColumn("TARGETSEGMENTQTY", 70)
				.SetDisplayFormat("#,##0", MaskTypes.Numeric, false);
			//설명
			grdLoadStandardList.View.AddTextBoxColumn("DESCRIPTION", 200);
			//회사
			grdLoadStandardList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Enterprise);
			//SITE
			grdLoadStandardList.View.AddTextBoxColumn("PLANTID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Plant);


			//유효상태, 생성자, 수정자...
			grdLoadStandardList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdLoadStandardList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdLoadStandardList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdLoadStandardList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdLoadStandardList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdLoadStandardList.View.PopulateColumns();


            if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                grdLoadStandardList.View.FixColumn(new string[] { "PRODUCTDEFID", "PRODUCTDEFVERSION", "PRODUCTDEFNAME", "USERSEQUENCE", "PROCESSSEGMENTID", "PROCESSSEGMENTNAME", "PROCESSPATHPLANTID" });
        }

		/// <summary>
		/// 표준공정 맵핑 그리드 초기화
		/// </summary>
		private void InitializeGridStandardSegmentMappingList()
		{
			grdStandardSegmentMapList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdStandardSegmentMapList.GridButtonItem = GridButtonItem.Export;

			//표준공정코드
			grdStandardSegmentMapList.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
				.SetIsReadOnly();
			//표준공정명
			grdStandardSegmentMapList.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 250)
				.SetIsReadOnly();
			//대공정그룹코드
			InitializeGrid_LoadTopSegmentClassIdListPopup(grdStandardSegmentMapList);
			grdStandardSegmentMapList.View.AddTextBoxColumn("LOADTOPSEGMENTCLASSID")
				.SetIsReadOnly()
				.SetIsHidden();
			//중공정그룹코드
			InitializeGrid_LoadMiddleSegmentClassIdListPopup(grdStandardSegmentMapList);
			grdStandardSegmentMapList.View.AddTextBoxColumn("LOADMIDDLESEGMENTCLASSID")
				.SetIsReadOnly()
				.SetIsHidden();
			//소공정그룹코드
			InitializeGrid_LoadSmallSegmentClassIdListPopup(grdStandardSegmentMapList);
			grdStandardSegmentMapList.View.AddTextBoxColumn("LOADSMALLSEGMENTCLASSID")
				.SetIsReadOnly()
				.SetIsHidden();
			//설명
			grdStandardSegmentMapList.View.AddTextBoxColumn("DESCRIPTION", 150);
			//회사
			grdStandardSegmentMapList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Enterprise);
			//SITE
			grdStandardSegmentMapList.View.AddTextBoxColumn("PLANTID")
				.SetIsHidden()
				.SetDefault(UserInfo.Current.Plant);


			//유효상태, 생성자, 수정자...
			grdStandardSegmentMapList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdStandardSegmentMapList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdStandardSegmentMapList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdStandardSegmentMapList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdStandardSegmentMapList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdStandardSegmentMapList.View.PopulateColumns();
		}

		/// <summary>
		/// 대공정그룹 그리드 초기화
		/// </summary>
		private void InitializeGridTopSegmentGroupList()
		{
			grdTopSegmentGroupList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdTopSegmentGroupList.GridButtonItem = GridButtonItem.Export;
            //공정부하 공정그룹ID
            grdTopSegmentGroupList.View.AddTextBoxColumn("LOADSEGMENTCLASSID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			//공정부하 공정그룹명
			grdTopSegmentGroupList.View.AddLanguageColumn("LOADSEGMENTCLASSNAME", 150);
			//공정부하 공정그룹유형
			grdTopSegmentGroupList.View.AddComboBoxColumn("LOADSEGMENTCLASSTYPE", 150, new SqlQuery("GetTypeList", "10001", "CODECLASSID=LoadSegmentClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetIsReadOnly();
			//상위 공정부하 공정그룹ID
			//InitializeParentSegmentClass(grdTopSegmentGroupList, "TXTPARENTTOPSEGMENT", "TOPLOADSEGMENT");
			//설명
			grdTopSegmentGroupList.View.AddTextBoxColumn("DESCRIPTION", 200);
			//회사
			grdTopSegmentGroupList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Enterprise)
				.SetIsHidden();
			//SITE ID
			grdTopSegmentGroupList.View.AddTextBoxColumn("PLANTID")
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Plant)
				.SetIsHidden();

			//유효상태, 생성자, 수정자...
			grdTopSegmentGroupList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdTopSegmentGroupList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdTopSegmentGroupList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdTopSegmentGroupList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdTopSegmentGroupList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdTopSegmentGroupList.View.PopulateColumns();
		}

		/// <summary>
		/// 중공정그룹 그리드 초기화
		/// </summary>
		private void InitializeGridMiddleSegmentGroupList()
		{
			grdMiddleSegmentGroupList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMiddleSegmentGroupList.GridButtonItem = GridButtonItem.Export;
            //공정부하 공정그룹ID
            grdMiddleSegmentGroupList.View.AddTextBoxColumn("LOADSEGMENTCLASSID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			//공정부하 공정그룹명
			grdMiddleSegmentGroupList.View.AddLanguageColumn("LOADSEGMENTCLASSNAME", 150);
			//공정부하 공정그룹유형
			grdMiddleSegmentGroupList.View.AddComboBoxColumn("LOADSEGMENTCLASSTYPE", 150, new SqlQuery("GetTypeList", "10001", "CODECLASSID=LoadSegmentClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetIsReadOnly();
			//상위 공정부하 공정그룹ID
			//InitializeParentSegmentClass(grdMiddleSegmentGroupList, "TXTPARENTMIDDLESEGMENT", "MIDDLELOADSEGMENT");
			//설명
			grdMiddleSegmentGroupList.View.AddTextBoxColumn("DESCRIPTION", 200);
			//회사 ID
			grdMiddleSegmentGroupList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Enterprise)
				.SetIsHidden();
			//SITE ID
			grdMiddleSegmentGroupList.View.AddTextBoxColumn("PLANTID")
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Plant)
				.SetIsHidden();

			//유효상태, 생성자, 수정자...
			grdMiddleSegmentGroupList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdMiddleSegmentGroupList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMiddleSegmentGroupList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMiddleSegmentGroupList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdMiddleSegmentGroupList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdMiddleSegmentGroupList.View.PopulateColumns();
		}

		/// <summary>
		/// 소공정그룹 그리드 초기화
		/// </summary>
		private void InitializeGridSmallSegmentGroupList()
		{
			grdSmallSegmentGroupList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdSmallSegmentGroupList.GridButtonItem = GridButtonItem.Export;
            //공정부하 공정그룹ID
            grdSmallSegmentGroupList.View.AddTextBoxColumn("LOADSEGMENTCLASSID", 150)
				.SetValidationIsRequired()
				.SetValidationKeyColumn();
			//공정부하 공정그룹명
			grdSmallSegmentGroupList.View.AddLanguageColumn("LOADSEGMENTCLASSNAME", 150);
			//공정부하 공정그룹유형
			grdSmallSegmentGroupList.View.AddComboBoxColumn("LOADSEGMENTCLASSTYPE", 150, new SqlQuery("GetTypeList", "10001", "CODECLASSID=LoadSegmentClassType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetIsReadOnly();
			//상위 공정부하 공정그룹ID
			//InitializeParentSegmentClass(grdSmallSegmentGroupList, "TXTPARENTSEGMENT", "SMALLLOADSEGMENT");
			//설명
			grdSmallSegmentGroupList.View.AddTextBoxColumn("DESCRIPTION", 200);
			//회사
			grdSmallSegmentGroupList.View.AddTextBoxColumn("ENTERPRISEID")
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Enterprise)
				.SetIsHidden();
			//SITE ID
			grdSmallSegmentGroupList.View.AddTextBoxColumn("PLANTID")
				.SetIsReadOnly()
				.SetDefault(UserInfo.Current.Plant)
				.SetIsHidden(); 

			//유효상태, 생성자, 수정자...
			grdSmallSegmentGroupList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetDefault("Valid")
				.SetValidationIsRequired()
				.SetTextAlignment(TextAlignment.Center);
			grdSmallSegmentGroupList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdSmallSegmentGroupList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdSmallSegmentGroupList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdSmallSegmentGroupList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdSmallSegmentGroupList.View.PopulateColumns();
		}

		/// <summary>
		/// 담당자 맵핑
		/// </summary>
		private void InitializeGridOwnerMappingList()
		{
			grdOwnerMappingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
			grdOwnerMappingList.GridButtonItem = GridButtonItem.Export;

			//품목코드
			grdOwnerMappingList.View.AddTextBoxColumn("PRODUCTDEFID", 150)
				.SetIsReadOnly();
			//품목버전
			grdOwnerMappingList.View.AddTextBoxColumn("PRODUCTDEFVERSION", 50)
				.SetLabel("ITEMVERSION")
				.SetIsReadOnly();
			//품목명
			grdOwnerMappingList.View.AddTextBoxColumn("PRODUCTDEFNAME", 250)
				.SetIsReadOnly();
            //제품/반제품 구분
            grdOwnerMappingList.View.AddTextBoxColumn("PRODUCTDEFTYPE", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            //담당자
            InitializeGrid_OwnerIdListPopup(grdOwnerMappingList);
            //품목 사용여부
            grdOwnerMappingList.View.AddComboBoxColumn("ISLOADMNG", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Y")
                .SetTextAlignment(TextAlignment.Center);
            //품목별 진행현황 연계 Y/N (2021-01-19 오근영 22)
            grdOwnerMappingList.View.AddComboBoxColumn("ISPROGRESSPERPRODUCT", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Y")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsHidden();
            //담당자ID
            grdOwnerMappingList.View.AddTextBoxColumn("OWNER")
				.SetIsHidden();

			grdOwnerMappingList.View.AddSpinEditColumn("PNLLOT", 80);

			//생성자, 수정자...
			grdOwnerMappingList.View.AddTextBoxColumn("CREATOR", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdOwnerMappingList.View.AddTextBoxColumn("CREATEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdOwnerMappingList.View.AddTextBoxColumn("MODIFIER", 80)
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);
			grdOwnerMappingList.View.AddTextBoxColumn("MODIFIEDTIME", 130)
				.SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
				.SetIsReadOnly()
				.SetTextAlignment(TextAlignment.Center);

			grdOwnerMappingList.View.PopulateColumns();
		}

		/// <summary>
		/// 상위 Parent 팝업 컬럼 초기화
		/// </summary>
		private void InitializeParentSegmentClass(SmartBandedGrid grid, string conditionString, string type)
		{
			var parentSegmentColumn = grid.View.AddSelectPopupColumn("PARENTLOADSEGMENTCLASSID", 100, new SqlQuery("GetParentLoadSegmentList", "10001", $"LOADSEGMENTTYPE={type}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTPARENTLOADSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME");

			parentSegmentColumn.Conditions.AddTextBox(conditionString);

			parentSegmentColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSID", 150)
				.SetValidationKeyColumn()
				.SetIsReadOnly();
			parentSegmentColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150)
				.SetIsReadOnly();
		}

		/// <summary>
		/// 팝업형 컬럼 초기화 - 대공정 그룹
		/// </summary>
		private void InitializeGrid_LoadTopSegmentClassIdListPopup(SmartBandedGrid grid)
		{
			var loadTopSegmentClassIdColumn = grid.View.AddSelectPopupColumn("LOADTOPSEGMENTCLASSNAME", 150, new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={_TOPLOADSEGMENTTYPE}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTLOADTOPSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPopupResultMapping("LOADTOPSEGMENTCLASSNAME", "LOADSEGMENTCLASSNAME")
				.SetPopupApplySelection((selectedRows, dataGridRows) =>
				{
					List<string> selectList = selectedRows.AsEnumerable().Select(r => r.Field<string>("LOADSEGMENTCLASSID")).Distinct().ToList();
					if (selectList.Count > 0)
					{
						string loadTopSegmentClassId = selectList[0].ToString();
						dataGridRows["LOADTOPSEGMENTCLASSID"] = loadTopSegmentClassId;
					}
					else if(selectList.Count == 0 && grid.Name.Equals("grdStandardSegmentMapList"))
					{
						dataGridRows["LOADTOPSEGMENTCLASSID"] = "";
					}
				})
				.SetPopupValidationCustom(ValidationNullorEmpty_LoadTopSegClass);

			loadTopSegmentClassIdColumn.Conditions.AddTextBox("TXTLOADTOPSEGMENTCLASS");

			loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSID", 150)
				.SetValidationKeyColumn()
				.SetIsReadOnly();
			loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150)
				.SetIsReadOnly();
		}

		/// <summary>
		/// 팝업형 컬럼 초기화 - 중공정 그룹
		/// </summary>
		private void InitializeGrid_LoadMiddleSegmentClassIdListPopup(SmartBandedGrid grid)
		{
			var loadMiddleSegmentClassIdColumn = grid.View.AddSelectPopupColumn("LOADMIDDLESEGMENTCLASSNAME", 150, new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={_MIDDLELOADSEGMENTTYPE}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTLOADMIDDLESEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPopupResultMapping("LOADMIDDLESEGMENTCLASSNAME", "LOADSEGMENTCLASSNAME")
				.SetPopupApplySelection((selectedRows, dataGridRows) =>
				{
					List<string> selectList = selectedRows.AsEnumerable().Select(r => r.Field<string>("LOADSEGMENTCLASSID")).Distinct().ToList();
					if (selectList.Count > 0)
					{
						string loadMiddleSegmentClassId = selectList[0].ToString();
						dataGridRows["LOADMIDDLESEGMENTCLASSID"] = loadMiddleSegmentClassId;
					}
					else if (selectList.Count == 0 && grid.Name.Equals("grdStandardSegmentMapList"))
					{
						dataGridRows["LOADMIDDLESEGMENTCLASSID"] = "";
					}
				})
				.SetPopupValidationCustom(ValidationNullorEmpty_LoadMiddleSegClass);

			loadMiddleSegmentClassIdColumn.Conditions.AddTextBox("TXTLOADMIDDLESEGMENTCLASS");

			loadMiddleSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSID", 150)
				.SetValidationKeyColumn()
				.SetIsReadOnly();
			loadMiddleSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150)
				.SetIsReadOnly();
		}

		/// <summary>
		/// 팝업형 컬럼 초기화 - 소공정 그룹
		/// </summary>
		private void InitializeGrid_LoadSmallSegmentClassIdListPopup(SmartBandedGrid grid)
		{
			var loadSmallSegmentClassIdColumn = grid.View.AddSelectPopupColumn("LOADSMALLSEGMENTCLASSNAME", 150, new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={_SAMLLLOADSEGMENTTYPE}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTLOADSMALLSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPopupResultMapping("LOADSMALLSEGMENTCLASSNAME", "LOADSEGMENTCLASSNAME")
				.SetPopupApplySelection((selectedRows, dataGridRows) =>
				{
					List<string> selectList = selectedRows.AsEnumerable().Select(r => r.Field<string>("LOADSEGMENTCLASSID")).Distinct().ToList();
					if (selectList.Count > 0)
					{
						string loadSmallSegmentClassId = selectList[0].ToString();
						dataGridRows["LOADSMALLSEGMENTCLASSID"] = loadSmallSegmentClassId;
					}
					else if (selectList.Count == 0 && grid.Name.Equals("grdStandardSegmentMapList"))
					{
						dataGridRows["LOADSMALLSEGMENTCLASSID"] = "";
					}
				})
				.SetPopupValidationCustom(ValidationNullorEmpty_LoadSmallSegClass);

			loadSmallSegmentClassIdColumn.Conditions.AddTextBox("TXTLOADSMALLSEGMENTCLASS");

			loadSmallSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSID", 150)
				.SetValidationKeyColumn()
				.SetIsReadOnly();
			loadSmallSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150)
				.SetIsReadOnly();
		}

		/// <summary>
		/// 팝업형 컬럼 초기화 - 담당자
		/// </summary>
		private void InitializeGrid_OwnerIdListPopup(SmartBandedGrid grid)
		{
            //int ii = 0;
            //string id = string.Empty;

            var ownerIdColumn = grid.View.AddSelectPopupColumn("OWNERNAME", 100, new SqlQuery("GetUserList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTOWNERID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(1)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("USERNAME")
				.SetPopupResultMapping("OWNERNAME", "USERNAME")
                .SetPopupApplySelection((selectedRows, dataGridRows) =>
                {
                    List<string> selectList = selectedRows.AsEnumerable().Select(r => r.Field<string>("USERID")).Distinct().ToList();
                    if (selectList.Count > 0)
                    {
                        string id = selectList[0].ToString();
                        dataGridRows["OWNER"] = id;

                        //if (ii == 0)
                        //{
                        //    id = selectList[0].ToString();
                        //}
                        //else
                        //{
                        //    id += ","+selectList[ii].ToString();
                        //}
                        //dataGridRows["OWNER"] = id;
                        //ii++;

                    }
                    else if (selectList.Count == 0)
                    {
                        dataGridRows["OWNER"] = "";
                    }
                });

			ownerIdColumn.Conditions.AddTextBox("USERIDNAME");

			ownerIdColumn.GridColumns.AddTextBoxColumn("USERID", 150)
				.SetIsReadOnly();
			ownerIdColumn.GridColumns.AddTextBoxColumn("USERNAME", 150)
				.SetIsReadOnly();
		}
		#endregion

		#region Event

		/// <summary>
		/// 화면에서 사용되는 이벤트를 초기화한다.
		/// </summary>
		public void InitializeEvent()
		{
			grdSmallSegmentGroupList.View.AddingNewRow += View_AddingNewRow2;
			grdMiddleSegmentGroupList.View.AddingNewRow += View_AddingNewRow1;
			grdTopSegmentGroupList.View.AddingNewRow += View_AddingNewRow;
			grdLoadStandardList.View.ShowingEditor += View_ShowingEditor;
			tabPartion.SelectedPageChanged += TabPartion_SelectedPageChanged;

		}

		/// <summary>
		/// 소공정 그룹 팝업 컬럼 x버튼 누를 때 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadPredictionStdInfo_ButtonClick2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				DataRow row = grdLoadStandardList.View.GetFocusedDataRow();
				grdLoadStandardList.View.SetFocusedRowCellValue("LOADSMALLSEGMENTCLASSNAME", row["LOADSMALLSEGMENTCLASSNAME_COPY"]);
				(grdLoadStandardList.DataSource as DataTable).AcceptChanges();
			}
		}

		/// <summary>
		/// 중공정 그룹 팝업 컬럼 x버튼 누를 때 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadPredictionStdInfo_ButtonClick1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				DataRow row = grdLoadStandardList.View.GetFocusedDataRow();
				grdLoadStandardList.View.SetFocusedRowCellValue("LOADMIDDLESEGMENTCLASSNAME", row["LOADMIDDLESEGMENTCLASSNAME_COPY"]);
				(grdLoadStandardList.DataSource as DataTable).AcceptChanges();
			}
		}

		/// <summary>
		/// 대공정 그룹 팝업 컬럼 x버튼 누를 때 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadPredictionStdInfo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Kind.Equals(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear))
			{
				DataRow row = grdLoadStandardList.View.GetFocusedDataRow();
				grdLoadStandardList.View.SetFocusedRowCellValue("LOADTOPSEGMENTCLASSNAME", row["LOADTOPSEGMENTCLASSNAME_COPY"]);
				(grdLoadStandardList.DataSource as DataTable).AcceptChanges();
			}
		}

		/// <summary>
		/// 소공정 ROW 추가 시 공정 타입 SET
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow2(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			SetLoadSegmentClassType(args);
		}

		/// <summary>
		/// 중공정 ROW 추가 시 공정 타입 SET
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow1(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			SetLoadSegmentClassType(args);
		}

		/// <summary>
		/// 대공정 ROW 추가 시 공정 타입 SET
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			SetLoadSegmentClassType(args);
		}

		/// <summary>
		/// 셀 READONLY
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void View_ShowingEditor(object sender, CancelEventArgs e)
		{
			DataRow row = grdLoadStandardList.View.GetFocusedDataRow();
			GridView view = sender as GridView;

			if (view.FocusedColumn.FieldName.Equals("LOADTOPSEGMENTCLASSNAME") && string.IsNullOrEmpty(Format.GetString(view.FocusedValue, "")) 
				|| view.FocusedColumn.FieldName.Equals("LOADMIDDLESEGMENTCLASSNAME") && string.IsNullOrEmpty(Format.GetString(view.FocusedValue, ""))
				|| view.FocusedColumn.FieldName.Equals("LOADSMALLSEGMENTCLASSNAME") && string.IsNullOrEmpty(Format.GetString(view.FocusedValue, "")))
			{
				e.Cancel = true;
			}
		}

		/// <summary>
		/// 탭 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabPartion_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			SmartBandedGrid grid = e.Page.Controls[0] as SmartBandedGrid;
			string pageName= e.Page.Name;


			switch (pageName)
			{
				case "loadStandardPage"://부하량 기준정보
					if (!grid.View.IsInitializeColumns)
					{
						InitializeGridLoadStandardList();

						(grdLoadStandardList.View.Columns["LOADTOPSEGMENTCLASSNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += LoadPredictionStdInfo_ButtonClick;
						(grdLoadStandardList.View.Columns["LOADMIDDLESEGMENTCLASSNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += LoadPredictionStdInfo_ButtonClick1;
						(grdLoadStandardList.View.Columns["LOADSMALLSEGMENTCLASSNAME"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).ButtonClick += LoadPredictionStdInfo_ButtonClick2;
					} 

					SetConditionVisiblility("P_LOADTOPSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADMIDDLESEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADSMALLSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_NOINPUT", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_VALIDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("USERID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_ISLOADMNG", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

					break;
				case "standardSegmentMappingPage"://표준공정 맵핑
					if (!grid.View.IsInitializeColumns) InitializeGridStandardSegmentMappingList();

					SetConditionVisiblility("P_LOADTOPSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADMIDDLESEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADSMALLSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_NOINPUT", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_VALIDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("USERID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_ISLOADMNG", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

					break;
				case "topSegmentGroupPage"://대공정 그룹
					if (!grid.View.IsInitializeColumns) InitializeGridTopSegmentGroupList();

					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOADTOPSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOADMIDDLESEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADSMALLSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_NOINPUT", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_VALIDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("USERID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_ISLOADMNG", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

					break;
				case "middleSegmentGroupPage"://중공정 그룹
					if (!grid.View.IsInitializeColumns) InitializeGridMiddleSegmentGroupList();

					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOADTOPSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADMIDDLESEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOADSMALLSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_NOINPUT", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_VALIDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("USERID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_ISLOADMNG", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

					break;
				case "smallSegmentGroupPage"://소공정 그룹
					if (!grid.View.IsInitializeColumns) InitializeGridSmallSegmentGroupList();

					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOADTOPSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADMIDDLESEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADSMALLSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_NOINPUT", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_VALIDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("USERID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_ISLOADMNG", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

					break;
				case "ownerMappingPage"://담당자
					if (!grid.View.IsInitializeColumns) InitializeGridOwnerMappingList();

					SetConditionVisiblility("P_PLANTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTDEFID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTIONTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_NOINPUT", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_LOADTOPSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADMIDDLESEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_LOADSMALLSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_PROCESSSEGMENTID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("P_VALIDSTATE", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
					SetConditionVisiblility("USERID", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_PRODUCTDEFTYPE", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);
					SetConditionVisiblility("P_ISLOADMNG", DevExpress.XtraLayout.Utils.LayoutVisibility.Always);

					break;
			}//switch

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
			DataTable dt = null;
			int index = tabPartion.SelectedTabPageIndex;
			string tableName = "";
			string ruleName = "";

			switch(index)
			{
				case 0://부하량 기준정보
					dt = grdLoadStandardList.GetChangedRows();

					ruleName = "SaveLoadSegmentRel";
					tableName = "loadProductSegment";
					dt.TableName = tableName;

					break;
				case 1://표준공정 맵핑
					dt = grdStandardSegmentMapList.GetChangedRows();

					ruleName = "SaveLoadSegmentRel";
					tableName = "loadSegmentRel";
					dt.TableName = tableName;

					break;
				case 2://대공정그룹
					dt = grdTopSegmentGroupList.GetChangedRows();

					ruleName = "SaveLoadSegmentClass";
					tableName = "topLoadSegmentClass";
					dt.TableName = tableName;

					break;
				case 3://중공정그룹
					dt = grdMiddleSegmentGroupList.GetChangedRows();

					ruleName = "SaveLoadSegmentClass";
					tableName = "middleLoadSegmentClass";
					dt.TableName = tableName;

					break;
				case 4://소공정그룹
					dt = grdSmallSegmentGroupList.GetChangedRows();

					ruleName = "SaveLoadSegmentClass";
					tableName = "smallLoadSegmentClass";
					dt.TableName = tableName;

					break;
				case 5://담당자
					dt = grdOwnerMappingList.GetChangedRows();

					//List<DataRow> savedList = dt.AsEnumerable().Where(r => string.IsNullOrEmpty(r.Field<string>("OWNERNAME")) || string.IsNullOrEmpty(r.Field<string>("OWNER"))).ToList();
					//if (savedList.Count > 0)
					//{
					//	for(int i = 0; i<savedList.Count; i++) {
					//		dt.Rows.Remove(savedList[i]);
					//	}
					//}

					ruleName = "SaveLoadSegmentRel";
					tableName = "ownerMappingToProductDef";
					dt.TableName = tableName;

					break;
			}

			MessageWorker worker = new MessageWorker(ruleName);
			worker.SetBody(new MessageBody()
			{
				{ "enterpriseId", UserInfo.Current.Enterprise },
				{ "plantId", UserInfo.Current.Plant },
				{ tableName, dt }

			});
			worker.Execute();
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

			DataTable dt = null;

			int index = tabPartion.SelectedTabPageIndex;
			switch(index)
			{
				case 0://부하량 기준정보
					values.Remove("P_LOADTOPSEGMENTCLASSID");
					values.Remove("P_LOADMIDDLESEGMENTCLASSID");
					values.Remove("P_LOADSMALLSEGMENTCLASSID");

					dt = await QueryAsync("SelectProductSegmentMappingList", "10001", values);

					if (dt.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}
					grdLoadStandardList.DataSource = dt;
					break;
				case 1://표준공정 맵핑
					values.Remove("P_LOADTOPSEGMENTCLASSID");
					values.Remove("P_LOADMIDDLESEGMENTCLASSID");
					values.Remove("P_LOADSMALLSEGMENTCLASSID");
					values.Remove("P_PLANTID");
					values.Remove("P_PRODUCTIONTYPE");
					values.Remove("P_PRODUCTDEFID");
					values.Remove("P_PRODUCTDEFTYPE"); 

					dt = await QueryAsync("SelectLoadSegmentRelList", "10001", values);
					if (dt.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}
					grdStandardSegmentMapList.DataSource = dt;
					break;
				case 2://대공정그룹
					values.Remove("P_PRODUCTDEFID");
					values.Remove("P_LOADMIDDLESEGMENTCLASSID");
					values.Remove("P_LOADSMALLSEGMENTCLASSID");
					values.Remove("P_PROCESSDEFID");
					values.Remove("P_NOINPUT");
					values.Remove("P_PRODUCTIONTYPE");
					values.Remove("P_PROCESSSEGMENTID");
					values.Remove("P_PRODUCTDEFTYPE");

					values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
					values.Add("TYPE", "TOP");

					dt = await QueryAsync("SelectLoadSegmentList", "10001", values);
					if(dt.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData"); 
					}
					grdTopSegmentGroupList.DataSource = dt;
					break;
				case 3://중공정그룹
					values.Remove("P_PRODUCTDEFID");
					values.Remove("P_LOADTOPSEGMENTCLASSID");
					values.Remove("P_LOADSMALLSEGMENTCLASSID");
					values.Remove("P_PROCESSDEFID");
					values.Remove("P_PRODUCTDEFTYPE");

					values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
					values.Add("TYPE", "MIDDLE");

					dt = await QueryAsync("SelectLoadSegmentList", "10001", values);
					if (dt.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}
					grdMiddleSegmentGroupList.DataSource = dt;
					break;
				case 4://소공정그룹
					values.Remove("P_PRODUCTDEFID");
					values.Remove("P_LOADTOPSEGMENTCLASSID");
					values.Remove("P_LOADMIDDLESEGMENTCLASSID");
					values.Remove("P_PROCESSDEFID");
					values.Remove("P_PRODUCTDEFTYPE");

					values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
					values.Add("TYPE", "SMALL");

					dt = await QueryAsync("SelectLoadSegmentList", "10001", values);
					if (dt.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}
					grdSmallSegmentGroupList.DataSource = dt;
					break;
				case 5://담당자
					values.Remove("P_PROCESSSEGMENTID");
					values.Remove("P_LOADTOPSEGMENTCLASSID");
					values.Remove("P_LOADMIDDLESEGMENTCLASSID");
					values.Remove("P_LOADSMALLSEGMENTCLASSID");

                    //2021-01-19 오근영 (22) 컬럼추가 (ISPROGRESSPERPRODUCT,	품목별 진행현황 연계 Y/N)
                    //dt = await QueryAsync("SelectProductDefListForOwnerMapping", "10001", values);
                    dt = await QueryAsync("SelectProductDefListForOwnerMapping3", "10003", values);
                    if (dt.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}
					grdOwnerMappingList.DataSource = dt;
					break;
			}//switch

		}

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

			// TODO : 조회조건 추가 구성이 필요한 경우 사용

			//표준공정
			InitializeConditionProcessSegId_Popup();
			//대공정그룹
			InitializeConditionTopLoadSegmentClassId_Popup();
			//중공정그룹
			InitializeConditionMiddleLoadSegmentClassId_Popup();
			//소공정그룹
			InitializeConditionSmallLoadSegmentClassId_Popup();

			//품목코드
			InitializeConditionProductDefId_Popup();

			//담당자
			InitializeConditionOwner_Popup();

			//미입력
			//InitializeConditionNoInputListOfLoadStd_Popup();
			//InitializeConditionNoInputListOfStdSegment_Popup();
			//InitializeConditionNoInputListOfOwnerMapping_Popup();
			//InitializeConditionNoInputList_Popup();
		}

		/// <summary>
		/// 조회조건의 컨트롤을 추가한다.
		/// </summary>
		protected override void InitializeConditionControls()
		{
			base.InitializeConditionControls();

			// TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
			SetConditionVisiblility("P_LOADTOPSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_LOADMIDDLESEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_LOADSMALLSEGMENTCLASSID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("USERID", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
			SetConditionVisiblility("P_ISLOADMNG", DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 담당자
		/// </summary>
		private void InitializeConditionOwner_Popup()
		{
			var ownerId = Conditions.AddSelectPopup("USERID", new SqlQuery("GetUserList", "10001"), "USERNAME")
				.SetPopupLayout("SELECTOWNER", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("USERNAME")
				.SetLabel("MANAGER")
				.SetPosition(5);

			ownerId.Conditions.AddTextBox("USERIDNAME");

			ownerId.GridColumns.AddTextBoxColumn("USERID", 150);
			ownerId.GridColumns.AddTextBoxColumn("USERNAME", 200);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 표준공정
		/// </summary>
		private void InitializeConditionProcessSegId_Popup()
		{
			var processSegId = Conditions.AddSelectPopup("P_PROCESSSEGMENTID", new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"))
				.SetPopupLayout("SELECTSTANDARDPROCESSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(800, 800, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
				.SetPosition(1.1)
				.SetLabel("STANDARDOPERATIONID");

			processSegId.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
				.SetLabel("MIDDLEPROCESSSEGMENT")
				.SetEmptyItem();
			processSegId.Conditions.AddTextBox("PROCESSSEGMENT");

			processSegId.GridColumns.AddTextBoxColumn("P_PROCESSSEGMENTID", 200)
				.SetLabel("PROCESSSEGMENTID");
			processSegId.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 150);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 대공정 그룹
		/// </summary>
		private void InitializeConditionTopLoadSegmentClassId_Popup()
		{
			var loadTopSegmentClassId = Conditions.AddSelectPopup("P_LOADTOPSEGMENTCLASSID", new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={_TOPLOADSEGMENTTYPE}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTLOADTOPSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPosition(1.2)
				.SetLabel("TOPSEGMENTGROUP");

			loadTopSegmentClassId.Conditions.AddTextBox("TXTLOADTOPSEGMENTCLASS");

			loadTopSegmentClassId.GridColumns.AddTextBoxColumn("P_LOADTOPSEGMENTCLASSID", 150)
				.SetLabel("LOADSEGMENTCLASSID");
			loadTopSegmentClassId.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 중공정 그룹
		/// </summary>
		private void InitializeConditionMiddleLoadSegmentClassId_Popup()
		{
			var loadMiddleSegmentClassId = Conditions.AddSelectPopup("P_LOADMIDDLESEGMENTCLASSID", new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={_MIDDLELOADSEGMENTTYPE}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
				.SetPopupLayout("SELECTLOADMIDDLESEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPosition(1.3)
				.SetLabel("MIDDLESEGMENTGROUP");

			loadMiddleSegmentClassId.Conditions.AddTextBox("TXTLOADMIDDLESEGMENTCLASS");

			loadMiddleSegmentClassId.GridColumns.AddTextBoxColumn("P_LOADMIDDLESEGMENTCLASSID", 150)
				.SetLabel("LOADSEGMENTCLASSID");
			loadMiddleSegmentClassId.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 소공정 그룹
		/// </summary>
		private void InitializeConditionSmallLoadSegmentClassId_Popup()
		{
			var loadMiddleSegmentClassId = Conditions.AddSelectPopup("P_LOADSMALLSEGMENTCLASSID", new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={_SAMLLLOADSEGMENTTYPE}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "", "")
				.SetPopupLayout("SELECTLOADSMALLSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPosition(1.4)
				.SetLabel("SMALLSEGMENTGROUP");

			loadMiddleSegmentClassId.Conditions.AddTextBox("TXTLOADSMALLSEGMENTCLASS");

			loadMiddleSegmentClassId.GridColumns.AddTextBoxColumn("P_LOADSMALLSEGMENTCLASSID", 150)
				.SetLabel("LOADSEGMENTCLASSID");
			loadMiddleSegmentClassId.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150);
		}

		/// <summary>
		/// 팝업형 조회조건 생성 - 품목
		/// </summary>
		private void InitializeConditionProductDefId_Popup()
		{
			// SelectPopup 항목 추가
			var conditionProductId = Conditions.AddSelectPopup("P_PRODUCTDEFID", new SqlQuery("GetProductDefListToPrd", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PRODUCTDEFID", "PRODUCTDEFID")
				.SetPopupLayout("SELECTPRODUCTDEFID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupLayoutForm(800, 800)
				.SetPopupResultCount(0)
				.SetPopupAutoFillColumns("PRODUCTDEFNAME")
				.SetLabel("PRODUCTDEF")
				.SetPosition(2.5);

			conditionProductId.Conditions.AddComboBox("PRODUCTDIVISION", new SqlQuery("GetTypeList", "10001", "CODECLASSID=ProductDefType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
				.SetEmptyItem();
			conditionProductId.Conditions.AddTextBox("TXTPRODUCTDEFNAME");

			// 품목코드
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 150);
			// 품목버전
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 60);
			// 품목명
			conditionProductId.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 250);
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
			DataTable changed = null;
			int index = tabPartion.SelectedTabPageIndex;
			switch(index)
			{
				case 0:
					grdLoadStandardList.View.CheckValidation();
					changed = grdLoadStandardList.GetChangedRows();
					/*
					if(changed.Rows.Count > 0)
					{
						List<DataRow> savedList = changed.AsEnumerable().Where(r => string.IsNullOrEmpty(r.Field<string>("LOADTOPSEGMENTCLASSID"))
																												|| string.IsNullOrEmpty(r.Field<string>("LOADMIDDLESEGMENTCLASSID"))
																												|| string.IsNullOrEmpty(r.Field<string>("LOADSMALLSEGMENTCLASSID"))).ToList();
						if (savedList.Count > 0)
						{
							for(int i = 0; i<savedList.Count; i++)
							{
								DataRow row = savedList[i];
								if( (Format.GetString(row["LOADTOPSEGMENTCLASSID"], "") != Format.GetString(row["LOADTOPSEGMENTCLASSID_COPY"], "")) 
									|| (Format.GetString(row["LOADMIDDLESEGMENTCLASSID"], "") != Format.GetString(row["LOADMIDDLESEGMENTCLASSID_COPY"], ""))
									|| (Format.GetString(row["LOADSMALLSEGMENTCLASSID"], "") != Format.GetString(row["LOADSMALLSEGMENTCLASSID_COPY"], "")))
								{
									// 대공정, 중공정, 소공정 값 변경은 표준공정 탭에서 가능합니다.
									throw MessageException.Create("ChangeValueInLoadPrdSeg");
								}

							}//for
						}//if
					}
					*/
					break;
				case 1://표준공정 맵핑
					grdStandardSegmentMapList.View.CheckValidation();
					changed = grdStandardSegmentMapList.GetChangedRows();
					break;
				case 2://대공정그룹
					grdTopSegmentGroupList.View.CheckValidation();
					changed = grdTopSegmentGroupList.GetChangedRows();
					break;
				case 3://중공정그룹
					grdMiddleSegmentGroupList.View.CheckValidation();
					changed = grdMiddleSegmentGroupList.GetChangedRows();
					break;
				case 4://소공정그룹
					grdSmallSegmentGroupList.View.CheckValidation();
					changed = grdSmallSegmentGroupList.GetChangedRows();
					break;
				case 5://담당자
					grdOwnerMappingList.View.CheckValidation();
					changed = grdOwnerMappingList.GetChangedRows();
					break;
			}//switch

			if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
				throw MessageException.Create("NoSaveData");
			}
		}

		#endregion

		#region Private Function


		/// <summary>
		/// 대공정, 중공정, 소공정 ROW 추가 시 공정 타입 SET
		/// </summary>
		private void SetLoadSegmentClassType(Framework.SmartControls.Grid.AddNewRowArgs args)
		{
			int index = tabPartion.SelectedTabPageIndex;
			switch (index)
			{
				case 2://대공정
					args.NewRow["LOADSEGMENTCLASSTYPE"] = _TOPLOADSEGMENTTYPE;
					break;
				case 3://중공정
					args.NewRow["LOADSEGMENTCLASSTYPE"] = _MIDDLELOADSEGMENTTYPE;
					break;
				case 4://소공정
					args.NewRow["LOADSEGMENTCLASSTYPE"] = _SAMLLLOADSEGMENTTYPE;
					break;
			}
		}

		/// <summary>
		/// 대공정 그룹 팝업 컬럼 Validation
		/// </summary>
		/// <param name="currentGridRow"></param>
		/// <param name="popupSelections"></param>
		/// <returns></returns>
		private ValidationResultCommon ValidationNullorEmpty_LoadTopSegClass(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
		{
			ValidationResultCommon result = new ValidationResultCommon();

			int selected = popupSelections.Count();
			if (selected.Equals(0))
			{
				Language.LanguageMessageItem item = Language.GetMessage("SelectItem", Language.Get("LOADTOPSEGMENTCLASSID"));
				result.IsSucced = false;
				result.FailMessage = item.Message;
				result.Caption = item.Title;
			}

			return result;
		}

		/// <summary>
		/// 중공정 그룹 팝업 컬럼 Validation
		/// </summary>
		/// <param name="currentGridRow"></param>
		/// <param name="popupSelections"></param>
		/// <returns></returns>
		private ValidationResultCommon ValidationNullorEmpty_LoadMiddleSegClass(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
		{
			ValidationResultCommon result = new ValidationResultCommon();

			int selected = popupSelections.Count();
			if (selected.Equals(0))
			{
				Language.LanguageMessageItem item = Language.GetMessage("SelectItem", Language.Get("LOADMIDDLESEGMENTCLASSID"));
				result.IsSucced = false;
				result.FailMessage = item.Message;
				result.Caption = item.Title;
			}

			return result;
		}

		/// <summary>
		/// 소공정 그룹 팝업 컬럼 Validation
		/// </summary>
		/// <param name="currentGridRow"></param>
		/// <param name="popupSelections"></param>
		/// <returns></returns>
		private ValidationResultCommon ValidationNullorEmpty_LoadSmallSegClass(DataRow currentGridRow, IEnumerable<DataRow> popupSelections)
		{
			ValidationResultCommon result = new ValidationResultCommon();

			int selected = popupSelections.Count();
			if (selected.Equals(0))
			{
				Language.LanguageMessageItem item = Language.GetMessage("SelectItem", Language.Get("LOADSMALLSEGMENTCLASSID"));
				result.IsSucced = false;
				result.FailMessage = item.Message;
				result.Caption = item.Title;
			}

			return result;
		}


		#endregion
	}
}
