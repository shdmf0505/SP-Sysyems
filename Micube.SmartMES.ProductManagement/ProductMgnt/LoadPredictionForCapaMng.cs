#region using

using DevExpress.XtraGrid.Views.Grid;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.Conditions;
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

namespace Micube.SmartMES.ProductManagement
{
	/// <summary>
	/// 프 로 그 램 명  : 생산관리 > 공정부하 > 공정부하 CAPA관리
	/// 업  무  설  명  : 공정 부하 CAPA 관리
	/// 생    성    자  : 한주석
	/// 생    성    일  : 2019-09-16
	/// 수  정  이  력  : 
	/// 
	/// 
	/// </summary>
	public partial class LoadPredictionForCapaMng : SmartConditionManualBaseForm
	{
		#region 생성자

		public LoadPredictionForCapaMng()
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

			InitializeGridCapaMgmtOfSegmentBaseInfo();
			InitializeGridCapaMgmtOfLoadBaseInfo();

		}

        /// <summary>
        /// 공정 CAPA 그리드 초기화
        /// </summary>
        private void InitializeGridCapaMgmtOfSegmentBaseInfo()
        {
            #region 공정 capa 정보
            grdCapaMgmtOfSegBaseInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdCapaMgmtOfSegBaseInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            var grdcapainfo = grdCapaMgmtOfSegBaseInfo.View.AddGroupColumn("DEFAULTINFO");
            //SITE
            grdcapainfo.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault(UserInfo.Current.Plant);
            //작업장ID
            grdcapainfo.AddTextBoxColumn("AREAID", 10)
               .SetIsHidden();
            //작업장명
            InitializeConditionAreaId_Popup(grdcapainfo);
            //공정부하 CAPA TYPE
            grdcapainfo.AddComboBoxColumn("LOADCAPATYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LOADCAPATYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("ProcessCapa")
                .SetIsHidden();
            //기업
            grdcapainfo.AddTextBoxColumn("ENTERPRISEID", 10)
                .SetIsHidden();
            //자사구분
            grdcapainfo.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //자원유형  
            grdcapainfo.AddComboBoxColumn("RESOURCETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CapaResourceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //자원ID  
            grdcapainfo.AddTextBoxColumn("RESOURCEID", 200)
                  .SetIsHidden();
            //자원(설비그룹)
            InitializeConditionResourceId_Popup(grdcapainfo);
            //RTR/SHT
            grdcapainfo.AddComboBoxColumn("RTRSHT", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RTRSHT", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //대공정 ID
            grdcapainfo.AddTextBoxColumn("TOPLOADSEGMENTCLASSID", 130)
                .SetIsReadOnly()
                .SetIsHidden();
            //대공정 NAME
            InitializeGridLoadTopSegmentClassIdListPopup(grdcapainfo);
            //중공정 ID
            grdcapainfo.AddTextBoxColumn("MIDDLESEGMENTCLASSID", 130)
                .SetIsReadOnly()
                .SetIsHidden();
            //중공정 NAME
            InitializeGridLoadMiddleSegmentClassIdListPopup(grdcapainfo);
            //소공정 ID
            grdcapainfo.AddTextBoxColumn("SMALLSEGMENTCLASSID", 130)
               .SetIsReadOnly()
               .SetIsHidden();
            //소공정 NAME
            InitializeGridLoadSmallSegmentClassIdListPopup(grdcapainfo);
            #endregion

            #region 공정 capa 부하 기준 정보

            var grdproductioncapa = grdCapaMgmtOfSegBaseInfo.View.AddGroupColumn("LOADSTANDARD");
            //설비 TOTAL 
            grdproductioncapa.AddTextBoxColumn("TOTAL", 100)
                .SetIsReadOnly()
                .SetDefault(0);

            //가동
            grdproductioncapa.AddTextBoxColumn("CAPARUN", 100);
            //대기
            grdproductioncapa.AddTextBoxColumn("CAPAIDLE", 100);
            //비가동
            grdproductioncapa.AddTextBoxColumn("CAPANOTRUN", 100);
            //부하시간
            grdproductioncapa.AddTextBoxColumn("LOADTIME", 100);
            //주중(%)
            grdproductioncapa.AddTextBoxColumn("WEEKDAYCAPARATE", 100);
            //주말(%)
            grdproductioncapa.AddTextBoxColumn("WEEKENDCAPARATE", 100);
            //MAX 실적
            grdproductioncapa.AddTextBoxColumn("MAXRESULT", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);



            #endregion

            #region  공정 대/인당 표준 capa 

            var grdstdcapa = grdCapaMgmtOfSegBaseInfo.View.AddGroupColumn("STDCAPA");
            //PNL(500)
            grdstdcapa.AddTextBoxColumn("STDCAPAPANEL500", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //PNL(250)
            grdstdcapa.AddTextBoxColumn("STDCAPAPANEL250", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //MM
            grdstdcapa.AddTextBoxColumn("STDCAPAMM", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            #endregion

            #region  공정 합계 표준 capa 

            var grdsumstdcapa = grdCapaMgmtOfSegBaseInfo.View.AddGroupColumn("SUMSTDCAPA");
            //PNL(500)
            grdsumstdcapa.AddTextBoxColumn("SUMSTDCAPAPANEL500", 100)
                .SetLabel("STDCAPAPANEL500")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //PNL(250)
            grdsumstdcapa.AddTextBoxColumn("SUMSTDCAPAPANEL250", 100)
                .SetLabel("STDCAPAPANEL250")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //MM
            grdsumstdcapa.AddTextBoxColumn("SUMSTDCAPAMM", 100)
                .SetLabel("STDCAPAMM")
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdCapaMgmtOfSegBaseInfo.View.PopulateColumns();
            #endregion
        }

        private void InitializeGridCapaMgmtOfLoadBaseInfo()
        {
            #region 부하량 capa 정보
            grdCapaMgmtOfLoadBaseInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdCapaMgmtOfLoadBaseInfo.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            var grdloadcapainfo = grdCapaMgmtOfLoadBaseInfo.View.AddGroupColumn("DEFAULTINFO");
            //SITE  
            grdloadcapainfo.AddComboBoxColumn("PLANTID", 80, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                 .SetValidationIsRequired()
                 .SetTextAlignment(TextAlignment.Center)
                 .SetDefault(UserInfo.Current.Plant);
            //작업장ID
            grdloadcapainfo.AddTextBoxColumn("AREAID", 10)
               .SetIsHidden();
            //작업장명
            InitializeConditionAreaId_Popup(grdloadcapainfo);
            //공정부하 CAPA TYPE
            grdloadcapainfo.AddComboBoxColumn("LOADCAPATYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=LOADCAPATYPE", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("LoadCapa")
                .SetIsHidden();
            //자원ID
            grdloadcapainfo.AddTextBoxColumn("RESOURCEID", 200)
                .SetIsHidden();
            //자원유형  
            grdloadcapainfo.AddComboBoxColumn("RESOURCETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=CapaResourceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //자원(설비그룹)
            InitializeConditionResourceId_Popup(grdloadcapainfo);
  
            //기업
            grdloadcapainfo.AddTextBoxColumn("ENTERPRISEID", 10)
               .SetIsHidden();
            // 자사구분
            grdloadcapainfo.AddComboBoxColumn("OWNTYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=OwnType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //대공정 ID
            grdloadcapainfo.AddTextBoxColumn("TOPLOADSEGMENTCLASSID", 130)
                .SetIsReadOnly()
                .SetIsHidden();
            //대공정 NAME
            InitializeGridLoadTopSegmentClassIdListPopup(grdloadcapainfo);
            //중공정 ID
            grdloadcapainfo.AddTextBoxColumn("MIDDLESEGMENTCLASSID", 130)
                .SetIsReadOnly()
                .SetIsHidden();
            //중공정 NAME
            InitializeGridLoadMiddleSegmentClassIdListPopup(grdloadcapainfo);
            //소공정 ID
            grdloadcapainfo.AddTextBoxColumn("SMALLSEGMENTCLASSID", 130)
               .SetIsReadOnly()
               .SetIsHidden();
            //소공정 NAME
            InitializeGridLoadSmallSegmentClassIdListPopup(grdloadcapainfo); ;
            #endregion

            #region  부하량 표준 capa 

            var grdstdloadcapa = grdCapaMgmtOfLoadBaseInfo.View.AddGroupColumn("STDCAPA");
            //부하량 일주일 평균
            grdstdloadcapa.AddTextBoxColumn("WEEKAVGCAPA", 150)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //PNL(500)
            grdstdloadcapa.AddTextBoxColumn("STDCAPAPANEL500", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //PNL(250)
            grdstdloadcapa.AddTextBoxColumn("STDCAPAPANEL250", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //MM
            grdstdloadcapa.AddTextBoxColumn("STDCAPAMM", 100)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);
            //표준 CAPA - 부하량 차이
            grdstdloadcapa.AddTextBoxColumn("CAPASUBLOAD", 200)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric);

            grdCapaMgmtOfLoadBaseInfo.View.PopulateColumns();
            #endregion

        }
        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdCapaMgmtOfLoadBaseInfo.View.ShowingEditor += View_ShowingEditor;
            grdCapaMgmtOfSegBaseInfo.View.ShowingEditor += View_ShowingEditor;
            grdCapaMgmtOfSegBaseInfo.View.CellValueChanged += View_CellValueChanged;
        }

        private void View_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dr = grdCapaMgmtOfSegBaseInfo.View.GetFocusedDataRow();
            if (e.Column.FieldName == "CAPARUN")
            {
                    dr["TOTAL"] = Convert.ToDouble(dr["TOTAL"].ToString()) + Convert.ToDouble(dr["CAPARUN"].ToString());                
            }
            if(e.Column.FieldName == "CAPAIDLE")
            {
                dr["TOTAL"] = Convert.ToDouble(dr["TOTAL"].ToString()) + Convert.ToDouble(dr["CAPAIDLE"].ToString());
            }
            if (e.Column.FieldName == "CAPANOTRUN")
            {
                dr["TOTAL"] = Convert.ToDouble(dr["TOTAL"].ToString()) + Convert.ToDouble(dr["CAPANOTRUN"].ToString());
            }
        }


        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
		{
            base.OnToolbarSaveClick();

            DataTable dt = null;
            int index = tabPartion.SelectedTabPageIndex;
            string tableName = "";
            string ruleName = "";

            switch (index)
            {
                case 0://표준공정 맵핑
                    dt = grdCapaMgmtOfSegBaseInfo.GetChangedRows();
                    ruleName = "SavePredictionForCapaMng";
                    tableName = "segmentCapaMng";
                    dt.TableName = tableName;

                    break;
                case 1://부하량 기준정보
                    dt = grdCapaMgmtOfLoadBaseInfo.GetChangedRows();
                    ruleName = "SavePredictionForCapaMng";
                    tableName = "loadCapaMng";
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

            var values = Conditions.GetValues();
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
     
            if (tabPartion.SelectedTabPage == segmentcapamng) //공정 CAPA 관리 탭 선택시
            {
                values.Add("LOADCAPATYPE", "ProcessCapa");
                DataTable dtcapaClass = await SqlExecuter.QueryAsync("SelectProductCapaMng", "10001", values);

                if (dtcapaClass.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }
                grdCapaMgmtOfSegBaseInfo.DataSource = dtcapaClass;
            }
            else //부하량 관리 CAPA 비교 탭 선택시
            {
                
                values.Add("LOADCAPATYPE", "LoadCapa");
                DataTable dtloadcapaClass = await SqlExecuter.QueryAsync("SelectProductLoadCapaMng", "10001", values);
                if (dtloadcapaClass.Rows.Count < 1)
                {
                    ShowMessage("NoSelectData");
                }
                grdCapaMgmtOfLoadBaseInfo.DataSource = dtloadcapaClass;
 
            }
        }

		/// <summary>
		/// 조회조건 항목을 추가한다.
		/// </summary>
		protected override void InitializeCondition()
		{
			base.InitializeCondition();

            //자원유형 
            Conditions.AddComboBox("RESOURCETYPE", new SqlQuery("GetCodeList", "00001", "CODECLASSID=CapaResourceType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            //작업장
            CommonFunction.AddConditionAreaPopup("P_AREAID", 4, false, Conditions, false);
			//대공정그룹
			InitializeConditionTopLoadSegmentClassId_Popup();
			//중공정그룹
			InitializeConditionMiddleLoadSegmentClassId_Popup();
            //소공정그룹
            InitializeConditionSmallLoadSegmentClassId_Popup();
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
		/// 팝업형 조회조건 생성 - 대공정 그룹
		/// </summary>
		private void InitializeConditionTopLoadSegmentClassId_Popup()
		{
			var loadTopSegmentClassId = Conditions.AddSelectPopup("P_LOADTOPSEGMENTCLASSID", new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "LOADSEGMENTCLASSTYPE=TopLoadSegmentClass", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "LOADSEGMENTCLASSNAME", "P_LOADTOPSEGMENTCLASSID")
				.SetPopupLayout("SELECTLOADTOPSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPosition(5.0)
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
			var loadMiddleSegmentClassId = Conditions.AddSelectPopup("P_LOADMIDDLESEGMENTCLASSID", new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "LOADSEGMENTCLASSTYPE=MiddleLoadSegmentClass", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "LOADSEGMENTCLASSNAME", "P_LOADMIDDLESEGMENTCLASSID")
				.SetPopupLayout("SELECTLOADMIDDLESEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
				.SetPopupResultCount(0)
				.SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
				.SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
				.SetPosition(6.0)
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
            var loadSmallSegmentClassId = Conditions.AddSelectPopup("P_LOADSMALLSEGMENTCLASSID", new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "LOADSEGMENTCLASSTYPE=SmallLoadSegmentClass", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "LOADSEGMENTCLASSNAME", "P_LOADSMALLSEGMENTCLASSID")
                .SetPopupLayout("SELECTLOADSMALLSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(0)
                .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                .SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
                .SetPosition(6.0)
                .SetLabel("SMALLSEGMENTGROUP");

            loadSmallSegmentClassId.Conditions.AddTextBox("TXTLOADSMALLSEGMENTCLASS");

            loadSmallSegmentClassId.GridColumns.AddTextBoxColumn("P_LOADSMALLSEGMENTCLASSID", 150)
                .SetLabel("LOADSEGMENTCLASSID");
            loadSmallSegmentClassId.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 150);
        }

        /// <summary>
        /// 팝업형 컬럼 초기화 - 대공정 그룹
        /// </summary>
        private void InitializeGridLoadTopSegmentClassIdListPopup(Framework.SmartControls.Grid.Conditions.ConditionItemGroup grdcapainfo)
        {
            var loadTopSegmentClassIdColumn = grdcapainfo.AddSelectPopupColumn("TOPPROCESSSEGMENTCLASSNAME", 150, new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={"TopLoadSegmentClass"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "TOPLOADSEGMENTCLASSID")
                  .SetPopupLayout("SELECTLOADTOPSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
                  .SetPopupResultCount(1)
                  .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                  .SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
                  .SetPopupResultMapping("TOPLOADSEGMENTCLASSID", "P_LOADTOPSEGMENTCLASSID")
                  .SetPopupResultMapping("TOPPROCESSSEGMENTCLASSNAME", "LOADSEGMENTCLASSNAME");
                  
            loadTopSegmentClassIdColumn.Conditions.AddTextBox("TXTLOADTOPSEGMENTCLASS");
            loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("P_LOADTOPSEGMENTCLASSID", 130)
                 .SetLabel("TOPPROCESSSEGMENTCLASSID");
            loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 130)
                .SetLabel("TOPPROCESSSEGMENTCLASSNAME")
                .SetIsReadOnly();
        }
       
        /// <summary>
        /// 팝업형 컬럼 초기화 - 중공정 그룹
        /// </summary>
        private void InitializeGridLoadMiddleSegmentClassIdListPopup(Framework.SmartControls.Grid.Conditions.ConditionItemGroup grdcapainfo)
        {
            var loadTopSegmentClassIdColumn = grdcapainfo.AddSelectPopupColumn("MIDDLEPROCESSSEGMENTCLASSNAME", 150, new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={"MiddleLoadSegmentClass"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "MIDDLESEGMENTCLASSID")
                  .SetPopupLayout("SELECTLOADMIDDLESEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
                  .SetPopupResultCount(1)
                  .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                  .SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
                  .SetPopupResultMapping("MIDDLESEGMENTCLASSID", "P_LOADMIDDLESEGMENTCLASSID")
                  .SetPopupResultMapping("MIDDLEPROCESSSEGMENTCLASSNAME", "LOADSEGMENTCLASSNAME");

            loadTopSegmentClassIdColumn.Conditions.AddTextBox("TXTLOADTOPSEGMENTCLASS");
            loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("P_LOADTOPSEGMENTCLASSID", 130)
                 .SetLabel("MIDDLEPROCESSSEGMENTCLASSID");
            loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 130)
                .SetLabel("MIDDLEPROCESSSEGMENTCLASSNAME")
                .SetIsReadOnly();
        }

        /// <summary>
        /// 팝업형 컬럼 초기화 - 소공정 그룹
        /// </summary>
        private void InitializeGridLoadSmallSegmentClassIdListPopup(Framework.SmartControls.Grid.Conditions.ConditionItemGroup grdcapainfo)
        {
            var loadTopSegmentClassIdColumn = grdcapainfo.AddSelectPopupColumn("SMALLPROCESSSEGMENTCLASSNAME", 150, new SqlQuery("GetLoadSegmentListByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"LOADSEGMENTCLASSTYPE={"SmallLoadSegmentClass"}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"), "SMALLSEGMENTCLASSID")
                  .SetPopupLayout("SELECTLOADSMALLSEGMENTCLASSID", PopupButtonStyles.Ok_Cancel, true, true)
                  .SetPopupResultCount(1)
                  .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                  .SetPopupAutoFillColumns("LOADSEGMENTCLASSNAME")
                  .SetPopupResultMapping("SMALLSEGMENTCLASSID", "P_LOADSMALLSEGMENTCLASSID")
                  .SetPopupResultMapping("SMALLPROCESSSEGMENTCLASSNAME", "LOADSEGMENTCLASSNAME");
          

            loadTopSegmentClassIdColumn.Conditions.AddTextBox("TXTLOADSMALLSEGMENTCLASS");
            loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("P_LOADSMALLSEGMENTCLASSID", 130)
                 .SetLabel("SMALLPROCESSSEGMENTCLASSID");
            loadTopSegmentClassIdColumn.GridColumns.AddTextBoxColumn("LOADSEGMENTCLASSNAME", 130)
                .SetLabel("SMALLPROCESSSEGMENTCLASSNAME")
                .SetIsReadOnly();
        }

        /// <summary>
        /// 팝업형 컬럼 초기화 - 자원 ID
        /// </summary>  
        private void InitializeConditionResourceId_Popup(Framework.SmartControls.Grid.Conditions.ConditionItemGroup grdcapainfo)
        {
            var resourceColumn = grdcapainfo.AddSelectPopupColumn("DESCRIPTION", 300, new SqlQuery("GetResourceNameAndResourceId", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}"))
                              .SetPopupLayout("RESOURCEID", PopupButtonStyles.Ok_Cancel, true, true)
                              .SetPopupResultCount(1)
                              .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                              .SetValidationIsRequired()
                              .SetLabel("RESOURCE")
                              .SetPopupApplySelection((selectedRows, dataGridRow) =>
                              {
                                  List<string> selectList = selectedRows.AsEnumerable().Select(r => r.Field<string>("RESOURCEID")).Distinct().ToList();
                                  foreach (DataRow row in selectedRows)
                                  {
                                      dataGridRow["RESOURCEID"] = row["RESOURCEID"].ToString();
                                      dataGridRow["DESCRIPTION"] = row["DESCRIPTION"].ToString();
                                  }
                              });

            resourceColumn.Conditions.AddTextBox("RESOURCE");
            resourceColumn.Conditions.AddTextBox("AREAID")
                         .SetPopupDefaultByGridColumnId("AREAID")
                         .SetIsHidden();

            resourceColumn.GridColumns.AddTextBoxColumn("RESOURCEID", 170);
            resourceColumn.GridColumns.AddTextBoxColumn("DESCRIPTION", 250)
                             .SetLabel("RESOURCE");
           
        }

        /// <summary>
        /// 팝업형 컬럼 초기화 - 작업장 NAME
        /// </summary>  
        private void InitializeConditionAreaId_Popup(Framework.SmartControls.Grid.Conditions.ConditionItemGroup grdcapainfo)
        { 
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("AreaType", "Area");
            param.Add("PlantId", UserInfo.Current.Plant);
            param.Add("LanguageType", UserInfo.Current.LanguageType);

            var resourceColumn = grdcapainfo.AddSelectPopupColumn("AREANAME", 200, new SqlQuery("GetAreaList", "10003", param), "AREAID")
                              .SetPopupLayout("TXTAREA", PopupButtonStyles.Ok_Cancel, true, true)
                              .SetPopupResultCount(1)
                              .SetPopupLayoutForm(600, 600, FormBorderStyle.FixedToolWindow)
                              .SetValidationIsRequired()
                              .SetPopupAutoFillColumns("AREANAME");
                          
            resourceColumn.Conditions.AddTextBox("TXTAREA");
            resourceColumn.GridColumns.AddTextBoxColumn("AREAID", 150);
            resourceColumn.GridColumns.AddTextBoxColumn("AREANAME", 200);
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
            switch (index)
            {
                case 0:
                    grdCapaMgmtOfSegBaseInfo.View.CheckValidation();
                    changed = grdCapaMgmtOfSegBaseInfo.GetChangedRows();

                    break;
                case 1:
                    grdCapaMgmtOfLoadBaseInfo.View.CheckValidation();
                    changed = grdCapaMgmtOfLoadBaseInfo.GetChangedRows();
                    break;
            }
            if (changed.Rows.Count == 0)
			{
				// 저장할 데이터가 존재하지 않습니다.
			throw MessageException.Create("NoSaveData");
			}
		}

        #endregion

        #region Private Function
        /// <summary>
        /// PK 수정 방지
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataRow row = null;
            int index = tabPartion.SelectedTabPageIndex;
            switch (index)
            {
                case 0://표준공정 맵핑
                    row = grdCapaMgmtOfSegBaseInfo.View.GetFocusedDataRow();
                    if (row.RowState != DataRowState.Added)
                    {
                        GridView view = sender as GridView;

                        if (view.FocusedColumn.FieldName.Equals("DESCRIPTION"))
                        {
                            e.Cancel = true;
                        }
                        if (view.FocusedColumn.FieldName.Equals("AREANAME"))
                        {
                            e.Cancel = true;
                        }
                        if (view.FocusedColumn.FieldName.Equals("LOADCAPATYPE"))
                        {
                            e.Cancel = true;
                        }
                    }

                    break;
                case 1://부하량 기준정보
                    row = grdCapaMgmtOfLoadBaseInfo.View.GetFocusedDataRow();
                    if (row.RowState != DataRowState.Added)
                    {
                        GridView view = sender as GridView;

                        if (view.FocusedColumn.FieldName.Equals("RESOURCEID"))
                        {
                            e.Cancel = true;
                        }
                        if (view.FocusedColumn.FieldName.Equals("AREANAME"))
                        {
                            e.Cancel = true;
                        }
                        if (view.FocusedColumn.FieldName.Equals("LOADCAPATYPE"))
                        {
                            e.Cancel = true;
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}
