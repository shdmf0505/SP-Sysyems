namespace Micube.SmartMES.ProcessManagement
{
	partial class ConsumableInOutHistory
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
			this.tabInConsumOutHistory = new Micube.Framework.SmartControls.SmartTabControl();
			this.In_RequestConsumPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdConsumRequestInbound = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.In_StockMovePage = new DevExpress.XtraTab.XtraTabPage();
			this.grdStockMoveInbound = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.In_EtcInboundPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdEtcInbound = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.Out_ReturnsPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdReturnOutbound = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.Out_StockMovePage = new DevExpress.XtraTab.XtraTabPage();
			this.grdStockMoveOutbound = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.Out_EtcOutboundPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdEtcOutbound = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.grpHistory = new Micube.Framework.SmartControls.SmartGroupBox();
			this.spcInOutHistory = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grdInOutHistory = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.In_WipPage = new DevExpress.XtraTab.XtraTabPage();
			this.Out_WipPage = new DevExpress.XtraTab.XtraTabPage();
			this.grdWipInbound = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.grdWipOutBound = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabInConsumOutHistory)).BeginInit();
			this.tabInConsumOutHistory.SuspendLayout();
			this.In_RequestConsumPage.SuspendLayout();
			this.In_StockMovePage.SuspendLayout();
			this.In_EtcInboundPage.SuspendLayout();
			this.Out_ReturnsPage.SuspendLayout();
			this.Out_StockMovePage.SuspendLayout();
			this.Out_EtcOutboundPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpHistory)).BeginInit();
			this.grpHistory.SuspendLayout();
			this.In_WipPage.SuspendLayout();
			this.Out_WipPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 522);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Size = new System.Drawing.Size(642, 24);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdInOutHistory);
			this.pnlContent.Controls.Add(this.spcInOutHistory);
			this.pnlContent.Controls.Add(this.grpHistory);
			this.pnlContent.Size = new System.Drawing.Size(642, 526);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(947, 555);
			// 
			// tabInConsumOutHistory
			// 
			this.tabInConsumOutHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabInConsumOutHistory.Location = new System.Drawing.Point(2, 31);
			this.tabInConsumOutHistory.Name = "tabInConsumOutHistory";
			this.tabInConsumOutHistory.SelectedTabPage = this.In_RequestConsumPage;
			this.tabInConsumOutHistory.Size = new System.Drawing.Size(638, 417);
			this.tabInConsumOutHistory.TabIndex = 0;
			this.tabInConsumOutHistory.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.In_RequestConsumPage,
            this.In_StockMovePage,
            this.In_EtcInboundPage,
            this.In_WipPage,
            this.Out_ReturnsPage,
            this.Out_StockMovePage,
            this.Out_EtcOutboundPage,
            this.Out_WipPage});
			// 
			// In_RequestConsumPage
			// 
			this.In_RequestConsumPage.Controls.Add(this.grdConsumRequestInbound);
			this.tabInConsumOutHistory.SetLanguageKey(this.In_RequestConsumPage, "REQUESTCONSUMINBOUND");
			this.In_RequestConsumPage.Name = "In_RequestConsumPage";
			this.In_RequestConsumPage.Size = new System.Drawing.Size(632, 388);
			this.In_RequestConsumPage.Text = "입고 - 불출";
			// 
			// grdConsumRequestInbound
			// 
			this.grdConsumRequestInbound.Caption = "";
			this.grdConsumRequestInbound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdConsumRequestInbound.IsUsePaging = false;
			this.grdConsumRequestInbound.LanguageKey = null;
			this.grdConsumRequestInbound.Location = new System.Drawing.Point(0, 0);
			this.grdConsumRequestInbound.Margin = new System.Windows.Forms.Padding(0);
			this.grdConsumRequestInbound.Name = "grdConsumRequestInbound";
			this.grdConsumRequestInbound.ShowBorder = true;
			this.grdConsumRequestInbound.ShowStatusBar = false;
			this.grdConsumRequestInbound.Size = new System.Drawing.Size(632, 388);
			this.grdConsumRequestInbound.TabIndex = 0;
			// 
			// In_StockMovePage
			// 
			this.In_StockMovePage.Controls.Add(this.grdStockMoveInbound);
			this.tabInConsumOutHistory.SetLanguageKey(this.In_StockMovePage, "STOCKMOVEINBOUND");
			this.In_StockMovePage.Name = "In_StockMovePage";
			this.In_StockMovePage.Size = new System.Drawing.Size(465, 388);
			this.In_StockMovePage.Text = "입고 - 재고이동";
			// 
			// grdStockMoveInbound
			// 
			this.grdStockMoveInbound.Caption = "";
			this.grdStockMoveInbound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdStockMoveInbound.IsUsePaging = false;
			this.grdStockMoveInbound.LanguageKey = null;
			this.grdStockMoveInbound.Location = new System.Drawing.Point(0, 0);
			this.grdStockMoveInbound.Margin = new System.Windows.Forms.Padding(0);
			this.grdStockMoveInbound.Name = "grdStockMoveInbound";
			this.grdStockMoveInbound.ShowBorder = true;
			this.grdStockMoveInbound.ShowStatusBar = false;
			this.grdStockMoveInbound.Size = new System.Drawing.Size(465, 388);
			this.grdStockMoveInbound.TabIndex = 0;
			// 
			// In_EtcInboundPage
			// 
			this.In_EtcInboundPage.Controls.Add(this.grdEtcInbound);
			this.tabInConsumOutHistory.SetLanguageKey(this.In_EtcInboundPage, "ETCINBOUND");
			this.In_EtcInboundPage.Name = "In_EtcInboundPage";
			this.In_EtcInboundPage.Size = new System.Drawing.Size(632, 388);
			this.In_EtcInboundPage.Text = "입고 - 기타입고";
			// 
			// grdEtcInbound
			// 
			this.grdEtcInbound.Caption = "";
			this.grdEtcInbound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdEtcInbound.IsUsePaging = false;
			this.grdEtcInbound.LanguageKey = null;
			this.grdEtcInbound.Location = new System.Drawing.Point(0, 0);
			this.grdEtcInbound.Margin = new System.Windows.Forms.Padding(0);
			this.grdEtcInbound.Name = "grdEtcInbound";
			this.grdEtcInbound.ShowBorder = true;
			this.grdEtcInbound.ShowStatusBar = false;
			this.grdEtcInbound.Size = new System.Drawing.Size(632, 388);
			this.grdEtcInbound.TabIndex = 0;
			// 
			// Out_ReturnsPage
			// 
			this.Out_ReturnsPage.Controls.Add(this.grdReturnOutbound);
			this.tabInConsumOutHistory.SetLanguageKey(this.Out_ReturnsPage, "RETURNOUTBOUND");
			this.Out_ReturnsPage.Name = "Out_ReturnsPage";
			this.Out_ReturnsPage.Size = new System.Drawing.Size(632, 388);
			this.Out_ReturnsPage.Text = "출고 - 반납";
			// 
			// grdReturnOutbound
			// 
			this.grdReturnOutbound.Caption = "";
			this.grdReturnOutbound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdReturnOutbound.IsUsePaging = false;
			this.grdReturnOutbound.LanguageKey = null;
			this.grdReturnOutbound.Location = new System.Drawing.Point(0, 0);
			this.grdReturnOutbound.Margin = new System.Windows.Forms.Padding(0);
			this.grdReturnOutbound.Name = "grdReturnOutbound";
			this.grdReturnOutbound.ShowBorder = true;
			this.grdReturnOutbound.ShowStatusBar = false;
			this.grdReturnOutbound.Size = new System.Drawing.Size(632, 388);
			this.grdReturnOutbound.TabIndex = 0;
			// 
			// Out_StockMovePage
			// 
			this.Out_StockMovePage.Controls.Add(this.grdStockMoveOutbound);
			this.tabInConsumOutHistory.SetLanguageKey(this.Out_StockMovePage, "STOCKMOVEOUTBOUND");
			this.Out_StockMovePage.Name = "Out_StockMovePage";
			this.Out_StockMovePage.Size = new System.Drawing.Size(632, 388);
			this.Out_StockMovePage.Text = "출고 - 재고이동";
			// 
			// grdStockMoveOutbound
			// 
			this.grdStockMoveOutbound.Caption = "";
			this.grdStockMoveOutbound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdStockMoveOutbound.IsUsePaging = false;
			this.grdStockMoveOutbound.LanguageKey = null;
			this.grdStockMoveOutbound.Location = new System.Drawing.Point(0, 0);
			this.grdStockMoveOutbound.Margin = new System.Windows.Forms.Padding(0);
			this.grdStockMoveOutbound.Name = "grdStockMoveOutbound";
			this.grdStockMoveOutbound.ShowBorder = true;
			this.grdStockMoveOutbound.ShowStatusBar = false;
			this.grdStockMoveOutbound.Size = new System.Drawing.Size(632, 388);
			this.grdStockMoveOutbound.TabIndex = 0;
			// 
			// Out_EtcOutboundPage
			// 
			this.Out_EtcOutboundPage.Controls.Add(this.grdEtcOutbound);
			this.tabInConsumOutHistory.SetLanguageKey(this.Out_EtcOutboundPage, "ETCOUTBOUND");
			this.Out_EtcOutboundPage.Name = "Out_EtcOutboundPage";
			this.Out_EtcOutboundPage.Size = new System.Drawing.Size(632, 388);
			this.Out_EtcOutboundPage.Text = "출고 - 기타출고";
			// 
			// grdEtcOutbound
			// 
			this.grdEtcOutbound.Caption = "";
			this.grdEtcOutbound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdEtcOutbound.IsUsePaging = false;
			this.grdEtcOutbound.LanguageKey = null;
			this.grdEtcOutbound.Location = new System.Drawing.Point(0, 0);
			this.grdEtcOutbound.Margin = new System.Windows.Forms.Padding(0);
			this.grdEtcOutbound.Name = "grdEtcOutbound";
			this.grdEtcOutbound.ShowBorder = true;
			this.grdEtcOutbound.ShowStatusBar = false;
			this.grdEtcOutbound.Size = new System.Drawing.Size(632, 388);
			this.grdEtcOutbound.TabIndex = 0;
			// 
			// grpHistory
			// 
			this.grpHistory.Controls.Add(this.tabInConsumOutHistory);
			this.grpHistory.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.grpHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grpHistory.GroupStyle = DevExpress.Utils.GroupStyle.Card;
			this.grpHistory.LanguageKey = "INOUTHISTORYINFO";
			this.grpHistory.Location = new System.Drawing.Point(0, 76);
			this.grpHistory.Name = "grpHistory";
			this.grpHistory.ShowBorder = true;
			this.grpHistory.Size = new System.Drawing.Size(642, 450);
			this.grpHistory.TabIndex = 1;
			this.grpHistory.Text = "이력정보";
			// 
			// spcInOutHistory
			// 
			this.spcInOutHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.spcInOutHistory.Location = new System.Drawing.Point(0, 71);
			this.spcInOutHistory.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.spcInOutHistory.Name = "spcInOutHistory";
			this.spcInOutHistory.Size = new System.Drawing.Size(642, 5);
			this.spcInOutHistory.TabIndex = 2;
			this.spcInOutHistory.TabStop = false;
			// 
			// grdInOutHistory
			// 
			this.grdInOutHistory.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdInOutHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdInOutHistory.IsUsePaging = false;
			this.grdInOutHistory.LanguageKey = "CONSUMINOUTHISTORYLIST";
			this.grdInOutHistory.Location = new System.Drawing.Point(0, 0);
			this.grdInOutHistory.Margin = new System.Windows.Forms.Padding(0);
			this.grdInOutHistory.Name = "grdInOutHistory";
			this.grdInOutHistory.ShowBorder = true;
			this.grdInOutHistory.Size = new System.Drawing.Size(642, 71);
			this.grdInOutHistory.TabIndex = 3;
			// 
			// In_WipPage
			// 
			this.In_WipPage.Controls.Add(this.grdWipInbound);
			this.tabInConsumOutHistory.SetLanguageKey(this.In_WipPage, "WIPINBOUND");
			this.In_WipPage.Name = "In_WipPage";
			this.In_WipPage.Size = new System.Drawing.Size(632, 388);
			this.In_WipPage.Text = "입고 - 생산입고";
			// 
			// Out_WipPage
			// 
			this.Out_WipPage.Controls.Add(this.grdWipOutBound);
			this.tabInConsumOutHistory.SetLanguageKey(this.Out_WipPage, "WIPOUTBOUND");
			this.Out_WipPage.Name = "Out_WipPage";
			this.Out_WipPage.Size = new System.Drawing.Size(632, 388);
			this.Out_WipPage.Text = "출고 - 공정출고";
			// 
			// grdWipInbound
			// 
			this.grdWipInbound.Caption = "";
			this.grdWipInbound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdWipInbound.IsUsePaging = false;
			this.grdWipInbound.LanguageKey = null;
			this.grdWipInbound.Location = new System.Drawing.Point(0, 0);
			this.grdWipInbound.Margin = new System.Windows.Forms.Padding(0);
			this.grdWipInbound.Name = "grdWipInbound";
			this.grdWipInbound.ShowBorder = true;
			this.grdWipInbound.ShowStatusBar = false;
			this.grdWipInbound.Size = new System.Drawing.Size(632, 388);
			this.grdWipInbound.TabIndex = 0;
			// 
			// grdWipOutBound
			// 
			this.grdWipOutBound.Caption = "";
			this.grdWipOutBound.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdWipOutBound.IsUsePaging = false;
			this.grdWipOutBound.LanguageKey = null;
			this.grdWipOutBound.Location = new System.Drawing.Point(0, 0);
			this.grdWipOutBound.Margin = new System.Windows.Forms.Padding(0);
			this.grdWipOutBound.Name = "grdWipOutBound";
			this.grdWipOutBound.ShowBorder = true;
			this.grdWipOutBound.Size = new System.Drawing.Size(632, 388);
			this.grdWipOutBound.TabIndex = 0;
			// 
			// ConsumableInOutHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(967, 575);
			this.Name = "ConsumableInOutHistory";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabInConsumOutHistory)).EndInit();
			this.tabInConsumOutHistory.ResumeLayout(false);
			this.In_RequestConsumPage.ResumeLayout(false);
			this.In_StockMovePage.ResumeLayout(false);
			this.In_EtcInboundPage.ResumeLayout(false);
			this.Out_ReturnsPage.ResumeLayout(false);
			this.Out_StockMovePage.ResumeLayout(false);
			this.Out_EtcOutboundPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpHistory)).EndInit();
			this.grpHistory.ResumeLayout(false);
			this.In_WipPage.ResumeLayout(false);
			this.Out_WipPage.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdInOutHistory;
		private Framework.SmartControls.SmartSpliterControl spcInOutHistory;
		private Framework.SmartControls.SmartGroupBox grpHistory;
		private Framework.SmartControls.SmartTabControl tabInConsumOutHistory;
		private DevExpress.XtraTab.XtraTabPage In_RequestConsumPage;
		private DevExpress.XtraTab.XtraTabPage In_StockMovePage;
		private DevExpress.XtraTab.XtraTabPage In_EtcInboundPage;
		private DevExpress.XtraTab.XtraTabPage Out_ReturnsPage;
		private DevExpress.XtraTab.XtraTabPage Out_StockMovePage;
		private DevExpress.XtraTab.XtraTabPage Out_EtcOutboundPage;
		private Framework.SmartControls.SmartBandedGrid grdConsumRequestInbound;
		private Framework.SmartControls.SmartBandedGrid grdStockMoveInbound;
		private Framework.SmartControls.SmartBandedGrid grdEtcInbound;
		private Framework.SmartControls.SmartBandedGrid grdReturnOutbound;
		private Framework.SmartControls.SmartBandedGrid grdStockMoveOutbound;
		private Framework.SmartControls.SmartBandedGrid grdEtcOutbound;
		private DevExpress.XtraTab.XtraTabPage In_WipPage;
		private Framework.SmartControls.SmartBandedGrid grdWipInbound;
		private DevExpress.XtraTab.XtraTabPage Out_WipPage;
		private Framework.SmartControls.SmartBandedGrid grdWipOutBound;
	}
}