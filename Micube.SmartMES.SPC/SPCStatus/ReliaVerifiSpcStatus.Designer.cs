﻿namespace Micube.SmartMES.SPC
{
    partial class ReliaVerifiSpcStatus
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.ucXBarFrame1 = new Micube.SmartMES.SPC.UserControl.ucXBarFrame();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.ucCpkFrame1 = new Micube.SmartMES.SPC.UserControl.ucCpkFrame();
            this.tabAnalysis = new DevExpress.XtraTab.XtraTabPage();
            this.ucAnalysisPlot1 = new Micube.SmartMES.SPC.UserControl.ucAnalysisPlot();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.btnChartAnalysisRawData = new Micube.Framework.SmartControls.SmartButton();
            this.grdRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.grdOverRules = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.tabAnalysis.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(475, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
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
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(428, 24);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.SelectedTabPage = this.xtraTabPage1;
            this.tabMain.Size = new System.Drawing.Size(475, 401);
            this.tabMain.TabIndex = 3;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.tabAnalysis,
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.ucXBarFrame1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(469, 372);
            this.xtraTabPage1.Text = "관리도";
            // 
            // ucXBarFrame1
            // 
            this.ucXBarFrame1.Appearance.BackColor = System.Drawing.Color.White;
            this.ucXBarFrame1.Appearance.Options.UseBackColor = true;
            this.ucXBarFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucXBarFrame1.Location = new System.Drawing.Point(0, 0);
            this.ucXBarFrame1.Name = "ucXBarFrame1";
            this.ucXBarFrame1.Padding = new System.Windows.Forms.Padding(3);
            this.ucXBarFrame1.Size = new System.Drawing.Size(469, 372);
            this.ucXBarFrame1.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.ucCpkFrame1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(750, 460);
            this.xtraTabPage2.Text = "공정능력";
            // 
            // ucCpkFrame1
            // 
            this.ucCpkFrame1.Appearance.BackColor = System.Drawing.Color.White;
            this.ucCpkFrame1.Appearance.Options.UseBackColor = true;
            this.ucCpkFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCpkFrame1.Location = new System.Drawing.Point(0, 0);
            this.ucCpkFrame1.Name = "ucCpkFrame1";
            this.ucCpkFrame1.Padding = new System.Windows.Forms.Padding(3);
            this.ucCpkFrame1.Size = new System.Drawing.Size(750, 460);
            this.ucCpkFrame1.TabIndex = 0;
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
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.btnChartAnalysisRawData);
            this.xtraTabPage3.Controls.Add(this.grdRawData);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(469, 372);
            this.xtraTabPage3.Text = "Raw Data";
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
            this.btnChartAnalysisRawData.TabIndex = 2;
            this.btnChartAnalysisRawData.Text = "분석 Raw Data";
            this.btnChartAnalysisRawData.TooltipLanguageKey = "";
            this.btnChartAnalysisRawData.Click += new System.EventHandler(this.btnChartAnalysisRawData_Click);
            // 
            // grdRawData
            // 
            this.grdRawData.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawData.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdRawData.IsUsePaging = false;
            this.grdRawData.LanguageKey = "SPCRAWDATA";
            this.grdRawData.Location = new System.Drawing.Point(0, 0);
            this.grdRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawData.Name = "grdRawData";
            this.grdRawData.Padding = new System.Windows.Forms.Padding(3);
            this.grdRawData.ShowBorder = true;
            this.grdRawData.ShowStatusBar = false;
            this.grdRawData.Size = new System.Drawing.Size(469, 372);
            this.grdRawData.TabIndex = 0;
            this.grdRawData.UseAutoBestFitColumns = false;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.grdOverRules);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(750, 460);
            this.xtraTabPage4.Text = "Over Rules";
            // 
            // grdOverRules
            // 
            this.grdOverRules.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdOverRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOverRules.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOverRules.IsUsePaging = false;
            this.grdOverRules.LanguageKey = "SPCOVERRAWDATA";
            this.grdOverRules.Location = new System.Drawing.Point(0, 0);
            this.grdOverRules.Margin = new System.Windows.Forms.Padding(0);
            this.grdOverRules.Name = "grdOverRules";
            this.grdOverRules.Padding = new System.Windows.Forms.Padding(3);
            this.grdOverRules.ShowBorder = true;
            this.grdOverRules.ShowStatusBar = false;
            this.grdOverRules.Size = new System.Drawing.Size(750, 460);
            this.grdOverRules.TabIndex = 1;
            this.grdOverRules.UseAutoBestFitColumns = false;
            // 
            // ReliaVerifiSpcStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ReliaVerifiSpcStatus";
            this.Text = "SmartConditionBaseForm";
            this.Load += new System.EventHandler(this.ReliaVerifiSpcStatus_Load);
            this.Resize += new System.EventHandler(this.ReliaVerifiSpcStatus_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.tabAnalysis.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private UserControl.ucXBarFrame ucXBarFrame1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private UserControl.ucCpkFrame ucCpkFrame1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private Framework.SmartControls.SmartBandedGrid grdRawData;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private Framework.SmartControls.SmartBandedGrid grdOverRules;
        private DevExpress.XtraTab.XtraTabPage tabAnalysis;
        private UserControl.ucAnalysisPlot ucAnalysisPlot1;
        private Framework.SmartControls.SmartButton btnChartAnalysisRawData;
    }
}