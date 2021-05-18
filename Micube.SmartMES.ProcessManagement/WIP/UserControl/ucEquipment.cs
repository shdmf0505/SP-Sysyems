#region using
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.Utils;

using Micube.Framework;
using Micube.Framework.Net;
using Micube.Framework.SmartControls;
using Micube.Framework.SmartControls.Grid;
using Micube.Framework.SmartControls.Grid.BandedGrid;

using Micube.SmartMES.Commons;
using Micube.SmartMES.Commons.Controls;
using Micube.Framework.SmartControls.Forms;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion


namespace Micube.SmartMES.ProcessManagement
{
    public delegate void delEquipmentCheckedChange();
    public partial class ucEquipment : UserControl
    {
        public event delEquipmentCheckedChange EquipmentCheckedChange;
        public ProcessType ProcessType { get; set; }
        public DataTable _lotInfo;
        public ucEquipment()
        {
            InitializeComponent();

            InitializeEvent();

            if(!this.IsDesignMode())
                InitializeControls();
        }

        private void InitializeControls()
        {
            #region 설비 그리드
            ConditionItemSelectPopup equipmentCondition = new ConditionItemSelectPopup();
            equipmentCondition.Id = "EQUIPMENT";
            equipmentCondition.SearchQuery = new SqlQueryAdapter();
            equipmentCondition.ValueFieldName = "EQUIPMENTID";
            equipmentCondition.DisplayFieldName = "EQUIPMENTNAME";
            equipmentCondition.SetPopupLayout("SELECTEQUIPMENT", PopupButtonStyles.Ok_Cancel, true, false);
            equipmentCondition.SetPopupResultCount(1);
            equipmentCondition.SetPopupLayoutForm(400, 600, FormBorderStyle.SizableToolWindow);
            equipmentCondition.SetPopupAutoFillColumns("EQUIPMENTNAME");
            equipmentCondition.SetPopupApplySelection((selectedRows, dataGridRow) =>
            {
                if (selectedRows.Count() > 0)
                {
                    DataRow row = selectedRows.FirstOrDefault();

                    grdEquipment.View.CheckRow(grdEquipment.View.LocateByValue("EQUIPMENTID", row["EQUIPMENTID"]), true);

                    txtEquipment.Editor.SetValue(null);
                    txtEquipment.Editor.Text = "";
                }
            });

            equipmentCondition.Conditions.AddTextBox("EQUIPMENT");

            equipmentCondition.GridColumns.AddTextBoxColumn("EQUIPMENTID", 150);
            equipmentCondition.GridColumns.AddTextBoxColumn("EQUIPMENTNAME", 200);

            txtEquipment.Editor.SelectPopupCondition = equipmentCondition;


            grdEquipment.GridButtonItem = GridButtonItem.None;
            grdEquipment.ShowButtonBar = false;
            grdEquipment.ShowStatusBar = false;

            grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;
            grdEquipment.View.EnableRowStateStyle = false;
            grdEquipment.View.CheckMarkSelection.ShowCheckBoxHeader = false;
            grdEquipment.View.SetSortOrder("EQUIPMENTID");

            // 설비ID
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsReadOnly();
            // 설비명
            grdEquipment.View.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetIsReadOnly();
            // 작업 시작 일시
            var colTrackinTime = grdEquipment.View.AddTextBoxColumn("TRACKINTIME", 130)
                .SetIsReadOnly();
            // 작업 종료 일시
            var colTrackoutTime = grdEquipment.View.AddTextBoxColumn("TRACKOUTTIME", 130)
                .SetIsReadOnly();
            // PCS 수량
            grdEquipment.View.AddSpinEditColumn("PCSQTY", 80)
                .SetValidationIsRequired();
            // PLN 수량
            grdEquipment.View.AddSpinEditColumn("PNLQTY", 80)
                .SetValidationIsRequired();
            // Recipe
            InitializeRecipeIdPopup();
            // Recipe Version
            grdEquipment.View.AddTextBoxColumn("RECIPEVERSION", 80)
                .SetIsReadOnly();
            // Recipe Type
            grdEquipment.View.AddComboBoxColumn("RECIPETYPE", 150, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RecipeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly();


            grdEquipment.View.PopulateColumns();


            grdEquipment.View.OptionsView.ShowIndicator = false;

            #endregion

            #region 설비 레시피 그리드
            grdEquipmentRecipe.GridButtonItem = GridButtonItem.None;
            grdEquipmentRecipe.ShowButtonBar = false;
            grdEquipmentRecipe.ShowStatusBar = false;

            grdEquipmentRecipe.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;
            //grdEquipmentRecipe.View.SetIsReadOnly();

            // 설비 코드
            grdEquipmentRecipe.View.AddTextBoxColumn("EQUIPMENTID", 150)
                .SetIsReadOnly();
            // 설비명
            grdEquipmentRecipe.View.AddTextBoxColumn("EQUIPMENTNAME", 200)
                .SetIsReadOnly();
            // Recipe Id
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPEID", 120)
                .SetIsReadOnly();
            // Recipe Version
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPEVERSION", 70)
                .SetIsReadOnly();
            // Recipe Name
            grdEquipmentRecipe.View.AddTextBoxColumn("RECIPENAME", 150)
                .SetIsReadOnly();
            // Parameter Id
            grdEquipmentRecipe.View.AddTextBoxColumn("PARAMETERID", 120)
                .SetIsReadOnly();
            // Parameter Name
            grdEquipmentRecipe.View.AddTextBoxColumn("PARAMETERNAME", 150)
                .SetIsReadOnly();
            // 하한값
            grdEquipmentRecipe.View.AddSpinEditColumn("LSL", 80)
                .SetIsReadOnly();
            // 목표값
            grdEquipmentRecipe.View.AddSpinEditColumn("TARGET", 80)
                .SetIsReadOnly();
            // 상한값
            grdEquipmentRecipe.View.AddSpinEditColumn("USL", 80)
                .SetIsReadOnly();
            // Validation Type
            grdEquipmentRecipe.View.AddTextBoxColumn("VALIDATIONTYPE", 120)
                .SetIsReadOnly();
            // Data Type
            grdEquipmentRecipe.View.AddComboBoxColumn("DATATYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RecipeParameterDataType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"))
                .SetIsReadOnly()
                .SetTextAlignment(TextAlignment.Center);
            // Value
            grdEquipmentRecipe.View.AddTextBoxColumn("VALUE", 100);


            grdEquipmentRecipe.View.PopulateColumns();


            grdEquipmentRecipe.View.OptionsView.ShowIndicator = false;
            #endregion


        }
        public void SetEquipmentDataSource(object dataSource, List<string> lotEquipment = null)
        {
            grdEquipment.DataSource = dataSource;

            DataTable equipmentList = dataSource as DataTable;
            if (equipmentList.Rows.Count == 1)
            {
                grdEquipment.View.CheckedAll();

            }
            if (EquipmentCheckedChange != null)
            {
                EquipmentCheckedChange();
            }

        }
        public void SetEquipmentRecipeDataSource(DataTable dtRecipe)
        {
            grdEquipmentRecipe.DataSource = dtRecipe;
        }
        public void SetRecipeComboDataSource(object dataSource, string queryVersion = "10001")
        {
            if (ProcessType == ProcessType.StartWork)
            {
                grdEquipment.View.Columns["RECIPEID"].OptionsColumn.AllowEdit = true;

                Dictionary<string, object> param = dataSource as Dictionary<string, object>;

                string recipeFieldName = Format.GetString(param["RECIPENAME"]);
                string productId = Format.GetString(param["PRODUCTID"]);
                string productVersion = Format.GetString(param["PRODUCTVERSION"]);
                string processDefId = Format.GetString(param["PROCESSDEFID"]);
                string processDefVersion = Format.GetString(param["PROCESSDEFVERSION"]);
                string segmentId = Format.GetString(param["SEGMENTID"]);
                string resourceId = Format.GetString(param["RESOURCEID"]);
                string lotId = Format.GetString(param["LOTID"]);

                ConditionItemSelectPopup conditionItem = grdEquipment.View.GetConditions().GetCondition<ConditionItemSelectPopup>("RECIPEID");
                conditionItem.SearchQuery = new SqlQuery("GetEquipmentRecipeList", queryVersion, $"RECIPENAME={recipeFieldName}", $"PRODUCTID={productId}", $"PRODUCTVERSION={productVersion}", $"PROCESSDEFID={processDefId}", $"PROCESSDEFVERSION={processDefVersion}", $"SEGMENTID={segmentId}", $"RESOURCEID={resourceId}", $"LOTID={lotId}");
            }
            else if (ProcessType == ProcessType.WorkCompletion)
            {
                //grdEquipment.View.GetConditions().GetCondition<ConditionItemSelectPopup>("RECIPEID").SetIsReadOnly().SetSearchButtonReadOnly().SetClearButtonReadOnly();
                grdEquipment.View.Columns["RECIPEID"].OptionsColumn.AllowEdit = false;
            }
        }
        private void InitializeEvent()
        {
            grdEquipment.View.CheckStateChanged += EquipmentView_CheckStateChanged;
            grdEquipment.View.CellValueChanged += EquipmentView_CellValueChanged;
            grdEquipment.View.RowStyle += EquipmentView_RowStyle;
        }
        private void EquipmentView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            string isEquipmentid = grdEquipment.View.GetRowCellValue(e.RowHandle, "EQUIPMENTID").ToString();

