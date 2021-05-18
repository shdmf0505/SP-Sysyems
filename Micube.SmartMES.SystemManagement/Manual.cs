#region using
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid.BandedGrid;
using Micube.SmartMES.Commons.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace Micube.SmartMES.SystemManagement
{
    /// <summary>
    /// 프 로 그 램 명  : 시스템 관리 > 메뉴 관리 > 매뉴얼 정보
    /// 업  무  설  명  : 메뉴별 매뉴얼을 관리한다.
    /// 생    성    자  : 황유성
    /// 생    성    일  : 2020-02-18
    /// 수  정  이  력  : 
    /// 
    /// 
    /// </summary>
    public partial class Manual : SmartConditionManualBaseForm
    {
        #region 생성자

        public Manual()
        {
            InitializeComponent();
        }

        #endregion

        #region 컨텐츠 영역 초기화

        protected override void InitializeContent()
        {
            base.InitializeContent();

            smartGroupBox1.GridButtonItem = GridButtonItem.Refresh;

            InitializeManual();
            InitializeGrid();

            InitializeEvent();

            LoadDataManual();
        }

        /// <summary>
        /// 트리 초기화
        /// </summary>
        private void InitializeManual()
        {
            treeManual.SetResultCount(1);
            treeManual.SetIsReadOnly();
            treeManual.SetEmptyRoot("Root", "*");
            treeManual.SetMember("MENUNAME", "MENUID", "PARENTMENUID");
            treeManual.SetSortColumn("DISPLAYSEQUENCE", SortOrder.Ascending);

            string strManualId = "";
            if (treeManual.FocusedNode != null)
                strManualId = treeManual.GetRowCellValue(treeManual.FocusedNode, treeManual.Columns["MENUID_COPY"]).ToString();

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", "*"); // focus root value
            param.Add("p_languageType", UserInfo.Current.LanguageType);
            treeManual.DataSource = SqlExecuter.Query("SelectMenuTree", "10001", param);

            treeManual.PopulateColumns();

            treeManual.ExpandToLevel(1);
            treeManual.SetFocusedNode(treeManual.FindNodeByFieldValue("MENUID_COPY", strManualId));
        }

        /// <summary>        
        /// 그리드 초기화
        /// </summary>
        private void InitializeGrid()
        {
            // Sort는 usp로 설정. -- 부모메뉴ID/부모메뉴ID/.../메뉴ID

            grdManual.GridButtonItem = GridButtonItem.None;
            grdManual.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdManual.View.SetSortOrder("DISPLAYSEQUENCE");

            grdManual.View.AddTextBoxColumn("UIID", 80)
                .SetValidationKeyColumn()
                .SetIsHidden()
                .SetIsReadOnly()
                .SetDefault(UserInfo.Current.Uiid);
            grdManual.View.AddTextBoxColumn("MENUID", 150)
                .SetValidationKeyColumn()
                .SetValidationIsRequired();
            grdManual.View.AddTextBoxColumn("MENUNAME$$" + UserInfo.Current.LanguageType, 200)
                .SetLabel("MENUNAME")
                .SetIsReadOnly();
            grdManual.View.AddTextBoxColumn("HASMANUAL", 100)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdManual.View.AddButtonColumn("REGISTERMANUAL", 110);
            grdManual.View.AddTextBoxColumn("CREATOR", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdManual.View.AddTextBoxColumn("CREATEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);
            grdManual.View.AddTextBoxColumn("MODIFIER", 80)
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            grdManual.View.AddTextBoxColumn("MODIFIEDTIME", 130)
                .SetIsReadOnly()
                .SetDisplayFormat("yyyy-MM-dd HH:mm:ss")
                .SetTextAlignment(TextAlignment.Center);

            grdManual.View.PopulateColumns();
        }

        #endregion

        #region Event

        /// <summary>        
        /// 이벤트 초기화
        /// </summary>
        private void InitializeEvent()
        {
            treeManual.FocusedNodeChanged += treeManual_FocusedNodeChanged;
            smartGroupBox1.CustomButtonClick += SmartGroupBox1_RefreshButtonClick;
            grdManual.View.GridCellButtonClickEvent += View_GridCellButtonClickEvent;
        }

        private void View_GridCellButtonClickEvent(SmartBandedGridView sender, Framework.SmartControls.Grid.GridCellButtonClickEventArgs args)
        {
            DataRow menuDataRow = grdManual.View.GetFocusedDataRow();
            if(menuDataRow == null)
            {
                return;
            }
            if(menuDataRow["MENUTYPE"].ToString() != "Screen")
            {
                return;
            }

            ModifyManualPopup manualPopup = new ModifyManualPopup(menuDataRow["MENUID"].ToString());
            manualPopup.FileInfo += (dtFileInfo) =>
            {
                string menuId = menuDataRow["MENUID"].ToString();
                MessageWorker worker = new MessageWorker("SaveManual");
                worker.SetBody(new MessageBody()
                {
                    { "menuid", menuId },
                    { "uiid", UserInfo.Current.Uiid },
                    { "list", dtFileInfo }
                });
                worker.Execute();
                RefreshGrdManual();
            };
            manualPopup.ShowDialog();
        }

        private void SmartGroupBox1_RefreshButtonClick(object sender, BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "GridRefresh")
            {
                InitializeManual();
                treeManual.FocusedNode = treeManual.GetNodeByVisibleIndex(0);
            }
        }

        private void treeManual_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            RefreshGrdManual();
        }

        private void RefreshGrdManual()
        {
            pnlContent.ShowWaitArea();
            try
            {
                DataRow focusRow = treeManual.GetFocusedDataRow();
                if (focusRow == null)
                {
                    return;
                }
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("P_MENUID", focusRow["MENUID"].ToString());
                param.Add("P_VALIDSTATE", "Valid");
                param.Add("P_CONDITIONITEM", "*");
                param.Add("P_CONDITIONVALUE", "");

                if (focusRow["MENUTYPE"].ToString().ToUpper().Equals("FOLDER") || focusRow["MENUID"].ToString().Equals("*"))
                {
                    grdManual.View.OptionsCustomization.AllowFilter = true;
                    grdManual.View.OptionsCustomization.AllowSort = true;
                    grdManual.ShowButtonBar = true;

                    grdManual.DataSource = SqlExecuter.Query("SelectMenuGrid", "10001", param);
                }
                else
                {
                    grdManual.View.OptionsCustomization.AllowFilter = false;
                    grdManual.View.OptionsCustomization.AllowSort = false;
                    grdManual.ShowButtonBar = false;

                    grdManual.DataSource = null;
                }
            }
            finally
            {
                pnlContent.CloseWaitArea();
            }
        }

        #endregion

        #region 검색

        /// <summary> 
        /// 비동기 override 모델, 비동기 모델은 검색에서만 제공합니다. ESC키로 취소 가능합니다.
        /// </summary>
        protected async override Task OnSearchAsync()
        {
            await base.OnSearchAsync();
            TreeListNode beforeTreeListNode = treeManual.FocusedNode;  // 기존 FocusedManual 

            DataRow focusRow = treeManual.GetFocusedDataRow();

            var param = Conditions.GetValues();
            param.Add("P_MENUID", focusRow["MENUID"]);
            await base.OnSearchAsync();

            if (focusRow["MENUTYPE"].ToString().ToUpper().Equals("FOLDER") || focusRow["MENUID"].ToString().Equals("*"))
            {
                grdManual.DataSource = await QueryAsync("SelectMenuGrid", "10001", param);
            }
            else
            {
                grdManual.DataSource = null;
            }
            treeManual.SetFocusedNode(treeManual.FindNodeByKeyID(beforeTreeListNode.GetValue("MENUID")));
            
            TreeListNode afterTreeListNode = treeManual.FocusedNode;
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
            grdManual.View.CheckValidation();

            System.Data.DataTable changed = grdManual.GetChangedRows();

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

            DataRow focusRow = treeManual.GetFocusedDataRow();
            string parentManualId = string.Empty;
            string strModifiedOrDeletedManualId = "";
            string strAddedOrModifiedDispalySequence = "";

            foreach (var g in group)
            {
                // DisplaySequence Check - db.
                parentManualId = g.PARENTMENUID;

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
                        strModifiedOrDeletedManualId += row["MENUID"].ToString() + ";";
                    }

                    else if (state == "deleted")
                    {
                        strModifiedOrDeletedManualId += row["MENUID"].ToString() + ";";
                    }
                }

                if (strModifiedOrDeletedManualId != "")
                {
                    strModifiedOrDeletedManualId = strModifiedOrDeletedManualId.Remove(strModifiedOrDeletedManualId.Length - 1, 1);
                }

                if (strAddedOrModifiedDispalySequence != "")
                {
                    strAddedOrModifiedDispalySequence = strAddedOrModifiedDispalySequence.Remove(strAddedOrModifiedDispalySequence.Length - 1, 1);
                }

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("p_PARENTMENUID", parentManualId);
                param.Add("p_MODIFIEDORDELETEDMENUID", strModifiedOrDeletedManualId);
                param.Add("p_ADDEDORMODIFIEDDISPLAYSEQUENCE", strAddedOrModifiedDispalySequence);


                //DataTable dtDuplicated = Procedure("usp_com_selectDuplicatedDisplaySequenceManual", param) as DataTable;
                System.Data.DataTable dtDuplicated = SqlExecuter.Query("GetDuplicatedMenuDisplaySequence", "10001", param);

                if (dtDuplicated.Rows.Count > 0)
                {
                    foreach (DataRow row in dtDuplicated.Rows)
                    {
                        throw MessageException.Create("DuplicatedDisplaySequence", parentManualId);
                    }
                }
            }
        }

        #endregion

        #region Private Function
        /// <summary>        
        /// 메뉴 데이터 로드
        /// </summary>
        private async void LoadDataManual()
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("p_menuId", "*");
            param.Add("p_validState", "Valid");
            param.Add("p_conditionItem", "*");
            param.Add("p_conditionValue", "");

            grdManual.DataSource = await QueryAsync("SelectMenuGrid", "10001", param);
        }
        #endregion
    }
}
