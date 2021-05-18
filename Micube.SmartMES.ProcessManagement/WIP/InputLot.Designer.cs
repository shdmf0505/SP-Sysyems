namespace Micube.SmartMES.ProcessManagement
{
	partial class InputLot
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
			this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
			this.grdProductList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
			this.dtpCompleteDate = new Micube.Framework.SmartControls.SmartDateEdit();
			this.chkPrintLotCard = new Micube.Framework.SmartControls.SmartCheckBox();
			this.smartLabel6 = new Micube.Framework.SmartControls.SmartLabel();
			this.txtDueDate = new Micube.Framework.SmartControls.SmartTextBox();
			this.smartLabel5 = new Micube.Framework.SmartControls.SmartLabel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnInput = new Micube.Framework.SmartControls.SmartButton();
			this.spFrom = new Micube.Framework.SmartControls.SmartSpinEdit();
			this.spTo = new Micube.Framework.SmartControls.SmartSpinEdit();
			this.lblGap = new Micube.Framework.SmartControls.SmartLabel();
			this.btnApply = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
			this.pnlToolbar.SuspendLayout();
			this.pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
			this.smartSpliterContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
			this.smartPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtpCompleteDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtpCompleteDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDueDate.Properties)).BeginInit();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spFrom.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spTo.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlCondition
			// 
			this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
			this.pnlCondition.Size = new System.Drawing.Size(296, 609);
			// 
			// pnlToolbar
			// 
			this.pnlToolbar.Controls.Add(this.flowLayoutPanel2);
			this.pnlToolbar.Size = new System.Drawing.Size(1060, 24);
			this.pnlToolbar.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
			// 
			// pnlContent
			// 
			this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
			this.pnlContent.Appearance.Options.UseBackColor = true;
			this.pnlContent.Controls.Add(this.smartSpliterContainer1);
			this.pnlContent.Size = new System.Drawing.Size(1060, 613);
			// 
			// pnlMain
			// 
			this.pnlMain.Size = new System.Drawing.Size(1365, 642);
			// 
			// smartSpliterContainer1
			// 
			this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSpliterContainer1.Horizontal = false;
			this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
			this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSpliterContainer1.Name = "smartSpliterContainer1";
			this.smartSpliterContainer1.Panel1.Controls.Add(this.grdProductList);
			this.smartSpliterContainer1.Panel1.Text = "Panel1";
			this.smartSpliterContainer1.Panel2.Controls.Add(this.grdLotList);
			this.smartSpliterContainer1.Panel2.Controls.Add(this.smartPanel2);
			this.smartSpliterContainer1.Panel2.Text = "Panel2";
			this.smartSpliterContainer1.Size = new System.Drawing.Size(1060, 613);
			this.smartSpliterContainer1.SplitterPosition = 250;
			this.smartSpliterContainer1.TabIndex = 1;
			this.smartSpliterContainer1.Text = "smartSpliterContainer1";
			// 
			// grdProductList
			// 
			this.grdProductList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdProductList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdProductList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdProductList.IsUsePaging = false;
			this.grdProductList.LanguageKey = "GRIDPRODUCTLIST";
			this.grdProductList.Location = new System.Drawing.Point(0, 0);
			this.grdProductList.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.grdProductList.Name = "grdProductList";
			this.grdProductList.ShowBorder = true;
			this.grdProductList.Size = new System.Drawing.Size(1060, 250);
			this.grdProductList.TabIndex = 0;
			this.grdProductList.UseAutoBestFitColumns = false;
			// 
			// grdLotList
			// 
			this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLotList.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
			this.grdLotList.IsUsePaging = false;
			this.grdLotList.LanguageKey = "GRIDLOTLIST";
			this.grdLotList.Location = new System.Drawing.Point(0, 27);
			this.grdLotList.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.grdLotList.Name = "grdLotList";
			this.grdLotList.ShowBorder = true;
			this.grdLotList.Size = new System.Drawing.Size(1060, 331);
			this.grdLotList.TabIndex = 1;
			this.grdLotList.UseAutoBestFitColumns = false;
			// 
			// smartPanel2
			// 
			this.smartPanel2.AutoSize = true;
			this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smartPanel2.Controls.Add(this.btnApply);
			this.smartPanel2.Controls.Add(this.lblGap);
			this.smartPanel2.Controls.Add(this.spTo);
			this.smartPanel2.Controls.Add(this.spFrom);
			this.smartPanel2.Controls.Add(this.dtpCompleteDate);
			this.smartPanel2.Controls.Add(this.chkPrintLotCard);
			this.smartPanel2.Controls.Add(this.smartLabel6);
			this.smartPanel2.Controls.Add(this.txtDueDate);
			this.smartPanel2.Controls.Add(this.smartLabel5);
			this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.smartPanel2.Location = new System.Drawing.Point(0, 0);
			this.smartPanel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.smartPanel2.Name = "smartPanel2";
			this.smartPanel2.Size = new System.Drawing.Size(1060, 27);
			this.smartPanel2.TabIndex = 0;
			// 
			// dtpCompleteDate
			// 
			this.dtpCompleteDate.EditValue = null;
			this.dtpCompleteDate.LabelText = null;
			this.dtpCompleteDate.LanguageKey = null;
			this.dtpCompleteDate.Location = new System.Drawing.Point(308, 4);
			this.dtpCompleteDate.Name = "dtpCompleteDate";
			this.dtpCompleteDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtpCompleteDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dtpCompleteDate.Size = new System.Drawing.Size(100, 20);
			this.dtpCompleteDate.TabIndex = 4;
			// 
			// chkPrintLotCard
			// 
			this.chkPrintLotCard.LanguageKey = "LOTCARDPRINT";
			this.chkPrintLotCard.Location = new System.Drawing.Point(472, 5);
			this.chkPrintLotCard.Name = "chkPrintLotCard";
			this.chkPrintLotCard.Properties.Caption = "LOT CARD 출력";
			this.chkPrintLotCard.Size = new System.Drawing.Size(136, 19);
			this.chkPrintLotCard.TabIndex = 5;
			// 
			// smartLabel6
			// 
			this.smartLabel6.LanguageKey = "EXPECTPRODUCTDATE";
			this.smartLabel6.Location = new System.Drawing.Point(209, 7);
			this.smartLabel6.Name = "smartLabel6";
			this.smartLabel6.Size = new System.Drawing.Size(78, 14);
			this.smartLabel6.TabIndex = 4;
			this.smartLabel6.Text = "예상 생산 완료일";
			// 
			// txtDueDate
			// 
			this.txtDueDate.LabelText = null;
			this.txtDueDate.LanguageKey = null;
			this.txtDueDate.Location = new System.Drawing.Point(50, 4);
			this.txtDueDate.Name = "txtDueDate";
			this.txtDueDate.Properties.ReadOnly = true;
			this.txtDueDate.Size = new System.Drawing.Size(100, 20);
			this.txtDueDate.TabIndex = 3;
			// 
			// smartLabel5
			// 
			this.smartLabel5.LanguageKey = "DUEDATE";
			this.smartLabel5.Location = new System.Drawing.Point(5, 7);
			this.smartLabel5.Name = "smartLabel5";
			this.smartLabel5.Size = new System.Drawing.Size(30, 14);
			this.smartLabel5.TabIndex = 2;
			this.smartLabel5.Text = "납기일";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btnInput);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(47, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(1013, 24);
			this.flowLayoutPanel2.TabIndex = 5;
			// 
			// btnInput
			// 
			this.btnInput.AllowFocus = false;
			this.btnInput.IsBusy = false;
			this.btnInput.IsWrite = false;
			this.btnInput.Location = new System.Drawing.Point(938, 0);
			this.btnInput.Margin = new System.Windows.Forms.Padding(0);
			this.btnInput.Name = "btnInput";
			this.btnInput.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnInput.Size = new System.Drawing.Size(75, 23);
			this.btnInput.TabIndex = 0;
			this.btnInput.Text = "투입";
			this.btnInput.TooltipLanguageKey = "";
			// 
			// spFrom
			// 
			this.spFrom.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spFrom.LabelText = null;
			this.spFrom.LanguageKey = null;
			this.spFrom.Location = new System.Drawing.Point(602, 4);
			this.spFrom.Name = "spFrom";
			this.spFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.spFrom.Size = new System.Drawing.Size(100, 20);
			this.spFrom.TabIndex = 6;
			// 
			// spTo
			// 
			this.spTo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spTo.LabelText = null;
			this.spTo.LanguageKey = null;
			this.spTo.Location = new System.Drawing.Point(723, 4);
			this.spTo.Name = "spTo";
			this.spTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.spTo.Size = new System.Drawing.Size(100, 20);
			this.spTo.TabIndex = 7;
			// 
			// lblGap
			// 
			this.lblGap.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblGap.Location = new System.Drawing.Point(707, 4);
			this.lblGap.Name = "lblGap";
			this.lblGap.Size = new System.Drawing.Size(9, 20);
			this.lblGap.TabIndex = 8;
			this.lblGap.Text = "~";
			// 
			// btnApply
			// 
			this.btnApply.AllowFocus = false;
			this.btnApply.IsBusy = false;
			this.btnApply.IsWrite = false;
			this.btnApply.LanguageKey = "APPLY";
			this.btnApply.Location = new System.Drawing.Point(830, 3);
			this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnApply.Name = "btnApply";
			this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 9;
			this.btnApply.Text = "적용";
			this.btnApply.TooltipLanguageKey = "";
			// 
			// InputLot
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1385, 662);
			this.Name = "InputLot";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
			this.pnlToolbar.ResumeLayout(false);
			this.pnlToolbar.PerformLayout();
			this.pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
			this.smartSpliterContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
			this.smartPanel2.ResumeLayout(false);
			this.smartPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtpCompleteDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtpCompleteDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkPrintLotCard.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDueDate.Properties)).EndInit();
			this.flowLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spFrom.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spTo.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
		private Framework.SmartControls.SmartBandedGrid grdProductList;
		private Framework.SmartControls.SmartBandedGrid grdLotList;
		private Framework.SmartControls.SmartPanel smartPanel2;
		private Framework.SmartControls.SmartCheckBox chkPrintLotCard;
		private Framework.SmartControls.SmartLabel smartLabel6;
		private Framework.SmartControls.SmartTextBox txtDueDate;
		private Framework.SmartControls.SmartLabel smartLabel5;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Framework.SmartControls.SmartButton btnInput;
        private Framework.SmartControls.SmartDateEdit dtpCompleteDate;
		private Framework.SmartControls.SmartButton btnApply;
		private Framework.SmartControls.SmartLabel lblGap;
		private Framework.SmartControls.SmartSpinEdit spTo;
		private Framework.SmartControls.SmartSpinEdit spFrom;
	}
}