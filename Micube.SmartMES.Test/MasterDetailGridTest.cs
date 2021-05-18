#region using

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
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

namespace Micube.SmartMES.Test
{
    /// <summary>
    /// 프 로 그 램 명  : ex> 시스템관리 > 코드 관리 > 코드그룹 정보
    /// 업  무  설  명  : ex> 시스템에서 공통으로 사용되는 코드그룹 정보를 관리한다.
    /// 생    성    자  : 홍길동
    /// 생    성    일  : 2019-05-14
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MasterDetailGridTest : SmartConditionManualBaseForm
    {
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가
        string _Text;                                       // 설명
        SmartBandedGrid grdList = new SmartBandedGrid();    // Main Grid

        #endregion

        #region 생성자

        public MasterDetailGridTest()
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
            GetDataSource();
        }

        /// <summary>        
        /// 코드그룹 리스트 그리드를 초기화한다.
        /// </summary>
        private void InitializeGrid()
        {
            // TODO : 그리드 초기화 로직 추가
            grdMasterDetail.GridButtonItem = GridButtonItem.None;
            grdMasterDetail.ShowStatusBar = false;

            grdMasterDetail.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            grdMasterDetail.View.SetIsReadOnly();

            grdMasterDetail.View.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsHidden();
            grdMasterDetail.View.AddTextBoxColumn("PRODUCTDEFID", 120);
            grdMasterDetail.View.AddTextBoxColumn("PRODUCTDEFVERSION", 90);
            grdMasterDetail.View.AddTextBoxColumn("PRODUCTDEFNAME", 200);
            grdMasterDetail.View.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            grdMasterDetail.View.AddTextBoxColumn("CONSUMABLEDEFVERSION", 90);
            grdMasterDetail.View.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            grdMasterDetail.View.AddSpinEditColumn("NOTINPUTPCSQTY", 90);
            grdMasterDetail.View.AddSpinEditColumn("NOTINPUTPNLQTY", 90);
            grdMasterDetail.View.AddSpinEditColumn("MATERIALREQUIREMENTQTY", 90);


            grdMasterDetail.View.PopulateColumns();

            grdMasterDetail.View.OptionsDetail.ShowDetailTabs = false;
            grdMasterDetail.View.OptionsDetail.SmartDetailExpand = false;


            SmartGridControl mainGrid = grdMasterDetail.GridControl;

            SmartBandedGridView detailView = new SmartBandedGridView(mainGrid);
            mainGrid.LevelTree.Nodes.Add("MasterDetail", detailView);
            detailView.ViewCaption = "Detail View";

            detailView.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            detailView.SetIsReadOnly();

            detailView.AddTextBoxColumn("KEYCOLUMN", 100)
                .SetIsHidden();
            detailView.AddTextBoxColumn("PRODUCTDEFID", 120)
                .SetIsHidden();
            detailView.AddTextBoxColumn("PRODUCTDEFVERSION", 90)
                .SetIsHidden();
            detailView.AddTextBoxColumn("CONSUMABLEDEFID", 120);
            detailView.AddTextBoxColumn("CONSUMABLEDEFVERSION", 90);
            detailView.AddTextBoxColumn("CONSUMABLEDEFNAME", 200);
            detailView.AddSpinEditColumn("QTY", 90);


            detailView.PopulateColumns();

            detailView.OptionsDetail.SmartDetailExpand = false;
        }

        private void GetDataSource()
        {
            DataTable master = new DataTable();
            master.Columns.Add("KEYCOLUMN", typeof(string));
            master.Columns.Add("PRODUCTDEFID", typeof(string));
            master.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            master.Columns.Add("PRODUCTDEFNAME", typeof(string));
            master.Columns.Add("CONSUMABLEDEFID", typeof(string));
            master.Columns.Add("CONSUMABLEDEFVERSION", typeof(string));
            master.Columns.Add("CONSUMABLEDEFNAME", typeof(string));
            master.Columns.Add("NOTINPUTPCSQTY", typeof(decimal));
            master.Columns.Add("NOTINPUTPNLQTY", typeof(decimal));
            master.Columns.Add("MATERIALREQUIREMENTQTY", typeof(decimal));

            master.Rows.Add("1042093B1|A1", "1042093B1", "A1", "B S40N01BB RF CAMERA(4L)-M2/3L", "Consumable1-1", "A1", "R1208D500NX(II) [500mm*100M]", 100000, 2800, 100);
            master.Rows.Add("1042076A6|A1", "1042076A6", "A1", "C2D2 GW2211L RF(4L)-M2/3L", "Consumable2-1", "A1", "E1210D500NS [500mm*100M]", 35000, 400, 32);
            master.Rows.Add("1042060B1|A1", "1042060B1", "A1", "ORAT-0026 RF CAMERA(4L)-M2/3L", "Consumable3-1", "A1", "R1210D500NX(II) [500mm*100M]", 20000, 250, 23);


            DataTable detail = new DataTable();
            detail.Columns.Add("KEYCOLUMN", typeof(string));
            detail.Columns.Add("PRODUCTDEFID", typeof(string));
            detail.Columns.Add("PRODUCTDEFVERSION", typeof(string));
            detail.Columns.Add("CONSUMABLEDEFID", typeof(string));
            detail.Columns.Add("CONSUMABLEDEFVERSION", typeof(string));
            detail.Columns.Add("CONSUMABLEDEFNAME", typeof(string));
            detail.Columns.Add("QTY", typeof(decimal));

            detail.Rows.Add("1042093B1|A1", "1042093B1", "A1", "Consumable1-2", "A1", "DS-7402 BS DFP 1027 [500mm*100M]", 200);
            detail.Rows.Add("1042093B1|A1", "1042093B1", "A1", "Consumable1-3", "A1", "DS-7402 BS DFP 1035 [500mm*100M]", 200);
            detail.Rows.Add("1042093B1|A1", "1042093B1", "A1", "Consumable1-4", "A1", "ED CO.T12 [500mm*950M]", 1000);
            detail.Rows.Add("1042093B1|A1", "1042093B1", "A1", "Consumable1-5", "A1", "HGCS-A305L(Y) [500mm*200M]", 300);
            detail.Rows.Add("1042093B1|A1", "1042093B1", "A1", "Consumable1-6", "A1", "R1210D500NX(II) [500mm*100M]", 250);


            DataSet dataSource = new DataSet();
            dataSource.Tables.Add(master);
            dataSource.Tables.Add(detail);

            dataSource.Relations.Add("MasterDetail", master.Columns["KEYCOLUMN"], detail.Columns["KEYCOLUMN"]);

            grdMasterDetail.DataSource = dataSource.Tables[0];
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