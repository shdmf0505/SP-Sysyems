namespace Micube.SmartMES.ProductManagement
{
	partial class SimulationPopup
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
			this.tlpBaseLayout = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.pnlCondition = new Micube.Framework.SmartControls.SmartPanel();
			this.btnRunSimulation = new Micube.Framework.SmartControls.SmartButton();
			this.popArea = new Micube.Framework.SmartControls.SmartLabelSelectPopupEdit();
			this.flpConfrim = new System.Windows.Forms.FlowLayoutPanel();
			this.btnClose = new Micube.Framework.SmartControls.SmartButton();
			this.grdSimulationList = new Micube.Framework.SmartControls.SmartBandedGrid();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.tlpBaseLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlCondition.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.popArea.Properties)).BeginInit();
			this.flpConfrim.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.tlpBaseLayout);
			this.pnlMain.Size = new System.Drawing.Size(1464, 741);
			// 
			// tlpBaseLayout
			// 
			this.tlpBaseLayout.ColumnCount = 1;
			this.tlpBaseLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpBaseLayout.Controls.Add(this.pnlCondition, 0, 0);
			this.tlpBaseLayout.Controls.Add(this.flpConfrim, 0, 4);
			this.tlpBaseLayout.Controls.Add(this.grdSimulationList, 0, 2);
			this.tlpBaseLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpBaseLayout.Location = new System.Drawing.Point(0, 0);
			this.tlpBaseLayout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.tlpBaseLayout.Name = "tlpBaseLayout";
			this.tlpBaseLayout.RowCount = 5;
			this.tlpBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tlpBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.tlpBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.tlpBaseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tlpBaseLayout.Size = new System.Drawing.Size(1464, 741);
			this.tlpBaseLayout.TabIndex = 0;
			// 
			// pnlCondition
			// 
			this.pnlCondition.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlCondition.Controls.Add(this.btnRunSimulation);
			this.pnlCondition.Controls.Add(this.popArea);
			this.pnlCondition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCondition.Location = new System.Drawing.Point(0, 0);
			this.pnlCondition.Margin = new System.Windows.Forms.Padding(0);
			this.pnlCondition.Name = "pnlCondition";
			this.pnlCondition.Size = new System.Drawing.Size(1464, 26);
			this.pnlCondition.TabIndex = 0;
			// 
			// btnRunSimulation
			// 
			this.btnRunSimulation.AllowFocus = false;
			this.btnRunSimulation.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnRunSimulation.IsBusy = false;
			this.btnRunSimulation.LanguageKey = "SIMULATION";
			this.btnRunSimulation.Location = new System.Drawing.Point(1389, 2);
			this.btnRunSimulation.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnRunSimulation.Name = "btnRunSimulation";
			this.btnRunSimulation.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnRunSimulation.Size = new System.Drawing.Size(75, 23);
			this.btnRunSimulation.TabIndex = 1;
			this.btnRunSimulation.Text = "시뮬레이션";
			this.btnRunSimulation.TooltipLanguageKey = "";
			// 
			// popArea
			// 
			this.popArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.popArea.LabelText = "작업장";
			this.popArea.LanguageKey = "AREA";
			this.popArea.Location = new System.Drawing.Point(3, 3);
			this.popArea.Name = "popArea";
			this.popArea.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.popArea.Size = new System.Drawing.Size(220, 20);
			this.popArea.TabIndex = 0;
			// 
			// flpConfrim
			// 
			this.flpConfrim.Controls.Add(this.btnClose);
			this.flpConfrim.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpConfrim.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flpConfrim.Location = new System.Drawing.Point(0, 715);
			this.flpConfrim.Margin = new System.Windows.Forms.Padding(0);
			this.flpConfrim.Name = "flpConfrim";
			this.flpConfrim.Size = new System.Drawing.Size(1464, 26);
			this.flpConfrim.TabIndex = 1;
			// 
			// btnClose
			// 
			this.btnClose.AllowFocus = false;
			this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnClose.Enabled = false;
			this.btnClose.IsBusy = false;
			this.btnClose.LanguageKey = "CLOSE";
			this.btnClose.Location = new System.Drawing.Point(1389, 0);
			this.btnClose.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "닫기";
			this.btnClose.TooltipLanguageKey = "";
			// 
			// grdSimulationList
			// 
			this.grdSimulationList.Caption = "";
			this.grdSimulationList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSimulationList.IsUsePaging = false;
			this.grdSimulationList.LanguageKey = null;
			this.grdSimulationList.Location = new System.Drawing.Point(0, 31);
			this.grdSimulationList.Margin = new System.Windows.Forms.Padding(0);
			this.grdSimulationList.Name = "grdSimulationList";
			this.grdSimulationList.ShowBorder = true;
			this.grdSimulationList.Size = new System.Drawing.Size(1464, 679);
			this.grdSimulationList.TabIndex = 2;
			// 
			// SimulationPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1484, 761);
			this.Name = "SimulationPopup";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.tlpBaseLayout.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlCondition.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.popArea.Properties)).EndInit();
			this.flpConfrim.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel tlpBaseLayout;
		private Framework.SmartControls.SmartPanel pnlCondition;
		private Framework.SmartControls.SmartButton btnRunSimulation;
		private Framework.SmartControls.SmartLabelSelectPopupEdit popArea;
		private System.Windows.Forms.FlowLayoutPanel flpConfrim;
		private Framework.SmartControls.SmartButton btnClose;
		private Framework.SmartControls.SmartBandedGrid grdSimulationList;
	}
}