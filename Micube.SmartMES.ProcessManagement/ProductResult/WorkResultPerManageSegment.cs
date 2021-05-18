#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.ProcessManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 공정관리 > 생산실적 > 관리공정별 실적현황
    /// 업  무  설  명  : 관리공정별 실적현황을 조회한다.
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2019-10-10
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class WorkResultPerManageSegment : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        
        #endregion

        #region 생성자

        public WorkResultPerManageSegment()
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
            InitializePNLGrid();
            InitializeMMGrid();
        }
        

        /// <summary>        
        /// PNL 탭 그리드를 초기화한다.
        /// </summary>
        private void InitializePNLGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdWorkResultPerPNL.GridButtonItem = GridButtonItem.Export;
			grdWorkResultPerPNL.View.SetIsReadOnly();


			var grdworkresultinfo = grdWorkResultPerPNL.View.AddGroupColumn("");
            // 대공정
            grdworkresultinfo.AddTextBoxColumn("TOPPROCESSSEGMENTCLASS", 80);
            // 작업장
            grdworkresultinfo.AddTextBoxColumn("AREANAME", 150);
            //생산량합계(금일실적)
            grdworkresultinfo.AddTextBoxColumn("SUM_RESULT", 100).SetDisplayFormat("{0:#,###}").SetLabel("PRODUCTIONQTYSUM");

			//전일실적
            var grdlastdayresult = grdWorkResultPerPNL.View.AddGroupColumn("LASTDAYRESULT");
            grdlastdayresult.AddTextBoxColumn("RESULT_PREVNOTOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetLabel("NOOUTSOURCING");
            grdlastdayresult.AddTextBoxColumn("RESULT_PREVOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetLabel("OUTSOURCING");

			//금일실적
            var grdtodayresult = grdWorkResultPerPNL.View.AddGroupColumn("TODAYRESULT");
            grdtodayresult.AddTextBoxColumn("RESULT_TODAYNOTOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetLabel("NOOUTSOURCING");
            grdtodayresult.AddTextBoxColumn("RESULT_TODAYOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetLabel("OUTSOURCING");

			//전일재공
            var grdlastdaywip = grdWorkResultPerPNL.View.AddGroupColumn("LASTDAYWIP");
            grdlastdaywip.AddTextBoxColumn("WIP_PREVNOTOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetLabel("NOOUTSOURCING");
            grdlastdaywip.AddTextBoxColumn("WIP_PREVOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetIsReadOnly().SetLabel("OUTSOURCING");

			//금일재공
            var grdtodaywip = grdWorkResultPerPNL.View.AddGroupColumn("TODAYWIP");
            grdtodaywip.AddTextBoxColumn("WIP_TODAYNOTOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetLabel("NOOUTSOURCING");
            grdtodaywip.AddTextBoxColumn("WIP_TODAYOSPQTY", 100).SetDisplayFormat("{0:#,###}").SetLabel("OUTSOURCING");

            grdWorkResultPerPNL.View.PopulateColumns();
        }

 

        /// <summary>        
        /// MM 탭 그리드를 초기화한다.
        /// </summary>
        private void InitializeMMGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdWorkResultPerMM.GridButtonItem = GridButtonItem.Export;
			grdWorkResultPerMM.View.SetIsReadOnly();

            var workResultInfo = grdWorkResultPerMM.View.AddGroupColumn("");

			// 대공정
			workResultInfo.AddTextBoxColumn("TOPPROCESSSEGMENTCLASS", 80);
			// 작업장
			workResultInfo.AddTextBoxColumn("AREANAME", 200);
			// 생산량합계(금일실적)
			workResultInfo.AddTextBoxColumn("SUM_RESULT", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("PRODUCTIONQTYSUM");

			//전일실적
            var lastDayResult = grdWorkResultPerMM.View.AddGroupColumn("LASTDAYRESULT");
			lastDayResult.AddTextBoxColumn("RESULT_PREVNOTOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("NOOUTSOURCING");
			lastDayResult.AddTextBoxColumn("RESULT_PREVOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("OUTSOURCING");

			//금일실적
            var todayResult = grdWorkResultPerMM.View.AddGroupColumn("TODAYRESULT");
			todayResult.AddTextBoxColumn("RESULT_TODAYNOTOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("NOOUTSOURCING");
			todayResult.AddTextBoxColumn("RESULT_TODAYOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("OUTSOURCING");

			//전일재공
            var grdlastdaywip = grdWorkResultPerMM.View.AddGroupColumn("LASTDAYWIP");
            grdlastdaywip.AddTextBoxColumn("WIP_PREVNOTOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("NOOUTSOURCING");
            grdlastdaywip.AddTextBoxColumn("WIP_PREVOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("OUTSOURCING");

			//금일재공
            var grdtodaywip = grdWorkResultPerMM.View.AddGroupColumn("TODAYWIP");
            grdtodaywip.AddTextBoxColumn("WIP_TODAYNOTOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("NOOUTSOURCING");
            grdtodaywip.AddTextBoxColumn("WIP_TODAYOSPMM", 100).SetDisplayFormat("{0:#,###.##}").SetLabel("OUTSOURCING");

            grdWorkResultPerMM.View.PopulateColumns();
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

			int index = tabWorkResultPerSegment.SelectedTabPageIndex;
			switch (index)
			{
				case 0:
					values.Add("TYPE", "PNL");
					DataTable dtLotWorkResultPanel = await SqlExecuter.QueryAsync("SelectWorkResultPerManageSegment", "10002", values);

					if (dtLotWorkResultPanel.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdWorkResultPerPNL.DataSource = dtLotWorkResultPanel;
					break;
				case 1:
					DataTable dtLotWorkResultMM = await SqlExecuter.QueryAsync("SelectWorkResultPerManageSegment", "10002", values);

					if (dtLotWorkResultMM.Rows.Count < 1)
					{
						// 조회할 데이터가 없습니다.
						ShowMessage("NoSelectData");
					}

					grdWorkResultPerMM.DataSource = dtLotWorkResultMM;
					break;
			}

            
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
			//공정
			Commons.CommonFunction.AddConditionProcessSegmentPopup("P_PROCESSSEGMENTID", 4.5, true, Conditions);
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

    }
}