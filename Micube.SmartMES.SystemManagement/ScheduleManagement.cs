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

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 스케쥴 관리 > 스케쥴 관리
    /// 업  무  설  명  : 스케쥴 정보를 관리한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2019-10-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ScheduleManagement : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public ScheduleManagement()
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
            InitializeGridScheduleManagement();
        }

        /// <summary>        
        /// 그리드를 초기화한다.
        /// </summary>
        private void InitializeGridScheduleManagement()
        {
            // TODO : 그리드 초기화 로직 추가
            grdScheduleManagement.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdScheduleManagement.View.SetSortOrder("JOBID");

            // Job ID
            grdScheduleManagement.View.AddTextBoxColumn("JOBID", 150);
            // Rule ID
            grdScheduleManagement.View.AddTextBoxColumn("RULEID", 350).SetLabel("GRIDRULEID");
            // Cron Context
            grdScheduleManagement.View.AddTextBoxColumn("CRONCONTEXT", 450);
            // Timeout
            grdScheduleManagement.View.AddTextBoxColumn("TIMEOUT", 80).SetDefault("60");

            grdScheduleManagement.View.OptionsCustomization.AllowFilter = false;
            grdScheduleManagement.View.OptionsCustomization.AllowSort = false;
            grdScheduleManagement.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            this.Load += ScheduleManagement_Load;
        }

        private async void ScheduleManagement_Load(object sender, EventArgs e)
        {
            await this.OnSearchAsync();
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
            DataTable changed = grdScheduleManagement.GetChangedRows();

            MessageWorker worker = new MessageWorker("SaveScheduleManagement");
            worker.SetBody(new MessageBody()
            {
                { "list", changed }
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

            // Table 을 조회 하는것이 아니라 API 를 통해 해당내용을 조회(Rule 호출 필요)
            MessageWorker worker = new MessageWorker("SearchScheduleManagement");
            worker.SetBody(new MessageBody()
            {
                { "JOBID", values["JOBID"] }
                , { "RULEID", values["RULEID"] }
            });

            var result = worker.Execute<DataTable>();
            var resultTable = result.GetResultSet();

            if (resultTable.Rows.Count > 0)
            {
                grdScheduleManagement.DataSource = resultTable;
            }
            else
            {
                grdScheduleManagement.DataSource = CreateEmptyTable();
            }
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용

            // Job ID
            Conditions.AddTextBox("JOBID")
                .SetLabel("JOBID");

            // Rule ID
            Conditions.AddTextBox("RULEID")
                .SetLabel("CONDRULEID");
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
            grdScheduleManagement.View.CheckValidation();

            DataTable changed = grdScheduleManagement.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        private DataTable CreateEmptyTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("JOBID");
            table.Columns.Add("RULEID");
            table.Columns.Add("CRONCONTEXT");
            table.Columns.Add("TIMEOUT");
            return table;
        }

        #endregion
    }
}
