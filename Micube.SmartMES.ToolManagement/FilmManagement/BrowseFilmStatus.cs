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

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 필름관리 > 필름 현황 조회
    /// 업  무  설  명  : 필름의 사용현황을 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-30 
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class BrowseFilmStatus : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _searchAreaID;

        ConditionItemSelectPopup areaCondition;
        ConditionItemSelectPopup filmCodeCondition;
        ConditionItemComboBox segmentCondition;
        #endregion

        #region 생성자

        public BrowseFilmStatus()
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
            InitializeUseStatusGrid();
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdFilmStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            //grdToolStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdFilmStatus.View.AddTextBoxColumn("DURABLESTATEID")
                .SetIsHidden();                                                        //연마상태아이디
            grdFilmStatus.View.AddTextBoxColumn("DURABLESTATE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //연마상태
            grdFilmStatus.View.AddTextBoxColumn("ISHOLD", 80)
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //연마상태
            grdFilmStatus.View.AddTextBoxColumn("FILMNO", 150)                          //Tool번호
                .SetIsReadOnly(true);
            grdFilmStatus.View.AddTextBoxColumn("FILMCODE", 120)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdFilmStatus.View.AddTextBoxColumn("FILMVERSION", 120)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdFilmStatus.View.AddTextBoxColumn("FILMNAME", 400)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdFilmStatus.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(true);                                                  //품목코드
            grdFilmStatus.View.AddTextBoxColumn("PRODUCTDEFNAME", 280)
                .SetIsReadOnly(true);                                                  //품목명
            grdFilmStatus.View.AddTextBoxColumn("DURABLECLASSID")
                .SetIsHidden();                                                        //작업장아이디
            grdFilmStatus.View.AddTextBoxColumn("CONTRACTIONX", 120)
                .SetIsReadOnly(true);                                                  //작업장            
            grdFilmStatus.View.AddTextBoxColumn("CONTRACTIONY", 120)
                .SetIsReadOnly(true);                                                      //상태
            grdFilmStatus.View.AddTextBoxColumn("RESOLUTION", 120)
                .SetIsReadOnly(true);                                                  //상태
            grdFilmStatus.View.AddTextBoxColumn("ISCOATING", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                 //Hold여부
            grdFilmStatus.View.AddTextBoxColumn("VENDORID")
                .SetIsHidden();                                                        //연마상태아이디
            grdFilmStatus.View.AddTextBoxColumn("VENDORNAME", 150)
                .SetIsHidden()
                .SetIsReadOnly(true);                                                  //연마상태
            grdFilmStatus.View.AddTextBoxColumn("AREAID")
               .SetIsHidden();                                                        //연마상태아이디
            grdFilmStatus.View.AddTextBoxColumn("AREANAME", 150)
                .SetIsReadOnly(true);                                                  //연마상태            
            grdFilmStatus.View.AddTextBoxColumn("SCRAPPEDTIME", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetIsReadOnly(true);                                                  //사용타수
            grdFilmStatus.View.AddTextBoxColumn("SCRAPPEDUSERID", 150)
                .SetIsHidden();                                                          //사용타수
            grdFilmStatus.View.AddTextBoxColumn("SCRAPPEDUSER", 100)
                .SetIsReadOnly(true);                                                  //사용타수
            grdFilmStatus.View.AddTextBoxColumn("REASONCODEID")
                .SetIsHidden();                                                        //연마기준타수
            grdFilmStatus.View.AddTextBoxColumn("REASONCODENAME", 100)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //누적사용타수
            grdFilmStatus.View.AddTextBoxColumn("SCRAPPEDCOMMENT", 200)
                .SetIsReadOnly(true);                                                  //보증타수       

            grdFilmStatus.View.PopulateColumns();
        }
        #endregion

        #region InitializeUseStatusGrid : 그리드를 초기화한다.
        private void InitializeUseStatusGrid()
        {
            grdFilmHistory.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기     

            grdFilmHistory.View.AddTextBoxColumn("TRACKINTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdFilmHistory.View.AddTextBoxColumn("TRACKOUTTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdFilmHistory.View.AddTextBoxColumn("LOTID", 250);
            grdFilmHistory.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdFilmHistory.View.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            grdFilmHistory.View.AddTextBoxColumn("AREANAME", 250);
            grdFilmHistory.View.AddTextBoxColumn("EQUIPMENTID", 180);
            grdFilmHistory.View.AddTextBoxColumn("EQUIPMENTNAME", 250);
            grdFilmHistory.View.AddSpinEditColumn("CONSUMEDQTY", 100);

            grdFilmHistory.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdFilmStatus.View.FocusedRowChanged += grdFilmStatus_FocusedRowChanged;
            Shown += BrowseFilmStatus_Shown;
        }

        #region BrowseFilmStatus_Shown - Site관련정보를 화면로딩후 설정한다.
        private void BrowseFilmStatus_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (s, arg) => ChangeSiteCondition();
        }
        #endregion

        #region grdFilmStatus_FocusedRowChanged - 그리드의 행 선택변경 이벤트
        private void grdFilmStatus_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchUseStatusList();
        }
        #endregion
        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        #endregion

        #region 검색
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            values.Add("USERID", UserInfo.Current.Id);
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable filmStatusTable = SqlExecuter.Query("GetFilmStatusListForReportByEqp", "10001", values);

            grdFilmStatus.DataSource = filmStatusTable;

            if (filmStatusTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdFilmStatus.View.FocusedRowHandle = 0;
                grdFilmStatus_FocusedRowChanged(grdFilmStatus, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }
        private void SearchUseStatusList()
        {
            DataRow currentRow = grdFilmStatus.View.GetFocusedDataRow();
            if (currentRow != null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("FILMNO", currentRow.GetString("FILMNO"));
                #endregion

                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable searchResult = SqlExecuter.Query("GetFilmStatusUseHistoryListForReportByEqp", "10001", values);

                grdFilmHistory.DataSource = searchResult;
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // 작업구분
            Conditions.AddComboBox("JOBTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=JobType"), "CODENAME", "CODEID")
                      .SetLabel("JOBTYPE")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3.1)
                      .SetEmptyItem("", "", true);

            // 생산구분
            Conditions.AddComboBox("PRODUCTIONTYPEID", new SqlQuery("GetCodeListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ProductionType"), "CODENAME", "CODEID")
                      .SetLabel("PRODUCTIONTYPE")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3.2)
                      .SetEmptyItem("", "", true);

            #region 작업장

            areaCondition = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}"))
                                      .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
                                      .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
                                      .SetPopupResultMapping("AREANAME", "AREANAME")
                                      .SetLabel("AREANAME")
                                      .SetPopupResultCount(1)
                                      .SetPosition(3.4)
                                      .SetPopupApplySelection((selectedRows, dataGridRow) =>
                                      {
                                          foreach (DataRow row in selectedRows)
                                          {
                                              _searchAreaID = row["AREAID"].ToString();
                                          }
                                      });

            areaCondition.Conditions.AddTextBox("AREANAME");

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 300).SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 300).SetIsReadOnly();

            #endregion

            // DurableState설정
            Conditions.AddComboBox("DURABLESTATE", new SqlQuery("GetDurableStateListByToolWithLanguage", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"USESTATUS=Browse"), "CODENAME", "CODEID")
                      .SetLabel("DURABLESTATE")
                      .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                      .SetPosition(3.6)
                      .SetEmptyItem("", "", true);
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion

        #region Private Function
        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경
        private void ChangeSiteCondition()
        {
            if (segmentCondition != null)
                segmentCondition.Query = new SqlQuery("GetProcessSegmentByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", Conditions.GetValue("P_PLANTID") } });

            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            if (filmCodeCondition != null)
                filmCodeCondition.SearchQuery = new SqlQuery("GetFilmCodePopupListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            //if (productPopup != null)
            //    productPopup.SearchQuery = new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"PRODUCTDEFTYPE=Product");

            //작업장 검색조건 초기화
            ((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }
        #endregion
        #endregion
    }
}
