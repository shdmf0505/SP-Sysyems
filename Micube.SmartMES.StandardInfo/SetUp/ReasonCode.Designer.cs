namespace Micube.SmartMES.StandardInfo
{
	partial class ReasonCode
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
            this.grdMainReasonCodeClassList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdReasonCodeClassList = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdReasonCodeList = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).BeginInit();
            this.tabPartition.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 373);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(381, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabPartition);
            this.pnlContent.Size = new System.Drawing.Size(381, 376);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(762, 412);
            // 
            // tabPartition
            // 
            this.tabPartition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPartition.Location = new System.Drawing.Point(0, 0);
            this.tabPartition.Name = "tabPartition";
            this.tabPartition.SelectedTabPage = this.xtraTabPage1;
            this.tabPartition.Size = new System.Drawing.Size(381, 376);
            this.tabPartition.TabIndex = 0;
            this.tabPartition.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdMainReasonCodeClassList);
            this.tabPartition.SetLanguageKey(this.xtraTabPage1, "REASONCODECLASS");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(374, 340);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // grdMainReasonCodeClassList
            // 
            this.grdMainReasonCodeClassList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdMainReasonCodeClassList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMainReasonCodeClassList.IsUsePaging = false;
            this.grdMainReasonCodeClassList.LanguageKey = "REASONCODECLASSLIST";
            this.grdMainReasonCodeClassList.Location = new System.Drawing.Point(0, 0);
            this.grdMainReasonCodeClassList.Margin = new System.Windows.Forms.Padding(0);
            this.grdMainReasonCodeClassList.Name = "grdMainReasonCodeClassList";
            this.grdMainReasonCodeClassList.ShowBorder = true;
            this.grdMainReasonCodeClassList.ShowStatusBar = false;
            this.grdMainReasonCodeClassList.Size = new System.Drawing.Size(374, 340);
            this.grdMainReasonCodeClassList.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.smartSpliterContainer1);
            this.tabPartition.SetLanguageKey(this.xtraTabPage2, "REASONCODE");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(374, 340);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdReasonCodeClassList);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdReasonCodeList);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(374, 340);
            this.smartSpliterContainer1.SplitterPosition = 400;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdReasonCodeClassList
            // 
            this.grdReasonCodeClassList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReasonCodeClassList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReasonCodeClassList.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.Refresh;
            this.grdReasonCodeClassList.IsUsePaging = false;
            this.grdReasonCodeClassList.LanguageKey = "REASONCODECLASSLIST";
            this.grdReasonCodeClassList.Location = new System.Drawing.Point(0, 0);
            this.grdReasonCodeClassList.Margin = new System.Windows.Forms.Padding(0);
            this.grdReasonCodeClassList.Name = "grdReasonCodeClassList";
            this.grdReasonCodeClassList.ShowBorder = true;
            this.grdReasonCodeClassList.ShowStatusBar = false;
            this.grdReasonCodeClassList.Size = new System.Drawing.Size(368, 340);
            this.grdReasonCodeClassList.TabIndex = 0;
            // 
            // grdReasonCodeList
            // 
            this.grdReasonCodeList.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdReasonCodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReasonCodeList.IsUsePaging = false;
            this.grdReasonCodeList.LanguageKey = "REASONCODELIST";
            this.grdReasonCodeList.Location = new System.Drawing.Point(0, 0);
            this.grdReasonCodeList.Margin = new System.Windows.Forms.Padding(0);
            this.grdReasonCodeList.Name = "grdReasonCodeList";
            this.grdReasonCodeList.ShowBorder = true;
            this.grdReasonCodeList.ShowStatusBar = false;
            this.grdReasonCodeList.Size = new System.Drawing.Size(0, 0);
            this.grdReasonCodeList.TabIndex = 0;
            // 
            // ReasonCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ReasonCode";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).EndInit();
            this.tabPartition.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabPartition;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private Framework.SmartControls.SmartBandedGrid grdMainReasonCodeClassList;
		private Framework.SmartControls.SmartBandedGrid grdReasonCodeList;
		private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
		private Framework.SmartControls.SmartBandedGrid grdReasonCodeClassList;
	}
}