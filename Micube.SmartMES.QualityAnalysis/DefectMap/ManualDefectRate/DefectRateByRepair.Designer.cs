namespace Micube.SmartMES.QualityAnalysis
{
    partial class DefectRateByRepair
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
            this.grbBottom = new Micube.Framework.SmartControls.SmartGroupBox();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabProduct = new DevExpress.XtraTab.XtraTabPage();
            this.flowProduct = new System.Windows.Forms.FlowLayoutPanel();
            this.tabLot = new DevExpress.XtraTab.XtraTabPage();
            this.flowLot = new System.Windows.Forms.FlowLayoutPanel();
            this.grbTop = new Micube.Framework.SmartControls.SmartGroupBox();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdMain = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.chartMain = new Micube.Framework.SmartControls.SmartChart();
            this.smartSpliterControl3 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.grdSummary = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterControl2 = new Micube.Framework.SmartControls.SmartSpliterControl();
            this.btnApply = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbBottom)).BeginInit();
            this.grbBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabProduct.SuspendLayout();
            this.tabLot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbTop)).BeginInit();
            this.grbTop.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 498);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnApply);
            this.pnlToolbar.Size = new System.Drawing.Size(659, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.btnApply, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(659, 502);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(964, 531);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grbBottom, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grbTop, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl1, 0, 1);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdSummary, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSpliterControl2, 0, 3);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 5;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(659, 502);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grbBottom
            // 
            this.grbBottom.Controls.Add(this.tabMain);
            this.grbBottom.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grbBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbBottom.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grbBottom.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grbBottom.Location = new System.Drawing.Point(0, 281);
            this.grbBottom.Margin = new System.Windows.Forms.Padding(0);
            this.grbBottom.Name = "grbBottom";
            this.grbBottom.ShowBorder = true;
            this.grbBottom.Size = new System.Drawing.Size(659, 221);
            this.grbBottom.TabIndex = 7;
            this.grbBottom.Text = "smartGroupBox3";
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(2, 31);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabProduct;
            this.tabMain.Size = new System.Drawing.Size(655, 188);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabProduct,
            this.tabLot});
            // 
            // tabProduct
            // 
            this.tabProduct.Controls.Add(this.flowProduct);
            this.tabProduct.Name = "tabProduct";
            this.tabProduct.Size = new System.Drawing.Size(649, 159);
            this.tabProduct.Text = "xtraTabPage2";
            // 
            // flowProduct
            // 
            this.flowProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowProduct.Location = new System.Drawing.Point(0, 0);
            this.flowProduct.Name = "flowProduct";
            this.flowProduct.Size = new System.Drawing.Size(649, 159);
            this.flowProduct.TabIndex = 0;
            // 
            // tabLot
            // 
            this.tabLot.Controls.Add(this.flowLot);
            this.tabLot.Name = "tabLot";
            this.tabLot.Size = new System.Drawing.Size(746, 151);
            this.tabLot.Text = "xtraTabPage1";
            // 
            // flowLot
            // 
            this.flowLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLot.Location = new System.Drawing.Point(0, 0);
            this.flowLot.Name = "flowLot";
            this.flowLot.Size = new System.Drawing.Size(746, 151);
            this.flowLot.TabIndex = 0;
            // 
            // grbTop
            // 
            this.grbTop.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.grbTop.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.grbTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbTop.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((Micube.Framework.SmartControls.GridButtonItem.Expand | Micube.Framework.SmartControls.GridButtonItem.Restore)));
            this.grbTop.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.grbTop.Location = new System.Drawing.Point(0, 0);
            this.grbTop.Margin = new System.Windows.Forms.Padding(0);
            this.grbTop.Name = "grbTop";
            this.grbTop.ShowBorder = true;
            this.grbTop.Size = new System.Drawing.Size(659, 146);
            this.grbTop.TabIndex = 0;
            this.grbTop.Text = "smartGroupBox1";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 3;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdMain, 0, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.chartMain, 2, 0);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSpliterControl3, 1, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(2, 31);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(655, 113);
            this.smartSplitTableLayoutPanel2.TabIndex = 0;
            // 
            // grdMain
            // 
            this.grdMain.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMain.IsUsePaging = false;
            this.grdMain.LanguageKey = null;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Margin = new System.Windows.Forms.Padding(0);
            this.grdMain.Name = "grdMain";
            this.grdMain.ShowBorder = true;
            this.grdMain.Size = new System.Drawing.Size(548, 113);
            this.grdMain.TabIndex = 0;
            this.grdMain.UseAutoBestFitColumns = false;
            // 
            // chartMain
            // 
            this.chartMain.AutoLayout = false;
            this.chartMain.CacheToMemory = true;
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Legend.Name = "Default Legend";
            this.chartMain.Location = new System.Drawing.Point(558, 0);
            this.chartMain.Margin = new System.Windows.Forms.Padding(0);
            this.chartMain.Name = "chartMain";
            this.chartMain.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartMain.Size = new System.Drawing.Size(97, 113);
            this.chartMain.TabIndex = 1;
            // 
            // smartSpliterControl3
            // 
            this.smartSpliterControl3.Location = new System.Drawing.Point(548, 0);
            this.smartSpliterControl3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl3.Name = "smartSpliterControl3";
            this.smartSpliterControl3.Size = new System.Drawing.Size(5, 113);
            this.smartSpliterControl3.TabIndex = 2;
            this.smartSpliterControl3.TabStop = false;
            // 
            // smartSpliterControl1
            // 
            this.smartSpliterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl1.Location = new System.Drawing.Point(0, 146);
            this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl1.Name = "smartSpliterControl1";
            this.smartSpliterControl1.Size = new System.Drawing.Size(659, 5);
            this.smartSpliterControl1.TabIndex = 4;
            this.smartSpliterControl1.TabStop = false;
            // 
            // grdSummary
            // 
            this.grdSummary.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSummary.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdSummary.IsUsePaging = false;
            this.grdSummary.LanguageKey = null;
            this.grdSummary.Location = new System.Drawing.Point(0, 156);
            this.grdSummary.Margin = new System.Windows.Forms.Padding(0);
            this.grdSummary.Name = "grdSummary";
            this.grdSummary.ShowBorder = true;
            this.grdSummary.Size = new System.Drawing.Size(659, 115);
            this.grdSummary.TabIndex = 8;
            this.grdSummary.UseAutoBestFitColumns = false;
            // 
            // smartSpliterControl2
            // 
            this.smartSpliterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartSpliterControl2.Location = new System.Drawing.Point(0, 271);
            this.smartSpliterControl2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterControl2.Name = "smartSpliterControl2";
            this.smartSpliterControl2.Size = new System.Drawing.Size(659, 5);
            this.smartSpliterControl2.TabIndex = 9;
            this.smartSpliterControl2.TabStop = false;
            // 
            // btnApply
            // 
            this.btnApply.AllowFocus = false;
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApply.IsBusy = false;
            this.btnApply.IsWrite = false;
            this.btnApply.Location = new System.Drawing.Point(584, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnApply.Size = new System.Drawing.Size(75, 24);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "비교";
            this.btnApply.TooltipLanguageKey = "";
            // 
            // DefectRateByRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 551);
            this.Name = "DefectRateByRepair";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grbBottom)).EndInit();
            this.grbBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabProduct.ResumeLayout(false);
            this.tabLot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grbTop)).EndInit();
            this.grbTop.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartGroupBox grbTop;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartButton btnApply;
        private Framework.SmartControls.SmartGroupBox grbBottom;
        private Framework.SmartControls.SmartBandedGrid grdSummary;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl2;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdMain;
        private Framework.SmartControls.SmartChart chartMain;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl3;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabLot;
        private System.Windows.Forms.FlowLayoutPanel flowLot;
        private DevExpress.XtraTab.XtraTabPage tabProduct;
        private System.Windows.Forms.FlowLayoutPanel flowProduct;
    }
}