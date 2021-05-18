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

namespace Micube.SmartMES.EquipmentManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 보전실적조회
    /// 업  무  설  명  : 보전실적을 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class BrowseMaintWorkOrderResult : SmartConditionManualBaseForm
    {
        #region Local Variables
        string _searchAreaID;
        string _searchEquipmentID;
        #endregion

        #region 생성자

        public BrowseMaintWorkOrderResult()
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
            InitializeHisotryGrid();
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaintWorkOrderStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdMaintWorkOrderStatus.View.SetIsReadOnly(true);

            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WORKORDERTYPEID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WORKORDERTYPE", 100)              //작업구분
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WORKORDERSTATUSID", 100)
               .SetIsHidden();                                                             //진행상태
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WORKORDERSTATUS", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WORKORDERSTEPID", 100)
               .SetIsHidden();                                                             //진행상태
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WORKORDERSTEP", 100)
                .SetTextAlignment(TextAlignment.Center)
                ;

            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WORKORDERID", 150);              //작업번호
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("FACTORYNAME", 50)                //공장명
                 .SetTextAlignment(TextAlignment.Center); 
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("EQUIPMENTID", 100);              //설비 아이디
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("EQUIPMENTNAME", 300);            //설비명
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("PROCESSSEGMENTCLASS", 100)       //대공정
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTITEMID", 150);              //점검항목 아이디
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTITEMNAME", 180);            //점검항목명
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTTYPEID", 100)
                .SetIsHidden();                                                             //점검유형
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTTYPE", 120)
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTDURATION", 60);            //점검주기
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTDURATIONUNIT", 80)         //점검주기단위
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("ISEQUIPMENTDOWN", 60);          //비가동           
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("SCHEDULETIME", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");             //계획일시
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("STARTTIME", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                //시작시간
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("FINISHTIME", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");               //종료시간
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REPAIRUSER", 120);               //점검자
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REPAIRUSERCOUNT", 80);          //점검인원수	
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REPAIRCOMMENT", 250);            //점검내용
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("DELAYMAINTREASON", 250);         //지연사유
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("DONOTMAINTREASON", 250);         //생략사유
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REQUESTUSERID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REQUESTUSER", 100);              //요청자
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REQUESTTIME", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");              //요청시간
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REQUESTCOMMENTS", 250);          //요청내용
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("ACCEPTUSERID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("ACCEPTUSER", 100);               //접수자
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("ACCEPTTIME", 160)               //접수시간
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("ACCEPTRESULTID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("ACCEPTRESULT", 80)              //접수결과
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("DOWNTYPEID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("DOWNTYPE", 80)                  //고장유형
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("DOWNCODEID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("DOWNCODE", 80)                  //고장현상
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("CAUSECODEID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("CAUSECODE", 100)                 //고장원인
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REPAIRCODEID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REPAIRCODE", 100)                //조치구분
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTKINDCODEID", 100)
                .SetIsHidden();
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("MAINTKINDCODE", 100)             //보전종류
                .SetTextAlignment(TextAlignment.Center)
                ;
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("REPAIRTIME", 160);               //수리시간
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("WAITTIME", 160);					//대기시간
            grdMaintWorkOrderStatus.View.AddTextBoxColumn("BREAKTIME", 160);				//고장시간  

            grdMaintWorkOrderStatus.View.PopulateColumns();
        }
        #endregion

        #region InitializeHisotryGrid : 그리드를 초기화한다.
        private void InitializeHisotryGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdHistory.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기       
            grdHistory.View.SetIsReadOnly(true);

            grdHistory.View.AddTextBoxColumn("WORKORDERID", 150)
                .SetIsHidden();                                                       //작업번호
            grdHistory.View.AddTextBoxColumn("SPAREPARTID", 150)                      //Tool번호
                .SetIsReadOnly(true);
            grdHistory.View.AddTextBoxColumn("SPAREPARTNAME", 250)
                .SetIsReadOnly(true);                                                 //Tool코드
            grdHistory.View.AddTextBoxColumn("MODELNAME", 250)
                .SetIsReadOnly(true);                                                 //Model명
            grdHistory.View.AddTextBoxColumn("OUTPUTTIME", 160)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                             //Tool Version            
            grdHistory.View.AddTextBoxColumn("QTY", 80)                               //Tool번호
               .SetIsReadOnly(true);
            grdHistory.View.AddSpinEditColumn("PRICE", 160)
                ;                                                                     //Tool코드
            


            grdHistory.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            grdMaintWorkOrderStatus.View.FocusedRowChanged += grdMaintWorkOrderStatus_FocusedRowChanged;

            Shown += BrowseMaintWorkOrderResult_Shown;
        }

        #region BrowseMaintWorkOrderResult_Shown - Site관련정보를 화면로딩후 설정한다.
        private void BrowseMaintWorkOrderResult_Shown(object sender, EventArgs e)
        {
            ChangeSiteCondition();

            ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += ConditionPlant_EditValueChanged;
        }
        #endregion

        #region ConditionPlant_EditValueChanged - 검색조건의 Site정보 변경시 관련 쿼리들을 일괄 변경한다.
        private void ConditionPlant_EditValueChanged(object sender, EventArgs e)
        {
            ChangeSiteCondition();
        }
        #endregion

        #region grdMaintWorkOrderStatus_FocusedRowChanged - 그리디의 행 변경이벤트
        private void grdMaintWorkOrderStatus_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SearchHistoryList();
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

        #region OnSearchAsync : 현황조회를 검색한다.
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("ENTERPRISEID", UserInfo.Current.Enterprise);
            #endregion

            if (Conditions.GetValue("AREANAME").ToString() != "")
                values.Add("AREAID", _searchAreaID);

            if (Conditions.GetValue("EQUIPMENTNAME").ToString() != "")
                values.Add("EQUIPMENTID", _searchEquipmentID);

            if (Conditions.GetValue("p_MAINTORDERTYPE").ToString() != "")
                values.Add("MAINTORDERTYPE", GetConditionStringValue(Conditions.GetValue("p_MAINTORDERTYPE").ToString()));

            if (Conditions.GetValue("p_WORKORDERSTEP").ToString() != "")
                values.Add("WORKORDERSTEP", GetConditionStringValue(Conditions.GetValue("p_WORKORDERSTEP").ToString()));

            DataTable toolStatusTable = new DataTable();
            //values = Commons.CommonFunction.ConvertParameter(values);
            toolStatusTable = SqlExecuter.Query("GetMaintWorkOrderListForReportByEqp", "10001", values);

            grdMaintWorkOrderStatus.DataSource = toolStatusTable;

            if (toolStatusTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                grdHistory.View.ClearDatas();
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }
            else
            {
                grdMaintWorkOrderStatus.View.FocusedRowHandle = 0;
                grdMaintWorkOrderStatus_FocusedRowChanged(grdMaintWorkOrderStatus, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }
        #endregion

        #region SearchHistoryList - 내역을 조회한다.
        private void SearchHistoryList()
        {
            DataRow currentRow = grdMaintWorkOrderStatus.View.GetFocusedDataRow();
            if (currentRow != null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음            
                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("WORKORDERID", currentRow.GetString("WORKORDERID"));
                #endregion
                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable searchResult = SqlExecuter.Query("GetSparePartInMaintWorkOrderListForReportByEqp", "10001", values);

                grdHistory.DataSource = searchResult;
            }
        }
        #endregion

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializePlant();
            InitializeAreaPopup();
            InitializeConditionEquipmentIDPopup();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region GetConditionStringValue : 멀티콤보박스의 값을 ' 을 씌워서 검색가능한 값으로 변경한다.
        string GetConditionStringValue(string originCondition)
        {
            if (originCondition.IndexOf(",") > -1)
            {
                string[] conditions = originCondition.Split(',');
                string returnStr = "";
                // ' 기호 추가
                for (int i = 0; i < conditions.Length; i++)
                {
                    conditions[i] = "'" + conditions[i].Trim() + "'";
                }

                // ,로 구분하여 합산
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (i == 0)
                        returnStr = conditions[i];
                    else
                        returnStr += "," + conditions[i];
                }

                return returnStr;
            }
            else
            {
                return "'" + originCondition.Trim() + "'";
            }
        }
        #endregion

        #region InitializePlant : Site설정
        /// <summary>
        /// site 설정 
        /// </summary>
        private void InitializePlant()
        {

            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANTBLANK")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }
        #endregion

        #region InitializeAreaPopup : 팝업창 제어
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeAreaPopup()
        {
            ConditionItemSelectPopup toolCodePopup = Conditions.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
            .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("AREANAME", "AREANAME")
            .SetLabel("AREANAME")
            .SetPopupResultCount(1)
            .SetPosition(2.1)
            .SetRelationIds("P_PLANTID")
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                foreach (DataRow row in selectedRows)
                {
                    _searchAreaID = row["AREAID"].ToString();
                }
            })
            ;

            // 팝업에서 사용할 조회조건 항목 추가
            toolCodePopup.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            toolCodePopup.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            toolCodePopup.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();

        }
        #endregion

        #region InitializeConditionEquipmentIDPopup : 설비조회
        /// <summary>
        /// 설비그룹조회설정
        /// </summary>
        private void InitializeConditionEquipmentIDPopup()
        {
            ConditionItemSelectPopup equipmentPopup = Conditions.AddSelectPopup("EQUIPMENTNAME", new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={ UserInfo.Current.LanguageType }", $"ENTERPRISEID={UserInfo.Current.Enterprise}"))
            .SetPopupLayout("EQUIPMENTNAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("EQUIPMENTNAME", "EQUIPMENTNAME")
            .SetLabel("EQUIPMENT")
            .SetPopupResultCount(1)
            .SetPosition(2.2)
            .SetRelationIds("P_PLANTID")
            .SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                int i = 0;

                foreach (DataRow row in selectedRows)
                {
                    if (i == 0)
                    {
                        _searchEquipmentID = row["EQUIPMENTID"].ToString();
                    }
                    i++;
                }
            })
            ;

            // 팝업 조회조건            
            //equipmentPopup.Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType } }), "PLANTID", "PLANTID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PLANTID")
            //    .SetDefault(UserInfo.Current.Plant, "PLANTID")
            //    .SetIsReadOnly(true)
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;

            //equipmentPopup.Conditions.AddComboBox("AREAID", new SqlQuery("GetAreaListByTool", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PLANTID", UserInfo.Current.Plant } }), "AREANAME", "AREAID")
            //   .SetEmptyItem("", "", true)
            //   .SetLabel("AREAID")
            //   .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //   ;
            InitializeReceiptAreaPopupForPopup(equipmentPopup.Conditions);

            equipmentPopup.Conditions.AddTextBox("EQUIPMENTNAME")
                .SetLabel("EQUIPMENTNAME");

            equipmentPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID", new SqlQuery("GetProcessSegmentClassForRequestByEqp", "10001", new Dictionary<string, object>() { { "LANGUAGETYPE", UserInfo.Current.LanguageType }, { "ENTERPRISEID", UserInfo.Current.Enterprise }, { "PROCESSSEGMENTCLASSTYPE", "TopProcessSegmentClass" } }), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
               .SetEmptyItem("", "", true)
               .SetLabel("PROCESSSEGMENTCLASS")
               .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
               ;            

            equipmentPopup.Conditions.AddTextBox("AREAID")
                .SetLabel("AREAID")
                .SetIsHidden()
                ;

            // 팝업 그리드
            equipmentPopup.IsMultiGrid = false;
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTID", 120)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 250)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("FACTORYID", 150)
              .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("AREAID", 150)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("AREANAME", 120)
                .SetIsReadOnly();
           
            equipmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSID", 200)
                .SetIsHidden();
            equipmentPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME", 200)
                .SetIsReadOnly();
            equipmentPopup.GridColumns.AddTextBoxColumn("EQUPMENTLEVEL", 200)
                .SetIsReadOnly()
                .SetIsHidden();
        }
        #endregion

        #region InitializeReceiptAreaPopup : 입고작업장 팝업창
        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeReceiptAreaPopupForPopup(ConditionCollection tempControl)
        {
            ConditionItemSelectPopup areaCondition = tempControl.AddSelectPopup("AREANAME", new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            areaCondition.ValueFieldName = "AREAID";
            areaCondition.DisplayFieldName = "AREANAME";
            areaCondition.SetPopupLayout("AREA", PopupButtonStyles.Ok_Cancel, true, true);
            areaCondition.SetPopupResultCount(1);
            areaCondition.SetPopupLayoutForm(600, 800, FormBorderStyle.FixedToolWindow);
            areaCondition.SetPopupAutoFillColumns("AREANAME");
            areaCondition.SetPopupResultMapping("AREANAME", "AREANAME");

            areaCondition.Conditions.AddTextBox("AREANAME");
            areaCondition.Conditions.AddTextBox("AREAID");            

            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 150)
                ;
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 200);
        }
        #endregion
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
            //if (popupGridSparepart != null)
            //    popupGridSparepart.SearchQuery = new SqlQuery("GetSparePartStockListForPMByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            if (Conditions.GetCondition("EQUIPMENTNAME") != null)
                ((ConditionItemSelectPopup)((ConditionItemSelectPopup)Conditions.GetCondition("EQUIPMENTNAME")).Conditions.GetCondition("AREANAME")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");

            //if (equipmentPopup != null)
            //{
            //    equipmentPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
            //    ((ConditionItemSelectPopup)equipmentPopup.Conditions.GetCondition("AREANAME")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
            //}

            //if (areaCondition != null)
            //    areaCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");

            //if (equipmentSearchPopup != null)
            //{
            //    equipmentSearchPopup.SearchQuery = new SqlQuery("GetRequestRepairMaintWorkOrderForEquipmentListByEqp", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
            //    ((ConditionItemSelectPopup)equipmentSearchPopup.Conditions.GetCondition("AREANAME")).SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}");
            //}

            //if (areaSearchCondition != null)
            //    areaSearchCondition.SearchQuery = new SqlQuery("GetAreaListByTool", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}");



            //ucFilmCode.PlantID = Conditions.GetValue("P_PLANTID").ToString();

            //작업장 검색조건 초기화
            //((SmartSelectPopupEdit)Conditions.GetControl("AREANAME")).ClearValue();
        }
        #endregion

        #endregion
    }
}
