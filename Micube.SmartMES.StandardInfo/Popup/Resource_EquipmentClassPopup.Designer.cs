namespace Micube.SmartMES.StandardInfo.Popup
{
	partial class Resource_EquipmentClassPopup
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
			Micube.Framework.SmartControls.ConditionCollection conditionCollection1 = new Micube.Framework.SmartControls.ConditionCollection();
			this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.grdEquipmentClassList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
			this.btnSave = new Micube.Framework.SmartControls.SmartButton();
			this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
			this.layout = new Micube.Framework.SmartControls.SmartLayout();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.smartPanel2 = new Micube.Framework.SmartControls.SmartPanel();
			this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
			this.smartPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).BeginInit();
			this.smartPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(811, 430);
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.grdEquipmentClassList, 0, 1);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 3;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(811, 430);
			this.smartSplitTableLayoutPanel1.TabIndex = 0;
			// 
			// grdEquipmentClassList
			// 
			this.grdEquipmentClassList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdEquipmentClassList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdEquipmentClassList.IsUsePaging = false;
			this.grdEquipmentClassList.LanguageKey = null;
			this.grdEquipmentClassList.Location = new System.Drawing.Point(0, 70);
			this.grdEquipmentClassList.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.grdEquipmentClassList.Name = "grdEquipmentClassList";
			this.grdEquipmentClassList.ShowBorder = true;
			this.grdEquipmentClassList.Size = new System.Drawing.Size(811, 327);
			this.grdEquipmentClassList.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Controls.Add(this.btnSave);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 407);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(811, 23);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.AllowFocus = false;
			this.btnCancel.IsBusy = false;
			this.btnCancel.LanguageKey = "CANCEL";
			this.btnCancel.Location = new System.Drawing.Point(736, 0);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "smartButton1";
			this.btnCancel.TooltipLanguageKey = "";
			// 
			// btnSave
			// 
			this.btnSave.AllowFocus = false;
			this.btnSave.IsBusy = false;
			this.btnSave.LanguageKey = "SAVE";
			this.btnSave.Location = new System.Drawing.Point(655, 0);
			this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "smartButton2";
			this.btnSave.TooltipLanguageKey = "";
			// 
			// smartPanel1
			// 
			this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smartPanel1.Controls.Add(this.layout);
			this.smartPanel1.Controls.Add(this.smartPanel2);
			this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.smartPanel1.Name = "smartPanel1";
			this.smartPanel1.Size = new System.Drawing.Size(811, 60);
			this.smartPanel1.TabIndex = 4;
			// 
			// layout
			// 
			this.layout.Conditions = conditionCollection1;
			this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layout.IsBusy = false;
			this.layout.LayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
			this.layout.Location = new System.Drawing.Point(0, 0);
			this.layout.Margin = new System.Windows.Forms.Padding(0);
			this.layout.Name = "layout";
			this.layout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(926, 541, 650, 400);
			this.layout.Root = this.layoutControlGroup1;
			this.layout.Size = new System.Drawing.Size(736, 60);
			this.layout.TabIndex = 2;
			this.layout.Text = "smartLayout1";
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.DefaultLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(736, 60);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// smartPanel2
			// 
			this.smartPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smartPanel2.Controls.Add(this.btnSearch);
			this.smartPanel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.smartPanel2.Location = new System.Drawing.Point(736, 0);
			this.smartPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.smartPanel2.Name = "smartPanel2";
			this.smartPanel2.Size = new System.Drawing.Size(75, 60);
			this.smartPanel2.TabIndex = 3;
			// 
			// btnSearch
			// 
			this.btnSearch.AllowFocus = false;
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.IsBusy = false;
			this.btnSearch.Location = new System.Drawing.Point(0, 0);
			this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 0;
			this.btnSearch.Text = "smartButton1";
			this.btnSearch.TooltipLanguageKey = "";
			// 
			// Resource_EquipmentClassPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(831, 450);
			this.Name = "Resource_EquipmentClassPopup";
			this.Text = "Resource_EquipmentClassPopup";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
			this.smartPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel2)).EndInit();
			this.smartPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartBandedGrid grdEquipmentClassList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Framework.SmartControls.SmartButton btnCancel;
		private Framework.SmartControls.SmartButton btnSave;
		private Framework.SmartControls.SmartLayout layout;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private Framework.SmartControls.SmartPanel smartPanel1;
		private Framework.SmartControls.SmartPanel smartPanel2;
		private Framework.SmartControls.SmartButton btnSearch;
    }
}