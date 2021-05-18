namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class OutsourcedWarehouseShipmentPopup
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
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            Micube.Framework.SmartControls.ConditionItemSelectPopup conditionItemSelectPopup2 = new Micube.Framework.SmartControls.ConditionItemSelectPopup();
            Micube.Framework.SmartControls.ConditionCollection conditionCollection3 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection4 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tplPopup = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdOutsourcedWarehouseShipment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel5 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.popupOspAreaid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.usrProductdefid = new Micube.SmartMES.OutsideOrderMgnt.ucItemPopup();
            this.popupProcesssegmentid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblLotid = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPlantid = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblProcesssegmentname = new Micube.Framework.SmartControls.SmartLabel();
            this.lblAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblPlantid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblReceiptdate = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel11 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtLotid = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel7 = new Micube.Framework.SmartControls.SmartPanel();
            this.smartLabel9 = new Micube.Framework.SmartControls.SmartLabel();
            this.dtpReceiptdateTo = new Micube.Framework.SmartControls.SmartDateEdit();
            this.dtpReceiptdateFr = new Micube.Framework.SmartControls.SmartDateEdit();
            this.smartPanel6 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tplPopup.SuspendLayout();
            this.smartSplitTableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupProcesssegmentid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel7)).BeginInit();
            this.smartPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateFr.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateFr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel6)).BeginInit();
            this.smartPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tplPopup);
            this.pnlMain.Size = new System.Drawing.Size(874, 605);
            // 
            // tplPopup
            // 
            this.tplPopup.ColumnCount = 1;
            this.tplPopup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplPopup.Controls.Add(this.grdOutsourcedWarehouseShipment, 0, 1);
            this.tplPopup.Controls.Add(this.smartSplitTableLayoutPanel5, 0, 0);
            this.tplPopup.Controls.Add(this.smartPanel6, 0, 2);
            this.tplPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplPopup.Location = new System.Drawing.Point(0, 0);
            this.tplPopup.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplPopup.Name = "tplPopup";
            this.tplPopup.RowCount = 3;
            this.tplPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tplPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tplPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplPopup.Size = new System.Drawing.Size(874, 605);
            this.tplPopup.TabIndex = 2;
            // 
            // grdOutsourcedWarehouseShipment
            // 
            this.grdOutsourcedWarehouseShipment.Caption = "외주창고 입고 목록";
            this.grdOutsourcedWarehouseShipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOutsourcedWarehouseShipment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOutsourcedWarehouseShipment.IsUsePaging = false;
            this.grdOutsourcedWarehouseShipment.LanguageKey = "OUTSOURCEDWAREHOUSEWEARLIST";
            this.grdOutsourcedWarehouseShipment.Location = new System.Drawing.Point(0, 90);
            this.grdOutsourcedWarehouseShipment.Margin = new System.Windows.Forms.Padding(0);
            this.grdOutsourcedWarehouseShipment.Name = "grdOutsourcedWarehouseShipment";
            this.grdOutsourcedWarehouseShipment.ShowBorder = true;
            this.grdOutsourcedWarehouseShipment.Size = new System.Drawing.Size(874, 465);
            this.grdOutsourcedWarehouseShipment.TabIndex = 7;
            // 
            // smartSplitTableLayoutPanel5
            // 
            this.smartSplitTableLayoutPanel5.ColumnCount = 5;
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel5.Controls.Add(this.popupOspAreaid, 1, 1);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.usrProductdefid, 3, 1);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.popupProcesssegmentid, 1, 2);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.lblLotid, 2, 2);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.cboPlantid, 1, 0);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.lblProcesssegmentname, 0, 2);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.lblAreaid, 0, 1);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.lblPlantid, 0, 0);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.lblReceiptdate, 2, 0);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.smartLabel11, 2, 1);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.txtLotid, 3, 2);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.btnSearch, 4, 2);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.smartPanel7, 3, 0);
            this.smartSplitTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel5.Name = "smartSplitTableLayoutPanel5";
            this.smartSplitTableLayoutPanel5.RowCount = 3;
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel5.Size = new System.Drawing.Size(874, 90);
            this.smartSplitTableLayoutPanel5.TabIndex = 0;
            // 
            // popupOspAreaid
            // 
            this.popupOspAreaid.Enabled = false;
            this.popupOspAreaid.LabelText = null;
            this.popupOspAreaid.LanguageKey = null;
            this.popupOspAreaid.Location = new System.Drawing.Point(113, 33);
            this.popupOspAreaid.Name = "popupOspAreaid";
            this.popupOspAreaid.Properties.ReadOnly = true;
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
            this.popupOspAreaid.Size = new System.Drawing.Size(194, 24);
            this.popupOspAreaid.TabIndex = 118;
            // 
            // usrProductdefid
            // 
            this.usrProductdefid.blReadOnly = false;
            this.smartSplitTableLayoutPanel5.SetColumnSpan(this.usrProductdefid, 2);
            this.usrProductdefid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usrProductdefid.Location = new System.Drawing.Point(420, 32);
            this.usrProductdefid.Margin = new System.Windows.Forms.Padding(0, 2, 3, 1);
            this.usrProductdefid.Name = "usrProductdefid";
            this.usrProductdefid.Size = new System.Drawing.Size(451, 27);
            this.usrProductdefid.strTempPlantid = "";
            this.usrProductdefid.TabIndex = 3;
            // 
            // popupProcesssegmentid
            // 
            this.popupProcesssegmentid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupProcesssegmentid.LabelText = null;
            this.popupProcesssegmentid.LanguageKey = null;
            this.popupProcesssegmentid.Location = new System.Drawing.Point(113, 63);
            this.popupProcesssegmentid.Name = "popupProcesssegmentid";
            this.popupProcesssegmentid.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            conditionItemSelectPopup2.ApplySelection = null;
            conditionItemSelectPopup2.AutoFillColumnNames = null;
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
            conditionItemSelectPopup2.RelationIds = null;
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
            this.popupProcesssegmentid.SelectPopupCondition = conditionItemSelectPopup2;
            this.popupProcesssegmentid.Size = new System.Drawing.Size(194, 24);
            this.popupProcesssegmentid.TabIndex = 4;
            // 
            // lblLotid
            // 
            this.lblLotid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLotid.Location = new System.Drawing.Point(313, 63);
            this.lblLotid.Name = "lblLotid";
            this.lblLotid.Size = new System.Drawing.Size(104, 24);
            this.lblLotid.TabIndex = 112;
            this.lblLotid.Text = "LOT NO";
            // 
            // cboPlantid
            // 
            this.cboPlantid.LabelText = null;
            this.cboPlantid.LanguageKey = null;
            this.cboPlantid.Location = new System.Drawing.Point(113, 3);
            this.cboPlantid.Name = "cboPlantid";
            this.cboPlantid.PopupWidth = 0;
            this.cboPlantid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlantid.Properties.NullText = "";
            this.cboPlantid.ShowHeader = true;
            this.cboPlantid.Size = new System.Drawing.Size(194, 24);
            this.cboPlantid.TabIndex = 2;
            this.cboPlantid.VisibleColumns = null;
            this.cboPlantid.VisibleColumnsWidth = null;
            // 
            // lblProcesssegmentname
            // 
            this.lblProcesssegmentname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcesssegmentname.LanguageKey = "PROCESSSEGMENTNAME";
            this.lblProcesssegmentname.Location = new System.Drawing.Point(3, 63);
            this.lblProcesssegmentname.Name = "lblProcesssegmentname";
            this.lblProcesssegmentname.Size = new System.Drawing.Size(104, 24);
            this.lblProcesssegmentname.TabIndex = 112;
            this.lblProcesssegmentname.Text = "공정";
            // 
            // lblAreaid
            // 
            this.lblAreaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAreaid.Location = new System.Drawing.Point(3, 33);
            this.lblAreaid.Name = "lblAreaid";
            this.lblAreaid.Size = new System.Drawing.Size(104, 24);
            this.lblAreaid.TabIndex = 112;
            this.lblAreaid.Text = "외주작업장";
            // 
            // lblPlantid
            // 
            this.lblPlantid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlantid.Location = new System.Drawing.Point(3, 3);
            this.lblPlantid.Name = "lblPlantid";
            this.lblPlantid.Size = new System.Drawing.Size(104, 24);
            this.lblPlantid.TabIndex = 112;
            this.lblPlantid.Text = "SITE";
            // 
            // lblReceiptdate
            // 
            this.lblReceiptdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReceiptdate.LanguageKey = "RECEIPTDATE";
            this.lblReceiptdate.Location = new System.Drawing.Point(313, 3);
            this.lblReceiptdate.Name = "lblReceiptdate";
            this.lblReceiptdate.Size = new System.Drawing.Size(104, 24);
            this.lblReceiptdate.TabIndex = 112;
            this.lblReceiptdate.Text = "입고일자";
            // 
            // smartLabel11
            // 
            this.smartLabel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel11.LanguageKey = "PRODUCTDEFID";
            this.smartLabel11.Location = new System.Drawing.Point(313, 33);
            this.smartLabel11.Name = "smartLabel11";
            this.smartLabel11.Size = new System.Drawing.Size(104, 24);
            this.smartLabel11.TabIndex = 112;
            this.smartLabel11.Text = "품목코드";
            // 
            // txtLotid
            // 
            this.txtLotid.LabelText = null;
            this.txtLotid.LanguageKey = null;
            this.txtLotid.Location = new System.Drawing.Point(423, 63);
            this.txtLotid.Name = "txtLotid";
            this.txtLotid.Size = new System.Drawing.Size(293, 24);
            this.txtLotid.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(780, 63);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(91, 25);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // smartPanel7
            // 
            this.smartPanel7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel7.Controls.Add(this.smartLabel9);
            this.smartPanel7.Controls.Add(this.dtpReceiptdateTo);
            this.smartPanel7.Controls.Add(this.dtpReceiptdateFr);
            this.smartPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel7.Location = new System.Drawing.Point(420, 0);
            this.smartPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel7.Name = "smartPanel7";
            this.smartPanel7.Size = new System.Drawing.Size(300, 30);
            this.smartPanel7.TabIndex = 117;
            // 
            // smartLabel9
            // 
            this.smartLabel9.Location = new System.Drawing.Point(141, 6);
            this.smartLabel9.Name = "smartLabel9";
            this.smartLabel9.Size = new System.Drawing.Size(11, 18);
            this.smartLabel9.TabIndex = 1;
            this.smartLabel9.Text = "~";
            // 
            // dtpReceiptdateTo
            // 
            this.dtpReceiptdateTo.EditValue = null;
            this.dtpReceiptdateTo.LabelText = null;
            this.dtpReceiptdateTo.LanguageKey = null;
            this.dtpReceiptdateTo.Location = new System.Drawing.Point(165, 3);
            this.dtpReceiptdateTo.Name = "dtpReceiptdateTo";
            this.dtpReceiptdateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReceiptdateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReceiptdateTo.Size = new System.Drawing.Size(129, 24);
            this.dtpReceiptdateTo.TabIndex = 1;
            // 
            // dtpReceiptdateFr
            // 
            this.dtpReceiptdateFr.EditValue = null;
            this.dtpReceiptdateFr.LabelText = null;
            this.dtpReceiptdateFr.LanguageKey = null;
            this.dtpReceiptdateFr.Location = new System.Drawing.Point(3, 3);
            this.dtpReceiptdateFr.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.dtpReceiptdateFr.Name = "dtpReceiptdateFr";
            this.dtpReceiptdateFr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReceiptdateFr.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReceiptdateFr.Size = new System.Drawing.Size(129, 24);
            this.dtpReceiptdateFr.TabIndex = 0;
            // 
            // smartPanel6
            // 
            this.smartPanel6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel6.Controls.Add(this.btnClose);
            this.smartPanel6.Controls.Add(this.btnApply);
            this.smartPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel6.Location = new System.Drawing.Point(0, 555);
            this.smartPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel6.Name = "smartPanel6";
            this.smartPanel6.Size = new System.Drawing.Size(874, 50);
            this.smartPanel6.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(436, 14);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(109, 25);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.LanguageKey = "APPLY";
            this.btnApply.Location = new System.Drawing.Point(308, 14);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(109, 25);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "적용";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // OutsourcedWarehouseShipmentPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 625);
            this.LanguageKey = "OUTSOURCEDWAREHOUSESHIPMENTPOPUP";
            this.Name = "OutsourcedWarehouseShipmentPopup";
            this.Text = "제품조회";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tplPopup.ResumeLayout(false);
            this.smartSplitTableLayoutPanel5.ResumeLayout(false);
            this.smartSplitTableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupProcesssegmentid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel7)).EndInit();
            this.smartPanel7.ResumeLayout(false);
            this.smartPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateFr.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReceiptdateFr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel6)).EndInit();
            this.smartPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplPopup;
        private Framework.SmartControls.SmartBandedGrid grdOutsourcedWarehouseShipment;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel5;
        private ucItemPopup usrProductdefid;
        private Framework.SmartControls.SmartSelectPopupEdit popupProcesssegmentid;
        private Framework.SmartControls.SmartLabel lblLotid;
        private Framework.SmartControls.SmartComboBox cboPlantid;
        private Framework.SmartControls.SmartLabel lblProcesssegmentname;
        private Framework.SmartControls.SmartLabel lblAreaid;
        private Framework.SmartControls.SmartLabel lblPlantid;
        private Framework.SmartControls.SmartLabel lblReceiptdate;
        private Framework.SmartControls.SmartLabel smartLabel11;
        private Framework.SmartControls.SmartTextBox txtLotid;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartPanel smartPanel7;
        private Framework.SmartControls.SmartLabel smartLabel9;
        private Framework.SmartControls.SmartDateEdit dtpReceiptdateTo;
        private Framework.SmartControls.SmartDateEdit dtpReceiptdateFr;
        private Framework.SmartControls.SmartPanel smartPanel6;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnApply;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspAreaid;
    }
}