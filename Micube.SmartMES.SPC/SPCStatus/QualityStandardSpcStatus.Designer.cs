namespace Micube.SmartMES.SPC
{
    partial class QualityStandardSpcStatus
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
            this.tabControlChart = new DevExpress.XtraTab.XtraTabPage();
            this.tabProcess = new DevExpress.XtraTab.XtraTabPage();
            this.tabAnalysis = new DevExpress.XtraTab.XtraTabPage();
            this.ucAnalysisPlot1 = new Micube.SmartMES.SPC.UserControl.ucAnalysisPlot();
            this.tabRowData = new DevExpress.XtraTab.XtraTabPage();
            this.btnChartAnalysisRawData = new Micube.Framework.SmartControls.SmartButton();
            this.grdRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabOverRules = new DevExpress.XtraTab.XtraTabPage();
            this.grdOverRules = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartButton1 = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabAnalysis.SuspendLayout();
            this.tabRowData.SuspendLayout();
            this.tabOverRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(475, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(475, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(780, 430);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabControlChart;
            this.tabMain.Size = new System.Drawing.Size(475, 401);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabControlChart,
            this.tabProcess,
            this.tabAnalysis,
            this.tabRowData,
            this.tabOverRules});
            // 
            // tabControlChart
            // 
            this.tabControlChart.Name = "tabControlChart";
            this.tabControlChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabControlChart.Size = new System.Drawing.Size(469, 372);
            this.tabControlChart.Text = "ControlChart";
            // 
            // tabProcess
            // 
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcess.Size = new System.Drawing.Size(750, 460);
            this.tabProcess.Text = "Process";
            // 
            // tabAnalysis
            // 
            this.tabAnalysis.Controls.Add(this.ucAnalysisPlot1);
            this.tabAnalysis.Name = "tabAnalysis";
            this.tabAnalysis.Size = new System.Drawing.Size(750, 460);
            this.tabAnalysis.Text = "Analysis";
            // 
            // ucAnalysisPlot1
            // 
            this.ucAnalysisPlot1.Appearance.BackColor = System.Drawing.Color.White;
            this.ucAnalysisPlot1.Appearance.Options.UseBackColor = true;
            this.ucAnalysisPlot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAnalysisPlot1.Location = new System.Drawing.Point(0, 0);
            this.ucAnalysisPlot1.Name = "ucAnalysisPlot1";
            this.ucAnalysisPlot1.Size = new System.Drawing.Size(750, 460);
            this.ucAnalysisPlot1.TabIndex = 0;
            // 
            // tabRowData
            // 
            this.tabRowData.Controls.Add(this.btnChartAnalysisRawData);
            this.tabRowData.Controls.Add(this.grdRawData);
            this.tabRowData.Name = "tabRowData";
            this.tabRowData.Padding = new System.Windows.Forms.Padding(3);
            this.tabRowData.Size = new System.Drawing.Size(469, 372);
            this.tabRowData.Text = "Row Data";
            // 
            // btnChartAnalysisRawData
            // 
            this.btnChartAnalysisRawData.AllowFocus = false;
            this.btnChartAnalysisRawData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChartAnalysisRawData.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnChartAnalysisRawData.IsBusy = false;
            this.btnChartAnalysisRawData.IsWrite = false;
            this.btnChartAnalysisRawData.Location = new System.Drawing.Point(293, 4);
            this.btnChartAnalysisRawData.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnChartAnalysisRawData.Name = "btnChartAnalysisRawData";
            this.btnChartAnalysisRawData.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnChartAnalysisRawData.Size = new System.Drawing.Size(140, 25);
            this.btnChartAnalysisRawData.TabIndex = 1;
            this.btnChartAnalysisRawData.Text = "분석 Raw Data";
            this.btnChartAnalysisRawData.TooltipLanguageKey = "";
            this.btnChartAnalysisRawData.Click += new System.EventHandler(this.btnChartAnalysisRawData_Click);
            // 
            // grdRawData
            // 
            this.grdRawData.Caption = "";
            this.grdRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawData.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRawData.IsUsePaging = false;
            this.grdRawData.LanguageKey = "SPCRAWDATA";
            this.grdRawData.Location = new System.Drawing.Point(3, 3);
            this.grdRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawData.Name = "grdRawData";
            this.grdRawData.ShowBorder = true;
            this.grdRawData.ShowStatusBar = false;
            this.grdRawData.Size = new System.Drawing.Size(463, 366);
            this.grdRawData.TabIndex = 0;
            this.grdRawData.UseAutoBestFitColumns = false;
            // 
            // tabOverRules
            // 
            this.tabOverRules.Controls.Add(this.grdOverRules);
            this.tabOverRules.Name = "tabOverRules";
            this.tabOverRules.Padding = new System.Windows.Forms.Padding(3);
            this.tabOverRules.Size = new System.Drawing.Size(750, 460);
            this.tabOverRules.Text = "Over Rules";
            // 
            // grdOverRules
            // 
            this.grdOverRules.Caption = "";
            this.grdOverRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOverRules.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOverRules.IsUsePaging = false;
            this.grdOverRules.LanguageKey = "SPCOVERRAWDATA";
            this.grdOverRules.Location = new System.Drawing.Point(3, 3);
            this.grdOverRules.Margin = new System.Windows.Forms.Padding(0);
            this.grdOverRules.Name = "grdOverRules";
            this.grdOverRules.ShowBorder = true;
            this.grdOverRules.ShowStatusBar = false;
            this.grdOverRules.Size = new System.Drawing.Size(744, 454);
            this.grdOverRules.TabIndex = 0;
            this.grdOverRules.UseAutoBestFitColumns = false;
            // 
            // smartButton1
            // 
            this.smartButton1.AllowFocus = false;
            this.smartButton1.IsBusy = false;
            this.smartButton1.IsWrite = false;
            this.smartButton1.Location = new System.Drawing.Point(0, 0);
            this.smartButton1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.smartButton1.Name = "smartButton1";
            this.smartButton1.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.smartButton1.Size = new System.Drawing.Size(80, 25);
            this.smartButton1.TabIndex = 5;
            this.smartButton1.Text = "smartButton1";
            this.smartButton1.TooltipLanguageKey = "";
            // 
            // QualityStandardSpcStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "QualityStandardSpcStatus";
            this.Text = " ";
            this.Load += new System.EventHandler(this.QualityStandardSpcStatus_Load);
            this.Resize += new System.EventHandler(this.QualityStandardSpcStatus_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabAnalysis.ResumeLayout(false);
            this.tabRowData.ResumeLayout(false);
            this.tabOverRules.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabControlChart;
        private DevExpress.XtraTab.XtraTabPage tabProcess;
        private DevExpress.XtraTab.XtraTabPage tabRowData;
        private DevExpress.XtraTab.XtraTabPage tabOverRules;
        private Framework.SmartControls.SmartBandedGrid grdOverRules;
        private Framework.SmartControls.SmartBandedGrid grdRawData;
        private DevExpress.XtraTab.XtraTabPage tabAnalysis;
        private UserControl.ucAnalysisPlot ucAnalysisPlot1;
        private Framework.SmartControls.SmartButton btnChartAnalysisRawData;
        private Framework.SmartControls.SmartButton smartButton1;
    }
}