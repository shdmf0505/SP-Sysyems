namespace Micube.SmartMES.StandardInfo.Popup
{
	partial class SetWarehousePopup
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
			this.grdWarehouseByPlant = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.pnlButton = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
			this.btnSave = new Micube.Framework.SmartControls.SmartButton();
			this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.pnlButton.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(544, 369);
			// 
			// grdWarehouseByPlant
			// 
			this.grdWarehouseByPlant.Caption = "";
			this.grdWarehouseByPlant.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdWarehouseByPlant.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdWarehouseByPlant.IsUsePaging = false;
			this.grdWarehouseByPlant.LanguageKey = null;
			this.grdWarehouseByPlant.Location = new System.Drawing.Point(0, 0);
			this.grdWarehouseByPlant.Margin = new System.Windows.Forms.Padding(0);
			this.grdWarehouseByPlant.Name = "grdWarehouseByPlant";
			this.grdWarehouseByPlant.ShowBorder = true;
			this.grdWarehouseByPlant.Size = new System.Drawing.Size(544, 329);
			this.grdWarehouseByPlant.TabIndex = 0;
			this.grdWarehouseByPlant.UseAutoBestFitColumns = false;
			// 
			// pnlButton
			// 
			this.pnlButton.Controls.Add(this.btnCancel);
			this.pnlButton.Controls.Add(this.btnSave);
			this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.pnlButton.Location = new System.Drawing.Point(0, 339);
			this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
			this.pnlButton.Name = "pnlButton";
			this.pnlButton.Size = new System.Drawing.Size(544, 30);
			this.pnlButton.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.AllowFocus = false;
			this.btnCancel.IsBusy = false;
			this.btnCancel.IsWrite = false;
			this.btnCancel.LanguageKey = "CANCEL";
			this.btnCancel.Location = new System.Drawing.Point(469, 0);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "취소";
			this.btnCancel.TooltipLanguageKey = "";
			// 
			// btnSave
			// 
			this.btnSave.AllowFocus = false;
			this.btnSave.IsBusy = false;
			this.btnSave.IsWrite = false;
			this.btnSave.LanguageKey = "SAVE";
			this.btnSave.Location = new System.Drawing.Point(388, 0);
			this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "저장";
			this.btnSave.TooltipLanguageKey = "";
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.grdWarehouseByPlant, 0, 0);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.pnlButton, 0, 2);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 3;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(544, 369);
			this.smartSplitTableLayoutPanel1.TabIndex = 2;
			// 
			// SetWarehousePopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 389);
			this.LanguageKey = "SELECTFINISHWAREHOUSE";
			this.Name = "SetWarehousePopup";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.pnlButton.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnlButton;
		private Framework.SmartControls.SmartButton btnCancel;
		private Framework.SmartControls.SmartButton btnSave;
		private Framework.SmartControls.SmartBandedGrid grdWarehouseByPlant;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
	}
}