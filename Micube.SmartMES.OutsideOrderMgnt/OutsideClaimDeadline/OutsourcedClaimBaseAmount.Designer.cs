namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcedClaimBaseAmount
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
            this.tplMain = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdOutsourcedClaimBaseAmount = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnCal = new Micube.Framework.SmartControls.SmartButton();
            this.btnExchang = new Micube.Framework.SmartControls.SmartButton();
            this.txtPcsmm = new Micube.Framework.SmartControls.SmartTextBox();
            this.txtDefectamount = new Micube.Framework.SmartControls.SmartTextBox();
            this.cboCurrencyunit = new Micube.Framework.SmartControls.SmartComboBox();
            this.usrProductdefid = new Micube.SmartMES.OutsideOrderMgnt.ucItemPopup();
            this.lblPcsmm = new Micube.Framework.SmartControls.SmartLabel();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.lblCurrencyunit = new Micube.Framework.SmartControls.SmartLabel();
            this.lblProductdefid = new Micube.Framework.SmartControls.SmartLabel();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnCopy = new Micube.Framework.SmartControls.SmartButton();
            this.lblProductdefidTO = new Micube.Framework.SmartControls.SmartLabel();
            this.usrProductdefidTO = new Micube.SmartMES.OutsideOrderMgnt.ucItemPopup();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsmm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectamount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrencyunit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 908);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(845, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tplMain);
            this.pnlContent.Size = new System.Drawing.Size(845, 911);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1226, 947);
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.grdOutsourcedClaimBaseAmount, 0, 2);
            this.tplMain.Controls.Add(this.smartSplitTableLayoutPanel1, 0, 0);
            this.tplMain.Controls.Add(this.smartPanel1, 0, 1);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 3;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Size = new System.Drawing.Size(845, 911);
            this.tplMain.TabIndex = 1;
            // 
            // grdOutsourcedClaimBaseAmount
            // 
            this.grdOutsourcedClaimBaseAmount.Caption = "공정별 Claim 금액기준 목록";
            this.grdOutsourcedClaimBaseAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOutsourcedClaimBaseAmount.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOutsourcedClaimBaseAmount.IsUsePaging = false;
            this.grdOutsourcedClaimBaseAmount.LanguageKey = "OUTSOURCEDCLAIMBASEAMOUNT";
            this.grdOutsourcedClaimBaseAmount.Location = new System.Drawing.Point(0, 95);
            this.grdOutsourcedClaimBaseAmount.Margin = new System.Windows.Forms.Padding(0);
            this.grdOutsourcedClaimBaseAmount.Name = "grdOutsourcedClaimBaseAmount";
            this.grdOutsourcedClaimBaseAmount.ShowBorder = true;
            this.grdOutsourcedClaimBaseAmount.Size = new System.Drawing.Size(845, 816);
            this.grdOutsourcedClaimBaseAmount.TabIndex = 1;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 7;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnCal, 6, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.btnExchang, 6, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtPcsmm, 5, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.txtDefectamount, 3, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.cboCurrencyunit, 1, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.usrProductdefid, 1, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblPcsmm, 4, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartLabel2, 2, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblCurrencyunit, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblProductdefid, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(845, 60);
            this.smartSplitTableLayoutPanel1.TabIndex = 2;
            // 
            // btnCal
            // 
            this.btnCal.AllowFocus = false;
            this.btnCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCal.IsBusy = false;
            this.btnCal.IsWrite = true;
            this.btnCal.LanguageKey = "RECALCULATION";
            this.btnCal.Location = new System.Drawing.Point(708, 0);
            this.btnCal.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCal.Name = "btnCal";
            this.btnCal.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCal.Size = new System.Drawing.Size(134, 25);
            this.btnCal.TabIndex = 8;
            this.btnCal.Text = "재계산";
            this.btnCal.TooltipLanguageKey = "";
            this.btnCal.Visible = false;
            // 
            // btnExchang
            // 
            this.btnExchang.AllowFocus = false;
            this.btnExchang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExchang.IsBusy = false;
            this.btnExchang.IsWrite = true;
            this.btnExchang.LanguageKey = "CONVERTAMOUNTVIEW";
            this.btnExchang.Location = new System.Drawing.Point(708, 30);
            this.btnExchang.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnExchang.Name = "btnExchang";
            this.btnExchang.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExchang.Size = new System.Drawing.Size(134, 25);
            this.btnExchang.TabIndex = 8;
            this.btnExchang.Text = "환산금액조회";
            this.btnExchang.TooltipLanguageKey = "";
            this.btnExchang.Visible = false;
            // 
            // txtPcsmm
            // 
            this.txtPcsmm.Enabled = false;
            this.txtPcsmm.LabelText = null;
            this.txtPcsmm.LanguageKey = null;
            this.txtPcsmm.Location = new System.Drawing.Point(588, 33);
            this.txtPcsmm.Name = "txtPcsmm";
            this.txtPcsmm.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPcsmm.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtPcsmm.Properties.DisplayFormat.FormatString = "#,###,###,##0.##";
            this.txtPcsmm.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPcsmm.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPcsmm.Properties.Mask.EditMask = "#,###,###,##0";
            this.txtPcsmm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPcsmm.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPcsmm.Size = new System.Drawing.Size(114, 24);
            this.txtPcsmm.TabIndex = 7;
            // 
            // txtDefectamount
            // 
            this.txtDefectamount.LabelText = null;
            this.txtDefectamount.LanguageKey = null;
            this.txtDefectamount.Location = new System.Drawing.Point(353, 33);
            this.txtDefectamount.Name = "txtDefectamount";
            this.txtDefectamount.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDefectamount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDefectamount.Properties.DisplayFormat.FormatString = "#,###,###,##0";
            this.txtDefectamount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDefectamount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDefectamount.Properties.Mask.EditMask = "#,###,###,##0";
            this.txtDefectamount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDefectamount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDefectamount.Size = new System.Drawing.Size(114, 24);
            this.txtDefectamount.TabIndex = 7;
            // 
            // cboCurrencyunit
            // 
            this.cboCurrencyunit.LabelText = null;
            this.cboCurrencyunit.LanguageKey = "OSPETCTYPE";
            this.cboCurrencyunit.Location = new System.Drawing.Point(118, 33);
            this.cboCurrencyunit.Name = "cboCurrencyunit";
            this.cboCurrencyunit.PopupWidth = 0;
            this.cboCurrencyunit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCurrencyunit.Properties.NullText = "";
            this.cboCurrencyunit.ShowHeader = true;
            this.cboCurrencyunit.Size = new System.Drawing.Size(114, 24);
            this.cboCurrencyunit.TabIndex = 6;
            this.cboCurrencyunit.VisibleColumns = null;
            this.cboCurrencyunit.VisibleColumnsWidth = null;
            // 
            // usrProductdefid
            // 
            this.usrProductdefid.blReadOnly = false;
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.usrProductdefid, 5);
            this.usrProductdefid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usrProductdefid.Location = new System.Drawing.Point(115, 4);
            this.usrProductdefid.Margin = new System.Windows.Forms.Padding(0, 4, 3, 4);
            this.usrProductdefid.Name = "usrProductdefid";
            this.usrProductdefid.Size = new System.Drawing.Size(587, 22);
            this.usrProductdefid.strTempPlantid = "";
            this.usrProductdefid.TabIndex = 5;
            // 
            // lblPcsmm
            // 
            this.lblPcsmm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPcsmm.LanguageKey = "PCSMM";
            this.lblPcsmm.Location = new System.Drawing.Point(473, 33);
            this.lblPcsmm.Name = "lblPcsmm";
            this.lblPcsmm.Size = new System.Drawing.Size(109, 24);
            this.lblPcsmm.TabIndex = 1;
            this.lblPcsmm.Text = "1MM당 PCS";
            // 
            // smartLabel2
            // 
            this.smartLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartLabel2.LanguageKey = "DEFECTAMOUNT";
            this.smartLabel2.Location = new System.Drawing.Point(238, 33);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(109, 24);
            this.smartLabel2.TabIndex = 1;
            this.smartLabel2.Text = "불량반영금액";
            // 
            // lblCurrencyunit
            // 
            this.lblCurrencyunit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrencyunit.LanguageKey = "CURRENCYUNIT";
            this.lblCurrencyunit.Location = new System.Drawing.Point(3, 33);
            this.lblCurrencyunit.Name = "lblCurrencyunit";
            this.lblCurrencyunit.Size = new System.Drawing.Size(109, 24);
            this.lblCurrencyunit.TabIndex = 1;
            this.lblCurrencyunit.Text = "화폐단위";
            // 
            // lblProductdefid
            // 
            this.lblProductdefid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductdefid.LanguageKey = "PRODUCTDEFID";
            this.lblProductdefid.Location = new System.Drawing.Point(3, 3);
            this.lblProductdefid.Name = "lblProductdefid";
            this.lblProductdefid.Size = new System.Drawing.Size(109, 24);
            this.lblProductdefid.TabIndex = 1;
            this.lblProductdefid.Text = "품목코드";
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.btnCopy);
            this.smartPanel1.Controls.Add(this.lblProductdefidTO);
            this.smartPanel1.Controls.Add(this.usrProductdefidTO);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 60);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(845, 35);
            this.smartPanel1.TabIndex = 3;
            // 
            // btnCopy
            // 
            this.btnCopy.AllowFocus = false;
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.IsBusy = false;
            this.btnCopy.IsWrite = true;
            this.btnCopy.LanguageKey = "COPY";
            this.btnCopy.Location = new System.Drawing.Point(707, 4);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCopy.Size = new System.Drawing.Size(134, 25);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "복사하기";
            this.btnCopy.TooltipLanguageKey = "";
            this.btnCopy.Visible = false;
            // 
            // lblProductdefidTO
            // 
            this.lblProductdefidTO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductdefidTO.LanguageKey = "TOPRODUCTDEFID";
            this.lblProductdefidTO.Location = new System.Drawing.Point(8, 10);
            this.lblProductdefidTO.Name = "lblProductdefidTO";
            this.lblProductdefidTO.Size = new System.Drawing.Size(52, 18);
            this.lblProductdefidTO.TabIndex = 1;
            this.lblProductdefidTO.Text = "품목코드";
            // 
            // usrProductdefidTO
            // 
            this.usrProductdefidTO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.usrProductdefidTO.blReadOnly = false;
            this.usrProductdefidTO.Location = new System.Drawing.Point(115, 6);
            this.usrProductdefidTO.Margin = new System.Windows.Forms.Padding(0, 5, 3, 5);
            this.usrProductdefidTO.Name = "usrProductdefidTO";
            this.usrProductdefidTO.Size = new System.Drawing.Size(587, 26);
            this.usrProductdefidTO.strTempPlantid = "";
            this.usrProductdefidTO.TabIndex = 5;
            // 
            // OutsourcedClaimBaseAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Name = "OutsourcedClaimBaseAmount";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsmm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectamount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrencyunit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplMain;
        private Framework.SmartControls.SmartBandedGrid grdOutsourcedClaimBaseAmount;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartLabel lblProductdefid;
        private ucItemPopup usrProductdefid;
        private Framework.SmartControls.SmartLabel lblPcsmm;
        private Framework.SmartControls.SmartLabel smartLabel2;
        private Framework.SmartControls.SmartLabel lblCurrencyunit;
        private Framework.SmartControls.SmartComboBox cboCurrencyunit;
        private Framework.SmartControls.SmartTextBox txtPcsmm;
        private Framework.SmartControls.SmartTextBox txtDefectamount;
        private Framework.SmartControls.SmartButton btnExchang;
        private Framework.SmartControls.SmartButton btnCal;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnCopy;
        private Framework.SmartControls.SmartLabel lblProductdefidTO;
        private ucItemPopup usrProductdefidTO;
    }
}