namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcedClaimClosureStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutsourcedClaimClosureStatus));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup3 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection5 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection6 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdClaimConfirm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdClaimStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtperiod = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblPeriod = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.lblClaimdeadline = new Micube.Framework.SmartControls.SmartLabel();
            this.btnDetailSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel3 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.popupOspAreaid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel4 = new Micube.Framework.SmartControls.SmartLabel();
            this.popupOspVendorid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.cboexpensegubun = new Micube.Framework.SmartControls.SmartComboBox();
            this.popupProcesssegmentid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).BeginInit();
            this.smartPanel3.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspVendorid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboexpensegubun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupProcesssegmentid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 900);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(843, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tplMain);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.grdClaimConfirm, 0, 4);
            this.tplMain.Controls.Add(this.grdClaimStatus, 0, 1);
            this.tplMain.Controls.Add(this.smartPanel1, 0, 0);
            this.tplMain.Controls.Add(this.smartPanel2, 0, 2);
            this.tplMain.Controls.Add(this.smartPanel3, 0, 3);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 5;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tplMain.Size = new System.Drawing.Size(843, 903);
            this.tplMain.TabIndex = 0;
            // 
            // grdClaimConfirm
            // 
            this.grdClaimConfirm.Caption = "";
            this.grdClaimConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimConfirm.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdClaimConfirm.IsUsePaging = false;
            this.grdClaimConfirm.LanguageKey = "OUTSOURCEDCLAIMCONFIRMATIONLIST";
            this.grdClaimConfirm.Location = new System.Drawing.Point(0, 526);
            this.grdClaimConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimConfirm.Name = "grdClaimConfirm";
            this.grdClaimConfirm.ShowBorder = true;
            this.grdClaimConfirm.ShowStatusBar = false;
            this.grdClaimConfirm.Size = new System.Drawing.Size(843, 377);
            this.grdClaimConfirm.TabIndex = 8;
            this.grdClaimConfirm.UseAutoBestFitColumns = false;
            // 
            // grdClaimStatus
            // 
            this.grdClaimStatus.Caption = "";
            this.grdClaimStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimStatus.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdClaimStatus.IsUsePaging = false;
            this.grdClaimStatus.LanguageKey = "OUTSOURCEDCLAIMCLOSINGCOUNT";
            this.grdClaimStatus.Location = new System.Drawing.Point(0, 40);
            this.grdClaimStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimStatus.Name = "grdClaimStatus";
            this.grdClaimStatus.ShowBorder = true;
            this.grdClaimStatus.ShowStatusBar = false;
            this.grdClaimStatus.Size = new System.Drawing.Size(843, 376);
            this.grdClaimStatus.TabIndex = 6;
            this.grdClaimStatus.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.txtperiod);
            this.smartPanel1.Controls.Add(this.lblPeriod);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(843, 40);
            this.smartPanel1.TabIndex = 7;
            // 
            // txtperiod
            // 
            this.txtperiod.EditValue = "";
            this.txtperiod.LabelText = null;
            this.txtperiod.LanguageKey = null;
            this.txtperiod.Location = new System.Drawing.Point(122, 6);
            this.txtperiod.Name = "txtperiod";
            this.txtperiod.Properties.ReadOnly = true;
            this.txtperiod.Size = new System.Drawing.Size(147, 24);
            this.txtperiod.TabIndex = 9;
            // 
            // lblPeriod
            // 
            this.lblPeriod.LanguageKey = "PERIODSTATE";
            this.lblPeriod.Location = new System.Drawing.Point(5, 9);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(79, 18);
            this.lblPeriod.TabIndex = 7;
            this.lblPeriod.Text = "smartLabel1";
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.lblClaimdeadline);
            this.smartPanel2.Controls.Add(this.btnDetailSearch);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 416);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(843, 40);
            this.smartPanel2.TabIndex = 9;
            // 
            // lblClaimdeadline
            // 
            this.lblClaimdeadline.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimdeadline.Appearance.Options.UseFont = true;
            this.lblClaimdeadline.LanguageKey = "OUTSOURCEDCLAIMDEADLINEINQUIRY";
            this.lblClaimdeadline.Location = new System.Drawing.Point(14, 11);
            this.lblClaimdeadline.Name = "lblClaimdeadline";
            this.lblClaimdeadline.Size = new System.Drawing.Size(91, 21);
            this.lblClaimdeadline.TabIndex = 8;
            this.lblClaimdeadline.Text = "smartLabel1";
            // 
            // btnDetailSearch
            // 
            this.btnDetailSearch.AllowFocus = false;
            this.btnDetailSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetailSearch.IsBusy = false;
            this.btnDetailSearch.IsWrite = false;
            this.btnDetailSearch.LanguageKey = "SEARCH";
            this.btnDetailSearch.Location = new System.Drawing.Point(756, 7);
            this.btnDetailSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnDetailSearch.Name = "btnDetailSearch";
            this.btnDetailSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnDetailSearch.Size = new System.Drawing.Size(80, 25);
            this.btnDetailSearch.TabIndex = 4;
            this.btnDetailSearch.Text = "검색";
            this.btnDetailSearch.TooltipLanguageKey = "";
            // 
            // smartPanel3
            // 
            this.smartPanel3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel3.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.smartPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel3.Location = new System.Drawing.Point(0, 456);
            this.smartPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel3.Name = "smartPanel3";
            this.smartPanel3.Size = new System.Drawing.Size(843, 70);
            this.smartPanel3.TabIndex = 9;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 4;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel3, 2, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel2, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popupOspAreaid, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel4, 2, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popupOspVendorid, 3, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboexpensegubun, 3, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.popupProcesssegmentid, 1, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(843, 70);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartLabel3
            // 
            this.smartLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel3.LanguageKey = "OSPVENDORID";
            this.smartLabel3.Location = new System.Drawing.Point(424, 3);
            this.smartLabel3.Name = "smartLabel3";
            this.smartLabel3.Size = new System.Drawing.Size(144, 29);
            this.smartLabel3.TabIndex = 14;
            this.smartLabel3.Text = "smartLabel1";
            // 
            // smartLabel2
            // 
            this.smartLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel2.LanguageKey = "PROCESSSEGMENTNAME";
            this.smartLabel2.Location = new System.Drawing.Point(3, 38);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(144, 29);
            this.smartLabel2.TabIndex = 14;
            this.smartLabel2.Text = "smartLabel1";
            // 
            // popupOspAreaid
            // 
            this.popupOspAreaid.LabelText = null;
            this.popupOspAreaid.LanguageKey = null;
            this.popupOspAreaid.Location = new System.Drawing.Point(153, 3);
            this.popupOspAreaid.Name = "popupOspAreaid";
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
            this.popupOspAreaid.SelectPopupCondition = conditionItemSelectPopup1;
            this.popupOspAreaid.Size = new System.Drawing.Size(265, 24);
            this.popupOspAreaid.TabIndex = 10;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel1.LanguageKey = "AREANAME";
            this.smartLabel1.Location = new System.Drawing.Point(3, 3);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(144, 29);
            this.smartLabel1.TabIndex = 14;
            this.smartLabel1.Text = "작업장";
            // 
            // smartLabel4
            // 
            this.smartLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel4.LanguageKey = "EXPENSEGUBUN";
            this.smartLabel4.Location = new System.Drawing.Point(424, 38);
            this.smartLabel4.Name = "smartLabel4";
            this.smartLabel4.Size = new System.Drawing.Size(144, 29);
            this.smartLabel4.TabIndex = 14;
            this.smartLabel4.Text = "비용구분";
            // 
            // popupOspVendorid
            // 
            this.popupOspVendorid.LabelText = null;
            this.popupOspVendorid.LanguageKey = null;
            this.popupOspVendorid.Location = new System.Drawing.Point(574, 3);
            this.popupOspVendorid.Name = "popupOspVendorid";
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
            this.popupOspVendorid.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupOspVendorid.Size = new System.Drawing.Size(265, 24);
            this.popupOspVendorid.TabIndex = 11;
            // 
            // cboexpensegubun
            // 
            this.cboexpensegubun.LabelText = null;
            this.cboexpensegubun.LanguageKey = null;
            this.cboexpensegubun.Location = new System.Drawing.Point(574, 38);
            this.cboexpensegubun.Name = "cboexpensegubun";
            this.cboexpensegubun.PopupWidth = 0;
            this.cboexpensegubun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboexpensegubun.Properties.NullText = "";
            this.cboexpensegubun.ShowHeader = true;
            this.cboexpensegubun.Size = new System.Drawing.Size(181, 24);
            this.cboexpensegubun.TabIndex = 13;
            this.cboexpensegubun.VisibleColumns = null;
            this.cboexpensegubun.VisibleColumnsWidth = null;
            // 
            // popupProcesssegmentid
            // 
            this.popupProcesssegmentid.LabelText = null;
            this.popupProcesssegmentid.LanguageKey = null;
            this.popupProcesssegmentid.Location = new System.Drawing.Point(153, 38);
            this.popupProcesssegmentid.Name = "popupProcesssegmentid";
            conditionItemSelectPopup3.ApplySelection = null;
            conditionItemSelectPopup3.AutoFillColumnNames = null;
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
            conditionItemSelectPopup3.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            conditionItemSelectPopup3.GreatThenEqual = false;
            conditionItemSelectPopup3.GreatThenId = "";
            conditionItemSelectPopup3.GridColumns = conditionCollection6;
            conditionItemSelectPopup3.Id = null;
            conditionItemSelectPopup3.InitAction = null;
            conditionItemSelectPopup3.IsCaptionWordWrap = false;
            conditionItemSelectPopup3.IsEnabled = true;
            conditionItemSelectPopup3.IsHidden = false;
            conditionItemSelectPopup3.IsImmediatlyUpdate = true;
            conditionItemSelectPopup3.IsKeyColumn = false;
            conditionItemSelectPopup3.IsMultiGrid = false;
            conditionItemSelectPopup3.IsReadOnly = false;
            conditionItemSelectPopup3.IsRequired = false;
            conditionItemSelectPopup3.IsSearchOnLoading = true;
            conditionItemSelectPopup3.IsUseMultiColumnPaste = true;
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
            conditionItemSelectPopup3.RelationIds = null;
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
            this.popupProcesssegmentid.SelectPopupCondition = conditionItemSelectPopup3;
            this.popupProcesssegmentid.Size = new System.Drawing.Size(265, 24);
            this.popupProcesssegmentid.TabIndex = 12;
            // 
            // OutsourcedClaimClosureStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcedClaimClosureStatus";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtperiod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.smartPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel3)).EndInit();
            this.smartPanel3.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspVendorid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboexpensegubun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupProcesssegmentid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdClaimStatus;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartLabel lblPeriod;
        private Framework.SmartControls.SmartBandedGrid grdClaimConfirm;
        private Framework.SmartControls.SmartTextBox txtperiod;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartPanel smartPanel3;
        private Framework.SmartControls.SmartLabel lblClaimdeadline;
        private Framework.SmartControls.SmartButton btnDetailSearch;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartComboBox cboexpensegubun;
        private Framework.SmartControls.SmartSelectPopupEdit popupProcesssegmentid;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspVendorid;
        private Framework.SmartControls.SmartLabel smartLabel3;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartLabel smartLabel4;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspAreaid;
    }
}