namespace Micube.SmartMES.ProcessManagement
{
    partial class InputLotRateSearch
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
            this.grdInputDay = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInputDaySummaryByProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInputDaySummaryByLotInputType = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.tabInputLotRateSearch = new Micube.Framework.SmartControls.SmartTabControl();
            this.pagInputDay = new DevExpress.XtraTab.XtraTabPage();
            this.pagModel = new DevExpress.XtraTab.XtraTabPage();
            this.grdModel = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.pagCustomer = new DevExpress.XtraTab.XtraTabPage();
            this.grdCustomer = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabInputLotRateSearch)).BeginInit();
            this.tabInputLotRateSearch.SuspendLayout();
            this.pagInputDay.SuspendLayout();
            this.pagModel.SuspendLayout();
            this.pagCustomer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 587);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(868, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabInputLotRateSearch);
            this.pnlContent.Size = new System.Drawing.Size(868, 591);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1173, 620);
            // 
            // grdInputDay
            // 
            this.grdInputDay.Caption = "";
            this.grdInputDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputDay.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputDay.IsUsePaging = false;
            this.grdInputDay.LanguageKey = "INPUTRATESTATUS";
            this.grdInputDay.Location = new System.Drawing.Point(0, 0);
            this.grdInputDay.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputDay.Name = "grdInputDay";
            this.grdInputDay.ShowBorder = true;
            this.grdInputDay.ShowStatusBar = false;
            this.grdInputDay.Size = new System.Drawing.Size(862, 233);
            this.grdInputDay.TabIndex = 0;
            this.grdInputDay.Tag = "INPUTDAY";
            this.grdInputDay.UseAutoBestFitColumns = false;
            // 
            // grdInputDaySummaryByProduct
            // 
            this.grdInputDaySummaryByProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInputDaySummaryByProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputDaySummaryByProduct.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputDaySummaryByProduct.IsUsePaging = false;
            this.grdInputDaySummaryByProduct.LanguageKey = "DAILYDETAIL";
            this.grdInputDaySummaryByProduct.Location = new System.Drawing.Point(0, 0);
            this.grdInputDaySummaryByProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputDaySummaryByProduct.Name = "grdInputDaySummaryByProduct";
            this.grdInputDaySummaryByProduct.ShowBorder = true;
            this.grdInputDaySummaryByProduct.ShowStatusBar = false;
            this.grdInputDaySummaryByProduct.Size = new System.Drawing.Size(862, 106);
            this.grdInputDaySummaryByProduct.TabIndex = 1;
            this.grdInputDaySummaryByProduct.UseAutoBestFitColumns = false;
            // 
            // grdInputDaySummaryByLotInputType
            // 
            this.grdInputDaySummaryByLotInputType.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInputDaySummaryByLotInputType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInputDaySummaryByLotInputType.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInputDaySummaryByLotInputType.IsUsePaging = false;
            this.grdInputDaySummaryByLotInputType.LanguageKey = "SUMMARYGROUP";
            this.grdInputDaySummaryByLotInputType.Location = new System.Drawing.Point(0, 0);
            this.grdInputDaySummaryByLotInputType.Margin = new System.Windows.Forms.Padding(0);
            this.grdInputDaySummaryByLotInputType.Name = "grdInputDaySummaryByLotInputType";
            this.grdInputDaySummaryByLotInputType.ShowBorder = true;
            this.grdInputDaySummaryByLotInputType.ShowStatusBar = false;
            this.grdInputDaySummaryByLotInputType.Size = new System.Drawing.Size(862, 213);
            this.grdInputDaySummaryByLotInputType.TabIndex = 2;
            this.grdInputDaySummaryByLotInputType.UseAutoBestFitColumns = false;
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Horizontal = false;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdInputDay);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdInputDaySummaryByLotInputType);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(862, 451);
            this.smartSpliterContainer1.SplitterPosition = 233;
            this.smartSpliterContainer1.TabIndex = 3;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Horizontal = false;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.smartSpliterContainer1);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdInputDaySummaryByProduct);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(862, 562);
            this.smartSpliterContainer2.SplitterPosition = 451;
            this.smartSpliterContainer2.TabIndex = 4;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // tabInputLotRateSearch
            // 
            this.tabInputLotRateSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInputLotRateSearch.Location = new System.Drawing.Point(0, 0);
            this.tabInputLotRateSearch.Name = "tabInputLotRateSearch";
            this.tabInputLotRateSearch.SelectedTabPage = this.pagInputDay;
            this.tabInputLotRateSearch.Size = new System.Drawing.Size(868, 591);
            this.tabInputLotRateSearch.TabIndex = 5;
            this.tabInputLotRateSearch.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pagInputDay,
            this.pagModel,
            this.pagCustomer});
            // 
            // pagInputDay
            // 
            this.pagInputDay.Controls.Add(this.smartSpliterContainer2);
            this.tabInputLotRateSearch.SetLanguageKey(this.pagInputDay, "INPUTDAY");
            this.pagInputDay.Name = "pagInputDay";
            this.pagInputDay.Size = new System.Drawing.Size(862, 562);
            this.pagInputDay.Text = "투입일";
            // 
            // pagModel
            // 
            this.pagModel.Controls.Add(this.grdModel);
            this.tabInputLotRateSearch.SetLanguageKey(this.pagModel, "BYMODEL");
            this.pagModel.Name = "pagModel";
            this.pagModel.Size = new System.Drawing.Size(750, 460);
            this.pagModel.Text = "모델별";
            // 
            // grdModel
            // 
            this.grdModel.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdModel.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdModel.IsUsePaging = false;
            this.grdModel.LanguageKey = "INPUTRATESTATUS";
            this.grdModel.Location = new System.Drawing.Point(0, 0);
            this.grdModel.Margin = new System.Windows.Forms.Padding(0);
            this.grdModel.Name = "grdModel";
            this.grdModel.ShowBorder = true;
            this.grdModel.ShowStatusBar = false;
            this.grdModel.Size = new System.Drawing.Size(750, 460);
            this.grdModel.TabIndex = 0;
            this.grdModel.UseAutoBestFitColumns = false;
            // 
            // pagCustomer
            // 
            this.pagCustomer.Controls.Add(this.grdCustomer);
            this.tabInputLotRateSearch.SetLanguageKey(this.pagCustomer, "BYCOMPANY");
            this.pagCustomer.Name = "pagCustomer";
            this.pagCustomer.Size = new System.Drawing.Size(750, 460);
            this.pagCustomer.Text = "고객사별";
            // 
            // grdCustomer
            // 
            this.grdCustomer.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCustomer.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdCustomer.IsUsePaging = false;
            this.grdCustomer.LanguageKey = "INPUTRATESTATUS";
            this.grdCustomer.Location = new System.Drawing.Point(0, 0);
            this.grdCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.grdCustomer.Name = "grdCustomer";
            this.grdCustomer.ShowBorder = true;
            this.grdCustomer.ShowStatusBar = false;
            this.grdCustomer.Size = new System.Drawing.Size(750, 460);
            this.grdCustomer.TabIndex = 0;
            this.grdCustomer.UseAutoBestFitColumns = false;
            // 
            // InputLotRateSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 658);
            this.Name = "InputLotRateSearch";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabInputLotRateSearch)).EndInit();
            this.tabInputLotRateSearch.ResumeLayout(false);
            this.pagInputDay.ResumeLayout(false);
            this.pagModel.ResumeLayout(false);
            this.pagCustomer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartBandedGrid grdInputDay;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdInputDaySummaryByLotInputType;
        private Framework.SmartControls.SmartBandedGrid grdInputDaySummaryByProduct;
        private Framework.SmartControls.SmartTabControl tabInputLotRateSearch;
        private DevExpress.XtraTab.XtraTabPage pagInputDay;
        private DevExpress.XtraTab.XtraTabPage pagModel;
        private DevExpress.XtraTab.XtraTabPage pagCustomer;
        private Framework.SmartControls.SmartBandedGrid grdModel;
        private Framework.SmartControls.SmartBandedGrid grdCustomer;
    }
}