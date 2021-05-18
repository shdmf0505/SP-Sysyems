#region using

using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraTreeList.Nodes;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 메뉴 관리 > 메뉴 권한 정보
    /// 업  무  설  명  : 메뉴 권한 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MenuAuthority : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public MenuAuthority()
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
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            grdUserMapping.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            grdUserMapping.View.IsUseReadOnlyColor = false;

            grdUserMapping.View.SetSortOrder("USERCLASSID");

            grdUserMapping.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly()
                .SetDefault(UserInfo.Current.Uiid);
            grdUserMapping.View.AddTextBoxColumn("USERCLASSID", 150)
                .SetIsReadOnly();
            grdUserMapping.View.AddTextBoxColumn("DESCRIPTION", 200)
                .SetIsReadOnly();
            grdUserMapping.View.AddCheckBoxColumn("ACTIVATED", 90);
            //grdUserMapping.View.AddTextBoxColumn("MENUID", 150)
            //    .SetIsReadOnly();

            grdUserMapping.GridButtonItem = GridButtonItem.None;
            //grdUserMapping.View.SetIsReadOnly(); // 그리드 read Only 하돼, 체크박스는 활성화 가능하게.
            grdUserMapping.View.PopulateColumns();

            grdUserMapping.View.OptionsCustomization.AllowFilter = true;
            grdUserMapping.View.OptionsCustomization.AllowSort = true;

            grdUserMapping.GridButtonItem = GridButtonItem.Refresh;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            Load += MenuAuthority_Load;
            treeMenu.RowClick += TreeMenu_RowClick;
            smartGroupBox1.CustomButtonClick += SmartGroupBox1_CustomButtonClick;
            grdUserMapping.ToolbarRefresh += GrdUserMapping_ToolbarRefresh;
        }

        private void GrdUserMapping_ToolbarRefresh(object sender, EventArgs e)
        {
            try
            {
                int beforeFocusUserClass = grdUserMapping.View.FocusedRowHandle;
                grdUserMapping.ShowWaitArea();

                LoadGridMenuAuthority();

                grdUserMapping.View.FocusedRowHandle = 0;
                grdUserMapping.View.UnselectRow(beforeFocusUserClass);
                grdUserMapping.View.SelectRow(0);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                grdUserMapping.CloseWaitArea();
            }
        }

        private void SmartGroupBox1_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeMenu();
            }
        }

        private void MenuAuthority_Load(object sender, EventArgs e)
        {
            InitializeMenu();
            InitializeGrid();
            //LoadGridMenuAuthority();
        }

        private void TreeMenu_RowClick(object sender, DevExpress.XtraTreeList.RowClickEventArgs e)
        {
            pnlContent.ShowWaitArea();

            LoadGridMenuAuthority();

            pnlContent.CloseWaitArea();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <returns></returns>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            //TODO :  RuleName을 수정하세요 (저장기능이 없다면 현재 함수를 삭제하세요.)
            DataTable changed = grdUserMapping.GetChangedRows();

            DataRow focusRow = treeMenu.GetFocusedDataRow();
            var selectedValue = focusRow["MENUID"].ToString();

            foreach (DataRow row in changed.Rows)
            {
                row["MENUID"] = selectedValue;
            }

            ExecuteRule("SaveMenuAuthority", changed);
        }

        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        /// <returns></returns>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();

            TreeListNode beforeFocusNode = treeMenu.FocusedNode; 

            InitializeMenu();

            // setFocusedNode에 Node를 파라미터로 줄 경우 Node값 자체가 아닌 인덱스로 인식하고 Focusing 처리
            // setFocusedNode에 특정 Focusing하고 싶을 경우 KeyId로 값 가져와서 처리. 
            // focus 노드가 속해있는 노드들 자동 expand
            treeMenu.SetFocusedNode(treeMenu.FindNodeByKeyID(beforeFocusNode.GetValue("MENUID")));

            LoadGridMenuAuthority();
        }
        
        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            grdUserMapping.View.CheckValidation();

            DataTable changed = grdUserMapping.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Private Function

        private void LoadGridMenuAuthority()
        {
            DataRow focusRow = treeMenu.GetFocusedDataRow();
            string menuType = focusRow["MENUTYPE"].ToString();

            if (focusRow["MENUID"].ToString().Equals("*"))
            {
                grdUserMapping.DataSource = null;
            }
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("p_menuId", focusRow["MENUID"].ToString());

                //grdUserMapping.DataSource = SqlExecuter.Procedure("usp_com_selectMenuAuthority", param);
                grdUserMapping.DataSource = SqlExecuter.Query("SelectMenuAuthority", "10001", param);
            }
        }

        #endregion
    }
}