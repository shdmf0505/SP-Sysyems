namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class OutsourcedCostsforMajorSuppliersPopup
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
            this.grdClaimConfirm = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.cboIssettle = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblIssettle = new Micube.Framework.SmartControls.SmartLabel();
            this.txtOspVendorName = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtPeriodname = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtOspVendorid = new Micube.Framework.SmartControls.SmartTextBox();
            this.lblVendorid = new Micube.Framework.SmartControls.SmartLabel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.lblCloseYm = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtAreaname = new Micube.Framework.SmartControls.SmartTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboIssettle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOspVendorName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOspVendorid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaname.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1095, 506);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdClaimConfirm, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1095, 506);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
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
            this.grdClaimConfirm.LanguageKey = "";
            this.grdClaimConfirm.Location = new System.Drawing.Point(0, 40);
            this.grdClaimConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimConfirm.Name = "grdClaimConfirm";
            this.grdClaimConfirm.ShowBorder = true;
            this.grdClaimConfirm.Size = new System.Drawing.Size(1095, 466);
            this.grdClaimConfirm.TabIndex = 112;
            this.grdClaimConfirm.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.txtAreaname);
            this.smartPanel1.Controls.Add(this.cboIssettle);
            this.smartPanel1.Controls.Add(this.lblIssettle);
            this.smartPanel1.Controls.Add(this.txtOspVendorName);
            this.smartPanel1.Controls.Add(this.txtPeriodname);
            this.smartPanel1.Controls.Add(this.txtOspVendorid);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Controls.Add(this.lblVendorid);
            this.smartPanel1.Controls.Add(this.btnClose);
            this.smartPanel1.Controls.Add(this.btnSearch);
            this.smartPanel1.Controls.Add(this.lblCloseYm);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(1095, 40);
            this.smartPanel1.TabIndex = 0;
            // 
            // cboIssettle
            // 
            this.cboIssettle.LabelText = null;
            this.cboIssettle.LanguageKey = "OSPETCTYPE";
            this.cboIssettle.Location = new System.Drawing.Point(809, 8);
            this.cboIssettle.Name = "cboIssettle";
            this.cboIssettle.PopupWidth = 0;
            this.cboIssettle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboIssettle.Properties.NullText = "";
            this.cboIssettle.ShowHeader = true;
            this.cboIssettle.Size = new System.Drawing.Size(106, 24);
            this.cboIssettle.TabIndex = 121;
            this.cboIssettle.VisibleColumns = null;
            this.cboIssettle.VisibleColumnsWidth = null;
            // 
            // lblIssettle
            // 
            this.lblIssettle.LanguageKey = "ISSETTLE";
            this.lblIssettle.Location = new System.Drawing.Point(751, 8);
            this.lblIssettle.Name = "lblIssettle";
            this.lblIssettle.Size = new System.Drawing.Size(52, 18);
            this.lblIssettle.TabIndex = 120;
            this.lblIssettle.Text = "정산여부";
            // 
            // txtOspVendorName
            // 
            this.txtOspVendorName.LabelText = null;
            this.txtOspVendorName.LanguageKey = "EXPSETTLEUSERNAME";
            this.txtOspVendorName.Location = new System.Drawing.Point(302, 8);
            this.txtOspVendorName.Name = "txtOspVendorName";
            this.txtOspVendorName.Properties.ReadOnly = true;
            this.txtOspVendorName.Size = new System.Drawing.Size(196, 24);
            this.txtOspVendorName.TabIndex = 119;
            // 
            // txtPeriodname
            // 
            this.txtPeriodname.LabelText = null;
            this.txtPeriodname.LanguageKey = "EXPSETTLEUSERNAME";
            this.txtPeriodname.Location = new System.Drawing.Point(83, 8);
            this.txtPeriodname.Name = "txtPeriodname";
            this.txtPeriodname.Properties.ReadOnly = true;
            this.txtPeriodname.Size = new System.Drawing.Size(96, 24);
            this.txtPeriodname.TabIndex = 119;
            // 
            // txtOspVendorid
            // 
            this.txtOspVendorid.LabelText = null;
            this.txtOspVendorid.LanguageKey = "EXPSETTLEUSERNAME";
            this.txtOspVendorid.Location = new System.Drawing.Point(287, 8);
            this.txtOspVendorid.Name = "txtOspVendorid";
            this.txtOspVendorid.Properties.ReadOnly = true;
            this.txtOspVendorid.Size = new System.Drawing.Size(10, 24);
            this.txtOspVendorid.TabIndex = 119;
            this.txtOspVendorid.Visible = false;
            // 
            // lblVendorid
            // 
            this.lblVendorid.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblVendorid.Appearance.Options.UseForeColor = true;
            this.lblVendorid.LanguageKey = "OSPVENDORNAME";
            this.lblVendorid.Location = new System.Drawing.Point(208, 11);
            this.lblVendorid.Name = "lblVendorid";
            this.lblVendorid.Size = new System.Drawing.Size(39, 18);
            this.lblVendorid.TabIndex = 118;
            this.lblVendorid.Text = "협력사";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(1011, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 24);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.IsWrite = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(923, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 116;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // lblCloseYm
            // 
            this.lblCloseYm.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblCloseYm.Appearance.Options.UseForeColor = true;
            this.lblCloseYm.LanguageKey = "CLOSEYM";
            this.lblCloseYm.Location = new System.Drawing.Point(11, 11);
            this.lblCloseYm.Name = "lblCloseYm";
            this.lblCloseYm.Size = new System.Drawing.Size(52, 18);
            this.lblCloseYm.TabIndex = 113;
            this.lblCloseYm.Text = "마감년도";
            // 
            // smartLabel1
            // 
            this.smartLabel1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.smartLabel1.Appearance.Options.UseForeColor = true;
            this.smartLabel1.LanguageKey = "AREANAME";
            this.smartLabel1.Location = new System.Drawing.Point(540, 11);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(39, 18);
            this.smartLabel1.TabIndex = 118;
            this.smartLabel1.Text = "협력사";
            // 
            // txtAreaname
            // 
            this.txtAreaname.LabelText = null;
            this.txtAreaname.LanguageKey = "EXPSETTLEUSERNAME";
            this.txtAreaname.Location = new System.Drawing.Point(601, 8);
            this.txtAreaname.Name = "txtAreaname";
            this.txtAreaname.Properties.ReadOnly = true;
            this.txtAreaname.Size = new System.Drawing.Size(144, 24);
            this.txtAreaname.TabIndex = 122;
            // 
            // OutsourcedCostsforMajorSuppliersPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 526);
            this.LanguageKey = "OUTSOURCEDCOSTSFORMAJORSUPPLIERSPOPUP";
            this.Name = "OutsourcedCostsforMajorSuppliersPopup";
            this.Text = "Claim마감기간";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboIssettle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOspVendorName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOspVendorid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaname.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartBandedGrid grdClaimConfirm;
        private Framework.SmartControls.SmartLabel lblCloseYm;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartLabel lblVendorid;
        private Framework.SmartControls.SmartTextBox txtOspVendorName;
        private Framework.SmartControls.SmartTextBox txtOspVendorid;
        private Framework.SmartControls.SmartTextBox txtPeriodname;
        private Framework.SmartControls.SmartLabel lblIssettle;
        private Framework.SmartControls.SmartComboBox cboIssettle;
        private Framework.SmartControls.SmartTextBox txtAreaname;
        private Framework.SmartControls.SmartLabel smartLabel1;
    }
}