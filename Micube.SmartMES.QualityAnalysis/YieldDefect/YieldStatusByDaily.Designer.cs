namespace Micube.SmartMES.QualityAnalysis
{
    partial class YieldStatusByDaily
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
            this.tapPageYieldRateDaily = new DevExpress.XtraTab.XtraTabPage();
            this.gridYieldRateDaily = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapPageYieldRateDailyWorst = new DevExpress.XtraTab.XtraTabPage();
            this.gridYieldRateWorst = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapPageYieldRateByType = new DevExpress.XtraTab.XtraTabPage();
            this.gridYieldRateTypePivot = new Micube.Framework.SmartControls.SmartPivotGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tapPageYieldRateDaily.SuspendLayout();
            this.tapPageYieldRateDailyWorst.SuspendLayout();
            this.tapPageYieldRateByType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridYieldRateTypePivot)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 609);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(814, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartTabControl1);
            this.pnlContent.Size = new System.Drawing.Size(814, 613);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1119, 642);
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTabControl1.Location = new System.Drawing.Point(0, 0);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.tapPageYieldRateDaily;
            this.smartTabControl1.Size = new System.Drawing.Size(814, 613);
            this.smartTabControl1.TabIndex = 0;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapPageYieldRateDaily,
            this.tapPageYieldRateDailyWorst,
            this.tapPageYieldRateByType});
            // 
            // tapPageYieldRateDaily
            // 
            this.tapPageYieldRateDaily.Controls.Add(this.gridYieldRateDaily);
            this.smartTabControl1.SetLanguageKey(this.tapPageYieldRateDaily, "DAILYYIELD");
            this.tapPageYieldRateDaily.Name = "tapPageYieldRateDaily";
            this.tapPageYieldRateDaily.Size = new System.Drawing.Size(808, 584);
            this.tapPageYieldRateDaily.Text = "일별 수율";
            // 
            // gridYieldRateDaily
            // 
            this.gridYieldRateDaily.Caption = "";
            this.gridYieldRateDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridYieldRateDaily.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridYieldRateDaily.IsUsePaging = false;
            this.gridYieldRateDaily.LanguageKey = "DAILYYIELD";
            this.gridYieldRateDaily.Location = new System.Drawing.Point(0, 0);
            this.gridYieldRateDaily.Margin = new System.Windows.Forms.Padding(0);
            this.gridYieldRateDaily.Name = "gridYieldRateDaily";
            this.gridYieldRateDaily.ShowBorder = true;
            this.gridYieldRateDaily.ShowStatusBar = false;
            this.gridYieldRateDaily.Size = new System.Drawing.Size(808, 584);
            this.gridYieldRateDaily.TabIndex = 0;
            this.gridYieldRateDaily.UseAutoBestFitColumns = false;
            // 
            // tapPageYieldRateDailyWorst
            // 
            this.tapPageYieldRateDailyWorst.Controls.Add(this.gridYieldRateWorst);
            this.smartTabControl1.SetLanguageKey(this.tapPageYieldRateDailyWorst, "DAILYYIELDWORST");
            this.tapPageYieldRateDailyWorst.Name = "tapPageYieldRateDailyWorst";
            this.tapPageYieldRateDailyWorst.Size = new System.Drawing.Size(750, 460);
            this.tapPageYieldRateDailyWorst.Text = "일별 Worst";
            // 
            // gridYieldRateWorst
            // 
            this.gridYieldRateWorst.Caption = "";
            this.gridYieldRateWorst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridYieldRateWorst.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridYieldRateWorst.IsUsePaging = false;
            this.gridYieldRateWorst.LanguageKey = "DAILYYIELDWORST";
            this.gridYieldRateWorst.Location = new System.Drawing.Point(0, 0);
            this.gridYieldRateWorst.Margin = new System.Windows.Forms.Padding(0);
            this.gridYieldRateWorst.Name = "gridYieldRateWorst";
            this.gridYieldRateWorst.ShowBorder = true;
            this.gridYieldRateWorst.ShowStatusBar = false;
            this.gridYieldRateWorst.Size = new System.Drawing.Size(750, 460);
            this.gridYieldRateWorst.TabIndex = 0;
            this.gridYieldRateWorst.UseAutoBestFitColumns = false;
            // 
            // tapPageYieldRateByType
            // 
            this.tapPageYieldRateByType.Controls.Add(this.gridYieldRateTypePivot);
            this.smartTabControl1.SetLanguageKey(this.tapPageYieldRateByType, "DAILYYIELDPIVOTBYTYPE");
            this.tapPageYieldRateByType.Name = "tapPageYieldRateByType";
            this.tapPageYieldRateByType.Size = new System.Drawing.Size(750, 460);
            this.tapPageYieldRateByType.Text = "타입별(PIVOT)";
            // 
            // gridYieldRateTypePivot
            // 
            this.gridYieldRateTypePivot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridYieldRateTypePivot.GrandTotalCaptionText = null;
            this.gridYieldRateTypePivot.Location = new System.Drawing.Point(0, 0);
            this.gridYieldRateTypePivot.Name = "gridYieldRateTypePivot";
            this.gridYieldRateTypePivot.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.gridYieldRateTypePivot.OptionsView.ShowTotalsForSingleValues = true;
            this.gridYieldRateTypePivot.Size = new System.Drawing.Size(750, 460);
            this.gridYieldRateTypePivot.TabIndex = 0;
            this.gridYieldRateTypePivot.TotalFieldNames = null;
            this.gridYieldRateTypePivot.UseCheckBoxField = false;
            this.gridYieldRateTypePivot.UseGrandTotalCaption = false;
            // 
            // YieldStatusByDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Name = "YieldStatusByDaily";
            this.Text = "Yield & Defect Status by Product Item";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tapPageYieldRateDaily.ResumeLayout(false);
            this.tapPageYieldRateDailyWorst.ResumeLayout(false);
            this.tapPageYieldRateByType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridYieldRateTypePivot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tapPageYieldRateDaily;
        private DevExpress.XtraTab.XtraTabPage tapPageYieldRateDailyWorst;
        private Framework.SmartControls.SmartBandedGrid gridYieldRateDaily;
        private Framework.SmartControls.SmartBandedGrid gridYieldRateWorst;
        private DevExpress.XtraTab.XtraTabPage tapPageYieldRateByType;
        private Framework.SmartControls.SmartPivotGridControl gridYieldRateTypePivot;
    }
}