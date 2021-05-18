namespace Micube.SmartMES.ProcessManagement
{
	partial class WorkIncommingResult
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
			this.tabWorkIncommingResult = new Micube.Framework.SmartControls.SmartTabControl();
			this.workInResultByDayPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdByDay = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.workInResultByProductPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdByProduct = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.workInResultByLotPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdByLot = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.workInResultByDaySMT = new DevExpress.XtraTab.XtraTabPage();
			this.grdByDaySMT = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabWorkIncommingResult)).BeginInit();
			this.tabWorkIncommingResult.SuspendLayout();
			this.workInResultByDayPage.SuspendLayout();
			this.workInResultByProductPage.SuspendLayout();
			this.workInResultByLotPage.SuspendLayout();
			this.workInResultByDaySMT.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 460);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(610, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.tabWorkIncommingResult);
			this.pnlContent.Size = new System.Drawing.Size(610, 464);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(915, 493);
			// 
			// tabWorkIncommingResult
			// 
			this.tabWorkIncommingResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabWorkIncommingResult.Location = new System.Drawing.Point(0, 0);
			this.tabWorkIncommingResult.Name = "tabWorkIncommingResult";
			this.tabWorkIncommingResult.SelectedTabPage = this.workInResultByDayPage;
			this.tabWorkIncommingResult.Size = new System.Drawing.Size(610, 464);
			this.tabWorkIncommingResult.TabIndex = 0;
			this.tabWorkIncommingResult.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.workInResultByDayPage,
            this.workInResultByProductPage,
            this.workInResultByLotPage,
            this.workInResultByDaySMT});
			// 
			// workInResultByDayPage
			// 
			this.workInResultByDayPage.Controls.Add(this.grdByDay);
			this.tabWorkIncommingResult.SetLanguageKey(this.workInResultByDayPage, "BYDAY");
			this.workInResultByDayPage.Name = "workInResultByDayPage";
			this.workInResultByDayPage.Size = new System.Drawing.Size(604, 435);
			this.workInResultByDayPage.Text = "일별";
			// 
			// grdByDay
			// 
			this.grdByDay.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdByDay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdByDay.IsUsePaging = false;
			this.grdByDay.LanguageKey = "WORKINCOMERESULTBYDATE";
			this.grdByDay.Location = new System.Drawing.Point(0, 0);
			this.grdByDay.Margin = new System.Windows.Forms.Padding(0);
			this.grdByDay.Name = "grdByDay";
			this.grdByDay.ShowBorder = true;
			this.grdByDay.ShowStatusBar = false;
			this.grdByDay.Size = new System.Drawing.Size(604, 435);
			this.grdByDay.TabIndex = 0;
			// 
			// workInResultByProductPage
			// 
			this.workInResultByProductPage.Controls.Add(this.grdByProduct);
			this.tabWorkIncommingResult.SetLanguageKey(this.workInResultByProductPage, "BYPRODUCT");
			this.workInResultByProductPage.Name = "workInResultByProductPage";
			this.workInResultByProductPage.Size = new System.Drawing.Size(604, 435);
			this.workInResultByProductPage.Text = "품목별";
			// 
			// grdByProduct
			// 
			this.grdByProduct.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdByProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdByProduct.IsUsePaging = false;
			this.grdByProduct.LanguageKey = "WORKINCOMERESULTBYPRODUCT";
			this.grdByProduct.Location = new System.Drawing.Point(0, 0);
			this.grdByProduct.Margin = new System.Windows.Forms.Padding(0);
			this.grdByProduct.Name = "grdByProduct";
			this.grdByProduct.ShowBorder = true;
			this.grdByProduct.ShowStatusBar = false;
			this.grdByProduct.Size = new System.Drawing.Size(604, 435);
			this.grdByProduct.TabIndex = 0;
			// 
			// workInResultByLotPage
			// 
			this.workInResultByLotPage.Controls.Add(this.grdByLot);
			this.tabWorkIncommingResult.SetLanguageKey(this.workInResultByLotPage, "BYLOT");
			this.workInResultByLotPage.Name = "workInResultByLotPage";
			this.workInResultByLotPage.Size = new System.Drawing.Size(604, 435);
			this.workInResultByLotPage.Text = "LOT별";
			// 
			// grdByLot
			// 
			this.grdByLot.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdByLot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdByLot.IsUsePaging = false;
			this.grdByLot.LanguageKey = "WORKINCOMERESULTBYLOT";
			this.grdByLot.Location = new System.Drawing.Point(0, 0);
			this.grdByLot.Margin = new System.Windows.Forms.Padding(0);
			this.grdByLot.Name = "grdByLot";
			this.grdByLot.ShowBorder = true;
			this.grdByLot.ShowStatusBar = false;
			this.grdByLot.Size = new System.Drawing.Size(604, 435);
			this.grdByLot.TabIndex = 0;
			// 
			// workInResultByDaySMT
			// 
			this.workInResultByDaySMT.Controls.Add(this.grdByDaySMT);
			this.tabWorkIncommingResult.SetLanguageKey(this.workInResultByDaySMT, "BYDAYSMT");
			this.workInResultByDaySMT.Name = "workInResultByDaySMT";
			this.workInResultByDaySMT.Size = new System.Drawing.Size(604, 435);
			this.workInResultByDaySMT.Text = "일별(SMT)";
			// 
			// grdByDaySMT
			// 
			this.grdByDaySMT.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdByDaySMT.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdByDaySMT.IsUsePaging = false;
			this.grdByDaySMT.LanguageKey = "WORKINCOMERESULTBYDATESMT";
			this.grdByDaySMT.Location = new System.Drawing.Point(0, 0);
			this.grdByDaySMT.Margin = new System.Windows.Forms.Padding(0);
			this.grdByDaySMT.Name = "grdByDaySMT";
			this.grdByDaySMT.ShowBorder = true;
			this.grdByDaySMT.Size = new System.Drawing.Size(604, 435);
			this.grdByDaySMT.TabIndex = 0;
			// 
			// WorkIncommingResult
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(935, 513);
			this.Name = "WorkIncommingResult";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabWorkIncommingResult)).EndInit();
			this.tabWorkIncommingResult.ResumeLayout(false);
			this.workInResultByDayPage.ResumeLayout(false);
			this.workInResultByProductPage.ResumeLayout(false);
			this.workInResultByLotPage.ResumeLayout(false);
			this.workInResultByDaySMT.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabWorkIncommingResult;
		private DevExpress.XtraTab.XtraTabPage workInResultByDayPage;
		private DevExpress.XtraTab.XtraTabPage workInResultByProductPage;
		private DevExpress.XtraTab.XtraTabPage workInResultByLotPage;
		private DevExpress.XtraTab.XtraTabPage workInResultByDaySMT;
		private Framework.SmartControls.SmartBandedGrid grdByDay;
		private Framework.SmartControls.SmartBandedGrid grdByProduct;
		private Framework.SmartControls.SmartBandedGrid grdByLot;
		private Framework.SmartControls.SmartBandedGrid grdByDaySMT;
	}
}