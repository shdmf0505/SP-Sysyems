namespace Micube.SmartMES.MaterialsManagement.Popup
{
    partial class MaterialGoodsReturnPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialGoodsReturnPopup));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.tplPopup = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdMaterialReceiptPopup = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblconsumabledefid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblMaterialReceiptDate = new Micube.Framework.SmartControls.SmartLabel();
            this.lblAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.lblPlantid = new Micube.Framework.SmartControls.SmartLabel();
            this.cboPlantid = new Micube.Framework.SmartControls.SmartComboBox();
            this.pnlDate = new Micube.Framework.SmartControls.SmartPanel();
            this.dtpEndDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.dtpStartDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.lbldate = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.txtConsumabledefid = new Micube.Framework.SmartControls.SmartTextBox();
            this.pnlWork = new Micube.Framework.SmartControls.SmartPanel();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.popupOspAreaid = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tplPopup.SuspendLayout();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDate)).BeginInit();
            this.pnlDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumabledefid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWork)).BeginInit();
            this.pnlWork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tplPopup);
            this.pnlMain.Size = new System.Drawing.Size(1007, 605);
            // 
            // tplPopup
            // 
            this.tplPopup.ColumnCount = 1;
            this.tplPopup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplPopup.Controls.Add(this.grdMaterialReceiptPopup, 0, 1);
            this.tplPopup.Controls.Add(this.tplMain, 0, 0);
            this.tplPopup.Controls.Add(this.pnlWork, 0, 2);
            this.tplPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplPopup.Location = new System.Drawing.Point(0, 0);
            this.tplPopup.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplPopup.Name = "tplPopup";
            this.tplPopup.RowCount = 3;
            this.tplPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tplPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tplPopup.Size = new System.Drawing.Size(1007, 605);
            this.tplPopup.TabIndex = 0;
            // 
            // grdMaterialReceiptPopup
            // 
            this.grdMaterialReceiptPopup.Caption = "";
            this.grdMaterialReceiptPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterialReceiptPopup.IsUsePaging = false;
            this.grdMaterialReceiptPopup.LanguageKey = "MATERIALRETURNLIST";
            this.grdMaterialReceiptPopup.Location = new System.Drawing.Point(0, 96);
            this.grdMaterialReceiptPopup.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterialReceiptPopup.Name = "grdMaterialReceiptPopup";
            this.grdMaterialReceiptPopup.ShowBorder = true;
            this.grdMaterialReceiptPopup.Size = new System.Drawing.Size(1007, 459);
            this.grdMaterialReceiptPopup.TabIndex = 111;
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 5;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.popupOspAreaid, 3, 0);
            this.tplMain.Controls.Add(this.lblconsumabledefid, 0, 2);
            this.tplMain.Controls.Add(this.lblMaterialReceiptDate, 0, 1);
            this.tplMain.Controls.Add(this.lblAreaid, 2, 0);
            this.tplMain.Controls.Add(this.lblPlantid, 0, 0);
            this.tplMain.Controls.Add(this.cboPlantid, 1, 0);
            this.tplMain.Controls.Add(this.pnlDate, 1, 1);
            this.tplMain.Controls.Add(this.btnSearch, 4, 2);
            this.tplMain.Controls.Add(this.txtConsumabledefid, 1, 2);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 3;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tplMain.Size = new System.Drawing.Size(1007, 96);
            this.tplMain.TabIndex = 112;
            // 
            // lblconsumabledefid
            // 
            this.lblconsumabledefid.LanguageKey = "CONSUMABLEDEFID";
            this.lblconsumabledefid.Location = new System.Drawing.Point(3, 68);
            this.lblconsumabledefid.Name = "lblconsumabledefid";
            this.lblconsumabledefid.Size = new System.Drawing.Size(135, 18);
            this.lblconsumabledefid.TabIndex = 111;
            this.lblconsumabledefid.Text = "CONSUMABLEDEFID";
            // 
            // lblMaterialReceiptDate
            // 
            this.lblMaterialReceiptDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaterialReceiptDate.LanguageKey = "RECEIPTDATERETURN";
            this.lblMaterialReceiptDate.Location = new System.Drawing.Point(3, 35);
            this.lblMaterialReceiptDate.Name = "lblMaterialReceiptDate";
            this.lblMaterialReceiptDate.Size = new System.Drawing.Size(144, 27);
            this.lblMaterialReceiptDate.TabIndex = 111;
            this.lblMaterialReceiptDate.Text = "smartLabel7";
            // 
            // lblAreaid
            // 
            this.lblAreaid.LanguageKey = "AREANAME";
            this.lblAreaid.Location = new System.Drawing.Point(353, 3);
            this.lblAreaid.Name = "lblAreaid";
            this.lblAreaid.Size = new System.Drawing.Size(79, 18);
            this.lblAreaid.TabIndex = 111;
            this.lblAreaid.Text = "smartLabel7";
            // 
            // lblPlantid
            // 
            this.lblPlantid.LanguageKey = "PLANTID";
            this.lblPlantid.Location = new System.Drawing.Point(3, 3);
            this.lblPlantid.Name = "lblPlantid";
            this.lblPlantid.Size = new System.Drawing.Size(60, 18);
            this.lblPlantid.TabIndex = 111;
            this.lblPlantid.Text = "PLANTID";
            // 
            // cboPlantid
            // 
            this.cboPlantid.LabelText = null;
            this.cboPlantid.LanguageKey = null;
            this.cboPlantid.Location = new System.Drawing.Point(153, 3);
            this.cboPlantid.Name = "cboPlantid";
            this.cboPlantid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlantid.Properties.NullText = "";
            this.cboPlantid.Properties.ReadOnly = true;
            this.cboPlantid.ShowHeader = true;
            this.cboPlantid.Size = new System.Drawing.Size(194, 24);
            this.cboPlantid.TabIndex = 112;
            // 
            // pnlDate
            // 
            this.pnlDate.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tplMain.SetColumnSpan(this.pnlDate, 2);
            this.pnlDate.Controls.Add(this.dtpEndDate);
            this.pnlDate.Controls.Add(this.dtpStartDate);
            this.pnlDate.Controls.Add(this.lbldate);
            this.pnlDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDate.Location = new System.Drawing.Point(150, 32);
            this.pnlDate.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(350, 33);
            this.pnlDate.TabIndex = 113;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.EditValue = null;
            this.dtpEndDate.LabelText = null;
            this.dtpEndDate.LanguageKey = null;
            this.dtpEndDate.Location = new System.Drawing.Point(194, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Size = new System.Drawing.Size(153, 24);
            this.dtpEndDate.TabIndex = 0;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.EditValue = null;
            this.dtpStartDate.LabelText = null;
            this.dtpStartDate.LanguageKey = null;
            this.dtpStartDate.Location = new System.Drawing.Point(4, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartDate.Size = new System.Drawing.Size(161, 24);
            this.dtpStartDate.TabIndex = 0;
            // 
            // lbldate
            // 
            this.lbldate.LanguageKey = "";
            this.lbldate.Location = new System.Drawing.Point(171, 4);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(11, 18);
            this.lbldate.TabIndex = 111;
            this.lbldate.Text = "~";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(922, 68);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 110;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // txtConsumabledefid
            // 
            this.txtConsumabledefid.LabelText = null;
            this.txtConsumabledefid.LanguageKey = null;
            this.txtConsumabledefid.Location = new System.Drawing.Point(153, 68);
            this.txtConsumabledefid.Name = "txtConsumabledefid";
            this.txtConsumabledefid.Size = new System.Drawing.Size(194, 24);
            this.txtConsumabledefid.TabIndex = 114;
            // 
            // pnlWork
            // 
            this.pnlWork.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlWork.Controls.Add(this.btnConfirm);
            this.pnlWork.Controls.Add(this.btnClose);
            this.pnlWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWork.Location = new System.Drawing.Point(0, 555);
            this.pnlWork.Margin = new System.Windows.Forms.Padding(0);
            this.pnlWork.Name = "pnlWork";
            this.pnlWork.Size = new System.Drawing.Size(1007, 50);
            this.pnlWork.TabIndex = 113;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(425, 14);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "smartButton2";
            this.btnConfirm.TooltipLanguageKey = "";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CANCEL";
            this.btnClose.Location = new System.Drawing.Point(506, 14);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // popupOspAreaid
            // 
            this.popupOspAreaid.LabelText = null;
            this.popupOspAreaid.LanguageKey = null;
            this.popupOspAreaid.Location = new System.Drawing.Point(503, 3);
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
            conditionItemSelectPopup1.IsEnabled = true;
            conditionItemSelectPopup1.IsHidden = false;
            conditionItemSelectPopup1.IsImmediatlyUpdate = true;
            conditionItemSelectPopup1.IsKeyColumn = false;
            conditionItemSelectPopup1.IsMultiGrid = false;
            conditionItemSelectPopup1.IsReadOnly = false;
            conditionItemSelectPopup1.IsRequired = false;
            conditionItemSelectPopup1.IsSearchOnLoading = true;
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
            conditionItemSelectPopup1.SearchQuery = null;
            conditionItemSelectPopup1.SelectionQuery = null;
            conditionItemSelectPopup1.ShowSearchButton = true;
            conditionItemSelectPopup1.TextAlignment = Micube.Framework.SmartControls.TextAlignment.Default;
            conditionItemSelectPopup1.Title = null;
            conditionItemSelectPopup1.ToolTip = null;
            conditionItemSelectPopup1.ToolTipLanguageKey = null;
            conditionItemSelectPopup1.ValueFieldName = "";
            conditionItemSelectPopup1.WindowSize = new System.Drawing.Size(800, 500);
            this.popupOspAreaid.SelectPopupCondition = conditionItemSelectPopup1;
            this.popupOspAreaid.Size = new System.Drawing.Size(244, 24);
            this.popupOspAreaid.TabIndex = 118;
            // 
            // MaterialGoodsReturnPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 625);
            this.LanguageKey = "MATERIALRETURNHISTORYINQUIRY";
            this.Name = "MaterialGoodsReturnPopup";
            this.Text = "";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tplPopup.ResumeLayout(false);
            this.tplMain.ResumeLayout(false);
            this.tplMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDate)).EndInit();
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsumabledefid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWork)).EndInit();
            this.pnlWork.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupOspAreaid.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplPopup;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartBandedGrid grdMaterialReceiptPopup;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartLabel lblconsumabledefid;
        private Framework.SmartControls.SmartLabel lblMaterialReceiptDate;
        private Framework.SmartControls.SmartLabel lblAreaid;
        private Framework.SmartControls.SmartLabel lblPlantid;
        private Framework.SmartControls.SmartComboBox cboPlantid;
        private Framework.SmartControls.SmartPanel pnlWork;
        private Framework.SmartControls.SmartPanel pnlDate;
        private Framework.SmartControls.SmartDateEdit dtpEndDate;
        private Framework.SmartControls.SmartDateEdit dtpStartDate;
        private Framework.SmartControls.SmartTextBox txtConsumabledefid;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartLabel lbldate;
        private Framework.SmartControls.SmartSelectPopupEdit popupOspAreaid;
    }
}