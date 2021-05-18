using DevExpress.XtraTreeList.Nodes;
using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.SmartMES.Commons.Controls;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Micube.SmartMES.SystemManagement
{
    public partial class MenuToolbarMapping : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public MenuToolbarMapping()
        {
            InitializeComponent();
        }

        #endregion

        #region 툴바

        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        protected override void OnToolbarSaveClick()
        {
            base.OnToolbarSaveClick();

            DataTable changed = grdMenuToolbarMapping.GetChangedRows();
            this.ExecuteRule("SaveMenuToolbarMapping", changed);
        }
        
        #endregion

        #region 검색

        /// <summary>
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
         protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            TreeListNode beforeTreeListNode = this.treeMenu.FocusedNode;  // 기존 FocusedMenu 

            DataRow focusRow = treeMenu.GetFocusedDataRow();

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("p_menuId", focusRow["MENUID"].ToString());
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            this.grdMenuToolbarMapping.DataSource = await this.ProcedureAsync("usp_com_selectMenuToolbarMapping", values);

            this.treeMenu.SetFocusedNode(beforeTreeListNode); // 기존 FocusedMenu Focus 유지

        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            this.grdMenuToolbarMapping.View.CheckValidation();

            DataTable changed = this.grdMenuToolbarMapping.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            this.treeMenu.RowClick += TreeMenu_RowClick;
            this.smartGroupBox1.CustomButtonClick += SmartGroupBox1_CustomButtonClick1;
        }

        private void SmartGroupBox1_CustomButtonClick1(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeMenu();
            }
        }

        private void TreeMenu_RowClick(object sender, DevExpress.XtraTreeList.RowClickEventArgs e)
        {
            DataRow focusRow = treeMenu.GetFocusedDataRow();
            string menuType = focusRow["MENUTYPE"].ToString();

            if (menuType.ToUpper().Equals("FOLDER"))
            {
                this.grdMenuToolbarMapping.DataSource = null;
            }
            else
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("p_menuId", focusRow["MENUID"].ToString());
                param.Add("p_languageType", UserInfo.Current.LanguageType);
                this.grdMenuToolbarMapping.DataSource = SqlExecuter.Procedure("usp_com_selectMenuToolbarMapping", param);
            }
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {   
            base.InitializeContent();

            smartGroupBox1.GridButtonItem = GridButtonItem.Refresh;

            InitializeEvent();

            InitializeMenu();
            InitializeGridToolbarMapping();
        }

        /// <summary>        
        /// 트리 초기화
        /// </summary>
        private void InitializeMenu()
        {
            this.treeMenu.SetResultCount(1);
            this.treeMenu.SetIsReadOnly();
            this.treeMenu.SetEmptyRoot("Root", "*");
            this.treeMenu.SetMember("MENUNAME", "MENUID", "PARENTMENUID");
            this.treeMenu.SetSortColumn("DISPLAYSEQUENCE");

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", "*"); // focus root value
            param.Add("p_languageType", UserInfo.Current.LanguageType);

            this.treeMenu.DataSource = SqlExecuter.Procedure("usp_com_selectMenu_tree", param);
            this.treeMenu.PopulateColumns();
            this.treeMenu.ExpandToLevel(1);

            TreeListNode rootNode = this.treeMenu.Nodes.FirstNode;
            this.treeMenu.SetFocusedNode(treeMenu.FindNodeByKeyID(rootNode.GetValue("MENUID")));
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGridToolbarMapping()
        {
            this.grdMenuToolbarMapping.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;
            this.grdMenuToolbarMapping.View.IsUseReadOnlyColor = false;

            this.grdMenuToolbarMapping.View.SetSortOrder("USERCLASSID");
            this.grdMenuToolbarMapping.View.SetSortOrder("DISPLAYSEQUENCE");
            
            this.grdMenuToolbarMapping.View.AddTextBoxColumn("MENUID", 150)
                .SetIsReadOnly();
            this.grdMenuToolbarMapping.View.AddTextBoxColumn("USERCLASSID", 150)
                .SetIsReadOnly();
            this.grdMenuToolbarMapping.View.AddTextBoxColumn("TOOLBARID", 150)
                .SetIsReadOnly();
            this.grdMenuToolbarMapping.View.AddTextBoxColumn("TOOLBARNAME", 200)
                .SetIsReadOnly();
            this.grdMenuToolbarMapping.View.AddCheckBoxColumn("ACTIVATED", 90)
                .SetValidationIsRequired();

            this.grdMenuToolbarMapping.GridButtonItem = GridButtonItem.None;

            this.grdMenuToolbarMapping.View.OptionsCustomization.AllowFilter = true;
            this.grdMenuToolbarMapping.View.OptionsCustomization.AllowSort = true;
            //this.grdMenuToolbarMapping.ShowButtonBar = false;

            this.grdMenuToolbarMapping.View.PopulateColumns();
            // activated만 활성화
         }

        #endregion       
    }
}