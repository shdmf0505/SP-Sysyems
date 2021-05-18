namespace Micube.SmartMES.ToolManagement.Popup
{
    partial class MultiFilmCodePopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiFilmCodePopup));
            Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
            Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection conditionCollection2 = new Micube.Framework.SmartControls.Grid.Conditions.ConditionCollection();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.popProductCode = new Micube.Framework.SmartControls.SmartSelectPopupEdit();
            this.lblItemCode = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtProductDefName = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblItemVer = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.txtProductVersion = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblItemNm = new Micube.Framework.SmartControls.SmartLabel();
            this.cboProcessSegment = new Micube.Framework.SmartControls.SmartComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.grdFilmCodeList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popProductCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProcessSegment.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grdFilmCodeList);
            this.pnlMain.Controls.Add(this.flowLayoutPanel2);
            this.pnlMain.Controls.Add(this.flowLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(863, 456);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panelControl1);
            this.flowLayoutPanel1.Controls.Add(this.panelControl2);
            this.flowLayoutPanel1.Controls.Add(this.panelControl3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(863, 34);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.popProductCode);
            this.panelControl1.Controls.Add(this.lblItemCode);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(270, 33);
            this.panelControl1.TabIndex = 87;
            // 
            // popProductCode
            // 
            this.popProductCode.LabelText = null;
            this.popProductCode.LanguageKey = null;
            this.popProductCode.Location = new System.Drawing.Point(111, 6);
            this.popProductCode.Name = "popProductCode";
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
            this.popProductCode.SelectPopupCondition = conditionItemSelectPopup1;
            this.popProductCode.Size = new System.Drawing.Size(156, 20);
            this.popProductCode.TabIndex = 1;
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemCode.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemCode.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblItemCode.Appearance.Options.UseFont = true;
            this.lblItemCode.Appearance.Options.UseForeColor = true;
            this.lblItemCode.Appearance.Options.UseTextOptions = true;
            this.lblItemCode.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemCode.LanguageKey = "PRODUCTDEFID";
            this.lblItemCode.Location = new System.Drawing.Point(5, 5);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(100, 22);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "품목코드:";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.txtProductDefName);
            this.panelControl2.Controls.Add(this.lblItemVer);
            this.panelControl2.Location = new System.Drawing.Point(270, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(296, 33);
            this.panelControl2.TabIndex = 88;
            // 
            // txtProductDefName
            // 
            this.txtProductDefName.EditValue = "";
            this.txtProductDefName.LabelText = null;
            this.txtProductDefName.LanguageKey = "";
            this.txtProductDefName.Location = new System.Drawing.Point(83, 5);
            this.txtProductDefName.Name = "txtProductDefName";
            this.txtProductDefName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtProductDefName.Properties.Appearance.Options.UseFont = true;
            this.txtProductDefName.Properties.AutoHeight = false;
            this.txtProductDefName.Properties.ReadOnly = true;
            this.txtProductDefName.Size = new System.Drawing.Size(199, 22);
            this.txtProductDefName.TabIndex = 5;
            this.txtProductDefName.Tag = "MASTERDATACLASS";
            // 
            // lblItemVer
            // 
            this.lblItemVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemVer.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemVer.Appearance.Options.UseFont = true;
            this.lblItemVer.Appearance.Options.UseTextOptions = true;
            this.lblItemVer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemVer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemVer.LanguageKey = "PRODUCTDEFNAME";
            this.lblItemVer.Location = new System.Drawing.Point(5, 5);
            this.lblItemVer.Name = "lblItemVer";
            this.lblItemVer.Size = new System.Drawing.Size(72, 22);
            this.lblItemVer.TabIndex = 0;
            this.lblItemVer.Text = "품목명:";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.btnSearch);
            this.panelControl3.Controls.Add(this.txtProductVersion);
            this.panelControl3.Controls.Add(this.lblItemNm);
            this.panelControl3.Location = new System.Drawing.Point(566, 0);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(282, 33);
            this.panelControl3.TabIndex = 89;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(194, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 109;
            this.btnSearch.Text = "Search";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // txtProductVersion
            // 
            this.txtProductVersion.EditValue = "";
            this.txtProductVersion.LabelText = null;
            this.txtProductVersion.LanguageKey = "";
            this.txtProductVersion.Location = new System.Drawing.Point(71, 5);
            this.txtProductVersion.Name = "txtProductVersion";
            this.txtProductVersion.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtProductVersion.Properties.Appearance.Options.UseFont = true;
            this.txtProductVersion.Properties.AutoHeight = false;
            this.txtProductVersion.Properties.ReadOnly = true;
            this.txtProductVersion.Size = new System.Drawing.Size(101, 22);
            this.txtProductVersion.TabIndex = 6;
            this.txtProductVersion.Tag = "MASTERDATACLASS";
            // 
            // lblItemNm
            // 
            this.lblItemNm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemNm.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemNm.Appearance.Options.UseFont = true;
            this.lblItemNm.Appearance.Options.UseTextOptions = true;
            this.lblItemNm.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemNm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemNm.LanguageKey = "PRODUCTREVISIONINPUT";
            this.lblItemNm.Location = new System.Drawing.Point(5, 5);
            this.lblItemNm.Name = "lblItemNm";
            this.lblItemNm.Size = new System.Drawing.Size(60, 22);
            this.lblItemNm.TabIndex = 0;
            this.lblItemNm.Text = "Version:";
            // 
            // cboProcessSegment
            // 
            this.cboProcessSegment.LabelText = null;
            this.cboProcessSegment.LanguageKey = null;
            this.cboProcessSegment.Location = new System.Drawing.Point(558, 3);
            this.cboProcessSegment.Name = "cboProcessSegment";
            this.cboProcessSegment.PopupWidth = 0;
            this.cboProcessSegment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProcessSegment.Properties.NullText = "";
            this.cboProcessSegment.ShowHeader = true;
            this.cboProcessSegment.Size = new System.Drawing.Size(92, 20);
            this.cboProcessSegment.TabIndex = 117;
            this.cboProcessSegment.Visible = false;
            this.cboProcessSegment.VisibleColumns = null;
            this.cboProcessSegment.VisibleColumnsWidth = null;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.smartPanel1);
            this.flowLayoutPanel2.Controls.Add(this.cboProcessSegment);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 412);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(863, 44);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.btnConfirm);
            this.smartPanel1.Controls.Add(this.btnCancel);
            this.smartPanel1.Location = new System.Drawing.Point(3, 3);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(549, 38);
            this.smartPanel1.TabIndex = 0;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.IsWrite = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(384, 8);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.TooltipLanguageKey = "";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(465, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // grdFilmCodeList
            // 
            this.grdFilmCodeList.Caption = "필름목록:";
            this.grdFilmCodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilmCodeList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdFilmCodeList.IsUsePaging = false;
            this.grdFilmCodeList.LanguageKey = "FILMCODEBROWSE";
            this.grdFilmCodeList.Location = new System.Drawing.Point(0, 34);
            this.grdFilmCodeList.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilmCodeList.Name = "grdFilmCodeList";
            this.grdFilmCodeList.ShowBorder = true;
            this.grdFilmCodeList.ShowStatusBar = false;
            this.grdFilmCodeList.Size = new System.Drawing.Size(863, 378);
            this.grdFilmCodeList.TabIndex = 112;
            // 
            // MultiFilmCodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 476);
            this.LanguageKey = "BROWSEREQUESTFILMLIST";
            this.Name = "MultiFilmCodePopup";
            this.Text = "";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popProductCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductDefName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProcessSegment.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdFilmCodeList;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Framework.SmartControls.SmartTextBox txtProductVersion;
        private Framework.SmartControls.SmartLabel lblItemCode;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Framework.SmartControls.SmartTextBox txtProductDefName;
        private Framework.SmartControls.SmartLabel lblItemVer;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Framework.SmartControls.SmartComboBox cboProcessSegment;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartLabel lblItemNm;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartSelectPopupEdit popProductCode;
    }
}