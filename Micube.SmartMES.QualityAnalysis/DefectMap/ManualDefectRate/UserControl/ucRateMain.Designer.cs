namespace Micube.SmartMES.QualityAnalysis
{
    partial class ucRateMain
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabProduct = new DevExpress.XtraTab.XtraTabPage();
            this.tabLot = new DevExpress.XtraTab.XtraTabPage();
            this.tabRawData = new DevExpress.XtraTab.XtraTabPage();
            this.grdRawData = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabRawData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabProduct;
            this.tabMain.Size = new System.Drawing.Size(1000, 600);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabProduct,
            this.tabLot,
            this.tabRawData});
            // 
            // tabProduct
            // 
            this.tabProduct.Name = "tabProduct";
            this.tabProduct.Padding = new System.Windows.Forms.Padding(5);
            this.tabProduct.Size = new System.Drawing.Size(994, 571);
            this.tabProduct.Text = "xtraTabPage1";
            // 
            // tabLot
            // 
            this.tabLot.Name = "tabLot";
            this.tabLot.Padding = new System.Windows.Forms.Padding(5);
            this.tabLot.Size = new System.Drawing.Size(994, 571);
            this.tabLot.Text = "xtraTabPage2";
            // 
            // tabRawData
            // 
            this.tabRawData.Controls.Add(this.grdRawData);
            this.tabRawData.Name = "tabRawData";
            this.tabRawData.Padding = new System.Windows.Forms.Padding(3);
            this.tabRawData.Size = new System.Drawing.Size(994, 571);
            this.tabRawData.Text = "xtraTabPage3";
            // 
            // grdRawData
            // 
            this.grdRawData.Caption = "";
            this.grdRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRawData.IsUsePaging = false;
            this.grdRawData.LanguageKey = null;
            this.grdRawData.Location = new System.Drawing.Point(3, 3);
            this.grdRawData.Margin = new System.Windows.Forms.Padding(0);
            this.grdRawData.Name = "grdRawData";
            this.grdRawData.ShowBorder = true;
            this.grdRawData.ShowStatusBar = false;
            this.grdRawData.Size = new System.Drawing.Size(988, 565);
            this.grdRawData.TabIndex = 0;
            // 
            // ucRateMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucRateMain";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabRawData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabProduct;
        private DevExpress.XtraTab.XtraTabPage tabLot;
        private DevExpress.XtraTab.XtraTabPage tabRawData;
        private Framework.SmartControls.SmartBandedGrid grdRawData;
    }
}
