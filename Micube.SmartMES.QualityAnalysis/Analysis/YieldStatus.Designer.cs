namespace Micube.SmartMES.QualityAnalysis
{
    partial class YieldStatus
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
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabPageDayYieldRate = new DevExpress.XtraTab.XtraTabPage();
            this.tabUCDayYieldRate = new Micube.SmartMES.QualityAnalysis.ucDayYieldRate();
            this.tabPageItemYieldRate = new DevExpress.XtraTab.XtraTabPage();
            this.tabUCItemYieldRate = new Micube.SmartMES.QualityAnalysis.ucItemYieldRate();
            this.tabPageLotYieldRate = new DevExpress.XtraTab.XtraTabPage();
            this.tabUCLotYieldRate = new Micube.SmartMES.QualityAnalysis.ucLotYieldRate();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tabPageDayYieldRate.SuspendLayout();
            this.tabPageItemYieldRate.SuspendLayout();
            this.tabPageLotYieldRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 567);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(936, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartTabControl1);
            this.pnlContent.Size = new System.Drawing.Size(936, 571);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1241, 600);
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTabControl1.Location = new System.Drawing.Point(0, 0);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.tabPageDayYieldRate;
            this.smartTabControl1.Size = new System.Drawing.Size(936, 571);
            this.smartTabControl1.TabIndex = 0;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageDayYieldRate,
            this.tabPageItemYieldRate,
            this.tabPageLotYieldRate});
            // 
            // tabPageDayYieldRate
            // 
            this.tabPageDayYieldRate.Controls.Add(this.tabUCDayYieldRate);
            this.smartTabControl1.SetLanguageKey(this.tabPageDayYieldRate, "DAILYYIELD");
            this.tabPageDayYieldRate.Name = "tabPageDayYieldRate";
            this.tabPageDayYieldRate.Size = new System.Drawing.Size(930, 542);
            this.tabPageDayYieldRate.Text = "일별수율";
            // 
            // tabUCDayYieldRate
            // 
            this.tabUCDayYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabUCDayYieldRate.ItemText = "";
            this.tabUCDayYieldRate.Location = new System.Drawing.Point(0, 0);
            this.tabUCDayYieldRate.Name = "tabUCDayYieldRate";
            this.tabUCDayYieldRate.PeriodText = "";
            this.tabUCDayYieldRate.Size = new System.Drawing.Size(930, 542);
            this.tabUCDayYieldRate.TabIndex = 0;
            // 
            // tabPageItemYieldRate
            // 
            this.tabPageItemYieldRate.Controls.Add(this.tabUCItemYieldRate);
            this.smartTabControl1.SetLanguageKey(this.tabPageItemYieldRate, "ITEMYIELD");
            this.tabPageItemYieldRate.Name = "tabPageItemYieldRate";
            this.tabPageItemYieldRate.Size = new System.Drawing.Size(930, 542);
            this.tabPageItemYieldRate.Text = "품목수율";
            // 
            // tabUCItemYieldRate
            // 
            this.tabUCItemYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabUCItemYieldRate.ItemText = "";
            this.tabUCItemYieldRate.Location = new System.Drawing.Point(0, 0);
            this.tabUCItemYieldRate.Name = "tabUCItemYieldRate";
            this.tabUCItemYieldRate.PeriodText = "";
            this.tabUCItemYieldRate.Size = new System.Drawing.Size(930, 542);
            this.tabUCItemYieldRate.TabIndex = 0;
            // 
            // tabPageLotYieldRate
            // 
            this.tabPageLotYieldRate.Controls.Add(this.tabUCLotYieldRate);
            this.smartTabControl1.SetLanguageKey(this.tabPageLotYieldRate, "LOTYIELD");
            this.tabPageLotYieldRate.Name = "tabPageLotYieldRate";
            this.tabPageLotYieldRate.Size = new System.Drawing.Size(930, 542);
            this.tabPageLotYieldRate.Text = "LOT수율";
            // 
            // tabUCLotYieldRate
            // 
            this.tabUCLotYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabUCLotYieldRate.ItemText = "";
            this.tabUCLotYieldRate.Location = new System.Drawing.Point(0, 0);
            this.tabUCLotYieldRate.Name = "tabUCLotYieldRate";
            this.tabUCLotYieldRate.PeriodText = "";
            this.tabUCLotYieldRate.Size = new System.Drawing.Size(930, 542);
            this.tabUCLotYieldRate.TabIndex = 0;
            // 
            // YieldStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 638);
            this.Name = "YieldStatus";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tabPageDayYieldRate.ResumeLayout(false);
            this.tabPageItemYieldRate.ResumeLayout(false);
            this.tabPageLotYieldRate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPageDayYieldRate;
        private DevExpress.XtraTab.XtraTabPage tabPageItemYieldRate;
        private DevExpress.XtraTab.XtraTabPage tabPageLotYieldRate;
        private ucDayYieldRate tabUCDayYieldRate;
        private ucLotYieldRate tabUCLotYieldRate;
        private ucItemYieldRate tabUCItemYieldRate;
    }
}