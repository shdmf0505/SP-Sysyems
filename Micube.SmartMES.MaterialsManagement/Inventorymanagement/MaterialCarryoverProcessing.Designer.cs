namespace Micube.SmartMES.MaterialsManagement
{
    partial class MaterialCarryoverProcessing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialCarryoverProcessing));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.popupOspWarehouseid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblPlant = new Micube.Framework.SmartControls.SmartLabel();
            this.lbMonth = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPlantid = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblwarehousename = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.dtpEndMonth = new Micube.Framework.SmartControls.SmartDateEdit();
            this.dtpStartMonth = new Micube.Framework.SmartControls.SmartDateEdit();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspWarehouseid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(0, 36);
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(0, 0);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1226, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(1226, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 5;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popupOspWarehouseid, 3, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.dtpEndMonth, 1, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblPlant, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lbMonth, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboPlantid, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel1, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblwarehousename, 2, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 1, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 4;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1226, 911);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // popupOspWarehouseid
            // 
            this.popupOspWarehouseid.LabelText = null;
            this.popupOspWarehouseid.LanguageKey = null;
            this.popupOspWarehouseid.Location = new System.Drawing.Point(453, 3);
            this.popupOspWarehouseid.Name = "popupOspWarehouseid";
            conditionItemSelectPopup2.ApplySelection = null;
            conditionItemSelectPopup2.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup2.AutoFillColumnNames")));
            conditionItemSelectPopup2.CanOkNoSelection = true;
            conditionItemSelectPopup2.ClearButtonRealOnly = false;
            conditionItemSelectPopup2.ClearButtonVisible = true;
            conditionItemSelectPopup2.ConditionDefaultId = null;
            conditionItemSelectPopup2.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            conditionItemSelectPopup2.ConditionRequireId = "";
            conditionItemSelectPopup2.Conditions = conditionCollection3;
            conditionItemSelectPopup2.CustomPopup = null;
            conditionItemSelectPopup2.CustomValidate = null;
            conditionItemSelectPopup2.DefaultDisplayValue = null;
            conditionItemSelectPopup2.DefaultValue = null;
            conditionItemSelectPopup2.DisplayFieldName = "";
            conditionItemSelectPopup2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            conditionItemSelectPopup2.GreatThenEqual = false;
            conditionItemSelectPopup2.GreatThenId = "";
            conditionItemSelectPopup2.GridColumns = conditionCollection4;
            conditionItemSelectPopup2.Id = null;
            conditionItemSelectPopup2.InitAction = null;
            conditionItemSelectPopup2.IsCaptionWordWrap = false;
            conditionItemSelectPopup2.IsEnabled = true;
            conditionItemSelectPopup2.IsHidden = false;
            conditionItemSelectPopup2.IsImmediatlyUpdate = true;
            conditionItemSelectPopup2.IsKeyColumn = false;
            conditionItemSelectPopup2.IsMultiGrid = false;
            conditionItemSelectPopup2.IsReadOnly = false;
            conditionItemSelectPopup2.IsRequired = false;
            conditionItemSelectPopup2.IsSearchOnLoading = true;
            conditionItemSelectPopup2.IsUseMultiColumnPaste = true;
            conditionItemSelectPopup2.IsUseRowCheckByMouseDrag = false;
            conditionItemSelectPopup2.LabelText = null;
            conditionItemSelectPopup2.LanguageKey = null;
            conditionItemSelectPopup2.LessThenEqual = false;
            conditionItemSelectPopup2.LessThenId = "";
            conditionItemSelectPopup2.NoSelectionMessageId = "";
            conditionItemSelectPopup2.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup2.PopupCustomValidation = null;
            conditionItemSelectPopup2.Position = 0D;
            conditionItemSelectPopup2.QueryPopup = null;
            conditionItemSelectPopup2.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup2.RelationIds")));
            conditionItemSelectPopup2.ResultAction = null;
            conditionItemSelectPopup2.ResultCount = 1;
            conditionItemSelectPopup2.SearchButtonReadOnly = false;
            conditionItemSelectPopup2.SearchQuery = null;
            conditionItemSelectPopup2.SearchText = null;
            conditionItemSelectPopup2.SearchTextControlId = null;
            conditionItemSelectPopup2.SelectionQuery = null;
            conditionItemSelectPopup2.ShowSearchButton = true;
            conditionItemSelectPopup2.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup2.Title = null;
            conditionItemSelectPopup2.ToolTip = null;
            conditionItemSelectPopup2.ToolTipLanguageKey = null;
            conditionItemSelectPopup2.ValueFieldName = "";
            conditionItemSelectPopup2.WindowSize = new System.Drawing.Size(800, 500);
            this.popupOspWarehouseid.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupOspWarehouseid.Size = new System.Drawing.Size(174, 24);
            this.popupOspWarehouseid.TabIndex = 15;
            // 
            // lblPlant
            // 
            this.lblPlant.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblPlant.Appearance.Options.UseForeColor = true;
            this.lblPlant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlant.LanguageKey = "PLANTID";
            this.lblPlant.Location = new System.Drawing.Point(3, 3);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(144, 24);
            this.lblPlant.TabIndex = 0;
            this.lblPlant.Text = "smartLabel1";
            // 
            // lbMonth
            // 
            this.lbMonth.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbMonth.Appearance.Options.UseForeColor = true;
            this.lbMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMonth.LanguageKey = "FROM";
            this.lbMonth.Location = new System.Drawing.Point(3, 33);
            this.lbMonth.Name = "lbMonth";
            this.lbMonth.Size = new System.Drawing.Size(144, 24);
            this.lbMonth.TabIndex = 1;
            this.lbMonth.Text = "smartLabel2";
            // 
            // cboPlantid
            // 
            this.cboPlantid.LabelText = null;
            this.cboPlantid.LanguageKey = null;
            this.cboPlantid.Location = new System.Drawing.Point(153, 3);
            this.cboPlantid.Name = "cboPlantid";
            this.cboPlantid.PopupWidth = 0;
            this.cboPlantid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlantid.Properties.NullText = "";
            this.cboPlantid.ShowHeader = true;
            this.cboPlantid.Size = new System.Drawing.Size(144, 24);
            this.cboPlantid.TabIndex = 3;
            this.cboPlantid.VisibleColumns = null;
            this.cboPlantid.VisibleColumnsWidth = null;
            // 
            // lblwarehousename
            // 
            this.lblwarehousename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblwarehousename.LanguageKey = "WAREHOUSENAME";
            this.lblwarehousename.Location = new System.Drawing.Point(303, 3);
            this.lblwarehousename.Name = "lblwarehousename";
            this.lblwarehousename.Size = new System.Drawing.Size(144, 24);
            this.lblwarehousename.TabIndex = 4;
            this.lblwarehousename.Text = "smartLabel3";
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.smartPanel1, 2);
            this.smartPanel1.Controls.Add(this.dtpStartMonth);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(150, 30);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(300, 30);
            this.smartPanel1.TabIndex = 5;
            // 
            // dtpEndMonth
            // 
            this.dtpEndMonth.EditValue = null;
            this.dtpEndMonth.Enabled = false;
            this.dtpEndMonth.LabelText = null;
            this.dtpEndMonth.LanguageKey = null;
            this.dtpEndMonth.Location = new System.Drawing.Point(153, 63);
            this.dtpEndMonth.Name = "dtpEndMonth";
            this.dtpEndMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndMonth.Properties.DisplayFormat.FormatString = "yyyy-MM";
            this.dtpEndMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEndMonth.Properties.Mask.EditMask = "yyyy-MM";
            this.dtpEndMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpEndMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpEndMonth.Size = new System.Drawing.Size(144, 24);
            this.dtpEndMonth.TabIndex = 0;
            // 
            // dtpStartMonth
            // 
            this.dtpStartMonth.EditValue = null;
            this.dtpStartMonth.LabelText = null;
            this.dtpStartMonth.LanguageKey = null;
            this.dtpStartMonth.Location = new System.Drawing.Point(3, 3);
            this.dtpStartMonth.Name = "dtpStartMonth";
            this.dtpStartMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartMonth.Properties.DisplayFormat.FormatString = "yyyy-MM";
            this.dtpStartMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStartMonth.Properties.EditFormat.FormatString = "yyyy-MM";
            this.dtpStartMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStartMonth.Properties.Mask.EditMask = "yyyy-MM";
            this.dtpStartMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpStartMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpStartMonth.Size = new System.Drawing.Size(144, 24);
            this.dtpStartMonth.TabIndex = 0;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.LanguageKey = "TO";
            this.smartLabel1.Location = new System.Drawing.Point(3, 63);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(144, 24);
            this.smartLabel1.TabIndex = 4;
            this.smartLabel1.Text = "smartLabel3";
            // 
            // MaterialCarryoverProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.ConditionsVisible = false;
            this.Name = "MaterialCarryoverProcessing";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspWarehouseid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartMonth.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartLabel lblPlant;
        private Framework.SmartControls.SmartLabel lbMonth;
        private Framework.SmartControls.SmartComboBox cboPlantid;
        private Framework.SmartControls.SmartLabel lblwarehousename;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartDateEdit dtpEndMonth;
        private Framework.SmartControls.SmartDateEdit dtpStartMonth;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspWarehouseid;
        private Framework.SmartControls.SmartLabel smartLabel1;
    }
}