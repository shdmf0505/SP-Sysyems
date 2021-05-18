namespace Micube.SmartMES.QualityAnalysis
{
    partial class NonAdjRatio
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
            this.pnlGridMain = new Micube.Framework.SmartControls.SmartPanel();
            this.smartTabControl1 = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapItemNonAdjusted = new DevExpress.XtraTab.XtraTabPage();
            this.gridItemNonAdjust = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapProcessNonAdjusted = new DevExpress.XtraTab.XtraTabPage();
            this.gridItemProcessNonAdjust = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).BeginInit();
            this.pnlGridMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).BeginInit();
            this.smartTabControl1.SuspendLayout();
            this.tapItemNonAdjusted.SuspendLayout();
            this.tapProcessNonAdjusted.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 593);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(728, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.pnlGridMain);
            this.pnlContent.Size = new System.Drawing.Size(728, 596);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1109, 632);
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.smartTabControl1);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 0);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(728, 596);
            this.pnlGridMain.TabIndex = 5;
            // 
            // smartTabControl1
            // 
            this.smartTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartTabControl1.Location = new System.Drawing.Point(2, 2);
            this.smartTabControl1.Name = "smartTabControl1";
            this.smartTabControl1.SelectedTabPage = this.tapItemNonAdjusted;
            this.smartTabControl1.Size = new System.Drawing.Size(724, 592);
            this.smartTabControl1.TabIndex = 1;
            this.smartTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapItemNonAdjusted,
            this.tapProcessNonAdjusted});
            // 
            // tapItemNonAdjusted
            // 
            this.tapItemNonAdjusted.Controls.Add(this.gridItemNonAdjust);
            this.tapItemNonAdjusted.Name = "tapItemNonAdjusted";
            this.tapItemNonAdjusted.Size = new System.Drawing.Size(717, 556);
            this.tapItemNonAdjusted.Text = "품목";
            // 
            // gridItemNonAdjust
            // 
            this.gridItemNonAdjust.Caption = "";
            this.gridItemNonAdjust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItemNonAdjust.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridItemNonAdjust.IsUsePaging = false;
            this.gridItemNonAdjust.LanguageKey = "";
            this.gridItemNonAdjust.Location = new System.Drawing.Point(0, 0);
            this.gridItemNonAdjust.Margin = new System.Windows.Forms.Padding(0);
            this.gridItemNonAdjust.Name = "gridItemNonAdjust";
            this.gridItemNonAdjust.ShowBorder = true;
            this.gridItemNonAdjust.Size = new System.Drawing.Size(717, 556);
            this.gridItemNonAdjust.TabIndex = 0;
            this.gridItemNonAdjust.UseAutoBestFitColumns = false;
            // 
            // tapProcessNonAdjusted
            // 
            this.tapProcessNonAdjusted.Controls.Add(this.gridItemProcessNonAdjust);
            this.tapProcessNonAdjusted.Name = "tapProcessNonAdjusted";
            this.tapProcessNonAdjusted.Size = new System.Drawing.Size(717, 556);
            this.tapProcessNonAdjusted.Text = "품목공정";
            // 
            // gridItemProcessNonAdjust
            // 
            this.gridItemProcessNonAdjust.Caption = "";
            this.gridItemProcessNonAdjust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItemProcessNonAdjust.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Export;
            this.gridItemProcessNonAdjust.IsUsePaging = false;
            this.gridItemProcessNonAdjust.LanguageKey = "";
            this.gridItemProcessNonAdjust.Location = new System.Drawing.Point(0, 0);
            this.gridItemProcessNonAdjust.Margin = new System.Windows.Forms.Padding(0);
            this.gridItemProcessNonAdjust.Name = "gridItemProcessNonAdjust";
            this.gridItemProcessNonAdjust.ShowBorder = true;
            this.gridItemProcessNonAdjust.ShowStatusBar = false;
            this.gridItemProcessNonAdjust.Size = new System.Drawing.Size(717, 556);
            this.gridItemProcessNonAdjust.TabIndex = 0;
            this.gridItemProcessNonAdjust.UseAutoBestFitColumns = false;
            // 
            // NonAdjRatio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 662);
            this.Name = "NonAdjRatio";
            this.Text = "Non-Adjusted Ratio";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).EndInit();
            this.pnlGridMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartTabControl1)).EndInit();
            this.smartTabControl1.ResumeLayout(false);
            this.tapItemNonAdjusted.ResumeLayout(false);
            this.tapProcessNonAdjusted.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartPanel pnlGridMain;
        private Framework.SmartControls.SmartTabControl smartTabControl1;
        private DevExpress.XtraTab.XtraTabPage tapItemNonAdjusted;
        private Framework.SmartControls.SmartBandedGrid gridItemNonAdjust;
        private DevExpress.XtraTab.XtraTabPage tapProcessNonAdjusted;
        private Framework.SmartControls.SmartBandedGrid gridItemProcessNonAdjust;
    }
}