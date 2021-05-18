using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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

namespace SmartMES
{
    public partial class FavoriteSetting : XtraForm
    {
        DataTable _searchData = new DataTable();
        DataTable _changedData = new DataTable();

        public FavoriteSetting()
        {
            InitializeComponent();

            InitializeContents();
        }

        private void InitializeContents()
        {
            InitializeControls();
            LoadData();
            InitializeBehaviorManager();

            InitializeEvent();
        }

        private void InitializeEvent()
        {
            grdFavoriteList.ToolbarDeleteRow += GrdFavoriteList_ToolbarDeleteRow;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void GrdFavoriteList_ToolbarDeleteRow(object sender, EventArgs e)
        {
            if (grdFavoriteList.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdFavoriteList.View.GetFocusedDataRow();

            DataRow addRow = _changedData.NewRow();
            addRow.ItemArray = row.ItemArray.Clone() as object[];
            addRow["_STATE_"] = "deleted";
            _changedData.Rows.Add(addRow);

            _changedData.AcceptChanges();

            DataTable favoriteMenuList = grdFavoriteList.DataSource as DataTable;
            DataRow[] changingRows = favoriteMenuList.Select("DISPLAYSEQUENCE > " + row["DISPLAYSEQUENCE"].ToString());

            foreach (DataRow changingRow in changingRows)
            {
                changingRow["DISPLAYSEQUENCE"] = Format.GetInteger(changingRow["DISPLAYSEQUENCE"]) - 1;
            }

            favoriteMenuList.AcceptChanges();


            (grdFavoriteList.View.DataSource as DataView).Table.Rows.Remove(row);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (MSGBox.Show(MessageBoxType.Question, "FAVORITESETTINGCONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable resultData = grdFavoriteList.DataSource as DataTable;

                foreach (DataRow row in resultData.Rows)
                {
                    DataRow[] dataRows = _searchData.Select("MENUID = '" + row["MENUID"].ToString() + "'");

                    if (dataRows.Length > 0 && !row["DISPLAYSEQUENCE"].Equals(dataRows.First()["DISPLAYSEQUENCE"]))
                    {
                        DataRow changedRow = _changedData.NewRow();
                        changedRow.ItemArray = row.ItemArray.Clone() as object[];
                        changedRow["_STATE_"] = "modified";

                        _changedData.Rows.Add(changedRow);
                    }
                }

                MessageWorker worker = new MessageWorker("SaveFavoriteMenu");
                worker.SetBody(new MessageBody()
                {
                    { "list", _changedData }
                });

                worker.Execute();

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void InitializeControls()
        {
            grdFavoriteList.View.GridMultiSelectionMode = GridMultiSelectionMode.RowSelect;

            grdFavoriteList.GridButtonItem = GridButtonItem.Delete;

            grdFavoriteList.ShowStatusBar = false;

            grdFavoriteList.View.SetIsReadOnly();

            grdFavoriteList.View.SetAutoFillColumn("MENUNAME");
            //grdFavoriteList.View.SetSortOrder("DISPLAYSEQUENCE");

            grdFavoriteList.View.AddTextBoxColumn("USERID", 90)
                .SetIsHidden();
            grdFavoriteList.View.AddTextBoxColumn("MENUID", 150);
            grdFavoriteList.View.AddTextBoxColumn("MENUNAME", 200);
            grdFavoriteList.View.AddSpinEditColumn("DISPLAYSEQUENCE", 70)
                .SetIsHidden();
            grdFavoriteList.View.AddTextBoxColumn("REGTYPE", 70)
                .SetIsHidden();
            grdFavoriteList.View.AddTextBoxColumn("TXNHISTKEY", 130)
                .SetIsHidden();

            grdFavoriteList.View.SetKeyColumn("MENUID");

            grdFavoriteList.View.PopulateColumns();


            grdFavoriteList.Caption = Language.Get(grdFavoriteList.LanguageKey);
            btnSave.Text = Language.Get(btnSave.LanguageKey);
            btnCancel.Text = Language.Get(btnCancel.LanguageKey);
        }

        private void LoadData()
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("LANGUAGETYPE", UserInfo.Current.LanguageType);
            dicParam.Add("USERID", UserInfo.Current.Id);

            DataTable favoriteMenuList = SqlExecuter.Query("GetFavoriteMenuList", "00001", dicParam);

            grdFavoriteList.DataSource = favoriteMenuList;


            _changedData = grdFavoriteList.GetChangedRows();

            _searchData = favoriteMenuList.Clone();

            foreach (DataRow row in favoriteMenuList.Rows)
            {
                DataRow addRow = _searchData.NewRow();
                addRow.ItemArray = row.ItemArray.Clone() as object[];

                _searchData.Rows.Add(addRow);
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            HandleBehaviorDragDropEvents();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DialogResult == DialogResult.Cancel)
            {
                if (MSGBox.Show(MessageBoxType.Question, "FAVORITESETTINGCANCEL", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }

        }

        private void InitializeBehaviorManager()
        {
            behaviorManager1.SetBehaviors(grdFavoriteList.View, new DevExpress.Utils.Behaviors.Behavior[] {
                ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.DragDrop.DragDropBehavior.Create(typeof(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), true, true, true)))
            });
        }

        private void HandleBehaviorDragDropEvents()
        {
            DragDropBehavior gridControlBehavior = behaviorManager1.GetBehavior<DragDropBehavior>(grdFavoriteList.View);
            gridControlBehavior.DragDrop += GridControlBehavior_DragDrop;
            gridControlBehavior.DragOver += GridControlBehavior_DragOver;
        }

        private void GridControlBehavior_DragDrop(object sender, DragDropEventArgs e)
        {
            GridView targetGrid = e.Target as GridView;
            GridView sourceGrid = e.Source as GridView;

            if (e.Action == DragDropActions.None || targetGrid != sourceGrid)
                return;

            DataTable sourceTable = sourceGrid.GridControl.DataSource as DataTable;

            Point hitPoint = targetGrid.GridControl.PointToClient(Cursor.Position);
            GridHitInfo hitInfo = targetGrid.CalcHitInfo(hitPoint);

            int[] sourceHandles = e.GetData<int[]>();

            int targetRowHandle = hitInfo.RowHandle;
            int targetRowIndex = targetGrid.GetDataSourceRowIndex(targetRowHandle);

            List<DataRow> draggedRows = new List<DataRow>();

            foreach (int sourceHandle in sourceHandles)
            {
                int oldRowIndex = sourceGrid.GetDataSourceRowIndex(sourceHandle);
                DataRow oldRow = sourceTable.Rows[oldRowIndex];
                draggedRows.Add(oldRow);
            }

            int newRowIndex;

            switch (e.InsertType)
            {
                case InsertType.After:
                    newRowIndex = targetRowIndex < sourceHandles[0] ? targetRowIndex + 1 : targetRowIndex;

                    for (int i = 0; i < draggedRows.Count; i++)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }
                    break;

                case InsertType.Before:
                    newRowIndex = targetRowIndex > sourceHandles[sourceHandles.Length - 1] ? targetRowIndex - 1 : targetRowIndex;

                    for (int i = draggedRows.Count - 1; i >= 0; i--)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }
                    break;
                default:
                    newRowIndex = -1;
                    break;
            }

            int insertedIndex = targetGrid.GetRowHandle(newRowIndex);

            targetGrid.FocusedRowHandle = insertedIndex;
            targetGrid.SelectRow(targetGrid.FocusedRowHandle);


            
            DataTable favoriteMenuList = grdFavoriteList.DataSource as DataTable;
            DataRow[] changingRows = null;
            int targetSequence = Format.GetInteger(favoriteMenuList.Rows[newRowIndex]["DISPLAYSEQUENCE"]);
            int startSequence = 0;
            int endSequence = 0;
            int changeSequence = 0;

            if (targetSequence - 1 > newRowIndex)
            {
                startSequence = (newRowIndex - 1) < 0 ? 0 : Format.GetInteger(favoriteMenuList.Rows[newRowIndex - 1]["DISPLAYSEQUENCE"]);
                endSequence = targetSequence;
                changeSequence = 1;
            }
            else if (targetSequence - 1 < newRowIndex)
            {
                startSequence = targetSequence;
                endSequence = (newRowIndex + 1) >= favoriteMenuList.Rows.Count ? (favoriteMenuList.Rows.Count + 1) : Format.GetInteger(favoriteMenuList.Rows[newRowIndex + 1]["DISPLAYSEQUENCE"]);
                changeSequence = -1;
            }

            //if (insertedIndex > newRowIndex)
            //{
            //    changingRows = favoriteMenuList.Select("DISPLAYSEQUENCE > " + newRowIndex.ToString());
            //    changeSequence = 1;
            //}
            //else if (insertedIndex <= newRowIndex)
            //{
            //    changingRows = favoriteMenuList.Select("DISPLAYSEQUENCE <= " + (newRowIndex + 1).ToString());
            //    changeSequence = -1;
            //}

            changingRows = favoriteMenuList.Select("DISPLAYSEQUENCE > " + startSequence.ToString() + " AND DISPLAYSEQUENCE < " + endSequence.ToString());

            //if (e.InsertType == InsertType.After)
            //    changeSequence = -1;
            //else if (e.InsertType == InsertType.Before)
            //    changeSequence = 1;

            if (changingRows != null && changingRows.Length > 0)
            {
                foreach (DataRow row in changingRows)
                {
                    row["DISPLAYSEQUENCE"] = Format.GetInteger(row["DISPLAYSEQUENCE"]) + changeSequence;
                }
            }

            targetGrid.SetFocusedRowCellValue("DISPLAYSEQUENCE", newRowIndex + 1);
            //favoriteMenuList.Rows.Cast<DataRow>().OrderBy(row => row["DISPLAYSEQUENCE"]);

            favoriteMenuList.AcceptChanges();


            targetGrid.RefreshData();
        }

        private void GridControlBehavior_DragOver(object sender, DragOverEventArgs e)
        {
            DragOverGridEventArgs args = DragOverGridEventArgs.GetDragOverGridEventArgs(e);

            e.InsertType = args.InsertType;
            e.InsertIndicatorLocation = args.InsertIndicatorLocation;
            e.Action = args.Action;
            Cursor.Current = args.Cursor;

            args.Handled = true;
        }
    }
}
