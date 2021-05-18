namespace Micube.SmartMES.MaterialsManagement
{
    partial class MaterialStatusStatisticsBrowse
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
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdMaterialStatistics = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdMaterialDetailInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 502);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(646, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(646, 505);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1027, 541);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdMaterialStatistics);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdMaterialDetailInfo);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(646, 505);
            this.smartSpliterContainer2.SplitterPosition = 244;
            this.smartSpliterContainer2.TabIndex = 4;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdMaterialStatistics
            // 
            this.grdMaterialStatistics.Caption = "";
            this.grdMaterialStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterialStatistics.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaterialStatistics.IsUsePaging = false;
            this.grdMaterialStatistics.LanguageKey = "MATERIALSTATUSSTATISTICSLIST";
            this.grdMaterialStatistics.Location = new System.Drawing.Point(0, 0);
            this.grdMaterialStatistics.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterialStatistics.Name = "grdMaterialStatistics";
            this.grdMaterialStatistics.ShowBorder = true;
            this.grdMaterialStatistics.ShowStatusBar = false;
            this.grdMaterialStatistics.Size = new System.Drawing.Size(646, 255);
            this.grdMaterialStatistics.TabIndex = 115;
            this.grdMaterialStatistics.UseAutoBestFitColumns = false;
            // 
            // grdMaterialDetailInfo
            // 
            this.grdMaterialDetailInfo.Caption = "";
            this.grdMaterialDetailInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaterialDetailInfo.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMaterialDetailInfo.IsUsePaging = false;
            this.grdMaterialDetailInfo.LanguageKey = "MATERIALSTATUSSTATISTICSDETAIL";
            this.grdMaterialDetailInfo.Location = new System.Drawing.Point(0, 0);
            this.grdMaterialDetailInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaterialDetailInfo.Name = "grdMaterialDetailInfo";
            this.grdMaterialDetailInfo.ShowBorder = true;
            this.grdMaterialDetailInfo.ShowStatusBar = false;
            this.grdMaterialDetailInfo.Size = new System.Drawing.Size(646, 244);
            this.grdMaterialDetailInfo.TabIndex = 115;
            this.grdMaterialDetailInfo.UseAutoBestFitColumns = false;
            // 
            // MaterialStatusStatisticsBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 571);
            this.Name = "MaterialStatusStatisticsBrowse";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdMaterialStatistics;
        private Framework.SmartControls.SmartBandedGrid grdMaterialDetailInfo;
    }
}