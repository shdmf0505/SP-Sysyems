#region using

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
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

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 조건 관리 > 메뉴 - 조회조건 매핑
    /// 업  무  설  명  : 메뉴 - 조회조건 매핑 정보를 관리한다.
    /// 생    성    자  : 강호윤
    /// 생    성    일  : 2019-04-01
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class MenuConditionItemMapping : SmartConditionManualBaseForm
    {
        #region Local Variables

        private DataTable _searchData;
        private DataTable _saveData;
        private DataTable _realSaveData;

        #endregion

        #region 생성자

        public MenuConditionItemMapping()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        /// <summary>
        /// 우측 컨텐츠 영역에 초기화할 코드를 넣으세요.
        /// </summary>
        protected override void InitializeContent()
        {
            base.InitializeContent();

            smartGroupBox1.GridButtonItem = GridButtonItem.Refresh;

            InitializeEvent();

            InitializeMenuTree();
            InitializeNotMappingGrid();
            InitializeMappingGrid();
        }

        /// <summary>
        /// 메뉴 트리 초기화
        /// </summary>
        private void InitializeMenuTree()
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>
            {
                { "p_userId", UserInfo.Current.Id },
                { "p_validState", "Valid" },
                { "p_languageType", UserInfo.Current.LanguageType }
            };

            //treeMenuList.DataSource = SqlExecuter.Procedure("usp_com_selectMenuConditionItemMappingMenuList", dicParam);
            treeMenuList.DataSource = SqlExecuter.Query("GetMenuConditionItemMappingMenu", "10001", dicParam);

            treeMenuList.SetSortColumn("MENUSEQ");
            treeMenuList.SetSortColumn("DISPLAYSEQUENCE");
            treeMenuList.SetMember("MENUNAME", "MENUID", "PARENTMENUID");

            treeMenuList.PopulateColumns();

            treeMenuList.ExpandToLevel(0);
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeNotMappingGrid()
        {
            grdNotMappingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdNotMappingList.View.SetSortOrder("CONDITIONITEMID");

            grdNotMappingList.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly();
            grdNotMappingList.View.AddTextBoxColumn("MENUID", 100)
                .SetValidationKeyColumn()
                .SetIsHidden();
            grdNotMappingList.View.AddTextBoxColumn("CONDITIONITEMID", 150)
                .SetValidationKeyColumn();
            grdNotMappingList.View.AddTextBoxColumn("CONDITIONITEMNAME", 200);
            grdNotMappingList.View.AddComboBoxColumn("CONDITIONTYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConditionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            grdNotMappingList.View.AddSpinEditColumn("DISPLAYSEQUENCE", 70)
                .SetIsHidden();
            grdNotMappingList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsHidden();
            grdNotMappingList.View.AddTextBoxColumn("RELATIONCOLUMNSTACK", 140)
                .SetIsHidden();
            grdNotMappingList.View.AddTextBoxColumn("COLUMNNAME", 120);
            grdNotMappingList.View.AddComboBoxColumn("ITEMTYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConditionItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
            grdNotMappingList.View.AddTextBoxColumn("DATAFORMAT", 130);
            grdNotMappingList.View.AddTextBoxColumn("DEFAULTVALUE", 150);
            grdNotMappingList.View.AddComboBoxColumn("ISREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            grdNotMappingList.View.AddComboBoxColumn("ISHIDDEN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            grdNotMappingList.View.AddComboBoxColumn("ISALL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            grdNotMappingList.View.AddComboBoxColumn("EXECUTETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ExecuteType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center);
            grdNotMappingList.View.AddTextBoxColumn("PARENTCONDITIONITEMGROUPID", 120)
                .SetIsHidden();

            grdNotMappingList.View.SetIsReadOnly();
            grdNotMappingList.GridButtonItem = GridButtonItem.Export;
            grdNotMappingList.ShowButtonBar = true;

            grdNotMappingList.View.PopulateColumns();
        }

        private void InitializeMappingGrid()
        {
            grdMappingList.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

            grdMappingList.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly();
            grdMappingList.View.AddTextBoxColumn("MENUID", 100)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly();
            grdMappingList.View.AddTextBoxColumn("CONDITIONITEMID", 150)
                .SetValidationKeyColumn()
                .SetIsReadOnly();
            grdMappingList.View.AddTextBoxColumn("CONDITIONITEMNAME", 200)
                .SetIsReadOnly();
            grdMappingList.View.AddComboBoxColumn("CONDITIONTYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConditionType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetIsHidden();
            var colDisplaySequence = grdMappingList.View.AddSpinEditColumn("DISPLAYSEQUENCE", 70)
                .SetDisplayFormat("#,###0.#")
                .SetValidationIsRequired();
            colDisplaySequence.IsImmediatlyUpdate = false;
            grdMappingList.View.AddComboBoxColumn("VALIDSTATE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ValidState", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetDefault("Valid")
                .SetValidationIsRequired();
            grdMappingList.View.AddTextBoxColumn("RELATIONCOLUMNSTACK", 140);
            grdMappingList.View.AddTextBoxColumn("COLUMNNAME", 120)
                .SetIsReadOnly();
            grdMappingList.View.AddComboBoxColumn("ITEMTYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ConditionItemType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();
            grdMappingList.View.AddTextBoxColumn("DATAFORMAT", 130)
                .SetIsReadOnly();
            grdMappingList.View.AddTextBoxColumn("DEFAULTVALUE", 150)
                .SetIsReadOnly();
            grdMappingList.View.AddComboBoxColumn("ISREQUIRED", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMappingList.View.AddComboBoxColumn("ISHIDDEN", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMappingList.View.AddComboBoxColumn("ISALL", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=YesNo", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMappingList.View.AddComboBoxColumn("EXECUTETYPE", 80, new SqlQuery("GetCodeList", "00001", "CODECLASSID=ExecuteType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetTextAlignment(TextAlignment.Center)
                .SetIsReadOnly();
            grdMappingList.View.AddTextBoxColumn("PARENTCONDITIONITEMGROUPID", 120)
                .SetIsReadOnly()
                .SetIsHidden();

            grdMappingList.GridButtonItem = GridButtonItem.Export;
            grdMappingList.ShowButtonBar = true;

            grdMappingList.View.PopulateColumns();

            grdMappingList.View.Columns["DISPLAYSEQUENCE"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            treeMenuList.FocusedNodeChanged += TreeMenuList_FocusedNodeChanged;

            grdNotMappingList.View.RowStyle += ViewNotMappingList_RowStyle;
            grdMappingList.View.RowStyle += ViewMappingList_RowStyle;

            grdMappingList.View.CellValueChanged += ViewMappingList_CellValueChanged;

            btnSelectItemDelete.Click += BtnSelectItemDelete_Click;
            btnSelectItemAdd.Click += BtnSelectItemAdd_Click;

            smartGroupBox1.CustomButtonClick += SmartGroupBox1_CustomButtonClick;
        }

        private void SmartGroupBox1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeMenuTree();
            }
        }

        private void BtnSelectItemDelete_Click(object sender, EventArgs e)
        {
            SelectItemDelete();
        }

        private void BtnSelectItemAdd_Click(object sender, EventArgs e)
        {
            SelectItemAdd();
        }

        private void ViewMappingList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (grdMappingList.View.IsEditing)
            //    return;

            if (e.Column.FieldName == "DISPLAYSEQUENCE")
            {
                //grdMappingList.View.CellValueChanged -= ViewMappingList_CellValueChanged;

                //DataTable dtMapping = (DataTable)grdMappingList.DataSource;

                //decimal dDisplaySequence = Format.GetDecimal(e.Value) - (decimal)0.5;
                //string strConditionItemid = grdMappingList.View.GetRowCellValue(e.RowHandle, "CONDITIONITEMID").ToString();

                //foreach (DataRow row in dtMapping.Rows)
                //{
                //    if (row.Field<string>("CONDITIONITEMID") == strConditionItemid)
                //        row["DISPLAYSEQUENCE"] = dDisplaySequence;
                //}

                //grdMappingList.View.SetRowCellValue(e.RowHandle, "DISPLAYSEQUENCE", dDisplaySequence);

                //int index = 1;
                //foreach (var dr in dtMapping.Rows.Cast<DataRow>().OrderBy(ex => ex["DISPLAYSEQUENCE"]))
                //{
                //    dr["DISPLAYSEQUENCE"] = index++;
                //}

                //dtMapping.AcceptChanges();

                grdMappingList.View.RefreshData();

                //DataView dvMapping = dtMapping.DefaultView;
                //dvMapping.Sort = "DISPLAYSEQUENCE ASC";

                //DataTable dtResult = dvMapping.ToTable();

                //grdMappingList.DataSource = dtResult;


                //int iRowHandle = grdMappingList.View.LocateByValue("CONDITIONITEMID", strConditionItemid);
                //if (iRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                //    grdMappingList.View.FocusedRowHandle = iRowHandle;

                //grdMappingList.View.CellValueChanged += ViewMappingList_CellValueChanged;
            }

            //if (e.Column.FieldName == "VALIDSTATE" || e.Column.FieldName == "RELATIONCOLUMNSTACK")
            //{

            //}
        }

        private void ViewMappingList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.Transparent;
        }

        private void ViewNotMappingList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.Transparent;
        }

        private void TreeMenuList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeFocusedNodeChanged();
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

            InitializeDataTable();

            DataTable dtMapping = (DataTable)grdMappingList.DataSource;
            DataView dvMapping = dtMapping.DefaultView;
            dvMapping.Sort = "DISPLAYSEQUENCE ASC";

            _saveData = dvMapping.ToTable();

            for (int i = 0; i < _saveData.Rows.Count; i++)
            {
                _saveData.Rows[i]["DISPLAYSEQUENCE"] = i + 1;
            }

            for (int i = 0; i < _searchData.Rows.Count; i++)
            {
                bool bIsHave = false;

                string strConditionItemIdBefore = _searchData.Rows[i]["CONDITIONITEMID"].ToString();
                decimal dDisplaySequenceBefore = Format.GetDecimal(_searchData.Rows[i]["DISPLAYSEQUENCE"]);
                string strRelationColumnStackBefore = _searchData.Rows[i]["RELATIONCOLUMNSTACK"].ToString();
                string strValidStateBefore = _searchData.Rows[i]["VALIDSTATE"].ToString();

                for (int j = 0; j < _saveData.Rows.Count; j++)
                {
                    string strConditionItemIdAfter = _saveData.Rows[j]["CONDITIONITEMID"].ToString();
                    decimal dDisplaySequenceAfter = Format.GetDecimal(_saveData.Rows[j]["DISPLAYSEQUENCE"]);
                    string strRelationColumnStackAfter = _saveData.Rows[j]["RELATIONCOLUMNSTACK"].ToString();
                    string strValidStateAfter = _saveData.Rows[j]["VALIDSTATE"].ToString();

                    if (strConditionItemIdBefore == strConditionItemIdAfter)
                    {
                        if (dDisplaySequenceBefore != dDisplaySequenceAfter || strValidStateBefore != strValidStateAfter || strRelationColumnStackBefore != strRelationColumnStackAfter)
                        {
                            DataRow addRow = _realSaveData.NewRow();
                            addRow.ItemArray = _saveData.Rows[j].ItemArray.Clone() as object[];
                            addRow["_STATE_"] = "modified";

                            _realSaveData.Rows.Add(addRow);
                        }

                        bIsHave = true;
                        break;
                    }
                    else
                    {
                        if (j == _saveData.Rows.Count - 1)
                            bIsHave = false;
                    }
                }

                if (!bIsHave)
                {
                    DataRow addRow = _realSaveData.NewRow();
                    //addRow["CONDITIONITEMGROUPID"] = grdConditionItemGroupList.View.GetFocusedRowCellValue("CONDITIONITEMGROUPID");
                    addRow["_STATE_"] = "deleted";

                    foreach (DataColumn col in _searchData.Columns)
                    {
                        if (col.ColumnName != "_STATE_")
                            addRow[col.ColumnName] = _searchData.Rows[i][col];
                    }

                    _realSaveData.Rows.Add(addRow);
                }
            }

            for (int i = 0; i < _saveData.Rows.Count; i++)
            {
                bool bIsHave = false;

                string strConditionItemIdBefore = _saveData.Rows[i]["CONDITIONITEMID"].ToString();

                for (int j = 0; j < _searchData.Rows.Count; j++)
                {
                    string strConditionItemIdAfter = _searchData.Rows[j]["CONDITIONITEMID"].ToString();

                    if (strConditionItemIdBefore == strConditionItemIdAfter)
                    {
                        bIsHave = true;
                        break;
                    }
                    else
                    {
                        if (j == _searchData.Rows.Count - 1)
                            bIsHave = false;
                    }
                }

                if (!bIsHave)
                {
                    DataRow addRow = _realSaveData.NewRow();
                    addRow.ItemArray = _saveData.Rows[i].ItemArray.Clone() as object[];
                    addRow["_STATE_"] = "added";

                    _realSaveData.Rows.Add(addRow);
                }
            }

            if (_realSaveData == null || _realSaveData.Rows.Count == 0)
                throw MessageException.Create("NoSaveData");

            //TODO :  RuleName을 수정하세요 (저장기능이 없다면 현재 함수를 삭제하세요.)
            //DataTable changed = gridMain.GetChangedRows();
            ExecuteRule("SaveMenuConditionItemMapping", _realSaveData);

            TreeFocusedNodeChanged();

            //dtSearchData = (DataTable)grdMappingList.DataSource;
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

            var values = Conditions.GetValues();
            values.Add("p_uiid", treeMenuList.GetRowCellValue(treeMenuList.FocusedNode, treeMenuList.Columns["UIID"]));
            values.Add("p_menuId", treeMenuList.GetRowCellValue(treeMenuList.FocusedNode, treeMenuList.Columns["MENUID_COPY"]));
            values.Add("p_validState", "Valid");
            values.Add("p_languageType", UserInfo.Current.LanguageType);

            //TODO : Id를 수정하세요            
            //grdNotMappingList.DataSource = await SqlExecuter.ProcedureAsync("usp_com_selectMenuConditionItemNotMappingList", values);
            grdNotMappingList.DataSource = await QueryAsync("SelectMenuConditionItemNotMapping", "10001", values);


            // Delete condition in mapping grid item
            DataTable dtSearchResult = (DataTable)grdNotMappingList.DataSource;
            for (int i = dtSearchResult.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dtSearchResult.Rows[i];

                string strConditionItemId = row["CONDITIONITEMID"].ToString();

                if (grdMappingList.View.GetRowHandleByValue("CONDITIONITEMID", strConditionItemId) >= 0)
                {
                    row.Delete();
                }
            }
            dtSearchResult.AcceptChanges();

            grdNotMappingList.DataSource = dtSearchResult;
        }

        #endregion

        #region 유효성 검사

        /// <summary>
        /// 데이터 저장할때 컨텐츠 영역의 유효성 검사
        /// </summary>
        protected override void OnValidateContent()
        {
            base.OnValidateContent();

            //TODO : 그리드의 유효성 검사
            grdMappingList.View.CheckValidation();

            //if (dtSaveData == null || dtSaveData.Rows.Count == 0)
            //{
            //    throw MessageException.Create("NoSaveData");
            //}

            DataTable dtData = (DataTable)grdMappingList.DataSource;
            foreach (DataRow row in dtData.Rows)
            {
                bool bIsHasColumn = false;
                bool bIsUpperSequence = false;

                if (!string.IsNullOrEmpty(row.Field<string>("RELATIONCOLUMNSTACK")))
                {
                    foreach (DataRow secondRow in dtData.Rows)
                    {
                        if (row.Field<string>("RELATIONCOLUMNSTACK") == secondRow.Field<string>("COLUMNNAME"))
                        {
                            bIsHasColumn = true;

                            if (row.Field<int>("DISPLAYSEQUENCE") > secondRow.Field<int>("DISPLAYSEQUENCE"))
                                bIsUpperSequence = true;

                            break;
                        }
                    }

                    if (!bIsHasColumn)
                    {
                        throw MessageException.Create("00004", row.Field<string>("RELATIONCOLUMNSTACK"));
                    }

                    if (!bIsUpperSequence)
                    {
                        throw MessageException.Create("00005", row.Field<string>("COLUMNNAME"), row.Field<string>("RELATIONCOLUMNSTACK"));
                    }
                }
            }
        }

        #endregion

        #region Private Function

        private void InitializeDataTable()
        {
            _realSaveData = new DataTable();

            foreach (DataColumn col in ((DataTable)grdMappingList.DataSource).Columns)
            {
                _realSaveData.Columns.Add(col.ColumnName, col.DataType);
            }

            _realSaveData.Columns.Add("_STATE_", typeof(string));
        }

        private void TreeFocusedNodeChanged()
        {
            if (treeMenuList.FocusedNode == null || treeMenuList.FocusedNode.GetValue("MENUTYPE").Equals("Folder"))
            {
                //grdNotMappingList.View.Clear();
                //grdMappingList.View.Clear();
                grdNotMappingList.DataSource = null;
                grdMappingList.DataSource = null;
                btnSelectItemDelete.Enabled = false;
                btnSelectItemAdd.Enabled = false;

                _searchData = null;

                return;
            }
            else if (treeMenuList.FocusedNode.GetValue("MENUTYPE").Equals("Screen"))
            {
                pnlContent.ShowWaitArea();

                btnSelectItemDelete.Enabled = true;
                btnSelectItemAdd.Enabled = true;

                //var values = Conditions.GetValues();
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("p_uiid", treeMenuList.FocusedNode.GetValue("UIID"));
                values.Add("p_menuId", treeMenuList.FocusedNode.GetValue("MENUID"));
                values.Add("p_validState", "Valid");
                values.Add("p_languageType", UserInfo.Current.LanguageType);

                //grdNotMappingList.DataSource = SqlExecuter.Procedure("usp_com_selectMenuConditionItemNotMappingList", values);
                grdNotMappingList.DataSource = SqlExecuter.Query("SelectMenuConditionItemNotMapping", "10001", values);

                Dictionary<string, object> dicParam = new Dictionary<string, object>
                {
                    { "p_uiid", treeMenuList.FocusedNode.GetValue("UIID") },
                    { "p_menuId", treeMenuList.FocusedNode.GetValue("MENUID") },
                    { "p_validState", "Valid" },
                    { "p_languageType", UserInfo.Current.LanguageType }
                };

                //DataTable dtMapping = SqlExecuter.Procedure("usp_com_selectMenuConditionItemMappingList", dicParam);
                DataTable dtMapping = SqlExecuter.Query("SelectMenuConditionItemMapping", "10001", dicParam);
                DataTable dtMappingClone = dtMapping.Clone();
                //dtMappingClone.Columns["DISPLAYSEQUENCE"].DataType = typeof(decimal);
                foreach (DataRow row in dtMapping.Rows)
                {
                    DataRow drRow = dtMappingClone.NewRow();
                    drRow.ItemArray = row.ItemArray.Clone() as object[];

                    dtMappingClone.Rows.Add(drRow);
                }

                grdMappingList.DataSource = dtMappingClone;



                _searchData = ((DataTable)grdMappingList.DataSource).Clone();
                foreach (DataRow row in ((DataTable)grdMappingList.DataSource).Rows)
                {
                    DataRow addRow = _searchData.NewRow();
                    addRow.ItemArray = row.ItemArray.Clone() as object[];
                    _searchData.Rows.Add(addRow);
                }

                pnlContent.CloseWaitArea();
            }
            else
            {
                grdNotMappingList.DataSource = null;
                grdMappingList.DataSource = null;
                btnSelectItemDelete.Enabled = false;
                btnSelectItemAdd.Enabled = false;

                _searchData = null;
            }
        }

        private void SelectItemDelete()
        {
            List<DataRowView> listDeleteRows = new List<DataRowView>();

            DataTable dtMapping = (DataTable)grdMappingList.DataSource;
            DataTable dtNotMapping = (DataTable)grdNotMappingList.DataSource;

            for (int i = grdMappingList.View.RowCount - 1; i >= 0; i--)
            {
                if (!grdMappingList.View.IsRowChecked(i))
                    continue;

                DataRowView row = grdMappingList.View.GetRow(i) as DataRowView;
                //dtNotMapping.AcceptChanges();

                //row.Delete();
                //dtMapping.AcceptChanges();

                listDeleteRows.Add(row);
            }

            foreach (DataRowView row in listDeleteRows)
            {
                row["DISPLAYSEQUENCE"] = DBNull.Value;
                row["VALIDSTATE"] = DBNull.Value;
                row["RELATIONCOLUMNSTACK"] = DBNull.Value;

                DataRowView newRow = (grdNotMappingList.View.DataSource as DataView).AddNew();
                newRow.BeginEdit();
                newRow.Row.ItemArray = row.Row.ItemArray.Clone() as object[];

                row.Delete();
                newRow.EndEdit();
            }

            dtMapping.AcceptChanges();
            dtNotMapping.AcceptChanges();

            int index = 1;
            foreach (var dr in dtMapping.Rows.Cast<DataRow>().OrderBy(e => e["DISPLAYSEQUENCE"]))
            {
                dr["DISPLAYSEQUENCE"] = index++;
            }
        }

        private void SelectItemAdd()
        {
            List<DataRowView> listAddRows = new List<DataRowView>();

            DataTable dtMapping = (DataTable)grdMappingList.DataSource;
            DataTable dtNotMapping = (DataTable)grdNotMappingList.DataSource;

            for (int i = grdNotMappingList.View.RowCount - 1; i >= 0; i--)
            {
                if (!grdNotMappingList.View.IsRowChecked(i))
                    continue;

                DataRowView row = grdNotMappingList.View.GetRow(i) as DataRowView;

                listAddRows.Add(row);
            }

            int iRowIndex = dtMapping.Rows.Count + 1;

            foreach (DataRowView row in listAddRows)
            {
                row["DISPLAYSEQUENCE"] = iRowIndex;
                row["VALIDSTATE"] = "Valid";

                DataRowView newRow = (grdMappingList.View.DataSource as DataView).AddNew();
                newRow.BeginEdit();
                newRow.Row.ItemArray = row.Row.ItemArray.Clone() as object[];

                row.Delete();
                newRow.EndEdit();

                iRowIndex++;
            }

            dtMapping.AcceptChanges();
            dtNotMapping.AcceptChanges();

            int index = 1;
            foreach (var dr in dtMapping.Rows.Cast<DataRow>().OrderBy(e => e["DISPLAYSEQUENCE"]))
            {
                dr["DISPLAYSEQUENCE"] = index++;
            }
        }

        #endregion
    }
}