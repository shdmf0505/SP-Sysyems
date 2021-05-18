namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingPublishStatusBrowse
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
            this.tcMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtpDetailInfo = new DevExpress.XtraTab.XtraTabPage();
            this.grdDetailInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtpVendorInfo = new DevExpress.XtraTab.XtraTabPage();
            this.grdVendorInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).BeginInit();
            this.tcMain.SuspendLayout();
            this.xtpDetailInfo.SuspendLayout();
            this.xtpVendorInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 527);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(655, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tcMain);
            this.pnlContent.Size = new System.Drawing.Size(655, 530);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1036, 566);
            // 
            // tcMain
            // 
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedTabPage = this.xtpDetailInfo;
            this.tcMain.Size = new System.Drawing.Size(655, 530);
            this.tcMain.TabIndex = 0;
            this.tcMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpDetailInfo,
            this.xtpVendorInfo});
            // 
            // xtpDetailInfo
            // 
            this.xtpDetailInfo.Controls.Add(this.grdDetailInfo);
            this.tcMain.SetLanguageKey(this.xtpDetailInfo, "OSPPublishStatusDetailInfo");
            this.xtpDetailInfo.Name = "xtpDetailInfo";
            this.xtpDetailInfo.Size = new System.Drawing.Size(648, 494);
            this.xtpDetailInfo.Text = "OSPPublishStatusDetailInfo";
            // 
            // grdDetailInfo
            // 
            this.grdDetailInfo.Caption = "";
            this.grdDetailInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetailInfo.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDetailInfo.IsUsePaging = false;
            this.grdDetailInfo.LanguageKey = "";
            this.grdDetailInfo.Location = new System.Drawing.Point(0, 0);
            this.grdDetailInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdDetailInfo.Name = "grdDetailInfo";
            this.grdDetailInfo.ShowBorder = true;
            this.grdDetailInfo.ShowStatusBar = false;
            this.grdDetailInfo.Size = new System.Drawing.Size(648, 494);
            this.grdDetailInfo.TabIndex = 118;
            this.grdDetailInfo.UseAutoBestFitColumns = false;
            // 
            // xtpVendorInfo
            // 
            this.xtpVendorInfo.Controls.Add(this.grdVendorInfo);
            this.tcMain.SetLanguageKey(this.xtpVendorInfo, "OSPPublishStatusVendorInfo");
            this.xtpVendorInfo.Name = "xtpVendorInfo";
            this.xtpVendorInfo.Size = new System.Drawing.Size(648, 494);
            this.xtpVendorInfo.Text = "OSPPublishStatusVendorInfo";
            // 
            // grdVendorInfo
            // 
            this.grdVendorInfo.Caption = "";
            this.grdVendorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVendorInfo.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdVendorInfo.IsUsePaging = false;
            this.grdVendorInfo.LanguageKey = "";
            this.grdVendorInfo.Location = new System.Drawing.Point(0, 0);
            this.grdVendorInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdVendorInfo.Name = "grdVendorInfo";
            this.grdVendorInfo.ShowBorder = true;
            this.grdVendorInfo.ShowStatusBar = false;
            this.grdVendorInfo.Size = new System.Drawing.Size(648, 494);
            this.grdVendorInfo.TabIndex = 118;
            this.grdVendorInfo.UseAutoBestFitColumns = false;
            // 
            // OutsourcingPublishStatusBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 604);
            this.Name = "OutsourcingPublishStatusBrowse";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.xtpDetailInfo.ResumeLayout(false);
            this.xtpVendorInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tcMain;
        private DevExpress.XtraTab.XtraTabPage xtpDetailInfo;
        private DevExpress.XtraTab.XtraTabPage xtpVendorInfo;
        private Framework.SmartControls.SmartBandedGrid grdDetailInfo;
        private Framework.SmartControls.SmartBandedGrid grdVendorInfo;
    }
}