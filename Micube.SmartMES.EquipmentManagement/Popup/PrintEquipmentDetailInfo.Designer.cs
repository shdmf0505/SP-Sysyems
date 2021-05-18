namespace Micube.SmartMES.EquipmentManagement.Popup
{
    partial class PrintEquipmentDetailInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintEquipmentDetailInfo));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExit = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            this.dcDocument = new Micube.Framework.SmartControls.SmartSpreadSheet();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dcDocument);
            this.pnlMain.Controls.Add(this.flowLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(837, 690);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnExit);
            this.flowLayoutPanel1.Controls.Add(this.btnPrint);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(837, 33);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.AllowFocus = false;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.IsBusy = false;
            this.btnExit.IsWrite = false;
            this.btnExit.LanguageKey = "CLOSE";
            this.btnExit.Location = new System.Drawing.Point(754, 0);
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
            this.btnPrint.Location = new System.Drawing.Point(668, 0);
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
            this.dcDocument.Location = new System.Drawing.Point(0, 33);
            this.dcDocument.Name = "dcDocument";
            this.dcDocument.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("dcDocument.Options.Import.Csv.Encoding")));
            this.dcDocument.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("dcDocument.Options.Import.Txt.Encoding")));
            this.dcDocument.Size = new System.Drawing.Size(837, 657);
            this.dcDocument.TabIndex = 2;
            // 
            // PrintEquipmentDetailInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 710);
            this.LanguageKey = "EQUIPMENTPMITEMLIST";
            this.Name = "PrintEquipmentDetailInfo";
            this.Text = "EquipmentDetailInfo";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnExit;
        private Framework.SmartControls.SmartButton btnPrint;
        private Framework.SmartControls.SmartSpreadSheet dcDocument;
    }
}