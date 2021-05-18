#region using

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
using DevExpress.Utils.Menu;
#endregion

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 공정별 체공시간 관리
    /// 업  무  설  명  : 표준공정별 Step의 체공시간 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-12-03
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcessSegmentStayingTime : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        private List<DXMenuItem> menuList = new List<DXMenuItem>();

        #endregion

        #region 생성자

        public ProcessSegmentStayingTime()
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
            InitializeQuickMenuList();
        }

        /// <summary>
        /// 퀵 메뉴 리스트 등록
        /// </summary>
        private void InitializeQuickMenuList()
        {
            menuList.Add(new DXMenuItem(Language.Get("ALLAPPLY"), ApplyAllRow) { BeginGroup = true});
        }

        /// <summary>        
        /// 공정별 체공시간 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdStayingTime.GridButtonItem = GridButtonItem.Delete | GridButtonItem.Import | GridButtonItem.Export;
            grdStayingTime.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            // 표준공정ID
            grdStayingTime.View.AddTextBoxColumn("PROCESSSEGMENTID", 100)
                .SetIsReadOnly();
            // 표준공정버전
            grdStayingTime.View.AddTextBoxColumn("PROCESSSEGMENTVERSION", 80)
                .SetIsReadOnly()
                .SetIsHidden();
            // 표준공정명
            grdStayingTime.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 150)
                .SetIsReadOnly();
            // Site
            grdStayingTime.View.AddComboBoxColumn("PLANTID", 120, new SqlQuery("GetPlantList", "10001", $"ENTERPRISEID={UserInfo.Current.Enterprise}", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "PLANTNAME", "PLANTID")
                .SetIsReadOnly()
                .SetIsHidden()
                .SetLabel("SITE");
            // 인수대기시간(H)
            grdStayingTime.View.AddSpinEditColumn("RECEIVESTAYINGTIME", 100);
            // 시작대기시간(H)
            grdStayingTime.View.AddSpinEditColumn("TRACKINSTAYINGTIME", 100);
            // 완료대기시간(H)
            grdStayingTime.View.AddSpinEditColumn("TRACKOUTSTAYINGTIME", 100);
            // 인계대기시간(H)
            grdStayingTime.View.AddSpinEditColumn("SENDSTAYINGTIME", 100);
            // 설명
            grdStayingTime.View.AddTextBoxColumn("DESCRIPTION", 200);
            // 유효상태
            grdStayingTime.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            // 생성자
            grdStayingTime.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 생성시간
            grdStayingTime.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);
            // 수정자
            grdStayingTime.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // 수정시간
            grdStayingTime.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);


            grdStayingTime.View.PopulateColumns();
        }

        private void InitializeProcessSegmentPopup()
        {
            var processSegmentIdPopup = grdStayingTime.View.AddSelectPopupColumn("PROCESSSEGMENTID", 100, new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("PROCESSSEGMENTVERSION", "PROCESSSEGMENTVERSION")
                .SetPopupResultMapping("PROCESSSEGMENTNAME", "PROCESSSEGMENTNAME")
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetValidationIsRequired()
                .SetSearchTextControlId("PROCESSSEGMENT");
          
            // 복수 선택 여부에 따른 Result Count 지정
            processSegmentIdPopup.SetPopupResultCount(1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT")
                .SetEmptyItem();
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 150)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 150)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 150);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdStayingTime.InitContextMenuEvent += GrdStayingTime_InitContextMenuEvent;
        }

        private void GrdStayingTime_InitContextMenuEvent(SmartBandedGrid sender, Framework.SmartControls.Grid.InitContextMenuEventArgs args)
        {
            for (int i = 0; i < menuList.Count; i++)
            {
                args.AddMenus.Add(menuList[i]);
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

            // TODO : 저장 Rule 변경
            DataTable changed = grdStayingTime.GetChangedRows();

            ExecuteRule("SaveProcessSegmentStayingTime", changed);
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

            //DataTable dtCodeClass = await ProcedureAsync("usp_com_selectCodeClass", values);
            DataTable stayingTimeList = await QueryAsync("SelectProcessSegmentStayingTime", "10001", values);

            if (stayingTimeList.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdStayingTime.DataSource = stayingTimeList;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
            // CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 3, false, Conditions);
            InitializeConditionPopup_ProcessSegment();
        }

        /// <summary>
        /// 조회조건의 컨트롤을 추가한다.
        /// </summary>
        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
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
            grdStayingTime.View.CheckValidation();

            DataTable changed = grdStayingTime.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        private void ApplyAllRow(object sender, EventArgs args)
        {

            DataRow currentRow = grdStayingTime.View.GetFocusedDataRow();
            if (currentRow == null) return;

            if ( grdStayingTime.View.FocusedColumn.FieldName.Equals("RECEIVESTAYINGTIME") || 
                grdStayingTime.View.FocusedColumn.FieldName.Equals("TRACKINSTAYINGTIME") || 
                grdStayingTime.View.FocusedColumn.FieldName.Equals("TRACKOUTSTAYINGTIME") ||
                grdStayingTime.View.FocusedColumn.FieldName.Equals("SENDSTAYINGTIME"))
            {
                string value;
                value = currentRow[grdStayingTime.View.FocusedColumn.FieldName].ToString();
                DataTable dataTable = grdStayingTime.GridControl.DataSource as DataTable;
                for ( int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i][grdStayingTime.View.FocusedColumn.FieldName] = value;
                }
            }
        }

        private void InitializeConditionPopup_ProcessSegment()
        {
            var processSegmentIdPopup = this.Conditions.AddSelectPopup("PROCESSSEGMENTID",  new SqlQuery("GetProcessSegmentList", "10002", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTID")
                .SetPopupLayout(Language.Get("SELECTPROCESSSEGMENTID"), PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupResultMapping("PROCESSSEGMENTVERSION", "PROCESSSEGMENTVERSION")
                .SetPopupResultMapping("PROCESSSEGMENTNAME", "PROCESSSEGMENTNAME")
                .SetPopupLayoutForm(800, 800)
                .SetPopupAutoFillColumns("PROCESSSEGMENTNAME")
                .SetLabel("PROCESSSEGMENT")
                .SetSearchTextControlId("PROCESSSEGMENT")
                .SetRelationIds("P_PROCESSSEGMENTCLASSID_TOP", "P_PROCESSSEGMENTCLASSID_MIDDLE")
                .SetPosition(3);

            // 복수 선택 여부에 따른 Result Count 지정
            processSegmentIdPopup.SetPopupResultCount(1);

            processSegmentIdPopup.Conditions.AddComboBox("PROCESSSEGMENTCLASSID_MIDDLE", new SqlQuery("GetProcessSegmentClassByType", "10001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", "PROCESSSEGMENTCLASSTYPE=MiddleProcessSegmentClass"), "PROCESSSEGMENTCLASSNAME", "PROCESSSEGMENTCLASSID")
                .SetLabel("MIDDLEPROCESSSEGMENT")
                .SetEmptyItem();
            processSegmentIdPopup.Conditions.AddTextBox("PROCESSSEGMENT");


            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_TOP", 100)
                .SetLabel("LARGEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTCLASSNAME_MIDDLE", 100)
                .SetLabel("MIDDLEPROCESSSEGMENT");
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTID", 100);
            processSegmentIdPopup.GridColumns.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
        }

        #endregion
    }
}
