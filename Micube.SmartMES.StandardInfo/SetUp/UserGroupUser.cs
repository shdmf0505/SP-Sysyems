#region using

using DevExpress.XtraEditors.Repository;

using Micube.Framework.Net;
using Micube.Framework;
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
    /// 프 로 그 램 명  : 기준정보 > 사용자그룹 - 사용자 매핑
    /// 업  무  설  명  : 사용자그룹 - 사용자 매핑 정보를 관리한다.
    /// 생    성    자  : 장선미
    /// 생    성    일  : 2020-01-02
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class UserGroupUser : SmartConditionManualBaseForm
    {
        #region Local Variables

        private DataTable _saveList; // 팝업에서 저장한 데이터
        private DataTable _realSaveList;  //실제 db에 저장할 데이터
        private DataTable _searchList; // 초기 비교 데이터

        private DataTable _mappingDataSource; // 팝업 그리드와 원래 그리드를 비교하기 위한 변수

        #endregion

        #region 생성자

        public UserGroupUser()
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
        private void InitializeGridUserGroup()
        {
            grdUserGroup.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdUserGroup.View.SetSortOrder("USERGROUPID");
            grdUserGroup.View.SetAutoFillColumn("USERGROUPNAME");

            grdUserGroup.View.AddTextBoxColumn("USERGROUPID", 150);
            grdUserGroup.View.AddTextBoxColumn("USERGROUPNAME", 180);

            grdUserGroup.View.SetIsReadOnly();
            grdUserGroup.View.SetSortOrder("USERID");

            grdUserGroup.View.OptionsCustomization.AllowFilter = true;
            grdUserGroup.View.OptionsCustomization.AllowSort = true;

            grdUserGroup.View.PopulateColumns();
        }

        /// <summary>        
        /// 사용자 그룹 - 사용자 맵핑 그리드 초기화
        /// </summary>
        private void InitializeGridUserGroupUser()
        {
            grdUserGroupUser.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdUserGroupUser.View.SetSortOrder("USERID");

            grdUserGroupUser.View.AddTextBoxColumn("USERID", 150);
            grdUserGroupUser.View.AddTextBoxColumn("USERNAME", 200);
            grdUserGroupUser.View.AddTextBoxColumn("DEPARTMENT", 150);
            grdUserGroupUser.View.AddTextBoxColumn("POSITION", 100);
            grdUserGroupUser.View.AddTextBoxColumn("EMAILADDRESS", 150);
            grdUserGroupUser.View.AddTextBoxColumn("CELLPHONENUMBER", 100);

            grdUserGroupUser.View.SetIsReadOnly();

            grdUserGroupUser.View.OptionsCustomization.AllowFilter = true;
            grdUserGroupUser.View.OptionsCustomization.AllowSort = true;

            grdUserGroupUser.GridButtonItem = GridButtonItem.Export;
            grdUserGroupUser.View.PopulateColumns();
        }

        /// <summary>        
        /// 팝업 초기화
        /// </summary>
        private void UserSearchPopup(string userGroupId)
        {
            // 팝업 컬럼 설정
            var userListPopup = CreateSelectPopup("USERID", new SqlQuery("GetUser", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                .SetMultiGrid(true, new SqlQuery("GetUserByUserGroupId", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"USERGROUPID={userGroupId}"))
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
            userListPopup.GridColumns.AddTextBoxColumn("USERGROUPID", 150)
                .SetIsHidden();

            DataTable mappingTable = grdUserGroupUser.DataSource as DataTable;

            IEnumerable<DataRow> selectedDatas = ShowPopup(userListPopup, mappingTable.Rows.Cast<DataRow>().Where(m => !grdUserGroupUser.View.IsDeletedRow(m)));
            if (selectedDatas == null) { return; }
            //else if (selectedDatas.Count<DataRow>() == 0) { return; }

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
            grdUserGroupUser.SetDataSourceRemainRowStatus( //DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
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
            Load += UserGroupUser_Load;
            grdUserGroup.View.FocusedRowChanged += View_FocusedRowChanged;
            btnUserSearch.Click += BtnUserSearch_Click;
            grdUserGroup.ToolbarRefresh += GrdUserGroup_ToolbarRefresh;
        }

        private void UserGroupUser_Load(object sender, EventArgs e)
        {
            InitializeGridUserGroup(); // 사용자 그룹 그리드(메인) 초기화
            InitializeGridUserGroupUser(); // 사용자 그룹에 매핑된 사용자 그리드(서브) 초기화

            LoadDataGridUserGroup(); // 사용자 그룹 그리드(메인) 데이터 불러오기
        }

        //사용자 그룹 선택 변경시.
        private async void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdUserGroup.View.FocusedRowHandle < 0)
                return;

            await OnSearchAsync();

            _searchList = (DataTable)grdUserGroupUser.DataSource;
        }

        private void BtnUserSearch_Click(object sender, EventArgs e)
        {
            string strUserGroupId = grdUserGroup.View.GetRowCellValue(grdUserGroup.View.FocusedRowHandle, "USERGROUPID").ToString();

            UserSearchPopup(strUserGroupId);
        }

        private void GrdUserGroup_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusUserGroup = grdUserGroup.View.FocusedRowHandle;
                grdUserGroup.ShowWaitArea();
                LoadDataGridUserGroup();
                grdUserGroup.View.FocusedRowHandle = 0;
                grdUserGroup.View.UnselectRow(beforeFocusUserGroup);
                grdUserGroup.View.SelectRow(0);
                focusedRowChanged();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdUserGroup.CloseWaitArea();
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

            DataTable changed = grdUserGroupUser.GetChangedRows();
            DataRow focusRow = grdUserGroup.View.GetFocusedDataRow();
            string usergroupid = focusRow["USERGROUPID"].ToString();

            foreach (DataRow row in changed.Rows)
            {
                row["USERGROUPID"] = usergroupid;
            }

            ExecuteRule("SaveUserGroupUser", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {

            if (grdUserGroup.View.DataRowCount <= 0)
                return;

            await base.OnSearchAsync();

            string userGroupId = grdUserGroup.View.GetRowCellValue(grdUserGroup.View.FocusedRowHandle, "USERGROUPID").ToString();

            var values = Conditions.GetValues();
            values.Add("p_userGroupId", userGroupId);
            grdUserGroupUser.DataSource = _mappingDataSource = await QueryAsync("SelectUserGroupUserSearch", "10001", values);

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
            grdUserGroupUser.View.CheckValidation();

            DataTable changed = grdUserGroupUser.GetChangedRows();

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
            if (grdUserGroup.View.DataRowCount <= 0)
                return;

            string userGroupId = grdUserGroup.View.GetRowCellValue(grdUserGroup.View.FocusedRowHandle, "USERGROUPID").ToString();

            var values = Conditions.GetValues();
            values.Add("p_userGroupId", userGroupId);

            grdUserGroupUser.DataSource = SqlExecuter.Query("SelectUserGroupUserSearch", "10001", values);
        }

        /// <summary>        
        /// 사용자 그룹 그리드 데이터 로드
        /// </summary>
        private async void LoadDataGridUserGroup()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_validState", "Valid");
            param.Add("p_languageType", UserInfo.Current.LanguageType);
            grdUserGroup.DataSource = await QueryAsync("SelectUserGroupMapping", "10001", param);

            if (grdUserGroup.View.DataRowCount > 0)
            {
                grdUserGroup.View.SelectRow(0);
            }
        }

        /// <summary>        
        /// 사용자 그룹 - 사용자 맵핑 그리드 데이터 로드
        /// </summary>
        private void LoadDataGridUserGroupUser()
        {
            grdUserGroupUser.DataSource = _saveList;
        }

        #endregion
    }
}
