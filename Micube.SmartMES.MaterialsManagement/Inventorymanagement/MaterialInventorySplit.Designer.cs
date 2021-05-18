namespace Micube.SmartMES.MaterialsManagement
{
    partial class MaterialInventorySplit
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdMaterial = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpldetail = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtwarehouseid = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtwarehousename = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblwarehousename = new Micube.Framework.SmartControls.SmartLabel();
            this.popupOspAreaid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblUserid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblTransavtionno = new Micube.Framework.SmartControls.SmartLabel();
            this.lblTransactiondate = new Micube.Framework.SmartControls.SmartLabel();
            this.lblDescription = new Micube.Framework.SmartControls.SmartLabel();
            this.lblAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblPlantid = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPlantid = new Micube.Framework.SmartControls.SmartComboBox();
            this.txtUserName = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtDescription = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnTransactionno = new Micube.Framework.SmartControls.SmartButtonEdit();
            this.dptTransactiondate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnClear = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            this.tpldetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehouseid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehousename.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTransactionno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dptTransactiondate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dptTransactiondate.Properties)).BeginInit();
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
            this.pnlToolbar.Controls.Add(this.btnSave);
            this.pnlToolbar.Controls.Add(this.btnClear);
            this.pnlToolbar.Size = new System.Drawing.Size(1226, 30);
            this.pnlToolbar.Controls.SetChildIndex(this.btnClear, 0);
            this.pnlToolbar.Controls.SetChildIndex(this.btnSave, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tplMain);
            this.pnlContent.Size = new System.Drawing.Size(1226, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.grdMaterial, 0, 1);
            this.tplMain.Controls.Add(this.tpldetail, 0, 0);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 2;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplMain.Size = new System.Drawing.Size(1226, 911);
            this.tplMain.TabIndex = 0;
            // 
            // grdMaterial
            // 
            this.grdMaterial.Caption = "";
            this.grdMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterial.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaterial.IsUsePaging = false;
            this.grdMaterial.LanguageKey = "MATERIALINVENTORYSPLIT";
            this.grdMaterial.Location = new System.Drawing.Point(0, 90);
            this.grdMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterial.Name = "grdMaterial";
            this.grdMaterial.ShowBorder = true;
            this.grdMaterial.Size = new System.Drawing.Size(1226, 821);
            this.grdMaterial.TabIndex = 7;
            this.grdMaterial.UseAutoBestFitColumns = false;
            // 
            // tpldetail
            // 
            this.tpldetail.ColumnCount = 7;
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tpldetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpldetail.Controls.Add(this.txtwarehouseid, 6, 0);
            this.tpldetail.Controls.Add(this.txtwarehousename, 5, 0);
            this.tpldetail.Controls.Add(this.lblwarehousename, 4, 0);
            this.tpldetail.Controls.Add(this.popupOspAreaid, 1, 1);
            this.tpldetail.Controls.Add(this.lblUserid, 4, 1);
            this.tpldetail.Controls.Add(this.lblTransavtionno, 2, 0);
            this.tpldetail.Controls.Add(this.lblTransactiondate, 2, 1);
            this.tpldetail.Controls.Add(this.lblDescription, 0, 2);
            this.tpldetail.Controls.Add(this.lblAreaid, 0, 1);
            this.tpldetail.Controls.Add(this.lblPlantid, 0, 0);
            this.tpldetail.Controls.Add(this.cboPlantid, 1, 0);
            this.tpldetail.Controls.Add(this.txtUserName, 5, 1);
            this.tpldetail.Controls.Add(this.txtDescription, 1, 2);
            this.tpldetail.Controls.Add(this.btnTransactionno, 3, 0);
            this.tpldetail.Controls.Add(this.dptTransactiondate, 3, 1);
            this.tpldetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpldetail.Location = new System.Drawing.Point(0, 0);
            this.tpldetail.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tpldetail.Name = "tpldetail";
            this.tpldetail.RowCount = 3;
            this.tpldetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tpldetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tpldetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tpldetail.Size = new System.Drawing.Size(1226, 90);
            this.tpldetail.TabIndex = 0;
            // 
            // txtwarehouseid
            // 
            this.txtwarehouseid.LabelText = null;
            this.txtwarehouseid.LanguageKey = null;
            this.txtwarehouseid.Location = new System.Drawing.Point(863, 3);
            this.txtwarehouseid.Name = "txtwarehouseid";
            this.txtwarehouseid.Properties.ReadOnly = true;
            this.txtwarehouseid.Size = new System.Drawing.Size(49, 24);
            this.txtwarehouseid.TabIndex = 19;
            this.txtwarehouseid.Visible = false;
            // 
            // txtwarehousename
            // 
            this.txtwarehousename.LabelText = null;
            this.txtwarehousename.LanguageKey = null;
            this.txtwarehousename.Location = new System.Drawing.Point(743, 3);
            this.txtwarehousename.Name = "txtwarehousename";
            this.txtwarehousename.Properties.ReadOnly = true;
            this.txtwarehousename.Size = new System.Drawing.Size(114, 24);
            this.txtwarehousename.TabIndex = 18;
            // 
            // lblwarehousename
            // 
            this.lblwarehousename.LanguageKey = "WAREHOUSENAME";
            this.lblwarehousename.Location = new System.Drawing.Point(623, 3);
            this.lblwarehousename.Name = "lblwarehousename";
            this.lblwarehousename.Size = new System.Drawing.Size(26, 18);
            this.lblwarehousename.TabIndex = 16;
            this.lblwarehousename.Text = "창고";
            // 
            // popupOspAreaid
            // 
            this.popupOspAreaid.LabelText = null;
            this.popupOspAreaid.LanguageKey = null;
            this.popupOspAreaid.Location = new System.Drawing.Point(123, 33);
            this.popupOspAreaid.Name = "popupOspAreaid";
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = null;
            conditionItemSelectPopup1.CanOkNoSelection = true;
            conditionItemSelectPopup1.ClearButtonRealOnly = false;
            conditionItemSelectPopup1.ClearButtonVisible = true;
            conditionItemSelectPopup1.ConditionDefaultId = null;
            conditionItemSelectPopup1.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            conditionItemSelectPopup1.ConditionRequireId = "";
            conditionItemSelectPopup1.Conditions = conditionCollection1;
            conditionItemSelectPopup1.CustomPopup = null;
            conditionItemSelectPopup1.CustomValidate = null;
            conditionItemSelectPopup1.DefaultDisplayValue = null;
            conditionItemSelectPopup1.DefaultValue = null;
            conditionItemSelectPopup1.DisplayFieldName = "";
            conditionItemSelectPopup1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            conditionItemSelectPopup1.GreatThenEqual = false;
            conditionItemSelectPopup1.GreatThenId = "";
            conditionItemSelectPopup1.GridColumns = conditionCollection2;
            conditionItemSelectPopup1.Id = null;
            conditionItemSelectPopup1.InitAction = null;
            conditionItemSelectPopup1.IsCaptionWordWrap = false;
            conditionItemSelectPopup1.IsEnabled = true;
            conditionItemSelectPopup1.IsHidden = false;
            conditionItemSelectPopup1.IsImmediatlyUpdate = true;
            conditionItemSelectPopup1.IsKeyColumn = false;
            conditionItemSelectPopup1.IsMultiGrid = false;
            conditionItemSelectPopup1.IsReadOnly = false;
            conditionItemSelectPopup1.IsRequired = false;
            conditionItemSelectPopup1.IsSearchOnLoading = true;
            conditionItemSelectPopup1.IsUseMultiColumnPaste = true;
            conditionItemSelectPopup1.IsUseRowCheckByMouseDrag = false;
            conditionItemSelectPopup1.LabelText = null;
            conditionItemSelectPopup1.LanguageKey = null;
            conditionItemSelectPopup1.LessThenEqual = false;
            conditionItemSelectPopup1.LessThenId = "";
            conditionItemSelectPopup1.NoSelectionMessageId = "";
            conditionItemSelectPopup1.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup1.PopupCustomValidation = null;
            conditionItemSelectPopup1.Position = 0D;
            conditionItemSelectPopup1.QueryPopup = null;
            conditionItemSelectPopup1.RelationIds = null;
            conditionItemSelectPopup1.ResultAction = null;
            conditionItemSelectPopup1.ResultCount = 1;
            conditionItemSelectPopup1.SearchButtonReadOnly = false;
            conditionItemSelectPopup1.SearchQuery = null;
            conditionItemSelectPopup1.SearchText = null;
            conditionItemSelectPopup1.SearchTextControlId = null;
            conditionItemSelectPopup1.SelectionQuery = null;
            conditionItemSelectPopup1.ShowSearchButton = true;
            conditionItemSelectPopup1.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup1.Title = null;
            conditionItemSelectPopup1.ToolTip = null;
            conditionItemSelectPopup1.ToolTipLanguageKey = null;
            conditionItemSelectPopup1.ValueFieldName = "";
            conditionItemSelectPopup1.WindowSize = new System.Drawing.Size(800, 500);
            this.popupOspAreaid.SelectPopupCondition = conditionItemSelectPopup1;
            this.popupOspAreaid.Size = new System.Drawing.Size(174, 24);
            this.popupOspAreaid.TabIndex = 15;
            // 
            // lblUserid
            // 
            this.lblUserid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserid.LanguageKey = "RECEIPTUSERSPLIT";
            this.lblUserid.Location = new System.Drawing.Point(623, 33);
            this.lblUserid.Name = "lblUserid";
            this.lblUserid.Size = new System.Drawing.Size(114, 24);
            this.lblUserid.TabIndex = 0;
            this.lblUserid.Text = "smartLabel1";
            // 
            // lblTransavtionno
            // 
            this.lblTransavtionno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransavtionno.LanguageKey = "TRANSACTIONNOSPLIT";
            this.lblTransavtionno.Location = new System.Drawing.Point(303, 3);
            this.lblTransavtionno.Name = "lblTransavtionno";
            this.lblTransavtionno.Size = new System.Drawing.Size(114, 24);
            this.lblTransavtionno.TabIndex = 0;
            this.lblTransavtionno.Text = "smartLabel1";
            // 
            // lblTransactiondate
            // 
            this.lblTransactiondate.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTransactiondate.Appearance.Options.UseForeColor = true;
            this.lblTransactiondate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransactiondate.LanguageKey = "RECEIPTDATESPLIT";
            this.lblTransactiondate.Location = new System.Drawing.Point(303, 33);
            this.lblTransactiondate.Name = "lblTransactiondate";
            this.lblTransactiondate.Size = new System.Drawing.Size(114, 24);
            this.lblTransactiondate.TabIndex = 0;
            this.lblTransactiondate.Text = "입고일";
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.LanguageKey = "MATREMARK";
            this.lblDescription.Location = new System.Drawing.Point(3, 63);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(114, 24);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "smartLabel1";
            // 
            // lblAreaid
            // 
            this.lblAreaid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblAreaid.Appearance.Options.UseForeColor = true;
            this.lblAreaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAreaid.LanguageKey = "AREANAME";
            this.lblAreaid.Location = new System.Drawing.Point(3, 33);
            this.lblAreaid.Name = "lblAreaid";
            this.lblAreaid.Size = new System.Drawing.Size(114, 24);
            this.lblAreaid.TabIndex = 0;
            this.lblAreaid.Text = "smartLabel1";
            // 
            // lblPlantid
            // 
            this.lblPlantid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblPlantid.Appearance.Options.UseForeColor = true;
            this.lblPlantid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlantid.LanguageKey = "PLANTID";
            this.lblPlantid.Location = new System.Drawing.Point(3, 3);
            this.lblPlantid.Name = "lblPlantid";
            this.lblPlantid.Size = new System.Drawing.Size(114, 24);
            this.lblPlantid.TabIndex = 0;
            this.lblPlantid.Text = "smartLabel1";
            // 
            // cboPlantid
            // 
            this.cboPlantid.LabelText = null;
            this.cboPlantid.LanguageKey = null;
            this.cboPlantid.Location = new System.Drawing.Point(123, 3);
            this.cboPlantid.Name = "cboPlantid";
            this.cboPlantid.PopupWidth = 0;
            this.cboPlantid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlantid.Properties.NullText = "";
            this.cboPlantid.ShowHeader = true;
            this.cboPlantid.Size = new System.Drawing.Size(174, 24);
            this.cboPlantid.TabIndex = 1;
            this.cboPlantid.VisibleColumns = null;
            this.cboPlantid.VisibleColumnsWidth = null;
            // 
            // txtUserName
            // 
            this.txtUserName.LabelText = null;
            this.txtUserName.LanguageKey = null;
            this.txtUserName.Location = new System.Drawing.Point(743, 33);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(114, 24);
            this.txtUserName.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.tpldetail.SetColumnSpan(this.txtDescription, 5);
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.LabelText = null;
            this.txtDescription.LanguageKey = null;
            this.txtDescription.Location = new System.Drawing.Point(123, 63);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(734, 24);
            this.txtDescription.TabIndex = 5;
            // 
            // btnTransactionno
            // 
            this.btnTransactionno.Location = new System.Drawing.Point(423, 3);
            this.btnTransactionno.Name = "btnTransactionno";
            this.btnTransactionno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnTransactionno.Properties.ReadOnly = true;
            this.btnTransactionno.Size = new System.Drawing.Size(194, 24);
            this.btnTransactionno.TabIndex = 6;
            // 
            // dptTransactiondate
            // 
            this.dptTransactiondate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dptTransactiondate.EditValue = null;
            this.dptTransactiondate.Enabled = false;
            this.dptTransactiondate.LabelText = null;
            this.dptTransactiondate.LanguageKey = null;
            this.dptTransactiondate.Location = new System.Drawing.Point(423, 33);
            this.dptTransactiondate.Name = "dptTransactiondate";
            this.dptTransactiondate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dptTransactiondate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dptTransactiondate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dptTransactiondate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dptTransactiondate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.dptTransactiondate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dptTransactiondate.Size = new System.Drawing.Size(194, 24);
            this.dptTransactiondate.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(2199, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.AllowFocus = false;
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.IsBusy = false;
            this.btnClear.IsWrite = false;
            this.btnClear.LanguageKey = "CLEAR";
            this.btnClear.Location = new System.Drawing.Point(2113, 3);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClear.Size = new System.Drawing.Size(80, 25);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "초기화";
            this.btnClear.TooltipLanguageKey = "";
            this.btnClear.Visible = false;
            // 
            // MaterialInventorySplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.ConditionsVisible = false;
            this.Name = "MaterialInventorySplit";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            this.tpldetail.ResumeLayout(false);
            this.tpldetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehouseid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtwarehousename.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTransactionno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dptTransactiondate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dptTransactiondate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tpldetail;
        private Framework.SmartControls.SmartLabel lblUserid;
        private Framework.SmartControls.SmartLabel lblTransavtionno;
        private Framework.SmartControls.SmartLabel lblTransactiondate;
        private Framework.SmartControls.SmartLabel lblDescription;
        private Framework.SmartControls.SmartLabel lblAreaid;
        private Framework.SmartControls.SmartLabel lblPlantid;
        private Framework.SmartControls.SmartComboBox cboPlantid;
        private Framework.SmartControls.SmartTextBox txtUserName;
        private Framework.SmartControls.SmartTextBox txtDescription;
        private Framework.SmartControls.SmartButton btnClear;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdMaterial;
        private Framework.SmartControls.SmartButtonEdit btnTransactionno;
        private Framework.SmartControls.SmartDateEdit dptTransactiondate;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspAreaid;
        private Framework.SmartControls.SmartLabel lblwarehousename;
        private Framework.SmartControls.SmartTextBox txtwarehousename;
        private Framework.SmartControls.SmartTextBox txtwarehouseid;
    }
}