namespace Micube.SmartMES.QualityAnalysis
{
    partial class YieldDefectStatusByProductItem
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
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapPageItemYieldRate = new DevExpress.XtraTab.XtraTabPage();
            this.gridYieldRate = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapPageItemDefectStatus = new DevExpress.XtraTab.XtraTabPage();
            this.gridDefectStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapPageItemDefectDetail = new DevExpress.XtraTab.XtraTabPage();
            this.gridDefectDetail = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tapPageItemYieldRate.SuspendLayout();
            this.tapPageItemDefectStatus.SuspendLayout();
            this.tapPageItemDefectDetail.SuspendLayout();
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
            this.smartTabControl1.SelectedTabPage = this.tapPageItemYieldRate;
            this.smartTabControl1.Size = new System.Drawing.Size(814, 613);
            this.smartTabControl1.TabIndex = 0;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapPageItemYieldRate,
            this.tapPageItemDefectStatus,
            this.tapPageItemDefectDetail});
            // 
            // tapPageItemYieldRate
            // 
            this.tapPageItemYieldRate.Controls.Add(this.gridYieldRate);
            this.smartTabControl1.SetLanguageKey(this.tapPageItemYieldRate, "ITEMYIELDRATE");
            this.tapPageItemYieldRate.Name = "tapPageItemYieldRate";
            this.tapPageItemYieldRate.Padding = new System.Windows.Forms.Padding(3);
            this.tapPageItemYieldRate.Size = new System.Drawing.Size(808, 584);
            this.tapPageItemYieldRate.Text = "품목별 수율";
            // 
            // gridYieldRate
            // 
            this.gridYieldRate.Caption = "";
            this.gridYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridYieldRate.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridYieldRate.IsUsePaging = false;
            this.gridYieldRate.LanguageKey = "ITEMYIELDRATE";
            this.gridYieldRate.Location = new System.Drawing.Point(3, 3);
            this.gridYieldRate.Margin = new System.Windows.Forms.Padding(0);
            this.gridYieldRate.Name = "gridYieldRate";
            this.gridYieldRate.ShowBorder = true;
            this.gridYieldRate.Size = new System.Drawing.Size(802, 578);
            this.gridYieldRate.TabIndex = 0;
            this.gridYieldRate.UseAutoBestFitColumns = false;
            // 
            // tapPageItemDefectStatus
            // 
            this.tapPageItemDefectStatus.Controls.Add(this.gridDefectStatus);
            this.smartTabControl1.SetLanguageKey(this.tapPageItemDefectStatus, "ITEMDEFECTSTATUS");
            this.tapPageItemDefectStatus.Name = "tapPageItemDefectStatus";
            this.tapPageItemDefectStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tapPageItemDefectStatus.Size = new System.Drawing.Size(750, 460);
            this.tapPageItemDefectStatus.Text = "품목별 불량현황";
            // 
            // gridDefectStatus
            // 
            this.gridDefectStatus.Caption = "";
            this.gridDefectStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDefectStatus.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridDefectStatus.IsUsePaging = false;
            this.gridDefectStatus.LanguageKey = "ITEMDEFECTSTATUS";
            this.gridDefectStatus.Location = new System.Drawing.Point(3, 3);
            this.gridDefectStatus.Margin = new System.Windows.Forms.Padding(0);
            this.gridDefectStatus.Name = "gridDefectStatus";
            this.gridDefectStatus.ShowBorder = true;
            this.gridDefectStatus.ShowStatusBar = false;
            this.gridDefectStatus.Size = new System.Drawing.Size(744, 454);
            this.gridDefectStatus.TabIndex = 0;
            this.gridDefectStatus.UseAutoBestFitColumns = false;
            // 
            // tapPageItemDefectDetail
            // 
            this.tapPageItemDefectDetail.Controls.Add(this.gridDefectDetail);
            this.smartTabControl1.SetLanguageKey(this.tapPageItemDefectDetail, "ITEMDEFECTDETAIL");
            this.tapPageItemDefectDetail.Name = "tapPageItemDefectDetail";
            this.tapPageItemDefectDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tapPageItemDefectDetail.Size = new System.Drawing.Size(750, 460);
            this.tapPageItemDefectDetail.Text = "품목별 불량세부";
            // 
            // gridDefectDetail
            // 
            this.gridDefectDetail.Caption = "";
            this.gridDefectDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDefectDetail.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridDefectDetail.IsUsePaging = false;
            this.gridDefectDetail.LanguageKey = "ITEMDEFECTDETAIL";
            this.gridDefectDetail.Location = new System.Drawing.Point(3, 3);
            this.gridDefectDetail.Margin = new System.Windows.Forms.Padding(0);
            this.gridDefectDetail.Name = "gridDefectDetail";
            this.gridDefectDetail.ShowBorder = true;
            this.gridDefectDetail.ShowStatusBar = false;
            this.gridDefectDetail.Size = new System.Drawing.Size(744, 454);
            this.gridDefectDetail.TabIndex = 1;
            this.gridDefectDetail.UseAutoBestFitColumns = false;
            // 
            // YieldDefectStatusByProductItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Name = "YieldDefectStatusByProductItem";
            this.Text = "Yield & Defect Status by Product Item";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tapPageItemYieldRate.ResumeLayout(false);
            this.tapPageItemDefectStatus.ResumeLayout(false);
            this.tapPageItemDefectDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tapPageItemYieldRate;
        private DevExpress.XtraTab.XtraTabPage tapPageItemDefectStatus;
        private Framework.SmartControls.SmartBandedGrid gridYieldRate;
        private Framework.SmartControls.SmartBandedGrid gridDefectStatus;
        private DevExpress.XtraTab.XtraTabPage tapPageItemDefectDetail;
        private Framework.SmartControls.SmartBandedGrid gridDefectDetail;
    }
}