namespace Micube.SmartMES.ProductManagement
{
	partial class PriorityApplyStandard
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
			this.grdDispachingList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSpliterControl1 = new Micube.Framework.SmartControls.SmartSpliterControl();
			this.grdSetPriorityList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnSimulation = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlToolbar.SuspendLayout();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.Location = new System.Drawing.Point(2, 31);
			this.pnlCondition.Size = new System.Drawing.Size(296, 623);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
			this.pnlToolbar.Size = new System.Drawing.Size(889, 24);
			this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.grdSetPriorityList);
			this.pnlContent.Controls.Add(this.smartSpliterControl1);
			this.pnlContent.Controls.Add(this.grdDispachingList);
			this.pnlContent.Size = new System.Drawing.Size(889, 627);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(1194, 656);
			// 
			// grdDispachingList
			// 
			this.grdDispachingList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdDispachingList.Dock = System.Windows.Forms.DockStyle.Left;
			this.grdDispachingList.IsUsePaging = false;
			this.grdDispachingList.LanguageKey = "DISPATCHINGITEMLIST";
			this.grdDispachingList.Location = new System.Drawing.Point(0, 0);
			this.grdDispachingList.Margin = new System.Windows.Forms.Padding(0);
			this.grdDispachingList.Name = "grdDispachingList";
			this.grdDispachingList.ShowBorder = true;
			this.grdDispachingList.Size = new System.Drawing.Size(700, 627);
			this.grdDispachingList.TabIndex = 0;
			// 
			// smartSpliterControl1
			// 
			this.smartSpliterControl1.Location = new System.Drawing.Point(700, 0);
			this.smartSpliterControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterControl1.Name = "smartSpliterControl1";
			this.smartSpliterControl1.Size = new System.Drawing.Size(5, 627);
			this.smartSpliterControl1.TabIndex = 1;
			this.smartSpliterControl1.TabStop = false;
			// 
			// grdSetPriorityList
			// 
			this.grdSetPriorityList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdSetPriorityList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSetPriorityList.IsUsePaging = false;
			this.grdSetPriorityList.LanguageKey = "PRIORITYLIST";
			this.grdSetPriorityList.Location = new System.Drawing.Point(705, 0);
			this.grdSetPriorityList.Margin = new System.Windows.Forms.Padding(0);
			this.grdSetPriorityList.Name = "grdSetPriorityList";
			this.grdSetPriorityList.ShowBorder = true;
			this.grdSetPriorityList.Size = new System.Drawing.Size(184, 627);
			this.grdSetPriorityList.TabIndex = 2;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btnSimulation);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(842, 24);
			this.flowLayoutPanel2.TabIndex = 5;
			// 
			// btnSimulation
			// 
			this.btnSimulation.AllowFocus = false;
			this.btnSimulation.IsBusy = false;
			this.btnSimulation.LanguageKey = "SIMULATION";
			this.btnSimulation.Location = new System.Drawing.Point(764, 0);
			this.btnSimulation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnSimulation.Name = "btnSimulation";
			this.btnSimulation.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSimulation.Size = new System.Drawing.Size(75, 23);
			this.btnSimulation.TabIndex = 0;
			this.btnSimulation.Text = "시뮬레이션";
			this.btnSimulation.TooltipLanguageKey = "";
			// 
			// PriorityApplyStandard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1214, 676);
			this.Name = "PriorityApplyStandard";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlToolbar.ResumeLayout(false);
			this.pnlToolbar.PerformLayout();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartBandedGrid grdDispachingList;
		private Framework.SmartControls.SmartBandedGrid grdSetPriorityList;
		private Framework.SmartControls.SmartSpliterControl smartSpliterControl1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Framework.SmartControls.SmartButton btnSimulation;
	}
}