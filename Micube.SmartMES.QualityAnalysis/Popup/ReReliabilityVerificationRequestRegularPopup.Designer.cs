namespace Micube.SmartMES.QualityAnalysis
{
    partial class ReReliabilityVerificationRequestRegularPopup
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
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tabReReliabilityVerificationRequestRegular = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.tlpMainTable = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.manufacturingHistoryControl1 = new Micube.SmartMES.QualityAnalysis.ManufacturingHistoryControl();
            this.grdProductInformation1 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdMeasuredValue2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdInspection2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdManufacturingHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdProductInformation2 = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnSave = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabReReliabilityVerificationRequestRegular)).BeginInit();
            this.tabReReliabilityVerificationRequestRegular.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.tlpMainTable.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(1244, 705);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabReReliabilityVerificationRequestRegular, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1244, 705);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // tabReReliabilityVerificationRequestRegular
            // 
            this.tabReReliabilityVerificationRequestRegular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReReliabilityVerificationRequestRegular.Location = new System.Drawing.Point(3, 3);
            this.tabReReliabilityVerificationRequestRegular.Name = "tabReReliabilityVerificationRequestRegular";
            this.tabReReliabilityVerificationRequestRegular.SelectedTabPage = this.xtraTabPage1;
            this.tabReReliabilityVerificationRequestRegular.Size = new System.Drawing.Size(1238, 664);
            this.tabReReliabilityVerificationRequestRegular.TabIndex = 1;
            this.tabReReliabilityVerificationRequestRegular.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.tlpMainTable);
            this.tabReReliabilityVerificationRequestRegular.SetLanguageKey(this.xtraTabPage1, "REQUESTSECONDOPINION");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1232, 635);
            this.xtraTabPage1.Text = "재의뢰";
            // 
            // tlpMainTable
            // 
            this.tlpMainTable.ColumnCount = 2;
            this.tlpMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainTable.Controls.Add(this.manufacturingHistoryControl1, 0, 1);
            this.tlpMainTable.Controls.Add(this.grdProductInformation1, 0, 0);
            this.tlpMainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainTable.Location = new System.Drawing.Point(0, 0);
            this.tlpMainTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tlpMainTable.Name = "tlpMainTable";
            this.tlpMainTable.RowCount = 3;
            this.tlpMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.33203F));
            this.tlpMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.33443F));
            this.tlpMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.33353F));
            this.tlpMainTable.Size = new System.Drawing.Size(1232, 635);
            this.tlpMainTable.TabIndex = 1;
            // 
            // manufacturingHistoryControl1
            // 
            this.tlpMainTable.SetColumnSpan(this.manufacturingHistoryControl1, 2);
            this.manufacturingHistoryControl1.CurrentDataRow = null;
            this.manufacturingHistoryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manufacturingHistoryControl1.Location = new System.Drawing.Point(3, 132);
            this.manufacturingHistoryControl1.Name = "manufacturingHistoryControl1";
            this.tlpMainTable.SetRowSpan(this.manufacturingHistoryControl1, 2);
            this.manufacturingHistoryControl1.Size = new System.Drawing.Size(1226, 500);
            this.manufacturingHistoryControl1.TabIndex = 7;
            // 
            // grdProductInformation1
            // 
            this.grdProductInformation1.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.tlpMainTable.SetColumnSpan(this.grdProductInformation1, 2);
            this.grdProductInformation1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductInformation1.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProductInformation1.IsUsePaging = false;
            this.grdProductInformation1.LanguageKey = "PRODUCTINFORMATION";
            this.grdProductInformation1.Location = new System.Drawing.Point(0, 0);
            this.grdProductInformation1.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductInformation1.Name = "grdProductInformation1";
            this.grdProductInformation1.ShowBorder = true;
            this.grdProductInformation1.ShowStatusBar = false;
            this.grdProductInformation1.Size = new System.Drawing.Size(1232, 129);
            this.grdProductInformation1.TabIndex = 3;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.tabReReliabilityVerificationRequestRegular.SetLanguageKey(this.xtraTabPage2, "PREVIOUSRESULTS");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1232, 635);
            this.xtraTabPage2.Text = "이전결과";
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdMeasuredValue2, 1, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdInspection2, 0, 2);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdManufacturingHistory, 0, 1);
            this.smartSplitTableLayoutPanel2.Controls.Add(this.grdProductInformation2, 0, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 3;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.33203F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.33443F));
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.33353F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(1232, 635);
            this.smartSplitTableLayoutPanel2.TabIndex = 1;
            // 
            // grdMeasuredValue2
            // 
            this.grdMeasuredValue2.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMeasuredValue2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMeasuredValue2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdMeasuredValue2.IsUsePaging = false;
            this.grdMeasuredValue2.LanguageKey = null;
            this.grdMeasuredValue2.Location = new System.Drawing.Point(616, 410);
            this.grdMeasuredValue2.Margin = new System.Windows.Forms.Padding(0);
            this.grdMeasuredValue2.Name = "grdMeasuredValue2";
            this.grdMeasuredValue2.ShowBorder = true;
            this.grdMeasuredValue2.ShowStatusBar = false;
            this.grdMeasuredValue2.Size = new System.Drawing.Size(616, 225);
            this.grdMeasuredValue2.TabIndex = 10;
            // 
            // grdInspection2
            // 
            this.grdInspection2.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdInspection2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInspection2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdInspection2.IsUsePaging = false;
            this.grdInspection2.LanguageKey = null;
            this.grdInspection2.Location = new System.Drawing.Point(0, 410);
            this.grdInspection2.Margin = new System.Windows.Forms.Padding(0);
            this.grdInspection2.Name = "grdInspection2";
            this.grdInspection2.ShowBorder = true;
            this.grdInspection2.ShowStatusBar = false;
            this.grdInspection2.Size = new System.Drawing.Size(616, 225);
            this.grdInspection2.TabIndex = 9;
            // 
            // grdManufacturingHistory
            // 
            this.grdManufacturingHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.smartSplitTableLayoutPanel2.SetColumnSpan(this.grdManufacturingHistory, 2);
            this.grdManufacturingHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdManufacturingHistory.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdManufacturingHistory.IsUsePaging = false;
            this.grdManufacturingHistory.LanguageKey = "MANUFACTURINGHISTORY";
            this.grdManufacturingHistory.Location = new System.Drawing.Point(0, 129);
            this.grdManufacturingHistory.Margin = new System.Windows.Forms.Padding(0);
            this.grdManufacturingHistory.Name = "grdManufacturingHistory";
            this.grdManufacturingHistory.ShowBorder = true;
            this.grdManufacturingHistory.ShowStatusBar = false;
            this.grdManufacturingHistory.Size = new System.Drawing.Size(1232, 281);
            this.grdManufacturingHistory.TabIndex = 6;
            // 
            // grdProductInformation2
            // 
            this.grdProductInformation2.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.smartSplitTableLayoutPanel2.SetColumnSpan(this.grdProductInformation2, 2);
            this.grdProductInformation2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductInformation2.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdProductInformation2.IsUsePaging = false;
            this.grdProductInformation2.LanguageKey = "PRODUCTINFORMATION";
            this.grdProductInformation2.Location = new System.Drawing.Point(0, 0);
            this.grdProductInformation2.Margin = new System.Windows.Forms.Padding(0);
            this.grdProductInformation2.Name = "grdProductInformation2";
            this.grdProductInformation2.ShowBorder = true;
            this.grdProductInformation2.ShowStatusBar = false;
            this.grdProductInformation2.Size = new System.Drawing.Size(1232, 129);
            this.grdProductInformation2.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 673);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1238, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(1163, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "smartButton1";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.IsBusy = false;
            this.btnSave.IsWrite = false;
            this.btnSave.LanguageKey = "SAVE";
            this.btnSave.Location = new System.Drawing.Point(1077, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "smartButton2";
            this.btnSave.TooltipLanguageKey = "";
            // 
            // ReReliabilityVerificationRequestRegularPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 725);
            this.LanguageKey = "REREGISTMODIFYRELIABVERIFIREQREG";
            this.Name = "ReReliabilityVerificationRequestRegularPopup";
            this.Text = "신뢰성 검증 재의뢰 등록/수정(정기)";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabReReliabilityVerificationRequestRegular)).EndInit();
            this.tabReReliabilityVerificationRequestRegular.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.tlpMainTable.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartTabControl tabReReliabilityVerificationRequestRegular;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartBandedGrid grdManufacturingHistory;
        private Framework.SmartControls.SmartBandedGrid grdProductInformation2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Framework.SmartControls.SmartSplitTableLayoutPanel tlpMainTable;
        private ManufacturingHistoryControl manufacturingHistoryControl1;
        private Framework.SmartControls.SmartBandedGrid grdProductInformation1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnSave;
        private Framework.SmartControls.SmartBandedGrid grdInspection2;
        private Framework.SmartControls.SmartBandedGrid grdMeasuredValue2;
    }
}