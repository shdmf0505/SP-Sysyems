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

namespace Micube.SmartMES.StandardInfo
{
    /// <summary>
    /// 프 로 그 램 명  : 기준정보 > Setup > 창고 Import 및 조회
    /// 업  무  설  명  : 창고정보를 Import및 관리한다.
    /// 생    성    자  : 강유라
    /// 생    성    일  : 2019-05-17
    /// 수  정  이  력  : 윤성원 2019-07-05 using 에 #region #endregion 추가
    /// 
    /// 
    /// </summary>
    public partial class WarehouseManagement : SmartConditionManualBaseForm
	{
        #region Local Variables

        // TODO : 화면에서 사용할 내부 변수 추가

        #endregion

        #region 생성자

        public WarehouseManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();

            // TODO : 컨트롤 초기화 로직 구성
            InitializeGrid();
            InitializeLocatorGrid();
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            this.grdWarehouse.GridButtonItem = GridButtonItem.Export;
            this.grdWarehouse.View.SetIsReadOnly();

            //회사
            this.grdWarehouse.View.AddTextBoxColumn("ENTERPRISEID", 150)
                .SetIsHidden();
            
            //공장
            this.grdWarehouse.View.AddTextBoxColumn("PLANTID", 150);

            //창고 ID
            this.grdWarehouse.View.AddTextBoxColumn("WAREHOUSEID", 150);

            //창고 명
            this.grdWarehouse.View.AddTextBoxColumn("WAREHOUSENAME", 200);

            //설명
            this.grdWarehouse.View.AddTextBoxColumn("DESCRIPTION", 150);

            //CODECLASSID 수정필
            //this.grdWarehouse.View.AddTextBoxColumn("WAREHOUSETYPE", 100);

            //위치
            this.grdWarehouse.View.AddTextBoxColumn("LOCATION", 100);

            //Locator 관리 여부
            this.grdWarehouse.View.AddTextBoxColumn("ISLOCATOR", 100);

            //유효 상태
            this.grdWarehouse.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                             .SetDefault("Valid")
                             .SetValidationIsRequired()
                             .SetTextAlignment(TextAlignment.Center);

            //생성자
            this.grdWarehouse.View.AddTextBoxColumn("CREATOR", 80)
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            //생성 일자
            this.grdWarehouse.View.AddTextBoxColumn("CREATEDTIME", 130)
                             .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            //수정자
            this.grdWarehouse.View.AddTextBoxColumn("MODIFIER", 80)
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            //수정 일자
            this.grdWarehouse.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                             .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            this.grdWarehouse.View.PopulateColumns();

        }

        private void InitializeLocatorGrid()
        {
            this.grdLocator.GridButtonItem = GridButtonItem.Export;
            this.grdLocator.View.SetIsReadOnly();

            //위치 ID
            this.grdLocator.View.AddTextBoxColumn("LOCATIONID", 150);

            //위치 명
            this.grdLocator.View.AddTextBoxColumn("LOCATIONNAME", 150);

            //유효 상태
            this.grdLocator.View.AddComboBoxColumn("VALIDSTATE", 60, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                             .SetDefault("Valid")
                             .SetValidationIsRequired()
                             .SetTextAlignment(TextAlignment.Center);

            //생성자
            this.grdLocator.View.AddTextBoxColumn("CREATOR", 80)
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            //생성 일자
            this.grdLocator.View.AddTextBoxColumn("CREATEDTIME", 130)
                             .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            //수정자
            this.grdLocator.View.AddTextBoxColumn("MODIFIER", 80)
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            //수정 일자
            this.grdLocator.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                             .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                             .SetIsReadOnly()
                             .SetTextAlignment(TextAlignment.Center);

            this.grdLocator.View.PopulateColumns();

        }

            #endregion

        #region Event

            /// <summary>        
            /// 이벤트 초기화
            /// </summary>
        public void InitializeEvent()
        {
            //Import 버튼을 클릭 했을 때 이벤트 
            this.btnImport.Click += BtnImport_Click;
            //grdWarehouse 그리드의 Focused Row가 바뀔때 이벤트  
            this.grdWarehouse.View.FocusedRowChanged += View_FocusedRowChanged;
        }

        /// <summary>
        /// grdWarehouse 그리드의 Focuse된 Row 의 창고 ID로 mm_location 테이블을 조회하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = grdWarehouse.View.GetFocusedDataRow();

            if (row == null) return;

            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);
            values.Add("P_WAREHOUSEID", row["WAREHOUSEID"]);

            DataTable dt = SqlExecuter.Query("SelectWarehouseLocation", "10001", values);

            grdLocator.DataSource = dt;
        }

        /// <summary>
        /// Import 버튼을 클릭 했을 때 이벤트 - 구현 안됨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImport_Click(object sender, EventArgs e)
        {
            this.ShowMessage("Import!");
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            // TODO : 저장 Rule 변경
            DataTable changed = grdWarehouse.GetChangedRows();

            ExecuteRule("RULEID", changed);

        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            grdLocator.View.ClearDatas();
            // TODO : 조회 SP 변경
            var values = Conditions.GetValues();
            values.Add("P_LANGUAGETYPE", Framework.UserInfo.Current.LanguageType);

            DataTable dt = await SqlExecuter.QueryAsync("SelectWarehouseManagement", "10001", values);

            if (dt.Rows.Count < 1) // 검색 조건에 해당하는 코드를 포함한 코드클래스가 없는 경우
            {
                ShowMessage("NoSelectData"); // 조회할 데이터가 없습니다.
            }

            grdWarehouse.DataSource = dt;
        }

        protected override void InitializeCondition()
        {
            base.InitializeCondition();

            // TODO : 조회조건 추가 구성이 필요한 경우 사용
        }

        protected override void InitializeConditionControls()
        {
            base.InitializeConditionControls();

            // TODO : 조회조건의 컨트롤에 기능 추가가 필요한 경우 사용
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            // TODO : 유효성 로직 변경
            grdWarehouse.View.CheckValidation();

            DataTable changed = grdWarehouse.GetChangedRows();
            
            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        // TODO : 화면에서 사용할 내부 함수 추가

        #endregion

    }
}
