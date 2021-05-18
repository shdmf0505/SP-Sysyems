namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class OutsourcedResourceidpopup
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
            this.grdSearch = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.txtResourcename = new Micube.Framework.SmartControls.SmartTextBox();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.cboAreaid = new Micube.Framework.SmartControls.SmartComboBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.lblAreaid = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtResourcename.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAreaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
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
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdSearch, 0, 1);
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
            // grdSearch
            // 
            this.grdSearch.Caption = "";
            this.grdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSearch.IsUsePaging = false;
            this.grdSearch.LanguageKey = "";
            this.grdSearch.Location = new System.Drawing.Point(0, 40);
            this.grdSearch.Margin = new System.Windows.Forms.Padding(0);
            this.grdSearch.Name = "grdSearch";
            this.grdSearch.ShowBorder = true;
            this.grdSearch.Size = new System.Drawing.Size(695, 346);
            this.grdSearch.TabIndex = 112;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.txtResourcename);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Controls.Add(this.cboAreaid);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.lblAreaid);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(695, 40);
            this.smartPanel1.TabIndex = 0;
            // 
            // txtResourcename
            // 
            this.txtResourcename.LabelText = null;
            this.txtResourcename.LanguageKey = "RESOURCENAME";
            this.txtResourcename.Location = new System.Drawing.Point(388, 8);
            this.txtResourcename.Name = "txtResourcename";
            this.txtResourcename.Size = new System.Drawing.Size(199, 24);
            this.txtResourcename.TabIndex = 120;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
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
            // cboAreaid
            // 
            this.cboAreaid.LabelText = null;
            this.cboAreaid.LanguageKey = null;
            this.cboAreaid.Location = new System.Drawing.Point(87, 8);
            this.cboAreaid.Name = "cboAreaid";
            this.cboAreaid.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAreaid.Properties.NullText = "";
            this.cboAreaid.ShowHeader = true;
            this.cboAreaid.Size = new System.Drawing.Size(194, 24);
            this.cboAreaid.TabIndex = 114;
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.smartLabel1.Appearance.Options.UseForeColor = true;
            this.smartLabel1.LanguageKey = "RESOURCENAME";
            this.smartLabel1.Location = new System.Drawing.Point(287, 11);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(39, 18);
            this.smartLabel1.TabIndex = 113;
            this.smartLabel1.Text = "작업장";
            // 
            // lblAreaid
            // 
            this.lblAreaid.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblAreaid.Appearance.Options.UseForeColor = true;
            this.lblAreaid.LanguageKey = "AREANAME";
            this.lblAreaid.Location = new System.Drawing.Point(20, 11);
            this.lblAreaid.Name = "lblAreaid";
            this.lblAreaid.Size = new System.Drawing.Size(39, 18);
            this.lblAreaid.TabIndex = 113;
            this.lblAreaid.Text = "작업장";
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
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "OK";
            this.btnSave.Location = new System.Drawing.Point(507, 7);
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
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(603, 7);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // OutsourcedResourceidpopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 446);
            this.LanguageKey = "OSPRESOURCEIDPOPUP";
            this.Name = "OutsourcedResourceidpopup";
            this.Text = "";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtResourcename.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAreaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartBandedGrid grdSearch;
        private Framework.SmartControls.SmartLabel lblAreaid;
        private Framework.SmartControls.SmartComboBox cboAreaid;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartPanel smartPanel2;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartTextBox txtResourcename;
    }
}