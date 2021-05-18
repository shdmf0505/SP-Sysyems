namespace Micube.SmartMES.ProductManagement
{
	partial class LoadPredictionStdInfo
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
			this.tabPartion = new Micube.Framework.SmartControls.SmartTabControl();
			this.loadStandardPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdLoadStandardList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.standardSegmentMappingPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdStandardSegmentMapList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.topSegmentGroupPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdTopSegmentGroupList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.middleSegmentGroupPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdMiddleSegmentGroupList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smallSegmentGroupPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdSmallSegmentGroupList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.ownerMappingPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdOwnerMappingList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPartion)).BeginInit();
			this.tabPartion.SuspendLayout();
			this.loadStandardPage.SuspendLayout();
			this.standardSegmentMappingPage.SuspendLayout();
			this.topSegmentGroupPage.SuspendLayout();
			this.middleSegmentGroupPage.SuspendLayout();
			this.smallSegmentGroupPage.SuspendLayout();
			this.ownerMappingPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 397);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.tabPartion);
			// 
			// tabPartion
			// 
			this.tabPartion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabPartion.Location = new System.Drawing.Point(0, 0);
			this.tabPartion.Name = "tabPartion";
			this.tabPartion.SelectedTabPage = this.loadStandardPage;
			this.tabPartion.Size = new System.Drawing.Size(475, 401);
			this.tabPartion.TabIndex = 0;
			this.tabPartion.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.loadStandardPage,
            this.standardSegmentMappingPage,
            this.topSegmentGroupPage,
            this.middleSegmentGroupPage,
            this.smallSegmentGroupPage,
            this.ownerMappingPage});
			// 
			// loadStandardPage
			// 
			this.loadStandardPage.Controls.Add(this.grdLoadStandardList);
			this.tabPartion.SetLanguageKey(this.loadStandardPage, "LOADSTANDARD");
			this.loadStandardPage.Name = "loadStandardPage";
			this.loadStandardPage.Size = new System.Drawing.Size(469, 372);
			this.loadStandardPage.Text = "부하량 기준정보";
			// 
			// grdLoadStandardList
			// 
			this.grdLoadStandardList.Caption = "";
			this.grdLoadStandardList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLoadStandardList.IsUsePaging = false;
			this.grdLoadStandardList.LanguageKey = "LOADSTANDARDLIST";
			this.grdLoadStandardList.Location = new System.Drawing.Point(0, 0);
			this.grdLoadStandardList.Margin = new System.Windows.Forms.Padding(0);
			this.grdLoadStandardList.Name = "grdLoadStandardList";
			this.grdLoadStandardList.ShowBorder = true;
			this.grdLoadStandardList.Size = new System.Drawing.Size(469, 372);
			this.grdLoadStandardList.TabIndex = 0;
			// 
			// standardSegmentMappingPage
			// 
			this.standardSegmentMappingPage.Controls.Add(this.grdStandardSegmentMapList);
			this.tabPartion.SetLanguageKey(this.standardSegmentMappingPage, "STANDARDSEGMENTMAPPING");
			this.standardSegmentMappingPage.Name = "standardSegmentMappingPage";
			this.standardSegmentMappingPage.Size = new System.Drawing.Size(469, 372);
			this.standardSegmentMappingPage.Text = "표준공정 맵핑";
			// 
			// grdStandardSegmentMapList
			// 
			this.grdStandardSegmentMapList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdStandardSegmentMapList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdStandardSegmentMapList.IsUsePaging = false;
			this.grdStandardSegmentMapList.LanguageKey = "STANDARDSEGMENTLIST";
			this.grdStandardSegmentMapList.Location = new System.Drawing.Point(0, 0);
			this.grdStandardSegmentMapList.Margin = new System.Windows.Forms.Padding(0);
			this.grdStandardSegmentMapList.Name = "grdStandardSegmentMapList";
			this.grdStandardSegmentMapList.ShowBorder = true;
			this.grdStandardSegmentMapList.ShowStatusBar = false;
			this.grdStandardSegmentMapList.Size = new System.Drawing.Size(469, 372);
			this.grdStandardSegmentMapList.TabIndex = 0;
			// 
			// topSegmentGroupPage
			// 
			this.topSegmentGroupPage.Controls.Add(this.grdTopSegmentGroupList);
			this.tabPartion.SetLanguageKey(this.topSegmentGroupPage, "TOPSEGMENTGROUP");
			this.topSegmentGroupPage.Name = "topSegmentGroupPage";
			this.topSegmentGroupPage.Size = new System.Drawing.Size(469, 372);
			this.topSegmentGroupPage.Text = "대공정그룹";
			// 
			// grdTopSegmentGroupList
			// 
			this.grdTopSegmentGroupList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdTopSegmentGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdTopSegmentGroupList.IsUsePaging = false;
			this.grdTopSegmentGroupList.LanguageKey = "TOPLOADSEGMENTGROUPLIST";
			this.grdTopSegmentGroupList.Location = new System.Drawing.Point(0, 0);
			this.grdTopSegmentGroupList.Margin = new System.Windows.Forms.Padding(0);
			this.grdTopSegmentGroupList.Name = "grdTopSegmentGroupList";
			this.grdTopSegmentGroupList.ShowBorder = true;
			this.grdTopSegmentGroupList.ShowStatusBar = false;
			this.grdTopSegmentGroupList.Size = new System.Drawing.Size(469, 372);
			this.grdTopSegmentGroupList.TabIndex = 0;
			// 
			// middleSegmentGroupPage
			// 
			this.middleSegmentGroupPage.Controls.Add(this.grdMiddleSegmentGroupList);
			this.tabPartion.SetLanguageKey(this.middleSegmentGroupPage, "MIDDLESEGMENTGROUP");
			this.middleSegmentGroupPage.Name = "middleSegmentGroupPage";
			this.middleSegmentGroupPage.Size = new System.Drawing.Size(469, 372);
			this.middleSegmentGroupPage.Text = "중공정그룹";
			// 
			// grdMiddleSegmentGroupList
			// 
			this.grdMiddleSegmentGroupList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdMiddleSegmentGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMiddleSegmentGroupList.IsUsePaging = false;
			this.grdMiddleSegmentGroupList.LanguageKey = "MIDDLELOADSEGMENTGROUPLIST";
			this.grdMiddleSegmentGroupList.Location = new System.Drawing.Point(0, 0);
			this.grdMiddleSegmentGroupList.Margin = new System.Windows.Forms.Padding(0);
			this.grdMiddleSegmentGroupList.Name = "grdMiddleSegmentGroupList";
			this.grdMiddleSegmentGroupList.ShowBorder = true;
			this.grdMiddleSegmentGroupList.ShowStatusBar = false;
			this.grdMiddleSegmentGroupList.Size = new System.Drawing.Size(469, 372);
			this.grdMiddleSegmentGroupList.TabIndex = 0;
			// 
			// smallSegmentGroupPage
			// 
			this.smallSegmentGroupPage.Controls.Add(this.grdSmallSegmentGroupList);
			this.tabPartion.SetLanguageKey(this.smallSegmentGroupPage, "SMALLSEGMENTGROUP");
			this.smallSegmentGroupPage.Name = "smallSegmentGroupPage";
			this.smallSegmentGroupPage.Size = new System.Drawing.Size(469, 372);
			this.smallSegmentGroupPage.Text = "소공정그룹";
			// 
			// grdSmallSegmentGroupList
			// 
			this.grdSmallSegmentGroupList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdSmallSegmentGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSmallSegmentGroupList.IsUsePaging = false;
			this.grdSmallSegmentGroupList.LanguageKey = "SMALLLOADSEGMENTGROUPLIST";
			this.grdSmallSegmentGroupList.Location = new System.Drawing.Point(0, 0);
			this.grdSmallSegmentGroupList.Margin = new System.Windows.Forms.Padding(0);
			this.grdSmallSegmentGroupList.Name = "grdSmallSegmentGroupList";
			this.grdSmallSegmentGroupList.ShowBorder = true;
			this.grdSmallSegmentGroupList.ShowStatusBar = false;
			this.grdSmallSegmentGroupList.Size = new System.Drawing.Size(469, 372);
			this.grdSmallSegmentGroupList.TabIndex = 0;
			// 
			// ownerMappingPage
			// 
			this.ownerMappingPage.Controls.Add(this.grdOwnerMappingList);
			this.tabPartion.SetLanguageKey(this.ownerMappingPage, "OWNER");
			this.ownerMappingPage.Name = "ownerMappingPage";
			this.ownerMappingPage.Size = new System.Drawing.Size(469, 372);
			this.ownerMappingPage.Text = "담당자";
			// 
			// grdOwnerMappingList
			// 
			this.grdOwnerMappingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdOwnerMappingList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdOwnerMappingList.IsUsePaging = false;
			this.grdOwnerMappingList.LanguageKey = "OWNERMAPPINGLIST";
			this.grdOwnerMappingList.Location = new System.Drawing.Point(0, 0);
			this.grdOwnerMappingList.Margin = new System.Windows.Forms.Padding(0);
			this.grdOwnerMappingList.Name = "grdOwnerMappingList";
			this.grdOwnerMappingList.ShowBorder = true;
			this.grdOwnerMappingList.ShowStatusBar = false;
			this.grdOwnerMappingList.Size = new System.Drawing.Size(469, 372);
			this.grdOwnerMappingList.TabIndex = 0;
			// 
			// LoadPredictionStdInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "LoadPredictionStdInfo";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabPartion)).EndInit();
			this.tabPartion.ResumeLayout(false);
			this.loadStandardPage.ResumeLayout(false);
			this.standardSegmentMappingPage.ResumeLayout(false);
			this.topSegmentGroupPage.ResumeLayout(false);
			this.middleSegmentGroupPage.ResumeLayout(false);
			this.smallSegmentGroupPage.ResumeLayout(false);
			this.ownerMappingPage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabPartion;
		private DevExpress.XtraTab.XtraTabPage standardSegmentMappingPage;
		private DevExpress.XtraTab.XtraTabPage topSegmentGroupPage;
		private Framework.SmartControls.SmartBandedGrid grdStandardSegmentMapList;
		private Framework.SmartControls.SmartBandedGrid grdTopSegmentGroupList;
		private DevExpress.XtraTab.XtraTabPage middleSegmentGroupPage;
		private Framework.SmartControls.SmartBandedGrid grdMiddleSegmentGroupList;
		private DevExpress.XtraTab.XtraTabPage smallSegmentGroupPage;
		private Framework.SmartControls.SmartBandedGrid grdSmallSegmentGroupList;
		private DevExpress.XtraTab.XtraTabPage ownerMappingPage;
		private Framework.SmartControls.SmartBandedGrid grdOwnerMappingList;
		private DevExpress.XtraTab.XtraTabPage loadStandardPage;
		private Framework.SmartControls.SmartBandedGrid grdLoadStandardList;
	}
}