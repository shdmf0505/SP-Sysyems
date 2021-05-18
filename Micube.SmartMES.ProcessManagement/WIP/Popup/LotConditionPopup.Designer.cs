namespace Micube.SmartMES.ProcessManagement
{
	partial class LotConditionPopup
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
			this.btnApply = new Micube.Framework.SmartControls.SmartButton();
			this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.grdLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartSplitTableLayoutPanel3 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
			this.btnPlus = new Micube.Framework.SmartControls.SmartButton();
			this.btnMinus = new Micube.Framework.SmartControls.SmartButton();
			this.grdApplyLotList = new Micube.Framework.SmartControls.SmartBandedGrid();
			this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
			this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
			this.cboArea = new Micube.Framework.SmartControls.SmartComboBox();
			this.smartLabel5 = new Micube.Framework.SmartControls.SmartLabel();
			this.cboProcessSeg = new Micube.Framework.SmartControls.SmartComboBox();
			this.smartLabel4 = new Micube.Framework.SmartControls.SmartLabel();
			this.cboProductionType = new Micube.Framework.SmartControls.SmartComboBox();
			this.btnSearch = new Micube.Framework.SmartControls.SmartButton();
			this.smartLabel3 = new Micube.Framework.SmartControls.SmartLabel();
			this.txtLotId = new Micube.Framework.SmartControls.SmartTextBox();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.smartSplitTableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.smartSplitTableLayoutPanel2.SuspendLayout();
			this.smartSplitTableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
			this.smartPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboProcessSeg.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboProductionType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
			this.pnlMain.Size = new System.Drawing.Size(1464, 641);
			// 
			// smartSplitTableLayoutPanel1
			// 
			this.smartSplitTableLayoutPanel1.AutoScroll = true;
			this.smartSplitTableLayoutPanel1.ColumnCount = 1;
			this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.smartSplitTableLayoutPanel2, 0, 1);
			this.smartSplitTableLayoutPanel1.Controls.Add(this.smartPanel1, 0, 0);
			this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
			this.smartSplitTableLayoutPanel1.RowCount = 3;
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(1464, 641);
			this.smartSplitTableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnCancel);
			this.flowLayoutPanel1.Controls.Add(this.btnApply);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 618);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1464, 23);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.AllowFocus = false;
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.IsBusy = false;
			this.btnCancel.LanguageKey = "CANCEL";
			this.btnCancel.Location = new System.Drawing.Point(1389, 0);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.TooltipLanguageKey = "";
			// 
			// btnApply
			// 
			this.btnApply.AllowFocus = false;
			this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnApply.IsBusy = false;
			this.btnApply.LanguageKey = "APPLY";
			this.btnApply.Location = new System.Drawing.Point(1308, 0);
			this.btnApply.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnApply.Name = "btnApply";
			this.btnApply.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 1;
			this.btnApply.Text = "Apply";
			this.btnApply.TooltipLanguageKey = "";
			// 
			// smartSplitTableLayoutPanel2
			// 
			this.smartSplitTableLayoutPanel2.ColumnCount = 3;
			this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.smartSplitTableLayoutPanel2.Controls.Add(this.grdLotList, 0, 0);
			this.smartSplitTableLayoutPanel2.Controls.Add(this.smartSplitTableLayoutPanel3, 1, 0);
			this.smartSplitTableLayoutPanel2.Controls.Add(this.grdApplyLotList, 2, 0);
			this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(0, 28);
			this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
			this.smartSplitTableLayoutPanel2.RowCount = 1;
			this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(1464, 587);
			this.smartSplitTableLayoutPanel2.TabIndex = 0;
			// 
			// grdLotList
			// 
			this.grdLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLotList.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.None;
			this.grdLotList.IsUsePaging = false;
			this.grdLotList.LanguageKey = "GRIDLOTLIST";
			this.grdLotList.Location = new System.Drawing.Point(0, 10);
			this.grdLotList.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.grdLotList.Name = "grdLotList";
			this.grdLotList.ShowBorder = true;
			this.grdLotList.Size = new System.Drawing.Size(712, 577);
			this.grdLotList.TabIndex = 0;
			// 
			// smartSplitTableLayoutPanel3
			// 
			this.smartSplitTableLayoutPanel3.ColumnCount = 1;
			this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartSplitTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.smartSplitTableLayoutPanel3.Controls.Add(this.btnPlus, 0, 1);
			this.smartSplitTableLayoutPanel3.Controls.Add(this.btnMinus, 0, 3);
			this.smartSplitTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartSplitTableLayoutPanel3.Location = new System.Drawing.Point(712, 10);
			this.smartSplitTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.smartSplitTableLayoutPanel3.Name = "smartSplitTableLayoutPanel3";
			this.smartSplitTableLayoutPanel3.RowCount = 5;
			this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.smartSplitTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.smartSplitTableLayoutPanel3.Size = new System.Drawing.Size(40, 577);
			this.smartSplitTableLayoutPanel3.TabIndex = 1;
			// 
			// btnPlus
			// 
			this.btnPlus.AllowFocus = false;
			this.btnPlus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnPlus.IsBusy = false;
			this.btnPlus.Location = new System.Drawing.Point(3, 238);
			this.btnPlus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnPlus.Name = "btnPlus";
			this.btnPlus.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnPlus.Size = new System.Drawing.Size(34, 40);
			this.btnPlus.TabIndex = 0;
			this.btnPlus.Text = ">";
			this.btnPlus.TooltipLanguageKey = "";
			// 
			// btnMinus
			// 
			this.btnMinus.AllowFocus = false;
			this.btnMinus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnMinus.IsBusy = false;
			this.btnMinus.Location = new System.Drawing.Point(3, 298);
			this.btnMinus.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.btnMinus.Name = "btnMinus";
			this.btnMinus.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnMinus.Size = new System.Drawing.Size(34, 40);
			this.btnMinus.TabIndex = 1;
			this.btnMinus.Text = "<";
			this.btnMinus.TooltipLanguageKey = "";
			// 
			// grdApplyLotList
			// 
			this.grdApplyLotList.Caption = "그리드제목( LanguageKey를 입력하세요)";
			this.grdApplyLotList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdApplyLotList.GridButtonItem = Micube.Framework.SmartControls.GridButtonItem.None;
			this.grdApplyLotList.IsUsePaging = false;
			this.grdApplyLotList.LanguageKey = null;
			this.grdApplyLotList.Location = new System.Drawing.Point(752, 10);
			this.grdApplyLotList.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.grdApplyLotList.Name = "grdApplyLotList";
			this.grdApplyLotList.ShowBorder = true;
			this.grdApplyLotList.Size = new System.Drawing.Size(712, 577);
			this.grdApplyLotList.TabIndex = 2;
			// 
			// smartPanel1
			// 
			this.smartPanel1.AutoSize = true;
			this.smartPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smartPanel1.Controls.Add(this.txtLotId);
			this.smartPanel1.Controls.Add(this.smartLabel1);
			this.smartPanel1.Controls.Add(this.cboArea);
			this.smartPanel1.Controls.Add(this.smartLabel5);
			this.smartPanel1.Controls.Add(this.cboProcessSeg);
			this.smartPanel1.Controls.Add(this.smartLabel4);
			this.smartPanel1.Controls.Add(this.cboProductionType);
			this.smartPanel1.Controls.Add(this.smartLabel3);
			this.smartPanel1.Controls.Add(this.btnSearch);
			this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smartPanel1.Location = new System.Drawing.Point(0, 0);
			this.smartPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.smartPanel1.Name = "smartPanel1";
			this.smartPanel1.Size = new System.Drawing.Size(1464, 28);
			this.smartPanel1.TabIndex = 2;
			// 
			// smartLabel1
			// 
			this.smartLabel1.LanguageKey = "AREA";
			this.smartLabel1.Location = new System.Drawing.Point(4, 8);
			this.smartLabel1.Name = "smartLabel1";
			this.smartLabel1.Size = new System.Drawing.Size(30, 14);
			this.smartLabel1.TabIndex = 22;
			this.smartLabel1.Text = "작업장";
			// 
			// cboArea
			// 
			this.cboArea.LabelText = null;
			this.cboArea.LanguageKey = null;
			this.cboArea.Location = new System.Drawing.Point(55, 5);
			this.cboArea.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
			this.cboArea.Name = "cboArea";
			this.cboArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboArea.Properties.NullText = "";
			this.cboArea.ShowHeader = true;
			this.cboArea.Size = new System.Drawing.Size(100, 20);
			this.cboArea.TabIndex = 31;
			// 
			// smartLabel5
			// 
			this.smartLabel5.LanguageKey = "MAINPROCESSSEGMENT";
			this.smartLabel5.Location = new System.Drawing.Point(162, 8);
			this.smartLabel5.Name = "smartLabel5";
			this.smartLabel5.Size = new System.Drawing.Size(40, 14);
			this.smartLabel5.TabIndex = 28;
			this.smartLabel5.Text = "주요공정";
			// 
			// cboProcessSeg
			// 
			this.cboProcessSeg.LabelText = null;
			this.cboProcessSeg.LanguageKey = null;
			this.cboProcessSeg.Location = new System.Drawing.Point(223, 5);
			this.cboProcessSeg.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
			this.cboProcessSeg.Name = "cboProcessSeg";
			this.cboProcessSeg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboProcessSeg.Properties.NullText = "";
			this.cboProcessSeg.ShowHeader = true;
			this.cboProcessSeg.Size = new System.Drawing.Size(100, 20);
			this.cboProcessSeg.TabIndex = 29;
			// 
			// smartLabel4
			// 
			this.smartLabel4.LanguageKey = "";
			this.smartLabel4.Location = new System.Drawing.Point(330, 8);
			this.smartLabel4.Name = "smartLabel4";
			this.smartLabel4.Size = new System.Drawing.Size(40, 14);
			this.smartLabel4.TabIndex = 30;
			this.smartLabel4.Text = "양산구분";
			// 
			// cboProductionType
			// 
			this.cboProductionType.LabelText = null;
			this.cboProductionType.LanguageKey = null;
			this.cboProductionType.Location = new System.Drawing.Point(391, 5);
			this.cboProductionType.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
			this.cboProductionType.Name = "cboProductionType";
			this.cboProductionType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboProductionType.Properties.NullText = "";
			this.cboProductionType.ShowHeader = true;
			this.cboProductionType.Size = new System.Drawing.Size(100, 20);
			this.cboProductionType.TabIndex = 27;
			// 
			// btnSearch
			// 
			this.btnSearch.AllowFocus = false;
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.IsBusy = false;
			this.btnSearch.LanguageKey = "SEARCH";
			this.btnSearch.Location = new System.Drawing.Point(1389, 4);
			this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 32;
			this.btnSearch.Text = "Search";
			this.btnSearch.TooltipLanguageKey = "";
			// 
			// smartLabel3
			// 
			this.smartLabel3.LanguageKey = "";
			this.smartLabel3.Location = new System.Drawing.Point(498, 8);
			this.smartLabel3.Name = "smartLabel3";
			this.smartLabel3.Size = new System.Drawing.Size(32, 14);
			this.smartLabel3.TabIndex = 26;
			this.smartLabel3.Text = "LOT#";
			// 
			// txtLotId
			// 
			this.txtLotId.LabelText = null;
			this.txtLotId.LanguageKey = null;
			this.txtLotId.Location = new System.Drawing.Point(551, 5);
			this.txtLotId.Name = "txtLotId";
			this.txtLotId.Size = new System.Drawing.Size(100, 20);
			this.txtLotId.TabIndex = 33;
			// 
			// LotConditionPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1484, 661);
			this.Name = "LotConditionPopup";
			this.Text = "SmartConditionBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.ResumeLayout(false);
			this.smartSplitTableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.smartSplitTableLayoutPanel2.ResumeLayout(false);
			this.smartSplitTableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
			this.smartPanel1.ResumeLayout(false);
			this.smartPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboArea.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboProcessSeg.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboProductionType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLotId.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Framework.SmartControls.SmartButton btnCancel;
		private Framework.SmartControls.SmartButton btnApply;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
		private Framework.SmartControls.SmartBandedGrid grdLotList;
		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel3;
		private Framework.SmartControls.SmartButton btnPlus;
		private Framework.SmartControls.SmartButton btnMinus;
		private Framework.SmartControls.SmartBandedGrid grdApplyLotList;
		private Framework.SmartControls.SmartPanel smartPanel1;
		private Framework.SmartControls.SmartLabel smartLabel1;
		private Framework.SmartControls.SmartComboBox cboArea;
		private Framework.SmartControls.SmartLabel smartLabel5;
		private Framework.SmartControls.SmartComboBox cboProcessSeg;
		private Framework.SmartControls.SmartLabel smartLabel4;
		private Framework.SmartControls.SmartComboBox cboProductionType;
		private Framework.SmartControls.SmartButton btnSearch;
		private Framework.SmartControls.SmartTextBox txtLotId;
		private Framework.SmartControls.SmartLabel smartLabel3;
	}
}