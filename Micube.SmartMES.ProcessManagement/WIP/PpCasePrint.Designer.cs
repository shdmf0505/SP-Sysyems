namespace Micube.SmartMES.ProcessManagement
{
	partial class PpCasePrint
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
            this.txtCurrentSeq = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel2 = new Micube.Framework.SmartControls.SmartLabel();
            this.txtPcsNoScan = new Micube.Framework.SmartControls.SmartTextBox();
            this.smartLabel1 = new Micube.Framework.SmartControls.SmartLabel();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.smartSpliterContainer2 = new Micube.Framework.SmartControls.SmartSpliterContainer();
            this.grdPrinted = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.grdPrintedPpCase = new Micube.Framework.SmartControls.SmartBandedGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).BeginInit();
            this.tabPartition.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).BeginInit();
            this.smartSpliterContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).BeginInit();
            this.smartPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentSeq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsNoScan.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).BeginInit();
            this.smartSpliterContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCondition
            // 
            this.pnlCondition.Location = new System.Drawing.Point(2, 31);
            this.pnlCondition.Size = new System.Drawing.Size(296, 691);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Size = new System.Drawing.Size(1109, 24);
            // 
            // pnlContent
            // 
            this.pnlContent.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlContent.Appearance.Options.UseBackColor = true;
            this.pnlContent.Controls.Add(this.tabPartition);
            this.pnlContent.Size = new System.Drawing.Size(1109, 695);
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(19, 19);
            this.pnlMain.Size = new System.Drawing.Size(1414, 724);
            // 
            // tabPartition
            // 
            this.tabPartition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPartition.Location = new System.Drawing.Point(0, 0);
            this.tabPartition.Name = "tabPartition";
            this.tabPartition.SelectedTabPage = this.xtraTabPage1;
            this.tabPartition.Size = new System.Drawing.Size(1109, 695);
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
            this.xtraTabPage1.Size = new System.Drawing.Size(1103, 666);
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
            this.smartSpliterContainer1.Size = new System.Drawing.Size(1103, 666);
            this.smartSpliterContainer1.SplitterPosition = 614;
            this.smartSpliterContainer1.TabIndex = 1;
            this.smartSpliterContainer1.Text = "smartSpliterContainer1";
            // 
            // grdWaitForPrint
            // 
            this.grdWaitForPrint.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWaitForPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWaitForPrint.IsUsePaging = false;
            this.grdWaitForPrint.LanguageKey = "LOT";
            this.grdWaitForPrint.Location = new System.Drawing.Point(0, 0);
            this.grdWaitForPrint.Margin = new System.Windows.Forms.Padding(0);
            this.grdWaitForPrint.Name = "grdWaitForPrint";
            this.grdWaitForPrint.ShowBorder = true;
            this.grdWaitForPrint.ShowStatusBar = false;
            this.grdWaitForPrint.Size = new System.Drawing.Size(614, 666);
            this.grdWaitForPrint.TabIndex = 0;
            // 
            // grdWaitForPrintPcs
            // 
            this.grdWaitForPrintPcs.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdWaitForPrintPcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdWaitForPrintPcs.IsUsePaging = false;
            this.grdWaitForPrintPcs.LanguageKey = "PCS";
            this.grdWaitForPrintPcs.Location = new System.Drawing.Point(0, 30);
            this.grdWaitForPrintPcs.Margin = new System.Windows.Forms.Padding(0);
            this.grdWaitForPrintPcs.Name = "grdWaitForPrintPcs";
            this.grdWaitForPrintPcs.ShowBorder = true;
            this.grdWaitForPrintPcs.ShowStatusBar = false;
            this.grdWaitForPrintPcs.Size = new System.Drawing.Size(484, 636);
            this.grdWaitForPrintPcs.TabIndex = 1;
            // 
            // smartPanel1
            // 
            this.smartPanel1.Controls.Add(this.txtCurrentSeq);
            this.smartPanel1.Controls.Add(this.smartLabel2);
            this.smartPanel1.Controls.Add(this.txtPcsNoScan);
            this.smartPanel1.Controls.Add(this.smartLabel1);
            this.smartPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smartPanel1.Location = new System.Drawing.Point(0, 0);
            this.smartPanel1.Name = "smartPanel1";
            this.smartPanel1.Size = new System.Drawing.Size(484, 30);
            this.smartPanel1.TabIndex = 2;
            // 
            // txtCurrentSeq
            // 
            this.txtCurrentSeq.EditValue = "";
            this.txtCurrentSeq.LabelText = null;
            this.txtCurrentSeq.LanguageKey = null;
            this.txtCurrentSeq.Location = new System.Drawing.Point(455, 5);
            this.txtCurrentSeq.Name = "txtCurrentSeq";
            this.txtCurrentSeq.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCurrentSeq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCurrentSeq.Size = new System.Drawing.Size(78, 20);
            this.txtCurrentSeq.TabIndex = 9;
            // 
            // smartLabel2
            // 
            this.smartLabel2.LanguageKey = "CURRENTSEQ";
            this.smartLabel2.Location = new System.Drawing.Point(401, 8);
            this.smartLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.smartLabel2.Name = "smartLabel2";
            this.smartLabel2.Size = new System.Drawing.Size(40, 14);
            this.smartLabel2.TabIndex = 8;
            this.smartLabel2.Text = "진행순번";
            // 
            // txtPcsNoScan
            // 
            this.txtPcsNoScan.EditValue = "";
            this.txtPcsNoScan.LabelText = null;
            this.txtPcsNoScan.LanguageKey = null;
            this.txtPcsNoScan.Location = new System.Drawing.Point(116, 5);
            this.txtPcsNoScan.Name = "txtPcsNoScan";
            this.txtPcsNoScan.Size = new System.Drawing.Size(262, 20);
            this.txtPcsNoScan.TabIndex = 9;
            // 
            // smartLabel1
            // 
            this.smartLabel1.LanguageKey = "PCSNOSCAN";
            this.smartLabel1.Location = new System.Drawing.Point(9, 8);
            this.smartLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.smartLabel1.Name = "smartLabel1";
            this.smartLabel1.Size = new System.Drawing.Size(72, 14);
            this.smartLabel1.TabIndex = 8;
            this.smartLabel1.Text = "PCS NO Scan";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.smartSpliterContainer2);
            this.xtraTabPage2.Controls.Add(this.splitter1);
            this.tabPartition.SetLanguageKey(this.xtraTabPage2, "PRINTED");
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1103, 666);
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
            this.smartSpliterContainer2.Panel2.Controls.Add(this.grdPrintedPpCase);
            this.smartSpliterContainer2.Panel2.Text = "Panel2";
            this.smartSpliterContainer2.Size = new System.Drawing.Size(1103, 663);
            this.smartSpliterContainer2.SplitterPosition = 631;
            this.smartSpliterContainer2.TabIndex = 3;
            this.smartSpliterContainer2.Text = "smartSpliterContainer2";
            // 
            // grdPrinted
            // 
            this.grdPrinted.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPrinted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPrinted.IsUsePaging = false;
            this.grdPrinted.LanguageKey = "LABELDEFINITIONLIST";
            this.grdPrinted.Location = new System.Drawing.Point(0, 0);
            this.grdPrinted.Margin = new System.Windows.Forms.Padding(0);
            this.grdPrinted.Name = "grdPrinted";
            this.grdPrinted.ShowBorder = true;
            this.grdPrinted.ShowStatusBar = false;
            this.grdPrinted.Size = new System.Drawing.Size(631, 663);
            this.grdPrinted.TabIndex = 0;
            // 
            // grdPrintedPpCase
            // 
            this.grdPrintedPpCase.Caption = "그리드제목( LanguageKey를 입력하세요)";
            this.grdPrintedPpCase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPrintedPpCase.IsUsePaging = false;
            this.grdPrintedPpCase.LanguageKey = "LABELCLASSLIST";
            this.grdPrintedPpCase.Location = new System.Drawing.Point(0, 0);
            this.grdPrintedPpCase.Margin = new System.Windows.Forms.Padding(0);
            this.grdPrintedPpCase.Name = "grdPrintedPpCase";
            this.grdPrintedPpCase.ShowBorder = true;
            this.grdPrintedPpCase.ShowStatusBar = false;
            this.grdPrintedPpCase.Size = new System.Drawing.Size(467, 663);
            this.grdPrintedPpCase.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1103, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // PpCasePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 762);
            this.Name = "PpCasePrint";
            this.Padding = new System.Windows.Forms.Padding(19);
            this.Text = "SmartConditionBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPartition)).EndInit();
            this.tabPartition.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer1)).EndInit();
            this.smartSpliterContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartPanel1)).EndInit();
            this.smartPanel1.ResumeLayout(false);
            this.smartPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentSeq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPcsNoScan.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smartSpliterContainer2)).EndInit();
            this.smartSpliterContainer2.ResumeLayout(false);
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
        private Framework.SmartControls.SmartBandedGrid grdPrintedPpCase;
        private Framework.SmartControls.SmartPanel smartPanel1;
        private Framework.SmartControls.SmartTextBox txtPcsNoScan;
        private Framework.SmartControls.SmartLabel smartLabel1;
        private Framework.SmartControls.SmartTextBox txtCurrentSeq;
        private Framework.SmartControls.SmartLabel smartLabel2;
    }
}
