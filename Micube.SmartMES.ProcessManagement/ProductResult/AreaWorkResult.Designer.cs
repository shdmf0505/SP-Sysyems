namespace Micube.SmartMES.ProcessManagement
{
	partial class AreaWorkResult
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
            this.tabAreaWorkResult = new Micube.Framework.SmartControls.SmartTabControl();
            this.bySegmentAreaPage = new DevExpress.XtraTab.XtraTabPage();
            this.grdByArea = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.byLotPage = new DevExpress.XtraTab.XtraTabPage();
            this.grdByLot = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAreaWorkResult)).BeginInit();
            this.tabAreaWorkResult.SuspendLayout();
            this.bySegmentAreaPage.SuspendLayout();
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
            // tabAreaWorkResult
            // 
            this.tabAreaWorkResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAreaWorkResult.Location = new System.Drawing.Point(3, 31);
            this.tabAreaWorkResult.Name = "tabAreaWorkResult";
            this.tabAreaWorkResult.SelectedTabPage = this.bySegmentAreaPage;
            this.tabAreaWorkResult.Size = new System.Drawing.Size(790, 485);
            this.tabAreaWorkResult.TabIndex = 0;
            this.tabAreaWorkResult.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.bySegmentAreaPage,
            this.byLotPage});
            // 
            // bySegmentAreaPage
            // 
            this.bySegmentAreaPage.Controls.Add(this.grdByArea);
            this.tabAreaWorkResult.SetLanguageKey(this.bySegmentAreaPage, "BYSEGMENTAREA");
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
            this.grdByArea.LanguageKey = "AreaWorkResultBYAREA";
            this.grdByArea.Location = new System.Drawing.Point(0, 0);
            this.grdByArea.Margin = new System.Windows.Forms.Padding(0);
            this.grdByArea.Name = "grdByArea";
            this.grdByArea.ShowBorder = true;
            this.grdByArea.ShowStatusBar = false;
            this.grdByArea.Size = new System.Drawing.Size(784, 456);
            this.grdByArea.TabIndex = 0;
            this.grdByArea.UseAutoBestFitColumns = false;
            // 
            // byLotPage
            // 
            this.byLotPage.Controls.Add(this.grdByLot);
            this.tabAreaWorkResult.SetLanguageKey(this.byLotPage, "BYLOT");
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
            this.grdByLot.LanguageKey = "AreaWorkResultBYLOT";
            this.grdByLot.Location = new System.Drawing.Point(0, 0);
            this.grdByLot.Margin = new System.Windows.Forms.Padding(0);
            this.grdByLot.Name = "grdByLot";
            this.grdByLot.ShowBorder = true;
            this.grdByLot.ShowStatusBar = false;
            this.grdByLot.Size = new System.Drawing.Size(744, 426);
            this.grdByLot.TabIndex = 0;
            this.grdByLot.UseAutoBestFitColumns = false;
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tabAreaWorkResult, 0, 2);
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
            // AreaWorkResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 568);
            this.Name = "AreaWorkResult";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAreaWorkResult)).EndInit();
            this.tabAreaWorkResult.ResumeLayout(false);
            this.bySegmentAreaPage.ResumeLayout(false);
            this.byLotPage.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabAreaWorkResult;
		private DevExpress.XtraTab.XtraTabPage bySegmentAreaPage;
		private DevExpress.XtraTab.XtraTabPage byLotPage;
		private Framework.SmartControls.SmartBandedGrid grdByArea;
		private Framework.SmartControls.SmartBandedGrid grdByLot;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
    }
}