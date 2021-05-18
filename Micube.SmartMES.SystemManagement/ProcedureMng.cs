#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템관리 > 스케줄관리 > 프로시저 실행
    /// 업  무  설  명  : 프로시저를 실행시킨다
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2020-11-04
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ProcedureMng : SmartConditionBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가


        #endregion

        #region 생성자

        public ProcedureMng()
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
            InitializeGrid();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            gridList2.GridButtonItem = GridButtonItem.None;

            // 순번
            gridList2.View.AddTextBoxColumn("SEQ", 150)
                .SetValidationIsRequired();
            // 대상
            gridList2.View.AddTextBoxColumn("TARGETCOLUMN", 200);

            // 완료여부
            gridList2.View.AddTextBoxColumn("ISCOMPLETION", 200);

            gridList2.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            btn_excute.Click += Btn_excute_Click;
        }

        private void Btn_excute_Click(object sender, EventArgs e)
        {
            var values = Conditions.GetValues();
            DataTable dt = gridList2.DataSource as DataTable;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();


      
        

            // Table 을 조회 하는것이 아니라 API 를 통해 해당내용을 조회(Rule 호출 필요)
            MessageWorker worker = new MessageWorker("ExcuteProcedure");
            worker.SetBody(new MessageBody()
            {

                { "P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"] }
                , { "P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"] }
                , { "SEQEUNCE", "1" }
            });
            worker.Execute();

            dt.Rows[0]["ISCOMPLETION"] = "OK";


            Thread.Sleep(1000);

            TimeSpan t = new TimeSpan(2, 30, 0);
            worker = new MessageWorker("ExcuteProcedure");
            worker.SetBody(new MessageBody()
            {

                { "P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"] }
                , { "P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"] }
                , { "SEQEUNCE", "2" }
            });
            worker.SetTimeOut(t);
            worker.Execute();




            dt.Rows[1]["ISCOMPLETION"] = "OK";
            


            worker = new MessageWorker("ExcuteProcedure");
            worker.SetBody(new MessageBody()
            {

                { "P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"] }
                , { "P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"] }
                , { "SEQEUNCE", "3" }
            });
            worker.SetTimeOut(t);
            worker.Execute();
            dt.Rows[2]["ISCOMPLETION"] = "OK";
            worker = new MessageWorker("ExcuteProcedure");
            worker.SetBody(new MessageBody()
            {

                { "P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"] }
                , { "P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"] }
                , { "SEQEUNCE", "4" }
            });
            worker.SetTimeOut(t);
            worker.Execute();
            dt.Rows[3]["ISCOMPLETION"] = "OK";
            worker = new MessageWorker("ExcuteProcedure");
            worker.SetBody(new MessageBody()
            {

                { "P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"] }
                , { "P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"] }
                , { "SEQEUNCE", "5" }
            });
            worker.SetTimeOut(t);
            worker.Execute();
            dt.Rows[4]["ISCOMPLETION"] = "OK";
            worker = new MessageWorker("ExcuteProcedure");
            worker.SetBody(new MessageBody()
            {

                { "P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"] }
                , { "P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"] }
                , { "SEQEUNCE", "6" }
            });
            worker.SetTimeOut(t);
            worker.Execute();
            dt.Rows[5]["ISCOMPLETION"] = "OK";

            worker = new MessageWorker("ExcuteProcedure");
            worker.SetBody(new MessageBody()
            {

                { "P_PERIOD_PERIODFR", values["P_PERIOD_PERIODFR"] }
                , { "P_PERIOD_PERIODTO", values["P_PERIOD_PERIODTO"] }
                , { "SEQEUNCE", "7" }
            });
            worker.SetTimeOut(t);
            worker.Execute();
            dt.Rows[6]["ISCOMPLETION"] = "OK";

        }

        /// <summary>
        /// 코드그룹 리스트 그리드에서 추가 버튼 클릭 시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장 버튼을 클릭하면 호출한다.
        /// </summary>
        protected override void OnToolbarSaveClick()
        {

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
            values.Add("P_LANGUAGETYPE", UserInfo.Current.LanguageType);
            DataTable dtCodeClass = SqlExecuter.Query("SelectProcedure", "10001",values);



            gridList2.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
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

        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가
        /// <summary>
        /// 마스터 정보를 조회한다.
        /// </summary>
        private void LoadData()
        {

        }

        #endregion
    }
}