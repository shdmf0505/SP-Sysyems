namespace Micube.SmartMES.StandardInfo
{
	partial class GovernanceMaterialSpecPopup
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
			this.grdMaterialSpecList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
			this.btnConfirm = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.grdMaterialSpecList);
			this.pnlMain.Controls.Add(this.flowLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(346, 559);
			// 
			// grdMaterialSpecList
			// 
			this.grdMaterialSpecList.Caption = "";
			this.grdMaterialSpecList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdMaterialSpecList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdMaterialSpecList.IsUsePaging = false;
			this.grdMaterialSpecList.LanguageKey = null;
			this.grdMaterialSpecList.Location = new System.Drawing.Point(0, 0);
			this.grdMaterialSpecList.Margin = new System.Windows.Forms.Padding(0);
			this.grdMaterialSpecList.Name = "grdMaterialSpecList";
			this.grdMaterialSpecList.ShowBorder = true;
			this.grdMaterialSpecList.Size = new System.Drawing.Size(346, 524);
			this.grdMaterialSpecList.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Controls.Add(this.btnConfirm);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 524);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.flowLayoutPanel1.Size = new System.Drawing.Size(346, 35);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.AllowFocus = false;
			this.btnCancel.IsBusy = false;
			this.btnCancel.IsWrite = false;
			this.btnCancel.LanguageKey = "CANCEL";
			this.btnCancel.Location = new System.Drawing.Point(268, 10);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnCancel.Size = new System.Drawing.Size(75, 25);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "취소";
			this.btnCancel.TooltipLanguageKey = "";
			// 
			// btnConfirm
			// 
			this.btnConfirm.AllowFocus = false;
			this.btnConfirm.IsBusy = false;
			this.btnConfirm.IsWrite = false;
			this.btnConfirm.LanguageKey = "CONFIRM";
			this.btnConfirm.Location = new System.Drawing.Point(187, 10);
			this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnConfirm.Size = new System.Drawing.Size(75, 25);
			this.btnConfirm.TabIndex = 1;
			this.btnConfirm.Text = "확인";
			this.btnConfirm.TooltipLanguageKey = "";
			// 
			// MaterialSpecPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(366, 579);
			this.LanguageKey = "MATERIALSPEC";
			this.Name = "MaterialSpecPopup";
			this.Text = "MaterialSpecPopup";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdMaterialSpecList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Framework.SmartControls.SmartButton btnCancel;
		private Framework.SmartControls.SmartButton btnConfirm;
	}
}