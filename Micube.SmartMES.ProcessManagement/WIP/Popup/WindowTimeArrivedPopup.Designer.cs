namespace Micube.SmartMES.ProcessManagement
{
	partial class WindowTimeArrivedPopup
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new Micube.Framework.SmartControls.SmartButton();
            this.btnLink = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.grdWindowTime = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.lblConsumableLotId = new Micube.Framework.SmartControls.SmartLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(865, 442);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnLink);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 418);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(865, 24);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.IsBusy = false;
            this.btnClose.IsWrite = false;
            this.btnClose.LanguageKey = "CLOSE";
            this.btnClose.Location = new System.Drawing.Point(790, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.TooltipLanguageKey = "";
            // 
            // btnLink
            // 
            this.btnLink.AllowFocus = false;
            this.btnLink.IsBusy = false;
            this.btnLink.IsWrite = false;
            this.btnLink.Location = new System.Drawing.Point(712, 0);
            this.btnLink.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnLink.Name = "btnLink";
            this.btnLink.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnLink.Size = new System.Drawing.Size(75, 23);
            this.btnLink.TabIndex = 1;
            this.btnLink.Text = "WTime";
            this.btnLink.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.lblConsumableLotId, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.grdWindowTime, 0, 2);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 5;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(865, 442);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // grdWindowTime
            // 
            this.grdWindowTime.Caption = "Window Time Lot 조회";
            this.grdWindowTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWindowTime.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWindowTime.IsUsePaging = false;
            this.grdWindowTime.LanguageKey = "WINDOWTIMELOTSEARCH";
            this.grdWindowTime.Location = new System.Drawing.Point(0, 29);
            this.grdWindowTime.Margin = new System.Windows.Forms.Padding(0);
            this.grdWindowTime.Name = "grdWindowTime";
            this.grdWindowTime.ShowBorder = true;
            this.grdWindowTime.Size = new System.Drawing.Size(865, 384);
            this.grdWindowTime.TabIndex = 4;
            this.grdWindowTime.UseAutoBestFitColumns = false;
            // 
            // lblConsumableLotId
            // 
            this.lblConsumableLotId.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblConsumableLotId.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblConsumableLotId.Appearance.Options.UseFont = true;
            this.lblConsumableLotId.Appearance.Options.UseForeColor = true;
            this.lblConsumableLotId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblConsumableLotId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConsumableLotId.LanguageKey = "WTIMEARRIVEDLOTS";
            this.lblConsumableLotId.Location = new System.Drawing.Point(0, 0);
            this.lblConsumableLotId.Margin = new System.Windows.Forms.Padding(0);
            this.lblConsumableLotId.Name = "lblConsumableLotId";
            this.lblConsumableLotId.Size = new System.Drawing.Size(865, 24);
            this.lblConsumableLotId.TabIndex = 5;
            this.lblConsumableLotId.Text = "작업장에 Window Time 이 도래한 Lot이 있습니다.";
            // 
            // WindowTimeArrivedPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 462);
            this.LanguageKey = "RESOURCE";
            this.Name = "WindowTimeArrivedPopup";
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

        #endregion

        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Framework.SmartControls.SmartButton btnClose;
        private Framework.SmartControls.SmartButton btnLink;
        private Framework.SmartControls.SmartBandedGrid grdWindowTime;
        private Framework.SmartControls.SmartLabel lblConsumableLotId;
    }
}
