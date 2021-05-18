namespace Micube.SmartMES.ProductManagement
{
	partial class ProcessTimePerPlan
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
            this.grdTackTime = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.tplTackTime = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.tabTactTime = new Micube.Framework.SmartControls.SmartTabControl();
            this.tabpageTactTime = new DevExpress.XtraTab.XtraTabPage();
            this.tabApply = new DevExpress.XtraTab.XtraTabPage();
            this.grdApply = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.tplTackTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabTactTime)).BeginInit();
            this.tabTactTime.SuspendLayout();
            this.tabpageTactTime.SuspendLayout();
            this.tabApply.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 527);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(649, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tplTackTime);
            this.pnlContent.Size = new System.Drawing.Size(649, 531);
            // 
            // pnlMain
            // 
            this.pnlMain.Size = new System.Drawing.Size(954, 560);
            // 
            // grdTackTime
            // 
            this.grdTackTime.Caption = "";
            this.grdTackTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTackTime.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdTackTime.IsUsePaging = false;
            this.grdTackTime.LanguageKey = "";
            this.grdTackTime.Location = new System.Drawing.Point(0, 0);
            this.grdTackTime.Margin = new System.Windows.Forms.Padding(0);
            this.grdTackTime.Name = "grdTackTime";
            this.grdTackTime.ShowBorder = true;
            this.grdTackTime.Size = new System.Drawing.Size(637, 496);
            this.grdTackTime.TabIndex = 0;
            // 
            // tplTackTime
            // 
            this.tplTackTime.ColumnCount = 1;
            this.tplTackTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplTackTime.Controls.Add(this.tabTactTime, 0, 0);
            this.tplTackTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplTackTime.Location = new System.Drawing.Point(0, 0);
            this.tplTackTime.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tplTackTime.Name = "tplTackTime";
            this.tplTackTime.RowCount = 1;
            this.tplTackTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplTackTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplTackTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplTackTime.Size = new System.Drawing.Size(649, 531);
            this.tplTackTime.TabIndex = 1;
            // 
            // tabTactTime
            // 
            this.tabTactTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTactTime.Location = new System.Drawing.Point(3, 3);
            this.tabTactTime.Name = "tabTactTime";
            this.tabTactTime.SelectedTabPage = this.tabpageTactTime;
            this.tabTactTime.Size = new System.Drawing.Size(643, 525);
            this.tabTactTime.TabIndex = 0;
            this.tabTactTime.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabpageTactTime,
            this.tabApply});
            // 
            // tabpageTactTime
            // 
            this.tabpageTactTime.Controls.Add(this.grdTackTime);
            this.tabTactTime.SetLanguageKey(this.tabpageTactTime, "TACKTIMELIST");
            this.tabpageTactTime.Name = "tabpageTactTime";
            this.tabpageTactTime.Size = new System.Drawing.Size(637, 496);
            this.tabpageTactTime.Text = "xtraTabPage1";
            // 
            // tabApply
            // 
            this.tabApply.Controls.Add(this.grdApply);
            this.tabTactTime.SetLanguageKey(this.tabApply, "STDTACTTIMEAPPLY");
            this.tabApply.Name = "tabApply";
            this.tabApply.Size = new System.Drawing.Size(637, 496);
            this.tabApply.Text = "xtraTabPage2";
            // 
            // grdApply
            // 
            this.grdApply.Caption = "";
            this.grdApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdApply.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdApply.IsUsePaging = false;
            this.grdApply.LanguageKey = "";
            this.grdApply.Location = new System.Drawing.Point(0, 0);
            this.grdApply.Margin = new System.Windows.Forms.Padding(0);
            this.grdApply.Name = "grdApply";
            this.grdApply.ShowBorder = true;
            this.grdApply.ShowStatusBar = false;
            this.grdApply.Size = new System.Drawing.Size(637, 496);
            this.grdApply.TabIndex = 1;
            // 
            // ProcessTimePerPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 580);
            this.Name = "ProcessTimePerPlan";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.tplTackTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabTactTime)).EndInit();
            this.tabTactTime.ResumeLayout(false);
            this.tabpageTactTime.ResumeLayout(false);
            this.tabApply.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel tplTackTime;
        private Framework.SmartControls.SmartTabControl tabTactTime;
        private DevExpress.XtraTab.XtraTabPage tabpageTactTime;
        private Framework.SmartControls.SmartBandedGrid grdTackTime;
        private DevExpress.XtraTab.XtraTabPage tabApply;
        private Framework.SmartControls.SmartBandedGrid grdApply;
    }
}