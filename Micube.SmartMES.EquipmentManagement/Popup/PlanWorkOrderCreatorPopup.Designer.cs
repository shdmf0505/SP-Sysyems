namespace Micube.SmartMES.EquipmentManagement.Popup
{
    partial class PlanWorkOrderCreatorPopup
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
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup1 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanWorkOrderCreatorPopup));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup3 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection5 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection6 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartGroupBox1 = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.popMaintItem = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.deEndDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.deStartDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.popEquipmentID = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.cboPlanPeriod = new Micube.Framework.SmartControls.SmartComboBox();
            this.cboFactory = new Micube.Framework.SmartControls.SmartComboBox();
            this.popEquipmentGroup = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.cboMaintType = new Micube.Framework.SmartControls.SmartComboBox();
            this.cboPlant = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblPlanDate = new Micube.Framework.SmartControls.SmartLabel();
            this.lblMaintITem = new Micube.Framework.SmartControls.SmartLabel();
            this.lblEquipment = new Micube.Framework.SmartControls.SmartLabel();
            this.lblFactory = new Micube.Framework.SmartControls.SmartLabel();
            this.lblEquipmentGroup = new Micube.Framework.SmartControls.SmartLabel();
            this.lblMaintType = new Micube.Framework.SmartControls.SmartLabel();
            this.lblSite = new Micube.Framework.SmartControls.SmartLabel();
            this.grdEquipmentMaint = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).BeginInit();
            this.smartGroupBox1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popMaintItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popEquipmentID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlanPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFactory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popEquipmentGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaintType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlant.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdEquipmentMaint);
            this.pnlMain.Controls.Add(this.smartGroupBox1);
            this.pnlMain.Controls.Add(this.flowLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(909, 566);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnSearch);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(909, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(805, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(101, 22);
            this.btnClose.TabIndex = 323;
            this.btnClose.Text = "닫기:";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(698, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(101, 22);
            this.btnSave.TabIndex = 324;
            this.btnSave.Text = "저장:";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(591, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(101, 22);
            this.btnSearch.TabIndex = 325;
            this.btnSearch.Text = "검색:";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // smartGroupBox1
            // 
            this.smartGroupBox1.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartGroupBox1.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.smartGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartGroupBox1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.smartGroupBox1.LanguageKey = "PMMAINTWORKORDERINFO";
            this.smartGroupBox1.Location = new System.Drawing.Point(0, 26);
            this.smartGroupBox1.Name = "smartGroupBox1";
            this.smartGroupBox1.ShowBorder = true;
            this.smartGroupBox1.Size = new System.Drawing.Size(909, 130);
            this.smartGroupBox1.TabIndex = 327;
            this.smartGroupBox1.Text = "PMMAINTWORKORDERINFO";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 7;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popMaintItem, 5, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.deEndDate, 3, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.deStartDate, 2, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popEquipmentID, 3, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboPlanPeriod, 1, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboFactory, 5, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popEquipmentGroup, 1, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboMaintType, 3, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboPlant, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblPlanDate, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblMaintITem, 4, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblEquipment, 2, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblFactory, 4, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblEquipmentGroup, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblMaintType, 2, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblSite, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(2, 31);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 4;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(905, 97);
            this.smartSplitTableLayoutPanel1.TabIndex = 116;
            // 
            // popMaintItem
            // 
            this.popMaintItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popMaintItem.LabelText = null;
            this.popMaintItem.LanguageKey = null;
            this.popMaintItem.Location = new System.Drawing.Point(753, 33);
            this.popMaintItem.Name = "popMaintItem";
            conditionItemSelectPopup1.ApplySelection = null;
            conditionItemSelectPopup1.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.AutoFillColumnNames")));
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
            conditionItemSelectPopup1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            conditionItemSelectPopup1.GreatThenEqual = false;
            conditionItemSelectPopup1.GreatThenId = "";
            conditionItemSelectPopup1.GridColumns = conditionCollection2;
            conditionItemSelectPopup1.Id = null;
            conditionItemSelectPopup1.InitAction = null;
            conditionItemSelectPopup1.IsEnabled = true;
            conditionItemSelectPopup1.IsHidden = false;
            conditionItemSelectPopup1.IsImmediatlyUpdate = true;
            conditionItemSelectPopup1.IsKeyColumn = false;
            conditionItemSelectPopup1.IsMultiGrid = false;
            conditionItemSelectPopup1.IsReadOnly = false;
            conditionItemSelectPopup1.IsRequired = false;
            conditionItemSelectPopup1.IsSearchOnLoading = true;
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
            conditionItemSelectPopup1.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup1.RelationIds")));
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
            this.popMaintItem.SelectPopupCondition = conditionItemSelectPopup1;
            this.popMaintItem.Size = new System.Drawing.Size(144, 20);
            this.popMaintItem.TabIndex = 330;
            // 
            // deEndDate
            // 
            this.deEndDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deEndDate.EditValue = null;
            this.deEndDate.LabelText = null;
            this.deEndDate.LanguageKey = null;
            this.deEndDate.Location = new System.Drawing.Point(453, 63);
            this.deEndDate.Name = "deEndDate";
            this.deEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Size = new System.Drawing.Size(144, 20);
            this.deEndDate.TabIndex = 328;
            // 
            // deStartDate
            // 
            this.deStartDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deStartDate.EditValue = null;
            this.deStartDate.LabelText = null;
            this.deStartDate.LanguageKey = null;
            this.deStartDate.Location = new System.Drawing.Point(303, 63);
            this.deStartDate.Name = "deStartDate";
            this.deStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Size = new System.Drawing.Size(144, 20);
            this.deStartDate.TabIndex = 329;
            // 
            // popEquipmentID
            // 
            this.popEquipmentID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popEquipmentID.LabelText = null;
            this.popEquipmentID.LanguageKey = null;
            this.popEquipmentID.Location = new System.Drawing.Point(453, 33);
            this.popEquipmentID.Name = "popEquipmentID";
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
            conditionItemSelectPopup2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            conditionItemSelectPopup2.GreatThenEqual = false;
            conditionItemSelectPopup2.GreatThenId = "";
            conditionItemSelectPopup2.GridColumns = conditionCollection4;
            conditionItemSelectPopup2.Id = null;
            conditionItemSelectPopup2.InitAction = null;
            conditionItemSelectPopup2.IsEnabled = true;
            conditionItemSelectPopup2.IsHidden = false;
            conditionItemSelectPopup2.IsImmediatlyUpdate = true;
            conditionItemSelectPopup2.IsKeyColumn = false;
            conditionItemSelectPopup2.IsMultiGrid = false;
            conditionItemSelectPopup2.IsReadOnly = false;
            conditionItemSelectPopup2.IsRequired = false;
            conditionItemSelectPopup2.IsSearchOnLoading = true;
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
            this.popEquipmentID.SelectPopupCondition = conditionItemSelectPopup2;
            this.popEquipmentID.Size = new System.Drawing.Size(144, 20);
            this.popEquipmentID.TabIndex = 329;
            // 
            // cboPlanPeriod
            // 
            this.cboPlanPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPlanPeriod.LabelText = null;
            this.cboPlanPeriod.LanguageKey = null;
            this.cboPlanPeriod.Location = new System.Drawing.Point(153, 63);
            this.cboPlanPeriod.Name = "cboPlanPeriod";
            this.cboPlanPeriod.PopupWidth = 0;
            this.cboPlanPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlanPeriod.Properties.NullText = "";
            this.cboPlanPeriod.ShowHeader = true;
            this.cboPlanPeriod.Size = new System.Drawing.Size(144, 20);
            this.cboPlanPeriod.TabIndex = 326;
            this.cboPlanPeriod.VisibleColumns = null;
            this.cboPlanPeriod.VisibleColumnsWidth = null;
            // 
            // cboFactory
            // 
            this.cboFactory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboFactory.LabelText = null;
            this.cboFactory.LanguageKey = null;
            this.cboFactory.Location = new System.Drawing.Point(753, 3);
            this.cboFactory.Name = "cboFactory";
            this.cboFactory.PopupWidth = 0;
            this.cboFactory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFactory.Properties.NullText = "";
            this.cboFactory.ShowHeader = true;
            this.cboFactory.Size = new System.Drawing.Size(144, 20);
            this.cboFactory.TabIndex = 326;
            this.cboFactory.VisibleColumns = null;
            this.cboFactory.VisibleColumnsWidth = null;
            // 
            // popEquipmentGroup
            // 
            this.popEquipmentGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popEquipmentGroup.LabelText = null;
            this.popEquipmentGroup.LanguageKey = null;
            this.popEquipmentGroup.Location = new System.Drawing.Point(153, 33);
            this.popEquipmentGroup.Name = "popEquipmentGroup";
            conditionItemSelectPopup3.ApplySelection = null;
            conditionItemSelectPopup3.AutoFillColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup3.AutoFillColumnNames")));
            conditionItemSelectPopup3.CanOkNoSelection = true;
            conditionItemSelectPopup3.ClearButtonRealOnly = false;
            conditionItemSelectPopup3.ClearButtonVisible = true;
            conditionItemSelectPopup3.ConditionDefaultId = null;
            conditionItemSelectPopup3.ConditionLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            conditionItemSelectPopup3.ConditionRequireId = "";
            conditionItemSelectPopup3.Conditions = conditionCollection5;
            conditionItemSelectPopup3.CustomPopup = null;
            conditionItemSelectPopup3.CustomValidate = null;
            conditionItemSelectPopup3.DefaultDisplayValue = null;
            conditionItemSelectPopup3.DefaultValue = null;
            conditionItemSelectPopup3.DisplayFieldName = "";
            conditionItemSelectPopup3.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            conditionItemSelectPopup3.GreatThenEqual = false;
            conditionItemSelectPopup3.GreatThenId = "";
            conditionItemSelectPopup3.GridColumns = conditionCollection6;
            conditionItemSelectPopup3.Id = null;
            conditionItemSelectPopup3.InitAction = null;
            conditionItemSelectPopup3.IsEnabled = true;
            conditionItemSelectPopup3.IsHidden = false;
            conditionItemSelectPopup3.IsImmediatlyUpdate = true;
            conditionItemSelectPopup3.IsKeyColumn = false;
            conditionItemSelectPopup3.IsMultiGrid = false;
            conditionItemSelectPopup3.IsReadOnly = false;
            conditionItemSelectPopup3.IsRequired = false;
            conditionItemSelectPopup3.IsSearchOnLoading = true;
            conditionItemSelectPopup3.IsUseRowCheckByMouseDrag = false;
            conditionItemSelectPopup3.LabelText = null;
            conditionItemSelectPopup3.LanguageKey = null;
            conditionItemSelectPopup3.LessThenEqual = false;
            conditionItemSelectPopup3.LessThenId = "";
            conditionItemSelectPopup3.NoSelectionMessageId = "";
            conditionItemSelectPopup3.PopupButtonStyle = Micube.Framework.SmartControls.PopupButtonStyles.Ok_Cancel;
            conditionItemSelectPopup3.PopupCustomValidation = null;
            conditionItemSelectPopup3.Position = 0D;
            conditionItemSelectPopup3.QueryPopup = null;
            conditionItemSelectPopup3.RelationIds = ((System.Collections.Generic.List<string>)(resources.GetObject("conditionItemSelectPopup3.RelationIds")));
            conditionItemSelectPopup3.ResultAction = null;
            conditionItemSelectPopup3.ResultCount = 1;
            conditionItemSelectPopup3.SearchButtonReadOnly = false;
            conditionItemSelectPopup3.SearchQuery = null;
            conditionItemSelectPopup3.SearchText = null;
            conditionItemSelectPopup3.SearchTextControlId = null;
            conditionItemSelectPopup3.SelectionQuery = null;
            conditionItemSelectPopup3.ShowSearchButton = true;
            conditionItemSelectPopup3.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup3.Title = null;
            conditionItemSelectPopup3.ToolTip = null;
            conditionItemSelectPopup3.ToolTipLanguageKey = null;
            conditionItemSelectPopup3.ValueFieldName = "";
            conditionItemSelectPopup3.WindowSize = new System.Drawing.Size(800, 500);
            this.popEquipmentGroup.SelectPopupCondition = conditionItemSelectPopup3;
            this.popEquipmentGroup.Size = new System.Drawing.Size(144, 20);
            this.popEquipmentGroup.TabIndex = 328;
            // 
            // cboMaintType
            // 
            this.cboMaintType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMaintType.LabelText = null;
            this.cboMaintType.LanguageKey = null;
            this.cboMaintType.Location = new System.Drawing.Point(453, 3);
            this.cboMaintType.Name = "cboMaintType";
            this.cboMaintType.PopupWidth = 0;
            this.cboMaintType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMaintType.Properties.NullText = "";
            this.cboMaintType.ShowHeader = true;
            this.cboMaintType.Size = new System.Drawing.Size(144, 20);
            this.cboMaintType.TabIndex = 315;
            this.cboMaintType.VisibleColumns = null;
            this.cboMaintType.VisibleColumnsWidth = null;
            // 
            // cboPlant
            // 
            this.cboPlant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPlant.LabelText = null;
            this.cboPlant.LanguageKey = null;
            this.cboPlant.Location = new System.Drawing.Point(153, 3);
            this.cboPlant.Name = "cboPlant";
            this.cboPlant.PopupWidth = 0;
            this.cboPlant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlant.Properties.NullText = "";
            this.cboPlant.ShowHeader = true;
            this.cboPlant.Size = new System.Drawing.Size(144, 20);
            this.cboPlant.TabIndex = 313;
            this.cboPlant.VisibleColumns = null;
            this.cboPlant.VisibleColumnsWidth = null;
            // 
            // lblPlanDate
            // 
            this.lblPlanDate.Appearance.Options.UseTextOptions = true;
            this.lblPlanDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblPlanDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPlanDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlanDate.LanguageKey = "PLANDATE";
            this.lblPlanDate.Location = new System.Drawing.Point(3, 63);
            this.lblPlanDate.Name = "lblPlanDate";
            this.lblPlanDate.Size = new System.Drawing.Size(144, 24);
            this.lblPlanDate.TabIndex = 31;
            this.lblPlanDate.Text = "계획일:";
            // 
            // lblMaintITem
            // 
            this.lblMaintITem.Appearance.Options.UseTextOptions = true;
            this.lblMaintITem.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblMaintITem.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMaintITem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaintITem.LanguageKey = "MAINTITEM";
            this.lblMaintITem.Location = new System.Drawing.Point(603, 33);
            this.lblMaintITem.Name = "lblMaintITem";
            this.lblMaintITem.Size = new System.Drawing.Size(144, 24);
            this.lblMaintITem.TabIndex = 17;
            this.lblMaintITem.Text = "점검항목:";
            // 
            // lblEquipment
            // 
            this.lblEquipment.Appearance.Options.UseTextOptions = true;
            this.lblEquipment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblEquipment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEquipment.LanguageKey = "EQUIPMENT";
            this.lblEquipment.Location = new System.Drawing.Point(303, 33);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(144, 24);
            this.lblEquipment.TabIndex = 12;
            this.lblEquipment.Text = "설비:";
            // 
            // lblFactory
            // 
            this.lblFactory.Appearance.Options.UseTextOptions = true;
            this.lblFactory.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblFactory.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFactory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFactory.LanguageKey = "FACTORY";
            this.lblFactory.Location = new System.Drawing.Point(603, 3);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(144, 24);
            this.lblFactory.TabIndex = 9;
            this.lblFactory.Text = "공장:";
            // 
            // lblEquipmentGroup
            // 
            this.lblEquipmentGroup.Appearance.Options.UseTextOptions = true;
            this.lblEquipmentGroup.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblEquipmentGroup.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblEquipmentGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEquipmentGroup.LanguageKey = "EQUIPMENTGROUP";
            this.lblEquipmentGroup.Location = new System.Drawing.Point(3, 33);
            this.lblEquipmentGroup.Name = "lblEquipmentGroup";
            this.lblEquipmentGroup.Size = new System.Drawing.Size(144, 24);
            this.lblEquipmentGroup.TabIndex = 7;
            this.lblEquipmentGroup.Text = "설비그룹:";
            // 
            // lblMaintType
            // 
            this.lblMaintType.Appearance.Options.UseTextOptions = true;
            this.lblMaintType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblMaintType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMaintType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaintType.LanguageKey = "MAINTTYPE";
            this.lblMaintType.Location = new System.Drawing.Point(303, 3);
            this.lblMaintType.Name = "lblMaintType";
            this.lblMaintType.Size = new System.Drawing.Size(144, 24);
            this.lblMaintType.TabIndex = 2;
            this.lblMaintType.Text = "보전구분:";
            // 
            // lblSite
            // 
            this.lblSite.Appearance.Options.UseTextOptions = true;
            this.lblSite.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblSite.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSite.LanguageKey = "SITE";
            this.lblSite.Location = new System.Drawing.Point(3, 3);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(144, 24);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "SITE:";
            // 
            // grdEquipmentMaint
            // 
            this.grdEquipmentMaint.Caption = "설비점검항목목록 :";
            this.grdEquipmentMaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEquipmentMaint.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdEquipmentMaint.IsUsePaging = false;
            this.grdEquipmentMaint.LanguageKey = "EQUIPMENTINSPECTIONLIST";
            this.grdEquipmentMaint.Location = new System.Drawing.Point(0, 156);
            this.grdEquipmentMaint.Margin = new System.Windows.Forms.Padding(0);
            this.grdEquipmentMaint.Name = "grdEquipmentMaint";
            this.grdEquipmentMaint.ShowBorder = true;
            this.grdEquipmentMaint.Size = new System.Drawing.Size(909, 410);
            this.grdEquipmentMaint.TabIndex = 328;
            // 
            // PlanWorkOrderCreatorPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 586);
            this.LanguageKey = "PMMAINTWORKORDERCREATEPOPUP";
            this.Name = "PlanWorkOrderCreatorPopup";
            this.Text = "PMMAINTWORKORDERCREATEPOPUP";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartGroupBox1)).EndInit();
            this.smartGroupBox1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popMaintItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popEquipmentID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlanPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFactory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popEquipmentGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaintType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlant.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartGroupBox smartGroupBox1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartSelectPopupEdit popEquipmentID;
        private Framework.SmartControls.SmartSelectPopupEdit popEquipmentGroup;
        private Framework.SmartControls.SmartComboBox cboMaintType;
        private Framework.SmartControls.SmartComboBox cboPlant;
        private Framework.SmartControls.SmartLabel lblPlanDate;
        private Framework.SmartControls.SmartLabel lblMaintITem;
        private Framework.SmartControls.SmartLabel lblEquipment;
        private Framework.SmartControls.SmartLabel lblFactory;
        private Framework.SmartControls.SmartLabel lblEquipmentGroup;
        private Framework.SmartControls.SmartLabel lblMaintType;
        private Framework.SmartControls.SmartLabel lblSite;
        private Framework.SmartControls.SmartDateEdit deEndDate;
        private Framework.SmartControls.SmartDateEdit deStartDate;
        private Framework.SmartControls.SmartBandedGrid grdEquipmentMaint;
        private Framework.SmartControls.SmartComboBox cboFactory;
        private Framework.SmartControls.SmartComboBox cboPlanPeriod;
        private Framework.SmartControls.SmartSelectPopupEdit popMaintItem;
    }
}