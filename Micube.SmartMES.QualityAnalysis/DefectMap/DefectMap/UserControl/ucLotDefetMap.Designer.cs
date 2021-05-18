namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucLotDefetMap
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabDefectMap = new DevExpress.XtraTab.XtraTabPage();
            this.tabComparison = new DevExpress.XtraTab.XtraTabPage();
            this.tabRawData = new DevExpress.XtraTab.XtraTabPage();
            this.grdRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabRawData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabDefectMap;
            this.tabMain.Size = new System.Drawing.Size(849, 525);
            this.tabMain.TabIndex = 2;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabDefectMap,
            this.tabComparison,
            this.tabRawData});
            // 
            // tabDefectMap
            // 
            this.tabDefectMap.Margin = new System.Windows.Forms.Padding(0);
            this.tabDefectMap.Name = "tabDefectMap";
            this.tabDefectMap.Padding = new System.Windows.Forms.Padding(8);
            this.tabDefectMap.Size = new System.Drawing.Size(843, 496);
            this.tabDefectMap.Text = "Defect Map";
            // 
            // tabComparison
            // 
            this.tabComparison.Margin = new System.Windows.Forms.Padding(0);
            this.tabComparison.Name = "tabComparison";
            this.tabComparison.Padding = new System.Windows.Forms.Padding(8);
            this.tabComparison.Size = new System.Drawing.Size(843, 496);
            this.tabComparison.Text = "비교";
            // 
            // tabRawData
            // 
            this.tabRawData.Controls.Add(this.grdRawData);
            this.tabRawData.Margin = new System.Windows.Forms.Padding(0);
            this.tabRawData.Name = "tabRawData";
            this.tabRawData.Padding = new System.Windows.Forms.Padding(8);
            this.tabRawData.Size = new System.Drawing.Size(843, 496);
            this.tabRawData.Text = "Raw Data";
            // 
            // grdRowData
            // 
            this.grdRawData.Caption = "";
            this.grdRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawData.IsUsePaging = false;
            this.grdRawData.LanguageKey = null;
            this.grdRawData.Location = new System.Drawing.Point(8, 8);
            this.grdRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawData.Name = "grdRowData";
            this.grdRawData.ShowBorder = true;
            this.grdRawData.ShowStatusBar = false;
            this.grdRawData.Size = new System.Drawing.Size(827, 480);
            this.grdRawData.TabIndex = 0;
            // 
            // ucLotDefetMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Name = "ucLotDefetMap";
            this.Size = new System.Drawing.Size(849, 525);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabRawData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabDefectMap;
        private DevExpress.XtraTab.XtraTabPage tabComparison;
        private DevExpress.XtraTab.XtraTabPage tabRawData;
        private Framework.SmartControls.SmartBandedGrid grdRawData;
    }
}
