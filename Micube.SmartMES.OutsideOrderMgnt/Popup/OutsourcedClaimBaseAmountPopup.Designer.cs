namespace Micube.SmartMES.OutsideOrderMgnt.Popup
{
    partial class OutsourcedClaimBaseAmountPopup
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
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdClaimBaseAmountPopup = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel5 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblProductdefid = new Micube.Framework.SmartControls.SmartLabel();
            this.usrProductdefid = new Micube.SmartMES.OutsideOrderMgnt.ucItemPopup();
            this.smartSplitTableLayoutPanel6 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.dtpStdDate = new Micube.Framework.SmartControls.SmartDateEdit();
            this.txtExchangevalue = new Micube.Framework.SmartControls.SmartTextBox();
            this.cboCurrencyunitTo = new Micube.Framework.SmartControls.SmartComboBox();
            this.cboCurrencyunit = new Micube.Framework.SmartControls.SmartComboBox();
            this.lblExchangevalue = new Micube.Framework.SmartControls.SmartLabel();
            this.lblStandardym = new Micube.Framework.SmartControls.SmartLabel();
            this.lblTounit = new Micube.Framework.SmartControls.SmartLabel();
            this.lblFromUnit = new Micube.Framework.SmartControls.SmartLabel();
            this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tplMain.SuspendLayout();
            this.smartSplitTableLayoutPanel5.SuspendLayout();
            this.smartSplitTableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStdDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStdDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangevalue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrencyunitTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrencyunit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tplMain);
            this.pnlMain.Size = new System.Drawing.Size(1007, 605);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.grdClaimBaseAmountPopup, 0, 2);
            this.tplMain.Controls.Add(this.smartSplitTableLayoutPanel5, 0, 0);
            this.tplMain.Controls.Add(this.smartSplitTableLayoutPanel6, 0, 1);
            this.tplMain.Controls.Add(this.btnClose, 0, 3);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 4;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tplMain.Size = new System.Drawing.Size(1007, 605);
            this.tplMain.TabIndex = 2;
            // 
            // grdClaimBaseAmountPopup
            // 
            this.grdClaimBaseAmountPopup.Caption = "공정별 Claim 금액기준 목록";
            this.grdClaimBaseAmountPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimBaseAmountPopup.IsUsePaging = false;
            this.grdClaimBaseAmountPopup.LanguageKey = "OUTSOURCEDCLAIMBASEAMOUNT";
            this.grdClaimBaseAmountPopup.Location = new System.Drawing.Point(0, 63);
            this.grdClaimBaseAmountPopup.Margin = new System.Windows.Forms.Padding(0);
            this.grdClaimBaseAmountPopup.Name = "grdClaimBaseAmountPopup";
            this.grdClaimBaseAmountPopup.ShowBorder = true;
            this.grdClaimBaseAmountPopup.Size = new System.Drawing.Size(1007, 492);
            this.grdClaimBaseAmountPopup.TabIndex = 111;
            // 
            // smartSplitTableLayoutPanel5
            // 
            this.smartSplitTableLayoutPanel5.ColumnCount = 2;
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.smartSplitTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel5.Controls.Add(this.lblProductdefid, 0, 0);
            this.smartSplitTableLayoutPanel5.Controls.Add(this.usrProductdefid, 1, 0);
            this.smartSplitTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel5.Name = "smartSplitTableLayoutPanel5";
            this.smartSplitTableLayoutPanel5.RowCount = 1;
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel5.Size = new System.Drawing.Size(1007, 30);
            this.smartSplitTableLayoutPanel5.TabIndex = 0;
            // 
            // lblProductdefid
            // 
            this.lblProductdefid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductdefid.LanguageKey = "PRODUCTDEFID";
            this.lblProductdefid.Location = new System.Drawing.Point(3, 3);
            this.lblProductdefid.Name = "lblProductdefid";
            this.lblProductdefid.Size = new System.Drawing.Size(109, 24);
            this.lblProductdefid.TabIndex = 2;
            this.lblProductdefid.Text = "품목코드";
            // 
            // usrProductdefid
            // 
            this.usrProductdefid.blReadOnly = false;
            this.usrProductdefid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usrProductdefid.Enabled = false;
            this.usrProductdefid.Location = new System.Drawing.Point(118, 2);
            this.usrProductdefid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 1);
            this.usrProductdefid.Name = "usrProductdefid";
            this.usrProductdefid.Size = new System.Drawing.Size(886, 27);
            this.usrProductdefid.strTempPlantid = "";
            this.usrProductdefid.TabIndex = 6;
            // 
            // smartSplitTableLayoutPanel6
            // 
            this.smartSplitTableLayoutPanel6.ColumnCount = 10;
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.smartSplitTableLayoutPanel6.Controls.Add(this.dtpStdDate, 5, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.txtExchangevalue, 7, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.cboCurrencyunitTo, 3, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.cboCurrencyunit, 1, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.lblExchangevalue, 6, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.lblStandardym, 4, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.lblTounit, 2, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.lblFromUnit, 0, 0);
            this.smartSplitTableLayoutPanel6.Controls.Add(this.btnSearch, 9, 0);
            this.smartSplitTableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel6.Location = new System.Drawing.Point(0, 30);
            this.smartSplitTableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel6.Name = "smartSplitTableLayoutPanel6";
            this.smartSplitTableLayoutPanel6.RowCount = 1;
            this.smartSplitTableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel6.Size = new System.Drawing.Size(1007, 33);
            this.smartSplitTableLayoutPanel6.TabIndex = 1;
            // 
            // dtpStdDate
            // 
            this.dtpStdDate.EditValue = null;
            this.dtpStdDate.LabelText = null;
            this.dtpStdDate.LanguageKey = null;
            this.dtpStdDate.Location = new System.Drawing.Point(563, 3);
            this.dtpStdDate.Name = "dtpStdDate";
            this.dtpStdDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStdDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStdDate.Properties.DisplayFormat.FormatString = "yyyy-MM";
            this.dtpStdDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStdDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtpStdDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStdDate.Properties.Mask.EditMask = "yyyy-MM";
            this.dtpStdDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dtpStdDate.Size = new System.Drawing.Size(94, 24);
            this.dtpStdDate.TabIndex = 16;
            // 
            // txtExchangevalue
            // 
            this.txtExchangevalue.Enabled = false;
            this.txtExchangevalue.LabelText = null;
            this.txtExchangevalue.LanguageKey = null;
            this.txtExchangevalue.Location = new System.Drawing.Point(803, 3);
            this.txtExchangevalue.Name = "txtExchangevalue";
            this.txtExchangevalue.Properties.Appearance.Options.UseTextOptions = true;
            this.txtExchangevalue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtExchangevalue.Properties.DisplayFormat.FormatString = "#,###,###,##0.##";
            this.txtExchangevalue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtExchangevalue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtExchangevalue.Properties.Mask.EditMask = "#,###,###,##0";
            this.txtExchangevalue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtExchangevalue.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtExchangevalue.Size = new System.Drawing.Size(94, 24);
            this.txtExchangevalue.TabIndex = 8;
            // 
            // cboCurrencyunitTo
            // 
            this.cboCurrencyunitTo.LabelText = null;
            this.cboCurrencyunitTo.LanguageKey = "OSPETCTYPE";
            this.cboCurrencyunitTo.Location = new System.Drawing.Point(343, 3);
            this.cboCurrencyunitTo.Name = "cboCurrencyunitTo";
            this.cboCurrencyunitTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCurrencyunitTo.Properties.NullText = "";
            this.cboCurrencyunitTo.ShowHeader = true;
            this.cboCurrencyunitTo.Size = new System.Drawing.Size(94, 24);
            this.cboCurrencyunitTo.TabIndex = 7;
            // 
            // cboCurrencyunit
            // 
            this.cboCurrencyunit.Enabled = false;
            this.cboCurrencyunit.LabelText = null;
            this.cboCurrencyunit.LanguageKey = "OSPETCTYPE";
            this.cboCurrencyunit.Location = new System.Drawing.Point(123, 3);
            this.cboCurrencyunit.Name = "cboCurrencyunit";
            this.cboCurrencyunit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCurrencyunit.Properties.NullText = "";
            this.cboCurrencyunit.ShowHeader = true;
            this.cboCurrencyunit.Size = new System.Drawing.Size(94, 24);
            this.cboCurrencyunit.TabIndex = 7;
            // 
            // lblExchangevalue
            // 
            this.lblExchangevalue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExchangevalue.LanguageKey = "OSPEXCHANGEVALUE";
            this.lblExchangevalue.Location = new System.Drawing.Point(663, 3);
            this.lblExchangevalue.Name = "lblExchangevalue";
            this.lblExchangevalue.Size = new System.Drawing.Size(134, 27);
            this.lblExchangevalue.TabIndex = 2;
            this.lblExchangevalue.Text = "계획환율";
            // 
            // lblStandardym
            // 
            this.lblStandardym.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStandardym.LanguageKey = "STANDARDYM";
            this.lblStandardym.Location = new System.Drawing.Point(443, 3);
            this.lblStandardym.Name = "lblStandardym";
            this.lblStandardym.Size = new System.Drawing.Size(114, 27);
            this.lblStandardym.TabIndex = 2;
            this.lblStandardym.Text = "기준년월";
            // 
            // lblTounit
            // 
            this.lblTounit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTounit.LanguageKey = "TOUNIT";
            this.lblTounit.Location = new System.Drawing.Point(223, 3);
            this.lblTounit.Name = "lblTounit";
            this.lblTounit.Size = new System.Drawing.Size(114, 27);
            this.lblTounit.TabIndex = 2;
            this.lblTounit.Text = "To 단위";
            // 
            // lblFromUnit
            // 
            this.lblFromUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFromUnit.LanguageKey = "FROMUNIT";
            this.lblFromUnit.Location = new System.Drawing.Point(3, 3);
            this.lblFromUnit.Name = "lblFromUnit";
            this.lblFromUnit.Size = new System.Drawing.Size(114, 27);
            this.lblFromUnit.TabIndex = 2;
            this.lblFromUnit.Text = "From 단위";
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.IsBusy = false;
            this.btnSearch.LanguageKey = "SEARCH";
            this.btnSearch.Location = new System.Drawing.Point(924, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 110;
            this.btnSearch.Text = "조회";
            this.btnSearch.TooltipLanguageKey = "";
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.IsBusy = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(463, 567);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 110;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // OutsourcedClaimBaseAmountPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 625);
            this.LanguageKey = "OUTSOURCEDCLAIMBASEAMOUNTPOPUP";
            this.Name = "OutsourcedClaimBaseAmountPopup";
            this.Text = "제품조회";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tplMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel5.ResumeLayout(false);
            this.smartSplitTableLayoutPanel5.PerformLayout();
            this.smartSplitTableLayoutPanel6.ResumeLayout(false);
            this.smartSplitTableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStdDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStdDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangevalue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrencyunitTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrencyunit.Properties)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdClaimBaseAmountPopup;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel5;
        private Framework.SmartControls.SmartLabel lblProductdefid;
        private ucItemPopup usrProductdefid;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel6;
        private Framework.SmartControls.SmartDateEdit dtpStdDate;
        private Framework.SmartControls.SmartTextBox txtExchangevalue;
        private Framework.SmartControls.SmartComboBox cboCurrencyunitTo;
        private Framework.SmartControls.SmartComboBox cboCurrencyunit;
        private Framework.SmartControls.SmartLabel lblExchangevalue;
        private Framework.SmartControls.SmartLabel lblStandardym;
        private Framework.SmartControls.SmartLabel lblTounit;
        private Framework.SmartControls.SmartLabel lblFromUnit;
        private Framework.SmartControls.SmartButton btnSearch;
        private Framework.SmartControls.SmartButton btnClose;
    }
}