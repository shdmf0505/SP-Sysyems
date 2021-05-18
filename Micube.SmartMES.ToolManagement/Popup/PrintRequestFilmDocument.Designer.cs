namespace Micube.SmartMES.ToolManagement.Popup
{
    partial class PrintRequestFilmDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintRequestFilmDocument));
            this.smartSplitTableLayoutPanel4 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            this.dcDocument = new Micube.Framework.SmartControls.SmartSpreadSheet();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel4);
            this.pnlMain.Size = new System.Drawing.Size(1015, 576);
            // 
            // smartSplitTableLayoutPanel4
            // 
            this.smartSplitTableLayoutPanel4.ColumnCount = 1;
            this.smartSplitTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.smartSplitTableLayoutPanel4.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.smartSplitTableLayoutPanel4.Controls.Add(this.dcDocument, 0, 1);
            this.smartSplitTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel4.Name = "smartSplitTableLayoutPanel4";
            this.smartSplitTableLayoutPanel4.RowCount = 2;
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.smartSplitTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.smartSplitTableLayoutPanel4.Size = new System.Drawing.Size(1015, 576);
            this.smartSplitTableLayoutPanel4.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnExit);
            this.flowLayoutPanel1.Controls.Add(this.btnPrint);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1015, 33);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.AllowFocus = false;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.IsBusy = false;
            this.btnExit.IsWrite = false;
            this.btnExit.LanguageKey = "CLOSE";
            this.btnExit.Location = new System.Drawing.Point(932, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnExit.Size = new System.Drawing.Size(80, 25);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Close";
            this.btnExit.TooltipLanguageKey = "";
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.IsBusy = false;
            this.btnPrint.IsWrite = false;
            this.btnPrint.LanguageKey = "PRINT";
            this.btnPrint.Location = new System.Drawing.Point(846, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrint.Size = new System.Drawing.Size(80, 25);
            this.btnPrint.TabIndex = 111;
            this.btnPrint.Text = "Print";
            this.btnPrint.TooltipLanguageKey = "";
            // 
            // dcDocument
            // 
            this.dcDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcDocument.Location = new System.Drawing.Point(3, 36);
            this.dcDocument.Name = "dcDocument";
            this.dcDocument.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("dcDocument.Options.Import.Csv.Encoding")));
            this.dcDocument.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("dcDocument.Options.Import.Txt.Encoding")));
            this.dcDocument.Size = new System.Drawing.Size(1009, 537);
            this.dcDocument.TabIndex = 1;
            // 
            // PrintRequestFilmDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 596);
            this.LanguageKey = "REQUESTFILMREPORT";
            this.Name = "PrintRequestFilmDocument";
            this.Text = "REQUESTFILMREPORT";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnExit;
        private Framework.SmartControls.SmartButton btnPrint;
        private Framework.SmartControls.SmartSpreadSheet dcDocument;
    }
}