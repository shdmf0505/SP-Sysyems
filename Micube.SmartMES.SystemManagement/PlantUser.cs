#region using

using DevExpress.XtraEditors.Repository;

using Micube.Framework.Net;
using Micube.Framework;
using Micube.Framework.SmartControls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 사용자 관리 >사이트 - 사용자 매핑
    /// 업  무  설  명  : 사이트 - 사용자 매핑 정보를 관리한다.
    /// 생    성    자  : 장선미
    /// 생    성    일  : 2019-10-23
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class PlantUser : SmartConditionManualBaseForm
    {
        #region Local Variables

        private DataTable _saveList; // 팝업에서 저장한 데이터
        private DataTable _realSaveList;  //실제 db에 저장할 데이터
        private DataTable _searchList; // 초기 비교 데이터

        private DataTable _mappingDataSource; // 팝업 그리드와 원래 그리드를 비교하기 위한 변수

        #endregion

        #region 생성자

        public PlantUser()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            InitializeEvent();
        }

        // dtRealSaveList (db에 실제 저장할 데이터 테이블) 초기화 // state 컬럼 추가 
        private void InitializeSaveDataTable()
        {
            _realSaveList = new DataTable();

            foreach (DataColumn col in _saveList.Columns)
            {
                _realSaveList.Columns.Add(col.ColumnName, col.DataType);
            }

            _realSaveList.Columns.Add("_STATE_", typeof(string));
        }

        /// <summary>        
        /// 사용자 그룹 그리드 초기화
        /// </summary>
        private void InitializeGridPlant()
        {
            grdPlant.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdPlant.View.SetSortOrder("PLANTID");
            grdPlant.View.SetAutoFillColumn("PLANTNAME");

            grdPlant.View.AddTextBoxColumn("PLANTID", 150);
            grdPlant.View.AddTextBoxColumn("PLANTNAME", 180);

            grdPlant.View.SetIsReadOnly();
            grdPlant.View.SetSortOrder("ENTERPRISEID");

            grdPlant.View.OptionsCustomization.AllowFilter = true;
            grdPlant.View.OptionsCustomization.AllowSort = true;

            grdPlant.View.PopulateColumns();
        }

        /// <summary>        
        /// 사용자 그룹 - 사용자 맵핑 그리드 초기화
        /// </summary>
        private void InitializeGridPlantUser()
        {
            grdPlantUser.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdPlantUser.View.SetSortOrder("USERID");

            grdPlantUser.View.AddTextBoxColumn("USERID", 150);
            grdPlantUser.View.AddTextBoxColumn("USERNAME", 200);
            grdPlantUser.View.AddTextBoxColumn("DEPARTMENT", 150);
            grdPlantUser.View.AddTextBoxColumn("POSITION", 100);
            grdPlantUser.View.AddTextBoxColumn("EMAILADDRESS", 150);
            grdPlantUser.View.AddTextBoxColumn("CELLPHONENUMBER", 100);

            grdPlantUser.View.SetIsReadOnly();

            grdPlantUser.View.OptionsCustomization.AllowFilter = true;
            grdPlantUser.View.OptionsCustomization.AllowSort = true;

            grdPlantUser.GridButtonItem = GridButtonItem.Export;
            grdPlantUser.View.PopulateColumns();
        }

        /// <summary>        
        /// 팝업 초기화
        /// </summary>
        private void UserSearchPopup(string plantId)
        {
            // 팝업 컬럼 설정
            var userListPopup = CreateSelectPopup("USERID", new SqlQuery("GetUser", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                //.SetMultiGrid(true, new SqlQuery("GetUserByUserClassId", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"USERCLASSID={userClassId}"))
                                                .SetMultiGrid(true, new SqlQuery("GetUserByPlant", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"PLANTID={plantId}"))
                                                .SetPopupLayout("USERLIST", PopupButtonStyles.Ok_Cancel, true, false)
                                                .SetPopupResultCount(0)
                                                .SetPopupLayoutForm(1200, 700, System.Windows.Forms.FormBorderStyle.FixedToolWindow);

            // 팝업 조회조건
            userListPopup.Conditions.AddComboBox("CONDALLCOND", new SqlQuery("GetCodeList", "00001", "CODECLASSID=UserClassUserPopupCboData", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                     .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberOnly, false)
                                     .SetEmptyItem();

            userListPopup.Conditions.AddTextBox("TXTALLCOND")
                .SetLabel("");

            // 팝업 그리드
            userListPopup.SetIsReadOnly();
            userListPopup.GridColumns.AddTextBoxColumn("USERID", 90)
                .SetValidationKeyColumn();
            userListPopup.GridColumns.AddTextBoxColumn("USERNAME", 100);
            userListPopup.GridColumns.AddTextBoxColumn("DEPARTMENT", 150);
            userListPopup.GridColumns.AddTextBoxColumn("POSITION", 70);
            userListPopup.GridColumns.AddTextBoxColumn("EMAILADDRESS", 150);
            userListPopup.GridColumns.AddTextBoxColumn("CELLPHONENUMBER", 100);
            userListPopup.GridColumns.AddTextBoxColumn("USERCLASSID", 150)
                .SetIsHidden();

            DataTable mappingTable = grdPlantUser.DataSource as DataTable;

            IEnumerable<DataRow> selectedDatas = ShowPopup(userListPopup, mappingTable.Rows.Cast<DataRow>().Where(m => !grdPlantUser.View.IsDeletedRow(m)));
            if (selectedDatas == null) { return; }
            else if (selectedDatas.Count<DataRow>() == 0) { return; }

            //if (_MappingDataSource.Columns.Count == 0)
            //{
            //    _MappingDataSource.Columns.Add("USERCLASSID");
            //    _MappingDataSource.Columns.Add("USERID");
            //    _MappingDataSource.Columns.Add("USERNAME");
            //    _MappingDataSource.Columns.Add("DEPARTMENT");
            //    _MappingDataSource.Columns.Add("POSITION");
            //    _MappingDataSource.Columns.Add("EMAILADDRESS");
            //    _MappingDataSource.Columns.Add("CELLPHONENUMBER");
            //}
            grdPlantUser.SetDataSourceRemainRowStatus( //DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
                DataSourceHelper.MappingChanged(_mappingDataSource, selectedDatas, new List<string>() { "USERID" }) //원래것과 매핑결과를 비교하여 새로운 DataSource를 생성해 준다
            );
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>        
        public void InitializeEvent()
        {
            Load += PlantUser_Load;
            grdPlant.View.FocusedRowChanged += View_FocusedRowChanged;
            btnUserSearch.Click += BtnUserSearch_Click;
            grdPlant.ToolbarRefresh += GrdPlant_ToolbarRefresh;
        }

        private void PlantUser_Load(object sender, EventArgs e)
        {
            InitializeGridPlant(); // 사용자 그룹 그리드(메인) 초기화
            InitializeGridPlantUser(); // 사용자 그룹에 매핑된 사용자 그리드(서브) 초기화

            LoadDataGridPlant(); // 사용자 그룹 그리드(메인) 데이터 불러오기
        }

        //사용자 그룹 선택 변경시.
        private async void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdPlant.View.FocusedRowHandle < 0)
                return;

            await OnSearchAsync();

            _searchList = (DataTable)grdPlantUser.DataSource;
        }

        private void BtnUserSearch_Click(object sender, EventArgs e)
        {
            string strPlantID = grdPlant.View.GetRowCellValue(grdPlant.View.FocusedRowHandle, "PLANTID").ToString();

            UserSearchPopup(strPlantID);
        }

        private void GrdPlant_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusUserClass = grdPlant.View.FocusedRowHandle;
                grdPlant.ShowWaitArea();
                LoadDataGridPlant();
                grdPlant.View.FocusedRowHandle = 0;
                grdPlant.View.UnselectRow(beforeFocusUserClass);
                grdPlant.View.SelectRow(0);
                focusedRowChanged();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdPlant.CloseWaitArea();
            }
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdPlantUser.GetChangedRows();
            DataRow focusRow = grdPlant.View.GetFocusedDataRow();
            string strPlantID = focusRow["PLANTID"].ToString();
            string strEnterpriseID = focusRow["ENTERPRISEID"].ToString();

            foreach (DataRow row in changed.Rows)
            {
                row["PLANTID"] = strPlantID;
                row["ENTERPRISEID"] = strEnterpriseID;
            }

            ExecuteRule("SavePlantUser", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {

            if (grdPlant.View.DataRowCount <= 0)
                return;

            await base.OnSearchAsync();

            string strPlantID = grdPlant.View.GetRowCellValue(grdPlant.View.FocusedRowHandle, "PLANTID").ToString();

            var values = Conditions.GetValues();
            values.Add("p_plantId", strPlantID);
            //grdUserClassUser.DataSource = _mappingDataSource = await ProcedureAsync("usp_com_selectUserClassUser_search", values);
            grdPlantUser.DataSource = _mappingDataSource = await QueryAsync("SelectPlantUserSearch", "10001", values);

            _saveList = null;
            _realSaveList = null;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            grdPlantUser.View.CheckValidation();

            DataTable changed = grdPlantUser.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        private void focusedRowChanged()
        {
            if (grdPlant.View.DataRowCount <= 0)
                return;

            string strPlantID = grdPlant.View.GetRowCellValue(grdPlant.View.FocusedRowHandle, "PLANTID").ToString();

            var values = Conditions.GetValues();
            values.Add("p_plantId", strPlantID);

            //grdUserClassUser.DataSource = SqlExecuter.Procedure("usp_com_selectUserClassUser_search", values);
            grdPlantUser.DataSource = SqlExecuter.Query("SelectPlantUserSearch", "10001", values);
        }

        /// <summary>        
        /// 사용자 그룹 그리드 데이터 로드
        /// </summary>
        private async void LoadDataGridPlant()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_validState", "Valid");
            param.Add("p_languageType", UserInfo.Current.LanguageType);
            grdPlant.DataSource = await QueryAsync("SelectPlantMapping", "10001", param);

            if (grdPlant.View.DataRowCount > 0)
            {
                grdPlant.View.SelectRow(0);
            }
        }

        /// <summary>        
        /// 사용자 그룹 - 사용자 맵핑 그리드 데이터 로드
        /// </summary>
        private void LoadDataGridPlantUser()
        {
            grdPlantUser.DataSource = _saveList;
        }

        #endregion
    }
}