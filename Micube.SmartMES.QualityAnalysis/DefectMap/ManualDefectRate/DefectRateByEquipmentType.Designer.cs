namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectRateByEquipmentType
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
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grbComparison = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.chartMain = new Micube.Framework.SmartControls.SmartChart();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grbInspectionType = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabProduct = new DevExpress.XtraTab.XtraTabPage();
            this.tabLot = new DevExpress.XtraTab.XtraTabPage();
            this.flowProduct = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLot = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbComparison)).BeginInit();
            this.grbComparison.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbInspectionType)).BeginInit();
            this.grbInspectionType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabProduct.SuspendLayout();
            this.tabLot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 508);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnApply);
            this.pnlToolbar.Size = new System.Drawing.Size(859, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnApply, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(859, 512);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1164, 541);
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.Location = new System.Drawing.Point(784, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(75, 24);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "비교";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grbComparison, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grbInspectionType, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(859, 512);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grbComparison
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.grbComparison, 2);
            this.grbComparison.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.grbComparison.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grbComparison.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbComparison.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grbComparison.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grbComparison.Location = new System.Drawing.Point(3, 3);
            this.grbComparison.Name = "grbComparison";
            this.grbComparison.ShowBorder = true;
            this.grbComparison.Size = new System.Drawing.Size(853, 194);
            this.grbComparison.TabIndex = 0;
            this.grbComparison.Text = "smartGroupBox1";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 3;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.chartMain, 2, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdMain, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSpliterControl2, 1, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(849, 161);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // chartMain
            // 
            this.chartMain.AutoLayout = false;
            this.chartMain.CacheToMemory = true;
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Legend.Name = "Default Legend";
            this.chartMain.Location = new System.Drawing.Point(597, 0);
            this.chartMain.Margin = new System.Windows.Forms.Padding(0);
            this.chartMain.Name = "chartMain";
            this.chartMain.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartMain.Size = new System.Drawing.Size(252, 161);
            this.chartMain.TabIndex = 0;
            // 
            // grdMain
            // 
            this.grdMain.Caption = "";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(587, 161);
            this.grdMain.TabIndex = 0;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Location = new System.Drawing.Point(587, 0);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(5, 161);
            this.smartSpliterControl2.TabIndex = 1;
            this.smartSpliterControl2.TabStop = false;
            // 
            // grbInspectionType
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.grbInspectionType, 2);
            this.grbInspectionType.Controls.Add(this.tabMain);
            this.grbInspectionType.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grbInspectionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbInspectionType.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grbInspectionType.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grbInspectionType.Location = new System.Drawing.Point(3, 213);
            this.grbInspectionType.Name = "grbInspectionType";
            this.grbInspectionType.ShowBorder = true;
            this.grbInspectionType.Size = new System.Drawing.Size(853, 296);
            this.grbInspectionType.TabIndex = 2;
            this.grbInspectionType.Text = "smartGroupBox2";
            // 
            // smartSpliterControl1
            // 
            this.smartSplitTableLayoutPanel1.SetColumnSpan(this.smartSpliterControl1, 2);
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 200);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(859, 5);
            this.smartSpliterControl1.TabIndex = 3;
            this.smartSpliterControl1.TabStop = false;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(2, 31);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabProduct;
            this.tabMain.Size = new System.Drawing.Size(849, 263);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabProduct,
            this.tabLot});
            // 
            // tabProduct
            // 
            this.tabProduct.Controls.Add(this.flowProduct);
            this.tabProduct.Name = "tabProduct";
            this.tabProduct.Size = new System.Drawing.Size(843, 234);
            this.tabProduct.Text = "xtraTabPage1";
            // 
            // tabLot
            // 
            this.tabLot.Controls.Add(this.flowLot);
            this.tabLot.Name = "tabLot";
            this.tabLot.Size = new System.Drawing.Size(843, 234);
            this.tabLot.Text = "xtraTabPage2";
            // 
            // flowProduct
            // 
            this.flowProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowProduct.Location = new System.Drawing.Point(0, 0);
            this.flowProduct.Name = "flowProduct";
            this.flowProduct.Size = new System.Drawing.Size(843, 234);
            this.flowProduct.TabIndex = 0;
            // 
            // flowLot
            // 
            this.flowLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLot.Location = new System.Drawing.Point(0, 0);
            this.flowLot.Name = "flowLot";
            this.flowLot.Size = new System.Drawing.Size(843, 234);
            this.flowLot.TabIndex = 0;
            // 
            // DefectRateByEquipmentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Name = "DefectRateByEquipmentType";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grbComparison)).EndInit();
            this.grbComparison.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbInspectionType)).EndInit();
            this.grbInspectionType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabProduct.ResumeLayout(false);
            this.tabLot.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartButton btnApply;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox grbComparison;
        private Framework.SmartControls.SmartGroupBox grbInspectionType;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartChart chartMain;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabProduct;
        private System.Windows.Forms.FlowLayoutPanel flowProduct;
        private DevExpress.XtraTab.XtraTabPage tabLot;
        private System.Windows.Forms.FlowLayoutPanel flowLot;
    }
}