namespace Micube.SmartMES.ProcessManagement
{
	partial class InkjetQrPrint
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
            this.tabPartition = new Micube.Framework.SmartControls.SmartTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer1 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdWaitForPrint = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdWaitForPrintPcs = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.smartPanel1 = new Micube.Framework.SmartControls.SmartPanel();
            this.btnSelect = new Micube.Framework.SmartControls.SmartButton();
            this.lseEndNo = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.lseStartNo = new Micube.Framework.SmartControls.SmartLabelSpinEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdPrinted = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdPrintedPcs = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.smartSplitTableLayoutPanel2 = new Micube.Framework.SmartControls.SmartSplitTableLayoutPanel();
            this.btnCancel = new Micube.Framework.SmartControls.SmartButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).BeginInit();
            this.tabPartition.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lseEndNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lseStartNo.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.smartSplitTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(441, 278, 650, 400);
            this.pnlCondition.Size = new System.Drawing.Size(296, 691);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.smartSplitTableLayoutPanel2);
            this.pnlToolbar.Size = new System.Drawing.Size(879, 24);
            this.pnlToolbar.Controls.SetChildIndex(this.smartSplitTableLayoutPanel2, 0);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabPartition);
            this.pnlContent.Size = new System.Drawing.Size(879, 695);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1184, 724);
            // 
            // tabPartition
            // 
            this.tabPartition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPartition.Location = new System.Drawing.Point(0, 0);
            this.tabPartition.Name = "tabPartition";
            this.tabPartition.SelectedTabPage = this.xtraTabPage1;
            this.tabPartition.Size = new System.Drawing.Size(879, 695);
            this.tabPartition.TabIndex = 0;
            this.tabPartition.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.smartSpliterContainer1);
            this.tabPartition.SetLanguageKey(this.xtraTabPage1, "WAITTOPRINT");
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(873, 666);
            this.xtraTabPage1.Text = "출력대기";
            // 
            // smartSpliterContainer1
            // 
            this.smartSpliterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer1.Location = new System.Drawing.Point(0, 0);
            this.smartSpliterContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer1.Name = "smartSpliterContainer1";
            this.smartSpliterContainer1.Panel1.Controls.Add(this.grdWaitForPrint);
            this.smartSpliterContainer1.Panel1.Text = "Panel1";
            this.smartSpliterContainer1.Panel2.Controls.Add(this.grdWaitForPrintPcs);
            this.smartSpliterContainer1.Panel2.Controls.Add(this.smartPanel1);
            this.smartSpliterContainer1.Panel2.Text = "Panel2";
            this.smartSpliterContainer1.Size = new System.Drawing.Size(873, 666);
            this.smartSpliterContainer1.SplitterPosition = 656;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdWaitForPrint
            // 
            this.grdWaitForPrint.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWaitForPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWaitForPrint.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWaitForPrint.IsUsePaging = false;
            this.grdWaitForPrint.LanguageKey = "LOT";
            this.grdWaitForPrint.Location = new System.Drawing.Point(0, 0);
            this.grdWaitForPrint.Margin = new System.Windows.Forms.Padding(0);
            this.grdWaitForPrint.Name = "grdWaitForPrint";
            this.grdWaitForPrint.ShowBorder = true;
            this.grdWaitForPrint.ShowStatusBar = false;
            this.grdWaitForPrint.Size = new System.Drawing.Size(656, 666);
            this.grdWaitForPrint.TabIndex = 0;
            this.grdWaitForPrint.UseAutoBestFitColumns = false;
            // 
            // grdWaitForPrintPcs
            // 
            this.grdWaitForPrintPcs.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWaitForPrintPcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWaitForPrintPcs.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdWaitForPrintPcs.IsUsePaging = false;
            this.grdWaitForPrintPcs.LanguageKey = "PCS";
            this.grdWaitForPrintPcs.Location = new System.Drawing.Point(0, 29);
            this.grdWaitForPrintPcs.Margin = new System.Windows.Forms.Padding(0);
            this.grdWaitForPrintPcs.Name = "grdWaitForPrintPcs";
            this.grdWaitForPrintPcs.ShowBorder = true;
            this.grdWaitForPrintPcs.ShowStatusBar = false;
            this.grdWaitForPrintPcs.Size = new System.Drawing.Size(212, 637);
            this.grdWaitForPrintPcs.TabIndex = 1;
            this.grdWaitForPrintPcs.UseAutoBestFitColumns = false;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.btnSelect);
            this.smartPanel1.Controls.Add(this.lseEndNo);
            this.smartPanel1.Controls.Add(this.lseStartNo);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(212, 29);
            this.smartPanel1.TabIndex = 2;
            // 
            // btnSelect
            // 
            this.btnSelect.AllowFocus = false;
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.IsBusy = false;
            this.btnSelect.IsWrite = false;
            this.btnSelect.LanguageKey = "SELECT";
            this.btnSelect.Location = new System.Drawing.Point(127, 2);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnSelect.Size = new System.Drawing.Size(80, 25);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "선택";
            this.btnSelect.TooltipLanguageKey = "";
            // 
            // lseEndNo
            // 
            this.lseEndNo.LabelText = "마지막번호";
            this.lseEndNo.LanguageKey = "ENDNO";
            this.lseEndNo.Location = new System.Drawing.Point(231, 4);
            this.lseEndNo.Name = "lseEndNo";
            this.lseEndNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lseEndNo.Size = new System.Drawing.Size(220, 20);
            this.lseEndNo.TabIndex = 0;
            // 
            // lseStartNo
            // 
            this.lseStartNo.LabelText = "시작번호";
            this.lseStartNo.LanguageKey = "STARTNO";
            this.lseStartNo.Location = new System.Drawing.Point(5, 4);
            this.lseStartNo.Name = "lseStartNo";
            this.lseStartNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lseStartNo.Size = new System.Drawing.Size(220, 20);
            this.lseStartNo.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.smartSpliterContainer2);
            this.xtraTabPage2.Controls.Add(this.splitter1);
            this.tabPartition.SetLanguageKey(this.xtraTabPage2, "PRINTED");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(873, 666);
            this.xtraTabPage2.Text = "출력완료";
            // 
            // smartSpliterContainer2
            // 
            this.smartSpliterContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSpliterContainer2.Location = new System.Drawing.Point(0, 3);
            this.smartSpliterContainer2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSpliterContainer2.Name = "smartSpliterContainer2";
            this.smartSpliterContainer2.Panel1.Controls.Add(this.grdPrinted);
            this.smartSpliterContainer2.Panel1.Text = "Panel1";
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdPrintedPcs);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(873, 663);
            this.smartSpliterContainer2.SplitterPosition = 631;
            this.smartSpliterContainer2.TabIndex = 3;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdPrinted
            // 
            this.grdPrinted.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPrinted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPrinted.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPrinted.IsUsePaging = false;
            this.grdPrinted.LanguageKey = "LABELDEFINITIONLIST";
            this.grdPrinted.Location = new System.Drawing.Point(0, 0);
            this.grdPrinted.Margin = new System.Windows.Forms.Padding(0);
            this.grdPrinted.Name = "grdPrinted";
            this.grdPrinted.ShowBorder = true;
            this.grdPrinted.ShowStatusBar = false;
            this.grdPrinted.Size = new System.Drawing.Size(631, 663);
            this.grdPrinted.TabIndex = 0;
            this.grdPrinted.UseAutoBestFitColumns = false;
            // 
            // grdPrintedPcs
            // 
            this.grdPrintedPcs.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPrintedPcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPrintedPcs.GridButtonItem = ((Micube.Framework.SmartControls.GridButtonItem)((((((Micube.Framework.SmartControls.GridButtonItem.Add | Micube.Framework.SmartControls.GridButtonItem.Copy) 
            | Micube.Framework.SmartControls.GridButtonItem.Delete) 
            | Micube.Framework.SmartControls.GridButtonItem.Preview) 
            | Micube.Framework.SmartControls.GridButtonItem.Import) 
            | Micube.Framework.SmartControls.GridButtonItem.Export)));
            this.grdPrintedPcs.IsUsePaging = false;
            this.grdPrintedPcs.LanguageKey = "LABELCLASSLIST";
            this.grdPrintedPcs.Location = new System.Drawing.Point(0, 0);
            this.grdPrintedPcs.Margin = new System.Windows.Forms.Padding(0);
            this.grdPrintedPcs.Name = "grdPrintedPcs";
            this.grdPrintedPcs.ShowBorder = true;
            this.grdPrintedPcs.ShowStatusBar = false;
            this.grdPrintedPcs.Size = new System.Drawing.Size(237, 663);
            this.grdPrintedPcs.TabIndex = 2;
            this.grdPrintedPcs.UseAutoBestFitColumns = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(873, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // smartSplitTableLayoutPanel2
            // 
            this.smartSplitTableLayoutPanel2.ColumnCount = 2;
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.smartSplitTableLayoutPanel2.Controls.Add(this.btnCancel, 3, 0);
            this.smartSplitTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartSplitTableLayoutPanel2.Location = new System.Drawing.Point(47, 0);
            this.smartSplitTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.smartSplitTableLayoutPanel2.Name = "smartSplitTableLayoutPanel2";
            this.smartSplitTableLayoutPanel2.RowCount = 1;
            this.smartSplitTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.smartSplitTableLayoutPanel2.Size = new System.Drawing.Size(832, 24);
            this.smartSplitTableLayoutPanel2.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.IsBusy = false;
            this.btnCancel.IsWrite = false;
            this.btnCancel.LanguageKey = "CANCEL";
            this.btnCancel.Location = new System.Drawing.Point(750, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.TooltipLanguageKey = "";
            // 
            // InkjetQrPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 762);
            this.Name = "InkjetQrPrint";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).EndInit();
            this.tabPartition.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lseEndNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lseStartNo.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
            this.smartSplitTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Framework.SmartControls.SmartTabControl tabPartition;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
		private System.Windows.Forms.Splitter splitter1;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer1;
        private Framework.SmartControls.SmartBandedGrid grdWaitForPrint;
        private Framework.SmartControls.SmartBandedGrid grdWaitForPrintPcs;
        private Framework.SmartControls.SmartSpliterContainer smartSpliterContainer2;
        private Framework.SmartControls.SmartBandedGrid grdPrinted;
        private Framework.SmartControls.SmartBandedGrid grdPrintedPcs;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartButton btnSelect;
        private Framework.SmartControls.SmartLabelSpinEdit lseEndNo;
        private Framework.SmartControls.SmartLabelSpinEdit lseStartNo;
        private Framework.SmartControls.SmartSplitTableLayoutPanel smartSplitTableLayoutPanel2;
        private Framework.SmartControls.SmartButton btnCancel;
    }
}
