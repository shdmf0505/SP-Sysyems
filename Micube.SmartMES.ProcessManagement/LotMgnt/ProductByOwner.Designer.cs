namespace Micube.SmartMES.ProcessManagement
{
    partial class ProductByOwner
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
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabPageStandard = new DevExpress.XtraTab.XtraTabPage();
            this.tabPageHistory = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabPageStandard.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 585);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(883, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(883, 589);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1188, 618);
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // grdMain
            // 
            this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.ShowStatusBar = false;
            this.grdMain.Size = new System.Drawing.Size(877, 560);
            this.grdMain.TabIndex = 0;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // grdHistory
            // 
            this.grdHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdHistory.IsUsePaging = false;
            this.grdHistory.LanguageKey = null;
            this.grdHistory.Location = new System.Drawing.Point(0, 0);
            this.grdHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.ShowBorder = true;
            this.grdHistory.ShowStatusBar = false;
            this.grdHistory.Size = new System.Drawing.Size(877, 560);
            this.grdHistory.TabIndex = 0;
            this.grdHistory.UseAutoBestFitColumns = false;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabPageStandard;
            this.tabMain.Size = new System.Drawing.Size(883, 589);
            this.tabMain.TabIndex = 1;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageStandard,
            this.tabPageHistory});
            // 
            // tabPageStandard
            // 
            this.tabPageStandard.Controls.Add(this.grdMain);
            this.tabPageStandard.Name = "tabPageStandard";
            this.tabPageStandard.Size = new System.Drawing.Size(877, 560);
            this.tabPageStandard.Text = "xtraTabPage1";
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.grdHistory);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Size = new System.Drawing.Size(877, 560);
            this.tabPageHistory.Text = "xtraTabPage2";
            // 
            // ProductByOwner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 638);
            this.Name = "ProductByOwner";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabPageStandard.ResumeLayout(false);
            this.tabPageHistory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartBandedGrid grdHistory;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabPageStandard;
        private DevExpress.XtraTab.XtraTabPage tabPageHistory;
    }
}