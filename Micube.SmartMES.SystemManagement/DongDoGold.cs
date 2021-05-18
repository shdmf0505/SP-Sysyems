#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons;
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
    /// 프 로 그 램 명  : 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : 동도금 정보 조회
    /// 생    성    자  : 한주석
    /// 생    성    일  : 2021-01-20
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class DongDoGold : SmartConditionBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public DongDoGold()
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

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdmeasure.GridButtonItem = GridButtonItem.None;
            // 생성시간
            grdmeasure.View.AddTextBoxColumn("CREATEDTIME", 180)
                .SetTextAlignment(TextAlignment.Center);
            // LOTID
            grdmeasure.View.AddTextBoxColumn("LOTID", 180)
                  .SetTextAlignment(TextAlignment.Center);
            // 결과
            grdmeasure.View.AddTextBoxColumn("RESULT", 60)
                .SetLabel("INTERFACEOKNG")
                .SetTextAlignment(TextAlignment.Center);
            // 공정명
            grdmeasure.View.AddTextBoxColumn("PROCESSSEGMENTNAME", 200);
            // 공정상태
            grdmeasure.View.AddTextBoxColumn("PROCESSSTATE", 90)  
                .SetTextAlignment(TextAlignment.Center);
            // 작업장명
            grdmeasure.View.AddTextBoxColumn("AREANAME", 200);
            // WORKCOUNT
            grdmeasure.View.AddTextBoxColumn("WORKCOUNT", 70)
                .SetTextAlignment(TextAlignment.Center);
            // 오류명
            grdmeasure.View.AddTextBoxColumn("DESCRIPTION", 300)
                .SetLabel("ERRORCOMMENT");
            // IP 주소
            grdmeasure.View.AddTextBoxColumn("IPADDRESS", 300)
                .SetLabel("Sender Id");



            grdmeasure.View.SetIsReadOnly();

            grdmeasure.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>
        /// 화면에서 사용되는 이벤트를 초기화한다.
        /// </summary>
        public void InitializeEvent()
        {
            // TODO : 화면에서 사용할 이벤트 추가
            grdList.View.AddingNewRow += View_AddingNewRow;
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
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdList.GetChangedRows();

            ExecuteRule("SaveCodeClass", changed);
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

            DataTable dtCodeClass =  SqlExecuter.Query("SelectCopperPlating", "10001", values);

            if (dtCodeClass.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdmeasure.DataSource = dtCodeClass;
        }

        /// <summary>
        /// 조회조건 항목을 추가한다.
        /// </summary>
        protected override void InitializeCondition()
        {
            base.InitializeCondition();
            CommonFunction.AddConditionLotPopup("P_LOTID", 0.1, true, Conditions);

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
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdList.View.CheckValidation();

            DataTable changed = grdList.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
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