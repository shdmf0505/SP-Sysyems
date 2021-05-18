namespace Micube.SmartMES.ProcessManagement
{
	partial class PrintPackingLabelPopup
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
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            this.btnPrint = new Micube.Framework.SmartControls.SmartButton();
            this.smartSplitTableLayoutPanel1 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.slcbPrintBoxLabel = new Micube.Framework.SmartControls.SmartLabelCheckedComboBox();
            this.ssePrintCount = new Micube.Framework.SmartControls.SmartSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.smartSplitTableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slcbPrintBoxLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ssePrintCount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.smartSplitTableLayoutPanel1);
            this.pnlMain.Size = new System.Drawing.Size(731, 511);
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(648, 1);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(77, 22);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Close";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.IsBusy = false;
            this.btnPrint.IsWrite = false;
            this.btnPrint.LanguageKey = "PRINT";
            this.btnPrint.Location = new System.Drawing.Point(568, 1);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnPrint.Size = new System.Drawing.Size(74, 22);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.TooltipLanguageKey = "";
            // 
            // smartSplitTableLayoutPanel1
            // 
            this.smartSplitTableLayoutPanel1.ColumnCount = 1;
            this.smartSplitTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.Controls.Add(this.xtraTabControl1, 0, 0);
            this.smartSplitTableLayoutPanel1.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.smartSplitTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartSplitTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel1.Name = "smartSplitTableLayoutPanel1";
            this.smartSplitTableLayoutPanel1.RowCount = 2;
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.smartSplitTableLayoutPanel1.Size = new System.Drawing.Size(731, 511);
            this.smartSplitTableLayoutPanel1.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new System.Drawing.Size(731, 481);
            this.xtraTabControl1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Controls.Add(this.btnPrint, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.slcbPrintBoxLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ssePrintCount, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 484);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(725, 24);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // slcbPrintBoxLabel
            // 
            this.slcbPrintBoxLabel.AutoHeight = false;
            this.slcbPrintBoxLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slcbPrintBoxLabel.EditorWidth = "80%";
            this.slcbPrintBoxLabel.LabelText = "출력 BOX 라벨 선택";
            this.slcbPrintBoxLabel.LanguageKey = "SELECTPRINTBOXLABEL";
            this.slcbPrintBoxLabel.Location = new System.Drawing.Point(3, 1);
            this.slcbPrintBoxLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.slcbPrintBoxLabel.Name = "slcbPrintBoxLabel";
            this.slcbPrintBoxLabel.Properties.AutoHeight = false;
            this.slcbPrintBoxLabel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slcbPrintBoxLabel.Properties.DisplayMember = null;
            this.slcbPrintBoxLabel.Properties.PopupWidth = 400;
            this.slcbPrintBoxLabel.Properties.ShowHeader = true;
            this.slcbPrintBoxLabel.Properties.ValueMember = null;
            this.slcbPrintBoxLabel.Properties.VisibleColumns = null;
            this.slcbPrintBoxLabel.Properties.VisibleColumnsWidth = null;
            this.slcbPrintBoxLabel.Size = new System.Drawing.Size(489, 22);
            this.slcbPrintBoxLabel.TabIndex = 3;
            // 
            // ssePrintCount
            // 
            this.ssePrintCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssePrintCount.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ssePrintCount.LabelText = null;
            this.ssePrintCount.LanguageKey = null;
            this.ssePrintCount.Location = new System.Drawing.Point(498, 1);
            this.ssePrintCount.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.ssePrintCount.Name = "ssePrintCount";
            this.ssePrintCount.Properties.AutoHeight = false;
            this.ssePrintCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ssePrintCount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ssePrintCount.Properties.Mask.EditMask = "d";
            this.ssePrintCount.Properties.MaxValue = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.ssePrintCount.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ssePrintCount.Size = new System.Drawing.Size(64, 22);
            this.ssePrintCount.TabIndex = 4;
            // 
            // PrintPackingLabelPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 531);
            this.LanguageKey = "LABELVIEW";
            this.Name = "PrintPackingLabelPopup";
            this.Text = "AddImagePopup";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.smartSplitTableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slcbPrintBoxLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ssePrintCount.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel1;
		private Framework.SmartControls.SmartButton btnCancel;
        private Framework.SmartControls.SmartButton btnPrint;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private Framework.SmartControls.SmartLabelCheckedComboBox slcbPrintBoxLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Framework.SmartControls.SmartSpinEdit ssePrintCount;
    }
}