            DataTable dt = grdEquipmentRecipe.DataSource as DataTable;

            if (dt == null) return;

            int cnt = dt.AsEnumerable().Where(c => Format.GetTrimString(c["EQUIPMENTID"]).Equals(isEquipmentid)).Count();

            if (cnt > 0)
            {
                e.Appearance.BackColor = Color.Green;
                e.HighPriority = true;
            }
        }
        private void EquipmentView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "PCSQTY")
            {
                grdEquipment.View.CellValueChanged -= EquipmentView_CellValueChanged;

                decimal panelPerQty = Format.GetDecimal(_lotInfo.Rows[0]["PANELPERQTY"]);
                grdEquipment.View.SetFocusedRowCellValue("PNLQTY", Math.Ceiling(Format.GetDecimal(e.Value) / panelPerQty));

                grdEquipment.View.CellValueChanged += EquipmentView_CellValueChanged;
            }
            else if (e.Column.FieldName == "PNLQTY")
            {
                grdEquipment.View.CellValueChanged -= EquipmentView_CellValueChanged;

                int panelPerQty = Format.GetInteger(_lotInfo.Rows[0]["PANELPERQTY"]);
                grdEquipment.View.SetFocusedRowCellValue("PCSQTY", Format.GetInteger(e.Value) * panelPerQty);

                grdEquipment.View.CellValueChanged += EquipmentView_CellValueChanged;
            }
        }

        private void EquipmentView_CheckStateChanged(object sender, EventArgs e)
        {

            DataTable dataSource = grdEquipment.View.GetCheckedRows();
            //grdEquipment.View.CheckedAll();
            if (EquipmentCheckedChange != null)
            {
                EquipmentCheckedChange();
            }

        }
        public DataTable GetEquipmentDataSource()
        {
            return grdEquipment.DataSource as DataTable;
        }
        public DataTable GetEquipmentCheckeRow()
        {
            return grdEquipment.View.GetCheckedRows() as DataTable;
        }
        public DataTable GetEquipmentRecipeDataSource()
        {
            return grdEquipmentRecipe.DataSource as DataTable;
        }
        public void SetEquipmentWorkTimeColumnHidden()
        {
            //grdEquipment.View.Columns["TRACKINTIME"].Visible = false;
            //grdEquipment.View.Columns["TRACKOUTTIME"].Visible = false;

            if (ProcessType == ProcessType.StartWork)
            {
                grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CheckBoxSelect;

                grdEquipment.View.GetConditions().GetCondition<ConditionItemTextBox>("TRACKINTIME").SetIsHidden();
                grdEquipment.View.GetConditions().GetCondition<ConditionItemTextBox>("TRACKOUTTIME").SetIsHidden();
                grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PCSQTY").SetIsHidden();
                grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PNLQTY").SetIsHidden();
            }

            if (ProcessType == ProcessType.WorkCompletion)
            {
                grdEquipment.View.GridMultiSelectionMode = GridMultiSelectionMode.CellSelect;

                if (UserInfo.Current.Enterprise == Constants.EnterPrise_InterFlex)
                {
                    grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PCSQTY").SetIsHidden();
                    grdEquipment.View.GetConditions().GetCondition<ConditionItemSpinEdit>("PNLQTY").SetIsHidden();
                }
            }

            grdEquipment.View.PopulateColumns();
        }
        public void SetLotInfo(DataTable lotinfo)
        {
            _lotInfo = lotinfo;
        }
        public void ClearDatas()
        {
            txtEquipment.Text = string.Empty;
            txtEquipmentId.Editor.Text = string.Empty;
            txtEquipmentId.Editor.EditValue = null;

            grdEquipment.View.ClearDatas();
            grdEquipmentRecipe.View.ClearDatas();
        }
        private void InitializeRecipeIdPopup()
        {
            var recipeIdColumn = grdEquipment.View.AddSelectPopupColumn("RECIPEID", 120, new SqlQuery("GetEquipmentRecipeList", "10001"), "RECIPEID")
                .SetPopupLayout("SELECTRECIPE", PopupButtonStyles.Ok_Cancel, true, true)
                .SetPopupResultCount(1)
                .SetPopupLayoutForm(600, 800)
                .SetPopupAutoFillColumns("RECIPETYPE")
                .SetPopupApplySelection((selectedRows, gridRow) =>
                {
                    DataRow row = selectedRows.FirstOrDefault();

                    if (row == null)
                    {
                        gridRow["RECIPEVERSION"] = string.Empty;
                        gridRow["RECIPETYPE"] = string.Empty;
                    }
                    else
                    {
                        gridRow["RECIPEVERSION"] = Format.GetString(row["RECIPEVERSION"]);
                        gridRow["RECIPETYPE"] = Format.GetString(row["RECIPETYPE"]);
                    }
                });

            recipeIdColumn.Conditions.AddTextBox("EQUIPMENTID")
                .SetPopupDefaultByGridColumnId("EQUIPMENTID")
                .SetIsReadOnly();

            recipeIdColumn.GridColumns.AddTextBoxColumn("EQUIPMENTID", 80).SetIsHidden();
            recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPEID", 100);
            recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPEVERSION", 80);
            recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPENAME", 150);
            recipeIdColumn.GridColumns.AddTextBoxColumn("RECIPETYPE", 100);
            recipeIdColumn.GridColumns.AddComboBoxColumn("RECIPETYPE", 100, new SqlQuery("GetCodeList", "00001", "CODECLASSID=RecipeType", $"LANGUAGETYPE={UserInfo.Current.LanguageType}"));
        }

        public void EnableColumn(string columnName)
        {
            grdEquipment.View.Columns[columnName].OptionsColumn.AllowEdit = true;
        }

        public void DisableColumn(string columnName)
        {
            grdEquipment.View.Columns[columnName].OptionsColumn.AllowEdit = false;
        }
    }
}
