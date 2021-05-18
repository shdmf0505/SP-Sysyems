namespace Micube.SmartMES.ToolManagement
{
    partial class BrowseToolStatus
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
            this.grdToolStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabRequestToolInfo = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabPageToolHistory = new DevExpress.XtraTab.XtraTabPage();
            this.grdToolHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabPageToolUseStatus = new DevExpress.XtraTab.XtraTabPage();
            this.grdToolUseStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabRequestToolInfo)).BeginInit();
            this.tabRequestToolInfo.SuspendLayout();
            this.tabPageToolHistory.SuspendLayout();
            this.tabPageToolUseStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 634);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1067, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(1067, 638);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1372, 667);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdToolStatus);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.tabRequestToolInfo);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1067, 638);
            this.smartSpliterContainer2.SplitterPosition = 244;
            this.smartSpliterContainer2.TabIndex = 1;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdToolStatus
            // 
            this.grdToolStatus.Caption = "TOOLSTATUSLIST";
            this.grdToolStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolStatus.IsUsePaging = false;
            this.grdToolStatus.LanguageKey = "TOOLSTATUSLIST";
            this.grdToolStatus.Location = new System.Drawing.Point(0, 0);
            this.grdToolStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolStatus.Name = "grdToolStatus";
            this.grdToolStatus.ShowBorder = true;
            this.grdToolStatus.ShowStatusBar = false;
            this.grdToolStatus.Size = new System.Drawing.Size(1067, 389);
            this.grdToolStatus.TabIndex = 115;
            // 
            // tabRequestToolInfo
            // 
            this.tabRequestToolInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRequestToolInfo.Location = new System.Drawing.Point(0, 0);
            this.tabRequestToolInfo.Name = "tabRequestToolInfo";
            this.tabRequestToolInfo.SelectedTabPage = this.tabPageToolHistory;
            this.tabRequestToolInfo.Size = new System.Drawing.Size(1067, 244);
            this.tabRequestToolInfo.TabIndex = 0;
            this.tabRequestToolInfo.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageToolHistory,
            this.tabPageToolUseStatus});
            // 
            // tabPageToolHistory
            // 
            this.tabPageToolHistory.Controls.Add(this.grdToolHistory);
            this.tabRequestToolInfo.SetLanguageKey(this.tabPageToolHistory, "TOOLHISTORY");
            this.tabPageToolHistory.Name = "tabPageToolHistory";
            this.tabPageToolHistory.Size = new System.Drawing.Size(1061, 215);
            this.tabPageToolHistory.Text = "이력:";
            // 
            // grdToolHistory
            // 
            this.grdToolHistory.Caption = "";
            this.grdToolHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolHistory.IsUsePaging = false;
            this.grdToolHistory.LanguageKey = "";
            this.grdToolHistory.Location = new System.Drawing.Point(0, 0);
            this.grdToolHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolHistory.Name = "grdToolHistory";
            this.grdToolHistory.ShowBorder = true;
            this.grdToolHistory.ShowStatusBar = false;
            this.grdToolHistory.Size = new System.Drawing.Size(1061, 215);
            this.grdToolHistory.TabIndex = 114;
            // 
            // tabPageToolUseStatus
            // 
            this.tabPageToolUseStatus.Controls.Add(this.grdToolUseStatus);
            this.tabRequestToolInfo.SetLanguageKey(this.tabPageToolUseStatus, "TOOLUSESTATUS");
            this.tabPageToolUseStatus.Name = "tabPageToolUseStatus";
            this.tabPageToolUseStatus.Size = new System.Drawing.Size(469, 215);
            this.tabPageToolUseStatus.Text = "사용내역:";
            // 
            // grdToolUseStatus
            // 
            this.grdToolUseStatus.Caption = "";
            this.grdToolUseStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdToolUseStatus.IsUsePaging = false;
            this.grdToolUseStatus.LanguageKey = "";
            this.grdToolUseStatus.Location = new System.Drawing.Point(0, 0);
            this.grdToolUseStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdToolUseStatus.Name = "grdToolUseStatus";
            this.grdToolUseStatus.ShowBorder = true;
            this.grdToolUseStatus.ShowStatusBar = false;
            this.grdToolUseStatus.Size = new System.Drawing.Size(469, 215);
            this.grdToolUseStatus.TabIndex = 115;
            // 
            // BrowseToolStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 687);
            this.Name = "BrowseToolStatus";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabRequestToolInfo)).EndInit();
            this.tabRequestToolInfo.ResumeLayout(false);
            this.tabPageToolHistory.ResumeLayout(false);
            this.tabPageToolUseStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdToolStatus;
        private Framework.SmartControls.SmartTabControl tabRequestToolInfo;
        private DevExpress.XtraTab.XtraTabPage tabPageToolHistory;
        private Framework.SmartControls.SmartBandedGrid grdToolHistory;
        private DevExpress.XtraTab.XtraTabPage tabPageToolUseStatus;
        private Framework.SmartControls.SmartBandedGrid grdToolUseStatus;
    }
}