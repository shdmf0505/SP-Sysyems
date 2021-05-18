namespace Micube.SmartMES.ProductManagement
{
	partial class LoadPredictionForCapaMng
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
            this.tabPartion = new Micube.Framework.SmartControls.SmartTabControl();
            this.segmentcapamng = new DevExpress.XtraTab.XtraTabPage();
            this.grdCapaMgmtOfSegBaseInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.loadcapamng = new DevExpress.XtraTab.XtraTabPage();
            this.grdCapaMgmtOfLoadBaseInfo = new Micube.Framework.SmartControls.SmartBandedGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartion)).BeginInit();
            this.tabPartion.SuspendLayout();
            this.segmentcapamng.SuspendLayout();
            this.loadcapamng.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Size = new System.Drawing.Size(371, 670);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(880, 30);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabPartion);
            this.pnlContent.Size = new System.Drawing.Size(880, 673);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1261, 709);
            // 
            // tabPartion
            // 
            this.tabPartion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPartion.Location = new System.Drawing.Point(0, 0);
            this.tabPartion.Name = "tabPartion";
            this.tabPartion.SelectedTabPage = this.segmentcapamng;
            this.tabPartion.Size = new System.Drawing.Size(880, 673);
            this.tabPartion.TabIndex = 0;
            this.tabPartion.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.segmentcapamng,
            this.loadcapamng});
            // 
            // segmentcapamng
            // 
            this.segmentcapamng.Controls.Add(this.grdCapaMgmtOfSegBaseInfo);
            this.segmentcapamng.Name = "segmentcapamng";
            this.segmentcapamng.Size = new System.Drawing.Size(873, 637);
            this.segmentcapamng.Text = "공정 CAPA관리";
            // 
            // grdCapaMgmtOfSegBaseInfo
            // 
            this.grdCapaMgmtOfSegBaseInfo.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCapaMgmtOfSegBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCapaMgmtOfSegBaseInfo.IsUsePaging = false;
            this.grdCapaMgmtOfSegBaseInfo.LanguageKey = "SGMCAPAMNG";
            this.grdCapaMgmtOfSegBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.grdCapaMgmtOfSegBaseInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdCapaMgmtOfSegBaseInfo.Name = "grdCapaMgmtOfSegBaseInfo";
            this.grdCapaMgmtOfSegBaseInfo.ShowBorder = true;
            this.grdCapaMgmtOfSegBaseInfo.ShowStatusBar = false;
            this.grdCapaMgmtOfSegBaseInfo.Size = new System.Drawing.Size(873, 637);
            this.grdCapaMgmtOfSegBaseInfo.TabIndex = 0;
            // 
            // loadcapamng
            // 
            this.loadcapamng.Controls.Add(this.grdCapaMgmtOfLoadBaseInfo);
            this.loadcapamng.Name = "loadcapamng";
            this.loadcapamng.Size = new System.Drawing.Size(873, 637);
            this.loadcapamng.Text = "부하량 대비 CAPA 비교";
            // 
            // grdCapaMgmtOfLoadBaseInfo
            // 
            this.grdCapaMgmtOfLoadBaseInfo.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdCapaMgmtOfLoadBaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCapaMgmtOfLoadBaseInfo.IsUsePaging = false;
            this.grdCapaMgmtOfLoadBaseInfo.LanguageKey = "LOADCAPACOMPARE";
            this.grdCapaMgmtOfLoadBaseInfo.Location = new System.Drawing.Point(0, 0);
            this.grdCapaMgmtOfLoadBaseInfo.Margin = new System.Windows.Forms.Padding(0);
            this.grdCapaMgmtOfLoadBaseInfo.Name = "grdCapaMgmtOfLoadBaseInfo";
            this.grdCapaMgmtOfLoadBaseInfo.ShowBorder = true;
            this.grdCapaMgmtOfLoadBaseInfo.ShowStatusBar = false;
            this.grdCapaMgmtOfLoadBaseInfo.Size = new System.Drawing.Size(873, 637);
            this.grdCapaMgmtOfLoadBaseInfo.TabIndex = 0;
            // 
            // LoadPredictionForCapaMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 747);
            this.Name = "LoadPredictionForCapaMng";
            this.Padding = new System.Windows.Forms.Padding(19, 19, 19, 19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartion)).EndInit();
            this.tabPartion.ResumeLayout(false);
            this.segmentcapamng.ResumeLayout(false);
            this.loadcapamng.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        
		#endregion

		private Framework.SmartControls.SmartTabControl tabPartion;
		private DevExpress.XtraTab.XtraTabPage segmentcapamng;
		private DevExpress.XtraTab.XtraTabPage loadcapamng;
		private Framework.SmartControls.SmartBandedGrid grdCapaMgmtOfSegBaseInfo;
		private Framework.SmartControls.SmartBandedGrid grdCapaMgmtOfLoadBaseInfo;
    }
}