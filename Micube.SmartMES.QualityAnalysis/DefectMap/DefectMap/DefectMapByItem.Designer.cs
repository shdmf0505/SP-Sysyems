namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectMapByItem
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
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tnpLotList = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.chartMain = new Micube.Framework.SmartControls.SmartChart();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabDefectMap = new DevExpress.XtraTab.XtraTabPage();
            this.tabComparison = new DevExpress.XtraTab.XtraTabPage();
            this.tabRawData = new DevExpress.XtraTab.XtraTabPage();
            this.grdRowData = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.btnInterpretation = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tnpLotList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.tabRawData.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 508);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnInterpretation);
            this.pnlToolbar.Size = new System.Drawing.Size(783, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnInterpretation, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(783, 512);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1088, 541);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tnpLotList;
            this.tabMain.Size = new System.Drawing.Size(783, 512);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tnpLotList,
            this.tabDefectMap,
            this.tabComparison,
            this.tabRawData});
            // 
            // tnpLotList
            // 
            this.tnpLotList.Controls.Add(this.smartSpliterContainer1);
            this.tnpLotList.Margin = new System.Windows.Forms.Padding(0);
            this.tnpLotList.Name = "tnpLotList";
            this.tnpLotList.Padding = new System.Windows.Forms.Padding(8);
            this.tnpLotList.Size = new System.Drawing.Size(777, 483);
            this.tnpLotList.Text = "Lot List";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(8, 8);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.chartMain);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdMain);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(761, 467);
            this.smartSpliterContainer1.SplitterPosition = 250;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // chartMain
            // 
            this.chartMain.AutoLayout = false;
            this.chartMain.CacheToMemory = true;
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Legend.Name = "Default Legend";
            this.chartMain.Location = new System.Drawing.Point(0, 0);
            this.chartMain.Name = "chartMain";
            this.chartMain.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartMain.Size = new System.Drawing.Size(761, 250);
            this.chartMain.TabIndex = 0;
            // 
            // grdMain
            // 
            this.grdMain.Caption = "Lot List";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.ShowStatusBar = false;
            this.grdMain.Size = new System.Drawing.Size(761, 212);
            this.grdMain.TabIndex = 3;
            // 
            // tabDefectMap
            // 
            this.tabDefectMap.Margin = new System.Windows.Forms.Padding(0);
            this.tabDefectMap.Name = "tabDefectMap";
            this.tabDefectMap.Padding = new System.Windows.Forms.Padding(8);
            this.tabDefectMap.Size = new System.Drawing.Size(777, 483);
            this.tabDefectMap.Text = "Defect Map";
            // 
            // tabComparison
            // 
            this.tabComparison.Margin = new System.Windows.Forms.Padding(0);
            this.tabComparison.Name = "tabComparison";
            this.tabComparison.Padding = new System.Windows.Forms.Padding(8);
            this.tabComparison.Size = new System.Drawing.Size(469, 372);
            this.tabComparison.Text = "비교";
            // 
            // tabRawData
            // 
            this.tabRawData.Controls.Add(this.grdRowData);
            this.tabRawData.Margin = new System.Windows.Forms.Padding(0);
            this.tabRawData.Name = "tabRawData";
            this.tabRawData.Padding = new System.Windows.Forms.Padding(8);
            this.tabRawData.Size = new System.Drawing.Size(777, 483);
            this.tabRawData.Text = "Raw Data";
            // 
            // grdRowData
            // 
            this.grdRowData.Caption = "";
            this.grdRowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRowData.IsUsePaging = false;
            this.grdRowData.LanguageKey = null;
            this.grdRowData.Location = new System.Drawing.Point(8, 8);
            this.grdRowData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRowData.Name = "grdRowData";
            this.grdRowData.ShowBorder = true;
            this.grdRowData.Size = new System.Drawing.Size(761, 467);
            this.grdRowData.TabIndex = 0;
            // 
            // btnInterpretation
            // 
            this.btnInterpretation.AllowFocus = false;
            this.btnInterpretation.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnInterpretation.IsBusy = false;
            this.btnInterpretation.Location = new System.Drawing.Point(708, 0);
            this.btnInterpretation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnInterpretation.Name = "btnInterpretation";
            this.btnInterpretation.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnInterpretation.Size = new System.Drawing.Size(75, 24);
            this.btnInterpretation.TabIndex = 5;
            this.btnInterpretation.Text = "해석";
            this.btnInterpretation.TooltipLanguageKey = "";
            // 
            // DefectMapByItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 561);
            this.Name = "DefectMapByItem";
            this.Text = "DefectMapByItem";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tnpLotList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.tabRawData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;        
        private DevExpress.XtraTab.XtraTabPage tnpLotList;
        private DevExpress.XtraTab.XtraTabPage tabDefectMap;
        private DevExpress.XtraTab.XtraTabPage tabComparison;
        private Framework.SmartControls.SmartButton btnInterpretation;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartChart chartMain;
        private DevExpress.XtraTab.XtraTabPage tabRawData;
        private Framework.SmartControls.SmartBandedGrid grdRowData;
    }
}