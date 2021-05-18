#region using

using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;

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
    /// 프 로 그 램 명  : 시스템 관리 > 메뉴 관리 > 메뉴 정보
    /// 업  무  설  명  : 메뉴 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Menu : SmartConditionManualBaseForm
    {
        #region Local Variables

        #endregion

        #region 생성자

        public Menu()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            smartGroupBox1.GridButtonItem = GridButtonItem.Refresh;

            InitializeMenu();
            InitializeGrid();

            InitializeEvent();

            LoadDataMenu();
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
            treeMenu.SetSortColumn("DISPLAYSEQUENCE", SortOrder.Ascending);

            string strMenuId = "";
            if (treeMenu.FocusedNode != null)
                strMenuId = treeMenu.GetRowCellValue(treeMenu.FocusedNode, treeMenu.Columns["MENUID_COPY"]).ToString();

            //int itemIndex = treeMenu.GetVisibleIndexByNode(treeMenu.FocusedNode);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", "*"); // focus root value
            param.Add("p_languageType", UserInfo.Current.LanguageType);
            //treeMenu.DataSource = Procedure("usp_com_selectMenu_tree", param);
            treeMenu.DataSource = SqlExecuter.Query("SelectMenuTree", "10001", param);

            treeMenu.PopulateColumns();

            treeMenu.ExpandToLevel(1);
            //treeMenu.FocusedNode = treeMenu.GetNodeByVisibleIndex(itemIndex);
            treeMenu.SetFocusedNode(treeMenu.FindNodeByFieldValue("MENUID_COPY", strMenuId));
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // Sort는 usp로 설정. -- 부모메뉴ID/부모메뉴ID/.../메뉴ID

            grdMenu.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdMenu.View.SetSortOrder("DISPLAYSEQUENCE");

            grdMenu.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly()
                .SetDefault(UserInfo.Current.Uiid);

            grdMenu.View.AddTextBoxColumn("MENUID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();

            grdMenu.View.AddLanguageColumn("MENUNAME", 200);
            //grdMenu.View.AddTextBoxColumn("KMENUNAME",200);
            //grdMenu.View.AddTextBoxColumn("EMENUNAME",200);
            //grdMenu.View.AddTextBoxColumn("CMENUNAME",200);
            //grdMenu.View.AddTextBoxColumn("LMENUNAME",200);

            grdMenu.View.AddTextBoxColumn("DESCRIPTION", 200);

            //grdMenu.View.AddComboBoxColumn("PARENTMENUID", 200, new SqlQuery("GetParentMenuId", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MENUNAME", "MENUID")
            //    .SetMultiColumns(ComboBoxColumnShowType.DisplayMemberAndValueMember, true);

            grdMenu.View.AddTreeListColumn("PARENTMENUID", 150, new SqlQuery("GetParentMenuId", "00001", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "MENUNAME", "MENUID", "PARENTMENUID")
                .SetSortColumn("DISPLAYSEQUENCE");

            grdMenu.View.AddSpinEditColumn("DISPLAYSEQUENCE", 70)
                .SetDisplayFormat("#,##0", MaskTypes.Numeric, false)
                .SetValueRange(1, decimal.MaxValue)
                .SetDefault("1");
            grdMenu.View.AddTextBoxColumn("PROGRAMID", 300); //CODECLASSID='MenuType'
            grdMenu.View.AddComboBoxColumn("MENUTYPE", 90, new SqlQuery("GetCodeList", "00001", "CODECLASSID=MenuType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME")
                .SetDefault("Screen")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdMenu.View.AddComboBoxColumn("ISUSEPLANTAUTHORITY", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("N")
                //.SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);
            grdMenu.View.AddComboBoxColumn("ISUSEPLANTSINGLE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetDefault("N")
                .SetTextAlignment(TextAlignment.Center);
            grdMenu.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"), "CODENAME")
                .SetDefault("Valid")
                .SetValidationIsRequired()
                .SetTextAlignment(TextAlignment.Center);

            grdMenu.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdMenu.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);
            grdMenu.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdMenu.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdMenu.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            grdMenu.View.AddingNewRow += View_AddingNewRow;
            treeMenu.FocusedNodeChanged += treeMenu_FocusedNodeChanged;
            smartGroupBox1.CustomButtonClick += SmartGroupBox1_RefreshButtonClick;
        }

        private void SmartGroupBox1_RefreshButtonClick(object sender, BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeMenu();
                treeMenu.FocusedNode = treeMenu.GetNodeByVisibleIndex(0);
            }
        }

        private void View_AddingNewRow(Framework.SmartControls.Grid.BandedGrid.SmartBandedGridView sender, Framework.SmartControls.Grid.AddNewRowArgs args)
        {
            DataRow focusRow = treeMenu.GetFocusedDataRow();
            string parentMenuId = focusRow["MENUID"].ToString();

            args.NewRow["PARENTMENUID"] = parentMenuId == "*" ? "" : parentMenuId;

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("p_parentMenuId", parentMenuId == "*" ? "" : parentMenuId);

            //args.NewRow["DISPLAYSEQUENCE"] = Format.GetInteger(Procedure("usp_com_selectMenu_Displaysequence", dicParam).Rows[0]["DISPLAYSEQUENCE"]) + 1;
            args.NewRow["DISPLAYSEQUENCE"] = Format.GetInteger(SqlExecuter.Query("GetMenuDisplaySequence", "10001", dicParam).Rows[0]["DISPLAYSEQUENCE"]) + 1;

            //args.NewRow["DISPLAYSEQUENCE"] = args.NewRow.Table.Rows.Count == 0 ? 1 :
            //    args.NewRow.Table.Rows.Cast<DataRow>()
            //        .Where(r => r != args.NewRow && r["DISPLAYSEQUENCE"].ToString() != "" )
            //        .Max(r => decimal.Parse(r["DISPLAYSEQUENCE"].ToString())) + 1;
        }

        private void treeMenu_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            pnlContent.ShowWaitArea();
            try
            {
                DataRow focusRow = treeMenu.GetFocusedDataRow();
                if(focusRow == null)
                {
                    return;
                }
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("p_menuId", focusRow["MENUID"].ToString());
                param.Add("p_validState", "Valid");
                param.Add("p_conditionItem", "*");
                param.Add("p_conditionValue", "");

                if (focusRow["MENUTYPE"].ToString().ToUpper().Equals("FOLDER") || focusRow["MENUID"].ToString().Equals("*"))
                {
                    grdMenu.View.OptionsCustomization.AllowFilter = true;
                    grdMenu.View.OptionsCustomization.AllowSort = true;
                    grdMenu.ShowButtonBar = true;

                    //grdMenu.DataSource = Procedure("usp_com_selectMenu_grid", param);
                    grdMenu.DataSource = SqlExecuter.Query("SelectMenuGrid", "10001", param);
                }
                else
                {
                    grdMenu.View.OptionsCustomization.AllowFilter = false;
                    grdMenu.View.OptionsCustomization.AllowSort = false;
                    grdMenu.ShowButtonBar = false;

                    grdMenu.DataSource = null;
                }
            }
            finally
            {
                pnlContent.CloseWaitArea();
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

            DataTable changed = grdMenu.GetChangedRows();
            ExecuteRule("SaveMenu", changed);

            var conditionItemParentMenuId = grdMenu.View.GetConditions().GetCondition<ConditionItemTreeList>("PARENTMENUID");

            var colParentMenuId = grdMenu.View.Columns["PARENTMENUID"] as BandedGridColumnEx<RepositoryItemTreeListLookUpEdit>;
            colParentMenuId.Editor.DataSource = conditionItemParentMenuId.Query.Execute();

            InitializeMenu();
        }

        #endregion

        #region 검색

        /// <summary> 
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            TreeListNode beforeTreeListNode = treeMenu.FocusedNode;  // 기존 FocusedMenu 

            DataRow focusRow = treeMenu.GetFocusedDataRow();

            var param = Conditions.GetValues();
            param.Add("P_MENUID", focusRow["MENUID"]);
            await base.OnSearchAsync();

            if (focusRow["MENUTYPE"].ToString().ToUpper().Equals("FOLDER") || focusRow["MENUID"].ToString().Equals("*"))
            {
                //grdMenu.DataSource = await ProcedureAsync("usp_com_selectMenu_grid", param);
                grdMenu.DataSource = await QueryAsync("SelectMenuGrid", "10001", param);
            }
            else
            {
                grdMenu.DataSource = null;
            }
            treeMenu.SetFocusedNode(treeMenu.FindNodeByKeyID(beforeTreeListNode.GetValue("MENUID")));
            
            TreeListNode afterTreeListNode = treeMenu.FocusedNode;
            afterTreeListNode.Expanded = true;
            afterTreeListNode.Expand();
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();
            grdMenu.View.CheckValidation();

            DataTable changed = grdMenu.GetChangedRows();

            if (changed.Rows.Count == 0)
            {
                // 저장할 데이터가 존재하지 않습니다.
                throw MessageException.Create("NoSaveData");
            }

            var group = from t1 in changed.Rows.Cast<DataRow>()
                        group t1 by new { PARNETMENUID = t1.Field<String>("PARENTMENUID") } into grp
                        select new
                        {
                            PARENTMENUID = grp.Key.PARNETMENUID
                        };

            DataRow focusRow = treeMenu.GetFocusedDataRow();
            string parentMenuId = string.Empty;
            string strModifiedOrDeletedMenuId = "";
            string strAddedOrModifiedDispalySequence = "";

            foreach (var g in group)
            {
                // DisplaySequence Check - db.
                parentMenuId = g.PARENTMENUID;

                foreach (DataRow row in changed.Rows)
                {
                    string state = row["_STATE_"].ToString();
                    if (state == "added")
                    {
                        strAddedOrModifiedDispalySequence += row["DISPLAYSEQUENCE"].ToString() + ";";
                    }
                    else if (state == "modified")
                    {
                        strAddedOrModifiedDispalySequence += row["DISPLAYSEQUENCE"].ToString() + ";";
                        strModifiedOrDeletedMenuId += row["MENUID"].ToString() + ";";
                    }

                    else if (state == "deleted")
                    {
                        //strAddedOrModifiedDispalySequence += row["DISPLAYSEQUENCE"].ToString() + ";";
                        strModifiedOrDeletedMenuId += row["MENUID"].ToString() + ";";
                    }
                }

                if (strModifiedOrDeletedMenuId != "")
                {
                    strModifiedOrDeletedMenuId = strModifiedOrDeletedMenuId.Remove(strModifiedOrDeletedMenuId.Length - 1, 1);
                }

                if (strAddedOrModifiedDispalySequence != "")
                {
                    strAddedOrModifiedDispalySequence = strAddedOrModifiedDispalySequence.Remove(strAddedOrModifiedDispalySequence.Length - 1, 1);
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("p_PARENTMENUID", parentMenuId);
                param.Add("p_MODIFIEDORDELETEDMENUID", strModifiedOrDeletedMenuId);
                param.Add("p_ADDEDORMODIFIEDDISPLAYSEQUENCE", strAddedOrModifiedDispalySequence);


                //DataTable dtDuplicated = Procedure("usp_com_selectDuplicatedDisplaySequenceMenu", param) as DataTable;
                DataTable dtDuplicated = SqlExecuter.Query("GetDuplicatedMenuDisplaySequence", "10001", param);

                if (dtDuplicated.Rows.Count > 0)
                {
                    foreach (DataRow row in dtDuplicated.Rows)
                    {
                        throw MessageException.Create("DuplicatedDisplaySequence", parentMenuId);
                    }
                }
            }
        }

        #endregion

        #region Private Function

        /// <summary>        
        /// 메뉴 데이터 로드
        /// </summary>
        private async void LoadDataMenu()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", "*");
            param.Add("p_validState", "Valid");
            param.Add("p_conditionItem", "*");
            param.Add("p_conditionValue", "");

            //grdMenu.DataSource = await ProcedureAsync("usp_com_selectMenu_grid", param);
            grdMenu.DataSource = await QueryAsync("SelectMenuGrid", "10001", param);
        }

        #endregion
    }
}