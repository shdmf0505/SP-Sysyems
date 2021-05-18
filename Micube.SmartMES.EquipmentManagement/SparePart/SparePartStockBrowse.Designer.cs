namespace Micube.SmartMES.EquipmentManagement
{
    partial class SparePartStockBrowse
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
            this.grdSPStock = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInOutHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.speImage = new Micube.Framework.SmartControls.SmartPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speImage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 690);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1144, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSpliterContainer2);
            this.pnlContent.Size = new System.Drawing.Size(1144, 694);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1449, 723);
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdSPStock);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.smartSpliterContainer1);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1144, 694);
            this.smartSpliterContainer2.SplitterPosition = 244;
            this.smartSpliterContainer2.TabIndex = 3;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdSPStock
            // 
            this.grdSPStock.Caption = "EquipmentSPStockStatus";
            this.grdSPStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSPStock.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSPStock.IsUsePaging = false;
            this.grdSPStock.LanguageKey = "EquipmentSPStockStatus";
            this.grdSPStock.Location = new System.Drawing.Point(0, 0);
            this.grdSPStock.Margin = new System.Windows.Forms.Padding(0);
            this.grdSPStock.Name = "grdSPStock";
            this.grdSPStock.ShowBorder = true;
            this.grdSPStock.ShowStatusBar = false;
            this.grdSPStock.Size = new System.Drawing.Size(1144, 445);
            this.grdSPStock.TabIndex = 115;
            // 
            // grdInOutHistory
            // 
            this.grdInOutHistory.Caption = "";
            this.grdInOutHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInOutHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInOutHistory.IsUsePaging = false;
            this.grdInOutHistory.LanguageKey = "FILMUSESTATUS";
            this.grdInOutHistory.Location = new System.Drawing.Point(0, 0);
            this.grdInOutHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdInOutHistory.Name = "grdInOutHistory";
            this.grdInOutHistory.ShowBorder = true;
            this.grdInOutHistory.ShowStatusBar = false;
            this.grdInOutHistory.Size = new System.Drawing.Size(857, 244);
            this.grdInOutHistory.TabIndex = 115;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdInOutHistory);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.speImage);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1144, 244);
            this.smartSpliterContainer1.SplitterPosition = 282;
            this.smartSpliterContainer1.TabIndex = 116;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // speImage
            // 
            this.speImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speImage.Location = new System.Drawing.Point(0, 0);
            this.speImage.Name = "speImage";
            this.speImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.speImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.speImage.Size = new System.Drawing.Size(282, 244);
            this.speImage.TabIndex = 7;
            // 
            // SparePartStockBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 743);
            this.Name = "SparePartStockBrowse";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.speImage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdSPStock;
        private Framework.SmartControls.SmartBandedGrid grdInOutHistory;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartPictureEdit speImage;
    }
}