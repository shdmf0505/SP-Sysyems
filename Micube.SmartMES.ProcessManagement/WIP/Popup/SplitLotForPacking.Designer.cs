namespace Micube.SmartMES.ProcessManagement
{
	partial class SplitLotForPacking
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
			this.numBoxQty = new Micube.Framework.SmartControls.SmartSpinEdit();
			this.lblBoxQty = new Micube.Framework.SmartControls.SmartLabel();
			this.btnSplitLot = new Micube.Framework.SmartControls.SmartButton();
			this.numSplitLotQty = new Micube.Framework.SmartControls.SmartSpinEdit();
			this.lblLotQty = new Micube.Framework.SmartControls.SmartLabel();
			this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
			this.lblLotId = new Micube.Framework.SmartControls.SmartLabel();
			this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.grdLotInfo = new Micube.SmartMES.Commons.Controls.SmartLotInfoGrid();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnClose = new Micube.Framework.SmartControls.SmartButton();
			this.btnPrintLotCard = new Micube.Framework.SmartControls.SmartButton();
			this.btnSave = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
			this.smartPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBoxQty.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSplitLotQty.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(877, 442);
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotList, 0, 4);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.grdLotInfo, 0, 2);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 6);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 7;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 201F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(877, 442);
			this.smartSplitTableLayoutPanel1.TabIndex = 0;
			// 
			// smartPanel1
			// 
			this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smartPanel1.Controls.Add(this.numBoxQty);
			this.smartPanel1.Controls.Add(this.lblBoxQty);
			this.smartPanel1.Controls.Add(this.btnSplitLot);
			this.smartPanel1.Controls.Add(this.numSplitLotQty);
			this.smartPanel1.Controls.Add(this.lblLotQty);
			this.smartPanel1.Controls.Add(this.txtLotId);
			this.smartPanel1.Controls.Add(this.lblLotId);
			this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.smartPanel1.Name = "smartPanel1";
			this.smartPanel1.Size = new System.Drawing.Size(877, 24);
			this.smartPanel1.TabIndex = 1;
			// 
			// numBoxQty
			// 
			this.numBoxQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.numBoxQty.LabelText = null;
			this.numBoxQty.LanguageKey = null;
			this.numBoxQty.Location = new System.Drawing.Point(456, 2);
			this.numBoxQty.Name = "numBoxQty";
			this.numBoxQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.numBoxQty.Properties.ReadOnly = true;
			this.numBoxQty.Size = new System.Drawing.Size(80, 20);
			this.numBoxQty.TabIndex = 6;
			// 
			// lblBoxQty
			// 
			this.lblBoxQty.LanguageKey = "BOXQTY";
			this.lblBoxQty.Location = new System.Drawing.Point(382, 5);
			this.lblBoxQty.Margin = new System.Windows.Forms.Padding(0);
			this.lblBoxQty.Name = "lblBoxQty";
			this.lblBoxQty.Size = new System.Drawing.Size(60, 14);
			this.lblBoxQty.TabIndex = 5;
			this.lblBoxQty.Text = "단위Box수량";
			// 
			// btnSplitLot
			// 
			this.btnSplitLot.AllowFocus = false;
			this.btnSplitLot.IsBusy = false;
			this.btnSplitLot.LanguageKey = "SPLIT";
			this.btnSplitLot.Location = new System.Drawing.Point(689, 0);
			this.btnSplitLot.Margin = new System.Windows.Forms.Padding(0);
			this.btnSplitLot.Name = "btnSplitLot";
			this.btnSplitLot.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSplitLot.Size = new System.Drawing.Size(75, 23);
			this.btnSplitLot.TabIndex = 4;
			this.btnSplitLot.Text = "분햘";
			this.btnSplitLot.TooltipLanguageKey = "";
			// 
			// numSplitLotQty
			// 
			this.numSplitLotQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.numSplitLotQty.LabelText = null;
			this.numSplitLotQty.LanguageKey = null;
			this.numSplitLotQty.Location = new System.Drawing.Point(587, 2);
			this.numSplitLotQty.Name = "numSplitLotQty";
			this.numSplitLotQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.numSplitLotQty.Size = new System.Drawing.Size(80, 20);
			this.numSplitLotQty.TabIndex = 3;
			// 
			// lblLotQty
			// 
			this.lblLotQty.LanguageKey = "QTY";
			this.lblLotQty.Location = new System.Drawing.Point(554, 5);
			this.lblLotQty.Margin = new System.Windows.Forms.Padding(0);
			this.lblLotQty.Name = "lblLotQty";
			this.lblLotQty.Size = new System.Drawing.Size(20, 14);
			this.lblLotQty.TabIndex = 2;
			this.lblLotQty.Text = "수량";
			// 
			// txtLotId
			// 
			this.txtLotId.LabelText = null;
			this.txtLotId.LanguageKey = null;
			this.txtLotId.Location = new System.Drawing.Point(60, 2);
			this.txtLotId.Margin = new System.Windows.Forms.Padding(0);
			this.txtLotId.Name = "txtLotId";
			this.txtLotId.Properties.ReadOnly = true;
			this.txtLotId.Size = new System.Drawing.Size(285, 20);
			this.txtLotId.TabIndex = 1;
			// 
			// lblLotId
			// 
			this.lblLotId.LanguageKey = "LOTID";
			this.lblLotId.Location = new System.Drawing.Point(5, 5);
			this.lblLotId.Margin = new System.Windows.Forms.Padding(0);
			this.lblLotId.Name = "lblLotId";
			this.lblLotId.Size = new System.Drawing.Size(39, 14);
			this.lblLotId.TabIndex = 0;
			this.lblLotId.Text = "LOT ID";
			// 
			// grdLotList
			// 
			this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLotList.IsUsePaging = false;
			this.grdLotList.LanguageKey = null;
			this.grdLotList.Location = new System.Drawing.Point(0, 235);
			this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
			this.grdLotList.Name = "grdLotList";
			this.grdLotList.ShowBorder = true;
			this.grdLotList.Size = new System.Drawing.Size(877, 178);
			this.grdLotList.TabIndex = 2;
			// 
			// grdLotInfo
			// 
			this.grdLotInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLotInfo.Location = new System.Drawing.Point(0, 29);
			this.grdLotInfo.Margin = new System.Windows.Forms.Padding(0);
			this.grdLotInfo.Name = "grdLotInfo";
			this.grdLotInfo.Size = new System.Drawing.Size(877, 201);
			this.grdLotInfo.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnClose);
			this.flowLayoutPanel1.Controls.Add(this.btnPrintLotCard);
			this.flowLayoutPanel1.Controls.Add(this.btnSave);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 418);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(877, 24);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// btnClose
			// 
			this.btnClose.AllowFocus = false;
			this.btnClose.IsBusy = false;
			this.btnClose.Location = new System.Drawing.Point(802, 0);
			this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "닫기";
			this.btnClose.TooltipLanguageKey = "";
			// 
			// btnPrintLotCard
			// 
			this.btnPrintLotCard.AllowFocus = false;
			this.btnPrintLotCard.IsBusy = false;
			this.btnPrintLotCard.LanguageKey = "PRINT";
			this.btnPrintLotCard.Location = new System.Drawing.Point(724, 0);
			this.btnPrintLotCard.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnPrintLotCard.Name = "btnPrintLotCard";
			this.btnPrintLotCard.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnPrintLotCard.Size = new System.Drawing.Size(75, 23);
			this.btnPrintLotCard.TabIndex = 1;
			this.btnPrintLotCard.Text = "출력";
			this.btnPrintLotCard.TooltipLanguageKey = "";
			// 
			// btnSave
			// 
			this.btnSave.AllowFocus = false;
			this.btnSave.IsBusy = false;
			this.btnSave.LanguageKey = "SAVE";
			this.btnSave.Location = new System.Drawing.Point(646, 0);
			this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "저장";
			this.btnSave.TooltipLanguageKey = "";
			// 
			// SplitLotForPacking
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(897, 462);
			this.Name = "SplitLotForPacking";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
			this.smartPanel1.ResumeLayout(false);
			this.smartPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBoxQty.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSplitLotQty.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Commons.Controls.SmartLotInfoGrid grdLotInfo;
		private Framework.SmartControls.SmartPanel smartPanel1;
		private Framework.SmartControls.SmartLabel lblLotId;
		private Framework.SmartControls.SmartTextBox txtLotId;
		private Framework.SmartControls.SmartButton btnSplitLot;
		private Framework.SmartControls.SmartSpinEdit numSplitLotQty;
		private Framework.SmartControls.SmartLabel lblLotQty;
		private Framework.SmartControls.SmartBandedGrid grdLotList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Framework.SmartControls.SmartButton btnClose;
		private Framework.SmartControls.SmartButton btnPrintLotCard;
		private Framework.SmartControls.SmartButton btnSave;
		private Framework.SmartControls.SmartSpinEdit numBoxQty;
		private Framework.SmartControls.SmartLabel lblBoxQty;
	}
}