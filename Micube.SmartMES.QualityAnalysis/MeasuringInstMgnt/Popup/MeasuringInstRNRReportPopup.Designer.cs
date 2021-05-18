namespace Micube.SmartMES.QualityAnalysis
{
    partial class MeasuringInstRNRReportPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasuringInstRNRReportPopup));
            this.grpBottom = new DevExpress.XtraEditors.GroupControl();
            this.btnExport = new Micube.Framework.SmartControls.SmartButton();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            this.shtRNR = new Micube.Framework.SmartControls.SmartSpreadSheet();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpBottom)).BeginInit();
            this.grpBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.shtRNR);
            this.pnlMain.Controls.Add(this.grpBottom);
            this.pnlMain.Size = new System.Drawing.Size(747, 741);
            // 
            // grpBottom
            // 
            this.grpBottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.grpBottom.Appearance.Options.UseBackColor = true;
            this.grpBottom.Controls.Add(this.btnExport);
            this.grpBottom.Controls.Add(this.btnClose);
            this.grpBottom.Controls.Add(this.btnSave);
            this.grpBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpBottom.Location = new System.Drawing.Point(0, 707);
            this.grpBottom.Name = "grpBottom";
            this.grpBottom.ShowCaption = false;
            this.grpBottom.Size = new System.Drawing.Size(747, 34);
            this.grpBottom.TabIndex = 102;
            this.grpBottom.Text = "groupControl1";
            // 
            // btnExport
            // 
            this.btnExport.AllowFocus = false;
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.IsBusy = false;
            this.btnExport.IsWrite = false;
            this.btnExport.LanguageKey = "EXPORT";
            this.btnExport.Location = new System.Drawing.Point(490, 5);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExport.Size = new System.Drawing.Size(80, 25);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export";
            this.btnExport.TooltipLanguageKey = "";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(662, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(576, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "저장";
            this.btnSave.TooltipLanguageKey = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // shtRNR
            // 
            this.shtRNR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shtRNR.Location = new System.Drawing.Point(0, 0);
            this.shtRNR.Name = "shtRNR";
            this.shtRNR.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("shtRNR.Options.Import.Csv.Encoding")));
            this.shtRNR.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("shtRNR.Options.Import.Txt.Encoding")));
            this.shtRNR.Size = new System.Drawing.Size(747, 707);
            this.shtRNR.TabIndex = 103;
            this.shtRNR.Text = "smartSpreadSheet1";
            // 
            // MeasuringInstRNRReportPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 761);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "ETCHINGRATEREMEASUREPOPUP";
            this.Name = "MeasuringInstRNRReportPopup";
            this.Text = "MeasuringInstRNRReportPopup";
            this.Load += new System.EventHandler(this.MeasuringInstRNRReportPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBottom)).EndInit();
            this.grpBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpBottom;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartSpreadSheet shtRNR;
        private Framework.SmartControls.SmartButton btnExport;
    }
}