namespace Micube.SmartMES.ProcessManagement
{
    partial class LotLeadTime
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
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgType = new DevExpress.XtraTab.XtraTabPage();
            this.grdType = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgProduct = new DevExpress.XtraTab.XtraTabPage();
            this.grdProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgLot = new DevExpress.XtraTab.XtraTabPage();
            this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgProcess = new DevExpress.XtraTab.XtraTabPage();
            this.grdProcess = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgArea = new DevExpress.XtraTab.XtraTabPage();
            this.grdArea = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpggraph = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdmonth = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.chamonth = new Micube.Framework.SmartControls.SmartChart();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdresaon = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.chareason = new Micube.Framework.SmartControls.SmartChart();
            this.tablayer = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer3 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdlayer = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.chalayer = new Micube.Framework.SmartControls.SmartChart();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tpgType.SuspendLayout();
            this.tpgProduct.SuspendLayout();
            this.tpgLot.SuspendLayout();
            this.tpgProcess.SuspendLayout();
            this.tpgArea.SuspendLayout();
            this.tpggraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chamonth)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chareason)).BeginInit();
            this.tablayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).BeginInit();
            this.smartSpliterContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chalayer)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabMain);
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.SelectedTabPage = this.tpgType;
            this.tabMain.Size = new System.Drawing.Size(756, 489);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgType,
            this.tpgProduct,
            this.tpgLot,
            this.tpgProcess,
            this.tpgArea,
            this.tpggraph,
            this.xtraTabPage1,
            this.tablayer});
            // 
            // tpgType
            // 
            this.tpgType.Controls.Add(this.grdType);
            this.tpgType.Name = "tpgType";
            this.tpgType.Padding = new System.Windows.Forms.Padding(3);
            this.tpgType.Size = new System.Drawing.Size(750, 460);
            this.tpgType.Text = "Type";
            // 
            // grdType
            // 
            this.grdType.Caption = "Type";
            this.grdType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdType.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdType.IsUsePaging = false;
            this.grdType.LanguageKey = "";
            this.grdType.Location = new System.Drawing.Point(3, 3);
            this.grdType.Margin = new System.Windows.Forms.Padding(0);
            this.grdType.Name = "grdType";
            this.grdType.ShowBorder = true;
            this.grdType.ShowStatusBar = false;
            this.grdType.Size = new System.Drawing.Size(744, 454);
            this.grdType.TabIndex = 3;
            this.grdType.UseAutoBestFitColumns = false;
            // 
            // tpgProduct
            // 
            this.tpgProduct.Controls.Add(this.grdProduct);
            this.tabMain.SetLanguageKey(this.tpgProduct, "PRODUCTDEF");
            this.tpgProduct.Name = "tpgProduct";
            this.tpgProduct.Padding = new System.Windows.Forms.Padding(3);
            this.tpgProduct.Size = new System.Drawing.Size(750, 460);
            this.tpgProduct.Text = "품목";
            // 
            // grdProduct
            // 
            this.grdProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProduct.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProduct.IsUsePaging = false;
            this.grdProduct.LanguageKey = "PRODUCT";
            this.grdProduct.Location = new System.Drawing.Point(3, 3);
            this.grdProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdProduct.Name = "grdProduct";
            this.grdProduct.ShowBorder = true;
            this.grdProduct.ShowStatusBar = false;
            this.grdProduct.Size = new System.Drawing.Size(744, 454);
            this.grdProduct.TabIndex = 2;
            this.grdProduct.UseAutoBestFitColumns = false;
            // 
            // tpgLot
            // 
            this.tpgLot.Controls.Add(this.grdLotList);
            this.tpgLot.Name = "tpgLot";
            this.tpgLot.Padding = new System.Windows.Forms.Padding(3);
            this.tpgLot.Size = new System.Drawing.Size(750, 460);
            this.tpgLot.Text = "Lot";
            // 
            // grdLotList
            // 
            this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotList.IsUsePaging = false;
            this.grdLotList.LanguageKey = "LOT";
            this.grdLotList.Location = new System.Drawing.Point(3, 3);
            this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotList.Name = "grdLotList";
            this.grdLotList.ShowBorder = true;
            this.grdLotList.ShowStatusBar = false;
            this.grdLotList.Size = new System.Drawing.Size(744, 454);
            this.grdLotList.TabIndex = 3;
            this.grdLotList.UseAutoBestFitColumns = false;
            // 
            // tpgProcess
            // 
            this.tpgProcess.Controls.Add(this.grdProcess);
            this.tabMain.SetLanguageKey(this.tpgProcess, "PROCESSSEGMENT");
            this.tpgProcess.Name = "tpgProcess";
            this.tpgProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tpgProcess.Size = new System.Drawing.Size(750, 460);
            this.tpgProcess.Text = "공정";
            // 
            // grdProcess
            // 
            this.grdProcess.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProcess.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProcess.IsUsePaging = false;
            this.grdProcess.LanguageKey = "PROCESSSEGMENT";
            this.grdProcess.Location = new System.Drawing.Point(3, 3);
            this.grdProcess.Margin = new System.Windows.Forms.Padding(0);
            this.grdProcess.Name = "grdProcess";
            this.grdProcess.ShowBorder = true;
            this.grdProcess.ShowStatusBar = false;
            this.grdProcess.Size = new System.Drawing.Size(744, 454);
            this.grdProcess.TabIndex = 3;
            this.grdProcess.UseAutoBestFitColumns = false;
            // 
            // tpgArea
            // 
            this.tpgArea.Controls.Add(this.grdArea);
            this.tabMain.SetLanguageKey(this.tpgArea, "AREA");
            this.tpgArea.Name = "tpgArea";
            this.tpgArea.Padding = new System.Windows.Forms.Padding(3);
            this.tpgArea.Size = new System.Drawing.Size(750, 460);
            this.tpgArea.Text = "작업장";
            // 
            // grdArea
            // 
            this.grdArea.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdArea.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdArea.IsUsePaging = false;
            this.grdArea.LanguageKey = "AREA";
            this.grdArea.Location = new System.Drawing.Point(3, 3);
            this.grdArea.Margin = new System.Windows.Forms.Padding(0);
            this.grdArea.Name = "grdArea";
            this.grdArea.ShowBorder = true;
            this.grdArea.ShowStatusBar = false;
            this.grdArea.Size = new System.Drawing.Size(744, 454);
            this.grdArea.TabIndex = 3;
            this.grdArea.UseAutoBestFitColumns = false;
            // 
            // tpggraph
            // 
            this.tpggraph.Controls.Add(this.smartSpliterContainer1);
            this.tpggraph.Name = "tpggraph";
            this.tpggraph.Size = new System.Drawing.Size(750, 460);
            this.tpggraph.Text = "월별 LT 현황";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdmonth);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.chamonth);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(750, 460);
            this.smartSpliterContainer1.SplitterPosition = 235;
            this.smartSpliterContainer1.TabIndex = 0;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdmonth
            // 
            this.grdmonth.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdmonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdmonth.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdmonth.IsUsePaging = false;
            this.grdmonth.LanguageKey = "MONTHLT";
            this.grdmonth.Location = new System.Drawing.Point(0, 0);
            this.grdmonth.Margin = new System.Windows.Forms.Padding(0);
            this.grdmonth.Name = "grdmonth";
            this.grdmonth.ShowBorder = true;
            this.grdmonth.ShowStatusBar = false;
            this.grdmonth.Size = new System.Drawing.Size(750, 235);
            this.grdmonth.TabIndex = 0;
            this.grdmonth.UseAutoBestFitColumns = false;
            // 
            // chamonth
            // 
            this.chamonth.AutoLayout = false;
            this.chamonth.CacheToMemory = true;
            this.chamonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chamonth.Legend.Name = "Default Legend";
            this.chamonth.Location = new System.Drawing.Point(0, 0);
            this.chamonth.Name = "chamonth";
            this.chamonth.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chamonth.Size = new System.Drawing.Size(750, 220);
            this.chamonth.TabIndex = 0;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.smartSpliterContainer2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(750, 460);
            this.xtraTabPage1.Text = "월별 LT 원인별";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdresaon);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.chareason);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(750, 460);
            this.smartSpliterContainer2.SplitterPosition = 239;
            this.smartSpliterContainer2.TabIndex = 0;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdresaon
            // 
            this.grdresaon.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdresaon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdresaon.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdresaon.IsUsePaging = false;
            this.grdresaon.LanguageKey = "MONTHREASONLT";
            this.grdresaon.Location = new System.Drawing.Point(0, 0);
            this.grdresaon.Margin = new System.Windows.Forms.Padding(0);
            this.grdresaon.Name = "grdresaon";
            this.grdresaon.ShowBorder = true;
            this.grdresaon.ShowStatusBar = false;
            this.grdresaon.Size = new System.Drawing.Size(750, 239);
            this.grdresaon.TabIndex = 0;
            this.grdresaon.UseAutoBestFitColumns = false;
            // 
            // chareason
            // 
            this.chareason.AutoLayout = false;
            this.chareason.CacheToMemory = true;
            this.chareason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chareason.Legend.Name = "Default Legend";
            this.chareason.Location = new System.Drawing.Point(0, 0);
            this.chareason.Name = "chareason";
            this.chareason.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chareason.Size = new System.Drawing.Size(750, 216);
            this.chareason.TabIndex = 0;
            // 
            // tablayer
            // 
            this.tablayer.Controls.Add(this.smartSpliterContainer3);
            this.tablayer.Name = "tablayer";
            this.tablayer.Size = new System.Drawing.Size(750, 460);
            this.tablayer.Text = "층별 LT 세부현황";
            // 
            // smartSpliterContainer3
            // 
            this.smartSpliterContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer3.Horizontal = false;
            this.smartSpliterContainer3.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer3.Name = "smartSpliterContainer3";
            this.smartSpliterContainer3.Panel1.Controls.Add(this.grdlayer);
            this.smartSpliterContainer3.Panel1.Text = "Panel1";
            this.smartSpliterContainer3.Panel2.Controls.Add(this.chalayer);
            this.smartSpliterContainer3.Panel2.Text = "Panel2";
            this.smartSpliterContainer3.Size = new System.Drawing.Size(750, 460);
            this.smartSpliterContainer3.SplitterPosition = 242;
            this.smartSpliterContainer3.TabIndex = 0;
            this.smartSpliterContainer3.Text = "smartSpliterContainer3";
            this.smartSpliterContainer3.Paint += new System.Windows.Forms.PaintEventHandler(this.smartSpliterContainer3_Paint);
            // 
            // grdlayer
            // 
            this.grdlayer.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdlayer.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdlayer.IsUsePaging = false;
            this.grdlayer.LanguageKey = "LAYERLT";
            this.grdlayer.Location = new System.Drawing.Point(0, 0);
            this.grdlayer.Margin = new System.Windows.Forms.Padding(0);
            this.grdlayer.Name = "grdlayer";
            this.grdlayer.ShowBorder = true;
            this.grdlayer.Size = new System.Drawing.Size(750, 242);
            this.grdlayer.TabIndex = 0;
            this.grdlayer.UseAutoBestFitColumns = false;
            // 
            // chalayer
            // 
            this.chalayer.AutoLayout = false;
            this.chalayer.CacheToMemory = true;
            this.chalayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chalayer.Legend.Name = "Default Legend";
            this.chalayer.Location = new System.Drawing.Point(0, 0);
            this.chalayer.Name = "chalayer";
            this.chalayer.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chalayer.Size = new System.Drawing.Size(750, 213);
            this.chalayer.TabIndex = 0;
            // 
            // LotLeadTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 538);
            this.Name = "LotLeadTime";
            this.Text = "Lead Time";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tpgType.ResumeLayout(false);
            this.tpgProduct.ResumeLayout(false);
            this.tpgLot.ResumeLayout(false);
            this.tpgProcess.ResumeLayout(false);
            this.tpgArea.ResumeLayout(false);
            this.tpggraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chamonth)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chareason)).EndInit();
            this.tablayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).EndInit();
            this.smartSpliterContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chalayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tpgType;
        private DevExpress.XtraTab.XtraTabPage tpgProduct;
        private DevExpress.XtraTab.XtraTabPage tpgLot;
        private DevExpress.XtraTab.XtraTabPage tpgProcess;
        private DevExpress.XtraTab.XtraTabPage tpgArea;
        private Framework.SmartControls.SmartBandedGrid grdProduct;
        private Framework.SmartControls.SmartBandedGrid grdType;
        private Framework.SmartControls.SmartBandedGrid grdLotList;
        private Framework.SmartControls.SmartBandedGrid grdProcess;
        private Framework.SmartControls.SmartBandedGrid grdArea;
        private DevExpress.XtraTab.XtraTabPage tpggraph;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdmonth;
        private Framework.SmartControls.SmartChart chamonth;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdresaon;
        private Framework.SmartControls.SmartChart chareason;
        private DevExpress.XtraTab.XtraTabPage tablayer;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer3;
        private Framework.SmartControls.SmartBandedGrid grdlayer;
        private Framework.SmartControls.SmartChart chalayer;
    }
}