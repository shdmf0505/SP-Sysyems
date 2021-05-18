namespace Micube.SmartMES.EquipmentManagement
{
    partial class BrowseMaintWorkOrderResult
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
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdMaintWorkOrderStatus = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
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
            this.pnlCondition.Size = new System.Drawing.Size(296, 598);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(894, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(894, 602);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1199, 631);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdMaintWorkOrderStatus);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdHistory);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(894, 602);
            this.smartSpliterContainer2.SplitterPosition = 244;
            this.smartSpliterContainer2.TabIndex = 3;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdMaintWorkOrderStatus
            // 
            this.grdMaintWorkOrderStatus.Caption = "";
            this.grdMaintWorkOrderStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaintWorkOrderStatus.IsUsePaging = false;
            this.grdMaintWorkOrderStatus.LanguageKey = "MAINTWORKORDERRESULT";
            this.grdMaintWorkOrderStatus.Location = new System.Drawing.Point(0, 0);
            this.grdMaintWorkOrderStatus.Margin = new System.Windows.Forms.Padding(0);
            this.grdMaintWorkOrderStatus.Name = "grdMaintWorkOrderStatus";
            this.grdMaintWorkOrderStatus.ShowBorder = true;
            this.grdMaintWorkOrderStatus.ShowStatusBar = false;
            this.grdMaintWorkOrderStatus.Size = new System.Drawing.Size(894, 353);
            this.grdMaintWorkOrderStatus.TabIndex = 115;
            // 
            // grdHistory
            // 
            this.grdHistory.Caption = "";
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.IsUsePaging = false;
            this.grdHistory.LanguageKey = "SPAREPARTMWOSTATUS";
            this.grdHistory.Location = new System.Drawing.Point(0, 0);
            this.grdHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.ShowBorder = true;
            this.grdHistory.ShowStatusBar = false;
            this.grdHistory.Size = new System.Drawing.Size(894, 244);
            this.grdHistory.TabIndex = 115;
            // 
            // BrowseMaintWorkOrderResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 651);
            this.Name = "BrowseMaintWorkOrderResult";
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
        private Framework.SmartControls.SmartBandedGrid grdMaintWorkOrderStatus;
        private Framework.SmartControls.SmartBandedGrid grdHistory;
    }
}