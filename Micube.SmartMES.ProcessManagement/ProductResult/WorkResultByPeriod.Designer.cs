namespace Micube.SmartMES.ProcessManagement
{
    partial class WorkResultByPeriod
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
            this.tabWorkResultByPeriod = new Micube.Framework.SmartControls.SmartTabControl();
            this.bySegmentAreaPage = new DevExpress.XtraTab.XtraTabPage();
            this.grdByArea = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.byProductPage = new DevExpress.XtraTab.XtraTabPage();
            this.grdByProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.byLotPage = new DevExpress.XtraTab.XtraTabPage();
            this.grdByLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.lblSearchCriteria = new Micube.Framework.SmartControls.SmartLabel();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabWorkResultByPeriod)).BeginInit();
            this.tabWorkResultByPeriod.SuspendLayout();
            this.bySegmentAreaPage.SuspendLayout();
            this.byProductPage.SuspendLayout();
            this.byLotPage.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 515);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(796, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlContent.Size = new System.Drawing.Size(796, 519);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(1101, 548);
            // 
            // tabWorkResultByPeriod
            // 
            this.tabWorkResultByPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWorkResultByPeriod.Location = new System.Drawing.Point(3, 31);
            this.tabWorkResultByPeriod.Name = "tabWorkResultByPeriod";
            this.tabWorkResultByPeriod.SelectedTabPage = this.bySegmentAreaPage;
            this.tabWorkResultByPeriod.Size = new System.Drawing.Size(790, 485);
            this.tabWorkResultByPeriod.TabIndex = 0;
            this.tabWorkResultByPeriod.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.bySegmentAreaPage,
            this.byProductPage,
            this.byLotPage});
            // 
            // bySegmentAreaPage
            // 
            this.bySegmentAreaPage.Controls.Add(this.grdByArea);
            this.tabWorkResultByPeriod.SetLanguageKey(this.bySegmentAreaPage, "BYSEGMENTAREA");
            this.bySegmentAreaPage.Name = "bySegmentAreaPage";
            this.bySegmentAreaPage.Size = new System.Drawing.Size(784, 456);
            this.bySegmentAreaPage.Text = "공정/ 작업장";
            // 
            // grdByArea
            // 
            this.grdByArea.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdByArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdByArea.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdByArea.IsUsePaging = false;
            this.grdByArea.LanguageKey = "WORKRESULTBYAREA";
            this.grdByArea.Location = new System.Drawing.Point(0, 0);
            this.grdByArea.Margin = new System.Windows.Forms.Padding(0);
            this.grdByArea.Name = "grdByArea";
            this.grdByArea.ShowBorder = true;
            this.grdByArea.ShowStatusBar = false;
            this.grdByArea.Size = new System.Drawing.Size(784, 456);
            this.grdByArea.TabIndex = 0;
            this.grdByArea.UseAutoBestFitColumns = false;
            // 
            // byProductPage
            // 
            this.byProductPage.Controls.Add(this.grdByProduct);
            this.tabWorkResultByPeriod.SetLanguageKey(this.byProductPage, "BYPRODUCT");
            this.byProductPage.Name = "byProductPage";
            this.byProductPage.Size = new System.Drawing.Size(744, 426);
            this.byProductPage.Text = "품목";
            // 
            // grdByProduct
            // 
            this.grdByProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdByProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdByProduct.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdByProduct.IsUsePaging = false;
            this.grdByProduct.LanguageKey = "WORKRESULTBYPRODUCT";
            this.grdByProduct.Location = new System.Drawing.Point(0, 0);
            this.grdByProduct.Margin = new System.Windows.Forms.Padding(0);
            this.grdByProduct.Name = "grdByProduct";
            this.grdByProduct.ShowBorder = true;
            this.grdByProduct.ShowStatusBar = false;
            this.grdByProduct.Size = new System.Drawing.Size(744, 426);
            this.grdByProduct.TabIndex = 0;
            this.grdByProduct.UseAutoBestFitColumns = false;
            // 
            // byLotPage
            // 
            this.byLotPage.Controls.Add(this.grdByLot);
            this.tabWorkResultByPeriod.SetLanguageKey(this.byLotPage, "BYLOT");
            this.byLotPage.Name = "byLotPage";
            this.byLotPage.Size = new System.Drawing.Size(744, 426);
            this.byLotPage.Text = "LOT";
            // 
            // grdByLot
            // 
            this.grdByLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdByLot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdByLot.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdByLot.IsUsePaging = false;
            this.grdByLot.LanguageKey = "WORKRESULTBYLOT";
            this.grdByLot.Location = new System.Drawing.Point(0, 0);
            this.grdByLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdByLot.Name = "grdByLot";
            this.grdByLot.ShowBorder = true;
            this.grdByLot.ShowStatusBar = false;
            this.grdByLot.Size = new System.Drawing.Size(744, 426);
            this.grdByLot.TabIndex = 0;
            this.grdByLot.UseAutoBestFitColumns = false;
            // 
            // lblSearchCriteria
            // 
            this.lblSearchCriteria.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblSearchCriteria.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblSearchCriteria.Appearance.Options.UseFont = true;
            this.lblSearchCriteria.Appearance.Options.UseForeColor = true;
            this.lblSearchCriteria.Location = new System.Drawing.Point(3, 3);
            this.lblSearchCriteria.Name = "lblSearchCriteria";
            this.lblSearchCriteria.Size = new System.Drawing.Size(127, 19);
            this.lblSearchCriteria.TabIndex = 3;
            this.lblSearchCriteria.Text = "조회 기준1  08:30";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblSearchCriteria, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabWorkResultByPeriod, 0, 2);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(796, 519);
            this.smartSplitTableLayoutPanel1.TabIndex = 1;
            // 
            // WorkResultByPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 568);
            this.Name = "WorkResultByPeriod";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabWorkResultByPeriod)).EndInit();
            this.tabWorkResultByPeriod.ResumeLayout(false);
            this.bySegmentAreaPage.ResumeLayout(false);
            this.byProductPage.ResumeLayout(false);
            this.byLotPage.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Framework.SmartControls.SmartTabControl tabWorkResultByPeriod;
        private DevExpress.XtraTab.XtraTabPage bySegmentAreaPage;
        private DevExpress.XtraTab.XtraTabPage byProductPage;
        private DevExpress.XtraTab.XtraTabPage byLotPage;
        private Framework.SmartControls.SmartBandedGrid grdByArea;
        private Framework.SmartControls.SmartBandedGrid grdByProduct;
        private Framework.SmartControls.SmartBandedGrid grdByLot;
        private Framework.SmartControls.SmartLabel lblSearchCriteria;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
    }
}