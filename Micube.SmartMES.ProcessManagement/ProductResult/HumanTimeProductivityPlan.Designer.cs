namespace Micube.SmartMES.ProcessManagement
{
	partial class HumanTimeProductivityPlan
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
            this.tpgManual = new DevExpress.XtraTab.XtraTabPage();
            this.grdExcelUpLoad = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tpbHumanTime = new DevExpress.XtraTab.XtraTabPage();
            this.grdPlan = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tabPlan = new Micube.Framework.SmartControls.SmartTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tpgManual.SuspendLayout();
            this.tpbHumanTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPlan)).BeginInit();
            this.tabPlan.SuspendLayout();
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
            this.pnlContent.Controls.Add(this.tabPlan);
            this.pnlContent.Size = new System.Drawing.Size(659, 401);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(964, 430);
            // 
            // tpgManual
            // 
            this.tpgManual.Controls.Add(this.grdExcelUpLoad);
            this.tabPlan.SetLanguageKey(this.tpgManual, "UPLOADEXCEL");
            this.tpgManual.Name = "tpgManual";
            this.tpgManual.Padding = new System.Windows.Forms.Padding(3);
            this.tpgManual.Size = new System.Drawing.Size(750, 460);
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
            this.grdExcelUpLoad.Size = new System.Drawing.Size(744, 454);
            this.grdExcelUpLoad.TabIndex = 1;
            this.grdExcelUpLoad.UseAutoBestFitColumns = false;
            // 
            // tpbHumanTime
            // 
            this.tpbHumanTime.Controls.Add(this.grdPlan);
            this.tabPlan.SetLanguageKey(this.tpbHumanTime, "HUMANTIMEPLAN");
            this.tpbHumanTime.Name = "tpbHumanTime";
            this.tpbHumanTime.Padding = new System.Windows.Forms.Padding(3);
            this.tpbHumanTime.Size = new System.Drawing.Size(653, 372);
            this.tpbHumanTime.Text = "인시생산성 계획";
            // 
            // grdPlan
            // 
            this.grdPlan.Caption = "";
            this.grdPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPlan.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPlan.IsUsePaging = false;
            this.grdPlan.LanguageKey = null;
            this.grdPlan.Location = new System.Drawing.Point(3, 3);
            this.grdPlan.Margin = new System.Windows.Forms.Padding(0);
            this.grdPlan.Name = "grdPlan";
            this.grdPlan.ShowBorder = true;
            this.grdPlan.ShowStatusBar = false;
            this.grdPlan.Size = new System.Drawing.Size(647, 366);
            this.grdPlan.TabIndex = 0;
            this.grdPlan.UseAutoBestFitColumns = false;
            // 
            // tabPlan
            // 
            this.tabPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPlan.Location = new System.Drawing.Point(0, 0);
            this.tabPlan.Name = "tabPlan";
            this.tabPlan.SelectedTabPage = this.tpbHumanTime;
            this.tabPlan.Size = new System.Drawing.Size(659, 401);
            this.tabPlan.TabIndex = 0;
            this.tabPlan.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpbHumanTime,
            this.tpgManual});
            // 
            // HumanTimeProductivityPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 450);
            this.Name = "HumanTimeProductivityPlan";
            this.Text = "인시생산성";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tpgManual.ResumeLayout(false);
            this.tpbHumanTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabPlan)).EndInit();
            this.tabPlan.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private Framework.SmartControls.SmartTabControl tabPlan;
        private DevExpress.XtraTab.XtraTabPage tpbHumanTime;
        private Framework.SmartControls.SmartBandedGrid grdPlan;
        private DevExpress.XtraTab.XtraTabPage tpgManual;
        private Framework.SmartControls.SmartBandedGrid grdExcelUpLoad;
    }
}