#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion using

namespace Micube.SmartMES.ToolManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 치공구관리 > 치공구현황관리 > 치공구 현황 조회
    /// 업  무  설  명  : 치공구의 현황을 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-09-30
    /// 수  정  이  력  :
    ///        1. 2021.01.22 전우성 : 누적타수가 보증타수보다 크다면 붉은색 표시 및 화면 정리
    ///
    /// </summary>
    public partial class BrowseToolStatus : SmartConditionManualBaseForm
    {
        #region Local Variables

        /// <summary>
        /// 
        /// </summary>
        private string _defaultSearchCondition = "";

        /// <summary>
        /// 
        /// </summary>
        private ConditionItemSelectPopup areaCondition;

        #endregion Local Variables

        #region 생성자

        public BrowseToolStatus()
        {
            InitializeComponent();
        }

        #endregion 생성자

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 화면의 컨텐츠 영역을 초기화한다.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            InitializeGrid();
            InitializeHisotryGrid();
            InitializeUseStatusGrid();
        }

        #region InitializeGrid : 그리드를 초기화한다.

        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            //grdToolStatus.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdToolStatus.View.AddTextBoxColumn("DURABLESTATEID", 150)
                .SetIsHidden();                                                      //상태
            grdToolStatus.View.AddTextBoxColumn("DURABLESTATE")
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //상태
            grdToolStatus.View.AddTextBoxColumn("ISHOLD", 80)
                .SetIsReadOnly(true);                                                 //Hold여부
            grdToolStatus.View.AddTextBoxColumn("TOOLNO", 150)                          //Tool번호
                .SetIsReadOnly(true)
                .SetIsHidden();
            grdToolStatus.View.AddTextBoxColumn("TOOLCODE", 120)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolStatus.View.AddTextBoxColumn("TOOLNOSEQ", 40)
                .SetIsReadOnly(true);                                                  //벌수
            grdToolStatus.View.AddTextBoxColumn("TOOLVERSION", 80)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolStatus.View.AddTextBoxColumn("TOOLNAME", 400)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolStatus.View.AddComboBoxColumn("PRODUCTIONTYPE", 90, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YieldProductionType"))
                .SetIsReadOnly(true);
            grdToolStatus.View.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsReadOnly(true);                                                  //품목코드
            grdToolStatus.View.AddTextBoxColumn("PRODUCTDEFNAME", 300)
                .SetIsReadOnly(true);                                                  //품목명
            grdToolStatus.View.AddComboBoxColumn("TOOLFORMCODE", 120, new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=ToolForm"), "CODENAME", "CODEID")
               .SetTextAlignment(TextAlignment.Center)
               .SetIsReadOnly(true);                                                  //상태 - (이 회면에서 입력해야 한다면 SetIsReadOnly를 풀어준다.
            grdToolStatus.View.AddTextBoxColumn("TOOLCATEGORYID")
               .SetIsHidden();                                                        //Tool구분아이디
            grdToolStatus.View.AddTextBoxColumn("TOOLCATEGORY", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool구분
            grdToolStatus.View.AddTextBoxColumn("TOOLCATEGORYDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolStatus.View.AddTextBoxColumn("TOOLCATEGORYDETAIL", 120)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolStatus.View.AddTextBoxColumn("TOOLDETAILID")
                .SetIsHidden();                                                        //Tool유형아이디
            grdToolStatus.View.AddTextBoxColumn("TOOLDETAIL", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool유형
            grdToolStatus.View.AddTextBoxColumn("AREAID")
                .SetIsHidden();                                                        //작업장아이디
            grdToolStatus.View.AddTextBoxColumn("AREANAME", 180)
                .SetIsReadOnly(true);                                                  //작업장
            grdToolStatus.View.AddTextBoxColumn("DURABLECLEANSTATEID")
                .SetIsHidden();                                                        //연마상태아이디
            grdToolStatus.View.AddTextBoxColumn("DURABLECLEANSTATE", 80)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //연마상태
            grdToolStatus.View.AddTextBoxColumn("USEDCOUNT", 60)
                .SetIsReadOnly(true);                                                  //사용타수
            grdToolStatus.View.AddTextBoxColumn("CLEANLIMIT", 60)
                .SetIsReadOnly(true);                                                  //연마기준타수
            grdToolStatus.View.AddTextBoxColumn("TOTALUSEDCOUNT", 60)
                .SetIsReadOnly(true);                                                  //누적사용타수
            grdToolStatus.View.AddTextBoxColumn("USEDLIMIT", 60)
                .SetIsReadOnly(true);                                                  //보증타수
            grdToolStatus.View.AddTextBoxColumn("TOTALCLEANCOUNT", 60)
                .SetIsReadOnly(true);                                                  //연마횟수
            grdToolStatus.View.AddTextBoxColumn("TOTALREPAIRCOUNT", 60)
                .SetIsReadOnly(true);                                                  //수리횟수
            grdToolStatus.View.AddSpinEditColumn("WEIGHT", 80)
                .SetDisplayFormat("#,###.####")
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //무게
            grdToolStatus.View.AddSpinEditColumn("HORIZONTAL", 80)
                .SetDisplayFormat("#,###.####")
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //가로
            grdToolStatus.View.AddSpinEditColumn("VERTICAL", 80)
                .SetDisplayFormat("#,###.####")
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //세로
            grdToolStatus.View.AddSpinEditColumn("THEIGHT", 80)
                .SetDisplayFormat("#,###.####")
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //높이
            grdToolStatus.View.AddTextBoxColumn("USEDFACTOR")
                .SetIsReadOnly(true);                                                  //의뢰자이이디
            grdToolStatus.View.AddSpinEditColumn("POLISHTHICKNESS", 80)
                .SetDisplayFormat("#,###.####")
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //연마두께
            grdToolStatus.View.AddSpinEditColumn("TOTALPOLISHTHICKNESS", 80)
                .SetDisplayFormat("#,###.####")
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //누적연마두께
            grdToolStatus.View.AddSpinEditColumn("CREATEDTHICKNESS", 80)
                .SetDisplayFormat("#,###.####")
                .SetIsReadOnly(true)
                .IsFloatValue = true;                                                  //최초두께
            grdToolStatus.View.AddTextBoxColumn("TOOLTHICKNESSLIMIT")
                .SetIsReadOnly(true);                                                  //두께기준

            grdToolStatus.View.PopulateColumns();
        }

        #endregion InitializeGrid : 그리드를 초기화한다.

        #region InitializeHisotryGrid : 그리드를 초기화한다.

        private void InitializeHisotryGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolHistory.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기

            grdToolHistory.View.AddTextBoxColumn("TOOLNO", 150)                          //Tool번호
                .SetIsReadOnly(true)
                .SetIsHidden();
            grdToolHistory.View.AddTextBoxColumn("TOOLCODE", 120)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("TOOLNOSEQ", 40)
                .SetIsReadOnly(true);                                                  //벌수
            grdToolHistory.View.AddTextBoxColumn("TOOLVERSION", 100)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("TRXDATE", 150)                          //Tool번호
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
               .SetIsReadOnly(true);
            grdToolHistory.View.AddTextBoxColumn("TXNID", 120)
                .SetIsHidden();                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("TXNNAME", 150)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("TXNUSER", 100)                          //Tool번호
               .SetIsHidden();
            grdToolHistory.View.AddTextBoxColumn("TXNUSERNAME", 100)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("PREVPLANTID", 150)
                .SetIsHidden();                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("PREVPLANTNAME", 100)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("PREVAREAID", 150)                          //Tool번호
               .SetIsHidden();
            grdToolHistory.View.AddTextBoxColumn("PREVAREANAME", 160)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("PLANTID", 150)
                .SetIsHidden();                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("PLANT", 100)                          //Tool번호
               .SetIsReadOnly(true);
            grdToolHistory.View.AddTextBoxColumn("AREAID", 120)
                .SetIsHidden();                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("AREANAME", 180)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("QTY", 60)                          //Tool번호
               .SetIsReadOnly(true);
            grdToolHistory.View.AddTextBoxColumn("REPAIRDESCRIPTION", 200)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("REPAIRRESULTCOMMENT", 200)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("VENDORID", 120)
                .SetIsHidden();                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("VENDORNAME", 180)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("REQUESTNO", 150)                          //Tool번호
               .SetIsReadOnly(true);
            grdToolHistory.View.AddTextBoxColumn("REQUESTUSER", 100)
                .SetIsReadOnly(true);                                                  //Tool코드
            grdToolHistory.View.AddTextBoxColumn("TOOLMAKETYPE", 60)
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly(true);                                                  //Tool Version
            grdToolHistory.View.AddTextBoxColumn("TOOLMAKETYPEID", 150)                          //Tool번호
               .SetIsHidden();
            grdToolHistory.View.AddTextBoxColumn("TXNHISTKEY", 200)
                .SetIsReadOnly(true);                                                  //Tool코드

            grdToolHistory.View.PopulateColumns();
        }

        #endregion InitializeHisotryGrid : 그리드를 초기화한다.

        #region InitializeUseStatusGrid : 그리드를 초기화한다.

        private void InitializeUseStatusGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdToolUseStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기
            grdToolUseStatus.View.SetIsReadOnly(true);

            grdToolUseStatus.View.AddTextBoxColumn("TRACKINTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdToolUseStatus.View.AddTextBoxColumn("TRACKOUTTIME", 150)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");
            grdToolUseStatus.View.AddTextBoxColumn("LOTID", 250);
            grdToolUseStatus.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            grdToolUseStatus.View.AddTextBoxColumn("AREAID", 100).SetIsHidden();
            grdToolUseStatus.View.AddTextBoxColumn("AREANAME", 180);
            grdToolUseStatus.View.AddTextBoxColumn("EQUIPMENTID", 180);
            grdToolUseStatus.View.AddTextBoxColumn("EQUIPMENTNAME", 250);
            grdToolUseStatus.View.AddSpinEditColumn("TOOLHITCOUNT", 100);
            grdToolUseStatus.View.AddTextBoxColumn("USEDFACTOR", 100);
            grdToolUseStatus.View.AddSpinEditColumn("TOOLPCS", 100);
            grdToolUseStatus.View.AddSpinEditColumn("TOOLPNL", 100);

            grdToolUseStatus.View.PopulateColumns();
        }

        #endregion InitializeUseStatusGrid : 그리드를 초기화한다.

        #endregion 컨텐츠 영역 초기화

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            //그리드 선택행 변경이벤트
            grdToolStatus.View.FocusedRowChanged += (s, e) =>
            {
                SearchHistoryList();
                SearchUseStatusList();
            };

            // ToolBar 및 Site관련 설정을 화면 로딩후에 일괄 적용
            Shown += (s, e) =>
            {
                ChangeSiteCondition();

                //다중 Site 권한을 가진 사용자가 Site를 변경시 환경을 변경해줘야한다.
                ((SmartComboBox)Conditions.GetControl("P_PLANTID")).EditValueChanged += (sender, args) =>
                {
                    ChangeSiteCondition();
                };
            };

            // 누적타수, 보증타수에 따른 색상 변경
            grdToolStatus.View.RowCellStyle += (s, e) =>
            {
                if (e.RowHandle < 0)
                {
                    return;
                }

                int totalusedcnt = Format.GetInteger(grdToolStatus.View.GetRowCellValue(e.RowHandle, "TOTALUSEDCOUNT"), int.MinValue);
                int usedLimit = Format.GetInteger(grdToolStatus.View.GetRowCellValue(e.RowHandle, "USEDLIMIT"), int.MinValue);

                if (totalusedcnt.Equals(int.MinValue) || usedLimit.Equals(int.MinValue))
                {
                    return;
                }

                e.Appearance.ForeColor = totalusedcnt >= usedLimit ? Color.Red : e.Appearance.ForeColor;
            };
        }

        #endregion Event

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();
        }

        #endregion 툴바

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
            values.Add("CURRENTLOGINID", UserInfo.Current.Id);

            #endregion 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음
            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable toolStatusTable = SqlExecuter.Query("GetToolStatusListForReportByEqp", "10001", values);

            grdToolStatus.DataSource = toolStatusTable;

            if (toolStatusTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.

                grdToolHistory.View.ClearDatas();
                grdToolUseStatus.View.ClearDatas();
            }
            else
            {
                grdToolStatus.View.FocusedRowHandle = 0;
                grdToolStatus.View.SelectRow(0);
                //grdToolStatus_FocusedRowChanged(grdToolStatus, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
        }

        #endregion OnSearchAsync : 현황조회를 검색한다.

        #region SearchHistoryList - 내역을 조회한다.

        private void SearchHistoryList()
        {
            DataRow currentRow = grdToolStatus.View.GetFocusedDataRow();
            if (currentRow != null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음

                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("TRANSACTIONSTATUSCODES", _defaultSearchCondition);
                values.Add("DURABLELOTID", currentRow.GetString("TOOLNO"));

                #endregion 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음

                //values = Commons.CommonFunction.ConvertParameter(values);
                DataTable searchResult = SqlExecuter.Query("GetToolHistoryListForReportByEqp", "10001", values);

                grdToolHistory.DataSource = searchResult;
            }
        }

        #endregion SearchHistoryList - 내역을 조회한다.

        #region SearchUseStatusList - 사용내역을 조회한다.

        private void SearchUseStatusList()
        {
            DataRow currentRow = grdToolStatus.View.GetFocusedDataRow();
            if (currentRow != null)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음

                values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
                values.Add("DURABLELOTID", currentRow.GetString("TOOLNO"));

                #endregion 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음

                values = Commons.CommonFunction.ConvertParameter(values);
                DataTable searchResult = SqlExecuter.Query("GetToolUseStatusListForReportByEqp", "10001", values);

                grdToolUseStatus.DataSource = searchResult;
            }
        }

        #endregion SearchUseStatusList - 사용내역을 조회한다.

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            //InitializePlant();
            InitializeAreaPopup();
            InitializeProductPopup();
            InitializeDurableState();
            InitializeIsHold();

            InitializeTransactionStatusCode();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #region InitializePlant : Site설정

        /// <summary>
        /// site 설정
        /// </summary>
        private void InitializePlant()
        {
            var planttxtbox = Conditions.AddComboBox("PLANTID", new SqlQuery("GetPlantList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTID", "PLANTID")
               .SetLabel("PLANT")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.1)
               .SetIsReadOnly(true)
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
        }

        #endregion InitializePlant : Site설정

        #region InitializeAreaPopup : 팝업창 제어

        /// <summary>
        /// 팝업창 제어
        /// </summary>
        private void InitializeAreaPopup()
        {
            areaCondition = Conditions.AddSelectPopup("AREAID", new SqlQuery("GetAreaListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"CURRENTLOGINID={UserInfo.Current.Id}"), "AREANAME", "AREAID")
            .SetPopupLayout("AREANAME", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(500, 600, FormBorderStyle.FixedToolWindow)
            .SetLabel("AREANAME")
            .SetPopupResultCount(1)
            .SetPosition(0.2);

            // 팝업에서 사용할 조회조건 항목 추가
            areaCondition.Conditions.AddTextBox("AREANAME");

            // 팝업 그리드 설정
            areaCondition.GridColumns.AddTextBoxColumn("AREAID", 300)
                .SetIsHidden();
            areaCondition.GridColumns.AddTextBoxColumn("AREANAME", 300)
                .SetIsReadOnly();
        }

        #endregion InitializeAreaPopup : 팝업창 제어

        #region InitializeProductPopup : 품목코드 설정

        /// <summary>
        /// 품목코드 조회 설정
        /// </summary>
        private void InitializeProductPopup()
        {
            ConditionItemSelectPopup productPopup = Conditions.AddSelectPopup("PRODUCTDEFID", new SqlQuery("GetProductdefidPoplistByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={UserInfo.Current.Plant}", $"PRODUCTDEFTYPE=Product"))
            .SetPopupLayout("PRODUCTDEFIDPOPUP", PopupButtonStyles.Ok_Cancel, true, false)
            .SetPopupLayoutForm(984, 465, FormBorderStyle.FixedToolWindow)
            .SetPopupResultMapping("PRODUCTDEFID", "PRODUCTDEFID")
            .SetLabel("PRODUCTDEF")
            .SetPopupResultCount(1)
            .SetPosition(2.5)
            ;

            productPopup.ValueFieldName = "PRODUCTDEFID";
            productPopup.DisplayFieldName = "PRODUCTDEFNAME";

            // 팝업 조회조건
            productPopup.Conditions.AddTextBox("PRODUCTDEF")
                .SetLabel("PRODUCTDEF");
            //productPopup.Conditions.AddComboBox("PRODUCTDEFTYPE", new SqlQuery("GetCodeListByOspProductdeftype", "10001", new Dictionary<string, object>() {{"LANGUAGETYPE", UserInfo.Current.LanguageType}}), "CODENAME", "CODEID")
            //    .SetEmptyItem("", "", true)
            //    .SetLabel("PRODUCTDEFTYPE")
            //    .ComboBoxColumnShowType = ComboBoxColumnShowType.DisplayMemberOnly
            //    ;
            //productPopup.Conditions.AddTextBox("PRODUCTDEFID")
            //    .SetLabel("PRODUCTDEFID");

            // 팝업 그리드
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFID", 250)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFVERSION", 80)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFNAME", 350)
                .SetIsReadOnly();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFCODE", 50)
                .SetIsHidden();
            productPopup.GridColumns.AddTextBoxColumn("PRODUCTDEFTYPE", 200)
                .SetIsReadOnly();
        }

        #endregion InitializeProductPopup : 품목코드 설정

        #region InitializeDurableState : DurableState설정

        /// <summary>
        /// site 설정
        /// </summary>
        private void InitializeDurableState()
        {
            var planttxtbox = Conditions.AddComboBox("DURABLESTATE", new SqlQuery("GetStateListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"STATEMODELID=DurableState"), "STATENAME", "STATEID")
               .SetLabel("DURABLESTATE")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.4)
               .SetEmptyItem("", "", true)
            ;
        }

        #endregion InitializeDurableState : DurableState설정

        #region InitializeIsHold : IsHold설정

        /// <summary>
        /// site 설정
        /// </summary>
        private void InitializeIsHold()
        {
            var planttxtbox = Conditions.AddComboBox("ISHOLD", new SqlQuery("GetCodeList", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"CODECLASSID=YesNo"), "CODENAME", "CODEID")
               .SetLabel("ISHOLD")
               .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
               .SetPosition(0.5)
               .SetEmptyItem("", "", true)
            ;
        }

        #endregion InitializeIsHold : IsHold설정

        #region InitializeTransactionStatusCode : 기본검색조건 초기화

        /// <summary>
        /// site 설정
        /// </summary>
        private void InitializeTransactionStatusCode()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            #region 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음

            values.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            values.Add("CODECLASSID", "ToolTransactionStatus");

            #endregion 기간 검색형 전환 처리 - 만약 시간까지 포함하여 검색한다면 아래 문구는 필요없음

            values = Commons.CommonFunction.ConvertParameter(values);
            DataTable searchResult = SqlExecuter.Query("GetCodeList", "00001", values);

            foreach (DataRow resultRow in searchResult.Rows)
            {
                if (_defaultSearchCondition.Equals(""))
                    _defaultSearchCondition = "'" + resultRow.GetString("CODEID") + "'";
                else
                    _defaultSearchCondition += ",'" + resultRow.GetString("CODEID") + "'";
            }
        }

        #endregion InitializeTransactionStatusCode : 기본검색조건 초기화

        #endregion 검색

        #region 유효성 검사

        /// <summary>
        /// 데이터를 저장 할 때 컨텐츠 영역의 유효성을 검사한다.
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
        }

        #endregion 유효성 검사

        #region Private Function

        #region ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경

        private void ChangeSiteCondition()
        {
            if (areaCondition != null)
                areaCondition.SearchQuery = new SqlQuery("GetAreaIDListByTool", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"PLANTID={Conditions.GetValue("P_PLANTID")}", $"CURRENTLOGINID={UserInfo.Current.Id}");

            ((SmartSelectPopupEdit)Conditions.GetControl("AREAID")).ClearValue();//VENDORNAME
        }

        #endregion ChangeSiteCondition - 검색조건의 Site변경시 관련된 쿼리들을 변경

        #endregion Private Function
    }
}