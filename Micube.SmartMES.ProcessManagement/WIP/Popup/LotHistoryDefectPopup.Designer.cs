namespace Micube.SmartMES.ProcessManagement
{
	partial class LotHistoryDefectPopup
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
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
            this.tabMain = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpgDefect = new DevExpress.XtraTab.XtraTabPage();
            this.grdLotDefect = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgInspect = new DevExpress.XtraTab.XtraTabPage();
            this.usInspectionResult = new Micube.SmartMES.ProcessManagement.usInspectionResult();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
            this.smartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tpgDefect.SuspendLayout();
            this.tpgInspect.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(739, 527);
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 2);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel2, 0, 0);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 3;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(739, 527);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // smartPanel1
            // 
            this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel1.Controls.Add(this.btnClose);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel1.Location = new System.Drawing.Point(0, 501);
            this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(739, 26);
            this.smartPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(661, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // smartPanel2
            // 
            this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smartPanel2.Controls.Add(this.tabMain);
            this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPanel2.Location = new System.Drawing.Point(0, 0);
            this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartPanel2.Name = "smartPanel2";
            this.smartPanel2.Size = new System.Drawing.Size(739, 498);
            this.smartPanel2.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tpgDefect;
            this.tabMain.Size = new System.Drawing.Size(739, 498);
            this.tabMain.TabIndex = 3;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpgDefect,
            this.tpgInspect});
            // 
            // tpgDefect
            // 
            this.tpgDefect.Controls.Add(this.grdLotDefect);
            this.tabMain.SetLanguageKey(this.tpgDefect, "DEFECT");
            this.tpgDefect.Name = "tpgDefect";
            this.tpgDefect.Padding = new System.Windows.Forms.Padding(3);
            this.tpgDefect.Size = new System.Drawing.Size(733, 469);
            this.tpgDefect.Text = "Defect";
            // 
            // grdLotDefect
            // 
            this.grdLotDefect.Caption = "";
            this.grdLotDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLotDefect.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdLotDefect.IsUsePaging = false;
            this.grdLotDefect.LanguageKey = null;
            this.grdLotDefect.Location = new System.Drawing.Point(3, 3);
            this.grdLotDefect.Margin = new System.Windows.Forms.Padding(0);
            this.grdLotDefect.Name = "grdLotDefect";
            this.grdLotDefect.ShowBorder = true;
            this.grdLotDefect.ShowStatusBar = false;
            this.grdLotDefect.Size = new System.Drawing.Size(727, 463);
            this.grdLotDefect.TabIndex = 2;
            this.grdLotDefect.UseAutoBestFitColumns = false;
            // 
            // tpgInspect
            // 
            this.tpgInspect.Controls.Add(this.usInspectionResult);
            this.tabMain.SetLanguageKey(this.tpgInspect, "INSPECT");
            this.tpgInspect.Name = "tpgInspect";
            this.tpgInspect.Padding = new System.Windows.Forms.Padding(3);
            this.tpgInspect.Size = new System.Drawing.Size(733, 469);
            this.tpgInspect.Text = "검사";
            // 
            // usInspectionResult
            // 
            this.usInspectionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usInspectionResult.InspectionData = null;
            this.usInspectionResult.Location = new System.Drawing.Point(3, 3);
            this.usInspectionResult.LotID = null;
            this.usInspectionResult.Name = "usInspectionResult";
            this.usInspectionResult.Size = new System.Drawing.Size(727, 463);
            this.usInspectionResult.TabIndex = 0;
            // 
            // LotHistoryDefectPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 547);
            this.Name = "LotHistoryDefectPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lot Defect History";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
            this.smartPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tpgDefect.ResumeLayout(false);
            this.tpgInspect.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartPanel smartPanel1;
		private Framework.SmartControls.SmartButton btnClose;
		private Framework.SmartControls.SmartPanel smartPanel2;
		private Framework.SmartControls.SmartBandedGrid grdLotDefect;
        private Framework.SmartControls.SmartTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tpgDefect;
        private DevExpress.XtraTab.XtraTabPage tpgInspect;
        private usInspectionResult usInspectionResult;
    }
}