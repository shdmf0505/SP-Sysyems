namespace Micube.SmartMES.Commons
{
    partial class ProductRevisionInputPopup
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
            this.tlpProductRevisionInputPopup = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblProductRevisionInputContext = new Micube.Framework.SmartControls.SmartLabel();
            this.pnlPrintLotCard = new Micube.Framework.SmartControls.SmartPanel();
            this.btnPrintRCLotCard = new Micube.Framework.SmartControls.SmartButton();
            this.tlpProductRevisionInput = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.txtProductRevisionInput = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.lblProductRevisionInputBottom = new Micube.Framework.SmartControls.SmartLabel();
            this.lblProductRevisionInputTop = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tlpProductRevisionInputPopup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPrintLotCard)).BeginInit();
            this.pnlPrintLotCard.SuspendLayout();
            this.tlpProductRevisionInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductRevisionInput.Properties)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tlpProductRevisionInputPopup);
            this.pnlMain.Size = new System.Drawing.Size(380, 174);
            // 
            // tlpProductRevisionInputPopup
            // 
            this.tlpProductRevisionInputPopup.ColumnCount = 1;
            this.tlpProductRevisionInputPopup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProductRevisionInputPopup.Controls.Add(this.lblProductRevisionInputContext, 0, 0);
            this.tlpProductRevisionInputPopup.Controls.Add(this.pnlPrintLotCard, 0, 2);
            this.tlpProductRevisionInputPopup.Controls.Add(this.tlpProductRevisionInput, 0, 1);
            this.tlpProductRevisionInputPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProductRevisionInputPopup.Location = new System.Drawing.Point(0, 0);
            this.tlpProductRevisionInputPopup.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpProductRevisionInputPopup.Name = "tlpProductRevisionInputPopup";
            this.tlpProductRevisionInputPopup.RowCount = 3;
            this.tlpProductRevisionInputPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProductRevisionInputPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpProductRevisionInputPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpProductRevisionInputPopup.Size = new System.Drawing.Size(380, 174);
            this.tlpProductRevisionInputPopup.TabIndex = 0;
            // 
            // lblProductRevisionInputContext
            // 
            this.lblProductRevisionInputContext.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblProductRevisionInputContext.Appearance.Options.UseFont = true;
            this.lblProductRevisionInputContext.Appearance.Options.UseTextOptions = true;
            this.lblProductRevisionInputContext.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblProductRevisionInputContext.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblProductRevisionInputContext.AutoEllipsis = true;
            this.lblProductRevisionInputContext.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProductRevisionInputContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductRevisionInputContext.Location = new System.Drawing.Point(5, 3);
            this.lblProductRevisionInputContext.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.lblProductRevisionInputContext.Name = "lblProductRevisionInputContext";
            this.lblProductRevisionInputContext.Size = new System.Drawing.Size(370, 94);
            this.lblProductRevisionInputContext.TabIndex = 0;
            this.lblProductRevisionInputContext.Text = "변경 Lot Card의 \'품목 Barcode\' 를 스캔 하십시오. 변경 Lot Card가 없는 경우 출력/교체 후 재 진행 하시기 바랍니다.";
            // 
            // pnlPrintLotCard
            // 
            this.pnlPrintLotCard.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlPrintLotCard.Controls.Add(this.btnPrintRCLotCard);
            this.pnlPrintLotCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrintLotCard.Location = new System.Drawing.Point(5, 152);
            this.pnlPrintLotCard.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.pnlPrintLotCard.Name = "pnlPrintLotCard";
            this.pnlPrintLotCard.Size = new System.Drawing.Size(370, 20);
            this.pnlPrintLotCard.TabIndex = 2;
            // 
            // btnPrintRCLotCard
            // 
            this.btnPrintRCLotCard.AllowFocus = false;
            this.btnPrintRCLotCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrintRCLotCard.IsBusy = false;
            this.btnPrintRCLotCard.IsWrite = false;
            this.btnPrintRCLotCard.Location = new System.Drawing.Point(220, 0);
            this.btnPrintRCLotCard.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrintRCLotCard.Name = "btnPrintRCLotCard";
            this.btnPrintRCLotCard.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrintRCLotCard.Size = new System.Drawing.Size(150, 20);
            this.btnPrintRCLotCard.TabIndex = 0;
            this.btnPrintRCLotCard.Text = "변경 Lot Card 출력";
            this.btnPrintRCLotCard.TooltipLanguageKey = "";
            // 
            // tlpProductRevisionInput
            // 
            this.tlpProductRevisionInput.ColumnCount = 2;
            this.tlpProductRevisionInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpProductRevisionInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProductRevisionInput.Controls.Add(this.txtProductRevisionInput, 1, 0);
            this.tlpProductRevisionInput.Controls.Add(this.smartSplitTableLayoutPanel1, 0, 0);
            this.tlpProductRevisionInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProductRevisionInput.Location = new System.Drawing.Point(0, 99);
            this.tlpProductRevisionInput.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpProductRevisionInput.Name = "tlpProductRevisionInput";
            this.tlpProductRevisionInput.RowCount = 1;
            this.tlpProductRevisionInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProductRevisionInput.Size = new System.Drawing.Size(380, 50);
            this.tlpProductRevisionInput.TabIndex = 3;
            // 
            // txtProductRevisionInput
            // 
            this.txtProductRevisionInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductRevisionInput.EditValue = "";
            this.txtProductRevisionInput.LabelText = null;
            this.txtProductRevisionInput.LanguageKey = null;
            this.txtProductRevisionInput.Location = new System.Drawing.Point(125, 8);
            this.txtProductRevisionInput.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.txtProductRevisionInput.Name = "txtProductRevisionInput";
            this.txtProductRevisionInput.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtProductRevisionInput.Properties.Appearance.Options.UseFont = true;
            this.txtProductRevisionInput.Properties.Appearance.Options.UseTextOptions = true;
            this.txtProductRevisionInput.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtProductRevisionInput.Properties.AutoHeight = false;
            this.txtProductRevisionInput.Size = new System.Drawing.Size(250, 34);
            this.txtProductRevisionInput.TabIndex = 2;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblProductRevisionInputBottom, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblProductRevisionInputTop, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(120, 50);
            this.smartSplitTableLayoutPanel1.TabIndex = 3;
            // 
            // lblProductRevisionInputBottom
            // 
            this.lblProductRevisionInputBottom.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblProductRevisionInputBottom.Appearance.Options.UseFont = true;
            this.lblProductRevisionInputBottom.Appearance.Options.UseTextOptions = true;
            this.lblProductRevisionInputBottom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblProductRevisionInputBottom.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProductRevisionInputBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductRevisionInputBottom.Location = new System.Drawing.Point(5, 27);
            this.lblProductRevisionInputBottom.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.lblProductRevisionInputBottom.Name = "lblProductRevisionInputBottom";
            this.lblProductRevisionInputBottom.Size = new System.Drawing.Size(110, 21);
            this.lblProductRevisionInputBottom.TabIndex = 5;
            this.lblProductRevisionInputBottom.Text = "(Barcode Scan)";
            // 
            // lblProductRevisionInputTop
            // 
            this.lblProductRevisionInputTop.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductRevisionInputTop.Appearance.Options.UseFont = true;
            this.lblProductRevisionInputTop.Appearance.Options.UseTextOptions = true;
            this.lblProductRevisionInputTop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblProductRevisionInputTop.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProductRevisionInputTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductRevisionInputTop.Location = new System.Drawing.Point(5, 2);
            this.lblProductRevisionInputTop.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.lblProductRevisionInputTop.Name = "lblProductRevisionInputTop";
            this.lblProductRevisionInputTop.Size = new System.Drawing.Size(110, 21);
            this.lblProductRevisionInputTop.TabIndex = 4;
            this.lblProductRevisionInputTop.Text = "품목 Rev";
            // 
            // ProductRevisionInputPopup
            // 
            this.Appearance.BackColor = System.Drawing.Color.Red;
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 194);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductRevisionInputPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProductRevisionInputPopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tlpProductRevisionInputPopup.ResumeLayout(false);
            this.tlpProductRevisionInputPopup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPrintLotCard)).EndInit();
            this.pnlPrintLotCard.ResumeLayout(false);
            this.tlpProductRevisionInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProductRevisionInput.Properties)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpProductRevisionInputPopup;
        private Framework.SmartControls.SmartLabel lblProductRevisionInputContext;
        private Framework.SmartControls.SmartPanel pnlPrintLotCard;
        private Framework.SmartControls.SmartButton btnPrintRCLotCard;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpProductRevisionInput;
        private Framework.SmartControls.SmartTextBox txtProductRevisionInput;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartLabel lblProductRevisionInputBottom;
        private Framework.SmartControls.SmartLabel lblProductRevisionInputTop;
    }
}