using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.Utils.DragDrop;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;

namespace Micube.SmartMES.SystemManagement
{
    public partial class ConditionInput_Popup : SmartPopupBaseForm, ISmartCustomPopup
    {
        #region ISmartCustomPopup

        public DataRow CurrentDataRow { get; set; }

        #endregion

        public string _Type = "";
        public string _Result = "";

        private string CurrentValue = "";

        public ConditionInput_Popup()
        {
            InitializeComponent();

            InitializeGird();
            InitializeBehaviorManager();
            InitializeEvent();
        }

        #region Initialize

        private void InitializeGird()
        {
            grdConditionInput.GridButtonItem -= GridButtonItem.All;
            grdConditionInput.GridButtonItem |= GridButtonItem.Add;
            grdConditionInput.GridButtonItem |= GridButtonItem.Delete;

            grdConditionInput.View.AddTextBoxColumn("KEY", 120);
            grdConditionInput.View.AddTextBoxColumn("VALUE", 120);

            grdConditionInput.View.PopulateColumns();

            grdConditionInput.View.EnableRowStateStyle = false;
            grdConditionInput.View.SetAutoFillColumn("VALUE");
            grdConditionInput.View.OptionsSelection.MultiSelect = false;


            grdReservedWord.View.AddTextBoxColumn("CODEID");
            grdReservedWord.View.AddTextBoxColumn("CODENAME");

            grdReservedWord.View.PopulateColumns();

            grdReservedWord.View.SetIsReadOnly();
            grdReservedWord.View.OptionsView.ColumnAutoWidth = true;
            grdReservedWord.View.OptionsView.ShowIndicator = false;
            grdReservedWord.View.OptionsCustomization.AllowSort = false;
            grdReservedWord.View.OptionsCustomization.AllowFilter = false;

            grdReservedWord.GridButtonItem = GridButtonItem.None;
            grdReservedWord.ShowStatusBar = false;
        }

        private void InitializeEvent()
        {
            Load += ConditionInput_Popup_Load;
            Shown += ConditionInput_Popup_Shown;
            //FormClosing += ConditionInput_Popup_FormClosing;

            grdConditionInput.ToolbarDeleteRow += GrdConditionInput_ToolbarDeleteRow;
            grdConditionInput.View.CellValueChanged += View_CellValueChanged;

            grdReservedWord.View.DoubleClick += View_DoubleClick;
            grdReservedWord.View.FocusedRowChanged += View_FocusedRowChanged;

            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void InitializeBehaviorManager()
        {
            behaviorManager1.SetBehaviors(grdConditionInput.View, new DevExpress.Utils.Behaviors.Behavior[] {
                ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.DragDrop.DragDropBehavior.Create(typeof(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), true, true, true)))
            });
        }

        #endregion

        #region Event

        private void ConditionInput_Popup_Load(object sender, EventArgs e)
        {
            Dictionary<string, object> dicParam = new Dictionary<string, object>
            {
                { "LANGUAGETYPE", UserInfo.Current.LanguageType },
                { "CODECLASSID", "ConditionReservedWord" }
            };

            grdReservedWord.DataSource = SqlExecuter.Query("GetCodeList", "00001", dicParam);


            CurrentValue = CurrentDataRow[_Type].ToString();

            SetConditionInputData();
            SetFormCaptionByLanguage();

            HandleBehaviorDragDropEvents();
        }

        private void ConditionInput_Popup_Shown(object sender, EventArgs e)
        {
            //if (grdConditionInput.View.RowCount < 1)
            //{
            //    grdConditionInput.View.AddNewRow();

            //    grdConditionInput.View.FocusedRowHandle = 0;
            //    grdConditionInput.View.FocusedColumn = grdConditionInput.View.Columns["KEY"];

            //    grdConditionInput.View.ShowEditor();
            //}

            if (_Type == "CONSTANTDATA")
            {
                splitterControl1.Visible = false;
                smartPanel1.Visible = false;
            }
            else if (_Type == "EXECUTEPARAMETER")
            {
                splitterControl1.Visible = true;
                smartPanel1.Visible = true;
            }

            SetGridColumnHeaderByLanguage();
        }

        private void GrdConditionInput_ToolbarDeleteRow(object sender, EventArgs e)
        {
            if (grdConditionInput.View.FocusedRowHandle < 0)
                return;

            DataRow row = grdConditionInput.View.GetFocusedDataRow();

            (grdConditionInput.View.DataSource as DataView).Table.Rows.Remove(row);
        }

