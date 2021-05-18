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
    /// 프 로 그 램 명  : 시스템 관리 > 사용자 관리 > 사용자그룹 - 사용자 매핑
    /// 업  무  설  명  : 사용자그룹 - 사용자 매핑 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class UserClassUser : SmartConditionManualBaseForm
    {
        #region Local Variables

        private DataTable _saveList; // 팝업에서 저장한 데이터
        private DataTable _realSaveList;  //실제 db에 저장할 데이터
        private DataTable _searchList; // 초기 비교 데이터

        private DataTable _mappingDataSource; // 팝업 그리드와 원래 그리드를 비교하기 위한 변수

        #endregion

        #region 생성자

        public UserClassUser()
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
        private void InitializeGridUserClass()
        {
            grdUserClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdUserClass.View.SetSortOrder("USERCLASSID");
            grdUserClass.View.SetAutoFillColumn("USERCLASSNAME");

            grdUserClass.View.AddTextBoxColumn("USERCLASSID", 150);
            grdUserClass.View.AddTextBoxColumn("USERCLASSNAME", 180);

            grdUserClass.View.SetIsReadOnly();
            grdUserClass.View.SetSortOrder("USERID");

            grdUserClass.View.OptionsCustomization.AllowFilter = true;
            grdUserClass.View.OptionsCustomization.AllowSort = true;

            grdUserClass.View.PopulateColumns();
        }

        /// <summary>        
        /// 사용자 그룹 - 사용자 맵핑 그리드 초기화
        /// </summary>
        private void InitializeGridUserClassUser()
        {
            grdUserClassUser.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdUserClassUser.View.SetSortOrder("USERID");

            grdUserClassUser.View.AddTextBoxColumn("USERID", 150);
            grdUserClassUser.View.AddTextBoxColumn("USERNAME", 200);
            grdUserClassUser.View.AddTextBoxColumn("DEPARTMENT", 150);
            grdUserClassUser.View.AddTextBoxColumn("POSITION", 100);
            grdUserClassUser.View.AddTextBoxColumn("EMAILADDRESS", 150);
            grdUserClassUser.View.AddTextBoxColumn("CELLPHONENUMBER", 100);

            grdUserClassUser.View.SetIsReadOnly();

            grdUserClassUser.View.OptionsCustomization.AllowFilter = true;
            grdUserClassUser.View.OptionsCustomization.AllowSort = true;

            grdUserClassUser.GridButtonItem = GridButtonItem.Export;
            grdUserClassUser.View.PopulateColumns();
        }

        /// <summary>        
        /// 팝업 초기화
        /// </summary>
        private void UserSearchPopup(string userClassId)
        {
            // 팝업 컬럼 설정
            var userListPopup = CreateSelectPopup("USERID", new SqlQuery("GetUser", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                                                .SetMultiGrid(true, new SqlQuery("GetUserByUserClassId", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}", $"USERCLASSID={userClassId}"))
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

            DataTable mappingTable = grdUserClassUser.DataSource as DataTable;

            IEnumerable<DataRow> selectedDatas = ShowPopup(userListPopup, mappingTable.Rows.Cast<DataRow>().Where(m => !grdUserClassUser.View.IsDeletedRow(m)));
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
            grdUserClassUser.SetDataSourceRemainRowStatus( //DataTable의 RowStatus 즉 추가, 삭제, 수정 형태를 유지해주는 메소드
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
            Load += UserClassUser_Load;
            grdUserClass.View.FocusedRowChanged += View_FocusedRowChanged;
            btnUserSearch.Click += BtnUserSearch_Click;
            grdUserClass.ToolbarRefresh += GrdUserClass_ToolbarRefresh;
        }

        private void UserClassUser_Load(object sender, EventArgs e)
        {
            InitializeGridUserClass(); // 사용자 그룹 그리드(메인) 초기화
            InitializeGridUserClassUser(); // 사용자 그룹에 매핑된 사용자 그리드(서브) 초기화

            LoadDataGridUserClass(); // 사용자 그룹 그리드(메인) 데이터 불러오기
        }

        //사용자 그룹 선택 변경시.
        private async void View_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdUserClass.View.FocusedRowHandle < 0)
                return;

            await OnSearchAsync();

            _searchList = (DataTable)grdUserClassUser.DataSource;
        }

        private void BtnUserSearch_Click(object sender, EventArgs e)
        {
            string strUserClassId = grdUserClass.View.GetRowCellValue(grdUserClass.View.FocusedRowHandle, "USERCLASSID").ToString();
            //UserClassUserMapping_Popup childForm = new UserClassUserMapping_Popup(strUserClassId);
            //childForm.ShowDialog();
            //dtSaveList = childForm._dtSaveList;
            //if (childForm.DialogResult == DialogResult.OK)
            //{
            //    dtSaveList = childForm._dtSaveList;

            //    dtSaveList.AcceptChanges();

            //    grdUserClassUser.DataSource = dtSaveList;
            //}

            UserSearchPopup(strUserClassId);
        }

        private void GrdUserClass_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusUserClass = grdUserClass.View.FocusedRowHandle;
                grdUserClass.ShowWaitArea();
                LoadDataGridUserClass();
                grdUserClass.View.FocusedRowHandle = 0;
                grdUserClass.View.UnselectRow(beforeFocusUserClass);
                grdUserClass.View.SelectRow(0);
                focusedRowChanged();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdUserClass.CloseWaitArea();
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

            DataTable changed = grdUserClassUser.GetChangedRows();
            DataRow focusRow = grdUserClass.View.GetFocusedDataRow();
            string userclassid = focusRow["USERCLASSID"].ToString();

            foreach (DataRow row in changed.Rows)
            {
                row["USERCLASSID"] = userclassid;
            }

            ExecuteRule("SaveUserClassUser", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {

            if (grdUserClass.View.DataRowCount <= 0)
                return;

            await base.OnSearchAsync();

            string userClassId = grdUserClass.View.GetRowCellValue(grdUserClass.View.FocusedRowHandle, "USERCLASSID").ToString();

            var values = Conditions.GetValues();
            values.Add("p_userClassId", userClassId);
            //grdUserClassUser.DataSource = _mappingDataSource = await ProcedureAsync("usp_com_selectUserClassUser_search", values);
            grdUserClassUser.DataSource = _mappingDataSource = await QueryAsync("SelectUserClassUserSearch", "10001", values);

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
            grdUserClassUser.View.CheckValidation();

            DataTable changed = grdUserClassUser.GetChangedRows();

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
            if (grdUserClass.View.DataRowCount <= 0)
                return;

            string userClassId = grdUserClass.View.GetRowCellValue(grdUserClass.View.FocusedRowHandle, "USERCLASSID").ToString();

            var values = Conditions.GetValues();
            values.Add("p_userClassId", userClassId);

            //grdUserClassUser.DataSource = SqlExecuter.Procedure("usp_com_selectUserClassUser_search", values);
            grdUserClassUser.DataSource = SqlExecuter.Query("SelectUserClassUserSearch", "10001", values);
        }

        /// <summary>        
        /// 사용자 그룹 그리드 데이터 로드
        /// </summary>
        private async void LoadDataGridUserClass()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_validState", "Valid");
            param.Add("p_languageType", UserInfo.Current.LanguageType);
            //grdUserClass.DataSource = await ProcedureAsync("usp_com_selectUserClass_mapping", param);
            grdUserClass.DataSource = await QueryAsync("SelectUserClassMapping", "10001", param);

            if (grdUserClass.View.DataRowCount > 0)
            {
                grdUserClass.View.SelectRow(0);
            }
        }

        /// <summary>        
        /// 사용자 그룹 - 사용자 맵핑 그리드 데이터 로드
        /// </summary>
        private void LoadDataGridUserClassUser()
        {
            grdUserClassUser.DataSource = _saveList;
        }

        #endregion
    }
}