namespace Micube.SmartMES.ToolManagement.Popup
{
    partial class RequestToolMakingLotPopup
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
            this.smartSplitTableLayoutPanel4 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdToolRequestLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cboToolCategory = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblItemCode = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.cboToolCategoryDetail = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblItemVer = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.lblItemNm = new Micube.Framework.SmartControls.SmartLabel();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.txtToolNumber = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.cboArea = new Micube.Framework.SmartControls.SmartComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboToolCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboToolCategoryDetail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel4);
            this.pnlMain.Size = new System.Drawing.Size(956, 404);
            // 
            // smartSplitTableLayoutPanel4
            // 
            this.smartSplitTableLayoutPanel4.ColumnCount = 4;
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 209F));
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl4, 2, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.grdToolRequestLotList, 0, 1);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl5, 0, 2);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl1, 0, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl2, 1, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.panelControl3, 3, 0);
            this.smartSplitTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel4.Name = "smartSplitTableLayoutPanel4";
            this.smartSplitTableLayoutPanel4.RowCount = 3;
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 341F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel4.Size = new System.Drawing.Size(956, 404);
            this.smartSplitTableLayoutPanel4.TabIndex = 2;
            // 
            // grdToolRequestLotList
            // 
            this.grdToolRequestLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.smartSplitTableLayoutPanel4.SetColumnSpan(this.grdToolRequestLotList, 4);
            this.grdToolRequestLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolRequestLotList.IsUsePaging = false;
            this.grdToolRequestLotList.LanguageKey = "PRODUCTDEFIDNAME";
            this.grdToolRequestLotList.Location = new System.Drawing.Point(0, 33);
            this.grdToolRequestLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolRequestLotList.Name = "grdToolRequestLotList";
            this.grdToolRequestLotList.ShowBorder = true;
            this.grdToolRequestLotList.ShowStatusBar = false;
            this.grdToolRequestLotList.Size = new System.Drawing.Size(956, 341);
            this.grdToolRequestLotList.TabIndex = 111;
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartSplitTableLayoutPanel4.SetColumnSpan(this.panelControl5, 4);
            this.panelControl5.Controls.Add(this.btnConfirm);
            this.panelControl5.Controls.Add(this.btnCancel);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(0, 374);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(956, 30);
            this.panelControl5.TabIndex = 113;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AllowFocus = false;
            this.btnConfirm.IsBusy = false;
            this.btnConfirm.LanguageKey = "CONFIRM";
            this.btnConfirm.Location = new System.Drawing.Point(404, 3);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "smartButton2";
            this.btnConfirm.TooltipLanguageKey = "";
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(485, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "smartButton1";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.cboToolCategory);
            this.panelControl1.Controls.Add(this.lblItemCode);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(228, 33);
            this.panelControl1.TabIndex = 86;
            // 
            // cboToolCategory
            // 
            this.cboToolCategory.LabelText = null;
            this.cboToolCategory.LanguageKey = null;
            this.cboToolCategory.Location = new System.Drawing.Point(91, 6);
            this.cboToolCategory.Name = "cboToolCategory";
            this.cboToolCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboToolCategory.Properties.NullText = "";
            this.cboToolCategory.ShowHeader = true;
            this.cboToolCategory.Size = new System.Drawing.Size(124, 20);
            this.cboToolCategory.TabIndex = 116;
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemCode.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemCode.Appearance.Options.UseFont = true;
            this.lblItemCode.Appearance.Options.UseTextOptions = true;
            this.lblItemCode.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemCode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemCode.LanguageKey = "TOOLCATEGORY";
            this.lblItemCode.Location = new System.Drawing.Point(5, 5);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(80, 22);
            this.lblItemCode.TabIndex = 0;
            this.lblItemCode.Text = "품목코드:";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.cboToolCategoryDetail);
            this.panelControl2.Controls.Add(this.lblItemVer);
            this.panelControl2.Location = new System.Drawing.Point(229, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(201, 33);
            this.panelControl2.TabIndex = 87;
            // 
            // cboToolCategoryDetail
            // 
            this.cboToolCategoryDetail.LabelText = null;
            this.cboToolCategoryDetail.LanguageKey = null;
            this.cboToolCategoryDetail.Location = new System.Drawing.Point(83, 6);
            this.cboToolCategoryDetail.Name = "cboToolCategoryDetail";
            this.cboToolCategoryDetail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboToolCategoryDetail.Properties.NullText = "";
            this.cboToolCategoryDetail.ShowHeader = true;
            this.cboToolCategoryDetail.Size = new System.Drawing.Size(113, 20);
            this.cboToolCategoryDetail.TabIndex = 117;
            // 
            // lblItemVer
            // 
            this.lblItemVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemVer.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemVer.Appearance.Options.UseFont = true;
            this.lblItemVer.Appearance.Options.UseTextOptions = true;
            this.lblItemVer.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemVer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemVer.LanguageKey = "TOOLCATEGORYDETAIL";
            this.lblItemVer.Location = new System.Drawing.Point(5, 5);
            this.lblItemVer.Name = "lblItemVer";
            this.lblItemVer.Size = new System.Drawing.Size(72, 22);
            this.lblItemVer.TabIndex = 0;
            this.lblItemVer.Text = "Ver:";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.cboArea);
            this.panelControl3.Controls.Add(this.btnSearch);
            this.panelControl3.Controls.Add(this.lblItemNm);
            this.panelControl3.Location = new System.Drawing.Point(640, 0);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(316, 33);
            this.panelControl3.TabIndex = 88;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(233, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 109;
            this.btnSearch.Text = "smartButton1";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // lblItemNm
            // 
            this.lblItemNm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblItemNm.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblItemNm.Appearance.Options.UseFont = true;
            this.lblItemNm.Appearance.Options.UseTextOptions = true;
            this.lblItemNm.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblItemNm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemNm.LanguageKey = "AREA";
            this.lblItemNm.Location = new System.Drawing.Point(5, 5);
            this.lblItemNm.Name = "lblItemNm";
            this.lblItemNm.Size = new System.Drawing.Size(71, 22);
            this.lblItemNm.TabIndex = 0;
            this.lblItemNm.Text = "작업장:";
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.txtToolNumber);
            this.panelControl4.Controls.Add(this.smartLabel1);
            this.panelControl4.Location = new System.Drawing.Point(431, 0);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(209, 33);
            this.panelControl4.TabIndex = 114;
            // 
            // txtToolNumber
            // 
            this.txtToolNumber.EditValue = "";
            this.txtToolNumber.LabelText = null;
            this.txtToolNumber.LanguageKey = "";
            this.txtToolNumber.Location = new System.Drawing.Point(83, 5);
            this.txtToolNumber.Name = "txtToolNumber";
            this.txtToolNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtToolNumber.Properties.Appearance.Options.UseFont = true;
            this.txtToolNumber.Properties.AutoHeight = false;
            this.txtToolNumber.Size = new System.Drawing.Size(102, 22);
            this.txtToolNumber.TabIndex = 5;
            this.txtToolNumber.Tag = "SUBCLASS";
            // 
            // smartLabel1
            // 
            this.smartLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.smartLabel1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.smartLabel1.Appearance.Options.UseFont = true;
            this.smartLabel1.Appearance.Options.UseTextOptions = true;
            this.smartLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.smartLabel1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.smartLabel1.LanguageKey = "TOOLNUMBER";
            this.smartLabel1.Location = new System.Drawing.Point(5, 5);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(72, 22);
            this.smartLabel1.TabIndex = 0;
            this.smartLabel1.Text = "품목명:";
            // 
            // cboArea
            // 
            this.cboArea.LabelText = null;
            this.cboArea.LanguageKey = null;
            this.cboArea.Location = new System.Drawing.Point(82, 6);
            this.cboArea.Name = "cboArea";
            this.cboArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboArea.Properties.NullText = "";
            this.cboArea.ShowHeader = true;
            this.cboArea.Size = new System.Drawing.Size(145, 20);
            this.cboArea.TabIndex = 117;
            // 
            // RequestToolMakingLotPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 424);
            this.LanguageKey = "TOOLNOBROWSE";
            this.Name = "RequestToolMakingLotPopup";
            this.Text = "RequestToolMakingLotPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboToolCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboToolCategoryDetail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtToolNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel4;
        private Framework.SmartControls.SmartBandedGrid grdToolRequestLotList;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private Framework.SmartControls.SmartButton btnConfirm;
        private Framework.SmartControls.SmartButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Framework.SmartControls.SmartComboBox cboToolCategory;
        private Framework.SmartControls.SmartLabel lblItemCode;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Framework.SmartControls.SmartComboBox cboToolCategoryDetail;
        private Framework.SmartControls.SmartLabel lblItemVer;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartLabel lblItemNm;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Framework.SmartControls.SmartTextBox txtToolNumber;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartComboBox cboArea;
    }
}