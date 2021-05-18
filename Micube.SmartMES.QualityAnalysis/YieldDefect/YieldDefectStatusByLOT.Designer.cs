namespace Micube.SmartMES.QualityAnalysis
{
    partial class YieldDefectStatusByLOT
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
            this.tapLOTYieldRate = new DevExpress.XtraTab.XtraTabPage();
            this.gridLOTYieldRate = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapDefectDetail = new DevExpress.XtraTab.XtraTabPage();
            this.gridDefectDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tapLOTYieldRate.SuspendLayout();
            this.tapDefectDetail.SuspendLayout();
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
            this.smartTabControl1.SelectedTabPage = this.tapLOTYieldRate;
            this.smartTabControl1.Size = new System.Drawing.Size(814, 613);
            this.smartTabControl1.TabIndex = 0;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapLOTYieldRate,
            this.tapDefectDetail});
            // 
            // tapLOTYieldRate
            // 
            this.tapLOTYieldRate.Controls.Add(this.gridLOTYieldRate);
            this.smartTabControl1.SetLanguageKey(this.tapLOTYieldRate, "LOTYIELDRATE");
            this.tapLOTYieldRate.Name = "tapLOTYieldRate";
            this.tapLOTYieldRate.Size = new System.Drawing.Size(808, 584);
            this.tapLOTYieldRate.Text = "LOT별 수율";
            // 
            // gridLOTYieldRate
            // 
            this.gridLOTYieldRate.Caption = "";
            this.gridLOTYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLOTYieldRate.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridLOTYieldRate.IsUsePaging = false;
            this.gridLOTYieldRate.LanguageKey = "LOTYIELDRATE";
            this.gridLOTYieldRate.Location = new System.Drawing.Point(0, 0);
            this.gridLOTYieldRate.Margin = new System.Windows.Forms.Padding(0);
            this.gridLOTYieldRate.Name = "gridLOTYieldRate";
            this.gridLOTYieldRate.ShowBorder = true;
            this.gridLOTYieldRate.ShowStatusBar = false;
            this.gridLOTYieldRate.Size = new System.Drawing.Size(808, 584);
            this.gridLOTYieldRate.TabIndex = 0;
            this.gridLOTYieldRate.UseAutoBestFitColumns = false;
            // 
            // tapDefectDetail
            // 
            this.tapDefectDetail.Controls.Add(this.gridDefectDetail);
            this.smartTabControl1.SetLanguageKey(this.tapDefectDetail, "DEFECTDETAIL");
            this.tapDefectDetail.Name = "tapDefectDetail";
            this.tapDefectDetail.Size = new System.Drawing.Size(808, 584);
            this.tapDefectDetail.Text = "불량세부";
            // 
            // gridDefectDetail
            // 
            this.gridDefectDetail.Caption = "";
            this.gridDefectDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDefectDetail.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridDefectDetail.IsUsePaging = false;
            this.gridDefectDetail.LanguageKey = "DEFECTDETAIL";
            this.gridDefectDetail.Location = new System.Drawing.Point(0, 0);
            this.gridDefectDetail.Margin = new System.Windows.Forms.Padding(0);
            this.gridDefectDetail.Name = "gridDefectDetail";
            this.gridDefectDetail.ShowBorder = true;
            this.gridDefectDetail.ShowStatusBar = false;
            this.gridDefectDetail.Size = new System.Drawing.Size(808, 584);
            this.gridDefectDetail.TabIndex = 0;
            this.gridDefectDetail.UseAutoBestFitColumns = false;
            // 
            // YieldDefectStatusByLOT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Name = "YieldDefectStatusByLOT";
            this.Text = "Yield & Defect Status by LOT";
            this.Load += new System.EventHandler(this.LoadForm);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tapLOTYieldRate.ResumeLayout(false);
            this.tapDefectDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tapLOTYieldRate;
        private DevExpress.XtraTab.XtraTabPage tapDefectDetail;
        private Framework.SmartControls.SmartBandedGrid gridLOTYieldRate;
        private Framework.SmartControls.SmartBandedGrid gridDefectDetail;
    }
}