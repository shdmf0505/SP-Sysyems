namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class ClaimClosePeriodPopup
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdPeriod = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.cboPlantid = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblCloseYm = new Micube.Framework.SmartControls.SmartLabel();
            this.lblPlantid = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.speCloseYm = new Micube.Framework.SmartControls.SmartSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speCloseYm.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(695, 426);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdPeriod, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 2);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(695, 426);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdPeriod
            // 
            this.grdPeriod.Caption = "";
            this.grdPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPeriod.IsUsePaging = false;
            this.grdPeriod.LanguageKey = "";
            this.grdPeriod.Location = new System.Drawing.Point(0, 40);
            this.grdPeriod.Margin = new System.Windows.Forms.Padding(0);
            this.grdPeriod.Name = "grdPeriod";
            this.grdPeriod.ShowBorder = true;
            this.grdPeriod.Size = new System.Drawing.Size(695, 346);
            this.grdPeriod.TabIndex = 112;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.speCloseYm);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Controls.Add(this.cboPlantid);
            this.smartPanel1.Controls.Add(this.lblCloseYm);
            this.smartPanel1.Controls.Add(this.lblPlantid);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(695, 40);
            this.smartPanel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(610, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 116;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // cboPlantid
            // 
            this.cboPlantid.LabelText = null;
            this.cboPlantid.LanguageKey = null;
            this.cboPlantid.Location = new System.Drawing.Point(87, 8);
            this.cboPlantid.Name = "cboPlantid";
            this.cboPlantid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPlantid.Properties.NullText = "";
            this.cboPlantid.ShowHeader = true;
            this.cboPlantid.Size = new System.Drawing.Size(194, 24);
            this.cboPlantid.TabIndex = 114;
            // 
            // lblCloseYm
            // 
            this.lblCloseYm.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblCloseYm.Appearance.Options.UseForeColor = true;
            this.lblCloseYm.LanguageKey = "CLOSEYEAR";
            this.lblCloseYm.Location = new System.Drawing.Point(316, 11);
            this.lblCloseYm.Name = "lblCloseYm";
            this.lblCloseYm.Size = new System.Drawing.Size(52, 18);
            this.lblCloseYm.TabIndex = 113;
            this.lblCloseYm.Text = "마감년도";
            // 
            // lblPlantid
            // 
            this.lblPlantid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblPlantid.Appearance.Options.UseForeColor = true;
            this.lblPlantid.LanguageKey = "SITE";
            this.lblPlantid.Location = new System.Drawing.Point(20, 11);
            this.lblPlantid.Name = "lblPlantid";
            this.lblPlantid.Size = new System.Drawing.Size(32, 18);
            this.lblPlantid.TabIndex = 113;
            this.lblPlantid.Text = "SITE";
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.btnSave);
            this.smartPanel2.Controls.Add(this.btnClose);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 386);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(695, 40);
            this.smartPanel2.TabIndex = 113;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.IsBusy = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(259, 8);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(355, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // speCloseYm
            // 
            this.speCloseYm.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.speCloseYm.LabelText = null;
            this.speCloseYm.LanguageKey = null;
            this.speCloseYm.Location = new System.Drawing.Point(401, 8);
            this.speCloseYm.Name = "speCloseYm";
            this.speCloseYm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.speCloseYm.Properties.Mask.EditMask = "####";
            this.speCloseYm.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.speCloseYm.Size = new System.Drawing.Size(110, 24);
            this.speCloseYm.TabIndex = 117;
            // 
            // ClaimClosePeriodPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 446);
            this.LanguageKey = "CLAIMCLOSEPERIODPOPUP";
            this.Name = "ClaimClosePeriodPopup";
            this.Text = "Claim마감기간";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.speCloseYm.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartBandedGrid grdPeriod;
        private Framework.SmartControls.SmartLabel lblPlantid;
        private Framework.SmartControls.SmartComboBox cboPlantid;
        private Framework.SmartControls.SmartLabel lblCloseYm;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartSpinEdit speCloseYm;
    }
}