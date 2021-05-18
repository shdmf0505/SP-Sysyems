namespace Micube.SmartMES.ProcessManagement
{
	partial class PrintLotCard
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
			this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
			this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlToolbar.SuspendLayout();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 397);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel1, 0);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdLotList);
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 2;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.btnPrint, 1, 0);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(47, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 1;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(428, 24);
			this.smartSplitTableLayoutPanel1.TabIndex = 5;
			// 
			// btnPrint
			// 
			this.btnPrint.AllowFocus = false;
			this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnPrint.IsBusy = false;
			this.btnPrint.LanguageKey = "PRINT";
			this.btnPrint.Location = new System.Drawing.Point(353, 0);
			this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnPrint.Size = new System.Drawing.Size(75, 24);
			this.btnPrint.TabIndex = 1;
			this.btnPrint.Text = "Print";
			this.btnPrint.TooltipLanguageKey = "";
			// 
			// grdLotList
			// 
			this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLotList.IsUsePaging = false;
			this.grdLotList.LanguageKey = null;
			this.grdLotList.Location = new System.Drawing.Point(0, 0);
			this.grdLotList.Margin = new System.Windows.Forms.Padding(0);
			this.grdLotList.Name = "grdLotList";
			this.grdLotList.ShowBorder = true;
			this.grdLotList.Size = new System.Drawing.Size(475, 401);
			this.grdLotList.TabIndex = 0;
			// 
			// PrintLotCard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Name = "PrintLotCard";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlToolbar.ResumeLayout(false);
			this.pnlToolbar.PerformLayout();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private Framework.SmartControls.SmartButton btnPrint;
		private Framework.SmartControls.SmartBandedGrid grdLotList;
	}
}