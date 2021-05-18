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
    /// 프 로 그 램 명  : 설비관리 > 보전관리 > 보전일보
    /// 업  무  설  명  : 보전일보 현황을 조회한다.
    /// 생    성    자  : 김용조
    /// 생    성    일  : 2019-10-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class BrowseMaintWorkOrderDailyReport : SmartConditionManualBaseForm
    {
        #region Local Variables
        #endregion

        #region 생성자

        public BrowseMaintWorkOrderDailyReport()
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
        }

        #region InitializeGrid : 그리드를 초기화한다.
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMaintWorkOrderStatus.GridButtonItem = GridButtonItem.Expand | GridButtonItem.Export | GridButtonItem.Restore;  // 행추가, 행삭제, 엑셀 내보내기, 엑셀 가져오기            
            grdMaintWorkOrderStatus.View.SetIsReadOnly(true);



            Framework.SmartControls.Grid.Conditions.ConditionItemGroup defaultGroup = grdMaintWorkOrderStatus.View.AddGroupColumn("DEFAULTINFO");
            defaultGroup.AddTextBoxColumn("MAINTKINDCODEID", 150)                
                .SetIsHidden();
            defaultGroup.AddTextBoxColumn("MAINTKINDCODE", 100)
                .SetTextAlignment(TextAlignment.Center);          
            defaultGroup.AddTextBoxColumn("WORKORDERID", 120);
            defaultGroup.AddTextBoxColumn("FACTORYID", 100)
               .SetIsHidden();                                                             //진행상태
            defaultGroup.AddTextBoxColumn("FACTORYNAME", 150);
            defaultGroup.AddTextBoxColumn("EQUIPMENTID", 100);              //설비 아이디
            defaultGroup.AddTextBoxColumn("EQUIPMENTNAME", 300);            //설비명              
            defaultGroup.AddTextBoxColumn("REQUESTCOMMENTS", 250);
            defaultGroup.AddTextBoxColumn("CAUSECODEID", 100)
               .SetIsHidden();                                                             //진행상태
            defaultGroup.AddTextBoxColumn("CAUSECODE", 120)
                .SetTextAlignment(TextAlignment.Center);
            defaultGroup.AddTextBoxColumn("REPAIRCOMMENT", 250);
            defaultGroup.AddTextBoxColumn("REPAIRUSER", 150);

            Framework.SmartControls.Grid.Conditions.ConditionItemGroup statusScheduleGroup = grdMaintWorkOrderStatus.View.AddGroupColumn("PROGRESSSTEP");

            statusScheduleGroup.AddTextBoxColumn("ACCEPTTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                //시작시간
            statusScheduleGroup.AddTextBoxColumn("FINISHTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");               //종료시간

            Framework.SmartControls.Grid.Conditions.ConditionItemGroup repairTimeGroup = grdMaintWorkOrderStatus.View.AddGroupColumn("REPAIRTIME");
            repairTimeGroup.AddTextBoxColumn("STARTTIME", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");                //시작시간
            repairTimeGroup.AddTextBoxColumn("FINISHTIME2", 120)
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss");               //종료시간
            repairTimeGroup.AddTextBoxColumn("REPAIRTIME", 120)
                ;                //시작시간

            Framework.SmartControls.Grid.Conditions.ConditionItemGroup anotherGroup = grdMaintWorkOrderStatus.View.AddGroupColumn("ANOTHERTIME");
            anotherGroup.AddTextBoxColumn("BREAKTIME", 120)
                ;               //종료시간
            anotherGroup.AddTextBoxColumn("WAITTIME", 120)
                ;               //종료시간
            
            grdMaintWorkOrderStatus.View.PopulateColumns();
        }
        #endregion

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {         
        }

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

            DataTable toolStatusTable = new DataTable();
            values = Commons.CommonFunction.ConvertParameter(values);
            toolStatusTable = SqlExecuter.Query("GetMaintWorkOrderDailyReportByEqp", "10001", values);

            grdMaintWorkOrderStatus.DataSource = toolStatusTable;

            if (toolStatusTable.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
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
               .SetValidationIsRequired()
               .SetDefault(UserInfo.Current.Plant, "PLANTID") //기본값 설정 UserInfo.Current.Plant
            ;
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

        #endregion
    }
}
