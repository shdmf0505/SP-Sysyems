namespace Micube.SmartMES.OutsideOrderMgnt
{
    partial class OutsourcingYoungPongProcessingCosts
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tapProcessCosts = new Micube.Framework.SmartControls.SmartTabControl();
            this.tapOspProcProduct = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspProcProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tapOspProcLayer = new DevExpress.XtraTab.XtraTabPage();
            this.grdOspProcLayer = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tapProcessCosts)).BeginInit();
            this.tapProcessCosts.SuspendLayout();
            this.tapOspProcProduct.SuspendLayout();
            this.tapOspProcLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(371, 900);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(843, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(843, 903);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1224, 939);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tapProcessCosts, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 1;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 472F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(843, 903);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // tapProcessCosts
            // 
            this.tapProcessCosts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapProcessCosts.Location = new System.Drawing.Point(3, 3);
            this.tapProcessCosts.Name = "tapProcessCosts";
            this.tapProcessCosts.SelectedTabPage = this.tapOspProcProduct;
            this.tapProcessCosts.Size = new System.Drawing.Size(837, 897);
            this.tapProcessCosts.TabIndex = 0;
            this.tapProcessCosts.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tapOspProcProduct,
            this.tapOspProcLayer});
            // 
            // tapOspProcProduct
            // 
            this.tapOspProcProduct.Controls.Add(this.grdOspProcProduct);
            this.tapProcessCosts.SetLanguageKey(this.tapOspProcProduct, "OSPPROCESSINGPRODUCT");
            this.tapOspProcProduct.Name = "tapOspProcProduct";
            this.tapOspProcProduct.Size = new System.Drawing.Size(830, 861);
            this.tapOspProcProduct.Text = "제품타입별 집계";
            // 
            // grdOspProcProduct
            // 
            this.grdOspProcProduct.Caption = "";
            this.grdOspProcProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspProcProduct.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspProcProduct.IsUsePaging = false;
            this.grdOspProcProduct.LanguageKey = "OUTSOURCINGPROCESSINGCOSTSVENDOR";
            this.grdOspProcProduct.Location = new System.Drawing.Point(0, 0);
            this.grdOspProcProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspProcProduct.Name = "grdOspProcProduct";
            this.grdOspProcProduct.ShowBorder = true;
            this.grdOspProcProduct.ShowStatusBar = false;
            this.grdOspProcProduct.Size = new System.Drawing.Size(830, 861);
            this.grdOspProcProduct.TabIndex = 9;
            this.grdOspProcProduct.UseAutoBestFitColumns = false;
            // 
            // tapOspProcLayer
            // 
            this.tapOspProcLayer.Controls.Add(this.grdOspProcLayer);
            this.tapProcessCosts.SetLanguageKey(this.tapOspProcLayer, "OSPPROCESSINGLAYER");
            this.tapOspProcLayer.Name = "tapOspProcLayer";
            this.tapOspProcLayer.Size = new System.Drawing.Size(830, 861);
            this.tapOspProcLayer.Text = "층수별집계";
            // 
            // grdOspProcLayer
            // 
            this.grdOspProcLayer.Caption = "";
            this.grdOspProcLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOspProcLayer.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdOspProcLayer.IsUsePaging = false;
            this.grdOspProcLayer.LanguageKey = "OUTSOURCINGPROCESSINGCOSTSLAYER";
            this.grdOspProcLayer.Location = new System.Drawing.Point(0, 0);
            this.grdOspProcLayer.Margin = new System.Windows.Forms.Padding(0);
            this.grdOspProcLayer.Name = "grdOspProcLayer";
            this.grdOspProcLayer.ShowBorder = true;
            this.grdOspProcLayer.ShowStatusBar = false;
            this.grdOspProcLayer.Size = new System.Drawing.Size(830, 861);
            this.grdOspProcLayer.TabIndex = 8;
            this.grdOspProcLayer.UseAutoBestFitColumns = false;
            // 
            // OutsourcingYoungPongProcessingCosts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Name = "OutsourcingYoungPongProcessingCosts";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tapProcessCosts)).EndInit();
            this.tapProcessCosts.ResumeLayout(false);
            this.tapOspProcProduct.ResumeLayout(false);
            this.tapOspProcLayer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartTabControl tapProcessCosts;
        private DevExpress.XtraTab.XtraTabPage tapOspProcLayer;
        private Framework.SmartControls.SmartBandedGrid grdOspProcLayer;
        private DevExpress.XtraTab.XtraTabPage tapOspProcProduct;
        private Framework.SmartControls.SmartBandedGrid grdOspProcProduct;
    }
}