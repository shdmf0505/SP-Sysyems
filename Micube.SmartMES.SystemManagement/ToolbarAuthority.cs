#region using

using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraTreeList.Nodes;

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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Micube.SmartMES.Commons.Controls;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 메뉴 관리 > 툴바 권한 정보
    /// 업  무  설  명  : 툴바 권한 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class ToolbarAuthority : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public ToolbarAuthority()
        {
            InitializeComponent();

        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            smartGroupBox1.GridButtonItem = GridButtonItem.Refresh;

            InitializeEvent();
        }

        /// <summary>        
        /// 트리 초기화
        /// </summary>
        private void InitializeMenu()
        {
            treeMenu.SetResultCount(1);
            treeMenu.SetIsReadOnly();
            treeMenu.SetEmptyRoot("Root", "*");
            treeMenu.SetMember("MENUNAME", "MENUID", "PARENTMENUID");
            treeMenu.SetSortColumn("DISPLAYSEQUENCE");

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", "*"); // focus root value
            param.Add("p_languageType", UserInfo.Current.LanguageType);

            //treeMenu.DataSource = SqlExecuter.Procedure("usp_com_selectMenu_tree", param);
            treeMenu.DataSource = SqlExecuter.Query("SelectMenuTree", "10001", param);
            treeMenu.PopulateColumns();
            treeMenu.ExpandToLevel(1);

            TreeListNode rootNode = treeMenu.Nodes.FirstNode;
            treeMenu.SetFocusedNode(treeMenu.FindNodeByKeyID(rootNode.GetValue("MENUID")));
        }

        /// <summary>        
        /// 사용자 그룹 그리드 초기화
        /// </summary>
        private void InitializeGridUserClass()
        {
            grdUserClass.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdUserClass.View.SetSortOrder("USERCLASSID");

            grdUserClass.View.AddTextBoxColumn("USERCLASSID", 150);
            grdUserClass.View.AddTextBoxColumn("USERCLASSNAME", 200);
            //grdUserClass.View.AddCheckBoxColumn("ACTIVATED", 90);
            grdUserClass.View.PopulateColumns();

            grdUserClass.View.SetIsReadOnly();
            grdUserClass.View.PopulateColumns();

            grdUserClass.GridButtonItem = GridButtonItem.None;
            grdUserClass.View.OptionsCustomization.AllowFilter = true;
            grdUserClass.View.OptionsCustomization.AllowSort = true;

            grdUserClass.GridButtonItem = GridButtonItem.Refresh;
        }

        /// <summary>        
        /// 툴바 그리드 초기화
        /// </summary>
        private void InitializeGridMenuToolbar()
        {

            grdMenuToolbar.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdMenuToolbar.View.IsUseReadOnlyColor = false;

            grdMenuToolbar.View.SetSortOrder("DISPLAYSEQUENCE");

            grdMenuToolbar.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly()
                .SetDefault(UserInfo.Current.Uiid);
            grdMenuToolbar.View.AddTextBoxColumn("TOOLBARID", 150)
                .SetIsReadOnly();
            grdMenuToolbar.View.AddTextBoxColumn("TOOLBARNAME", 200)
                .SetIsReadOnly();
            grdMenuToolbar.View.AddCheckBoxColumn("ACTIVATED", 90)
                .SetValidationIsRequired();
            grdMenuToolbar.View.AddSpinEditColumn("DISPLAYSEQUENCE", 70)
                .SetIsHidden()
                .SetIsReadOnly();

            grdMenuToolbar.View.PopulateColumns();

            grdMenuToolbar.GridButtonItem = GridButtonItem.None;
            grdMenuToolbar.View.OptionsCustomization.AllowFilter = true;
            grdMenuToolbar.View.OptionsCustomization.AllowSort = true;

            grdMenuToolbar.GridButtonItem = GridButtonItem.Refresh;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        public void InitializeEvent()
        {
            Load += ToolbarAuthority_Load;
            treeMenu.RowClick += TreeMenu_RowClick;
            grdUserClass.View.FocusedRowChanged += UserClassView_FocusedRowChanged; // 사용자그룹 그리드의 포커스 데이터가 바뀌면.
            //grdMenuToolbar.View.CellValueChanging += ToolbarView_CellValueChanging;
            smartGroupBox1.CustomButtonClick += SmartGroupBox1_CustomButtonClick1;
            grdUserClass.ToolbarRefresh += GrdUserClass_ToolbarRefresh;
            grdMenuToolbar.ToolbarRefresh += GrdMenuToolbar_ToolbarRefresh;
        }

        private void GrdUserClass_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusUserClass = grdUserClass.View.FocusedRowHandle;
                grdUserClass.ShowWaitArea();

                LoadDataGridUserClass();

                grdUserClass.View.FocusedRowHandle = 0;
                //grdUserClass.View.FocusedRowHandle = beforeFocusUserClass;
                //grdUserClass.View.UnselectRow(afterFocusUserClass);
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

        private void GrdMenuToolbar_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusUserClass = grdMenuToolbar.View.FocusedRowHandle;
                grdMenuToolbar.ShowWaitArea();

                LoadDataGridToolbar();

                grdMenuToolbar.View.FocusedRowHandle = 0;
                //grdUserClass.View.FocusedRowHandle = beforeFocusUserClass;
                //grdUserClass.View.UnselectRow(afterFocusUserClass);
                grdMenuToolbar.View.UnselectRow(beforeFocusUserClass);
                grdMenuToolbar.View.SelectRow(0);

                //focusedRowChanged();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdMenuToolbar.CloseWaitArea();
            }
        }

        private void SmartGroupBox1_CustomButtonClick1(object sender, BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeMenu();
            }
        }

        private void UserClassView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadGridToolbar();
            System.Diagnostics.Debug.WriteLine("--------- FocusRowHandle : " + e.FocusedRowHandle.ToString());
            focusedRowChanged();
        }

        private void ToolbarView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataTable dtUserClass = (DataTable)grdUserClass.DataSource;
            Boolean checkTrue = false;

            foreach (DataRow row in dtUserClass.Rows)
            {
                if (row["ACTIVATED"].ToString() == "True")
                {
                    checkTrue = true;
                }
            }

            if (checkTrue == false)
            {
                //throw MessageException.Create("메뉴에 선택한 사용자 권한이 부여되지 않았습니다.");
                //LoadGridToolbar();
            }
        }

        private void ToolbarAuthority_Load(object sender, EventArgs e)
        {
            InitializeMenu();
            InitializeGridUserClass();
            InitializeGridMenuToolbar();

            LoadDataGridUserClass();
        }

        private void TreeMenu_RowClick(object sender, DevExpress.XtraTreeList.RowClickEventArgs e)
        {
            pnlContent.ShowWaitArea();

            LoadDataGridUserClass();

            pnlContent.CloseWaitArea();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdMenuToolbar.GetChangedRows();
            DataRow focusTreeRow = treeMenu.GetFocusedDataRow();
            
            var selectedMenu = focusTreeRow["MENUID"].ToString();
            var selectedUserClass = grdUserClass.View.GetRowCellValue(grdUserClass.View.FocusedRowHandle, "USERCLASSID").ToString();
            
            foreach (DataRow row in changed.Rows)
            {
                row["MENUID"] = selectedMenu;
                row["USERCLASSID"] = selectedUserClass;
            }

            ExecuteRule("SaveToolbarAuthority", changed);

            // 저장 후 사용자 그룹 그리드에 기존 focus 처리
            int beforeFocusUserClass = grdUserClass.View.FocusedRowHandle;

            LoadDataGridUserClass();

            int afterFocusUserClass = grdUserClass.View.FocusedRowHandle;

            grdUserClass.View.FocusedRowHandle = beforeFocusUserClass;
            grdUserClass.View.UnselectRow(afterFocusUserClass);
            grdUserClass.View.SelectRow(beforeFocusUserClass);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            TreeListNode beforeFocusNode = treeMenu.FocusedNode;

            treeMenu.SetFocusedNode(treeMenu.FindNodeByKeyID(beforeFocusNode.GetValue("MENUID")));
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            if (grdMenuToolbar.DataSource == null)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }


            grdUserClass.View.CheckValidation();
            grdMenuToolbar.View.CheckValidation();

            DataTable changed = grdMenuToolbar.GetChangedRows();

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
            //var row = grdUserClass.View.GetDataRow(grdUserClass.View.FocusedRowHandle);

            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("P_CODECLASSID", row["CODECLASSID"].ToString());

            //if (string.IsNullOrEmpty(row["CODECLASSID"].ToString()))
            //{
            //    ShowMessage("NoSelectData");
            //}
            //grdMenuToolbar.DataSource = SqlExecuter.Procedure("usp_com_selectCode", param);
            LoadDataGridToolbar();
        }

        private void LoadDataGridUserClass()
        {
            int beforeHandle = grdUserClass.View.FocusedRowHandle;

            DataRow focusRow = treeMenu.GetFocusedDataRow();

            if (focusRow["MENUID"].ToString().Equals("*") || focusRow["MENUTYPE"].ToString().Equals("Folder"))
            {
                grdUserClass.DataSource = null;
            }
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("p_menuId", focusRow["MENUID"].ToString());
                param.Add("p_userID", UserInfo.Current.Id);
                param.Add("p_validState", "Valid");
                param.Add("p_languageType", UserInfo.Current.LanguageType);

                //grdUserClass.DataSource = SqlExecuter.Procedure("usp_com_selectToolbarAuthority_userClass", param);
                grdUserClass.DataSource = SqlExecuter.Query("SelectToolbarAuthorityUserClass", "10001", param);
            }

            int afterHandle = grdUserClass.View.FocusedRowHandle;

            if ((beforeHandle == 0 && afterHandle == 0) || (beforeHandle == afterHandle))
            {
                LoadDataGridToolbar();
            }
        }

        private void LoadDataGridToolbar()
        {
            DataRow focusRow = treeMenu.GetFocusedDataRow();
            if (focusRow["MENUID"].ToString() == "*")
            {
                grdUserClass.DataSource = null;
                grdMenuToolbar.DataSource = null;
                return;
            }


            // 사용자그룹 그리드에서 아무것도 선택안됐을 때.
            if (grdUserClass.View.FocusedRowHandle < 0)
            {
                grdMenuToolbar.DataSource = null;
            }
            else
            {
                var row = grdUserClass.View.GetDataRow(grdUserClass.View.FocusedRowHandle);

                if (row == null)
                {
                    grdUserClass.View.FocusedRowHandle = grdUserClass.View.RowCount - 1;
                    grdUserClass.View.DeleteSelectedRows();
                    grdUserClass.View.SelectRow(grdUserClass.View.RowCount - 1);
                    row = grdUserClass.View.GetDataRow(grdUserClass.View.RowCount - 1);
                }

                if (focusRow["MENUTYPE"].ToString().ToUpper().Equals("FOLDER"))
                {
                    grdUserClass.DataSource = null;
                    grdMenuToolbar.DataSource = null;
                }
                else
                {
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("p_userClassId", row["USERCLASSID"].ToString());
                    param.Add("p_menuId", focusRow["MENUID"].ToString());
                    param.Add("p_userID", UserInfo.Current.Id);
                    param.Add("p_validState", "Valid");
                    param.Add("p_languageType", UserInfo.Current.LanguageType);

                    //grdMenuToolbar.DataSource = SqlExecuter.Procedure("usp_com_selectToolbarAuthority_toolbar", param);
                    grdMenuToolbar.DataSource = SqlExecuter.Query("SelectToolbarAuthorityToolbar", "10001", param);
                    grdMenuToolbar.View.SetFocusedRowCellValue("userClassId", row["USERCLASSID"].ToString());
                }
            }
        }

        #endregion
    }
}