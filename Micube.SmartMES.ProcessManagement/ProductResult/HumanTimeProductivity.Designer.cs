namespace Micube.SmartMES.ProcessManagement
{
	partial class HumanTimeProductivity
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
            this.tabHumanTimeProductivity = new Micube.Framework.SmartControls.SmartTabControl();
            this.tpbHumanTime = new DevExpress.XtraTab.XtraTabPage();
            this.grdHumanTimeDetailList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgSegment = new DevExpress.XtraTab.XtraTabPage();
            this.grdDailySegment = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgArea = new DevExpress.XtraTab.XtraTabPage();
            this.grdDailyVendor = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpgManual = new DevExpress.XtraTab.XtraTabPage();
            this.grdExcelUpLoad = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabHumanTimeProductivity)).BeginInit();
            this.tabHumanTimeProductivity.SuspendLayout();
            this.tpbHumanTime.SuspendLayout();
            this.tpgSegment.SuspendLayout();
            this.tpgArea.SuspendLayout();
            this.tpgManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 397);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(659, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabHumanTimeProductivity);
            this.pnlContent.Size = new System.Drawing.Size(659, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(964, 430);
            // 
            // tabHumanTimeProductivity
            // 
            this.tabHumanTimeProductivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHumanTimeProductivity.Location = new System.Drawing.Point(0, 0);
            this.tabHumanTimeProductivity.Name = "tabHumanTimeProductivity";
            this.tabHumanTimeProductivity.SelectedTabPage = this.tpbHumanTime;
            this.tabHumanTimeProductivity.Size = new System.Drawing.Size(659, 401);
            this.tabHumanTimeProductivity.TabIndex = 0;
            this.tabHumanTimeProductivity.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpbHumanTime,
            this.tpgSegment,
            this.tpgArea,
            this.tpgManual});
            // 
            // tpbHumanTime
            // 
            this.tpbHumanTime.Controls.Add(this.grdHumanTimeDetailList);
            this.tpbHumanTime.Name = "tpbHumanTime";
            this.tpbHumanTime.Padding = new System.Windows.Forms.Padding(3);
            this.tpbHumanTime.Size = new System.Drawing.Size(653, 372);
            this.tpbHumanTime.Text = "인시당 상세";
            // 
            // grdHumanTimeDetailList
            // 
            this.grdHumanTimeDetailList.Caption = "";
            this.grdHumanTimeDetailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHumanTimeDetailList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdHumanTimeDetailList.IsUsePaging = false;
            this.grdHumanTimeDetailList.LanguageKey = null;
            this.grdHumanTimeDetailList.Location = new System.Drawing.Point(3, 3);
            this.grdHumanTimeDetailList.Margin = new System.Windows.Forms.Padding(0);
            this.grdHumanTimeDetailList.Name = "grdHumanTimeDetailList";
            this.grdHumanTimeDetailList.ShowBorder = true;
            this.grdHumanTimeDetailList.ShowStatusBar = false;
            this.grdHumanTimeDetailList.Size = new System.Drawing.Size(647, 366);
            this.grdHumanTimeDetailList.TabIndex = 0;
            this.grdHumanTimeDetailList.UseAutoBestFitColumns = false;
            // 
            // tpgSegment
            // 
            this.tpgSegment.Controls.Add(this.grdDailySegment);
            this.tabHumanTimeProductivity.SetLanguageKey(this.tpgSegment, "BYPROCESSSEG");
            this.tpgSegment.Name = "tpgSegment";
            this.tpgSegment.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSegment.Size = new System.Drawing.Size(750, 460);
            this.tpgSegment.Text = "공정별";
            // 
            // grdDailySegment
            // 
            this.grdDailySegment.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDailySegment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDailySegment.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDailySegment.IsUsePaging = false;
            this.grdDailySegment.LanguageKey = "BYPROCESSSEG";
            this.grdDailySegment.Location = new System.Drawing.Point(3, 3);
            this.grdDailySegment.Margin = new System.Windows.Forms.Padding(0);
            this.grdDailySegment.Name = "grdDailySegment";
            this.grdDailySegment.ShowBorder = true;
            this.grdDailySegment.ShowStatusBar = false;
            this.grdDailySegment.Size = new System.Drawing.Size(744, 454);
            this.grdDailySegment.TabIndex = 0;
            this.grdDailySegment.UseAutoBestFitColumns = false;
            // 
            // tpgArea
            // 
            this.tpgArea.Controls.Add(this.grdDailyVendor);
            this.tabHumanTimeProductivity.SetLanguageKey(this.tpgArea, "BYAREA");
            this.tpgArea.Name = "tpgArea";
            this.tpgArea.Padding = new System.Windows.Forms.Padding(3);
            this.tpgArea.Size = new System.Drawing.Size(750, 460);
            this.tpgArea.Text = "작업장별";
            // 
            // grdDailyVendor
            // 
            this.grdDailyVendor.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdDailyVendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDailyVendor.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdDailyVendor.IsUsePaging = false;
            this.grdDailyVendor.LanguageKey = "BYAREA";
            this.grdDailyVendor.Location = new System.Drawing.Point(3, 3);
            this.grdDailyVendor.Margin = new System.Windows.Forms.Padding(0);
            this.grdDailyVendor.Name = "grdDailyVendor";
            this.grdDailyVendor.ShowBorder = true;
            this.grdDailyVendor.ShowStatusBar = false;
            this.grdDailyVendor.Size = new System.Drawing.Size(744, 454);
            this.grdDailyVendor.TabIndex = 0;
            this.grdDailyVendor.UseAutoBestFitColumns = false;
            // 
            // tpgManual
            // 
            this.tpgManual.Controls.Add(this.grdExcelUpLoad);
            this.tabHumanTimeProductivity.SetLanguageKey(this.tpgManual, "UPLOADEXCEL");
            this.tpgManual.Name = "tpgManual";
            this.tpgManual.Padding = new System.Windows.Forms.Padding(3);
            this.tpgManual.Size = new System.Drawing.Size(653, 372);
            this.tpgManual.Text = "엑셀업로드";
            // 
            // grdExcelUpLoad
            // 
            this.grdExcelUpLoad.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdExcelUpLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdExcelUpLoad.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdExcelUpLoad.IsUsePaging = false;
            this.grdExcelUpLoad.LanguageKey = "UPLOADEXCEL";
            this.grdExcelUpLoad.Location = new System.Drawing.Point(3, 3);
            this.grdExcelUpLoad.Margin = new System.Windows.Forms.Padding(0);
            this.grdExcelUpLoad.Name = "grdExcelUpLoad";
            this.grdExcelUpLoad.ShowBorder = true;
            this.grdExcelUpLoad.ShowStatusBar = false;
            this.grdExcelUpLoad.Size = new System.Drawing.Size(647, 366);
            this.grdExcelUpLoad.TabIndex = 1;
            this.grdExcelUpLoad.UseAutoBestFitColumns = false;
            // 
            // HumanTimeProductivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 450);
            this.Name = "HumanTimeProductivity";
            this.Text = "인시생산성";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabHumanTimeProductivity)).EndInit();
            this.tabHumanTimeProductivity.ResumeLayout(false);
            this.tpbHumanTime.ResumeLayout(false);
            this.tpgSegment.ResumeLayout(false);
            this.tpgArea.ResumeLayout(false);
            this.tpgManual.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabHumanTimeProductivity;
		private DevExpress.XtraTab.XtraTabPage tpbHumanTime;
		private DevExpress.XtraTab.XtraTabPage tpgSegment;
		private DevExpress.XtraTab.XtraTabPage tpgArea;
		private Framework.SmartControls.SmartBandedGrid grdHumanTimeDetailList;
		private Framework.SmartControls.SmartBandedGrid grdDailySegment;
		private Framework.SmartControls.SmartBandedGrid grdDailyVendor;
        private DevExpress.XtraTab.XtraTabPage tpgManual;
        private Framework.SmartControls.SmartBandedGrid grdExcelUpLoad;
    }
}