        private void View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "KEY")
            {
                if (_Type == "EXECUTEPARAMETER")
                    return;

                if (string.IsNullOrWhiteSpace(grdConditionInput.View.GetRowCellValue(e.RowHandle, "VALUE").ToString()))
                {
                    string strKey = e.Value.ToString();

                    grdConditionInput.View.SetRowCellValue(e.RowHandle, "VALUE", strKey);
                }
            }

            //if (e.Column.FieldName == "VALUE")
            //{
            //    if (e.RowHandle == grdConditionInput.View.RowCount - 1)
            //    {
            //        grdConditionInput.View.CellValueChanged -= View_CellValueChanged;

            //        grdConditionInput.View.AddNewRow();
            //        //grdConditionInput.View.UpdateCurrentRow();
            //        grdConditionInput.View.FocusedRowHandle = e.RowHandle;
            //        grdConditionInput.View.FocusedColumn = e.Column;

            //        grdConditionInput.View.CellValueChanged += View_CellValueChanged;
            //    }
            //}
        }

        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (grdConditionInput.View.FocusedRowHandle < 0)
                return;

            int iFocusedRowHandle = grdConditionInput.View.FocusedRowHandle;
            GridColumn FocusedColumn = grdConditionInput.View.FocusedColumn;
            string strConditionInput = grdConditionInput.View.GetFocusedRowCellValue(FocusedColumn).ToString();

            string strSelectedWord = grdReservedWord.View.GetFocusedRowCellValue("CODEID").ToString();

            grdConditionInput.View.SetRowCellValue(iFocusedRowHandle, "VALUE", strConditionInput + strSelectedWord);
        }

        private void View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            string strReservedWord = grdReservedWord.View.GetRowCellValue(e.FocusedRowHandle, "CODEID").ToString();

            if (strReservedWord.Contains("AddDays"))
            {
                strReservedWord = strReservedWord.Replace("}", ":1}");
            }

            txtUseExample.Text = strReservedWord;
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
                default:newRowIndex = -1;
                    break;
            }

            int insertedIndex = targetGrid.GetRowHandle(newRowIndex);

            targetGrid.FocusedRowHandle = insertedIndex;
            targetGrid.SelectRow(targetGrid.FocusedRowHandle);
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

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            grdConditionInput.View.PostEditor();
            grdConditionInput.View.UpdateCurrentRow();

            bool bIsEmptyData = false;
            string strResult = "";
            DataTable dtConditionInput = grdConditionInput.DataSource as DataTable;

            for (int i = 0; i < dtConditionInput.Rows.Count; i++)
            {
                DataRow row = dtConditionInput.Rows[i];

                if (string.IsNullOrWhiteSpace(row["KEY"].ToString()) && string.IsNullOrWhiteSpace(row["VALUE"].ToString()))
                {
                    if (i != dtConditionInput.Rows.Count - 1)
                        bIsEmptyData = true;

                    continue;
                }
                else
                {
                    strResult += row["KEY"].ToString() + "=" + row["VALUE"].ToString() + ";";
                }
            }

            if (bIsEmptyData && _Type == "CONSTANTDATA")
            {
                if (ShowMessage(MessageBoxButtons.YesNo, "ConditionInputPopupApply") == DialogResult.Yes)
                {
                    _Result = strResult;
                    CurrentDataRow[_Type] = strResult;

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    return;
                }
            }

            _Result = strResult;
            CurrentDataRow[_Type] = strResult;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DialogResult == DialogResult.Cancel)
            {
                if (ShowMessage(MessageBoxButtons.YesNo, "ConditionInputPopupCancel", Language.Get(_Type)) == DialogResult.Yes)
                {
                    _Result = CurrentValue;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void ConditionInput_Popup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                if (ShowMessage(MessageBoxButtons.YesNo, "ConditionInputPopupCancel", Language.Get(_Type)) == DialogResult.Yes)
                {
                    _Result = CurrentValue;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region Method

        private void SetConditionInputData()
        {
            if (string.IsNullOrEmpty(CurrentValue))
                return;

            string[] strCurrentValue = CurrentValue.Split(';');
            DataTable dtConditionInput = grdConditionInput.DataSource as DataTable;

            foreach (string value in strCurrentValue)
            {
                if (string.IsNullOrWhiteSpace(value))
                    continue;

                string[] strValue = value.Split('=');

                if (strValue.Length == 2)
                    dtConditionInput.Rows.Add(strValue[0], strValue[1]);
            }

            dtConditionInput.AcceptChanges();
        }

        private void SetGridColumnHeaderByLanguage()
        {
            if (_Type == "CONSTANTDATA")
            {
                grdConditionInput.View.Columns["KEY"].Caption = Language.Get("COLUMNID");
                grdConditionInput.View.Columns["KEY"].ToolTip = Language.Get("COLUMNID");
                grdConditionInput.View.Columns["VALUE"].Caption = Language.Get("DICTIONARYID");
                grdConditionInput.View.Columns["VALUE"].ToolTip = Language.Get("DICTIONARYID");
            }
            else if (_Type == "EXECUTEPARAMETER")
            {
                grdConditionInput.View.Columns["KEY"].Caption = Language.Get("PARAMETERID");
                grdConditionInput.View.Columns["KEY"].ToolTip = Language.Get("PARAMETERID");
                grdConditionInput.View.Columns["VALUE"].Caption = Language.Get("PARAMETERVALUE");
                grdConditionInput.View.Columns["VALUE"].ToolTip = Language.Get("PARAMETERVALUE");
            }

            grdReservedWord.View.Columns["CODEID"].Caption = Language.Get("RESERVEDWORD");
            grdReservedWord.View.Columns["CODEID"].ToolTip = Language.Get("RESERVEDWORD");
            grdReservedWord.View.Columns["CODENAME"].Caption = Language.Get("DESCRIPTION");
            grdReservedWord.View.Columns["CODENAME"].ToolTip = Language.Get("DESCRIPTION");
        }

        private void SetFormCaptionByLanguage()
        {
            if (_Type == "CONSTANTDATA")
            {
                Text = Language.Get("CONSTANTDATAINPUT");
                grdConditionInput.LanguageKey = "GRIDCONSTANTDATALIST";
            }
            else if (_Type == "EXECUTEPARAMETER")
            {
                Text = Language.Get("EXECUTEPARAMETERINPUT");
                grdConditionInput.LanguageKey = "GRIDEXECUTEPARAMETERLIST";
            }
        }

        private void HandleBehaviorDragDropEvents()
        {
            DragDropBehavior gridControlBehavior = behaviorManager1.GetBehavior<DragDropBehavior>(grdConditionInput.View);
            gridControlBehavior.DragDrop += GridControlBehavior_DragDrop;
            gridControlBehavior.DragOver += GridControlBehavior_DragOver;
        }

        #endregion
    }
}
