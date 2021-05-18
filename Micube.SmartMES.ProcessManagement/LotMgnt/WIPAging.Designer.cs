namespace Micube.SmartMES.ProcessManagement
{
    partial class WIPAging
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
			this.tabWipAging = new Micube.Framework.SmartControls.SmartTabControl();
			this.agingByProductPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdAgingByProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.agingByLotPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdAgingByLot = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabWipAging)).BeginInit();
			this.tabWipAging.SuspendLayout();
			this.agingByProductPage.SuspendLayout();
			this.agingByLotPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
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
			this.pnlContent.Controls.Add(this.tabWipAging);
			this.pnlContent.Size = new System.Drawing.Size(457, 383);
			// 
			// pnlMain
			// 
			this.pnlMain.Location = new System.Drawing.Point(19, 19);
			this.pnlMain.Size = new System.Drawing.Size(762, 412);
			// 
			// tabWipAging
			// 
			this.tabWipAging.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabWipAging.Location = new System.Drawing.Point(0, 0);
			this.tabWipAging.Name = "tabWipAging";
			this.tabWipAging.SelectedTabPage = this.agingByProductPage;
			this.tabWipAging.Size = new System.Drawing.Size(457, 383);
			this.tabWipAging.TabIndex = 0;
			this.tabWipAging.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.agingByProductPage,
            this.agingByLotPage});
			// 
			// agingByProductPage
			// 
			this.agingByProductPage.Controls.Add(this.grdAgingByProduct);
			this.tabWipAging.SetLanguageKey(this.agingByProductPage, "BYPRODUCT");
			this.agingByProductPage.Name = "agingByProductPage";
			this.agingByProductPage.Size = new System.Drawing.Size(451, 354);
			this.agingByProductPage.Text = "품목별";
			// 
			// grdAgingByProduct
			// 
			this.grdAgingByProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdAgingByProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdAgingByProduct.IsUsePaging = false;
			this.grdAgingByProduct.LanguageKey = "AGINGSTATUSBYPRODUCT";
			this.grdAgingByProduct.Location = new System.Drawing.Point(0, 0);
			this.grdAgingByProduct.Margin = new System.Windows.Forms.Padding(0);
			this.grdAgingByProduct.Name = "grdAgingByProduct";
			this.grdAgingByProduct.ShowBorder = true;
			this.grdAgingByProduct.Size = new System.Drawing.Size(451, 354);
			this.grdAgingByProduct.TabIndex = 0;
			// 
			// agingByLotPage
			// 
			this.agingByLotPage.Controls.Add(this.grdAgingByLot);
			this.tabWipAging.SetLanguageKey(this.agingByLotPage, "BYLOT");
			this.agingByLotPage.Name = "agingByLotPage";
			this.agingByLotPage.Size = new System.Drawing.Size(451, 354);
			this.agingByLotPage.Text = "LOT별";
			// 
			// grdAgingByLot
			// 
			this.grdAgingByLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdAgingByLot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdAgingByLot.IsUsePaging = false;
			this.grdAgingByLot.LanguageKey = "AGINGSTATUSBYLOT";
			this.grdAgingByLot.Location = new System.Drawing.Point(0, 0);
			this.grdAgingByLot.Margin = new System.Windows.Forms.Padding(0);
			this.grdAgingByLot.Name = "grdAgingByLot";
			this.grdAgingByLot.ShowBorder = true;
			this.grdAgingByLot.ShowStatusBar = false;
			this.grdAgingByLot.Size = new System.Drawing.Size(451, 354);
			this.grdAgingByLot.TabIndex = 0;
			// 
			// WIPAging
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "WIPAging";
			this.Padding = new System.Windows.Forms.Padding(19);
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabWipAging)).EndInit();
			this.tabWipAging.ResumeLayout(false);
			this.agingByProductPage.ResumeLayout(false);
			this.agingByLotPage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabWipAging;
        private DevExpress.XtraTab.XtraTabPage agingByProductPage;
        private DevExpress.XtraTab.XtraTabPage agingByLotPage;
        private Framework.SmartControls.SmartBandedGrid grdAgingByProduct;
        private Framework.SmartControls.SmartBandedGrid grdAgingByLot;
    }
}