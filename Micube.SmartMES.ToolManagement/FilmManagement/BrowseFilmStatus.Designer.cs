namespace Micube.SmartMES.ToolManagement
{
    partial class BrowseFilmStatus
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
            this.grdFilmStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdFilmHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 674);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1127, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(1127, 678);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1432, 707);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdFilmStatus);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdFilmHistory);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1127, 678);
            this.smartSpliterContainer2.SplitterPosition = 244;
            this.smartSpliterContainer2.TabIndex = 2;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdFilmStatus
            // 
            this.grdFilmStatus.Caption = "";
            this.grdFilmStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilmStatus.IsUsePaging = false;
            this.grdFilmStatus.LanguageKey = "FILMSTATUSHISTORYBROWSE";
            this.grdFilmStatus.Location = new System.Drawing.Point(0, 0);
            this.grdFilmStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilmStatus.Name = "grdFilmStatus";
            this.grdFilmStatus.ShowBorder = true;
            this.grdFilmStatus.ShowStatusBar = false;
            this.grdFilmStatus.Size = new System.Drawing.Size(1127, 429);
            this.grdFilmStatus.TabIndex = 115;
            // 
            // grdFilmHistory
            // 
            this.grdFilmHistory.Caption = "";
            this.grdFilmHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilmHistory.IsUsePaging = false;
            this.grdFilmHistory.LanguageKey = "FILMUSESTATUS";
            this.grdFilmHistory.Location = new System.Drawing.Point(0, 0);
            this.grdFilmHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdFilmHistory.Name = "grdFilmHistory";
            this.grdFilmHistory.ShowBorder = true;
            this.grdFilmHistory.ShowStatusBar = false;
            this.grdFilmHistory.Size = new System.Drawing.Size(1127, 244);
            this.grdFilmHistory.TabIndex = 115;
            // 
            // BrowseFilmStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 727);
            this.Name = "BrowseFilmStatus";
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
        private Framework.SmartControls.SmartBandedGrid grdFilmStatus;
        private Framework.SmartControls.SmartBandedGrid grdFilmHistory;
    }
}