namespace Micube.SmartMES.ProcessManagement
{
    partial class ReworkHistory
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
            this.grdReworkHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabPageStandard = new DevExpress.XtraTab.XtraTabPage();
            this.grdReworkHistoryInput = new Micube.Framework.SmartControls.SmartBandedGrid();
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
            this.pnlCondition.Size = new System.Drawing.Size(296, 379);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(457, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            this.pnlContent.Size = new System.Drawing.Size(457, 383);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // grdReworkHistory
            // 
            this.grdReworkHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReworkHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReworkHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReworkHistory.IsUsePaging = false;
            this.grdReworkHistory.LanguageKey = "REWORKHISTORY";
            this.grdReworkHistory.Location = new System.Drawing.Point(0, 0);
            this.grdReworkHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdReworkHistory.Name = "grdReworkHistory";
            this.grdReworkHistory.ShowBorder = true;
            this.grdReworkHistory.Size = new System.Drawing.Size(451, 354);
            this.grdReworkHistory.TabIndex = 0;
            this.grdReworkHistory.UseAutoBestFitColumns = false;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabPageStandard;
            this.tabMain.Size = new System.Drawing.Size(457, 383);
            this.tabMain.TabIndex = 1;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageStandard,
            this.tabPageHistory});
            // 
            // tabPageStandard
            // 
            this.tabPageStandard.Controls.Add(this.grdReworkHistoryInput);
            this.tabMain.SetLanguageKey(this.tabPageStandard, "REWORKHISTORYINPUT");
            this.tabPageStandard.Name = "tabPageStandard";
            this.tabPageStandard.Size = new System.Drawing.Size(451, 354);
            this.tabPageStandard.Text = "xtraTabPage1";
            // 
            // grdReworkHistoryInput
            // 
            this.grdReworkHistoryInput.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReworkHistoryInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReworkHistoryInput.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdReworkHistoryInput.IsUsePaging = false;
            this.grdReworkHistoryInput.LanguageKey = "REWORKHISTORYINPUT";
            this.grdReworkHistoryInput.Location = new System.Drawing.Point(0, 0);
            this.grdReworkHistoryInput.Margin = new System.Windows.Forms.Padding(0);
            this.grdReworkHistoryInput.Name = "grdReworkHistoryInput";
            this.grdReworkHistoryInput.ShowBorder = true;
            this.grdReworkHistoryInput.ShowStatusBar = false;
            this.grdReworkHistoryInput.Size = new System.Drawing.Size(451, 354);
            this.grdReworkHistoryInput.TabIndex = 0;
            this.grdReworkHistoryInput.UseAutoBestFitColumns = false;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.grdReworkHistory);
            this.tabMain.SetLanguageKey(this.tabPageHistory, "REWORKHISTORY");
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Size = new System.Drawing.Size(451, 354);
            this.tabPageHistory.Text = "xtraTabPage2";
            // 
            // ReworkHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ReworkHistory";
            this.Padding = new System.Windows.Forms.Padding(19);
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

        private Framework.SmartControls.SmartBandedGrid grdReworkHistory;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabPageStandard;
        private DevExpress.XtraTab.XtraTabPage tabPageHistory;
        private Framework.SmartControls.SmartBandedGrid grdReworkHistoryInput;
    }
}