namespace Micube.SmartMES.StandardInfo
{
	partial class UOMManagement
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
			this.tabPartition = new Micube.Framework.SmartControls.SmartTabControl();
			this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
			this.grdMainUOMClassList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
			this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
			this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
			this.grdUOMClassList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.grdUOMDefinitionList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterContainer4 = new Micube.Framework.SmartControls.SmartSpliterContainer();
			this.ucDataUpDownBtn = new Micube.SmartMES.Commons.Controls.ucDataUpDownBtn();
			this.smartSpliterContainer3 = new Micube.Framework.SmartControls.SmartSpliterContainer();
			this.grdMapList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.grdMapSave = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPartition)).BeginInit();
			this.tabPartition.SuspendLayout();
			this.xtraTabPage1.SuspendLayout();
			this.xtraTabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
			this.smartSpliterContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
			this.smartSpliterContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer4)).BeginInit();
			this.smartSpliterContainer4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).BeginInit();
			this.smartSpliterContainer3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
			this.pnlCondition.Size = new System.Drawing.Size(296, 571);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(961, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.tabPartition);
			this.pnlContent.Size = new System.Drawing.Size(961, 575);
			// 
			// pnlMain
			// 
			this.pnlMain.Location = new System.Drawing.Point(19, 19);
			this.pnlMain.Size = new System.Drawing.Size(1266, 604);
			// 
			// tabPartition
			// 
			this.tabPartition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabPartition.Location = new System.Drawing.Point(0, 0);
			this.tabPartition.Name = "tabPartition";
			this.tabPartition.SelectedTabPage = this.xtraTabPage1;
			this.tabPartition.Size = new System.Drawing.Size(961, 575);
			this.tabPartition.TabIndex = 2;
			this.tabPartition.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
			// 
			// xtraTabPage1
			// 
			this.xtraTabPage1.Controls.Add(this.grdMainUOMClassList);
			this.xtraTabPage1.Name = "xtraTabPage1";
			this.xtraTabPage1.Size = new System.Drawing.Size(955, 546);
			this.xtraTabPage1.Text = "xtraTabPage1";
			// 
			// grdMainUOMClassList
			// 
			this.grdMainUOMClassList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdMainUOMClassList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMainUOMClassList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdMainUOMClassList.IsUsePaging = false;
			this.grdMainUOMClassList.LanguageKey = "UOMCLASSLIST";
			this.grdMainUOMClassList.Location = new System.Drawing.Point(0, 0);
			this.grdMainUOMClassList.Margin = new System.Windows.Forms.Padding(0);
			this.grdMainUOMClassList.Name = "grdMainUOMClassList";
			this.grdMainUOMClassList.ShowBorder = true;
			this.grdMainUOMClassList.ShowStatusBar = false;
			this.grdMainUOMClassList.Size = new System.Drawing.Size(955, 546);
			this.grdMainUOMClassList.TabIndex = 0;
			this.grdMainUOMClassList.UseAutoBestFitColumns = false;
			// 
			// xtraTabPage2
			// 
			this.xtraTabPage2.Controls.Add(this.smartSpliterContainer2);
			this.xtraTabPage2.Controls.Add(this.smartSpliterControl1);
			this.xtraTabPage2.Name = "xtraTabPage2";
			this.xtraTabPage2.Size = new System.Drawing.Size(955, 546);
			this.xtraTabPage2.Text = "xtraTabPage2";
			// 
			// smartSpliterContainer2
			// 
			this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSpliterContainer2.Horizontal = false;
			this.smartSpliterContainer2.Location = new System.Drawing.Point(5, 0);
			this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterContainer2.Name = "smartSpliterContainer2";
			this.smartSpliterContainer2.Panel1.Controls.Add(this.smartSpliterContainer1);
			this.smartSpliterContainer2.Panel1.Text = "Panel1";
			this.smartSpliterContainer2.Panel2.Controls.Add(this.smartSpliterContainer4);
			this.smartSpliterContainer2.Panel2.Text = "Panel2";
			this.smartSpliterContainer2.Size = new System.Drawing.Size(950, 546);
			this.smartSpliterContainer2.SplitterPosition = 263;
			this.smartSpliterContainer2.TabIndex = 3;
			this.smartSpliterContainer2.Text = "smartSpliterContainer2";
			// 
			// smartSpliterContainer1
			// 
			this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
			this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterContainer1.Name = "smartSpliterContainer1";
			this.smartSpliterContainer1.Panel1.Controls.Add(this.grdUOMClassList);
			this.smartSpliterContainer1.Panel1.Text = "Panel1";
			this.smartSpliterContainer1.Panel2.Controls.Add(this.grdUOMDefinitionList);
			this.smartSpliterContainer1.Panel2.Text = "Panel2";
			this.smartSpliterContainer1.Size = new System.Drawing.Size(950, 263);
			this.smartSpliterContainer1.SplitterPosition = 415;
			this.smartSpliterContainer1.TabIndex = 1;
			this.smartSpliterContainer1.Text = "smartSpliterContainer1";
			// 
			// grdUOMClassList
			// 
			this.grdUOMClassList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdUOMClassList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdUOMClassList.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Refresh;
			this.grdUOMClassList.IsUsePaging = false;
			this.grdUOMClassList.LanguageKey = "UOMCLASSLIST";
			this.grdUOMClassList.Location = new System.Drawing.Point(0, 0);
			this.grdUOMClassList.Margin = new System.Windows.Forms.Padding(0);
			this.grdUOMClassList.Name = "grdUOMClassList";
			this.grdUOMClassList.ShowBorder = true;
			this.grdUOMClassList.ShowStatusBar = false;
			this.grdUOMClassList.Size = new System.Drawing.Size(415, 263);
			this.grdUOMClassList.TabIndex = 0;
			this.grdUOMClassList.UseAutoBestFitColumns = false;
			// 
			// grdUOMDefinitionList
			// 
			this.grdUOMDefinitionList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdUOMDefinitionList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdUOMDefinitionList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdUOMDefinitionList.IsUsePaging = false;
			this.grdUOMDefinitionList.LanguageKey = "UOMDEFLIST";
			this.grdUOMDefinitionList.Location = new System.Drawing.Point(0, 0);
			this.grdUOMDefinitionList.Margin = new System.Windows.Forms.Padding(0);
			this.grdUOMDefinitionList.Name = "grdUOMDefinitionList";
			this.grdUOMDefinitionList.ShowBorder = true;
			this.grdUOMDefinitionList.ShowStatusBar = false;
			this.grdUOMDefinitionList.Size = new System.Drawing.Size(530, 263);
			this.grdUOMDefinitionList.TabIndex = 0;
			this.grdUOMDefinitionList.UseAutoBestFitColumns = false;
			// 
			// smartSpliterContainer4
			// 
			this.smartSpliterContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSpliterContainer4.Horizontal = false;
			this.smartSpliterContainer4.IsSplitterFixed = true;
			this.smartSpliterContainer4.Location = new System.Drawing.Point(0, 0);
			this.smartSpliterContainer4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterContainer4.Name = "smartSpliterContainer4";
			this.smartSpliterContainer4.Panel1.Controls.Add(this.ucDataUpDownBtn);
			this.smartSpliterContainer4.Panel1.Text = "Panel1";
			this.smartSpliterContainer4.Panel2.Controls.Add(this.smartSpliterContainer3);
			this.smartSpliterContainer4.Panel2.Text = "Panel2";
			this.smartSpliterContainer4.Size = new System.Drawing.Size(950, 278);
			this.smartSpliterContainer4.SplitterPosition = 38;
			this.smartSpliterContainer4.TabIndex = 2;
			this.smartSpliterContainer4.Text = "smartSpliterContainer4";
			// 
			// ucDataUpDownBtn
			// 
			this.ucDataUpDownBtn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDataUpDownBtn.Location = new System.Drawing.Point(0, 0);
			this.ucDataUpDownBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ucDataUpDownBtn.Name = "ucDataUpDownBtn";
			this.ucDataUpDownBtn.Size = new System.Drawing.Size(950, 38);
			this.ucDataUpDownBtn.SourceGrid = null;
			this.ucDataUpDownBtn.TabIndex = 1;
			this.ucDataUpDownBtn.TargetGrid = null;
			// 
			// smartSpliterContainer3
			// 
			this.smartSpliterContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSpliterContainer3.Location = new System.Drawing.Point(0, 0);
			this.smartSpliterContainer3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterContainer3.Name = "smartSpliterContainer3";
			this.smartSpliterContainer3.Panel1.Controls.Add(this.grdMapList);
			this.smartSpliterContainer3.Panel1.Text = "Panel1";
			this.smartSpliterContainer3.Panel2.Controls.Add(this.grdMapSave);
			this.smartSpliterContainer3.Panel2.Text = "Panel2";
			this.smartSpliterContainer3.Size = new System.Drawing.Size(950, 235);
			this.smartSpliterContainer3.SplitterPosition = 414;
			this.smartSpliterContainer3.TabIndex = 1;
			this.smartSpliterContainer3.Text = "smartSpliterContainer3";
			// 
			// grdMapList
			// 
			this.grdMapList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdMapList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMapList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdMapList.IsUsePaging = false;
			this.grdMapList.LanguageKey = "UOMMAP";
			this.grdMapList.Location = new System.Drawing.Point(0, 0);
			this.grdMapList.Margin = new System.Windows.Forms.Padding(0);
			this.grdMapList.Name = "grdMapList";
			this.grdMapList.ShowBorder = true;
			this.grdMapList.ShowStatusBar = false;
			this.grdMapList.Size = new System.Drawing.Size(414, 235);
			this.grdMapList.TabIndex = 0;
			this.grdMapList.UseAutoBestFitColumns = false;
			// 
			// grdMapSave
			// 
			this.grdMapSave.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdMapSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMapSave.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdMapSave.IsUsePaging = false;
			this.grdMapSave.LanguageKey = "UOMDEFLIST";
			this.grdMapSave.Location = new System.Drawing.Point(0, 0);
			this.grdMapSave.Margin = new System.Windows.Forms.Padding(0);
			this.grdMapSave.Name = "grdMapSave";
			this.grdMapSave.ShowBorder = true;
			this.grdMapSave.ShowStatusBar = false;
			this.grdMapSave.Size = new System.Drawing.Size(531, 235);
			this.grdMapSave.TabIndex = 0;
			this.grdMapSave.UseAutoBestFitColumns = false;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Location = new System.Drawing.Point(0, 0);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(5, 546);
			this.smartSpliterControl1.TabIndex = 2;
			this.smartSpliterControl1.TabStop = false;
			// 
			// UOMManagement
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1304, 642);
			this.Name = "UOMManagement";
			this.Padding = new System.Windows.Forms.Padding(19);
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPartition)).EndInit();
			this.tabPartition.ResumeLayout(false);
			this.xtraTabPage1.ResumeLayout(false);
			this.xtraTabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
			this.smartSpliterContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
			this.smartSpliterContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer4)).EndInit();
			this.smartSpliterContainer4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer3)).EndInit();
			this.smartSpliterContainer3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Framework.SmartControls.SmartTabControl tabPartition;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private Framework.SmartControls.SmartBandedGrid grdMainUOMClassList;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdUOMClassList;
        private Framework.SmartControls.SmartBandedGrid grdUOMDefinitionList;
        private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer4;
        private Commons.Controls.ucDataUpDownBtn ucDataUpDownBtn;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer3;
        private Framework.SmartControls.SmartBandedGrid grdMapList;
        private Framework.SmartControls.SmartBandedGrid grdMapSave;
    }
}